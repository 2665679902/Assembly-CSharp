using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007F1 RID: 2033
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryLeaderboardRanksCompleteCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06004949 RID: 18761 RVA: 0x000914F0 File Offset: 0x0008F6F0
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x0600494A RID: 18762 RVA: 0x00091514 File Offset: 0x0008F714
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x0600494B RID: 18763 RVA: 0x00091536 File Offset: 0x0008F736
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04001C44 RID: 7236
		private Result m_ResultCode;

		// Token: 0x04001C45 RID: 7237
		private IntPtr m_ClientData;
	}
}
