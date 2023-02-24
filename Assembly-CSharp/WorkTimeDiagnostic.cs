using System;
using STRINGS;

// Token: 0x0200071C RID: 1820
public class WorkTimeDiagnostic : ColonyDiagnostic
{
	// Token: 0x060031C6 RID: 12742 RVA: 0x0010A42C File Offset: 0x0010862C
	public WorkTimeDiagnostic(int worldID, ChoreGroup choreGroup)
		: base(worldID, UI.COLONY_DIAGNOSTICS.WORKTIMEDIAGNOSTIC.ALL_NAME)
	{
		this.choreGroup = choreGroup;
		this.tracker = TrackerTool.Instance.GetWorkTimeTracker(worldID, choreGroup);
		this.trackerSampleCountSeconds = 100f;
		this.name = choreGroup.Name;
		this.id = "WorkTimeDiagnostic_" + choreGroup.Id;
		this.colors[ColonyDiagnostic.DiagnosticResult.Opinion.Good] = Constants.NEUTRAL_COLOR;
	}

	// Token: 0x060031C7 RID: 12743 RVA: 0x0010A4A4 File Offset: 0x001086A4
	public override ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		return new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null)
		{
			opinion = ((this.tracker.GetAverageValue(this.trackerSampleCountSeconds) > 0f) ? ColonyDiagnostic.DiagnosticResult.Opinion.Good : ColonyDiagnostic.DiagnosticResult.Opinion.Normal),
			Message = string.Format(UI.COLONY_DIAGNOSTICS.ALLWORKTIMEDIAGNOSTIC.NORMAL, this.tracker.FormatValueString(this.tracker.GetAverageValue(this.trackerSampleCountSeconds)))
		};
	}

	// Token: 0x04001E3E RID: 7742
	public ChoreGroup choreGroup;
}
