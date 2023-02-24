using System;

// Token: 0x02000990 RID: 2448
public class MaterialsStatusItem : StatusItem
{
	// Token: 0x06004877 RID: 18551 RVA: 0x001965FC File Offset: 0x001947FC
	public MaterialsStatusItem(string id, string prefix, string icon, StatusItem.IconType icon_type, NotificationType notification_type, bool allow_multiples, HashedString overlay)
		: base(id, prefix, icon, icon_type, notification_type, allow_multiples, overlay, true, 129022, null)
	{
	}
}
