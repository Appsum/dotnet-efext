using System.ComponentModel;

namespace appsum.efext;

public sealed record Config
{
    public List<ContextConfig> Contexts { get; init; } = [];

    public List<ContextConfig> GetContextsByName(List<string> contextNames) => Contexts.Where(x => contextNames.Any(c => c.Equals(x.Name, StringComparison.OrdinalIgnoreCase))).ToList();
}

public sealed record ContextConfig
{
    public required string Name { get; init; }
    
    public string? DbContext { get; init; }
    public string StartupProject { get; init; } = string.Empty;
    public string Project { get; init; } = string.Empty;
    public MigrationsConfigOptions Migrations { get; init; } = new();
    public BundleConfigOptions Bundles { get; init; } = new();
    public SqlConfigOptions Scripts { get; init; } = new();

    public List<string> GetOptions()
    {
        List<string> options = [];
        
        if (!string.IsNullOrWhiteSpace(StartupProject))
        {
            options.Add($"--startup-project {StartupProject}");
        }        
    
        if (!string.IsNullOrWhiteSpace(Project))
        {
            options.Add($"--project {Project}");
        }
        if (!string.IsNullOrWhiteSpace(Name))
        {
            options.Add($"--context {DbContext ?? Name}");
        }
        
        return options;
    }
}

public sealed record BundleConfigOptions
{
    public string OutputFile { get; init; } = string.Empty;
    public List<string> GetOptions()
    {
        List<string> options = [];
        if (!string.IsNullOrWhiteSpace(OutputFile))
        {
            options.Add($"--output {OutputFile}");
        }     
        return options;
    }
}
public sealed record MigrationsConfigOptions
{
    [Description("The directory to put files in. Paths are relative to the project directory. Defaults to \"Migrations\".")]
    public string OutputDir { get; init; } = "Migrations";
    public List<string> GetOptions()
    {
        List<string> options = [];
        if (!string.IsNullOrWhiteSpace(OutputDir))
        {
            options.Add($"--output-dir {OutputDir}");
        }     
        return options;
    }
}

public sealed record SqlConfigOptions
{
    public string OutputFile { get; init; } = string.Empty;
    public bool IsIdempotent { get; init; } = false;
    public bool NoTransactions { get; init; } = false;

    public List<string> GetOptions()
    {
        List<string> options = [];
        
        if (IsIdempotent)
        {
            options.Add("--idempotent");
        }        
        if (!string.IsNullOrWhiteSpace(OutputFile))
        {
            options.Add("--output");
        }
        if (NoTransactions)
        {
            options.Add("--no-transactions");
        }
        
        return options;
    }
}