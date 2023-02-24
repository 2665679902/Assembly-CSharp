using System;
using KSerialization;

// Token: 0x0200093C RID: 2364
public class ClusterDestinationSelector : KMonoBehaviour
{
	// Token: 0x06004541 RID: 17729 RVA: 0x001868F1 File Offset: 0x00184AF1
	protected override void OnPrefabInit()
	{
		base.Subscribe<ClusterDestinationSelector>(-1298331547, this.OnClusterLocationChangedDelegate);
	}

	// Token: 0x06004542 RID: 17730 RVA: 0x00186905 File Offset: 0x00184B05
	protected virtual void OnClusterLocationChanged(object data)
	{
		if (((ClusterLocationChangedEvent)data).newLocation == this.m_destination)
		{
			base.Trigger(1796608350, data);
		}
	}

	// Token: 0x06004543 RID: 17731 RVA: 0x0018692B File Offset: 0x00184B2B
	public int GetDestinationWorld()
	{
		return ClusterUtil.GetAsteroidWorldIdAtLocation(this.m_destination);
	}

	// Token: 0x06004544 RID: 17732 RVA: 0x00186938 File Offset: 0x00184B38
	public AxialI GetDestination()
	{
		return this.m_destination;
	}

	// Token: 0x06004545 RID: 17733 RVA: 0x00186940 File Offset: 0x00184B40
	public virtual void SetDestination(AxialI location)
	{
		if (this.requireAsteroidDestination)
		{
			Debug.Assert(ClusterUtil.GetAsteroidWorldIdAtLocation(location) != -1, string.Format("Cannot SetDestination to {0} as there is no world there", location));
		}
		this.m_destination = location;
		base.Trigger(543433792, location);
	}

	// Token: 0x06004546 RID: 17734 RVA: 0x0018698E File Offset: 0x00184B8E
	public bool HasAsteroidDestination()
	{
		return ClusterUtil.GetAsteroidWorldIdAtLocation(this.m_destination) != -1;
	}

	// Token: 0x06004547 RID: 17735 RVA: 0x001869A1 File Offset: 0x00184BA1
	public virtual bool IsAtDestination()
	{
		return this.GetMyWorldLocation() == this.m_destination;
	}

	// Token: 0x04002E2D RID: 11821
	[Serialize]
	protected AxialI m_destination;

	// Token: 0x04002E2E RID: 11822
	public bool assignable;

	// Token: 0x04002E2F RID: 11823
	public bool requireAsteroidDestination;

	// Token: 0x04002E30 RID: 11824
	[Serialize]
	public bool canNavigateFogOfWar;

	// Token: 0x04002E31 RID: 11825
	public bool requireLaunchPadOnAsteroidDestination;

	// Token: 0x04002E32 RID: 11826
	public bool shouldPointTowardsPath;

	// Token: 0x04002E33 RID: 11827
	private EventSystem.IntraObjectHandler<ClusterDestinationSelector> OnClusterLocationChangedDelegate = new EventSystem.IntraObjectHandler<ClusterDestinationSelector>(delegate(ClusterDestinationSelector cmp, object data)
	{
		cmp.OnClusterLocationChanged(data);
	});
}
