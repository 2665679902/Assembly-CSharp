using System;
using Klei;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200039D RID: 925
public class VomitChore : Chore<VomitChore.StatesInstance>
{
	// Token: 0x060012A3 RID: 4771 RVA: 0x00063D04 File Offset: 0x00061F04
	public VomitChore(ChoreType chore_type, IStateMachineTarget target, StatusItem status_item, Notification notification, Action<Chore> on_complete = null)
		: base(Db.Get().ChoreTypes.Vomit, target, target.GetComponent<ChoreProvider>(), true, on_complete, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new VomitChore.StatesInstance(this, target.gameObject, status_item, notification);
	}

	// Token: 0x02000FA3 RID: 4003
	public class StatesInstance : GameStateMachine<VomitChore.States, VomitChore.StatesInstance, VomitChore, object>.GameInstance
	{
		// Token: 0x0600701B RID: 28699 RVA: 0x002A5B20 File Offset: 0x002A3D20
		public StatesInstance(VomitChore master, GameObject vomiter, StatusItem status_item, Notification notification)
			: base(master)
		{
			base.sm.vomiter.Set(vomiter, base.smi, false);
			this.bodyTemperature = Db.Get().Amounts.Temperature.Lookup(vomiter);
			this.statusItem = status_item;
			this.notification = notification;
			this.vomitCellQuery = new SafetyQuery(Game.Instance.safetyConditions.VomitCellChecker, base.GetComponent<KMonoBehaviour>(), 10);
		}

		// Token: 0x0600701C RID: 28700 RVA: 0x002A5B9C File Offset: 0x002A3D9C
		private static bool CanEmitLiquid(int cell)
		{
			bool flag = true;
			if (Grid.Solid[cell] || (Grid.Properties[cell] & 2) != 0)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600701D RID: 28701 RVA: 0x002A5BCC File Offset: 0x002A3DCC
		public void SpawnDirtyWater(float dt)
		{
			if (dt > 0f)
			{
				float totalTime = base.GetComponent<KBatchedAnimController>().CurrentAnim.totalTime;
				float num = dt / totalTime;
				Sicknesses sicknesses = base.master.GetComponent<MinionModifiers>().sicknesses;
				SimUtil.DiseaseInfo invalid = SimUtil.DiseaseInfo.Invalid;
				int num2 = 0;
				while (num2 < sicknesses.Count && sicknesses[num2].modifier.sicknessType != Sickness.SicknessType.Pathogen)
				{
					num2++;
				}
				Facing component = base.sm.vomiter.Get(base.smi).GetComponent<Facing>();
				int num3 = Grid.PosToCell(component.transform.GetPosition());
				int num4 = component.GetFrontCell();
				if (!VomitChore.StatesInstance.CanEmitLiquid(num4))
				{
					num4 = num3;
				}
				Equippable equippable = base.GetComponent<SuitEquipper>().IsWearingAirtightSuit();
				if (equippable != null)
				{
					equippable.GetComponent<Storage>().AddLiquid(SimHashes.DirtyWater, STRESS.VOMIT_AMOUNT * num, this.bodyTemperature.value, invalid.idx, invalid.count, false, true);
					return;
				}
				SimMessages.AddRemoveSubstance(num4, SimHashes.DirtyWater, CellEventLogger.Instance.Vomit, STRESS.VOMIT_AMOUNT * num, this.bodyTemperature.value, invalid.idx, invalid.count, true, -1);
			}
		}

		// Token: 0x0600701E RID: 28702 RVA: 0x002A5CFC File Offset: 0x002A3EFC
		public int GetVomitCell()
		{
			this.vomitCellQuery.Reset();
			Navigator component = base.GetComponent<Navigator>();
			component.RunQuery(this.vomitCellQuery);
			int num = this.vomitCellQuery.GetResultCell();
			if (Grid.InvalidCell == num)
			{
				num = Grid.PosToCell(component);
			}
			return num;
		}

		// Token: 0x0400550F RID: 21775
		public StatusItem statusItem;

		// Token: 0x04005510 RID: 21776
		private AmountInstance bodyTemperature;

		// Token: 0x04005511 RID: 21777
		public Notification notification;

		// Token: 0x04005512 RID: 21778
		private SafetyQuery vomitCellQuery;
	}

