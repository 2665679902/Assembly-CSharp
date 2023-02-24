using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Database;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000B4F RID: 2895
public class OutfitDesignerScreen : KMonoBehaviour
{
	// Token: 0x1700065F RID: 1631
	// (get) Token: 0x060059C7 RID: 22983 RVA: 0x002078D5 File Offset: 0x00205AD5
	// (set) Token: 0x060059C8 RID: 22984 RVA: 0x002078DD File Offset: 0x00205ADD
	public OutfitDesignerScreenConfig Config { get; private set; }

	// Token: 0x17000660 RID: 1632
	// (get) Token: 0x060059C9 RID: 22985 RVA: 0x002078E6 File Offset: 0x00205AE6
	// (set) Token: 0x060059CA RID: 22986 RVA: 0x002078EE File Offset: 0x00205AEE
	public PermitResource SelectedPermit { get; private set; }

	// Token: 0x17000661 RID: 1633
	// (get) Token: 0x060059CB RID: 22987 RVA: 0x002078F7 File Offset: 0x00205AF7
	// (set) Token: 0x060059CC RID: 22988 RVA: 0x002078FF File Offset: 0x00205AFF
	public PermitCategory SelectedCategory { get; private set; }

	// Token: 0x17000662 RID: 1634
	// (get) Token: 0x060059CD RID: 22989 RVA: 0x00207908 File Offset: 0x00205B08
	// (set) Token: 0x060059CE RID: 22990 RVA: 0x00207910 File Offset: 0x00205B10
	public OutfitDesignerScreen_OutfitState outfitState { get; private set; }

