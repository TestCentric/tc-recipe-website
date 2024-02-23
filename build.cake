using Path = System.IO.Path;

var target = Argument("target", "Default");

const string GIT = "git";

var OUTPUT_DIR = Path.GetFullPath("output/");

var TARGET_PROJECT = "testcentric.github.io";
var TARGET_PROJECT_URI = $"https://github.com/TestCentric/{TARGET_PROJECT}";
var PREVIEW_URI = "http://localhost#5080";

var DEPLOY_DIR_NAME = "deploy/";
var DEPLOY_DIR = Path.GetFullPath(DEPLOY_DIR_NAME);
var DEPLOY_BRANCH = "master";

const string USER_ID = "USER_ID";
const string USER_EMAIL = "USER_EMAIL";
const string GITHUB_ACCESS_TOKEN = "GITHUB_ACCESS_TOKEN";

string UserId;
string UserEmail;
string GitHubAccessToken;

Setup((context) =>
{
    UserId = context.EnvironmentVariable(USER_ID);
    UserEmail = context.EnvironmentVariable(USER_EMAIL);
    GitHubAccessToken = context.EnvironmentVariable(GITHUB_ACCESS_TOKEN);
});

Task("Build")
    .Does(() => StartProcess("dotnet", "run"));

Task("Preview")
    .Does(() => StartProcess("dotnet", "run -- preview"));

Task("Deploy")
    .IsDependentOn("Build")
    .Does(() => 
    {
        if(FileExists("./CNAME"))
            CopyFileToDirectory("./CNAME", OUTPUT_DIR);

        if (DirectoryExists(DEPLOY_DIR))
            DeleteDirectory(DEPLOY_DIR, new DeleteDirectorySettings {
                Recursive = true,
                Force = true
            });

        GitCommand($"clone {TARGET_PROJECT_URI} {DEPLOY_DIR}");

        CopyDirectory(OUTPUT_DIR, DEPLOY_DIR);

        GitCommand("add .", DEPLOY_DIR);
        GitCommand("commit -m \"Deploy site to GitHub Pages\"", DEPLOY_DIR);
        GitCommand("push -u origin master", DEPLOY_DIR);

        void GitCommand(string arguments, string workDir = null)
        {
            var settings = new ProcessSettings()
            {
                Arguments = arguments,
                WorkingDirectory = workDir
            };

            int rc = StartProcess(GIT, settings);
            if (rc != 0)
                throw new Exception($"Received result of {rc} from 'git {arguments}'");
        }
    });

Task("Default")
    .IsDependentOn("Build");
    
RunTarget(target);
