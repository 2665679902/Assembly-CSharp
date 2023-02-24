using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using STRINGS;
using UnityEngine;

// Token: 0x02000684 RID: 1668
public class CircuitManager
{
	// Token: 0x06002CE5 RID: 11493 RVA: 0x000EB796 File Offset: 0x000E9996
	public void Connect(Generator generator)
	{
		if (Game.IsQuitting())
		{
			return;
		}
		this.generators.Add(generator);
		this.dirty = true;
	}

	// Token: 0x06002CE6 RID: 11494 RVA: 0x000EB7B4 File Offset: 0x000E99B4
	public void Disconnect(Generator generator)
	{
		if (Game.IsQuitting())
		{
			return;
		}
		this.generators.Remove(generator);
		this.dirty = true;
	}

	// Token: 0x06002CE7 RID: 11495 RVA: 0x000EB7D2 File Offset: 0x000E99D2
	public void Connect(IEnergyConsumer consumer)
	{
		if (Game.IsQuitting())
		{
			return;
		}
		this.consumers.Add(consumer);
		this.dirty = true;
	}

	// Token: 0x06002CE8 RID: 11496 RVA: 0x000EB7F0 File Offset: 0x000E99F0
	public void Disconnect(IEnergyConsumer consumer, bool isDestroy)
	{
		if (Game.IsQuitting())
		{
			return;
		}
		this.consumers.Remove(consumer);
		if (!isDestroy)
		{
			consumer.SetConnectionStatus(CircuitManager.ConnectionStatus.NotConnected);
		}
		this.dirty = true;
	}

	// Token: 0x06002CE9 RID: 11497 RVA: 0x000EB818 File Offset: 0x000E9A18
	public void Connect(WireUtilityNetworkLink bridge)
	{
		this.bridges.Add(bridge);
		this.dirty = true;
	}

	// Token: 0x06002CEA RID: 11498 RVA: 0x000EB82E File Offset: 0x000E9A2E
	public void Disconnect(WireUtilityNetworkLink bridge)
	{
		this.bridges.Remove(bridge);
		this.dirty = true;
	}

	// Token: 0x06002CEB RID: 11499 RVA: 0x000EB844 File Offset: 0x000E9A44
	public float GetPowerDraw(ushort circuitID, Generator generator)
	{
		float num = 0f;
		if ((int)circuitID < this.circuitInfo.Count)
		{
			CircuitManager.CircuitInfo circuitInfo = this.circuitInfo[(int)circuitID];
			this.circuitInfo[(int)circuitID] = circuitInfo;
			this.circuitInfo[(int)circuitID] = circuitInfo;
		}
		return num;
	}

	// Token: 0x06002CEC RID: 11500 RVA: 0x000EB88C File Offset: 0x000E9A8C
	public ushort GetCircuitID(int cell)
	{
		UtilityNetwork networkForCell = Game.Instance.electricalConduitSystem.GetNetworkForCell(cell);
		return (ushort)((networkForCell == null) ? 65535 : networkForCell.id);
	}

	// Token: 0x06002CED RID: 11501 RVA: 0x000EB8BC File Offset: 0x000E9ABC
	public ushort GetVirtualCircuitID(object virtualKey)
	{
		UtilityNetwork networkForVirtualKey = Game.Instance.electricalConduitSystem.GetNetworkForVirtualKey(virtualKey);
		return (ushort)((networkForVirtualKey == null) ? 65535 : networkForVirtualKey.id);
	}

	// Token: 0x06002CEE RID: 11502 RVA: 0x000EB8EB File Offset: 0x000E9AEB
	public ushort GetCircuitID(ICircuitConnected ent)
	{
		if (!ent.IsVirtual)
		{
			return this.GetCircuitID(ent.PowerCell);
		}
		return this.GetVirtualCircuitID(ent.VirtualCircuitKey);
	}

	// Token: 0x06002CEF RID: 11503 RVA: 0x000EB90E File Offset: 0x000E9B0E
	public void Sim200msFirst(float dt)
	{
		this.Refresh(dt);
	}

