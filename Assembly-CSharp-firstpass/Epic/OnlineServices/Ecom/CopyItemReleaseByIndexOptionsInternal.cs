using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200083B RID: 2107
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyItemReleaseByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06004B6E RID: 19310 RVA: 0x00093624 File Offset: 0x00091824
		// (set) Token: 0x06004B6F RID: 19311 RVA: 0x00093646 File Offset: 0x00091846
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

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06004B70 RID: 19312 RVA: 0x00093658 File Offset: 0x00091858
		// (set) Token: 0x06004B71 RID: 19313 RVA: 0x0009367A File Offset: 0x0009187A
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

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06004B72 RID: 19314 RVA: 0x0009368C File Offset: 0x0009188C
		// (set) Token: 0x06004B73 RID: 19315 RVA: 0x000936AE File Offset: 0x000918AE
		public string ItemId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_ItemId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ItemId, value);
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06004B74 RID: 19316 RVA: 0x000936C0 File Offset: 0x000918C0
		// (set) Token: 0x06004B75 RID: 19317 RVA: 0x000936E2 File Offset: 0x000918E2
		public uint ReleaseIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_ReleaseIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_ReleaseIndex, value);
			}
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x000936F1 File Offset: 0x000918F1
		public void Dispose()
		{
		}

		// Token: 0x04001D2A RID: 7466
		private int m_ApiVersion;

		// Token: 0x04001D2B RID: 7467
		private IntPtr m_LocalUserId;

		// Token: 0x04001D2C RID: 7468
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ItemId;

		// Token: 0x04001D2D RID: 7469
		private uint m_ReleaseIndex;
	}
}
