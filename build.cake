#tool "nuget:?package=coveralls.io&version=1.4.2"
#addin Cake.Git
#addin nuget:?package=Nuget.Core
#addin "nuget:?package=Cake.Coveralls&version=0.10.1"
#addin "nuget:?package=Cake.Coverlet&version=2.3.4"

using NuGet;

var target = Argument("target", "Complete");
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
Task("Test")
  .IsDependentOn("Build")
  .Does(() => {
    var settings = new DotNetCoreTestSettings {
      Configuration = releaseConfig,
      NoBuild = true
    };

    var coverletSettings = new CoverletSettings {
        CollectCoverage = true,
        CoverletOutputFormat = CoverletOutputFormat.opencover,
        CoverletOutputDirectory = artifactsDir,
        CoverletOutputName = coverageFileName
    };

      DotNetCoreTest(solutionPath, settings, coverletSettings);
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

Task("Complete")
  .IsDependentOn("Build")
  .IsDependentOn("Test")
  .IsDependentOn("UploadToCoverall");

RunTarget(target);