using System;
using Klei;
using STRINGS;
using UnityEngine;

// Token: 0x020000D0 RID: 208
public class LayEggStates : GameStateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>
{
	// Token: 0x0600039D RID: 925 RVA: 0x0001C1B0 File Offset: 0x0001A3B0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.layeggpre;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.LAYINGANEGG.NAME, CREATURES.STATUSITEMS.LAYINGANEGG.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.layeggpre.Enter(new StateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State.Callback(LayEggStates.LayEgg)).Exit(new StateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State.Callback(LayEggStates.ShowEgg)).PlayAnim("lay_egg_pre")
			.OnAnimQueueComplete(this.layeggpst);
		this.layeggpst.PlayAnim("lay_egg_pst").OnAnimQueueComplete(this.moveaside);
		this.moveaside.MoveTo(new Func<LayEggStates.Instance, int>(LayEggStates.GetMoveAsideCell), this.lookategg, this.behaviourcomplete, false);
		this.lookategg.Enter(new StateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State.Callback(LayEggStates.FaceEgg)).GoTo(this.behaviourcomplete);
		this.behaviourcomplete.QueueAnim("idle_loop", true, null).BehaviourComplete(GameTags.Creatures.Fertile, false);
	}

	// Token: 0x0600039E RID: 926 RVA: 0x0001C2CC File Offset: 0x0001A4CC
	private static void LayEgg(LayEggStates.Instance smi)
	{
		smi.eggPos = smi.transform.GetPosition();
		smi.GetSMI<FertilityMonitor.Instance>().LayEgg();
	}

	// Token: 0x0600039F RID: 927 RVA: 0x0001C2EC File Offset: 0x0001A4EC
	private static void ShowEgg(LayEggStates.Instance smi)
	{
		FertilityMonitor.Instance smi2 = smi.GetSMI<FertilityMonitor.Instance>();
		if (smi2 != null)
		{
			smi2.ShowEgg();
		}
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x0001C309 File Offset: 0x0001A509
	private static void FaceEgg(LayEggStates.Instance smi)
	{
		smi.Get<Facing>().Face(smi.eggPos);
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x0001C31C File Offset: 0x0001A51C
	private static int GetMoveAsideCell(LayEggStates.Instance smi)
	{
		int num = 1;
		if (GenericGameSettings.instance.acceleratedLifecycle)
		{
			num = 8;
		}
		int num2 = Grid.PosToCell(smi);
		if (Grid.IsValidCell(num2))
		{
			int num3 = Grid.OffsetCell(num2, num, 0);
			if (Grid.IsValidCell(num3) && !Grid.Solid[num3])
			{
				return num3;
			}
			int num4 = Grid.OffsetCell(num2, -num, 0);
			if (Grid.IsValidCell(num4))
			{
				return num4;
			}
		}
		return Grid.InvalidCell;
	}

	// Token: 0x04000254 RID: 596
	public GameStateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State layeggpre;

	// Token: 0x04000255 RID: 597
	public GameStateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State layeggpst;

	// Token: 0x04000256 RID: 598
	public GameStateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State moveaside;

	// Token: 0x04000257 RID: 599
	public GameStateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State lookategg;

	// Token: 0x04000258 RID: 600
	public GameStateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State behaviourcomplete;

	// Token: 0x02000E8B RID: 3723
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E8C RID: 3724
	public new class Instance : GameStateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.GameInstance
	{
		// Token: 0x06006C85 RID: 27781 RVA: 0x00298170 File Offset: 0x00296370
		public Instance(Chore<LayEggStates.Instance> chore, LayEggStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Fertile);
		}

		// Token: 0x040051BD RID: 20925
		public Vector3 eggPos;
	}
}
