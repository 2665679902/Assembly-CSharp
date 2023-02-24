using System;
using System.Collections.Generic;
using ProcGen;
using STRINGS;
using UnityEngine;

// Token: 0x02000A73 RID: 2675
public class ColonyDestinationAsteroidBeltData
{
	// Token: 0x17000618 RID: 1560
	// (get) Token: 0x060051C4 RID: 20932 RVA: 0x001D8875 File Offset: 0x001D6A75
	// (set) Token: 0x060051C5 RID: 20933 RVA: 0x001D887D File Offset: 0x001D6A7D
	public float TargetScale { get; set; }

	// Token: 0x17000619 RID: 1561
	// (get) Token: 0x060051C6 RID: 20934 RVA: 0x001D8886 File Offset: 0x001D6A86
	// (set) Token: 0x060051C7 RID: 20935 RVA: 0x001D888E File Offset: 0x001D6A8E
	public float Scale { get; set; }

	// Token: 0x1700061A RID: 1562
	// (get) Token: 0x060051C8 RID: 20936 RVA: 0x001D8897 File Offset: 0x001D6A97
	// (set) Token: 0x060051C9 RID: 20937 RVA: 0x001D889F File Offset: 0x001D6A9F
	public int seed { get; private set; }

	// Token: 0x1700061B RID: 1563
	// (get) Token: 0x060051CA RID: 20938 RVA: 0x001D88A8 File Offset: 0x001D6AA8
	public string startWorldPath
	{
		get
		{
			return this.startWorld.filePath;
		}
	}

	// Token: 0x1700061C RID: 1564
	// (get) Token: 0x060051CB RID: 20939 RVA: 0x001D88B5 File Offset: 0x001D6AB5
	// (set) Token: 0x060051CC RID: 20940 RVA: 0x001D88BD File Offset: 0x001D6ABD
	public Sprite sprite { get; private set; }

	// Token: 0x1700061D RID: 1565
	// (get) Token: 0x060051CD RID: 20941 RVA: 0x001D88C6 File Offset: 0x001D6AC6
	// (set) Token: 0x060051CE RID: 20942 RVA: 0x001D88CE File Offset: 0x001D6ACE
	public int difficulty { get; private set; }

	// Token: 0x1700061E RID: 1566
	// (get) Token: 0x060051CF RID: 20943 RVA: 0x001D88D7 File Offset: 0x001D6AD7
	public string startWorldName
	{
		get
		{
			return Strings.Get(this.startWorld.name);
		}
	}

	// Token: 0x1700061F RID: 1567
	// (get) Token: 0x060051D0 RID: 20944 RVA: 0x001D88EE File Offset: 0x001D6AEE
	public string properName
	{
		get
		{
			if (this.cluster == null)
			{
				return "";
			}
			return this.cluster.name;
		}
	}

	// Token: 0x17000620 RID: 1568
	// (get) Token: 0x060051D1 RID: 20945 RVA: 0x001D8909 File Offset: 0x001D6B09
	public string beltPath
	{
		get
		{
			if (this.cluster == null)
			{
				return WorldGenSettings.ClusterDefaultName;
			}
			return this.cluster.filePath;
		}
	}

	// Token: 0x17000621 RID: 1569
	// (get) Token: 0x060051D2 RID: 20946 RVA: 0x001D8924 File Offset: 0x001D6B24
	// (set) Token: 0x060051D3 RID: 20947 RVA: 0x001D892C File Offset: 0x001D6B2C
	public List<ProcGen.World> worlds { get; private set; }

	// Token: 0x17000622 RID: 1570
	// (get) Token: 0x060051D4 RID: 20948 RVA: 0x001D8935 File Offset: 0x001D6B35
	public ClusterLayout Layout
	{
		get
		{
			return this.cluster;
		}
	}

	// Token: 0x17000623 RID: 1571
	// (get) Token: 0x060051D5 RID: 20949 RVA: 0x001D893D File Offset: 0x001D6B3D
	public ProcGen.World GetStartWorld
	{
		get
		{
			return this.startWorld;
		}
	}

	// Token: 0x060051D6 RID: 20950 RVA: 0x001D8948 File Offset: 0x001D6B48
	public ColonyDestinationAsteroidBeltData(string staringWorldName, int seed, string clusterPath)
	{
		this.startWorld = SettingsCache.worlds.GetWorldData(staringWorldName);
		this.Scale = (this.TargetScale = this.startWorld.iconScale);
		this.worlds = new List<ProcGen.World>();
		if (clusterPath != null)
		{
			this.cluster = SettingsCache.clusterLayouts.GetClusterData(clusterPath);
			for (int i = 0; i < this.cluster.worldPlacements.Count; i++)
			{
				if (i != this.cluster.startWorldIndex)
				{
					this.worlds.Add(SettingsCache.worlds.GetWorldData(this.cluster.worldPlacements[i].world));
				}
			}
		}
		this.ReInitialize(seed);
	}

