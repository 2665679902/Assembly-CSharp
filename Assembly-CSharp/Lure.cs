using System;

// Token: 0x02000AEE RID: 2798
public class Lure : GameStateMachine<Lure, Lure.Instance, IStateMachineTarget, Lure.Def>
{
	// Token: 0x060055C0 RID: 21952 RVA: 0x001EFFB9 File Offset: 0x001EE1B9
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.off.DoNothing();
		this.on.Enter(new StateMachine<Lure, Lure.Instance, IStateMachineTarget, Lure.Def>.State.Callback(this.AddToScenePartitioner)).Exit(new StateMachine<Lure, Lure.Instance, IStateMachineTarget, Lure.Def>.State.Callback(this.RemoveFromScenePartitioner));
	}

	// Token: 0x060055C1 RID: 21953 RVA: 0x001EFFF8 File Offset: 0x001EE1F8
	private void AddToScenePartitioner(Lure.Instance smi)
	{
		Extents extents = new Extents(Grid.PosToCell(smi.transform.GetPosition()), smi.def.radius);
		smi.partitionerEntry = GameScenePartitioner.Instance.Add(this.name, smi, extents, GameScenePartitioner.Instance.lure, null);
	}

	// Token: 0x060055C2 RID: 21954 RVA: 0x001F004A File Offset: 0x001EE24A
	private void RemoveFromScenePartitioner(Lure.Instance smi)
	{
		GameScenePartitioner.Instance.Free(ref smi.partitionerEntry);
	}

	// Token: 0x04003A4F RID: 14927
	public GameStateMachine<Lure, Lure.Instance, IStateMachineTarget, Lure.Def>.State off;

	// Token: 0x04003A50 RID: 14928
	public GameStateMachine<Lure, Lure.Instance, IStateMachineTarget, Lure.Def>.State on;

	// Token: 0x0200196B RID: 6507
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04007449 RID: 29769
		public CellOffset[] lurePoints = new CellOffset[1];

		// Token: 0x0400744A RID: 29770
		public int radius = 50;

		// Token: 0x0400744B RID: 29771
		public Tag[] initialLures;
	}

	// Token: 0x0200196C RID: 6508
	public new class Instance : GameStateMachine<Lure, Lure.Instance, IStateMachineTarget, Lure.Def>.GameInstance
	{
		// Token: 0x06009035 RID: 36917 RVA: 0x003121FC File Offset: 0x003103FC
		public Instance(IStateMachineTarget master, Lure.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06009036 RID: 36918 RVA: 0x00312206 File Offset: 0x00310406
		public override void StartSM()
		{
			base.StartSM();
			if (base.def.initialLures != null)
			{
				this.SetActiveLures(base.def.initialLures);
			}
		}

		// Token: 0x06009037 RID: 36919 RVA: 0x0031222C File Offset: 0x0031042C
		public void SetActiveLures(Tag[] lures)
		{
			this.lures = lures;
			if (lures == null || lures.Length == 0)
			{
				this.GoTo(base.sm.off);
				return;
			}
			this.GoTo(base.sm.on);
		}

		// Token: 0x06009038 RID: 36920 RVA: 0x0031225F File Offset: 0x0031045F
		public bool IsActive()
		{
			return this.GetCurrentState() == base.sm.on;
		}

		// Token: 0x06009039 RID: 36921 RVA: 0x00312274 File Offset: 0x00310474
		public bool HasAnyLure(Tag[] creature_lures)
		{
			if (this.lures == null || creature_lures == null)
			{
				return false;
			}
			foreach (Tag tag in creature_lures)
			{
				foreach (Tag tag2 in this.lures)
				{
					if (tag == tag2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400744C RID: 29772
		private Tag[] lures;

		// Token: 0x0400744D RID: 29773
		public HandleVector<int>.Handle partitionerEntry;
	}
}
