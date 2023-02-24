using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000394 RID: 916
public class RescueSweepBotChore : Chore<RescueSweepBotChore.StatesInstance>
{
	// Token: 0x0600128F RID: 4751 RVA: 0x0006355C File Offset: 0x0006175C
	public RescueSweepBotChore(IStateMachineTarget master, GameObject sweepBot, GameObject baseStation)
	{
		Chore.Precondition precondition = default(Chore.Precondition);
		precondition.id = "CanReachBaseStation";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.CAN_MOVE_TO;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			if (context.consumerState.consumer == null)
			{
				return false;
			}
			KMonoBehaviour kmonoBehaviour = (KMonoBehaviour)data;
			return !(kmonoBehaviour == null) && context.consumerState.consumer.navigator.CanReach(Grid.PosToCell(kmonoBehaviour));
		};
		this.CanReachBaseStation = precondition;
		base..ctor(Db.Get().ChoreTypes.RescueIncapacitated, master, null, false, null, null, null, PriorityScreen.PriorityClass.personalNeeds, 5, false, true, 0, false, ReportManager.ReportType.WorkTime);
		base.smi = new RescueSweepBotChore.StatesInstance(this);
		base.runUntilComplete = true;
		base.AddPrecondition(RescueSweepBotChore.CanReachIncapacitated, sweepBot.GetComponent<Storage>());
		base.AddPrecondition(this.CanReachBaseStation, baseStation.GetComponent<Storage>());
	}

	// Token: 0x06001290 RID: 4752 RVA: 0x00063614 File Offset: 0x00061814
	public override void Begin(Chore.Precondition.Context context)
	{
		base.smi.sm.rescuer.Set(context.consumerState.gameObject, base.smi, false);
		base.smi.sm.rescueTarget.Set(this.gameObject, base.smi, false);
		base.smi.sm.deliverTarget.Set(this.gameObject.GetSMI<SweepBotTrappedStates.Instance>().sm.GetSweepLocker(this.gameObject.GetSMI<SweepBotTrappedStates.Instance>()).gameObject, base.smi, false);
		base.Begin(context);
	}

	// Token: 0x06001291 RID: 4753 RVA: 0x000636B5 File Offset: 0x000618B5
	protected override void End(string reason)
	{
		this.DropSweepBot();
		base.End(reason);
	}

	// Token: 0x06001292 RID: 4754 RVA: 0x000636C4 File Offset: 0x000618C4
	private void DropSweepBot()
	{
		if (base.smi.sm.rescuer.Get(base.smi) != null && base.smi.sm.rescueTarget.Get(base.smi) != null)
		{
			base.smi.sm.rescuer.Get(base.smi).GetComponent<Storage>().Drop(base.smi.sm.rescueTarget.Get(base.smi), true);
		}
	}

	// Token: 0x04000A04 RID: 2564
	public Chore.Precondition CanReachBaseStation;

	// Token: 0x04000A05 RID: 2565
	public static Chore.Precondition CanReachIncapacitated = new Chore.Precondition
	{
		id = "CanReachIncapacitated",
		description = DUPLICANTS.CHORES.PRECONDITIONS.CAN_MOVE_TO,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			KMonoBehaviour kmonoBehaviour = (KMonoBehaviour)data;
			if (kmonoBehaviour == null)
			{
				return false;
			}
			int navigationCost = context.consumerState.navigator.GetNavigationCost(Grid.PosToCell(kmonoBehaviour.transform.GetPosition()));
			if (-1 != navigationCost)
			{
				context.cost += navigationCost;
				return true;
			}
			return false;
		}
	};

	// Token: 0x02000F8E RID: 3982
	public class StatesInstance : GameStateMachine<RescueSweepBotChore.States, RescueSweepBotChore.StatesInstance, RescueSweepBotChore, object>.GameInstance
	{
		// Token: 0x06006FDB RID: 28635 RVA: 0x002A442E File Offset: 0x002A262E
		public StatesInstance(RescueSweepBotChore master)
			: base(master)
		{
		}
	}

	// Token: 0x02000F8F RID: 3983
	public class States : GameStateMachine<RescueSweepBotChore.States, RescueSweepBotChore.StatesInstance, RescueSweepBotChore>
	{
		// Token: 0x06006FDC RID: 28636 RVA: 0x002A4438 File Offset: 0x002A2638
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.approachSweepBot;
			this.approachSweepBot.InitializeStates(this.rescuer, this.rescueTarget, this.holding.pickup, this.failure, Grid.DefaultOffset, null);
			this.holding.Target(this.rescuer).Enter(delegate(RescueSweepBotChore.StatesInstance smi)
			{
				KAnimFile anim = Assets.GetAnim("anim_incapacitated_carrier_kanim");
				this.rescuer.Get(smi).GetComponent<KAnimControllerBase>().RemoveAnimOverrides(anim);
				this.rescuer.Get(smi).GetComponent<KAnimControllerBase>().AddAnimOverrides(anim, 0f);
			}).Exit(delegate(RescueSweepBotChore.StatesInstance smi)
			{
				KAnimFile anim2 = Assets.GetAnim("anim_incapacitated_carrier_kanim");
				this.rescuer.Get(smi).GetComponent<KAnimControllerBase>().RemoveAnimOverrides(anim2);
			});
			this.holding.pickup.Target(this.rescuer).PlayAnim("pickup").Enter(delegate(RescueSweepBotChore.StatesInstance smi)
			{
			})
				.Exit(delegate(RescueSweepBotChore.StatesInstance smi)
				{
					this.rescuer.Get(smi).GetComponent<Storage>().Store(this.rescueTarget.Get(smi), false, false, true, false);
					this.rescueTarget.Get(smi).transform.SetLocalPosition(Vector3.zero);
					KBatchedAnimTracker component = this.rescueTarget.Get(smi).GetComponent<KBatchedAnimTracker>();
					component.symbol = new HashedString("snapTo_pivot");
					component.offset = new Vector3(0f, 0f, 1f);
				})
				.EventTransition(GameHashes.AnimQueueComplete, this.holding.delivering, null);
			this.holding.delivering.InitializeStates(this.rescuer, this.deliverTarget, this.holding.deposit, this.holding.ditch, null, null).Update(delegate(RescueSweepBotChore.StatesInstance smi, float dt)
			{
				if (this.deliverTarget.Get(smi) == null)
				{
					smi.GoTo(this.holding.ditch);
				}
			}, UpdateRate.SIM_200ms, false);
			this.holding.deposit.PlayAnim("place").EventHandler(GameHashes.AnimQueueComplete, delegate(RescueSweepBotChore.StatesInstance smi)
			{
				smi.master.DropSweepBot();
				smi.SetStatus(StateMachine.Status.Success);
				smi.StopSM("complete");
			});
			this.holding.ditch.PlayAnim("place").ScheduleGoTo(0.5f, this.failure).Exit(delegate(RescueSweepBotChore.StatesInstance smi)
			{
				smi.master.DropSweepBot();
			});
			this.failure.Enter(delegate(RescueSweepBotChore.StatesInstance smi)
			{
				smi.SetStatus(StateMachine.Status.Failed);
				smi.StopSM("failed");
			});
		}

		// Token: 0x040054D3 RID: 21715
		public GameStateMachine<RescueSweepBotChore.States, RescueSweepBotChore.StatesInstance, RescueSweepBotChore, object>.ApproachSubState<Storage> approachSweepBot;

		// Token: 0x040054D4 RID: 21716
		public GameStateMachine<RescueSweepBotChore.States, RescueSweepBotChore.StatesInstance, RescueSweepBotChore, object>.State failure;

		// Token: 0x040054D5 RID: 21717
		public RescueSweepBotChore.States.HoldingSweepBot holding;

		// Token: 0x040054D6 RID: 21718
		public StateMachine<RescueSweepBotChore.States, RescueSweepBotChore.StatesInstance, RescueSweepBotChore, object>.TargetParameter rescueTarget;

		// Token: 0x040054D7 RID: 21719
		public StateMachine<RescueSweepBotChore.States, RescueSweepBotChore.StatesInstance, RescueSweepBotChore, object>.TargetParameter deliverTarget;

		// Token: 0x040054D8 RID: 21720
		public StateMachine<RescueSweepBotChore.States, RescueSweepBotChore.StatesInstance, RescueSweepBotChore, object>.TargetParameter rescuer;

		// Token: 0x02001ECB RID: 7883
		public class HoldingSweepBot : GameStateMachine<RescueSweepBotChore.States, RescueSweepBotChore.StatesInstance, RescueSweepBotChore, object>.State
		{
			// Token: 0x040089ED RID: 35309
			public GameStateMachine<RescueSweepBotChore.States, RescueSweepBotChore.StatesInstance, RescueSweepBotChore, object>.State pickup;

			// Token: 0x040089EE RID: 35310
			public GameStateMachine<RescueSweepBotChore.States, RescueSweepBotChore.StatesInstance, RescueSweepBotChore, object>.ApproachSubState<IApproachable> delivering;

			// Token: 0x040089EF RID: 35311
			public GameStateMachine<RescueSweepBotChore.States, RescueSweepBotChore.StatesInstance, RescueSweepBotChore, object>.State deposit;

			// Token: 0x040089F0 RID: 35312
			public GameStateMachine<RescueSweepBotChore.States, RescueSweepBotChore.StatesInstance, RescueSweepBotChore, object>.State ditch;
		}
	}
}
