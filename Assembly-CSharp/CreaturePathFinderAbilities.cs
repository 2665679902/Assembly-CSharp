using System;
using Klei.AI;

// Token: 0x0200036E RID: 878
public class CreaturePathFinderAbilities : PathFinderAbilities
{
	// Token: 0x060011F7 RID: 4599 RVA: 0x0005EA1F File Offset: 0x0005CC1F
	public CreaturePathFinderAbilities(Navigator navigator)
		: base(navigator)
	{
	}

	// Token: 0x060011F8 RID: 4600 RVA: 0x0005EA28 File Offset: 0x0005CC28
	protected override void Refresh(Navigator navigator)
	{
		if (PathFinder.IsSubmerged(Grid.PosToCell(navigator)))
		{
			this.canTraverseSubmered = true;
			return;
		}
		AttributeInstance attributeInstance = Db.Get().Attributes.MaxUnderwaterTravelCost.Lookup(navigator);
		this.canTraverseSubmered = attributeInstance == null;
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x0005EA6A File Offset: 0x0005CC6A
	public override bool TraversePath(ref PathFinder.PotentialPath path, int from_cell, NavType from_nav_type, int cost, int transition_id, bool submerged)
	{
		return !submerged || this.canTraverseSubmered;
	}

	// Token: 0x040009A2 RID: 2466
	public bool canTraverseSubmered;
}
