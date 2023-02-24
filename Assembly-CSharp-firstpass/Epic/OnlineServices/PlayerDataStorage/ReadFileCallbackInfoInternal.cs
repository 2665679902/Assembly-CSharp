using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006C6 RID: 1734
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06004202 RID: 16898 RVA: 0x00089DDC File Offset: 0x00087FDC
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06004203 RID: 16899 RVA: 0x00089E00 File Offset: 0x00088000
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06004204 RID: 16900 RVA: 0x00089E22 File Offset: 0x00088022
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06004205 RID: 16901 RVA: 0x00089E2C File Offset: 0x0008802C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06004206 RID: 16902 RVA: 0x00089E50 File Offset: 0x00088050
		public string Filename
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Filename, out @default);
				return @default;
			}
		}

		// Token: 0x04001931 RID: 6449
		private Result m_ResultCode;

		// Token: 0x04001932 RID: 6450
		private IntPtr m_ClientData;

		// Token: 0x04001933 RID: 6451
		private IntPtr m_LocalUserId;

		// Token: 0x04001934 RID: 6452
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;
	}
}
