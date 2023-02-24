using System;
using STRINGS;

// Token: 0x02000718 RID: 1816
public class RocketsInOrbitDiagnostic : ColonyDiagnostic
{
	// Token: 0x060031B8 RID: 12728 RVA: 0x00109A00 File Offset: 0x00107C00
	public RocketsInOrbitDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.ROCKETINORBITDIAGNOSTIC.ALL_NAME)
	{
		this.icon = "icon_errand_rocketry";
		base.AddCriterion("RocketsOrbiting", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.ROCKETINORBITDIAGNOSTIC.CRITERIA.CHECKORBIT, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckOrbit)));
	}

	// Token: 0x060031B9 RID: 12729 RVA: 0x00109A4F File Offset: 0x00107C4F
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060031BA RID: 12730 RVA: 0x00109A58 File Offset: 0x00107C58
	public ColonyDiagnostic.DiagnosticResult CheckOrbit()
	{
		AxialI myWorldLocation = ClusterManager.Instance.GetWorld(base.worldID).GetMyWorldLocation();
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.NO_MINIONS, null);
		diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
		this.numRocketsInOrbit = 0;
		Clustercraft clustercraft = null;
		bool flag = false;
		foreach (Clustercraft clustercraft2 in Components.Clustercrafts.Items)
		{
			AxialI myWorldLocation2 = clustercraft2.GetMyWorldLocation();
			AxialI destination = clustercraft2.Destination;
			if (myWorldLocation2 != myWorldLocation && ClusterGrid.Instance.IsInRange(myWorldLocation2, myWorldLocation, 1) && ClusterGrid.Instance.IsInRange(myWorldLocation, destination, 1))
			{
				this.numRocketsInOrbit++;
				clustercraft = clustercraft2;
				flag = flag || !clustercraft2.CanLandAtAsteroid(myWorldLocation, false);
			}
		}
		if (this.numRocketsInOrbit == 1 && clustercraft != null)
		{
			diagnosticResult.Message = string.Format(flag ? UI.COLONY_DIAGNOSTICS.ROCKETINORBITDIAGNOSTIC.WARNING_ONE_ROCKETS_STRANDED : UI.COLONY_DIAGNOSTICS.ROCKETINORBITDIAGNOSTIC.NORMAL_ONE_IN_ORBIT, clustercraft.Name);
		}
		else if (this.numRocketsInOrbit > 0)
		{
			diagnosticResult.Message = string.Format(flag ? UI.COLONY_DIAGNOSTICS.ROCKETINORBITDIAGNOSTIC.WARNING_ROCKETS_STRANDED : UI.COLONY_DIAGNOSTICS.ROCKETINORBITDIAGNOSTIC.NORMAL_IN_ORBIT, this.numRocketsInOrbit);
		}
		else
		{
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.ROCKETINORBITDIAGNOSTIC.NORMAL_NO_ROCKETS;
		}
		if (flag)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Warning;
		}
		else if (this.numRocketsInOrbit > 0)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Suggestion;
		}
		return diagnosticResult;
	}

	// Token: 0x04001E3D RID: 7741
	private int numRocketsInOrbit;
}
