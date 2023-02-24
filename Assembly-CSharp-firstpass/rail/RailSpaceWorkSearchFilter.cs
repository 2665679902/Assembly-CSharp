using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000430 RID: 1072
	public class RailSpaceWorkSearchFilter
	{
		// Token: 0x04001042 RID: 4162
		public string search_text;

		// Token: 0x04001043 RID: 4163
		public bool match_all_required_tags;

		// Token: 0x04001044 RID: 4164
		public List<RailKeyValue> required_metadata = new List<RailKeyValue>();

		// Token: 0x04001045 RID: 4165
		public bool match_all_required_metadata;

		// Token: 0x04001046 RID: 4166
		public List<string> required_tags = new List<string>();

		// Token: 0x04001047 RID: 4167
		public List<RailKeyValue> excluded_metadata = new List<RailKeyValue>();

		// Token: 0x04001048 RID: 4168
		public List<string> excluded_tags = new List<string>();
	}
}
