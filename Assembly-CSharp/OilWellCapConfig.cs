using System;
using TUNING;
using UnityEngine;

// Token: 0x02000286 RID: 646
public class OilWellCapConfig : IBuildingConfig
{
	// Token: 0x06000CE4 RID: 3300 RVA: 0x00047F3C File Offset: 0x0004613C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "OilWellCap";
		int num = 4;
		int num2 = 4;
		string text2 = "geyser_oil_cap_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		BuildingTemplates.CreateElectricalBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.EnergyConsumptionWhenActive = 240f;
		buildingDef.SelfHeatKilowattsWhenActive = 2f;
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.UtilityInputOffset = new CellOffset(0, 1);
		buildingDef.PowerInputOffset = new CellOffset(1, 1);
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		buildingDef.AttachmentSlotTag = GameTags.OilWell;
		buildingDef.BuildLocationRule = BuildLocationRule.BuildingAttachPoint;
		buildingDef.ObjectLayer = ObjectLayer.AttachableBuilding;
		return buildingDef;
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x00048014 File Offset: 0x00046214
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		BuildingTemplates.CreateDefaultStorage(go, false).showInUI = true;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.consumptionRate = 2f;
		conduitConsumer.capacityKG = 10f;
		conduitConsumer.capacityTag = OilWellCapConfig.INPUT_WATER_TAG;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(OilWellCapConfig.INPUT_WATER_TAG, 1f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(3.3333333f, SimHashes.CrudeOil, 363.15f, false, false, 2f, 1.5f, 0f, byte.MaxValue, 0, true)
		};
		OilWellCap oilWellCap = go.AddOrGet<OilWellCap>();
		oilWellCap.gasElement = SimHashes.Methane;
		oilWellCap.gasTemperature = 573.15f;
		oilWellCap.addGasRate = 0.033333335f;
		oilWellCap.maxGasPressure = 80.00001f;
		oilWellCap.releaseGasRate = 0.44444448f;
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x0004810E File Offset: 0x0004630E
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
	}

	// Token: 0x04000788 RID: 1928
	private const float WATER_INTAKE_RATE = 1f;

	// Token: 0x04000789 RID: 1929
	private const float WATER_TO_OIL_RATIO = 3.3333333f;

	// Token: 0x0400078A RID: 1930
	private const float LIQUID_STORAGE = 10f;

	// Token: 0x0400078B RID: 1931
	private const float GAS_RATE = 0.033333335f;

	// Token: 0x0400078C RID: 1932
	private const float OVERPRESSURE_TIME = 2400f;

	// Token: 0x0400078D RID: 1933
	private const float PRESSURE_RELEASE_TIME = 180f;

	// Token: 0x0400078E RID: 1934
	private const float PRESSURE_RELEASE_RATE = 0.44444448f;

	// Token: 0x0400078F RID: 1935
	private static readonly Tag INPUT_WATER_TAG = SimHashes.Water.CreateTag();

	// Token: 0x04000790 RID: 1936
	public const string ID = "OilWellCap";
}
