using System;

// Token: 0x0200078B RID: 1931
public class GasBreatherFromWorldProvider : OxygenBreather.IGasProvider
{
	// Token: 0x06003601 RID: 13825 RVA: 0x0012B9D0 File Offset: 0x00129BD0
	public void OnSetOxygenBreather(OxygenBreather oxygen_breather)
	{
		this.suffocationMonitor = new SuffocationMonitor.Instance(oxygen_breather);
		this.suffocationMonitor.StartSM();
		this.safeCellMonitor = new SafeCellMonitor.Instance(oxygen_breather);
		this.safeCellMonitor.StartSM();
		this.oxygenBreather = oxygen_breather;
		this.nav = this.oxygenBreather.GetComponent<Navigator>();
	}

	// Token: 0x06003602 RID: 13826 RVA: 0x0012BA23 File Offset: 0x00129C23
	public void OnClearOxygenBreather(OxygenBreather oxygen_breather)
	{
		this.suffocationMonitor.StopSM("Removed gas provider");
		this.safeCellMonitor.StopSM("Removed gas provider");
	}

	// Token: 0x06003603 RID: 13827 RVA: 0x0012BA45 File Offset: 0x00129C45
	public bool ShouldEmitCO2()
	{
		return this.nav.CurrentNavType != NavType.Tube;
	}

	// Token: 0x06003604 RID: 13828 RVA: 0x0012BA58 File Offset: 0x00129C58
	public bool ShouldStoreCO2()
	{
		return false;
	}

	// Token: 0x06003605 RID: 13829 RVA: 0x0012BA5C File Offset: 0x00129C5C
	public bool ConsumeGas(OxygenBreather oxygen_breather, float gas_consumed)
	{
		if (this.nav.CurrentNavType != NavType.Tube)
		{
			SimHashes getBreathableElement = oxygen_breather.GetBreathableElement;
			if (getBreathableElement == SimHashes.Vacuum)
			{
				return false;
			}
			HandleVector<Game.ComplexCallbackInfo<Sim.MassConsumedCallback>>.Handle handle = Game.Instance.massConsumedCallbackManager.Add(new Action<Sim.MassConsumedCallback, object>(GasBreatherFromWorldProvider.OnSimConsumeCallback), this, "GasBreatherFromWorldProvider");
			SimMessages.ConsumeMass(oxygen_breather.mouthCell, getBreathableElement, gas_consumed, 3, handle.index);
		}
		return true;
	}

	// Token: 0x06003606 RID: 13830 RVA: 0x0012BAC0 File Offset: 0x00129CC0
	private static void OnSimConsumeCallback(Sim.MassConsumedCallback mass_cb_info, object data)
	{
		((GasBreatherFromWorldProvider)data).OnSimConsume(mass_cb_info);
	}

	// Token: 0x06003607 RID: 13831 RVA: 0x0012BAD0 File Offset: 0x00129CD0
	private void OnSimConsume(Sim.MassConsumedCallback mass_cb_info)
	{
		if (this.oxygenBreather == null || this.oxygenBreather.GetComponent<KPrefabID>().HasTag(GameTags.Dead))
		{
			return;
		}
		if (ElementLoader.elements[(int)mass_cb_info.elemIdx].id == SimHashes.ContaminatedOxygen)
		{
			this.oxygenBreather.Trigger(-935848905, mass_cb_info);
		}
		Game.Instance.accumulators.Accumulate(this.oxygenBreather.O2Accumulator, mass_cb_info.mass);
		float num = -mass_cb_info.mass;
		ReportManager.Instance.ReportValue(ReportManager.ReportType.OxygenCreated, num, this.oxygenBreather.GetProperName(), null);
		this.oxygenBreather.Consume(mass_cb_info);
	}

	// Token: 0x0400240B RID: 9227
	private SuffocationMonitor.Instance suffocationMonitor;

	// Token: 0x0400240C RID: 9228
	private SafeCellMonitor.Instance safeCellMonitor;

	// Token: 0x0400240D RID: 9229
	private OxygenBreather oxygenBreather;

	// Token: 0x0400240E RID: 9230
	private Navigator nav;
}
