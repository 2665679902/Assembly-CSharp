using System;
using System.Linq;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x0200048B RID: 1163
	[Serializable]
	public class NodeKnob : ScriptableObject
	{
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x00065A4F File Offset: 0x00063C4F
		protected virtual GUIStyle defaultLabelStyle
		{
			get
			{
				return GUI.skin.label;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060031E6 RID: 12774 RVA: 0x00065A5B File Offset: 0x00063C5B
		protected virtual NodeSide defaultSide
		{
			get
			{
				return NodeSide.Right;
			}
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x00065A5E File Offset: 0x00063C5E
		protected void InitBase(Node nodeBody, NodeSide nodeSide, float nodeSidePosition, string knobName)
		{
			this.body = nodeBody;
			this.side = nodeSide;
			this.sidePosition = nodeSidePosition;
			base.name = knobName;
			nodeBody.nodeKnobs.Add(this);
			this.ReloadKnobTexture();
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x00065A8F File Offset: 0x00063C8F
		public virtual void Delete()
		{
			this.body.nodeKnobs.Remove(this);
			UnityEngine.Object.DestroyImmediate(this, true);
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x00065AAA File Offset: 0x00063CAA
		internal void Check()
		{
			if (this.side == (NodeSide)0)
			{
				this.side = this.defaultSide;
			}
			if (this.knobTexture == null)
			{
				this.ReloadKnobTexture();
			}
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x00065AD4 File Offset: 0x00063CD4
		protected void ReloadKnobTexture()
		{
			this.ReloadTexture();
			if (this.knobTexture == null)
			{
				throw new UnityException("Knob texture of " + base.name + " could not be loaded!");
			}
			if (this.side != this.defaultSide)
			{
				ResourceManager.SetDefaultResourcePath(NodeEditor.editorPath + "Resources/");
				int rotationStepsAntiCW = NodeKnob.getRotationStepsAntiCW(this.defaultSide, this.side);
				ResourceManager.MemoryTexture memoryTexture = ResourceManager.FindInMemory(this.knobTexture);
				if (memoryTexture != null)
				{
					string[] array = new string[memoryTexture.modifications.Length + 1];
					memoryTexture.modifications.CopyTo(array, 0);
					array[array.Length - 1] = "Rotation:" + rotationStepsAntiCW.ToString();
					Texture2D texture = ResourceManager.GetTexture(memoryTexture.path, array);
					if (texture != null)
					{
						this.knobTexture = texture;
						return;
					}
					this.knobTexture = RTEditorGUI.RotateTextureCCW(this.knobTexture, rotationStepsAntiCW);
					ResourceManager.AddTextureToMemory(memoryTexture.path, this.knobTexture, array.ToArray<string>());
					return;
				}
				else
				{
					this.knobTexture = RTEditorGUI.RotateTextureCCW(this.knobTexture, rotationStepsAntiCW);
				}
			}
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x00065BE5 File Offset: 0x00063DE5
		protected virtual void ReloadTexture()
		{
			this.knobTexture = RTEditorGUI.ColorToTex(1, Color.red);
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x00065BF8 File Offset: 0x00063DF8
		public virtual ScriptableObject[] GetScriptableObjects()
		{
			return new ScriptableObject[0];
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x00065C00 File Offset: 0x00063E00
		protected internal virtual void CopyScriptableObjects(Func<ScriptableObject, ScriptableObject> replaceSerializableObject)
		{
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x00065C02 File Offset: 0x00063E02
		public virtual void DrawKnob()
		{
			GUI.DrawTexture(this.GetGUIKnob(), this.knobTexture);
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x00065C15 File Offset: 0x00063E15
		public void DisplayLayout()
		{
			this.DisplayLayout(new GUIContent(base.name), this.defaultLabelStyle);
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x00065C2E File Offset: 0x00063E2E
		public void DisplayLayout(GUIStyle style)
		{
			this.DisplayLayout(new GUIContent(base.name), style);
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x00065C42 File Offset: 0x00063E42
		public void DisplayLayout(GUIContent content)
		{
			this.DisplayLayout(content, this.defaultLabelStyle);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x00065C51 File Offset: 0x00063E51
		public void DisplayLayout(GUIContent content, GUIStyle style)
		{
			GUILayout.Label(content, style, Array.Empty<GUILayoutOption>());
			if (Event.current.type == EventType.Repaint)
			{
				this.SetPosition();
			}
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x00065C72 File Offset: 0x00063E72
		public void SetPosition(float position, NodeSide nodeSide)
		{
			if (this.side != nodeSide)
			{
				this.side = nodeSide;
				this.ReloadKnobTexture();
			}
			this.SetPosition(position);
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x00065C91 File Offset: 0x00063E91
		public void SetPosition(float position)
		{
			this.sidePosition = position;
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x00065C9C File Offset: 0x00063E9C
		public void SetPosition()
		{
			Vector2 vector = GUILayoutUtility.GetLastRect().center + this.body.contentOffset;
			this.sidePosition = ((this.side == NodeSide.Bottom || this.side == NodeSide.Top) ? vector.x : vector.y);
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x00065CF0 File Offset: 0x00063EF0
		public Rect GetGUIKnob()
		{
			Rect canvasSpaceKnob = this.GetCanvasSpaceKnob();
			canvasSpaceKnob.position += NodeEditor.curEditorState.zoomPanAdjust + NodeEditor.curEditorState.panOffset;
			return canvasSpaceKnob;
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x00065D30 File Offset: 0x00063F30
		public Rect GetCanvasSpaceKnob()
		{
			this.Check();
			Vector2 vector = new Vector2((float)(this.knobTexture.width / this.knobTexture.height * NodeEditorGUI.knobSize), (float)(this.knobTexture.height / this.knobTexture.width * NodeEditorGUI.knobSize));
			Vector2 knobCenter = this.GetKnobCenter(vector);
			return new Rect(knobCenter.x - vector.x / 2f, knobCenter.y - vector.y / 2f, vector.x, vector.y);
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x00065DC8 File Offset: 0x00063FC8
		private Vector2 GetKnobCenter(Vector2 knobSize)
		{
			if (this.side == NodeSide.Left)
			{
				return this.body.rect.position + new Vector2(-this.sideOffset - knobSize.x / 2f, this.sidePosition);
			}
			if (this.side == NodeSide.Right)
			{
				return this.body.rect.position + new Vector2(this.sideOffset + knobSize.x / 2f + this.body.rect.width, this.sidePosition);
			}
			if (this.side == NodeSide.Bottom)
			{
				return this.body.rect.position + new Vector2(this.sidePosition, this.sideOffset + knobSize.y / 2f + this.body.rect.height);
			}
			return this.body.rect.position + new Vector2(this.sidePosition, -this.sideOffset - knobSize.y / 2f);
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x00065EE3 File Offset: 0x000640E3
		public Vector2 GetDirection()
		{
			if (this.side == NodeSide.Right)
			{
				return Vector2.right;
			}
			if (this.side == NodeSide.Bottom)
			{
				return Vector2.up;
			}
			if (this.side != NodeSide.Top)
			{
				return Vector2.left;
			}
			return Vector2.down;
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x00065F17 File Offset: 0x00064117
		private static int getRotationStepsAntiCW(NodeSide sideA, NodeSide sideB)
		{
			return sideB - sideA + ((sideA > sideB) ? 4 : 0);
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x00065F25 File Offset: 0x00064125
		public virtual Node GetNodeAcrossConnection()
		{
			return null;
		}

		// Token: 0x0400115A RID: 4442
		public Node body;

		// Token: 0x0400115B RID: 4443
		[NonSerialized]
		protected internal Texture2D knobTexture;

		// Token: 0x0400115C RID: 4444
		public NodeSide side;

		// Token: 0x0400115D RID: 4445
		public float sidePosition;

		// Token: 0x0400115E RID: 4446
		public float sideOffset;
	}
}
