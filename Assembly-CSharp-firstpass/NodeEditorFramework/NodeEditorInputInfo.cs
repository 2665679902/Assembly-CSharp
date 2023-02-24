using System;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x02000481 RID: 1153
	public class NodeEditorInputInfo
	{
		// Token: 0x0600319A RID: 12698 RVA: 0x0006493C File Offset: 0x00062B3C
		public NodeEditorInputInfo(NodeEditorState EditorState)
		{
			this.message = null;
			this.editorState = EditorState;
			this.inputEvent = Event.current;
			this.inputPos = this.inputEvent.mousePosition;
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x0006496E File Offset: 0x00062B6E
		public NodeEditorInputInfo(string Message, NodeEditorState EditorState)
		{
			this.message = Message;
			this.editorState = EditorState;
			this.inputEvent = Event.current;
			this.inputPos = this.inputEvent.mousePosition;
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x000649A0 File Offset: 0x00062BA0
		public void SetAsCurrentEnvironment()
		{
			NodeEditor.curEditorState = this.editorState;
			NodeEditor.curNodeCanvas = this.editorState.canvas;
		}

		// Token: 0x0400112E RID: 4398
		public string message;

		// Token: 0x0400112F RID: 4399
		public NodeEditorState editorState;

		// Token: 0x04001130 RID: 4400
		public Event inputEvent;

		// Token: 0x04001131 RID: 4401
		public Vector2 inputPos;
	}
}
