using System;
using STRINGS;

// Token: 0x020000DE RID: 222
public class UpTopPoopStates : GameStateMachine<UpTopPoopStates, UpTopPoopStates.Instance, IStateMachineTarget, UpTopPoopStates.Def>
{
	// Token: 0x060003F5 RID: 1013 RVA: 0x0001E7EC File Offset: 0x0001C9EC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.goingtopoop;
		this.root.Enter("SetTarget", delegate(UpTopPoopStates.Instance smi)
		{
			this.targetCell.Set(smi.GetSMI<GasAndLiquidConsumerMonitor.Instance>().targetCell, smi, false);
		});
		this.goingtopoop.MoveTo((UpTopPoopStates.Instance smi) => smi.GetPoopCell(), this.pooping, this.pooping, false);
		this.pooping.PlayAnim("poop").ToggleStatusItem(CREATURES.STATUSITEMS.EXPELLING_SOLID.NAME, CREATURES.STATUSITEMS.EXPELLING_SOLID.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.PlayAnim("idle_loop", KAnim.PlayMode.Loop).BehaviourComplete(GameTags.Creatures.Poop, false);
	}

	// Token: 0x0400027F RID: 639
	public GameStateMachine<UpTopPoopStates, UpTopPoopStates.Instance, IStateMachineTarget, UpTopPoopStates.Def>.State goingtopoop;

	// Token: 0x04000280 RID: 640
	public GameStateMachine<UpTopPoopStates, UpTopPoopStates.Instance, IStateMachineTarget, UpTopPoopStates.Def>.State pooping;

	// Token: 0x04000281 RID: 641
	public GameStateMachine<UpTopPoopStates, UpTopPoopStates.Instance, IStateMachineTarget, UpTopPoopStates.Def>.State behaviourcomplete;

	// Token: 0x04000282 RID: 642
	public StateMachine<UpTopPoopStates, UpTopPoopStates.Instance, IStateMachineTarget, UpTopPoopStates.Def>.IntParameter targetCell;

	// Token: 0x02000EBB RID: 3771
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000EBC RID: 3772
	public new class Instance : GameStateMachine<UpTopPoopStates, UpTopPoopStates.Instance, IStateMachineTarget, UpTopPoopStates.Def>.GameInstance
	{
		// Token: 0x06006D01 RID: 27905 RVA: 0x00299710 File Offset: 0x00297910
		public Instance(Chore<UpTopPoopStates.Instance> chore, UpTopPoopStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Poop);
		}

		// Token: 0x06006D02 RID: 27906 RVA: 0x00299734 File Offset: 0x00297934
		public int GetPoopCell()
		{
			int num = base.master.gameObject.GetComponent<Navigator>().maxProbingRadius - 1;
			int num2 = Grid.PosToCell(base.gameObject);
			int num3 = Grid.OffsetCell(num2, 0, 1);
			while (num > 0 && Grid.IsValidCell(num3) && !Grid.Solid[num3] && !this.IsClosedDoor(num3))
			{
				num--;
				num2 = num3;
				num3 = Grid.OffsetCell(num2, 0, 1);
			}
			return num2;
		}

		// Token: 0x06006D03 RID: 27907 RVA: 0x002997A4 File Offset: 0x002979A4
		public bool IsClosedDoor(int cellAbove)
		{
			if (Grid.HasDoor[cellAbove])
			{
				Door component = Grid.Objects[cellAbove, 1].GetComponent<Door>();
				return component != null && component.CurrentState != Door.ControlState.Opened;
			}
			return false;
		}
	}
}
