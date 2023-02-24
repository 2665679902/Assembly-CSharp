using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000414 RID: 1044
	public interface IRailUserSpaceHelper
	{
		// Token: 0x06003040 RID: 12352
		RailResult AsyncGetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options, string user_data);

		// Token: 0x06003041 RID: 12353
		RailResult AsyncGetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options);

		// Token: 0x06003042 RID: 12354
		RailResult AsyncGetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type);

		// Token: 0x06003043 RID: 12355
		RailResult AsyncGetMyFavoritesWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options, string user_data);

		// Token: 0x06003044 RID: 12356
		RailResult AsyncGetMyFavoritesWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options);

		// Token: 0x06003045 RID: 12357
		RailResult AsyncGetMyFavoritesWorks(uint offset, uint max_works, EnumRailSpaceWorkType type);

		// Token: 0x06003046 RID: 12358
		RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works, EnumRailSpaceWorkOrderBy order_by, RailQueryWorkFileOptions options, string user_data);

		// Token: 0x06003047 RID: 12359
		RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works, EnumRailSpaceWorkOrderBy order_by, RailQueryWorkFileOptions options);

		// Token: 0x06003048 RID: 12360
		RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works, EnumRailSpaceWorkOrderBy order_by);

		// Token: 0x06003049 RID: 12361
		RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works);

		// Token: 0x0600304A RID: 12362
		RailResult AsyncSubscribeSpaceWorks(List<SpaceWorkID> ids, bool subscribe, string user_data);

		// Token: 0x0600304B RID: 12363
		IRailSpaceWork OpenSpaceWork(SpaceWorkID id);

		// Token: 0x0600304C RID: 12364
		IRailSpaceWork CreateSpaceWork(EnumRailSpaceWorkType type);

		// Token: 0x0600304D RID: 12365
		RailResult GetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, QueryMySubscribedSpaceWorksResult result);

		// Token: 0x0600304E RID: 12366
		uint GetMySubscribedWorksCount(EnumRailSpaceWorkType type, out RailResult result);

		// Token: 0x0600304F RID: 12367
		RailResult AsyncRemoveSpaceWork(SpaceWorkID id, string user_data);

		// Token: 0x06003050 RID: 12368
		RailResult AsyncModifyFavoritesWorks(List<SpaceWorkID> ids, EnumRailModifyFavoritesSpaceWorkType modify_flag, string user_data);

		// Token: 0x06003051 RID: 12369
		RailResult AsyncVoteSpaceWork(SpaceWorkID id, EnumRailSpaceWorkVoteValue vote, string user_data);

		// Token: 0x06003052 RID: 12370
		RailResult AsyncSearchSpaceWork(RailSpaceWorkSearchFilter filter, RailQueryWorkFileOptions options, List<EnumRailSpaceWorkType> types, uint offset, uint max_works, EnumRailSpaceWorkOrderBy order_by, string user_data);

		// Token: 0x06003053 RID: 12371
		RailResult AsyncRateSpaceWork(SpaceWorkID id, EnumRailSpaceWorkRateValue mark, string user_data);

		// Token: 0x06003054 RID: 12372
		RailResult AsyncQuerySpaceWorksInfo(List<SpaceWorkID> ids, string user_data);
	}
}
