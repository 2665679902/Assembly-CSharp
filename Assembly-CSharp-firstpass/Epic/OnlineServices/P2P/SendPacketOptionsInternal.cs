using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000706 RID: 1798
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendPacketOptionsInternal : IDisposable
	{
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060043FB RID: 17403 RVA: 0x0008BE88 File Offset: 0x0008A088
		// (set) Token: 0x060043FC RID: 17404 RVA: 0x0008BEAA File Offset: 0x0008A0AA
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

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060043FD RID: 17405 RVA: 0x0008BEBC File Offset: 0x0008A0BC
		// (set) Token: 0x060043FE RID: 17406 RVA: 0x0008BEDE File Offset: 0x0008A0DE
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

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060043FF RID: 17407 RVA: 0x0008BEF0 File Offset: 0x0008A0F0
		// (set) Token: 0x06004400 RID: 17408 RVA: 0x0008BF12 File Offset: 0x0008A112
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

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06004401 RID: 17409 RVA: 0x0008BF24 File Offset: 0x0008A124
		// (set) Token: 0x06004402 RID: 17410 RVA: 0x0008BF46 File Offset: 0x0008A146
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

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06004403 RID: 17411 RVA: 0x0008BF58 File Offset: 0x0008A158
		// (set) Token: 0x06004404 RID: 17412 RVA: 0x0008BF7A File Offset: 0x0008A17A
		public byte Channel
		{
			get
			{
				byte @default = Helper.GetDefault<byte>();
				Helper.TryMarshalGet<byte>(this.m_Channel, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<byte>(ref this.m_Channel, value);
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06004405 RID: 17413 RVA: 0x0008BF8C File Offset: 0x0008A18C
		// (set) Token: 0x06004406 RID: 17414 RVA: 0x0008BFB4 File Offset: 0x0008A1B4
		public byte[] Data
		{
			get
			{
				byte[] @default = Helper.GetDefault<byte[]>();
				Helper.TryMarshalGet<byte>(this.m_Data, out @default, this.m_DataLengthBytes);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<byte>(ref this.m_Data, value, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06004407 RID: 17415 RVA: 0x0008BFCC File Offset: 0x0008A1CC
		// (set) Token: 0x06004408 RID: 17416 RVA: 0x0008BFEE File Offset: 0x0008A1EE
		public bool AllowDelayedDelivery
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_AllowDelayedDelivery, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_AllowDelayedDelivery, value);
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06004409 RID: 17417 RVA: 0x0008C000 File Offset: 0x0008A200
		// (set) Token: 0x0600440A RID: 17418 RVA: 0x0008C022 File Offset: 0x0008A222
		public PacketReliability Reliability
		{
			get
			{
				PacketReliability @default = Helper.GetDefault<PacketReliability>();
				Helper.TryMarshalGet<PacketReliability>(this.m_Reliability, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<PacketReliability>(ref this.m_Reliability, value);
			}
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x0008C031 File Offset: 0x0008A231
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SocketId);
			Helper.TryMarshalDispose(ref this.m_Data);
		}

		// Token: 0x04001A1D RID: 6685
		private int m_ApiVersion;

		// Token: 0x04001A1E RID: 6686
		private IntPtr m_LocalUserId;

		// Token: 0x04001A1F RID: 6687
		private IntPtr m_RemoteUserId;

		// Token: 0x04001A20 RID: 6688
		private IntPtr m_SocketId;

		// Token: 0x04001A21 RID: 6689
		private byte m_Channel;

		// Token: 0x04001A22 RID: 6690
		private uint m_DataLengthBytes;

		// Token: 0x04001A23 RID: 6691
		private IntPtr m_Data;

		// Token: 0x04001A24 RID: 6692
		private int m_AllowDelayedDelivery;

		// Token: 0x04001A25 RID: 6693
		private PacketReliability m_Reliability;
	}
}
