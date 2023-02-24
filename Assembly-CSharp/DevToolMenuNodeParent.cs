using System;
using System.Collections.Generic;
using ImGuiNET;

// Token: 0x02000521 RID: 1313
public class DevToolMenuNodeParent : IMenuNode
{
	// Token: 0x06001F93 RID: 8083 RVA: 0x000A9D7C File Offset: 0x000A7F7C
	public DevToolMenuNodeParent(string name)
	{
		this.name = name;
		this.children = new List<IMenuNode>();
	}

	// Token: 0x06001F94 RID: 8084 RVA: 0x000A9D96 File Offset: 0x000A7F96
	public void AddChild(IMenuNode menuNode)
	{
		this.children.Add(menuNode);
	}

	// Token: 0x06001F95 RID: 8085 RVA: 0x000A9DA4 File Offset: 0x000A7FA4
	public string GetName()
	{
		return this.name;
	}

	// Token: 0x06001F96 RID: 8086 RVA: 0x000A9DAC File Offset: 0x000A7FAC
	public void Draw()
	{
		if (ImGui.BeginMenu(this.name))
		{
			foreach (IMenuNode menuNode in this.children)
			{
				menuNode.Draw();
			}
			ImGui.EndMenu();
		}
	}

	// Token: 0x04001201 RID: 4609
	public string name;

	// Token: 0x04001202 RID: 4610
	public List<IMenuNode> children;
}
