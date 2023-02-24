using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200071B RID: 1819
public class TrappedDuplicantDiagnostic : ColonyDiagnostic
{
	// Token: 0x060031C3 RID: 12739 RVA: 0x0010A0E0 File Offset: 0x001082E0
	public TrappedDuplicantDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.TRAPPEDDUPLICANTDIAGNOSTIC.ALL_NAME)
	{
		this.icon = "overlay_power";
		base.AddCriterion("CheckTrapped", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.TRAPPEDDUPLICANTDIAGNOSTIC.CRITERIA.CHECKTRAPPED, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckTrapped)));
	}

	// Token: 0x060031C4 RID: 12740 RVA: 0x0010A130 File Offset: 0x00108330
	public ColonyDiagnostic.DiagnosticResult CheckTrapped()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		bool flag = false;
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.GetWorldItems(base.worldID, false))
		{
			if (flag)
			{
				break;
			}
			if (!ClusterManager.Instance.GetWorld(base.worldID).IsModuleInterior && this.CheckMinionBasicallyIdle(minionIdentity))
			{
				Navigator component = minionIdentity.GetComponent<Navigator>();
				bool flag2 = true;
				foreach (MinionIdentity minionIdentity2 in Components.LiveMinionIdentities.GetWorldItems(base.worldID, false))
				{
					if (!(minionIdentity == minionIdentity2) && !this.CheckMinionBasicallyIdle(minionIdentity2) && component.CanReach(minionIdentity2.GetComponent<IApproachable>()))
					{
						flag2 = false;
						break;
					}
				}
				List<Telepad> worldItems = Components.Telepads.GetWorldItems(component.GetMyWorld().id, false);
				if (worldItems != null && worldItems.Count > 0)
				{
					flag2 = flag2 && !component.CanReach(worldItems[0].GetComponent<IApproachable>());
				}
				List<WarpReceiver> worldItems2 = Components.WarpReceivers.GetWorldItems(component.GetMyWorld().id, false);
				if (worldItems2 != null && worldItems2.Count > 0)
				{
					foreach (WarpReceiver warpReceiver in worldItems2)
					{
						flag2 = flag2 && !component.CanReach(worldItems2[0].GetComponent<IApproachable>());
					}
				}
				List<Sleepable> worldItems3 = Components.Sleepables.GetWorldItems(component.GetMyWorld().id, false);
				for (int i = 0; i < worldItems3.Count; i++)
				{
					Assignable component2 = worldItems3[i].GetComponent<Assignable>();
					if (component2 != null && component2.IsAssignedTo(minionIdentity))
					{
						flag2 = flag2 && !component.CanReach(worldItems3[i].GetComponent<IApproachable>());
					}
				}
				if (flag2)
				{
					diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(minionIdentity.transform.position, minionIdentity.gameObject);
				}
				flag = flag || flag2;
			}
		}
		diagnosticResult.opinion = (flag ? ColonyDiagnostic.DiagnosticResult.Opinion.Bad : ColonyDiagnostic.DiagnosticResult.Opinion.Normal);
		diagnosticResult.Message = (flag ? UI.COLONY_DIAGNOSTICS.TRAPPEDDUPLICANTDIAGNOSTIC.STUCK : UI.COLONY_DIAGNOSTICS.TRAPPEDDUPLICANTDIAGNOSTIC.NORMAL);
		return diagnosticResult;
	}

	// Token: 0x060031C5 RID: 12741 RVA: 0x0010A400 File Offset: 0x00108600
	private bool CheckMinionBasicallyIdle(MinionIdentity minion)
	{
		return minion.HasTag(GameTags.Idle) || minion.HasTag(GameTags.RecoveringBreath) || minion.HasTag(GameTags.MakingMess);
	}
}
