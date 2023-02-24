using System;
using TUNING;
using UnityEngine;

// Token: 0x02000292 RID: 658
public class OreScrubberConfig : IBuildingConfig
{
	// Token: 0x06000D1C RID: 3356 RVA: 0x00048A8C File Offset: 0x00046C8C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "OreScrubber";
		int num = 3;
		int num2 = 3;
		string text2 = "orescrubber_kanim";
		int num3 = 30;
		float num4 = 30f;
		string[] array = new string[] { "Metal" };
		float[] array2 = new float[] { BUILDINGS.CONSTRUCTION_MASS_KG.TIER3[0] };
		string[] array3 = array;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array2, array3, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.UtilityInputOffset = new CellOffset(1, 1);
		buildingDef.ForegroundLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.InputConduitType = ConduitType.Gas;
		return buildingDef;
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x00048B04 File Offset: 0x00046D04
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		OreScrubber oreScrubber = go.AddOrGet<OreScrubber>();
		oreScrubber.massConsumedPerUse = 0.07f;
		oreScrubber.consumedElement = SimHashes.ChlorineGas;
		oreScrubber.diseaseRemovalCount = 480000;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.consumptionRate = 1f;
		conduitConsumer.capacityKG = 10f;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.ChlorineGas).tag;
		go.AddOrGet<DirectionControl>();
		OreScrubber.Work work = go.AddOrGet<OreScrubber.Work>();
		work.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_ore_scrubber_kanim") };
		work.workTime = 10.200001f;
		work.trackUses = true;
		work.workLayer = Grid.SceneLayer.BuildingUse;
		go.AddOrGet<Storage>().SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x00048BDC File Offset: 0x00046DDC
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<RequireInputs>().requireConduitHasMass = false;
	}

	// Token: 0x04000799 RID: 1945
	public const string ID = "OreScrubber";

	// Token: 0x0400079A RID: 1946
	private const float MASS_PER_USE = 0.07f;

	// Token: 0x0400079B RID: 1947
	private const int DISEASE_REMOVAL_COUNT = 480000;

	// Token: 0x0400079C RID: 1948
	private const SimHashes CONSUMED_ELEMENT = SimHashes.ChlorineGas;
}
