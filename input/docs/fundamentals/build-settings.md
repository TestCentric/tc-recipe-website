Title: Build Settings
Description: Documents the build setting parameters, which drive the build
Order: 4
---
<style>
    td, th { border: solid 1px black; padding: 0 10px }
	th { background-color: #eee}
</style>

### Initialization

The static class `BuildSettings` holds the parameters used by your build. You initialize the settings immedieately after loading the recipe by calling the `BuildSettings.Initialize` method, which has the following signature:

```
	public static void Initialize(
	 // Required parameters
		ICakeContext context,
		string title,
		string githubRepository,
     
	 // Optional parameters
		bool suppressHeaderCheck = false,
		string[] standardHeader = null,
		string copyright = null,
		string[] exemptFiles = null,

		string solutionFile = null,
		string[] validConfigurations = null,
		bool msbuildAllowPreviewVersion = false,
		string githubOwner = "TestCentric",
		
		string unitTests = null, // Defaults to "**/*.tests.dll|**/*.tests.exe" (case insensitive)
		TestRunner unitTestRunner = null, // If not set, NUnitLite is used
		string unitTestArguments = null,

		string defaultTarget = null, // Defaults to "Build"
		
		// Verbosity
		Verbosity msbuildVerbosity = Verbosity.Minimal,
		NuGetVerbosity nugetVerbosity = NuGetVerbosity.Normal,
		bool chocolateyVerbosity = false );
```

NOTES:
1. Since the call to `Initialize` is made at the top level in the script, you can pass in the current `Context` directly.
2. Default values are used for all omitted parameters. You should only specify them if you don't want the default.
3. If no solution file is specified and only one is present in the directory, we use it.
4. Only the default unit test pattern is case-insensitive. If you specify a pattern yourself, you must use the correct casing. Best practice is to name your test files so that the default may be used.
5. In general, we use NUnitLite for unit tests.
6. The `unitTestArguments` parameter is handy for tracing all unit tests.

### Properties

`BuildSettings` exposes a number of properties, which are used in the recipe and may also be used by your own scripts, provided that `BuildSettings.Initialize` has been called at the point where you use them. Although a few properties allow set access because of how they are used internally, you should not change any of them in your own code.

<table>
<tr><th>Property Name</th><th>Type</th><th>Usage</th>

<tr><td>Context</td><td>ICakeContext</td><td>The Cake context under which we are running</td>
<tr><td>Target</td><td>string</td><td>The target task chosen by the user</td></tr>
<tr><td>TasksToExecute</td><td>string[]</td><td>Array of tasks which will be run</td></tr>
<tr><td>IsLocalBuild</td><td>bool</td><td>True if this is a local (desktop) build</td></tr>
<tr><td>IsRunningOnUnix</td><td>bool</td><td>True if we are running on Unix or Linux</td></tr>
<tr><td>IsRunningOnWindows</td><td>bool</td><td>True if we are running on Windows</td></tr>
<tr><td>IsRunningOnAppVeyor</td><td>bool</td><td>True if we are running under AppVeyor CI</td></tr>

<tr><th colspan=3>Building</th></tr>
<tr><td>Title</td><td>string</td><td>The title of this project</td></tr>
<tr><td>SolutionFile</td><td>string</td><td>File name of the solution</td></tr>
<tr><td>Configuration</td><td>string</td><td>The selected configuration</td></tr>
<tr><td>ValidConfigurations</td><td>string[]</td><td>Array of valid configurations, for checking</td></tr>
<tr><td>SuppressHeaderCheck</td><td>bool</td><td>If true, we skip checking the headers of source files</td></tr>
<tr><td>StandardHeader</td><td>string[]</td><td>Standard header text</td></tr>
<tr><td>ExemptFiles</td><td>string[]</td><td>File names exempt from checking</td></tr>
<tr><td>MSBuildAllowPreviewVersion</td><td>bool</td><td>Used for testing preview versions of .NET</td></tr>
<tr><td>MSBuildVerbosity</td><td>Verbosity</td><td>Verbosity for MSBuild operations</td></tr>
<tr><td>MSBuildSettings</td><td>MSBuildSettings</td><td>Standard MSBuildSettings</td></tr>
<tr><td>ShouldAddToLocalFeed</td><td>bool</td><td>If true, each bu</td></tr>
<tr><td>NuGetVerbosity</td><td>NuGetVerbosity</td><td>Verbosity for NuGet operations</td></tr>
<tr><td>ChocolateyVerbosity</td><td>bool</td><td>Verbosity for Chocolatey operations (true => Verbose)</td></tr>

<tr><th colspan=3>Versioning</th></tr>
<tr><td>BuildVersion</td><td>BuildVersion</td><td>Object used to calculate versions</td></tr>
<tr><td>BranchName</td><td>string</td><td>Tne name of the current branch</td></tr>
<tr><td>IsReleaseBranch</td><td>bool</td><td>True if we are on a release branch (release-x.y.z)</td></tr>
<tr><td>PackageVersion</td><td>string</td><td>The full package version, including any pre-release suffix</td></tr>
<tr><td>AssemblyVersion</td><td>string</td><td>The AssemblyVersion</td></tr>
<tr><td>AssemblyFileVersion</td><td>string</td><td>The AssemblyFileVersion</td></tr>
<tr><td>AssemblyInformationalVersion</td><td>string</td><td>The AssemblyInformationalVersion</td></tr>
<tr><td>IsPreRelease</td><td>bool</td><td>True if this is a pre-release</td></tr>

<tr><th colspan=3>Unit Testing</th></tr>
<tr><td>UnitTests</td><td>string</td><td>Pattern used to recognize unit test assemblies</td></tr>
<tr><td>UnitTestRunner</td><td>TestRunner</td><td>An instance of the runner used for unit tests</td></tr>

<tr><th colspan=3>Packaging</th></tr>
<tr><td>Packages</td><td>List&lt;PackageDefinition&gt;</td><td>List of package definitions for this project</td></tr>
<tr><td>PackageTestLevel</td><td>int</td><td>Level of package testing: 1, 2 or 3</td></tr>
<tr><td>MyGetPushUrl</td><td>string</td><td>URL for publishing to MyGet</td></tr>
<tr><td>MyGetApiKey</td><td>string</td><td>MyGet API key</td></tr>
<tr><td>NuGetPushUrl</td><td>string</td><td>URL for publishing to NuGet</td></tr>
<tr><td>NuGetApiKey</td><td>string</td><td>NuGet API key</td></tr>
<tr><td>ChocolateyPushUrl</td><td>string</td><td>URL for publishing to chocolatey</td></tr>
<tr><td>ChocolateyApiKey</td><td>string</td><td>Chocolatey API Key</td></tr>
<tr><td>GitHubOwner</td><td>string</td><td>The GitHub project owner, normally "TestCentric"</td></tr>
<tr><td>GitHubRepository</td><td>string</td><td>The GitHub repository for this project</td></tr>
<tr><td>GitHubAccessToken</td><td>string</td><td>The GitHub access token</td></tr>
<tr><td>ShouldPublishToMyGet</td><td>bool</td><td>True if this build should publish to MyGet</td></tr>
<tr><td>ShouldPublishToNuGet</td><td>bool</td><td>True if this build should publish to NuGet</td></tr>
<tr><td>ShouldPublishToChocolatey</td><td>bool</td><td>True if this build should publish to Chocolatey</td></tr>
<tr><td>IsProductionRelease</td><td>bool</td><td>True if this is a production release</td></tr>
<tr><td>IsDevelopmentRelease</td><td>bool</td><td>True if this is a dev release, which publishes only to MyGet</td></tr>

<tr><th colspan=3>Standard Directory Paths</th></tr>
<tr><td>ProjectDirectory</td><td>string</td><td>Project root directory</td></tr>
<tr><td>SourceDirectory</td><td>string</td><td>./src</td></tr>
<tr><td>OutputDirectory</td><td>string</td><td>./bin/CONFIGURATION</td></tr>
<tr><td>ZipDirectory</td><td>string</td><td>./zip</td></tr>
<tr><td>NuGetDirectory</td><td>string</td><td>./nuget</td></tr>
<tr><td>ChocolateyDirectory</td><td>string</td><td>./choco</td></tr>
<tr><td>PackagingDirectory</td><td>string</td><td>./packaging</td></tr>
<tr><td>ZipImageDirectory</td><td>string</td><td>./zipimage</td></tr>
<tr><td>ToolsDirectory</td><td>string</td><td>./tools</td></tr>
<tr><td>PackageTestDirectory</td><td>string</td><td>./packaging/tests</td></tr>
<tr><td>NuGetTestDirectory</td><td>string</td><td>./packaging/tests/nuget</td></tr>
<tr><td>ChocolateyTestDirectory</td><td>string</td><td>./packaging/tests/chocolatey</td></tr>
<tr><td>ZipTestDirectory</td><td>string</td><td>./packaging/tests/zip</td></tr>
<tr><td>PackageResultDirectory</td><td>string</td><td>./packaging/results</td></tr>
<tr><td>NuGetResultDirectory</td><td>string</td><td>./packaging/results/NuGet</td></tr>
<tr><td>ChocolateyResultDirectory</td><td>string</td><td>./packaging/results/chocolatey</td></tr>
<tr><td>ZipResultDirectory</td><td>string</td><td>./packaging/results/zip</td></tr>
<tr><td>LocalPackagesDirectory</td><td>string</td><td>../LocalPackages</td></tr>

</table>

NOTES:
1. There is only one PackageVersion for each project, which means that all packages produced in a build must use the same version.
2. We are no longer using zip packages, so the various zip directories will be removed at some future date.