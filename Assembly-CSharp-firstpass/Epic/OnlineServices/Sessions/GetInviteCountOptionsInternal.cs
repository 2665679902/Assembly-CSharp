using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005E4 RID: 1508
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetInviteCountOptionsInternal : IDisposable
	{
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x00084CF8 File Offset: 0x00082EF8
		// (set) Token: 0x06003CCA RID: 15562 RVA: 0x00084D1A File Offset: 0x00082F1A
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

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x00084D2C File Offset: 0x00082F2C
		// (set) Token: 0x06003CCC RID: 15564 RVA: 0x00084D4E File Offset: 0x00082F4E
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x00084D5D File Offset: 0x00082F5D
		public void Dispose()
		{
		}

		// Token: 0x0400172E RID: 5934
		private int m_ApiVersion;

		// Token: 0x0400172F RID: 5935
		private IntPtr m_LocalUserId;
	}
}
