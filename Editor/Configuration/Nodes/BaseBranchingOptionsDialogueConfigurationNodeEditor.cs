using Juce.Dialogue.Configuration.Flow;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace Juce.Dialogue.Configuration.Nodes
{
    public abstract class BaseBranchingOptionsDialogueConfigurationNodeEditor<TContent> : NodeEditor
    {
        public sealed override void OnBodyGUI()
        {
            serializedObject.Update();

            BaseBranchingOptionsDialogueConfigurationNode<TContent> node = target as BaseBranchingOptionsDialogueConfigurationNode<TContent>;

            GUILayout.BeginHorizontal();
            NodeEditorGUILayout.PortField(new GUIContent("Input"), target.GetInputPort("Input"), GUILayout.MinWidth(0));
            NodeEditorGUILayout.PortField(new GUIContent("Output"), target.GetOutputPort("Output"), GUILayout.MinWidth(0));
            GUILayout.EndHorizontal();

            NodeEditorGUILayout.DynamicPortList(
                "entries",
                typeof(DialogueFlow),
                serializedObject,
                NodePort.IO.Output,
                Node.ConnectionType.Override
                );

            serializedObject.ApplyModifiedProperties();
        }
    }
}