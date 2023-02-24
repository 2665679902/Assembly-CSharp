using System;

// Token: 0x020000C1 RID: 193
public struct TagChangedEventData
{
	// Token: 0x06000741 RID: 1857 RVA: 0x0001ECB6 File Offset: 0x0001CEB6
	public TagChangedEventData(Tag tag, bool added)
	{
		this.tag = tag;
		this.added = added;
	}

	// Token: 0x040005E6 RID: 1510
	public Tag tag;

	// Token: 0x040005E7 RID: 1511
	public bool added;
}
