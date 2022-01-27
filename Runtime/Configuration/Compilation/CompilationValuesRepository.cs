using Juce.Dialogue.Configuration.Nodes;
using Juce.Dialogue.Tree;
using System.Collections.Generic;

namespace Juce.Dialogue.Configuration.Compilation
{
    public class CompilationValuesRepository : ICompilationValuesRepository
    {
        private readonly Dictionary<DialogueConfigurationNode, IDialogueNode> nodeValues
            = new Dictionary<DialogueConfigurationNode, IDialogueNode>();

        private readonly List<DialogueConfigurationNode> linkedNodes = new List<DialogueConfigurationNode>();

        public void AddConfigurationValue(DialogueConfigurationNode configuration, IDialogueNode node)
        {
            nodeValues.Add(configuration, node);
        }

        public bool TryGetConfigurationValue(DialogueConfigurationNode configuration, out IDialogueNode foundNode)
        {
            return nodeValues.TryGetValue(configuration, out foundNode);
        }

        public void AddLinked(DialogueConfigurationNode configuration)
        {
            linkedNodes.Add(configuration);
        }

        public bool IsLinked(DialogueConfigurationNode configuration)
        {
            return linkedNodes.Contains(configuration);
        }
    }
}
