using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000C76 RID: 3190
	public class ArtableStage : PermitResource
	{
		// Token: 0x06006517 RID: 25879 RVA: 0x0025C664 File Offset: 0x0025A864
		public ArtableStage(string id, string name, string desc, PermitRarity rarity, string animFile, string anim, int decor_value, bool cheer_on_complete, ArtableStatusItem status_item, string prefabId, string symbolName = "")
			: base(id, name, desc, PermitCategory.Artwork, rarity)
		{
			this.id = id;
			this.animFile = animFile;
			this.anim = anim;
			this.symbolName = symbolName;
			this.decor = decor_value;
			this.cheerOnComplete = cheer_on_complete;
			this.statusItem = status_item;
			this.prefabId = prefabId;
		}

		// Token: 0x06006518 RID: 25880 RVA: 0x0025C6C0 File Offset: 0x0025A8C0
		public override PermitPresentationInfo GetPermitPresentationInfo()
		{
			PermitPresentationInfo permitPresentationInfo = default(PermitPresentationInfo);
			permitPresentationInfo.sprite = Def.GetUISpriteFromMultiObjectAnim(Assets.GetAnim(this.animFile), "ui", false, "");
			permitPresentationInfo.SetFacadeForText(UI.KLEI_INVENTORY_SCREEN.ARTABLE_ITEM_FACADE_FOR.Replace("{ConfigProperName}", Assets.GetPrefab(this.prefabId).GetProperName()).Replace("{ArtableQuality}", this.statusItem.GetName(null)));
			return permitPresentationInfo;
		}

		// Token: 0x0400460A RID: 17930
		public string id;

		// Token: 0x0400460B RID: 17931
		public string anim;

		// Token: 0x0400460C RID: 17932
		public string animFile;

		// Token: 0x0400460D RID: 17933
		public string prefabId;

		// Token: 0x0400460E RID: 17934
		public string symbolName;

		// Token: 0x0400460F RID: 17935
		public int decor;

		// Token: 0x04004610 RID: 17936
		public bool cheerOnComplete;

		// Token: 0x04004611 RID: 17937
		public ArtableStatusItem statusItem;
	}
}
