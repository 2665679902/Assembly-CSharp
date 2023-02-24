using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000880 RID: 2176
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RedeemEntitlementsOptionsInternal : IDisposable
	{
		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06004D5B RID: 19803 RVA: 0x00095674 File Offset: 0x00093874
		// (set) Token: 0x06004D5C RID: 19804 RVA: 0x00095696 File Offset: 0x00093896
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

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06004D5D RID: 19805 RVA: 0x000956A8 File Offset: 0x000938A8
		// (set) Token: 0x06004D5E RID: 19806 RVA: 0x000956CA File Offset: 0x000938CA
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

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06004D5F RID: 19807 RVA: 0x000956DC File Offset: 0x000938DC
		// (set) Token: 0x06004D60 RID: 19808 RVA: 0x00095704 File Offset: 0x00093904
		public string[] EntitlementIds
		{
			get
			{
				string[] @default = Helper.GetDefault<string[]>();
				Helper.TryMarshalGet<string>(this.m_EntitlementIds, out @default, this.m_EntitlementIdCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_EntitlementIds, value, out this.m_EntitlementIdCount);
			}
		}

		// Token: 0x06004D61 RID: 19809 RVA: 0x00095719 File Offset: 0x00093919
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_EntitlementIds);
		}

		// Token: 0x04001E02 RID: 7682
		private int m_ApiVersion;

		// Token: 0x04001E03 RID: 7683
		private IntPtr m_LocalUserId;

		// Token: 0x04001E04 RID: 7684
		private uint m_EntitlementIdCount;

		// Token: 0x04001E05 RID: 7685
		private IntPtr m_EntitlementIds;
	}
}
