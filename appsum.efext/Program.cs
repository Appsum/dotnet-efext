using appsum.efext;
using Spectre.Console;
using Spectre.Console.Cli;

AnsiConsole.MarkupLine("EF Extensions tooling by Appsum Solutions");

CommandApp app = CliAppFactory.CreateCliApp();

await app.RunAsync(args);

