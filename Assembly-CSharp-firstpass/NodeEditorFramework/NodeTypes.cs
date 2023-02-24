using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x0200048D RID: 1165
	public static class NodeTypes
	{
		// Token: 0x06003212 RID: 12818 RVA: 0x00066378 File Offset: 0x00064578
		public static void FetchNodes()
		{
			NodeTypes.nodes = new Dictionary<Node, NodeData>();
			foreach (Assembly assembly2 in from assembly in AppDomain.CurrentDomain.GetAssemblies()
				where assembly.FullName.Contains("Assembly")
				select assembly)
			{
				foreach (Type type in from T in assembly2.GetTypes()
					where T.IsClass && !T.IsAbstract && T.IsSubclassOf(typeof(Node))
					select T)
				{
					NodeAttribute nodeAttribute = type.GetCustomAttributes(typeof(NodeAttribute), false)[0] as NodeAttribute;
					if (nodeAttribute == null || !nodeAttribute.hide)
					{
						try
						{
							Node node = ScriptableObject.CreateInstance(type.Name) as Node;
							node = node.Create(Vector2.zero);
							NodeTypes.nodes.Add(node, new NodeData((nodeAttribute == null) ? node.name : nodeAttribute.contextText, nodeAttribute.typeOfNodeCanvas));
						}
						catch (Exception ex)
						{
							global::Debug.LogError(ex.Message + " " + type.Name);
						}
					}
				}
			}
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x000664EC File Offset: 0x000646EC
		public static NodeData getNodeData(Node node)
		{
			return NodeTypes.nodes[NodeTypes.getDefaultNode(node.GetID)];
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x00066504 File Offset: 0x00064704
		public static Node getDefaultNode(string nodeID)
		{
			return NodeTypes.nodes.Keys.Single((Node node) => node.GetID == nodeID);
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x00066539 File Offset: 0x00064739
		public static T getDefaultNode<T>() where T : Node
		{
			return NodeTypes.nodes.Keys.Single((Node node) => node.GetType() == typeof(T)) as T;
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x00066574 File Offset: 0x00064774
		public static List<Node> getCompatibleNodes(NodeOutput nodeOutput)
		{
			if (nodeOutput == null)
			{
				throw new ArgumentNullException("nodeOutput");
			}
			List<Node> list = new List<Node>();
			foreach (Node node in NodeTypes.nodes.Keys)
			{
				for (int i = 0; i < node.Inputs.Count; i++)
				{
					NodeInput nodeInput = node.Inputs[i];
					if (nodeInput == null)
					{
						throw new UnityException("Input " + i.ToString() + " is null!");
					}
					if (nodeInput.typeData.Type.IsAssignableFrom(nodeOutput.typeData.Type))
					{
						list.Add(node);
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x04001165 RID: 4453
		public static Dictionary<Node, NodeData> nodes;
	}
}
