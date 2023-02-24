using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200087C RID: 2172
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOwnershipTokenOptionsInternal : IDisposable
	{
		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06004D41 RID: 19777 RVA: 0x000954B0 File Offset: 0x000936B0
		// (set) Token: 0x06004D42 RID: 19778 RVA: 0x000954D2 File Offset: 0x000936D2
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

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06004D43 RID: 19779 RVA: 0x000954E4 File Offset: 0x000936E4
		// (set) Token: 0x06004D44 RID: 19780 RVA: 0x00095506 File Offset: 0x00093706
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

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06004D45 RID: 19781 RVA: 0x00095518 File Offset: 0x00093718
		// (set) Token: 0x06004D46 RID: 19782 RVA: 0x00095540 File Offset: 0x00093740
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

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06004D47 RID: 19783 RVA: 0x00095558 File Offset: 0x00093758
		// (set) Token: 0x06004D48 RID: 19784 RVA: 0x0009557A File Offset: 0x0009377A
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

		// Token: 0x06004D49 RID: 19785 RVA: 0x00095589 File Offset: 0x00093789
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_CatalogItemIds);
		}

		// Token: 0x04001DF5 RID: 7669
		private int m_ApiVersion;

		// Token: 0x04001DF6 RID: 7670
		private IntPtr m_LocalUserId;

		// Token: 0x04001DF7 RID: 7671
		private IntPtr m_CatalogItemIds;

		// Token: 0x04001DF8 RID: 7672
		private uint m_CatalogItemIdCount;

		// Token: 0x04001DF9 RID: 7673
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_CatalogNamespace;
	}
}
