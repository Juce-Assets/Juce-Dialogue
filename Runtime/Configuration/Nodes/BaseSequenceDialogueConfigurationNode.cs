using Juce.Dialogue.Configuration.Compilation;
using Juce.Dialogue.Tree;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Dialogue.Configuration.Nodes
{
    public abstract class BaseSequenceDialogueConfigurationNode<TContent> : DialogueConfigurationNode
    {
        [SerializeField] private List<TContent> entries = new List<TContent>();

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

            foreach (TContent entry in entries)
            {
                object contentObject = ProcessContent(entry);

                IDialogueContent dialogueContent = new DialogueContent(contentObject);

                sequenceDialogueNode.Add(dialogueContent);
            }
        }

        protected abstract object ProcessContent(TContent content);
    }
}
