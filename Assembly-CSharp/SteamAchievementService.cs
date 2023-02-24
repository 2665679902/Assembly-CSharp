using System;
using System.Diagnostics;
using Steamworks;
using UnityEngine;

// Token: 0x020009DD RID: 2525
public class SteamAchievementService : MonoBehaviour
{
	// Token: 0x170005A4 RID: 1444
	// (get) Token: 0x06004B6A RID: 19306 RVA: 0x001A7219 File Offset: 0x001A5419
	public static SteamAchievementService Instance
	{
		get
		{
			return SteamAchievementService.instance;
		}
	}

	// Token: 0x06004B6B RID: 19307 RVA: 0x001A7220 File Offset: 0x001A5420
	public static void Initialize()
	{
		if (SteamAchievementService.instance == null)
		{
			GameObject gameObject = GameObject.Find("/SteamManager");
			SteamAchievementService.instance = gameObject.GetComponent<SteamAchievementService>();
			if (SteamAchievementService.instance == null)
			{
				SteamAchievementService.instance = gameObject.AddComponent<SteamAchievementService>();
			}
		}
	}

	// Token: 0x06004B6C RID: 19308 RVA: 0x001A7268 File Offset: 0x001A5468
	public void Awake()
	{
		this.setupComplete = false;
		global::Debug.Assert(SteamAchievementService.instance == null);
		SteamAchievementService.instance = this;
	}

	// Token: 0x06004B6D RID: 19309 RVA: 0x001A7287 File Offset: 0x001A5487
	private void OnDestroy()
	{
		global::Debug.Assert(SteamAchievementService.instance == this);
		SteamAchievementService.instance = null;
	}

	// Token: 0x06004B6E RID: 19310 RVA: 0x001A729F File Offset: 0x001A549F
	private void Update()
	{
		if (!SteamManager.Initialized)
		{
			return;
		}
		if (Game.Instance != null)
		{
			return;
		}
		if (!this.setupComplete && DistributionPlatform.Initialized)
		{
			this.Setup();
		}
	}

	// Token: 0x06004B6F RID: 19311 RVA: 0x001A72CC File Offset: 0x001A54CC
	private void Setup()
	{
		this.cbUserStatsReceived = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate(this.OnUserStatsReceived));
		this.cbUserStatsStored = Callback<UserStatsStored_t>.Create(new Callback<UserStatsStored_t>.DispatchDelegate(this.OnUserStatsStored));
		this.cbUserAchievementStored = Callback<UserAchievementStored_t>.Create(new Callback<UserAchievementStored_t>.DispatchDelegate(this.OnUserAchievementStored));
		this.setupComplete = true;
		this.RefreshStats();
	}

	// Token: 0x06004B70 RID: 19312 RVA: 0x001A732B File Offset: 0x001A552B
	private void RefreshStats()
	{
		SteamUserStats.RequestCurrentStats();
	}

	// Token: 0x06004B71 RID: 19313 RVA: 0x001A7333 File Offset: 0x001A5533
	private void OnUserStatsReceived(UserStatsReceived_t data)
	{
		if (data.m_eResult != EResult.k_EResultOK)
		{
			DebugUtil.LogWarningArgs(new object[] { "OnUserStatsReceived", data.m_eResult, data.m_steamIDUser });
			return;
		}
	}

	// Token: 0x06004B72 RID: 19314 RVA: 0x001A736E File Offset: 0x001A556E
	private void OnUserStatsStored(UserStatsStored_t data)
	{
		if (data.m_eResult != EResult.k_EResultOK)
		{
			DebugUtil.LogWarningArgs(new object[] { "OnUserStatsStored", data.m_eResult });
			return;
		}
	}

	// Token: 0x06004B73 RID: 19315 RVA: 0x001A739B File Offset: 0x001A559B
	private void OnUserAchievementStored(UserAchievementStored_t data)
	{
	}

	// Token: 0x06004B74 RID: 19316 RVA: 0x001A73A0 File Offset: 0x001A55A0
	public void Unlock(string achievement_id)
	{
		bool flag = SteamUserStats.SetAchievement(achievement_id);
		global::Debug.LogFormat("SetAchievement {0} {1}", new object[] { achievement_id, flag });
		bool flag2 = SteamUserStats.StoreStats();
		global::Debug.LogFormat("StoreStats {0}", new object[] { flag2 });
	}

	// Token: 0x06004B75 RID: 19317 RVA: 0x001A73F0 File Offset: 0x001A55F0
	[Conditional("UNITY_EDITOR")]
	[ContextMenu("Reset All Achievements")]
	private void ResetAllAchievements()
	{
		bool flag = SteamUserStats.ResetAllStats(true);
		global::Debug.LogFormat("ResetAllStats {0}", new object[] { flag });
		if (flag)
		{
			this.RefreshStats();
		}
	}

	// Token: 0x04003165 RID: 12645
	private Callback<UserStatsReceived_t> cbUserStatsReceived;

	// Token: 0x04003166 RID: 12646
	private Callback<UserStatsStored_t> cbUserStatsStored;

	// Token: 0x04003167 RID: 12647
	private Callback<UserAchievementStored_t> cbUserAchievementStored;

	// Token: 0x04003168 RID: 12648
	private bool setupComplete;

	// Token: 0x04003169 RID: 12649
	private static SteamAchievementService instance;
}
