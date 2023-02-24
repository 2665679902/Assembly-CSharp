﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x020002E5 RID: 741
public class ResetSkillsStationConfig : IBuildingConfig
{
	// Token: 0x06000EAF RID: 3759 RVA: 0x0004F784 File Offset: 0x0004D984
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ResetSkillsStation";
		int num = 3;
		int num2 = 3;
		string text2 = "reSpeccer_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x06000EB0 RID: 3760 RVA: 0x0004F814 File Offset: 0x0004DA14
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddTag(GameTags.NotRoomAssignable);
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		go.AddOrGet<Ownable>().slotID = Db.Get().AssignableSlots.ResetSkillsStation.Id;
		ResetSkillsStation resetSkillsStation = go.AddOrGet<ResetSkillsStation>();
		resetSkillsStation.workTime = 180f;
		resetSkillsStation.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_reSpeccer_kanim") };
		resetSkillsStation.workLayer = Grid.SceneLayer.BuildingFront;
	}

	// Token: 0x06000EB1 RID: 3761 RVA: 0x0004F8A0 File Offset: 0x0004DAA0
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400081D RID: 2077
	public const string ID = "ResetSkillsStation";
}
