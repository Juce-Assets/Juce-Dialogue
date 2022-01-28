using Juce.Dialogue.Content;
using Juce.Dialogue.Tree;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Dialogue.Execution
{
    public class ExampleDialogueTreeExecutor : DialogueTreeExecutor
    {
        public ExampleDialogueTreeExecutor(IDialogueTree dialogueTree) : base(dialogueTree)
        {

        }

        protected override void OnStart()
        {
            UnityEngine.Debug.Log("Starting Dialogue!");
        }

        protected override Task OnSequence(SequenceDialogueNode sequenceDialogueNode, CancellationToken cancellationToken)
        {
            foreach(IDialogueContent content in sequenceDialogueNode.Items)
            {
                SequenceDialogueContent sequenceContent = content.Get<SequenceDialogueContent>();

                UnityEngine.Debug.Log($"- {sequenceContent.Text}");
            }

            return Task.CompletedTask;
        }

        protected override Task<IDialogueNode> OnBranch(BranchDialogueNode branchDialogueNode, CancellationToken cancellationToken)
        {
            if(branchDialogueNode.Branches.Count == 0)
            {
                return Task.FromResult<IDialogueNode>(new CompositeDialogueNode()); 
            }


            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Options: ");

            foreach (KeyValuePair<IDialogueContent, IDialogueNode> content in branchDialogueNode.Branches)
            {
                BranchDialogueContent branchContent = content.Key.Get<BranchDialogueContent>();

                stringBuilder.Append($"{branchContent.Text} | ");
            }

            UnityEngine.Debug.Log(stringBuilder);

            int randomOptionIndex = UnityEngine.Random.Range(0, branchDialogueNode.Branches.Count);

            KeyValuePair<IDialogueContent, IDialogueNode> option = branchDialogueNode.Branches[randomOptionIndex];
            BranchDialogueContent optionContent = option.Key.Get<BranchDialogueContent>();

            UnityEngine.Debug.Log($"Selected: {optionContent.Text}");

            return Task.FromResult(option.Value);
        }

        protected override void OnFinish()
        {
            UnityEngine.Debug.Log("Dialogue Finished!");
        }
    }
}
