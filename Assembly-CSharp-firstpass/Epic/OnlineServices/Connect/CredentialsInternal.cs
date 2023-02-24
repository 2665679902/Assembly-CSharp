using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200089E RID: 2206
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CredentialsInternal : IDisposable
	{
		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06004E22 RID: 20002 RVA: 0x000966E8 File Offset: 0x000948E8
		// (set) Token: 0x06004E23 RID: 20003 RVA: 0x0009670A File Offset: 0x0009490A
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

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06004E24 RID: 20004 RVA: 0x0009671C File Offset: 0x0009491C
		// (set) Token: 0x06004E25 RID: 20005 RVA: 0x0009673E File Offset: 0x0009493E
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

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06004E26 RID: 20006 RVA: 0x00096750 File Offset: 0x00094950
		// (set) Token: 0x06004E27 RID: 20007 RVA: 0x00096772 File Offset: 0x00094972
		public ExternalCredentialType Type
		{
			get
			{
				ExternalCredentialType @default = Helper.GetDefault<ExternalCredentialType>();
				Helper.TryMarshalGet<ExternalCredentialType>(this.m_Type, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ExternalCredentialType>(ref this.m_Type, value);
			}
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x00096781 File Offset: 0x00094981
		public void Dispose()
		{
		}

		// Token: 0x04001E50 RID: 7760
		private int m_ApiVersion;

		// Token: 0x04001E51 RID: 7761
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Token;

		// Token: 0x04001E52 RID: 7762
		private ExternalCredentialType m_Type;
	}
}
