using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200085D RID: 2141
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ItemOwnershipInternal : IDisposable
	{
		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06004C8C RID: 19596 RVA: 0x00094C44 File Offset: 0x00092E44
		// (set) Token: 0x06004C8D RID: 19597 RVA: 0x00094C66 File Offset: 0x00092E66
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

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06004C8E RID: 19598 RVA: 0x00094C78 File Offset: 0x00092E78
		// (set) Token: 0x06004C8F RID: 19599 RVA: 0x00094C9A File Offset: 0x00092E9A
		public string Id
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Id, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Id, value);
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06004C90 RID: 19600 RVA: 0x00094CAC File Offset: 0x00092EAC
		// (set) Token: 0x06004C91 RID: 19601 RVA: 0x00094CCE File Offset: 0x00092ECE
		public OwnershipStatus OwnershipStatus
		{
			get
			{
				OwnershipStatus @default = Helper.GetDefault<OwnershipStatus>();
				Helper.TryMarshalGet<OwnershipStatus>(this.m_OwnershipStatus, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<OwnershipStatus>(ref this.m_OwnershipStatus, value);
			}
		}

		// Token: 0x06004C92 RID: 19602 RVA: 0x00094CDD File Offset: 0x00092EDD
		public void Dispose()
		{
		}

		// Token: 0x04001DB1 RID: 7601
		private int m_ApiVersion;

		// Token: 0x04001DB2 RID: 7602
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Id;

		// Token: 0x04001DB3 RID: 7603
		private OwnershipStatus m_OwnershipStatus;
	}
}
