using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008EC RID: 2284
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeletePersistentAuthOptionsInternal : IDisposable
	{
		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06004FDA RID: 20442 RVA: 0x00097EB8 File Offset: 0x000960B8
		// (set) Token: 0x06004FDB RID: 20443 RVA: 0x00097EDA File Offset: 0x000960DA
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

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06004FDC RID: 20444 RVA: 0x00097EEC File Offset: 0x000960EC
		// (set) Token: 0x06004FDD RID: 20445 RVA: 0x00097F0E File Offset: 0x0009610E
		public string RefreshToken
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_RefreshToken, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_RefreshToken, value);
			}
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x00097F1D File Offset: 0x0009611D
		public void Dispose()
		{
		}

		// Token: 0x04001EFF RID: 7935
		private int m_ApiVersion;

		// Token: 0x04001F00 RID: 7936
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_RefreshToken;
	}
}
