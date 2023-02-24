using System;
using TUNING;
using UnityEngine;

// Token: 0x0200007F RID: 127
public class ExobaseHeadquartersConfig : IBuildingConfig
{
	// Token: 0x0600025C RID: 604 RVA: 0x0001125F File Offset: 0x0000F45F
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600025D RID: 605 RVA: 0x00011268 File Offset: 0x0000F468
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ExobaseHeadquarters";
		int num = 3;
		int num2 = 3;
		string text2 = "porta_pod_y_kanim";
		int num3 = 250;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] all_MINERALS = MATERIALS.ALL_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER5, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.BaseTimeUntilRepair = 400f;
		buildingDef.ShowInBuildMenu = true;
		buildingDef.DefaultAnimState = "idle";
		buildingDef.OnePerWorld = true;
		SoundEventVolumeCache.instance.AddVolume("hqbase_kanim", "Portal_LP", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("hqbase_kanim", "Portal_open", NOISE_POLLUTION.NOISY.TIER4);
		SoundEventVolumeCache.instance.AddVolume("hqbase_kanim", "Portal_close", NOISE_POLLUTION.NOISY.TIER4);
		return buildingDef;
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0001133C File Offset: 0x0000F53C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		LoreBearerUtil.AddLoreTo(go);
		Telepad telepad = go.AddOrGet<Telepad>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.Telepad, false);
		telepad.startingSkillPoints = 1f;
		SocialGatheringPoint socialGatheringPoint = go.AddOrGet<SocialGatheringPoint>();
		socialGatheringPoint.choreOffsets = new CellOffset[]
		{
			new CellOffset(-1, 0),
			new CellOffset(-2, 0),
			new CellOffset(2, 0),
			new CellOffset(3, 0),
			new CellOffset(0, 0),
			new CellOffset(1, 0)
		};
		socialGatheringPoint.choreCount = 4;
		socialGatheringPoint.basePriority = RELAXATION.PRIORITY.TIER0;
		Light2D light2D = go.AddOrGet<Light2D>();
		light2D.Color = LIGHT2D.HEADQUARTERS_COLOR;
		light2D.Range = 5f;
		light2D.Offset = LIGHT2D.EXOBASE_HEADQUARTERS_OFFSET;
		light2D.overlayColour = LIGHT2D.HEADQUARTERS_OVERLAYCOLOR;
		light2D.shape = global::LightShape.Circle;
		light2D.drawOverlay = true;
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.LightSource, false);
		go.GetComponent<KPrefabID>().AddTag(GameTags.Experimental, false);
		RoleStation roleStation = go.AddOrGet<RoleStation>();
		roleStation.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_hqbase_skill_upgrade_kanim") };
		roleStation.workAnims = new HashedString[] { "upgrade" };
		roleStation.workingPstComplete = null;
		roleStation.workingPstFailed = null;
		Activatable activatable = go.AddOrGet<Activatable>();
		activatable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_porta_pod_kanim") };
		activatable.workAnims = new HashedString[] { "activate_pre", "activate_loop" };
		activatable.workingPstComplete = new HashedString[] { "activate_pst" };
		activatable.workingPstFailed = new HashedString[] { "activate_pre" };
		activatable.SetWorkTime(15f);
	}

	// Token: 0x0600025F RID: 607 RVA: 0x00011533 File Offset: 0x0000F733
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400014C RID: 332
	public const string ID = "ExobaseHeadquarters";
}
