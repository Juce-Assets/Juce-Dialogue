using Juce.Dialogue.Configuration.Graph;
using Juce.Dialogue.Execution;
using Juce.Dialogue.Factories;
using Juce.Dialogue.Tree;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private ExampleDialogueConfigurationGraph graph = default;

    void Start()
    {
        Execute();
    }

    private async void Execute()
    {
        IDialogueTree dialogueTree = DialogueFactory.Instance.Create(graph);

        await new ExampleDialogueTreeExecutor(dialogueTree).Execute(CancellationToken.None);
    }
}
