using Juce.Dialogue.Configuration.Compilation;
using Juce.Dialogue.Configuration.Entries;
using Juce.Dialogue.Factories;
using Juce.Dialogue.Tree;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace Juce.Dialogue.Configuration.Nodes
{
    [NodeWidth(300)]
    public class BranchingOptionsDialogueConfiguration : DialogueConfigurationNode
    {
        [SerializeField] private List<BranchingOptionEntryConfiguration> entries = new List<BranchingOptionEntryConfiguration>();

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

            for(int i = 0; i < entries.Count; ++i)
            {
                BranchingOptionEntryConfiguration entry = entries[i];
                NodePort nodePort = outputPorts[i];

                BranchDialogueContent content = new BranchDialogueContent(entry.Text);
                IDialogueContent dialogueContent = new DialogueContent(content);

                IDialogueNode dialogueNode = CompilationUtils.LinkFlow(nodeValuesRepository, nodePort);

                branchDialogueNode.Add(dialogueContent, dialogueNode);
            }
        }
    }
}
