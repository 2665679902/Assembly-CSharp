using System;
using TUNING;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class DecontaminationShowerConfig : IBuildingConfig
{
	// Token: 0x060001A4 RID: 420 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x0000BEFC File Offset: 0x0000A0FC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "DecontaminationShower";
		int num = 2;
		int num2 = 4;
		string text2 = "decontamination_shower_kanim";
		int num3 = 250;
		float num4 = 120f;
		string[] radiation_CONTAINMENT = MATERIALS.RADIATION_CONTAINMENT;
		float[] array = new float[]
		{
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER5[0],
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER2[0]
		};
		string[] array2 = radiation_CONTAINMENT;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER3, tier, 0.2f);
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.UtilityInputOffset = new CellOffset(1, 2);
		return buildingDef;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x0000BF88 File Offset: 0x0000A188
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		KBatchedAnimController kbatchedAnimController = go.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.sceneLayer = Grid.SceneLayer.BuildingBack;
		kbatchedAnimController.fgLayer = Grid.SceneLayer.BuildingFront;
		HandSanitizer handSanitizer = go.AddOrGet<HandSanitizer>();
		handSanitizer.massConsumedPerUse = 100f;
		handSanitizer.consumedElement = SimHashes.Water;
		handSanitizer.outputElement = SimHashes.DirtyWater;
		handSanitizer.diseaseRemovalCount = 1000000;
		handSanitizer.maxUses = 1;
		handSanitizer.canSanitizeSuit = true;
		handSanitizer.canSanitizeStorage = true;
		go.AddOrGet<DirectionControl>();
		HandSanitizer.Work work = go.AddOrGet<HandSanitizer.Work>();
		work.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_decontamination_shower_kanim") };
		work.workLayer = Grid.SceneLayer.BuildingUse;
		work.workTime = 15f;
		work.trackUses = true;
		work.removeIrritation = true;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Water).tag;
		conduitConsumer.capacityKG = 100f;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Store;
		AutoStorageDropper.Def def = go.AddOrGetDef<AutoStorageDropper.Def>();
		def.elementFilter = new SimHashes[] { SimHashes.DirtyWater };
		def.dropOffset = new CellOffset(1, 0);
		go.AddOrGet<Storage>().SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040000F1 RID: 241
	public const string ID = "DecontaminationShower";

	// Token: 0x040000F2 RID: 242
	private const float MASS_PER_USE = 100f;

	// Token: 0x040000F3 RID: 243
	private const int DISEASE_REMOVAL_COUNT = 1000000;

	// Token: 0x040000F4 RID: 244
	private const float WATER_PER_USE = 100f;

	// Token: 0x040000F5 RID: 245
	private const int USES_PER_FLUSH = 1;

	// Token: 0x040000F6 RID: 246
	private const float WORK_TIME = 15f;

	// Token: 0x040000F7 RID: 247
	private const SimHashes CONSUMED_ELEMENT = SimHashes.Water;

	// Token: 0x040000F8 RID: 248
	private const SimHashes PRODUCED_ELEMENT = SimHashes.DirtyWater;
}
