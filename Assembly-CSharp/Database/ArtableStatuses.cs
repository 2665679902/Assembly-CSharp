using System;

namespace Database
{
	// Token: 0x02000C74 RID: 3188
	public class ArtableStatuses : ResourceSet<ArtableStatusItem>
	{
		// Token: 0x06006514 RID: 25876 RVA: 0x0025C5A8 File Offset: 0x0025A7A8
		public ArtableStatuses(ResourceSet parent)
			: base("ArtableStatuses", parent)
		{
			this.AwaitingArting = this.Add("AwaitingArting", ArtableStatuses.ArtableStatusType.AwaitingArting);
			this.LookingUgly = this.Add("LookingUgly", ArtableStatuses.ArtableStatusType.LookingUgly);
			this.LookingOkay = this.Add("LookingOkay", ArtableStatuses.ArtableStatusType.LookingOkay);
			this.LookingGreat = this.Add("LookingGreat", ArtableStatuses.ArtableStatusType.LookingGreat);
		}

		// Token: 0x06006515 RID: 25877 RVA: 0x0025C60C File Offset: 0x0025A80C
		public ArtableStatusItem Add(string id, ArtableStatuses.ArtableStatusType statusType)
		{
			ArtableStatusItem artableStatusItem = new ArtableStatusItem(id, statusType);
			this.resources.Add(artableStatusItem);
			return artableStatusItem;
		}

		// Token: 0x04004605 RID: 17925
		public ArtableStatusItem AwaitingArting;

		// Token: 0x04004606 RID: 17926
		public ArtableStatusItem LookingUgly;

		// Token: 0x04004607 RID: 17927
		public ArtableStatusItem LookingOkay;

		// Token: 0x04004608 RID: 17928
		public ArtableStatusItem LookingGreat;

		// Token: 0x02001B0F RID: 6927
		public enum ArtableStatusType
		{
			// Token: 0x04007980 RID: 31104
			AwaitingArting,
			// Token: 0x04007981 RID: 31105
			LookingUgly,
			// Token: 0x04007982 RID: 31106
			LookingOkay,
			// Token: 0x04007983 RID: 31107
			LookingGreat
		}
	}
}
