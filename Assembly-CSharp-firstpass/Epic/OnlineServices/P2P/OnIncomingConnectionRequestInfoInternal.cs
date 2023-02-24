using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006F5 RID: 1781
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnIncomingConnectionRequestInfoInternal : ICallbackInfo
	{
		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06004380 RID: 17280 RVA: 0x0008B5A0 File Offset: 0x000897A0
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06004381 RID: 17281 RVA: 0x0008B5C2 File Offset: 0x000897C2
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06004382 RID: 17282 RVA: 0x0008B5CC File Offset: 0x000897CC
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06004383 RID: 17283 RVA: 0x0008B5F0 File Offset: 0x000897F0
		public ProductUserId RemoteUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_RemoteUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06004384 RID: 17284 RVA: 0x0008B614 File Offset: 0x00089814
		public SocketIdInternal? SocketId
		{
			get
			{
				SocketIdInternal? @default = Helper.GetDefault<SocketIdInternal?>();
				Helper.TryMarshalGet<SocketIdInternal>(this.m_SocketId, out @default);
				return @default;
			}
		}

		// Token: 0x040019E1 RID: 6625
		private IntPtr m_ClientData;

		// Token: 0x040019E2 RID: 6626
		private IntPtr m_LocalUserId;

		// Token: 0x040019E3 RID: 6627
		private IntPtr m_RemoteUserId;

		// Token: 0x040019E4 RID: 6628
		private IntPtr m_SocketId;
	}
}
