using System;
using System.Collections.Generic;
using Klei;
using Klei.AI;
using STRINGS;

// Token: 0x020006C6 RID: 1734
public class CreatureSimTemperatureTransfer : SimTemperatureTransfer, ISim200ms
{
	// Token: 0x1700034F RID: 847
	// (get) Token: 0x06002F2F RID: 12079 RVA: 0x000F9792 File Offset: 0x000F7992
	// (set) Token: 0x06002F30 RID: 12080 RVA: 0x000F979A File Offset: 0x000F799A
	public float deltaEnergy
	{
		get
		{
			return this.deltaKJ;
		}
		protected set
		{
			this.deltaKJ = value;
		}
	}

	// Token: 0x17000350 RID: 848
	// (get) Token: 0x06002F31 RID: 12081 RVA: 0x000F97A3 File Offset: 0x000F79A3
	public float currentExchangeWattage
	{
		get
		{
			return this.deltaKJ * 5f * 1000f;
		}
	}

	// Token: 0x06002F32 RID: 12082 RVA: 0x000F97B8 File Offset: 0x000F79B8
	protected override void OnPrefabInit()
	{
		this.primaryElement = base.GetComponent<PrimaryElement>();
		this.average_kilowatts_exchanged = new RunningWeightedAverage(-10f, 10f, 20, true);
		this.surfaceArea = 1f;
		this.thickness = 0.002f;
		this.groundTransferScale = 0f;
		AttributeInstance attributeInstance = base.gameObject.GetAttributes().Add(Db.Get().Attributes.ThermalConductivityBarrier);
		AttributeModifier attributeModifier = new AttributeModifier(Db.Get().Attributes.ThermalConductivityBarrier.Id, this.thickness, DUPLICANTS.MODIFIERS.BASEDUPLICANT.NAME, false, false, true);
		attributeInstance.Add(attributeModifier);
		this.averageTemperatureTransferPerSecond = new AttributeModifier("TemperatureDelta", 0f, DUPLICANTS.MODIFIERS.TEMPEXCHANGE.NAME, false, true, false);
		this.GetAttributes().Add(this.averageTemperatureTransferPerSecond);
		base.OnPrefabInit();
	}

	// Token: 0x06002F33 RID: 12083 RVA: 0x000F9898 File Offset: 0x000F7A98
	public void Sim200ms(float dt)
	{
		this.average_kilowatts_exchanged.AddSample(this.currentExchangeWattage * 0.001f);
		this.averageTemperatureTransferPerSecond.SetValue(SimUtil.EnergyFlowToTemperatureDelta(this.average_kilowatts_exchanged.GetWeightedAverage, this.primaryElement.Element.specificHeatCapacity, this.primaryElement.Mass));
		float num = 0f;
		foreach (AttributeModifier attributeModifier in this.NonSimTemperatureModifiers)
		{
			num += attributeModifier.Value;
		}
		if (Sim.IsValidHandle(this.simHandle))
		{
			SimMessages.ModifyElementChunkEnergy(this.simHandle, num * dt * (this.primaryElement.Mass * 1000f) * this.primaryElement.Element.specificHeatCapacity * 0.001f);
		}
	}

	// Token: 0x06002F34 RID: 12084 RVA: 0x000F9984 File Offset: 0x000F7B84
	public void RefreshRegistration()
	{
		base.SimUnregister();
		AttributeInstance attributeInstance = base.gameObject.GetAttributes().Get("ThermalConductivityBarrier");
		this.thickness = attributeInstance.GetTotalValue();
		this.simHandle = -1;
		base.SimRegister();
	}

	// Token: 0x06002F35 RID: 12085 RVA: 0x000F99C6 File Offset: 0x000F7BC6
	public static float PotentialEnergyFlowToCreature(int cell, PrimaryElement transfererPrimaryElement, SimTemperatureTransfer temperatureTransferer, float deltaTime = 1f)
	{
		return SimUtil.CalculateEnergyFlowCreatures(cell, transfererPrimaryElement.Temperature, transfererPrimaryElement.Element.specificHeatCapacity, transfererPrimaryElement.Element.thermalConductivity, temperatureTransferer.SurfaceArea, temperatureTransferer.Thickness);
	}

	// Token: 0x04001C62 RID: 7266
	public AttributeModifier averageTemperatureTransferPerSecond;

	// Token: 0x04001C63 RID: 7267
	private PrimaryElement primaryElement;

	// Token: 0x04001C64 RID: 7268
	public RunningWeightedAverage average_kilowatts_exchanged;

	// Token: 0x04001C65 RID: 7269
	public List<AttributeModifier> NonSimTemperatureModifiers = new List<AttributeModifier>();
}
