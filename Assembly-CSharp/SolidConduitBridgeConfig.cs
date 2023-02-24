using System;
using TUNING;
using UnityEngine;

// Token: 0x02000311 RID: 785
public class SolidConduitBridgeConfig : IBuildingConfig
{
	// Token: 0x06000FA1 RID: 4001 RVA: 0x000550C8 File Offset: 0x000532C8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SolidConduitBridge";
		int num = 3;
		int num2 = 1;
		string text2 = "utilities_conveyorbridge_kanim";
		int num3 = 10;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Conduit;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.ObjectLayer = ObjectLayer.SolidConduitConnection;
		buildingDef.SceneLayer = Grid.SceneLayer.SolidConduitBridges;
		buildingDef.InputConduitType = ConduitType.Solid;
		buildingDef.OutputConduitType = ConduitType.Solid;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.SolidConveyor.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.UtilityInputOffset = new CellOffset(-1, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(1, 0);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SolidConveyorIDs, "SolidConduitBridge");
		return buildingDef;
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x0005519D File Offset: 0x0005339D
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x000551BA File Offset: 0x000533BA
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.GetComponent<Constructable>().requiredSkillPerk = Db.Get().SkillPerks.ConveyorBuild.Id;
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x000551E2 File Offset: 0x000533E2
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<SolidConduitBridge>();
	}

	// Token: 0x04000894 RID: 2196
	public const string ID = "SolidConduitBridge";

	// Token: 0x04000895 RID: 2197
	private const ConduitType CONDUIT_TYPE = ConduitType.Solid;
}
