using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006E1 RID: 1761
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyPeerConnectionClosedOptionsInternal : IDisposable
	{
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06004321 RID: 17185 RVA: 0x0008B054 File Offset: 0x00089254
		// (set) Token: 0x06004322 RID: 17186 RVA: 0x0008B076 File Offset: 0x00089276
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

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06004323 RID: 17187 RVA: 0x0008B088 File Offset: 0x00089288
		// (set) Token: 0x06004324 RID: 17188 RVA: 0x0008B0AA File Offset: 0x000892AA
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

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06004325 RID: 17189 RVA: 0x0008B0BC File Offset: 0x000892BC
		// (set) Token: 0x06004326 RID: 17190 RVA: 0x0008B0DE File Offset: 0x000892DE
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

		// Token: 0x06004327 RID: 17191 RVA: 0x0008B0ED File Offset: 0x000892ED
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SocketId);
		}

		// Token: 0x040019B0 RID: 6576
		private int m_ApiVersion;

		// Token: 0x040019B1 RID: 6577
		private IntPtr m_LocalUserId;

		// Token: 0x040019B2 RID: 6578
		private IntPtr m_SocketId;
	}
}
