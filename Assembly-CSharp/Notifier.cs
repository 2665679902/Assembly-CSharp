using System;
using UnityEngine;

// Token: 0x020004B0 RID: 1200
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Notifier")]
public class Notifier : KMonoBehaviour
{
	// Token: 0x06001B59 RID: 7001 RVA: 0x000915F4 File Offset: 0x0008F7F4
	protected override void OnPrefabInit()
	{
		Components.Notifiers.Add(this);
	}

	// Token: 0x06001B5A RID: 7002 RVA: 0x00091601 File Offset: 0x0008F801
	protected override void OnCleanUp()
	{
		Components.Notifiers.Remove(this);
	}

	// Token: 0x06001B5B RID: 7003 RVA: 0x00091610 File Offset: 0x0008F810
	public void Add(Notification notification, string suffix = "")
	{
		if (KScreenManager.Instance == null)
		{
			return;
		}
		if (this.DisableNotifications)
		{
			return;
		}
		if (DebugHandler.NotificationsDisabled)
		{
			return;
		}
		DebugUtil.DevAssert(notification != null, "Trying to add null notification. It's safe to continue playing, the notification won't be displayed.", null);
		if (notification == null)
		{
			return;
		}
		if (notification.Notifier == null)
		{
			if (this.Selectable != null)
			{
				notification.NotifierName = "• " + this.Selectable.GetName() + suffix;
			}
			else
			{
				notification.NotifierName = "• " + base.name + suffix;
			}
			notification.Notifier = this;
			if (this.AutoClickFocus && notification.clickFocus == null)
			{
				notification.clickFocus = base.transform;
			}
			if (this.OnAdd != null)
			{
				this.OnAdd(notification);
			}
			notification.GameTime = Time.time;
		}
		else
		{
			DebugUtil.Assert(notification.Notifier == this);
		}
		notification.Time = KTime.Instance.UnscaledGameTime;
	}

	// Token: 0x06001B5C RID: 7004 RVA: 0x0009170E File Offset: 0x0008F90E
	public void Remove(Notification notification)
	{
		if (notification == null)
		{
			return;
		}
		if (notification.Notifier != null)
		{
			notification.Notifier = null;
			if (this.OnRemove != null)
			{
				this.OnRemove(notification);
			}
		}
	}

	// Token: 0x04000F45 RID: 3909
	[MyCmpGet]
	private KSelectable Selectable;

	// Token: 0x04000F46 RID: 3910
	public Action<Notification> OnAdd;

	// Token: 0x04000F47 RID: 3911
	public Action<Notification> OnRemove;

	// Token: 0x04000F48 RID: 3912
	public bool DisableNotifications;

	// Token: 0x04000F49 RID: 3913
	public bool AutoClickFocus = true;
}
