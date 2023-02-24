using System;
using UnityEngine;

// Token: 0x02000387 RID: 903
public class IdleChore : Chore<IdleChore.StatesInstance>
{
	// Token: 0x06001271 RID: 4721 RVA: 0x000629B0 File Offset: 0x00060BB0
	public IdleChore(IStateMachineTarget target)
		: base(Db.Get().ChoreTypes.Idle, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.idle, 5, false, true, 0, false, ReportManager.ReportType.IdleTime)
	{
		this.showAvailabilityInHoverText = false;
		base.smi = new IdleChore.StatesInstance(this, target.gameObject);
	}

	// Token: 0x02000F70 RID: 3952
	public class StatesInstance : GameStateMachine<IdleChore.States, IdleChore.StatesInstance, IdleChore, object>.GameInstance
	{
		// Token: 0x06006F80 RID: 28544 RVA: 0x002A2655 File Offset: 0x002A0855
		public StatesInstance(IdleChore master, GameObject idler)
			: base(master)
		{
			base.sm.idler.Set(idler, base.smi, false);
			this.idleCellSensor = base.GetComponent<Sensors>().GetSensor<IdleCellSensor>();
		}

		// Token: 0x06006F81 RID: 28545 RVA: 0x002A2688 File Offset: 0x002A0888
		public void UpdateNavType()
		{
			NavType currentNavType = base.GetComponent<Navigator>().CurrentNavType;
			base.sm.isOnLadder.Set(currentNavType == NavType.Ladder || currentNavType == NavType.Pole, this, false);
			base.sm.isOnTube.Set(currentNavType == NavType.Tube, this, false);
		}

		// Token: 0x06006F82 RID: 28546 RVA: 0x002A26D6 File Offset: 0x002A08D6
		public int GetIdleCell()
		{
			return this.idleCellSensor.GetCell();
		}

		// Token: 0x06006F83 RID: 28547 RVA: 0x002A26E3 File Offset: 0x002A08E3
		public bool HasIdleCell()
		{
			return this.idleCellSensor.GetCell() != Grid.InvalidCell;
		}

		// Token: 0x04005486 RID: 21638
		private IdleCellSensor idleCellSensor;
	}

	// Token: 0x02000F71 RID: 3953
	public class States : GameStateMachine<IdleChore.States, IdleChore.StatesInstance, IdleChore>
	{
		// Token: 0x06006F84 RID: 28548 RVA: 0x002A26FC File Offset: 0x002A08FC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			base.Target(this.idler);
			this.idle.DefaultState(this.idle.onfloor).Enter("UpdateNavType", delegate(IdleChore.StatesInstance smi)
			{
				smi.UpdateNavType();
			}).Update("UpdateNavType", delegate(IdleChore.StatesInstance smi, float dt)
			{
				smi.UpdateNavType();
			}, UpdateRate.SIM_200ms, false)
				.ToggleStateMachine((IdleChore.StatesInstance smi) => new TaskAvailabilityMonitor.Instance(smi.master))
				.ToggleTag(GameTags.Idle);
			this.idle.onfloor.PlayAnim("idle_default", KAnim.PlayMode.Loop).ParamTransition<bool>(this.isOnLadder, this.idle.onladder, GameStateMachine<IdleChore.States, IdleChore.StatesInstance, IdleChore, object>.IsTrue).ParamTransition<bool>(this.isOnTube, this.idle.ontube, GameStateMachine<IdleChore.States, IdleChore.StatesInstance, IdleChore, object>.IsTrue)
				.ToggleScheduleCallback("IdleMove", (IdleChore.StatesInstance smi) => (float)UnityEngine.Random.Range(5, 15), delegate(IdleChore.StatesInstance smi)
				{
					smi.GoTo(this.idle.move);
				});
			this.idle.onladder.PlayAnim("ladder_idle", KAnim.PlayMode.Loop).ToggleScheduleCallback("IdleMove", (IdleChore.StatesInstance smi) => (float)UnityEngine.Random.Range(5, 15), delegate(IdleChore.StatesInstance smi)
			{
				smi.GoTo(this.idle.move);
			});
			this.idle.ontube.PlayAnim("tube_idle_loop", KAnim.PlayMode.Loop).Update("IdleMove", delegate(IdleChore.StatesInstance smi, float dt)
			{
				if (smi.HasIdleCell())
				{
					smi.GoTo(this.idle.move);
				}
			}, UpdateRate.SIM_1000ms, false);
			this.idle.move.Transition(this.idle, (IdleChore.StatesInstance smi) => !smi.HasIdleCell(), UpdateRate.SIM_200ms).TriggerOnEnter(GameHashes.BeginWalk, null).TriggerOnExit(GameHashes.EndWalk, null)
				.ToggleAnims("anim_loco_walk_kanim", 0f, "")
				.MoveTo((IdleChore.StatesInstance smi) => smi.GetIdleCell(), this.idle, this.idle, false)
				.Exit("UpdateNavType", delegate(IdleChore.StatesInstance smi)
				{
					smi.UpdateNavType();
				})
				.Exit("ClearWalk", delegate(IdleChore.StatesInstance smi)
				{
					smi.GetComponent<KBatchedAnimController>().Play("idle_default", KAnim.PlayMode.Once, 1f, 0f);
				});
		}

		// Token: 0x04005487 RID: 21639
		public StateMachine<IdleChore.States, IdleChore.StatesInstance, IdleChore, object>.BoolParameter isOnLadder;

		// Token: 0x04005488 RID: 21640
		public StateMachine<IdleChore.States, IdleChore.StatesInstance, IdleChore, object>.BoolParameter isOnTube;

		// Token: 0x04005489 RID: 21641
		public StateMachine<IdleChore.States, IdleChore.StatesInstance, IdleChore, object>.TargetParameter idler;

		// Token: 0x0400548A RID: 21642
		public IdleChore.States.IdleState idle;

		// Token: 0x02001EBB RID: 7867
		public class IdleState : GameStateMachine<IdleChore.States, IdleChore.StatesInstance, IdleChore, object>.State
		{
			// Token: 0x040089A3 RID: 35235
			public GameStateMachine<IdleChore.States, IdleChore.StatesInstance, IdleChore, object>.State onfloor;

			// Token: 0x040089A4 RID: 35236
			public GameStateMachine<IdleChore.States, IdleChore.StatesInstance, IdleChore, object>.State onladder;

			// Token: 0x040089A5 RID: 35237
			public GameStateMachine<IdleChore.States, IdleChore.StatesInstance, IdleChore, object>.State ontube;

			// Token: 0x040089A6 RID: 35238
			public GameStateMachine<IdleChore.States, IdleChore.StatesInstance, IdleChore, object>.State move;
		}
	}
}
