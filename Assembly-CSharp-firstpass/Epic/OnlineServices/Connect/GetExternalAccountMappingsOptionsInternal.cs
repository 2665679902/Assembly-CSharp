using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008A7 RID: 2215
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetExternalAccountMappingsOptionsInternal : IDisposable
	{
		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06004E57 RID: 20055 RVA: 0x00096A18 File Offset: 0x00094C18
		// (set) Token: 0x06004E58 RID: 20056 RVA: 0x00096A3A File Offset: 0x00094C3A
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

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06004E59 RID: 20057 RVA: 0x00096A4C File Offset: 0x00094C4C
		// (set) Token: 0x06004E5A RID: 20058 RVA: 0x00096A6E File Offset: 0x00094C6E
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06004E5B RID: 20059 RVA: 0x00096A80 File Offset: 0x00094C80
		// (set) Token: 0x06004E5C RID: 20060 RVA: 0x00096AA2 File Offset: 0x00094CA2
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

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06004E5D RID: 20061 RVA: 0x00096AB4 File Offset: 0x00094CB4
		// (set) Token: 0x06004E5E RID: 20062 RVA: 0x00096AD6 File Offset: 0x00094CD6
		public string TargetExternalUserId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_TargetExternalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_TargetExternalUserId, value);
			}
		}

		// Token: 0x06004E5F RID: 20063 RVA: 0x00096AE5 File Offset: 0x00094CE5
		public void Dispose()
		{
		}

		// Token: 0x04001E73 RID: 7795
		private int m_ApiVersion;

		// Token: 0x04001E74 RID: 7796
		private IntPtr m_LocalUserId;

		// Token: 0x04001E75 RID: 7797
		private ExternalAccountType m_AccountIdType;

		// Token: 0x04001E76 RID: 7798
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_TargetExternalUserId;
	}
}
