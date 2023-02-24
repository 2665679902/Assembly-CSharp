using System;
using STRINGS;

// Token: 0x020000D5 RID: 213
public class RanchedStates : GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>
{
	// Token: 0x060003B0 RID: 944 RVA: 0x0001C7CC File Offset: 0x0001A9CC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.ranch;
		this.root.Exit("AbandonedRanchStation", delegate(RanchedStates.Instance smi)
		{
			RanchStation.Instance ranchStation = smi.GetRanchStation();
			if (ranchStation == null)
			{
				return;
			}
			ranchStation.Abandon(smi.Monitor);
		});
		this.ranch.Enter(new StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State.Callback(RanchedStates.SubscribeToRancherStateChanges)).EventHandler(GameHashes.RanchStationNoLongerAvailable, new StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State.Callback(RanchedStates.OnRanchStationNotAvailable)).BehaviourComplete(GameTags.Creatures.WantsToGetRanched, true)
			.Exit(new StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State.Callback(RanchedStates.UnsubscribeFromRancherStateChanges))
			.Exit(new StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State.Callback(RanchedStates.ClearLayerOverride));
		this.ranch.Cheer.ToggleStatusItem(CREATURES.STATUSITEMS.EXCITED_TO_GET_RANCHED.NAME, CREATURES.STATUSITEMS.EXCITED_TO_GET_RANCHED.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).Enter("FaceRancher", delegate(RanchedStates.Instance smi)
		{
			smi.GetComponent<Facing>().Face(smi.GetRanchStation().transform.GetPosition());
		}).PlayAnim("excited_loop")
			.OnAnimQueueComplete(this.ranch.Cheer.Pst);
		this.ranch.Cheer.Pst.ScheduleGoTo(0.2f, this.ranch.Move);
		this.ranch.Move.DefaultState(this.ranch.Move.MoveToRanch).Enter("Speedup", delegate(RanchedStates.Instance smi)
		{
			smi.GetComponent<Navigator>().defaultSpeed = smi.OriginalSpeed * 1.25f;
		}).ToggleStatusItem(CREATURES.STATUSITEMS.EXCITED_TO_GET_RANCHED.NAME, CREATURES.STATUSITEMS.EXCITED_TO_GET_RANCHED.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main)
			.Exit("RestoreSpeed", delegate(RanchedStates.Instance smi)
			{
				smi.GetComponent<Navigator>().defaultSpeed = smi.OriginalSpeed;
			});
		this.ranch.Move.MoveToRanch.EnterTransition(this.ranch.Move.WaitInLine, GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.Not(new StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.Transition.ConditionCallback(RanchedStates.IsCrittersTurn))).MoveTo(new Func<RanchedStates.Instance, int>(RanchedStates.GetRanchNavTarget), this.ranch.Move.WaitInLine, null, false);
		this.ranch.Move.WaitInLine.EnterTransition(this.ranch.Ranching, new StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.Transition.ConditionCallback(RanchedStates.IsCrittersTurn)).Enter(new StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State.Callback(RanchedStates.MoveToWaitPosition)).EventHandler(GameHashes.DestinationReached, new StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State.Callback(RanchedStates.Wait));
		this.ranch.Ranching.Enter(new StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State.Callback(RanchedStates.GetOnTable)).Enter("SetCreatureAtRanchingStation", delegate(RanchedStates.Instance smi)
		{
			smi.GetRanchStation().MessageCreatureArrived(smi);
			smi.AnimController.SetSceneLayer(Grid.SceneLayer.BuildingUse);
		}).EventTransition(GameHashes.RanchingComplete, this.ranch.Wavegoodbye, null)
			.ToggleStatusItem(CREATURES.STATUSITEMS.GETTING_RANCHED.NAME, CREATURES.STATUSITEMS.GETTING_RANCHED.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.ranch.Wavegoodbye.Enter(new StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State.Callback(RanchedStates.ClearLayerOverride)).OnAnimQueueComplete(this.ranch.Runaway).ToggleStatusItem(CREATURES.STATUSITEMS.EXCITED_TO_BE_RANCHED.NAME, CREATURES.STATUSITEMS.EXCITED_TO_BE_RANCHED.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.ranch.Runaway.MoveTo(new Func<RanchedStates.Instance, int>(RanchedStates.GetRunawayCell), null, null, false).ToggleStatusItem(CREATURES.STATUSITEMS.IDLE.NAME, CREATURES.STATUSITEMS.IDLE.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x0001CBF0 File Offset: 0x0001ADF0
	private static void ClearLayerOverride(RanchedStates.Instance smi)
	{
		smi.AnimController.SetSceneLayer(Grid.SceneLayer.Creatures);
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x0001CBFF File Offset: 0x0001ADFF
	private static RanchStation.Instance GetRanchStation(RanchedStates.Instance smi)
	{
		return smi.GetRanchStation();
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x0001CC07 File Offset: 0x0001AE07
	private static GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State GetInitialRanchState(RanchedStates.Instance smi)
	{
		if (!RanchedStates.IsCrittersTurn(smi))
		{
			return smi.sm.ranch.Move.WaitInLine;
		}
		return smi.sm.ranch.Cheer;
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x0001CC37 File Offset: 0x0001AE37
	private static void OnRanchStationNotAvailable(RanchedStates.Instance smi)
	{
		smi.GoTo(null);
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x0001CC40 File Offset: 0x0001AE40
	private static void GetOnTable(RanchedStates.Instance smi)
	{
		Navigator navigator = smi.Get<Navigator>();
		if (navigator.IsValidNavType(NavType.Floor))
		{
			navigator.SetCurrentNavType(NavType.Floor);
		}
		smi.Get<Facing>().SetFacing(false);
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x0001CC70 File Offset: 0x0001AE70
	private static bool IsCrittersTurn(RanchedStates.Instance smi)
	{
		RanchStation.Instance ranchStation = RanchedStates.GetRanchStation(smi);
		return ranchStation != null && ranchStation.IsRancherReady && ranchStation.TryGetRanched(smi);
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x0001CC9C File Offset: 0x0001AE9C
	private static int GetRanchNavTarget(RanchedStates.Instance smi)
	{
		RanchStation.Instance ranchStation = RanchedStates.GetRanchStation(smi);
		return smi.ModifyNavTargetForCritter(ranchStation.GetRanchNavTarget());
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x0001CCBC File Offset: 0x0001AEBC
	private static void MoveToWaitPosition(RanchedStates.Instance smi)
	{
		smi.EnterQueue();
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x0001CCC4 File Offset: 0x0001AEC4
	private static void Wait(RanchedStates.Instance smi)
	{
		RanchStation.Instance targetRanchStation = smi.Monitor.TargetRanchStation;
		smi.Monitor.NavComponent.IsFacingLeft = targetRanchStation.transform.position.x - smi.transform.position.x < 0f;
		smi.AnimController.Queue(smi.def.StartWaitingAnim, KAnim.PlayMode.Once, 1f, 0f);
		smi.AnimController.Play(smi.def.WaitingAnim, KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x060003BA RID: 954 RVA: 0x0001CD58 File Offset: 0x0001AF58
	private static void SubscribeToRancherStateChanges(RanchedStates.Instance smi)
	{
		RanchStation.Instance ranchStation = smi.GetRanchStation();
		if (ranchStation == null)
		{
			return;
		}
		RanchStation.Instance instance = ranchStation;
		instance.RancherStateChanged = (Action<RanchStation.Instance>)Delegate.Combine(instance.RancherStateChanged, new Action<RanchStation.Instance>(smi.OnRancherStateChanged));
		if (ranchStation.IsRancherReady)
		{
			smi.OnRancherStateChanged(ranchStation);
		}
	}

	// Token: 0x060003BB RID: 955 RVA: 0x0001CDA4 File Offset: 0x0001AFA4
	private static void UnsubscribeFromRancherStateChanges(RanchedStates.Instance smi)
	{
		RanchStation.Instance ranchStation = smi.GetRanchStation();
		if (ranchStation == null)
		{
			return;
		}
		RanchStation.Instance instance = ranchStation;
		instance.RancherStateChanged = (Action<RanchStation.Instance>)Delegate.Remove(instance.RancherStateChanged, new Action<RanchStation.Instance>(smi.OnRancherStateChanged));
	}

	// Token: 0x060003BC RID: 956 RVA: 0x0001CDE0 File Offset: 0x0001AFE0
	private static int GetRunawayCell(RanchedStates.Instance smi)
	{
		int num = Grid.PosToCell(smi.transform.GetPosition());
		int num2 = Grid.OffsetCell(num, 2, 0);
		if (Grid.Solid[num2])
		{
			num2 = Grid.OffsetCell(num, -2, 0);
		}
		return num2;
	}

	// Token: 0x04000264 RID: 612
	private RanchedStates.RanchStates ranch;

	// Token: 0x02000E98 RID: 3736
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06006C9F RID: 27807 RVA: 0x002985DC File Offset: 0x002967DC
		public bool IsQueueAnim(HashedString anim)
		{
			return (this.StartWaitingAnim == anim) | (this.WaitingAnim == anim) | (this.EndWaitingAnim == anim);
		}

		// Token: 0x040051CC RID: 20940
		public HashedString StartWaitingAnim = "queue_pre";

		// Token: 0x040051CD RID: 20941
		public HashedString WaitingAnim = "queue_loop";

		// Token: 0x040051CE RID: 20942
		public HashedString EndWaitingAnim = "queue_pst";

		// Token: 0x040051CF RID: 20943
		public int WaitCellOffset = 1;
	}

	// Token: 0x02000E99 RID: 3737
	public new class Instance : GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.GameInstance
	{
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06006CA1 RID: 27809 RVA: 0x00298643 File Offset: 0x00296843
		public RanchableMonitor.Instance Monitor
		{
			get
			{
				if (this.ranchMonitor == null)
				{
					this.ranchMonitor = this.GetSMI<RanchableMonitor.Instance>();
				}
				return this.ranchMonitor;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06006CA2 RID: 27810 RVA: 0x0029865F File Offset: 0x0029685F
		public KBatchedAnimController AnimController
		{
			get
			{
				return this.animController;
			}
		}

		// Token: 0x06006CA3 RID: 27811 RVA: 0x00298668 File Offset: 0x00296868
		public Instance(Chore<RanchedStates.Instance> chore, RanchedStates.Def def)
			: base(chore, def)
		{
			this.animController = base.GetComponent<KBatchedAnimController>();
			this.OriginalSpeed = this.Monitor.NavComponent.defaultSpeed;
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToGetRanched);
		}

		// Token: 0x06006CA4 RID: 27812 RVA: 0x002986B9 File Offset: 0x002968B9
		public override void StartSM()
		{
			base.StartSM();
			this.animController.onAnimComplete += this.OnAnimComplete;
		}

		// Token: 0x06006CA5 RID: 27813 RVA: 0x002986D8 File Offset: 0x002968D8
		public override void StopSM(string reason)
		{
			base.StopSM(reason);
			this.animController.onAnimComplete -= this.OnAnimComplete;
		}

		// Token: 0x06006CA6 RID: 27814 RVA: 0x002986F8 File Offset: 0x002968F8
		public RanchStation.Instance GetRanchStation()
		{
			if (this.Monitor != null)
			{
				return this.Monitor.TargetRanchStation;
			}
			return null;
		}

		// Token: 0x06006CA7 RID: 27815 RVA: 0x0029870F File Offset: 0x0029690F
		public void EnterQueue()
		{
			this.InitializeWaitCell();
			this.Monitor.NavComponent.GoTo(this.waitCell, null);
		}

		// Token: 0x06006CA8 RID: 27816 RVA: 0x00298730 File Offset: 0x00296930
		public void ExitQueue()
		{
			if (!RanchedStates.IsCrittersTurn(this))
			{
				return;
			}
			if (this.animController.HasAnimation(base.def.EndWaitingAnim) && this.animController.currentAnim != base.def.EndWaitingAnim && base.def.IsQueueAnim(this.animController.currentAnim))
			{
				this.animController.Play(base.def.EndWaitingAnim, KAnim.PlayMode.Once, 1f, 0f);
				return;
			}
			this.GoTo(base.sm.ranch.Move.MoveToRanch);
		}

		// Token: 0x06006CA9 RID: 27817 RVA: 0x002987D3 File Offset: 0x002969D3
		public void AbandonRanchStation()
		{
			if (this.Monitor.TargetRanchStation == null || this.status == StateMachine.Status.Failed)
			{
				return;
			}
			this.StopSM("Abandoned Ranch");
		}

		// Token: 0x06006CAA RID: 27818 RVA: 0x002987F7 File Offset: 0x002969F7
		public int ModifyNavTargetForCritter(int navCell)
		{
			if (base.smi.HasTag(GameTags.Creatures.Flyer))
			{
				return Grid.CellAbove(navCell);
			}
			return navCell;
		}

		// Token: 0x06006CAB RID: 27819 RVA: 0x00298814 File Offset: 0x00296A14
		private void InitializeWaitCell()
		{
			if (this.Monitor == null)
			{
				return;
			}
			int num = 0;
			Extents stationExtents = this.Monitor.TargetRanchStation.StationExtents;
			int num2 = this.ModifyNavTargetForCritter(Grid.XYToCell(stationExtents.x, stationExtents.y));
			int num3 = 0;
			int num4;
			if (Grid.Raycast(num2, new Vector2I(-1, 0), out num4, base.def.WaitCellOffset, ~(Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable)))
			{
				num3 = 1 + base.def.WaitCellOffset - num4;
				num = this.ModifyNavTargetForCritter(Grid.XYToCell(stationExtents.x + 1, stationExtents.y));
			}
			int num5 = 0;
			int num6;
			if (num3 != 0 && Grid.Raycast(num, new Vector2I(1, 0), out num6, base.def.WaitCellOffset, ~(Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable)))
			{
				num5 = base.def.WaitCellOffset - num6;
			}
			int num7 = (base.def.WaitCellOffset - num3) * -1;
			if (num3 == base.def.WaitCellOffset)
			{
				num7 = 1 + base.def.WaitCellOffset - num5;
			}
			CellOffset cellOffset = new CellOffset(num7, 0);
			this.waitCell = Grid.OffsetCell(num2, cellOffset);
		}

		// Token: 0x06006CAC RID: 27820 RVA: 0x00298924 File Offset: 0x00296B24
		public void OnRancherStateChanged(RanchStation.Instance ranch)
		{
			RanchedStates.IRanchStatesCallbacks ranchStatesCallbacks = base.smi.GetCurrentState() as RanchedStates.IRanchStatesCallbacks;
			if (ranchStatesCallbacks == null)
			{
				return;
			}
			ranchStatesCallbacks.OnRancherStateChanged(this);
		}

		// Token: 0x06006CAD RID: 27821 RVA: 0x00298950 File Offset: 0x00296B50
		private void OnAnimComplete(HashedString completedAnim)
		{
			RanchedStates.IRanchStatesCallbacks ranchStatesCallbacks = base.smi.GetCurrentState() as RanchedStates.IRanchStatesCallbacks;
			if (ranchStatesCallbacks == null)
			{
				return;
			}
			ranchStatesCallbacks.OnAnimComplete(this, completedAnim);
		}

		// Token: 0x040051D0 RID: 20944
		public float OriginalSpeed;

		// Token: 0x040051D1 RID: 20945
		private int waitCell;

		// Token: 0x040051D2 RID: 20946
		private KBatchedAnimController animController;

		// Token: 0x040051D3 RID: 20947
		private RanchableMonitor.Instance ranchMonitor;
	}

	// Token: 0x02000E9A RID: 3738
	public class RanchStates : GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State, RanchedStates.IRanchStatesCallbacks
	{
		// Token: 0x06006CAE RID: 27822 RVA: 0x0029897A File Offset: 0x00296B7A
		public void OnRancherStateChanged(RanchedStates.Instance smi)
		{
			if (RanchedStates.IsCrittersTurn(smi))
			{
				smi.GoTo(smi.sm.ranch.Cheer);
				return;
			}
			smi.GoTo(smi.sm.ranch.Move.WaitInLine);
		}

		// Token: 0x06006CAF RID: 27823 RVA: 0x002989B6 File Offset: 0x00296BB6
		public void OnAnimComplete(RanchedStates.Instance smi, HashedString completedAnim)
		{
		}

		// Token: 0x040051D4 RID: 20948
		public RanchedStates.CheerStates Cheer;

		// Token: 0x040051D5 RID: 20949
		public RanchedStates.MoveStates Move;

		// Token: 0x040051D6 RID: 20950
		public GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State Ranching;

		// Token: 0x040051D7 RID: 20951
		public GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State Wavegoodbye;

		// Token: 0x040051D8 RID: 20952
		public GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State Runaway;
	}

	// Token: 0x02000E9B RID: 3739
	public class CheerStates : GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State
	{
		// Token: 0x040051D9 RID: 20953
		public GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State Cheer;

		// Token: 0x040051DA RID: 20954
		public GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State Pst;
	}

	// Token: 0x02000E9C RID: 3740
	public class MoveStates : GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State
	{
		// Token: 0x040051DB RID: 20955
		public RanchedStates.MoveState MoveToRanch;

		// Token: 0x040051DC RID: 20956
		public RanchedStates.WaitState WaitInLine;
	}

	// Token: 0x02000E9D RID: 3741
	public interface IRanchStatesCallbacks
	{
		// Token: 0x06006CB3 RID: 27827
		void OnRancherStateChanged(RanchedStates.Instance smi);

		// Token: 0x06006CB4 RID: 27828
		void OnAnimComplete(RanchedStates.Instance smi, HashedString completedAnim);
	}

	// Token: 0x02000E9E RID: 3742
	public class MoveState : GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State, RanchedStates.IRanchStatesCallbacks
	{
		// Token: 0x06006CB5 RID: 27829 RVA: 0x002989D0 File Offset: 0x00296BD0
		public void OnRancherStateChanged(RanchedStates.Instance smi)
		{
			smi.GoTo(this.sm.ranch.Move.WaitInLine);
		}

		// Token: 0x06006CB6 RID: 27830 RVA: 0x002989ED File Offset: 0x00296BED
		public void OnAnimComplete(RanchedStates.Instance smi, HashedString completedAnim)
		{
		}
	}

	// Token: 0x02000E9F RID: 3743
	public class WaitState : GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State, RanchedStates.IRanchStatesCallbacks
	{
		// Token: 0x06006CB8 RID: 27832 RVA: 0x002989F7 File Offset: 0x00296BF7
		public void OnRancherStateChanged(RanchedStates.Instance smi)
		{
			smi.ExitQueue();
		}

		// Token: 0x06006CB9 RID: 27833 RVA: 0x002989FF File Offset: 0x00296BFF
		public void OnAnimComplete(RanchedStates.Instance smi, HashedString completedAnim)
		{
			if (completedAnim == smi.def.EndWaitingAnim)
			{
				smi.ExitQueue();
			}
		}
	}
}
