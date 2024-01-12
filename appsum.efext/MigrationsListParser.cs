using System.Globalization;
using System.Text.RegularExpressions;
using Spectre.Console;

namespace appsum.efext;

public static partial class MigrationsListParser
{
    public const string MIGRATION_DATE_FORMAT = "yyyyMMddHHmmss";

    [GeneratedRegex(@"(?<date>\d*)_(?<name>(\S*))\s?\((?<status>\w*)\)", RegexOptions.Multiline)]
    private static partial Regex MigrationsListRegex();

    public static IEnumerable<Migration> Parse(string migrationsListOutput)
    {
        foreach (Match m in MigrationsListRegex().Matches(migrationsListOutput))
        {
            yield return new Migration
            {
                Name = m.Groups["name"].Value,
                CreationDate = DateTime.ParseExact(m.Groups["date"].Value, MIGRATION_DATE_FORMAT, CultureInfo.InvariantCulture),
                Status = m.Groups["status"].Success ? m.Groups["status"].Value == "Pending" ? Migration.MigrationStatus.Pending : Migration.MigrationStatus.Active : Migration.MigrationStatus.Active
            };
        }
    }
}

public readonly record struct Migration()
{
    public required DateTime CreationDate { get; init; } = default;
    public required string Name { get; init; } = default!;
    public MigrationStatus Status { get; init; } = MigrationStatus.Active;
    public string FileName => $"{CreationDate.ToString(MigrationsListParser.MIGRATION_DATE_FORMAT)}_{Name}.cs";

    public enum MigrationStatus
    {
        Active,
        Pending
    }

    public override string ToString() => $"{CreationDate.ToString("g", CultureInfo.CurrentCulture)} - {Name}";
}
