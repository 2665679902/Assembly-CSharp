using System;
using KSerialization;

// Token: 0x02000760 RID: 1888
[SerializationConfig(MemberSerialization.OptIn)]
public class CellEventInstance : EventInstanceBase, ISaveLoadable
{
	// Token: 0x060033F0 RID: 13296 RVA: 0x0011747E File Offset: 0x0011567E
	public CellEventInstance(int cell, int data, int data2, CellEvent ev)
		: base(ev)
	{
		this.cell = cell;
		this.data = data;
		this.data2 = data2;
	}

	// Token: 0x04001FDA RID: 8154
	[Serialize]
	public int cell;

	// Token: 0x04001FDB RID: 8155
	[Serialize]
	public int data;

	// Token: 0x04001FDC RID: 8156
	[Serialize]
	public int data2;
}
