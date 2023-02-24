using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000764 RID: 1892
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsGetMemberAttributeCountOptionsInternal : IDisposable
	{
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06004605 RID: 17925 RVA: 0x0008E238 File Offset: 0x0008C438
		// (set) Token: 0x06004606 RID: 17926 RVA: 0x0008E25A File Offset: 0x0008C45A
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

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06004607 RID: 17927 RVA: 0x0008E26C File Offset: 0x0008C46C
		// (set) Token: 0x06004608 RID: 17928 RVA: 0x0008E28E File Offset: 0x0008C48E
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x0008E29D File Offset: 0x0008C49D
		public void Dispose()
		{
		}

		// Token: 0x04001B0A RID: 6922
		private int m_ApiVersion;

		// Token: 0x04001B0B RID: 6923
		private IntPtr m_TargetUserId;
	}
}
