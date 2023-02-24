using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008D5 RID: 2261
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct TransferDeviceIdAccountCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06004F52 RID: 20306 RVA: 0x00097480 File Offset: 0x00095680
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06004F53 RID: 20307 RVA: 0x000974A4 File Offset: 0x000956A4
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06004F54 RID: 20308 RVA: 0x000974C6 File Offset: 0x000956C6
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06004F55 RID: 20309 RVA: 0x000974D0 File Offset: 0x000956D0
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001EC0 RID: 7872
		private Result m_ResultCode;

		// Token: 0x04001EC1 RID: 7873
		private IntPtr m_ClientData;

		// Token: 0x04001EC2 RID: 7874
		private IntPtr m_LocalUserId;
	}
}
