using McMaster.Extensions.CommandLineUtils;
using CustomTranslatorCLI.Commands;

namespace CustomTranslatorCLI
{
    [Command(Name = "translator", Description = "Command-line interface for Azure Custom Translator service.")]
    [Subcommand(typeof(ConfigCommand))]
    [Subcommand(typeof(WorkspaceCommand))]
    //[Subcommand("model", typeof(ModelCommand))]
    //[Subcommand("test", typeof(TestCommand))]
    //[Subcommand("endpoint", typeof(EndpointCommand))]
    //[Subcommand("compile", typeof(CompileCommand))]
    //[Subcommand("transcript", typeof(TranscriptCommand))]
    public class MainApp
    {
        int OnExecute(CommandLineApplication app, IConsole console)
        {
            app.ShowHelp();
            return 0;
        }
    }
}
