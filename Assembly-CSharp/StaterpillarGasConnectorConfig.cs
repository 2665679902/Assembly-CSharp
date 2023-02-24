using System;
using TUNING;
using UnityEngine;

// Token: 0x02000323 RID: 803
public class StaterpillarGasConnectorConfig : IBuildingConfig
{
	// Token: 0x06001002 RID: 4098 RVA: 0x000568A4 File Offset: 0x00054AA4
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x000568AC File Offset: 0x00054AAC
	public override BuildingDef CreateBuildingDef()
	{
		string id = StaterpillarGasConnectorConfig.ID;
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
		buildingDef.OutputConduitType = ConduitType.Gas;
		buildingDef.UtilityOutputOffset = new CellOffset(0, 1);
		buildingDef.PlayConstructionSounds = false;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x00056950 File Offset: 0x00054B50
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x00056968 File Offset: 0x00054B68
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<Storage>();
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Gas;
		conduitDispenser.alwaysDispense = true;
		conduitDispenser.elementFilter = null;
		conduitDispenser.isOn = false;
		go.GetComponent<Deconstructable>().SetAllowDeconstruction(false);
		go.GetComponent<KSelectable>().IsSelectable = false;
	}

	// Token: 0x040008C0 RID: 2240
	public static readonly string ID = "StaterpillarGasConnector";

	// Token: 0x040008C1 RID: 2241
	private const int WIDTH = 1;

	// Token: 0x040008C2 RID: 2242
	private const int HEIGHT = 2;
}
