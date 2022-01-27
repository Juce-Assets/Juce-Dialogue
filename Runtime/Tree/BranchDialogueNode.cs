using System.Collections.Generic;

namespace Juce.Dialogue.Tree
{
    public class BranchDialogueNode : IDialogueNode
    {
        private readonly List<KeyValuePair<IDialogueContent, IDialogueNode>> branches;

        public IReadOnlyList<KeyValuePair<IDialogueContent, IDialogueNode>> Branches => branches;

        public BranchDialogueNode(
            List<KeyValuePair<IDialogueContent, IDialogueNode>> branches
            )
        {
            this.branches = branches;
        }

        public BranchDialogueNode()
        {
            this.branches = new List<KeyValuePair<IDialogueContent, IDialogueNode>>();
        }

        public void Add(IDialogueContent content, IDialogueNode node)
        {
            this.branches.Add(new KeyValuePair<IDialogueContent, IDialogueNode>(content, node));
        }
    }
}
