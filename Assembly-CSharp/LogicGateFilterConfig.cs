using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200020C RID: 524
public class LogicGateFilterConfig : LogicGateBaseConfig
{
	// Token: 0x06000A57 RID: 2647 RVA: 0x0003B171 File Offset: 0x00039371
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.CustomSingle;
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0003B174 File Offset: 0x00039374
	protected override CellOffset[] InputPortOffsets
	{
		get
		{
			return new CellOffset[] { CellOffset.none };
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0003B188 File Offset: 0x00039388
	protected override CellOffset[] OutputPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				new CellOffset(1, 0)
			};
		}
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0003B19E File Offset: 0x0003939E
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x0003B1A4 File Offset: 0x000393A4
	protected override LogicGate.LogicGateDescriptions GetDescriptions()
	{
		return new LogicGate.LogicGateDescriptions
		{
			outputOne = new LogicGate.LogicGateDescriptions.Description
			{
				name = BUILDINGS.PREFABS.LOGICGATEFILTER.OUTPUT_NAME,
				active = BUILDINGS.PREFABS.LOGICGATEFILTER.OUTPUT_ACTIVE,
				inactive = BUILDINGS.PREFABS.LOGICGATEFILTER.OUTPUT_INACTIVE
			}
		};
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x0003B1F1 File Offset: 0x000393F1
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateFILTER", "logic_filter_kanim", 2, 1);
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x0003B208 File Offset: 0x00039408
	public override void DoPostConfigureComplete(GameObject go)
	{
		LogicGateFilter logicGateFilter = go.AddComponent<LogicGateFilter>();
		logicGateFilter.op = this.GetLogicOp();
		logicGateFilter.inputPortOffsets = this.InputPortOffsets;
		logicGateFilter.outputPortOffsets = this.OutputPortOffsets;
		logicGateFilter.controlPortOffsets = this.ControlPortOffsets;
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject game_object)
		{
			game_object.GetComponent<LogicGateFilter>().SetPortDescriptions(this.GetDescriptions());
		};
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits, false);
	}

	// Token: 0x0400062C RID: 1580
	public const string ID = "LogicGateFILTER";
}
