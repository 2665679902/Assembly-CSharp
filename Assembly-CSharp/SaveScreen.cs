using System;
using System.IO;
using STRINGS;
using TMPro;
using UnityEngine;

// Token: 0x02000A1B RID: 2587
public class SaveScreen : KModalScreen
{
	// Token: 0x06004E02 RID: 19970 RVA: 0x001B8790 File Offset: 0x001B6990
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.oldSaveButtonPrefab.gameObject.SetActive(false);
		this.newSaveButton.onClick += this.OnClickNewSave;
		this.closeButton.onClick += this.Deactivate;
	}

	// Token: 0x06004E03 RID: 19971 RVA: 0x001B87E4 File Offset: 0x001B69E4
	protected override void OnCmpEnable()
	{
		foreach (SaveLoader.SaveFileEntry saveFileEntry in SaveLoader.GetAllColonyFiles(true, SearchOption.TopDirectoryOnly))
		{
			this.AddExistingSaveFile(saveFileEntry.path);
		}
		SpeedControlScreen.Instance.Pause(true, false);
	}

	// Token: 0x06004E04 RID: 19972 RVA: 0x001B884C File Offset: 0x001B6A4C
	protected override void OnDeactivate()
	{
		SpeedControlScreen.Instance.Unpause(true);
		base.OnDeactivate();
	}

	// Token: 0x06004E05 RID: 19973 RVA: 0x001B8860 File Offset: 0x001B6A60
	private void AddExistingSaveFile(string filename)
	{
		KButton kbutton = Util.KInstantiateUI<KButton>(this.oldSaveButtonPrefab.gameObject, this.oldSavesRoot.gameObject, true);
		HierarchyReferences component = kbutton.GetComponent<HierarchyReferences>();
		LocText component2 = component.GetReference<RectTransform>("Title").GetComponent<LocText>();
		TMP_Text component3 = component.GetReference<RectTransform>("Date").GetComponent<LocText>();
		System.DateTime lastWriteTime = File.GetLastWriteTime(filename);
		component2.text = string.Format("{0}", Path.GetFileNameWithoutExtension(filename));
		component3.text = string.Format("{0:H:mm:ss}" + Localization.GetFileDateFormat(0), lastWriteTime);
		kbutton.onClick += delegate
		{
			this.Save(filename);
		};
	}

	// Token: 0x06004E06 RID: 19974 RVA: 0x001B891C File Offset: 0x001B6B1C
	public static string GetValidSaveFilename(string filename)
	{
		string text = ".sav";
		if (Path.GetExtension(filename).ToLower() != text)
		{
			filename += text;
		}
		return filename;
	}

	// Token: 0x06004E07 RID: 19975 RVA: 0x001B894C File Offset: 0x001B6B4C
	public void Save(string filename)
	{
		filename = SaveScreen.GetValidSaveFilename(filename);
		if (File.Exists(filename))
		{
			ScreenPrefabs.Instance.ConfirmDoAction(string.Format(UI.FRONTEND.SAVESCREEN.OVERWRITEMESSAGE, Path.GetFileNameWithoutExtension(filename)), delegate
			{
				this.DoSave(filename);
			}, base.transform.parent);
			return;
		}
		this.DoSave(filename);
	}

	// Token: 0x06004E08 RID: 19976 RVA: 0x001B89D4 File Offset: 0x001B6BD4
	private void DoSave(string filename)
	{
		try
		{
			SaveLoader.Instance.Save(filename, false, true);
			this.Deactivate();
		}
		catch (IOException ex)
		{
			IOException ex2 = ex;
			IOException e = ex2;
			Util.KInstantiateUI(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, base.transform.parent.gameObject, true).GetComponent<ConfirmDialogScreen>().PopupConfirmDialog(string.Format(UI.FRONTEND.SAVESCREEN.IO_ERROR, e.ToString()), delegate
			{
				this.Deactivate();
			}, null, UI.FRONTEND.SAVESCREEN.REPORT_BUG, delegate
			{
				KCrashReporter.ReportError(e.Message, e.StackTrace.ToString(), null, null, null, "");
			}, null, null, null, null);
		}
	}

	// Token: 0x06004E09 RID: 19977 RVA: 0x001B8A8C File Offset: 0x001B6C8C
	public void OnClickNewSave()
	{
		FileNameDialog fileNameDialog = (FileNameDialog)KScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.FileNameDialog.gameObject, base.transform.parent.gameObject);
		string activeSaveFilePath = SaveLoader.GetActiveSaveFilePath();
		if (activeSaveFilePath != null)
		{
			string text = SaveLoader.GetOriginalSaveFileName(activeSaveFilePath);
			text = Path.GetFileNameWithoutExtension(text);
			fileNameDialog.SetTextAndSelect(text);
		}
		fileNameDialog.onConfirm = delegate(string filename)
		{
			filename = Path.Combine(SaveLoader.GetActiveSaveColonyFolder(), filename);
			this.Save(filename);
		};
	}

	// Token: 0x06004E0A RID: 19978 RVA: 0x001B8AF8 File Offset: 0x001B6CF8
	public override void OnKeyUp(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape))
		{
			this.Deactivate();
		}
		e.Consumed = true;
	}

	// Token: 0x06004E0B RID: 19979 RVA: 0x001B8B10 File Offset: 0x001B6D10
	public override void OnKeyDown(KButtonEvent e)
	{
		e.Consumed = true;
	}

	// Token: 0x04003387 RID: 13191
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003388 RID: 13192
	[SerializeField]
	private KButton newSaveButton;

	// Token: 0x04003389 RID: 13193
	[SerializeField]
	private KButton oldSaveButtonPrefab;

	// Token: 0x0400338A RID: 13194
	[SerializeField]
	private Transform oldSavesRoot;
}
