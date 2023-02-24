using System;
using KSerialization;

// Token: 0x02000B16 RID: 2838
public class EODReportMessage : Message
{
	// Token: 0x06005780 RID: 22400 RVA: 0x001FCB52 File Offset: 0x001FAD52
	public EODReportMessage(string title, string tooltip)
	{
		this.day = GameUtil.GetCurrentCycle();
		this.title = title;
		this.tooltip = tooltip;
	}

	// Token: 0x06005781 RID: 22401 RVA: 0x001FCB73 File Offset: 0x001FAD73
	public EODReportMessage()
	{
	}

	// Token: 0x06005782 RID: 22402 RVA: 0x001FCB7B File Offset: 0x001FAD7B
	public override string GetSound()
	{
		return null;
	}

	// Token: 0x06005783 RID: 22403 RVA: 0x001FCB7E File Offset: 0x001FAD7E
	public override string GetMessageBody()
	{
		return "";
	}

	// Token: 0x06005784 RID: 22404 RVA: 0x001FCB85 File Offset: 0x001FAD85
	public override string GetTooltip()
	{
		return this.tooltip;
	}

	// Token: 0x06005785 RID: 22405 RVA: 0x001FCB8D File Offset: 0x001FAD8D
	public override string GetTitle()
	{
		return this.title;
	}

	// Token: 0x06005786 RID: 22406 RVA: 0x001FCB95 File Offset: 0x001FAD95
	public void OpenReport()
	{
		ManagementMenu.Instance.OpenReports(this.day);
	}

	// Token: 0x06005787 RID: 22407 RVA: 0x001FCBA7 File Offset: 0x001FADA7
	public override bool ShowDialog()
	{
		return false;
	}

	// Token: 0x06005788 RID: 22408 RVA: 0x001FCBAA File Offset: 0x001FADAA
	public override void OnClick()
	{
		this.OpenReport();
	}

	// Token: 0x04003B52 RID: 15186
	[Serialize]
	private int day;

	// Token: 0x04003B53 RID: 15187
	[Serialize]
	private string title;

	// Token: 0x04003B54 RID: 15188
	[Serialize]
	private string tooltip;
}
