using System;

// Token: 0x020003BD RID: 957
public struct NavOffset
{
	// Token: 0x060013C3 RID: 5059 RVA: 0x00068F94 File Offset: 0x00067194
	public NavOffset(NavType nav_type, int x, int y)
	{
		this.navType = nav_type;
		this.offset.x = x;
		this.offset.y = y;
	}

	// Token: 0x04000ACF RID: 2767
	public NavType navType;

	// Token: 0x04000AD0 RID: 2768
	public CellOffset offset;
}
