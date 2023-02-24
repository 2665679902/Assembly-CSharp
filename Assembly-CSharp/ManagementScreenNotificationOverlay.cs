using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B42 RID: 2882
public class ManagementScreenNotificationOverlay : KMonoBehaviour
{
	// Token: 0x0600594D RID: 22861 RVA: 0x0020565C File Offset: 0x0020385C
	protected void OnEnable()
	{
	}

	// Token: 0x0600594E RID: 22862 RVA: 0x0020565E File Offset: 0x0020385E
	protected void OnDisable()
	{
	}

	// Token: 0x0600594F RID: 22863 RVA: 0x00205660 File Offset: 0x00203860
	private NotificationAlertBar CreateAlertBar(ManagementMenuNotification notification)
	{
		NotificationAlertBar notificationAlertBar = Util.KInstantiateUI<NotificationAlertBar>(this.alertBarPrefab.gameObject, this.alertContainer.gameObject, false);
		notificationAlertBar.Init(notification);
		notificationAlertBar.gameObject.SetActive(true);
		return notificationAlertBar;
	}

	// Token: 0x06005950 RID: 22864 RVA: 0x00205691 File Offset: 0x00203891
	private void NotificationsChanged()
	{
	}

	// Token: 0x04003C59 RID: 15449
	public global::Action currentMenu;

	// Token: 0x04003C5A RID: 15450
	public NotificationAlertBar alertBarPrefab;

	// Token: 0x04003C5B RID: 15451
	public RectTransform alertContainer;

	// Token: 0x04003C5C RID: 15452
	private List<NotificationAlertBar> alertBars = new List<NotificationAlertBar>();
}
