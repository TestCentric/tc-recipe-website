Title: build.cake
Description: Creating your build.cake file
Order: 2
---
To use **TestCentric.Recipe.Cake** you set up a `build.cake` file with a special structure. The following is an example of a simple `build.cake` file for a project which creates a single package.

```
#load nuget:?package=TestCentric.Cake.Recipe&version=1.1.0 // Note 1

BuildSettings.Initialize(                                  // Note 2
    context: Context,
    title: "Sample Application",
    githubRepository: "TestCentric.Sample.Application");

BuildSettings.Packages.Add(                                // Note 3
    new NuGetPackage(
        id: "TestCentric.Sample.Application",
        source: "nuget/TestCentric.Sample.Application.nuspec",
        checks: new PackageCheck[] {
            HasFiles(
                "LICENSE.txt", "README.md", "testcentric.png",
                "lib/net8.0/testcentric.sample.application.dll") }));

Build.Run();                                               // Note 4
```

**NOTES:**

1. The `#load` statement loads `TestCentric.Cake.Recipe`, specifying the version to use, 
   in this case version 1.1.0. See [LoadStatement](xref:load-statement).

2. `BuildSettings.Initialize` sets the parameters which drive the build process. The example
   specifies only the three required paraameters. See [Build Settings](xref:build-settings) for a complete list of parameters.

3. Define a single `nuget` package. See [Package Definition](xref:package-definition) for more information. Once constructed, the package is added to `BuildSettings.Packages`.

4. Run the selected target. See [RunCommand](xref:run-command).

