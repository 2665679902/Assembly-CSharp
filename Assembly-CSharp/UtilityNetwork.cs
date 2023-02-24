using System;

// Token: 0x020009BA RID: 2490
public class UtilityNetwork
{
	// Token: 0x060049F5 RID: 18933 RVA: 0x0019E900 File Offset: 0x0019CB00
	public virtual void AddItem(object item)
	{
	}

	// Token: 0x060049F6 RID: 18934 RVA: 0x0019E902 File Offset: 0x0019CB02
	public virtual void RemoveItem(object item)
	{
	}

	// Token: 0x060049F7 RID: 18935 RVA: 0x0019E904 File Offset: 0x0019CB04
	public virtual void ConnectItem(object item)
	{
	}

	// Token: 0x060049F8 RID: 18936 RVA: 0x0019E906 File Offset: 0x0019CB06
	public virtual void DisconnectItem(object item)
	{
	}

	// Token: 0x060049F9 RID: 18937 RVA: 0x0019E908 File Offset: 0x0019CB08
	public virtual void Reset(UtilityNetworkGridNode[] grid)
	{
	}

	// Token: 0x04003095 RID: 12437
	public int id;

	// Token: 0x04003096 RID: 12438
	public ConduitType conduitType;
}
