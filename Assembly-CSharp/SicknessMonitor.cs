using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000843 RID: 2115
public class SicknessMonitor : GameStateMachine<SicknessMonitor, SicknessMonitor.Instance>
{
	// Token: 0x06003CFA RID: 15610 RVA: 0x001547AC File Offset: 0x001529AC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		default_state = this.healthy;
		this.healthy.EventTransition(GameHashes.SicknessAdded, this.sick, (SicknessMonitor.Instance smi) => smi.IsSick());
		this.sick.DefaultState(this.sick.minor).EventTransition(GameHashes.SicknessCured, this.post_nocheer, (SicknessMonitor.Instance smi) => !smi.IsSick()).ToggleThought(Db.Get().Thoughts.GotInfected, null);
		this.sick.minor.EventTransition(GameHashes.SicknessAdded, this.sick.major, (SicknessMonitor.Instance smi) => smi.HasMajorDisease());
		this.sick.major.EventTransition(GameHashes.SicknessCured, this.sick.minor, (SicknessMonitor.Instance smi) => !smi.HasMajorDisease()).ToggleUrge(Db.Get().Urges.RestDueToDisease).Update("AutoAssignClinic", delegate(SicknessMonitor.Instance smi, float dt)
		{
			smi.AutoAssignClinic();
		}, UpdateRate.SIM_4000ms, false)
			.Exit(delegate(SicknessMonitor.Instance smi)
			{
				smi.UnassignClinic();
			});
		this.post_nocheer.Enter(delegate(SicknessMonitor.Instance smi)
		{
			new SicknessCuredFX.Instance(smi.master, new Vector3(0f, 0f, -0.1f)).StartSM();
			if (smi.IsSleepingOrSleepSchedule())
			{
				smi.GoTo(this.healthy);
				return;
			}
			smi.GoTo(this.post);
		});
		this.post.ToggleChore((SicknessMonitor.Instance smi) => new EmoteChore(smi.master, Db.Get().ChoreTypes.EmoteHighPriority, SicknessMonitor.SickPostKAnim, SicknessMonitor.SickPostAnims, KAnim.PlayMode.Once, false), this.healthy);
	}

	// Token: 0x040027DC RID: 10204
	public GameStateMachine<SicknessMonitor, SicknessMonitor.Instance, IStateMachineTarget, object>.State healthy;

	// Token: 0x040027DD RID: 10205
	public SicknessMonitor.SickStates sick;

	// Token: 0x040027DE RID: 10206
	public GameStateMachine<SicknessMonitor, SicknessMonitor.Instance, IStateMachineTarget, object>.State post;

	// Token: 0x040027DF RID: 10207
	public GameStateMachine<SicknessMonitor, SicknessMonitor.Instance, IStateMachineTarget, object>.State post_nocheer;

	// Token: 0x040027E0 RID: 10208
	private static readonly HashedString SickPostKAnim = "anim_cheer_kanim";

	// Token: 0x040027E1 RID: 10209
	private static readonly HashedString[] SickPostAnims = new HashedString[] { "cheer_pre", "cheer_loop", "cheer_pst" };

	// Token: 0x020015DC RID: 5596
	public class SickStates : GameStateMachine<SicknessMonitor, SicknessMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04006819 RID: 26649
		public GameStateMachine<SicknessMonitor, SicknessMonitor.Instance, IStateMachineTarget, object>.State minor;

		// Token: 0x0400681A RID: 26650
		public GameStateMachine<SicknessMonitor, SicknessMonitor.Instance, IStateMachineTarget, object>.State major;
	}

	// Token: 0x020015DD RID: 5597
	public new class Instance : GameStateMachine<SicknessMonitor, SicknessMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060085A0 RID: 34208 RVA: 0x002ECC3A File Offset: 0x002EAE3A
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.sicknesses = master.GetComponent<MinionModifiers>().sicknesses;
		}

		// Token: 0x060085A1 RID: 34209 RVA: 0x002ECC54 File Offset: 0x002EAE54
		private string OnGetToolTip(List<Notification> notifications, object data)
		{
			return DUPLICANTS.STATUSITEMS.HASDISEASE.TOOLTIP;
		}

		// Token: 0x060085A2 RID: 34210 RVA: 0x002ECC60 File Offset: 0x002EAE60
		public bool IsSick()
		{
			return this.sicknesses.Count > 0;
		}

		// Token: 0x060085A3 RID: 34211 RVA: 0x002ECC70 File Offset: 0x002EAE70
		public bool HasMajorDisease()
		{
			using (IEnumerator<SicknessInstance> enumerator = this.sicknesses.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.modifier.severity >= Sickness.Severity.Major)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060085A4 RID: 34212 RVA: 0x002ECCCC File Offset: 0x002EAECC
		public void AutoAssignClinic()
		{
			Ownables soleOwner = base.sm.masterTarget.Get(base.smi).GetComponent<MinionIdentity>().GetSoleOwner();
			AssignableSlot clinic = Db.Get().AssignableSlots.Clinic;
			AssignableSlotInstance slot = soleOwner.GetSlot(clinic);
			if (slot == null)
			{
				return;
			}
			if (slot.assignable != null)
			{
				return;
			}
			soleOwner.AutoAssignSlot(clinic);
		}

		// Token: 0x060085A5 RID: 34213 RVA: 0x002ECD30 File Offset: 0x002EAF30
		public void UnassignClinic()
		{
			Assignables soleOwner = base.sm.masterTarget.Get(base.smi).GetComponent<MinionIdentity>().GetSoleOwner();
			AssignableSlot clinic = Db.Get().AssignableSlots.Clinic;
			AssignableSlotInstance slot = soleOwner.GetSlot(clinic);
			if (slot != null)
			{
				slot.Unassign(true);
			}
		}

		// Token: 0x060085A6 RID: 34214 RVA: 0x002ECD80 File Offset: 0x002EAF80
		public bool IsSleepingOrSleepSchedule()
		{
			Schedulable component = base.GetComponent<Schedulable>();
			if (component != null && component.IsAllowed(Db.Get().ScheduleBlockTypes.Sleep))
			{
				return true;
			}
			KPrefabID component2 = base.GetComponent<KPrefabID>();
			return component2 != null && component2.HasTag(GameTags.Asleep);
		}

		// Token: 0x0400681B RID: 26651
		private Sicknesses sicknesses;
	}
}
