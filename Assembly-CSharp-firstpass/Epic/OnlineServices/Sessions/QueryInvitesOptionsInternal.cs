using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200060E RID: 1550
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryInvitesOptionsInternal : IDisposable
	{
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06003D87 RID: 15751 RVA: 0x00085238 File Offset: 0x00083438
		// (set) Token: 0x06003D88 RID: 15752 RVA: 0x0008525A File Offset: 0x0008345A
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

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06003D89 RID: 15753 RVA: 0x0008526C File Offset: 0x0008346C
		// (set) Token: 0x06003D8A RID: 15754 RVA: 0x0008528E File Offset: 0x0008348E
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

		// Token: 0x06003D8B RID: 15755 RVA: 0x0008529D File Offset: 0x0008349D
		public void Dispose()
		{
		}

		// Token: 0x04001761 RID: 5985
		private int m_ApiVersion;

		// Token: 0x04001762 RID: 5986
		private IntPtr m_LocalUserId;
	}
}
