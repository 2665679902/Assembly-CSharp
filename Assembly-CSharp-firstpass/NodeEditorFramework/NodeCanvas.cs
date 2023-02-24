using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x02000477 RID: 1143
	[NodeCanvasType("Default")]
	public class NodeCanvas : ScriptableObject
	{
		// Token: 0x06003141 RID: 12609 RVA: 0x0006251C File Offset: 0x0006071C
		public void Validate()
		{
			if (this.nodes == null)
			{
				global::Debug.LogWarning("NodeCanvas '" + base.name + "' nodes were erased and set to null! Automatically fixed!");
				this.nodes = new List<Node>();
			}
			for (int i = 0; i < this.nodes.Count; i++)
			{
				Node node = this.nodes[i];
				if (node == null)
				{
					global::Debug.LogWarning("NodeCanvas '" + base.name + "' contained broken (null) nodes! Automatically fixed!");
					this.nodes.RemoveAt(i);
					i--;
				}
				else
				{
					for (int j = 0; j < node.Inputs.Count; j++)
					{
						NodeInput nodeInput = node.Inputs[j];
						if (nodeInput == null)
						{
							global::Debug.LogWarning(string.Concat(new string[] { "NodeCanvas '", base.name, "' Node '", node.name, "' contained broken (null) NodeKnobs! Automatically fixed!" }));
							node.Inputs.RemoveAt(j);
							j--;
						}
						else if (nodeInput.connection != null && nodeInput.connection.body == null)
						{
							nodeInput.connection = null;
						}
					}
					for (int k = 0; k < node.Outputs.Count; k++)
					{
						NodeOutput nodeOutput = node.Outputs[k];
						if (nodeOutput == null)
						{
							global::Debug.LogWarning(string.Concat(new string[] { "NodeCanvas '", base.name, "' Node '", node.name, "' contained broken (null) NodeKnobs! Automatically fixed!" }));
							node.Outputs.RemoveAt(k);
							k--;
						}
						else
						{
							for (int l = 0; l < nodeOutput.connections.Count; l++)
							{
								NodeInput nodeInput2 = nodeOutput.connections[l];
								if (nodeInput2 == null || nodeInput2.body == null)
								{
									nodeOutput.connections.RemoveAt(l);
									l--;
								}
							}
						}
					}
					for (int m = 0; m < node.nodeKnobs.Count; m++)
					{
						NodeKnob nodeKnob = node.nodeKnobs[m];
						if (nodeKnob == null)
						{
							global::Debug.LogWarning(string.Concat(new string[] { "NodeCanvas '", base.name, "' Node '", node.name, "' contained broken (null) NodeKnobs! Automatically fixed!" }));
							node.nodeKnobs.RemoveAt(m);
							m--;
						}
						else if (nodeKnob is NodeInput)
						{
							NodeInput nodeInput3 = nodeKnob as NodeInput;
							if (nodeInput3.connection != null && nodeInput3.connection.body == null)
							{
								nodeInput3.connection = null;
							}
						}
						else if (nodeKnob is NodeOutput)
						{
							NodeOutput nodeOutput2 = nodeKnob as NodeOutput;
							for (int n = 0; n < nodeOutput2.connections.Count; n++)
							{
								NodeInput nodeInput4 = nodeOutput2.connections[n];
								if (nodeInput4 == null || nodeInput4.body == null)
								{
									nodeOutput2.connections.RemoveAt(n);
									n--;
								}
							}
						}
					}
				}
			}
			if (this.editorStates == null)
			{
				global::Debug.LogWarning("NodeCanvas '" + base.name + "' editorStates were erased! Automatically fixed!");
				this.editorStates = new NodeEditorState[0];
			}
			this.editorStates = this.editorStates.Where((NodeEditorState state) => state != null).ToArray<NodeEditorState>();
			foreach (NodeEditorState nodeEditorState in this.editorStates)
			{
				if (!this.nodes.Contains(nodeEditorState.selectedNode))
				{
					nodeEditorState.selectedNode = null;
				}
			}
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x00062915 File Offset: 0x00060B15
		public virtual void BeforeSavingCanvas()
		{
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x00062917 File Offset: 0x00060B17
		public virtual void AdditionalSaveMethods(string sceneCanvasName, NodeCanvas.CompleteLoadCallback onComplete)
		{
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x00062919 File Offset: 0x00060B19
		public virtual string DrawAdditionalSettings(string sceneCanvasName)
		{
			return sceneCanvasName;
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x0006291C File Offset: 0x00060B1C
		public virtual void UpdateSettings(string sceneCanvasName)
		{
		}

		// Token: 0x040010FC RID: 4348
		public List<Node> nodes = new List<Node>();

		// Token: 0x040010FD RID: 4349
		public NodeEditorState[] editorStates = new NodeEditorState[0];

		// Token: 0x040010FE RID: 4350
		public bool livesInScene;

		// Token: 0x02000AB7 RID: 2743
		// (Invoke) Token: 0x0600572D RID: 22317
		public delegate void CompleteLoadCallback(string fileName, NodeCanvas canvas);
	}
}
