using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008F6 RID: 2294
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginOptionsInternal : IDisposable
	{
		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06005018 RID: 20504 RVA: 0x0009828C File Offset: 0x0009648C
		// (set) Token: 0x06005019 RID: 20505 RVA: 0x000982AE File Offset: 0x000964AE
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

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x0600501A RID: 20506 RVA: 0x000982C0 File Offset: 0x000964C0
		// (set) Token: 0x0600501B RID: 20507 RVA: 0x000982E2 File Offset: 0x000964E2
		public CredentialsInternal? Credentials
		{
			get
			{
				CredentialsInternal? @default = Helper.GetDefault<CredentialsInternal?>();
				Helper.TryMarshalGet<CredentialsInternal>(this.m_Credentials, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<CredentialsInternal>(ref this.m_Credentials, value);
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x0600501C RID: 20508 RVA: 0x000982F4 File Offset: 0x000964F4
		// (set) Token: 0x0600501D RID: 20509 RVA: 0x00098316 File Offset: 0x00096516
		public AuthScopeFlags ScopeFlags
		{
			get
			{
				AuthScopeFlags @default = Helper.GetDefault<AuthScopeFlags>();
				Helper.TryMarshalGet<AuthScopeFlags>(this.m_ScopeFlags, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<AuthScopeFlags>(ref this.m_ScopeFlags, value);
			}
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x00098325 File Offset: 0x00096525
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Credentials);
		}

		// Token: 0x04001F2A RID: 7978
		private int m_ApiVersion;

		// Token: 0x04001F2B RID: 7979
		private IntPtr m_Credentials;

		// Token: 0x04001F2C RID: 7980
		private AuthScopeFlags m_ScopeFlags;
	}
}
