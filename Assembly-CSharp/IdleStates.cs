using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class IdleStates : GameStateMachine<IdleStates, IdleStates.Instance, IStateMachineTarget, IdleStates.Def>
{
	// Token: 0x0600038C RID: 908 RVA: 0x0001B9BC File Offset: 0x00019BBC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.loop;
		this.root.Exit("StopNavigator", delegate(IdleStates.Instance smi)
		{
			smi.GetComponent<Navigator>().Stop(false, true);
		}).ToggleStatusItem(CREATURES.STATUSITEMS.IDLE.NAME, CREATURES.STATUSITEMS.IDLE.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).ToggleTag(GameTags.Idle);
		this.loop.Enter(new StateMachine<IdleStates, IdleStates.Instance, IStateMachineTarget, IdleStates.Def>.State.Callback(this.PlayIdle)).ToggleScheduleCallback("IdleMove", (IdleStates.Instance smi) => (float)UnityEngine.Random.Range(3, 10), delegate(IdleStates.Instance smi)
		{
			smi.GoTo(this.move);
		});
		this.move.Enter(new StateMachine<IdleStates, IdleStates.Instance, IStateMachineTarget, IdleStates.Def>.State.Callback(this.MoveToNewCell)).EventTransition(GameHashes.DestinationReached, this.loop, null).EventTransition(GameHashes.NavigationFailed, this.loop, null);
	}

	// Token: 0x0600038D RID: 909 RVA: 0x0001BAD4 File Offset: 0x00019CD4
	public void MoveToNewCell(IdleStates.Instance smi)
	{
		if (smi.HasTag(GameTags.StationaryIdling))
		{
			smi.GoTo(smi.sm.loop);
			return;
		}
		Navigator component = smi.GetComponent<Navigator>();
		IdleStates.MoveCellQuery moveCellQuery = new IdleStates.MoveCellQuery(component.CurrentNavType);
		moveCellQuery.allowLiquid = smi.gameObject.HasTag(GameTags.Amphibious);
		moveCellQuery.submerged = smi.gameObject.HasTag(GameTags.Creatures.Submerged);
		component.RunQuery(moveCellQuery);
		component.GoTo(moveCellQuery.GetResultCell(), null);
	}

	// Token: 0x0600038E RID: 910 RVA: 0x0001BB54 File Offset: 0x00019D54
	public void PlayIdle(IdleStates.Instance smi)
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

	// Token: 0x0400024A RID: 586
	private GameStateMachine<IdleStates, IdleStates.Instance, IStateMachineTarget, IdleStates.Def>.State loop;

	// Token: 0x0400024B RID: 587
	private GameStateMachine<IdleStates, IdleStates.Instance, IStateMachineTarget, IdleStates.Def>.State move;

	// Token: 0x02000E7E RID: 3710
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005196 RID: 20886
		public IdleStates.Def.IdleAnimCallback customIdleAnim;

		// Token: 0x02001E90 RID: 7824
		// (Invoke) Token: 0x06009BFA RID: 39930
		public delegate HashedString IdleAnimCallback(IdleStates.Instance smi, ref HashedString pre_anim);
	}

	// Token: 0x02000E7F RID: 3711
	public new class Instance : GameStateMachine<IdleStates, IdleStates.Instance, IStateMachineTarget, IdleStates.Def>.GameInstance
	{
		// Token: 0x06006C62 RID: 27746 RVA: 0x00297DE8 File Offset: 0x00295FE8
		public Instance(Chore<IdleStates.Instance> chore, IdleStates.Def def)
			: base(chore, def)
		{
		}
	}

	// Token: 0x02000E80 RID: 3712
	public class MoveCellQuery : PathFinderQuery
	{
		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06006C63 RID: 27747 RVA: 0x00297DF2 File Offset: 0x00295FF2
		// (set) Token: 0x06006C64 RID: 27748 RVA: 0x00297DFA File Offset: 0x00295FFA
		public bool allowLiquid { get; set; }

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06006C65 RID: 27749 RVA: 0x00297E03 File Offset: 0x00296003
		// (set) Token: 0x06006C66 RID: 27750 RVA: 0x00297E0B File Offset: 0x0029600B
		public bool submerged { get; set; }

		// Token: 0x06006C67 RID: 27751 RVA: 0x00297E14 File Offset: 0x00296014
		public MoveCellQuery(NavType navType)
		{
			this.navType = navType;
			this.maxIterations = UnityEngine.Random.Range(5, 25);
		}

		// Token: 0x06006C68 RID: 27752 RVA: 0x00297E3C File Offset: 0x0029603C
		public override bool IsMatch(int cell, int parent_cell, int cost)
		{
			if (!Grid.IsValidCell(cell))
			{
				return false;
			}
			GameObject gameObject;
			Grid.ObjectLayers[1].TryGetValue(cell, out gameObject);
			if (gameObject != null)
			{
				BuildingUnderConstruction component = gameObject.GetComponent<BuildingUnderConstruction>();
				if (component != null && (component.Def.IsFoundation || component.HasTag(GameTags.NoCreatureIdling)))
				{
					return false;
				}
			}
			this.submerged = this.submerged || Grid.IsSubstantialLiquid(cell, 0.35f);
			bool flag = this.navType != NavType.Swim;
			bool flag2 = this.navType == NavType.Swim || this.allowLiquid;
			if (this.submerged && !flag2)
			{
				return false;
			}
			if (!this.submerged && !flag)
			{
				return false;
			}
			this.targetCell = cell;
			int num = this.maxIterations - 1;
			this.maxIterations = num;
			return num <= 0;
		}

		// Token: 0x06006C69 RID: 27753 RVA: 0x00297F0F File Offset: 0x0029610F
		public override int GetResultCell()
		{
			return this.targetCell;
		}

		// Token: 0x04005197 RID: 20887
		private NavType navType;

		// Token: 0x04005198 RID: 20888
		private int targetCell = Grid.InvalidCell;

		// Token: 0x04005199 RID: 20889
		private int maxIterations;
	}
}
