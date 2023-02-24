using System;

// Token: 0x02000592 RID: 1426
[Serializable]
public class ConduitPortInfo
{
	// Token: 0x0600230B RID: 8971 RVA: 0x000BE0F1 File Offset: 0x000BC2F1
	public ConduitPortInfo(ConduitType type, CellOffset offset)
	{
		this.conduitType = type;
		this.offset = offset;
	}

	// Token: 0x0400142B RID: 5163
	public ConduitType conduitType;

	// Token: 0x0400142C RID: 5164
	public CellOffset offset;
}
