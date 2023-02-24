using System;
using UnityEngine;

// Token: 0x02000B24 RID: 2852
public class TargetMessageDialog : MessageDialog
{
	// Token: 0x060057E8 RID: 22504 RVA: 0x001FD8AB File Offset: 0x001FBAAB
	public override bool CanDisplay(Message message)
	{
		return typeof(TargetMessage).IsAssignableFrom(message.GetType());
	}

	// Token: 0x060057E9 RID: 22505 RVA: 0x001FD8C2 File Offset: 0x001FBAC2
	public override void SetMessage(Message base_message)
	{
		this.message = (TargetMessage)base_message;
		this.description.text = this.message.GetMessageBody();
	}

	// Token: 0x060057EA RID: 22506 RVA: 0x001FD8E8 File Offset: 0x001FBAE8
	public override void OnClickAction()
	{
		MessageTarget target = this.message.GetTarget();
		SelectTool.Instance.SelectAndFocus(target.GetPosition(), target.GetSelectable());
	}

	// Token: 0x060057EB RID: 22507 RVA: 0x001FD917 File Offset: 0x001FBB17
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.message.OnCleanUp();
	}

	// Token: 0x04003B76 RID: 15222
	[SerializeField]
	private LocText description;

	// Token: 0x04003B77 RID: 15223
	private TargetMessage message;
}
