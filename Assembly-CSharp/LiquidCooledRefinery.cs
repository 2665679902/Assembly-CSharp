using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020005DD RID: 1501
public class LiquidCooledRefinery : ComplexFabricator
{
	// Token: 0x06002583 RID: 9603 RVA: 0x000CABE9 File Offset: 0x000C8DE9
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LiquidCooledRefinery>(-1697596308, LiquidCooledRefinery.OnStorageChangeDelegate);
	}

	// Token: 0x06002584 RID: 9604 RVA: 0x000CAC04 File Offset: 0x000C8E04
	protected override void OnSpawn()
	{
		base.OnSpawn();
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		this.meter_coolant = new MeterController(component, "meter_target", "meter_coolant", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Vector3.zero, null);
		this.meter_metal = new MeterController(component, "meter_target_metal", "meter_metal", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Vector3.zero, null);
		this.meter_metal.SetPositionPercent(1f);
		this.smi = new LiquidCooledRefinery.StatesInstance(this);
		this.smi.StartSM();
		Game.Instance.liquidConduitFlow.AddConduitUpdater(new Action<float>(this.OnConduitUpdate), ConduitFlowPriority.Default);
		Building component2 = base.GetComponent<Building>();
		this.outputCell = component2.GetUtilityOutputCell();
		this.workable.OnWorkTickActions = delegate(Worker worker, float dt)
		{
			float percentComplete = this.workable.GetPercentComplete();
			this.meter_metal.SetPositionPercent(percentComplete);
		};
	}

	// Token: 0x06002585 RID: 9605 RVA: 0x000CACC9 File Offset: 0x000C8EC9
	protected override void OnCleanUp()
	{
		Game.Instance.liquidConduitFlow.RemoveConduitUpdater(new Action<float>(this.OnConduitUpdate));
		base.OnCleanUp();
	}

	// Token: 0x06002586 RID: 9606 RVA: 0x000CACEC File Offset: 0x000C8EEC
	private void OnConduitUpdate(float dt)
	{
		bool flag = Game.Instance.liquidConduitFlow.GetContents(this.outputCell).mass > 0f;
		this.smi.sm.outputBlocked.Set(flag, this.smi, false);
		this.operational.SetFlag(LiquidCooledRefinery.coolantOutputPipeEmpty, !flag);
	}

	// Token: 0x06002587 RID: 9607 RVA: 0x000CAD50 File Offset: 0x000C8F50
	public bool HasEnoughCoolant()
	{
		return this.inStorage.GetAmountAvailable(this.coolantTag) + this.buildStorage.GetAmountAvailable(this.coolantTag) >= this.minCoolantMass;
	}

	// Token: 0x06002588 RID: 9608 RVA: 0x000CAD80 File Offset: 0x000C8F80
	private void OnStorageChange(object data)
	{
		float amountAvailable = this.inStorage.GetAmountAvailable(this.coolantTag);
		float capacityKG = this.conduitConsumer.capacityKG;
		float num = Mathf.Clamp01(amountAvailable / capacityKG);
		if (this.meter_coolant != null)
		{
			this.meter_coolant.SetPositionPercent(num);
		}
	}

	// Token: 0x06002589 RID: 9609 RVA: 0x000CADC6 File Offset: 0x000C8FC6
	protected override bool HasIngredients(ComplexRecipe recipe, Storage storage)
	{
		return storage.GetAmountAvailable(this.coolantTag) >= this.minCoolantMass && base.HasIngredients(recipe, storage);
	}

	// Token: 0x0600258A RID: 9610 RVA: 0x000CADE8 File Offset: 0x000C8FE8
	protected override void TransferCurrentRecipeIngredientsForBuild()
	{
		base.TransferCurrentRecipeIngredientsForBuild();
		float num = this.minCoolantMass;
		while (this.buildStorage.GetAmountAvailable(this.coolantTag) < this.minCoolantMass && this.inStorage.GetAmountAvailable(this.coolantTag) > 0f && num > 0f)
		{
			float num2 = this.inStorage.Transfer(this.buildStorage, this.coolantTag, num, false, true);
			num -= num2;
		}
	}

	// Token: 0x0600258B RID: 9611 RVA: 0x000CAE5C File Offset: 0x000C905C
	protected override List<GameObject> SpawnOrderProduct(ComplexRecipe recipe)
	{
		List<GameObject> list = base.SpawnOrderProduct(recipe);
		PrimaryElement component = list[0].GetComponent<PrimaryElement>();
		component.Temperature = this.outputTemperature;
		float num = GameUtil.CalculateEnergyDeltaForElementChange(component.Element.specificHeatCapacity, component.Mass, component.Element.highTemp, this.outputTemperature);
		ListPool<GameObject, LiquidCooledRefinery>.PooledList pooledList = ListPool<GameObject, LiquidCooledRefinery>.Allocate();
		this.buildStorage.Find(this.coolantTag, pooledList);
		float num2 = 0f;
		foreach (GameObject gameObject in pooledList)
		{
			PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
			if (component2.Mass != 0f)
			{
				num2 += component2.Mass * component2.Element.specificHeatCapacity;
			}
		}
		foreach (GameObject gameObject2 in pooledList)
		{
			PrimaryElement component3 = gameObject2.GetComponent<PrimaryElement>();
			if (component3.Mass != 0f)
			{
				float num3 = component3.Mass * component3.Element.specificHeatCapacity / num2;
				float num4 = -num * num3 * this.thermalFudge;
				float num5 = GameUtil.CalculateTemperatureChange(component3.Element.specificHeatCapacity, component3.Mass, num4);
				float temperature = component3.Temperature;
				component3.Temperature += num5;
			}
		}
		this.buildStorage.Transfer(this.outStorage, this.coolantTag, float.MaxValue, false, true);
		pooledList.Recycle();
		return list;
	}

	// Token: 0x0600258C RID: 9612 RVA: 0x000CB008 File Offset: 0x000C9208
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> descriptors = base.GetDescriptors(go);
		descriptors.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.COOLANT, this.coolantTag.ProperName(), GameUtil.GetFormattedMass(this.minCoolantMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.COOLANT, this.coolantTag.ProperName(), GameUtil.GetFormattedMass(this.minCoolantMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Requirement, false));
		return descriptors;
	}

	// Token: 0x0600258D RID: 9613 RVA: 0x000CB084 File Offset: 0x000C9284
	public override List<Descriptor> AdditionalEffectsForRecipe(ComplexRecipe recipe)
	{
		List<Descriptor> list = base.AdditionalEffectsForRecipe(recipe);
		PrimaryElement component = Assets.GetPrefab(recipe.results[0].material).GetComponent<PrimaryElement>();
		PrimaryElement primaryElement = this.inStorage.FindFirstWithMass(this.coolantTag, 0f);
		string text = UI.BUILDINGEFFECTS.TOOLTIPS.REFINEMENT_ENERGY_HAS_COOLANT;
		if (primaryElement == null)
		{
			primaryElement = Assets.GetPrefab(GameTags.Water).GetComponent<PrimaryElement>();
			text = UI.BUILDINGEFFECTS.TOOLTIPS.REFINEMENT_ENERGY_NO_COOLANT;
		}
		float num = -GameUtil.CalculateEnergyDeltaForElementChange(component.Element.specificHeatCapacity, recipe.results[0].amount, component.Element.highTemp, this.outputTemperature);
		float num2 = GameUtil.CalculateTemperatureChange(primaryElement.Element.specificHeatCapacity, this.minCoolantMass, num * this.thermalFudge);
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.REFINEMENT_ENERGY, GameUtil.GetFormattedJoules(num, "F1", GameUtil.TimeSlice.None)), string.Format(text, GameUtil.GetFormattedJoules(num, "F1", GameUtil.TimeSlice.None), primaryElement.GetProperName(), GameUtil.GetFormattedTemperature(num2, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Relative, true, false)), Descriptor.DescriptorType.Effect, false));
		return list;
	}

	// Token: 0x040015DF RID: 5599
	[MyCmpReq]
	private ConduitConsumer conduitConsumer;

	// Token: 0x040015E0 RID: 5600
	public static readonly Operational.Flag coolantOutputPipeEmpty = new Operational.Flag("coolantOutputPipeEmpty", Operational.Flag.Type.Requirement);

	// Token: 0x040015E1 RID: 5601
	private int outputCell;

	// Token: 0x040015E2 RID: 5602
	public Tag coolantTag;

	// Token: 0x040015E3 RID: 5603
	public float minCoolantMass = 100f;

	// Token: 0x040015E4 RID: 5604
	public float thermalFudge = 0.8f;

	// Token: 0x040015E5 RID: 5605
	public float outputTemperature = 313.15f;

	// Token: 0x040015E6 RID: 5606
	private MeterController meter_coolant;

	// Token: 0x040015E7 RID: 5607
	private MeterController meter_metal;

	// Token: 0x040015E8 RID: 5608
	private LiquidCooledRefinery.StatesInstance smi;

	// Token: 0x040015E9 RID: 5609
	private static readonly EventSystem.IntraObjectHandler<LiquidCooledRefinery> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<LiquidCooledRefinery>(delegate(LiquidCooledRefinery component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x0200123B RID: 4667
	public class StatesInstance : GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.GameInstance
	{
		// Token: 0x06007977 RID: 31095 RVA: 0x002C3DD7 File Offset: 0x002C1FD7
		public StatesInstance(LiquidCooledRefinery master)
			: base(master)
		{
		}
	}

	// Token: 0x0200123C RID: 4668
	public class States : GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery>
	{
		// Token: 0x06007978 RID: 31096 RVA: 0x002C3DE0 File Offset: 0x002C1FE0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			if (LiquidCooledRefinery.States.waitingForCoolantStatus == null)
			{
				LiquidCooledRefinery.States.waitingForCoolantStatus = new StatusItem("waitingForCoolantStatus", BUILDING.STATUSITEMS.ENOUGH_COOLANT.NAME, BUILDING.STATUSITEMS.ENOUGH_COOLANT.TOOLTIP, "status_item_no_liquid_to_pump", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022, true, null);
				LiquidCooledRefinery.States.waitingForCoolantStatus.resolveStringCallback = delegate(string str, object obj)
				{
					LiquidCooledRefinery liquidCooledRefinery = (LiquidCooledRefinery)obj;
					return string.Format(str, liquidCooledRefinery.coolantTag.ProperName(), GameUtil.GetFormattedMass(liquidCooledRefinery.minCoolantMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				};
			}
			default_state = this.waiting_for_coolant;
			this.waiting_for_coolant.ToggleStatusItem(LiquidCooledRefinery.States.waitingForCoolantStatus, (LiquidCooledRefinery.StatesInstance smi) => smi.master).EventTransition(GameHashes.OnStorageChange, this.ready, (LiquidCooledRefinery.StatesInstance smi) => smi.master.HasEnoughCoolant()).ParamTransition<bool>(this.outputBlocked, this.output_blocked, GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.IsTrue);
			this.ready.EventTransition(GameHashes.OnStorageChange, this.waiting_for_coolant, (LiquidCooledRefinery.StatesInstance smi) => !smi.master.HasEnoughCoolant()).ParamTransition<bool>(this.outputBlocked, this.output_blocked, GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.IsTrue).Enter(delegate(LiquidCooledRefinery.StatesInstance smi)
			{
				smi.master.SetQueueDirty();
			});
			this.output_blocked.ToggleStatusItem(Db.Get().BuildingStatusItems.OutputPipeFull, null).ParamTransition<bool>(this.outputBlocked, this.waiting_for_coolant, GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.IsFalse);
		}

		// Token: 0x04005D54 RID: 23892
		public static StatusItem waitingForCoolantStatus;

		// Token: 0x04005D55 RID: 23893
		public StateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.BoolParameter outputBlocked;

		// Token: 0x04005D56 RID: 23894
		public GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.State waiting_for_coolant;

		// Token: 0x04005D57 RID: 23895
		public GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.State ready;

		// Token: 0x04005D58 RID: 23896
		public GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.State output_blocked;
	}
}
