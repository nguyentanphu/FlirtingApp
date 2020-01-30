#tool "nuget:?package=coveralls.io&version=1.4.2"
#addin Cake.Git
#addin nuget:?package=Nuget.Core
#addin "nuget:?package=Cake.Coveralls&version=0.10.1"
#addin "nuget:?package=Cake.Coverlet&version=2.3.4"

using NuGet;

var target = Argument("target", "Default");
var releaseConfig = "Release";
var artifactsDir = "./artifacts/";
var solutionPath = "./FlirtingApp.sln";

Task("CleanUp")
  .Does(() => {
    if (DirectoryExists(artifactsDir))
    {
      DeleteDirectory(artifactsDir, new DeleteDirectorySettings {
        Force = true,
        Recursive = true
      });
    }

    CreateDirectory(artifactsDir);
    DotNetCoreClean(solutionPath);
  });

Task("Restore")
  .Does(() => {
    DotNetCoreRestore(solutionPath);
  });

Task("Build")
  .IsDependentOn("CleanUp")
  .IsDependentOn("Restore")
  .Does(() => {
      DotNetCoreBuild(
          solutionPath,
          new DotNetCoreBuildSettings 
          {
              Configuration = releaseConfig,
              NoRestore = true
          }
      );
  });

var coverageFileName = "coverage.xml";
var testProjectsRelativePaths = new string[] {
  "./tests/FlirtingApp.Application.Tests/FlirtingApp.Application.Tests.csproj",
  "./tests/FlirtingApp.Domain.Tests/FlirtingApp.Domain.Tests.csproj",
  "./tests/FlirtingApp.Infrastructure.Tests/FlirtingApp.Infrastructure.Tests.csproj",
  "./tests/FlirtingApp.Persistent.Tests/FlirtingApp.Persistent.Tests.csproj",
  "./tests/FlirtingApp.WebApi.Tests/FlirtingApp.WebApi.Tests.csproj"
};
Task("Test")
  .IsDependentOn("Build")
  .Does(() => {
    var testSettings = new DotNetCoreTestSettings {
      Configuration = releaseConfig,
      NoBuild = true
    };

    var tempJson = artifactsDir + "temp.json";
    var coverletSettings = new CoverletSettings {
        CollectCoverage = true,
        CoverletOutputFormat = CoverletOutputFormat.json,
        CoverletOutputDirectory = artifactsDir,
        CoverletOutputName = tempJson
    };

    DotNetCoreTest(testProjectsRelativePaths[0], testSettings, coverletSettings);

    coverletSettings.MergeWithFile = tempJson;
    for (int i = 1; i < testProjectsRelativePaths.Length; i++)
    {
        if (i == testProjectsRelativePaths.Length - 1)
        {
            coverletSettings.CoverletOutputFormat  = CoverletOutputFormat.opencover;
            coverletSettings.CoverletOutputName = coverageFileName;
        }

        DotNetCoreTest(testProjectsRelativePaths[i], testSettings, coverletSettings);
    }

      // MoveFile(@".\coverage-test\" + coverageResultsFileName, artifactsDir + coverageResultsFileName);
  });

Task("UploadToCoverall")
  .IsDependentOn("Test")
  .Does(() => {
    var coverallsToken = Argument<string>("coverallsToken", null);
    
    CoverallsIo(artifactsDir + coverageFileName, new CoverallsIoSettings()
    {
        RepoToken = coverallsToken
    });
  });

Task("Default")
  .IsDependentOn("Build")
  .IsDependentOn("Test")
  .IsDependentOn("UploadToCoverall");

Task("BuildAndTest")
  .IsDependentOn("Build")
  .IsDependentOn("Test");

RunTarget(target);