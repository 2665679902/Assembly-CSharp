using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200072C RID: 1836
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AttributeInternal : IDisposable
	{
		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x060044AE RID: 17582 RVA: 0x0008CACC File Offset: 0x0008ACCC
		// (set) Token: 0x060044AF RID: 17583 RVA: 0x0008CAEE File Offset: 0x0008ACEE
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

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x060044B0 RID: 17584 RVA: 0x0008CB00 File Offset: 0x0008AD00
		// (set) Token: 0x060044B1 RID: 17585 RVA: 0x0008CB22 File Offset: 0x0008AD22
		public AttributeDataInternal? Data
		{
			get
			{
				AttributeDataInternal? @default = Helper.GetDefault<AttributeDataInternal?>();
				Helper.TryMarshalGet<AttributeDataInternal>(this.m_Data, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<AttributeDataInternal>(ref this.m_Data, value);
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060044B2 RID: 17586 RVA: 0x0008CB34 File Offset: 0x0008AD34
		// (set) Token: 0x060044B3 RID: 17587 RVA: 0x0008CB56 File Offset: 0x0008AD56
		public LobbyAttributeVisibility Visibility
		{
			get
			{
				LobbyAttributeVisibility @default = Helper.GetDefault<LobbyAttributeVisibility>();
				Helper.TryMarshalGet<LobbyAttributeVisibility>(this.m_Visibility, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<LobbyAttributeVisibility>(ref this.m_Visibility, value);
			}
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x0008CB65 File Offset: 0x0008AD65
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Data);
		}

		// Token: 0x04001A86 RID: 6790
		private int m_ApiVersion;

		// Token: 0x04001A87 RID: 6791
		private IntPtr m_Data;

		// Token: 0x04001A88 RID: 6792
		private LobbyAttributeVisibility m_Visibility;
	}
}
