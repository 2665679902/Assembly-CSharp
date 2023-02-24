using System;

namespace rail
{
	// Token: 0x020002C1 RID: 705
	public class IRailStreamFileImpl : RailObject, IRailStreamFile, IRailComponent
	{
		// Token: 0x06002A33 RID: 10803 RVA: 0x00055160 File Offset: 0x00053360
		internal IRailStreamFileImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x00055170 File Offset: 0x00053370
		~IRailStreamFileImpl()
		{
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x00055198 File Offset: 0x00053398
		public virtual string GetFilename()
		{
			return UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.IRailStreamFile_GetFilename(this.swigCPtr_));
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000551AA File Offset: 0x000533AA
		public virtual RailResult AsyncRead(int offset, uint bytes_to_read, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStreamFile_AsyncRead(this.swigCPtr_, offset, bytes_to_read, user_data);
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000551BA File Offset: 0x000533BA
		public virtual RailResult AsyncWrite(byte[] buff, uint bytes_to_write, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStreamFile_AsyncWrite(this.swigCPtr_, buff, bytes_to_write, user_data);
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000551CA File Offset: 0x000533CA
		public virtual ulong GetSize()
		{
			return RAIL_API_PINVOKE.IRailStreamFile_GetSize(this.swigCPtr_);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000551D7 File Offset: 0x000533D7
		public virtual RailResult Close()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStreamFile_Close(this.swigCPtr_);
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000551E4 File Offset: 0x000533E4
		public virtual void Cancel()
		{
			RAIL_API_PINVOKE.IRailStreamFile_Cancel(this.swigCPtr_);
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000551F1 File Offset: 0x000533F1
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000551FE File Offset: 0x000533FE
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
