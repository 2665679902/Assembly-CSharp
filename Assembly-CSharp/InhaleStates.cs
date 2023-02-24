using System;
using STRINGS;

// Token: 0x020000CF RID: 207
public class InhaleStates : GameStateMachine<InhaleStates, InhaleStates.Instance, IStateMachineTarget, InhaleStates.Def>
{
	// Token: 0x06000397 RID: 919 RVA: 0x0001BE5C File Offset: 0x0001A05C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.goingtoeat;
		this.root.Enter("SetTarget", delegate(InhaleStates.Instance smi)
		{
			this.targetCell.Set(smi.monitor.targetCell, smi, false);
		});
		this.goingtoeat.MoveTo((InhaleStates.Instance smi) => this.targetCell.Get(smi), this.inhaling, null, false).ToggleMainStatusItem(new Func<InhaleStates.Instance, StatusItem>(InhaleStates.GetMovingStatusItem), null);
		this.inhaling.DefaultState(this.inhaling.inhale).ToggleStatusItem(CREATURES.STATUSITEMS.INHALING.NAME, CREATURES.STATUSITEMS.INHALING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.inhaling.inhale.PlayAnim((InhaleStates.Instance smi) => smi.def.inhaleAnimPre, KAnim.PlayMode.Once).QueueAnim((InhaleStates.Instance smi) => smi.def.inhaleAnimLoop, true, null).Update("Consume", delegate(InhaleStates.Instance smi, float dt)
		{
			smi.monitor.Consume(dt);
		}, UpdateRate.SIM_200ms, false)
			.EventTransition(GameHashes.ElementNoLongerAvailable, this.inhaling.pst, null)
			.Enter("StartInhaleSound", delegate(InhaleStates.Instance smi)
			{
				smi.StartInhaleSound();
			})
			.Exit("StopInhaleSound", delegate(InhaleStates.Instance smi)
			{
				smi.StopInhaleSound();
			})
			.ScheduleGoTo((InhaleStates.Instance smi) => smi.def.inhaleTime, this.inhaling.pst);
		this.inhaling.pst.Transition(this.inhaling.full, (InhaleStates.Instance smi) => smi.def.alwaysPlayPstAnim || InhaleStates.IsFull(smi), UpdateRate.SIM_200ms).Transition(this.behaviourcomplete, GameStateMachine<InhaleStates, InhaleStates.Instance, IStateMachineTarget, InhaleStates.Def>.Not(new StateMachine<InhaleStates, InhaleStates.Instance, IStateMachineTarget, InhaleStates.Def>.Transition.ConditionCallback(InhaleStates.IsFull)), UpdateRate.SIM_200ms);
		this.inhaling.full.QueueAnim((InhaleStates.Instance smi) => smi.def.inhaleAnimPst, false, null).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.PlayAnim("idle_loop", KAnim.PlayMode.Loop).BehaviourComplete((InhaleStates.Instance smi) => smi.def.behaviourTag, false);
	}

	// Token: 0x06000398 RID: 920 RVA: 0x0001C0FD File Offset: 0x0001A2FD
	private static StatusItem GetMovingStatusItem(InhaleStates.Instance smi)
	{
		if (smi.def.useStorage)
		{
			return smi.def.storageStatusItem;
		}
		return Db.Get().CreatureStatusItems.LookingForFood;
	}

	// Token: 0x06000399 RID: 921 RVA: 0x0001C128 File Offset: 0x0001A328
	private static bool IsFull(InhaleStates.Instance smi)
	{
		if (smi.def.useStorage)
		{
			if (smi.storage != null)
			{
				return smi.storage.IsFull();
			}
		}
		else
		{
			CreatureCalorieMonitor.Instance smi2 = smi.GetSMI<CreatureCalorieMonitor.Instance>();
			if (smi2 != null)
			{
				return smi2.stomach.GetFullness() >= 1f;
			}
		}
		return false;
	}

