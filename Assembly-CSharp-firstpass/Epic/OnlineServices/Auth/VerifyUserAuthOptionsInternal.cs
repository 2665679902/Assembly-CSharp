using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000910 RID: 2320
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VerifyUserAuthOptionsInternal : IDisposable
	{
		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x060050BF RID: 20671 RVA: 0x00098A28 File Offset: 0x00096C28
		// (set) Token: 0x060050C0 RID: 20672 RVA: 0x00098A4A File Offset: 0x00096C4A
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

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x060050C1 RID: 20673 RVA: 0x00098A5C File Offset: 0x00096C5C
		// (set) Token: 0x060050C2 RID: 20674 RVA: 0x00098A7E File Offset: 0x00096C7E
		public TokenInternal? AuthToken
		{
			get
			{
				TokenInternal? @default = Helper.GetDefault<TokenInternal?>();
				Helper.TryMarshalGet<TokenInternal>(this.m_AuthToken, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<TokenInternal>(ref this.m_AuthToken, value);
			}
		}

		// Token: 0x060050C3 RID: 20675 RVA: 0x00098A8D File Offset: 0x00096C8D
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_AuthToken);
		}

		// Token: 0x04001F61 RID: 8033
		private int m_ApiVersion;

		// Token: 0x04001F62 RID: 8034
		private IntPtr m_AuthToken;
	}
}
