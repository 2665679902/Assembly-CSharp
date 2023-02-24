using System;
using KSerialization;
using UnityEngine;

// Token: 0x020005D3 RID: 1491
[SerializationConfig(MemberSerialization.OptIn)]
public class IceCooledFan : StateMachineComponent<IceCooledFan.StatesInstance>
{
	// Token: 0x06002535 RID: 9525 RVA: 0x000C8F8A File Offset: 0x000C718A
	public bool HasMaterial()
	{
		this.UpdateMeter();
		return this.iceStorage.MassStored() > 0f;
	}

	// Token: 0x06002536 RID: 9526 RVA: 0x000C8FA4 File Offset: 0x000C71A4
	public void CheckWorking()
	{
		if (base.smi.master.workable.worker == null)
		{
			base.smi.GoTo(base.smi.sm.unworkable);
		}
	}

	// Token: 0x06002537 RID: 9527 RVA: 0x000C8FE0 File Offset: 0x000C71E0
	private void UpdateUnworkableStatusItems()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (!base.smi.EnvironmentNeedsCooling())
		{
			if (!component.HasStatusItem(Db.Get().BuildingStatusItems.CannotCoolFurther))
			{
				component.AddStatusItem(Db.Get().BuildingStatusItems.CannotCoolFurther, this.minCooledTemperature);
			}
		}
		else if (component.HasStatusItem(Db.Get().BuildingStatusItems.CannotCoolFurther))
		{
			component.RemoveStatusItem(Db.Get().BuildingStatusItems.CannotCoolFurther, false);
		}
		if (!base.smi.EnvironmentHighEnoughPressure())
		{
			if (!component.HasStatusItem(Db.Get().BuildingStatusItems.UnderPressure))
			{
				component.AddStatusItem(Db.Get().BuildingStatusItems.UnderPressure, this.minEnvironmentMass);
				return;
			}
		}
		else if (component.HasStatusItem(Db.Get().BuildingStatusItems.UnderPressure))
		{
			component.RemoveStatusItem(Db.Get().BuildingStatusItems.UnderPressure, false);
		}
	}

	// Token: 0x06002538 RID: 9528 RVA: 0x000C90E0 File Offset: 0x000C72E0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_waterbody", "meter_waterlevel" });
		base.smi.StartSM();
		base.GetComponent<ManualDeliveryKG>().SetStorage(this.iceStorage);
	}

	// Token: 0x06002539 RID: 9529 RVA: 0x000C914C File Offset: 0x000C734C
	private void UpdateMeter()
	{
		float num = 0f;
		foreach (GameObject gameObject in this.iceStorage.items)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			num += component.Temperature;
		}
		num /= (float)this.iceStorage.items.Count;
		float num2 = Mathf.Clamp01((num - this.LOW_ICE_TEMP) / (this.targetTemperature - this.LOW_ICE_TEMP));
		this.meter.SetPositionPercent(1f - num2);
	}

	// Token: 0x0600253A RID: 9530 RVA: 0x000C91F4 File Offset: 0x000C73F4
	private void DoCooling(float dt)
	{
		float num = this.coolingRate * dt;
		foreach (GameObject gameObject in this.iceStorage.items)
		{
			GameUtil.DeltaThermalEnergy(gameObject.GetComponent<PrimaryElement>(), num, this.targetTemperature);
		}
		for (int i = this.iceStorage.items.Count; i > 0; i--)
		{
			GameObject gameObject2 = this.iceStorage.items[i - 1];
			if (gameObject2 != null && gameObject2.GetComponent<PrimaryElement>().Temperature > gameObject2.GetComponent<PrimaryElement>().Element.highTemp && gameObject2.GetComponent<PrimaryElement>().Element.HasTransitionUp)
			{
				PrimaryElement component = gameObject2.GetComponent<PrimaryElement>();
				this.iceStorage.AddLiquid(component.Element.highTempTransitionTarget, component.Mass, component.Temperature, component.DiseaseIdx, component.DiseaseCount, false, true);
				this.iceStorage.ConsumeIgnoringDisease(gameObject2);
			}
		}
		for (int j = this.iceStorage.items.Count; j > 0; j--)
		{
			GameObject gameObject3 = this.iceStorage.items[j - 1];
			if (gameObject3 != null && gameObject3.GetComponent<PrimaryElement>().Temperature >= this.targetTemperature)
			{
				this.iceStorage.Transfer(gameObject3, this.liquidStorage, true, true);
			}
		}
		if (!this.liquidStorage.IsEmpty())
		{
			this.liquidStorage.DropAll(false, false, new Vector3(1f, 0f, 0f), true, null);
		}
		this.UpdateMeter();
	}

	// Token: 0x0400158B RID: 5515
	[SerializeField]
	public float minCooledTemperature;

	// Token: 0x0400158C RID: 5516
	[SerializeField]
	public float minEnvironmentMass;

	// Token: 0x0400158D RID: 5517
	[SerializeField]
	public float coolingRate;

	// Token: 0x0400158E RID: 5518
	[SerializeField]
	public float targetTemperature;

	// Token: 0x0400158F RID: 5519
	[SerializeField]
	public Vector2I minCoolingRange;

	// Token: 0x04001590 RID: 5520
	[SerializeField]
	public Vector2I maxCoolingRange;

	// Token: 0x04001591 RID: 5521
	[SerializeField]
	public Storage iceStorage;

	// Token: 0x04001592 RID: 5522
	[SerializeField]
	public Storage liquidStorage;

	// Token: 0x04001593 RID: 5523
	[SerializeField]
	public Tag consumptionTag;

	// Token: 0x04001594 RID: 5524
	private float LOW_ICE_TEMP = 173.15f;

	// Token: 0x04001595 RID: 5525
	[MyCmpAdd]
	private IceCooledFanWorkable workable;

	// Token: 0x04001596 RID: 5526
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001597 RID: 5527
	private MeterController meter;

	// Token: 0x0200122A RID: 4650
	public class StatesInstance : GameStateMachine<IceCooledFan.States, IceCooledFan.StatesInstance, IceCooledFan, object>.GameInstance
	{
		// Token: 0x06007945 RID: 31045 RVA: 0x002C2ADD File Offset: 0x002C0CDD
		public StatesInstance(IceCooledFan smi)
			: base(smi)
		{
		}

		// Token: 0x06007946 RID: 31046 RVA: 0x002C2AE8 File Offset: 0x002C0CE8
		public bool IsWorkable()
		{
			bool flag = false;
			if (base.master.operational.IsOperational && this.EnvironmentNeedsCooling() && base.smi.master.HasMaterial() && base.smi.EnvironmentHighEnoughPressure())
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06007947 RID: 31047 RVA: 0x002C2B34 File Offset: 0x002C0D34
		public bool EnvironmentNeedsCooling()
		{
			bool flag = false;
			int num = Grid.PosToCell(base.transform.GetPosition());
			for (int i = base.master.minCoolingRange.y; i < base.master.maxCoolingRange.y; i++)
			{
				for (int j = base.master.minCoolingRange.x; j < base.master.maxCoolingRange.x; j++)
				{
					CellOffset cellOffset = new CellOffset(j, i);
					int num2 = Grid.OffsetCell(num, cellOffset);
					if (Grid.Temperature[num2] > base.master.minCooledTemperature)
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06007948 RID: 31048 RVA: 0x002C2BDC File Offset: 0x002C0DDC
		public bool EnvironmentHighEnoughPressure()
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			for (int i = base.master.minCoolingRange.y; i < base.master.maxCoolingRange.y; i++)
			{
				for (int j = base.master.minCoolingRange.x; j < base.master.maxCoolingRange.x; j++)
				{
					CellOffset cellOffset = new CellOffset(j, i);
					int num2 = Grid.OffsetCell(num, cellOffset);
					if (Grid.Mass[num2] >= base.master.minEnvironmentMass)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	// Token: 0x0200122B RID: 4651
	public class States : GameStateMachine<IceCooledFan.States, IceCooledFan.StatesInstance, IceCooledFan>
	{
		// Token: 0x06007949 RID: 31049 RVA: 0x002C2C7C File Offset: 0x002C0E7C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unworkable;
			this.root.Enter(delegate(IceCooledFan.StatesInstance smi)
			{
				smi.master.workable.SetWorkTime(float.PositiveInfinity);
			});
			this.workable.ToggleChore(new Func<IceCooledFan.StatesInstance, Chore>(this.CreateUseChore), this.work_pst).EventTransition(GameHashes.ActiveChanged, this.workable.cooling, (IceCooledFan.StatesInstance smi) => smi.master.workable.worker != null).EventTransition(GameHashes.OperationalChanged, this.workable.cooling, (IceCooledFan.StatesInstance smi) => smi.master.workable.worker != null)
				.Transition(this.unworkable, (IceCooledFan.StatesInstance smi) => !smi.IsWorkable(), UpdateRate.SIM_200ms);
			this.workable.cooling.EventTransition(GameHashes.OperationalChanged, this.unworkable, (IceCooledFan.StatesInstance smi) => smi.master.workable.worker == null).EventHandler(GameHashes.ActiveChanged, delegate(IceCooledFan.StatesInstance smi)
			{
				smi.master.CheckWorking();
			}).Enter(delegate(IceCooledFan.StatesInstance smi)
			{
				smi.master.gameObject.GetComponent<ManualDeliveryKG>().Pause(true, "Working");
				if (!smi.EnvironmentNeedsCooling() || !smi.master.HasMaterial() || !smi.EnvironmentHighEnoughPressure())
				{
					smi.GoTo(this.unworkable);
				}
			})
				.Update("IceCooledFanCooling", delegate(IceCooledFan.StatesInstance smi, float dt)
				{
					smi.master.DoCooling(dt);
				}, UpdateRate.SIM_200ms, false)
				.Exit(delegate(IceCooledFan.StatesInstance smi)
				{
					if (!smi.master.HasMaterial())
					{
						smi.master.gameObject.GetComponent<ManualDeliveryKG>().Pause(false, "Working");
					}
					smi.master.liquidStorage.DropAll(false, false, default(Vector3), true, null);
				});
			this.work_pst.ScheduleGoTo(2f, this.unworkable);
			this.unworkable.Update("IceFanUnworkableStatusItems", delegate(IceCooledFan.StatesInstance smi, float dt)
			{
				smi.master.UpdateUnworkableStatusItems();
			}, UpdateRate.SIM_200ms, false).Transition(this.workable.waiting, (IceCooledFan.StatesInstance smi) => smi.IsWorkable(), UpdateRate.SIM_200ms).Enter(delegate(IceCooledFan.StatesInstance smi)
			{
				smi.master.UpdateUnworkableStatusItems();
			})
				.Exit(delegate(IceCooledFan.StatesInstance smi)
				{
					smi.master.UpdateUnworkableStatusItems();
				});
		}

		// Token: 0x0600794A RID: 31050 RVA: 0x002C2EF4 File Offset: 0x002C10F4
		private Chore CreateUseChore(IceCooledFan.StatesInstance smi)
		{
			return new WorkChore<IceCooledFanWorkable>(Db.Get().ChoreTypes.IceCooledFan, smi.master.workable, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}

		// Token: 0x04005D32 RID: 23858
		public IceCooledFan.States.Workable workable;

		// Token: 0x04005D33 RID: 23859
		public GameStateMachine<IceCooledFan.States, IceCooledFan.StatesInstance, IceCooledFan, object>.State unworkable;

		// Token: 0x04005D34 RID: 23860
		public GameStateMachine<IceCooledFan.States, IceCooledFan.StatesInstance, IceCooledFan, object>.State work_pst;

		// Token: 0x02001FBA RID: 8122
		public class Workable : GameStateMachine<IceCooledFan.States, IceCooledFan.StatesInstance, IceCooledFan, object>.State
		{
			// Token: 0x04008D50 RID: 36176
			public GameStateMachine<IceCooledFan.States, IceCooledFan.StatesInstance, IceCooledFan, object>.State waiting;

			// Token: 0x04008D51 RID: 36177
			public GameStateMachine<IceCooledFan.States, IceCooledFan.StatesInstance, IceCooledFan, object>.State cooling;
		}
	}
}
