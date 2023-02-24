using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMODUnity;
using KMod;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x0200054F RID: 1359
[AddComponentMenu("KMonoBehaviour/scripts/Assets")]
public class Assets : KMonoBehaviour, ISerializationCallbackReceiver
{
	// Token: 0x0600206B RID: 8299 RVA: 0x000B1068 File Offset: 0x000AF268
	protected override void OnPrefabInit()
	{
		Assets.instance = this;
		if (KPlayerPrefs.HasKey("TemperatureUnit"))
		{
			GameUtil.temperatureUnit = (GameUtil.TemperatureUnit)KPlayerPrefs.GetInt("TemperatureUnit");
		}
		if (KPlayerPrefs.HasKey("MassUnit"))
		{
			GameUtil.massUnit = (GameUtil.MassUnit)KPlayerPrefs.GetInt("MassUnit");
		}
		RecipeManager.DestroyInstance();
		RecipeManager.Get();
		Assets.AnimMaterial = this.AnimMaterialAsset;
		Assets.Prefabs = new List<KPrefabID>(this.PrefabAssets.Where((KPrefabID x) => x != null));
		Assets.PrefabsByTag.Clear();
		Assets.PrefabsByAdditionalTags.Clear();
		Assets.CountableTags.Clear();
		Assets.Sprites = new Dictionary<HashedString, Sprite>();
		foreach (Sprite sprite in this.SpriteAssets)
		{
			if (!(sprite == null))
			{
				HashedString hashedString = new HashedString(sprite.name);
				Assets.Sprites.Add(hashedString, sprite);
			}
		}
		Assets.TintedSprites = this.TintedSpriteAssets.Where((TintedSprite x) => x != null && x.sprite != null).ToList<TintedSprite>();
		Assets.Materials = this.MaterialAssets.Where((Material x) => x != null).ToList<Material>();
		Assets.Textures = this.TextureAssets.Where((Texture2D x) => x != null).ToList<Texture2D>();
		Assets.TextureAtlases = this.TextureAtlasAssets.Where((TextureAtlas x) => x != null).ToList<TextureAtlas>();
		Assets.BlockTileDecorInfos = this.BlockTileDecorInfoAssets.Where((BlockTileDecorInfo x) => x != null).ToList<BlockTileDecorInfo>();
		this.LoadAnims();
		Assets.UIPrefabs = this.UIPrefabAssets;
		Assets.DebugFont = this.DebugFontAsset;
		AsyncLoadManager<IGlobalAsyncLoader>.Run();
		GameAudioSheets.Get().Initialize();
		this.SubstanceListHookup();
		this.CreatePrefabs();
	}

	// Token: 0x0600206C RID: 8300 RVA: 0x000B12C0 File Offset: 0x000AF4C0
	private void CreatePrefabs()
	{
		Db.Get();
		Assets.BuildingDefs = new List<BuildingDef>();
		foreach (KPrefabID kprefabID in this.PrefabAssets)
		{
			if (!(kprefabID == null))
			{
				kprefabID.InitializeTags(true);
				Assets.AddPrefab(kprefabID);
			}
		}
		LegacyModMain.Load();
		Db.Get().PostProcess();
	}

