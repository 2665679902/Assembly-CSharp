using System;

// Token: 0x02000A84 RID: 2692
public class DemoOverScreen : KModalScreen
{
	// Token: 0x06005276 RID: 21110 RVA: 0x001DCD98 File Offset: 0x001DAF98
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Init();
		PlayerController.Instance.ActivateTool(SelectTool.Instance);
		SelectTool.Instance.Select(null, false);
	}

	// Token: 0x06005277 RID: 21111 RVA: 0x001DCDC1 File Offset: 0x001DAFC1
	private void Init()
	{
		this.QuitButton.onClick += delegate
		{
			this.Quit();
		};
	}

	// Token: 0x06005278 RID: 21112 RVA: 0x001DCDDA File Offset: 0x001DAFDA
	private void Quit()
	{
		PauseScreen.TriggerQuitGame();
	}

	// Token: 0x040037AF RID: 14255
	public KButton QuitButton;
}
