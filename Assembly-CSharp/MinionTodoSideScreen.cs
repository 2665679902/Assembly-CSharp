using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BC5 RID: 3013
public class MinionTodoSideScreen : SideScreenContent
{
	// Token: 0x17000696 RID: 1686
	// (get) Token: 0x06005EB1 RID: 24241 RVA: 0x002299CC File Offset: 0x00227BCC
	public static List<JobsTableScreen.PriorityInfo> priorityInfo
	{
		get
		{
			if (MinionTodoSideScreen._priorityInfo == null)
			{
				MinionTodoSideScreen._priorityInfo = new List<JobsTableScreen.PriorityInfo>
				{
					new JobsTableScreen.PriorityInfo(4, Assets.GetSprite("ic_dupe"), UI.JOBSSCREEN.PRIORITY_CLASS.COMPULSORY),
					new JobsTableScreen.PriorityInfo(3, Assets.GetSprite("notification_exclamation"), UI.JOBSSCREEN.PRIORITY_CLASS.EMERGENCY),
					new JobsTableScreen.PriorityInfo(2, Assets.GetSprite("status_item_room_required"), UI.JOBSSCREEN.PRIORITY_CLASS.PERSONAL_NEEDS),
					new JobsTableScreen.PriorityInfo(1, Assets.GetSprite("status_item_prioritized"), UI.JOBSSCREEN.PRIORITY_CLASS.HIGH),
					new JobsTableScreen.PriorityInfo(0, null, UI.JOBSSCREEN.PRIORITY_CLASS.BASIC),
					new JobsTableScreen.PriorityInfo(-1, Assets.GetSprite("icon_gear"), UI.JOBSSCREEN.PRIORITY_CLASS.IDLE)
				};
			}
			return MinionTodoSideScreen._priorityInfo;
		}
	}

