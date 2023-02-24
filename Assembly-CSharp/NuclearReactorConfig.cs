using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000282 RID: 642
public class NuclearReactorConfig : IBuildingConfig
{
	// Token: 0x06000CD2 RID: 3282 RVA: 0x000476B0 File Offset: 0x000458B0
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x000476B8 File Offset: 0x000458B8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "NuclearReactor";
		int num = 5;
		int num2 = 6;
		string text2 = "generatornuclear_kanim";
		int num3 = 100;
		float num4 = 480f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.PENALTY.TIER2, tier2, 0.2f);
		buildingDef.GeneratorWattageRating = 0f;
		buildingDef.GeneratorBaseCapacity = 10000f;
		buildingDef.RequiresPowerInput = false;
		buildingDef.RequiresPowerOutput = false;
		buildingDef.ThermalConductivity = 0.1f;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.Overheatable = false;
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.UtilityInputOffset = new CellOffset(-2, 2);
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.InputPort("CONTROL_FUEL_DELIVERY", new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.NUCLEARREACTOR.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.NUCLEARREACTOR.INPUT_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.NUCLEARREACTOR.INPUT_PORT_INACTIVE, false, true) };
		buildingDef.ViewMode = OverlayModes.Temperature.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.AudioSize = "large";
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.Breakable = false;
		buildingDef.Invincible = true;
		buildingDef.Deprecated = !Sim.IsRadiationEnabled();
		return buildingDef;
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x000477FC File Offset: 0x000459FC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		UnityEngine.Object.Destroy(go.GetComponent<BuildingEnabledButton>());
		RadiationEmitter radiationEmitter = go.AddComponent<RadiationEmitter>();
		radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
		radiationEmitter.emitRadiusX = 25;
		radiationEmitter.emitRadiusY = 25;
		radiationEmitter.radiusProportionalToRads = false;
		radiationEmitter.emissionOffset = new Vector3(0f, 2f, 0f);
		Storage storage = go.AddComponent<Storage>();
		storage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Insulate,
			Storage.StoredItemModifier.Hide
		});
		go.AddComponent<Storage>().SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Insulate,
			Storage.StoredItemModifier.Hide
		});
		go.AddComponent<Storage>().SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Insulate,
			Storage.StoredItemModifier.Hide
		});
		ManualDeliveryKG manualDeliveryKG = go.AddComponent<ManualDeliveryKG>();
		manualDeliveryKG.RequestedItemTag = ElementLoader.FindElementByHash(SimHashes.EnrichedUranium).tag;
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.PowerFetch.IdHash;
		manualDeliveryKG.capacity = 180f;
		manualDeliveryKG.MinimumMass = 0.5f;
		go.AddOrGet<Reactor>();
		go.AddOrGet<LoopingSounds>();
		Prioritizable.AddRef(go);
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.consumptionRate = 10f;
		conduitConsumer.capacityKG = 90f;
		conduitConsumer.capacityTag = GameTags.AnyWater;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		conduitConsumer.storage = storage;
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x0004797A File Offset: 0x00045B7A
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddTag(GameTags.CorrosionProof);
	}

	// Token: 0x04000764 RID: 1892
	public const string ID = "NuclearReactor";

	// Token: 0x04000765 RID: 1893
	private const float FUEL_CAPACITY = 180f;

	// Token: 0x04000766 RID: 1894
	public const float VENT_STEAM_TEMPERATURE = 673.15f;

	// Token: 0x04000767 RID: 1895
	public const float MELT_DOWN_TEMPERATURE = 3000f;

	// Token: 0x04000768 RID: 1896
	public const float MAX_VENT_PRESSURE = 150f;

	// Token: 0x04000769 RID: 1897
	public const float INCREASED_CONDUCTION_SCALE = 5f;

	// Token: 0x0400076A RID: 1898
	public const float REACTION_STRENGTH = 100f;

	// Token: 0x0400076B RID: 1899
	public const int RADIATION_EMITTER_RANGE = 25;

	// Token: 0x0400076C RID: 1900
	public const float OPERATIONAL_RADIATOR_INTENSITY = 2400f;

	// Token: 0x0400076D RID: 1901
	public const float MELT_DOWN_RADIATOR_INTENSITY = 4800f;

	// Token: 0x0400076E RID: 1902
	public const float FUEL_CONSUMPTION_SPEED = 0.016666668f;

	// Token: 0x0400076F RID: 1903
	public const float BEGIN_REACTION_MASS = 0.5f;

	// Token: 0x04000770 RID: 1904
	public const float STOP_REACTION_MASS = 0.25f;

	// Token: 0x04000771 RID: 1905
	public const float DUMP_WASTE_AMOUNT = 100f;

	// Token: 0x04000772 RID: 1906
	public const float WASTE_MASS_MULTIPLIER = 100f;

	// Token: 0x04000773 RID: 1907
	public const float REACTION_MASS_TARGET = 60f;

	// Token: 0x04000774 RID: 1908
	public const float COOLANT_AMOUNT = 30f;

	// Token: 0x04000775 RID: 1909
	public const float COOLANT_CAPACITY = 90f;

	// Token: 0x04000776 RID: 1910
	public const float MINIMUM_COOLANT_MASS = 30f;

	// Token: 0x04000777 RID: 1911
	public const float WASTE_GERMS_PER_KG = 50f;

	// Token: 0x04000778 RID: 1912
	public const float PST_MELTDOWN_COOLING_TIME = 3000f;

	// Token: 0x04000779 RID: 1913
	public const string INPUT_PORT_ID = "CONTROL_FUEL_DELIVERY";
}
