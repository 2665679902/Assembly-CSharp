using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using ProcGen;
using ProcGenGame;
using Steamworks;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A0A RID: 2570
public class LoadScreen : KModalScreen
{
	// Token: 0x170005C4 RID: 1476
	// (get) Token: 0x06004D27 RID: 19751 RVA: 0x001B2214 File Offset: 0x001B0414
	// (set) Token: 0x06004D28 RID: 19752 RVA: 0x001B221B File Offset: 0x001B041B
	public static LoadScreen Instance { get; private set; }

	// Token: 0x06004D29 RID: 19753 RVA: 0x001B2223 File Offset: 0x001B0423
	public static void DestroyInstance()
	{
		LoadScreen.Instance = null;
	}

	// Token: 0x06004D2A RID: 19754 RVA: 0x001B222C File Offset: 0x001B042C
	protected override void OnPrefabInit()
	{
		global::Debug.Assert(LoadScreen.Instance == null);
		LoadScreen.Instance = this;
		base.OnPrefabInit();
		this.colonyListPool = new UIPool<HierarchyReferences>(this.saveButtonPrefab);
		if (SpeedControlScreen.Instance != null)
		{
			SpeedControlScreen.Instance.Pause(false, false);
		}
		if (this.closeButton != null)
		{
			this.closeButton.onClick += delegate
			{
				this.Deactivate();
			};
		}
		if (this.colonyCloudButton != null)
		{
			this.colonyCloudButton.onClick += delegate
			{
				this.ConvertAllToCloud();
			};
		}
		if (this.colonyLocalButton != null)
		{
			this.colonyLocalButton.onClick += delegate
			{
				this.ConvertAllToLocal();
			};
		}
		if (this.colonyInfoButton != null)
		{
			this.colonyInfoButton.onClick += delegate
			{
				this.ShowSaveInfo();
			};
		}
	}

	// Token: 0x06004D2B RID: 19755 RVA: 0x001B2313 File Offset: 0x001B0513
	private bool IsInMenu()
	{
		return App.GetCurrentSceneName() == "frontend";
	}

	// Token: 0x06004D2C RID: 19756 RVA: 0x001B2324 File Offset: 0x001B0524
	private bool CloudSavesVisible()
	{
		return SaveLoader.GetCloudSavesAvailable() && this.IsInMenu();
	}

	// Token: 0x06004D2D RID: 19757 RVA: 0x001B2338 File Offset: 0x001B0538
	protected override void OnActivate()
	{
		base.OnActivate();
		WorldGen.LoadSettings(false);
		this.SetCloudSaveInfoActive(this.CloudSavesVisible());
		this.RefreshColonyList();
		this.ShowColonyList();
		bool cloudSavesAvailable = SaveLoader.GetCloudSavesAvailable();
		this.cloudTutorialBouncer.gameObject.SetActive(cloudSavesAvailable);
		if (cloudSavesAvailable && !this.cloudTutorialBouncer.IsBouncing())
		{
			int @int = KPlayerPrefs.GetInt("LoadScreenCloudTutorialTimes", 0);
			if (@int < 5)
			{
				this.cloudTutorialBouncer.Bounce();
				KPlayerPrefs.SetInt("LoadScreenCloudTutorialTimes", @int + 1);
				KPlayerPrefs.GetInt("LoadScreenCloudTutorialTimes", 0);
			}
			else
			{
				this.cloudTutorialBouncer.gameObject.SetActive(false);
			}
		}
		if (DistributionPlatform.Initialized && SteamUtils.IsSteamRunningOnSteamDeck())
		{
			this.colonyInfoButton.gameObject.SetActive(false);
		}
	}

	// Token: 0x06004D2E RID: 19758 RVA: 0x001B23F8 File Offset: 0x001B05F8
	private Dictionary<string, List<LoadScreen.SaveGameFileDetails>> GetColoniesDetails(List<SaveLoader.SaveFileEntry> files)
	{
		Dictionary<string, List<LoadScreen.SaveGameFileDetails>> dictionary = new Dictionary<string, List<LoadScreen.SaveGameFileDetails>>();
		if (files.Count <= 0)
		{
			return dictionary;
		}
		for (int i = 0; i < files.Count; i++)
		{
			if (this.IsFileValid(files[i].path))
			{
				global::Tuple<SaveGame.Header, SaveGame.GameInfo> fileInfo = SaveGame.GetFileInfo(files[i].path);
				SaveGame.Header first = fileInfo.first;
				SaveGame.GameInfo second = fileInfo.second;
				System.DateTime timeStamp = files[i].timeStamp;
				long num = 0L;
				try
				{
					num = new FileInfo(files[i].path).Length;
				}
				catch (Exception ex)
				{
					global::Debug.LogWarning("Failed to get size for file: " + files[i].ToString() + "\n" + ex.ToString());
				}
				LoadScreen.SaveGameFileDetails saveGameFileDetails = new LoadScreen.SaveGameFileDetails
				{
					BaseName = second.baseName,
					FileName = files[i].path,
					FileDate = timeStamp,
					FileHeader = first,
					FileInfo = second,
					Size = num,
					UniqueID = SaveGame.GetSaveUniqueID(second)
				};
				if (!dictionary.ContainsKey(saveGameFileDetails.UniqueID))
				{
					dictionary.Add(saveGameFileDetails.UniqueID, new List<LoadScreen.SaveGameFileDetails>());
				}
				dictionary[saveGameFileDetails.UniqueID].Add(saveGameFileDetails);
			}
		}
		return dictionary;
	}

	// Token: 0x06004D2F RID: 19759 RVA: 0x001B2560 File Offset: 0x001B0760
	private Dictionary<string, List<LoadScreen.SaveGameFileDetails>> GetColonies(bool sort)
	{
		List<SaveLoader.SaveFileEntry> allFiles = SaveLoader.GetAllFiles(sort, SaveLoader.SaveType.both);
		return this.GetColoniesDetails(allFiles);
	}

	// Token: 0x06004D30 RID: 19760 RVA: 0x001B257C File Offset: 0x001B077C
	private Dictionary<string, List<LoadScreen.SaveGameFileDetails>> GetLocalColonies(bool sort)
	{
		List<SaveLoader.SaveFileEntry> allFiles = SaveLoader.GetAllFiles(sort, SaveLoader.SaveType.local);
		return this.GetColoniesDetails(allFiles);
	}

	// Token: 0x06004D31 RID: 19761 RVA: 0x001B2598 File Offset: 0x001B0798
	private Dictionary<string, List<LoadScreen.SaveGameFileDetails>> GetCloudColonies(bool sort)
	{
		List<SaveLoader.SaveFileEntry> allFiles = SaveLoader.GetAllFiles(sort, SaveLoader.SaveType.cloud);
		return this.GetColoniesDetails(allFiles);
	}

	// Token: 0x06004D32 RID: 19762 RVA: 0x001B25B4 File Offset: 0x001B07B4
	private bool IsFileValid(string filename)
	{
		bool flag = false;
		try
		{
			SaveGame.Header header;
			flag = SaveLoader.LoadHeader(filename, out header).saveMajorVersion >= 7;
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning("Corrupted save file: " + filename + "\n" + ex.ToString());
		}
		return flag;
	}

