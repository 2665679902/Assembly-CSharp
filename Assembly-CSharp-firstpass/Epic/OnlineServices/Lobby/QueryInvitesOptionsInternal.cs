using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007C1 RID: 1985
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryInvitesOptionsInternal : IDisposable
	{
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06004826 RID: 18470 RVA: 0x00090240 File Offset: 0x0008E440
		// (set) Token: 0x06004827 RID: 18471 RVA: 0x00090262 File Offset: 0x0008E462
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

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06004828 RID: 18472 RVA: 0x00090274 File Offset: 0x0008E474
		// (set) Token: 0x06004829 RID: 18473 RVA: 0x00090296 File Offset: 0x0008E496
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

		// Token: 0x0600482A RID: 18474 RVA: 0x000902A5 File Offset: 0x0008E4A5
		public void Dispose()
		{
		}

		// Token: 0x04001BCC RID: 7116
		private int m_ApiVersion;

		// Token: 0x04001BCD RID: 7117
		private IntPtr m_LocalUserId;
	}
}
