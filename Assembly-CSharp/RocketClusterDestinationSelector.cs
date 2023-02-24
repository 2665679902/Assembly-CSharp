using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x0200095A RID: 2394
public class RocketClusterDestinationSelector : ClusterDestinationSelector
{
	// Token: 0x17000546 RID: 1350
	// (get) Token: 0x060046BB RID: 18107 RVA: 0x0018E22C File Offset: 0x0018C42C
	// (set) Token: 0x060046BC RID: 18108 RVA: 0x0018E234 File Offset: 0x0018C434
	public bool Repeat
	{
		get
		{
			return this.m_repeat;
		}
		set
		{
			this.m_repeat = value;
		}
	}

	// Token: 0x060046BD RID: 18109 RVA: 0x0018E23D File Offset: 0x0018C43D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<RocketClusterDestinationSelector>(-1277991738, this.OnLaunchDelegate);
	}

	// Token: 0x060046BE RID: 18110 RVA: 0x0018E257 File Offset: 0x0018C457
	protected override void OnSpawn()
	{
		if (this.isHarvesting)
		{
			this.WaitForPOIHarvest();
		}
	}

	// Token: 0x060046BF RID: 18111 RVA: 0x0018E268 File Offset: 0x0018C468
	public LaunchPad GetDestinationPad(AxialI destination)
	{
		int asteroidWorldIdAtLocation = ClusterUtil.GetAsteroidWorldIdAtLocation(destination);
		if (this.m_launchPad.ContainsKey(asteroidWorldIdAtLocation))
		{
			return this.m_launchPad[asteroidWorldIdAtLocation].Get();
		}
		return null;
	}

	// Token: 0x060046C0 RID: 18112 RVA: 0x0018E29D File Offset: 0x0018C49D
	public LaunchPad GetDestinationPad()
	{
		return this.GetDestinationPad(this.m_destination);
	}

	// Token: 0x060046C1 RID: 18113 RVA: 0x0018E2AB File Offset: 0x0018C4AB
	public override void SetDestination(AxialI location)
	{
		base.SetDestination(location);
	}

	// Token: 0x060046C2 RID: 18114 RVA: 0x0018E2B4 File Offset: 0x0018C4B4
	public void SetDestinationPad(LaunchPad pad)
	{
		Debug.Assert(pad == null || ClusterGrid.Instance.IsInRange(pad.GetMyWorldLocation(), this.m_destination, 1), "Tried sending a rocket to a launchpad that wasn't its destination world.");
		if (pad != null)
		{
			this.AddDestinationPad(pad.GetMyWorldLocation(), pad);
			base.SetDestination(pad.GetMyWorldLocation());
		}
		base.GetComponent<CraftModuleInterface>().TriggerEventOnCraftAndRocket(GameHashes.ClusterDestinationChanged, null);
	}

	// Token: 0x060046C3 RID: 18115 RVA: 0x0018E324 File Offset: 0x0018C524
	private void AddDestinationPad(AxialI location, LaunchPad pad)
	{
		int asteroidWorldIdAtLocation = ClusterUtil.GetAsteroidWorldIdAtLocation(location);
		if (asteroidWorldIdAtLocation < 0)
		{
			return;
		}
		if (!this.m_launchPad.ContainsKey(asteroidWorldIdAtLocation))
		{
			this.m_launchPad.Add(asteroidWorldIdAtLocation, new Ref<LaunchPad>());
		}
		this.m_launchPad[asteroidWorldIdAtLocation].Set(pad);
	}

	// Token: 0x060046C4 RID: 18116 RVA: 0x0018E370 File Offset: 0x0018C570
	protected override void OnClusterLocationChanged(object data)
	{
		ClusterLocationChangedEvent clusterLocationChangedEvent = (ClusterLocationChangedEvent)data;
		if (clusterLocationChangedEvent.newLocation == this.m_destination)
		{
			base.GetComponent<CraftModuleInterface>().TriggerEventOnCraftAndRocket(GameHashes.ClusterDestinationReached, null);
			if (this.m_repeat)
			{
				if (ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(clusterLocationChangedEvent.newLocation, EntityLayer.POI) != null && this.CanRocketHarvest())
				{
					this.WaitForPOIHarvest();
					return;
				}
				this.SetUpReturnTrip();
			}
		}
	}

	// Token: 0x060046C5 RID: 18117 RVA: 0x0018E3E4 File Offset: 0x0018C5E4
	private void SetUpReturnTrip()
	{
		this.AddDestinationPad(this.m_prevDestination, this.m_prevLaunchPad.Get());
		this.m_destination = this.m_prevDestination;
		this.m_prevDestination = base.GetComponent<Clustercraft>().Location;
		this.m_prevLaunchPad.Set(base.GetComponent<CraftModuleInterface>().CurrentPad);
	}

	// Token: 0x060046C6 RID: 18118 RVA: 0x0018E43C File Offset: 0x0018C63C
	private bool CanRocketHarvest()
	{
		bool flag = false;
		List<ResourceHarvestModule.StatesInstance> allResourceHarvestModules = base.GetComponent<Clustercraft>().GetAllResourceHarvestModules();
		if (allResourceHarvestModules.Count > 0)
		{
			using (List<ResourceHarvestModule.StatesInstance>.Enumerator enumerator = allResourceHarvestModules.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.CheckIfCanHarvest())
					{
						flag = true;
					}
				}
			}
		}
		if (!flag)
		{
			List<ArtifactHarvestModule.StatesInstance> allArtifactHarvestModules = base.GetComponent<Clustercraft>().GetAllArtifactHarvestModules();
			if (allArtifactHarvestModules.Count > 0)
			{
				using (List<ArtifactHarvestModule.StatesInstance>.Enumerator enumerator2 = allArtifactHarvestModules.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.CheckIfCanHarvest())
						{
							flag = true;
						}
					}
				}
			}
		}
		return flag;
	}

	// Token: 0x060046C7 RID: 18119 RVA: 0x0018E4FC File Offset: 0x0018C6FC
	private void OnStorageChange(object data)
	{
		if (!this.CanRocketHarvest())
		{
			this.isHarvesting = false;
			foreach (Ref<RocketModuleCluster> @ref in base.GetComponent<Clustercraft>().ModuleInterface.ClusterModules)
			{
				if (@ref.Get().GetComponent<Storage>())
				{
					base.Unsubscribe(@ref.Get().gameObject, -1697596308, new Action<object>(this.OnStorageChange));
				}
			}
			this.SetUpReturnTrip();
		}
	}

	// Token: 0x060046C8 RID: 18120 RVA: 0x0018E598 File Offset: 0x0018C798
	private void WaitForPOIHarvest()
	{
		this.isHarvesting = true;
		foreach (Ref<RocketModuleCluster> @ref in base.GetComponent<Clustercraft>().ModuleInterface.ClusterModules)
		{
			if (@ref.Get().GetComponent<Storage>())
			{
				base.Subscribe(@ref.Get().gameObject, -1697596308, new Action<object>(this.OnStorageChange));
			}
		}
	}

	// Token: 0x060046C9 RID: 18121 RVA: 0x0018E624 File Offset: 0x0018C824
	private void OnLaunch(object data)
	{
		CraftModuleInterface component = base.GetComponent<CraftModuleInterface>();
		this.m_prevLaunchPad.Set(component.CurrentPad);
		Clustercraft component2 = base.GetComponent<Clustercraft>();
		this.m_prevDestination = component2.Location;
	}

	// Token: 0x04002EC7 RID: 11975
	[Serialize]
	private Dictionary<int, Ref<LaunchPad>> m_launchPad = new Dictionary<int, Ref<LaunchPad>>();

	// Token: 0x04002EC8 RID: 11976
	[Serialize]
	private bool m_repeat;

	// Token: 0x04002EC9 RID: 11977
	[Serialize]
	private AxialI m_prevDestination;

	// Token: 0x04002ECA RID: 11978
	[Serialize]
	private Ref<LaunchPad> m_prevLaunchPad = new Ref<LaunchPad>();

	// Token: 0x04002ECB RID: 11979
	[Serialize]
	private bool isHarvesting;

	// Token: 0x04002ECC RID: 11980
	private EventSystem.IntraObjectHandler<RocketClusterDestinationSelector> OnLaunchDelegate = new EventSystem.IntraObjectHandler<RocketClusterDestinationSelector>(delegate(RocketClusterDestinationSelector cmp, object data)
	{
		cmp.OnLaunch(data);
	});
}
