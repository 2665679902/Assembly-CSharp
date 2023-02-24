using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x020005A1 RID: 1441
	public sealed class TitleStorageFileTransferRequest : Handle
	{
		// Token: 0x06003B20 RID: 15136 RVA: 0x00082F9F File Offset: 0x0008119F
		public TitleStorageFileTransferRequest(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x00082FA8 File Offset: 0x000811A8
		public Result GetFileRequestState()
		{
			Result result = TitleStorageFileTransferRequest.EOS_TitleStorageFileTransferRequest_GetFileRequestState(base.InnerHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x00082FD0 File Offset: 0x000811D0
		public Result GetFilename(uint filenameStringBufferSizeBytes, StringBuilder outStringBuffer, out int outStringLength)
		{
			outStringLength = Helper.GetDefault<int>();
			Result result = TitleStorageFileTransferRequest.EOS_TitleStorageFileTransferRequest_GetFilename(base.InnerHandle, filenameStringBufferSizeBytes, outStringBuffer, ref outStringLength);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x00083004 File Offset: 0x00081204
		public Result CancelRequest()
		{
			Result result = TitleStorageFileTransferRequest.EOS_TitleStorageFileTransferRequest_CancelRequest(base.InnerHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x0008302B File Offset: 0x0008122B
		public void Release()
		{
			TitleStorageFileTransferRequest.EOS_TitleStorageFileTransferRequest_Release(base.InnerHandle);
		}

		// Token: 0x06003B25 RID: 15141
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_TitleStorageFileTransferRequest_Release(IntPtr titleStorageFileTransferHandle);

		// Token: 0x06003B26 RID: 15142
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_TitleStorageFileTransferRequest_CancelRequest(IntPtr handle);

		// Token: 0x06003B27 RID: 15143
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_TitleStorageFileTransferRequest_GetFilename(IntPtr handle, uint filenameStringBufferSizeBytes, StringBuilder outStringBuffer, ref int outStringLength);

		// Token: 0x06003B28 RID: 15144
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_TitleStorageFileTransferRequest_GetFileRequestState(IntPtr handle);
	}
}