	// Token: 0x060059CF RID: 22991 RVA: 0x0020791C File Offset: 0x00205B1C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		global::Debug.Assert(this.categoryRowPrefab.transform.parent == this.categoryListContent.transform);
		global::Debug.Assert(this.gridItemPrefab.transform.parent == this.galleryGridContent.transform);
		this.categoryRowPrefab.SetActive(false);
		this.gridItemPrefab.SetActive(false);
		this.galleryGridLayouter = new GridLayouter
		{
			minCellSize = 64f,
			maxCellSize = 96f,
			targetGridLayout = this.galleryGridContent.GetComponent<GridLayoutGroup>()
		};
		this.categoryRowPool = new UIPrefabLocalPool(this.categoryRowPrefab, this.categoryListContent.gameObject);
		this.galleryGridItemPool = new UIPrefabLocalPool(this.gridItemPrefab, this.galleryGridContent.gameObject);
		if (OutfitDesignerScreen.outfitTypeToCategoriesDict == null)
		{
			Dictionary<ClothingOutfitUtility.OutfitType, PermitCategory[]> dictionary = new Dictionary<ClothingOutfitUtility.OutfitType, PermitCategory[]>();
			dictionary[ClothingOutfitUtility.OutfitType.Clothing] = new PermitCategory[]
			{
				PermitCategory.DupeTops,
				PermitCategory.DupeGloves,
				PermitCategory.DupeBottoms,
				PermitCategory.DupeShoes
			};
			OutfitDesignerScreen.outfitTypeToCategoriesDict = dictionary;
		}
	}

	// Token: 0x060059D0 RID: 22992 RVA: 0x00207A26 File Offset: 0x00205C26
	private void Update()
	{
		this.galleryGridLayouter.CheckIfShouldResizeGrid();
	}

	// Token: 0x060059D1 RID: 22993 RVA: 0x00207A34 File Offset: 0x00205C34
	protected override void OnSpawn()
	{
		this.postponeConfiguration = false;
		this.minionOrMannequin.TrySpawn();
		if (this.Config.isValid)
		{
			this.Configure(this.Config);
			return;
		}
		this.Configure(OutfitDesignerScreenConfig.Mannequin(ClothingOutfitTarget.ForNewOutfit()));
	}

	// Token: 0x060059D2 RID: 22994 RVA: 0x00207A83 File Offset: 0x00205C83
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		KleiItemsStatusRefresher.AddOrGetListener(this).OnRefreshUI(delegate
		{
			this.RefreshCategories();
			this.RefreshGallery();
			this.RefreshOutfitState();
		});
		KleiItemsStatusRefresher.RequestRefreshFromServer();
	}

	// Token: 0x060059D3 RID: 22995 RVA: 0x00207AA7 File Offset: 0x00205CA7
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		this.UnregisterPreventScreenPop();
	}

	// Token: 0x060059D4 RID: 22996 RVA: 0x00207AB5 File Offset: 0x00205CB5
	private void UpdateSaveButtons()
	{
		if (this.updateSaveButtonsFn != null)
		{
			this.updateSaveButtonsFn();
		}
	}

	// Token: 0x060059D5 RID: 22997 RVA: 0x00207ACC File Offset: 0x00205CCC
	public void Configure(OutfitDesignerScreenConfig config)
	{
		this.Config = config;
		if (config.targetMinionInstance.HasValue)
		{
			this.outfitState = OutfitDesignerScreen_OutfitState.ForMinionInstance(this.Config.sourceTarget, config.targetMinionInstance.Value);
		}
		else
		{
			this.outfitState = OutfitDesignerScreen_OutfitState.ForTemplateOutfit(this.Config.sourceTarget);
		}
		if (this.postponeConfiguration)
		{
			return;
		}
		this.RegisterPreventScreenPop();
		this.minionOrMannequin.SetFrom(config.minionPersonality).SpawnedAvatar.GetComponent<WearableAccessorizer>();
		using (ListPool<ClothingItemResource, OutfitDesignerScreen>.PooledList pooledList = PoolsFor<OutfitDesignerScreen>.AllocateList<ClothingItemResource>())
		{
			this.outfitState.AddItemValuesTo(pooledList);
			this.minionOrMannequin.SetFrom(config.minionPersonality).SetOutfit(pooledList);
		}
		this.PopulateCategories();
		this.SelectCategory(OutfitDesignerScreen.outfitTypeToCategoriesDict[this.outfitState.outfitType][0]);
		this.galleryGridLayouter.RequestGridResize();
		this.RefreshOutfitState();
		OutfitDesignerScreenConfig outfitDesignerScreenConfig = this.Config;
		if (outfitDesignerScreenConfig.targetMinionInstance.HasValue)
		{
			this.updateSaveButtonsFn = null;
			this.primaryButton.ClearOnClick();
			TMP_Text componentInChildren = this.primaryButton.GetComponentInChildren<LocText>();
			LocString button_APPLY_TO_MINION = UI.OUTFIT_DESIGNER_SCREEN.MINION_INSTANCE.BUTTON_APPLY_TO_MINION;
			string text = "{MinionName}";
			outfitDesignerScreenConfig = this.Config;
			componentInChildren.SetText(button_APPLY_TO_MINION.Replace(text, outfitDesignerScreenConfig.targetMinionInstance.Value.GetProperName()));
			this.primaryButton.onClick += delegate
			{
				ClothingOutfitTarget clothingOutfitTarget = ClothingOutfitTarget.FromMinion(this.Config.targetMinionInstance.Value);
				clothingOutfitTarget.WriteItems(this.outfitState.GetItems());
				if (this.Config.onWriteToOutfitTargetFn != null)
				{
					this.Config.onWriteToOutfitTargetFn(clothingOutfitTarget);
				}
				LockerNavigator.Instance.PopScreen();
			};
			this.secondaryButton.ClearOnClick();
			this.secondaryButton.GetComponentInChildren<LocText>().SetText(UI.OUTFIT_DESIGNER_SCREEN.MINION_INSTANCE.BUTTON_APPLY_TO_TEMPLATE);
			this.secondaryButton.onClick += delegate
			{
				OutfitDesignerScreen.MakeApplyToTemplatePopup(this.inputFieldPrefab, this.outfitState, this.Config.targetMinionInstance.Value, this.Config.outfitTemplate, this.Config.onWriteToOutfitTargetFn);
			};
			this.updateSaveButtonsFn = (System.Action)Delegate.Combine(this.updateSaveButtonsFn, new System.Action(delegate
			{
				if (this.outfitState.DoesContainNonOwnedItems())
				{
					this.primaryButton.isInteractable = false;
					this.primaryButton.gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(UI.OUTFIT_DESIGNER_SCREEN.OUTFIT_TEMPLATE.TOOLTIP_SAVE_ERROR_LOCKED);
					this.secondaryButton.isInteractable = false;
					this.secondaryButton.gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(UI.OUTFIT_DESIGNER_SCREEN.OUTFIT_TEMPLATE.TOOLTIP_SAVE_ERROR_LOCKED);
					return;
				}
				this.primaryButton.isInteractable = true;
				this.primaryButton.gameObject.AddOrGet<ToolTip>().ClearMultiStringTooltip();
				this.secondaryButton.isInteractable = true;
				this.secondaryButton.gameObject.AddOrGet<ToolTip>().ClearMultiStringTooltip();
			}));
		}
		else
		{
			outfitDesignerScreenConfig = this.Config;
			if (!outfitDesignerScreenConfig.outfitTemplate.HasValue)
			{
				throw new NotSupportedException();
			}
			this.updateSaveButtonsFn = null;
			this.primaryButton.ClearOnClick();
			this.primaryButton.GetComponentInChildren<LocText>().SetText(UI.OUTFIT_DESIGNER_SCREEN.OUTFIT_TEMPLATE.BUTTON_SAVE);
			this.primaryButton.onClick += delegate
			{
				this.outfitState.destinationTarget.WriteName(this.outfitState.name);
				this.outfitState.destinationTarget.WriteItems(this.outfitState.GetItems());
				OutfitDesignerScreenConfig outfitDesignerScreenConfig2 = this.Config;
				if (outfitDesignerScreenConfig2.minionPersonality.HasValue)
				{
					ClothingOutfits clothingOutfits = Db.Get().Permits.ClothingOutfits;
					outfitDesignerScreenConfig2 = this.Config;
					clothingOutfits.SetDuplicantPersonalityOutfit(outfitDesignerScreenConfig2.minionPersonality.Value.Id, this.outfitState.destinationTarget.Id, ClothingOutfitUtility.OutfitType.Clothing);
				}
				if (this.Config.onWriteToOutfitTargetFn != null)
				{
					this.Config.onWriteToOutfitTargetFn(this.outfitState.destinationTarget);
				}
				LockerNavigator.Instance.PopScreen();
			};
			this.secondaryButton.ClearOnClick();
			this.secondaryButton.GetComponentInChildren<LocText>().SetText(UI.OUTFIT_DESIGNER_SCREEN.OUTFIT_TEMPLATE.BUTTON_COPY);
			this.secondaryButton.onClick += delegate
			{
				OutfitDesignerScreen.MakeCopyPopup(this, this.inputFieldPrefab, this.outfitState, this.Config.outfitTemplate.Value, this.Config.minionPersonality, this.Config.onWriteToOutfitTargetFn);
			};
			this.updateSaveButtonsFn = (System.Action)Delegate.Combine(this.updateSaveButtonsFn, new System.Action(delegate
			{
				if (!this.outfitState.destinationTarget.CanWriteItems)
				{
					this.primaryButton.isInteractable = false;
					this.primaryButton.gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(UI.OUTFIT_DESIGNER_SCREEN.OUTFIT_TEMPLATE.TOOLTIP_SAVE_ERROR_READONLY);
					this.secondaryButton.isInteractable = true;
					this.secondaryButton.gameObject.AddOrGet<ToolTip>().ClearMultiStringTooltip();
					return;
				}
				if (this.outfitState.DoesContainNonOwnedItems())
				{
					this.primaryButton.isInteractable = false;
					this.primaryButton.gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(UI.OUTFIT_DESIGNER_SCREEN.OUTFIT_TEMPLATE.TOOLTIP_SAVE_ERROR_LOCKED);
					this.secondaryButton.isInteractable = false;
					this.secondaryButton.gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(UI.OUTFIT_DESIGNER_SCREEN.OUTFIT_TEMPLATE.TOOLTIP_SAVE_ERROR_LOCKED);
					return;
				}
				this.primaryButton.isInteractable = true;
				this.primaryButton.gameObject.AddOrGet<ToolTip>().ClearMultiStringTooltip();
				this.secondaryButton.isInteractable = true;
				this.secondaryButton.gameObject.AddOrGet<ToolTip>().ClearMultiStringTooltip();
			}));
		}
		this.UpdateSaveButtons();
	}

	// Token: 0x060059D6 RID: 22998 RVA: 0x00207D70 File Offset: 0x00205F70
	private void RefreshOutfitState()
	{
		this.selectionHeaderLabel.text = this.outfitState.name;
		this.outfitDescriptionPanel.Refresh(this.outfitState, ClothingOutfitUtility.OutfitType.Clothing);
		this.UpdateSaveButtons();
	}

	// Token: 0x060059D7 RID: 22999 RVA: 0x00207DA0 File Offset: 0x00205FA0
	private void RefreshCategories()
	{
		if (this.RefreshCategoriesFn != null)
		{
			this.RefreshCategoriesFn();
		}
	}

	// Token: 0x060059D8 RID: 23000 RVA: 0x00207DB8 File Offset: 0x00205FB8
	public void PopulateCategories()
	{
		this.RefreshCategoriesFn = null;
		this.categoryRowPool.ReturnAll();
		PermitCategory[] array = OutfitDesignerScreen.outfitTypeToCategoriesDict[this.outfitState.outfitType];
		for (int i = 0; i < array.Length; i++)
		{
			OutfitDesignerScreen.<>c__DisplayClass44_0 CS$<>8__locals1 = new OutfitDesignerScreen.<>c__DisplayClass44_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.permitCategory = array[i];
			GameObject gameObject = this.categoryRowPool.Borrow();
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			component.GetReference<LocText>("Label").SetText(PermitCategories.GetUppercaseDisplayName(CS$<>8__locals1.permitCategory));
			component.GetReference<Image>("Icon").sprite = Assets.GetSprite(PermitCategories.GetIconName(CS$<>8__locals1.permitCategory));
			MultiToggle toggle = gameObject.GetComponent<MultiToggle>();
			MultiToggle toggle2 = toggle;
			toggle2.onEnter = (System.Action)Delegate.Combine(toggle2.onEnter, new System.Action(this.OnMouseOverToggle));
			toggle.onClick = delegate
			{
				CS$<>8__locals1.<>4__this.SelectCategory(CS$<>8__locals1.permitCategory);
			};
			this.RefreshCategoriesFn = (System.Action)Delegate.Combine(this.RefreshCategoriesFn, new System.Action(delegate
			{
				toggle.ChangeState((CS$<>8__locals1.permitCategory == CS$<>8__locals1.<>4__this.SelectedCategory) ? 1 : 0);
			}));
			this.SetCatogoryClickUISound(CS$<>8__locals1.permitCategory, toggle);
		}
	}

	// Token: 0x060059D9 RID: 23001 RVA: 0x00207F0C File Offset: 0x0020610C
	public void SelectCategory(PermitCategory permitCategory)
	{
		this.SelectedCategory = permitCategory;
		this.galleryHeaderLabel.text = PermitCategories.GetDisplayName(permitCategory);
		this.RefreshCategories();
		this.PopulateGallery();
		ref Option<ClothingItemResource> itemSlotForCategory = ref this.outfitState.GetItemSlotForCategory(permitCategory);
		if (itemSlotForCategory.HasValue)
		{
			this.SelectPermit(itemSlotForCategory.Value);
			return;
		}
		this.SelectPermit(null);
	}

	// Token: 0x060059DA RID: 23002 RVA: 0x00207F66 File Offset: 0x00206166
	private void RefreshGallery()
	{
		if (this.RefreshGalleryFn != null)
		{
			this.RefreshGalleryFn();
		}
	}

	// Token: 0x060059DB RID: 23003 RVA: 0x00207F7C File Offset: 0x0020617C
	public void PopulateGallery()
	{
		this.RefreshGalleryFn = null;
		this.galleryGridItemPool.ReturnAll();
		this.<PopulateGallery>g__AddGridIconForPermit|48_0(null);
		foreach (PermitResource permitResource in Db.Get().Permits.resources)
		{
			if (permitResource.Category == this.SelectedCategory)
			{
				this.<PopulateGallery>g__AddGridIconForPermit|48_0(permitResource);
			}
		}
		this.RefreshGallery();
	}

	// Token: 0x060059DC RID: 23004 RVA: 0x00208008 File Offset: 0x00206208
	public void SelectPermit(PermitResource permit)
	{
		this.SelectedPermit = permit;
		this.RefreshGallery();
		this.UpdateSelectedItemDetails();
		this.UpdateSaveButtons();
	}

	// Token: 0x060059DD RID: 23005 RVA: 0x00208024 File Offset: 0x00206224
	public unsafe void UpdateSelectedItemDetails()
	{
		Option<ClothingItemResource> option = Option.None;
		if (this.SelectedPermit != null)
		{
			ClothingItemResource clothingItemResource = this.SelectedPermit as ClothingItemResource;
			if (clothingItemResource != null)
			{
				option = clothingItemResource;
			}
		}
		*this.outfitState.GetItemSlotForCategory(this.SelectedCategory) = option;
		this.minionOrMannequin.current.SetOutfit(this.outfitState);
		this.minionOrMannequin.current.ReactToClothingItemChange(this.SelectedCategory);
		this.outfitDescriptionPanel.Refresh(this.outfitState, ClothingOutfitUtility.OutfitType.Clothing);
	}

	// Token: 0x060059DE RID: 23006 RVA: 0x002080AF File Offset: 0x002062AF
	private void RegisterPreventScreenPop()
	{
		this.UnregisterPreventScreenPop();
		this.preventScreenPopFn = delegate
		{
			if (this.outfitState.IsDirty())
			{
				this.RegisterPreventScreenPop();
				OutfitDesignerScreen.MakeSaveWarningPopup(this.outfitState, delegate
				{
					this.UnregisterPreventScreenPop();
					LockerNavigator.Instance.PopScreen();
				});
				return true;
			}
			return false;
		};
		LockerNavigator.Instance.preventScreenPop.Add(this.preventScreenPopFn);
	}

	// Token: 0x060059DF RID: 23007 RVA: 0x002080DE File Offset: 0x002062DE
	private void UnregisterPreventScreenPop()
	{
		if (this.preventScreenPopFn != null)
		{
			LockerNavigator.Instance.preventScreenPop.Remove(this.preventScreenPopFn);
			this.preventScreenPopFn = null;
		}
	}

	// Token: 0x060059E0 RID: 23008 RVA: 0x00208108 File Offset: 0x00206308
	public static void MakeSaveWarningPopup(OutfitDesignerScreen_OutfitState outfitState, System.Action discardChangesFn)
	{
		Action<InfoDialogScreen> <>9__1;
		LockerNavigator.Instance.ShowDialogPopup(delegate(InfoDialogScreen dialog)
		{
			InfoDialogScreen infoDialogScreen = dialog.SetHeader(UI.OUTFIT_DESIGNER_SCREEN.CHANGES_NOT_SAVED_WARNING_POPUP.HEADER.Replace("{OutfitName}", outfitState.name)).AddPlainText(UI.OUTFIT_DESIGNER_SCREEN.CHANGES_NOT_SAVED_WARNING_POPUP.BODY);
			string text = UI.OUTFIT_DESIGNER_SCREEN.CHANGES_NOT_SAVED_WARNING_POPUP.BUTTON_DISCARD;
			Action<InfoDialogScreen> action;
			if ((action = <>9__1) == null)
			{
				action = (<>9__1 = delegate(InfoDialogScreen d)
				{
					d.Deactivate();
					discardChangesFn();
				});
			}
			infoDialogScreen.AddOption(text, action, true).AddOption(UI.OUTFIT_DESIGNER_SCREEN.CHANGES_NOT_SAVED_WARNING_POPUP.BUTTON_RETURN, delegate(InfoDialogScreen d)
			{
				d.Deactivate();
			}, false);
		});
	}

	// Token: 0x060059E1 RID: 23009 RVA: 0x00208140 File Offset: 0x00206340
	public static void MakeApplyToTemplatePopup(KInputTextField inputFieldPrefab, OutfitDesignerScreen_OutfitState outfitState, GameObject targetMinionInstance, Option<ClothingOutfitTarget> existingOutfitTemplate, Action<ClothingOutfitTarget> onWriteToOutfitTargetFn)
	{
		ClothingOutfitNameProposal proposal = default(ClothingOutfitNameProposal);
		Color errorTextColor = Util.ColorFromHex("F44A47");
		Color defaultTextColor = Util.ColorFromHex("FFFFFF");
		KInputTextField inputField;
		InfoScreenPlainText descLabel;
		KButton saveButton;
		LocText saveButtonText;
		LocText descLocText;
		LockerNavigator.Instance.ShowDialogPopup(delegate(InfoDialogScreen dialog)
		{
			dialog.SetHeader(UI.OUTFIT_DESIGNER_SCREEN.MINION_INSTANCE.APPLY_TEMPLATE_POPUP.HEADER.Replace("{OutfitName}", outfitState.name)).AddUI<KInputTextField>(inputFieldPrefab, out inputField).AddSpacer(8f)
				.AddUI<InfoScreenPlainText>(dialog.GetPlainTextPrefab(), out descLabel)
				.AddOption(true, out saveButton, out saveButtonText)
				.AddDefaultCancel();
			descLocText = descLabel.gameObject.GetComponent<LocText>();
			descLocText.allowOverride = true;
			descLocText.alignment = TextAlignmentOptions.BottomLeft;
			descLocText.color = errorTextColor;
			descLocText.fontSize = 14f;
			descLabel.SetText("");
			inputField.onValueChanged.AddListener(new UnityAction<string>(base.<MakeApplyToTemplatePopup>g__Refresh|1));
			saveButton.onClick += delegate
			{
				ClothingOutfitNameProposal.Result result = proposal.result;
				ClothingOutfitTarget clothingOutfitTarget;
				if (result != ClothingOutfitNameProposal.Result.NewOutfit)
				{
					if (result != ClothingOutfitNameProposal.Result.SameOutfit)
					{
						throw new NotSupportedException(string.Format("Can't save outfit with name \"{0}\", failed with result: {1}", proposal.candidateName, proposal.result));
					}
					clothingOutfitTarget = existingOutfitTemplate.Value;
				}
				else
				{
					clothingOutfitTarget = ClothingOutfitTarget.ForNewOutfit(proposal.candidateName);
				}
				clothingOutfitTarget.WriteItems(outfitState.GetItems());
				ClothingOutfitTarget.FromMinion(targetMinionInstance).WriteItems(outfitState.GetItems());
				if (onWriteToOutfitTargetFn != null)
				{
					onWriteToOutfitTargetFn(clothingOutfitTarget);
				}
				dialog.Deactivate();
				LockerNavigator.Instance.PopScreen();
			};
			if (!existingOutfitTemplate.HasValue)
			{
				base.<MakeApplyToTemplatePopup>g__Refresh|1(outfitState.name);
				return;
			}
			if (existingOutfitTemplate.Value.CanWriteName && existingOutfitTemplate.Value.CanWriteItems)
			{
				base.<MakeApplyToTemplatePopup>g__Refresh|1(existingOutfitTemplate.Value.Id);
				return;
			}
			base.<MakeApplyToTemplatePopup>g__Refresh|1(ClothingOutfitTarget.ForCopyOf(existingOutfitTemplate.Value).Id);
		});
	}

	// Token: 0x060059E2 RID: 23010 RVA: 0x002081BC File Offset: 0x002063BC
	public static void MakeCopyPopup(OutfitDesignerScreen screen, KInputTextField inputFieldPrefab, OutfitDesignerScreen_OutfitState outfitState, ClothingOutfitTarget outfitTemplate, Option<Personality> minionPersonality, Action<ClothingOutfitTarget> onWriteToOutfitTargetFn)
	{
		ClothingOutfitNameProposal proposal = default(ClothingOutfitNameProposal);
		KInputTextField inputField;
		InfoScreenPlainText errorText;
		KButton okButton;
		LocText okButtonText;
		LockerNavigator.Instance.ShowDialogPopup(delegate(InfoDialogScreen dialog)
		{
			dialog.SetHeader(UI.OUTFIT_DESIGNER_SCREEN.COPY_POPUP.HEADER).AddUI<KInputTextField>(inputFieldPrefab, out inputField).AddSpacer(8f)
				.AddUI<InfoScreenPlainText>(dialog.GetPlainTextPrefab(), out errorText)
				.AddOption(true, out okButton, out okButtonText)
				.AddOption(UI.CONFIRMDIALOG.CANCEL, delegate(InfoDialogScreen d)
				{
					d.Deactivate();
				}, false);
			inputField.onValueChanged.AddListener(new UnityAction<string>(base.<MakeCopyPopup>g__Refresh|1));
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
				if (proposal.result == ClothingOutfitNameProposal.Result.NewOutfit)
				{
					ClothingOutfitTarget clothingOutfitTarget = ClothingOutfitTarget.ForNewOutfit(proposal.candidateName);
					clothingOutfitTarget.WriteItems(outfitState.GetItems());
					if (minionPersonality.HasValue)
					{
						Db.Get().Permits.ClothingOutfits.SetDuplicantPersonalityOutfit(minionPersonality.Value.Id, clothingOutfitTarget.Id, ClothingOutfitUtility.OutfitType.Clothing);
					}
					if (onWriteToOutfitTargetFn != null)
					{
						onWriteToOutfitTargetFn(clothingOutfitTarget);
					}
					dialog.Deactivate();
					screen.Configure(screen.Config.WithOutfit(clothingOutfitTarget));
					return;
				}
				throw new NotSupportedException(string.Format("Can't save outfit with name \"{0}\", failed with result: {1}", proposal.candidateName, proposal.result));
			};
			base.<MakeCopyPopup>g__Refresh|1(ClothingOutfitTarget.ForCopyOf(outfitTemplate).Id);
		});
	}

	// Token: 0x060059E3 RID: 23011 RVA: 0x00208220 File Offset: 0x00206420
	private void SetCatogoryClickUISound(PermitCategory category, MultiToggle toggle)
	{
		toggle.states[1].on_click_override_sound_path = category.ToString() + "_Click";
		toggle.states[0].on_click_override_sound_path = category.ToString() + "_Click";
	}

	// Token: 0x060059E4 RID: 23012 RVA: 0x00208280 File Offset: 0x00206480
	private void SetItemClickUISound(PermitResource permit, MultiToggle toggle)
	{
		if (permit == null)
		{
			toggle.states[1].on_click_override_sound_path = "HUD_Click";
			toggle.states[0].on_click_override_sound_path = "HUD_Click";
			return;
		}
		string clothingItemSoundName = OutfitDesignerScreen.GetClothingItemSoundName(permit);
		toggle.states[1].on_click_override_sound_path = clothingItemSoundName + "_Click";
		toggle.states[1].sound_parameter_name = "Unlocked";
		toggle.states[1].sound_parameter_value = (permit.IsUnlocked() ? 1f : 0f);
		toggle.states[1].has_sound_parameter = true;
		toggle.states[0].on_click_override_sound_path = clothingItemSoundName + "_Click";
		toggle.states[0].sound_parameter_name = "Unlocked";
		toggle.states[0].sound_parameter_value = (permit.IsUnlocked() ? 1f : 0f);
		toggle.states[0].has_sound_parameter = true;
	}

	// Token: 0x060059E5 RID: 23013 RVA: 0x00208398 File Offset: 0x00206598
	public static string GetClothingItemSoundName(PermitResource permit)
	{
		if (permit == null)
		{
			return "HUD";
		}
		switch (permit.Category)
		{
		case PermitCategory.DupeTops:
			return "tops";
		case PermitCategory.DupeBottoms:
			return "bottoms";
		case PermitCategory.DupeGloves:
			return "gloves";
		case PermitCategory.DupeShoes:
			return "shoes";
		case PermitCategory.DupeHats:
			return "hats";
		default:
			return "HUD";
		}
	}

	// Token: 0x060059E6 RID: 23014 RVA: 0x002083F9 File Offset: 0x002065F9
	private void OnMouseOverToggle()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Mouseover", false));
	}

	// Token: 0x060059EF RID: 23023 RVA: 0x002087BC File Offset: 0x002069BC
	[CompilerGenerated]
	private void <PopulateGallery>g__AddGridIconForPermit|48_0(PermitResource permit)
	{
		GameObject gameObject = this.galleryGridItemPool.Borrow();
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		Image reference = component.GetReference<Image>("Icon");
		MultiToggle toggle = gameObject.GetComponent<MultiToggle>();
		Image isUnownedOverlay = component.GetReference<Image>("IsUnownedOverlay");
		if (permit == null)
		{
			reference.sprite = KleiItemsUI.GetNoneClothingItemIcon(this.SelectedCategory);
			KleiItemsUI.ConfigureTooltipOn(gameObject, KleiItemsUI.GetNoneClothingItemString(this.SelectedCategory));
			isUnownedOverlay.gameObject.SetActive(false);
		}
		else
		{
			PermitPresentationInfo permitPresentationInfo = permit.GetPermitPresentationInfo();
			reference.sprite = permitPresentationInfo.sprite;
			KleiItemsUI.ConfigureTooltipOn(gameObject, KleiItemsUI.GetTooltipStringFor(permit));
			this.RefreshGalleryFn = (System.Action)Delegate.Combine(this.RefreshGalleryFn, new System.Action(delegate
			{
				isUnownedOverlay.gameObject.SetActive(!permit.IsUnlocked());
			}));
		}
		MultiToggle toggle2 = toggle;
		toggle2.onEnter = (System.Action)Delegate.Combine(toggle2.onEnter, new System.Action(this.OnMouseOverToggle));
		toggle.onClick = delegate
		{
			this.SelectPermit(permit);
		};
		this.RefreshGalleryFn = (System.Action)Delegate.Combine(this.RefreshGalleryFn, new System.Action(delegate
		{
			toggle.ChangeState((permit == this.SelectedPermit) ? 1 : 0);
		}));
		this.SetItemClickUISound(permit, toggle);
	}

	// Token: 0x04003CAA RID: 15530
	[Header("CategoryColumn")]
	[SerializeField]
	private RectTransform categoryListContent;

	// Token: 0x04003CAB RID: 15531
	[SerializeField]
	private GameObject categoryRowPrefab;

	// Token: 0x04003CAC RID: 15532
	private UIPrefabLocalPool categoryRowPool;

	// Token: 0x04003CAD RID: 15533
	[Header("ItemGalleryColumn")]
	[SerializeField]
	private LocText galleryHeaderLabel;

	// Token: 0x04003CAE RID: 15534
	[SerializeField]
	private RectTransform galleryGridContent;

	// Token: 0x04003CAF RID: 15535
	[SerializeField]
	private GameObject gridItemPrefab;

	// Token: 0x04003CB0 RID: 15536
	private UIPrefabLocalPool galleryGridItemPool;

	// Token: 0x04003CB1 RID: 15537
	private GridLayouter galleryGridLayouter;

	// Token: 0x04003CB2 RID: 15538
	[Header("SelectionDetailsColumn")]
	[SerializeField]
	private LocText selectionHeaderLabel;

	// Token: 0x04003CB3 RID: 15539
	[SerializeField]
	private UIMinionOrMannequin minionOrMannequin;

	// Token: 0x04003CB4 RID: 15540
	[SerializeField]
	private KButton primaryButton;

	// Token: 0x04003CB5 RID: 15541
	[SerializeField]
	private KButton secondaryButton;

	// Token: 0x04003CB6 RID: 15542
	[SerializeField]
	private OutfitDescriptionPanel outfitDescriptionPanel;

	// Token: 0x04003CB7 RID: 15543
	[SerializeField]
	private KInputTextField inputFieldPrefab;

	// Token: 0x04003CBC RID: 15548
	public static Dictionary<ClothingOutfitUtility.OutfitType, PermitCategory[]> outfitTypeToCategoriesDict;

	// Token: 0x04003CBD RID: 15549
	private bool postponeConfiguration = true;

	// Token: 0x04003CBE RID: 15550
	private System.Action updateSaveButtonsFn;

	// Token: 0x04003CBF RID: 15551
	private System.Action RefreshCategoriesFn;

	// Token: 0x04003CC0 RID: 15552
	private System.Action RefreshGalleryFn;

	// Token: 0x04003CC1 RID: 15553
	private Func<bool> preventScreenPopFn;

	// Token: 0x020019EA RID: 6634
	private enum MultiToggleState
	{
		// Token: 0x040075C3 RID: 30147
		Default,
		// Token: 0x040075C4 RID: 30148
		Selected,
		// Token: 0x040075C5 RID: 30149
		NonInteractable
	}
}
