using System.Text;
using Spectre.Console;

namespace appsum.efext.Commands.Migrations;

internal sealed class RemoveInteractiveCommand : BaseDotnetCommand<RemoveInteractiveCommand.Settings>
{
    protected override string GetDotnetCommand(ParamContext<Settings> ctx) => $"ef migrations remove {ctx.RestParams}";

    protected override async Task RunDotnetCommand(ParamContext<Settings> ctx)
    {
        var interactiveCommandHandler = new InteractiveCommandHandler
        {
            IsDebug = IsDebug
        };

        List<Migration> migrations = await interactiveCommandHandler.GetMigrations(ctx.RestParams);

        Migration chosenMigration = interactiveCommandHandler.AskSingleSelection(migrations, "Select a migration to remove, all migrations above it will also be removed (pending migrations are orange)");

        List<Migration> migrationsToRemove = migrations[..(migrations.IndexOf(chosenMigration) + 1)];
        var sb = new StringBuilder();
        sb.AppendLine("[red]About to remove migrations[/]");
        foreach (Migration m in migrationsToRemove)
            sb.AppendLine(m.ToString());
        sb.AppendLine("[red]Are you sure?[/]");
        AnsiConsole.Prompt(new ConfirmationPrompt(sb.ToString()).ShowChoices(true));
        
        // loop over all migrations in the list, newest to oldest, calling 'dotnet ef migrations remove --no-build <rest-params>' for each, which will remove the 'last migration'
        foreach (Migration migration in migrationsToRemove)
        {
            AnsiConsole.MarkupLine($"Removing migration {migration.ToString()}...");
            await DotnetCliProxy.RunCommand($"dotnet ef migrations remove {ctx.RestParams}");
            LogDebug("Migration removed");
        }
        AnsiConsole.MarkupLine("All selected migrations removed");
    }

   



    internal sealed class Settings : MigrationsSettings;
}
