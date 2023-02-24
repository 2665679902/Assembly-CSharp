using System;

// Token: 0x02000840 RID: 2112
public class RocketPassengerMonitor : GameStateMachine<RocketPassengerMonitor, RocketPassengerMonitor.Instance>
{
	// Token: 0x06003CEE RID: 15598 RVA: 0x001544D4 File Offset: 0x001526D4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.satisfied.ParamTransition<int>(this.targetCell, this.moving, (RocketPassengerMonitor.Instance smi, int p) => p != Grid.InvalidCell);
		this.moving.ParamTransition<int>(this.targetCell, this.satisfied, (RocketPassengerMonitor.Instance smi, int p) => p == Grid.InvalidCell).ToggleChore((RocketPassengerMonitor.Instance smi) => this.CreateChore(smi), this.satisfied).Exit(delegate(RocketPassengerMonitor.Instance smi)
		{
			this.targetCell.Set(Grid.InvalidCell, smi, false);
		});
		this.movingToModuleDeployPre.Enter(delegate(RocketPassengerMonitor.Instance smi)
		{
			this.targetCell.Set(smi.moduleDeployTaskTargetMoveCell, smi, false);
			smi.GoTo(this.movingToModuleDeploy);
		});
		this.movingToModuleDeploy.ParamTransition<int>(this.targetCell, this.satisfied, (RocketPassengerMonitor.Instance smi, int p) => p == Grid.InvalidCell).ToggleChore((RocketPassengerMonitor.Instance smi) => this.CreateChore(smi), this.moduleDeploy);
		this.moduleDeploy.Enter(delegate(RocketPassengerMonitor.Instance smi)
		{
			smi.moduleDeployCompleteCallback(null);
			this.targetCell.Set(Grid.InvalidCell, smi, false);
			smi.moduleDeployCompleteCallback = null;
			smi.GoTo(smi.sm.satisfied);
		});
	}

	// Token: 0x06003CEF RID: 15599 RVA: 0x00154604 File Offset: 0x00152804
	public Chore CreateChore(RocketPassengerMonitor.Instance smi)
	{
		MoveChore moveChore = new MoveChore(smi.master, Db.Get().ChoreTypes.RocketEnterExit, (MoveChore.StatesInstance mover_smi) => this.targetCell.Get(smi), false);
		moveChore.AddPrecondition(ChorePreconditions.instance.CanMoveToCell, this.targetCell.Get(smi));
		return moveChore;
	}

	// Token: 0x040027D3 RID: 10195
	public StateMachine<RocketPassengerMonitor, RocketPassengerMonitor.Instance, IStateMachineTarget, object>.IntParameter targetCell = new StateMachine<RocketPassengerMonitor, RocketPassengerMonitor.Instance, IStateMachineTarget, object>.IntParameter(Grid.InvalidCell);

	// Token: 0x040027D4 RID: 10196
	public GameStateMachine<RocketPassengerMonitor, RocketPassengerMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x040027D5 RID: 10197
	public GameStateMachine<RocketPassengerMonitor, RocketPassengerMonitor.Instance, IStateMachineTarget, object>.State moving;

	// Token: 0x040027D6 RID: 10198
	public GameStateMachine<RocketPassengerMonitor, RocketPassengerMonitor.Instance, IStateMachineTarget, object>.State movingToModuleDeployPre;

	// Token: 0x040027D7 RID: 10199
	public GameStateMachine<RocketPassengerMonitor, RocketPassengerMonitor.Instance, IStateMachineTarget, object>.State movingToModuleDeploy;

	// Token: 0x040027D8 RID: 10200
	public GameStateMachine<RocketPassengerMonitor, RocketPassengerMonitor.Instance, IStateMachineTarget, object>.State moduleDeploy;

	// Token: 0x020015D8 RID: 5592
	public new class Instance : GameStateMachine<RocketPassengerMonitor, RocketPassengerMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008590 RID: 34192 RVA: 0x002ECA5E File Offset: 0x002EAC5E
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x06008591 RID: 34193 RVA: 0x002ECA68 File Offset: 0x002EAC68
		public bool ShouldMoveThroughRocketDoor()
		{
			int num = base.sm.targetCell.Get(this);
			if (!Grid.IsValidCell(num))
			{
				return false;
			}
			if ((int)Grid.WorldIdx[num] == this.GetMyWorldId())
			{
				base.sm.targetCell.Set(Grid.InvalidCell, this, false);
				return false;
			}
			return true;
		}

		// Token: 0x06008592 RID: 34194 RVA: 0x002ECABB File Offset: 0x002EACBB
		public void SetMoveTarget(int cell)
		{
			if ((int)Grid.WorldIdx[cell] == this.GetMyWorldId())
			{
				return;
			}
			base.sm.targetCell.Set(cell, this, false);
		}

		// Token: 0x06008593 RID: 34195 RVA: 0x002ECAE1 File Offset: 0x002EACE1
		public void SetModuleDeployChore(int cell, Action<Chore> OnChoreCompleteCallback)
		{
			this.moduleDeployCompleteCallback = OnChoreCompleteCallback;
			this.moduleDeployTaskTargetMoveCell = cell;
			this.GoTo(base.sm.movingToModuleDeployPre);
			base.sm.targetCell.Set(cell, this, false);
		}

		// Token: 0x06008594 RID: 34196 RVA: 0x002ECB16 File Offset: 0x002EAD16
		public void CancelModuleDeployChore()
		{
			this.moduleDeployCompleteCallback = null;
			this.moduleDeployTaskTargetMoveCell = Grid.InvalidCell;
			base.sm.targetCell.Set(Grid.InvalidCell, base.smi, false);
		}

		// Token: 0x06008595 RID: 34197 RVA: 0x002ECB48 File Offset: 0x002EAD48
		public void ClearMoveTarget(int testCell)
		{
			int num = base.sm.targetCell.Get(this);
			if (Grid.IsValidCell(num) && Grid.WorldIdx[num] == Grid.WorldIdx[testCell])
			{
				base.sm.targetCell.Set(Grid.InvalidCell, this, false);
				if (base.IsInsideState(base.sm.moving))
				{
					this.GoTo(base.sm.satisfied);
				}
			}
		}

		// Token: 0x0400680F RID: 26639
		public int lastWorldID;

		// Token: 0x04006810 RID: 26640
		public Action<Chore> moduleDeployCompleteCallback;

		// Token: 0x04006811 RID: 26641
		public int moduleDeployTaskTargetMoveCell;
	}
}
