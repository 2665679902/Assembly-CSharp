using System;
using TUNING;
using UnityEngine;

// Token: 0x02000285 RID: 645
public class OilRefineryConfig : IBuildingConfig
{
	// Token: 0x06000CE0 RID: 3296 RVA: 0x00047D10 File Offset: 0x00045F10
	public override BuildingDef CreateBuildingDef()
	{
		string text = "OilRefinery";
		int num = 4;
		int num2 = 4;
		string text2 = "oilrefinery_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER3;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(1, 0);
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.ExhaustKilowattsWhenActive = 2f;
		buildingDef.SelfHeatKilowattsWhenActive = 8f;
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.UtilityOutputOffset = new CellOffset(1, 1);
		return buildingDef;
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x00047DD0 File Offset: 0x00045FD0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		OilRefinery oilRefinery = go.AddOrGet<OilRefinery>();
		oilRefinery.overpressureWarningMass = 4.5f;
		oilRefinery.overpressureMass = 5f;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.consumptionRate = 10f;
		conduitConsumer.capacityTag = SimHashes.CrudeOil.CreateTag();
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		conduitConsumer.capacityKG = 100f;
		conduitConsumer.forceAlwaysSatisfied = true;
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.invertElementFilter = true;
		conduitDispenser.elementFilter = new SimHashes[] { SimHashes.CrudeOil };
		go.AddOrGet<Storage>().showInUI = true;
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(SimHashes.CrudeOil.CreateTag(), 10f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(5f, SimHashes.Petroleum, 348.15f, false, true, 0f, 1f, 1f, byte.MaxValue, 0, true),
			new ElementConverter.OutputElement(0.09f, SimHashes.Methane, 348.15f, false, false, 0f, 3f, 1f, byte.MaxValue, 0, true)
		};
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x00047F2F File Offset: 0x0004612F
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000781 RID: 1921
	public const string ID = "OilRefinery";

	// Token: 0x04000782 RID: 1922
	public const SimHashes INPUT_ELEMENT = SimHashes.CrudeOil;

	// Token: 0x04000783 RID: 1923
	private const SimHashes OUTPUT_LIQUID_ELEMENT = SimHashes.Petroleum;

	// Token: 0x04000784 RID: 1924
	private const SimHashes OUTPUT_GAS_ELEMENT = SimHashes.Methane;

	// Token: 0x04000785 RID: 1925
	public const float CONSUMPTION_RATE = 10f;

	// Token: 0x04000786 RID: 1926
	public const float OUTPUT_LIQUID_RATE = 5f;

	// Token: 0x04000787 RID: 1927
	public const float OUTPUT_GAS_RATE = 0.09f;
}
