using System;
using TUNING;
using UnityEngine;

// Token: 0x020002AA RID: 682
public class PolymerizerConfig : IBuildingConfig
{
	// Token: 0x06000D89 RID: 3465 RVA: 0x0004B0B4 File Offset: 0x000492B4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Polymerizer";
		int num = 3;
		int num2 = 3;
		string text2 = "plasticrefinery_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER3;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		BuildingTemplates.CreateElectricalBuildingDef(buildingDef);
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.EnergyConsumptionWhenActive = 240f;
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 32f;
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.OutputConduitType = ConduitType.Gas;
		buildingDef.UtilityOutputOffset = new CellOffset(0, 1);
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 1));
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		return buildingDef;
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x0004B188 File Offset: 0x00049388
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		Polymerizer polymerizer = go.AddOrGet<Polymerizer>();
		polymerizer.emitMass = 30f;
		polymerizer.emitTag = GameTagExtensions.Create(SimHashes.Polypropylene);
		polymerizer.emitOffset = new Vector3(-1.45f, 1f, 0f);
		polymerizer.exhaustElement = SimHashes.Steam;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.consumptionRate = 1.6666666f;
		conduitConsumer.capacityTag = GameTagExtensions.Create(SimHashes.Petroleum);
		conduitConsumer.capacityKG = 1.6666666f;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Gas;
		conduitDispenser.invertElementFilter = false;
		conduitDispenser.elementFilter = new SimHashes[] { SimHashes.CarbonDioxide };
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(GameTagExtensions.Create(SimHashes.Petroleum), 0.8333333f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(0.5f, SimHashes.Polypropylene, 348.15f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0, true),
			new ElementConverter.OutputElement(0.008333334f, SimHashes.Steam, 473.15f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0, true),
			new ElementConverter.OutputElement(0.008333334f, SimHashes.CarbonDioxide, 423.15f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0, true)
		};
		go.AddOrGet<DropAllWorkable>();
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x0004B333 File Offset: 0x00049533
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGetDef<PoweredActiveController.Def>();
	}

	// Token: 0x040007D4 RID: 2004
	public const string ID = "Polymerizer";

	// Token: 0x040007D5 RID: 2005
	private const ConduitType INPUT_CONDUIT_TYPE = ConduitType.Liquid;

	// Token: 0x040007D6 RID: 2006
	private const ConduitType OUTPUT_CONDUIT_TYPE = ConduitType.Gas;

	// Token: 0x040007D7 RID: 2007
	private const float CONSUMED_OIL_KG_PER_DAY = 500f;

	// Token: 0x040007D8 RID: 2008
	private const float GENERATED_PLASTIC_KG_PER_DAY = 300f;

	// Token: 0x040007D9 RID: 2009
	private const float SECONDS_PER_PLASTIC_BLOCK = 60f;

	// Token: 0x040007DA RID: 2010
	private const float GENERATED_EXHAUST_STEAM_KG_PER_DAY = 5f;

	// Token: 0x040007DB RID: 2011
	private const float GENERATED_EXHAUST_CO2_KG_PER_DAY = 5f;

	// Token: 0x040007DC RID: 2012
	public const SimHashes INPUT_ELEMENT = SimHashes.Petroleum;

	// Token: 0x040007DD RID: 2013
	private const SimHashes PRODUCED_ELEMENT = SimHashes.Polypropylene;

	// Token: 0x040007DE RID: 2014
	private const SimHashes EXHAUST_ENVIRONMENT_ELEMENT = SimHashes.Steam;

	// Token: 0x040007DF RID: 2015
	private const SimHashes EXHAUST_CONDUIT_ELEMENT = SimHashes.CarbonDioxide;
}
