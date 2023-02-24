using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Database;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AC3 RID: 2755
public class JoyResponseDesignerScreen : KMonoBehaviour
{
	// Token: 0x17000639 RID: 1593
	// (get) Token: 0x06005442 RID: 21570 RVA: 0x001E956F File Offset: 0x001E776F
	// (set) Token: 0x06005443 RID: 21571 RVA: 0x001E9577 File Offset: 0x001E7777
	public JoyResponseScreenConfig Config { get; private set; }

	// Token: 0x06005444 RID: 21572 RVA: 0x001E9580 File Offset: 0x001E7780
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		global::Debug.Assert(this.categoryRowPrefab.transform.parent == this.categoryListContent.transform);
		global::Debug.Assert(this.galleryItemPrefab.transform.parent == this.galleryGridContent.transform);
		this.categoryRowPrefab.SetActive(false);
		this.galleryItemPrefab.SetActive(false);
		this.galleryGridLayouter = new GridLayouter
		{
			minCellSize = 64f,
			maxCellSize = 96f,
			targetGridLayout = this.galleryGridContent.GetComponent<GridLayoutGroup>()
		};
		this.categoryRowPool = new UIPrefabLocalPool(this.categoryRowPrefab, this.categoryListContent.gameObject);
		this.galleryGridItemPool = new UIPrefabLocalPool(this.galleryItemPrefab, this.galleryGridContent.gameObject);
		JoyResponseDesignerScreen.JoyResponseCategory[] array = new JoyResponseDesignerScreen.JoyResponseCategory[1];
		int num = 0;
		JoyResponseDesignerScreen.JoyResponseCategory joyResponseCategory = new JoyResponseDesignerScreen.JoyResponseCategory();
		joyResponseCategory.displayName = UI.KLEI_INVENTORY_SCREEN.CATEGORIES.JOY_RESPONSES.BALLOON_ARTIST;
		joyResponseCategory.icon = Assets.GetSprite("icon_inventory_balloonartist");
		JoyResponseDesignerScreen.GalleryItem[] array2 = Db.Get().Permits.BalloonArtistFacades.resources.Select((BalloonArtistFacadeResource r) => JoyResponseDesignerScreen.GalleryItem.Of(r)).Prepend(JoyResponseDesignerScreen.GalleryItem.Of(Option.None)).ToArray<JoyResponseDesignerScreen.GalleryItem.BalloonArtistFacadeTarget>();
		joyResponseCategory.items = array2;
		array[num] = joyResponseCategory;
		this.joyResponseCategories = array;
		this.dioramaVis.ConfigureSetup();
	}

	// Token: 0x06005445 RID: 21573 RVA: 0x001E96FC File Offset: 0x001E78FC
	private void Update()
	{
		this.galleryGridLayouter.CheckIfShouldResizeGrid();
	}

	// Token: 0x06005446 RID: 21574 RVA: 0x001E9709 File Offset: 0x001E7909
	protected override void OnSpawn()
	{
		this.postponeConfiguration = false;
		if (this.Config.isValid)
		{
			this.Configure(this.Config);
			return;
		}
		throw new InvalidOperationException("Cannot open up JoyResponseDesignerScreen without a target personality or minion instance");
	}

	// Token: 0x06005447 RID: 21575 RVA: 0x001E9736 File Offset: 0x001E7936
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		KleiItemsStatusRefresher.AddOrGetListener(this).OnRefreshUI(delegate
		{
			this.Configure(this.Config);
		});
		KleiItemsStatusRefresher.RequestRefreshFromServer();
	}

	// Token: 0x06005448 RID: 21576 RVA: 0x001E975C File Offset: 0x001E795C
	public void Configure(JoyResponseScreenConfig config)
	{
		this.Config = config;
		if (this.postponeConfiguration)
		{
			return;
		}
		this.RegisterPreventScreenPop();
		this.primaryButton.ClearOnClick();
		TMP_Text componentInChildren = this.primaryButton.GetComponentInChildren<LocText>();
		LocString button_APPLY_TO_MINION = UI.JOY_RESPONSE_DESIGNER_SCREEN.BUTTON_APPLY_TO_MINION;
		string text = "{MinionName}";
		JoyResponseScreenConfig joyResponseScreenConfig = this.Config;
		componentInChildren.SetText(button_APPLY_TO_MINION.Replace(text, joyResponseScreenConfig.target.GetMinionName()));
		this.primaryButton.onClick += delegate
		{
			Option<PermitResource> permitResource = this.selectedGalleryItem.GetPermitResource();
			if (permitResource.IsSome())
			{
				string text2 = "Save selected balloon ";
				string name = this.selectedGalleryItem.GetName();
				string text3 = " for ";
				JoyResponseScreenConfig joyResponseScreenConfig2 = this.Config;
				global::Debug.Log(text2 + name + text3 + joyResponseScreenConfig2.target.GetMinionName());
				if (this.CanSaveSelection())
				{
					joyResponseScreenConfig2 = this.Config;
					joyResponseScreenConfig2.target.WriteFacadeId(permitResource.Unwrap().Id);
				}
			}
			else
			{
				string text4 = "Save selected balloon ";
				string name2 = this.selectedGalleryItem.GetName();
				string text5 = " for ";
				JoyResponseScreenConfig joyResponseScreenConfig2 = this.Config;
				global::Debug.Log(text4 + name2 + text5 + joyResponseScreenConfig2.target.GetMinionName());
				joyResponseScreenConfig2 = this.Config;
				joyResponseScreenConfig2.target.WriteFacadeId(Option.None);
			}
			LockerNavigator.Instance.PopScreen();
		};
		this.PopulateCategories();
		this.PopulateGallery();
		this.PopulatePreview();
		joyResponseScreenConfig = this.Config;
		if (joyResponseScreenConfig.initalSelectedItem.IsSome())
		{
			joyResponseScreenConfig = this.Config;
			this.SelectGalleryItem(joyResponseScreenConfig.initalSelectedItem.Unwrap());
		}
	}

	// Token: 0x06005449 RID: 21577 RVA: 0x001E9814 File Offset: 0x001E7A14
	private bool CanSaveSelection()
	{
		return this.GetSaveSelectionError().IsNone();
	}

	// Token: 0x0600544A RID: 21578 RVA: 0x001E9830 File Offset: 0x001E7A30
	private Option<string> GetSaveSelectionError()
	{
		if (!this.selectedGalleryItem.IsUnlocked())
		{
			return Option.Some<string>(UI.JOY_RESPONSE_DESIGNER_SCREEN.TOOLTIP_PICK_JOY_RESPONSE_ERROR_LOCKED.Replace("{MinionName}", this.Config.target.GetMinionName()));
		}
		return Option.None;
	}

	// Token: 0x0600544B RID: 21579 RVA: 0x001E987C File Offset: 0x001E7A7C
	private void RefreshCategories()
	{
		if (this.RefreshCategoriesFn != null)
		{
			this.RefreshCategoriesFn();
		}
	}

	// Token: 0x0600544C RID: 21580 RVA: 0x001E9894 File Offset: 0x001E7A94
	public void PopulateCategories()
	{
		this.RefreshCategoriesFn = null;
		this.categoryRowPool.ReturnAll();
		JoyResponseDesignerScreen.JoyResponseCategory[] array = this.joyResponseCategories;
		for (int i = 0; i < array.Length; i++)
		{
			JoyResponseDesignerScreen.<>c__DisplayClass28_0 CS$<>8__locals1 = new JoyResponseDesignerScreen.<>c__DisplayClass28_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.category = array[i];
			GameObject gameObject = this.categoryRowPool.Borrow();
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			component.GetReference<LocText>("Label").SetText(CS$<>8__locals1.category.displayName);
			component.GetReference<Image>("Icon").sprite = CS$<>8__locals1.category.icon;
			MultiToggle toggle = gameObject.GetComponent<MultiToggle>();
			MultiToggle toggle2 = toggle;
			toggle2.onEnter = (System.Action)Delegate.Combine(toggle2.onEnter, new System.Action(this.OnMouseOverToggle));
			toggle.onClick = delegate
			{
				CS$<>8__locals1.<>4__this.SelectCategory(CS$<>8__locals1.category);
			};
			this.RefreshCategoriesFn = (System.Action)Delegate.Combine(this.RefreshCategoriesFn, new System.Action(delegate
			{
				toggle.ChangeState((CS$<>8__locals1.category == CS$<>8__locals1.<>4__this.selectedCategoryOpt) ? 1 : 0);
			}));
			this.SetCatogoryClickUISound(CS$<>8__locals1.category, toggle);
		}
		this.SelectCategory(this.joyResponseCategories[0]);
	}

	// Token: 0x0600544D RID: 21581 RVA: 0x001E99DB File Offset: 0x001E7BDB
	public void SelectCategory(JoyResponseDesignerScreen.JoyResponseCategory category)
	{
		this.selectedCategoryOpt = category;
		this.galleryHeaderLabel.text = category.displayName;
		this.RefreshCategories();
		this.PopulateGallery();
		this.RefreshPreview();
	}

	// Token: 0x0600544E RID: 21582 RVA: 0x001E9A0C File Offset: 0x001E7C0C
	private void SetCatogoryClickUISound(JoyResponseDesignerScreen.JoyResponseCategory category, MultiToggle toggle)
	{
	}

	// Token: 0x0600544F RID: 21583 RVA: 0x001E9A0E File Offset: 0x001E7C0E
	private void RefreshGallery()
	{
		if (this.RefreshGalleryFn != null)
		{
			this.RefreshGalleryFn();
		}
	}

	// Token: 0x06005450 RID: 21584 RVA: 0x001E9A24 File Offset: 0x001E7C24
	public void PopulateGallery()
	{
		this.RefreshGalleryFn = null;
		this.galleryGridItemPool.ReturnAll();
		if (this.selectedCategoryOpt.IsNone())
		{
			return;
		}
		JoyResponseDesignerScreen.JoyResponseCategory joyResponseCategory = this.selectedCategoryOpt.Unwrap();
		foreach (JoyResponseDesignerScreen.GalleryItem galleryItem in joyResponseCategory.items)
		{
			this.<PopulateGallery>g__AddGridIcon|36_0(galleryItem);
		}
		this.SelectGalleryItem(joyResponseCategory.items[0]);
	}

	// Token: 0x06005451 RID: 21585 RVA: 0x001E9A8B File Offset: 0x001E7C8B
	public void SelectGalleryItem(JoyResponseDesignerScreen.GalleryItem item)
	{
		this.selectedGalleryItem = item;
		this.RefreshGallery();
		this.RefreshPreview();
	}

	// Token: 0x06005452 RID: 21586 RVA: 0x001E9AA0 File Offset: 0x001E7CA0
	private void OnMouseOverToggle()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Mouseover", false));
	}

	// Token: 0x06005453 RID: 21587 RVA: 0x001E9AB2 File Offset: 0x001E7CB2
	public void RefreshPreview()
	{
		if (this.RefreshPreviewFn != null)
		{
			this.RefreshPreviewFn();
		}
	}

	// Token: 0x06005454 RID: 21588 RVA: 0x001E9AC7 File Offset: 0x001E7CC7
	public void PopulatePreview()
	{
		this.RefreshPreviewFn = (System.Action)Delegate.Combine(this.RefreshPreviewFn, new System.Action(delegate
		{
			JoyResponseDesignerScreen.GalleryItem.BalloonArtistFacadeTarget balloonArtistFacadeTarget = this.selectedGalleryItem as JoyResponseDesignerScreen.GalleryItem.BalloonArtistFacadeTarget;
			if (balloonArtistFacadeTarget == null)
			{
				throw new NotImplementedException();
			}
			Option<PermitResource> permitResource = balloonArtistFacadeTarget.GetPermitResource();
			this.selectionHeaderLabel.SetText(balloonArtistFacadeTarget.GetName());
			this.dioramaVis.SetMinion(this.Config.target.GetPersonality());
			this.dioramaVis.ConfigureWith(balloonArtistFacadeTarget.permit);
			this.outfitDescriptionPanel.Refresh(permitResource.UnwrapOr(null, null), ClothingOutfitUtility.OutfitType.JoyResponse);
			Option<string> saveSelectionError = this.GetSaveSelectionError();
			if (saveSelectionError.IsSome())
			{
				this.primaryButton.isInteractable = false;
				this.primaryButton.gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(saveSelectionError.Unwrap());
				return;
			}
			this.primaryButton.isInteractable = true;
			this.primaryButton.gameObject.AddOrGet<ToolTip>().ClearMultiStringTooltip();
		}));
		this.RefreshPreview();
	}

	// Token: 0x06005455 RID: 21589 RVA: 0x001E9AF1 File Offset: 0x001E7CF1
	private void RegisterPreventScreenPop()
	{
		this.UnregisterPreventScreenPop();
		this.preventScreenPopFn = delegate
		{
			if (this.Config.target.ReadFacadeId() != this.selectedGalleryItem.GetPermitResource().AndThen<string>((PermitResource r) => r.Id))
			{
				this.RegisterPreventScreenPop();
				JoyResponseDesignerScreen.MakeSaveWarningPopup(this.Config.target, delegate
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

	// Token: 0x06005456 RID: 21590 RVA: 0x001E9B20 File Offset: 0x001E7D20
	private void UnregisterPreventScreenPop()
	{
		if (this.preventScreenPopFn != null)
		{
			LockerNavigator.Instance.preventScreenPop.Remove(this.preventScreenPopFn);
			this.preventScreenPopFn = null;
		}
	}

	// Token: 0x06005457 RID: 21591 RVA: 0x001E9B48 File Offset: 0x001E7D48
	public static void MakeSaveWarningPopup(JoyResponseOutfitTarget target, System.Action discardChangesFn)
	{
		Action<InfoDialogScreen> <>9__1;
		LockerNavigator.Instance.ShowDialogPopup(delegate(InfoDialogScreen dialog)
		{
			InfoDialogScreen infoDialogScreen = dialog.SetHeader(UI.JOY_RESPONSE_DESIGNER_SCREEN.CHANGES_NOT_SAVED_WARNING_POPUP.HEADER.Replace("{MinionName}", target.GetMinionName())).AddPlainText(UI.OUTFIT_DESIGNER_SCREEN.CHANGES_NOT_SAVED_WARNING_POPUP.BODY);
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

	// Token: 0x0600545B RID: 21595 RVA: 0x001E9C78 File Offset: 0x001E7E78
	[CompilerGenerated]
	private void <PopulateGallery>g__AddGridIcon|36_0(JoyResponseDesignerScreen.GalleryItem item)
	{
		GameObject gameObject = this.galleryGridItemPool.Borrow();
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		component.GetReference<Image>("Icon").sprite = item.GetIcon();
		component.GetReference<Image>("IsUnownedOverlay").gameObject.SetActive(!item.IsUnlocked());
		gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(item.GetName());
		MultiToggle toggle = gameObject.GetComponent<MultiToggle>();
		MultiToggle toggle3 = toggle;
		toggle3.onEnter = (System.Action)Delegate.Combine(toggle3.onEnter, new System.Action(this.OnMouseOverToggle));
		MultiToggle toggle2 = toggle;
		toggle2.onClick = (System.Action)Delegate.Combine(toggle2.onClick, new System.Action(delegate
		{
			this.SelectGalleryItem(item);
		}));
		this.RefreshGalleryFn = (System.Action)Delegate.Combine(this.RefreshGalleryFn, new System.Action(delegate
		{
			toggle.ChangeState((item == this.selectedGalleryItem) ? 1 : 0);
		}));
	}

	// Token: 0x04003946 RID: 14662
	[Header("CategoryColumn")]
	[SerializeField]
	private RectTransform categoryListContent;

	// Token: 0x04003947 RID: 14663
	[SerializeField]
	private GameObject categoryRowPrefab;

	// Token: 0x04003948 RID: 14664
	[Header("GalleryColumn")]
	[SerializeField]
	private LocText galleryHeaderLabel;

	// Token: 0x04003949 RID: 14665
	[SerializeField]
	private RectTransform galleryGridContent;

	// Token: 0x0400394A RID: 14666
	[SerializeField]
	private GameObject galleryItemPrefab;

	// Token: 0x0400394B RID: 14667
	[Header("SelectionDetailsColumn")]
	[SerializeField]
	private LocText selectionHeaderLabel;

	// Token: 0x0400394C RID: 14668
	[SerializeField]
	private KleiPermitDioramaVis_JoyResponseBalloon dioramaVis;

	// Token: 0x0400394D RID: 14669
	[SerializeField]
	private OutfitDescriptionPanel outfitDescriptionPanel;

	// Token: 0x0400394E RID: 14670
	[SerializeField]
	private KButton primaryButton;

	// Token: 0x04003950 RID: 14672
	public JoyResponseDesignerScreen.JoyResponseCategory[] joyResponseCategories;

	// Token: 0x04003951 RID: 14673
	private bool postponeConfiguration = true;

	// Token: 0x04003952 RID: 14674
	private Option<JoyResponseDesignerScreen.JoyResponseCategory> selectedCategoryOpt;

	// Token: 0x04003953 RID: 14675
	private UIPrefabLocalPool categoryRowPool;

	// Token: 0x04003954 RID: 14676
	private System.Action RefreshCategoriesFn;

	// Token: 0x04003955 RID: 14677
	private JoyResponseDesignerScreen.GalleryItem selectedGalleryItem;

	// Token: 0x04003956 RID: 14678
	private UIPrefabLocalPool galleryGridItemPool;

	// Token: 0x04003957 RID: 14679
	private GridLayouter galleryGridLayouter;

	// Token: 0x04003958 RID: 14680
	private System.Action RefreshGalleryFn;

	// Token: 0x04003959 RID: 14681
	public System.Action RefreshPreviewFn;

	// Token: 0x0400395A RID: 14682
	private Func<bool> preventScreenPopFn;

	// Token: 0x02001936 RID: 6454
	public class JoyResponseCategory
	{
		// Token: 0x040073A8 RID: 29608
		public string displayName;

		// Token: 0x040073A9 RID: 29609
		public Sprite icon;

		// Token: 0x040073AA RID: 29610
		public JoyResponseDesignerScreen.GalleryItem[] items;
	}

	// Token: 0x02001937 RID: 6455
	private enum MultiToggleState
	{
		// Token: 0x040073AC RID: 29612
		Default,
		// Token: 0x040073AD RID: 29613
		Selected
	}

	// Token: 0x02001938 RID: 6456
	public abstract class GalleryItem : IEquatable<JoyResponseDesignerScreen.GalleryItem>
	{
		// Token: 0x06008F8D RID: 36749
		public abstract string GetName();

		// Token: 0x06008F8E RID: 36750
		public abstract Sprite GetIcon();

		// Token: 0x06008F8F RID: 36751
		public abstract string GetUniqueId();

		// Token: 0x06008F90 RID: 36752
		public abstract bool IsUnlocked();

		// Token: 0x06008F91 RID: 36753
		public abstract Option<PermitResource> GetPermitResource();

		// Token: 0x06008F92 RID: 36754 RVA: 0x0031084C File Offset: 0x0030EA4C
		public override bool Equals(object obj)
		{
			JoyResponseDesignerScreen.GalleryItem galleryItem = obj as JoyResponseDesignerScreen.GalleryItem;
			return galleryItem != null && this.Equals(galleryItem);
		}

		// Token: 0x06008F93 RID: 36755 RVA: 0x0031086C File Offset: 0x0030EA6C
		public bool Equals(JoyResponseDesignerScreen.GalleryItem other)
		{
			return this.GetHashCode() == other.GetHashCode();
		}

		// Token: 0x06008F94 RID: 36756 RVA: 0x0031087C File Offset: 0x0030EA7C
		public override int GetHashCode()
		{
			return Hash.SDBMLower(this.GetUniqueId());
		}

		// Token: 0x06008F95 RID: 36757 RVA: 0x00310889 File Offset: 0x0030EA89
		public override string ToString()
		{
			return this.GetUniqueId();
		}

		// Token: 0x06008F96 RID: 36758 RVA: 0x00310891 File Offset: 0x0030EA91
		public static JoyResponseDesignerScreen.GalleryItem.BalloonArtistFacadeTarget Of(Option<BalloonArtistFacadeResource> permit)
		{
			return new JoyResponseDesignerScreen.GalleryItem.BalloonArtistFacadeTarget
			{
				permit = permit
			};
		}

		// Token: 0x02002100 RID: 8448
		public class BalloonArtistFacadeTarget : JoyResponseDesignerScreen.GalleryItem
		{
			// Token: 0x0600A5B8 RID: 42424 RVA: 0x0034ADEC File Offset: 0x00348FEC
			public override Sprite GetIcon()
			{
				return this.permit.AndThen<Sprite>((BalloonArtistFacadeResource p) => p.GetPermitPresentationInfo().sprite).UnwrapOrElse(() => KleiItemsUI.GetNoneBalloonArtistIcon(), null);
			}

			// Token: 0x0600A5B9 RID: 42425 RVA: 0x0034AE4C File Offset: 0x0034904C
			public override string GetName()
			{
				return this.permit.AndThen<string>((BalloonArtistFacadeResource p) => p.Name).UnwrapOrElse(() => KleiItemsUI.GetNoneClothingItemString(PermitCategory.JoyResponse), null);
			}

			// Token: 0x0600A5BA RID: 42426 RVA: 0x0034AEAC File Offset: 0x003490AC
			public override string GetUniqueId()
			{
				return "balloon_artist_facade::" + this.permit.AndThen<string>((BalloonArtistFacadeResource p) => p.Id).UnwrapOr("<none>", null);
			}

			// Token: 0x0600A5BB RID: 42427 RVA: 0x0034AEFB File Offset: 0x003490FB
			public override Option<PermitResource> GetPermitResource()
			{
				return this.permit.AndThen<PermitResource>((BalloonArtistFacadeResource p) => p);
			}

			// Token: 0x0600A5BC RID: 42428 RVA: 0x0034AF28 File Offset: 0x00349128
			public override bool IsUnlocked()
			{
				return this.GetPermitResource().AndThen<bool>((PermitResource p) => p.IsUnlocked()).UnwrapOr(true, null);
			}

			// Token: 0x040092DD RID: 37597
			public Option<BalloonArtistFacadeResource> permit;
		}
	}
}
