using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200061E RID: 1566
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsAttributeInternal : IDisposable
	{
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06003DE2 RID: 15842 RVA: 0x00085848 File Offset: 0x00083A48
		// (set) Token: 0x06003DE3 RID: 15843 RVA: 0x0008586A File Offset: 0x00083A6A
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

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06003DE4 RID: 15844 RVA: 0x0008587C File Offset: 0x00083A7C
		// (set) Token: 0x06003DE5 RID: 15845 RVA: 0x0008589E File Offset: 0x00083A9E
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

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06003DE6 RID: 15846 RVA: 0x000858B0 File Offset: 0x00083AB0
		// (set) Token: 0x06003DE7 RID: 15847 RVA: 0x000858D2 File Offset: 0x00083AD2
		public SessionAttributeAdvertisementType AdvertisementType
		{
			get
			{
				SessionAttributeAdvertisementType @default = Helper.GetDefault<SessionAttributeAdvertisementType>();
				Helper.TryMarshalGet<SessionAttributeAdvertisementType>(this.m_AdvertisementType, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<SessionAttributeAdvertisementType>(ref this.m_AdvertisementType, value);
			}
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x000858E1 File Offset: 0x00083AE1
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Data);
		}

		// Token: 0x04001786 RID: 6022
		private int m_ApiVersion;

		// Token: 0x04001787 RID: 6023
		private IntPtr m_Data;

		// Token: 0x04001788 RID: 6024
		private SessionAttributeAdvertisementType m_AdvertisementType;
	}
}
