using System;

namespace rail
{
	// Token: 0x02000434 RID: 1076
	public class RailUserSpaceDownloadProgress
	{
		// Token: 0x04001055 RID: 4181
		public uint progress;

		// Token: 0x04001056 RID: 4182
		public ulong total;

		// Token: 0x04001057 RID: 4183
		public uint speed;

		// Token: 0x04001058 RID: 4184
		public SpaceWorkID id = new SpaceWorkID();

		// Token: 0x04001059 RID: 4185
		public ulong finidshed;
	}
}
