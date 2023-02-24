using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000825 RID: 2085
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CatalogItemInternal : IDisposable
	{
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06004A88 RID: 19080 RVA: 0x000926D0 File Offset: 0x000908D0
		// (set) Token: 0x06004A89 RID: 19081 RVA: 0x000926F2 File Offset: 0x000908F2
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

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06004A8A RID: 19082 RVA: 0x00092704 File Offset: 0x00090904
		// (set) Token: 0x06004A8B RID: 19083 RVA: 0x00092726 File Offset: 0x00090926
		public string CatalogNamespace
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_CatalogNamespace, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_CatalogNamespace, value);
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06004A8C RID: 19084 RVA: 0x00092738 File Offset: 0x00090938
		// (set) Token: 0x06004A8D RID: 19085 RVA: 0x0009275A File Offset: 0x0009095A
		public string Id
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Id, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Id, value);
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06004A8E RID: 19086 RVA: 0x0009276C File Offset: 0x0009096C
		// (set) Token: 0x06004A8F RID: 19087 RVA: 0x0009278E File Offset: 0x0009098E
		public string EntitlementName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_EntitlementName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_EntitlementName, value);
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06004A90 RID: 19088 RVA: 0x000927A0 File Offset: 0x000909A0
		// (set) Token: 0x06004A91 RID: 19089 RVA: 0x000927C2 File Offset: 0x000909C2
		public string TitleText
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_TitleText, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_TitleText, value);
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06004A92 RID: 19090 RVA: 0x000927D4 File Offset: 0x000909D4
		// (set) Token: 0x06004A93 RID: 19091 RVA: 0x000927F6 File Offset: 0x000909F6
		public string DescriptionText
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_DescriptionText, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_DescriptionText, value);
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06004A94 RID: 19092 RVA: 0x00092808 File Offset: 0x00090A08
		// (set) Token: 0x06004A95 RID: 19093 RVA: 0x0009282A File Offset: 0x00090A2A
		public string LongDescriptionText
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LongDescriptionText, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_LongDescriptionText, value);
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06004A96 RID: 19094 RVA: 0x0009283C File Offset: 0x00090A3C
		// (set) Token: 0x06004A97 RID: 19095 RVA: 0x0009285E File Offset: 0x00090A5E
		public string TechnicalDetailsText
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_TechnicalDetailsText, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_TechnicalDetailsText, value);
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06004A98 RID: 19096 RVA: 0x00092870 File Offset: 0x00090A70
		// (set) Token: 0x06004A99 RID: 19097 RVA: 0x00092892 File Offset: 0x00090A92
		public string DeveloperText
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_DeveloperText, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_DeveloperText, value);
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06004A9A RID: 19098 RVA: 0x000928A4 File Offset: 0x00090AA4
		// (set) Token: 0x06004A9B RID: 19099 RVA: 0x000928C6 File Offset: 0x00090AC6
		public EcomItemType ItemType
		{
			get
			{
				EcomItemType @default = Helper.GetDefault<EcomItemType>();
				Helper.TryMarshalGet<EcomItemType>(this.m_ItemType, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<EcomItemType>(ref this.m_ItemType, value);
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06004A9C RID: 19100 RVA: 0x000928D8 File Offset: 0x00090AD8
		// (set) Token: 0x06004A9D RID: 19101 RVA: 0x000928FA File Offset: 0x00090AFA
		public long EntitlementEndTimestamp
		{
			get
			{
				long @default = Helper.GetDefault<long>();
				Helper.TryMarshalGet<long>(this.m_EntitlementEndTimestamp, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<long>(ref this.m_EntitlementEndTimestamp, value);
			}
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x00092909 File Offset: 0x00090B09
		public void Dispose()
		{
		}

		// Token: 0x04001CC2 RID: 7362
		private int m_ApiVersion;

		// Token: 0x04001CC3 RID: 7363
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_CatalogNamespace;

		// Token: 0x04001CC4 RID: 7364
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Id;

		// Token: 0x04001CC5 RID: 7365
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_EntitlementName;

		// Token: 0x04001CC6 RID: 7366
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_TitleText;

		// Token: 0x04001CC7 RID: 7367
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_DescriptionText;

		// Token: 0x04001CC8 RID: 7368
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LongDescriptionText;

		// Token: 0x04001CC9 RID: 7369
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_TechnicalDetailsText;

		// Token: 0x04001CCA RID: 7370
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_DeveloperText;

		// Token: 0x04001CCB RID: 7371
		private EcomItemType m_ItemType;

		// Token: 0x04001CCC RID: 7372
		private long m_EntitlementEndTimestamp;
	}
}
