using System;
using STRINGS;

// Token: 0x020000BF RID: 191
public class FleeStates : GameStateMachine<FleeStates, FleeStates.Instance, IStateMachineTarget, FleeStates.Def>
{
	// Token: 0x06000358 RID: 856 RVA: 0x0001A6B4 File Offset: 0x000188B4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.plan;
		this.root.Enter("SetFleeTarget", delegate(FleeStates.Instance smi)
		{
			this.fleeToTarget.Set(CreatureHelpers.GetFleeTargetLocatorObject(smi.master.gameObject, smi.GetSMI<ThreatMonitor.Instance>().MainThreat), smi, false);
		}).ToggleStatusItem(CREATURES.STATUSITEMS.FLEEING.NAME, CREATURES.STATUSITEMS.FLEEING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.plan.Enter(delegate(FleeStates.Instance smi)
		{
			ThreatMonitor.Instance smi2 = smi.master.gameObject.GetSMI<ThreatMonitor.Instance>();
			this.fleeToTarget.Set(CreatureHelpers.GetFleeTargetLocatorObject(smi.master.gameObject, smi2.MainThreat), smi, false);
			if (this.fleeToTarget.Get(smi) != null)
			{
				smi.GoTo(this.approach);
				return;
			}
			smi.GoTo(this.cower);
		});
		this.approach.InitializeStates(this.mover, this.fleeToTarget, this.cower, this.cower, null, NavigationTactics.ReduceTravelDistance).Enter(delegate(FleeStates.Instance smi)
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, CREATURES.STATUSITEMS.FLEEING.NAME.text, smi.master.transform, 1.5f, false);
		});
		this.cower.Enter(delegate(FleeStates.Instance smi)
		{
			string text = "DEFAULT COWER ANIMATION";
			if (smi.Get<KBatchedAnimController>().HasAnimation("cower"))
			{
				text = "cower";
			}
			else if (smi.Get<KBatchedAnimController>().HasAnimation("idle"))
			{
				text = "idle";
			}
			else if (smi.Get<KBatchedAnimController>().HasAnimation("idle_loop"))
			{
				text = "idle_loop";
			}
			smi.Get<KBatchedAnimController>().Play(text, KAnim.PlayMode.Loop, 1f, 0f);
		}).ScheduleGoTo(2f, this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.Flee, false);
	}

	// Token: 0x0400022F RID: 559
	private StateMachine<FleeStates, FleeStates.Instance, IStateMachineTarget, FleeStates.Def>.TargetParameter mover;

	// Token: 0x04000230 RID: 560
	public StateMachine<FleeStates, FleeStates.Instance, IStateMachineTarget, FleeStates.Def>.TargetParameter fleeToTarget;

	// Token: 0x04000231 RID: 561
	public GameStateMachine<FleeStates, FleeStates.Instance, IStateMachineTarget, FleeStates.Def>.State plan;

	// Token: 0x04000232 RID: 562
	public GameStateMachine<FleeStates, FleeStates.Instance, IStateMachineTarget, FleeStates.Def>.ApproachSubState<IApproachable> approach;

	// Token: 0x04000233 RID: 563
	public GameStateMachine<FleeStates, FleeStates.Instance, IStateMachineTarget, FleeStates.Def>.State cower;

	// Token: 0x04000234 RID: 564
	public GameStateMachine<FleeStates, FleeStates.Instance, IStateMachineTarget, FleeStates.Def>.State behaviourcomplete;

	// Token: 0x02000E5B RID: 3675
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E5C RID: 3676
	public new class Instance : GameStateMachine<FleeStates, FleeStates.Instance, IStateMachineTarget, FleeStates.Def>.GameInstance
	{
		// Token: 0x06006C18 RID: 27672 RVA: 0x002975C6 File Offset: 0x002957C6
		public Instance(Chore<FleeStates.Instance> chore, FleeStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Flee);
			base.sm.mover.Set(base.GetComponent<Navigator>(), base.smi);
		}
	}
}
