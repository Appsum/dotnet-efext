using appsum.efext;
using Spectre.Console.Cli;

CommandApp app = CliAppFactory.CreateCliApp();

await app.RunAsync(args);
