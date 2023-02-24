using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007ED RID: 2029
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryLeaderboardDefinitionsCompleteCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06004939 RID: 18745 RVA: 0x00091478 File Offset: 0x0008F678
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x0600493A RID: 18746 RVA: 0x0009149C File Offset: 0x0008F69C
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600493B RID: 18747 RVA: 0x000914BE File Offset: 0x0008F6BE
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04001C40 RID: 7232
		private Result m_ResultCode;

		// Token: 0x04001C41 RID: 7233
		private IntPtr m_ClientData;
	}
}
