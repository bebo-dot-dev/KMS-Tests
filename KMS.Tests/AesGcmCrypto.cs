using System.Security.Cryptography;
using System.Text;

namespace KMS.Tests;

public static class AesGcmCrypto
{
    public static string Encrypt(byte[] key, string plaintext)
    {
        using var aes = new AesGcm(key);
        
        var nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
        RandomNumberGenerator.Fill(nonce);

        var tag = new byte[AesGcm.TagByteSizes.MaxSize];

        var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
        var cipherBytes = new byte[plaintextBytes.Length];

        aes.Encrypt(nonce, plaintextBytes, cipherBytes, tag);

        var base64Nonce = Convert.ToBase64String(nonce);
        var base64CipherText = Convert.ToBase64String(cipherBytes);
        var base64Tag = Convert.ToBase64String(tag);

        return $"{base64Nonce}|{base64CipherText}|{base64Tag}";
    }

    public static string Decrypt(byte[] key, string cipherText)
    {
        var parts = cipherText.Split("|");

        var nonceBytes = Convert.FromBase64String(parts[0]);
        var cipherTextBytes = Convert.FromBase64String(parts[1]);
        var tagBytes = Convert.FromBase64String(parts[2]);
        
        var plaintextBytes = new byte[cipherTextBytes.Length];

        using var aes = new AesGcm(key);
        aes.Decrypt(nonceBytes, cipherTextBytes, tagBytes, plaintextBytes);

        return Encoding.UTF8.GetString(plaintextBytes);
    }
}