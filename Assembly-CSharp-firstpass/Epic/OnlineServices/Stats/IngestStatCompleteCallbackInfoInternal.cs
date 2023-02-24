using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005AC RID: 1452
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IngestStatCompleteCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x000836B8 File Offset: 0x000818B8
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x000836DC File Offset: 0x000818DC
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06003B7A RID: 15226 RVA: 0x000836FE File Offset: 0x000818FE
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06003B7B RID: 15227 RVA: 0x00083708 File Offset: 0x00081908
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06003B7C RID: 15228 RVA: 0x0008372C File Offset: 0x0008192C
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x040016AC RID: 5804
		private Result m_ResultCode;

		// Token: 0x040016AD RID: 5805
		private IntPtr m_ClientData;

		// Token: 0x040016AE RID: 5806
		private IntPtr m_LocalUserId;

		// Token: 0x040016AF RID: 5807
		private IntPtr m_TargetUserId;
	}
}
