using System.ComponentModel;
using Spectre.Console.Cli;

namespace appsum.efext.Commands;

internal abstract class BaseCommandSettings : CommandSettings
{
    [CommandOption("-d|--debug")]
    [Description("Show debug output.")]
    public bool IsDebug { get; init; } = false;

    [CommandOption("--contexts")]
    [Description("The contexts to do the actions on, separated by a comma (,). If this is provided, the multiselector will not be shown.")]
    public string? ContextsString { get; init; }
    public List<string> Contexts => ContextsString is not null ? ContextsString.Split(',').ToList() : [];
    
    [CommandOption("-b|--no-build")]
    [Description("([lime]Passthrough[/]) Don't build the project. Intended to be used when the build is up-to-date.")]
    public bool ShouldNotBuild { get; init; }

    public virtual List<string> GetOptions()
    {
        var options = new List<string>();
        
        if (IsDebug)
        {
            // Also output the verbose output from the dotnet ef CLI
            options.Add("--verbose");
        }
        if (ShouldNotBuild)
        {
            options.Add("--no-build");
        }
        
        return options;
    }
}
