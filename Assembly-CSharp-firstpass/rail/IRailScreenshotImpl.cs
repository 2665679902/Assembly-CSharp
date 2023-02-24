using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002BB RID: 699
	public class IRailScreenshotImpl : RailObject, IRailScreenshot, IRailComponent
	{
		// Token: 0x060029C7 RID: 10695 RVA: 0x00053FF6 File Offset: 0x000521F6
		internal IRailScreenshotImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x00054008 File Offset: 0x00052208
		~IRailScreenshotImpl()
		{
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x00054030 File Offset: 0x00052230
		public virtual bool SetLocation(string location)
		{
			return RAIL_API_PINVOKE.IRailScreenshot_SetLocation(this.swigCPtr_, location);
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x00054040 File Offset: 0x00052240
		public virtual bool SetUsers(List<RailID> users)
		{
			IntPtr intPtr = ((users == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (users != null)
			{
				RailConverter.Csharp2Cpp(users, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailScreenshot_SetUsers(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return flag;
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x00054090 File Offset: 0x00052290
		public virtual bool AssociatePublishedFiles(List<SpaceWorkID> work_files)
		{
			IntPtr intPtr = ((work_files == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0());
			if (work_files != null)
			{
				RailConverter.Csharp2Cpp(work_files, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailScreenshot_AssociatePublishedFiles(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(intPtr);
			}
			return flag;
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x000540E0 File Offset: 0x000522E0
		public virtual RailResult AsyncPublishScreenshot(string work_name, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailScreenshot_AsyncPublishScreenshot(this.swigCPtr_, work_name, user_data);
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x000540EF File Offset: 0x000522EF
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x000540FC File Offset: 0x000522FC
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
