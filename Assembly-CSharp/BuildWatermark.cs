using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class BuildWatermark : KScreen
{
	// Token: 0x0600002C RID: 44 RVA: 0x00002BA8 File Offset: 0x00000DA8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		BuildWatermark.Instance = this;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002BB6 File Offset: 0x00000DB6
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.RefreshText();
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002BC4 File Offset: 0x00000DC4
	public static string GetBuildText()
	{
		string text = (DistributionPlatform.Initialized ? (LaunchInitializer.BuildPrefix() + "-") : "??-");
		if (Application.isEditor)
		{
			text += "<EDITOR>";
		}
		else
		{
			text += 544519U.ToString();
			if (DistributionPlatform.Initialized)
			{
				text = text + "-" + DlcManager.GetActiveContentLetters();
			}
			else
			{
				text += "-?";
			}
			if (DebugHandler.enabled)
			{
				text += "D";
			}
		}
		return text;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002C54 File Offset: 0x00000E54
	public void RefreshText()
	{
		bool flag = true;
		bool flag2 = DistributionPlatform.Initialized && DistributionPlatform.Inst.IsArchiveBranch;
		string buildText = BuildWatermark.GetBuildText();
		this.button.ClearOnClick();
		if (flag)
		{
			this.textDisplay.SetText(string.Format(UI.DEVELOPMENTBUILDS.WATERMARK, buildText));
			this.toolTip.ClearMultiStringTooltip();
		}
		else
		{
			this.textDisplay.SetText(string.Format(UI.DEVELOPMENTBUILDS.TESTING_WATERMARK, buildText));
			this.toolTip.SetSimpleTooltip(UI.DEVELOPMENTBUILDS.TESTING_TOOLTIP);
			if (this.interactable)
			{
				this.button.onClick += this.ShowTestingMessage;
			}
		}
		foreach (GameObject gameObject in this.archiveIcons)
		{
			gameObject.SetActive(flag && flag2);
		}
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002D4C File Offset: 0x00000F4C
	private void ShowTestingMessage()
	{
		Util.KInstantiateUI<ConfirmDialogScreen>(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, Global.Instance.globalCanvas, true).PopupConfirmDialog(UI.DEVELOPMENTBUILDS.TESTING_MESSAGE, delegate
		{
			App.OpenWebURL("https://forums.kleientertainment.com/klei-bug-tracker/oni/");
		}, delegate
		{
		}, null, null, UI.DEVELOPMENTBUILDS.TESTING_MESSAGE_TITLE, UI.DEVELOPMENTBUILDS.TESTING_MORE_INFO, null, null);
	}

	// Token: 0x04000025 RID: 37
	public bool interactable = true;

	// Token: 0x04000026 RID: 38
	public LocText textDisplay;

	// Token: 0x04000027 RID: 39
	public ToolTip toolTip;

	// Token: 0x04000028 RID: 40
	public KButton button;

	// Token: 0x04000029 RID: 41
	public List<GameObject> archiveIcons;

	// Token: 0x0400002A RID: 42
	public static BuildWatermark Instance;
}
