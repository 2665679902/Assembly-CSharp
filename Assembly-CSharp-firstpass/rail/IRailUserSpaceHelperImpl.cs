using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002C6 RID: 710
	public class IRailUserSpaceHelperImpl : RailObject, IRailUserSpaceHelper
	{
		// Token: 0x06002A55 RID: 10837 RVA: 0x000556B4 File Offset: 0x000538B4
		internal IRailUserSpaceHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000556C4 File Offset: 0x000538C4
		~IRailUserSpaceHelperImpl()
		{
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x000556EC File Offset: 0x000538EC
		public virtual RailResult AsyncGetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options, string user_data)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_0(this.swigCPtr_, offset, max_works, (int)type, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x00055744 File Offset: 0x00053944
		public virtual RailResult AsyncGetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_1(this.swigCPtr_, offset, max_works, (int)type, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x0005579C File Offset: 0x0005399C
		public virtual RailResult AsyncGetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_2(this.swigCPtr_, offset, max_works, (int)type);
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000557AC File Offset: 0x000539AC
		public virtual RailResult AsyncGetMyFavoritesWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options, string user_data)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_0(this.swigCPtr_, offset, max_works, (int)type, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x00055804 File Offset: 0x00053A04
		public virtual RailResult AsyncGetMyFavoritesWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_1(this.swigCPtr_, offset, max_works, (int)type, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x0005585C File Offset: 0x00053A5C
		public virtual RailResult AsyncGetMyFavoritesWorks(uint offset, uint max_works, EnumRailSpaceWorkType type)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_2(this.swigCPtr_, offset, max_works, (int)type);
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x0005586C File Offset: 0x00053A6C
		public virtual RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works, EnumRailSpaceWorkOrderBy order_by, RailQueryWorkFileOptions options, string user_data)
		{
			IntPtr intPtr = ((filter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkFilter__SWIG_0());
			if (filter != null)
			{
				RailConverter.Csharp2Cpp(filter, intPtr);
			}
			IntPtr intPtr2 = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_0(this.swigCPtr_, intPtr, offset, max_works, (int)order_by, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSpaceWorkFilter(intPtr);
				RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x000558E8 File Offset: 0x00053AE8
		public virtual RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works, EnumRailSpaceWorkOrderBy order_by, RailQueryWorkFileOptions options)
		{
			IntPtr intPtr = ((filter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkFilter__SWIG_0());
			if (filter != null)
			{
				RailConverter.Csharp2Cpp(filter, intPtr);
			}
			IntPtr intPtr2 = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_1(this.swigCPtr_, intPtr, offset, max_works, (int)order_by, intPtr2);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSpaceWorkFilter(intPtr);
				RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x00055960 File Offset: 0x00053B60
		public virtual RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works, EnumRailSpaceWorkOrderBy order_by)
		{
			IntPtr intPtr = ((filter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkFilter__SWIG_0());
			if (filter != null)
			{
				RailConverter.Csharp2Cpp(filter, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_2(this.swigCPtr_, intPtr, offset, max_works, (int)order_by);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSpaceWorkFilter(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x000559B4 File Offset: 0x00053BB4
		public virtual RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works)
		{
			IntPtr intPtr = ((filter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkFilter__SWIG_0());
			if (filter != null)
			{
				RailConverter.Csharp2Cpp(filter, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_3(this.swigCPtr_, intPtr, offset, max_works);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSpaceWorkFilter(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x00055A08 File Offset: 0x00053C08
		public virtual RailResult AsyncSubscribeSpaceWorks(List<SpaceWorkID> ids, bool subscribe, string user_data)
		{
			IntPtr intPtr = ((ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0());
			if (ids != null)
			{
				RailConverter.Csharp2Cpp(ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncSubscribeSpaceWorks(this.swigCPtr_, intPtr, subscribe, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x00055A5C File Offset: 0x00053C5C
		public virtual IRailSpaceWork OpenSpaceWork(SpaceWorkID id)
		{
			IntPtr intPtr = ((id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0());
			if (id != null)
			{
				RailConverter.Csharp2Cpp(id, intPtr);
			}
			IRailSpaceWork railSpaceWork;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailUserSpaceHelper_OpenSpaceWork(this.swigCPtr_, intPtr);
				railSpaceWork = ((intPtr2 == IntPtr.Zero) ? null : new IRailSpaceWorkImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_SpaceWorkID(intPtr);
			}
			return railSpaceWork;
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x00055AD0 File Offset: 0x00053CD0
		public virtual IRailSpaceWork CreateSpaceWork(EnumRailSpaceWorkType type)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailUserSpaceHelper_CreateSpaceWork(this.swigCPtr_, (int)type);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailSpaceWorkImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x00055B00 File Offset: 0x00053D00
		public virtual RailResult GetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, QueryMySubscribedSpaceWorksResult result)
		{
			IntPtr intPtr = ((result == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_QueryMySubscribedSpaceWorksResult__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_GetMySubscribedWorks(this.swigCPtr_, offset, max_works, (int)type, intPtr);
			}
			finally
			{
				if (result != null)
				{
					RailConverter.Cpp2Csharp(intPtr, result);
				}
				RAIL_API_PINVOKE.delete_QueryMySubscribedSpaceWorksResult(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x00055B54 File Offset: 0x00053D54
		public virtual uint GetMySubscribedWorksCount(EnumRailSpaceWorkType type, out RailResult result)
		{
			return RAIL_API_PINVOKE.IRailUserSpaceHelper_GetMySubscribedWorksCount(this.swigCPtr_, (int)type, out result);
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x00055B64 File Offset: 0x00053D64
		public virtual RailResult AsyncRemoveSpaceWork(SpaceWorkID id, string user_data)
		{
			IntPtr intPtr = ((id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0());
			if (id != null)
			{
				RailConverter.Csharp2Cpp(id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncRemoveSpaceWork(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_SpaceWorkID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x00055BC0 File Offset: 0x00053DC0
		public virtual RailResult AsyncModifyFavoritesWorks(List<SpaceWorkID> ids, EnumRailModifyFavoritesSpaceWorkType modify_flag, string user_data)
		{
			IntPtr intPtr = ((ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0());
			if (ids != null)
			{
				RailConverter.Csharp2Cpp(ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncModifyFavoritesWorks(this.swigCPtr_, intPtr, (int)modify_flag, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x00055C14 File Offset: 0x00053E14
		public virtual RailResult AsyncVoteSpaceWork(SpaceWorkID id, EnumRailSpaceWorkVoteValue vote, string user_data)
		{
			IntPtr intPtr = ((id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0());
			if (id != null)
			{
				RailConverter.Csharp2Cpp(id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncVoteSpaceWork(this.swigCPtr_, intPtr, (int)vote, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_SpaceWorkID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x00055C74 File Offset: 0x00053E74
		public virtual RailResult AsyncSearchSpaceWork(RailSpaceWorkSearchFilter filter, RailQueryWorkFileOptions options, List<EnumRailSpaceWorkType> types, uint offset, uint max_works, EnumRailSpaceWorkOrderBy order_by, string user_data)
		{
			IntPtr intPtr = ((filter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkSearchFilter__SWIG_0());
			if (filter != null)
			{
				RailConverter.Csharp2Cpp(filter, intPtr);
			}
			IntPtr intPtr2 = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr2);
			}
			IntPtr intPtr3 = ((types == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayEnumRailSpaceWorkType__SWIG_0());
			if (types != null)
			{
				RailConverter.Csharp2Cpp(types, intPtr3);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncSearchSpaceWork(this.swigCPtr_, intPtr, intPtr2, intPtr3, offset, max_works, (int)order_by, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSpaceWorkSearchFilter(intPtr);
				RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(intPtr2);
				RAIL_API_PINVOKE.delete_RailArrayEnumRailSpaceWorkType(intPtr3);
			}
			return railResult;
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x00055D10 File Offset: 0x00053F10
		public virtual RailResult AsyncRateSpaceWork(SpaceWorkID id, EnumRailSpaceWorkRateValue mark, string user_data)
		{
			IntPtr intPtr = ((id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0());
			if (id != null)
			{
				RailConverter.Csharp2Cpp(id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncRateSpaceWork(this.swigCPtr_, intPtr, (int)mark, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_SpaceWorkID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x00055D70 File Offset: 0x00053F70
		public virtual RailResult AsyncQuerySpaceWorksInfo(List<SpaceWorkID> ids, string user_data)
		{
			IntPtr intPtr = ((ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0());
			if (ids != null)
			{
				RailConverter.Csharp2Cpp(ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncQuerySpaceWorksInfo(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(intPtr);
			}
			return railResult;
		}
	}
}
