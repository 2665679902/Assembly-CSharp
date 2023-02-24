using System;
using STRINGS;

// Token: 0x0200070F RID: 1807
public class FloatingRocketDiagnostic : ColonyDiagnostic
{
	// Token: 0x06003197 RID: 12695 RVA: 0x00108899 File Offset: 0x00106A99
	public FloatingRocketDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.FLOATINGROCKETDIAGNOSTIC.ALL_NAME)
	{
		this.icon = "icon_errand_rocketry";
	}

	// Token: 0x06003198 RID: 12696 RVA: 0x001088B7 File Offset: 0x00106AB7
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06003199 RID: 12697 RVA: 0x001088C0 File Offset: 0x00106AC0
	public override ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		WorldContainer world = ClusterManager.Instance.GetWorld(base.worldID);
		Clustercraft component = world.gameObject.GetComponent<Clustercraft>();
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.NO_MINIONS, null);
		if (ColonyDiagnosticUtility.IgnoreRocketsWithNoCrewRequested(base.worldID, out diagnosticResult))
		{
			return diagnosticResult;
		}
		diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
		if (world.ParentWorldId == (int)ClusterManager.INVALID_WORLD_IDX || world.ParentWorldId == world.id)
		{
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.FLOATINGROCKETDIAGNOSTIC.NORMAL_FLIGHT;
			if (component.Destination == component.Location)
			{
				bool flag = false;
				foreach (Ref<RocketModuleCluster> @ref in component.ModuleInterface.ClusterModules)
				{
					ResourceHarvestModule.StatesInstance smi = @ref.Get().GetSMI<ResourceHarvestModule.StatesInstance>();
					if (smi != null && smi.IsInsideState(smi.sm.not_grounded.harvesting))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
					diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.FLOATINGROCKETDIAGNOSTIC.NORMAL_UTILITY;
				}
				else
				{
					diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Suggestion;
					diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.FLOATINGROCKETDIAGNOSTIC.WARNING_NO_DESTINATION;
				}
			}
			else if (component.Speed == 0f)
			{
				diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
				diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.FLOATINGROCKETDIAGNOSTIC.WARNING_NO_SPEED;
			}
		}
		else
		{
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.FLOATINGROCKETDIAGNOSTIC.NORMAL_LANDED;
		}
		return diagnosticResult;
	}
}
