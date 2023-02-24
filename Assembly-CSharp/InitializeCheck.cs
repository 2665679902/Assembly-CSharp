using System;
using System.IO;
using ProcGenGame;
using STRINGS;
using UnityEngine;

// Token: 0x020007B9 RID: 1977
public class InitializeCheck : MonoBehaviour
{
	// Token: 0x17000418 RID: 1048
	// (get) Token: 0x0600383A RID: 14394 RVA: 0x001377E0 File Offset: 0x001359E0
	// (set) Token: 0x0600383B RID: 14395 RVA: 0x001377E7 File Offset: 0x001359E7
	public static InitializeCheck.SavePathIssue savePathState { get; private set; }

	// Token: 0x0600383C RID: 14396 RVA: 0x001377F0 File Offset: 0x001359F0
	private void Awake()
	{
		this.CheckForSavePathIssue();
		if (InitializeCheck.savePathState == InitializeCheck.SavePathIssue.Ok && !KCrashReporter.hasCrash)
		{
			AudioMixer.Create();
			App.LoadScene("frontend");
			return;
		}
		Canvas canvas = base.gameObject.AddComponent<Canvas>();
		canvas.rectTransform().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 500f);
		canvas.rectTransform().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 500f);
		Camera camera = base.gameObject.AddComponent<Camera>();
		camera.orthographic = true;
		camera.orthographicSize = 200f;
		camera.backgroundColor = Color.black;
		camera.clearFlags = CameraClearFlags.Color;
		camera.nearClipPlane = 0f;
		global::Debug.Log("Cannot initialize filesystem. [" + InitializeCheck.savePathState.ToString() + "]");
		Localization.Initialize();
		GameObject.Find("BootCanvas").SetActive(false);
		this.ShowFileErrorDialogs();
	}

	// Token: 0x0600383D RID: 14397 RVA: 0x001378C9 File Offset: 0x00135AC9
	private GameObject CreateUIRoot()
	{
		return Util.KInstantiate(this.rootCanvasPrefab, null, "CanvasRoot");
	}

	// Token: 0x0600383E RID: 14398 RVA: 0x001378DC File Offset: 0x00135ADC
	private void ShowErrorDialog(string msg)
	{
		GameObject gameObject = this.CreateUIRoot();
		Util.KInstantiateUI<ConfirmDialogScreen>(this.confirmDialogScreen.gameObject, gameObject, true).PopupConfirmDialog(msg, new System.Action(this.Quit), null, null, null, null, null, null, this.sadDupe);
	}

	// Token: 0x0600383F RID: 14399 RVA: 0x00137920 File Offset: 0x00135B20
	private void ShowFileErrorDialogs()
	{
		string text = null;
		switch (InitializeCheck.savePathState)
		{
		case InitializeCheck.SavePathIssue.WriteTestFail:
			text = string.Format(UI.FRONTEND.SUPPORTWARNINGS.SAVE_DIRECTORY_READ_ONLY, SaveLoader.GetSavePrefix());
			break;
		case InitializeCheck.SavePathIssue.SpaceTestFail:
			text = string.Format(UI.FRONTEND.SUPPORTWARNINGS.SAVE_DIRECTORY_INSUFFICIENT_SPACE, SaveLoader.GetSavePrefix());
			break;
		case InitializeCheck.SavePathIssue.WorldGenFilesFail:
			text = string.Format(UI.FRONTEND.SUPPORTWARNINGS.WORLD_GEN_FILES, WorldGen.WORLDGEN_SAVE_FILENAME + "\n" + WorldGen.GetSIMSaveFilename(-1));
			break;
		}
		if (text != null)
		{
			this.ShowErrorDialog(text);
		}
	}

	// Token: 0x06003840 RID: 14400 RVA: 0x001379A8 File Offset: 0x00135BA8
	private void CheckForSavePathIssue()
	{
		if (this.test_issue != InitializeCheck.SavePathIssue.Ok)
		{
			InitializeCheck.savePathState = this.test_issue;
			return;
		}
		string savePrefix = SaveLoader.GetSavePrefix();
		InitializeCheck.savePathState = InitializeCheck.SavePathIssue.Ok;
		try
		{
			SaveLoader.GetSavePrefixAndCreateFolder();
			using (FileStream fileStream = File.Open(savePrefix + InitializeCheck.testFile, FileMode.Create, FileAccess.Write))
			{
				new BinaryWriter(fileStream);
				fileStream.Close();
			}
		}
		catch
		{
			InitializeCheck.savePathState = InitializeCheck.SavePathIssue.WriteTestFail;
			goto IL_E7;
		}
		using (FileStream fileStream2 = File.Open(savePrefix + InitializeCheck.testSave, FileMode.Create, FileAccess.Write))
		{
			try
			{
				fileStream2.SetLength(15000000L);
				new BinaryWriter(fileStream2);
				fileStream2.Close();
			}
			catch
			{
				fileStream2.Close();
				InitializeCheck.savePathState = InitializeCheck.SavePathIssue.SpaceTestFail;
				goto IL_E7;
			}
		}
		try
		{
			using (File.Open(WorldGen.WORLDGEN_SAVE_FILENAME, FileMode.Append))
			{
			}
			using (File.Open(WorldGen.GetSIMSaveFilename(-1), FileMode.Append))
			{
			}
		}
		catch
		{
			InitializeCheck.savePathState = InitializeCheck.SavePathIssue.WorldGenFilesFail;
		}
		IL_E7:
		try
		{
			if (File.Exists(savePrefix + InitializeCheck.testFile))
			{
				File.Delete(savePrefix + InitializeCheck.testFile);
			}
			if (File.Exists(savePrefix + InitializeCheck.testSave))
			{
				File.Delete(savePrefix + InitializeCheck.testSave);
			}
		}
		catch
		{
		}
	}

	// Token: 0x06003841 RID: 14401 RVA: 0x00137B4C File Offset: 0x00135D4C
	private void Quit()
	{
		global::Debug.Log("Quitting...");
		App.Quit();
	}

	// Token: 0x0400257A RID: 9594
	private static readonly string testFile = "testfile";

	// Token: 0x0400257B RID: 9595
	private static readonly string testSave = "testsavefile";

	// Token: 0x0400257C RID: 9596
	public Canvas rootCanvasPrefab;

	// Token: 0x0400257D RID: 9597
	public ConfirmDialogScreen confirmDialogScreen;

	// Token: 0x0400257E RID: 9598
	public Sprite sadDupe;

	// Token: 0x0400257F RID: 9599
	private InitializeCheck.SavePathIssue test_issue;

	// Token: 0x0200151A RID: 5402
	public enum SavePathIssue
	{
		// Token: 0x04006598 RID: 26008
		Ok,
		// Token: 0x04006599 RID: 26009
		WriteTestFail,
		// Token: 0x0400659A RID: 26010
		SpaceTestFail,
		// Token: 0x0400659B RID: 26011
		WorldGenFilesFail
	}
}
