using System;
using TUNING;
using UnityEngine;

// Token: 0x020005A2 RID: 1442
public class DesalinatorConfig : IBuildingConfig
{
	// Token: 0x06002392 RID: 9106 RVA: 0x000BFF58 File Offset: 0x000BE158
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Desalinator";
		int num = 4;
		int num2 = 3;
		string text2 = "desalinator_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.SelfHeatKilowattsWhenActive = 8f;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.Floodable = false;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.UtilityInputOffset = new CellOffset(-1, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		return buildingDef;
	}

	// Token: 0x06002393 RID: 9107 RVA: 0x000C0014 File Offset: 0x000BE214
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		Storage storage = go.AddOrGet<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage.showInUI = true;
		go.AddOrGet<Desalinator>().maxSalt = 945f;
		ElementConverter elementConverter = go.AddComponent<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(new Tag("SaltWater"), 5f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(4.65f, SimHashes.Water, 0f, false, true, 0f, 0.5f, 0.75f, byte.MaxValue, 0, true),
			new ElementConverter.OutputElement(0.35f, SimHashes.Salt, 0f, false, true, 0f, 0.5f, 0.25f, byte.MaxValue, 0, true)
		};
		ElementConverter elementConverter2 = go.AddComponent<ElementConverter>();
		elementConverter2.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(new Tag("Brine"), 5f, true)
		};
		elementConverter2.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(3.5f, SimHashes.Water, 0f, false, true, 0f, 0.5f, 0.75f, byte.MaxValue, 0, true),
			new ElementConverter.OutputElement(1.5f, SimHashes.Salt, 0f, false, true, 0f, 0.5f, 0.25f, byte.MaxValue, 0, true)
		};
		DesalinatorWorkableEmpty desalinatorWorkableEmpty = go.AddOrGet<DesalinatorWorkableEmpty>();
		desalinatorWorkableEmpty.workTime = 90f;
		desalinatorWorkableEmpty.workLayer = Grid.SceneLayer.BuildingFront;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.consumptionRate = 10f;
		conduitConsumer.capacityKG = 20f;
		conduitConsumer.capacityTag = GameTags.AnyWater;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Store;
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.invertElementFilter = true;
		conduitDispenser.elementFilter = new SimHashes[]
		{
			SimHashes.SaltWater,
			SimHashes.Brine
		};
		Prioritizable.AddRef(go);
	}

	// Token: 0x06002394 RID: 9108 RVA: 0x000C0223 File Offset: 0x000BE423
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
	}

	// Token: 0x06002395 RID: 9109 RVA: 0x000C022D File Offset: 0x000BE42D
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
	}

	// Token: 0x06002396 RID: 9110 RVA: 0x000C0236 File Offset: 0x000BE436
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<PoweredActiveController.Def>().showWorkingStatus = true;
	}

	// Token: 0x04001466 RID: 5222
	public const string ID = "Desalinator";

	// Token: 0x04001467 RID: 5223
	private const ConduitType CONDUIT_TYPE = ConduitType.Liquid;

	// Token: 0x04001468 RID: 5224
	private const float INPUT_RATE = 5f;

	// Token: 0x04001469 RID: 5225
	private const float SALT_WATER_TO_SALT_OUTPUT_RATE = 0.35f;

	// Token: 0x0400146A RID: 5226
	private const float SALT_WATER_TO_CLEAN_WATER_OUTPUT_RATE = 4.65f;

	// Token: 0x0400146B RID: 5227
	private const float BRINE_TO_SALT_OUTPUT_RATE = 1.5f;

	// Token: 0x0400146C RID: 5228
	private const float BRINE_TO_CLEAN_WATER_OUTPUT_RATE = 3.5f;
}
