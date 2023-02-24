using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200039C RID: 924
public class UglyCryChore : Chore<UglyCryChore.StatesInstance>
{
	// Token: 0x060012A2 RID: 4770 RVA: 0x00063CBC File Offset: 0x00061EBC
	public UglyCryChore(ChoreType chore_type, IStateMachineTarget target, Action<Chore> on_complete = null)
		: base(Db.Get().ChoreTypes.UglyCry, target, target.GetComponent<ChoreProvider>(), false, on_complete, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new UglyCryChore.StatesInstance(this, target.gameObject);
	}

	// Token: 0x02000FA1 RID: 4001
	public class StatesInstance : GameStateMachine<UglyCryChore.States, UglyCryChore.StatesInstance, UglyCryChore, object>.GameInstance
	{
		// Token: 0x06007016 RID: 28694 RVA: 0x002A5834 File Offset: 0x002A3A34
		public StatesInstance(UglyCryChore master, GameObject crier)
			: base(master)
		{
			base.sm.crier.Set(crier, base.smi, false);
			this.bodyTemperature = Db.Get().Amounts.Temperature.Lookup(crier);
		}

		// Token: 0x06007017 RID: 28695 RVA: 0x002A5874 File Offset: 0x002A3A74
		public void ProduceTears(float dt)
		{
			if (dt <= 0f)
			{
				return;
			}
			int num = Grid.PosToCell(base.smi.master.gameObject);
			Equippable equippable = base.GetComponent<SuitEquipper>().IsWearingAirtightSuit();
			if (equippable != null)
			{
				equippable.GetComponent<Storage>().AddLiquid(SimHashes.Water, 1f * STRESS.TEARS_RATE * dt, this.bodyTemperature.value, byte.MaxValue, 0, false, true);
				return;
			}
			SimMessages.AddRemoveSubstance(num, SimHashes.Water, CellEventLogger.Instance.Tears, 1f * STRESS.TEARS_RATE * dt, this.bodyTemperature.value, byte.MaxValue, 0, true, -1);
		}

		// Token: 0x0400550A RID: 21770
		private AmountInstance bodyTemperature;
	}

	// Token: 0x02000FA2 RID: 4002
	public class States : GameStateMachine<UglyCryChore.States, UglyCryChore.StatesInstance, UglyCryChore>
	{
		// Token: 0x06007018 RID: 28696 RVA: 0x002A591C File Offset: 0x002A3B1C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.cry;
			base.Target(this.crier);
			this.uglyCryingEffect = new Effect("UglyCrying", DUPLICANTS.MODIFIERS.UGLY_CRYING.NAME, DUPLICANTS.MODIFIERS.UGLY_CRYING.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
			this.uglyCryingEffect.Add(new AttributeModifier(Db.Get().Attributes.Decor.Id, -30f, DUPLICANTS.MODIFIERS.UGLY_CRYING.NAME, false, false, true));
			Db.Get().effects.Add(this.uglyCryingEffect);
			this.cry.defaultState = this.cry.cry_pre.RemoveEffect("CryFace").ToggleAnims("anim_cry_kanim", 0f, "");
			this.cry.cry_pre.PlayAnim("working_pre").ScheduleGoTo(2f, this.cry.cry_loop);
			this.cry.cry_loop.ToggleAnims("anim_cry_kanim", 0f, "").Enter(delegate(UglyCryChore.StatesInstance smi)
			{
				smi.Play("working_loop", KAnim.PlayMode.Loop);
			}).ScheduleGoTo(18f, this.cry.cry_pst)
				.ToggleEffect((UglyCryChore.StatesInstance smi) => this.uglyCryingEffect)
				.Update(delegate(UglyCryChore.StatesInstance smi, float dt)
				{
					smi.ProduceTears(dt);
				}, UpdateRate.SIM_200ms, false);
			this.cry.cry_pst.QueueAnim("working_pst", false, null).OnAnimQueueComplete(this.complete);
			this.complete.AddEffect("CryFace").Enter(delegate(UglyCryChore.StatesInstance smi)
			{
				smi.StopSM("complete");
			});
		}

		// Token: 0x0400550B RID: 21771
		public StateMachine<UglyCryChore.States, UglyCryChore.StatesInstance, UglyCryChore, object>.TargetParameter crier;

		// Token: 0x0400550C RID: 21772
		public UglyCryChore.States.Cry cry;

		// Token: 0x0400550D RID: 21773
		public GameStateMachine<UglyCryChore.States, UglyCryChore.StatesInstance, UglyCryChore, object>.State complete;

		// Token: 0x0400550E RID: 21774
		private Effect uglyCryingEffect;

		// Token: 0x02001ED1 RID: 7889
		public class Cry : GameStateMachine<UglyCryChore.States, UglyCryChore.StatesInstance, UglyCryChore, object>.State
		{
			// Token: 0x04008A0E RID: 35342
			public GameStateMachine<UglyCryChore.States, UglyCryChore.StatesInstance, UglyCryChore, object>.State cry_pre;

			// Token: 0x04008A0F RID: 35343
			public GameStateMachine<UglyCryChore.States, UglyCryChore.StatesInstance, UglyCryChore, object>.State cry_loop;

			// Token: 0x04008A10 RID: 35344
			public GameStateMachine<UglyCryChore.States, UglyCryChore.StatesInstance, UglyCryChore, object>.State cry_pst;
		}
	}
}
