using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.OnlineServices
{
	// Token: 0x0200052E RID: 1326
	public sealed class ContinuanceToken : Handle
	{
		// Token: 0x06003864 RID: 14436 RVA: 0x00080748 File Offset: 0x0007E948
		public ContinuanceToken(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x00080754 File Offset: 0x0007E954
		public Result ToString(StringBuilder outBuffer, ref int inOutBufferLength)
		{
			Result result = ContinuanceToken.EOS_ContinuanceToken_ToString(base.InnerHandle, outBuffer, ref inOutBufferLength);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003866 RID: 14438
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_ContinuanceToken_ToString(IntPtr continuanceToken, StringBuilder outBuffer, ref int inOutBufferLength);
	}
}
