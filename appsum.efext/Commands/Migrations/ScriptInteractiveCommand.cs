namespace appsum.efext.Commands.Migrations;

internal sealed class ScriptInteractiveCommand : BaseDotnetCommand<ScriptInteractiveCommand.Settings>
{
    protected override string GetDotnetCommand(ParamContext<Settings> ctx) => $"ef migrations script {ctx.RestParams}";

    protected override IEnumerable<string> EnrichCommandParams(IEnumerable<string> commandParams, ContextConfig contextConfig) => 
        commandParams.Union(contextConfig.Scripts.GetOptions());
    internal sealed class Settings : MigrationsSettings;
}
