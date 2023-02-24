using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NodeEditorFramework.Utilities
{
	// Token: 0x02000490 RID: 1168
	public static class GUIScaleUtility
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600321D RID: 12829 RVA: 0x000666B2 File Offset: 0x000648B2
		public static Rect getTopRect
		{
			get
			{
				return GUIScaleUtility.GetTopRectDelegate();
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x000666BE File Offset: 0x000648BE
		public static Rect getTopRectScreenSpace
		{
			get
			{
				return GUIScaleUtility.topmostRectDelegate();
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600321F RID: 12831 RVA: 0x000666CA File Offset: 0x000648CA
		// (set) Token: 0x06003220 RID: 12832 RVA: 0x000666D1 File Offset: 0x000648D1
		public static List<Rect> currentRectStack { get; private set; }

		// Token: 0x06003221 RID: 12833 RVA: 0x000666D9 File Offset: 0x000648D9
		public static void CheckInit()
		{
			if (!GUIScaleUtility.initiated)
			{
				GUIScaleUtility.Init();
			}
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000666E8 File Offset: 0x000648E8
		public static void Init()
		{
			Type type = Assembly.GetAssembly(typeof(GUI)).GetType("UnityEngine.GUIClip", true);
			PropertyInfo property = type.GetProperty("topmostRect", BindingFlags.Static | BindingFlags.Public);
			MethodInfo method = type.GetMethod("GetTopRect", BindingFlags.Static | BindingFlags.NonPublic);
			MethodInfo method2 = type.GetMethod("Clip", BindingFlags.Static | BindingFlags.Public, Type.DefaultBinder, new Type[] { typeof(Rect) }, new ParameterModifier[0]);
			if (type == null || property == null || method == null || method2 == null)
			{
				global::Debug.LogWarning("GUIScaleUtility cannot run on this system! Compability mode enabled. For you that means you're not able to use the Node Editor inside more than one group:( Please PM me (Seneral @UnityForums) so I can figure out what causes this! Thanks!");
				global::Debug.LogWarning(((type == null) ? "GUIClipType is Null, " : "") + ((property == null) ? "topmostRect is Null, " : "") + ((method == null) ? "GetTopRect is Null, " : "") + ((method2 == null) ? "ClipRect is Null, " : ""));
				GUIScaleUtility.compabilityMode = true;
				GUIScaleUtility.initiated = true;
				return;
			}
			GUIScaleUtility.GetTopRectDelegate = (Func<Rect>)Delegate.CreateDelegate(typeof(Func<Rect>), method);
			GUIScaleUtility.topmostRectDelegate = (Func<Rect>)Delegate.CreateDelegate(typeof(Func<Rect>), property.GetGetMethod());
			if (GUIScaleUtility.GetTopRectDelegate == null || GUIScaleUtility.topmostRectDelegate == null)
			{
				global::Debug.LogWarning("GUIScaleUtility cannot run on this system! Compability mode enabled. For you that means you're not able to use the Node Editor inside more than one group:( Please PM me (Seneral @UnityForums) so I can figure out what causes this! Thanks!");
				global::Debug.LogWarning(((type == null) ? "GUIClipType is Null, " : "") + ((property == null) ? "topmostRect is Null, " : "") + ((method == null) ? "GetTopRect is Null, " : "") + ((method2 == null) ? "ClipRect is Null, " : ""));
				GUIScaleUtility.compabilityMode = true;
				GUIScaleUtility.initiated = true;
				return;
			}
			GUIScaleUtility.currentRectStack = new List<Rect>();
			GUIScaleUtility.rectStackGroups = new List<List<Rect>>();
			GUIScaleUtility.GUIMatrices = new List<Matrix4x4>();
			GUIScaleUtility.adjustedGUILayout = new List<bool>();
			GUIScaleUtility.initiated = true;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x000668DC File Offset: 0x00064ADC
		public static Vector2 getCurrentScale
		{
			get
			{
				return new Vector2(1f / GUI.matrix.GetColumn(0).magnitude, 1f / GUI.matrix.GetColumn(1).magnitude);
			}
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x00066928 File Offset: 0x00064B28
		public static Vector2 BeginScale(ref Rect rect, Vector2 zoomPivot, float zoom, bool adjustGUILayout)
		{
			Rect rect2;
			if (GUIScaleUtility.compabilityMode)
			{
				GUI.EndGroup();
				rect2 = rect;
			}
			else
			{
				GUIScaleUtility.BeginNoClip();
				rect2 = GUIScaleUtility.GUIToScaledSpace(rect);
			}
			rect = GUIScaleUtility.Scale(rect2, rect2.position + zoomPivot, new Vector2(zoom, zoom));
			GUI.BeginGroup(rect);
			rect.position = Vector2.zero;
			Vector2 vector = rect.center - rect2.size / 2f + zoomPivot;
			GUIScaleUtility.adjustedGUILayout.Add(adjustGUILayout);
			if (adjustGUILayout)
			{
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Space(rect.center.x - rect2.size.x + zoomPivot.x);
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				GUILayout.Space(rect.center.y - rect2.size.y + zoomPivot.y);
			}
			GUIScaleUtility.GUIMatrices.Add(GUI.matrix);
			GUIUtility.ScaleAroundPivot(new Vector2(1f / zoom, 1f / zoom), vector);
			return vector;
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x00066A48 File Offset: 0x00064C48
		public static void EndScale()
		{
			if (GUIScaleUtility.GUIMatrices.Count == 0 || GUIScaleUtility.adjustedGUILayout.Count == 0)
			{
				throw new UnityException("GUIScaleUtility: You are ending more scale regions than you are beginning!");
			}
			GUI.matrix = GUIScaleUtility.GUIMatrices[GUIScaleUtility.GUIMatrices.Count - 1];
			GUIScaleUtility.GUIMatrices.RemoveAt(GUIScaleUtility.GUIMatrices.Count - 1);
			if (GUIScaleUtility.adjustedGUILayout[GUIScaleUtility.adjustedGUILayout.Count - 1])
			{
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
			}
			GUIScaleUtility.adjustedGUILayout.RemoveAt(GUIScaleUtility.adjustedGUILayout.Count - 1);
			GUI.EndGroup();
			if (!GUIScaleUtility.compabilityMode)
			{
				GUIScaleUtility.RestoreClips();
				return;
			}
			if (!Application.isPlaying)
			{
				GUI.BeginClip(new Rect(0f, 23f, (float)Screen.width, (float)(Screen.height - 23)));
				return;
			}
			GUI.BeginClip(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x00066B40 File Offset: 0x00064D40
		public static void BeginNoClip()
		{
			List<Rect> list = new List<Rect>();
			Rect rect = GUIScaleUtility.getTopRect;
			while (rect != new Rect(-10000f, -10000f, 40000f, 40000f))
			{
				list.Add(rect);
				GUI.EndClip();
				rect = GUIScaleUtility.getTopRect;
			}
			list.Reverse();
			GUIScaleUtility.rectStackGroups.Add(list);
			GUIScaleUtility.currentRectStack.AddRange(list);
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x00066BAC File Offset: 0x00064DAC
		public static void MoveClipsUp(int count)
		{
			List<Rect> list = new List<Rect>();
			Rect rect = GUIScaleUtility.getTopRect;
			while (rect != new Rect(-10000f, -10000f, 40000f, 40000f) && count > 0)
			{
				list.Add(rect);
				GUI.EndClip();
				rect = GUIScaleUtility.getTopRect;
				count--;
			}
			list.Reverse();
			GUIScaleUtility.rectStackGroups.Add(list);
			GUIScaleUtility.currentRectStack.AddRange(list);
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x00066C20 File Offset: 0x00064E20
		public static void RestoreClips()
		{
			if (GUIScaleUtility.rectStackGroups.Count == 0)
			{
				global::Debug.LogError("GUIClipHierarchy: BeginNoClip/MoveClipsUp - RestoreClips count not balanced!");
				return;
			}
			List<Rect> list = GUIScaleUtility.rectStackGroups[GUIScaleUtility.rectStackGroups.Count - 1];
			for (int i = 0; i < list.Count; i++)
			{
				GUI.BeginClip(list[i]);
				GUIScaleUtility.currentRectStack.RemoveAt(GUIScaleUtility.currentRectStack.Count - 1);
			}
			GUIScaleUtility.rectStackGroups.RemoveAt(GUIScaleUtility.rectStackGroups.Count - 1);
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x00066CA4 File Offset: 0x00064EA4
		public static void BeginNewLayout()
		{
			if (GUIScaleUtility.compabilityMode)
			{
				return;
			}
			Rect getTopRect = GUIScaleUtility.getTopRect;
			if (getTopRect != new Rect(-10000f, -10000f, 40000f, 40000f))
			{
				GUILayout.BeginArea(new Rect(0f, 0f, getTopRect.width, getTopRect.height));
				return;
			}
			GUILayout.BeginArea(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x00066D23 File Offset: 0x00064F23
		public static void EndNewLayout()
		{
			if (!GUIScaleUtility.compabilityMode)
			{
				GUILayout.EndArea();
			}
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x00066D31 File Offset: 0x00064F31
		public static void BeginIgnoreMatrix()
		{
			GUIScaleUtility.GUIMatrices.Add(GUI.matrix);
			GUI.matrix = Matrix4x4.identity;
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x00066D4C File Offset: 0x00064F4C
		public static void EndIgnoreMatrix()
		{
			if (GUIScaleUtility.GUIMatrices.Count == 0)
			{
				throw new UnityException("GUIScaleutility: You are ending more ignoreMatrices than you are beginning!");
			}
			GUI.matrix = GUIScaleUtility.GUIMatrices[GUIScaleUtility.GUIMatrices.Count - 1];
			GUIScaleUtility.GUIMatrices.RemoveAt(GUIScaleUtility.GUIMatrices.Count - 1);
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x00066DA1 File Offset: 0x00064FA1
		public static Vector2 Scale(Vector2 pos, Vector2 pivot, Vector2 scale)
		{
			return Vector2.Scale(pos - pivot, scale) + pivot;
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x00066DB6 File Offset: 0x00064FB6
		public static Rect Scale(Rect rect, Vector2 pivot, Vector2 scale)
		{
			rect.position = Vector2.Scale(rect.position - pivot, scale) + pivot;
			rect.size = Vector2.Scale(rect.size, scale);
			return rect;
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x00066DF0 File Offset: 0x00064FF0
		public static Vector2 ScaledToGUISpace(Vector2 scaledPosition)
		{
			if (GUIScaleUtility.rectStackGroups == null || GUIScaleUtility.rectStackGroups.Count == 0)
			{
				return scaledPosition;
			}
			List<Rect> list = GUIScaleUtility.rectStackGroups[GUIScaleUtility.rectStackGroups.Count - 1];
			for (int i = 0; i < list.Count; i++)
			{
				scaledPosition -= list[i].position;
			}
			return scaledPosition;
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x00066E52 File Offset: 0x00065052
		public static Rect ScaledToGUISpace(Rect scaledRect)
		{
			if (GUIScaleUtility.rectStackGroups == null || GUIScaleUtility.rectStackGroups.Count == 0)
			{
				return scaledRect;
			}
			scaledRect.position = GUIScaleUtility.ScaledToGUISpace(scaledRect.position);
			return scaledRect;
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x00066E80 File Offset: 0x00065080
		public static Vector2 GUIToScaledSpace(Vector2 guiPosition)
		{
			if (GUIScaleUtility.rectStackGroups == null || GUIScaleUtility.rectStackGroups.Count == 0)
			{
				return guiPosition;
			}
			List<Rect> list = GUIScaleUtility.rectStackGroups[GUIScaleUtility.rectStackGroups.Count - 1];
			for (int i = 0; i < list.Count; i++)
			{
				guiPosition += list[i].position;
			}
			return guiPosition;
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x00066EE2 File Offset: 0x000650E2
		public static Rect GUIToScaledSpace(Rect guiRect)
		{
			if (GUIScaleUtility.rectStackGroups == null || GUIScaleUtility.rectStackGroups.Count == 0)
			{
				return guiRect;
			}
			guiRect.position = GUIScaleUtility.GUIToScaledSpace(guiRect.position);
			return guiRect;
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x00066F10 File Offset: 0x00065110
		public static Vector2 GUIToScreenSpace(Vector2 guiPosition)
		{
			return guiPosition + GUIScaleUtility.getTopRectScreenSpace.position;
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x00066F30 File Offset: 0x00065130
		public static Rect GUIToScreenSpace(Rect guiRect)
		{
			guiRect.position += GUIScaleUtility.getTopRectScreenSpace.position;
			return guiRect;
		}

		// Token: 0x0400116B RID: 4459
		private static bool compabilityMode;

		// Token: 0x0400116C RID: 4460
		private static bool initiated;

		// Token: 0x0400116D RID: 4461
		private static FieldInfo currentGUILayoutCache;

		// Token: 0x0400116E RID: 4462
		private static FieldInfo currentTopLevelGroup;

		// Token: 0x0400116F RID: 4463
		private static Func<Rect> GetTopRectDelegate;

		// Token: 0x04001170 RID: 4464
		private static Func<Rect> topmostRectDelegate;

		// Token: 0x04001172 RID: 4466
		private static List<List<Rect>> rectStackGroups;

		// Token: 0x04001173 RID: 4467
		private static List<Matrix4x4> GUIMatrices;

		// Token: 0x04001174 RID: 4468
		private static List<bool> adjustedGUILayout;
	}
}
