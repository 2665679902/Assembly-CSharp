using System;

// Token: 0x0200050D RID: 1293
public class RoomTypeCategory : Resource
{
	// Token: 0x17000166 RID: 358
	// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x000A56AD File Offset: 0x000A38AD
	// (set) Token: 0x06001EF1 RID: 7921 RVA: 0x000A56B5 File Offset: 0x000A38B5
	public string colorName { get; private set; }

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x000A56BE File Offset: 0x000A38BE
	// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x000A56C6 File Offset: 0x000A38C6
	public string icon { get; private set; }

	// Token: 0x06001EF4 RID: 7924 RVA: 0x000A56CF File Offset: 0x000A38CF
	public RoomTypeCategory(string id, string name, string colorName, string icon)
		: base(id, name)
	{
		this.colorName = colorName;
		this.icon = icon;
	}
}
