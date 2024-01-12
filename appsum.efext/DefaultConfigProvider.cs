using System.Text.Json;
using Spectre.Console;

namespace appsum.efext;

public static class ConfigProvider
{
    private static IEnumerable<string> EfConfigPaths => ["efconfig.json"];

    private static bool _isDebug;
    private static void LogDebug(string message)
    {
        if (_isDebug)
        {
            AnsiConsole.MarkupLineInterpolated($"[gray]{message}[/]");
        }
    }
    public static async Task<Config> ReadConfigAsync(bool isDebug = false)
    {
        _isDebug = isDebug;
        string existingFilePath = GetExistingFilePath();
        if (existingFilePath == string.Empty)
            return new Config();

        string configJson = await File.ReadAllTextAsync(existingFilePath);
        LogDebug($"Config file data: {configJson}");
        try
        {
            return JsonSerializer.Deserialize<Config>(configJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            }) ?? new Config();
        }
        catch (Exception)
        {
            AnsiConsole.MarkupLineInterpolated($"Config file at {existingFilePath} is not in a valid format");
            throw;
        }
    }

    private static string GetExistingFilePath()
    {
        foreach (string configPath in EfConfigPaths)
        {
            if (File.Exists(configPath))
            {
                LogDebug($"Config file found on path {configPath}");
                return configPath;
            }
            LogDebug($"Config file not found on path {configPath}");
        }
        // no config found
        return string.Empty;
    }
}