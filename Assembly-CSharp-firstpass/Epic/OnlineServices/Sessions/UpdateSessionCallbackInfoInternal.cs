using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200065F RID: 1631
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateSessionCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06003F94 RID: 16276 RVA: 0x00087900 File Offset: 0x00085B00
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06003F95 RID: 16277 RVA: 0x00087924 File Offset: 0x00085B24
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06003F96 RID: 16278 RVA: 0x00087946 File Offset: 0x00085B46
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06003F97 RID: 16279 RVA: 0x00087950 File Offset: 0x00085B50
		public string SessionName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_SessionName, out @default);
				return @default;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06003F98 RID: 16280 RVA: 0x00087974 File Offset: 0x00085B74
		public string SessionId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_SessionId, out @default);
				return @default;
			}
		}

		// Token: 0x04001845 RID: 6213
		private Result m_ResultCode;

		// Token: 0x04001846 RID: 6214
		private IntPtr m_ClientData;

		// Token: 0x04001847 RID: 6215
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionName;

		// Token: 0x04001848 RID: 6216
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionId;
	}
}
