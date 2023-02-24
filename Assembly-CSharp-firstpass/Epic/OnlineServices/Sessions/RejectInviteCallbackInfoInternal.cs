using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000614 RID: 1556
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06003DA6 RID: 15782 RVA: 0x00085428 File Offset: 0x00083628
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06003DA7 RID: 15783 RVA: 0x0008544C File Offset: 0x0008364C
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06003DA8 RID: 15784 RVA: 0x0008546E File Offset: 0x0008366E
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x0400176F RID: 5999
		private Result m_ResultCode;

		// Token: 0x04001770 RID: 6000
		private IntPtr m_ClientData;
	}
}
