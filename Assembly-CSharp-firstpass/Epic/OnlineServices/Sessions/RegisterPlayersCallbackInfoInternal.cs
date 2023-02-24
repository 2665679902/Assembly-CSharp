using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000610 RID: 1552
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterPlayersCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06003D91 RID: 15761 RVA: 0x000852CC File Offset: 0x000834CC
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06003D92 RID: 15762 RVA: 0x000852F0 File Offset: 0x000834F0
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06003D93 RID: 15763 RVA: 0x00085312 File Offset: 0x00083512
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04001765 RID: 5989
		private Result m_ResultCode;

		// Token: 0x04001766 RID: 5990
		private IntPtr m_ClientData;
	}
}
