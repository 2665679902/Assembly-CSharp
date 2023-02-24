using System;
using UnityEngine;

// Token: 0x020005E8 RID: 1512
[AddComponentMenu("KMonoBehaviour/scripts/LogicGateBase")]
public class LogicGateBase : KMonoBehaviour
{
	// Token: 0x0600263E RID: 9790 RVA: 0x000CDF68 File Offset: 0x000CC168
	private int GetActualCell(CellOffset offset)
	{
		Rotatable component = base.GetComponent<Rotatable>();
		if (component != null)
		{
			offset = component.GetRotatedCellOffset(offset);
		}
		return Grid.OffsetCell(Grid.PosToCell(base.transform.GetPosition()), offset);
	}

	// Token: 0x17000223 RID: 547
	// (get) Token: 0x0600263F RID: 9791 RVA: 0x000CDFA4 File Offset: 0x000CC1A4
	public int InputCellOne
	{
		get
		{
			return this.GetActualCell(this.inputPortOffsets[0]);
		}
	}

	// Token: 0x17000224 RID: 548
	// (get) Token: 0x06002640 RID: 9792 RVA: 0x000CDFB8 File Offset: 0x000CC1B8
	public int InputCellTwo
	{
		get
		{
			return this.GetActualCell(this.inputPortOffsets[1]);
		}
	}

	// Token: 0x17000225 RID: 549
	// (get) Token: 0x06002641 RID: 9793 RVA: 0x000CDFCC File Offset: 0x000CC1CC
	public int InputCellThree
	{
		get
		{
			return this.GetActualCell(this.inputPortOffsets[2]);
		}
	}

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x06002642 RID: 9794 RVA: 0x000CDFE0 File Offset: 0x000CC1E0
	public int InputCellFour
	{
		get
		{
			return this.GetActualCell(this.inputPortOffsets[3]);
		}
	}

	// Token: 0x17000227 RID: 551
	// (get) Token: 0x06002643 RID: 9795 RVA: 0x000CDFF4 File Offset: 0x000CC1F4
	public int OutputCellOne
	{
		get
		{
			return this.GetActualCell(this.outputPortOffsets[0]);
		}
	}

	// Token: 0x17000228 RID: 552
	// (get) Token: 0x06002644 RID: 9796 RVA: 0x000CE008 File Offset: 0x000CC208
	public int OutputCellTwo
	{
		get
		{
			return this.GetActualCell(this.outputPortOffsets[1]);
		}
	}

	// Token: 0x17000229 RID: 553
	// (get) Token: 0x06002645 RID: 9797 RVA: 0x000CE01C File Offset: 0x000CC21C
	public int OutputCellThree
	{
		get
		{
			return this.GetActualCell(this.outputPortOffsets[2]);
		}
	}

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x06002646 RID: 9798 RVA: 0x000CE030 File Offset: 0x000CC230
	public int OutputCellFour
	{
		get
		{
			return this.GetActualCell(this.outputPortOffsets[3]);
		}
	}

	// Token: 0x1700022B RID: 555
	// (get) Token: 0x06002647 RID: 9799 RVA: 0x000CE044 File Offset: 0x000CC244
	public int ControlCellOne
	{
		get
		{
			return this.GetActualCell(this.controlPortOffsets[0]);
		}
	}

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x06002648 RID: 9800 RVA: 0x000CE058 File Offset: 0x000CC258
	public int ControlCellTwo
	{
		get
		{
			return this.GetActualCell(this.controlPortOffsets[1]);
		}
	}

	// Token: 0x06002649 RID: 9801 RVA: 0x000CE06C File Offset: 0x000CC26C
	public int PortCell(LogicGateBase.PortId port)
	{
		switch (port)
		{
		case LogicGateBase.PortId.InputOne:
			return this.InputCellOne;
		case LogicGateBase.PortId.InputTwo:
			return this.InputCellTwo;
		case LogicGateBase.PortId.InputThree:
			return this.InputCellThree;
		case LogicGateBase.PortId.InputFour:
			return this.InputCellFour;
		case LogicGateBase.PortId.OutputOne:
			return this.OutputCellOne;
		case LogicGateBase.PortId.OutputTwo:
			return this.OutputCellTwo;
		case LogicGateBase.PortId.OutputThree:
			return this.OutputCellThree;
		case LogicGateBase.PortId.OutputFour:
			return this.OutputCellFour;
		case LogicGateBase.PortId.ControlOne:
			return this.ControlCellOne;
		case LogicGateBase.PortId.ControlTwo:
			return this.ControlCellTwo;
		default:
			return this.OutputCellOne;
		}
	}

