using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B56 RID: 2902
public class PinnedResourcesPanel : KScreen, IRender1000ms
{
	// Token: 0x06005A3A RID: 23098 RVA: 0x0020A6DB File Offset: 0x002088DB
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.rowContainerLayout = this.rowContainer.GetComponent<QuickLayout>();
	}

	// Token: 0x06005A3B RID: 23099 RVA: 0x0020A6F4 File Offset: 0x002088F4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		PinnedResourcesPanel.Instance = this;
		this.Populate(null);
		Game.Instance.Subscribe(1983128072, new Action<object>(this.Populate));
		MultiToggle component = this.headerButton.GetComponent<MultiToggle>();
		component.onClick = (System.Action)Delegate.Combine(component.onClick, new System.Action(delegate
		{
			this.Refresh();
		}));
		MultiToggle component2 = this.seeAllButton.GetComponent<MultiToggle>();
		component2.onClick = (System.Action)Delegate.Combine(component2.onClick, new System.Action(delegate
		{
			AllResourcesScreen.Instance.Show(!AllResourcesScreen.Instance.gameObject.activeSelf);
		}));
		this.seeAllLabel = this.seeAllButton.GetComponentInChildren<LocText>();
		MultiToggle component3 = this.clearNewButton.GetComponent<MultiToggle>();
		component3.onClick = (System.Action)Delegate.Combine(component3.onClick, new System.Action(delegate
		{
			this.ClearAllNew();
		}));
		this.clearAllButton.onClick += delegate
		{
			this.ClearAllNew();
			this.UnPinAll();
			this.Refresh();
		};
		AllResourcesScreen.Instance.Init();
		this.Refresh();
	}

	// Token: 0x06005A3C RID: 23100 RVA: 0x0020A7FF File Offset: 0x002089FF
	protected override void OnForcedCleanUp()
	{
		PinnedResourcesPanel.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06005A3D RID: 23101 RVA: 0x0020A80D File Offset: 0x00208A0D
	public void ClearExcessiveNewItems()
	{
		if (DiscoveredResources.Instance.CheckAllDiscoveredAreNew())
		{
			DiscoveredResources.Instance.newDiscoveries.Clear();
		}
	}

	// Token: 0x06005A3E RID: 23102 RVA: 0x0020A82C File Offset: 0x00208A2C
	private void ClearAllNew()
	{
		foreach (KeyValuePair<Tag, PinnedResourcesPanel.PinnedResourceRow> keyValuePair in this.rows)
		{
			if (keyValuePair.Value.gameObject.activeSelf && DiscoveredResources.Instance.newDiscoveries.ContainsKey(keyValuePair.Key))
			{
				DiscoveredResources.Instance.newDiscoveries.Remove(keyValuePair.Key);
			}
		}
	}

	// Token: 0x06005A3F RID: 23103 RVA: 0x0020A8BC File Offset: 0x00208ABC
	private void UnPinAll()
	{
		WorldInventory worldInventory = ClusterManager.Instance.GetWorld(ClusterManager.Instance.activeWorldId).worldInventory;
		foreach (KeyValuePair<Tag, PinnedResourcesPanel.PinnedResourceRow> keyValuePair in this.rows)
		{
			worldInventory.pinnedResources.Remove(keyValuePair.Key);
		}
	}

	// Token: 0x06005A40 RID: 23104 RVA: 0x0020A938 File Offset: 0x00208B38
	private PinnedResourcesPanel.PinnedResourceRow CreateRow(Tag tag)
	{
		PinnedResourcesPanel.PinnedResourceRow pinnedResourceRow = new PinnedResourcesPanel.PinnedResourceRow(tag);
		GameObject gameObject = Util.KInstantiateUI(this.linePrefab, this.rowContainer, false);
		pinnedResourceRow.gameObject = gameObject;
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		pinnedResourceRow.icon = component.GetReference<Image>("Icon");
		pinnedResourceRow.nameLabel = component.GetReference<LocText>("NameLabel");
		pinnedResourceRow.valueLabel = component.GetReference<LocText>("ValueLabel");
		pinnedResourceRow.pinToggle = component.GetReference<MultiToggle>("PinToggle");
		pinnedResourceRow.notifyToggle = component.GetReference<MultiToggle>("NotifyToggle");
		pinnedResourceRow.newLabel = component.GetReference<MultiToggle>("NewLabel");
		global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(tag, "ui", false);
		pinnedResourceRow.icon.sprite = uisprite.first;
		pinnedResourceRow.icon.color = uisprite.second;
		pinnedResourceRow.nameLabel.SetText(tag.ProperNameStripLink());
		MultiToggle component2 = pinnedResourceRow.gameObject.GetComponent<MultiToggle>();
		component2.onClick = (System.Action)Delegate.Combine(component2.onClick, new System.Action(delegate
		{
			List<Pickupable> list = ClusterManager.Instance.activeWorld.worldInventory.CreatePickupablesList(tag);
			if (list != null && list.Count > 0)
			{
				SelectTool.Instance.SelectAndFocus(list[this.clickIdx % list.Count].transform.position, list[this.clickIdx % list.Count].GetComponent<KSelectable>());
				this.clickIdx++;
				return;
			}
			this.clickIdx = 0;
		}));
		return pinnedResourceRow;
	}

	// Token: 0x06005A41 RID: 23105 RVA: 0x0020AA68 File Offset: 0x00208C68
	public void Populate(object data = null)
	{
		WorldInventory worldInventory = ClusterManager.Instance.GetWorld(ClusterManager.Instance.activeWorldId).worldInventory;
		foreach (KeyValuePair<Tag, float> keyValuePair in DiscoveredResources.Instance.newDiscoveries)
		{
			if (!this.rows.ContainsKey(keyValuePair.Key) && this.IsDisplayedTag(keyValuePair.Key))
			{
				this.rows.Add(keyValuePair.Key, this.CreateRow(keyValuePair.Key));
			}
		}
		foreach (Tag tag in worldInventory.pinnedResources)
		{
			if (!this.rows.ContainsKey(tag))
			{
				this.rows.Add(tag, this.CreateRow(tag));
			}
		}
		foreach (Tag tag2 in worldInventory.notifyResources)
		{
			if (!this.rows.ContainsKey(tag2))
			{
				this.rows.Add(tag2, this.CreateRow(tag2));
			}
		}
		foreach (KeyValuePair<Tag, PinnedResourcesPanel.PinnedResourceRow> keyValuePair2 in this.rows)
		{
			if (false || worldInventory.pinnedResources.Contains(keyValuePair2.Key) || worldInventory.notifyResources.Contains(keyValuePair2.Key) || (DiscoveredResources.Instance.newDiscoveries.ContainsKey(keyValuePair2.Key) && worldInventory.GetAmount(keyValuePair2.Key, false) > 0f))
			{
				if (!keyValuePair2.Value.gameObject.activeSelf)
				{
					keyValuePair2.Value.gameObject.SetActive(true);
				}
			}
			else if (keyValuePair2.Value.gameObject.activeSelf)
			{
				keyValuePair2.Value.gameObject.SetActive(false);
			}
		}
		foreach (KeyValuePair<Tag, PinnedResourcesPanel.PinnedResourceRow> keyValuePair3 in this.rows)
		{
			keyValuePair3.Value.pinToggle.gameObject.SetActive(worldInventory.pinnedResources.Contains(keyValuePair3.Key));
		}
		this.SortRows();
		this.rowContainerLayout.ForceUpdate();
	}

	// Token: 0x06005A42 RID: 23106 RVA: 0x0020AD40 File Offset: 0x00208F40
	private void SortRows()
	{
		List<PinnedResourcesPanel.PinnedResourceRow> list = new List<PinnedResourcesPanel.PinnedResourceRow>();
		foreach (KeyValuePair<Tag, PinnedResourcesPanel.PinnedResourceRow> keyValuePair in this.rows)
		{
			list.Add(keyValuePair.Value);
		}
		list.Sort((PinnedResourcesPanel.PinnedResourceRow a, PinnedResourcesPanel.PinnedResourceRow b) => a.SortableNameWithoutLink.CompareTo(b.SortableNameWithoutLink));
		foreach (PinnedResourcesPanel.PinnedResourceRow pinnedResourceRow in list)
		{
			this.rows[pinnedResourceRow.Tag].gameObject.transform.SetAsLastSibling();
		}
		this.clearNewButton.transform.SetAsLastSibling();
		this.seeAllButton.transform.SetAsLastSibling();
	}

	// Token: 0x06005A43 RID: 23107 RVA: 0x0020AE3C File Offset: 0x0020903C
	private bool IsDisplayedTag(Tag tag)
	{
		foreach (TagSet tagSet in AllResourcesScreen.Instance.allowDisplayCategories)
		{
			foreach (KeyValuePair<Tag, HashSet<Tag>> keyValuePair in DiscoveredResources.Instance.GetDiscoveredResourcesFromTagSet(tagSet))
			{
				if (keyValuePair.Value.Contains(tag))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06005A44 RID: 23108 RVA: 0x0020AEE4 File Offset: 0x002090E4
	private void SyncRows()
	{
		WorldInventory worldInventory = ClusterManager.Instance.GetWorld(ClusterManager.Instance.activeWorldId).worldInventory;
		bool flag = false;
		foreach (Tag tag in worldInventory.pinnedResources)
		{
			if (!this.rows.ContainsKey(tag))
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			foreach (KeyValuePair<Tag, float> keyValuePair in DiscoveredResources.Instance.newDiscoveries)
			{
				if (!this.rows.ContainsKey(keyValuePair.Key) && this.IsDisplayedTag(keyValuePair.Key))
				{
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			foreach (Tag tag2 in worldInventory.notifyResources)
			{
				if (!this.rows.ContainsKey(tag2))
				{
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			foreach (KeyValuePair<Tag, PinnedResourcesPanel.PinnedResourceRow> keyValuePair2 in this.rows)
			{
				if ((worldInventory.pinnedResources.Contains(keyValuePair2.Key) || worldInventory.notifyResources.Contains(keyValuePair2.Key) || (DiscoveredResources.Instance.newDiscoveries.ContainsKey(keyValuePair2.Key) && worldInventory.GetAmount(keyValuePair2.Key, false) > 0f)) != keyValuePair2.Value.gameObject.activeSelf)
				{
					flag = true;
					break;
				}
			}
		}
		if (flag)
		{
			this.Populate(null);
		}
	}

	// Token: 0x06005A45 RID: 23109 RVA: 0x0020B0E0 File Offset: 0x002092E0
	public void Refresh()
	{
		this.SyncRows();
		WorldInventory worldInventory = ClusterManager.Instance.GetWorld(ClusterManager.Instance.activeWorldId).worldInventory;
		bool flag = false;
		foreach (KeyValuePair<Tag, PinnedResourcesPanel.PinnedResourceRow> keyValuePair in this.rows)
		{
			if (keyValuePair.Value.gameObject.activeSelf)
			{
				this.RefreshLine(keyValuePair.Key, worldInventory, false);
				flag = flag || DiscoveredResources.Instance.newDiscoveries.ContainsKey(keyValuePair.Key);
			}
		}
		this.clearNewButton.gameObject.SetActive(flag);
		this.seeAllLabel.SetText(string.Format(UI.RESOURCESCREEN.SEE_ALL, AllResourcesScreen.Instance.UniqueResourceRowCount()));
	}

	// Token: 0x06005A46 RID: 23110 RVA: 0x0020B1C8 File Offset: 0x002093C8
	private void RefreshLine(Tag tag, WorldInventory inventory, bool initialConfig = false)
	{
		Tag tag2 = tag;
		if (!AllResourcesScreen.Instance.units.ContainsKey(tag))
		{
			AllResourcesScreen.Instance.units.Add(tag, GameUtil.MeasureUnit.quantity);
		}
		if (!inventory.HasValidCount)
		{
			this.rows[tag].valueLabel.SetText(UI.ALLRESOURCESSCREEN.FIRST_FRAME_NO_DATA);
		}
		else
		{
			switch (AllResourcesScreen.Instance.units[tag])
			{
			case GameUtil.MeasureUnit.mass:
			{
				float amount = inventory.GetAmount(tag2, false);
				if (this.rows[tag].CheckAmountChanged(amount, true))
				{
					this.rows[tag].valueLabel.SetText(GameUtil.GetFormattedMass(amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				}
				break;
			}
			case GameUtil.MeasureUnit.kcal:
			{
				float num = RationTracker.Get().CountRationsByFoodType(tag.Name, ClusterManager.Instance.activeWorld.worldInventory, true);
				if (this.rows[tag].CheckAmountChanged(num, true))
				{
					this.rows[tag].valueLabel.SetText(GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true));
				}
				break;
			}
			case GameUtil.MeasureUnit.quantity:
			{
				float amount2 = inventory.GetAmount(tag2, false);
				if (this.rows[tag].CheckAmountChanged(amount2, true))
				{
					this.rows[tag].valueLabel.SetText(GameUtil.GetFormattedUnits(amount2, GameUtil.TimeSlice.None, true, ""));
				}
				break;
			}
			}
		}
		this.rows[tag].pinToggle.onClick = delegate
		{
			inventory.pinnedResources.Remove(tag);
			this.SyncRows();
		};
		this.rows[tag].notifyToggle.onClick = delegate
		{
			inventory.notifyResources.Remove(tag);
			this.SyncRows();
		};
		this.rows[tag].newLabel.gameObject.SetActive(DiscoveredResources.Instance.newDiscoveries.ContainsKey(tag));
		this.rows[tag].newLabel.onClick = delegate
		{
			AllResourcesScreen.Instance.Show(!AllResourcesScreen.Instance.gameObject.activeSelf);
		};
	}

	// Token: 0x06005A47 RID: 23111 RVA: 0x0020B45C File Offset: 0x0020965C
	public void Render1000ms(float dt)
	{
		if (this.headerButton != null && this.headerButton.CurrentState == 0)
		{
			return;
		}
		this.Refresh();
	}

	// Token: 0x04003CFA RID: 15610
	public GameObject linePrefab;

	// Token: 0x04003CFB RID: 15611
	public GameObject rowContainer;

	// Token: 0x04003CFC RID: 15612
	public MultiToggle headerButton;

	// Token: 0x04003CFD RID: 15613
	public MultiToggle clearNewButton;

	// Token: 0x04003CFE RID: 15614
	public KButton clearAllButton;

	// Token: 0x04003CFF RID: 15615
	public MultiToggle seeAllButton;

	// Token: 0x04003D00 RID: 15616
	private LocText seeAllLabel;

	// Token: 0x04003D01 RID: 15617
	private QuickLayout rowContainerLayout;

	// Token: 0x04003D02 RID: 15618
	private Dictionary<Tag, PinnedResourcesPanel.PinnedResourceRow> rows = new Dictionary<Tag, PinnedResourcesPanel.PinnedResourceRow>();

	// Token: 0x04003D03 RID: 15619
	public static PinnedResourcesPanel Instance;

	// Token: 0x04003D04 RID: 15620
	private int clickIdx;

	// Token: 0x020019FC RID: 6652
	public class PinnedResourceRow
	{
		// Token: 0x060091CF RID: 37327 RVA: 0x00315CE8 File Offset: 0x00313EE8
		public PinnedResourceRow(Tag tag)
		{
			this.Tag = tag;
			this.SortableNameWithoutLink = tag.ProperNameStripLink();
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x060091D0 RID: 37328 RVA: 0x00315D0E File Offset: 0x00313F0E
		// (set) Token: 0x060091D1 RID: 37329 RVA: 0x00315D16 File Offset: 0x00313F16
		public Tag Tag { get; private set; }

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x060091D2 RID: 37330 RVA: 0x00315D1F File Offset: 0x00313F1F
		// (set) Token: 0x060091D3 RID: 37331 RVA: 0x00315D27 File Offset: 0x00313F27
		public string SortableNameWithoutLink { get; private set; }

		// Token: 0x060091D4 RID: 37332 RVA: 0x00315D30 File Offset: 0x00313F30
		public bool CheckAmountChanged(float newResourceAmount, bool updateIfTrue)
		{
			bool flag = newResourceAmount != this.oldResourceAmount;
			if (flag && updateIfTrue)
			{
				this.oldResourceAmount = newResourceAmount;
			}
			return flag;
		}

		// Token: 0x04007607 RID: 30215
		public GameObject gameObject;

		// Token: 0x04007608 RID: 30216
		public Image icon;

		// Token: 0x04007609 RID: 30217
		public LocText nameLabel;

		// Token: 0x0400760A RID: 30218
		public LocText valueLabel;

		// Token: 0x0400760B RID: 30219
		public MultiToggle pinToggle;

		// Token: 0x0400760C RID: 30220
		public MultiToggle notifyToggle;

		// Token: 0x0400760D RID: 30221
		public MultiToggle newLabel;

		// Token: 0x0400760E RID: 30222
		private float oldResourceAmount = -1f;
	}
}
