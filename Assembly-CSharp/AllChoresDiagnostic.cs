using System;
using STRINGS;

// Token: 0x02000704 RID: 1796
public class AllChoresDiagnostic : ColonyDiagnostic
{
	// Token: 0x06003162 RID: 12642 RVA: 0x001074E3 File Offset: 0x001056E3
	public AllChoresDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.ALLCHORESDIAGNOSTIC.ALL_NAME)
	{
		this.tracker = TrackerTool.Instance.GetWorldTracker<AllChoresCountTracker>(worldID);
		this.colors[ColonyDiagnostic.DiagnosticResult.Opinion.Good] = Constants.NEUTRAL_COLOR;
		this.icon = "icon_errand_operate";
	}

	// Token: 0x06003163 RID: 12643 RVA: 0x00107524 File Offset: 0x00105724
	public override ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		return new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null)
		{
			opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal,
			Message = string.Format(UI.COLONY_DIAGNOSTICS.ALLCHORESDIAGNOSTIC.NORMAL, this.tracker.FormatValueString(this.tracker.GetCurrentValue()))
		};
	}
}