	// Token: 0x0600206D RID: 8301 RVA: 0x000B1344 File Offset: 0x000AF544
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Db.Get();
	}

	// Token: 0x0600206E RID: 8302 RVA: 0x000B1354 File Offset: 0x000AF554
	private static void TryAddCountableTag(KPrefabID prefab)
	{
		foreach (Tag tag in GameTags.DisplayAsUnits)
		{
			if (prefab.HasTag(tag))
			{
				Assets.AddCountableTag(prefab.PrefabTag);
				break;
			}
		}
	}

	// Token: 0x0600206F RID: 8303 RVA: 0x000B13B0 File Offset: 0x000AF5B0
	public static void AddCountableTag(Tag tag)
	{
		Assets.CountableTags.Add(tag);
	}

	// Token: 0x06002070 RID: 8304 RVA: 0x000B13BE File Offset: 0x000AF5BE
	public static bool IsTagCountable(Tag tag)
	{
		return Assets.CountableTags.Contains(tag);
	}

	// Token: 0x06002071 RID: 8305 RVA: 0x000B13CC File Offset: 0x000AF5CC
	private void LoadAnims()
	{
		KAnimBatchManager.DestroyInstance();
		KAnimGroupFile.DestroyInstance();
		KGlobalAnimParser.DestroyInstance();
		KAnimBatchManager.CreateInstance();
		KGlobalAnimParser.CreateInstance();
		KAnimGroupFile.LoadGroupResourceFile();
		if (BundledAssetsLoader.instance.Expansion1Assets != null)
		{
			this.AnimAssets.AddRange(BundledAssetsLoader.instance.Expansion1Assets.AnimAssets);
		}
		Assets.Anims = this.AnimAssets.Where((KAnimFile x) => x != null).ToList<KAnimFile>();
		Assets.Anims.AddRange(Assets.ModLoadedKAnims);
		Assets.AnimTable.Clear();
		foreach (KAnimFile kanimFile in Assets.Anims)
		{
			if (kanimFile != null)
			{
				HashedString hashedString = kanimFile.name;
				Assets.AnimTable[hashedString] = kanimFile;
			}
		}
		KAnimGroupFile.MapNamesToAnimFiles(Assets.AnimTable);
		Global.Instance.modManager.Load(Content.Animation);
		Assets.Anims.AddRange(Assets.ModLoadedKAnims);
		foreach (KAnimFile kanimFile2 in Assets.ModLoadedKAnims)
		{
			if (kanimFile2 != null)
			{
				HashedString hashedString2 = kanimFile2.name;
				Assets.AnimTable[hashedString2] = kanimFile2;
			}
		}
		global::Debug.Assert(Assets.AnimTable.Count > 0, "Anim Assets not yet loaded");
		KAnimGroupFile.LoadAll();
		foreach (KAnimFile kanimFile3 in Assets.Anims)
		{
			kanimFile3.FinalizeLoading();
		}
		KAnimBatchManager.Instance().CompleteInit();
	}

	// Token: 0x06002072 RID: 8306 RVA: 0x000B15BC File Offset: 0x000AF7BC
	private void SubstanceListHookup()
	{
		Dictionary<string, SubstanceTable> dictionary = new Dictionary<string, SubstanceTable> { { "", this.substanceTable } };
		if (BundledAssetsLoader.instance.Expansion1Assets != null)
		{
			dictionary["EXPANSION1_ID"] = BundledAssetsLoader.instance.Expansion1Assets.SubstanceTable;
		}
		Hashtable hashtable = new Hashtable();
		ElementsAudio.Instance.LoadData(AsyncLoadManager<IGlobalAsyncLoader>.AsyncLoader<ElementAudioFileLoader>.Get().entries);
		ElementLoader.Load(ref hashtable, dictionary);
	}

	// Token: 0x06002073 RID: 8307 RVA: 0x000B162E File Offset: 0x000AF82E
	public static string GetSimpleSoundEventName(EventReference event_ref)
	{
		return Assets.GetSimpleSoundEventName(KFMOD.GetEventReferencePath(event_ref));
	}

	// Token: 0x06002074 RID: 8308 RVA: 0x000B163C File Offset: 0x000AF83C
	public static string GetSimpleSoundEventName(string path)
	{
		string text = null;
		if (!Assets.simpleSoundEventNames.TryGetValue(path, out text))
		{
			int num = path.LastIndexOf('/');
			text = ((num != -1) ? path.Substring(num + 1) : path);
			Assets.simpleSoundEventNames[path] = text;
		}
		return text;
	}

	// Token: 0x06002075 RID: 8309 RVA: 0x000B1684 File Offset: 0x000AF884
	private static BuildingDef GetDef(IList<BuildingDef> defs, string prefab_id)
	{
		int count = defs.Count;
		for (int i = 0; i < count; i++)
		{
			if (defs[i].PrefabID == prefab_id)
			{
				return defs[i];
			}
		}
		return null;
	}

	// Token: 0x06002076 RID: 8310 RVA: 0x000B16C1 File Offset: 0x000AF8C1
	public static BuildingDef GetBuildingDef(string prefab_id)
	{
		return Assets.GetDef(Assets.BuildingDefs, prefab_id);
	}

	// Token: 0x06002077 RID: 8311 RVA: 0x000B16D0 File Offset: 0x000AF8D0
	public static TintedSprite GetTintedSprite(string name)
	{
		TintedSprite tintedSprite = null;
		if (Assets.TintedSprites != null)
		{
			for (int i = 0; i < Assets.TintedSprites.Count; i++)
			{
				if (Assets.TintedSprites[i].sprite.name == name)
				{
					tintedSprite = Assets.TintedSprites[i];
					break;
				}
			}
		}
		return tintedSprite;
	}

	// Token: 0x06002078 RID: 8312 RVA: 0x000B1728 File Offset: 0x000AF928
	public static Sprite GetSprite(HashedString name)
	{
		Sprite sprite = null;
		if (Assets.Sprites != null)
		{
			Assets.Sprites.TryGetValue(name, out sprite);
		}
		return sprite;
	}

	// Token: 0x06002079 RID: 8313 RVA: 0x000B174D File Offset: 0x000AF94D
	public static VideoClip GetVideo(string name)
	{
		return Resources.Load<VideoClip>("video_webm/" + name);
	}

	// Token: 0x0600207A RID: 8314 RVA: 0x000B1760 File Offset: 0x000AF960
	public static Texture2D GetTexture(string name)
	{
		Texture2D texture2D = null;
		if (Assets.Textures != null)
		{
			for (int i = 0; i < Assets.Textures.Count; i++)
			{
				if (Assets.Textures[i].name == name)
				{
					texture2D = Assets.Textures[i];
					break;
				}
			}
		}
		return texture2D;
	}

	// Token: 0x0600207B RID: 8315 RVA: 0x000B17B4 File Offset: 0x000AF9B4
	public static ComicData GetComic(string id)
	{
		foreach (ComicData comicData in Assets.instance.comics)
		{
			if (comicData.name == id)
			{
				return comicData;
			}
		}
		return null;
	}

	// Token: 0x0600207C RID: 8316 RVA: 0x000B17F0 File Offset: 0x000AF9F0
	public static void AddPrefab(KPrefabID prefab)
	{
		if (prefab == null)
		{
			return;
		}
		prefab.InitializeTags(true);
		prefab.UpdateSaveLoadTag();
		if (Assets.PrefabsByTag.ContainsKey(prefab.PrefabTag))
		{
			string text = "Tried loading prefab with duplicate tag, ignoring: ";
			Tag prefabTag = prefab.PrefabTag;
			global::Debug.LogWarning(text + prefabTag.ToString());
			return;
		}
		Assets.PrefabsByTag[prefab.PrefabTag] = prefab;
		foreach (Tag tag in prefab.Tags)
		{
			if (!Assets.PrefabsByAdditionalTags.ContainsKey(tag))
			{
				Assets.PrefabsByAdditionalTags[tag] = new List<KPrefabID>();
			}
			Assets.PrefabsByAdditionalTags[tag].Add(prefab);
		}
		Assets.Prefabs.Add(prefab);
		Assets.TryAddCountableTag(prefab);
		if (Assets.OnAddPrefab != null)
		{
			Assets.OnAddPrefab(prefab);
		}
	}

	// Token: 0x0600207D RID: 8317 RVA: 0x000B18EC File Offset: 0x000AFAEC
	public static void RegisterOnAddPrefab(Action<KPrefabID> on_add)
	{
		Assets.OnAddPrefab = (Action<KPrefabID>)Delegate.Combine(Assets.OnAddPrefab, on_add);
		foreach (KPrefabID kprefabID in Assets.Prefabs)
		{
			on_add(kprefabID);
		}
	}

	// Token: 0x0600207E RID: 8318 RVA: 0x000B1954 File Offset: 0x000AFB54
	public static void UnregisterOnAddPrefab(Action<KPrefabID> on_add)
	{
		Assets.OnAddPrefab = (Action<KPrefabID>)Delegate.Remove(Assets.OnAddPrefab, on_add);
	}

	// Token: 0x0600207F RID: 8319 RVA: 0x000B196B File Offset: 0x000AFB6B
	public static void ClearOnAddPrefab()
	{
		Assets.OnAddPrefab = null;
	}

	// Token: 0x06002080 RID: 8320 RVA: 0x000B1974 File Offset: 0x000AFB74
	public static GameObject GetPrefab(Tag tag)
	{
		GameObject gameObject = Assets.TryGetPrefab(tag);
		if (gameObject == null)
		{
			string text = "Missing prefab: ";
			Tag tag2 = tag;
			global::Debug.LogWarning(text + tag2.ToString());
		}
		return gameObject;
	}

	// Token: 0x06002081 RID: 8321 RVA: 0x000B19B0 File Offset: 0x000AFBB0
	public static GameObject TryGetPrefab(Tag tag)
	{
		KPrefabID kprefabID = null;
		Assets.PrefabsByTag.TryGetValue(tag, out kprefabID);
		if (!(kprefabID != null))
		{
			return null;
		}
		return kprefabID.gameObject;
	}

	// Token: 0x06002082 RID: 8322 RVA: 0x000B19E0 File Offset: 0x000AFBE0
	public static List<GameObject> GetPrefabsWithTag(Tag tag)
	{
		List<GameObject> list = new List<GameObject>();
		if (Assets.PrefabsByAdditionalTags.ContainsKey(tag))
		{
			for (int i = 0; i < Assets.PrefabsByAdditionalTags[tag].Count; i++)
			{
				list.Add(Assets.PrefabsByAdditionalTags[tag][i].gameObject);
			}
		}
		return list;
	}

	// Token: 0x06002083 RID: 8323 RVA: 0x000B1A38 File Offset: 0x000AFC38
	public static List<GameObject> GetPrefabsWithComponent<Type>()
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < Assets.Prefabs.Count; i++)
		{
			if (Assets.Prefabs[i].GetComponent<Type>() != null)
			{
				list.Add(Assets.Prefabs[i].gameObject);
			}
		}
		return list;
	}

	// Token: 0x06002084 RID: 8324 RVA: 0x000B1A8E File Offset: 0x000AFC8E
	public static GameObject GetPrefabWithComponent<Type>()
	{
		List<GameObject> prefabsWithComponent = Assets.GetPrefabsWithComponent<Type>();
		global::Debug.Assert(prefabsWithComponent.Count > 0, "There are no prefabs of type " + typeof(Type).Name);
		return prefabsWithComponent[0];
	}

	// Token: 0x06002085 RID: 8325 RVA: 0x000B1AC4 File Offset: 0x000AFCC4
	public static List<Tag> GetPrefabTagsWithComponent<Type>()
	{
		List<Tag> list = new List<Tag>();
		for (int i = 0; i < Assets.Prefabs.Count; i++)
		{
			if (Assets.Prefabs[i].GetComponent<Type>() != null)
			{
				list.Add(Assets.Prefabs[i].PrefabID());
			}
		}
		return list;
	}

	// Token: 0x06002086 RID: 8326 RVA: 0x000B1B1C File Offset: 0x000AFD1C
	public static Assets GetInstanceEditorOnly()
	{
		Assets[] array = (Assets[])Resources.FindObjectsOfTypeAll(typeof(Assets));
		if (array != null)
		{
			int num = array.Length;
		}
		return array[0];
	}

	// Token: 0x06002087 RID: 8327 RVA: 0x000B1B48 File Offset: 0x000AFD48
	public static TextureAtlas GetTextureAtlas(string name)
	{
		foreach (TextureAtlas textureAtlas in Assets.TextureAtlases)
		{
			if (textureAtlas.name == name)
			{
				return textureAtlas;
			}
		}
		return null;
	}

	// Token: 0x06002088 RID: 8328 RVA: 0x000B1BA8 File Offset: 0x000AFDA8
	public static Material GetMaterial(string name)
	{
		foreach (Material material in Assets.Materials)
		{
			if (material.name == name)
			{
				return material;
			}
		}
		return null;
	}

	// Token: 0x06002089 RID: 8329 RVA: 0x000B1C08 File Offset: 0x000AFE08
	public static BlockTileDecorInfo GetBlockTileDecorInfo(string name)
	{
		foreach (BlockTileDecorInfo blockTileDecorInfo in Assets.BlockTileDecorInfos)
		{
			if (blockTileDecorInfo.name == name)
			{
				return blockTileDecorInfo;
			}
		}
		global::Debug.LogError("Could not find BlockTileDecorInfo named [" + name + "]");
		return null;
	}

	// Token: 0x0600208A RID: 8330 RVA: 0x000B1C80 File Offset: 0x000AFE80
	public static KAnimFile GetAnim(HashedString name)
	{
		if (!name.IsValid)
		{
			global::Debug.LogWarning("Invalid hash name");
			return null;
		}
		KAnimFile kanimFile = null;
		Assets.AnimTable.TryGetValue(name, out kanimFile);
		if (kanimFile == null)
		{
			global::Debug.LogWarning("Missing Anim: [" + name.ToString() + "]. You may have to run Collect Anim on the Assets prefab");
		}
		return kanimFile;
	}

	// Token: 0x0600208B RID: 8331 RVA: 0x000B1CDD File Offset: 0x000AFEDD
	public static bool TryGetAnim(HashedString name, out KAnimFile anim)
	{
		if (!name.IsValid)
		{
			global::Debug.LogWarning("Invalid hash name");
			anim = null;
			return false;
		}
		Assets.AnimTable.TryGetValue(name, out anim);
		return anim != null;
	}

	// Token: 0x0600208C RID: 8332 RVA: 0x000B1D0C File Offset: 0x000AFF0C
	public void OnAfterDeserialize()
	{
		this.TintedSpriteAssets = this.TintedSpriteAssets.Where((TintedSprite x) => x != null && x.sprite != null).ToList<TintedSprite>();
		this.TintedSpriteAssets.Sort((TintedSprite a, TintedSprite b) => a.name.CompareTo(b.name));
	}

	// Token: 0x0600208D RID: 8333 RVA: 0x000B1D78 File Offset: 0x000AFF78
	public void OnBeforeSerialize()
	{
	}

	// Token: 0x0600208E RID: 8334 RVA: 0x000B1D7C File Offset: 0x000AFF7C
	public static void AddBuildingDef(BuildingDef def)
	{
		Assets.BuildingDefs = Assets.BuildingDefs.Where((BuildingDef x) => x.PrefabID != def.PrefabID).ToList<BuildingDef>();
		Assets.BuildingDefs.Add(def);
	}

	// Token: 0x040012A6 RID: 4774
	public static List<KAnimFile> ModLoadedKAnims = new List<KAnimFile>();

	// Token: 0x040012A7 RID: 4775
	private static Action<KPrefabID> OnAddPrefab;

	// Token: 0x040012A8 RID: 4776
	public static List<BuildingDef> BuildingDefs;

	// Token: 0x040012A9 RID: 4777
	public List<KPrefabID> PrefabAssets = new List<KPrefabID>();

	// Token: 0x040012AA RID: 4778
	public static List<KPrefabID> Prefabs = new List<KPrefabID>();

	// Token: 0x040012AB RID: 4779
	private static HashSet<Tag> CountableTags = new HashSet<Tag>();

	// Token: 0x040012AC RID: 4780
	public List<Sprite> SpriteAssets;

	// Token: 0x040012AD RID: 4781
	public static Dictionary<HashedString, Sprite> Sprites;

	// Token: 0x040012AE RID: 4782
	public List<string> videoClipNames;

	// Token: 0x040012AF RID: 4783
	private const string VIDEO_ASSET_PATH = "video_webm";

	// Token: 0x040012B0 RID: 4784
	public List<TintedSprite> TintedSpriteAssets;

	// Token: 0x040012B1 RID: 4785
	public static List<TintedSprite> TintedSprites;

	// Token: 0x040012B2 RID: 4786
	public List<Texture2D> TextureAssets;

	// Token: 0x040012B3 RID: 4787
	public static List<Texture2D> Textures;

	// Token: 0x040012B4 RID: 4788
	public static List<TextureAtlas> TextureAtlases;

	// Token: 0x040012B5 RID: 4789
	public List<TextureAtlas> TextureAtlasAssets;

	// Token: 0x040012B6 RID: 4790
	public static List<Material> Materials;

	// Token: 0x040012B7 RID: 4791
	public List<Material> MaterialAssets;

	// Token: 0x040012B8 RID: 4792
	public static List<Shader> Shaders;

	// Token: 0x040012B9 RID: 4793
	public List<Shader> ShaderAssets;

	// Token: 0x040012BA RID: 4794
	public static List<BlockTileDecorInfo> BlockTileDecorInfos;

	// Token: 0x040012BB RID: 4795
	public List<BlockTileDecorInfo> BlockTileDecorInfoAssets;

	// Token: 0x040012BC RID: 4796
	public Material AnimMaterialAsset;

	// Token: 0x040012BD RID: 4797
	public static Material AnimMaterial;

	// Token: 0x040012BE RID: 4798
	public DiseaseVisualization DiseaseVisualization;

	// Token: 0x040012BF RID: 4799
	public Sprite LegendColourBox;

	// Token: 0x040012C0 RID: 4800
	public Texture2D invalidAreaTex;

	// Token: 0x040012C1 RID: 4801
	public Assets.UIPrefabData UIPrefabAssets;

	// Token: 0x040012C2 RID: 4802
	public static Assets.UIPrefabData UIPrefabs;

	// Token: 0x040012C3 RID: 4803
	private static Dictionary<Tag, KPrefabID> PrefabsByTag = new Dictionary<Tag, KPrefabID>();

	// Token: 0x040012C4 RID: 4804
	private static Dictionary<Tag, List<KPrefabID>> PrefabsByAdditionalTags = new Dictionary<Tag, List<KPrefabID>>();

	// Token: 0x040012C5 RID: 4805
	public List<KAnimFile> AnimAssets;

	// Token: 0x040012C6 RID: 4806
	public static List<KAnimFile> Anims;

	// Token: 0x040012C7 RID: 4807
	private static Dictionary<HashedString, KAnimFile> AnimTable = new Dictionary<HashedString, KAnimFile>();

	// Token: 0x040012C8 RID: 4808
	public Font DebugFontAsset;

	// Token: 0x040012C9 RID: 4809
	public static Font DebugFont;

	// Token: 0x040012CA RID: 4810
	public SubstanceTable substanceTable;

	// Token: 0x040012CB RID: 4811
	[SerializeField]
	public TextAsset elementAudio;

	// Token: 0x040012CC RID: 4812
	[SerializeField]
	public TextAsset personalitiesFile;

	// Token: 0x040012CD RID: 4813
	public LogicModeUI logicModeUIData;

	// Token: 0x040012CE RID: 4814
	public CommonPlacerConfig.CommonPlacerAssets commonPlacerAssets;

	// Token: 0x040012CF RID: 4815
	public DigPlacerConfig.DigPlacerAssets digPlacerAssets;

	// Token: 0x040012D0 RID: 4816
	public MopPlacerConfig.MopPlacerAssets mopPlacerAssets;

	// Token: 0x040012D1 RID: 4817
	public ComicData[] comics;

	// Token: 0x040012D2 RID: 4818
	public static Assets instance;

	// Token: 0x040012D3 RID: 4819
	private static Dictionary<string, string> simpleSoundEventNames = new Dictionary<string, string>();

	// Token: 0x0200117B RID: 4475
	[Serializable]
	public struct UIPrefabData
	{
		// Token: 0x04005AE5 RID: 23269
		public ProgressBar ProgressBar;

		// Token: 0x04005AE6 RID: 23270
		public HealthBar HealthBar;

		// Token: 0x04005AE7 RID: 23271
		public GameObject ResourceVisualizer;

		// Token: 0x04005AE8 RID: 23272
		public Image RegionCellBlocked;

		// Token: 0x04005AE9 RID: 23273
		public RectTransform PriorityOverlayIcon;

		// Token: 0x04005AEA RID: 23274
		public RectTransform HarvestWhenReadyOverlayIcon;

		// Token: 0x04005AEB RID: 23275
		public Assets.TableScreenAssets TableScreenWidgets;
	}

	// Token: 0x0200117C RID: 4476
	[Serializable]
	public struct TableScreenAssets
	{
		// Token: 0x04005AEC RID: 23276
		public Material DefaultUIMaterial;

		// Token: 0x04005AED RID: 23277
		public Material DesaturatedUIMaterial;

		// Token: 0x04005AEE RID: 23278
		public GameObject MinionPortrait;

		// Token: 0x04005AEF RID: 23279
		public GameObject GenericPortrait;

		// Token: 0x04005AF0 RID: 23280
		public GameObject TogglePortrait;

		// Token: 0x04005AF1 RID: 23281
		public GameObject ButtonLabel;

		// Token: 0x04005AF2 RID: 23282
		public GameObject ButtonLabelWhite;

		// Token: 0x04005AF3 RID: 23283
		public GameObject Label;

		// Token: 0x04005AF4 RID: 23284
		public GameObject LabelHeader;

		// Token: 0x04005AF5 RID: 23285
		public GameObject Checkbox;

		// Token: 0x04005AF6 RID: 23286
		public GameObject BlankCell;

		// Token: 0x04005AF7 RID: 23287
		public GameObject SuperCheckbox_Horizontal;

		// Token: 0x04005AF8 RID: 23288
		public GameObject SuperCheckbox_Vertical;

		// Token: 0x04005AF9 RID: 23289
		public GameObject Spacer;

		// Token: 0x04005AFA RID: 23290
		public GameObject NumericDropDown;

		// Token: 0x04005AFB RID: 23291
		public GameObject DropDownHeader;

		// Token: 0x04005AFC RID: 23292
		public GameObject PriorityGroupSelector;

		// Token: 0x04005AFD RID: 23293
		public GameObject PriorityGroupSelectorHeader;

		// Token: 0x04005AFE RID: 23294
		public GameObject PrioritizeRowWidget;

		// Token: 0x04005AFF RID: 23295
		public GameObject PrioritizeRowHeaderWidget;
	}
}
