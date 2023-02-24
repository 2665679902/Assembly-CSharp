using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000631 RID: 1585
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationAddAttributeOptionsInternal : IDisposable
	{
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06003E6B RID: 15979 RVA: 0x00086170 File Offset: 0x00084370
		// (set) Token: 0x06003E6C RID: 15980 RVA: 0x00086192 File Offset: 0x00084392
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

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06003E6D RID: 15981 RVA: 0x000861A4 File Offset: 0x000843A4
		// (set) Token: 0x06003E6E RID: 15982 RVA: 0x000861C6 File Offset: 0x000843C6
		public AttributeDataInternal? SessionAttribute
		{
			get
			{
				AttributeDataInternal? @default = Helper.GetDefault<AttributeDataInternal?>();
				Helper.TryMarshalGet<AttributeDataInternal>(this.m_SessionAttribute, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<AttributeDataInternal>(ref this.m_SessionAttribute, value);
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06003E6F RID: 15983 RVA: 0x000861D8 File Offset: 0x000843D8
		// (set) Token: 0x06003E70 RID: 15984 RVA: 0x000861FA File Offset: 0x000843FA
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

		// Token: 0x06003E71 RID: 15985 RVA: 0x00086209 File Offset: 0x00084409
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionAttribute);
		}

		// Token: 0x040017B9 RID: 6073
		private int m_ApiVersion;

		// Token: 0x040017BA RID: 6074
		private IntPtr m_SessionAttribute;

		// Token: 0x040017BB RID: 6075
		private SessionAttributeAdvertisementType m_AdvertisementType;
	}
}
