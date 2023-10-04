using Amazon;

namespace KMS.Tests;

public static class TestMother
{
    public static readonly RegionEndpoint RegionEndpoint = RegionEndpoint.EUWest1;

    public static string KMSKeyId = "arn:aws:kms:eu-west-1:398477550730:key/26108461-13c9-4420-898b-7caae65e0fae";
    
    public static string PlainTextValue = "Plain text value";

    public static string EncryptedValue =
        "AQICAHgTpbgBx1s7dnvnp3+Wrqt/SJ7ZYeUCuFrXslR5mGWh3wGr5Y9YWE3RkH8zVE55pry0AAAAbjBsBgkqhkiG9w0BBwagXzBdAgEAMFgGCSqGSIb3DQEHATAeBglghkgBZQMEAS4wEQQMLZrMeRmmDXcL+DaoAgEQgCs65WG+Jgr9SVMh9lM3drVssELn76zCt51kAoHnbgBpHp3+rzhdzv5Q43AZ";

    public static string StartingKey = "yv6BifMa31ZzlXboxS9xiz1HhjVb/M3jvp4EVAjUmdU=";
    
    public static string EncryptedKeyWithStoredSymmetricCipherKey = 
        "J1wfT/FKoAUFyI4l|" +
        "yDqFBCz5S/t4TeEeI9OGB5DJv0DayMYJHEfjHF2lclgZsuAWKo+JbOa/uBM=|" +
        "oOyCOfxpoL7V8FN40u2nWg==|" +
        "AQIDAHgTpbgBx1s7dnvnp3+Wrqt/SJ7ZYeUCuFrXslR5mGWh3wFqFVfMy/DkIKkE7J7VHLF+AAAAfjB8BgkqhkiG9w0BBwagbzBtAgEAMGgGCSqGSIb3DQEHATAeBglghkgBZQMEAS4wEQQM8BZQ/BhTdTAhhtjCAgEQgDuK5DA3DvvkOkAhTekscioKjDJpvZz1S2sIZhnWdWeEvIxZjzSlMwxEV7xZewG1Vjggqid/ossfmeSnig==";
}