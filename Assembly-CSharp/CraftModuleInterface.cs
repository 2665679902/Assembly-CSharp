using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000944 RID: 2372
[SerializationConfig(MemberSerialization.OptIn)]
public class CraftModuleInterface : KMonoBehaviour, ISim4000ms
{
	// Token: 0x1700050D RID: 1293
	// (get) Token: 0x060045E1 RID: 17889 RVA: 0x001897A0 File Offset: 0x001879A0
	public IList<Ref<RocketModuleCluster>> ClusterModules
	{
		get
		{
			return this.clusterModules.AsReadOnly();
		}
	}

	// Token: 0x060045E2 RID: 17890 RVA: 0x001897AD File Offset: 0x001879AD
	public LaunchPad GetPreferredLaunchPadForWorld(int world_id)
	{
		if (this.preferredLaunchPad.ContainsKey(world_id))
		{
			return this.preferredLaunchPad[world_id].Get();
		}
		return null;
	}

	// Token: 0x060045E3 RID: 17891 RVA: 0x001897D0 File Offset: 0x001879D0
	private void SetPreferredLaunchPadForWorld(LaunchPad pad)
	{
		if (!this.preferredLaunchPad.ContainsKey(pad.GetMyWorldId()))
		{
			this.preferredLaunchPad.Add(this.CurrentPad.GetMyWorldId(), new Ref<LaunchPad>());
		}
		this.preferredLaunchPad[this.CurrentPad.GetMyWorldId()].Set(this.CurrentPad);
	}

	// Token: 0x1700050E RID: 1294
	// (get) Token: 0x060045E4 RID: 17892 RVA: 0x0018982C File Offset: 0x00187A2C
	public LaunchPad CurrentPad
	{
		get
		{
			if (this.m_clustercraft != null && this.m_clustercraft.Status != Clustercraft.CraftStatus.InFlight && this.clusterModules.Count > 0)
			{
				if (this.bottomModule == null)
				{
					this.SetBottomModule();
				}
				global::Debug.Assert(this.bottomModule != null && this.bottomModule.Get() != null, "More than one cluster module but no bottom module found.");
				int num = Grid.CellBelow(Grid.PosToCell(this.bottomModule.Get().transform.position));
				if (Grid.IsValidCell(num))
				{
					GameObject gameObject = null;
					Grid.ObjectLayers[1].TryGetValue(num, out gameObject);
					if (gameObject != null)
					{
						return gameObject.GetComponent<LaunchPad>();
					}
				}
			}
			return null;
		}
	}

	// Token: 0x1700050F RID: 1295
	// (get) Token: 0x060045E5 RID: 17893 RVA: 0x001898E8 File Offset: 0x00187AE8
	public float Speed
	{
		get
		{
			return this.m_clustercraft.Speed;
		}
	}

	// Token: 0x17000510 RID: 1296
	// (get) Token: 0x060045E6 RID: 17894 RVA: 0x001898F8 File Offset: 0x00187AF8
	public float Range
	{
		get
		{
			foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
			{
				RocketEngineCluster component = @ref.Get().GetComponent<RocketEngineCluster>();
				if (component != null)
				{
					return this.BurnableMassRemaining / component.GetComponent<RocketModuleCluster>().performanceStats.FuelKilogramPerDistance;
				}
			}
			return 0f;
		}
	}

	// Token: 0x17000511 RID: 1297
	// (get) Token: 0x060045E7 RID: 17895 RVA: 0x00189978 File Offset: 0x00187B78
	public float FuelPerHex
	{
		get
		{
			foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
			{
				RocketEngineCluster component = @ref.Get().GetComponent<RocketEngineCluster>();
				if (component != null)
				{
					return component.GetComponent<RocketModuleCluster>().performanceStats.FuelKilogramPerDistance * 600f;
				}
			}
			return float.PositiveInfinity;
		}
	}

	// Token: 0x17000512 RID: 1298
	// (get) Token: 0x060045E8 RID: 17896 RVA: 0x001899F8 File Offset: 0x00187BF8
	public float BurnableMassRemaining
	{
		get
		{
			RocketEngineCluster rocketEngineCluster = null;
			foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
			{
				rocketEngineCluster = @ref.Get().GetComponent<RocketEngineCluster>();
				if (rocketEngineCluster != null)
				{
					break;
				}
			}
			if (rocketEngineCluster == null)
			{
				return 0f;
			}
			if (!rocketEngineCluster.requireOxidizer)
			{
				return this.FuelRemaining;
			}
			return Mathf.Min(this.FuelRemaining, this.OxidizerPowerRemaining);
		}
	}

