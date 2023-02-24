using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200089C RID: 2204
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateUserOptionsInternal : IDisposable
	{
		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06004E17 RID: 19991 RVA: 0x00096654 File Offset: 0x00094854
		// (set) Token: 0x06004E18 RID: 19992 RVA: 0x00096676 File Offset: 0x00094876
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

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06004E19 RID: 19993 RVA: 0x00096688 File Offset: 0x00094888
		// (set) Token: 0x06004E1A RID: 19994 RVA: 0x000966AA File Offset: 0x000948AA
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

		// Token: 0x06004E1B RID: 19995 RVA: 0x000966B9 File Offset: 0x000948B9
		public void Dispose()
		{
		}

		// Token: 0x04001E4C RID: 7756
		private int m_ApiVersion;

		// Token: 0x04001E4D RID: 7757
		private IntPtr m_ContinuanceToken;
	}
}
