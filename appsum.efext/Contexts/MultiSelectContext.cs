using Spectre.Console;

namespace appsum.efext.Contexts;

public static class ContextRequester
{
    public static List<ContextConfig> RequestWithMultiSelect(Config config, bool isDebug)
    {
        if (config.Contexts.Count < 2)
            return config.Contexts;

        List<string> selectedContexts = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Select contexts to handle?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle, " +
                    "[green]<enter>[/] to accept)[/]")
                .Required(true)
                .AddChoiceGroup("All", config.Contexts.Select(x => x.Name)));
        
        selectedContexts = selectedContexts.Where(c => c != "All").ToList();
        AnsiConsole.MarkupLineInterpolated($"Selected context(s): {string.Join(", ", selectedContexts)}");
        
        return config.GetContextsByName(selectedContexts);
    }
}