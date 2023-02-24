using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B35 RID: 2869
public class NameDisplayScreen : KScreen
{
	// Token: 0x060058C2 RID: 22722 RVA: 0x0020272B File Offset: 0x0020092B
	public static void DestroyInstance()
	{
		NameDisplayScreen.Instance = null;
	}

	// Token: 0x060058C3 RID: 22723 RVA: 0x00202733 File Offset: 0x00200933
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		NameDisplayScreen.Instance = this;
	}

	// Token: 0x060058C4 RID: 22724 RVA: 0x00202744 File Offset: 0x00200944
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Health.Register(new Action<Health>(this.OnHealthAdded), null);
		Components.Equipment.Register(new Action<Equipment>(this.OnEquipmentAdded), null);
		this.updateSectionIndex = 0;
		this.lateUpdateSections = new List<System.Action>
		{
			new System.Action(this.LateUpdatePart0),
			new System.Action(this.LateUpdatePart1),
			new System.Action(this.LateUpdatePart2)
		};
		this.bindOnOverlayChange();
	}

	// Token: 0x060058C5 RID: 22725 RVA: 0x002027D4 File Offset: 0x002009D4
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.isOverlayChangeBound && OverlayScreen.Instance != null)
		{
			OverlayScreen instance = OverlayScreen.Instance;
			instance.OnOverlayChanged = (Action<HashedString>)Delegate.Remove(instance.OnOverlayChanged, new Action<HashedString>(this.OnOverlayChanged));
			this.isOverlayChangeBound = false;
		}
	}

	// Token: 0x060058C6 RID: 22726 RVA: 0x0020282C File Offset: 0x00200A2C
	private void bindOnOverlayChange()
	{
		if (this.isOverlayChangeBound)
		{
			return;
		}
		if (OverlayScreen.Instance != null)
		{
			OverlayScreen instance = OverlayScreen.Instance;
			instance.OnOverlayChanged = (Action<HashedString>)Delegate.Combine(instance.OnOverlayChanged, new Action<HashedString>(this.OnOverlayChanged));
			this.isOverlayChangeBound = true;
		}
	}

	// Token: 0x060058C7 RID: 22727 RVA: 0x0020287C File Offset: 0x00200A7C
	private void OnOverlayChanged(HashedString new_mode)
	{
		HashedString hashedString = this.lastKnownOverlayID;
		this.lastKnownOverlayID = new_mode;
		if (hashedString != OverlayModes.None.ID && new_mode != OverlayModes.None.ID)
		{
			return;
		}
		bool flag = new_mode == OverlayModes.None.ID;
		int count = this.entries.Count;
		for (int i = 0; i < count; i++)
		{
			NameDisplayScreen.Entry entry = this.entries[i];
			if (!(entry.world_go == null))
			{
				entry.display_go.SetActive(flag);
			}
		}
	}

	// Token: 0x060058C8 RID: 22728 RVA: 0x002028FB File Offset: 0x00200AFB
	private void OnHealthAdded(Health health)
	{
		this.RegisterComponent(health.gameObject, health, false);
	}

	// Token: 0x060058C9 RID: 22729 RVA: 0x0020290C File Offset: 0x00200B0C
	private void OnEquipmentAdded(Equipment equipment)
	{
		MinionAssignablesProxy component = equipment.GetComponent<MinionAssignablesProxy>();
		GameObject targetGameObject = component.GetTargetGameObject();
		if (targetGameObject)
		{
			this.RegisterComponent(targetGameObject, equipment, false);
			return;
		}
		global::Debug.LogWarningFormat("OnEquipmentAdded proxy target {0} was null.", new object[] { component.TargetInstanceID });
	}

	// Token: 0x060058CA RID: 22730 RVA: 0x00202958 File Offset: 0x00200B58
	private bool ShouldShowName(GameObject representedObject)
	{
		CharacterOverlay component = representedObject.GetComponent<CharacterOverlay>();
		return component != null && component.shouldShowName;
	}

	// Token: 0x060058CB RID: 22731 RVA: 0x00202980 File Offset: 0x00200B80
	public Guid AddWorldText(string initialText, GameObject prefab)
	{
		NameDisplayScreen.TextEntry textEntry = new NameDisplayScreen.TextEntry();
		textEntry.guid = Guid.NewGuid();
		textEntry.display_go = Util.KInstantiateUI(prefab, base.gameObject, true);
		textEntry.display_go.GetComponentInChildren<LocText>().text = initialText;
		this.textEntries.Add(textEntry);
		return textEntry.guid;
	}

	// Token: 0x060058CC RID: 22732 RVA: 0x002029D4 File Offset: 0x00200BD4
	public GameObject GetWorldText(Guid guid)
	{
		GameObject gameObject = null;
		foreach (NameDisplayScreen.TextEntry textEntry in this.textEntries)
		{
			if (textEntry.guid == guid)
			{
				gameObject = textEntry.display_go;
				break;
			}
		}
		return gameObject;
	}

	// Token: 0x060058CD RID: 22733 RVA: 0x00202A3C File Offset: 0x00200C3C
	public void RemoveWorldText(Guid guid)
	{
		int num = -1;
		for (int i = 0; i < this.textEntries.Count; i++)
		{
			if (this.textEntries[i].guid == guid)
			{
				num = i;
				break;
			}
		}
		if (num >= 0)
		{
			UnityEngine.Object.Destroy(this.textEntries[num].display_go);
			this.textEntries.RemoveAt(num);
		}
	}

	// Token: 0x060058CE RID: 22734 RVA: 0x00202AA4 File Offset: 0x00200CA4
	public void AddNewEntry(GameObject representedObject)
	{
		NameDisplayScreen.Entry entry = new NameDisplayScreen.Entry();
		entry.world_go = representedObject;
		entry.world_go_anim_controller = representedObject.GetComponent<KAnimControllerBase>();
		GameObject gameObject = Util.KInstantiateUI(this.ShouldShowName(representedObject) ? this.nameAndBarsPrefab : this.barsPrefab, base.gameObject, true);
		entry.display_go = gameObject;
		entry.display_go_rect = gameObject.GetComponent<RectTransform>();
		if (this.worldSpace)
		{
			entry.display_go.transform.localScale = Vector3.one * 0.01f;
		}
		gameObject.name = representedObject.name + " character overlay";
		entry.Name = representedObject.name;
		entry.refs = gameObject.GetComponent<HierarchyReferences>();
		this.entries.Add(entry);
		UnityEngine.Object component = representedObject.GetComponent<KSelectable>();
		FactionAlignment component2 = representedObject.GetComponent<FactionAlignment>();
		if (component != null)
		{
			if (component2 != null)
			{
				if (component2.Alignment == FactionManager.FactionID.Friendly || component2.Alignment == FactionManager.FactionID.Duplicant)
				{
					this.UpdateName(representedObject);
					return;
				}
			}
			else
			{
				this.UpdateName(representedObject);
			}
		}
	}

	// Token: 0x060058CF RID: 22735 RVA: 0x00202BA0 File Offset: 0x00200DA0
	public void RegisterComponent(GameObject representedObject, object component, bool force_new_entry = false)
	{
		NameDisplayScreen.Entry entry = (force_new_entry ? null : this.GetEntry(representedObject));
		if (entry == null)
		{
			CharacterOverlay component2 = representedObject.GetComponent<CharacterOverlay>();
			if (component2 != null)
			{
				component2.Register();
				entry = this.GetEntry(representedObject);
			}
		}
		if (entry == null)
		{
			return;
		}
		Transform reference = entry.refs.GetReference<Transform>("Bars");
		entry.bars_go = reference.gameObject;
		if (component is Health)
		{
			if (!entry.healthBar)
			{
				Health health = (Health)component;
				GameObject gameObject = Util.KInstantiateUI(ProgressBarsConfig.Instance.healthBarPrefab, reference.gameObject, false);
				gameObject.name = "Health Bar";
				health.healthBar = gameObject.GetComponent<HealthBar>();
				health.healthBar.GetComponent<KSelectable>().entityName = UI.METERS.HEALTH.TOOLTIP;
				health.healthBar.GetComponent<KSelectableHealthBar>().IsSelectable = representedObject.GetComponent<MinionBrain>() != null;
				entry.healthBar = health.healthBar;
				entry.healthBar.autoHide = false;
				gameObject.transform.Find("Bar").GetComponent<Image>().color = ProgressBarsConfig.Instance.GetBarColor("HealthBar");
				return;
			}
			global::Debug.LogWarningFormat("Health added twice {0}", new object[] { component });
			return;
		}
		else if (component is OxygenBreather)
		{
			if (!entry.breathBar)
			{
				GameObject gameObject2 = Util.KInstantiateUI(ProgressBarsConfig.Instance.progressBarUIPrefab, reference.gameObject, false);
				entry.breathBar = gameObject2.GetComponent<ProgressBar>();
				entry.breathBar.autoHide = false;
				gameObject2.gameObject.GetComponent<ToolTip>().AddMultiStringTooltip("Breath", this.ToolTipStyle_Property);
				gameObject2.name = "Breath Bar";
				gameObject2.transform.Find("Bar").GetComponent<Image>().color = ProgressBarsConfig.Instance.GetBarColor("BreathBar");
				gameObject2.GetComponent<KSelectable>().entityName = UI.METERS.BREATH.TOOLTIP;
				return;
			}
			global::Debug.LogWarningFormat("OxygenBreather added twice {0}", new object[] { component });
			return;
		}
		else if (component is Equipment)
		{
			if (!entry.suitBar)
			{
				GameObject gameObject3 = Util.KInstantiateUI(ProgressBarsConfig.Instance.progressBarUIPrefab, reference.gameObject, false);
				entry.suitBar = gameObject3.GetComponent<ProgressBar>();
				entry.suitBar.autoHide = false;
				gameObject3.name = "Suit Tank Bar";
				gameObject3.transform.Find("Bar").GetComponent<Image>().color = ProgressBarsConfig.Instance.GetBarColor("OxygenTankBar");
				gameObject3.GetComponent<KSelectable>().entityName = UI.METERS.BREATH.TOOLTIP;
			}
			else
			{
				global::Debug.LogWarningFormat("SuitBar added twice {0}", new object[] { component });
			}
			if (!entry.suitFuelBar)
			{
				GameObject gameObject4 = Util.KInstantiateUI(ProgressBarsConfig.Instance.progressBarUIPrefab, reference.gameObject, false);
				entry.suitFuelBar = gameObject4.GetComponent<ProgressBar>();
				entry.suitFuelBar.autoHide = false;
				gameObject4.name = "Suit Fuel Bar";
				gameObject4.transform.Find("Bar").GetComponent<Image>().color = ProgressBarsConfig.Instance.GetBarColor("FuelTankBar");
				gameObject4.GetComponent<KSelectable>().entityName = UI.METERS.FUEL.TOOLTIP;
			}
			else
			{
				global::Debug.LogWarningFormat("FuelBar added twice {0}", new object[] { component });
			}
			if (!entry.suitBatteryBar)
			{
				GameObject gameObject5 = Util.KInstantiateUI(ProgressBarsConfig.Instance.progressBarUIPrefab, reference.gameObject, false);
				entry.suitBatteryBar = gameObject5.GetComponent<ProgressBar>();
				entry.suitBatteryBar.autoHide = false;
				gameObject5.name = "Suit Battery Bar";
				gameObject5.transform.Find("Bar").GetComponent<Image>().color = ProgressBarsConfig.Instance.GetBarColor("BatteryBar");
				gameObject5.GetComponent<KSelectable>().entityName = UI.METERS.BATTERY.TOOLTIP;
				return;
			}
			global::Debug.LogWarningFormat("CoolantBar added twice {0}", new object[] { component });
			return;
		}
		else if (component is ThoughtGraph.Instance)
		{
			if (!entry.thoughtBubble)
			{
				GameObject gameObject6 = Util.KInstantiateUI(EffectPrefabs.Instance.ThoughtBubble, entry.display_go, false);
				entry.thoughtBubble = gameObject6.GetComponent<HierarchyReferences>();
				gameObject6.name = "Thought Bubble";
				GameObject gameObject7 = Util.KInstantiateUI(EffectPrefabs.Instance.ThoughtBubbleConvo, entry.display_go, false);
				entry.thoughtBubbleConvo = gameObject7.GetComponent<HierarchyReferences>();
				gameObject7.name = "Thought Bubble Convo";
				return;
			}
			global::Debug.LogWarningFormat("ThoughtGraph added twice {0}", new object[] { component });
			return;
		}
		else
		{
			if (!(component is GameplayEventMonitor.Instance))
			{
				if (component is Dreamer.Instance && !entry.dreamBubble)
				{
					GameObject gameObject8 = Util.KInstantiateUI(EffectPrefabs.Instance.DreamBubble, entry.display_go, false);
					gameObject8.name = "Dream Bubble";
					entry.dreamBubble = gameObject8.GetComponent<DreamBubble>();
				}
				return;
			}
			if (!entry.gameplayEventDisplay)
			{
				GameObject gameObject9 = Util.KInstantiateUI(EffectPrefabs.Instance.GameplayEventDisplay, entry.display_go, false);
				entry.gameplayEventDisplay = gameObject9.GetComponent<HierarchyReferences>();
				gameObject9.name = "Gameplay Event Display";
				return;
			}
			global::Debug.LogWarningFormat("GameplayEventDisplay added twice {0}", new object[] { component });
			return;
		}
	}

	// Token: 0x060058D0 RID: 22736 RVA: 0x002030C8 File Offset: 0x002012C8
	private void LateUpdate()
	{
		if (App.isLoading || App.IsExiting)
		{
			return;
		}
		this.bindOnOverlayChange();
		Camera mainCamera = Game.MainCamera;
		if (mainCamera == null)
		{
			return;
		}
		if (this.lastKnownOverlayID != OverlayModes.None.ID)
		{
			return;
		}
		int count = this.entries.Count;
		this.LateUpdatePos(mainCamera.orthographicSize < this.HideDistance);
		this.lateUpdateSections[this.updateSectionIndex]();
		this.updateSectionIndex = (this.updateSectionIndex + 1) % this.lateUpdateSections.Count;
	}

	// Token: 0x060058D1 RID: 22737 RVA: 0x00203160 File Offset: 0x00201360
	private void LateUpdatePos(bool visibleToZoom)
	{
		CameraController instance = CameraController.Instance;
		Transform followTarget = instance.followTarget;
		int count = this.entries.Count;
		for (int i = 0; i < count; i++)
		{
			NameDisplayScreen.Entry entry = this.entries[i];
			GameObject world_go = entry.world_go;
			if (!(world_go == null))
			{
				Vector3 vector = world_go.transform.GetPosition();
				if (visibleToZoom && CameraController.Instance.IsVisiblePos(vector))
				{
					if (instance != null && followTarget == world_go.transform)
					{
						vector = instance.followTargetPos;
					}
					else if (entry.world_go_anim_controller != null)
					{
						vector = entry.world_go_anim_controller.GetWorldPivot();
					}
					entry.display_go_rect.anchoredPosition = (this.worldSpace ? vector : base.WorldToScreen(vector));
					entry.display_go.SetActive(true);
				}
				else if (entry.display_go.activeSelf)
				{
					entry.display_go.SetActive(false);
				}
			}
		}
	}

	// Token: 0x060058D2 RID: 22738 RVA: 0x0020326C File Offset: 0x0020146C
	private void LateUpdatePart0()
	{
		int num = this.entries.Count;
		int i = 0;
		while (i < num)
		{
			if (this.entries[i].world_go == null)
			{
				UnityEngine.Object.Destroy(this.entries[i].display_go);
				num--;
				this.entries[i] = this.entries[num];
			}
			else
			{
				i++;
			}
		}
		this.entries.RemoveRange(num, this.entries.Count - num);
	}

	// Token: 0x060058D3 RID: 22739 RVA: 0x002032F8 File Offset: 0x002014F8
	private void LateUpdatePart1()
	{
		int count = this.entries.Count;
		for (int i = 0; i < count; i++)
		{
			if (!(this.entries[i].world_go == null) && this.entries[i].world_go.HasTag(GameTags.Dead))
			{
				this.entries[i].bars_go.SetActive(false);
			}
		}
	}

	// Token: 0x060058D4 RID: 22740 RVA: 0x0020336C File Offset: 0x0020156C
	private void LateUpdatePart2()
	{
		int count = this.entries.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.entries[i].bars_go != null)
			{
				this.entries[i].bars_go.GetComponentsInChildren<KCollider2D>(false, this.workingList);
				foreach (KCollider2D kcollider2D in this.workingList)
				{
					kcollider2D.MarkDirty(false);
				}
			}
		}
	}

	// Token: 0x060058D5 RID: 22741 RVA: 0x0020340C File Offset: 0x0020160C
	public void UpdateName(GameObject representedObject)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(representedObject);
		if (entry == null)
		{
			return;
		}
		KSelectable component = representedObject.GetComponent<KSelectable>();
		entry.display_go.name = component.GetProperName() + " character overlay";
		LocText componentInChildren = entry.display_go.GetComponentInChildren<LocText>();
		if (componentInChildren != null)
		{
			componentInChildren.text = component.GetProperName();
			if (representedObject.GetComponent<RocketModule>() != null)
			{
				componentInChildren.text = representedObject.GetComponent<RocketModule>().GetParentRocketName();
			}
		}
	}

	// Token: 0x060058D6 RID: 22742 RVA: 0x00203488 File Offset: 0x00201688
	public void SetDream(GameObject minion_go, Dream dream)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.dreamBubble == null)
		{
			return;
		}
		entry.dreamBubble.SetDream(dream);
		entry.dreamBubble.GetComponent<KSelectable>().entityName = "Dreaming";
		entry.dreamBubble.gameObject.SetActive(true);
		entry.dreamBubble.SetVisibility(true);
	}

	// Token: 0x060058D7 RID: 22743 RVA: 0x002034F0 File Offset: 0x002016F0
	public void StopDreaming(GameObject minion_go)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.dreamBubble == null)
		{
			return;
		}
		entry.dreamBubble.StopDreaming();
		entry.dreamBubble.gameObject.SetActive(false);
	}

	// Token: 0x060058D8 RID: 22744 RVA: 0x00203534 File Offset: 0x00201734
	public void DreamTick(GameObject minion_go, float dt)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.dreamBubble == null)
		{
			return;
		}
		entry.dreamBubble.Tick(dt);
	}

	// Token: 0x060058D9 RID: 22745 RVA: 0x00203568 File Offset: 0x00201768
	public void SetThoughtBubbleDisplay(GameObject minion_go, bool bVisible, string hover_text, Sprite bubble_sprite, Sprite topic_sprite)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.thoughtBubble == null)
		{
			return;
		}
		this.ApplyThoughtSprite(entry.thoughtBubble, bubble_sprite, "bubble_sprite");
		this.ApplyThoughtSprite(entry.thoughtBubble, topic_sprite, "icon_sprite");
		entry.thoughtBubble.GetComponent<KSelectable>().entityName = hover_text;
		entry.thoughtBubble.gameObject.SetActive(bVisible);
	}

	// Token: 0x060058DA RID: 22746 RVA: 0x002035D8 File Offset: 0x002017D8
	public void SetThoughtBubbleConvoDisplay(GameObject minion_go, bool bVisible, string hover_text, Sprite bubble_sprite, Sprite topic_sprite, Sprite mode_sprite)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.thoughtBubble == null)
		{
			return;
		}
		this.ApplyThoughtSprite(entry.thoughtBubbleConvo, bubble_sprite, "bubble_sprite");
		this.ApplyThoughtSprite(entry.thoughtBubbleConvo, topic_sprite, "icon_sprite");
		this.ApplyThoughtSprite(entry.thoughtBubbleConvo, mode_sprite, "icon_sprite_mode");
		entry.thoughtBubbleConvo.GetComponent<KSelectable>().entityName = hover_text;
		entry.thoughtBubbleConvo.gameObject.SetActive(bVisible);
	}

	// Token: 0x060058DB RID: 22747 RVA: 0x0020365A File Offset: 0x0020185A
	private void ApplyThoughtSprite(HierarchyReferences active_bubble, Sprite sprite, string target)
	{
		active_bubble.GetReference<Image>(target).sprite = sprite;
	}

	// Token: 0x060058DC RID: 22748 RVA: 0x0020366C File Offset: 0x0020186C
	public void SetGameplayEventDisplay(GameObject minion_go, bool bVisible, string hover_text, Sprite sprite)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.gameplayEventDisplay == null)
		{
			return;
		}
		entry.gameplayEventDisplay.GetReference<Image>("icon_sprite").sprite = sprite;
		entry.gameplayEventDisplay.GetComponent<KSelectable>().entityName = hover_text;
		entry.gameplayEventDisplay.gameObject.SetActive(bVisible);
	}

	// Token: 0x060058DD RID: 22749 RVA: 0x002036CC File Offset: 0x002018CC
	public void SetBreathDisplay(GameObject minion_go, Func<float> updatePercentFull, bool bVisible)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.breathBar == null)
		{
			return;
		}
		entry.breathBar.SetUpdateFunc(updatePercentFull);
		entry.breathBar.gameObject.SetActive(bVisible);
	}

	// Token: 0x060058DE RID: 22750 RVA: 0x00203710 File Offset: 0x00201910
	public void SetHealthDisplay(GameObject minion_go, Func<float> updatePercentFull, bool bVisible)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.healthBar == null)
		{
			return;
		}
		entry.healthBar.OnChange();
		entry.healthBar.SetUpdateFunc(updatePercentFull);
		if (entry.healthBar.gameObject.activeSelf != bVisible)
		{
			entry.healthBar.gameObject.SetActive(bVisible);
		}
	}

	// Token: 0x060058DF RID: 22751 RVA: 0x00203774 File Offset: 0x00201974
	public void SetSuitTankDisplay(GameObject minion_go, Func<float> updatePercentFull, bool bVisible)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.suitBar == null)
		{
			return;
		}
		entry.suitBar.SetUpdateFunc(updatePercentFull);
		entry.suitBar.gameObject.SetActive(bVisible);
	}

	// Token: 0x060058E0 RID: 22752 RVA: 0x002037B8 File Offset: 0x002019B8
	public void SetSuitFuelDisplay(GameObject minion_go, Func<float> updatePercentFull, bool bVisible)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.suitFuelBar == null)
		{
			return;
		}
		entry.suitFuelBar.SetUpdateFunc(updatePercentFull);
		entry.suitFuelBar.gameObject.SetActive(bVisible);
	}

	// Token: 0x060058E1 RID: 22753 RVA: 0x002037FC File Offset: 0x002019FC
	public void SetSuitBatteryDisplay(GameObject minion_go, Func<float> updatePercentFull, bool bVisible)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.suitBatteryBar == null)
		{
			return;
		}
		entry.suitBatteryBar.SetUpdateFunc(updatePercentFull);
		entry.suitBatteryBar.gameObject.SetActive(bVisible);
	}

	// Token: 0x060058E2 RID: 22754 RVA: 0x00203840 File Offset: 0x00201A40
	private NameDisplayScreen.Entry GetEntry(GameObject worldObject)
	{
		return this.entries.Find((NameDisplayScreen.Entry entry) => entry.world_go == worldObject);
	}

	// Token: 0x04003BF0 RID: 15344
	[SerializeField]
	private float HideDistance;

	// Token: 0x04003BF1 RID: 15345
	public static NameDisplayScreen Instance;

	// Token: 0x04003BF2 RID: 15346
	public GameObject nameAndBarsPrefab;

	// Token: 0x04003BF3 RID: 15347
	public GameObject barsPrefab;

	// Token: 0x04003BF4 RID: 15348
	public TextStyleSetting ToolTipStyle_Property;

	// Token: 0x04003BF5 RID: 15349
	[SerializeField]
	private Color selectedColor;

	// Token: 0x04003BF6 RID: 15350
	[SerializeField]
	private Color defaultColor;

	// Token: 0x04003BF7 RID: 15351
	public int fontsize_min = 14;

	// Token: 0x04003BF8 RID: 15352
	public int fontsize_max = 32;

	// Token: 0x04003BF9 RID: 15353
	public float cameraDistance_fontsize_min = 6f;

	// Token: 0x04003BFA RID: 15354
	public float cameraDistance_fontsize_max = 4f;

	// Token: 0x04003BFB RID: 15355
	public List<NameDisplayScreen.Entry> entries = new List<NameDisplayScreen.Entry>();

	// Token: 0x04003BFC RID: 15356
	public List<NameDisplayScreen.TextEntry> textEntries = new List<NameDisplayScreen.TextEntry>();

	// Token: 0x04003BFD RID: 15357
	public bool worldSpace = true;

	// Token: 0x04003BFE RID: 15358
	private int updateSectionIndex;

	// Token: 0x04003BFF RID: 15359
	private List<System.Action> lateUpdateSections = new List<System.Action>();

	// Token: 0x04003C00 RID: 15360
	private bool isOverlayChangeBound;

	// Token: 0x04003C01 RID: 15361
	private HashedString lastKnownOverlayID = OverlayModes.None.ID;

	// Token: 0x04003C02 RID: 15362
	private List<KCollider2D> workingList = new List<KCollider2D>();

	// Token: 0x020019D5 RID: 6613
	[Serializable]
	public class Entry
	{
		// Token: 0x04007574 RID: 30068
		public string Name;

		// Token: 0x04007575 RID: 30069
		public GameObject world_go;

		// Token: 0x04007576 RID: 30070
		public GameObject display_go;

		// Token: 0x04007577 RID: 30071
		public GameObject bars_go;

		// Token: 0x04007578 RID: 30072
		public KAnimControllerBase world_go_anim_controller;

		// Token: 0x04007579 RID: 30073
		public RectTransform display_go_rect;

		// Token: 0x0400757A RID: 30074
		public HealthBar healthBar;

		// Token: 0x0400757B RID: 30075
		public ProgressBar breathBar;

		// Token: 0x0400757C RID: 30076
		public ProgressBar suitBar;

		// Token: 0x0400757D RID: 30077
		public ProgressBar suitFuelBar;

		// Token: 0x0400757E RID: 30078
		public ProgressBar suitBatteryBar;

		// Token: 0x0400757F RID: 30079
		public DreamBubble dreamBubble;

		// Token: 0x04007580 RID: 30080
		public HierarchyReferences thoughtBubble;

		// Token: 0x04007581 RID: 30081
		public HierarchyReferences thoughtBubbleConvo;

		// Token: 0x04007582 RID: 30082
		public HierarchyReferences gameplayEventDisplay;

		// Token: 0x04007583 RID: 30083
		public HierarchyReferences refs;
	}

	// Token: 0x020019D6 RID: 6614
	public class TextEntry
	{
		// Token: 0x04007584 RID: 30084
		public Guid guid;

		// Token: 0x04007585 RID: 30085
		public GameObject display_go;
	}
}
