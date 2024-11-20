using GlobExpressions;
using static Bullseye.Targets;
using static SimpleExec.Command;

const string FORMAT = "format";
const string RESTORE_TOOLS = "restore-tools";

const string RESTORE = "restore";
const string BUILD = "build";
const string PACK = "pack";
const string CLEAN_LOCKS = "clean-locks";

const string SOLUTION = "src/Speckle.WebIfc.Importer.sln";
const string BASE_PATH = ".";

Target(
  CLEAN_LOCKS,
  () =>
  {
    foreach (var f in Glob.Files(".", "**/*.lock.json"))
    {
      Console.WriteLine("Found and will delete: " + f);
      File.Delete(f);
    }
    Console.WriteLine("Running restore now.");
    Run("dotnet", $"restore .\\{SOLUTION}");
  }
);

Target(RESTORE_TOOLS, () => RunAsync("dotnet", "tool restore", BASE_PATH));

Target(
  FORMAT,
  DependsOn(RESTORE_TOOLS),
  () => RunAsync("dotnet", "csharpier --check .", BASE_PATH)
);

Target(
  RESTORE,
  () => RunAsync("dotnet", $"restore {SOLUTION} --locked-mode", BASE_PATH)
);

Target(
  BUILD,
  DependsOn(RESTORE),
  async () =>
  {
    await RunAsync("dotnet", $"build {SOLUTION} -c Release --no-restore", BASE_PATH)
      .ConfigureAwait(false);
  }
);

Target(
  "publish",
  DependsOn(BUILD),
  async () =>
  {
    await RunAsync("dotnet", $"publish {SOLUTION} -c Release --no-restore --no-build -o publish/", BASE_PATH)
      .ConfigureAwait(false);
  }
);

static Task RunPack() =>
  RunAsync("dotnet", $"pack {SOLUTION} -c Release -o output --no-build", BASE_PATH);

Target(PACK, DependsOn(BUILD), RunPack);

Target("default", DependsOn(BUILD), () => Console.WriteLine("Done!"));
await RunTargetsAndExitAsync(args).ConfigureAwait(true);
