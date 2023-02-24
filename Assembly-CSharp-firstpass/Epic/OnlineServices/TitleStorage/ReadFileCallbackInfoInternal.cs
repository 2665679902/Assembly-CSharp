using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200059B RID: 1435
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06003AEF RID: 15087 RVA: 0x00082C50 File Offset: 0x00080E50
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06003AF0 RID: 15088 RVA: 0x00082C74 File Offset: 0x00080E74
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x00082C96 File Offset: 0x00080E96
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x00082CA0 File Offset: 0x00080EA0
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06003AF3 RID: 15091 RVA: 0x00082CC4 File Offset: 0x00080EC4
		public string Filename
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Filename, out @default);
				return @default;
			}
		}

		// Token: 0x0400166D RID: 5741
		private Result m_ResultCode;

		// Token: 0x0400166E RID: 5742
		private IntPtr m_ClientData;

		// Token: 0x0400166F RID: 5743
		private IntPtr m_LocalUserId;

		// Token: 0x04001670 RID: 5744
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;
	}
}
