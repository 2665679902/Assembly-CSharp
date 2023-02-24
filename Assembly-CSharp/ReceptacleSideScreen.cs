using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BD3 RID: 3027
public class ReceptacleSideScreen : SideScreenContent, IRender1000ms
{
	// Token: 0x06005F31 RID: 24369 RVA: 0x0022CFB4 File Offset: 0x0022B1B4
	public override string GetTitle()
	{
		if (this.targetReceptacle == null)
		{
			return Strings.Get(this.titleKey).ToString().Replace("{0}", "");
		}
		return string.Format(Strings.Get(this.titleKey), this.targetReceptacle.GetProperName());
	}

	// Token: 0x06005F32 RID: 24370 RVA: 0x0022D010 File Offset: 0x0022B210
	public void Initialize(SingleEntityReceptacle target)
	{
		if (target == null)
		{
			global::Debug.LogError("SingleObjectReceptacle provided was null.");
			return;
		}
		this.targetReceptacle = target;
		base.gameObject.SetActive(true);
		this.depositObjectMap = new Dictionary<ReceptacleToggle, ReceptacleSideScreen.SelectableEntity>();
		this.entityToggles.ForEach(delegate(ReceptacleToggle rbi)
		{
			UnityEngine.Object.Destroy(rbi.gameObject);
		});
		this.entityToggles.Clear();
		foreach (Tag tag in this.targetReceptacle.possibleDepositObjectTags)
		{
			List<GameObject> prefabsWithTag = Assets.GetPrefabsWithTag(tag);
			int num = prefabsWithTag.Count;
			List<IHasSortOrder> list = new List<IHasSortOrder>();
			foreach (GameObject gameObject in prefabsWithTag)
			{
				if (!this.targetReceptacle.IsValidEntity(gameObject))
				{
					num--;
				}
				else
				{
					IHasSortOrder component = gameObject.GetComponent<IHasSortOrder>();
					if (component != null)
					{
						list.Add(component);
					}
				}
			}
			global::Debug.Assert(list.Count == num, "Not all entities in this receptacle implement IHasSortOrder!");
			list.Sort((IHasSortOrder a, IHasSortOrder b) => a.sortOrder - b.sortOrder);
			foreach (IHasSortOrder hasSortOrder in list)
			{
				GameObject gameObject2 = (hasSortOrder as MonoBehaviour).gameObject;
				GameObject gameObject3 = Util.KInstantiateUI(this.entityToggle, this.requestObjectList, false);
				gameObject3.SetActive(true);
				ReceptacleToggle newToggle = gameObject3.GetComponent<ReceptacleToggle>();
				IReceptacleDirection component2 = gameObject2.GetComponent<IReceptacleDirection>();
				string properName = gameObject2.GetProperName();
				newToggle.title.text = properName;
				Sprite entityIcon = this.GetEntityIcon(gameObject2.PrefabID());
				if (entityIcon == null)
				{
					entityIcon = this.elementPlaceholderSpr;
				}
				newToggle.image.sprite = entityIcon;
				newToggle.toggle.onClick += delegate
				{
					this.ToggleClicked(newToggle);
				};
				newToggle.toggle.onPointerEnter += delegate
				{
					this.CheckAmountsAndUpdate(null);
				};
				this.depositObjectMap.Add(newToggle, new ReceptacleSideScreen.SelectableEntity
				{
					tag = gameObject2.PrefabID(),
					direction = ((component2 != null) ? component2.Direction : SingleEntityReceptacle.ReceptacleDirection.Top),
					asset = gameObject2
				});
				this.entityToggles.Add(newToggle);
			}
		}
		this.RestoreSelectionFromOccupant();
		this.selectedEntityToggle = null;
		if (this.entityToggles.Count > 0)
		{
			if (this.entityPreviousSelectionMap.ContainsKey(this.targetReceptacle))
			{
				int num2 = this.entityPreviousSelectionMap[this.targetReceptacle];
				this.ToggleClicked(this.entityToggles[num2]);
			}
			else
			{
				this.subtitleLabel.SetText(Strings.Get(this.subtitleStringSelect).ToString());
				this.requestSelectedEntityBtn.isInteractable = false;
				this.descriptionLabel.SetText(Strings.Get(this.subtitleStringSelectDescription).ToString());
				this.HideAllDescriptorPanels();
			}
		}
		this.onStorageChangedHandle = this.targetReceptacle.gameObject.Subscribe(-1697596308, new Action<object>(this.CheckAmountsAndUpdate));
		this.onOccupantValidChangedHandle = this.targetReceptacle.gameObject.Subscribe(-1820564715, new Action<object>(this.OnOccupantValidChanged));
		this.UpdateState(null);
		SimAndRenderScheduler.instance.Add(this, false);
	}

