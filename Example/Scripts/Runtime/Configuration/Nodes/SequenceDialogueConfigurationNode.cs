using Juce.Dialogue.Configuration.Compilation;
using Juce.Dialogue.Configuration.Entries;
using Juce.Dialogue.Content;
using Juce.Dialogue.Tree;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Dialogue.Configuration.Nodes
{
    [NodeWidth(300)]
    public class SequenceDialogueConfigurationNode : DialogueConfigurationNode
    {
        [SerializeField] private List<SequenceDialogueEntryConfiguration> entries = new List<SequenceDialogueEntryConfiguration>();

        public sealed override IDialogueNode Create()
        {
            return new SequenceDialogueNode();
        }

        public sealed override void Link(IDialogueNode node, ICompilationValuesRepository nodeValuesRepository)
        {
            SequenceDialogueNode sequenceDialogueNode = node as SequenceDialogueNode;

            if (sequenceDialogueNode == null)
            {
                return;
            }

            foreach(SequenceDialogueEntryConfiguration entry in entries)
            {
                SequenceDialogueContent content = new SequenceDialogueContent(entry.Text);
                IDialogueContent dialogueContent = new DialogueContent(content);

                sequenceDialogueNode.Add(dialogueContent);
            }
        }
    }
}
