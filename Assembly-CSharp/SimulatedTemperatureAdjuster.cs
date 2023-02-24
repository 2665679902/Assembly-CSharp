using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000920 RID: 2336
public class SimulatedTemperatureAdjuster
{
	// Token: 0x06004443 RID: 17475 RVA: 0x00181880 File Offset: 0x0017FA80
	public SimulatedTemperatureAdjuster(float simulated_temperature, float heat_capacity, float thermal_conductivity, Storage storage)
	{
		this.temperature = simulated_temperature;
		this.heatCapacity = heat_capacity;
		this.thermalConductivity = thermal_conductivity;
		this.storage = storage;
		storage.gameObject.Subscribe(824508782, new Action<object>(this.OnActivechanged));
		storage.gameObject.Subscribe(-1697596308, new Action<object>(this.OnStorageChanged));
		Operational component = storage.gameObject.GetComponent<Operational>();
		this.OnActivechanged(component);
	}

	// Token: 0x06004444 RID: 17476 RVA: 0x00181900 File Offset: 0x0017FB00
	public List<Descriptor> GetDescriptors()
	{
		return SimulatedTemperatureAdjuster.GetDescriptors(this.temperature);
	}

	// Token: 0x06004445 RID: 17477 RVA: 0x00181910 File Offset: 0x0017FB10
	public static List<Descriptor> GetDescriptors(float temperature)
	{
		List<Descriptor> list = new List<Descriptor>();
		string formattedTemperature = GameUtil.GetFormattedTemperature(temperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false);
		Descriptor descriptor = new Descriptor(string.Format(UI.BUILDINGEFFECTS.ITEM_TEMPERATURE_ADJUST, formattedTemperature), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ITEM_TEMPERATURE_ADJUST, formattedTemperature), Descriptor.DescriptorType.Effect, false);
		list.Add(descriptor);
		return list;
	}

	// Token: 0x06004446 RID: 17478 RVA: 0x00181960 File Offset: 0x0017FB60
	private void Register(SimTemperatureTransfer stt)
	{
		stt.onSimRegistered = (Action<SimTemperatureTransfer>)Delegate.Remove(stt.onSimRegistered, new Action<SimTemperatureTransfer>(this.OnItemSimRegistered));
		stt.onSimRegistered = (Action<SimTemperatureTransfer>)Delegate.Combine(stt.onSimRegistered, new Action<SimTemperatureTransfer>(this.OnItemSimRegistered));
		if (Sim.IsValidHandle(stt.SimHandle))
		{
			this.OnItemSimRegistered(stt);
		}
	}

	// Token: 0x06004447 RID: 17479 RVA: 0x001819C8 File Offset: 0x0017FBC8
	private void Unregister(SimTemperatureTransfer stt)
	{
		stt.onSimRegistered = (Action<SimTemperatureTransfer>)Delegate.Remove(stt.onSimRegistered, new Action<SimTemperatureTransfer>(this.OnItemSimRegistered));
		if (Sim.IsValidHandle(stt.SimHandle))
		{
			SimMessages.ModifyElementChunkTemperatureAdjuster(stt.SimHandle, 0f, 0f, 0f);
		}
	}

	// Token: 0x06004448 RID: 17480 RVA: 0x00181A20 File Offset: 0x0017FC20
	private void OnItemSimRegistered(SimTemperatureTransfer stt)
	{
		if (stt == null)
		{
			return;
		}
		if (Sim.IsValidHandle(stt.SimHandle))
		{
			float num = this.temperature;
			float num2 = this.heatCapacity;
			float num3 = this.thermalConductivity;
			if (!this.active)
			{
				num = 0f;
				num2 = 0f;
				num3 = 0f;
			}
			SimMessages.ModifyElementChunkTemperatureAdjuster(stt.SimHandle, num, num2, num3);
		}
	}

	// Token: 0x06004449 RID: 17481 RVA: 0x00181A84 File Offset: 0x0017FC84
	private void OnActivechanged(object data)
	{
		Operational operational = (Operational)data;
		this.active = operational.IsActive;
		if (this.active)
		{
			using (List<GameObject>.Enumerator enumerator = this.storage.items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GameObject gameObject = enumerator.Current;
					if (gameObject != null)
					{
						SimTemperatureTransfer component = gameObject.GetComponent<SimTemperatureTransfer>();
						this.OnItemSimRegistered(component);
					}
				}
				return;
			}
		}
		foreach (GameObject gameObject2 in this.storage.items)
		{
			if (gameObject2 != null)
			{
				SimTemperatureTransfer component2 = gameObject2.GetComponent<SimTemperatureTransfer>();
				this.Unregister(component2);
			}
		}
	}

	// Token: 0x0600444A RID: 17482 RVA: 0x00181B64 File Offset: 0x0017FD64
	public void CleanUp()
	{
		this.storage.gameObject.Unsubscribe(-1697596308, new Action<object>(this.OnStorageChanged));
		foreach (GameObject gameObject in this.storage.items)
		{
			if (gameObject != null)
			{
				SimTemperatureTransfer component = gameObject.GetComponent<SimTemperatureTransfer>();
				this.Unregister(component);
			}
		}
	}

	// Token: 0x0600444B RID: 17483 RVA: 0x00181BF0 File Offset: 0x0017FDF0
	private void OnStorageChanged(object data)
	{
		GameObject gameObject = (GameObject)data;
		SimTemperatureTransfer component = gameObject.GetComponent<SimTemperatureTransfer>();
		if (component == null)
		{
			return;
		}
		Pickupable component2 = gameObject.GetComponent<Pickupable>();
		if (component2 == null)
		{
			return;
		}
		if (this.active && component2.storage == this.storage)
		{
			this.Register(component);
			return;
		}
		this.Unregister(component);
	}

	// Token: 0x04002D85 RID: 11653
	private float temperature;

	// Token: 0x04002D86 RID: 11654
	private float heatCapacity;

	// Token: 0x04002D87 RID: 11655
	private float thermalConductivity;

	// Token: 0x04002D88 RID: 11656
	private bool active;

	// Token: 0x04002D89 RID: 11657
	private Storage storage;
}
