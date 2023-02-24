using System;
using System.Collections.Generic;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x02000488 RID: 1160
	public class NodeEditorState : ScriptableObject
	{
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060031CE RID: 12750 RVA: 0x0006568A File Offset: 0x0006388A
		public Vector2 zoomPos
		{
			get
			{
				return this.canvasRect.size / 2f;
			}
		}

		// Token: 0x04001140 RID: 4416
		public NodeCanvas canvas;

		// Token: 0x04001141 RID: 4417
		public NodeEditorState parentEditor;

		// Token: 0x04001142 RID: 4418
		public bool drawing = true;

		// Token: 0x04001143 RID: 4419
		public Node selectedNode;

		// Token: 0x04001144 RID: 4420
		[NonSerialized]
		public Node focusedNode;

		// Token: 0x04001145 RID: 4421
		[NonSerialized]
		public NodeKnob focusedNodeKnob;

		// Token: 0x04001146 RID: 4422
		public Vector2 panOffset;

		// Token: 0x04001147 RID: 4423
		public float zoom = 1f;

		// Token: 0x04001148 RID: 4424
		[NonSerialized]
		public NodeOutput connectOutput;

		// Token: 0x04001149 RID: 4425
		[NonSerialized]
		public bool dragNode;

		// Token: 0x0400114A RID: 4426
		[NonSerialized]
		public bool panWindow;

		// Token: 0x0400114B RID: 4427
		[NonSerialized]
		public Vector2 dragStart;

		// Token: 0x0400114C RID: 4428
		[NonSerialized]
		public Vector2 dragPos;

		// Token: 0x0400114D RID: 4429
		[NonSerialized]
		public Vector2 dragOffset;

		// Token: 0x0400114E RID: 4430
		[NonSerialized]
		public bool navigate;

		// Token: 0x0400114F RID: 4431
		[NonSerialized]
		public Rect canvasRect;

		// Token: 0x04001150 RID: 4432
		[NonSerialized]
		public Vector2 zoomPanAdjust;

		// Token: 0x04001151 RID: 4433
		[NonSerialized]
		public List<Rect> ignoreInput = new List<Rect>();
	}
}
