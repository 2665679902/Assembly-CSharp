using System;

// Token: 0x020009D4 RID: 2516
public class WorldParentChangedEventArgs
{
	// Token: 0x0400311F RID: 12575
	public int lastParentId = (int)ClusterManager.INVALID_WORLD_IDX;

	// Token: 0x04003120 RID: 12576
	public WorldContainer world;
}
