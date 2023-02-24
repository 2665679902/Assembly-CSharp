using System;
using System.Collections.Generic;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x0200047B RID: 1147
	public static class NodeEditor
	{
		// Token: 0x0600314C RID: 12620 RVA: 0x00062B17 File Offset: 0x00060D17
		public static void Update()
		{
			if (NodeEditor.NEUpdate != null)
			{
				NodeEditor.NEUpdate();
			}
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x00062B2A File Offset: 0x00060D2A
		public static void RepaintClients()
		{
			if (NodeEditor.ClientRepaints != null)
			{
				NodeEditor.ClientRepaints();
			}
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x00062B3D File Offset: 0x00060D3D
		public static void checkInit(bool GUIFunction)
		{
			if (!NodeEditor.initiated && !NodeEditor.InitiationError)
			{
				NodeEditor.ReInit(GUIFunction);
			}
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x00062B54 File Offset: 0x00060D54
		public static void ReInit(bool GUIFunction)
		{
			NodeEditor.CheckEditorPath();
			ResourceManager.SetDefaultResourcePath(NodeEditor.editorPath + "Resources/");
			if (!NodeEditorGUI.Init(GUIFunction))
			{
				NodeEditor.InitiationError = true;
				return;
			}
			ConnectionTypes.FetchTypes();
			NodeTypes.FetchNodes();
			NodeCanvasManager.GetAllCanvasTypes();
			NodeEditorCallbacks.SetupReceivers();
			NodeEditorCallbacks.IssueOnEditorStartUp();
			GUIScaleUtility.CheckInit();
			NodeEditorInputSystem.SetupInput();
			NodeEditor.initiated = GUIFunction;
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x00062BB2 File Offset: 0x00060DB2
		public static void CheckEditorPath()
		{
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x00062BB4 File Offset: 0x00060DB4
		public static void DrawCanvas(NodeCanvas nodeCanvas, NodeEditorState editorState)
		{
			if (!editorState.drawing)
			{
				return;
			}
			NodeEditor.checkInit(true);
			NodeEditor.DrawSubCanvas(nodeCanvas, editorState);
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x00062BCC File Offset: 0x00060DCC
		private static void DrawSubCanvas(NodeCanvas nodeCanvas, NodeEditorState editorState)
		{
			if (!editorState.drawing)
			{
				return;
			}
			NodeCanvas nodeCanvas2 = NodeEditor.curNodeCanvas;
			NodeEditorState nodeEditorState = NodeEditor.curEditorState;
			NodeEditor.curNodeCanvas = nodeCanvas;
			NodeEditor.curEditorState = editorState;
			if (Event.current.type == EventType.Repaint)
			{
				float num = NodeEditor.curEditorState.zoom / (float)NodeEditorGUI.Background.width;
				float num2 = NodeEditor.curEditorState.zoom / (float)NodeEditorGUI.Background.height;
				Vector2 vector = NodeEditor.curEditorState.zoomPos + NodeEditor.curEditorState.panOffset / NodeEditor.curEditorState.zoom;
				Rect rect = new Rect(-vector.x * num, (vector.y - NodeEditor.curEditorState.canvasRect.height) * num2, NodeEditor.curEditorState.canvasRect.width * num, NodeEditor.curEditorState.canvasRect.height * num2);
				GUI.DrawTextureWithTexCoords(NodeEditor.curEditorState.canvasRect, NodeEditorGUI.Background, rect);
			}
			NodeEditorInputSystem.HandleInputEvents(NodeEditor.curEditorState);
			if (Event.current.type != EventType.Layout)
			{
				NodeEditor.curEditorState.ignoreInput = new List<Rect>();
			}
			Rect canvasRect = NodeEditor.curEditorState.canvasRect;
			NodeEditor.curEditorState.zoomPanAdjust = GUIScaleUtility.BeginScale(ref canvasRect, NodeEditor.curEditorState.zoomPos, NodeEditor.curEditorState.zoom, false);
			if (NodeEditor.curEditorState.navigate)
			{
				Vector2 vector2 = ((NodeEditor.curEditorState.selectedNode != null) ? NodeEditor.curEditorState.selectedNode.rect.center : NodeEditor.curEditorState.panOffset) + NodeEditor.curEditorState.zoomPanAdjust;
				Vector2 mousePosition = Event.current.mousePosition;
				RTEditorGUI.DrawLine(vector2, mousePosition, Color.green, null, 3f);
				NodeEditor.RepaintClients();
			}
			if (NodeEditor.curEditorState.connectOutput != null)
			{
				NodeOutput connectOutput = NodeEditor.curEditorState.connectOutput;
				Vector2 center = connectOutput.GetGUIKnob().center;
				Vector2 direction = connectOutput.GetDirection();
				Vector2 mousePosition2 = Event.current.mousePosition;
				Vector2 secondConnectionVector = NodeEditorGUI.GetSecondConnectionVector(center, mousePosition2, direction);
				NodeEditorGUI.DrawConnection(center, direction, mousePosition2, secondConnectionVector, connectOutput.typeData.Color);
				NodeEditor.RepaintClients();
			}
			if (Event.current.type == EventType.Layout && NodeEditor.curEditorState.selectedNode != null)
			{
				NodeEditor.curNodeCanvas.nodes.Remove(NodeEditor.curEditorState.selectedNode);
				NodeEditor.curNodeCanvas.nodes.Add(NodeEditor.curEditorState.selectedNode);
			}
			for (int i = 0; i < NodeEditor.curNodeCanvas.nodes.Count; i++)
			{
				NodeEditor.curNodeCanvas.nodes[i].DrawConnections();
			}
			for (int j = 0; j < NodeEditor.curNodeCanvas.nodes.Count; j++)
			{
				Node node = NodeEditor.curNodeCanvas.nodes[j];
				node.DrawNode();
				if (Event.current.type == EventType.Repaint)
				{
					node.DrawKnobs();
				}
			}
			GUIScaleUtility.EndScale();
			NodeEditorInputSystem.HandleLateInputEvents(NodeEditor.curEditorState);
			NodeEditor.curNodeCanvas = nodeCanvas2;
			NodeEditor.curEditorState = nodeEditorState;
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x00062EE8 File Offset: 0x000610E8
		public static Node NodeAtPosition(Vector2 canvasPos)
		{
			NodeKnob nodeKnob;
			return NodeEditor.NodeAtPosition(NodeEditor.curEditorState, canvasPos, out nodeKnob);
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x00062F02 File Offset: 0x00061102
		public static Node NodeAtPosition(Vector2 canvasPos, out NodeKnob focusedKnob)
		{
			return NodeEditor.NodeAtPosition(NodeEditor.curEditorState, canvasPos, out focusedKnob);
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x00062F10 File Offset: 0x00061110
		public static Node NodeAtPosition(NodeEditorState editorState, Vector2 canvasPos, out NodeKnob focusedKnob)
		{
			focusedKnob = null;
			if (NodeEditorInputSystem.shouldIgnoreInput(editorState))
			{
				return null;
			}
			NodeCanvas canvas = editorState.canvas;
			for (int i = canvas.nodes.Count - 1; i >= 0; i--)
			{
				Node node = canvas.nodes[i];
				if (node.rect.Contains(canvasPos))
				{
					return node;
				}
				for (int j = 0; j < node.nodeKnobs.Count; j++)
				{
					if (node.nodeKnobs[j].GetCanvasSpaceKnob().Contains(canvasPos))
					{
						focusedKnob = node.nodeKnobs[j];
						return node;
					}
				}
			}
			return null;
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x00062FAA File Offset: 0x000611AA
		public static Vector2 ScreenToCanvasSpace(Vector2 screenPos)
		{
			return NodeEditor.ScreenToCanvasSpace(NodeEditor.curEditorState, screenPos);
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x00062FB7 File Offset: 0x000611B7
		public static Vector2 ScreenToCanvasSpace(NodeEditorState editorState, Vector2 screenPos)
		{
			return (screenPos - editorState.canvasRect.position - editorState.zoomPos) * editorState.zoom - editorState.panOffset;
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x00062FEC File Offset: 0x000611EC
		public static void RecalculateAll(NodeCanvas nodeCanvas)
		{
			NodeEditor.workList = new List<Node>();
			foreach (Node node in nodeCanvas.nodes)
			{
				if (node.isInput())
				{
					node.ClearCalculation();
					NodeEditor.workList.Add(node);
				}
			}
			NodeEditor.StartCalculation();
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x00063060 File Offset: 0x00061260
		public static void RecalculateFrom(Node node)
		{
			node.ClearCalculation();
			NodeEditor.workList = new List<Node> { node };
			NodeEditor.StartCalculation();
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x00063080 File Offset: 0x00061280
		public static void StartCalculation()
		{
			NodeEditor.checkInit(false);
			if (NodeEditor.InitiationError)
			{
				return;
			}
			if (NodeEditor.workList == null || NodeEditor.workList.Count == 0)
			{
				return;
			}
			NodeEditor.calculationCount = 0;
			bool flag = false;
			int num = 0;
			while (!flag)
			{
				flag = true;
				for (int i = 0; i < NodeEditor.workList.Count; i++)
				{
					if (NodeEditor.ContinueCalculation(NodeEditor.workList[i]))
					{
						flag = false;
					}
				}
				num++;
			}
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000630F0 File Offset: 0x000612F0
		private static bool ContinueCalculation(Node node)
		{
			if (node.calculated)
			{
				return false;
			}
			if ((node.descendantsCalculated() || node.isInLoop()) && node.Calculate())
			{
				node.calculated = true;
				NodeEditor.calculationCount++;
				NodeEditor.workList.Remove(node);
				if (node.ContinueCalculation && NodeEditor.calculationCount < 1000)
				{
					for (int i = 0; i < node.Outputs.Count; i++)
					{
						NodeOutput nodeOutput = node.Outputs[i];
						if (!nodeOutput.calculationBlockade)
						{
							for (int j = 0; j < nodeOutput.connections.Count; j++)
							{
								NodeEditor.ContinueCalculation(nodeOutput.connections[j].body);
							}
						}
					}
				}
				else if (NodeEditor.calculationCount >= 1000)
				{
					global::Debug.LogError("Stopped calculation because of suspected Recursion. Maximum calculation iteration is currently at 1000!");
				}
				return true;
			}
			if (!NodeEditor.workList.Contains(node))
			{
				NodeEditor.workList.Add(node);
			}
			return false;
		}

		// Token: 0x04001104 RID: 4356
		public static string editorPath = "Assets/Plugins/Node_Editor/";

		// Token: 0x04001105 RID: 4357
		public static NodeCanvas curNodeCanvas;

		// Token: 0x04001106 RID: 4358
		public static NodeEditorState curEditorState;

		// Token: 0x04001107 RID: 4359
		internal static System.Action NEUpdate;

		// Token: 0x04001108 RID: 4360
		public static System.Action ClientRepaints;

		// Token: 0x04001109 RID: 4361
		public static bool initiated;

		// Token: 0x0400110A RID: 4362
		public static bool InitiationError;

		// Token: 0x0400110B RID: 4363
		public static List<Node> workList;

		// Token: 0x0400110C RID: 4364
		private static int calculationCount;
	}
}
