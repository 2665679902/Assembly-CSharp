using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000851 RID: 2129
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetItemImageInfoCountOptionsInternal : IDisposable
	{
		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06004C46 RID: 19526 RVA: 0x0009481C File Offset: 0x00092A1C
		// (set) Token: 0x06004C47 RID: 19527 RVA: 0x0009483E File Offset: 0x00092A3E
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

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06004C48 RID: 19528 RVA: 0x00094850 File Offset: 0x00092A50
		// (set) Token: 0x06004C49 RID: 19529 RVA: 0x00094872 File Offset: 0x00092A72
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

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06004C4A RID: 19530 RVA: 0x00094884 File Offset: 0x00092A84
		// (set) Token: 0x06004C4B RID: 19531 RVA: 0x000948A6 File Offset: 0x00092AA6
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

		// Token: 0x06004C4C RID: 19532 RVA: 0x000948B5 File Offset: 0x00092AB5
		public void Dispose()
		{
		}

		// Token: 0x04001D97 RID: 7575
		private int m_ApiVersion;

		// Token: 0x04001D98 RID: 7576
		private IntPtr m_LocalUserId;

		// Token: 0x04001D99 RID: 7577
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ItemId;
	}
}
