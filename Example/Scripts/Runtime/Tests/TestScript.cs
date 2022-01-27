using Juce.Dialogue.Configuration.Graph;
using Juce.Dialogue.Factories;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private ExampleDialogueConfigurationGraph graph = default;

    void Start()
    {
        DialogueFactory.Instance.Create(graph);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
