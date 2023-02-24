using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008D1 RID: 2257
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryProductUserIdMappingsCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06004F36 RID: 20278 RVA: 0x000972AC File Offset: 0x000954AC
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06004F37 RID: 20279 RVA: 0x000972D0 File Offset: 0x000954D0
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06004F38 RID: 20280 RVA: 0x000972F2 File Offset: 0x000954F2
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06004F39 RID: 20281 RVA: 0x000972FC File Offset: 0x000954FC
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001EB2 RID: 7858
		private Result m_ResultCode;

		// Token: 0x04001EB3 RID: 7859
		private IntPtr m_ClientData;

		// Token: 0x04001EB4 RID: 7860
		private IntPtr m_LocalUserId;
	}
}
