using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200083F RID: 2111
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyOfferByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06004B8A RID: 19338 RVA: 0x000937E8 File Offset: 0x000919E8
		// (set) Token: 0x06004B8B RID: 19339 RVA: 0x0009380A File Offset: 0x00091A0A
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

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06004B8C RID: 19340 RVA: 0x0009381C File Offset: 0x00091A1C
		// (set) Token: 0x06004B8D RID: 19341 RVA: 0x0009383E File Offset: 0x00091A3E
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

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06004B8E RID: 19342 RVA: 0x00093850 File Offset: 0x00091A50
		// (set) Token: 0x06004B8F RID: 19343 RVA: 0x00093872 File Offset: 0x00091A72
		public uint OfferIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_OfferIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_OfferIndex, value);
			}
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x00093881 File Offset: 0x00091A81
		public void Dispose()
		{
		}

		// Token: 0x04001D35 RID: 7477
		private int m_ApiVersion;

		// Token: 0x04001D36 RID: 7478
		private IntPtr m_LocalUserId;

		// Token: 0x04001D37 RID: 7479
		private uint m_OfferIndex;
	}
}
