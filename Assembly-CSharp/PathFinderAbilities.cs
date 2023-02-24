using System;

// Token: 0x020003C8 RID: 968
public abstract class PathFinderAbilities
{
	// Token: 0x0600140E RID: 5134 RVA: 0x0006A673 File Offset: 0x00068873
	public PathFinderAbilities(Navigator navigator)
	{
		this.navigator = navigator;
	}

	// Token: 0x0600140F RID: 5135 RVA: 0x0006A682 File Offset: 0x00068882
	public void Refresh()
	{
		this.prefabInstanceID = this.navigator.gameObject.GetComponent<KPrefabID>().InstanceID;
		this.Refresh(this.navigator);
	}

	// Token: 0x06001410 RID: 5136
	protected abstract void Refresh(Navigator navigator);

	// Token: 0x06001411 RID: 5137
	public abstract bool TraversePath(ref PathFinder.PotentialPath path, int from_cell, NavType from_nav_type, int cost, int transition_id, bool submerged);

	// Token: 0x06001412 RID: 5138 RVA: 0x0006A6AB File Offset: 0x000688AB
	public virtual int GetSubmergedPathCostPenalty(PathFinder.PotentialPath path, NavGrid.Link link)
	{
		return 0;
	}

	// Token: 0x04000B18 RID: 2840
	protected Navigator navigator;

	// Token: 0x04000B19 RID: 2841
	protected int prefabInstanceID;
}
