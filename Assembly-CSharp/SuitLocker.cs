using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200064F RID: 1615
public class SuitLocker : StateMachineComponent<SuitLocker.StatesInstance>
{
	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x06002B01 RID: 11009 RVA: 0x000E2C60 File Offset: 0x000E0E60
	public float OxygenAvailable
	{
		get
		{
			KPrefabID storedOutfit = this.GetStoredOutfit();
			if (storedOutfit == null)
			{
				return 0f;
			}
			return storedOutfit.GetComponent<SuitTank>().PercentFull();
		}
	}

	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x06002B02 RID: 11010 RVA: 0x000E2C90 File Offset: 0x000E0E90
	public float BatteryAvailable
	{
		get
		{
			KPrefabID storedOutfit = this.GetStoredOutfit();
			if (storedOutfit == null)
			{
				return 0f;
			}
			return storedOutfit.GetComponent<LeadSuitTank>().batteryCharge;
		}
	}

	// Token: 0x06002B03 RID: 11011 RVA: 0x000E2CC0 File Offset: 0x000E0EC0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_arrow", "meter_scale" });
		SuitLocker.UpdateSuitMarkerStates(Grid.PosToCell(base.transform.position), base.gameObject);
		base.smi.StartSM();
		Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Suits, true);
	}

	// Token: 0x06002B04 RID: 11012 RVA: 0x000E2D44 File Offset: 0x000E0F44
	public KPrefabID GetStoredOutfit()
	{
		foreach (GameObject gameObject in base.GetComponent<Storage>().items)
		{
			if (!(gameObject == null))
			{
				KPrefabID component = gameObject.GetComponent<KPrefabID>();
				if (!(component == null) && component.IsAnyPrefabID(this.OutfitTags))
				{
					return component;
				}
			}
		}
		return null;
	}

	// Token: 0x06002B05 RID: 11013 RVA: 0x000E2DC4 File Offset: 0x000E0FC4
	public float GetSuitScore()
	{
		float num = -1f;
		KPrefabID partiallyChargedOutfit = this.GetPartiallyChargedOutfit();
		if (partiallyChargedOutfit)
		{
			num = partiallyChargedOutfit.GetComponent<SuitTank>().PercentFull();
			JetSuitTank component = partiallyChargedOutfit.GetComponent<JetSuitTank>();
			if (component && component.PercentFull() < num)
			{
				num = component.PercentFull();
			}
		}
		return num;
	}

	// Token: 0x06002B06 RID: 11014 RVA: 0x000E2E14 File Offset: 0x000E1014
	public KPrefabID GetPartiallyChargedOutfit()
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (!storedOutfit)
		{
			return null;
		}
		if (storedOutfit.GetComponent<SuitTank>().PercentFull() < TUNING.EQUIPMENT.SUITS.MINIMUM_USABLE_SUIT_CHARGE)
		{
			return null;
		}
		JetSuitTank component = storedOutfit.GetComponent<JetSuitTank>();
		if (component && component.PercentFull() < TUNING.EQUIPMENT.SUITS.MINIMUM_USABLE_SUIT_CHARGE)
		{
			return null;
		}
		return storedOutfit;
	}

	// Token: 0x06002B07 RID: 11015 RVA: 0x000E2E68 File Offset: 0x000E1068
	public KPrefabID GetFullyChargedOutfit()
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (!storedOutfit)
		{
			return null;
		}
		if (!storedOutfit.GetComponent<SuitTank>().IsFull())
		{
			return null;
		}
		JetSuitTank component = storedOutfit.GetComponent<JetSuitTank>();
		if (component && !component.IsFull())
		{
			return null;
		}
		return storedOutfit;
	}

	// Token: 0x06002B08 RID: 11016 RVA: 0x000E2EB0 File Offset: 0x000E10B0
	private void CreateFetchChore()
	{
		this.fetchChore = new FetchChore(Db.Get().ChoreTypes.EquipmentFetch, base.GetComponent<Storage>(), 1f, new HashSet<Tag>(this.OutfitTags), FetchChore.MatchCriteria.MatchID, Tag.Invalid, new Tag[] { GameTags.Assigned }, null, true, null, null, null, Operational.State.None, 0);
		this.fetchChore.allowMultifetch = false;
	}

	// Token: 0x06002B09 RID: 11017 RVA: 0x000E2F18 File Offset: 0x000E1118
	private void CancelFetchChore()
	{
		if (this.fetchChore != null)
		{
			this.fetchChore.Cancel("SuitLocker.CancelFetchChore");
			this.fetchChore = null;
		}
	}

	// Token: 0x06002B0A RID: 11018 RVA: 0x000E2F3C File Offset: 0x000E113C
	public bool HasOxygen()
	{
		GameObject oxygen = this.GetOxygen();
		return oxygen != null && oxygen.GetComponent<PrimaryElement>().Mass > 0f;
	}

	// Token: 0x06002B0B RID: 11019 RVA: 0x000E2F70 File Offset: 0x000E1170
	private void RefreshMeter()
	{
		GameObject oxygen = this.GetOxygen();
		float num = 0f;
		if (oxygen != null)
		{
			num = oxygen.GetComponent<PrimaryElement>().Mass / base.GetComponent<ConduitConsumer>().capacityKG;
			num = Math.Min(num, 1f);
		}
		this.meter.SetPositionPercent(num);
	}

	// Token: 0x06002B0C RID: 11020 RVA: 0x000E2FC4 File Offset: 0x000E11C4
	public bool IsSuitFullyCharged()
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (!(storedOutfit != null))
		{
			return false;
		}
		SuitTank component = storedOutfit.GetComponent<SuitTank>();
		if (component != null && component.PercentFull() < 1f)
		{
			return false;
		}
		JetSuitTank component2 = storedOutfit.GetComponent<JetSuitTank>();
		if (component2 != null && component2.PercentFull() < 1f)
		{
			return false;
		}
		LeadSuitTank leadSuitTank = ((storedOutfit != null) ? storedOutfit.GetComponent<LeadSuitTank>() : null);
		return !(leadSuitTank != null) || leadSuitTank.PercentFull() >= 1f;
	}

	// Token: 0x06002B0D RID: 11021 RVA: 0x000E3050 File Offset: 0x000E1250
	public bool IsOxygenTankFull()
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (storedOutfit != null)
		{
			SuitTank component = storedOutfit.GetComponent<SuitTank>();
			return component == null || component.PercentFull() >= 1f;
		}
		return false;
	}

	// Token: 0x06002B0E RID: 11022 RVA: 0x000E3091 File Offset: 0x000E1291
	private void OnRequestOutfit()
	{
		base.smi.sm.isWaitingForSuit.Set(true, base.smi, false);
	}

	// Token: 0x06002B0F RID: 11023 RVA: 0x000E30B1 File Offset: 0x000E12B1
	private void OnCancelRequest()
	{
		base.smi.sm.isWaitingForSuit.Set(false, base.smi, false);
	}

	// Token: 0x06002B10 RID: 11024 RVA: 0x000E30D4 File Offset: 0x000E12D4
	public void DropSuit()
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (storedOutfit == null)
		{
			return;
		}
		base.GetComponent<Storage>().Drop(storedOutfit.gameObject, true);
	}

	// Token: 0x06002B11 RID: 11025 RVA: 0x000E3108 File Offset: 0x000E1308
	public void EquipTo(Equipment equipment)
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (storedOutfit == null)
		{
			return;
		}
		base.GetComponent<Storage>().Drop(storedOutfit.gameObject, true);
		Prioritizable component = storedOutfit.GetComponent<Prioritizable>();
		PrioritySetting masterPriority = component.GetMasterPriority();
		PrioritySetting prioritySetting = new PrioritySetting(PriorityScreen.PriorityClass.basic, 5);
		if (component != null && component.GetMasterPriority().priority_class == PriorityScreen.PriorityClass.topPriority)
		{
			component.SetMasterPriority(prioritySetting);
		}
		storedOutfit.GetComponent<Equippable>().Assign(equipment.GetComponent<IAssignableIdentity>());
		storedOutfit.GetComponent<EquippableWorkable>().CancelChore("Manual equip");
		if (component != null && component.GetMasterPriority() != masterPriority)
		{
			component.SetMasterPriority(masterPriority);
		}
		equipment.Equip(storedOutfit.GetComponent<Equippable>());
		this.returnSuitWorkable.CreateChore();
	}

	// Token: 0x06002B12 RID: 11026 RVA: 0x000E31C4 File Offset: 0x000E13C4
	public void UnequipFrom(Equipment equipment)
	{
		Assignable assignable = equipment.GetAssignable(Db.Get().AssignableSlots.Suit);
		assignable.Unassign();
		base.GetComponent<Storage>().Store(assignable.gameObject, false, false, true, false);
		Durability component = assignable.GetComponent<Durability>();
		if (component != null && component.IsWornOut())
		{
			this.ConfigRequestSuit();
		}
	}

	// Token: 0x06002B13 RID: 11027 RVA: 0x000E3221 File Offset: 0x000E1421
	public void ConfigRequestSuit()
	{
		base.smi.sm.isConfigured.Set(true, base.smi, false);
		base.smi.sm.isWaitingForSuit.Set(true, base.smi, false);
	}

	// Token: 0x06002B14 RID: 11028 RVA: 0x000E325F File Offset: 0x000E145F
	public void ConfigNoSuit()
	{
		base.smi.sm.isConfigured.Set(true, base.smi, false);
		base.smi.sm.isWaitingForSuit.Set(false, base.smi, false);
	}

	// Token: 0x06002B15 RID: 11029 RVA: 0x000E32A0 File Offset: 0x000E14A0
	public bool CanDropOffSuit()
	{
		return base.smi.sm.isConfigured.Get(base.smi) && !base.smi.sm.isWaitingForSuit.Get(base.smi) && this.GetStoredOutfit() == null;
	}

	// Token: 0x06002B16 RID: 11030 RVA: 0x000E32F5 File Offset: 0x000E14F5
	private GameObject GetOxygen()
	{
		return base.GetComponent<Storage>().FindFirst(GameTags.Oxygen);
	}

	// Token: 0x06002B17 RID: 11031 RVA: 0x000E3308 File Offset: 0x000E1508
	private void ChargeSuit(float dt)
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (storedOutfit == null)
		{
			return;
		}
		GameObject oxygen = this.GetOxygen();
		if (oxygen == null)
		{
			return;
		}
		SuitTank component = storedOutfit.GetComponent<SuitTank>();
		float num = component.capacity * 15f * dt / 600f;
		num = Mathf.Min(num, component.capacity - component.GetTankAmount());
		num = Mathf.Min(oxygen.GetComponent<PrimaryElement>().Mass, num);
		if (num > 0f)
		{
			base.GetComponent<Storage>().Transfer(component.storage, component.elementTag, num, false, true);
		}
	}

	// Token: 0x06002B18 RID: 11032 RVA: 0x000E339C File Offset: 0x000E159C
	public void SetSuitMarker(SuitMarker suit_marker)
	{
		SuitLocker.SuitMarkerState suitMarkerState = SuitLocker.SuitMarkerState.HasMarker;
		if (suit_marker == null)
		{
			suitMarkerState = SuitLocker.SuitMarkerState.NoMarker;
		}
		else if (suit_marker.transform.GetPosition().x > base.transform.GetPosition().x && suit_marker.GetComponent<Rotatable>().IsRotated)
		{
			suitMarkerState = SuitLocker.SuitMarkerState.WrongSide;
		}
		else if (suit_marker.transform.GetPosition().x < base.transform.GetPosition().x && !suit_marker.GetComponent<Rotatable>().IsRotated)
		{
			suitMarkerState = SuitLocker.SuitMarkerState.WrongSide;
		}
		else if (!suit_marker.GetComponent<Operational>().IsOperational)
		{
			suitMarkerState = SuitLocker.SuitMarkerState.NotOperational;
		}
		if (suitMarkerState != this.suitMarkerState)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoSuitMarker, false);
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.SuitMarkerWrongSide, false);
			switch (suitMarkerState)
			{
			case SuitLocker.SuitMarkerState.NoMarker:
				base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoSuitMarker, null);
				break;
			case SuitLocker.SuitMarkerState.WrongSide:
				base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.SuitMarkerWrongSide, null);
				break;
			}
			this.suitMarkerState = suitMarkerState;
		}
	}

	// Token: 0x06002B19 RID: 11033 RVA: 0x000E34C6 File Offset: 0x000E16C6
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		SuitLocker.UpdateSuitMarkerStates(Grid.PosToCell(base.transform.position), null);
	}

	// Token: 0x06002B1A RID: 11034 RVA: 0x000E34E4 File Offset: 0x000E16E4
	private static void GatherSuitBuildings(int cell, int dir, List<SuitLocker.SuitLockerEntry> suit_lockers, List<SuitLocker.SuitMarkerEntry> suit_markers)
	{
		int num = dir;
		for (;;)
		{
			int num2 = Grid.OffsetCell(cell, num, 0);
			if (Grid.IsValidCell(num2) && !SuitLocker.GatherSuitBuildingsOnCell(num2, suit_lockers, suit_markers))
			{
				break;
			}
			num += dir;
		}
	}

	// Token: 0x06002B1B RID: 11035 RVA: 0x000E3514 File Offset: 0x000E1714
	private static bool GatherSuitBuildingsOnCell(int cell, List<SuitLocker.SuitLockerEntry> suit_lockers, List<SuitLocker.SuitMarkerEntry> suit_markers)
	{
		GameObject gameObject = Grid.Objects[cell, 1];
		if (gameObject == null)
		{
			return false;
		}
		SuitMarker component = gameObject.GetComponent<SuitMarker>();
		if (component != null)
		{
			suit_markers.Add(new SuitLocker.SuitMarkerEntry
			{
				suitMarker = component,
				cell = cell
			});
			return true;
		}
		SuitLocker component2 = gameObject.GetComponent<SuitLocker>();
		if (component2 != null)
		{
			suit_lockers.Add(new SuitLocker.SuitLockerEntry
			{
				suitLocker = component2,
				cell = cell
			});
			return true;
		}
		return false;
	}

	// Token: 0x06002B1C RID: 11036 RVA: 0x000E35A0 File Offset: 0x000E17A0
	private static SuitMarker FindSuitMarker(int cell, List<SuitLocker.SuitMarkerEntry> suit_markers)
	{
		if (!Grid.IsValidCell(cell))
		{
			return null;
		}
		foreach (SuitLocker.SuitMarkerEntry suitMarkerEntry in suit_markers)
		{
			if (suitMarkerEntry.cell == cell)
			{
				return suitMarkerEntry.suitMarker;
			}
		}
		return null;
	}

	// Token: 0x06002B1D RID: 11037 RVA: 0x000E3608 File Offset: 0x000E1808
	public static void UpdateSuitMarkerStates(int cell, GameObject self)
	{
		ListPool<SuitLocker.SuitLockerEntry, SuitLocker>.PooledList pooledList = ListPool<SuitLocker.SuitLockerEntry, SuitLocker>.Allocate();
		ListPool<SuitLocker.SuitMarkerEntry, SuitLocker>.PooledList pooledList2 = ListPool<SuitLocker.SuitMarkerEntry, SuitLocker>.Allocate();
		if (self != null)
		{
			SuitLocker component = self.GetComponent<SuitLocker>();
			if (component != null)
			{
				pooledList.Add(new SuitLocker.SuitLockerEntry
				{
					suitLocker = component,
					cell = cell
				});
			}
			SuitMarker component2 = self.GetComponent<SuitMarker>();
			if (component2 != null)
			{
				pooledList2.Add(new SuitLocker.SuitMarkerEntry
				{
					suitMarker = component2,
					cell = cell
				});
			}
		}
		SuitLocker.GatherSuitBuildings(cell, 1, pooledList, pooledList2);
		SuitLocker.GatherSuitBuildings(cell, -1, pooledList, pooledList2);
		pooledList.Sort(SuitLocker.SuitLockerEntry.comparer);
		for (int i = 0; i < pooledList.Count; i++)
		{
			SuitLocker.SuitLockerEntry suitLockerEntry = pooledList[i];
			SuitLocker.SuitLockerEntry suitLockerEntry2 = suitLockerEntry;
			ListPool<SuitLocker.SuitLockerEntry, SuitLocker>.PooledList pooledList3 = ListPool<SuitLocker.SuitLockerEntry, SuitLocker>.Allocate();
			pooledList3.Add(suitLockerEntry);
			for (int j = i + 1; j < pooledList.Count; j++)
			{
				SuitLocker.SuitLockerEntry suitLockerEntry3 = pooledList[j];
				if (Grid.CellRight(suitLockerEntry2.cell) != suitLockerEntry3.cell)
				{
					break;
				}
				i++;
				suitLockerEntry2 = suitLockerEntry3;
				pooledList3.Add(suitLockerEntry3);
			}
			int num = Grid.CellLeft(suitLockerEntry.cell);
			int num2 = Grid.CellRight(suitLockerEntry2.cell);
			SuitMarker suitMarker = SuitLocker.FindSuitMarker(num, pooledList2);
			if (suitMarker == null)
			{
				suitMarker = SuitLocker.FindSuitMarker(num2, pooledList2);
			}
			foreach (SuitLocker.SuitLockerEntry suitLockerEntry4 in pooledList3)
			{
				suitLockerEntry4.suitLocker.SetSuitMarker(suitMarker);
			}
			pooledList3.Recycle();
		}
		pooledList.Recycle();
		pooledList2.Recycle();
	}

	// Token: 0x0400198B RID: 6539
	[MyCmpGet]
	private Building building;

	// Token: 0x0400198C RID: 6540
	public Tag[] OutfitTags;

	// Token: 0x0400198D RID: 6541
	private FetchChore fetchChore;

	// Token: 0x0400198E RID: 6542
	[MyCmpAdd]
	public SuitLocker.ReturnSuitWorkable returnSuitWorkable;

	// Token: 0x0400198F RID: 6543
	private MeterController meter;

	// Token: 0x04001990 RID: 6544
	private SuitLocker.SuitMarkerState suitMarkerState;

	// Token: 0x020012FB RID: 4859
	[AddComponentMenu("KMonoBehaviour/Workable/ReturnSuitWorkable")]
	public class ReturnSuitWorkable : Workable
	{
		// Token: 0x06007C19 RID: 31769 RVA: 0x002CE7DF File Offset: 0x002CC9DF
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.resetProgressOnStop = true;
			this.workTime = 0.25f;
			this.synchronizeAnims = false;
		}

		// Token: 0x06007C1A RID: 31770 RVA: 0x002CE800 File Offset: 0x002CCA00
		public void CreateChore()
		{
			if (this.urgentChore == null)
			{
				SuitLocker component = base.GetComponent<SuitLocker>();
				this.urgentChore = new WorkChore<SuitLocker.ReturnSuitWorkable>(Db.Get().ChoreTypes.ReturnSuitUrgent, this, null, true, null, null, null, true, null, false, false, null, false, true, false, PriorityScreen.PriorityClass.personalNeeds, 5, false, false);
				this.urgentChore.AddPrecondition(SuitLocker.ReturnSuitWorkable.DoesSuitNeedRechargingUrgent, null);
				this.urgentChore.AddPrecondition(this.HasSuitMarker, component);
				this.urgentChore.AddPrecondition(this.SuitTypeMatchesLocker, component);
				this.idleChore = new WorkChore<SuitLocker.ReturnSuitWorkable>(Db.Get().ChoreTypes.ReturnSuitIdle, this, null, true, null, null, null, true, null, false, false, null, false, true, false, PriorityScreen.PriorityClass.idle, 5, false, false);
				this.idleChore.AddPrecondition(SuitLocker.ReturnSuitWorkable.DoesSuitNeedRechargingIdle, null);
				this.idleChore.AddPrecondition(this.HasSuitMarker, component);
				this.idleChore.AddPrecondition(this.SuitTypeMatchesLocker, component);
			}
		}

		// Token: 0x06007C1B RID: 31771 RVA: 0x002CE8E1 File Offset: 0x002CCAE1
		public void CancelChore()
		{
			if (this.urgentChore != null)
			{
				this.urgentChore.Cancel("ReturnSuitWorkable.CancelChore");
				this.urgentChore = null;
			}
			if (this.idleChore != null)
			{
				this.idleChore.Cancel("ReturnSuitWorkable.CancelChore");
				this.idleChore = null;
			}
		}

		// Token: 0x06007C1C RID: 31772 RVA: 0x002CE921 File Offset: 0x002CCB21
		protected override void OnStartWork(Worker worker)
		{
			base.ShowProgressBar(false);
		}

		// Token: 0x06007C1D RID: 31773 RVA: 0x002CE92A File Offset: 0x002CCB2A
		protected override bool OnWorkTick(Worker worker, float dt)
		{
			return true;
		}

		// Token: 0x06007C1E RID: 31774 RVA: 0x002CE930 File Offset: 0x002CCB30
		protected override void OnCompleteWork(Worker worker)
		{
			Equipment equipment = worker.GetComponent<MinionIdentity>().GetEquipment();
			if (equipment.IsSlotOccupied(Db.Get().AssignableSlots.Suit))
			{
				if (base.GetComponent<SuitLocker>().CanDropOffSuit())
				{
					base.GetComponent<SuitLocker>().UnequipFrom(equipment);
				}
				else
				{
					equipment.GetAssignable(Db.Get().AssignableSlots.Suit).Unassign();
				}
			}
			if (this.urgentChore != null)
			{
				this.CancelChore();
				this.CreateChore();
			}
		}

		// Token: 0x06007C1F RID: 31775 RVA: 0x002CE9A9 File Offset: 0x002CCBA9
		public override HashedString[] GetWorkAnims(Worker worker)
		{
			return new HashedString[]
			{
				new HashedString("none")
			};
		}

		// Token: 0x06007C20 RID: 31776 RVA: 0x002CE9C4 File Offset: 0x002CCBC4
		public ReturnSuitWorkable()
		{
			Chore.Precondition precondition = default(Chore.Precondition);
			precondition.id = "IsValid";
			precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.HAS_SUIT_MARKER;
			precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				return ((SuitLocker)data).suitMarkerState == SuitLocker.SuitMarkerState.HasMarker;
			};
			this.HasSuitMarker = precondition;
			precondition = default(Chore.Precondition);
			precondition.id = "IsValid";
			precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.HAS_SUIT_MARKER;
			precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				SuitLocker suitLocker = (SuitLocker)data;
				Equipment equipment = context.consumerState.equipment;
				if (equipment == null)
				{
					return false;
				}
				AssignableSlotInstance slot = equipment.GetSlot(Db.Get().AssignableSlots.Suit);
				return !(slot.assignable == null) && slot.assignable.GetComponent<KPrefabID>().IsAnyPrefabID(suitLocker.OutfitTags);
			};
			this.SuitTypeMatchesLocker = precondition;
			base..ctor();
		}

		// Token: 0x04005F2C RID: 24364
		public static readonly Chore.Precondition DoesSuitNeedRechargingUrgent = new Chore.Precondition
		{
			id = "DoesSuitNeedRechargingUrgent",
			description = DUPLICANTS.CHORES.PRECONDITIONS.DOES_SUIT_NEED_RECHARGING_URGENT,
			fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				Equipment equipment = context.consumerState.equipment;
				if (equipment == null)
				{
					return false;
				}
				AssignableSlotInstance slot = equipment.GetSlot(Db.Get().AssignableSlots.Suit);
				if (slot.assignable == null)
				{
					return false;
				}
				Equippable component = slot.assignable.GetComponent<Equippable>();
				if (component == null || !component.isEquipped)
				{
					return false;
				}
				SuitTank component2 = slot.assignable.GetComponent<SuitTank>();
				if (component2 != null && component2.NeedsRecharging())
				{
					return true;
				}
				JetSuitTank component3 = slot.assignable.GetComponent<JetSuitTank>();
				if (component3 != null && component3.NeedsRecharging())
				{
					return true;
				}
				LeadSuitTank component4 = slot.assignable.GetComponent<LeadSuitTank>();
				return component4 != null && component4.NeedsRecharging();
			}
		};

		// Token: 0x04005F2D RID: 24365
		public static readonly Chore.Precondition DoesSuitNeedRechargingIdle = new Chore.Precondition
		{
			id = "DoesSuitNeedRechargingIdle",
			description = DUPLICANTS.CHORES.PRECONDITIONS.DOES_SUIT_NEED_RECHARGING_IDLE,
			fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				Equipment equipment2 = context.consumerState.equipment;
				if (equipment2 == null)
				{
					return false;
				}
				AssignableSlotInstance slot2 = equipment2.GetSlot(Db.Get().AssignableSlots.Suit);
				if (slot2.assignable == null)
				{
					return false;
				}
				Equippable component5 = slot2.assignable.GetComponent<Equippable>();
				return !(component5 == null) && component5.isEquipped && (slot2.assignable.GetComponent<SuitTank>() != null || slot2.assignable.GetComponent<JetSuitTank>() != null || slot2.assignable.GetComponent<LeadSuitTank>() != null);
			}
		};

		// Token: 0x04005F2E RID: 24366
		public Chore.Precondition HasSuitMarker;

		// Token: 0x04005F2F RID: 24367
		public Chore.Precondition SuitTypeMatchesLocker;

		// Token: 0x04005F30 RID: 24368
		private WorkChore<SuitLocker.ReturnSuitWorkable> urgentChore;

		// Token: 0x04005F31 RID: 24369
		private WorkChore<SuitLocker.ReturnSuitWorkable> idleChore;
	}

	// Token: 0x020012FC RID: 4860
	public class StatesInstance : GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.GameInstance
	{
		// Token: 0x06007C22 RID: 31778 RVA: 0x002CEB0D File Offset: 0x002CCD0D
		public StatesInstance(SuitLocker suit_locker)
			: base(suit_locker)
		{
		}
	}

	// Token: 0x020012FD RID: 4861
	public class States : GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker>
	{
		// Token: 0x06007C23 RID: 31779 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.empty;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.Update("RefreshMeter", delegate(SuitLocker.StatesInstance smi, float dt)
			{
				smi.master.RefreshMeter();
			}, UpdateRate.RENDER_200ms, false);
			this.empty.DefaultState(this.empty.notconfigured).EventTransition(GameHashes.OnStorageChange, this.charging, (SuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit() != null).ParamTransition<bool>(this.isWaitingForSuit, this.waitingforsuit, GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.IsTrue)
				.Enter("CreateReturnSuitChore", delegate(SuitLocker.StatesInstance smi)
				{
					smi.master.returnSuitWorkable.CreateChore();
				})
				.RefreshUserMenuOnEnter()
				.Exit("CancelReturnSuitChore", delegate(SuitLocker.StatesInstance smi)
				{
					smi.master.returnSuitWorkable.CancelChore();
				})
				.PlayAnim("no_suit_pre")
				.QueueAnim("no_suit", false, null);
			this.empty.notconfigured.ParamTransition<bool>(this.isConfigured, this.empty.configured, GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.IsTrue).ToggleStatusItem(BUILDING.STATUSITEMS.SUIT_LOCKER_NEEDS_CONFIGURATION.NAME, BUILDING.STATUSITEMS.SUIT_LOCKER_NEEDS_CONFIGURATION.TOOLTIP, "status_item_no_filter_set", StatusItem.IconType.Custom, NotificationType.BadMinor, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
			this.empty.configured.RefreshUserMenuOnEnter().ToggleStatusItem(BUILDING.STATUSITEMS.SUIT_LOCKER.READY.NAME, BUILDING.STATUSITEMS.SUIT_LOCKER.READY.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
			this.waitingforsuit.EventTransition(GameHashes.OnStorageChange, this.charging, (SuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit() != null).Enter("CreateFetchChore", delegate(SuitLocker.StatesInstance smi)
			{
				smi.master.CreateFetchChore();
			}).ParamTransition<bool>(this.isWaitingForSuit, this.empty, GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.IsFalse)
				.RefreshUserMenuOnEnter()
				.PlayAnim("no_suit_pst")
				.QueueAnim("awaiting_suit", false, null)
				.Exit("ClearIsWaitingForSuit", delegate(SuitLocker.StatesInstance smi)
				{
					this.isWaitingForSuit.Set(false, smi, false);
				})
				.Exit("CancelFetchChore", delegate(SuitLocker.StatesInstance smi)
				{
					smi.master.CancelFetchChore();
				})
				.ToggleStatusItem(BUILDING.STATUSITEMS.SUIT_LOCKER.SUIT_REQUESTED.NAME, BUILDING.STATUSITEMS.SUIT_LOCKER.SUIT_REQUESTED.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
			this.charging.DefaultState(this.charging.pre).RefreshUserMenuOnEnter().EventTransition(GameHashes.OnStorageChange, this.empty, (SuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit() == null)
				.ToggleStatusItem(Db.Get().MiscStatusItems.StoredItemDurability, (SuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit().gameObject)
				.Enter(delegate(SuitLocker.StatesInstance smi)
				{
					KAnim.Build.Symbol symbol = smi.master.GetStoredOutfit().GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build.GetSymbol("suit");
					SymbolOverrideController component = smi.GetComponent<SymbolOverrideController>();
					component.TryRemoveSymbolOverride("suit_swap", 0);
					if (symbol != null)
					{
						component.AddSymbolOverride("suit_swap", symbol, 0);
					}
				});
			this.charging.pre.Enter(delegate(SuitLocker.StatesInstance smi)
			{
				if (smi.master.IsSuitFullyCharged())
				{
					smi.GoTo(this.suitfullycharged);
					return;
				}
				smi.GetComponent<KBatchedAnimController>().Play("no_suit_pst", KAnim.PlayMode.Once, 1f, 0f);
				smi.GetComponent<KBatchedAnimController>().Queue("charging_pre", KAnim.PlayMode.Once, 1f, 0f);
			}).OnAnimQueueComplete(this.charging.operational);
			this.charging.operational.TagTransition(GameTags.Operational, this.charging.notoperational, true).Transition(this.charging.nooxygen, (SuitLocker.StatesInstance smi) => !smi.master.HasOxygen(), UpdateRate.SIM_200ms).PlayAnim("charging_loop", KAnim.PlayMode.Loop)
				.Enter("SetActive", delegate(SuitLocker.StatesInstance smi)
				{
					smi.master.GetComponent<Operational>().SetActive(true, false);
				})
				.Transition(this.charging.pst, (SuitLocker.StatesInstance smi) => smi.master.IsSuitFullyCharged(), UpdateRate.SIM_200ms)
				.Update("ChargeSuit", delegate(SuitLocker.StatesInstance smi, float dt)
				{
					smi.master.ChargeSuit(dt);
				}, UpdateRate.SIM_200ms, false)
				.Exit("ClearActive", delegate(SuitLocker.StatesInstance smi)
				{
					smi.master.GetComponent<Operational>().SetActive(false, false);
				})
				.ToggleStatusItem(BUILDING.STATUSITEMS.SUIT_LOCKER.CHARGING.NAME, BUILDING.STATUSITEMS.SUIT_LOCKER.CHARGING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
			this.charging.nooxygen.TagTransition(GameTags.Operational, this.charging.notoperational, true).Transition(this.charging.operational, (SuitLocker.StatesInstance smi) => smi.master.HasOxygen(), UpdateRate.SIM_200ms).Transition(this.charging.pst, (SuitLocker.StatesInstance smi) => smi.master.IsSuitFullyCharged(), UpdateRate.SIM_200ms)
				.PlayAnim("no_o2_loop", KAnim.PlayMode.Loop)
				.ToggleStatusItem(BUILDING.STATUSITEMS.SUIT_LOCKER.NO_OXYGEN.NAME, BUILDING.STATUSITEMS.SUIT_LOCKER.NO_OXYGEN.TOOLTIP, "status_item_suit_locker_no_oxygen", StatusItem.IconType.Custom, NotificationType.BadMinor, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
			this.charging.notoperational.TagTransition(GameTags.Operational, this.charging.operational, false).PlayAnim("not_charging_loop", KAnim.PlayMode.Loop).Transition(this.charging.pst, (SuitLocker.StatesInstance smi) => smi.master.IsSuitFullyCharged(), UpdateRate.SIM_200ms)
				.ToggleStatusItem(BUILDING.STATUSITEMS.SUIT_LOCKER.NOT_OPERATIONAL.NAME, BUILDING.STATUSITEMS.SUIT_LOCKER.NOT_OPERATIONAL.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
			this.charging.pst.PlayAnim("charging_pst").OnAnimQueueComplete(this.suitfullycharged);
			this.suitfullycharged.EventTransition(GameHashes.OnStorageChange, this.empty, (SuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit() == null).PlayAnim("has_suit").RefreshUserMenuOnEnter()
				.ToggleStatusItem(Db.Get().MiscStatusItems.StoredItemDurability, (SuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit().gameObject)
				.ToggleStatusItem(BUILDING.STATUSITEMS.SUIT_LOCKER.FULLY_CHARGED.NAME, BUILDING.STATUSITEMS.SUIT_LOCKER.FULLY_CHARGED.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		}

		// Token: 0x04005F32 RID: 24370
		public SuitLocker.States.EmptyStates empty;

		// Token: 0x04005F33 RID: 24371
		public SuitLocker.States.ChargingStates charging;

		// Token: 0x04005F34 RID: 24372
		public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State waitingforsuit;

		// Token: 0x04005F35 RID: 24373
		public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State suitfullycharged;

		// Token: 0x04005F36 RID: 24374
		public StateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.BoolParameter isWaitingForSuit;

		// Token: 0x04005F37 RID: 24375
		public StateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.BoolParameter isConfigured;

		// Token: 0x04005F38 RID: 24376
		public StateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.BoolParameter hasSuitMarker;

		// Token: 0x02002006 RID: 8198
		public class ChargingStates : GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State
		{
			// Token: 0x04008ECA RID: 36554
			public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State pre;

			// Token: 0x04008ECB RID: 36555
			public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State pst;

			// Token: 0x04008ECC RID: 36556
			public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State operational;

			// Token: 0x04008ECD RID: 36557
			public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State nooxygen;

			// Token: 0x04008ECE RID: 36558
			public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State notoperational;
		}

		// Token: 0x02002007 RID: 8199
		public class EmptyStates : GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State
		{
			// Token: 0x04008ECF RID: 36559
			public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State configured;

			// Token: 0x04008ED0 RID: 36560
			public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State notconfigured;
		}
	}

	// Token: 0x020012FE RID: 4862
	private enum SuitMarkerState
	{
		// Token: 0x04005F3A RID: 24378
		HasMarker,
		// Token: 0x04005F3B RID: 24379
		NoMarker,
		// Token: 0x04005F3C RID: 24380
		WrongSide,
		// Token: 0x04005F3D RID: 24381
		NotOperational
	}

	// Token: 0x020012FF RID: 4863
	private struct SuitLockerEntry
	{
		// Token: 0x04005F3E RID: 24382
		public SuitLocker suitLocker;

		// Token: 0x04005F3F RID: 24383
		public int cell;

		// Token: 0x04005F40 RID: 24384
		public static SuitLocker.SuitLockerEntry.Comparer comparer = new SuitLocker.SuitLockerEntry.Comparer();

		// Token: 0x02002009 RID: 8201
		public class Comparer : IComparer<SuitLocker.SuitLockerEntry>
		{
			// Token: 0x0600A214 RID: 41492 RVA: 0x00343E54 File Offset: 0x00342054
			public int Compare(SuitLocker.SuitLockerEntry a, SuitLocker.SuitLockerEntry b)
			{
				return a.cell - b.cell;
			}
		}
	}

	// Token: 0x02001300 RID: 4864
	private struct SuitMarkerEntry
	{
		// Token: 0x04005F41 RID: 24385
		public SuitMarker suitMarker;

		// Token: 0x04005F42 RID: 24386
		public int cell;
	}
}
