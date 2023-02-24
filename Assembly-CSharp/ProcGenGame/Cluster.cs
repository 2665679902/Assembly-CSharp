﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Klei;
using KSerialization;
using ProcGen;
using STRINGS;
using UnityEngine;

namespace ProcGenGame
{
	// Token: 0x02000C46 RID: 3142
	[Serializable]
	public class Cluster
	{
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060063DE RID: 25566 RVA: 0x00255802 File Offset: 0x00253A02
		// (set) Token: 0x060063DF RID: 25567 RVA: 0x0025580A File Offset: 0x00253A0A
		public ClusterLayout clusterLayout { get; private set; }

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060063E0 RID: 25568 RVA: 0x00255813 File Offset: 0x00253A13
		// (set) Token: 0x060063E1 RID: 25569 RVA: 0x0025581B File Offset: 0x00253A1B
		public bool IsGenerationComplete { get; private set; }

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060063E2 RID: 25570 RVA: 0x00255824 File Offset: 0x00253A24
		public bool IsGenerating
		{
			get
			{
				return this.thread != null && this.thread.IsAlive;
			}
		}

		// Token: 0x060063E3 RID: 25571 RVA: 0x0025583B File Offset: 0x00253A3B
		private Cluster()
		{
		}

		// Token: 0x060063E4 RID: 25572 RVA: 0x00255874 File Offset: 0x00253A74
		public Cluster(string name, int seed, List<string> chosenStoryTraitIds, bool assertMissingTraits, bool skipWorldTraits)
		{
			DebugUtil.Assert(!string.IsNullOrEmpty(name), "Cluster file is missing");
			this.seed = seed;
			WorldGen.LoadSettings(false);
			this.clusterLayout = SettingsCache.clusterLayouts.clusterCache[name];
			this.unplacedStoryTraits = new List<WorldTrait>();
			if (!this.clusterLayout.disableStoryTraits)
			{
				this.chosenStoryTraitIds = chosenStoryTraitIds;
				using (List<string>.Enumerator enumerator = chosenStoryTraitIds.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						WorldTrait cachedStoryTrait = SettingsCache.GetCachedStoryTrait(text, assertMissingTraits);
						if (cachedStoryTrait != null)
						{
							this.unplacedStoryTraits.Add(cachedStoryTrait);
						}
					}
					goto IL_D5;
				}
			}
			this.chosenStoryTraitIds = new List<string>();
			IL_D5:
			this.Id = name;
			bool flag = seed > 0 && !skipWorldTraits;
			for (int i = 0; i < this.clusterLayout.worldPlacements.Count; i++)
			{
				ProcGen.World worldData = SettingsCache.worlds.GetWorldData(this.clusterLayout.worldPlacements[i].world);
				if (worldData != null)
				{
					this.clusterLayout.worldPlacements[i].SetSize(worldData.worldsize);
					if (i == this.clusterLayout.startWorldIndex)
					{
						this.clusterLayout.worldPlacements[i].startWorld = true;
					}
				}
			}
			this.size = BestFit.BestFitWorlds(this.clusterLayout.worldPlacements, false);
			foreach (WorldPlacement worldPlacement in this.clusterLayout.worldPlacements)
			{
				List<string> list = new List<string>();
				if (flag)
				{
					ProcGen.World worldData2 = SettingsCache.worlds.GetWorldData(worldPlacement.world);
					list = SettingsCache.GetRandomTraits(seed, worldData2);
					seed++;
				}
				WorldGen worldGen = new WorldGen(worldPlacement.world, list, null, assertMissingTraits);
				Vector2I worldsize = worldGen.Settings.world.worldsize;
				worldGen.SetWorldSize(worldsize.x, worldsize.y);
				worldGen.SetPosition(new Vector2I(worldPlacement.x, worldPlacement.y));
				this.worlds.Add(worldGen);
				if (worldPlacement.startWorld)
				{
					this.currentWorld = worldGen;
					worldGen.isStartingWorld = true;
				}
			}
			if (this.currentWorld == null)
			{
				global::Debug.LogWarning(string.Format("Start world not set. Defaulting to first world {0}", this.worlds[0].Settings.world.name));
				this.currentWorld = this.worlds[0];
			}
			if (this.clusterLayout.numRings > 0)
			{
				this.numRings = this.clusterLayout.numRings;
			}
		}

