using Juce.Dialogue.Configuration.Entries;
using Juce.Dialogue.Content;

namespace Juce.Dialogue.Configuration.Nodes
{
    [NodeWidth(400)]
    [NodeTint(60, 60, 160)]
    public sealed class ExampleBranchingOptionsDialogueConfigurationNode 
        : BaseBranchingOptionsDialogueConfigurationNode<BranchingOptionEntryConfiguration> 
    {
        protected sealed override object ProcessContent(BranchingOptionEntryConfiguration content)
        {
            return new BranchDialogueContent(content.Text);
        }
    }
}
