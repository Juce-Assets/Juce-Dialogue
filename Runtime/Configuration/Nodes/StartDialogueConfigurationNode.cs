using Juce.Dialogue.Configuration.Flow;
using XNode;

namespace Juce.Dialogue.Configuration.Nodes
{
	public class StartDialogueConfigurationNode : Node
	{
		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)] public DialogueFlow Output;

		public override sealed object GetValue(NodePort port)
		{
			return default;
		}
	}
}
