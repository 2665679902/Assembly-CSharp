using System;
using System.Collections.Generic;

// Token: 0x02000744 RID: 1860
public class EnergySim
{
	// Token: 0x170003C2 RID: 962
	// (get) Token: 0x0600334C RID: 13132 RVA: 0x0011482D File Offset: 0x00112A2D
	public HashSet<Generator> Generators
	{
		get
		{
			return this.generators;
		}
	}

	// Token: 0x0600334D RID: 13133 RVA: 0x00114835 File Offset: 0x00112A35
	public void AddGenerator(Generator generator)
	{
		this.generators.Add(generator);
	}

	// Token: 0x0600334E RID: 13134 RVA: 0x00114844 File Offset: 0x00112A44
	public void RemoveGenerator(Generator generator)
	{
		this.generators.Remove(generator);
	}

	// Token: 0x0600334F RID: 13135 RVA: 0x00114853 File Offset: 0x00112A53
	public void AddManualGenerator(ManualGenerator manual_generator)
	{
		this.manualGenerators.Add(manual_generator);
	}

	// Token: 0x06003350 RID: 13136 RVA: 0x00114862 File Offset: 0x00112A62
	public void RemoveManualGenerator(ManualGenerator manual_generator)
	{
		this.manualGenerators.Remove(manual_generator);
	}

	// Token: 0x06003351 RID: 13137 RVA: 0x00114871 File Offset: 0x00112A71
	public void AddBattery(Battery battery)
	{
		this.batteries.Add(battery);
	}

	// Token: 0x06003352 RID: 13138 RVA: 0x00114880 File Offset: 0x00112A80
	public void RemoveBattery(Battery battery)
	{
		this.batteries.Remove(battery);
	}

	// Token: 0x06003353 RID: 13139 RVA: 0x0011488F File Offset: 0x00112A8F
	public void AddEnergyConsumer(EnergyConsumer energy_consumer)
	{
		this.energyConsumers.Add(energy_consumer);
	}

	// Token: 0x06003354 RID: 13140 RVA: 0x0011489E File Offset: 0x00112A9E
	public void RemoveEnergyConsumer(EnergyConsumer energy_consumer)
	{
		this.energyConsumers.Remove(energy_consumer);
	}

	// Token: 0x06003355 RID: 13141 RVA: 0x001148B0 File Offset: 0x00112AB0
	public void EnergySim200ms(float dt)
	{
		foreach (Generator generator in this.generators)
		{
			generator.EnergySim200ms(dt);
		}
		foreach (ManualGenerator manualGenerator in this.manualGenerators)
		{
			manualGenerator.EnergySim200ms(dt);
		}
		foreach (Battery battery in this.batteries)
		{
			battery.EnergySim200ms(dt);
		}
		foreach (EnergyConsumer energyConsumer in this.energyConsumers)
		{
			energyConsumer.EnergySim200ms(dt);
		}
	}

	// Token: 0x04001F81 RID: 8065
	private HashSet<Generator> generators = new HashSet<Generator>();

	// Token: 0x04001F82 RID: 8066
	private HashSet<ManualGenerator> manualGenerators = new HashSet<ManualGenerator>();

	// Token: 0x04001F83 RID: 8067
	private HashSet<Battery> batteries = new HashSet<Battery>();

	// Token: 0x04001F84 RID: 8068
	private HashSet<EnergyConsumer> energyConsumers = new HashSet<EnergyConsumer>();
}
