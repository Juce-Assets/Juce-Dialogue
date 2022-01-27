using System.Collections.Generic;

namespace Juce.Dialogue.Tree
{
    public class CompositeDialogueNode : IDialogueNode
    {
        public IReadOnlyList<IDialogueNode> Entries { get; }

        public CompositeDialogueNode(
            IReadOnlyList<IDialogueNode> entries
            )
        {
            Entries = entries;
        }
    }
}
