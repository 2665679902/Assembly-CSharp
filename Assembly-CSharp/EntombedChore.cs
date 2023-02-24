using System;
using UnityEngine;

// Token: 0x02000380 RID: 896
public class EntombedChore : Chore<EntombedChore.StatesInstance>
{
	// Token: 0x06001241 RID: 4673 RVA: 0x00061848 File Offset: 0x0005FA48
	public EntombedChore(IStateMachineTarget target, string entombedAnimOverride)
		: base(Db.Get().ChoreTypes.Entombed, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new EntombedChore.StatesInstance(this, target.gameObject, entombedAnimOverride);
	}

	// Token: 0x02000F5F RID: 3935
	public class StatesInstance : GameStateMachine<EntombedChore.States, EntombedChore.StatesInstance, EntombedChore, object>.GameInstance
	{
		// Token: 0x06006F4E RID: 28494 RVA: 0x002A0682 File Offset: 0x0029E882
		public StatesInstance(EntombedChore master, GameObject entombable, string entombedAnimOverride)
			: base(master)
		{
			base.sm.entombable.Set(entombable, base.smi, false);
			this.entombedAnimOverride = entombedAnimOverride;
		}

		// Token: 0x06006F4F RID: 28495 RVA: 0x002A06AC File Offset: 0x0029E8AC
		public void UpdateFaceEntombed()
		{
			int num = Grid.CellAbove(Grid.PosToCell(base.transform.GetPosition()));
			base.sm.isFaceEntombed.Set(Grid.IsValidCell(num) && Grid.Solid[num], base.smi, false);
		}

		// Token: 0x0400543C RID: 21564
		public string entombedAnimOverride;
	}

	// Token: 0x02000F60 RID: 3936
	public class States : GameStateMachine<EntombedChore.States, EntombedChore.StatesInstance, EntombedChore>
	{
		// Token: 0x06006F50 RID: 28496 RVA: 0x002A0700 File Offset: 0x0029E900
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.entombedbody;
			base.Target(this.entombable);
			this.root.ToggleAnims((EntombedChore.StatesInstance smi) => smi.entombedAnimOverride).Update("IsFaceEntombed", delegate(EntombedChore.StatesInstance smi, float dt)
			{
				smi.UpdateFaceEntombed();
			}, UpdateRate.SIM_200ms, false).ToggleStatusItem(Db.Get().DuplicantStatusItems.EntombedChore, null);
			this.entombedface.PlayAnim("entombed_ceiling", KAnim.PlayMode.Loop).ParamTransition<bool>(this.isFaceEntombed, this.entombedbody, GameStateMachine<EntombedChore.States, EntombedChore.StatesInstance, EntombedChore, object>.IsFalse);
			this.entombedbody.PlayAnim("entombed_floor", KAnim.PlayMode.Loop).StopMoving().ParamTransition<bool>(this.isFaceEntombed, this.entombedface, GameStateMachine<EntombedChore.States, EntombedChore.StatesInstance, EntombedChore, object>.IsTrue);
		}

		// Token: 0x0400543D RID: 21565
		public StateMachine<EntombedChore.States, EntombedChore.StatesInstance, EntombedChore, object>.BoolParameter isFaceEntombed;

		// Token: 0x0400543E RID: 21566
		public StateMachine<EntombedChore.States, EntombedChore.StatesInstance, EntombedChore, object>.TargetParameter entombable;

		// Token: 0x0400543F RID: 21567
		public GameStateMachine<EntombedChore.States, EntombedChore.StatesInstance, EntombedChore, object>.State entombedface;

		// Token: 0x04005440 RID: 21568
		public GameStateMachine<EntombedChore.States, EntombedChore.StatesInstance, EntombedChore, object>.State entombedbody;
	}
}
