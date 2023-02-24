using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200038E RID: 910
public class PeeChore : Chore<PeeChore.StatesInstance>
{
	// Token: 0x06001281 RID: 4737 RVA: 0x00062FD4 File Offset: 0x000611D4
	public PeeChore(IStateMachineTarget target)
		: base(Db.Get().ChoreTypes.Pee, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new PeeChore.StatesInstance(this, target.gameObject);
	}

	// Token: 0x02000F80 RID: 3968
	public class StatesInstance : GameStateMachine<PeeChore.States, PeeChore.StatesInstance, PeeChore, object>.GameInstance
	{
		// Token: 0x06006FAB RID: 28587 RVA: 0x002A3294 File Offset: 0x002A1494
		public StatesInstance(PeeChore master, GameObject worker)
			: base(master)
		{
			this.bladder = Db.Get().Amounts.Bladder.Lookup(worker);
			this.bodyTemperature = Db.Get().Amounts.Temperature.Lookup(worker);
			base.sm.worker.Set(worker, base.smi, false);
		}

		// Token: 0x06006FAC RID: 28588 RVA: 0x002A3339 File Offset: 0x002A1539
		public bool IsDonePeeing()
		{
			return this.bladder.value <= 0f;
		}

		// Token: 0x06006FAD RID: 28589 RVA: 0x002A3350 File Offset: 0x002A1550
		public void SpawnDirtyWater(float dt)
		{
			int num = Grid.PosToCell(base.sm.worker.Get<KMonoBehaviour>(base.smi));
			byte index = Db.Get().Diseases.GetIndex("FoodPoisoning");
			float num2 = dt * -this.bladder.GetDelta() / this.bladder.GetMax();
			if (num2 > 0f)
			{
				float num3 = 2f * num2;
				Equippable equippable = base.GetComponent<SuitEquipper>().IsWearingAirtightSuit();
				if (equippable != null)
				{
					equippable.GetComponent<Storage>().AddLiquid(SimHashes.DirtyWater, num3, this.bodyTemperature.value, index, Mathf.CeilToInt(100000f * num2), false, true);
					return;
				}
				SimMessages.AddRemoveSubstance(num, SimHashes.DirtyWater, CellEventLogger.Instance.Vomit, num3, this.bodyTemperature.value, index, Mathf.CeilToInt(100000f * num2), true, -1);
			}
		}

		// Token: 0x040054B0 RID: 21680
		public Notification stressfullyEmptyingBladder = new Notification(DUPLICANTS.STATUSITEMS.STRESSFULLYEMPTYINGBLADDER.NOTIFICATION_NAME, NotificationType.Bad, (List<Notification> notificationList, object data) => DUPLICANTS.STATUSITEMS.STRESSFULLYEMPTYINGBLADDER.NOTIFICATION_TOOLTIP + notificationList.ReduceMessages(false), null, true, 0f, null, null, null, true, false, false);

		// Token: 0x040054B1 RID: 21681
		public AmountInstance bladder;

		// Token: 0x040054B2 RID: 21682
		private AmountInstance bodyTemperature;
	}

	// Token: 0x02000F81 RID: 3969
	public class States : GameStateMachine<PeeChore.States, PeeChore.StatesInstance, PeeChore>
	{
		// Token: 0x06006FAE RID: 28590 RVA: 0x002A3434 File Offset: 0x002A1634
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.running;
			base.Target(this.worker);
			this.running.ToggleAnims("anim_expel_kanim", 0f, "").ToggleEffect("StressfulyEmptyingBladder").DoNotification((PeeChore.StatesInstance smi) => smi.stressfullyEmptyingBladder)
				.DoReport(ReportManager.ReportType.ToiletIncident, (PeeChore.StatesInstance smi) => 1f, (PeeChore.StatesInstance smi) => this.masterTarget.Get(smi).GetProperName())
				.DoTutorial(Tutorial.TutorialMessages.TM_Mopping)
				.Transition(null, (PeeChore.StatesInstance smi) => smi.IsDonePeeing(), UpdateRate.SIM_200ms)
				.Update("SpawnDirtyWater", delegate(PeeChore.StatesInstance smi, float dt)
				{
					smi.SpawnDirtyWater(dt);
				}, UpdateRate.SIM_200ms, false)
				.PlayAnim("working_loop", KAnim.PlayMode.Loop)
				.ToggleTag(GameTags.MakingMess)
				.Enter(delegate(PeeChore.StatesInstance smi)
				{
					if (Sim.IsRadiationEnabled() && smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.RadiationBalance).value > 0f)
					{
						smi.master.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, null);
					}
				})
				.Exit(delegate(PeeChore.StatesInstance smi)
				{
					if (Sim.IsRadiationEnabled())
					{
						smi.master.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, false);
						AmountInstance amountInstance = smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.RadiationBalance.Id);
						RadiationMonitor.Instance smi2 = smi.master.gameObject.GetSMI<RadiationMonitor.Instance>();
						if (smi2 != null)
						{
							float num = Math.Min(amountInstance.value, 100f * smi2.difficultySettingMod);
							smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.RadiationBalance.Id).ApplyDelta(-num);
							if (num >= 1f)
							{
								PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, Mathf.FloorToInt(num).ToString() + UI.UNITSUFFIXES.RADIATION.RADS, smi.master.transform, 1.5f, false);
							}
						}
					}
				});
		}

		// Token: 0x040054B3 RID: 21683
		public StateMachine<PeeChore.States, PeeChore.StatesInstance, PeeChore, object>.TargetParameter worker;

		// Token: 0x040054B4 RID: 21684
		public GameStateMachine<PeeChore.States, PeeChore.StatesInstance, PeeChore, object>.State running;
	}
}
