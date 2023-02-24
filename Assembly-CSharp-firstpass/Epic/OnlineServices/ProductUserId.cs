using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.OnlineServices
{
	// Token: 0x02000536 RID: 1334
	public sealed class ProductUserId : Handle
	{
		// Token: 0x06003889 RID: 14473 RVA: 0x0008099F File Offset: 0x0007EB9F
		public ProductUserId(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000809A8 File Offset: 0x0007EBA8
		public bool IsValid()
		{
			int num = ProductUserId.EOS_ProductUserId_IsValid(base.InnerHandle);
			bool @default = Helper.GetDefault<bool>();
			Helper.TryMarshalGet(num, out @default);
			return @default;
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000809D0 File Offset: 0x0007EBD0
		public Result ToString(StringBuilder outBuffer, ref int inOutBufferLength)
		{
			Result result = ProductUserId.EOS_ProductUserId_ToString(base.InnerHandle, outBuffer, ref inOutBufferLength);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000809FC File Offset: 0x0007EBFC
		public static ProductUserId FromString(string accountIdString)
		{
			IntPtr intPtr = ProductUserId.EOS_ProductUserId_FromString(accountIdString);
			ProductUserId @default = Helper.GetDefault<ProductUserId>();
			Helper.TryMarshalGet<ProductUserId>(intPtr, out @default);
			return @default;
		}

		// Token: 0x0600388D RID: 14477
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_ProductUserId_FromString([MarshalAs(UnmanagedType.LPStr)] string accountIdString);

		// Token: 0x0600388E RID: 14478
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_ProductUserId_ToString(IntPtr accountId, StringBuilder outBuffer, ref int inOutBufferLength);

		// Token: 0x0600388F RID: 14479
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern int EOS_ProductUserId_IsValid(IntPtr accountId);
	}
}
