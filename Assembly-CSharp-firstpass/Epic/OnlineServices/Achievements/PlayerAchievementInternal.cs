using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000945 RID: 2373
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PlayerAchievementInternal : IDisposable
	{
		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x0600523B RID: 21051 RVA: 0x0009A220 File Offset: 0x00098420
		// (set) Token: 0x0600523C RID: 21052 RVA: 0x0009A242 File Offset: 0x00098442
		public int ApiVersion
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_ApiVersion, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_ApiVersion, value);
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x0600523D RID: 21053 RVA: 0x0009A254 File Offset: 0x00098454
		// (set) Token: 0x0600523E RID: 21054 RVA: 0x0009A276 File Offset: 0x00098476
		public string AchievementId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_AchievementId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_AchievementId, value);
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x0600523F RID: 21055 RVA: 0x0009A288 File Offset: 0x00098488
		// (set) Token: 0x06005240 RID: 21056 RVA: 0x0009A2AA File Offset: 0x000984AA
		public double Progress
		{
			get
			{
				double @default = Helper.GetDefault<double>();
				Helper.TryMarshalGet<double>(this.m_Progress, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<double>(ref this.m_Progress, value);
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06005241 RID: 21057 RVA: 0x0009A2BC File Offset: 0x000984BC
		// (set) Token: 0x06005242 RID: 21058 RVA: 0x0009A2DE File Offset: 0x000984DE
		public DateTimeOffset? UnlockTime
		{
			get
			{
				DateTimeOffset? @default = Helper.GetDefault<DateTimeOffset?>();
				Helper.TryMarshalGet(this.m_UnlockTime, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UnlockTime, value);
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06005243 RID: 21059 RVA: 0x0009A2F0 File Offset: 0x000984F0
		// (set) Token: 0x06005244 RID: 21060 RVA: 0x0009A318 File Offset: 0x00098518
		public PlayerStatInfoInternal[] StatInfo
		{
			get
			{
				PlayerStatInfoInternal[] @default = Helper.GetDefault<PlayerStatInfoInternal[]>();
				Helper.TryMarshalGet<PlayerStatInfoInternal>(this.m_StatInfo, out @default, this.m_StatInfoCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<PlayerStatInfoInternal>(ref this.m_StatInfo, value, out this.m_StatInfoCount);
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06005245 RID: 21061 RVA: 0x0009A330 File Offset: 0x00098530
		// (set) Token: 0x06005246 RID: 21062 RVA: 0x0009A352 File Offset: 0x00098552
		public string DisplayName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_DisplayName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_DisplayName, value);
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06005247 RID: 21063 RVA: 0x0009A364 File Offset: 0x00098564
		// (set) Token: 0x06005248 RID: 21064 RVA: 0x0009A386 File Offset: 0x00098586
		public string Description
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Description, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Description, value);
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06005249 RID: 21065 RVA: 0x0009A398 File Offset: 0x00098598
		// (set) Token: 0x0600524A RID: 21066 RVA: 0x0009A3BA File Offset: 0x000985BA
		public string IconURL
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_IconURL, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_IconURL, value);
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x0600524B RID: 21067 RVA: 0x0009A3CC File Offset: 0x000985CC
		// (set) Token: 0x0600524C RID: 21068 RVA: 0x0009A3EE File Offset: 0x000985EE
		public string FlavorText
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_FlavorText, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_FlavorText, value);
			}
		}

		// Token: 0x0600524D RID: 21069 RVA: 0x0009A3FD File Offset: 0x000985FD
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_StatInfo);
		}

		// Token: 0x04001FFD RID: 8189
		private int m_ApiVersion;

		// Token: 0x04001FFE RID: 8190
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AchievementId;

		// Token: 0x04001FFF RID: 8191
		private double m_Progress;

		// Token: 0x04002000 RID: 8192
		private long m_UnlockTime;

		// Token: 0x04002001 RID: 8193
		private int m_StatInfoCount;

		// Token: 0x04002002 RID: 8194
		private IntPtr m_StatInfo;

		// Token: 0x04002003 RID: 8195
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_DisplayName;

		// Token: 0x04002004 RID: 8196
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Description;

		// Token: 0x04002005 RID: 8197
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_IconURL;

		// Token: 0x04002006 RID: 8198
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_FlavorText;
	}
}
