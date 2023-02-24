using System;
using System.Collections.Generic;
using System.Diagnostics;
using ImGuiNET;

// Token: 0x020000CD RID: 205
public class Logger<EntryType> : Logger
{
	// Token: 0x060007D2 RID: 2002 RVA: 0x0001FFF3 File Offset: 0x0001E1F3
	public IEnumerator<EntryType> GetEnumerator()
	{
		if (this.entries == null)
		{
			this.entries = new List<EntryType>();
		}
		return this.entries.GetEnumerator();
	}

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00020018 File Offset: 0x0001E218
	public override int Count
	{
		get
		{
			if (this.entries == null)
			{
				return 0;
			}
			return this.entries.Count;
		}
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x0002002F File Offset: 0x0001E22F
	public void SetMaxEntries(int new_max)
	{
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x00020031 File Offset: 0x0001E231
	public Logger(string name, int new_max = 35)
		: base(name)
	{
		this.SetMaxEntries(new_max);
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x00020041 File Offset: 0x0001E241
	[Conditional("UNITY_EDITOR")]
	public void Log(EntryType entry)
	{
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x00020044 File Offset: 0x0001E244
	public override void DebugDevTool()
	{
		bool flag = base.enableConsoleLogging;
		if (ImGui.Checkbox("Console Logging:", ref flag))
		{
			base.enableConsoleLogging = flag;
		}
		flag = base.breakOnLog;
		if (ImGui.Checkbox("Break On Log:", ref flag))
		{
			base.breakOnLog = flag;
		}
		ImGui.Text(this.name + " Log:");
		if (ImGui.Button("Clear"))
		{
			this.entries.Clear();
		}
		if (this.entries != null)
		{
			ImGui.Indent();
			foreach (EntryType entryType in this.entries)
			{
				ImGui.Text(entryType.ToString());
			}
			ImGui.Unindent();
		}
	}

	// Token: 0x04000617 RID: 1559
	private List<EntryType> entries;

	// Token: 0x04000618 RID: 1560
	public Action<EntryType> OnLog;
}