	// Token: 0x0600264A RID: 9802 RVA: 0x000CE0F8 File Offset: 0x000CC2F8
	public bool TryGetPortAtCell(int cell, out LogicGateBase.PortId port)
	{
		if (cell == this.InputCellOne)
		{
			port = LogicGateBase.PortId.InputOne;
			return true;
		}
		if ((this.RequiresTwoInputs || this.RequiresFourInputs) && cell == this.InputCellTwo)
		{
			port = LogicGateBase.PortId.InputTwo;
			return true;
		}
		if (this.RequiresFourInputs && cell == this.InputCellThree)
		{
			port = LogicGateBase.PortId.InputThree;
			return true;
		}
		if (this.RequiresFourInputs && cell == this.InputCellFour)
		{
			port = LogicGateBase.PortId.InputFour;
			return true;
		}
		if (cell == this.OutputCellOne)
		{
			port = LogicGateBase.PortId.OutputOne;
			return true;
		}
		if (this.RequiresFourOutputs && cell == this.OutputCellTwo)
		{
			port = LogicGateBase.PortId.OutputTwo;
			return true;
		}
		if (this.RequiresFourOutputs && cell == this.OutputCellThree)
		{
			port = LogicGateBase.PortId.OutputThree;
			return true;
		}
		if (this.RequiresFourOutputs && cell == this.OutputCellFour)
		{
			port = LogicGateBase.PortId.OutputFour;
			return true;
		}
		if (this.RequiresControlInputs && cell == this.ControlCellOne)
		{
			port = LogicGateBase.PortId.ControlOne;
			return true;
		}
		if (this.RequiresControlInputs && cell == this.ControlCellTwo)
		{
			port = LogicGateBase.PortId.ControlTwo;
			return true;
		}
		port = LogicGateBase.PortId.InputOne;
		return false;
	}

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x0600264B RID: 9803 RVA: 0x000CE1DE File Offset: 0x000CC3DE
	public bool RequiresTwoInputs
	{
		get
		{
			return LogicGateBase.OpRequiresTwoInputs(this.op);
		}
	}

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x0600264C RID: 9804 RVA: 0x000CE1EB File Offset: 0x000CC3EB
	public bool RequiresFourInputs
	{
		get
		{
			return LogicGateBase.OpRequiresFourInputs(this.op);
		}
	}

	// Token: 0x1700022F RID: 559
	// (get) Token: 0x0600264D RID: 9805 RVA: 0x000CE1F8 File Offset: 0x000CC3F8
	public bool RequiresFourOutputs
	{
		get
		{
			return LogicGateBase.OpRequiresFourOutputs(this.op);
		}
	}

	// Token: 0x17000230 RID: 560
	// (get) Token: 0x0600264E RID: 9806 RVA: 0x000CE205 File Offset: 0x000CC405
	public bool RequiresControlInputs
	{
		get
		{
			return LogicGateBase.OpRequiresControlInputs(this.op);
		}
	}

	// Token: 0x0600264F RID: 9807 RVA: 0x000CE212 File Offset: 0x000CC412
	public static bool OpRequiresTwoInputs(LogicGateBase.Op op)
	{
		return op != LogicGateBase.Op.Not && op - LogicGateBase.Op.CustomSingle > 2;
	}

	// Token: 0x06002650 RID: 9808 RVA: 0x000CE221 File Offset: 0x000CC421
	public static bool OpRequiresFourInputs(LogicGateBase.Op op)
	{
		return op == LogicGateBase.Op.Multiplexer;
	}

	// Token: 0x06002651 RID: 9809 RVA: 0x000CE22A File Offset: 0x000CC42A
	public static bool OpRequiresFourOutputs(LogicGateBase.Op op)
	{
		return op == LogicGateBase.Op.Demultiplexer;
	}

	// Token: 0x06002652 RID: 9810 RVA: 0x000CE233 File Offset: 0x000CC433
	public static bool OpRequiresControlInputs(LogicGateBase.Op op)
	{
		return op - LogicGateBase.Op.Multiplexer <= 1;
	}

	// Token: 0x04001655 RID: 5717
	public static LogicModeUI uiSrcData;

	// Token: 0x04001656 RID: 5718
	public static readonly HashedString OUTPUT_TWO_PORT_ID = new HashedString("LogicGateOutputTwo");

	// Token: 0x04001657 RID: 5719
	public static readonly HashedString OUTPUT_THREE_PORT_ID = new HashedString("LogicGateOutputThree");

	// Token: 0x04001658 RID: 5720
	public static readonly HashedString OUTPUT_FOUR_PORT_ID = new HashedString("LogicGateOutputFour");

	// Token: 0x04001659 RID: 5721
	[SerializeField]
	public LogicGateBase.Op op;

	// Token: 0x0400165A RID: 5722
	public static CellOffset[] portOffsets = new CellOffset[]
	{
		CellOffset.none,
		new CellOffset(0, 1),
		new CellOffset(1, 0)
	};

	// Token: 0x0400165B RID: 5723
	public CellOffset[] inputPortOffsets;

	// Token: 0x0400165C RID: 5724
	public CellOffset[] outputPortOffsets;

	// Token: 0x0400165D RID: 5725
	public CellOffset[] controlPortOffsets;

	// Token: 0x02001247 RID: 4679
	public enum PortId
	{
		// Token: 0x04005D6F RID: 23919
		InputOne,
		// Token: 0x04005D70 RID: 23920
		InputTwo,
		// Token: 0x04005D71 RID: 23921
		InputThree,
		// Token: 0x04005D72 RID: 23922
		InputFour,
		// Token: 0x04005D73 RID: 23923
		OutputOne,
		// Token: 0x04005D74 RID: 23924
		OutputTwo,
		// Token: 0x04005D75 RID: 23925
		OutputThree,
		// Token: 0x04005D76 RID: 23926
		OutputFour,
		// Token: 0x04005D77 RID: 23927
		ControlOne,
		// Token: 0x04005D78 RID: 23928
		ControlTwo
	}

	// Token: 0x02001248 RID: 4680
	public enum Op
	{
		// Token: 0x04005D7A RID: 23930
		And,
		// Token: 0x04005D7B RID: 23931
		Or,
		// Token: 0x04005D7C RID: 23932
		Not,
		// Token: 0x04005D7D RID: 23933
		Xor,
		// Token: 0x04005D7E RID: 23934
		CustomSingle,
		// Token: 0x04005D7F RID: 23935
		Multiplexer,
		// Token: 0x04005D80 RID: 23936
		Demultiplexer
	}
}
