using System;

// Token: 0x02000864 RID: 2148
[SkipSaveFileSerialization]
public class SolitarySleeper : StateMachineComponent<SolitarySleeper.StatesInstance>
{
	// Token: 0x06003DAE RID: 15790 RVA: 0x00158B4F File Offset: 0x00156D4F
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x06003DAF RID: 15791 RVA: 0x00158B5C File Offset: 0x00156D5C
	protected bool IsUncomfortable()
	{
		if (!base.gameObject.GetSMI<StaminaMonitor.Instance>().IsSleeping())
		{
			return false;
		}
		int num = 5;
		bool flag = true;
		bool flag2 = true;
		int num2 = Grid.PosToCell(base.gameObject);
		for (int i = 1; i < num; i++)
		{
			int num3 = Grid.OffsetCell(num2, i, 0);
			int num4 = Grid.OffsetCell(num2, -i, 0);
			if (Grid.Solid[num4])
			{
				flag = false;
			}
			if (Grid.Solid[num3])
			{
				flag2 = false;
			}
			foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
			{
				if (flag && Grid.PosToCell(minionIdentity.gameObject) == num4)
				{
					return true;
				}
				if (flag2 && Grid.PosToCell(minionIdentity.gameObject) == num3)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0200162B RID: 5675
	public class StatesInstance : GameStateMachine<SolitarySleeper.States, SolitarySleeper.StatesInstance, SolitarySleeper, object>.GameInstance
	{
		// Token: 0x060086E9 RID: 34537 RVA: 0x002F03CA File Offset: 0x002EE5CA
		public StatesInstance(SolitarySleeper master)
			: base(master)
		{
		}
	}

	// Token: 0x0200162C RID: 5676
	public class States : GameStateMachine<SolitarySleeper.States, SolitarySleeper.StatesInstance, SolitarySleeper>
	{
		// Token: 0x060086EA RID: 34538 RVA: 0x002F03D4 File Offset: 0x002EE5D4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.satisfied;
			this.root.TagTransition(GameTags.Dead, null, false).EventTransition(GameHashes.NewDay, this.satisfied, null).Update("SolitarySleeperCheck", delegate(SolitarySleeper.StatesInstance smi, float dt)
			{
				if (smi.master.IsUncomfortable())
				{
					if (smi.GetCurrentState() != this.suffering)
					{
						smi.GoTo(this.suffering);
						return;
					}
				}
				else if (smi.GetCurrentState() != this.satisfied)
				{
					smi.GoTo(this.satisfied);
				}
			}, UpdateRate.SIM_4000ms, false);
			this.suffering.AddEffect("PeopleTooCloseWhileSleeping").ToggleExpression(Db.Get().Expressions.Uncomfortable, null).Update("PeopleTooCloseSleepFail", delegate(SolitarySleeper.StatesInstance smi, float dt)
			{
				smi.master.gameObject.Trigger(1338475637, this);
			}, UpdateRate.SIM_1000ms, false);
			this.satisfied.DoNothing();
		}

		// Token: 0x04006927 RID: 26919
		public GameStateMachine<SolitarySleeper.States, SolitarySleeper.StatesInstance, SolitarySleeper, object>.State satisfied;

		// Token: 0x04006928 RID: 26920
		public GameStateMachine<SolitarySleeper.States, SolitarySleeper.StatesInstance, SolitarySleeper, object>.State suffering;
	}
}
