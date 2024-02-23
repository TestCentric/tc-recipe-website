Title: Package Definition
Description: Shows how packages are defined in the buid script
Order: 5
---
<style>
    td, th { border: solid 1px black; padding: 0 10px }
	th { background-color: #eee}
</style>

Currently, `TestCentric.Cake.Recipe` supports multiple packages generated from a single build. The recipe was designed in this way becuase we have historically had a few projects which generated a number of different packages. In addition, some projects produce a single logical package using multiple formats. Consequently, now and for the forseeable future, we will continue to allow more than one package per project.

A packages is defined defined by creatimg an instance of `PackageDefinition`, which is an abstract class. In your script, you use one or more of the derived concrete classes: `NuGetPackage`, `ChocolateyPackage`, `ZipPackage` or `RecipePackage`. Package definitions must follow the call to `BuildSettings.Initialize`. Each package definition is then added to the `BuildSettings.Packages` property.

### Package Types

#### NuGetPackage

`NuGetPackage` is our most common package type, used for packages we publish at **nuget.org**. Its constructor is as follows:

```
public NuGetPackage(
    string id,
    string title = null,                               
    string description = null,
    string summary = null,
    string[] releaseNotes = null,
    string[] tags = null,
    string source = null,
    string basePath = null,
    TestRunner testRunner = null,
    string extraTestArguments = null,
    PackageCheck[] checks = null,
    PackageCheck[] symbols = null,
    IEnumerable<PackageTest> tests = null, 
    PackageReference[] preloadedExtensions = null,
    PackageContent packageContent = null)
```

The required `id` argument is used as the package id. Arguments `title`, `description`, `summary`, `releaseNotes` and `tags` are used in creating the corresponding metadata elements in the package.

The remaining arguments are discussed separately in the sections that follow.


#### ChocolateyPackage

A `ChocolateyPackage` is also a nuget package, but is tailored for publication at **chocolatey.org**, which has different naming standards and additional package content. The constructor has all the same arguments as for a `NuGetPackage` although the `symbol` argument is ignored.


```
public ChocolateyPackage(
    string id,
    string summary = null,
    string description = null,
    string[] releaseNotes = null,
    string[] tags = null,
    string source = null, 
    string basePath = null,
    TestRunner testRunner = null,
    string extraTestArguments = null,
    PackageCheck[] checks = null,
    PackageCheck[] symbols = null,
    IEnumerable<PackageTest> tests = null,
    PackageReference[] preloadedExtensions = null,
    PackageContent packageContent = null)
```

#### ZipPackage

We previously used `ZipPackage` to provide alternative distributions for some of our nuget packages. Although we no longer do this, the package type is still supported in case we need it and to allow re-creation of older packages when necessary. This package type
will most likely be removed in a future release.

```
public ZipPackage(
    string id,
    string source = null,
    string basePath = null,
    TestRunner testRunner = null,
    PackageCheck[] checks = null,
    IEnumerable<PackageTest> tests = null,
    PackageReference[] preloadedExtensions = null,
    ExtensionSpecifier[] bundledExtensions = null)
```

#### RecipePackage

`RecipePackage` derives from `NuGetPackage` and differs from it because there is never a solution file to build, only `cake` files delivered as content. The `TestCentric.Cake.Recipe` package is of this type.

```
public RecipePackage(
    string id,
    string title = null,
    string description = null,
    string summary = null,
    string[] releaseNotes = null,
    string[] tags = null,
    string basePath = null,
    string content = "./recipe/*.cake")
```

The `content` argument is a pattern providing the list of files to be included.

### Building Packages

Packages are built in dfferent ways depending on the value of the `source`, `basepath` and `packageContent` arguments.

### Verifying Packages

If the `checks` argument is non-null the package content is checked after it is built.

### Testing Packages

If a package contains tests (`PackageTests` property), they are run after it has been successfully verified.
