using System;
using UnityEngine;

// Token: 0x02000377 RID: 887
public class BeIncapacitatedChore : Chore<BeIncapacitatedChore.StatesInstance>
{
	// Token: 0x06001225 RID: 4645 RVA: 0x000600CC File Offset: 0x0005E2CC
	public void FindAvailableMedicalBed(Navigator navigator)
	{
		Clinic clinic = null;
		AssignableSlot clinic2 = Db.Get().AssignableSlots.Clinic;
		Ownables soleOwner = this.gameObject.GetComponent<MinionIdentity>().GetSoleOwner();
		AssignableSlotInstance slot = soleOwner.GetSlot(clinic2);
		if (slot.assignable == null)
		{
			Assignable assignable = soleOwner.AutoAssignSlot(clinic2);
			if (assignable != null)
			{
				clinic = assignable.GetComponent<Clinic>();
			}
		}
		else
		{
			clinic = slot.assignable.GetComponent<Clinic>();
		}
		if (clinic != null && navigator.CanReach(clinic))
		{
			base.smi.sm.clinic.Set(clinic.gameObject, base.smi, false);
			base.smi.GoTo(base.smi.sm.incapacitation_root.rescue.waitingForPickup);
		}
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x00060195 File Offset: 0x0005E395
	public GameObject GetChosenClinic()
	{
		return base.smi.sm.clinic.Get(base.smi);
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x000601B4 File Offset: 0x0005E3B4
	public BeIncapacitatedChore(IStateMachineTarget master)
		: base(Db.Get().ChoreTypes.BeIncapacitated, master, master.GetComponent<ChoreProvider>(), true, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new BeIncapacitatedChore.StatesInstance(this);
	}

	// Token: 0x040009B4 RID: 2484
	private static string IncapacitatedDuplicantAnim_pre = "incapacitate_pre";

	// Token: 0x040009B5 RID: 2485
	private static string IncapacitatedDuplicantAnim_loop = "incapacitate_loop";

	// Token: 0x040009B6 RID: 2486
	private static string IncapacitatedDuplicantAnim_death = "incapacitate_death";

	// Token: 0x040009B7 RID: 2487
	private static string IncapacitatedDuplicantAnim_carry = "carry_loop";

	// Token: 0x040009B8 RID: 2488
	private static string IncapacitatedDuplicantAnim_place = "place";

	// Token: 0x02000F51 RID: 3921
	public class StatesInstance : GameStateMachine<BeIncapacitatedChore.States, BeIncapacitatedChore.StatesInstance, BeIncapacitatedChore, object>.GameInstance
	{
		// Token: 0x06006EF5 RID: 28405 RVA: 0x0029E767 File Offset: 0x0029C967
		public StatesInstance(BeIncapacitatedChore master)
			: base(master)
		{
		}
	}

	// Token: 0x02000F52 RID: 3922
	public class States : GameStateMachine<BeIncapacitatedChore.States, BeIncapacitatedChore.StatesInstance, BeIncapacitatedChore>
	{
		// Token: 0x06006EF6 RID: 28406 RVA: 0x0029E770 File Offset: 0x0029C970
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			this.root.ToggleAnims("anim_incapacitated_kanim", 0f, "").ToggleStatusItem(Db.Get().DuplicantStatusItems.Incapacitated, (BeIncapacitatedChore.StatesInstance smi) => smi.master.gameObject.GetSMI<IncapacitationMonitor.Instance>()).Enter(delegate(BeIncapacitatedChore.StatesInstance smi)
			{
				smi.SetStatus(StateMachine.Status.Failed);
				smi.GoTo(this.incapacitation_root.lookingForBed);
			});
			this.incapacitation_root.EventHandler(GameHashes.Died, delegate(BeIncapacitatedChore.StatesInstance smi)
			{
				smi.SetStatus(StateMachine.Status.Failed);
				smi.StopSM("died");
			});
			this.incapacitation_root.lookingForBed.Update("LookForAvailableClinic", delegate(BeIncapacitatedChore.StatesInstance smi, float dt)
			{
				smi.master.FindAvailableMedicalBed(smi.master.GetComponent<Navigator>());
			}, UpdateRate.SIM_1000ms, false).Enter("PlayAnim", delegate(BeIncapacitatedChore.StatesInstance smi)
			{
				smi.sm.clinic.Set(null, smi);
				smi.Play(BeIncapacitatedChore.IncapacitatedDuplicantAnim_pre, KAnim.PlayMode.Once);
				smi.Queue(BeIncapacitatedChore.IncapacitatedDuplicantAnim_loop, KAnim.PlayMode.Loop);
			});
			this.incapacitation_root.rescue.ToggleChore((BeIncapacitatedChore.StatesInstance smi) => new RescueIncapacitatedChore(smi.master, this.masterTarget.Get(smi)), this.incapacitation_root.recovering, this.incapacitation_root.lookingForBed);
			this.incapacitation_root.rescue.waitingForPickup.EventTransition(GameHashes.OnStore, this.incapacitation_root.rescue.carried, null).Update("LookForAvailableClinic", delegate(BeIncapacitatedChore.StatesInstance smi, float dt)
			{
				bool flag = false;
				if (smi.sm.clinic.Get(smi) == null)
				{
					flag = true;
				}
				else if (!smi.master.gameObject.GetComponent<Navigator>().CanReach(this.clinic.Get(smi).GetComponent<Clinic>()))
				{
					flag = true;
				}
				else if (!this.clinic.Get(smi).GetComponent<Assignable>().IsAssignedTo(smi.master.GetComponent<IAssignableIdentity>()))
				{
					flag = true;
				}
				if (flag)
				{
					smi.GoTo(this.incapacitation_root.lookingForBed);
				}
			}, UpdateRate.SIM_1000ms, false);
			this.incapacitation_root.rescue.carried.Update("LookForAvailableClinic", delegate(BeIncapacitatedChore.StatesInstance smi, float dt)
			{
				bool flag2 = false;
				if (smi.sm.clinic.Get(smi) == null)
				{
					flag2 = true;
				}
				else if (!this.clinic.Get(smi).GetComponent<Assignable>().IsAssignedTo(smi.master.GetComponent<IAssignableIdentity>()))
				{
					flag2 = true;
				}
				if (flag2)
				{
					smi.GoTo(this.incapacitation_root.lookingForBed);
				}
			}, UpdateRate.SIM_1000ms, false).Enter(delegate(BeIncapacitatedChore.StatesInstance smi)
			{
				smi.Queue(BeIncapacitatedChore.IncapacitatedDuplicantAnim_carry, KAnim.PlayMode.Loop);
			}).Exit(delegate(BeIncapacitatedChore.StatesInstance smi)
			{
				smi.Play(BeIncapacitatedChore.IncapacitatedDuplicantAnim_place, KAnim.PlayMode.Once);
			});
			this.incapacitation_root.death.PlayAnim(BeIncapacitatedChore.IncapacitatedDuplicantAnim_death).Enter(delegate(BeIncapacitatedChore.StatesInstance smi)
			{
				smi.SetStatus(StateMachine.Status.Failed);
				smi.StopSM("died");
			});
			this.incapacitation_root.recovering.ToggleUrge(Db.Get().Urges.HealCritical).Enter(delegate(BeIncapacitatedChore.StatesInstance smi)
			{
				smi.Trigger(-1256572400, null);
				smi.SetStatus(StateMachine.Status.Success);
				smi.StopSM("recovering");
			});
		}

		// Token: 0x040053DE RID: 21470
		public BeIncapacitatedChore.States.IncapacitatedStates incapacitation_root;

		// Token: 0x040053DF RID: 21471
		public StateMachine<BeIncapacitatedChore.States, BeIncapacitatedChore.StatesInstance, BeIncapacitatedChore, object>.TargetParameter clinic;

		// Token: 0x02001EA4 RID: 7844
		public class IncapacitatedStates : GameStateMachine<BeIncapacitatedChore.States, BeIncapacitatedChore.StatesInstance, BeIncapacitatedChore, object>.State
		{
			// Token: 0x04008947 RID: 35143
			public GameStateMachine<BeIncapacitatedChore.States, BeIncapacitatedChore.StatesInstance, BeIncapacitatedChore, object>.State lookingForBed;

			// Token: 0x04008948 RID: 35144
			public BeIncapacitatedChore.States.BeingRescued rescue;

			// Token: 0x04008949 RID: 35145
			public GameStateMachine<BeIncapacitatedChore.States, BeIncapacitatedChore.StatesInstance, BeIncapacitatedChore, object>.State death;

			// Token: 0x0400894A RID: 35146
			public GameStateMachine<BeIncapacitatedChore.States, BeIncapacitatedChore.StatesInstance, BeIncapacitatedChore, object>.State recovering;
		}

		// Token: 0x02001EA5 RID: 7845
		public class BeingRescued : GameStateMachine<BeIncapacitatedChore.States, BeIncapacitatedChore.StatesInstance, BeIncapacitatedChore, object>.State
		{
			// Token: 0x0400894B RID: 35147
			public GameStateMachine<BeIncapacitatedChore.States, BeIncapacitatedChore.StatesInstance, BeIncapacitatedChore, object>.State waitingForPickup;

			// Token: 0x0400894C RID: 35148
			public GameStateMachine<BeIncapacitatedChore.States, BeIncapacitatedChore.StatesInstance, BeIncapacitatedChore, object>.State carried;
		}
	}
}
