using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002DE RID: 734
	public interface IRailAssets : IRailComponent
	{
		// Token: 0x06002D8F RID: 11663
		RailResult AsyncRequestAllAssets(string user_data);

		// Token: 0x06002D90 RID: 11664
		RailResult QueryAssetInfo(ulong asset_id, RailAssetInfo asset_info);

		// Token: 0x06002D91 RID: 11665
		RailResult AsyncUpdateAssetsProperty(List<RailAssetProperty> asset_property_list, string user_data);

		// Token: 0x06002D92 RID: 11666
		RailResult AsyncDirectConsumeAssets(List<RailAssetItem> assets, string user_data);

		// Token: 0x06002D93 RID: 11667
		RailResult AsyncStartConsumeAsset(ulong asset_id, string user_data);

		// Token: 0x06002D94 RID: 11668
		RailResult AsyncUpdateConsumeProgress(ulong asset_id, string progress, string user_data);

		// Token: 0x06002D95 RID: 11669
		RailResult AsyncCompleteConsumeAsset(ulong asset_id, uint quantity, string user_data);

		// Token: 0x06002D96 RID: 11670
		RailResult AsyncExchangeAssets(List<RailAssetItem> old_assets, RailProductItem to_product_info, string user_data);

		// Token: 0x06002D97 RID: 11671
		RailResult AsyncExchangeAssetsTo(List<RailAssetItem> old_assets, RailProductItem to_product_info, ulong add_to_exist_assets, string user_data);

		// Token: 0x06002D98 RID: 11672
		RailResult AsyncSplitAsset(ulong source_asset, uint to_quantity, string user_data);

		// Token: 0x06002D99 RID: 11673
		RailResult AsyncSplitAssetTo(ulong source_asset, uint to_quantity, ulong add_to_asset, string user_data);

		// Token: 0x06002D9A RID: 11674
		RailResult AsyncMergeAsset(List<RailAssetItem> source_assets, string user_data);

		// Token: 0x06002D9B RID: 11675
		RailResult AsyncMergeAssetTo(List<RailAssetItem> source_assets, ulong add_to_asset, string user_data);

		// Token: 0x06002D9C RID: 11676
		RailResult SerializeAssetsToBuffer(out string buffer);

		// Token: 0x06002D9D RID: 11677
		RailResult SerializeAssetsToBuffer(List<ulong> assets, out string buffer);

		// Token: 0x06002D9E RID: 11678
		RailResult DeserializeAssetsFromBuffer(RailID assets_owner, string buffer, List<RailAssetInfo> assets_info);
	}
}
