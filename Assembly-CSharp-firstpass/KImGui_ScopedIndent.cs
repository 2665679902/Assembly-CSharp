using System;
using ImGuiNET;

// Token: 0x02000007 RID: 7
public struct KImGui_ScopedIndent : IDisposable
{
	// Token: 0x06000060 RID: 96 RVA: 0x00004026 File Offset: 0x00002226
	public KImGui_ScopedIndent(float indent_amount = 0f)
	{
		this.indent_amount = indent_amount;
		ImGui.Indent(indent_amount);
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00004035 File Offset: 0x00002235
	public void Dispose()
	{
		ImGui.Unindent(this.indent_amount);
	}

	// Token: 0x04000004 RID: 4
	private float indent_amount;
}
