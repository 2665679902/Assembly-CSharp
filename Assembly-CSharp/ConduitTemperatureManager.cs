using System;
using System.Runtime.InteropServices;

// Token: 0x020006B0 RID: 1712
public class ConduitTemperatureManager
{
	// Token: 0x06002E8D RID: 11917 RVA: 0x000F5F0A File Offset: 0x000F410A
	public ConduitTemperatureManager()
	{
		ConduitTemperatureManager.ConduitTemperatureManager_Initialize();
	}

	// Token: 0x06002E8E RID: 11918 RVA: 0x000F5F2F File Offset: 0x000F412F
	public void Shutdown()
	{
		ConduitTemperatureManager.ConduitTemperatureManager_Shutdown();
	}

	// Token: 0x06002E8F RID: 11919 RVA: 0x000F5F38 File Offset: 0x000F4138
	public HandleVector<int>.Handle Allocate(ConduitType conduit_type, int conduit_idx, HandleVector<int>.Handle conduit_structure_temperature_handle, ref ConduitFlow.ConduitContents contents)
	{
		StructureTemperaturePayload payload = GameComps.StructureTemperatures.GetPayload(conduit_structure_temperature_handle);
		Element element = payload.primaryElement.Element;
		BuildingDef def = payload.building.Def;
		float num = def.MassForTemperatureModification * element.specificHeatCapacity;
		float num2 = element.thermalConductivity * def.ThermalConductivity;
		int num3 = ConduitTemperatureManager.ConduitTemperatureManager_Add(contents.temperature, contents.mass, (int)contents.element, payload.simHandleCopy, num, num2, def.ThermalConductivity < 1f);
		HandleVector<int>.Handle handle = default(HandleVector<int>.Handle);
		handle.index = num3;
		int handleIndex = Sim.GetHandleIndex(num3);
		if (handleIndex + 1 > this.temperatures.Length)
		{
			Array.Resize<float>(ref this.temperatures, (handleIndex + 1) * 2);
			Array.Resize<ConduitTemperatureManager.ConduitInfo>(ref this.conduitInfo, (handleIndex + 1) * 2);
		}
		this.temperatures[handleIndex] = contents.temperature;
		this.conduitInfo[handleIndex] = new ConduitTemperatureManager.ConduitInfo
		{
			type = conduit_type,
			idx = conduit_idx
		};
		return handle;
	}

	// Token: 0x06002E90 RID: 11920 RVA: 0x000F603C File Offset: 0x000F423C
	public void SetData(HandleVector<int>.Handle handle, ref ConduitFlow.ConduitContents contents)
	{
		if (!handle.IsValid())
		{
			return;
		}
		this.temperatures[Sim.GetHandleIndex(handle.index)] = contents.temperature;
		ConduitTemperatureManager.ConduitTemperatureManager_Set(handle.index, contents.temperature, contents.mass, (int)contents.element);
	}

	// Token: 0x06002E91 RID: 11921 RVA: 0x000F608C File Offset: 0x000F428C
	public void Free(HandleVector<int>.Handle handle)
	{
		if (handle.IsValid())
		{
			int handleIndex = Sim.GetHandleIndex(handle.index);
			this.temperatures[handleIndex] = -1f;
			this.conduitInfo[handleIndex] = new ConduitTemperatureManager.ConduitInfo
			{
				type = ConduitType.None,
				idx = -1
			};
			ConduitTemperatureManager.ConduitTemperatureManager_Remove(handle.index);
		}
	}

	// Token: 0x06002E92 RID: 11922 RVA: 0x000F60ED File Offset: 0x000F42ED
	public void Clear()
	{
		ConduitTemperatureManager.ConduitTemperatureManager_Clear();
	}

