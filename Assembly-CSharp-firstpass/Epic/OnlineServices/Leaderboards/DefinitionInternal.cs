using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007DD RID: 2013
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DefinitionInternal : IDisposable
	{
		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x060048CA RID: 18634 RVA: 0x00090BD8 File Offset: 0x0008EDD8
		// (set) Token: 0x060048CB RID: 18635 RVA: 0x00090BFA File Offset: 0x0008EDFA
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

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060048CC RID: 18636 RVA: 0x00090C0C File Offset: 0x0008EE0C
		// (set) Token: 0x060048CD RID: 18637 RVA: 0x00090C2E File Offset: 0x0008EE2E
		public string LeaderboardId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LeaderboardId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_LeaderboardId, value);
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060048CE RID: 18638 RVA: 0x00090C40 File Offset: 0x0008EE40
		// (set) Token: 0x060048CF RID: 18639 RVA: 0x00090C62 File Offset: 0x0008EE62
		public string StatName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_StatName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_StatName, value);
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060048D0 RID: 18640 RVA: 0x00090C74 File Offset: 0x0008EE74
		// (set) Token: 0x060048D1 RID: 18641 RVA: 0x00090C96 File Offset: 0x0008EE96
		public LeaderboardAggregation Aggregation
		{
			get
			{
				LeaderboardAggregation @default = Helper.GetDefault<LeaderboardAggregation>();
				Helper.TryMarshalGet<LeaderboardAggregation>(this.m_Aggregation, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<LeaderboardAggregation>(ref this.m_Aggregation, value);
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060048D2 RID: 18642 RVA: 0x00090CA8 File Offset: 0x0008EEA8
		// (set) Token: 0x060048D3 RID: 18643 RVA: 0x00090CCA File Offset: 0x0008EECA
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

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060048D4 RID: 18644 RVA: 0x00090CDC File Offset: 0x0008EEDC
		// (set) Token: 0x060048D5 RID: 18645 RVA: 0x00090CFE File Offset: 0x0008EEFE
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

		// Token: 0x060048D6 RID: 18646 RVA: 0x00090D0D File Offset: 0x0008EF0D
		public void Dispose()
		{
		}

		// Token: 0x04001C0F RID: 7183
		private int m_ApiVersion;

		// Token: 0x04001C10 RID: 7184
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LeaderboardId;

		// Token: 0x04001C11 RID: 7185
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_StatName;

		// Token: 0x04001C12 RID: 7186
		private LeaderboardAggregation m_Aggregation;

		// Token: 0x04001C13 RID: 7187
		private long m_StartTime;

		// Token: 0x04001C14 RID: 7188
		private long m_EndTime;
	}
}
