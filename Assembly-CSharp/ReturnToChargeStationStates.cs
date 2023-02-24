using System;
using UnityEngine;

// Token: 0x020002E7 RID: 743
public class ReturnToChargeStationStates : GameStateMachine<ReturnToChargeStationStates, ReturnToChargeStationStates.Instance, IStateMachineTarget, ReturnToChargeStationStates.Def>
{
	// Token: 0x06000EB8 RID: 3768 RVA: 0x0004FAF8 File Offset: 0x0004DCF8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.emote;
		this.emote.ToggleStatusItem(Db.Get().RobotStatusItems.MovingToChargeStation, (ReturnToChargeStationStates.Instance smi) => smi.gameObject, Db.Get().StatusItemCategories.Main).PlayAnim("react_lobatt", KAnim.PlayMode.Once).OnAnimQueueComplete(this.movingToChargingStation);
		this.idle.ToggleStatusItem(Db.Get().RobotStatusItems.MovingToChargeStation, (ReturnToChargeStationStates.Instance smi) => smi.gameObject, Db.Get().StatusItemCategories.Main).ScheduleGoTo(1f, this.movingToChargingStation);
		this.movingToChargingStation.ToggleStatusItem(Db.Get().RobotStatusItems.MovingToChargeStation, (ReturnToChargeStationStates.Instance smi) => smi.gameObject, Db.Get().StatusItemCategories.Main).MoveTo(delegate(ReturnToChargeStationStates.Instance smi)
		{
			Storage sweepLocker = this.GetSweepLocker(smi);
			if (!(sweepLocker == null))
			{
				return Grid.PosToCell(sweepLocker);
			}
			return Grid.InvalidCell;
		}, this.chargingstates.waitingForCharging, this.idle, false);
		this.chargingstates.Enter(delegate(ReturnToChargeStationStates.Instance smi)
		{
			Storage sweepLocker2 = this.GetSweepLocker(smi);
			if (sweepLocker2 != null)
			{
				smi.master.GetComponent<Facing>().Face(sweepLocker2.gameObject.transform.position + Vector3.right);
				Vector3 position = smi.transform.GetPosition();
				position.z = Grid.GetLayerZ(Grid.SceneLayer.BuildingUse);
				smi.transform.SetPosition(position);
				KBatchedAnimController component = smi.GetComponent<KBatchedAnimController>();
				component.enabled = false;
				component.enabled = true;
			}
		}).Exit(delegate(ReturnToChargeStationStates.Instance smi)
		{
			Vector3 position2 = smi.transform.GetPosition();
			position2.z = Grid.GetLayerZ(Grid.SceneLayer.Creatures);
			smi.transform.SetPosition(position2);
			KBatchedAnimController component2 = smi.GetComponent<KBatchedAnimController>();
			component2.enabled = false;
			component2.enabled = true;
		}).Enter(delegate(ReturnToChargeStationStates.Instance smi)
		{
			this.Station_DockRobot(smi, true);
		})
			.Exit(delegate(ReturnToChargeStationStates.Instance smi)
			{
				this.Station_DockRobot(smi, false);
			});
		this.chargingstates.waitingForCharging.PlayAnim("react_base", KAnim.PlayMode.Loop).TagTransition(GameTags.Robots.Behaviours.RechargeBehaviour, this.chargingstates.completed, true).Transition(this.chargingstates.charging, (ReturnToChargeStationStates.Instance smi) => smi.StationReadyToCharge(), UpdateRate.SIM_200ms);
		this.chargingstates.charging.TagTransition(GameTags.Robots.Behaviours.RechargeBehaviour, this.chargingstates.completed, true).Transition(this.chargingstates.interupted, (ReturnToChargeStationStates.Instance smi) => !smi.StationReadyToCharge(), UpdateRate.SIM_200ms).ToggleEffect("Charging")
			.PlayAnim("sleep_pre")
			.QueueAnim("sleep_idle", true, null)
			.Enter(new StateMachine<ReturnToChargeStationStates, ReturnToChargeStationStates.Instance, IStateMachineTarget, ReturnToChargeStationStates.Def>.State.Callback(this.Station_StartCharging))
			.Exit(new StateMachine<ReturnToChargeStationStates, ReturnToChargeStationStates.Instance, IStateMachineTarget, ReturnToChargeStationStates.Def>.State.Callback(this.Station_StopCharging));
		this.chargingstates.interupted.PlayAnim("sleep_pst").TagTransition(GameTags.Robots.Behaviours.RechargeBehaviour, this.chargingstates.completed, true).OnAnimQueueComplete(this.chargingstates.waitingForCharging);
		this.chargingstates.completed.PlayAnim("sleep_pst").OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Robots.Behaviours.RechargeBehaviour, false);
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x0004FDF0 File Offset: 0x0004DFF0
	public Storage GetSweepLocker(ReturnToChargeStationStates.Instance smi)
	{
		StorageUnloadMonitor.Instance smi2 = smi.master.gameObject.GetSMI<StorageUnloadMonitor.Instance>();
		if (smi2 == null)
		{
			return null;
		}
		return smi2.sm.sweepLocker.Get(smi2);
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x0004FE24 File Offset: 0x0004E024
	public void Station_StartCharging(ReturnToChargeStationStates.Instance smi)
	{
		Storage sweepLocker = this.GetSweepLocker(smi);
		if (sweepLocker != null)
		{
			sweepLocker.GetComponent<SweepBotStation>().StartCharging();
		}
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x0004FE50 File Offset: 0x0004E050
	public void Station_StopCharging(ReturnToChargeStationStates.Instance smi)
	{
		Storage sweepLocker = this.GetSweepLocker(smi);
		if (sweepLocker != null)
		{
			sweepLocker.GetComponent<SweepBotStation>().StopCharging();
		}
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x0004FE7C File Offset: 0x0004E07C
	public void Station_DockRobot(ReturnToChargeStationStates.Instance smi, bool dockState)
	{
		Storage sweepLocker = this.GetSweepLocker(smi);
		if (sweepLocker != null)
		{
			sweepLocker.GetComponent<SweepBotStation>().DockRobot(dockState);
		}
	}

	// Token: 0x04000823 RID: 2083
	public GameStateMachine<ReturnToChargeStationStates, ReturnToChargeStationStates.Instance, IStateMachineTarget, ReturnToChargeStationStates.Def>.State emote;

	// Token: 0x04000824 RID: 2084
	public GameStateMachine<ReturnToChargeStationStates, ReturnToChargeStationStates.Instance, IStateMachineTarget, ReturnToChargeStationStates.Def>.State idle;

	// Token: 0x04000825 RID: 2085
	public GameStateMachine<ReturnToChargeStationStates, ReturnToChargeStationStates.Instance, IStateMachineTarget, ReturnToChargeStationStates.Def>.State movingToChargingStation;

	// Token: 0x04000826 RID: 2086
	public GameStateMachine<ReturnToChargeStationStates, ReturnToChargeStationStates.Instance, IStateMachineTarget, ReturnToChargeStationStates.Def>.State behaviourcomplete;

	// Token: 0x04000827 RID: 2087
	public ReturnToChargeStationStates.ChargingStates chargingstates;

	// Token: 0x02000EFE RID: 3838
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000EFF RID: 3839
	public new class Instance : GameStateMachine<ReturnToChargeStationStates, ReturnToChargeStationStates.Instance, IStateMachineTarget, ReturnToChargeStationStates.Def>.GameInstance
	{
		// Token: 0x06006D8F RID: 28047 RVA: 0x0029A5FA File Offset: 0x002987FA
		public Instance(Chore<ReturnToChargeStationStates.Instance> chore, ReturnToChargeStationStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Robots.Behaviours.RechargeBehaviour);
		}

		// Token: 0x06006D90 RID: 28048 RVA: 0x0029A620 File Offset: 0x00298820
		public bool ChargeAborted()
		{
			return base.smi.sm.GetSweepLocker(base.smi) == null || !base.smi.sm.GetSweepLocker(base.smi).GetComponent<Operational>().IsActive;
		}

		// Token: 0x06006D91 RID: 28049 RVA: 0x0029A670 File Offset: 0x00298870
		public bool StationReadyToCharge()
		{
			return base.smi.sm.GetSweepLocker(base.smi) != null && base.smi.sm.GetSweepLocker(base.smi).GetComponent<Operational>().IsActive;
		}
	}

	// Token: 0x02000F00 RID: 3840
	public class ChargingStates : GameStateMachine<ReturnToChargeStationStates, ReturnToChargeStationStates.Instance, IStateMachineTarget, ReturnToChargeStationStates.Def>.State
	{
		// Token: 0x040052DB RID: 21211
		public GameStateMachine<ReturnToChargeStationStates, ReturnToChargeStationStates.Instance, IStateMachineTarget, ReturnToChargeStationStates.Def>.State waitingForCharging;

		// Token: 0x040052DC RID: 21212
		public GameStateMachine<ReturnToChargeStationStates, ReturnToChargeStationStates.Instance, IStateMachineTarget, ReturnToChargeStationStates.Def>.State charging;

		// Token: 0x040052DD RID: 21213
		public GameStateMachine<ReturnToChargeStationStates, ReturnToChargeStationStates.Instance, IStateMachineTarget, ReturnToChargeStationStates.Def>.State interupted;

		// Token: 0x040052DE RID: 21214
		public GameStateMachine<ReturnToChargeStationStates, ReturnToChargeStationStates.Instance, IStateMachineTarget, ReturnToChargeStationStates.Def>.State completed;
	}
}
