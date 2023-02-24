using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000650 RID: 1616
[AddComponentMenu("KMonoBehaviour/scripts/SuitMarker")]
public class SuitMarker : KMonoBehaviour
{
	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x06002B1F RID: 11039 RVA: 0x000E37C4 File Offset: 0x000E19C4
	// (set) Token: 0x06002B20 RID: 11040 RVA: 0x000E37E4 File Offset: 0x000E19E4
	private bool OnlyTraverseIfUnequipAvailable
	{
		get
		{
			DebugUtil.Assert(this.onlyTraverseIfUnequipAvailable == (this.gridFlags & Grid.SuitMarker.Flags.OnlyTraverseIfUnequipAvailable) > (Grid.SuitMarker.Flags)0);
			return this.onlyTraverseIfUnequipAvailable;
		}
		set
		{
			this.onlyTraverseIfUnequipAvailable = value;
			this.UpdateGridFlag(Grid.SuitMarker.Flags.OnlyTraverseIfUnequipAvailable, this.onlyTraverseIfUnequipAvailable);
		}
	}

	// Token: 0x170002EA RID: 746
	// (get) Token: 0x06002B21 RID: 11041 RVA: 0x000E37FA File Offset: 0x000E19FA
	// (set) Token: 0x06002B22 RID: 11042 RVA: 0x000E3807 File Offset: 0x000E1A07
	private bool isRotated
	{
		get
		{
			return (this.gridFlags & Grid.SuitMarker.Flags.Rotated) > (Grid.SuitMarker.Flags)0;
		}
		set
		{
			this.UpdateGridFlag(Grid.SuitMarker.Flags.Rotated, value);
		}
	}

	// Token: 0x170002EB RID: 747
	// (get) Token: 0x06002B23 RID: 11043 RVA: 0x000E3811 File Offset: 0x000E1A11
	// (set) Token: 0x06002B24 RID: 11044 RVA: 0x000E381E File Offset: 0x000E1A1E
	private bool isOperational
	{
		get
		{
			return (this.gridFlags & Grid.SuitMarker.Flags.Operational) > (Grid.SuitMarker.Flags)0;
		}
		set
		{
			this.UpdateGridFlag(Grid.SuitMarker.Flags.Operational, value);
		}
	}

