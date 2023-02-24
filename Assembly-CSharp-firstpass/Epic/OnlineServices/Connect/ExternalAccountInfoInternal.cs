using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008A4 RID: 2212
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ExternalAccountInfoInternal : IDisposable
	{
		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06004E42 RID: 20034 RVA: 0x000968A0 File Offset: 0x00094AA0
		// (set) Token: 0x06004E43 RID: 20035 RVA: 0x000968C2 File Offset: 0x00094AC2
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

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06004E44 RID: 20036 RVA: 0x000968D4 File Offset: 0x00094AD4
		// (set) Token: 0x06004E45 RID: 20037 RVA: 0x000968F6 File Offset: 0x00094AF6
		public ProductUserId ProductUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_ProductUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ProductUserId, value);
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06004E46 RID: 20038 RVA: 0x00096908 File Offset: 0x00094B08
		// (set) Token: 0x06004E47 RID: 20039 RVA: 0x0009692A File Offset: 0x00094B2A
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

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06004E48 RID: 20040 RVA: 0x0009693C File Offset: 0x00094B3C
		// (set) Token: 0x06004E49 RID: 20041 RVA: 0x0009695E File Offset: 0x00094B5E
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

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06004E4A RID: 20042 RVA: 0x00096970 File Offset: 0x00094B70
		// (set) Token: 0x06004E4B RID: 20043 RVA: 0x00096992 File Offset: 0x00094B92
		public ExternalAccountType AccountIdType
		{
			get
			{
				ExternalAccountType @default = Helper.GetDefault<ExternalAccountType>();
				Helper.TryMarshalGet<ExternalAccountType>(this.m_AccountIdType, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ExternalAccountType>(ref this.m_AccountIdType, value);
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06004E4C RID: 20044 RVA: 0x000969A4 File Offset: 0x00094BA4
		// (set) Token: 0x06004E4D RID: 20045 RVA: 0x000969C6 File Offset: 0x00094BC6
		public DateTimeOffset? LastLoginTime
		{
			get
			{
				DateTimeOffset? @default = Helper.GetDefault<DateTimeOffset?>();
				Helper.TryMarshalGet(this.m_LastLoginTime, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LastLoginTime, value);
			}
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x000969D5 File Offset: 0x00094BD5
		public void Dispose()
		{
		}

		// Token: 0x04001E5D RID: 7773
		private int m_ApiVersion;

		// Token: 0x04001E5E RID: 7774
		private IntPtr m_ProductUserId;

		// Token: 0x04001E5F RID: 7775
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_DisplayName;

		// Token: 0x04001E60 RID: 7776
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AccountId;

		// Token: 0x04001E61 RID: 7777
		private ExternalAccountType m_AccountIdType;

		// Token: 0x04001E62 RID: 7778
		private long m_LastLoginTime;
	}
}
