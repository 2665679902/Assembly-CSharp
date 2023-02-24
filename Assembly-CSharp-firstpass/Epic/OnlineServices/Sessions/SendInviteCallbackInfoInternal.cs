using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000618 RID: 1560
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06003DBB RID: 15803 RVA: 0x0008556C File Offset: 0x0008376C
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06003DBC RID: 15804 RVA: 0x00085590 File Offset: 0x00083790
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06003DBD RID: 15805 RVA: 0x000855B2 File Offset: 0x000837B2
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04001778 RID: 6008
		private Result m_ResultCode;

		// Token: 0x04001779 RID: 6009
		private IntPtr m_ClientData;
	}
}
