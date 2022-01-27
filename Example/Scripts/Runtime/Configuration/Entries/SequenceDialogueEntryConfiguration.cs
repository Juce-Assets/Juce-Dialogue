using UnityEngine;

namespace Juce.Dialogue.Configuration.Entries
{
    [System.Serializable]
    public class SequenceDialogueEntryConfiguration 
    {
        [SerializeField, TextArea] private string text = default;

        public string Text => text;
    }
}