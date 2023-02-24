using System;

// Token: 0x02000863 RID: 2147
[SkipSaveFileSerialization]
public class SensitiveFeet : StateMachineComponent<SensitiveFeet.StatesInstance>
{
	// Token: 0x06003DAB RID: 15787 RVA: 0x00158AEE File Offset: 0x00156CEE
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x06003DAC RID: 15788 RVA: 0x00158AFC File Offset: 0x00156CFC
	protected bool IsUncomfortable()
	{
		int num = Grid.CellBelow(Grid.PosToCell(base.gameObject));
		return Grid.IsValidCell(num) && Grid.Solid[num] && Grid.Objects[num, 9] == null;
	}

	// Token: 0x02001629 RID: 5673
	public class StatesInstance : GameStateMachine<SensitiveFeet.States, SensitiveFeet.StatesInstance, SensitiveFeet, object>.GameInstance
	{
		// Token: 0x060086E5 RID: 34533 RVA: 0x002F032A File Offset: 0x002EE52A
		public StatesInstance(SensitiveFeet master)
			: base(master)
		{
		}
	}

	// Token: 0x0200162A RID: 5674
	public class States : GameStateMachine<SensitiveFeet.States, SensitiveFeet.StatesInstance, SensitiveFeet>
	{
		// Token: 0x060086E6 RID: 34534 RVA: 0x002F0334 File Offset: 0x002EE534
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.satisfied;
			this.root.Update("SensitiveFeetCheck", delegate(SensitiveFeet.StatesInstance smi, float dt)
			{
				if (smi.master.IsUncomfortable())
				{
					smi.GoTo(this.suffering);
					return;
				}
				smi.GoTo(this.satisfied);
			}, UpdateRate.SIM_1000ms, false);
			this.suffering.AddEffect("UncomfortableFeet").ToggleExpression(Db.Get().Expressions.Uncomfortable, null);
			this.satisfied.DoNothing();
		}

		// Token: 0x04006925 RID: 26917
		public GameStateMachine<SensitiveFeet.States, SensitiveFeet.StatesInstance, SensitiveFeet, object>.State satisfied;

		// Token: 0x04006926 RID: 26918
		public GameStateMachine<SensitiveFeet.States, SensitiveFeet.StatesInstance, SensitiveFeet, object>.State suffering;
	}
}
