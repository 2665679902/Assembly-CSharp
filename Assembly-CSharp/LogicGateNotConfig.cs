using System;
using STRINGS;

// Token: 0x0200020A RID: 522
public class LogicGateNotConfig : LogicGateBaseConfig
{
	// Token: 0x06000A47 RID: 2631 RVA: 0x0003AFB9 File Offset: 0x000391B9
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.Not;
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0003AFBC File Offset: 0x000391BC
	protected override CellOffset[] InputPortOffsets
	{
		get
		{
			return new CellOffset[] { CellOffset.none };
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0003AFD0 File Offset: 0x000391D0
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

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0003AFE6 File Offset: 0x000391E6
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x0003AFEC File Offset: 0x000391EC
	protected override LogicGate.LogicGateDescriptions GetDescriptions()
	{
		return new LogicGate.LogicGateDescriptions
		{
			outputOne = new LogicGate.LogicGateDescriptions.Description
			{
				name = BUILDINGS.PREFABS.LOGICGATENOT.OUTPUT_NAME,
				active = BUILDINGS.PREFABS.LOGICGATENOT.OUTPUT_ACTIVE,
				inactive = BUILDINGS.PREFABS.LOGICGATENOT.OUTPUT_INACTIVE
			}
		};
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x0003B039 File Offset: 0x00039239
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateNOT", "logic_not_kanim", 2, 1);
	}

	// Token: 0x0400062A RID: 1578
	public const string ID = "LogicGateNOT";
}
