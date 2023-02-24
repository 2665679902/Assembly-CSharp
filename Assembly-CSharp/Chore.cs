using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020003A0 RID: 928
public abstract class Chore
{
	// Token: 0x17000066 RID: 102
	// (get) Token: 0x060012AE RID: 4782 RVA: 0x00064244 File Offset: 0x00062444
	// (set) Token: 0x060012AF RID: 4783 RVA: 0x0006424C File Offset: 0x0006244C
	public int id { get; private set; }

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x060012B0 RID: 4784 RVA: 0x00064255 File Offset: 0x00062455
	// (set) Token: 0x060012B1 RID: 4785 RVA: 0x0006425D File Offset: 0x0006245D
	public ChoreDriver driver { get; set; }

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x060012B2 RID: 4786 RVA: 0x00064266 File Offset: 0x00062466
	// (set) Token: 0x060012B3 RID: 4787 RVA: 0x0006426E File Offset: 0x0006246E
	public ChoreDriver lastDriver { get; set; }

	// Token: 0x060012B4 RID: 4788
	protected abstract StateMachine.Instance GetSMI();

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00064277 File Offset: 0x00062477
	// (set) Token: 0x060012B6 RID: 4790 RVA: 0x0006427F File Offset: 0x0006247F
	public ChoreType choreType { get; set; }

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x060012B7 RID: 4791 RVA: 0x00064288 File Offset: 0x00062488
	// (set) Token: 0x060012B8 RID: 4792 RVA: 0x00064290 File Offset: 0x00062490
	public ChoreProvider provider { get; set; }

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x060012B9 RID: 4793 RVA: 0x00064299 File Offset: 0x00062499
	// (set) Token: 0x060012BA RID: 4794 RVA: 0x000642A1 File Offset: 0x000624A1
	public ChoreConsumer overrideTarget { get; private set; }

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x060012BB RID: 4795 RVA: 0x000642AA File Offset: 0x000624AA
	// (set) Token: 0x060012BC RID: 4796 RVA: 0x000642B2 File Offset: 0x000624B2
	public bool isComplete { get; protected set; }

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x060012BD RID: 4797 RVA: 0x000642BB File Offset: 0x000624BB
	// (set) Token: 0x060012BE RID: 4798 RVA: 0x000642C3 File Offset: 0x000624C3
	public IStateMachineTarget target { get; protected set; }

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x060012BF RID: 4799 RVA: 0x000642CC File Offset: 0x000624CC
	// (set) Token: 0x060012C0 RID: 4800 RVA: 0x000642D4 File Offset: 0x000624D4
	public bool runUntilComplete { get; set; }

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x060012C1 RID: 4801 RVA: 0x000642DD File Offset: 0x000624DD
	// (set) Token: 0x060012C2 RID: 4802 RVA: 0x000642E5 File Offset: 0x000624E5
	public int priorityMod { get; set; }

	// Token: 0x060012C3 RID: 4803 RVA: 0x000642EE File Offset: 0x000624EE
	public bool InProgress()
	{
		return this.driver != null;
	}

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x060012C4 RID: 4804
	public abstract GameObject gameObject { get; }

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x060012C5 RID: 4805
	public abstract bool isNull { get; }

	// Token: 0x060012C6 RID: 4806 RVA: 0x000642FC File Offset: 0x000624FC
	public bool IsValid()
	{
		return this.provider != null && this.gameObject.GetMyWorldId() != -1;
	}

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0006431F File Offset: 0x0006251F
	// (set) Token: 0x060012C8 RID: 4808 RVA: 0x00064327 File Offset: 0x00062527
	public bool IsPreemptable { get; protected set; }

