using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200084F RID: 2127
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetEntitlementsCountOptionsInternal : IDisposable
	{
		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06004C3B RID: 19515 RVA: 0x00094788 File Offset: 0x00092988
		// (set) Token: 0x06004C3C RID: 19516 RVA: 0x000947AA File Offset: 0x000929AA
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

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06004C3D RID: 19517 RVA: 0x000947BC File Offset: 0x000929BC
		// (set) Token: 0x06004C3E RID: 19518 RVA: 0x000947DE File Offset: 0x000929DE
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

		// Token: 0x06004C3F RID: 19519 RVA: 0x000947ED File Offset: 0x000929ED
		public void Dispose()
		{
		}

		// Token: 0x04001D93 RID: 7571
		private int m_ApiVersion;

		// Token: 0x04001D94 RID: 7572
		private IntPtr m_LocalUserId;
	}
}
