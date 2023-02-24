using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020000D4 RID: 212
internal class NestingPoopState : GameStateMachine<NestingPoopState, NestingPoopState.Instance, IStateMachineTarget, NestingPoopState.Def>
{
	// Token: 0x060003AE RID: 942 RVA: 0x0001C680 File Offset: 0x0001A880
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.goingtopoop;
		this.goingtopoop.MoveTo((NestingPoopState.Instance smi) => smi.GetPoopPosition(), this.pooping, this.failedtonest, false);
		this.failedtonest.Enter(delegate(NestingPoopState.Instance smi)
		{
			smi.SetLastPoopCell();
		}).GoTo(this.pooping);
		this.pooping.Enter(delegate(NestingPoopState.Instance smi)
		{
			smi.master.GetComponent<Facing>().SetFacing(Grid.PosToCell(smi.master.gameObject) > smi.targetPoopCell);
		}).ToggleStatusItem(CREATURES.STATUSITEMS.EXPELLING_SOLID.NAME, CREATURES.STATUSITEMS.EXPELLING_SOLID.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).PlayAnim("poop")
			.OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.Enter(delegate(NestingPoopState.Instance smi)
		{
			smi.SetLastPoopCell();
		}).PlayAnim("idle_loop", KAnim.PlayMode.Loop).BehaviourComplete(GameTags.Creatures.Poop, false);
	}

	// Token: 0x04000260 RID: 608
	public GameStateMachine<NestingPoopState, NestingPoopState.Instance, IStateMachineTarget, NestingPoopState.Def>.State goingtopoop;

	// Token: 0x04000261 RID: 609
	public GameStateMachine<NestingPoopState, NestingPoopState.Instance, IStateMachineTarget, NestingPoopState.Def>.State pooping;

	// Token: 0x04000262 RID: 610
	public GameStateMachine<NestingPoopState, NestingPoopState.Instance, IStateMachineTarget, NestingPoopState.Def>.State behaviourcomplete;

	// Token: 0x04000263 RID: 611
	public GameStateMachine<NestingPoopState, NestingPoopState.Instance, IStateMachineTarget, NestingPoopState.Def>.State failedtonest;

	// Token: 0x02000E95 RID: 3733
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06006C92 RID: 27794 RVA: 0x0029825D File Offset: 0x0029645D
		public Def(Tag tag)
		{
			this.nestingPoopElement = tag;
		}

		// Token: 0x040051C3 RID: 20931
		public Tag nestingPoopElement = Tag.Invalid;
	}

	// Token: 0x02000E96 RID: 3734
	public new class Instance : GameStateMachine<NestingPoopState, NestingPoopState.Instance, IStateMachineTarget, NestingPoopState.Def>.GameInstance
	{
		// Token: 0x06006C93 RID: 27795 RVA: 0x00298277 File Offset: 0x00296477
		public Instance(Chore<NestingPoopState.Instance> chore, NestingPoopState.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Poop);
		}

		// Token: 0x06006C94 RID: 27796 RVA: 0x002982B4 File Offset: 0x002964B4
		private static bool IsValidNestingCell(int cell, object arg)
		{
			return Grid.IsValidCell(cell) && !Grid.Solid[cell] && Grid.Solid[Grid.CellBelow(cell)] && (NestingPoopState.Instance.IsValidPoopFromCell(cell, true) || NestingPoopState.Instance.IsValidPoopFromCell(cell, false));
		}

		// Token: 0x06006C95 RID: 27797 RVA: 0x002982F4 File Offset: 0x002964F4
		private static bool IsValidPoopFromCell(int cell, bool look_left)
		{
			if (look_left)
			{
				int num = Grid.CellDownLeft(cell);
				int num2 = Grid.CellLeft(cell);
				return Grid.IsValidCell(num) && Grid.Solid[num] && Grid.IsValidCell(num2) && !Grid.Solid[num2];
			}
			int num3 = Grid.CellDownRight(cell);
			int num4 = Grid.CellRight(cell);
			return Grid.IsValidCell(num3) && Grid.Solid[num3] && Grid.IsValidCell(num4) && !Grid.Solid[num4];
		}

		// Token: 0x06006C96 RID: 27798 RVA: 0x0029837C File Offset: 0x0029657C
		public int GetPoopPosition()
		{
			this.targetPoopCell = this.GetTargetPoopCell();
			List<Direction> list = new List<Direction>();
			if (NestingPoopState.Instance.IsValidPoopFromCell(this.targetPoopCell, true))
			{
				list.Add(Direction.Left);
			}
			if (NestingPoopState.Instance.IsValidPoopFromCell(this.targetPoopCell, false))
			{
				list.Add(Direction.Right);
			}
			if (list.Count > 0)
			{
				Direction direction = list[UnityEngine.Random.Range(0, list.Count)];
				int cellInDirection = Grid.GetCellInDirection(this.targetPoopCell, direction);
				if (Grid.IsValidCell(cellInDirection))
				{
					return cellInDirection;
				}
			}
			if (Grid.IsValidCell(this.targetPoopCell))
			{
				return this.targetPoopCell;
			}
			if (!Grid.IsValidCell(Grid.PosToCell(this)))
			{
				global::Debug.LogWarning("This is bad, how is Mole occupying an invalid cell?");
			}
			return Grid.PosToCell(this);
		}

		// Token: 0x06006C97 RID: 27799 RVA: 0x0029842C File Offset: 0x0029662C
		private int GetTargetPoopCell()
		{
			CreatureCalorieMonitor.Instance smi = base.smi.GetSMI<CreatureCalorieMonitor.Instance>();
			this.currentlyPoopingElement = smi.stomach.GetNextPoopEntry();
			int num;
			if (this.currentlyPoopingElement == base.smi.def.nestingPoopElement && base.smi.def.nestingPoopElement != Tag.Invalid && this.lastPoopCell != -1)
			{
				num = this.lastPoopCell;
			}
			else
			{
				num = Grid.PosToCell(this);
			}
			int num2 = GameUtil.FloodFillFind<object>(new Func<int, object, bool>(NestingPoopState.Instance.IsValidNestingCell), null, num, 8, false, true);
			if (num2 == -1)
			{
				CellOffset[] array = new CellOffset[]
				{
					new CellOffset(0, 0),
					new CellOffset(-1, 0),
					new CellOffset(1, 0),
					new CellOffset(-1, -1),
					new CellOffset(1, -1)
				};
				num2 = Grid.OffsetCell(this.lastPoopCell, array[UnityEngine.Random.Range(0, array.Length)]);
				int num3 = Grid.CellAbove(num2);
				while (Grid.IsValidCell(num3) && Grid.Solid[num3])
				{
					num2 = num3;
					num3 = Grid.CellAbove(num2);
				}
			}
			return num2;
		}

		// Token: 0x06006C98 RID: 27800 RVA: 0x0029855B File Offset: 0x0029675B
		public void SetLastPoopCell()
		{
			if (this.currentlyPoopingElement == base.smi.def.nestingPoopElement)
			{
				this.lastPoopCell = Grid.PosToCell(this);
			}
		}

		// Token: 0x040051C4 RID: 20932
		[Serialize]
		private int lastPoopCell = -1;

		// Token: 0x040051C5 RID: 20933
		public int targetPoopCell = -1;

		// Token: 0x040051C6 RID: 20934
		private Tag currentlyPoopingElement = Tag.Invalid;
	}
}
