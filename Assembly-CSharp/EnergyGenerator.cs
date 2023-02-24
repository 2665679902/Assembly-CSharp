using System;
using System.Collections.Generic;
using System.Diagnostics;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000743 RID: 1859
[SerializationConfig(MemberSerialization.OptIn)]
public class EnergyGenerator : Generator, IGameObjectEffectDescriptor, ISingleSliderControl, ISliderControl
{
	// Token: 0x170003BF RID: 959
	// (get) Token: 0x06003334 RID: 13108 RVA: 0x00113E6B File Offset: 0x0011206B
	public string SliderTitleKey
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.MANUALDELIVERYGENERATORSIDESCREEN.TITLE";
		}
	}

	// Token: 0x170003C0 RID: 960
	// (get) Token: 0x06003335 RID: 13109 RVA: 0x00113E72 File Offset: 0x00112072
	public string SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.PERCENT;
		}
	}

	// Token: 0x06003336 RID: 13110 RVA: 0x00113E7E File Offset: 0x0011207E
	public int SliderDecimalPlaces(int index)
	{
		return 0;
	}

	// Token: 0x06003337 RID: 13111 RVA: 0x00113E81 File Offset: 0x00112081
	public float GetSliderMin(int index)
	{
		return 0f;
	}

	// Token: 0x06003338 RID: 13112 RVA: 0x00113E88 File Offset: 0x00112088
	public float GetSliderMax(int index)
	{
		return 100f;
	}

	// Token: 0x06003339 RID: 13113 RVA: 0x00113E8F File Offset: 0x0011208F
	public float GetSliderValue(int index)
	{
		return this.batteryRefillPercent * 100f;
	}

	// Token: 0x0600333A RID: 13114 RVA: 0x00113E9D File Offset: 0x0011209D
	public void SetSliderValue(float value, int index)
	{
		this.batteryRefillPercent = value / 100f;
	}

	// Token: 0x0600333B RID: 13115 RVA: 0x00113EAC File Offset: 0x001120AC
	string ISliderControl.GetSliderTooltip()
	{
		ManualDeliveryKG component = base.GetComponent<ManualDeliveryKG>();
		return string.Format(Strings.Get("STRINGS.UI.UISIDESCREENS.MANUALDELIVERYGENERATORSIDESCREEN.TOOLTIP"), component.RequestedItemTag.ProperName(), this.batteryRefillPercent * 100f);
	}

	// Token: 0x0600333C RID: 13116 RVA: 0x00113EF0 File Offset: 0x001120F0
	public string GetSliderTooltipKey(int index)
	{
		return "STRINGS.UI.UISIDESCREENS.MANUALDELIVERYGENERATORSIDESCREEN.TOOLTIP";
	}

	// Token: 0x0600333D RID: 13117 RVA: 0x00113EF8 File Offset: 0x001120F8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		EnergyGenerator.EnsureStatusItemAvailable();
		base.Subscribe<EnergyGenerator>(824508782, EnergyGenerator.OnActiveChangedDelegate);
		if (!this.ignoreBatteryRefillPercent)
		{
			base.gameObject.AddOrGet<CopyBuildingSettings>();
			base.Subscribe<EnergyGenerator>(-905833192, EnergyGenerator.OnCopySettingsDelegate);
		}
	}

	// Token: 0x0600333E RID: 13118 RVA: 0x00113F48 File Offset: 0x00112148
	private void OnCopySettings(object data)
	{
		EnergyGenerator component = ((GameObject)data).GetComponent<EnergyGenerator>();
		if (component != null)
		{
			this.batteryRefillPercent = component.batteryRefillPercent;
		}
	}

	// Token: 0x0600333F RID: 13119 RVA: 0x00113F78 File Offset: 0x00112178
	protected void OnActiveChanged(object data)
	{
		StatusItem statusItem = (((Operational)data).IsActive ? Db.Get().BuildingStatusItems.Wattage : Db.Get().BuildingStatusItems.GeneratorOffline);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, this);
	}

	// Token: 0x06003340 RID: 13120 RVA: 0x00113FD0 File Offset: 0x001121D0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.hasMeter)
		{
			this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", this.meterOffset, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
		}
	}

	// Token: 0x06003341 RID: 13121 RVA: 0x00114034 File Offset: 0x00112234
	private bool IsConvertible(float dt)
	{
		bool flag = true;
		foreach (EnergyGenerator.InputItem inputItem in this.formula.inputs)
		{
			float massAvailable = this.storage.GetMassAvailable(inputItem.tag);
			float num = inputItem.consumptionRate * dt;
			flag = flag && massAvailable >= num;
			if (!flag)
			{
				break;
			}
		}
		return flag;
	}

	// Token: 0x06003342 RID: 13122 RVA: 0x00114098 File Offset: 0x00112298
	public override void EnergySim200ms(float dt)
	{
		base.EnergySim200ms(dt);
		if (this.hasMeter)
		{
			EnergyGenerator.InputItem inputItem = this.formula.inputs[0];
			float num = this.storage.GetMassAvailable(inputItem.tag) / inputItem.maxStoredMass;
			this.meter.SetPositionPercent(num);
		}
		ushort circuitID = base.CircuitID;
		this.operational.SetFlag(Generator.wireConnectedFlag, circuitID != ushort.MaxValue);
		bool flag = false;
		if (this.operational.IsOperational)
		{
			bool flag2 = false;
			List<Battery> batteriesOnCircuit = Game.Instance.circuitManager.GetBatteriesOnCircuit(circuitID);
			if (!this.ignoreBatteryRefillPercent && batteriesOnCircuit.Count > 0)
			{
				using (List<Battery>.Enumerator enumerator = batteriesOnCircuit.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Battery battery = enumerator.Current;
						if (this.batteryRefillPercent <= 0f && battery.PercentFull <= 0f)
						{
							flag2 = true;
							break;
						}
						if (battery.PercentFull < this.batteryRefillPercent)
						{
							flag2 = true;
							break;
						}
					}
					goto IL_105;
				}
			}
			flag2 = true;
			IL_105:
			if (!this.ignoreBatteryRefillPercent)
			{
				this.selectable.ToggleStatusItem(EnergyGenerator.batteriesSufficientlyFull, !flag2, null);
			}
			if (this.delivery != null)
			{
				this.delivery.Pause(!flag2, "Circuit has sufficient energy");
			}
			if (this.formula.inputs != null)
			{
				bool flag3 = this.IsConvertible(dt);
				this.selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.NeedResourceMass, !flag3, this.formula);
				if (flag3)
				{
					foreach (EnergyGenerator.InputItem inputItem2 in this.formula.inputs)
					{
						float num2 = inputItem2.consumptionRate * dt;
						this.storage.ConsumeIgnoringDisease(inputItem2.tag, num2);
					}
					PrimaryElement component = base.GetComponent<PrimaryElement>();
					foreach (EnergyGenerator.OutputItem outputItem in this.formula.outputs)
					{
						this.Emit(outputItem, dt, component);
					}
					base.GenerateJoules(base.WattageRating * dt, false);
					this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Power, Db.Get().BuildingStatusItems.Wattage, this);
					flag = true;
				}
			}
		}
		this.operational.SetActive(flag, false);
	}

	// Token: 0x06003343 RID: 13123 RVA: 0x00114318 File Offset: 0x00112518
	public List<Descriptor> RequirementDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.formula.inputs == null || this.formula.inputs.Length == 0)
		{
			return list;
		}
		for (int i = 0; i < this.formula.inputs.Length; i++)
		{
			EnergyGenerator.InputItem inputItem = this.formula.inputs[i];
			string text = inputItem.tag.ProperName();
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMED, text, GameUtil.GetFormattedMass(inputItem.consumptionRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMED, text, GameUtil.GetFormattedMass(inputItem.consumptionRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), Descriptor.DescriptorType.Requirement);
			list.Add(descriptor);
		}
		return list;
	}

	// Token: 0x06003344 RID: 13124 RVA: 0x001143E4 File Offset: 0x001125E4
	public List<Descriptor> EffectDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.formula.outputs == null || this.formula.outputs.Length == 0)
		{
			return list;
		}
		for (int i = 0; i < this.formula.outputs.Length; i++)
		{
			EnergyGenerator.OutputItem outputItem = this.formula.outputs[i];
			string text = ElementLoader.FindElementByHash(outputItem.element).tag.ProperName();
			Descriptor descriptor = default(Descriptor);
			if (outputItem.minTemperature > 0f)
			{
				descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTEMITTED_MINORENTITYTEMP, text, GameUtil.GetFormattedMass(outputItem.creationRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), GameUtil.GetFormattedTemperature(outputItem.minTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTEMITTED_MINORENTITYTEMP, text, GameUtil.GetFormattedMass(outputItem.creationRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), GameUtil.GetFormattedTemperature(outputItem.minTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), Descriptor.DescriptorType.Effect);
			}
			else
			{
				descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTEMITTED_ENTITYTEMP, text, GameUtil.GetFormattedMass(outputItem.creationRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTEMITTED_ENTITYTEMP, text, GameUtil.GetFormattedMass(outputItem.creationRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Effect);
			}
			list.Add(descriptor);
		}
		return list;
	}

	// Token: 0x06003345 RID: 13125 RVA: 0x00114534 File Offset: 0x00112734
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in this.RequirementDescriptors())
		{
			list.Add(descriptor);
		}
		foreach (Descriptor descriptor2 in this.EffectDescriptors())
		{
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x170003C1 RID: 961
	// (get) Token: 0x06003346 RID: 13126 RVA: 0x001145D0 File Offset: 0x001127D0
	public static StatusItem BatteriesSufficientlyFull
	{
		get
		{
			return EnergyGenerator.batteriesSufficientlyFull;
		}
	}

	// Token: 0x06003347 RID: 13127 RVA: 0x001145D8 File Offset: 0x001127D8
	public static void EnsureStatusItemAvailable()
	{
		if (EnergyGenerator.batteriesSufficientlyFull == null)
		{
			EnergyGenerator.batteriesSufficientlyFull = new StatusItem("BatteriesSufficientlyFull", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
		}
	}

	// Token: 0x06003348 RID: 13128 RVA: 0x00114614 File Offset: 0x00112814
	public static EnergyGenerator.Formula CreateSimpleFormula(Tag input_element, float input_mass_rate, float max_stored_input_mass, SimHashes output_element = SimHashes.Void, float output_mass_rate = 0f, bool store_output_mass = true, CellOffset output_offset = default(CellOffset), float min_output_temperature = 0f)
	{
		EnergyGenerator.Formula formula = default(EnergyGenerator.Formula);
		formula.inputs = new EnergyGenerator.InputItem[]
		{
			new EnergyGenerator.InputItem(input_element, input_mass_rate, max_stored_input_mass)
		};
		if (output_element != SimHashes.Void)
		{
			formula.outputs = new EnergyGenerator.OutputItem[]
			{
				new EnergyGenerator.OutputItem(output_element, output_mass_rate, store_output_mass, output_offset, min_output_temperature)
			};
		}
		else
		{
			formula.outputs = null;
		}
		return formula;
	}

	// Token: 0x06003349 RID: 13129 RVA: 0x0011467C File Offset: 0x0011287C
	private void Emit(EnergyGenerator.OutputItem output, float dt, PrimaryElement root_pe)
	{
		Element element = ElementLoader.FindElementByHash(output.element);
		float num = output.creationRate * dt;
		if (output.store)
		{
			if (element.IsGas)
			{
				this.storage.AddGasChunk(output.element, num, root_pe.Temperature, byte.MaxValue, 0, true, true);
				return;
			}
			if (element.IsLiquid)
			{
				this.storage.AddLiquid(output.element, num, root_pe.Temperature, byte.MaxValue, 0, true, true);
				return;
			}
			GameObject gameObject = element.substance.SpawnResource(base.transform.GetPosition(), num, root_pe.Temperature, byte.MaxValue, 0, false, false, false);
			this.storage.Store(gameObject, true, false, true, false);
			return;
		}
		else
		{
			int num2 = Grid.OffsetCell(Grid.PosToCell(base.transform.GetPosition()), output.emitOffset);
			float num3 = Mathf.Max(root_pe.Temperature, output.minTemperature);
			if (element.IsGas)
			{
				SimMessages.ModifyMass(num2, num, byte.MaxValue, 0, CellEventLogger.Instance.EnergyGeneratorModifyMass, num3, output.element);
				return;
			}
			if (element.IsLiquid)
			{
				ushort elementIndex = ElementLoader.GetElementIndex(output.element);
				FallingWater.instance.AddParticle(num2, elementIndex, num, num3, byte.MaxValue, 0, true, false, false, false);
				return;
			}
			element.substance.SpawnResource(Grid.CellToPosCCC(num2, Grid.SceneLayer.Front), num, num3, byte.MaxValue, 0, true, false, false);
			return;
		}
	}

	// Token: 0x04001F76 RID: 8054
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04001F77 RID: 8055
	[MyCmpGet]
	private ManualDeliveryKG delivery;

	// Token: 0x04001F78 RID: 8056
	[SerializeField]
	[Serialize]
	private float batteryRefillPercent = 0.5f;

	// Token: 0x04001F79 RID: 8057
	public bool ignoreBatteryRefillPercent;

	// Token: 0x04001F7A RID: 8058
	public bool hasMeter = true;

	// Token: 0x04001F7B RID: 8059
	private static StatusItem batteriesSufficientlyFull;

	// Token: 0x04001F7C RID: 8060
	public Meter.Offset meterOffset;

	// Token: 0x04001F7D RID: 8061
	[SerializeField]
	public EnergyGenerator.Formula formula;

	// Token: 0x04001F7E RID: 8062
	private MeterController meter;

	// Token: 0x04001F7F RID: 8063
	private static readonly EventSystem.IntraObjectHandler<EnergyGenerator> OnActiveChangedDelegate = new EventSystem.IntraObjectHandler<EnergyGenerator>(delegate(EnergyGenerator component, object data)
	{
		component.OnActiveChanged(data);
	});

	// Token: 0x04001F80 RID: 8064
	private static readonly EventSystem.IntraObjectHandler<EnergyGenerator> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<EnergyGenerator>(delegate(EnergyGenerator component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x0200144A RID: 5194
	[DebuggerDisplay("{tag} -{consumptionRate} kg/s")]
	[Serializable]
	public struct InputItem
	{
		// Token: 0x060080BF RID: 32959 RVA: 0x002DFCA4 File Offset: 0x002DDEA4
		public InputItem(Tag tag, float consumption_rate, float max_stored_mass)
		{
			this.tag = tag;
			this.consumptionRate = consumption_rate;
			this.maxStoredMass = max_stored_mass;
		}

		// Token: 0x0400630A RID: 25354
		public Tag tag;

		// Token: 0x0400630B RID: 25355
		public float consumptionRate;

		// Token: 0x0400630C RID: 25356
		public float maxStoredMass;
	}

	// Token: 0x0200144B RID: 5195
	[DebuggerDisplay("{element} {creationRate} kg/s")]
	[Serializable]
	public struct OutputItem
	{
		// Token: 0x060080C0 RID: 32960 RVA: 0x002DFCBB File Offset: 0x002DDEBB
		public OutputItem(SimHashes element, float creation_rate, bool store, float min_temperature = 0f)
		{
			this = new EnergyGenerator.OutputItem(element, creation_rate, store, CellOffset.none, min_temperature);
		}

		// Token: 0x060080C1 RID: 32961 RVA: 0x002DFCCD File Offset: 0x002DDECD
		public OutputItem(SimHashes element, float creation_rate, bool store, CellOffset emit_offset, float min_temperature = 0f)
		{
			this.element = element;
			this.creationRate = creation_rate;
			this.store = store;
			this.emitOffset = emit_offset;
			this.minTemperature = min_temperature;
		}

		// Token: 0x0400630D RID: 25357
		public SimHashes element;

		// Token: 0x0400630E RID: 25358
		public float creationRate;

		// Token: 0x0400630F RID: 25359
		public bool store;

		// Token: 0x04006310 RID: 25360
		public CellOffset emitOffset;

		// Token: 0x04006311 RID: 25361
		public float minTemperature;
	}

	// Token: 0x0200144C RID: 5196
	[Serializable]
	public struct Formula
	{
		// Token: 0x04006312 RID: 25362
		public EnergyGenerator.InputItem[] inputs;

		// Token: 0x04006313 RID: 25363
		public EnergyGenerator.OutputItem[] outputs;

		// Token: 0x04006314 RID: 25364
		public Tag meterTag;
	}
}
