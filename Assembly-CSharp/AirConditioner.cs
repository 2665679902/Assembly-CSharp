using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000570 RID: 1392
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/AirConditioner")]
public class AirConditioner : KMonoBehaviour, ISaveLoadable, IGameObjectEffectDescriptor, ISim200ms
{
	// Token: 0x1700019D RID: 413
	// (get) Token: 0x060021A1 RID: 8609 RVA: 0x000B70F3 File Offset: 0x000B52F3
	// (set) Token: 0x060021A2 RID: 8610 RVA: 0x000B70FB File Offset: 0x000B52FB
	public float lastEnvTemp { get; private set; }

	// Token: 0x1700019E RID: 414
	// (get) Token: 0x060021A3 RID: 8611 RVA: 0x000B7104 File Offset: 0x000B5304
	// (set) Token: 0x060021A4 RID: 8612 RVA: 0x000B710C File Offset: 0x000B530C
	public float lastGasTemp { get; private set; }

	// Token: 0x1700019F RID: 415
	// (get) Token: 0x060021A5 RID: 8613 RVA: 0x000B7115 File Offset: 0x000B5315
	public float TargetTemperature
	{
		get
		{
			return this.targetTemperature;
		}
	}

	// Token: 0x060021A6 RID: 8614 RVA: 0x000B711D File Offset: 0x000B531D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<AirConditioner>(-592767678, AirConditioner.OnOperationalChangedDelegate);
		base.Subscribe<AirConditioner>(824508782, AirConditioner.OnActiveChangedDelegate);
	}

	// Token: 0x060021A7 RID: 8615 RVA: 0x000B7148 File Offset: 0x000B5348
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameScheduler.Instance.Schedule("InsulationTutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Insulation, true);
		}, null, null);
		this.structureTemperature = GameComps.StructureTemperatures.GetHandle(base.gameObject);
		this.cooledAirOutputCell = this.building.GetUtilityOutputCell();
	}

	// Token: 0x060021A8 RID: 8616 RVA: 0x000B71B8 File Offset: 0x000B53B8
	public void Sim200ms(float dt)
	{
		if (this.operational != null && !this.operational.IsOperational)
		{
			this.operational.SetActive(false, false);
			return;
		}
		this.UpdateState(dt);
	}

	// Token: 0x060021A9 RID: 8617 RVA: 0x000B71EA File Offset: 0x000B53EA
	private static bool UpdateStateCb(int cell, object data)
	{
		AirConditioner airConditioner = data as AirConditioner;
		airConditioner.cellCount++;
		airConditioner.envTemp += Grid.Temperature[cell];
		return true;
	}

	// Token: 0x060021AA RID: 8618 RVA: 0x000B7218 File Offset: 0x000B5418
	private void UpdateState(float dt)
	{
		bool flag = this.consumer.IsSatisfied;
		this.envTemp = 0f;
		this.cellCount = 0;
		if (this.occupyArea != null && base.gameObject != null)
		{
			this.occupyArea.TestArea(Grid.PosToCell(base.gameObject), this, AirConditioner.UpdateStateCbDelegate);
			this.envTemp /= (float)this.cellCount;
		}
		this.lastEnvTemp = this.envTemp;
		List<GameObject> items = this.storage.items;
		for (int i = 0; i < items.Count; i++)
		{
			PrimaryElement component = items[i].GetComponent<PrimaryElement>();
			if (component.Mass > 0f && (!this.isLiquidConditioner || !component.Element.IsGas) && (this.isLiquidConditioner || !component.Element.IsLiquid))
			{
				flag = true;
				this.lastGasTemp = component.Temperature;
				float num = component.Temperature + this.temperatureDelta;
				if (num < 1f)
				{
					num = 1f;
					this.lowTempLag = Mathf.Min(this.lowTempLag + dt / 5f, 1f);
				}
				else
				{
					this.lowTempLag = Mathf.Min(this.lowTempLag - dt / 5f, 0f);
				}
				float num2 = (this.isLiquidConditioner ? Game.Instance.liquidConduitFlow : Game.Instance.gasConduitFlow).AddElement(this.cooledAirOutputCell, component.ElementID, component.Mass, num, component.DiseaseIdx, component.DiseaseCount);
				component.KeepZeroMassObject = true;
				float num3 = num2 / component.Mass;
				int num4 = (int)((float)component.DiseaseCount * num3);
				component.Mass -= num2;
				component.ModifyDiseaseCount(-num4, "AirConditioner.UpdateState");
				float num5 = (num - component.Temperature) * component.Element.specificHeatCapacity * num2;
				float num6 = ((this.lastSampleTime > 0f) ? (Time.time - this.lastSampleTime) : 1f);
				this.lastSampleTime = Time.time;
				GameComps.StructureTemperatures.ProduceEnergy(this.structureTemperature, -num5, BUILDING.STATUSITEMS.OPERATINGENERGY.PIPECONTENTS_TRANSFER, num6);
				break;
			}
		}
		if (Time.time - this.lastSampleTime > 2f)
		{
			GameComps.StructureTemperatures.ProduceEnergy(this.structureTemperature, 0f, BUILDING.STATUSITEMS.OPERATINGENERGY.PIPECONTENTS_TRANSFER, Time.time - this.lastSampleTime);
			this.lastSampleTime = Time.time;
		}
		this.operational.SetActive(flag, false);
		this.UpdateStatus();
	}

	// Token: 0x060021AB RID: 8619 RVA: 0x000B74BC File Offset: 0x000B56BC
	private void OnOperationalChanged(object data)
	{
		if (this.operational.IsOperational)
		{
			this.UpdateState(0f);
		}
	}

	// Token: 0x060021AC RID: 8620 RVA: 0x000B74D6 File Offset: 0x000B56D6
	private void OnActiveChanged(object data)
	{
		this.UpdateStatus();
	}

	// Token: 0x060021AD RID: 8621 RVA: 0x000B74E0 File Offset: 0x000B56E0
	private void UpdateStatus()
	{
		if (this.operational.IsActive)
		{
			if (this.lowTempLag >= 1f && !this.showingLowTemp)
			{
				this.statusHandle = (this.isLiquidConditioner ? this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.CoolingStalledColdLiquid, this) : this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.CoolingStalledColdGas, this));
				this.showingLowTemp = true;
				this.showingHotEnv = false;
				return;
			}
			if (this.lowTempLag <= 0f && (this.showingHotEnv || this.showingLowTemp))
			{
				this.statusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Cooling, null);
				this.showingLowTemp = false;
				this.showingHotEnv = false;
				return;
			}
			if (this.statusHandle == Guid.Empty)
			{
				this.statusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Cooling, null);
				this.showingLowTemp = false;
				this.showingHotEnv = false;
				return;
			}
		}
		else
		{
			this.statusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
		}
	}

	// Token: 0x060021AE RID: 8622 RVA: 0x000B7654 File Offset: 0x000B5854
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		string formattedTemperature = GameUtil.GetFormattedTemperature(this.temperatureDelta, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Relative, true, false);
		Element element = ElementLoader.FindElementByName(this.isLiquidConditioner ? "Water" : "Oxygen");
		float num;
		if (this.isLiquidConditioner)
		{
			num = Mathf.Abs(this.temperatureDelta * element.specificHeatCapacity * 10000f);
		}
		else
		{
			num = Mathf.Abs(this.temperatureDelta * element.specificHeatCapacity * 1000f);
		}
		float num2 = num * 1f;
		Descriptor descriptor = default(Descriptor);
		string text = string.Format(this.isLiquidConditioner ? UI.BUILDINGEFFECTS.HEATGENERATED_LIQUIDCONDITIONER : UI.BUILDINGEFFECTS.HEATGENERATED_AIRCONDITIONER, GameUtil.GetFormattedHeatEnergy(num2, GameUtil.HeatEnergyFormatterUnit.Automatic), GameUtil.GetFormattedTemperature(Mathf.Abs(this.temperatureDelta), GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Relative, true, false));
		string text2 = string.Format(this.isLiquidConditioner ? UI.BUILDINGEFFECTS.TOOLTIPS.HEATGENERATED_LIQUIDCONDITIONER : UI.BUILDINGEFFECTS.TOOLTIPS.HEATGENERATED_AIRCONDITIONER, GameUtil.GetFormattedHeatEnergy(num2, GameUtil.HeatEnergyFormatterUnit.Automatic), GameUtil.GetFormattedTemperature(Mathf.Abs(this.temperatureDelta), GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Relative, true, false));
		descriptor.SetupDescriptor(text, text2, Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		Descriptor descriptor2 = default(Descriptor);
		descriptor2.SetupDescriptor(string.Format(this.isLiquidConditioner ? UI.BUILDINGEFFECTS.LIQUIDCOOLING : UI.BUILDINGEFFECTS.GASCOOLING, formattedTemperature), string.Format(this.isLiquidConditioner ? UI.BUILDINGEFFECTS.TOOLTIPS.LIQUIDCOOLING : UI.BUILDINGEFFECTS.TOOLTIPS.GASCOOLING, formattedTemperature), Descriptor.DescriptorType.Effect);
		list.Add(descriptor2);
		return list;
	}

	// Token: 0x0400134F RID: 4943
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04001350 RID: 4944
	[MyCmpReq]
	protected Storage storage;

	// Token: 0x04001351 RID: 4945
	[MyCmpReq]
	protected Operational operational;

	// Token: 0x04001352 RID: 4946
	[MyCmpReq]
	private ConduitConsumer consumer;

	// Token: 0x04001353 RID: 4947
	[MyCmpReq]
	private BuildingComplete building;

	// Token: 0x04001354 RID: 4948
	[MyCmpGet]
	private OccupyArea occupyArea;

	// Token: 0x04001355 RID: 4949
	private HandleVector<int>.Handle structureTemperature;

	// Token: 0x04001356 RID: 4950
	public float temperatureDelta = -14f;

	// Token: 0x04001357 RID: 4951
	public float maxEnvironmentDelta = -50f;

	// Token: 0x04001358 RID: 4952
	private float lowTempLag;

	// Token: 0x04001359 RID: 4953
	private bool showingLowTemp;

	// Token: 0x0400135A RID: 4954
	public bool isLiquidConditioner;

	// Token: 0x0400135B RID: 4955
	private bool showingHotEnv;

	// Token: 0x0400135E RID: 4958
	private Guid statusHandle;

	// Token: 0x0400135F RID: 4959
	[Serialize]
	private float targetTemperature;

	// Token: 0x04001360 RID: 4960
	private int cooledAirOutputCell = -1;

	// Token: 0x04001361 RID: 4961
	private static readonly EventSystem.IntraObjectHandler<AirConditioner> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<AirConditioner>(delegate(AirConditioner component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x04001362 RID: 4962
	private static readonly EventSystem.IntraObjectHandler<AirConditioner> OnActiveChangedDelegate = new EventSystem.IntraObjectHandler<AirConditioner>(delegate(AirConditioner component, object data)
	{
		component.OnActiveChanged(data);
	});

	// Token: 0x04001363 RID: 4963
	private float lastSampleTime = -1f;

	// Token: 0x04001364 RID: 4964
	private float envTemp;

	// Token: 0x04001365 RID: 4965
	private int cellCount;

	// Token: 0x04001366 RID: 4966
	private static readonly Func<int, object, bool> UpdateStateCbDelegate = (int cell, object data) => AirConditioner.UpdateStateCb(cell, data);
}
