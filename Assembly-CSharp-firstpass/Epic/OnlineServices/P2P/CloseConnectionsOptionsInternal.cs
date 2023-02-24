using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006E7 RID: 1767
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CloseConnectionsOptionsInternal : IDisposable
	{
		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x0600434C RID: 17228 RVA: 0x0008B318 File Offset: 0x00089518
		// (set) Token: 0x0600434D RID: 17229 RVA: 0x0008B33A File Offset: 0x0008953A
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

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x0600434E RID: 17230 RVA: 0x0008B34C File Offset: 0x0008954C
		// (set) Token: 0x0600434F RID: 17231 RVA: 0x0008B36E File Offset: 0x0008956E
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

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06004350 RID: 17232 RVA: 0x0008B380 File Offset: 0x00089580
		// (set) Token: 0x06004351 RID: 17233 RVA: 0x0008B3A2 File Offset: 0x000895A2
		public SocketIdInternal? SocketId
		{
			get
			{
				SocketIdInternal? @default = Helper.GetDefault<SocketIdInternal?>();
				Helper.TryMarshalGet<SocketIdInternal>(this.m_SocketId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<SocketIdInternal>(ref this.m_SocketId, value);
			}
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x0008B3B1 File Offset: 0x000895B1
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SocketId);
		}

		// Token: 0x040019C1 RID: 6593
		private int m_ApiVersion;

		// Token: 0x040019C2 RID: 6594
		private IntPtr m_LocalUserId;

		// Token: 0x040019C3 RID: 6595
		private IntPtr m_SocketId;
	}
}
