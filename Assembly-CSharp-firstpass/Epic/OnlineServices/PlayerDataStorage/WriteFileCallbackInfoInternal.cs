using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006CD RID: 1741
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct WriteFileCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x0600423C RID: 16956 RVA: 0x0008A178 File Offset: 0x00088378
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600423D RID: 16957 RVA: 0x0008A19C File Offset: 0x0008839C
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600423E RID: 16958 RVA: 0x0008A1BE File Offset: 0x000883BE
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600423F RID: 16959 RVA: 0x0008A1C8 File Offset: 0x000883C8
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06004240 RID: 16960 RVA: 0x0008A1EC File Offset: 0x000883EC
		public string Filename
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Filename, out @default);
				return @default;
			}
		}

		// Token: 0x04001955 RID: 6485
		private Result m_ResultCode;

		// Token: 0x04001956 RID: 6486
		private IntPtr m_ClientData;

		// Token: 0x04001957 RID: 6487
		private IntPtr m_LocalUserId;

		// Token: 0x04001958 RID: 6488
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;
	}
}
