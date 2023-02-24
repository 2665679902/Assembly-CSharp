using System;
using System.Collections.Generic;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x0200047D RID: 1149
	public static class NodeEditorCallbacks
	{
		// Token: 0x06003169 RID: 12649 RVA: 0x0006320D File Offset: 0x0006140D
		public static void SetupReceivers()
		{
			NodeEditorCallbacks.callbackReceiver = new List<NodeEditorCallbackReceiver>(UnityEngine.Object.FindObjectsOfType<NodeEditorCallbackReceiver>());
			NodeEditorCallbacks.receiverCount = NodeEditorCallbacks.callbackReceiver.Count;
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x00063230 File Offset: 0x00061430
		public static void IssueOnEditorStartUp()
		{
			if (NodeEditorCallbacks.OnEditorStartUp != null)
			{
				NodeEditorCallbacks.OnEditorStartUp();
			}
			for (int i = 0; i < NodeEditorCallbacks.receiverCount; i++)
			{
				if (NodeEditorCallbacks.callbackReceiver[i] == null)
				{
					NodeEditorCallbacks.callbackReceiver.RemoveAt(i--);
				}
				else
				{
					NodeEditorCallbacks.callbackReceiver[i].OnEditorStartUp();
				}
			}
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x00063294 File Offset: 0x00061494
		public static void IssueOnLoadCanvas(NodeCanvas canvas)
		{
			if (NodeEditorCallbacks.OnLoadCanvas != null)
			{
				NodeEditorCallbacks.OnLoadCanvas(canvas);
			}
			for (int i = 0; i < NodeEditorCallbacks.receiverCount; i++)
			{
				if (NodeEditorCallbacks.callbackReceiver[i] == null)
				{
					NodeEditorCallbacks.callbackReceiver.RemoveAt(i--);
				}
				else
				{
					NodeEditorCallbacks.callbackReceiver[i].OnLoadCanvas(canvas);
				}
			}
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x000632F8 File Offset: 0x000614F8
		public static void IssueOnLoadEditorState(NodeEditorState editorState)
		{
			if (NodeEditorCallbacks.OnLoadEditorState != null)
			{
				NodeEditorCallbacks.OnLoadEditorState(editorState);
			}
			for (int i = 0; i < NodeEditorCallbacks.receiverCount; i++)
			{
				if (NodeEditorCallbacks.callbackReceiver[i] == null)
				{
					NodeEditorCallbacks.callbackReceiver.RemoveAt(i--);
				}
				else
				{
					NodeEditorCallbacks.callbackReceiver[i].OnLoadEditorState(editorState);
				}
			}
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x0006335C File Offset: 0x0006155C
		public static void IssueOnSaveCanvas(NodeCanvas canvas)
		{
			if (NodeEditorCallbacks.OnSaveCanvas != null)
			{
				NodeEditorCallbacks.OnSaveCanvas(canvas);
			}
			for (int i = 0; i < NodeEditorCallbacks.receiverCount; i++)
			{
				if (NodeEditorCallbacks.callbackReceiver[i] == null)
				{
					NodeEditorCallbacks.callbackReceiver.RemoveAt(i--);
				}
				else
				{
					NodeEditorCallbacks.callbackReceiver[i].OnSaveCanvas(canvas);
				}
			}
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000633C0 File Offset: 0x000615C0
		public static void IssueOnSaveEditorState(NodeEditorState editorState)
		{
			if (NodeEditorCallbacks.OnSaveEditorState != null)
			{
				NodeEditorCallbacks.OnSaveEditorState(editorState);
			}
			for (int i = 0; i < NodeEditorCallbacks.receiverCount; i++)
			{
				if (NodeEditorCallbacks.callbackReceiver[i] == null)
				{
					NodeEditorCallbacks.callbackReceiver.RemoveAt(i--);
				}
				else
				{
					NodeEditorCallbacks.callbackReceiver[i].OnSaveEditorState(editorState);
				}
			}
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x00063424 File Offset: 0x00061624
		public static void IssueOnAddNode(Node node)
		{
			if (NodeEditorCallbacks.OnAddNode != null)
			{
				NodeEditorCallbacks.OnAddNode(node);
			}
			for (int i = 0; i < NodeEditorCallbacks.receiverCount; i++)
			{
				if (NodeEditorCallbacks.callbackReceiver[i] == null)
				{
					NodeEditorCallbacks.callbackReceiver.RemoveAt(i--);
				}
				else
				{
					NodeEditorCallbacks.callbackReceiver[i].OnAddNode(node);
				}
			}
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x00063488 File Offset: 0x00061688
		public static void IssueOnDeleteNode(Node node)
		{
			if (NodeEditorCallbacks.OnDeleteNode != null)
			{
				NodeEditorCallbacks.OnDeleteNode(node);
			}
			for (int i = 0; i < NodeEditorCallbacks.receiverCount; i++)
			{
				if (NodeEditorCallbacks.callbackReceiver[i] == null)
				{
					NodeEditorCallbacks.callbackReceiver.RemoveAt(i--);
				}
				else
				{
					NodeEditorCallbacks.callbackReceiver[i].OnDeleteNode(node);
					node.OnDelete();
				}
			}
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000634F4 File Offset: 0x000616F4
		public static void IssueOnMoveNode(Node node)
		{
			if (NodeEditorCallbacks.OnMoveNode != null)
			{
				NodeEditorCallbacks.OnMoveNode(node);
			}
			for (int i = 0; i < NodeEditorCallbacks.receiverCount; i++)
			{
				if (NodeEditorCallbacks.callbackReceiver[i] == null)
				{
					NodeEditorCallbacks.callbackReceiver.RemoveAt(i--);
				}
				else
				{
					NodeEditorCallbacks.callbackReceiver[i].OnMoveNode(node);
				}
			}
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x00063558 File Offset: 0x00061758
		public static void IssueOnAddNodeKnob(NodeKnob nodeKnob)
		{
			if (NodeEditorCallbacks.OnAddNodeKnob != null)
			{
				NodeEditorCallbacks.OnAddNodeKnob(nodeKnob);
			}
			for (int i = 0; i < NodeEditorCallbacks.receiverCount; i++)
			{
				if (NodeEditorCallbacks.callbackReceiver[i] == null)
				{
					NodeEditorCallbacks.callbackReceiver.RemoveAt(i--);
				}
				else
				{
					NodeEditorCallbacks.callbackReceiver[i].OnAddNodeKnob(nodeKnob);
				}
			}
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x000635BC File Offset: 0x000617BC
		public static void IssueOnAddConnection(NodeInput input)
		{
			if (NodeEditorCallbacks.OnAddConnection != null)
			{
				NodeEditorCallbacks.OnAddConnection(input);
			}
			for (int i = 0; i < NodeEditorCallbacks.receiverCount; i++)
			{
				if (NodeEditorCallbacks.callbackReceiver[i] == null)
				{
					NodeEditorCallbacks.callbackReceiver.RemoveAt(i--);
				}
				else
				{
					NodeEditorCallbacks.callbackReceiver[i].OnAddConnection(input);
				}
			}
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x00063620 File Offset: 0x00061820
		public static void IssueOnRemoveConnection(NodeInput input)
		{
			if (NodeEditorCallbacks.OnRemoveConnection != null)
			{
				NodeEditorCallbacks.OnRemoveConnection(input);
			}
			for (int i = 0; i < NodeEditorCallbacks.receiverCount; i++)
			{
				if (NodeEditorCallbacks.callbackReceiver[i] == null)
				{
					NodeEditorCallbacks.callbackReceiver.RemoveAt(i--);
				}
				else
				{
					NodeEditorCallbacks.callbackReceiver[i].OnRemoveConnection(input);
				}
			}
		}

		// Token: 0x0400110D RID: 4365
		private static int receiverCount;

		// Token: 0x0400110E RID: 4366
		private static List<NodeEditorCallbackReceiver> callbackReceiver;

		// Token: 0x0400110F RID: 4367
		public static System.Action OnEditorStartUp;

		// Token: 0x04001110 RID: 4368
		public static Action<NodeCanvas> OnLoadCanvas;

		// Token: 0x04001111 RID: 4369
		public static Action<NodeEditorState> OnLoadEditorState;

		// Token: 0x04001112 RID: 4370
		public static Action<NodeCanvas> OnSaveCanvas;

		// Token: 0x04001113 RID: 4371
		public static Action<NodeEditorState> OnSaveEditorState;

		// Token: 0x04001114 RID: 4372
		public static Action<Node> OnAddNode;

		// Token: 0x04001115 RID: 4373
		public static Action<Node> OnDeleteNode;

		// Token: 0x04001116 RID: 4374
		public static Action<Node> OnMoveNode;

		// Token: 0x04001117 RID: 4375
		public static Action<NodeKnob> OnAddNodeKnob;

		// Token: 0x04001118 RID: 4376
		public static Action<NodeInput> OnAddConnection;

		// Token: 0x04001119 RID: 4377
		public static Action<NodeInput> OnRemoveConnection;
	}
}
