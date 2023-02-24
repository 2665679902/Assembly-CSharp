using System;

namespace rail
{
	// Token: 0x020002AD RID: 685
	public class IRailIMEHelperImpl : RailObject, IRailIMEHelper
	{
		// Token: 0x0600291F RID: 10527 RVA: 0x0005207B File Offset: 0x0005027B
		internal IRailIMEHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x0005208C File Offset: 0x0005028C
		~IRailIMEHelperImpl()
		{
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000520B4 File Offset: 0x000502B4
		public virtual RailResult EnableIMEHelperTextInputWindow(bool enable, RailTextInputImeWindowOption option)
		{
			IntPtr intPtr = ((option == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailTextInputImeWindowOption__SWIG_0());
			if (option != null)
			{
				RailConverter.Csharp2Cpp(option, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailIMEHelper_EnableIMEHelperTextInputWindow(this.swigCPtr_, enable, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailTextInputImeWindowOption(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x00052104 File Offset: 0x00050304
		public virtual RailResult UpdateIMEHelperTextInputWindowPosition(RailWindowPosition position)
		{
			IntPtr intPtr = ((position == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailWindowPosition__SWIG_0());
			if (position != null)
			{
				RailConverter.Csharp2Cpp(position, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailIMEHelper_UpdateIMEHelperTextInputWindowPosition(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailWindowPosition(intPtr);
			}
			return railResult;
		}
	}
}
