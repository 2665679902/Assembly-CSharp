using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200029E RID: 670
	public class IRailDlcHelperImpl : RailObject, IRailDlcHelper
	{
		// Token: 0x0600283D RID: 10301 RVA: 0x0004FBCA File Offset: 0x0004DDCA
		internal IRailDlcHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x0004FBDC File Offset: 0x0004DDDC
		~IRailDlcHelperImpl()
		{
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x0004FC04 File Offset: 0x0004DE04
		public virtual RailResult AsyncQueryIsOwnedDlcsOnServer(List<RailDlcID> dlc_ids, string user_data)
		{
			IntPtr intPtr = ((dlc_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailDlcID__SWIG_0());
			if (dlc_ids != null)
			{
				RailConverter.Csharp2Cpp(dlc_ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailDlcHelper_AsyncQueryIsOwnedDlcsOnServer(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailDlcID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x0004FC54 File Offset: 0x0004DE54
		public virtual RailResult AsyncCheckAllDlcsStateReady(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailDlcHelper_AsyncCheckAllDlcsStateReady(this.swigCPtr_, user_data);
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x0004FC64 File Offset: 0x0004DE64
		public virtual bool IsDlcInstalled(RailDlcID dlc_id, out string installed_path)
		{
			IntPtr intPtr = ((dlc_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcID__SWIG_0());
			if (dlc_id != null)
			{
				RailConverter.Csharp2Cpp(dlc_id, intPtr);
			}
			IntPtr intPtr2 = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailDlcHelper_IsDlcInstalled__SWIG_0(this.swigCPtr_, intPtr, intPtr2);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailDlcID(intPtr);
				installed_path = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr2));
				RAIL_API_PINVOKE.delete_RailString(intPtr2);
			}
			return flag;
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x0004FCD8 File Offset: 0x0004DED8
		public virtual bool IsDlcInstalled(RailDlcID dlc_id)
		{
			IntPtr intPtr = ((dlc_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcID__SWIG_0());
			if (dlc_id != null)
			{
				RailConverter.Csharp2Cpp(dlc_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailDlcHelper_IsDlcInstalled__SWIG_1(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailDlcID(intPtr);
			}
			return flag;
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x0004FD34 File Offset: 0x0004DF34
		public virtual bool IsOwnedDlc(RailDlcID dlc_id)
		{
			IntPtr intPtr = ((dlc_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcID__SWIG_0());
			if (dlc_id != null)
			{
				RailConverter.Csharp2Cpp(dlc_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailDlcHelper_IsOwnedDlc(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailDlcID(intPtr);
			}
			return flag;
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x0004FD90 File Offset: 0x0004DF90
		public virtual uint GetDlcCount()
		{
			return RAIL_API_PINVOKE.IRailDlcHelper_GetDlcCount(this.swigCPtr_);
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x0004FDA0 File Offset: 0x0004DFA0
		public virtual bool GetDlcInfo(uint index, RailDlcInfo dlc_info)
		{
			IntPtr intPtr = ((dlc_info == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcInfo__SWIG_0());
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailDlcHelper_GetDlcInfo(this.swigCPtr_, index, intPtr);
			}
			finally
			{
				if (dlc_info != null)
				{
					RailConverter.Cpp2Csharp(intPtr, dlc_info);
				}
				RAIL_API_PINVOKE.delete_RailDlcInfo(intPtr);
			}
			return flag;
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x0004FDF0 File Offset: 0x0004DFF0
		public virtual bool AsyncInstallDlc(RailDlcID dlc_id, string user_data)
		{
			IntPtr intPtr = ((dlc_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcID__SWIG_0());
			if (dlc_id != null)
			{
				RailConverter.Csharp2Cpp(dlc_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailDlcHelper_AsyncInstallDlc(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailDlcID(intPtr);
			}
			return flag;
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x0004FE4C File Offset: 0x0004E04C
		public virtual bool AsyncRemoveDlc(RailDlcID dlc_id, string user_data)
		{
			IntPtr intPtr = ((dlc_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcID__SWIG_0());
			if (dlc_id != null)
			{
				RailConverter.Csharp2Cpp(dlc_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailDlcHelper_AsyncRemoveDlc(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailDlcID(intPtr);
			}
			return flag;
		}
	}
}
