using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000853 RID: 2131
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetItemReleaseCountOptionsInternal : IDisposable
	{
		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06004C53 RID: 19539 RVA: 0x000948E4 File Offset: 0x00092AE4
		// (set) Token: 0x06004C54 RID: 19540 RVA: 0x00094906 File Offset: 0x00092B06
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

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06004C55 RID: 19541 RVA: 0x00094918 File Offset: 0x00092B18
		// (set) Token: 0x06004C56 RID: 19542 RVA: 0x0009493A File Offset: 0x00092B3A
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

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06004C57 RID: 19543 RVA: 0x0009494C File Offset: 0x00092B4C
		// (set) Token: 0x06004C58 RID: 19544 RVA: 0x0009496E File Offset: 0x00092B6E
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

		// Token: 0x06004C59 RID: 19545 RVA: 0x0009497D File Offset: 0x00092B7D
		public void Dispose()
		{
		}

		// Token: 0x04001D9C RID: 7580
		private int m_ApiVersion;

		// Token: 0x04001D9D RID: 7581
		private IntPtr m_LocalUserId;

		// Token: 0x04001D9E RID: 7582
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ItemId;
	}
}
