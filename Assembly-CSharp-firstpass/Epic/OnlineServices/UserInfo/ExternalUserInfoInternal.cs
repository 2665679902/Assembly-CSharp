using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000541 RID: 1345
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ExternalUserInfoInternal : IDisposable
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060038D8 RID: 14552 RVA: 0x00080E54 File Offset: 0x0007F054
		// (set) Token: 0x060038D9 RID: 14553 RVA: 0x00080E76 File Offset: 0x0007F076
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

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x00080E88 File Offset: 0x0007F088
		// (set) Token: 0x060038DB RID: 14555 RVA: 0x00080EAA File Offset: 0x0007F0AA
		public ExternalAccountType AccountType
		{
			get
			{
				ExternalAccountType @default = Helper.GetDefault<ExternalAccountType>();
				Helper.TryMarshalGet<ExternalAccountType>(this.m_AccountType, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ExternalAccountType>(ref this.m_AccountType, value);
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060038DC RID: 14556 RVA: 0x00080EBC File Offset: 0x0007F0BC
		// (set) Token: 0x060038DD RID: 14557 RVA: 0x00080EDE File Offset: 0x0007F0DE
		public string AccountId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_AccountId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_AccountId, value);
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x00080EF0 File Offset: 0x0007F0F0
		// (set) Token: 0x060038DF RID: 14559 RVA: 0x00080F12 File Offset: 0x0007F112
		public string DisplayName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_DisplayName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_DisplayName, value);
			}
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x00080F21 File Offset: 0x0007F121
		public void Dispose()
		{
		}

		// Token: 0x04001534 RID: 5428
		private int m_ApiVersion;

		// Token: 0x04001535 RID: 5429
		private ExternalAccountType m_AccountType;

		// Token: 0x04001536 RID: 5430
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AccountId;

		// Token: 0x04001537 RID: 5431
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_DisplayName;
	}
}
