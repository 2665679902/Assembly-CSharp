using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000AB RID: 171
public class BeeSleepStates : GameStateMachine<BeeSleepStates, BeeSleepStates.Instance, IStateMachineTarget, BeeSleepStates.Def>
{
	// Token: 0x060002FA RID: 762 RVA: 0x00017CCC File Offset: 0x00015ECC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.findSleepLocation;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.SLEEPING.NAME, CREATURES.STATUSITEMS.SLEEPING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.findSleepLocation.Enter(delegate(BeeSleepStates.Instance smi)
		{
			BeeSleepStates.FindSleepLocation(smi);
			if (smi.targetSleepCell != Grid.InvalidCell)
			{
				smi.GoTo(this.moveToSleepLocation);
				return;
			}
			smi.GoTo(this.behaviourcomplete);
		});
		this.moveToSleepLocation.MoveTo((BeeSleepStates.Instance smi) => smi.targetSleepCell, this.sleep.pre, this.behaviourcomplete, false);
		this.sleep.Enter("EnableGravity", delegate(BeeSleepStates.Instance smi)
		{
			GameComps.Gravities.Add(smi.gameObject, Vector2.zero, delegate
			{
				if (GameComps.Gravities.Has(smi.gameObject))
				{
					GameComps.Gravities.Remove(smi.gameObject);
				}
			});
		}).TriggerOnEnter(GameHashes.SleepStarted, null).TriggerOnExit(GameHashes.SleepFinished, null)
			.Transition(this.sleep.pst, new StateMachine<BeeSleepStates, BeeSleepStates.Instance, IStateMachineTarget, BeeSleepStates.Def>.Transition.ConditionCallback(BeeSleepStates.ShouldWakeUp), UpdateRate.SIM_1000ms);
		this.sleep.pre.QueueAnim("sleep_pre", false, null).OnAnimQueueComplete(this.sleep.loop);
		this.sleep.loop.Enter(delegate(BeeSleepStates.Instance smi)
		{
			smi.GetComponent<LoopingSounds>().PauseSound(GlobalAssets.GetSound("Bee_wings_LP", false), true);
		}).QueueAnim("sleep_loop", true, null).Exit(delegate(BeeSleepStates.Instance smi)
		{
			smi.GetComponent<LoopingSounds>().PauseSound(GlobalAssets.GetSound("Bee_wings_LP", false), false);
		});
		this.sleep.pst.QueueAnim("sleep_pst", false, null).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.BeeWantsToSleep, false);
	}

	// Token: 0x060002FB RID: 763 RVA: 0x00017EA0 File Offset: 0x000160A0
	private static void FindSleepLocation(BeeSleepStates.Instance smi)
	{
		smi.targetSleepCell = Grid.InvalidCell;
		FloorCellQuery floorCellQuery = PathFinderQueries.floorCellQuery.Reset(1, 0);
		smi.GetComponent<Navigator>().RunQuery(floorCellQuery);
		if (floorCellQuery.result_cells.Count > 0)
		{
			smi.targetSleepCell = floorCellQuery.result_cells[UnityEngine.Random.Range(0, floorCellQuery.result_cells.Count)];
		}
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00017F01 File Offset: 0x00016101
	public static bool ShouldWakeUp(BeeSleepStates.Instance smi)
	{
		return smi.GetSMI<BeeSleepMonitor.Instance>().CO2Exposure <= 0f;
	}

	// Token: 0x040001F3 RID: 499
	public BeeSleepStates.SleepStates sleep;

	// Token: 0x040001F4 RID: 500
	public GameStateMachine<BeeSleepStates, BeeSleepStates.Instance, IStateMachineTarget, BeeSleepStates.Def>.State findSleepLocation;

	// Token: 0x040001F5 RID: 501
	public GameStateMachine<BeeSleepStates, BeeSleepStates.Instance, IStateMachineTarget, BeeSleepStates.Def>.State moveToSleepLocation;

	// Token: 0x040001F6 RID: 502
	public GameStateMachine<BeeSleepStates, BeeSleepStates.Instance, IStateMachineTarget, BeeSleepStates.Def>.State behaviourcomplete;

	// Token: 0x02000E16 RID: 3606
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E17 RID: 3607
	public new class Instance : GameStateMachine<BeeSleepStates, BeeSleepStates.Instance, IStateMachineTarget, BeeSleepStates.Def>.GameInstance
	{
		// Token: 0x06006B84 RID: 27524 RVA: 0x00296982 File Offset: 0x00294B82
		public Instance(Chore<BeeSleepStates.Instance> chore, BeeSleepStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.BeeWantsToSleep);
		}

		// Token: 0x040050D8 RID: 20696
		public int targetSleepCell;

		// Token: 0x040050D9 RID: 20697
		public float co2Exposure;
	}

	// Token: 0x02000E18 RID: 3608
	public class SleepStates : GameStateMachine<BeeSleepStates, BeeSleepStates.Instance, IStateMachineTarget, BeeSleepStates.Def>.State
	{
		// Token: 0x040050DA RID: 20698
		public GameStateMachine<BeeSleepStates, BeeSleepStates.Instance, IStateMachineTarget, BeeSleepStates.Def>.State pre;

		// Token: 0x040050DB RID: 20699
		public GameStateMachine<BeeSleepStates, BeeSleepStates.Instance, IStateMachineTarget, BeeSleepStates.Def>.State loop;

		// Token: 0x040050DC RID: 20700
		public GameStateMachine<BeeSleepStates, BeeSleepStates.Instance, IStateMachineTarget, BeeSleepStates.Def>.State pst;
	}
}
