using System.Collections.Generic;

namespace Juce.Dialogue.Tree
{
    public class SequenceDialogueNode : IDialogueNode
    {
        private readonly List<IDialogueContent> items;

        public IReadOnlyList<IDialogueContent> Items => items;

        public SequenceDialogueNode(
            List<IDialogueContent> nodes
            )
        {
            this.items = nodes;
        }

        public SequenceDialogueNode()
        {
            this.items = new List<IDialogueContent>();
        }

        public void Add(IDialogueContent node)
        {
            items.Add(node);
        }
    }
}
