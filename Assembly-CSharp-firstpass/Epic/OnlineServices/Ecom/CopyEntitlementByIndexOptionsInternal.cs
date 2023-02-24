using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000833 RID: 2099
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyEntitlementByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06004B30 RID: 19248 RVA: 0x00093260 File Offset: 0x00091460
		// (set) Token: 0x06004B31 RID: 19249 RVA: 0x00093282 File Offset: 0x00091482
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

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06004B32 RID: 19250 RVA: 0x00093294 File Offset: 0x00091494
		// (set) Token: 0x06004B33 RID: 19251 RVA: 0x000932B6 File Offset: 0x000914B6
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

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06004B34 RID: 19252 RVA: 0x000932C8 File Offset: 0x000914C8
		// (set) Token: 0x06004B35 RID: 19253 RVA: 0x000932EA File Offset: 0x000914EA
		public uint EntitlementIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_EntitlementIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_EntitlementIndex, value);
			}
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x000932F9 File Offset: 0x000914F9
		public void Dispose()
		{
		}

		// Token: 0x04001D11 RID: 7441
		private int m_ApiVersion;

		// Token: 0x04001D12 RID: 7442
		private IntPtr m_LocalUserId;

		// Token: 0x04001D13 RID: 7443
		private uint m_EntitlementIndex;
	}
}
