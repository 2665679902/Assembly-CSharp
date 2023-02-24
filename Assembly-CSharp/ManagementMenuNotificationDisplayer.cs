using System;
using System.Collections.Generic;

// Token: 0x02000B41 RID: 2881
public class ManagementMenuNotificationDisplayer : NotificationDisplayer
{
	// Token: 0x1700065C RID: 1628
	// (get) Token: 0x06005942 RID: 22850 RVA: 0x00205505 File Offset: 0x00203705
	// (set) Token: 0x06005943 RID: 22851 RVA: 0x0020550D File Offset: 0x0020370D
	public List<ManagementMenuNotification> displayedManagementMenuNotifications { get; private set; }

	// Token: 0x14000028 RID: 40
	// (add) Token: 0x06005944 RID: 22852 RVA: 0x00205518 File Offset: 0x00203718
	// (remove) Token: 0x06005945 RID: 22853 RVA: 0x00205550 File Offset: 0x00203750
	public event System.Action onNotificationsChanged;

	// Token: 0x06005946 RID: 22854 RVA: 0x00205585 File Offset: 0x00203785
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.displayedManagementMenuNotifications = new List<ManagementMenuNotification>();
	}

	// Token: 0x06005947 RID: 22855 RVA: 0x00205598 File Offset: 0x00203798
	public void NotificationWasViewed(ManagementMenuNotification notification)
	{
		this.onNotificationsChanged();
	}

	// Token: 0x06005948 RID: 22856 RVA: 0x002055A5 File Offset: 0x002037A5
	protected override void OnNotificationAdded(Notification notification)
	{
		this.displayedManagementMenuNotifications.Add(notification as ManagementMenuNotification);
		this.onNotificationsChanged();
	}

	// Token: 0x06005949 RID: 22857 RVA: 0x002055C3 File Offset: 0x002037C3
	protected override void OnNotificationRemoved(Notification notification)
	{
		this.displayedManagementMenuNotifications.Remove(notification as ManagementMenuNotification);
		this.onNotificationsChanged();
	}

	// Token: 0x0600594A RID: 22858 RVA: 0x002055E2 File Offset: 0x002037E2
	protected override bool ShouldDisplayNotification(Notification notification)
	{
		return notification is ManagementMenuNotification;
	}

	// Token: 0x0600594B RID: 22859 RVA: 0x002055F0 File Offset: 0x002037F0
	public List<ManagementMenuNotification> GetNotificationsForAction(global::Action hotKey)
	{
		List<ManagementMenuNotification> list = new List<ManagementMenuNotification>();
		foreach (ManagementMenuNotification managementMenuNotification in this.displayedManagementMenuNotifications)
		{
			if (managementMenuNotification.targetMenu == hotKey)
			{
				list.Add(managementMenuNotification);
			}
		}
		return list;
	}
}
