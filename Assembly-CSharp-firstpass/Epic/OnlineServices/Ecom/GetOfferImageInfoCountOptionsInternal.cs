using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000857 RID: 2135
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetOfferImageInfoCountOptionsInternal : IDisposable
	{
		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06004C69 RID: 19561 RVA: 0x00094A30 File Offset: 0x00092C30
		// (set) Token: 0x06004C6A RID: 19562 RVA: 0x00094A52 File Offset: 0x00092C52
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

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06004C6B RID: 19563 RVA: 0x00094A64 File Offset: 0x00092C64
		// (set) Token: 0x06004C6C RID: 19564 RVA: 0x00094A86 File Offset: 0x00092C86
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

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06004C6D RID: 19565 RVA: 0x00094A98 File Offset: 0x00092C98
		// (set) Token: 0x06004C6E RID: 19566 RVA: 0x00094ABA File Offset: 0x00092CBA
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

		// Token: 0x06004C6F RID: 19567 RVA: 0x00094AC9 File Offset: 0x00092CC9
		public void Dispose()
		{
		}

		// Token: 0x04001DA4 RID: 7588
		private int m_ApiVersion;

		// Token: 0x04001DA5 RID: 7589
		private IntPtr m_LocalUserId;

		// Token: 0x04001DA6 RID: 7590
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_OfferId;
	}
}
