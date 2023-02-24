using System;
using STRINGS;

// Token: 0x02000705 RID: 1797
public class AllWorkTimeDiagnostic : ColonyDiagnostic
{
	// Token: 0x06003164 RID: 12644 RVA: 0x00107579 File Offset: 0x00105779
	public AllWorkTimeDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.ALLWORKTIMEDIAGNOSTIC.ALL_NAME)
	{
		this.tracker = TrackerTool.Instance.GetWorldTracker<AllWorkTimeTracker>(worldID);
		this.colors[ColonyDiagnostic.DiagnosticResult.Opinion.Good] = Constants.NEUTRAL_COLOR;
	}

	// Token: 0x06003165 RID: 12645 RVA: 0x001075B0 File Offset: 0x001057B0
	public override ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		return new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null)
		{
			opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal,
			Message = string.Format(UI.COLONY_DIAGNOSTICS.ALLWORKTIMEDIAGNOSTIC.NORMAL, this.tracker.FormatValueString(this.tracker.GetCurrentValue()))
		};
	}
}
