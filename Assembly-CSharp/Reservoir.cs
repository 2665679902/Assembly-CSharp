using System;
using UnityEngine;

// Token: 0x020008E4 RID: 2276
[AddComponentMenu("KMonoBehaviour/scripts/Reservoir")]
public class Reservoir : KMonoBehaviour
{
	// Token: 0x0600418E RID: 16782 RVA: 0x0016F69C File Offset: 0x0016D89C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_fill", "meter_OL" });
		base.Subscribe<Reservoir>(-1697596308, Reservoir.OnStorageChangeDelegate);
		this.OnStorageChange(null);
	}

	// Token: 0x0600418F RID: 16783 RVA: 0x0016F6FB File Offset: 0x0016D8FB
	private void OnStorageChange(object data)
	{
		this.meter.SetPositionPercent(Mathf.Clamp01(this.storage.MassStored() / this.storage.capacityKg));
	}

	// Token: 0x04002BB6 RID: 11190
	private MeterController meter;

	// Token: 0x04002BB7 RID: 11191
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04002BB8 RID: 11192
	private static readonly EventSystem.IntraObjectHandler<Reservoir> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<Reservoir>(delegate(Reservoir component, object data)
	{
		component.OnStorageChange(data);
	});
}
