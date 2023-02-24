using System;
using System.Collections.Generic;
using KMod;
using STRINGS;
using UnityEngine;

// Token: 0x02000B65 RID: 2917
public class ReportErrorDialog : MonoBehaviour
{
	// Token: 0x06005B06 RID: 23302 RVA: 0x00210B34 File Offset: 0x0020ED34
	private void Start()
	{
		ThreadedHttps<KleiMetrics>.Instance.EndSession(true);
		if (KScreenManager.Instance)
		{
			KScreenManager.Instance.DisableInput(true);
		}
		this.StackTrace.SetActive(false);
		this.CrashLabel.text = ((this.mode == ReportErrorDialog.Mode.SubmitError) ? UI.CRASHSCREEN.TITLE : UI.CRASHSCREEN.TITLE_MODS);
		this.CrashDescription.SetActive(this.mode == ReportErrorDialog.Mode.SubmitError);
		this.ModsInfo.SetActive(this.mode == ReportErrorDialog.Mode.DisableMods);
		if (this.mode == ReportErrorDialog.Mode.DisableMods)
		{
			this.BuildModsList();
		}
		this.submitButton.gameObject.SetActive(this.submitAction != null);
		this.submitButton.onClick += this.OnSelect_SUBMIT;
		this.moreInfoButton.onClick += this.OnSelect_MOREINFO;
		this.continueGameButton.gameObject.SetActive(this.continueAction != null);
		this.continueGameButton.onClick += this.OnSelect_CONTINUE;
		this.quitButton.onClick += this.OnSelect_QUIT;
		this.messageInputField.text = UI.CRASHSCREEN.BODY;
	}

	// Token: 0x06005B07 RID: 23303 RVA: 0x00210C70 File Offset: 0x0020EE70
	private void BuildModsList()
	{
		DebugUtil.Assert(Global.Instance != null && Global.Instance.modManager != null);
		Manager mod_mgr = Global.Instance.modManager;
		List<Mod> allCrashableMods = mod_mgr.GetAllCrashableMods();
		allCrashableMods.Sort((Mod x, Mod y) => y.foundInStackTrace.CompareTo(x.foundInStackTrace));
		foreach (Mod mod in allCrashableMods)
		{
			if (mod.foundInStackTrace && mod.label.distribution_platform != Label.DistributionPlatform.Dev)
			{
				mod_mgr.EnableMod(mod.label, false, this);
			}
			HierarchyReferences hierarchyReferences = Util.KInstantiateUI<HierarchyReferences>(this.modEntryPrefab, this.modEntryParent.gameObject, false);
			LocText reference = hierarchyReferences.GetReference<LocText>("Title");
			reference.text = mod.title;
			reference.color = (mod.foundInStackTrace ? Color.red : Color.white);
			MultiToggle toggle = hierarchyReferences.GetReference<MultiToggle>("EnabledToggle");
			toggle.ChangeState(mod.IsEnabledForActiveDlc() ? 1 : 0);
			Label mod_label = mod.label;
			MultiToggle toggle2 = toggle;
			toggle2.onClick = (System.Action)Delegate.Combine(toggle2.onClick, new System.Action(delegate
			{
				bool flag = !mod_mgr.IsModEnabled(mod_label);
				toggle.ChangeState(flag ? 1 : 0);
				mod_mgr.EnableMod(mod_label, flag, this);
			}));
			toggle.GetComponent<ToolTip>().OnToolTip = () => mod_mgr.IsModEnabled(mod_label) ? UI.FRONTEND.MODS.TOOLTIPS.ENABLED : UI.FRONTEND.MODS.TOOLTIPS.DISABLED;
			hierarchyReferences.gameObject.SetActive(true);
		}
	}

	// Token: 0x06005B08 RID: 23304 RVA: 0x00210E44 File Offset: 0x0020F044
	private void Update()
	{
		global::Debug.developerConsoleVisible = false;
	}

	// Token: 0x06005B09 RID: 23305 RVA: 0x00210E4C File Offset: 0x0020F04C
	private void OnDestroy()
	{
		if (KCrashReporter.terminateOnError)
		{
			App.Quit();
		}
		if (KScreenManager.Instance)
		{
			KScreenManager.Instance.DisableInput(false);
		}
	}

