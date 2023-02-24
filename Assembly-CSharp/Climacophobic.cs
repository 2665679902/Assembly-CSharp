using System;
using UnityEngine;

// Token: 0x0200085F RID: 2143
[SkipSaveFileSerialization]
public class Climacophobic : StateMachineComponent<Climacophobic.StatesInstance>
{
	// Token: 0x06003DA0 RID: 15776 RVA: 0x0015899E File Offset: 0x00156B9E
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x06003DA1 RID: 15777 RVA: 0x001589AC File Offset: 0x00156BAC
	protected bool IsUncomfortable()
	{
		int num = 5;
		int num2 = Grid.PosToCell(base.gameObject);
		if (this.isCellLadder(num2))
		{
			int num3 = 1;
			bool flag = true;
			bool flag2 = true;
			for (int i = 1; i < num; i++)
			{
				int num4 = Grid.OffsetCell(num2, 0, i);
				int num5 = Grid.OffsetCell(num2, 0, -i);
				if (flag && this.isCellLadder(num4))
				{
					num3++;
				}
				else
				{
					flag = false;
				}
				if (flag2 && this.isCellLadder(num5))
				{
					num3++;
				}
				else
				{
					flag2 = false;
				}
			}
			return num3 >= num;
		}
		return false;
	}

	// Token: 0x06003DA2 RID: 15778 RVA: 0x00158A34 File Offset: 0x00156C34
	private bool isCellLadder(int cell)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		GameObject gameObject = Grid.Objects[cell, 1];
		return !(gameObject == null) && !(gameObject.GetComponent<Ladder>() == null);
	}

	// Token: 0x02001621 RID: 5665
	public class StatesInstance : GameStateMachine<Climacophobic.States, Climacophobic.StatesInstance, Climacophobic, object>.GameInstance
	{
		// Token: 0x060086D4 RID: 34516 RVA: 0x002F00DE File Offset: 0x002EE2DE
		public StatesInstance(Climacophobic master)
			: base(master)
		{
		}
	}

	// Token: 0x02001622 RID: 5666
	public class States : GameStateMachine<Climacophobic.States, Climacophobic.StatesInstance, Climacophobic>
	{
		// Token: 0x060086D5 RID: 34517 RVA: 0x002F00E8 File Offset: 0x002EE2E8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.satisfied;
			this.root.Update("ClimacophobicCheck", delegate(Climacophobic.StatesInstance smi, float dt)
			{
				if (smi.master.IsUncomfortable())
				{
					smi.GoTo(this.suffering);
					return;
				}
				smi.GoTo(this.satisfied);
			}, UpdateRate.SIM_1000ms, false);
			this.suffering.AddEffect("Vertigo").ToggleExpression(Db.Get().Expressions.Uncomfortable, null);
			this.satisfied.DoNothing();
		}

		// Token: 0x0400691F RID: 26911
		public GameStateMachine<Climacophobic.States, Climacophobic.StatesInstance, Climacophobic, object>.State satisfied;

		// Token: 0x04006920 RID: 26912
		public GameStateMachine<Climacophobic.States, Climacophobic.StatesInstance, Climacophobic, object>.State suffering;
	}
}
