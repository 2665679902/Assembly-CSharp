using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005B6 RID: 1462
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryStatsOptionsInternal : IDisposable
	{
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06003BB8 RID: 15288 RVA: 0x000839B8 File Offset: 0x00081BB8
		// (set) Token: 0x06003BB9 RID: 15289 RVA: 0x000839DA File Offset: 0x00081BDA
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

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x000839EC File Offset: 0x00081BEC
		// (set) Token: 0x06003BBB RID: 15291 RVA: 0x00083A0E File Offset: 0x00081C0E
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x00083A20 File Offset: 0x00081C20
		// (set) Token: 0x06003BBD RID: 15293 RVA: 0x00083A42 File Offset: 0x00081C42
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

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06003BBE RID: 15294 RVA: 0x00083A54 File Offset: 0x00081C54
		// (set) Token: 0x06003BBF RID: 15295 RVA: 0x00083A76 File Offset: 0x00081C76
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

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06003BC0 RID: 15296 RVA: 0x00083A88 File Offset: 0x00081C88
		// (set) Token: 0x06003BC1 RID: 15297 RVA: 0x00083AB0 File Offset: 0x00081CB0
		public string[] StatNames
		{
			get
			{
				string[] @default = Helper.GetDefault<string[]>();
				Helper.TryMarshalGet<string>(this.m_StatNames, out @default, this.m_StatNamesCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_StatNames, value, out this.m_StatNamesCount);
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06003BC2 RID: 15298 RVA: 0x00083AC8 File Offset: 0x00081CC8
		// (set) Token: 0x06003BC3 RID: 15299 RVA: 0x00083AEA File Offset: 0x00081CEA
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x00083AF9 File Offset: 0x00081CF9
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_StatNames);
		}

		// Token: 0x040016C5 RID: 5829
		private int m_ApiVersion;

		// Token: 0x040016C6 RID: 5830
		private IntPtr m_LocalUserId;

		// Token: 0x040016C7 RID: 5831
		private long m_StartTime;

		// Token: 0x040016C8 RID: 5832
		private long m_EndTime;

		// Token: 0x040016C9 RID: 5833
		private IntPtr m_StatNames;

		// Token: 0x040016CA RID: 5834
		private uint m_StatNamesCount;

		// Token: 0x040016CB RID: 5835
		private IntPtr m_TargetUserId;
	}
}
