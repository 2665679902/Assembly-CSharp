using System;
using TUNING;
using UnityEngine;

// Token: 0x020001EE RID: 494
public class LiquidConduitOverflowConfig : IBuildingConfig
{
	// Token: 0x060009C1 RID: 2497 RVA: 0x000387B8 File Offset: 0x000369B8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LiquidConduitOverflow";
		int num = 2;
		int num2 = 2;
		string text2 = "valveliquid_kanim";
		int num3 = 10;
		float num4 = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Conduit;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.Deprecated = true;
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(1, 0);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.LiquidVentIDs, buildingDef.PrefabID);
		return buildingDef;
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x00038880 File Offset: 0x00036A80
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		this.AttachPort(go);
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x00038891 File Offset: 0x00036A91
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		this.AttachPort(go);
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x000388A1 File Offset: 0x00036AA1
	private void AttachPort(GameObject go)
	{
		go.AddComponent<ConduitSecondaryOutput>().portInfo = this.secondaryPort;
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x000388B4 File Offset: 0x00036AB4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<ConduitOverflow>().portInfo = this.secondaryPort;
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x000388E2 File Offset: 0x00036AE2
	public override void DoPostConfigureComplete(GameObject go)
	{
		UnityEngine.Object.DestroyImmediate(go.GetComponent<RequireInputs>());
		UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitConsumer>());
		UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitDispenser>());
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x040005FB RID: 1531
	public const string ID = "LiquidConduitOverflow";

	// Token: 0x040005FC RID: 1532
	private const ConduitType CONDUIT_TYPE = ConduitType.Liquid;

	// Token: 0x040005FD RID: 1533
	private ConduitPortInfo secondaryPort = new ConduitPortInfo(ConduitType.Liquid, new CellOffset(1, 1));
}
