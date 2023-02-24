using System;
using STRINGS;

// Token: 0x0200020D RID: 525
public class LogicGateMultiplexerConfig : LogicGateBaseConfig
{
	// Token: 0x06000A60 RID: 2656 RVA: 0x0003B28D File Offset: 0x0003948D
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.Multiplexer;
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0003B290 File Offset: 0x00039490
	protected override CellOffset[] InputPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				new CellOffset(-1, 3),
				new CellOffset(-1, 2),
				new CellOffset(-1, 1),
				new CellOffset(-1, 0)
			};
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000A62 RID: 2658 RVA: 0x0003B2D0 File Offset: 0x000394D0
	protected override CellOffset[] OutputPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				new CellOffset(1, 3)
			};
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0003B2E6 File Offset: 0x000394E6
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				new CellOffset(0, 0),
				new CellOffset(1, 0)
			};
		}
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x0003B30C File Offset: 0x0003950C
	protected override LogicGate.LogicGateDescriptions GetDescriptions()
	{
		return new LogicGate.LogicGateDescriptions
		{
			outputOne = new LogicGate.LogicGateDescriptions.Description
			{
				name = BUILDINGS.PREFABS.LOGICGATEMULTIPLEXER.OUTPUT_NAME,
				active = BUILDINGS.PREFABS.LOGICGATEMULTIPLEXER.OUTPUT_ACTIVE,
				inactive = BUILDINGS.PREFABS.LOGICGATEMULTIPLEXER.OUTPUT_INACTIVE
			}
		};
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x0003B359 File Offset: 0x00039559
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateMultiplexer", "logic_multiplexer_kanim", 3, 4);
	}

	// Token: 0x0400062D RID: 1581
	public const string ID = "LogicGateMultiplexer";
}
