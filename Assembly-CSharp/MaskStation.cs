using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000604 RID: 1540
public class MaskStation : StateMachineComponent<MaskStation.SMInstance>, IBasicBuilding
{
	// Token: 0x1700028D RID: 653
	// (get) Token: 0x06002815 RID: 10261 RVA: 0x000D5526 File Offset: 0x000D3726
	// (set) Token: 0x06002816 RID: 10262 RVA: 0x000D5533 File Offset: 0x000D3733
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

	// Token: 0x1700028E RID: 654
	// (get) Token: 0x06002817 RID: 10263 RVA: 0x000D553D File Offset: 0x000D373D
	// (set) Token: 0x06002818 RID: 10264 RVA: 0x000D554A File Offset: 0x000D374A
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

	// Token: 0x06002819 RID: 10265 RVA: 0x000D5554 File Offset: 0x000D3754
	public void UpdateOperational()
	{
		bool flag = this.GetTotalOxygenAmount() >= this.oxygenConsumedPerMask * (float)this.maxUses;
		this.shouldPump = this.IsPumpable();
		if (this.operational.IsOperational && this.shouldPump && !flag)
		{
			this.operational.SetActive(true, false);
		}
		else
		{
			this.operational.SetActive(false, false);
		}
		this.noElementStatusGuid = this.selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.InvalidMaskStationConsumptionState, this.noElementStatusGuid, !this.shouldPump, null);
	}

	// Token: 0x0600281A RID: 10266 RVA: 0x000D55EC File Offset: 0x000D37EC
	private bool IsPumpable()
	{
		ElementConsumer[] components = base.GetComponents<ElementConsumer>();
		int num = Grid.PosToCell(base.transform.GetPosition());
		bool flag = false;
		foreach (ElementConsumer elementConsumer in components)
		{
			for (int j = 0; j < (int)elementConsumer.consumptionRadius; j++)
			{
				for (int k = 0; k < (int)elementConsumer.consumptionRadius; k++)
				{
					int num2 = num + k + Grid.WidthInCells * j;
					bool flag2 = Grid.Element[num2].IsState(Element.State.Gas);
					bool flag3 = Grid.Element[num2].id == elementConsumer.elementToConsume;
					if (flag2 && flag3)
					{
						flag = true;
					}
				}
			}
		}
		return flag;
	}

	// Token: 0x0600281B RID: 10267 RVA: 0x000D5690 File Offset: 0x000D3890
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ChoreType choreType = Db.Get().ChoreTypes.Get(this.choreTypeID);
		this.filteredStorage = new FilteredStorage(this, null, null, false, choreType);
	}

	// Token: 0x0600281C RID: 10268 RVA: 0x000D56CC File Offset: 0x000D38CC
	private List<GameObject> GetPossibleMaterials()
	{
		List<GameObject> list = new List<GameObject>();
		this.materialStorage.Find(this.materialTag, list);
		return list;
	}

	// Token: 0x0600281D RID: 10269 RVA: 0x000D56F3 File Offset: 0x000D38F3
	private float GetTotalMaterialAmount()
	{
		return this.materialStorage.GetMassAvailable(this.materialTag);
	}

	// Token: 0x0600281E RID: 10270 RVA: 0x000D5706 File Offset: 0x000D3906
	private float GetTotalOxygenAmount()
	{
		return this.oxygenStorage.GetMassAvailable(this.oxygenTag);
	}

	// Token: 0x0600281F RID: 10271 RVA: 0x000D571C File Offset: 0x000D391C
	private void RefreshMeters()
	{
		float num = this.GetTotalMaterialAmount();
		num = Mathf.Clamp01(num / ((float)this.maxUses * this.materialConsumedPerMask));
		float num2 = this.GetTotalOxygenAmount();
		num2 = Mathf.Clamp01(num2 / ((float)this.maxUses * this.oxygenConsumedPerMask));
		this.materialsMeter.SetPositionPercent(num);
		this.oxygenMeter.SetPositionPercent(num2);
	}

	// Token: 0x06002820 RID: 10272 RVA: 0x000D577C File Offset: 0x000D397C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		this.CreateNewReactable();
		this.cell = Grid.PosToCell(this);
		Grid.RegisterSuitMarker(this.cell);
		this.isOperational = base.GetComponent<Operational>().IsOperational;
		base.Subscribe<MaskStation>(-592767678, MaskStation.OnOperationalChangedDelegate);
		this.isRotated = base.GetComponent<Rotatable>().IsRotated;
		base.Subscribe<MaskStation>(-1643076535, MaskStation.OnRotatedDelegate);
		this.materialsMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_resources_target", "meter_resources", this.materialsMeterOffset, Grid.SceneLayer.BuildingBack, new string[] { "meter_resources_target" });
		this.oxygenMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_oxygen_target", "meter_oxygen", this.oxygenMeterOffset, Grid.SceneLayer.BuildingFront, new string[] { "meter_oxygen_target" });
		if (this.filteredStorage != null)
		{
			this.filteredStorage.FilterChanged();
		}
		base.Subscribe<MaskStation>(-1697596308, MaskStation.OnStorageChangeDelegate);
		this.RefreshMeters();
	}

	// Token: 0x06002821 RID: 10273 RVA: 0x000D5888 File Offset: 0x000D3A88
	private void Update()
	{
		float num = this.GetTotalMaterialAmount() / this.materialConsumedPerMask;
		float num2 = this.GetTotalOxygenAmount() / this.oxygenConsumedPerMask;
		int num3 = (int)Mathf.Min(num, num2);
		int num4 = 0;
		Grid.UpdateSuitMarker(this.cell, num3, num4, this.gridFlags, this.PathFlag);
	}

	// Token: 0x06002822 RID: 10274 RVA: 0x000D58D4 File Offset: 0x000D3AD4
	protected override void OnCleanUp()
	{
		if (this.filteredStorage != null)
		{
			this.filteredStorage.CleanUp();
		}
		if (base.isSpawned)
		{
			Grid.UnregisterSuitMarker(this.cell);
		}
		if (this.reactable != null)
		{
			this.reactable.Cleanup();
		}
		base.OnCleanUp();
	}

	// Token: 0x06002823 RID: 10275 RVA: 0x000D5920 File Offset: 0x000D3B20
	private void OnOperationalChanged(bool isOperational)
	{
		this.isOperational = isOperational;
	}

	// Token: 0x06002824 RID: 10276 RVA: 0x000D5929 File Offset: 0x000D3B29
	private void OnStorageChange(object data)
	{
		this.RefreshMeters();
	}

	// Token: 0x06002825 RID: 10277 RVA: 0x000D5931 File Offset: 0x000D3B31
	private void UpdateGridFlag(Grid.SuitMarker.Flags flag, bool state)
	{
		if (state)
		{
			this.gridFlags |= flag;
			return;
		}
		this.gridFlags &= ~flag;
	}

	// Token: 0x06002826 RID: 10278 RVA: 0x000D5955 File Offset: 0x000D3B55
	private void CreateNewReactable()
	{
		this.reactable = new MaskStation.OxygenMaskReactable(this);
	}

	// Token: 0x0400178F RID: 6031
	private static readonly EventSystem.IntraObjectHandler<MaskStation> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<MaskStation>(delegate(MaskStation component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x04001790 RID: 6032
	private static readonly EventSystem.IntraObjectHandler<MaskStation> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<MaskStation>(delegate(MaskStation component, object data)
	{
		component.OnOperationalChanged((bool)data);
	});

	// Token: 0x04001791 RID: 6033
	private static readonly EventSystem.IntraObjectHandler<MaskStation> OnRotatedDelegate = new EventSystem.IntraObjectHandler<MaskStation>(delegate(MaskStation component, object data)
	{
		component.isRotated = ((Rotatable)data).IsRotated;
	});

	// Token: 0x04001792 RID: 6034
	public float materialConsumedPerMask = 1f;

	// Token: 0x04001793 RID: 6035
	public float oxygenConsumedPerMask = 1f;

	// Token: 0x04001794 RID: 6036
	public Tag materialTag = GameTags.Metal;

	// Token: 0x04001795 RID: 6037
	public Tag oxygenTag = GameTags.Breathable;

	// Token: 0x04001796 RID: 6038
	public int maxUses = 10;

	// Token: 0x04001797 RID: 6039
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001798 RID: 6040
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x04001799 RID: 6041
	public Storage materialStorage;

	// Token: 0x0400179A RID: 6042
	public Storage oxygenStorage;

	// Token: 0x0400179B RID: 6043
	private bool shouldPump;

	// Token: 0x0400179C RID: 6044
	private MaskStation.OxygenMaskReactable reactable;

	// Token: 0x0400179D RID: 6045
	private MeterController materialsMeter;

	// Token: 0x0400179E RID: 6046
	private MeterController oxygenMeter;

	// Token: 0x0400179F RID: 6047
	public Meter.Offset materialsMeterOffset = Meter.Offset.Behind;

	// Token: 0x040017A0 RID: 6048
	public Meter.Offset oxygenMeterOffset;

	// Token: 0x040017A1 RID: 6049
	public string choreTypeID;

	// Token: 0x040017A2 RID: 6050
	protected FilteredStorage filteredStorage;

	// Token: 0x040017A3 RID: 6051
	public KAnimFile interactAnim = Assets.GetAnim("anim_equip_clothing_kanim");

	// Token: 0x040017A4 RID: 6052
	private int cell;

	// Token: 0x040017A5 RID: 6053
	public PathFinder.PotentialPath.Flags PathFlag;

	// Token: 0x040017A6 RID: 6054
	private Guid noElementStatusGuid;

	// Token: 0x040017A7 RID: 6055
	private Grid.SuitMarker.Flags gridFlags;

	// Token: 0x0200126D RID: 4717
	private class OxygenMaskReactable : Reactable
	{
		// Token: 0x06007A28 RID: 31272 RVA: 0x002C6408 File Offset: 0x002C4608
		public OxygenMaskReactable(MaskStation mask_station)
			: base(mask_station.gameObject, "OxygenMask", Db.Get().ChoreTypes.SuitMarker, 1, 1, false, 0f, 0f, float.PositiveInfinity, 0f, ObjectLayer.NumLayers)
		{
			this.maskStation = mask_station;
		}

		// Token: 0x06007A29 RID: 31273 RVA: 0x002C645C File Offset: 0x002C465C
		public override bool InternalCanBegin(GameObject new_reactor, Navigator.ActiveTransition transition)
		{
			if (this.reactor != null)
			{
				return false;
			}
			if (this.maskStation == null)
			{
				base.Cleanup();
				return false;
			}
			bool flag = !new_reactor.GetComponent<MinionIdentity>().GetEquipment().IsSlotOccupied(Db.Get().AssignableSlots.Suit);
			int x = transition.navGridTransition.x;
			if (x == 0)
			{
				return false;
			}
			if (!flag)
			{
				return (x >= 0 || !this.maskStation.isRotated) && (x <= 0 || this.maskStation.isRotated);
			}
			return this.maskStation.smi.IsReady() && (x <= 0 || !this.maskStation.isRotated) && (x >= 0 || this.maskStation.isRotated);
		}

		// Token: 0x06007A2A RID: 31274 RVA: 0x002C652C File Offset: 0x002C472C
		protected override void InternalBegin()
		{
			this.startTime = Time.time;
			KBatchedAnimController component = this.reactor.GetComponent<KBatchedAnimController>();
			component.AddAnimOverrides(this.maskStation.interactAnim, 1f);
			component.Play("working_pre", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue("working_loop", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue("working_pst", KAnim.PlayMode.Once, 1f, 0f);
			this.maskStation.CreateNewReactable();
		}

		// Token: 0x06007A2B RID: 31275 RVA: 0x002C65C0 File Offset: 0x002C47C0
		public override void Update(float dt)
		{
			Facing facing = (this.reactor ? this.reactor.GetComponent<Facing>() : null);
			if (facing && this.maskStation)
			{
				facing.SetFacing(this.maskStation.GetComponent<Rotatable>().GetOrientation() == Orientation.FlipH);
			}
			if (Time.time - this.startTime > 2.8f)
			{
				this.Run();
				base.Cleanup();
			}
		}

		// Token: 0x06007A2C RID: 31276 RVA: 0x002C6638 File Offset: 0x002C4838
		private void Run()
		{
			GameObject reactor = this.reactor;
			Equipment equipment = reactor.GetComponent<MinionIdentity>().GetEquipment();
			bool flag = !equipment.IsSlotOccupied(Db.Get().AssignableSlots.Suit);
			Navigator component = reactor.GetComponent<Navigator>();
			bool flag2 = component != null && (component.flags & this.maskStation.PathFlag) > PathFinder.PotentialPath.Flags.None;
			if (flag)
			{
				if (!this.maskStation.smi.IsReady())
				{
					return;
				}
				GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("Oxygen_Mask".ToTag()), null, null);
				gameObject.SetActive(true);
				SimHashes elementID = this.maskStation.GetPossibleMaterials()[0].GetComponent<PrimaryElement>().ElementID;
				gameObject.GetComponent<PrimaryElement>().SetElement(elementID, false);
				SuitTank component2 = gameObject.GetComponent<SuitTank>();
				this.maskStation.materialStorage.ConsumeIgnoringDisease(this.maskStation.materialTag, this.maskStation.materialConsumedPerMask);
				this.maskStation.oxygenStorage.Transfer(component2.storage, component2.elementTag, this.maskStation.oxygenConsumedPerMask, false, true);
				Equippable component3 = gameObject.GetComponent<Equippable>();
				component3.Assign(equipment.GetComponent<IAssignableIdentity>());
				component3.isEquipped = true;
			}
			if (!flag)
			{
				Assignable assignable = equipment.GetAssignable(Db.Get().AssignableSlots.Suit);
				assignable.Unassign();
				if (!flag2)
				{
					Notification notification = new Notification(MISC.NOTIFICATIONS.SUIT_DROPPED.NAME, NotificationType.BadMinor, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.SUIT_DROPPED.TOOLTIP, null, true, 0f, null, null, null, true, false, false);
					assignable.GetComponent<Notifier>().Add(notification, "");
				}
			}
		}

		// Token: 0x06007A2D RID: 31277 RVA: 0x002C67DF File Offset: 0x002C49DF
		protected override void InternalEnd()
		{
			if (this.reactor != null)
			{
				this.reactor.GetComponent<KBatchedAnimController>().RemoveAnimOverrides(this.maskStation.interactAnim);
			}
		}

		// Token: 0x06007A2E RID: 31278 RVA: 0x002C680A File Offset: 0x002C4A0A
		protected override void InternalCleanup()
		{
		}

		// Token: 0x04005DC4 RID: 24004
		private MaskStation maskStation;

		// Token: 0x04005DC5 RID: 24005
		private float startTime;
	}

	// Token: 0x0200126E RID: 4718
	public class SMInstance : GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.GameInstance
	{
		// Token: 0x06007A2F RID: 31279 RVA: 0x002C680C File Offset: 0x002C4A0C
		public SMInstance(MaskStation master)
			: base(master)
		{
		}

		// Token: 0x06007A30 RID: 31280 RVA: 0x002C6815 File Offset: 0x002C4A15
		private bool HasSufficientMaterials()
		{
			return base.master.GetTotalMaterialAmount() >= base.master.materialConsumedPerMask;
		}

		// Token: 0x06007A31 RID: 31281 RVA: 0x002C6832 File Offset: 0x002C4A32
		private bool HasSufficientOxygen()
		{
			return base.master.GetTotalOxygenAmount() >= base.master.oxygenConsumedPerMask;
		}

		// Token: 0x06007A32 RID: 31282 RVA: 0x002C684F File Offset: 0x002C4A4F
		public bool OxygenIsFull()
		{
			return base.master.GetTotalOxygenAmount() >= base.master.oxygenConsumedPerMask * (float)base.master.maxUses;
		}

		// Token: 0x06007A33 RID: 31283 RVA: 0x002C6879 File Offset: 0x002C4A79
		public bool IsReady()
		{
			return this.HasSufficientMaterials() && this.HasSufficientOxygen();
		}
	}

	// Token: 0x0200126F RID: 4719
	public class States : GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation>
	{
		// Token: 0x06007A34 RID: 31284 RVA: 0x002C6890 File Offset: 0x002C4A90
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.notOperational;
			this.notOperational.PlayAnim("off").TagTransition(GameTags.Operational, this.charging, false);
			this.charging.TagTransition(GameTags.Operational, this.notOperational, true).EventTransition(GameHashes.OnStorageChange, this.notCharging, (MaskStation.SMInstance smi) => smi.OxygenIsFull() || !smi.master.shouldPump).Update(delegate(MaskStation.SMInstance smi, float dt)
			{
				smi.master.UpdateOperational();
			}, UpdateRate.SIM_1000ms, false)
				.Enter(delegate(MaskStation.SMInstance smi)
				{
					if (smi.OxygenIsFull() || !smi.master.shouldPump)
					{
						smi.GoTo(this.notCharging);
						return;
					}
					if (smi.IsReady())
					{
						smi.GoTo(this.charging.openChargingPre);
						return;
					}
					smi.GoTo(this.charging.closedChargingPre);
				});
			this.charging.opening.QueueAnim("opening_charging", false, null).OnAnimQueueComplete(this.charging.open);
			this.charging.open.PlayAnim("open_charging_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.OnStorageChange, this.charging.closing, (MaskStation.SMInstance smi) => !smi.IsReady());
			this.charging.closing.QueueAnim("closing_charging", false, null).OnAnimQueueComplete(this.charging.closed);
			this.charging.closed.PlayAnim("closed_charging_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.OnStorageChange, this.charging.opening, (MaskStation.SMInstance smi) => smi.IsReady());
			this.charging.openChargingPre.PlayAnim("open_charging_pre").OnAnimQueueComplete(this.charging.open);
			this.charging.closedChargingPre.PlayAnim("closed_charging_pre").OnAnimQueueComplete(this.charging.closed);
			this.notCharging.TagTransition(GameTags.Operational, this.notOperational, true).EventTransition(GameHashes.OnStorageChange, this.charging, (MaskStation.SMInstance smi) => !smi.OxygenIsFull() && smi.master.shouldPump).Update(delegate(MaskStation.SMInstance smi, float dt)
			{
				smi.master.UpdateOperational();
			}, UpdateRate.SIM_1000ms, false)
				.Enter(delegate(MaskStation.SMInstance smi)
				{
					if (!smi.OxygenIsFull() && smi.master.shouldPump)
					{
						smi.GoTo(this.charging);
						return;
					}
					if (smi.IsReady())
					{
						smi.GoTo(this.notCharging.openChargingPst);
						return;
					}
					smi.GoTo(this.notCharging.closedChargingPst);
				});
			this.notCharging.opening.PlayAnim("opening_not_charging").OnAnimQueueComplete(this.notCharging.open);
			this.notCharging.open.PlayAnim("open_not_charging_loop").EventTransition(GameHashes.OnStorageChange, this.notCharging.closing, (MaskStation.SMInstance smi) => !smi.IsReady());
			this.notCharging.closing.PlayAnim("closing_not_charging").OnAnimQueueComplete(this.notCharging.closed);
			this.notCharging.closed.PlayAnim("closed_not_charging_loop").EventTransition(GameHashes.OnStorageChange, this.notCharging.opening, (MaskStation.SMInstance smi) => smi.IsReady());
			this.notCharging.openChargingPst.PlayAnim("open_charging_pst").OnAnimQueueComplete(this.notCharging.open);
			this.notCharging.closedChargingPst.PlayAnim("closed_charging_pst").OnAnimQueueComplete(this.notCharging.closed);
		}

		// Token: 0x04005DC6 RID: 24006
		public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State notOperational;

		// Token: 0x04005DC7 RID: 24007
		public MaskStation.States.ChargingStates charging;

		// Token: 0x04005DC8 RID: 24008
		public MaskStation.States.NotChargingStates notCharging;

		// Token: 0x02001FCB RID: 8139
		public class ChargingStates : GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State
		{
			// Token: 0x04008DAB RID: 36267
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State opening;

			// Token: 0x04008DAC RID: 36268
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State open;

			// Token: 0x04008DAD RID: 36269
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State closing;

			// Token: 0x04008DAE RID: 36270
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State closed;

			// Token: 0x04008DAF RID: 36271
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State openChargingPre;

			// Token: 0x04008DB0 RID: 36272
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State closedChargingPre;
		}

		// Token: 0x02001FCC RID: 8140
		public class NotChargingStates : GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State
		{
			// Token: 0x04008DB1 RID: 36273
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State opening;

			// Token: 0x04008DB2 RID: 36274
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State open;

			// Token: 0x04008DB3 RID: 36275
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State closing;

			// Token: 0x04008DB4 RID: 36276
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State closed;

			// Token: 0x04008DB5 RID: 36277
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State openChargingPst;

			// Token: 0x04008DB6 RID: 36278
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State closedChargingPst;
		}
	}
}
