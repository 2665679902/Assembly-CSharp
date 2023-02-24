using System;
using System.Collections.Generic;

// Token: 0x0200060D RID: 1549
public class MissionControl : GameStateMachine<MissionControl, MissionControl.Instance, IStateMachineTarget, MissionControl.Def>
{
	// Token: 0x06002866 RID: 10342 RVA: 0x000D6510 File Offset: 0x000D4710
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.Inoperational;
		this.Inoperational.EventTransition(GameHashes.OperationalChanged, this.Operational, new StateMachine<MissionControl, MissionControl.Instance, IStateMachineTarget, MissionControl.Def>.Transition.ConditionCallback(this.ValidateOperationalTransition)).EventTransition(GameHashes.UpdateRoom, this.Operational, new StateMachine<MissionControl, MissionControl.Instance, IStateMachineTarget, MissionControl.Def>.Transition.ConditionCallback(this.ValidateOperationalTransition));
		this.Operational.EventTransition(GameHashes.OperationalChanged, this.Inoperational, new StateMachine<MissionControl, MissionControl.Instance, IStateMachineTarget, MissionControl.Def>.Transition.ConditionCallback(this.ValidateOperationalTransition)).EventTransition(GameHashes.UpdateRoom, this.Operational.WrongRoom, GameStateMachine<MissionControl, MissionControl.Instance, IStateMachineTarget, MissionControl.Def>.Not(new StateMachine<MissionControl, MissionControl.Instance, IStateMachineTarget, MissionControl.Def>.Transition.ConditionCallback(this.IsInLabRoom))).Enter(new StateMachine<MissionControl, MissionControl.Instance, IStateMachineTarget, MissionControl.Def>.State.Callback(this.OnEnterOperational))
			.DefaultState(this.Operational.NoRockets)
			.Update(delegate(MissionControl.Instance smi, float dt)
			{
				smi.UpdateWorkableRockets(null);
			}, UpdateRate.SIM_1000ms, false);
		this.Operational.WrongRoom.EventTransition(GameHashes.UpdateRoom, this.Operational.NoRockets, new StateMachine<MissionControl, MissionControl.Instance, IStateMachineTarget, MissionControl.Def>.Transition.ConditionCallback(this.IsInLabRoom));
		this.Operational.NoRockets.ToggleStatusItem(Db.Get().BuildingStatusItems.NoRocketsToMissionControlBoost, null).ParamTransition<bool>(this.WorkableRocketsAreInRange, this.Operational.HasRockets, (MissionControl.Instance smi, bool inRange) => this.WorkableRocketsAreInRange.Get(smi));
		this.Operational.HasRockets.ParamTransition<bool>(this.WorkableRocketsAreInRange, this.Operational.NoRockets, (MissionControl.Instance smi, bool inRange) => !this.WorkableRocketsAreInRange.Get(smi)).ToggleChore(new Func<MissionControl.Instance, Chore>(this.CreateChore), this.Operational);
	}

	// Token: 0x06002867 RID: 10343 RVA: 0x000D66AC File Offset: 0x000D48AC
	private Chore CreateChore(MissionControl.Instance smi)
	{
		MissionControlWorkable component = smi.master.gameObject.GetComponent<MissionControlWorkable>();
		Chore chore = new WorkChore<MissionControlWorkable>(Db.Get().ChoreTypes.Research, component, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		Spacecraft randomBoostableSpacecraft = smi.GetRandomBoostableSpacecraft();
		component.TargetSpacecraft = randomBoostableSpacecraft;
		return chore;
	}

	// Token: 0x06002868 RID: 10344 RVA: 0x000D66FE File Offset: 0x000D48FE
	private void OnEnterOperational(MissionControl.Instance smi)
	{
		smi.UpdateWorkableRockets(null);
		if (this.WorkableRocketsAreInRange.Get(smi))
		{
			smi.GoTo(this.Operational.HasRockets);
			return;
		}
		smi.GoTo(this.Operational.NoRockets);
	}

	// Token: 0x06002869 RID: 10345 RVA: 0x000D6738 File Offset: 0x000D4938
	private bool ValidateOperationalTransition(MissionControl.Instance smi)
	{
		Operational component = smi.GetComponent<Operational>();
		bool flag = smi.IsInsideState(smi.sm.Operational);
		return component != null && flag != component.IsOperational;
	}

	// Token: 0x0600286A RID: 10346 RVA: 0x000D6775 File Offset: 0x000D4975
	private bool IsInLabRoom(MissionControl.Instance smi)
	{
		return smi.roomTracker.IsInCorrectRoom();
	}

	// Token: 0x040017B6 RID: 6070
	public GameStateMachine<MissionControl, MissionControl.Instance, IStateMachineTarget, MissionControl.Def>.State Inoperational;

	// Token: 0x040017B7 RID: 6071
	public MissionControl.OperationalState Operational;

	// Token: 0x040017B8 RID: 6072
	public StateMachine<MissionControl, MissionControl.Instance, IStateMachineTarget, MissionControl.Def>.BoolParameter WorkableRocketsAreInRange;

	// Token: 0x02001278 RID: 4728
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001279 RID: 4729
	public new class Instance : GameStateMachine<MissionControl, MissionControl.Instance, IStateMachineTarget, MissionControl.Def>.GameInstance
	{
		// Token: 0x06007A75 RID: 31349 RVA: 0x002C81A3 File Offset: 0x002C63A3
		public Instance(IStateMachineTarget master, MissionControl.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007A76 RID: 31350 RVA: 0x002C81B8 File Offset: 0x002C63B8
		public void UpdateWorkableRockets(object data)
		{
			this.boostableSpacecraft.Clear();
			for (int i = 0; i < SpacecraftManager.instance.GetSpacecraft().Count; i++)
			{
				if (this.CanBeBoosted(SpacecraftManager.instance.GetSpacecraft()[i]))
				{
					bool flag = false;
					foreach (object obj in Components.MissionControlWorkables)
					{
						MissionControlWorkable missionControlWorkable = (MissionControlWorkable)obj;
						if (!(missionControlWorkable.gameObject == base.gameObject) && missionControlWorkable.TargetSpacecraft == SpacecraftManager.instance.GetSpacecraft()[i])
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						this.boostableSpacecraft.Add(SpacecraftManager.instance.GetSpacecraft()[i]);
					}
				}
			}
			base.sm.WorkableRocketsAreInRange.Set(this.boostableSpacecraft.Count > 0, base.smi, false);
		}

		// Token: 0x06007A77 RID: 31351 RVA: 0x002C82C8 File Offset: 0x002C64C8
		public Spacecraft GetRandomBoostableSpacecraft()
		{
			return this.boostableSpacecraft.GetRandom<Spacecraft>();
		}

		// Token: 0x06007A78 RID: 31352 RVA: 0x002C82D5 File Offset: 0x002C64D5
		private bool CanBeBoosted(Spacecraft spacecraft)
		{
			return spacecraft.controlStationBuffTimeRemaining == 0f && spacecraft.state == Spacecraft.MissionState.Underway;
		}

		// Token: 0x06007A79 RID: 31353 RVA: 0x002C82F2 File Offset: 0x002C64F2
		public void ApplyEffect(Spacecraft spacecraft)
		{
			spacecraft.controlStationBuffTimeRemaining = 600f;
		}

		// Token: 0x04005DEE RID: 24046
		private List<Spacecraft> boostableSpacecraft = new List<Spacecraft>();

		// Token: 0x04005DEF RID: 24047
		[MyCmpReq]
		public RoomTracker roomTracker;
	}

	// Token: 0x0200127A RID: 4730
	public class OperationalState : GameStateMachine<MissionControl, MissionControl.Instance, IStateMachineTarget, MissionControl.Def>.State
	{
		// Token: 0x04005DF0 RID: 24048
		public GameStateMachine<MissionControl, MissionControl.Instance, IStateMachineTarget, MissionControl.Def>.State WrongRoom;

		// Token: 0x04005DF1 RID: 24049
		public GameStateMachine<MissionControl, MissionControl.Instance, IStateMachineTarget, MissionControl.Def>.State NoRockets;

		// Token: 0x04005DF2 RID: 24050
		public GameStateMachine<MissionControl, MissionControl.Instance, IStateMachineTarget, MissionControl.Def>.State HasRockets;
	}
}