	// Token: 0x06005F33 RID: 24371 RVA: 0x0022D400 File Offset: 0x0022B600
	protected virtual void UpdateState(object data)
	{
		this.requestSelectedEntityBtn.ClearOnClick();
		if (this.targetReceptacle == null)
		{
			return;
		}
		if (this.CheckReceptacleOccupied())
		{
			Uprootable uprootable = this.targetReceptacle.Occupant.GetComponent<Uprootable>();
			if (uprootable != null && uprootable.IsMarkedForUproot)
			{
				this.requestSelectedEntityBtn.onClick += delegate
				{
					uprootable.ForceCancelUproot(null);
					this.UpdateState(null);
				};
				this.requestSelectedEntityBtn.GetComponentInChildren<LocText>().text = Strings.Get(this.requestStringCancelRemove).ToString();
				this.subtitleLabel.SetText(string.Format(Strings.Get(this.subtitleStringAwaitingRemoval).ToString(), this.targetReceptacle.Occupant.GetProperName()));
			}
			else
			{
				this.requestSelectedEntityBtn.onClick += delegate
				{
					this.targetReceptacle.OrderRemoveOccupant();
					this.UpdateState(null);
				};
				this.requestSelectedEntityBtn.GetComponentInChildren<LocText>().text = Strings.Get(this.requestStringRemove).ToString();
				this.subtitleLabel.SetText(string.Format(Strings.Get(this.subtitleStringEntityDeposited).ToString(), this.targetReceptacle.Occupant.GetProperName()));
			}
			this.requestSelectedEntityBtn.isInteractable = true;
			this.ToggleObjectPicker(false);
			Tag tag = this.targetReceptacle.Occupant.GetComponent<KSelectable>().PrefabID();
			this.ConfigureActiveEntity(tag);
			this.SetResultDescriptions(this.targetReceptacle.Occupant);
		}
		else if (this.targetReceptacle.GetActiveRequest != null)
		{
			this.requestSelectedEntityBtn.onClick += delegate
			{
				this.targetReceptacle.CancelActiveRequest();
				this.ClearSelection();
				this.UpdateAvailableAmounts(null);
				this.UpdateState(null);
			};
			this.requestSelectedEntityBtn.GetComponentInChildren<LocText>().text = Strings.Get(this.requestStringCancelDeposit).ToString();
			this.requestSelectedEntityBtn.isInteractable = true;
			this.ToggleObjectPicker(false);
			this.ConfigureActiveEntity(this.targetReceptacle.GetActiveRequest.tagsFirst);
			GameObject prefab = Assets.GetPrefab(this.targetReceptacle.GetActiveRequest.tagsFirst);
			if (prefab != null)
			{
				this.subtitleLabel.SetText(string.Format(Strings.Get(this.subtitleStringAwaitingDelivery).ToString(), prefab.GetProperName()));
				this.SetResultDescriptions(prefab);
			}
		}
		else if (this.selectedEntityToggle != null)
		{
			this.requestSelectedEntityBtn.onClick += delegate
			{
				this.targetReceptacle.CreateOrder(this.selectedDepositObjectTag, this.selectedDepositObjectAdditionalTag);
				this.UpdateAvailableAmounts(null);
				this.UpdateState(null);
			};
			this.requestSelectedEntityBtn.GetComponentInChildren<LocText>().text = Strings.Get(this.requestStringDeposit).ToString();
			this.targetReceptacle.SetPreview(this.depositObjectMap[this.selectedEntityToggle].tag, false);
			bool flag = this.CanDepositEntity(this.depositObjectMap[this.selectedEntityToggle]);
			this.requestSelectedEntityBtn.isInteractable = flag;
			this.SetImageToggleState(this.selectedEntityToggle.toggle, flag ? ImageToggleState.State.Active : ImageToggleState.State.DisabledActive);
			this.ToggleObjectPicker(true);
			GameObject prefab2 = Assets.GetPrefab(this.selectedDepositObjectTag);
			if (prefab2 != null)
			{
				this.subtitleLabel.SetText(string.Format(Strings.Get(this.subtitleStringAwaitingSelection).ToString(), prefab2.GetProperName()));
				this.SetResultDescriptions(prefab2);
			}
		}
		else
		{
			this.requestSelectedEntityBtn.GetComponentInChildren<LocText>().text = Strings.Get(this.requestStringDeposit).ToString();
			this.requestSelectedEntityBtn.isInteractable = false;
			this.ToggleObjectPicker(true);
		}
		this.UpdateAvailableAmounts(null);
		this.UpdateListeners();
	}

