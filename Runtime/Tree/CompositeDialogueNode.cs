using System.Collections.Generic;

namespace Juce.Dialogue.Tree
{
    public class CompositeDialogueNode : IDialogueNode
    {
        private readonly List<IDialogueNode> entries;

        public IReadOnlyList<IDialogueNode> Entries => entries;

        public CompositeDialogueNode(
            List<IDialogueNode> entries
            )
        {
            this.entries = entries;
        }

        public CompositeDialogueNode()
        {
            this.entries = new List<IDialogueNode>();
        }

        public void Add(IDialogueNode node)
        {
            entries.Add(node);
        }
    }
}
