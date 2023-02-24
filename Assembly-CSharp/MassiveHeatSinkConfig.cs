using System;
using TUNING;
using UnityEngine;

// Token: 0x0200022D RID: 557
public class MassiveHeatSinkConfig : IBuildingConfig
{
	// Token: 0x06000AFF RID: 2815 RVA: 0x0003E028 File Offset: 0x0003C228
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MassiveHeatSink";
		int num = 4;
		int num2 = 4;
		string text2 = "massiveheatsink_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER2, tier2, 0.2f);
		buildingDef.ExhaustKilowattsWhenActive = -16f;
		buildingDef.SelfHeatKilowattsWhenActive = -64f;
		buildingDef.Floodable = true;
		buildingDef.Entombable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x0003E0B8 File Offset: 0x0003C2B8
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<MassiveHeatSink>();
		go.AddOrGet<MinimumOperatingTemperature>().minimumTemperature = 100f;
		PrimaryElement component = go.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Iron, true);
		component.Temperature = 294.15f;
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<Storage>().capacityKg = 0.099999994f;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.consumptionRate = 1f;
		conduitConsumer.capacityTag = GameTagExtensions.Create(SimHashes.Hydrogen);
		conduitConsumer.capacityKG = 0.099999994f;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		go.AddOrGet<ElementConverter>().consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(ElementLoader.FindElementByHash(SimHashes.Hydrogen).tag, 0.01f, true)
		};
		go.AddOrGetDef<PoweredActiveController.Def>();
		go.GetComponent<Deconstructable>().allowDeconstruction = false;
		go.AddOrGet<Demolishable>();
	}

	// Token: 0x04000673 RID: 1651
	public const string ID = "MassiveHeatSink";

	// Token: 0x04000674 RID: 1652
	private const float CONSUMPTION_RATE = 0.01f;

	// Token: 0x04000675 RID: 1653
	private const float STORAGE_CAPACITY = 0.099999994f;
}
