using System;

// Token: 0x02000B46 RID: 2886
public class NotificationHighlightTarget : KMonoBehaviour
{
	// Token: 0x06005968 RID: 22888 RVA: 0x00205BD1 File Offset: 0x00203DD1
	protected void OnEnable()
	{
		this.controller = base.GetComponentInParent<NotificationHighlightController>();
		if (this.controller != null)
		{
			this.controller.AddTarget(this);
		}
	}

	// Token: 0x06005969 RID: 22889 RVA: 0x00205BF9 File Offset: 0x00203DF9
	protected void OnDisable()
	{
		if (this.controller != null)
		{
			this.controller.RemoveTarget(this);
		}
	}

	// Token: 0x0600596A RID: 22890 RVA: 0x00205C15 File Offset: 0x00203E15
	public void View()
	{
		base.GetComponentInParent<NotificationHighlightController>().TargetViewed(this);
	}

	// Token: 0x04003C69 RID: 15465
	public string targetKey;

	// Token: 0x04003C6A RID: 15466
	private NotificationHighlightController controller;
}
