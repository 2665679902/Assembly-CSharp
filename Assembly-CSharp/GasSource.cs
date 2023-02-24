using System;
using KSerialization;

// Token: 0x02000483 RID: 1155
[SerializationConfig(MemberSerialization.OptIn)]
public class GasSource : SubstanceSource
{
	// Token: 0x060019CB RID: 6603 RVA: 0x0008A842 File Offset: 0x00088A42
	protected override CellOffset[] GetOffsetGroup()
	{
		return OffsetGroups.LiquidSource;
	}

	// Token: 0x060019CC RID: 6604 RVA: 0x0008A849 File Offset: 0x00088A49
	protected override IChunkManager GetChunkManager()
	{
		return GasSourceManager.Instance;
	}

	// Token: 0x060019CD RID: 6605 RVA: 0x0008A850 File Offset: 0x00088A50
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}
}
