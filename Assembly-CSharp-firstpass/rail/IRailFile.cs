using System;

namespace rail
{
	// Token: 0x020003F1 RID: 1009
	public interface IRailFile : IRailComponent
	{
		// Token: 0x06002FC3 RID: 12227
		string GetFilename();

		// Token: 0x06002FC4 RID: 12228
		uint Read(byte[] buff, uint bytes_to_read, out RailResult result);

		// Token: 0x06002FC5 RID: 12229
		uint Read(byte[] buff, uint bytes_to_read);

		// Token: 0x06002FC6 RID: 12230
		uint Write(byte[] buff, uint bytes_to_write, out RailResult result);

		// Token: 0x06002FC7 RID: 12231
		uint Write(byte[] buff, uint bytes_to_write);

		// Token: 0x06002FC8 RID: 12232
		RailResult AsyncRead(uint bytes_to_read, string user_data);

		// Token: 0x06002FC9 RID: 12233
		RailResult AsyncWrite(byte[] buffer, uint bytes_to_write, string user_data);

		// Token: 0x06002FCA RID: 12234
		uint GetSize();

		// Token: 0x06002FCB RID: 12235
		void Close();
	}
}
