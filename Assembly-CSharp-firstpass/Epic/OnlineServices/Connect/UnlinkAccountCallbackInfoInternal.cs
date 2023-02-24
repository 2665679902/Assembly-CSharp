using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008D9 RID: 2265
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnlinkAccountCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06004F6E RID: 20334 RVA: 0x0009763C File Offset: 0x0009583C
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06004F6F RID: 20335 RVA: 0x00097660 File Offset: 0x00095860
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06004F70 RID: 20336 RVA: 0x00097682 File Offset: 0x00095882
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06004F71 RID: 20337 RVA: 0x0009768C File Offset: 0x0009588C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001ECD RID: 7885
		private Result m_ResultCode;

		// Token: 0x04001ECE RID: 7886
		private IntPtr m_ClientData;

		// Token: 0x04001ECF RID: 7887
		private IntPtr m_LocalUserId;
	}
}
