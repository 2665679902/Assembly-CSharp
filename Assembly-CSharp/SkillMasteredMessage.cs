using System;
using KSerialization;
using STRINGS;

// Token: 0x02000B21 RID: 2849
public class SkillMasteredMessage : Message
{
	// Token: 0x060057D9 RID: 22489 RVA: 0x001FD79C File Offset: 0x001FB99C
	public SkillMasteredMessage()
	{
	}

	// Token: 0x060057DA RID: 22490 RVA: 0x001FD7A4 File Offset: 0x001FB9A4
	public SkillMasteredMessage(MinionResume resume)
	{
		this.minionName = resume.GetProperName();
	}

	// Token: 0x060057DB RID: 22491 RVA: 0x001FD7B8 File Offset: 0x001FB9B8
	public override string GetSound()
	{
		return "AI_Notification_ResearchComplete";
	}

	// Token: 0x060057DC RID: 22492 RVA: 0x001FD7C0 File Offset: 0x001FB9C0
	public override string GetMessageBody()
	{
		Debug.Assert(this.minionName != null);
		string text = string.Format(MISC.NOTIFICATIONS.SKILL_POINT_EARNED.LINE, this.minionName);
		return string.Format(MISC.NOTIFICATIONS.SKILL_POINT_EARNED.MESSAGEBODY, text);
	}

	// Token: 0x060057DD RID: 22493 RVA: 0x001FD801 File Offset: 0x001FBA01
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.SKILL_POINT_EARNED.NAME.Replace("{Duplicant}", this.minionName);
	}

	// Token: 0x060057DE RID: 22494 RVA: 0x001FD818 File Offset: 0x001FBA18
	public override string GetTooltip()
	{
		return MISC.NOTIFICATIONS.SKILL_POINT_EARNED.TOOLTIP.Replace("{Duplicant}", this.minionName);
	}

	// Token: 0x060057DF RID: 22495 RVA: 0x001FD82F File Offset: 0x001FBA2F
	public override bool IsValid()
	{
		return this.minionName != null;
	}

	// Token: 0x04003B72 RID: 15218
	[Serialize]
	private string minionName;
}