	// Token: 0x060051D7 RID: 20951 RVA: 0x001D8A18 File Offset: 0x001D6C18
	public static Sprite GetUISprite(string filename)
	{
		if (filename.IsNullOrWhiteSpace())
		{
			filename = (DlcManager.FeatureClusterSpaceEnabled() ? "asteroid_sandstone_start_kanim" : "Asteroid_sandstone");
		}
		KAnimFile kanimFile;
		Assets.TryGetAnim(filename, out kanimFile);
		if (kanimFile != null)
		{
			return Def.GetUISpriteFromMultiObjectAnim(kanimFile, "ui", false, "");
		}
		return Assets.GetSprite(filename);
	}

	// Token: 0x060051D8 RID: 20952 RVA: 0x001D8A78 File Offset: 0x001D6C78
	public void ReInitialize(int seed)
	{
		this.seed = seed;
		this.paramDescriptors.Clear();
		this.traitDescriptors.Clear();
		this.sprite = ColonyDestinationAsteroidBeltData.GetUISprite(this.startWorld.asteroidIcon);
		this.difficulty = this.cluster.difficulty;
	}

	// Token: 0x060051D9 RID: 20953 RVA: 0x001D8AC9 File Offset: 0x001D6CC9
	public List<AsteroidDescriptor> GetParamDescriptors()
	{
		if (this.paramDescriptors.Count == 0)
		{
			this.paramDescriptors = this.GenerateParamDescriptors();
		}
		return this.paramDescriptors;
	}

	// Token: 0x060051DA RID: 20954 RVA: 0x001D8AEA File Offset: 0x001D6CEA
	public List<AsteroidDescriptor> GetTraitDescriptors()
	{
		if (this.traitDescriptors.Count == 0)
		{
			this.traitDescriptors = this.GenerateTraitDescriptors();
		}
		return this.traitDescriptors;
	}