	// Token: 0x060012C9 RID: 4809 RVA: 0x00064330 File Offset: 0x00062530
	public Chore(ChoreType chore_type, IStateMachineTarget target, ChoreProvider chore_provider, bool run_until_complete, Action<Chore> on_complete, Action<Chore> on_begin, Action<Chore> on_end, PriorityScreen.PriorityClass priority_class, int priority_value, bool is_preemptable, bool allow_in_context_menu, int priority_mod, bool add_to_daily_report, ReportManager.ReportType report_type)
	{
		this.target = target;
		if (priority_value == 2147483647)
		{
			priority_class = PriorityScreen.PriorityClass.topPriority;
			priority_value = 2;
		}
		if (priority_value < 1 || priority_value > 9)
		{
			global::Debug.LogErrorFormat("Priority Value Out Of Range: {0}", new object[] { priority_value });
		}
		this.masterPriority = new PrioritySetting(priority_class, priority_value);
		this.priorityMod = priority_mod;
		this.id = ++Chore.nextId;
		if (chore_provider == null)
		{
			chore_provider = GlobalChoreProvider.Instance;
			DebugUtil.Assert(chore_provider != null);
		}
		this.choreType = chore_type;
		this.runUntilComplete = run_until_complete;
		this.onComplete = on_complete;
		this.onEnd = on_end;
		this.onBegin = on_begin;
		this.IsPreemptable = is_preemptable;
		this.AddPrecondition(ChorePreconditions.instance.IsValid, null);
		this.AddPrecondition(ChorePreconditions.instance.IsPermitted, null);
		this.AddPrecondition(ChorePreconditions.instance.IsPreemptable, null);
		this.AddPrecondition(ChorePreconditions.instance.HasUrge, null);
		this.AddPrecondition(ChorePreconditions.instance.IsMoreSatisfyingEarly, null);
		this.AddPrecondition(ChorePreconditions.instance.IsMoreSatisfyingLate, null);
		this.AddPrecondition(ChorePreconditions.instance.IsOverrideTargetNullOrMe, null);
		chore_provider.AddChore(this);
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x00064483 File Offset: 0x00062683
	public virtual void Cleanup()
	{
		this.ClearPrioritizable();
	}

	// Token: 0x060012CB RID: 4811 RVA: 0x0006448B File Offset: 0x0006268B
	public void SetPriorityMod(int priorityMod)
	{
		this.priorityMod = priorityMod;
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x00064494 File Offset: 0x00062694
	public List<Chore.PreconditionInstance> GetPreconditions()
	{
		if (this.arePreconditionsDirty)
		{
			this.preconditions.Sort((Chore.PreconditionInstance x, Chore.PreconditionInstance y) => x.sortOrder.CompareTo(y.sortOrder));
			this.arePreconditionsDirty = false;
		}
		return this.preconditions;
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x000644E0 File Offset: 0x000626E0
	protected void SetPrioritizable(Prioritizable prioritizable)
	{
		if (prioritizable != null && prioritizable.IsPrioritizable())
		{
			this.prioritizable = prioritizable;
			this.masterPriority = prioritizable.GetMasterPriority();
			prioritizable.onPriorityChanged = (Action<PrioritySetting>)Delegate.Combine(prioritizable.onPriorityChanged, new Action<PrioritySetting>(this.OnMasterPriorityChanged));
		}
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x00064533 File Offset: 0x00062733
	private void ClearPrioritizable()
	{
		if (this.prioritizable != null)
		{
			Prioritizable prioritizable = this.prioritizable;
			prioritizable.onPriorityChanged = (Action<PrioritySetting>)Delegate.Remove(prioritizable.onPriorityChanged, new Action<PrioritySetting>(this.OnMasterPriorityChanged));
		}
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x0006456A File Offset: 0x0006276A
	private void OnMasterPriorityChanged(PrioritySetting priority)
	{
		this.masterPriority = priority;
	}

	// Token: 0x060012D0 RID: 4816 RVA: 0x00064573 File Offset: 0x00062773
	public void SetOverrideTarget(ChoreConsumer chore_consumer)
	{
		if (chore_consumer != null)
		{
			string name = chore_consumer.name;
		}
		this.overrideTarget = chore_consumer;
		this.Fail("New override target");
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x00064598 File Offset: 0x00062798
	public void AddPrecondition(Chore.Precondition precondition, object data = null)
	{
		this.arePreconditionsDirty = true;
		this.preconditions.Add(new Chore.PreconditionInstance
		{
			id = precondition.id,
			description = precondition.description,
			sortOrder = precondition.sortOrder,
			fn = precondition.fn,
			data = data
		});
	}

	// Token: 0x060012D2 RID: 4818 RVA: 0x000645FC File Offset: 0x000627FC
	public virtual void CollectChores(ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded_contexts, List<Chore.Precondition.Context> failed_contexts, bool is_attempting_override)
	{
		Chore.Precondition.Context context = new Chore.Precondition.Context(this, consumer_state, is_attempting_override, null);
		context.RunPreconditions();
		if (context.IsSuccess())
		{
			succeeded_contexts.Add(context);
			return;
		}
		failed_contexts.Add(context);
	}

	// Token: 0x060012D3 RID: 4819 RVA: 0x00064634 File Offset: 0x00062834
	public bool SatisfiesUrge(Urge urge)
	{
		return urge == this.choreType.urge;
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x00064644 File Offset: 0x00062844
	public ReportManager.ReportType GetReportType()
	{
		return this.reportType;
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x0006464C File Offset: 0x0006284C
	public virtual void PrepareChore(ref Chore.Precondition.Context context)
	{
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x0006464E File Offset: 0x0006284E
	public virtual string ResolveString(string str)
	{
		return str;
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x00064654 File Offset: 0x00062854
	public virtual void Begin(Chore.Precondition.Context context)
	{
		if (this.driver != null)
		{
			global::Debug.LogErrorFormat("Chore.Begin driver already set {0} {1} {2}, provider {3}, driver {4} -> {5}", new object[]
			{
				this.id,
				base.GetType(),
				this.choreType.Id,
				this.provider,
				this.driver,
				context.consumerState.choreDriver
			});
		}
		if (this.provider == null)
		{
			global::Debug.LogErrorFormat("Chore.Begin provider is null {0} {1} {2}, provider {3}, driver {4}", new object[]
			{
				this.id,
				base.GetType(),
				this.choreType.Id,
				this.provider,
				this.driver
			});
		}
		this.driver = context.consumerState.choreDriver;
		StateMachine.Instance smi = this.GetSMI();
		smi.OnStop = (Action<string, StateMachine.Status>)Delegate.Combine(smi.OnStop, new Action<string, StateMachine.Status>(this.OnStateMachineStop));
		KSelectable component = this.driver.GetComponent<KSelectable>();
		if (component != null)
		{
			component.SetStatusItem(Db.Get().StatusItemCategories.Main, this.GetStatusItem(), this);
		}
		smi.StartSM();
		if (this.onBegin != null)
		{
			this.onBegin(this);
		}
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x000647A0 File Offset: 0x000629A0
	protected virtual void End(string reason)
	{
		if (this.driver != null)
		{
			KSelectable component = this.driver.GetComponent<KSelectable>();
			if (component != null)
			{
				component.SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
			}
		}
		StateMachine.Instance smi = this.GetSMI();
		smi.OnStop = (Action<string, StateMachine.Status>)Delegate.Remove(smi.OnStop, new Action<string, StateMachine.Status>(this.OnStateMachineStop));
		smi.StopSM(reason);
		if (this.driver == null)
		{
			return;
		}
		this.lastDriver = this.driver;
		this.driver = null;
		if (this.onEnd != null)
		{
			this.onEnd(this);
		}
		if (this.onExit != null)
		{
			this.onExit(this);
		}
		this.driver = null;
	}

	// Token: 0x060012D9 RID: 4825 RVA: 0x00064868 File Offset: 0x00062A68
	protected void Succeed(string reason)
	{
		if (!this.RemoveFromProvider())
		{
			return;
		}
		this.isComplete = true;
		if (this.onComplete != null)
		{
			this.onComplete(this);
		}
		if (this.addToDailyReport)
		{
			ReportManager.Instance.ReportValue(ReportManager.ReportType.ChoreStatus, -1f, this.choreType.Name, GameUtil.GetChoreName(this, null));
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().LogSuitChore((this.driver != null) ? this.driver : this.lastDriver);
		}
		this.End(reason);
		this.Cleanup();
	}

	// Token: 0x060012DA RID: 4826 RVA: 0x000648FB File Offset: 0x00062AFB
	protected virtual StatusItem GetStatusItem()
	{
		return this.choreType.statusItem;
	}

	// Token: 0x060012DB RID: 4827 RVA: 0x00064908 File Offset: 0x00062B08
	public virtual void Fail(string reason)
	{
		if (this.provider == null)
		{
			return;
		}
		if (this.driver == null)
		{
			return;
		}
		if (!this.runUntilComplete)
		{
			this.Cancel(reason);
			return;
		}
		this.End(reason);
	}

	// Token: 0x060012DC RID: 4828 RVA: 0x00064940 File Offset: 0x00062B40
	public void Cancel(string reason)
	{
		if (!this.RemoveFromProvider())
		{
			return;
		}
		if (this.addToDailyReport)
		{
			ReportManager.Instance.ReportValue(ReportManager.ReportType.ChoreStatus, -1f, this.choreType.Name, GameUtil.GetChoreName(this, null));
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().LogSuitChore((this.driver != null) ? this.driver : this.lastDriver);
		}
		this.End(reason);
		this.Cleanup();
	}

	// Token: 0x060012DD RID: 4829 RVA: 0x000649B8 File Offset: 0x00062BB8
	protected virtual void OnStateMachineStop(string reason, StateMachine.Status status)
	{
		if (status == StateMachine.Status.Success)
		{
			this.Succeed(reason);
			return;
		}
		this.Fail(reason);
	}

	// Token: 0x060012DE RID: 4830 RVA: 0x000649CD File Offset: 0x00062BCD
	private bool RemoveFromProvider()
	{
		if (this.provider != null)
		{
			this.provider.RemoveChore(this);
			return true;
		}
		return false;
	}

	// Token: 0x060012DF RID: 4831 RVA: 0x000649EC File Offset: 0x00062BEC
	public virtual bool CanPreempt(Chore.Precondition.Context context)
	{
		return this.IsPreemptable;
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x000649F4 File Offset: 0x00062BF4
	protected virtual void ShowCustomEditor(string filter, int width)
	{
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x000649F6 File Offset: 0x00062BF6
	public virtual string GetReportName(string context = null)
	{
		if (context == null || this.choreType.reportName == null)
		{
			return this.choreType.Name;
		}
		return string.Format(this.choreType.reportName, context);
	}

	// Token: 0x04000A11 RID: 2577
	private static int nextId;

	// Token: 0x04000A18 RID: 2584
	public bool isExpanded;

	// Token: 0x04000A1A RID: 2586
	public bool showAvailabilityInHoverText = true;

	// Token: 0x04000A1D RID: 2589
	public PrioritySetting masterPriority;

	// Token: 0x04000A1F RID: 2591
	public Action<Chore> onExit;

	// Token: 0x04000A20 RID: 2592
	public Action<Chore> onComplete;

	// Token: 0x04000A21 RID: 2593
	private Action<Chore> onBegin;

	// Token: 0x04000A22 RID: 2594
	private Action<Chore> onEnd;

	// Token: 0x04000A23 RID: 2595
	public Action<Chore> onCleanup;

	// Token: 0x04000A24 RID: 2596
	public bool debug;

	// Token: 0x04000A25 RID: 2597
	private List<Chore.PreconditionInstance> preconditions = new List<Chore.PreconditionInstance>();

	// Token: 0x04000A26 RID: 2598
	private bool arePreconditionsDirty;

	// Token: 0x04000A27 RID: 2599
	public bool addToDailyReport;

	// Token: 0x04000A28 RID: 2600
	public ReportManager.ReportType reportType;

	// Token: 0x04000A2A RID: 2602
	private Prioritizable prioritizable;

	// Token: 0x04000A2B RID: 2603
	public const int MAX_PLAYER_BASIC_PRIORITY = 9;

	// Token: 0x04000A2C RID: 2604
	public const int MIN_PLAYER_BASIC_PRIORITY = 1;

	// Token: 0x04000A2D RID: 2605
	public const int MAX_PLAYER_HIGH_PRIORITY = 0;

	// Token: 0x04000A2E RID: 2606
	public const int MIN_PLAYER_HIGH_PRIORITY = 0;

	// Token: 0x04000A2F RID: 2607
	public const int MAX_PLAYER_EMERGENCY_PRIORITY = 1;

	// Token: 0x04000A30 RID: 2608
	public const int MIN_PLAYER_EMERGENCY_PRIORITY = 1;

	// Token: 0x04000A31 RID: 2609
	public const int DEFAULT_BASIC_PRIORITY = 5;

	// Token: 0x04000A32 RID: 2610
	public const int MAX_BASIC_PRIORITY = 10;

	// Token: 0x04000A33 RID: 2611
	public const int MIN_BASIC_PRIORITY = 0;

	// Token: 0x04000A34 RID: 2612
	public static bool ENABLE_PERSONAL_PRIORITIES = true;

	// Token: 0x04000A35 RID: 2613
	public static PrioritySetting DefaultPrioritySetting = new PrioritySetting(PriorityScreen.PriorityClass.basic, 5);

	// Token: 0x02000FA9 RID: 4009
	// (Invoke) Token: 0x0600702B RID: 28715
	public delegate bool PreconditionFn(ref Chore.Precondition.Context context, object data);

	// Token: 0x02000FAA RID: 4010
	public struct PreconditionInstance
	{
		// Token: 0x04005526 RID: 21798
		public string id;

		// Token: 0x04005527 RID: 21799
		public string description;

		// Token: 0x04005528 RID: 21800
		public int sortOrder;

		// Token: 0x04005529 RID: 21801
		public Chore.PreconditionFn fn;

		// Token: 0x0400552A RID: 21802
		public object data;
	}

	// Token: 0x02000FAB RID: 4011
	public struct Precondition
	{
		// Token: 0x0400552B RID: 21803
		public string id;

		// Token: 0x0400552C RID: 21804
		public string description;

		// Token: 0x0400552D RID: 21805
		public int sortOrder;

		// Token: 0x0400552E RID: 21806
		public Chore.PreconditionFn fn;

		// Token: 0x02001ED7 RID: 7895
		[DebuggerDisplay("{chore.GetType()}, {chore.gameObject.name}")]
		public struct Context : IComparable<Chore.Precondition.Context>, IEquatable<Chore.Precondition.Context>
		{
			// Token: 0x06009D13 RID: 40211 RVA: 0x0033B25C File Offset: 0x0033945C
			public Context(Chore chore, ChoreConsumerState consumer_state, bool is_attempting_override, object data = null)
			{
				this.masterPriority = chore.masterPriority;
				this.personalPriority = consumer_state.consumer.GetPersonalPriority(chore.choreType);
				this.priority = 0;
				this.priorityMod = chore.priorityMod;
				this.consumerPriority = 0;
				this.interruptPriority = 0;
				this.cost = 0;
				this.chore = chore;
				this.consumerState = consumer_state;
				this.failedPreconditionId = -1;
				this.isAttemptingOverride = is_attempting_override;
				this.data = data;
				this.choreTypeForPermission = chore.choreType;
				this.skipMoreSatisfyingEarlyPrecondition = RootMenu.Instance != null && RootMenu.Instance.IsBuildingChorePanelActive();
				this.SetPriority(chore);
			}

			// Token: 0x06009D14 RID: 40212 RVA: 0x0033B30C File Offset: 0x0033950C
			public void Set(Chore chore, ChoreConsumerState consumer_state, bool is_attempting_override, object data = null)
			{
				this.masterPriority = chore.masterPriority;
				this.priority = 0;
				this.priorityMod = chore.priorityMod;
				this.consumerPriority = 0;
				this.interruptPriority = 0;
				this.cost = 0;
				this.chore = chore;
				this.consumerState = consumer_state;
				this.failedPreconditionId = -1;
				this.isAttemptingOverride = is_attempting_override;
				this.data = data;
				this.choreTypeForPermission = chore.choreType;
				this.SetPriority(chore);
			}

			// Token: 0x06009D15 RID: 40213 RVA: 0x0033B384 File Offset: 0x00339584
			public void SetPriority(Chore chore)
			{
				this.priority = (Game.Instance.advancedPersonalPriorities ? chore.choreType.explicitPriority : chore.choreType.priority);
				this.priorityMod = chore.priorityMod;
				this.interruptPriority = chore.choreType.interruptPriority;
			}

			// Token: 0x06009D16 RID: 40214 RVA: 0x0033B3D8 File Offset: 0x003395D8
			public bool IsSuccess()
			{
				return this.failedPreconditionId == -1;
			}

			// Token: 0x06009D17 RID: 40215 RVA: 0x0033B3E4 File Offset: 0x003395E4
			public bool IsPotentialSuccess()
			{
				if (this.IsSuccess())
				{
					return true;
				}
				if (this.chore.driver == this.consumerState.choreDriver)
				{
					return true;
				}
				if (this.failedPreconditionId != -1)
				{
					if (this.failedPreconditionId >= 0 && this.failedPreconditionId < this.chore.preconditions.Count)
					{
						return this.chore.preconditions[this.failedPreconditionId].id == ChorePreconditions.instance.IsMoreSatisfyingLate.id;
					}
					DebugUtil.DevLogErrorFormat("failedPreconditionId out of range {0}/{1}", new object[]
					{
						this.failedPreconditionId,
						this.chore.preconditions.Count
					});
				}
				return false;
			}

			// Token: 0x06009D18 RID: 40216 RVA: 0x0033B4B0 File Offset: 0x003396B0
			public void RunPreconditions()
			{
				if (this.chore.debug)
				{
					int num = 0;
					num++;
					if (this.consumerState.consumer.debug)
					{
						num++;
						Debugger.Break();
					}
				}
				if (this.chore.arePreconditionsDirty)
				{
					this.chore.preconditions.Sort((Chore.PreconditionInstance x, Chore.PreconditionInstance y) => x.sortOrder.CompareTo(y.sortOrder));
					this.chore.arePreconditionsDirty = false;
				}
				for (int i = 0; i < this.chore.preconditions.Count; i++)
				{
					Chore.PreconditionInstance preconditionInstance = this.chore.preconditions[i];
					if (!preconditionInstance.fn(ref this, preconditionInstance.data))
					{
						this.failedPreconditionId = i;
						return;
					}
				}
			}

			// Token: 0x06009D19 RID: 40217 RVA: 0x0033B57C File Offset: 0x0033977C
			public int CompareTo(Chore.Precondition.Context obj)
			{
				bool flag = this.failedPreconditionId != -1;
				bool flag2 = obj.failedPreconditionId != -1;
				if (flag == flag2)
				{
					int num = this.masterPriority.priority_class - obj.masterPriority.priority_class;
					if (num != 0)
					{
						return num;
					}
					int num2 = this.personalPriority - obj.personalPriority;
					if (num2 != 0)
					{
						return num2;
					}
					int num3 = this.masterPriority.priority_value - obj.masterPriority.priority_value;
					if (num3 != 0)
					{
						return num3;
					}
					int num4 = this.priority - obj.priority;
					if (num4 != 0)
					{
						return num4;
					}
					int num5 = this.priorityMod - obj.priorityMod;
					if (num5 != 0)
					{
						return num5;
					}
					int num6 = this.consumerPriority - obj.consumerPriority;
					if (num6 != 0)
					{
						return num6;
					}
					int num7 = obj.cost - this.cost;
					if (num7 != 0)
					{
						return num7;
					}
					if (this.chore == null && obj.chore == null)
					{
						return 0;
					}
					if (this.chore == null)
					{
						return -1;
					}
					if (obj.chore == null)
					{
						return 1;
					}
					return this.chore.id - obj.chore.id;
				}
				else
				{
					if (!flag)
					{
						return 1;
					}
					return -1;
				}
			}

			// Token: 0x06009D1A RID: 40218 RVA: 0x0033B698 File Offset: 0x00339898
			public override bool Equals(object obj)
			{
				Chore.Precondition.Context context = (Chore.Precondition.Context)obj;
				return this.CompareTo(context) == 0;
			}

			// Token: 0x06009D1B RID: 40219 RVA: 0x0033B6B6 File Offset: 0x003398B6
			public bool Equals(Chore.Precondition.Context other)
			{
				return this.CompareTo(other) == 0;
			}

			// Token: 0x06009D1C RID: 40220 RVA: 0x0033B6C2 File Offset: 0x003398C2
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x06009D1D RID: 40221 RVA: 0x0033B6D4 File Offset: 0x003398D4
			public static bool operator ==(Chore.Precondition.Context x, Chore.Precondition.Context y)
			{
				return x.CompareTo(y) == 0;
			}

			// Token: 0x06009D1E RID: 40222 RVA: 0x0033B6E1 File Offset: 0x003398E1
			public static bool operator !=(Chore.Precondition.Context x, Chore.Precondition.Context y)
			{
				return x.CompareTo(y) != 0;
			}

			// Token: 0x06009D1F RID: 40223 RVA: 0x0033B6EE File Offset: 0x003398EE
			public static bool ShouldFilter(string filter, string text)
			{
				return !string.IsNullOrEmpty(filter) && (string.IsNullOrEmpty(text) || text.ToLower().IndexOf(filter) < 0);
			}

			// Token: 0x04008A26 RID: 35366
			public PrioritySetting masterPriority;

			// Token: 0x04008A27 RID: 35367
			public int personalPriority;

			// Token: 0x04008A28 RID: 35368
			public int priority;

			// Token: 0x04008A29 RID: 35369
			public int priorityMod;

			// Token: 0x04008A2A RID: 35370
			public int interruptPriority;

			// Token: 0x04008A2B RID: 35371
			public int cost;

			// Token: 0x04008A2C RID: 35372
			public int consumerPriority;

			// Token: 0x04008A2D RID: 35373
			public Chore chore;

			// Token: 0x04008A2E RID: 35374
			public ChoreConsumerState consumerState;

			// Token: 0x04008A2F RID: 35375
			public int failedPreconditionId;

			// Token: 0x04008A30 RID: 35376
			public object data;

			// Token: 0x04008A31 RID: 35377
			public bool isAttemptingOverride;

			// Token: 0x04008A32 RID: 35378
			public ChoreType choreTypeForPermission;

			// Token: 0x04008A33 RID: 35379
			public bool skipMoreSatisfyingEarlyPrecondition;
		}
	}
}
