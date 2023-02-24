using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003DF RID: 991
	public interface IRailScreenshot : IRailComponent
	{
		// Token: 0x06002F9B RID: 12187
		bool SetLocation(string location);

		// Token: 0x06002F9C RID: 12188
		bool SetUsers(List<RailID> users);

		// Token: 0x06002F9D RID: 12189
		bool AssociatePublishedFiles(List<SpaceWorkID> work_files);

		// Token: 0x06002F9E RID: 12190
		RailResult AsyncPublishScreenshot(string work_name, string user_data);
	}
}
