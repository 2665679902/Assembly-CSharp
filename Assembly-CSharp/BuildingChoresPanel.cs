using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A4D RID: 2637
public class BuildingChoresPanel : TargetScreen
{
	// Token: 0x06005011 RID: 20497 RVA: 0x001CA720 File Offset: 0x001C8920
	public override bool IsValidForTarget(GameObject target)
	{
		KPrefabID component = target.GetComponent<KPrefabID>();
		return component != null && component.HasTag(GameTags.HasChores) && !component.IsPrefabID(GameTags.Minion);
	}

	// Token: 0x06005012 RID: 20498 RVA: 0x001CA75A File Offset: 0x001C895A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.choreGroup = Util.KInstantiateUI<HierarchyReferences>(this.choreGroupPrefab, base.gameObject, false);
		this.choreGroup.gameObject.SetActive(true);
	}

	// Token: 0x06005013 RID: 20499 RVA: 0x001CA78B File Offset: 0x001C898B
	private void Update()
	{
		this.Refresh();
	}

	// Token: 0x06005014 RID: 20500 RVA: 0x001CA793 File Offset: 0x001C8993
	public override void OnSelectTarget(GameObject target)
	{
		base.OnSelectTarget(target);
		this.Refresh();
	}

	// Token: 0x06005015 RID: 20501 RVA: 0x001CA7A2 File Offset: 0x001C89A2
	public override void OnDeselectTarget(GameObject target)
	{
		base.OnDeselectTarget(target);
	}

	// Token: 0x06005016 RID: 20502 RVA: 0x001CA7AB File Offset: 0x001C89AB
	private void Refresh()
	{
		this.RefreshDetails();
	}

	// Token: 0x06005017 RID: 20503 RVA: 0x001CA7B4 File Offset: 0x001C89B4
	private void RefreshDetails()
	{
		int myParentWorldId = this.selectedTarget.GetMyParentWorldId();
		List<Chore> list = null;
		GlobalChoreProvider.Instance.choreWorldMap.TryGetValue(myParentWorldId, out list);
		int num = 0;
		while (list != null && num < list.Count)
		{
			Chore chore = list[num];
			if (!chore.isNull && chore.gameObject == this.selectedTarget)
			{
				this.AddChoreEntry(chore);
			}
			num++;
		}
		List<FetchChore> list2 = null;
		GlobalChoreProvider.Instance.fetchMap.TryGetValue(myParentWorldId, out list2);
		int num2 = 0;
		while (list2 != null && num2 < list2.Count)
		{
			FetchChore fetchChore = list2[num2];
			if (!fetchChore.isNull && fetchChore.gameObject == this.selectedTarget)
			{
				this.AddChoreEntry(fetchChore);
			}
			num2++;
		}
		for (int i = this.activeDupeEntries; i < this.dupeEntries.Count; i++)
		{
			this.dupeEntries[i].gameObject.SetActive(false);
		}
		this.activeDupeEntries = 0;
		for (int j = this.activeChoreEntries; j < this.choreEntries.Count; j++)
		{
			this.choreEntries[j].gameObject.SetActive(false);
		}
		this.activeChoreEntries = 0;
	}

	// Token: 0x06005018 RID: 20504 RVA: 0x001CA8FC File Offset: 0x001C8AFC
	private void AddChoreEntry(Chore chore)
	{
		HierarchyReferences choreEntry = this.GetChoreEntry(GameUtil.GetChoreName(chore, null), chore.choreType, this.choreGroup.GetReference<RectTransform>("EntriesContainer"));
		FetchChore fetchChore = chore as FetchChore;
		ListPool<Chore.Precondition.Context, BuildingChoresPanel>.PooledList pooledList = ListPool<Chore.Precondition.Context, BuildingChoresPanel>.Allocate();
		foreach (Component component in Components.LiveMinionIdentities.Items)
		{
			pooledList.Clear();
			ChoreConsumer component2 = component.GetComponent<ChoreConsumer>();
			Chore.Precondition.Context context = default(Chore.Precondition.Context);
			ChoreConsumer.PreconditionSnapshot lastPreconditionSnapshot = component2.GetLastPreconditionSnapshot();
			if (lastPreconditionSnapshot.doFailedContextsNeedSorting)
			{
				lastPreconditionSnapshot.failedContexts.Sort();
				lastPreconditionSnapshot.doFailedContextsNeedSorting = false;
			}
			pooledList.AddRange(lastPreconditionSnapshot.failedContexts);
			pooledList.AddRange(lastPreconditionSnapshot.succeededContexts);
			int num = -1;
			int num2 = 0;
			for (int i = pooledList.Count - 1; i >= 0; i--)
			{
				if (!(pooledList[i].chore.driver != null) || !(pooledList[i].chore.driver != component2.choreDriver))
				{
					bool flag = pooledList[i].IsPotentialSuccess();
					if (flag)
					{
						num2++;
					}
					FetchAreaChore fetchAreaChore = pooledList[i].chore as FetchAreaChore;
					if (pooledList[i].chore == chore || (fetchChore != null && fetchAreaChore != null && fetchAreaChore.smi.SameDestination(fetchChore)))
					{
						num = (flag ? num2 : int.MaxValue);
						context = pooledList[i];
						break;
					}
				}
			}
			if (num >= 0)
			{
				this.DupeEntryDatas.Add(new BuildingChoresPanel.DupeEntryData
				{
					consumer = component2,
					context = context,
					personalPriority = component2.GetPersonalPriority(chore.choreType),
					rank = num
				});
			}
		}
		pooledList.Recycle();
		this.DupeEntryDatas.Sort();
		foreach (BuildingChoresPanel.DupeEntryData dupeEntryData in this.DupeEntryDatas)
		{
			this.GetDupeEntry(dupeEntryData, choreEntry.GetReference<RectTransform>("DupeContainer"));
		}
		this.DupeEntryDatas.Clear();
	}

	// Token: 0x06005019 RID: 20505 RVA: 0x001CAB6C File Offset: 0x001C8D6C
	private HierarchyReferences GetChoreEntry(string label, ChoreType choreType, RectTransform parent)
	{
		HierarchyReferences hierarchyReferences;
		if (this.activeChoreEntries >= this.choreEntries.Count)
		{
			hierarchyReferences = Util.KInstantiateUI<HierarchyReferences>(this.chorePrefab, parent.gameObject, false);
			this.choreEntries.Add(hierarchyReferences);
		}
		else
		{
			hierarchyReferences = this.choreEntries[this.activeChoreEntries];
			hierarchyReferences.transform.SetParent(parent);
			hierarchyReferences.transform.SetAsLastSibling();
		}
		this.activeChoreEntries++;
		hierarchyReferences.GetReference<LocText>("ChoreLabel").text = label;
		hierarchyReferences.GetReference<LocText>("ChoreSubLabel").text = GameUtil.ChoreGroupsForChoreType(choreType);
		Image reference = hierarchyReferences.GetReference<Image>("Icon");
		if (choreType.groups.Length != 0)
		{
			Sprite sprite = Assets.GetSprite(choreType.groups[0].sprite);
			reference.sprite = sprite;
			reference.gameObject.SetActive(true);
			reference.GetComponent<ToolTip>().toolTip = string.Format(UI.DETAILTABS.BUILDING_CHORES.CHORE_TYPE_TOOLTIP, choreType.groups[0].Name);
		}
		else
		{
			reference.gameObject.SetActive(false);
		}
		Image reference2 = hierarchyReferences.GetReference<Image>("Icon2");
		if (choreType.groups.Length > 1)
		{
			Sprite sprite2 = Assets.GetSprite(choreType.groups[1].sprite);
			reference2.sprite = sprite2;
			reference2.gameObject.SetActive(true);
			reference2.GetComponent<ToolTip>().toolTip = string.Format(UI.DETAILTABS.BUILDING_CHORES.CHORE_TYPE_TOOLTIP, choreType.groups[1].Name);
		}
		else
		{
			reference2.gameObject.SetActive(false);
		}
		hierarchyReferences.gameObject.SetActive(true);
		return hierarchyReferences;
	}

	// Token: 0x0600501A RID: 20506 RVA: 0x001CAD08 File Offset: 0x001C8F08
	private BuildingChoresPanelDupeRow GetDupeEntry(BuildingChoresPanel.DupeEntryData data, RectTransform parent)
	{
		BuildingChoresPanelDupeRow buildingChoresPanelDupeRow;
		if (this.activeDupeEntries >= this.dupeEntries.Count)
		{
			buildingChoresPanelDupeRow = Util.KInstantiateUI<BuildingChoresPanelDupeRow>(this.dupePrefab.gameObject, parent.gameObject, false);
			this.dupeEntries.Add(buildingChoresPanelDupeRow);
		}
		else
		{
			buildingChoresPanelDupeRow = this.dupeEntries[this.activeDupeEntries];
			buildingChoresPanelDupeRow.transform.SetParent(parent);
			buildingChoresPanelDupeRow.transform.SetAsLastSibling();
		}
		this.activeDupeEntries++;
		buildingChoresPanelDupeRow.Init(data);
		buildingChoresPanelDupeRow.gameObject.SetActive(true);
		return buildingChoresPanelDupeRow;
	}

	// Token: 0x040035C6 RID: 13766
	public GameObject choreGroupPrefab;

	// Token: 0x040035C7 RID: 13767
	public GameObject chorePrefab;

	// Token: 0x040035C8 RID: 13768
	public BuildingChoresPanelDupeRow dupePrefab;

	// Token: 0x040035C9 RID: 13769
	private GameObject detailsPanel;

	// Token: 0x040035CA RID: 13770
	private DetailsPanelDrawer drawer;

	// Token: 0x040035CB RID: 13771
	private HierarchyReferences choreGroup;

	// Token: 0x040035CC RID: 13772
	private List<HierarchyReferences> choreEntries = new List<HierarchyReferences>();

	// Token: 0x040035CD RID: 13773
	private int activeChoreEntries;

	// Token: 0x040035CE RID: 13774
	private List<BuildingChoresPanelDupeRow> dupeEntries = new List<BuildingChoresPanelDupeRow>();

	// Token: 0x040035CF RID: 13775
	private int activeDupeEntries;

	// Token: 0x040035D0 RID: 13776
	private List<BuildingChoresPanel.DupeEntryData> DupeEntryDatas = new List<BuildingChoresPanel.DupeEntryData>();

	// Token: 0x020018DB RID: 6363
	public class DupeEntryData : IComparable<BuildingChoresPanel.DupeEntryData>
	{
		// Token: 0x06008E98 RID: 36504 RVA: 0x0030DB94 File Offset: 0x0030BD94
		public int CompareTo(BuildingChoresPanel.DupeEntryData other)
		{
			if (this.personalPriority != other.personalPriority)
			{
				return other.personalPriority.CompareTo(this.personalPriority);
			}
			if (this.rank != other.rank)
			{
				return this.rank.CompareTo(other.rank);
			}
			if (this.consumer.GetProperName() != other.consumer.GetProperName())
			{
				return this.consumer.GetProperName().CompareTo(other.consumer.GetProperName());
			}
			return this.consumer.GetInstanceID().CompareTo(other.consumer.GetInstanceID());
		}

		// Token: 0x04007291 RID: 29329
		public ChoreConsumer consumer;

		// Token: 0x04007292 RID: 29330
		public Chore.Precondition.Context context;

		// Token: 0x04007293 RID: 29331
		public int personalPriority;

		// Token: 0x04007294 RID: 29332
		public int rank;
	}
}
