using Juce.Dialogue.Configuration.Entries;
using Juce.Dialogue.Content;

namespace Juce.Dialogue.Configuration.Nodes
{
    [NodeWidth(300)]
    [NodeTint(160, 60, 60)]
    public sealed class ExampleSequenceDialogueConfigurationNode : BaseSequenceDialogueConfigurationNode<SequenceDialogueEntryConfiguration> 
    {
        protected sealed override object ProcessContent(SequenceDialogueEntryConfiguration content)
        {
            return new SequenceDialogueContent(content.Text);
        }
    }
}
