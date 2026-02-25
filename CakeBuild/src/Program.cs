namespace CakeBuild;

using System.IO;

using Cake.Common;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Clean;
using Cake.Common.Tools.DotNet.Publish;
using Cake.Core;
using Cake.Frosting;
using Cake.Json;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Vintagestory.API.Common;

public static class Program {
    public static int Main(string[] args) =>
        new CakeHost()
            .UseContext<BuildContext>()
            .Run(args);
}

public class BuildContext : FrostingContext {
    public static readonly string ProjectRootPath = Path.GetFullPath("..");
    public const string ProjectName = "Glek.VintageStory.AFKMod";
    public string BuildConfiguration { get; }
    public string Version { get; }
    public string Name { get; }
    public bool SkipJsonValidation { get; }

    public BuildContext(ICakeContext context)
        : base(context) {
        this.BuildConfiguration = context.Argument("configuration", "Release");
        this.SkipJsonValidation = context.Argument("skipJsonValidation", false);
        var modInfo = context.DeserializeJsonFromFile<ModInfo>($"../{ProjectName}/modinfo.json");
        this.Version = modInfo.Version;
        this.Name = modInfo.ModID;
    }
}

[TaskName("ValidateJson")]
public sealed class ValidateJsonTask : FrostingTask<BuildContext> {
    public override void Run(BuildContext context) {
        if (context.SkipJsonValidation) {
            return;
        }
        var jsonFiles = context.GetFiles($"../{BuildContext.ProjectName}/assets/**/*.json");
        foreach (var file in jsonFiles) {
            try {
                var json = File.ReadAllText(file.FullPath);
                JToken.Parse(json);
            } catch (JsonException ex) {
                throw new JsonException($"Validation failed for JSON file: {file.FullPath}{Environment.NewLine}{ex.Message}", ex);
            }
        }
    }
}

[TaskName("Build")]
[IsDependentOn(typeof(ValidateJsonTask))]
public sealed class BuildTask : FrostingTask<BuildContext> {
    public static readonly string ProjectFilePath = Path.Join(
        BuildContext.ProjectRootPath,
        $"{BuildContext.ProjectName}",
        $"{BuildContext.ProjectName}.csproj"
    );

    public override void Run(BuildContext context) {
        context.DotNetClean(ProjectFilePath,
            new DotNetCleanSettings {
                Configuration = context.BuildConfiguration
            });


        context.DotNetPublish(ProjectFilePath,
            new DotNetPublishSettings {
                Configuration = context.BuildConfiguration
            });
    }
}

[TaskName("Package")]
[IsDependentOn(typeof(BuildTask))]
public sealed class PackageTask : FrostingTask<BuildContext> {
    public static readonly string ProjectAssetsRootPath = Path.Join(
        BuildContext.ProjectRootPath,
        BuildContext.ProjectName,
        "assets"
    );

    public static readonly string ProjectModInfoPath = Path.Join(
        BuildContext.ProjectRootPath,
        BuildContext.ProjectName,
        "modinfo.json"
    );

    public static readonly string ProjectModIconPath = Path.Join(
        BuildContext.ProjectRootPath,
        BuildContext.ProjectName,
        "modicon.png"
    );

    public static readonly string DistDir = Path.Join(
        BuildContext.ProjectRootPath,
        "dist"
    );

    public override void Run(BuildContext context) {
        var archivePath = Path.Join(DistDir, $"{context.Name}_{context.Version}.zip");
        var buildPath = Path.Join(DistDir, context.Name);
        var assetsBuildPath = Path.Join(buildPath, "assets");
        var builtCodePath = Path.Join(
            BuildContext.ProjectRootPath,
            BuildContext.ProjectName,
            "bin",
            context.BuildConfiguration,
            "Mods",
            "mod",
            "publish",
            "*"
        );
        context.EnsureDirectoryExists(DistDir);
        context.CleanDirectory(DistDir);
        context.EnsureDirectoryExists(buildPath);
        context.CopyFiles(builtCodePath, buildPath);
        if (context.DirectoryExists(ProjectAssetsRootPath)) {
            context.CopyDirectory(ProjectAssetsRootPath, assetsBuildPath);
        }
        context.CopyFile(ProjectModInfoPath, Path.Join(buildPath, "modinfo.json"));
        if (context.FileExists(ProjectModIconPath)) {
            context.CopyFile(ProjectModIconPath, Path.Join(buildPath, "modicon.png"));
        }
        context.Zip(buildPath, archivePath);
        context.DeleteDirectory(buildPath, new DeleteDirectorySettings() {
            Recursive = true,
            Force = true
        });
    }
}

[TaskName("Default")]
[IsDependentOn(typeof(PackageTask))]
public class DefaultTask : FrostingTask { }
