using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006BC RID: 1724
	public sealed class PlayerDataStorageInterface : Handle
	{
		// Token: 0x060041AD RID: 16813 RVA: 0x00089608 File Offset: 0x00087808
		public PlayerDataStorageInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x00089614 File Offset: 0x00087814
		public void QueryFile(QueryFileOptions queryFileOptions, object clientData, OnQueryFileCompleteCallback completionCallback)
		{
			QueryFileOptionsInternal queryFileOptionsInternal = Helper.CopyProperties<QueryFileOptionsInternal>(queryFileOptions);
			OnQueryFileCompleteCallbackInternal onQueryFileCompleteCallbackInternal = new OnQueryFileCompleteCallbackInternal(PlayerDataStorageInterface.OnQueryFileComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionCallback, onQueryFileCompleteCallbackInternal, Array.Empty<Delegate>());
			PlayerDataStorageInterface.EOS_PlayerDataStorage_QueryFile(base.InnerHandle, ref queryFileOptionsInternal, zero, onQueryFileCompleteCallbackInternal);
			Helper.TryMarshalDispose<QueryFileOptionsInternal>(ref queryFileOptionsInternal);
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x00089664 File Offset: 0x00087864
		public void QueryFileList(QueryFileListOptions queryFileListOptions, object clientData, OnQueryFileListCompleteCallback completionCallback)
		{
			QueryFileListOptionsInternal queryFileListOptionsInternal = Helper.CopyProperties<QueryFileListOptionsInternal>(queryFileListOptions);
			OnQueryFileListCompleteCallbackInternal onQueryFileListCompleteCallbackInternal = new OnQueryFileListCompleteCallbackInternal(PlayerDataStorageInterface.OnQueryFileListComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionCallback, onQueryFileListCompleteCallbackInternal, Array.Empty<Delegate>());
			PlayerDataStorageInterface.EOS_PlayerDataStorage_QueryFileList(base.InnerHandle, ref queryFileListOptionsInternal, zero, onQueryFileListCompleteCallbackInternal);
			Helper.TryMarshalDispose<QueryFileListOptionsInternal>(ref queryFileListOptionsInternal);
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x000896B4 File Offset: 0x000878B4
		public Result CopyFileMetadataByFilename(CopyFileMetadataByFilenameOptions copyFileMetadataOptions, out FileMetadata outMetadata)
		{
			CopyFileMetadataByFilenameOptionsInternal copyFileMetadataByFilenameOptionsInternal = Helper.CopyProperties<CopyFileMetadataByFilenameOptionsInternal>(copyFileMetadataOptions);
			outMetadata = Helper.GetDefault<FileMetadata>();
			IntPtr zero = IntPtr.Zero;
			Result result = PlayerDataStorageInterface.EOS_PlayerDataStorage_CopyFileMetadataByFilename(base.InnerHandle, ref copyFileMetadataByFilenameOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyFileMetadataByFilenameOptionsInternal>(ref copyFileMetadataByFilenameOptionsInternal);
			if (Helper.TryMarshalGet<FileMetadataInternal, FileMetadata>(zero, out outMetadata))
			{
				PlayerDataStorageInterface.EOS_PlayerDataStorage_FileMetadata_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x0008970C File Offset: 0x0008790C
		public Result GetFileMetadataCount(GetFileMetadataCountOptions getFileMetadataCountOptions, out int outFileMetadataCount)
		{
			GetFileMetadataCountOptionsInternal getFileMetadataCountOptionsInternal = Helper.CopyProperties<GetFileMetadataCountOptionsInternal>(getFileMetadataCountOptions);
			outFileMetadataCount = Helper.GetDefault<int>();
			Result result = PlayerDataStorageInterface.EOS_PlayerDataStorage_GetFileMetadataCount(base.InnerHandle, ref getFileMetadataCountOptionsInternal, ref outFileMetadataCount);
			Helper.TryMarshalDispose<GetFileMetadataCountOptionsInternal>(ref getFileMetadataCountOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x0008974C File Offset: 0x0008794C
		public Result CopyFileMetadataAtIndex(CopyFileMetadataAtIndexOptions copyFileMetadataOptions, out FileMetadata outMetadata)
		{
			CopyFileMetadataAtIndexOptionsInternal copyFileMetadataAtIndexOptionsInternal = Helper.CopyProperties<CopyFileMetadataAtIndexOptionsInternal>(copyFileMetadataOptions);
			outMetadata = Helper.GetDefault<FileMetadata>();
			IntPtr zero = IntPtr.Zero;
			Result result = PlayerDataStorageInterface.EOS_PlayerDataStorage_CopyFileMetadataAtIndex(base.InnerHandle, ref copyFileMetadataAtIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyFileMetadataAtIndexOptionsInternal>(ref copyFileMetadataAtIndexOptionsInternal);
			if (Helper.TryMarshalGet<FileMetadataInternal, FileMetadata>(zero, out outMetadata))
			{
				PlayerDataStorageInterface.EOS_PlayerDataStorage_FileMetadata_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x000897A4 File Offset: 0x000879A4
		public void DuplicateFile(DuplicateFileOptions duplicateOptions, object clientData, OnDuplicateFileCompleteCallback completionCallback)
		{
			DuplicateFileOptionsInternal duplicateFileOptionsInternal = Helper.CopyProperties<DuplicateFileOptionsInternal>(duplicateOptions);
			OnDuplicateFileCompleteCallbackInternal onDuplicateFileCompleteCallbackInternal = new OnDuplicateFileCompleteCallbackInternal(PlayerDataStorageInterface.OnDuplicateFileComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionCallback, onDuplicateFileCompleteCallbackInternal, Array.Empty<Delegate>());
			PlayerDataStorageInterface.EOS_PlayerDataStorage_DuplicateFile(base.InnerHandle, ref duplicateFileOptionsInternal, zero, onDuplicateFileCompleteCallbackInternal);
			Helper.TryMarshalDispose<DuplicateFileOptionsInternal>(ref duplicateFileOptionsInternal);
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x000897F4 File Offset: 0x000879F4
		public void DeleteFile(DeleteFileOptions deleteOptions, object clientData, OnDeleteFileCompleteCallback completionCallback)
		{
			DeleteFileOptionsInternal deleteFileOptionsInternal = Helper.CopyProperties<DeleteFileOptionsInternal>(deleteOptions);
			OnDeleteFileCompleteCallbackInternal onDeleteFileCompleteCallbackInternal = new OnDeleteFileCompleteCallbackInternal(PlayerDataStorageInterface.OnDeleteFileComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionCallback, onDeleteFileCompleteCallbackInternal, Array.Empty<Delegate>());
			PlayerDataStorageInterface.EOS_PlayerDataStorage_DeleteFile(base.InnerHandle, ref deleteFileOptionsInternal, zero, onDeleteFileCompleteCallbackInternal);
			Helper.TryMarshalDispose<DeleteFileOptionsInternal>(ref deleteFileOptionsInternal);
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x00089844 File Offset: 0x00087A44
		public PlayerDataStorageFileTransferRequest ReadFile(ReadFileOptions readOptions, object clientData, OnReadFileCompleteCallback completionCallback)
		{
			ReadFileOptionsInternal readFileOptionsInternal = Helper.CopyProperties<ReadFileOptionsInternal>(readOptions);
			OnReadFileCompleteCallbackInternal onReadFileCompleteCallbackInternal = new OnReadFileCompleteCallbackInternal(PlayerDataStorageInterface.OnReadFileComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionCallback, onReadFileCompleteCallbackInternal, new Delegate[] { readOptions.ReadFileDataCallback, readFileOptionsInternal.ReadFileDataCallback, readOptions.FileTransferProgressCallback, readFileOptionsInternal.FileTransferProgressCallback });
			IntPtr intPtr = PlayerDataStorageInterface.EOS_PlayerDataStorage_ReadFile(base.InnerHandle, ref readFileOptionsInternal, zero, onReadFileCompleteCallbackInternal);
			Helper.TryMarshalDispose<ReadFileOptionsInternal>(ref readFileOptionsInternal);
			PlayerDataStorageFileTransferRequest @default = Helper.GetDefault<PlayerDataStorageFileTransferRequest>();
			Helper.TryMarshalGet<PlayerDataStorageFileTransferRequest>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x000898C8 File Offset: 0x00087AC8
		public PlayerDataStorageFileTransferRequest WriteFile(WriteFileOptions writeOptions, object clientData, OnWriteFileCompleteCallback completionCallback)
		{
			WriteFileOptionsInternal writeFileOptionsInternal = Helper.CopyProperties<WriteFileOptionsInternal>(writeOptions);
			OnWriteFileCompleteCallbackInternal onWriteFileCompleteCallbackInternal = new OnWriteFileCompleteCallbackInternal(PlayerDataStorageInterface.OnWriteFileComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionCallback, onWriteFileCompleteCallbackInternal, new Delegate[] { writeOptions.WriteFileDataCallback, writeFileOptionsInternal.WriteFileDataCallback, writeOptions.FileTransferProgressCallback, writeFileOptionsInternal.FileTransferProgressCallback });
			IntPtr intPtr = PlayerDataStorageInterface.EOS_PlayerDataStorage_WriteFile(base.InnerHandle, ref writeFileOptionsInternal, zero, onWriteFileCompleteCallbackInternal);
			Helper.TryMarshalDispose<WriteFileOptionsInternal>(ref writeFileOptionsInternal);
			PlayerDataStorageFileTransferRequest @default = Helper.GetDefault<PlayerDataStorageFileTransferRequest>();
			Helper.TryMarshalGet<PlayerDataStorageFileTransferRequest>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x0008994C File Offset: 0x00087B4C
		[MonoPInvokeCallback]
		internal static WriteResult OnWriteFileData(IntPtr callbackInfoAddress, IntPtr outDataBuffer, ref uint outDataWritten)
		{
			OnWriteFileDataCallback onWriteFileDataCallback = null;
			WriteFileDataCallbackInfo writeFileDataCallbackInfo = null;
			if (Helper.TryGetAdditionalCallback<OnWriteFileDataCallback, WriteFileDataCallbackInfoInternal, WriteFileDataCallbackInfo>(callbackInfoAddress, out onWriteFileDataCallback, out writeFileDataCallbackInfo))
			{
				byte[] array = null;
				WriteResult writeResult = onWriteFileDataCallback(writeFileDataCallbackInfo, out array, out outDataWritten);
				Marshal.Copy(array, 0, outDataBuffer, (int)outDataWritten);
				return writeResult;
			}
			return Helper.GetDefault<WriteResult>();
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x00089988 File Offset: 0x00087B88
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

		// Token: 0x060041B9 RID: 16825 RVA: 0x000899AC File Offset: 0x00087BAC
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

		// Token: 0x060041BA RID: 16826 RVA: 0x000899D8 File Offset: 0x00087BD8
		[MonoPInvokeCallback]
		internal static void OnWriteFileComplete(IntPtr address)
		{
			OnWriteFileCompleteCallback onWriteFileCompleteCallback = null;
			WriteFileCallbackInfo writeFileCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnWriteFileCompleteCallback, WriteFileCallbackInfoInternal, WriteFileCallbackInfo>(address, out onWriteFileCompleteCallback, out writeFileCallbackInfo))
			{
				onWriteFileCompleteCallback(writeFileCallbackInfo);
			}
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x000899FC File Offset: 0x00087BFC
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

		// Token: 0x060041BC RID: 16828 RVA: 0x00089A20 File Offset: 0x00087C20
		[MonoPInvokeCallback]
		internal static void OnDeleteFileComplete(IntPtr address)
		{
			OnDeleteFileCompleteCallback onDeleteFileCompleteCallback = null;
			DeleteFileCallbackInfo deleteFileCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnDeleteFileCompleteCallback, DeleteFileCallbackInfoInternal, DeleteFileCallbackInfo>(address, out onDeleteFileCompleteCallback, out deleteFileCallbackInfo))
			{
				onDeleteFileCompleteCallback(deleteFileCallbackInfo);
			}
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x00089A44 File Offset: 0x00087C44
		[MonoPInvokeCallback]
		internal static void OnDuplicateFileComplete(IntPtr address)
		{
			OnDuplicateFileCompleteCallback onDuplicateFileCompleteCallback = null;
			DuplicateFileCallbackInfo duplicateFileCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnDuplicateFileCompleteCallback, DuplicateFileCallbackInfoInternal, DuplicateFileCallbackInfo>(address, out onDuplicateFileCompleteCallback, out duplicateFileCallbackInfo))
			{
				onDuplicateFileCompleteCallback(duplicateFileCallbackInfo);
			}
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x00089A68 File Offset: 0x00087C68
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

		// Token: 0x060041BF RID: 16831 RVA: 0x00089A8C File Offset: 0x00087C8C
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

		// Token: 0x060041C0 RID: 16832
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_PlayerDataStorage_FileMetadata_Release(IntPtr fileMetadata);

		// Token: 0x060041C1 RID: 16833
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_PlayerDataStorage_WriteFile(IntPtr handle, ref WriteFileOptionsInternal writeOptions, IntPtr clientData, OnWriteFileCompleteCallbackInternal completionCallback);

		// Token: 0x060041C2 RID: 16834
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_PlayerDataStorage_ReadFile(IntPtr handle, ref ReadFileOptionsInternal readOptions, IntPtr clientData, OnReadFileCompleteCallbackInternal completionCallback);

		// Token: 0x060041C3 RID: 16835
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_PlayerDataStorage_DeleteFile(IntPtr handle, ref DeleteFileOptionsInternal deleteOptions, IntPtr clientData, OnDeleteFileCompleteCallbackInternal completionCallback);

		// Token: 0x060041C4 RID: 16836
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_PlayerDataStorage_DuplicateFile(IntPtr handle, ref DuplicateFileOptionsInternal duplicateOptions, IntPtr clientData, OnDuplicateFileCompleteCallbackInternal completionCallback);

		// Token: 0x060041C5 RID: 16837
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_PlayerDataStorage_CopyFileMetadataAtIndex(IntPtr handle, ref CopyFileMetadataAtIndexOptionsInternal copyFileMetadataOptions, ref IntPtr outMetadata);

		// Token: 0x060041C6 RID: 16838
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_PlayerDataStorage_GetFileMetadataCount(IntPtr handle, ref GetFileMetadataCountOptionsInternal getFileMetadataCountOptions, ref int outFileMetadataCount);

		// Token: 0x060041C7 RID: 16839
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_PlayerDataStorage_CopyFileMetadataByFilename(IntPtr handle, ref CopyFileMetadataByFilenameOptionsInternal copyFileMetadataOptions, ref IntPtr outMetadata);

		// Token: 0x060041C8 RID: 16840
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_PlayerDataStorage_QueryFileList(IntPtr handle, ref QueryFileListOptionsInternal queryFileListOptions, IntPtr clientData, OnQueryFileListCompleteCallbackInternal completionCallback);

		// Token: 0x060041C9 RID: 16841
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_PlayerDataStorage_QueryFile(IntPtr handle, ref QueryFileOptionsInternal queryFileOptions, IntPtr clientData, OnQueryFileCompleteCallbackInternal completionCallback);

		// Token: 0x0400190B RID: 6411
		public const int WritefileoptionsApiLatest = 1;

		// Token: 0x0400190C RID: 6412
		public const int ReadfileoptionsApiLatest = 1;

		// Token: 0x0400190D RID: 6413
		public const int DeletefileoptionsApiLatest = 1;

		// Token: 0x0400190E RID: 6414
		public const int DuplicatefileoptionsApiLatest = 1;

		// Token: 0x0400190F RID: 6415
		public const int CopyfilemetadatabyfilenameoptionsApiLatest = 1;

		// Token: 0x04001910 RID: 6416
		public const int CopyfilemetadataatindexoptionsApiLatest = 1;

		// Token: 0x04001911 RID: 6417
		public const int GetfilemetadatacountoptionsApiLatest = 1;

		// Token: 0x04001912 RID: 6418
		public const int QueryfilelistoptionsApiLatest = 1;

		// Token: 0x04001913 RID: 6419
		public const int QueryfileoptionsApiLatest = 1;

		// Token: 0x04001914 RID: 6420
		public const int FilemetadataApiLatest = 1;

		// Token: 0x04001915 RID: 6421
		public const int FileMaxSizeBytes = 67108864;

		// Token: 0x04001916 RID: 6422
		public const int FilenameMaxLengthBytes = 64;
	}
}
