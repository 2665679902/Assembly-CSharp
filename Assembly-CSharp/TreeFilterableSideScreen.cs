using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BEE RID: 3054
public class TreeFilterableSideScreen : SideScreenContent
{
	// Token: 0x170006AE RID: 1710
	// (get) Token: 0x0600604C RID: 24652 RVA: 0x00233CF0 File Offset: 0x00231EF0
	public bool IsStorage
	{
		get
		{
			return this.storage != null;
		}
	}

	// Token: 0x0600604D RID: 24653 RVA: 0x00233CFE File Offset: 0x00231EFE
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.Initialize();
	}

	// Token: 0x0600604E RID: 24654 RVA: 0x00233D0C File Offset: 0x00231F0C
	private void Initialize()
	{
		if (this.initialized)
		{
			return;
		}
		this.rowPool = new UIPool<TreeFilterableSideScreenRow>(this.rowPrefab);
		this.elementPool = new UIPool<TreeFilterableSideScreenElement>(this.elementPrefab);
		MultiToggle multiToggle = this.allCheckBox;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			TreeFilterableSideScreenRow.State allCheckboxState = this.GetAllCheckboxState();
			if (allCheckboxState > TreeFilterableSideScreenRow.State.Mixed)
			{
				if (allCheckboxState == TreeFilterableSideScreenRow.State.On)
				{
					this.SetAllCheckboxState(TreeFilterableSideScreenRow.State.Off);
					return;
				}
			}
			else
			{
				this.SetAllCheckboxState(TreeFilterableSideScreenRow.State.On);
			}
		}));
		this.onlyAllowTransportItemsCheckBox.onClick = new System.Action(this.OnlyAllowTransportItemsClicked);
		this.onlyAllowSpicedItemsCheckBox.onClick = new System.Action(this.OnlyAllowSpicedItemsClicked);
		this.initialized = true;
	}

	// Token: 0x0600604F RID: 24655 RVA: 0x00233DA0 File Offset: 0x00231FA0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.allCheckBox.transform.parent.parent.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.TREEFILTERABLESIDESCREEN.ALLBUTTONTOOLTIP);
		this.onlyAllowTransportItemsCheckBox.transform.parent.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.TREEFILTERABLESIDESCREEN.ONLYALLOWTRANSPORTITEMSBUTTONTOOLTIP);
		this.onlyAllowSpicedItemsCheckBox.transform.parent.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.TREEFILTERABLESIDESCREEN.ONLYALLOWSPICEDITEMSBUTTONTOOLTIP);
	}

	// Token: 0x06006050 RID: 24656 RVA: 0x00233E24 File Offset: 0x00232024
	private void UpdateAllCheckBoxVisualState()
	{
		switch (this.GetAllCheckboxState())
		{
		case TreeFilterableSideScreenRow.State.Off:
			this.allCheckBox.ChangeState(0);
			break;
		case TreeFilterableSideScreenRow.State.Mixed:
			this.allCheckBox.ChangeState(1);
			break;
		case TreeFilterableSideScreenRow.State.On:
			this.allCheckBox.ChangeState(2);
			break;
		}
		this.visualDirty = false;
	}

	// Token: 0x06006051 RID: 24657 RVA: 0x00233E7C File Offset: 0x0023207C
	public void Update()
	{
		foreach (KeyValuePair<Tag, TreeFilterableSideScreenRow> keyValuePair in this.tagRowMap)
		{
			if (keyValuePair.Value.visualDirty)
			{
				keyValuePair.Value.UpdateCheckBoxVisualState();
				this.visualDirty = true;
			}
		}
		if (this.visualDirty)
		{
			this.UpdateAllCheckBoxVisualState();
		}
	}

	// Token: 0x06006052 RID: 24658 RVA: 0x00233EF8 File Offset: 0x002320F8
	private void OnlyAllowTransportItemsClicked()
	{
		this.storage.SetOnlyFetchMarkedItems(!this.storage.GetOnlyFetchMarkedItems());
	}

	// Token: 0x06006053 RID: 24659 RVA: 0x00233F13 File Offset: 0x00232113
	private void OnlyAllowSpicedItemsClicked()
	{
		FoodStorage component = this.storage.GetComponent<FoodStorage>();
		component.SpicedFoodOnly = !component.SpicedFoodOnly;
	}

	// Token: 0x06006054 RID: 24660 RVA: 0x00233F30 File Offset: 0x00232130
	private TreeFilterableSideScreenRow.State GetAllCheckboxState()
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		foreach (KeyValuePair<Tag, TreeFilterableSideScreenRow> keyValuePair in this.tagRowMap)
		{
			switch (keyValuePair.Value.GetState())
			{
			case TreeFilterableSideScreenRow.State.Off:
				flag2 = true;
				break;
			case TreeFilterableSideScreenRow.State.Mixed:
				flag3 = true;
				break;
			case TreeFilterableSideScreenRow.State.On:
				flag = true;
				break;
			}
		}
		if (flag3)
		{
			return TreeFilterableSideScreenRow.State.Mixed;
		}
		if (flag && !flag2)
		{
			return TreeFilterableSideScreenRow.State.On;
		}
		if (!flag && flag2)
		{
			return TreeFilterableSideScreenRow.State.Off;
		}
		if (flag && flag2)
		{
			return TreeFilterableSideScreenRow.State.Mixed;
		}
		return TreeFilterableSideScreenRow.State.Off;
	}

	// Token: 0x06006055 RID: 24661 RVA: 0x00233FD0 File Offset: 0x002321D0
	private void SetAllCheckboxState(TreeFilterableSideScreenRow.State newState)
	{
		switch (newState)
		{
		case TreeFilterableSideScreenRow.State.Off:
		{
			using (Dictionary<Tag, TreeFilterableSideScreenRow>.Enumerator enumerator = this.tagRowMap.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<Tag, TreeFilterableSideScreenRow> keyValuePair = enumerator.Current;
					keyValuePair.Value.ChangeCheckBoxState(TreeFilterableSideScreenRow.State.Off);
				}
				goto IL_8C;
			}
			break;
		}
		case TreeFilterableSideScreenRow.State.Mixed:
			goto IL_8C;
		case TreeFilterableSideScreenRow.State.On:
			break;
		default:
			goto IL_8C;
		}
		foreach (KeyValuePair<Tag, TreeFilterableSideScreenRow> keyValuePair2 in this.tagRowMap)
		{
			keyValuePair2.Value.ChangeCheckBoxState(TreeFilterableSideScreenRow.State.On);
		}
		IL_8C:
		this.visualDirty = true;
	}

	// Token: 0x06006056 RID: 24662 RVA: 0x0023408C File Offset: 0x0023228C
	public bool GetElementTagAcceptedState(Tag t)
	{
		return this.targetFilterable.ContainsTag(t);
	}

	// Token: 0x06006057 RID: 24663 RVA: 0x0023409C File Offset: 0x0023229C
	public override bool IsValidForTarget(GameObject target)
	{
		TreeFilterable component = target.GetComponent<TreeFilterable>();
		Storage component2 = target.GetComponent<Storage>();
		return component != null && target.GetComponent<FlatTagFilterable>() == null && component.showUserMenu && (component2 == null || component2.showInUI);
	}

	// Token: 0x06006058 RID: 24664 RVA: 0x002340EC File Offset: 0x002322EC
	public override void SetTarget(GameObject target)
	{
		this.Initialize();
		this.target = target;
		if (target == null)
		{
			global::Debug.LogError("The target object provided was null");
			return;
		}
		this.targetFilterable = target.GetComponent<TreeFilterable>();
		if (this.targetFilterable == null)
		{
			global::Debug.LogError("The target provided does not have a Tree Filterable component");
			return;
		}
		this.contentMask.GetComponent<LayoutElement>().minHeight = (float)((this.targetFilterable.uiHeight == TreeFilterable.UISideScreenHeight.Tall) ? 380 : 256);
		this.storage = this.targetFilterable.GetComponent<Storage>();
		this.storage.Subscribe(644822890, new Action<object>(this.OnOnlyFetchMarkedItemsSettingChanged));
		this.storage.Subscribe(1163645216, new Action<object>(this.OnOnlySpicedItemsSettingChanged));
		this.OnOnlyFetchMarkedItemsSettingChanged(null);
		this.OnOnlySpicedItemsSettingChanged(null);
		this.CreateCategories();
		this.titlebar.SetActive(false);
		if (this.storage.showSideScreenTitleBar)
		{
			this.titlebar.SetActive(true);
			this.titlebar.GetComponentInChildren<LocText>().SetText(this.storage.GetProperName());
		}
	}

	// Token: 0x06006059 RID: 24665 RVA: 0x0023420C File Offset: 0x0023240C
	private void OnOnlyFetchMarkedItemsSettingChanged(object data)
	{
		this.onlyAllowTransportItemsCheckBox.ChangeState(this.storage.GetOnlyFetchMarkedItems() ? 1 : 0);
		if (this.storage.allowSettingOnlyFetchMarkedItems)
		{
			this.onlyallowTransportItemsRow.SetActive(true);
			return;
		}
		this.onlyallowTransportItemsRow.SetActive(false);
	}

	// Token: 0x0600605A RID: 24666 RVA: 0x0023425C File Offset: 0x0023245C
	private void OnOnlySpicedItemsSettingChanged(object data)
	{
		FoodStorage component = this.storage.GetComponent<FoodStorage>();
		if (component != null)
		{
			this.onlyallowSpicedItemsRow.SetActive(true);
			this.onlyAllowSpicedItemsCheckBox.ChangeState(component.SpicedFoodOnly ? 1 : 0);
			return;
		}
		this.onlyallowSpicedItemsRow.SetActive(false);
	}

	// Token: 0x0600605B RID: 24667 RVA: 0x002342AE File Offset: 0x002324AE
	public bool IsTagAllowed(Tag tag)
	{
		return this.targetFilterable.AcceptedTags.Contains(tag);
	}

	// Token: 0x0600605C RID: 24668 RVA: 0x002342C1 File Offset: 0x002324C1
	public void AddTag(Tag tag)
	{
		if (this.targetFilterable == null)
		{
			return;
		}
		this.targetFilterable.AddTagToFilter(tag);
	}

	// Token: 0x0600605D RID: 24669 RVA: 0x002342DE File Offset: 0x002324DE
	public void RemoveTag(Tag tag)
	{
		if (this.targetFilterable == null)
		{
			return;
		}
		this.targetFilterable.RemoveTagFromFilter(tag);
	}

	// Token: 0x0600605E RID: 24670 RVA: 0x002342FC File Offset: 0x002324FC
	private List<TreeFilterableSideScreen.TagOrderInfo> GetTagsSortedAlphabetically(ICollection<Tag> tags)
	{
		List<TreeFilterableSideScreen.TagOrderInfo> list = new List<TreeFilterableSideScreen.TagOrderInfo>();
		foreach (Tag tag in tags)
		{
			list.Add(new TreeFilterableSideScreen.TagOrderInfo
			{
				tag = tag,
				strippedName = tag.ProperNameStripLink()
			});
		}
		list.Sort((TreeFilterableSideScreen.TagOrderInfo a, TreeFilterableSideScreen.TagOrderInfo b) => a.strippedName.CompareTo(b.strippedName));
		return list;
	}

	// Token: 0x0600605F RID: 24671 RVA: 0x00234390 File Offset: 0x00232590
	private TreeFilterableSideScreenRow AddRow(Tag rowTag)
	{
		if (this.tagRowMap.ContainsKey(rowTag))
		{
			return this.tagRowMap[rowTag];
		}
		TreeFilterableSideScreenRow freeElement = this.rowPool.GetFreeElement(this.rowGroup, true);
		freeElement.Parent = this;
		this.tagRowMap.Add(rowTag, freeElement);
		Dictionary<Tag, bool> dictionary = new Dictionary<Tag, bool>();
		foreach (TreeFilterableSideScreen.TagOrderInfo tagOrderInfo in this.GetTagsSortedAlphabetically(DiscoveredResources.Instance.GetDiscoveredResourcesFromTag(rowTag)))
		{
			dictionary.Add(tagOrderInfo.tag, this.targetFilterable.ContainsTag(tagOrderInfo.tag) || this.targetFilterable.ContainsTag(rowTag));
		}
		freeElement.SetElement(rowTag, this.targetFilterable.ContainsTag(rowTag), dictionary);
		freeElement.transform.SetAsLastSibling();
		return freeElement;
	}

	// Token: 0x06006060 RID: 24672 RVA: 0x00234480 File Offset: 0x00232680
	public float GetAmountInStorage(Tag tag)
	{
		if (!this.IsStorage)
		{
			return 0f;
		}
		return this.storage.GetMassAvailable(tag);
	}

	// Token: 0x06006061 RID: 24673 RVA: 0x0023449C File Offset: 0x0023269C
	private void CreateCategories()
	{
		if (this.storage.storageFilters != null && this.storage.storageFilters.Count >= 1)
		{
			bool flag = this.target.GetComponent<CreatureDeliveryPoint>() != null;
			foreach (TreeFilterableSideScreen.TagOrderInfo tagOrderInfo in this.GetTagsSortedAlphabetically(this.storage.storageFilters))
			{
				Tag tag = tagOrderInfo.tag;
				if (flag || DiscoveredResources.Instance.IsDiscovered(tag))
				{
					this.AddRow(tag);
				}
			}
			this.visualDirty = true;
			return;
		}
		global::Debug.LogError("If you're filtering, your storage filter should have the filters set on it");
	}

	// Token: 0x06006062 RID: 24674 RVA: 0x0023455C File Offset: 0x0023275C
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (this.storage != null)
		{
			this.storage.Unsubscribe(644822890, new Action<object>(this.OnOnlyFetchMarkedItemsSettingChanged));
			this.storage.Unsubscribe(1163645216, new Action<object>(this.OnOnlySpicedItemsSettingChanged));
		}
		this.rowPool.ClearAll();
		this.elementPool.ClearAll();
		this.tagRowMap.Clear();
	}

	// Token: 0x04004200 RID: 16896
	[SerializeField]
	private MultiToggle allCheckBox;

	// Token: 0x04004201 RID: 16897
	[SerializeField]
	private MultiToggle onlyAllowTransportItemsCheckBox;

	// Token: 0x04004202 RID: 16898
	[SerializeField]
	private GameObject onlyallowTransportItemsRow;

	// Token: 0x04004203 RID: 16899
	[SerializeField]
	private MultiToggle onlyAllowSpicedItemsCheckBox;

	// Token: 0x04004204 RID: 16900
	[SerializeField]
	private GameObject onlyallowSpicedItemsRow;

	// Token: 0x04004205 RID: 16901
	[SerializeField]
	private TreeFilterableSideScreenRow rowPrefab;

	// Token: 0x04004206 RID: 16902
	[SerializeField]
	private GameObject rowGroup;

	// Token: 0x04004207 RID: 16903
	[SerializeField]
	private TreeFilterableSideScreenElement elementPrefab;

	// Token: 0x04004208 RID: 16904
	[SerializeField]
	private GameObject titlebar;

	// Token: 0x04004209 RID: 16905
	[SerializeField]
	private GameObject contentMask;

	// Token: 0x0400420A RID: 16906
	private GameObject target;

	// Token: 0x0400420B RID: 16907
	private bool visualDirty;

	// Token: 0x0400420C RID: 16908
	private bool initialized;

	// Token: 0x0400420D RID: 16909
	private KImage onlyAllowTransportItemsImg;

	// Token: 0x0400420E RID: 16910
	public UIPool<TreeFilterableSideScreenElement> elementPool;

	// Token: 0x0400420F RID: 16911
	private UIPool<TreeFilterableSideScreenRow> rowPool;

	// Token: 0x04004210 RID: 16912
	private TreeFilterable targetFilterable;

	// Token: 0x04004211 RID: 16913
	private Dictionary<Tag, TreeFilterableSideScreenRow> tagRowMap = new Dictionary<Tag, TreeFilterableSideScreenRow>();

	// Token: 0x04004212 RID: 16914
	private Storage storage;

	// Token: 0x02001A8A RID: 6794
	private struct TagOrderInfo
	{
		// Token: 0x040077CF RID: 30671
		public Tag tag;

		// Token: 0x040077D0 RID: 30672
		public string strippedName;
	}
}
