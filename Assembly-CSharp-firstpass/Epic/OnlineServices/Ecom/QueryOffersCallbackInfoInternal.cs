using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000872 RID: 2162
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOffersCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06004CFB RID: 19707 RVA: 0x00095044 File Offset: 0x00093244
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06004CFC RID: 19708 RVA: 0x00095068 File Offset: 0x00093268
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06004CFD RID: 19709 RVA: 0x0009508A File Offset: 0x0009328A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06004CFE RID: 19710 RVA: 0x00095094 File Offset: 0x00093294
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001DD1 RID: 7633
		private Result m_ResultCode;

		// Token: 0x04001DD2 RID: 7634
		private IntPtr m_ClientData;

		// Token: 0x04001DD3 RID: 7635
		private IntPtr m_LocalUserId;
	}
}
