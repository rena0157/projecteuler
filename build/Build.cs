using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GlobExpressions;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;

    [Parameter("The project to run")] readonly string Project;
    
    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath OutputDirectory => RootDirectory / "output";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(OutputDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target Publish => _ => _
        .Executes(() =>
        {
            DotNetPublish(p => p
                .SetProject(Solution)
                .SetConfiguration(Configuration.Release)
                .EnablePublishSingleFile()
                .DisableSelfContained()
                .SetRuntime("win-x64")
                .SetOutput(OutputDirectory));

            OutputDirectory.GlobFiles("*.pdb").ForEach(DeleteFile);
        });
    
    Target Run => _ => _
        .Executes(() =>
        {
            DotNetRun(r => r
                .SetProjectFile(Project)
                .SetConfiguration(Configuration.Release));
        });

    
    Target RunAll => _ => _
        .Executes(() =>
        {
            var executables = OutputDirectory.GlobFiles("*.exe");
            
            Parallel.ForEach(executables, exe =>
            {
                var output = new List<string>();
                var stopwatch = Stopwatch.StartNew();
                var p = ProcessTasks.StartProcess(exe, customLogger: (type, s) =>
                {
                    output.Add(s);
                });
                p.WaitForExit();
                stopwatch.Stop();
                Logger.Info($"{output.FirstOrDefault()} completed in {stopwatch.ElapsedMilliseconds}ms");
            });

        });

    Target RunTest => _ => _
        .DependsOn(Publish)
        .Executes(() =>
        {
            
        });
}
