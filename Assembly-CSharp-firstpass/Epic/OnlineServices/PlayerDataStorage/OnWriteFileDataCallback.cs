using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006B9 RID: 1721
	// (Invoke) Token: 0x0600419D RID: 16797
	public delegate WriteResult OnWriteFileDataCallback(WriteFileDataCallbackInfo data, out byte[] outDataBuffer, out uint outDataWritten);
}
