using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000837 RID: 2103
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyItemByIdOptionsInternal : IDisposable
	{
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06004B4E RID: 19278 RVA: 0x00093438 File Offset: 0x00091638
		// (set) Token: 0x06004B4F RID: 19279 RVA: 0x0009345A File Offset: 0x0009165A
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

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06004B50 RID: 19280 RVA: 0x0009346C File Offset: 0x0009166C
		// (set) Token: 0x06004B51 RID: 19281 RVA: 0x0009348E File Offset: 0x0009168E
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

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06004B52 RID: 19282 RVA: 0x000934A0 File Offset: 0x000916A0
		// (set) Token: 0x06004B53 RID: 19283 RVA: 0x000934C2 File Offset: 0x000916C2
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

		// Token: 0x06004B54 RID: 19284 RVA: 0x000934D1 File Offset: 0x000916D1
		public void Dispose()
		{
		}

		// Token: 0x04001D1D RID: 7453
		private int m_ApiVersion;

		// Token: 0x04001D1E RID: 7454
		private IntPtr m_LocalUserId;

		// Token: 0x04001D1F RID: 7455
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ItemId;
	}
}
