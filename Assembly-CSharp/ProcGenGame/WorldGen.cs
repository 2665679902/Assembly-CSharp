using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Delaunay.Geo;
using Klei;
using KSerialization;
using LibNoiseDotNet.Graphics.Tools.Noise.Builder;
using ProcGen;
using ProcGen.Map;
using ProcGen.Noise;
using STRINGS;
using UnityEngine;
using VoronoiTree;

namespace ProcGenGame
{
	// Token: 0x02000C45 RID: 3141
	[Serializable]
	public class WorldGen
	{
		// Token: 0x06006383 RID: 25475 RVA: 0x00251771 File Offset: 0x0024F971
		public static string GetSIMSaveFilename(int baseID = -1)
		{
			return System.IO.Path.Combine(global::Util.RootFolder(), (baseID == -1) ? "WorldGenSimSave.dat" : string.Format("{0}{1}{2}", "WorldGenSimSave", baseID, ".dat"));
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06006384 RID: 25476 RVA: 0x002517A2 File Offset: 0x0024F9A2
		public static string WORLDGEN_SAVE_FILENAME
		{
			get
			{
				return System.IO.Path.Combine(global::Util.RootFolder(), "WorldGenDataSave.dat");
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06006385 RID: 25477 RVA: 0x002517B3 File Offset: 0x0024F9B3
		public static Diseases diseaseStats
		{
			get
			{
				if (WorldGen.m_diseasesDb == null)
				{
					WorldGen.m_diseasesDb = new Diseases(null, true);
				}
				return WorldGen.m_diseasesDb;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06006386 RID: 25478 RVA: 0x002517CD File Offset: 0x0024F9CD
		public int BaseLeft
		{
			get
			{
				return this.Settings.GetBaseLocation().left;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06006387 RID: 25479 RVA: 0x002517DF File Offset: 0x0024F9DF
		public int BaseRight
		{
			get
			{
				return this.Settings.GetBaseLocation().right;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06006388 RID: 25480 RVA: 0x002517F1 File Offset: 0x0024F9F1
		public int BaseTop
		{
			get
			{
				return this.Settings.GetBaseLocation().top;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06006389 RID: 25481 RVA: 0x00251803 File Offset: 0x0024FA03
		public int BaseBot
		{
			get
			{
				return this.Settings.GetBaseLocation().bottom;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600638A RID: 25482 RVA: 0x00251815 File Offset: 0x0024FA15
		// (set) Token: 0x0600638B RID: 25483 RVA: 0x0025181D File Offset: 0x0024FA1D
		public Dictionary<string, object> stats { get; private set; }

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x0600638C RID: 25484 RVA: 0x00251826 File Offset: 0x0024FA26
		// (set) Token: 0x0600638D RID: 25485 RVA: 0x0025182E File Offset: 0x0024FA2E
		public Data data { get; private set; }

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600638E RID: 25486 RVA: 0x00251837 File Offset: 0x0024FA37
		public bool HasData
		{
			get
			{
				return this.data != null;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600638F RID: 25487 RVA: 0x00251842 File Offset: 0x0024FA42
		public bool HasNoiseData
		{
			get
			{
				return this.HasData && this.data.world != null;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06006390 RID: 25488 RVA: 0x0025185C File Offset: 0x0024FA5C
		public float[] DensityMap
		{
			get
			{
				return this.data.world.density;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06006391 RID: 25489 RVA: 0x0025186E File Offset: 0x0024FA6E
		public float[] HeatMap
		{
			get
			{
				return this.data.world.heatOffset;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06006392 RID: 25490 RVA: 0x00251880 File Offset: 0x0024FA80
		public float[] OverrideMap
		{
			get
			{
				return this.data.world.overrides;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06006393 RID: 25491 RVA: 0x00251892 File Offset: 0x0024FA92
		public float[] BaseNoiseMap
		{
			get
			{
				return this.data.world.data;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06006394 RID: 25492 RVA: 0x002518A4 File Offset: 0x0024FAA4
		public float[] DefaultTendMap
		{
			get
			{
				return this.data.world.defaultTemp;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06006395 RID: 25493 RVA: 0x002518B6 File Offset: 0x0024FAB6
		public Chunk World
		{
			get
			{
				return this.data.world;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06006396 RID: 25494 RVA: 0x002518C3 File Offset: 0x0024FAC3
		public Vector2I WorldSize
		{
			get
			{
				return this.data.world.size;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06006397 RID: 25495 RVA: 0x002518D5 File Offset: 0x0024FAD5
		public Vector2I WorldOffset
		{
			get
			{
				return this.data.world.offset;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06006398 RID: 25496 RVA: 0x002518E7 File Offset: 0x0024FAE7
		public WorldLayout WorldLayout
		{
			get
			{
				return this.data.worldLayout;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06006399 RID: 25497 RVA: 0x002518F4 File Offset: 0x0024FAF4
		public List<TerrainCell> OverworldCells
		{
			get
			{
				return this.data.overworldCells;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600639A RID: 25498 RVA: 0x00251901 File Offset: 0x0024FB01
		public List<TerrainCell> TerrainCells
		{
			get
			{
				return this.data.terrainCells;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600639B RID: 25499 RVA: 0x0025190E File Offset: 0x0024FB0E
		public List<River> Rivers
		{
			get
			{
				return this.data.rivers;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600639C RID: 25500 RVA: 0x0025191B File Offset: 0x0024FB1B
		public GameSpawnData SpawnData
		{
			get
			{
				return this.data.gameSpawnData;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600639D RID: 25501 RVA: 0x00251928 File Offset: 0x0024FB28
		public int ChunkEdgeSize
		{
			get
			{
				return this.data.chunkEdgeSize;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x0600639E RID: 25502 RVA: 0x00251935 File Offset: 0x0024FB35
		public HashSet<int> ClaimedCells
		{
			get
			{
				return this.claimedCells;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x0600639F RID: 25503 RVA: 0x0025193D File Offset: 0x0024FB3D
		public HashSet<int> HighPriorityClaimedCells
		{
			get
			{
				return this.highPriorityClaims;
			}
		}

		// Token: 0x060063A0 RID: 25504 RVA: 0x00251945 File Offset: 0x0024FB45
		public void ClearClaimedCells()
		{
			this.claimedCells.Clear();
			this.highPriorityClaims.Clear();
		}

		// Token: 0x060063A1 RID: 25505 RVA: 0x0025195D File Offset: 0x0024FB5D
		public void AddHighPriorityCells(HashSet<int> cells)
		{
			this.highPriorityClaims.Union(cells);
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060063A2 RID: 25506 RVA: 0x0025196C File Offset: 0x0024FB6C
		// (set) Token: 0x060063A3 RID: 25507 RVA: 0x00251974 File Offset: 0x0024FB74
		public WorldGenSettings Settings { get; private set; }

		// Token: 0x060063A4 RID: 25508 RVA: 0x00251980 File Offset: 0x0024FB80
		public WorldGen(string worldName, List<string> chosenWorldTraits, List<string> chosenStoryTraits, bool assertMissingTraits)
		{
			WorldGen.LoadSettings(false);
			this.Settings = new WorldGenSettings(worldName, chosenWorldTraits, chosenStoryTraits, assertMissingTraits);
			this.data = new Data();
			this.data.chunkEdgeSize = this.Settings.GetIntSetting("ChunkEdgeSize");
			this.stats = new Dictionary<string, object>();
		}

		// Token: 0x060063A5 RID: 25509 RVA: 0x00251A1C File Offset: 0x0024FC1C
		public WorldGen(string worldName, Data data, Dictionary<string, object> stats, List<string> chosenTraits, List<string> chosenStoryTraits, bool assertMissingTraits)
		{
			WorldGen.LoadSettings(false);
			this.Settings = new WorldGenSettings(worldName, chosenTraits, chosenStoryTraits, assertMissingTraits);
			this.data = data;
			this.stats = stats;
		}

		// Token: 0x060063A6 RID: 25510 RVA: 0x00251A96 File Offset: 0x0024FC96
		public static void SetupDefaultElements()
		{
			WorldGen.voidElement = ElementLoader.FindElementByHash(SimHashes.Void);
			WorldGen.vacuumElement = ElementLoader.FindElementByHash(SimHashes.Vacuum);
			WorldGen.katairiteElement = ElementLoader.FindElementByHash(SimHashes.Katairite);
			WorldGen.unobtaniumElement = ElementLoader.FindElementByHash(SimHashes.Unobtanium);
		}

		// Token: 0x060063A7 RID: 25511 RVA: 0x00251AD4 File Offset: 0x0024FCD4
		public void Reset()
		{
			this.wasLoaded = false;
		}

		// Token: 0x060063A8 RID: 25512 RVA: 0x00251AE0 File Offset: 0x0024FCE0
		public static void LoadSettings(bool in_async_thread = false)
		{
			bool is_playing = Application.isPlaying;
			if (in_async_thread)
			{
				WorldGen.loadSettingsTask = Task.Run(delegate
				{
					WorldGen.LoadSettings_Internal(is_playing);
				});
				return;
			}
			if (WorldGen.loadSettingsTask != null)
			{
				WorldGen.loadSettingsTask.Wait();
				WorldGen.loadSettingsTask = null;
			}
			WorldGen.LoadSettings_Internal(is_playing);
		}

		// Token: 0x060063A9 RID: 25513 RVA: 0x00251B3C File Offset: 0x0024FD3C
		private static void LoadSettings_Internal(bool is_playing)
		{
			ListPool<YamlIO.Error, WorldGen>.PooledList pooledList = ListPool<YamlIO.Error, WorldGen>.Allocate();
			if (SettingsCache.LoadFiles(pooledList))
			{
				TemplateCache.Init();
			}
			CustomGameSettings.Instance != null;
			if (is_playing)
			{
				Global.Instance.modManager.HandleErrors(pooledList);
			}
			else
			{
				foreach (YamlIO.Error error in pooledList)
				{
					YamlIO.LogError(error, false);
				}
			}
			pooledList.Recycle();
		}

		// Token: 0x060063AA RID: 25514 RVA: 0x00251BC4 File Offset: 0x0024FDC4
		public void InitRandom(int worldSeed, int layoutSeed, int terrainSeed, int noiseSeed)
		{
			this.data.globalWorldSeed = worldSeed;
			this.data.globalWorldLayoutSeed = layoutSeed;
			this.data.globalTerrainSeed = terrainSeed;
			this.data.globalNoiseSeed = noiseSeed;
			this.myRandom = new SeededRandom(worldSeed);
		}

		// Token: 0x060063AB RID: 25515 RVA: 0x00251C04 File Offset: 0x0024FE04
		public void Initialise(WorldGen.OfflineCallbackFunction callbackFn, Action<OfflineWorldGen.ErrorInfo> error_cb, int worldSeed = -1, int layoutSeed = -1, int terrainSeed = -1, int noiseSeed = -1, bool debug = false)
		{
			if (this.wasLoaded)
			{
				global::Debug.LogError("Initialise called after load");
				return;
			}
			this.successCallbackFn = callbackFn;
			this.errorCallback = error_cb;
			global::Debug.Assert(this.successCallbackFn != null);
			this.isRunningDebugGen = debug;
			this.running = false;
			int num = UnityEngine.Random.Range(0, int.MaxValue);
			if (worldSeed == -1)
			{
				worldSeed = num;
			}
			if (layoutSeed == -1)
			{
				layoutSeed = num;
			}
			if (terrainSeed == -1)
			{
				terrainSeed = num;
			}
			if (noiseSeed == -1)
			{
				noiseSeed = num;
			}
			this.data.gameSpawnData = new GameSpawnData();
			this.InitRandom(worldSeed, layoutSeed, terrainSeed, noiseSeed);
			this.successCallbackFn(UI.WORLDGEN.COMPLETE.key, 0f, WorldGenProgressStages.Stages.Failure);
			this.stats["GenerateTime"] = 0;
			this.stats["GenerateNoiseTime"] = 0;
			this.stats["GenerateLayoutTime"] = 0;
			this.stats["ConvertVoroToMapTime"] = 0;
			WorldLayout.SetLayerGradient(SettingsCache.layers.LevelLayers);
		}

		// Token: 0x060063AC RID: 25516 RVA: 0x00251D1A File Offset: 0x0024FF1A
		public void DontGenerateNoiseData()
		{
			this.generateNoiseData = false;
		}

		// Token: 0x060063AD RID: 25517 RVA: 0x00251D24 File Offset: 0x0024FF24
		public void GenerateOffline()
		{
			int num = 1;
			int num2 = 0;
			while (num2 < num && !this.GenerateWorldData())
			{
				DebugUtil.DevLogError("Failed worldgen");
				this.successCallbackFn(UI.WORLDGEN.RETRYCOUNT.key, (float)num2, WorldGenProgressStages.Stages.Failure);
				num2++;
			}
		}

		// Token: 0x060063AE RID: 25518 RVA: 0x00251D6A File Offset: 0x0024FF6A
		private void PlaceTemplateSpawners(Vector2I position, TemplateContainer template, ref Dictionary<int, int> claimedCells)
		{
			this.data.gameSpawnData.AddTemplate(template, position, ref claimedCells);
		}

		// Token: 0x060063AF RID: 25519 RVA: 0x00251D80 File Offset: 0x0024FF80
		public bool RenderOffline(bool doSettle, ref Sim.Cell[] cells, ref Sim.DiseaseCell[] dc, int baseId, ref List<WorldTrait> placedStoryTraits, bool isStartingWorld = false)
		{
			float[] array = null;
			dc = null;
			HashSet<int> hashSet = new HashSet<int>();
			this.POIBounds = new List<RectInt>();
			this.WriteOverWorldNoise(this.successCallbackFn);
			if (!this.RenderToMap(this.successCallbackFn, ref cells, ref array, ref dc, ref hashSet, ref this.POIBounds))
			{
				this.successCallbackFn(UI.WORLDGEN.FAILED.key, -100f, WorldGenProgressStages.Stages.Failure);
				if (!this.isRunningDebugGen)
				{
					return false;
				}
			}
			foreach (int num in hashSet)
			{
				cells[num].SetValues(WorldGen.unobtaniumElement, ElementLoader.elements);
				this.claimedPOICells[num] = 1;
			}
			List<KeyValuePair<Vector2I, TemplateContainer>> list = null;
			try
			{
				list = TemplateSpawning.DetermineTemplatesForWorld(this.Settings, this.data.terrainCells, this.myRandom, ref this.POIBounds, this.isRunningDebugGen, ref placedStoryTraits, this.successCallbackFn);
			}
			catch (TemplateSpawningException ex)
			{
				if (!this.isRunningDebugGen)
				{
					this.ReportWorldGenError(ex, ex.userMessage);
					return false;
				}
			}
			catch (Exception ex2)
			{
				if (!this.isRunningDebugGen)
				{
					this.ReportWorldGenError(ex2, null);
					return false;
				}
			}
			if (isStartingWorld)
			{
				this.EnsureEnoughElementsInStartingBiome(cells);
			}
			List<TerrainCell> terrainCellsForTag = this.GetTerrainCellsForTag(WorldGenTags.StartWorld);
			foreach (TerrainCell terrainCell in this.OverworldCells)
			{
				foreach (TerrainCell terrainCell2 in terrainCellsForTag)
				{
					if (terrainCell.poly.PointInPolygon(terrainCell2.poly.Centroid()))
					{
						terrainCell.node.tags.Add(WorldGenTags.StartWorld);
						break;
					}
				}
			}
			if (doSettle)
			{
				this.running = WorldGenSimUtil.DoSettleSim(this.Settings, ref cells, ref array, ref dc, this.successCallbackFn, this.data, list, this.errorCallback, baseId);
			}
			foreach (KeyValuePair<Vector2I, TemplateContainer> keyValuePair in list)
			{
				this.PlaceTemplateSpawners(keyValuePair.Key, keyValuePair.Value, ref this.claimedPOICells);
			}
			if (doSettle)
			{
				this.SpawnMobsAndTemplates(cells, array, dc, new HashSet<int>(this.claimedPOICells.Keys));
			}
			this.successCallbackFn(UI.WORLDGEN.COMPLETE.key, 1f, WorldGenProgressStages.Stages.Complete);
			this.running = false;
			return true;
		}

		// Token: 0x060063B0 RID: 25520 RVA: 0x00252064 File Offset: 0x00250264
		private void SpawnMobsAndTemplates(Sim.Cell[] cells, float[] bgTemp, Sim.DiseaseCell[] dc, HashSet<int> claimedCells)
		{
			MobSpawning.DetectNaturalCavities(this.TerrainCells, this.successCallbackFn, cells);
			SeededRandom seededRandom = new SeededRandom(this.data.globalTerrainSeed);
			for (int i = 0; i < this.TerrainCells.Count; i++)
			{
				float num = (float)i / (float)this.TerrainCells.Count;
				this.successCallbackFn(UI.WORLDGEN.PLACINGCREATURES.key, num, WorldGenProgressStages.Stages.PlacingCreatures);
				TerrainCell terrainCell = this.TerrainCells[i];
				Dictionary<int, string> dictionary = MobSpawning.PlaceFeatureAmbientMobs(this.Settings, terrainCell, seededRandom, cells, bgTemp, dc, claimedCells, this.isRunningDebugGen);
				if (dictionary != null)
				{
					this.data.gameSpawnData.AddRange(dictionary);
				}
				dictionary = MobSpawning.PlaceBiomeAmbientMobs(this.Settings, terrainCell, seededRandom, cells, bgTemp, dc, claimedCells, this.isRunningDebugGen);
				if (dictionary != null)
				{
					this.data.gameSpawnData.AddRange(dictionary);
				}
			}
			this.successCallbackFn(UI.WORLDGEN.PLACINGCREATURES.key, 1f, WorldGenProgressStages.Stages.PlacingCreatures);
		}

		// Token: 0x060063B1 RID: 25521 RVA: 0x00252164 File Offset: 0x00250364
		public void ReportWorldGenError(Exception e, string errorMessage = null)
		{
			if (errorMessage == null)
			{
				errorMessage = UI.FRONTEND.SUPPORTWARNINGS.WORLD_GEN_FAILURE;
			}
			bool flag = FileSystem.IsModdedFile(SettingsCache.RewriteWorldgenPathYaml(this.Settings.world.filePath));
			string text = ((CustomGameSettings.Instance != null) ? CustomGameSettings.Instance.GetSettingsCoordinate() : this.data.globalWorldLayoutSeed.ToString());
			global::Debug.LogWarning(string.Format("Worldgen Failure on seed {0}, modded={1}", text, flag));
			if (this.errorCallback != null)
			{
				this.errorCallback(new OfflineWorldGen.ErrorInfo
				{
					errorDesc = string.Format(errorMessage, text),
					exception = e
				});
			}
			if (!flag)
			{
				KCrashReporter.ReportErrorDevNotification("WorldgenFailure", e.StackTrace, text + " - " + e.Message);
			}
		}

		// Token: 0x060063B2 RID: 25522 RVA: 0x00252234 File Offset: 0x00250434
		public void SetWorldSize(int width, int height)
		{
			if (this.data.world != null && this.data.world.offset != Vector2I.zero)
			{
				global::Debug.LogWarning("Resetting world chunk to defaults.");
			}
			this.data.world = new Chunk(0, 0, width, height);
		}

		// Token: 0x060063B3 RID: 25523 RVA: 0x00252288 File Offset: 0x00250488
		public Vector2I GetSize()
		{
			return this.data.world.size;
		}

		// Token: 0x060063B4 RID: 25524 RVA: 0x0025229A File Offset: 0x0025049A
		public void SetPosition(Vector2I position)
		{
			this.data.world.offset = position;
		}

		// Token: 0x060063B5 RID: 25525 RVA: 0x002522AD File Offset: 0x002504AD
		public Vector2I GetPosition()
		{
			return this.data.world.offset;
		}

		// Token: 0x060063B6 RID: 25526 RVA: 0x002522BF File Offset: 0x002504BF
		public void SetClusterLocation(AxialI location)
		{
			this.data.clusterLocation = location;
		}

		// Token: 0x060063B7 RID: 25527 RVA: 0x002522CD File Offset: 0x002504CD
		public AxialI GetClusterLocation()
		{
			return this.data.clusterLocation;
		}

		// Token: 0x060063B8 RID: 25528 RVA: 0x002522DC File Offset: 0x002504DC
		public bool GenerateNoiseData(WorldGen.OfflineCallbackFunction updateProgressFn)
		{
			this.stats["GenerateNoiseTime"] = System.DateTime.Now.Ticks;
			try
			{
				this.running = updateProgressFn(UI.WORLDGEN.SETUPNOISE.key, 0f, WorldGenProgressStages.Stages.SetupNoise);
				if (!this.running)
				{
					this.stats["GenerateNoiseTime"] = 0;
					return false;
				}
				this.SetupNoise(updateProgressFn);
				this.running = updateProgressFn(UI.WORLDGEN.SETUPNOISE.key, 1f, WorldGenProgressStages.Stages.SetupNoise);
				if (!this.running)
				{
					this.stats["GenerateNoiseTime"] = 0;
					return false;
				}
				this.GenerateUnChunkedNoise(updateProgressFn);
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				string stackTrace = ex.StackTrace;
				this.ReportWorldGenError(ex, null);
				WorldGenLogger.LogException(message, stackTrace);
				this.running = this.successCallbackFn(new StringKey("Exception in GenerateNoiseData"), -1f, WorldGenProgressStages.Stages.Failure);
				return false;
			}
			this.stats["GenerateNoiseTime"] = System.DateTime.Now.Ticks - (long)this.stats["GenerateNoiseTime"];
			return true;
		}

		// Token: 0x060063B9 RID: 25529 RVA: 0x0025242C File Offset: 0x0025062C
		public bool GenerateLayout(WorldGen.OfflineCallbackFunction updateProgressFn)
		{
			this.stats["GenerateLayoutTime"] = System.DateTime.Now.Ticks;
			try
			{
				this.running = updateProgressFn(UI.WORLDGEN.WORLDLAYOUT.key, 0f, WorldGenProgressStages.Stages.WorldLayout);
				if (!this.running)
				{
					return false;
				}
				global::Debug.Assert(this.data.world.size.x != 0 && this.data.world.size.y != 0, "Map size has not been set");
				this.data.worldLayout = new WorldLayout(this, this.data.world.size.x, this.data.world.size.y, this.data.globalWorldLayoutSeed);
				this.running = updateProgressFn(UI.WORLDGEN.WORLDLAYOUT.key, 1f, WorldGenProgressStages.Stages.WorldLayout);
				this.data.voronoiTree = null;
				try
				{
					this.data.voronoiTree = this.WorldLayout.GenerateOverworld(this.Settings.world.layoutMethod == ProcGen.World.LayoutMethod.PowerTree, this.isRunningDebugGen);
					this.WorldLayout.PopulateSubworlds();
					this.CompleteLayout(updateProgressFn);
				}
				catch (Exception ex)
				{
					string message = ex.Message;
					string stackTrace = ex.StackTrace;
					WorldGenLogger.LogException(message, stackTrace);
					this.ReportWorldGenError(ex, null);
					this.running = updateProgressFn(new StringKey("Exception in InitVoronoiTree"), -1f, WorldGenProgressStages.Stages.Failure);
					return false;
				}
				this.data.overworldCells = new List<TerrainCell>(40);
				for (int i = 0; i < this.data.voronoiTree.ChildCount(); i++)
				{
					VoronoiTree.Tree tree = this.data.voronoiTree.GetChild(i) as VoronoiTree.Tree;
					Cell cell = this.data.worldLayout.overworldGraph.FindNodeByID(tree.site.id);
					this.data.overworldCells.Add(new TerrainCellLogged(cell, tree.site, tree.minDistanceToTag));
				}
				this.running = updateProgressFn(UI.WORLDGEN.WORLDLAYOUT.key, 1f, WorldGenProgressStages.Stages.WorldLayout);
			}
			catch (Exception ex2)
			{
				string message2 = ex2.Message;
				string stackTrace2 = ex2.StackTrace;
				WorldGenLogger.LogException(message2, stackTrace2);
				this.ReportWorldGenError(ex2, null);
				this.successCallbackFn(new StringKey("Exception in GenerateLayout"), -1f, WorldGenProgressStages.Stages.Failure);
				return false;
			}
			this.stats["GenerateLayoutTime"] = System.DateTime.Now.Ticks - (long)this.stats["GenerateLayoutTime"];
			return true;
		}

		// Token: 0x060063BA RID: 25530 RVA: 0x00252714 File Offset: 0x00250914
		public bool CompleteLayout(WorldGen.OfflineCallbackFunction updateProgressFn)
		{
			long num = System.DateTime.Now.Ticks;
			try
			{
				this.running = updateProgressFn(UI.WORLDGEN.COMPLETELAYOUT.key, 0f, WorldGenProgressStages.Stages.CompleteLayout);
				if (!this.running)
				{
					return false;
				}
				this.data.terrainCells = null;
				this.running = updateProgressFn(UI.WORLDGEN.COMPLETELAYOUT.key, 0.65f, WorldGenProgressStages.Stages.CompleteLayout);
				if (!this.running)
				{
					return false;
				}
				this.running = updateProgressFn(UI.WORLDGEN.COMPLETELAYOUT.key, 0.75f, WorldGenProgressStages.Stages.CompleteLayout);
				if (!this.running)
				{
					return false;
				}
				this.data.terrainCells = new List<TerrainCell>(4000);
				List<VoronoiTree.Node> list = new List<VoronoiTree.Node>();
				this.data.voronoiTree.ForceLowestToLeaf();
				this.ApplyStartNode();
				this.ApplySwapTags();
				this.data.voronoiTree.GetLeafNodes(list, null);
				WorldLayout.ResetMapGraphFromVoronoiTree(list, this.WorldLayout.localGraph, true);
				for (int i = 0; i < list.Count; i++)
				{
					VoronoiTree.Node node = list[i];
					Cell tn = this.data.worldLayout.localGraph.FindNodeByID(node.site.id);
					if (tn != null)
					{
						TerrainCell terrainCell = this.data.terrainCells.Find((TerrainCell c) => c.node == tn);
						if (terrainCell == null)
						{
							TerrainCell terrainCell2 = new TerrainCellLogged(tn, node.site, node.parent.minDistanceToTag);
							this.data.terrainCells.Add(terrainCell2);
						}
						else
						{
							global::Debug.LogWarning("Duplicate cell found" + terrainCell.node.NodeId.ToString());
						}
					}
				}
				for (int j = 0; j < this.data.terrainCells.Count; j++)
				{
					TerrainCell terrainCell3 = this.data.terrainCells[j];
					for (int k = j + 1; k < this.data.terrainCells.Count; k++)
					{
						int num2 = 0;
						TerrainCell terrainCell4 = this.data.terrainCells[k];
						LineSegment lineSegment;
						if (terrainCell4.poly.SharesEdge(terrainCell3.poly, ref num2, out lineSegment) == Polygon.Commonality.Edge)
						{
							terrainCell3.neighbourTerrainCells.Add(k);
							terrainCell4.neighbourTerrainCells.Add(j);
						}
					}
				}
				this.running = updateProgressFn(UI.WORLDGEN.COMPLETELAYOUT.key, 1f, WorldGenProgressStages.Stages.CompleteLayout);
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				string stackTrace = ex.StackTrace;
				WorldGenLogger.LogException(message, stackTrace);
				this.successCallbackFn(new StringKey("Exception in CompleteLayout"), -1f, WorldGenProgressStages.Stages.Failure);
				return false;
			}
			num = System.DateTime.Now.Ticks - num;
			this.stats["GenerateLayoutTime"] = (long)this.stats["GenerateLayoutTime"] + num;
			return true;
		}

		// Token: 0x060063BB RID: 25531 RVA: 0x00252A4C File Offset: 0x00250C4C
		public void UpdateVoronoiNodeTags(VoronoiTree.Node node)
		{
			ProcGen.Node node2;
			if (node.tags.Contains(WorldGenTags.Overworld))
			{
				node2 = this.WorldLayout.overworldGraph.FindNodeByID(node.site.id);
			}
			else
			{
				node2 = this.WorldLayout.localGraph.FindNodeByID(node.site.id);
			}
			if (node2 != null)
			{
				node2.tags.Union(node.tags);
			}
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x00252ABC File Offset: 0x00250CBC
		public bool GenerateWorldData()
		{
			this.stats["GenerateDataTime"] = System.DateTime.Now.Ticks;
			if (this.generateNoiseData && !this.GenerateNoiseData(this.successCallbackFn))
			{
				return false;
			}
			if (!this.GenerateLayout(this.successCallbackFn))
			{
				return false;
			}
			this.stats["GenerateDataTime"] = System.DateTime.Now.Ticks - (long)this.stats["GenerateDataTime"];
			return true;
		}

		// Token: 0x060063BD RID: 25533 RVA: 0x00252B4C File Offset: 0x00250D4C
		public void EnsureEnoughElementsInStartingBiome(Sim.Cell[] cells)
		{
			List<StartingWorldElementSetting> defaultStartingElements = this.Settings.GetDefaultStartingElements();
			List<TerrainCell> terrainCellsForTag = this.GetTerrainCellsForTag(WorldGenTags.StartWorld);
			foreach (StartingWorldElementSetting startingWorldElementSetting in defaultStartingElements)
			{
				float amount = startingWorldElementSetting.amount;
				Element element = ElementLoader.GetElement(new Tag(((SimHashes)Enum.Parse(typeof(SimHashes), startingWorldElementSetting.element, true)).ToString()));
				float num = 0f;
				int num2 = 0;
				foreach (TerrainCell terrainCell in terrainCellsForTag)
				{
					foreach (int num3 in terrainCell.GetAllCells())
					{
						if (element.idx == cells[num3].elementIdx)
						{
							num2++;
							num += cells[num3].mass;
						}
					}
				}
				DebugUtil.DevAssert(num2 > 0, string.Format("No {0} found in starting biome and trying to ensure at least {1}. Skipping.", element.id, amount), null);
				if (num < amount && num2 > 0)
				{
					float num4 = num / (float)num2;
					float num5 = (amount - num) / (float)num2;
					DebugUtil.DevAssert(num4 + num5 <= 2f * element.maxMass, string.Format("Number of cells ({0}) of {1} in the starting biome is insufficient, this will result in extremely dense cells. {2} but expecting less than {3}", new object[]
					{
						num2,
						element.id,
						num4 + num5,
						2f * element.maxMass
					}), null);
					foreach (TerrainCell terrainCell2 in terrainCellsForTag)
					{
						foreach (int num6 in terrainCell2.GetAllCells())
						{
							if (element.idx == cells[num6].elementIdx)
							{
								int num7 = num6;
								cells[num7].mass = cells[num7].mass + num5;
							}
						}
					}
				}
			}
		}

		// Token: 0x060063BE RID: 25534 RVA: 0x00252E20 File Offset: 0x00251020
		public bool RenderToMap(WorldGen.OfflineCallbackFunction updateProgressFn, ref Sim.Cell[] cells, ref float[] bgTemp, ref Sim.DiseaseCell[] dcs, ref HashSet<int> borderCells, ref List<RectInt> poiBounds)
		{
			global::Debug.Assert(Grid.WidthInCells == this.Settings.world.worldsize.x);
			global::Debug.Assert(Grid.HeightInCells == this.Settings.world.worldsize.y);
			global::Debug.Assert(Grid.CellCount == Grid.WidthInCells * Grid.HeightInCells);
			global::Debug.Assert(Grid.CellSizeInMeters != 0f);
			borderCells = new HashSet<int>();
			cells = new Sim.Cell[Grid.CellCount];
			bgTemp = new float[Grid.CellCount];
			dcs = new Sim.DiseaseCell[Grid.CellCount];
			this.running = updateProgressFn(UI.WORLDGEN.CLEARINGLEVEL.key, 0f, WorldGenProgressStages.Stages.ClearingLevel);
			if (!this.running)
			{
				return false;
			}
			for (int i = 0; i < cells.Length; i++)
			{
				cells[i].SetValues(WorldGen.katairiteElement, ElementLoader.elements);
				bgTemp[i] = -1f;
				dcs[i] = default(Sim.DiseaseCell);
				dcs[i].diseaseIdx = byte.MaxValue;
				this.running = updateProgressFn(UI.WORLDGEN.CLEARINGLEVEL.key, (float)i / (float)Grid.CellCount, WorldGenProgressStages.Stages.ClearingLevel);
				if (!this.running)
				{
					return false;
				}
			}
			updateProgressFn(UI.WORLDGEN.CLEARINGLEVEL.key, 1f, WorldGenProgressStages.Stages.ClearingLevel);
			try
			{
				this.ProcessByTerrainCell(cells, bgTemp, dcs, updateProgressFn, this.highPriorityClaims);
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				string stackTrace = ex.StackTrace;
				WorldGenLogger.LogException(message, stackTrace);
				this.running = updateProgressFn(new StringKey("Exception in ProcessByTerrainCell"), -1f, WorldGenProgressStages.Stages.Failure);
				return false;
			}
			if (this.Settings.GetBoolSetting("DrawWorldBorder"))
			{
				SeededRandom seededRandom = new SeededRandom(0);
				this.DrawWorldBorder(cells, this.data.world, seededRandom, ref borderCells, ref poiBounds, updateProgressFn);
				updateProgressFn(UI.WORLDGEN.DRAWWORLDBORDER.key, 1f, WorldGenProgressStages.Stages.DrawWorldBorder);
			}
			this.data.gameSpawnData.baseStartPos = this.data.worldLayout.GetStartLocation();
			return true;
		}

		// Token: 0x060063BF RID: 25535 RVA: 0x00253048 File Offset: 0x00251248
		public SubWorld GetSubWorldForNode(VoronoiTree.Tree node)
		{
			ProcGen.Node node2 = this.WorldLayout.overworldGraph.FindNodeByID(node.site.id);
			if (node2 == null)
			{
				return null;
			}
			if (!this.Settings.HasSubworld(node2.type))
			{
				return null;
			}
			return this.Settings.GetSubWorld(node2.type);
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x0025309C File Offset: 0x0025129C
		public VoronoiTree.Tree GetOverworldForNode(Leaf leaf)
		{
			if (leaf == null)
			{
				return null;
			}
			return this.data.worldLayout.GetVoronoiTree().GetChildContainingLeaf(leaf);
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x002530B9 File Offset: 0x002512B9
		public Leaf GetLeafForTerrainCell(TerrainCell cell)
		{
			if (cell == null)
			{
				return null;
			}
			return this.data.worldLayout.GetVoronoiTree().GetNodeForSite(cell.site) as Leaf;
		}

		// Token: 0x060063C2 RID: 25538 RVA: 0x002530E0 File Offset: 0x002512E0
		public List<TerrainCell> GetTerrainCellsForTag(Tag tag)
		{
			List<TerrainCell> list = new List<TerrainCell>();
			List<VoronoiTree.Node> leafNodesWithTag = this.WorldLayout.GetLeafNodesWithTag(tag);
			for (int i = 0; i < leafNodesWithTag.Count; i++)
			{
				VoronoiTree.Node node = leafNodesWithTag[i];
				TerrainCell terrainCell = this.data.terrainCells.Find((TerrainCell cell) => cell.site.id == node.site.id);
				if (terrainCell != null)
				{
					list.Add(terrainCell);
				}
			}
			return list;
		}

		// Token: 0x060063C3 RID: 25539 RVA: 0x00253150 File Offset: 0x00251350
		private void GetStartCells(out int baseX, out int baseY)
		{
			Vector2I startLocation = new Vector2I(this.data.world.size.x / 2, (int)((float)this.data.world.size.y * 0.7f));
			if (this.data.worldLayout != null)
			{
				startLocation = this.data.worldLayout.GetStartLocation();
			}
			baseX = startLocation.x;
			baseY = startLocation.y;
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x002531C8 File Offset: 0x002513C8
		public void FinalizeStartLocation()
		{
			if (string.IsNullOrEmpty(this.Settings.world.startSubworldName))
			{
				return;
			}
			List<VoronoiTree.Node> startNodes = this.WorldLayout.GetStartNodes();
			global::Debug.Assert(startNodes.Count > 0, "Couldn't find a start node on a world that expects it!!");
			TagSet tagSet = new TagSet { WorldGenTags.StartLocation };
			for (int i = 1; i < startNodes.Count; i++)
			{
				startNodes[i].tags.Remove(tagSet);
			}
		}

		// Token: 0x060063C5 RID: 25541 RVA: 0x00253240 File Offset: 0x00251440
		private void SwitchNodes(VoronoiTree.Node n1, VoronoiTree.Node n2)
		{
			if (n1 is VoronoiTree.Tree || n2 is VoronoiTree.Tree)
			{
				global::Debug.Log("WorldGen::SwitchNodes() Skipping tree node");
				return;
			}
			Diagram.Site site = n1.site;
			n1.site = n2.site;
			n2.site = site;
			Cell cell = this.data.worldLayout.localGraph.FindNodeByID(n1.site.id);
			ProcGen.Node node = this.data.worldLayout.localGraph.FindNodeByID(n2.site.id);
			string type = cell.type;
			cell.SetType(node.type);
			node.SetType(type);
		}

		// Token: 0x060063C6 RID: 25542 RVA: 0x002532DC File Offset: 0x002514DC
		private void ApplyStartNode()
		{
			List<VoronoiTree.Node> leafNodesWithTag = this.data.worldLayout.GetLeafNodesWithTag(WorldGenTags.StartLocation);
			if (leafNodesWithTag.Count == 0)
			{
				return;
			}
			VoronoiTree.Node node = leafNodesWithTag[0];
			VoronoiTree.Tree parent = node.parent;
			node.parent.AddTagToChildren(WorldGenTags.IgnoreCaveOverride);
			node.parent.tags.Remove(WorldGenTags.StartLocation);
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x0025333C File Offset: 0x0025153C
		private void ApplySwapTags()
		{
			List<VoronoiTree.Node> list = new List<VoronoiTree.Node>();
			for (int i = 0; i < this.data.voronoiTree.ChildCount(); i++)
			{
				if (this.data.voronoiTree.GetChild(i).tags.Contains(WorldGenTags.SwapLakesToBelow))
				{
					list.Add(this.data.voronoiTree.GetChild(i));
				}
			}
			foreach (VoronoiTree.Node node in list)
			{
				if (!node.tags.Contains(WorldGenTags.CenteralFeature))
				{
					List<VoronoiTree.Node> list2 = new List<VoronoiTree.Node>();
					((VoronoiTree.Tree)node).GetNodesWithoutTag(WorldGenTags.CenteralFeature, list2);
					this.SwapNodesAround(WorldGenTags.Wet, true, list2, node.site.poly.Centroid());
				}
			}
		}

		// Token: 0x060063C8 RID: 25544 RVA: 0x00253428 File Offset: 0x00251628
		private void SwapNodesAround(Tag swapTag, bool sendTagToBottom, List<VoronoiTree.Node> nodes, Vector2 pivot)
		{
			nodes.ShuffleSeeded(this.myRandom.RandomSource());
			List<VoronoiTree.Node> list = new List<VoronoiTree.Node>();
			List<VoronoiTree.Node> list2 = new List<VoronoiTree.Node>();
			foreach (VoronoiTree.Node node in nodes)
			{
				bool flag = node.tags.Contains(swapTag);
				bool flag2 = node.site.poly.Centroid().y > pivot.y;
				bool flag3 = (flag2 && sendTagToBottom) || (!flag2 && !sendTagToBottom);
				if (flag && flag3)
				{
					if (list2.Count > 0)
					{
						this.SwitchNodes(node, list2[0]);
						list2.RemoveAt(0);
					}
					else
					{
						list.Add(node);
					}
				}
				else if (!flag && !flag3)
				{
					if (list.Count > 0)
					{
						this.SwitchNodes(node, list[0]);
						list.RemoveAt(0);
					}
					else
					{
						list2.Add(node);
					}
				}
			}
			if (list2.Count > 0)
			{
				int num = 0;
				while (num < list.Count && list2.Count > 0)
				{
					this.SwitchNodes(list[num], list2[0]);
					list2.RemoveAt(0);
					num++;
				}
			}
		}

		// Token: 0x060063C9 RID: 25545 RVA: 0x00253578 File Offset: 0x00251778
		public void GetElementForBiomePoint(Chunk chunk, ElementBandConfiguration elementBands, Vector2I pos, out Element element, out Sim.PhysicsData pd, out Sim.DiseaseCell dc, float erode)
		{
			TerrainCell.ElementOverride elementOverride = TerrainCell.GetElementOverride(WorldGen.voidElement.tag.ToString(), null);
			elementOverride = this.GetElementFromBiomeElementTable(chunk, pos, elementBands, erode);
			element = elementOverride.element;
			pd = elementOverride.pdelement;
			dc = elementOverride.dc;
		}

		// Token: 0x060063CA RID: 25546 RVA: 0x002535D0 File Offset: 0x002517D0
		public void ConvertIntersectingCellsToType(MathUtil.Pair<Vector2, Vector2> segment, string type)
		{
			List<Vector2I> line = ProcGen.Util.GetLine(segment.First, segment.Second);
			for (int i = 0; i < this.data.terrainCells.Count; i++)
			{
				if (this.data.terrainCells[i].node.type != type)
				{
					for (int j = 0; j < line.Count; j++)
					{
						if (this.data.terrainCells[i].poly.Contains(line[j]))
						{
							this.data.terrainCells[i].node.SetType(type);
						}
					}
				}
			}
		}

		// Token: 0x060063CB RID: 25547 RVA: 0x00253688 File Offset: 0x00251888
		public string GetSubWorldType(Vector2I pos)
		{
			for (int i = 0; i < this.data.overworldCells.Count; i++)
			{
				if (this.data.overworldCells[i].poly.Contains(pos))
				{
					return this.data.overworldCells[i].node.type;
				}
			}
			return null;
		}

		// Token: 0x060063CC RID: 25548 RVA: 0x002536F0 File Offset: 0x002518F0
		private void ProcessByTerrainCell(Sim.Cell[] map_cells, float[] bgTemp, Sim.DiseaseCell[] dcs, WorldGen.OfflineCallbackFunction updateProgressFn, HashSet<int> hightPriorityCells)
		{
			updateProgressFn(UI.WORLDGEN.PROCESSING.key, 0f, WorldGenProgressStages.Stages.Processing);
			SeededRandom seededRandom = new SeededRandom(this.data.globalTerrainSeed);
			try
			{
				for (int i = 0; i < this.data.terrainCells.Count; i++)
				{
					updateProgressFn(UI.WORLDGEN.PROCESSING.key, (float)i / (float)this.data.terrainCells.Count, WorldGenProgressStages.Stages.Processing);
					this.data.terrainCells[i].Process(this, map_cells, bgTemp, dcs, this.data.world, seededRandom);
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				string stackTrace = ex.StackTrace;
				updateProgressFn(new StringKey("Exception in TerrainCell.Process"), -1f, WorldGenProgressStages.Stages.Failure);
				global::Debug.LogError("Error:" + message + "\n" + stackTrace);
			}
			List<Border> list = new List<Border>();
			updateProgressFn(UI.WORLDGEN.BORDERS.key, 0f, WorldGenProgressStages.Stages.Borders);
			try
			{
				List<Edge> edgesWithTag = this.data.worldLayout.overworldGraph.GetEdgesWithTag(WorldGenTags.EdgeUnpassable);
				for (int j = 0; j < edgesWithTag.Count; j++)
				{
					Edge edge = edgesWithTag[j];
					List<Cell> cells2 = this.data.worldLayout.overworldGraph.GetNodes(edge);
					global::Debug.Assert(cells2[0] != cells2[1], "Both nodes on an arc were the same. Allegedly this means it was a world border but I don't think we do that anymore.");
					TerrainCell terrainCell = this.data.overworldCells.Find((TerrainCell c) => c.node == cells2[0]);
					TerrainCell terrainCell2 = this.data.overworldCells.Find((TerrainCell c) => c.node == cells2[1]);
					global::Debug.Assert(terrainCell != null && terrainCell2 != null, "NULL Terrainell nodes with EdgeUnpassable");
					terrainCell.LogInfo("BORDER WITH " + terrainCell2.site.id.ToString(), "UNPASSABLE", 0f);
					terrainCell2.LogInfo("BORDER WITH " + terrainCell.site.id.ToString(), "UNPASSABLE", 0f);
					list.Add(new Border(new Neighbors(terrainCell, terrainCell2), edge.corner0.position, edge.corner1.position)
					{
						element = SettingsCache.borders["impenetrable"],
						width = (float)seededRandom.RandomRange(2, 3)
					});
				}
				List<Edge> edgesWithTag2 = this.data.worldLayout.overworldGraph.GetEdgesWithTag(WorldGenTags.EdgeClosed);
				for (int k = 0; k < edgesWithTag2.Count; k++)
				{
					Edge edge2 = edgesWithTag2[k];
					if (!edgesWithTag.Contains(edge2))
					{
						List<Cell> cells = this.data.worldLayout.overworldGraph.GetNodes(edge2);
						global::Debug.Assert(cells[0] != cells[1], "Both nodes on an arc were the same. Allegedly this means it was a world border but I don't think we do that anymore.");
						TerrainCell terrainCell3 = this.data.overworldCells.Find((TerrainCell c) => c.node == cells[0]);
						TerrainCell terrainCell4 = this.data.overworldCells.Find((TerrainCell c) => c.node == cells[1]);
						global::Debug.Assert(terrainCell3 != null && terrainCell4 != null, "NULL Terraincell nodes with EdgeClosed");
						string borderOverride = this.Settings.GetSubWorld(terrainCell3.node.type).borderOverride;
						string borderOverride2 = this.Settings.GetSubWorld(terrainCell4.node.type).borderOverride;
						string text;
						if (!string.IsNullOrEmpty(borderOverride2) && !string.IsNullOrEmpty(borderOverride))
						{
							int borderOverridePriority = this.Settings.GetSubWorld(terrainCell3.node.type).borderOverridePriority;
							int borderOverridePriority2 = this.Settings.GetSubWorld(terrainCell4.node.type).borderOverridePriority;
							if (borderOverridePriority == borderOverridePriority2)
							{
								text = ((seededRandom.RandomValue() > 0.5f) ? borderOverride2 : borderOverride);
								terrainCell3.LogInfo("BORDER WITH " + terrainCell4.site.id.ToString(), "Picked Random:" + text, 0f);
								terrainCell4.LogInfo("BORDER WITH " + terrainCell3.site.id.ToString(), "Picked Random:" + text, 0f);
							}
							else
							{
								text = ((borderOverridePriority > borderOverridePriority2) ? borderOverride : borderOverride2);
								terrainCell3.LogInfo("BORDER WITH " + terrainCell4.site.id.ToString(), "Picked priority:" + text, 0f);
								terrainCell4.LogInfo("BORDER WITH " + terrainCell3.site.id.ToString(), "Picked priority:" + text, 0f);
							}
						}
						else if (string.IsNullOrEmpty(borderOverride2) && string.IsNullOrEmpty(borderOverride))
						{
							text = "hardToDig";
							terrainCell3.LogInfo("BORDER WITH " + terrainCell4.site.id.ToString(), "Both null", 0f);
							terrainCell4.LogInfo("BORDER WITH " + terrainCell3.site.id.ToString(), "Both null", 0f);
						}
						else
						{
							text = ((!string.IsNullOrEmpty(borderOverride2)) ? borderOverride2 : borderOverride);
							terrainCell3.LogInfo("BORDER WITH " + terrainCell4.site.id.ToString(), "Picked specific " + text, 0f);
							terrainCell4.LogInfo("BORDER WITH " + terrainCell3.site.id.ToString(), "Picked specific " + text, 0f);
						}
						if (!(text == "NONE"))
						{
							Border border = new Border(new Neighbors(terrainCell3, terrainCell4), edge2.corner0.position, edge2.corner1.position);
							border.element = SettingsCache.borders[text];
							MinMax minMax = new MinMax(1.5f, 2f);
							MinMax borderSizeOverride = this.Settings.GetSubWorld(terrainCell3.node.type).borderSizeOverride;
							MinMax borderSizeOverride2 = this.Settings.GetSubWorld(terrainCell4.node.type).borderSizeOverride;
							bool flag = borderSizeOverride.min != 0f || borderSizeOverride.max != 0f;
							bool flag2 = borderSizeOverride2.min != 0f || borderSizeOverride2.max != 0f;
							if (flag && flag2)
							{
								minMax = ((borderSizeOverride.max > borderSizeOverride2.max) ? borderSizeOverride : borderSizeOverride2);
							}
							else if (flag)
							{
								minMax = borderSizeOverride;
							}
							else if (flag2)
							{
								minMax = borderSizeOverride2;
							}
							border.width = seededRandom.RandomRange(minMax.min, minMax.max);
							list.Add(border);
						}
					}
				}
			}
			catch (Exception ex2)
			{
				string message2 = ex2.Message;
				string stackTrace2 = ex2.StackTrace;
				updateProgressFn(new StringKey("Exception in Border creation"), -1f, WorldGenProgressStages.Stages.Failure);
				global::Debug.LogError("Error:" + message2 + " " + stackTrace2);
			}
			try
			{
				if (this.data.world.defaultTemp == null)
				{
					this.data.world.defaultTemp = new float[this.data.world.density.Length];
				}
				for (int l = 0; l < this.data.world.defaultTemp.Length; l++)
				{
					this.data.world.defaultTemp[l] = bgTemp[l];
				}
			}
			catch (Exception ex3)
			{
				string message3 = ex3.Message;
				string stackTrace3 = ex3.StackTrace;
				updateProgressFn(new StringKey("Exception in border.defaultTemp"), -1f, WorldGenProgressStages.Stages.Failure);
				global::Debug.LogError("Error:" + message3 + " " + stackTrace3);
			}
			try
			{
				TerrainCell.SetValuesFunction setValuesFunction = delegate(int index, object elem, Sim.PhysicsData pd, Sim.DiseaseCell dc)
				{
					if (!Grid.IsValidCell(index))
					{
						global::Debug.LogError(string.Concat(new string[]
						{
							"Process::SetValuesFunction Index [",
							index.ToString(),
							"] is not valid. cells.Length [",
							map_cells.Length.ToString(),
							"]"
						}));
						return;
					}
					if (this.highPriorityClaims.Contains(index))
					{
						return;
					}
					if ((elem as Element).HasTag(GameTags.Special))
					{
						pd = (elem as Element).defaultValues;
					}
					map_cells[index].SetValues(elem as Element, pd, ElementLoader.elements);
					dcs[index] = dc;
				};
				for (int m = 0; m < list.Count; m++)
				{
					Border border2 = list[m];
					SubWorld subWorld = this.Settings.GetSubWorld(border2.neighbors.n0.node.type);
					SubWorld subWorld2 = this.Settings.GetSubWorld(border2.neighbors.n1.node.type);
					float num = (SettingsCache.temperatures[subWorld.temperatureRange].min + SettingsCache.temperatures[subWorld.temperatureRange].max) / 2f;
					float num2 = (SettingsCache.temperatures[subWorld2.temperatureRange].min + SettingsCache.temperatures[subWorld2.temperatureRange].max) / 2f;
					float num3 = Mathf.Min(SettingsCache.temperatures[subWorld.temperatureRange].min, SettingsCache.temperatures[subWorld2.temperatureRange].min);
					float num4 = Mathf.Max(SettingsCache.temperatures[subWorld.temperatureRange].max, SettingsCache.temperatures[subWorld2.temperatureRange].max);
					float num5 = (num + num2) / 2f;
					float num6 = num4 - num3;
					float num7 = 2f;
					float num8 = 5f;
					int num9 = 1;
					if (num6 >= 150f)
					{
						num7 = 0f;
						num8 = border2.width * 0.2f;
						num9 = 2;
						border2.width = Mathf.Max(border2.width, 2f);
						float num10 = num - 273.15f;
						float num11 = num2 - 273.15f;
						if (Mathf.Abs(num10) < Mathf.Abs(num11))
						{
							num5 = num;
						}
						else
						{
							num5 = num2;
						}
					}
					border2.Stagger(seededRandom, (float)seededRandom.RandomRange(8, 13), seededRandom.RandomRange(num7, num8));
					border2.ConvertToMap(this.data.world, setValuesFunction, num, num2, num5, seededRandom, num9);
				}
			}
			catch (Exception ex4)
			{
				string message4 = ex4.Message;
				string stackTrace4 = ex4.StackTrace;
				updateProgressFn(new StringKey("Exception in border.ConvertToMap"), -1f, WorldGenProgressStages.Stages.Failure);
				global::Debug.LogError("Error:" + message4 + " " + stackTrace4);
			}
		}

		// Token: 0x060063CD RID: 25549 RVA: 0x002541F4 File Offset: 0x002523F4
		private void DrawWorldBorder(Sim.Cell[] cells, Chunk world, SeededRandom rnd, ref HashSet<int> borderCells, ref List<RectInt> poiBounds, WorldGen.OfflineCallbackFunction updateProgressFn)
		{
			WorldGen.<>c__DisplayClass140_0 CS$<>8__locals1 = new WorldGen.<>c__DisplayClass140_0();
			CS$<>8__locals1.world = world;
			bool boolSetting = this.Settings.GetBoolSetting("DrawWorldBorderForce");
			int intSetting = this.Settings.GetIntSetting("WorldBorderThickness");
			int intSetting2 = this.Settings.GetIntSetting("WorldBorderRange");
			ushort idx = WorldGen.vacuumElement.idx;
			ushort idx2 = WorldGen.voidElement.idx;
			ushort idx3 = WorldGen.unobtaniumElement.idx;
			float temperature = WorldGen.unobtaniumElement.defaultValues.temperature;
			float mass = WorldGen.unobtaniumElement.defaultValues.mass;
			int num = 0;
			int num2 = 0;
			updateProgressFn(UI.WORLDGEN.DRAWWORLDBORDER.key, 0f, WorldGenProgressStages.Stages.DrawWorldBorder);
			int num3 = CS$<>8__locals1.world.size.y - 1;
			int num4 = 0;
			int num5 = CS$<>8__locals1.world.size.x - 1;
			List<TerrainCell> terrainCellsForTag = this.GetTerrainCellsForTag(WorldGenTags.RemoveWorldBorderOverVacuum);
			int y;
			int num9;
			for (y = num3; y >= 0; y = num9 - 1)
			{
				updateProgressFn(UI.WORLDGEN.DRAWWORLDBORDER.key, (float)y / (float)num3 * 0.33f, WorldGenProgressStages.Stages.DrawWorldBorder);
				num = Mathf.Max(-intSetting2, Mathf.Min(num + rnd.RandomRange(-2, 2), intSetting2));
				bool flag = terrainCellsForTag.Find((TerrainCell n) => n.poly.Contains(new Vector2(0f, (float)y))) != null;
				for (int i = 0; i < intSetting + num; i++)
				{
					int num6 = Grid.XYToCell(i, y);
					if (boolSetting || (cells[num6].elementIdx != idx && cells[num6].elementIdx != idx2 && flag) || !flag)
					{
						borderCells.Add(num6);
						cells[num6].SetValues(idx3, temperature, mass);
						num4 = Mathf.Max(num4, i);
					}
				}
				num2 = Mathf.Max(-intSetting2, Mathf.Min(num2 + rnd.RandomRange(-2, 2), intSetting2));
				bool flag2 = terrainCellsForTag.Find((TerrainCell n) => n.poly.Contains(new Vector2((float)(CS$<>8__locals1.world.size.x - 1), (float)y))) != null;
				for (int j = 0; j < intSetting + num2; j++)
				{
					int num7 = CS$<>8__locals1.world.size.x - 1 - j;
					int num8 = Grid.XYToCell(num7, y);
					if (boolSetting || (cells[num8].elementIdx != idx && cells[num8].elementIdx != idx2 && flag2) || !flag2)
					{
						borderCells.Add(num8);
						cells[num8].SetValues(idx3, temperature, mass);
						num5 = Mathf.Min(num5, num7);
					}
				}
				num9 = y;
			}
			this.POIBounds.Add(new RectInt(0, 0, num4 + 1, this.World.size.y));
			this.POIBounds.Add(new RectInt(num5, 0, CS$<>8__locals1.world.size.x - num5, this.World.size.y));
			int num10 = 0;
			int num11 = 0;
			int num12 = 0;
			int num13 = this.World.size.y - 1;
			int x;
			for (x = 0; x < CS$<>8__locals1.world.size.x; x = num9 + 1)
			{
				updateProgressFn(UI.WORLDGEN.DRAWWORLDBORDER.key, (float)x / (float)CS$<>8__locals1.world.size.x * 0.66f + 0.33f, WorldGenProgressStages.Stages.DrawWorldBorder);
				num10 = Mathf.Max(-intSetting2, Mathf.Min(num10 + rnd.RandomRange(-2, 2), intSetting2));
				bool flag3 = terrainCellsForTag.Find((TerrainCell n) => n.poly.Contains(new Vector2((float)x, 0f))) != null;
				for (int k = 0; k < intSetting + num10; k++)
				{
					int num14 = Grid.XYToCell(x, k);
					if (boolSetting || (cells[num14].elementIdx != idx && cells[num14].elementIdx != idx2 && flag3) || !flag3)
					{
						borderCells.Add(num14);
						cells[num14].SetValues(idx3, temperature, mass);
						num12 = Mathf.Max(num12, k);
					}
				}
				num11 = Mathf.Max(-intSetting2, Mathf.Min(num11 + rnd.RandomRange(-2, 2), intSetting2));
				bool flag4 = terrainCellsForTag.Find((TerrainCell n) => n.poly.Contains(new Vector2((float)x, (float)(CS$<>8__locals1.world.size.y - 1)))) != null;
				for (int l = 0; l < intSetting + num11; l++)
				{
					int num15 = CS$<>8__locals1.world.size.y - 1 - l;
					int num16 = Grid.XYToCell(x, num15);
					if (boolSetting || (cells[num16].elementIdx != idx && cells[num16].elementIdx != idx2 && flag4) || !flag4)
					{
						borderCells.Add(num16);
						cells[num16].SetValues(idx3, temperature, mass);
						num13 = Mathf.Min(num13, num15);
					}
				}
				num9 = x;
			}
			this.POIBounds.Add(new RectInt(0, 0, this.World.size.x, num12 + 1));
			this.POIBounds.Add(new RectInt(0, num13, this.World.size.x, this.World.size.y - num13));
		}

		// Token: 0x060063CE RID: 25550 RVA: 0x002547C0 File Offset: 0x002529C0
		private void SetupNoise(WorldGen.OfflineCallbackFunction updateProgressFn)
		{
			updateProgressFn(UI.WORLDGEN.BUILDNOISESOURCE.key, 0f, WorldGenProgressStages.Stages.SetupNoise);
			this.heatSource = this.BuildNoiseSource(this.data.world.size.x, this.data.world.size.y, "noise/Heat");
			updateProgressFn(UI.WORLDGEN.BUILDNOISESOURCE.key, 1f, WorldGenProgressStages.Stages.SetupNoise);
		}

		// Token: 0x060063CF RID: 25551 RVA: 0x00254838 File Offset: 0x00252A38
		public NoiseMapBuilderPlane BuildNoiseSource(int width, int height, string name)
		{
			ProcGen.Noise.Tree tree = SettingsCache.noise.GetTree(name);
			global::Debug.Assert(tree != null, name);
			return this.BuildNoiseSource(width, height, tree);
		}

		// Token: 0x060063D0 RID: 25552 RVA: 0x00254864 File Offset: 0x00252A64
		public NoiseMapBuilderPlane BuildNoiseSource(int width, int height, ProcGen.Noise.Tree tree)
		{
			Vector2f lowerBound = tree.settings.lowerBound;
			Vector2f upperBound = tree.settings.upperBound;
			global::Debug.Assert(lowerBound.x < upperBound.x, string.Concat(new string[]
			{
				"BuildNoiseSource X range broken [l: ",
				lowerBound.x.ToString(),
				" h: ",
				upperBound.x.ToString(),
				"]"
			}));
			global::Debug.Assert(lowerBound.y < upperBound.y, string.Concat(new string[]
			{
				"BuildNoiseSource Y range broken [l: ",
				lowerBound.y.ToString(),
				" h: ",
				upperBound.y.ToString(),
				"]"
			}));
			global::Debug.Assert(width > 0, "BuildNoiseSource width <=0: [" + width.ToString() + "]");
			global::Debug.Assert(height > 0, "BuildNoiseSource height <=0: [" + height.ToString() + "]");
			NoiseMapBuilderPlane noiseMapBuilderPlane = new NoiseMapBuilderPlane(lowerBound.x, upperBound.x, lowerBound.y, upperBound.y, false);
			noiseMapBuilderPlane.SetSize(width, height);
			noiseMapBuilderPlane.SourceModule = tree.BuildFinalModule(this.data.globalNoiseSeed);
			return noiseMapBuilderPlane;
		}

		// Token: 0x060063D1 RID: 25553 RVA: 0x002549AC File Offset: 0x00252BAC
		private void GetMinMaxDataValues(float[] data, int width, int height)
		{
		}

		// Token: 0x060063D2 RID: 25554 RVA: 0x002549B0 File Offset: 0x00252BB0
		public static NoiseMap BuildNoiseMap(Vector2 offset, float zoom, NoiseMapBuilderPlane nmbp, int width, int height, NoiseMapBuilderCallback cb = null)
		{
			double num = (double)offset.x;
			double num2 = (double)offset.y;
			if (zoom == 0f)
			{
				zoom = 0.01f;
			}
			double num3 = num * (double)zoom;
			double num4 = (num + (double)width) * (double)zoom;
			double num5 = num2 * (double)zoom;
			double num6 = (num2 + (double)height) * (double)zoom;
			NoiseMap noiseMap = new NoiseMap(width, height);
			nmbp.NoiseMap = noiseMap;
			nmbp.SetBounds((float)num3, (float)num4, (float)num5, (float)num6);
			nmbp.CallBack = cb;
			nmbp.Build();
			return noiseMap;
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x00254A28 File Offset: 0x00252C28
		public static float[] GenerateNoise(Vector2 offset, float zoom, NoiseMapBuilderPlane nmbp, int width, int height, NoiseMapBuilderCallback cb = null)
		{
			NoiseMap noiseMap = WorldGen.BuildNoiseMap(offset, zoom, nmbp, width, height, cb);
			float[] array = new float[noiseMap.Width * noiseMap.Height];
			noiseMap.CopyTo(ref array);
			return array;
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x00254A60 File Offset: 0x00252C60
		public static void Normalise(float[] data)
		{
			global::Debug.Assert(data != null && data.Length != 0, "MISSING DATA FOR NORMALIZE");
			float num = float.MaxValue;
			float num2 = float.MinValue;
			for (int i = 0; i < data.Length; i++)
			{
				num = Mathf.Min(data[i], num);
				num2 = Mathf.Max(data[i], num2);
			}
			float num3 = num2 - num;
			for (int j = 0; j < data.Length; j++)
			{
				data[j] = (data[j] - num) / num3;
			}
		}

		// Token: 0x060063D5 RID: 25557 RVA: 0x00254AD4 File Offset: 0x00252CD4
		private void GenerateUnChunkedNoise(WorldGen.OfflineCallbackFunction updateProgressFn)
		{
			Vector2 vector = new Vector2(0f, 0f);
			updateProgressFn(UI.WORLDGEN.GENERATENOISE.key, 0f, WorldGenProgressStages.Stages.GenerateNoise);
			NoiseMapBuilderCallback noiseMapBuilderCallback = delegate(int line)
			{
				updateProgressFn(UI.WORLDGEN.GENERATENOISE.key, (float)((int)(0f + 0.25f * ((float)line / (float)this.data.world.size.y))), WorldGenProgressStages.Stages.GenerateNoise);
			};
			noiseMapBuilderCallback = delegate(int line)
			{
				updateProgressFn(UI.WORLDGEN.GENERATENOISE.key, (float)((int)(0.25f + 0.25f * ((float)line / (float)this.data.world.size.y))), WorldGenProgressStages.Stages.GenerateNoise);
			};
			if (noiseMapBuilderCallback == null)
			{
				global::Debug.LogError("nupd is null");
			}
			this.data.world.heatOffset = WorldGen.GenerateNoise(vector, SettingsCache.noise.GetZoomForTree("noise/Heat"), this.heatSource, this.data.world.size.x, this.data.world.size.y, noiseMapBuilderCallback);
			this.data.world.data = new float[this.data.world.heatOffset.Length];
			this.data.world.density = new float[this.data.world.heatOffset.Length];
			this.data.world.overrides = new float[this.data.world.heatOffset.Length];
			updateProgressFn(UI.WORLDGEN.NORMALISENOISE.key, 0.5f, WorldGenProgressStages.Stages.GenerateNoise);
			if (SettingsCache.noise.ShouldNormaliseTree("noise/Heat"))
			{
				WorldGen.Normalise(this.data.world.heatOffset);
			}
			updateProgressFn(UI.WORLDGEN.NORMALISENOISE.key, 1f, WorldGenProgressStages.Stages.GenerateNoise);
		}

		// Token: 0x060063D6 RID: 25558 RVA: 0x00254C70 File Offset: 0x00252E70
		public void WriteOverWorldNoise(WorldGen.OfflineCallbackFunction updateProgressFn)
		{
			Dictionary<HashedString, WorldGen.NoiseNormalizationStats> dictionary = new Dictionary<HashedString, WorldGen.NoiseNormalizationStats>();
			float num = (float)this.OverworldCells.Count;
			float perCell = 1f / num;
			float currentProgress = 0f;
			foreach (TerrainCell terrainCell in this.OverworldCells)
			{
				ProcGen.Noise.Tree tree = SettingsCache.noise.GetTree("noise/Default");
				ProcGen.Noise.Tree tree2 = SettingsCache.noise.GetTree("noise/DefaultCave");
				ProcGen.Noise.Tree tree3 = SettingsCache.noise.GetTree("noise/DefaultDensity");
				string text = "noise/Default";
				string text2 = "noise/DefaultCave";
				string text3 = "noise/DefaultDensity";
				SubWorld subWorld = this.Settings.GetSubWorld(terrainCell.node.type);
				if (subWorld == null)
				{
					global::Debug.Log("Couldnt find Subworld for overworld node [" + terrainCell.node.type + "] using defaults");
				}
				else
				{
					if (subWorld.biomeNoise != null)
					{
						ProcGen.Noise.Tree tree4 = SettingsCache.noise.GetTree(subWorld.biomeNoise);
						if (tree4 != null)
						{
							tree = tree4;
							text = subWorld.biomeNoise;
						}
					}
					if (subWorld.overrideNoise != null)
					{
						ProcGen.Noise.Tree tree5 = SettingsCache.noise.GetTree(subWorld.overrideNoise);
						if (tree5 != null)
						{
							tree2 = tree5;
							text2 = subWorld.overrideNoise;
						}
					}
					if (subWorld.densityNoise != null)
					{
						ProcGen.Noise.Tree tree6 = SettingsCache.noise.GetTree(subWorld.densityNoise);
						if (tree6 != null)
						{
							tree3 = tree6;
							text3 = subWorld.densityNoise;
						}
					}
				}
				WorldGen.NoiseNormalizationStats noiseNormalizationStats;
				if (!dictionary.TryGetValue(text, out noiseNormalizationStats))
				{
					noiseNormalizationStats = new WorldGen.NoiseNormalizationStats(this.BaseNoiseMap);
					dictionary.Add(text, noiseNormalizationStats);
				}
				WorldGen.NoiseNormalizationStats noiseNormalizationStats2;
				if (!dictionary.TryGetValue(text2, out noiseNormalizationStats2))
				{
					noiseNormalizationStats2 = new WorldGen.NoiseNormalizationStats(this.OverrideMap);
					dictionary.Add(text2, noiseNormalizationStats2);
				}
				WorldGen.NoiseNormalizationStats noiseNormalizationStats3;
				if (!dictionary.TryGetValue(text3, out noiseNormalizationStats3))
				{
					noiseNormalizationStats3 = new WorldGen.NoiseNormalizationStats(this.DensityMap);
					dictionary.Add(text3, noiseNormalizationStats3);
				}
				int num2 = (int)Mathf.Ceil(terrainCell.poly.bounds.width + 2f);
				int height = (int)Mathf.Ceil(terrainCell.poly.bounds.height + 2f);
				int num3 = (int)Mathf.Floor(terrainCell.poly.bounds.xMin - 1f);
				int num4 = (int)Mathf.Floor(terrainCell.poly.bounds.yMin - 1f);
				Vector2 vector2;
				Vector2 vector = (vector2 = new Vector2((float)num3, (float)num4));
				NoiseMapBuilderCallback noiseMapBuilderCallback = delegate(int line)
				{
					updateProgressFn(UI.WORLDGEN.GENERATENOISE.key, (float)((int)(currentProgress + perCell * ((float)line / (float)height))), WorldGenProgressStages.Stages.NoiseMapBuilder);
				};
				NoiseMapBuilderPlane noiseMapBuilderPlane = this.BuildNoiseSource(num2, height, tree);
				NoiseMap noiseMap = WorldGen.BuildNoiseMap(vector, tree.settings.zoom, noiseMapBuilderPlane, num2, height, noiseMapBuilderCallback);
				NoiseMapBuilderPlane noiseMapBuilderPlane2 = this.BuildNoiseSource(num2, height, tree2);
				NoiseMap noiseMap2 = WorldGen.BuildNoiseMap(vector, tree2.settings.zoom, noiseMapBuilderPlane2, num2, height, noiseMapBuilderCallback);
				NoiseMapBuilderPlane noiseMapBuilderPlane3 = this.BuildNoiseSource(num2, height, tree3);
				NoiseMap noiseMap3 = WorldGen.BuildNoiseMap(vector, tree3.settings.zoom, noiseMapBuilderPlane3, num2, height, noiseMapBuilderCallback);
				vector2.x = (float)((int)Mathf.Floor(terrainCell.poly.bounds.xMin));
				while (vector2.x <= (float)((int)Mathf.Ceil(terrainCell.poly.bounds.xMax)))
				{
					vector2.y = (float)((int)Mathf.Floor(terrainCell.poly.bounds.yMin));
					while (vector2.y <= (float)((int)Mathf.Ceil(terrainCell.poly.bounds.yMax)))
					{
						if (terrainCell.poly.PointInPolygon(vector2))
						{
							int num5 = Grid.XYToCell((int)vector2.x, (int)vector2.y);
							if (tree.settings.normalise)
							{
								noiseNormalizationStats.cells.Add(num5);
							}
							if (tree2.settings.normalise)
							{
								noiseNormalizationStats2.cells.Add(num5);
							}
							if (tree3.settings.normalise)
							{
								noiseNormalizationStats3.cells.Add(num5);
							}
							int num6 = (int)vector2.x - num3;
							int num7 = (int)vector2.y - num4;
							this.BaseNoiseMap[num5] = noiseMap.GetValue(num6, num7);
							this.OverrideMap[num5] = noiseMap2.GetValue(num6, num7);
							this.DensityMap[num5] = noiseMap3.GetValue(num6, num7);
							noiseNormalizationStats.min = Mathf.Min(this.BaseNoiseMap[num5], noiseNormalizationStats.min);
							noiseNormalizationStats.max = Mathf.Max(this.BaseNoiseMap[num5], noiseNormalizationStats.max);
							noiseNormalizationStats2.min = Mathf.Min(this.OverrideMap[num5], noiseNormalizationStats2.min);
							noiseNormalizationStats2.max = Mathf.Max(this.OverrideMap[num5], noiseNormalizationStats2.max);
							noiseNormalizationStats3.min = Mathf.Min(this.DensityMap[num5], noiseNormalizationStats3.min);
							noiseNormalizationStats3.max = Mathf.Max(this.DensityMap[num5], noiseNormalizationStats3.max);
						}
						vector2.y += 1f;
					}
					vector2.x += 1f;
				}
			}
			foreach (KeyValuePair<HashedString, WorldGen.NoiseNormalizationStats> keyValuePair in dictionary)
			{
				float num8 = keyValuePair.Value.max - keyValuePair.Value.min;
				foreach (int num9 in keyValuePair.Value.cells)
				{
					keyValuePair.Value.noise[num9] = (keyValuePair.Value.noise[num9] - keyValuePair.Value.min) / num8;
				}
			}
		}

		// Token: 0x060063D7 RID: 25559 RVA: 0x00255318 File Offset: 0x00253518
		private float GetValue(Chunk chunk, Vector2I pos)
		{
			int num = pos.x + this.data.world.size.x * pos.y;
			if (num < 0 || num >= chunk.data.Length)
			{
				throw new ArgumentOutOfRangeException("chunkDataIndex [" + num.ToString() + "]", "chunk data length [" + chunk.data.Length.ToString() + "]");
			}
			return chunk.data[num];
		}

		// Token: 0x060063D8 RID: 25560 RVA: 0x0025539C File Offset: 0x0025359C
		public bool InChunkRange(Chunk chunk, Vector2I pos)
		{
			int num = pos.x + this.data.world.size.x * pos.y;
			return num >= 0 && num < chunk.data.Length;
		}

		// Token: 0x060063D9 RID: 25561 RVA: 0x002553E0 File Offset: 0x002535E0
		private TerrainCell.ElementOverride GetElementFromBiomeElementTable(Chunk chunk, Vector2I pos, List<ElementGradient> table, float erode)
		{
			float num = this.GetValue(chunk, pos) * erode;
			TerrainCell.ElementOverride elementOverride = TerrainCell.GetElementOverride(WorldGen.voidElement.tag.ToString(), null);
			if (table.Count == 0)
			{
				return elementOverride;
			}
			for (int i = 0; i < table.Count; i++)
			{
				global::Debug.Assert(table[i].content != null, i.ToString());
				if (num < table[i].maxValue)
				{
					return TerrainCell.GetElementOverride(table[i].content, table[i].overrides);
				}
			}
			return TerrainCell.GetElementOverride(table[table.Count - 1].content, table[table.Count - 1].overrides);
		}

		// Token: 0x060063DA RID: 25562 RVA: 0x002554A8 File Offset: 0x002536A8
		public static bool CanLoad(string fileName)
		{
			if (fileName == null || fileName == "")
			{
				return false;
			}
			bool flag;
			try
			{
				if (File.Exists(fileName))
				{
					using (BinaryReader binaryReader = new BinaryReader(File.Open(fileName, FileMode.Open)))
					{
						return binaryReader.BaseStream.CanRead;
					}
				}
				flag = false;
			}
			catch (FileNotFoundException)
			{
				flag = false;
			}
			catch (Exception ex)
			{
				DebugUtil.LogWarningArgs(new object[] { "Failed to read " + fileName + "\n" + ex.ToString() });
				flag = false;
			}
			return flag;
		}

		// Token: 0x060063DB RID: 25563 RVA: 0x00255550 File Offset: 0x00253750
		public void SaveWorldGen()
		{
			try
			{
				Manager.Clear();
				WorldGenSave worldGenSave = new WorldGenSave();
				worldGenSave.version = new Vector2I(1, 1);
				worldGenSave.stats = this.stats;
				worldGenSave.data = this.data;
				worldGenSave.worldID = this.Settings.world.filePath;
				worldGenSave.traitIDs = new List<string>(this.Settings.GetWorldTraitIDs());
				worldGenSave.storyTraitIDs = new List<string>(this.Settings.GetStoryTraitIDs());
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
					{
						try
						{
							Serializer.Serialize(worldGenSave, binaryWriter);
						}
						catch (Exception ex)
						{
							DebugUtil.LogErrorArgs(new object[] { "Couldn't serialize", ex.Message, ex.StackTrace });
						}
					}
					using (BinaryWriter binaryWriter2 = new BinaryWriter(File.Open(WorldGen.WORLDGEN_SAVE_FILENAME, FileMode.Create)))
					{
						Manager.SerializeDirectory(binaryWriter2);
						binaryWriter2.Write(memoryStream.ToArray());
					}
				}
			}
			catch (Exception ex2)
			{
				DebugUtil.LogErrorArgs(new object[] { "Couldn't write", ex2.Message, ex2.StackTrace });
			}
		}

		// Token: 0x060063DC RID: 25564 RVA: 0x002556C8 File Offset: 0x002538C8
		public static WorldGen Load(IReader reader, bool defaultDiscovered)
		{
			WorldGen worldGen2;
			try
			{
				WorldGenSave worldGenSave = new WorldGenSave();
				Deserializer.Deserialize(worldGenSave, reader);
				WorldGen worldGen = new WorldGen(worldGenSave.worldID, worldGenSave.data, worldGenSave.stats, worldGenSave.traitIDs, worldGenSave.storyTraitIDs, false);
				worldGen.isStartingWorld = true;
				if (worldGenSave.version.x != 1 || worldGenSave.version.y > 1)
				{
					DebugUtil.LogErrorArgs(new object[] { string.Concat(new string[]
					{
						"LoadWorldGenSim Error! Wrong save version Current: [",
						1.ToString(),
						".",
						1.ToString(),
						"] File: [",
						worldGenSave.version.x.ToString(),
						".",
						worldGenSave.version.y.ToString(),
						"]"
					}) });
					worldGen.wasLoaded = false;
				}
				else
				{
					worldGen.wasLoaded = true;
				}
				worldGen2 = worldGen;
			}
			catch (Exception ex)
			{
				DebugUtil.LogErrorArgs(new object[] { "WorldGen.Load Error!\n", ex.Message, ex.StackTrace });
				worldGen2 = null;
			}
			return worldGen2;
		}

		// Token: 0x060063DD RID: 25565 RVA: 0x00255800 File Offset: 0x00253A00
		public void DrawDebug()
		{
		}

		// Token: 0x040044FF RID: 17663
		private const string _SIM_SAVE_FILENAME = "WorldGenSimSave";

		// Token: 0x04004500 RID: 17664
		private const string _SIM_SAVE_EXTENSION = ".dat";

		// Token: 0x04004501 RID: 17665
		private const string _WORLDGEN_SAVE_FILENAME = "WorldGenDataSave.dat";

		// Token: 0x04004502 RID: 17666
		private const int heatScale = 2;

		// Token: 0x04004503 RID: 17667
		private const int UNPASSABLE_EDGE_COUNT = 4;

		// Token: 0x04004504 RID: 17668
		private const string heat_noise_name = "noise/Heat";

		// Token: 0x04004505 RID: 17669
		private const string default_base_noise_name = "noise/Default";

		// Token: 0x04004506 RID: 17670
		private const string default_cave_noise_name = "noise/DefaultCave";

		// Token: 0x04004507 RID: 17671
		private const string default_density_noise_name = "noise/DefaultDensity";

		// Token: 0x04004508 RID: 17672
		public const int WORLDGEN_SAVE_MAJOR_VERSION = 1;

		// Token: 0x04004509 RID: 17673
		public const int WORLDGEN_SAVE_MINOR_VERSION = 1;

		// Token: 0x0400450A RID: 17674
		private const float EXTREME_TEMPERATURE_BORDER_RANGE = 150f;

		// Token: 0x0400450B RID: 17675
		private const float EXTREME_TEMPERATURE_BORDER_MIN_WIDTH = 2f;

		// Token: 0x0400450C RID: 17676
		public static Element voidElement;

		// Token: 0x0400450D RID: 17677
		public static Element vacuumElement;

		// Token: 0x0400450E RID: 17678
		public static Element katairiteElement;

		// Token: 0x0400450F RID: 17679
		public static Element unobtaniumElement;

		// Token: 0x04004510 RID: 17680
		private static Diseases m_diseasesDb;

		// Token: 0x04004511 RID: 17681
		public bool isRunningDebugGen;

		// Token: 0x04004512 RID: 17682
		private bool generateNoiseData = true;

		// Token: 0x04004515 RID: 17685
		private HashSet<int> claimedCells = new HashSet<int>();

		// Token: 0x04004516 RID: 17686
		public Dictionary<int, int> claimedPOICells = new Dictionary<int, int>();

		// Token: 0x04004517 RID: 17687
		private HashSet<int> highPriorityClaims = new HashSet<int>();

		// Token: 0x04004518 RID: 17688
		public List<RectInt> POIBounds = new List<RectInt>();

		// Token: 0x04004519 RID: 17689
		private WorldGen.OfflineCallbackFunction successCallbackFn;

		// Token: 0x0400451A RID: 17690
		private bool running = true;

		// Token: 0x0400451B RID: 17691
		private Action<OfflineWorldGen.ErrorInfo> errorCallback;

		// Token: 0x0400451C RID: 17692
		private SeededRandom myRandom;

		// Token: 0x0400451D RID: 17693
		private NoiseMapBuilderPlane heatSource;

		// Token: 0x0400451F RID: 17695
		private bool wasLoaded;

		// Token: 0x04004520 RID: 17696
		public int polyIndex = -1;

		// Token: 0x04004521 RID: 17697
		public bool isStartingWorld;

		// Token: 0x04004522 RID: 17698
		public bool isModuleInterior;

		// Token: 0x04004523 RID: 17699
		private static Task loadSettingsTask;

		// Token: 0x02001AE9 RID: 6889
		// (Invoke) Token: 0x0600945E RID: 37982
		public delegate bool OfflineCallbackFunction(StringKey stringKeyRoot, float completePercent, WorldGenProgressStages.Stages stage);

		// Token: 0x02001AEA RID: 6890
		public enum GenerateSection
		{
			// Token: 0x04007911 RID: 30993
			SolarSystem,
			// Token: 0x04007912 RID: 30994
			WorldNoise,
			// Token: 0x04007913 RID: 30995
			WorldLayout,
			// Token: 0x04007914 RID: 30996
			RenderToMap,
			// Token: 0x04007915 RID: 30997
			CollectSpawners
		}

		// Token: 0x02001AEB RID: 6891
		private class NoiseNormalizationStats
		{
			// Token: 0x06009461 RID: 37985 RVA: 0x0031BB83 File Offset: 0x00319D83
			public NoiseNormalizationStats(float[] noise)
			{
				this.noise = noise;
			}

			// Token: 0x04007916 RID: 30998
			public float[] noise;

			// Token: 0x04007917 RID: 30999
			public float min = float.MaxValue;

			// Token: 0x04007918 RID: 31000
			public float max = float.MinValue;

			// Token: 0x04007919 RID: 31001
			public HashSet<int> cells = new HashSet<int>();
		}
	}
}
