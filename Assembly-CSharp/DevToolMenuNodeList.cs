using System;
using System.IO;
using ImGuiNET;

// Token: 0x0200051F RID: 1311
public class DevToolMenuNodeList
{
	// Token: 0x06001F8C RID: 8076 RVA: 0x000A9C1C File Offset: 0x000A7E1C
	public DevToolMenuNodeParent AddOrGetParentFor(string childPath)
	{
		string[] array = Path.GetDirectoryName(childPath).Split(new char[] { '/' });
		string text = "";
		DevToolMenuNodeParent devToolMenuNodeParent = this.root;
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string split = array2[i];
			text += devToolMenuNodeParent.GetName();
			IMenuNode menuNode = devToolMenuNodeParent.children.Find((IMenuNode x) => x.GetName() == split);
			DevToolMenuNodeParent devToolMenuNodeParent3;
			if (menuNode != null)
			{
				DevToolMenuNodeParent devToolMenuNodeParent2 = menuNode as DevToolMenuNodeParent;
				if (devToolMenuNodeParent2 == null)
				{
					throw new Exception("Conflict! Both a leaf and parent node exist at path: " + text);
				}
				devToolMenuNodeParent3 = devToolMenuNodeParent2;
			}
			else
			{
				devToolMenuNodeParent3 = new DevToolMenuNodeParent(split);
				devToolMenuNodeParent.AddChild(devToolMenuNodeParent3);
			}
			devToolMenuNodeParent = devToolMenuNodeParent3;
		}
		return devToolMenuNodeParent;
	}

	// Token: 0x06001F8D RID: 8077 RVA: 0x000A9CD0 File Offset: 0x000A7ED0
	public DevToolMenuNodeAction AddAction(string path, System.Action onClickFn)
	{
		DevToolMenuNodeAction devToolMenuNodeAction = new DevToolMenuNodeAction(Path.GetFileName(path), onClickFn);
		this.AddOrGetParentFor(path).AddChild(devToolMenuNodeAction);
		return devToolMenuNodeAction;
	}

	// Token: 0x06001F8E RID: 8078 RVA: 0x000A9CF8 File Offset: 0x000A7EF8
	public void Draw()
	{
		foreach (IMenuNode menuNode in this.root.children)
		{
			menuNode.Draw();
		}
	}

	// Token: 0x06001F8F RID: 8079 RVA: 0x000A9D50 File Offset: 0x000A7F50
	public void DrawFull()
	{
		if (ImGui.BeginMainMenuBar())
		{
			this.Draw();
			ImGui.EndMainMenuBar();
		}
	}

	// Token: 0x04001200 RID: 4608
	private DevToolMenuNodeParent root = new DevToolMenuNodeParent("<root>");
}
