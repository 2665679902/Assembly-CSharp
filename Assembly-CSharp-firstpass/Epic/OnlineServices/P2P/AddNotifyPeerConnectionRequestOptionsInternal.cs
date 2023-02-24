using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006E3 RID: 1763
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyPeerConnectionRequestOptionsInternal : IDisposable
	{
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x0600432E RID: 17198 RVA: 0x0008B128 File Offset: 0x00089328
		// (set) Token: 0x0600432F RID: 17199 RVA: 0x0008B14A File Offset: 0x0008934A
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

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06004330 RID: 17200 RVA: 0x0008B15C File Offset: 0x0008935C
		// (set) Token: 0x06004331 RID: 17201 RVA: 0x0008B17E File Offset: 0x0008937E
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

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06004332 RID: 17202 RVA: 0x0008B190 File Offset: 0x00089390
		// (set) Token: 0x06004333 RID: 17203 RVA: 0x0008B1B2 File Offset: 0x000893B2
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

		// Token: 0x06004334 RID: 17204 RVA: 0x0008B1C1 File Offset: 0x000893C1
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SocketId);
		}

		// Token: 0x040019B5 RID: 6581
		private int m_ApiVersion;

		// Token: 0x040019B6 RID: 6582
		private IntPtr m_LocalUserId;

		// Token: 0x040019B7 RID: 6583
		private IntPtr m_SocketId;
	}
}
