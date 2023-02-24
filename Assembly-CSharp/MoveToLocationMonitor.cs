using System;
using STRINGS;

// Token: 0x02000839 RID: 2105
public class MoveToLocationMonitor : GameStateMachine<MoveToLocationMonitor, MoveToLocationMonitor.Instance>
{
	// Token: 0x06003CB8 RID: 15544 RVA: 0x00152D80 File Offset: 0x00150F80
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.DoNothing();
		this.moving.ToggleChore((MoveToLocationMonitor.Instance smi) => new MoveChore(smi.master, Db.Get().ChoreTypes.MoveTo, (MoveChore.StatesInstance smii) => smi.targetCell, false), this.satisfied);
	}

	// Token: 0x040027A7 RID: 10151
	public GameStateMachine<MoveToLocationMonitor, MoveToLocationMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x040027A8 RID: 10152
	public GameStateMachine<MoveToLocationMonitor, MoveToLocationMonitor.Instance, IStateMachineTarget, object>.State moving;

	// Token: 0x020015C7 RID: 5575
	public new class Instance : GameStateMachine<MoveToLocationMonitor, MoveToLocationMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600854A RID: 34122 RVA: 0x002EC33D File Offset: 0x002EA53D
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			master.Subscribe(493375141, new Action<object>(this.OnRefreshUserMenu));
		}

		// Token: 0x0600854B RID: 34123 RVA: 0x002EC360 File Offset: 0x002EA560
		private void OnRefreshUserMenu(object data)
		{
			Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_control", UI.USERMENUACTIONS.MOVETOLOCATION.NAME, new System.Action(this.OnClickMoveToLocation), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.MOVETOLOCATION.TOOLTIP, true), 0.2f);
		}

		// Token: 0x0600854C RID: 34124 RVA: 0x002EC3BA File Offset: 0x002EA5BA
		private void OnClickMoveToLocation()
		{
			MoveToLocationTool.Instance.Activate(base.GetComponent<Navigator>());
		}

		// Token: 0x0600854D RID: 34125 RVA: 0x002EC3CC File Offset: 0x002EA5CC
		public void MoveToLocation(int cell)
		{
			this.targetCell = cell;
			base.smi.GoTo(base.smi.sm.satisfied);
			base.smi.GoTo(base.smi.sm.moving);
		}

		// Token: 0x0600854E RID: 34126 RVA: 0x002EC40B File Offset: 0x002EA60B
		public override void StopSM(string reason)
		{
			base.master.Unsubscribe(493375141, new Action<object>(this.OnRefreshUserMenu));
			base.StopSM(reason);
		}

		// Token: 0x040067E3 RID: 26595
		public int targetCell;
	}
}
