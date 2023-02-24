using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000708 RID: 1800
public class BreathabilityDiagnostic : ColonyDiagnostic
{
	// Token: 0x0600316E RID: 12654 RVA: 0x00107C58 File Offset: 0x00105E58
	public BreathabilityDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.BREATHABILITYDIAGNOSTIC.ALL_NAME)
	{
		this.tracker = TrackerTool.Instance.GetWorldTracker<BreathabilityTracker>(worldID);
		this.trackerSampleCountSeconds = 50f;
		this.icon = "overlay_oxygen";
		base.AddCriterion("CheckSuffocation", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.BREATHABILITYDIAGNOSTIC.CRITERIA.CHECKSUFFOCATION, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckSuffocation)));
		base.AddCriterion("CheckLowBreathability", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.BREATHABILITYDIAGNOSTIC.CRITERIA.CHECKLOWBREATHABILITY, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckLowBreathability)));
	}

	// Token: 0x0600316F RID: 12655 RVA: 0x00107CEC File Offset: 0x00105EEC
	private ColonyDiagnostic.DiagnosticResult CheckSuffocation()
	{
		List<MinionIdentity> worldItems = Components.LiveMinionIdentities.GetWorldItems(base.worldID, false);
		if (worldItems.Count != 0)
		{
			foreach (MinionIdentity minionIdentity in worldItems)
			{
				minionIdentity.GetComponent<OxygenBreather>().GetGasProvider();
				SuffocationMonitor.Instance smi = minionIdentity.GetSMI<SuffocationMonitor.Instance>();
				if (smi != null && smi.IsInsideState(smi.sm.nooxygen.suffocating))
				{
					return new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.DuplicantThreatening, UI.COLONY_DIAGNOSTICS.BREATHABILITYDIAGNOSTIC.SUFFOCATING, new global::Tuple<Vector3, GameObject>(smi.transform.position, smi.gameObject));
				}
			}
		}
		return new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
	}

	// Token: 0x06003170 RID: 12656 RVA: 0x00107DB8 File Offset: 0x00105FB8
	private ColonyDiagnostic.DiagnosticResult CheckLowBreathability()
	{
		if (Components.LiveMinionIdentities.GetWorldItems(base.worldID, false).Count != 0 && this.tracker.GetAverageValue(this.trackerSampleCountSeconds) < 60f)
		{
			return new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Concern, UI.COLONY_DIAGNOSTICS.BREATHABILITYDIAGNOSTIC.POOR, null);
		}
		return new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
	}

	// Token: 0x06003171 RID: 12657 RVA: 0x00107E18 File Offset: 0x00106018
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
