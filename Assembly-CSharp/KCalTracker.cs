using System;

// Token: 0x020004EC RID: 1260
public class KCalTracker : WorldTracker
{
	// Token: 0x06001DC4 RID: 7620 RVA: 0x0009EC84 File Offset: 0x0009CE84
	public KCalTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06001DC5 RID: 7621 RVA: 0x0009EC8D File Offset: 0x0009CE8D
	public override void UpdateData()
	{
		base.AddPoint(RationTracker.Get().CountRations(null, ClusterManager.Instance.GetWorld(base.WorldID).worldInventory, true));
	}

	// Token: 0x06001DC6 RID: 7622 RVA: 0x0009ECB6 File Offset: 0x0009CEB6
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedCalories(value, GameUtil.TimeSlice.None, true);
	}
}
