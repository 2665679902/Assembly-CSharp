using System;
using STRINGS;

// Token: 0x02000709 RID: 1801
public class ChoreGroupDiagnostic : ColonyDiagnostic
{
	// Token: 0x06003172 RID: 12658 RVA: 0x00107E50 File Offset: 0x00106050
	public ChoreGroupDiagnostic(int worldID, ChoreGroup choreGroup)
		: base(worldID, UI.COLONY_DIAGNOSTICS.CHOREGROUPDIAGNOSTIC.ALL_NAME)
	{
		this.choreGroup = choreGroup;
		this.tracker = TrackerTool.Instance.GetChoreGroupTracker(worldID, choreGroup);
		this.name = choreGroup.Name;
		this.colors[ColonyDiagnostic.DiagnosticResult.Opinion.Good] = Constants.NEUTRAL_COLOR;
		this.id = "ChoreGroupDiagnostic_" + choreGroup.Id;
	}

	// Token: 0x06003173 RID: 12659 RVA: 0x00107EBC File Offset: 0x001060BC
	public override ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		return new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null)
		{
			opinion = ((this.tracker.GetCurrentValue() > 0f) ? ColonyDiagnostic.DiagnosticResult.Opinion.Good : ColonyDiagnostic.DiagnosticResult.Opinion.Normal),
			Message = string.Format(UI.COLONY_DIAGNOSTICS.ALLCHORESDIAGNOSTIC.NORMAL, this.tracker.FormatValueString(this.tracker.GetCurrentValue()))
		};
	}

	// Token: 0x04001E2D RID: 7725
	public ChoreGroup choreGroup;
}
