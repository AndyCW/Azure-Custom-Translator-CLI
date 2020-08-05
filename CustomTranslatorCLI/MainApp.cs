using McMaster.Extensions.CommandLineUtils;
using CustomTranslatorCLI.Commands;

namespace CustomTranslatorCLI
{
    [Command(Name = "translator", Description = "Command-line interface for Azure Custom Translator service.")]
    [Subcommand(typeof(ConfigCommand))]
    [Subcommand(typeof(WorkspaceCommand))]
    [Subcommand(typeof(ProjectCommand))]
    public class MainApp
    {
        int OnExecute(CommandLineApplication app, IConsole console)
        {
            app.ShowHelp();
            return 0;
        }
    }
}
