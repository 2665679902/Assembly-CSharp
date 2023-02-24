using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006FD RID: 1789
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnRemoteConnectionClosedInfoInternal : ICallbackInfo
	{
		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x0008B744 File Offset: 0x00089944
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060043AC RID: 17324 RVA: 0x0008B766 File Offset: 0x00089966
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060043AD RID: 17325 RVA: 0x0008B770 File Offset: 0x00089970
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060043AE RID: 17326 RVA: 0x0008B794 File Offset: 0x00089994
		public ProductUserId RemoteUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_RemoteUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060043AF RID: 17327 RVA: 0x0008B7B8 File Offset: 0x000899B8
		public SocketIdInternal? SocketId
		{
			get
			{
				SocketIdInternal? @default = Helper.GetDefault<SocketIdInternal?>();
				Helper.TryMarshalGet<SocketIdInternal>(this.m_SocketId, out @default);
				return @default;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060043B0 RID: 17328 RVA: 0x0008B7DC File Offset: 0x000899DC
		public ConnectionClosedReason Reason
		{
			get
			{
				ConnectionClosedReason @default = Helper.GetDefault<ConnectionClosedReason>();
				Helper.TryMarshalGet<ConnectionClosedReason>(this.m_Reason, out @default);
				return @default;
			}
		}

		// Token: 0x040019F0 RID: 6640
		private IntPtr m_ClientData;

		// Token: 0x040019F1 RID: 6641
		private IntPtr m_LocalUserId;

		// Token: 0x040019F2 RID: 6642
		private IntPtr m_RemoteUserId;

		// Token: 0x040019F3 RID: 6643
		private IntPtr m_SocketId;

		// Token: 0x040019F4 RID: 6644
		private ConnectionClosedReason m_Reason;
	}
}
