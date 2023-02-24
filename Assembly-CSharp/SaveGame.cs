using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Klei.CustomSettings;
using KSerialization;
using Newtonsoft.Json;
using ProcGen;
using STRINGS;
using UnityEngine;

// Token: 0x020008FD RID: 2301
[SerializationConfig(KSerialization.MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/SaveGame")]
public class SaveGame : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x170004BC RID: 1212
	// (get) Token: 0x06004275 RID: 17013 RVA: 0x00176ADB File Offset: 0x00174CDB
	// (set) Token: 0x06004276 RID: 17014 RVA: 0x00176AE3 File Offset: 0x00174CE3
	public int AutoSaveCycleInterval
	{
		get
		{
			return this.autoSaveCycleInterval;
		}
		set
		{
			this.autoSaveCycleInterval = value;
		}
	}

	// Token: 0x170004BD RID: 1213
	// (get) Token: 0x06004277 RID: 17015 RVA: 0x00176AEC File Offset: 0x00174CEC
	// (set) Token: 0x06004278 RID: 17016 RVA: 0x00176AF4 File Offset: 0x00174CF4
	public Vector2I TimelapseResolution
	{
		get
		{
			return this.timelapseResolution;
		}
		set
		{
			this.timelapseResolution = value;
		}
	}

	// Token: 0x170004BE RID: 1214
	// (get) Token: 0x06004279 RID: 17017 RVA: 0x00176AFD File Offset: 0x00174CFD
	public string BaseName
	{
		get
		{
			return this.baseName;
		}
	}

	// Token: 0x0600427A RID: 17018 RVA: 0x00176B05 File Offset: 0x00174D05
	public static void DestroyInstance()
	{
		SaveGame.Instance = null;
	}

	// Token: 0x0600427B RID: 17019 RVA: 0x00176B10 File Offset: 0x00174D10
	protected override void OnPrefabInit()
	{
		SaveGame.Instance = this;
		new ColonyRationMonitor.Instance(this).StartSM();
		this.entombedItemManager = base.gameObject.AddComponent<EntombedItemManager>();
		this.worldGenSpawner = base.gameObject.AddComponent<WorldGenSpawner>();
		base.gameObject.AddOrGetDef<GameplaySeasonManager.Def>();
		base.gameObject.AddOrGetDef<ClusterFogOfWarManager.Def>();
	}

	// Token: 0x0600427C RID: 17020 RVA: 0x00176B68 File Offset: 0x00174D68
	[OnSerializing]
	private void OnSerialize()
	{
		this.speed = SpeedControlScreen.Instance.GetSpeed();
	}

	// Token: 0x0600427D RID: 17021 RVA: 0x00176B7A File Offset: 0x00174D7A
	[OnDeserializing]
	private void OnDeserialize()
	{
		this.baseName = SaveLoader.Instance.GameInfo.baseName;
	}

	// Token: 0x0600427E RID: 17022 RVA: 0x00176B91 File Offset: 0x00174D91
	public int GetSpeed()
	{
		return this.speed;
	}

	// Token: 0x0600427F RID: 17023 RVA: 0x00176B9C File Offset: 0x00174D9C
	public byte[] GetSaveHeader(bool isAutoSave, bool isCompressed, out SaveGame.Header header)
	{
		string originalSaveFileName = SaveLoader.GetOriginalSaveFileName(SaveLoader.GetActiveSaveFilePath());
		string text = JsonConvert.SerializeObject(new SaveGame.GameInfo(GameClock.Instance.GetCycle(), Components.LiveMinionIdentities.Count, this.baseName, isAutoSave, originalSaveFileName, SaveLoader.Instance.GameInfo.clusterId, SaveLoader.Instance.GameInfo.worldTraits, SaveLoader.Instance.GameInfo.colonyGuid, DlcManager.GetHighestActiveDlcId(), this.sandboxEnabled));
		byte[] bytes = Encoding.UTF8.GetBytes(text);
		header = default(SaveGame.Header);
		header.buildVersion = 544519U;
		header.headerSize = bytes.Length;
		header.headerVersion = 1U;
		header.compression = (isCompressed ? 1 : 0);
		return bytes;
	}

	// Token: 0x06004280 RID: 17024 RVA: 0x00176C56 File Offset: 0x00174E56
	public static string GetSaveUniqueID(SaveGame.GameInfo info)
	{
		if (!(info.colonyGuid != Guid.Empty))
		{
			return info.baseName + "/" + info.clusterId;
		}
		return info.colonyGuid.ToString();
	}

	// Token: 0x06004281 RID: 17025 RVA: 0x00176C94 File Offset: 0x00174E94
	public static global::Tuple<SaveGame.Header, SaveGame.GameInfo> GetFileInfo(string filename)
	{
		try
		{
			SaveGame.Header header;
			SaveGame.GameInfo gameInfo = SaveLoader.LoadHeader(filename, out header);
			if (gameInfo.saveMajorVersion >= 7)
			{
				return new global::Tuple<SaveGame.Header, SaveGame.GameInfo>(header, gameInfo);
			}
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning("Exception while loading " + filename);
			global::Debug.LogWarning(ex);
		}
		return null;
	}

	// Token: 0x06004282 RID: 17026 RVA: 0x00176CEC File Offset: 0x00174EEC
	public static SaveGame.GameInfo GetHeader(IReader br, out SaveGame.Header header, string debugFileName)
	{
		header = default(SaveGame.Header);
		header.buildVersion = br.ReadUInt32();
		header.headerSize = br.ReadInt32();
		header.headerVersion = br.ReadUInt32();
		if (1U <= header.headerVersion)
		{
			header.compression = br.ReadInt32();
		}
		byte[] array = br.ReadBytes(header.headerSize);
		if (header.headerSize == 0 && !SaveGame.debug_SaveFileHeaderBlank_sent)
		{
			SaveGame.debug_SaveFileHeaderBlank_sent = true;
			global::Debug.LogWarning("SaveFileHeaderBlank - " + debugFileName);
		}
		SaveGame.GameInfo gameInfo = SaveGame.GetGameInfo(array);
		if (gameInfo.IsVersionOlderThan(7, 14) && gameInfo.worldTraits != null)
		{
			string[] worldTraits = gameInfo.worldTraits;
			for (int i = 0; i < worldTraits.Length; i++)
			{
				worldTraits[i] = worldTraits[i].Replace('\\', '/');
			}
		}
		if (gameInfo.IsVersionOlderThan(7, 20))
		{
			gameInfo.dlcId = "";
		}
		return gameInfo;
	}

	// Token: 0x06004283 RID: 17027 RVA: 0x00176DC1 File Offset: 0x00174FC1
	public static SaveGame.GameInfo GetGameInfo(byte[] data)
	{
		return JsonConvert.DeserializeObject<SaveGame.GameInfo>(Encoding.UTF8.GetString(data));
	}

	// Token: 0x06004284 RID: 17028 RVA: 0x00176DD3 File Offset: 0x00174FD3
	public void SetBaseName(string newBaseName)
	{
		if (string.IsNullOrEmpty(newBaseName))
		{
			global::Debug.LogWarning("Cannot give the base an empty name");
			return;
		}
		this.baseName = newBaseName;
	}

	// Token: 0x06004285 RID: 17029 RVA: 0x00176DEF File Offset: 0x00174FEF
	protected override void OnSpawn()
	{
		ThreadedHttps<KleiMetrics>.Instance.SendProfileStats();
		Game.Instance.Trigger(-1917495436, null);
	}

	// Token: 0x06004286 RID: 17030 RVA: 0x00176E0C File Offset: 0x0017500C
	public List<global::Tuple<string, TextStyleSetting>> GetColonyToolTip()
	{
		List<global::Tuple<string, TextStyleSetting>> list = new List<global::Tuple<string, TextStyleSetting>>();
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.ClusterLayout);
		ClusterLayout clusterLayout;
		SettingsCache.clusterLayouts.clusterCache.TryGetValue(currentQualitySetting.id, out clusterLayout);
		list.Add(new global::Tuple<string, TextStyleSetting>(this.baseName, ToolTipScreen.Instance.defaultTooltipHeaderStyle));
		if (DlcManager.IsExpansion1Active())
		{
			StringEntry stringEntry = Strings.Get(clusterLayout.name);
			list.Add(new global::Tuple<string, TextStyleSetting>(stringEntry, ToolTipScreen.Instance.defaultTooltipBodyStyle));
		}
		if (GameClock.Instance != null)
		{
			list.Add(new global::Tuple<string, TextStyleSetting>(" ", null));
			list.Add(new global::Tuple<string, TextStyleSetting>(string.Format(UI.ASTEROIDCLOCK.CYCLES_OLD, GameUtil.GetCurrentCycle()), ToolTipScreen.Instance.defaultTooltipHeaderStyle));
			list.Add(new global::Tuple<string, TextStyleSetting>(string.Format(UI.ASTEROIDCLOCK.TIME_PLAYED, (GameClock.Instance.GetTimePlayedInSeconds() / 3600f).ToString("0.00")), ToolTipScreen.Instance.defaultTooltipBodyStyle));
		}
		int cameraActiveCluster = CameraController.Instance.cameraActiveCluster;
		WorldContainer world = ClusterManager.Instance.GetWorld(cameraActiveCluster);
		list.Add(new global::Tuple<string, TextStyleSetting>(" ", null));
		if (DlcManager.IsExpansion1Active())
		{
			list.Add(new global::Tuple<string, TextStyleSetting>(world.GetComponent<ClusterGridEntity>().Name, ToolTipScreen.Instance.defaultTooltipHeaderStyle));
		}
		else
		{
			StringEntry stringEntry2 = Strings.Get(clusterLayout.name);
			list.Add(new global::Tuple<string, TextStyleSetting>(stringEntry2, ToolTipScreen.Instance.defaultTooltipHeaderStyle));
		}
		if (SaveLoader.Instance.GameInfo.worldTraits != null && SaveLoader.Instance.GameInfo.worldTraits.Length != 0)
		{
			string[] worldTraits = SaveLoader.Instance.GameInfo.worldTraits;
			for (int i = 0; i < worldTraits.Length; i++)
			{
				WorldTrait cachedWorldTrait = SettingsCache.GetCachedWorldTrait(worldTraits[i], false);
				if (cachedWorldTrait != null)
				{
					list.Add(new global::Tuple<string, TextStyleSetting>(Strings.Get(cachedWorldTrait.name), ToolTipScreen.Instance.defaultTooltipBodyStyle));
				}
				else
				{
					list.Add(new global::Tuple<string, TextStyleSetting>(WORLD_TRAITS.MISSING_TRAIT, ToolTipScreen.Instance.defaultTooltipBodyStyle));
				}
			}
		}
		else if (world.WorldTraitIds != null)
		{
			foreach (string text in world.WorldTraitIds)
			{
				WorldTrait cachedWorldTrait2 = SettingsCache.GetCachedWorldTrait(text, false);
				if (cachedWorldTrait2 != null)
				{
					list.Add(new global::Tuple<string, TextStyleSetting>(Strings.Get(cachedWorldTrait2.name), ToolTipScreen.Instance.defaultTooltipBodyStyle));
				}
				else
				{
					list.Add(new global::Tuple<string, TextStyleSetting>(WORLD_TRAITS.MISSING_TRAIT, ToolTipScreen.Instance.defaultTooltipBodyStyle));
				}
			}
			if (world.WorldTraitIds.Count == 0)
			{
				list.Add(new global::Tuple<string, TextStyleSetting>(WORLD_TRAITS.NO_TRAITS.NAME_SHORTHAND, ToolTipScreen.Instance.defaultTooltipBodyStyle));
			}
		}
		return list;
	}

	// Token: 0x04002C68 RID: 11368
	[Serialize]
	private int speed;

	// Token: 0x04002C69 RID: 11369
	[Serialize]
	public List<Tag> expandedResourceTags = new List<Tag>();

	// Token: 0x04002C6A RID: 11370
	[Serialize]
	public int minGermCountForDisinfect = 10000;

	// Token: 0x04002C6B RID: 11371
	[Serialize]
	public bool enableAutoDisinfect = true;

	// Token: 0x04002C6C RID: 11372
	[Serialize]
	public bool sandboxEnabled;

	// Token: 0x04002C6D RID: 11373
	[Serialize]
	private int autoSaveCycleInterval = 1;

	// Token: 0x04002C6E RID: 11374
	[Serialize]
	private Vector2I timelapseResolution = new Vector2I(512, 768);

	// Token: 0x04002C6F RID: 11375
	private string baseName;

	// Token: 0x04002C70 RID: 11376
	public static SaveGame Instance;

	// Token: 0x04002C71 RID: 11377
	public EntombedItemManager entombedItemManager;

	// Token: 0x04002C72 RID: 11378
	public WorldGenSpawner worldGenSpawner;

	// Token: 0x04002C73 RID: 11379
	[MyCmpReq]
	public MaterialSelectorSerializer materialSelectorSerializer;

	// Token: 0x04002C74 RID: 11380
	private static bool debug_SaveFileHeaderBlank_sent;

	// Token: 0x020016C9 RID: 5833
	public struct Header
	{
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060088B3 RID: 34995 RVA: 0x002F6700 File Offset: 0x002F4900
		public bool IsCompressed
		{
			get
			{
				return this.compression != 0;
			}
		}

		// Token: 0x04006AF3 RID: 27379
		public uint buildVersion;

		// Token: 0x04006AF4 RID: 27380
		public int headerSize;

		// Token: 0x04006AF5 RID: 27381
		public uint headerVersion;

		// Token: 0x04006AF6 RID: 27382
		public int compression;
	}

	// Token: 0x020016CA RID: 5834
	public struct GameInfo
	{
		// Token: 0x060088B4 RID: 34996 RVA: 0x002F670C File Offset: 0x002F490C
		public GameInfo(int numberOfCycles, int numberOfDuplicants, string baseName, bool isAutoSave, string originalSaveName, string clusterId, string[] worldTraits, Guid colonyGuid, string dlcId, bool sandboxEnabled = false)
		{
			this.numberOfCycles = numberOfCycles;
			this.numberOfDuplicants = numberOfDuplicants;
			this.baseName = baseName;
			this.isAutoSave = isAutoSave;
			this.originalSaveName = originalSaveName;
			this.clusterId = clusterId;
			this.worldTraits = worldTraits;
			this.colonyGuid = colonyGuid;
			this.sandboxEnabled = sandboxEnabled;
			this.dlcId = dlcId;
			this.saveMajorVersion = 7;
			this.saveMinorVersion = 31;
		}

		// Token: 0x060088B5 RID: 34997 RVA: 0x002F6775 File Offset: 0x002F4975
		public bool IsVersionOlderThan(int major, int minor)
		{
			return this.saveMajorVersion < major || (this.saveMajorVersion == major && this.saveMinorVersion < minor);
		}

		// Token: 0x060088B6 RID: 34998 RVA: 0x002F6796 File Offset: 0x002F4996
		public bool IsVersionExactly(int major, int minor)
		{
			return this.saveMajorVersion == major && this.saveMinorVersion == minor;
		}

		// Token: 0x04006AF7 RID: 27383
		public int numberOfCycles;

		// Token: 0x04006AF8 RID: 27384
		public int numberOfDuplicants;

		// Token: 0x04006AF9 RID: 27385
		public string baseName;

		// Token: 0x04006AFA RID: 27386
		public bool isAutoSave;

		// Token: 0x04006AFB RID: 27387
		public string originalSaveName;

		// Token: 0x04006AFC RID: 27388
		public int saveMajorVersion;

		// Token: 0x04006AFD RID: 27389
		public int saveMinorVersion;

		// Token: 0x04006AFE RID: 27390
		public string clusterId;

		// Token: 0x04006AFF RID: 27391
		public string[] worldTraits;

		// Token: 0x04006B00 RID: 27392
		public bool sandboxEnabled;

		// Token: 0x04006B01 RID: 27393
		public Guid colonyGuid;

		// Token: 0x04006B02 RID: 27394
		public string dlcId;
	}
}
