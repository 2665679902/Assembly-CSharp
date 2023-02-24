using System;
using System.IO;
using System.Threading;
using UnityEngine;

// Token: 0x020007EF RID: 2031
public class LaunchInitializer : MonoBehaviour
{
	// Token: 0x06003A93 RID: 14995 RVA: 0x0014457A File Offset: 0x0014277A
	public static string BuildPrefix()
	{
		return LaunchInitializer.BUILD_PREFIX;
	}

	// Token: 0x06003A94 RID: 14996 RVA: 0x00144581 File Offset: 0x00142781
	public static int UpdateNumber()
	{
		return 45;
	}

	// Token: 0x06003A95 RID: 14997 RVA: 0x00144588 File Offset: 0x00142788
	private void Update()
	{
		if (this.numWaitFrames > Time.renderedFrameCount)
		{
			return;
		}
		if (!DistributionPlatform.Initialized)
		{
			if (!SystemInfo.SupportsTextureFormat(TextureFormat.RGBAFloat))
			{
				global::Debug.LogError("Machine does not support RGBAFloat32");
			}
			GraphicsOptionsScreen.SetSettingsFromPrefs();
			Util.ApplyInvariantCultureToThread(Thread.CurrentThread);
			global::Debug.Log("Current date: " + System.DateTime.Now.ToString());
			global::Debug.Log("release Build: " + BuildWatermark.GetBuildText());
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			KPlayerPrefs.instance.Load();
			DistributionPlatform.Initialize();
		}
		if (!DistributionPlatform.Inst.IsDLCStatusReady())
		{
			return;
		}
		global::Debug.Log("DistributionPlatform initialized.");
		global::Debug.Log("release Build: " + BuildWatermark.GetBuildText());
		global::Debug.Log(string.Format("EXPANSION1 installed: {0}  active: {1}", DlcManager.IsExpansion1Installed(), DlcManager.IsExpansion1Active()));
		KFMOD.Initialize();
		for (int i = 0; i < this.SpawnPrefabs.Length; i++)
		{
			GameObject gameObject = this.SpawnPrefabs[i];
			if (gameObject != null)
			{
				Util.KInstantiate(gameObject, base.gameObject, null);
			}
		}
		LaunchInitializer.DeleteLingeringFiles();
		base.enabled = false;
	}

	// Token: 0x06003A96 RID: 14998 RVA: 0x001446A8 File Offset: 0x001428A8
	private static void DeleteLingeringFiles()
	{
		string[] array = new string[] { "fmod.log", "load_stats_0.json", "OxygenNotIncluded_Data/output_log.txt" };
		string directoryName = Path.GetDirectoryName(Application.dataPath);
		foreach (string text in array)
		{
			string text2 = Path.Combine(directoryName, text);
			try
			{
				if (File.Exists(text2))
				{
					File.Delete(text2);
				}
			}
			catch (Exception ex)
			{
				global::Debug.LogWarning(ex);
			}
		}
	}

	// Token: 0x0400266B RID: 9835
	private const string PREFIX = "U";

	// Token: 0x0400266C RID: 9836
	private const int UPDATE_NUMBER = 45;

	// Token: 0x0400266D RID: 9837
	private static readonly string BUILD_PREFIX = "U" + 45.ToString();

	// Token: 0x0400266E RID: 9838
	public GameObject[] SpawnPrefabs;

	// Token: 0x0400266F RID: 9839
	[SerializeField]
	private int numWaitFrames = 1;
}
