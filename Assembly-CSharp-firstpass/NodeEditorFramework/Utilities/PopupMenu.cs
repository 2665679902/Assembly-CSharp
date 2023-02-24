using System;
using System.Collections.Generic;
using UnityEngine;

namespace NodeEditorFramework.Utilities
{
	// Token: 0x02000492 RID: 1170
	public class PopupMenu
	{
		// Token: 0x06003238 RID: 12856 RVA: 0x00066FC1 File Offset: 0x000651C1
		public PopupMenu()
		{
			this.SetupGUI();
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x00066FDC File Offset: 0x000651DC
		public void SetupGUI()
		{
			PopupMenu.backgroundStyle = new GUIStyle(GUI.skin.box);
			PopupMenu.backgroundStyle.contentOffset = new Vector2(2f, 2f);
			PopupMenu.expandRight = ResourceManager.LoadTexture("Textures/expandRight.png");
			PopupMenu.itemHeight = GUI.skin.label.CalcHeight(new GUIContent("text"), 100f);
			PopupMenu.selectedLabel = new GUIStyle(GUI.skin.label);
			PopupMenu.selectedLabel.normal.background = RTEditorGUI.ColorToTex(1, new Color(0.4f, 0.4f, 0.4f));
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x00067085 File Offset: 0x00065285
		public void Show(Vector2 pos, float MinWidth = 40f)
		{
			this.minWidth = MinWidth;
			this.position = PopupMenu.calculateRect(pos, this.menuItems, this.minWidth);
			this.selectedPath = "";
			OverlayGUI.currentPopup = this;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x000670B7 File Offset: 0x000652B7
		public Vector2 Position
		{
			get
			{
				return this.position.position;
			}
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000670C4 File Offset: 0x000652C4
		public void AddItem(GUIContent content, bool on, PopupMenu.MenuFunctionData func, object userData)
		{
			string text;
			PopupMenu.MenuItem menuItem = this.AddHierarchy(ref content, out text);
			if (menuItem != null)
			{
				menuItem.subItems.Add(new PopupMenu.MenuItem(text, content, func, userData));
				return;
			}
			this.menuItems.Add(new PopupMenu.MenuItem(text, content, func, userData));
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x0006710C File Offset: 0x0006530C
		public void AddItem(GUIContent content, bool on, PopupMenu.MenuFunction func)
		{
			string text;
			PopupMenu.MenuItem menuItem = this.AddHierarchy(ref content, out text);
			if (menuItem != null)
			{
				menuItem.subItems.Add(new PopupMenu.MenuItem(text, content, func));
				return;
			}
			this.menuItems.Add(new PopupMenu.MenuItem(text, content, func));
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x00067150 File Offset: 0x00065350
		public void AddSeparator(string path)
		{
			GUIContent guicontent = new GUIContent(path);
			PopupMenu.MenuItem menuItem = this.AddHierarchy(ref guicontent, out path);
			if (menuItem != null)
			{
				menuItem.subItems.Add(new PopupMenu.MenuItem());
				return;
			}
			this.menuItems.Add(new PopupMenu.MenuItem());
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x00067194 File Offset: 0x00065394
		private PopupMenu.MenuItem AddHierarchy(ref GUIContent content, out string path)
		{
			path = content.text;
			if (path.Contains("/"))
			{
				string[] array = path.Split(new char[] { '/' });
				string folderPath = array[0];
				PopupMenu.MenuItem menuItem = this.menuItems.Find((PopupMenu.MenuItem item) => item.content != null && item.content.text == folderPath && item.group);
				if (menuItem == null)
				{
					this.menuItems.Add(menuItem = new PopupMenu.MenuItem(folderPath, new GUIContent(folderPath), true));
				}
				for (int i = 1; i < array.Length - 1; i++)
				{
					string folder = array[i];
					folderPath = folderPath + "/" + folder;
					if (menuItem == null)
					{
						global::Debug.LogError("Parent is null!");
					}
					else if (menuItem.subItems == null)
					{
						global::Debug.LogError("Subitems of " + menuItem.content.text + " is null!");
					}
					PopupMenu.MenuItem menuItem2 = menuItem.subItems.Find((PopupMenu.MenuItem item) => item.content != null && item.content.text == folder && item.group);
					if (menuItem2 == null)
					{
						menuItem.subItems.Add(menuItem2 = new PopupMenu.MenuItem(folderPath, new GUIContent(folder), true));
					}
					menuItem = menuItem2;
				}
				path = content.text;
				content = new GUIContent(array[array.Length - 1], content.tooltip);
				return menuItem;
			}
			return null;
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x00067300 File Offset: 0x00065500
		public void Draw()
		{
			bool flag = this.DrawGroup(this.position, this.menuItems);
			while (this.groupToDraw != null && !this.close)
			{
				PopupMenu.MenuItem menuItem = this.groupToDraw;
				this.groupToDraw = null;
				if (menuItem.group && this.DrawGroup(menuItem.groupPos, menuItem.subItems))
				{
					flag = true;
				}
			}
			if (!flag || this.close)
			{
				OverlayGUI.currentPopup = null;
			}
			NodeEditor.RepaintClients();
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x00067374 File Offset: 0x00065574
		private bool DrawGroup(Rect pos, List<PopupMenu.MenuItem> menuItems)
		{
			Rect rect = PopupMenu.calculateRect(pos.position, menuItems, this.minWidth);
			Rect rect2 = new Rect(rect);
			rect2.xMax += 20f;
			rect2.xMin -= 20f;
			rect2.yMax += 20f;
			rect2.yMin -= 20f;
			bool flag = rect2.Contains(Event.current.mousePosition);
			this.currentItemHeight = PopupMenu.backgroundStyle.contentOffset.y;
			GUI.BeginGroup(PopupMenu.extendRect(rect, PopupMenu.backgroundStyle.contentOffset), GUIContent.none, PopupMenu.backgroundStyle);
			for (int i = 0; i < menuItems.Count; i++)
			{
				this.DrawItem(menuItems[i], rect);
				if (this.close)
				{
					break;
				}
			}
			GUI.EndGroup();
			return flag;
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x0006745C File Offset: 0x0006565C
		private void DrawItem(PopupMenu.MenuItem item, Rect groupRect)
		{
			if (item.separator)
			{
				if (Event.current.type == EventType.Repaint)
				{
					RTEditorGUI.Seperator(new Rect(PopupMenu.backgroundStyle.contentOffset.x + 1f, this.currentItemHeight + 1f, groupRect.width - 2f, 1f));
				}
				this.currentItemHeight += 3f;
				return;
			}
			Rect rect = new Rect(PopupMenu.backgroundStyle.contentOffset.x, this.currentItemHeight, groupRect.width, PopupMenu.itemHeight);
			if (rect.Contains(Event.current.mousePosition))
			{
				this.selectedPath = item.path;
			}
			bool flag = this.selectedPath == item.path || this.selectedPath.Contains(item.path + "/");
			GUI.Label(rect, item.content, flag ? PopupMenu.selectedLabel : GUI.skin.label);
			if (item.group)
			{
				GUI.DrawTexture(new Rect(rect.x + rect.width - 12f, rect.y + (rect.height - 12f) / 2f, 12f, 12f), PopupMenu.expandRight);
				if (flag)
				{
					item.groupPos = new Rect(groupRect.x + groupRect.width + 4f, groupRect.y + this.currentItemHeight - 2f, 0f, 0f);
					this.groupToDraw = item;
				}
			}
			else if (flag && (Event.current.type == EventType.MouseDown || (Event.current.button != 1 && Event.current.type == EventType.MouseUp)))
			{
				item.Execute();
				this.close = true;
				Event.current.Use();
			}
			this.currentItemHeight += PopupMenu.itemHeight;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x0006765C File Offset: 0x0006585C
		private static Rect extendRect(Rect rect, Vector2 extendValue)
		{
			rect.x -= extendValue.x;
			rect.y -= extendValue.y;
			rect.width += extendValue.x + extendValue.x;
			rect.height += extendValue.y + extendValue.y;
			return rect;
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x000676C8 File Offset: 0x000658C8
		private static Rect calculateRect(Vector2 position, List<PopupMenu.MenuItem> menuItems, float minWidth)
		{
			float num = minWidth;
			float num2 = 0f;
			for (int i = 0; i < menuItems.Count; i++)
			{
				PopupMenu.MenuItem menuItem = menuItems[i];
				if (menuItem.separator)
				{
					num2 += 3f;
				}
				else
				{
					num = Mathf.Max(num, GUI.skin.label.CalcSize(menuItem.content).x + (float)(menuItem.group ? 22 : 10));
					num2 += PopupMenu.itemHeight;
				}
			}
			Vector2 vector = new Vector2(num, num2);
			bool flag = position.y + vector.y <= (float)Screen.height;
			return new Rect(position.x, position.y - (flag ? 0f : vector.y), vector.x, vector.y);
		}

		// Token: 0x04001176 RID: 4470
		public List<PopupMenu.MenuItem> menuItems = new List<PopupMenu.MenuItem>();

		// Token: 0x04001177 RID: 4471
		private Rect position;

		// Token: 0x04001178 RID: 4472
		private string selectedPath;

		// Token: 0x04001179 RID: 4473
		private PopupMenu.MenuItem groupToDraw;

		// Token: 0x0400117A RID: 4474
		private float currentItemHeight;

		// Token: 0x0400117B RID: 4475
		private bool close;

		// Token: 0x0400117C RID: 4476
		public static GUIStyle backgroundStyle;

		// Token: 0x0400117D RID: 4477
		public static Texture2D expandRight;

		// Token: 0x0400117E RID: 4478
		public static float itemHeight;

		// Token: 0x0400117F RID: 4479
		public static GUIStyle selectedLabel;

		// Token: 0x04001180 RID: 4480
		public float minWidth;

		// Token: 0x02000AC3 RID: 2755
		// (Invoke) Token: 0x06005753 RID: 22355
		public delegate void MenuFunction();

		// Token: 0x02000AC4 RID: 2756
		// (Invoke) Token: 0x06005757 RID: 22359
		public delegate void MenuFunctionData(object userData);

		// Token: 0x02000AC5 RID: 2757
		public class MenuItem
		{
			// Token: 0x0600575A RID: 22362 RVA: 0x000A3149 File Offset: 0x000A1349
			public MenuItem()
			{
				this.separator = true;
			}

			// Token: 0x0600575B RID: 22363 RVA: 0x000A3158 File Offset: 0x000A1358
			public MenuItem(string _path, GUIContent _content, bool _group)
			{
				this.path = _path;
				this.content = _content;
				this.group = _group;
				if (this.group)
				{
					this.subItems = new List<PopupMenu.MenuItem>();
				}
			}

			// Token: 0x0600575C RID: 22364 RVA: 0x000A3188 File Offset: 0x000A1388
			public MenuItem(string _path, GUIContent _content, PopupMenu.MenuFunction _func)
			{
				this.path = _path;
				this.content = _content;
				this.func = _func;
			}

			// Token: 0x0600575D RID: 22365 RVA: 0x000A31A5 File Offset: 0x000A13A5
			public MenuItem(string _path, GUIContent _content, PopupMenu.MenuFunctionData _func, object _userData)
			{
				this.path = _path;
				this.content = _content;
				this.funcData = _func;
				this.userData = _userData;
			}

			// Token: 0x0600575E RID: 22366 RVA: 0x000A31CA File Offset: 0x000A13CA
			public void Execute()
			{
				if (this.funcData != null)
				{
					this.funcData(this.userData);
					return;
				}
				if (this.func != null)
				{
					this.func();
				}
			}

			// Token: 0x040024E9 RID: 9449
			public string path;

			// Token: 0x040024EA RID: 9450
			public GUIContent content;

			// Token: 0x040024EB RID: 9451
			public PopupMenu.MenuFunction func;

			// Token: 0x040024EC RID: 9452
			public PopupMenu.MenuFunctionData funcData;

			// Token: 0x040024ED RID: 9453
			public object userData;

			// Token: 0x040024EE RID: 9454
			public bool separator;

			// Token: 0x040024EF RID: 9455
			public bool group;

			// Token: 0x040024F0 RID: 9456
			public Rect groupPos;

			// Token: 0x040024F1 RID: 9457
			public List<PopupMenu.MenuItem> subItems;
		}
	}
}
