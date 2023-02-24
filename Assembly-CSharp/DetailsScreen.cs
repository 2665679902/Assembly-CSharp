using System;
using System.Collections.Generic;
using FMOD.Studio;
using STRINGS;
using UnityEngine;

// Token: 0x02000A8F RID: 2703
public class DetailsScreen : KTabMenu
{
	// Token: 0x060052BC RID: 21180 RVA: 0x001DE86C File Offset: 0x001DCA6C
	public static void DestroyInstance()
	{
		DetailsScreen.Instance = null;
	}

	// Token: 0x1700062B RID: 1579
	// (get) Token: 0x060052BD RID: 21181 RVA: 0x001DE874 File Offset: 0x001DCA74
	// (set) Token: 0x060052BE RID: 21182 RVA: 0x001DE87C File Offset: 0x001DCA7C
	public GameObject target { get; private set; }

	// Token: 0x060052BF RID: 21183 RVA: 0x001DE888 File Offset: 0x001DCA88
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.SortScreenOrder();
		base.ConsumeMouseScroll = true;
		global::Debug.Assert(DetailsScreen.Instance == null);
		DetailsScreen.Instance = this;
		this.DeactivateSideContent();
		this.Show(false);
		base.Subscribe(Game.Instance.gameObject, -1503271301, new Action<object>(this.OnSelectObject));
	}

	// Token: 0x060052C0 RID: 21184 RVA: 0x001DE8ED File Offset: 0x001DCAED
	private void OnSelectObject(object data)
	{
		if (data == null)
		{
			this.previouslyActiveTab = -1;
		}
	}

	// Token: 0x060052C1 RID: 21185 RVA: 0x001DE8FC File Offset: 0x001DCAFC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.CodexEntryButton.onClick += this.OpenCodexEntry;
		this.ChangeOutfitButton.onClick += this.OnClickChangeOutfit;
		this.CloseButton.onClick += this.DeselectAndClose;
		this.TabTitle.OnNameChanged += this.OnNameChanged;
		this.TabTitle.OnStartedEditing += this.OnStartedEditing;
		this.sideScreen2.SetActive(false);
		base.Subscribe<DetailsScreen>(-1514841199, DetailsScreen.OnRefreshDataDelegate);
	}

	// Token: 0x060052C2 RID: 21186 RVA: 0x001DE99F File Offset: 0x001DCB9F
	private void OnStartedEditing()
	{
		base.isEditing = true;
		KScreenManager.Instance.RefreshStack();
	}

	// Token: 0x060052C3 RID: 21187 RVA: 0x001DE9B4 File Offset: 0x001DCBB4
	private void OnNameChanged(string newName)
	{
		base.isEditing = false;
		if (string.IsNullOrEmpty(newName))
		{
			return;
		}
		MinionIdentity component = this.target.GetComponent<MinionIdentity>();
		UserNameable component2 = this.target.GetComponent<UserNameable>();
		ClustercraftExteriorDoor component3 = this.target.GetComponent<ClustercraftExteriorDoor>();
		CommandModule component4 = this.target.GetComponent<CommandModule>();
		if (component != null)
		{
			component.SetName(newName);
		}
		else if (component4 != null)
		{
			SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(component4.GetComponent<LaunchConditionManager>()).SetRocketName(newName);
		}
		else if (component3 != null)
		{
			component3.GetTargetWorld().GetComponent<UserNameable>().SetName(newName);
		}
		else if (component2 != null)
		{
			component2.SetName(newName);
		}
		this.TabTitle.UpdateRenameTooltip(this.target);
	}

	// Token: 0x060052C4 RID: 21188 RVA: 0x001DEA71 File Offset: 0x001DCC71
	protected override void OnDeactivate()
	{
		if (this.target != null && this.setRocketTitleHandle != -1)
		{
			this.target.Unsubscribe(this.setRocketTitleHandle);
		}
		this.setRocketTitleHandle = -1;
		this.DeactivateSideContent();
		base.OnDeactivate();
	}

	// Token: 0x060052C5 RID: 21189 RVA: 0x001DEAAE File Offset: 0x001DCCAE
	protected override void OnShow(bool show)
	{
		if (!show)
		{
			this.DeactivateSideContent();
		}
		else
		{
			this.MaskSideContent(false);
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().MenuOpenHalfEffect);
		}
		base.OnShow(show);
	}

	// Token: 0x060052C6 RID: 21190 RVA: 0x001DEADE File Offset: 0x001DCCDE
	protected override void OnCmpDisable()
	{
		this.DeactivateSideContent();
		base.OnCmpDisable();
	}

	// Token: 0x060052C7 RID: 21191 RVA: 0x001DEAEC File Offset: 0x001DCCEC
	public override void OnKeyUp(KButtonEvent e)
	{
		if (!base.isEditing && this.target != null && PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
		{
			this.DeselectAndClose();
		}
	}

	// Token: 0x060052C8 RID: 21192 RVA: 0x001DEB18 File Offset: 0x001DCD18
	private static Component GetComponent(GameObject go, string name)
	{
		Type type = Type.GetType(name);
		Component component;
		if (type != null)
		{
			component = go.GetComponent(type);
		}
		else
		{
			component = go.GetComponent(name);
		}
		return component;
	}

	// Token: 0x060052C9 RID: 21193 RVA: 0x001DEB4C File Offset: 0x001DCD4C
	private static bool IsExcludedPrefabTag(GameObject go, Tag[] excluded_tags)
	{
		if (excluded_tags == null || excluded_tags.Length == 0)
		{
			return false;
		}
		bool flag = false;
		KPrefabID component = go.GetComponent<KPrefabID>();
		foreach (Tag tag in excluded_tags)
		{
			if (component.PrefabTag == tag)
			{
				flag = true;
				break;
			}
		}
		return flag;
	}

	// Token: 0x060052CA RID: 21194 RVA: 0x001DEB94 File Offset: 0x001DCD94
	private void UpdateCodexButton()
	{
		string selectedObjectCodexID = this.GetSelectedObjectCodexID();
		this.CodexEntryButton.isInteractable = selectedObjectCodexID != "";
		this.CodexEntryButton.GetComponent<ToolTip>().SetSimpleTooltip(this.CodexEntryButton.isInteractable ? UI.TOOLTIPS.OPEN_CODEX_ENTRY : UI.TOOLTIPS.NO_CODEX_ENTRY);
	}

	// Token: 0x060052CB RID: 21195 RVA: 0x001DEBEC File Offset: 0x001DCDEC
	private void UpdateOutfitButton()
	{
		this.ChangeOutfitButton.gameObject.SetActive(this.target.GetComponent<MinionIdentity>());
	}

	// Token: 0x060052CC RID: 21196 RVA: 0x001DEC10 File Offset: 0x001DCE10
	public void OnRefreshData(object obj)
	{
		this.SetTitle(base.PreviousActiveTab);
		for (int i = 0; i < this.tabs.Count; i++)
		{
			if (this.tabs[i].gameObject.activeInHierarchy)
			{
				this.tabs[i].Trigger(-1514841199, obj);
			}
		}
	}

	// Token: 0x060052CD RID: 21197 RVA: 0x001DEC70 File Offset: 0x001DCE70
	public void Refresh(GameObject go)
	{
		if (this.screens == null)
		{
			return;
		}
		if (this.target != go && this.setRocketTitleHandle != -1)
		{
			this.target.Unsubscribe(this.setRocketTitleHandle);
			this.setRocketTitleHandle = -1;
		}
		this.target = go;
		this.sortedSideScreens.Clear();
		CellSelectionObject component = this.target.GetComponent<CellSelectionObject>();
		if (component)
		{
			component.OnObjectSelected(null);
		}
		if (!this.HasActivated)
		{
			if (this.screens != null)
			{
				for (int i = 0; i < this.screens.Length; i++)
				{
					GameObject gameObject = KScreenManager.Instance.InstantiateScreen(this.screens[i].screen.gameObject, this.body.gameObject).gameObject;
					this.screens[i].screen = gameObject.GetComponent<TargetScreen>();
					this.screens[i].tabIdx = base.AddTab(this.screens[i].icon, Strings.Get(this.screens[i].displayName), this.screens[i].screen, Strings.Get(this.screens[i].tooltip));
				}
			}
			base.onTabActivated += this.OnTabActivated;
			this.HasActivated = true;
		}
		int num = -1;
		int num2 = 0;
		for (int j = 0; j < this.screens.Length; j++)
		{
			bool flag = this.screens[j].screen.IsValidForTarget(go);
			bool flag2 = this.screens[j].hideWhenDead && base.gameObject.HasTag(GameTags.Dead);
			bool flag3 = flag && !flag2;
			base.SetTabEnabled(this.screens[j].tabIdx, flag3);
			if (flag3)
			{
				num2++;
				if (num == -1)
				{
					if (SimDebugView.Instance.GetMode() != OverlayModes.None.ID)
					{
						if (SimDebugView.Instance.GetMode() == this.screens[j].focusInViewMode)
						{
							num = j;
						}
					}
					else if (flag3 && this.previouslyActiveTab >= 0 && this.previouslyActiveTab < this.screens.Length && this.screens[j].name == this.screens[this.previouslyActiveTab].name)
					{
						num = this.screens[j].tabIdx;
					}
				}
			}
		}
		if (num != -1)
		{
			this.ActivateTab(num);
		}
		else
		{
			this.ActivateTab(0);
		}
		this.tabHeaderContainer.gameObject.SetActive(base.CountTabs() > 1);
		if (this.sideScreens != null && this.sideScreens.Count > 0)
		{
			bool areAnyValid = false;
			this.sideScreens.ForEach(delegate(DetailsScreen.SideScreenRef scn)
			{
				if (!scn.screenPrefab.IsValidForTarget(this.target))
				{
					if (scn.screenInstance != null && scn.screenInstance.gameObject.activeSelf)
					{
						scn.screenInstance.gameObject.SetActive(false);
					}
					return;
				}
				areAnyValid = true;
				if (scn.screenInstance == null)
				{
					scn.screenInstance = global::Util.KInstantiateUI<SideScreenContent>(scn.screenPrefab.gameObject, this.sideScreenContentBody, false);
				}
				if (!this.sideScreen.activeSelf)
				{
					this.sideScreen.SetActive(true);
				}
				scn.screenInstance.SetTarget(this.target);
				scn.screenInstance.Show(true);
				int sideScreenSortOrder = scn.screenInstance.GetSideScreenSortOrder();
				this.sortedSideScreens.Add(new KeyValuePair<GameObject, int>(scn.screenInstance.gameObject, sideScreenSortOrder));
				if (this.currentSideScreen == null || !this.currentSideScreen.gameObject.activeSelf || sideScreenSortOrder > this.sortedSideScreens.Find((KeyValuePair<GameObject, int> match) => match.Key == this.currentSideScreen.gameObject).Value)
				{
					this.currentSideScreen = scn.screenInstance;
				}
				this.RefreshTitle();
			});
			if (!areAnyValid)
			{
				this.sideScreen.SetActive(false);
			}
		}
		this.sortedSideScreens.Sort(delegate(KeyValuePair<GameObject, int> x, KeyValuePair<GameObject, int> y)
		{
			if (x.Value <= y.Value)
			{
				return 1;
			}
			return -1;
		});
		for (int k = 0; k < this.sortedSideScreens.Count; k++)
		{
			this.sortedSideScreens[k].Key.transform.SetSiblingIndex(k);
		}
	}

	// Token: 0x060052CE RID: 21198 RVA: 0x001DF00E File Offset: 0x001DD20E
	public void RefreshTitle()
	{
		if (this.currentSideScreen)
		{
			this.sideScreenTitle.SetText(this.currentSideScreen.GetTitle());
		}
	}

	// Token: 0x060052CF RID: 21199 RVA: 0x001DF034 File Offset: 0x001DD234
	private void OnTabActivated(int newTab, int oldTab)
	{
		this.SetTitle(newTab);
		if (oldTab != -1)
		{
			this.screens[oldTab].screen.SetTarget(null);
		}
		if (newTab != -1)
		{
			this.screens[newTab].screen.SetTarget(this.target);
		}
	}

	// Token: 0x060052D0 RID: 21200 RVA: 0x001DF084 File Offset: 0x001DD284
	public KScreen SetSecondarySideScreen(KScreen secondaryPrefab, string title)
	{
		this.ClearSecondarySideScreen();
		if (this.instantiatedSecondarySideScreens.ContainsKey(secondaryPrefab))
		{
			this.activeSideScreen2 = this.instantiatedSecondarySideScreens[secondaryPrefab];
			this.activeSideScreen2.gameObject.SetActive(true);
		}
		else
		{
			this.activeSideScreen2 = KScreenManager.Instance.InstantiateScreen(secondaryPrefab.gameObject, this.sideScreen2ContentBody);
			this.activeSideScreen2.Activate();
			this.instantiatedSecondarySideScreens.Add(secondaryPrefab, this.activeSideScreen2);
		}
		this.sideScreen2Title.text = title;
		this.sideScreen2.SetActive(true);
		return this.activeSideScreen2;
	}

	// Token: 0x060052D1 RID: 21201 RVA: 0x001DF121 File Offset: 0x001DD321
	public void ClearSecondarySideScreen()
	{
		if (this.activeSideScreen2 != null)
		{
			this.activeSideScreen2.gameObject.SetActive(false);
			this.activeSideScreen2 = null;
		}
		this.sideScreen2.SetActive(false);
	}

	// Token: 0x060052D2 RID: 21202 RVA: 0x001DF158 File Offset: 0x001DD358
	public void DeactivateSideContent()
	{
		if (SideDetailsScreen.Instance != null && SideDetailsScreen.Instance.gameObject.activeInHierarchy)
		{
			SideDetailsScreen.Instance.Show(false);
		}
		if (this.sideScreens != null && this.sideScreens.Count > 0)
		{
			this.sideScreens.ForEach(delegate(DetailsScreen.SideScreenRef scn)
			{
				if (scn.screenInstance != null)
				{
					scn.screenInstance.ClearTarget();
					scn.screenInstance.Show(false);
				}
			});
		}
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().MenuOpenHalfEffect, STOP_MODE.ALLOWFADEOUT);
		this.sideScreen.SetActive(false);
	}

	// Token: 0x060052D3 RID: 21203 RVA: 0x001DF1F0 File Offset: 0x001DD3F0
	public void MaskSideContent(bool hide)
	{
		if (hide)
		{
			this.sideScreen.transform.localScale = Vector3.zero;
			return;
		}
		this.sideScreen.transform.localScale = Vector3.one;
	}

	// Token: 0x060052D4 RID: 21204 RVA: 0x001DF220 File Offset: 0x001DD420
	private string GetSelectedObjectCodexID()
	{
		string text = "";
		global::Debug.Assert(this.target != null, "Details Screen has no target");
		KSelectable component = this.target.GetComponent<KSelectable>();
		DebugUtil.AssertArgs(component != null, new object[] { "Details Screen target is not a KSelectable", this.target });
		CellSelectionObject component2 = component.GetComponent<CellSelectionObject>();
		BuildingUnderConstruction component3 = component.GetComponent<BuildingUnderConstruction>();
		CreatureBrain component4 = component.GetComponent<CreatureBrain>();
		PlantableSeed component5 = component.GetComponent<PlantableSeed>();
		BudUprootedMonitor component6 = component.GetComponent<BudUprootedMonitor>();
		if (component2 != null)
		{
			text = CodexCache.FormatLinkID(component2.element.id.ToString());
		}
		else if (component3 != null)
		{
			text = CodexCache.FormatLinkID(component3.Def.PrefabID);
		}
		else if (component4 != null)
		{
			text = CodexCache.FormatLinkID(component.PrefabID().ToString());
			text = text.Replace("BABY", "");
		}
		else if (component5 != null)
		{
			text = CodexCache.FormatLinkID(component.PrefabID().ToString());
			text = text.Replace("SEED", "");
		}
		else if (component6 != null)
		{
			if (component6.parentObject.Get() != null)
			{
				text = CodexCache.FormatLinkID(component6.parentObject.Get().PrefabID().ToString());
			}
			else if (component6.GetComponent<TreeBud>() != null)
			{
				text = CodexCache.FormatLinkID(component6.GetComponent<TreeBud>().buddingTrunk.Get().PrefabID().ToString());
			}
		}
		else
		{
			text = CodexCache.FormatLinkID(component.PrefabID().ToString());
		}
		if (CodexCache.entries.ContainsKey(text) || CodexCache.FindSubEntry(text) != null)
		{
			return text;
		}
		return "";
	}

	// Token: 0x060052D5 RID: 21205 RVA: 0x001DF418 File Offset: 0x001DD618
	public void OpenCodexEntry()
	{
		string selectedObjectCodexID = this.GetSelectedObjectCodexID();
		if (selectedObjectCodexID != "")
		{
			ManagementMenu.Instance.OpenCodexToEntry(selectedObjectCodexID, null);
		}
	}

	// Token: 0x060052D6 RID: 21206 RVA: 0x001DF448 File Offset: 0x001DD648
	public void OnClickChangeOutfit()
	{
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().FrontEndSupplyClosetSnapshot);
		MinionBrowserScreenConfig.MinionInstances(this.target).ApplyAndOpenScreen(delegate
		{
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndSupplyClosetSnapshot, STOP_MODE.ALLOWFADEOUT);
		});
	}

	// Token: 0x060052D7 RID: 21207 RVA: 0x001DF4A4 File Offset: 0x001DD6A4
	public void DeselectAndClose()
	{
		if (base.gameObject.activeInHierarchy)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Back", false));
		}
		if (this.GetActiveTab() != null)
		{
			this.GetActiveTab().SetTarget(null);
		}
		SelectTool.Instance.Select(null, false);
		ClusterMapSelectTool.Instance.Select(null, false);
		if (this.target == null)
		{
			return;
		}
		this.target = null;
		this.DeactivateSideContent();
		this.Show(false);
	}

	// Token: 0x060052D8 RID: 21208 RVA: 0x001DF523 File Offset: 0x001DD723
	private void SortScreenOrder()
	{
		Array.Sort<DetailsScreen.Screens>(this.screens, (DetailsScreen.Screens x, DetailsScreen.Screens y) => x.displayOrderPriority.CompareTo(y.displayOrderPriority));
	}

	// Token: 0x060052D9 RID: 21209 RVA: 0x001DF550 File Offset: 0x001DD750
	public void UpdatePortrait(GameObject target)
	{
		KSelectable component = target.GetComponent<KSelectable>();
		if (component == null)
		{
			return;
		}
		this.TabTitle.portrait.ClearPortrait();
		Building component2 = component.GetComponent<Building>();
		if (component2)
		{
			Sprite uisprite = component2.Def.GetUISprite("ui", false);
			if (uisprite != null)
			{
				this.TabTitle.portrait.SetPortrait(uisprite);
				return;
			}
		}
		if (target.GetComponent<MinionIdentity>())
		{
			this.TabTitle.SetPortrait(component.gameObject);
			return;
		}
		Edible component3 = target.GetComponent<Edible>();
		if (component3 != null)
		{
			Sprite uispriteFromMultiObjectAnim = Def.GetUISpriteFromMultiObjectAnim(component3.GetComponent<KBatchedAnimController>().AnimFiles[0], "ui", false, "");
			this.TabTitle.portrait.SetPortrait(uispriteFromMultiObjectAnim);
			return;
		}
		PrimaryElement component4 = target.GetComponent<PrimaryElement>();
		if (component4 != null)
		{
			this.TabTitle.portrait.SetPortrait(Def.GetUISpriteFromMultiObjectAnim(ElementLoader.FindElementByHash(component4.ElementID).substance.anim, "ui", false, ""));
			return;
		}
		CellSelectionObject component5 = target.GetComponent<CellSelectionObject>();
		if (component5 != null)
		{
			string text = (component5.element.IsSolid ? "ui" : component5.element.substance.name);
			Sprite uispriteFromMultiObjectAnim2 = Def.GetUISpriteFromMultiObjectAnim(component5.element.substance.anim, text, false, "");
			this.TabTitle.portrait.SetPortrait(uispriteFromMultiObjectAnim2);
			return;
		}
	}

	// Token: 0x060052DA RID: 21210 RVA: 0x001DF6D4 File Offset: 0x001DD8D4
	public bool CompareTargetWith(GameObject compare)
	{
		return this.target == compare;
	}

	// Token: 0x060052DB RID: 21211 RVA: 0x001DF6E4 File Offset: 0x001DD8E4
	public void SetTitle(int selectedTabIndex)
	{
		this.UpdateCodexButton();
		this.UpdateOutfitButton();
		if (this.TabTitle != null)
		{
			this.TabTitle.SetTitle(this.target.GetProperName());
			MinionIdentity minionIdentity = null;
			UserNameable userNameable = null;
			ClustercraftExteriorDoor clustercraftExteriorDoor = null;
			CommandModule commandModule = null;
			if (this.target != null)
			{
				minionIdentity = this.target.gameObject.GetComponent<MinionIdentity>();
				userNameable = this.target.gameObject.GetComponent<UserNameable>();
				clustercraftExteriorDoor = this.target.gameObject.GetComponent<ClustercraftExteriorDoor>();
				commandModule = this.target.gameObject.GetComponent<CommandModule>();
			}
			if (minionIdentity != null)
			{
				this.TabTitle.SetSubText(minionIdentity.GetComponent<MinionResume>().GetSkillsSubtitle(), "");
				this.TabTitle.SetUserEditable(true);
			}
			else if (userNameable != null)
			{
				this.TabTitle.SetSubText("", "");
				this.TabTitle.SetUserEditable(true);
			}
			else if (commandModule != null)
			{
				this.TrySetRocketTitle(commandModule);
			}
			else if (clustercraftExteriorDoor != null)
			{
				this.TrySetRocketTitle(clustercraftExteriorDoor);
			}
			else
			{
				this.TabTitle.SetSubText("", "");
				this.TabTitle.SetUserEditable(false);
			}
			this.TabTitle.UpdateRenameTooltip(this.target);
		}
	}

	// Token: 0x060052DC RID: 21212 RVA: 0x001DF834 File Offset: 0x001DDA34
	private void TrySetRocketTitle(ClustercraftExteriorDoor clusterCraftDoor)
	{
		if (clusterCraftDoor2.HasTargetWorld())
		{
			WorldContainer targetWorld = clusterCraftDoor2.GetTargetWorld();
			this.TabTitle.SetTitle(targetWorld.GetComponent<ClusterGridEntity>().Name);
			this.TabTitle.SetUserEditable(true);
			this.TabTitle.SetSubText(this.target.GetProperName(), "");
			this.setRocketTitleHandle = -1;
			return;
		}
		if (this.setRocketTitleHandle == -1)
		{
			this.setRocketTitleHandle = this.target.Subscribe(-71801987, delegate(object clusterCraftDoor)
			{
				this.OnRefreshData(null);
				this.target.Unsubscribe(this.setRocketTitleHandle);
				this.setRocketTitleHandle = -1;
			});
		}
	}

	// Token: 0x060052DD RID: 21213 RVA: 0x001DF8C0 File Offset: 0x001DDAC0
	private void TrySetRocketTitle(CommandModule commandModule)
	{
		if (commandModule != null)
		{
			this.TabTitle.SetTitle(SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(commandModule.GetComponent<LaunchConditionManager>()).GetRocketName());
			this.TabTitle.SetUserEditable(true);
		}
		this.TabTitle.SetSubText(this.target.GetProperName(), "");
	}

	// Token: 0x060052DE RID: 21214 RVA: 0x001DF91D File Offset: 0x001DDB1D
	public void SetTitle(string title)
	{
		this.TabTitle.SetTitle(title);
	}

	// Token: 0x060052DF RID: 21215 RVA: 0x001DF92B File Offset: 0x001DDB2B
	public TargetScreen GetActiveTab()
	{
		if (this.previouslyActiveTab >= 0 && this.previouslyActiveTab < this.screens.Length)
		{
			return this.screens[this.previouslyActiveTab].screen;
		}
		return null;
	}

	// Token: 0x040037FA RID: 14330
	public static DetailsScreen Instance;

	// Token: 0x040037FB RID: 14331
	[SerializeField]
	private KButton CodexEntryButton;

	// Token: 0x040037FC RID: 14332
	[SerializeField]
	private KButton ChangeOutfitButton;

	// Token: 0x040037FD RID: 14333
	[Header("Panels")]
	public Transform UserMenuPanel;

	// Token: 0x040037FE RID: 14334
	[Header("Name Editing (disabled)")]
	[SerializeField]
	private KButton CloseButton;

	// Token: 0x040037FF RID: 14335
	[Header("Tabs")]
	[SerializeField]
	private EditableTitleBar TabTitle;

	// Token: 0x04003800 RID: 14336
	[SerializeField]
	private DetailsScreen.Screens[] screens;

	// Token: 0x04003801 RID: 14337
	[SerializeField]
	private GameObject tabHeaderContainer;

	// Token: 0x04003802 RID: 14338
	[Header("Side Screens")]
	[SerializeField]
	private GameObject sideScreenContentBody;

	// Token: 0x04003803 RID: 14339
	[SerializeField]
	private GameObject sideScreen;

	// Token: 0x04003804 RID: 14340
	[SerializeField]
	private LocText sideScreenTitle;

	// Token: 0x04003805 RID: 14341
	[SerializeField]
	private List<DetailsScreen.SideScreenRef> sideScreens;

	// Token: 0x04003806 RID: 14342
	[Header("Secondary Side Screens")]
	[SerializeField]
	private GameObject sideScreen2ContentBody;

	// Token: 0x04003807 RID: 14343
	[SerializeField]
	private GameObject sideScreen2;

	// Token: 0x04003808 RID: 14344
	[SerializeField]
	private LocText sideScreen2Title;

	// Token: 0x04003809 RID: 14345
	private KScreen activeSideScreen2;

	// Token: 0x0400380B RID: 14347
	private bool HasActivated;

	// Token: 0x0400380C RID: 14348
	private SideScreenContent currentSideScreen;

	// Token: 0x0400380D RID: 14349
	private Dictionary<KScreen, KScreen> instantiatedSecondarySideScreens = new Dictionary<KScreen, KScreen>();

	// Token: 0x0400380E RID: 14350
	private static readonly EventSystem.IntraObjectHandler<DetailsScreen> OnRefreshDataDelegate = new EventSystem.IntraObjectHandler<DetailsScreen>(delegate(DetailsScreen component, object data)
	{
		component.OnRefreshData(data);
	});

	// Token: 0x0400380F RID: 14351
	private List<KeyValuePair<GameObject, int>> sortedSideScreens = new List<KeyValuePair<GameObject, int>>();

	// Token: 0x04003810 RID: 14352
	private int setRocketTitleHandle = -1;

	// Token: 0x02001913 RID: 6419
	[Serializable]
	private struct Screens
	{
		// Token: 0x04007337 RID: 29495
		public string name;

		// Token: 0x04007338 RID: 29496
		public string displayName;

		// Token: 0x04007339 RID: 29497
		public string tooltip;

		// Token: 0x0400733A RID: 29498
		public Sprite icon;

		// Token: 0x0400733B RID: 29499
		public TargetScreen screen;

		// Token: 0x0400733C RID: 29500
		public int displayOrderPriority;

		// Token: 0x0400733D RID: 29501
		public bool hideWhenDead;

		// Token: 0x0400733E RID: 29502
		public HashedString focusInViewMode;

		// Token: 0x0400733F RID: 29503
		[HideInInspector]
		public int tabIdx;
	}

	// Token: 0x02001914 RID: 6420
	[Serializable]
	public class SideScreenRef
	{
		// Token: 0x04007340 RID: 29504
		public string name;

		// Token: 0x04007341 RID: 29505
		public SideScreenContent screenPrefab;

		// Token: 0x04007342 RID: 29506
		public Vector2 offset;

		// Token: 0x04007343 RID: 29507
		[HideInInspector]
		public SideScreenContent screenInstance;
	}
}