	// Token: 0x06002B25 RID: 11045 RVA: 0x000E3828 File Offset: 0x000E1A28
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.OnlyTraverseIfUnequipAvailable = this.onlyTraverseIfUnequipAvailable;
		global::Debug.Assert(this.interactAnim != null, "interactAnim is null");
		base.Subscribe<SuitMarker>(493375141, SuitMarker.OnRefreshUserMenuDelegate);
		this.isOperational = base.GetComponent<Operational>().IsOperational;
		base.Subscribe<SuitMarker>(-592767678, SuitMarker.OnOperationalChangedDelegate);
		this.isRotated = base.GetComponent<Rotatable>().IsRotated;
		base.Subscribe<SuitMarker>(-1643076535, SuitMarker.OnRotatedDelegate);
		this.CreateNewEquipReactable();
		this.CreateNewUnequipReactable();
		this.cell = Grid.PosToCell(this);
		Grid.RegisterSuitMarker(this.cell);
		base.GetComponent<KAnimControllerBase>().Play("no_suit", KAnim.PlayMode.Once, 1f, 0f);
		Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Suits, true);
		this.RefreshTraverseIfUnequipStatusItem();
		SuitLocker.UpdateSuitMarkerStates(Grid.PosToCell(base.transform.position), base.gameObject);
	}

	// Token: 0x06002B26 RID: 11046 RVA: 0x000E3924 File Offset: 0x000E1B24
	private void CreateNewEquipReactable()
	{
		this.equipReactable = new SuitMarker.EquipSuitReactable(this);
	}

	// Token: 0x06002B27 RID: 11047 RVA: 0x000E3932 File Offset: 0x000E1B32
	private void CreateNewUnequipReactable()
	{
		this.unequipReactable = new SuitMarker.UnequipSuitReactable(this);
	}

	// Token: 0x06002B28 RID: 11048 RVA: 0x000E3940 File Offset: 0x000E1B40
	public void GetAttachedLockers(List<SuitLocker> suit_lockers)
	{
		int num = (this.isRotated ? 1 : (-1));
		int num2 = 1;
		for (;;)
		{
			int num3 = Grid.OffsetCell(this.cell, num2 * num, 0);
			GameObject gameObject = Grid.Objects[num3, 1];
			if (gameObject == null)
			{
				break;
			}
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			if (!(component == null))
			{
				if (!component.IsAnyPrefabID(this.LockerTags))
				{
					break;
				}
				SuitLocker component2 = gameObject.GetComponent<SuitLocker>();
				if (component2 == null)
				{
					break;
				}
				if (!suit_lockers.Contains(component2))
				{
					suit_lockers.Add(component2);
				}
			}
			num2++;
		}
	}

	// Token: 0x06002B29 RID: 11049 RVA: 0x000E39CD File Offset: 0x000E1BCD
	public static bool DoesTraversalDirectionRequireSuit(int source_cell, int dest_cell, Grid.SuitMarker.Flags flags)
	{
		return Grid.CellColumn(dest_cell) > Grid.CellColumn(source_cell) == ((flags & Grid.SuitMarker.Flags.Rotated) == (Grid.SuitMarker.Flags)0);
	}

	// Token: 0x06002B2A RID: 11050 RVA: 0x000E39E5 File Offset: 0x000E1BE5
	public bool DoesTraversalDirectionRequireSuit(int source_cell, int dest_cell)
	{
		return SuitMarker.DoesTraversalDirectionRequireSuit(source_cell, dest_cell, this.gridFlags);
	}

	// Token: 0x06002B2B RID: 11051 RVA: 0x000E39F4 File Offset: 0x000E1BF4
	private void Update()
	{
		ListPool<SuitLocker, SuitMarker>.PooledList pooledList = ListPool<SuitLocker, SuitMarker>.Allocate();
		this.GetAttachedLockers(pooledList);
		int num = 0;
		int num2 = 0;
		KPrefabID kprefabID = null;
		foreach (SuitLocker suitLocker in pooledList)
		{
			if (suitLocker.CanDropOffSuit())
			{
				num++;
			}
			if (suitLocker.GetPartiallyChargedOutfit() != null)
			{
				num2++;
			}
			if (kprefabID == null)
			{
				kprefabID = suitLocker.GetStoredOutfit();
			}
		}
		pooledList.Recycle();
		bool flag = kprefabID != null;
		if (flag != this.hasAvailableSuit)
		{
			base.GetComponent<KAnimControllerBase>().Play(flag ? "off" : "no_suit", KAnim.PlayMode.Once, 1f, 0f);
			this.hasAvailableSuit = flag;
		}
		Grid.UpdateSuitMarker(this.cell, num2, num, this.gridFlags, this.PathFlag);
	}

	// Token: 0x06002B2C RID: 11052 RVA: 0x000E3AE8 File Offset: 0x000E1CE8
	private void RefreshTraverseIfUnequipStatusItem()
	{
		if (this.OnlyTraverseIfUnequipAvailable)
		{
			base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.SuitMarkerTraversalOnlyWhenRoomAvailable, null);
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.SuitMarkerTraversalAnytime, false);
			return;
		}
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.SuitMarkerTraversalOnlyWhenRoomAvailable, false);
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.SuitMarkerTraversalAnytime, null);
	}

	// Token: 0x06002B2D RID: 11053 RVA: 0x000E3B6E File Offset: 0x000E1D6E
	private void OnEnableTraverseIfUnequipAvailable()
	{
		this.OnlyTraverseIfUnequipAvailable = true;
		this.RefreshTraverseIfUnequipStatusItem();
	}

	// Token: 0x06002B2E RID: 11054 RVA: 0x000E3B7D File Offset: 0x000E1D7D
	private void OnDisableTraverseIfUnequipAvailable()
	{
		this.OnlyTraverseIfUnequipAvailable = false;
		this.RefreshTraverseIfUnequipStatusItem();
	}

	// Token: 0x06002B2F RID: 11055 RVA: 0x000E3B8C File Offset: 0x000E1D8C
	private void UpdateGridFlag(Grid.SuitMarker.Flags flag, bool state)
	{
		if (state)
		{
			this.gridFlags |= flag;
			return;
		}
		this.gridFlags &= ~flag;
	}

	// Token: 0x06002B30 RID: 11056 RVA: 0x000E3BB0 File Offset: 0x000E1DB0
	private void OnOperationalChanged(bool isOperational)
	{
		SuitLocker.UpdateSuitMarkerStates(Grid.PosToCell(base.transform.position), base.gameObject);
		this.isOperational = isOperational;
	}

	// Token: 0x06002B31 RID: 11057 RVA: 0x000E3BD4 File Offset: 0x000E1DD4
	private void OnRefreshUserMenu(object data)
	{
		KIconButtonMenu.ButtonInfo buttonInfo = ((!this.OnlyTraverseIfUnequipAvailable) ? new KIconButtonMenu.ButtonInfo("action_clearance", UI.USERMENUACTIONS.SUIT_MARKER_TRAVERSAL.ONLY_WHEN_ROOM_AVAILABLE.NAME, new System.Action(this.OnEnableTraverseIfUnequipAvailable), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.SUIT_MARKER_TRAVERSAL.ONLY_WHEN_ROOM_AVAILABLE.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_clearance", UI.USERMENUACTIONS.SUIT_MARKER_TRAVERSAL.ALWAYS.NAME, new System.Action(this.OnDisableTraverseIfUnequipAvailable), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.SUIT_MARKER_TRAVERSAL.ALWAYS.TOOLTIP, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x06002B32 RID: 11058 RVA: 0x000E3C70 File Offset: 0x000E1E70
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (base.isSpawned)
		{
			Grid.UnregisterSuitMarker(this.cell);
		}
		if (this.equipReactable != null)
		{
			this.equipReactable.Cleanup();
		}
		if (this.unequipReactable != null)
		{
			this.unequipReactable.Cleanup();
		}
		SuitLocker.UpdateSuitMarkerStates(Grid.PosToCell(base.transform.position), null);
	}

	// Token: 0x04001991 RID: 6545
	[MyCmpGet]
	private Building building;

	// Token: 0x04001992 RID: 6546
	private SuitMarker.SuitMarkerReactable equipReactable;

	// Token: 0x04001993 RID: 6547
	private SuitMarker.SuitMarkerReactable unequipReactable;

	// Token: 0x04001994 RID: 6548
	private bool hasAvailableSuit;

	// Token: 0x04001995 RID: 6549
	[Serialize]
	private bool onlyTraverseIfUnequipAvailable;

	// Token: 0x04001996 RID: 6550
	private Grid.SuitMarker.Flags gridFlags;

	// Token: 0x04001997 RID: 6551
	private int cell;

	// Token: 0x04001998 RID: 6552
	public Tag[] LockerTags;

	// Token: 0x04001999 RID: 6553
	public PathFinder.PotentialPath.Flags PathFlag;

	// Token: 0x0400199A RID: 6554
	public KAnimFile interactAnim = Assets.GetAnim("anim_equip_clothing_kanim");

	// Token: 0x0400199B RID: 6555
	private static readonly EventSystem.IntraObjectHandler<SuitMarker> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<SuitMarker>(delegate(SuitMarker component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x0400199C RID: 6556
	private static readonly EventSystem.IntraObjectHandler<SuitMarker> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<SuitMarker>(delegate(SuitMarker component, object data)
	{
		component.OnOperationalChanged((bool)data);
	});

	// Token: 0x0400199D RID: 6557
	private static readonly EventSystem.IntraObjectHandler<SuitMarker> OnRotatedDelegate = new EventSystem.IntraObjectHandler<SuitMarker>(delegate(SuitMarker component, object data)
	{
		component.isRotated = ((Rotatable)data).IsRotated;
	});

	// Token: 0x02001301 RID: 4865
	private class EquipSuitReactable : SuitMarker.SuitMarkerReactable
	{
		// Token: 0x06007C28 RID: 31784 RVA: 0x002CF2DF File Offset: 0x002CD4DF
		public EquipSuitReactable(SuitMarker marker)
			: base("EquipSuitReactable", marker)
		{
		}

		// Token: 0x06007C29 RID: 31785 RVA: 0x002CF2F4 File Offset: 0x002CD4F4
		public override bool InternalCanBegin(GameObject newReactor, Navigator.ActiveTransition transition)
		{
			return !newReactor.GetComponent<MinionIdentity>().GetEquipment().IsSlotOccupied(Db.Get().AssignableSlots.Suit) && base.InternalCanBegin(newReactor, transition) && Grid.HasSuit(Grid.PosToCell(this.suitMarker), newReactor.GetComponent<KPrefabID>().InstanceID);
		}

		// Token: 0x06007C2A RID: 31786 RVA: 0x002CF349 File Offset: 0x002CD549
		protected override void InternalBegin()
		{
			base.InternalBegin();
			this.suitMarker.CreateNewEquipReactable();
		}

		// Token: 0x06007C2B RID: 31787 RVA: 0x002CF35C File Offset: 0x002CD55C
		protected override bool MovingTheRightWay(GameObject newReactor, Navigator.ActiveTransition transition)
		{
			bool flag = transition.navGridTransition.x < 0;
			return this.IsRocketDoorExitEquip(newReactor, transition) || flag == this.suitMarker.isRotated;
		}

		// Token: 0x06007C2C RID: 31788 RVA: 0x002CF394 File Offset: 0x002CD594
		private bool IsRocketDoorExitEquip(GameObject new_reactor, Navigator.ActiveTransition transition)
		{
			bool flag = transition.end != NavType.Teleport && transition.start != NavType.Teleport;
			return transition.navGridTransition.x == 0 && new_reactor.GetMyWorld().IsModuleInterior && !flag;
		}

		// Token: 0x06007C2D RID: 31789 RVA: 0x002CF3DC File Offset: 0x002CD5DC
		protected override void Run()
		{
			ListPool<SuitLocker, SuitMarker>.PooledList pooledList = ListPool<SuitLocker, SuitMarker>.Allocate();
			this.suitMarker.GetAttachedLockers(pooledList);
			SuitLocker suitLocker = null;
			for (int i = 0; i < pooledList.Count; i++)
			{
				float suitScore = pooledList[i].GetSuitScore();
				if (suitScore >= 1f)
				{
					suitLocker = pooledList[i];
					break;
				}
				if (suitLocker == null || suitScore > suitLocker.GetSuitScore())
				{
					suitLocker = pooledList[i];
				}
			}
			pooledList.Recycle();
			if (suitLocker != null)
			{
				Equipment equipment = this.reactor.GetComponent<MinionIdentity>().GetEquipment();
				suitLocker.EquipTo(equipment);
			}
		}
	}

	// Token: 0x02001302 RID: 4866
	private class UnequipSuitReactable : SuitMarker.SuitMarkerReactable
	{
		// Token: 0x06007C2E RID: 31790 RVA: 0x002CF470 File Offset: 0x002CD670
		public UnequipSuitReactable(SuitMarker marker)
			: base("UnequipSuitReactable", marker)
		{
		}

		// Token: 0x06007C2F RID: 31791 RVA: 0x002CF483 File Offset: 0x002CD683
		public override bool InternalCanBegin(GameObject newReactor, Navigator.ActiveTransition transition)
		{
			return newReactor.GetComponent<MinionIdentity>().GetEquipment().IsSlotOccupied(Db.Get().AssignableSlots.Suit) && base.InternalCanBegin(newReactor, transition);
		}

		// Token: 0x06007C30 RID: 31792 RVA: 0x002CF4B3 File Offset: 0x002CD6B3
		protected override void InternalBegin()
		{
			base.InternalBegin();
			this.suitMarker.CreateNewUnequipReactable();
		}

		// Token: 0x06007C31 RID: 31793 RVA: 0x002CF4C8 File Offset: 0x002CD6C8
		protected override bool MovingTheRightWay(GameObject newReactor, Navigator.ActiveTransition transition)
		{
			bool flag = transition.navGridTransition.x < 0;
			return transition.navGridTransition.x != 0 && flag != this.suitMarker.isRotated;
		}

		// Token: 0x06007C32 RID: 31794 RVA: 0x002CF504 File Offset: 0x002CD704
		protected override void Run()
		{
			Navigator component = this.reactor.GetComponent<Navigator>();
			Equipment equipment = this.reactor.GetComponent<MinionIdentity>().GetEquipment();
			if (component != null && (component.flags & this.suitMarker.PathFlag) > PathFinder.PotentialPath.Flags.None)
			{
				ListPool<SuitLocker, SuitMarker>.PooledList pooledList = ListPool<SuitLocker, SuitMarker>.Allocate();
				this.suitMarker.GetAttachedLockers(pooledList);
				SuitLocker suitLocker = null;
				int num = 0;
				while (suitLocker == null && num < pooledList.Count)
				{
					if (pooledList[num].CanDropOffSuit())
					{
						suitLocker = pooledList[num];
					}
					num++;
				}
				pooledList.Recycle();
				if (suitLocker != null)
				{
					suitLocker.UnequipFrom(equipment);
					return;
				}
			}
			Assignable assignable = equipment.GetAssignable(Db.Get().AssignableSlots.Suit);
			assignable.Unassign();
			Notification notification = new Notification(MISC.NOTIFICATIONS.SUIT_DROPPED.NAME, NotificationType.BadMinor, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.SUIT_DROPPED.TOOLTIP, null, true, 0f, null, null, null, true, false, false);
			assignable.GetComponent<Notifier>().Add(notification, "");
		}
	}

	// Token: 0x02001303 RID: 4867
	private abstract class SuitMarkerReactable : Reactable
	{
		// Token: 0x06007C33 RID: 31795 RVA: 0x002CF620 File Offset: 0x002CD820
		public SuitMarkerReactable(HashedString id, SuitMarker suit_marker)
			: base(suit_marker.gameObject, id, Db.Get().ChoreTypes.SuitMarker, 1, 1, false, 0f, 0f, float.PositiveInfinity, 0f, ObjectLayer.NumLayers)
		{
			this.suitMarker = suit_marker;
		}

		// Token: 0x06007C34 RID: 31796 RVA: 0x002CF669 File Offset: 0x002CD869
		public override bool InternalCanBegin(GameObject new_reactor, Navigator.ActiveTransition transition)
		{
			if (this.reactor != null)
			{
				return false;
			}
			if (this.suitMarker == null)
			{
				base.Cleanup();
				return false;
			}
			return this.suitMarker.isOperational && this.MovingTheRightWay(new_reactor, transition);
		}

		// Token: 0x06007C35 RID: 31797 RVA: 0x002CF6A8 File Offset: 0x002CD8A8
		protected override void InternalBegin()
		{
			this.startTime = Time.time;
			KBatchedAnimController component = this.reactor.GetComponent<KBatchedAnimController>();
			component.AddAnimOverrides(this.suitMarker.interactAnim, 1f);
			component.Play("working_pre", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue("working_loop", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue("working_pst", KAnim.PlayMode.Once, 1f, 0f);
			if (this.suitMarker.HasTag(GameTags.JetSuitBlocker))
			{
				KBatchedAnimController component2 = this.suitMarker.GetComponent<KBatchedAnimController>();
				component2.Play("working_pre", KAnim.PlayMode.Once, 1f, 0f);
				component2.Queue("working_loop", KAnim.PlayMode.Once, 1f, 0f);
				component2.Queue("working_pst", KAnim.PlayMode.Once, 1f, 0f);
			}
		}

		// Token: 0x06007C36 RID: 31798 RVA: 0x002CF7A0 File Offset: 0x002CD9A0
		public override void Update(float dt)
		{
			Facing facing = (this.reactor ? this.reactor.GetComponent<Facing>() : null);
			if (facing && this.suitMarker)
			{
				facing.SetFacing(this.suitMarker.GetComponent<Rotatable>().GetOrientation() == Orientation.FlipH);
			}
			if (Time.time - this.startTime > 2.8f)
			{
				if (this.reactor != null && this.suitMarker != null)
				{
					this.reactor.GetComponent<KBatchedAnimController>().RemoveAnimOverrides(this.suitMarker.interactAnim);
					this.Run();
				}
				base.Cleanup();
			}
		}

		// Token: 0x06007C37 RID: 31799 RVA: 0x002CF84D File Offset: 0x002CDA4D
		protected override void InternalEnd()
		{
			if (this.reactor != null)
			{
				this.reactor.GetComponent<KBatchedAnimController>().RemoveAnimOverrides(this.suitMarker.interactAnim);
			}
		}

		// Token: 0x06007C38 RID: 31800 RVA: 0x002CF878 File Offset: 0x002CDA78
		protected override void InternalCleanup()
		{
		}

		// Token: 0x06007C39 RID: 31801
		protected abstract bool MovingTheRightWay(GameObject reactor, Navigator.ActiveTransition transition);

		// Token: 0x06007C3A RID: 31802
		protected abstract void Run();

		// Token: 0x04005F43 RID: 24387
		protected SuitMarker suitMarker;

		// Token: 0x04005F44 RID: 24388
		protected float startTime;
	}
}