	// Token: 0x17000513 RID: 1299
	// (get) Token: 0x060045E9 RID: 17897 RVA: 0x00189A8C File Offset: 0x00187C8C
	public float FuelRemaining
	{
		get
		{
			RocketEngineCluster engine = this.GetEngine();
			if (engine == null)
			{
				return 0f;
			}
			float num = 0f;
			foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
			{
				IFuelTank component = @ref.Get().GetComponent<IFuelTank>();
				if (!component.IsNullOrDestroyed())
				{
					num += component.Storage.GetAmountAvailable(engine.fuelTag);
				}
			}
			return (float)Mathf.CeilToInt(num);
		}
	}

	// Token: 0x17000514 RID: 1300
	// (get) Token: 0x060045EA RID: 17898 RVA: 0x00189B24 File Offset: 0x00187D24
	public float OxidizerPowerRemaining
	{
		get
		{
			float num = 0f;
			foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
			{
				OxidizerTank component = @ref.Get().GetComponent<OxidizerTank>();
				if (component != null)
				{
					num += component.TotalOxidizerPower;
				}
			}
			return (float)Mathf.CeilToInt(num);
		}
	}

	// Token: 0x17000515 RID: 1301
	// (get) Token: 0x060045EB RID: 17899 RVA: 0x00189B9C File Offset: 0x00187D9C
	public int MaxHeight
	{
		get
		{
			RocketEngineCluster engine = this.GetEngine();
			if (engine != null)
			{
				return engine.maxHeight;
			}
			return -1;
		}
	}

	// Token: 0x17000516 RID: 1302
	// (get) Token: 0x060045EC RID: 17900 RVA: 0x00189BC1 File Offset: 0x00187DC1
	public float TotalBurden
	{
		get
		{
			return this.m_clustercraft.TotalBurden;
		}
	}

	// Token: 0x17000517 RID: 1303
	// (get) Token: 0x060045ED RID: 17901 RVA: 0x00189BCE File Offset: 0x00187DCE
	public float EnginePower
	{
		get
		{
			return this.m_clustercraft.EnginePower;
		}
	}

	// Token: 0x17000518 RID: 1304
	// (get) Token: 0x060045EE RID: 17902 RVA: 0x00189BDC File Offset: 0x00187DDC
	public int RocketHeight
	{
		get
		{
			int num = 0;
			foreach (Ref<RocketModuleCluster> @ref in this.ClusterModules)
			{
				num += @ref.Get().GetComponent<Building>().Def.HeightInCells;
			}
			return num;
		}
	}

