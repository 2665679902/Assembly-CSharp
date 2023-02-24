using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008B3 RID: 2227
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginOptionsInternal : IDisposable
	{
		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06004EA6 RID: 20134 RVA: 0x00096F04 File Offset: 0x00095104
		// (set) Token: 0x06004EA7 RID: 20135 RVA: 0x00096F26 File Offset: 0x00095126
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

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06004EA8 RID: 20136 RVA: 0x00096F38 File Offset: 0x00095138
		// (set) Token: 0x06004EA9 RID: 20137 RVA: 0x00096F5A File Offset: 0x0009515A
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

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06004EAA RID: 20138 RVA: 0x00096F6C File Offset: 0x0009516C
		// (set) Token: 0x06004EAB RID: 20139 RVA: 0x00096F8E File Offset: 0x0009518E
		public UserLoginInfoInternal? UserLoginInfo
		{
			get
			{
				UserLoginInfoInternal? @default = Helper.GetDefault<UserLoginInfoInternal?>();
				Helper.TryMarshalGet<UserLoginInfoInternal>(this.m_UserLoginInfo, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<UserLoginInfoInternal>(ref this.m_UserLoginInfo, value);
			}
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x00096F9D File Offset: 0x0009519D
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Credentials);
			Helper.TryMarshalDispose(ref this.m_UserLoginInfo);
		}

		// Token: 0x04001E96 RID: 7830
		private int m_ApiVersion;

		// Token: 0x04001E97 RID: 7831
		private IntPtr m_Credentials;

		// Token: 0x04001E98 RID: 7832
		private IntPtr m_UserLoginInfo;
	}
}
