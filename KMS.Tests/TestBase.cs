using System.Security.Cryptography;
using Amazon.KeyManagementService;
using Amazon.Runtime;
using static KMS.Tests.TestMother;

namespace KMS.Tests;

public abstract class TestBase
{
    protected static AmazonKeyManagementServiceClient GetClient()
    {
        //valid AWS_ACCESS_KEY_ID and AWS_SECRET_ACCESS_KEY env vars must be set
        return new AmazonKeyManagementServiceClient(
            new EnvironmentVariablesAWSCredentials(),
            new AmazonKeyManagementServiceConfig
            {
                RegionEndpoint = RegionEndpoint
            });
    }

    protected static byte[] CreateNewEncryptionKey()
    {
        var key = new byte[32];
        RandomNumberGenerator.Fill(key);
        return key;
    }
}