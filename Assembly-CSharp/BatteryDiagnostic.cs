﻿using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000706 RID: 1798
public class BatteryDiagnostic : ColonyDiagnostic
{
	// Token: 0x06003166 RID: 12646 RVA: 0x00107608 File Offset: 0x00105808
	public BatteryDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.BATTERYDIAGNOSTIC.ALL_NAME)
	{
		this.tracker = TrackerTool.Instance.GetWorldTracker<BatteryTracker>(worldID);
		this.trackerSampleCountSeconds = 4f;
		this.icon = "overlay_power";
		base.AddCriterion("CheckCapacity", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.BATTERYDIAGNOSTIC.CRITERIA.CHECKCAPACITY, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckCapacity)));
		base.AddCriterion("CheckDead", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.BATTERYDIAGNOSTIC.CRITERIA.CHECKDEAD, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckDead)));
	}

	// Token: 0x06003167 RID: 12647 RVA: 0x0010769C File Offset: 0x0010589C
	public ColonyDiagnostic.DiagnosticResult CheckCapacity()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		int num = 5;
		foreach (UtilityNetwork utilityNetwork in Game.Instance.electricalConduitSystem.GetNetworks())
		{
			ElectricalUtilityNetwork electricalUtilityNetwork = (ElectricalUtilityNetwork)utilityNetwork;
			if (electricalUtilityNetwork.allWires != null && electricalUtilityNetwork.allWires.Count != 0)
			{
				float num2 = 0f;
				int num3 = Grid.PosToCell(electricalUtilityNetwork.allWires[0]);
				if ((int)Grid.WorldIdx[num3] == base.worldID)
				{
					ushort circuitID = Game.Instance.circuitManager.GetCircuitID(num3);
					List<Battery> batteriesOnCircuit = Game.Instance.circuitManager.GetBatteriesOnCircuit(circuitID);
					if (batteriesOnCircuit != null && batteriesOnCircuit.Count != 0)
					{
						foreach (Battery battery in Game.Instance.circuitManager.GetBatteriesOnCircuit(circuitID))
						{
							diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
							diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.BATTERYDIAGNOSTIC.NORMAL;
							num2 += battery.capacity;
						}
						if (num2 < Game.Instance.circuitManager.GetWattsUsedByCircuit(circuitID) * (float)num)
						{
							diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
							diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.BATTERYDIAGNOSTIC.LIMITED_CAPACITY;
							Battery battery2 = Game.Instance.circuitManager.GetBatteriesOnCircuit(circuitID)[0];
							if (battery2 != null)
							{
								diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(battery2.transform.position, battery2.gameObject);
							}
						}
					}
				}
			}
		}
		return diagnosticResult;
	}

	// Token: 0x06003168 RID: 12648 RVA: 0x00107888 File Offset: 0x00105A88
	public ColonyDiagnostic.DiagnosticResult CheckDead()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		foreach (UtilityNetwork utilityNetwork in Game.Instance.electricalConduitSystem.GetNetworks())
		{
			ElectricalUtilityNetwork electricalUtilityNetwork = (ElectricalUtilityNetwork)utilityNetwork;
			if (electricalUtilityNetwork.allWires != null && electricalUtilityNetwork.allWires.Count != 0)
			{
				int num = Grid.PosToCell(electricalUtilityNetwork.allWires[0]);
				if ((int)Grid.WorldIdx[num] == base.worldID)
				{
					ushort circuitID = Game.Instance.circuitManager.GetCircuitID(num);
					List<Battery> batteriesOnCircuit = Game.Instance.circuitManager.GetBatteriesOnCircuit(circuitID);
					if (batteriesOnCircuit != null && batteriesOnCircuit.Count != 0)
					{
						foreach (Battery battery in Game.Instance.circuitManager.GetBatteriesOnCircuit(circuitID))
						{
							if (ColonyDiagnosticUtility.PastNewBuildingGracePeriod(battery.transform) && battery.CircuitID != 65535 && battery.JoulesAvailable == 0f)
							{
								diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
								diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.BATTERYDIAGNOSTIC.DEAD_BATTERY;
								diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(battery.transform.position, battery.gameObject);
								break;
							}
						}
					}
				}
			}
		}
		return diagnosticResult;
	}

	// Token: 0x06003169 RID: 12649 RVA: 0x00107A34 File Offset: 0x00105C34
	public override ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.NO_MINIONS, null);
		if (ColonyDiagnosticUtility.IgnoreRocketsWithNoCrewRequested(base.worldID, out diagnosticResult))
		{
			return diagnosticResult;
		}
		return base.Evaluate();
	}
}
