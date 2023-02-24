using System;
using System.Collections.Generic;

// Token: 0x02000B47 RID: 2887
public class NotificationManager : KMonoBehaviour
{
	// Token: 0x1700065D RID: 1629
	// (get) Token: 0x0600596C RID: 22892 RVA: 0x00205C2B File Offset: 0x00203E2B
	// (set) Token: 0x0600596D RID: 22893 RVA: 0x00205C32 File Offset: 0x00203E32
	public static NotificationManager Instance { get; private set; }

	// Token: 0x14000029 RID: 41
	// (add) Token: 0x0600596E RID: 22894 RVA: 0x00205C3C File Offset: 0x00203E3C
	// (remove) Token: 0x0600596F RID: 22895 RVA: 0x00205C74 File Offset: 0x00203E74
	public event Action<Notification> notificationAdded;

	// Token: 0x1400002A RID: 42
	// (add) Token: 0x06005970 RID: 22896 RVA: 0x00205CAC File Offset: 0x00203EAC
	// (remove) Token: 0x06005971 RID: 22897 RVA: 0x00205CE4 File Offset: 0x00203EE4
	public event Action<Notification> notificationRemoved;

	// Token: 0x06005972 RID: 22898 RVA: 0x00205D1C File Offset: 0x00203F1C
	protected override void OnPrefabInit()
	{
		Debug.Assert(NotificationManager.Instance == null);
		NotificationManager.Instance = this;
		Components.Notifiers.OnAdd += this.OnAddNotifier;
		Components.Notifiers.OnRemove += this.OnRemoveNotifier;
		foreach (Notifier notifier in Components.Notifiers.Items)
		{
			this.OnAddNotifier(notifier);
		}
	}

	// Token: 0x06005973 RID: 22899 RVA: 0x00205DB8 File Offset: 0x00203FB8
	protected override void OnForcedCleanUp()
	{
		NotificationManager.Instance = null;
	}

	// Token: 0x06005974 RID: 22900 RVA: 0x00205DC0 File Offset: 0x00203FC0
	private void OnAddNotifier(Notifier notifier)
	{
		notifier.OnAdd = (Action<Notification>)Delegate.Combine(notifier.OnAdd, new Action<Notification>(this.OnAddNotification));
		notifier.OnRemove = (Action<Notification>)Delegate.Combine(notifier.OnRemove, new Action<Notification>(this.OnRemoveNotification));
	}

	// Token: 0x06005975 RID: 22901 RVA: 0x00205E14 File Offset: 0x00204014
	private void OnRemoveNotifier(Notifier notifier)
	{
		notifier.OnAdd = (Action<Notification>)Delegate.Remove(notifier.OnAdd, new Action<Notification>(this.OnAddNotification));
		notifier.OnRemove = (Action<Notification>)Delegate.Remove(notifier.OnRemove, new Action<Notification>(this.OnRemoveNotification));
	}

	// Token: 0x06005976 RID: 22902 RVA: 0x00205E65 File Offset: 0x00204065
	private void OnAddNotification(Notification notification)
	{
		this.pendingNotifications.Add(notification);
	}

	// Token: 0x06005977 RID: 22903 RVA: 0x00205E73 File Offset: 0x00204073
	private void OnRemoveNotification(Notification notification)
	{
		this.pendingNotifications.Remove(notification);
		if (this.notifications.Remove(notification))
		{
			this.notificationRemoved(notification);
		}
	}

	// Token: 0x06005978 RID: 22904 RVA: 0x00205E9C File Offset: 0x0020409C
	private void Update()
	{
		int i = 0;
		while (i < this.pendingNotifications.Count)
		{
			if (this.pendingNotifications[i].IsReady())
			{
				this.DoAddNotification(this.pendingNotifications[i]);
				this.pendingNotifications.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x06005979 RID: 22905 RVA: 0x00205EF2 File Offset: 0x002040F2
	private void DoAddNotification(Notification notification)
	{
		this.notifications.Add(notification);
		if (this.notificationAdded != null)
		{
			this.notificationAdded(notification);
		}
	}

	// Token: 0x04003C6E RID: 15470
	private List<Notification> pendingNotifications = new List<Notification>();

	// Token: 0x04003C6F RID: 15471
	private List<Notification> notifications = new List<Notification>();
}
