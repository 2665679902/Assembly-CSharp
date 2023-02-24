using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006BB RID: 1723
	public sealed class PlayerDataStorageFileTransferRequest : Handle
	{
		// Token: 0x060041A4 RID: 16804 RVA: 0x0008956F File Offset: 0x0008776F
		public PlayerDataStorageFileTransferRequest(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x00089578 File Offset: 0x00087778
		public Result GetFileRequestState()
		{
			Result result = PlayerDataStorageFileTransferRequest.EOS_PlayerDataStorageFileTransferRequest_GetFileRequestState(base.InnerHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x000895A0 File Offset: 0x000877A0
		public Result GetFilename(uint filenameStringBufferSizeBytes, StringBuilder outStringBuffer, out int outStringLength)
		{
			outStringLength = Helper.GetDefault<int>();
			Result result = PlayerDataStorageFileTransferRequest.EOS_PlayerDataStorageFileTransferRequest_GetFilename(base.InnerHandle, filenameStringBufferSizeBytes, outStringBuffer, ref outStringLength);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x000895D4 File Offset: 0x000877D4
		public Result CancelRequest()
		{
			Result result = PlayerDataStorageFileTransferRequest.EOS_PlayerDataStorageFileTransferRequest_CancelRequest(base.InnerHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x000895FB File Offset: 0x000877FB
		public void Release()
		{
			PlayerDataStorageFileTransferRequest.EOS_PlayerDataStorageFileTransferRequest_Release(base.InnerHandle);
		}

		// Token: 0x060041A9 RID: 16809
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_PlayerDataStorageFileTransferRequest_Release(IntPtr playerDataStorageFileTransferHandle);

		// Token: 0x060041AA RID: 16810
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_PlayerDataStorageFileTransferRequest_CancelRequest(IntPtr handle);

		// Token: 0x060041AB RID: 16811
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_PlayerDataStorageFileTransferRequest_GetFilename(IntPtr handle, uint filenameStringBufferSizeBytes, StringBuilder outStringBuffer, ref int outStringLength);

		// Token: 0x060041AC RID: 16812
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_PlayerDataStorageFileTransferRequest_GetFileRequestState(IntPtr handle);
	}
}
