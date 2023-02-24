using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000389 RID: 905
public class MournChore : Chore<MournChore.StatesInstance>
{
	// Token: 0x06001275 RID: 4725 RVA: 0x00062B18 File Offset: 0x00060D18
	private static int GetStandableCell(int cell, Navigator navigator)
	{
		foreach (CellOffset cellOffset in MournChore.ValidStandingOffsets)
		{
			if (Grid.IsCellOffsetValid(cell, cellOffset))
			{
				int num = Grid.OffsetCell(cell, cellOffset);
				if (!Grid.Reserved[num] && navigator.NavGrid.NavTable.IsValid(num, NavType.Floor) && navigator.GetNavigationCost(num) != -1)
				{
					return num;
				}
			}
		}
		return -1;
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x00062B80 File Offset: 0x00060D80
	public MournChore(IStateMachineTarget master)
		: base(Db.Get().ChoreTypes.Mourn, master, master.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.high, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new MournChore.StatesInstance(this);
		base.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		base.AddPrecondition(ChorePreconditions.instance.NoDeadBodies, null);
		base.AddPrecondition(MournChore.HasValidMournLocation, master);
	}

	// Token: 0x06001277 RID: 4727 RVA: 0x00062BF0 File Offset: 0x00060DF0
	public static Grave FindGraveToMournAt()
	{
		Grave grave = null;
		float num = -1f;
		foreach (object obj in Components.Graves)
		{
			Grave grave2 = (Grave)obj;
			if (grave2.burialTime > num)
			{
				num = grave2.burialTime;
				grave = grave2;
			}
		}
		return grave;
	}

	// Token: 0x06001278 RID: 4728 RVA: 0x00062C60 File Offset: 0x00060E60
	public override void Begin(Chore.Precondition.Context context)
	{
		if (context.consumerState.consumer == null)
		{
			global::Debug.LogError("MournChore null context.consumer");
			return;
		}
		if (base.smi == null)
		{
			global::Debug.LogError("MournChore null smi");
			return;
		}
		if (base.smi.sm == null)
		{
			global::Debug.LogError("MournChore null smi.sm");
			return;
		}
		if (MournChore.FindGraveToMournAt() == null)
		{
			global::Debug.LogError("MournChore no grave");
			return;
		}
		base.smi.sm.mourner.Set(context.consumerState.gameObject, base.smi, false);
		base.Begin(context);
	}

	// Token: 0x040009FC RID: 2556
	private static readonly CellOffset[] ValidStandingOffsets = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(-1, 0),
		new CellOffset(1, 0)
	};

	// Token: 0x040009FD RID: 2557
	private static readonly Chore.Precondition HasValidMournLocation = new Chore.Precondition
	{
		id = "HasPlaceToStand",
		description = DUPLICANTS.CHORES.PRECONDITIONS.HAS_PLACE_TO_STAND,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			Navigator component = ((IStateMachineTarget)data).GetComponent<Navigator>();
			bool flag = false;
			Grave grave = MournChore.FindGraveToMournAt();
			if (grave != null && Grid.IsValidCell(MournChore.GetStandableCell(Grid.PosToCell(grave), component)))
			{
				flag = true;
			}
			return flag;
		}
	};

	// Token: 0x02000F75 RID: 3957
	public class StatesInstance : GameStateMachine<MournChore.States, MournChore.StatesInstance, MournChore, object>.GameInstance
	{
		// Token: 0x06006F93 RID: 28563 RVA: 0x002A2CD7 File Offset: 0x002A0ED7
		public StatesInstance(MournChore master)
			: base(master)
		{
		}

		// Token: 0x06006F94 RID: 28564 RVA: 0x002A2CE8 File Offset: 0x002A0EE8
		public void CreateLocator()
		{
			int num = Grid.PosToCell(MournChore.FindGraveToMournAt().transform.GetPosition());
			Navigator component = base.master.GetComponent<Navigator>();
			int standableCell = MournChore.GetStandableCell(num, component);
			if (standableCell < 0)
			{
				base.smi.GoTo(null);
				return;
			}
			Grid.Reserved[standableCell] = true;
			Vector3 vector = Grid.CellToPosCBC(standableCell, Grid.SceneLayer.Move);
			GameObject gameObject = ChoreHelpers.CreateLocator("MournLocator", vector);
			base.smi.sm.locator.Set(gameObject, base.smi, false);
			this.locatorCell = standableCell;
			base.smi.GoTo(base.sm.moveto);
		}

		// Token: 0x06006F95 RID: 28565 RVA: 0x002A2D8C File Offset: 0x002A0F8C
		public void DestroyLocator()
		{
			if (this.locatorCell >= 0)
			{
				Grid.Reserved[this.locatorCell] = false;
				ChoreHelpers.DestroyLocator(base.sm.locator.Get(this));
				base.sm.locator.Set(null, this);
				this.locatorCell = -1;
			}
		}

		// Token: 0x04005495 RID: 21653
		private int locatorCell = -1;
	}

	// Token: 0x02000F76 RID: 3958
	public class States : GameStateMachine<MournChore.States, MournChore.StatesInstance, MournChore>
	{
		// Token: 0x06006F96 RID: 28566 RVA: 0x002A2DE4 File Offset: 0x002A0FE4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.findOffset;
			base.Target(this.mourner);
			this.root.ToggleAnims("anim_react_mourning_kanim", 0f, "").Exit("DestroyLocator", delegate(MournChore.StatesInstance smi)
			{
				smi.DestroyLocator();
			});
			this.findOffset.Enter("CreateLocator", delegate(MournChore.StatesInstance smi)
			{
				smi.CreateLocator();
			});
			this.moveto.InitializeStates(this.mourner, this.locator, this.mourn, null, null, null);
			this.mourn.PlayAnims((MournChore.StatesInstance smi) => MournChore.States.WORK_ANIMS, KAnim.PlayMode.Loop).ScheduleGoTo(10f, this.completed);
			this.completed.PlayAnim("working_pst").OnAnimQueueComplete(null).Exit(delegate(MournChore.StatesInstance smi)
			{
				this.mourner.Get<Effects>(smi).Remove(Db.Get().effects.Get("Mourning"));
			});
		}

		// Token: 0x04005496 RID: 21654
		public StateMachine<MournChore.States, MournChore.StatesInstance, MournChore, object>.TargetParameter mourner;

		// Token: 0x04005497 RID: 21655
		public StateMachine<MournChore.States, MournChore.StatesInstance, MournChore, object>.TargetParameter locator;

		// Token: 0x04005498 RID: 21656
		public GameStateMachine<MournChore.States, MournChore.StatesInstance, MournChore, object>.State findOffset;

		// Token: 0x04005499 RID: 21657
		public GameStateMachine<MournChore.States, MournChore.StatesInstance, MournChore, object>.ApproachSubState<IApproachable> moveto;

		// Token: 0x0400549A RID: 21658
		public GameStateMachine<MournChore.States, MournChore.StatesInstance, MournChore, object>.State mourn;

		// Token: 0x0400549B RID: 21659
		public GameStateMachine<MournChore.States, MournChore.StatesInstance, MournChore, object>.State completed;

		// Token: 0x0400549C RID: 21660
		private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "working_pre", "working_loop" };
	}
}
