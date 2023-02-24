using System;
using UnityEngine;

// Token: 0x02000B11 RID: 2833
public class CodexMessageDialog : MessageDialog
{
	// Token: 0x0600575E RID: 22366 RVA: 0x001FC962 File Offset: 0x001FAB62
	public override bool CanDisplay(Message message)
	{
		return typeof(CodexUnlockedMessage).IsAssignableFrom(message.GetType());
	}

	// Token: 0x0600575F RID: 22367 RVA: 0x001FC979 File Offset: 0x001FAB79
	public override void SetMessage(Message base_message)
	{
		this.message = (CodexUnlockedMessage)base_message;
		this.description.text = this.message.GetMessageBody();
	}

	// Token: 0x06005760 RID: 22368 RVA: 0x001FC99D File Offset: 0x001FAB9D
	public override void OnClickAction()
	{
	}

	// Token: 0x06005761 RID: 22369 RVA: 0x001FC99F File Offset: 0x001FAB9F
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.message.OnCleanUp();
	}

	// Token: 0x04003B4B RID: 15179
	[SerializeField]
	private LocText description;

	// Token: 0x04003B4C RID: 15180
	private CodexUnlockedMessage message;
}
