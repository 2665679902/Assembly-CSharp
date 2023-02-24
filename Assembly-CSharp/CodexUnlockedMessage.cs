using System;
using STRINGS;

// Token: 0x02000B12 RID: 2834
public class CodexUnlockedMessage : Message
{
	// Token: 0x06005763 RID: 22371 RVA: 0x001FC9BA File Offset: 0x001FABBA
	public CodexUnlockedMessage()
	{
	}

	// Token: 0x06005764 RID: 22372 RVA: 0x001FC9C2 File Offset: 0x001FABC2
	public CodexUnlockedMessage(string lock_id, string unlock_message)
	{
		this.lockId = lock_id;
		this.unlockMessage = unlock_message;
	}

	// Token: 0x06005765 RID: 22373 RVA: 0x001FC9D8 File Offset: 0x001FABD8
	public string GetLockId()
	{
		return this.lockId;
	}

	// Token: 0x06005766 RID: 22374 RVA: 0x001FC9E0 File Offset: 0x001FABE0
	public override string GetSound()
	{
		return "AI_Notification_ResearchComplete";
	}

	// Token: 0x06005767 RID: 22375 RVA: 0x001FC9E7 File Offset: 0x001FABE7
	public override string GetMessageBody()
	{
		return UI.CODEX.CODEX_DISCOVERED_MESSAGE.BODY.Replace("{codex}", this.unlockMessage);
	}

	// Token: 0x06005768 RID: 22376 RVA: 0x001FC9FE File Offset: 0x001FABFE
	public override string GetTitle()
	{
		return UI.CODEX.CODEX_DISCOVERED_MESSAGE.TITLE;
	}

	// Token: 0x06005769 RID: 22377 RVA: 0x001FCA0A File Offset: 0x001FAC0A
	public override string GetTooltip()
	{
		return this.GetMessageBody();
	}

	// Token: 0x0600576A RID: 22378 RVA: 0x001FCA12 File Offset: 0x001FAC12
	public override bool IsValid()
	{
		return true;
	}

	// Token: 0x04003B4D RID: 15181
	private string unlockMessage;

	// Token: 0x04003B4E RID: 15182
	private string lockId;
}
