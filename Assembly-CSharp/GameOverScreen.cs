using System;

// Token: 0x02000AA2 RID: 2722
public class GameOverScreen : KModalScreen
{
	// Token: 0x06005372 RID: 21362 RVA: 0x001E46CE File Offset: 0x001E28CE
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Init();
	}

	// Token: 0x06005373 RID: 21363 RVA: 0x001E46DC File Offset: 0x001E28DC
	private void Init()
	{
		if (this.QuitButton)
		{
			this.QuitButton.onClick += delegate
			{
				this.Quit();
			};
		}
		if (this.DismissButton)
		{
			this.DismissButton.onClick += delegate
			{
				this.Dismiss();
			};
		}
	}

	// Token: 0x06005374 RID: 21364 RVA: 0x001E4731 File Offset: 0x001E2931
	private void Quit()
	{
		PauseScreen.TriggerQuitGame();
	}

	// Token: 0x06005375 RID: 21365 RVA: 0x001E4738 File Offset: 0x001E2938
	private void Dismiss()
	{
		this.Show(false);
	}

	// Token: 0x0400388C RID: 14476
	public KButton DismissButton;

	// Token: 0x0400388D RID: 14477
	public KButton QuitButton;
}
