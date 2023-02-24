using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008EE RID: 2286
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LinkAccountCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06004FE8 RID: 20456 RVA: 0x00097F6C File Offset: 0x0009616C
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06004FE9 RID: 20457 RVA: 0x00097F90 File Offset: 0x00096190
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06004FEA RID: 20458 RVA: 0x00097FB2 File Offset: 0x000961B2
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06004FEB RID: 20459 RVA: 0x00097FBC File Offset: 0x000961BC
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06004FEC RID: 20460 RVA: 0x00097FE0 File Offset: 0x000961E0
		public PinGrantInfoInternal? PinGrantInfo
		{
			get
			{
				PinGrantInfoInternal? @default = Helper.GetDefault<PinGrantInfoInternal?>();
				Helper.TryMarshalGet<PinGrantInfoInternal>(this.m_PinGrantInfo, out @default);
				return @default;
			}
		}

		// Token: 0x04001F05 RID: 7941
		private Result m_ResultCode;

		// Token: 0x04001F06 RID: 7942
		private IntPtr m_ClientData;

		// Token: 0x04001F07 RID: 7943
		private IntPtr m_LocalUserId;

		// Token: 0x04001F08 RID: 7944
		private IntPtr m_PinGrantInfo;
	}
}
