using System;
using TUNING;
using UnityEngine;

// Token: 0x02000196 RID: 406
public class GasConduitBridgeConfig : IBuildingConfig
{
	// Token: 0x060007E1 RID: 2017 RVA: 0x0002E238 File Offset: 0x0002C438
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GasConduitBridge";
		int num = 3;
		int num2 = 1;
		string text2 = "utilitygasbridge_kanim";
		int num3 = 10;
		float num4 = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Conduit;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.ObjectLayer = ObjectLayer.GasConduitConnection;
		buildingDef.SceneLayer = Grid.SceneLayer.GasConduitBridges;
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.OutputConduitType = ConduitType.Gas;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.GasConduits.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.UtilityInputOffset = new CellOffset(-1, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(1, 0);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.GasVentIDs, buildingDef.PrefabID);
		return buildingDef;
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x0002E30F File Offset: 0x0002C50F
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<ConduitBridge>().type = ConduitType.Gas;
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x0002E338 File Offset: 0x0002C538
	public override void DoPostConfigureComplete(GameObject go)
	{
		UnityEngine.Object.DestroyImmediate(go.GetComponent<RequireInputs>());
		UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitConsumer>());
		UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitDispenser>());
	}

	// Token: 0x0400050B RID: 1291
	public const string ID = "GasConduitBridge";

	// Token: 0x0400050C RID: 1292
	private const ConduitType CONDUIT_TYPE = ConduitType.Gas;
}
