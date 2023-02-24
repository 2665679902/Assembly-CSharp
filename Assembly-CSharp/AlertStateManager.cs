using System;
using System.Collections.Generic;
using STRINGS;

// Token: 0x02000819 RID: 2073
public class AlertStateManager : GameStateMachine<AlertStateManager, AlertStateManager.Instance, IStateMachineTarget, AlertStateManager.Def>
{
	// Token: 0x06003C35 RID: 15413 RVA: 0x0014F4A0 File Offset: 0x0014D6A0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.off.ParamTransition<bool>(this.isOn, this.on, GameStateMachine<AlertStateManager, AlertStateManager.Instance, IStateMachineTarget, AlertStateManager.Def>.IsTrue);
		this.on.Exit("VignetteOff", delegate(AlertStateManager.Instance smi)
		{
			Vignette.Instance.Reset();
		}).ParamTransition<bool>(this.isRedAlert, this.on.red, (AlertStateManager.Instance smi, bool p) => this.isRedAlert.Get(smi)).ParamTransition<bool>(this.isRedAlert, this.on.yellow, (AlertStateManager.Instance smi, bool p) => this.isYellowAlert.Get(smi) && !this.isRedAlert.Get(smi))
			.ParamTransition<bool>(this.isYellowAlert, this.on.yellow, (AlertStateManager.Instance smi, bool p) => this.isYellowAlert.Get(smi) && !this.isRedAlert.Get(smi))
			.ParamTransition<bool>(this.isOn, this.off, GameStateMachine<AlertStateManager, AlertStateManager.Instance, IStateMachineTarget, AlertStateManager.Def>.IsFalse);
		this.on.red.Enter("EnterEvent", delegate(AlertStateManager.Instance smi)
		{
			Game.Instance.Trigger(1585324898, null);
		}).Exit("ExitEvent", delegate(AlertStateManager.Instance smi)
		{
			Game.Instance.Trigger(-1393151672, null);
		}).Enter("SoundsOnRedAlert", delegate(AlertStateManager.Instance smi)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("RedAlert_ON", false));
		})
			.Exit("SoundsOffRedAlert", delegate(AlertStateManager.Instance smi)
			{
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("RedAlert_OFF", false));
			})
			.ToggleNotification((AlertStateManager.Instance smi) => smi.redAlertNotification);
		this.on.yellow.Enter("EnterEvent", delegate(AlertStateManager.Instance smi)
		{
			Game.Instance.Trigger(-741654735, null);
		}).Exit("ExitEvent", delegate(AlertStateManager.Instance smi)
		{
			Game.Instance.Trigger(-2062778933, null);
		}).Enter("SoundsOnYellowAlert", delegate(AlertStateManager.Instance smi)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("YellowAlert_ON", false));
		})
			.Exit("SoundsOffRedAlert", delegate(AlertStateManager.Instance smi)
			{
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("YellowAlert_OFF", false));
			});
	}

	// Token: 0x0400272E RID: 10030
	public GameStateMachine<AlertStateManager, AlertStateManager.Instance, IStateMachineTarget, AlertStateManager.Def>.State off;

	// Token: 0x0400272F RID: 10031
	public AlertStateManager.OnStates on;

	// Token: 0x04002730 RID: 10032
	public StateMachine<AlertStateManager, AlertStateManager.Instance, IStateMachineTarget, AlertStateManager.Def>.BoolParameter isRedAlert = new StateMachine<AlertStateManager, AlertStateManager.Instance, IStateMachineTarget, AlertStateManager.Def>.BoolParameter();

	// Token: 0x04002731 RID: 10033
	public StateMachine<AlertStateManager, AlertStateManager.Instance, IStateMachineTarget, AlertStateManager.Def>.BoolParameter isYellowAlert = new StateMachine<AlertStateManager, AlertStateManager.Instance, IStateMachineTarget, AlertStateManager.Def>.BoolParameter();

	// Token: 0x04002732 RID: 10034
	public StateMachine<AlertStateManager, AlertStateManager.Instance, IStateMachineTarget, AlertStateManager.Def>.BoolParameter isOn = new StateMachine<AlertStateManager, AlertStateManager.Instance, IStateMachineTarget, AlertStateManager.Def>.BoolParameter();

	// Token: 0x02001574 RID: 5492
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001575 RID: 5493
	public class OnStates : GameStateMachine<AlertStateManager, AlertStateManager.Instance, IStateMachineTarget, AlertStateManager.Def>.State
	{
		// Token: 0x040066CA RID: 26314
		public GameStateMachine<AlertStateManager, AlertStateManager.Instance, IStateMachineTarget, AlertStateManager.Def>.State yellow;

		// Token: 0x040066CB RID: 26315
		public GameStateMachine<AlertStateManager, AlertStateManager.Instance, IStateMachineTarget, AlertStateManager.Def>.State red;
	}

	// Token: 0x02001576 RID: 5494
	public new class Instance : GameStateMachine<AlertStateManager, AlertStateManager.Instance, IStateMachineTarget, AlertStateManager.Def>.GameInstance
	{
		// Token: 0x060083EC RID: 33772 RVA: 0x002E8E8C File Offset: 0x002E708C
		public Instance(IStateMachineTarget master, AlertStateManager.Def def)
			: base(master, def)
		{
		}

		// Token: 0x060083ED RID: 33773 RVA: 0x002E8EE4 File Offset: 0x002E70E4
		public void UpdateState(float dt)
		{
			if (this.IsRedAlert())
			{
				base.smi.GoTo(base.sm.on.red);
				return;
			}
			if (this.IsYellowAlert())
			{
				base.smi.GoTo(base.sm.on.yellow);
				return;
			}
			if (!this.IsOn())
			{
				base.smi.GoTo(base.sm.off);
			}
		}

		// Token: 0x060083EE RID: 33774 RVA: 0x002E8F57 File Offset: 0x002E7157
		public bool IsOn()
		{
			return base.sm.isYellowAlert.Get(base.smi) || base.sm.isRedAlert.Get(base.smi);
		}

		// Token: 0x060083EF RID: 33775 RVA: 0x002E8F89 File Offset: 0x002E7189
		public bool IsRedAlert()
		{
			return base.sm.isRedAlert.Get(base.smi);
		}

		// Token: 0x060083F0 RID: 33776 RVA: 0x002E8FA1 File Offset: 0x002E71A1
		public bool IsYellowAlert()
		{
			return base.sm.isYellowAlert.Get(base.smi);
		}

		// Token: 0x060083F1 RID: 33777 RVA: 0x002E8FB9 File Offset: 0x002E71B9
		public bool IsRedAlertToggledOn()
		{
			return this.isToggled;
		}

		// Token: 0x060083F2 RID: 33778 RVA: 0x002E8FC1 File Offset: 0x002E71C1
		public void ToggleRedAlert(bool on)
		{
			this.isToggled = on;
			this.Refresh();
		}

		// Token: 0x060083F3 RID: 33779 RVA: 0x002E8FD0 File Offset: 0x002E71D0
		public void SetHasTopPriorityChore(bool on)
		{
			this.hasTopPriorityChore = on;
			this.Refresh();
		}

		// Token: 0x060083F4 RID: 33780 RVA: 0x002E8FE0 File Offset: 0x002E71E0
		private void Refresh()
		{
			base.sm.isYellowAlert.Set(this.hasTopPriorityChore, base.smi, false);
			base.sm.isRedAlert.Set(this.isToggled, base.smi, false);
			base.sm.isOn.Set(this.hasTopPriorityChore || this.isToggled, base.smi, false);
		}

		// Token: 0x040066CC RID: 26316
		private bool isToggled;

		// Token: 0x040066CD RID: 26317
		private bool hasTopPriorityChore;

		// Token: 0x040066CE RID: 26318
		public Notification redAlertNotification = new Notification(MISC.NOTIFICATIONS.REDALERT.NAME, NotificationType.Bad, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.REDALERT.TOOLTIP, null, false, 0f, null, null, null, true, false, false);
	}
}
