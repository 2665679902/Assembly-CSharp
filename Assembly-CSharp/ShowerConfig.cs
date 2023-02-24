using System;
using TUNING;
using UnityEngine;

// Token: 0x02000306 RID: 774
public class ShowerConfig : IBuildingConfig
{
	// Token: 0x06000F69 RID: 3945 RVA: 0x00053CD4 File Offset: 0x00051ED4
	public override BuildingDef CreateBuildingDef()
	{
		string id = ShowerConfig.ID;
		int num = 2;
		int num2 = 4;
		string text = "shower_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER3;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, tier2, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.ExhaustKilowattsWhenActive = 0.25f;
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(1, 1);
		return buildingDef;
	}

	// Token: 0x06000F6A RID: 3946 RVA: 0x00053D6C File Offset: 0x00051F6C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.WashStation, false);
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.AdvancedWashStation, false);
		Shower shower = go.AddOrGet<Shower>();
		shower.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_shower_kanim") };
		shower.workTime = 15f;
		shower.outputTargetElement = SimHashes.DirtyWater;
		shower.fractionalDiseaseRemoval = 0.95f;
		shower.absoluteDiseaseRemoval = -2000;
		shower.workLayer = Grid.SceneLayer.BuildingFront;
		shower.trackUses = true;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Water).tag;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Store;
		conduitConsumer.capacityKG = 5f;
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.invertElementFilter = true;
		conduitDispenser.elementFilter = new SimHashes[] { SimHashes.Water };
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(new Tag("Water"), 1f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(1f, SimHashes.DirtyWater, 0f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0, true)
		};
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 10f;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		go.AddOrGet<RequireOutputs>().ignoreFullPipe = true;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x00053EF8 File Offset: 0x000520F8
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400087F RID: 2175
	public static string ID = "Shower";
}
