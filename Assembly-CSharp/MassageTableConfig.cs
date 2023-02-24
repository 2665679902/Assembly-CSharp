﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x0200022C RID: 556
public class MassageTableConfig : IBuildingConfig
{
	// Token: 0x06000AFB RID: 2811 RVA: 0x0003DEC0 File Offset: 0x0003C0C0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MassageTable";
		int num = 2;
		int num2 = 2;
		string text2 = "masseur_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.Overheatable = true;
		buildingDef.EnergyConsumptionWhenActive = 240f;
		buildingDef.ExhaustKilowattsWhenActive = 0.125f;
		buildingDef.SelfHeatKilowattsWhenActive = 0.5f;
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x0003DF50 File Offset: 0x0003C150
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.DeStressingBuilding, false);
		MassageTable massageTable = go.AddOrGet<MassageTable>();
		massageTable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_masseur_kanim") };
		massageTable.stressModificationValue = -30f;
		massageTable.roomStressModificationValue = -60f;
		massageTable.workLayer = Grid.SceneLayer.BuildingFront;
		Ownable ownable = go.AddOrGet<Ownable>();
		ownable.slotID = Db.Get().AssignableSlots.MassageTable.Id;
		ownable.canBePublic = true;
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.MassageClinic.Id;
		roomTracker.requirement = RoomTracker.Requirement.Recommended;
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x0003E004 File Offset: 0x0003C204
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KAnimControllerBase>().initialAnim = "off";
		go.AddOrGet<CopyBuildingSettings>();
	}

	// Token: 0x04000672 RID: 1650
	public const string ID = "MassageTable";
}
