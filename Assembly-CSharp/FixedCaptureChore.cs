using System;
using STRINGS;

// Token: 0x02000384 RID: 900
public class FixedCaptureChore : Chore<FixedCaptureChore.FixedCaptureChoreStates.Instance>
{
	// Token: 0x06001267 RID: 4711 RVA: 0x00062418 File Offset: 0x00060618
	public FixedCaptureChore(KPrefabID capture_point)
	{
		Chore.Precondition precondition = default(Chore.Precondition);
		precondition.id = "IsCreatureAvailableForFixedCapture";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_CREATURE_AVAILABLE_FOR_FIXED_CAPTURE;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return (data as FixedCapturePoint.Instance).IsCreatureAvailableForFixedCapture();
		};
		this.IsCreatureAvailableForFixedCapture = precondition;
		base..ctor(Db.Get().ChoreTypes.Ranch, capture_point, null, false, null, null, null, PriorityScreen.PriorityClass.basic, 5, false, true, 0, false, ReportManager.ReportType.WorkTime);
		base.AddPrecondition(this.IsCreatureAvailableForFixedCapture, capture_point.GetSMI<FixedCapturePoint.Instance>());
		base.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, Db.Get().SkillPerks.CanWrangleCreatures.Id);
		base.AddPrecondition(ChorePreconditions.instance.IsScheduledTime, Db.Get().ScheduleBlockTypes.Work);
		base.AddPrecondition(ChorePreconditions.instance.CanMoveTo, capture_point.GetComponent<Building>());
		Operational component = capture_point.GetComponent<Operational>();
		base.AddPrecondition(ChorePreconditions.instance.IsOperational, component);
		Deconstructable component2 = capture_point.GetComponent<Deconstructable>();
		base.AddPrecondition(ChorePreconditions.instance.IsNotMarkedForDeconstruction, component2);
		BuildingEnabledButton component3 = capture_point.GetComponent<BuildingEnabledButton>();
		base.AddPrecondition(ChorePreconditions.instance.IsNotMarkedForDisable, component3);
		base.smi = new FixedCaptureChore.FixedCaptureChoreStates.Instance(capture_point);
		base.SetPrioritizable(capture_point.GetComponent<Prioritizable>());
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x00062568 File Offset: 0x00060768
	public override void Begin(Chore.Precondition.Context context)
	{
		base.smi.sm.rancher.Set(context.consumerState.gameObject, base.smi, false);
		base.smi.sm.creature.Set(base.smi.fixedCapturePoint.targetCapturable.gameObject, base.smi, false);
		base.Begin(context);
	}

	// Token: 0x040009F7 RID: 2551
	public Chore.Precondition IsCreatureAvailableForFixedCapture;

	// Token: 0x02000F69 RID: 3945
	public class FixedCaptureChoreStates : GameStateMachine<FixedCaptureChore.FixedCaptureChoreStates, FixedCaptureChore.FixedCaptureChoreStates.Instance>
	{
		// Token: 0x06006F6D RID: 28525 RVA: 0x002A1B78 File Offset: 0x0029FD78
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.movetopoint;
			base.Target(this.rancher);
			this.root.Exit("ResetCapturePoint", delegate(FixedCaptureChore.FixedCaptureChoreStates.Instance smi)
			{
				smi.fixedCapturePoint.ResetCapturePoint();
			});
			this.movetopoint.MoveTo((FixedCaptureChore.FixedCaptureChoreStates.Instance smi) => Grid.PosToCell(smi.transform.GetPosition()), this.waitforcreature_pre, null, false).Target(this.masterTarget).EventTransition(GameHashes.CreatureAbandonedCapturePoint, this.failed, null);
			this.waitforcreature_pre.EnterTransition(null, (FixedCaptureChore.FixedCaptureChoreStates.Instance smi) => smi.fixedCapturePoint.IsNullOrStopped()).EnterTransition(this.failed, new StateMachine<FixedCaptureChore.FixedCaptureChoreStates, FixedCaptureChore.FixedCaptureChoreStates.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(FixedCaptureChore.FixedCaptureChoreStates.HasCreatureLeft)).EnterTransition(this.waitforcreature, (FixedCaptureChore.FixedCaptureChoreStates.Instance smi) => true);
			this.waitforcreature.ToggleAnims("anim_interacts_rancherstation_kanim", 0f, "").PlayAnim("calling_loop", KAnim.PlayMode.Loop).Transition(this.failed, new StateMachine<FixedCaptureChore.FixedCaptureChoreStates, FixedCaptureChore.FixedCaptureChoreStates.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(FixedCaptureChore.FixedCaptureChoreStates.HasCreatureLeft), UpdateRate.SIM_200ms)
				.Face(this.creature, 0f)
				.Enter("SetRancherIsAvailableForCapturing", delegate(FixedCaptureChore.FixedCaptureChoreStates.Instance smi)
				{
					smi.fixedCapturePoint.SetRancherIsAvailableForCapturing();
				})
				.Exit("ClearRancherIsAvailableForCapturing", delegate(FixedCaptureChore.FixedCaptureChoreStates.Instance smi)
				{
					smi.fixedCapturePoint.ClearRancherIsAvailableForCapturing();
				})
				.Target(this.masterTarget)
				.EventTransition(GameHashes.CreatureArrivedAtCapturePoint, this.capturecreature, null);
			this.capturecreature.EventTransition(GameHashes.CreatureAbandonedCapturePoint, this.failed, null).EnterTransition(this.failed, (FixedCaptureChore.FixedCaptureChoreStates.Instance smi) => smi.fixedCapturePoint.targetCapturable.IsNullOrStopped()).ToggleWork<Capturable>(this.creature, this.success, this.failed, null);
			this.failed.GoTo(null);
			this.success.ReturnSuccess();
		}

		// Token: 0x06006F6E RID: 28526 RVA: 0x002A1DB4 File Offset: 0x0029FFB4
		private static bool HasCreatureLeft(FixedCaptureChore.FixedCaptureChoreStates.Instance smi)
		{
			return smi.fixedCapturePoint.targetCapturable.IsNullOrStopped() || !smi.fixedCapturePoint.targetCapturable.GetComponent<ChoreConsumer>().IsChoreEqualOrAboveCurrentChorePriority<FixedCaptureStates>();
		}

		// Token: 0x04005466 RID: 21606
		public StateMachine<FixedCaptureChore.FixedCaptureChoreStates, FixedCaptureChore.FixedCaptureChoreStates.Instance, IStateMachineTarget, object>.TargetParameter rancher;

		// Token: 0x04005467 RID: 21607
		public StateMachine<FixedCaptureChore.FixedCaptureChoreStates, FixedCaptureChore.FixedCaptureChoreStates.Instance, IStateMachineTarget, object>.TargetParameter creature;

		// Token: 0x04005468 RID: 21608
		private GameStateMachine<FixedCaptureChore.FixedCaptureChoreStates, FixedCaptureChore.FixedCaptureChoreStates.Instance, IStateMachineTarget, object>.State movetopoint;

		// Token: 0x04005469 RID: 21609
		private GameStateMachine<FixedCaptureChore.FixedCaptureChoreStates, FixedCaptureChore.FixedCaptureChoreStates.Instance, IStateMachineTarget, object>.State waitforcreature_pre;

		// Token: 0x0400546A RID: 21610
		private GameStateMachine<FixedCaptureChore.FixedCaptureChoreStates, FixedCaptureChore.FixedCaptureChoreStates.Instance, IStateMachineTarget, object>.State waitforcreature;

		// Token: 0x0400546B RID: 21611
		private GameStateMachine<FixedCaptureChore.FixedCaptureChoreStates, FixedCaptureChore.FixedCaptureChoreStates.Instance, IStateMachineTarget, object>.State capturecreature;

		// Token: 0x0400546C RID: 21612
		private GameStateMachine<FixedCaptureChore.FixedCaptureChoreStates, FixedCaptureChore.FixedCaptureChoreStates.Instance, IStateMachineTarget, object>.State failed;

		// Token: 0x0400546D RID: 21613
		private GameStateMachine<FixedCaptureChore.FixedCaptureChoreStates, FixedCaptureChore.FixedCaptureChoreStates.Instance, IStateMachineTarget, object>.State success;

		// Token: 0x02001EB5 RID: 7861
		public new class Instance : GameStateMachine<FixedCaptureChore.FixedCaptureChoreStates, FixedCaptureChore.FixedCaptureChoreStates.Instance, IStateMachineTarget, object>.GameInstance
		{
			// Token: 0x06009C84 RID: 40068 RVA: 0x0033A857 File Offset: 0x00338A57
			public Instance(KPrefabID capture_point)
				: base(capture_point)
			{
				this.fixedCapturePoint = capture_point.GetSMI<FixedCapturePoint.Instance>();
			}

			// Token: 0x04008991 RID: 35217
			public FixedCapturePoint.Instance fixedCapturePoint;
		}
	}
}
