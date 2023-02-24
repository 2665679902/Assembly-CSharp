using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

// Token: 0x020007FF RID: 2047
public class LogicCircuitManager
{
	// Token: 0x06003B23 RID: 15139 RVA: 0x00148468 File Offset: 0x00146668
	public LogicCircuitManager(UtilityNetworkManager<LogicCircuitNetwork, LogicWire> conduit_system)
	{
		this.conduitSystem = conduit_system;
		this.timeSinceBridgeRefresh = 0f;
		this.elapsedTime = 0f;
		for (int i = 0; i < 2; i++)
		{
			this.bridgeGroups[i] = new List<LogicUtilityNetworkLink>();
		}
	}

	// Token: 0x06003B24 RID: 15140 RVA: 0x001484C8 File Offset: 0x001466C8
	public void RenderEveryTick(float dt)
	{
		this.Refresh(dt);
	}

	// Token: 0x06003B25 RID: 15141 RVA: 0x001484D4 File Offset: 0x001466D4
	private void Refresh(float dt)
	{
		if (this.conduitSystem.IsDirty)
		{
			this.conduitSystem.Update();
			LogicCircuitNetwork.logicSoundRegister.Clear();
			this.PropagateSignals(true);
			this.elapsedTime = 0f;
		}
		else if (SpeedControlScreen.Instance != null && !SpeedControlScreen.Instance.IsPaused)
		{
			this.elapsedTime += dt;
			this.timeSinceBridgeRefresh += dt;
			while (this.elapsedTime > LogicCircuitManager.ClockTickInterval)
			{
				this.elapsedTime -= LogicCircuitManager.ClockTickInterval;
				this.PropagateSignals(false);
				if (this.onLogicTick != null)
				{
					this.onLogicTick();
				}
			}
			if (this.timeSinceBridgeRefresh > LogicCircuitManager.BridgeRefreshInterval)
			{
				this.UpdateCircuitBridgeLists();
				this.timeSinceBridgeRefresh = 0f;
			}
		}
		foreach (UtilityNetwork utilityNetwork in Game.Instance.logicCircuitSystem.GetNetworks())
		{
			LogicCircuitNetwork logicCircuitNetwork = (LogicCircuitNetwork)utilityNetwork;
			this.CheckCircuitOverloaded(dt, logicCircuitNetwork.id, logicCircuitNetwork.GetBitsUsed());
		}
	}

	// Token: 0x06003B26 RID: 15142 RVA: 0x00148608 File Offset: 0x00146808
	private void PropagateSignals(bool force_send_events)
	{
		IList<UtilityNetwork> networks = Game.Instance.logicCircuitSystem.GetNetworks();
		foreach (UtilityNetwork utilityNetwork in networks)
		{
			((LogicCircuitNetwork)utilityNetwork).UpdateLogicValue();
		}
		foreach (UtilityNetwork utilityNetwork2 in networks)
		{
			LogicCircuitNetwork logicCircuitNetwork = (LogicCircuitNetwork)utilityNetwork2;
			logicCircuitNetwork.SendLogicEvents(force_send_events, logicCircuitNetwork.id);
		}
	}

	// Token: 0x06003B27 RID: 15143 RVA: 0x001486A4 File Offset: 0x001468A4
	public LogicCircuitNetwork GetNetworkForCell(int cell)
	{
		return this.conduitSystem.GetNetworkForCell(cell) as LogicCircuitNetwork;
	}

	// Token: 0x06003B28 RID: 15144 RVA: 0x001486B7 File Offset: 0x001468B7
	public void AddVisElem(ILogicUIElement elem)
	{
		this.uiVisElements.Add(elem);
		if (this.onElemAdded != null)
		{
			this.onElemAdded(elem);
		}
	}

	// Token: 0x06003B29 RID: 15145 RVA: 0x001486D9 File Offset: 0x001468D9
	public void RemoveVisElem(ILogicUIElement elem)
	{
		if (this.onElemRemoved != null)
		{
			this.onElemRemoved(elem);
		}
		this.uiVisElements.Remove(elem);
	}

