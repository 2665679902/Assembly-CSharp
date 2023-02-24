using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000385 RID: 901
public class FleeChore : Chore<FleeChore.StatesInstance>
{
	// Token: 0x06001269 RID: 4713 RVA: 0x000625D8 File Offset: 0x000607D8
	public FleeChore(IStateMachineTarget target, GameObject enemy)
		: base(Db.Get().ChoreTypes.Flee, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new FleeChore.StatesInstance(this);
		base.smi.sm.self.Set(this.gameObject, base.smi, false);
		this.nav = this.gameObject.GetComponent<Navigator>();
		base.smi.sm.fleeFromTarget.Set(enemy, base.smi, false);
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x0006266C File Offset: 0x0006086C
	private bool isInFavoredDirection(int cell, int fleeFromCell)
	{
		bool flag = Grid.CellToPos(fleeFromCell).x < this.gameObject.transform.GetPosition().x;
		bool flag2 = Grid.CellToPos(fleeFromCell).x < Grid.CellToPos(cell).x;
		return flag == flag2;
	}

	// Token: 0x0600126B RID: 4715 RVA: 0x000626C0 File Offset: 0x000608C0
	private bool CanFleeTo(int cell)
	{
		return this.nav.CanReach(cell) || this.nav.CanReach(Grid.OffsetCell(cell, -1, -1)) || this.nav.CanReach(Grid.OffsetCell(cell, 1, -1)) || this.nav.CanReach(Grid.OffsetCell(cell, -1, 1)) || this.nav.CanReach(Grid.OffsetCell(cell, 1, 1));
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x0006272F File Offset: 0x0006092F
	public GameObject CreateLocator(Vector3 pos)
	{
		return ChoreHelpers.CreateLocator("GoToLocator", pos);
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x0006273C File Offset: 0x0006093C
	protected override void OnStateMachineStop(string reason, StateMachine.Status status)
	{
		if (base.smi.sm.fleeToTarget.Get(base.smi) != null)
		{
			ChoreHelpers.DestroyLocator(base.smi.sm.fleeToTarget.Get(base.smi));
		}
		base.OnStateMachineStop(reason, status);
	}

	// Token: 0x040009F8 RID: 2552
	private Navigator nav;

	// Token: 0x02000F6B RID: 3947
	public class StatesInstance : GameStateMachine<FleeChore.States, FleeChore.StatesInstance, FleeChore, object>.GameInstance
	{
		// Token: 0x06006F73 RID: 28531 RVA: 0x002A1E0B File Offset: 0x002A000B
		public StatesInstance(FleeChore master)
			: base(master)
		{
		}
	}

	// Token: 0x02000F6C RID: 3948
	public class States : GameStateMachine<FleeChore.States, FleeChore.StatesInstance, FleeChore>
	{
		// Token: 0x06006F74 RID: 28532 RVA: 0x002A1E14 File Offset: 0x002A0014
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.planFleeRoute;
			this.root.ToggleStatusItem(Db.Get().DuplicantStatusItems.Fleeing, null);
			this.planFleeRoute.Enter(delegate(FleeChore.StatesInstance smi)
			{
				int num = Grid.PosToCell(this.fleeFromTarget.Get(smi));
				HashSet<int> hashSet = GameUtil.FloodCollectCells(Grid.PosToCell(smi.master.gameObject), new Func<int, bool>(smi.master.CanFleeTo), 300, null, true);
				int num2 = -1;
				int num3 = -1;
				foreach (int num4 in hashSet)
				{
					if (smi.master.nav.CanReach(num4))
					{
						int num5 = -1;
						num5 += Grid.GetCellDistance(num4, num);
						if (smi.master.isInFavoredDirection(num4, num))
						{
							num5 += 8;
						}
						if (num5 > num3)
						{
							num3 = num5;
							num2 = num4;
						}
					}
				}
				int num6 = num2;
				if (num6 == -1)
				{
					smi.GoTo(this.cower);
					return;
				}
				smi.sm.fleeToTarget.Set(smi.master.CreateLocator(Grid.CellToPos(num6)), smi, false);
				smi.sm.fleeToTarget.Get(smi).name = "FleeLocator";
				if (num6 == num)
				{
					smi.GoTo(this.cower);
					return;
				}
				smi.GoTo(this.flee);
			});
			this.flee.InitializeStates(this.self, this.fleeToTarget, this.cower, this.cower, null, NavigationTactics.ReduceTravelDistance).ToggleAnims("anim_loco_run_insane_kanim", 2f, "");
			this.cower.ToggleAnims("anim_cringe_kanim", 4f, "").PlayAnim("cringe_pre").QueueAnim("cringe_loop", false, null)
				.QueueAnim("cringe_pst", false, null)
				.OnAnimQueueComplete(this.end);
			this.end.Enter(delegate(FleeChore.StatesInstance smi)
			{
				smi.StopSM("stopped");
			});
		}

		// Token: 0x04005470 RID: 21616
		public StateMachine<FleeChore.States, FleeChore.StatesInstance, FleeChore, object>.TargetParameter fleeFromTarget;

		// Token: 0x04005471 RID: 21617
		public StateMachine<FleeChore.States, FleeChore.StatesInstance, FleeChore, object>.TargetParameter fleeToTarget;

		// Token: 0x04005472 RID: 21618
		public StateMachine<FleeChore.States, FleeChore.StatesInstance, FleeChore, object>.TargetParameter self;

		// Token: 0x04005473 RID: 21619
		public GameStateMachine<FleeChore.States, FleeChore.StatesInstance, FleeChore, object>.State planFleeRoute;

		// Token: 0x04005474 RID: 21620
		public GameStateMachine<FleeChore.States, FleeChore.StatesInstance, FleeChore, object>.ApproachSubState<IApproachable> flee;

		// Token: 0x04005475 RID: 21621
		public GameStateMachine<FleeChore.States, FleeChore.StatesInstance, FleeChore, object>.State cower;

		// Token: 0x04005476 RID: 21622
		public GameStateMachine<FleeChore.States, FleeChore.StatesInstance, FleeChore, object>.State end;
	}
}
