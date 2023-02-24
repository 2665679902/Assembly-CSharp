using System;
using ImGuiNET;

// Token: 0x02000006 RID: 6
public struct KImGui_ScopedTreeNode : IDisposable
{
	// Token: 0x06000059 RID: 89 RVA: 0x00003FB6 File Offset: 0x000021B6
	public KImGui_ScopedTreeNode(string label)
	{
		this.do_pop = ImGui.TreeNode(label);
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00003FC4 File Offset: 0x000021C4
	public void Dispose()
	{
		if (this.do_pop)
		{
			ImGui.TreePop();
		}
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00003FD3 File Offset: 0x000021D3
	public static implicit operator bool(KImGui_ScopedTreeNode n)
	{
		return n.do_pop;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00003FDB File Offset: 0x000021DB
	public static bool operator ==(KImGui_ScopedTreeNode node, bool value)
	{
		return node.do_pop == value;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00003FE6 File Offset: 0x000021E6
	public static bool operator !=(KImGui_ScopedTreeNode node, bool value)
	{
		return node.do_pop != value;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00003FF4 File Offset: 0x000021F4
	public override bool Equals(object obj)
	{
		KImGui_ScopedTreeNode kimGui_ScopedTreeNode = (KImGui_ScopedTreeNode)obj;
		return this == kimGui_ScopedTreeNode;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00004019 File Offset: 0x00002219
	public override int GetHashCode()
	{
		return this.do_pop.GetHashCode();
	}

	// Token: 0x04000003 RID: 3
	private bool do_pop;
}
