using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020006A3 RID: 1699
public class Components
{
	// Token: 0x04001B74 RID: 7028
	public static Components.Cmps<MinionIdentity> LiveMinionIdentities = new Components.Cmps<MinionIdentity>();

	// Token: 0x04001B75 RID: 7029
	public static Components.Cmps<MinionIdentity> MinionIdentities = new Components.Cmps<MinionIdentity>();

	// Token: 0x04001B76 RID: 7030
	public static Components.Cmps<StoredMinionIdentity> StoredMinionIdentities = new Components.Cmps<StoredMinionIdentity>();

	// Token: 0x04001B77 RID: 7031
	public static Components.Cmps<MinionStorage> MinionStorages = new Components.Cmps<MinionStorage>();

	// Token: 0x04001B78 RID: 7032
	public static Components.Cmps<MinionResume> MinionResumes = new Components.Cmps<MinionResume>();

	// Token: 0x04001B79 RID: 7033
	public static Components.Cmps<Sleepable> Sleepables = new Components.Cmps<Sleepable>();

	// Token: 0x04001B7A RID: 7034
	public static Components.Cmps<IUsable> Toilets = new Components.Cmps<IUsable>();

	// Token: 0x04001B7B RID: 7035
	public static Components.Cmps<Pickupable> Pickupables = new Components.Cmps<Pickupable>();

	// Token: 0x04001B7C RID: 7036
	public static Components.Cmps<Brain> Brains = new Components.Cmps<Brain>();

	// Token: 0x04001B7D RID: 7037
	public static Components.Cmps<BuildingComplete> BuildingCompletes = new Components.Cmps<BuildingComplete>();

	// Token: 0x04001B7E RID: 7038
	public static Components.Cmps<Notifier> Notifiers = new Components.Cmps<Notifier>();

	// Token: 0x04001B7F RID: 7039
	public static Components.Cmps<Fabricator> Fabricators = new Components.Cmps<Fabricator>();

	// Token: 0x04001B80 RID: 7040
	public static Components.Cmps<Refinery> Refineries = new Components.Cmps<Refinery>();

	// Token: 0x04001B81 RID: 7041
	public static Components.CmpsByWorld<PlantablePlot> PlantablePlots = new Components.CmpsByWorld<PlantablePlot>();

	// Token: 0x04001B82 RID: 7042
	public static Components.Cmps<Ladder> Ladders = new Components.Cmps<Ladder>();

	// Token: 0x04001B83 RID: 7043
	public static Components.Cmps<NavTeleporter> NavTeleporters = new Components.Cmps<NavTeleporter>();

	// Token: 0x04001B84 RID: 7044
	public static Components.Cmps<ITravelTubePiece> ITravelTubePieces = new Components.Cmps<ITravelTubePiece>();

	// Token: 0x04001B85 RID: 7045
	public static Components.CmpsByWorld<CreatureFeeder> CreatureFeeders = new Components.CmpsByWorld<CreatureFeeder>();

	// Token: 0x04001B86 RID: 7046
	public static Components.Cmps<Light2D> Light2Ds = new Components.Cmps<Light2D>();

	// Token: 0x04001B87 RID: 7047
	public static Components.Cmps<Radiator> Radiators = new Components.Cmps<Radiator>();

	// Token: 0x04001B88 RID: 7048
	public static Components.Cmps<Edible> Edibles = new Components.Cmps<Edible>();

	// Token: 0x04001B89 RID: 7049
	public static Components.Cmps<Diggable> Diggables = new Components.Cmps<Diggable>();

	// Token: 0x04001B8A RID: 7050
	public static Components.Cmps<IResearchCenter> ResearchCenters = new Components.Cmps<IResearchCenter>();

	// Token: 0x04001B8B RID: 7051
	public static Components.Cmps<Harvestable> Harvestables = new Components.Cmps<Harvestable>();

	// Token: 0x04001B8C RID: 7052
	public static Components.Cmps<HarvestDesignatable> HarvestDesignatables = new Components.Cmps<HarvestDesignatable>();

	// Token: 0x04001B8D RID: 7053
	public static Components.Cmps<Uprootable> Uprootables = new Components.Cmps<Uprootable>();

	// Token: 0x04001B8E RID: 7054
	public static Components.Cmps<Health> Health = new Components.Cmps<Health>();

	// Token: 0x04001B8F RID: 7055
	public static Components.Cmps<Equipment> Equipment = new Components.Cmps<Equipment>();

