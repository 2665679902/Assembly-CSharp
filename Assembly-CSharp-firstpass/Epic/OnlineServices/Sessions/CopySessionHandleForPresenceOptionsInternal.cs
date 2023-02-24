using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005D4 RID: 1492
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopySessionHandleForPresenceOptionsInternal : IDisposable
	{
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06003C6F RID: 15471 RVA: 0x00084790 File Offset: 0x00082990
		// (set) Token: 0x06003C70 RID: 15472 RVA: 0x000847B2 File Offset: 0x000829B2
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

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06003C71 RID: 15473 RVA: 0x000847C4 File Offset: 0x000829C4
		// (set) Token: 0x06003C72 RID: 15474 RVA: 0x000847E6 File Offset: 0x000829E6
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

		// Token: 0x06003C73 RID: 15475 RVA: 0x000847F5 File Offset: 0x000829F5
		public void Dispose()
		{
		}

		// Token: 0x0400170A RID: 5898
		private int m_ApiVersion;

		// Token: 0x0400170B RID: 5899
		private IntPtr m_LocalUserId;
	}
}
