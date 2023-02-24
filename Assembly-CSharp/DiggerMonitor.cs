using System;
using KSerialization;
using ProcGen;
using UnityEngine;

// Token: 0x020006CC RID: 1740
public class DiggerMonitor : GameStateMachine<DiggerMonitor, DiggerMonitor.Instance, IStateMachineTarget, DiggerMonitor.Def>
{
	// Token: 0x06002F58 RID: 12120 RVA: 0x000FA168 File Offset: 0x000F8368
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.loop;
		this.loop.EventTransition(GameHashes.BeginMeteorBombardment, (DiggerMonitor.Instance smi) => Game.Instance, this.dig, (DiggerMonitor.Instance smi) => smi.CanTunnel());
		this.dig.ToggleBehaviour(GameTags.Creatures.Tunnel, (DiggerMonitor.Instance smi) => true, null).GoTo(this.loop);
	}

	// Token: 0x04001C73 RID: 7283
	public GameStateMachine<DiggerMonitor, DiggerMonitor.Instance, IStateMachineTarget, DiggerMonitor.Def>.State loop;

	// Token: 0x04001C74 RID: 7284
	public GameStateMachine<DiggerMonitor, DiggerMonitor.Instance, IStateMachineTarget, DiggerMonitor.Def>.State dig;

	// Token: 0x020013B7 RID: 5047
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06007EB5 RID: 32437 RVA: 0x002D9A4D File Offset: 0x002D7C4D
		// (set) Token: 0x06007EB6 RID: 32438 RVA: 0x002D9A55 File Offset: 0x002D7C55
		public int depthToDig { get; set; }
	}

	// Token: 0x020013B8 RID: 5048
	public new class Instance : GameStateMachine<DiggerMonitor, DiggerMonitor.Instance, IStateMachineTarget, DiggerMonitor.Def>.GameInstance
	{
		// Token: 0x06007EB8 RID: 32440 RVA: 0x002D9A68 File Offset: 0x002D7C68
		public Instance(IStateMachineTarget master, DiggerMonitor.Def def)
			: base(master, def)
		{
			global::World instance = global::World.Instance;
			instance.OnSolidChanged = (Action<int>)Delegate.Combine(instance.OnSolidChanged, new Action<int>(this.OnSolidChanged));
			this.OnDestinationReachedDelegate = new Action<object>(this.OnDestinationReached);
			master.Subscribe(387220196, this.OnDestinationReachedDelegate);
			master.Subscribe(-766531887, this.OnDestinationReachedDelegate);
		}

		// Token: 0x06007EB9 RID: 32441 RVA: 0x002D9AE0 File Offset: 0x002D7CE0
		protected override void OnCleanUp()
		{
			base.OnCleanUp();
			global::World instance = global::World.Instance;
			instance.OnSolidChanged = (Action<int>)Delegate.Remove(instance.OnSolidChanged, new Action<int>(this.OnSolidChanged));
			base.master.Unsubscribe(387220196, this.OnDestinationReachedDelegate);
			base.master.Unsubscribe(-766531887, this.OnDestinationReachedDelegate);
		}

		// Token: 0x06007EBA RID: 32442 RVA: 0x002D9B45 File Offset: 0x002D7D45
		private void OnDestinationReached(object data)
		{
			this.CheckInSolid();
		}

		// Token: 0x06007EBB RID: 32443 RVA: 0x002D9B50 File Offset: 0x002D7D50
		private void CheckInSolid()
		{
			Navigator component = base.gameObject.GetComponent<Navigator>();
			if (component == null)
			{
				return;
			}
			int num = Grid.PosToCell(base.gameObject);
			if (component.CurrentNavType != NavType.Solid && Grid.IsSolidCell(num))
			{
				component.SetCurrentNavType(NavType.Solid);
				return;
			}
			if (component.CurrentNavType == NavType.Solid && !Grid.IsSolidCell(num))
			{
				component.SetCurrentNavType(NavType.Floor);
				base.gameObject.AddTag(GameTags.Creatures.Falling);
			}
		}

		// Token: 0x06007EBC RID: 32444 RVA: 0x002D9BC3 File Offset: 0x002D7DC3
		private void OnSolidChanged(int cell)
		{
			this.CheckInSolid();
		}

		// Token: 0x06007EBD RID: 32445 RVA: 0x002D9BCC File Offset: 0x002D7DCC
		public bool CanTunnel()
		{
			int num = Grid.PosToCell(this);
			if (global::World.Instance.zoneRenderData.GetSubWorldZoneType(num) == SubWorld.ZoneType.Space)
			{
				int num2 = num;
				while (Grid.IsValidCell(num2) && !Grid.Solid[num2])
				{
					num2 = Grid.CellAbove(num2);
				}
				if (!Grid.IsValidCell(num2))
				{
					return this.FoundValidDigCell();
				}
			}
			return false;
		}

		// Token: 0x06007EBE RID: 32446 RVA: 0x002D9C24 File Offset: 0x002D7E24
		private bool FoundValidDigCell()
		{
			int num = base.smi.def.depthToDig;
			int num2 = Grid.PosToCell(base.smi.master.gameObject);
			this.lastDigCell = num2;
			int num3 = Grid.CellBelow(num2);
			while (this.IsValidDigCell(num3, null) && num > 0)
			{
				num3 = Grid.CellBelow(num3);
				num--;
			}
			if (num > 0)
			{
				num3 = GameUtil.FloodFillFind<object>(new Func<int, object, bool>(this.IsValidDigCell), null, num2, base.smi.def.depthToDig, false, true);
			}
			this.lastDigCell = num3;
			return this.lastDigCell != -1;
		}

		// Token: 0x06007EBF RID: 32447 RVA: 0x002D9CC0 File Offset: 0x002D7EC0
		private bool IsValidDigCell(int cell, object arg = null)
		{
			if (Grid.IsValidCell(cell) && Grid.Solid[cell])
			{
				if (!Grid.HasDoor[cell] && !Grid.Foundation[cell])
				{
					ushort num = Grid.ElementIdx[cell];
					Element element = ElementLoader.elements[(int)num];
					return Grid.Element[cell].hardness < 150 && !element.HasTag(GameTags.RefinedMetal);
				}
				GameObject gameObject = Grid.Objects[cell, 1];
				if (gameObject != null)
				{
					PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
					return Grid.Element[cell].hardness < 150 && !component.Element.HasTag(GameTags.RefinedMetal);
				}
			}
			return false;
		}

		// Token: 0x04006161 RID: 24929
		[Serialize]
		public int lastDigCell = -1;

		// Token: 0x04006162 RID: 24930
		private Action<object> OnDestinationReachedDelegate;
	}
}
