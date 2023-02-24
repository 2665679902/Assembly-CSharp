using System;
using System.Collections.Generic;

namespace ProcGen
{
	// Token: 0x020004D4 RID: 1236
	public class WorldGenSettings
	{
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06003503 RID: 13571 RVA: 0x000744F4 File Offset: 0x000726F4
		public World world
		{
			get
			{
				return this.mutatedWorldData.world;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06003504 RID: 13572 RVA: 0x00074501 File Offset: 0x00072701
		public static string ClusterDefaultName
		{
			get
			{
				if (!DlcManager.FeatureClusterSpaceEnabled())
				{
					return "clusters/SandstoneDefault";
				}
				return "expansion1::clusters/SandstoneStartCluster";
			}
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x00074518 File Offset: 0x00072718
		public WorldGenSettings(string worldName, List<string> worldTraits, List<string> storyTraits, bool assertMissingTraits)
		{
			DebugUtil.Assert(SettingsCache.worlds.HasWorld(worldName), "Failed to load world " + worldName);
			World worldData = SettingsCache.worlds.GetWorldData(worldName);
			List<WorldTrait> list = new List<WorldTrait>();
			if (worldTraits != null)
			{
				foreach (string text in worldTraits)
				{
					WorldTrait cachedWorldTrait = SettingsCache.GetCachedWorldTrait(text, assertMissingTraits);
					if (cachedWorldTrait != null)
					{
						list.Add(cachedWorldTrait);
					}
				}
			}
			List<WorldTrait> list2 = new List<WorldTrait>();
			if (storyTraits != null)
			{
				foreach (string text2 in storyTraits)
				{
					WorldTrait cachedStoryTrait = SettingsCache.GetCachedStoryTrait(text2, assertMissingTraits);
					if (cachedStoryTrait != null)
					{
						list2.Add(cachedStoryTrait);
					}
				}
			}
			this.mutatedWorldData = new MutatedWorldData(worldData, list, list2);
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x0007460C File Offset: 0x0007280C
		public BaseLocation GetBaseLocation()
		{
			if (this.world != null && this.world.defaultsOverrides != null && this.world.defaultsOverrides.baseData != null)
			{
				DebugUtil.LogArgs(new object[] { string.Format("World '{0}' is overriding baseData", this.world.name) });
				return this.world.defaultsOverrides.baseData;
			}
			return SettingsCache.defaults.baseData;
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x00074680 File Offset: 0x00072880
		public List<string> GetOverworldAddTags()
		{
			if (this.world != null && this.world.defaultsOverrides != null && this.world.defaultsOverrides.overworldAddTags != null)
			{
				DebugUtil.LogArgs(new object[] { string.Format("World '{0}' is overriding overworldAddTags", this.world.name) });
				return this.world.defaultsOverrides.overworldAddTags;
			}
			return SettingsCache.defaults.overworldAddTags;
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000746F4 File Offset: 0x000728F4
		public List<string> GetDefaultMoveTags()
		{
			if (this.world != null && this.world.defaultsOverrides != null && this.world.defaultsOverrides.defaultMoveTags != null)
			{
				DebugUtil.LogArgs(new object[] { string.Format("World '{0}' is overriding defaultMoveTags", this.world.name) });
				return this.world.defaultsOverrides.defaultMoveTags;
			}
			return SettingsCache.defaults.defaultMoveTags;
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x00074768 File Offset: 0x00072968
		public List<StartingWorldElementSetting> GetDefaultStartingElements()
		{
			if (this.world != null && this.world.defaultsOverrides != null && this.world.defaultsOverrides.startingWorldElements != null)
			{
				DebugUtil.LogArgs(new object[] { string.Format("World '{0}' is overriding startingWorldElements", this.world.name) });
				return this.world.defaultsOverrides.startingWorldElements;
			}
			return SettingsCache.defaults.startingWorldElements;
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000747DC File Offset: 0x000729DC
		public string[] GetWorldTraitIDs()
		{
			if (this.mutatedWorldData.worldTraits != null && this.mutatedWorldData.worldTraits.Count > 0)
			{
				string[] array = new string[this.mutatedWorldData.worldTraits.Count];
				for (int i = 0; i < this.mutatedWorldData.worldTraits.Count; i++)
				{
					array[i] = this.mutatedWorldData.worldTraits[i].filePath;
				}
				return array;
			}
			return Array.Empty<string>();
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x0007485A File Offset: 0x00072A5A
		public void SetStoryTraitCandidates(List<WorldTrait> storyTraits)
		{
			this.mutatedWorldData.storyTraitCandidates = storyTraits;
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x00074868 File Offset: 0x00072A68
		public List<WorldTrait> GetStoryTraitCandiates()
		{
			return this.mutatedWorldData.storyTraitCandidates;
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x00074878 File Offset: 0x00072A78
		public string[] GetStoryTraitCandidateIds()
		{
			if (this.mutatedWorldData.storyTraitCandidates != null && this.mutatedWorldData.storyTraitCandidates.Count > 0)
			{
				string[] array = new string[this.mutatedWorldData.storyTraitCandidates.Count];
				for (int i = 0; i < this.mutatedWorldData.storyTraitCandidates.Count; i++)
				{
					array[i] = this.mutatedWorldData.storyTraitCandidates[i].filePath;
				}
				return array;
			}
			return Array.Empty<string>();
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x000748F6 File Offset: 0x00072AF6
		public void ApplyStoryTrait(WorldTrait storyTrait)
		{
			this.mutatedWorldData.storyTraits.Add(storyTrait);
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x0007490C File Offset: 0x00072B0C
		public string[] GetStoryTraitIDs()
		{
			if (this.mutatedWorldData.storyTraits != null && this.mutatedWorldData.storyTraits.Count > 0)
			{
				string[] array = new string[this.mutatedWorldData.storyTraits.Count];
				for (int i = 0; i < this.mutatedWorldData.storyTraits.Count; i++)
				{
					array[i] = this.mutatedWorldData.storyTraits[i].filePath;
				}
				return array;
			}
			return Array.Empty<string>();
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x0007498C File Offset: 0x00072B8C
		private bool GetSetting<T>(DefaultSettings set, string target, WorldGenSettings.ParserFn<T> parser, out T res)
		{
			if (set == null || set.data == null || !set.data.ContainsKey(target))
			{
				res = default(T);
				return false;
			}
			object obj = set.data[target];
			if (obj.GetType() == typeof(T))
			{
				res = (T)((object)obj);
				return true;
			}
			bool flag = parser(obj as string, out res);
			if (flag)
			{
				set.data[target] = res;
			}
			return flag;
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x00074A18 File Offset: 0x00072C18
		private T GetSetting<T>(string target, WorldGenSettings.ParserFn<T> parser)
		{
			T t;
			if (this.world != null)
			{
				if (!this.GetSetting<T>(this.world.defaultsOverrides, target, parser, out t))
				{
					this.GetSetting<T>(SettingsCache.defaults, target, parser, out t);
				}
			}
			else if (!this.GetSetting<T>(SettingsCache.defaults, target, parser, out t))
			{
				DebugUtil.LogWarningArgs(new object[] { string.Format("Couldn't find setting '{0}' in default settings!", target) });
			}
			return t;
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x00074A81 File Offset: 0x00072C81
		public bool GetBoolSetting(string target)
		{
			return this.GetSetting<bool>(target, new WorldGenSettings.ParserFn<bool>(bool.TryParse));
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x00074A96 File Offset: 0x00072C96
		private bool TryParseString(string input, out string res)
		{
			res = input;
			return true;
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x00074A9C File Offset: 0x00072C9C
		public string GetStringSetting(string target)
		{
			return this.GetSetting<string>(target, new WorldGenSettings.ParserFn<string>(this.TryParseString));
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x00074AB1 File Offset: 0x00072CB1
		public float GetFloatSetting(string target)
		{
			return this.GetSetting<float>(target, new WorldGenSettings.ParserFn<float>(float.TryParse));
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x00074AC6 File Offset: 0x00072CC6
		public int GetIntSetting(string target)
		{
			return this.GetSetting<int>(target, new WorldGenSettings.ParserFn<int>(int.TryParse));
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x00074ADB File Offset: 0x00072CDB
		public E GetEnumSetting<E>(string target) where E : struct
		{
			return this.GetSetting<E>(target, new WorldGenSettings.ParserFn<E>(WorldGenSettings.TryParseEnum<E>));
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x00074AF0 File Offset: 0x00072CF0
		private static bool TryParseEnum<E>(string value, out E result) where E : struct
		{
			try
			{
				result = (E)((object)Enum.Parse(typeof(E), value));
				return true;
			}
			catch (Exception)
			{
				result = new E();
			}
			return false;
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x00074B40 File Offset: 0x00072D40
		public bool HasFeature(string name)
		{
			return this.mutatedWorldData.features.ContainsKey(name);
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x00074B53 File Offset: 0x00072D53
		public FeatureSettings GetFeature(string name)
		{
			if (this.mutatedWorldData.features.ContainsKey(name))
			{
				return this.mutatedWorldData.features[name];
			}
			throw new Exception("Couldnt get feature from active world data [" + name + "]");
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x00074B90 File Offset: 0x00072D90
		public FeatureSettings TryGetFeature(string name)
		{
			FeatureSettings featureSettings;
			this.mutatedWorldData.features.TryGetValue(name, out featureSettings);
			return featureSettings;
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x00074BB2 File Offset: 0x00072DB2
		public bool HasSubworld(string name)
		{
			return this.mutatedWorldData.subworlds.ContainsKey(name);
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x00074BC5 File Offset: 0x00072DC5
		public SubWorld GetSubWorld(string name)
		{
			if (this.mutatedWorldData.subworlds.ContainsKey(name))
			{
				return this.mutatedWorldData.subworlds[name];
			}
			throw new Exception("Couldnt get subworld from active world data [" + name + "]");
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x00074C04 File Offset: 0x00072E04
		public SubWorld TryGetSubWorld(string name)
		{
			SubWorld subWorld;
			this.mutatedWorldData.subworlds.TryGetValue(name, out subWorld);
			return subWorld;
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x00074C28 File Offset: 0x00072E28
		public List<WeightedSubWorld> GetSubworldsForWorld(List<WeightedSubworldName> subworldList)
		{
			List<WeightedSubWorld> list = new List<WeightedSubWorld>();
			foreach (KeyValuePair<string, SubWorld> keyValuePair in this.mutatedWorldData.subworlds)
			{
				foreach (WeightedSubworldName weightedSubworldName in subworldList)
				{
					if (keyValuePair.Key == weightedSubworldName.name)
					{
						list.Add(new WeightedSubWorld(weightedSubworldName.weight, keyValuePair.Value, weightedSubworldName.overridePower, weightedSubworldName.minCount, weightedSubworldName.maxCount, weightedSubworldName.priority));
					}
				}
			}
			return list;
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x00074D04 File Offset: 0x00072F04
		public bool HasMob(string id)
		{
			return this.mutatedWorldData.mobs.HasMob(id);
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x00074D17 File Offset: 0x00072F17
		public Mob GetMob(string id)
		{
			return this.mutatedWorldData.mobs.GetMob(id);
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x00074D2C File Offset: 0x00072F2C
		public ElementBandConfiguration GetElementBandForBiome(string name)
		{
			ElementBandConfiguration elementBandConfiguration;
			if (this.mutatedWorldData.biomes.BiomeBackgroundElementBandConfigurations.TryGetValue(name, out elementBandConfiguration))
			{
				return elementBandConfiguration;
			}
			return null;
		}

		// Token: 0x040012A7 RID: 4775
		private MutatedWorldData mutatedWorldData;

		// Token: 0x02000B01 RID: 2817
		// (Invoke) Token: 0x06005803 RID: 22531
		private delegate bool ParserFn<T>(string input, out T res);
	}
}
