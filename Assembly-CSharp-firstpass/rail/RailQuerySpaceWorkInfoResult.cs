using System;

namespace rail
{
	// Token: 0x0200042C RID: 1068
	public class RailQuerySpaceWorkInfoResult
	{
		// Token: 0x04001029 RID: 4137
		public RailSpaceWorkDescriptor spacework_descriptor = new RailSpaceWorkDescriptor();

		// Token: 0x0400102A RID: 4138
		public RailResult error_code;

		// Token: 0x0400102B RID: 4139
		public SpaceWorkID id = new SpaceWorkID();
	}
}
