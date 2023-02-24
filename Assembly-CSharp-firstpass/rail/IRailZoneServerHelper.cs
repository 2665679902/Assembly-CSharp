using System;

namespace rail
{
	// Token: 0x0200046E RID: 1134
	public interface IRailZoneServerHelper
	{
		// Token: 0x060030D8 RID: 12504
		RailZoneID GetPlayerSelectedZoneID();

		// Token: 0x060030D9 RID: 12505
		RailZoneID GetRootZoneID();

		// Token: 0x060030DA RID: 12506
		IRailZoneServer OpenZoneServer(RailZoneID zone_id, out RailResult result);

		// Token: 0x060030DB RID: 12507
		RailResult AsyncSwitchPlayerSelectedZone(RailZoneID zone_id);
	}
}
