
using System.ComponentModel;
using Spectre.Console.Cli;

namespace appsum.efext.Commands.Migrations;

internal sealed class RemoveCommand : BaseDotnetCommand<RemoveCommand.Settings>
{
    protected override string GetDotnetCommand(ParamContext<Settings> ctx) => $"ef migrations remove {ctx.RestParams}";

    internal sealed class Settings : MigrationsSettings
    {
        [CommandOption("-f|--force")]
        [Description("([lime]Passthrough[/]) Revert the migration if it has been applied to the database.")]
        public bool IsForced { get; init; }

        public override List<string> GetOptions()
        {
            List<string> options = base.GetOptions();
            if (IsForced)
            {
                options.Add("--force");
            }
            return options;
        }
    }
}