	// Token: 0x04001B90 RID: 7056
	public static Components.Cmps<FactionAlignment> FactionAlignments = new Components.Cmps<FactionAlignment>();

	// Token: 0x04001B91 RID: 7057
	public static Components.Cmps<Telepad> Telepads = new Components.Cmps<Telepad>();

	// Token: 0x04001B92 RID: 7058
	public static Components.Cmps<Generator> Generators = new Components.Cmps<Generator>();

	// Token: 0x04001B93 RID: 7059
	public static Components.Cmps<EnergyConsumer> EnergyConsumers = new Components.Cmps<EnergyConsumer>();

	// Token: 0x04001B94 RID: 7060
	public static Components.Cmps<Battery> Batteries = new Components.Cmps<Battery>();

	// Token: 0x04001B95 RID: 7061
	public static Components.Cmps<Breakable> Breakables = new Components.Cmps<Breakable>();

	// Token: 0x04001B96 RID: 7062
	public static Components.Cmps<Crop> Crops = new Components.Cmps<Crop>();

	// Token: 0x04001B97 RID: 7063
	public static Components.Cmps<Prioritizable> Prioritizables = new Components.Cmps<Prioritizable>();

	// Token: 0x04001B98 RID: 7064
	public static Components.Cmps<Clinic> Clinics = new Components.Cmps<Clinic>();

	// Token: 0x04001B99 RID: 7065
	public static Components.Cmps<HandSanitizer> HandSanitizers = new Components.Cmps<HandSanitizer>();

	// Token: 0x04001B9A RID: 7066
	public static Components.Cmps<BuildingCellVisualizer> BuildingCellVisualizers = new Components.Cmps<BuildingCellVisualizer>();

	// Token: 0x04001B9B RID: 7067
	public static Components.Cmps<RoleStation> RoleStations = new Components.Cmps<RoleStation>();

	// Token: 0x04001B9C RID: 7068
	public static Components.Cmps<Telescope> Telescopes = new Components.Cmps<Telescope>();

	// Token: 0x04001B9D RID: 7069
	public static Components.Cmps<Capturable> Capturables = new Components.Cmps<Capturable>();

	// Token: 0x04001B9E RID: 7070
	public static Components.Cmps<NotCapturable> NotCapturables = new Components.Cmps<NotCapturable>();

	// Token: 0x04001B9F RID: 7071
	public static Components.Cmps<DiseaseSourceVisualizer> DiseaseSourceVisualizers = new Components.Cmps<DiseaseSourceVisualizer>();

	// Token: 0x04001BA0 RID: 7072
	public static Components.Cmps<DetectorNetwork.Instance> DetectorNetworks = new Components.Cmps<DetectorNetwork.Instance>();

	// Token: 0x04001BA1 RID: 7073
	public static Components.Cmps<Grave> Graves = new Components.Cmps<Grave>();

	// Token: 0x04001BA2 RID: 7074
	public static Components.Cmps<AttachableBuilding> AttachableBuildings = new Components.Cmps<AttachableBuilding>();

	// Token: 0x04001BA3 RID: 7075
	public static Components.Cmps<BuildingAttachPoint> BuildingAttachPoints = new Components.Cmps<BuildingAttachPoint>();

	// Token: 0x04001BA4 RID: 7076
	public static Components.Cmps<MinionAssignablesProxy> MinionAssignablesProxy = new Components.Cmps<MinionAssignablesProxy>();

	// Token: 0x04001BA5 RID: 7077
	public static Components.Cmps<ComplexFabricator> ComplexFabricators = new Components.Cmps<ComplexFabricator>();

	// Token: 0x04001BA6 RID: 7078
	public static Components.Cmps<MonumentPart> MonumentParts = new Components.Cmps<MonumentPart>();

	// Token: 0x04001BA7 RID: 7079
	public static Components.Cmps<PlantableSeed> PlantableSeeds = new Components.Cmps<PlantableSeed>();

	// Token: 0x04001BA8 RID: 7080
	public static Components.Cmps<IBasicBuilding> BasicBuildings = new Components.Cmps<IBasicBuilding>();

	// Token: 0x04001BA9 RID: 7081
	public static Components.Cmps<Painting> Paintings = new Components.Cmps<Painting>();

	// Token: 0x04001BAA RID: 7082
	public static Components.Cmps<BuildingComplete> TemplateBuildings = new Components.Cmps<BuildingComplete>();

