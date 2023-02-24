using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.OnlineServices
{
	// Token: 0x0200052F RID: 1327
	public sealed class EpicAccountId : Handle
	{
		// Token: 0x06003867 RID: 14439 RVA: 0x0008077D File Offset: 0x0007E97D
		public EpicAccountId(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x00080788 File Offset: 0x0007E988
		public bool IsValid()
		{
			int num = EpicAccountId.EOS_EpicAccountId_IsValid(base.InnerHandle);
			bool @default = Helper.GetDefault<bool>();
			Helper.TryMarshalGet(num, out @default);
			return @default;
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x000807B0 File Offset: 0x0007E9B0
		public Result ToString(StringBuilder outBuffer, ref int inOutBufferLength)
		{
			Result result = EpicAccountId.EOS_EpicAccountId_ToString(base.InnerHandle, outBuffer, ref inOutBufferLength);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x000807DC File Offset: 0x0007E9DC
		public static EpicAccountId FromString(string accountIdString)
		{
			IntPtr intPtr = EpicAccountId.EOS_EpicAccountId_FromString(accountIdString);
			EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
			Helper.TryMarshalGet<EpicAccountId>(intPtr, out @default);
			return @default;
		}

		// Token: 0x0600386B RID: 14443
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_EpicAccountId_FromString([MarshalAs(UnmanagedType.LPStr)] string accountIdString);

		// Token: 0x0600386C RID: 14444
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_EpicAccountId_ToString(IntPtr accountId, StringBuilder outBuffer, ref int inOutBufferLength);

		// Token: 0x0600386D RID: 14445
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern int EOS_EpicAccountId_IsValid(IntPtr accountId);
	}
}
