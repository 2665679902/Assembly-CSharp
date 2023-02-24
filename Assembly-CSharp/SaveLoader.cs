using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ionic.Zlib;
using Klei;
using Klei.AI;
using Klei.CustomSettings;
using KMod;
using KSerialization;
using Newtonsoft.Json;
using ProcGen;
using ProcGenGame;
using STRINGS;
using UnityEngine;

// Token: 0x020008FE RID: 2302
[AddComponentMenu("KMonoBehaviour/scripts/SaveLoader")]
public class SaveLoader : KMonoBehaviour
{
	// Token: 0x170004BF RID: 1215
	// (get) Token: 0x06004288 RID: 17032 RVA: 0x00177164 File Offset: 0x00175364
	// (set) Token: 0x06004289 RID: 17033 RVA: 0x0017716C File Offset: 0x0017536C
	public bool loadedFromSave { get; private set; }

	// Token: 0x0600428A RID: 17034 RVA: 0x00177175 File Offset: 0x00175375
	public static void DestroyInstance()
	{
		SaveLoader.Instance = null;
	}

	// Token: 0x170004C0 RID: 1216
	// (get) Token: 0x0600428B RID: 17035 RVA: 0x0017717D File Offset: 0x0017537D
	// (set) Token: 0x0600428C RID: 17036 RVA: 0x00177184 File Offset: 0x00175384
	public static SaveLoader Instance { get; private set; }

	// Token: 0x170004C1 RID: 1217
	// (get) Token: 0x0600428D RID: 17037 RVA: 0x0017718C File Offset: 0x0017538C
	// (set) Token: 0x0600428E RID: 17038 RVA: 0x00177194 File Offset: 0x00175394
	public Action<Cluster> OnWorldGenComplete { get; set; }

	// Token: 0x170004C2 RID: 1218
	// (get) Token: 0x0600428F RID: 17039 RVA: 0x0017719D File Offset: 0x0017539D
	public Cluster ClusterLayout
	{
		get
		{
			return this.m_clusterLayout;
		}
	}

	// Token: 0x170004C3 RID: 1219
	// (get) Token: 0x06004290 RID: 17040 RVA: 0x001771A5 File Offset: 0x001753A5
	// (set) Token: 0x06004291 RID: 17041 RVA: 0x001771AD File Offset: 0x001753AD
	public SaveGame.GameInfo GameInfo { get; private set; }

	// Token: 0x06004292 RID: 17042 RVA: 0x001771B6 File Offset: 0x001753B6
	protected override void OnPrefabInit()
	{
		SaveLoader.Instance = this;
		this.saveManager = base.GetComponent<SaveManager>();
	}

	// Token: 0x06004293 RID: 17043 RVA: 0x001771CA File Offset: 0x001753CA
	private void MoveCorruptFile(string filename)
	{
	}

	// Token: 0x06004294 RID: 17044 RVA: 0x001771CC File Offset: 0x001753CC
	protected override void OnSpawn()
	{
		string activeSaveFilePath = SaveLoader.GetActiveSaveFilePath();
		if (WorldGen.CanLoad(activeSaveFilePath))
		{
			Sim.SIM_Initialize(new Sim.GAME_MessageHandler(Sim.DLL_MessageHandler));
			SimMessages.CreateSimElementsTable(ElementLoader.elements);
			SimMessages.CreateDiseaseTable(Db.Get().Diseases);
			this.loadedFromSave = true;
			this.loadedFromSave = this.Load(activeSaveFilePath);
			this.saveFileCorrupt = !this.loadedFromSave;
			if (!this.loadedFromSave)
			{
				SaveLoader.SetActiveSaveFilePath(null);
				if (this.mustRestartOnFail)
				{
					this.MoveCorruptFile(activeSaveFilePath);
					Sim.Shutdown();
					App.LoadScene("frontend");
					return;
				}
			}
		}
		if (!this.loadedFromSave)
		{
			Sim.Shutdown();
			if (!string.IsNullOrEmpty(activeSaveFilePath))
			{
				DebugUtil.LogArgs(new object[] { "Couldn't load [" + activeSaveFilePath + "]" });
			}
			if (this.saveFileCorrupt)
			{
				this.MoveCorruptFile(activeSaveFilePath);
			}
			int num = 0;
			bool flag = WorldGen.CanLoad(WorldGen.GetSIMSaveFilename(num));
			if (!flag || !this.LoadFromWorldGen())
			{
				DebugUtil.LogWarningArgs(new object[] { "Couldn't start new game with current world gen, moving file" });
				if (flag)
				{
					KMonoBehaviour.isLoadingScene = true;
					while (FileSystem.FileExists(WorldGen.GetSIMSaveFilename(num)))
					{
						this.MoveCorruptFile(WorldGen.GetSIMSaveFilename(num));
						num++;
					}
				}
				App.LoadScene("frontend");
			}
		}
	}

	// Token: 0x06004295 RID: 17045 RVA: 0x00177304 File Offset: 0x00175504
	private static void CompressContents(BinaryWriter fileWriter, byte[] uncompressed, int length)
	{
		using (ZlibStream zlibStream = new ZlibStream(fileWriter.BaseStream, CompressionMode.Compress, Ionic.Zlib.CompressionLevel.BestSpeed))
		{
			zlibStream.Write(uncompressed, 0, length);
			zlibStream.Flush();
		}
	}

	// Token: 0x06004296 RID: 17046 RVA: 0x0017734C File Offset: 0x0017554C
	private byte[] FloatToBytes(float[] floats)
	{
		byte[] array = new byte[floats.Length * 4];
		Buffer.BlockCopy(floats, 0, array, 0, array.Length);
		return array;
	}

	// Token: 0x06004297 RID: 17047 RVA: 0x00177371 File Offset: 0x00175571
	private static byte[] DecompressContents(byte[] compressed)
	{
		return ZlibStream.UncompressBuffer(compressed);
	}

	// Token: 0x06004298 RID: 17048 RVA: 0x0017737C File Offset: 0x0017557C
	private float[] BytesToFloat(byte[] bytes)
	{
		float[] array = new float[bytes.Length / 4];
		Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
		return array;
	}

