using System.Diagnostics;
using Spectre.Console;

namespace appsum.efext;

public static class DotnetCliProxy
{
    public static async Task<CommandResult> RunCommand(string commandToRun)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments =  commandToRun,
            RedirectStandardError = true,
            RedirectStandardOutput = true
        }; 
        Process p = Process.Start(processStartInfo)!;
        string output = await p.StandardOutput.ReadToEndAsync();
        AnsiConsole.WriteLine(output);
        string errors = await p.StandardError.ReadToEndAsync();
        AnsiConsole.MarkupInterpolated($"[darkred]{errors}[/]");
        await p.WaitForExitAsync();
        return new CommandResult
        {
            ExitCode = p.ExitCode,
            Output = output,
            Errors = errors
        };
    }
}

public record struct CommandResult
{
    public string Output { get; init; }
    public string Errors { get; init; }
    public int ExitCode { get; init; }
}
