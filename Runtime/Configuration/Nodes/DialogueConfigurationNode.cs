using Juce.Dialogue.Configuration.Compilation;
using Juce.Dialogue.Configuration.Flow;
using Juce.Dialogue.Tree;
using XNode;

namespace Juce.Dialogue.Configuration.Nodes
{
    public abstract class DialogueConfigurationNode : Node
    {
		[Input(backingValue = ShowBackingValue.Never)] public DialogueFlow Input;
		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)] public DialogueFlow Output;

		public override sealed object GetValue(NodePort port)
		{
			return default;
		}

		public abstract IDialogueNode Create();
		public abstract void Link(IDialogueNode node, ICompilationValuesRepository nodeValuesRepository);
	}
}
