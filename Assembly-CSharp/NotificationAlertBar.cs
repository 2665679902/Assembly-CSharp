using System;
using System.Collections.Generic;

// Token: 0x02000B43 RID: 2883
public class NotificationAlertBar : KMonoBehaviour
{
	// Token: 0x06005952 RID: 22866 RVA: 0x002056A8 File Offset: 0x002038A8
	public void Init(ManagementMenuNotification notification)
	{
		this.notification = notification;
		this.thisButton.onClick += this.OnThisButtonClicked;
		this.background.colorStyleSetting = this.alertColorStyle[(int)notification.valence];
		this.background.ApplyColorStyleSetting();
		this.text.text = notification.titleText;
		this.tooltip.SetSimpleTooltip(notification.ToolTip(null, notification.tooltipData));
		this.muteButton.onClick += this.OnMuteButtonClicked;
	}

	// Token: 0x06005953 RID: 22867 RVA: 0x00205740 File Offset: 0x00203940
	private void OnThisButtonClicked()
	{
		NotificationHighlightController componentInParent = base.GetComponentInParent<NotificationHighlightController>();
		if (componentInParent != null)
		{
			componentInParent.SetActiveTarget(this.notification);
			return;
		}
		this.notification.View();
	}

	// Token: 0x06005954 RID: 22868 RVA: 0x00205775 File Offset: 0x00203975
	private void OnMuteButtonClicked()
	{
	}

	// Token: 0x04003C5D RID: 15453
	public ManagementMenuNotification notification;

	// Token: 0x04003C5E RID: 15454
	public KButton thisButton;

	// Token: 0x04003C5F RID: 15455
	public KImage background;

	// Token: 0x04003C60 RID: 15456
	public LocText text;

	// Token: 0x04003C61 RID: 15457
	public ToolTip tooltip;

	// Token: 0x04003C62 RID: 15458
	public KButton muteButton;

	// Token: 0x04003C63 RID: 15459
	public List<ColorStyleSetting> alertColorStyle;
}
