using System;
using System.Collections.Generic;
using System.Linq;
using STRINGS;
using UnityEngine;

// Token: 0x02000383 RID: 899
public class FetchChore : Chore<FetchChore.StatesInstance>
{
	// Token: 0x17000060 RID: 96
	// (get) Token: 0x0600124E RID: 4686 RVA: 0x00061C54 File Offset: 0x0005FE54
	public float originalAmount
	{
		get
		{
			return base.smi.sm.requestedamount.Get(base.smi);
		}
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x0600124F RID: 4687 RVA: 0x00061C71 File Offset: 0x0005FE71
	// (set) Token: 0x06001250 RID: 4688 RVA: 0x00061C8E File Offset: 0x0005FE8E
	public float amount
	{
		get
		{
			return base.smi.sm.actualamount.Get(base.smi);
		}
		set
		{
			base.smi.sm.actualamount.Set(value, base.smi, false);
		}
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x06001251 RID: 4689 RVA: 0x00061CAE File Offset: 0x0005FEAE
	// (set) Token: 0x06001252 RID: 4690 RVA: 0x00061CCB File Offset: 0x0005FECB
	public Pickupable fetchTarget
	{
		get
		{
			return base.smi.sm.chunk.Get<Pickupable>(base.smi);
		}
		set
		{
			base.smi.sm.chunk.Set(value, base.smi);
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06001253 RID: 4691 RVA: 0x00061CE9 File Offset: 0x0005FEE9
	// (set) Token: 0x06001254 RID: 4692 RVA: 0x00061D06 File Offset: 0x0005FF06
	public GameObject fetcher
	{
		get
		{
			return base.smi.sm.fetcher.Get(base.smi);
		}
		set
		{
			base.smi.sm.fetcher.Set(value, base.smi, false);
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06001255 RID: 4693 RVA: 0x00061D26 File Offset: 0x0005FF26
	// (set) Token: 0x06001256 RID: 4694 RVA: 0x00061D2E File Offset: 0x0005FF2E
	public Storage destination { get; private set; }

	// Token: 0x06001257 RID: 4695 RVA: 0x00061D38 File Offset: 0x0005FF38
	public void FetchAreaBegin(Chore.Precondition.Context context, float amount_to_be_fetched)
	{
		this.amount = amount_to_be_fetched;
		base.smi.sm.fetcher.Set(context.consumerState.gameObject, base.smi, false);
		ReportManager.Instance.ReportValue(ReportManager.ReportType.ChoreStatus, 1f, context.chore.choreType.Name, GameUtil.GetChoreName(this, context.data));
		base.Begin(context);
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x00061DA8 File Offset: 0x0005FFA8
	public void FetchAreaEnd(ChoreDriver driver, Pickupable pickupable, bool is_success)
	{
		if (is_success)
		{
			ReportManager.Instance.ReportValue(ReportManager.ReportType.ChoreStatus, -1f, base.choreType.Name, GameUtil.GetChoreName(this, pickupable));
			this.fetchTarget = pickupable;
			base.driver = driver;
			this.fetcher = driver.gameObject;
			base.Succeed("FetchAreaEnd");
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().LogFetchChore(this.fetcher, base.choreType);
			return;
		}
		base.SetOverrideTarget(null);
		this.Fail("FetchAreaFail");
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x00061E30 File Offset: 0x00060030
	public Pickupable FindFetchTarget(ChoreConsumerState consumer_state)
	{
		if (!(this.destination != null))
		{
			return null;
		}
		if (consumer_state.hasSolidTransferArm)
		{
			return consumer_state.solidTransferArm.FindFetchTarget(this.destination, this);
		}
		return Game.Instance.fetchManager.FindFetchTarget(this.destination, this);
	}

	// Token: 0x0600125A RID: 4698 RVA: 0x00061E80 File Offset: 0x00060080
	public override void Begin(Chore.Precondition.Context context)
	{
		Pickupable pickupable = (Pickupable)context.data;
		if (pickupable == null)
		{
			pickupable = this.FindFetchTarget(context.consumerState);
		}
		base.smi.sm.source.Set(pickupable.gameObject, base.smi, false);
		pickupable.Subscribe(-1582839653, new Action<object>(this.OnTagsChanged));
		base.Begin(context);
	}

	// Token: 0x0600125B RID: 4699 RVA: 0x00061EF4 File Offset: 0x000600F4
	protected override void End(string reason)
	{
		Pickupable pickupable = base.smi.sm.source.Get<Pickupable>(base.smi);
		if (pickupable != null)
		{
			pickupable.Unsubscribe(-1582839653, new Action<object>(this.OnTagsChanged));
		}
		base.End(reason);
	}

	// Token: 0x0600125C RID: 4700 RVA: 0x00061F44 File Offset: 0x00060144
	private void OnTagsChanged(object data)
	{
		if (base.smi.sm.chunk.Get(base.smi) != null)
		{
			this.Fail("Tags changed");
		}
	}

	// Token: 0x0600125D RID: 4701 RVA: 0x00061F74 File Offset: 0x00060174
	public override void PrepareChore(ref Chore.Precondition.Context context)
	{
		context.chore = new FetchAreaChore(context);
	}

	// Token: 0x0600125E RID: 4702 RVA: 0x00061F87 File Offset: 0x00060187
	public float AmountWaitingToFetch()
	{
		if (this.fetcher == null)
		{
			return this.originalAmount;
		}
		return this.amount;
	}

	// Token: 0x0600125F RID: 4703 RVA: 0x00061FA4 File Offset: 0x000601A4
	public FetchChore(ChoreType choreType, Storage destination, float amount, HashSet<Tag> tags, FetchChore.MatchCriteria criteria, Tag required_tag, Tag[] forbidden_tags = null, ChoreProvider chore_provider = null, bool run_until_complete = true, Action<Chore> on_complete = null, Action<Chore> on_begin = null, Action<Chore> on_end = null, Operational.State operational_requirement = Operational.State.Operational, int priority_mod = 0)
		: base(choreType, destination, chore_provider, run_until_complete, on_complete, on_begin, on_end, PriorityScreen.PriorityClass.basic, 5, false, true, priority_mod, false, ReportManager.ReportType.WorkTime)
	{
		if (choreType == null)
		{
			global::Debug.LogError("You must specify a chore type for fetching!");
		}
		this.tagsFirst = ((tags.Count > 0) ? tags.First<Tag>() : Tag.Invalid);
		if (amount <= PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT)
		{
			DebugUtil.LogWarningArgs(new object[] { string.Format("Chore {0} is requesting {1} {2} to {3}", new object[]
			{
				choreType.Id,
				this.tagsFirst,
				amount,
				(destination != null) ? destination.name : "to nowhere"
			}) });
		}
		base.SetPrioritizable((destination.prioritizable != null) ? destination.prioritizable : destination.GetComponent<Prioritizable>());
		base.smi = new FetchChore.StatesInstance(this);
		base.smi.sm.requestedamount.Set(amount, base.smi, false);
		this.destination = destination;
		DebugUtil.DevAssert(criteria != FetchChore.MatchCriteria.MatchTags || tags.Count <= 1, "For performance reasons fetch chores are limited to one tag when matching tags!", null);
		this.tags = tags;
		this.criteria = criteria;
		this.tagsHash = FetchChore.ComputeHashCodeForTags(tags);
		this.requiredTag = required_tag;
		this.forbiddenTags = ((forbidden_tags != null) ? forbidden_tags : new Tag[0]);
		this.forbidHash = FetchChore.ComputeHashCodeForTags(this.forbiddenTags);
		DebugUtil.DevAssert(!tags.Contains(GameTags.Preserved), "Fetch chore fetching invalid tags.", null);
		if (destination.GetOnlyFetchMarkedItems())
		{
			DebugUtil.DevAssert(!this.requiredTag.IsValid, "Only one requiredTag is supported at a time, this will stomp!", null);
			this.requiredTag = GameTags.Garbage;
		}
		base.AddPrecondition(ChorePreconditions.instance.IsScheduledTime, Db.Get().ScheduleBlockTypes.Work);
		base.AddPrecondition(ChorePreconditions.instance.CanMoveTo, destination);
		base.AddPrecondition(FetchChore.IsFetchTargetAvailable, null);
		Deconstructable component = base.target.GetComponent<Deconstructable>();
		if (component != null)
		{
			base.AddPrecondition(ChorePreconditions.instance.IsNotMarkedForDeconstruction, component);
		}
		BuildingEnabledButton component2 = base.target.GetComponent<BuildingEnabledButton>();
		if (component2 != null)
		{
			base.AddPrecondition(ChorePreconditions.instance.IsNotMarkedForDisable, component2);
		}
		if (operational_requirement != Operational.State.None)
		{
			Operational component3 = destination.GetComponent<Operational>();
			if (component3 != null)
			{
				Chore.Precondition precondition = ChorePreconditions.instance.IsOperational;
				if (operational_requirement == Operational.State.Functional)
				{
					precondition = ChorePreconditions.instance.IsFunctional;
				}
				base.AddPrecondition(precondition, component3);
			}
		}
		this.partitionerEntry = GameScenePartitioner.Instance.Add(destination.name, this, Grid.PosToCell(destination), GameScenePartitioner.Instance.fetchChoreLayer, null);
		destination.Subscribe(644822890, new Action<object>(this.OnOnlyFetchMarkedItemsSettingChanged));
		this.automatable = destination.GetComponent<Automatable>();
		if (this.automatable)
		{
			base.AddPrecondition(ChorePreconditions.instance.IsAllowedByAutomation, this.automatable);
		}
	}

	// Token: 0x06001260 RID: 4704 RVA: 0x00062290 File Offset: 0x00060490
	private void OnOnlyFetchMarkedItemsSettingChanged(object data)
	{
		if (this.destination != null)
		{
			if (this.destination.GetOnlyFetchMarkedItems())
			{
				DebugUtil.DevAssert(!this.requiredTag.IsValid, "Only one requiredTag is supported at a time, this will stomp!", null);
				this.requiredTag = GameTags.Garbage;
				return;
			}
			this.requiredTag = Tag.Invalid;
		}
	}

	// Token: 0x06001261 RID: 4705 RVA: 0x000622E8 File Offset: 0x000604E8
	private void OnMasterPriorityChanged(PriorityScreen.PriorityClass priorityClass, int priority_value)
	{
		this.masterPriority.priority_class = priorityClass;
		this.masterPriority.priority_value = priority_value;
	}

	// Token: 0x06001262 RID: 4706 RVA: 0x00062302 File Offset: 0x00060502
	public override void CollectChores(ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded_contexts, List<Chore.Precondition.Context> failed_contexts, bool is_attempting_override)
	{
	}

	// Token: 0x06001263 RID: 4707 RVA: 0x00062304 File Offset: 0x00060504
	public void CollectChoresFromGlobalChoreProvider(ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded_contexts, List<Chore.Precondition.Context> failed_contexts, bool is_attempting_override)
	{
		base.CollectChores(consumer_state, succeeded_contexts, failed_contexts, is_attempting_override);
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x00062314 File Offset: 0x00060514
	public override void Cleanup()
	{
		base.Cleanup();
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		if (this.destination != null)
		{
			this.destination.Unsubscribe(644822890, new Action<object>(this.OnOnlyFetchMarkedItemsSettingChanged));
		}
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x00062364 File Offset: 0x00060564
	public static int ComputeHashCodeForTags(IEnumerable<Tag> tags)
	{
		int num = 123137;
		foreach (Tag tag in new SortedSet<Tag>(tags))
		{
			num = ((num << 5) + num) ^ tag.GetHash();
		}
		return num;
	}

	// Token: 0x040009EC RID: 2540
	public HashSet<Tag> tags;

	// Token: 0x040009ED RID: 2541
	public Tag tagsFirst;

	// Token: 0x040009EE RID: 2542
	public FetchChore.MatchCriteria criteria;

	// Token: 0x040009EF RID: 2543
	public int tagsHash;

	// Token: 0x040009F0 RID: 2544
	public Tag requiredTag;

	// Token: 0x040009F1 RID: 2545
	public Tag[] forbiddenTags;

	// Token: 0x040009F2 RID: 2546
	public int forbidHash;

	// Token: 0x040009F3 RID: 2547
	public Automatable automatable;

	// Token: 0x040009F4 RID: 2548
	public bool allowMultifetch = true;

	// Token: 0x040009F5 RID: 2549
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x040009F6 RID: 2550
	public static readonly Chore.Precondition IsFetchTargetAvailable = new Chore.Precondition
	{
		id = "IsFetchTargetAvailable",
		description = DUPLICANTS.CHORES.PRECONDITIONS.IS_FETCH_TARGET_AVAILABLE,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			FetchChore fetchChore = (FetchChore)context.chore;
			Pickupable pickupable = (Pickupable)context.data;
			bool flag;
			if (pickupable == null)
			{
				pickupable = fetchChore.FindFetchTarget(context.consumerState);
				flag = pickupable != null;
			}
			else
			{
				flag = FetchManager.IsFetchablePickup(pickupable, fetchChore, context.consumerState.storage);
			}
			if (flag)
			{
				if (pickupable == null)
				{
					global::Debug.Log(string.Format("Failed to find fetch target for {0}", fetchChore.destination));
					return false;
				}
				context.data = pickupable;
				int num;
				if (context.consumerState.consumer.GetNavigationCost(pickupable, out num))
				{
					context.cost += num;
					return true;
				}
			}
			return false;
		}
	};

	// Token: 0x02000F65 RID: 3941
	public enum MatchCriteria
	{
		// Token: 0x0400545E RID: 21598
		MatchID,
		// Token: 0x0400545F RID: 21599
		MatchTags
	}

	// Token: 0x02000F66 RID: 3942
	public class StatesInstance : GameStateMachine<FetchChore.States, FetchChore.StatesInstance, FetchChore, object>.GameInstance
	{
		// Token: 0x06006F67 RID: 28519 RVA: 0x002A1AA0 File Offset: 0x0029FCA0
		public StatesInstance(FetchChore master)
			: base(master)
		{
		}
	}

	// Token: 0x02000F67 RID: 3943
	public class States : GameStateMachine<FetchChore.States, FetchChore.StatesInstance, FetchChore>
	{
		// Token: 0x06006F68 RID: 28520 RVA: 0x002A1AA9 File Offset: 0x0029FCA9
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
		}

		// Token: 0x04005460 RID: 21600
		public StateMachine<FetchChore.States, FetchChore.StatesInstance, FetchChore, object>.TargetParameter fetcher;

		// Token: 0x04005461 RID: 21601
		public StateMachine<FetchChore.States, FetchChore.StatesInstance, FetchChore, object>.TargetParameter source;

		// Token: 0x04005462 RID: 21602
		public StateMachine<FetchChore.States, FetchChore.StatesInstance, FetchChore, object>.TargetParameter chunk;

		// Token: 0x04005463 RID: 21603
		public StateMachine<FetchChore.States, FetchChore.StatesInstance, FetchChore, object>.FloatParameter requestedamount;

		// Token: 0x04005464 RID: 21604
		public StateMachine<FetchChore.States, FetchChore.StatesInstance, FetchChore, object>.FloatParameter actualamount;
	}
}
