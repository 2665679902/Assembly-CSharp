using System;
using UnityEngine;

// Token: 0x02000467 RID: 1127
public class CreatureFallMonitor : GameStateMachine<CreatureFallMonitor, CreatureFallMonitor.Instance, IStateMachineTarget, CreatureFallMonitor.Def>
{
	// Token: 0x06001900 RID: 6400 RVA: 0x00085A3C File Offset: 0x00083C3C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.grounded;
		this.grounded.ToggleBehaviour(GameTags.Creatures.Falling, (CreatureFallMonitor.Instance smi) => smi.ShouldFall(), null);
	}

	// Token: 0x04000DFC RID: 3580
	public static float FLOOR_DISTANCE = -0.065f;

	// Token: 0x04000DFD RID: 3581
	public GameStateMachine<CreatureFallMonitor, CreatureFallMonitor.Instance, IStateMachineTarget, CreatureFallMonitor.Def>.State grounded;

	// Token: 0x04000DFE RID: 3582
	public GameStateMachine<CreatureFallMonitor, CreatureFallMonitor.Instance, IStateMachineTarget, CreatureFallMonitor.Def>.State falling;

	// Token: 0x02001099 RID: 4249
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0400582C RID: 22572
		public bool canSwim;

		// Token: 0x0400582D RID: 22573
		public bool checkHead = true;
	}

	// Token: 0x0200109A RID: 4250
	public new class Instance : GameStateMachine<CreatureFallMonitor, CreatureFallMonitor.Instance, IStateMachineTarget, CreatureFallMonitor.Def>.GameInstance
	{
		// Token: 0x060073B6 RID: 29622 RVA: 0x002B145C File Offset: 0x002AF65C
		public Instance(IStateMachineTarget master, CreatureFallMonitor.Def def)
			: base(master, def)
		{
			this.navigator = master.GetComponent<Navigator>();
		}

		// Token: 0x060073B7 RID: 29623 RVA: 0x002B1480 File Offset: 0x002AF680
		public void SnapToGround()
		{
			Vector3 position = base.smi.transform.GetPosition();
			Vector3 vector = Grid.CellToPosCBC(Grid.PosToCell(position), Grid.SceneLayer.Creatures);
			vector.x = position.x;
			base.smi.transform.SetPosition(vector);
			if (this.navigator.IsValidNavType(NavType.Floor))
			{
				this.navigator.SetCurrentNavType(NavType.Floor);
				return;
			}
			if (this.navigator.IsValidNavType(NavType.Hover))
			{
				this.navigator.SetCurrentNavType(NavType.Hover);
			}
		}

		// Token: 0x060073B8 RID: 29624 RVA: 0x002B1500 File Offset: 0x002AF700
		public bool ShouldFall()
		{
			if (base.gameObject.HasTag(GameTags.Stored))
			{
				return false;
			}
			Vector3 position = base.smi.transform.GetPosition();
			int num = Grid.PosToCell(position);
			if (Grid.IsValidCell(num) && Grid.Solid[num])
			{
				return false;
			}
			if (this.navigator.IsMoving())
			{
				return false;
			}
			if (this.CanSwimAtCurrentLocation())
			{
				return false;
			}
			if (this.navigator.CurrentNavType != NavType.Swim)
			{
				if (this.navigator.NavGrid.NavTable.IsValid(num, this.navigator.CurrentNavType))
				{
					return false;
				}
				if (this.navigator.CurrentNavType == NavType.Ceiling)
				{
					return true;
				}
				if (this.navigator.CurrentNavType == NavType.LeftWall)
				{
					return true;
				}
				if (this.navigator.CurrentNavType == NavType.RightWall)
				{
					return true;
				}
			}
			Vector3 vector = position;
			vector.y += CreatureFallMonitor.FLOOR_DISTANCE;
			int num2 = Grid.PosToCell(vector);
			return !Grid.IsValidCell(num2) || !Grid.Solid[num2];
		}

		// Token: 0x060073B9 RID: 29625 RVA: 0x002B1608 File Offset: 0x002AF808
		public bool CanSwimAtCurrentLocation()
		{
			if (base.def.canSwim)
			{
				Vector3 position = base.transform.GetPosition();
				float num = 1f;
				if (!base.def.checkHead)
				{
					num = 0.5f;
				}
				position.y += base.transform.GetComponent<KBoxCollider2D>().size.y * num;
				if (Grid.IsSubstantialLiquid(Grid.PosToCell(position), 0.35f))
				{
					if (!GameComps.Gravities.Has(base.gameObject))
					{
						return true;
					}
					if (GameComps.Gravities.GetData(GameComps.Gravities.GetHandle(base.gameObject)).velocity.magnitude < 2f)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400582E RID: 22574
		public string anim = "fall";

		// Token: 0x0400582F RID: 22575
		private Navigator navigator;
	}
}