	// Token: 0x06004299 RID: 17049 RVA: 0x001773A4 File Offset: 0x001755A4
	private SaveFileRoot PrepSaveFile()
	{
		SaveFileRoot saveFileRoot = new SaveFileRoot();
		saveFileRoot.WidthInCells = Grid.WidthInCells;
		saveFileRoot.HeightInCells = Grid.HeightInCells;
		saveFileRoot.streamed["GridVisible"] = Grid.Visible;
		saveFileRoot.streamed["GridSpawnable"] = Grid.Spawnable;
		saveFileRoot.streamed["GridDamage"] = this.FloatToBytes(Grid.Damage);
		Global.Instance.modManager.SendMetricsEvent();
		saveFileRoot.active_mods = new List<Label>();
		foreach (Mod mod in Global.Instance.modManager.mods)
		{
			if (mod.IsEnabledForActiveDlc())
			{
				saveFileRoot.active_mods.Add(mod.label);
			}
		}
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				Camera.main.transform.parent.GetComponent<CameraController>().Save(binaryWriter);
			}
			saveFileRoot.streamed["Camera"] = memoryStream.ToArray();
		}
		return saveFileRoot;
	}

	// Token: 0x0600429A RID: 17050 RVA: 0x00177500 File Offset: 0x00175700
	private void Save(BinaryWriter writer)
	{
		writer.WriteKleiString("world");
		Serializer.Serialize(this.PrepSaveFile(), writer);
		Game.SaveSettings(writer);
		Sim.Save(writer, 0, 0);
		this.saveManager.Save(writer);
		Game.Instance.Save(writer);
	}

	// Token: 0x0600429B RID: 17051 RVA: 0x00177540 File Offset: 0x00175740
	private bool Load(IReader reader)
	{
		global::Debug.Assert(reader.ReadKleiString() == "world");
		Deserializer deserializer = new Deserializer(reader);
		SaveFileRoot saveFileRoot = new SaveFileRoot();
		deserializer.Deserialize(saveFileRoot);
		if ((this.GameInfo.saveMajorVersion == 7 || this.GameInfo.saveMinorVersion < 8) && saveFileRoot.requiredMods != null)
		{
			saveFileRoot.active_mods = new List<Label>();
			foreach (ModInfo modInfo in saveFileRoot.requiredMods)
			{
				saveFileRoot.active_mods.Add(new Label
				{
					id = modInfo.assetID,
					version = (long)modInfo.lastModifiedTime,
					distribution_platform = Label.DistributionPlatform.Steam,
					title = modInfo.description
				});
			}
			saveFileRoot.requiredMods.Clear();
		}
		KMod.Manager modManager = Global.Instance.modManager;
		modManager.Load(Content.LayerableFiles);
		if (!modManager.MatchFootprint(saveFileRoot.active_mods, Content.LayerableFiles | Content.Strings | Content.DLL | Content.Translation | Content.Animation))
		{
			DebugUtil.LogWarningArgs(new object[] { "Mod footprint of save file doesn't match current mod configuration" });
		}
		Global.Instance.modManager.SendMetricsEvent();
		WorldGen.LoadSettings(false);
		CustomGameSettings.Instance.LoadClusters();
		if (this.GameInfo.clusterId == null)
		{
			SaveGame.GameInfo gameInfo = this.GameInfo;
			if (!string.IsNullOrEmpty(saveFileRoot.clusterID))
			{
				gameInfo.clusterId = saveFileRoot.clusterID;
			}
			else
			{
				try
				{
					gameInfo.clusterId = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.ClusterLayout).id;
				}
				catch
				{
					gameInfo.clusterId = WorldGenSettings.ClusterDefaultName;
					CustomGameSettings.Instance.SetQualitySetting(CustomGameSettingConfigs.ClusterLayout, gameInfo.clusterId);
				}
			}
			this.GameInfo = gameInfo;
		}
		Game.clusterId = this.GameInfo.clusterId;
		Game.LoadSettings(deserializer);
		GridSettings.Reset(saveFileRoot.WidthInCells, saveFileRoot.HeightInCells);
		if (Application.isPlaying)
		{
			Singleton<KBatchedAnimUpdater>.Instance.InitializeGrid();
		}
		Sim.SIM_Initialize(new Sim.GAME_MessageHandler(Sim.DLL_MessageHandler));
		SimMessages.CreateSimElementsTable(ElementLoader.elements);
		Sim.AllocateCells(saveFileRoot.WidthInCells, saveFileRoot.HeightInCells, false);
		SimMessages.CreateDiseaseTable(Db.Get().Diseases);
		Sim.HandleMessage(SimMessageHashes.ClearUnoccupiedCells, 0, null);
		IReader reader2;
		if (saveFileRoot.streamed.ContainsKey("Sim"))
		{
			reader2 = new FastReader(saveFileRoot.streamed["Sim"]);
		}
		else
		{
			reader2 = reader;
		}
		if (Sim.LoadWorld(reader2) != 0)
		{
			DebugUtil.LogWarningArgs(new object[] { "\n--- Error loading save ---\nSimDLL found bad data\n" });
			Sim.Shutdown();
			return false;
		}
		Sim.Start();
		SceneInitializer.Instance.PostLoadPrefabs();
		this.mustRestartOnFail = true;
		if (!this.saveManager.Load(reader))
		{
			Sim.Shutdown();
			DebugUtil.LogWarningArgs(new object[] { "\n--- Error loading save ---\n" });
			SaveLoader.SetActiveSaveFilePath(null);
			return false;
		}
		Grid.Visible = saveFileRoot.streamed["GridVisible"];
		if (saveFileRoot.streamed.ContainsKey("GridSpawnable"))
		{
			Grid.Spawnable = saveFileRoot.streamed["GridSpawnable"];
		}
		Grid.Damage = this.BytesToFloat(saveFileRoot.streamed["GridDamage"]);
		Game.Instance.Load(deserializer);
		CameraSaveData.Load(new FastReader(saveFileRoot.streamed["Camera"]));
		ClusterManager.Instance.InitializeWorldGrid();
		SimMessages.DefineWorldOffsets(ClusterManager.Instance.WorldContainers.Select((WorldContainer container) => new SimMessages.WorldOffsetData
		{
			worldOffsetX = container.WorldOffset.x,
			worldOffsetY = container.WorldOffset.y,
			worldSizeX = container.WorldSize.x,
			worldSizeY = container.WorldSize.y
		}).ToList<SimMessages.WorldOffsetData>());
		return true;
	}

	// Token: 0x0600429C RID: 17052 RVA: 0x001778EC File Offset: 0x00175AEC
	public static string GetSavePrefix()
	{
		return System.IO.Path.Combine(global::Util.RootFolder(), string.Format("{0}{1}", "save_files", System.IO.Path.DirectorySeparatorChar));
	}

	// Token: 0x0600429D RID: 17053 RVA: 0x00177914 File Offset: 0x00175B14
	public static string GetCloudSavePrefix()
	{
		string text = System.IO.Path.Combine(global::Util.RootFolder(), string.Format("{0}{1}", "cloud_save_files", System.IO.Path.DirectorySeparatorChar));
		string userID = SaveLoader.GetUserID();
		if (string.IsNullOrEmpty(userID))
		{
			return null;
		}
		text = System.IO.Path.Combine(text, userID);
		if (!System.IO.Directory.Exists(text))
		{
			System.IO.Directory.CreateDirectory(text);
		}
		return text;
	}

	// Token: 0x0600429E RID: 17054 RVA: 0x00177970 File Offset: 0x00175B70
	public static string GetSavePrefixAndCreateFolder()
	{
		string savePrefix = SaveLoader.GetSavePrefix();
		if (!System.IO.Directory.Exists(savePrefix))
		{
			System.IO.Directory.CreateDirectory(savePrefix);
		}
		return savePrefix;
	}

	// Token: 0x0600429F RID: 17055 RVA: 0x00177994 File Offset: 0x00175B94
	public static string GetUserID()
	{
		DistributionPlatform.User localUser = DistributionPlatform.Inst.LocalUser;
		if (localUser == null)
		{
			return null;
		}
		return localUser.Id.ToString();
	}

	// Token: 0x060042A0 RID: 17056 RVA: 0x001779BC File Offset: 0x00175BBC
	public static string GetNextUsableSavePath(string filename)
	{
		int num = 0;
		string text = System.IO.Path.ChangeExtension(filename, null);
		while (File.Exists(filename))
		{
			filename = SaveScreen.GetValidSaveFilename(string.Format("{0} ({1})", text, num));
			num++;
		}
		return filename;
	}

	// Token: 0x060042A1 RID: 17057 RVA: 0x001779FA File Offset: 0x00175BFA
	public static string GetOriginalSaveFileName(string filename)
	{
		if (!filename.Contains("/") && !filename.Contains("\\"))
		{
			return filename;
		}
		filename.Replace('\\', '/');
		return System.IO.Path.GetFileName(filename);
	}

	// Token: 0x060042A2 RID: 17058 RVA: 0x00177A29 File Offset: 0x00175C29
	public static bool IsSaveAuto(string filename)
	{
		filename = filename.Replace('\\', '/');
		return filename.Contains("/auto_save/");
	}

	// Token: 0x060042A3 RID: 17059 RVA: 0x00177A42 File Offset: 0x00175C42
	public static bool IsSaveLocal(string filename)
	{
		filename = filename.Replace('\\', '/');
		return filename.Contains("/save_files/");
	}

	// Token: 0x060042A4 RID: 17060 RVA: 0x00177A5B File Offset: 0x00175C5B
	public static bool IsSaveCloud(string filename)
	{
		filename = filename.Replace('\\', '/');
		return filename.Contains("/cloud_save_files/");
	}

	// Token: 0x060042A5 RID: 17061 RVA: 0x00177A74 File Offset: 0x00175C74
	public static string GetAutoSavePrefix()
	{
		string text = System.IO.Path.Combine(SaveLoader.GetSavePrefixAndCreateFolder(), string.Format("{0}{1}", "auto_save", System.IO.Path.DirectorySeparatorChar));
		if (!System.IO.Directory.Exists(text))
		{
			System.IO.Directory.CreateDirectory(text);
		}
		return text;
	}

	// Token: 0x060042A6 RID: 17062 RVA: 0x00177AB5 File Offset: 0x00175CB5
	public static void SetActiveSaveFilePath(string path)
	{
		KPlayerPrefs.SetString("SaveFilenameKey/", path);
	}

	// Token: 0x060042A7 RID: 17063 RVA: 0x00177AC2 File Offset: 0x00175CC2
	public static string GetActiveSaveFilePath()
	{
		return KPlayerPrefs.GetString("SaveFilenameKey/");
	}

	// Token: 0x060042A8 RID: 17064 RVA: 0x00177AD0 File Offset: 0x00175CD0
	public static string GetActiveAutoSavePath()
	{
		string activeSaveFilePath = SaveLoader.GetActiveSaveFilePath();
		if (activeSaveFilePath == null)
		{
			return SaveLoader.GetAutoSavePrefix();
		}
		return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(activeSaveFilePath), "auto_save");
	}

	// Token: 0x060042A9 RID: 17065 RVA: 0x00177AFC File Offset: 0x00175CFC
	public static string GetAutosaveFilePath()
	{
		return SaveLoader.GetAutoSavePrefix() + "AutoSave Cycle 1.sav";
	}

	// Token: 0x060042AA RID: 17066 RVA: 0x00177B10 File Offset: 0x00175D10
	public static string GetActiveSaveColonyFolder()
	{
		string text = SaveLoader.GetActiveSaveFolder();
		if (text == null)
		{
			text = System.IO.Path.Combine(SaveLoader.GetSavePrefix(), SaveLoader.Instance.GameInfo.baseName);
		}
		return text;
	}

	// Token: 0x060042AB RID: 17067 RVA: 0x00177B44 File Offset: 0x00175D44
	public static string GetActiveSaveFolder()
	{
		string activeSaveFilePath = SaveLoader.GetActiveSaveFilePath();
		if (!string.IsNullOrEmpty(activeSaveFilePath))
		{
			return System.IO.Path.GetDirectoryName(activeSaveFilePath);
		}
		return null;
	}

	// Token: 0x060042AC RID: 17068 RVA: 0x00177B68 File Offset: 0x00175D68
	public static List<SaveLoader.SaveFileEntry> GetSaveFiles(string save_dir, bool sort, SearchOption search = SearchOption.AllDirectories)
	{
		List<SaveLoader.SaveFileEntry> list = new List<SaveLoader.SaveFileEntry>();
		if (string.IsNullOrEmpty(save_dir))
		{
			return list;
		}
		try
		{
			if (!System.IO.Directory.Exists(save_dir))
			{
				System.IO.Directory.CreateDirectory(save_dir);
			}
			foreach (string text in System.IO.Directory.GetFiles(save_dir, "*.sav", search))
			{
				try
				{
					System.DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(text);
					SaveLoader.SaveFileEntry saveFileEntry = new SaveLoader.SaveFileEntry
					{
						path = text,
						timeStamp = lastWriteTimeUtc
					};
					list.Add(saveFileEntry);
				}
				catch (Exception ex)
				{
					global::Debug.LogWarning("Problem reading file: " + text + "\n" + ex.ToString());
				}
			}
			if (sort)
			{
				list.Sort((SaveLoader.SaveFileEntry x, SaveLoader.SaveFileEntry y) => y.timeStamp.CompareTo(x.timeStamp));
			}
		}
		catch (Exception ex2)
		{
			string text2 = null;
			if (ex2 is UnauthorizedAccessException)
			{
				text2 = string.Format(UI.FRONTEND.SUPPORTWARNINGS.SAVE_DIRECTORY_READ_ONLY, save_dir);
			}
			else if (ex2 is IOException)
			{
				text2 = string.Format(UI.FRONTEND.SUPPORTWARNINGS.SAVE_DIRECTORY_INSUFFICIENT_SPACE, save_dir);
			}
			if (text2 == null)
			{
				throw ex2;
			}
			GameObject gameObject = ((FrontEndManager.Instance == null) ? GameScreenManager.Instance.ssOverlayCanvas : FrontEndManager.Instance.gameObject);
			global::Util.KInstantiateUI(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, gameObject, true).GetComponent<ConfirmDialogScreen>().PopupConfirmDialog(text2, null, null, null, null, null, null, null, null);
		}
		return list;
	}

	// Token: 0x060042AD RID: 17069 RVA: 0x00177CE8 File Offset: 0x00175EE8
	public static List<SaveLoader.SaveFileEntry> GetAllFiles(bool sort, SaveLoader.SaveType type = SaveLoader.SaveType.both)
	{
		switch (type)
		{
		case SaveLoader.SaveType.local:
			return SaveLoader.GetSaveFiles(SaveLoader.GetSavePrefixAndCreateFolder(), sort, SearchOption.AllDirectories);
		case SaveLoader.SaveType.cloud:
			return SaveLoader.GetSaveFiles(SaveLoader.GetCloudSavePrefix(), sort, SearchOption.AllDirectories);
		case SaveLoader.SaveType.both:
		{
			List<SaveLoader.SaveFileEntry> saveFiles = SaveLoader.GetSaveFiles(SaveLoader.GetSavePrefixAndCreateFolder(), false, SearchOption.AllDirectories);
			List<SaveLoader.SaveFileEntry> saveFiles2 = SaveLoader.GetSaveFiles(SaveLoader.GetCloudSavePrefix(), false, SearchOption.AllDirectories);
			saveFiles.AddRange(saveFiles2);
			if (sort)
			{
				saveFiles.Sort((SaveLoader.SaveFileEntry x, SaveLoader.SaveFileEntry y) => y.timeStamp.CompareTo(x.timeStamp));
			}
			return saveFiles;
		}
		default:
			return new List<SaveLoader.SaveFileEntry>();
		}
	}

	// Token: 0x060042AE RID: 17070 RVA: 0x00177D73 File Offset: 0x00175F73
	public static List<SaveLoader.SaveFileEntry> GetAllColonyFiles(bool sort, SearchOption search = SearchOption.TopDirectoryOnly)
	{
		return SaveLoader.GetSaveFiles(SaveLoader.GetActiveSaveColonyFolder(), sort, search);
	}

	// Token: 0x060042AF RID: 17071 RVA: 0x00177D81 File Offset: 0x00175F81
	public static bool GetCloudSavesDefault()
	{
		return !(SaveLoader.GetCloudSavesDefaultPref() == "Disabled");
	}

	// Token: 0x060042B0 RID: 17072 RVA: 0x00177D98 File Offset: 0x00175F98
	public static string GetCloudSavesDefaultPref()
	{
		string text = KPlayerPrefs.GetString("SavesDefaultToCloud", "Enabled");
		if (text != "Enabled" && text != "Disabled")
		{
			text = "Enabled";
		}
		return text;
	}

	// Token: 0x060042B1 RID: 17073 RVA: 0x00177DD6 File Offset: 0x00175FD6
	public static void SetCloudSavesDefault(bool value)
	{
		SaveLoader.SetCloudSavesDefaultPref(value ? "Enabled" : "Disabled");
	}

	// Token: 0x060042B2 RID: 17074 RVA: 0x00177DEC File Offset: 0x00175FEC
	public static void SetCloudSavesDefaultPref(string pref)
	{
		if (pref != "Enabled" && pref != "Disabled")
		{
			global::Debug.LogWarning("Ignoring cloud saves default pref `" + pref + "` as it's not valid, expected `Enabled` or `Disabled`");
			return;
		}
		KPlayerPrefs.SetString("SavesDefaultToCloud", pref);
	}

	// Token: 0x060042B3 RID: 17075 RVA: 0x00177E29 File Offset: 0x00176029
	public static bool GetCloudSavesAvailable()
	{
		return !string.IsNullOrEmpty(SaveLoader.GetUserID()) && SaveLoader.GetCloudSavePrefix() != null;
	}

	// Token: 0x060042B4 RID: 17076 RVA: 0x00177E44 File Offset: 0x00176044
	public static string GetLatestSaveFile()
	{
		List<SaveLoader.SaveFileEntry> allFiles = SaveLoader.GetAllFiles(true, SaveLoader.SaveType.both);
		if (allFiles.Count == 0)
		{
			return null;
		}
		return allFiles[0].path;
	}

	// Token: 0x060042B5 RID: 17077 RVA: 0x00177E70 File Offset: 0x00176070
	public static string GetLatestSaveForCurrentDLC()
	{
		List<SaveLoader.SaveFileEntry> allFiles = SaveLoader.GetAllFiles(true, SaveLoader.SaveType.both);
		for (int i = 0; i < allFiles.Count; i++)
		{
			global::Tuple<SaveGame.Header, SaveGame.GameInfo> fileInfo = SaveGame.GetFileInfo(allFiles[i].path);
			if (fileInfo != null)
			{
				SaveGame.Header first = fileInfo.first;
				SaveGame.GameInfo second = fileInfo.second;
				if (second.saveMajorVersion >= 7 && DlcManager.GetHighestActiveDlcId() == second.dlcId)
				{
					return allFiles[i].path;
				}
			}
		}
		return null;
	}

	// Token: 0x060042B6 RID: 17078 RVA: 0x00177EE8 File Offset: 0x001760E8
	public void InitialSave()
	{
		string text = SaveLoader.GetActiveSaveFilePath();
		if (string.IsNullOrEmpty(text))
		{
			text = SaveLoader.GetAutosaveFilePath();
		}
		else if (!text.Contains(".sav"))
		{
			text += ".sav";
		}
		this.Save(text, false, true);
	}

	// Token: 0x060042B7 RID: 17079 RVA: 0x00177F30 File Offset: 0x00176130
	public string Save(string filename, bool isAutoSave = false, bool updateSavePointer = true)
	{
		KSerialization.Manager.Clear();
		string directoryName = System.IO.Path.GetDirectoryName(filename);
		try
		{
			if (directoryName != null && !System.IO.Directory.Exists(directoryName))
			{
				System.IO.Directory.CreateDirectory(directoryName);
			}
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning("Problem creating save folder for " + filename + "!\n" + ex.ToString());
		}
		this.ReportSaveMetrics(isAutoSave);
		RetireColonyUtility.SaveColonySummaryData();
		if (isAutoSave && !GenericGameSettings.instance.keepAllAutosaves)
		{
			List<SaveLoader.SaveFileEntry> saveFiles = SaveLoader.GetSaveFiles(SaveLoader.GetActiveAutoSavePath(), true, SearchOption.AllDirectories);
			List<string> list = new List<string>();
			foreach (SaveLoader.SaveFileEntry saveFileEntry in saveFiles)
			{
				global::Tuple<SaveGame.Header, SaveGame.GameInfo> fileInfo = SaveGame.GetFileInfo(saveFileEntry.path);
				if (fileInfo != null && SaveGame.GetSaveUniqueID(fileInfo.second) == SaveLoader.Instance.GameInfo.colonyGuid.ToString())
				{
					list.Add(saveFileEntry.path);
				}
			}
			for (int i = list.Count - 1; i >= 9; i--)
			{
				string text = list[i];
				try
				{
					global::Debug.Log("Deleting old autosave: " + text);
					File.Delete(text);
				}
				catch (Exception ex2)
				{
					global::Debug.LogWarning("Problem deleting autosave: " + text + "\n" + ex2.ToString());
				}
				string text2 = System.IO.Path.ChangeExtension(text, ".png");
				try
				{
					if (File.Exists(text2))
					{
						File.Delete(text2);
					}
				}
				catch (Exception ex3)
				{
					global::Debug.LogWarning("Problem deleting autosave screenshot: " + text2 + "\n" + ex3.ToString());
				}
			}
		}
		using (MemoryStream memoryStream = new MemoryStream((int)((float)this.lastUncompressedSize * 1.1f)))
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				this.Save(binaryWriter);
				this.lastUncompressedSize = (int)memoryStream.Length;
				try
				{
					using (BinaryWriter binaryWriter2 = new BinaryWriter(File.Open(filename, FileMode.Create)))
					{
						SaveGame.Header header;
						byte[] saveHeader = SaveGame.Instance.GetSaveHeader(isAutoSave, this.compressSaveData, out header);
						binaryWriter2.Write(header.buildVersion);
						binaryWriter2.Write(header.headerSize);
						binaryWriter2.Write(header.headerVersion);
						binaryWriter2.Write(header.compression);
						binaryWriter2.Write(saveHeader);
						KSerialization.Manager.SerializeDirectory(binaryWriter2);
						if (this.compressSaveData)
						{
							SaveLoader.CompressContents(binaryWriter2, memoryStream.GetBuffer(), (int)memoryStream.Length);
						}
						else
						{
							binaryWriter2.Write(memoryStream.ToArray());
						}
						KCrashReporter.MOST_RECENT_SAVEFILE = filename;
						Stats.Print();
					}
				}
				catch (Exception ex4)
				{
					if (ex4 is UnauthorizedAccessException)
					{
						DebugUtil.LogArgs(new object[] { "UnauthorizedAccessException for " + filename });
						((ConfirmDialogScreen)GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay)).PopupConfirmDialog(string.Format(UI.CRASHSCREEN.SAVEFAILED, "Unauthorized Access Exception"), null, null, null, null, null, null, null, null);
						return SaveLoader.GetActiveSaveFilePath();
					}
					if (ex4 is IOException)
					{
						DebugUtil.LogArgs(new object[] { "IOException (probably out of disk space) for " + filename });
						((ConfirmDialogScreen)GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay)).PopupConfirmDialog(string.Format(UI.CRASHSCREEN.SAVEFAILED, "IOException. You may not have enough free space!"), null, null, null, null, null, null, null, null);
						return SaveLoader.GetActiveSaveFilePath();
					}
					throw ex4;
				}
			}
		}
		if (updateSavePointer)
		{
			SaveLoader.SetActiveSaveFilePath(filename);
		}
		Game.Instance.timelapser.SaveColonyPreview(filename);
		DebugUtil.LogArgs(new object[]
		{
			"Saved to",
			"[" + filename + "]"
		});
		GC.Collect();
		return filename;
	}

	// Token: 0x060042B8 RID: 17080 RVA: 0x001783D8 File Offset: 0x001765D8
	public static SaveGame.GameInfo LoadHeader(string filename, out SaveGame.Header header)
	{
		byte[] array = new byte[512];
		SaveGame.GameInfo header2;
		using (FileStream fileStream = File.OpenRead(filename))
		{
			fileStream.Read(array, 0, 512);
			header2 = SaveGame.GetHeader(new FastReader(array), out header, filename);
		}
		return header2;
	}

	// Token: 0x060042B9 RID: 17081 RVA: 0x00178430 File Offset: 0x00176630
	public bool Load(string filename)
	{
		SaveLoader.SetActiveSaveFilePath(filename);
		try
		{
			KSerialization.Manager.Clear();
			byte[] array = File.ReadAllBytes(filename);
			IReader reader = new FastReader(array);
			SaveGame.Header header;
			this.GameInfo = SaveGame.GetHeader(reader, out header, filename);
			DebugUtil.LogArgs(new object[] { string.Format("Loading save file: {4}\n headerVersion:{0}, buildVersion:{1}, headerSize:{2}, IsCompressed:{3}", new object[] { header.headerVersion, header.buildVersion, header.headerSize, header.IsCompressed, filename }) });
			DebugUtil.LogArgs(new object[] { string.Format("GameInfo loaded from save header:\n  numberOfCycles:{0},\n  numberOfDuplicants:{1},\n  baseName:{2},\n  isAutoSave:{3},\n  originalSaveName:{4},\n  clusterId:{5},\n  worldTraits:{6},\n  colonyGuid:{7},\n  saveVersion:{8}.{9}", new object[]
			{
				this.GameInfo.numberOfCycles,
				this.GameInfo.numberOfDuplicants,
				this.GameInfo.baseName,
				this.GameInfo.isAutoSave,
				this.GameInfo.originalSaveName,
				this.GameInfo.clusterId,
				(this.GameInfo.worldTraits != null && this.GameInfo.worldTraits.Length != 0) ? string.Join(", ", this.GameInfo.worldTraits) : "<i>none</i>",
				this.GameInfo.colonyGuid,
				this.GameInfo.saveMajorVersion,
				this.GameInfo.saveMinorVersion
			}) });
			string originalSaveName = this.GameInfo.originalSaveName;
			if (originalSaveName.Contains("/") || originalSaveName.Contains("\\"))
			{
				string originalSaveFileName = SaveLoader.GetOriginalSaveFileName(originalSaveName);
				SaveGame.GameInfo gameInfo = this.GameInfo;
				gameInfo.originalSaveName = originalSaveFileName;
				this.GameInfo = gameInfo;
				global::Debug.Log(string.Concat(new string[]
				{
					"Migration / Save originalSaveName updated from: `",
					originalSaveName,
					"` => `",
					this.GameInfo.originalSaveName,
					"`"
				}));
			}
			if (this.GameInfo.saveMajorVersion == 7 && this.GameInfo.saveMinorVersion < 4)
			{
				Helper.SetTypeInfoMask((SerializationTypeInfo)191);
			}
			KSerialization.Manager.DeserializeDirectory(reader);
			if (header.IsCompressed)
			{
				int num = array.Length - reader.Position;
				byte[] array2 = new byte[num];
				Array.Copy(array, reader.Position, array2, 0, num);
				byte[] array3 = SaveLoader.DecompressContents(array2);
				this.lastUncompressedSize = array3.Length;
				IReader reader2 = new FastReader(array3);
				this.Load(reader2);
			}
			else
			{
				this.lastUncompressedSize = array.Length;
				this.Load(reader);
			}
			KCrashReporter.MOST_RECENT_SAVEFILE = filename;
			if (this.GameInfo.isAutoSave && !string.IsNullOrEmpty(this.GameInfo.originalSaveName))
			{
				string originalSaveFileName2 = SaveLoader.GetOriginalSaveFileName(this.GameInfo.originalSaveName);
				string text;
				if (SaveLoader.IsSaveCloud(filename))
				{
					string cloudSavePrefix = SaveLoader.GetCloudSavePrefix();
					if (cloudSavePrefix != null)
					{
						text = System.IO.Path.Combine(cloudSavePrefix, this.GameInfo.baseName, originalSaveFileName2);
					}
					else
					{
						text = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filename).Replace("auto_save", ""), this.GameInfo.baseName, originalSaveFileName2);
					}
				}
				else
				{
					text = System.IO.Path.Combine(SaveLoader.GetSavePrefix(), this.GameInfo.baseName, originalSaveFileName2);
				}
				if (text != null)
				{
					SaveLoader.SetActiveSaveFilePath(text);
				}
			}
		}
		catch (Exception ex)
		{
			DebugUtil.LogWarningArgs(new object[] { "\n--- Error loading save ---\n" + ex.Message + "\n" + ex.StackTrace });
			Sim.Shutdown();
			SaveLoader.SetActiveSaveFilePath(null);
			return false;
		}
		Stats.Print();
		DebugUtil.LogArgs(new object[]
		{
			"Loaded",
			"[" + filename + "]"
		});
		DebugUtil.LogArgs(new object[]
		{
			"World Seeds",
			string.Concat(new string[]
			{
				"[",
				this.clusterDetailSave.globalWorldSeed.ToString(),
				"/",
				this.clusterDetailSave.globalWorldLayoutSeed.ToString(),
				"/",
				this.clusterDetailSave.globalTerrainSeed.ToString(),
				"/",
				this.clusterDetailSave.globalNoiseSeed.ToString(),
				"]"
			})
		});
		GC.Collect();
		return true;
	}

	// Token: 0x060042BA RID: 17082 RVA: 0x001788B0 File Offset: 0x00176AB0
	public bool LoadFromWorldGen()
	{
		DebugUtil.LogArgs(new object[] { "Attempting to start a new game with current world gen" });
		WorldGen.LoadSettings(false);
		this.m_clusterLayout = Cluster.Load();
		ListPool<SimSaveFileStructure, SaveLoader>.PooledList pooledList = ListPool<SimSaveFileStructure, SaveLoader>.Allocate();
		this.m_clusterLayout.LoadClusterLayoutSim(pooledList);
		SaveGame.GameInfo gameInfo = this.GameInfo;
		gameInfo.clusterId = this.m_clusterLayout.Id;
		gameInfo.colonyGuid = Guid.NewGuid();
		this.GameInfo = gameInfo;
		if (pooledList.Count != this.m_clusterLayout.worlds.Count)
		{
			global::Debug.LogError("Attempt failed. Failed to load all worlds.");
			pooledList.Recycle();
			return false;
		}
		GridSettings.Reset(this.m_clusterLayout.size.x, this.m_clusterLayout.size.y);
		if (Application.isPlaying)
		{
			Singleton<KBatchedAnimUpdater>.Instance.InitializeGrid();
		}
		this.clusterDetailSave = new WorldDetailSave();
		foreach (SimSaveFileStructure simSaveFileStructure in pooledList)
		{
			this.clusterDetailSave.globalNoiseSeed = simSaveFileStructure.worldDetail.globalNoiseSeed;
			this.clusterDetailSave.globalTerrainSeed = simSaveFileStructure.worldDetail.globalTerrainSeed;
			this.clusterDetailSave.globalWorldLayoutSeed = simSaveFileStructure.worldDetail.globalWorldLayoutSeed;
			this.clusterDetailSave.globalWorldSeed = simSaveFileStructure.worldDetail.globalWorldSeed;
			Vector2 vector = Grid.CellToPos2D(Grid.PosToCell(new Vector2I(simSaveFileStructure.x, simSaveFileStructure.y)));
			foreach (WorldDetailSave.OverworldCell overworldCell in simSaveFileStructure.worldDetail.overworldCells)
			{
				for (int num = 0; num != overworldCell.poly.Vertices.Count; num++)
				{
					List<Vector2> vertices = overworldCell.poly.Vertices;
					int num2 = num;
					vertices[num2] += vector;
				}
				overworldCell.poly.RefreshBounds();
			}
			this.clusterDetailSave.overworldCells.AddRange(simSaveFileStructure.worldDetail.overworldCells);
		}
		Sim.SIM_Initialize(new Sim.GAME_MessageHandler(Sim.DLL_MessageHandler));
		SimMessages.CreateSimElementsTable(ElementLoader.elements);
		Sim.AllocateCells(this.m_clusterLayout.size.x, this.m_clusterLayout.size.y, false);
		SimMessages.DefineWorldOffsets(this.m_clusterLayout.worlds.Select((WorldGen world) => new SimMessages.WorldOffsetData
		{
			worldOffsetX = world.WorldOffset.x,
			worldOffsetY = world.WorldOffset.y,
			worldSizeX = world.WorldSize.x,
			worldSizeY = world.WorldSize.y
		}).ToList<SimMessages.WorldOffsetData>());
		SimMessages.CreateDiseaseTable(Db.Get().Diseases);
		Sim.HandleMessage(SimMessageHashes.ClearUnoccupiedCells, 0, null);
		try
		{
			foreach (SimSaveFileStructure simSaveFileStructure2 in pooledList)
			{
				FastReader fastReader = new FastReader(simSaveFileStructure2.Sim);
				if (Sim.Load(fastReader) != 0)
				{
					DebugUtil.LogWarningArgs(new object[] { "\n--- Error loading save ---\nSimDLL found bad data\n" });
					Sim.Shutdown();
					pooledList.Recycle();
					return false;
				}
			}
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning("--- Error loading Sim FROM NEW WORLDGEN ---" + ex.Message + "\n" + ex.StackTrace);
			Sim.Shutdown();
			pooledList.Recycle();
			return false;
		}
		global::Debug.Log("Attempt success");
		Sim.Start();
		SceneInitializer.Instance.PostLoadPrefabs();
		SceneInitializer.Instance.NewSaveGamePrefab();
		this.cachedGSD = this.m_clusterLayout.currentWorld.SpawnData;
		this.OnWorldGenComplete.Signal(this.m_clusterLayout);
		OniMetrics.LogEvent(OniMetrics.Event.NewSave, "NewGame", true);
		StoryManager.Instance.InitialSaveSetup();
		ThreadedHttps<KleiMetrics>.Instance.IncrementGameCount();
		OniMetrics.SendEvent(OniMetrics.Event.NewSave, "New Save");
		pooledList.Recycle();
		return true;
	}

	// Token: 0x170004C4 RID: 1220
	// (get) Token: 0x060042BB RID: 17083 RVA: 0x00178CFC File Offset: 0x00176EFC
	// (set) Token: 0x060042BC RID: 17084 RVA: 0x00178D04 File Offset: 0x00176F04
	public GameSpawnData cachedGSD { get; private set; }

	// Token: 0x170004C5 RID: 1221
	// (get) Token: 0x060042BD RID: 17085 RVA: 0x00178D0D File Offset: 0x00176F0D
	// (set) Token: 0x060042BE RID: 17086 RVA: 0x00178D15 File Offset: 0x00176F15
	public WorldDetailSave clusterDetailSave { get; private set; }

	// Token: 0x060042BF RID: 17087 RVA: 0x00178D1E File Offset: 0x00176F1E
	public void SetWorldDetail(WorldDetailSave worldDetail)
	{
		this.clusterDetailSave = worldDetail;
	}

	// Token: 0x060042C0 RID: 17088 RVA: 0x00178D28 File Offset: 0x00176F28
	private void ReportSaveMetrics(bool is_auto_save)
	{
		if (ThreadedHttps<KleiMetrics>.Instance == null || !ThreadedHttps<KleiMetrics>.Instance.enabled || this.saveManager == null)
		{
			return;
		}
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary[GameClock.NewCycleKey] = GameClock.Instance.GetCycle() + 1;
		dictionary["IsAutoSave"] = is_auto_save;
		dictionary["SavedPrefabs"] = this.GetSavedPrefabMetrics();
		dictionary["ResourcesAccessible"] = this.GetWorldInventoryMetrics();
		dictionary["MinionMetrics"] = this.GetMinionMetrics();
		if (is_auto_save)
		{
			dictionary["DailyReport"] = this.GetDailyReportMetrics();
			dictionary["PerformanceMeasurements"] = this.GetPerformanceMeasurements();
			dictionary["AverageFrameTime"] = this.GetFrameTime();
		}
		dictionary["CustomGameSettings"] = CustomGameSettings.Instance.GetSettingsForMetrics();
		ThreadedHttps<KleiMetrics>.Instance.SendEvent(dictionary, "ReportSaveMetrics");
	}

	// Token: 0x060042C1 RID: 17089 RVA: 0x00178E20 File Offset: 0x00177020
	private List<SaveLoader.MinionMetricsData> GetMinionMetrics()
	{
		List<SaveLoader.MinionMetricsData> list = new List<SaveLoader.MinionMetricsData>();
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			if (!(minionIdentity == null))
			{
				Amounts amounts = minionIdentity.gameObject.GetComponent<Modifiers>().amounts;
				List<SaveLoader.MinionAttrFloatData> list2 = new List<SaveLoader.MinionAttrFloatData>(amounts.Count);
				foreach (AmountInstance amountInstance in amounts)
				{
					float value = amountInstance.value;
					if (!float.IsNaN(value) && !float.IsInfinity(value))
					{
						list2.Add(new SaveLoader.MinionAttrFloatData
						{
							Name = amountInstance.modifier.Id,
							Value = amountInstance.value
						});
					}
				}
				MinionResume component = minionIdentity.gameObject.GetComponent<MinionResume>();
				float totalExperienceGained = component.TotalExperienceGained;
				List<string> list3 = new List<string>();
				foreach (KeyValuePair<string, bool> keyValuePair in component.MasteryBySkillID)
				{
					if (keyValuePair.Value)
					{
						list3.Add(keyValuePair.Key);
					}
				}
				list.Add(new SaveLoader.MinionMetricsData
				{
					Name = minionIdentity.name,
					Modifiers = list2,
					TotalExperienceGained = totalExperienceGained,
					Skills = list3
				});
			}
		}
		return list;
	}

	// Token: 0x060042C2 RID: 17090 RVA: 0x00178FF0 File Offset: 0x001771F0
	private List<SaveLoader.SavedPrefabMetricsData> GetSavedPrefabMetrics()
	{
		Dictionary<Tag, List<SaveLoadRoot>> lists = this.saveManager.GetLists();
		List<SaveLoader.SavedPrefabMetricsData> list = new List<SaveLoader.SavedPrefabMetricsData>(lists.Count);
		foreach (KeyValuePair<Tag, List<SaveLoadRoot>> keyValuePair in lists)
		{
			Tag key = keyValuePair.Key;
			List<SaveLoadRoot> value = keyValuePair.Value;
			if (value.Count > 0)
			{
				list.Add(new SaveLoader.SavedPrefabMetricsData
				{
					PrefabName = key.ToString(),
					Count = value.Count
				});
			}
		}
		return list;
	}

	// Token: 0x060042C3 RID: 17091 RVA: 0x0017909C File Offset: 0x0017729C
	private List<SaveLoader.WorldInventoryMetricsData> GetWorldInventoryMetrics()
	{
		Dictionary<Tag, float> allWorldsAccessibleAmounts = ClusterManager.Instance.GetAllWorldsAccessibleAmounts();
		List<SaveLoader.WorldInventoryMetricsData> list = new List<SaveLoader.WorldInventoryMetricsData>(allWorldsAccessibleAmounts.Count);
		foreach (KeyValuePair<Tag, float> keyValuePair in allWorldsAccessibleAmounts)
		{
			float value = keyValuePair.Value;
			if (!float.IsInfinity(value) && !float.IsNaN(value))
			{
				list.Add(new SaveLoader.WorldInventoryMetricsData
				{
					Name = keyValuePair.Key.ToString(),
					Amount = value
				});
			}
		}
		return list;
	}

	// Token: 0x060042C4 RID: 17092 RVA: 0x00179148 File Offset: 0x00177348
	private List<SaveLoader.DailyReportMetricsData> GetDailyReportMetrics()
	{
		List<SaveLoader.DailyReportMetricsData> list = new List<SaveLoader.DailyReportMetricsData>();
		int cycle = GameClock.Instance.GetCycle();
		ReportManager.DailyReport dailyReport = ReportManager.Instance.FindReport(cycle);
		if (dailyReport != null)
		{
			foreach (ReportManager.ReportEntry reportEntry in dailyReport.reportEntries)
			{
				SaveLoader.DailyReportMetricsData dailyReportMetricsData = default(SaveLoader.DailyReportMetricsData);
				dailyReportMetricsData.Name = reportEntry.reportType.ToString();
				if (!float.IsInfinity(reportEntry.Net) && !float.IsNaN(reportEntry.Net))
				{
					dailyReportMetricsData.Net = new float?(reportEntry.Net);
				}
				if (SaveLoader.force_infinity)
				{
					dailyReportMetricsData.Net = null;
				}
				if (!float.IsInfinity(reportEntry.Positive) && !float.IsNaN(reportEntry.Positive))
				{
					dailyReportMetricsData.Positive = new float?(reportEntry.Positive);
				}
				if (!float.IsInfinity(reportEntry.Negative) && !float.IsNaN(reportEntry.Negative))
				{
					dailyReportMetricsData.Negative = new float?(reportEntry.Negative);
				}
				list.Add(dailyReportMetricsData);
			}
			list.Add(new SaveLoader.DailyReportMetricsData
			{
				Name = "MinionCount",
				Net = new float?((float)Components.LiveMinionIdentities.Count),
				Positive = new float?(0f),
				Negative = new float?(0f)
			});
		}
		return list;
	}

	// Token: 0x060042C5 RID: 17093 RVA: 0x001792E0 File Offset: 0x001774E0
	private List<SaveLoader.PerformanceMeasurement> GetPerformanceMeasurements()
	{
		List<SaveLoader.PerformanceMeasurement> list = new List<SaveLoader.PerformanceMeasurement>();
		if (Global.Instance != null)
		{
			PerformanceMonitor component = Global.Instance.GetComponent<PerformanceMonitor>();
			list.Add(new SaveLoader.PerformanceMeasurement
			{
				name = "FramesAbove30",
				value = component.NumFramesAbove30
			});
			list.Add(new SaveLoader.PerformanceMeasurement
			{
				name = "FramesBelow30",
				value = component.NumFramesBelow30
			});
			component.Reset();
		}
		return list;
	}

	// Token: 0x060042C6 RID: 17094 RVA: 0x00179368 File Offset: 0x00177568
	private float GetFrameTime()
	{
		PerformanceMonitor component = Global.Instance.GetComponent<PerformanceMonitor>();
		DebugUtil.LogArgs(new object[]
		{
			"Average frame time:",
			1f / component.FPS
		});
		return 1f / component.FPS;
	}

	// Token: 0x04002C75 RID: 11381
	[MyCmpGet]
	private GridSettings gridSettings;

	// Token: 0x04002C77 RID: 11383
	private bool saveFileCorrupt;

	// Token: 0x04002C78 RID: 11384
	private bool compressSaveData = true;

	// Token: 0x04002C79 RID: 11385
	private int lastUncompressedSize;

	// Token: 0x04002C7A RID: 11386
	public bool saveAsText;

	// Token: 0x04002C7B RID: 11387
	public const string MAINMENU_LEVELNAME = "launchscene";

	// Token: 0x04002C7C RID: 11388
	public const string FRONTEND_LEVELNAME = "frontend";

	// Token: 0x04002C7D RID: 11389
	public const string BACKEND_LEVELNAME = "backend";

	// Token: 0x04002C7E RID: 11390
	public const string SAVE_EXTENSION = ".sav";

	// Token: 0x04002C7F RID: 11391
	public const string AUTOSAVE_FOLDER = "auto_save";

	// Token: 0x04002C80 RID: 11392
	public const string CLOUDSAVE_FOLDER = "cloud_save_files";

	// Token: 0x04002C81 RID: 11393
	public const string SAVE_FOLDER = "save_files";

	// Token: 0x04002C82 RID: 11394
	public const int MAX_AUTOSAVE_FILES = 10;

	// Token: 0x04002C84 RID: 11396
	[NonSerialized]
	public SaveManager saveManager;

	// Token: 0x04002C86 RID: 11398
	private Cluster m_clusterLayout;

	// Token: 0x04002C88 RID: 11400
	private const string CorruptFileSuffix = "_";

	// Token: 0x04002C89 RID: 11401
	private const float SAVE_BUFFER_HEAD_ROOM = 0.1f;

	// Token: 0x04002C8A RID: 11402
	private bool mustRestartOnFail;

	// Token: 0x04002C8D RID: 11405
	public const string METRIC_SAVED_PREFAB_KEY = "SavedPrefabs";

	// Token: 0x04002C8E RID: 11406
	public const string METRIC_IS_AUTO_SAVE_KEY = "IsAutoSave";

	// Token: 0x04002C8F RID: 11407
	public const string METRIC_WAS_DEBUG_EVER_USED = "WasDebugEverUsed";

	// Token: 0x04002C90 RID: 11408
	public const string METRIC_IS_SANDBOX_ENABLED = "IsSandboxEnabled";

	// Token: 0x04002C91 RID: 11409
	public const string METRIC_RESOURCES_ACCESSIBLE_KEY = "ResourcesAccessible";

	// Token: 0x04002C92 RID: 11410
	public const string METRIC_DAILY_REPORT_KEY = "DailyReport";

	// Token: 0x04002C93 RID: 11411
	public const string METRIC_MINION_METRICS_KEY = "MinionMetrics";

	// Token: 0x04002C94 RID: 11412
	public const string METRIC_CUSTOM_GAME_SETTINGS = "CustomGameSettings";

	// Token: 0x04002C95 RID: 11413
	public const string METRIC_PERFORMANCE_MEASUREMENTS = "PerformanceMeasurements";

	// Token: 0x04002C96 RID: 11414
	public const string METRIC_FRAME_TIME = "AverageFrameTime";

	// Token: 0x04002C97 RID: 11415
	private static bool force_infinity;

	// Token: 0x020016CB RID: 5835
	public class FlowUtilityNetworkInstance
	{
		// Token: 0x04006B03 RID: 27395
		public int id = -1;

		// Token: 0x04006B04 RID: 27396
		public SimHashes containedElement = SimHashes.Vacuum;

		// Token: 0x04006B05 RID: 27397
		public float containedMass;

		// Token: 0x04006B06 RID: 27398
		public float containedTemperature;
	}

	// Token: 0x020016CC RID: 5836
	[SerializationConfig(KSerialization.MemberSerialization.OptOut)]
	public class FlowUtilityNetworkSaver : ISaveLoadable
	{
		// Token: 0x060088B8 RID: 35000 RVA: 0x002F67C6 File Offset: 0x002F49C6
		public FlowUtilityNetworkSaver()
		{
			this.gas = new List<SaveLoader.FlowUtilityNetworkInstance>();
			this.liquid = new List<SaveLoader.FlowUtilityNetworkInstance>();
		}

		// Token: 0x04006B07 RID: 27399
		public List<SaveLoader.FlowUtilityNetworkInstance> gas;

		// Token: 0x04006B08 RID: 27400
		public List<SaveLoader.FlowUtilityNetworkInstance> liquid;
	}

	// Token: 0x020016CD RID: 5837
	public struct SaveFileEntry
	{
		// Token: 0x04006B09 RID: 27401
		public string path;

		// Token: 0x04006B0A RID: 27402
		public System.DateTime timeStamp;
	}

	// Token: 0x020016CE RID: 5838
	public enum SaveType
	{
		// Token: 0x04006B0C RID: 27404
		local,
		// Token: 0x04006B0D RID: 27405
		cloud,
		// Token: 0x04006B0E RID: 27406
		both
	}

	// Token: 0x020016CF RID: 5839
	private struct MinionAttrFloatData
	{
		// Token: 0x04006B0F RID: 27407
		public string Name;

		// Token: 0x04006B10 RID: 27408
		public float Value;
	}

	// Token: 0x020016D0 RID: 5840
	private struct MinionMetricsData
	{
		// Token: 0x04006B11 RID: 27409
		public string Name;

		// Token: 0x04006B12 RID: 27410
		public List<SaveLoader.MinionAttrFloatData> Modifiers;

		// Token: 0x04006B13 RID: 27411
		public float TotalExperienceGained;

		// Token: 0x04006B14 RID: 27412
		public List<string> Skills;
	}

	// Token: 0x020016D1 RID: 5841
	private struct SavedPrefabMetricsData
	{
		// Token: 0x04006B15 RID: 27413
		public string PrefabName;

		// Token: 0x04006B16 RID: 27414
		public int Count;
	}

	// Token: 0x020016D2 RID: 5842
	private struct WorldInventoryMetricsData
	{
		// Token: 0x04006B17 RID: 27415
		public string Name;

		// Token: 0x04006B18 RID: 27416
		public float Amount;
	}

	// Token: 0x020016D3 RID: 5843
	private struct DailyReportMetricsData
	{
		// Token: 0x04006B19 RID: 27417
		public string Name;

		// Token: 0x04006B1A RID: 27418
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public float? Net;

		// Token: 0x04006B1B RID: 27419
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public float? Positive;

		// Token: 0x04006B1C RID: 27420
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public float? Negative;
	}

	// Token: 0x020016D4 RID: 5844
	private struct PerformanceMeasurement
	{
		// Token: 0x04006B1D RID: 27421
		public string name;

		// Token: 0x04006B1E RID: 27422
		public float value;
	}
}
