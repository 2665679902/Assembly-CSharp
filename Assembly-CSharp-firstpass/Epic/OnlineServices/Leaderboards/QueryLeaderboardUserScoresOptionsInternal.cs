using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007FB RID: 2043
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryLeaderboardUserScoresOptionsInternal : IDisposable
	{
		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x0600497C RID: 18812 RVA: 0x00091754 File Offset: 0x0008F954
		// (set) Token: 0x0600497D RID: 18813 RVA: 0x00091776 File Offset: 0x0008F976
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

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x0600497E RID: 18814 RVA: 0x00091788 File Offset: 0x0008F988
		// (set) Token: 0x0600497F RID: 18815 RVA: 0x000917B0 File Offset: 0x0008F9B0
		public ProductUserId[] UserIds
		{
			get
			{
				ProductUserId[] @default = Helper.GetDefault<ProductUserId[]>();
				Helper.TryMarshalGet<ProductUserId>(this.m_UserIds, out @default, this.m_UserIdsCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ProductUserId>(ref this.m_UserIds, value, out this.m_UserIdsCount);
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06004980 RID: 18816 RVA: 0x000917C8 File Offset: 0x0008F9C8
		// (set) Token: 0x06004981 RID: 18817 RVA: 0x000917F0 File Offset: 0x0008F9F0
		public UserScoresQueryStatInfoInternal[] StatInfo
		{
			get
			{
				UserScoresQueryStatInfoInternal[] @default = Helper.GetDefault<UserScoresQueryStatInfoInternal[]>();
				Helper.TryMarshalGet<UserScoresQueryStatInfoInternal>(this.m_StatInfo, out @default, this.m_StatInfoCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<UserScoresQueryStatInfoInternal>(ref this.m_StatInfo, value, out this.m_StatInfoCount);
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06004982 RID: 18818 RVA: 0x00091808 File Offset: 0x0008FA08
		// (set) Token: 0x06004983 RID: 18819 RVA: 0x0009182A File Offset: 0x0008FA2A
		public DateTimeOffset? StartTime
		{
			get
			{
				DateTimeOffset? @default = Helper.GetDefault<DateTimeOffset?>();
				Helper.TryMarshalGet(this.m_StartTime, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_StartTime, value);
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06004984 RID: 18820 RVA: 0x0009183C File Offset: 0x0008FA3C
		// (set) Token: 0x06004985 RID: 18821 RVA: 0x0009185E File Offset: 0x0008FA5E
		public DateTimeOffset? EndTime
		{
			get
			{
				DateTimeOffset? @default = Helper.GetDefault<DateTimeOffset?>();
				Helper.TryMarshalGet(this.m_EndTime, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_EndTime, value);
			}
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x0009186D File Offset: 0x0008FA6D
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserIds);
			Helper.TryMarshalDispose(ref this.m_StatInfo);
		}

		// Token: 0x04001C56 RID: 7254
		private int m_ApiVersion;

		// Token: 0x04001C57 RID: 7255
		private IntPtr m_UserIds;

		// Token: 0x04001C58 RID: 7256
		private uint m_UserIdsCount;

		// Token: 0x04001C59 RID: 7257
		private IntPtr m_StatInfo;

		// Token: 0x04001C5A RID: 7258
		private uint m_StatInfoCount;

		// Token: 0x04001C5B RID: 7259
		private long m_StartTime;

		// Token: 0x04001C5C RID: 7260
		private long m_EndTime;
	}
}
