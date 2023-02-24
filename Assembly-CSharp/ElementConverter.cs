using System;
using System.Collections.Generic;
using System.Diagnostics;
using Klei;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200073B RID: 1851
[SerializationConfig(MemberSerialization.OptIn)]
public class ElementConverter : StateMachineComponent<ElementConverter.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x060032C2 RID: 12994 RVA: 0x00111CCF File Offset: 0x0010FECF
	public void SetWorkSpeedMultiplier(float speed)
	{
		this.workSpeedMultiplier = speed;
	}

	// Token: 0x060032C3 RID: 12995 RVA: 0x00111CD8 File Offset: 0x0010FED8
	public void SetConsumedElementActive(Tag elementId, bool active)
	{
		int i = 0;
		while (i < this.consumedElements.Length)
		{
			if (!(this.consumedElements[i].Tag != elementId))
			{
				this.consumedElements[i].IsActive = active;
				if (!this.ShowInUI)
				{
					break;
				}
				ElementConverter.ConsumedElement consumedElement = this.consumedElements[i];
				if (active)
				{
					base.smi.AddStatusItem<ElementConverter.ConsumedElement, Tag>(consumedElement, consumedElement.Tag, ElementConverter.ElementConverterInput, this.consumedElementStatusHandles);
					return;
				}
				base.smi.RemoveStatusItem<Tag>(consumedElement.Tag, this.consumedElementStatusHandles);
				return;
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x060032C4 RID: 12996 RVA: 0x00111D74 File Offset: 0x0010FF74
	public void SetOutputElementActive(SimHashes element, bool active)
	{
		int i = 0;
		while (i < this.outputElements.Length)
		{
			if (this.outputElements[i].elementHash == element)
			{
				this.outputElements[i].IsActive = active;
				ElementConverter.OutputElement outputElement = this.outputElements[i];
				if (active)
				{
					base.smi.AddStatusItem<ElementConverter.OutputElement, SimHashes>(outputElement, outputElement.elementHash, ElementConverter.ElementConverterOutput, this.outputElementStatusHandles);
					return;
				}
				base.smi.RemoveStatusItem<SimHashes>(outputElement.elementHash, this.outputElementStatusHandles);
				return;
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x060032C5 RID: 12997 RVA: 0x00111E00 File Offset: 0x00110000
	public void SetStorage(Storage storage)
	{
		this.storage = storage;
	}

	// Token: 0x170003A8 RID: 936
	// (get) Token: 0x060032C6 RID: 12998 RVA: 0x00111E09 File Offset: 0x00110009
	// (set) Token: 0x060032C7 RID: 12999 RVA: 0x00111E11 File Offset: 0x00110011
	public float OutputMultiplier
	{
		get
		{
			return this.outputMultiplier;
		}
		set
		{
			this.outputMultiplier = value;
		}
	}

	// Token: 0x170003A9 RID: 937
	// (get) Token: 0x060032C8 RID: 13000 RVA: 0x00111E1A File Offset: 0x0011001A
	public float AverageConvertRate
	{
		get
		{
			return Game.Instance.accumulators.GetAverageRate(this.outputElements[0].accumulator);
		}
	}

	// Token: 0x060032C9 RID: 13001 RVA: 0x00111E3C File Offset: 0x0011003C
	public bool HasEnoughMass(Tag tag, bool includeInactive = false)
	{
		bool flag = false;
		List<GameObject> items = this.storage.items;
		foreach (ElementConverter.ConsumedElement consumedElement in this.consumedElements)
		{
			if (!(tag != consumedElement.Tag) && (includeInactive || consumedElement.IsActive))
			{
				float num = 0f;
				for (int j = 0; j < items.Count; j++)
				{
					GameObject gameObject = items[j];
					if (!(gameObject == null) && gameObject.HasTag(tag))
					{
						num += gameObject.GetComponent<PrimaryElement>().Mass;
					}
				}
				flag = num >= consumedElement.MassConsumptionRate;
				break;
			}
		}
		return flag;
	}

	// Token: 0x060032CA RID: 13002 RVA: 0x00111EF4 File Offset: 0x001100F4
	public bool HasEnoughMassToStartConverting(bool includeInactive = false)
	{
		float speedMultiplier = this.GetSpeedMultiplier();
		float num = 1f * speedMultiplier;
		bool flag = includeInactive || this.consumedElements.Length == 0;
		bool flag2 = true;
		List<GameObject> items = this.storage.items;
		for (int i = 0; i < this.consumedElements.Length; i++)
		{
			ElementConverter.ConsumedElement consumedElement = this.consumedElements[i];
			flag |= consumedElement.IsActive;
			if (includeInactive || consumedElement.IsActive)
			{
				float num2 = 0f;
				for (int j = 0; j < items.Count; j++)
				{
					GameObject gameObject = items[j];
					if (!(gameObject == null) && gameObject.HasTag(consumedElement.Tag))
					{
						num2 += gameObject.GetComponent<PrimaryElement>().Mass;
					}
				}
				if (num2 < consumedElement.MassConsumptionRate * num)
				{
					flag2 = false;
					break;
				}
			}
		}
		return flag && flag2;
	}

	// Token: 0x060032CB RID: 13003 RVA: 0x00111FDC File Offset: 0x001101DC
	public bool CanConvertAtAll()
	{
		bool flag = this.consumedElements.Length == 0;
		bool flag2 = true;
		List<GameObject> items = this.storage.items;
		for (int i = 0; i < this.consumedElements.Length; i++)
		{
			ElementConverter.ConsumedElement consumedElement = this.consumedElements[i];
			flag |= consumedElement.IsActive;
			if (consumedElement.IsActive)
			{
				bool flag3 = false;
				for (int j = 0; j < items.Count; j++)
				{
					GameObject gameObject = items[j];
					if (!(gameObject == null) && gameObject.HasTag(consumedElement.Tag) && gameObject.GetComponent<PrimaryElement>().Mass > 0f)
					{
						flag3 = true;
						break;
					}
				}
				if (!flag3)
				{
					flag2 = false;
					break;
				}
			}
		}
		return flag && flag2;
	}

	// Token: 0x060032CC RID: 13004 RVA: 0x0011209B File Offset: 0x0011029B
	private float GetSpeedMultiplier()
	{
		return this.machinerySpeedAttribute.GetTotalValue() * this.workSpeedMultiplier;
	}

	// Token: 0x060032CD RID: 13005 RVA: 0x001120B0 File Offset: 0x001102B0
	private void ConvertMass()
	{
		float speedMultiplier = this.GetSpeedMultiplier();
		float num = 1f * speedMultiplier;
		bool flag = this.consumedElements.Length == 0;
		float num2 = 1f;
		for (int i = 0; i < this.consumedElements.Length; i++)
		{
			ElementConverter.ConsumedElement consumedElement = this.consumedElements[i];
			flag |= consumedElement.IsActive;
			if (consumedElement.IsActive)
			{
				float num3 = consumedElement.MassConsumptionRate * num * num2;
				if (num3 <= 0f)
				{
					num2 = 0f;
					break;
				}
				float num4 = 0f;
				for (int j = 0; j < this.storage.items.Count; j++)
				{
					GameObject gameObject = this.storage.items[j];
					if (!(gameObject == null) && gameObject.HasTag(consumedElement.Tag))
					{
						PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
						float num5 = Mathf.Min(num3, component.Mass);
						num4 += num5 / num3;
					}
				}
				num2 = Mathf.Min(num2, num4);
			}
		}
		if (!flag || num2 <= 0f)
		{
			return;
		}
		SimUtil.DiseaseInfo diseaseInfo = SimUtil.DiseaseInfo.Invalid;
		diseaseInfo.idx = byte.MaxValue;
		diseaseInfo.count = 0;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		for (int k = 0; k < this.consumedElements.Length; k++)
		{
			ElementConverter.ConsumedElement consumedElement2 = this.consumedElements[k];
			if (consumedElement2.IsActive)
			{
				float num9 = consumedElement2.MassConsumptionRate * num * num2;
				Game.Instance.accumulators.Accumulate(consumedElement2.Accumulator, num9);
				for (int l = 0; l < this.storage.items.Count; l++)
				{
					GameObject gameObject2 = this.storage.items[l];
					if (!(gameObject2 == null))
					{
						if (gameObject2.HasTag(consumedElement2.Tag))
						{
							PrimaryElement component2 = gameObject2.GetComponent<PrimaryElement>();
							component2.KeepZeroMassObject = true;
							float num10 = Mathf.Min(num9, component2.Mass);
							int num11 = (int)(num10 / component2.Mass * (float)component2.DiseaseCount);
							float num12 = num10 * component2.Element.specificHeatCapacity;
							num8 += num12;
							num7 += num12 * component2.Temperature;
							component2.Mass -= num10;
							component2.ModifyDiseaseCount(-num11, "ElementConverter.ConvertMass");
							num6 += num10;
							diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(diseaseInfo.idx, diseaseInfo.count, component2.DiseaseIdx, num11);
							num9 -= num10;
							if (num9 <= 0f)
							{
								break;
							}
						}
						if (num9 <= 0f)
						{
							global::Debug.Assert(num9 <= 0f);
						}
					}
				}
			}
		}
		float num13 = ((num8 > 0f) ? (num7 / num8) : 0f);
		if (this.onConvertMass != null && num6 > 0f)
		{
			this.onConvertMass(num6);
		}
		for (int m = 0; m < this.outputElements.Length; m++)
		{
			ElementConverter.OutputElement outputElement = this.outputElements[m];
			if (outputElement.IsActive)
			{
				SimUtil.DiseaseInfo diseaseInfo2 = diseaseInfo;
				if (this.totalDiseaseWeight <= 0f)
				{
					diseaseInfo2.idx = byte.MaxValue;
					diseaseInfo2.count = 0;
				}
				else
				{
					float num14 = outputElement.diseaseWeight / this.totalDiseaseWeight;
					diseaseInfo2.count = (int)((float)diseaseInfo2.count * num14);
				}
				if (outputElement.addedDiseaseIdx != 255)
				{
					diseaseInfo2 = SimUtil.CalculateFinalDiseaseInfo(diseaseInfo2, new SimUtil.DiseaseInfo
					{
						idx = outputElement.addedDiseaseIdx,
						count = outputElement.addedDiseaseCount
					});
				}
				float num15 = outputElement.massGenerationRate * this.OutputMultiplier * num * num2;
				Game.Instance.accumulators.Accumulate(outputElement.accumulator, num15);
				float num16;
				if (outputElement.useEntityTemperature || (num13 == 0f && outputElement.minOutputTemperature == 0f))
				{
					num16 = base.GetComponent<PrimaryElement>().Temperature;
				}
				else
				{
					num16 = Mathf.Max(outputElement.minOutputTemperature, num13);
				}
				Element element = ElementLoader.FindElementByHash(outputElement.elementHash);
				if (outputElement.storeOutput)
				{
					PrimaryElement primaryElement = this.storage.AddToPrimaryElement(outputElement.elementHash, num15, num16);
					if (primaryElement == null)
					{
						if (element.IsGas)
						{
							this.storage.AddGasChunk(outputElement.elementHash, num15, num16, diseaseInfo2.idx, diseaseInfo2.count, true, true);
						}
						else if (element.IsLiquid)
						{
							this.storage.AddLiquid(outputElement.elementHash, num15, num16, diseaseInfo2.idx, diseaseInfo2.count, true, true);
						}
						else
						{
							GameObject gameObject3 = element.substance.SpawnResource(base.transform.GetPosition(), num15, num16, diseaseInfo2.idx, diseaseInfo2.count, true, false, false);
							this.storage.Store(gameObject3, true, false, true, false);
						}
					}
					else
					{
						primaryElement.AddDisease(diseaseInfo2.idx, diseaseInfo2.count, "ElementConverter.ConvertMass");
					}
				}
				else
				{
					Vector3 vector = new Vector3(base.transform.GetPosition().x + outputElement.outputElementOffset.x, base.transform.GetPosition().y + outputElement.outputElementOffset.y, 0f);
					int num17 = Grid.PosToCell(vector);
					if (element.IsLiquid)
					{
						FallingWater.instance.AddParticle(num17, element.idx, num15, num16, diseaseInfo2.idx, diseaseInfo2.count, true, false, false, false);
					}
					else if (element.IsSolid)
					{
						element.substance.SpawnResource(vector, num15, num16, diseaseInfo2.idx, diseaseInfo2.count, false, false, false);
					}
					else
					{
						SimMessages.AddRemoveSubstance(num17, outputElement.elementHash, CellEventLogger.Instance.OxygenModifierSimUpdate, num15, num16, diseaseInfo2.idx, diseaseInfo2.count, true, -1);
					}
				}
				if (outputElement.elementHash == SimHashes.Oxygen || outputElement.elementHash == SimHashes.ContaminatedOxygen)
				{
					ReportManager.Instance.ReportValue(ReportManager.ReportType.OxygenCreated, num15, base.gameObject.GetProperName(), null);
				}
			}
		}
		this.storage.Trigger(-1697596308, base.gameObject);
	}

	// Token: 0x060032CE RID: 13006 RVA: 0x0011271C File Offset: 0x0011091C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Attributes attributes = base.gameObject.GetAttributes();
		this.machinerySpeedAttribute = attributes.Add(Db.Get().Attributes.MachinerySpeed);
		if (ElementConverter.ElementConverterInput == null)
		{
			ElementConverter.ElementConverterInput = new StatusItem("ElementConverterInput", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, true, OverlayModes.None.ID, true, 129022, null).SetResolveStringCallback(delegate(string str, object data)
			{
				ElementConverter.ConsumedElement consumedElement = (ElementConverter.ConsumedElement)data;
				str = str.Replace("{ElementTypes}", consumedElement.Name);
				str = str.Replace("{FlowRate}", GameUtil.GetFormattedByTag(consumedElement.Tag, consumedElement.Rate, GameUtil.TimeSlice.PerSecond));
				return str;
			});
		}
		if (ElementConverter.ElementConverterOutput == null)
		{
			ElementConverter.ElementConverterOutput = new StatusItem("ElementConverterOutput", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, true, OverlayModes.None.ID, true, 129022, null).SetResolveStringCallback(delegate(string str, object data)
			{
				ElementConverter.OutputElement outputElement = (ElementConverter.OutputElement)data;
				str = str.Replace("{ElementTypes}", outputElement.Name);
				str = str.Replace("{FlowRate}", GameUtil.GetFormattedMass(outputElement.Rate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			});
		}
	}

	// Token: 0x060032CF RID: 13007 RVA: 0x001127FC File Offset: 0x001109FC
	public void SetAllConsumedActive(bool active)
	{
		for (int i = 0; i < this.consumedElements.Length; i++)
		{
			this.consumedElements[i].IsActive = active;
		}
		base.smi.sm.canConvert.Set(active, base.smi, false);
	}

	// Token: 0x060032D0 RID: 13008 RVA: 0x0011284C File Offset: 0x00110A4C
	public void SetConsumedActive(Tag id, bool active)
	{
		bool flag = this.consumedElements.Length == 0;
		for (int i = 0; i < this.consumedElements.Length; i++)
		{
			ref ElementConverter.ConsumedElement ptr = ref this.consumedElements[i];
			if (ptr.Tag == id)
			{
				ptr.IsActive = active;
				if (active)
				{
					flag = true;
					break;
				}
			}
			flag |= ptr.IsActive;
		}
		base.smi.sm.canConvert.Set(flag, base.smi, false);
	}

	// Token: 0x060032D1 RID: 13009 RVA: 0x001128C8 File Offset: 0x00110AC8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		for (int i = 0; i < this.consumedElements.Length; i++)
		{
			this.consumedElements[i].Accumulator = Game.Instance.accumulators.Add("ElementsConsumed", this);
		}
		this.totalDiseaseWeight = 0f;
		for (int j = 0; j < this.outputElements.Length; j++)
		{
			this.outputElements[j].accumulator = Game.Instance.accumulators.Add("OutputElements", this);
			this.totalDiseaseWeight += this.outputElements[j].diseaseWeight;
		}
		base.smi.StartSM();
	}

	// Token: 0x060032D2 RID: 13010 RVA: 0x00112984 File Offset: 0x00110B84
	protected override void OnCleanUp()
	{
		for (int i = 0; i < this.consumedElements.Length; i++)
		{
			Game.Instance.accumulators.Remove(this.consumedElements[i].Accumulator);
		}
		for (int j = 0; j < this.outputElements.Length; j++)
		{
			Game.Instance.accumulators.Remove(this.outputElements[j].accumulator);
		}
		base.OnCleanUp();
	}

	// Token: 0x060032D3 RID: 13011 RVA: 0x00112A00 File Offset: 0x00110C00
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (!this.showDescriptors)
		{
			return list;
		}
		if (this.consumedElements != null)
		{
			foreach (ElementConverter.ConsumedElement consumedElement in this.consumedElements)
			{
				if (consumedElement.IsActive)
				{
					Descriptor descriptor = default(Descriptor);
					descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMED, consumedElement.Name, GameUtil.GetFormattedMass(consumedElement.MassConsumptionRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMED, consumedElement.Name, GameUtil.GetFormattedMass(consumedElement.MassConsumptionRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), Descriptor.DescriptorType.Requirement);
					list.Add(descriptor);
				}
			}
		}
		if (this.outputElements != null)
		{
			foreach (ElementConverter.OutputElement outputElement in this.outputElements)
			{
				if (outputElement.IsActive)
				{
					LocString locString = UI.BUILDINGEFFECTS.ELEMENTEMITTED_INPUTTEMP;
					LocString locString2 = UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTEMITTED_INPUTTEMP;
					if (outputElement.useEntityTemperature)
					{
						locString = UI.BUILDINGEFFECTS.ELEMENTEMITTED_ENTITYTEMP;
						locString2 = UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTEMITTED_ENTITYTEMP;
					}
					else if (outputElement.minOutputTemperature > 0f)
					{
						locString = UI.BUILDINGEFFECTS.ELEMENTEMITTED_MINTEMP;
						locString2 = UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTEMITTED_MINTEMP;
					}
					Descriptor descriptor2 = new Descriptor(string.Format(locString, outputElement.Name, GameUtil.GetFormattedMass(outputElement.massGenerationRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}"), GameUtil.GetFormattedTemperature(outputElement.minOutputTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), string.Format(locString2, outputElement.Name, GameUtil.GetFormattedMass(outputElement.massGenerationRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}"), GameUtil.GetFormattedTemperature(outputElement.minOutputTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), Descriptor.DescriptorType.Effect, false);
					list.Add(descriptor2);
				}
			}
		}
		return list;
	}

	// Token: 0x04001F39 RID: 7993
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001F3A RID: 7994
	[MyCmpReq]
	private Storage storage;

	// Token: 0x04001F3B RID: 7995
	public Action<float> onConvertMass;

	// Token: 0x04001F3C RID: 7996
	private float totalDiseaseWeight = float.MaxValue;

	// Token: 0x04001F3D RID: 7997
	public Operational.State OperationalRequirement = Operational.State.Active;

	// Token: 0x04001F3E RID: 7998
	private AttributeInstance machinerySpeedAttribute;

	// Token: 0x04001F3F RID: 7999
	private float workSpeedMultiplier = 1f;

	// Token: 0x04001F40 RID: 8000
	public bool showDescriptors = true;

	// Token: 0x04001F41 RID: 8001
	private const float BASE_INTERVAL = 1f;

	// Token: 0x04001F42 RID: 8002
	public ElementConverter.ConsumedElement[] consumedElements;

	// Token: 0x04001F43 RID: 8003
	public ElementConverter.OutputElement[] outputElements;

	// Token: 0x04001F44 RID: 8004
	public bool ShowInUI = true;

	// Token: 0x04001F45 RID: 8005
	private float outputMultiplier = 1f;

	// Token: 0x04001F46 RID: 8006
	private Dictionary<Tag, Guid> consumedElementStatusHandles = new Dictionary<Tag, Guid>();

	// Token: 0x04001F47 RID: 8007
	private Dictionary<SimHashes, Guid> outputElementStatusHandles = new Dictionary<SimHashes, Guid>();

	// Token: 0x04001F48 RID: 8008
	private static StatusItem ElementConverterInput;

	// Token: 0x04001F49 RID: 8009
	private static StatusItem ElementConverterOutput;

	// Token: 0x02001443 RID: 5187
	[DebuggerDisplay("{tag} {massConsumptionRate}")]
	[Serializable]
	public struct ConsumedElement
	{
		// Token: 0x060080A4 RID: 32932 RVA: 0x002DF695 File Offset: 0x002DD895
		public ConsumedElement(Tag tag, float kgPerSecond, bool isActive = true)
		{
			this.Tag = tag;
			this.MassConsumptionRate = kgPerSecond;
			this.IsActive = isActive;
			this.Accumulator = HandleVector<int>.InvalidHandle;
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x060080A5 RID: 32933 RVA: 0x002DF6B7 File Offset: 0x002DD8B7
		public string Name
		{
			get
			{
				return this.Tag.ProperName();
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060080A6 RID: 32934 RVA: 0x002DF6C4 File Offset: 0x002DD8C4
		public float Rate
		{
			get
			{
				return Game.Instance.accumulators.GetAverageRate(this.Accumulator);
			}
		}

		// Token: 0x040062F2 RID: 25330
		public Tag Tag;

		// Token: 0x040062F3 RID: 25331
		public float MassConsumptionRate;

		// Token: 0x040062F4 RID: 25332
		public bool IsActive;

		// Token: 0x040062F5 RID: 25333
		public HandleVector<int>.Handle Accumulator;
	}

	// Token: 0x02001444 RID: 5188
	[Serializable]
	public struct OutputElement
	{
		// Token: 0x060080A7 RID: 32935 RVA: 0x002DF6DC File Offset: 0x002DD8DC
		public OutputElement(float kgPerSecond, SimHashes element, float minOutputTemperature, bool useEntityTemperature = false, bool storeOutput = false, float outputElementOffsetx = 0f, float outputElementOffsety = 0.5f, float diseaseWeight = 1f, byte addedDiseaseIdx = 255, int addedDiseaseCount = 0, bool isActive = true)
		{
			this.elementHash = element;
			this.minOutputTemperature = minOutputTemperature;
			this.useEntityTemperature = useEntityTemperature;
			this.storeOutput = storeOutput;
			this.massGenerationRate = kgPerSecond;
			this.outputElementOffset = new Vector2(outputElementOffsetx, outputElementOffsety);
			this.accumulator = HandleVector<int>.InvalidHandle;
			this.diseaseWeight = diseaseWeight;
			this.addedDiseaseIdx = addedDiseaseIdx;
			this.addedDiseaseCount = addedDiseaseCount;
			this.IsActive = isActive;
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x060080A8 RID: 32936 RVA: 0x002DF748 File Offset: 0x002DD948
		public string Name
		{
			get
			{
				return ElementLoader.FindElementByHash(this.elementHash).tag.ProperName();
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060080A9 RID: 32937 RVA: 0x002DF75F File Offset: 0x002DD95F
		public float Rate
		{
			get
			{
				return Game.Instance.accumulators.GetAverageRate(this.accumulator);
			}
		}

		// Token: 0x040062F6 RID: 25334
		public bool IsActive;

		// Token: 0x040062F7 RID: 25335
		public SimHashes elementHash;

		// Token: 0x040062F8 RID: 25336
		public float minOutputTemperature;

		// Token: 0x040062F9 RID: 25337
		public bool useEntityTemperature;

		// Token: 0x040062FA RID: 25338
		public float massGenerationRate;

		// Token: 0x040062FB RID: 25339
		public bool storeOutput;

		// Token: 0x040062FC RID: 25340
		public Vector2 outputElementOffset;

		// Token: 0x040062FD RID: 25341
		public HandleVector<int>.Handle accumulator;

		// Token: 0x040062FE RID: 25342
		public float diseaseWeight;

		// Token: 0x040062FF RID: 25343
		public byte addedDiseaseIdx;

		// Token: 0x04006300 RID: 25344
		public int addedDiseaseCount;
	}

	// Token: 0x02001445 RID: 5189
	public class StatesInstance : GameStateMachine<ElementConverter.States, ElementConverter.StatesInstance, ElementConverter, object>.GameInstance
	{
		// Token: 0x060080AA RID: 32938 RVA: 0x002DF776 File Offset: 0x002DD976
		public StatesInstance(ElementConverter smi)
			: base(smi)
		{
			this.selectable = base.GetComponent<KSelectable>();
		}

		// Token: 0x060080AB RID: 32939 RVA: 0x002DF78C File Offset: 0x002DD98C
		public void AddStatusItems()
		{
			if (!base.master.ShowInUI)
			{
				return;
			}
			foreach (ElementConverter.ConsumedElement consumedElement in base.master.consumedElements)
			{
				if (consumedElement.IsActive)
				{
					this.AddStatusItem<ElementConverter.ConsumedElement, Tag>(consumedElement, consumedElement.Tag, ElementConverter.ElementConverterInput, base.master.consumedElementStatusHandles);
				}
			}
			foreach (ElementConverter.OutputElement outputElement in base.master.outputElements)
			{
				if (outputElement.IsActive)
				{
					this.AddStatusItem<ElementConverter.OutputElement, SimHashes>(outputElement, outputElement.elementHash, ElementConverter.ElementConverterOutput, base.master.outputElementStatusHandles);
				}
			}
		}

		// Token: 0x060080AC RID: 32940 RVA: 0x002DF83C File Offset: 0x002DDA3C
		public void RemoveStatusItems()
		{
			if (!base.master.ShowInUI)
			{
				return;
			}
			for (int i = 0; i < base.master.consumedElements.Length; i++)
			{
				ElementConverter.ConsumedElement consumedElement = base.master.consumedElements[i];
				this.RemoveStatusItem<Tag>(consumedElement.Tag, base.master.consumedElementStatusHandles);
			}
			for (int j = 0; j < base.master.outputElements.Length; j++)
			{
				ElementConverter.OutputElement outputElement = base.master.outputElements[j];
				this.RemoveStatusItem<SimHashes>(outputElement.elementHash, base.master.outputElementStatusHandles);
			}
			base.master.consumedElementStatusHandles.Clear();
			base.master.outputElementStatusHandles.Clear();
		}

		// Token: 0x060080AD RID: 32941 RVA: 0x002DF8FC File Offset: 0x002DDAFC
		public void AddStatusItem<ElementType, IDType>(ElementType element, IDType id, StatusItem status, Dictionary<IDType, Guid> collection)
		{
			Guid guid = this.selectable.AddStatusItem(status, element);
			collection[id] = guid;
		}

		// Token: 0x060080AE RID: 32942 RVA: 0x002DF928 File Offset: 0x002DDB28
		public void RemoveStatusItem<IDType>(IDType id, Dictionary<IDType, Guid> collection)
		{
			Guid guid;
			if (!collection.TryGetValue(id, out guid))
			{
				return;
			}
			this.selectable.RemoveStatusItem(guid, false);
		}

		// Token: 0x060080AF RID: 32943 RVA: 0x002DF950 File Offset: 0x002DDB50
		public void OnOperationalRequirementChanged(object data)
		{
			Operational operational = data as Operational;
			bool flag = ((operational == null) ? ((bool)data) : operational.IsActive);
			base.sm.canConvert.Set(flag, this, false);
		}

		// Token: 0x04006301 RID: 25345
		private KSelectable selectable;
	}

	// Token: 0x02001446 RID: 5190
	public class States : GameStateMachine<ElementConverter.States, ElementConverter.StatesInstance, ElementConverter>
	{
		// Token: 0x060080B0 RID: 32944 RVA: 0x002DF990 File Offset: 0x002DDB90
		private bool ValidateStateTransition(ElementConverter.StatesInstance smi, bool _)
		{
			bool flag = smi.GetCurrentState() == smi.sm.disabled;
			if (smi.master.operational == null)
			{
				return flag;
			}
			bool flag2 = smi.master.consumedElements.Length == 0;
			bool flag3 = this.canConvert.Get(smi);
			int num = 0;
			while (!flag2 && num < smi.master.consumedElements.Length)
			{
				flag2 = smi.master.consumedElements[num].IsActive;
				num++;
			}
			if (flag3 && !flag2)
			{
				this.canConvert.Set(false, smi, true);
				return false;
			}
			return smi.master.operational.MeetsRequirements(smi.master.OperationalRequirement) == flag;
		}

		// Token: 0x060080B1 RID: 32945 RVA: 0x002DFA4C File Offset: 0x002DDC4C
		private void OnEnterRoot(ElementConverter.StatesInstance smi)
		{
			int eventForState = (int)Operational.GetEventForState(smi.master.OperationalRequirement);
			smi.Subscribe(eventForState, new Action<object>(smi.OnOperationalRequirementChanged));
		}

		// Token: 0x060080B2 RID: 32946 RVA: 0x002DFA80 File Offset: 0x002DDC80
		private void OnExitRoot(ElementConverter.StatesInstance smi)
		{
			int eventForState = (int)Operational.GetEventForState(smi.master.OperationalRequirement);
			smi.Unsubscribe(eventForState, new Action<object>(smi.OnOperationalRequirementChanged));
		}

		// Token: 0x060080B3 RID: 32947 RVA: 0x002DFAB4 File Offset: 0x002DDCB4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.disabled;
			this.root.Enter(new StateMachine<ElementConverter.States, ElementConverter.StatesInstance, ElementConverter, object>.State.Callback(this.OnEnterRoot)).Exit(new StateMachine<ElementConverter.States, ElementConverter.StatesInstance, ElementConverter, object>.State.Callback(this.OnExitRoot));
			this.disabled.ParamTransition<bool>(this.canConvert, this.converting, new StateMachine<ElementConverter.States, ElementConverter.StatesInstance, ElementConverter, object>.Parameter<bool>.Callback(this.ValidateStateTransition));
			this.converting.Enter("AddStatusItems", delegate(ElementConverter.StatesInstance smi)
			{
				smi.AddStatusItems();
			}).Exit("RemoveStatusItems", delegate(ElementConverter.StatesInstance smi)
			{
				smi.RemoveStatusItems();
			}).ParamTransition<bool>(this.canConvert, this.disabled, new StateMachine<ElementConverter.States, ElementConverter.StatesInstance, ElementConverter, object>.Parameter<bool>.Callback(this.ValidateStateTransition))
				.Update("ConvertMass", delegate(ElementConverter.StatesInstance smi, float dt)
				{
					smi.master.ConvertMass();
				}, UpdateRate.SIM_1000ms, true);
		}

		// Token: 0x04006302 RID: 25346
		public GameStateMachine<ElementConverter.States, ElementConverter.StatesInstance, ElementConverter, object>.State disabled;

		// Token: 0x04006303 RID: 25347
		public GameStateMachine<ElementConverter.States, ElementConverter.StatesInstance, ElementConverter, object>.State converting;

		// Token: 0x04006304 RID: 25348
		public StateMachine<ElementConverter.States, ElementConverter.StatesInstance, ElementConverter, object>.BoolParameter canConvert;
	}
}
