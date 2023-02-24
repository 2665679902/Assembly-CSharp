using System;
using System.Collections.Generic;

// Token: 0x02000B44 RID: 2884
public abstract class NotificationDisplayer : KMonoBehaviour
{
	// Token: 0x06005956 RID: 22870 RVA: 0x0020577F File Offset: 0x0020397F
	protected override void OnSpawn()
	{
		this.displayedNotifications = new List<Notification>();
		NotificationManager.Instance.notificationAdded += this.NotificationAdded;
		NotificationManager.Instance.notificationRemoved += this.NotificationRemoved;
	}

	// Token: 0x06005957 RID: 22871 RVA: 0x002057B8 File Offset: 0x002039B8
	public void NotificationAdded(Notification notification)
	{
		if (this.ShouldDisplayNotification(notification))
		{
			this.displayedNotifications.Add(notification);
			this.OnNotificationAdded(notification);
		}
	}

	// Token: 0x06005958 RID: 22872
	protected abstract void OnNotificationAdded(Notification notification);

	// Token: 0x06005959 RID: 22873 RVA: 0x002057D6 File Offset: 0x002039D6
	public void NotificationRemoved(Notification notification)
	{
		if (this.displayedNotifications.Contains(notification))
		{
			this.displayedNotifications.Remove(notification);
			this.OnNotificationRemoved(notification);
		}
	}

	// Token: 0x0600595A RID: 22874
	protected abstract void OnNotificationRemoved(Notification notification);

	// Token: 0x0600595B RID: 22875
	protected abstract bool ShouldDisplayNotification(Notification notification);

	// Token: 0x04003C64 RID: 15460
	protected List<Notification> displayedNotifications;
}
