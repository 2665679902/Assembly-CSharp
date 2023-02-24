using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000450 RID: 1104
	public class RailGameSettingMetadataChanged : EventBase
	{
		// Token: 0x04001099 RID: 4249
		public List<RailKeyValue> key_values = new List<RailKeyValue>();

		// Token: 0x0400109A RID: 4250
		public RailGameSettingMetadataChangedSource source;
	}
}
