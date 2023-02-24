using System;

// Token: 0x020004A8 RID: 1192
public static class NavigationTactics
{
	// Token: 0x04000EFA RID: 3834
	public static NavTactic ReduceTravelDistance = new NavTactic(0, 0, 1, 4);

	// Token: 0x04000EFB RID: 3835
	public static NavTactic Range_2_AvoidOverlaps = new NavTactic(2, 6, 12, 1);

	// Token: 0x04000EFC RID: 3836
	public static NavTactic Range_3_ProhibitOverlap = new NavTactic(3, 6, 9999, 1);
}
