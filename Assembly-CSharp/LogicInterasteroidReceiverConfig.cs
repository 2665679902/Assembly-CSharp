using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000212 RID: 530
public class LogicInterasteroidReceiverConfig : IBuildingConfig
{
	// Token: 0x06000A83 RID: 2691 RVA: 0x0003B8ED File Offset: 0x00039AED
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x0003B8F4 File Offset: 0x00039AF4
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("LogicInterasteroidReceiver", 1, 1, "inter_asteroid_automation_signal_receiver_kanim", 30, 30f, TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER2, MATERIALS.REFINED_METALS, 1600f, BuildLocationRule.OnFloor, TUNING.BUILDINGS.DECOR.PENALTY.TIER0, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.Entombable = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.ViewMode = OverlayModes.Logic.ID;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.PermittedRotations = PermittedRotations.Unrotatable;
		buildingDef.AlwaysOperational = false;
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort("OutputPort", new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LOGICINTERASTEROIDRECEIVER.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.LOGICINTERASTEROIDRECEIVER.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICINTERASTEROIDRECEIVER.LOGIC_PORT_INACTIVE, true, false) };
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, "LogicInterasteroidReceiver");
		return buildingDef;
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x0003B9CF File Offset: 0x00039BCF
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x0003B9D1 File Offset: 0x00039BD1
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicBroadcastReceiver>().PORT_ID = "OutputPort";
	}

	// Token: 0x04000632 RID: 1586
	public const string ID = "LogicInterasteroidReceiver";

	// Token: 0x04000633 RID: 1587
	public const string OUTPUT_PORT_ID = "OutputPort";
}
