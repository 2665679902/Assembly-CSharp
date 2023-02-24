using System;
using System.Collections.Generic;

// Token: 0x0200049F RID: 1183
public class MessageNotification : Notification
{
	// Token: 0x06001A9C RID: 6812 RVA: 0x0008E3B5 File Offset: 0x0008C5B5
	private string OnToolTip(List<Notification> notifications, string tooltipText)
	{
		return tooltipText;
	}

	// Token: 0x06001A9D RID: 6813 RVA: 0x0008E3B8 File Offset: 0x0008C5B8
	public MessageNotification(Message m)
		: base(m.GetTitle(), NotificationType.Messages, null, null, false, 0f, null, null, null, true, false, true)
	{
		MessageNotification <>4__this = this;
		this.message = m;
		if (!this.message.PlayNotificationSound())
		{
			this.playSound = false;
		}
		base.ToolTip = (List<Notification> notifications, object data) => <>4__this.OnToolTip(notifications, m.GetTooltip());
		base.clickFocus = null;
	}

	// Token: 0x04000EBD RID: 3773
	public Message message;
}
