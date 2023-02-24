using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003FF RID: 1023
	public class RailPublishFileToUserSpaceOption
	{
		// Token: 0x04000FA8 RID: 4008
		public RailKeyValue key_value = new RailKeyValue();

		// Token: 0x04000FA9 RID: 4009
		public string description;

		// Token: 0x04000FAA RID: 4010
		public List<string> tags = new List<string>();

		// Token: 0x04000FAB RID: 4011
		public EnumRailSpaceWorkShareLevel level;

		// Token: 0x04000FAC RID: 4012
		public string version;

		// Token: 0x04000FAD RID: 4013
		public string preview_path_filename;

		// Token: 0x04000FAE RID: 4014
		public EnumRailSpaceWorkType type;

		// Token: 0x04000FAF RID: 4015
		public string space_work_name;
	}
}
