using System;
using STRINGS;

// Token: 0x02000208 RID: 520
public class LogicGateOrConfig : LogicGateBaseConfig
{
	// Token: 0x06000A39 RID: 2617 RVA: 0x0003AE69 File Offset: 0x00039069
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.Or;
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0003AE6C File Offset: 0x0003906C
	protected override CellOffset[] InputPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				CellOffset.none,
				new CellOffset(0, 1)
			};
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0003AE8E File Offset: 0x0003908E
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

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0003AEA4 File Offset: 0x000390A4
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x0003AEA8 File Offset: 0x000390A8
	protected override LogicGate.LogicGateDescriptions GetDescriptions()
	{
		return new LogicGate.LogicGateDescriptions
		{
			outputOne = new LogicGate.LogicGateDescriptions.Description
			{
				name = BUILDINGS.PREFABS.LOGICGATEOR.OUTPUT_NAME,
				active = BUILDINGS.PREFABS.LOGICGATEOR.OUTPUT_ACTIVE,
				inactive = BUILDINGS.PREFABS.LOGICGATEOR.OUTPUT_INACTIVE
			}
		};
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x0003AEF5 File Offset: 0x000390F5
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateOR", "logic_or_kanim", 2, 2);
	}

	// Token: 0x04000628 RID: 1576
	public const string ID = "LogicGateOR";
}
