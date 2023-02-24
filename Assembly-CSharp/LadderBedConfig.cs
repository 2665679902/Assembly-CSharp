using System;
using TUNING;
using UnityEngine;

// Token: 0x020001DE RID: 478
public class LadderBedConfig : IBuildingConfig
{
	// Token: 0x0600096A RID: 2410 RVA: 0x00036B5C File Offset: 0x00034D5C
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x00036B64 File Offset: 0x00034D64
	public override BuildingDef CreateBuildingDef()
	{
		string id = LadderBedConfig.ID;
		int num = 2;
		int num2 = 2;
		string text = "ladder_bed_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloorOrBuildingAttachPoint;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.AttachmentSlotTag = GameTags.LadderBed;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		return buildingDef;
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x00036BE4 File Offset: 0x00034DE4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.BedType, false);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 2), GameTags.LadderBed, null)
		};
		go.AddOrGet<AnimTileable>();
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x00036C3C File Offset: 0x00034E3C
	public override void DoPostConfigureComplete(GameObject go)
	{
		CellOffset[] array = new CellOffset[]
		{
			new CellOffset(0, 0),
			new CellOffset(0, 1)
		};
		Ladder ladder = go.AddOrGet<Ladder>();
		ladder.upwardsMovementSpeedMultiplier = 0.75f;
		ladder.downwardsMovementSpeedMultiplier = 0.75f;
		ladder.offsets = array;
		go.AddOrGetDef<LadderBed.Def>().offsets = array;
		go.GetComponent<KAnimControllerBase>().initialAnim = "off";
		Bed bed = go.AddOrGet<Bed>();
		bed.effects = new string[] { "LadderBedStamina", "BedHealth" };
		bed.workLayer = Grid.SceneLayer.BuildingFront;
		Sleepable sleepable = go.AddOrGet<Sleepable>();
		sleepable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_ladder_bed_kanim") };
		sleepable.workLayer = Grid.SceneLayer.BuildingFront;
		go.AddOrGet<Ownable>().slotID = Db.Get().AssignableSlots.Bed.Id;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x040005E4 RID: 1508
	public static string ID = "LadderBed";
}
