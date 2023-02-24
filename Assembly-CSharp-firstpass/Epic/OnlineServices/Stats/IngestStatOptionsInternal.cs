using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005AE RID: 1454
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IngestStatOptionsInternal : IDisposable
	{
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06003B85 RID: 15237 RVA: 0x0008378C File Offset: 0x0008198C
		// (set) Token: 0x06003B86 RID: 15238 RVA: 0x000837AE File Offset: 0x000819AE
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

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06003B87 RID: 15239 RVA: 0x000837C0 File Offset: 0x000819C0
		// (set) Token: 0x06003B88 RID: 15240 RVA: 0x000837E2 File Offset: 0x000819E2
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

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06003B89 RID: 15241 RVA: 0x000837F4 File Offset: 0x000819F4
		// (set) Token: 0x06003B8A RID: 15242 RVA: 0x0008381C File Offset: 0x00081A1C
		public IngestDataInternal[] Stats
		{
			get
			{
				IngestDataInternal[] @default = Helper.GetDefault<IngestDataInternal[]>();
				Helper.TryMarshalGet<IngestDataInternal>(this.m_Stats, out @default, this.m_StatsCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<IngestDataInternal>(ref this.m_Stats, value, out this.m_StatsCount);
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06003B8B RID: 15243 RVA: 0x00083834 File Offset: 0x00081A34
		// (set) Token: 0x06003B8C RID: 15244 RVA: 0x00083856 File Offset: 0x00081A56
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

		// Token: 0x06003B8D RID: 15245 RVA: 0x00083865 File Offset: 0x00081A65
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Stats);
		}

		// Token: 0x040016B3 RID: 5811
		private int m_ApiVersion;

		// Token: 0x040016B4 RID: 5812
		private IntPtr m_LocalUserId;

		// Token: 0x040016B5 RID: 5813
		private IntPtr m_Stats;

		// Token: 0x040016B6 RID: 5814
		private uint m_StatsCount;

		// Token: 0x040016B7 RID: 5815
		private IntPtr m_TargetUserId;
	}
}
