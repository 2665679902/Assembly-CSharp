using System;
using UnityEngine;

namespace NodeEditorFramework.Utilities
{
	// Token: 0x02000493 RID: 1171
	public class GenericMenu
	{
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06003245 RID: 12869 RVA: 0x0006779A File Offset: 0x0006599A
		public Vector2 Position
		{
			get
			{
				return GenericMenu.popup.Position;
			}
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000677A6 File Offset: 0x000659A6
		public GenericMenu()
		{
			GenericMenu.popup = new PopupMenu();
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000677B8 File Offset: 0x000659B8
		public void ShowAsContext()
		{
			GenericMenu.popup.Show(GUIScaleUtility.GUIToScreenSpace(Event.current.mousePosition), 40f);
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000677D8 File Offset: 0x000659D8
		public void Show(Vector2 pos, float MinWidth = 40f)
		{
			GenericMenu.popup.Show(GUIScaleUtility.GUIToScreenSpace(pos), MinWidth);
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000677EB File Offset: 0x000659EB
		public void AddItem(GUIContent content, bool on, PopupMenu.MenuFunctionData func, object userData)
		{
			GenericMenu.popup.AddItem(content, on, func, userData);
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000677FC File Offset: 0x000659FC
		public void AddItem(GUIContent content, bool on, PopupMenu.MenuFunction func)
		{
			GenericMenu.popup.AddItem(content, on, func);
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x0006780B File Offset: 0x00065A0B
		public void AddSeparator(string path)
		{
			GenericMenu.popup.AddSeparator(path);
		}

		// Token: 0x04001181 RID: 4481
		private static PopupMenu popup;
	}
}
