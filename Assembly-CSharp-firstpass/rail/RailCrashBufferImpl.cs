using System;

namespace rail
{
	// Token: 0x020002CC RID: 716
	public class RailCrashBufferImpl : RailObject, RailCrashBuffer
	{
		// Token: 0x06002AB9 RID: 10937 RVA: 0x000569C0 File Offset: 0x00054BC0
		internal RailCrashBufferImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000569D0 File Offset: 0x00054BD0
		~RailCrashBufferImpl()
		{
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x000569F8 File Offset: 0x00054BF8
		public virtual string GetData()
		{
			return UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailCrashBuffer_GetData(this.swigCPtr_));
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x00056A0A File Offset: 0x00054C0A
		public virtual uint GetBufferLength()
		{
			return RAIL_API_PINVOKE.RailCrashBuffer_GetBufferLength(this.swigCPtr_);
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x00056A17 File Offset: 0x00054C17
		public virtual uint GetValidLength()
		{
			return RAIL_API_PINVOKE.RailCrashBuffer_GetValidLength(this.swigCPtr_);
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x00056A24 File Offset: 0x00054C24
		public virtual uint SetData(string data, uint length, uint offset)
		{
			return RAIL_API_PINVOKE.RailCrashBuffer_SetData__SWIG_0(this.swigCPtr_, data, length, offset);
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x00056A34 File Offset: 0x00054C34
		public virtual uint SetData(string data, uint length)
		{
			return RAIL_API_PINVOKE.RailCrashBuffer_SetData__SWIG_1(this.swigCPtr_, data, length);
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x00056A43 File Offset: 0x00054C43
		public virtual uint AppendData(string data, uint length)
		{
			return RAIL_API_PINVOKE.RailCrashBuffer_AppendData(this.swigCPtr_, data, length);
		}
	}
}
