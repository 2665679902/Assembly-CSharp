using System;
using System.Collections.Generic;
using STRINGS;

// Token: 0x02000707 RID: 1799
public class BedDiagnostic : ColonyDiagnostic
{
	// Token: 0x0600316A RID: 12650 RVA: 0x00107A6C File Offset: 0x00105C6C
	public BedDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.BEDDIAGNOSTIC.ALL_NAME)
	{
		this.icon = "icon_action_region_bedroom";
		base.AddCriterion("CheckEnoughBeds", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.BEDDIAGNOSTIC.CRITERIA.CHECKENOUGHBEDS, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckEnoughBeds)));
	}

	// Token: 0x0600316B RID: 12651 RVA: 0x00107ABC File Offset: 0x00105CBC
	private ColonyDiagnostic.DiagnosticResult CheckEnoughBeds()
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
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.BEDDIAGNOSTIC.NORMAL;
			int num = 0;
			List<Sleepable> worldItems2 = Components.Sleepables.GetWorldItems(base.worldID, false);
			for (int i = 0; i < worldItems2.Count; i++)
			{
				if (worldItems2[i].GetComponent<Assignable>() != null && worldItems2[i].GetComponent<Clinic>() == null)
				{
					num++;
				}
			}
			if (num < worldItems.Count)
			{
				diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
				diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.BEDDIAGNOSTIC.NOT_ENOUGH_BEDS;
			}
		}
		return diagnosticResult;
	}

	// Token: 0x0600316C RID: 12652 RVA: 0x00107BA8 File Offset: 0x00105DA8
	public override ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.NO_MINIONS, null);
		if (ColonyDiagnosticUtility.IgnoreRocketsWithNoCrewRequested(base.worldID, out diagnosticResult))
		{
			return diagnosticResult;
		}
		return base.Evaluate();
	}

	// Token: 0x0600316D RID: 12653 RVA: 0x00107BE0 File Offset: 0x00105DE0
	public override string GetAverageValueString()
	{
		return Components.Sleepables.GetWorldItems(base.worldID, false).FindAll((Sleepable match) => match.GetComponent<Assignable>() != null).Count.ToString() + "/" + Components.LiveMinionIdentities.GetWorldItems(base.worldID, false).Count.ToString();
	}
}
