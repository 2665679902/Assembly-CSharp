using System;
using STRINGS;

// Token: 0x02000711 RID: 1809
public class HeatDiagnostic : ColonyDiagnostic
{
	// Token: 0x060031A0 RID: 12704 RVA: 0x00108DA0 File Offset: 0x00106FA0
	public HeatDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.HEATDIAGNOSTIC.ALL_NAME)
	{
		this.tracker = TrackerTool.Instance.GetWorldTracker<BatteryTracker>(worldID);
		this.trackerSampleCountSeconds = 4f;
		base.AddCriterion("CheckHeat", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.HEATDIAGNOSTIC.CRITERIA.CHECKHEAT, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckHeat)));
	}

	// Token: 0x060031A1 RID: 12705 RVA: 0x00108E00 File Offset: 0x00107000
	private ColonyDiagnostic.DiagnosticResult CheckHeat()
	{
		return new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null)
		{
			opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal,
			Message = UI.COLONY_DIAGNOSTICS.BATTERYDIAGNOSTIC.NORMAL
		};
	}
}
