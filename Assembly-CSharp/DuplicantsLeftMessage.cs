using System;
using STRINGS;

// Token: 0x02000B15 RID: 2837
public class DuplicantsLeftMessage : Message
{
	// Token: 0x0600577B RID: 22395 RVA: 0x001FCB1F File Offset: 0x001FAD1F
	public override string GetSound()
	{
		return "";
	}

	// Token: 0x0600577C RID: 22396 RVA: 0x001FCB26 File Offset: 0x001FAD26
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.DUPLICANTABSORBED.NAME;
	}

	// Token: 0x0600577D RID: 22397 RVA: 0x001FCB32 File Offset: 0x001FAD32
	public override string GetMessageBody()
	{
		return MISC.NOTIFICATIONS.DUPLICANTABSORBED.MESSAGEBODY;
	}

	// Token: 0x0600577E RID: 22398 RVA: 0x001FCB3E File Offset: 0x001FAD3E
	public override string GetTooltip()
	{
		return MISC.NOTIFICATIONS.DUPLICANTABSORBED.TOOLTIP;
	}
}
