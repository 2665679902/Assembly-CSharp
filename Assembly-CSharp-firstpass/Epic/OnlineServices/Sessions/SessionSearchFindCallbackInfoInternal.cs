using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000644 RID: 1604
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchFindCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06003ED3 RID: 16083 RVA: 0x0008688C File Offset: 0x00084A8C
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06003ED4 RID: 16084 RVA: 0x000868B0 File Offset: 0x00084AB0
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06003ED5 RID: 16085 RVA: 0x000868D2 File Offset: 0x00084AD2
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x040017D6 RID: 6102
		private Result m_ResultCode;

		// Token: 0x040017D7 RID: 6103
		private IntPtr m_ClientData;
	}
}
