using System;

namespace rail
{
	// Token: 0x020002F1 RID: 753
	public class SplitAssetsFinished : EventBase
	{
		// Token: 0x04000AB5 RID: 2741
		public ulong source_asset;

		// Token: 0x04000AB6 RID: 2742
		public uint to_quantity;

		// Token: 0x04000AB7 RID: 2743
		public ulong new_asset_id;
	}
}
