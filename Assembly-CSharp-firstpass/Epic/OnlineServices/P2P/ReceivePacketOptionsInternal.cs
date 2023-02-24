using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000703 RID: 1795
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReceivePacketOptionsInternal : IDisposable
	{
		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x060043E2 RID: 17378 RVA: 0x0008BD28 File Offset: 0x00089F28
		// (set) Token: 0x060043E3 RID: 17379 RVA: 0x0008BD4A File Offset: 0x00089F4A
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

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060043E4 RID: 17380 RVA: 0x0008BD5C File Offset: 0x00089F5C
		// (set) Token: 0x060043E5 RID: 17381 RVA: 0x0008BD7E File Offset: 0x00089F7E
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

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060043E6 RID: 17382 RVA: 0x0008BD90 File Offset: 0x00089F90
		// (set) Token: 0x060043E7 RID: 17383 RVA: 0x0008BDB2 File Offset: 0x00089FB2
		public uint MaxDataSizeBytes
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_MaxDataSizeBytes, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_MaxDataSizeBytes, value);
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060043E8 RID: 17384 RVA: 0x0008BDC4 File Offset: 0x00089FC4
		// (set) Token: 0x060043E9 RID: 17385 RVA: 0x0008BDE6 File Offset: 0x00089FE6
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

		// Token: 0x060043EA RID: 17386 RVA: 0x0008BDF5 File Offset: 0x00089FF5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_RequestedChannel);
		}

		// Token: 0x04001A0E RID: 6670
		private int m_ApiVersion;

		// Token: 0x04001A0F RID: 6671
		private IntPtr m_LocalUserId;

		// Token: 0x04001A10 RID: 6672
		private uint m_MaxDataSizeBytes;

		// Token: 0x04001A11 RID: 6673
		private IntPtr m_RequestedChannel;
	}
}
