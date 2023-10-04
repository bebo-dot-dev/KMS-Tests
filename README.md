## KMS.Tests

.NET6 AWS KMS integration tests

### KMS Test Prerequisites

* An active AWS account (free tier is enough)
* A valid access key/secret configured for an IAM user
* `AWS_ACCESS_KEY_ID` and `AWS_SECRET_ACCESS_KEY` environment variables set to a valid access key/secret

### Nuget packages used

```
<PackageReference Include="AWSSDK.KeyManagementService" Version="3.7.200.49" />
<PackageReference Include="FluentAssertions" Version="6.11.0" />
<PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="6.0.21" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.6.3" />
<PackageReference Include="NUnit" Version="3.13.3" />
<PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
```

### Build requirements

* .NET6 SDK
* Optional: an IDE i.e. Visual Studio Code / Rider / Visual Studio

### Running the tests

```
$ dotnet test ./KMS.Tests/KMS.Tests.csproj 
  Determining projects to restore...
  All projects are up-to-date for restore.
  KMS.Tests -> /home/joe/Code/git-repos/KMS.Tests/KMS.Tests/bin/Debug/net6.0/KMS.Tests.dll
Test run for /home/joe/Code/git-repos/KMS.Tests/KMS.Tests/bin/Debug/net6.0/KMS.Tests.dll (.NETCoreApp,Version=v6.0)
Microsoft (R) Test Execution Command Line Tool Version 17.4.1+79d56b02b69a582cd90428878a5e9411ab7538f5 (x64)
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     4, Skipped:     0, Total:     4, Duration: 1 s - KMS.Tests.dll (net6.0)
```