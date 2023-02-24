using System;
using System.IO;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace NodeEditorFramework.Standard
{
	// Token: 0x02000497 RID: 1175
	public class RuntimeNodeEditor : MonoBehaviour
	{
		// Token: 0x060032AF RID: 12975 RVA: 0x000691D8 File Offset: 0x000673D8
		public void Start()
		{
			NodeEditor.checkInit(false);
			NodeEditor.initiated = false;
			this.LoadNodeCanvas(this.canvasPath);
			FPSCounter.Create();
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000691F7 File Offset: 0x000673F7
		public void Update()
		{
			NodeEditor.Update();
			FPSCounter.Update();
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x00069204 File Offset: 0x00067404
		public void OnGUI()
		{
			if (this.canvas != null)
			{
				if (this.state == null)
				{
					this.NewEditorState();
				}
				NodeEditor.checkInit(true);
				if (NodeEditor.InitiationError)
				{
					GUILayout.Label("Initiation failed! Check console for more information!", Array.Empty<GUILayoutOption>());
					return;
				}
				try
				{
					if (!this.screenSize && this.specifiedRootRect.max != this.specifiedRootRect.min)
					{
						GUI.BeginGroup(this.specifiedRootRect, NodeEditorGUI.nodeSkin.box);
					}
					NodeEditorGUI.StartNodeGUI();
					this.canvasRect = (this.screenSize ? new Rect(0f, 0f, (float)Screen.width, (float)Screen.height) : this.specifiedCanvasRect);
					this.canvasRect.width = this.canvasRect.width - 200f;
					this.state.canvasRect = this.canvasRect;
					NodeEditor.DrawCanvas(this.canvas, this.state);
					GUILayout.BeginArea(new Rect(this.canvasRect.x + this.state.canvasRect.width, this.state.canvasRect.y, 200f, this.state.canvasRect.height), NodeEditorGUI.nodeSkin.box);
					this.SideGUI();
					GUILayout.EndArea();
					NodeEditorGUI.EndNodeGUI();
					if (!this.screenSize && this.specifiedRootRect.max != this.specifiedRootRect.min)
					{
						GUI.EndGroup();
					}
				}
				catch (UnityException ex)
				{
					this.NewNodeCanvas();
					NodeEditor.ReInit(true);
					global::Debug.LogError("Unloaded Canvas due to exception in Draw!");
					global::Debug.LogException(ex);
				}
			}
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000693C8 File Offset: 0x000675C8
		public void SideGUI()
		{
			GUILayout.Label(new GUIContent("Node Editor (" + this.canvas.name + ")", "The currently opened canvas in the Node Editor"), Array.Empty<GUILayoutOption>());
			this.screenSize = GUILayout.Toggle(this.screenSize, "Adapt to Screen", Array.Empty<GUILayoutOption>());
			GUILayout.Label("FPS: " + FPSCounter.currentFPS.ToString(), Array.Empty<GUILayoutOption>());
			GUILayout.Label(new GUIContent("Node Editor (" + this.canvas.name + ")"), NodeEditorGUI.nodeLabelBold, Array.Empty<GUILayoutOption>());
			if (GUILayout.Button(new GUIContent("New Canvas", "Loads an empty Canvas"), Array.Empty<GUILayoutOption>()))
			{
				this.NewNodeCanvas();
			}
			GUILayout.Space(6f);
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			this.sceneCanvasName = GUILayout.TextField(this.sceneCanvasName, new GUILayoutOption[] { GUILayout.ExpandWidth(true) });
			if (GUILayout.Button(new GUIContent("Save to Scene", "Saves the Canvas to the Scene"), new GUILayoutOption[] { GUILayout.ExpandWidth(false) }))
			{
				this.SaveSceneNodeCanvas(this.sceneCanvasName);
			}
			GUILayout.EndHorizontal();
			if (GUILayout.Button(new GUIContent("Load from Scene", "Loads the Canvas from the Scene"), Array.Empty<GUILayoutOption>()))
			{
				GenericMenu genericMenu = new GenericMenu();
				foreach (string text in NodeEditorSaveManager.GetSceneSaves())
				{
					genericMenu.AddItem(new GUIContent(text), false, new PopupMenu.MenuFunctionData(this.LoadSceneCanvasCallback), text);
				}
				genericMenu.Show(this.loadScenePos, 40f);
			}
			if (Event.current.type == EventType.Repaint)
			{
				Rect lastRect = GUILayoutUtility.GetLastRect();
				this.loadScenePos = new Vector2(lastRect.x + 2f, lastRect.yMax + 2f);
			}
			GUILayout.Space(6f);
			if (GUILayout.Button(new GUIContent("Recalculate All", "Initiates complete recalculate. Usually does not need to be triggered manually."), Array.Empty<GUILayoutOption>()))
			{
				NodeEditor.RecalculateAll(this.canvas);
			}
			if (GUILayout.Button("Force Re-Init", Array.Empty<GUILayoutOption>()))
			{
				NodeEditor.ReInit(true);
			}
			NodeEditorGUI.knobSize = RTEditorGUI.IntSlider(new GUIContent("Handle Size", "The size of the Node Input/Output handles"), NodeEditorGUI.knobSize, 12, 20, Array.Empty<GUILayoutOption>());
			this.state.zoom = RTEditorGUI.Slider(new GUIContent("Zoom", "Use the Mousewheel. Seriously."), this.state.zoom, 0.6f, 2f, Array.Empty<GUILayoutOption>());
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x00069639 File Offset: 0x00067839
		private void LoadSceneCanvasCallback(object save)
		{
			this.LoadSceneNodeCanvas((string)save);
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x00069647 File Offset: 0x00067847
		public void SaveSceneNodeCanvas(string path)
		{
			this.canvas.editorStates = new NodeEditorState[] { this.state };
			NodeEditorSaveManager.SaveSceneNodeCanvas(path, ref this.canvas, true);
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x00069670 File Offset: 0x00067870
		public void LoadSceneNodeCanvas(string path)
		{
			if ((this.canvas = NodeEditorSaveManager.LoadSceneNodeCanvas(path, true)) == null)
			{
				this.NewNodeCanvas();
				return;
			}
			this.state = NodeEditorSaveManager.ExtractEditorState(this.canvas, "MainEditorState");
			NodeEditor.RecalculateAll(this.canvas);
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000696C0 File Offset: 0x000678C0
		public void LoadNodeCanvas(string path)
		{
			if (!File.Exists(path) || (this.canvas = NodeEditorSaveManager.LoadNodeCanvas(path, true)) == null)
			{
				this.NewNodeCanvas();
				return;
			}
			this.state = NodeEditorSaveManager.ExtractEditorState(this.canvas, "MainEditorState");
			NodeEditor.RecalculateAll(this.canvas);
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x00069715 File Offset: 0x00067915
		public void NewNodeCanvas()
		{
			this.canvas = ScriptableObject.CreateInstance<NodeCanvas>();
			this.canvas.name = "New Canvas";
			this.NewEditorState();
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x00069738 File Offset: 0x00067938
		private void NewEditorState()
		{
			this.state = ScriptableObject.CreateInstance<NodeEditorState>();
			this.state.canvas = this.canvas;
			this.state.name = "MainEditorState";
			this.canvas.editorStates = new NodeEditorState[] { this.state };
		}

		// Token: 0x04001190 RID: 4496
		public string canvasPath;

		// Token: 0x04001191 RID: 4497
		public NodeCanvas canvas;

		// Token: 0x04001192 RID: 4498
		private NodeEditorState state;

		// Token: 0x04001193 RID: 4499
		public bool screenSize;

		// Token: 0x04001194 RID: 4500
		private Rect canvasRect;

		// Token: 0x04001195 RID: 4501
		public Rect specifiedRootRect;

		// Token: 0x04001196 RID: 4502
		public Rect specifiedCanvasRect;

		// Token: 0x04001197 RID: 4503
		private string sceneCanvasName = "";

		// Token: 0x04001198 RID: 4504
		private Vector2 loadScenePos;
	}
}
