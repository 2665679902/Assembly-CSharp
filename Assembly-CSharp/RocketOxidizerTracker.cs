using System;

// Token: 0x020004F0 RID: 1264
public class RocketOxidizerTracker : WorldTracker
{
	// Token: 0x06001DD0 RID: 7632 RVA: 0x0009EE48 File Offset: 0x0009D048
	public RocketOxidizerTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06001DD1 RID: 7633 RVA: 0x0009EE54 File Offset: 0x0009D054
	public override void UpdateData()
	{
		Clustercraft component = ClusterManager.Instance.GetWorld(base.WorldID).GetComponent<Clustercraft>();
		base.AddPoint((component != null) ? component.ModuleInterface.OxidizerPowerRemaining : 0f);
	}

	// Token: 0x06001DD2 RID: 7634 RVA: 0x0009EE98 File Offset: 0x0009D098
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedMass(value, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
	}
}
