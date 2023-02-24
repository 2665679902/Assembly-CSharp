using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000AD RID: 173
public class BuzzStates : GameStateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>
{
	// Token: 0x06000304 RID: 772 RVA: 0x00018034 File Offset: 0x00016234
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.root.Exit("StopNavigator", delegate(BuzzStates.Instance smi)
		{
			smi.GetComponent<Navigator>().Stop(false, true);
		}).ToggleStatusItem(CREATURES.STATUSITEMS.IDLE.NAME, CREATURES.STATUSITEMS.IDLE.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).ToggleTag(GameTags.Idle);
		this.idle.Enter(new StateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.State.Callback(this.PlayIdle)).ToggleScheduleCallback("DoBuzz", (BuzzStates.Instance smi) => (float)UnityEngine.Random.Range(3, 10), delegate(BuzzStates.Instance smi)
		{
			this.numMoves.Set(UnityEngine.Random.Range(4, 6), smi, false);
			smi.GoTo(this.buzz.move);
		});
		this.buzz.ParamTransition<int>(this.numMoves, this.idle, (BuzzStates.Instance smi, int p) => p <= 0);
		this.buzz.move.Enter(new StateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.State.Callback(this.MoveToNewCell)).EventTransition(GameHashes.DestinationReached, this.buzz.pause, null).EventTransition(GameHashes.NavigationFailed, this.buzz.pause, null);
		this.buzz.pause.Enter(delegate(BuzzStates.Instance smi)
		{
			this.numMoves.Set(this.numMoves.Get(smi) - 1, smi, false);
			smi.GoTo(this.buzz.move);
		});
	}

	// Token: 0x06000305 RID: 773 RVA: 0x000181B0 File Offset: 0x000163B0
	public void MoveToNewCell(BuzzStates.Instance smi)
	{
		Navigator component = smi.GetComponent<Navigator>();
		BuzzStates.MoveCellQuery moveCellQuery = new BuzzStates.MoveCellQuery(component.CurrentNavType);
		moveCellQuery.allowLiquid = smi.gameObject.HasTag(GameTags.Amphibious);
		component.RunQuery(moveCellQuery);
		component.GoTo(moveCellQuery.GetResultCell(), null);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x000181FC File Offset: 0x000163FC
	public void PlayIdle(BuzzStates.Instance smi)
	{
		KAnimControllerBase component = smi.GetComponent<KAnimControllerBase>();
		Navigator component2 = smi.GetComponent<Navigator>();
		NavType navType = component2.CurrentNavType;
		if (smi.GetComponent<Facing>().GetFacing())
		{
			navType = NavGrid.MirrorNavType(navType);
		}
		if (smi.def.customIdleAnim != null)
		{
			HashedString invalid = HashedString.Invalid;
			HashedString hashedString = smi.def.customIdleAnim(smi, ref invalid);
			if (hashedString != HashedString.Invalid)
			{
				if (invalid != HashedString.Invalid)
				{
					component.Play(invalid, KAnim.PlayMode.Once, 1f, 0f);
				}
				component.Queue(hashedString, KAnim.PlayMode.Loop, 1f, 0f);
				return;
			}
		}
		HashedString idleAnim = component2.NavGrid.GetIdleAnim(navType);
		component.Play(idleAnim, KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x040001F7 RID: 503
	private StateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.IntParameter numMoves;

	// Token: 0x040001F8 RID: 504
	private BuzzStates.BuzzingStates buzz;

	// Token: 0x040001F9 RID: 505
	public GameStateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.State idle;

	// Token: 0x040001FA RID: 506
	public GameStateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.State move;

	// Token: 0x02000E1D RID: 3613
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040050E4 RID: 20708
		public BuzzStates.Def.IdleAnimCallback customIdleAnim;

		// Token: 0x02001E8B RID: 7819
		// (Invoke) Token: 0x06009BED RID: 39917
		public delegate HashedString IdleAnimCallback(BuzzStates.Instance smi, ref HashedString pre_anim);
	}

	// Token: 0x02000E1E RID: 3614
	public new class Instance : GameStateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.GameInstance
	{
		// Token: 0x06006B91 RID: 27537 RVA: 0x00296A8D File Offset: 0x00294C8D
		public Instance(Chore<BuzzStates.Instance> chore, BuzzStates.Def def)
			: base(chore, def)
		{
		}
	}

	// Token: 0x02000E1F RID: 3615
	public class BuzzingStates : GameStateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.State
	{
		// Token: 0x040050E5 RID: 20709
		public GameStateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.State move;

		// Token: 0x040050E6 RID: 20710
		public GameStateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.State pause;
	}

	// Token: 0x02000E20 RID: 3616
	public class MoveCellQuery : PathFinderQuery
	{
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06006B93 RID: 27539 RVA: 0x00296A9F File Offset: 0x00294C9F
		// (set) Token: 0x06006B94 RID: 27540 RVA: 0x00296AA7 File Offset: 0x00294CA7
		public bool allowLiquid { get; set; }

		// Token: 0x06006B95 RID: 27541 RVA: 0x00296AB0 File Offset: 0x00294CB0
		public MoveCellQuery(NavType navType)
		{
			this.navType = navType;
			this.maxIterations = UnityEngine.Random.Range(5, 25);
		}

		// Token: 0x06006B96 RID: 27542 RVA: 0x00296AD8 File Offset: 0x00294CD8
		public override bool IsMatch(int cell, int parent_cell, int cost)
		{
			if (!Grid.IsValidCell(cell))
			{
				return false;
			}
			bool flag = this.navType != NavType.Swim;
			bool flag2 = this.navType == NavType.Swim || this.allowLiquid;
			bool flag3 = Grid.IsSubstantialLiquid(cell, 0.35f);
			if (flag3 && !flag2)
			{
				return false;
			}
			if (!flag3 && !flag)
			{
				return false;
			}
			this.targetCell = cell;
			int num = this.maxIterations - 1;
			this.maxIterations = num;
			return num <= 0;
		}

		// Token: 0x06006B97 RID: 27543 RVA: 0x00296B49 File Offset: 0x00294D49
		public override int GetResultCell()
		{
			return this.targetCell;
		}

		// Token: 0x040050E7 RID: 20711
		private NavType navType;

		// Token: 0x040050E8 RID: 20712
		private int targetCell = Grid.InvalidCell;

		// Token: 0x040050E9 RID: 20713
		private int maxIterations;
	}
}
