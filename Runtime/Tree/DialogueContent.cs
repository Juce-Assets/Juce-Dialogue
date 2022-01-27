namespace Juce.Dialogue.Tree
{
    public class DialogueContent : IDialogueContent
    {
        public object Content { get; }

        public DialogueContent(object content)
        {
            Content = content;
        }

        public T Get<T>()
        {
            return (T)Content;
        }
    }
}
