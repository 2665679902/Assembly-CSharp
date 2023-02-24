using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace NodeEditorFramework
{
	// Token: 0x02000489 RID: 1161
	public class NodeInput : NodeKnob
	{
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x000656C6 File Offset: 0x000638C6
		protected override NodeSide defaultSide
		{
			get
			{
				return NodeSide.Left;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060031D1 RID: 12753 RVA: 0x000656C9 File Offset: 0x000638C9
		internal TypeData typeData
		{
			get
			{
				this.CheckType();
				return this._typeData;
			}
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x000656D7 File Offset: 0x000638D7
		public static NodeInput Create(Node nodeBody, string inputName, string inputType)
		{
			return NodeInput.Create(nodeBody, inputName, inputType, NodeSide.Left, 20f);
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000656E7 File Offset: 0x000638E7
		public static NodeInput Create(Node nodeBody, string inputName, string inputType, NodeSide nodeSide)
		{
			return NodeInput.Create(nodeBody, inputName, inputType, nodeSide, 20f);
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000656F8 File Offset: 0x000638F8
		public static NodeInput Create(Node nodeBody, string inputName, string inputType, NodeSide nodeSide, float sidePosition)
		{
			NodeInput nodeInput = ScriptableObject.CreateInstance<NodeInput>();
			nodeInput.typeID = inputType;
			nodeInput.InitBase(nodeBody, nodeSide, sidePosition, inputName);
			nodeBody.Inputs.Add(nodeInput);
			return nodeInput;
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x0006572A File Offset: 0x0006392A
		public override void Delete()
		{
			this.RemoveConnection();
			this.body.Inputs.Remove(this);
			base.Delete();
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x0006574A File Offset: 0x0006394A
		protected internal override void CopyScriptableObjects(Func<ScriptableObject, ScriptableObject> replaceSerializableObject)
		{
			this.connection = replaceSerializableObject(this.connection) as NodeOutput;
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x00065763 File Offset: 0x00063963
		protected override void ReloadTexture()
		{
			this.CheckType();
			this.knobTexture = this.typeData.InKnobTex;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x0006577C File Offset: 0x0006397C
		private void CheckType()
		{
			if (this._typeData == null || !this._typeData.isValid())
			{
				this._typeData = ConnectionTypes.GetTypeData(this.typeID);
			}
			if (this._typeData == null || !this._typeData.isValid())
			{
				ConnectionTypes.FetchTypes();
				this._typeData = ConnectionTypes.GetTypeData(this.typeID);
				if (this._typeData == null || !this._typeData.isValid())
				{
					throw new UnityException("Could not find type " + this.typeID + "!");
				}
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060031D9 RID: 12761 RVA: 0x0006580A File Offset: 0x00063A0A
		public bool IsValueNull
		{
			get
			{
				return !(this.connection != null) || this.connection.IsValueNull;
			}
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x00065827 File Offset: 0x00063A27
		public object GetValue()
		{
			if (!(this.connection != null))
			{
				return null;
			}
			return this.connection.GetValue();
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x00065844 File Offset: 0x00063A44
		public object GetValue(Type type)
		{
			if (!(this.connection != null))
			{
				return null;
			}
			return this.connection.GetValue(type);
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x00065862 File Offset: 0x00063A62
		public void SetValue(object value)
		{
			if (this.connection != null)
			{
				this.connection.SetValue(value);
			}
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x0006587E File Offset: 0x00063A7E
		public T GetValue<T>()
		{
			if (!(this.connection != null))
			{
				return NodeOutput.GetDefault<T>();
			}
			return this.connection.GetValue<T>();
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x0006589F File Offset: 0x00063A9F
		public void SetValue<T>(T value)
		{
			if (this.connection != null)
			{
				this.connection.SetValue<T>(value);
			}
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000658BB File Offset: 0x00063ABB
		public bool TryApplyConnection(NodeOutput output)
		{
			if (this.CanApplyConnection(output))
			{
				this.ApplyConnection(output);
				return true;
			}
			return false;
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000658D0 File Offset: 0x00063AD0
		public bool CanApplyConnection(NodeOutput output)
		{
			if (output == null || this.body == output.body || this.connection == output || !this.typeData.Type.IsAssignableFrom(output.typeData.Type))
			{
				return false;
			}
			if (output.body.isChildOf(this.body) && !output.body.allowsLoopRecursion(this.body))
			{
				global::Debug.LogWarning("Cannot apply connection: Recursion detected!");
				return false;
			}
			return true;
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x0006595C File Offset: 0x00063B5C
		public void ApplyConnection(NodeOutput output)
		{
			if (output == null)
			{
				return;
			}
			if (this.connection != null)
			{
				NodeEditorCallbacks.IssueOnRemoveConnection(this);
				this.connection.connections.Remove(this);
			}
			this.connection = output;
			output.connections.Add(this);
			if (!output.body.calculated)
			{
				NodeEditor.RecalculateFrom(output.body);
			}
			else
			{
				NodeEditor.RecalculateFrom(this.body);
			}
			output.body.OnAddOutputConnection(output);
			this.body.OnAddInputConnection(this);
			NodeEditorCallbacks.IssueOnAddConnection(this);
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000659EF File Offset: 0x00063BEF
		public void RemoveConnection()
		{
			if (this.connection == null)
			{
				return;
			}
			NodeEditorCallbacks.IssueOnRemoveConnection(this);
			this.connection.connections.Remove(this);
			this.connection = null;
			NodeEditor.RecalculateFrom(this.body);
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x00065A2A File Offset: 0x00063C2A
		public override Node GetNodeAcrossConnection()
		{
			if (!(this.connection != null))
			{
				return null;
			}
			return this.connection.body;
		}

		// Token: 0x04001152 RID: 4434
		public NodeOutput connection;

		// Token: 0x04001153 RID: 4435
		[FormerlySerializedAs("type")]
		public string typeID;

		// Token: 0x04001154 RID: 4436
		private TypeData _typeData;
	}
}
