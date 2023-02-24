using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200073A RID: 1850
[SkipSaveFileSerialization]
[SerializationConfig(MemberSerialization.OptIn)]
public class ElementConsumer : SimComponent, ISaveLoadable, IGameObjectEffectDescriptor
{
	// Token: 0x14000019 RID: 25
	// (add) Token: 0x060032A4 RID: 12964 RVA: 0x00111474 File Offset: 0x0010F674
	// (remove) Token: 0x060032A5 RID: 12965 RVA: 0x001114AC File Offset: 0x0010F6AC
	public event Action<Sim.ConsumedMassInfo> OnElementConsumed;

	// Token: 0x170003A6 RID: 934
	// (get) Token: 0x060032A6 RID: 12966 RVA: 0x001114E1 File Offset: 0x0010F6E1
	public float AverageConsumeRate
	{
		get
		{
			return Game.Instance.accumulators.GetAverageRate(this.accumulator);
		}
	}

	// Token: 0x060032A7 RID: 12967 RVA: 0x001114F8 File Offset: 0x0010F6F8
	public static void ClearInstanceMap()
	{
		ElementConsumer.handleInstanceMap.Clear();
	}

	// Token: 0x060032A8 RID: 12968 RVA: 0x00111504 File Offset: 0x0010F704
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.accumulator = Game.Instance.accumulators.Add("Element", this);
		if (this.elementToConsume == SimHashes.Void)
		{
			throw new ArgumentException("No consumable elements specified");
		}
		if (!this.ignoreActiveChanged)
		{
			base.Subscribe<ElementConsumer>(824508782, ElementConsumer.OnActiveChangedDelegate);
		}
		if (this.capacityKG != float.PositiveInfinity)
		{
			this.hasAvailableCapacity = !this.IsStorageFull();
			base.Subscribe<ElementConsumer>(-1697596308, ElementConsumer.OnStorageChangeDelegate);
		}
	}

	// Token: 0x060032A9 RID: 12969 RVA: 0x00111590 File Offset: 0x0010F790
	protected override void OnCleanUp()
	{
		Game.Instance.accumulators.Remove(this.accumulator);
		base.OnCleanUp();
	}

	// Token: 0x060032AA RID: 12970 RVA: 0x001115AE File Offset: 0x0010F7AE
	protected virtual bool IsActive()
	{
		return this.operational == null || this.operational.IsActive;
	}

	// Token: 0x060032AB RID: 12971 RVA: 0x001115CC File Offset: 0x0010F7CC
	public void EnableConsumption(bool enabled)
	{
		bool flag = this.consumptionEnabled;
		this.consumptionEnabled = enabled;
		if (!Sim.IsValidHandle(this.simHandle))
		{
			return;
		}
		if (enabled != flag)
		{
			this.UpdateSimData();
		}
	}

	// Token: 0x060032AC RID: 12972 RVA: 0x00111600 File Offset: 0x0010F800
	private bool IsStorageFull()
	{
		PrimaryElement primaryElement = this.storage.FindPrimaryElement(this.elementToConsume);
		return primaryElement != null && primaryElement.Mass >= this.capacityKG;
	}

	// Token: 0x060032AD RID: 12973 RVA: 0x0011163B File Offset: 0x0010F83B
	public void RefreshConsumptionRate()
	{
		if (!Sim.IsValidHandle(this.simHandle))
		{
			return;
		}
		this.UpdateSimData();
	}

	// Token: 0x060032AE RID: 12974 RVA: 0x00111654 File Offset: 0x0010F854
	private void UpdateSimData()
	{
		global::Debug.Assert(Sim.IsValidHandle(this.simHandle));
		int sampleCell = this.GetSampleCell();
		float num = ((this.consumptionEnabled && this.hasAvailableCapacity) ? this.consumptionRate : 0f);
		SimMessages.SetElementConsumerData(this.simHandle, sampleCell, num);
		this.UpdateStatusItem();
	}

	// Token: 0x060032AF RID: 12975 RVA: 0x001116AC File Offset: 0x0010F8AC
	public static void AddMass(Sim.ConsumedMassInfo consumed_info)
	{
		if (!Sim.IsValidHandle(consumed_info.simHandle))
		{
			return;
		}
		ElementConsumer elementConsumer;
		if (ElementConsumer.handleInstanceMap.TryGetValue(consumed_info.simHandle, out elementConsumer))
		{
			elementConsumer.AddMassInternal(consumed_info);
		}
	}

	// Token: 0x060032B0 RID: 12976 RVA: 0x001116E2 File Offset: 0x0010F8E2
	private int GetSampleCell()
	{
		return Grid.PosToCell(base.transform.GetPosition() + this.sampleCellOffset);
	}

	// Token: 0x060032B1 RID: 12977 RVA: 0x00111700 File Offset: 0x0010F900
	private void AddMassInternal(Sim.ConsumedMassInfo consumed_info)
	{
		if (consumed_info.mass > 0f)
		{
			if (this.storeOnConsume)
			{
				Element element = ElementLoader.elements[(int)consumed_info.removedElemIdx];
				if (this.elementToConsume == SimHashes.Vacuum || this.elementToConsume == element.id)
				{
					if (element.IsLiquid)
					{
						this.storage.AddLiquid(element.id, consumed_info.mass, consumed_info.temperature, consumed_info.diseaseIdx, consumed_info.diseaseCount, true, true);
					}
					else if (element.IsGas)
					{
						this.storage.AddGasChunk(element.id, consumed_info.mass, consumed_info.temperature, consumed_info.diseaseIdx, consumed_info.diseaseCount, true, true);
					}
				}
			}
			else
			{
				this.consumedTemperature = GameUtil.GetFinalTemperature(consumed_info.temperature, consumed_info.mass, this.consumedTemperature, this.consumedMass);
				this.consumedMass += consumed_info.mass;
				if (this.OnElementConsumed != null)
				{
					this.OnElementConsumed(consumed_info);
				}
			}
		}
		Game.Instance.accumulators.Accumulate(this.accumulator, consumed_info.mass);
	}

	// Token: 0x170003A7 RID: 935
	// (get) Token: 0x060032B2 RID: 12978 RVA: 0x0011182C File Offset: 0x0010FA2C
	public bool IsElementAvailable
	{
		get
		{
			int sampleCell = this.GetSampleCell();
			SimHashes id = Grid.Element[sampleCell].id;
			return this.elementToConsume == id && Grid.Mass[sampleCell] >= this.minimumMass;
		}
	}

	// Token: 0x060032B3 RID: 12979 RVA: 0x00111870 File Offset: 0x0010FA70
	private void UpdateStatusItem()
	{
		if (this.showInStatusPanel)
		{
			if (this.statusHandle == Guid.Empty && this.IsActive() && this.consumptionEnabled)
			{
				this.statusHandle = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.ElementConsumer, this);
				return;
			}
			if (this.statusHandle != Guid.Empty)
			{
				base.GetComponent<KSelectable>().RemoveStatusItem(this.statusHandle, false);
			}
		}
	}

	// Token: 0x060032B4 RID: 12980 RVA: 0x001118F0 File Offset: 0x0010FAF0
	private void OnStorageChange(object data)
	{
		bool flag = !this.IsStorageFull();
		if (flag != this.hasAvailableCapacity)
		{
			this.hasAvailableCapacity = flag;
			this.RefreshConsumptionRate();
		}
	}

	// Token: 0x060032B5 RID: 12981 RVA: 0x0011191D File Offset: 0x0010FB1D
	protected override void OnCmpEnable()
	{
		if (!base.isSpawned)
		{
			return;
		}
		if (!this.IsActive())
		{
			return;
		}
		this.UpdateStatusItem();
	}

	// Token: 0x060032B6 RID: 12982 RVA: 0x00111937 File Offset: 0x0010FB37
	protected override void OnCmpDisable()
	{
		this.UpdateStatusItem();
	}

	// Token: 0x060032B7 RID: 12983 RVA: 0x00111940 File Offset: 0x0010FB40
	public List<Descriptor> RequirementDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.isRequired && this.showDescriptor)
		{
			Element element = ElementLoader.FindElementByHash(this.elementToConsume);
			string text = element.tag.ProperName();
			if (element.IsVacuum)
			{
				if (this.configuration == ElementConsumer.Configuration.AllGas)
				{
					text = ELEMENTS.STATE.GAS;
				}
				else if (this.configuration == ElementConsumer.Configuration.AllLiquid)
				{
					text = ELEMENTS.STATE.LIQUID;
				}
				else
				{
					text = UI.BUILDINGEFFECTS.CONSUMESANYELEMENT;
				}
			}
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.REQUIRESELEMENT, text), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESELEMENT, text), Descriptor.DescriptorType.Requirement);
			list.Add(descriptor);
		}
		return list;
	}

	// Token: 0x060032B8 RID: 12984 RVA: 0x001119F8 File Offset: 0x0010FBF8
	public List<Descriptor> EffectDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.showDescriptor)
		{
			Element element = ElementLoader.FindElementByHash(this.elementToConsume);
			string text = element.tag.ProperName();
			if (element.IsVacuum)
			{
				if (this.configuration == ElementConsumer.Configuration.AllGas)
				{
					text = ELEMENTS.STATE.GAS;
				}
				else if (this.configuration == ElementConsumer.Configuration.AllLiquid)
				{
					text = ELEMENTS.STATE.LIQUID;
				}
				else
				{
					text = UI.BUILDINGEFFECTS.CONSUMESANYELEMENT;
				}
			}
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMED, text, GameUtil.GetFormattedMass(this.consumptionRate / 100f * 100f, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMED, text, GameUtil.GetFormattedMass(this.consumptionRate / 100f * 100f, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), Descriptor.DescriptorType.Effect);
			list.Add(descriptor);
		}
		return list;
	}

	// Token: 0x060032B9 RID: 12985 RVA: 0x00111AE4 File Offset: 0x0010FCE4
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

	// Token: 0x060032BA RID: 12986 RVA: 0x00111B80 File Offset: 0x0010FD80
	private void OnActiveChanged(object data)
	{
		bool isActive = this.operational.IsActive;
		this.EnableConsumption(isActive);
	}

	// Token: 0x060032BB RID: 12987 RVA: 0x00111BA0 File Offset: 0x0010FDA0
	protected override void OnSimUnregister()
	{
		global::Debug.Assert(Sim.IsValidHandle(this.simHandle));
		ElementConsumer.handleInstanceMap.Remove(this.simHandle);
		ElementConsumer.StaticUnregister(this.simHandle);
	}

	// Token: 0x060032BC RID: 12988 RVA: 0x00111BCE File Offset: 0x0010FDCE
	protected override void OnSimRegister(HandleVector<Game.ComplexCallbackInfo<int>>.Handle cb_handle)
	{
		SimMessages.AddElementConsumer(this.GetSampleCell(), this.configuration, this.elementToConsume, this.consumptionRadius, cb_handle.index);
	}

	// Token: 0x060032BD RID: 12989 RVA: 0x00111BF4 File Offset: 0x0010FDF4
	protected override Action<int> GetStaticUnregister()
	{
		return new Action<int>(ElementConsumer.StaticUnregister);
	}

	// Token: 0x060032BE RID: 12990 RVA: 0x00111C02 File Offset: 0x0010FE02
	private static void StaticUnregister(int sim_handle)
	{
		global::Debug.Assert(Sim.IsValidHandle(sim_handle));
		SimMessages.RemoveElementConsumer(-1, sim_handle);
	}

	// Token: 0x060032BF RID: 12991 RVA: 0x00111C16 File Offset: 0x0010FE16
	protected override void OnSimRegistered()
	{
		if (this.consumptionEnabled)
		{
			this.UpdateSimData();
		}
		ElementConsumer.handleInstanceMap[this.simHandle] = this;
	}

	// Token: 0x04001F20 RID: 7968
	[HashedEnum]
	[SerializeField]
	public SimHashes elementToConsume = SimHashes.Vacuum;

	// Token: 0x04001F21 RID: 7969
	[SerializeField]
	public float consumptionRate;

	// Token: 0x04001F22 RID: 7970
	[SerializeField]
	public byte consumptionRadius = 1;

	// Token: 0x04001F23 RID: 7971
	[SerializeField]
	public float minimumMass;

	// Token: 0x04001F24 RID: 7972
	[SerializeField]
	public bool showInStatusPanel = true;

	// Token: 0x04001F25 RID: 7973
	[SerializeField]
	public Vector3 sampleCellOffset;

	// Token: 0x04001F26 RID: 7974
	[SerializeField]
	public float capacityKG = float.PositiveInfinity;

	// Token: 0x04001F27 RID: 7975
	[SerializeField]
	public ElementConsumer.Configuration configuration;

	// Token: 0x04001F28 RID: 7976
	[Serialize]
	[NonSerialized]
	public float consumedMass;

	// Token: 0x04001F29 RID: 7977
	[Serialize]
	[NonSerialized]
	public float consumedTemperature;

	// Token: 0x04001F2A RID: 7978
	[SerializeField]
	public bool storeOnConsume;

	// Token: 0x04001F2B RID: 7979
	[MyCmpGet]
	public Storage storage;

	// Token: 0x04001F2C RID: 7980
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001F2D RID: 7981
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x04001F2F RID: 7983
	private HandleVector<int>.Handle accumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x04001F30 RID: 7984
	public bool ignoreActiveChanged;

	// Token: 0x04001F31 RID: 7985
	private Guid statusHandle;

	// Token: 0x04001F32 RID: 7986
	public bool showDescriptor = true;

	// Token: 0x04001F33 RID: 7987
	public bool isRequired = true;

	// Token: 0x04001F34 RID: 7988
	private bool consumptionEnabled;

	// Token: 0x04001F35 RID: 7989
	private bool hasAvailableCapacity = true;

	// Token: 0x04001F36 RID: 7990
	private static Dictionary<int, ElementConsumer> handleInstanceMap = new Dictionary<int, ElementConsumer>();

	// Token: 0x04001F37 RID: 7991
	private static readonly EventSystem.IntraObjectHandler<ElementConsumer> OnActiveChangedDelegate = new EventSystem.IntraObjectHandler<ElementConsumer>(delegate(ElementConsumer component, object data)
	{
		component.OnActiveChanged(data);
	});

	// Token: 0x04001F38 RID: 7992
	private static readonly EventSystem.IntraObjectHandler<ElementConsumer> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<ElementConsumer>(delegate(ElementConsumer component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x02001441 RID: 5185
	public enum Configuration
	{
		// Token: 0x040062EE RID: 25326
		Element,
		// Token: 0x040062EF RID: 25327
		AllLiquid,
		// Token: 0x040062F0 RID: 25328
		AllGas
	}
}
