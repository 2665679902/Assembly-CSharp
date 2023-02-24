using System;

// Token: 0x02000865 RID: 2149
[SkipSaveFileSerialization]
public class Workaholic : StateMachineComponent<Workaholic.StatesInstance>
{
	// Token: 0x06003DB1 RID: 15793 RVA: 0x00158C60 File Offset: 0x00156E60
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x06003DB2 RID: 15794 RVA: 0x00158C6D File Offset: 0x00156E6D
	protected bool IsUncomfortable()
	{
		return base.smi.master.GetComponent<ChoreDriver>().GetCurrentChore() is IdleChore;
	}

	// Token: 0x0200162D RID: 5677
	public class StatesInstance : GameStateMachine<Workaholic.States, Workaholic.StatesInstance, Workaholic, object>.GameInstance
	{
		// Token: 0x060086EE RID: 34542 RVA: 0x002F04DF File Offset: 0x002EE6DF
		public StatesInstance(Workaholic master)
			: base(master)
		{
		}
	}

	// Token: 0x0200162E RID: 5678
	public class States : GameStateMachine<Workaholic.States, Workaholic.StatesInstance, Workaholic>
	{
		// Token: 0x060086EF RID: 34543 RVA: 0x002F04E8 File Offset: 0x002EE6E8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.satisfied;
			this.root.Update("WorkaholicCheck", delegate(Workaholic.StatesInstance smi, float dt)
			{
				if (smi.master.IsUncomfortable())
				{
					smi.GoTo(this.suffering);
					return;
				}
				smi.GoTo(this.satisfied);
			}, UpdateRate.SIM_1000ms, false);
			this.suffering.AddEffect("Restless").ToggleExpression(Db.Get().Expressions.Uncomfortable, null);
			this.satisfied.DoNothing();
		}

		// Token: 0x04006929 RID: 26921
		public GameStateMachine<Workaholic.States, Workaholic.StatesInstance, Workaholic, object>.State satisfied;

		// Token: 0x0400692A RID: 26922
		public GameStateMachine<Workaholic.States, Workaholic.StatesInstance, Workaholic, object>.State suffering;
	}
}
