using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007C3 RID: 1987
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06004832 RID: 18482 RVA: 0x000902E4 File Offset: 0x0008E4E4
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06004833 RID: 18483 RVA: 0x00090308 File Offset: 0x0008E508
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06004834 RID: 18484 RVA: 0x0009032A File Offset: 0x0008E52A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06004835 RID: 18485 RVA: 0x00090334 File Offset: 0x0008E534
		public string InviteId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_InviteId, out @default);
				return @default;
			}
		}

		// Token: 0x04001BD1 RID: 7121
		private Result m_ResultCode;

		// Token: 0x04001BD2 RID: 7122
		private IntPtr m_ClientData;

		// Token: 0x04001BD3 RID: 7123
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_InviteId;
	}
}
