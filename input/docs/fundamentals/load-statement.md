Title: Load Statement
Description: The pre-processor statement, which loads the recipe
Order: 3
---
The first line of your `build.cake` file should look something like this:

```
#load nuget:?package=TestCentric.Cake.Recipe&version=1.1.0
```

This causes Cake to download the recipe package and make it available to your script. It is generally recommended that you specify the recipe version to avoid unpleasant surprises when the recipe is updated. This is a requirement for any TestCentric projects.

The `build.cake` for some projects may start with a slightly more complex set of statements, like this:

```
#load nuget:?package=TestCentric.Cake.Recipe&version=1.1.0
// Comment out above line and uncomment below for local tests of recipe changes
//#load ../TestCentric.Cake.Recipe/recipe/*.cake
```

This is used for local testing when the recipe itself is changed. Since it only works locally, a file using the local load statement should never be checked in.