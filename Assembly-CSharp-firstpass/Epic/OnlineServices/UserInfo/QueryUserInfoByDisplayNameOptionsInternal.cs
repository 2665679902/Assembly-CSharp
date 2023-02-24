using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200054D RID: 1357
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoByDisplayNameOptionsInternal : IDisposable
	{
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600391D RID: 14621 RVA: 0x00081130 File Offset: 0x0007F330
		// (set) Token: 0x0600391E RID: 14622 RVA: 0x00081152 File Offset: 0x0007F352
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

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600391F RID: 14623 RVA: 0x00081164 File Offset: 0x0007F364
		// (set) Token: 0x06003920 RID: 14624 RVA: 0x00081186 File Offset: 0x0007F386
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

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06003921 RID: 14625 RVA: 0x00081198 File Offset: 0x0007F398
		// (set) Token: 0x06003922 RID: 14626 RVA: 0x000811BA File Offset: 0x0007F3BA
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

		// Token: 0x06003923 RID: 14627 RVA: 0x000811C9 File Offset: 0x0007F3C9
		public void Dispose()
		{
		}

		// Token: 0x04001549 RID: 5449
		private int m_ApiVersion;

		// Token: 0x0400154A RID: 5450
		private IntPtr m_LocalUserId;

		// Token: 0x0400154B RID: 5451
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_DisplayName;
	}
}
