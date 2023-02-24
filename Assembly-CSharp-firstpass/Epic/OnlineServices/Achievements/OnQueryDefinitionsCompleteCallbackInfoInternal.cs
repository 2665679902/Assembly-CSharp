using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200093B RID: 2363
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryDefinitionsCompleteCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x060051FD RID: 20989 RVA: 0x00099FA8 File Offset: 0x000981A8
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x060051FE RID: 20990 RVA: 0x00099FCC File Offset: 0x000981CC
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x060051FF RID: 20991 RVA: 0x00099FEE File Offset: 0x000981EE
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04001FE5 RID: 8165
		private Result m_ResultCode;

		// Token: 0x04001FE6 RID: 8166
		private IntPtr m_ClientData;
	}
}
