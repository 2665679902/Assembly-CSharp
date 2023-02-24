using System;
using UnityEngine;

// Token: 0x0200039F RID: 927
public class WorkChore<WorkableType> : Chore<WorkChore<WorkableType>.StatesInstance> where WorkableType : Workable
{
	// Token: 0x17000065 RID: 101
	// (get) Token: 0x060012A7 RID: 4775 RVA: 0x00063EB3 File Offset: 0x000620B3
	// (set) Token: 0x060012A8 RID: 4776 RVA: 0x00063EBB File Offset: 0x000620BB
	public bool onlyWhenOperational { get; private set; }

	// Token: 0x060012A9 RID: 4777 RVA: 0x00063EC4 File Offset: 0x000620C4
	public override string ToString()
	{
		return "WorkChore<" + typeof(WorkableType).ToString() + ">";
	}

	// Token: 0x060012AA RID: 4778 RVA: 0x00063EE4 File Offset: 0x000620E4
	public WorkChore(ChoreType chore_type, IStateMachineTarget target, ChoreProvider chore_provider = null, bool run_until_complete = true, Action<Chore> on_complete = null, Action<Chore> on_begin = null, Action<Chore> on_end = null, bool allow_in_red_alert = true, ScheduleBlockType schedule_block = null, bool ignore_schedule_block = false, bool only_when_operational = true, KAnimFile override_anims = null, bool is_preemptable = false, bool allow_in_context_menu = true, bool allow_prioritization = true, PriorityScreen.PriorityClass priority_class = PriorityScreen.PriorityClass.basic, int priority_class_value = 5, bool ignore_building_assignment = false, bool add_to_daily_report = true)
		: base(chore_type, target, chore_provider, run_until_complete, on_complete, on_begin, on_end, priority_class, priority_class_value, is_preemptable, allow_in_context_menu, 0, add_to_daily_report, ReportManager.ReportType.WorkTime)
	{
		base.smi = new WorkChore<WorkableType>.StatesInstance(this, target.gameObject, override_anims);
		this.onlyWhenOperational = only_when_operational;
		if (allow_prioritization)
		{
			base.SetPrioritizable(target.GetComponent<Prioritizable>());
		}
		base.AddPrecondition(ChorePreconditions.instance.IsNotTransferArm, null);
		if (!allow_in_red_alert)
		{
			base.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		}
		if (schedule_block != null)
		{
			base.AddPrecondition(ChorePreconditions.instance.IsScheduledTime, schedule_block);
		}
		else if (!ignore_schedule_block)
		{
			base.AddPrecondition(ChorePreconditions.instance.IsScheduledTime, Db.Get().ScheduleBlockTypes.Work);
		}
		base.AddPrecondition(ChorePreconditions.instance.CanMoveTo, base.smi.sm.workable.Get<WorkableType>(base.smi));
		Operational component = target.GetComponent<Operational>();
		if (only_when_operational && component != null)
		{
			base.AddPrecondition(ChorePreconditions.instance.IsOperational, component);
		}
		if (only_when_operational)
		{
			Deconstructable component2 = target.GetComponent<Deconstructable>();
			if (component2 != null)
			{
				base.AddPrecondition(ChorePreconditions.instance.IsNotMarkedForDeconstruction, component2);
			}
			BuildingEnabledButton component3 = target.GetComponent<BuildingEnabledButton>();
			if (component3 != null)
			{
				base.AddPrecondition(ChorePreconditions.instance.IsNotMarkedForDisable, component3);
			}
		}
		if (!ignore_building_assignment && base.smi.sm.workable.Get(base.smi).GetComponent<Assignable>() != null)
		{
			base.AddPrecondition(ChorePreconditions.instance.IsAssignedtoMe, base.smi.sm.workable.Get<Assignable>(base.smi));
		}
		WorkableType workableType = target as WorkableType;
		if (workableType != null)
		{
			if (!string.IsNullOrEmpty(workableType.requiredSkillPerk))
			{
				HashedString hashedString = workableType.requiredSkillPerk;
				base.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, hashedString);
			}
			if (workableType.requireMinionToWork)
			{
				base.AddPrecondition(ChorePreconditions.instance.IsMinion, null);
			}
		}
	}

	// Token: 0x060012AB RID: 4779 RVA: 0x0006410B File Offset: 0x0006230B
	public override void Begin(Chore.Precondition.Context context)
	{
		base.smi.sm.worker.Set(context.consumerState.gameObject, base.smi, false);
		base.Begin(context);
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x0006413C File Offset: 0x0006233C
	public bool IsOperationalValid()
	{
		if (this.onlyWhenOperational)
		{
			Operational component = base.smi.master.GetComponent<Operational>();
			if (component != null && !component.IsOperational)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060012AD RID: 4781 RVA: 0x00064178 File Offset: 0x00062378
	public override bool CanPreempt(Chore.Precondition.Context context)
	{
		if (!base.CanPreempt(context))
		{
			return false;
		}
		if (context.chore.driver == null)
		{
			return false;
		}
		if (context.chore.driver == context.consumerState.choreDriver)
		{
			return false;
		}
		Workable workable = base.smi.sm.workable.Get<WorkableType>(base.smi);
		if (workable == null)
		{
			return false;
		}
		if (this.preemption_cb != null)
		{
			if (!this.preemption_cb(context))
			{
				return false;
			}
		}
		else
		{
			int num = 4;
			int navigationCost = context.chore.driver.GetComponent<Navigator>().GetNavigationCost(workable);
			if (navigationCost == -1 || navigationCost < num)
			{
				return false;
			}
			if (context.consumerState.navigator.GetNavigationCost(workable) * 2 > navigationCost)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x04000A0F RID: 2575
	public Func<Chore.Precondition.Context, bool> preemption_cb;

	// Token: 0x02000FA7 RID: 4007
	public class StatesInstance : GameStateMachine<WorkChore<WorkableType>.States, WorkChore<WorkableType>.StatesInstance, WorkChore<WorkableType>, object>.GameInstance
	{
		// Token: 0x06007025 RID: 28709 RVA: 0x002A6177 File Offset: 0x002A4377
		public StatesInstance(WorkChore<WorkableType> master, GameObject workable, KAnimFile override_anims)
			: base(master)
		{
			this.overrideAnims = override_anims;
			base.sm.workable.Set(workable, base.smi, false);
		}

		// Token: 0x06007026 RID: 28710 RVA: 0x002A61A0 File Offset: 0x002A43A0
		public void EnableAnimOverrides()
		{
			if (this.overrideAnims != null)
			{
				base.sm.worker.Get<KAnimControllerBase>(base.smi).AddAnimOverrides(this.overrideAnims, 0f);
			}
		}

		// Token: 0x06007027 RID: 28711 RVA: 0x002A61D6 File Offset: 0x002A43D6
		public void DisableAnimOverrides()
		{
			if (this.overrideAnims != null)
			{
				base.sm.worker.Get<KAnimControllerBase>(base.smi).RemoveAnimOverrides(this.overrideAnims);
			}
		}

		// Token: 0x04005520 RID: 21792
		private KAnimFile overrideAnims;
	}

	// Token: 0x02000FA8 RID: 4008
	public class States : GameStateMachine<WorkChore<WorkableType>.States, WorkChore<WorkableType>.StatesInstance, WorkChore<WorkableType>>
	{
		// Token: 0x06007028 RID: 28712 RVA: 0x002A6208 File Offset: 0x002A4408
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.approach;
			base.Target(this.worker);
			this.approach.InitializeStates(this.worker, this.workable, this.work, null, null, null).Update("CheckOperational", delegate(WorkChore<WorkableType>.StatesInstance smi, float dt)
			{
				if (!smi.master.IsOperationalValid())
				{
					smi.StopSM("Building not operational");
				}
			}, UpdateRate.SIM_200ms, false);
			this.work.Enter(delegate(WorkChore<WorkableType>.StatesInstance smi)
			{
				smi.EnableAnimOverrides();
			}).ToggleWork<WorkableType>(this.workable, this.success, null, (WorkChore<WorkableType>.StatesInstance smi) => smi.master.IsOperationalValid()).Exit(delegate(WorkChore<WorkableType>.StatesInstance smi)
			{
				smi.DisableAnimOverrides();
			});
			this.success.ReturnSuccess();
		}

		// Token: 0x04005521 RID: 21793
		public GameStateMachine<WorkChore<WorkableType>.States, WorkChore<WorkableType>.StatesInstance, WorkChore<WorkableType>, object>.ApproachSubState<WorkableType> approach;

		// Token: 0x04005522 RID: 21794
		public GameStateMachine<WorkChore<WorkableType>.States, WorkChore<WorkableType>.StatesInstance, WorkChore<WorkableType>, object>.State work;

		// Token: 0x04005523 RID: 21795
		public GameStateMachine<WorkChore<WorkableType>.States, WorkChore<WorkableType>.StatesInstance, WorkChore<WorkableType>, object>.State success;

		// Token: 0x04005524 RID: 21796
		public StateMachine<WorkChore<WorkableType>.States, WorkChore<WorkableType>.StatesInstance, WorkChore<WorkableType>, object>.TargetParameter workable;

		// Token: 0x04005525 RID: 21797
		public StateMachine<WorkChore<WorkableType>.States, WorkChore<WorkableType>.StatesInstance, WorkChore<WorkableType>, object>.TargetParameter worker;
	}
}
