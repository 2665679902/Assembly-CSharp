using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008DF RID: 2271
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AccountFeatureRestrictedInfoInternal : IDisposable
	{
		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06004F88 RID: 20360 RVA: 0x000977D4 File Offset: 0x000959D4
		// (set) Token: 0x06004F89 RID: 20361 RVA: 0x000977F6 File Offset: 0x000959F6
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

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06004F8A RID: 20362 RVA: 0x00097808 File Offset: 0x00095A08
		// (set) Token: 0x06004F8B RID: 20363 RVA: 0x0009782A File Offset: 0x00095A2A
		public string VerificationURI
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_VerificationURI, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_VerificationURI, value);
			}
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x00097839 File Offset: 0x00095A39
		public void Dispose()
		{
		}

		// Token: 0x04001ED7 RID: 7895
		private int m_ApiVersion;

		// Token: 0x04001ED8 RID: 7896
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_VerificationURI;
	}
}
