using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace NodeEditorFramework
{
	// Token: 0x0200048C RID: 1164
	public class NodeOutput : NodeKnob
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060031FD RID: 12797 RVA: 0x00065F30 File Offset: 0x00064130
		protected override NodeSide defaultSide
		{
			get
			{
				return NodeSide.Right;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x00065F33 File Offset: 0x00064133
		protected override GUIStyle defaultLabelStyle
		{
			get
			{
				if (NodeOutput._defaultStyle == null)
				{
					NodeOutput._defaultStyle = new GUIStyle(GUI.skin.label);
					NodeOutput._defaultStyle.alignment = TextAnchor.MiddleRight;
				}
				return NodeOutput._defaultStyle;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060031FF RID: 12799 RVA: 0x00065F60 File Offset: 0x00064160
		internal TypeData typeData
		{
			get
			{
				this.CheckType();
				return this._typeData;
			}
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x00065F6E File Offset: 0x0006416E
		public static NodeOutput Create(Node nodeBody, string outputName, string outputType)
		{
			return NodeOutput.Create(nodeBody, outputName, outputType, NodeSide.Right, 20f);
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x00065F7E File Offset: 0x0006417E
		public static NodeOutput Create(Node nodeBody, string outputName, string outputType, NodeSide nodeSide)
		{
			return NodeOutput.Create(nodeBody, outputName, outputType, nodeSide, 20f);
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x00065F90 File Offset: 0x00064190
		public static NodeOutput Create(Node nodeBody, string outputName, string outputType, NodeSide nodeSide, float sidePosition)
		{
			NodeOutput nodeOutput = ScriptableObject.CreateInstance<NodeOutput>();
			nodeOutput.typeID = outputType;
			nodeOutput.InitBase(nodeBody, nodeSide, sidePosition, outputName);
			nodeBody.Outputs.Add(nodeOutput);
			return nodeOutput;
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x00065FC2 File Offset: 0x000641C2
		public override void Delete()
		{
			while (this.connections.Count > 0)
			{
				this.connections[0].RemoveConnection();
			}
			this.body.Outputs.Remove(this);
			base.Delete();
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x00066000 File Offset: 0x00064200
		protected internal override void CopyScriptableObjects(Func<ScriptableObject, ScriptableObject> replaceSerializableObject)
		{
			for (int i = 0; i < this.connections.Count; i++)
			{
				this.connections[i] = replaceSerializableObject(this.connections[i]) as NodeInput;
			}
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x00066046 File Offset: 0x00064246
		protected override void ReloadTexture()
		{
			this.CheckType();
			this.knobTexture = this.typeData.OutKnobTex;
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x00066060 File Offset: 0x00064260
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

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x000660EE File Offset: 0x000642EE
		public bool IsValueNull
		{
			get
			{
				return this.value == null;
			}
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x000660F9 File Offset: 0x000642F9
		public object GetValue()
		{
			return this.value;
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x00066104 File Offset: 0x00064304
		public object GetValue(Type type)
		{
			if (type == null)
			{
				throw new UnityException("Trying to get value of " + base.name + " with null type!");
			}
			this.CheckType();
			if (type.IsAssignableFrom(this.typeData.Type))
			{
				return this.value;
			}
			global::Debug.LogError("Trying to GetValue<" + type.FullName + "> for Output Type: " + this.typeData.Type.FullName);
			return null;
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x00066180 File Offset: 0x00064380
		public void SetValue(object Value)
		{
			this.CheckType();
			if (Value == null || this.typeData.Type.IsAssignableFrom(Value.GetType()))
			{
				this.value = Value;
				return;
			}
			global::Debug.LogError("Trying to SetValue of type " + Value.GetType().FullName + " for Output Type: " + this.typeData.Type.FullName);
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x000661E8 File Offset: 0x000643E8
		public T GetValue<T>()
		{
			this.CheckType();
			if (typeof(T).IsAssignableFrom(this.typeData.Type))
			{
				object obj;
				if ((obj = this.value) == null)
				{
					obj = (this.value = NodeOutput.GetDefault<T>());
				}
				return (T)((object)obj);
			}
			global::Debug.LogError("Trying to GetValue<" + typeof(T).FullName + "> for Output Type: " + this.typeData.Type.FullName);
			return NodeOutput.GetDefault<T>();
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x00066274 File Offset: 0x00064474
		public void SetValue<T>(T Value)
		{
			this.CheckType();
			if (this.typeData.Type.IsAssignableFrom(typeof(T)))
			{
				this.value = Value;
				return;
			}
			global::Debug.LogError("Trying to SetValue<" + typeof(T).FullName + "> for Output Type: " + this.typeData.Type.FullName);
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x000662E3 File Offset: 0x000644E3
		public void ResetValue()
		{
			this.value = null;
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x000662EC File Offset: 0x000644EC
		public static T GetDefault<T>()
		{
			if (typeof(T).GetConstructor(Type.EmptyTypes) != null)
			{
				return Activator.CreateInstance<T>();
			}
			return default(T);
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x00066324 File Offset: 0x00064524
		public static object GetDefault(Type type)
		{
			if (type.GetConstructor(Type.EmptyTypes) != null)
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x00066341 File Offset: 0x00064541
		public override Node GetNodeAcrossConnection()
		{
			if (this.connections.Count <= 0)
			{
				return null;
			}
			return this.connections[0].body;
		}

		// Token: 0x0400115F RID: 4447
		private static GUIStyle _defaultStyle;

		// Token: 0x04001160 RID: 4448
		public List<NodeInput> connections = new List<NodeInput>();

		// Token: 0x04001161 RID: 4449
		[FormerlySerializedAs("type")]
		public string typeID;

		// Token: 0x04001162 RID: 4450
		private TypeData _typeData;

		// Token: 0x04001163 RID: 4451
		[NonSerialized]
		private object value;

		// Token: 0x04001164 RID: 4452
		public bool calculationBlockade;
	}
}
