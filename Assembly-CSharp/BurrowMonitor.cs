using System;
using UnityEngine;

// Token: 0x02000461 RID: 1121
public class BurrowMonitor : GameStateMachine<BurrowMonitor, BurrowMonitor.Instance, IStateMachineTarget, BurrowMonitor.Def>
{
	// Token: 0x060018E9 RID: 6377 RVA: 0x00084FBC File Offset: 0x000831BC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.openair;
		this.openair.ToggleBehaviour(GameTags.Creatures.WantsToEnterBurrow, (BurrowMonitor.Instance smi) => smi.ShouldBurrow() && smi.timeinstate > smi.def.minimumAwakeTime, delegate(BurrowMonitor.Instance smi)
		{
			smi.BurrowComplete();
		}).Transition(this.entombed, (BurrowMonitor.Instance smi) => smi.IsEntombed() && !smi.HasTag(GameTags.Creatures.Bagged), UpdateRate.SIM_200ms).Enter("SetCollider", delegate(BurrowMonitor.Instance smi)
		{
			smi.SetCollider(true);
		});
		this.entombed.Enter("SetCollider", delegate(BurrowMonitor.Instance smi)
		{
			smi.SetCollider(false);
		}).Transition(this.openair, (BurrowMonitor.Instance smi) => !smi.IsEntombed(), UpdateRate.SIM_200ms).TagTransition(GameTags.Creatures.Bagged, this.openair, false)
			.ToggleBehaviour(GameTags.Creatures.Burrowed, (BurrowMonitor.Instance smi) => smi.IsEntombed(), delegate(BurrowMonitor.Instance smi)
			{
				smi.GoTo(this.openair);
			})
			.ToggleBehaviour(GameTags.Creatures.WantsToExitBurrow, (BurrowMonitor.Instance smi) => smi.EmergeIsClear() && GameClock.Instance.IsNighttime(), delegate(BurrowMonitor.Instance smi)
			{
				smi.ExitBurrowComplete();
			});
	}

	// Token: 0x04000DED RID: 3565
	public GameStateMachine<BurrowMonitor, BurrowMonitor.Instance, IStateMachineTarget, BurrowMonitor.Def>.State openair;

	// Token: 0x04000DEE RID: 3566
	public GameStateMachine<BurrowMonitor, BurrowMonitor.Instance, IStateMachineTarget, BurrowMonitor.Def>.State entombed;

	// Token: 0x02001086 RID: 4230
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040057E1 RID: 22497
		public float burrowHardnessLimit = 20f;

		// Token: 0x040057E2 RID: 22498
		public float minimumAwakeTime = 24f;

		// Token: 0x040057E3 RID: 22499
		public Vector2 moundColliderSize = new Vector2f(1f, 1.5f);

		// Token: 0x040057E4 RID: 22500
		public Vector2 moundColliderOffset = new Vector2(0f, -0.25f);
	}

	// Token: 0x02001087 RID: 4231
	public new class Instance : GameStateMachine<BurrowMonitor, BurrowMonitor.Instance, IStateMachineTarget, BurrowMonitor.Def>.GameInstance
	{
		// Token: 0x06007354 RID: 29524 RVA: 0x002B002C File Offset: 0x002AE22C
		public Instance(IStateMachineTarget master, BurrowMonitor.Def def)
			: base(master, def)
		{
			KBoxCollider2D component = master.GetComponent<KBoxCollider2D>();
			this.originalColliderSize = component.size;
			this.originalColliderOffset = component.offset;
		}

		// Token: 0x06007355 RID: 29525 RVA: 0x002B0060 File Offset: 0x002AE260
		public bool EmergeIsClear()
		{
			int num = Grid.PosToCell(base.gameObject);
			if (!Grid.IsValidCell(num) || !Grid.IsValidCell(Grid.CellAbove(num)))
			{
				return false;
			}
			int num2 = Grid.CellAbove(num);
			return !Grid.Solid[num2] && !Grid.IsSubstantialLiquid(Grid.CellAbove(num), 0.9f);
		}

		// Token: 0x06007356 RID: 29526 RVA: 0x002B00BB File Offset: 0x002AE2BB
		public bool ShouldBurrow()
		{
			return !GameClock.Instance.IsNighttime() && this.CanBurrowInto(Grid.CellBelow(Grid.PosToCell(base.gameObject))) && !base.HasTag(GameTags.Creatures.Bagged);
		}

		// Token: 0x06007357 RID: 29527 RVA: 0x002B00F8 File Offset: 0x002AE2F8
		public bool CanBurrowInto(int cell)
		{
			return Grid.IsValidCell(cell) && Grid.Solid[cell] && !Grid.IsSubstantialLiquid(Grid.CellAbove(cell), 0.35f) && !(Grid.Objects[cell, 1] != null) && (float)Grid.Element[cell].hardness <= base.def.burrowHardnessLimit && !Grid.Foundation[cell];
		}

		// Token: 0x06007358 RID: 29528 RVA: 0x002B0174 File Offset: 0x002AE374
		public bool IsEntombed()
		{
			int num = Grid.PosToCell(base.smi);
			return Grid.IsValidCell(num) && Grid.Solid[num];
		}

		// Token: 0x06007359 RID: 29529 RVA: 0x002B01A2 File Offset: 0x002AE3A2
		public void ExitBurrowComplete()
		{
			base.smi.GetComponent<KBatchedAnimController>().Play("idle_loop", KAnim.PlayMode.Once, 1f, 0f);
			this.GoTo(base.sm.openair);
		}

		// Token: 0x0600735A RID: 29530 RVA: 0x002B01DC File Offset: 0x002AE3DC
		public void BurrowComplete()
		{
			base.smi.transform.SetPosition(Grid.CellToPosCBC(Grid.CellBelow(Grid.PosToCell(base.transform.GetPosition())), Grid.SceneLayer.Creatures));
			base.smi.GetComponent<KBatchedAnimController>().Play("idle_mound", KAnim.PlayMode.Once, 1f, 0f);
			this.GoTo(base.sm.entombed);
		}

		// Token: 0x0600735B RID: 29531 RVA: 0x002B024C File Offset: 0x002AE44C
		public void SetCollider(bool original_size)
		{
			KBoxCollider2D component = base.master.GetComponent<KBoxCollider2D>();
			AnimEventHandler component2 = base.master.GetComponent<AnimEventHandler>();
			if (original_size)
			{
				component.size = this.originalColliderSize;
				component.offset = this.originalColliderOffset;
				component2.baseOffset = this.originalColliderOffset;
				return;
			}
			component.size = base.def.moundColliderSize;
			component.offset = base.def.moundColliderOffset;
			component2.baseOffset = base.def.moundColliderOffset;
		}

		// Token: 0x040057E5 RID: 22501
		private Vector2 originalColliderSize;

		// Token: 0x040057E6 RID: 22502
		private Vector2 originalColliderOffset;
	}
}
