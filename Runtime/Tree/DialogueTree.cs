namespace Juce.Dialogue.Tree
{
    public class DialogueTree : IDialogueTree
    {
        public IDialogueNode RootNode { get; }

        public DialogueTree(IDialogueNode rootNode)
        {
            RootNode = rootNode;
        }
    }
}
