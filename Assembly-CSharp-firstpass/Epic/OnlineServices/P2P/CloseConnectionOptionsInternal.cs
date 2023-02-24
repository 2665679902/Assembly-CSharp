using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006E5 RID: 1765
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CloseConnectionOptionsInternal : IDisposable
	{
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x0600433D RID: 17213 RVA: 0x0008B210 File Offset: 0x00089410
		// (set) Token: 0x0600433E RID: 17214 RVA: 0x0008B232 File Offset: 0x00089432
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

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x0600433F RID: 17215 RVA: 0x0008B244 File Offset: 0x00089444
		// (set) Token: 0x06004340 RID: 17216 RVA: 0x0008B266 File Offset: 0x00089466
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

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06004341 RID: 17217 RVA: 0x0008B278 File Offset: 0x00089478
		// (set) Token: 0x06004342 RID: 17218 RVA: 0x0008B29A File Offset: 0x0008949A
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

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06004343 RID: 17219 RVA: 0x0008B2AC File Offset: 0x000894AC
		// (set) Token: 0x06004344 RID: 17220 RVA: 0x0008B2CE File Offset: 0x000894CE
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

		// Token: 0x06004345 RID: 17221 RVA: 0x0008B2DD File Offset: 0x000894DD
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SocketId);
		}

		// Token: 0x040019BB RID: 6587
		private int m_ApiVersion;

		// Token: 0x040019BC RID: 6588
		private IntPtr m_LocalUserId;

		// Token: 0x040019BD RID: 6589
		private IntPtr m_RemoteUserId;

		// Token: 0x040019BE RID: 6590
		private IntPtr m_SocketId;
	}
}
