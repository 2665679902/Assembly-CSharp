using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200082D RID: 2093
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CheckoutEntryInternal : IDisposable
	{
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06004B07 RID: 19207 RVA: 0x00092FDC File Offset: 0x000911DC
		// (set) Token: 0x06004B08 RID: 19208 RVA: 0x00092FFE File Offset: 0x000911FE
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

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06004B09 RID: 19209 RVA: 0x00093010 File Offset: 0x00091210
		// (set) Token: 0x06004B0A RID: 19210 RVA: 0x00093032 File Offset: 0x00091232
		public string OfferId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_OfferId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_OfferId, value);
			}
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x00093041 File Offset: 0x00091241
		public void Dispose()
		{
		}

		// Token: 0x04001D00 RID: 7424
		private int m_ApiVersion;

		// Token: 0x04001D01 RID: 7425
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_OfferId;
	}
}
