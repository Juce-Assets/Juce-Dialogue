using Juce.Dialogue.Tree;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Dialogue.Execution
{
    public abstract class DialogueTreeExecutor : IDialogueTreeExecutor
    {
        private readonly IDialogueTree dialogueTree;

        public DialogueTreeExecutor(IDialogueTree dialogueTree)
        {
            this.dialogueTree = dialogueTree;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            OnStart();

            await ExecuteNext(dialogueTree.RootNode, cancellationToken);

            OnFinish();
        }

        private async Task ExecuteNext(
            IDialogueNode dialogueNodes,
            CancellationToken cancellationToken
            )
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            switch (dialogueNodes)
            {
                case CompositeDialogueNode compositeDialogueNode:
                    {
                        foreach (IDialogueNode node in compositeDialogueNode.Entries)
                        {
                            if (cancellationToken.IsCancellationRequested)
                            {
                                return;
                            }

                            await ExecuteNext(node, cancellationToken);
                        }
                    }
                    break;

                case SequenceDialogueNode contentSequenceDialogueNode:
                    {
                        await OnSequence(
                            contentSequenceDialogueNode,
                            cancellationToken
                            );
                    }
                    break;

                case BranchDialogueNode branchDialogueNode:
                    {
                        IDialogueNode selectedNode = await OnBranch(
                            branchDialogueNode,
                            cancellationToken
                            );

                        await ExecuteNext(selectedNode, cancellationToken);
                    }
                    break;
            }
        }

        protected abstract void OnStart();

        protected abstract Task OnSequence(
            SequenceDialogueNode sequenceDialogueNode,
            CancellationToken cancellationToken
            );

        protected abstract Task<IDialogueNode> OnBranch(
            BranchDialogueNode branchDialogueNode,
            CancellationToken cancellationToken
            );

        protected abstract void OnFinish();
    }
}
