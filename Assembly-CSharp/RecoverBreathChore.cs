using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000392 RID: 914
public class RecoverBreathChore : Chore<RecoverBreathChore.StatesInstance>
{
	// Token: 0x06001289 RID: 4745 RVA: 0x00063324 File Offset: 0x00061524
	public RecoverBreathChore(IStateMachineTarget target)
		: base(Db.Get().ChoreTypes.RecoverBreath, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new RecoverBreathChore.StatesInstance(this, target.gameObject);
	}

	// Token: 0x02000F89 RID: 3977
	public class StatesInstance : GameStateMachine<RecoverBreathChore.States, RecoverBreathChore.StatesInstance, RecoverBreathChore, object>.GameInstance
	{
		// Token: 0x06006FC8 RID: 28616 RVA: 0x002A3C8C File Offset: 0x002A1E8C
		public StatesInstance(RecoverBreathChore master, GameObject recoverer)
			: base(master)
		{
			base.sm.recoverer.Set(recoverer, base.smi, false);
			Klei.AI.Attribute deltaAttribute = Db.Get().Amounts.Breath.deltaAttribute;
			float num = 3f;
			this.recoveringbreath = new AttributeModifier(deltaAttribute.Id, num, DUPLICANTS.MODIFIERS.RECOVERINGBREATH.NAME, false, false, true);
		}

		// Token: 0x06006FC9 RID: 28617 RVA: 0x002A3CF4 File Offset: 0x002A1EF4
		public void CreateLocator()
		{
			GameObject gameObject = ChoreHelpers.CreateLocator("RecoverBreathLocator", Vector3.zero);
			base.sm.locator.Set(gameObject, this, false);
			this.UpdateLocator();
		}

		// Token: 0x06006FCA RID: 28618 RVA: 0x002A3D2C File Offset: 0x002A1F2C
		public void UpdateLocator()
		{
			int num = base.sm.recoverer.GetSMI<BreathMonitor.Instance>(base.smi).GetRecoverCell();
			if (num == Grid.InvalidCell)
			{
				num = Grid.PosToCell(base.sm.recoverer.Get<Transform>(base.smi).GetPosition());
			}
			Vector3 vector = Grid.CellToPosCBC(num, Grid.SceneLayer.Move);
			base.sm.locator.Get<Transform>(base.smi).SetPosition(vector);
		}

		// Token: 0x06006FCB RID: 28619 RVA: 0x002A3DA4 File Offset: 0x002A1FA4
		public void DestroyLocator()
		{
			ChoreHelpers.DestroyLocator(base.sm.locator.Get(this));
			base.sm.locator.Set(null, this);
		}

		// Token: 0x06006FCC RID: 28620 RVA: 0x002A3DD0 File Offset: 0x002A1FD0
		public void RemoveSuitIfNecessary()
		{
			Equipment equipment = base.sm.recoverer.Get<Equipment>(base.smi);
			if (equipment == null)
			{
				return;
			}
			Assignable assignable = equipment.GetAssignable(Db.Get().AssignableSlots.Suit);
			if (assignable == null)
			{
				return;
			}
			assignable.Unassign();
		}

		// Token: 0x040054C6 RID: 21702
		public AttributeModifier recoveringbreath;
	}

	// Token: 0x02000F8A RID: 3978
	public class States : GameStateMachine<RecoverBreathChore.States, RecoverBreathChore.StatesInstance, RecoverBreathChore>
	{
		// Token: 0x06006FCD RID: 28621 RVA: 0x002A3E24 File Offset: 0x002A2024
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.approach;
			base.Target(this.recoverer);
			this.root.Enter("CreateLocator", delegate(RecoverBreathChore.StatesInstance smi)
			{
				smi.CreateLocator();
			}).Exit("DestroyLocator", delegate(RecoverBreathChore.StatesInstance smi)
			{
				smi.DestroyLocator();
			}).Update("UpdateLocator", delegate(RecoverBreathChore.StatesInstance smi, float dt)
			{
				smi.UpdateLocator();
			}, UpdateRate.SIM_200ms, true);
			this.approach.InitializeStates(this.recoverer, this.locator, this.remove_suit, null, null, null);
			this.remove_suit.GoTo(this.recover);
			this.recover.ToggleAnims("anim_emotes_default_kanim", 0f, "").DefaultState(this.recover.pre).ToggleAttributeModifier("Recovering Breath", (RecoverBreathChore.StatesInstance smi) => smi.recoveringbreath, null)
				.ToggleTag(GameTags.RecoveringBreath)
				.TriggerOnEnter(GameHashes.BeginBreathRecovery, null)
				.TriggerOnExit(GameHashes.EndBreathRecovery, null);
			this.recover.pre.PlayAnim("breathe_pre").OnAnimQueueComplete(this.recover.loop);
			this.recover.loop.PlayAnim("breathe_loop", KAnim.PlayMode.Loop);
			this.recover.pst.QueueAnim("breathe_pst", false, null).OnAnimQueueComplete(null);
		}

		// Token: 0x040054C7 RID: 21703
		public GameStateMachine<RecoverBreathChore.States, RecoverBreathChore.StatesInstance, RecoverBreathChore, object>.ApproachSubState<IApproachable> approach;

		// Token: 0x040054C8 RID: 21704
		public GameStateMachine<RecoverBreathChore.States, RecoverBreathChore.StatesInstance, RecoverBreathChore, object>.PreLoopPostState recover;

		// Token: 0x040054C9 RID: 21705
		public GameStateMachine<RecoverBreathChore.States, RecoverBreathChore.StatesInstance, RecoverBreathChore, object>.State remove_suit;

		// Token: 0x040054CA RID: 21706
		public StateMachine<RecoverBreathChore.States, RecoverBreathChore.StatesInstance, RecoverBreathChore, object>.TargetParameter recoverer;

		// Token: 0x040054CB RID: 21707
		public StateMachine<RecoverBreathChore.States, RecoverBreathChore.StatesInstance, RecoverBreathChore, object>.TargetParameter locator;
	}
}
