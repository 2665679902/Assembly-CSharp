using System;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class DistributionPlatform : MonoBehaviour
{
	// Token: 0x17000070 RID: 112
	// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000FF4A File Offset: 0x0000E14A
	public static bool Initialized
	{
		get
		{
			return DistributionPlatform.Impl != null && DistributionPlatform.Impl.Initialized;
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000FF5F File Offset: 0x0000E15F
	public static DistributionPlatform.Implementation Inst
	{
		get
		{
			return DistributionPlatform.Impl;
		}
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x0000FF66 File Offset: 0x0000E166
	public static void Initialize()
	{
		if (DistributionPlatform.sImpl == null)
		{
			DistributionPlatform.sImpl = new GameObject("DistributionPlatform").AddComponent<SteamDistributionPlatform>();
			if (!SteamManager.Initialized)
			{
				global::Debug.LogError("Steam not initialized in time.");
			}
		}
	}

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x060002D5 RID: 725 RVA: 0x0000FF94 File Offset: 0x0000E194
	// (remove) Token: 0x060002D6 RID: 726 RVA: 0x0000FFC8 File Offset: 0x0000E1C8
	public static event System.Action onExitRequest;

	// Token: 0x060002D7 RID: 727 RVA: 0x0000FFFB File Offset: 0x0000E1FB
	public static void RequestExit()
	{
		if (DistributionPlatform.onExitRequest != null)
		{
			DistributionPlatform.onExitRequest();
		}
	}

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x060002D8 RID: 728 RVA: 0x00010010 File Offset: 0x0000E210
	// (remove) Token: 0x060002D9 RID: 729 RVA: 0x00010044 File Offset: 0x0000E244
	public static event System.Action onDlcAuthenticationFailed;

	// Token: 0x060002DA RID: 730 RVA: 0x00010077 File Offset: 0x0000E277
	public static void TriggerDlcAuthenticationFailed()
	{
		if (DistributionPlatform.onDlcAuthenticationFailed != null)
		{
			DistributionPlatform.onDlcAuthenticationFailed();
		}
	}

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x060002DB RID: 731 RVA: 0x0001008A File Offset: 0x0000E28A
	private static DistributionPlatform.Implementation Impl
	{
		get
		{
			return DistributionPlatform.sImpl;
		}
	}

	// Token: 0x040003A6 RID: 934
	private static DistributionPlatform.Implementation sImpl;

	// Token: 0x0200099A RID: 2458
	public interface Implementation
	{
		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x0600531A RID: 21274
		bool Initialized { get; }

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x0600531B RID: 21275
		string Name { get; }

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x0600531C RID: 21276
		string Platform { get; }

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x0600531D RID: 21277
		string AccountLoginEndpoint { get; }

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x0600531E RID: 21278
		string MetricsClientKey { get; }

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x0600531F RID: 21279
		string MetricsUserIDField { get; }

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06005320 RID: 21280
		DistributionPlatform.User LocalUser { get; }

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06005321 RID: 21281
		bool IsArchiveBranch { get; }

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06005322 RID: 21282
		bool IsPreviousVersionBranch { get; }

		// Token: 0x06005323 RID: 21283
		bool IsDLCStatusReady();

		// Token: 0x06005324 RID: 21284
		string ApplyWordFilter(string text);

		// Token: 0x06005325 RID: 21285
		void GetAuthTicket(DistributionPlatform.AuthTicketHandler callback);

		// Token: 0x06005326 RID: 21286
		bool IsDLCPurchased(string dlcID);

		// Token: 0x06005327 RID: 21287
		bool IsDLCSubscribed(string dlcID);

		// Token: 0x06005328 RID: 21288
		void ToggleDLCSubscription(string dlcID);
	}

	// Token: 0x0200099B RID: 2459
	// (Invoke) Token: 0x0600532A RID: 21290
	public delegate void AuthTicketHandler(byte[] ticket);

	// Token: 0x0200099C RID: 2460
	public abstract class UserId
	{
		// Token: 0x0600532D RID: 21293
		public abstract ulong ToInt64();
	}

	// Token: 0x0200099D RID: 2461
	public abstract class User
	{
		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x0600532F RID: 21295
		public abstract DistributionPlatform.UserId Id { get; }

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06005330 RID: 21296
		public abstract string Name { get; }
	}
}
