using System;
using UnityEngine;

// Token: 0x02000450 RID: 1104
public class AutoStorageDropper : GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>
{
	// Token: 0x060017DF RID: 6111 RVA: 0x0007CFC0 File Offset: 0x0007B1C0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.root.Update(delegate(AutoStorageDropper.Instance smi, float dt)
		{
			smi.UpdateBlockedStatus();
		}, UpdateRate.SIM_200ms, true);
		this.idle.EventTransition(GameHashes.OnStorageChange, this.pre_drop, null).OnSignal(this.checkCanDrop, this.pre_drop, (AutoStorageDropper.Instance smi) => !smi.GetComponent<Storage>().IsEmpty()).ParamTransition<bool>(this.isBlocked, this.blocked, GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.IsTrue);
		this.pre_drop.ScheduleGoTo((AutoStorageDropper.Instance smi) => smi.def.delay, this.dropping);
		this.dropping.Enter(delegate(AutoStorageDropper.Instance smi)
		{
			smi.Drop();
		}).GoTo(this.idle);
		this.blocked.ParamTransition<bool>(this.isBlocked, this.pre_drop, GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.IsFalse).ToggleStatusItem(Db.Get().BuildingStatusItems.OutputTileBlocked, null);
	}

	// Token: 0x04000D39 RID: 3385
	private GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.State idle;

	// Token: 0x04000D3A RID: 3386
	private GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.State pre_drop;

	// Token: 0x04000D3B RID: 3387
	private GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.State dropping;

	// Token: 0x04000D3C RID: 3388
	private GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.State blocked;

	// Token: 0x04000D3D RID: 3389
	private StateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.BoolParameter isBlocked;

	// Token: 0x04000D3E RID: 3390
	public StateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.Signal checkCanDrop;

	// Token: 0x02001066 RID: 4198
	public class DropperFxConfig
	{
		// Token: 0x04005766 RID: 22374
		public string animFile;

		// Token: 0x04005767 RID: 22375
		public string animName;

		// Token: 0x04005768 RID: 22376
		public Grid.SceneLayer layer = Grid.SceneLayer.FXFront;

		// Token: 0x04005769 RID: 22377
		public bool useElementTint = true;

		// Token: 0x0400576A RID: 22378
		public bool flipX;

		// Token: 0x0400576B RID: 22379
		public bool flipY;
	}

	// Token: 0x02001067 RID: 4199
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0400576C RID: 22380
		public CellOffset dropOffset;

		// Token: 0x0400576D RID: 22381
		public bool asOre;

		// Token: 0x0400576E RID: 22382
		public SimHashes[] elementFilter;

		// Token: 0x0400576F RID: 22383
		public bool invertElementFilter;

		// Token: 0x04005770 RID: 22384
		public bool blockedBySubstantialLiquid;

		// Token: 0x04005771 RID: 22385
		public AutoStorageDropper.DropperFxConfig neutralFx;

		// Token: 0x04005772 RID: 22386
		public AutoStorageDropper.DropperFxConfig leftFx;

		// Token: 0x04005773 RID: 22387
		public AutoStorageDropper.DropperFxConfig rightFx;

		// Token: 0x04005774 RID: 22388
		public AutoStorageDropper.DropperFxConfig upFx;

		// Token: 0x04005775 RID: 22389
		public AutoStorageDropper.DropperFxConfig downFx;

		// Token: 0x04005776 RID: 22390
		public Vector3 fxOffset = Vector3.zero;

		// Token: 0x04005777 RID: 22391
		public float cooldown = 2f;

		// Token: 0x04005778 RID: 22392
		public float delay;
	}

	// Token: 0x02001068 RID: 4200
	public new class Instance : GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.GameInstance
	{
		// Token: 0x060072E3 RID: 29411 RVA: 0x002AEDB6 File Offset: 0x002ACFB6
		public Instance(IStateMachineTarget master, AutoStorageDropper.Def def)
			: base(master, def)
		{
		}

		// Token: 0x060072E4 RID: 29412 RVA: 0x002AEDC0 File Offset: 0x002ACFC0
		public void SetInvertElementFilter(bool value)
		{
			base.def.invertElementFilter = value;
			base.smi.sm.checkCanDrop.Trigger(base.smi);
		}

		// Token: 0x060072E5 RID: 29413 RVA: 0x002AEDEC File Offset: 0x002ACFEC
		public void UpdateBlockedStatus()
		{
			int num = Grid.PosToCell(base.smi.GetDropPosition());
			bool flag = Grid.IsSolidCell(num) || (base.def.blockedBySubstantialLiquid && Grid.IsSubstantialLiquid(num, 0.35f));
			base.sm.isBlocked.Set(flag, base.smi, false);
		}

		// Token: 0x060072E6 RID: 29414 RVA: 0x002AEE4C File Offset: 0x002AD04C
		private bool IsFilteredElement(SimHashes element)
		{
			for (int num = 0; num != base.def.elementFilter.Length; num++)
			{
				if (base.def.elementFilter[num] == element)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060072E7 RID: 29415 RVA: 0x002AEE84 File Offset: 0x002AD084
		private bool AllowedToDrop(SimHashes element)
		{
			return base.def.elementFilter == null || base.def.elementFilter.Length == 0 || (!base.def.invertElementFilter && this.IsFilteredElement(element)) || (base.def.invertElementFilter && !this.IsFilteredElement(element));
		}

		// Token: 0x060072E8 RID: 29416 RVA: 0x002AEEE0 File Offset: 0x002AD0E0
		public void Drop()
		{
			bool flag = false;
			Element element = null;
			for (int i = this.m_storage.Count - 1; i >= 0; i--)
			{
				GameObject gameObject = this.m_storage.items[i];
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (this.AllowedToDrop(component.ElementID))
				{
					if (base.def.asOre)
					{
						this.m_storage.Drop(gameObject, true);
						gameObject.transform.SetPosition(this.GetDropPosition());
						element = component.Element;
						flag = true;
					}
					else
					{
						Dumpable component2 = gameObject.GetComponent<Dumpable>();
						if (!component2.IsNullOrDestroyed())
						{
							component2.Dump(this.GetDropPosition());
							element = component.Element;
							flag = true;
						}
					}
				}
			}
			AutoStorageDropper.DropperFxConfig dropperAnim = this.GetDropperAnim();
			if (flag && dropperAnim != null && GameClock.Instance.GetTime() > this.m_timeSinceLastDrop + base.def.cooldown)
			{
				this.m_timeSinceLastDrop = GameClock.Instance.GetTime();
				Vector3 vector = Grid.CellToPosCCC(Grid.PosToCell(this.GetDropPosition()), dropperAnim.layer);
				vector += ((this.m_rotatable != null) ? this.m_rotatable.GetRotatedOffset(base.def.fxOffset) : base.def.fxOffset);
				KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect(dropperAnim.animFile, vector, null, false, dropperAnim.layer, false);
				kbatchedAnimController.destroyOnAnimComplete = false;
				kbatchedAnimController.FlipX = dropperAnim.flipX;
				kbatchedAnimController.FlipY = dropperAnim.flipY;
				if (dropperAnim.useElementTint)
				{
					kbatchedAnimController.TintColour = element.substance.colour;
				}
				kbatchedAnimController.Play(dropperAnim.animName, KAnim.PlayMode.Once, 1f, 0f);
			}
		}

		// Token: 0x060072E9 RID: 29417 RVA: 0x002AF0A8 File Offset: 0x002AD2A8
		public AutoStorageDropper.DropperFxConfig GetDropperAnim()
		{
			CellOffset cellOffset = ((this.m_rotatable != null) ? this.m_rotatable.GetRotatedCellOffset(base.def.dropOffset) : base.def.dropOffset);
			if (cellOffset.x < 0)
			{
				return base.def.leftFx;
			}
			if (cellOffset.x > 0)
			{
				return base.def.rightFx;
			}
			if (cellOffset.y < 0)
			{
				return base.def.downFx;
			}
			if (cellOffset.y > 0)
			{
				return base.def.upFx;
			}
			return base.def.neutralFx;
		}

		// Token: 0x060072EA RID: 29418 RVA: 0x002AF148 File Offset: 0x002AD348
		public Vector3 GetDropPosition()
		{
			if (!(this.m_rotatable != null))
			{
				return base.transform.GetPosition() + base.def.dropOffset.ToVector3();
			}
			return base.transform.GetPosition() + this.m_rotatable.GetRotatedCellOffset(base.def.dropOffset).ToVector3();
		}

		// Token: 0x04005779 RID: 22393
		[MyCmpGet]
		private Storage m_storage;

		// Token: 0x0400577A RID: 22394
		[MyCmpGet]
		private Rotatable m_rotatable;

		// Token: 0x0400577B RID: 22395
		private float m_timeSinceLastDrop;
	}
}
