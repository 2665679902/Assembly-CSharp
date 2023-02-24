using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000557 RID: 1367
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserInfoDataInternal : IDisposable
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06003970 RID: 14704 RVA: 0x00081634 File Offset: 0x0007F834
		// (set) Token: 0x06003971 RID: 14705 RVA: 0x00081656 File Offset: 0x0007F856
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

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06003972 RID: 14706 RVA: 0x00081668 File Offset: 0x0007F868
		// (set) Token: 0x06003973 RID: 14707 RVA: 0x0008168A File Offset: 0x0007F88A
		public EpicAccountId UserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_UserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06003974 RID: 14708 RVA: 0x0008169C File Offset: 0x0007F89C
		// (set) Token: 0x06003975 RID: 14709 RVA: 0x000816BE File Offset: 0x0007F8BE
		public string Country
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Country, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Country, value);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06003976 RID: 14710 RVA: 0x000816D0 File Offset: 0x0007F8D0
		// (set) Token: 0x06003977 RID: 14711 RVA: 0x000816F2 File Offset: 0x0007F8F2
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

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06003978 RID: 14712 RVA: 0x00081704 File Offset: 0x0007F904
		// (set) Token: 0x06003979 RID: 14713 RVA: 0x00081726 File Offset: 0x0007F926
		public string PreferredLanguage
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_PreferredLanguage, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_PreferredLanguage, value);
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600397A RID: 14714 RVA: 0x00081738 File Offset: 0x0007F938
		// (set) Token: 0x0600397B RID: 14715 RVA: 0x0008175A File Offset: 0x0007F95A
		public string Nickname
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Nickname, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Nickname, value);
			}
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x00081769 File Offset: 0x0007F969
		public void Dispose()
		{
		}

		// Token: 0x04001571 RID: 5489
		private int m_ApiVersion;

		// Token: 0x04001572 RID: 5490
		private IntPtr m_UserId;

		// Token: 0x04001573 RID: 5491
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Country;

		// Token: 0x04001574 RID: 5492
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_DisplayName;

		// Token: 0x04001575 RID: 5493
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_PreferredLanguage;

		// Token: 0x04001576 RID: 5494
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Nickname;
	}
}
