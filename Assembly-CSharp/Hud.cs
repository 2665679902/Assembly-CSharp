using System;

// Token: 0x02000AB6 RID: 2742
public class Hud : KScreen
{
	// Token: 0x060053EC RID: 21484 RVA: 0x001E823E File Offset: 0x001E643E
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Help))
		{
			GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.ControlsScreen.gameObject, null, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay);
		}
	}
}
