using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000878 RID: 2168
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOwnershipOptionsInternal : IDisposable
	{
		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06004D22 RID: 19746 RVA: 0x000952A8 File Offset: 0x000934A8
		// (set) Token: 0x06004D23 RID: 19747 RVA: 0x000952CA File Offset: 0x000934CA
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

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06004D24 RID: 19748 RVA: 0x000952DC File Offset: 0x000934DC
		// (set) Token: 0x06004D25 RID: 19749 RVA: 0x000952FE File Offset: 0x000934FE
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

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06004D26 RID: 19750 RVA: 0x00095310 File Offset: 0x00093510
		// (set) Token: 0x06004D27 RID: 19751 RVA: 0x00095338 File Offset: 0x00093538
		public string[] CatalogItemIds
		{
			get
			{
				string[] @default = Helper.GetDefault<string[]>();
				Helper.TryMarshalGet<string>(this.m_CatalogItemIds, out @default, this.m_CatalogItemIdCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_CatalogItemIds, value, out this.m_CatalogItemIdCount);
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06004D28 RID: 19752 RVA: 0x00095350 File Offset: 0x00093550
		// (set) Token: 0x06004D29 RID: 19753 RVA: 0x00095372 File Offset: 0x00093572
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

		// Token: 0x06004D2A RID: 19754 RVA: 0x00095381 File Offset: 0x00093581
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_CatalogItemIds);
		}

		// Token: 0x04001DE5 RID: 7653
		private int m_ApiVersion;

		// Token: 0x04001DE6 RID: 7654
		private IntPtr m_LocalUserId;

		// Token: 0x04001DE7 RID: 7655
		private IntPtr m_CatalogItemIds;

		// Token: 0x04001DE8 RID: 7656
		private uint m_CatalogItemIdCount;

		// Token: 0x04001DE9 RID: 7657
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_CatalogNamespace;
	}
}
