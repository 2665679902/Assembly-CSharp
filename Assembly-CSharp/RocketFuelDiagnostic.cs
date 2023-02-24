using System;
using STRINGS;

// Token: 0x02000716 RID: 1814
public class RocketFuelDiagnostic : ColonyDiagnostic
{
	// Token: 0x060031B2 RID: 12722 RVA: 0x00109850 File Offset: 0x00107A50
	public RocketFuelDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.ROCKETFUELDIAGNOSTIC.ALL_NAME)
	{
		this.tracker = TrackerTool.Instance.GetWorldTracker<RocketFuelTracker>(worldID);
		this.icon = "rocket_fuel";
	}

	// Token: 0x060031B3 RID: 12723 RVA: 0x0010987F File Offset: 0x00107A7F
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060031B4 RID: 12724 RVA: 0x00109888 File Offset: 0x00107A88
	public override ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		Clustercraft component = ClusterManager.Instance.GetWorld(base.worldID).gameObject.GetComponent<Clustercraft>();
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.NO_MINIONS, null);
		if (ColonyDiagnosticUtility.IgnoreRocketsWithNoCrewRequested(base.worldID, out diagnosticResult))
		{
			return diagnosticResult;
		}
		diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
		diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.ROCKETFUELDIAGNOSTIC.NORMAL;
		if (component.ModuleInterface.FuelRemaining == 0f)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.ROCKETFUELDIAGNOSTIC.WARNING;
		}
		return diagnosticResult;
	}
}
