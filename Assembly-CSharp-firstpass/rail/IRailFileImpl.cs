using System;

namespace rail
{
	// Token: 0x020002A0 RID: 672
	public class IRailFileImpl : RailObject, IRailFile, IRailComponent
	{
		// Token: 0x0600286B RID: 10347 RVA: 0x0005050E File Offset: 0x0004E70E
		internal IRailFileImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x00050520 File Offset: 0x0004E720
		~IRailFileImpl()
		{
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x00050548 File Offset: 0x0004E748
		public virtual string GetFilename()
		{
			return UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.IRailFile_GetFilename(this.swigCPtr_));
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x0005055A File Offset: 0x0004E75A
		public virtual uint Read(byte[] buff, uint bytes_to_read, out RailResult result)
		{
			return RAIL_API_PINVOKE.IRailFile_Read__SWIG_0(this.swigCPtr_, buff, bytes_to_read, out result);
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x0005056A File Offset: 0x0004E76A
		public virtual uint Read(byte[] buff, uint bytes_to_read)
		{
			return RAIL_API_PINVOKE.IRailFile_Read__SWIG_1(this.swigCPtr_, buff, bytes_to_read);
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x00050579 File Offset: 0x0004E779
		public virtual uint Write(byte[] buff, uint bytes_to_write, out RailResult result)
		{
			return RAIL_API_PINVOKE.IRailFile_Write__SWIG_0(this.swigCPtr_, buff, bytes_to_write, out result);
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x00050589 File Offset: 0x0004E789
		public virtual uint Write(byte[] buff, uint bytes_to_write)
		{
			return RAIL_API_PINVOKE.IRailFile_Write__SWIG_1(this.swigCPtr_, buff, bytes_to_write);
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x00050598 File Offset: 0x0004E798
		public virtual RailResult AsyncRead(uint bytes_to_read, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFile_AsyncRead(this.swigCPtr_, bytes_to_read, user_data);
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000505A7 File Offset: 0x0004E7A7
		public virtual RailResult AsyncWrite(byte[] buffer, uint bytes_to_write, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFile_AsyncWrite(this.swigCPtr_, buffer, bytes_to_write, user_data);
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000505B7 File Offset: 0x0004E7B7
		public virtual uint GetSize()
		{
			return RAIL_API_PINVOKE.IRailFile_GetSize(this.swigCPtr_);
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000505C4 File Offset: 0x0004E7C4
		public virtual void Close()
		{
			RAIL_API_PINVOKE.IRailFile_Close(this.swigCPtr_);
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x000505D1 File Offset: 0x0004E7D1
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x000505DE File Offset: 0x0004E7DE
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
