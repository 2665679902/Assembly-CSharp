﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x02000324 RID: 804
public class StaterpillarLiquidConnectorConfig : IBuildingConfig
{
	// Token: 0x06001008 RID: 4104 RVA: 0x000569C9 File Offset: 0x00054BC9
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x000569D0 File Offset: 0x00054BD0
	public override BuildingDef CreateBuildingDef()
	{
		string id = StaterpillarLiquidConnectorConfig.ID;
		int num = 1;
		int num2 = 2;
		string text = "egg_caterpillar_kanim";
		int num3 = 1000;
		float num4 = 10f;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] array = all_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFoundationRotatable;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.OverheatTemperature = 423.15f;
		buildingDef.PermittedRotations = PermittedRotations.FlipV;
		buildingDef.ViewMode = OverlayModes.GasConduits.ID;
		buildingDef.AudioCategory = "Plastic";
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.UtilityOutputOffset = new CellOffset(0, 1);
		buildingDef.PlayConstructionSounds = false;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x00056A74 File Offset: 0x00054C74
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x00056A8C File Offset: 0x00054C8C
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<Storage>();
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.alwaysDispense = true;
		conduitDispenser.elementFilter = null;
		conduitDispenser.isOn = false;
		go.GetComponent<Deconstructable>().SetAllowDeconstruction(false);
		go.GetComponent<KSelectable>().IsSelectable = false;
	}

	// Token: 0x040008C3 RID: 2243
	public static readonly string ID = "StaterpillarLiquidConnector";

	// Token: 0x040008C4 RID: 2244
	private const int WIDTH = 1;

	// Token: 0x040008C5 RID: 2245
	private const int HEIGHT = 2;
}