		// Token: 0x060063E5 RID: 25573 RVA: 0x00255B68 File Offset: 0x00253D68
		public void Reset()
		{
			this.worlds.Clear();
		}

		// Token: 0x060063E6 RID: 25574 RVA: 0x00255B78 File Offset: 0x00253D78
		private void LogBeginGeneration()
		{
			string text = ((CustomGameSettings.Instance != null) ? CustomGameSettings.Instance.GetSettingsCoordinate() : this.seed.ToString());
			Console.WriteLine("\n\n");
			DebugUtil.LogArgs(new object[] { "WORLDGEN START" });
			DebugUtil.LogArgs(new object[] { " - seed:     " + text });
			DebugUtil.LogArgs(new object[] { " - cluster:  " + this.clusterLayout.filePath });
			if (this.chosenStoryTraitIds.Count == 0)
			{
				DebugUtil.LogArgs(new object[] { " - storytraits: none" });
				return;
			}
			DebugUtil.LogArgs(new object[] { " - storytraits:" });
			foreach (string text2 in this.chosenStoryTraitIds)
			{
				DebugUtil.LogArgs(new object[] { "    - " + text2 });
			}
		}

		// Token: 0x060063E7 RID: 25575 RVA: 0x00255C90 File Offset: 0x00253E90
		public void Generate(WorldGen.OfflineCallbackFunction callbackFn, Action<OfflineWorldGen.ErrorInfo> error_cb, int worldSeed = -1, int layoutSeed = -1, int terrainSeed = -1, int noiseSeed = -1, bool doSimSettle = true, bool debug = false)
		{
			this.doSimSettle = doSimSettle;
			for (int num = 0; num != this.worlds.Count; num++)
			{
				this.worlds[num].Initialise(callbackFn, error_cb, worldSeed + num, layoutSeed + num, terrainSeed + num, noiseSeed + num, debug);
			}
			this.IsGenerationComplete = false;
			this.thread = new Thread(new ThreadStart(this.ThreadMain));
			global::Util.ApplyInvariantCultureToThread(this.thread);
			this.thread.Start();
		}

		// Token: 0x060063E8 RID: 25576 RVA: 0x00255D14 File Offset: 0x00253F14
		private void BeginGeneration()
		{
			this.LogBeginGeneration();
			Sim.Cell[] array = null;
			Sim.DiseaseCell[] array2 = null;
			int num = 0;
			List<WorldGen> list = new List<WorldGen>(this.worlds);
			list.Sort(delegate(WorldGen a, WorldGen b)
			{
				WorldPlacement worldPlacement = this.clusterLayout.worldPlacements.Find((WorldPlacement x) => x.world == a.Settings.world.filePath);
				WorldPlacement worldPlacement2 = this.clusterLayout.worldPlacements.Find((WorldPlacement x) => x.world == b.Settings.world.filePath);
				return WorldPlacement.CompareLocationType(worldPlacement, worldPlacement2);
			});
			for (int i = 0; i < list.Count; i++)
			{
				WorldGen worldGen = list[i];
				if (this.ShouldSkipWorldCallback == null || !this.ShouldSkipWorldCallback(i, worldGen))
				{
					DebugUtil.Separator();
					DebugUtil.LogArgs(new object[] { "Generating world: " + worldGen.Settings.world.filePath });
					if (worldGen.Settings.GetWorldTraitIDs().Length != 0)
					{
						DebugUtil.LogArgs(new object[] { " - worldtraits: " + string.Join(", ", worldGen.Settings.GetWorldTraitIDs().ToArray<string>()) });
					}
					if (this.PerWorldGenBeginCallback != null)
					{
						this.PerWorldGenBeginCallback(i, worldGen);
					}
					List<WorldTrait> list2 = new List<WorldTrait>();
					list2.AddRange(this.unplacedStoryTraits);
					worldGen.Settings.SetStoryTraitCandidates(list2);
					GridSettings.Reset(worldGen.GetSize().x, worldGen.GetSize().y);
					worldGen.GenerateOffline();
					worldGen.FinalizeStartLocation();
					array = null;
					array2 = null;
					List<WorldTrait> list3 = new List<WorldTrait>();
					if (!worldGen.RenderOffline(this.doSimSettle, ref array, ref array2, num, ref list3, worldGen.isStartingWorld))
					{
						this.thread = null;
						return;
					}
					if (this.PerWorldGenCompleteCallback != null)
					{
						this.PerWorldGenCompleteCallback(i, worldGen, array, array2);
					}
					foreach (WorldTrait worldTrait in list3)
					{
						this.unplacedStoryTraits.Remove(worldTrait);
					}
					num++;
				}
			}
			if (this.unplacedStoryTraits.Count > 0)
			{
				List<string> list4 = new List<string>();
				foreach (WorldTrait worldTrait2 in this.unplacedStoryTraits)
				{
					list4.Add(worldTrait2.filePath);
				}
				string text = "Story trait failure, unable to place on any world: " + string.Join(", ", list4.ToArray());
				if (!this.worlds[0].isRunningDebugGen)
				{
					this.worlds[0].ReportWorldGenError(new Exception(text), UI.FRONTEND.SUPPORTWARNINGS.WORLD_GEN_FAILURE_STORY);
				}
				DebugUtil.LogWarningArgs(Array.Empty<object>());
				this.thread = null;
				return;
			}
			DebugUtil.Separator();
			DebugUtil.LogArgs(new object[] { "Placing worlds on cluster map" });
			if (!this.AssignClusterLocations())
			{
				this.thread = null;
				return;
			}
			this.Save();
			this.thread = null;
			DebugUtil.Separator();
			DebugUtil.LogArgs(new object[] { "WORLDGEN COMPLETE\n\n\n" });
			this.IsGenerationComplete = true;
		}

