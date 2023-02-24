using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200046D RID: 1133
	public interface IRailZoneServer : IRailComponent
	{
		// Token: 0x060030CC RID: 12492
		RailZoneID GetZoneID();

		// Token: 0x060030CD RID: 12493
		RailResult GetZoneNameLanguages(List<string> languages);

		// Token: 0x060030CE RID: 12494
		RailResult GetZoneName(string language_filter, out string zone_name);

		// Token: 0x060030CF RID: 12495
		RailResult GetZoneDescriptionLanguages(List<string> languages);

		// Token: 0x060030D0 RID: 12496
		RailResult GetZoneDescription(string language_filter, out string zone_description);

		// Token: 0x060030D1 RID: 12497
		RailResult GetGameServerAddresses(List<string> server_addresses);

		// Token: 0x060030D2 RID: 12498
		RailResult GetZoneMetadatas(List<RailKeyValue> key_values);

		// Token: 0x060030D3 RID: 12499
		RailResult GetChildrenZoneIDs(List<RailZoneID> zone_ids);

		// Token: 0x060030D4 RID: 12500
		bool IsZoneVisiable();

		// Token: 0x060030D5 RID: 12501
		bool IsZoneJoinable();

		// Token: 0x060030D6 RID: 12502
		uint GetZoneEnableStartTime();

		// Token: 0x060030D7 RID: 12503
		uint GetZoneEnableEndTime();
	}
}
