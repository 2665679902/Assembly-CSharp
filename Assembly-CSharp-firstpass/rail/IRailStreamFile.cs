using System;

namespace rail
{
	// Token: 0x020003F3 RID: 1011
	public interface IRailStreamFile : IRailComponent
	{
		// Token: 0x06002FE3 RID: 12259
		string GetFilename();

		// Token: 0x06002FE4 RID: 12260
		RailResult AsyncRead(int offset, uint bytes_to_read, string user_data);

		// Token: 0x06002FE5 RID: 12261
		RailResult AsyncWrite(byte[] buff, uint bytes_to_write, string user_data);

		// Token: 0x06002FE6 RID: 12262
		ulong GetSize();

		// Token: 0x06002FE7 RID: 12263
		RailResult Close();

		// Token: 0x06002FE8 RID: 12264
		void Cancel();
	}
}