	// Token: 0x060051DB RID: 20955 RVA: 0x001D8B0C File Offset: 0x001D6D0C
	private List<AsteroidDescriptor> GenerateParamDescriptors()
	{
		List<AsteroidDescriptor> list = new List<AsteroidDescriptor>();
		if (this.cluster != null && DlcManager.FeatureClusterSpaceEnabled())
		{
			list.Add(new AsteroidDescriptor(string.Format(WORLDS.SURVIVAL_CHANCE.CLUSTERNAME, Strings.Get(this.cluster.name)), Strings.Get(this.cluster.description), Color.white, null, null));
		}
		list.Add(new AsteroidDescriptor(string.Format(WORLDS.SURVIVAL_CHANCE.PLANETNAME, this.startWorldName), null, Color.white, null, null));
		list.Add(new AsteroidDescriptor(Strings.Get(this.startWorld.description), null, Color.white, null, null));
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			list.Add(new AsteroidDescriptor(string.Format(WORLDS.SURVIVAL_CHANCE.MOONNAMES, Array.Empty<object>()), null, Color.white, null, null));
			foreach (ProcGen.World world in this.worlds)
			{
				list.Add(new AsteroidDescriptor(string.Format("{0}", Strings.Get(world.name)), Strings.Get(world.description), Color.white, null, null));
			}
		}
		int num = Mathf.Clamp(this.difficulty, 0, ColonyDestinationAsteroidBeltData.survivalOptions.Count - 1);
		global::Tuple<string, string, string> tuple = ColonyDestinationAsteroidBeltData.survivalOptions[num];
		list.Add(new AsteroidDescriptor(string.Format(WORLDS.SURVIVAL_CHANCE.TITLE, tuple.first, tuple.third), null, Color.white, null, null));
		return list;
	}

	// Token: 0x060051DC RID: 20956 RVA: 0x001D8CC4 File Offset: 0x001D6EC4
	private List<AsteroidDescriptor> GenerateTraitDescriptors()
	{
		List<AsteroidDescriptor> list = new List<AsteroidDescriptor>();
		List<ProcGen.World> list2 = new List<ProcGen.World>();
		list2.Add(this.startWorld);
		list2.AddRange(this.worlds);
		for (int i = 0; i < list2.Count; i++)
		{
			ProcGen.World world = list2[i];
			if (DlcManager.IsExpansion1Active())
			{
				list.Add(new AsteroidDescriptor("", null, Color.white, null, null));
				list.Add(new AsteroidDescriptor(string.Format("<b>{0}</b>", Strings.Get(world.name)), null, Color.white, null, null));
			}
			List<WorldTrait> worldTraits = this.GetWorldTraits(world);
			foreach (WorldTrait worldTrait in worldTraits)
			{
				string text = worldTrait.filePath.Substring(worldTrait.filePath.LastIndexOf("/") + 1);
				list.Add(new AsteroidDescriptor(string.Format("<color=#{1}>{0}</color>", Strings.Get(worldTrait.name), worldTrait.colorHex), Strings.Get(worldTrait.description), global::Util.ColorFromHex(worldTrait.colorHex), null, text));
			}
			if (worldTraits.Count == 0)
			{
				list.Add(new AsteroidDescriptor(WORLD_TRAITS.NO_TRAITS.NAME, WORLD_TRAITS.NO_TRAITS.DESCRIPTION, Color.white, null, "NoTraits"));
			}
		}
		return list;
	}

	// Token: 0x060051DD RID: 20957 RVA: 0x001D8E40 File Offset: 0x001D7040
	public List<AsteroidDescriptor> GenerateTraitDescriptors(ProcGen.World singleWorld, bool includeDefaultTrait = true)
	{
		List<AsteroidDescriptor> list = new List<AsteroidDescriptor>();
		List<ProcGen.World> list2 = new List<ProcGen.World>();
		list2.Add(this.startWorld);
		list2.AddRange(this.worlds);
		for (int i = 0; i < list2.Count; i++)
		{
			if (list2[i] == singleWorld)
			{
				ProcGen.World world = list2[i];
				List<WorldTrait> worldTraits = this.GetWorldTraits(world);
				foreach (WorldTrait worldTrait in worldTraits)
				{
					string text = worldTrait.filePath.Substring(worldTrait.filePath.LastIndexOf("/") + 1);
					list.Add(new AsteroidDescriptor(string.Format("<color=#{1}>{0}</color>", Strings.Get(worldTrait.name), worldTrait.colorHex), Strings.Get(worldTrait.description), global::Util.ColorFromHex(worldTrait.colorHex), null, text));
				}
				if (worldTraits.Count == 0 && includeDefaultTrait)
				{
					list.Add(new AsteroidDescriptor(WORLD_TRAITS.NO_TRAITS.NAME, WORLD_TRAITS.NO_TRAITS.DESCRIPTION, Color.white, null, "NoTraits"));
				}
			}
		}
		return list;
	}

	// Token: 0x060051DE RID: 20958 RVA: 0x001D8F88 File Offset: 0x001D7188
	public List<WorldTrait> GetWorldTraits(ProcGen.World singleWorld)
	{
		List<WorldTrait> list = new List<WorldTrait>();
		List<ProcGen.World> list2 = new List<ProcGen.World>();
		list2.Add(this.startWorld);
		list2.AddRange(this.worlds);
		for (int i = 0; i < list2.Count; i++)
		{
			if (list2[i] == singleWorld)
			{
				ProcGen.World world = list2[i];
				int num = this.seed;
				if (num > 0)
				{
					num += this.cluster.worldPlacements.FindIndex((WorldPlacement x) => x.world == world.filePath);
				}
				foreach (string text in SettingsCache.GetRandomTraits(num, world))
				{
					WorldTrait cachedWorldTrait = SettingsCache.GetCachedWorldTrait(text, true);
					list.Add(cachedWorldTrait);
				}
			}
		}
		return list;
	}

	// Token: 0x040036D9 RID: 14041
	private ProcGen.World startWorld;

	// Token: 0x040036DA RID: 14042
	private ClusterLayout cluster;

	// Token: 0x040036DB RID: 14043
	private List<AsteroidDescriptor> paramDescriptors = new List<AsteroidDescriptor>();

	// Token: 0x040036DC RID: 14044
	private List<AsteroidDescriptor> traitDescriptors = new List<AsteroidDescriptor>();

	// Token: 0x040036DD RID: 14045
	public static List<global::Tuple<string, string, string>> survivalOptions = new List<global::Tuple<string, string, string>>
	{
		new global::Tuple<string, string, string>(WORLDS.SURVIVAL_CHANCE.MOSTHOSPITABLE, "", "D2F40C"),
		new global::Tuple<string, string, string>(WORLDS.SURVIVAL_CHANCE.VERYHIGH, "", "7DE419"),
		new global::Tuple<string, string, string>(WORLDS.SURVIVAL_CHANCE.HIGH, "", "36D246"),
		new global::Tuple<string, string, string>(WORLDS.SURVIVAL_CHANCE.NEUTRAL, "", "63C2B7"),
		new global::Tuple<string, string, string>(WORLDS.SURVIVAL_CHANCE.LOW, "", "6A8EB1"),
		new global::Tuple<string, string, string>(WORLDS.SURVIVAL_CHANCE.VERYLOW, "", "937890"),
		new global::Tuple<string, string, string>(WORLDS.SURVIVAL_CHANCE.LEASTHOSPITABLE, "", "9636DF")
	};
}
