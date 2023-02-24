using System;
using System.Collections.Generic;
using System.Diagnostics;
using Database;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020003A4 RID: 932
[AddComponentMenu("KMonoBehaviour/scripts/ChoreConsumer")]
public class ChoreConsumer : KMonoBehaviour, IPersonalPriorityManager
{
	// Token: 0x06001300 RID: 4864 RVA: 0x00064E9D File Offset: 0x0006309D
	public List<ChoreProvider> GetProviders()
	{
		return this.providers;
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x00064EA5 File Offset: 0x000630A5
	public ChoreConsumer.PreconditionSnapshot GetLastPreconditionSnapshot()
	{
		return this.preconditionSnapshot;
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x00064EAD File Offset: 0x000630AD
	public List<Chore.Precondition.Context> GetSuceededPreconditionContexts()
	{
		return this.lastSuccessfulPreconditionSnapshot.succeededContexts;
	}

	// Token: 0x06001303 RID: 4867 RVA: 0x00064EBA File Offset: 0x000630BA
	public List<Chore.Precondition.Context> GetFailedPreconditionContexts()
	{
		return this.lastSuccessfulPreconditionSnapshot.failedContexts;
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x00064EC7 File Offset: 0x000630C7
	public ChoreConsumer.PreconditionSnapshot GetLastSuccessfulPreconditionSnapshot()
	{
		return this.lastSuccessfulPreconditionSnapshot;
	}

	// Token: 0x06001305 RID: 4869 RVA: 0x00064ED0 File Offset: 0x000630D0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (ChoreGroupManager.instance != null)
		{
			foreach (KeyValuePair<Tag, int> keyValuePair in ChoreGroupManager.instance.DefaultChorePermission)
			{
				bool flag = false;
				foreach (HashedString hashedString in this.userDisabledChoreGroups)
				{
					if (hashedString.HashValue == keyValuePair.Key.GetHashCode())
					{
						flag = true;
						break;
					}
				}
				if (!flag && keyValuePair.Value == 0)
				{
					this.userDisabledChoreGroups.Add(new HashedString(keyValuePair.Key.GetHashCode()));
				}
			}
		}
		this.providers.Add(this.choreProvider);
	}

	// Token: 0x06001306 RID: 4870 RVA: 0x00064FE0 File Offset: 0x000631E0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		KPrefabID component = base.GetComponent<KPrefabID>();
		if (this.choreTable != null)
		{
			this.choreTableInstance = new ChoreTable.Instance(this.choreTable, component);
		}
		foreach (ChoreGroup choreGroup in Db.Get().ChoreGroups.resources)
		{
			int personalPriority = this.GetPersonalPriority(choreGroup);
			this.UpdateChoreTypePriorities(choreGroup, personalPriority);
			this.SetPermittedByUser(choreGroup, personalPriority != 0);
		}
		this.consumerState = new ChoreConsumerState(this);
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x00065084 File Offset: 0x00063284
	protected override void OnForcedCleanUp()
	{
		if (this.consumerState != null)
		{
			this.consumerState.navigator = null;
		}
		this.navigator = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x000650A7 File Offset: 0x000632A7
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.choreTableInstance != null)
		{
			this.choreTableInstance.OnCleanUp(base.GetComponent<KPrefabID>());
			this.choreTableInstance = null;
		}
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x000650CF File Offset: 0x000632CF
	public bool IsPermittedByUser(ChoreGroup chore_group)
	{
		return chore_group == null || !this.userDisabledChoreGroups.Contains(chore_group.IdHash);
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x000650EC File Offset: 0x000632EC
	public void SetPermittedByUser(ChoreGroup chore_group, bool is_allowed)
	{
		if (is_allowed)
		{
			if (this.userDisabledChoreGroups.Remove(chore_group.IdHash))
			{
				this.choreRulesChanged.Signal();
				return;
			}
		}
		else if (!this.userDisabledChoreGroups.Contains(chore_group.IdHash))
		{
			this.userDisabledChoreGroups.Add(chore_group.IdHash);
			this.choreRulesChanged.Signal();
		}
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x0006514A File Offset: 0x0006334A
	public bool IsPermittedByTraits(ChoreGroup chore_group)
	{
		return chore_group == null || !this.traitDisabledChoreGroups.Contains(chore_group.IdHash);
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x00065168 File Offset: 0x00063368
	public void SetPermittedByTraits(ChoreGroup chore_group, bool is_enabled)
	{
		if (is_enabled)
		{
			if (this.traitDisabledChoreGroups.Remove(chore_group.IdHash))
			{
				this.choreRulesChanged.Signal();
				return;
			}
		}
		else if (!this.traitDisabledChoreGroups.Contains(chore_group.IdHash))
		{
			this.traitDisabledChoreGroups.Add(chore_group.IdHash);
			this.choreRulesChanged.Signal();
		}
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x000651C8 File Offset: 0x000633C8
	private bool ChooseChore(ref Chore.Precondition.Context out_context, List<Chore.Precondition.Context> succeeded_contexts)
	{
		if (succeeded_contexts.Count == 0)
		{
			return false;
		}
		Chore currentChore = this.choreDriver.GetCurrentChore();
		if (currentChore == null)
		{
			for (int i = succeeded_contexts.Count - 1; i >= 0; i--)
			{
				Chore.Precondition.Context context = succeeded_contexts[i];
				if (context.IsSuccess())
				{
					out_context = context;
					return true;
				}
			}
		}
		else
		{
			int interruptPriority = Db.Get().ChoreTypes.TopPriority.interruptPriority;
			int num = ((currentChore.masterPriority.priority_class == PriorityScreen.PriorityClass.topPriority) ? interruptPriority : currentChore.choreType.interruptPriority);
			for (int j = succeeded_contexts.Count - 1; j >= 0; j--)
			{
				Chore.Precondition.Context context2 = succeeded_contexts[j];
				if (context2.IsSuccess() && ((context2.masterPriority.priority_class == PriorityScreen.PriorityClass.topPriority) ? interruptPriority : context2.interruptPriority) > num && !currentChore.choreType.interruptExclusion.Overlaps(context2.chore.choreType.tags))
				{
					out_context = context2;
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x000652C8 File Offset: 0x000634C8
	public bool FindNextChore(ref Chore.Precondition.Context out_context)
	{
		if (this.debug)
		{
			int num = 0 + 1;
		}
		this.preconditionSnapshot.Clear();
		this.consumerState.Refresh();
		if (this.consumerState.hasSolidTransferArm)
		{
			global::Debug.Assert(this.stationaryReach > 0);
			CellOffset offset = Grid.GetOffset(Grid.PosToCell(this));
			Extents extents = new Extents(offset.x, offset.y, this.stationaryReach);
			ListPool<ScenePartitionerEntry, ChoreConsumer>.PooledList pooledList = ListPool<ScenePartitionerEntry, ChoreConsumer>.Allocate();
			GameScenePartitioner.Instance.GatherEntries(extents, GameScenePartitioner.Instance.fetchChoreLayer, pooledList);
			foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
			{
				if (scenePartitionerEntry.obj == null)
				{
					DebugUtil.Assert(false, "FindNextChore found an entry that was null");
				}
				else
				{
					FetchChore fetchChore = scenePartitionerEntry.obj as FetchChore;
					if (fetchChore == null)
					{
						DebugUtil.Assert(false, "FindNextChore found an entry that wasn't a FetchChore");
					}
					else if (fetchChore.target == null)
					{
						DebugUtil.Assert(false, "FindNextChore found an entry with a null target");
					}
					else if (fetchChore.isNull)
					{
						global::Debug.LogWarning("FindNextChore found an entry that isNull");
					}
					else
					{
						int num2 = Grid.PosToCell(fetchChore.gameObject);
						if (this.consumerState.solidTransferArm.IsCellReachable(num2))
						{
							fetchChore.CollectChoresFromGlobalChoreProvider(this.consumerState, this.preconditionSnapshot.succeededContexts, this.preconditionSnapshot.failedContexts, false);
						}
					}
				}
			}
			pooledList.Recycle();
		}
		else
		{
			for (int i = 0; i < this.providers.Count; i++)
			{
				this.providers[i].CollectChores(this.consumerState, this.preconditionSnapshot.succeededContexts, this.preconditionSnapshot.failedContexts);
			}
		}
		this.preconditionSnapshot.succeededContexts.Sort();
		List<Chore.Precondition.Context> succeededContexts = this.preconditionSnapshot.succeededContexts;
		bool flag = this.ChooseChore(ref out_context, succeededContexts);
		if (flag)
		{
			this.preconditionSnapshot.CopyTo(this.lastSuccessfulPreconditionSnapshot);
		}
		return flag;
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x000654CC File Offset: 0x000636CC
	public void AddProvider(ChoreProvider provider)
	{
		DebugUtil.Assert(provider != null);
		this.providers.Add(provider);
	}

	// Token: 0x06001310 RID: 4880 RVA: 0x000654E6 File Offset: 0x000636E6
	public void RemoveProvider(ChoreProvider provider)
	{
		this.providers.Remove(provider);
	}

	// Token: 0x06001311 RID: 4881 RVA: 0x000654F5 File Offset: 0x000636F5
	public void AddUrge(Urge urge)
	{
		DebugUtil.Assert(urge != null);
		this.urges.Add(urge);
		base.Trigger(-736698276, urge);
	}

	// Token: 0x06001312 RID: 4882 RVA: 0x00065518 File Offset: 0x00063718
	public void RemoveUrge(Urge urge)
	{
		this.urges.Remove(urge);
		base.Trigger(231622047, urge);
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x00065533 File Offset: 0x00063733
	public bool HasUrge(Urge urge)
	{
		return this.urges.Contains(urge);
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x00065541 File Offset: 0x00063741
	public List<Urge> GetUrges()
	{
		return this.urges;
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x00065549 File Offset: 0x00063749
	[Conditional("ENABLE_LOGGER")]
	public void Log(string evt, string param)
	{
	}

	// Token: 0x06001316 RID: 4886 RVA: 0x0006554C File Offset: 0x0006374C
	public bool IsPermittedOrEnabled(ChoreType chore_type, Chore chore)
	{
		if (chore_type.groups.Length == 0)
		{
			return true;
		}
		bool flag = false;
		bool flag2 = true;
		for (int i = 0; i < chore_type.groups.Length; i++)
		{
			ChoreGroup choreGroup = chore_type.groups[i];
			if (!this.IsPermittedByTraits(choreGroup))
			{
				flag2 = false;
			}
			if (this.IsPermittedByUser(choreGroup))
			{
				flag = true;
			}
		}
		return flag && flag2;
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x0006559D File Offset: 0x0006379D
	public void SetReach(int reach)
	{
		this.stationaryReach = reach;
	}

	// Token: 0x06001318 RID: 4888 RVA: 0x000655A8 File Offset: 0x000637A8
	public bool GetNavigationCost(IApproachable approachable, out int cost)
	{
		if (this.navigator)
		{
			cost = this.navigator.GetNavigationCost(approachable);
			if (cost != -1)
			{
				return true;
			}
		}
		else if (this.consumerState.hasSolidTransferArm)
		{
			int cell = approachable.GetCell();
			if (this.consumerState.solidTransferArm.IsCellReachable(cell))
			{
				cost = Grid.GetCellRange(this.NaturalBuildingCell(), cell);
				return true;
			}
		}
		cost = 0;
		return false;
	}

	// Token: 0x06001319 RID: 4889 RVA: 0x00065614 File Offset: 0x00063814
	public bool GetNavigationCost(int cell, out int cost)
	{
		if (this.navigator)
		{
			cost = this.navigator.GetNavigationCost(cell);
			if (cost != -1)
			{
				return true;
			}
		}
		else if (this.consumerState.hasSolidTransferArm && this.consumerState.solidTransferArm.IsCellReachable(cell))
		{
			cost = Grid.GetCellRange(this.NaturalBuildingCell(), cell);
			return true;
		}
		cost = 0;
		return false;
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x00065678 File Offset: 0x00063878
	public bool CanReach(IApproachable approachable)
	{
		if (this.navigator)
		{
			return this.navigator.CanReach(approachable);
		}
		if (this.consumerState.hasSolidTransferArm)
		{
			int cell = approachable.GetCell();
			return this.consumerState.solidTransferArm.IsCellReachable(cell);
		}
		return false;
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x000656C8 File Offset: 0x000638C8
	public bool IsWithinReach(IApproachable approachable)
	{
		if (this.navigator)
		{
			return !(this == null) && !(base.gameObject == null) && Grid.IsCellOffsetOf(Grid.PosToCell(this), approachable.GetCell(), approachable.GetOffsets());
		}
		return this.consumerState.hasSolidTransferArm && this.consumerState.solidTransferArm.IsCellReachable(approachable.GetCell());
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x00065738 File Offset: 0x00063938
	public void ShowHoverTextOnHoveredItem(Chore.Precondition.Context context, KSelectable hover_obj, HoverTextDrawer drawer, SelectToolHoverTextCard hover_text_card)
	{
		if (context.chore.target.isNull || context.chore.target.gameObject != hover_obj.gameObject)
		{
			return;
		}
		drawer.NewLine(26);
		drawer.AddIndent(36);
		drawer.DrawText(context.chore.choreType.Name, hover_text_card.Styles_BodyText.Standard);
		if (!context.IsSuccess())
		{
			Chore.PreconditionInstance preconditionInstance = context.chore.GetPreconditions()[context.failedPreconditionId];
			string text = preconditionInstance.description;
			if (string.IsNullOrEmpty(text))
			{
				text = preconditionInstance.id;
			}
			if (context.chore.driver != null)
			{
				text = text.Replace("{Assignee}", context.chore.driver.GetProperName());
			}
			text = text.Replace("{Selected}", this.GetProperName());
			drawer.DrawText(" (" + text + ")", hover_text_card.Styles_BodyText.Standard);
		}
	}

	// Token: 0x0600131D RID: 4893 RVA: 0x00065844 File Offset: 0x00063A44
	public void ShowHoverTextOnHoveredItem(KSelectable hover_obj, HoverTextDrawer drawer, SelectToolHoverTextCard hover_text_card)
	{
		bool flag = false;
		foreach (Chore.Precondition.Context context in this.preconditionSnapshot.succeededContexts)
		{
			if (context.chore.showAvailabilityInHoverText && !context.chore.target.isNull && !(context.chore.target.gameObject != hover_obj.gameObject))
			{
				if (!flag)
				{
					drawer.NewLine(26);
					drawer.DrawText(DUPLICANTS.CHORES.PRECONDITIONS.HEADER.ToString().Replace("{Selected}", this.GetProperName()), hover_text_card.Styles_BodyText.Standard);
					flag = true;
				}
				this.ShowHoverTextOnHoveredItem(context, hover_obj, drawer, hover_text_card);
			}
		}
		foreach (Chore.Precondition.Context context2 in this.preconditionSnapshot.failedContexts)
		{
			if (context2.chore.showAvailabilityInHoverText && !context2.chore.target.isNull && !(context2.chore.target.gameObject != hover_obj.gameObject))
			{
				if (!flag)
				{
					drawer.NewLine(26);
					drawer.DrawText(DUPLICANTS.CHORES.PRECONDITIONS.HEADER.ToString().Replace("{Selected}", this.GetProperName()), hover_text_card.Styles_BodyText.Standard);
					flag = true;
				}
				this.ShowHoverTextOnHoveredItem(context2, hover_obj, drawer, hover_text_card);
			}
		}
	}

	// Token: 0x0600131E RID: 4894 RVA: 0x000659E0 File Offset: 0x00063BE0
	public int GetPersonalPriority(ChoreType chore_type)
	{
		int num;
		if (!this.choreTypePriorities.TryGetValue(chore_type.IdHash, out num))
		{
			num = 3;
		}
		num = Mathf.Clamp(num, 0, 5);
		return num;
	}

	// Token: 0x0600131F RID: 4895 RVA: 0x00065A10 File Offset: 0x00063C10
	public int GetPersonalPriority(ChoreGroup group)
	{
		int num = 3;
		ChoreConsumer.PriorityInfo priorityInfo;
		if (this.choreGroupPriorities.TryGetValue(group.IdHash, out priorityInfo))
		{
			num = priorityInfo.priority;
		}
		return Mathf.Clamp(num, 0, 5);
	}

	// Token: 0x06001320 RID: 4896 RVA: 0x00065A48 File Offset: 0x00063C48
	public void SetPersonalPriority(ChoreGroup group, int value)
	{
		if (group.choreTypes == null)
		{
			return;
		}
		value = Mathf.Clamp(value, 0, 5);
		ChoreConsumer.PriorityInfo priorityInfo;
		if (!this.choreGroupPriorities.TryGetValue(group.IdHash, out priorityInfo))
		{
			priorityInfo.priority = 3;
		}
		this.choreGroupPriorities[group.IdHash] = new ChoreConsumer.PriorityInfo
		{
			priority = value
		};
		this.UpdateChoreTypePriorities(group, value);
		this.SetPermittedByUser(group, value != 0);
	}

	// Token: 0x06001321 RID: 4897 RVA: 0x00065ABA File Offset: 0x00063CBA
	public int GetAssociatedSkillLevel(ChoreGroup group)
	{
		return (int)this.GetAttributes().GetValue(group.attribute.Id);
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x00065AD4 File Offset: 0x00063CD4
	private void UpdateChoreTypePriorities(ChoreGroup group, int value)
	{
		ChoreGroups choreGroups = Db.Get().ChoreGroups;
		foreach (ChoreType choreType in group.choreTypes)
		{
			int num = 0;
			foreach (ChoreGroup choreGroup in choreGroups.resources)
			{
				if (choreGroup.choreTypes != null)
				{
					using (List<ChoreType>.Enumerator enumerator3 = choreGroup.choreTypes.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							if (enumerator3.Current.IdHash == choreType.IdHash)
							{
								int personalPriority = this.GetPersonalPriority(choreGroup);
								num = Mathf.Max(num, personalPriority);
							}
						}
					}
				}
			}
			this.choreTypePriorities[choreType.IdHash] = num;
		}
	}

	// Token: 0x06001323 RID: 4899 RVA: 0x00065BEC File Offset: 0x00063DEC
	public void ResetPersonalPriorities()
	{
	}

	// Token: 0x06001324 RID: 4900 RVA: 0x00065BF0 File Offset: 0x00063DF0
	public bool RunBehaviourPrecondition(Tag tag)
	{
		ChoreConsumer.BehaviourPrecondition behaviourPrecondition = default(ChoreConsumer.BehaviourPrecondition);
		return this.behaviourPreconditions.TryGetValue(tag, out behaviourPrecondition) && behaviourPrecondition.cb(behaviourPrecondition.arg);
	}

	// Token: 0x06001325 RID: 4901 RVA: 0x00065C28 File Offset: 0x00063E28
	public void AddBehaviourPrecondition(Tag tag, Func<object, bool> precondition, object arg)
	{
		DebugUtil.Assert(!this.behaviourPreconditions.ContainsKey(tag));
		this.behaviourPreconditions[tag] = new ChoreConsumer.BehaviourPrecondition
		{
			cb = precondition,
			arg = arg
		};
	}

	// Token: 0x06001326 RID: 4902 RVA: 0x00065C6E File Offset: 0x00063E6E
	public void RemoveBehaviourPrecondition(Tag tag, Func<object, bool> precondition, object arg)
	{
		this.behaviourPreconditions.Remove(tag);
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x00065C80 File Offset: 0x00063E80
	public bool IsChoreEqualOrAboveCurrentChorePriority<StateMachineType>()
	{
		Chore currentChore = this.choreDriver.GetCurrentChore();
		return currentChore == null || currentChore.choreType.priority <= this.choreTable.GetChorePriority<StateMachineType>(this);
	}

	// Token: 0x06001328 RID: 4904 RVA: 0x00065CBC File Offset: 0x00063EBC
	public bool IsChoreGroupDisabled(ChoreGroup chore_group)
	{
		bool flag = false;
		Traits component = base.gameObject.GetComponent<Traits>();
		if (component != null && component.IsChoreGroupDisabled(chore_group))
		{
			flag = true;
		}
		return flag;
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x00065CEC File Offset: 0x00063EEC
	public Dictionary<HashedString, ChoreConsumer.PriorityInfo> GetChoreGroupPriorities()
	{
		return this.choreGroupPriorities;
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x00065CF4 File Offset: 0x00063EF4
	public void SetChoreGroupPriorities(Dictionary<HashedString, ChoreConsumer.PriorityInfo> priorities)
	{
		this.choreGroupPriorities = priorities;
	}

	// Token: 0x04000A4C RID: 2636
	public const int DEFAULT_PERSONAL_CHORE_PRIORITY = 3;

	// Token: 0x04000A4D RID: 2637
	public const int MIN_PERSONAL_PRIORITY = 0;

	// Token: 0x04000A4E RID: 2638
	public const int MAX_PERSONAL_PRIORITY = 5;

	// Token: 0x04000A4F RID: 2639
	public const int PRIORITY_DISABLED = 0;

	// Token: 0x04000A50 RID: 2640
	public const int PRIORITY_VERYLOW = 1;

	// Token: 0x04000A51 RID: 2641
	public const int PRIORITY_LOW = 2;

	// Token: 0x04000A52 RID: 2642
	public const int PRIORITY_FLAT = 3;

	// Token: 0x04000A53 RID: 2643
	public const int PRIORITY_HIGH = 4;

	// Token: 0x04000A54 RID: 2644
	public const int PRIORITY_VERYHIGH = 5;

	// Token: 0x04000A55 RID: 2645
	[MyCmpAdd]
	public ChoreProvider choreProvider;

	// Token: 0x04000A56 RID: 2646
	[MyCmpAdd]
	public ChoreDriver choreDriver;

	// Token: 0x04000A57 RID: 2647
	[MyCmpGet]
	public Navigator navigator;

	// Token: 0x04000A58 RID: 2648
	[MyCmpGet]
	public MinionResume resume;

	// Token: 0x04000A59 RID: 2649
	[MyCmpAdd]
	private User user;

	// Token: 0x04000A5A RID: 2650
	public System.Action choreRulesChanged;

	// Token: 0x04000A5B RID: 2651
	public bool debug;

	// Token: 0x04000A5C RID: 2652
	private List<ChoreProvider> providers = new List<ChoreProvider>();

	// Token: 0x04000A5D RID: 2653
	private List<Urge> urges = new List<Urge>();

	// Token: 0x04000A5E RID: 2654
	public ChoreTable choreTable;

	// Token: 0x04000A5F RID: 2655
	private ChoreTable.Instance choreTableInstance;

	// Token: 0x04000A60 RID: 2656
	public ChoreConsumerState consumerState;

	// Token: 0x04000A61 RID: 2657
	private Dictionary<Tag, ChoreConsumer.BehaviourPrecondition> behaviourPreconditions = new Dictionary<Tag, ChoreConsumer.BehaviourPrecondition>();

	// Token: 0x04000A62 RID: 2658
	private ChoreConsumer.PreconditionSnapshot preconditionSnapshot = new ChoreConsumer.PreconditionSnapshot();

	// Token: 0x04000A63 RID: 2659
	private ChoreConsumer.PreconditionSnapshot lastSuccessfulPreconditionSnapshot = new ChoreConsumer.PreconditionSnapshot();

	// Token: 0x04000A64 RID: 2660
	[Serialize]
	private Dictionary<HashedString, ChoreConsumer.PriorityInfo> choreGroupPriorities = new Dictionary<HashedString, ChoreConsumer.PriorityInfo>();

	// Token: 0x04000A65 RID: 2661
	private Dictionary<HashedString, int> choreTypePriorities = new Dictionary<HashedString, int>();

	// Token: 0x04000A66 RID: 2662
	private List<HashedString> traitDisabledChoreGroups = new List<HashedString>();

	// Token: 0x04000A67 RID: 2663
	private List<HashedString> userDisabledChoreGroups = new List<HashedString>();

	// Token: 0x04000A68 RID: 2664
	private int stationaryReach = -1;

	// Token: 0x02000FAD RID: 4013
	private struct BehaviourPrecondition
	{
		// Token: 0x04005531 RID: 21809
		public Func<object, bool> cb;

		// Token: 0x04005532 RID: 21810
		public object arg;
	}

	// Token: 0x02000FAE RID: 4014
	public class PreconditionSnapshot
	{
		// Token: 0x06007031 RID: 28721 RVA: 0x002A6331 File Offset: 0x002A4531
		public void CopyTo(ChoreConsumer.PreconditionSnapshot snapshot)
		{
			snapshot.Clear();
			snapshot.succeededContexts.AddRange(this.succeededContexts);
			snapshot.failedContexts.AddRange(this.failedContexts);
			snapshot.doFailedContextsNeedSorting = true;
		}

		// Token: 0x06007032 RID: 28722 RVA: 0x002A6362 File Offset: 0x002A4562
		public void Clear()
		{
			this.succeededContexts.Clear();
			this.failedContexts.Clear();
			this.doFailedContextsNeedSorting = true;
		}

		// Token: 0x04005533 RID: 21811
		public List<Chore.Precondition.Context> succeededContexts = new List<Chore.Precondition.Context>();

		// Token: 0x04005534 RID: 21812
		public List<Chore.Precondition.Context> failedContexts = new List<Chore.Precondition.Context>();

		// Token: 0x04005535 RID: 21813
		public bool doFailedContextsNeedSorting = true;
	}

	// Token: 0x02000FAF RID: 4015
	public struct PriorityInfo
	{
		// Token: 0x04005536 RID: 21814
		public int priority;
	}
}
