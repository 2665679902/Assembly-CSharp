using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200047E RID: 1150
public class FallMonitor : GameStateMachine<FallMonitor, FallMonitor.Instance>
{
	// Token: 0x060019B5 RID: 6581 RVA: 0x0008A088 File Offset: 0x00088288
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.standing;
		this.root.TagTransition(GameTags.Stored, this.instorage, false).Update("CheckLanded", delegate(FallMonitor.Instance smi, float dt)
		{
			smi.UpdateFalling();
		}, UpdateRate.SIM_33ms, true);
		this.standing.ParamTransition<bool>(this.isEntombed, this.entombed, GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.IsTrue).ParamTransition<bool>(this.isFalling, this.falling_pre, GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.IsTrue);
		this.falling_pre.Enter("StopNavigator", delegate(FallMonitor.Instance smi)
		{
			smi.GetComponent<Navigator>().Stop(false, true);
		}).Enter("AttemptInitialRecovery", delegate(FallMonitor.Instance smi)
		{
			smi.AttemptInitialRecovery();
		}).GoTo(this.falling)
			.ToggleBrain("falling_pre");
		this.falling.ToggleBrain("falling").PlayAnim("fall_pre").QueueAnim("fall_loop", true, null)
			.ParamTransition<bool>(this.isEntombed, this.entombed, GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.IsTrue)
			.Transition(this.recoverladder, (FallMonitor.Instance smi) => smi.CanRecoverToLadder(), UpdateRate.SIM_33ms)
			.Transition(this.recoverpole, (FallMonitor.Instance smi) => smi.CanRecoverToPole(), UpdateRate.SIM_33ms)
			.ToggleGravity(this.landfloor);
		this.recoverinitialfall.ToggleBrain("recoverinitialfall").Enter("Recover", delegate(FallMonitor.Instance smi)
		{
			smi.Recover();
		}).EventTransition(GameHashes.DestinationReached, this.standing, null)
			.EventTransition(GameHashes.NavigationFailed, this.standing, null)
			.Exit(delegate(FallMonitor.Instance smi)
			{
				smi.RecoverEmote();
			});
		this.landfloor.Enter("Land", delegate(FallMonitor.Instance smi)
		{
			smi.LandFloor();
		}).GoTo(this.standing);
		this.recoverladder.ToggleBrain("recoverladder").PlayAnim("floor_ladder_0_0").Enter("MountLadder", delegate(FallMonitor.Instance smi)
		{
			smi.MountLadder();
		})
			.OnAnimQueueComplete(this.standing);
		this.recoverpole.ToggleBrain("recoverpole").PlayAnim("floor_pole_0_0").Enter("MountPole", delegate(FallMonitor.Instance smi)
		{
			smi.MountPole();
		})
			.OnAnimQueueComplete(this.standing);
		this.instorage.TagTransition(GameTags.Stored, this.standing, true);
		this.entombed.DefaultState(this.entombed.recovering);
		this.entombed.recovering.Enter("TryEntombedEscape", delegate(FallMonitor.Instance smi)
		{
			smi.TryEntombedEscape();
		});
		this.entombed.stuck.Enter("StopNavigator", delegate(FallMonitor.Instance smi)
		{
			smi.GetComponent<Navigator>().Stop(false, true);
		}).ToggleChore((FallMonitor.Instance smi) => new EntombedChore(smi.master, smi.entombedAnimOverride), this.standing).ParamTransition<bool>(this.isEntombed, this.standing, GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.IsFalse);
	}

	// Token: 0x04000E5F RID: 3679
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State standing;

	// Token: 0x04000E60 RID: 3680
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State falling_pre;

	// Token: 0x04000E61 RID: 3681
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State falling;

	// Token: 0x04000E62 RID: 3682
	public FallMonitor.EntombedStates entombed;

	// Token: 0x04000E63 RID: 3683
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State recoverladder;

	// Token: 0x04000E64 RID: 3684
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State recoverpole;

	// Token: 0x04000E65 RID: 3685
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State recoverinitialfall;

	// Token: 0x04000E66 RID: 3686
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State landfloor;

	// Token: 0x04000E67 RID: 3687
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State instorage;

	// Token: 0x04000E68 RID: 3688
	public StateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.BoolParameter isEntombed;

	// Token: 0x04000E69 RID: 3689
	public StateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.BoolParameter isFalling;

	// Token: 0x020010C7 RID: 4295
	public class EntombedStates : GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x0400589C RID: 22684
		public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State recovering;

		// Token: 0x0400589D RID: 22685
		public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State stuck;
	}

	// Token: 0x020010C8 RID: 4296
	public new class Instance : GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600744B RID: 29771 RVA: 0x002B27F4 File Offset: 0x002B09F4
		public Instance(IStateMachineTarget master, bool shouldPlayEmotes, string entombedAnimOverride = null)
			: base(master)
		{
			this.navigator = base.GetComponent<Navigator>();
			this.shouldPlayEmotes = shouldPlayEmotes;
			this.entombedAnimOverride = entombedAnimOverride;
			Pathfinding.Instance.FlushNavGridsOnLoad();
			base.Subscribe(915392638, new Action<object>(this.OnCellChanged));
			base.Subscribe(1027377649, new Action<object>(this.OnMovementStateChanged));
			base.Subscribe(387220196, new Action<object>(this.OnDestinationReached));
		}

		// Token: 0x0600744C RID: 29772 RVA: 0x002B28F4 File Offset: 0x002B0AF4
		private void OnDestinationReached(object data)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			if (!this.safeCells.Contains(num))
			{
				this.safeCells.Add(num);
				if (this.safeCells.Count > this.MAX_CELLS_TRACKED)
				{
					this.safeCells.RemoveAt(0);
				}
			}
		}

		// Token: 0x0600744D RID: 29773 RVA: 0x002B294C File Offset: 0x002B0B4C
		private void OnMovementStateChanged(object data)
		{
			if ((GameHashes)data == GameHashes.ObjectMovementWakeUp)
			{
				int num = Grid.PosToCell(base.transform.GetPosition());
				if (!this.safeCells.Contains(num))
				{
					this.safeCells.Add(num);
					if (this.safeCells.Count > this.MAX_CELLS_TRACKED)
					{
						this.safeCells.RemoveAt(0);
					}
				}
			}
		}

		// Token: 0x0600744E RID: 29774 RVA: 0x002B29B0 File Offset: 0x002B0BB0
		private void OnCellChanged(object data)
		{
			int num = (int)data;
			if (!this.safeCells.Contains(num))
			{
				this.safeCells.Add(num);
				if (this.safeCells.Count > this.MAX_CELLS_TRACKED)
				{
					this.safeCells.RemoveAt(0);
				}
			}
		}

		// Token: 0x0600744F RID: 29775 RVA: 0x002B2A00 File Offset: 0x002B0C00
		public void Recover()
		{
			int num = Grid.PosToCell(this.navigator);
			foreach (NavGrid.Transition transition in this.navigator.NavGrid.transitions)
			{
				if (transition.isEscape && this.navigator.CurrentNavType == transition.start)
				{
					int num2 = transition.IsValid(num, this.navigator.NavGrid.NavTable);
					if (Grid.InvalidCell != num2)
					{
						Vector2I vector2I = Grid.CellToXY(num);
						Vector2I vector2I2 = Grid.CellToXY(num2);
						this.flipRecoverEmote = vector2I2.x < vector2I.x;
						this.navigator.BeginTransition(transition);
						return;
					}
				}
			}
		}

		// Token: 0x06007450 RID: 29776 RVA: 0x002B2AB8 File Offset: 0x002B0CB8
		public void RecoverEmote()
		{
			if (!this.shouldPlayEmotes)
			{
				return;
			}
			if (UnityEngine.Random.Range(0, 9) == 8)
			{
				new EmoteChore(base.master.GetComponent<ChoreProvider>(), Db.Get().ChoreTypes.EmoteHighPriority, Db.Get().Emotes.Minion.CloseCall_Fall, KAnim.PlayMode.Once, 1, this.flipRecoverEmote);
			}
		}

		// Token: 0x06007451 RID: 29777 RVA: 0x002B2B15 File Offset: 0x002B0D15
		public void LandFloor()
		{
			this.navigator.SetCurrentNavType(NavType.Floor);
			base.GetComponent<Transform>().SetPosition(Grid.CellToPosCBC(Grid.PosToCell(base.GetComponent<Transform>().GetPosition()), Grid.SceneLayer.Move));
		}

		// Token: 0x06007452 RID: 29778 RVA: 0x002B2B48 File Offset: 0x002B0D48
		public void AttemptInitialRecovery()
		{
			if (base.gameObject.HasTag(GameTags.Incapacitated))
			{
				return;
			}
			int num = Grid.PosToCell(this.navigator);
			foreach (NavGrid.Transition transition in this.navigator.NavGrid.transitions)
			{
				if (transition.isEscape && this.navigator.CurrentNavType == transition.start)
				{
					int num2 = transition.IsValid(num, this.navigator.NavGrid.NavTable);
					if (Grid.InvalidCell != num2)
					{
						base.smi.GoTo(base.smi.sm.recoverinitialfall);
						return;
					}
				}
			}
		}

		// Token: 0x06007453 RID: 29779 RVA: 0x002B2BF8 File Offset: 0x002B0DF8
		public bool CanRecoverToLadder()
		{
			int num = Grid.PosToCell(base.master.transform.GetPosition());
			return this.navigator.NavGrid.NavTable.IsValid(num, NavType.Ladder) && !base.gameObject.HasTag(GameTags.Incapacitated);
		}

		// Token: 0x06007454 RID: 29780 RVA: 0x002B2C49 File Offset: 0x002B0E49
		public void MountLadder()
		{
			this.navigator.SetCurrentNavType(NavType.Ladder);
			base.GetComponent<Transform>().SetPosition(Grid.CellToPosCBC(Grid.PosToCell(base.GetComponent<Transform>().GetPosition()), Grid.SceneLayer.Move));
		}

		// Token: 0x06007455 RID: 29781 RVA: 0x002B2C7C File Offset: 0x002B0E7C
		public bool CanRecoverToPole()
		{
			int num = Grid.PosToCell(base.master.transform.GetPosition());
			return this.navigator.NavGrid.NavTable.IsValid(num, NavType.Pole) && !base.gameObject.HasTag(GameTags.Incapacitated);
		}

		// Token: 0x06007456 RID: 29782 RVA: 0x002B2CCD File Offset: 0x002B0ECD
		public void MountPole()
		{
			this.navigator.SetCurrentNavType(NavType.Pole);
			base.GetComponent<Transform>().SetPosition(Grid.CellToPosCBC(Grid.PosToCell(base.GetComponent<Transform>().GetPosition()), Grid.SceneLayer.Move));
		}

		// Token: 0x06007457 RID: 29783 RVA: 0x002B2D00 File Offset: 0x002B0F00
		public void UpdateFalling()
		{
			bool flag = false;
			bool flag2 = false;
			if (!this.navigator.IsMoving() && this.navigator.CurrentNavType != NavType.Tube)
			{
				int num = Grid.PosToCell(base.transform.GetPosition());
				int num2 = Grid.CellAbove(num);
				bool flag3 = Grid.IsValidCell(num);
				bool flag4 = Grid.IsValidCell(num2);
				bool flag5 = this.IsValidNavCell(num);
				flag5 = flag5 && (!base.gameObject.HasTag(GameTags.Incapacitated) || (this.navigator.CurrentNavType != NavType.Ladder && this.navigator.CurrentNavType != NavType.Pole));
				flag2 = (!flag5 && flag3 && Grid.Solid[num] && !Grid.DupePassable[num]) || (flag4 && Grid.Solid[num2] && !Grid.DupePassable[num2]) || (flag3 && Grid.DupeImpassable[num]) || (flag4 && Grid.DupeImpassable[num2]);
				flag = !flag5 && !flag2;
				if ((!flag3 && flag4) || (flag4 && Grid.WorldIdx[num] != Grid.WorldIdx[num2] && Grid.IsWorldValidCell(num2)))
				{
					this.TeleportInWorld(num);
				}
			}
			base.sm.isFalling.Set(flag, base.smi, false);
			base.sm.isEntombed.Set(flag2, base.smi, false);
		}

		// Token: 0x06007458 RID: 29784 RVA: 0x002B2E80 File Offset: 0x002B1080
		private void TeleportInWorld(int cell)
		{
			int num = Grid.CellAbove(cell);
			WorldContainer world = ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[num]);
			if (world != null)
			{
				int safeCell = world.GetSafeCell();
				global::Debug.Log(string.Format("Teleporting {0} to {1}", this.navigator.name, safeCell));
				this.MoveToCell(safeCell, false);
				return;
			}
			global::Debug.LogError(string.Format("Unable to teleport {0} stuck on {1}", this.navigator.name, cell));
		}

		// Token: 0x06007459 RID: 29785 RVA: 0x002B2EFF File Offset: 0x002B10FF
		private bool IsValidNavCell(int cell)
		{
			return this.navigator.NavGrid.NavTable.IsValid(cell, this.navigator.CurrentNavType) && !Grid.DupeImpassable[cell];
		}

		// Token: 0x0600745A RID: 29786 RVA: 0x002B2F34 File Offset: 0x002B1134
		public void TryEntombedEscape()
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			int backCell = base.GetComponent<Facing>().GetBackCell();
			int num2 = Grid.CellAbove(backCell);
			int num3 = Grid.CellBelow(backCell);
			foreach (int num4 in new int[] { backCell, num2, num3 })
			{
				if (this.IsValidNavCell(num4) && !Grid.HasDoor[num4])
				{
					this.MoveToCell(num4, false);
					return;
				}
			}
			int num5 = Grid.PosToCell(base.transform.GetPosition());
			foreach (CellOffset cellOffset in this.entombedEscapeOffsets)
			{
				if (Grid.IsCellOffsetValid(num5, cellOffset))
				{
					int num6 = Grid.OffsetCell(num5, cellOffset);
					if (this.IsValidNavCell(num6) && !Grid.HasDoor[num6])
					{
						this.MoveToCell(num6, false);
						return;
					}
				}
			}
			for (int k = this.safeCells.Count - 1; k >= 0; k--)
			{
				int num7 = this.safeCells[k];
				if (num7 != num && this.IsValidNavCell(num7) && !Grid.HasDoor[num7])
				{
					this.MoveToCell(num7, false);
					return;
				}
			}
			foreach (CellOffset cellOffset2 in this.entombedEscapeOffsets)
			{
				if (Grid.IsCellOffsetValid(num5, cellOffset2))
				{
					int num8 = Grid.OffsetCell(num5, cellOffset2);
					int num9 = Grid.CellAbove(num8);
					if (Grid.IsValidCell(num9) && !Grid.Solid[num8] && !Grid.Solid[num9] && !Grid.DupeImpassable[num8] && !Grid.DupeImpassable[num9] && !Grid.HasDoor[num8] && !Grid.HasDoor[num9])
					{
						this.MoveToCell(num8, true);
						return;
					}
				}
			}
			this.GoTo(base.sm.entombed.stuck);
		}

		// Token: 0x0600745B RID: 29787 RVA: 0x002B3148 File Offset: 0x002B1348
		private void MoveToCell(int cell, bool forceFloorNav = false)
		{
			base.transform.SetPosition(Grid.CellToPosCBC(cell, Grid.SceneLayer.Move));
			base.transform.GetComponent<Navigator>().Stop(false, true);
			if (base.gameObject.HasTag(GameTags.Incapacitated) || forceFloorNav)
			{
				base.transform.GetComponent<Navigator>().SetCurrentNavType(NavType.Floor);
			}
			this.UpdateFalling();
			if (base.sm.isEntombed.Get(base.smi))
			{
				this.GoTo(base.sm.entombed.stuck);
				return;
			}
			this.GoTo(base.sm.standing);
		}

		// Token: 0x0400589E RID: 22686
		private CellOffset[] entombedEscapeOffsets = new CellOffset[]
		{
			new CellOffset(0, 1),
			new CellOffset(1, 0),
			new CellOffset(-1, 0),
			new CellOffset(1, 1),
			new CellOffset(-1, 1),
			new CellOffset(1, -1),
			new CellOffset(-1, -1)
		};

		// Token: 0x0400589F RID: 22687
		private Navigator navigator;

		// Token: 0x040058A0 RID: 22688
		private bool shouldPlayEmotes;

		// Token: 0x040058A1 RID: 22689
		public string entombedAnimOverride;

		// Token: 0x040058A2 RID: 22690
		private List<int> safeCells = new List<int>();

		// Token: 0x040058A3 RID: 22691
		private int MAX_CELLS_TRACKED = 3;

		// Token: 0x040058A4 RID: 22692
		private bool flipRecoverEmote;
	}
}
