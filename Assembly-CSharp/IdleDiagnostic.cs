using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000712 RID: 1810
public class IdleDiagnostic : ColonyDiagnostic
{
	// Token: 0x060031A2 RID: 12706 RVA: 0x00108E3C File Offset: 0x0010703C
	public IdleDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.IDLEDIAGNOSTIC.ALL_NAME)
	{
		this.tracker = TrackerTool.Instance.GetWorldTracker<IdleTracker>(worldID);
		this.icon = "icon_errand_operate";
		base.AddCriterion("CheckIdle", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.IDLEDIAGNOSTIC.CRITERIA.CHECKIDLE, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckIdle)));
	}

	// Token: 0x060031A3 RID: 12707 RVA: 0x00108E9C File Offset: 0x0010709C
	private ColonyDiagnostic.DiagnosticResult CheckIdle()
	{
		List<MinionIdentity> worldItems = Components.LiveMinionIdentities.GetWorldItems(base.worldID, false);
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		if (worldItems.Count == 0)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.NO_MINIONS;
		}
		else
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.IDLEDIAGNOSTIC.NORMAL;
			if (this.tracker.GetMinValue(30f) > 0f && this.tracker.GetCurrentValue() > 0f)
			{
				diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
				diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.IDLEDIAGNOSTIC.IDLE;
				MinionIdentity minionIdentity = Components.LiveMinionIdentities.GetWorldItems(base.worldID, false).Find((MinionIdentity match) => match.HasTag(GameTags.Idle));
				if (minionIdentity != null)
				{
					diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(minionIdentity.transform.position, minionIdentity.gameObject);
				}
			}
		}
		return diagnosticResult;
	}
}
