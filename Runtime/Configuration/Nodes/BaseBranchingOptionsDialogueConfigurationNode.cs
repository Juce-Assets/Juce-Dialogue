using Juce.Dialogue.Configuration.Compilation;
using Juce.Dialogue.Tree;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace Juce.Dialogue.Configuration.Nodes
{
    public abstract class BaseBranchingOptionsDialogueConfigurationNode<TContent> : DialogueConfigurationNode
    {
        [SerializeField] private List<TContent> entries = new List<TContent>();

        public sealed override IDialogueNode Create()
        {
            return new BranchDialogueNode();
        }

        public sealed override void Link(IDialogueNode node, ICompilationValuesRepository nodeValuesRepository)
        {
            BranchDialogueNode branchDialogueNode = node as BranchDialogueNode;

            if (branchDialogueNode == null)
            {
                return;
            }

            List<NodePort> outputPorts = DynamicOutputs.ToList();

            for (int i = 0; i < entries.Count; ++i)
            {
                TContent entry = entries[i];
                NodePort nodePort = outputPorts[i];

                object contentObject = ProcessContent(entry);

                IDialogueContent dialogueContent = new DialogueContent(contentObject);

                IDialogueNode dialogueNode = CompilationUtils.LinkFlow(nodeValuesRepository, nodePort);

                branchDialogueNode.Add(dialogueContent, dialogueNode);
            }
        }

        protected abstract object ProcessContent(TContent content);
    }
}
