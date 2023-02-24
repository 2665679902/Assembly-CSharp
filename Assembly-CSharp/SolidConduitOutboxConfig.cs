using System;
using TUNING;
using UnityEngine;

// Token: 0x02000314 RID: 788
public class SolidConduitOutboxConfig : IBuildingConfig
{
	// Token: 0x06000FAF RID: 4015 RVA: 0x00055524 File Offset: 0x00053724
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SolidConduitOutbox";
		int num = 1;
		int num2 = 2;
		string text2 = "conveyorout_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.SolidConveyor.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.InputConduitType = ConduitType.Solid;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.PermittedRotations = PermittedRotations.R360;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SolidConveyorIDs, "SolidConduitOutbox");
		return buildingDef;
	}

	// Token: 0x06000FB0 RID: 4016 RVA: 0x000555B8 File Offset: 0x000537B8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		go.AddOrGet<SolidConduitOutbox>();
		go.AddOrGet<SolidConduitConsumer>();
		Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
		storage.capacityKg = 100f;
		storage.showInUI = true;
		storage.allowItemRemoval = true;
		go.AddOrGet<SimpleVent>();
	}

	// Token: 0x06000FB1 RID: 4017 RVA: 0x000555F4 File Offset: 0x000537F4
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.GetComponent<Constructable>().requiredSkillPerk = Db.Get().SkillPerks.ConveyorBuild.Id;
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x0005561C File Offset: 0x0005381C
	public override void DoPostConfigureComplete(GameObject go)
	{
		Prioritizable.AddRef(go);
		go.AddOrGet<Automatable>();
	}

	// Token: 0x04000898 RID: 2200
	public const string ID = "SolidConduitOutbox";
}
