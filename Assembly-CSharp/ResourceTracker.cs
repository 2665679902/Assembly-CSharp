using System;

// Token: 0x020004E8 RID: 1256
public class ResourceTracker : WorldTracker
{
	// Token: 0x1700014C RID: 332
	// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x0009E968 File Offset: 0x0009CB68
	// (set) Token: 0x06001DB7 RID: 7607 RVA: 0x0009E970 File Offset: 0x0009CB70
	public Tag tag { get; private set; }

	// Token: 0x06001DB8 RID: 7608 RVA: 0x0009E979 File Offset: 0x0009CB79
	public ResourceTracker(int worldID, Tag materialCategoryTag)
		: base(worldID)
	{
		this.tag = materialCategoryTag;
	}

	// Token: 0x06001DB9 RID: 7609 RVA: 0x0009E98C File Offset: 0x0009CB8C
	public override void UpdateData()
	{
		if (ClusterManager.Instance.GetWorld(base.WorldID).worldInventory == null)
		{
			return;
		}
		base.AddPoint(ClusterManager.Instance.GetWorld(base.WorldID).worldInventory.GetAmount(this.tag, false));
	}

	// Token: 0x06001DBA RID: 7610 RVA: 0x0009E9DE File Offset: 0x0009CBDE
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedMass(value, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
	}
}
