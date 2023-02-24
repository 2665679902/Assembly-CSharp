using System;

// Token: 0x020004F2 RID: 1266
public abstract class WorldTracker : Tracker
{
	// Token: 0x1700014D RID: 333
	// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x0009EF09 File Offset: 0x0009D109
	// (set) Token: 0x06001DD7 RID: 7639 RVA: 0x0009EF11 File Offset: 0x0009D111
	public int WorldID { get; private set; }

	// Token: 0x06001DD8 RID: 7640 RVA: 0x0009EF1A File Offset: 0x0009D11A
	public WorldTracker(int worldID)
	{
		this.WorldID = worldID;
	}
}
