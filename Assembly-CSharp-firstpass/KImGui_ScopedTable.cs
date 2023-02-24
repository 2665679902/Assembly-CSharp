using System;
using ImGuiNET;

// Token: 0x02000008 RID: 8
public struct KImGui_ScopedTable : IDisposable
{
	// Token: 0x06000062 RID: 98 RVA: 0x00004042 File Offset: 0x00002242
	public KImGui_ScopedTable(string label, int num_columns, ImGuiTableFlags flags = ImGuiTableFlags.None)
	{
		if (flags == ImGuiTableFlags.None)
		{
			this.do_pop = ImGui.BeginTable(label, num_columns);
			return;
		}
		this.do_pop = ImGui.BeginTable(label, num_columns, flags);
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00004063 File Offset: 0x00002263
	public void Dispose()
	{
		if (this.do_pop)
		{
			ImGui.EndTable();
		}
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00004072 File Offset: 0x00002272
	public static implicit operator bool(KImGui_ScopedTable n)
	{
		return n.do_pop;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x0000407A File Offset: 0x0000227A
	public static bool operator ==(KImGui_ScopedTable node, bool value)
	{
		return node.do_pop == value;
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00004085 File Offset: 0x00002285
	public static bool operator !=(KImGui_ScopedTable node, bool value)
	{
		return node.do_pop != value;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00004094 File Offset: 0x00002294
	public override bool Equals(object obj)
	{
		KImGui_ScopedTable kimGui_ScopedTable = (KImGui_ScopedTable)obj;
		return this == kimGui_ScopedTable;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x000040B9 File Offset: 0x000022B9
	public override int GetHashCode()
	{
		return this.do_pop.GetHashCode();
	}

	// Token: 0x04000005 RID: 5
	private bool do_pop;
}
