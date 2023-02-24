using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x02000476 RID: 1142
	public abstract class Node : ScriptableObject
	{
		// Token: 0x06003116 RID: 12566 RVA: 0x00061A8C File Offset: 0x0005FC8C
		protected internal void InitBase()
		{
			NodeEditor.RecalculateFrom(this);
			if (NodeEditor.curNodeCanvas == null || NodeEditor.curNodeCanvas.nodes == null)
			{
				return;
			}
			if (!NodeEditor.curNodeCanvas.nodes.Contains(this))
			{
				NodeEditor.curNodeCanvas.nodes.Add(this);
			}
			NodeEditor.RepaintClients();
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x00061AE0 File Offset: 0x0005FCE0
		public void Delete()
		{
			if (!NodeEditor.curNodeCanvas.nodes.Contains(this))
			{
				throw new UnityException(string.Concat(new string[]
				{
					"The Node ",
					base.name,
					" does not exist on the Canvas ",
					NodeEditor.curNodeCanvas.name,
					"!"
				}));
			}
			NodeEditorCallbacks.IssueOnDeleteNode(this);
			NodeEditor.curNodeCanvas.nodes.Remove(this);
			for (int i = 0; i < this.Outputs.Count; i++)
			{
				NodeOutput nodeOutput = this.Outputs[i];
				while (nodeOutput.connections.Count != 0)
				{
					nodeOutput.connections[0].RemoveConnection();
				}
				UnityEngine.Object.DestroyImmediate(nodeOutput, true);
			}
			for (int j = 0; j < this.Inputs.Count; j++)
			{
				NodeInput nodeInput = this.Inputs[j];
				if (nodeInput.connection != null)
				{
					nodeInput.connection.connections.Remove(nodeInput);
				}
				UnityEngine.Object.DestroyImmediate(nodeInput, true);
			}
			for (int k = 0; k < this.nodeKnobs.Count; k++)
			{
				if (this.nodeKnobs[k] != null)
				{
					UnityEngine.Object.DestroyImmediate(this.nodeKnobs[k], true);
				}
			}
			UnityEngine.Object.DestroyImmediate(this, true);
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x00061C32 File Offset: 0x0005FE32
		public static Node Create(string nodeID, Vector2 position)
		{
			return Node.Create(nodeID, position, null);
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x00061C3C File Offset: 0x0005FE3C
		public static Node Create(string nodeID, Vector2 position, NodeOutput connectingOutput)
		{
			Node node = NodeTypes.getDefaultNode(nodeID);
			if (node == null)
			{
				throw new UnityException("Cannot create Node with id " + nodeID + " as no such Node type is registered!");
			}
			node = node.Create(position);
			node.InitBase();
			if (connectingOutput != null)
			{
				using (List<NodeInput>.Enumerator enumerator = node.Inputs.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.TryApplyConnection(connectingOutput))
						{
							break;
						}
					}
				}
			}
			NodeEditorCallbacks.IssueOnAddNode(node);
			return node;
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x00061CD8 File Offset: 0x0005FED8
		internal void CheckNodeKnobMigration()
		{
			if (this.nodeKnobs.Count == 0 && (this.Inputs.Count != 0 || this.Outputs.Count != 0))
			{
				this.nodeKnobs.AddRange(this.Inputs.Cast<NodeKnob>());
				this.nodeKnobs.AddRange(this.Outputs.Cast<NodeKnob>());
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600311B RID: 12571
		public abstract string GetID { get; }

		// Token: 0x0600311C RID: 12572
		public abstract Node Create(Vector2 pos);

		// Token: 0x0600311D RID: 12573
		protected internal abstract void NodeGUI();

		// Token: 0x0600311E RID: 12574 RVA: 0x00061D38 File Offset: 0x0005FF38
		public virtual void DrawNodePropertyEditor()
		{
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x00061D3A File Offset: 0x0005FF3A
		public virtual bool Calculate()
		{
			return true;
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x00061D3D File Offset: 0x0005FF3D
		public virtual bool AllowRecursion
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06003121 RID: 12577 RVA: 0x00061D40 File Offset: 0x0005FF40
		public virtual bool ContinueCalculation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x00061D43 File Offset: 0x0005FF43
		protected internal virtual void OnDelete()
		{
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x00061D45 File Offset: 0x0005FF45
		protected internal virtual void OnAddInputConnection(NodeInput input)
		{
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x00061D47 File Offset: 0x0005FF47
		protected internal virtual void OnAddOutputConnection(NodeOutput output)
		{
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x00061D49 File Offset: 0x0005FF49
		public virtual ScriptableObject[] GetScriptableObjects()
		{
			return new ScriptableObject[0];
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x00061D51 File Offset: 0x0005FF51
		protected internal virtual void CopyScriptableObjects(Func<ScriptableObject, ScriptableObject> replaceSerializableObject)
		{
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x00061D53 File Offset: 0x0005FF53
		public void SerializeInputsAndOutputs(Func<ScriptableObject, ScriptableObject> replaceSerializableObject)
		{
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x00061D58 File Offset: 0x0005FF58
		protected internal virtual void DrawNode()
		{
			Rect rect = this.rect;
			rect.position += NodeEditor.curEditorState.zoomPanAdjust + NodeEditor.curEditorState.panOffset;
			this.contentOffset = new Vector2(0f, 20f);
			GUI.Label(new Rect(rect.x, rect.y, rect.width, this.contentOffset.y), base.name, (NodeEditor.curEditorState.selectedNode == this) ? NodeEditorGUI.nodeBoxBold : NodeEditorGUI.nodeBox);
			Rect rect2 = new Rect(rect.x, rect.y + this.contentOffset.y, rect.width, rect.height - this.contentOffset.y);
			GUI.BeginGroup(rect2, GUI.skin.box);
			rect2.position = Vector2.zero;
			GUILayout.BeginArea(rect2, GUI.skin.box);
			GUI.changed = false;
			this.NodeGUI();
			GUILayout.EndArea();
			GUI.EndGroup();
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x00061E78 File Offset: 0x00060078
		protected internal virtual void DrawKnobs()
		{
			this.CheckNodeKnobMigration();
			for (int i = 0; i < this.nodeKnobs.Count; i++)
			{
				this.nodeKnobs[i].DrawKnob();
			}
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x00061EB4 File Offset: 0x000600B4
		protected internal virtual void DrawConnections()
		{
			this.CheckNodeKnobMigration();
			if (Event.current.type != EventType.Repaint)
			{
				return;
			}
			for (int i = 0; i < this.Outputs.Count; i++)
			{
				NodeOutput nodeOutput = this.Outputs[i];
				Vector2 center = nodeOutput.GetGUIKnob().center;
				Vector2 direction = nodeOutput.GetDirection();
				for (int j = 0; j < nodeOutput.connections.Count; j++)
				{
					NodeInput nodeInput = nodeOutput.connections[j];
					NodeEditorGUI.DrawConnection(center, direction, nodeInput.GetGUIKnob().center, nodeInput.GetDirection(), nodeOutput.typeData.Color);
				}
			}
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x00061F64 File Offset: 0x00060164
		protected internal bool allInputsReady()
		{
			for (int i = 0; i < this.Inputs.Count; i++)
			{
				if (this.Inputs[i].connection == null || this.Inputs[i].connection.IsValueNull)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x00061FBC File Offset: 0x000601BC
		protected internal bool hasUnassignedInputs()
		{
			for (int i = 0; i < this.Inputs.Count; i++)
			{
				if (this.Inputs[i].connection == null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x00061FFC File Offset: 0x000601FC
		protected internal bool descendantsCalculated()
		{
			for (int i = 0; i < this.Inputs.Count; i++)
			{
				if (this.Inputs[i].connection != null && !this.Inputs[i].connection.body.calculated)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x00062058 File Offset: 0x00060258
		protected internal bool isInput()
		{
			for (int i = 0; i < this.Inputs.Count; i++)
			{
				if (this.Inputs[i].connection != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x00062097 File Offset: 0x00060297
		public NodeOutput CreateOutput(string outputName, string outputType)
		{
			return NodeOutput.Create(this, outputName, outputType);
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000620A1 File Offset: 0x000602A1
		public NodeOutput CreateOutput(string outputName, string outputType, NodeSide nodeSide)
		{
			return NodeOutput.Create(this, outputName, outputType, nodeSide);
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000620AC File Offset: 0x000602AC
		public NodeOutput CreateOutput(string outputName, string outputType, NodeSide nodeSide, float sidePosition)
		{
			return NodeOutput.Create(this, outputName, outputType, nodeSide, sidePosition);
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000620B9 File Offset: 0x000602B9
		protected void OutputKnob(int outputIdx)
		{
			if (Event.current.type == EventType.Repaint)
			{
				this.Outputs[outputIdx].SetPosition();
			}
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000620D9 File Offset: 0x000602D9
		public NodeInput CreateInput(string inputName, string inputType)
		{
			return NodeInput.Create(this, inputName, inputType);
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000620E3 File Offset: 0x000602E3
		public NodeInput CreateInput(string inputName, string inputType, NodeSide nodeSide)
		{
			return NodeInput.Create(this, inputName, inputType, nodeSide);
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000620EE File Offset: 0x000602EE
		public NodeInput CreateInput(string inputName, string inputType, NodeSide nodeSide, float sidePosition)
		{
			return NodeInput.Create(this, inputName, inputType, nodeSide, sidePosition);
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000620FB File Offset: 0x000602FB
		protected void InputKnob(int inputIdx)
		{
			if (Event.current.type == EventType.Repaint)
			{
				this.Inputs[inputIdx].SetPosition();
			}
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x0006211C File Offset: 0x0006031C
		protected static void ReassignOutputType(ref NodeOutput output, Type newOutputType)
		{
			Node body = output.body;
			string name = output.name;
			IEnumerable<NodeInput> enumerable = output.connections.Where((NodeInput connection) => connection.typeData.Type.IsAssignableFrom(newOutputType));
			output.Delete();
			NodeEditorCallbacks.IssueOnAddNodeKnob(NodeOutput.Create(body, name, newOutputType.AssemblyQualifiedName));
			output = body.Outputs[body.Outputs.Count - 1];
			foreach (NodeInput nodeInput in enumerable)
			{
				nodeInput.ApplyConnection(output);
			}
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000621D0 File Offset: 0x000603D0
		protected static void ReassignInputType(ref NodeInput input, Type newInputType)
		{
			Node body = input.body;
			string name = input.name;
			NodeOutput nodeOutput = null;
			if (input.connection != null && newInputType.IsAssignableFrom(input.connection.typeData.Type))
			{
				nodeOutput = input.connection;
			}
			input.Delete();
			NodeEditorCallbacks.IssueOnAddNodeKnob(NodeInput.Create(body, name, newInputType.AssemblyQualifiedName));
			input = body.Inputs[body.Inputs.Count - 1];
			if (nodeOutput != null)
			{
				input.ApplyConnection(nodeOutput);
			}
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x00062264 File Offset: 0x00060464
		public bool isChildOf(Node otherNode)
		{
			if (otherNode == null || otherNode == this)
			{
				return false;
			}
			if (this.BeginRecursiveSearchLoop())
			{
				return false;
			}
			for (int i = 0; i < this.Inputs.Count; i++)
			{
				NodeOutput connection = this.Inputs[i].connection;
				if (connection != null && connection.body != this.startRecursiveSearchNode && (connection.body == otherNode || connection.body.isChildOf(otherNode)))
				{
					this.StopRecursiveSearchLoop();
					return true;
				}
			}
			this.EndRecursiveSearchLoop();
			return false;
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x00062300 File Offset: 0x00060500
		internal bool isInLoop()
		{
			if (this.BeginRecursiveSearchLoop())
			{
				return this == this.startRecursiveSearchNode;
			}
			for (int i = 0; i < this.Inputs.Count; i++)
			{
				NodeOutput connection = this.Inputs[i].connection;
				if (connection != null && connection.body.isInLoop())
				{
					this.StopRecursiveSearchLoop();
					return true;
				}
			}
			this.EndRecursiveSearchLoop();
			return false;
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x00062370 File Offset: 0x00060570
		internal bool allowsLoopRecursion(Node otherNode)
		{
			if (this.AllowRecursion)
			{
				return true;
			}
			if (otherNode == null)
			{
				return false;
			}
			if (this.BeginRecursiveSearchLoop())
			{
				return false;
			}
			for (int i = 0; i < this.Inputs.Count; i++)
			{
				NodeOutput connection = this.Inputs[i].connection;
				if (connection != null && connection.body.allowsLoopRecursion(otherNode))
				{
					this.StopRecursiveSearchLoop();
					return true;
				}
			}
			this.EndRecursiveSearchLoop();
			return false;
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x000623EC File Offset: 0x000605EC
		public void ClearCalculation()
		{
			if (this.BeginRecursiveSearchLoop())
			{
				return;
			}
			this.calculated = false;
			for (int i = 0; i < this.Outputs.Count; i++)
			{
				NodeOutput nodeOutput = this.Outputs[i];
				for (int j = 0; j < nodeOutput.connections.Count; j++)
				{
					nodeOutput.connections[j].body.ClearCalculation();
				}
			}
			this.EndRecursiveSearchLoop();
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x00062460 File Offset: 0x00060660
		internal bool BeginRecursiveSearchLoop()
		{
			if (this.startRecursiveSearchNode == null || this.recursiveSearchSurpassed == null)
			{
				this.recursiveSearchSurpassed = new List<Node>();
				this.startRecursiveSearchNode = this;
			}
			if (this.recursiveSearchSurpassed.Contains(this))
			{
				return true;
			}
			this.recursiveSearchSurpassed.Add(this);
			return false;
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000624B2 File Offset: 0x000606B2
		internal void EndRecursiveSearchLoop()
		{
			if (this.startRecursiveSearchNode == this)
			{
				this.recursiveSearchSurpassed = null;
				this.startRecursiveSearchNode = null;
			}
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x000624D0 File Offset: 0x000606D0
		internal void StopRecursiveSearchLoop()
		{
			this.recursiveSearchSurpassed = null;
			this.startRecursiveSearchNode = null;
		}

		// Token: 0x040010F4 RID: 4340
		public Rect rect;

		// Token: 0x040010F5 RID: 4341
		internal Vector2 contentOffset = Vector2.zero;

		// Token: 0x040010F6 RID: 4342
		[SerializeField]
		public List<NodeKnob> nodeKnobs = new List<NodeKnob>();

		// Token: 0x040010F7 RID: 4343
		[SerializeField]
		public List<NodeInput> Inputs = new List<NodeInput>();

		// Token: 0x040010F8 RID: 4344
		[SerializeField]
		public List<NodeOutput> Outputs = new List<NodeOutput>();

		// Token: 0x040010F9 RID: 4345
		[HideInInspector]
		[NonSerialized]
		internal bool calculated = true;

		// Token: 0x040010FA RID: 4346
		[NonSerialized]
		private List<Node> recursiveSearchSurpassed;

		// Token: 0x040010FB RID: 4347
		[NonSerialized]
		private Node startRecursiveSearchNode;
	}
}
