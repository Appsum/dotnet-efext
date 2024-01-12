using System.ComponentModel;
using Spectre.Console.Cli;

namespace appsum.efext.Commands.Migrations;

internal sealed class BundleCommand : BaseDotnetCommand<BundleCommand.Settings>
{
    protected override string GetDotnetCommand(ParamContext<Settings> ctx) => $"ef migrations bundle {ctx.RestParams}";

    protected override IEnumerable<string> EnrichCommandParams(IEnumerable<string> commandParams, ContextConfig contextConfig) => 
        commandParams.Union(contextConfig.Bundles.GetOptions());

    internal sealed class Settings : MigrationsSettings;
}
