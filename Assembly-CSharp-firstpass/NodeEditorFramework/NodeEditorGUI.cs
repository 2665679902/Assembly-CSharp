using System;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x0200047E RID: 1150
	public static class NodeEditorGUI
	{
		// Token: 0x06003176 RID: 12662 RVA: 0x00063688 File Offset: 0x00061888
		public static bool Init(bool GUIFunction)
		{
			NodeEditorGUI.Background = ResourceManager.LoadTexture("Textures/background.png");
			NodeEditorGUI.AALineTex = ResourceManager.LoadTexture("Textures/AALine.png");
			NodeEditorGUI.GUIBox = ResourceManager.LoadTexture("Textures/NE_Box.png");
			NodeEditorGUI.GUIButton = ResourceManager.LoadTexture("Textures/NE_Button.png");
			NodeEditorGUI.GUIBoxSelection = ResourceManager.LoadTexture("Textures/BoxSelection.png");
			if (!NodeEditorGUI.Background || !NodeEditorGUI.AALineTex || !NodeEditorGUI.GUIBox || !NodeEditorGUI.GUIButton)
			{
				return false;
			}
			if (!GUIFunction)
			{
				return true;
			}
			NodeEditorGUI.nodeSkin = UnityEngine.Object.Instantiate<GUISkin>(GUI.skin);
			NodeEditorGUI.nodeSkin.label.normal.textColor = NodeEditorGUI.NE_TextColor;
			NodeEditorGUI.nodeLabel = NodeEditorGUI.nodeSkin.label;
			NodeEditorGUI.nodeSkin.box.normal.textColor = NodeEditorGUI.NE_TextColor;
			NodeEditorGUI.nodeSkin.box.normal.background = NodeEditorGUI.GUIBox;
			NodeEditorGUI.nodeBox = NodeEditorGUI.nodeSkin.box;
			NodeEditorGUI.nodeSkin.button.normal.textColor = NodeEditorGUI.NE_TextColor;
			NodeEditorGUI.nodeSkin.button.normal.background = NodeEditorGUI.GUIButton;
			NodeEditorGUI.nodeSkin.textArea.normal.background = NodeEditorGUI.GUIBox;
			NodeEditorGUI.nodeSkin.textArea.active.background = NodeEditorGUI.GUIBox;
			NodeEditorGUI.nodeLabelBold = new GUIStyle(NodeEditorGUI.nodeLabel);
			NodeEditorGUI.nodeLabelBold.fontStyle = FontStyle.Bold;
			NodeEditorGUI.nodeLabelSelected = new GUIStyle(NodeEditorGUI.nodeLabel);
			NodeEditorGUI.nodeLabelSelected.normal.background = RTEditorGUI.ColorToTex(1, NodeEditorGUI.NE_LightColor);
			NodeEditorGUI.nodeBoxBold = new GUIStyle(NodeEditorGUI.nodeBox);
			NodeEditorGUI.nodeBoxBold.fontStyle = FontStyle.Bold;
			return true;
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x00063851 File Offset: 0x00061A51
		public static void StartNodeGUI()
		{
			if (GUI.skin != NodeEditorGUI.defaultSkin)
			{
				if (NodeEditorGUI.nodeSkin == null)
				{
					NodeEditorGUI.Init(true);
				}
				GUI.skin = NodeEditorGUI.nodeSkin;
			}
			OverlayGUI.StartOverlayGUI();
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x00063887 File Offset: 0x00061A87
		public static void EndNodeGUI()
		{
			OverlayGUI.EndOverlayGUI();
			if (GUI.skin == NodeEditorGUI.defaultSkin)
			{
				GUI.skin = NodeEditorGUI.defaultSkin;
			}
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000638AC File Offset: 0x00061AAC
		public static void DrawConnection(Vector2 startPos, Vector2 endPos, Color col)
		{
			Vector2 vector = ((startPos.x <= endPos.x) ? Vector2.right : Vector2.left);
			NodeEditorGUI.DrawConnection(startPos, vector, endPos, -vector, col);
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x000638E3 File Offset: 0x00061AE3
		public static void DrawConnection(Vector2 startPos, Vector2 startDir, Vector2 endPos, Vector2 endDir, Color col)
		{
			NodeEditorGUI.DrawConnection(startPos, startDir, endPos, endDir, ConnectionDrawMethod.Bezier, col);
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x000638F4 File Offset: 0x00061AF4
		public static void DrawConnection(Vector2 startPos, Vector2 startDir, Vector2 endPos, Vector2 endDir, ConnectionDrawMethod drawMethod, Color col)
		{
			if (drawMethod == ConnectionDrawMethod.Bezier)
			{
				float num = 80f;
				RTEditorGUI.DrawBezier(startPos, endPos, startPos + startDir * num, endPos + endDir * num, col * Color.gray, null, 3f);
				return;
			}
			if (drawMethod == ConnectionDrawMethod.StraightLine)
			{
				RTEditorGUI.DrawLine(startPos, endPos, col * Color.gray, null, 3f);
			}
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x00063960 File Offset: 0x00061B60
		internal static Vector2 GetSecondConnectionVector(Vector2 startPos, Vector2 endPos, Vector2 firstVector)
		{
			if (firstVector.x != 0f && firstVector.y == 0f)
			{
				if (startPos.x > endPos.x)
				{
					return firstVector;
				}
				return -firstVector;
			}
			else
			{
				if (firstVector.y == 0f || firstVector.x != 0f)
				{
					return -firstVector;
				}
				if (startPos.y > endPos.y)
				{
					return firstVector;
				}
				return -firstVector;
			}
		}

		// Token: 0x0400111A RID: 4378
		public static int knobSize = 16;

		// Token: 0x0400111B RID: 4379
		public static Color NE_LightColor = new Color(0.4f, 0.4f, 0.4f);

		// Token: 0x0400111C RID: 4380
		public static Color NE_TextColor = new Color(0.7f, 0.7f, 0.7f);

		// Token: 0x0400111D RID: 4381
		public static Texture2D Background;

		// Token: 0x0400111E RID: 4382
		public static Texture2D AALineTex;

		// Token: 0x0400111F RID: 4383
		public static Texture2D GUIBox;

		// Token: 0x04001120 RID: 4384
		public static Texture2D GUIButton;

		// Token: 0x04001121 RID: 4385
		public static Texture2D GUIBoxSelection;

		// Token: 0x04001122 RID: 4386
		public static GUISkin nodeSkin;

		// Token: 0x04001123 RID: 4387
		public static GUISkin defaultSkin;

		// Token: 0x04001124 RID: 4388
		public static GUIStyle nodeLabel;

		// Token: 0x04001125 RID: 4389
		public static GUIStyle nodeLabelBold;

		// Token: 0x04001126 RID: 4390
		public static GUIStyle nodeLabelSelected;

		// Token: 0x04001127 RID: 4391
		public static GUIStyle nodeBox;

		// Token: 0x04001128 RID: 4392
		public static GUIStyle nodeBoxBold;
	}
}
