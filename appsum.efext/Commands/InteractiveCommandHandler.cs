using Spectre.Console;

namespace appsum.efext.Commands;

internal sealed class InteractiveCommandHandler
{
    public async Task<List<Migration>> GetMigrations(string restParams)
    {
        var migrationsListCommand = $"dotnet ef migrations list {restParams}";
        LogDebug($"Running {migrationsListCommand}");
        CommandResult result = await DotnetCliProxy.RunCommand(migrationsListCommand);

        // extract dates and names
        List<Migration> migrations = MigrationsListParser.Parse(result.Output).OrderByDescending(m => m.CreationDate).ToList();
        if (migrations.Count == 0)
        {
            throw new InvalidOperationException("No migrations found.");
        }
        return migrations;
    }

    public Migration AskSingleSelection(List<Migration> migrations, string askText)
    {
        Migration chosenMigration = AnsiConsole.Prompt(
            new SelectionPrompt<Migration>()
                .Title(askText)
                .AddChoices(migrations)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .UseConverter(MigrationPromptConverter)
        );
        return chosenMigration;
    }

    private void LogDebug(string message)
    {
        if (IsDebug)
        {
            AnsiConsole.MarkupInterpolated($"[{Color.Grey}]{message}[/]");
        }
    }

    private static string MigrationPromptConverter(Migration migration)
    {
        var text = migration.ToString();
        if (migration.Status == Migration.MigrationStatus.Pending)
        {
            text = $"[{Color.DarkOrange}]{text}[/]";
        }
        return text;
    }

    public bool IsDebug { get; init; } = false;
}
