using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000B4A RID: 2890
public class OptionsMenuScreen : KModalButtonMenu
{
	// Token: 0x06005987 RID: 22919 RVA: 0x00206238 File Offset: 0x00204438
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.keepMenuOpen = true;
		this.buttons = new List<KButtonMenu.ButtonInfo>
		{
			new KButtonMenu.ButtonInfo(UI.FRONTEND.OPTIONS_SCREEN.GRAPHICS, global::Action.NumActions, new UnityAction(this.OnGraphicsOptions), null, null),
			new KButtonMenu.ButtonInfo(UI.FRONTEND.OPTIONS_SCREEN.AUDIO, global::Action.NumActions, new UnityAction(this.OnAudioOptions), null, null),
			new KButtonMenu.ButtonInfo(UI.FRONTEND.OPTIONS_SCREEN.GAME, global::Action.NumActions, new UnityAction(this.OnGameOptions), null, null),
			new KButtonMenu.ButtonInfo(UI.FRONTEND.OPTIONS_SCREEN.METRICS, global::Action.NumActions, new UnityAction(this.OnMetrics), null, null),
			new KButtonMenu.ButtonInfo(UI.FRONTEND.OPTIONS_SCREEN.FEEDBACK, global::Action.NumActions, new UnityAction(this.OnFeedback), null, null),
			new KButtonMenu.ButtonInfo(UI.FRONTEND.OPTIONS_SCREEN.CREDITS, global::Action.NumActions, new UnityAction(this.OnCredits), null, null)
		};
		this.closeButton.onClick += this.Deactivate;
		this.backButton.onClick += this.Deactivate;
	}

	// Token: 0x06005988 RID: 22920 RVA: 0x0020637D File Offset: 0x0020457D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.title.SetText(UI.FRONTEND.OPTIONS_SCREEN.TITLE);
		this.backButton.transform.SetAsLastSibling();
	}

	// Token: 0x06005989 RID: 22921 RVA: 0x002063AC File Offset: 0x002045AC
	protected override void OnActivate()
	{
		base.OnActivate();
		foreach (GameObject gameObject in this.buttonObjects)
		{
		}
	}

	// Token: 0x0600598A RID: 22922 RVA: 0x002063D8 File Offset: 0x002045D8
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.Deactivate();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x0600598B RID: 22923 RVA: 0x002063FA File Offset: 0x002045FA
	private void OnGraphicsOptions()
	{
		base.ActivateChildScreen(this.graphicsOptionsScreenPrefab.gameObject);
	}

	// Token: 0x0600598C RID: 22924 RVA: 0x0020640E File Offset: 0x0020460E
	private void OnAudioOptions()
	{
		base.ActivateChildScreen(this.audioOptionsScreenPrefab.gameObject);
	}

	// Token: 0x0600598D RID: 22925 RVA: 0x00206422 File Offset: 0x00204622
	private void OnGameOptions()
	{
		base.ActivateChildScreen(this.gameOptionsScreenPrefab.gameObject);
	}

	// Token: 0x0600598E RID: 22926 RVA: 0x00206436 File Offset: 0x00204636
	private void OnMetrics()
	{
		base.ActivateChildScreen(this.metricsScreenPrefab.gameObject);
	}

	// Token: 0x0600598F RID: 22927 RVA: 0x0020644A File Offset: 0x0020464A
	public void ShowMetricsScreen()
	{
		base.ActivateChildScreen(this.metricsScreenPrefab.gameObject);
	}

	// Token: 0x06005990 RID: 22928 RVA: 0x0020645E File Offset: 0x0020465E
	private void OnFeedback()
	{
		base.ActivateChildScreen(this.feedbackScreenPrefab.gameObject);
	}

	// Token: 0x06005991 RID: 22929 RVA: 0x00206472 File Offset: 0x00204672
	private void OnCredits()
	{
		base.ActivateChildScreen(this.creditsScreenPrefab.gameObject);
	}

	// Token: 0x06005992 RID: 22930 RVA: 0x00206486 File Offset: 0x00204686
	private void Update()
	{
		global::Debug.developerConsoleVisible = false;
	}

	// Token: 0x04003C7A RID: 15482
	[SerializeField]
	private GameOptionsScreen gameOptionsScreenPrefab;

	// Token: 0x04003C7B RID: 15483
	[SerializeField]
	private AudioOptionsScreen audioOptionsScreenPrefab;

	// Token: 0x04003C7C RID: 15484
	[SerializeField]
	private GraphicsOptionsScreen graphicsOptionsScreenPrefab;

	// Token: 0x04003C7D RID: 15485
	[SerializeField]
	private CreditsScreen creditsScreenPrefab;

	// Token: 0x04003C7E RID: 15486
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003C7F RID: 15487
	[SerializeField]
	private MetricsOptionsScreen metricsScreenPrefab;

	// Token: 0x04003C80 RID: 15488
	[SerializeField]
	private FeedbackScreen feedbackScreenPrefab;

	// Token: 0x04003C81 RID: 15489
	[SerializeField]
	private LocText title;

	// Token: 0x04003C82 RID: 15490
	[SerializeField]
	private KButton backButton;
}
