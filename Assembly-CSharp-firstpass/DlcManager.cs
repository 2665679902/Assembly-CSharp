using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000095 RID: 149
public class DlcManager
{
	// Token: 0x060005AC RID: 1452 RVA: 0x0001ABB2 File Offset: 0x00018DB2
	public static void ClearCachedValues()
	{
		DlcManager.dlcPurchasedCache = new Dictionary<string, bool>();
		DlcManager.dlcSubscribedCache = new Dictionary<string, bool>();
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x0001ABC8 File Offset: 0x00018DC8
	public static bool IsVanillaId(string dlcId)
	{
		return dlcId == null || dlcId == "";
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x0001ABDA File Offset: 0x00018DDA
	public static bool IsVanillaId(string[] dlcIds)
	{
		return dlcIds == null || (dlcIds.Length == 1 && dlcIds[0] == "");
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x0001ABF6 File Offset: 0x00018DF6
	public static bool IsValidForVanilla(string[] dlcIds)
	{
		return dlcIds == null || Array.IndexOf<string>(dlcIds, "") != -1;
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x0001AC0E File Offset: 0x00018E0E
	public static bool IsExpansion1Id(string dlcId)
	{
		return dlcId == "EXPANSION1_ID";
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x0001AC1B File Offset: 0x00018E1B
	public static string GetContentBundleName(string dlcId)
	{
		if (dlcId != null && dlcId == "EXPANSION1_ID")
		{
			return "expansion1_bundle";
		}
		global::Debug.LogError("No bundle exists for " + dlcId);
		return null;
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x0001AC44 File Offset: 0x00018E44
	public static string GetContentDirectoryName(string dlcId)
	{
		if (dlcId != null)
		{
			if (dlcId != null && dlcId.Length == 0)
			{
				return "";
			}
			if (dlcId == "EXPANSION1_ID")
			{
				return "expansion1";
			}
		}
		global::Debug.LogError("No content directory name exists for " + dlcId);
		return null;
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x0001AC80 File Offset: 0x00018E80
	public static string GetDlcIdFromContentDirectory(string contentDirectory)
	{
		if (contentDirectory != null)
		{
			if (contentDirectory != null && contentDirectory.Length == 0)
			{
				return "";
			}
			if (contentDirectory == "expansion1")
			{
				return "EXPANSION1_ID";
			}
		}
		global::Debug.LogError("No dlcId matches content directory " + contentDirectory);
		return null;
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x0001ACBC File Offset: 0x00018EBC
	public static void ToggleDLC(string id)
	{
		DlcManager.SetContentSettingEnabled(id, !DlcManager.IsContentSettingEnabled(id));
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x0001ACCD File Offset: 0x00018ECD
	public static bool IsContentActive(string dlcId)
	{
		return DlcManager.CheckPlatformSubscription(dlcId) && DlcManager.IsContentSettingEnabled(dlcId);
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x0001ACE0 File Offset: 0x00018EE0
	public static bool IsDlcListValidForCurrentContent(string[] dlcIds)
	{
		if (DlcManager.GetHighestActiveDlcId() == "")
		{
			return Array.IndexOf<string>(dlcIds, "") != -1;
		}
		foreach (string text in dlcIds)
		{
			if (!(text == "") && DlcManager.IsContentActive(text))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x0001AD3C File Offset: 0x00018F3C
	public static string GetHighestActiveDlcId()
	{
		for (int i = DlcManager.RELEASE_ORDER.Count - 1; i >= 0; i--)
		{
			string text = DlcManager.RELEASE_ORDER[i];
			if (DlcManager.CheckPlatformSubscription(text) && DlcManager.IsContentSettingEnabled(text))
			{
				return text;
			}
		}
		return "";
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x0001AD83 File Offset: 0x00018F83
	private static string GetInstalledDlcId()
	{
		if (DlcManager.CheckPlatformSubscription("EXPANSION1_ID"))
		{
			return "EXPANSION1_ID";
		}
		return "";
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x0001AD9C File Offset: 0x00018F9C
	private static bool CheckPlatformSubscription(string dlcId)
	{
		if (dlcId == null || dlcId == "")
		{
			return true;
		}
		if (Application.isEditor && (!DlcManager.IsMainThread || !Application.isPlaying))
		{
			return true;
		}
		bool flag;
		if (!DlcManager.dlcSubscribedCache.TryGetValue(dlcId, out flag))
		{
			flag = DistributionPlatform.Inst.IsDLCSubscribed(dlcId);
			DlcManager.dlcSubscribedCache[dlcId] = flag;
		}
		bool flag2 = DlcManager.CheckForExpansionFileExistence();
		return flag && flag2;
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x0001AE04 File Offset: 0x00019004
	private static bool CheckForExpansionFileExistence()
	{
		string text = Path.Combine(Application.streamingAssetsPath, "expansion1_bundle");
		bool flag = false;
		try
		{
			flag = File.Exists(text);
		}
		catch (Exception ex)
		{
			global::Debug.Log("[DlcManager] Error at reading file. CheckPlatformSubscription() - " + ex.Message);
		}
		return flag;
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x0001AE58 File Offset: 0x00019058
	private static bool IsContentSettingEnabled(string dlcId)
	{
		return dlcId == null || dlcId == "" || (DlcManager.CheckPlatformSubscription(dlcId) && KPlayerPrefs.GetInt(dlcId + ".ENABLED", 1) == 1);
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x0001AE8C File Offset: 0x0001908C
	private static bool IsContentOwned(string dlcId)
	{
		if (DlcManager.IsVanillaId(dlcId))
		{
			return true;
		}
		bool flag;
		if (!DlcManager.dlcPurchasedCache.TryGetValue(dlcId, out flag))
		{
			flag = DistributionPlatform.Inst.IsDLCPurchased(dlcId);
			DlcManager.dlcPurchasedCache[dlcId] = flag;
		}
		return flag;
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x0001AECC File Offset: 0x000190CC
	public static List<string> GetOwnedDLCIds()
	{
		List<string> list = new List<string>();
		for (int i = DlcManager.RELEASE_ORDER.Count - 1; i >= 0; i--)
		{
			string text = DlcManager.RELEASE_ORDER[i];
			if (!DlcManager.IsVanillaId(text) && DlcManager.IsContentOwned(text))
			{
				list.Add(text);
			}
		}
		return list;
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x0001AF1C File Offset: 0x0001911C
	public static List<string> GetActiveDLCIds()
	{
		List<string> list = new List<string>();
		for (int i = DlcManager.RELEASE_ORDER.Count - 1; i >= 0; i--)
		{
			string text = DlcManager.RELEASE_ORDER[i];
			if (!DlcManager.IsVanillaId(text) && DlcManager.IsContentActive(text))
			{
				list.Add(text);
			}
		}
		return list;
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x0001AF6A File Offset: 0x0001916A
	public static string GetContentLetter(string dlcId)
	{
		if (dlcId != null)
		{
			if (dlcId != null && dlcId.Length == 0)
			{
				return "V";
			}
			if (dlcId == "EXPANSION1_ID")
			{
				return "S";
			}
		}
		global::Debug.LogError("No content letter exists for " + dlcId);
		return null;
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x0001AFA8 File Offset: 0x000191A8
	public static string GetActiveContentLetters()
	{
		if (DlcManager.IsPureVanilla())
		{
			return DlcManager.GetContentLetter("");
		}
		string text = "";
		for (int i = 0; i < DlcManager.RELEASE_ORDER.Count; i++)
		{
			string text2 = DlcManager.RELEASE_ORDER[i];
			if (!DlcManager.IsVanillaId(text2) && DlcManager.IsContentActive(text2))
			{
				text += DlcManager.GetContentLetter(text2);
			}
		}
		return text;
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x0001B00C File Offset: 0x0001920C
	private static void SetContentSettingEnabled(string dlcId, bool enabled)
	{
		global::Debug.Assert(dlcId != "", "There is no KPlayerPrefs value for vanilla - it is always enabled");
		bool flag = Application.isEditor || DistributionPlatform.Inst.IsDLCPurchased(dlcId);
		if (enabled && !flag)
		{
			return;
		}
		DlcManager.dlcPurchasedCache.Clear();
		DlcManager.dlcSubscribedCache.Clear();
		KPlayerPrefs.SetInt(dlcId + ".ENABLED", enabled ? 1 : 0);
		if (enabled && !DlcManager.CheckPlatformSubscription(dlcId))
		{
			global::Debug.Log("ToggleDLCSubscription");
			DistributionPlatform.Inst.ToggleDLCSubscription(dlcId);
			return;
		}
		if (App.instance)
		{
			global::Debug.Log("Restart");
			App.instance.Restart();
		}
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x0001B0B7 File Offset: 0x000192B7
	public static bool IsPureVanilla()
	{
		return !DlcManager.IsExpansion1Active();
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x0001B0C1 File Offset: 0x000192C1
	public static bool IsExpansion1Installed()
	{
		return DlcManager.CheckPlatformSubscription("EXPANSION1_ID");
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x0001B0CD File Offset: 0x000192CD
	public static bool IsExpansion1Active()
	{
		return DlcManager.IsContentActive("EXPANSION1_ID");
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x0001B0D9 File Offset: 0x000192D9
	public static bool FeatureRadiationEnabled()
	{
		return DlcManager.IsExpansion1Active();
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x0001B0E0 File Offset: 0x000192E0
	public static bool FeaturePlantMutationsEnabled()
	{
		return DlcManager.IsExpansion1Active();
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x0001B0E7 File Offset: 0x000192E7
	public static bool FeatureClusterSpaceEnabled()
	{
		return DlcManager.IsExpansion1Active();
	}

	// Token: 0x0400057D RID: 1405
	[ThreadStatic]
	public static readonly bool IsMainThread = true;

	// Token: 0x0400057E RID: 1406
	public const string VANILLA_ID = "";

	// Token: 0x0400057F RID: 1407
	public const string EXPANSION1_ID = "EXPANSION1_ID";

	// Token: 0x04000580 RID: 1408
	public const string EXPANSION1_VERIFICATION_FILE_NAME = "expansion1_bundle";

	// Token: 0x04000581 RID: 1409
	public const string VANILLA_DIRECTORY = "";

	// Token: 0x04000582 RID: 1410
	public const string EXPANSION1_DIRECTORY = "expansion1";

	// Token: 0x04000583 RID: 1411
	public static readonly string[] AVAILABLE_VANILLA_ONLY = new string[] { "" };

	// Token: 0x04000584 RID: 1412
	public static readonly string[] AVAILABLE_EXPANSION1_ONLY = new string[] { "EXPANSION1_ID" };

	// Token: 0x04000585 RID: 1413
	public static readonly string[] AVAILABLE_ALL_VERSIONS = new string[] { "", "EXPANSION1_ID" };

	// Token: 0x04000586 RID: 1414
	public static List<string> RELEASE_ORDER = new List<string> { "", "EXPANSION1_ID" };

	// Token: 0x04000587 RID: 1415
	private static Dictionary<string, bool> dlcPurchasedCache = new Dictionary<string, bool>();

	// Token: 0x04000588 RID: 1416
	private static Dictionary<string, bool> dlcSubscribedCache = new Dictionary<string, bool>();
}
