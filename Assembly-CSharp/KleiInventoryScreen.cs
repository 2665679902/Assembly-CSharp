using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Database;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000ACD RID: 2765
public class KleiInventoryScreen : KModalScreen
{
	// Token: 0x1700063A RID: 1594
	// (get) Token: 0x060054BC RID: 21692 RVA: 0x001EBB27 File Offset: 0x001E9D27
	// (set) Token: 0x060054BD RID: 21693 RVA: 0x001EBB2F File Offset: 0x001E9D2F
	private PermitResource SelectedPermit { get; set; }

	// Token: 0x1700063B RID: 1595
	// (get) Token: 0x060054BE RID: 21694 RVA: 0x001EBB38 File Offset: 0x001E9D38
	// (set) Token: 0x060054BF RID: 21695 RVA: 0x001EBB40 File Offset: 0x001E9D40
	private PermitCategory SelectedCategory { get; set; }

	// Token: 0x060054C0 RID: 21696 RVA: 0x001EBB4C File Offset: 0x001E9D4C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.closeButton.onClick += delegate
		{
			this.Show(false);
		};
		base.ConsumeMouseScroll = true;
		this.galleryGridLayouter = new GridLayouter
		{
			minCellSize = 64f,
			maxCellSize = 96f,
			targetGridLayout = this.galleryGridContent.GetComponent<GridLayoutGroup>()
		};
	}

	// Token: 0x060054C1 RID: 21697 RVA: 0x001EBBAF File Offset: 0x001E9DAF
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.Show(false);
		}
		base.OnKeyDown(e);
	}

	// Token: 0x060054C2 RID: 21698 RVA: 0x001EBBD1 File Offset: 0x001E9DD1
	public override float GetSortKey()
	{
		return 20f;
	}

	// Token: 0x060054C3 RID: 21699 RVA: 0x001EBBD8 File Offset: 0x001E9DD8
	protected override void OnActivate()
	{
		this.OnShow(true);
	}

	// Token: 0x060054C4 RID: 21700 RVA: 0x001EBBE1 File Offset: 0x001E9DE1
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			this.galleryGridLayouter.RequestGridResize();
			this.PopulateCategories();
			this.PopulateGallery();
			this.SelectCategory(PermitCategory.Building);
		}
	}

	// Token: 0x060054C5 RID: 21701 RVA: 0x001EBC0B File Offset: 0x001E9E0B
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		KleiItemsStatusRefresher.AddOrGetListener(this).OnRefreshUI(delegate
		{
			this.RefreshCategories();
			this.RefreshGallery();
			this.RefreshDetails();
		});
		KleiItemsStatusRefresher.RequestRefreshFromServer();
	}

	// Token: 0x060054C6 RID: 21702 RVA: 0x001EBC2F File Offset: 0x001E9E2F
	private void Update()
	{
		this.galleryGridLayouter.CheckIfShouldResizeGrid();
	}

	// Token: 0x060054C7 RID: 21703 RVA: 0x001EBC3C File Offset: 0x001E9E3C
	private GameObject GetAvailableGridButton()
	{
		if (this.recycledGalleryGridButtons.Count == 0)
		{
			return Util.KInstantiateUI(this.gridItemPrefab, this.galleryGridContent.gameObject, true);
		}
		GameObject gameObject = this.recycledGalleryGridButtons[0];
		this.recycledGalleryGridButtons.RemoveAt(0);
		return gameObject;
	}

	// Token: 0x060054C8 RID: 21704 RVA: 0x001EBC7B File Offset: 0x001E9E7B
	private void RecycleGalleryGridButton(GameObject button)
	{
		button.GetComponent<MultiToggle>().onClick = null;
		this.recycledGalleryGridButtons.Add(button);
	}

	// Token: 0x060054C9 RID: 21705 RVA: 0x001EBC98 File Offset: 0x001E9E98
	public void PopulateCategories()
	{
		foreach (KeyValuePair<PermitCategory, MultiToggle> keyValuePair in this.categoryToggles)
		{
			UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
		}
		this.categoryToggles.Clear();
		this.emptyCategories.Clear();
		this.AddPermitCategory(PermitCategory.Building);
		this.AddPermitCategory(PermitCategory.Artwork);
		this.AddPermitCategory(PermitCategory.DupeTops);
		this.AddPermitCategory(PermitCategory.DupeBottoms);
		this.AddPermitCategory(PermitCategory.DupeGloves);
		this.AddPermitCategory(PermitCategory.DupeShoes);
		this.AddPermitCategory(PermitCategory.JoyResponse);
	}

	// Token: 0x060054CA RID: 21706 RVA: 0x001EBD40 File Offset: 0x001E9F40
	private void AddPermitCategory(PermitCategory permitCategory)
	{
		GameObject gameObject = Util.KInstantiateUI(this.categoryRowPrefab, this.categoryListContent.gameObject, true);
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		component.GetReference<LocText>("Label").SetText(PermitCategories.GetUppercaseDisplayName(permitCategory));
		component.GetReference<Image>("Icon").sprite = Assets.GetSprite(PermitCategories.GetIconName(permitCategory));
		MultiToggle component2 = gameObject.GetComponent<MultiToggle>();
		MultiToggle multiToggle = component2;
		multiToggle.onEnter = (System.Action)Delegate.Combine(multiToggle.onEnter, new System.Action(this.OnMouseOverToggle));
		component2.onClick = delegate
		{
			this.SelectCategory(permitCategory);
		};
		this.categoryToggles.Add(permitCategory, component2);
		this.emptyCategories.Add(permitCategory, true);
		this.SetCatogoryClickUISound(permitCategory, component2);
	}

	// Token: 0x060054CB RID: 21707 RVA: 0x001EBE28 File Offset: 0x001EA028
	public void PopulateGallery()
	{
		foreach (KeyValuePair<PermitResource, MultiToggle> keyValuePair in this.galleryGridButtons)
		{
			this.RecycleGalleryGridButton(keyValuePair.Value.gameObject);
		}
		this.galleryGridButtons.Clear();
		this.galleryGridLayouter.ImmediateSizeGridToScreenResolution();
		foreach (PermitResource permitResource in Db.Get().Permits.resources)
		{
			if (permitResource.Rarity != PermitRarity.Universal)
			{
				this.AddItemToGallery(permitResource);
			}
		}
	}

	// Token: 0x060054CC RID: 21708 RVA: 0x001EBEF0 File Offset: 0x001EA0F0
	private void AddItemToGallery(PermitResource permit)
	{
		if (this.galleryGridButtons.ContainsKey(permit))
		{
			return;
		}
		PermitPresentationInfo permitPresentationInfo = permit.GetPermitPresentationInfo();
		this.emptyCategories[permit.Category] = false;
		GameObject availableGridButton = this.GetAvailableGridButton();
		HierarchyReferences component = availableGridButton.GetComponent<HierarchyReferences>();
		Image reference = component.GetReference<Image>("Icon");
		LocText reference2 = component.GetReference<LocText>("OwnedCountLabel");
		Image reference3 = component.GetReference<Image>("IsUnownedOverlay");
		MultiToggle component2 = availableGridButton.GetComponent<MultiToggle>();
		reference.sprite = permitPresentationInfo.sprite;
		if (permit.IsOwnable())
		{
			int ownedCount = PermitItems.GetOwnedCount(permit);
			reference2.text = UI.KLEI_INVENTORY_SCREEN.ITEM_PLAYER_OWNED_AMOUNT_ICON.Replace("{OwnedCount}", ownedCount.ToString());
			reference2.gameObject.SetActive(ownedCount > 0);
			reference3.gameObject.SetActive(ownedCount <= 0);
		}
		else
		{
			reference2.gameObject.SetActive(false);
			reference3.gameObject.SetActive(false);
		}
		MultiToggle multiToggle = component2;
		multiToggle.onEnter = (System.Action)Delegate.Combine(multiToggle.onEnter, new System.Action(this.OnMouseOverToggle));
		component2.onClick = delegate
		{
			this.SelectItem(permit);
		};
		this.galleryGridButtons.Add(permit, component2);
		this.SetItemClickUISound(permit, component2);
		KleiItemsUI.ConfigureTooltipOn(availableGridButton, KleiItemsUI.GetTooltipStringFor(permit));
	}

	// Token: 0x060054CD RID: 21709 RVA: 0x001EC06D File Offset: 0x001EA26D
	public void SelectCategory(PermitCategory category)
	{
		if (this.emptyCategories[category])
		{
			return;
		}
		this.SelectedCategory = category;
		this.galleryHeaderLabel.SetText(PermitCategories.GetDisplayName(category));
		this.RefreshCategories();
		this.SelectDefaultCategoryItem();
	}

	// Token: 0x060054CE RID: 21710 RVA: 0x001EC0A4 File Offset: 0x001EA2A4
	private void SelectDefaultCategoryItem()
	{
		foreach (KeyValuePair<PermitResource, MultiToggle> keyValuePair in this.galleryGridButtons)
		{
			if (keyValuePair.Key.Category == this.SelectedCategory)
			{
				this.SelectItem(keyValuePair.Key);
				return;
			}
		}
		this.SelectItem(null);
	}

	// Token: 0x060054CF RID: 21711 RVA: 0x001EC11C File Offset: 0x001EA31C
	public void SelectItem(PermitResource permit)
	{
		this.SelectedPermit = permit;
		this.RefreshGallery();
		this.RefreshDetails();
	}

	// Token: 0x060054D0 RID: 21712 RVA: 0x001EC134 File Offset: 0x001EA334
	private void RefreshGallery()
	{
		foreach (KeyValuePair<PermitResource, MultiToggle> keyValuePair in this.galleryGridButtons)
		{
			PermitResource permitResource;
			MultiToggle multiToggle;
			keyValuePair.Deconstruct(out permitResource, out multiToggle);
			PermitResource permitResource2 = permitResource;
			MultiToggle multiToggle2 = multiToggle;
			multiToggle2.gameObject.SetActive(permitResource2.Category == this.SelectedCategory);
			multiToggle2.ChangeState((permitResource2 == this.SelectedPermit) ? 1 : 0);
			HierarchyReferences component = multiToggle2.gameObject.GetComponent<HierarchyReferences>();
			LocText reference = component.GetReference<LocText>("OwnedCountLabel");
			Image reference2 = component.GetReference<Image>("IsUnownedOverlay");
			if (permitResource2.IsOwnable())
			{
				int ownedCount = PermitItems.GetOwnedCount(permitResource2);
				reference.text = UI.KLEI_INVENTORY_SCREEN.ITEM_PLAYER_OWNED_AMOUNT_ICON.Replace("{OwnedCount}", ownedCount.ToString());
				reference.gameObject.SetActive(ownedCount > 0);
				reference2.gameObject.SetActive(ownedCount <= 0);
			}
			else
			{
				reference.gameObject.SetActive(false);
				reference2.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060054D1 RID: 21713 RVA: 0x001EC250 File Offset: 0x001EA450
	private void RefreshCategories()
	{
		foreach (KeyValuePair<PermitCategory, MultiToggle> keyValuePair in this.categoryToggles)
		{
			PermitCategory key = keyValuePair.Key;
			if (this.emptyCategories[key])
			{
				keyValuePair.Value.ChangeState(2);
			}
			else
			{
				keyValuePair.Value.ChangeState((key == this.SelectedCategory) ? 1 : 0);
			}
		}
	}

	// Token: 0x060054D2 RID: 21714 RVA: 0x001EC2DC File Offset: 0x001EA4DC
	private void RefreshDetails()
	{
		PermitResource selectedPermit = this.SelectedPermit;
		PermitPresentationInfo permitPresentationInfo = selectedPermit.GetPermitPresentationInfo();
		this.permitVis.ConfigureWith(selectedPermit);
		this.selectionHeaderLabel.SetText(selectedPermit.Name);
		this.selectionNameLabel.SetText(selectedPermit.Name);
		this.selectionDescriptionLabel.gameObject.SetActive(!string.IsNullOrWhiteSpace(selectedPermit.Description));
		this.selectionDescriptionLabel.SetText(selectedPermit.Description);
		this.selectionFacadeForLabel.gameObject.SetActive(!string.IsNullOrWhiteSpace(permitPresentationInfo.facadeFor));
		this.selectionFacadeForLabel.SetText(permitPresentationInfo.facadeFor);
		string text = UI.KLEI_INVENTORY_SCREEN.ITEM_RARITY_DETAILS.Replace("{RarityName}", selectedPermit.Rarity.GetLocStringName());
		this.selectionRarityDetailsLabel.gameObject.SetActive(!string.IsNullOrWhiteSpace(text));
		this.selectionRarityDetailsLabel.SetText(text);
		this.selectionOwnedCount.gameObject.SetActive(true);
		if (!selectedPermit.IsOwnable())
		{
			this.selectionOwnedCount.SetText(UI.KLEI_INVENTORY_SCREEN.ITEM_PLAYER_UNLOCKED_BUT_UNOWNABLE);
			return;
		}
		int ownedCount = PermitItems.GetOwnedCount(selectedPermit);
		if (ownedCount > 0)
		{
			this.selectionOwnedCount.SetText(UI.KLEI_INVENTORY_SCREEN.ITEM_PLAYER_OWNED_AMOUNT.Replace("{OwnedCount}", ownedCount.ToString()));
			return;
		}
		this.selectionOwnedCount.SetText(KleiItemsUI.WrapWithColor(UI.KLEI_INVENTORY_SCREEN.ITEM_PLAYER_OWN_NONE, KleiItemsUI.TEXT_COLOR__PERMIT_NOT_OWNED));
	}

	// Token: 0x060054D3 RID: 21715 RVA: 0x001EC440 File Offset: 0x001EA640
	private void SetCatogoryClickUISound(PermitCategory category, MultiToggle toggle)
	{
		if (!this.categoryToggles.ContainsKey(category))
		{
			toggle.states[1].on_click_override_sound_path = "";
			toggle.states[0].on_click_override_sound_path = "";
			return;
		}
		toggle.states[1].on_click_override_sound_path = "General_Category_Click";
		toggle.states[0].on_click_override_sound_path = "General_Category_Click";
	}

	// Token: 0x060054D4 RID: 21716 RVA: 0x001EC4B4 File Offset: 0x001EA6B4
	private void SetItemClickUISound(PermitResource permit, MultiToggle toggle)
	{
		string facadeItemSoundName = KleiInventoryScreen.GetFacadeItemSoundName(permit);
		toggle.states[1].on_click_override_sound_path = facadeItemSoundName + "_Click";
		toggle.states[1].sound_parameter_name = "Unlocked";
		toggle.states[1].sound_parameter_value = (permit.IsUnlocked() ? 1f : 0f);
		toggle.states[1].has_sound_parameter = true;
		toggle.states[0].on_click_override_sound_path = facadeItemSoundName + "_Click";
		toggle.states[0].sound_parameter_name = "Unlocked";
		toggle.states[0].sound_parameter_value = (permit.IsUnlocked() ? 1f : 0f);
		toggle.states[0].has_sound_parameter = true;
	}

	// Token: 0x060054D5 RID: 21717 RVA: 0x001EC59C File Offset: 0x001EA79C
	public static string GetFacadeItemSoundName(PermitResource permit)
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
			if (permit.Category == PermitCategory.Building)
			{
				bool flag;
				BuildingDef buildingDef;
				KleiPermitVisUtil.GetBuildingDef(permit).Deconstruct(out flag, out buildingDef);
				bool flag2 = flag;
				BuildingDef buildingDef2 = buildingDef;
				if (!flag2)
				{
					return "HUD";
				}
				string prefabID = buildingDef2.PrefabID;
				if (prefabID != null)
				{
					if (prefabID == "ExteriorWall")
					{
						return "wall";
					}
					if (prefabID == "FlowerVase" || prefabID == "FlowerVaseWall")
					{
						return "flowervase";
					}
					if (prefabID == "Bed")
					{
						return "bed";
					}
					if (prefabID == "LuxuryBed")
					{
						string id = permit.Id;
						if (id != null)
						{
							if (id == "LuxuryBed_boat")
							{
								return "elegantbed_boat";
							}
							if (id == "LuxuryBed_bouncy")
							{
								return "elegantbed_bouncy";
							}
						}
						return "elegantbed";
					}
					if (prefabID == "CeilingLight")
					{
						return "ceilingLight";
					}
				}
			}
			if (permit.Category == PermitCategory.Artwork)
			{
				bool flag;
				BuildingDef buildingDef;
				KleiPermitVisUtil.GetBuildingDef(permit).Deconstruct(out flag, out buildingDef);
				bool flag3 = flag;
				BuildingDef buildingDef3 = buildingDef;
				if (!flag3)
				{
					return "HUD";
				}
				ArtableStage artableStage = (ArtableStage)permit;
				if (KleiInventoryScreen.<GetFacadeItemSoundName>g__Has|47_0<Sculpture>(buildingDef3))
				{
					if (buildingDef3.PrefabID == "IceSculpture")
					{
						return "icesculpture";
					}
					return "sculpture";
				}
				else if (KleiInventoryScreen.<GetFacadeItemSoundName>g__Has|47_0<Painting>(buildingDef3))
				{
					return "painting";
				}
			}
			if (permit.Category == PermitCategory.JoyResponse && permit is BalloonArtistFacadeResource)
			{
				return "balloon";
			}
			return "HUD";
		}
	}

	// Token: 0x060054D6 RID: 21718 RVA: 0x001EC75F File Offset: 0x001EA95F
	private void OnMouseOverToggle()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Mouseover", false));
	}

	// Token: 0x060054DA RID: 21722 RVA: 0x001EC7C2 File Offset: 0x001EA9C2
	[CompilerGenerated]
	internal static bool <GetFacadeItemSoundName>g__Has|47_0<T>(BuildingDef buildingDef) where T : Component
	{
		return !buildingDef.BuildingComplete.GetComponent<T>().IsNullOrDestroyed();
	}

	// Token: 0x04003996 RID: 14742
	[Header("Header")]
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003997 RID: 14743
	[Header("CategoryColumn")]
	[SerializeField]
	private RectTransform categoryListContent;

	// Token: 0x04003998 RID: 14744
	[SerializeField]
	private GameObject categoryRowPrefab;

	// Token: 0x04003999 RID: 14745
	private Dictionary<PermitCategory, MultiToggle> categoryToggles = new Dictionary<PermitCategory, MultiToggle>();

	// Token: 0x0400399A RID: 14746
	private Dictionary<PermitCategory, bool> emptyCategories = new Dictionary<PermitCategory, bool>();

	// Token: 0x0400399B RID: 14747
	[Header("ItemGalleryColumn")]
	[SerializeField]
	private LocText galleryHeaderLabel;

	// Token: 0x0400399C RID: 14748
	[SerializeField]
	private RectTransform galleryGridContent;

	// Token: 0x0400399D RID: 14749
	[SerializeField]
	private GameObject gridItemPrefab;

	// Token: 0x0400399E RID: 14750
	private Dictionary<PermitResource, MultiToggle> galleryGridButtons = new Dictionary<PermitResource, MultiToggle>();

	// Token: 0x0400399F RID: 14751
	private List<GameObject> recycledGalleryGridButtons = new List<GameObject>();

	// Token: 0x040039A0 RID: 14752
	private GridLayouter galleryGridLayouter;

	// Token: 0x040039A1 RID: 14753
	[Header("SelectionDetailsColumn")]
	[SerializeField]
	private LocText selectionHeaderLabel;

	// Token: 0x040039A2 RID: 14754
	[SerializeField]
	private KleiPermitDioramaVis permitVis;

	// Token: 0x040039A3 RID: 14755
	[SerializeField]
	private LocText selectionNameLabel;

	// Token: 0x040039A4 RID: 14756
	[SerializeField]
	private LocText selectionDescriptionLabel;

	// Token: 0x040039A5 RID: 14757
	[SerializeField]
	private LocText selectionFacadeForLabel;

	// Token: 0x040039A6 RID: 14758
	[SerializeField]
	private LocText selectionRarityDetailsLabel;

	// Token: 0x040039A7 RID: 14759
	[SerializeField]
	private LocText selectionOwnedCount;

	// Token: 0x0200194F RID: 6479
	private enum MultiToggleState
	{
		// Token: 0x040073FF RID: 29695
		Default,
		// Token: 0x04007400 RID: 29696
		Selected,
		// Token: 0x04007401 RID: 29697
		NonInteractable
	}
}
