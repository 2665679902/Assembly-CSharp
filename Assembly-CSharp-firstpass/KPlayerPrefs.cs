using System;
using System.Collections.Generic;
using System.IO;
using Klei;

// Token: 0x020000BF RID: 191
public class KPlayerPrefs
{
	// Token: 0x170000CD RID: 205
	// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001E8F4 File Offset: 0x0001CAF4
	public static KPlayerPrefs instance
	{
		get
		{
			if (KPlayerPrefs._instance == null)
			{
				try
				{
					KPlayerPrefs._instance = new KPlayerPrefs();
					KPlayerPrefs.PATH = KPlayerPrefs.GetPath();
					KPlayerPrefs._instance = YamlIO.LoadFile<KPlayerPrefs>(KPlayerPrefs.PATH, null, null);
				}
				catch
				{
				}
			}
			if (KPlayerPrefs._instance == null)
			{
				Debug.LogWarning("Failed to load KPlayerPrefs, Creating new instance..");
				KPlayerPrefs._corruptedFlag = true;
				KPlayerPrefs._instance = new KPlayerPrefs();
			}
			return KPlayerPrefs._instance;
		}
	}

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001E968 File Offset: 0x0001CB68
	// (set) Token: 0x06000727 RID: 1831 RVA: 0x0001E970 File Offset: 0x0001CB70
	public Dictionary<string, string> strings { get; private set; }

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001E979 File Offset: 0x0001CB79
	// (set) Token: 0x06000729 RID: 1833 RVA: 0x0001E981 File Offset: 0x0001CB81
	public Dictionary<string, int> ints { get; private set; }

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001E98A File Offset: 0x0001CB8A
	// (set) Token: 0x0600072B RID: 1835 RVA: 0x0001E992 File Offset: 0x0001CB92
	public Dictionary<string, float> floats { get; private set; }

	// Token: 0x0600072C RID: 1836 RVA: 0x0001E99B File Offset: 0x0001CB9B
	public KPlayerPrefs()
	{
		this.strings = new Dictionary<string, string>();
		this.ints = new Dictionary<string, int>();
		this.floats = new Dictionary<string, float>();
		KPlayerPrefs._instance = this;
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x0001E9CA File Offset: 0x0001CBCA
	public static bool HasCorruptedFlag()
	{
		return KPlayerPrefs._corruptedFlag;
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x0001E9D1 File Offset: 0x0001CBD1
	public static void ResetCorruptedFlag()
	{
		KPlayerPrefs._corruptedFlag = false;
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x0001E9D9 File Offset: 0x0001CBD9
	public static void DeleteAll()
	{
		KPlayerPrefs.instance.strings.Clear();
		KPlayerPrefs.instance.ints.Clear();
		KPlayerPrefs.instance.floats.Clear();
		KPlayerPrefs.Save();
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x0001EA0D File Offset: 0x0001CC0D
	private static string GetPath()
	{
		if (!Directory.Exists(Util.RootFolder()))
		{
			Directory.CreateDirectory(Util.RootFolder());
		}
		return Path.Combine(Util.RootFolder(), KPlayerPrefs.FILENAME);
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x0001EA38 File Offset: 0x0001CC38
	public static void Save()
	{
		try
		{
			YamlIO.SaveOrWarnUser<KPlayerPrefs>(KPlayerPrefs.instance, KPlayerPrefs.PATH, null);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Failed to save kplayerprefs: " + ex.ToString());
		}
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0001EA80 File Offset: 0x0001CC80
	public void Load()
	{
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x0001EA82 File Offset: 0x0001CC82
	public static void DeleteKey(string key)
	{
		KPlayerPrefs.instance.strings.Remove(key);
		KPlayerPrefs.instance.ints.Remove(key);
		KPlayerPrefs.instance.floats.Remove(key);
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x0001EAB8 File Offset: 0x0001CCB8
	public static float GetFloat(string key)
	{
		float num = 0f;
		KPlayerPrefs.instance.floats.TryGetValue(key, out num);
		return num;
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x0001EAE0 File Offset: 0x0001CCE0
	public static float GetFloat(string key, float defaultValue)
	{
		float num = 0f;
		if (!KPlayerPrefs.instance.floats.TryGetValue(key, out num))
		{
			num = defaultValue;
		}
		return num;
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0001EB0C File Offset: 0x0001CD0C
	public static int GetInt(string key)
	{
		int num = 0;
		KPlayerPrefs.instance.ints.TryGetValue(key, out num);
		return num;
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x0001EB30 File Offset: 0x0001CD30
	public static int GetInt(string key, int defaultValue)
	{
		int num = 0;
		if (!KPlayerPrefs.instance.ints.TryGetValue(key, out num))
		{
			num = defaultValue;
		}
		return num;
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0001EB58 File Offset: 0x0001CD58
	public static string GetString(string key)
	{
		string text = null;
		KPlayerPrefs.instance.strings.TryGetValue(key, out text);
		return text;
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x0001EB7C File Offset: 0x0001CD7C
	public static string GetString(string key, string defaultValue)
	{
		string text = null;
		if (!KPlayerPrefs.instance.strings.TryGetValue(key, out text))
		{
			text = defaultValue;
		}
		return text;
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x0001EBA2 File Offset: 0x0001CDA2
	public static bool HasKey(string key)
	{
		return KPlayerPrefs.instance.strings.ContainsKey(key) || KPlayerPrefs.instance.ints.ContainsKey(key) || KPlayerPrefs.instance.floats.ContainsKey(key);
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x0001EBE1 File Offset: 0x0001CDE1
	public static void SetFloat(string key, float value)
	{
		if (KPlayerPrefs.instance.floats.ContainsKey(key))
		{
			KPlayerPrefs.instance.floats[key] = value;
		}
		else
		{
			KPlayerPrefs.instance.floats.Add(key, value);
		}
		KPlayerPrefs.Save();
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x0001EC1E File Offset: 0x0001CE1E
	public static void SetInt(string key, int value)
	{
		if (KPlayerPrefs.instance.ints.ContainsKey(key))
		{
			KPlayerPrefs.instance.ints[key] = value;
		}
		else
		{
			KPlayerPrefs.instance.ints.Add(key, value);
		}
		KPlayerPrefs.Save();
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0001EC5B File Offset: 0x0001CE5B
	public static void SetString(string key, string value)
	{
		if (KPlayerPrefs.instance.strings.ContainsKey(key))
		{
			KPlayerPrefs.instance.strings[key] = value;
		}
		else
		{
			KPlayerPrefs.instance.strings.Add(key, value);
		}
		KPlayerPrefs.Save();
	}

	// Token: 0x040005DE RID: 1502
	private static KPlayerPrefs _instance = null;

	// Token: 0x040005DF RID: 1503
	private static bool _corruptedFlag = false;

	// Token: 0x040005E0 RID: 1504
	public const string KPLAYER_PREFS_DATA_COLLECTION_KEY = "DisableDataCollection";

	// Token: 0x040005E1 RID: 1505
	public static readonly string FILENAME = "kplayerprefs.yaml";

	// Token: 0x040005E2 RID: 1506
	private static string PATH = null;
}
