using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x020005A2 RID: 1442
	public sealed class TitleStorageInterface : Handle
	{
		// Token: 0x06003B29 RID: 15145 RVA: 0x00083038 File Offset: 0x00081238
		public TitleStorageInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x00083044 File Offset: 0x00081244
		public void QueryFile(QueryFileOptions options, object clientData, OnQueryFileCompleteCallback completionCallback)
		{
			QueryFileOptionsInternal queryFileOptionsInternal = Helper.CopyProperties<QueryFileOptionsInternal>(options);
			OnQueryFileCompleteCallbackInternal onQueryFileCompleteCallbackInternal = new OnQueryFileCompleteCallbackInternal(TitleStorageInterface.OnQueryFileComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionCallback, onQueryFileCompleteCallbackInternal, Array.Empty<Delegate>());
			TitleStorageInterface.EOS_TitleStorage_QueryFile(base.InnerHandle, ref queryFileOptionsInternal, zero, onQueryFileCompleteCallbackInternal);
			Helper.TryMarshalDispose<QueryFileOptionsInternal>(ref queryFileOptionsInternal);
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x00083094 File Offset: 0x00081294
		public void QueryFileList(QueryFileListOptions options, object clientData, OnQueryFileListCompleteCallback completionCallback)
		{
			QueryFileListOptionsInternal queryFileListOptionsInternal = Helper.CopyProperties<QueryFileListOptionsInternal>(options);
			OnQueryFileListCompleteCallbackInternal onQueryFileListCompleteCallbackInternal = new OnQueryFileListCompleteCallbackInternal(TitleStorageInterface.OnQueryFileListComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionCallback, onQueryFileListCompleteCallbackInternal, Array.Empty<Delegate>());
			TitleStorageInterface.EOS_TitleStorage_QueryFileList(base.InnerHandle, ref queryFileListOptionsInternal, zero, onQueryFileListCompleteCallbackInternal);
			Helper.TryMarshalDispose<QueryFileListOptionsInternal>(ref queryFileListOptionsInternal);
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x000830E4 File Offset: 0x000812E4
		public Result CopyFileMetadataByFilename(CopyFileMetadataByFilenameOptions options, out FileMetadata outMetadata)
		{
			CopyFileMetadataByFilenameOptionsInternal copyFileMetadataByFilenameOptionsInternal = Helper.CopyProperties<CopyFileMetadataByFilenameOptionsInternal>(options);
			outMetadata = Helper.GetDefault<FileMetadata>();
			IntPtr zero = IntPtr.Zero;
			Result result = TitleStorageInterface.EOS_TitleStorage_CopyFileMetadataByFilename(base.InnerHandle, ref copyFileMetadataByFilenameOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyFileMetadataByFilenameOptionsInternal>(ref copyFileMetadataByFilenameOptionsInternal);
			if (Helper.TryMarshalGet<FileMetadataInternal, FileMetadata>(zero, out outMetadata))
			{
				TitleStorageInterface.EOS_TitleStorage_FileMetadata_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x0008313C File Offset: 0x0008133C
		public uint GetFileMetadataCount(GetFileMetadataCountOptions options)
		{
			GetFileMetadataCountOptionsInternal getFileMetadataCountOptionsInternal = Helper.CopyProperties<GetFileMetadataCountOptionsInternal>(options);
			uint num = TitleStorageInterface.EOS_TitleStorage_GetFileMetadataCount(base.InnerHandle, ref getFileMetadataCountOptionsInternal);
			Helper.TryMarshalDispose<GetFileMetadataCountOptionsInternal>(ref getFileMetadataCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x00083174 File Offset: 0x00081374
		public Result CopyFileMetadataAtIndex(CopyFileMetadataAtIndexOptions options, out FileMetadata outMetadata)
		{
			CopyFileMetadataAtIndexOptionsInternal copyFileMetadataAtIndexOptionsInternal = Helper.CopyProperties<CopyFileMetadataAtIndexOptionsInternal>(options);
			outMetadata = Helper.GetDefault<FileMetadata>();
			IntPtr zero = IntPtr.Zero;
			Result result = TitleStorageInterface.EOS_TitleStorage_CopyFileMetadataAtIndex(base.InnerHandle, ref copyFileMetadataAtIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyFileMetadataAtIndexOptionsInternal>(ref copyFileMetadataAtIndexOptionsInternal);
			if (Helper.TryMarshalGet<FileMetadataInternal, FileMetadata>(zero, out outMetadata))
			{
				TitleStorageInterface.EOS_TitleStorage_FileMetadata_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000831CC File Offset: 0x000813CC
		public TitleStorageFileTransferRequest ReadFile(ReadFileOptions options, object clientData, OnReadFileCompleteCallback completionCallback)
		{
			ReadFileOptionsInternal readFileOptionsInternal = Helper.CopyProperties<ReadFileOptionsInternal>(options);
			OnReadFileCompleteCallbackInternal onReadFileCompleteCallbackInternal = new OnReadFileCompleteCallbackInternal(TitleStorageInterface.OnReadFileComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionCallback, onReadFileCompleteCallbackInternal, new Delegate[] { options.ReadFileDataCallback, readFileOptionsInternal.ReadFileDataCallback, options.FileTransferProgressCallback, readFileOptionsInternal.FileTransferProgressCallback });
			IntPtr intPtr = TitleStorageInterface.EOS_TitleStorage_ReadFile(base.InnerHandle, ref readFileOptionsInternal, zero, onReadFileCompleteCallbackInternal);
			Helper.TryMarshalDispose<ReadFileOptionsInternal>(ref readFileOptionsInternal);
			TitleStorageFileTransferRequest @default = Helper.GetDefault<TitleStorageFileTransferRequest>();
			Helper.TryMarshalGet<TitleStorageFileTransferRequest>(intPtr, out @default);
			return @default;
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x00083250 File Offset: 0x00081450
		public Result DeleteCache(DeleteCacheOptions options, object clientData, OnDeleteCacheCompleteCallback completionCallback)
		{
			DeleteCacheOptionsInternal deleteCacheOptionsInternal = Helper.CopyProperties<DeleteCacheOptionsInternal>(options);
			OnDeleteCacheCompleteCallbackInternal onDeleteCacheCompleteCallbackInternal = new OnDeleteCacheCompleteCallbackInternal(TitleStorageInterface.OnDeleteCacheComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionCallback, onDeleteCacheCompleteCallbackInternal, Array.Empty<Delegate>());
			Result result = TitleStorageInterface.EOS_TitleStorage_DeleteCache(base.InnerHandle, ref deleteCacheOptionsInternal, zero, onDeleteCacheCompleteCallbackInternal);
			Helper.TryMarshalDispose<DeleteCacheOptionsInternal>(ref deleteCacheOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x000832AC File Offset: 0x000814AC
		[MonoPInvokeCallback]
		internal static void OnFileTransferProgress(IntPtr callbackInfoAddress)
		{
			OnFileTransferProgressCallback onFileTransferProgressCallback = null;
			FileTransferProgressCallbackInfo fileTransferProgressCallbackInfo = null;
			if (Helper.TryGetAdditionalCallback<OnFileTransferProgressCallback, FileTransferProgressCallbackInfoInternal, FileTransferProgressCallbackInfo>(callbackInfoAddress, out onFileTransferProgressCallback, out fileTransferProgressCallbackInfo))
			{
				onFileTransferProgressCallback(fileTransferProgressCallbackInfo);
			}
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000832D0 File Offset: 0x000814D0
		[MonoPInvokeCallback]
		internal static ReadResult OnReadFileData(IntPtr callbackInfoAddress)
		{
			OnReadFileDataCallback onReadFileDataCallback = null;
			ReadFileDataCallbackInfo readFileDataCallbackInfo = null;
			if (Helper.TryGetAdditionalCallback<OnReadFileDataCallback, ReadFileDataCallbackInfoInternal, ReadFileDataCallbackInfo>(callbackInfoAddress, out onReadFileDataCallback, out readFileDataCallbackInfo))
			{
				return onReadFileDataCallback(readFileDataCallbackInfo);
			}
			return Helper.GetDefault<ReadResult>();
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x000832FC File Offset: 0x000814FC
		[MonoPInvokeCallback]
		internal static void OnDeleteCacheComplete(IntPtr address)
		{
			OnDeleteCacheCompleteCallback onDeleteCacheCompleteCallback = null;
			DeleteCacheCallbackInfo deleteCacheCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnDeleteCacheCompleteCallback, DeleteCacheCallbackInfoInternal, DeleteCacheCallbackInfo>(address, out onDeleteCacheCompleteCallback, out deleteCacheCallbackInfo))
			{
				onDeleteCacheCompleteCallback(deleteCacheCallbackInfo);
			}
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x00083320 File Offset: 0x00081520
		[MonoPInvokeCallback]
		internal static void OnReadFileComplete(IntPtr address)
		{
			OnReadFileCompleteCallback onReadFileCompleteCallback = null;
			ReadFileCallbackInfo readFileCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnReadFileCompleteCallback, ReadFileCallbackInfoInternal, ReadFileCallbackInfo>(address, out onReadFileCompleteCallback, out readFileCallbackInfo))
			{
				onReadFileCompleteCallback(readFileCallbackInfo);
			}
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x00083344 File Offset: 0x00081544
		[MonoPInvokeCallback]
		internal static void OnQueryFileListComplete(IntPtr address)
		{
			OnQueryFileListCompleteCallback onQueryFileListCompleteCallback = null;
			QueryFileListCallbackInfo queryFileListCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryFileListCompleteCallback, QueryFileListCallbackInfoInternal, QueryFileListCallbackInfo>(address, out onQueryFileListCompleteCallback, out queryFileListCallbackInfo))
			{
				onQueryFileListCompleteCallback(queryFileListCallbackInfo);
			}
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x00083368 File Offset: 0x00081568
		[MonoPInvokeCallback]
		internal static void OnQueryFileComplete(IntPtr address)
		{
			OnQueryFileCompleteCallback onQueryFileCompleteCallback = null;
			QueryFileCallbackInfo queryFileCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryFileCompleteCallback, QueryFileCallbackInfoInternal, QueryFileCallbackInfo>(address, out onQueryFileCompleteCallback, out queryFileCallbackInfo))
			{
				onQueryFileCompleteCallback(queryFileCallbackInfo);
			}
		}

		// Token: 0x06003B37 RID: 15159
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_TitleStorage_FileMetadata_Release(IntPtr fileMetadata);

		// Token: 0x06003B38 RID: 15160
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_TitleStorage_DeleteCache(IntPtr handle, ref DeleteCacheOptionsInternal options, IntPtr clientData, OnDeleteCacheCompleteCallbackInternal completionCallback);

		// Token: 0x06003B39 RID: 15161
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_TitleStorage_ReadFile(IntPtr handle, ref ReadFileOptionsInternal options, IntPtr clientData, OnReadFileCompleteCallbackInternal completionCallback);

		// Token: 0x06003B3A RID: 15162
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_TitleStorage_CopyFileMetadataAtIndex(IntPtr handle, ref CopyFileMetadataAtIndexOptionsInternal options, ref IntPtr outMetadata);

		// Token: 0x06003B3B RID: 15163
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_TitleStorage_GetFileMetadataCount(IntPtr handle, ref GetFileMetadataCountOptionsInternal options);

		// Token: 0x06003B3C RID: 15164
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_TitleStorage_CopyFileMetadataByFilename(IntPtr handle, ref CopyFileMetadataByFilenameOptionsInternal options, ref IntPtr outMetadata);

		// Token: 0x06003B3D RID: 15165
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_TitleStorage_QueryFileList(IntPtr handle, ref QueryFileListOptionsInternal options, IntPtr clientData, OnQueryFileListCompleteCallbackInternal completionCallback);

		// Token: 0x06003B3E RID: 15166
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_TitleStorage_QueryFile(IntPtr handle, ref QueryFileOptionsInternal options, IntPtr clientData, OnQueryFileCompleteCallbackInternal completionCallback);

		// Token: 0x0400168D RID: 5773
		public const int DeletecacheoptionsApiLatest = 1;

		// Token: 0x0400168E RID: 5774
		public const int ReadfileoptionsApiLatest = 1;

		// Token: 0x0400168F RID: 5775
		public const int CopyfilemetadatabyfilenameoptionsApiLatest = 1;

		// Token: 0x04001690 RID: 5776
		public const int CopyfilemetadataatindexoptionsApiLatest = 1;

		// Token: 0x04001691 RID: 5777
		public const int GetfilemetadatacountoptionsApiLatest = 1;

		// Token: 0x04001692 RID: 5778
		public const int QueryfilelistoptionsApiLatest = 1;

		// Token: 0x04001693 RID: 5779
		public const int QueryfileoptionsApiLatest = 1;

		// Token: 0x04001694 RID: 5780
		public const int FilemetadataApiLatest = 1;

		// Token: 0x04001695 RID: 5781
		public const int FilenameMaxLengthBytes = 64;
	}
}
