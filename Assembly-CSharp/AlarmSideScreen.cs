using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B8F RID: 2959
public class AlarmSideScreen : SideScreenContent
{
	// Token: 0x06005CF9 RID: 23801 RVA: 0x0021FB7C File Offset: 0x0021DD7C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.nameInputField.onEndEdit += this.OnEndEditName;
		this.nameInputField.field.characterLimit = 30;
		this.tooltipInputField.onEndEdit += this.OnEndEditTooltip;
		this.tooltipInputField.field.characterLimit = 90;
		this.pauseToggle.onClick += this.TogglePause;
		this.zoomToggle.onClick += this.ToggleZoom;
		this.InitializeToggles();
	}

	// Token: 0x06005CFA RID: 23802 RVA: 0x0021FC15 File Offset: 0x0021DE15
	private void OnEndEditName()
	{
		this.targetAlarm.notificationName = this.nameInputField.field.text;
		this.UpdateNotification(true);
	}

	// Token: 0x06005CFB RID: 23803 RVA: 0x0021FC39 File Offset: 0x0021DE39
	private void OnEndEditTooltip()
	{
		this.targetAlarm.notificationTooltip = this.tooltipInputField.field.text;
		this.UpdateNotification(true);
	}

	// Token: 0x06005CFC RID: 23804 RVA: 0x0021FC5D File Offset: 0x0021DE5D
	private void TogglePause()
	{
		this.targetAlarm.pauseOnNotify = !this.targetAlarm.pauseOnNotify;
		this.pauseCheckmark.enabled = this.targetAlarm.pauseOnNotify;
		this.UpdateNotification(true);
	}

	// Token: 0x06005CFD RID: 23805 RVA: 0x0021FC95 File Offset: 0x0021DE95
	private void ToggleZoom()
	{
		this.targetAlarm.zoomOnNotify = !this.targetAlarm.zoomOnNotify;
		this.zoomCheckmark.enabled = this.targetAlarm.zoomOnNotify;
		this.UpdateNotification(true);
	}

	// Token: 0x06005CFE RID: 23806 RVA: 0x0021FCCD File Offset: 0x0021DECD
	private void SelectType(NotificationType type)
	{
		this.targetAlarm.notificationType = type;
		this.UpdateNotification(true);
		this.RefreshToggles();
	}

	// Token: 0x06005CFF RID: 23807 RVA: 0x0021FCE8 File Offset: 0x0021DEE8
	private void InitializeToggles()
	{
		if (this.toggles_by_type.Count == 0)
		{
			using (List<NotificationType>.Enumerator enumerator = this.validTypes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NotificationType type = enumerator.Current;
					GameObject gameObject = Util.KInstantiateUI(this.typeButtonPrefab, this.typeButtonPrefab.transform.parent.gameObject, true);
					gameObject.name = "TypeButton: " + type.ToString();
					HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
					Color notificationBGColour = NotificationScreen.Instance.GetNotificationBGColour(type);
					Color notificationColour = NotificationScreen.Instance.GetNotificationColour(type);
					notificationBGColour.a = 1f;
					notificationColour.a = 1f;
					component.GetReference<KImage>("bg").color = notificationBGColour;
					component.GetReference<KImage>("icon").color = notificationColour;
					component.GetReference<KImage>("icon").sprite = NotificationScreen.Instance.GetNotificationIcon(type);
					ToolTip component2 = gameObject.GetComponent<ToolTip>();
					NotificationType type2 = type;
					if (type2 != NotificationType.Bad)
					{
						if (type2 != NotificationType.Neutral)
						{
							if (type2 == NotificationType.DuplicantThreatening)
							{
								component2.SetSimpleTooltip(UI.UISIDESCREENS.LOGICALARMSIDESCREEN.TOOLTIPS.DUPLICANT_THREATENING);
							}
						}
						else
						{
							component2.SetSimpleTooltip(UI.UISIDESCREENS.LOGICALARMSIDESCREEN.TOOLTIPS.NEUTRAL);
						}
					}
					else
					{
						component2.SetSimpleTooltip(UI.UISIDESCREENS.LOGICALARMSIDESCREEN.TOOLTIPS.BAD);
					}
					if (!this.toggles_by_type.ContainsKey(type))
					{
						this.toggles_by_type.Add(type, gameObject.GetComponent<MultiToggle>());
					}
					this.toggles_by_type[type].onClick = delegate
					{
						this.SelectType(type);
					};
					for (int i = 0; i < this.toggles_by_type[type].states.Length; i++)
					{
						this.toggles_by_type[type].states[i].on_click_override_sound_path = NotificationScreen.Instance.GetNotificationSound(type);
					}
				}
			}
		}
	}

	// Token: 0x06005D00 RID: 23808 RVA: 0x0021FF2C File Offset: 0x0021E12C
	private void RefreshToggles()
	{
		this.InitializeToggles();
		foreach (KeyValuePair<NotificationType, MultiToggle> keyValuePair in this.toggles_by_type)
		{
			if (this.targetAlarm.notificationType == keyValuePair.Key)
			{
				keyValuePair.Value.ChangeState(0);
			}
			else
			{
				keyValuePair.Value.ChangeState(1);
			}
		}
	}

	// Token: 0x06005D01 RID: 23809 RVA: 0x0021FFB0 File Offset: 0x0021E1B0
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<LogicAlarm>() != null;
	}

	// Token: 0x06005D02 RID: 23810 RVA: 0x0021FFBE File Offset: 0x0021E1BE
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetAlarm = target.GetComponent<LogicAlarm>();
		this.RefreshToggles();
		this.UpdateVisuals();
	}

	// Token: 0x06005D03 RID: 23811 RVA: 0x0021FFDF File Offset: 0x0021E1DF
	private void UpdateNotification(bool clear)
	{
		this.targetAlarm.UpdateNotification(clear);
	}

	// Token: 0x06005D04 RID: 23812 RVA: 0x0021FFF0 File Offset: 0x0021E1F0
	private void UpdateVisuals()
	{
		this.nameInputField.SetDisplayValue(this.targetAlarm.notificationName);
		this.tooltipInputField.SetDisplayValue(this.targetAlarm.notificationTooltip);
		this.pauseCheckmark.enabled = this.targetAlarm.pauseOnNotify;
		this.zoomCheckmark.enabled = this.targetAlarm.zoomOnNotify;
	}

	// Token: 0x04003F8E RID: 16270
	public LogicAlarm targetAlarm;

	// Token: 0x04003F8F RID: 16271
	[SerializeField]
	private KInputField nameInputField;

	// Token: 0x04003F90 RID: 16272
	[SerializeField]
	private KInputField tooltipInputField;

	// Token: 0x04003F91 RID: 16273
	[SerializeField]
	private KToggle pauseToggle;

	// Token: 0x04003F92 RID: 16274
	[SerializeField]
	private Image pauseCheckmark;

	// Token: 0x04003F93 RID: 16275
	[SerializeField]
	private KToggle zoomToggle;

	// Token: 0x04003F94 RID: 16276
	[SerializeField]
	private Image zoomCheckmark;

	// Token: 0x04003F95 RID: 16277
	[SerializeField]
	private GameObject typeButtonPrefab;

	// Token: 0x04003F96 RID: 16278
	private List<NotificationType> validTypes = new List<NotificationType>
	{
		NotificationType.Bad,
		NotificationType.Neutral,
		NotificationType.DuplicantThreatening
	};

	// Token: 0x04003F97 RID: 16279
	private Dictionary<NotificationType, MultiToggle> toggles_by_type = new Dictionary<NotificationType, MultiToggle>();
}
