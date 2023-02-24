using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200093F RID: 2367
public class Clustercraft : ClusterGridEntity, IClusterRange, ISim4000ms, ISim1000ms
{
	// Token: 0x170004FD RID: 1277
	// (get) Token: 0x06004566 RID: 17766 RVA: 0x001870E1 File Offset: 0x001852E1
	public override string Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x170004FE RID: 1278
	// (get) Token: 0x06004567 RID: 17767 RVA: 0x001870E9 File Offset: 0x001852E9
	// (set) Token: 0x06004568 RID: 17768 RVA: 0x001870F1 File Offset: 0x001852F1
	public bool Exploding { get; protected set; }

	// Token: 0x170004FF RID: 1279
	// (get) Token: 0x06004569 RID: 17769 RVA: 0x001870FA File Offset: 0x001852FA
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.Craft;
		}
	}

	// Token: 0x17000500 RID: 1280
	// (get) Token: 0x0600456A RID: 17770 RVA: 0x00187100 File Offset: 0x00185300
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>
			{
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim("rocket01_kanim"),
					initialAnim = "idle_loop"
				}
			};
		}
	}

	// Token: 0x0600456B RID: 17771 RVA: 0x00187143 File Offset: 0x00185343
	public override Sprite GetUISprite()
	{
		return Def.GetUISprite(this.m_moduleInterface.GetPassengerModule().gameObject, "ui", false).first;
	}

	// Token: 0x17000501 RID: 1281
	// (get) Token: 0x0600456C RID: 17772 RVA: 0x00187165 File Offset: 0x00185365
	public override bool IsVisible
	{
		get
		{
			return !this.Exploding;
		}
	}

	// Token: 0x17000502 RID: 1282
	// (get) Token: 0x0600456D RID: 17773 RVA: 0x00187170 File Offset: 0x00185370
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Visible;
		}
	}

	// Token: 0x0600456E RID: 17774 RVA: 0x00187173 File Offset: 0x00185373
	public override bool SpaceOutInSameHex()
	{
		return true;
	}

	// Token: 0x17000503 RID: 1283
	// (get) Token: 0x0600456F RID: 17775 RVA: 0x00187176 File Offset: 0x00185376
	public CraftModuleInterface ModuleInterface
	{
		get
		{
			return this.m_moduleInterface;
		}
	}

	// Token: 0x17000504 RID: 1284
	// (get) Token: 0x06004570 RID: 17776 RVA: 0x0018717E File Offset: 0x0018537E
	public AxialI Destination
	{
		get
		{
			return this.m_moduleInterface.GetClusterDestinationSelector().GetDestination();
		}
	}

	// Token: 0x17000505 RID: 1285
	// (get) Token: 0x06004571 RID: 17777 RVA: 0x00187190 File Offset: 0x00185390
	public float Speed
	{
		get
		{
			float num = this.EnginePower / this.TotalBurden;
			float num2 = num * this.AutoPilotMultiplier * this.PilotSkillMultiplier;
			if (this.controlStationBuffTimeRemaining > 0f)
			{
				num2 += num * 0.20000005f;
			}
			return num2;
		}
	}

	// Token: 0x17000506 RID: 1286
	// (get) Token: 0x06004572 RID: 17778 RVA: 0x001871D4 File Offset: 0x001853D4
	public float EnginePower
	{
		get
		{
			float num = 0f;
			foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
			{
				num += @ref.Get().performanceStats.EnginePower;
			}
			return num;
		}
	}

	// Token: 0x17000507 RID: 1287
	// (get) Token: 0x06004573 RID: 17779 RVA: 0x0018723C File Offset: 0x0018543C
	public float FuelPerDistance
	{
		get
		{
			float num = 0f;
			foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
			{
				num += @ref.Get().performanceStats.FuelKilogramPerDistance;
			}
			return num;
		}
	}

	// Token: 0x17000508 RID: 1288
	// (get) Token: 0x06004574 RID: 17780 RVA: 0x001872A4 File Offset: 0x001854A4
	public float TotalBurden
	{
		get
		{
			float num = 0f;
			foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
			{
				num += @ref.Get().performanceStats.Burden;
			}
			global::Debug.Assert(num > 0f);
			return num;
		}
	}

	// Token: 0x17000509 RID: 1289
	// (get) Token: 0x06004575 RID: 17781 RVA: 0x00187318 File Offset: 0x00185518
	// (set) Token: 0x06004576 RID: 17782 RVA: 0x00187320 File Offset: 0x00185520
	public bool LaunchRequested
	{
		get
		{
			return this.m_launchRequested;
		}
		private set
		{
			this.m_launchRequested = value;
			this.m_moduleInterface.TriggerEventOnCraftAndRocket(GameHashes.RocketRequestLaunch, this);
		}
	}

	// Token: 0x1700050A RID: 1290
	// (get) Token: 0x06004577 RID: 17783 RVA: 0x0018733A File Offset: 0x0018553A
	public Clustercraft.CraftStatus Status
	{
		get
		{
			return this.status;
		}
	}

	// Token: 0x06004578 RID: 17784 RVA: 0x00187342 File Offset: 0x00185542
	public void SetCraftStatus(Clustercraft.CraftStatus craft_status)
	{
		this.status = craft_status;
		this.UpdateGroundTags();
	}

	// Token: 0x06004579 RID: 17785 RVA: 0x00187351 File Offset: 0x00185551
	public void SetExploding()
	{
		this.Exploding = true;
	}

	// Token: 0x0600457A RID: 17786 RVA: 0x0018735A File Offset: 0x0018555A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.Clustercrafts.Add(this);
	}

	// Token: 0x0600457B RID: 17787 RVA: 0x00187370 File Offset: 0x00185570
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.m_clusterTraveler.getSpeedCB = new Func<float>(this.GetSpeed);
		this.m_clusterTraveler.getCanTravelCB = new Func<bool, bool>(this.CanTravel);
		this.m_clusterTraveler.onTravelCB = new System.Action(this.BurnFuelForTravel);
		this.m_clusterTraveler.validateTravelCB = new Func<AxialI, bool>(this.CanTravelToCell);
		this.UpdateGroundTags();
		base.Subscribe<Clustercraft>(1512695988, Clustercraft.RocketModuleChangedHandler);
		base.Subscribe<Clustercraft>(543433792, Clustercraft.ClusterDestinationChangedHandler);
		base.Subscribe<Clustercraft>(1796608350, Clustercraft.ClusterDestinationReachedHandler);
		base.Subscribe(-688990705, delegate(object o)
		{
			this.UpdateStatusItem();
		});
		base.Subscribe<Clustercraft>(1102426921, Clustercraft.NameChangedHandler);
		this.SetRocketName(this.m_name);
		this.UpdateStatusItem();
	}

	// Token: 0x0600457C RID: 17788 RVA: 0x00187454 File Offset: 0x00185654
	public void Sim1000ms(float dt)
	{
		this.controlStationBuffTimeRemaining = Mathf.Max(this.controlStationBuffTimeRemaining - dt, 0f);
		if (this.controlStationBuffTimeRemaining > 0f)
		{
			this.missionControlStatusHandle = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.MissionControlBoosted, this);
			return;
		}
		this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.MissionControlBoosted, false);
		this.missionControlStatusHandle = Guid.Empty;
	}

	// Token: 0x0600457D RID: 17789 RVA: 0x001874D0 File Offset: 0x001856D0
	public void Sim4000ms(float dt)
	{
		RocketClusterDestinationSelector clusterDestinationSelector = this.m_moduleInterface.GetClusterDestinationSelector();
		if (this.Status == Clustercraft.CraftStatus.InFlight && this.m_location == clusterDestinationSelector.GetDestination())
		{
			this.OnClusterDestinationReached(null);
		}
	}

	// Token: 0x0600457E RID: 17790 RVA: 0x0018750C File Offset: 0x0018570C
	public void Init(AxialI location, LaunchPad pad)
	{
		this.m_location = location;
		base.GetComponent<RocketClusterDestinationSelector>().SetDestination(this.m_location);
		this.SetRocketName(GameUtil.GenerateRandomRocketName());
		if (pad != null)
		{
			this.Land(pad, true);
		}
		this.UpdateStatusItem();
	}

	// Token: 0x0600457F RID: 17791 RVA: 0x00187548 File Offset: 0x00185748
	protected override void OnCleanUp()
	{
		Components.Clustercrafts.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06004580 RID: 17792 RVA: 0x0018755B File Offset: 0x0018575B
	private bool CanTravel(bool tryingToLand)
	{
		return this.HasTag(GameTags.RocketInSpace) && (tryingToLand || this.HasResourcesToMove(1, Clustercraft.CombustionResource.All));
	}

	// Token: 0x06004581 RID: 17793 RVA: 0x00187579 File Offset: 0x00185779
	private bool CanTravelToCell(AxialI location)
	{
		return !(ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(location, EntityLayer.Asteroid) != null) || this.CanLandAtAsteroid(location, true);
	}

	// Token: 0x06004582 RID: 17794 RVA: 0x00187599 File Offset: 0x00185799
	private float GetSpeed()
	{
		return this.Speed;
	}

	// Token: 0x06004583 RID: 17795 RVA: 0x001875A4 File Offset: 0x001857A4
	private void RocketModuleChanged(object data)
	{
		RocketModuleCluster rocketModuleCluster = (RocketModuleCluster)data;
		if (rocketModuleCluster != null)
		{
			this.UpdateGroundTags(rocketModuleCluster.gameObject);
		}
	}

	// Token: 0x06004584 RID: 17796 RVA: 0x001875CD File Offset: 0x001857CD
	private void OnClusterDestinationChanged(object data)
	{
		this.UpdateStatusItem();
	}

	// Token: 0x06004585 RID: 17797 RVA: 0x001875D8 File Offset: 0x001857D8
	private void OnClusterDestinationReached(object data)
	{
		RocketClusterDestinationSelector clusterDestinationSelector = this.m_moduleInterface.GetClusterDestinationSelector();
		global::Debug.Assert(base.Location == clusterDestinationSelector.GetDestination());
		if (clusterDestinationSelector.HasAsteroidDestination())
		{
			LaunchPad destinationPad = clusterDestinationSelector.GetDestinationPad();
			this.Land(base.Location, destinationPad);
		}
		this.UpdateStatusItem();
	}

	// Token: 0x06004586 RID: 17798 RVA: 0x00187629 File Offset: 0x00185829
	public void SetRocketName(object newName)
	{
		this.SetRocketName((string)newName);
	}

	// Token: 0x06004587 RID: 17799 RVA: 0x00187638 File Offset: 0x00185838
	public void SetRocketName(string newName)
	{
		this.m_name = newName;
		base.name = "Clustercraft: " + newName;
		foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
		{
			CharacterOverlay component = @ref.Get().GetComponent<CharacterOverlay>();
			if (component != null)
			{
				NameDisplayScreen.Instance.UpdateName(component.gameObject);
				break;
			}
		}
		ClusterManager.Instance.Trigger(1943181844, newName);
	}

	// Token: 0x06004588 RID: 17800 RVA: 0x001876D0 File Offset: 0x001858D0
	public bool CheckPreppedForLaunch()
	{
		return this.m_moduleInterface.CheckPreppedForLaunch();
	}

	// Token: 0x06004589 RID: 17801 RVA: 0x001876DD File Offset: 0x001858DD
	public bool CheckReadyToLaunch()
	{
		return this.m_moduleInterface.CheckReadyToLaunch();
	}

	// Token: 0x0600458A RID: 17802 RVA: 0x001876EA File Offset: 0x001858EA
	public bool IsFlightInProgress()
	{
		return this.Status == Clustercraft.CraftStatus.InFlight && this.m_clusterTraveler.IsTraveling();
	}

	// Token: 0x0600458B RID: 17803 RVA: 0x00187702 File Offset: 0x00185902
	public ClusterGridEntity GetPOIAtCurrentLocation()
	{
		if (this.status != Clustercraft.CraftStatus.InFlight || this.IsFlightInProgress())
		{
			return null;
		}
		return ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(this.m_location, EntityLayer.POI);
	}

	// Token: 0x0600458C RID: 17804 RVA: 0x00187728 File Offset: 0x00185928
	public ClusterGridEntity GetStableOrbitAsteroid()
	{
		if (this.status != Clustercraft.CraftStatus.InFlight || this.IsFlightInProgress())
		{
			return null;
		}
		return ClusterGrid.Instance.GetVisibleEntityOfLayerAtAdjacentCell(this.m_location, EntityLayer.Asteroid);
	}

	// Token: 0x0600458D RID: 17805 RVA: 0x0018774E File Offset: 0x0018594E
	public ClusterGridEntity GetOrbitAsteroid()
	{
		if (this.status != Clustercraft.CraftStatus.InFlight)
		{
			return null;
		}
		return ClusterGrid.Instance.GetVisibleEntityOfLayerAtAdjacentCell(this.m_location, EntityLayer.Asteroid);
	}

	// Token: 0x0600458E RID: 17806 RVA: 0x0018776C File Offset: 0x0018596C
	public ClusterGridEntity GetAdjacentAsteroid()
	{
		return ClusterGrid.Instance.GetVisibleEntityOfLayerAtAdjacentCell(this.m_location, EntityLayer.Asteroid);
	}

	// Token: 0x0600458F RID: 17807 RVA: 0x0018777F File Offset: 0x0018597F
	private bool CheckDesinationInRange()
	{
		return this.m_clusterTraveler.CurrentPath != null && this.Speed * this.m_clusterTraveler.TravelETA() <= this.ModuleInterface.Range;
	}

	// Token: 0x06004590 RID: 17808 RVA: 0x001877B4 File Offset: 0x001859B4
	public bool HasResourcesToMove(int hexes = 1, Clustercraft.CombustionResource combustionResource = Clustercraft.CombustionResource.All)
	{
		switch (combustionResource)
		{
		case Clustercraft.CombustionResource.Fuel:
			return this.m_moduleInterface.FuelRemaining / this.FuelPerDistance >= 600f * (float)hexes - 0.001f;
		case Clustercraft.CombustionResource.Oxidizer:
			return this.m_moduleInterface.OxidizerPowerRemaining / this.FuelPerDistance >= 600f * (float)hexes - 0.001f;
		case Clustercraft.CombustionResource.All:
			return this.m_moduleInterface.BurnableMassRemaining / this.FuelPerDistance >= 600f * (float)hexes - 0.001f;
		default:
			return false;
		}
	}

	// Token: 0x06004591 RID: 17809 RVA: 0x00187848 File Offset: 0x00185A48
	private void BurnFuelForTravel()
	{
		float num = 600f;
		foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
		{
			RocketEngineCluster component = @ref.Get().GetComponent<RocketEngineCluster>();
			if (component != null)
			{
				Tag fuelTag = component.fuelTag;
				float num2 = 0f;
				if (component.requireOxidizer)
				{
					num2 = this.ModuleInterface.OxidizerPowerRemaining;
				}
				if (num > 0f)
				{
					foreach (Ref<RocketModuleCluster> ref2 in this.m_moduleInterface.ClusterModules)
					{
						IFuelTank component2 = ref2.Get().GetComponent<IFuelTank>();
						if (!component2.IsNullOrDestroyed())
						{
							num -= this.BurnFromTank(num, component, fuelTag, component2.Storage, ref num2);
						}
						if (num <= 0f)
						{
							break;
						}
					}
				}
			}
		}
		this.UpdateStatusItem();
	}

	// Token: 0x06004592 RID: 17810 RVA: 0x00187958 File Offset: 0x00185B58
	private float BurnFromTank(float attemptTravelAmount, RocketEngineCluster engine, Tag fuelTag, IStorage storage, ref float totalOxidizerRemaining)
	{
		float num = attemptTravelAmount * engine.GetComponent<RocketModuleCluster>().performanceStats.FuelKilogramPerDistance;
		num = Mathf.Min(storage.GetAmountAvailable(fuelTag), num);
		if (engine.requireOxidizer)
		{
			num = Mathf.Min(num, totalOxidizerRemaining);
		}
		storage.ConsumeIgnoringDisease(fuelTag, num);
		if (engine.requireOxidizer)
		{
			this.BurnOxidizer(num);
			totalOxidizerRemaining -= num;
		}
		return num / engine.GetComponent<RocketModuleCluster>().performanceStats.FuelKilogramPerDistance;
	}

	// Token: 0x06004593 RID: 17811 RVA: 0x001879CC File Offset: 0x00185BCC
	private void BurnOxidizer(float fuelEquivalentKGs)
	{
		foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
		{
			OxidizerTank component = @ref.Get().GetComponent<OxidizerTank>();
			if (component != null)
			{
				foreach (KeyValuePair<Tag, float> keyValuePair in component.GetOxidizersAvailable())
				{
					float num = Clustercraft.dlc1OxidizerEfficiencies[keyValuePair.Key];
					float num2 = Mathf.Min(fuelEquivalentKGs / num, keyValuePair.Value);
					if (num2 > 0f)
					{
						component.storage.ConsumeIgnoringDisease(keyValuePair.Key, num2);
						fuelEquivalentKGs -= num2 * num;
					}
				}
			}
			if (fuelEquivalentKGs <= 0f)
			{
				break;
			}
		}
	}

	// Token: 0x06004594 RID: 17812 RVA: 0x00187AC0 File Offset: 0x00185CC0
	public List<ResourceHarvestModule.StatesInstance> GetAllResourceHarvestModules()
	{
		List<ResourceHarvestModule.StatesInstance> list = new List<ResourceHarvestModule.StatesInstance>();
		foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
		{
			ResourceHarvestModule.StatesInstance smi = @ref.Get().GetSMI<ResourceHarvestModule.StatesInstance>();
			if (smi != null)
			{
				list.Add(smi);
			}
		}
		return list;
	}

	// Token: 0x06004595 RID: 17813 RVA: 0x00187B28 File Offset: 0x00185D28
	public List<ArtifactHarvestModule.StatesInstance> GetAllArtifactHarvestModules()
	{
		List<ArtifactHarvestModule.StatesInstance> list = new List<ArtifactHarvestModule.StatesInstance>();
		foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
		{
			ArtifactHarvestModule.StatesInstance smi = @ref.Get().GetSMI<ArtifactHarvestModule.StatesInstance>();
			if (smi != null)
			{
				list.Add(smi);
			}
		}
		return list;
	}

	// Token: 0x06004596 RID: 17814 RVA: 0x00187B90 File Offset: 0x00185D90
	public List<CargoBayCluster> GetAllCargoBays()
	{
		List<CargoBayCluster> list = new List<CargoBayCluster>();
		foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
		{
			CargoBayCluster component = @ref.Get().GetComponent<CargoBayCluster>();
			if (component != null)
			{
				list.Add(component);
			}
		}
		return list;
	}

	// Token: 0x06004597 RID: 17815 RVA: 0x00187BFC File Offset: 0x00185DFC
	public List<CargoBayCluster> GetCargoBaysOfType(CargoBay.CargoType cargoType)
	{
		List<CargoBayCluster> list = new List<CargoBayCluster>();
		foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
		{
			CargoBayCluster component = @ref.Get().GetComponent<CargoBayCluster>();
			if (component != null && component.storageType == cargoType)
			{
				list.Add(component);
			}
		}
		return list;
	}

	// Token: 0x06004598 RID: 17816 RVA: 0x00187C74 File Offset: 0x00185E74
	public void DestroyCraftAndModules()
	{
		List<RocketModuleCluster> list = this.m_moduleInterface.ClusterModules.Select((Ref<RocketModuleCluster> x) => x.Get()).ToList<RocketModuleCluster>();
		for (int i = list.Count - 1; i >= 0; i--)
		{
			RocketModuleCluster rocketModuleCluster = list[i];
			Storage component = rocketModuleCluster.GetComponent<Storage>();
			if (component != null)
			{
				component.ConsumeAllIgnoringDisease();
			}
			MinionStorage component2 = rocketModuleCluster.GetComponent<MinionStorage>();
			if (component2 != null)
			{
				List<MinionStorage.Info> storedMinionInfo = component2.GetStoredMinionInfo();
				for (int j = storedMinionInfo.Count - 1; j >= 0; j--)
				{
					component2.DeleteStoredMinion(storedMinionInfo[j].id);
				}
			}
			Util.KDestroyGameObject(rocketModuleCluster.gameObject);
		}
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06004599 RID: 17817 RVA: 0x00187D45 File Offset: 0x00185F45
	public void CancelLaunch()
	{
		if (this.LaunchRequested)
		{
			global::Debug.Log("Cancelling launch!");
			this.LaunchRequested = false;
		}
	}

	// Token: 0x0600459A RID: 17818 RVA: 0x00187D60 File Offset: 0x00185F60
	public void RequestLaunch(bool automated = false)
	{
		if (this.HasTag(GameTags.RocketNotOnGround) || this.m_moduleInterface.GetClusterDestinationSelector().IsAtDestination())
		{
			return;
		}
		if (DebugHandler.InstantBuildMode && !automated)
		{
			this.Launch(false);
		}
		if (this.LaunchRequested)
		{
			return;
		}
		if (!this.CheckPreppedForLaunch())
		{
			return;
		}
		global::Debug.Log("Triggering launch!");
		this.LaunchRequested = true;
	}

	// Token: 0x0600459B RID: 17819 RVA: 0x00187DC4 File Offset: 0x00185FC4
	public void Launch(bool automated = false)
	{
		if (this.HasTag(GameTags.RocketNotOnGround) || this.m_moduleInterface.GetClusterDestinationSelector().IsAtDestination())
		{
			this.LaunchRequested = false;
			return;
		}
		if ((!DebugHandler.InstantBuildMode || automated) && !this.CheckReadyToLaunch())
		{
			return;
		}
		if (automated && !this.m_moduleInterface.CheckReadyForAutomatedLaunchCommand())
		{
			this.LaunchRequested = false;
			return;
		}
		this.LaunchRequested = false;
		this.SetCraftStatus(Clustercraft.CraftStatus.Launching);
		this.m_moduleInterface.DoLaunch();
		this.BurnFuelForTravel();
		this.m_clusterTraveler.AdvancePathOneStep();
		this.UpdateStatusItem();
	}

	// Token: 0x0600459C RID: 17820 RVA: 0x00187E54 File Offset: 0x00186054
	public void LandAtPad(LaunchPad pad)
	{
		this.m_moduleInterface.GetClusterDestinationSelector().SetDestinationPad(pad);
	}

	// Token: 0x0600459D RID: 17821 RVA: 0x00187E68 File Offset: 0x00186068
	public Clustercraft.PadLandingStatus CanLandAtPad(LaunchPad pad, out string failReason)
	{
		if (pad == null)
		{
			failReason = UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.NONEAVAILABLE;
			return Clustercraft.PadLandingStatus.CanNeverLand;
		}
		if (pad.HasRocket() && pad.LandedRocket.CraftInterface != this.m_moduleInterface)
		{
			failReason = "<TEMP>The pad already has a rocket on it!<TEMP>";
			return Clustercraft.PadLandingStatus.CanLandEventually;
		}
		if (ConditionFlightPathIsClear.PadTopEdgeDistanceToCeilingEdge(pad.gameObject) < this.ModuleInterface.RocketHeight)
		{
			failReason = UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.DROPDOWN_TOOLTIP_TOO_SHORT;
			return Clustercraft.PadLandingStatus.CanNeverLand;
		}
		int num = -1;
		if (!ConditionFlightPathIsClear.CheckFlightPathClear(this.ModuleInterface, pad.gameObject, out num))
		{
			failReason = string.Format(UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.DROPDOWN_TOOLTIP_PATH_OBSTRUCTED, pad.GetProperName());
			return Clustercraft.PadLandingStatus.CanNeverLand;
		}
		if (!pad.GetComponent<Operational>().IsOperational)
		{
			failReason = UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.DROPDOWN_TOOLTIP_PAD_DISABLED;
			return Clustercraft.PadLandingStatus.CanNeverLand;
		}
		int rocketBottomPosition = pad.RocketBottomPosition;
		foreach (Ref<RocketModuleCluster> @ref in this.ModuleInterface.ClusterModules)
		{
			GameObject gameObject = @ref.Get().gameObject;
			int moduleRelativeVerticalPosition = this.ModuleInterface.GetModuleRelativeVerticalPosition(gameObject);
			Building component = gameObject.GetComponent<Building>();
			BuildingUnderConstruction component2 = gameObject.GetComponent<BuildingUnderConstruction>();
			BuildingDef buildingDef = ((component != null) ? component.Def : component2.Def);
			for (int i = 0; i < buildingDef.WidthInCells; i++)
			{
				for (int j = 0; j < buildingDef.HeightInCells; j++)
				{
					int num2 = Grid.OffsetCell(rocketBottomPosition, 0, moduleRelativeVerticalPosition);
					num2 = Grid.OffsetCell(num2, -(buildingDef.WidthInCells / 2) + i, j);
					if (Grid.Solid[num2])
					{
						num = num2;
						failReason = string.Format(UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.DROPDOWN_TOOLTIP_SITE_OBSTRUCTED, pad.GetProperName());
						return Clustercraft.PadLandingStatus.CanNeverLand;
					}
				}
			}
		}
		failReason = null;
		return Clustercraft.PadLandingStatus.CanLandImmediately;
	}

	// Token: 0x0600459E RID: 17822 RVA: 0x00188038 File Offset: 0x00186238
	private LaunchPad FindValidLandingPad(AxialI location, bool mustLandImmediately)
	{
		LaunchPad launchPad = null;
		int asteroidWorldIdAtLocation = ClusterUtil.GetAsteroidWorldIdAtLocation(location);
		LaunchPad preferredLaunchPadForWorld = this.m_moduleInterface.GetPreferredLaunchPadForWorld(asteroidWorldIdAtLocation);
		string text;
		if (preferredLaunchPadForWorld != null && this.CanLandAtPad(preferredLaunchPadForWorld, out text) == Clustercraft.PadLandingStatus.CanLandImmediately)
		{
			return preferredLaunchPadForWorld;
		}
		foreach (object obj in Components.LaunchPads)
		{
			LaunchPad launchPad2 = (LaunchPad)obj;
			if (launchPad2.GetMyWorldLocation() == location)
			{
				string text2;
				Clustercraft.PadLandingStatus padLandingStatus = this.CanLandAtPad(launchPad2, out text2);
				if (padLandingStatus == Clustercraft.PadLandingStatus.CanLandImmediately)
				{
					return launchPad2;
				}
				if (!mustLandImmediately && padLandingStatus == Clustercraft.PadLandingStatus.CanLandEventually)
				{
					launchPad = launchPad2;
				}
			}
		}
		return launchPad;
	}

	// Token: 0x0600459F RID: 17823 RVA: 0x001880F4 File Offset: 0x001862F4
	public bool CanLandAtAsteroid(AxialI location, bool mustLandImmediately)
	{
		LaunchPad destinationPad = this.m_moduleInterface.GetClusterDestinationSelector().GetDestinationPad();
		global::Debug.Assert(destinationPad == null || destinationPad.GetMyWorldLocation() == location, "A rocket is trying to travel to an asteroid but has selected a landing pad at a different asteroid!");
		if (destinationPad != null)
		{
			string text;
			Clustercraft.PadLandingStatus padLandingStatus = this.CanLandAtPad(destinationPad, out text);
			return padLandingStatus == Clustercraft.PadLandingStatus.CanLandImmediately || (!mustLandImmediately && padLandingStatus == Clustercraft.PadLandingStatus.CanLandEventually);
		}
		return this.FindValidLandingPad(location, mustLandImmediately) != null;
	}

	// Token: 0x060045A0 RID: 17824 RVA: 0x00188164 File Offset: 0x00186364
	private void Land(LaunchPad pad, bool forceGrounded)
	{
		string text;
		if (this.CanLandAtPad(pad, out text) != Clustercraft.PadLandingStatus.CanLandImmediately)
		{
			return;
		}
		this.BurnFuelForTravel();
		this.m_location = pad.GetMyWorldLocation();
		this.SetCraftStatus(forceGrounded ? Clustercraft.CraftStatus.Grounded : Clustercraft.CraftStatus.Landing);
		this.m_moduleInterface.DoLand(pad);
		this.UpdateStatusItem();
	}

	// Token: 0x060045A1 RID: 17825 RVA: 0x001881B0 File Offset: 0x001863B0
	private void Land(AxialI destination, LaunchPad chosenPad)
	{
		if (chosenPad == null)
		{
			chosenPad = this.FindValidLandingPad(destination, true);
		}
		global::Debug.Assert(chosenPad == null || chosenPad.GetMyWorldLocation() == this.m_location, "Attempting to land on a pad that isn't at our current position");
		this.Land(chosenPad, false);
	}

	// Token: 0x060045A2 RID: 17826 RVA: 0x00188200 File Offset: 0x00186400
	public void UpdateStatusItem()
	{
		if (ClusterGrid.Instance == null)
		{
			return;
		}
		if (this.mainStatusHandle != Guid.Empty)
		{
			this.selectable.RemoveStatusItem(this.mainStatusHandle, false);
		}
		ClusterGridEntity visibleEntityOfLayerAtCell = ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(this.m_location, EntityLayer.Asteroid);
		ClusterGridEntity orbitAsteroid = this.GetOrbitAsteroid();
		bool flag = false;
		if (orbitAsteroid != null)
		{
			using (IEnumerator enumerator = Components.LaunchPads.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((LaunchPad)enumerator.Current).GetMyWorldLocation() == orbitAsteroid.Location)
					{
						flag = true;
						break;
					}
				}
			}
		}
		bool flag2 = false;
		if (visibleEntityOfLayerAtCell != null)
		{
			this.mainStatusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.InFlight, this.m_clusterTraveler);
		}
		else if (!this.HasResourcesToMove(1, Clustercraft.CombustionResource.All) && !flag)
		{
			flag2 = true;
			this.mainStatusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.RocketStranded, orbitAsteroid);
		}
		else if (!this.m_moduleInterface.GetClusterDestinationSelector().IsAtDestination() && !this.CheckDesinationInRange())
		{
			this.mainStatusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.DestinationOutOfRange, this.m_clusterTraveler);
		}
		else if (this.IsFlightInProgress() || this.Status == Clustercraft.CraftStatus.Launching)
		{
			this.mainStatusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.InFlight, this.m_clusterTraveler);
		}
		else if (orbitAsteroid != null)
		{
			this.mainStatusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.InOrbit, orbitAsteroid);
		}
		else
		{
			this.mainStatusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Normal, null);
		}
		base.GetComponent<KPrefabID>().SetTag(GameTags.RocketStranded, flag2);
		float num = 0f;
		float num2 = 0f;
		foreach (CargoBayCluster cargoBayCluster in this.GetAllCargoBays())
		{
			num += cargoBayCluster.MaxCapacity;
			num2 += cargoBayCluster.RemainingCapacity;
		}
		if (this.Status == Clustercraft.CraftStatus.Grounded || num <= 0f)
		{
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.FlightCargoRemaining, false);
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.FlightAllCargoFull, false);
			return;
		}
		if (num2 == 0f)
		{
			this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.FlightAllCargoFull, null);
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.FlightCargoRemaining, false);
			return;
		}
		this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.FlightAllCargoFull, false);
		if (this.cargoStatusHandle == Guid.Empty)
		{
			this.cargoStatusHandle = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.FlightCargoRemaining, num2);
			return;
		}
		this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.FlightCargoRemaining, true);
		this.cargoStatusHandle = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.FlightCargoRemaining, num2);
	}

	// Token: 0x060045A3 RID: 17827 RVA: 0x001885F0 File Offset: 0x001867F0
	private void UpdateGroundTags()
	{
		foreach (Ref<RocketModuleCluster> @ref in this.ModuleInterface.ClusterModules)
		{
			if (@ref != null && !(@ref.Get() == null))
			{
				this.UpdateGroundTags(@ref.Get().gameObject);
			}
		}
		this.UpdateGroundTags(base.gameObject);
	}

	// Token: 0x060045A4 RID: 17828 RVA: 0x0018866C File Offset: 0x0018686C
	private void UpdateGroundTags(GameObject go)
	{
		this.SetTagOnGameObject(go, GameTags.RocketOnGround, this.status == Clustercraft.CraftStatus.Grounded);
		this.SetTagOnGameObject(go, GameTags.RocketNotOnGround, this.status > Clustercraft.CraftStatus.Grounded);
		this.SetTagOnGameObject(go, GameTags.RocketInSpace, this.status == Clustercraft.CraftStatus.InFlight);
		this.SetTagOnGameObject(go, GameTags.EntityInSpace, this.status == Clustercraft.CraftStatus.InFlight);
	}

	// Token: 0x060045A5 RID: 17829 RVA: 0x001886CD File Offset: 0x001868CD
	private void SetTagOnGameObject(GameObject go, Tag tag, bool set)
	{
		if (set)
		{
			go.AddTag(tag);
			return;
		}
		go.RemoveTag(tag);
	}

	// Token: 0x060045A6 RID: 17830 RVA: 0x001886E1 File Offset: 0x001868E1
	public override bool ShowName()
	{
		return this.status > Clustercraft.CraftStatus.Grounded;
	}

	// Token: 0x060045A7 RID: 17831 RVA: 0x001886EC File Offset: 0x001868EC
	public override bool ShowPath()
	{
		return this.status > Clustercraft.CraftStatus.Grounded;
	}

	// Token: 0x060045A8 RID: 17832 RVA: 0x001886F7 File Offset: 0x001868F7
	public bool IsTravellingAndFueled()
	{
		return this.HasResourcesToMove(1, Clustercraft.CombustionResource.All) && this.m_clusterTraveler.IsTraveling();
	}

	// Token: 0x060045A9 RID: 17833 RVA: 0x00188710 File Offset: 0x00186910
	public override bool ShowProgressBar()
	{
		return this.IsTravellingAndFueled();
	}

	// Token: 0x060045AA RID: 17834 RVA: 0x00188718 File Offset: 0x00186918
	public override float GetProgress()
	{
		return this.m_clusterTraveler.GetMoveProgress();
	}

	// Token: 0x060045AB RID: 17835 RVA: 0x00188728 File Offset: 0x00186928
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.Status != Clustercraft.CraftStatus.Grounded && SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 27))
		{
			UIScheduler.Instance.ScheduleNextFrame("Check Fuel Costs", delegate(object o)
			{
				foreach (Ref<RocketModuleCluster> @ref in this.ModuleInterface.ClusterModules)
				{
					RocketModuleCluster rocketModuleCluster = @ref.Get();
					IFuelTank component = rocketModuleCluster.GetComponent<IFuelTank>();
					if (component != null && !component.Storage.IsEmpty())
					{
						component.DEBUG_FillTank();
					}
					OxidizerTank component2 = rocketModuleCluster.GetComponent<OxidizerTank>();
					if (component2 != null)
					{
						Dictionary<Tag, float> oxidizersAvailable = component2.GetOxidizersAvailable();
						if (oxidizersAvailable.Count > 0)
						{
							foreach (KeyValuePair<Tag, float> keyValuePair in oxidizersAvailable)
							{
								if (keyValuePair.Value > 0f)
								{
									component2.DEBUG_FillTank(ElementLoader.GetElementID(keyValuePair.Key));
									break;
								}
							}
						}
					}
				}
			}, null, null);
		}
	}

	// Token: 0x060045AC RID: 17836 RVA: 0x00188772 File Offset: 0x00186972
	public float GetRange()
	{
		return this.ModuleInterface.Range;
	}

	// Token: 0x04002E44 RID: 11844
	[Serialize]
	private string m_name;

	// Token: 0x04002E46 RID: 11846
	[MyCmpReq]
	private ClusterTraveler m_clusterTraveler;

	// Token: 0x04002E47 RID: 11847
	[MyCmpReq]
	private CraftModuleInterface m_moduleInterface;

	// Token: 0x04002E48 RID: 11848
	private Guid mainStatusHandle;

	// Token: 0x04002E49 RID: 11849
	private Guid cargoStatusHandle;

	// Token: 0x04002E4A RID: 11850
	private Guid missionControlStatusHandle = Guid.Empty;

	// Token: 0x04002E4B RID: 11851
	public static Dictionary<Tag, float> dlc1OxidizerEfficiencies = new Dictionary<Tag, float>
	{
		{
			SimHashes.OxyRock.CreateTag(),
			ROCKETRY.DLC1_OXIDIZER_EFFICIENCY.LOW
		},
		{
			SimHashes.LiquidOxygen.CreateTag(),
			ROCKETRY.DLC1_OXIDIZER_EFFICIENCY.HIGH
		},
		{
			SimHashes.Fertilizer.CreateTag(),
			ROCKETRY.DLC1_OXIDIZER_EFFICIENCY.VERY_LOW
		}
	};

	// Token: 0x04002E4C RID: 11852
	[Serialize]
	[Range(0f, 1f)]
	public float AutoPilotMultiplier = 1f;

	// Token: 0x04002E4D RID: 11853
	[Serialize]
	[Range(0f, 2f)]
	public float PilotSkillMultiplier = 1f;

	// Token: 0x04002E4E RID: 11854
	[Serialize]
	public float controlStationBuffTimeRemaining;

	// Token: 0x04002E4F RID: 11855
	[Serialize]
	private bool m_launchRequested;

	// Token: 0x04002E50 RID: 11856
	[Serialize]
	private Clustercraft.CraftStatus status;

	// Token: 0x04002E51 RID: 11857
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x04002E52 RID: 11858
	private static EventSystem.IntraObjectHandler<Clustercraft> RocketModuleChangedHandler = new EventSystem.IntraObjectHandler<Clustercraft>(delegate(Clustercraft cmp, object data)
	{
		cmp.RocketModuleChanged(data);
	});

	// Token: 0x04002E53 RID: 11859
	private static EventSystem.IntraObjectHandler<Clustercraft> ClusterDestinationChangedHandler = new EventSystem.IntraObjectHandler<Clustercraft>(delegate(Clustercraft cmp, object data)
	{
		cmp.OnClusterDestinationChanged(data);
	});

	// Token: 0x04002E54 RID: 11860
	private static EventSystem.IntraObjectHandler<Clustercraft> ClusterDestinationReachedHandler = new EventSystem.IntraObjectHandler<Clustercraft>(delegate(Clustercraft cmp, object data)
	{
		cmp.OnClusterDestinationReached(data);
	});

	// Token: 0x04002E55 RID: 11861
	private static EventSystem.IntraObjectHandler<Clustercraft> NameChangedHandler = new EventSystem.IntraObjectHandler<Clustercraft>(delegate(Clustercraft cmp, object data)
	{
		cmp.SetRocketName(data);
	});

	// Token: 0x0200172E RID: 5934
	public enum CraftStatus
	{
		// Token: 0x04006C46 RID: 27718
		Grounded,
		// Token: 0x04006C47 RID: 27719
		Launching,
		// Token: 0x04006C48 RID: 27720
		InFlight,
		// Token: 0x04006C49 RID: 27721
		Landing
	}

	// Token: 0x0200172F RID: 5935
	public enum CombustionResource
	{
		// Token: 0x04006C4B RID: 27723
		Fuel,
		// Token: 0x04006C4C RID: 27724
		Oxidizer,
		// Token: 0x04006C4D RID: 27725
		All
	}

	// Token: 0x02001730 RID: 5936
	public enum PadLandingStatus
	{
		// Token: 0x04006C4F RID: 27727
		CanLandImmediately,
		// Token: 0x04006C50 RID: 27728
		CanLandEventually,
		// Token: 0x04006C51 RID: 27729
		CanNeverLand
	}
}
