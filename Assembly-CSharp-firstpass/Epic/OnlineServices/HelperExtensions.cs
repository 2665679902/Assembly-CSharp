using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000527 RID: 1319
	public static class HelperExtensions
	{
		// Token: 0x0600385F RID: 14431 RVA: 0x00080730 File Offset: 0x0007E930
		public static bool IsOperationComplete(this Result result)
		{
			return Helper.IsOperationComplete(result);
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x00080738 File Offset: 0x0007E938
		public static string ToHexString(this byte[] byteArray)
		{
			return Helper.ToHexString(byteArray);
		}
	}
}
