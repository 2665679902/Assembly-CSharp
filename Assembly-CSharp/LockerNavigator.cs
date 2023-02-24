using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AEB RID: 2795
public class LockerNavigator : KModalScreen
{
	// Token: 0x1700063F RID: 1599
	// (get) Token: 0x060055AC RID: 21932 RVA: 0x001EF8E3 File Offset: 0x001EDAE3
	public GameObject ContentSlot
	{
		get
		{
			return this.slot.gameObject;
		}
	}

	// Token: 0x060055AD RID: 21933 RVA: 0x001EF8F0 File Offset: 0x001EDAF0
	protected override void OnActivate()
	{
		LockerNavigator.Instance = this;
		this.Show(false);
		this.backButton.onClick += this.OnClickBack;
	}

	// Token: 0x060055AE RID: 21934 RVA: 0x001EF916 File Offset: 0x001EDB16
	public override float GetSortKey()
	{
		return 41f;
	}

	// Token: 0x060055AF RID: 21935 RVA: 0x001EF91D File Offset: 0x001EDB1D
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.PopScreen();
		}
		base.OnKeyDown(e);
	}

	// Token: 0x060055B0 RID: 21936 RVA: 0x001EF93F File Offset: 0x001EDB3F
	public override void Show(bool show = true)
	{
		base.Show(show);
		if (!show)
		{
			this.PopAllScreens();
		}
		StreamedTextures.SetBundlesLoaded(show);
	}

	// Token: 0x060055B1 RID: 21937 RVA: 0x001EF957 File Offset: 0x001EDB57
	private void OnClickBack()
	{
		this.PopScreen();
	}

	// Token: 0x060055B2 RID: 21938 RVA: 0x001EF960 File Offset: 0x001EDB60
	public void PushScreen(GameObject screen, System.Action onClose = null)
	{
		if (screen == null)
		{
			return;
		}
		if (this.navigationHistory.Count == 0)
		{
			this.Show(true);
			if (!LockerNavigator.didDisplayDataCollectionWarningPopupOnce && KPrivacyPrefs.instance.disableDataCollection)
			{
				LockerNavigator.MakeDataCollectionWarningPopup(base.gameObject.transform.parent.gameObject);
				LockerNavigator.didDisplayDataCollectionWarningPopupOnce = true;
			}
		}
		if (this.navigationHistory.Count > 0 && screen == this.navigationHistory[this.navigationHistory.Count - 1].screen)
		{
			return;
		}
		if (this.navigationHistory.Count > 0)
		{
			this.navigationHistory[this.navigationHistory.Count - 1].screen.SetActive(false);
		}
		this.navigationHistory.Add(new LockerNavigator.HistoryEntry(screen, onClose));
		this.navigationHistory[this.navigationHistory.Count - 1].screen.SetActive(true);
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		this.RefreshButtons();
	}

	// Token: 0x060055B3 RID: 21939 RVA: 0x001EFA78 File Offset: 0x001EDC78
	public bool PopScreen()
	{
		while (this.preventScreenPop.Count > 0)
		{
			int num = this.preventScreenPop.Count - 1;
			Func<bool> func = this.preventScreenPop[num];
			this.preventScreenPop.RemoveAt(num);
			if (func())
			{
				return true;
			}
		}
		int num2 = this.navigationHistory.Count - 1;
		LockerNavigator.HistoryEntry historyEntry = this.navigationHistory[num2];
		historyEntry.screen.SetActive(false);
		if (historyEntry.onClose.IsSome())
		{
			historyEntry.onClose.Unwrap()();
		}
		this.navigationHistory.RemoveAt(num2);
		if (this.navigationHistory.Count > 0)
		{
			this.navigationHistory[this.navigationHistory.Count - 1].screen.SetActive(true);
			this.RefreshButtons();
			return true;
		}
		this.Show(false);
		MusicManager.instance.SetSongParameter("Music_SupplyCloset", "SupplyClosetView", "initial", true);
		return false;
	}

	// Token: 0x060055B4 RID: 21940 RVA: 0x001EFB74 File Offset: 0x001EDD74
	public void PopAllScreens()
	{
		if (this.navigationHistory.Count == 0 && this.preventScreenPop.Count == 0)
		{
			return;
		}
		int num = 0;
		while (this.PopScreen())
		{
			if (num > 100)
			{
				DebugUtil.DevAssert(false, string.Format("Can't close all LockerNavigator screens, hit limit of trying to close {0} screens", 100), null);
				return;
			}
			num++;
		}
	}

	// Token: 0x060055B5 RID: 21941 RVA: 0x001EFBCA File Offset: 0x001EDDCA
	private void RefreshButtons()
	{
		this.backButton.isInteractable = true;
	}

	// Token: 0x060055B6 RID: 21942 RVA: 0x001EFBD8 File Offset: 0x001EDDD8
	public void ShowDialogPopup(Action<InfoDialogScreen> configureDialogFn)
	{
		InfoDialogScreen dialog = Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, this.ContentSlot, false);
		configureDialogFn(dialog);
		dialog.Activate();
		dialog.gameObject.AddOrGet<LayoutElement>().ignoreLayout = true;
		dialog.gameObject.AddOrGet<RectTransform>().Fill();
		Func<bool> preventScreenPopFn = delegate
		{
			dialog.Deactivate();
			return true;
		};
		this.preventScreenPop.Add(preventScreenPopFn);
		InfoDialogScreen dialog2 = dialog;
		dialog2.onDeactivateFn = (System.Action)Delegate.Combine(dialog2.onDeactivateFn, new System.Action(delegate
		{
			this.preventScreenPop.Remove(preventScreenPopFn);
		}));
	}

	// Token: 0x060055B7 RID: 21943 RVA: 0x001EFCA0 File Offset: 0x001EDEA0
	public static void MakeDataCollectionWarningPopup(GameObject fullscreenParent)
	{
		Action<InfoDialogScreen> <>9__2;
		LockerNavigator.Instance.ShowDialogPopup(delegate(InfoDialogScreen dialog)
		{
			InfoDialogScreen infoDialogScreen = dialog.SetHeader(UI.LOCKER_NAVIGATOR.DATA_COLLECTION_WARNING_POPUP.HEADER).AddPlainText(UI.LOCKER_NAVIGATOR.DATA_COLLECTION_WARNING_POPUP.BODY).AddOption(UI.LOCKER_NAVIGATOR.DATA_COLLECTION_WARNING_POPUP.BUTTON_OK, delegate(InfoDialogScreen d)
			{
				d.Deactivate();
			}, true);
			string text = UI.LOCKER_NAVIGATOR.DATA_COLLECTION_WARNING_POPUP.BUTTON_OPEN_SETTINGS;
			Action<InfoDialogScreen> action;
			if ((action = <>9__2) == null)
			{
				action = (<>9__2 = delegate(InfoDialogScreen d)
				{
					d.Deactivate();
					LockerNavigator.Instance.PopAllScreens();
					LockerMenuScreen.Instance.Show(false);
					Util.KInstantiateUI<OptionsMenuScreen>(ScreenPrefabs.Instance.OptionsScreen.gameObject, fullscreenParent, true).ShowMetricsScreen();
				});
			}
			infoDialogScreen.AddOption(text, action, false);
		});
	}

	// Token: 0x04003A31 RID: 14897
	public static LockerNavigator Instance;

	// Token: 0x04003A32 RID: 14898
	[SerializeField]
	private RectTransform slot;

	// Token: 0x04003A33 RID: 14899
	[SerializeField]
	private KButton backButton;

	// Token: 0x04003A34 RID: 14900
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003A35 RID: 14901
	[SerializeField]
	public GameObject kleiInventoryScreen;

	// Token: 0x04003A36 RID: 14902
	[SerializeField]
	public GameObject duplicantCatalogueScreen;

	// Token: 0x04003A37 RID: 14903
	[SerializeField]
	public GameObject outfitDesignerScreen;

	// Token: 0x04003A38 RID: 14904
	[SerializeField]
	public GameObject outfitBrowserScreen;

	// Token: 0x04003A39 RID: 14905
	[SerializeField]
	public GameObject joyResponseDesignerScreen;

	// Token: 0x04003A3A RID: 14906
	private const string LOCKER_MENU_MUSIC = "Music_SupplyCloset";

	// Token: 0x04003A3B RID: 14907
	private const string MUSIC_PARAMETER = "SupplyClosetView";

	// Token: 0x04003A3C RID: 14908
	private List<LockerNavigator.HistoryEntry> navigationHistory = new List<LockerNavigator.HistoryEntry>();

	// Token: 0x04003A3D RID: 14909
	private Dictionary<string, GameObject> screens = new Dictionary<string, GameObject>();

	// Token: 0x04003A3E RID: 14910
	private static bool didDisplayDataCollectionWarningPopupOnce;

	// Token: 0x04003A3F RID: 14911
	public List<Func<bool>> preventScreenPop = new List<Func<bool>>();

	// Token: 0x02001967 RID: 6503
	public readonly struct HistoryEntry
	{
		// Token: 0x0600902A RID: 36906 RVA: 0x003120B0 File Offset: 0x003102B0
		public HistoryEntry(GameObject screen, System.Action onClose = null)
		{
			this.screen = screen;
			this.onClose = onClose;
		}

		// Token: 0x04007440 RID: 29760
		public readonly GameObject screen;

		// Token: 0x04007441 RID: 29761
		public readonly Option<System.Action> onClose;
	}
}
