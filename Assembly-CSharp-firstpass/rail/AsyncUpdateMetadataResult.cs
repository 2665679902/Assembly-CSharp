using System;

namespace rail
{
	// Token: 0x0200041E RID: 1054
	public class AsyncUpdateMetadataResult : EventBase
	{
		// Token: 0x04000FEE RID: 4078
		public EnumRailSpaceWorkType type;

		// Token: 0x04000FEF RID: 4079
		public SpaceWorkID id = new SpaceWorkID();
	}
}
