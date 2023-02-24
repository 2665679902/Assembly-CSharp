using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000213 RID: 531
public class LogicInterasteroidSenderConfig : IBuildingConfig
{
	// Token: 0x06000A88 RID: 2696 RVA: 0x0003B9EB File Offset: 0x00039BEB
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x0003B9F4 File Offset: 0x00039BF4
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("LogicInterasteroidSender", 1, 1, "inter_asteroid_automation_signal_sender_kanim", 30, 30f, TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER2, MATERIALS.REFINED_METALS, 1600f, BuildLocationRule.OnFloor, TUNING.BUILDINGS.DECOR.PENALTY.TIER0, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.Entombable = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.ViewMode = OverlayModes.Logic.ID;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.PermittedRotations = PermittedRotations.Unrotatable;
		buildingDef.AlwaysOperational = false;
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.InputPort("InputPort", new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LOGICDUPLICANTSENSOR.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.LOGICINTERASTEROIDSENDER.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICINTERASTEROIDSENDER.LOGIC_PORT_INACTIVE, true, false) };
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, "LogicInterasteroidSender");
		return buildingDef;
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x0003BACF File Offset: 0x00039CCF
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		base.ConfigureBuildingTemplate(go, prefab_tag);
		go.AddOrGet<UserNameable>().savedName = STRINGS.BUILDINGS.PREFABS.LOGICINTERASTEROIDSENDER.DEFAULTNAME;
		go.AddOrGet<LogicBroadcaster>().PORT_ID = "InputPort";
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x0003BAFE File Offset: 0x00039CFE
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x0003BB00 File Offset: 0x00039D00
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000634 RID: 1588
	public const string ID = "LogicInterasteroidSender";

	// Token: 0x04000635 RID: 1589
	public const string INPUT_PORT_ID = "InputPort";
}
