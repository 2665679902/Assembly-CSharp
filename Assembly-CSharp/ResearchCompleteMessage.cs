using System;
using KSerialization;
using STRINGS;

// Token: 0x02000B1F RID: 2847
public class ResearchCompleteMessage : Message
{
	// Token: 0x060057C7 RID: 22471 RVA: 0x001FD3EC File Offset: 0x001FB5EC
	public ResearchCompleteMessage()
	{
	}

	// Token: 0x060057C8 RID: 22472 RVA: 0x001FD3FF File Offset: 0x001FB5FF
	public ResearchCompleteMessage(Tech tech)
	{
		this.tech.Set(tech);
	}

	// Token: 0x060057C9 RID: 22473 RVA: 0x001FD41E File Offset: 0x001FB61E
	public override string GetSound()
	{
		return "AI_Notification_ResearchComplete";
	}

	// Token: 0x060057CA RID: 22474 RVA: 0x001FD428 File Offset: 0x001FB628
	public override string GetMessageBody()
	{
		Tech tech = this.tech.Get();
		string text = "";
		for (int i = 0; i < tech.unlockedItems.Count; i++)
		{
			if (i != 0)
			{
				text += ", ";
			}
			text += tech.unlockedItems[i].Name;
		}
		return string.Format(MISC.NOTIFICATIONS.RESEARCHCOMPLETE.MESSAGEBODY, tech.Name, text);
	}

	// Token: 0x060057CB RID: 22475 RVA: 0x001FD49A File Offset: 0x001FB69A
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.RESEARCHCOMPLETE.NAME;
	}

	// Token: 0x060057CC RID: 22476 RVA: 0x001FD4A8 File Offset: 0x001FB6A8
	public override string GetTooltip()
	{
		Tech tech = this.tech.Get();
		return string.Format(MISC.NOTIFICATIONS.RESEARCHCOMPLETE.TOOLTIP, tech.Name);
	}

	// Token: 0x060057CD RID: 22477 RVA: 0x001FD4D6 File Offset: 0x001FB6D6
	public override bool IsValid()
	{
		return this.tech.Get() != null;
	}

	// Token: 0x04003B6F RID: 15215
	[Serialize]
	private ResourceRef<Tech> tech = new ResourceRef<Tech>();
}
