using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.Connect;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008E8 RID: 2280
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CredentialsInternal : IDisposable
	{
		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06004FC1 RID: 20417 RVA: 0x00097CE8 File Offset: 0x00095EE8
		// (set) Token: 0x06004FC2 RID: 20418 RVA: 0x00097D0A File Offset: 0x00095F0A
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

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06004FC3 RID: 20419 RVA: 0x00097D1C File Offset: 0x00095F1C
		// (set) Token: 0x06004FC4 RID: 20420 RVA: 0x00097D3E File Offset: 0x00095F3E
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

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06004FC5 RID: 20421 RVA: 0x00097D50 File Offset: 0x00095F50
		// (set) Token: 0x06004FC6 RID: 20422 RVA: 0x00097D72 File Offset: 0x00095F72
		public string Token
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Token, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Token, value);
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06004FC7 RID: 20423 RVA: 0x00097D84 File Offset: 0x00095F84
		// (set) Token: 0x06004FC8 RID: 20424 RVA: 0x00097DA6 File Offset: 0x00095FA6
		public LoginCredentialType Type
		{
			get
			{
				LoginCredentialType @default = Helper.GetDefault<LoginCredentialType>();
				Helper.TryMarshalGet<LoginCredentialType>(this.m_Type, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<LoginCredentialType>(ref this.m_Type, value);
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06004FC9 RID: 20425 RVA: 0x00097DB8 File Offset: 0x00095FB8
		// (set) Token: 0x06004FCA RID: 20426 RVA: 0x00097DDA File Offset: 0x00095FDA
		public IntPtr SystemAuthCredentialsOptions
		{
			get
			{
				IntPtr @default = Helper.GetDefault<IntPtr>();
				Helper.TryMarshalGet(this.m_SystemAuthCredentialsOptions, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<IntPtr>(ref this.m_SystemAuthCredentialsOptions, value);
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06004FCB RID: 20427 RVA: 0x00097DEC File Offset: 0x00095FEC
		// (set) Token: 0x06004FCC RID: 20428 RVA: 0x00097E0E File Offset: 0x0009600E
		public ExternalCredentialType ExternalType
		{
			get
			{
				ExternalCredentialType @default = Helper.GetDefault<ExternalCredentialType>();
				Helper.TryMarshalGet<ExternalCredentialType>(this.m_ExternalType, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ExternalCredentialType>(ref this.m_ExternalType, value);
			}
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x00097E1D File Offset: 0x0009601D
		public void Dispose()
		{
		}

		// Token: 0x04001EF4 RID: 7924
		private int m_ApiVersion;

		// Token: 0x04001EF5 RID: 7925
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Id;

		// Token: 0x04001EF6 RID: 7926
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Token;

		// Token: 0x04001EF7 RID: 7927
		private LoginCredentialType m_Type;

		// Token: 0x04001EF8 RID: 7928
		private IntPtr m_SystemAuthCredentialsOptions;

		// Token: 0x04001EF9 RID: 7929
		private ExternalCredentialType m_ExternalType;
	}
}
