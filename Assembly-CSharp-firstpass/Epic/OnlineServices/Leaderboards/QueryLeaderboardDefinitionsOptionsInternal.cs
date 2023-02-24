using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007F7 RID: 2039
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryLeaderboardDefinitionsOptionsInternal : IDisposable
	{
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06004962 RID: 18786 RVA: 0x000915E4 File Offset: 0x0008F7E4
		// (set) Token: 0x06004963 RID: 18787 RVA: 0x00091606 File Offset: 0x0008F806
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

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06004964 RID: 18788 RVA: 0x00091618 File Offset: 0x0008F818
		// (set) Token: 0x06004965 RID: 18789 RVA: 0x0009163A File Offset: 0x0008F83A
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

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06004966 RID: 18790 RVA: 0x0009164C File Offset: 0x0008F84C
		// (set) Token: 0x06004967 RID: 18791 RVA: 0x0009166E File Offset: 0x0008F86E
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

		// Token: 0x06004968 RID: 18792 RVA: 0x0009167D File Offset: 0x0008F87D
		public void Dispose()
		{
		}

		// Token: 0x04001C4C RID: 7244
		private int m_ApiVersion;

		// Token: 0x04001C4D RID: 7245
		private long m_StartTime;

		// Token: 0x04001C4E RID: 7246
		private long m_EndTime;
	}
}