	// Token: 0x06005EB2 RID: 24242 RVA: 0x00229AA4 File Offset: 0x00227CA4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.priorityGroups.Count != 0)
		{
			return;
		}
		foreach (JobsTableScreen.PriorityInfo priorityInfo in MinionTodoSideScreen.priorityInfo)
		{
			PriorityScreen.PriorityClass priority = (PriorityScreen.PriorityClass)priorityInfo.priority;
			if (priority == PriorityScreen.PriorityClass.basic)
			{
				for (int i = 5; i >= 0; i--)
				{
					global::Tuple<PriorityScreen.PriorityClass, int, HierarchyReferences> tuple = new global::Tuple<PriorityScreen.PriorityClass, int, HierarchyReferences>(priority, i, Util.KInstantiateUI<HierarchyReferences>(this.priorityGroupPrefab, this.taskEntryContainer, false));
					tuple.third.name = "PriorityGroup_" + priorityInfo.name + "_" + i.ToString();
					tuple.third.gameObject.SetActive(true);
					JobsTableScreen.PriorityInfo priorityInfo2 = JobsTableScreen.priorityInfo[i];
					tuple.third.GetReference<LocText>("Title").text = priorityInfo2.name.text.ToUpper();
					tuple.third.GetReference<Image>("PriorityIcon").sprite = priorityInfo2.sprite;
					this.priorityGroups.Add(tuple);
				}
			}
			else
			{
				global::Tuple<PriorityScreen.PriorityClass, int, HierarchyReferences> tuple2 = new global::Tuple<PriorityScreen.PriorityClass, int, HierarchyReferences>(priority, 3, Util.KInstantiateUI<HierarchyReferences>(this.priorityGroupPrefab, this.taskEntryContainer, false));
				tuple2.third.name = "PriorityGroup_" + priorityInfo.name;
				tuple2.third.gameObject.SetActive(true);
				tuple2.third.GetReference<LocText>("Title").text = priorityInfo.name.text.ToUpper();
				tuple2.third.GetReference<Image>("PriorityIcon").sprite = priorityInfo.sprite;
				this.priorityGroups.Add(tuple2);
			}
		}
	}

	// Token: 0x06005EB3 RID: 24243 RVA: 0x00229C90 File Offset: 0x00227E90
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<MinionIdentity>() != null && !target.HasTag(GameTags.Dead);
	}

	// Token: 0x06005EB4 RID: 24244 RVA: 0x00229CB0 File Offset: 0x00227EB0
	public override void ClearTarget()
	{
		base.ClearTarget();
		this.refreshHandle.ClearScheduler();
	}

	// Token: 0x06005EB5 RID: 24245 RVA: 0x00229CC3 File Offset: 0x00227EC3
	public override void SetTarget(GameObject target)
	{
		this.refreshHandle.ClearScheduler();
		if (this.priorityGroups.Count == 0)
		{
			this.OnPrefabInit();
		}
		base.SetTarget(target);
	}

	// Token: 0x06005EB6 RID: 24246 RVA: 0x00229CEA File Offset: 0x00227EEA
	public override void ScreenUpdate(bool topLevel)
	{
		base.ScreenUpdate(topLevel);
		this.PopulateElements(null);
	}

	// Token: 0x06005EB7 RID: 24247 RVA: 0x00229CFC File Offset: 0x00227EFC
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		this.refreshHandle.ClearScheduler();
		if (!show)
		{
			if (this.useOffscreenIndicators)
			{
				foreach (GameObject gameObject in this.choreTargets)
				{
					OffscreenIndicator.Instance.DeactivateIndicator(gameObject);
				}
			}
			return;
		}
		if (DetailsScreen.Instance.target == null)
		{
			return;
		}
		this.choreConsumer = DetailsScreen.Instance.target.GetComponent<ChoreConsumer>();
		this.PopulateElements(null);
	}

	// Token: 0x06005EB8 RID: 24248 RVA: 0x00229DA0 File Offset: 0x00227FA0
	private void PopulateElements(object data = null)
	{
		this.refreshHandle.ClearScheduler();
		this.refreshHandle = UIScheduler.Instance.Schedule("RefreshToDoList", 0.1f, new Action<object>(this.PopulateElements), null, null);
		ListPool<Chore.Precondition.Context, BuildingChoresPanel>.PooledList pooledList = ListPool<Chore.Precondition.Context, BuildingChoresPanel>.Allocate();
		ChoreConsumer.PreconditionSnapshot lastPreconditionSnapshot = this.choreConsumer.GetLastPreconditionSnapshot();
		if (lastPreconditionSnapshot.doFailedContextsNeedSorting)
		{
			lastPreconditionSnapshot.failedContexts.Sort();
			lastPreconditionSnapshot.doFailedContextsNeedSorting = false;
		}
		pooledList.AddRange(lastPreconditionSnapshot.failedContexts);
		pooledList.AddRange(lastPreconditionSnapshot.succeededContexts);
		Chore.Precondition.Context context = default(Chore.Precondition.Context);
		MinionTodoChoreEntry minionTodoChoreEntry = null;
		int num = 0;
		Schedulable component = DetailsScreen.Instance.target.GetComponent<Schedulable>();
		string text = "";
		Schedule schedule = component.GetSchedule();
		if (schedule != null)
		{
			text = schedule.GetBlock(Schedule.GetBlockIdx()).name;
		}
		this.currentScheduleBlockLabel.SetText(string.Format(UI.UISIDESCREENS.MINIONTODOSIDESCREEN.CURRENT_SCHEDULE_BLOCK, text));
		this.choreTargets.Clear();
		bool flag = false;
		this.activeChoreEntries = 0;
		for (int i = pooledList.Count - 1; i >= 0; i--)
		{
			if (pooledList[i].chore != null && !pooledList[i].chore.target.isNull && !(pooledList[i].chore.target.gameObject == null) && pooledList[i].IsPotentialSuccess())
			{
				if (pooledList[i].chore.driver == this.choreConsumer.choreDriver)
				{
					this.currentTask.Apply(pooledList[i]);
					minionTodoChoreEntry = this.currentTask;
					context = pooledList[i];
					num = 0;
					flag = true;
				}
				else if (!flag && this.activeChoreEntries != 0 && GameUtil.AreChoresUIMergeable(pooledList[i], context))
				{
					num++;
					minionTodoChoreEntry.SetMoreAmount(num);
				}
				else
				{
					HierarchyReferences hierarchyReferences = this.PriorityGroupForPriority(this.choreConsumer, pooledList[i].chore);
					if (hierarchyReferences == null)
					{
						DebugUtil.DevLogError(string.Format("Priority group was null for {0} with priority class {1} and personaly priority {2}", pooledList[i].chore.GetReportName(null), pooledList[i].chore.masterPriority.priority_class, this.choreConsumer.GetPersonalPriority(pooledList[i].chore.choreType)));
					}
					else
					{
						MinionTodoChoreEntry choreEntry = this.GetChoreEntry(hierarchyReferences.GetReference<RectTransform>("EntriesContainer"));
						choreEntry.Apply(pooledList[i]);
						minionTodoChoreEntry = choreEntry;
						context = pooledList[i];
						num = 0;
						flag = false;
					}
				}
			}
		}
		pooledList.Recycle();
		for (int j = this.choreEntries.Count - 1; j >= this.activeChoreEntries; j--)
		{
			this.choreEntries[j].gameObject.SetActive(false);
		}
		foreach (global::Tuple<PriorityScreen.PriorityClass, int, HierarchyReferences> tuple in this.priorityGroups)
		{
			RectTransform reference = tuple.third.GetReference<RectTransform>("EntriesContainer");
			tuple.third.gameObject.SetActive(reference.childCount > 0);
		}
	}

	// Token: 0x06005EB9 RID: 24249 RVA: 0x0022A0FC File Offset: 0x002282FC
	private MinionTodoChoreEntry GetChoreEntry(RectTransform parent)
	{
		MinionTodoChoreEntry minionTodoChoreEntry;
		if (this.activeChoreEntries >= this.choreEntries.Count - 1)
		{
			minionTodoChoreEntry = Util.KInstantiateUI<MinionTodoChoreEntry>(this.taskEntryPrefab.gameObject, parent.gameObject, false);
			this.choreEntries.Add(minionTodoChoreEntry);
		}
		else
		{
			minionTodoChoreEntry = this.choreEntries[this.activeChoreEntries];
			minionTodoChoreEntry.transform.SetParent(parent);
			minionTodoChoreEntry.transform.SetAsLastSibling();
		}
		this.activeChoreEntries++;
		minionTodoChoreEntry.gameObject.SetActive(true);
		return minionTodoChoreEntry;
	}

	// Token: 0x06005EBA RID: 24250 RVA: 0x0022A188 File Offset: 0x00228388
	private HierarchyReferences PriorityGroupForPriority(ChoreConsumer choreConsumer, Chore chore)
	{
		foreach (global::Tuple<PriorityScreen.PriorityClass, int, HierarchyReferences> tuple in this.priorityGroups)
		{
			if (tuple.first == chore.masterPriority.priority_class)
			{
				if (chore.masterPriority.priority_class != PriorityScreen.PriorityClass.basic)
				{
					return tuple.third;
				}
				if (tuple.second == choreConsumer.GetPersonalPriority(chore.choreType))
				{
					return tuple.third;
				}
			}
		}
		return null;
	}

	// Token: 0x06005EBB RID: 24251 RVA: 0x0022A220 File Offset: 0x00228420
	private void Button_onPointerEnter()
	{
		throw new NotImplementedException();
	}

	// Token: 0x040040CD RID: 16589
	private bool useOffscreenIndicators;

	// Token: 0x040040CE RID: 16590
	public MinionTodoChoreEntry taskEntryPrefab;

	// Token: 0x040040CF RID: 16591
	public GameObject priorityGroupPrefab;

	// Token: 0x040040D0 RID: 16592
	public GameObject taskEntryContainer;

	// Token: 0x040040D1 RID: 16593
	public MinionTodoChoreEntry currentTask;

	// Token: 0x040040D2 RID: 16594
	public LocText currentScheduleBlockLabel;

	// Token: 0x040040D3 RID: 16595
	private List<global::Tuple<PriorityScreen.PriorityClass, int, HierarchyReferences>> priorityGroups = new List<global::Tuple<PriorityScreen.PriorityClass, int, HierarchyReferences>>();

	// Token: 0x040040D4 RID: 16596
	private List<MinionTodoChoreEntry> choreEntries = new List<MinionTodoChoreEntry>();

	// Token: 0x040040D5 RID: 16597
	private List<GameObject> choreTargets = new List<GameObject>();

	// Token: 0x040040D6 RID: 16598
	private SchedulerHandle refreshHandle;

	// Token: 0x040040D7 RID: 16599
	private ChoreConsumer choreConsumer;

	// Token: 0x040040D8 RID: 16600
	[SerializeField]
	private ColorStyleSetting buttonColorSettingCurrent;

	// Token: 0x040040D9 RID: 16601
	[SerializeField]
	private ColorStyleSetting buttonColorSettingStandard;

	// Token: 0x040040DA RID: 16602
	private static List<JobsTableScreen.PriorityInfo> _priorityInfo;

	// Token: 0x040040DB RID: 16603
	private int activeChoreEntries;
}
