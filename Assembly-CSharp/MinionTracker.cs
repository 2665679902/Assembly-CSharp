using System;

// Token: 0x020004F3 RID: 1267
public abstract class MinionTracker : Tracker
{
	// Token: 0x06001DD9 RID: 7641 RVA: 0x0009EF29 File Offset: 0x0009D129
	public MinionTracker(MinionIdentity identity)
	{
		this.identity = identity;
	}

	// Token: 0x040010B8 RID: 4280
	public MinionIdentity identity;
}
