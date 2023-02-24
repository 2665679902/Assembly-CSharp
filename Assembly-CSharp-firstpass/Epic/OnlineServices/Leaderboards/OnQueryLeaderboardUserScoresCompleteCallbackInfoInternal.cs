using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007F5 RID: 2037
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryLeaderboardUserScoresCompleteCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06004959 RID: 18777 RVA: 0x00091568 File Offset: 0x0008F768
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x0600495A RID: 18778 RVA: 0x0009158C File Offset: 0x0008F78C
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x0600495B RID: 18779 RVA: 0x000915AE File Offset: 0x0008F7AE
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04001C48 RID: 7240
		private Result m_ResultCode;

		// Token: 0x04001C49 RID: 7241
		private IntPtr m_ClientData;
	}
}
