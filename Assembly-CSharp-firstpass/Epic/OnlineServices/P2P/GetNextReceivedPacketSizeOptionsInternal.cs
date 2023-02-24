using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006EC RID: 1772
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetNextReceivedPacketSizeOptionsInternal : IDisposable
	{
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x0600435E RID: 17246 RVA: 0x0008B42C File Offset: 0x0008962C
		// (set) Token: 0x0600435F RID: 17247 RVA: 0x0008B44E File Offset: 0x0008964E
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

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06004360 RID: 17248 RVA: 0x0008B460 File Offset: 0x00089660
		// (set) Token: 0x06004361 RID: 17249 RVA: 0x0008B482 File Offset: 0x00089682
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

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06004362 RID: 17250 RVA: 0x0008B494 File Offset: 0x00089694
		// (set) Token: 0x06004363 RID: 17251 RVA: 0x0008B4B6 File Offset: 0x000896B6
		public byte? RequestedChannel
		{
			get
			{
				byte? @default = Helper.GetDefault<byte?>();
				Helper.TryMarshalGet<byte>(this.m_RequestedChannel, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<byte>(ref this.m_RequestedChannel, value);
			}
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x0008B4C5 File Offset: 0x000896C5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_RequestedChannel);
		}

		// Token: 0x040019D3 RID: 6611
		private int m_ApiVersion;

		// Token: 0x040019D4 RID: 6612
		private IntPtr m_LocalUserId;

		// Token: 0x040019D5 RID: 6613
		private IntPtr m_RequestedChannel;
	}
}
