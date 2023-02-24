using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000951 RID: 2385
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnlockedAchievementInternal : IDisposable
	{
		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06005299 RID: 21145 RVA: 0x0009A89C File Offset: 0x00098A9C
		// (set) Token: 0x0600529A RID: 21146 RVA: 0x0009A8BE File Offset: 0x00098ABE
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

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x0600529B RID: 21147 RVA: 0x0009A8D0 File Offset: 0x00098AD0
		// (set) Token: 0x0600529C RID: 21148 RVA: 0x0009A8F2 File Offset: 0x00098AF2
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

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x0600529D RID: 21149 RVA: 0x0009A904 File Offset: 0x00098B04
		// (set) Token: 0x0600529E RID: 21150 RVA: 0x0009A926 File Offset: 0x00098B26
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

		// Token: 0x0600529F RID: 21151 RVA: 0x0009A935 File Offset: 0x00098B35
		public void Dispose()
		{
		}

		// Token: 0x04002026 RID: 8230
		private int m_ApiVersion;

		// Token: 0x04002027 RID: 8231
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AchievementId;

		// Token: 0x04002028 RID: 8232
		private long m_UnlockTime;
	}
}
