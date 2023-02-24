using System;

// Token: 0x020000A0 RID: 160
public struct ValueArrayHandle
{
	// Token: 0x06000621 RID: 1569 RVA: 0x0001C1F8 File Offset: 0x0001A3F8
	public ValueArrayHandle(int handle)
	{
		this.handle = handle;
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x0001C201 File Offset: 0x0001A401
	public bool IsValid()
	{
		return this.handle >= 0;
	}

	// Token: 0x0400059C RID: 1436
	public int handle;

	// Token: 0x0400059D RID: 1437
	public static readonly ValueArrayHandle Invalid = new ValueArrayHandle(-1);
}
