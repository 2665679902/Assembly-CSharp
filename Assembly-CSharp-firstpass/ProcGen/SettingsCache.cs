using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Klei;
using ObjectCloner;
using ProcGen.Noise;
using UnityEngine;

namespace ProcGen
{
	// Token: 0x020004D3 RID: 1235
	public static class SettingsCache
	{
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060034D3 RID: 13523 RVA: 0x00072FB5 File Offset: 0x000711B5
		// (set) Token: 0x060034D4 RID: 13524 RVA: 0x00072FBC File Offset: 0x000711BC
		public static LevelLayerSettings layers { get; private set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060034D5 RID: 13525 RVA: 0x00072FC4 File Offset: 0x000711C4
		// (set) Token: 0x060034D6 RID: 13526 RVA: 0x00072FCB File Offset: 0x000711CB
		public static ComposableDictionary<string, River> rivers { get; private set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x00072FD3 File Offset: 0x000711D3
		// (set) Token: 0x060034D8 RID: 13528 RVA: 0x00072FDA File Offset: 0x000711DA
		public static ComposableDictionary<string, Room> rooms { get; private set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060034D9 RID: 13529 RVA: 0x00072FE2 File Offset: 0x000711E2
		// (set) Token: 0x060034DA RID: 13530 RVA: 0x00072FE9 File Offset: 0x000711E9
		public static ComposableDictionary<Temperature.Range, Temperature> temperatures { get; private set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060034DB RID: 13531 RVA: 0x00072FF1 File Offset: 0x000711F1
		// (set) Token: 0x060034DC RID: 13532 RVA: 0x00072FF8 File Offset: 0x000711F8
		public static ComposableDictionary<string, List<WeightedSimHash>> borders { get; private set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x00073000 File Offset: 0x00071200
		// (set) Token: 0x060034DE RID: 13534 RVA: 0x00073007 File Offset: 0x00071207
		public static DefaultSettings defaults { get; set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x0007300F File Offset: 0x0007120F
		// (set) Token: 0x060034E0 RID: 13536 RVA: 0x00073016 File Offset: 0x00071216
		public static MobSettings mobs { get; private set; }

		// Token: 0x060034E1 RID: 13537 RVA: 0x00073020 File Offset: 0x00071220
		public static string GetAbsoluteContentPath(string dlcId, string optionalSubpath = "")
		{
			string text;
			if (!SettingsCache.s_cachedPaths.TryGetValue(dlcId, out text))
			{
				if (dlcId == "")
				{
					text = FileSystem.Normalize(Path.Combine(new string[] { Application.streamingAssetsPath }));
				}
				else
				{
					string contentDirectoryName = DlcManager.GetContentDirectoryName(dlcId);
					text = FileSystem.Normalize(Path.Combine(Application.streamingAssetsPath, "dlc", contentDirectoryName));
				}
				SettingsCache.s_cachedPaths[dlcId] = text;
			}
			return FileSystem.Normalize(Path.Combine(text, optionalSubpath));
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x0007309C File Offset: 0x0007129C
		public static string RewriteWorldgenPath(string scopePath)
		{
			string text;
			string text2;
			SettingsCache.GetDlcIdAndPath(scopePath, out text, out text2);
			return SettingsCache.GetAbsoluteContentPath(text, "worldgen/" + text2);
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x000730C4 File Offset: 0x000712C4
		public static string RewriteWorldgenPathYaml(string scopePath)
		{
			return SettingsCache.RewriteWorldgenPath(scopePath) + ".yaml";
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000730D6 File Offset: 0x000712D6
		public static string GetScope(string dlcId)
		{
			if (dlcId == "")
			{
				return "";
			}
			return DlcManager.GetContentDirectoryName(dlcId) + "::";
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000730FC File Offset: 0x000712FC
		public static void GetDlcIdAndPath(string scopePath, out string dlcId, out string path)
		{
			string[] array = scopePath.Split(SettingsCache.s_sourceDelimiter, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 1 && scopePath.EndsWith("::"))
			{
				dlcId = DlcManager.GetDlcIdFromContentDirectory(array[0]);
				path = "";
				return;
			}
			if (array.Length > 1)
			{
				dlcId = DlcManager.GetDlcIdFromContentDirectory(array[0]);
				path = array[1];
				return;
			}
			dlcId = "";
			path = scopePath;
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x0007315C File Offset: 0x0007135C
		public static string GuessScopedPath(string path)
		{
			foreach (string text in DlcManager.RELEASE_ORDER)
			{
				if (DlcManager.IsContentActive(text))
				{
					string absoluteContentPath = SettingsCache.GetAbsoluteContentPath(text, "worldgen/");
					if (path.StartsWith(absoluteContentPath))
					{
						return SettingsCache.GetScope(text) + path.Substring(absoluteContentPath.Length);
					}
				}
			}
			return null;
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x000731E4 File Offset: 0x000713E4
		public static void CloneInToNewWorld(MutatedWorldData worldData)
		{
			worldData.subworlds = SerializingCloner.Copy<Dictionary<string, SubWorld>>(SettingsCache.subworlds);
			worldData.features = SerializingCloner.Copy<Dictionary<string, FeatureSettings>>(SettingsCache.featureSettings);
			worldData.biomes = SerializingCloner.Copy<TerrainElementBandSettings>(SettingsCache.biomes);
			worldData.mobs = SerializingCloner.Copy<MobSettings>(SettingsCache.mobs);
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x00073234 File Offset: 0x00071434
		public static List<string> GetCachedFeatureNames()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, FeatureSettings> keyValuePair in SettingsCache.featureSettings)
			{
				list.Add(keyValuePair.Key);
			}
			return list;
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x00073294 File Offset: 0x00071494
		public static FeatureSettings GetCachedFeature(string name)
		{
			if (SettingsCache.featureSettings.ContainsKey(name))
			{
				return SettingsCache.featureSettings[name];
			}
			throw new Exception("Couldnt get feature from cache [" + name + "]");
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000732C4 File Offset: 0x000714C4
		public static List<string> GetCachedWorldTraitNames()
		{
			return new List<string>(SettingsCache.worldTraits.Keys);
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000732D5 File Offset: 0x000714D5
		public static List<WorldTrait> GetCachedWorldTraits()
		{
			return new List<WorldTrait>(SettingsCache.worldTraits.Values);
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x000732E8 File Offset: 0x000714E8
		public static WorldTrait GetCachedWorldTrait(string name, bool assertMissingTrait)
		{
			if (SettingsCache.worldTraits.ContainsKey(name))
			{
				return SettingsCache.worldTraits[name];
			}
			if (assertMissingTrait)
			{
				throw new Exception("Couldn't get trait [" + name + "]");
			}
			global::Debug.LogWarning("Couldn't get trait [" + name + "]");
			return null;
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x0007333D File Offset: 0x0007153D
		public static List<string> GetCachedStoryTraitNames()
		{
			return new List<string>(SettingsCache.storyTraits.Keys);
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x0007334E File Offset: 0x0007154E
		public static List<WorldTrait> GetCachedStoryTraits()
		{
			return new List<WorldTrait>(SettingsCache.storyTraits.Values);
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x0007335F File Offset: 0x0007155F
		public static Dictionary<string, WorldTrait> GetCachedStoryTraitsDictionary()
		{
			return SettingsCache.storyTraits;
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x00073368 File Offset: 0x00071568
		public static WorldTrait GetCachedStoryTrait(string name, bool assertMissingTrait)
		{
			if (SettingsCache.storyTraits.ContainsKey(name))
			{
				return SettingsCache.storyTraits[name];
			}
			if (assertMissingTrait)
			{
				throw new Exception("Couldn't get story trait [" + name + "]");
			}
			global::Debug.LogWarning("Couldn't get story trait [" + name + "]");
			return null;
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000733BD File Offset: 0x000715BD
		public static SubWorld GetCachedSubWorld(string name)
		{
			if (SettingsCache.subworlds.ContainsKey(name))
			{
				return SettingsCache.subworlds[name];
			}
			throw new Exception("Couldnt get subworld [" + name + "]");
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x000733F0 File Offset: 0x000715F0
		private static void SplitNameFromPath(string scopePath, out string path, out string name)
		{
			int num = scopePath.LastIndexOf('/');
			name = scopePath.Substring(num + 1);
			path = scopePath.Substring(0, num);
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x0007341C File Offset: 0x0007161C
		private static bool LoadBiome(string longName, List<YamlIO.Error> errors)
		{
			string text;
			string text2;
			SettingsCache.SplitNameFromPath(longName, out text, out text2);
			if (SettingsCache.biomeSettingsCache.ContainsKey(text))
			{
				return true;
			}
			string text3 = SettingsCache.RewriteWorldgenPathYaml(text);
			BiomeSettings biomeSettings = SettingsCache.MergeLoad<BiomeSettings>(null, text3, errors);
			if (biomeSettings == null)
			{
				global::Debug.LogWarning("WorldGen: Attempting to load biome: " + text2 + " failed");
				return false;
			}
			global::Debug.Assert(biomeSettings.TerrainBiomeLookupTable.Count > 0, "Worldgen: TerrainBiomeLookupTable is empty: " + longName);
			SettingsCache.biomeSettingsCache.Add(text, biomeSettings);
			foreach (KeyValuePair<string, ElementBandConfiguration> keyValuePair in biomeSettings.TerrainBiomeLookupTable)
			{
				string text4 = text + "/" + keyValuePair.Key;
				if (!SettingsCache.biomes.BiomeBackgroundElementBandConfigurations.ContainsKey(text4))
				{
					SettingsCache.biomes.BiomeBackgroundElementBandConfigurations.Add(text4, keyValuePair.Value);
				}
			}
			return true;
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x00073518 File Offset: 0x00071718
		private static string LoadFeature(string longName, List<YamlIO.Error> errors)
		{
			if (SettingsCache.featureSettings.ContainsKey(longName))
			{
				return longName;
			}
			FeatureSettings featureSettings = YamlIO.LoadFile<FeatureSettings>(SettingsCache.RewriteWorldgenPathYaml(longName), null, null);
			if (featureSettings != null)
			{
				SettingsCache.featureSettings.Add(longName, featureSettings);
				if (featureSettings.forceBiome != null)
				{
					DebugUtil.Assert(SettingsCache.LoadBiome(featureSettings.forceBiome, errors), longName, "(feature) referenced a missing biome named", featureSettings.forceBiome);
				}
			}
			else
			{
				global::Debug.LogWarning("WorldGen: Attempting to load feature: " + longName + " failed");
			}
			return longName;
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x00073590 File Offset: 0x00071790
		public static void LoadFeatures(Dictionary<string, int> features, List<YamlIO.Error> errors)
		{
			foreach (KeyValuePair<string, int> keyValuePair in features)
			{
				SettingsCache.LoadFeature(keyValuePair.Key, errors);
			}
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000735E8 File Offset: 0x000717E8
		public static void LoadSubworlds(List<WeightedSubworldName> subworlds, string prefix, List<YamlIO.Error> errors)
		{
			foreach (WeightedSubworldName weightedSubworldName in subworlds)
			{
				SubWorld subWorld = null;
				string text = weightedSubworldName.name;
				if (weightedSubworldName.overrideName != null && weightedSubworldName.overrideName.Length > 0)
				{
					text = weightedSubworldName.overrideName;
				}
				SubWorld subWorld2 = YamlIO.LoadFile<SubWorld>(SettingsCache.RewriteWorldgenPathYaml(text), null, null);
				if (subWorld2 != null)
				{
					subWorld = subWorld2;
					subWorld.name = text;
					subWorld.EnforceTemplateSpawnRuleSelfConsistency();
					SettingsCache.subworlds[text] = subWorld;
					SettingsCache.noise.LoadTree(subWorld.biomeNoise);
					SettingsCache.noise.LoadTree(subWorld.densityNoise);
					SettingsCache.noise.LoadTree(subWorld.overrideNoise);
				}
				else
				{
					global::Debug.LogWarning("WorldGen: Attempting to load subworld: " + text + " failed");
				}
				if (subWorld.centralFeature != null)
				{
					subWorld.centralFeature.type = SettingsCache.LoadFeature(subWorld.centralFeature.type, errors);
				}
				foreach (WeightedBiome weightedBiome in subWorld.biomes)
				{
					SettingsCache.LoadBiome(weightedBiome.name, errors);
					DebugUtil.Assert(SettingsCache.biomes.BiomeBackgroundElementBandConfigurations.ContainsKey(weightedBiome.name), subWorld.name, "(subworld) referenced a missing biome named", weightedBiome.name);
				}
				DebugUtil.Assert(subWorld.features != null, "Features list for subworld", subWorld.name, "was null! Either remove it from the .yaml or set it to the empty list []");
				foreach (Feature feature in subWorld.features)
				{
					feature.type = SettingsCache.LoadFeature(feature.type, errors);
				}
			}
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x00073800 File Offset: 0x00071A00
		public static void LoadWorldTraits(string path, string prefix, List<YamlIO.Error> errors)
		{
			List<FileHandle> list = new List<FileHandle>();
			FileSystem.GetFiles(FileSystem.Normalize(Path.Combine(path, "traits")), "*.yaml", list);
			list.Sort((FileHandle s1, FileHandle s2) => string.Compare(s1.full_path, s2.full_path, StringComparison.OrdinalIgnoreCase));
			foreach (FileHandle fileHandle in list)
			{
				SettingsCache.LoadTrait(fileHandle, path, prefix, SettingsCache.worldTraits, errors);
			}
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x0007389C File Offset: 0x00071A9C
		public static void LoadStoryTraits(string path, string prefix, List<YamlIO.Error> errors)
		{
			List<FileHandle> list = new List<FileHandle>();
			FileSystem.GetFiles(FileSystem.Normalize(Path.Combine(path, "storytraits")), "*.yaml", list);
			list.Sort((FileHandle s1, FileHandle s2) => string.Compare(s1.full_path, s2.full_path, StringComparison.OrdinalIgnoreCase));
			foreach (FileHandle fileHandle in list)
			{
				SettingsCache.LoadTrait(fileHandle, path, prefix, SettingsCache.storyTraits, errors);
			}
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x00073938 File Offset: 0x00071B38
		public static void LoadTrait(FileHandle file, string path, string prefix, Dictionary<string, WorldTrait> traitsDict, List<YamlIO.Error> errors)
		{
			WorldTrait worldTrait = YamlIO.LoadFile<WorldTrait>(file, delegate(YamlIO.Error error, bool force_log_as_warning)
			{
				errors.Add(error);
			}, null);
			if (worldTrait.forbiddenDLCIds != null)
			{
				using (List<string>.Enumerator enumerator = worldTrait.forbiddenDLCIds.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (DlcManager.IsContentActive(enumerator.Current))
						{
							return;
						}
					}
				}
			}
			int num = SettingsCache.FirstUncommonCharacter(path, file.full_path);
			string text = ((num > -1) ? file.full_path.Substring(num) : file.full_path);
			text = Path.Combine(Path.GetDirectoryName(text), Path.GetFileNameWithoutExtension(text));
			text = text.Replace('\\', '/');
			text = prefix + text;
			worldTrait.filePath = text;
			DebugUtil.DevAssert(!traitsDict.ContainsKey(text), "Overwriting trait " + text + " already exists", null);
			traitsDict[text] = worldTrait;
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x00073A34 File Offset: 0x00071C34
		public static List<string> GetWorldNames()
		{
			return SettingsCache.worlds.GetNames();
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x00073A40 File Offset: 0x00071C40
		public static List<string> GetClusterNames()
		{
			return SettingsCache.clusterLayouts.GetNames();
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x00073A4C File Offset: 0x00071C4C
		public static void Clear()
		{
			SettingsCache.worlds.worldCache.Clear();
			SettingsCache.layers = null;
			SettingsCache.biomes.BiomeBackgroundElementBandConfigurations.Clear();
			SettingsCache.biomeSettingsCache.Clear();
			SettingsCache.rivers = null;
			SettingsCache.rooms = null;
			SettingsCache.temperatures = null;
			SettingsCache.borders = null;
			SettingsCache.noise.Clear();
			SettingsCache.defaults = null;
			SettingsCache.mobs = null;
			SettingsCache.featureSettings.Clear();
			SettingsCache.worldTraits.Clear();
			SettingsCache.storyTraits.Clear();
			SettingsCache.subworlds.Clear();
			SettingsCache.clusterLayouts.clusterCache.Clear();
			DebugUtil.LogArgs(new object[] { "World Settings cleared!" });
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x00073B00 File Offset: 0x00071D00
		private static T MergeLoad<T>(T existing, string filename, List<YamlIO.Error> errors) where T : class, IMerge<T>, new()
		{
			ListPool<FileHandle, WorldGenSettings>.PooledList pooledList = ListPool<FileHandle, WorldGenSettings>.Allocate();
			FileSystem.GetFiles(filename, pooledList);
			if (pooledList.Count == 0)
			{
				pooledList.Recycle();
				if (existing != null)
				{
					return existing;
				}
				throw new Exception(string.Format("File not found in any file system: {0}", filename));
			}
			else
			{
				pooledList.Reverse();
				ListPool<T, WorldGenSettings>.PooledList pooledList2 = ListPool<T, WorldGenSettings>.Allocate();
				pooledList2.Add(new T());
				YamlIO.ErrorHandler <>9__0;
				foreach (FileHandle fileHandle in pooledList)
				{
					YamlIO.ErrorHandler errorHandler;
					if ((errorHandler = <>9__0) == null)
					{
						errorHandler = (<>9__0 = delegate(YamlIO.Error error, bool force_log_as_warning)
						{
							errors.Add(error);
						});
					}
					T t = YamlIO.LoadFile<T>(fileHandle, errorHandler, null);
					if (t != null)
					{
						pooledList2.Add(t);
					}
				}
				pooledList.Recycle();
				T t2 = pooledList2[0];
				for (int num = 1; num != pooledList2.Count; num++)
				{
					t2.Merge(pooledList2[num]);
				}
				pooledList2.Recycle();
				if (existing != null)
				{
					return existing.Merge(t2);
				}
				return t2;
			}
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x00073C30 File Offset: 0x00071E30
		private static int FirstUncommonCharacter(string a, string b)
		{
			int num = Mathf.Min(a.Length, b.Length);
			int num2 = -1;
			while (++num2 < num)
			{
				if (a[num2] != b[num2])
				{
					return num2;
				}
			}
			return num2;
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x00073C70 File Offset: 0x00071E70
		public static bool LoadFiles(List<YamlIO.Error> errors)
		{
			if (SettingsCache.worlds.worldCache.Count > 0)
			{
				return false;
			}
			SettingsCache.defaults = YamlIO.LoadFile<DefaultSettings>(SettingsCache.GetAbsoluteContentPath("", "worldgen/") + "defaults.yaml", null, null);
			foreach (string text in DlcManager.RELEASE_ORDER)
			{
				if (DlcManager.IsContentActive(text))
				{
					SettingsCache.LoadFiles(SettingsCache.GetAbsoluteContentPath(text, "worldgen/"), SettingsCache.GetScope(text), errors);
				}
			}
			SettingsCache.worlds.Validate();
			DebugUtil.LogArgs(new object[] { "World settings reload complete!" });
			return true;
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x00073D34 File Offset: 0x00071F34
		private static bool LoadFiles(string worldgenFolderPath, string addPrefix, List<YamlIO.Error> errors)
		{
			SettingsCache.clusterLayouts.LoadFiles(worldgenFolderPath, addPrefix, errors);
			HashSet<string> hashSet = new HashSet<string>(from worldPlacment in SettingsCache.clusterLayouts.clusterCache.Values.SelectMany((ClusterLayout clusterLayout) => clusterLayout.worldPlacements)
				select worldPlacment.world);
			SettingsCache.worlds.LoadReferencedWorlds(worldgenFolderPath, addPrefix, hashSet, errors);
			SettingsCache.LoadWorldTraits(worldgenFolderPath, addPrefix, errors);
			SettingsCache.LoadStoryTraits(worldgenFolderPath, addPrefix, errors);
			foreach (KeyValuePair<string, World> keyValuePair in SettingsCache.worlds.worldCache)
			{
				SettingsCache.LoadFeatures(keyValuePair.Value.globalFeatures, errors);
				SettingsCache.LoadSubworlds(keyValuePair.Value.subworldFiles, addPrefix, errors);
			}
			foreach (KeyValuePair<string, WorldTrait> keyValuePair2 in SettingsCache.worldTraits)
			{
				SettingsCache.LoadFeatures(keyValuePair2.Value.globalFeatureMods, errors);
				SettingsCache.LoadSubworlds(keyValuePair2.Value.additionalSubworldFiles, addPrefix, errors);
			}
			foreach (KeyValuePair<string, WorldTrait> keyValuePair3 in SettingsCache.storyTraits)
			{
				SettingsCache.LoadFeatures(keyValuePair3.Value.globalFeatureMods, errors);
				SettingsCache.LoadSubworlds(keyValuePair3.Value.additionalSubworldFiles, addPrefix, errors);
			}
			SettingsCache.layers = SettingsCache.MergeLoad<LevelLayerSettings>(SettingsCache.layers, worldgenFolderPath + "layers.yaml", errors);
			SettingsCache.layers.LevelLayers.ConvertBandSizeToMaxSize();
			SettingsCache.rivers = SettingsCache.MergeLoad<ComposableDictionary<string, River>>(SettingsCache.rivers, worldgenFolderPath + "rivers.yaml", errors);
			SettingsCache.rooms = SettingsCache.MergeLoad<ComposableDictionary<string, Room>>(SettingsCache.rooms, worldgenFolderPath + "rooms.yaml", errors);
			foreach (KeyValuePair<string, Room> keyValuePair4 in SettingsCache.rooms)
			{
				keyValuePair4.Value.name = keyValuePair4.Key;
			}
			SettingsCache.temperatures = SettingsCache.MergeLoad<ComposableDictionary<Temperature.Range, Temperature>>(SettingsCache.temperatures, worldgenFolderPath + "temperatures.yaml", errors);
			SettingsCache.borders = SettingsCache.MergeLoad<ComposableDictionary<string, List<WeightedSimHash>>>(SettingsCache.borders, worldgenFolderPath + "borders.yaml", errors);
			SettingsCache.mobs = SettingsCache.MergeLoad<MobSettings>(SettingsCache.mobs, worldgenFolderPath + "mobs.yaml", errors);
			foreach (KeyValuePair<string, Mob> keyValuePair5 in SettingsCache.mobs.MobLookupTable)
			{
				keyValuePair5.Value.name = keyValuePair5.Key;
			}
			return true;
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x00074044 File Offset: 0x00072244
		public static List<string> GetRandomTraits(int seed, World world)
		{
			if (world.disableWorldTraits || world.worldTraitRules == null || seed == 0)
			{
				return new List<string>();
			}
			KRandom krandom = new KRandom(seed);
			List<WorldTrait> list = new List<WorldTrait>(SettingsCache.worldTraits.Values);
			List<WorldTrait> list2 = new List<WorldTrait>();
			TagSet tagSet = new TagSet();
			using (List<World.TraitRule>.Enumerator enumerator = world.worldTraitRules.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					World.TraitRule rule = enumerator.Current;
					if (rule.specificTraits != null)
					{
						foreach (string text in rule.specificTraits)
						{
							WorldTrait worldTrait;
							if (SettingsCache.worldTraits.TryGetValue(text, out worldTrait))
							{
								list2.Add(SettingsCache.worldTraits[text]);
							}
							else
							{
								DebugUtil.DevLogError("World traits " + text + " doesn't exist, skipping.");
							}
						}
					}
					List<WorldTrait> list3 = new List<WorldTrait>(list);
					TagSet requiredTags = ((rule.requiredTags != null) ? new TagSet(rule.requiredTags) : null);
					TagSet forbiddenTags = ((rule.forbiddenTags != null) ? new TagSet(rule.forbiddenTags) : null);
					list3.RemoveAll((WorldTrait trait) => (requiredTags != null && !trait.traitTagsSet.ContainsAll(requiredTags)) || (forbiddenTags != null && trait.traitTagsSet.ContainsOne(forbiddenTags)) || (rule.forbiddenTraits != null && rule.forbiddenTraits.Contains(trait.filePath)) || !trait.IsValid(world, true));
					int num = krandom.Next(rule.min, Mathf.Max(rule.min, rule.max + 1));
					int count = list2.Count;
					while (list2.Count < count + num && list3.Count > 0)
					{
						int num2 = krandom.Next(list3.Count);
						WorldTrait worldTrait2 = list3[num2];
						bool flag = false;
						using (List<string>.Enumerator enumerator2 = worldTrait2.exclusiveWith.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								string exclusiveId = enumerator2.Current;
								if (list2.Find((WorldTrait t) => t.filePath == exclusiveId) != null)
								{
									flag = true;
									break;
								}
							}
						}
						foreach (string text2 in worldTrait2.exclusiveWithTags)
						{
							if (tagSet.Contains(text2))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							list2.Add(worldTrait2);
							list.Remove(worldTrait2);
							foreach (string text3 in worldTrait2.exclusiveWithTags)
							{
								tagSet.Add(text3);
							}
						}
						list3.RemoveAt(num2);
					}
					if (list2.Count != count + num)
					{
						global::Debug.LogWarning(string.Format("TraitRule on {0} tried to generate {1} but only generated {2}", world.name, num, list2.Count - count));
					}
				}
			}
			List<string> list4 = new List<string>();
			foreach (WorldTrait worldTrait3 in list2)
			{
				list4.Add(worldTrait3.filePath);
			}
			return list4;
		}

		// Token: 0x0400128C RID: 4748
		public static ClusterLayouts clusterLayouts = new ClusterLayouts();

		// Token: 0x0400128D RID: 4749
		public static Worlds worlds = new Worlds();

		// Token: 0x0400128E RID: 4750
		public static Dictionary<string, SubWorld> subworlds = new Dictionary<string, SubWorld>();

		// Token: 0x0400128F RID: 4751
		public static TerrainElementBandSettings biomes = new TerrainElementBandSettings();

		// Token: 0x04001290 RID: 4752
		public static NoiseTreeFiles noise = new NoiseTreeFiles();

		// Token: 0x04001298 RID: 4760
		private static Dictionary<string, FeatureSettings> featureSettings = new Dictionary<string, FeatureSettings>();

		// Token: 0x04001299 RID: 4761
		private static Dictionary<string, WorldTrait> worldTraits = new Dictionary<string, WorldTrait>();

		// Token: 0x0400129A RID: 4762
		private static Dictionary<string, WorldTrait> storyTraits = new Dictionary<string, WorldTrait>();

		// Token: 0x0400129B RID: 4763
		private static Dictionary<string, BiomeSettings> biomeSettingsCache = new Dictionary<string, BiomeSettings>();

		// Token: 0x0400129C RID: 4764
		private static string[] s_sourceDelimiter = new string[] { "::" };

		// Token: 0x0400129D RID: 4765
		private static Dictionary<string, string> s_cachedPaths = new Dictionary<string, string>();

		// Token: 0x0400129E RID: 4766
		private const string LAYERS_FILE = "layers";

		// Token: 0x0400129F RID: 4767
		private const string RIVERS_FILE = "rivers";

		// Token: 0x040012A0 RID: 4768
		private const string ROOMS_FILE = "rooms";

		// Token: 0x040012A1 RID: 4769
		private const string TEMPERATURES_FILE = "temperatures";

		// Token: 0x040012A2 RID: 4770
		private const string BORDERS_FILE = "borders";

		// Token: 0x040012A3 RID: 4771
		private const string DEFAULTS_FILE = "defaults";

		// Token: 0x040012A4 RID: 4772
		private const string MOBS_FILE = "mobs";

		// Token: 0x040012A5 RID: 4773
		private const string WORLD_TRAITS_PATH = "traits";

		// Token: 0x040012A6 RID: 4774
		private const string STORY_TRAITS_PATH = "storytraits";
	}
}
