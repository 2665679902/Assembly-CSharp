using System;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x0200047C RID: 1148
	public abstract class NodeEditorCallbackReceiver : MonoBehaviour
	{
		// Token: 0x0600315D RID: 12637 RVA: 0x000631EF File Offset: 0x000613EF
		public virtual void OnEditorStartUp()
		{
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000631F1 File Offset: 0x000613F1
		public virtual void OnLoadCanvas(NodeCanvas canvas)
		{
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000631F3 File Offset: 0x000613F3
		public virtual void OnLoadEditorState(NodeEditorState editorState)
		{
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x000631F5 File Offset: 0x000613F5
		public virtual void OnSaveCanvas(NodeCanvas canvas)
		{
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000631F7 File Offset: 0x000613F7
		public virtual void OnSaveEditorState(NodeEditorState editorState)
		{
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000631F9 File Offset: 0x000613F9
		public virtual void OnAddNode(Node node)
		{
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000631FB File Offset: 0x000613FB
		public virtual void OnDeleteNode(Node node)
		{
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000631FD File Offset: 0x000613FD
		public virtual void OnMoveNode(Node node)
		{
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000631FF File Offset: 0x000613FF
		public virtual void OnAddNodeKnob(NodeKnob knob)
		{
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x00063201 File Offset: 0x00061401
		public virtual void OnAddConnection(NodeInput input)
		{
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x00063203 File Offset: 0x00061403
		public virtual void OnRemoveConnection(NodeInput input)
		{
		}
	}
}
