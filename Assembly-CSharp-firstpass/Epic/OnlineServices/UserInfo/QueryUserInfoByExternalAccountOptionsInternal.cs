using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000551 RID: 1361
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoByExternalAccountOptionsInternal : IDisposable
	{
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06003940 RID: 14656 RVA: 0x00081358 File Offset: 0x0007F558
		// (set) Token: 0x06003941 RID: 14657 RVA: 0x0008137A File Offset: 0x0007F57A
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

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06003942 RID: 14658 RVA: 0x0008138C File Offset: 0x0007F58C
		// (set) Token: 0x06003943 RID: 14659 RVA: 0x000813AE File Offset: 0x0007F5AE
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

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06003944 RID: 14660 RVA: 0x000813C0 File Offset: 0x0007F5C0
		// (set) Token: 0x06003945 RID: 14661 RVA: 0x000813E2 File Offset: 0x0007F5E2
		public string ExternalAccountId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_ExternalAccountId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ExternalAccountId, value);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06003946 RID: 14662 RVA: 0x000813F4 File Offset: 0x0007F5F4
		// (set) Token: 0x06003947 RID: 14663 RVA: 0x00081416 File Offset: 0x0007F616
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

		// Token: 0x06003948 RID: 14664 RVA: 0x00081425 File Offset: 0x0007F625
		public void Dispose()
		{
		}

		// Token: 0x0400155B RID: 5467
		private int m_ApiVersion;

		// Token: 0x0400155C RID: 5468
		private IntPtr m_LocalUserId;

		// Token: 0x0400155D RID: 5469
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ExternalAccountId;

		// Token: 0x0400155E RID: 5470
		private ExternalAccountType m_AccountType;
	}
}
