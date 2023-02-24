using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005DF RID: 1503
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/LogicAlarm")]
public class LogicAlarm : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x060025A1 RID: 9633 RVA: 0x000CBB74 File Offset: 0x000C9D74
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicAlarm>(-905833192, LogicAlarm.OnCopySettingsDelegate);
	}

	// Token: 0x060025A2 RID: 9634 RVA: 0x000CBB90 File Offset: 0x000C9D90
	private void OnCopySettings(object data)
	{
		LogicAlarm component = ((GameObject)data).GetComponent<LogicAlarm>();
		if (component != null)
		{
			this.notificationName = component.notificationName;
			this.notificationType = component.notificationType;
			this.pauseOnNotify = component.pauseOnNotify;
			this.zoomOnNotify = component.zoomOnNotify;
			this.cooldown = component.cooldown;
			this.notificationTooltip = component.notificationTooltip;
		}
	}

	// Token: 0x060025A3 RID: 9635 RVA: 0x000CBBFC File Offset: 0x000C9DFC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.notifier = base.gameObject.AddComponent<Notifier>();
		base.Subscribe<LogicAlarm>(-801688580, LogicAlarm.OnLogicValueChangedDelegate);
		if (string.IsNullOrEmpty(this.notificationName))
		{
			this.notificationName = UI.UISIDESCREENS.LOGICALARMSIDESCREEN.NAME_DEFAULT;
		}
		if (string.IsNullOrEmpty(this.notificationTooltip))
		{
			this.notificationTooltip = UI.UISIDESCREENS.LOGICALARMSIDESCREEN.TOOLTIP_DEFAULT;
		}
		this.UpdateVisualState();
		this.UpdateNotification(false);
	}

	// Token: 0x060025A4 RID: 9636 RVA: 0x000CBC78 File Offset: 0x000C9E78
	private void UpdateVisualState()
	{
		base.GetComponent<KBatchedAnimController>().Play(this.wasOn ? LogicAlarm.ON_ANIMS : LogicAlarm.OFF_ANIMS, KAnim.PlayMode.Once);
	}

	// Token: 0x060025A5 RID: 9637 RVA: 0x000CBC9C File Offset: 0x000C9E9C
	public void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID != LogicAlarm.INPUT_PORT_ID)
		{
			return;
		}
		int newValue = logicValueChanged.newValue;
		if (LogicCircuitNetwork.IsBitActive(0, newValue))
		{
			if (!this.wasOn)
			{
				this.PushNotification();
				this.wasOn = true;
				if (this.pauseOnNotify && !SpeedControlScreen.Instance.IsPaused)
				{
					SpeedControlScreen.Instance.Pause(false, false);
				}
				if (this.zoomOnNotify)
				{
					CameraController.Instance.ActiveWorldStarWipe(base.gameObject.GetMyWorldId(), base.transform.GetPosition(), 8f, null);
				}
				this.UpdateVisualState();
				return;
			}
		}
		else if (this.wasOn)
		{
			this.wasOn = false;
			this.UpdateVisualState();
		}
	}

	// Token: 0x060025A6 RID: 9638 RVA: 0x000CBD52 File Offset: 0x000C9F52
	private void PushNotification()
	{
		this.notification.Clear();
		this.notifier.Add(this.notification, "");
	}

	// Token: 0x060025A7 RID: 9639 RVA: 0x000CBD78 File Offset: 0x000C9F78
	public void UpdateNotification(bool clear)
	{
		if (this.notification != null && clear)
		{
			this.notification.Clear();
			this.lastNotificationCreated = null;
		}
		if (this.notification != this.lastNotificationCreated || this.lastNotificationCreated == null)
		{
			this.notification = this.CreateNotification();
		}
	}

	// Token: 0x060025A8 RID: 9640 RVA: 0x000CBDC8 File Offset: 0x000C9FC8
	public Notification CreateNotification()
	{
		base.GetComponent<KSelectable>();
		Notification notification = new Notification(this.notificationName, this.notificationType, (List<Notification> n, object d) => this.notificationTooltip, null, true, 0f, null, null, null, false, false, false);
		this.lastNotificationCreated = notification;
		return notification;
	}

	// Token: 0x040015F0 RID: 5616
	[Serialize]
	public string notificationName;

	// Token: 0x040015F1 RID: 5617
	[Serialize]
	public string notificationTooltip;

	// Token: 0x040015F2 RID: 5618
	[Serialize]
	public NotificationType notificationType;

	// Token: 0x040015F3 RID: 5619
	[Serialize]
	public bool pauseOnNotify;

	// Token: 0x040015F4 RID: 5620
	[Serialize]
	public bool zoomOnNotify;

	// Token: 0x040015F5 RID: 5621
	[Serialize]
	public float cooldown;

	// Token: 0x040015F6 RID: 5622
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x040015F7 RID: 5623
	private bool wasOn;

	// Token: 0x040015F8 RID: 5624
	private Notifier notifier;

	// Token: 0x040015F9 RID: 5625
	private Notification notification;

	// Token: 0x040015FA RID: 5626
	private Notification lastNotificationCreated;

	// Token: 0x040015FB RID: 5627
	private static readonly EventSystem.IntraObjectHandler<LogicAlarm> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicAlarm>(delegate(LogicAlarm component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x040015FC RID: 5628
	private static readonly EventSystem.IntraObjectHandler<LogicAlarm> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<LogicAlarm>(delegate(LogicAlarm component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x040015FD RID: 5629
	public static readonly HashedString INPUT_PORT_ID = new HashedString("LogicAlarmInput");

	// Token: 0x040015FE RID: 5630
	protected static readonly HashedString[] ON_ANIMS = new HashedString[] { "on_pre", "on_loop" };

	// Token: 0x040015FF RID: 5631
	protected static readonly HashedString[] OFF_ANIMS = new HashedString[] { "on_pst", "off" };
}
