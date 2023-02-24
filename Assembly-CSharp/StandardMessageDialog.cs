using System;
using UnityEngine;

// Token: 0x02000B22 RID: 2850
public class StandardMessageDialog : MessageDialog
{
	// Token: 0x060057E0 RID: 22496 RVA: 0x001FD83A File Offset: 0x001FBA3A
	public override bool CanDisplay(Message message)
	{
		return typeof(Message).IsAssignableFrom(message.GetType());
	}

	// Token: 0x060057E1 RID: 22497 RVA: 0x001FD851 File Offset: 0x001FBA51
	public override void SetMessage(Message base_message)
	{
		this.message = base_message;
		this.description.text = this.message.GetMessageBody();
	}

	// Token: 0x060057E2 RID: 22498 RVA: 0x001FD870 File Offset: 0x001FBA70
	public override void OnClickAction()
	{
	}

	// Token: 0x04003B73 RID: 15219
	[SerializeField]
	private LocText description;

	// Token: 0x04003B74 RID: 15220
	private Message message;
}
