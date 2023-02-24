using System;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public class ConduitSleepMonitor : GameStateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>
{
	// Token: 0x06000317 RID: 791 RVA: 0x00018B54 File Offset: 0x00016D54
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.idle.Enter(delegate(ConduitSleepMonitor.Instance smi)
		{
			this.targetSleepCell.Set(Grid.InvalidCell, smi, false);
			smi.GetComponent<Staterpillar>().DestroyOrphanedConnectorBuilding();
		}).EventTransition(GameHashes.NewBlock, (ConduitSleepMonitor.Instance smi) => GameClock.Instance, this.searching.looking, new StateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.Transition.ConditionCallback(ConduitSleepMonitor.IsSleepyTime));
		this.searching.Enter(new StateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.State.Callback(this.TryRecoverSave)).EventTransition(GameHashes.NewBlock, (ConduitSleepMonitor.Instance smi) => GameClock.Instance, this.idle, GameStateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.Not(new StateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.Transition.ConditionCallback(ConduitSleepMonitor.IsSleepyTime))).Exit(delegate(ConduitSleepMonitor.Instance smi)
		{
			this.targetSleepCell.Set(Grid.InvalidCell, smi, false);
			smi.GetComponent<Staterpillar>().DestroyOrphanedConnectorBuilding();
		});
		this.searching.looking.Update(delegate(ConduitSleepMonitor.Instance smi, float dt)
		{
			this.FindSleepLocation(smi);
		}, UpdateRate.SIM_1000ms, false).ToggleStatusItem(Db.Get().CreatureStatusItems.NoSleepSpot, null).ParamTransition<int>(this.targetSleepCell, this.searching.found, (ConduitSleepMonitor.Instance smi, int sleepCell) => sleepCell != Grid.InvalidCell);
		this.searching.found.Enter(delegate(ConduitSleepMonitor.Instance smi)
		{
			smi.GetComponent<Staterpillar>().SpawnConnectorBuilding(this.targetSleepCell.Get(smi));
		}).ParamTransition<int>(this.targetSleepCell, this.searching.looking, (ConduitSleepMonitor.Instance smi, int sleepCell) => sleepCell == Grid.InvalidCell).ToggleBehaviour(GameTags.Creatures.WantsConduitConnection, (ConduitSleepMonitor.Instance smi) => this.targetSleepCell.Get(smi) != Grid.InvalidCell && ConduitSleepMonitor.IsSleepyTime(smi), null);
	}

	// Token: 0x06000318 RID: 792 RVA: 0x00018D03 File Offset: 0x00016F03
	public static bool IsSleepyTime(ConduitSleepMonitor.Instance smi)
	{
		return GameClock.Instance.GetTimeSinceStartOfCycle() >= 500f;
	}

	// Token: 0x06000319 RID: 793 RVA: 0x00018D1C File Offset: 0x00016F1C
	private void TryRecoverSave(ConduitSleepMonitor.Instance smi)
	{
		Staterpillar component = smi.GetComponent<Staterpillar>();
		if (this.targetSleepCell.Get(smi) == Grid.InvalidCell && component.IsConnectorBuildingSpawned())
		{
			int num = Grid.PosToCell(component.GetConnectorBuilding());
			this.targetSleepCell.Set(num, smi, false);
		}
	}

	// Token: 0x0600031A RID: 794 RVA: 0x00018D68 File Offset: 0x00016F68
	private void FindSleepLocation(ConduitSleepMonitor.Instance smi)
	{
		StaterpillarCellQuery staterpillarCellQuery = PathFinderQueries.staterpillarCellQuery.Reset(10, smi.gameObject, smi.def.conduitLayer);
		smi.GetComponent<Navigator>().RunQuery(staterpillarCellQuery);
		if (staterpillarCellQuery.result_cells.Count > 0)
		{
			foreach (int num in staterpillarCellQuery.result_cells)
			{
				int cellInDirection = Grid.GetCellInDirection(num, Direction.Down);
				if (Grid.Objects[cellInDirection, (int)smi.def.conduitLayer] != null)
				{
					this.targetSleepCell.Set(num, smi, false);
					break;
				}
			}
			if (this.targetSleepCell.Get(smi) == Grid.InvalidCell)
			{
				this.targetSleepCell.Set(staterpillarCellQuery.result_cells[UnityEngine.Random.Range(0, staterpillarCellQuery.result_cells.Count)], smi, false);
			}
		}
	}

	// Token: 0x04000204 RID: 516
	private GameStateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.State idle;

	// Token: 0x04000205 RID: 517
	private ConduitSleepMonitor.SleepSearchStates searching;

	// Token: 0x04000206 RID: 518
	public StateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.IntParameter targetSleepCell = new StateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.IntParameter(Grid.InvalidCell);

	// Token: 0x02000E2D RID: 3629
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005114 RID: 20756
		public ObjectLayer conduitLayer;
	}

	// Token: 0x02000E2E RID: 3630
	private class SleepSearchStates : GameStateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.State
	{
		// Token: 0x04005115 RID: 20757
		public GameStateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.State looking;

		// Token: 0x04005116 RID: 20758
		public GameStateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.State found;
	}

	// Token: 0x02000E2F RID: 3631
	public new class Instance : GameStateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.GameInstance
	{
		// Token: 0x06006BC1 RID: 27585 RVA: 0x00296EE3 File Offset: 0x002950E3
		public Instance(IStateMachineTarget master, ConduitSleepMonitor.Def def)
			: base(master, def)
		{
		}
	}
}
