using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200042E RID: 1070
	public class RailSpaceWorkDescriptor
	{
		// Token: 0x04001033 RID: 4147
		public List<RailSpaceWorkVoteDetail> vote_details = new List<RailSpaceWorkVoteDetail>();

		// Token: 0x04001034 RID: 4148
		public string description;

		// Token: 0x04001035 RID: 4149
		public string preview_scaling_url;

		// Token: 0x04001036 RID: 4150
		public string recommendation_rate;

		// Token: 0x04001037 RID: 4151
		public string preview_url;

		// Token: 0x04001038 RID: 4152
		public SpaceWorkID id = new SpaceWorkID();

		// Token: 0x04001039 RID: 4153
		public uint create_time;

		// Token: 0x0400103A RID: 4154
		public string detail_url;

		// Token: 0x0400103B RID: 4155
		public List<RailID> uploader_ids = new List<RailID>();

		// Token: 0x0400103C RID: 4156
		public string name;
	}
}
