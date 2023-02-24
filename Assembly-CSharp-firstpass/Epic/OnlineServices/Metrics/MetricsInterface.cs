using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x02000716 RID: 1814
	public sealed class MetricsInterface : Handle
	{
		// Token: 0x0600446D RID: 17517 RVA: 0x0008C77B File Offset: 0x0008A97B
		public MetricsInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x0600446E RID: 17518 RVA: 0x0008C784 File Offset: 0x0008A984
		public Result BeginPlayerSession(BeginPlayerSessionOptions options)
		{
			BeginPlayerSessionOptionsInternal beginPlayerSessionOptionsInternal = Helper.CopyProperties<BeginPlayerSessionOptionsInternal>(options);
			Result result = MetricsInterface.EOS_Metrics_BeginPlayerSession(base.InnerHandle, ref beginPlayerSessionOptionsInternal);
			Helper.TryMarshalDispose<BeginPlayerSessionOptionsInternal>(ref beginPlayerSessionOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x0600446F RID: 17519 RVA: 0x0008C7BC File Offset: 0x0008A9BC
		public Result EndPlayerSession(EndPlayerSessionOptions options)
		{
			EndPlayerSessionOptionsInternal endPlayerSessionOptionsInternal = Helper.CopyProperties<EndPlayerSessionOptionsInternal>(options);
			Result result = MetricsInterface.EOS_Metrics_EndPlayerSession(base.InnerHandle, ref endPlayerSessionOptionsInternal);
			Helper.TryMarshalDispose<EndPlayerSessionOptionsInternal>(ref endPlayerSessionOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004470 RID: 17520
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Metrics_EndPlayerSession(IntPtr handle, ref EndPlayerSessionOptionsInternal options);

		// Token: 0x06004471 RID: 17521
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Metrics_BeginPlayerSession(IntPtr handle, ref BeginPlayerSessionOptionsInternal options);

		// Token: 0x04001A4E RID: 6734
		public const int EndplayersessionApiLatest = 1;

		// Token: 0x04001A4F RID: 6735
		public const int BeginplayersessionApiLatest = 1;
	}
}
