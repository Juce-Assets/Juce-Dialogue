using Juce.Dialogue.Configuration.Compilation;
using Juce.Dialogue.Configuration.Graph;
using Juce.Dialogue.Tree;
using UnityEngine;

namespace Juce.Dialogue.Configuration.Nodes
{
    public abstract class BaseSubGraphDialogueConfigurationNode<TSubGraph> : DialogueConfigurationNode where TSubGraph : DialogueConfigurationGraph
    {
        [SerializeField] private TSubGraph subGraph = default;

        public sealed override IDialogueNode Create()
        {
            return new CompositeDialogueNode();
        }

        public sealed override void Link(IDialogueNode node, ICompilationValuesRepository nodeValuesRepository)
        {
            CompositeDialogueNode compositeDialogueNode = node as CompositeDialogueNode;

            if (compositeDialogueNode == null)
            {
                return;
            }

            if (subGraph == null)
            {
                return;
            }

            IDialogueTree tree = CompilationUtils.CompileGraph(subGraph);

            compositeDialogueNode.Add(tree.RootNode);
        }
    }
}
