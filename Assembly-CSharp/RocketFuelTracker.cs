using System;

// Token: 0x020004EF RID: 1263
public class RocketFuelTracker : WorldTracker
{
	// Token: 0x06001DCD RID: 7629 RVA: 0x0009EDE9 File Offset: 0x0009CFE9
	public RocketFuelTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06001DCE RID: 7630 RVA: 0x0009EDF4 File Offset: 0x0009CFF4
	public override void UpdateData()
	{
		Clustercraft component = ClusterManager.Instance.GetWorld(base.WorldID).GetComponent<Clustercraft>();
		base.AddPoint((component != null) ? component.ModuleInterface.FuelRemaining : 0f);
	}

	// Token: 0x06001DCF RID: 7631 RVA: 0x0009EE38 File Offset: 0x0009D038
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedMass(value, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
	}
}