	// Token: 0x04000250 RID: 592
	public GameStateMachine<InhaleStates, InhaleStates.Instance, IStateMachineTarget, InhaleStates.Def>.State goingtoeat;

	// Token: 0x04000251 RID: 593
	public InhaleStates.InhalingStates inhaling;

	// Token: 0x04000252 RID: 594
	public GameStateMachine<InhaleStates, InhaleStates.Instance, IStateMachineTarget, InhaleStates.Def>.State behaviourcomplete;

	// Token: 0x04000253 RID: 595
	public StateMachine<InhaleStates, InhaleStates.Instance, IStateMachineTarget, InhaleStates.Def>.IntParameter targetCell;

	// Token: 0x02000E87 RID: 3719
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040051A4 RID: 20900
		public string inhaleSound;

		// Token: 0x040051A5 RID: 20901
		public float inhaleTime = 3f;

		// Token: 0x040051A6 RID: 20902
		public Tag behaviourTag = GameTags.Creatures.WantsToEat;

		// Token: 0x040051A7 RID: 20903
		public bool useStorage;

		// Token: 0x040051A8 RID: 20904
		public string inhaleAnimPre = "inhale_pre";

		// Token: 0x040051A9 RID: 20905
		public string inhaleAnimLoop = "inhale_loop";

		// Token: 0x040051AA RID: 20906
		public string inhaleAnimPst = "inhale_pst";

		// Token: 0x040051AB RID: 20907
		public bool alwaysPlayPstAnim;

		// Token: 0x040051AC RID: 20908
		public StatusItem storageStatusItem = Db.Get().CreatureStatusItems.LookingForGas;
	}

	// Token: 0x02000E88 RID: 3720
	public new class Instance : GameStateMachine<InhaleStates, InhaleStates.Instance, IStateMachineTarget, InhaleStates.Def>.GameInstance
	{
		// Token: 0x06006C75 RID: 27765 RVA: 0x0029803F File Offset: 0x0029623F
		public Instance(Chore<InhaleStates.Instance> chore, InhaleStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, def.behaviourTag);
			this.inhaleSound = GlobalAssets.GetSound(def.inhaleSound, false);
		}

		// Token: 0x06006C76 RID: 27766 RVA: 0x00298078 File Offset: 0x00296278
		public void StartInhaleSound()
		{
			LoopingSounds component = base.GetComponent<LoopingSounds>();
			if (component != null)
			{
				component.StartSound(base.smi.inhaleSound);
			}
		}

		// Token: 0x06006C77 RID: 27767 RVA: 0x002980A8 File Offset: 0x002962A8
		public void StopInhaleSound()
		{
			LoopingSounds component = base.GetComponent<LoopingSounds>();
			if (component != null)
			{
				component.StopSound(base.smi.inhaleSound);
			}
		}

		// Token: 0x040051AD RID: 20909
		public string inhaleSound;

		// Token: 0x040051AE RID: 20910
		[MySmiGet]
		public GasAndLiquidConsumerMonitor.Instance monitor;

		// Token: 0x040051AF RID: 20911
		[MyCmpGet]
		public Storage storage;
	}

	// Token: 0x02000E89 RID: 3721
	public class InhalingStates : GameStateMachine<InhaleStates, InhaleStates.Instance, IStateMachineTarget, InhaleStates.Def>.State
	{
		// Token: 0x040051B0 RID: 20912
		public GameStateMachine<InhaleStates, InhaleStates.Instance, IStateMachineTarget, InhaleStates.Def>.State inhale;

		// Token: 0x040051B1 RID: 20913
		public GameStateMachine<InhaleStates, InhaleStates.Instance, IStateMachineTarget, InhaleStates.Def>.State pst;

		// Token: 0x040051B2 RID: 20914
		public GameStateMachine<InhaleStates, InhaleStates.Instance, IStateMachineTarget, InhaleStates.Def>.State full;
	}
}
