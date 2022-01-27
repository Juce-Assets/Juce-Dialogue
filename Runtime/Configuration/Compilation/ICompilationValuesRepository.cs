using Juce.Dialogue.Configuration.Nodes;
using Juce.Dialogue.Tree;

namespace Juce.Dialogue.Configuration.Compilation
{
    public interface ICompilationValuesRepository    
    {
        void AddConfigurationValue(DialogueConfigurationNode configuration, IDialogueNode node);
        bool TryGetConfigurationValue(DialogueConfigurationNode configuration, out IDialogueNode node);

        void AddLinked(DialogueConfigurationNode configuration);
        bool IsLinked(DialogueConfigurationNode configuration);
    }
}
