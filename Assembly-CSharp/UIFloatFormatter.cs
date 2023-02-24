using System;
using System.Collections.Generic;

// Token: 0x02000A2A RID: 2602
public class UIFloatFormatter
{
	// Token: 0x06004EFB RID: 20219 RVA: 0x001C15C7 File Offset: 0x001BF7C7
	public string Format(string format, float value)
	{
		return this.Replace(format, "{0}", value);
	}

	// Token: 0x06004EFC RID: 20220 RVA: 0x001C15D8 File Offset: 0x001BF7D8
	private string Replace(string format, string key, float value)
	{
		UIFloatFormatter.Entry entry = default(UIFloatFormatter.Entry);
		if (this.activeStringCount >= this.entries.Count)
		{
			entry.format = format;
			entry.key = key;
			entry.value = value;
			entry.result = entry.format.Replace(key, value.ToString());
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
				entry.result = entry.format.Replace(key, value.ToString());
				this.entries[this.activeStringCount] = entry;
			}
		}
		this.activeStringCount++;
		return entry.result;
	}

	// Token: 0x06004EFD RID: 20221 RVA: 0x001C16CF File Offset: 0x001BF8CF
	public void BeginDrawing()
	{
		this.activeStringCount = 0;
	}

	// Token: 0x06004EFE RID: 20222 RVA: 0x001C16D8 File Offset: 0x001BF8D8
	public void EndDrawing()
	{
	}

	// Token: 0x0400351A RID: 13594
	private int activeStringCount;

	// Token: 0x0400351B RID: 13595
	private List<UIFloatFormatter.Entry> entries = new List<UIFloatFormatter.Entry>();

	// Token: 0x020018BB RID: 6331
	private struct Entry
	{
		// Token: 0x04007236 RID: 29238
		public string format;

		// Token: 0x04007237 RID: 29239
		public string key;

		// Token: 0x04007238 RID: 29240
		public float value;

		// Token: 0x04007239 RID: 29241
		public string result;
	}
}