	// Token: 0x06005B0A RID: 23306 RVA: 0x00210E71 File Offset: 0x0020F071
	public void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape))
		{
			this.OnSelect_QUIT();
		}
	}

	// Token: 0x06005B0B RID: 23307 RVA: 0x00210E82 File Offset: 0x0020F082
	public void PopupSubmitErrorDialog(string stackTrace, System.Action onSubmit, System.Action onQuit, System.Action onContinue)
	{
		this.mode = ReportErrorDialog.Mode.SubmitError;
		this.m_stackTrace = stackTrace;
		this.submitAction = onSubmit;
		this.quitAction = onQuit;
		this.continueAction = onContinue;
	}

	// Token: 0x06005B0C RID: 23308 RVA: 0x00210EA8 File Offset: 0x0020F0A8
	public void PopupDisableModsDialog(string stackTrace, System.Action onQuit, System.Action onContinue)
	{
		this.mode = ReportErrorDialog.Mode.DisableMods;
		this.m_stackTrace = stackTrace;
		this.quitAction = onQuit;
		this.continueAction = onContinue;
	}

	// Token: 0x06005B0D RID: 23309 RVA: 0x00210EC8 File Offset: 0x0020F0C8
	public void OnSelect_MOREINFO()
	{
		this.StackTrace.GetComponentInChildren<LocText>().text = this.m_stackTrace;
		this.StackTrace.SetActive(true);
		this.moreInfoButton.GetComponentInChildren<LocText>().text = UI.CRASHSCREEN.COPYTOCLIPBOARDBUTTON;
		this.moreInfoButton.ClearOnClick();
		this.moreInfoButton.onClick += this.OnSelect_COPYTOCLIPBOARD;
	}

	// Token: 0x06005B0E RID: 23310 RVA: 0x00210F33 File Offset: 0x0020F133
	public void OnSelect_COPYTOCLIPBOARD()
	{
		TextEditor textEditor = new TextEditor();
		textEditor.text = this.m_stackTrace + "\nBuild: " + BuildWatermark.GetBuildText();
		textEditor.SelectAll();
		textEditor.Copy();
	}

	// Token: 0x06005B0F RID: 23311 RVA: 0x00210F60 File Offset: 0x0020F160
	public void OnSelect_SUBMIT()
	{
		this.submitButton.GetComponentInChildren<LocText>().text = UI.CRASHSCREEN.REPORTING;
		this.submitButton.GetComponent<KButton>().isInteractable = false;
		this.Submit();
	}

	// Token: 0x06005B10 RID: 23312 RVA: 0x00210F93 File Offset: 0x0020F193
	public void OnSelect_QUIT()
	{
		if (this.quitAction != null)
		{
			this.quitAction();
		}
	}

	// Token: 0x06005B11 RID: 23313 RVA: 0x00210FA8 File Offset: 0x0020F1A8
	public void OnSelect_CONTINUE()
	{
		if (this.continueAction != null)
		{
			this.continueAction();
		}
	}

	// Token: 0x06005B12 RID: 23314 RVA: 0x00210FBD File Offset: 0x0020F1BD
	public void OpenRefMessage()
	{
		this.submitButton.gameObject.SetActive(false);
		this.referenceMessage.SetActive(true);
	}

	// Token: 0x06005B13 RID: 23315 RVA: 0x00210FDC File Offset: 0x0020F1DC
	public string UserMessage()
	{
		return this.messageInputField.text;
	}

	// Token: 0x06005B14 RID: 23316 RVA: 0x00210FE9 File Offset: 0x0020F1E9
	private void Submit()
	{
		this.submitAction();
		this.OpenRefMessage();
	}

	// Token: 0x04003DB0 RID: 15792
	private System.Action submitAction;

	// Token: 0x04003DB1 RID: 15793
	private System.Action quitAction;

	// Token: 0x04003DB2 RID: 15794
	private System.Action continueAction;

	// Token: 0x04003DB3 RID: 15795
	public KInputTextField messageInputField;

	// Token: 0x04003DB4 RID: 15796
	public GameObject referenceMessage;

	// Token: 0x04003DB5 RID: 15797
	private string m_stackTrace;

	// Token: 0x04003DB6 RID: 15798
	[SerializeField]
	private KButton submitButton;

	// Token: 0x04003DB7 RID: 15799
	[SerializeField]
	private KButton moreInfoButton;

	// Token: 0x04003DB8 RID: 15800
	[SerializeField]
	private KButton quitButton;

	// Token: 0x04003DB9 RID: 15801
	[SerializeField]
	private KButton continueGameButton;

	// Token: 0x04003DBA RID: 15802
	[SerializeField]
	private LocText CrashLabel;

	// Token: 0x04003DBB RID: 15803
	[SerializeField]
	private GameObject CrashDescription;

	// Token: 0x04003DBC RID: 15804
	[SerializeField]
	private GameObject ModsInfo;

	// Token: 0x04003DBD RID: 15805
	[SerializeField]
	private GameObject StackTrace;

	// Token: 0x04003DBE RID: 15806
	[SerializeField]
	private GameObject modEntryPrefab;

	// Token: 0x04003DBF RID: 15807
	[SerializeField]
	private Transform modEntryParent;

	// Token: 0x04003DC0 RID: 15808
	private ReportErrorDialog.Mode mode;

	// Token: 0x02001A0B RID: 6667
	private enum Mode
	{
		// Token: 0x04007655 RID: 30293
		SubmitError,
		// Token: 0x04007656 RID: 30294
		DisableMods
	}
}
