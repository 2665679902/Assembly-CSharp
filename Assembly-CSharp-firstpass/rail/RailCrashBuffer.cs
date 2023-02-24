using System;

namespace rail
{
	// Token: 0x0200044D RID: 1101
	public interface RailCrashBuffer
	{
		// Token: 0x0600308E RID: 12430
		string GetData();

		// Token: 0x0600308F RID: 12431
		uint GetBufferLength();

		// Token: 0x06003090 RID: 12432
		uint GetValidLength();

		// Token: 0x06003091 RID: 12433
		uint SetData(string data, uint length, uint offset);

		// Token: 0x06003092 RID: 12434
		uint SetData(string data, uint length);

		// Token: 0x06003093 RID: 12435
		uint AppendData(string data, uint length);
	}
}