	// Token: 0x06005F34 RID: 24372 RVA: 0x0022D780 File Offset: 0x0022B980
	private void UpdateListeners()
	{
		if (this.CheckReceptacleOccupied())
		{
			if (this.onObjectDestroyedHandle == -1)
			{
				this.onObjectDestroyedHandle = this.targetReceptacle.Occupant.gameObject.Subscribe(1969584890, delegate(object d)
				{
					this.UpdateState(null);
				});
				return;
			}
		}
		else if (this.onObjectDestroyedHandle != -1)
		{
			this.onObjectDestroyedHandle = -1;
		}
	}

	// Token: 0x06005F35 RID: 24373 RVA: 0x0022D7DC File Offset: 0x0022B9DC
	private void OnOccupantValidChanged(object obj)
	{
		if (this.targetReceptacle == null)
		{
			return;
		}
		if (!this.CheckReceptacleOccupied() && this.targetReceptacle.GetActiveRequest != null)
		{
			bool flag = false;
			ReceptacleSideScreen.SelectableEntity selectableEntity;
			if (this.depositObjectMap.TryGetValue(this.selectedEntityToggle, out selectableEntity))
			{
				flag = this.CanDepositEntity(selectableEntity);
			}
			if (!flag)
			{
				this.targetReceptacle.CancelActiveRequest();
				this.ClearSelection();
				this.UpdateState(null);
				this.UpdateAvailableAmounts(null);
			}
		}
	}

	// Token: 0x06005F36 RID: 24374 RVA: 0x0022D84F File Offset: 0x0022BA4F
	private bool CanDepositEntity(ReceptacleSideScreen.SelectableEntity entity)
	{
		return this.ValidRotationForDeposit(entity.direction) && (!this.RequiresAvailableAmountToDeposit() || this.GetAvailableAmount(entity.tag) > 0f) && this.AdditionalCanDepositTest();
	}

	// Token: 0x06005F37 RID: 24375 RVA: 0x0022D882 File Offset: 0x0022BA82
	protected virtual bool AdditionalCanDepositTest()
	{
		return true;
	}

	// Token: 0x06005F38 RID: 24376 RVA: 0x0022D885 File Offset: 0x0022BA85
	protected virtual bool RequiresAvailableAmountToDeposit()
	{
		return true;
	}

	// Token: 0x06005F39 RID: 24377 RVA: 0x0022D888 File Offset: 0x0022BA88
	private void ClearSelection()
	{
		foreach (KeyValuePair<ReceptacleToggle, ReceptacleSideScreen.SelectableEntity> keyValuePair in this.depositObjectMap)
		{
			keyValuePair.Key.toggle.Deselect();
		}
	}

	// Token: 0x06005F3A RID: 24378 RVA: 0x0022D8E8 File Offset: 0x0022BAE8
	private void ToggleObjectPicker(bool Show)
	{
		this.requestObjectListContainer.SetActive(Show);
		if (this.scrollBarContainer != null)
		{
			this.scrollBarContainer.SetActive(Show);
		}
		this.requestObjectList.SetActive(Show);
		this.activeEntityContainer.SetActive(!Show);
	}

	// Token: 0x06005F3B RID: 24379 RVA: 0x0022D938 File Offset: 0x0022BB38
	private void ConfigureActiveEntity(Tag tag)
	{
		string properName = Assets.GetPrefab(tag).GetProperName();
		this.activeEntityContainer.GetComponentInChildrenOnly<LocText>().text = properName;
		this.activeEntityContainer.transform.GetChild(0).gameObject.GetComponentInChildrenOnly<Image>().sprite = this.GetEntityIcon(tag);
	}

	// Token: 0x06005F3C RID: 24380 RVA: 0x0022D989 File Offset: 0x0022BB89
	protected virtual Sprite GetEntityIcon(Tag prefabTag)
	{
		return Def.GetUISprite(Assets.GetPrefab(prefabTag), "ui", false).first;
	}

