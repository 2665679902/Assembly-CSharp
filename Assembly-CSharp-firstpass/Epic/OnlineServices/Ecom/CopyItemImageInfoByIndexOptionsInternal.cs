using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000839 RID: 2105
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyItemImageInfoByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06004B5D RID: 19293 RVA: 0x00093514 File Offset: 0x00091714
		// (set) Token: 0x06004B5E RID: 19294 RVA: 0x00093536 File Offset: 0x00091736
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

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06004B5F RID: 19295 RVA: 0x00093548 File Offset: 0x00091748
		// (set) Token: 0x06004B60 RID: 19296 RVA: 0x0009356A File Offset: 0x0009176A
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

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06004B61 RID: 19297 RVA: 0x0009357C File Offset: 0x0009177C
		// (set) Token: 0x06004B62 RID: 19298 RVA: 0x0009359E File Offset: 0x0009179E
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

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06004B63 RID: 19299 RVA: 0x000935B0 File Offset: 0x000917B0
		// (set) Token: 0x06004B64 RID: 19300 RVA: 0x000935D2 File Offset: 0x000917D2
		public uint ImageInfoIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_ImageInfoIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_ImageInfoIndex, value);
			}
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x000935E1 File Offset: 0x000917E1
		public void Dispose()
		{
		}

		// Token: 0x04001D23 RID: 7459
		private int m_ApiVersion;

		// Token: 0x04001D24 RID: 7460
		private IntPtr m_LocalUserId;

		// Token: 0x04001D25 RID: 7461
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ItemId;

		// Token: 0x04001D26 RID: 7462
		private uint m_ImageInfoIndex;
	}
}
