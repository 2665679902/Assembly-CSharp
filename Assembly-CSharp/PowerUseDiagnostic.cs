using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000713 RID: 1811
public class PowerUseDiagnostic : ColonyDiagnostic
{
	// Token: 0x060031A4 RID: 12708 RVA: 0x00108FB0 File Offset: 0x001071B0
	public PowerUseDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.POWERUSEDIAGNOSTIC.ALL_NAME)
	{
		this.tracker = TrackerTool.Instance.GetWorldTracker<PowerUseTracker>(worldID);
		this.trackerSampleCountSeconds = 30f;
		this.icon = "overlay_power";
		base.AddCriterion("CheckOverWattage", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.POWERUSEDIAGNOSTIC.CRITERIA.CHECKOVERWATTAGE, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckOverWattage)));
		base.AddCriterion("CheckPowerUseChange", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.POWERUSEDIAGNOSTIC.CRITERIA.CHECKPOWERUSECHANGE, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckPowerChange)));
	}

	// Token: 0x060031A5 RID: 12709 RVA: 0x00109044 File Offset: 0x00107244
	private ColonyDiagnostic.DiagnosticResult CheckOverWattage()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
		diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.POWERUSEDIAGNOSTIC.NORMAL;
		foreach (UtilityNetwork utilityNetwork in Game.Instance.electricalConduitSystem.GetNetworks())
		{
			ElectricalUtilityNetwork electricalUtilityNetwork = (ElectricalUtilityNetwork)utilityNetwork;
			if (electricalUtilityNetwork.allWires != null && electricalUtilityNetwork.allWires.Count != 0)
			{
				int num = Grid.PosToCell(electricalUtilityNetwork.allWires[0]);
				if ((int)Grid.WorldIdx[num] == base.worldID)
				{
					ushort circuitID = Game.Instance.circuitManager.GetCircuitID(num);
					float maxSafeWattageForCircuit = Game.Instance.circuitManager.GetMaxSafeWattageForCircuit(circuitID);
					float wattsUsedByCircuit = Game.Instance.circuitManager.GetWattsUsedByCircuit(circuitID);
					if (wattsUsedByCircuit > maxSafeWattageForCircuit)
					{
						GameObject gameObject = electricalUtilityNetwork.allWires[0].gameObject;
						diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(gameObject.transform.position, gameObject);
						diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
						diagnosticResult.Message = string.Format(UI.COLONY_DIAGNOSTICS.POWERUSEDIAGNOSTIC.CIRCUIT_OVER_CAPACITY, GameUtil.GetFormattedWattage(wattsUsedByCircuit, GameUtil.WattageFormatterUnit.Automatic, true), GameUtil.GetFormattedWattage(maxSafeWattageForCircuit, GameUtil.WattageFormatterUnit.Automatic, true));
						break;
					}
				}
			}
		}
		return diagnosticResult;
	}

	// Token: 0x060031A6 RID: 12710 RVA: 0x001091A8 File Offset: 0x001073A8
	private ColonyDiagnostic.DiagnosticResult CheckPowerChange()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
		diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.POWERUSEDIAGNOSTIC.NORMAL;
		float num = 60f;
		if (this.tracker.GetDataTimeLength() < num)
		{
			return diagnosticResult;
		}
		float averageValue = this.tracker.GetAverageValue(1f);
		float averageValue2 = this.tracker.GetAverageValue(Mathf.Min(60f, this.trackerSampleCountSeconds));
		float num2 = 240f;
		if (averageValue < num2 && averageValue2 < num2)
		{
			return diagnosticResult;
		}
		float num3 = 0.5f;
		if (Mathf.Abs(averageValue - averageValue2) / averageValue2 > num3)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
			diagnosticResult.Message = string.Format(UI.COLONY_DIAGNOSTICS.POWERUSEDIAGNOSTIC.SIGNIFICANT_POWER_CHANGE_DETECTED, GameUtil.GetFormattedWattage(averageValue2, GameUtil.WattageFormatterUnit.Automatic, true), GameUtil.GetFormattedWattage(averageValue, GameUtil.WattageFormatterUnit.Automatic, true));
		}
		return diagnosticResult;
	}

	// Token: 0x060031A7 RID: 12711 RVA: 0x0010927C File Offset: 0x0010747C
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
