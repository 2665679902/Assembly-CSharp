using System;

// Token: 0x0200083F RID: 2111
public class RedAlertMonitor : GameStateMachine<RedAlertMonitor, RedAlertMonitor.Instance>
{
	// Token: 0x06003CEC RID: 15596 RVA: 0x001543BC File Offset: 0x001525BC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.off.EventTransition(GameHashes.EnteredRedAlert, (RedAlertMonitor.Instance smi) => Game.Instance, this.on, (RedAlertMonitor.Instance smi) => smi.master.gameObject.GetMyWorld().AlertManager.IsRedAlert());
		this.on.EventTransition(GameHashes.ExitedRedAlert, (RedAlertMonitor.Instance smi) => Game.Instance, this.off, (RedAlertMonitor.Instance smi) => !smi.master.gameObject.GetMyWorld().AlertManager.IsRedAlert()).Enter("EnableRedAlert", delegate(RedAlertMonitor.Instance smi)
		{
			smi.EnableRedAlert();
		}).ToggleEffect("RedAlert")
			.ToggleExpression(Db.Get().Expressions.RedAlert, null);
	}

	// Token: 0x040027D1 RID: 10193
	public GameStateMachine<RedAlertMonitor, RedAlertMonitor.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x040027D2 RID: 10194
	public GameStateMachine<RedAlertMonitor, RedAlertMonitor.Instance, IStateMachineTarget, object>.State on;

	// Token: 0x020015D6 RID: 5590
	public new class Instance : GameStateMachine<RedAlertMonitor, RedAlertMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008587 RID: 34183 RVA: 0x002EC97C File Offset: 0x002EAB7C
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x06008588 RID: 34184 RVA: 0x002EC988 File Offset: 0x002EAB88
		public void EnableRedAlert()
		{
			ChoreDriver component = base.GetComponent<ChoreDriver>();
			if (component != null)
			{
				Chore currentChore = component.GetCurrentChore();
				if (currentChore != null)
				{
					bool flag = false;
					for (int i = 0; i < currentChore.GetPreconditions().Count; i++)
					{
						if (currentChore.GetPreconditions()[i].id == ChorePreconditions.instance.IsNotRedAlert.id)
						{
							flag = true;
						}
					}
					if (flag)
					{
						component.StopChore();
					}
				}
			}
		}
	}
}