	// Token: 0x06002CF0 RID: 11504 RVA: 0x000EB917 File Offset: 0x000E9B17
	public void RenderEveryTick(float dt)
	{
		this.Refresh(dt);
	}

	// Token: 0x06002CF1 RID: 11505 RVA: 0x000EB920 File Offset: 0x000E9B20
	private void Refresh(float dt)
	{
		UtilityNetworkManager<ElectricalUtilityNetwork, Wire> electricalConduitSystem = Game.Instance.electricalConduitSystem;
		if (electricalConduitSystem.IsDirty || this.dirty)
		{
			electricalConduitSystem.Update();
			IList<UtilityNetwork> networks = electricalConduitSystem.GetNetworks();
			while (this.circuitInfo.Count < networks.Count)
			{
				CircuitManager.CircuitInfo circuitInfo = new CircuitManager.CircuitInfo
				{
					generators = new List<Generator>(),
					consumers = new List<IEnergyConsumer>(),
					batteries = new List<Battery>(),
					inputTransformers = new List<Battery>(),
					outputTransformers = new List<Generator>()
				};
				circuitInfo.bridgeGroups = new List<WireUtilityNetworkLink>[5];
				for (int i = 0; i < circuitInfo.bridgeGroups.Length; i++)
				{
					circuitInfo.bridgeGroups[i] = new List<WireUtilityNetworkLink>();
				}
				this.circuitInfo.Add(circuitInfo);
			}
			this.Rebuild();
		}
	}

	// Token: 0x06002CF2 RID: 11506 RVA: 0x000EBA00 File Offset: 0x000E9C00
	public void Rebuild()
	{
		for (int i = 0; i < this.circuitInfo.Count; i++)
		{
			CircuitManager.CircuitInfo circuitInfo = this.circuitInfo[i];
			circuitInfo.generators.Clear();
			circuitInfo.consumers.Clear();
			circuitInfo.batteries.Clear();
			circuitInfo.inputTransformers.Clear();
			circuitInfo.outputTransformers.Clear();
			circuitInfo.minBatteryPercentFull = 1f;
			for (int j = 0; j < circuitInfo.bridgeGroups.Length; j++)
			{
				circuitInfo.bridgeGroups[j].Clear();
			}
			this.circuitInfo[i] = circuitInfo;
		}
		this.consumersShadow.AddRange(this.consumers);
		foreach (IEnergyConsumer energyConsumer in this.consumersShadow)
		{
			ushort circuitID = this.GetCircuitID(energyConsumer);
			if (circuitID != 65535)
			{
				Battery battery = energyConsumer as Battery;
				if (battery != null)
				{
					CircuitManager.CircuitInfo circuitInfo2 = this.circuitInfo[(int)circuitID];
					if (battery.powerTransformer != null)
					{
						circuitInfo2.inputTransformers.Add(battery);
					}
					else
					{
						circuitInfo2.batteries.Add(battery);
						circuitInfo2.minBatteryPercentFull = Mathf.Min(this.circuitInfo[(int)circuitID].minBatteryPercentFull, battery.PercentFull);
					}
					this.circuitInfo[(int)circuitID] = circuitInfo2;
				}
				else
				{
					this.circuitInfo[(int)circuitID].consumers.Add(energyConsumer);
				}
			}
		}
		this.consumersShadow.Clear();
		for (int k = 0; k < this.circuitInfo.Count; k++)
		{
			this.circuitInfo[k].consumers.Sort((IEnergyConsumer a, IEnergyConsumer b) => a.WattsNeededWhenActive.CompareTo(b.WattsNeededWhenActive));
		}
		foreach (Generator generator in this.generators)
		{
			ushort circuitID2 = this.GetCircuitID(generator);
			if (circuitID2 != 65535)
			{
				if (generator.GetType() == typeof(PowerTransformer))
				{
					this.circuitInfo[(int)circuitID2].outputTransformers.Add(generator);
				}
				else
				{
					this.circuitInfo[(int)circuitID2].generators.Add(generator);
				}
			}
		}
		foreach (WireUtilityNetworkLink wireUtilityNetworkLink in this.bridges)
		{
			ushort circuitID3 = this.GetCircuitID(wireUtilityNetworkLink);
			if (circuitID3 != 65535)
			{
				Wire.WattageRating maxWattageRating = wireUtilityNetworkLink.GetMaxWattageRating();
				this.circuitInfo[(int)circuitID3].bridgeGroups[(int)maxWattageRating].Add(wireUtilityNetworkLink);
			}
		}
		this.dirty = false;
	}

