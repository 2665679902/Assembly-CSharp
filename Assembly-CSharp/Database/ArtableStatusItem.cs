using System;

namespace Database
{
	// Token: 0x02000C75 RID: 3189
	public class ArtableStatusItem : StatusItem
	{
		// Token: 0x06006516 RID: 25878 RVA: 0x0025C630 File Offset: 0x0025A830
		public ArtableStatusItem(string id, ArtableStatuses.ArtableStatusType statusType)
			: base(id, "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null)
		{
			this.StatusType = statusType;
		}

		// Token: 0x04004609 RID: 17929
		public ArtableStatuses.ArtableStatusType StatusType;
	}
}
