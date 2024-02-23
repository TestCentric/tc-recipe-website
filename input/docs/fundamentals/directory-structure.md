Title: Directory Structure
Description: Standard TestCentric project directory structure
Order: 1
---
The recipe assumes that the standard TestCentric project directory structure is used. While it's possible to change the structure, it might cause some features to fail.

```
$ (Project Root)
  bin/                      // Output directories for the build
    Debug/
    Release/
  choco/                    // nuspecs and associated files for chooclatey.org
  nuget/                    // nuspecs and associated files for nuget.org
  packaging/                // Packages are created and tested here
  src/
    PROJECT                 // One folder per project
      PROJECT.csproj
      source code
  build.cake                // The primary build script
  build.cmd                 // Runs the build script under windwows
  build.ps1                 // Runs the build script under Windows powershell
  build.sh                  // Runs the build script under Linux
  LICENSE.txt               // License for this project
  NuGet.config              // Configuration file for nuget
  README.md                 // Project README
  SOLUTION.sln              // Our solution file
  testcentric.png           // TestCentric icon
```

Note that the `choco` and `nuget` directories are only present if the project creates the corresponding package type using a nuspec. Of course, it's possible to create the packages without a nuspec, as discussed in [Package Definition](xref:package-definition).