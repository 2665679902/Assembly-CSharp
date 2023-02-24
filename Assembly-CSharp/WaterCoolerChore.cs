using System;
using Klei;
using Klei.AI;
using TUNING;

// Token: 0x0200039E RID: 926
public class WaterCoolerChore : Chore<WaterCoolerChore.StatesInstance>, IWorkerPrioritizable
{
	// Token: 0x060012A4 RID: 4772 RVA: 0x00063D50 File Offset: 0x00061F50
	public WaterCoolerChore(IStateMachineTarget master, Workable chat_workable, Action<Chore> on_complete = null, Action<Chore> on_begin = null, Action<Chore> on_end = null)
		: base(Db.Get().ChoreTypes.Relax, master, master.GetComponent<ChoreProvider>(), true, on_complete, on_begin, on_end, PriorityScreen.PriorityClass.high, 5, false, true, 0, false, ReportManager.ReportType.PersonalTime)
	{
		base.smi = new WaterCoolerChore.StatesInstance(this);
		base.smi.sm.chitchatlocator.Set(chat_workable, base.smi);
		base.AddPrecondition(ChorePreconditions.instance.CanMoveTo, chat_workable);
		base.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		base.AddPrecondition(ChorePreconditions.instance.IsScheduledTime, Db.Get().ScheduleBlockTypes.Recreation);
		base.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, this);
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x00063E22 File Offset: 0x00062022
	public override void Begin(Chore.Precondition.Context context)
	{
		base.smi.sm.drinker.Set(context.consumerState.gameObject, base.smi, false);
		base.Begin(context);
	}

	// Token: 0x060012A6 RID: 4774 RVA: 0x00063E54 File Offset: 0x00062054
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.trackingEffect) && component.HasEffect(this.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(this.specificEffect) && component.HasEffect(this.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x04000A0C RID: 2572
	public int basePriority = RELAXATION.PRIORITY.TIER2;

	// Token: 0x04000A0D RID: 2573
	public string specificEffect = "Socialized";

	// Token: 0x04000A0E RID: 2574
	public string trackingEffect = "RecentlySocialized";

	// Token: 0x02000FA5 RID: 4005
	public class States : GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore>
	{
		// Token: 0x06007021 RID: 28705 RVA: 0x002A5FA8 File Offset: 0x002A41A8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.drink_move;
			base.Target(this.drinker);
			this.drink_move.InitializeStates(this.drinker, this.masterTarget, this.drink, null, null, null);
			this.drink.ToggleAnims("anim_interacts_watercooler_kanim", 0f, "").DefaultState(this.drink.drink);
			this.drink.drink.Face(this.masterTarget, 0.5f).PlayAnim("working_pre").QueueAnim("working_loop", false, null)
				.OnAnimQueueComplete(this.drink.post);
			this.drink.post.Enter("Drink", new StateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.State.Callback(this.Drink)).PlayAnim("working_pst").OnAnimQueueComplete(this.chat_move);
			this.chat_move.InitializeStates(this.drinker, this.chitchatlocator, this.chat, null, null, null);
			this.chat.ToggleWork<SocialGatheringPointWorkable>(this.chitchatlocator, this.success, null, null);
			this.success.ReturnSuccess();
		}

		// Token: 0x06007022 RID: 28706 RVA: 0x002A60D8 File Offset: 0x002A42D8
		private void Drink(WaterCoolerChore.StatesInstance smi)
		{
			Storage storage = this.masterTarget.Get<Storage>(smi);
			Worker worker = this.stateTarget.Get<Worker>(smi);
			float num;
			SimUtil.DiseaseInfo diseaseInfo;
			float num2;
			storage.ConsumeAndGetDisease(GameTags.Water, 1f, out num, out diseaseInfo, out num2);
			GermExposureMonitor.Instance smi2 = worker.GetSMI<GermExposureMonitor.Instance>();
			if (smi2 != null)
			{
				smi2.TryInjectDisease(diseaseInfo.idx, diseaseInfo.count, GameTags.Water, Sickness.InfectionVector.Digestion);
			}
			Effects component = worker.GetComponent<Effects>();
			if (!string.IsNullOrEmpty(smi.master.trackingEffect))
			{
				component.Add(smi.master.trackingEffect, true);
			}
		}

		// Token: 0x04005519 RID: 21785
		public StateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.TargetParameter drinker;

		// Token: 0x0400551A RID: 21786
		public StateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.TargetParameter chitchatlocator;

		// Token: 0x0400551B RID: 21787
		public GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.ApproachSubState<WaterCooler> drink_move;

		// Token: 0x0400551C RID: 21788
		public WaterCoolerChore.States.DrinkStates drink;

		// Token: 0x0400551D RID: 21789
		public GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.ApproachSubState<IApproachable> chat_move;

		// Token: 0x0400551E RID: 21790
		public GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.State chat;

		// Token: 0x0400551F RID: 21791
		public GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.State success;

		// Token: 0x02001ED5 RID: 7893
		public class DrinkStates : GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.State
		{
			// Token: 0x04008A1F RID: 35359
			public GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.State drink;

			// Token: 0x04008A20 RID: 35360
			public GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.State post;
		}
	}

	// Token: 0x02000FA6 RID: 4006
	public class StatesInstance : GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.GameInstance
	{
		// Token: 0x06007024 RID: 28708 RVA: 0x002A616E File Offset: 0x002A436E
		public StatesInstance(WaterCoolerChore master)
			: base(master)
		{
		}
	}
}
