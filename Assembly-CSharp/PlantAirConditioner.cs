using System;
using KSerialization;

// Token: 0x02000624 RID: 1572
[SerializationConfig(MemberSerialization.OptIn)]
public class PlantAirConditioner : AirConditioner
{
	// Token: 0x06002928 RID: 10536 RVA: 0x000D9A58 File Offset: 0x000D7C58
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<PlantAirConditioner>(-1396791468, PlantAirConditioner.OnFertilizedDelegate);
		base.Subscribe<PlantAirConditioner>(-1073674739, PlantAirConditioner.OnUnfertilizedDelegate);
	}

	// Token: 0x06002929 RID: 10537 RVA: 0x000D9A82 File Offset: 0x000D7C82
	private void OnFertilized(object data)
	{
		this.operational.SetFlag(this.fertilizedFlag, true);
	}

	// Token: 0x0600292A RID: 10538 RVA: 0x000D9A96 File Offset: 0x000D7C96
	private void OnUnfertilized(object data)
	{
		this.operational.SetFlag(this.fertilizedFlag, false);
	}

	// Token: 0x04001841 RID: 6209
	private Operational.Flag fertilizedFlag = new Operational.Flag("fertilized", Operational.Flag.Type.Requirement);

	// Token: 0x04001842 RID: 6210
	private static readonly EventSystem.IntraObjectHandler<PlantAirConditioner> OnFertilizedDelegate = new EventSystem.IntraObjectHandler<PlantAirConditioner>(delegate(PlantAirConditioner component, object data)
	{
		component.OnFertilized(data);
	});

	// Token: 0x04001843 RID: 6211
	private static readonly EventSystem.IntraObjectHandler<PlantAirConditioner> OnUnfertilizedDelegate = new EventSystem.IntraObjectHandler<PlantAirConditioner>(delegate(PlantAirConditioner component, object data)
	{
		component.OnUnfertilized(data);
	});
}
