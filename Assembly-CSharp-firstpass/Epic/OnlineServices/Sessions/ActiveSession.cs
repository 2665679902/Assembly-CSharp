using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005BA RID: 1466
	public sealed class ActiveSession : Handle
	{
		// Token: 0x06003BE8 RID: 15336 RVA: 0x00083E34 File Offset: 0x00082034
		public ActiveSession(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x00083E40 File Offset: 0x00082040
		public Result CopyInfo(ActiveSessionCopyInfoOptions options, out ActiveSessionInfo outActiveSessionInfo)
		{
			ActiveSessionCopyInfoOptionsInternal activeSessionCopyInfoOptionsInternal = Helper.CopyProperties<ActiveSessionCopyInfoOptionsInternal>(options);
			outActiveSessionInfo = Helper.GetDefault<ActiveSessionInfo>();
			IntPtr zero = IntPtr.Zero;
			Result result = ActiveSession.EOS_ActiveSession_CopyInfo(base.InnerHandle, ref activeSessionCopyInfoOptionsInternal, ref zero);
			Helper.TryMarshalDispose<ActiveSessionCopyInfoOptionsInternal>(ref activeSessionCopyInfoOptionsInternal);
			if (Helper.TryMarshalGet<ActiveSessionInfoInternal, ActiveSessionInfo>(zero, out outActiveSessionInfo))
			{
				ActiveSession.EOS_ActiveSession_Info_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x00083E98 File Offset: 0x00082098
		public uint GetRegisteredPlayerCount(ActiveSessionGetRegisteredPlayerCountOptions options)
		{
			ActiveSessionGetRegisteredPlayerCountOptionsInternal activeSessionGetRegisteredPlayerCountOptionsInternal = Helper.CopyProperties<ActiveSessionGetRegisteredPlayerCountOptionsInternal>(options);
			uint num = ActiveSession.EOS_ActiveSession_GetRegisteredPlayerCount(base.InnerHandle, ref activeSessionGetRegisteredPlayerCountOptionsInternal);
			Helper.TryMarshalDispose<ActiveSessionGetRegisteredPlayerCountOptionsInternal>(ref activeSessionGetRegisteredPlayerCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x00083ED0 File Offset: 0x000820D0
		public ProductUserId GetRegisteredPlayerByIndex(ActiveSessionGetRegisteredPlayerByIndexOptions options)
		{
			ActiveSessionGetRegisteredPlayerByIndexOptionsInternal activeSessionGetRegisteredPlayerByIndexOptionsInternal = Helper.CopyProperties<ActiveSessionGetRegisteredPlayerByIndexOptionsInternal>(options);
			IntPtr intPtr = ActiveSession.EOS_ActiveSession_GetRegisteredPlayerByIndex(base.InnerHandle, ref activeSessionGetRegisteredPlayerByIndexOptionsInternal);
			Helper.TryMarshalDispose<ActiveSessionGetRegisteredPlayerByIndexOptionsInternal>(ref activeSessionGetRegisteredPlayerByIndexOptionsInternal);
			ProductUserId @default = Helper.GetDefault<ProductUserId>();
			Helper.TryMarshalGet<ProductUserId>(intPtr, out @default);
			return @default;
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x00083F08 File Offset: 0x00082108
		public void Release()
		{
			ActiveSession.EOS_ActiveSession_Release(base.InnerHandle);
		}

		// Token: 0x06003BED RID: 15341
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_ActiveSession_Info_Release(IntPtr activeSessionInfo);

		// Token: 0x06003BEE RID: 15342
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_ActiveSession_Release(IntPtr activeSessionHandle);

		// Token: 0x06003BEF RID: 15343
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_ActiveSession_GetRegisteredPlayerByIndex(IntPtr handle, ref ActiveSessionGetRegisteredPlayerByIndexOptionsInternal options);

		// Token: 0x06003BF0 RID: 15344
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_ActiveSession_GetRegisteredPlayerCount(IntPtr handle, ref ActiveSessionGetRegisteredPlayerCountOptionsInternal options);

		// Token: 0x06003BF1 RID: 15345
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_ActiveSession_CopyInfo(IntPtr handle, ref ActiveSessionCopyInfoOptionsInternal options, ref IntPtr outActiveSessionInfo);
	}
}
