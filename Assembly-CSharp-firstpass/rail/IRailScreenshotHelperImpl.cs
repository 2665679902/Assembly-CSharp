using System;

namespace rail
{
	// Token: 0x020002BC RID: 700
	public class IRailScreenshotHelperImpl : RailObject, IRailScreenshotHelper
	{
		// Token: 0x060029CF RID: 10703 RVA: 0x00054109 File Offset: 0x00052309
		internal IRailScreenshotHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x00054118 File Offset: 0x00052318
		~IRailScreenshotHelperImpl()
		{
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x00054140 File Offset: 0x00052340
		public virtual IRailScreenshot CreateScreenshotWithRawData(byte[] rgb_data, uint len, uint width, uint height)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailScreenshotHelper_CreateScreenshotWithRawData(this.swigCPtr_, rgb_data, len, width, height);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailScreenshotImpl(intPtr);
			}
			return null;
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x00054174 File Offset: 0x00052374
		public virtual IRailScreenshot CreateScreenshotWithLocalImage(string image_file, string thumbnail_file)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailScreenshotHelper_CreateScreenshotWithLocalImage(this.swigCPtr_, image_file, thumbnail_file);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailScreenshotImpl(intPtr);
			}
			return null;
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x000541A4 File Offset: 0x000523A4
		public virtual void AsyncTakeScreenshot(string user_data)
		{
			RAIL_API_PINVOKE.IRailScreenshotHelper_AsyncTakeScreenshot(this.swigCPtr_, user_data);
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x000541B2 File Offset: 0x000523B2
		public virtual void HookScreenshotHotKey(bool hook)
		{
			RAIL_API_PINVOKE.IRailScreenshotHelper_HookScreenshotHotKey(this.swigCPtr_, hook);
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x000541C0 File Offset: 0x000523C0
		public virtual bool IsScreenshotHotKeyHooked()
		{
			return RAIL_API_PINVOKE.IRailScreenshotHelper_IsScreenshotHotKeyHooked(this.swigCPtr_);
		}
	}
}
