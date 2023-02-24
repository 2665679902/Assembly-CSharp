using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200090C RID: 2316
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct TokenInternal : IDisposable
	{
		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x0600509C RID: 20636 RVA: 0x00098754 File Offset: 0x00096954
		// (set) Token: 0x0600509D RID: 20637 RVA: 0x00098776 File Offset: 0x00096976
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

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x0600509E RID: 20638 RVA: 0x00098788 File Offset: 0x00096988
		// (set) Token: 0x0600509F RID: 20639 RVA: 0x000987AA File Offset: 0x000969AA
		public string App
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_App, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_App, value);
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x060050A0 RID: 20640 RVA: 0x000987BC File Offset: 0x000969BC
		// (set) Token: 0x060050A1 RID: 20641 RVA: 0x000987DE File Offset: 0x000969DE
		public string ClientId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_ClientId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ClientId, value);
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x060050A2 RID: 20642 RVA: 0x000987F0 File Offset: 0x000969F0
		// (set) Token: 0x060050A3 RID: 20643 RVA: 0x00098812 File Offset: 0x00096A12
		public EpicAccountId AccountId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_AccountId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_AccountId, value);
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x060050A4 RID: 20644 RVA: 0x00098824 File Offset: 0x00096A24
		// (set) Token: 0x060050A5 RID: 20645 RVA: 0x00098846 File Offset: 0x00096A46
		public string AccessToken
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_AccessToken, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_AccessToken, value);
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x060050A6 RID: 20646 RVA: 0x00098858 File Offset: 0x00096A58
		// (set) Token: 0x060050A7 RID: 20647 RVA: 0x0009887A File Offset: 0x00096A7A
		public double ExpiresIn
		{
			get
			{
				double @default = Helper.GetDefault<double>();
				Helper.TryMarshalGet<double>(this.m_ExpiresIn, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<double>(ref this.m_ExpiresIn, value);
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x060050A8 RID: 20648 RVA: 0x0009888C File Offset: 0x00096A8C
		// (set) Token: 0x060050A9 RID: 20649 RVA: 0x000988AE File Offset: 0x00096AAE
		public string ExpiresAt
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_ExpiresAt, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ExpiresAt, value);
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x060050AA RID: 20650 RVA: 0x000988C0 File Offset: 0x00096AC0
		// (set) Token: 0x060050AB RID: 20651 RVA: 0x000988E2 File Offset: 0x00096AE2
		public AuthTokenType AuthType
		{
			get
			{
				AuthTokenType @default = Helper.GetDefault<AuthTokenType>();
				Helper.TryMarshalGet<AuthTokenType>(this.m_AuthType, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<AuthTokenType>(ref this.m_AuthType, value);
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x060050AC RID: 20652 RVA: 0x000988F4 File Offset: 0x00096AF4
		// (set) Token: 0x060050AD RID: 20653 RVA: 0x00098916 File Offset: 0x00096B16
		public string RefreshToken
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_RefreshToken, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_RefreshToken, value);
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x060050AE RID: 20654 RVA: 0x00098928 File Offset: 0x00096B28
		// (set) Token: 0x060050AF RID: 20655 RVA: 0x0009894A File Offset: 0x00096B4A
		public double RefreshExpiresIn
		{
			get
			{
				double @default = Helper.GetDefault<double>();
				Helper.TryMarshalGet<double>(this.m_RefreshExpiresIn, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<double>(ref this.m_RefreshExpiresIn, value);
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x060050B0 RID: 20656 RVA: 0x0009895C File Offset: 0x00096B5C
		// (set) Token: 0x060050B1 RID: 20657 RVA: 0x0009897E File Offset: 0x00096B7E
		public string RefreshExpiresAt
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_RefreshExpiresAt, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_RefreshExpiresAt, value);
			}
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x0009898D File Offset: 0x00096B8D
		public void Dispose()
		{
		}

		// Token: 0x04001F51 RID: 8017
		private int m_ApiVersion;

		// Token: 0x04001F52 RID: 8018
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_App;

		// Token: 0x04001F53 RID: 8019
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ClientId;

		// Token: 0x04001F54 RID: 8020
		private IntPtr m_AccountId;

		// Token: 0x04001F55 RID: 8021
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AccessToken;

		// Token: 0x04001F56 RID: 8022
		private double m_ExpiresIn;

		// Token: 0x04001F57 RID: 8023
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ExpiresAt;

		// Token: 0x04001F58 RID: 8024
		private AuthTokenType m_AuthType;

		// Token: 0x04001F59 RID: 8025
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_RefreshToken;

		// Token: 0x04001F5A RID: 8026
		private double m_RefreshExpiresIn;

		// Token: 0x04001F5B RID: 8027
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_RefreshExpiresAt;
	}
}
