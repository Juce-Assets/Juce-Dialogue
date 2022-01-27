using Juce.Dialogue.Configuration.Flow;
using Juce.Dialogue.Configuration.Graph;
using Juce.Dialogue.Configuration.Nodes;
using Juce.Dialogue.Tree;
using System;
using System.Collections.Generic;
using XNode;

namespace Juce.Dialogue.Configuration.Compilation
{
	public static class CompilationUtils
	{
        public static List<TNode> GetNodes <TNode>(
            DialogueConfigurationGraph graph,
            bool inherited = true
            ) where TNode : DialogueConfigurationNode
        {
            List<TNode> ret = new List<TNode>();

            Type type = typeof(TNode);

            foreach (Node node in graph.nodes)
            {
                if (!inherited)
                {
                    if (node.GetType() != type)
                    {
                        continue;
                    }
                }
                else
                {
                    if (!type.IsAssignableFrom(node.GetType()))
                    {
                        continue;
                    }
                }

                ret.Add((TNode)node);
            }

            return ret;
        }

        public static bool TryGetNode<TNode>(
            DialogueConfigurationGraph graph,
            out TNode node
            ) where TNode : DialogueConfigurationNode
        {
            List<TNode> nodes = GetNodes<TNode>(graph);

            if (nodes.Count == 0)
            {
                node = null;
                return false;
            }

            node = nodes[0];
            return true;
        }

        public static bool TryGetStartNode(
           DialogueConfigurationGraph graph,
           out StartDialogueConfigurationNode startNode
           ) 
        {
            Type type = typeof(StartDialogueConfigurationNode);

            foreach (Node node in graph.nodes)
            {
                if (node.GetType() != type)
                {
                    continue;
                }

                startNode = (StartDialogueConfigurationNode)node;
                return true;
            }

            startNode = default;
            return false;
        }

        public static bool TryGetNextDialogueFlowNode(
            NodePort nodePort, 
            out DialogueConfigurationNode foundNode
            )
        {
			bool isNotDialogueFlow = nodePort.ValueType != typeof(DialogueFlow);

			if(isNotDialogueFlow)
            {
				foundNode = default;
				return false;
            }

			bool hasConnection = nodePort.Connection != null;

			if(!hasConnection)
            {
				foundNode = default;
				return false;
			}

			bool connectedNodeisNotDialogueNode = !typeof(DialogueConfigurationNode).IsAssignableFrom(nodePort.Connection.node.GetType());

			if(connectedNodeisNotDialogueNode)
            {
				foundNode = default;
				return false;
			}

			foundNode = nodePort.Connection.node as DialogueConfigurationNode;
			return true;
		}

        public static void LinkNode(ICompilationValuesRepository nodeValuesRepository, DialogueConfigurationNode node, IDialogueNode nodeValue)
        {
            bool alreadyLinked = nodeValuesRepository.IsLinked(node);

            if(alreadyLinked)
            {
                return;
            }

            node.Link(nodeValue, nodeValuesRepository);

            nodeValuesRepository.AddLinked(node);
        }

        public static IDialogueNode LinkFlow(ICompilationValuesRepository nodeValuesRepository, NodePort nodePort)
        {
            bool nextFound = TryGetNextDialogueFlowNode(nodePort, out DialogueConfigurationNode foundNode);

            if(!nextFound)
            {
                return new SequenceDialogueNode();
            }

            return LinkFlow(nodeValuesRepository, foundNode);
        }

        public static IDialogueNode LinkFlow(ICompilationValuesRepository nodeValuesRepository, DialogueConfigurationNode node)
        {
            DialogueConfigurationNode currNode = node;

            bool valueFound = nodeValuesRepository.TryGetConfigurationValue(
                currNode,
                out IDialogueNode currNodeValue
                );

            if (!valueFound)
            {
                return new CompositeDialogueNode(Array.Empty<IDialogueNode>());
            }

            List<IDialogueNode> nodesFlow = new List<IDialogueNode>();

            while (currNode != null && currNodeValue != null)
            {
                nodesFlow.Add(currNodeValue);

                LinkNode(nodeValuesRepository, currNode, currNodeValue);

                NodePort outputPort = currNode.GetOutputPort(nameof(currNode.Output));

                if (outputPort == null)
                {
                    break;
                }

                bool nextFlowFound = TryGetNextDialogueFlowNode(outputPort, out DialogueConfigurationNode nextNode);
                
                if(!nextFlowFound)
                {
                    break;
                }

                bool nextValueFound = nodeValuesRepository.TryGetConfigurationValue(
                    nextNode,
                    out IDialogueNode nextNodeValue
                    );

                if (!nextValueFound)
                {
                    break;
                }

                currNode = nextNode;
                currNodeValue = nextNodeValue;
            }

            return new CompositeDialogueNode(nodesFlow);
        }

        public static IDialogueTree CompileGraph(DialogueConfigurationGraph graph)
        {
            bool startNodeFound = TryGetStartNode(
                graph,
                out StartDialogueConfigurationNode startNode
                );

            if (!startNodeFound)
            {
                return CreateNopDialogueTree();
            }

            List<DialogueConfigurationNode> nodes = GetNodes<DialogueConfigurationNode>(graph);

            ICompilationValuesRepository nodeValuesRepository = new CompilationValuesRepository();

            foreach (DialogueConfigurationNode node in nodes)
            {
                IDialogueNode dialogueNode = node.Create();

                nodeValuesRepository.AddConfigurationValue(node, dialogueNode);
            }

            NodePort outputPort = startNode.GetOutputPort(nameof(startNode.Output));

            IDialogueNode rootDialogueNode = LinkFlow(nodeValuesRepository, outputPort);

            return new DialogueTree(rootDialogueNode);
        }

        private static IDialogueTree CreateNopDialogueTree()
        {
            return new DialogueTree(new SequenceDialogueNode());
        }
    }
}
