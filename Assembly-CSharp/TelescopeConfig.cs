using System;
using TUNING;
using UnityEngine;

// Token: 0x02000337 RID: 823
public class TelescopeConfig : IBuildingConfig
{
	// Token: 0x06001067 RID: 4199 RVA: 0x00059557 File Offset: 0x00057757
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_VANILLA_ONLY;
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x00059560 File Offset: 0x00057760
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Telescope";
		int num = 4;
		int num2 = 6;
		string text2 = "telescope_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.ExhaustKilowattsWhenActive = 0.125f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x00059604 File Offset: 0x00057804
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.ScienceBuilding, false);
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		Prioritizable.AddRef(go);
		Telescope telescope = go.AddOrGet<Telescope>();
		telescope.clearScanCellRadius = 5;
		telescope.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_telescope_kanim") };
		telescope.requiredSkillPerk = Db.Get().SkillPerks.CanStudyWorldObjects.Id;
		telescope.workLayer = Grid.SceneLayer.BuildingFront;
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 1000f;
		storage.showInUI = true;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.consumptionRate = 1f;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Oxygen).tag;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		conduitConsumer.capacityKG = 10f;
		conduitConsumer.forceAlwaysSatisfied = true;
		go.AddOrGetDef<PoweredController.Def>();
	}

	// Token: 0x0600106A RID: 4202 RVA: 0x000596E5 File Offset: 0x000578E5
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040008F0 RID: 2288
	public const string ID = "Telescope";

	// Token: 0x040008F1 RID: 2289
	public const float POINTS_PER_DAY = 2f;

	// Token: 0x040008F2 RID: 2290
	public const float MASS_PER_POINT = 2f;

	// Token: 0x040008F3 RID: 2291
	public const float CAPACITY = 30f;

	// Token: 0x040008F4 RID: 2292
	public static readonly Tag INPUT_MATERIAL = GameTags.Glass;
}
