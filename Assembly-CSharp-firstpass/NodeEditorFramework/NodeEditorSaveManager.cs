﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x02000487 RID: 1159
	public static class NodeEditorSaveManager
	{
		// Token: 0x060031BE RID: 12734 RVA: 0x00064E6C File Offset: 0x0006306C
		private static void FetchSceneSaveHolder()
		{
			if (NodeEditorSaveManager.sceneSaveHolder == null)
			{
				NodeEditorSaveManager.sceneSaveHolder = GameObject.Find("NodeEditor_SceneSaveHolder");
				if (NodeEditorSaveManager.sceneSaveHolder == null)
				{
					NodeEditorSaveManager.sceneSaveHolder = new GameObject("NodeEditor_SceneSaveHolder");
				}
				NodeEditorSaveManager.sceneSaveHolder.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
			}
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x00064EBC File Offset: 0x000630BC
		public static string[] GetSceneSaves()
		{
			NodeEditorSaveManager.FetchSceneSaveHolder();
			return (from save in NodeEditorSaveManager.sceneSaveHolder.GetComponents<NodeCanvasSceneSave>()
				select save.savedNodeCanvas.name).ToArray<string>();
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x00064EF8 File Offset: 0x000630F8
		private static NodeCanvasSceneSave FindSceneSave(string saveName)
		{
			NodeEditorSaveManager.FetchSceneSaveHolder();
			return NodeEditorSaveManager.sceneSaveHolder.GetComponents<NodeCanvasSceneSave>().ToList<NodeCanvasSceneSave>().Find((NodeCanvasSceneSave save) => save.savedNodeCanvas.name == saveName);
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x00064F38 File Offset: 0x00063138
		public static void SaveSceneNodeCanvas(string saveName, ref NodeCanvas nodeCanvas, bool createWorkingCopy)
		{
			if (string.IsNullOrEmpty(saveName))
			{
				global::Debug.LogError("Cannot save Canvas to scene: No save name specified!");
				return;
			}
			nodeCanvas.livesInScene = true;
			nodeCanvas.name = saveName;
			NodeCanvasSceneSave nodeCanvasSceneSave = NodeEditorSaveManager.FindSceneSave(saveName);
			if (nodeCanvasSceneSave == null)
			{
				nodeCanvasSceneSave = NodeEditorSaveManager.sceneSaveHolder.AddComponent<NodeCanvasSceneSave>();
			}
			nodeCanvasSceneSave.savedNodeCanvas = nodeCanvas;
			if (createWorkingCopy)
			{
				nodeCanvasSceneSave.savedNodeCanvas = NodeEditorSaveManager.CreateWorkingCopy(nodeCanvasSceneSave.savedNodeCanvas, true);
				NodeEditorSaveManager.Compress(ref nodeCanvasSceneSave.savedNodeCanvas);
			}
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x00064FAC File Offset: 0x000631AC
		public static NodeCanvas LoadSceneNodeCanvas(string saveName, bool createWorkingCopy)
		{
			if (string.IsNullOrEmpty(saveName))
			{
				global::Debug.LogError("Cannot load Canvas from scene: No save name specified!");
				return null;
			}
			NodeCanvasSceneSave nodeCanvasSceneSave = NodeEditorSaveManager.FindSceneSave(saveName);
			if (nodeCanvasSceneSave == null)
			{
				return null;
			}
			NodeCanvas nodeCanvas = nodeCanvasSceneSave.savedNodeCanvas;
			nodeCanvas.livesInScene = true;
			if (createWorkingCopy)
			{
				nodeCanvas = NodeEditorSaveManager.CreateWorkingCopy(nodeCanvas, true);
			}
			NodeEditorSaveManager.Uncompress(ref nodeCanvas);
			return nodeCanvas;
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x00065000 File Offset: 0x00063200
		public static void SaveNodeCanvas(string path, NodeCanvas nodeCanvas, bool createWorkingCopy)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x00065008 File Offset: 0x00063208
		public static NodeCanvas LoadNodeCanvas(string path, bool createWorkingCopy)
		{
			if (!File.Exists(path))
			{
				throw new UnityException("Cannot Load NodeCanvas: File '" + path + "' deos not exist!");
			}
			NodeCanvas nodeCanvas = ResourceManager.LoadResource<NodeCanvas>(path);
			if (nodeCanvas == null)
			{
				throw new UnityException("Cannot Load NodeCanvas: The file at the specified path '" + path + "' is no valid save file as it does not contain a NodeCanvas!");
			}
			if (createWorkingCopy)
			{
				nodeCanvas = NodeEditorSaveManager.CreateWorkingCopy(nodeCanvas, true);
			}
			else
			{
				nodeCanvas.Validate();
			}
			NodeEditorSaveManager.Uncompress(ref nodeCanvas);
			NodeEditorCallbacks.IssueOnLoadCanvas(nodeCanvas);
			return nodeCanvas;
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x0006507A File Offset: 0x0006327A
		public static void Compress(ref NodeCanvas nodeCanvas)
		{
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x0006507C File Offset: 0x0006327C
		public static void Uncompress(ref NodeCanvas nodeCanvas)
		{
			for (int i = 0; i < nodeCanvas.nodes.Count; i++)
			{
				Node node = nodeCanvas.nodes[i];
				if (node.Inputs == null || node.Inputs.Count == 0 || node.Outputs == null || node.Outputs.Count == 0)
				{
					node.Inputs = new List<NodeInput>();
					node.Outputs = new List<NodeOutput>();
					for (int j = 0; j < node.nodeKnobs.Count; j++)
					{
						NodeKnob nodeKnob = node.nodeKnobs[j];
						if (nodeKnob is NodeInput)
						{
							node.Inputs.Add(nodeKnob as NodeInput);
						}
						else if (nodeKnob is NodeOutput)
						{
							node.Outputs.Add(nodeKnob as NodeOutput);
						}
					}
				}
			}
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x0006514C File Offset: 0x0006334C
		public static NodeCanvas CreateWorkingCopy(NodeCanvas nodeCanvas, bool editorStates)
		{
			nodeCanvas.Validate();
			nodeCanvas = NodeEditorSaveManager.Clone<NodeCanvas>(nodeCanvas);
			List<ScriptableObject> allSOs = new List<ScriptableObject>();
			List<ScriptableObject> clonedSOs = new List<ScriptableObject>();
			for (int i = 0; i < nodeCanvas.nodes.Count; i++)
			{
				Node node = nodeCanvas.nodes[i];
				node.CheckNodeKnobMigration();
				Node node2 = NodeEditorSaveManager.AddClonedSO<Node>(allSOs, clonedSOs, node);
				NodeEditorSaveManager.AddClonedSOs(allSOs, clonedSOs, node2.GetScriptableObjects());
				foreach (NodeKnob nodeKnob in node2.nodeKnobs)
				{
					NodeEditorSaveManager.AddClonedSO<NodeKnob>(allSOs, clonedSOs, nodeKnob);
					NodeEditorSaveManager.AddClonedSOs(allSOs, clonedSOs, nodeKnob.GetScriptableObjects());
				}
			}
			Func<ScriptableObject, ScriptableObject> <>9__0;
			Func<ScriptableObject, ScriptableObject> <>9__1;
			for (int j = 0; j < nodeCanvas.nodes.Count; j++)
			{
				Node node3 = nodeCanvas.nodes[j];
				Node node4 = (nodeCanvas.nodes[j] = NodeEditorSaveManager.ReplaceSO<Node>(allSOs, clonedSOs, node3));
				Node node5 = node4;
				Func<ScriptableObject, ScriptableObject> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (ScriptableObject so) => NodeEditorSaveManager.ReplaceSO<ScriptableObject>(allSOs, clonedSOs, so));
				}
				node5.CopyScriptableObjects(func);
				for (int k = 0; k < node4.nodeKnobs.Count; k++)
				{
					NodeKnob nodeKnob2 = (node4.nodeKnobs[k] = NodeEditorSaveManager.ReplaceSO<NodeKnob>(allSOs, clonedSOs, node4.nodeKnobs[k]));
					nodeKnob2.body = node4;
					Func<ScriptableObject, ScriptableObject> func2;
					if ((func2 = <>9__1) == null)
					{
						func2 = (<>9__1 = (ScriptableObject so) => NodeEditorSaveManager.ReplaceSO<ScriptableObject>(allSOs, clonedSOs, so));
					}
					nodeKnob2.CopyScriptableObjects(func2);
				}
				for (int l = 0; l < node4.Inputs.Count; l++)
				{
					(node4.Inputs[l] = NodeEditorSaveManager.ReplaceSO<NodeInput>(allSOs, clonedSOs, node4.Inputs[l])).body = node4;
				}
				for (int m = 0; m < node4.Outputs.Count; m++)
				{
					(node4.Outputs[m] = NodeEditorSaveManager.ReplaceSO<NodeOutput>(allSOs, clonedSOs, node4.Outputs[m])).body = node4;
				}
			}
			if (editorStates)
			{
				nodeCanvas.editorStates = NodeEditorSaveManager.CreateWorkingCopy(nodeCanvas.editorStates, nodeCanvas);
				foreach (NodeEditorState nodeEditorState in nodeCanvas.editorStates)
				{
					nodeEditorState.selectedNode = NodeEditorSaveManager.ReplaceSO<Node>(allSOs, clonedSOs, nodeEditorState.selectedNode);
				}
			}
			else
			{
				NodeEditorState[] array = nodeCanvas.editorStates;
				for (int n = 0; n < array.Length; n++)
				{
					array[n].selectedNode = null;
				}
			}
			return nodeCanvas;
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x0006546C File Offset: 0x0006366C
		private static NodeEditorState[] CreateWorkingCopy(NodeEditorState[] editorStates, NodeCanvas associatedNodeCanvas)
		{
			if (editorStates == null)
			{
				return new NodeEditorState[0];
			}
			editorStates = (NodeEditorState[])editorStates.Clone();
			for (int i = 0; i < editorStates.Length; i++)
			{
				if (!(editorStates[i] == null))
				{
					NodeEditorState nodeEditorState = (editorStates[i] = NodeEditorSaveManager.Clone<NodeEditorState>(editorStates[i]));
					if (nodeEditorState == null)
					{
						global::Debug.LogError("Failed to create a working copy for an NodeEditorState during the loading process of " + associatedNodeCanvas.name + "!");
					}
					else
					{
						nodeEditorState.canvas = associatedNodeCanvas;
					}
				}
			}
			associatedNodeCanvas.editorStates = editorStates;
			return editorStates;
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000654EC File Offset: 0x000636EC
		private static T Clone<T>(T SO) where T : ScriptableObject
		{
			string name = SO.name;
			SO = UnityEngine.Object.Instantiate<T>(SO);
			SO.name = name;
			return SO;
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x0006551A File Offset: 0x0006371A
		private static void AddClonedSOs(List<ScriptableObject> scriptableObjects, List<ScriptableObject> clonedScriptableObjects, ScriptableObject[] initialSOs)
		{
			scriptableObjects.AddRange(initialSOs);
			clonedScriptableObjects.AddRange(initialSOs.Select((ScriptableObject so) => NodeEditorSaveManager.Clone<ScriptableObject>(so)));
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x00065550 File Offset: 0x00063750
		private static T AddClonedSO<T>(List<ScriptableObject> scriptableObjects, List<ScriptableObject> clonedScriptableObjects, T initialSO) where T : ScriptableObject
		{
			if (initialSO == null)
			{
				return default(T);
			}
			scriptableObjects.Add(initialSO);
			T t = NodeEditorSaveManager.Clone<T>(initialSO);
			clonedScriptableObjects.Add(t);
			return t;
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x00065598 File Offset: 0x00063798
		private static T ReplaceSO<T>(List<ScriptableObject> scriptableObjects, List<ScriptableObject> clonedScriptableObjects, T initialSO) where T : ScriptableObject
		{
			if (initialSO == null)
			{
				return default(T);
			}
			int num = scriptableObjects.IndexOf(initialSO);
			if (num == -1)
			{
				global::Debug.LogError("GetWorkingCopy: ScriptableObject " + initialSO.name + " was not copied before! It will be null!");
			}
			if (num != -1)
			{
				return (T)((object)clonedScriptableObjects[num]);
			}
			return default(T);
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x00065608 File Offset: 0x00063808
		public static NodeEditorState ExtractEditorState(NodeCanvas canvas, string stateName)
		{
			NodeEditorState nodeEditorState = null;
			if (canvas.editorStates.Length != 0)
			{
				nodeEditorState = canvas.editorStates.First((NodeEditorState s) => s.name == stateName);
				if (nodeEditorState == null)
				{
					nodeEditorState = canvas.editorStates[0];
				}
			}
			if (nodeEditorState == null)
			{
				nodeEditorState = ScriptableObject.CreateInstance<NodeEditorState>();
				nodeEditorState.canvas = canvas;
				canvas.editorStates = new NodeEditorState[] { nodeEditorState };
			}
			nodeEditorState.name = stateName;
			return nodeEditorState;
		}

		// Token: 0x0400113F RID: 4415
		private static GameObject sceneSaveHolder;
	}
}
