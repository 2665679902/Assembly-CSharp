using System;

namespace rail
{
	// Token: 0x020002C3 RID: 707
	public class IRailTextInputHelperImpl : RailObject, IRailTextInputHelper
	{
		// Token: 0x06002A41 RID: 10817 RVA: 0x0005525F File Offset: 0x0005345F
		internal IRailTextInputHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x00055270 File Offset: 0x00053470
		~IRailTextInputHelperImpl()
		{
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x00055298 File Offset: 0x00053498
		public virtual RailResult ShowTextInputWindow(RailTextInputWindowOption options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailTextInputWindowOption__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailTextInputHelper_ShowTextInputWindow(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailTextInputWindowOption(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000552E8 File Offset: 0x000534E8
		public virtual void GetTextInputContent(out string content)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			try
			{
				RAIL_API_PINVOKE.IRailTextInputHelper_GetTextInputContent(this.swigCPtr_, intPtr);
			}
			finally
			{
				content = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x00055330 File Offset: 0x00053530
		public virtual RailResult HideTextInputWindow()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailTextInputHelper_HideTextInputWindow(this.swigCPtr_);
		}
	}
}
