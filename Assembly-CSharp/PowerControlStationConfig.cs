using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x020002AB RID: 683
public class PowerControlStationConfig : IBuildingConfig
{
	// Token: 0x06000D8D RID: 3469 RVA: 0x0004B34C File Offset: 0x0004954C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "PowerControlStation";
		int num = 2;
		int num2 = 4;
		string text2 = "electricianworkdesk_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.ViewMode = OverlayModes.Rooms.ID;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		return buildingDef;
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x0004B3CC File Offset: 0x000495CC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.PowerStation, false);
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x0004B3E8 File Offset: 0x000495E8
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 50f;
		storage.showInUI = true;
		storage.storageFilters = new List<Tag> { PowerControlStationConfig.MATERIAL_FOR_TINKER };
		TinkerStation tinkerStation = go.AddOrGet<TinkerStation>();
		tinkerStation.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_electricianworkdesk_kanim") };
		tinkerStation.inputMaterial = PowerControlStationConfig.MATERIAL_FOR_TINKER;
		tinkerStation.massPerTinker = 5f;
		tinkerStation.outputPrefab = PowerControlStationConfig.TINKER_TOOLS;
		tinkerStation.outputTemperature = 308.15f;
		tinkerStation.requiredSkillPerk = PowerControlStationConfig.ROLE_PERK;
		tinkerStation.choreType = Db.Get().ChoreTypes.PowerFabricate.IdHash;
		tinkerStation.useFilteredStorage = true;
		tinkerStation.fetchChoreType = Db.Get().ChoreTypes.PowerFetch.IdHash;
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.PowerPlant.Id;
		roomTracker.requirement = RoomTracker.Requirement.Required;
		Prioritizable.AddRef(go);
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject game_object)
		{
			TinkerStation component = game_object.GetComponent<TinkerStation>();
			component.AttributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
			component.AttributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
			component.SkillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
			component.SkillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		};
	}

	// Token: 0x040007E0 RID: 2016
	public const string ID = "PowerControlStation";

	// Token: 0x040007E1 RID: 2017
	public static Tag MATERIAL_FOR_TINKER = GameTags.RefinedMetal;

	// Token: 0x040007E2 RID: 2018
	public static Tag TINKER_TOOLS = PowerStationToolsConfig.tag;

	// Token: 0x040007E3 RID: 2019
	public const float MASS_PER_TINKER = 5f;

	// Token: 0x040007E4 RID: 2020
	public static string ROLE_PERK = "CanPowerTinker";

	// Token: 0x040007E5 RID: 2021
	public const float OUTPUT_TEMPERATURE = 308.15f;
}
