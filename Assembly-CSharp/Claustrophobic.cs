using System;

// Token: 0x0200085E RID: 2142
[SkipSaveFileSerialization]
public class Claustrophobic : StateMachineComponent<Claustrophobic.StatesInstance>
{
	// Token: 0x06003D9D RID: 15773 RVA: 0x001588FE File Offset: 0x00156AFE
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x06003D9E RID: 15774 RVA: 0x0015890C File Offset: 0x00156B0C
	protected bool IsUncomfortable()
	{
		int num = 4;
		int num2 = Grid.PosToCell(base.gameObject);
		for (int i = 0; i < num - 1; i++)
		{
			int num3 = Grid.OffsetCell(num2, 0, i);
			if (Grid.IsValidCell(num3) && Grid.Solid[num3])
			{
				return true;
			}
			if (Grid.IsValidCell(Grid.CellRight(num2)) && Grid.IsValidCell(Grid.CellLeft(num2)) && Grid.Solid[Grid.CellRight(num2)] && Grid.Solid[Grid.CellLeft(num2)])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0200161F RID: 5663
	public class StatesInstance : GameStateMachine<Claustrophobic.States, Claustrophobic.StatesInstance, Claustrophobic, object>.GameInstance
	{
		// Token: 0x060086D0 RID: 34512 RVA: 0x002F003F File Offset: 0x002EE23F
		public StatesInstance(Claustrophobic master)
			: base(master)
		{
		}
	}

	// Token: 0x02001620 RID: 5664
	public class States : GameStateMachine<Claustrophobic.States, Claustrophobic.StatesInstance, Claustrophobic>
	{
		// Token: 0x060086D1 RID: 34513 RVA: 0x002F0048 File Offset: 0x002EE248
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.satisfied;
			this.root.Update("ClaustrophobicCheck", delegate(Claustrophobic.StatesInstance smi, float dt)
			{
				if (smi.master.IsUncomfortable())
				{
					smi.GoTo(this.suffering);
					return;
				}
				smi.GoTo(this.satisfied);
			}, UpdateRate.SIM_1000ms, false);
			this.suffering.AddEffect("Claustrophobic").ToggleExpression(Db.Get().Expressions.Uncomfortable, null);
			this.satisfied.DoNothing();
		}

		// Token: 0x0400691D RID: 26909
		public GameStateMachine<Claustrophobic.States, Claustrophobic.StatesInstance, Claustrophobic, object>.State satisfied;

		// Token: 0x0400691E RID: 26910
		public GameStateMachine<Claustrophobic.States, Claustrophobic.StatesInstance, Claustrophobic, object>.State suffering;
	}
}
