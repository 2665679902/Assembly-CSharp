using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A08 RID: 2568
public class InspectSaveScreen : KModalScreen
{
	// Token: 0x06004D1A RID: 19738 RVA: 0x001B1ECD File Offset: 0x001B00CD
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.closeButton.onClick += this.CloseScreen;
		this.deleteSaveBtn.onClick += this.DeleteSave;
	}

	// Token: 0x06004D1B RID: 19739 RVA: 0x001B1F03 File Offset: 0x001B0103
	private void CloseScreen()
	{
		LoadScreen.Instance.Show(true);
		this.Show(false);
	}

	// Token: 0x06004D1C RID: 19740 RVA: 0x001B1F17 File Offset: 0x001B0117
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (!show)
		{
			this.buttonPool.ClearAll();
			this.buttonFileMap.Clear();
		}
	}

	// Token: 0x06004D1D RID: 19741 RVA: 0x001B1F3C File Offset: 0x001B013C
	public void SetTarget(string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			global::Debug.LogError("The directory path provided is empty.");
			this.Show(false);
			return;
		}
		if (!Directory.Exists(path))
		{
			global::Debug.LogError("The directory provided does not exist.");
			this.Show(false);
			return;
		}
		if (this.buttonPool == null)
		{
			this.buttonPool = new UIPool<KButton>(this.backupBtnPrefab);
		}
		this.currentPath = path;
		List<string> list = (from filename in Directory.GetFiles(path)
			where Path.GetExtension(filename).ToLower() == ".sav"
			orderby File.GetLastWriteTime(filename) descending
			select filename).ToList<string>();
		string text = list[0];
		if (File.Exists(text))
		{
			this.mainSaveBtn.gameObject.SetActive(true);
			this.AddNewSave(this.mainSaveBtn, text);
		}
		else
		{
			this.mainSaveBtn.gameObject.SetActive(false);
		}
		if (list.Count > 1)
		{
			for (int i = 1; i < list.Count; i++)
			{
				this.AddNewSave(this.buttonPool.GetFreeElement(this.buttonGroup, true), list[i]);
			}
		}
		this.Show(true);
	}

	// Token: 0x06004D1E RID: 19742 RVA: 0x001B2074 File Offset: 0x001B0274
	private void ConfirmDoAction(string message, System.Action action)
	{
		if (this.confirmScreen == null)
		{
			this.confirmScreen = Util.KInstantiateUI<ConfirmDialogScreen>(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, base.gameObject, false);
			this.confirmScreen.PopupConfirmDialog(message, action, delegate
			{
			}, null, null, null, null, null, null);
			this.confirmScreen.GetComponent<LayoutElement>().ignoreLayout = true;
			this.confirmScreen.gameObject.SetActive(true);
		}
	}

	// Token: 0x06004D1F RID: 19743 RVA: 0x001B2104 File Offset: 0x001B0304
	private void DeleteSave()
	{
		if (string.IsNullOrEmpty(this.currentPath))
		{
			global::Debug.LogError("The path provided is not valid and cannot be deleted.");
			return;
		}
		this.ConfirmDoAction(UI.FRONTEND.LOADSCREEN.CONFIRMDELETE, delegate
		{
			string[] files = Directory.GetFiles(this.currentPath);
			for (int i = 0; i < files.Length; i++)
			{
				File.Delete(files[i]);
			}
			Directory.Delete(this.currentPath);
			this.CloseScreen();
		});
	}

	// Token: 0x06004D20 RID: 19744 RVA: 0x001B213A File Offset: 0x001B033A
	private void AddNewSave(KButton btn, string file)
	{
	}

	// Token: 0x06004D21 RID: 19745 RVA: 0x001B213C File Offset: 0x001B033C
	private void ButtonClicked(KButton btn)
	{
		LoadingOverlay.Load(delegate
		{
			this.Load(this.buttonFileMap[btn]);
		});
	}

	// Token: 0x06004D22 RID: 19746 RVA: 0x001B2161 File Offset: 0x001B0361
	private void Load(string filename)
	{
		if (Game.Instance != null)
		{
			LoadScreen.ForceStopGame();
		}
		SaveLoader.SetActiveSaveFilePath(filename);
		App.LoadScene("backend");
		this.Deactivate();
	}

	// Token: 0x06004D23 RID: 19747 RVA: 0x001B218B File Offset: 0x001B038B
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.CloseScreen();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x040032D6 RID: 13014
	[SerializeField]
	private KButton closeButton;

	// Token: 0x040032D7 RID: 13015
	[SerializeField]
	private KButton mainSaveBtn;

	// Token: 0x040032D8 RID: 13016
	[SerializeField]
	private KButton backupBtnPrefab;

	// Token: 0x040032D9 RID: 13017
	[SerializeField]
	private KButton deleteSaveBtn;

	// Token: 0x040032DA RID: 13018
	[SerializeField]
	private GameObject buttonGroup;

	// Token: 0x040032DB RID: 13019
	private UIPool<KButton> buttonPool;

	// Token: 0x040032DC RID: 13020
	private Dictionary<KButton, string> buttonFileMap = new Dictionary<KButton, string>();

	// Token: 0x040032DD RID: 13021
	private ConfirmDialogScreen confirmScreen;

	// Token: 0x040032DE RID: 13022
	private string currentPath = "";
}