	// Token: 0x06002CF3 RID: 11507 RVA: 0x000EBCD8 File Offset: 0x000E9ED8
	private float GetBatteryJoulesAvailable(List<Battery> batteries, out int num_powered)
	{
		float num = 0f;
		num_powered = 0;
		for (int i = 0; i < batteries.Count; i++)
		{
			if (batteries[i].JoulesAvailable > 0f)
			{
				num = batteries[i].JoulesAvailable;
				num_powered = batteries.Count - i;
				break;
			}
		}
		return num;
	}

	// Token: 0x06002CF4 RID: 11508 RVA: 0x000EBD2C File Offset: 0x000E9F2C
	public void Sim200msLast(float dt)
	{
		this.elapsedTime += dt;
		if (this.elapsedTime < 0.2f)
		{
			return;
		}
		this.elapsedTime -= 0.2f;
		for (int i = 0; i < this.circuitInfo.Count; i++)
		{
			CircuitManager.CircuitInfo circuitInfo = this.circuitInfo[i];
			circuitInfo.wattsUsed = 0f;
			this.activeGenerators.Clear();
			List<Generator> list = circuitInfo.generators;
			List<IEnergyConsumer> list2 = circuitInfo.consumers;
			List<Battery> batteries = circuitInfo.batteries;
			List<Generator> outputTransformers = circuitInfo.outputTransformers;
			batteries.Sort((Battery a, Battery b) => a.JoulesAvailable.CompareTo(b.JoulesAvailable));
			bool flag = false;
			bool flag2 = list.Count > 0;
			for (int j = 0; j < list.Count; j++)
			{
				Generator generator = list[j];
				if (generator.JoulesAvailable > 0f)
				{
					flag = true;
					this.activeGenerators.Add(generator);
				}
			}
			this.activeGenerators.Sort((Generator a, Generator b) => a.JoulesAvailable.CompareTo(b.JoulesAvailable));
			if (!flag)
			{
				for (int k = 0; k < outputTransformers.Count; k++)
				{
					if (outputTransformers[k].JoulesAvailable > 0f)
					{
						flag = true;
					}
				}
			}
			float num = 1f;
			for (int l = 0; l < batteries.Count; l++)
			{
				Battery battery = batteries[l];
				if (battery.JoulesAvailable > 0f)
				{
					flag = true;
				}
				num = Mathf.Min(num, battery.PercentFull);
			}
			for (int m = 0; m < circuitInfo.inputTransformers.Count; m++)
			{
				Battery battery2 = circuitInfo.inputTransformers[m];
				num = Mathf.Min(num, battery2.PercentFull);
			}
			circuitInfo.minBatteryPercentFull = num;
			if (flag)
			{
				for (int n = 0; n < list2.Count; n++)
				{
					IEnergyConsumer energyConsumer = list2[n];
					float num2 = energyConsumer.WattsUsed * 0.2f;
					if (num2 > 0f)
					{
						bool flag3 = false;
						for (int num3 = 0; num3 < this.activeGenerators.Count; num3++)
						{
							Generator generator2 = this.activeGenerators[num3];
							num2 = this.PowerFromGenerator(num2, generator2, energyConsumer);
							if (num2 <= 0f)
							{
								flag3 = true;
								break;
							}
						}
						if (!flag3)
						{
							for (int num4 = 0; num4 < outputTransformers.Count; num4++)
							{
								Generator generator3 = outputTransformers[num4];
								num2 = this.PowerFromGenerator(num2, generator3, energyConsumer);
								if (num2 <= 0f)
								{
									flag3 = true;
									break;
								}
							}
						}
						if (!flag3)
						{
							num2 = this.PowerFromBatteries(num2, batteries, energyConsumer);
							flag3 = num2 <= 0.01f;
						}
						if (flag3)
						{
							circuitInfo.wattsUsed += energyConsumer.WattsUsed;
						}
						else
						{
							circuitInfo.wattsUsed += energyConsumer.WattsUsed - num2 / 0.2f;
						}
						energyConsumer.SetConnectionStatus(flag3 ? CircuitManager.ConnectionStatus.Powered : CircuitManager.ConnectionStatus.Unpowered);
					}
					else
					{
						energyConsumer.SetConnectionStatus(flag ? CircuitManager.ConnectionStatus.Powered : CircuitManager.ConnectionStatus.Unpowered);
					}
				}
			}
			else if (flag2)
			{
				for (int num5 = 0; num5 < list2.Count; num5++)
				{
					list2[num5].SetConnectionStatus(CircuitManager.ConnectionStatus.Unpowered);
				}
			}
			else
			{
				for (int num6 = 0; num6 < list2.Count; num6++)
				{
					list2[num6].SetConnectionStatus(CircuitManager.ConnectionStatus.NotConnected);
				}
			}
			this.circuitInfo[i] = circuitInfo;
		}
		for (int num7 = 0; num7 < this.circuitInfo.Count; num7++)
		{
			CircuitManager.CircuitInfo circuitInfo2 = this.circuitInfo[num7];
			circuitInfo2.batteries.Sort((Battery a, Battery b) => (a.Capacity - a.JoulesAvailable).CompareTo(b.Capacity - b.JoulesAvailable));
			circuitInfo2.inputTransformers.Sort((Battery a, Battery b) => (a.Capacity - a.JoulesAvailable).CompareTo(b.Capacity - b.JoulesAvailable));
			circuitInfo2.generators.Sort((Generator a, Generator b) => a.JoulesAvailable.CompareTo(b.JoulesAvailable));
			float num8 = 0f;
			this.ChargeTransformers<Generator>(circuitInfo2.inputTransformers, circuitInfo2.generators, ref num8);
			this.ChargeTransformers<Generator>(circuitInfo2.inputTransformers, circuitInfo2.outputTransformers, ref num8);
			float num9 = 0f;
			this.ChargeBatteries(circuitInfo2.batteries, circuitInfo2.generators, ref num9);
			this.ChargeBatteries(circuitInfo2.batteries, circuitInfo2.outputTransformers, ref num9);
			circuitInfo2.minBatteryPercentFull = 1f;
			for (int num10 = 0; num10 < circuitInfo2.batteries.Count; num10++)
			{
				float percentFull = circuitInfo2.batteries[num10].PercentFull;
				if (percentFull < circuitInfo2.minBatteryPercentFull)
				{
					circuitInfo2.minBatteryPercentFull = percentFull;
				}
			}
			for (int num11 = 0; num11 < circuitInfo2.inputTransformers.Count; num11++)
			{
				float percentFull2 = circuitInfo2.inputTransformers[num11].PercentFull;
				if (percentFull2 < circuitInfo2.minBatteryPercentFull)
				{
					circuitInfo2.minBatteryPercentFull = percentFull2;
				}
			}
			circuitInfo2.wattsUsed += num8 / 0.2f;
			this.circuitInfo[num7] = circuitInfo2;
		}
		for (int num12 = 0; num12 < this.circuitInfo.Count; num12++)
		{
			CircuitManager.CircuitInfo circuitInfo3 = this.circuitInfo[num12];
			circuitInfo3.batteries.Sort((Battery a, Battery b) => a.JoulesAvailable.CompareTo(b.JoulesAvailable));
			float num13 = 0f;
			this.ChargeTransformers<Battery>(circuitInfo3.inputTransformers, circuitInfo3.batteries, ref num13);
			circuitInfo3.wattsUsed += num13 / 0.2f;
			this.circuitInfo[num12] = circuitInfo3;
		}
		for (int num14 = 0; num14 < this.circuitInfo.Count; num14++)
		{
			CircuitManager.CircuitInfo circuitInfo4 = this.circuitInfo[num14];
			bool flag4 = circuitInfo4.generators.Count + circuitInfo4.consumers.Count + circuitInfo4.outputTransformers.Count > 0;
			this.UpdateBatteryConnectionStatus(circuitInfo4.batteries, flag4, num14);
			bool flag5 = circuitInfo4.generators.Count > 0 || circuitInfo4.outputTransformers.Count > 0;
			if (!flag5)
			{
				using (List<Battery>.Enumerator enumerator = circuitInfo4.batteries.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.JoulesAvailable > 0f)
						{
							flag5 = true;
							break;
						}
					}
				}
			}
			this.UpdateBatteryConnectionStatus(circuitInfo4.inputTransformers, flag5, num14);
			this.circuitInfo[num14] = circuitInfo4;
			for (int num15 = 0; num15 < circuitInfo4.generators.Count; num15++)
			{
				Generator generator4 = circuitInfo4.generators[num15];
				ReportManager.Instance.ReportValue(ReportManager.ReportType.EnergyWasted, -generator4.JoulesAvailable, StringFormatter.Replace(BUILDINGS.PREFABS.GENERATOR.OVERPRODUCTION, "{Generator}", generator4.gameObject.GetProperName()), null);
			}
		}
		for (int num16 = 0; num16 < this.circuitInfo.Count; num16++)
		{
			CircuitManager.CircuitInfo circuitInfo5 = this.circuitInfo[num16];
			this.CheckCircuitOverloaded(0.2f, num16, circuitInfo5.wattsUsed);
		}
	}

	// Token: 0x06002CF5 RID: 11509 RVA: 0x000EC4C0 File Offset: 0x000EA6C0
	private float PowerFromBatteries(float joules_needed, List<Battery> batteries, IEnergyConsumer c)
	{
		int num2;
		do
		{
			float num = this.GetBatteryJoulesAvailable(batteries, out num2) * (float)num2;
			float num3 = ((num < joules_needed) ? num : joules_needed);
			joules_needed -= num3;
			ReportManager.Instance.ReportValue(ReportManager.ReportType.EnergyCreated, -num3, c.Name, null);
			float num4 = num3 / (float)num2;
			for (int i = batteries.Count - num2; i < batteries.Count; i++)
			{
				batteries[i].ConsumeEnergy(num4);
			}
		}
		while (joules_needed >= 0.01f && num2 > 0);
		return joules_needed;
	}

	// Token: 0x06002CF6 RID: 11510 RVA: 0x000EC53C File Offset: 0x000EA73C
	private float PowerFromGenerator(float joules_needed, Generator g, IEnergyConsumer c)
	{
		float num = Mathf.Min(g.JoulesAvailable, joules_needed);
		joules_needed -= num;
		g.ApplyDeltaJoules(-num, false);
		ReportManager.Instance.ReportValue(ReportManager.ReportType.EnergyCreated, -num, c.Name, null);
		return joules_needed;
	}

	// Token: 0x06002CF7 RID: 11511 RVA: 0x000EC57C File Offset: 0x000EA77C
	private void ChargeBatteries(List<Battery> sink_batteries, List<Generator> source_generators, ref float joules_used)
	{
		if (sink_batteries.Count == 0)
		{
			return;
		}
		foreach (Generator generator in source_generators)
		{
			for (bool flag = true; flag && generator.JoulesAvailable >= 1f; flag = this.ChargeBatteriesFromGenerator(sink_batteries, generator, ref joules_used))
			{
			}
		}
	}

	// Token: 0x06002CF8 RID: 11512 RVA: 0x000EC5EC File Offset: 0x000EA7EC
	private bool ChargeBatteriesFromGenerator(List<Battery> sink_batteries, Generator source_generator, ref float joules_used)
	{
		float num = source_generator.JoulesAvailable;
		float num2 = 0f;
		for (int i = 0; i < sink_batteries.Count; i++)
		{
			Battery battery = sink_batteries[i];
			if (battery != null && source_generator != null && battery.gameObject != source_generator.gameObject)
			{
				float num3 = battery.Capacity - battery.JoulesAvailable;
				if (num3 > 0f)
				{
					float num4 = Mathf.Min(num3, num / (float)(sink_batteries.Count - i));
					battery.AddEnergy(num4);
					num -= num4;
					num2 += num4;
				}
			}
		}
		if (num2 > 0f)
		{
			source_generator.ApplyDeltaJoules(-num2, false);
			joules_used += num2;
			return true;
		}
		return false;
	}

	// Token: 0x06002CF9 RID: 11513 RVA: 0x000EC69C File Offset: 0x000EA89C
	private void UpdateBatteryConnectionStatus(List<Battery> batteries, bool is_connected_to_something_useful, int circuit_id)
	{
		foreach (Battery battery in batteries)
		{
			if (!(battery == null))
			{
				if (battery.powerTransformer == null)
				{
					battery.SetConnectionStatus(is_connected_to_something_useful ? CircuitManager.ConnectionStatus.Powered : CircuitManager.ConnectionStatus.NotConnected);
				}
				else if ((int)this.GetCircuitID(battery) == circuit_id)
				{
					battery.SetConnectionStatus(is_connected_to_something_useful ? CircuitManager.ConnectionStatus.Powered : CircuitManager.ConnectionStatus.Unpowered);
				}
			}
		}
	}

	// Token: 0x06002CFA RID: 11514 RVA: 0x000EC720 File Offset: 0x000EA920
	private void ChargeTransformer<T>(Battery sink_transformer, List<T> source_energy_producers, ref float joules_used) where T : IEnergyProducer
	{
		if (source_energy_producers.Count <= 0)
		{
			return;
		}
		float num = Mathf.Min(sink_transformer.Capacity - sink_transformer.JoulesAvailable, sink_transformer.ChargeCapacity);
		if (num <= 0f)
		{
			return;
		}
		float num2 = num;
		float num3 = 0f;
		for (int i = 0; i < source_energy_producers.Count; i++)
		{
			T t = source_energy_producers[i];
			if (t.JoulesAvailable > 0f)
			{
				float num4 = Mathf.Min(t.JoulesAvailable, num2 / (float)(source_energy_producers.Count - i));
				t.ConsumeEnergy(num4);
				num2 -= num4;
				num3 += num4;
			}
		}
		sink_transformer.AddEnergy(num3);
		joules_used += num3;
	}

	// Token: 0x06002CFB RID: 11515 RVA: 0x000EC7D4 File Offset: 0x000EA9D4
	private void ChargeTransformers<T>(List<Battery> sink_transformers, List<T> source_energy_producers, ref float joules_used) where T : IEnergyProducer
	{
		foreach (Battery battery in sink_transformers)
		{
			this.ChargeTransformer<T>(battery, source_energy_producers, ref joules_used);
		}
	}

	// Token: 0x06002CFC RID: 11516 RVA: 0x000EC824 File Offset: 0x000EAA24
	private void CheckCircuitOverloaded(float dt, int id, float watts_used)
	{
		UtilityNetwork networkByID = Game.Instance.electricalConduitSystem.GetNetworkByID(id);
		if (networkByID != null)
		{
			ElectricalUtilityNetwork electricalUtilityNetwork = (ElectricalUtilityNetwork)networkByID;
			if (electricalUtilityNetwork != null)
			{
				electricalUtilityNetwork.UpdateOverloadTime(dt, watts_used, this.circuitInfo[id].bridgeGroups);
			}
		}
	}

	// Token: 0x06002CFD RID: 11517 RVA: 0x000EC868 File Offset: 0x000EAA68
	public float GetWattsUsedByCircuit(ushort circuitID)
	{
		if (circuitID == 65535)
		{
			return -1f;
		}
		return this.circuitInfo[(int)circuitID].wattsUsed;
	}

	// Token: 0x06002CFE RID: 11518 RVA: 0x000EC88C File Offset: 0x000EAA8C
	public float GetWattsNeededWhenActive(ushort circuitID)
	{
		if (circuitID == 65535)
		{
			return -1f;
		}
		float num = 0f;
		foreach (IEnergyConsumer energyConsumer in this.circuitInfo[(int)circuitID].consumers)
		{
			num += energyConsumer.WattsNeededWhenActive;
		}
		foreach (Battery battery in this.circuitInfo[(int)circuitID].inputTransformers)
		{
			num += battery.WattsNeededWhenActive;
		}
		return num;
	}

	// Token: 0x06002CFF RID: 11519 RVA: 0x000EC954 File Offset: 0x000EAB54
	public float GetWattsGeneratedByCircuit(ushort circuitID)
	{
		if (circuitID == 65535)
		{
			return -1f;
		}
		float num = 0f;
		foreach (Generator generator in this.circuitInfo[(int)circuitID].generators)
		{
			if (!(generator == null) && generator.IsProducingPower())
			{
				num += generator.WattageRating;
			}
		}
		return num;
	}

	// Token: 0x06002D00 RID: 11520 RVA: 0x000EC9DC File Offset: 0x000EABDC
	public float GetPotentialWattsGeneratedByCircuit(ushort circuitID)
	{
		if (circuitID == 65535)
		{
			return -1f;
		}
		float num = 0f;
		foreach (Generator generator in this.circuitInfo[(int)circuitID].generators)
		{
			num += generator.WattageRating;
		}
		return num;
	}

	// Token: 0x06002D01 RID: 11521 RVA: 0x000ECA54 File Offset: 0x000EAC54
	public float GetJoulesAvailableOnCircuit(ushort circuitID)
	{
		int num;
		return this.GetBatteryJoulesAvailable(this.GetBatteriesOnCircuit(circuitID), out num) * (float)num;
	}

	// Token: 0x06002D02 RID: 11522 RVA: 0x000ECA73 File Offset: 0x000EAC73
	public ReadOnlyCollection<Generator> GetGeneratorsOnCircuit(ushort circuitID)
	{
		if (circuitID == 65535)
		{
			return null;
		}
		return this.circuitInfo[(int)circuitID].generators.AsReadOnly();
	}

	// Token: 0x06002D03 RID: 11523 RVA: 0x000ECA95 File Offset: 0x000EAC95
	public ReadOnlyCollection<IEnergyConsumer> GetConsumersOnCircuit(ushort circuitID)
	{
		if (circuitID == 65535)
		{
			return null;
		}
		return this.circuitInfo[(int)circuitID].consumers.AsReadOnly();
	}

	// Token: 0x06002D04 RID: 11524 RVA: 0x000ECAB7 File Offset: 0x000EACB7
	public ReadOnlyCollection<Battery> GetTransformersOnCircuit(ushort circuitID)
	{
		if (circuitID == 65535)
		{
			return null;
		}
		return this.circuitInfo[(int)circuitID].inputTransformers.AsReadOnly();
	}

	// Token: 0x06002D05 RID: 11525 RVA: 0x000ECAD9 File Offset: 0x000EACD9
	public List<Battery> GetBatteriesOnCircuit(ushort circuitID)
	{
		if (circuitID == 65535)
		{
			return null;
		}
		return this.circuitInfo[(int)circuitID].batteries;
	}

	// Token: 0x06002D06 RID: 11526 RVA: 0x000ECAF6 File Offset: 0x000EACF6
	public float GetMinBatteryPercentFullOnCircuit(ushort circuitID)
	{
		if (circuitID == 65535)
		{
			return 0f;
		}
		return this.circuitInfo[(int)circuitID].minBatteryPercentFull;
	}

	// Token: 0x06002D07 RID: 11527 RVA: 0x000ECB17 File Offset: 0x000EAD17
	public bool HasBatteries(ushort circuitID)
	{
		return circuitID != ushort.MaxValue && this.circuitInfo[(int)circuitID].batteries.Count + this.circuitInfo[(int)circuitID].inputTransformers.Count > 0;
	}

	// Token: 0x06002D08 RID: 11528 RVA: 0x000ECB53 File Offset: 0x000EAD53
	public bool HasGenerators(ushort circuitID)
	{
		return circuitID != ushort.MaxValue && this.circuitInfo[(int)circuitID].generators.Count + this.circuitInfo[(int)circuitID].outputTransformers.Count > 0;
	}

	// Token: 0x06002D09 RID: 11529 RVA: 0x000ECB8F File Offset: 0x000EAD8F
	public bool HasGenerators()
	{
		return this.generators.Count > 0;
	}

	// Token: 0x06002D0A RID: 11530 RVA: 0x000ECB9F File Offset: 0x000EAD9F
	public bool HasConsumers(ushort circuitID)
	{
		return circuitID != ushort.MaxValue && this.circuitInfo[(int)circuitID].consumers.Count > 0;
	}

	// Token: 0x06002D0B RID: 11531 RVA: 0x000ECBC4 File Offset: 0x000EADC4
	public float GetMaxSafeWattageForCircuit(ushort circuitID)
	{
		if (circuitID == 65535)
		{
			return 0f;
		}
		ElectricalUtilityNetwork electricalUtilityNetwork = Game.Instance.electricalConduitSystem.GetNetworkByID((int)circuitID) as ElectricalUtilityNetwork;
		if (electricalUtilityNetwork == null)
		{
			return 0f;
		}
		return electricalUtilityNetwork.GetMaxSafeWattage();
	}

	// Token: 0x04001AD0 RID: 6864
	public const ushort INVALID_ID = 65535;

	// Token: 0x04001AD1 RID: 6865
	private const int SimUpdateSortKey = 1000;

	// Token: 0x04001AD2 RID: 6866
	private const float MIN_POWERED_THRESHOLD = 0.01f;

	// Token: 0x04001AD3 RID: 6867
	private bool dirty = true;

	// Token: 0x04001AD4 RID: 6868
	private HashSet<Generator> generators = new HashSet<Generator>();

	// Token: 0x04001AD5 RID: 6869
	private HashSet<IEnergyConsumer> consumers = new HashSet<IEnergyConsumer>();

	// Token: 0x04001AD6 RID: 6870
	private HashSet<WireUtilityNetworkLink> bridges = new HashSet<WireUtilityNetworkLink>();

	// Token: 0x04001AD7 RID: 6871
	private float elapsedTime;

	// Token: 0x04001AD8 RID: 6872
	private List<CircuitManager.CircuitInfo> circuitInfo = new List<CircuitManager.CircuitInfo>();

	// Token: 0x04001AD9 RID: 6873
	private List<IEnergyConsumer> consumersShadow = new List<IEnergyConsumer>();

	// Token: 0x04001ADA RID: 6874
	private List<Generator> activeGenerators = new List<Generator>();

	// Token: 0x02001347 RID: 4935
	private struct CircuitInfo
	{
		// Token: 0x0400600A RID: 24586
		public List<Generator> generators;

		// Token: 0x0400600B RID: 24587
		public List<IEnergyConsumer> consumers;

		// Token: 0x0400600C RID: 24588
		public List<Battery> batteries;

		// Token: 0x0400600D RID: 24589
		public List<Battery> inputTransformers;

		// Token: 0x0400600E RID: 24590
		public List<Generator> outputTransformers;

		// Token: 0x0400600F RID: 24591
		public List<WireUtilityNetworkLink>[] bridgeGroups;

		// Token: 0x04006010 RID: 24592
		public float minBatteryPercentFull;

		// Token: 0x04006011 RID: 24593
		public float wattsUsed;
	}

	// Token: 0x02001348 RID: 4936
	public enum ConnectionStatus
	{
		// Token: 0x04006013 RID: 24595
		NotConnected,
		// Token: 0x04006014 RID: 24596
		Unpowered,
		// Token: 0x04006015 RID: 24597
		Powered
	}
}