	// Token: 0x06003B2A RID: 15146 RVA: 0x001486FC File Offset: 0x001468FC
	public ReadOnlyCollection<ILogicUIElement> GetVisElements()
	{
		return this.uiVisElements.AsReadOnly();
	}

	// Token: 0x06003B2B RID: 15147 RVA: 0x00148709 File Offset: 0x00146909
	public static void ToggleNoWireConnected(bool show_missing_wire, GameObject go)
	{
		go.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.NoLogicWireConnected, show_missing_wire, null);
	}

	// Token: 0x06003B2C RID: 15148 RVA: 0x00148728 File Offset: 0x00146928
	private void CheckCircuitOverloaded(float dt, int id, int bits_used)
	{
		UtilityNetwork networkByID = Game.Instance.logicCircuitSystem.GetNetworkByID(id);
		if (networkByID != null)
		{
			LogicCircuitNetwork logicCircuitNetwork = (LogicCircuitNetwork)networkByID;
			if (logicCircuitNetwork != null)
			{
				logicCircuitNetwork.UpdateOverloadTime(dt, bits_used);
			}
		}
	}

	// Token: 0x06003B2D RID: 15149 RVA: 0x0014875B File Offset: 0x0014695B
	public void Connect(LogicUtilityNetworkLink bridge)
	{
		this.bridgeGroups[(int)bridge.bitDepth].Add(bridge);
	}

	// Token: 0x06003B2E RID: 15150 RVA: 0x00148770 File Offset: 0x00146970
	public void Disconnect(LogicUtilityNetworkLink bridge)
	{
		this.bridgeGroups[(int)bridge.bitDepth].Remove(bridge);
	}

	// Token: 0x06003B2F RID: 15151 RVA: 0x00148788 File Offset: 0x00146988
	private void UpdateCircuitBridgeLists()
	{
		foreach (UtilityNetwork utilityNetwork in Game.Instance.logicCircuitSystem.GetNetworks())
		{
			LogicCircuitNetwork logicCircuitNetwork = (LogicCircuitNetwork)utilityNetwork;
			if (this.updateEvenBridgeGroups)
			{
				if (logicCircuitNetwork.id % 2 == 0)
				{
					logicCircuitNetwork.UpdateRelevantBridges(this.bridgeGroups);
				}
			}
			else if (logicCircuitNetwork.id % 2 == 1)
			{
				logicCircuitNetwork.UpdateRelevantBridges(this.bridgeGroups);
			}
		}
		this.updateEvenBridgeGroups = !this.updateEvenBridgeGroups;
	}

	// Token: 0x040026A6 RID: 9894
	public static float ClockTickInterval = 0.1f;

	// Token: 0x040026A7 RID: 9895
	private float elapsedTime;

	// Token: 0x040026A8 RID: 9896
	private UtilityNetworkManager<LogicCircuitNetwork, LogicWire> conduitSystem;

	// Token: 0x040026A9 RID: 9897
	private List<ILogicUIElement> uiVisElements = new List<ILogicUIElement>();

	// Token: 0x040026AA RID: 9898
	public static float BridgeRefreshInterval = 1f;

	// Token: 0x040026AB RID: 9899
	private List<LogicUtilityNetworkLink>[] bridgeGroups = new List<LogicUtilityNetworkLink>[2];

	// Token: 0x040026AC RID: 9900
	private bool updateEvenBridgeGroups;

	// Token: 0x040026AD RID: 9901
	private float timeSinceBridgeRefresh;

	// Token: 0x040026AE RID: 9902
	public System.Action onLogicTick;

	// Token: 0x040026AF RID: 9903
	public Action<ILogicUIElement> onElemAdded;

	// Token: 0x040026B0 RID: 9904
	public Action<ILogicUIElement> onElemRemoved;

	// Token: 0x02001554 RID: 5460
	private struct Signal
	{
		// Token: 0x060083A3 RID: 33699 RVA: 0x002E7E24 File Offset: 0x002E6024
		public Signal(int cell, int value)
		{
			this.cell = cell;
			this.value = value;
		}

		// Token: 0x0400666F RID: 26223
		public int cell;

		// Token: 0x04006670 RID: 26224
		public int value;
	}
}