	// Token: 0x06002E93 RID: 11923 RVA: 0x000F60F4 File Offset: 0x000F42F4
	public unsafe void Sim200ms(float dt)
	{
		ConduitTemperatureManager.ConduitTemperatureUpdateData* ptr = (ConduitTemperatureManager.ConduitTemperatureUpdateData*)(void*)ConduitTemperatureManager.ConduitTemperatureManager_Update(dt, (IntPtr)((void*)Game.Instance.simData.buildingTemperatures));
		int numEntries = ptr->numEntries;
		if (numEntries > 0)
		{
			Marshal.Copy((IntPtr)((void*)ptr->temperatures), this.temperatures, 0, numEntries);
		}
		for (int i = 0; i < ptr->numFrozenHandles; i++)
		{
			int handleIndex = Sim.GetHandleIndex(ptr->frozenHandles[i]);
			ConduitTemperatureManager.ConduitInfo conduitInfo = this.conduitInfo[handleIndex];
			Conduit.GetFlowManager(conduitInfo.type).FreezeConduitContents(conduitInfo.idx);
		}
		for (int j = 0; j < ptr->numMeltedHandles; j++)
		{
			int handleIndex2 = Sim.GetHandleIndex(ptr->meltedHandles[j]);
			ConduitTemperatureManager.ConduitInfo conduitInfo2 = this.conduitInfo[handleIndex2];
			Conduit.GetFlowManager(conduitInfo2.type).MeltConduitContents(conduitInfo2.idx);
		}
	}

	// Token: 0x06002E94 RID: 11924 RVA: 0x000F61DD File Offset: 0x000F43DD
	public float GetTemperature(HandleVector<int>.Handle handle)
	{
		return this.temperatures[Sim.GetHandleIndex(handle.index)];
	}

	// Token: 0x06002E95 RID: 11925
	[DllImport("SimDLL")]
	private static extern void ConduitTemperatureManager_Initialize();

	// Token: 0x06002E96 RID: 11926
	[DllImport("SimDLL")]
	private static extern void ConduitTemperatureManager_Shutdown();

	// Token: 0x06002E97 RID: 11927
	[DllImport("SimDLL")]
	private static extern int ConduitTemperatureManager_Add(float contents_temperature, float contents_mass, int contents_element_hash, int conduit_structure_temperature_handle, float conduit_heat_capacity, float conduit_thermal_conductivity, bool conduit_insulated);

	// Token: 0x06002E98 RID: 11928
	[DllImport("SimDLL")]
	private static extern int ConduitTemperatureManager_Set(int handle, float contents_temperature, float contents_mass, int contents_element_hash);

	// Token: 0x06002E99 RID: 11929
	[DllImport("SimDLL")]
	private static extern void ConduitTemperatureManager_Remove(int handle);

	// Token: 0x06002E9A RID: 11930
	[DllImport("SimDLL")]
	private static extern IntPtr ConduitTemperatureManager_Update(float dt, IntPtr building_conductivity_data);

	// Token: 0x06002E9B RID: 11931
	[DllImport("SimDLL")]
	private static extern void ConduitTemperatureManager_Clear();

	// Token: 0x04001C1E RID: 7198
	private float[] temperatures = new float[0];

	// Token: 0x04001C1F RID: 7199
	private ConduitTemperatureManager.ConduitInfo[] conduitInfo = new ConduitTemperatureManager.ConduitInfo[0];

	// Token: 0x02001386 RID: 4998
	private struct ConduitInfo
	{
		// Token: 0x040060F0 RID: 24816
		public ConduitType type;

		// Token: 0x040060F1 RID: 24817
		public int idx;
	}

	// Token: 0x02001387 RID: 4999
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct ConduitTemperatureUpdateData
	{
		// Token: 0x040060F2 RID: 24818
		public int numEntries;

		// Token: 0x040060F3 RID: 24819
		public unsafe float* temperatures;

		// Token: 0x040060F4 RID: 24820
		public int numFrozenHandles;

		// Token: 0x040060F5 RID: 24821
		public unsafe int* frozenHandles;

		// Token: 0x040060F6 RID: 24822
		public int numMeltedHandles;

		// Token: 0x040060F7 RID: 24823
		public unsafe int* meltedHandles;
	}
}
