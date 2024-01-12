using System.ComponentModel;
using Spectre.Console.Cli;

namespace appsum.efext.Commands.Database;

internal sealed class DropCommand : BaseDotnetCommand<DropCommand.Settings>
{
    protected override string GetDotnetCommand(ParamContext<Settings> ctx) => $"ef database drop {ctx.RestParams}";

    internal sealed class Settings : DatabaseSettings;
}
