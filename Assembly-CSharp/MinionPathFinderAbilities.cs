using System;
using System.Diagnostics;

// Token: 0x0200036F RID: 879
public class MinionPathFinderAbilities : PathFinderAbilities
{
	// Token: 0x060011FA RID: 4602 RVA: 0x0005EA7C File Offset: 0x0005CC7C
	public MinionPathFinderAbilities(Navigator navigator)
		: base(navigator)
	{
		this.transitionVoidOffsets = new CellOffset[navigator.NavGrid.transitions.Length][];
		for (int i = 0; i < this.transitionVoidOffsets.Length; i++)
		{
			this.transitionVoidOffsets[i] = navigator.NavGrid.transitions[i].voidOffsets;
		}
	}

	// Token: 0x060011FB RID: 4603 RVA: 0x0005EAD9 File Offset: 0x0005CCD9
	protected override void Refresh(Navigator navigator)
	{
		this.proxyID = navigator.GetComponent<MinionIdentity>().assignableProxy.Get().GetComponent<KPrefabID>().InstanceID;
		this.out_of_fuel = navigator.HasTag(GameTags.JetSuitOutOfFuel);
	}

	// Token: 0x060011FC RID: 4604 RVA: 0x0005EB0C File Offset: 0x0005CD0C
	public void SetIdleNavMaskEnabled(bool enabled)
	{
		this.idleNavMaskEnabled = enabled;
	}

	// Token: 0x060011FD RID: 4605 RVA: 0x0005EB15 File Offset: 0x0005CD15
	private static bool IsAccessPermitted(int proxyID, int cell, int from_cell, NavType from_nav_type)
	{
		return Grid.HasPermission(cell, proxyID, from_cell, from_nav_type);
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x0005EB20 File Offset: 0x0005CD20
	public override int GetSubmergedPathCostPenalty(PathFinder.PotentialPath path, NavGrid.Link link)
	{
		if (!path.HasAnyFlag(PathFinder.PotentialPath.Flags.HasAtmoSuit | PathFinder.PotentialPath.Flags.HasJetPack))
		{
			return (int)(link.cost * 2);
		}
		return 0;
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x0005EB38 File Offset: 0x0005CD38
	public override bool TraversePath(ref PathFinder.PotentialPath path, int from_cell, NavType from_nav_type, int cost, int transition_id, bool submerged)
	{
		if (!MinionPathFinderAbilities.IsAccessPermitted(this.proxyID, path.cell, from_cell, from_nav_type))
		{
			return false;
		}
		foreach (CellOffset cellOffset in this.transitionVoidOffsets[transition_id])
		{
			int num = Grid.OffsetCell(from_cell, cellOffset);
			if (!MinionPathFinderAbilities.IsAccessPermitted(this.proxyID, num, from_cell, from_nav_type))
			{
				return false;
			}
		}
		if (path.navType == NavType.Tube && from_nav_type == NavType.Floor && !Grid.HasUsableTubeEntrance(from_cell, this.prefabInstanceID))
		{
			return false;
		}
		if (path.navType == NavType.Hover && (this.out_of_fuel || !path.HasFlag(PathFinder.PotentialPath.Flags.HasJetPack)))
		{
			return false;
		}
		Grid.SuitMarker.Flags flags = (Grid.SuitMarker.Flags)0;
		PathFinder.PotentialPath.Flags flags2 = PathFinder.PotentialPath.Flags.None;
		bool flag = path.HasFlag(PathFinder.PotentialPath.Flags.PerformSuitChecks) && Grid.TryGetSuitMarkerFlags(from_cell, out flags, out flags2) && (flags & Grid.SuitMarker.Flags.Operational) > (Grid.SuitMarker.Flags)0;
		bool flag2 = SuitMarker.DoesTraversalDirectionRequireSuit(from_cell, path.cell, flags);
		bool flag3 = path.HasAnyFlag(PathFinder.PotentialPath.Flags.HasAtmoSuit | PathFinder.PotentialPath.Flags.HasJetPack | PathFinder.PotentialPath.Flags.HasOxygenMask);
		if (flag)
		{
			bool flag4 = path.HasFlag(flags2);
			if (flag2)
			{
				if (!flag3 && !Grid.HasSuit(from_cell, this.prefabInstanceID))
				{
					return false;
				}
			}
			else if (flag3 && (flags & Grid.SuitMarker.Flags.OnlyTraverseIfUnequipAvailable) != (Grid.SuitMarker.Flags)0 && (!flag4 || !Grid.HasEmptyLocker(from_cell, this.prefabInstanceID)))
			{
				return false;
			}
		}
		if (this.idleNavMaskEnabled && (Grid.PreventIdleTraversal[path.cell] || Grid.PreventIdleTraversal[from_cell]))
		{
			return false;
		}
		if (flag)
		{
			if (flag2)
			{
				if (!flag3)
				{
					path.SetFlags(flags2);
				}
			}
			else
			{
				path.ClearFlags(PathFinder.PotentialPath.Flags.HasAtmoSuit | PathFinder.PotentialPath.Flags.HasJetPack | PathFinder.PotentialPath.Flags.HasOxygenMask);
			}
		}
		return true;
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x0005EC98 File Offset: 0x0005CE98
	[Conditional("ENABLE_NAVIGATION_MASK_PROFILING")]
	private void BeginSample(string region_name)
	{
	}

	// Token: 0x06001201 RID: 4609 RVA: 0x0005EC9A File Offset: 0x0005CE9A
	[Conditional("ENABLE_NAVIGATION_MASK_PROFILING")]
	private void EndSample(string region_name)
	{
	}

	// Token: 0x040009A3 RID: 2467
	private CellOffset[][] transitionVoidOffsets;

	// Token: 0x040009A4 RID: 2468
	private int proxyID;

	// Token: 0x040009A5 RID: 2469
	private bool out_of_fuel;

	// Token: 0x040009A6 RID: 2470
	private bool idleNavMaskEnabled;
}
