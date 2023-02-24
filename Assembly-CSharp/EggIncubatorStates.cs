using System;
using UnityEngine;

// Token: 0x020005AF RID: 1455
public class EggIncubatorStates : GameStateMachine<EggIncubatorStates, EggIncubatorStates.Instance>
{
	// Token: 0x06002409 RID: 9225 RVA: 0x000C2F24 File Offset: 0x000C1124
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.empty;
		this.empty.PlayAnim("off", KAnim.PlayMode.Loop).EventTransition(GameHashes.OccupantChanged, this.egg, new StateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(EggIncubatorStates.HasEgg)).EventTransition(GameHashes.OccupantChanged, this.baby, new StateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(EggIncubatorStates.HasBaby));
		this.egg.DefaultState(this.egg.unpowered).EventTransition(GameHashes.OccupantChanged, this.empty, GameStateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.Not(new StateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(EggIncubatorStates.HasAny))).EventTransition(GameHashes.OccupantChanged, this.baby, new StateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(EggIncubatorStates.HasBaby))
			.ToggleStatusItem(Db.Get().BuildingStatusItems.IncubatorProgress, (EggIncubatorStates.Instance smi) => smi.master.GetComponent<EggIncubator>());
		this.egg.lose_power.PlayAnim("no_power_pre").EventTransition(GameHashes.OperationalChanged, this.egg.incubating, new StateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(EggIncubatorStates.IsOperational)).OnAnimQueueComplete(this.egg.unpowered);
		this.egg.unpowered.PlayAnim("no_power_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.OperationalChanged, this.egg.incubating, new StateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(EggIncubatorStates.IsOperational));
		this.egg.incubating.PlayAnim("no_power_pst").QueueAnim("working_loop", true, null).EventTransition(GameHashes.OperationalChanged, this.egg.lose_power, GameStateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.Not(new StateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(EggIncubatorStates.IsOperational)));
		this.baby.DefaultState(this.baby.idle).EventTransition(GameHashes.OccupantChanged, this.empty, GameStateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.Not(new StateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(EggIncubatorStates.HasBaby)));
		this.baby.idle.PlayAnim("no_power_pre").QueueAnim("no_power_loop", true, null);
	}

	// Token: 0x0600240A RID: 9226 RVA: 0x000C312B File Offset: 0x000C132B
	public static bool IsOperational(EggIncubatorStates.Instance smi)
	{
		return smi.GetComponent<Operational>().IsOperational;
	}

	// Token: 0x0600240B RID: 9227 RVA: 0x000C3138 File Offset: 0x000C1338
	public static bool HasEgg(EggIncubatorStates.Instance smi)
	{
		GameObject occupant = smi.GetComponent<EggIncubator>().Occupant;
		return occupant && occupant.HasTag(GameTags.Egg);
	}

	// Token: 0x0600240C RID: 9228 RVA: 0x000C3168 File Offset: 0x000C1368
	public static bool HasBaby(EggIncubatorStates.Instance smi)
	{
		GameObject occupant = smi.GetComponent<EggIncubator>().Occupant;
		return occupant && occupant.HasTag(GameTags.Creature);
	}

	// Token: 0x0600240D RID: 9229 RVA: 0x000C3196 File Offset: 0x000C1396
	public static bool HasAny(EggIncubatorStates.Instance smi)
	{
		return smi.GetComponent<EggIncubator>().Occupant;
	}

	// Token: 0x040014B4 RID: 5300
	public StateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.BoolParameter readyToHatch;

	// Token: 0x040014B5 RID: 5301
	public GameStateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.State empty;

	// Token: 0x040014B6 RID: 5302
	public EggIncubatorStates.EggStates egg;

	// Token: 0x040014B7 RID: 5303
	public EggIncubatorStates.BabyStates baby;

	// Token: 0x020011E6 RID: 4582
	public class EggStates : GameStateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04005C68 RID: 23656
		public GameStateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.State incubating;

		// Token: 0x04005C69 RID: 23657
		public GameStateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.State lose_power;

		// Token: 0x04005C6A RID: 23658
		public GameStateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.State unpowered;
	}

	// Token: 0x020011E7 RID: 4583
	public class BabyStates : GameStateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04005C6B RID: 23659
		public GameStateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.State idle;
	}

	// Token: 0x020011E8 RID: 4584
	public new class Instance : GameStateMachine<EggIncubatorStates, EggIncubatorStates.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06007868 RID: 30824 RVA: 0x002BEA74 File Offset: 0x002BCC74
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}
	}
}
