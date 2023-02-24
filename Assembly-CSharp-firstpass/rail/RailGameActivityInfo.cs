using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000376 RID: 886
	public class RailGameActivityInfo
	{
		// Token: 0x04000CED RID: 3309
		public ulong activity_id;

		// Token: 0x04000CEE RID: 3310
		public List<RailKeyValue> metadata_key_values = new List<RailKeyValue>();

		// Token: 0x04000CEF RID: 3311
		public uint end_time;

		// Token: 0x04000CF0 RID: 3312
		public uint begin_time;

		// Token: 0x04000CF1 RID: 3313
		public string activity_name;

		// Token: 0x04000CF2 RID: 3314
		public string activity_description;
	}
}
