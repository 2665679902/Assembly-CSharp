using System;
using STRINGS;

// Token: 0x02000B10 RID: 2832
public class AchievementEarnedMessage : Message
{
	// Token: 0x06005756 RID: 22358 RVA: 0x001FC908 File Offset: 0x001FAB08
	public override bool ShowDialog()
	{
		return false;
	}

	// Token: 0x06005757 RID: 22359 RVA: 0x001FC90B File Offset: 0x001FAB0B
	public override string GetSound()
	{
		return "AI_Notification_ResearchComplete";
	}

	// Token: 0x06005758 RID: 22360 RVA: 0x001FC912 File Offset: 0x001FAB12
	public override string GetMessageBody()
	{
		return "";
	}

	// Token: 0x06005759 RID: 22361 RVA: 0x001FC919 File Offset: 0x001FAB19
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.COLONY_ACHIEVEMENT_EARNED.NAME;
	}

	// Token: 0x0600575A RID: 22362 RVA: 0x001FC925 File Offset: 0x001FAB25
	public override string GetTooltip()
	{
		return MISC.NOTIFICATIONS.COLONY_ACHIEVEMENT_EARNED.TOOLTIP;
	}

	// Token: 0x0600575B RID: 22363 RVA: 0x001FC931 File Offset: 0x001FAB31
	public override bool IsValid()
	{
		return true;
	}

	// Token: 0x0600575C RID: 22364 RVA: 0x001FC934 File Offset: 0x001FAB34
	public override void OnClick()
	{
		RetireColonyUtility.SaveColonySummaryData();
		MainMenu.ActivateRetiredColoniesScreenFromData(PauseScreen.Instance.transform.parent.gameObject, RetireColonyUtility.GetCurrentColonyRetiredColonyData());
	}
}
