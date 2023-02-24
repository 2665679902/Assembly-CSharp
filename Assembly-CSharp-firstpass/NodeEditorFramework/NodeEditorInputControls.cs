using System;
using System.Collections.Generic;
using System.Linq;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x0200047F RID: 1151
	public static class NodeEditorInputControls
	{
		// Token: 0x0600317E RID: 12670 RVA: 0x00063A10 File Offset: 0x00061C10
		[ContextFiller(ContextType.Canvas)]
		private static void FillAddNodes(NodeEditorInputInfo inputInfo, GenericMenu canvasContextMenu)
		{
			NodeEditorState editorState = inputInfo.editorState;
			List<Node> list = ((editorState.connectOutput != null) ? NodeTypes.getCompatibleNodes(editorState.connectOutput) : NodeTypes.nodes.Keys.ToList<Node>());
			NodeEditorInputControls.DeCafList(ref list, editorState.canvas);
			foreach (Node node in list)
			{
				canvasContextMenu.AddItem(new GUIContent("Add " + NodeTypes.nodes[node].adress), false, new PopupMenu.MenuFunctionData(NodeEditorInputControls.CreateNodeCallback), new NodeEditorInputInfo(node.GetID, editorState));
			}
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x00063AD4 File Offset: 0x00061CD4
		private static void DeCafList(ref List<Node> displayedNodes, NodeCanvas canvas)
		{
			for (int i = 0; i < displayedNodes.Count; i++)
			{
				if (!NodeTypes.nodes[displayedNodes[i]].typeOfNodeCanvas.Contains(canvas.GetType()))
				{
					displayedNodes.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x00063B24 File Offset: 0x00061D24
		private static void CreateNodeCallback(object infoObj)
		{
			NodeEditorInputInfo nodeEditorInputInfo = infoObj as NodeEditorInputInfo;
			if (nodeEditorInputInfo == null)
			{
				throw new UnityException("Callback Object passed by context is not of type NodeEditorInputInfo!");
			}
			nodeEditorInputInfo.SetAsCurrentEnvironment();
			Node.Create(nodeEditorInputInfo.message, NodeEditor.ScreenToCanvasSpace(nodeEditorInputInfo.inputPos), nodeEditorInputInfo.editorState.connectOutput);
			nodeEditorInputInfo.editorState.connectOutput = null;
			NodeEditor.RepaintClients();
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x00063B7F File Offset: 0x00061D7F
		[ContextEntry(ContextType.Node, "Delete Node")]
		private static void DeleteNode(NodeEditorInputInfo inputInfo)
		{
			inputInfo.SetAsCurrentEnvironment();
			if (inputInfo.editorState.focusedNode != null)
			{
				inputInfo.editorState.focusedNode.Delete();
				inputInfo.inputEvent.Use();
			}
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x00063BB8 File Offset: 0x00061DB8
		[ContextEntry(ContextType.Node, "Duplicate Node")]
		private static void DuplicateNode(NodeEditorInputInfo inputInfo)
		{
			inputInfo.SetAsCurrentEnvironment();
			NodeEditorState editorState = inputInfo.editorState;
			if (editorState.focusedNode != null)
			{
				Node node = Node.Create(editorState.focusedNode.GetID, NodeEditor.ScreenToCanvasSpace(inputInfo.inputPos), editorState.connectOutput);
				editorState.selectedNode = (editorState.focusedNode = node);
				editorState.connectOutput = null;
				inputInfo.inputEvent.Use();
			}
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x00063C24 File Offset: 0x00061E24
		[Hotkey(KeyCode.UpArrow, EventType.KeyDown)]
		[Hotkey(KeyCode.LeftArrow, EventType.KeyDown)]
		[Hotkey(KeyCode.RightArrow, EventType.KeyDown)]
		[Hotkey(KeyCode.DownArrow, EventType.KeyDown)]
		private static void KB_MoveNode(NodeEditorInputInfo inputInfo)
		{
			NodeEditorState editorState = inputInfo.editorState;
			if (editorState.selectedNode != null)
			{
				Vector2 position = editorState.selectedNode.rect.position;
				int num;
				if (inputInfo.inputEvent.shift)
				{
					num = 10;
				}
				else
				{
					num = 5;
				}
				if (inputInfo.inputEvent.keyCode == KeyCode.RightArrow)
				{
					position = new Vector2(position.x + (float)num, position.y);
				}
				else if (inputInfo.inputEvent.keyCode == KeyCode.LeftArrow)
				{
					position = new Vector2(position.x - (float)num, position.y);
				}
				else if (inputInfo.inputEvent.keyCode == KeyCode.DownArrow)
				{
					position = new Vector2(position.x, position.y + (float)num);
				}
				else if (inputInfo.inputEvent.keyCode == KeyCode.UpArrow)
				{
					position = new Vector2(position.x, position.y - (float)num);
				}
				editorState.selectedNode.rect.position = position;
				inputInfo.inputEvent.Use();
			}
			NodeEditor.RepaintClients();
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x00063D38 File Offset: 0x00061F38
		[EventHandler(EventType.MouseDown, 110)]
		private static void HandleNodeDraggingStart(NodeEditorInputInfo inputInfo)
		{
			if (GUIUtility.hotControl > 0)
			{
				return;
			}
			NodeEditorState editorState = inputInfo.editorState;
			if (inputInfo.inputEvent.button == 0 && editorState.focusedNode != null && editorState.focusedNode == editorState.selectedNode && editorState.focusedNodeKnob == null)
			{
				editorState.dragNode = true;
				editorState.dragStart = inputInfo.inputPos;
				editorState.dragPos = editorState.focusedNode.rect.position;
				editorState.dragOffset = Vector2.zero;
				inputInfo.inputEvent.delta = Vector2.zero;
			}
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x00063DD8 File Offset: 0x00061FD8
		[EventHandler(EventType.MouseDrag)]
		private static void HandleNodeDragging(NodeEditorInputInfo inputInfo)
		{
			NodeEditorState editorState = inputInfo.editorState;
			if (editorState.dragNode)
			{
				if (editorState.selectedNode != null && GUIUtility.hotControl == 0)
				{
					editorState.dragOffset = inputInfo.inputPos - editorState.dragStart;
					editorState.selectedNode.rect.position = editorState.dragPos + editorState.dragOffset * editorState.zoom;
					NodeEditorCallbacks.IssueOnMoveNode(editorState.selectedNode);
					NodeEditor.RepaintClients();
					return;
				}
				editorState.dragNode = false;
			}
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x00063E64 File Offset: 0x00062064
		[EventHandler(EventType.MouseDown)]
		[EventHandler(EventType.MouseUp)]
		private static void HandleNodeDraggingEnd(NodeEditorInputInfo inputInfo)
		{
			inputInfo.editorState.dragNode = false;
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x00063E74 File Offset: 0x00062074
		[EventHandler(EventType.MouseDown, 100)]
		private static void HandleWindowPanningStart(NodeEditorInputInfo inputInfo)
		{
			if (GUIUtility.hotControl > 0)
			{
				return;
			}
			NodeEditorState editorState = inputInfo.editorState;
			if ((inputInfo.inputEvent.button == 0 || inputInfo.inputEvent.button == 2) && editorState.focusedNode == null)
			{
				editorState.panWindow = true;
				editorState.dragStart = inputInfo.inputPos;
				editorState.dragOffset = Vector2.zero;
			}
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x00063ED8 File Offset: 0x000620D8
		[EventHandler(EventType.MouseDrag)]
		private static void HandleWindowPanning(NodeEditorInputInfo inputInfo)
		{
			NodeEditorState editorState = inputInfo.editorState;
			if (editorState.panWindow)
			{
				Vector2 vector = editorState.dragOffset;
				editorState.dragOffset = inputInfo.inputPos - editorState.dragStart;
				vector = (editorState.dragOffset - vector) * editorState.zoom;
				editorState.panOffset += vector;
				NodeEditor.RepaintClients();
			}
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x00063F41 File Offset: 0x00062141
		[EventHandler(EventType.MouseDown)]
		[EventHandler(EventType.MouseUp)]
		private static void HandleWindowPanningEnd(NodeEditorInputInfo inputInfo)
		{
			inputInfo.editorState.panWindow = false;
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x00063F50 File Offset: 0x00062150
		[EventHandler(EventType.MouseDown)]
		private static void HandleConnectionDrawing(NodeEditorInputInfo inputInfo)
		{
			NodeEditorState editorState = inputInfo.editorState;
			if (inputInfo.inputEvent.button == 0 && editorState.focusedNodeKnob != null)
			{
				if (editorState.focusedNodeKnob is NodeOutput)
				{
					editorState.connectOutput = (NodeOutput)editorState.focusedNodeKnob;
					inputInfo.inputEvent.Use();
					return;
				}
				if (editorState.focusedNodeKnob is NodeInput)
				{
					NodeInput nodeInput = (NodeInput)editorState.focusedNodeKnob;
					if (nodeInput.connection != null)
					{
						editorState.connectOutput = nodeInput.connection;
						nodeInput.RemoveConnection();
						inputInfo.inputEvent.Use();
					}
				}
			}
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x00063FF0 File Offset: 0x000621F0
		[EventHandler(EventType.MouseUp)]
		private static void HandleApplyConnection(NodeEditorInputInfo inputInfo)
		{
			NodeEditorState editorState = inputInfo.editorState;
			if (inputInfo.inputEvent.button == 0 && editorState.connectOutput != null && editorState.focusedNode != null && editorState.focusedNodeKnob != null && editorState.focusedNodeKnob is NodeInput)
			{
				(editorState.focusedNodeKnob as NodeInput).TryApplyConnection(editorState.connectOutput);
				inputInfo.inputEvent.Use();
			}
			editorState.connectOutput = null;
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x00064074 File Offset: 0x00062274
		[EventHandler(EventType.ScrollWheel)]
		private static void HandleZooming(NodeEditorInputInfo inputInfo)
		{
			inputInfo.editorState.zoom = (float)Math.Round(Math.Min(2.0, Math.Max(0.6, (double)(inputInfo.editorState.zoom + inputInfo.inputEvent.delta.y / 15f))), 2);
			NodeEditor.RepaintClients();
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000640D7 File Offset: 0x000622D7
		[Hotkey(KeyCode.N, EventType.KeyDown)]
		private static void HandleStartNavigating(NodeEditorInputInfo inputInfo)
		{
			inputInfo.editorState.navigate = true;
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x000640E5 File Offset: 0x000622E5
		[Hotkey(KeyCode.N, EventType.KeyUp)]
		private static void HandleEndNavigating(NodeEditorInputInfo inputInfo)
		{
			inputInfo.editorState.navigate = false;
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x000640F4 File Offset: 0x000622F4
		[Hotkey(KeyCode.LeftControl, EventType.KeyDown, 60)]
		[Hotkey(KeyCode.LeftControl, EventType.KeyUp, 60)]
		private static void HandleNodeSnap(NodeEditorInputInfo inputInfo)
		{
			NodeEditorState editorState = inputInfo.editorState;
			if (editorState.selectedNode != null)
			{
				Vector2 position = editorState.selectedNode.rect.position;
				position = new Vector2((float)(Mathf.RoundToInt(position.x / 10f) * 10), (float)(Mathf.RoundToInt(position.y / 10f) * 10));
				editorState.selectedNode.rect.position = position;
				inputInfo.inputEvent.Use();
			}
			NodeEditor.RepaintClients();
		}
	}
}
