using System;
using STRINGS;

// Token: 0x02000209 RID: 521
public class LogicGateXorConfig : LogicGateBaseConfig
{
	// Token: 0x06000A40 RID: 2624 RVA: 0x0003AF11 File Offset: 0x00039111
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.Xor;
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0003AF14 File Offset: 0x00039114
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

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0003AF36 File Offset: 0x00039136
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

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0003AF4C File Offset: 0x0003914C
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x0003AF50 File Offset: 0x00039150
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

	// Token: 0x06000A45 RID: 2629 RVA: 0x0003AF9D File Offset: 0x0003919D
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateXOR", "logic_xor_kanim", 2, 2);
	}

	// Token: 0x04000629 RID: 1577
	public const string ID = "LogicGateXOR";
}
