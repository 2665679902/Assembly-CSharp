using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000489 RID: 1161
public struct InfraredVisualizerData
{
	// Token: 0x060019F0 RID: 6640 RVA: 0x0008AC5C File Offset: 0x00088E5C
	public void Update()
	{
		float num = 0f;
		if (this.temperatureAmount != null)
		{
			num = this.temperatureAmount.value;
		}
		else if (this.structureTemperature.IsValid())
		{
			num = GameComps.StructureTemperatures.GetPayload(this.structureTemperature).Temperature;
		}
		else if (this.primaryElement != null)
		{
			num = this.primaryElement.Temperature;
		}
		else if (this.temperatureVulnerable != null)
		{
			num = this.temperatureVulnerable.InternalTemperature;
		}
		if (num < 0f)
		{
			return;
		}
		Color32 color = SimDebugView.Instance.NormalizedTemperature(num);
		this.controller.OverlayColour = color;
	}

	// Token: 0x060019F1 RID: 6641 RVA: 0x0008AD10 File Offset: 0x00088F10
	public InfraredVisualizerData(GameObject go)
	{
		this.controller = go.GetComponent<KBatchedAnimController>();
		if (this.controller != null)
		{
			this.temperatureAmount = Db.Get().Amounts.Temperature.Lookup(go);
			this.structureTemperature = GameComps.StructureTemperatures.GetHandle(go);
			this.primaryElement = go.GetComponent<PrimaryElement>();
			this.temperatureVulnerable = go.GetComponent<TemperatureVulnerable>();
			return;
		}
		this.temperatureAmount = null;
		this.structureTemperature = HandleVector<int>.InvalidHandle;
		this.primaryElement = null;
		this.temperatureVulnerable = null;
	}

	// Token: 0x04000E84 RID: 3716
	public KAnimControllerBase controller;

	// Token: 0x04000E85 RID: 3717
	public AmountInstance temperatureAmount;

	// Token: 0x04000E86 RID: 3718
	public HandleVector<int>.Handle structureTemperature;

	// Token: 0x04000E87 RID: 3719
	public PrimaryElement primaryElement;

	// Token: 0x04000E88 RID: 3720
	public TemperatureVulnerable temperatureVulnerable;
}
