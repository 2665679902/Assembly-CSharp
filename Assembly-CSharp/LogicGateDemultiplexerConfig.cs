using System;
using STRINGS;

// Token: 0x0200020E RID: 526
public class LogicGateDemultiplexerConfig : LogicGateBaseConfig
{
	// Token: 0x06000A67 RID: 2663 RVA: 0x0003B375 File Offset: 0x00039575
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.Demultiplexer;
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000A68 RID: 2664 RVA: 0x0003B378 File Offset: 0x00039578
	protected override CellOffset[] InputPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				new CellOffset(-1, 3)
			};
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0003B38E File Offset: 0x0003958E
	protected override CellOffset[] OutputPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				new CellOffset(1, 3),
				new CellOffset(1, 2),
				new CellOffset(1, 1),
				new CellOffset(1, 0)
			};
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0003B3CE File Offset: 0x000395CE
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				new CellOffset(-1, 0),
				new CellOffset(0, 0)
			};
		}
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x0003B3F4 File Offset: 0x000395F4
	protected override LogicGate.LogicGateDescriptions GetDescriptions()
	{
		return new LogicGate.LogicGateDescriptions
		{
			outputOne = new LogicGate.LogicGateDescriptions.Description
			{
				name = BUILDINGS.PREFABS.LOGICGATEXOR.OUTPUT_NAME,
				active = BUILDINGS.PREFABS.LOGICGATEXOR.OUTPUT_ACTIVE,
				inactive = BUILDINGS.PREFABS.LOGICGATEXOR.OUTPUT_INACTIVE
			}
		};
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x0003B441 File Offset: 0x00039641
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateDemultiplexer", "logic_demultiplexer_kanim", 3, 4);
	}

	// Token: 0x0400062E RID: 1582
	public const string ID = "LogicGateDemultiplexer";
}
