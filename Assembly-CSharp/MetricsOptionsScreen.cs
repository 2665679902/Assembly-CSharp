using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B29 RID: 2857
public class MetricsOptionsScreen : KModalScreen
{
	// Token: 0x0600581F RID: 22559 RVA: 0x001FE703 File Offset: 0x001FC903
	private bool IsSettingsDirty()
	{
		return this.disableDataCollection != KPrivacyPrefs.instance.disableDataCollection;
	}

	// Token: 0x06005820 RID: 22560 RVA: 0x001FE71A File Offset: 0x001FC91A
	public override void OnKeyDown(KButtonEvent e)
	{
		if ((e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight)) && !this.IsSettingsDirty())
		{
			this.Show(false);
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06005821 RID: 22561 RVA: 0x001FE744 File Offset: 0x001FC944
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.disableDataCollection = KPrivacyPrefs.instance.disableDataCollection;
		this.title.SetText(UI.FRONTEND.METRICS_OPTIONS_SCREEN.TITLE);
		GameObject gameObject = this.enableButton.GetComponent<HierarchyReferences>().GetReference("Button").gameObject;
		gameObject.GetComponent<ToolTip>().SetSimpleTooltip(UI.FRONTEND.METRICS_OPTIONS_SCREEN.TOOLTIP);
		gameObject.GetComponent<KButton>().onClick += delegate
		{
			this.OnClickToggle();
		};
		this.enableButton.GetComponent<HierarchyReferences>().GetReference<LocText>("Text").SetText(UI.FRONTEND.METRICS_OPTIONS_SCREEN.ENABLE_BUTTON);
		this.dismissButton.onClick += delegate
		{
			if (this.IsSettingsDirty())
			{
				this.ApplySettingsAndDoRestart();
				return;
			}
			this.Deactivate();
		};
		this.closeButton.onClick += delegate
		{
			this.Deactivate();
		};
		this.descriptionButton.onClick.AddListener(delegate
		{
			App.OpenWebURL("https://www.kleientertainment.com/privacy-policy");
		});
		this.Refresh();
	}

	// Token: 0x06005822 RID: 22562 RVA: 0x001FE848 File Offset: 0x001FCA48
	private void OnClickToggle()
	{
		this.disableDataCollection = !this.disableDataCollection;
		this.enableButton.GetComponent<HierarchyReferences>().GetReference("CheckMark").gameObject.SetActive(this.disableDataCollection);
		this.Refresh();
	}

	// Token: 0x06005823 RID: 22563 RVA: 0x001FE884 File Offset: 0x001FCA84
	private void ApplySettingsAndDoRestart()
	{
		KPrivacyPrefs.instance.disableDataCollection = this.disableDataCollection;
		KPrivacyPrefs.Save();
		KPlayerPrefs.SetString("DisableDataCollection", KPrivacyPrefs.instance.disableDataCollection ? "yes" : "no");
		KPlayerPrefs.Save();
		ThreadedHttps<KleiMetrics>.Instance.SetEnabled(!KPrivacyPrefs.instance.disableDataCollection);
		this.enableButton.GetComponent<HierarchyReferences>().GetReference("CheckMark").gameObject.SetActive(ThreadedHttps<KleiMetrics>.Instance.enabled);
		App.instance.Restart();
	}

	// Token: 0x06005824 RID: 22564 RVA: 0x001FE918 File Offset: 0x001FCB18
	private void Refresh()
	{
		this.enableButton.GetComponent<HierarchyReferences>().GetReference("Button").transform.GetChild(0).gameObject.SetActive(!this.disableDataCollection);
		this.closeButton.isInteractable = !this.IsSettingsDirty();
		this.restartWarningText.gameObject.SetActive(this.IsSettingsDirty());
		if (this.IsSettingsDirty())
		{
			this.dismissButton.GetComponentInChildren<LocText>().text = UI.FRONTEND.METRICS_OPTIONS_SCREEN.RESTART_BUTTON;
			return;
		}
		this.dismissButton.GetComponentInChildren<LocText>().text = UI.FRONTEND.METRICS_OPTIONS_SCREEN.DONE_BUTTON;
	}

	// Token: 0x04003B99 RID: 15257
	public LocText title;

	// Token: 0x04003B9A RID: 15258
	public KButton dismissButton;

	// Token: 0x04003B9B RID: 15259
	public KButton closeButton;

	// Token: 0x04003B9C RID: 15260
	public GameObject enableButton;

	// Token: 0x04003B9D RID: 15261
	public Button descriptionButton;

	// Token: 0x04003B9E RID: 15262
	public LocText restartWarningText;

	// Token: 0x04003B9F RID: 15263
	private bool disableDataCollection;
}
