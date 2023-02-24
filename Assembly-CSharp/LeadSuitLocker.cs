using System;
using TUNING;
using UnityEngine;

// Token: 0x020005D9 RID: 1497
public class LeadSuitLocker : StateMachineComponent<LeadSuitLocker.StatesInstance>
{
	// Token: 0x0600255A RID: 9562 RVA: 0x000C9E34 File Offset: 0x000C8034
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.o2_meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target_top", "meter_oxygen", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Vector3.zero, new string[] { "meter_target_top" });
		this.battery_meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target_side", "meter_petrol", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Vector3.zero, new string[] { "meter_target_side" });
		base.smi.StartSM();
	}

	// Token: 0x0600255B RID: 9563 RVA: 0x000C9EB4 File Offset: 0x000C80B4
	public bool IsSuitFullyCharged()
	{
		return this.suit_locker.IsSuitFullyCharged();
	}

	// Token: 0x0600255C RID: 9564 RVA: 0x000C9EC1 File Offset: 0x000C80C1
	public KPrefabID GetStoredOutfit()
	{
		return this.suit_locker.GetStoredOutfit();
	}

	// Token: 0x0600255D RID: 9565 RVA: 0x000C9ED0 File Offset: 0x000C80D0
	private void FillBattery(float dt)
	{
		KPrefabID storedOutfit = this.suit_locker.GetStoredOutfit();
		if (storedOutfit == null)
		{
			return;
		}
		LeadSuitTank component = storedOutfit.GetComponent<LeadSuitTank>();
		if (!component.IsFull())
		{
			component.batteryCharge += dt / this.batteryChargeTime;
		}
	}

	// Token: 0x0600255E RID: 9566 RVA: 0x000C9F18 File Offset: 0x000C8118
	private void RefreshMeter()
	{
		this.o2_meter.SetPositionPercent(this.suit_locker.OxygenAvailable);
		this.battery_meter.SetPositionPercent(this.suit_locker.BatteryAvailable);
		this.anim_controller.SetSymbolVisiblity("oxygen_yes_bloom", this.IsOxygenTankAboveMinimumLevel());
		this.anim_controller.SetSymbolVisiblity("petrol_yes_bloom", this.IsBatteryAboveMinimumLevel());
	}

	// Token: 0x0600255F RID: 9567 RVA: 0x000C9F88 File Offset: 0x000C8188
	public bool IsOxygenTankAboveMinimumLevel()
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (storedOutfit != null)
		{
			SuitTank component = storedOutfit.GetComponent<SuitTank>();
			return component == null || component.PercentFull() >= EQUIPMENT.SUITS.MINIMUM_USABLE_SUIT_CHARGE;
		}
		return false;
	}

	// Token: 0x06002560 RID: 9568 RVA: 0x000C9FCC File Offset: 0x000C81CC
	public bool IsBatteryAboveMinimumLevel()
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (storedOutfit != null)
		{
			LeadSuitTank component = storedOutfit.GetComponent<LeadSuitTank>();
			return component == null || component.PercentFull() >= EQUIPMENT.SUITS.MINIMUM_USABLE_SUIT_CHARGE;
		}
		return false;
	}

	// Token: 0x040015B6 RID: 5558
	[MyCmpReq]
	private Building building;

	// Token: 0x040015B7 RID: 5559
	[MyCmpReq]
	private Storage storage;

	// Token: 0x040015B8 RID: 5560
	[MyCmpReq]
	private SuitLocker suit_locker;

	// Token: 0x040015B9 RID: 5561
	[MyCmpReq]
	private KBatchedAnimController anim_controller;

	// Token: 0x040015BA RID: 5562
	private MeterController o2_meter;

	// Token: 0x040015BB RID: 5563
	private MeterController battery_meter;

	// Token: 0x040015BC RID: 5564
	private float batteryChargeTime = 60f;

	// Token: 0x02001235 RID: 4661
	public class States : GameStateMachine<LeadSuitLocker.States, LeadSuitLocker.StatesInstance, LeadSuitLocker>
	{
		// Token: 0x06007965 RID: 31077 RVA: 0x002C35F8 File Offset: 0x002C17F8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.empty;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.Update("RefreshMeter", delegate(LeadSuitLocker.StatesInstance smi, float dt)
			{
				smi.master.RefreshMeter();
			}, UpdateRate.RENDER_200ms, false);
			this.empty.EventTransition(GameHashes.OnStorageChange, this.charging, (LeadSuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit() != null);
			this.charging.DefaultState(this.charging.notoperational).EventTransition(GameHashes.OnStorageChange, this.empty, (LeadSuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit() == null).Transition(this.charged, (LeadSuitLocker.StatesInstance smi) => smi.master.IsSuitFullyCharged(), UpdateRate.SIM_200ms);
			this.charging.notoperational.TagTransition(GameTags.Operational, this.charging.operational, false);
			this.charging.operational.TagTransition(GameTags.Operational, this.charging.notoperational, true).Update("FillBattery", delegate(LeadSuitLocker.StatesInstance smi, float dt)
			{
				smi.master.FillBattery(dt);
			}, UpdateRate.SIM_1000ms, false);
			this.charged.EventTransition(GameHashes.OnStorageChange, this.empty, (LeadSuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit() == null);
		}

		// Token: 0x04005D4B RID: 23883
		public GameStateMachine<LeadSuitLocker.States, LeadSuitLocker.StatesInstance, LeadSuitLocker, object>.State empty;

		// Token: 0x04005D4C RID: 23884
		public LeadSuitLocker.States.ChargingStates charging;

		// Token: 0x04005D4D RID: 23885
		public GameStateMachine<LeadSuitLocker.States, LeadSuitLocker.StatesInstance, LeadSuitLocker, object>.State charged;

		// Token: 0x02001FBE RID: 8126
		public class ChargingStates : GameStateMachine<LeadSuitLocker.States, LeadSuitLocker.StatesInstance, LeadSuitLocker, object>.State
		{
			// Token: 0x04008D6B RID: 36203
			public GameStateMachine<LeadSuitLocker.States, LeadSuitLocker.StatesInstance, LeadSuitLocker, object>.State notoperational;

			// Token: 0x04008D6C RID: 36204
			public GameStateMachine<LeadSuitLocker.States, LeadSuitLocker.StatesInstance, LeadSuitLocker, object>.State operational;
		}
	}

	// Token: 0x02001236 RID: 4662
	public class StatesInstance : GameStateMachine<LeadSuitLocker.States, LeadSuitLocker.StatesInstance, LeadSuitLocker, object>.GameInstance
	{
		// Token: 0x06007967 RID: 31079 RVA: 0x002C379A File Offset: 0x002C199A
		public StatesInstance(LeadSuitLocker lead_suit_locker)
			: base(lead_suit_locker)
		{
		}
	}
}
