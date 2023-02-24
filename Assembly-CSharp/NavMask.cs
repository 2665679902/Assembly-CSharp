using System;

// Token: 0x020003C1 RID: 961
public class NavMask
{
	// Token: 0x060013EF RID: 5103 RVA: 0x00069AE6 File Offset: 0x00067CE6
	public virtual bool IsTraversable(PathFinder.PotentialPath path, int from_cell, int cost, int transition_id, PathFinderAbilities abilities)
	{
		return true;
	}

	// Token: 0x060013F0 RID: 5104 RVA: 0x00069AE9 File Offset: 0x00067CE9
	public virtual void ApplyTraversalToPath(ref PathFinder.PotentialPath path, int from_cell)
	{
	}
}