	// Token: 0x06005F3D RID: 24381 RVA: 0x0022D9A4 File Offset: 0x0022BBA4
	public override bool IsValidForTarget(GameObject target)
	{
		SingleEntityReceptacle component = target.GetComponent<SingleEntityReceptacle>();
		return component != null && component.enabled && target.GetComponent<PlantablePlot>() == null && target.GetComponent<EggIncubator>() == null;
	}

	// Token: 0x06005F3E RID: 24382 RVA: 0x0022D9E8 File Offset: 0x0022BBE8
	public override void SetTarget(GameObject target)
	{
		SingleEntityReceptacle component = target.GetComponent<SingleEntityReceptacle>();
		if (component == null)
		{
			global::Debug.LogError("The object selected doesn't have a SingleObjectReceptacle!");
			return;
		}
		this.Initialize(component);
		this.UpdateState(null);
	}

	// Token: 0x06005F3F RID: 24383 RVA: 0x0022DA1E File Offset: 0x0022BC1E
	protected virtual void RestoreSelectionFromOccupant()
	{
	}

	// Token: 0x06005F40 RID: 24384 RVA: 0x0022DA20 File Offset: 0x0022BC20
	public override void ClearTarget()
	{
		if (this.targetReceptacle != null)
		{
			if (this.CheckReceptacleOccupied())
			{
				this.targetReceptacle.Occupant.gameObject.Unsubscribe(this.onObjectDestroyedHandle);
				this.onObjectDestroyedHandle = -1;
			}
			this.targetReceptacle.Unsubscribe(this.onStorageChangedHandle);
			this.onStorageChangedHandle = -1;
			this.targetReceptacle.Unsubscribe(this.onOccupantValidChangedHandle);
			this.onOccupantValidChangedHandle = -1;
			if (this.targetReceptacle.GetActiveRequest == null)
			{
				this.targetReceptacle.SetPreview(Tag.Invalid, false);
			}
			SimAndRenderScheduler.instance.Remove(this);
			this.targetReceptacle = null;
		}
	}

	// Token: 0x06005F41 RID: 24385 RVA: 0x0022DAC8 File Offset: 0x0022BCC8
	protected void SetImageToggleState(KToggle toggle, ImageToggleState.State state)
	{
		switch (state)
		{
		case ImageToggleState.State.Disabled:
			toggle.GetComponent<ImageToggleState>().SetDisabled();
			toggle.gameObject.GetComponentInChildrenOnly<Image>().material = this.desaturatedMaterial;
			return;
		case ImageToggleState.State.Inactive:
			toggle.GetComponent<ImageToggleState>().SetInactive();
			toggle.gameObject.GetComponentInChildrenOnly<Image>().material = this.defaultMaterial;
			return;
		case ImageToggleState.State.Active:
			toggle.GetComponent<ImageToggleState>().SetActive();
			toggle.gameObject.GetComponentInChildrenOnly<Image>().material = this.defaultMaterial;
			return;
		case ImageToggleState.State.DisabledActive:
			toggle.GetComponent<ImageToggleState>().SetDisabledActive();
			toggle.gameObject.GetComponentInChildrenOnly<Image>().material = this.desaturatedMaterial;
			return;
		default:
			return;
		}
	}

	// Token: 0x06005F42 RID: 24386 RVA: 0x0022DB73 File Offset: 0x0022BD73
	public void Render1000ms(float dt)
	{
		this.CheckAmountsAndUpdate(null);
	}

	// Token: 0x06005F43 RID: 24387 RVA: 0x0022DB7C File Offset: 0x0022BD7C
	private void CheckAmountsAndUpdate(object data)
	{
		if (this.targetReceptacle == null)
		{
			return;
		}
		if (this.UpdateAvailableAmounts(null))
		{
			this.UpdateState(null);
		}
	}

