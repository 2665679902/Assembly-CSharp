using System;
using System.Collections.Generic;

// Token: 0x02000A2B RID: 2603
public class UIStringFormatter
{
	// Token: 0x06004F00 RID: 20224 RVA: 0x001C16ED File Offset: 0x001BF8ED
	public string Format(string format, string s0)
	{
		return this.Replace(format, "{0}", s0);
	}

	// Token: 0x06004F01 RID: 20225 RVA: 0x001C16FC File Offset: 0x001BF8FC
	public string Format(string format, string s0, string s1)
	{
		return this.Replace(this.Replace(format, "{0}", s0), "{1}", s1);
	}

	// Token: 0x06004F02 RID: 20226 RVA: 0x001C1718 File Offset: 0x001BF918
	private string Replace(string format, string key, string value)
	{
		UIStringFormatter.Entry entry = default(UIStringFormatter.Entry);
		if (this.activeStringCount >= this.entries.Count)
		{
			entry.format = format;
			entry.key = key;
			entry.value = value;
			entry.result = entry.format.Replace(key, value);
			this.entries.Add(entry);
		}
		else
		{
			entry = this.entries[this.activeStringCount];
			if (entry.format != format || entry.key != key || entry.value != value)
			{
				entry.format = format;
				entry.key = key;
				entry.value = value;
				entry.result = entry.format.Replace(key, value);
				this.entries[this.activeStringCount] = entry;
			}
		}
		this.activeStringCount++;
		return entry.result;
	}

	// Token: 0x06004F03 RID: 20227 RVA: 0x001C1808 File Offset: 0x001BFA08
	public void BeginDrawing()
	{
		this.activeStringCount = 0;
	}

	// Token: 0x06004F04 RID: 20228 RVA: 0x001C1811 File Offset: 0x001BFA11
	public void EndDrawing()
	{
	}

	// Token: 0x0400351C RID: 13596
	private int activeStringCount;

	// Token: 0x0400351D RID: 13597
	private List<UIStringFormatter.Entry> entries = new List<UIStringFormatter.Entry>();

	// Token: 0x020018BC RID: 6332
	private struct Entry
	{
		// Token: 0x0400723A RID: 29242
		public string format;

		// Token: 0x0400723B RID: 29243
		public string key;

		// Token: 0x0400723C RID: 29244
		public string value;

		// Token: 0x0400723D RID: 29245
		public string result;
	}
}