	// Token: 0x02000FA4 RID: 4004
	public class States : GameStateMachine<VomitChore.States, VomitChore.StatesInstance, VomitChore>
	{
		// Token: 0x0600701F RID: 28703 RVA: 0x002A5D44 File Offset: 0x002A3F44
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.moveto;
			base.Target(this.vomiter);
			this.root.ToggleAnims("anim_emotes_default_kanim", 0f, "");
			this.moveto.TriggerOnEnter(GameHashes.BeginWalk, null).TriggerOnExit(GameHashes.EndWalk, null).ToggleAnims("anim_loco_vomiter_kanim", 0f, "")
				.MoveTo((VomitChore.StatesInstance smi) => smi.GetVomitCell(), this.vomit, this.vomit, false);
			this.vomit.DefaultState(this.vomit.buildup).ToggleAnims("anim_vomit_kanim", 0f, "").ToggleStatusItem((VomitChore.StatesInstance smi) => smi.statusItem, null)
				.DoNotification((VomitChore.StatesInstance smi) => smi.notification)
				.DoTutorial(Tutorial.TutorialMessages.TM_Mopping)
				.Enter(delegate(VomitChore.StatesInstance smi)
				{
					if (smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.RadiationBalance).value > 0f)
					{
						smi.master.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, null);
					}
				})
				.Exit(delegate(VomitChore.StatesInstance smi)
				{
					smi.master.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, false);
					float num = Mathf.Min(smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.RadiationBalance.Id).value, 20f);
					smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.RadiationBalance.Id).ApplyDelta(-num);
					if (num >= 1f)
					{
						PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, Mathf.FloorToInt(num).ToString() + UI.UNITSUFFIXES.RADIATION.RADS, smi.master.transform, 1.5f, false);
					}
				});
			this.vomit.buildup.PlayAnim("vomit_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.vomit.release);
			this.vomit.release.ToggleEffect("Vomiting").PlayAnim("vomit_loop", KAnim.PlayMode.Once).Update("SpawnDirtyWater", delegate(VomitChore.StatesInstance smi, float dt)
			{
				smi.SpawnDirtyWater(dt);
			}, UpdateRate.SIM_200ms, false)
				.OnAnimQueueComplete(this.vomit.release_pst);
			this.vomit.release_pst.PlayAnim("vomit_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.recover);
			this.recover.PlayAnim("breathe_pre").QueueAnim("breathe_loop", true, null).ScheduleGoTo(8f, this.recover_pst);
			this.recover_pst.QueueAnim("breathe_pst", false, null).OnAnimQueueComplete(this.complete);
			this.complete.ReturnSuccess();
		}

		// Token: 0x04005513 RID: 21779
		public StateMachine<VomitChore.States, VomitChore.StatesInstance, VomitChore, object>.TargetParameter vomiter;

		// Token: 0x04005514 RID: 21780
		public GameStateMachine<VomitChore.States, VomitChore.StatesInstance, VomitChore, object>.State moveto;

		// Token: 0x04005515 RID: 21781
		public VomitChore.States.VomitState vomit;

		// Token: 0x04005516 RID: 21782
		public GameStateMachine<VomitChore.States, VomitChore.StatesInstance, VomitChore, object>.State recover;

		// Token: 0x04005517 RID: 21783
		public GameStateMachine<VomitChore.States, VomitChore.StatesInstance, VomitChore, object>.State recover_pst;

		// Token: 0x04005518 RID: 21784
		public GameStateMachine<VomitChore.States, VomitChore.StatesInstance, VomitChore, object>.State complete;

		// Token: 0x02001ED3 RID: 7891
		public class VomitState : GameStateMachine<VomitChore.States, VomitChore.StatesInstance, VomitChore, object>.State
		{
			// Token: 0x04008A15 RID: 35349
			public GameStateMachine<VomitChore.States, VomitChore.StatesInstance, VomitChore, object>.State buildup;

			// Token: 0x04008A16 RID: 35350
			public GameStateMachine<VomitChore.States, VomitChore.StatesInstance, VomitChore, object>.State release;

			// Token: 0x04008A17 RID: 35351
			public GameStateMachine<VomitChore.States, VomitChore.StatesInstance, VomitChore, object>.State release_pst;
		}
	}
}
