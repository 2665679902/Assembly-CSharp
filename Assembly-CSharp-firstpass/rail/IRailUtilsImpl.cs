using System;

namespace rail
{
	// Token: 0x020002C7 RID: 711
	public class IRailUtilsImpl : RailObject, IRailUtils
	{
		// Token: 0x06002A6C RID: 10860 RVA: 0x00055DC0 File Offset: 0x00053FC0
		internal IRailUtilsImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x00055DD0 File Offset: 0x00053FD0
		~IRailUtilsImpl()
		{
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x00055DF8 File Offset: 0x00053FF8
		public virtual uint GetTimeCountSinceGameLaunch()
		{
			return RAIL_API_PINVOKE.IRailUtils_GetTimeCountSinceGameLaunch(this.swigCPtr_);
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x00055E05 File Offset: 0x00054005
		public virtual uint GetTimeCountSinceComputerLaunch()
		{
			return RAIL_API_PINVOKE.IRailUtils_GetTimeCountSinceComputerLaunch(this.swigCPtr_);
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x00055E12 File Offset: 0x00054012
		public virtual uint GetTimeFromServer()
		{
			return RAIL_API_PINVOKE.IRailUtils_GetTimeFromServer(this.swigCPtr_);
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x00055E1F File Offset: 0x0005401F
		public virtual RailResult AsyncGetImageData(string image_path, uint scale_to_width, uint scale_to_height, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailUtils_AsyncGetImageData(this.swigCPtr_, image_path, scale_to_width, scale_to_height, user_data);
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x00055E34 File Offset: 0x00054034
		public virtual void GetErrorString(RailResult result, out string error_string)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			try
			{
				RAIL_API_PINVOKE.IRailUtils_GetErrorString(this.swigCPtr_, (int)result, intPtr);
			}
			finally
			{
				error_string = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x00055E7C File Offset: 0x0005407C
		public virtual RailResult DirtyWordsFilter(string words, bool replace_sensitive, RailDirtyWordsCheckResult check_result)
		{
			IntPtr intPtr = ((check_result == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDirtyWordsCheckResult__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUtils_DirtyWordsFilter(this.swigCPtr_, words, replace_sensitive, intPtr);
			}
			finally
			{
				if (check_result != null)
				{
					RailConverter.Cpp2Csharp(intPtr, check_result);
				}
				RAIL_API_PINVOKE.delete_RailDirtyWordsCheckResult(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x00055ECC File Offset: 0x000540CC
		public virtual EnumRailPlatformType GetRailPlatformType()
		{
			return (EnumRailPlatformType)RAIL_API_PINVOKE.IRailUtils_GetRailPlatformType(this.swigCPtr_);
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x00055EDC File Offset: 0x000540DC
		public virtual RailResult GetLaunchAppParameters(EnumRailLaunchAppType app_type, out string parameter)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUtils_GetLaunchAppParameters(this.swigCPtr_, (int)app_type, intPtr);
			}
			finally
			{
				parameter = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x00055F24 File Offset: 0x00054124
		public virtual RailResult GetPlatformLanguageCode(out string language_code)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUtils_GetPlatformLanguageCode(this.swigCPtr_, intPtr);
			}
			finally
			{
				language_code = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x00055F6C File Offset: 0x0005416C
		public virtual RailResult SetWarningMessageCallback(RailWarningMessageCallbackFunction callback)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailUtils_SetWarningMessageCallback(this.swigCPtr_, callback);
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x00055F7C File Offset: 0x0005417C
		public virtual RailResult GetCountryCodeOfCurrentLoggedInIP(out string country_code)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUtils_GetCountryCodeOfCurrentLoggedInIP(this.swigCPtr_, intPtr);
			}
			finally
			{
				country_code = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}
	}
}
