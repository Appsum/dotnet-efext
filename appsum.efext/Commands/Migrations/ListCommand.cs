namespace appsum.efext.Commands.Migrations;

internal sealed class ListCommand : BaseDotnetCommand<ListCommand.Settings>
{
    protected override string GetDotnetCommand(ParamContext<Settings> ctx) => $"ef migrations list {ctx.RestParams}";

    internal sealed class Settings : MigrationsSettings
    {
        /*
                --connection <CONNECTION>
                --no-connect                           Don't connect to the database.
                --json                                 Show JSON output. Use with --prefix-output to parse programatically.
                -c|--context <DBCONTEXT>               The DbContext to use.
                -p|--project <PROJECT>                 The project to use. Defaults to the current working directory.
                -s|--startup-project <PROJECT>         The startup project to use. Defaults to the current working directory.
                --framework <FRAMEWORK>                The target framework. Defaults to the first one in the project.
                --configuration <CONFIGURATION>        The configuration to use.
                --runtime <RUNTIME_IDENTIFIER>         The runtime to use.
                --msbuildprojectextensionspath <PATH>  The MSBuild project extensions path. Defaults to "obj".
                --no-build                             Don't build the project. Intended to be used when the build is up-to-date.
                -h|--help                              Show help information
                -v|--verbose                           Show verbose output.
                --no-color                             Don't colorize output.
                --prefix-output                        Prefix output with level.
                */
    }
}