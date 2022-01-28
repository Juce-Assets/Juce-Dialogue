using System.Threading;
using System.Threading.Tasks;

namespace Juce.Dialogue.Execution
{
    public interface IDialogueTreeExecutor
    {
        Task Execute(CancellationToken cancellationToken);
    }
}
