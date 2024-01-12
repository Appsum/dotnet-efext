using appsum.efext.Contexts;
using Microsoft.VisualBasic;
using Spectre.Console;
using Spectre.Console.Cli;

namespace appsum.efext.Commands;

internal abstract class BaseDotnetCommand<TSettings> : AsyncCommand<TSettings> where TSettings : BaseCommandSettings
{
    protected bool IsDebug { get; set; }

    protected void LogDebug(string message)
    {
        if (IsDebug)
        {
            AnsiConsole.MarkupLineInterpolated($"[{Color.Grey}]{message}[/]");
        }
    }
    protected abstract string GetDotnetCommand(ParamContext<TSettings> ctx);

    protected virtual IEnumerable<string> EnrichCommandParams(IEnumerable<string> commandParams, ContextConfig contextConfig) => commandParams;
    public override async Task<int> ExecuteAsync(CommandContext context, TSettings settings)
    {
        IsDebug = settings.IsDebug;
        Config config = await ConfigProvider.ReadConfigAsync();

        List<ContextConfig> contextsToProcess = GetContexts(config, settings); 
        
        if (contextsToProcess.Count == 0)
        {
            AnsiConsole.MarkupLine("No contexts found. Either use an efconfig.json or provide the --contexts parameter.");
            return 0;
        }

        foreach (ContextConfig contextConfig in contextsToProcess)
        {
            AnsiConsole.MarkupLine($"Processing context: {contextConfig.Name}");
            LogDebug($"Context settings: {contextConfig}");
            List<string> configOptions = contextConfig.GetOptions();
            IEnumerable<string> commandParams = configOptions.Union(settings.GetOptions());
            commandParams = EnrichCommandParams(commandParams, contextConfig);
            await RunDotnetCommand(new ParamContext<TSettings>
            {
                Settings = settings,
                RestParams = string.Join(' ', commandParams),
                ContextConfig = contextConfig
            });
        }
        return 0;
    }

    protected virtual async Task RunDotnetCommand(ParamContext<TSettings> ctx)
    {
        string commandToRun = GetDotnetCommand(ctx);
        LogDebug($"About to run command:\n\tdotnet {commandToRun}");
        await DotnetCliProxy.RunCommand(commandToRun);
    }

    private List<ContextConfig> GetContexts(Config config, TSettings settings) => 
        settings.Contexts.Count == 0 
            ? ContextRequester.RequestWithMultiSelect(config, settings.IsDebug) 
            : config.GetContextsByName(settings.Contexts);
}

internal record ParamContext<TSettings> where TSettings : BaseCommandSettings
{
    public string RestParams { get; init; } = default!;
    public TSettings Settings { get; init; } = default!;
    public ContextConfig ContextConfig { get; init; } = default!;
}

internal static class ParamContextExtensions
{
    internal static string RestParamsWithNoBuildParam<TSettings>(this ParamContext<TSettings> ctx) where TSettings : BaseCommandSettings
    {
        if (ctx.RestParams.Contains("-b") || ctx.RestParams.Contains("--no-build"))
            return ctx.RestParams;

        return $"{ctx.RestParams} --no-build";
    }
}