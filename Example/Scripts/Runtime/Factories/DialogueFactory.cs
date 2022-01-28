using Juce.Dialogue.Configuration.Compilation;
using Juce.Dialogue.Configuration.Graph;
using Juce.Dialogue.Tree;

namespace Juce.Dialogue.Factories
{
    public class DialogueFactory
    {
        public static readonly DialogueFactory Instance = new DialogueFactory();

        private DialogueFactory()
        {

        }

        public IDialogueTree Create(ExampleDialogueConfigurationGraph graph)
        {
            return CompilationUtils.CompileGraph(graph);
        }
    }
}
