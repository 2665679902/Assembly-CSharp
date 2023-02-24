using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000719 RID: 1817
public class StressDiagnostic : ColonyDiagnostic
{
	// Token: 0x060031BB RID: 12731 RVA: 0x00109BE8 File Offset: 0x00107DE8
	public StressDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.STRESSDIAGNOSTIC.ALL_NAME)
	{
		this.tracker = TrackerTool.Instance.GetWorldTracker<StressTracker>(worldID);
		this.icon = "mod_stress";
		base.AddCriterion("CheckStressed", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.STRESSDIAGNOSTIC.CRITERIA.CHECKSTRESSED, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckStressed)));
	}

	// Token: 0x060031BC RID: 12732 RVA: 0x00109C48 File Offset: 0x00107E48
	private ColonyDiagnostic.DiagnosticResult CheckStressed()
	{
		List<MinionIdentity> worldItems = Components.LiveMinionIdentities.GetWorldItems(base.worldID, false);
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		if (worldItems.Count == 0)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
			TrackerTool.Instance.IsRocketInterior(base.worldID) ? UI.COLONY_DIAGNOSTICS.ROCKET : UI.CLUSTERMAP.PLANETOID_KEYWORD;
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.NO_MINIONS;
		}
		else
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.STRESSDIAGNOSTIC.NORMAL;
			if (this.tracker.GetAverageValue(this.trackerSampleCountSeconds) >= STRESS.ACTING_OUT_RESET)
			{
				diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
				diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.STRESSDIAGNOSTIC.HIGH_STRESS;
				MinionIdentity minionIdentity = worldItems.Find((MinionIdentity match) => match.gameObject.GetAmounts().GetValue(Db.Get().Amounts.Stress.Id) >= STRESS.ACTING_OUT_RESET);
				if (minionIdentity != null)
				{
					diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(minionIdentity.gameObject.transform.position, minionIdentity.gameObject);
				}
			}
		}
		return diagnosticResult;
	}
}
