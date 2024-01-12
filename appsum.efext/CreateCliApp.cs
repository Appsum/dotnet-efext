using appsum.efext.Commands.Database;
using appsum.efext.Commands.Migrations;
using Spectre.Console.Cli;

namespace appsum.efext;

public static class CliAppFactory
{
    public static CommandApp CreateCliApp()
    {
        var commandApp = new CommandApp();
    
        commandApp.Configure(c =>
        {
            c.Settings.ApplicationName = "efext";
            c.AddBranch("migrations", m =>
            {
                m.SetDescription($"Aliases: migration, m\t");
                m.AddCommand<ListCommand>("lists")
                 .WithAlias("ls")
                 .WithDescription("Alias: ls\tList the migrations using dotnet ef migrations list.");
    
                m.AddCommand<AddCommand>("add")
                 .WithAlias("a")
                 .WithDescription("Alias: a\tAdd a new migration by name.");
    
                m.AddCommand<RemoveCommand>("remove")
                 .WithAlias("rm")
                 .WithDescription("Alias: rm\t");
    
                m.AddCommand<RemoveInteractiveCommand>("remove-interactive")
                 .WithAlias("rmi")
                 .WithDescription("Alias: rmi\t");
    
                m.AddCommand<ScriptCommand>("script")
                 .WithAlias("s")
                 .WithDescription("Alias: s\t");
    
                m.AddCommand<ScriptInteractiveCommand>("script-interactive")
                 .WithAlias("si")
                 .WithDescription("Alias: si\t");
    
                m.AddCommand<BundleCommand>("bundle")
                 .WithAlias("b")
                 .WithDescription("Alias: b\t");
            }).WithAlias("m").WithAlias("migration");
            c.AddBranch<DatabaseSettings>("database", d =>
            {
                d.SetDescription("Alias: db\t");
                d.AddCommand<UpdateCommand>("update")
                 .WithAlias("u")
                 .WithDescription("Alias: u\t");
                d.AddCommand<UpdateInteractiveCommand>("update-interactive")
                 .WithAlias("ui")
                 .WithDescription("Alias: ui\t");
                d.AddCommand<DropCommand>("drop")
                 .WithAlias("d")
                 .WithDescription("Alias: d");
            }).WithAlias("db");
        });
        return commandApp;
    }
}