	// Token: 0x06004D33 RID: 19763 RVA: 0x001B2608 File Offset: 0x001B0808
	private void CheckCloudLocalOverlap()
	{
		if (!SaveLoader.GetCloudSavesAvailable())
		{
			return;
		}
		string cloudSavePrefix = SaveLoader.GetCloudSavePrefix();
		if (cloudSavePrefix == null)
		{
			return;
		}
		foreach (KeyValuePair<string, List<LoadScreen.SaveGameFileDetails>> keyValuePair in this.GetColonies(false))
		{
			bool flag = false;
			List<LoadScreen.SaveGameFileDetails> list = new List<LoadScreen.SaveGameFileDetails>();
			foreach (LoadScreen.SaveGameFileDetails saveGameFileDetails in keyValuePair.Value)
			{
				if (SaveLoader.IsSaveCloud(saveGameFileDetails.FileName))
				{
					flag = true;
				}
				else
				{
					list.Add(saveGameFileDetails);
				}
			}
			if (flag && list.Count != 0)
			{
				string baseName = list[0].BaseName;
				string text = System.IO.Path.Combine(SaveLoader.GetSavePrefix(), baseName);
				string text2 = System.IO.Path.Combine(cloudSavePrefix, baseName);
				if (!Directory.Exists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				global::Debug.Log("Saves / Found overlapped cloud/local saves for colony '" + baseName + "', moving to cloud...");
				foreach (LoadScreen.SaveGameFileDetails saveGameFileDetails2 in list)
				{
					string fileName = saveGameFileDetails2.FileName;
					string text3 = System.IO.Path.ChangeExtension(fileName, "png");
					string text4 = text2;
					if (SaveLoader.IsSaveAuto(fileName))
					{
						string text5 = System.IO.Path.Combine(text4, "auto_save");
						if (!Directory.Exists(text5))
						{
							Directory.CreateDirectory(text5);
						}
						text4 = text5;
					}
					string text6 = System.IO.Path.Combine(text4, System.IO.Path.GetFileName(fileName));
					global::Tuple<bool, bool> tuple;
					if (this.FileMatch(fileName, text6, out tuple))
					{
						global::Debug.Log("Saves / file match found for `" + fileName + "`...");
						this.MigrateFile(fileName, text6, false);
						string text7 = System.IO.Path.ChangeExtension(text6, "png");
						this.MigrateFile(text3, text7, true);
					}
					else
					{
						global::Debug.Log("Saves / no file match found for `" + fileName + "`... move as copy");
						string nextUsableSavePath = SaveLoader.GetNextUsableSavePath(text6);
						this.MigrateFile(fileName, nextUsableSavePath, false);
						string text8 = System.IO.Path.ChangeExtension(nextUsableSavePath, "png");
						this.MigrateFile(text3, text8, true);
					}
				}
				this.RemoveEmptyFolder(text);
			}
		}
	}

	// Token: 0x06004D34 RID: 19764 RVA: 0x001B2880 File Offset: 0x001B0A80
	private void DeleteFileAndEmptyFolder(string file)
	{
		if (File.Exists(file))
		{
			File.Delete(file);
		}
		this.RemoveEmptyFolder(System.IO.Path.GetDirectoryName(file));
	}

	// Token: 0x06004D35 RID: 19765 RVA: 0x001B289C File Offset: 0x001B0A9C
	private void RemoveEmptyFolder(string path)
	{
		if (!Directory.Exists(path))
		{
			return;
		}
		if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory))
		{
			return;
		}
		if (Directory.EnumerateFileSystemEntries(path).Any<string>())
		{
			return;
		}
		try
		{
			Directory.Delete(path);
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning("Failed to remove empty directory `" + path + "`...");
			global::Debug.LogWarning(ex);
		}
	}

	// Token: 0x06004D36 RID: 19766 RVA: 0x001B2910 File Offset: 0x001B0B10
	private void RefreshColonyList()
	{
		if (this.colonyListPool != null)
		{
			this.colonyListPool.ClearAll();
		}
		this.CheckCloudLocalOverlap();
		Dictionary<string, List<LoadScreen.SaveGameFileDetails>> colonies = this.GetColonies(true);
		if (colonies.Count > 0)
		{
			foreach (KeyValuePair<string, List<LoadScreen.SaveGameFileDetails>> keyValuePair in colonies)
			{
				this.AddColonyToList(keyValuePair.Value);
			}
		}
	}

	// Token: 0x06004D37 RID: 19767 RVA: 0x001B2990 File Offset: 0x001B0B90
	private string GetFileHash(string path)
	{
		string text;
		using (MD5 md = MD5.Create())
		{
			using (FileStream fileStream = File.OpenRead(path))
			{
				text = BitConverter.ToString(md.ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
			}
		}
		return text;
	}

	// Token: 0x06004D38 RID: 19768 RVA: 0x001B2A00 File Offset: 0x001B0C00
	private bool FileMatch(string file, string other_file, out global::Tuple<bool, bool> matches)
	{
		matches = new global::Tuple<bool, bool>(false, false);
		if (!File.Exists(file))
		{
			return false;
		}
		if (!File.Exists(other_file))
		{
			return false;
		}
		bool flag = false;
		bool flag2 = false;
		try
		{
			string fileHash = this.GetFileHash(file);
			string fileHash2 = this.GetFileHash(other_file);
			FileInfo fileInfo = new FileInfo(file);
			FileInfo fileInfo2 = new FileInfo(other_file);
			flag = fileInfo.Length == fileInfo2.Length;
			flag2 = fileHash == fileHash2;
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning(string.Concat(new string[] { "FileMatch / file match failed for `", file, "` vs `", other_file, "`!" }));
			global::Debug.LogWarning(ex);
			return false;
		}
		matches.first = flag;
		matches.second = flag2;
		return flag && flag2;
	}

	// Token: 0x06004D39 RID: 19769 RVA: 0x001B2AC8 File Offset: 0x001B0CC8
	private bool MigrateFile(string source, string dest, bool ignoreMissing = false)
	{
		global::Debug.Log(string.Concat(new string[] { "Migration / moving `", source, "` to `", dest, "` ..." }));
		if (dest == source)
		{
			global::Debug.Log(string.Concat(new string[] { "Migration / ignored `", source, "` to `", dest, "` ... same location" }));
			return true;
		}
		global::Tuple<bool, bool> tuple;
		if (this.FileMatch(source, dest, out tuple))
		{
			global::Debug.Log("Migration / dest and source are identical size + hash ... removing original");
			try
			{
				this.DeleteFileAndEmptyFolder(source);
			}
			catch (Exception ex)
			{
				global::Debug.LogWarning("Migration / removing original failed for `" + source + "`!");
				global::Debug.LogWarning(ex);
				throw ex;
			}
			return true;
		}
		try
		{
			global::Debug.Log("Migration / copying...");
			File.Copy(source, dest, false);
		}
		catch (FileNotFoundException obj) when (ignoreMissing)
		{
			global::Debug.Log("Migration / File `" + source + "` wasn't found but we're ignoring that.");
			return true;
		}
		catch (Exception ex2)
		{
			global::Debug.LogWarning("Migration / copy failed for `" + source + "`! Leaving it alone");
			global::Debug.LogWarning(ex2);
			global::Debug.LogWarning("failed to convert colony: " + ex2.ToString());
			throw ex2;
		}
		global::Debug.Log("Migration / copy ok ...");
		global::Tuple<bool, bool> tuple2;
		if (!this.FileMatch(source, dest, out tuple2))
		{
			global::Debug.LogWarning("Migration / failed to match dest file for `" + source + "`!");
			global::Debug.LogWarning(string.Format("Migration / did hash match? {0} did size match? {1}", tuple2.second, tuple2.first));
			throw new Exception("Hash/Size didn't match for source and destination");
		}
		global::Debug.Log("Migration / hash validation ok ... removing original");
		try
		{
			this.DeleteFileAndEmptyFolder(source);
		}
		catch (Exception ex3)
		{
			global::Debug.LogWarning("Migration / removing original failed for `" + source + "`!");
			global::Debug.LogWarning(ex3);
			throw ex3;
		}
		global::Debug.Log("Migration / moved ok for `" + source + "`!");
		return true;
	}

	// Token: 0x06004D3A RID: 19770 RVA: 0x001B2CCC File Offset: 0x001B0ECC
	private bool MigrateSave(string dest_root, string file, bool is_auto_save, out string saveError)
	{
		saveError = null;
		global::Tuple<SaveGame.Header, SaveGame.GameInfo> fileInfo = SaveGame.GetFileInfo(file);
		SaveGame.Header first = fileInfo.first;
		string text = fileInfo.second.baseName.TrimEnd(new char[] { ' ' });
		string fileName = System.IO.Path.GetFileName(file);
		string text2 = System.IO.Path.Combine(dest_root, text);
		if (!Directory.Exists(text2))
		{
			text2 = Directory.CreateDirectory(text2).FullName;
		}
		string text3 = text2;
		if (is_auto_save)
		{
			string text4 = System.IO.Path.Combine(text2, "auto_save");
			if (!Directory.Exists(text4))
			{
				Directory.CreateDirectory(text4);
			}
			text3 = text4;
		}
		string text5 = System.IO.Path.Combine(text3, fileName);
		string text6 = System.IO.Path.ChangeExtension(file, "png");
		string text7 = System.IO.Path.ChangeExtension(text5, "png");
		try
		{
			this.MigrateFile(file, text5, false);
			this.MigrateFile(text6, text7, true);
		}
		catch (Exception ex)
		{
			saveError = ex.Message;
			return false;
		}
		return true;
	}

	// Token: 0x06004D3B RID: 19771 RVA: 0x001B2DB0 File Offset: 0x001B0FB0
	private ValueTuple<int, int, ulong> GetSavesSizeAndCounts(List<LoadScreen.SaveGameFileDetails> list)
	{
		ulong num = 0UL;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < list.Count; i++)
		{
			LoadScreen.SaveGameFileDetails saveGameFileDetails = list[i];
			num += (ulong)saveGameFileDetails.Size;
			if (saveGameFileDetails.FileInfo.isAutoSave)
			{
				num3++;
			}
			else
			{
				num2++;
			}
		}
		return new ValueTuple<int, int, ulong>(num2, num3, num);
	}

	// Token: 0x06004D3C RID: 19772 RVA: 0x001B2E08 File Offset: 0x001B1008
	private int CountValidSaves(string path, SearchOption searchType = SearchOption.AllDirectories)
	{
		int num = 0;
		List<SaveLoader.SaveFileEntry> saveFiles = SaveLoader.GetSaveFiles(path, false, searchType);
		for (int i = 0; i < saveFiles.Count; i++)
		{
			if (this.IsFileValid(saveFiles[i].path))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06004D3D RID: 19773 RVA: 0x001B2E4C File Offset: 0x001B104C
	private ValueTuple<int, int> GetMigrationSaveCounts()
	{
		int num = this.CountValidSaves(SaveLoader.GetSavePrefixAndCreateFolder(), SearchOption.TopDirectoryOnly);
		int num2 = this.CountValidSaves(SaveLoader.GetAutoSavePrefix(), SearchOption.AllDirectories);
		return new ValueTuple<int, int>(num, num2);
	}

	// Token: 0x06004D3E RID: 19774 RVA: 0x001B2E78 File Offset: 0x001B1078
	private ValueTuple<int, int> MigrateSaves(out string errorColony, out string errorMessage)
	{
		errorColony = null;
		errorMessage = null;
		int num = 0;
		string savePrefixAndCreateFolder = SaveLoader.GetSavePrefixAndCreateFolder();
		List<SaveLoader.SaveFileEntry> saveFiles = SaveLoader.GetSaveFiles(savePrefixAndCreateFolder, false, SearchOption.TopDirectoryOnly);
		for (int i = 0; i < saveFiles.Count; i++)
		{
			SaveLoader.SaveFileEntry saveFileEntry = saveFiles[i];
			if (this.IsFileValid(saveFileEntry.path))
			{
				string text;
				if (this.MigrateSave(savePrefixAndCreateFolder, saveFileEntry.path, false, out text))
				{
					num++;
				}
				else if (errorColony == null)
				{
					errorColony = saveFileEntry.path;
					errorMessage = text;
				}
			}
		}
		int num2 = 0;
		List<SaveLoader.SaveFileEntry> saveFiles2 = SaveLoader.GetSaveFiles(SaveLoader.GetAutoSavePrefix(), false, SearchOption.AllDirectories);
		for (int j = 0; j < saveFiles2.Count; j++)
		{
			SaveLoader.SaveFileEntry saveFileEntry2 = saveFiles2[j];
			if (this.IsFileValid(saveFileEntry2.path))
			{
				string text2;
				if (this.MigrateSave(savePrefixAndCreateFolder, saveFileEntry2.path, true, out text2))
				{
					num2++;
				}
				else if (errorColony == null)
				{
					errorColony = saveFileEntry2.path;
					errorMessage = text2;
				}
			}
		}
		return new ValueTuple<int, int>(num, num2);
	}

	// Token: 0x06004D3F RID: 19775 RVA: 0x001B2F68 File Offset: 0x001B1168
	public void ShowMigrationIfNecessary(bool fromMainMenu)
	{
		ValueTuple<int, int> migrationSaveCounts = this.GetMigrationSaveCounts();
		int saveCount = migrationSaveCounts.Item1;
		int autoCount = migrationSaveCounts.Item2;
		if (saveCount == 0 && autoCount == 0)
		{
			if (fromMainMenu)
			{
				this.Deactivate();
			}
			return;
		}
		base.Activate();
		this.migrationPanelRefs.gameObject.SetActive(true);
		KButton migrateButton = this.migrationPanelRefs.GetReference<RectTransform>("MigrateSaves").GetComponent<KButton>();
		KButton continueButton = this.migrationPanelRefs.GetReference<RectTransform>("Continue").GetComponent<KButton>();
		KButton moreInfoButton = this.migrationPanelRefs.GetReference<RectTransform>("MoreInfo").GetComponent<KButton>();
		KButton component = this.migrationPanelRefs.GetReference<RectTransform>("OpenSaves").GetComponent<KButton>();
		LocText statsText = this.migrationPanelRefs.GetReference<RectTransform>("CountText").GetComponent<LocText>();
		LocText infoText = this.migrationPanelRefs.GetReference<RectTransform>("InfoText").GetComponent<LocText>();
		migrateButton.gameObject.SetActive(true);
		continueButton.gameObject.SetActive(false);
		moreInfoButton.gameObject.SetActive(false);
		statsText.text = string.Format(UI.FRONTEND.LOADSCREEN.MIGRATE_COUNT, saveCount, autoCount);
		component.ClearOnClick();
		component.onClick += delegate
		{
			App.OpenWebURL(SaveLoader.GetSavePrefixAndCreateFolder());
		};
		migrateButton.ClearOnClick();
		migrateButton.onClick += delegate
		{
			migrateButton.gameObject.SetActive(false);
			string text;
			string text2;
			ValueTuple<int, int> valueTuple = this.MigrateSaves(out text, out text2);
			int item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			bool flag = text == null;
			string text3 = (flag ? UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT.text : UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES.Replace("{ErrorColony}", text).Replace("{ErrorMessage}", text2));
			statsText.text = string.Format(text3, new object[] { item, saveCount, item2, autoCount });
			infoText.gameObject.SetActive(false);
			if (flag)
			{
				continueButton.gameObject.SetActive(true);
			}
			else
			{
				moreInfoButton.gameObject.SetActive(true);
			}
			MainMenu.Instance.RefreshResumeButton(false);
			this.RefreshColonyList();
		};
		continueButton.ClearOnClick();
		continueButton.onClick += delegate
		{
			this.migrationPanelRefs.gameObject.SetActive(false);
			this.cloudTutorialBouncer.Bounce();
		};
		moreInfoButton.ClearOnClick();
		Action<InfoDialogScreen> <>9__4;
		Action<InfoDialogScreen> <>9__6;
		moreInfoButton.onClick += delegate
		{
			if (DistributionPlatform.Initialized && SteamUtils.IsSteamRunningOnSteamDeck())
			{
				InfoDialogScreen infoDialogScreen = global::Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, this.gameObject, false).SetHeader(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_TITLE).AddPlainText(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_PRE)
					.AddLineItem(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM1, "")
					.AddLineItem(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM2, "")
					.AddLineItem(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM3, "")
					.AddPlainText(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_POST);
				string text4 = UI.CONFIRMDIALOG.OK;
				Action<InfoDialogScreen> action;
				if ((action = <>9__4) == null)
				{
					action = (<>9__4 = delegate(InfoDialogScreen d)
					{
						this.migrationPanelRefs.gameObject.SetActive(false);
						this.cloudTutorialBouncer.Bounce();
						d.Deactivate();
					});
				}
				infoDialogScreen.AddOption(text4, action, true).Activate();
				return;
			}
			InfoDialogScreen infoDialogScreen2 = global::Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, this.gameObject, false).SetHeader(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_TITLE).AddPlainText(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_PRE)
				.AddLineItem(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM1, "")
				.AddLineItem(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM2, "")
				.AddLineItem(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM3, "")
				.AddPlainText(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_POST)
				.AddOption(UI.FRONTEND.LOADSCREEN.MIGRATE_FAILURES_FORUM_BUTTON, delegate(InfoDialogScreen d)
				{
					App.OpenWebURL("https://forums.kleientertainment.com/klei-bug-tracker/oni/");
				}, false);
			string text5 = UI.CONFIRMDIALOG.OK;
			Action<InfoDialogScreen> action2;
			if ((action2 = <>9__6) == null)
			{
				action2 = (<>9__6 = delegate(InfoDialogScreen d)
				{
					this.migrationPanelRefs.gameObject.SetActive(false);
					this.cloudTutorialBouncer.Bounce();
					d.Deactivate();
				});
			}
			infoDialogScreen2.AddOption(text5, action2, true).Activate();
		};
	}

	// Token: 0x06004D40 RID: 19776 RVA: 0x001B3161 File Offset: 0x001B1361
	private void SetCloudSaveInfoActive(bool active)
	{
		this.colonyCloudButton.gameObject.SetActive(active);
		this.colonyLocalButton.gameObject.SetActive(active);
	}

	// Token: 0x06004D41 RID: 19777 RVA: 0x001B3188 File Offset: 0x001B1388
	private bool ConvertToLocalOrCloud(string fromRoot, string destRoot, string colonyName)
	{
		string text = System.IO.Path.Combine(fromRoot, colonyName);
		string text2 = System.IO.Path.Combine(destRoot, colonyName);
		global::Debug.Log(string.Concat(new string[] { "Convert / Colony '", colonyName, "' from `", text, "` => `", text2, "`" }));
		try
		{
			Directory.Move(text, text2);
			return true;
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning("failed to convert colony: " + ex.ToString());
			string text3 = UI.FRONTEND.LOADSCREEN.CONVERT_ERROR.Replace("{Colony}", colonyName).Replace("{Error}", ex.Message);
			this.ShowConvertError(text3);
		}
		return false;
	}

	// Token: 0x06004D42 RID: 19778 RVA: 0x001B3244 File Offset: 0x001B1444
	private bool ConvertColonyToCloud(string colonyName)
	{
		string savePrefix = SaveLoader.GetSavePrefix();
		string cloudSavePrefix = SaveLoader.GetCloudSavePrefix();
		if (cloudSavePrefix == null)
		{
			global::Debug.LogWarning("Failed to move colony to cloud, no cloud save prefix found (usually a userID is missing, not logged in?)");
			return false;
		}
		return this.ConvertToLocalOrCloud(savePrefix, cloudSavePrefix, colonyName);
	}

	// Token: 0x06004D43 RID: 19779 RVA: 0x001B3278 File Offset: 0x001B1478
	private bool ConvertColonyToLocal(string colonyName)
	{
		string savePrefix = SaveLoader.GetSavePrefix();
		string cloudSavePrefix = SaveLoader.GetCloudSavePrefix();
		if (cloudSavePrefix == null)
		{
			global::Debug.LogWarning("Failed to move colony from cloud, no cloud save prefix found (usually a userID is missing, not logged in?)");
			return false;
		}
		return this.ConvertToLocalOrCloud(cloudSavePrefix, savePrefix, colonyName);
	}

	// Token: 0x06004D44 RID: 19780 RVA: 0x001B32AC File Offset: 0x001B14AC
	private void DoConvertAllToLocal()
	{
		Dictionary<string, List<LoadScreen.SaveGameFileDetails>> cloudColonies = this.GetCloudColonies(false);
		if (cloudColonies.Count == 0)
		{
			return;
		}
		bool flag = true;
		foreach (KeyValuePair<string, List<LoadScreen.SaveGameFileDetails>> keyValuePair in cloudColonies)
		{
			flag &= this.ConvertColonyToLocal(keyValuePair.Value[0].BaseName);
		}
		if (flag)
		{
			string text = UI.PLATFORMS.STEAM;
			this.ShowSimpleDialog(UI.FRONTEND.LOADSCREEN.CONVERT_TO_LOCAL, UI.FRONTEND.LOADSCREEN.CONVERT_ALL_TO_LOCAL_SUCCESS.Replace("{Client}", text));
		}
		this.RefreshColonyList();
		MainMenu.Instance.RefreshResumeButton(false);
		SaveLoader.SetCloudSavesDefault(false);
	}

	// Token: 0x06004D45 RID: 19781 RVA: 0x001B3368 File Offset: 0x001B1568
	private void DoConvertAllToCloud()
	{
		Dictionary<string, List<LoadScreen.SaveGameFileDetails>> localColonies = this.GetLocalColonies(false);
		if (localColonies.Count == 0)
		{
			return;
		}
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, List<LoadScreen.SaveGameFileDetails>> keyValuePair in localColonies)
		{
			string baseName = keyValuePair.Value[0].BaseName;
			if (!list.Contains(baseName))
			{
				list.Add(baseName);
			}
		}
		bool flag = true;
		foreach (string text in list)
		{
			flag &= this.ConvertColonyToCloud(text);
		}
		if (flag)
		{
			string text2 = UI.PLATFORMS.STEAM;
			this.ShowSimpleDialog(UI.FRONTEND.LOADSCREEN.CONVERT_TO_CLOUD, UI.FRONTEND.LOADSCREEN.CONVERT_ALL_TO_CLOUD_SUCCESS.Replace("{Client}", text2));
		}
		this.RefreshColonyList();
		MainMenu.Instance.RefreshResumeButton(false);
		SaveLoader.SetCloudSavesDefault(true);
	}

	// Token: 0x06004D46 RID: 19782 RVA: 0x001B347C File Offset: 0x001B167C
	private void ConvertAllToCloud()
	{
		string text = string.Format("{0}\n{1}\n", UI.FRONTEND.LOADSCREEN.CONVERT_TO_CLOUD_DETAILS, UI.FRONTEND.LOADSCREEN.CONVERT_ALL_WARNING);
		KPlayerPrefs.SetInt("LoadScreenCloudTutorialTimes", 5);
		this.ConfirmCloudSaveMigrations(text, UI.FRONTEND.LOADSCREEN.CONVERT_TO_CLOUD, UI.FRONTEND.LOADSCREEN.CONVERT_ALL_COLONIES, UI.FRONTEND.LOADSCREEN.OPEN_SAVE_FOLDER, delegate
		{
			this.DoConvertAllToCloud();
		}, delegate
		{
			App.OpenWebURL(SaveLoader.GetSavePrefix());
		}, this.localToCloudSprite);
	}

	// Token: 0x06004D47 RID: 19783 RVA: 0x001B3500 File Offset: 0x001B1700
	private void ConvertAllToLocal()
	{
		string text = string.Format("{0}\n{1}\n", UI.FRONTEND.LOADSCREEN.CONVERT_TO_LOCAL_DETAILS, UI.FRONTEND.LOADSCREEN.CONVERT_ALL_WARNING);
		KPlayerPrefs.SetInt("LoadScreenCloudTutorialTimes", 5);
		this.ConfirmCloudSaveMigrations(text, UI.FRONTEND.LOADSCREEN.CONVERT_TO_LOCAL, UI.FRONTEND.LOADSCREEN.CONVERT_ALL_COLONIES, UI.FRONTEND.LOADSCREEN.OPEN_SAVE_FOLDER, delegate
		{
			this.DoConvertAllToLocal();
		}, delegate
		{
			App.OpenWebURL(SaveLoader.GetCloudSavePrefix());
		}, this.cloudToLocalSprite);
	}

	// Token: 0x06004D48 RID: 19784 RVA: 0x001B3584 File Offset: 0x001B1784
	private void ShowSaveInfo()
	{
		if (this.infoScreen == null)
		{
			this.infoScreen = global::Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, base.gameObject, false).SetHeader(UI.FRONTEND.LOADSCREEN.SAVE_INFO_DIALOG_TITLE).AddSprite(this.infoSprite)
				.AddPlainText(UI.FRONTEND.LOADSCREEN.SAVE_INFO_DIALOG_TEXT)
				.AddOption(UI.FRONTEND.LOADSCREEN.OPEN_SAVE_FOLDER, delegate(InfoDialogScreen d)
				{
					App.OpenWebURL(SaveLoader.GetSavePrefix());
				}, true)
				.AddDefaultCancel();
			string cloudRoot = SaveLoader.GetCloudSavePrefix();
			if (cloudRoot != null && this.CloudSavesVisible())
			{
				this.infoScreen.AddOption(UI.FRONTEND.LOADSCREEN.OPEN_CLOUDSAVE_FOLDER, delegate(InfoDialogScreen d)
				{
					App.OpenWebURL(cloudRoot);
				}, true);
			}
			this.infoScreen.gameObject.SetActive(true);
		}
	}

	// Token: 0x06004D49 RID: 19785 RVA: 0x001B3675 File Offset: 0x001B1875
	protected override void OnDeactivate()
	{
		if (SpeedControlScreen.Instance != null)
		{
			SpeedControlScreen.Instance.Unpause(false);
		}
		this.selectedSave = null;
		base.OnDeactivate();
	}

	// Token: 0x06004D4A RID: 19786 RVA: 0x001B369C File Offset: 0x001B189C
	private void ShowColonyList()
	{
		this.colonyListRoot.SetActive(true);
		this.colonyViewRoot.SetActive(false);
		this.currentColony = null;
		this.selectedSave = null;
	}

	// Token: 0x06004D4B RID: 19787 RVA: 0x001B36C4 File Offset: 0x001B18C4
	private bool CheckSave(LoadScreen.SaveGameFileDetails save, LocText display)
	{
		if (LoadScreen.IsSaveFileFromUninstalledDLC(save.FileInfo) && display != null)
		{
			display.text = string.Format(UI.FRONTEND.LOADSCREEN.SAVE_MISSING_CONTENT, save.FileName);
		}
		if (LoadScreen.IsSaveFileFromUnsupportedFutureBuild(save.FileHeader, save.FileInfo))
		{
			if (display != null)
			{
				display.text = string.Format(UI.FRONTEND.LOADSCREEN.SAVE_TOO_NEW, new object[]
				{
					save.FileName,
					save.FileHeader.buildVersion,
					save.FileInfo.saveMinorVersion,
					544519U,
					31
				});
			}
			return false;
		}
		if (save.FileInfo.saveMajorVersion < 7)
		{
			if (display != null)
			{
				display.text = string.Format(UI.FRONTEND.LOADSCREEN.UNSUPPORTED_SAVE_VERSION, new object[]
				{
					save.FileName,
					save.FileInfo.saveMajorVersion,
					save.FileInfo.saveMinorVersion,
					7,
					31
				});
			}
			return false;
		}
		return true;
	}

	// Token: 0x06004D4C RID: 19788 RVA: 0x001B37F8 File Offset: 0x001B19F8
	private void ShowColonySave(LoadScreen.SaveGameFileDetails save)
	{
		HierarchyReferences component = this.colonyViewRoot.GetComponent<HierarchyReferences>();
		component.GetReference<RectTransform>("Title").GetComponent<LocText>().text = save.BaseName;
		component.GetReference<RectTransform>("Date").GetComponent<LocText>().text = string.Format("{0:H:mm:ss} - " + Localization.GetFileDateFormat(0), save.FileDate.ToLocalTime());
		string text = save.FileInfo.clusterId;
		if (text != null && !SettingsCache.clusterLayouts.clusterCache.ContainsKey(text))
		{
			string text2 = SettingsCache.GetScope("EXPANSION1_ID") + text;
			if (SettingsCache.clusterLayouts.clusterCache.ContainsKey(text2))
			{
				text = text2;
			}
			else
			{
				DebugUtil.LogWarningArgs(new object[] { "Failed to find cluster " + text + " including the scoped path, setting to default cluster name." });
				global::Debug.Log("ClusterCache: " + string.Join(",", SettingsCache.clusterLayouts.clusterCache.Keys));
				text = WorldGenSettings.ClusterDefaultName;
			}
		}
		ProcGen.World world = ((text != null) ? SettingsCache.clusterLayouts.GetWorldData(text, 0) : null);
		string text3 = ((world != null) ? Strings.Get(world.name) : " - ");
		component.GetReference<LocText>("InfoWorld").text = string.Format(UI.FRONTEND.LOADSCREEN.COLONY_INFO_FMT, UI.FRONTEND.LOADSCREEN.WORLD_NAME, text3);
		component.GetReference<LocText>("InfoCycles").text = string.Format(UI.FRONTEND.LOADSCREEN.COLONY_INFO_FMT, UI.FRONTEND.LOADSCREEN.CYCLES_SURVIVED, save.FileInfo.numberOfCycles);
		component.GetReference<LocText>("InfoDupes").text = string.Format(UI.FRONTEND.LOADSCREEN.COLONY_INFO_FMT, UI.FRONTEND.LOADSCREEN.DUPLICANTS_ALIVE, save.FileInfo.numberOfDuplicants);
		TMP_Text component2 = component.GetReference<RectTransform>("FileSize").GetComponent<LocText>();
		string formattedBytes = GameUtil.GetFormattedBytes((ulong)save.Size);
		component2.text = string.Format(UI.FRONTEND.LOADSCREEN.COLONY_FILE_SIZE, formattedBytes);
		component.GetReference<RectTransform>("Filename").GetComponent<LocText>().text = string.Format(UI.FRONTEND.LOADSCREEN.COLONY_FILE_NAME, System.IO.Path.GetFileName(save.FileName));
		LocText component3 = component.GetReference<RectTransform>("AutoInfo").GetComponent<LocText>();
		component3.gameObject.SetActive(!this.CheckSave(save, component3));
		Image component4 = component.GetReference<RectTransform>("Preview").GetComponent<Image>();
		this.SetPreview(save.FileName, save.BaseName, component4, false);
		KButton component5 = component.GetReference<RectTransform>("DeleteButton").GetComponent<KButton>();
		component5.ClearOnClick();
		System.Action <>9__1;
		component5.onClick += delegate
		{
			LoadScreen <>4__this = this;
			System.Action action;
			if ((action = <>9__1) == null)
			{
				action = (<>9__1 = delegate
				{
					int num = this.currentColony.IndexOf(save);
					this.currentColony.Remove(save);
					this.ShowColony(this.currentColony, num - 1);
				});
			}
			<>4__this.Delete(action);
		};
	}

	// Token: 0x06004D4D RID: 19789 RVA: 0x001B3AE0 File Offset: 0x001B1CE0
	private void ShowColony(List<LoadScreen.SaveGameFileDetails> saves, int selectIndex = -1)
	{
		if (saves.Count <= 0)
		{
			this.RefreshColonyList();
			this.ShowColonyList();
			return;
		}
		this.currentColony = saves;
		this.colonyListRoot.SetActive(false);
		this.colonyViewRoot.SetActive(true);
		string baseName = saves[0].BaseName;
		HierarchyReferences component = this.colonyViewRoot.GetComponent<HierarchyReferences>();
		KButton component2 = component.GetReference<RectTransform>("Back").GetComponent<KButton>();
		component2.ClearOnClick();
		component2.onClick += delegate
		{
			this.ShowColonyList();
		};
		component.GetReference<RectTransform>("ColonyTitle").GetComponent<LocText>().text = string.Format(UI.FRONTEND.LOADSCREEN.COLONY_TITLE, baseName);
		GameObject gameObject = component.GetReference<RectTransform>("Content").gameObject;
		RectTransform reference = component.GetReference<RectTransform>("SaveTemplate");
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			GameObject gameObject2 = gameObject.transform.GetChild(i).gameObject;
			if (gameObject2 != null && gameObject2.name.Contains("Clone"))
			{
				UnityEngine.Object.Destroy(gameObject2);
			}
		}
		if (selectIndex < 0)
		{
			selectIndex = 0;
		}
		if (selectIndex > saves.Count - 1)
		{
			selectIndex = saves.Count - 1;
		}
		for (int j = 0; j < saves.Count; j++)
		{
			LoadScreen.SaveGameFileDetails save = saves[j];
			RectTransform rectTransform = UnityEngine.Object.Instantiate<RectTransform>(reference, gameObject.transform);
			HierarchyReferences component3 = rectTransform.GetComponent<HierarchyReferences>();
			rectTransform.gameObject.SetActive(true);
			component3.GetReference<RectTransform>("AutoLabel").gameObject.SetActive(save.FileInfo.isAutoSave);
			component3.GetReference<RectTransform>("SaveText").GetComponent<LocText>().text = System.IO.Path.GetFileNameWithoutExtension(save.FileName);
			component3.GetReference<RectTransform>("DateText").GetComponent<LocText>().text = string.Format("{0:H:mm:ss} - " + Localization.GetFileDateFormat(0), save.FileDate.ToLocalTime());
			component3.GetReference<RectTransform>("NewestLabel").gameObject.SetActive(j == 0);
			bool flag = this.CheckSave(save, null);
			KButton button = rectTransform.GetComponent<KButton>();
			button.ClearOnClick();
			button.onClick += delegate
			{
				this.UpdateSelected(button, save.FileName, save.FileInfo.dlcId);
				this.ShowColonySave(save);
			};
			if (flag)
			{
				button.onDoubleClick += delegate
				{
					this.UpdateSelected(button, save.FileName, save.FileInfo.dlcId);
					this.Load();
				};
			}
			KButton component4 = component3.GetReference<RectTransform>("LoadButton").GetComponent<KButton>();
			component4.ClearOnClick();
			if (!flag)
			{
				component4.isInteractable = false;
				component4.GetComponent<ImageToggleState>().SetState(ImageToggleState.State.Disabled);
			}
			else
			{
				component4.onClick += delegate
				{
					this.UpdateSelected(button, save.FileName, save.FileInfo.dlcId);
					this.Load();
				};
			}
			if (j == selectIndex)
			{
				this.UpdateSelected(button, save.FileName, save.FileInfo.dlcId);
				this.ShowColonySave(save);
			}
		}
	}

	// Token: 0x06004D4E RID: 19790 RVA: 0x001B3DF4 File Offset: 0x001B1FF4
	private void AddColonyToList(List<LoadScreen.SaveGameFileDetails> saves)
	{
		if (saves.Count == 0)
		{
			return;
		}
		HierarchyReferences freeElement = this.colonyListPool.GetFreeElement(this.saveButtonRoot, true);
		saves.Sort((LoadScreen.SaveGameFileDetails x, LoadScreen.SaveGameFileDetails y) => y.FileDate.CompareTo(x.FileDate));
		LoadScreen.SaveGameFileDetails firstSave = saves[0];
		string text;
		bool flag = LoadScreen.IsSaveFromCurrentDLC(firstSave.FileInfo, out text);
		string colonyName = firstSave.BaseName;
		ValueTuple<int, int, ulong> savesSizeAndCounts = this.GetSavesSizeAndCounts(saves);
		int item = savesSizeAndCounts.Item1;
		int item2 = savesSizeAndCounts.Item2;
		string formattedBytes = GameUtil.GetFormattedBytes(savesSizeAndCounts.Item3);
		freeElement.GetReference<RectTransform>("HeaderTitle").GetComponent<LocText>().text = colonyName;
		freeElement.GetReference<RectTransform>("HeaderDate").GetComponent<LocText>().text = string.Format("{0:H:mm:ss} - " + Localization.GetFileDateFormat(0), firstSave.FileDate.ToLocalTime());
		freeElement.GetReference<RectTransform>("SaveTitle").GetComponent<LocText>().text = string.Format(UI.FRONTEND.LOADSCREEN.SAVE_INFO, item, item2, formattedBytes);
		Image component = freeElement.GetReference<RectTransform>("Preview").GetComponent<Image>();
		this.SetPreview(firstSave.FileName, colonyName, component, true);
		KImage reference = freeElement.GetReference<KImage>("DlcIcon");
		if (firstSave.FileInfo.dlcId == "EXPANSION1_ID")
		{
			reference.enabled = true;
			reference.GetComponent<ToolTip>().SetSimpleTooltip(UI.FRONTEND.LOADSCREEN.SAVE_FROM_SPACED_OUT_TOOLTIP);
		}
		else
		{
			reference.enabled = false;
		}
		Component reference2 = freeElement.GetReference<RectTransform>("LocationIcons");
		bool flag2 = this.CloudSavesVisible();
		reference2.gameObject.SetActive(flag2);
		if (flag2)
		{
			LocText locationText = freeElement.GetReference<RectTransform>("LocationText").GetComponent<LocText>();
			bool isLocal = SaveLoader.IsSaveLocal(firstSave.FileName);
			locationText.text = (isLocal ? UI.FRONTEND.LOADSCREEN.LOCAL_SAVE : UI.FRONTEND.LOADSCREEN.CLOUD_SAVE);
			KButton cloudButton = freeElement.GetReference<RectTransform>("CloudButton").GetComponent<KButton>();
			KButton localButton = freeElement.GetReference<RectTransform>("LocalButton").GetComponent<KButton>();
			cloudButton.gameObject.SetActive(!isLocal);
			cloudButton.ClearOnClick();
			System.Action <>9__5;
			cloudButton.onClick += delegate
			{
				string text2 = string.Format("{0}\n", UI.FRONTEND.LOADSCREEN.CONVERT_TO_LOCAL_DETAILS);
				LoadScreen <>4__this = this;
				string text3 = text2;
				string text4 = UI.FRONTEND.LOADSCREEN.CONVERT_TO_LOCAL;
				string text5 = UI.FRONTEND.LOADSCREEN.CONVERT_COLONY;
				string text6 = null;
				System.Action action;
				if ((action = <>9__5) == null)
				{
					action = (<>9__5 = delegate
					{
						cloudButton.gameObject.SetActive(false);
						isLocal = true;
						locationText.text = (isLocal ? UI.FRONTEND.LOADSCREEN.LOCAL_SAVE : UI.FRONTEND.LOADSCREEN.CLOUD_SAVE);
						this.ConvertColonyToLocal(colonyName);
						this.RefreshColonyList();
						MainMenu.Instance.RefreshResumeButton(false);
					});
				}
				<>4__this.ConfirmCloudSaveMigrations(text3, text4, text5, text6, action, null, this.cloudToLocalSprite);
			};
			localButton.gameObject.SetActive(isLocal);
			localButton.ClearOnClick();
			System.Action <>9__6;
			localButton.onClick += delegate
			{
				string text7 = string.Format("{0}\n", UI.FRONTEND.LOADSCREEN.CONVERT_TO_CLOUD_DETAILS);
				LoadScreen <>4__this2 = this;
				string text8 = text7;
				string text9 = UI.FRONTEND.LOADSCREEN.CONVERT_TO_CLOUD;
				string text10 = UI.FRONTEND.LOADSCREEN.CONVERT_COLONY;
				string text11 = null;
				System.Action action2;
				if ((action2 = <>9__6) == null)
				{
					action2 = (<>9__6 = delegate
					{
						localButton.gameObject.SetActive(false);
						isLocal = false;
						locationText.text = (isLocal ? UI.FRONTEND.LOADSCREEN.LOCAL_SAVE : UI.FRONTEND.LOADSCREEN.CLOUD_SAVE);
						this.ConvertColonyToCloud(colonyName);
						this.RefreshColonyList();
						MainMenu.Instance.RefreshResumeButton(false);
					});
				}
				<>4__this2.ConfirmCloudSaveMigrations(text8, text9, text10, text11, action2, null, this.localToCloudSprite);
			};
		}
		KButton component2 = freeElement.GetReference<RectTransform>("Button").GetComponent<KButton>();
		component2.ClearOnClick();
		component2.isInteractable = flag;
		component2.onClick += delegate
		{
			this.ShowColony(saves, -1);
		};
		if (this.CheckSave(firstSave, null))
		{
			component2.onDoubleClick += delegate
			{
				this.UpdateSelected(null, firstSave.FileName, firstSave.FileInfo.dlcId);
				this.Load();
			};
		}
		freeElement.transform.SetAsLastSibling();
	}

	// Token: 0x06004D4F RID: 19791 RVA: 0x001B4164 File Offset: 0x001B2364
	private void SetPreview(string filename, string basename, Image preview, bool fallbackToTimelapse = false)
	{
		preview.color = Color.black;
		preview.gameObject.SetActive(false);
		try
		{
			Sprite sprite = RetireColonyUtility.LoadColonyPreview(filename, basename, fallbackToTimelapse);
			if (!(sprite == null))
			{
				Rect rect = preview.rectTransform.parent.rectTransform().rect;
				preview.sprite = sprite;
				preview.color = (sprite ? Color.white : Color.black);
				float num = sprite.bounds.size.x / sprite.bounds.size.y;
				if ((double)num >= 1.77777777777778)
				{
					preview.rectTransform.sizeDelta = new Vector2(rect.height * num, rect.height);
				}
				else
				{
					preview.rectTransform.sizeDelta = new Vector2(rect.width, rect.width / num);
				}
				preview.gameObject.SetActive(true);
			}
		}
		catch (Exception ex)
		{
			global::Debug.Log(ex);
		}
	}

	// Token: 0x06004D50 RID: 19792 RVA: 0x001B4274 File Offset: 0x001B2474
	public static void ForceStopGame()
	{
		ThreadedHttps<KleiMetrics>.Instance.SendProfileStats();
		Game.Instance.SetIsLoading();
		Grid.CellCount = 0;
		Sim.Shutdown();
	}

	// Token: 0x06004D51 RID: 19793 RVA: 0x001B4296 File Offset: 0x001B2496
	private static bool IsSaveFileFromUnsupportedFutureBuild(SaveGame.Header header, SaveGame.GameInfo gameInfo)
	{
		return gameInfo.saveMajorVersion > 7 || (gameInfo.saveMajorVersion == 7 && gameInfo.saveMinorVersion > 31) || header.buildVersion > 544519U;
	}

	// Token: 0x06004D52 RID: 19794 RVA: 0x001B42C4 File Offset: 0x001B24C4
	private static bool IsSaveFromCurrentDLC(SaveGame.GameInfo gameInfo, out string saveDlcName)
	{
		string dlcId = gameInfo.dlcId;
		if (dlcId != null)
		{
			if (dlcId == "EXPANSION1_ID")
			{
				saveDlcName = UI.DLC1.NAME_ITAL;
				goto IL_3E;
			}
			if (dlcId != null && dlcId.Length != 0)
			{
			}
		}
		saveDlcName = UI.VANILLA.NAME_ITAL;
		IL_3E:
		return gameInfo.dlcId == DlcManager.GetHighestActiveDlcId();
	}

	// Token: 0x06004D53 RID: 19795 RVA: 0x001B431F File Offset: 0x001B251F
	private static bool IsSaveFileFromUninstalledDLC(SaveGame.GameInfo gameInfo)
	{
		return DlcManager.IsContentActive(gameInfo.dlcId);
	}

	// Token: 0x06004D54 RID: 19796 RVA: 0x001B432C File Offset: 0x001B252C
	private void UpdateSelected(KButton button, string filename, string dlcId)
	{
		if (this.selectedSave != null && this.selectedSave.button != null)
		{
			this.selectedSave.button.GetComponent<ImageToggleState>().SetState(ImageToggleState.State.Inactive);
		}
		if (this.selectedSave == null)
		{
			this.selectedSave = new LoadScreen.SelectedSave();
		}
		this.selectedSave.button = button;
		this.selectedSave.filename = filename;
		this.selectedSave.dlcId = dlcId;
		if (this.selectedSave.button != null)
		{
			this.selectedSave.button.GetComponent<ImageToggleState>().SetState(ImageToggleState.State.Active);
		}
	}

	// Token: 0x06004D55 RID: 19797 RVA: 0x001B43CC File Offset: 0x001B25CC
	private void Load()
	{
		if (this.selectedSave.dlcId != DlcManager.GetHighestActiveDlcId())
		{
			string text = (DlcManager.IsVanillaId(this.selectedSave.dlcId) ? UI.FRONTEND.LOADSCREEN.VANILLA_RESTART : UI.FRONTEND.LOADSCREEN.EXPANSION1_RESTART);
			this.ConfirmDoAction(text, delegate
			{
				KPlayerPrefs.SetString("AutoResumeSaveFile", this.selectedSave.filename);
				DlcManager.ToggleDLC("EXPANSION1_ID");
			});
			return;
		}
		LoadingOverlay.Load(new System.Action(this.DoLoad));
	}

	// Token: 0x06004D56 RID: 19798 RVA: 0x001B4439 File Offset: 0x001B2639
	private void DoLoad()
	{
		if (this.selectedSave == null)
		{
			return;
		}
		LoadScreen.DoLoad(this.selectedSave.filename);
		this.Deactivate();
	}

	// Token: 0x06004D57 RID: 19799 RVA: 0x001B445C File Offset: 0x001B265C
	private static void DoLoad(string filename)
	{
		KCrashReporter.MOST_RECENT_SAVEFILE = filename;
		bool flag = true;
		SaveGame.Header header;
		SaveGame.GameInfo gameInfo = SaveLoader.LoadHeader(filename, out header);
		string text = null;
		string text2 = null;
		if (header.buildVersion > 544519U)
		{
			text = header.buildVersion.ToString();
			text2 = 544519U.ToString();
		}
		else if (gameInfo.saveMajorVersion < 7)
		{
			text = string.Format("v{0}.{1}", gameInfo.saveMajorVersion, gameInfo.saveMinorVersion);
			text2 = string.Format("v{0}.{1}", 7, 31);
		}
		if (!flag)
		{
			GameObject gameObject = ((FrontEndManager.Instance == null) ? GameScreenManager.Instance.ssOverlayCanvas : FrontEndManager.Instance.gameObject);
			global::Util.KInstantiateUI(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, gameObject, true).GetComponent<ConfirmDialogScreen>().PopupConfirmDialog(string.Format(UI.CRASHSCREEN.LOADFAILED, "Version Mismatch", text, text2), null, null, null, null, null, null, null, null);
			return;
		}
		if (Game.Instance != null)
		{
			LoadScreen.ForceStopGame();
		}
		SaveLoader.SetActiveSaveFilePath(filename);
		Time.timeScale = 0f;
		App.LoadScene("backend");
	}

	// Token: 0x06004D58 RID: 19800 RVA: 0x001B457D File Offset: 0x001B277D
	private void MoreInfo()
	{
		App.OpenWebURL("http://support.kleientertainment.com/customer/portal/articles/2776550");
	}

	// Token: 0x06004D59 RID: 19801 RVA: 0x001B458C File Offset: 0x001B278C
	private void Delete(System.Action onDelete)
	{
		if (this.selectedSave == null || string.IsNullOrEmpty(this.selectedSave.filename))
		{
			global::Debug.LogError("The path provided is not valid and cannot be deleted.");
			return;
		}
		this.ConfirmDoAction(string.Format(UI.FRONTEND.LOADSCREEN.CONFIRMDELETE, System.IO.Path.GetFileName(this.selectedSave.filename)), delegate
		{
			try
			{
				this.DeleteFileAndEmptyFolder(this.selectedSave.filename);
				string text = System.IO.Path.ChangeExtension(this.selectedSave.filename, "png");
				this.DeleteFileAndEmptyFolder(text);
				if (onDelete != null)
				{
					onDelete();
				}
				MainMenu.Instance.RefreshResumeButton(false);
			}
			catch (SystemException ex)
			{
				global::Debug.LogError(ex.ToString());
			}
		});
	}

	// Token: 0x06004D5A RID: 19802 RVA: 0x001B4603 File Offset: 0x001B2803
	private void ShowSimpleDialog(string title, string message)
	{
		global::Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, base.gameObject, false).SetHeader(title).AddPlainText(message)
			.AddDefaultOK(false)
			.Activate();
	}

	// Token: 0x06004D5B RID: 19803 RVA: 0x001B4638 File Offset: 0x001B2838
	private void ConfirmCloudSaveMigrations(string message, string title, string confirmText, string backupText, System.Action commitAction, System.Action backupAction, Sprite sprite)
	{
		global::Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, base.gameObject, false).SetHeader(title).AddSprite(sprite)
			.AddPlainText(message)
			.AddDefaultCancel()
			.AddOption(confirmText, delegate(InfoDialogScreen d)
			{
				d.Deactivate();
				commitAction();
			}, true)
			.Activate();
	}

	// Token: 0x06004D5C RID: 19804 RVA: 0x001B46A0 File Offset: 0x001B28A0
	private void ShowConvertError(string message)
	{
		if (this.errorInfoScreen == null)
		{
			if (DistributionPlatform.Initialized && SteamUtils.IsSteamRunningOnSteamDeck())
			{
				this.errorInfoScreen = global::Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, base.gameObject, false).SetHeader(UI.FRONTEND.LOADSCREEN.CONVERT_ERROR_TITLE).AddSprite(this.errorSprite)
					.AddPlainText(message)
					.AddDefaultOK(false);
				this.errorInfoScreen.Activate();
				return;
			}
			this.errorInfoScreen = global::Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, base.gameObject, false).SetHeader(UI.FRONTEND.LOADSCREEN.CONVERT_ERROR_TITLE).AddSprite(this.errorSprite)
				.AddPlainText(message)
				.AddOption(UI.FRONTEND.LOADSCREEN.MIGRATE_FAILURES_FORUM_BUTTON, delegate(InfoDialogScreen d)
				{
					App.OpenWebURL("https://forums.kleientertainment.com/klei-bug-tracker/oni/");
				}, false)
				.AddDefaultOK(false);
			this.errorInfoScreen.Activate();
		}
	}

	// Token: 0x06004D5D RID: 19805 RVA: 0x001B47A0 File Offset: 0x001B29A0
	private void ConfirmDoAction(string message, System.Action action)
	{
		if (this.confirmScreen == null)
		{
			this.confirmScreen = global::Util.KInstantiateUI<ConfirmDialogScreen>(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, base.gameObject, false);
			this.confirmScreen.PopupConfirmDialog(message, action, delegate
			{
			}, null, null, null, null, null, null);
			this.confirmScreen.gameObject.SetActive(true);
		}
	}

	// Token: 0x06004D5E RID: 19806 RVA: 0x001B481F File Offset: 0x001B2A1F
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.currentColony != null && e.TryConsume(global::Action.Escape))
		{
			this.ShowColonyList();
		}
		base.OnKeyDown(e);
	}

	// Token: 0x040032E0 RID: 13024
	private const int MAX_CLOUD_TUTORIALS = 5;

	// Token: 0x040032E1 RID: 13025
	private const string CLOUD_TUTORIAL_KEY = "LoadScreenCloudTutorialTimes";

	// Token: 0x040032E2 RID: 13026
	[SerializeField]
	private KButton closeButton;

	// Token: 0x040032E3 RID: 13027
	[SerializeField]
	private GameObject saveButtonRoot;

	// Token: 0x040032E4 RID: 13028
	[SerializeField]
	private GameObject colonyListRoot;

	// Token: 0x040032E5 RID: 13029
	[SerializeField]
	private GameObject colonyViewRoot;

	// Token: 0x040032E6 RID: 13030
	[SerializeField]
	private HierarchyReferences migrationPanelRefs;

	// Token: 0x040032E7 RID: 13031
	[SerializeField]
	private HierarchyReferences saveButtonPrefab;

	// Token: 0x040032E8 RID: 13032
	[Space]
	[SerializeField]
	private KButton colonyCloudButton;

	// Token: 0x040032E9 RID: 13033
	[SerializeField]
	private KButton colonyLocalButton;

	// Token: 0x040032EA RID: 13034
	[SerializeField]
	private KButton colonyInfoButton;

	// Token: 0x040032EB RID: 13035
	[SerializeField]
	private Sprite localToCloudSprite;

	// Token: 0x040032EC RID: 13036
	[SerializeField]
	private Sprite cloudToLocalSprite;

	// Token: 0x040032ED RID: 13037
	[SerializeField]
	private Sprite errorSprite;

	// Token: 0x040032EE RID: 13038
	[SerializeField]
	private Sprite infoSprite;

	// Token: 0x040032EF RID: 13039
	[SerializeField]
	private Bouncer cloudTutorialBouncer;

	// Token: 0x040032F0 RID: 13040
	public bool requireConfirmation = true;

	// Token: 0x040032F1 RID: 13041
	private LoadScreen.SelectedSave selectedSave;

	// Token: 0x040032F2 RID: 13042
	private List<LoadScreen.SaveGameFileDetails> currentColony;

	// Token: 0x040032F3 RID: 13043
	private UIPool<HierarchyReferences> colonyListPool;

	// Token: 0x040032F4 RID: 13044
	private ConfirmDialogScreen confirmScreen;

	// Token: 0x040032F5 RID: 13045
	private InfoDialogScreen infoScreen;

	// Token: 0x040032F6 RID: 13046
	private InfoDialogScreen errorInfoScreen;

	// Token: 0x040032F7 RID: 13047
	private ConfirmDialogScreen errorScreen;

	// Token: 0x040032F8 RID: 13048
	private InspectSaveScreen inspectScreenInstance;

	// Token: 0x02001815 RID: 6165
	private struct SaveGameFileDetails
	{
		// Token: 0x04006F07 RID: 28423
		public string BaseName;

		// Token: 0x04006F08 RID: 28424
		public string FileName;

		// Token: 0x04006F09 RID: 28425
		public string UniqueID;

		// Token: 0x04006F0A RID: 28426
		public System.DateTime FileDate;

		// Token: 0x04006F0B RID: 28427
		public SaveGame.Header FileHeader;

		// Token: 0x04006F0C RID: 28428
		public SaveGame.GameInfo FileInfo;

		// Token: 0x04006F0D RID: 28429
		public long Size;
	}

	// Token: 0x02001816 RID: 6166
	private class SelectedSave
	{
		// Token: 0x04006F0E RID: 28430
		public string filename;

		// Token: 0x04006F0F RID: 28431
		public string dlcId;

		// Token: 0x04006F10 RID: 28432
		public KButton button;
	}
}
