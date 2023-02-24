using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000827 RID: 2087
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CatalogOfferInternal : IDisposable
	{
		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06004AC1 RID: 19137 RVA: 0x00092A28 File Offset: 0x00090C28
		// (set) Token: 0x06004AC2 RID: 19138 RVA: 0x00092A4A File Offset: 0x00090C4A
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

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06004AC3 RID: 19139 RVA: 0x00092A5C File Offset: 0x00090C5C
		// (set) Token: 0x06004AC4 RID: 19140 RVA: 0x00092A7E File Offset: 0x00090C7E
		public int ServerIndex
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_ServerIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_ServerIndex, value);
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06004AC5 RID: 19141 RVA: 0x00092A90 File Offset: 0x00090C90
		// (set) Token: 0x06004AC6 RID: 19142 RVA: 0x00092AB2 File Offset: 0x00090CB2
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

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06004AC7 RID: 19143 RVA: 0x00092AC4 File Offset: 0x00090CC4
		// (set) Token: 0x06004AC8 RID: 19144 RVA: 0x00092AE6 File Offset: 0x00090CE6
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

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06004AC9 RID: 19145 RVA: 0x00092AF8 File Offset: 0x00090CF8
		// (set) Token: 0x06004ACA RID: 19146 RVA: 0x00092B1A File Offset: 0x00090D1A
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

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06004ACB RID: 19147 RVA: 0x00092B2C File Offset: 0x00090D2C
		// (set) Token: 0x06004ACC RID: 19148 RVA: 0x00092B4E File Offset: 0x00090D4E
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

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06004ACD RID: 19149 RVA: 0x00092B60 File Offset: 0x00090D60
		// (set) Token: 0x06004ACE RID: 19150 RVA: 0x00092B82 File Offset: 0x00090D82
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

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06004ACF RID: 19151 RVA: 0x00092B94 File Offset: 0x00090D94
		// (set) Token: 0x06004AD0 RID: 19152 RVA: 0x00092BB6 File Offset: 0x00090DB6
		public string TechnicalDetailsText_DEPRECATED
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_TechnicalDetailsText_DEPRECATED, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_TechnicalDetailsText_DEPRECATED, value);
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06004AD1 RID: 19153 RVA: 0x00092BC8 File Offset: 0x00090DC8
		// (set) Token: 0x06004AD2 RID: 19154 RVA: 0x00092BEA File Offset: 0x00090DEA
		public string CurrencyCode
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_CurrencyCode, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_CurrencyCode, value);
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06004AD3 RID: 19155 RVA: 0x00092BFC File Offset: 0x00090DFC
		// (set) Token: 0x06004AD4 RID: 19156 RVA: 0x00092C1E File Offset: 0x00090E1E
		public Result PriceResult
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_PriceResult, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<Result>(ref this.m_PriceResult, value);
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06004AD5 RID: 19157 RVA: 0x00092C30 File Offset: 0x00090E30
		// (set) Token: 0x06004AD6 RID: 19158 RVA: 0x00092C52 File Offset: 0x00090E52
		public uint OriginalPrice
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_OriginalPrice, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_OriginalPrice, value);
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06004AD7 RID: 19159 RVA: 0x00092C64 File Offset: 0x00090E64
		// (set) Token: 0x06004AD8 RID: 19160 RVA: 0x00092C86 File Offset: 0x00090E86
		public uint CurrentPrice
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_CurrentPrice, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_CurrentPrice, value);
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06004AD9 RID: 19161 RVA: 0x00092C98 File Offset: 0x00090E98
		// (set) Token: 0x06004ADA RID: 19162 RVA: 0x00092CBA File Offset: 0x00090EBA
		public byte DiscountPercentage
		{
			get
			{
				byte @default = Helper.GetDefault<byte>();
				Helper.TryMarshalGet<byte>(this.m_DiscountPercentage, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<byte>(ref this.m_DiscountPercentage, value);
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06004ADB RID: 19163 RVA: 0x00092CCC File Offset: 0x00090ECC
		// (set) Token: 0x06004ADC RID: 19164 RVA: 0x00092CEE File Offset: 0x00090EEE
		public long ExpirationTimestamp
		{
			get
			{
				long @default = Helper.GetDefault<long>();
				Helper.TryMarshalGet<long>(this.m_ExpirationTimestamp, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<long>(ref this.m_ExpirationTimestamp, value);
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06004ADD RID: 19165 RVA: 0x00092D00 File Offset: 0x00090F00
		// (set) Token: 0x06004ADE RID: 19166 RVA: 0x00092D22 File Offset: 0x00090F22
		public uint PurchasedCount
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_PurchasedCount, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_PurchasedCount, value);
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06004ADF RID: 19167 RVA: 0x00092D34 File Offset: 0x00090F34
		// (set) Token: 0x06004AE0 RID: 19168 RVA: 0x00092D56 File Offset: 0x00090F56
		public int PurchaseLimit
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_PurchaseLimit, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_PurchaseLimit, value);
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06004AE1 RID: 19169 RVA: 0x00092D68 File Offset: 0x00090F68
		// (set) Token: 0x06004AE2 RID: 19170 RVA: 0x00092D8A File Offset: 0x00090F8A
		public bool AvailableForPurchase
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_AvailableForPurchase, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_AvailableForPurchase, value);
			}
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x00092D99 File Offset: 0x00090F99
		public void Dispose()
		{
		}

		// Token: 0x04001CDD RID: 7389
		private int m_ApiVersion;

		// Token: 0x04001CDE RID: 7390
		private int m_ServerIndex;

		// Token: 0x04001CDF RID: 7391
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_CatalogNamespace;

		// Token: 0x04001CE0 RID: 7392
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Id;

		// Token: 0x04001CE1 RID: 7393
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_TitleText;

		// Token: 0x04001CE2 RID: 7394
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_DescriptionText;

		// Token: 0x04001CE3 RID: 7395
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LongDescriptionText;

		// Token: 0x04001CE4 RID: 7396
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_TechnicalDetailsText_DEPRECATED;

		// Token: 0x04001CE5 RID: 7397
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_CurrencyCode;

		// Token: 0x04001CE6 RID: 7398
		private Result m_PriceResult;

		// Token: 0x04001CE7 RID: 7399
		private uint m_OriginalPrice;

		// Token: 0x04001CE8 RID: 7400
		private uint m_CurrentPrice;

		// Token: 0x04001CE9 RID: 7401
		private byte m_DiscountPercentage;

		// Token: 0x04001CEA RID: 7402
		private long m_ExpirationTimestamp;

		// Token: 0x04001CEB RID: 7403
		private uint m_PurchasedCount;

		// Token: 0x04001CEC RID: 7404
		private int m_PurchaseLimit;

		// Token: 0x04001CED RID: 7405
		private int m_AvailableForPurchase;
	}
}
