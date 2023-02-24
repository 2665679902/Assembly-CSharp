using System;
using System.Collections.Generic;
using FMOD.Studio;
using KSerialization;
using ProcGenGame;
using TUNING;
using UnityEngine;

// Token: 0x0200068F RID: 1679
public class ClusterManager : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x06002D69 RID: 11625 RVA: 0x000EE544 File Offset: 0x000EC744
	public static void DestroyInstance()
	{
		ClusterManager.Instance = null;
	}

	// Token: 0x1700032C RID: 812
	// (get) Token: 0x06002D6A RID: 11626 RVA: 0x000EE54C File Offset: 0x000EC74C
	public int worldCount
	{
		get
		{
			return this.m_worldContainers.Count;
		}
	}

	// Token: 0x1700032D RID: 813
	// (get) Token: 0x06002D6B RID: 11627 RVA: 0x000EE559 File Offset: 0x000EC759
	public int activeWorldId
	{
		get
		{
			return this.activeWorldIdx;
		}
	}

	// Token: 0x1700032E RID: 814
	// (get) Token: 0x06002D6C RID: 11628 RVA: 0x000EE561 File Offset: 0x000EC761
	public IList<WorldContainer> WorldContainers
	{
		get
		{
			return this.m_worldContainers.AsReadOnly();
		}
	}

	// Token: 0x06002D6D RID: 11629 RVA: 0x000EE56E File Offset: 0x000EC76E
	public ClusterPOIManager GetClusterPOIManager()
	{
		return this.m_clusterPOIsManager;
	}

	// Token: 0x1700032F RID: 815
	// (get) Token: 0x06002D6E RID: 11630 RVA: 0x000EE578 File Offset: 0x000EC778
	public Dictionary<int, List<IAssignableIdentity>> MinionsByWorld
	{
		get
		{
			this.minionsByWorld.Clear();
			for (int i = 0; i < Components.MinionAssignablesProxy.Count; i++)
			{
				if (!Components.MinionAssignablesProxy[i].GetTargetGameObject().HasTag(GameTags.Dead))
				{
					int id = Components.MinionAssignablesProxy[i].GetTargetGameObject().GetComponent<KMonoBehaviour>().GetMyWorld()
						.id;
					if (!this.minionsByWorld.ContainsKey(id))
					{
						this.minionsByWorld.Add(id, new List<IAssignableIdentity>());
					}
					this.minionsByWorld[id].Add(Components.MinionAssignablesProxy[i]);
				}
			}
			return this.minionsByWorld;
		}
	}

	// Token: 0x06002D6F RID: 11631 RVA: 0x000EE625 File Offset: 0x000EC825
	public void RegisterWorldContainer(WorldContainer worldContainer)
	{
		this.m_worldContainers.Add(worldContainer);
	}

	// Token: 0x06002D70 RID: 11632 RVA: 0x000EE633 File Offset: 0x000EC833
	public void UnregisterWorldContainer(WorldContainer worldContainer)
	{
		base.Trigger(-1078710002, worldContainer.id);
		this.m_worldContainers.Remove(worldContainer);
	}

	// Token: 0x06002D71 RID: 11633 RVA: 0x000EE658 File Offset: 0x000EC858
	public List<int> GetWorldIDsSorted()
	{
		this.m_worldContainers.Sort((WorldContainer a, WorldContainer b) => a.DiscoveryTimestamp.CompareTo(b.DiscoveryTimestamp));
		this._worldIDs.Clear();
		foreach (WorldContainer worldContainer in this.m_worldContainers)
		{
			this._worldIDs.Add(worldContainer.id);
		}
		return this._worldIDs;
	}

	// Token: 0x06002D72 RID: 11634 RVA: 0x000EE6F0 File Offset: 0x000EC8F0
	public List<int> GetDiscoveredAsteroidIDsSorted()
	{
		this.m_worldContainers.Sort((WorldContainer a, WorldContainer b) => a.DiscoveryTimestamp.CompareTo(b.DiscoveryTimestamp));
		List<int> list = new List<int>();
		for (int i = 0; i < this.m_worldContainers.Count; i++)
		{
			if (this.m_worldContainers[i].IsDiscovered && !this.m_worldContainers[i].IsModuleInterior)
			{
				list.Add(this.m_worldContainers[i].id);
			}
		}
		return list;
	}

	// Token: 0x06002D73 RID: 11635 RVA: 0x000EE784 File Offset: 0x000EC984
	public WorldContainer GetStartWorld()
	{
		foreach (WorldContainer worldContainer in this.WorldContainers)
		{
			if (worldContainer.IsStartWorld)
			{
				return worldContainer;
			}
		}
		return this.WorldContainers[0];
	}

	// Token: 0x06002D74 RID: 11636 RVA: 0x000EE7E4 File Offset: 0x000EC9E4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ClusterManager.Instance = this;
		SaveLoader instance = SaveLoader.Instance;
		instance.OnWorldGenComplete = (Action<Cluster>)Delegate.Combine(instance.OnWorldGenComplete, new Action<Cluster>(this.OnWorldGenComplete));
	}

	// Token: 0x06002D75 RID: 11637 RVA: 0x000EE818 File Offset: 0x000ECA18
	protected override void OnSpawn()
	{
		if (this.m_grid == null)
		{
			this.m_grid = new ClusterGrid(this.m_numRings);
		}
		this.UpdateWorldReverbSnapshot(this.activeWorldId);
		base.OnSpawn();
	}

	// Token: 0x06002D76 RID: 11638 RVA: 0x000EE845 File Offset: 0x000ECA45
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x17000330 RID: 816
	// (get) Token: 0x06002D77 RID: 11639 RVA: 0x000EE84D File Offset: 0x000ECA4D
	public WorldContainer activeWorld
	{
		get
		{
			return this.GetWorld(this.activeWorldId);
		}
	}

	// Token: 0x06002D78 RID: 11640 RVA: 0x000EE85C File Offset: 0x000ECA5C
	private void OnWorldGenComplete(Cluster clusterLayout)
	{
		this.m_numRings = clusterLayout.numRings;
		this.m_grid = new ClusterGrid(this.m_numRings);
		AxialI axialI = AxialI.ZERO;
		foreach (WorldGen worldGen in clusterLayout.worlds)
		{
			int id = this.CreateAsteroidWorldContainer(worldGen).id;
			Vector2I position = worldGen.GetPosition();
			Vector2I vector2I = position + worldGen.GetSize();
			if (worldGen.isStartingWorld)
			{
				axialI = worldGen.GetClusterLocation();
			}
			for (int i = position.y; i < vector2I.y; i++)
			{
				for (int j = position.x; j < vector2I.x; j++)
				{
					int num = Grid.XYToCell(j, i);
					Grid.WorldIdx[num] = (byte)id;
					Pathfinding.Instance.AddDirtyNavGridCell(num);
				}
			}
			if (worldGen.isStartingWorld)
			{
				this.activeWorldIdx = id;
			}
		}
		this.GetSMI<ClusterFogOfWarManager.Instance>().RevealLocation(axialI, 1);
		this.m_clusterPOIsManager.PopulatePOIsFromWorldGen(clusterLayout);
	}

	// Token: 0x06002D79 RID: 11641 RVA: 0x000EE988 File Offset: 0x000ECB88
	private int GetNextWorldId()
	{
		HashSetPool<int, ClusterManager>.PooledHashSet pooledHashSet = HashSetPool<int, ClusterManager>.Allocate();
		foreach (WorldContainer worldContainer in this.m_worldContainers)
		{
			pooledHashSet.Add(worldContainer.id);
		}
		global::Debug.Assert(this.m_worldContainers.Count < 255, "Oh no! We're trying to generate our 255th world in this save, things are going to start going badly...");
		for (int i = 0; i < 255; i++)
		{
			if (!pooledHashSet.Contains(i))
			{
				pooledHashSet.Recycle();
				return i;
			}
		}
		pooledHashSet.Recycle();
		return (int)ClusterManager.INVALID_WORLD_IDX;
	}

	// Token: 0x06002D7A RID: 11642 RVA: 0x000EEA30 File Offset: 0x000ECC30
	private WorldContainer CreateAsteroidWorldContainer(WorldGen world)
	{
		int nextWorldId = this.GetNextWorldId();
		GameObject gameObject = global::Util.KInstantiate(Assets.GetPrefab("Asteroid"), null, null);
		WorldContainer component = gameObject.GetComponent<WorldContainer>();
		component.SetID(nextWorldId);
		component.SetWorldDetails(world);
		AsteroidGridEntity component2 = gameObject.GetComponent<AsteroidGridEntity>();
		if (world != null)
		{
			AxialI clusterLocation = world.GetClusterLocation();
			component2.Init(component.GetRandomName(), clusterLocation, world.Settings.world.asteroidIcon);
		}
		else
		{
			component2.Init("", AxialI.ZERO, "");
		}
		if (component.IsStartWorld)
		{
			OrbitalMechanics component3 = gameObject.GetComponent<OrbitalMechanics>();
			if (component3 != null)
			{
				component3.CreateOrbitalObject(Db.Get().OrbitalTypeCategories.backgroundEarth.Id);
			}
		}
		gameObject.SetActive(true);
		return component;
	}

	// Token: 0x06002D7B RID: 11643 RVA: 0x000EEAF4 File Offset: 0x000ECCF4
	private void CreateDefaultAsteroidWorldContainer()
	{
		if (this.m_worldContainers.Count == 0)
		{
			global::Debug.LogWarning("Cluster manager has no world containers, create a default using Grid settings.");
			WorldContainer worldContainer = this.CreateAsteroidWorldContainer(null);
			int id = worldContainer.id;
			int num = (int)worldContainer.minimumBounds.y;
			while ((float)num <= worldContainer.maximumBounds.y)
			{
				int num2 = (int)worldContainer.minimumBounds.x;
				while ((float)num2 <= worldContainer.maximumBounds.x)
				{
					int num3 = Grid.XYToCell(num2, num);
					Grid.WorldIdx[num3] = (byte)id;
					Pathfinding.Instance.AddDirtyNavGridCell(num3);
					num2++;
				}
				num++;
			}
		}
	}

	// Token: 0x06002D7C RID: 11644 RVA: 0x000EEB8C File Offset: 0x000ECD8C
	public void InitializeWorldGrid()
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 20))
		{
			this.CreateDefaultAsteroidWorldContainer();
		}
		bool flag = false;
		foreach (WorldContainer worldContainer in this.m_worldContainers)
		{
			Vector2I worldOffset = worldContainer.WorldOffset;
			Vector2I vector2I = worldOffset + worldContainer.WorldSize;
			for (int i = worldOffset.y; i < vector2I.y; i++)
			{
				for (int j = worldOffset.x; j < vector2I.x; j++)
				{
					int num = Grid.XYToCell(j, i);
					Grid.WorldIdx[num] = (byte)worldContainer.id;
					Pathfinding.Instance.AddDirtyNavGridCell(num);
				}
			}
			flag |= worldContainer.IsDiscovered;
		}
		if (!flag)
		{
			global::Debug.LogWarning("No worlds have been discovered. Setting the active world to discovered");
			this.activeWorld.SetDiscovered(false);
		}
	}

	// Token: 0x06002D7D RID: 11645 RVA: 0x000EEC94 File Offset: 0x000ECE94
	public void SetActiveWorld(int worldIdx)
	{
		int num = this.activeWorldIdx;
		if (num != worldIdx)
		{
			this.activeWorldIdx = worldIdx;
			Game.Instance.Trigger(1983128072, new global::Tuple<int, int>(this.activeWorldIdx, num));
		}
	}

	// Token: 0x06002D7E RID: 11646 RVA: 0x000EECCE File Offset: 0x000ECECE
	public void TimelapseModeOverrideActiveWorld(int overrideValue)
	{
		this.activeWorldIdx = overrideValue;
	}

	// Token: 0x06002D7F RID: 11647 RVA: 0x000EECD8 File Offset: 0x000ECED8
	public WorldContainer GetWorld(int id)
	{
		for (int i = 0; i < this.m_worldContainers.Count; i++)
		{
			if (this.m_worldContainers[i].id == id)
			{
				return this.m_worldContainers[i];
			}
		}
		return null;
	}

	// Token: 0x06002D80 RID: 11648 RVA: 0x000EED20 File Offset: 0x000ECF20
	public WorldContainer GetWorldFromPosition(Vector3 position)
	{
		foreach (WorldContainer worldContainer in this.m_worldContainers)
		{
			if (worldContainer.ContainsPoint(position))
			{
				return worldContainer;
			}
		}
		return null;
	}

	// Token: 0x06002D81 RID: 11649 RVA: 0x000EED84 File Offset: 0x000ECF84
	public float CountAllRations()
	{
		float num = 0f;
		foreach (WorldContainer worldContainer in this.m_worldContainers)
		{
			RationTracker.Get().CountRations(null, worldContainer.worldInventory, true);
		}
		return num;
	}

	// Token: 0x06002D82 RID: 11650 RVA: 0x000EEDEC File Offset: 0x000ECFEC
	public Dictionary<Tag, float> GetAllWorldsAccessibleAmounts()
	{
		Dictionary<Tag, float> dictionary = new Dictionary<Tag, float>();
		foreach (WorldContainer worldContainer in this.m_worldContainers)
		{
			foreach (KeyValuePair<Tag, float> keyValuePair in worldContainer.worldInventory.GetAccessibleAmounts())
			{
				if (dictionary.ContainsKey(keyValuePair.Key))
				{
					Dictionary<Tag, float> dictionary2 = dictionary;
					Tag key = keyValuePair.Key;
					dictionary2[key] += keyValuePair.Value;
				}
				else
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}
		return dictionary;
	}

	// Token: 0x06002D83 RID: 11651 RVA: 0x000EEED0 File Offset: 0x000ED0D0
	public void MigrateMinion(MinionIdentity minion, int targetID)
	{
		this.MigrateMinion(minion, targetID, minion.GetMyWorldId());
	}

	// Token: 0x06002D84 RID: 11652 RVA: 0x000EEEE0 File Offset: 0x000ED0E0
	public void MigrateMinion(MinionIdentity minion, int targetID, int prevID)
	{
		if (!ClusterManager.Instance.GetWorld(targetID).IsDiscovered)
		{
			ClusterManager.Instance.GetWorld(targetID).SetDiscovered(false);
		}
		if (!ClusterManager.Instance.GetWorld(targetID).IsDupeVisited)
		{
			ClusterManager.Instance.GetWorld(targetID).SetDupeVisited();
		}
		this.migrationEvArg.minionId = minion;
		this.migrationEvArg.prevWorldId = prevID;
		this.migrationEvArg.targetWorldId = targetID;
		Game.Instance.assignmentManager.RemoveFromWorld(minion, this.migrationEvArg.prevWorldId);
		Game.Instance.Trigger(586301400, this.migrationEvArg);
	}

	// Token: 0x06002D85 RID: 11653 RVA: 0x000EEF88 File Offset: 0x000ED188
	public int GetLandingBeaconLocation(int worldId)
	{
		foreach (object obj in Components.LandingBeacons)
		{
			LandingBeacon.Instance instance = (LandingBeacon.Instance)obj;
			if (instance.GetMyWorldId() == worldId && instance.CanBeTargeted())
			{
				return Grid.PosToCell(instance);
			}
		}
		return Grid.InvalidCell;
	}

	// Token: 0x06002D86 RID: 11654 RVA: 0x000EEFFC File Offset: 0x000ED1FC
	public int GetRandomClearCell(int worldId)
	{
		bool flag = false;
		int num = 0;
		while (!flag && num < 1000)
		{
			num++;
			int num2 = UnityEngine.Random.Range(0, Grid.CellCount);
			if (!Grid.Solid[num2] && !Grid.IsLiquid(num2) && (int)Grid.WorldIdx[num2] == worldId)
			{
				return num2;
			}
		}
		num = 0;
		while (!flag && num < 1000)
		{
			num++;
			int num3 = UnityEngine.Random.Range(0, Grid.CellCount);
			if (!Grid.Solid[num3] && (int)Grid.WorldIdx[num3] == worldId)
			{
				return num3;
			}
		}
		return Grid.InvalidCell;
	}

	// Token: 0x06002D87 RID: 11655 RVA: 0x000EF088 File Offset: 0x000ED288
	private bool NotObstructedCell(int x, int y)
	{
		int num = Grid.XYToCell(x, y);
		return Grid.IsValidCell(num) && Grid.Objects[num, 1] == null;
	}

	// Token: 0x06002D88 RID: 11656 RVA: 0x000EF0BC File Offset: 0x000ED2BC
	private int LowestYThatSeesSky(int topCellYPos, int x)
	{
		int num = topCellYPos;
		while (!this.ValidSurfaceCell(x, num))
		{
			num--;
		}
		return num;
	}

	// Token: 0x06002D89 RID: 11657 RVA: 0x000EF0DC File Offset: 0x000ED2DC
	private bool ValidSurfaceCell(int x, int y)
	{
		int num = Grid.XYToCell(x, y - 1);
		return Grid.Solid[num] || Grid.Foundation[num];
	}

	// Token: 0x06002D8A RID: 11658 RVA: 0x000EF110 File Offset: 0x000ED310
	public int GetRandomSurfaceCell(int worldID, int width = 1, bool excludeTopBorderHeight = true)
	{
		WorldContainer worldContainer = this.m_worldContainers.Find((WorldContainer match) => match.id == worldID);
		int num = Mathf.RoundToInt(UnityEngine.Random.Range(worldContainer.minimumBounds.x + (float)(worldContainer.Width / 10), worldContainer.maximumBounds.x - (float)(worldContainer.Width / 10)));
		int num2 = Mathf.RoundToInt(worldContainer.maximumBounds.y);
		if (excludeTopBorderHeight)
		{
			num2 -= Grid.TopBorderHeight;
		}
		int num3 = num;
		int num4 = this.LowestYThatSeesSky(num2, num3);
		int num5;
		if (this.NotObstructedCell(num3, num4))
		{
			num5 = 1;
		}
		else
		{
			num5 = 0;
		}
		while (num3 + 1 != num && num5 < width)
		{
			num3++;
			if ((float)num3 > worldContainer.maximumBounds.x)
			{
				num5 = 0;
				num3 = (int)worldContainer.minimumBounds.x;
			}
			int num6 = this.LowestYThatSeesSky(num2, num3);
			bool flag = this.NotObstructedCell(num3, num6);
			if (num6 == num4 && flag)
			{
				num5++;
			}
			else if (flag)
			{
				num5 = 1;
			}
			else
			{
				num5 = 0;
			}
			num4 = num6;
		}
		if (num5 < width)
		{
			return -1;
		}
		return Grid.XYToCell(num3, num4);
	}

	// Token: 0x06002D8B RID: 11659 RVA: 0x000EF23C File Offset: 0x000ED43C
	public bool IsPositionInActiveWorld(Vector3 pos)
	{
		if (this.activeWorld != null && !CameraController.Instance.ignoreClusterFX)
		{
			Vector2 vector = this.activeWorld.maximumBounds * Grid.CellSizeInMeters;
			Vector2 vector2 = this.activeWorld.minimumBounds * Grid.CellSizeInMeters;
			if (pos.x < vector2.x || pos.x > vector.x || pos.y < vector2.y || pos.y > vector.y)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002D8C RID: 11660 RVA: 0x000EF2CC File Offset: 0x000ED4CC
	public WorldContainer CreateRocketInteriorWorld(GameObject craft_go, string interiorTemplateName, System.Action callback)
	{
		Vector2I rocket_INTERIOR_SIZE = ROCKETRY.ROCKET_INTERIOR_SIZE;
		Vector2I vector2I;
		if (Grid.GetFreeGridSpace(rocket_INTERIOR_SIZE, out vector2I))
		{
			int nextWorldId = this.GetNextWorldId();
			craft_go.AddComponent<WorldInventory>();
			WorldContainer worldContainer = craft_go.AddComponent<WorldContainer>();
			worldContainer.SetRocketInteriorWorldDetails(nextWorldId, rocket_INTERIOR_SIZE, vector2I);
			Vector2I vector2I2 = vector2I + rocket_INTERIOR_SIZE;
			for (int i = vector2I.y; i < vector2I2.y; i++)
			{
				for (int j = vector2I.x; j < vector2I2.x; j++)
				{
					int num = Grid.XYToCell(j, i);
					Grid.WorldIdx[num] = (byte)nextWorldId;
					Pathfinding.Instance.AddDirtyNavGridCell(num);
				}
			}
			global::Debug.Log(string.Format("Created new rocket interior id: {0}, at {1} with size {2}", nextWorldId, vector2I, rocket_INTERIOR_SIZE));
			worldContainer.PlaceInteriorTemplate(interiorTemplateName, delegate
			{
				if (callback != null)
				{
					callback();
				}
				craft_go.GetComponent<CraftModuleInterface>().TriggerEventOnCraftAndRocket(GameHashes.RocketInteriorComplete, null);
			});
			craft_go.AddOrGet<OrbitalMechanics>().CreateOrbitalObject(Db.Get().OrbitalTypeCategories.landed.Id);
			base.Trigger(-1280433810, worldContainer.id);
			return worldContainer;
		}
		global::Debug.LogError("Failed to create rocket interior.");
		return null;
	}

	// Token: 0x06002D8D RID: 11661 RVA: 0x000EF408 File Offset: 0x000ED608
	public void DestoryRocketInteriorWorld(int world_id, ClustercraftExteriorDoor door)
	{
		WorldContainer world = this.GetWorld(world_id);
		if (world == null || !world.IsModuleInterior)
		{
			global::Debug.LogError(string.Format("Attempting to destroy world id {0}. The world is not a valid rocket interior", world_id));
			return;
		}
		GameObject gameObject = door.GetComponent<RocketModuleCluster>().CraftInterface.gameObject;
		if (this.activeWorldId == world_id)
		{
			if (gameObject.GetComponent<WorldContainer>().ParentWorldId == world_id)
			{
				this.SetActiveWorld(ClusterManager.Instance.GetStartWorld().id);
			}
			else
			{
				this.SetActiveWorld(gameObject.GetComponent<WorldContainer>().ParentWorldId);
			}
		}
		OrbitalMechanics component = gameObject.GetComponent<OrbitalMechanics>();
		if (!component.IsNullOrDestroyed())
		{
			UnityEngine.Object.Destroy(component);
		}
		bool flag = gameObject.GetComponent<Clustercraft>().Status == Clustercraft.CraftStatus.InFlight;
		PrimaryElement moduleElemet = door.GetComponent<PrimaryElement>();
		AxialI clusterLocation = world.GetComponent<ClusterGridEntity>().Location;
		Vector3 rocketModuleWorldPos = door.transform.position;
		if (!flag)
		{
			world.EjectAllDupes(rocketModuleWorldPos);
		}
		else
		{
			world.SpacePodAllDupes(clusterLocation, moduleElemet.ElementID);
		}
		world.CancelChores();
		HashSet<int> noRefundTiles;
		world.DestroyWorldBuildings(out noRefundTiles);
		this.UnregisterWorldContainer(world);
		if (!flag)
		{
			GameScheduler.Instance.ScheduleNextFrame("ClusterManager.world.TransferResourcesToParentWorld", delegate(object obj)
			{
				world.TransferResourcesToParentWorld(rocketModuleWorldPos + new Vector3(0f, 0.5f, 0f), noRefundTiles);
			}, null, null);
			GameScheduler.Instance.ScheduleNextFrame("ClusterManager.DeleteWorldObjects", delegate(object obj)
			{
				this.DeleteWorldObjects(world);
			}, null, null);
			return;
		}
		GameScheduler.Instance.ScheduleNextFrame("ClusterManager.world.TransferResourcesToDebris", delegate(object obj)
		{
			world.TransferResourcesToDebris(clusterLocation, noRefundTiles, moduleElemet.ElementID);
		}, null, null);
		GameScheduler.Instance.ScheduleNextFrame("ClusterManager.DeleteWorldObjects", delegate(object obj)
		{
			this.DeleteWorldObjects(world);
		}, null, null);
	}

	// Token: 0x06002D8E RID: 11662 RVA: 0x000EF5DC File Offset: 0x000ED7DC
	public void UpdateWorldReverbSnapshot(int worldId)
	{
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().SmallRocketInteriorReverbSnapshot, STOP_MODE.ALLOWFADEOUT);
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().MediumRocketInteriorReverbSnapshot, STOP_MODE.ALLOWFADEOUT);
		WorldContainer world = this.GetWorld(worldId);
		if (world.IsModuleInterior)
		{
			PassengerRocketModule passengerModule = world.GetComponent<Clustercraft>().ModuleInterface.GetPassengerModule();
			AudioMixer.instance.Start(passengerModule.interiorReverbSnapshot);
		}
	}

	// Token: 0x06002D8F RID: 11663 RVA: 0x000EF648 File Offset: 0x000ED848
	private void DeleteWorldObjects(WorldContainer world)
	{
		Grid.FreeGridSpace(world.WorldSize, world.WorldOffset);
		WorldInventory worldInventory = null;
		if (world != null)
		{
			worldInventory = world.GetComponent<WorldInventory>();
		}
		if (worldInventory != null)
		{
			UnityEngine.Object.Destroy(worldInventory);
		}
		if (world != null)
		{
			UnityEngine.Object.Destroy(world);
		}
	}

	// Token: 0x04001AFC RID: 6908
	public static int MAX_ROCKET_INTERIOR_COUNT = 16;

	// Token: 0x04001AFD RID: 6909
	public static ClusterManager Instance;

	// Token: 0x04001AFE RID: 6910
	private ClusterGrid m_grid;

	// Token: 0x04001AFF RID: 6911
	[Serialize]
	private int m_numRings = 9;

	// Token: 0x04001B00 RID: 6912
	[Serialize]
	private int activeWorldIdx;

	// Token: 0x04001B01 RID: 6913
	public static byte INVALID_WORLD_IDX = byte.MaxValue;

	// Token: 0x04001B02 RID: 6914
	public static Color[] worldColors = new Color[]
	{
		Color.HSVToRGB(0.15f, 0.3f, 0.5f),
		Color.HSVToRGB(0.3f, 0.3f, 0.5f),
		Color.HSVToRGB(0.45f, 0.3f, 0.5f),
		Color.HSVToRGB(0.6f, 0.3f, 0.5f),
		Color.HSVToRGB(0.75f, 0.3f, 0.5f),
		Color.HSVToRGB(0.9f, 0.3f, 0.5f)
	};

	// Token: 0x04001B03 RID: 6915
	private List<WorldContainer> m_worldContainers = new List<WorldContainer>();

	// Token: 0x04001B04 RID: 6916
	[MyCmpGet]
	private ClusterPOIManager m_clusterPOIsManager;

	// Token: 0x04001B05 RID: 6917
	private Dictionary<int, List<IAssignableIdentity>> minionsByWorld = new Dictionary<int, List<IAssignableIdentity>>();

	// Token: 0x04001B06 RID: 6918
	private MinionMigrationEventArgs migrationEvArg = new MinionMigrationEventArgs();

	// Token: 0x04001B07 RID: 6919
	private List<int> _worldIDs = new List<int>();
}
