using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200082F RID: 2095
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CheckoutOptionsInternal : IDisposable
	{
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06004B14 RID: 19220 RVA: 0x00093084 File Offset: 0x00091284
		// (set) Token: 0x06004B15 RID: 19221 RVA: 0x000930A6 File Offset: 0x000912A6
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

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06004B16 RID: 19222 RVA: 0x000930B8 File Offset: 0x000912B8
		// (set) Token: 0x06004B17 RID: 19223 RVA: 0x000930DA File Offset: 0x000912DA
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06004B18 RID: 19224 RVA: 0x000930EC File Offset: 0x000912EC
		// (set) Token: 0x06004B19 RID: 19225 RVA: 0x0009310E File Offset: 0x0009130E
		public string OverrideCatalogNamespace
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_OverrideCatalogNamespace, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_OverrideCatalogNamespace, value);
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06004B1A RID: 19226 RVA: 0x00093120 File Offset: 0x00091320
		// (set) Token: 0x06004B1B RID: 19227 RVA: 0x00093148 File Offset: 0x00091348
		public CheckoutEntryInternal[] Entries
		{
			get
			{
				CheckoutEntryInternal[] @default = Helper.GetDefault<CheckoutEntryInternal[]>();
				Helper.TryMarshalGet<CheckoutEntryInternal>(this.m_Entries, out @default, this.m_EntryCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<CheckoutEntryInternal>(ref this.m_Entries, value, out this.m_EntryCount);
			}
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x0009315D File Offset: 0x0009135D
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Entries);
		}

		// Token: 0x04001D05 RID: 7429
		private int m_ApiVersion;

		// Token: 0x04001D06 RID: 7430
		private IntPtr m_LocalUserId;

		// Token: 0x04001D07 RID: 7431
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_OverrideCatalogNamespace;

		// Token: 0x04001D08 RID: 7432
		private uint m_EntryCount;

		// Token: 0x04001D09 RID: 7433
		private IntPtr m_Entries;
	}
}
