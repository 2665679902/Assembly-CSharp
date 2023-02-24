using System;
using System.Runtime.CompilerServices;
using Database;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B2B RID: 2859
public class MinionBrowserScreen : KMonoBehaviour
{
	// Token: 0x0600582D RID: 22573 RVA: 0x001FEB5C File Offset: 0x001FCD5C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.gridLayouter = new GridLayouter
		{
			minCellSize = 112f,
			maxCellSize = 144f,
			targetGridLayout = this.galleryGridContent.GetComponent<GridLayoutGroup>()
		};
		this.galleryGridItemPool = new UIPrefabLocalPool(this.gridItemPrefab, this.galleryGridContent.gameObject);
	}

	// Token: 0x0600582E RID: 22574 RVA: 0x001FEBC0 File Offset: 0x001FCDC0
	protected override void OnCmpEnable()
	{
		if (this.isFirstDisplay)
		{
			this.isFirstDisplay = false;
			this.PopulateGallery();
			this.RefreshPreview();
			this.cycler.Initialize(this.CreateCycleOptions());
			this.editButton.onClick += delegate
			{
				if (this.OnEditClickedFn != null)
				{
					this.OnEditClickedFn();
				}
			};
			this.changeOutfitButton.onClick += this.OnClickChangeOutfit;
		}
		else
		{
			this.RefreshGallery();
			this.RefreshPreview();
		}
		KleiItemsStatusRefresher.AddOrGetListener(this).OnRefreshUI(delegate
		{
			this.RefreshGallery();
			this.RefreshPreview();
		});
		KleiItemsStatusRefresher.RequestRefreshFromServer();
	}

	// Token: 0x0600582F RID: 22575 RVA: 0x001FEC51 File Offset: 0x001FCE51
	private void Update()
	{
		this.gridLayouter.CheckIfShouldResizeGrid();
	}

	// Token: 0x06005830 RID: 22576 RVA: 0x001FEC60 File Offset: 0x001FCE60
	protected override void OnSpawn()
	{
		this.postponeConfiguration = false;
		if (this.Config.isValid)
		{
			this.Configure(this.Config);
			return;
		}
		this.Configure(MinionBrowserScreenConfig.Personalities(default(Option<Personality>)));
	}

	// Token: 0x17000657 RID: 1623
	// (get) Token: 0x06005831 RID: 22577 RVA: 0x001FECA2 File Offset: 0x001FCEA2
	// (set) Token: 0x06005832 RID: 22578 RVA: 0x001FECAA File Offset: 0x001FCEAA
	public MinionBrowserScreenConfig Config { get; private set; }

	// Token: 0x06005833 RID: 22579 RVA: 0x001FECB3 File Offset: 0x001FCEB3
	public void Configure(MinionBrowserScreenConfig config)
	{
		this.Config = config;
		if (this.postponeConfiguration)
		{
			return;
		}
		this.PopulateGallery();
		this.RefreshPreview();
	}

	// Token: 0x06005834 RID: 22580 RVA: 0x001FECD1 File Offset: 0x001FCED1
	private void RefreshGallery()
	{
		if (this.RefreshGalleryFn != null)
		{
			this.RefreshGalleryFn();
		}
	}

	// Token: 0x06005835 RID: 22581 RVA: 0x001FECE8 File Offset: 0x001FCEE8
	public void PopulateGallery()
	{
		this.RefreshGalleryFn = null;
		this.galleryGridItemPool.ReturnAll();
		foreach (MinionBrowserScreen.GridItem gridItem in this.Config.items)
		{
			this.<PopulateGallery>g__AddGridIcon|31_0(gridItem);
		}
		this.RefreshGallery();
		this.SelectMinion(this.Config.defaultSelectedItem.Unwrap());
	}

	// Token: 0x06005836 RID: 22582 RVA: 0x001FED4C File Offset: 0x001FCF4C
	private void SelectMinion(MinionBrowserScreen.GridItem item)
	{
		this.selectedGridItem = item;
		this.RefreshGallery();
		this.RefreshPreview();
		this.UIMinion.GetMinionVoice().PlaySoundUI("voice_land");
	}

	// Token: 0x06005837 RID: 22583 RVA: 0x001FED84 File Offset: 0x001FCF84
	public void RefreshPreview()
	{
		this.UIMinion.SetMinion(this.selectedGridItem.GetPersonality());
		this.UIMinion.ReactToPersonalityChange();
		this.detailsHeaderText.SetText(this.selectedGridItem.GetName());
		this.detailHeaderIcon.sprite = this.selectedGridItem.GetIcon();
		this.RefreshOutfitDescription();
		this.RefreshPreviewButtonsInteractable();
	}

	// Token: 0x06005838 RID: 22584 RVA: 0x001FEDEA File Offset: 0x001FCFEA
	private void RefreshOutfitDescription()
	{
		if (this.RefreshOutfitDescriptionFn != null)
		{
			this.RefreshOutfitDescriptionFn();
		}
	}

	// Token: 0x06005839 RID: 22585 RVA: 0x001FEE00 File Offset: 0x001FD000
	private void OnClickChangeOutfit()
	{
		OutfitBrowserScreenConfig.Minion(this.selectedGridItem).WithOutfit(this.selectedOutfit).ApplyAndOpenScreen();
	}

	// Token: 0x0600583A RID: 22586 RVA: 0x001FEE30 File Offset: 0x001FD030
	private void RefreshPreviewButtonsInteractable()
	{
		this.editButton.isInteractable = true;
		if (this.currentOutfitType == ClothingOutfitUtility.OutfitType.JoyResponse)
		{
			Option<string> joyResponseEditError = this.GetJoyResponseEditError();
			if (joyResponseEditError.IsSome())
			{
				this.editButton.isInteractable = false;
				this.editButton.gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(joyResponseEditError.Unwrap());
				return;
			}
			this.editButton.isInteractable = true;
			this.editButton.gameObject.AddOrGet<ToolTip>().ClearMultiStringTooltip();
		}
	}

	// Token: 0x0600583B RID: 22587 RVA: 0x001FEEAC File Offset: 0x001FD0AC
	private Option<string> GetJoyResponseEditError()
	{
		string joyTrait = this.selectedGridItem.GetPersonality().joyTrait;
		if (!(joyTrait == "BalloonArtist"))
		{
			return Option.Some<string>(UI.JOY_RESPONSE_DESIGNER_SCREEN.TOOLTIP_NO_FACADES_FOR_JOY_TRAIT.Replace("{JoyResponseType}", Db.Get().traits.Get(joyTrait).Name));
		}
		return Option.None;
	}

	// Token: 0x0600583C RID: 22588 RVA: 0x001FEF0C File Offset: 0x001FD10C
	public void SetEditingOutfitType(ClothingOutfitUtility.OutfitType outfitType)
	{
		this.currentOutfitType = outfitType;
		ClothingOutfitUtility.OutfitType outfitType2 = outfitType;
		if (outfitType2 != ClothingOutfitUtility.OutfitType.Clothing)
		{
			if (outfitType2 == ClothingOutfitUtility.OutfitType.JoyResponse)
			{
				this.editButtonText.text = UI.MINION_BROWSER_SCREEN.BUTTON_EDIT_JOY_RESPONSE;
				this.changeOutfitButton.gameObject.SetActive(false);
			}
		}
		else
		{
			this.editButtonText.text = UI.MINION_BROWSER_SCREEN.BUTTON_EDIT_OUTFIT_ITEMS;
			this.changeOutfitButton.gameObject.SetActive(true);
		}
		this.RefreshPreviewButtonsInteractable();
		this.OnEditClickedFn = delegate
		{
			ClothingOutfitUtility.OutfitType outfitType3 = outfitType;
			if (outfitType3 == ClothingOutfitUtility.OutfitType.Clothing)
			{
				OutfitDesignerScreenConfig.Minion(this.selectedOutfit, this.selectedGridItem).ApplyAndOpenScreen();
				return;
			}
			if (outfitType3 != ClothingOutfitUtility.OutfitType.JoyResponse)
			{
				return;
			}
			JoyResponseScreenConfig joyResponseScreenConfig = JoyResponseScreenConfig.From(this.selectedGridItem);
			joyResponseScreenConfig = joyResponseScreenConfig.WithInitialSelection(this.selectedGridItem.GetJoyResponseOutfitTarget().ReadFacadeId().AndThen<BalloonArtistFacadeResource>((string id) => Db.Get().Permits.BalloonArtistFacades.Get(id)));
			joyResponseScreenConfig.ApplyAndOpenScreen();
		};
		this.RefreshOutfitDescriptionFn = delegate
		{
			ClothingOutfitUtility.OutfitType outfitType4 = outfitType;
			if (outfitType4 == ClothingOutfitUtility.OutfitType.Clothing)
			{
				this.selectedOutfit = this.selectedGridItem.GetClothingOutfitTarget();
				this.UIMinion.SetOutfit(this.selectedOutfit);
				this.outfitDescriptionPanel.Refresh(this.selectedOutfit, outfitType);
				return;
			}
			if (outfitType4 != ClothingOutfitUtility.OutfitType.JoyResponse)
			{
				return;
			}
			string text = this.selectedGridItem.GetJoyResponseOutfitTarget().ReadFacadeId().UnwrapOr(null, null);
			this.outfitDescriptionPanel.Refresh((text != null) ? Db.Get().Permits.Get(text) : null, outfitType);
		};
		this.RefreshOutfitDescription();
	}

	// Token: 0x0600583D RID: 22589 RVA: 0x001FEFC8 File Offset: 0x001FD1C8
	private MinionBrowserScreen.CyclerUI.OnSelectedFn[] CreateCycleOptions()
	{
		MinionBrowserScreen.CyclerUI.OnSelectedFn[] array = new MinionBrowserScreen.CyclerUI.OnSelectedFn[2];
		for (int i = 0; i < 2; i++)
		{
			ClothingOutfitUtility.OutfitType outfitType = (ClothingOutfitUtility.OutfitType)i;
			array[i] = delegate
			{
				this.selectedOutfitType = Option.Some<ClothingOutfitUtility.OutfitType>(outfitType);
				this.cycler.SetLabel(outfitType.GetName());
				this.SetEditingOutfitType(outfitType);
				this.RefreshPreview();
			};
		}
		return array;
	}

	// Token: 0x0600583E RID: 22590 RVA: 0x001FF00C File Offset: 0x001FD20C
	private void OnMouseOverToggle()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Mouseover", false));
	}

	// Token: 0x06005842 RID: 22594 RVA: 0x001FF058 File Offset: 0x001FD258
	[CompilerGenerated]
	private void <PopulateGallery>g__AddGridIcon|31_0(MinionBrowserScreen.GridItem item)
	{
		GameObject gameObject = this.galleryGridItemPool.Borrow();
		gameObject.GetComponent<HierarchyReferences>().GetReference<Image>("Icon").sprite = item.GetIcon();
		gameObject.GetComponent<HierarchyReferences>().GetReference<LocText>("Label").SetText(item.GetName());
		MultiToggle toggle = gameObject.GetComponent<MultiToggle>();
		MultiToggle toggle3 = toggle;
		toggle3.onEnter = (System.Action)Delegate.Combine(toggle3.onEnter, new System.Action(this.OnMouseOverToggle));
		MultiToggle toggle2 = toggle;
		toggle2.onClick = (System.Action)Delegate.Combine(toggle2.onClick, new System.Action(delegate
		{
			this.SelectMinion(item);
		}));
		this.RefreshGalleryFn = (System.Action)Delegate.Combine(this.RefreshGalleryFn, new System.Action(delegate
		{
			toggle.ChangeState((item == this.selectedGridItem) ? 1 : 0);
		}));
	}

	// Token: 0x04003BA3 RID: 15267
	[Header("ItemGalleryColumn")]
	[SerializeField]
	private RectTransform galleryGridContent;

	// Token: 0x04003BA4 RID: 15268
	[SerializeField]
	private GameObject gridItemPrefab;

	// Token: 0x04003BA5 RID: 15269
	private GridLayouter gridLayouter;

	// Token: 0x04003BA6 RID: 15270
	[Header("SelectionDetailsColumn")]
	[SerializeField]
	private KleiPermitDioramaVis permitVis;

	// Token: 0x04003BA7 RID: 15271
	[SerializeField]
	private UIMinion UIMinion;

	// Token: 0x04003BA8 RID: 15272
	[SerializeField]
	private LocText detailsHeaderText;

	// Token: 0x04003BA9 RID: 15273
	[SerializeField]
	private Image detailHeaderIcon;

	// Token: 0x04003BAA RID: 15274
	[SerializeField]
	private OutfitDescriptionPanel outfitDescriptionPanel;

	// Token: 0x04003BAB RID: 15275
	[SerializeField]
	private MinionBrowserScreen.CyclerUI cycler;

	// Token: 0x04003BAC RID: 15276
	[SerializeField]
	private KButton editButton;

	// Token: 0x04003BAD RID: 15277
	[SerializeField]
	private LocText editButtonText;

	// Token: 0x04003BAE RID: 15278
	[SerializeField]
	private KButton changeOutfitButton;

	// Token: 0x04003BAF RID: 15279
	private Option<ClothingOutfitUtility.OutfitType> selectedOutfitType;

	// Token: 0x04003BB0 RID: 15280
	private Option<ClothingOutfitTarget> selectedOutfit;

	// Token: 0x04003BB1 RID: 15281
	private MinionBrowserScreen.GridItem selectedGridItem;

	// Token: 0x04003BB2 RID: 15282
	private System.Action OnEditClickedFn;

	// Token: 0x04003BB3 RID: 15283
	private bool isFirstDisplay = true;

	// Token: 0x04003BB5 RID: 15285
	private bool postponeConfiguration = true;

	// Token: 0x04003BB6 RID: 15286
	private UIPrefabLocalPool galleryGridItemPool;

	// Token: 0x04003BB7 RID: 15287
	private System.Action RefreshGalleryFn;

	// Token: 0x04003BB8 RID: 15288
	private System.Action RefreshOutfitDescriptionFn;

	// Token: 0x04003BB9 RID: 15289
	private ClothingOutfitUtility.OutfitType currentOutfitType;

	// Token: 0x020019BE RID: 6590
	private enum MultiToggleState
	{
		// Token: 0x04007517 RID: 29975
		Default,
		// Token: 0x04007518 RID: 29976
		Selected,
		// Token: 0x04007519 RID: 29977
		NonInteractable
	}

	// Token: 0x020019BF RID: 6591
	[Serializable]
	public class CyclerUI
	{
		// Token: 0x06009111 RID: 37137 RVA: 0x00313C58 File Offset: 0x00311E58
		public void Initialize(MinionBrowserScreen.CyclerUI.OnSelectedFn[] cycleOptions)
		{
			this.cyclePrevButton.onClick += this.CyclePrev;
			this.cycleNextButton.onClick += this.CycleNext;
			this.SetCycleOptions(cycleOptions);
		}

		// Token: 0x06009112 RID: 37138 RVA: 0x00313C8F File Offset: 0x00311E8F
		public void SetCycleOptions(MinionBrowserScreen.CyclerUI.OnSelectedFn[] cycleOptions)
		{
			DebugUtil.Assert(cycleOptions != null);
			DebugUtil.Assert(cycleOptions.Length != 0);
			this.cycleOptions = cycleOptions;
			this.GoTo(0);
		}

		// Token: 0x06009113 RID: 37139 RVA: 0x00313CB4 File Offset: 0x00311EB4
		public void GoTo(int wrappingIndex)
		{
			if (this.cycleOptions == null || this.cycleOptions.Length == 0)
			{
				return;
			}
			while (wrappingIndex < 0)
			{
				wrappingIndex += this.cycleOptions.Length;
			}
			while (wrappingIndex >= this.cycleOptions.Length)
			{
				wrappingIndex -= this.cycleOptions.Length;
			}
			this.selectedIndex = wrappingIndex;
			this.cycleOptions[this.selectedIndex]();
		}

		// Token: 0x06009114 RID: 37140 RVA: 0x00313D15 File Offset: 0x00311F15
		public void CyclePrev()
		{
			this.GoTo(this.selectedIndex - 1);
		}

		// Token: 0x06009115 RID: 37141 RVA: 0x00313D25 File Offset: 0x00311F25
		public void CycleNext()
		{
			this.GoTo(this.selectedIndex + 1);
		}

		// Token: 0x06009116 RID: 37142 RVA: 0x00313D35 File Offset: 0x00311F35
		public void SetLabel(string text)
		{
			this.currentLabel.text = text;
		}

		// Token: 0x0400751A RID: 29978
		[SerializeField]
		public KButton cyclePrevButton;

		// Token: 0x0400751B RID: 29979
		[SerializeField]
		public KButton cycleNextButton;

		// Token: 0x0400751C RID: 29980
		[SerializeField]
		public LocText currentLabel;

		// Token: 0x0400751D RID: 29981
		[NonSerialized]
		private int selectedIndex = -1;

		// Token: 0x0400751E RID: 29982
		[NonSerialized]
		private MinionBrowserScreen.CyclerUI.OnSelectedFn[] cycleOptions;

		// Token: 0x02002104 RID: 8452
		// (Invoke) Token: 0x0600A5CB RID: 42443
		public delegate void OnSelectedFn();
	}

	// Token: 0x020019C0 RID: 6592
	public abstract class GridItem : IEquatable<MinionBrowserScreen.GridItem>
	{
		// Token: 0x06009118 RID: 37144
		public abstract string GetName();

		// Token: 0x06009119 RID: 37145
		public abstract Sprite GetIcon();

		// Token: 0x0600911A RID: 37146
		public abstract string GetUniqueId();

		// Token: 0x0600911B RID: 37147
		public abstract Personality GetPersonality();

		// Token: 0x0600911C RID: 37148
		public abstract Option<ClothingOutfitTarget> GetClothingOutfitTarget();

		// Token: 0x0600911D RID: 37149
		public abstract JoyResponseOutfitTarget GetJoyResponseOutfitTarget();

		// Token: 0x0600911E RID: 37150 RVA: 0x00313D54 File Offset: 0x00311F54
		public override bool Equals(object obj)
		{
			MinionBrowserScreen.GridItem gridItem = obj as MinionBrowserScreen.GridItem;
			return gridItem != null && this.Equals(gridItem);
		}

		// Token: 0x0600911F RID: 37151 RVA: 0x00313D74 File Offset: 0x00311F74
		public bool Equals(MinionBrowserScreen.GridItem other)
		{
			return this.GetHashCode() == other.GetHashCode();
		}

		// Token: 0x06009120 RID: 37152 RVA: 0x00313D84 File Offset: 0x00311F84
		public override int GetHashCode()
		{
			return Hash.SDBMLower(this.GetUniqueId());
		}

		// Token: 0x06009121 RID: 37153 RVA: 0x00313D91 File Offset: 0x00311F91
		public override string ToString()
		{
			return this.GetUniqueId();
		}

		// Token: 0x06009122 RID: 37154 RVA: 0x00313D9C File Offset: 0x00311F9C
		public static MinionBrowserScreen.GridItem.MinionInstanceTarget Of(GameObject minionInstance)
		{
			MinionIdentity component = minionInstance.GetComponent<MinionIdentity>();
			return new MinionBrowserScreen.GridItem.MinionInstanceTarget
			{
				minionInstance = minionInstance,
				minionIdentity = component,
				personality = Db.Get().Personalities.Get(component.personalityResourceId)
			};
		}

		// Token: 0x06009123 RID: 37155 RVA: 0x00313DDE File Offset: 0x00311FDE
		public static MinionBrowserScreen.GridItem.PersonalityTarget Of(Personality personality)
		{
			return new MinionBrowserScreen.GridItem.PersonalityTarget
			{
				personality = personality
			};
		}

		// Token: 0x02002105 RID: 8453
		public class MinionInstanceTarget : MinionBrowserScreen.GridItem
		{
			// Token: 0x0600A5CE RID: 42446 RVA: 0x0034AF74 File Offset: 0x00349174
			public override Sprite GetIcon()
			{
				return this.personality.GetMiniIcon();
			}

			// Token: 0x0600A5CF RID: 42447 RVA: 0x0034AF81 File Offset: 0x00349181
			public override string GetName()
			{
				return this.minionIdentity.GetProperName();
			}

			// Token: 0x0600A5D0 RID: 42448 RVA: 0x0034AF90 File Offset: 0x00349190
			public override string GetUniqueId()
			{
				return "minion_instance_id::" + this.minionInstance.GetInstanceID().ToString();
			}

			// Token: 0x0600A5D1 RID: 42449 RVA: 0x0034AFBA File Offset: 0x003491BA
			public override Personality GetPersonality()
			{
				return this.personality;
			}

			// Token: 0x0600A5D2 RID: 42450 RVA: 0x0034AFC2 File Offset: 0x003491C2
			public override Option<ClothingOutfitTarget> GetClothingOutfitTarget()
			{
				return ClothingOutfitTarget.FromMinion(this.minionInstance);
			}

			// Token: 0x0600A5D3 RID: 42451 RVA: 0x0034AFD4 File Offset: 0x003491D4
			public override JoyResponseOutfitTarget GetJoyResponseOutfitTarget()
			{
				return JoyResponseOutfitTarget.FromMinion(this.minionInstance);
			}

			// Token: 0x040092DE RID: 37598
			public GameObject minionInstance;

			// Token: 0x040092DF RID: 37599
			public MinionIdentity minionIdentity;

			// Token: 0x040092E0 RID: 37600
			public Personality personality;
		}

		// Token: 0x02002106 RID: 8454
		public class PersonalityTarget : MinionBrowserScreen.GridItem
		{
			// Token: 0x0600A5D5 RID: 42453 RVA: 0x0034AFE9 File Offset: 0x003491E9
			public override Sprite GetIcon()
			{
				return this.personality.GetMiniIcon();
			}

			// Token: 0x0600A5D6 RID: 42454 RVA: 0x0034AFF6 File Offset: 0x003491F6
			public override string GetName()
			{
				return this.personality.Name;
			}

			// Token: 0x0600A5D7 RID: 42455 RVA: 0x0034B003 File Offset: 0x00349203
			public override string GetUniqueId()
			{
				return "personality::" + this.personality.nameStringKey;
			}

			// Token: 0x0600A5D8 RID: 42456 RVA: 0x0034B01A File Offset: 0x0034921A
			public override Personality GetPersonality()
			{
				return this.personality;
			}

			// Token: 0x0600A5D9 RID: 42457 RVA: 0x0034B022 File Offset: 0x00349222
			public override Option<ClothingOutfitTarget> GetClothingOutfitTarget()
			{
				return ClothingOutfitTarget.TryFromId(this.personality.GetOutfit(ClothingOutfitUtility.OutfitType.Clothing));
			}

			// Token: 0x0600A5DA RID: 42458 RVA: 0x0034B035 File Offset: 0x00349235
			public override JoyResponseOutfitTarget GetJoyResponseOutfitTarget()
			{
				return JoyResponseOutfitTarget.FromPersonality(this.personality);
			}

			// Token: 0x040092E1 RID: 37601
			public Personality personality;
		}
	}
}