	// Token: 0x04001BAB RID: 7083
	public static Components.Cmps<Teleporter> Teleporters = new Components.Cmps<Teleporter>();

	// Token: 0x04001BAC RID: 7084
	public static Components.Cmps<MutantPlant> MutantPlants = new Components.Cmps<MutantPlant>();

	// Token: 0x04001BAD RID: 7085
	public static Components.Cmps<LandingBeacon.Instance> LandingBeacons = new Components.Cmps<LandingBeacon.Instance>();

	// Token: 0x04001BAE RID: 7086
	public static Components.Cmps<HighEnergyParticle> HighEnergyParticles = new Components.Cmps<HighEnergyParticle>();

	// Token: 0x04001BAF RID: 7087
	public static Components.Cmps<HighEnergyParticlePort> HighEnergyParticlePorts = new Components.Cmps<HighEnergyParticlePort>();

	// Token: 0x04001BB0 RID: 7088
	public static Components.Cmps<Clustercraft> Clustercrafts = new Components.Cmps<Clustercraft>();

	// Token: 0x04001BB1 RID: 7089
	public static Components.Cmps<ClustercraftInteriorDoor> ClusterCraftInteriorDoors = new Components.Cmps<ClustercraftInteriorDoor>();

	// Token: 0x04001BB2 RID: 7090
	public static Components.Cmps<PassengerRocketModule> PassengerRocketModules = new Components.Cmps<PassengerRocketModule>();

	// Token: 0x04001BB3 RID: 7091
	public static Components.Cmps<ClusterTraveler> ClusterTravelers = new Components.Cmps<ClusterTraveler>();

	// Token: 0x04001BB4 RID: 7092
	public static Components.Cmps<LaunchPad> LaunchPads = new Components.Cmps<LaunchPad>();

	// Token: 0x04001BB5 RID: 7093
	public static Components.Cmps<WarpReceiver> WarpReceivers = new Components.Cmps<WarpReceiver>();

	// Token: 0x04001BB6 RID: 7094
	public static Components.Cmps<RocketControlStation> RocketControlStations = new Components.Cmps<RocketControlStation>();

	// Token: 0x04001BB7 RID: 7095
	public static Components.Cmps<Reactor> NuclearReactors = new Components.Cmps<Reactor>();

	// Token: 0x04001BB8 RID: 7096
	public static Components.Cmps<BuildingComplete> EntombedBuildings = new Components.Cmps<BuildingComplete>();

	// Token: 0x04001BB9 RID: 7097
	public static Components.Cmps<SpaceArtifact> SpaceArtifacts = new Components.Cmps<SpaceArtifact>();

	// Token: 0x04001BBA RID: 7098
	public static Components.Cmps<ArtifactAnalysisStationWorkable> ArtifactAnalysisStations = new Components.Cmps<ArtifactAnalysisStationWorkable>();

	// Token: 0x04001BBB RID: 7099
	public static Components.Cmps<RocketConduitReceiver> RocketConduitReceivers = new Components.Cmps<RocketConduitReceiver>();

	// Token: 0x04001BBC RID: 7100
	public static Components.Cmps<RocketConduitSender> RocketConduitSenders = new Components.Cmps<RocketConduitSender>();

	// Token: 0x04001BBD RID: 7101
	public static Components.Cmps<LogicBroadcaster> LogicBroadcasters = new Components.Cmps<LogicBroadcaster>();

	// Token: 0x04001BBE RID: 7102
	public static Components.Cmps<Telephone> Telephones = new Components.Cmps<Telephone>();

	// Token: 0x04001BBF RID: 7103
	public static Components.Cmps<MissionControlWorkable> MissionControlWorkables = new Components.Cmps<MissionControlWorkable>();

	// Token: 0x04001BC0 RID: 7104
	public static Components.Cmps<MissionControlClusterWorkable> MissionControlClusterWorkables = new Components.Cmps<MissionControlClusterWorkable>();

	// Token: 0x04001BC1 RID: 7105
	public static Components.CmpsByWorld<Geyser> Geysers = new Components.CmpsByWorld<Geyser>();

	// Token: 0x04001BC2 RID: 7106
	public static Components.CmpsByWorld<GeoTuner.Instance> GeoTuners = new Components.CmpsByWorld<GeoTuner.Instance>();

	// Token: 0x04001BC3 RID: 7107
	public static Components.Cmps<IncubationMonitor.Instance> IncubationMonitors = new Components.Cmps<IncubationMonitor.Instance>();

