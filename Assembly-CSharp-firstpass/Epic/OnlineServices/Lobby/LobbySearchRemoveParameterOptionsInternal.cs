using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200078F RID: 1935
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchRemoveParameterOptionsInternal : IDisposable
	{
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06004744 RID: 18244 RVA: 0x0008FC40 File Offset: 0x0008DE40
		// (set) Token: 0x06004745 RID: 18245 RVA: 0x0008FC62 File Offset: 0x0008DE62
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

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06004746 RID: 18246 RVA: 0x0008FC74 File Offset: 0x0008DE74
		// (set) Token: 0x06004747 RID: 18247 RVA: 0x0008FC96 File Offset: 0x0008DE96
		public string Key
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Key, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Key, value);
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06004748 RID: 18248 RVA: 0x0008FCA8 File Offset: 0x0008DEA8
		// (set) Token: 0x06004749 RID: 18249 RVA: 0x0008FCCA File Offset: 0x0008DECA
		public ComparisonOp ComparisonOp
		{
			get
			{
				ComparisonOp @default = Helper.GetDefault<ComparisonOp>();
				Helper.TryMarshalGet<ComparisonOp>(this.m_ComparisonOp, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ComparisonOp>(ref this.m_ComparisonOp, value);
			}
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x0008FCD9 File Offset: 0x0008DED9
		public void Dispose()
		{
		}

		// Token: 0x04001BA3 RID: 7075
		private int m_ApiVersion;

		// Token: 0x04001BA4 RID: 7076
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Key;

		// Token: 0x04001BA5 RID: 7077
		private ComparisonOp m_ComparisonOp;
	}
}
