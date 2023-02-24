using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200082B RID: 2091
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CheckoutCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06004AFE RID: 19198 RVA: 0x00092F28 File Offset: 0x00091128
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06004AFF RID: 19199 RVA: 0x00092F4C File Offset: 0x0009114C
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06004B00 RID: 19200 RVA: 0x00092F6E File Offset: 0x0009116E
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06004B01 RID: 19201 RVA: 0x00092F78 File Offset: 0x00091178
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06004B02 RID: 19202 RVA: 0x00092F9C File Offset: 0x0009119C
		public string TransactionId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_TransactionId, out @default);
				return @default;
			}
		}

		// Token: 0x04001CFB RID: 7419
		private Result m_ResultCode;

		// Token: 0x04001CFC RID: 7420
		private IntPtr m_ClientData;

		// Token: 0x04001CFD RID: 7421
		private IntPtr m_LocalUserId;

		// Token: 0x04001CFE RID: 7422
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_TransactionId;
	}
}
