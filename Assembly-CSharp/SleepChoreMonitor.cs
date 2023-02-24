using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000844 RID: 2116
public class SleepChoreMonitor : GameStateMachine<SleepChoreMonitor, SleepChoreMonitor.Instance>
{
	// Token: 0x06003CFE RID: 15614 RVA: 0x00154A40 File Offset: 0x00152C40
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		base.serializable = StateMachine.SerializeType.Never;
		this.root.EventHandler(GameHashes.AssignablesChanged, delegate(SleepChoreMonitor.Instance smi)
		{
			smi.UpdateBed();
		});
		this.satisfied.EventTransition(GameHashes.AddUrge, this.checkforbed, (SleepChoreMonitor.Instance smi) => smi.HasSleepUrge());
		this.checkforbed.Enter("SetBed", delegate(SleepChoreMonitor.Instance smi)
		{
			smi.UpdateBed();
			if (smi.GetSMI<StaminaMonitor.Instance>().NeedsToSleep())
			{
				smi.GoTo(this.passingout);
				return;
			}
			if (this.bed.Get(smi) == null || !smi.IsBedReachable())
			{
				smi.GoTo(this.sleeponfloor);
				return;
			}
			smi.GoTo(this.bedassigned);
		});
		this.passingout.ToggleChore(new Func<SleepChoreMonitor.Instance, Chore>(this.CreatePassingOutChore), this.satisfied, this.satisfied);
		this.sleeponfloor.EventTransition(GameHashes.AssignablesChanged, this.checkforbed, null).EventHandlerTransition(GameHashes.AssignableReachabilityChanged, this.checkforbed, (SleepChoreMonitor.Instance smi, object data) => smi.IsBedReachable()).ToggleChore(new Func<SleepChoreMonitor.Instance, Chore>(this.CreateSleepOnFloorChore), this.satisfied, this.satisfied);
		this.bedassigned.ParamTransition<GameObject>(this.bed, this.checkforbed, (SleepChoreMonitor.Instance smi, GameObject p) => p == null).EventTransition(GameHashes.AssignablesChanged, this.checkforbed, null).EventTransition(GameHashes.AssignableReachabilityChanged, this.checkforbed, (SleepChoreMonitor.Instance smi) => !smi.IsBedReachable())
			.ToggleChore(new Func<SleepChoreMonitor.Instance, Chore>(this.CreateSleepChore), this.satisfied, this.satisfied);
	}

	// Token: 0x06003CFF RID: 15615 RVA: 0x00154BFC File Offset: 0x00152DFC
	private Chore CreatePassingOutChore(SleepChoreMonitor.Instance smi)
	{
		GameObject gameObject = smi.CreatePassedOutLocator();
		return new SleepChore(Db.Get().ChoreTypes.Sleep, smi.master, gameObject, true, false);
	}

	// Token: 0x06003D00 RID: 15616 RVA: 0x00154C30 File Offset: 0x00152E30
	private Chore CreateSleepOnFloorChore(SleepChoreMonitor.Instance smi)
	{
		GameObject gameObject = smi.CreateFloorLocator();
		return new SleepChore(Db.Get().ChoreTypes.Sleep, smi.master, gameObject, true, true);
	}

	// Token: 0x06003D01 RID: 15617 RVA: 0x00154C61 File Offset: 0x00152E61
	private Chore CreateSleepChore(SleepChoreMonitor.Instance smi)
	{
		return new SleepChore(Db.Get().ChoreTypes.Sleep, smi.master, this.bed.Get(smi), false, true);
	}

	// Token: 0x040027E2 RID: 10210
	public GameStateMachine<SleepChoreMonitor, SleepChoreMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x040027E3 RID: 10211
	public GameStateMachine<SleepChoreMonitor, SleepChoreMonitor.Instance, IStateMachineTarget, object>.State checkforbed;

	// Token: 0x040027E4 RID: 10212
	public GameStateMachine<SleepChoreMonitor, SleepChoreMonitor.Instance, IStateMachineTarget, object>.State passingout;

	// Token: 0x040027E5 RID: 10213
	public GameStateMachine<SleepChoreMonitor, SleepChoreMonitor.Instance, IStateMachineTarget, object>.State sleeponfloor;

	// Token: 0x040027E6 RID: 10214
	public GameStateMachine<SleepChoreMonitor, SleepChoreMonitor.Instance, IStateMachineTarget, object>.State bedassigned;

	// Token: 0x040027E7 RID: 10215
	public StateMachine<SleepChoreMonitor, SleepChoreMonitor.Instance, IStateMachineTarget, object>.TargetParameter bed;

	// Token: 0x020015DF RID: 5599
	public new class Instance : GameStateMachine<SleepChoreMonitor, SleepChoreMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060085B0 RID: 34224 RVA: 0x002ECE48 File Offset: 0x002EB048
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x060085B1 RID: 34225 RVA: 0x002ECE54 File Offset: 0x002EB054
		public void UpdateBed()
		{
			Ownables soleOwner = base.sm.masterTarget.Get(base.smi).GetComponent<MinionIdentity>().GetSoleOwner();
			Assignable assignable = soleOwner.GetAssignable(Db.Get().AssignableSlots.MedicalBed);
			Assignable assignable2;
			if (assignable != null && assignable.CanAutoAssignTo(base.sm.masterTarget.Get(base.smi).GetComponent<MinionIdentity>().assignableProxy.Get()))
			{
				assignable2 = assignable;
			}
			else
			{
				assignable2 = soleOwner.GetAssignable(Db.Get().AssignableSlots.Bed);
				if (assignable2 == null)
				{
					assignable2 = soleOwner.AutoAssignSlot(Db.Get().AssignableSlots.Bed);
					if (assignable2 != null)
					{
						base.GetComponent<Sensors>().GetSensor<AssignableReachabilitySensor>().Update();
					}
				}
			}
			base.smi.sm.bed.Set(assignable2, base.smi);
		}

		// Token: 0x060085B2 RID: 34226 RVA: 0x002ECF3E File Offset: 0x002EB13E
		public bool HasSleepUrge()
		{
			return base.GetComponent<ChoreConsumer>().HasUrge(Db.Get().Urges.Sleep);
		}

		// Token: 0x060085B3 RID: 34227 RVA: 0x002ECF5C File Offset: 0x002EB15C
		public bool IsBedReachable()
		{
			AssignableReachabilitySensor sensor = base.GetComponent<Sensors>().GetSensor<AssignableReachabilitySensor>();
			return sensor.IsReachable(Db.Get().AssignableSlots.Bed) || sensor.IsReachable(Db.Get().AssignableSlots.MedicalBed);
		}

		// Token: 0x060085B4 RID: 34228 RVA: 0x002ECFA3 File Offset: 0x002EB1A3
		public GameObject CreatePassedOutLocator()
		{
			Sleepable safeFloorLocator = SleepChore.GetSafeFloorLocator(base.master.gameObject);
			safeFloorLocator.effectName = "PassedOutSleep";
			safeFloorLocator.wakeEffects = new List<string> { "SoreBack" };
			safeFloorLocator.stretchOnWake = false;
			return safeFloorLocator.gameObject;
		}

		// Token: 0x060085B5 RID: 34229 RVA: 0x002ECFE2 File Offset: 0x002EB1E2
		public GameObject CreateFloorLocator()
		{
			Sleepable safeFloorLocator = SleepChore.GetSafeFloorLocator(base.master.gameObject);
			safeFloorLocator.effectName = "FloorSleep";
			safeFloorLocator.wakeEffects = new List<string> { "SoreBack" };
			safeFloorLocator.stretchOnWake = false;
			return safeFloorLocator.gameObject;
		}

		// Token: 0x04006824 RID: 26660
		private int locatorCell;

		// Token: 0x04006825 RID: 26661
		public GameObject locator;
	}
}
