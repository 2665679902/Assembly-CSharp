using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004AD RID: 1197
public class ManagementMenuNotification : Notification
{
	// Token: 0x1700010D RID: 269
	// (get) Token: 0x06001B39 RID: 6969 RVA: 0x000911E9 File Offset: 0x0008F3E9
	// (set) Token: 0x06001B3A RID: 6970 RVA: 0x000911F1 File Offset: 0x0008F3F1
	public bool hasBeenViewed { get; private set; }

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x06001B3B RID: 6971 RVA: 0x000911FA File Offset: 0x0008F3FA
	// (set) Token: 0x06001B3C RID: 6972 RVA: 0x00091202 File Offset: 0x0008F402
	public string highlightTarget { get; set; }

	// Token: 0x06001B3D RID: 6973 RVA: 0x0009120C File Offset: 0x0008F40C
	public ManagementMenuNotification(global::Action targetMenu, NotificationValence valence, string highlightTarget, string title, NotificationType type, Func<List<Notification>, object, string> tooltip = null, object tooltip_data = null, bool expires = true, float delay = 0f, Notification.ClickCallback custom_click_callback = null, object custom_click_data = null, Transform click_focus = null, bool volume_attenuation = true)
		: base(title, type, tooltip, tooltip_data, expires, delay, custom_click_callback, custom_click_data, click_focus, volume_attenuation, false, false)
	{
		this.targetMenu = targetMenu;
		this.valence = valence;
		this.highlightTarget = highlightTarget;
	}

	// Token: 0x06001B3E RID: 6974 RVA: 0x0009124A File Offset: 0x0008F44A
	public void View()
	{
		this.hasBeenViewed = true;
		ManagementMenu.Instance.notificationDisplayer.NotificationWasViewed(this);
	}

	// Token: 0x04000F2E RID: 3886
	public global::Action targetMenu;

	// Token: 0x04000F2F RID: 3887
	public NotificationValence valence;
}
