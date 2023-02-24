using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008AF RID: 2223
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LinkAccountOptionsInternal : IDisposable
	{
		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06004E8B RID: 20107 RVA: 0x00096D58 File Offset: 0x00094F58
		// (set) Token: 0x06004E8C RID: 20108 RVA: 0x00096D7A File Offset: 0x00094F7A
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

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06004E8D RID: 20109 RVA: 0x00096D8C File Offset: 0x00094F8C
		// (set) Token: 0x06004E8E RID: 20110 RVA: 0x00096DAE File Offset: 0x00094FAE
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

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06004E8F RID: 20111 RVA: 0x00096DC0 File Offset: 0x00094FC0
		// (set) Token: 0x06004E90 RID: 20112 RVA: 0x00096DE2 File Offset: 0x00094FE2
		public ContinuanceToken ContinuanceToken
		{
			get
			{
				ContinuanceToken @default = Helper.GetDefault<ContinuanceToken>();
				Helper.TryMarshalGet<ContinuanceToken>(this.m_ContinuanceToken, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ContinuanceToken, value);
			}
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x00096DF1 File Offset: 0x00094FF1
		public void Dispose()
		{
		}

		// Token: 0x04001E89 RID: 7817
		private int m_ApiVersion;

		// Token: 0x04001E8A RID: 7818
		private IntPtr m_LocalUserId;

		// Token: 0x04001E8B RID: 7819
		private IntPtr m_ContinuanceToken;
	}
}
