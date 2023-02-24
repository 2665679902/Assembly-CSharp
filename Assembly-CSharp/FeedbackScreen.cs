using System;
using Steamworks;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A9B RID: 2715
public class FeedbackScreen : KModalScreen
{
	// Token: 0x0600533E RID: 21310 RVA: 0x001E35A4 File Offset: 0x001E17A4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.title.SetText(UI.FRONTEND.FEEDBACK_SCREEN.TITLE);
		this.dismissButton.onClick += delegate
		{
			this.Deactivate();
		};
		this.closeButton.onClick += delegate
		{
			this.Deactivate();
		};
		this.bugForumsButton.onClick += delegate
		{
			App.OpenWebURL("https://forums.kleientertainment.com/klei-bug-tracker/oni/");
		};
		this.suggestionForumsButton.onClick += delegate
		{
			App.OpenWebURL("https://forums.kleientertainment.com/forums/forum/133-oxygen-not-included-suggestions-and-feedback/");
		};
		this.logsDirectoryButton.onClick += delegate
		{
			App.OpenWebURL(Util.LogsFolder());
		};
		this.saveFilesDirectoryButton.onClick += delegate
		{
			App.OpenWebURL(SaveLoader.GetSavePrefix());
		};
		if (SteamUtils.IsSteamRunningOnSteamDeck())
		{
			this.logsDirectoryButton.GetComponentInParent<VerticalLayoutGroup>().padding = new RectOffset(0, 0, 0, 0);
			this.saveFilesDirectoryButton.gameObject.SetActive(false);
			this.logsDirectoryButton.gameObject.SetActive(false);
		}
	}

	// Token: 0x04003868 RID: 14440
	public LocText title;

	// Token: 0x04003869 RID: 14441
	public KButton dismissButton;

	// Token: 0x0400386A RID: 14442
	public KButton closeButton;

	// Token: 0x0400386B RID: 14443
	public KButton bugForumsButton;

	// Token: 0x0400386C RID: 14444
	public KButton suggestionForumsButton;

	// Token: 0x0400386D RID: 14445
	public KButton logsDirectoryButton;

	// Token: 0x0400386E RID: 14446
	public KButton saveFilesDirectoryButton;
}
