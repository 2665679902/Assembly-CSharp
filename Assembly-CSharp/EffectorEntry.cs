using System;
using STRINGS;

// Token: 0x02000732 RID: 1842
internal struct EffectorEntry
{
	// Token: 0x0600327E RID: 12926 RVA: 0x00110C53 File Offset: 0x0010EE53
	public EffectorEntry(string name, float value)
	{
		this.name = name;
		this.value = value;
		this.count = 1;
	}

	// Token: 0x0600327F RID: 12927 RVA: 0x00110C6C File Offset: 0x0010EE6C
	public override string ToString()
	{
		string text = "";
		if (this.count > 1)
		{
			text = string.Format(UI.OVERLAYS.DECOR.COUNT, this.count);
		}
		return string.Format(UI.OVERLAYS.DECOR.ENTRY, GameUtil.GetFormattedDecor(this.value, false), this.name, text);
	}

	// Token: 0x04001EC1 RID: 7873
	public string name;

	// Token: 0x04001EC2 RID: 7874
	public int count;

	// Token: 0x04001EC3 RID: 7875
	public float value;
}
