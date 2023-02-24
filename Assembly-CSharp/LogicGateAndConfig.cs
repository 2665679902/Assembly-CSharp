using System;
using STRINGS;

// Token: 0x02000207 RID: 519
public class LogicGateAndConfig : LogicGateBaseConfig
{
	// Token: 0x06000A32 RID: 2610 RVA: 0x0003ADC2 File Offset: 0x00038FC2
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.And;
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0003ADC5 File Offset: 0x00038FC5
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

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0003ADE7 File Offset: 0x00038FE7
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

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0003ADFD File Offset: 0x00038FFD
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x0003AE00 File Offset: 0x00039000
	protected override LogicGate.LogicGateDescriptions GetDescriptions()
	{
		return new LogicGate.LogicGateDescriptions
		{
			outputOne = new LogicGate.LogicGateDescriptions.Description
			{
				name = BUILDINGS.PREFABS.LOGICGATEAND.OUTPUT_NAME,
				active = BUILDINGS.PREFABS.LOGICGATEAND.OUTPUT_ACTIVE,
				inactive = BUILDINGS.PREFABS.LOGICGATEAND.OUTPUT_INACTIVE
			}
		};
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x0003AE4D File Offset: 0x0003904D
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateAND", "logic_and_kanim", 2, 2);
	}

	// Token: 0x04000627 RID: 1575
	public const string ID = "LogicGateAND";
}
