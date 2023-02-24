using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020009F2 RID: 2546
public class BuildMenuCategoriesScreen : KIconToggleMenu
{
	// Token: 0x06004C28 RID: 19496 RVA: 0x001AB62B File Offset: 0x001A982B
	public override float GetSortKey()
	{
		return 7f;
	}

	// Token: 0x170005BD RID: 1469
	// (get) Token: 0x06004C29 RID: 19497 RVA: 0x001AB632 File Offset: 0x001A9832
	public HashedString Category
	{
		get
		{
			return this.category;
		}
	}

	// Token: 0x06004C2A RID: 19498 RVA: 0x001AB63A File Offset: 0x001A983A
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.onSelect += this.OnClickCategory;
	}

	// Token: 0x06004C2B RID: 19499 RVA: 0x001AB654 File Offset: 0x001A9854
	public void Configure(HashedString category, int depth, object data, Dictionary<HashedString, List<BuildingDef>> categorized_building_map, Dictionary<HashedString, List<HashedString>> categorized_category_map, BuildMenuBuildingsScreen buildings_screen)
	{
		this.category = category;
		this.categorizedBuildingMap = categorized_building_map;
		this.categorizedCategoryMap = categorized_category_map;
		this.buildingsScreen = buildings_screen;
		List<KIconToggleMenu.ToggleInfo> list = new List<KIconToggleMenu.ToggleInfo>();
		if (typeof(IList<BuildMenu.BuildingInfo>).IsAssignableFrom(data.GetType()))
		{
			this.buildingInfos = (IList<BuildMenu.BuildingInfo>)data;
		}
		else if (typeof(IList<BuildMenu.DisplayInfo>).IsAssignableFrom(data.GetType()))
		{
			this.subcategories = new List<HashedString>();
			foreach (BuildMenu.DisplayInfo displayInfo in ((IList<BuildMenu.DisplayInfo>)data))
			{
				string iconName = displayInfo.iconName;
				string text = HashCache.Get().Get(displayInfo.category).ToUpper();
				text = text.Replace(" ", "");
				KIconToggleMenu.ToggleInfo toggleInfo = new KIconToggleMenu.ToggleInfo(Strings.Get("STRINGS.UI.NEWBUILDCATEGORIES." + text + ".NAME"), iconName, new BuildMenuCategoriesScreen.UserData
				{
					category = displayInfo.category,
					depth = depth,
					requirementsState = PlanScreen.RequirementsState.Tech
				}, displayInfo.hotkey, Strings.Get("STRINGS.UI.NEWBUILDCATEGORIES." + text + ".TOOLTIP"), "");
				list.Add(toggleInfo);
				this.subcategories.Add(displayInfo.category);
			}
			base.Setup(list);
			this.toggles.ForEach(delegate(KToggle to)
			{
				foreach (ImageToggleState imageToggleState in to.GetComponents<ImageToggleState>())
				{
					if (imageToggleState.TargetImage.sprite != null && imageToggleState.TargetImage.name == "FG" && !imageToggleState.useSprites)
					{
						imageToggleState.SetSprites(Assets.GetSprite(imageToggleState.TargetImage.sprite.name + "_disabled"), imageToggleState.TargetImage.sprite, imageToggleState.TargetImage.sprite, Assets.GetSprite(imageToggleState.TargetImage.sprite.name + "_disabled"));
					}
				}
				to.GetComponent<KToggle>().soundPlayer.Enabled = false;
			});
		}
		this.UpdateBuildableStates(true);
	}

	// Token: 0x06004C2C RID: 19500 RVA: 0x001AB7FC File Offset: 0x001A99FC
	private void OnClickCategory(KIconToggleMenu.ToggleInfo toggle_info)
	{
		BuildMenuCategoriesScreen.UserData userData = (BuildMenuCategoriesScreen.UserData)toggle_info.userData;
		PlanScreen.RequirementsState requirementsState = userData.requirementsState;
		if (requirementsState - PlanScreen.RequirementsState.Materials <= 1)
		{
			if (this.selectedCategory != userData.category)
			{
				this.selectedCategory = userData.category;
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click", false));
			}
			else
			{
				this.selectedCategory = HashedString.Invalid;
				this.ClearSelection();
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Deselect", false));
			}
		}
		else
		{
			this.selectedCategory = HashedString.Invalid;
			this.ClearSelection();
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
		}
		toggle_info.toggle.GetComponent<PlanCategoryNotifications>().ToggleAttention(false);
		if (this.onCategoryClicked != null)
		{
			this.onCategoryClicked(this.selectedCategory, userData.depth);
		}
	}

	// Token: 0x06004C2D RID: 19501 RVA: 0x001AB8C8 File Offset: 0x001A9AC8
	private void UpdateButtonStates()
	{
		if (this.toggleInfo != null && this.toggleInfo.Count > 0)
		{
			foreach (KIconToggleMenu.ToggleInfo toggleInfo in this.toggleInfo)
			{
				BuildMenuCategoriesScreen.UserData userData = (BuildMenuCategoriesScreen.UserData)toggleInfo.userData;
				HashedString hashedString = userData.category;
				PlanScreen.RequirementsState categoryRequirements = this.GetCategoryRequirements(hashedString);
				bool flag = categoryRequirements == PlanScreen.RequirementsState.Tech;
				toggleInfo.toggle.gameObject.SetActive(!flag);
				if (categoryRequirements != PlanScreen.RequirementsState.Materials)
				{
					if (categoryRequirements == PlanScreen.RequirementsState.Complete)
					{
						ImageToggleState.State state = ((!this.selectedCategory.IsValid || hashedString != this.selectedCategory) ? ImageToggleState.State.Inactive : ImageToggleState.State.Active);
						if (userData.currentToggleState == null || userData.currentToggleState.GetValueOrDefault() != state)
						{
							userData.currentToggleState = new ImageToggleState.State?(state);
							this.SetImageToggleState(toggleInfo.toggle.gameObject, state);
						}
					}
				}
				else
				{
					toggleInfo.toggle.fgImage.SetAlpha(flag ? 0.2509804f : 1f);
					ImageToggleState.State state2 = ((this.selectedCategory.IsValid && hashedString == this.selectedCategory) ? ImageToggleState.State.DisabledActive : ImageToggleState.State.Disabled);
					if (userData.currentToggleState == null || userData.currentToggleState.GetValueOrDefault() != state2)
					{
						userData.currentToggleState = new ImageToggleState.State?(state2);
						this.SetImageToggleState(toggleInfo.toggle.gameObject, state2);
					}
				}
				toggleInfo.toggle.fgImage.transform.Find("ResearchIcon").gameObject.gameObject.SetActive(flag);
			}
		}
	}

	// Token: 0x06004C2E RID: 19502 RVA: 0x001ABA8C File Offset: 0x001A9C8C
	private void SetImageToggleState(GameObject target, ImageToggleState.State state)
	{
		ImageToggleState[] components = target.GetComponents<ImageToggleState>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].SetState(state);
		}
	}

	// Token: 0x06004C2F RID: 19503 RVA: 0x001ABAB8 File Offset: 0x001A9CB8
	private PlanScreen.RequirementsState GetCategoryRequirements(HashedString category)
	{
		bool flag = true;
		bool flag2 = true;
		List<BuildingDef> list;
		if (this.categorizedBuildingMap.TryGetValue(category, out list))
		{
			if (list.Count <= 0)
			{
				goto IL_F3;
			}
			using (List<BuildingDef>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BuildingDef buildingDef = enumerator.Current;
					if (buildingDef.ShowInBuildMenu && buildingDef.IsAvailable())
					{
						PlanScreen.RequirementsState requirementsState = BuildMenu.Instance.BuildableState(buildingDef);
						flag = flag && requirementsState == PlanScreen.RequirementsState.Tech;
						flag2 = flag2 && (requirementsState == PlanScreen.RequirementsState.Materials || requirementsState == PlanScreen.RequirementsState.Tech);
					}
				}
				goto IL_F3;
			}
		}
		List<HashedString> list2;
		if (this.categorizedCategoryMap.TryGetValue(category, out list2))
		{
			foreach (HashedString hashedString in list2)
			{
				PlanScreen.RequirementsState categoryRequirements = this.GetCategoryRequirements(hashedString);
				flag = flag && categoryRequirements == PlanScreen.RequirementsState.Tech;
				flag2 = flag2 && (categoryRequirements == PlanScreen.RequirementsState.Materials || categoryRequirements == PlanScreen.RequirementsState.Tech);
			}
		}
		IL_F3:
		PlanScreen.RequirementsState requirementsState2;
		if (flag)
		{
			requirementsState2 = PlanScreen.RequirementsState.Tech;
		}
		else if (flag2)
		{
			requirementsState2 = PlanScreen.RequirementsState.Materials;
		}
		else
		{
			requirementsState2 = PlanScreen.RequirementsState.Complete;
		}
		if (DebugHandler.InstantBuildMode)
		{
			requirementsState2 = PlanScreen.RequirementsState.Complete;
		}
		return requirementsState2;
	}

	// Token: 0x06004C30 RID: 19504 RVA: 0x001ABBF0 File Offset: 0x001A9DF0
	public void UpdateNotifications(ICollection<HashedString> updated_categories)
	{
		if (this.toggleInfo == null)
		{
			return;
		}
		this.UpdateBuildableStates(false);
		foreach (KIconToggleMenu.ToggleInfo toggleInfo in this.toggleInfo)
		{
			HashedString hashedString = ((BuildMenuCategoriesScreen.UserData)toggleInfo.userData).category;
			if (updated_categories.Contains(hashedString))
			{
				toggleInfo.toggle.gameObject.GetComponent<PlanCategoryNotifications>().ToggleAttention(true);
			}
		}
	}

	// Token: 0x06004C31 RID: 19505 RVA: 0x001ABC78 File Offset: 0x001A9E78
	public override void Close()
	{
		base.Close();
		this.selectedCategory = HashedString.Invalid;
		this.SetHasFocus(false);
		if (this.buildingInfos != null)
		{
			this.buildingsScreen.Close();
		}
	}

	// Token: 0x06004C32 RID: 19506 RVA: 0x001ABCA5 File Offset: 0x001A9EA5
	[ContextMenu("ForceUpdateBuildableStates")]
	private void ForceUpdateBuildableStates()
	{
		this.UpdateBuildableStates(true);
	}

	// Token: 0x06004C33 RID: 19507 RVA: 0x001ABCB0 File Offset: 0x001A9EB0
	public void UpdateBuildableStates(bool skip_flourish)
	{
		if (this.subcategories != null && this.subcategories.Count > 0)
		{
			this.UpdateButtonStates();
			using (IEnumerator<KIconToggleMenu.ToggleInfo> enumerator = this.toggleInfo.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KIconToggleMenu.ToggleInfo toggleInfo = enumerator.Current;
					BuildMenuCategoriesScreen.UserData userData = (BuildMenuCategoriesScreen.UserData)toggleInfo.userData;
					HashedString hashedString = userData.category;
					PlanScreen.RequirementsState categoryRequirements = this.GetCategoryRequirements(hashedString);
					if (userData.requirementsState != categoryRequirements)
					{
						userData.requirementsState = categoryRequirements;
						toggleInfo.userData = userData;
						if (!skip_flourish)
						{
							toggleInfo.toggle.ActivateFlourish(false);
							string text = "NotificationPing";
							if (!toggleInfo.toggle.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag(text))
							{
								toggleInfo.toggle.gameObject.GetComponent<Animator>().Play(text);
								BuildMenu.Instance.PlayNewBuildingSounds();
							}
						}
					}
				}
				return;
			}
		}
		this.buildingsScreen.UpdateBuildableStates();
	}

	// Token: 0x06004C34 RID: 19508 RVA: 0x001ABDB4 File Offset: 0x001A9FB4
	protected override void OnShow(bool show)
	{
		if (this.buildingInfos != null)
		{
			if (show)
			{
				this.buildingsScreen.Configure(this.category, this.buildingInfos);
				this.buildingsScreen.Show(true);
			}
			else
			{
				this.buildingsScreen.Close();
			}
		}
		base.OnShow(show);
	}

	// Token: 0x06004C35 RID: 19509 RVA: 0x001ABE04 File Offset: 0x001AA004
	public override void ClearSelection()
	{
		this.selectedCategory = HashedString.Invalid;
		base.ClearSelection();
		foreach (KToggle ktoggle in this.toggles)
		{
			ktoggle.isOn = false;
		}
	}

	// Token: 0x06004C36 RID: 19510 RVA: 0x001ABE68 File Offset: 0x001AA068
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.modalKeyInputBehaviour)
		{
			if (this.HasFocus)
			{
				if (e.TryConsume(global::Action.Escape))
				{
					Game.Instance.Trigger(288942073, null);
					return;
				}
				base.OnKeyDown(e);
				if (!e.Consumed)
				{
					global::Action action = e.GetAction();
					if (action >= global::Action.BUILD_MENU_START_INTERCEPT)
					{
						e.TryConsume(action);
						return;
					}
				}
			}
		}
		else
		{
			base.OnKeyDown(e);
			if (e.Consumed)
			{
				this.UpdateButtonStates();
			}
		}
	}

	// Token: 0x06004C37 RID: 19511 RVA: 0x001ABED8 File Offset: 0x001AA0D8
	public override void OnKeyUp(KButtonEvent e)
	{
		if (this.modalKeyInputBehaviour)
		{
			if (this.HasFocus)
			{
				if (e.TryConsume(global::Action.Escape))
				{
					Game.Instance.Trigger(288942073, null);
					return;
				}
				base.OnKeyUp(e);
				if (!e.Consumed)
				{
					global::Action action = e.GetAction();
					if (action >= global::Action.BUILD_MENU_START_INTERCEPT)
					{
						e.TryConsume(action);
						return;
					}
				}
			}
		}
		else
		{
			base.OnKeyUp(e);
		}
	}

	// Token: 0x06004C38 RID: 19512 RVA: 0x001ABF3A File Offset: 0x001AA13A
	public override void SetHasFocus(bool has_focus)
	{
		base.SetHasFocus(has_focus);
		if (this.focusIndicator != null)
		{
			this.focusIndicator.color = (has_focus ? this.focusedColour : this.unfocusedColour);
		}
	}

	// Token: 0x04003223 RID: 12835
	public Action<HashedString, int> onCategoryClicked;

	// Token: 0x04003224 RID: 12836
	[SerializeField]
	public bool modalKeyInputBehaviour;

	// Token: 0x04003225 RID: 12837
	[SerializeField]
	private Image focusIndicator;

	// Token: 0x04003226 RID: 12838
	[SerializeField]
	private Color32 focusedColour;

	// Token: 0x04003227 RID: 12839
	[SerializeField]
	private Color32 unfocusedColour;

	// Token: 0x04003228 RID: 12840
	private IList<HashedString> subcategories;

	// Token: 0x04003229 RID: 12841
	private Dictionary<HashedString, List<BuildingDef>> categorizedBuildingMap;

	// Token: 0x0400322A RID: 12842
	private Dictionary<HashedString, List<HashedString>> categorizedCategoryMap;

	// Token: 0x0400322B RID: 12843
	private BuildMenuBuildingsScreen buildingsScreen;

	// Token: 0x0400322C RID: 12844
	private HashedString category;

	// Token: 0x0400322D RID: 12845
	private IList<BuildMenu.BuildingInfo> buildingInfos;

	// Token: 0x0400322E RID: 12846
	private HashedString selectedCategory = HashedString.Invalid;

	// Token: 0x020017FC RID: 6140
	private class UserData
	{
		// Token: 0x04006E9B RID: 28315
		public HashedString category;

		// Token: 0x04006E9C RID: 28316
		public int depth;

		// Token: 0x04006E9D RID: 28317
		public PlanScreen.RequirementsState requirementsState;

		// Token: 0x04006E9E RID: 28318
		public ImageToggleState.State? currentToggleState;
	}
}
