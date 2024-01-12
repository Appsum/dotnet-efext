using System.ComponentModel;
using Spectre.Console.Cli;

namespace appsum.efext.Commands.Database;

internal sealed class UpdateInteractiveCommand : BaseDotnetCommand<UpdateInteractiveCommand.Settings>
{
    protected override string GetDotnetCommand(ParamContext<Settings> ctx) => $"ef database update {ctx.RestParams}";
    internal sealed class Settings : DatabaseSettings;
}
