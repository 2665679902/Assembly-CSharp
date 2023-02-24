using System;
using KSerialization;

// Token: 0x02000B19 RID: 2841
[SerializationConfig(MemberSerialization.OptIn)]
public abstract class Message : ISaveLoadable
{
	// Token: 0x06005797 RID: 22423
	public abstract string GetTitle();

	// Token: 0x06005798 RID: 22424
	public abstract string GetSound();

	// Token: 0x06005799 RID: 22425
	public abstract string GetMessageBody();

	// Token: 0x0600579A RID: 22426
	public abstract string GetTooltip();

	// Token: 0x0600579B RID: 22427 RVA: 0x001FCD69 File Offset: 0x001FAF69
	public virtual bool ShowDialog()
	{
		return true;
	}

	// Token: 0x0600579C RID: 22428 RVA: 0x001FCD6C File Offset: 0x001FAF6C
	public virtual void OnCleanUp()
	{
	}

	// Token: 0x0600579D RID: 22429 RVA: 0x001FCD6E File Offset: 0x001FAF6E
	public virtual bool IsValid()
	{
		return true;
	}

	// Token: 0x0600579E RID: 22430 RVA: 0x001FCD71 File Offset: 0x001FAF71
	public virtual bool PlayNotificationSound()
	{
		return true;
	}

	// Token: 0x0600579F RID: 22431 RVA: 0x001FCD74 File Offset: 0x001FAF74
	public virtual void OnClick()
	{
	}
}
