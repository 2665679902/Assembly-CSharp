using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000874 RID: 2164
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOffersOptionsInternal : IDisposable
	{
		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06004D05 RID: 19717 RVA: 0x000950E4 File Offset: 0x000932E4
		// (set) Token: 0x06004D06 RID: 19718 RVA: 0x00095106 File Offset: 0x00093306
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

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06004D07 RID: 19719 RVA: 0x00095118 File Offset: 0x00093318
		// (set) Token: 0x06004D08 RID: 19720 RVA: 0x0009513A File Offset: 0x0009333A
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

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06004D09 RID: 19721 RVA: 0x0009514C File Offset: 0x0009334C
		// (set) Token: 0x06004D0A RID: 19722 RVA: 0x0009516E File Offset: 0x0009336E
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

		// Token: 0x06004D0B RID: 19723 RVA: 0x0009517D File Offset: 0x0009337D
		public void Dispose()
		{
		}

		// Token: 0x04001DD6 RID: 7638
		private int m_ApiVersion;

		// Token: 0x04001DD7 RID: 7639
		private IntPtr m_LocalUserId;

		// Token: 0x04001DD8 RID: 7640
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_OverrideCatalogNamespace;
	}
}
