using System;
using System.Collections.Generic;

// Token: 0x0200060E RID: 1550
public class MissionControlCluster : GameStateMachine<MissionControlCluster, MissionControlCluster.Instance, IStateMachineTarget, MissionControlCluster.Def>
{
	// Token: 0x0600286E RID: 10350 RVA: 0x000D67AC File Offset: 0x000D49AC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.Inoperational;
		this.Inoperational.EventTransition(GameHashes.OperationalChanged, this.Operational, new StateMachine<MissionControlCluster, MissionControlCluster.Instance, IStateMachineTarget, MissionControlCluster.Def>.Transition.ConditionCallback(this.ValidateOperationalTransition)).EventTransition(GameHashes.UpdateRoom, this.Operational, new StateMachine<MissionControlCluster, MissionControlCluster.Instance, IStateMachineTarget, MissionControlCluster.Def>.Transition.ConditionCallback(this.ValidateOperationalTransition));
		this.Operational.EventTransition(GameHashes.OperationalChanged, this.Inoperational, new StateMachine<MissionControlCluster, MissionControlCluster.Instance, IStateMachineTarget, MissionControlCluster.Def>.Transition.ConditionCallback(this.ValidateOperationalTransition)).EventTransition(GameHashes.UpdateRoom, this.Operational.WrongRoom, GameStateMachine<MissionControlCluster, MissionControlCluster.Instance, IStateMachineTarget, MissionControlCluster.Def>.Not(new StateMachine<MissionControlCluster, MissionControlCluster.Instance, IStateMachineTarget, MissionControlCluster.Def>.Transition.ConditionCallback(this.IsInLabRoom))).Enter(new StateMachine<MissionControlCluster, MissionControlCluster.Instance, IStateMachineTarget, MissionControlCluster.Def>.State.Callback(this.OnEnterOperational))
			.DefaultState(this.Operational.NoRockets)
			.Update(delegate(MissionControlCluster.Instance smi, float dt)
			{
				smi.UpdateWorkableRocketsInRange(null);
			}, UpdateRate.SIM_1000ms, false);
		this.Operational.WrongRoom.EventTransition(GameHashes.UpdateRoom, this.Operational.NoRockets, new StateMachine<MissionControlCluster, MissionControlCluster.Instance, IStateMachineTarget, MissionControlCluster.Def>.Transition.ConditionCallback(this.IsInLabRoom));
		this.Operational.NoRockets.ToggleStatusItem(Db.Get().BuildingStatusItems.NoRocketsToMissionControlClusterBoost, null).ParamTransition<bool>(this.WorkableRocketsAreInRange, this.Operational.HasRockets, (MissionControlCluster.Instance smi, bool inRange) => this.WorkableRocketsAreInRange.Get(smi));
		this.Operational.HasRockets.ParamTransition<bool>(this.WorkableRocketsAreInRange, this.Operational.NoRockets, (MissionControlCluster.Instance smi, bool inRange) => !this.WorkableRocketsAreInRange.Get(smi)).ToggleChore(new Func<MissionControlCluster.Instance, Chore>(this.CreateChore), this.Operational);
	}

	// Token: 0x0600286F RID: 10351 RVA: 0x000D6948 File Offset: 0x000D4B48
	private Chore CreateChore(MissionControlCluster.Instance smi)
	{
		MissionControlClusterWorkable component = smi.master.gameObject.GetComponent<MissionControlClusterWorkable>();
		Chore chore = new WorkChore<MissionControlClusterWorkable>(Db.Get().ChoreTypes.Research, component, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		Clustercraft randomBoostableClustercraft = smi.GetRandomBoostableClustercraft();
		component.TargetClustercraft = randomBoostableClustercraft;
		return chore;
	}

	// Token: 0x06002870 RID: 10352 RVA: 0x000D699A File Offset: 0x000D4B9A
	private void OnEnterOperational(MissionControlCluster.Instance smi)
	{
		smi.UpdateWorkableRocketsInRange(null);
		if (this.WorkableRocketsAreInRange.Get(smi))
		{
			smi.GoTo(this.Operational.HasRockets);
			return;
		}
		smi.GoTo(this.Operational.NoRockets);
	}

	// Token: 0x06002871 RID: 10353 RVA: 0x000D69D4 File Offset: 0x000D4BD4
	private bool ValidateOperationalTransition(MissionControlCluster.Instance smi)
	{
		Operational component = smi.GetComponent<Operational>();
		bool flag = smi.IsInsideState(smi.sm.Operational);
		return component != null && flag != component.IsOperational;
	}

	// Token: 0x06002872 RID: 10354 RVA: 0x000D6A11 File Offset: 0x000D4C11
	private bool IsInLabRoom(MissionControlCluster.Instance smi)
	{
		return smi.roomTracker.IsInCorrectRoom();
	}

	// Token: 0x040017B9 RID: 6073
	public GameStateMachine<MissionControlCluster, MissionControlCluster.Instance, IStateMachineTarget, MissionControlCluster.Def>.State Inoperational;

	// Token: 0x040017BA RID: 6074
	public MissionControlCluster.OperationalState Operational;

	// Token: 0x040017BB RID: 6075
	public StateMachine<MissionControlCluster, MissionControlCluster.Instance, IStateMachineTarget, MissionControlCluster.Def>.BoolParameter WorkableRocketsAreInRange;

	// Token: 0x0200127C RID: 4732
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200127D RID: 4733
	public new class Instance : GameStateMachine<MissionControlCluster, MissionControlCluster.Instance, IStateMachineTarget, MissionControlCluster.Def>.GameInstance
	{
		// Token: 0x06007A7F RID: 31359 RVA: 0x002C832C File Offset: 0x002C652C
		public Instance(IStateMachineTarget master, MissionControlCluster.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007A80 RID: 31360 RVA: 0x002C8348 File Offset: 0x002C6548
		public override void StartSM()
		{
			base.StartSM();
			this.clusterUpdatedHandle = Game.Instance.Subscribe(-1298331547, new Action<object>(this.UpdateWorkableRocketsInRange));
		}

		// Token: 0x06007A81 RID: 31361 RVA: 0x002C8371 File Offset: 0x002C6571
		public override void StopSM(string reason)
		{
			base.StopSM(reason);
			Game.Instance.Unsubscribe(this.clusterUpdatedHandle);
		}

		// Token: 0x06007A82 RID: 31362 RVA: 0x002C838C File Offset: 0x002C658C
		public void UpdateWorkableRocketsInRange(object data)
		{
			this.boostableClustercraft.Clear();
			AxialI myWorldLocation = base.gameObject.GetMyWorldLocation();
			for (int i = 0; i < Components.Clustercrafts.Count; i++)
			{
				if (ClusterGrid.Instance.IsInRange(Components.Clustercrafts[i].Location, myWorldLocation, 2) && !this.IsOwnWorld(Components.Clustercrafts[i]) && this.CanBeBoosted(Components.Clustercrafts[i]))
				{
					bool flag = false;
					foreach (object obj in Components.MissionControlClusterWorkables)
					{
						MissionControlClusterWorkable missionControlClusterWorkable = (MissionControlClusterWorkable)obj;
						if (!(missionControlClusterWorkable.gameObject == base.gameObject) && missionControlClusterWorkable.TargetClustercraft == Components.Clustercrafts[i])
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						this.boostableClustercraft.Add(Components.Clustercrafts[i]);
					}
				}
			}
			base.sm.WorkableRocketsAreInRange.Set(this.boostableClustercraft.Count > 0, base.smi, false);
		}

		// Token: 0x06007A83 RID: 31363 RVA: 0x002C84D4 File Offset: 0x002C66D4
		public Clustercraft GetRandomBoostableClustercraft()
		{
			return this.boostableClustercraft.GetRandom<Clustercraft>();
		}

		// Token: 0x06007A84 RID: 31364 RVA: 0x002C84E1 File Offset: 0x002C66E1
		private bool CanBeBoosted(Clustercraft clustercraft)
		{
			return clustercraft.controlStationBuffTimeRemaining == 0f && clustercraft.HasResourcesToMove(1, Clustercraft.CombustionResource.All) && clustercraft.IsFlightInProgress();
		}

		// Token: 0x06007A85 RID: 31365 RVA: 0x002C8508 File Offset: 0x002C6708
		private bool IsOwnWorld(Clustercraft candidateClustercraft)
		{
			int myWorldId = base.gameObject.GetMyWorldId();
			WorldContainer interiorWorld = candidateClustercraft.ModuleInterface.GetInteriorWorld();
			return !(interiorWorld == null) && myWorldId == interiorWorld.id;
		}

		// Token: 0x06007A86 RID: 31366 RVA: 0x002C8541 File Offset: 0x002C6741
		public void ApplyEffect(Clustercraft clustercraft)
		{
			clustercraft.controlStationBuffTimeRemaining = 600f;
		}

		// Token: 0x04005DF5 RID: 24053
		private int clusterUpdatedHandle = -1;

		// Token: 0x04005DF6 RID: 24054
		private List<Clustercraft> boostableClustercraft = new List<Clustercraft>();

		// Token: 0x04005DF7 RID: 24055
		[MyCmpReq]
		public RoomTracker roomTracker;
	}

	// Token: 0x0200127E RID: 4734
	public class OperationalState : GameStateMachine<MissionControlCluster, MissionControlCluster.Instance, IStateMachineTarget, MissionControlCluster.Def>.State
	{
		// Token: 0x04005DF8 RID: 24056
		public GameStateMachine<MissionControlCluster, MissionControlCluster.Instance, IStateMachineTarget, MissionControlCluster.Def>.State WrongRoom;

		// Token: 0x04005DF9 RID: 24057
		public GameStateMachine<MissionControlCluster, MissionControlCluster.Instance, IStateMachineTarget, MissionControlCluster.Def>.State NoRockets;

		// Token: 0x04005DFA RID: 24058
		public GameStateMachine<MissionControlCluster, MissionControlCluster.Instance, IStateMachineTarget, MissionControlCluster.Def>.State HasRockets;
	}
}