	// Token: 0x17000519 RID: 1305
	// (get) Token: 0x060045EF RID: 17903 RVA: 0x00189C40 File Offset: 0x00187E40
	public bool HasCargoModule
	{
		get
		{
			using (IEnumerator<Ref<RocketModuleCluster>> enumerator = this.ClusterModules.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Get().GetComponent<CargoBayCluster>() != null)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	// Token: 0x060045F0 RID: 17904 RVA: 0x00189C98 File Offset: 0x00187E98
	protected override void OnPrefabInit()
	{
		Game instance = Game.Instance;
		instance.OnLoad = (Action<Game.GameSaveData>)Delegate.Combine(instance.OnLoad, new Action<Game.GameSaveData>(this.OnLoad));
	}

	// Token: 0x060045F1 RID: 17905 RVA: 0x00189CC0 File Offset: 0x00187EC0
	protected override void OnSpawn()
	{
		Game instance = Game.Instance;
		instance.OnLoad = (Action<Game.GameSaveData>)Delegate.Remove(instance.OnLoad, new Action<Game.GameSaveData>(this.OnLoad));
		if (this.m_clustercraft.Status != Clustercraft.CraftStatus.Grounded)
		{
			this.ForceAttachmentNetwork();
		}
		this.SetBottomModule();
		base.Subscribe(-1311384361, new Action<object>(this.CompleteSelfDestruct));
	}

	// Token: 0x060045F2 RID: 17906 RVA: 0x00189D24 File Offset: 0x00187F24
	private void OnLoad(Game.GameSaveData data)
	{
		foreach (Ref<RocketModule> @ref in this.modules)
		{
			this.clusterModules.Add(new Ref<RocketModuleCluster>(@ref.Get().GetComponent<RocketModuleCluster>()));
		}
		this.modules.Clear();
		foreach (Ref<RocketModuleCluster> ref2 in this.clusterModules)
		{
			if (!(ref2.Get() == null))
			{
				ref2.Get().CraftInterface = this;
			}
		}
		bool flag = false;
		for (int i = this.clusterModules.Count - 1; i >= 0; i--)
		{
			if (this.clusterModules[i] == null || this.clusterModules[i].Get() == null)
			{
				global::Debug.LogWarning(string.Format("Rocket {0} had a null module at index {1} on load! Why????", base.name, i), this);
				this.clusterModules.RemoveAt(i);
				flag = true;
			}
		}
		this.SetBottomModule();
		if (flag && this.m_clustercraft.Status == Clustercraft.CraftStatus.Grounded)
		{
			global::Debug.LogWarning("The module stack was broken. Collapsing " + base.name + "...", this);
			this.SortModuleListByPosition();
			LaunchPad currentPad = this.CurrentPad;
			if (currentPad != null)
			{
				int num = currentPad.RocketBottomPosition;
				for (int j = 0; j < this.clusterModules.Count; j++)
				{
					RocketModuleCluster rocketModuleCluster = this.clusterModules[j].Get();
					if (num != Grid.PosToCell(rocketModuleCluster.transform.GetPosition()))
					{
						global::Debug.LogWarning(string.Format("Collapsing space under module {0}:{1}", j, rocketModuleCluster.name));
						rocketModuleCluster.transform.SetPosition(Grid.CellToPos(num, CellAlignment.Bottom, Grid.SceneLayer.Building));
					}
					num = Grid.OffsetCell(num, 0, this.clusterModules[j].Get().GetComponent<Building>().Def.HeightInCells);
				}
			}
			for (int k = 0; k < this.clusterModules.Count - 1; k++)
			{
				BuildingAttachPoint component = this.clusterModules[k].Get().GetComponent<BuildingAttachPoint>();
				if (component != null)
				{
					AttachableBuilding component2 = this.clusterModules[k + 1].Get().GetComponent<AttachableBuilding>();
					if (component.points[0].attachedBuilding != component2)
					{
						global::Debug.LogWarning("Reattaching " + component.name + " & " + component2.name);
						component.points[0].attachedBuilding = component2;
					}
				}
			}
		}
	}

	// Token: 0x060045F3 RID: 17907 RVA: 0x0018A018 File Offset: 0x00188218
	public void AddModule(RocketModuleCluster newModule)
	{
		for (int i = 0; i < this.clusterModules.Count; i++)
		{
			if (this.clusterModules[i].Get() == newModule)
			{
				global::Debug.LogError(string.Concat(new string[]
				{
					"Adding module ",
					(newModule != null) ? newModule.ToString() : null,
					" to the same rocket (",
					this.m_clustercraft.Name,
					") twice"
				}));
			}
		}
		this.clusterModules.Add(new Ref<RocketModuleCluster>(newModule));
		newModule.CraftInterface = this;
		base.Trigger(1512695988, newModule);
		foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
		{
			RocketModuleCluster rocketModuleCluster = @ref.Get();
			if (rocketModuleCluster != null && rocketModuleCluster != newModule)
			{
				rocketModuleCluster.Trigger(1512695988, newModule);
			}
		}
		newModule.Trigger(1512695988, newModule);
		this.SetBottomModule();
	}

	// Token: 0x060045F4 RID: 17908 RVA: 0x0018A134 File Offset: 0x00188334
	public void RemoveModule(RocketModuleCluster module)
	{
		for (int i = this.clusterModules.Count - 1; i >= 0; i--)
		{
			if (this.clusterModules[i].Get() == module)
			{
				this.clusterModules.RemoveAt(i);
				break;
			}
		}
		base.Trigger(1512695988, null);
		foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
		{
			@ref.Get().Trigger(1512695988, null);
		}
		this.SetBottomModule();
		if (this.clusterModules.Count == 0)
		{
			base.gameObject.DeleteObject();
		}
	}

	// Token: 0x060045F5 RID: 17909 RVA: 0x0018A1F8 File Offset: 0x001883F8
	private void SortModuleListByPosition()
	{
		this.clusterModules.Sort(delegate(Ref<RocketModuleCluster> a, Ref<RocketModuleCluster> b)
		{
			if (Grid.CellToPos(Grid.PosToCell(a.Get())).y >= Grid.CellToPos(Grid.PosToCell(b.Get())).y)
			{
				return 1;
			}
			return -1;
		});
	}

	// Token: 0x060045F6 RID: 17910 RVA: 0x0018A224 File Offset: 0x00188424
	private void SetBottomModule()
	{
		if (this.clusterModules.Count > 0)
		{
			this.bottomModule = this.clusterModules[0];
			Vector3 vector = this.bottomModule.Get().transform.position;
			using (List<Ref<RocketModuleCluster>>.Enumerator enumerator = this.clusterModules.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Ref<RocketModuleCluster> @ref = enumerator.Current;
					Vector3 position = @ref.Get().transform.position;
					if (position.y < vector.y)
					{
						this.bottomModule = @ref;
						vector = position;
					}
				}
				return;
			}
		}
		this.bottomModule = null;
	}

	// Token: 0x060045F7 RID: 17911 RVA: 0x0018A2D8 File Offset: 0x001884D8
	public int GetHeightOfModuleTop(GameObject module)
	{
		int num = 0;
		for (int i = 0; i < this.ClusterModules.Count; i++)
		{
			num += this.clusterModules[i].Get().GetComponent<Building>().Def.HeightInCells;
			if (this.clusterModules[i].Get().gameObject == module)
			{
				return num;
			}
		}
		global::Debug.LogError("Could not find module " + module.GetProperName() + " in CraftModuleInterface craft " + this.m_clustercraft.Name);
		return 0;
	}

	// Token: 0x060045F8 RID: 17912 RVA: 0x0018A368 File Offset: 0x00188568
	public int GetModuleRelativeVerticalPosition(GameObject module)
	{
		int num = 0;
		for (int i = 0; i < this.ClusterModules.Count; i++)
		{
			if (this.clusterModules[i].Get().gameObject == module)
			{
				return num;
			}
			num += this.clusterModules[i].Get().GetComponent<Building>().Def.HeightInCells;
		}
		global::Debug.LogError("Could not find module " + module.GetProperName() + " in CraftModuleInterface craft " + this.m_clustercraft.Name);
		return 0;
	}

	// Token: 0x060045F9 RID: 17913 RVA: 0x0018A3F8 File Offset: 0x001885F8
	public void Sim4000ms(float dt)
	{
		int num = 0;
		foreach (ProcessCondition.ProcessConditionType processConditionType in this.conditionsToCheck)
		{
			if (this.EvaluateConditionSet(processConditionType) != ProcessCondition.Status.Failure)
			{
				num++;
			}
		}
		if (num != this.lastConditionTypeSucceeded)
		{
			this.lastConditionTypeSucceeded = num;
			this.TriggerEventOnCraftAndRocket(GameHashes.LaunchConditionChanged, null);
		}
	}

	// Token: 0x060045FA RID: 17914 RVA: 0x0018A470 File Offset: 0x00188670
	public bool IsLaunchRequested()
	{
		return this.m_clustercraft.LaunchRequested;
	}

	// Token: 0x060045FB RID: 17915 RVA: 0x0018A47D File Offset: 0x0018867D
	public bool CheckPreppedForLaunch()
	{
		return this.EvaluateConditionSet(ProcessCondition.ProcessConditionType.RocketPrep) != ProcessCondition.Status.Failure && this.EvaluateConditionSet(ProcessCondition.ProcessConditionType.RocketStorage) != ProcessCondition.Status.Failure && this.EvaluateConditionSet(ProcessCondition.ProcessConditionType.RocketFlight) > ProcessCondition.Status.Failure;
	}

	// Token: 0x060045FC RID: 17916 RVA: 0x0018A49D File Offset: 0x0018869D
	public bool CheckReadyToLaunch()
	{
		return this.EvaluateConditionSet(ProcessCondition.ProcessConditionType.RocketPrep) != ProcessCondition.Status.Failure && this.EvaluateConditionSet(ProcessCondition.ProcessConditionType.RocketStorage) != ProcessCondition.Status.Failure && this.EvaluateConditionSet(ProcessCondition.ProcessConditionType.RocketFlight) != ProcessCondition.Status.Failure && this.EvaluateConditionSet(ProcessCondition.ProcessConditionType.RocketBoard) > ProcessCondition.Status.Failure;
	}

	// Token: 0x060045FD RID: 17917 RVA: 0x0018A4C6 File Offset: 0x001886C6
	public bool HasLaunchWarnings()
	{
		return this.EvaluateConditionSet(ProcessCondition.ProcessConditionType.RocketPrep) == ProcessCondition.Status.Warning || this.EvaluateConditionSet(ProcessCondition.ProcessConditionType.RocketStorage) == ProcessCondition.Status.Warning || this.EvaluateConditionSet(ProcessCondition.ProcessConditionType.RocketBoard) == ProcessCondition.Status.Warning;
	}

	// Token: 0x060045FE RID: 17918 RVA: 0x0018A4E8 File Offset: 0x001886E8
	public bool CheckReadyForAutomatedLaunchCommand()
	{
		return this.EvaluateConditionSet(ProcessCondition.ProcessConditionType.RocketPrep) == ProcessCondition.Status.Ready && this.EvaluateConditionSet(ProcessCondition.ProcessConditionType.RocketStorage) == ProcessCondition.Status.Ready;
	}

	// Token: 0x060045FF RID: 17919 RVA: 0x0018A500 File Offset: 0x00188700
	public bool CheckReadyForAutomatedLaunch()
	{
		return this.EvaluateConditionSet(ProcessCondition.ProcessConditionType.RocketPrep) == ProcessCondition.Status.Ready && this.EvaluateConditionSet(ProcessCondition.ProcessConditionType.RocketStorage) == ProcessCondition.Status.Ready && this.EvaluateConditionSet(ProcessCondition.ProcessConditionType.RocketBoard) == ProcessCondition.Status.Ready;
	}

	// Token: 0x06004600 RID: 17920 RVA: 0x0018A524 File Offset: 0x00188724
	public void TriggerEventOnCraftAndRocket(GameHashes evt, object data)
	{
		base.Trigger((int)evt, data);
		foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
		{
			@ref.Get().Trigger((int)evt, data);
		}
	}

	// Token: 0x06004601 RID: 17921 RVA: 0x0018A584 File Offset: 0x00188784
	public void CancelLaunch()
	{
		this.m_clustercraft.CancelLaunch();
	}

	// Token: 0x06004602 RID: 17922 RVA: 0x0018A591 File Offset: 0x00188791
	public void TriggerLaunch(bool automated = false)
	{
		this.m_clustercraft.RequestLaunch(automated);
	}

	// Token: 0x06004603 RID: 17923 RVA: 0x0018A5A0 File Offset: 0x001887A0
	public void DoLaunch()
	{
		this.SortModuleListByPosition();
		this.CurrentPad.Trigger(705820818, this);
		this.SetPreferredLaunchPadForWorld(this.CurrentPad);
		foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
		{
			@ref.Get().Trigger(705820818, this);
		}
	}

	// Token: 0x06004604 RID: 17924 RVA: 0x0018A620 File Offset: 0x00188820
	public void DoLand(LaunchPad pad)
	{
		int num = pad.RocketBottomPosition;
		for (int i = 0; i < this.clusterModules.Count; i++)
		{
			this.clusterModules[i].Get().MoveToPad(num);
			num = Grid.OffsetCell(num, 0, this.clusterModules[i].Get().GetComponent<Building>().Def.HeightInCells);
		}
		this.SetBottomModule();
		foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
		{
			@ref.Get().Trigger(-1165815793, pad);
		}
		pad.Trigger(-1165815793, this);
	}

	// Token: 0x06004605 RID: 17925 RVA: 0x0018A6EC File Offset: 0x001888EC
	public LaunchConditionManager FindLaunchConditionManager()
	{
		foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
		{
			LaunchConditionManager component = @ref.Get().GetComponent<LaunchConditionManager>();
			if (component != null)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x06004606 RID: 17926 RVA: 0x0018A754 File Offset: 0x00188954
	public LaunchableRocketCluster FindLaunchableRocket()
	{
		foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
		{
			RocketModuleCluster rocketModuleCluster = @ref.Get();
			LaunchableRocketCluster component = rocketModuleCluster.GetComponent<LaunchableRocketCluster>();
			if (component != null && rocketModuleCluster.CraftInterface != null && rocketModuleCluster.CraftInterface.GetComponent<Clustercraft>().Status == Clustercraft.CraftStatus.Grounded)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x06004607 RID: 17927 RVA: 0x0018A7DC File Offset: 0x001889DC
	public List<GameObject> GetParts()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
		{
			list.Add(@ref.Get().gameObject);
		}
		return list;
	}

	// Token: 0x06004608 RID: 17928 RVA: 0x0018A840 File Offset: 0x00188A40
	public RocketEngineCluster GetEngine()
	{
		foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
		{
			RocketEngineCluster component = @ref.Get().GetComponent<RocketEngineCluster>();
			if (component != null)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x06004609 RID: 17929 RVA: 0x0018A8A8 File Offset: 0x00188AA8
	public PassengerRocketModule GetPassengerModule()
	{
		foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
		{
			PassengerRocketModule component = @ref.Get().GetComponent<PassengerRocketModule>();
			if (component != null)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x0600460A RID: 17930 RVA: 0x0018A910 File Offset: 0x00188B10
	public WorldContainer GetInteriorWorld()
	{
		PassengerRocketModule passengerModule = this.GetPassengerModule();
		if (passengerModule == null)
		{
			return null;
		}
		ClustercraftInteriorDoor interiorDoor = passengerModule.GetComponent<ClustercraftExteriorDoor>().GetInteriorDoor();
		if (interiorDoor == null)
		{
			return null;
		}
		return interiorDoor.GetMyWorld();
	}

	// Token: 0x0600460B RID: 17931 RVA: 0x0018A94C File Offset: 0x00188B4C
	public RocketClusterDestinationSelector GetClusterDestinationSelector()
	{
		return base.GetComponent<RocketClusterDestinationSelector>();
	}

	// Token: 0x0600460C RID: 17932 RVA: 0x0018A954 File Offset: 0x00188B54
	public bool HasClusterDestinationSelector()
	{
		return base.GetComponent<RocketClusterDestinationSelector>() != null;
	}

	// Token: 0x0600460D RID: 17933 RVA: 0x0018A964 File Offset: 0x00188B64
	public List<ProcessCondition> GetConditionSet(ProcessCondition.ProcessConditionType conditionType)
	{
		this.returnConditions.Clear();
		foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
		{
			List<ProcessCondition> conditionSet = @ref.Get().GetConditionSet(conditionType);
			if (conditionSet != null)
			{
				this.returnConditions.AddRange(conditionSet);
			}
		}
		if (this.CurrentPad != null)
		{
			List<ProcessCondition> conditionSet2 = this.CurrentPad.GetComponent<LaunchPadConditions>().GetConditionSet(conditionType);
			if (conditionSet2 != null)
			{
				this.returnConditions.AddRange(conditionSet2);
			}
		}
		return this.returnConditions;
	}

	// Token: 0x0600460E RID: 17934 RVA: 0x0018AA0C File Offset: 0x00188C0C
	private ProcessCondition.Status EvaluateConditionSet(ProcessCondition.ProcessConditionType conditionType)
	{
		ProcessCondition.Status status = ProcessCondition.Status.Ready;
		foreach (ProcessCondition processCondition in this.GetConditionSet(conditionType))
		{
			ProcessCondition.Status status2 = processCondition.EvaluateCondition();
			if (status2 < status)
			{
				status = status2;
			}
			if (status == ProcessCondition.Status.Failure)
			{
				break;
			}
		}
		return status;
	}

	// Token: 0x0600460F RID: 17935 RVA: 0x0018AA6C File Offset: 0x00188C6C
	private void ForceAttachmentNetwork()
	{
		RocketModuleCluster rocketModuleCluster = null;
		foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
		{
			RocketModuleCluster rocketModuleCluster2 = @ref.Get();
			if (rocketModuleCluster != null)
			{
				BuildingAttachPoint component = rocketModuleCluster.GetComponent<BuildingAttachPoint>();
				AttachableBuilding component2 = rocketModuleCluster2.GetComponent<AttachableBuilding>();
				component.points[0].attachedBuilding = component2;
			}
			rocketModuleCluster = rocketModuleCluster2;
		}
	}

	// Token: 0x06004610 RID: 17936 RVA: 0x0018AAE8 File Offset: 0x00188CE8
	public static Storage SpawnRocketDebris(string nameSuffix, SimHashes element)
	{
		Vector3 vector = new Vector3(-1f, -1f, 0f);
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("DebrisPayload"), vector);
		gameObject.GetComponent<PrimaryElement>().SetElement(element, true);
		gameObject.name += nameSuffix;
		gameObject.SetActive(true);
		return gameObject.GetComponent<Storage>();
	}

	// Token: 0x06004611 RID: 17937 RVA: 0x0018AB4C File Offset: 0x00188D4C
	public void CompleteSelfDestruct(object data = null)
	{
		global::Debug.Assert(this.HasTag(GameTags.RocketInSpace), "Self Destruct is only valid for in-space rockets!");
		SimHashes elementID = this.GetPassengerModule().GetComponent<PrimaryElement>().ElementID;
		List<RocketModule> list = new List<RocketModule>();
		foreach (Ref<RocketModuleCluster> @ref in this.clusterModules)
		{
			list.Add(@ref.Get());
		}
		List<GameObject> list2 = new List<GameObject>();
		foreach (RocketModule rocketModule in list)
		{
			Storage[] components = rocketModule.GetComponents<Storage>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].DropAll(false, false, default(Vector3), true, list2);
			}
			Deconstructable component = rocketModule.GetComponent<Deconstructable>();
			list2.AddRange(component.ForceDestroyAndGetMaterials());
		}
		List<Storage> list3 = new List<Storage>();
		foreach (GameObject gameObject in list2)
		{
			Pickupable component2 = gameObject.GetComponent<Pickupable>();
			if (component2 != null)
			{
				component2.PrimaryElement.Units = (float)Mathf.Max(1, Mathf.RoundToInt(component2.PrimaryElement.Units * 0.5f));
				if ((list3.Count == 0 || list3[list3.Count - 1].RemainingCapacity() == 0f) && component2.PrimaryElement.Mass > 0f)
				{
					list3.Add(CraftModuleInterface.SpawnRocketDebris(" from CMI", elementID));
				}
				Storage storage = list3[list3.Count - 1];
				while (component2.PrimaryElement.Mass > storage.RemainingCapacity())
				{
					Pickupable pickupable = component2.Take(storage.RemainingCapacity());
					storage.Store(pickupable.gameObject, false, false, true, false);
					storage = CraftModuleInterface.SpawnRocketDebris(" from CMI", elementID);
					list3.Add(storage);
				}
				if (component2.PrimaryElement.Mass > 0f)
				{
					storage.Store(component2.gameObject, false, false, true, false);
				}
			}
		}
		foreach (Storage storage2 in list3)
		{
			RailGunPayload.StatesInstance smi = storage2.GetSMI<RailGunPayload.StatesInstance>();
			smi.StartSM();
			smi.Travel(this.m_clustercraft.Location, ClusterUtil.ClosestVisibleAsteroidToLocation(this.m_clustercraft.Location).Location);
		}
		this.m_clustercraft.SetExploding();
	}

	// Token: 0x04002E64 RID: 11876
	[Serialize]
	private List<Ref<RocketModule>> modules = new List<Ref<RocketModule>>();

	// Token: 0x04002E65 RID: 11877
	[Serialize]
	private List<Ref<RocketModuleCluster>> clusterModules = new List<Ref<RocketModuleCluster>>();

	// Token: 0x04002E66 RID: 11878
	private Ref<RocketModuleCluster> bottomModule;

	// Token: 0x04002E67 RID: 11879
	[Serialize]
	private Dictionary<int, Ref<LaunchPad>> preferredLaunchPad = new Dictionary<int, Ref<LaunchPad>>();

	// Token: 0x04002E68 RID: 11880
	[MyCmpReq]
	private Clustercraft m_clustercraft;

	// Token: 0x04002E69 RID: 11881
	private List<ProcessCondition.ProcessConditionType> conditionsToCheck = new List<ProcessCondition.ProcessConditionType>
	{
		ProcessCondition.ProcessConditionType.RocketPrep,
		ProcessCondition.ProcessConditionType.RocketStorage,
		ProcessCondition.ProcessConditionType.RocketBoard,
		ProcessCondition.ProcessConditionType.RocketFlight
	};

	// Token: 0x04002E6A RID: 11882
	private int lastConditionTypeSucceeded = -1;

	// Token: 0x04002E6B RID: 11883
	private List<ProcessCondition> returnConditions = new List<ProcessCondition>();
}
