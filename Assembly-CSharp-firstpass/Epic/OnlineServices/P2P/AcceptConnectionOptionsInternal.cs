using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006DF RID: 1759
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AcceptConnectionOptionsInternal : IDisposable
	{
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06004312 RID: 17170 RVA: 0x0008AF4C File Offset: 0x0008914C
		// (set) Token: 0x06004313 RID: 17171 RVA: 0x0008AF6E File Offset: 0x0008916E
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

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06004314 RID: 17172 RVA: 0x0008AF80 File Offset: 0x00089180
		// (set) Token: 0x06004315 RID: 17173 RVA: 0x0008AFA2 File Offset: 0x000891A2
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

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x0008AFB4 File Offset: 0x000891B4
		// (set) Token: 0x06004317 RID: 17175 RVA: 0x0008AFD6 File Offset: 0x000891D6
		public ProductUserId RemoteUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_RemoteUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_RemoteUserId, value);
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06004318 RID: 17176 RVA: 0x0008AFE8 File Offset: 0x000891E8
		// (set) Token: 0x06004319 RID: 17177 RVA: 0x0008B00A File Offset: 0x0008920A
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

		// Token: 0x0600431A RID: 17178 RVA: 0x0008B019 File Offset: 0x00089219
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SocketId);
		}

		// Token: 0x040019AA RID: 6570
		private int m_ApiVersion;

		// Token: 0x040019AB RID: 6571
		private IntPtr m_LocalUserId;

		// Token: 0x040019AC RID: 6572
		private IntPtr m_RemoteUserId;

		// Token: 0x040019AD RID: 6573
		private IntPtr m_SocketId;
	}
}
