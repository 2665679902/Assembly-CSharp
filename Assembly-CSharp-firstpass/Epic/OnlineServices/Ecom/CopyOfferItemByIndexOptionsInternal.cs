using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000843 RID: 2115
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyOfferItemByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06004BAA RID: 19370 RVA: 0x000939D4 File Offset: 0x00091BD4
		// (set) Token: 0x06004BAB RID: 19371 RVA: 0x000939F6 File Offset: 0x00091BF6
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

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06004BAC RID: 19372 RVA: 0x00093A08 File Offset: 0x00091C08
		// (set) Token: 0x06004BAD RID: 19373 RVA: 0x00093A2A File Offset: 0x00091C2A
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

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06004BAE RID: 19374 RVA: 0x00093A3C File Offset: 0x00091C3C
		// (set) Token: 0x06004BAF RID: 19375 RVA: 0x00093A5E File Offset: 0x00091C5E
		public string OfferId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_OfferId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_OfferId, value);
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06004BB0 RID: 19376 RVA: 0x00093A70 File Offset: 0x00091C70
		// (set) Token: 0x06004BB1 RID: 19377 RVA: 0x00093A92 File Offset: 0x00091C92
		public uint ItemIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_ItemIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_ItemIndex, value);
			}
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x00093AA1 File Offset: 0x00091CA1
		public void Dispose()
		{
		}

		// Token: 0x04001D42 RID: 7490
		private int m_ApiVersion;

		// Token: 0x04001D43 RID: 7491
		private IntPtr m_LocalUserId;

		// Token: 0x04001D44 RID: 7492
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_OfferId;

		// Token: 0x04001D45 RID: 7493
		private uint m_ItemIndex;
	}
}
