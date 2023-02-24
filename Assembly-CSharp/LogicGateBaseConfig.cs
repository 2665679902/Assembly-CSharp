using System;
using TUNING;
using UnityEngine;

// Token: 0x0200020F RID: 527
public abstract class LogicGateBaseConfig : IBuildingConfig
{
	// Token: 0x06000A6E RID: 2670 RVA: 0x0003B460 File Offset: 0x00039660
	protected BuildingDef CreateBuildingDef(string ID, string anim, int width = 2, int height = 2)
	{
		int num = 10;
		float num2 = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num3 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, width, height, anim, num, num2, tier, refined_METALS, num3, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
		buildingDef.ViewMode = OverlayModes.Logic.ID;
		buildingDef.ObjectLayer = ObjectLayer.LogicGate;
		buildingDef.SceneLayer = Grid.SceneLayer.LogicGates;
		buildingDef.ThermalConductivity = 0.05f;
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.Entombable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.DragBuild = true;
		LogicGateBase.uiSrcData = Assets.instance.logicModeUIData;
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, ID);
		return buildingDef;
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000A6F RID: 2671
	protected abstract CellOffset[] InputPortOffsets { get; }

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x06000A70 RID: 2672
	protected abstract CellOffset[] OutputPortOffsets { get; }

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x06000A71 RID: 2673
	protected abstract CellOffset[] ControlPortOffsets { get; }

	// Token: 0x06000A72 RID: 2674
	protected abstract LogicGateBase.Op GetLogicOp();

	// Token: 0x06000A73 RID: 2675
	protected abstract LogicGate.LogicGateDescriptions GetDescriptions();

	// Token: 0x06000A74 RID: 2676 RVA: 0x0003B523 File Offset: 0x00039723
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0003B540 File Offset: 0x00039740
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		MoveableLogicGateVisualizer moveableLogicGateVisualizer = go.AddComponent<MoveableLogicGateVisualizer>();
		moveableLogicGateVisualizer.op = this.GetLogicOp();
		moveableLogicGateVisualizer.inputPortOffsets = this.InputPortOffsets;
		moveableLogicGateVisualizer.outputPortOffsets = this.OutputPortOffsets;
		moveableLogicGateVisualizer.controlPortOffsets = this.ControlPortOffsets;
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x0003B57F File Offset: 0x0003977F
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		LogicGateVisualizer logicGateVisualizer = go.AddComponent<LogicGateVisualizer>();
		logicGateVisualizer.op = this.GetLogicOp();
		logicGateVisualizer.inputPortOffsets = this.InputPortOffsets;
		logicGateVisualizer.outputPortOffsets = this.OutputPortOffsets;
		logicGateVisualizer.controlPortOffsets = this.ControlPortOffsets;
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0003B5C0 File Offset: 0x000397C0
	public override void DoPostConfigureComplete(GameObject go)
	{
		LogicGate logicGate = go.AddComponent<LogicGate>();
		logicGate.op = this.GetLogicOp();
		logicGate.inputPortOffsets = this.InputPortOffsets;
		logicGate.outputPortOffsets = this.OutputPortOffsets;
		logicGate.controlPortOffsets = this.ControlPortOffsets;
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject game_object)
		{
			game_object.GetComponent<LogicGate>().SetPortDescriptions(this.GetDescriptions());
		};
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits, false);
	}
}
