using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002C0 RID: 704
	public class IRailStorageHelperImpl : RailObject, IRailStorageHelper
	{
		// Token: 0x06002A1A RID: 10778 RVA: 0x00054D58 File Offset: 0x00052F58
		internal IRailStorageHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x00054D68 File Offset: 0x00052F68
		~IRailStorageHelperImpl()
		{
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x00054D90 File Offset: 0x00052F90
		public virtual IRailFile OpenFile(string filename, out RailResult result)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailStorageHelper_OpenFile__SWIG_0(this.swigCPtr_, filename, out result);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailFileImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x00054DC0 File Offset: 0x00052FC0
		public virtual IRailFile OpenFile(string filename)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailStorageHelper_OpenFile__SWIG_1(this.swigCPtr_, filename);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailFileImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x00054DF0 File Offset: 0x00052FF0
		public virtual IRailFile CreateFile(string filename, out RailResult result)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailStorageHelper_CreateFile__SWIG_0(this.swigCPtr_, filename, out result);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailFileImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x00054E20 File Offset: 0x00053020
		public virtual IRailFile CreateFile(string filename)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailStorageHelper_CreateFile__SWIG_1(this.swigCPtr_, filename);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailFileImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x00054E4F File Offset: 0x0005304F
		public virtual bool IsFileExist(string filename)
		{
			return RAIL_API_PINVOKE.IRailStorageHelper_IsFileExist(this.swigCPtr_, filename);
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x00054E60 File Offset: 0x00053060
		public virtual bool ListFiles(List<string> filelist)
		{
			IntPtr intPtr = ((filelist == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailStorageHelper_ListFiles(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (filelist != null)
				{
					RailConverter.Cpp2Csharp(intPtr, filelist);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return flag;
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x00054EB0 File Offset: 0x000530B0
		public virtual RailResult RemoveFile(string filename)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_RemoveFile(this.swigCPtr_, filename);
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x00054EBE File Offset: 0x000530BE
		public virtual bool IsFileSyncedToCloud(string filename)
		{
			return RAIL_API_PINVOKE.IRailStorageHelper_IsFileSyncedToCloud(this.swigCPtr_, filename);
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x00054ECC File Offset: 0x000530CC
		public virtual RailResult GetFileTimestamp(string filename, out ulong time_stamp)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_GetFileTimestamp(this.swigCPtr_, filename, out time_stamp);
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x00054EDB File Offset: 0x000530DB
		public virtual uint GetFileCount()
		{
			return RAIL_API_PINVOKE.IRailStorageHelper_GetFileCount(this.swigCPtr_);
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x00054EE8 File Offset: 0x000530E8
		public virtual RailResult GetFileNameAndSize(uint file_index, out string filename, out ulong file_size)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_GetFileNameAndSize(this.swigCPtr_, file_index, intPtr, out file_size);
			}
			finally
			{
				filename = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x00054F34 File Offset: 0x00053134
		public virtual RailResult AsyncQueryQuota()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_AsyncQueryQuota(this.swigCPtr_);
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x00054F44 File Offset: 0x00053144
		public virtual RailResult SetSyncFileOption(string filename, RailSyncFileOption option)
		{
			IntPtr intPtr = ((option == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSyncFileOption__SWIG_0());
			if (option != null)
			{
				RailConverter.Csharp2Cpp(option, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_SetSyncFileOption(this.swigCPtr_, filename, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSyncFileOption(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x00054F94 File Offset: 0x00053194
		public virtual bool IsCloudStorageEnabledForApp()
		{
			return RAIL_API_PINVOKE.IRailStorageHelper_IsCloudStorageEnabledForApp(this.swigCPtr_);
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x00054FA1 File Offset: 0x000531A1
		public virtual bool IsCloudStorageEnabledForPlayer()
		{
			return RAIL_API_PINVOKE.IRailStorageHelper_IsCloudStorageEnabledForPlayer(this.swigCPtr_);
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x00054FB0 File Offset: 0x000531B0
		public virtual RailResult AsyncPublishFileToUserSpace(RailPublishFileToUserSpaceOption option, string user_data)
		{
			IntPtr intPtr = ((option == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailPublishFileToUserSpaceOption__SWIG_0());
			if (option != null)
			{
				RailConverter.Csharp2Cpp(option, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_AsyncPublishFileToUserSpace(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailPublishFileToUserSpaceOption(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x00055000 File Offset: 0x00053200
		public virtual IRailStreamFile OpenStreamFile(string filename, RailStreamFileOption option, out RailResult result)
		{
			IntPtr intPtr = ((option == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailStreamFileOption__SWIG_0());
			if (option != null)
			{
				RailConverter.Csharp2Cpp(option, intPtr);
			}
			IRailStreamFile railStreamFile;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailStorageHelper_OpenStreamFile__SWIG_0(this.swigCPtr_, filename, intPtr, out result);
				railStreamFile = ((intPtr2 == IntPtr.Zero) ? null : new IRailStreamFileImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailStreamFileOption(intPtr);
			}
			return railStreamFile;
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x00055068 File Offset: 0x00053268
		public virtual IRailStreamFile OpenStreamFile(string filename, RailStreamFileOption option)
		{
			IntPtr intPtr = ((option == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailStreamFileOption__SWIG_0());
			if (option != null)
			{
				RailConverter.Csharp2Cpp(option, intPtr);
			}
			IRailStreamFile railStreamFile;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailStorageHelper_OpenStreamFile__SWIG_1(this.swigCPtr_, filename, intPtr);
				railStreamFile = ((intPtr2 == IntPtr.Zero) ? null : new IRailStreamFileImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailStreamFileOption(intPtr);
			}
			return railStreamFile;
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x000550D0 File Offset: 0x000532D0
		public virtual RailResult AsyncListStreamFiles(string contents, RailListStreamFileOption option, string user_data)
		{
			IntPtr intPtr = ((option == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailListStreamFileOption__SWIG_0());
			if (option != null)
			{
				RailConverter.Csharp2Cpp(option, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_AsyncListStreamFiles(this.swigCPtr_, contents, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailListStreamFileOption(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x00055124 File Offset: 0x00053324
		public virtual RailResult AsyncRenameStreamFile(string old_filename, string new_filename, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_AsyncRenameStreamFile(this.swigCPtr_, old_filename, new_filename, user_data);
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x00055134 File Offset: 0x00053334
		public virtual RailResult AsyncDeleteStreamFile(string filename, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_AsyncDeleteStreamFile(this.swigCPtr_, filename, user_data);
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x00055143 File Offset: 0x00053343
		public virtual uint GetRailFileEnabledOS(string filename)
		{
			return RAIL_API_PINVOKE.IRailStorageHelper_GetRailFileEnabledOS(this.swigCPtr_, filename);
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x00055151 File Offset: 0x00053351
		public virtual RailResult SetRailFileEnabledOS(string filename, EnumRailStorageFileEnabledOS sync_os)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_SetRailFileEnabledOS(this.swigCPtr_, filename, (int)sync_os);
		}
	}
}