	// Token: 0x04001BC4 RID: 7108
	public static Components.Cmps<FixedCapturableMonitor.Instance> FixedCapturableMonitors = new Components.Cmps<FixedCapturableMonitor.Instance>();

	// Token: 0x04001BC5 RID: 7109
	public static Components.Cmps<BeeHive.StatesInstance> BeeHives = new Components.Cmps<BeeHive.StatesInstance>();

	// Token: 0x02001370 RID: 4976
	public class Cmps<T> : ICollection, IEnumerable
	{
		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06007DD7 RID: 32215 RVA: 0x002D6DE7 File Offset: 0x002D4FE7
		public List<T> Items
		{
			get
			{
				return this.items.GetDataList();
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06007DD8 RID: 32216 RVA: 0x002D6DF4 File Offset: 0x002D4FF4
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x06007DD9 RID: 32217 RVA: 0x002D6E01 File Offset: 0x002D5001
		public Cmps()
		{
			App.OnPreLoadScene = (System.Action)Delegate.Combine(App.OnPreLoadScene, new System.Action(this.Clear));
			this.items = new KCompactedVector<T>(0);
			this.table = new Dictionary<T, HandleVector<int>.Handle>();
		}

		// Token: 0x17000857 RID: 2135
		public T this[int idx]
		{
			get
			{
				return this.Items[idx];
			}
		}

		// Token: 0x06007DDB RID: 32219 RVA: 0x002D6E4E File Offset: 0x002D504E
		private void Clear()
		{
			this.items.Clear();
			this.table.Clear();
			this.OnAdd = null;
			this.OnRemove = null;
		}

		// Token: 0x06007DDC RID: 32220 RVA: 0x002D6E74 File Offset: 0x002D5074
		public void Add(T cmp)
		{
			HandleVector<int>.Handle handle = this.items.Allocate(cmp);
			this.table[cmp] = handle;
			if (this.OnAdd != null)
			{
				this.OnAdd(cmp);
			}
		}

		// Token: 0x06007DDD RID: 32221 RVA: 0x002D6EB0 File Offset: 0x002D50B0
		public void Remove(T cmp)
		{
			HandleVector<int>.Handle invalidHandle = HandleVector<int>.InvalidHandle;
			if (this.table.TryGetValue(cmp, out invalidHandle))
			{
				this.table.Remove(cmp);
				this.items.Free(invalidHandle);
				if (this.OnRemove != null)
				{
					this.OnRemove(cmp);
				}
			}
		}

		// Token: 0x06007DDE RID: 32222 RVA: 0x002D6F04 File Offset: 0x002D5104
		public void Register(Action<T> on_add, Action<T> on_remove)
		{
			this.OnAdd += on_add;
			this.OnRemove += on_remove;
			foreach (T t in this.Items)
			{
				this.OnAdd(t);
			}
		}

		// Token: 0x06007DDF RID: 32223 RVA: 0x002D6F6C File Offset: 0x002D516C
		public void Unregister(Action<T> on_add, Action<T> on_remove)
		{
			this.OnAdd -= on_add;
			this.OnRemove -= on_remove;
		}

		// Token: 0x06007DE0 RID: 32224 RVA: 0x002D6F7C File Offset: 0x002D517C
		public List<T> GetWorldItems(int worldId, bool checkChildWorlds = false)
		{
			List<T> list = new List<T>();
			foreach (T t in this.Items)
			{
				KMonoBehaviour kmonoBehaviour = t as KMonoBehaviour;
				bool flag = kmonoBehaviour.GetMyWorldId() == worldId;
				if (!flag && checkChildWorlds)
				{
					WorldContainer myWorld = kmonoBehaviour.GetMyWorld();
					if (myWorld != null && myWorld.ParentWorldId == worldId)
					{
						flag = true;
					}
				}
				if (flag)
				{
					list.Add(t);
				}
			}
			return list;
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06007DE1 RID: 32225 RVA: 0x002D701C File Offset: 0x002D521C
		// (remove) Token: 0x06007DE2 RID: 32226 RVA: 0x002D7054 File Offset: 0x002D5254
		public event Action<T> OnAdd;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06007DE3 RID: 32227 RVA: 0x002D708C File Offset: 0x002D528C
		// (remove) Token: 0x06007DE4 RID: 32228 RVA: 0x002D70C4 File Offset: 0x002D52C4
		public event Action<T> OnRemove;

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06007DE5 RID: 32229 RVA: 0x002D70F9 File Offset: 0x002D52F9
		public bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06007DE6 RID: 32230 RVA: 0x002D7100 File Offset: 0x002D5300
		public object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06007DE7 RID: 32231 RVA: 0x002D7107 File Offset: 0x002D5307
		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06007DE8 RID: 32232 RVA: 0x002D710E File Offset: 0x002D530E
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x0400608A RID: 24714
		private Dictionary<T, HandleVector<int>.Handle> table;

		// Token: 0x0400608B RID: 24715
		private KCompactedVector<T> items;
	}

	// Token: 0x02001371 RID: 4977
	public class CmpsByWorld<T>
	{
		// Token: 0x06007DE9 RID: 32233 RVA: 0x002D711B File Offset: 0x002D531B
		public CmpsByWorld()
		{
			App.OnPreLoadScene = (System.Action)Delegate.Combine(App.OnPreLoadScene, new System.Action(this.Clear));
			this.m_CmpsByWorld = new Dictionary<int, Components.Cmps<T>>();
		}

		// Token: 0x06007DEA RID: 32234 RVA: 0x002D714E File Offset: 0x002D534E
		public void Clear()
		{
			this.m_CmpsByWorld.Clear();
		}

		// Token: 0x06007DEB RID: 32235 RVA: 0x002D715C File Offset: 0x002D535C
		public Components.Cmps<T> CreateOrGetCmps(int worldId)
		{
			Components.Cmps<T> cmps;
			if (!this.m_CmpsByWorld.TryGetValue(worldId, out cmps))
			{
				cmps = new Components.Cmps<T>();
				this.m_CmpsByWorld[worldId] = cmps;
			}
			return cmps;
		}

		// Token: 0x06007DEC RID: 32236 RVA: 0x002D718D File Offset: 0x002D538D
		public void Add(int worldId, T cmp)
		{
			DebugUtil.DevAssertArgs(worldId != -1, new object[] { "CmpsByWorld tried to add a component to an invalid world. Did you call this during a state machine's constructor instead of StartSM? ", cmp });
			this.CreateOrGetCmps(worldId).Add(cmp);
		}

		// Token: 0x06007DED RID: 32237 RVA: 0x002D71BF File Offset: 0x002D53BF
		public void Remove(int worldId, T cmp)
		{
			this.CreateOrGetCmps(worldId).Remove(cmp);
		}

		// Token: 0x06007DEE RID: 32238 RVA: 0x002D71CE File Offset: 0x002D53CE
		public void Register(int worldId, Action<T> on_add, Action<T> on_remove)
		{
			this.CreateOrGetCmps(worldId).Register(on_add, on_remove);
		}

		// Token: 0x06007DEF RID: 32239 RVA: 0x002D71DE File Offset: 0x002D53DE
		public void Unregister(int worldId, Action<T> on_add, Action<T> on_remove)
		{
			this.CreateOrGetCmps(worldId).Unregister(on_add, on_remove);
		}

		// Token: 0x06007DF0 RID: 32240 RVA: 0x002D71EE File Offset: 0x002D53EE
		public List<T> GetItems(int worldId)
		{
			return this.CreateOrGetCmps(worldId).Items;
		}

		// Token: 0x06007DF1 RID: 32241 RVA: 0x002D71FC File Offset: 0x002D53FC
		public IEnumerator GetWorldEnumerator(int worldId)
		{
			return this.CreateOrGetCmps(worldId).GetEnumerator();
		}

		// Token: 0x06007DF2 RID: 32242 RVA: 0x002D720C File Offset: 0x002D540C
		public int[] GetWorldsIDs()
		{
			int[] array = new int[this.m_CmpsByWorld.Keys.Count];
			int num = 0;
			foreach (int num2 in this.m_CmpsByWorld.Keys)
			{
				array[num++] = num2;
			}
			return array;
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06007DF3 RID: 32243 RVA: 0x002D7280 File Offset: 0x002D5480
		public int GlobalCount
		{
			get
			{
				int num = 0;
				foreach (KeyValuePair<int, Components.Cmps<T>> keyValuePair in this.m_CmpsByWorld)
				{
					num += this.m_CmpsByWorld.Count;
				}
				return num;
			}
		}

		// Token: 0x0400608E RID: 24718
		private Dictionary<int, Components.Cmps<T>> m_CmpsByWorld;
	}
}
