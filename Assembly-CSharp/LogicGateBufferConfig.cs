using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200020B RID: 523
public class LogicGateBufferConfig : LogicGateBaseConfig
{
	// Token: 0x06000A4E RID: 2638 RVA: 0x0003B055 File Offset: 0x00039255
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.CustomSingle;
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0003B058 File Offset: 0x00039258
	protected override CellOffset[] InputPortOffsets
	{
		get
		{
			return new CellOffset[] { CellOffset.none };
		}
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0003B06C File Offset: 0x0003926C
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

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0003B082 File Offset: 0x00039282
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x0003B088 File Offset: 0x00039288
	protected override LogicGate.LogicGateDescriptions GetDescriptions()
	{
		return new LogicGate.LogicGateDescriptions
		{
			outputOne = new LogicGate.LogicGateDescriptions.Description
			{
				name = BUILDINGS.PREFABS.LOGICGATEBUFFER.OUTPUT_NAME,
				active = BUILDINGS.PREFABS.LOGICGATEBUFFER.OUTPUT_ACTIVE,
				inactive = BUILDINGS.PREFABS.LOGICGATEBUFFER.OUTPUT_INACTIVE
			}
		};
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x0003B0D5 File Offset: 0x000392D5
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateBUFFER", "logic_buffer_kanim", 2, 1);
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x0003B0EC File Offset: 0x000392EC
	public override void DoPostConfigureComplete(GameObject go)
	{
		LogicGateBuffer logicGateBuffer = go.AddComponent<LogicGateBuffer>();
		logicGateBuffer.op = this.GetLogicOp();
		logicGateBuffer.inputPortOffsets = this.InputPortOffsets;
		logicGateBuffer.outputPortOffsets = this.OutputPortOffsets;
		logicGateBuffer.controlPortOffsets = this.ControlPortOffsets;
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject game_object)
		{
			game_object.GetComponent<LogicGateBuffer>().SetPortDescriptions(this.GetDescriptions());
		};
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits, false);
	}

	// Token: 0x0400062B RID: 1579
	public const string ID = "LogicGateBUFFER";
}
