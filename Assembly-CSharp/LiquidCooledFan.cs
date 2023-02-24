using System;
using System.Collections.Generic;
using Klei;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005DC RID: 1500
[SerializationConfig(MemberSerialization.OptIn)]
public class LiquidCooledFan : StateMachineComponent<LiquidCooledFan.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x0600257A RID: 9594 RVA: 0x000CA614 File Offset: 0x000C8814
	public bool HasMaterial()
	{
		ListPool<GameObject, LiquidCooledFan>.PooledList pooledList = ListPool<GameObject, LiquidCooledFan>.Allocate();
		base.smi.master.gasStorage.Find(GameTags.Water, pooledList);
		if (pooledList.Count > 0)
		{
			global::Debug.LogWarning("Liquid Cooled fan Gas storage contains water - A duplicant probably delivered to the wrong storage - moving it to liquid storage.");
			foreach (GameObject gameObject in pooledList)
			{
				base.smi.master.gasStorage.Transfer(gameObject, base.smi.master.liquidStorage, false, false);
			}
		}
		pooledList.Recycle();
		this.UpdateMeter();
		return this.liquidStorage.MassStored() > 0f;
	}

	// Token: 0x0600257B RID: 9595 RVA: 0x000CA6D8 File Offset: 0x000C88D8
	public void CheckWorking()
	{
		if (base.smi.master.workable.worker == null)
		{
			base.smi.GoTo(base.smi.sm.unworkable);
		}
	}

	// Token: 0x0600257C RID: 9596 RVA: 0x000CA714 File Offset: 0x000C8914
	private void UpdateUnworkableStatusItems()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (!base.smi.EnvironmentNeedsCooling())
		{
			if (!component.HasStatusItem(Db.Get().BuildingStatusItems.CannotCoolFurther))
			{
				component.AddStatusItem(Db.Get().BuildingStatusItems.CannotCoolFurther, null);
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

	// Token: 0x0600257D RID: 9597 RVA: 0x000CA808 File Offset: 0x000C8A08
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Behind, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_waterbody", "meter_waterlevel" });
		base.GetComponent<ElementConsumer>().EnableConsumption(true);
		base.smi.StartSM();
		base.smi.master.waterConsumptionAccumulator = Game.Instance.accumulators.Add("waterConsumptionAccumulator", this);
		base.GetComponent<ElementConsumer>().storage = this.gasStorage;
		base.GetComponent<ManualDeliveryKG>().SetStorage(this.liquidStorage);
	}

	// Token: 0x0600257E RID: 9598 RVA: 0x000CA8B5 File Offset: 0x000C8AB5
	private void UpdateMeter()
	{
		this.meter.SetPositionPercent(Mathf.Clamp01(this.liquidStorage.MassStored() / this.liquidStorage.capacityKg));
	}

	// Token: 0x0600257F RID: 9599 RVA: 0x000CA8E0 File Offset: 0x000C8AE0
	private void EmitContents()
	{
		if (this.gasStorage.items.Count == 0)
		{
			return;
		}
		float num = 0.1f;
		PrimaryElement primaryElement = null;
		for (int i = 0; i < this.gasStorage.items.Count; i++)
		{
			PrimaryElement component = this.gasStorage.items[i].GetComponent<PrimaryElement>();
			if (component.Mass > num && component.Element.IsGas)
			{
				primaryElement = component;
				num = primaryElement.Mass;
			}
		}
		if (primaryElement != null)
		{
			SimMessages.AddRemoveSubstance(Grid.CellRight(Grid.CellAbove(Grid.PosToCell(base.gameObject))), ElementLoader.GetElementIndex(primaryElement.ElementID), CellEventLogger.Instance.ExhaustSimUpdate, primaryElement.Mass, primaryElement.Temperature, primaryElement.DiseaseIdx, primaryElement.DiseaseCount, true, -1);
			this.gasStorage.ConsumeIgnoringDisease(primaryElement.gameObject);
		}
	}

	// Token: 0x06002580 RID: 9600 RVA: 0x000CA9BC File Offset: 0x000C8BBC
	private void CoolContents(float dt)
	{
		if (this.gasStorage.items.Count == 0)
		{
			return;
		}
		float num = float.PositiveInfinity;
		float num2 = 0f;
		foreach (GameObject gameObject in this.gasStorage.items)
		{
			PrimaryElement primaryElement = gameObject.GetComponent<PrimaryElement>();
			if (!(primaryElement == null) && primaryElement.Mass >= 0.1f && primaryElement.Temperature >= this.minCooledTemperature)
			{
				float thermalEnergy = GameUtil.GetThermalEnergy(primaryElement);
				if (num > thermalEnergy)
				{
					num = thermalEnergy;
				}
			}
		}
		foreach (GameObject gameObject2 in this.gasStorage.items)
		{
			PrimaryElement primaryElement = gameObject2.GetComponent<PrimaryElement>();
			if (!(primaryElement == null) && primaryElement.Mass >= 0.1f && primaryElement.Temperature >= this.minCooledTemperature)
			{
				float num3 = Mathf.Min(num, 10f);
				GameUtil.DeltaThermalEnergy(primaryElement, -num3, this.minCooledTemperature);
				num2 += num3;
			}
		}
		float num4 = Mathf.Abs(num2 * this.waterKGConsumedPerKJ);
		Game.Instance.accumulators.Accumulate(base.smi.master.waterConsumptionAccumulator, num4);
		if (num4 != 0f)
		{
			float num5;
			SimUtil.DiseaseInfo diseaseInfo;
			float num6;
			this.liquidStorage.ConsumeAndGetDisease(GameTags.Water, num4, out num5, out diseaseInfo, out num6);
			SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(base.gameObject), diseaseInfo.idx, diseaseInfo.count);
			this.UpdateMeter();
		}
	}

	// Token: 0x06002581 RID: 9601 RVA: 0x000CAB6C File Offset: 0x000C8D6C
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.HEATCONSUMED, GameUtil.GetFormattedHeatEnergy(this.coolingKilowatts, GameUtil.HeatEnergyFormatterUnit.Automatic)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.HEATCONSUMED, GameUtil.GetFormattedHeatEnergy(this.coolingKilowatts, GameUtil.HeatEnergyFormatterUnit.Automatic)), Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		return list;
	}

	// Token: 0x040015D2 RID: 5586
	[SerializeField]
	public float coolingKilowatts;

	// Token: 0x040015D3 RID: 5587
	[SerializeField]
	public float minCooledTemperature;

	// Token: 0x040015D4 RID: 5588
	[SerializeField]
	public float minEnvironmentMass;

	// Token: 0x040015D5 RID: 5589
	[SerializeField]
	public float waterKGConsumedPerKJ;

	// Token: 0x040015D6 RID: 5590
	[SerializeField]
	public Vector2I minCoolingRange;

	// Token: 0x040015D7 RID: 5591
	[SerializeField]
	public Vector2I maxCoolingRange;

	// Token: 0x040015D8 RID: 5592
	private float flowRate = 0.3f;

	// Token: 0x040015D9 RID: 5593
	[SerializeField]
	public Storage gasStorage;

	// Token: 0x040015DA RID: 5594
	[SerializeField]
	public Storage liquidStorage;

	// Token: 0x040015DB RID: 5595
	[MyCmpAdd]
	private LiquidCooledFanWorkable workable;

	// Token: 0x040015DC RID: 5596
	[MyCmpGet]
	private Operational operational;

	// Token: 0x040015DD RID: 5597
	private HandleVector<int>.Handle waterConsumptionAccumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x040015DE RID: 5598
	private MeterController meter;

	// Token: 0x02001239 RID: 4665
	public class StatesInstance : GameStateMachine<LiquidCooledFan.States, LiquidCooledFan.StatesInstance, LiquidCooledFan, object>.GameInstance
	{
		// Token: 0x0600796F RID: 31087 RVA: 0x002C37ED File Offset: 0x002C19ED
		public StatesInstance(LiquidCooledFan smi)
			: base(smi)
		{
		}

		// Token: 0x06007970 RID: 31088 RVA: 0x002C37F8 File Offset: 0x002C19F8
		public bool IsWorkable()
		{
			bool flag = false;
			if (base.master.operational.IsOperational && this.EnvironmentNeedsCooling() && base.smi.master.HasMaterial() && base.smi.EnvironmentHighEnoughPressure())
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06007971 RID: 31089 RVA: 0x002C3844 File Offset: 0x002C1A44
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

		// Token: 0x06007972 RID: 31090 RVA: 0x002C38EC File Offset: 0x002C1AEC
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

	// Token: 0x0200123A RID: 4666
	public class States : GameStateMachine<LiquidCooledFan.States, LiquidCooledFan.StatesInstance, LiquidCooledFan>
	{
		// Token: 0x06007973 RID: 31091 RVA: 0x002C398C File Offset: 0x002C1B8C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unworkable;
			this.root.Enter(delegate(LiquidCooledFan.StatesInstance smi)
			{
				smi.master.workable.SetWorkTime(float.PositiveInfinity);
			});
			this.workable.ToggleChore(new Func<LiquidCooledFan.StatesInstance, Chore>(this.CreateUseChore), this.work_pst).EventTransition(GameHashes.ActiveChanged, this.workable.consuming, (LiquidCooledFan.StatesInstance smi) => smi.master.workable.worker != null).EventTransition(GameHashes.OperationalChanged, this.workable.consuming, (LiquidCooledFan.StatesInstance smi) => smi.master.workable.worker != null)
				.Transition(this.unworkable, (LiquidCooledFan.StatesInstance smi) => !smi.IsWorkable(), UpdateRate.SIM_200ms);
			this.work_pst.Update("LiquidFanEmitCooledContents", delegate(LiquidCooledFan.StatesInstance smi, float dt)
			{
				smi.master.EmitContents();
			}, UpdateRate.SIM_200ms, false).ScheduleGoTo(2f, this.unworkable);
			this.unworkable.Update("LiquidFanEmitCooledContents", delegate(LiquidCooledFan.StatesInstance smi, float dt)
			{
				smi.master.EmitContents();
			}, UpdateRate.SIM_200ms, false).Update("LiquidFanUnworkableStatusItems", delegate(LiquidCooledFan.StatesInstance smi, float dt)
			{
				smi.master.UpdateUnworkableStatusItems();
			}, UpdateRate.SIM_200ms, false).Transition(this.workable.waiting, (LiquidCooledFan.StatesInstance smi) => smi.IsWorkable(), UpdateRate.SIM_200ms)
				.Enter(delegate(LiquidCooledFan.StatesInstance smi)
				{
					smi.master.UpdateUnworkableStatusItems();
				})
				.Exit(delegate(LiquidCooledFan.StatesInstance smi)
				{
					smi.master.UpdateUnworkableStatusItems();
				});
			this.workable.consuming.EventTransition(GameHashes.OperationalChanged, this.unworkable, (LiquidCooledFan.StatesInstance smi) => smi.master.workable.worker == null).EventHandler(GameHashes.ActiveChanged, delegate(LiquidCooledFan.StatesInstance smi)
			{
				smi.master.CheckWorking();
			}).Enter(delegate(LiquidCooledFan.StatesInstance smi)
			{
				if (!smi.EnvironmentNeedsCooling() || !smi.master.HasMaterial() || !smi.EnvironmentHighEnoughPressure())
				{
					smi.GoTo(this.unworkable);
				}
				ElementConsumer component = smi.master.GetComponent<ElementConsumer>();
				component.consumptionRate = smi.master.flowRate;
				component.RefreshConsumptionRate();
			})
				.Update(delegate(LiquidCooledFan.StatesInstance smi, float dt)
				{
					smi.master.CoolContents(dt);
				}, UpdateRate.SIM_200ms, false)
				.ScheduleGoTo(12f, this.workable.emitting)
				.Exit(delegate(LiquidCooledFan.StatesInstance smi)
				{
					ElementConsumer component2 = smi.master.GetComponent<ElementConsumer>();
					component2.consumptionRate = 0f;
					component2.RefreshConsumptionRate();
				});
			this.workable.emitting.EventTransition(GameHashes.ActiveChanged, this.unworkable, (LiquidCooledFan.StatesInstance smi) => smi.master.workable.worker == null).EventTransition(GameHashes.OperationalChanged, this.unworkable, (LiquidCooledFan.StatesInstance smi) => smi.master.workable.worker == null).ScheduleGoTo(3f, this.workable.consuming)
				.Update(delegate(LiquidCooledFan.StatesInstance smi, float dt)
				{
					smi.master.CoolContents(dt);
				}, UpdateRate.SIM_200ms, false)
				.Update("LiquidFanEmitCooledContents", delegate(LiquidCooledFan.StatesInstance smi, float dt)
				{
					smi.master.EmitContents();
				}, UpdateRate.SIM_200ms, false);
		}

		// Token: 0x06007974 RID: 31092 RVA: 0x002C3D38 File Offset: 0x002C1F38
		private Chore CreateUseChore(LiquidCooledFan.StatesInstance smi)
		{
			return new WorkChore<LiquidCooledFanWorkable>(Db.Get().ChoreTypes.LiquidCooledFan, smi.master.workable, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}

		// Token: 0x04005D51 RID: 23889
		public LiquidCooledFan.States.Workable workable;

		// Token: 0x04005D52 RID: 23890
		public GameStateMachine<LiquidCooledFan.States, LiquidCooledFan.StatesInstance, LiquidCooledFan, object>.State unworkable;

		// Token: 0x04005D53 RID: 23891
		public GameStateMachine<LiquidCooledFan.States, LiquidCooledFan.StatesInstance, LiquidCooledFan, object>.State work_pst;

		// Token: 0x02001FC0 RID: 8128
		public class Workable : GameStateMachine<LiquidCooledFan.States, LiquidCooledFan.StatesInstance, LiquidCooledFan, object>.State
		{
			// Token: 0x04008D74 RID: 36212
			public GameStateMachine<LiquidCooledFan.States, LiquidCooledFan.StatesInstance, LiquidCooledFan, object>.State waiting;

			// Token: 0x04008D75 RID: 36213
			public GameStateMachine<LiquidCooledFan.States, LiquidCooledFan.StatesInstance, LiquidCooledFan, object>.State consuming;

			// Token: 0x04008D76 RID: 36214
			public GameStateMachine<LiquidCooledFan.States, LiquidCooledFan.StatesInstance, LiquidCooledFan, object>.State emitting;
		}
	}
}
