using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class KleiMetrics : ThreadedHttps<KleiMetrics>
{
	// Token: 0x0600029E RID: 670 RVA: 0x0000E8E4 File Offset: 0x0000CAE4
	public KleiMetrics()
	{
		this.LIVE_ENDPOINT = "oni.metrics.klei.com/write";
		this.serviceName = "KleiMetrics";
		this.CLIENT_KEY = DistributionPlatform.Inst.MetricsClientKey;
		this.PlatformUserIDFieldName = DistributionPlatform.Inst.MetricsUserIDField;
		KleiMetrics.sessionID = -1;
		this.enabled = !KPrivacyPrefs.instance.disableDataCollection;
		KleiMetrics.GameID();
		this.isMultiThreaded = true;
	}

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x0600029F RID: 671 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
	// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000E9C8 File Offset: 0x0000CBC8
	public bool isMultiThreaded { get; protected set; }

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000E9D1 File Offset: 0x0000CBD1
	// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000E9D9 File Offset: 0x0000CBD9
	public bool enabled { get; private set; }

	// Token: 0x060002A3 RID: 675 RVA: 0x0000E9E2 File Offset: 0x0000CBE2
	public void SetEnabled(bool enabled)
	{
		this.enabled = enabled;
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x0000E9EC File Offset: 0x0000CBEC
	protected string PostMetricData(Dictionary<string, object> data, string debug_source)
	{
		string text = JsonConvert.SerializeObject(new KleiMetrics.PostData(this.CLIENT_KEY, data));
		byte[] bytes = Encoding.UTF8.GetBytes(text);
		if (this.isMultiThreaded)
		{
			base.PutPacket(bytes, false);
			return "OK";
		}
		return base.Send(bytes, false);
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x0000EA3C File Offset: 0x0000CC3C
	public static string PlatformUserID()
	{
		DistributionPlatform.User localUser = DistributionPlatform.Inst.LocalUser;
		if (localUser == null)
		{
			return "";
		}
		return localUser.Id.ToString();
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0000EA68 File Offset: 0x0000CC68
	public static string UserID()
	{
		DistributionPlatform.User localUser = DistributionPlatform.Inst.LocalUser;
		if (localUser == null)
		{
			return "";
		}
		return localUser.Id.ToString();
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0000EA94 File Offset: 0x0000CC94
	private void IncrementSessionCount()
	{
		KleiMetrics.sessionID = KleiMetrics.SessionID() + 1;
		KPlayerPrefs.SetInt("SESSION_ID", KleiMetrics.sessionID);
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0000EAB1 File Offset: 0x0000CCB1
	public static int SessionID()
	{
		if (KleiMetrics.sessionID == -1)
		{
			KleiMetrics.sessionID = KPlayerPrefs.GetInt("SESSION_ID", -1);
		}
		return KleiMetrics.sessionID;
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0000EAD0 File Offset: 0x0000CCD0
	public void IncrementGameCount()
	{
		KleiMetrics.gameID = KleiMetrics.GameID() + 1;
		KleiMetrics.SetGameID(KleiMetrics.gameID);
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
	public static int GameID()
	{
		if (KleiMetrics.gameID == -1)
		{
			KleiMetrics.gameID = KPlayerPrefs.GetInt("GAME_ID", -1);
		}
		return KleiMetrics.gameID;
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0000EB07 File Offset: 0x0000CD07
	public static void SetGameID(int id)
	{
		KPlayerPrefs.SetInt("GAME_ID", id);
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0000EB14 File Offset: 0x0000CD14
	public static string GetInstallTimeStamp()
	{
		if (KleiMetrics.installTimeStamp == null)
		{
			KleiMetrics.installTimeStamp = KPlayerPrefs.GetString("INSTALL_TIMESTAMP", null);
			if (KleiMetrics.installTimeStamp == null || KleiMetrics.installTimeStamp == "")
			{
				KleiMetrics.installTimeStamp = DateTime.UtcNow.Ticks.ToString();
				KPlayerPrefs.SetString("INSTALL_TIMESTAMP", KleiMetrics.installTimeStamp);
			}
		}
		return KleiMetrics.installTimeStamp;
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0000EB7E File Offset: 0x0000CD7E
	public static string CurrentLevel()
	{
		return null;
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0000EB84 File Offset: 0x0000CD84
	protected static KleiMetrics.ExpansionsMetricsData[] Expansions()
	{
		List<string> ownedDLCIds = DlcManager.GetOwnedDLCIds();
		KleiMetrics.ExpansionsMetricsData[] array = new KleiMetrics.ExpansionsMetricsData[ownedDLCIds.Count];
		for (int i = 0; i < ownedDLCIds.Count; i++)
		{
			array[i] = new KleiMetrics.ExpansionsMetricsData
			{
				Name = ownedDLCIds[i],
				Activated = DlcManager.IsContentActive(ownedDLCIds[i])
			};
		}
		return array;
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
	public void SetLastUserAction(long lastUserActionTicks)
	{
		if (!this.enabled)
		{
			return;
		}
		if (!this.sessionStarted)
		{
			return;
		}
		this.currentSessionTicks = DateTime.Now.Ticks;
		if (this.shouldEndSession)
		{
			this.EndSession(false);
			this.shouldEndSession = false;
			this.shouldStartSession = true;
		}
		else if (this.shouldStartSession && lastUserActionTicks > this.lastHeartBeatTicks)
		{
			this.StartSession();
			this.shouldStartSession = false;
		}
		this.timeSinceLastUserAction = (float)TimeSpan.FromTicks(this.currentSessionTicks - lastUserActionTicks).TotalSeconds;
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x0000EC73 File Offset: 0x0000CE73
	private void StopHeartBeat()
	{
		if (KleiMetrics.heartbeatTimer != null)
		{
			KleiMetrics.heartbeatTimer.Stop();
			KleiMetrics.heartbeatTimer.Dispose();
			KleiMetrics.heartbeatTimer = null;
		}
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x0000EC98 File Offset: 0x0000CE98
	private void StartHeartBeat()
	{
		if (!this.enabled)
		{
			return;
		}
		if (!this.sessionStarted)
		{
			return;
		}
		this.StopHeartBeat();
		KleiMetrics.heartbeatTimer = new System.Timers.Timer((double)(this.HeartBeatInSeconds * 1000));
		KleiMetrics.heartbeatTimer.Elapsed += this.SendHeartBeat;
		KleiMetrics.heartbeatTimer.AutoReset = true;
		KleiMetrics.heartbeatTimer.Enabled = true;
		this.lastHeartBeatTicks = DateTime.Now.Ticks;
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x0000ED14 File Offset: 0x0000CF14
	private uint GetSessionTime()
	{
		int num = (int)TimeSpan.FromTicks(this.currentSessionTicks - this.startTimeTicks).TotalSeconds;
		if (num < 0)
		{
			global::Debug.LogWarning("Session time is < 0");
		}
		return (uint)num;
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0000ED4C File Offset: 0x0000CF4C
	private void SendHeartBeat(object source, ElapsedEventArgs e)
	{
		if (!this.enabled)
		{
			return;
		}
		if (!this.sessionStarted)
		{
			return;
		}
		Dictionary<string, object> dictionary = this.GetUserSession();
		dictionary.Add("LastUA", (int)this.timeSinceLastUserAction);
		if (this.timeSinceLastUserAction > (float)this.HeartBeatTimeOutInSeconds)
		{
			dictionary.Add("HeartBeatTimeOut", true);
			KleiMetrics.heartbeatTimer.Stop();
			this.shouldEndSession = true;
		}
		long num = DateTime.Now.Ticks - this.lastHeartBeatTicks;
		dictionary.Add("HeartBeat", (int)TimeSpan.FromTicks(num).TotalSeconds);
		this.PostMetricData(dictionary, "SendHeartBeat");
		this.lastHeartBeatTicks = DateTime.Now.Ticks;
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x0000EE0E File Offset: 0x0000D00E
	private void StartThread()
	{
		if (!this.hasStarted)
		{
			if (this.isMultiThreaded)
			{
				base.Start();
			}
			this.hasStarted = true;
		}
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x0000EE2D File Offset: 0x0000D02D
	private void EndThread()
	{
		if (this.hasStarted)
		{
			if (this.isMultiThreaded)
			{
				base.End();
			}
			this.hasStarted = false;
		}
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x0000EE4C File Offset: 0x0000D04C
	public void SetStaticSessionVariable(string name, object var)
	{
		if (this.userSession.ContainsKey(name))
		{
			this.userSession[name] = var;
			return;
		}
		this.userSession.Add(name, var);
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0000EE77 File Offset: 0x0000D077
	public void RemoveStaticSessionVariable(string name)
	{
		if (this.userSession.ContainsKey(name))
		{
			this.userSession.Remove(name);
		}
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x0000EE94 File Offset: 0x0000D094
	public void AddDefaultSessionVariables()
	{
		this.userSession.Clear();
		this.SetStaticSessionVariable("InstallTimeStamp", KleiMetrics.GetInstallTimeStamp());
		this.SetStaticSessionVariable("user", KleiMetrics.UserID());
		this.SetStaticSessionVariable("SessionID", KleiMetrics.SessionID());
		this.SetStaticSessionVariable("SessionStartTimeStamp", this.sessionStartUtcTicks.ToString());
		if (KleiAccount.KleiUserID != null)
		{
			this.SetStaticSessionVariable("KU", KleiAccount.KleiUserID);
		}
		this.SetStaticSessionVariable("Expansions", KleiMetrics.Expansions());
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x0000EF20 File Offset: 0x0000D120
	private Dictionary<string, object> GetUserSession()
	{
		global::Debug.Assert(this.enabled);
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		if (!this.sessionStarted)
		{
			return dictionary;
		}
		foreach (KeyValuePair<string, object> keyValuePair in this.userSession)
		{
			dictionary.Add(keyValuePair.Key, keyValuePair.Value);
		}
		dictionary.Add("SessionTimeSeconds", this.GetSessionTime());
		if (KleiMetrics.GameID() != -1)
		{
			dictionary.Add("GameID", KleiMetrics.GameID());
		}
		string text = KleiMetrics.CurrentLevel();
		if (text != null)
		{
			dictionary.Add("Level", text);
		}
		if (this.SetDynamicSessionVariables != null)
		{
			try
			{
				this.SetDynamicSessionVariables(dictionary);
			}
			catch (Exception ex)
			{
				global::Debug.LogError("Dynamic session variables may be set from a thread. " + ex.Message + "\n" + ex.StackTrace);
			}
		}
		return dictionary;
	}

	// Token: 0x060002BA RID: 698 RVA: 0x0000F02C File Offset: 0x0000D22C
	public void SetCallBacks(System.Action setStaticSessionVariables, Action<Dictionary<string, object>> setDynamicSessionVariables)
	{
		this.SetDynamicSessionVariables = setDynamicSessionVariables;
		this.SetStaticSessionVariables = setStaticSessionVariables;
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0000F03C File Offset: 0x0000D23C
	private void SetStartTime()
	{
		this.sessionStartUtcTicks = DateTime.UtcNow.Ticks;
		this.startTimeTicks = DateTime.Now.Ticks;
		this.currentSessionTicks = DateTime.Now.Ticks;
		this.sessionStarted = true;
	}

	// Token: 0x060002BC RID: 700 RVA: 0x0000F08C File Offset: 0x0000D28C
	public void StartSession()
	{
		if (!this.enabled)
		{
			return;
		}
		if (this.sessionStarted)
		{
			this.EndSession(false);
		}
		this.StartThread();
		this.SetStartTime();
		this.IncrementSessionCount();
		this.AddDefaultSessionVariables();
		if (this.SetStaticSessionVariables != null)
		{
			this.SetStaticSessionVariables();
		}
		Dictionary<string, object> dictionary = this.GetUserSession();
		dictionary.Add("StartSession", true);
		string text = KleiMetrics.PlatformUserID();
		if (text != null)
		{
			dictionary.Add(this.PlatformUserIDFieldName, text);
		}
		if (this.shouldStartSession)
		{
			dictionary.Add("HeartBeatTimeOut", false);
		}
		foreach (KeyValuePair<string, object> keyValuePair in KleiMetrics.GetHardwareStats())
		{
			dictionary.Add(keyValuePair.Key, keyValuePair.Value);
		}
		this.PostMetricData(dictionary, "StartSession");
		this.StartHeartBeat();
	}

	// Token: 0x060002BD RID: 701 RVA: 0x0000F188 File Offset: 0x0000D388
	public void EndSession(bool crashed = false)
	{
		if (!this.enabled)
		{
			return;
		}
		if (!this.sessionStarted)
		{
			return;
		}
		Dictionary<string, object> dictionary = this.GetUserSession();
		dictionary.Add("EndSession", true);
		if (crashed)
		{
			dictionary.Add("EndSessionCrashed", true);
		}
		if (this.shouldEndSession)
		{
			dictionary.Add("HeartBeatTimeOut", true);
		}
		this.PostMetricData(dictionary, "EndSession");
		this.sessionStarted = false;
		this.StopHeartBeat();
		this.EndThread();
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0000F20C File Offset: 0x0000D40C
	public void StartNewGame()
	{
		if (!this.enabled)
		{
			return;
		}
		if (!this.sessionStarted)
		{
			this.StartSession();
		}
		this.IncrementGameCount();
		Dictionary<string, object> dictionary = this.GetUserSession();
		dictionary.Add("NewGame", true);
		this.PostMetricData(dictionary, "StartNewGame");
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0000F25C File Offset: 0x0000D45C
	public void EndGame()
	{
		if (!this.enabled)
		{
			return;
		}
		if (!this.sessionStarted)
		{
			return;
		}
		Dictionary<string, object> dictionary = this.GetUserSession();
		dictionary.Add("EndGame", true);
		this.PostMetricData(dictionary, "EndGame");
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
	public void SendEvent(Dictionary<string, object> eventData, string debug_event_name)
	{
		if (!this.enabled)
		{
			return;
		}
		if (!this.sessionStarted)
		{
			this.StartSession();
		}
		Dictionary<string, object> dictionary = this.GetUserSession();
		foreach (KeyValuePair<string, object> keyValuePair in eventData)
		{
			dictionary.Add(keyValuePair.Key, keyValuePair.Value);
		}
		this.PostMetricData(dictionary, "SendEvent:" + debug_event_name);
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x0000F32C File Offset: 0x0000D52C
	public bool SendProfileStats()
	{
		if (!this.enabled)
		{
			return false;
		}
		Dictionary<string, object> dictionary = this.GetUserSession();
		return ThreadedHttps<KleiMetrics>.Instance.PostMetricData(dictionary, "SendProfileStats") == "OK";
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x0000F364 File Offset: 0x0000D564
	public static Dictionary<string, object> GetHardwareStats()
	{
		return new Dictionary<string, object>
		{
			{
				"Platform",
				Application.platform.ToString()
			},
			{
				"OSname",
				SystemInfo.operatingSystem
			},
			{
				"OSversion",
				Environment.OSVersion.Version.ToString()
			},
			{
				"CPUmodel",
				SystemInfo.deviceModel
			},
			{
				"CPUdeviceType",
				SystemInfo.deviceType.ToString()
			},
			{
				"CPUarch",
				Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE")
			},
			{
				"ProcBits",
				(IntPtr.Size == 4) ? 32 : 64
			},
			{
				"CPUcount",
				SystemInfo.processorCount
			},
			{
				"CPUtype",
				SystemInfo.processorType
			},
			{
				"SystemMemoryMegs",
				SystemInfo.systemMemorySize
			},
			{
				"GPUgraphicsDeviceID",
				SystemInfo.graphicsDeviceID
			},
			{
				"GPUname",
				SystemInfo.graphicsDeviceName
			},
			{
				"GPUgraphicsDeviceType",
				SystemInfo.graphicsDeviceType.ToString()
			},
			{
				"GPUgraphicsDeviceVendor",
				SystemInfo.graphicsDeviceVendor
			},
			{
				"GPUgraphicsDeviceVendorID",
				SystemInfo.graphicsDeviceVendorID
			},
			{
				"GPUgraphicsDeviceVersion",
				SystemInfo.graphicsDeviceVersion
			},
			{
				"GPUmemoryMegs",
				SystemInfo.graphicsMemorySize
			},
			{
				"GPUgraphicsMultiThreaded",
				SystemInfo.graphicsMultiThreaded
			},
			{
				"GPUgraphicsShaderLevel",
				SystemInfo.graphicsShaderLevel
			},
			{
				"GPUmaxTextureSize",
				SystemInfo.maxTextureSize
			},
			{
				"GPUnpotSupport",
				SystemInfo.npotSupport.ToString()
			},
			{
				"GPUsupportedRenderTargetCount",
				SystemInfo.supportedRenderTargetCount
			},
			{
				"GPUsupports2DArrayTextures",
				SystemInfo.supports2DArrayTextures
			},
			{
				"GPUsupports3DTextures",
				SystemInfo.supports3DTextures
			},
			{
				"GPUsupportsComputeShaders",
				SystemInfo.supportsComputeShaders
			},
			{ "GPUsupportsImageEffects", true },
			{
				"GPUsupportsInstancing",
				SystemInfo.supportsInstancing
			},
			{ "GPUsupportsRenderToCubemap", true },
			{
				"GPUsupportsShadows",
				SystemInfo.supportsShadows
			},
			{
				"GPUsupportsSparseTextures",
				SystemInfo.supportsSparseTextures
			},
			{
				"GPUcopyTextureSupport",
				SystemInfo.copyTextureSupport
			}
		};
	}

	// Token: 0x0400036C RID: 876
	private const string SessionIDKey = "SESSION_ID";

	// Token: 0x0400036D RID: 877
	private const string GameIDKey = "GAME_ID";

	// Token: 0x0400036E RID: 878
	private const string InstallTimeStampKey = "INSTALL_TIMESTAMP";

	// Token: 0x0400036F RID: 879
	private const string UserIDFieldName = "user";

	// Token: 0x04000370 RID: 880
	private const string SessionIDFieldName = "SessionID";

	// Token: 0x04000371 RID: 881
	private const string GameIDFieldName = "GameID";

	// Token: 0x04000372 RID: 882
	private const string InstallTimeStampFieldName = "InstallTimeStamp";

	// Token: 0x04000373 RID: 883
	private const string KleiUserFieldName = "KU";

	// Token: 0x04000374 RID: 884
	private const string StartSessionFieldName = "StartSession";

	// Token: 0x04000375 RID: 885
	private const string EndSessionFieldName = "EndSession";

	// Token: 0x04000376 RID: 886
	private const string EndSessionCrashedFieldName = "EndSessionCrashed";

	// Token: 0x04000377 RID: 887
	private const string SessionStartTimeStampFieldName = "SessionStartTimeStamp";

	// Token: 0x04000378 RID: 888
	private const string SessionTimeFieldName = "SessionTimeSeconds";

	// Token: 0x04000379 RID: 889
	public const string NewGameFieldName = "NewGame";

	// Token: 0x0400037A RID: 890
	private const string EndGameFieldName = "EndGame";

	// Token: 0x0400037B RID: 891
	public const string GameTimeFieldName = "GameTimeSeconds";

	// Token: 0x0400037C RID: 892
	private const string LevelFieldName = "Level";

	// Token: 0x0400037D RID: 893
	public const string BuildBranchName = "Branch";

	// Token: 0x0400037E RID: 894
	public const string BuildFieldName = "Build";

	// Token: 0x0400037F RID: 895
	private const int EDITOR_BUILD_ID = -1;

	// Token: 0x04000380 RID: 896
	private const string HeartBeatFieldName = "HeartBeat";

	// Token: 0x04000381 RID: 897
	private const string HeartBeatTimeOutFieldName = "HeartBeatTimeOut";

	// Token: 0x04000382 RID: 898
	private const string LastUserActionFieldName = "LastUA";

	// Token: 0x04000383 RID: 899
	public const string SaveFolderWriteTest = "SaveFolderWriteTest";

	// Token: 0x04000384 RID: 900
	public const string ExpansionsFieldName = "Expansions";

	// Token: 0x04000385 RID: 901
	private string PlatformUserIDFieldName;

	// Token: 0x04000386 RID: 902
	private static int sessionID = -1;

	// Token: 0x04000387 RID: 903
	private static int gameID = -1;

	// Token: 0x04000388 RID: 904
	private static string installTimeStamp = null;

	// Token: 0x04000389 RID: 905
	private static System.Timers.Timer heartbeatTimer;

	// Token: 0x0400038A RID: 906
	private int HeartBeatInSeconds = 180;

	// Token: 0x0400038B RID: 907
	private int HeartBeatTimeOutInSeconds = 1200;

	// Token: 0x0400038C RID: 908
	private long currentSessionTicks = DateTime.Now.Ticks;

	// Token: 0x0400038D RID: 909
	private float timeSinceLastUserAction;

	// Token: 0x0400038E RID: 910
	private long lastHeartBeatTicks = DateTime.Now.Ticks;

	// Token: 0x0400038F RID: 911
	private long startTimeTicks = DateTime.Now.Ticks;

	// Token: 0x04000390 RID: 912
	private bool shouldEndSession;

	// Token: 0x04000391 RID: 913
	private bool shouldStartSession;

	// Token: 0x04000392 RID: 914
	private bool hasStarted;

	// Token: 0x04000393 RID: 915
	private Dictionary<string, object> userSession = new Dictionary<string, object>();

	// Token: 0x04000394 RID: 916
	private Action<Dictionary<string, object>> SetDynamicSessionVariables;

	// Token: 0x04000395 RID: 917
	private System.Action SetStaticSessionVariables;

	// Token: 0x04000396 RID: 918
	private bool sessionStarted;

	// Token: 0x04000397 RID: 919
	private long sessionStartUtcTicks = DateTime.UtcNow.Ticks;

	// Token: 0x02000997 RID: 2455
	protected struct PostData
	{
		// Token: 0x06005317 RID: 21271 RVA: 0x0009B7C4 File Offset: 0x000999C4
		public PostData(string key, Dictionary<string, object> data)
		{
			this.clientKey = key;
			this.metricData = data;
		}

		// Token: 0x04002157 RID: 8535
		public string clientKey;

		// Token: 0x04002158 RID: 8536
		public Dictionary<string, object> metricData;
	}

	// Token: 0x02000998 RID: 2456
	protected struct ExpansionsMetricsData
	{
		// Token: 0x04002159 RID: 8537
		public string Name;

		// Token: 0x0400215A RID: 8538
		public bool Activated;
	}
}
