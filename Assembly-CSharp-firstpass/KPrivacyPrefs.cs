using System;
using System.IO;
using Klei;

// Token: 0x020000C4 RID: 196
public class KPrivacyPrefs
{
	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x06000777 RID: 1911 RVA: 0x0001F59E File Offset: 0x0001D79E
	// (set) Token: 0x06000778 RID: 1912 RVA: 0x0001F5A6 File Offset: 0x0001D7A6
	public bool disableDataCollection { get; set; }

	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x06000779 RID: 1913 RVA: 0x0001F5AF File Offset: 0x0001D7AF
	public static KPrivacyPrefs instance
	{
		get
		{
			if (KPrivacyPrefs._instance == null)
			{
				KPrivacyPrefs.Load();
			}
			return KPrivacyPrefs._instance;
		}
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x0001F5C2 File Offset: 0x0001D7C2
	public static string GetPath()
	{
		return Path.Combine(KPrivacyPrefs.GetDirectory(), KPrivacyPrefs.FILENAME);
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x0001F5D3 File Offset: 0x0001D7D3
	public static string GetDirectory()
	{
		return Path.Combine(Path.Combine(Util.GetKleiRootPath(), "Agreements"), Util.GetTitleFolderName());
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x0001F5F0 File Offset: 0x0001D7F0
	public static void Save()
	{
		try
		{
			if (!Directory.Exists(KPrivacyPrefs.GetDirectory()))
			{
				Directory.CreateDirectory(KPrivacyPrefs.GetDirectory());
			}
			YamlIO.SaveOrWarnUser<KPrivacyPrefs>(KPrivacyPrefs.instance, KPrivacyPrefs.GetPath(), null);
		}
		catch (Exception ex)
		{
			KPrivacyPrefs.LogError(ex.ToString());
		}
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x0001F644 File Offset: 0x0001D844
	public static void Load()
	{
		try
		{
			if (KPrivacyPrefs._instance == null)
			{
				KPrivacyPrefs._instance = new KPrivacyPrefs();
			}
			string path = KPrivacyPrefs.GetPath();
			if (File.Exists(path))
			{
				KPrivacyPrefs._instance = YamlIO.LoadFile<KPrivacyPrefs>(path, null, null);
				if (KPrivacyPrefs._instance == null)
				{
					KPrivacyPrefs.LogError("Exception while loading privacy prefs:" + path);
					KPrivacyPrefs._instance = new KPrivacyPrefs();
				}
			}
		}
		catch (Exception ex)
		{
			KPrivacyPrefs.LogError(ex.ToString());
		}
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x0001F6C0 File Offset: 0x0001D8C0
	private static void LogError(string msg)
	{
		Debug.LogWarning(msg);
	}

	// Token: 0x040005FD RID: 1533
	private static KPrivacyPrefs _instance;

	// Token: 0x040005FF RID: 1535
	public static readonly string FILENAME = "kprivacyprefs.yaml";
}