		// Token: 0x060063E9 RID: 25577 RVA: 0x00256014 File Offset: 0x00254214
		private bool IsValidHex(AxialI location)
		{
			return location.IsWithinRadius(AxialI.ZERO, this.numRings - 1);
		}

		// Token: 0x060063EA RID: 25578 RVA: 0x0025602C File Offset: 0x0025422C
		public bool AssignClusterLocations()
		{
			this.myRandom = new SeededRandom(this.seed);
			ClusterLayout clusterLayout = SettingsCache.clusterLayouts.clusterCache[this.Id];
			List<WorldPlacement> list = new List<WorldPlacement>(clusterLayout.worldPlacements);
			List<SpaceMapPOIPlacement> list2 = ((clusterLayout.poiPlacements == null) ? new List<SpaceMapPOIPlacement>() : new List<SpaceMapPOIPlacement>(clusterLayout.poiPlacements));
			this.currentWorld.SetClusterLocation(AxialI.ZERO);
			HashSet<AxialI> assignedLocations = new HashSet<AxialI>();
			HashSet<AxialI> worldForbiddenLocations = new HashSet<AxialI>();
			new HashSet<AxialI>();
			HashSet<AxialI> poiWorldAvoidance = new HashSet<AxialI>();
			int num = 2;
			for (int i = 0; i < this.worlds.Count; i++)
			{
				WorldGen worldGen = this.worlds[i];
				WorldPlacement worldPlacement = list[i];
				DebugUtil.Assert(worldPlacement != null, "Somehow we're trying to generate a cluster with a world that isn't the cluster .yaml's world list!", worldGen.Settings.world.filePath);
				HashSet<AxialI> antiBuffer = new HashSet<AxialI>();
				foreach (AxialI axialI in assignedLocations)
				{
					antiBuffer.UnionWith(AxialUtil.GetRings(axialI, 1, worldPlacement.buffer));
				}
				List<AxialI> list3 = (from location in AxialUtil.GetRings(AxialI.ZERO, worldPlacement.allowedRings.min, Mathf.Min(worldPlacement.allowedRings.max, this.numRings - 1))
					where !assignedLocations.Contains(location) && !worldForbiddenLocations.Contains(location) && !antiBuffer.Contains(location)
					select location).ToList<AxialI>();
				if (list3.Count > 0)
				{
					AxialI axialI2 = list3[this.myRandom.RandomRange(0, list3.Count)];
					worldGen.SetClusterLocation(axialI2);
					assignedLocations.Add(axialI2);
					worldForbiddenLocations.UnionWith(AxialUtil.GetRings(axialI2, 1, worldPlacement.buffer));
					poiWorldAvoidance.UnionWith(AxialUtil.GetRings(axialI2, 1, num));
				}
				else
				{
					DebugUtil.DevLogError(string.Concat(new string[]
					{
						"Could not find a spot in the cluster for ",
						worldGen.Settings.world.filePath,
						". Check the placement settings in ",
						this.Id,
						".yaml to ensure there are no conflicts."
					}));
					HashSet<AxialI> minBuffers = new HashSet<AxialI>();
					foreach (AxialI axialI3 in assignedLocations)
					{
						minBuffers.UnionWith(AxialUtil.GetRings(axialI3, 1, 2));
					}
					list3 = (from location in AxialUtil.GetRings(AxialI.ZERO, worldPlacement.allowedRings.min, Mathf.Min(worldPlacement.allowedRings.max, this.numRings - 1))
						where !assignedLocations.Contains(location) && !minBuffers.Contains(location)
						select location).ToList<AxialI>();
					if (list3.Count <= 0)
					{
						string text = string.Concat(new string[]
						{
							"Could not find a spot in the cluster for ",
							worldGen.Settings.world.filePath,
							" EVEN AFTER REDUCING BUFFERS. Check the placement settings in ",
							this.Id,
							".yaml to ensure there are no conflicts."
						});
						DebugUtil.LogErrorArgs(new object[] { text });
						if (!worldGen.isRunningDebugGen)
						{
							this.currentWorld.ReportWorldGenError(new Exception(text), null);
						}
						return false;
					}
					AxialI axialI4 = list3[this.myRandom.RandomRange(0, list3.Count)];
					worldGen.SetClusterLocation(axialI4);
					assignedLocations.Add(axialI4);
					worldForbiddenLocations.UnionWith(AxialUtil.GetRings(axialI4, 1, worldPlacement.buffer));
					poiWorldAvoidance.UnionWith(AxialUtil.GetRings(axialI4, 1, num));
				}
			}
			if (DlcManager.FeatureClusterSpaceEnabled() && list2 != null)
			{
				HashSet<AxialI> poiClumpLocations = new HashSet<AxialI>();
				HashSet<AxialI> poiForbiddenLocations = new HashSet<AxialI>();
				float num2 = 0.5f;
				int num3 = 3;
				int num4 = 0;
				Func<AxialI, bool> <>9__2;
				Func<AxialI, bool> <>9__3;
				foreach (SpaceMapPOIPlacement spaceMapPOIPlacement in list2)
				{
					List<string> list4 = new List<string>(spaceMapPOIPlacement.pois);
					for (int j = 0; j < spaceMapPOIPlacement.numToSpawn; j++)
					{
						bool flag = this.myRandom.RandomRange(0f, 1f) <= num2;
						List<AxialI> list5 = null;
						if (flag && num4 < num3 && !spaceMapPOIPlacement.avoidClumping)
						{
							num4++;
							IEnumerable<AxialI> rings = AxialUtil.GetRings(AxialI.ZERO, spaceMapPOIPlacement.allowedRings.min, Mathf.Min(spaceMapPOIPlacement.allowedRings.max, this.numRings - 1));
							Func<AxialI, bool> func;
							if ((func = <>9__2) == null)
							{
								func = (<>9__2 = (AxialI location) => !assignedLocations.Contains(location) && poiClumpLocations.Contains(location) && !poiWorldAvoidance.Contains(location));
							}
							list5 = rings.Where(func).ToList<AxialI>();
						}
						if (list5 == null || list5.Count <= 0)
						{
							num4 = 0;
							poiClumpLocations.Clear();
							IEnumerable<AxialI> rings2 = AxialUtil.GetRings(AxialI.ZERO, spaceMapPOIPlacement.allowedRings.min, Mathf.Min(spaceMapPOIPlacement.allowedRings.max, this.numRings - 1));
							Func<AxialI, bool> func2;
							if ((func2 = <>9__3) == null)
							{
								func2 = (<>9__3 = (AxialI location) => !assignedLocations.Contains(location) && !poiWorldAvoidance.Contains(location) && !poiForbiddenLocations.Contains(location));
							}
							list5 = rings2.Where(func2).ToList<AxialI>();
						}
						if (list5 != null && list5.Count > 0)
						{
							AxialI axialI5 = list5[this.myRandom.RandomRange(0, list5.Count)];
							string text2 = list4[this.myRandom.RandomRange(0, list4.Count)];
							if (!spaceMapPOIPlacement.canSpawnDuplicates)
							{
								list4.Remove(text2);
							}
							this.poiPlacements[axialI5] = text2;
							poiForbiddenLocations.UnionWith(AxialUtil.GetRings(axialI5, 1, 3));
							poiClumpLocations.UnionWith(AxialUtil.GetRings(axialI5, 1, 1));
							assignedLocations.Add(axialI5);
						}
						else
						{
							global::Debug.LogWarning(string.Format("There is no room for a Space POI in ring range [{0}, {1}]", spaceMapPOIPlacement.allowedRings.min, spaceMapPOIPlacement.allowedRings.max));
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060063EB RID: 25579 RVA: 0x0025675C File Offset: 0x0025495C
		public void AbortGeneration()
		{
			if (this.thread != null && this.thread.IsAlive)
			{
				this.thread.Abort();
				this.thread = null;
			}
		}

		// Token: 0x060063EC RID: 25580 RVA: 0x00256785 File Offset: 0x00254985
		private void ThreadMain()
		{
			this.BeginGeneration();
		}

		// Token: 0x060063ED RID: 25581 RVA: 0x00256790 File Offset: 0x00254990
		private void Save()
		{
			try
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
					{
						try
						{
							Manager.Clear();
							ClusterLayoutSave clusterLayoutSave = new ClusterLayoutSave();
							clusterLayoutSave.version = new Vector2I(1, 1);
							clusterLayoutSave.size = this.size;
							clusterLayoutSave.ID = this.Id;
							clusterLayoutSave.numRings = this.numRings;
							clusterLayoutSave.poiLocations = this.poiLocations;
							clusterLayoutSave.poiPlacements = this.poiPlacements;
							for (int num = 0; num != this.worlds.Count; num++)
							{
								WorldGen worldGen = this.worlds[num];
								if (this.ShouldSkipWorldCallback == null || !this.ShouldSkipWorldCallback(num, worldGen))
								{
									clusterLayoutSave.worlds.Add(new ClusterLayoutSave.World
									{
										data = worldGen.data,
										stats = worldGen.stats,
										name = worldGen.Settings.world.filePath,
										isDiscovered = worldGen.isStartingWorld,
										traits = worldGen.Settings.GetWorldTraitIDs().ToList<string>(),
										storyTraits = worldGen.Settings.GetStoryTraitIDs().ToList<string>()
									});
									if (worldGen == this.currentWorld)
									{
										clusterLayoutSave.currentWorldIdx = num;
									}
								}
							}
							Serializer.Serialize(clusterLayoutSave, binaryWriter);
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

		// Token: 0x060063EE RID: 25582 RVA: 0x002569FC File Offset: 0x00254BFC
		public static Cluster Load()
		{
			Cluster cluster = new Cluster();
			try
			{
				FastReader fastReader = new FastReader(File.ReadAllBytes(WorldGen.WORLDGEN_SAVE_FILENAME));
				Manager.DeserializeDirectory(fastReader);
				int position = fastReader.Position;
				ClusterLayoutSave clusterLayoutSave = new ClusterLayoutSave();
				if (!Deserializer.Deserialize(clusterLayoutSave, fastReader))
				{
					fastReader.Position = position;
					WorldGen worldGen = WorldGen.Load(fastReader, true);
					cluster.worlds.Add(worldGen);
					cluster.size = worldGen.GetSize();
					cluster.currentWorld = cluster.worlds[0] ?? null;
				}
				else
				{
					for (int num = 0; num != clusterLayoutSave.worlds.Count; num++)
					{
						ClusterLayoutSave.World world = clusterLayoutSave.worlds[num];
						WorldGen worldGen2 = new WorldGen(world.name, world.data, world.stats, world.traits, world.storyTraits, false);
						cluster.worlds.Add(worldGen2);
						if (num == clusterLayoutSave.currentWorldIdx)
						{
							cluster.currentWorld = worldGen2;
							cluster.worlds[num].isStartingWorld = true;
						}
					}
					cluster.size = clusterLayoutSave.size;
					cluster.Id = clusterLayoutSave.ID;
					cluster.numRings = clusterLayoutSave.numRings;
					cluster.poiLocations = clusterLayoutSave.poiLocations;
					cluster.poiPlacements = clusterLayoutSave.poiPlacements;
				}
				DebugUtil.Assert(cluster.currentWorld != null);
				if (cluster.currentWorld == null)
				{
					DebugUtil.Assert(0 < cluster.worlds.Count);
					cluster.currentWorld = cluster.worlds[0];
				}
			}
			catch (Exception ex)
			{
				DebugUtil.LogErrorArgs(new object[] { "SolarSystem.Load Error!\n", ex.Message, ex.StackTrace });
				cluster = null;
			}
			return cluster;
		}

		// Token: 0x060063EF RID: 25583 RVA: 0x00256BD0 File Offset: 0x00254DD0
		public void LoadClusterLayoutSim(List<SimSaveFileStructure> loadedWorlds)
		{
			for (int num = 0; num != this.worlds.Count; num++)
			{
				SimSaveFileStructure simSaveFileStructure = new SimSaveFileStructure();
				try
				{
					FastReader fastReader = new FastReader(File.ReadAllBytes(WorldGen.GetSIMSaveFilename(num)));
					Manager.DeserializeDirectory(fastReader);
					Deserializer.Deserialize(simSaveFileStructure, fastReader);
				}
				catch (Exception ex)
				{
					if (!GenericGameSettings.instance.devAutoWorldGenActive)
					{
						DebugUtil.LogErrorArgs(new object[] { "LoadSim Error!\n", ex.Message, ex.StackTrace });
						break;
					}
				}
				if (simSaveFileStructure.worldDetail == null)
				{
					if (!GenericGameSettings.instance.devAutoWorldGenActive)
					{
						global::Debug.LogError("Detail is null for world " + num.ToString());
					}
				}
				else
				{
					loadedWorlds.Add(simSaveFileStructure);
				}
			}
		}

		// Token: 0x060063F0 RID: 25584 RVA: 0x00256C9C File Offset: 0x00254E9C
		public void SetIsRunningDebug(bool isDebug)
		{
			foreach (WorldGen worldGen in this.worlds)
			{
				worldGen.isRunningDebugGen = isDebug;
			}
		}

		// Token: 0x060063F1 RID: 25585 RVA: 0x00256CF0 File Offset: 0x00254EF0
		public void DEBUG_UpdateSeed(int seed)
		{
			this.seed = seed;
		}

		// Token: 0x04004524 RID: 17700
		public List<WorldGen> worlds = new List<WorldGen>();

		// Token: 0x04004525 RID: 17701
		public WorldGen currentWorld;

		// Token: 0x04004526 RID: 17702
		public Vector2I size;

		// Token: 0x04004527 RID: 17703
		public string Id;

		// Token: 0x04004528 RID: 17704
		public int numRings = 5;

		// Token: 0x04004529 RID: 17705
		private int seed;

		// Token: 0x0400452A RID: 17706
		private SeededRandom myRandom;

		// Token: 0x0400452B RID: 17707
		private bool doSimSettle = true;

		// Token: 0x0400452C RID: 17708
		public Action<int, WorldGen> PerWorldGenBeginCallback;

		// Token: 0x0400452D RID: 17709
		public Action<int, WorldGen, Sim.Cell[], Sim.DiseaseCell[]> PerWorldGenCompleteCallback;

		// Token: 0x0400452E RID: 17710
		public Func<int, WorldGen, bool> ShouldSkipWorldCallback;

		// Token: 0x0400452F RID: 17711
		public Dictionary<ClusterLayoutSave.POIType, List<AxialI>> poiLocations = new Dictionary<ClusterLayoutSave.POIType, List<AxialI>>();

		// Token: 0x04004530 RID: 17712
		public Dictionary<AxialI, string> poiPlacements = new Dictionary<AxialI, string>();

		// Token: 0x04004531 RID: 17713
		public List<WorldTrait> unplacedStoryTraits;

		// Token: 0x04004532 RID: 17714
		public List<string> chosenStoryTraitIds;

		// Token: 0x04004534 RID: 17716
		private Thread thread;
	}
}
