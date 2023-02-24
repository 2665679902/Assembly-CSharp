using System;
using Klei;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000778 RID: 1912
[AddComponentMenu("KMonoBehaviour/scripts/FileErrorReporter")]
public class FileErrorReporter : KMonoBehaviour
{
	// Token: 0x060034A6 RID: 13478 RVA: 0x0011BE97 File Offset: 0x0011A097
	protected override void OnSpawn()
	{
		this.OnFileError();
		FileUtil.onErrorMessage += this.OnFileError;
	}

	// Token: 0x060034A7 RID: 13479 RVA: 0x0011BEB0 File Offset: 0x0011A0B0
	private void OnFileError()
	{
		if (FileUtil.errorType == FileUtil.ErrorType.None)
		{
			return;
		}
		string text;
		switch (FileUtil.errorType)
		{
		case FileUtil.ErrorType.UnauthorizedAccess:
			text = string.Format(UI.FRONTEND.SUPPORTWARNINGS.IO_UNAUTHORIZED, FileUtil.errorSubject);
			goto IL_65;
		case FileUtil.ErrorType.IOError:
			text = string.Format(UI.FRONTEND.SUPPORTWARNINGS.IO_SUFFICIENT_SPACE, FileUtil.errorSubject);
			goto IL_65;
		}
		text = string.Format(UI.FRONTEND.SUPPORTWARNINGS.IO_UNKNOWN, FileUtil.errorSubject);
		IL_65:
		GameObject gameObject;
		if (FrontEndManager.Instance != null)
		{
			gameObject = FrontEndManager.Instance.gameObject;
		}
		else if (GameScreenManager.Instance != null && GameScreenManager.Instance.ssOverlayCanvas != null)
		{
			gameObject = GameScreenManager.Instance.ssOverlayCanvas;
		}
		else
		{
			gameObject = new GameObject();
			gameObject.name = "FileErrorCanvas";
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			Canvas canvas = gameObject.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.TexCoord1;
			canvas.sortingOrder = 10;
			gameObject.AddComponent<GraphicRaycaster>();
		}
		if ((FileUtil.exceptionMessage != null || FileUtil.exceptionStackTrace != null) && !KCrashReporter.hasReportedError)
		{
			KCrashReporter.ReportError(FileUtil.exceptionMessage, FileUtil.exceptionStackTrace, null, null, null, "");
		}
		ConfirmDialogScreen component = Util.KInstantiateUI(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, gameObject, true).GetComponent<ConfirmDialogScreen>();
		component.PopupConfirmDialog(text, null, null, null, null, null, null, null, null);
		UnityEngine.Object.DontDestroyOnLoad(component.gameObject);
	}

	// Token: 0x060034A8 RID: 13480 RVA: 0x0011C001 File Offset: 0x0011A201
	private void OpenMoreInfo()
	{
	}
}
