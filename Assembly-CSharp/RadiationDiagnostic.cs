using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000714 RID: 1812
public class RadiationDiagnostic : ColonyDiagnostic
{
	// Token: 0x060031A8 RID: 12712 RVA: 0x001092B4 File Offset: 0x001074B4
	public RadiationDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.RADIATIONDIAGNOSTIC.ALL_NAME)
	{
		this.tracker = TrackerTool.Instance.GetWorldTracker<RadiationTracker>(worldID);
		this.trackerSampleCountSeconds = 150f;
		this.presentationSetting = ColonyDiagnostic.PresentationSetting.CurrentValue;
		this.icon = "overlay_radiation";
		base.AddCriterion("CheckSick", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.RADIATIONDIAGNOSTIC.CRITERIA.CHECKSICK, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckSick)));
		base.AddCriterion("CheckExposed", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.RADIATIONDIAGNOSTIC.CRITERIA.CHECKEXPOSED, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckExposure)));
	}

	// Token: 0x060031A9 RID: 12713 RVA: 0x0010934C File Offset: 0x0010754C
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060031AA RID: 12714 RVA: 0x00109353 File Offset: 0x00107553
	public override string GetCurrentValueString()
	{
		return string.Format(UI.COLONY_DIAGNOSTICS.RADIATIONDIAGNOSTIC.AVERAGE_RADS, GameUtil.GetFormattedRads(TrackerTool.Instance.GetWorldTracker<RadiationTracker>(base.worldID).GetCurrentValue(), GameUtil.TimeSlice.None));
	}

	// Token: 0x060031AB RID: 12715 RVA: 0x0010937F File Offset: 0x0010757F
	public override string GetAverageValueString()
	{
		return string.Format(UI.COLONY_DIAGNOSTICS.RADIATIONDIAGNOSTIC.AVERAGE_RADS, GameUtil.GetFormattedRads(TrackerTool.Instance.GetWorldTracker<RadiationTracker>(base.worldID).GetCurrentValue(), GameUtil.TimeSlice.None));
	}

	// Token: 0x060031AC RID: 12716 RVA: 0x001093AC File Offset: 0x001075AC
	private ColonyDiagnostic.DiagnosticResult CheckSick()
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
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.RADIATIONDIAGNOSTIC.NORMAL;
			foreach (MinionIdentity minionIdentity in worldItems)
			{
				RadiationMonitor.Instance smi = minionIdentity.GetSMI<RadiationMonitor.Instance>();
				if (smi != null && smi.sm.isSick.Get(smi))
				{
					diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
					diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.RADIATIONDIAGNOSTIC.CRITERIA_RADIATION_SICKNESS.FAIL;
					diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(minionIdentity.gameObject.transform.position, minionIdentity.gameObject);
				}
			}
		}
		return diagnosticResult;
	}

	// Token: 0x060031AD RID: 12717 RVA: 0x001094B4 File Offset: 0x001076B4
	private ColonyDiagnostic.DiagnosticResult CheckExposure()
	{
		List<MinionIdentity> worldItems = Components.LiveMinionIdentities.GetWorldItems(base.worldID, false);
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		if (worldItems.Count == 0)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.NO_MINIONS;
			return diagnosticResult;
		}
		diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
		diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.RADIATIONDIAGNOSTIC.CRITERIA_RADIATION_EXPOSURE.PASS;
		foreach (MinionIdentity minionIdentity in worldItems)
		{
			RadiationMonitor.Instance smi = minionIdentity.GetSMI<RadiationMonitor.Instance>();
			if (smi != null)
			{
				RadiationMonitor sm = smi.sm;
				GameObject gameObject = minionIdentity.gameObject;
				Vector3 position = gameObject.transform.position;
				float num = sm.currentExposurePerCycle.Get(smi);
				float num2 = sm.radiationExposure.Get(smi);
				if (RadiationMonitor.COMPARE_LT_MINOR(smi, num) && RadiationMonitor.COMPARE_RECOVERY_IMMEDIATE(smi, num2))
				{
					diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(position, gameObject);
					diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
					diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.RADIATIONDIAGNOSTIC.CRITERIA_RADIATION_EXPOSURE.FAIL_CONCERN;
				}
				if (RadiationMonitor.COMPARE_GTE_DEADLY(smi, num))
				{
					diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(position, minionIdentity.gameObject);
					diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Warning;
					diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.RADIATIONDIAGNOSTIC.CRITERIA_RADIATION_EXPOSURE.FAIL_WARNING;
				}
			}
		}
		return diagnosticResult;
	}
}
