using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020005F2 RID: 1522
[AddComponentMenu("KMonoBehaviour/scripts/LogicOperationalController")]
public class LogicOperationalController : KMonoBehaviour
{
	// Token: 0x060026F9 RID: 9977 RVA: 0x000D1FB8 File Offset: 0x000D01B8
	public static List<LogicPorts.Port> CreateSingleInputPortList(CellOffset offset)
	{
		return new List<LogicPorts.Port> { LogicPorts.Port.InputPort(LogicOperationalController.PORT_ID, offset, UI.LOGIC_PORTS.CONTROL_OPERATIONAL, UI.LOGIC_PORTS.CONTROL_OPERATIONAL_ACTIVE, UI.LOGIC_PORTS.CONTROL_OPERATIONAL_INACTIVE, false, false) };
	}

	// Token: 0x060026FA RID: 9978 RVA: 0x000D1FFC File Offset: 0x000D01FC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<LogicOperationalController>(-801688580, LogicOperationalController.OnLogicValueChangedDelegate);
		if (LogicOperationalController.infoStatusItem == null)
		{
			LogicOperationalController.infoStatusItem = new StatusItem("LogicOperationalInfo", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			LogicOperationalController.infoStatusItem.resolveStringCallback = new Func<string, object, string>(LogicOperationalController.ResolveInfoStatusItemString);
		}
		this.CheckWireState();
	}

	// Token: 0x060026FB RID: 9979 RVA: 0x000D206C File Offset: 0x000D026C
	private LogicCircuitNetwork GetNetwork()
	{
		int portCell = base.GetComponent<LogicPorts>().GetPortCell(LogicOperationalController.PORT_ID);
		return Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
	}

	// Token: 0x060026FC RID: 9980 RVA: 0x000D209C File Offset: 0x000D029C
	private LogicCircuitNetwork CheckWireState()
	{
		LogicCircuitNetwork network = this.GetNetwork();
		int num = ((network != null) ? network.OutputValue : this.unNetworkedValue);
		this.operational.SetFlag(LogicOperationalController.LogicOperationalFlag, LogicCircuitNetwork.IsBitActive(0, num));
		return network;
	}

	// Token: 0x060026FD RID: 9981 RVA: 0x000D20DA File Offset: 0x000D02DA
	private static string ResolveInfoStatusItemString(string format_str, object data)
	{
		return ((LogicOperationalController)data).operational.GetFlag(LogicOperationalController.LogicOperationalFlag) ? BUILDING.STATUSITEMS.LOGIC.LOGIC_CONTROLLED_ENABLED : BUILDING.STATUSITEMS.LOGIC.LOGIC_CONTROLLED_DISABLED;
	}

	// Token: 0x060026FE RID: 9982 RVA: 0x000D2104 File Offset: 0x000D0304
	private void OnLogicValueChanged(object data)
	{
		if (((LogicValueChanged)data).portID == LogicOperationalController.PORT_ID)
		{
			LogicCircuitNetwork logicCircuitNetwork = this.CheckWireState();
			base.GetComponent<KSelectable>().ToggleStatusItem(LogicOperationalController.infoStatusItem, logicCircuitNetwork != null, this);
		}
	}

	// Token: 0x0400170A RID: 5898
	public static readonly HashedString PORT_ID = "LogicOperational";

	// Token: 0x0400170B RID: 5899
	public int unNetworkedValue = 1;

	// Token: 0x0400170C RID: 5900
	public static readonly Operational.Flag LogicOperationalFlag = new Operational.Flag("LogicOperational", Operational.Flag.Type.Requirement);

	// Token: 0x0400170D RID: 5901
	private static StatusItem infoStatusItem;

	// Token: 0x0400170E RID: 5902
	[MyCmpGet]
	public Operational operational;

	// Token: 0x0400170F RID: 5903
	private static readonly EventSystem.IntraObjectHandler<LogicOperationalController> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<LogicOperationalController>(delegate(LogicOperationalController component, object data)
	{
		component.OnLogicValueChanged(data);
	});
}
