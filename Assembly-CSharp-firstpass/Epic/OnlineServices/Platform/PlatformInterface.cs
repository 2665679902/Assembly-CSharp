using System;
using System.Runtime.InteropServices;
using System.Text;
using Epic.OnlineServices.Achievements;
using Epic.OnlineServices.Auth;
using Epic.OnlineServices.Connect;
using Epic.OnlineServices.Ecom;
using Epic.OnlineServices.Friends;
using Epic.OnlineServices.Leaderboards;
using Epic.OnlineServices.Lobby;
using Epic.OnlineServices.Metrics;
using Epic.OnlineServices.P2P;
using Epic.OnlineServices.PlayerDataStorage;
using Epic.OnlineServices.Presence;
using Epic.OnlineServices.Sessions;
using Epic.OnlineServices.Stats;
using Epic.OnlineServices.TitleStorage;
using Epic.OnlineServices.UI;
using Epic.OnlineServices.UserInfo;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020006DB RID: 1755
	public sealed class PlatformInterface : Handle
	{
		// Token: 0x060042C9 RID: 17097 RVA: 0x0008AA83 File Offset: 0x00088C83
		public PlatformInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x0008AA8C File Offset: 0x00088C8C
		public static Result Initialize(InitializeOptions options)
		{
			InitializeOptionsInternal initializeOptionsInternal = Helper.CopyProperties<InitializeOptionsInternal>(options);
			int[] array = new int[] { 1, 1 };
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<int>(ref zero, array);
			initializeOptionsInternal.Reserved = zero;
			Result result = PlatformInterface.EOS_Initialize(ref initializeOptionsInternal);
			Helper.TryMarshalDispose<InitializeOptionsInternal>(ref initializeOptionsInternal);
			Helper.TryMarshalDispose(ref zero);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x0008AAEC File Offset: 0x00088CEC
		public static Result Shutdown()
		{
			Result result = PlatformInterface.EOS_Shutdown();
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x0008AB10 File Offset: 0x00088D10
		public static PlatformInterface Create(Options options)
		{
			OptionsInternal optionsInternal = Helper.CopyProperties<OptionsInternal>(options);
			IntPtr intPtr = PlatformInterface.EOS_Platform_Create(ref optionsInternal);
			Helper.TryMarshalDispose<OptionsInternal>(ref optionsInternal);
			PlatformInterface @default = Helper.GetDefault<PlatformInterface>();
			Helper.TryMarshalGet<PlatformInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x0008AB42 File Offset: 0x00088D42
		public void Release()
		{
			PlatformInterface.EOS_Platform_Release(base.InnerHandle);
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x0008AB4F File Offset: 0x00088D4F
		public void Tick()
		{
			PlatformInterface.EOS_Platform_Tick(base.InnerHandle);
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x0008AB5C File Offset: 0x00088D5C
		public MetricsInterface GetMetricsInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetMetricsInterface(base.InnerHandle);
			MetricsInterface @default = Helper.GetDefault<MetricsInterface>();
			Helper.TryMarshalGet<MetricsInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x0008AB84 File Offset: 0x00088D84
		public AuthInterface GetAuthInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetAuthInterface(base.InnerHandle);
			AuthInterface @default = Helper.GetDefault<AuthInterface>();
			Helper.TryMarshalGet<AuthInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x0008ABAC File Offset: 0x00088DAC
		public ConnectInterface GetConnectInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetConnectInterface(base.InnerHandle);
			ConnectInterface @default = Helper.GetDefault<ConnectInterface>();
			Helper.TryMarshalGet<ConnectInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x0008ABD4 File Offset: 0x00088DD4
		public EcomInterface GetEcomInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetEcomInterface(base.InnerHandle);
			EcomInterface @default = Helper.GetDefault<EcomInterface>();
			Helper.TryMarshalGet<EcomInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x0008ABFC File Offset: 0x00088DFC
		public UIInterface GetUIInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetUIInterface(base.InnerHandle);
			UIInterface @default = Helper.GetDefault<UIInterface>();
			Helper.TryMarshalGet<UIInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x0008AC24 File Offset: 0x00088E24
		public FriendsInterface GetFriendsInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetFriendsInterface(base.InnerHandle);
			FriendsInterface @default = Helper.GetDefault<FriendsInterface>();
			Helper.TryMarshalGet<FriendsInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x0008AC4C File Offset: 0x00088E4C
		public PresenceInterface GetPresenceInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetPresenceInterface(base.InnerHandle);
			PresenceInterface @default = Helper.GetDefault<PresenceInterface>();
			Helper.TryMarshalGet<PresenceInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x0008AC74 File Offset: 0x00088E74
		public SessionsInterface GetSessionsInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetSessionsInterface(base.InnerHandle);
			SessionsInterface @default = Helper.GetDefault<SessionsInterface>();
			Helper.TryMarshalGet<SessionsInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x0008AC9C File Offset: 0x00088E9C
		public LobbyInterface GetLobbyInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetLobbyInterface(base.InnerHandle);
			LobbyInterface @default = Helper.GetDefault<LobbyInterface>();
			Helper.TryMarshalGet<LobbyInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x0008ACC4 File Offset: 0x00088EC4
		public UserInfoInterface GetUserInfoInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetUserInfoInterface(base.InnerHandle);
			UserInfoInterface @default = Helper.GetDefault<UserInfoInterface>();
			Helper.TryMarshalGet<UserInfoInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x0008ACEC File Offset: 0x00088EEC
		public P2PInterface GetP2PInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetP2PInterface(base.InnerHandle);
			P2PInterface @default = Helper.GetDefault<P2PInterface>();
			Helper.TryMarshalGet<P2PInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x0008AD14 File Offset: 0x00088F14
		public PlayerDataStorageInterface GetPlayerDataStorageInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetPlayerDataStorageInterface(base.InnerHandle);
			PlayerDataStorageInterface @default = Helper.GetDefault<PlayerDataStorageInterface>();
			Helper.TryMarshalGet<PlayerDataStorageInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x0008AD3C File Offset: 0x00088F3C
		public TitleStorageInterface GetTitleStorageInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetTitleStorageInterface(base.InnerHandle);
			TitleStorageInterface @default = Helper.GetDefault<TitleStorageInterface>();
			Helper.TryMarshalGet<TitleStorageInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x0008AD64 File Offset: 0x00088F64
		public AchievementsInterface GetAchievementsInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetAchievementsInterface(base.InnerHandle);
			AchievementsInterface @default = Helper.GetDefault<AchievementsInterface>();
			Helper.TryMarshalGet<AchievementsInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x0008AD8C File Offset: 0x00088F8C
		public StatsInterface GetStatsInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetStatsInterface(base.InnerHandle);
			StatsInterface @default = Helper.GetDefault<StatsInterface>();
			Helper.TryMarshalGet<StatsInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x0008ADB4 File Offset: 0x00088FB4
		public LeaderboardsInterface GetLeaderboardsInterface()
		{
			IntPtr intPtr = PlatformInterface.EOS_Platform_GetLeaderboardsInterface(base.InnerHandle);
			LeaderboardsInterface @default = Helper.GetDefault<LeaderboardsInterface>();
			Helper.TryMarshalGet<LeaderboardsInterface>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x0008ADDC File Offset: 0x00088FDC
		public Result GetActiveCountryCode(EpicAccountId localUserId, StringBuilder outBuffer, ref int inOutBufferLength)
		{
			Result result = PlatformInterface.EOS_Platform_GetActiveCountryCode(base.InnerHandle, localUserId.InnerHandle, outBuffer, ref inOutBufferLength);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x0008AE0C File Offset: 0x0008900C
		public Result GetActiveLocaleCode(EpicAccountId localUserId, StringBuilder outBuffer, ref int inOutBufferLength)
		{
			Result result = PlatformInterface.EOS_Platform_GetActiveLocaleCode(base.InnerHandle, localUserId.InnerHandle, outBuffer, ref inOutBufferLength);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x0008AE3C File Offset: 0x0008903C
		public Result GetOverrideCountryCode(StringBuilder outBuffer, ref int inOutBufferLength)
		{
			Result result = PlatformInterface.EOS_Platform_GetOverrideCountryCode(base.InnerHandle, outBuffer, ref inOutBufferLength);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x0008AE68 File Offset: 0x00089068
		public Result GetOverrideLocaleCode(StringBuilder outBuffer, ref int inOutBufferLength)
		{
			Result result = PlatformInterface.EOS_Platform_GetOverrideLocaleCode(base.InnerHandle, outBuffer, ref inOutBufferLength);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x0008AE94 File Offset: 0x00089094
		public Result SetOverrideCountryCode(string newCountryCode)
		{
			Result result = PlatformInterface.EOS_Platform_SetOverrideCountryCode(base.InnerHandle, newCountryCode);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x0008AEBC File Offset: 0x000890BC
		public Result SetOverrideLocaleCode(string newLocaleCode)
		{
			Result result = PlatformInterface.EOS_Platform_SetOverrideLocaleCode(base.InnerHandle, newLocaleCode);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x0008AEE4 File Offset: 0x000890E4
		public Result CheckForLauncherAndRestart()
		{
			Result result = PlatformInterface.EOS_Platform_CheckForLauncherAndRestart(base.InnerHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060042E6 RID: 17126
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Platform_CheckForLauncherAndRestart(IntPtr handle);

		// Token: 0x060042E7 RID: 17127
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Platform_SetOverrideLocaleCode(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)] string newLocaleCode);

		// Token: 0x060042E8 RID: 17128
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Platform_SetOverrideCountryCode(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)] string newCountryCode);

		// Token: 0x060042E9 RID: 17129
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Platform_GetOverrideLocaleCode(IntPtr handle, StringBuilder outBuffer, ref int inOutBufferLength);

		// Token: 0x060042EA RID: 17130
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Platform_GetOverrideCountryCode(IntPtr handle, StringBuilder outBuffer, ref int inOutBufferLength);

		// Token: 0x060042EB RID: 17131
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Platform_GetActiveLocaleCode(IntPtr handle, IntPtr localUserId, StringBuilder outBuffer, ref int inOutBufferLength);

		// Token: 0x060042EC RID: 17132
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Platform_GetActiveCountryCode(IntPtr handle, IntPtr localUserId, StringBuilder outBuffer, ref int inOutBufferLength);

		// Token: 0x060042ED RID: 17133
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetLeaderboardsInterface(IntPtr handle);

		// Token: 0x060042EE RID: 17134
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetStatsInterface(IntPtr handle);

		// Token: 0x060042EF RID: 17135
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetAchievementsInterface(IntPtr handle);

		// Token: 0x060042F0 RID: 17136
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetTitleStorageInterface(IntPtr handle);

		// Token: 0x060042F1 RID: 17137
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetPlayerDataStorageInterface(IntPtr handle);

		// Token: 0x060042F2 RID: 17138
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetP2PInterface(IntPtr handle);

		// Token: 0x060042F3 RID: 17139
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetUserInfoInterface(IntPtr handle);

		// Token: 0x060042F4 RID: 17140
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetLobbyInterface(IntPtr handle);

		// Token: 0x060042F5 RID: 17141
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetSessionsInterface(IntPtr handle);

		// Token: 0x060042F6 RID: 17142
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetPresenceInterface(IntPtr handle);

		// Token: 0x060042F7 RID: 17143
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetFriendsInterface(IntPtr handle);

		// Token: 0x060042F8 RID: 17144
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetUIInterface(IntPtr handle);

		// Token: 0x060042F9 RID: 17145
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetEcomInterface(IntPtr handle);

		// Token: 0x060042FA RID: 17146
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetConnectInterface(IntPtr handle);

		// Token: 0x060042FB RID: 17147
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetAuthInterface(IntPtr handle);

		// Token: 0x060042FC RID: 17148
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_GetMetricsInterface(IntPtr handle);

		// Token: 0x060042FD RID: 17149
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Platform_Tick(IntPtr handle);

		// Token: 0x060042FE RID: 17150
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Platform_Release(IntPtr handle);

		// Token: 0x060042FF RID: 17151
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Platform_Create(ref OptionsInternal options);

		// Token: 0x06004300 RID: 17152
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Shutdown();

		// Token: 0x06004301 RID: 17153
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Initialize(ref InitializeOptionsInternal options);

		// Token: 0x040019A1 RID: 6561
		public const int OptionsApiLatest = 8;

		// Token: 0x040019A2 RID: 6562
		public const int LocalecodeMaxBufferLen = 10;

		// Token: 0x040019A3 RID: 6563
		public const int LocalecodeMaxLength = 9;

		// Token: 0x040019A4 RID: 6564
		public const int CountrycodeMaxBufferLen = 5;

		// Token: 0x040019A5 RID: 6565
		public const int CountrycodeMaxLength = 4;

		// Token: 0x040019A6 RID: 6566
		public const int InitializeApiLatest = 3;
	}
}
