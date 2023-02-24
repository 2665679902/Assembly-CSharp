using System;

namespace rail
{
	// Token: 0x020002CF RID: 719
	public interface IRailComponent
	{
		// Token: 0x06002AC8 RID: 10952
		ulong GetComponentVersion();

		// Token: 0x06002AC9 RID: 10953
		void Release();
	}
}
