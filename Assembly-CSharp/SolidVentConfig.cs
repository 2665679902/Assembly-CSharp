using System;
using TUNING;
using UnityEngine;

// Token: 0x0200031C RID: 796
public class SolidVentConfig : IBuildingConfig
{
	// Token: 0x06000FDA RID: 4058 RVA: 0x00055F08 File Offset: 0x00054108
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SolidVent";
		int num = 1;
		int num2 = 1;
		string text2 = "conveyer_dropper_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.InputConduitType = ConduitType.Solid;
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.SolidConveyor.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SolidConveyorIDs, "SolidVent");
		return buildingDef;
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x00055FB4 File Offset: 0x000541B4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LogicOperationalController>();
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x00055FBD File Offset: 0x000541BD
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.GetComponent<Constructable>().requiredSkillPerk = Db.Get().SkillPerks.ConveyorBuild.Id;
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x00055FE5 File Offset: 0x000541E5
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<SimpleVent>();
		go.AddOrGet<SolidConduitConsumer>();
		go.AddOrGet<SolidConduitDropper>();
		Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
		storage.capacityKg = 100f;
		storage.showInUI = true;
	}

	// Token: 0x040008A5 RID: 2213
	public const string ID = "SolidVent";

	// Token: 0x040008A6 RID: 2214
	private const ConduitType CONDUIT_TYPE = ConduitType.Solid;
}
