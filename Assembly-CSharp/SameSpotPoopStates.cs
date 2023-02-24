using System;
using KSerialization;
using STRINGS;

// Token: 0x020000D7 RID: 215
public class SameSpotPoopStates : GameStateMachine<SameSpotPoopStates, SameSpotPoopStates.Instance, IStateMachineTarget, SameSpotPoopStates.Def>
{
	// Token: 0x060003C0 RID: 960 RVA: 0x0001CECC File Offset: 0x0001B0CC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.goingtopoop;
		this.root.Enter("SetTarget", delegate(SameSpotPoopStates.Instance smi)
		{
			this.targetCell.Set(smi.GetSMI<GasAndLiquidConsumerMonitor.Instance>().targetCell, smi, false);
		});
		this.goingtopoop.MoveTo((SameSpotPoopStates.Instance smi) => smi.GetLastPoopCell(), this.pooping, this.updatepoopcell, false);
		this.pooping.PlayAnim("poop").ToggleStatusItem(CREATURES.STATUSITEMS.EXPELLING_SOLID.NAME, CREATURES.STATUSITEMS.EXPELLING_SOLID.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).OnAnimQueueComplete(this.behaviourcomplete);
		this.updatepoopcell.Enter(delegate(SameSpotPoopStates.Instance smi)
		{
			smi.SetLastPoopCell();
		}).GoTo(this.pooping);
		this.behaviourcomplete.PlayAnim("idle_loop", KAnim.PlayMode.Loop).BehaviourComplete(GameTags.Creatures.Poop, false);
	}

	// Token: 0x04000267 RID: 615
	public GameStateMachine<SameSpotPoopStates, SameSpotPoopStates.Instance, IStateMachineTarget, SameSpotPoopStates.Def>.State goingtopoop;

	// Token: 0x04000268 RID: 616
	public GameStateMachine<SameSpotPoopStates, SameSpotPoopStates.Instance, IStateMachineTarget, SameSpotPoopStates.Def>.State pooping;

	// Token: 0x04000269 RID: 617
	public GameStateMachine<SameSpotPoopStates, SameSpotPoopStates.Instance, IStateMachineTarget, SameSpotPoopStates.Def>.State behaviourcomplete;

	// Token: 0x0400026A RID: 618
	public GameStateMachine<SameSpotPoopStates, SameSpotPoopStates.Instance, IStateMachineTarget, SameSpotPoopStates.Def>.State updatepoopcell;

	// Token: 0x0400026B RID: 619
	public StateMachine<SameSpotPoopStates, SameSpotPoopStates.Instance, IStateMachineTarget, SameSpotPoopStates.Def>.IntParameter targetCell;

	// Token: 0x02000EA3 RID: 3747
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000EA4 RID: 3748
	public new class Instance : GameStateMachine<SameSpotPoopStates, SameSpotPoopStates.Instance, IStateMachineTarget, SameSpotPoopStates.Def>.GameInstance
	{
		// Token: 0x06006CC5 RID: 27845 RVA: 0x00298AF6 File Offset: 0x00296CF6
		public Instance(Chore<SameSpotPoopStates.Instance> chore, SameSpotPoopStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Poop);
		}

		// Token: 0x06006CC6 RID: 27846 RVA: 0x00298B21 File Offset: 0x00296D21
		public int GetLastPoopCell()
		{
			if (this.lastPoopCell == -1)
			{
				this.SetLastPoopCell();
			}
			return this.lastPoopCell;
		}

		// Token: 0x06006CC7 RID: 27847 RVA: 0x00298B38 File Offset: 0x00296D38
		public void SetLastPoopCell()
		{
			this.lastPoopCell = Grid.PosToCell(this);
		}

		// Token: 0x040051E3 RID: 20963
		[Serialize]
		private int lastPoopCell = -1;
	}
}