	// Token: 0x06005F44 RID: 24388 RVA: 0x0022DBA0 File Offset: 0x0022BDA0
	private bool UpdateAvailableAmounts(object data)
	{
		bool flag = false;
		foreach (KeyValuePair<ReceptacleToggle, ReceptacleSideScreen.SelectableEntity> keyValuePair in this.depositObjectMap)
		{
			if (!DebugHandler.InstantBuildMode && this.hideUndiscoveredEntities && !DiscoveredResources.Instance.IsDiscovered(keyValuePair.Value.tag))
			{
				keyValuePair.Key.gameObject.SetActive(false);
			}
			else if (!keyValuePair.Key.gameObject.activeSelf)
			{
				keyValuePair.Key.gameObject.SetActive(true);
			}
			float availableAmount = this.GetAvailableAmount(keyValuePair.Value.tag);
			if (keyValuePair.Value.lastAmount != availableAmount)
			{
				flag = true;
				keyValuePair.Value.lastAmount = availableAmount;
				keyValuePair.Key.amount.text = availableAmount.ToString();
			}
			if (!this.ValidRotationForDeposit(keyValuePair.Value.direction) || availableAmount <= 0f)
			{
				if (this.selectedEntityToggle != keyValuePair.Key)
				{
					this.SetImageToggleState(keyValuePair.Key.toggle, ImageToggleState.State.Disabled);
				}
				else
				{
					this.SetImageToggleState(keyValuePair.Key.toggle, ImageToggleState.State.DisabledActive);
				}
			}
			else if (this.selectedEntityToggle != keyValuePair.Key)
			{
				this.SetImageToggleState(keyValuePair.Key.toggle, ImageToggleState.State.Inactive);
			}
			else
			{
				this.SetImageToggleState(keyValuePair.Key.toggle, ImageToggleState.State.Active);
			}
		}
		return flag;
	}

	// Token: 0x06005F45 RID: 24389 RVA: 0x0022DD40 File Offset: 0x0022BF40
	private float GetAvailableAmount(Tag tag)
	{
		return this.targetReceptacle.GetMyWorld().worldInventory.GetAmount(tag, true);
	}

	// Token: 0x06005F46 RID: 24390 RVA: 0x0022DD59 File Offset: 0x0022BF59
	private bool ValidRotationForDeposit(SingleEntityReceptacle.ReceptacleDirection depositDir)
	{
		return this.targetReceptacle.rotatable == null || depositDir == this.targetReceptacle.Direction;
	}

	// Token: 0x06005F47 RID: 24391 RVA: 0x0022DD80 File Offset: 0x0022BF80
	protected virtual void ToggleClicked(ReceptacleToggle toggle)
	{
		if (!this.depositObjectMap.ContainsKey(toggle))
		{
			global::Debug.LogError("Recipe not found on recipe list.");
			return;
		}
		if (this.selectedEntityToggle != null)
		{
			bool flag = this.CanDepositEntity(this.depositObjectMap[this.selectedEntityToggle]);
			this.requestSelectedEntityBtn.isInteractable = flag;
			this.SetImageToggleState(this.selectedEntityToggle.toggle, flag ? ImageToggleState.State.Inactive : ImageToggleState.State.Disabled);
		}
		this.selectedEntityToggle = toggle;
		this.entityPreviousSelectionMap[this.targetReceptacle] = this.entityToggles.IndexOf(toggle);
		this.selectedDepositObjectTag = this.depositObjectMap[toggle].tag;
		MutantPlant component = this.depositObjectMap[toggle].asset.GetComponent<MutantPlant>();
		this.selectedDepositObjectAdditionalTag = (component ? component.SubSpeciesID : Tag.Invalid);
		this.UpdateAvailableAmounts(null);
		this.UpdateState(null);
	}

	// Token: 0x06005F48 RID: 24392 RVA: 0x0022DE6C File Offset: 0x0022C06C
	private void CreateOrder(bool isInfinite)
	{
		this.targetReceptacle.CreateOrder(this.selectedDepositObjectTag, this.selectedDepositObjectAdditionalTag);
	}

	// Token: 0x06005F49 RID: 24393 RVA: 0x0022DE85 File Offset: 0x0022C085
	protected bool CheckReceptacleOccupied()
	{
		return this.targetReceptacle != null && this.targetReceptacle.Occupant != null;
	}

	// Token: 0x06005F4A RID: 24394 RVA: 0x0022DEAC File Offset: 0x0022C0AC
	protected virtual void SetResultDescriptions(GameObject go)
	{
		string text = "";
		InfoDescription component = go.GetComponent<InfoDescription>();
		if (component)
		{
			text = component.description;
		}
		else
		{
			KPrefabID component2 = go.GetComponent<KPrefabID>();
			if (component2 != null)
			{
				Element element = ElementLoader.GetElement(component2.PrefabID());
				if (element != null)
				{
					text = element.Description();
				}
			}
			else
			{
				text = go.GetProperName();
			}
		}
		this.descriptionLabel.SetText(text);
	}

	// Token: 0x06005F4B RID: 24395 RVA: 0x0022DF14 File Offset: 0x0022C114
	protected virtual void HideAllDescriptorPanels()
	{
		for (int i = 0; i < this.descriptorPanels.Count; i++)
		{
			this.descriptorPanels[i].gameObject.SetActive(false);
		}
	}

