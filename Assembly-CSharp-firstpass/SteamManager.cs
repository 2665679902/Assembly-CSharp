using System;
using System.Text;
using Steamworks;
using UnityEngine;

// Token: 0x02000041 RID: 65
[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
	// Token: 0x1700007C RID: 124
	// (get) Token: 0x060002EE RID: 750 RVA: 0x00010318 File Offset: 0x0000E518
	private static SteamManager Instance
	{
		get
		{
			if (SteamManager.s_instance == null)
			{
				global::Debug.LogFormat("Creating SteamManager.", Array.Empty<object>());
				return new GameObject("SteamManager").AddComponent<SteamManager>();
			}
			return SteamManager.s_instance;
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x060002EF RID: 751 RVA: 0x0001034B File Offset: 0x0000E54B
	public static bool Initialized
	{
		get
		{
			return SteamManager.Instance.m_bInitialized;
		}
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x00010357 File Offset: 0x0000E557
	private static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
		global::Debug.LogWarning(pchDebugText);
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x00010360 File Offset: 0x0000E560
	private void Awake()
	{
		if (SteamManager.s_instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		SteamManager.s_instance = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		if (!Packsize.Test())
		{
			global::Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", this);
		}
		if (!DllCheck.Test())
		{
			global::Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", this);
		}
		try
		{
			if (SteamAPI.RestartAppIfNecessary(new AppId_t(457140U)))
			{
				App.Quit();
				return;
			}
		}
		catch (DllNotFoundException ex)
		{
			string text = "[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n";
			DllNotFoundException ex2 = ex;
			global::Debug.LogError(text + ((ex2 != null) ? ex2.ToString() : null), this);
			App.Quit();
			return;
		}
		this.m_bInitialized = SteamAPI.Init();
		if (!this.m_bInitialized)
		{
			global::Debug.LogWarning("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);
			App.Quit();
			return;
		}
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x00010430 File Offset: 0x0000E630
	private void OnEnable()
	{
		if (SteamManager.s_instance == null)
		{
			SteamManager.s_instance = this;
		}
		if (!this.m_bInitialized)
		{
			return;
		}
		if (this.m_SteamAPIWarningMessageHook == null)
		{
			this.m_SteamAPIWarningMessageHook = new SteamAPIWarningMessageHook_t(SteamManager.SteamAPIDebugTextHook);
			SteamClient.SetWarningMessageHook(this.m_SteamAPIWarningMessageHook);
		}
		if (SteamUtils.IsSteamChinaLauncher())
		{
			SteamUtils.InitFilterText(0U);
		}
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x0001048C File Offset: 0x0000E68C
	private void OnDestroy()
	{
		if (SteamManager.s_instance != this)
		{
			return;
		}
		SteamManager.s_instance = null;
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.Shutdown();
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x000104B0 File Offset: 0x0000E6B0
	private void Update()
	{
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.RunCallbacks();
	}

	// Token: 0x040003A9 RID: 937
	public const uint STEAM_APPLICATION_ID = 457140U;

	// Token: 0x040003AA RID: 938
	public const uint STEAM_EXPANSION1_APPLICATION_ID = 1452490U;

	// Token: 0x040003AB RID: 939
	private static SteamManager s_instance;

	// Token: 0x040003AC RID: 940
	private bool m_bInitialized;

	// Token: 0x040003AD RID: 941
	private SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;
}
