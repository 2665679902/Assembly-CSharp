using System;
using TUNING;
using UnityEngine;

// Token: 0x02000082 RID: 130
public class FarmStationConfig : IBuildingConfig
{
	// Token: 0x06000269 RID: 617 RVA: 0x00011788 File Offset: 0x0000F988
	public override BuildingDef CreateBuildingDef()
	{
		string text = "FarmStation";
		int num = 2;
		int num2 = 3;
		string text2 = "planttender_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.ViewMode = OverlayModes.Rooms.ID;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		return buildingDef;
	}

	// Token: 0x0600026A RID: 618 RVA: 0x00011808 File Offset: 0x0000FA08
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.FarmStationType, false);
	}

	// Token: 0x0600026B RID: 619 RVA: 0x00011824 File Offset: 0x0000FA24
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		Storage storage = go.AddOrGet<Storage>();
		storage.showInUI = true;
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = FarmStationConfig.MATERIAL_FOR_TINKER;
		manualDeliveryKG.refillMass = 5f;
		manualDeliveryKG.capacity = 50f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FarmFetch.IdHash;
		TinkerStation tinkerStation = go.AddOrGet<TinkerStation>();
		tinkerStation.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_planttender_kanim") };
		tinkerStation.inputMaterial = FarmStationConfig.MATERIAL_FOR_TINKER;
		tinkerStation.massPerTinker = 5f;
		tinkerStation.outputPrefab = FarmStationConfig.TINKER_TOOLS;
		tinkerStation.outputTemperature = 308.15f;
		tinkerStation.requiredSkillPerk = Db.Get().SkillPerks.CanFarmTinker.Id;
		tinkerStation.choreType = Db.Get().ChoreTypes.FarmingFabricate.IdHash;
		tinkerStation.fetchChoreType = Db.Get().ChoreTypes.FarmFetch.IdHash;
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.Farm.Id;
		roomTracker.requirement = RoomTracker.Requirement.Required;
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject game_object)
		{
			TinkerStation component = game_object.GetComponent<TinkerStation>();
			component.AttributeConverter = Db.Get().AttributeConverters.HarvestSpeed;
			component.AttributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
			component.SkillExperienceSkillGroup = Db.Get().SkillGroups.Farming.Id;
			component.SkillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		};
	}

	// Token: 0x0400014F RID: 335
	public const string ID = "FarmStation";

	// Token: 0x04000150 RID: 336
	public static Tag MATERIAL_FOR_TINKER = GameTags.Fertilizer;

	// Token: 0x04000151 RID: 337
	public static Tag TINKER_TOOLS = FarmStationToolsConfig.tag;

	// Token: 0x04000152 RID: 338
	public const float MASS_PER_TINKER = 5f;

	// Token: 0x04000153 RID: 339
	public const float OUTPUT_TEMPERATURE = 308.15f;
}
