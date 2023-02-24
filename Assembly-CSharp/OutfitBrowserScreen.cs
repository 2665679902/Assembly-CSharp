using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Database;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000B4C RID: 2892
public class OutfitBrowserScreen : KMonoBehaviour
{
	// Token: 0x0600599C RID: 22940 RVA: 0x0020662C File Offset: 0x0020482C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.galleryGridItemPool = new UIPrefabLocalPool(this.gridItemPrefab, this.galleryGridContent.gameObject);
		this.gridLayouter = new GridLayouter
		{
			minCellSize = 112f,
			maxCellSize = 144f,
			targetGridLayout = this.galleryGridContent.GetComponent<GridLayoutGroup>()
		};
		this.pickOutfitButton.onClick += this.OnClickPickOutfit;
		this.editOutfitButton.onClick += delegate
		{
			new OutfitDesignerScreenConfig(this.selectedOutfit, this.Config.minionPersonality, this.Config.targetMinionInstance, new Action<ClothingOutfitTarget>(this.OnOutfitDesignerWritesToOutfitTarget)).ApplyAndOpenScreen();
		};
		this.renameOutfitButton.onClick += delegate
		{
			OutfitBrowserScreen.MakeRenamePopup(this.inputFieldPrefab, this.selectedOutfit.Value, () => this.selectedOutfit.Value.ReadName(), delegate(string new_name)
			{
				this.selectedOutfit.Value.WriteName(new_name);
				OutfitBrowserScreenConfig outfitBrowserScreenConfig = this.Config;
				if (!outfitBrowserScreenConfig.minionPersonality.HasValue)
				{
					this.lastMannequinSelectedOutfit = this.selectedOutfit;
				}
				outfitBrowserScreenConfig = this.Config;
				this.Configure(outfitBrowserScreenConfig.WithOutfit(this.selectedOutfit));
			});
		};
		this.deleteOutfitButton.onClick += delegate
		{
			OutfitBrowserScreen.MakeDeletePopup(this.selectedOutfit.Value, delegate
			{
				this.selectedOutfit.Value.Delete();
				this.Configure(this.Config.WithOutfit(Option.None));
			});
		};
	}

	// Token: 0x1700065E RID: 1630
	// (get) Token: 0x0600599D RID: 22941 RVA: 0x002066E9 File Offset: 0x002048E9
	// (set) Token: 0x0600599E RID: 22942 RVA: 0x002066F1 File Offset: 0x002048F1
	public OutfitBrowserScreenConfig Config { get; private set; }

	// Token: 0x0600599F RID: 22943 RVA: 0x002066FC File Offset: 0x002048FC
	protected override void OnCmpEnable()
	{
		if (this.isFirstDisplay)
		{
			this.isFirstDisplay = false;
			this.dioramaMinionOrMannequin.TrySpawn();
			this.postponeConfiguration = false;
			this.Configure(this.Config);
		}
		this.PopulateGallery();
		this.SelectOutfit(this.selectedOutfit, true);
		KleiItemsStatusRefresher.AddOrGetListener(this).OnRefreshUI(delegate
		{
			this.RefreshGallery();
			this.outfitDescriptionPanel.Refresh(this.selectedOutfit, ClothingOutfitUtility.OutfitType.Clothing);
		});
		KleiItemsStatusRefresher.RequestRefreshFromServer();
	}

	// Token: 0x060059A0 RID: 22944 RVA: 0x00206768 File Offset: 0x00204968
	public void Configure(OutfitBrowserScreenConfig config)
	{
		this.Config = config;
		if (this.postponeConfiguration)
		{
			return;
		}
		this.dioramaMinionOrMannequin.SetFrom(config.minionPersonality);
		if (config.targetMinionInstance.HasValue)
		{
			this.galleryHeaderLabel.text = UI.OUTFIT_BROWSER_SCREEN.COLUMN_HEADERS.MINION_GALLERY_HEADER.Replace("{MinionName}", config.targetMinionInstance.Value.GetProperName());
		}
		else if (config.minionPersonality.HasValue)
		{
			this.galleryHeaderLabel.text = UI.OUTFIT_BROWSER_SCREEN.COLUMN_HEADERS.MINION_GALLERY_HEADER.Replace("{MinionName}", config.minionPersonality.Value.Name);
		}
		else
		{
			this.galleryHeaderLabel.text = UI.OUTFIT_BROWSER_SCREEN.COLUMN_HEADERS.GALLERY_HEADER;
		}
		Option<ClothingOutfitTarget> option;
		if (config.minionPersonality.HasValue || config.selectedTarget.HasValue)
		{
			option = config.selectedTarget;
		}
		else if (this.lastMannequinSelectedOutfit.HasValue)
		{
			option = this.lastMannequinSelectedOutfit;
		}
		else
		{
			option = ClothingOutfitTarget.GetRandom();
		}
		if (option.HasValue && option.Value.DoesExist())
		{
			this.SelectOutfit(option, true);
		}
		else
		{
			this.SelectOutfit(Option.None, true);
		}
		this.pickOutfitButton.gameObject.SetActive(config.isPickingOutfitForDupe);
		this.renameOutfitButton.gameObject.SetActive(false);
		this.deleteOutfitButton.gameObject.SetActive(false);
		if (base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(false);
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x060059A1 RID: 22945 RVA: 0x002068FA File Offset: 0x00204AFA
	private void RefreshGallery()
	{
		if (this.RefreshGalleryFn != null)
		{
			this.RefreshGalleryFn();
		}
	}

	// Token: 0x060059A2 RID: 22946 RVA: 0x00206910 File Offset: 0x00204B10
	private void PopulateGallery()
	{
		this.outfits.Clear();
		this.galleryGridItemPool.ReturnAll();
		this.RefreshGalleryFn = null;
		if (this.Config.isPickingOutfitForDupe)
		{
			this.<PopulateGallery>g__AddGridIconForTarget|29_0(Option.None);
		}
		OutfitBrowserScreenConfig outfitBrowserScreenConfig = this.Config;
		if (outfitBrowserScreenConfig.targetMinionInstance.HasValue)
		{
			outfitBrowserScreenConfig = this.Config;
			this.<PopulateGallery>g__AddGridIconForTarget|29_0(ClothingOutfitTarget.FromMinion(outfitBrowserScreenConfig.targetMinionInstance.Value));
		}
		foreach (ClothingOutfitTarget clothingOutfitTarget in ClothingOutfitTarget.GetAll())
		{
			this.<PopulateGallery>g__AddGridIconForTarget|29_0(clothingOutfitTarget);
		}
		this.addButtonGridItem.transform.SetAsLastSibling();
		this.addButtonGridItem.SetActive(true);
		this.addButtonGridItem.GetComponent<MultiToggle>().onClick = delegate
		{
			new OutfitDesignerScreenConfig(ClothingOutfitTarget.ForNewOutfit(), this.Config.minionPersonality, this.Config.targetMinionInstance, new Action<ClothingOutfitTarget>(this.OnOutfitDesignerWritesToOutfitTarget)).ApplyAndOpenScreen();
		};
		this.RefreshGallery();
	}

	// Token: 0x060059A3 RID: 22947 RVA: 0x00206A14 File Offset: 0x00204C14
	private void OnOutfitDesignerWritesToOutfitTarget(ClothingOutfitTarget outfit)
	{
		this.Configure(this.Config.WithOutfit(outfit));
	}

	// Token: 0x060059A4 RID: 22948 RVA: 0x00206A3B File Offset: 0x00204C3B
	private void Update()
	{
		this.gridLayouter.CheckIfShouldResizeGrid();
	}

	// Token: 0x060059A5 RID: 22949 RVA: 0x00206A48 File Offset: 0x00204C48
	private void SelectOutfit(string id, bool isFirstOpen = false)
	{
		this.SelectOutfit(ClothingOutfitTarget.FromId(id), isFirstOpen);
	}

	// Token: 0x060059A6 RID: 22950 RVA: 0x00206A5C File Offset: 0x00204C5C
	private void SelectOutfit(Option<ClothingOutfitTarget> outfit, bool isFirstOpen = false)
	{
		this.selectionHeaderLabel.text = outfit.ReadName();
		this.selectedOutfit = outfit;
		this.dioramaMinionOrMannequin.current.SetOutfit(outfit);
		this.dioramaMinionOrMannequin.current.ReactToFullOutfitChange();
		this.outfitDescriptionPanel.Refresh(outfit, ClothingOutfitUtility.OutfitType.Clothing);
		OutfitBrowserScreenConfig outfitBrowserScreenConfig = this.Config;
		if (!outfitBrowserScreenConfig.minionPersonality.HasValue)
		{
			this.lastMannequinSelectedOutfit = outfit;
		}
		outfitBrowserScreenConfig = this.Config;
		if (outfitBrowserScreenConfig.minionPersonality.HasValue)
		{
			this.pickOutfitButton.isInteractable = !outfit.HasValue || !outfit.Value.DoesContainNonOwnedItems();
			GameObject gameObject = this.pickOutfitButton.gameObject;
			Option<string> option;
			if (!this.pickOutfitButton.isInteractable)
			{
				LocString tooltip_PICK_OUTFIT_ERROR_LOCKED = UI.OUTFIT_BROWSER_SCREEN.TOOLTIP_PICK_OUTFIT_ERROR_LOCKED;
				string text = "{MinionName}";
				outfitBrowserScreenConfig = this.Config;
				option = Option.Some<string>(tooltip_PICK_OUTFIT_ERROR_LOCKED.Replace(text, outfitBrowserScreenConfig.GetMinionName()));
			}
			else
			{
				option = Option.None;
			}
			KleiItemsUI.ConfigureTooltipOn(gameObject, option);
		}
		this.editOutfitButton.isInteractable = outfit.HasValue;
		this.renameOutfitButton.gameObject.SetActive(true);
		this.renameOutfitButton.isInteractable = outfit.HasValue && outfit.Value.CanWriteName;
		KleiItemsUI.ConfigureTooltipOn(this.renameOutfitButton.gameObject, this.renameOutfitButton.isInteractable ? UI.OUTFIT_BROWSER_SCREEN.TOOLTIP_RENAME_OUTFIT : UI.OUTFIT_BROWSER_SCREEN.TOOLTIP_RENAME_OUTFIT_ERROR_READONLY);
		this.deleteOutfitButton.gameObject.SetActive(true);
		this.deleteOutfitButton.isInteractable = outfit.HasValue && outfit.Value.CanDelete;
		KleiItemsUI.ConfigureTooltipOn(this.deleteOutfitButton.gameObject, this.deleteOutfitButton.isInteractable ? UI.OUTFIT_BROWSER_SCREEN.TOOLTIP_DELETE_OUTFIT : UI.OUTFIT_BROWSER_SCREEN.TOOLTIP_DELETE_OUTFIT_ERROR_READONLY);
		this.RefreshGallery();
	}

	// Token: 0x060059A7 RID: 22951 RVA: 0x00206C38 File Offset: 0x00204E38
	private void OnClickPickOutfit()
	{
		OutfitBrowserScreenConfig outfitBrowserScreenConfig = this.Config;
		if (outfitBrowserScreenConfig.targetMinionInstance.HasValue)
		{
			outfitBrowserScreenConfig = this.Config;
			outfitBrowserScreenConfig.targetMinionInstance.Value.GetComponent<WearableAccessorizer>().ApplyClothingItems(this.selectedOutfit.ReadItemValues());
		}
		else
		{
			outfitBrowserScreenConfig = this.Config;
			if (outfitBrowserScreenConfig.minionPersonality.HasValue)
			{
				ClothingOutfits clothingOutfits = Db.Get().Permits.ClothingOutfits;
				outfitBrowserScreenConfig = this.Config;
				clothingOutfits.SetDuplicantPersonalityOutfit(outfitBrowserScreenConfig.minionPersonality.Value.Id, this.selectedOutfit.GetId(), ClothingOutfitUtility.OutfitType.Clothing);
				LockerNavigator.Instance.duplicantCatalogueScreen.GetComponent<MinionBrowserScreen>().RefreshPreview();
			}
		}
		LockerNavigator.Instance.PopScreen();
	}

	// Token: 0x060059A8 RID: 22952 RVA: 0x00206CF0 File Offset: 0x00204EF0
	public static void MakeDeletePopup(ClothingOutfitTarget sourceTarget, System.Action deleteFn)
	{
		Action<InfoDialogScreen> <>9__1;
		LockerNavigator.Instance.ShowDialogPopup(delegate(InfoDialogScreen dialog)
		{
			InfoDialogScreen infoDialogScreen = dialog.SetHeader(UI.OUTFIT_BROWSER_SCREEN.DELETE_WARNING_POPUP.HEADER.Replace("{OutfitName}", sourceTarget.ReadName())).AddPlainText(UI.OUTFIT_BROWSER_SCREEN.DELETE_WARNING_POPUP.BODY.Replace("{OutfitName}", sourceTarget.ReadName()));
			string text = UI.OUTFIT_BROWSER_SCREEN.DELETE_WARNING_POPUP.BUTTON_YES_DELETE;
			Action<InfoDialogScreen> action;
			if ((action = <>9__1) == null)
			{
				action = (<>9__1 = delegate(InfoDialogScreen d)
				{
					deleteFn();
					d.Deactivate();
				});
			}
			infoDialogScreen.AddOption(text, action, true).AddOption(UI.OUTFIT_BROWSER_SCREEN.DELETE_WARNING_POPUP.BUTTON_DONT_DELETE, delegate(InfoDialogScreen d)
			{
				d.Deactivate();
			}, false);
		});
	}

	// Token: 0x060059A9 RID: 22953 RVA: 0x00206D28 File Offset: 0x00204F28
	public static void MakeRenamePopup(KInputTextField inputFieldPrefab, ClothingOutfitTarget sourceTarget, Func<string> readName, Action<string> writeName)
	{
		KInputTextField inputField;
		InfoScreenPlainText errorText;
		KButton okButton;
		LocText okButtonText;
		LockerNavigator.Instance.ShowDialogPopup(delegate(InfoDialogScreen dialog)
		{
			dialog.SetHeader(UI.OUTFIT_BROWSER_SCREEN.RENAME_POPUP.HEADER).AddUI<KInputTextField>(inputFieldPrefab, out inputField).AddSpacer(8f)
				.AddUI<InfoScreenPlainText>(dialog.GetPlainTextPrefab(), out errorText)
				.AddOption(true, out okButton, out okButtonText)
				.AddOption(UI.CONFIRMDIALOG.CANCEL, delegate(InfoDialogScreen d)
				{
					d.Deactivate();
				}, false);
			inputField.onValueChanged.AddListener(new UnityAction<string>(base.<MakeRenamePopup>g__Refresh|1));
			errorText.gameObject.SetActive(false);
			LocText component = errorText.gameObject.GetComponent<LocText>();
			component.allowOverride = true;
			component.alignment = TextAlignmentOptions.BottomLeft;
			component.color = Util.ColorFromHex("F44A47");
			component.fontSize = 14f;
			errorText.SetText("");
			okButtonText.text = UI.CONFIRMDIALOG.OK;
			okButton.onClick += delegate
			{
				writeName(inputField.text);
				dialog.Deactivate();
			};
			base.<MakeRenamePopup>g__Refresh|1(readName());
		});
	}

	// Token: 0x060059AA RID: 22954 RVA: 0x00206D70 File Offset: 0x00204F70
	private void SetButtonClickUISound(Option<ClothingOutfitTarget> target, MultiToggle toggle)
	{
		if (!target.HasValue)
		{
			toggle.states[1].on_click_override_sound_path = "HUD_Click";
			toggle.states[0].on_click_override_sound_path = "HUD_Click";
			return;
		}
		bool flag = !target.Value.DoesContainNonOwnedItems();
		toggle.states[1].on_click_override_sound_path = "ClothingItem_Click";
		toggle.states[1].sound_parameter_name = "Unlocked";
		toggle.states[1].sound_parameter_value = (flag ? 1f : 0f);
		toggle.states[1].has_sound_parameter = true;
		toggle.states[0].on_click_override_sound_path = "ClothingItem_Click";
		toggle.states[0].sound_parameter_name = "Unlocked";
		toggle.states[0].sound_parameter_value = (flag ? 1f : 0f);
		toggle.states[0].has_sound_parameter = true;
	}

	// Token: 0x060059AB RID: 22955 RVA: 0x00206E82 File Offset: 0x00205082
	private void OnMouseOverToggle()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Mouseover", false));
	}

	// Token: 0x060059B4 RID: 22964 RVA: 0x00207020 File Offset: 0x00205220
	[CompilerGenerated]
	private void <PopulateGallery>g__AddGridIconForTarget|29_0(Option<ClothingOutfitTarget> target)
	{
		GameObject spawn = this.galleryGridItemPool.Borrow();
		GameObject gameObject = spawn.transform.GetChild(1).gameObject;
		GameObject gameObject2 = spawn.transform.GetChild(2).gameObject;
		GameObject isUnownedOverlayGO = spawn.transform.GetChild(3).gameObject;
		gameObject.SetActive(true);
		gameObject2.SetActive(false);
		gameObject.GetComponentInChildren<UIMannequin>().SetOutfit(target);
		if (!target.HasValue)
		{
			gameObject2.SetActive(true);
			gameObject2.GetComponent<Image>().sprite = KleiItemsUI.GetNoneOutfitIcon();
		}
		MultiToggle button = spawn.GetComponent<MultiToggle>();
		MultiToggle button2 = button;
		button2.onEnter = (System.Action)Delegate.Combine(button2.onEnter, new System.Action(this.OnMouseOverToggle));
		button.onClick = delegate
		{
			this.SelectOutfit(target, false);
		};
		this.RefreshGalleryFn = (System.Action)Delegate.Combine(this.RefreshGalleryFn, new System.Action(delegate
		{
			button.ChangeState((target == this.selectedOutfit) ? 1 : 0);
			if (!target.HasValue)
			{
				KleiItemsUI.ConfigureTooltipOn(spawn, KleiItemsUI.WrapAsToolTipTitle(UI.OUTFIT_NAME.NONE));
				isUnownedOverlayGO.SetActive(false);
				return;
			}
			KleiItemsUI.ConfigureTooltipOn(spawn, KleiItemsUI.WrapAsToolTipTitle(target.Value.ReadName()));
			isUnownedOverlayGO.SetActive(target.Value.DoesContainNonOwnedItems());
		}));
		this.SetButtonClickUISound(target, button);
	}

	// Token: 0x04003C88 RID: 15496
	[Header("ItemGalleryColumn")]
	[SerializeField]
	private LocText galleryHeaderLabel;

	// Token: 0x04003C89 RID: 15497
	[SerializeField]
	private RectTransform galleryGridContent;

	// Token: 0x04003C8A RID: 15498
	[SerializeField]
	private GameObject gridItemPrefab;

	// Token: 0x04003C8B RID: 15499
	[SerializeField]
	private GameObject addButtonGridItem;

	// Token: 0x04003C8C RID: 15500
	private UIPrefabLocalPool galleryGridItemPool;

	// Token: 0x04003C8D RID: 15501
	private GridLayouter gridLayouter;

	// Token: 0x04003C8E RID: 15502
	[Header("SelectionDetailsColumn")]
	[SerializeField]
	private LocText selectionHeaderLabel;

	// Token: 0x04003C8F RID: 15503
	[SerializeField]
	private UIMinionOrMannequin dioramaMinionOrMannequin;

	// Token: 0x04003C90 RID: 15504
	[SerializeField]
	private OutfitDescriptionPanel outfitDescriptionPanel;

	// Token: 0x04003C91 RID: 15505
	[SerializeField]
	private KButton pickOutfitButton;

	// Token: 0x04003C92 RID: 15506
	[SerializeField]
	private KButton editOutfitButton;

	// Token: 0x04003C93 RID: 15507
	[SerializeField]
	private KButton renameOutfitButton;

	// Token: 0x04003C94 RID: 15508
	[SerializeField]
	private KButton deleteOutfitButton;

	// Token: 0x04003C95 RID: 15509
	[SerializeField]
	private KInputTextField inputFieldPrefab;

	// Token: 0x04003C96 RID: 15510
	private Option<ClothingOutfitTarget> lastMannequinSelectedOutfit;

	// Token: 0x04003C97 RID: 15511
	private Option<ClothingOutfitTarget> selectedOutfit;

	// Token: 0x04003C98 RID: 15512
	private Dictionary<string, MultiToggle> outfits = new Dictionary<string, MultiToggle>();

	// Token: 0x04003C9A RID: 15514
	private bool postponeConfiguration = true;

	// Token: 0x04003C9B RID: 15515
	private bool isFirstDisplay = true;

	// Token: 0x04003C9C RID: 15516
	private System.Action RefreshGalleryFn;

	// Token: 0x020019E3 RID: 6627
	private enum MultiToggleState
	{
		// Token: 0x040075A6 RID: 30118
		Default,
		// Token: 0x040075A7 RID: 30119
		Selected,
		// Token: 0x040075A8 RID: 30120
		NonInteractable
	}
}