	// Token: 0x04004123 RID: 16675
	[SerializeField]
	protected KButton requestSelectedEntityBtn;

	// Token: 0x04004124 RID: 16676
	[SerializeField]
	private string requestStringDeposit;

	// Token: 0x04004125 RID: 16677
	[SerializeField]
	private string requestStringCancelDeposit;

	// Token: 0x04004126 RID: 16678
	[SerializeField]
	private string requestStringRemove;

	// Token: 0x04004127 RID: 16679
	[SerializeField]
	private string requestStringCancelRemove;

	// Token: 0x04004128 RID: 16680
	public GameObject activeEntityContainer;

	// Token: 0x04004129 RID: 16681
	public GameObject nothingDiscoveredContainer;

	// Token: 0x0400412A RID: 16682
	[SerializeField]
	protected LocText descriptionLabel;

	// Token: 0x0400412B RID: 16683
	protected Dictionary<SingleEntityReceptacle, int> entityPreviousSelectionMap = new Dictionary<SingleEntityReceptacle, int>();

	// Token: 0x0400412C RID: 16684
	[SerializeField]
	private string subtitleStringSelect;

	// Token: 0x0400412D RID: 16685
	[SerializeField]
	private string subtitleStringSelectDescription;

	// Token: 0x0400412E RID: 16686
	[SerializeField]
	private string subtitleStringAwaitingSelection;

	// Token: 0x0400412F RID: 16687
	[SerializeField]
	private string subtitleStringAwaitingDelivery;

	// Token: 0x04004130 RID: 16688
	[SerializeField]
	private string subtitleStringEntityDeposited;

	// Token: 0x04004131 RID: 16689
	[SerializeField]
	private string subtitleStringAwaitingRemoval;

	// Token: 0x04004132 RID: 16690
	[SerializeField]
	private LocText subtitleLabel;

	// Token: 0x04004133 RID: 16691
	[SerializeField]
	private List<DescriptorPanel> descriptorPanels;

	// Token: 0x04004134 RID: 16692
	public Material defaultMaterial;

	// Token: 0x04004135 RID: 16693
	public Material desaturatedMaterial;

	// Token: 0x04004136 RID: 16694
	[SerializeField]
	private GameObject requestObjectList;

	// Token: 0x04004137 RID: 16695
	[SerializeField]
	private GameObject requestObjectListContainer;

	// Token: 0x04004138 RID: 16696
	[SerializeField]
	private GameObject scrollBarContainer;

	// Token: 0x04004139 RID: 16697
	[SerializeField]
	private GameObject entityToggle;

	// Token: 0x0400413A RID: 16698
	[SerializeField]
	private Sprite buttonSelectedBG;

	// Token: 0x0400413B RID: 16699
	[SerializeField]
	private Sprite buttonNormalBG;

	// Token: 0x0400413C RID: 16700
	[SerializeField]
	private Sprite elementPlaceholderSpr;

	// Token: 0x0400413D RID: 16701
	[SerializeField]
	private bool hideUndiscoveredEntities;

	// Token: 0x0400413E RID: 16702
	protected ReceptacleToggle selectedEntityToggle;

	// Token: 0x0400413F RID: 16703
	protected SingleEntityReceptacle targetReceptacle;

	// Token: 0x04004140 RID: 16704
	protected Tag selectedDepositObjectTag;

	// Token: 0x04004141 RID: 16705
	protected Tag selectedDepositObjectAdditionalTag;

	// Token: 0x04004142 RID: 16706
	protected Dictionary<ReceptacleToggle, ReceptacleSideScreen.SelectableEntity> depositObjectMap;

	// Token: 0x04004143 RID: 16707
	protected List<ReceptacleToggle> entityToggles = new List<ReceptacleToggle>();

	// Token: 0x04004144 RID: 16708
	private int onObjectDestroyedHandle = -1;

	// Token: 0x04004145 RID: 16709
	private int onOccupantValidChangedHandle = -1;

	// Token: 0x04004146 RID: 16710
	private int onStorageChangedHandle = -1;

	// Token: 0x02001A7A RID: 6778
	protected class SelectableEntity
	{
		// Token: 0x040077A4 RID: 30628
		public Tag tag;

		// Token: 0x040077A5 RID: 30629
		public SingleEntityReceptacle.ReceptacleDirection direction;

		// Token: 0x040077A6 RID: 30630
		public GameObject asset;

		// Token: 0x040077A7 RID: 30631
		public float lastAmount = -1f;
	}
}
