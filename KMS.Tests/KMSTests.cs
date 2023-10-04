using System.Text;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using FluentAssertions;
using NUnit.Framework;
using static KMS.Tests.TestMother;

namespace KMS.Tests;

public class KmsTests : TestBase
{
    [Test]
    public async Task EncryptAsync_WhenEncryptingPlainText_ExpectCanDecryptCipheredText()
    {
        //arrange
        var encryptRequest = new EncryptRequest
        {
            KeyId = KMSKeyId,
            Plaintext = new MemoryStream(Encoding.UTF8.GetBytes(PlainTextValue))
        };
        
        using var client = GetClient();

        //act
        var encryptResponse = await client.EncryptAsync(encryptRequest);
        var base64Encrypted = Convert.ToBase64String(encryptResponse.CiphertextBlob.ToArray());
        
        var decryptRequest = new DecryptRequest
        {
            KeyId = KMSKeyId,
            CiphertextBlob = ConvertToMemoryStream(base64Encrypted)
        };

        //act
        var decryptResponse = await client.DecryptAsync(decryptRequest);
        var plainDecrypted = Encoding.UTF8.GetString(decryptResponse.Plaintext.ToArray());
        
        //assert
        plainDecrypted.Should().Be(PlainTextValue);
    }
    
    [Test]
    public async Task DecryptAsync_WhenDecryptingBase64CipheredText_ExpectCanDecryptCipheredText()
    {
        //arrange
        var decryptRequest = new DecryptRequest
        {
            KeyId = KMSKeyId,
            CiphertextBlob = ConvertToMemoryStream(EncryptedValue)
        };
        
        using var client = GetClient();

        //act
        var decryptResponse = await client.DecryptAsync(decryptRequest);
        var plainDecrypted = Encoding.UTF8.GetString(decryptResponse.Plaintext.ToArray());
        
        //assert
        plainDecrypted.Should().Be(PlainTextValue);
    }
    
    [Test]
    public async Task GenerateDataKeyAsync_WhenEncryptedWithSymmetricPlainKey_ExpectCanDecryptWithSymmetricCipherKey()
    {
        //arrange
        //create a new encryption key
        var newKey = CreateNewEncryptionKey();
        var startKey = Convert.ToBase64String(newKey);
        
        var dataKeyRequest = new GenerateDataKeyRequest
        {
            KeyId = KMSKeyId,
            KeySpec = DataKeySpec.AES_256
        };
        
        using var client = GetClient();
        
        //generate a data key and have it return the plain key for the follow on encryption operation at (1)
        var dataKeyResponse = await client.GenerateDataKeyAsync(dataKeyRequest);
        var dataKeyPlainKey = dataKeyResponse.Plaintext;
        var dataKeyCipherKey = dataKeyResponse.CiphertextBlob;
        
        //act
        //(1) encrypt the new key with the generated data key plain key
        var encryptedKey = AesGcmCrypto.Encrypt(dataKeyPlainKey.ToArray(), startKey);
        
        //tack on the data key cipher key for the follow on decryption operation at (2)
        var encryptedKeyWithCipherKey = $"{encryptedKey}|{Convert.ToBase64String(dataKeyCipherKey.ToArray())}";
        var cipherKey = Convert.FromBase64String(encryptedKeyWithCipherKey.Split("|").Last());

        var decryptRequest = new DecryptRequest
        {
            KeyId = KMSKeyId,
            CiphertextBlob = new MemoryStream(cipherKey)
        };

        //(2) decrypt the original data key cipher key to a plain key for use in the follow on decryption operation at (3)
        var decryptResponse = await client.DecryptAsync(decryptRequest);
        var cipherText = encryptedKeyWithCipherKey.Remove(encryptedKeyWithCipherKey.LastIndexOf("|", StringComparison.Ordinal));
        //(3) decrypt the key back to the original key
        var decryptedKey = AesGcmCrypto.Decrypt(decryptResponse.Plaintext.ToArray(), cipherText);
        
        //assert
        decryptedKey.Should().Be(startKey);
    }
    
    [Test]
    public async Task GenerateDataKeyAsync_WhenGivenPreEncryptedKey_ExpectCanDecryptWithSymmetricCipherKey()
    {
        //arrange
        var cipherKey = Convert.FromBase64String(EncryptedKeyWithStoredSymmetricCipherKey.Split("|").Last());

        var decryptRequest = new DecryptRequest
        {
            KeyId = KMSKeyId,
            CiphertextBlob = new MemoryStream(cipherKey)
        };
        
        //act
        using var client = GetClient();
        var decryptResponse = await client.DecryptAsync(decryptRequest);
        var reducedKey = EncryptedKeyWithStoredSymmetricCipherKey.Remove(EncryptedKeyWithStoredSymmetricCipherKey.LastIndexOf("|", StringComparison.Ordinal));
        var dataKeyDecryptedKey = AesGcmCrypto.Decrypt(decryptResponse.Plaintext.ToArray(), reducedKey);
        
        //assert
        dataKeyDecryptedKey.Should().Be(StartingKey);
    }
    
    private static MemoryStream ConvertToMemoryStream(string base64String)
    {
        var bytes = Convert.FromBase64String(base64String);
        return new MemoryStream(bytes);
    }
}