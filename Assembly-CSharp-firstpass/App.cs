using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using Klei;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000079 RID: 121
public class App : MonoBehaviour
{
	// Token: 0x060004D9 RID: 1241 RVA: 0x00018221 File Offset: 0x00016421
	public static string GetCurrentSceneName()
	{
		return App.currentSceneName;
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00018228 File Offset: 0x00016428
	private void OnApplicationQuit()
	{
		App.IsExiting = true;
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x00018230 File Offset: 0x00016430
	public void Restart()
	{
		string fileName = Process.GetCurrentProcess().MainModule.FileName;
		string fullPath = Path.GetFullPath(fileName);
		string directoryName = Path.GetDirectoryName(fullPath);
		global::Debug.LogFormat("Restarting\n\texe ({0})\n\tfull ({1})\n\tdir ({2})", new object[] { fileName, fullPath, directoryName });
		Process.Start(new ProcessStartInfo(Path.Combine(directoryName, "Restarter.exe"))
		{
			UseShellExecute = true,
			CreateNoWindow = true,
			Arguments = string.Format("\"{0}\"", fullPath)
		});
		App.Quit();
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x000182B4 File Offset: 0x000164B4
	static App()
	{
		foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
		{
			try
			{
				foreach (Type type in assembly.GetTypes())
				{
					App.types.Add(type);
				}
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0001835C File Offset: 0x0001655C
	public static void Quit()
	{
		Application.Quit();
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x00018363 File Offset: 0x00016563
	private void Awake()
	{
		App.instance = this;
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x0001836B File Offset: 0x0001656B
	public static void LoadScene(string scene_name)
	{
		global::Debug.Assert(!App.isLoading, "Scene [" + App.loadingSceneName + "] is already being loaded!");
		KMonoBehaviour.isLoadingScene = true;
		App.isLoading = true;
		App.loadingSceneName = scene_name;
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x000183A0 File Offset: 0x000165A0
	private void OnApplicationFocus(bool focus)
	{
		App.hasFocus = focus;
		this.lastSuspendTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x000183B4 File Offset: 0x000165B4
	public void LateUpdate()
	{
		if (App.isLoading)
		{
			KObjectManager.Instance.Cleanup();
			KMonoBehaviour.lastGameObject = null;
			KMonoBehaviour.lastObj = null;
			if (SimAndRenderScheduler.instance != null)
			{
				SimAndRenderScheduler.instance.Reset();
			}
			Resources.UnloadUnusedAssets();
			GC.Collect();
			if (App.OnPreLoadScene != null)
			{
				App.OnPreLoadScene();
			}
			SceneManager.LoadScene(App.loadingSceneName);
			if (App.OnPostLoadScene != null)
			{
				App.OnPostLoadScene();
			}
			App.isLoading = false;
			App.currentSceneName = App.loadingSceneName;
			App.loadingSceneName = null;
		}
		if (!App.hasFocus && GenericGameSettings.instance.sleepWhenOutOfFocus)
		{
			float num = (Time.realtimeSinceStartup - this.lastSuspendTime) * 1000f;
			float num2 = 0f;
			for (int i = 0; i < App.sleepIntervals.Length; i++)
			{
				num2 = App.sleepIntervals[i];
				if (num2 > num)
				{
					break;
				}
			}
			float num3 = num2 - num;
			num3 = Mathf.Max(0f, num3);
			Thread.Sleep((int)num3);
			this.lastSuspendTime = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x000184A8 File Offset: 0x000166A8
	private void OnDestroy()
	{
		GlobalJobManager.Cleanup();
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x000184AF File Offset: 0x000166AF
	public static List<Type> GetCurrentDomainTypes()
	{
		return App.types;
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x000184B6 File Offset: 0x000166B6
	public static void OpenWebURL(string url)
	{
		if (DistributionPlatform.Initialized && SteamUtils.IsSteamRunningOnSteamDeck() && SteamUtils.IsOverlayEnabled())
		{
			SteamFriends.ActivateGameOverlayToWebPage(url, EActivateGameOverlayToWebPageMode.k_EActivateGameOverlayToWebPageMode_Default);
			return;
		}
		Application.OpenURL(url);
	}

	// Token: 0x04000507 RID: 1287
	public static App instance;

	// Token: 0x04000508 RID: 1288
	public static bool IsExiting = false;

	// Token: 0x04000509 RID: 1289
	public static System.Action OnPreLoadScene;

	// Token: 0x0400050A RID: 1290
	public static System.Action OnPostLoadScene;

	// Token: 0x0400050B RID: 1291
	public static bool isLoading = false;

	// Token: 0x0400050C RID: 1292
	public static bool hasFocus = true;

	// Token: 0x0400050D RID: 1293
	public static string loadingSceneName = null;

	// Token: 0x0400050E RID: 1294
	private static string currentSceneName = null;

	// Token: 0x0400050F RID: 1295
	private float lastSuspendTime;

	// Token: 0x04000510 RID: 1296
	private const string PIPE_NAME = "KLEI_ONI_EXIT_CODE_PIPE";

	// Token: 0x04000511 RID: 1297
	private const string RESTART_FILENAME = "Restarter.exe";

	// Token: 0x04000512 RID: 1298
	private static List<Type> types = new List<Type>();

	// Token: 0x04000513 RID: 1299
	private static float[] sleepIntervals = new float[] { 8.333333f, 16.666666f, 33.333332f };
}
