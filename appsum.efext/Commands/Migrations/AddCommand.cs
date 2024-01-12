using System.ComponentModel;
using Spectre.Console.Cli;

namespace appsum.efext.Commands.Migrations;

internal sealed class AddCommand : BaseDotnetCommand<AddCommand.Settings>
{
    protected override string GetDotnetCommand(ParamContext<Settings> ctx) => $"ef migrations add {ctx.Settings.Name} {ctx.RestParams}";

    protected override IEnumerable<string> EnrichCommandParams(IEnumerable<string> commandParams, ContextConfig contextConfig) => commandParams.Union(contextConfig.Migrations.GetOptions());

    internal sealed class Settings : MigrationsSettings
    {
        [CommandArgument(1, "<name>")]
        [Description("The name of the migration.")]
        public required string Name { get; init; }
    }
}
