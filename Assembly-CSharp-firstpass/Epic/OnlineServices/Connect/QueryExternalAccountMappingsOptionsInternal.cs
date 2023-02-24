using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008CF RID: 2255
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryExternalAccountMappingsOptionsInternal : IDisposable
	{
		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06004F26 RID: 20262 RVA: 0x00097188 File Offset: 0x00095388
		// (set) Token: 0x06004F27 RID: 20263 RVA: 0x000971AA File Offset: 0x000953AA
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

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06004F28 RID: 20264 RVA: 0x000971BC File Offset: 0x000953BC
		// (set) Token: 0x06004F29 RID: 20265 RVA: 0x000971DE File Offset: 0x000953DE
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

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06004F2A RID: 20266 RVA: 0x000971F0 File Offset: 0x000953F0
		// (set) Token: 0x06004F2B RID: 20267 RVA: 0x00097212 File Offset: 0x00095412
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

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06004F2C RID: 20268 RVA: 0x00097224 File Offset: 0x00095424
		// (set) Token: 0x06004F2D RID: 20269 RVA: 0x0009724C File Offset: 0x0009544C
		public string[] ExternalAccountIds
		{
			get
			{
				string[] @default = Helper.GetDefault<string[]>();
				Helper.TryMarshalGet<string>(this.m_ExternalAccountIds, out @default, this.m_ExternalAccountIdCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ExternalAccountIds, value, out this.m_ExternalAccountIdCount);
			}
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x00097261 File Offset: 0x00095461
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ExternalAccountIds);
		}

		// Token: 0x04001EAA RID: 7850
		private int m_ApiVersion;

		// Token: 0x04001EAB RID: 7851
		private IntPtr m_LocalUserId;

		// Token: 0x04001EAC RID: 7852
		private ExternalAccountType m_AccountIdType;

		// Token: 0x04001EAD RID: 7853
		private IntPtr m_ExternalAccountIds;

		// Token: 0x04001EAE RID: 7854
		private uint m_ExternalAccountIdCount;
	}
}
