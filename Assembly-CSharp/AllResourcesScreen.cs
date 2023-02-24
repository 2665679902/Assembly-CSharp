using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A30 RID: 2608
public class AllResourcesScreen : KScreen, ISim4000ms, ISim1000ms
{
	// Token: 0x06004F38 RID: 20280 RVA: 0x001C3615 File Offset: 0x001C1815
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		AllResourcesScreen.Instance = this;
	}

	// Token: 0x06004F39 RID: 20281 RVA: 0x001C3623 File Offset: 0x001C1823
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.ConsumeMouseScroll = true;
		this.Init();
	}

	// Token: 0x06004F3A RID: 20282 RVA: 0x001C3638 File Offset: 0x001C1838
	protected override void OnForcedCleanUp()
	{
		AllResourcesScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06004F3B RID: 20283 RVA: 0x001C3648 File Offset: 0x001C1848
	public void Init()
	{
		if (this.initialized)
		{
			return;
		}
		this.initialized = true;
		this.Populate(null);
		Game.Instance.Subscribe(1983128072, new Action<object>(this.Populate));
		DiscoveredResources.Instance.OnDiscover += delegate(Tag a, Tag b)
		{
			this.Populate(null);
		};
		this.closeButton.onClick += delegate
		{
			this.Show(false);
		};
		this.clearSearchButton.onClick += delegate
		{
			this.searchInputField.text = "";
		};
		this.searchInputField.onValueChanged.AddListener(delegate(string value)
		{
			this.SearchFilter(value);
		});
		KInputTextField kinputTextField = this.searchInputField;
		kinputTextField.onFocus = (System.Action)Delegate.Combine(kinputTextField.onFocus, new System.Action(delegate
		{
			base.isEditing = true;
		}));
		this.searchInputField.onEndEdit.AddListener(delegate(string value)
		{
			base.isEditing = false;
		});
		this.Show(false);
	}

	// Token: 0x06004F3C RID: 20284 RVA: 0x001C3732 File Offset: 0x001C1932
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		this.currentlyShown = show;
		if (show)
		{
			ManagementMenu.Instance.CloseAll();
			AllDiagnosticsScreen.Instance.Show(false);
			this.RefreshRows();
		}
	}

	// Token: 0x06004F3D RID: 20285 RVA: 0x001C3760 File Offset: 0x001C1960
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape))
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
			this.Show(false);
			e.Consumed = true;
		}
		if (base.isEditing)
		{
			e.Consumed = true;
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06004F3E RID: 20286 RVA: 0x001C37A0 File Offset: 0x001C19A0
	public override void OnKeyUp(KButtonEvent e)
	{
		if (PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
			this.Show(false);
			e.Consumed = true;
		}
		if (!e.Consumed)
		{
			base.OnKeyUp(e);
		}
	}

	// Token: 0x06004F3F RID: 20287 RVA: 0x001C37DD File Offset: 0x001C19DD
	public override float GetSortKey()
	{
		return 50f;
	}

	// Token: 0x06004F40 RID: 20288 RVA: 0x001C37E4 File Offset: 0x001C19E4
	public void Populate(object data = null)
	{
		this.SpawnRows();
	}

	// Token: 0x06004F41 RID: 20289 RVA: 0x001C37EC File Offset: 0x001C19EC
	private void SpawnRows()
	{
		WorldInventory worldInventory = ClusterManager.Instance.GetWorld(ClusterManager.Instance.activeWorldId).worldInventory;
		this.allowDisplayCategories.Add(GameTags.MaterialCategories);
		this.allowDisplayCategories.Add(GameTags.CalorieCategories);
		this.allowDisplayCategories.Add(GameTags.UnitCategories);
		foreach (Tag tag in GameTags.MaterialCategories)
		{
			this.SpawnCategoryRow(tag, GameUtil.MeasureUnit.mass);
		}
		foreach (Tag tag2 in GameTags.CalorieCategories)
		{
			this.SpawnCategoryRow(tag2, GameUtil.MeasureUnit.kcal);
		}
		foreach (Tag tag3 in GameTags.UnitCategories)
		{
			this.SpawnCategoryRow(tag3, GameUtil.MeasureUnit.quantity);
		}
		List<Tag> list = new List<Tag>();
		foreach (KeyValuePair<Tag, AllResourcesScreen.CategoryRow> keyValuePair in this.categoryRows)
		{
			list.Add(keyValuePair.Key);
		}
		list.Sort((Tag a, Tag b) => a.ProperNameStripLink().CompareTo(b.ProperNameStripLink()));
		foreach (Tag tag4 in list)
		{
			this.categoryRows[tag4].GameObject.transform.SetAsLastSibling();
		}
	}

	// Token: 0x06004F42 RID: 20290 RVA: 0x001C39C8 File Offset: 0x001C1BC8
	private void SpawnCategoryRow(Tag categoryTag, GameUtil.MeasureUnit unit)
	{
		if (!this.categoryRows.ContainsKey(categoryTag))
		{
			GameObject gameObject = Util.KInstantiateUI(this.categoryLinePrefab, this.rootListContainer, true);
			AllResourcesScreen.CategoryRow categoryRow = new AllResourcesScreen.CategoryRow(categoryTag, gameObject);
			gameObject.GetComponent<HierarchyReferences>().GetReference<LocText>("NameLabel").SetText(categoryTag.ProperNameStripLink());
			this.categoryRows.Add(categoryTag, categoryRow);
			this.currentlyDisplayedRows.Add(categoryTag, true);
			this.units.Add(categoryTag, unit);
			GraphBase component = categoryRow.sparkLayer.GetComponent<GraphBase>();
			component.axis_x.min_value = 0f;
			component.axis_x.max_value = 600f;
			component.axis_x.guide_frequency = 120f;
			component.RefreshGuides();
		}
		GameObject container = this.categoryRows[categoryTag].FoldOutPanel.container;
		foreach (Tag tag in DiscoveredResources.Instance.GetDiscoveredResourcesFromTag(categoryTag))
		{
			if (!this.resourceRows.ContainsKey(tag))
			{
				GameObject gameObject2 = Util.KInstantiateUI(this.resourceLinePrefab, container, true);
				HierarchyReferences component2 = gameObject2.GetComponent<HierarchyReferences>();
				global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(tag, "ui", false);
				component2.GetReference<Image>("Icon").sprite = uisprite.first;
				component2.GetReference<Image>("Icon").color = uisprite.second;
				component2.GetReference<LocText>("NameLabel").SetText(tag.ProperNameStripLink());
				Tag targetTag = tag;
				MultiToggle pinToggle = component2.GetReference<MultiToggle>("PinToggle");
				MultiToggle pinToggle2 = pinToggle;
				pinToggle2.onClick = (System.Action)Delegate.Combine(pinToggle2.onClick, new System.Action(delegate
				{
					if (ClusterManager.Instance.activeWorld.worldInventory.pinnedResources.Contains(targetTag))
					{
						ClusterManager.Instance.activeWorld.worldInventory.pinnedResources.Remove(targetTag);
					}
					else
					{
						ClusterManager.Instance.activeWorld.worldInventory.pinnedResources.Add(targetTag);
						if (DiscoveredResources.Instance.newDiscoveries.ContainsKey(targetTag))
						{
							DiscoveredResources.Instance.newDiscoveries.Remove(targetTag);
						}
					}
					this.RefreshPinnedState(targetTag);
					pinToggle.ChangeState(ClusterManager.Instance.activeWorld.worldInventory.pinnedResources.Contains(targetTag) ? 1 : 0);
				}));
				gameObject2.GetComponent<MultiToggle>().onClick = pinToggle.onClick;
				MultiToggle notifyToggle = component2.GetReference<MultiToggle>("NotificationToggle");
				MultiToggle notifyToggle2 = notifyToggle;
				notifyToggle2.onClick = (System.Action)Delegate.Combine(notifyToggle2.onClick, new System.Action(delegate
				{
					if (ClusterManager.Instance.activeWorld.worldInventory.notifyResources.Contains(targetTag))
					{
						ClusterManager.Instance.activeWorld.worldInventory.notifyResources.Remove(targetTag);
					}
					else
					{
						ClusterManager.Instance.activeWorld.worldInventory.notifyResources.Add(targetTag);
					}
					this.RefreshPinnedState(targetTag);
					notifyToggle.ChangeState(ClusterManager.Instance.activeWorld.worldInventory.notifyResources.Contains(targetTag) ? 1 : 0);
				}));
				component2.GetReference<SparkLayer>("Chart").GetComponent<GraphBase>().axis_x.min_value = 0f;
				component2.GetReference<SparkLayer>("Chart").GetComponent<GraphBase>().axis_x.max_value = 600f;
				component2.GetReference<SparkLayer>("Chart").GetComponent<GraphBase>().axis_x.guide_frequency = 120f;
				component2.GetReference<SparkLayer>("Chart").GetComponent<GraphBase>().RefreshGuides();
				AllResourcesScreen.ResourceRow resourceRow = new AllResourcesScreen.ResourceRow(tag, gameObject2);
				this.resourceRows.Add(tag, resourceRow);
				this.currentlyDisplayedRows.Add(tag, true);
				if (this.units.ContainsKey(tag))
				{
					global::Debug.LogError(string.Concat(new string[]
					{
						"Trying to add ",
						tag.ToString(),
						":UnitType ",
						this.units[tag].ToString(),
						" but units dictionary already has key ",
						tag.ToString(),
						" with unit type:",
						unit.ToString()
					}));
				}
				else
				{
					this.units.Add(tag, unit);
				}
			}
		}
	}

	// Token: 0x06004F43 RID: 20291 RVA: 0x001C3D60 File Offset: 0x001C1F60
	private void FilterRowBySearch(Tag tag, string filter)
	{
		this.currentlyDisplayedRows[tag] = this.PassesSearchFilter(tag, filter);
	}

	// Token: 0x06004F44 RID: 20292 RVA: 0x001C3D78 File Offset: 0x001C1F78
	private void SearchFilter(string search)
	{
		foreach (KeyValuePair<Tag, AllResourcesScreen.ResourceRow> keyValuePair in this.resourceRows)
		{
			this.FilterRowBySearch(keyValuePair.Key, search);
		}
		foreach (KeyValuePair<Tag, AllResourcesScreen.CategoryRow> keyValuePair2 in this.categoryRows)
		{
			if (this.PassesSearchFilter(keyValuePair2.Key, search))
			{
				this.currentlyDisplayedRows[keyValuePair2.Key] = true;
				using (HashSet<Tag>.Enumerator enumerator3 = DiscoveredResources.Instance.GetDiscoveredResourcesFromTag(keyValuePair2.Key).GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						Tag tag = enumerator3.Current;
						if (this.currentlyDisplayedRows.ContainsKey(tag))
						{
							this.currentlyDisplayedRows[tag] = true;
						}
					}
					continue;
				}
			}
			this.currentlyDisplayedRows[keyValuePair2.Key] = false;
		}
		this.EnableCategoriesByActiveChildren();
		this.SetRowsActive();
	}

	// Token: 0x06004F45 RID: 20293 RVA: 0x001C3EBC File Offset: 0x001C20BC
	private bool PassesSearchFilter(Tag tag, string filter)
	{
		filter = filter.ToUpper();
		string text = tag.ProperName().ToUpper();
		return !(filter != "") || text.Contains(filter) || tag.Name.ToUpper().Contains(filter);
	}

	// Token: 0x06004F46 RID: 20294 RVA: 0x001C3F0C File Offset: 0x001C210C
	private void EnableCategoriesByActiveChildren()
	{
		foreach (KeyValuePair<Tag, AllResourcesScreen.CategoryRow> keyValuePair in this.categoryRows)
		{
			if (DiscoveredResources.Instance.GetDiscoveredResourcesFromTag(keyValuePair.Key).Count == 0)
			{
				this.currentlyDisplayedRows[keyValuePair.Key] = false;
			}
			else
			{
				GameObject container = keyValuePair.Value.GameObject.GetComponent<FoldOutPanel>().container;
				foreach (KeyValuePair<Tag, AllResourcesScreen.ResourceRow> keyValuePair2 in this.resourceRows)
				{
					if (!(keyValuePair2.Value.GameObject.transform.parent.gameObject != container))
					{
						this.currentlyDisplayedRows[keyValuePair.Key] = this.currentlyDisplayedRows[keyValuePair.Key] || this.currentlyDisplayedRows[keyValuePair2.Key];
					}
				}
			}
		}
	}

	// Token: 0x06004F47 RID: 20295 RVA: 0x001C4040 File Offset: 0x001C2240
	private void RefreshPinnedState(Tag tag)
	{
		this.resourceRows[tag].notificiationToggle.ChangeState(ClusterManager.Instance.activeWorld.worldInventory.notifyResources.Contains(tag) ? 1 : 0);
		this.resourceRows[tag].pinToggle.ChangeState(ClusterManager.Instance.activeWorld.worldInventory.pinnedResources.Contains(tag) ? 1 : 0);
	}

	// Token: 0x06004F48 RID: 20296 RVA: 0x001C40BC File Offset: 0x001C22BC
	public void RefreshRows()
	{
		WorldInventory worldInventory = ClusterManager.Instance.GetWorld(ClusterManager.Instance.activeWorldId).worldInventory;
		this.EnableCategoriesByActiveChildren();
		this.SetRowsActive();
		if (this.allowRefresh)
		{
			foreach (KeyValuePair<Tag, AllResourcesScreen.CategoryRow> keyValuePair in this.categoryRows)
			{
				if (keyValuePair.Value.GameObject.activeInHierarchy)
				{
					float amount = worldInventory.GetAmount(keyValuePair.Key, false);
					float totalAmount = worldInventory.GetTotalAmount(keyValuePair.Key, false);
					if (!worldInventory.HasValidCount)
					{
						keyValuePair.Value.availableLabel.SetText(UI.ALLRESOURCESSCREEN.FIRST_FRAME_NO_DATA);
						keyValuePair.Value.totalLabel.SetText(UI.ALLRESOURCESSCREEN.FIRST_FRAME_NO_DATA);
						keyValuePair.Value.reservedLabel.SetText(UI.ALLRESOURCESSCREEN.FIRST_FRAME_NO_DATA);
					}
					else
					{
						switch (this.units[keyValuePair.Key])
						{
						case GameUtil.MeasureUnit.mass:
							if (keyValuePair.Value.CheckAvailableAmountChanged(amount, true))
							{
								keyValuePair.Value.availableLabel.SetText(GameUtil.GetFormattedMass(amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
							}
							if (keyValuePair.Value.CheckTotalResourceAmountChanged(totalAmount, true))
							{
								keyValuePair.Value.totalLabel.SetText(GameUtil.GetFormattedMass(totalAmount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
							}
							if (keyValuePair.Value.CheckReservedResourceAmountChanged(totalAmount - amount, true))
							{
								keyValuePair.Value.reservedLabel.SetText(GameUtil.GetFormattedMass(totalAmount - amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
							}
							break;
						case GameUtil.MeasureUnit.kcal:
						{
							float num = RationTracker.Get().CountRations(null, ClusterManager.Instance.activeWorld.worldInventory, true);
							if (keyValuePair.Value.CheckAvailableAmountChanged(amount, true))
							{
								keyValuePair.Value.availableLabel.SetText(GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true));
							}
							if (keyValuePair.Value.CheckTotalResourceAmountChanged(totalAmount, true))
							{
								keyValuePair.Value.totalLabel.SetText(GameUtil.GetFormattedCalories(totalAmount, GameUtil.TimeSlice.None, true));
							}
							if (keyValuePair.Value.CheckReservedResourceAmountChanged(totalAmount - amount, true))
							{
								keyValuePair.Value.reservedLabel.SetText(GameUtil.GetFormattedCalories(totalAmount - amount, GameUtil.TimeSlice.None, true));
							}
							break;
						}
						case GameUtil.MeasureUnit.quantity:
							if (keyValuePair.Value.CheckAvailableAmountChanged(amount, true))
							{
								keyValuePair.Value.availableLabel.SetText(GameUtil.GetFormattedUnits(amount, GameUtil.TimeSlice.None, true, ""));
							}
							if (keyValuePair.Value.CheckTotalResourceAmountChanged(totalAmount, true))
							{
								keyValuePair.Value.totalLabel.SetText(GameUtil.GetFormattedUnits(totalAmount, GameUtil.TimeSlice.None, true, ""));
							}
							if (keyValuePair.Value.CheckReservedResourceAmountChanged(totalAmount - amount, true))
							{
								keyValuePair.Value.reservedLabel.SetText(GameUtil.GetFormattedUnits(totalAmount - amount, GameUtil.TimeSlice.None, true, ""));
							}
							break;
						}
					}
				}
			}
			foreach (KeyValuePair<Tag, AllResourcesScreen.ResourceRow> keyValuePair2 in this.resourceRows)
			{
				if (keyValuePair2.Value.GameObject.activeInHierarchy)
				{
					float amount2 = worldInventory.GetAmount(keyValuePair2.Key, false);
					float totalAmount2 = worldInventory.GetTotalAmount(keyValuePair2.Key, false);
					if (!worldInventory.HasValidCount)
					{
						keyValuePair2.Value.availableLabel.SetText(UI.ALLRESOURCESSCREEN.FIRST_FRAME_NO_DATA);
						keyValuePair2.Value.totalLabel.SetText(UI.ALLRESOURCESSCREEN.FIRST_FRAME_NO_DATA);
						keyValuePair2.Value.reservedLabel.SetText(UI.ALLRESOURCESSCREEN.FIRST_FRAME_NO_DATA);
					}
					else
					{
						switch (this.units[keyValuePair2.Key])
						{
						case GameUtil.MeasureUnit.mass:
							if (keyValuePair2.Value.CheckAvailableAmountChanged(amount2, true))
							{
								keyValuePair2.Value.availableLabel.SetText(GameUtil.GetFormattedMass(amount2, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
							}
							if (keyValuePair2.Value.CheckTotalResourceAmountChanged(totalAmount2, true))
							{
								keyValuePair2.Value.totalLabel.SetText(GameUtil.GetFormattedMass(totalAmount2, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
							}
							if (keyValuePair2.Value.CheckReservedResourceAmountChanged(totalAmount2 - amount2, true))
							{
								keyValuePair2.Value.reservedLabel.SetText(GameUtil.GetFormattedMass(totalAmount2 - amount2, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
							}
							break;
						case GameUtil.MeasureUnit.kcal:
						{
							float num2 = RationTracker.Get().CountRationsByFoodType(keyValuePair2.Key.Name, ClusterManager.Instance.activeWorld.worldInventory, true);
							if (keyValuePair2.Value.CheckAvailableAmountChanged(num2, true))
							{
								keyValuePair2.Value.availableLabel.SetText(GameUtil.GetFormattedCalories(num2, GameUtil.TimeSlice.None, true));
							}
							if (keyValuePair2.Value.CheckTotalResourceAmountChanged(totalAmount2, true))
							{
								keyValuePair2.Value.totalLabel.SetText(GameUtil.GetFormattedCalories(totalAmount2, GameUtil.TimeSlice.None, true));
							}
							if (keyValuePair2.Value.CheckReservedResourceAmountChanged(totalAmount2 - amount2, true))
							{
								keyValuePair2.Value.reservedLabel.SetText(GameUtil.GetFormattedCalories(totalAmount2 - amount2, GameUtil.TimeSlice.None, true));
							}
							break;
						}
						case GameUtil.MeasureUnit.quantity:
							if (keyValuePair2.Value.CheckAvailableAmountChanged(amount2, true))
							{
								keyValuePair2.Value.availableLabel.SetText(GameUtil.GetFormattedUnits(amount2, GameUtil.TimeSlice.None, true, ""));
							}
							if (keyValuePair2.Value.CheckTotalResourceAmountChanged(totalAmount2, true))
							{
								keyValuePair2.Value.totalLabel.SetText(GameUtil.GetFormattedUnits(totalAmount2, GameUtil.TimeSlice.None, true, ""));
							}
							if (keyValuePair2.Value.CheckReservedResourceAmountChanged(totalAmount2 - amount2, true))
							{
								keyValuePair2.Value.reservedLabel.SetText(GameUtil.GetFormattedUnits(totalAmount2 - amount2, GameUtil.TimeSlice.None, true, ""));
							}
							break;
						}
					}
					this.RefreshPinnedState(keyValuePair2.Key);
				}
			}
		}
	}

	// Token: 0x06004F49 RID: 20297 RVA: 0x001C46F0 File Offset: 0x001C28F0
	public int UniqueResourceRowCount()
	{
		return this.resourceRows.Count;
	}

	// Token: 0x06004F4A RID: 20298 RVA: 0x001C4700 File Offset: 0x001C2900
	private void RefreshCharts()
	{
		float time = GameClock.Instance.GetTime();
		float num = 3000f;
		foreach (KeyValuePair<Tag, AllResourcesScreen.CategoryRow> keyValuePair in this.categoryRows)
		{
			ResourceTracker resourceStatistic = TrackerTool.Instance.GetResourceStatistic(ClusterManager.Instance.activeWorldId, keyValuePair.Key);
			if (resourceStatistic != null)
			{
				SparkLayer sparkLayer = keyValuePair.Value.sparkLayer;
				global::Tuple<float, float>[] array = resourceStatistic.ChartableData(num);
				if (array.Length != 0)
				{
					sparkLayer.graph.axis_x.max_value = array[array.Length - 1].first;
				}
				else
				{
					sparkLayer.graph.axis_x.max_value = 0f;
				}
				sparkLayer.graph.axis_x.min_value = time - num;
				sparkLayer.RefreshLine(array, "resourceAmount");
			}
			else
			{
				DebugUtil.DevLogError("DevError: No tracker found for resource category " + keyValuePair.Key.ToString());
			}
		}
		foreach (KeyValuePair<Tag, AllResourcesScreen.ResourceRow> keyValuePair2 in this.resourceRows)
		{
			if (keyValuePair2.Value.GameObject.activeInHierarchy)
			{
				ResourceTracker resourceStatistic2 = TrackerTool.Instance.GetResourceStatistic(ClusterManager.Instance.activeWorldId, keyValuePair2.Key);
				if (resourceStatistic2 != null)
				{
					SparkLayer sparkLayer2 = keyValuePair2.Value.sparkLayer;
					global::Tuple<float, float>[] array2 = resourceStatistic2.ChartableData(num);
					if (array2.Length != 0)
					{
						sparkLayer2.graph.axis_x.max_value = array2[array2.Length - 1].first;
					}
					else
					{
						sparkLayer2.graph.axis_x.max_value = 0f;
					}
					sparkLayer2.graph.axis_x.min_value = time - num;
					sparkLayer2.RefreshLine(array2, "resourceAmount");
				}
				else
				{
					DebugUtil.DevLogError("DevError: No tracker found for resource " + keyValuePair2.Key.ToString());
				}
			}
		}
	}

	// Token: 0x06004F4B RID: 20299 RVA: 0x001C4938 File Offset: 0x001C2B38
	private void SetRowsActive()
	{
		foreach (KeyValuePair<Tag, AllResourcesScreen.CategoryRow> keyValuePair in this.categoryRows)
		{
			if (keyValuePair.Value.GameObject.activeSelf != this.currentlyDisplayedRows[keyValuePair.Key])
			{
				keyValuePair.Value.GameObject.SetActive(this.currentlyDisplayedRows[keyValuePair.Key]);
			}
		}
		foreach (KeyValuePair<Tag, AllResourcesScreen.ResourceRow> keyValuePair2 in this.resourceRows)
		{
			if (keyValuePair2.Value.GameObject.activeSelf != this.currentlyDisplayedRows[keyValuePair2.Key])
			{
				keyValuePair2.Value.GameObject.SetActive(this.currentlyDisplayedRows[keyValuePair2.Key]);
			}
		}
	}

	// Token: 0x06004F4C RID: 20300 RVA: 0x001C4A50 File Offset: 0x001C2C50
	public void Sim4000ms(float dt)
	{
		if (!this.currentlyShown)
		{
			return;
		}
		this.RefreshCharts();
	}

	// Token: 0x06004F4D RID: 20301 RVA: 0x001C4A61 File Offset: 0x001C2C61
	public void Sim1000ms(float dt)
	{
		if (!this.currentlyShown)
		{
			return;
		}
		this.RefreshRows();
	}

	// Token: 0x04003536 RID: 13622
	private Dictionary<Tag, AllResourcesScreen.ResourceRow> resourceRows = new Dictionary<Tag, AllResourcesScreen.ResourceRow>();

	// Token: 0x04003537 RID: 13623
	private Dictionary<Tag, AllResourcesScreen.CategoryRow> categoryRows = new Dictionary<Tag, AllResourcesScreen.CategoryRow>();

	// Token: 0x04003538 RID: 13624
	public Dictionary<Tag, GameUtil.MeasureUnit> units = new Dictionary<Tag, GameUtil.MeasureUnit>();

	// Token: 0x04003539 RID: 13625
	public GameObject rootListContainer;

	// Token: 0x0400353A RID: 13626
	public GameObject resourceLinePrefab;

	// Token: 0x0400353B RID: 13627
	public GameObject categoryLinePrefab;

	// Token: 0x0400353C RID: 13628
	public KButton closeButton;

	// Token: 0x0400353D RID: 13629
	public bool allowRefresh = true;

	// Token: 0x0400353E RID: 13630
	private bool currentlyShown;

	// Token: 0x0400353F RID: 13631
	[SerializeField]
	private KInputTextField searchInputField;

	// Token: 0x04003540 RID: 13632
	[SerializeField]
	private KButton clearSearchButton;

	// Token: 0x04003541 RID: 13633
	public static AllResourcesScreen Instance;

	// Token: 0x04003542 RID: 13634
	public Dictionary<Tag, bool> currentlyDisplayedRows = new Dictionary<Tag, bool>();

	// Token: 0x04003543 RID: 13635
	public List<TagSet> allowDisplayCategories = new List<TagSet>();

	// Token: 0x04003544 RID: 13636
	private bool initialized;

	// Token: 0x020018C4 RID: 6340
	private class ScreenRowBase
	{
		// Token: 0x06008E5E RID: 36446 RVA: 0x0030D1B4 File Offset: 0x0030B3B4
		public ScreenRowBase(Tag tag, GameObject gameObject)
		{
			this.Tag = tag;
			this.GameObject = gameObject;
			HierarchyReferences component = this.GameObject.GetComponent<HierarchyReferences>();
			this.availableLabel = component.GetReference<LocText>("AvailableLabel");
			this.totalLabel = component.GetReference<LocText>("TotalLabel");
			this.reservedLabel = component.GetReference<LocText>("ReservedLabel");
			this.sparkLayer = component.GetReference<SparkLayer>("Chart");
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06008E5F RID: 36447 RVA: 0x0030D246 File Offset: 0x0030B446
		// (set) Token: 0x06008E60 RID: 36448 RVA: 0x0030D24E File Offset: 0x0030B44E
		public Tag Tag { get; private set; }

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06008E61 RID: 36449 RVA: 0x0030D257 File Offset: 0x0030B457
		// (set) Token: 0x06008E62 RID: 36450 RVA: 0x0030D25F File Offset: 0x0030B45F
		public GameObject GameObject { get; private set; }

		// Token: 0x06008E63 RID: 36451 RVA: 0x0030D268 File Offset: 0x0030B468
		public bool CheckAvailableAmountChanged(float newAvailableResourceAmount, bool updateIfTrue)
		{
			bool flag = newAvailableResourceAmount != this.oldAvailableResourceAmount;
			if (flag && updateIfTrue)
			{
				this.oldAvailableResourceAmount = newAvailableResourceAmount;
			}
			return flag;
		}

		// Token: 0x06008E64 RID: 36452 RVA: 0x0030D282 File Offset: 0x0030B482
		public bool CheckTotalResourceAmountChanged(float newTotalResourceAmount, bool updateIfTrue)
		{
			bool flag = newTotalResourceAmount != this.oldTotalResourceAmount;
			if (flag && updateIfTrue)
			{
				this.oldTotalResourceAmount = newTotalResourceAmount;
			}
			return flag;
		}

		// Token: 0x06008E65 RID: 36453 RVA: 0x0030D29C File Offset: 0x0030B49C
		public bool CheckReservedResourceAmountChanged(float newReservedResourceAmount, bool updateIfTrue)
		{
			bool flag = newReservedResourceAmount != this.oldReserverResourceAmount;
			if (flag && updateIfTrue)
			{
				this.oldReserverResourceAmount = newReservedResourceAmount;
			}
			return flag;
		}

		// Token: 0x0400724D RID: 29261
		public LocText availableLabel;

		// Token: 0x0400724E RID: 29262
		public LocText totalLabel;

		// Token: 0x0400724F RID: 29263
		public LocText reservedLabel;

		// Token: 0x04007250 RID: 29264
		public SparkLayer sparkLayer;

		// Token: 0x04007251 RID: 29265
		private float oldAvailableResourceAmount = -1f;

		// Token: 0x04007252 RID: 29266
		private float oldTotalResourceAmount = -1f;

		// Token: 0x04007253 RID: 29267
		private float oldReserverResourceAmount = -1f;
	}

	// Token: 0x020018C5 RID: 6341
	private class CategoryRow : AllResourcesScreen.ScreenRowBase
	{
		// Token: 0x06008E66 RID: 36454 RVA: 0x0030D2B6 File Offset: 0x0030B4B6
		public CategoryRow(Tag tag, GameObject gameObject)
			: base(tag, gameObject)
		{
			this.FoldOutPanel = base.GameObject.GetComponent<FoldOutPanel>();
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06008E67 RID: 36455 RVA: 0x0030D2D1 File Offset: 0x0030B4D1
		// (set) Token: 0x06008E68 RID: 36456 RVA: 0x0030D2D9 File Offset: 0x0030B4D9
		public FoldOutPanel FoldOutPanel { get; private set; }
	}

	// Token: 0x020018C6 RID: 6342
	private class ResourceRow : AllResourcesScreen.ScreenRowBase
	{
		// Token: 0x06008E69 RID: 36457 RVA: 0x0030D2E4 File Offset: 0x0030B4E4
		public ResourceRow(Tag tag, GameObject gameObject)
			: base(tag, gameObject)
		{
			HierarchyReferences component = base.GameObject.GetComponent<HierarchyReferences>();
			this.notificiationToggle = component.GetReference<MultiToggle>("NotificationToggle");
			this.pinToggle = component.GetReference<MultiToggle>("PinToggle");
		}

		// Token: 0x04007255 RID: 29269
		public MultiToggle notificiationToggle;

		// Token: 0x04007256 RID: 29270
		public MultiToggle pinToggle;
	}
}
