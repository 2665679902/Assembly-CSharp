using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Database;
using Klei.AI;
using Klei.AI.DiseaseGrowthRules;
using STRINGS;

// Token: 0x02000A25 RID: 2597
public static class SimMessages
{
	// Token: 0x06004EA8 RID: 20136 RVA: 0x001BEF48 File Offset: 0x001BD148
	public unsafe static void AddElementConsumer(int gameCell, ElementConsumer.Configuration configuration, SimHashes element, byte radius, int cb_handle)
	{
		Debug.Assert(Grid.IsValidCell(gameCell));
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		ushort elementIndex = ElementLoader.GetElementIndex(element);
		SimMessages.AddElementConsumerMessage* ptr;
		checked
		{
			ptr = stackalloc SimMessages.AddElementConsumerMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.AddElementConsumerMessage)];
			ptr->cellIdx = gameCell;
		}
		ptr->configuration = (byte)configuration;
		ptr->elementIdx = elementIndex;
		ptr->radius = radius;
		ptr->callbackIdx = cb_handle;
		Sim.SIM_HandleMessage(2024405073, sizeof(SimMessages.AddElementConsumerMessage), (byte*)ptr);
	}

	// Token: 0x06004EA9 RID: 20137 RVA: 0x001BEFB4 File Offset: 0x001BD1B4
	public unsafe static void SetElementConsumerData(int sim_handle, int cell, float consumptionRate)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			return;
		}
		checked
		{
			SimMessages.SetElementConsumerDataMessage* ptr = stackalloc SimMessages.SetElementConsumerDataMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.SetElementConsumerDataMessage)];
			ptr->handle = sim_handle;
			ptr->cell = cell;
			ptr->consumptionRate = consumptionRate;
			Sim.SIM_HandleMessage(1575539738, sizeof(SimMessages.SetElementConsumerDataMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EAA RID: 20138 RVA: 0x001BF000 File Offset: 0x001BD200
	public unsafe static void RemoveElementConsumer(int cb_handle, int sim_handle)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			Debug.Assert(false, "Invalid handle");
			return;
		}
		checked
		{
			SimMessages.RemoveElementConsumerMessage* ptr = stackalloc SimMessages.RemoveElementConsumerMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveElementConsumerMessage)];
			ptr->callbackIdx = cb_handle;
			ptr->handle = sim_handle;
			Sim.SIM_HandleMessage(894417742, sizeof(SimMessages.RemoveElementConsumerMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EAB RID: 20139 RVA: 0x001BF050 File Offset: 0x001BD250
	public unsafe static void AddElementEmitter(float max_pressure, int on_registered, int on_blocked = -1, int on_unblocked = -1)
	{
		checked
		{
			SimMessages.AddElementEmitterMessage* ptr = stackalloc SimMessages.AddElementEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.AddElementEmitterMessage)];
			ptr->maxPressure = max_pressure;
			ptr->callbackIdx = on_registered;
			ptr->onBlockedCB = on_blocked;
			ptr->onUnblockedCB = on_unblocked;
			Sim.SIM_HandleMessage(-505471181, sizeof(SimMessages.AddElementEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EAC RID: 20140 RVA: 0x001BF098 File Offset: 0x001BD298
	public unsafe static void ModifyElementEmitter(int sim_handle, int game_cell, int max_depth, SimHashes element, float emit_interval, float emit_mass, float emit_temperature, float max_pressure, byte disease_idx, int disease_count)
	{
		Debug.Assert(Grid.IsValidCell(game_cell));
		if (!Grid.IsValidCell(game_cell))
		{
			return;
		}
		ushort elementIndex = ElementLoader.GetElementIndex(element);
		SimMessages.ModifyElementEmitterMessage* ptr;
		checked
		{
			ptr = stackalloc SimMessages.ModifyElementEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyElementEmitterMessage)];
			ptr->handle = sim_handle;
			ptr->cellIdx = game_cell;
			ptr->emitInterval = emit_interval;
			ptr->emitMass = emit_mass;
			ptr->emitTemperature = emit_temperature;
			ptr->maxPressure = max_pressure;
			ptr->elementIdx = elementIndex;
		}
		ptr->maxDepth = (byte)max_depth;
		ptr->diseaseIdx = disease_idx;
		ptr->diseaseCount = disease_count;
		Sim.SIM_HandleMessage(403589164, sizeof(SimMessages.ModifyElementEmitterMessage), (byte*)ptr);
	}

	// Token: 0x06004EAD RID: 20141 RVA: 0x001BF12C File Offset: 0x001BD32C
	public unsafe static void RemoveElementEmitter(int cb_handle, int sim_handle)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			Debug.Assert(false, "Invalid handle");
			return;
		}
		checked
		{
			SimMessages.RemoveElementEmitterMessage* ptr = stackalloc SimMessages.RemoveElementEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveElementEmitterMessage)];
			ptr->callbackIdx = cb_handle;
			ptr->handle = sim_handle;
			Sim.SIM_HandleMessage(-1524118282, sizeof(SimMessages.RemoveElementEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EAE RID: 20142 RVA: 0x001BF17C File Offset: 0x001BD37C
	public unsafe static void AddRadiationEmitter(int on_registered, int game_cell, short emitRadiusX, short emitRadiusY, float emitRads, float emitRate, float emitSpeed, float emitDirection, float emitAngle, RadiationEmitter.RadiationEmitterType emitType)
	{
		checked
		{
			SimMessages.AddRadiationEmitterMessage* ptr = stackalloc SimMessages.AddRadiationEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.AddRadiationEmitterMessage)];
			ptr->callbackIdx = on_registered;
			ptr->cell = game_cell;
			ptr->emitRadiusX = emitRadiusX;
			ptr->emitRadiusY = emitRadiusY;
			ptr->emitRads = emitRads;
			ptr->emitRate = emitRate;
			ptr->emitSpeed = emitSpeed;
			ptr->emitDirection = emitDirection;
			ptr->emitAngle = emitAngle;
			ptr->emitType = (int)emitType;
			Sim.SIM_HandleMessage(-1505895314, sizeof(SimMessages.AddRadiationEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EAF RID: 20143 RVA: 0x001BF1F4 File Offset: 0x001BD3F4
	public unsafe static void ModifyRadiationEmitter(int sim_handle, int game_cell, short emitRadiusX, short emitRadiusY, float emitRads, float emitRate, float emitSpeed, float emitDirection, float emitAngle, RadiationEmitter.RadiationEmitterType emitType)
	{
		if (!Grid.IsValidCell(game_cell))
		{
			return;
		}
		checked
		{
			SimMessages.ModifyRadiationEmitterMessage* ptr = stackalloc SimMessages.ModifyRadiationEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyRadiationEmitterMessage)];
			ptr->handle = sim_handle;
			ptr->cell = game_cell;
			ptr->callbackIdx = -1;
			ptr->emitRadiusX = emitRadiusX;
			ptr->emitRadiusY = emitRadiusY;
			ptr->emitRads = emitRads;
			ptr->emitRate = emitRate;
			ptr->emitSpeed = emitSpeed;
			ptr->emitDirection = emitDirection;
			ptr->emitAngle = emitAngle;
			ptr->emitType = (int)emitType;
			Sim.SIM_HandleMessage(-503965465, sizeof(SimMessages.ModifyRadiationEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EB0 RID: 20144 RVA: 0x001BF27C File Offset: 0x001BD47C
	public unsafe static void RemoveRadiationEmitter(int cb_handle, int sim_handle)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			Debug.Assert(false, "Invalid handle");
			return;
		}
		checked
		{
			SimMessages.RemoveRadiationEmitterMessage* ptr = stackalloc SimMessages.RemoveRadiationEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveRadiationEmitterMessage)];
			ptr->callbackIdx = cb_handle;
			ptr->handle = sim_handle;
			Sim.SIM_HandleMessage(-704259919, sizeof(SimMessages.RemoveRadiationEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EB1 RID: 20145 RVA: 0x001BF2CC File Offset: 0x001BD4CC
	public unsafe static void AddElementChunk(int gameCell, SimHashes element, float mass, float temperature, float surface_area, float thickness, float ground_transfer_scale, int cb_handle)
	{
		Debug.Assert(Grid.IsValidCell(gameCell));
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		if (mass * temperature > 0f)
		{
			ushort elementIndex = ElementLoader.GetElementIndex(element);
			checked
			{
				SimMessages.AddElementChunkMessage* ptr = stackalloc SimMessages.AddElementChunkMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.AddElementChunkMessage)];
				ptr->gameCell = gameCell;
				ptr->callbackIdx = cb_handle;
				ptr->mass = mass;
				ptr->temperature = temperature;
				ptr->surfaceArea = surface_area;
				ptr->thickness = thickness;
				ptr->groundTransferScale = ground_transfer_scale;
				ptr->elementIdx = elementIndex;
				Sim.SIM_HandleMessage(1445724082, sizeof(SimMessages.AddElementChunkMessage), (byte*)ptr);
			}
		}
	}

	// Token: 0x06004EB2 RID: 20146 RVA: 0x001BF358 File Offset: 0x001BD558
	public unsafe static void RemoveElementChunk(int sim_handle, int cb_handle)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			Debug.Assert(false, "Invalid handle");
			return;
		}
		checked
		{
			SimMessages.RemoveElementChunkMessage* ptr = stackalloc SimMessages.RemoveElementChunkMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveElementChunkMessage)];
			ptr->callbackIdx = cb_handle;
			ptr->handle = sim_handle;
			Sim.SIM_HandleMessage(-912908555, sizeof(SimMessages.RemoveElementChunkMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EB3 RID: 20147 RVA: 0x001BF3A8 File Offset: 0x001BD5A8
	public unsafe static void SetElementChunkData(int sim_handle, float temperature, float heat_capacity)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			return;
		}
		checked
		{
			SimMessages.SetElementChunkDataMessage* ptr = stackalloc SimMessages.SetElementChunkDataMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.SetElementChunkDataMessage)];
			ptr->handle = sim_handle;
			ptr->temperature = temperature;
			ptr->heatCapacity = heat_capacity;
			Sim.SIM_HandleMessage(-435115907, sizeof(SimMessages.SetElementChunkDataMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EB4 RID: 20148 RVA: 0x001BF3F4 File Offset: 0x001BD5F4
	public unsafe static void MoveElementChunk(int sim_handle, int cell)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			Debug.Assert(false, "Invalid handle");
			return;
		}
		checked
		{
			SimMessages.MoveElementChunkMessage* ptr = stackalloc SimMessages.MoveElementChunkMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.MoveElementChunkMessage)];
			ptr->handle = sim_handle;
			ptr->gameCell = cell;
			Sim.SIM_HandleMessage(-374911358, sizeof(SimMessages.MoveElementChunkMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EB5 RID: 20149 RVA: 0x001BF444 File Offset: 0x001BD644
	public unsafe static void ModifyElementChunkEnergy(int sim_handle, float delta_kj)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			Debug.Assert(false, "Invalid handle");
			return;
		}
		checked
		{
			SimMessages.ModifyElementChunkEnergyMessage* ptr = stackalloc SimMessages.ModifyElementChunkEnergyMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyElementChunkEnergyMessage)];
			ptr->handle = sim_handle;
			ptr->deltaKJ = delta_kj;
			Sim.SIM_HandleMessage(1020555667, sizeof(SimMessages.ModifyElementChunkEnergyMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EB6 RID: 20150 RVA: 0x001BF494 File Offset: 0x001BD694
	public unsafe static void ModifyElementChunkTemperatureAdjuster(int sim_handle, float temperature, float heat_capacity, float thermal_conductivity)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			Debug.Assert(false, "Invalid handle");
			return;
		}
		checked
		{
			SimMessages.ModifyElementChunkAdjusterMessage* ptr = stackalloc SimMessages.ModifyElementChunkAdjusterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyElementChunkAdjusterMessage)];
			ptr->handle = sim_handle;
			ptr->temperature = temperature;
			ptr->heatCapacity = heat_capacity;
			ptr->thermalConductivity = thermal_conductivity;
			Sim.SIM_HandleMessage(-1387601379, sizeof(SimMessages.ModifyElementChunkAdjusterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EB7 RID: 20151 RVA: 0x001BF4F0 File Offset: 0x001BD6F0
	public unsafe static void AddBuildingHeatExchange(Extents extents, float mass, float temperature, float thermal_conductivity, float operating_kw, ushort elem_idx, int callbackIdx = -1)
	{
		if (!Grid.IsValidCell(Grid.XYToCell(extents.x, extents.y)))
		{
			return;
		}
		int num = Grid.XYToCell(extents.x + extents.width, extents.y + extents.height);
		if (!Grid.IsValidCell(num))
		{
			Debug.LogErrorFormat("Invalid Cell [{0}] Extents [{1},{2}] [{3},{4}]", new object[] { num, extents.x, extents.y, extents.width, extents.height });
		}
		if (!Grid.IsValidCell(num))
		{
			return;
		}
		SimMessages.AddBuildingHeatExchangeMessage* ptr;
		checked
		{
			ptr = stackalloc SimMessages.AddBuildingHeatExchangeMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.AddBuildingHeatExchangeMessage)];
			ptr->callbackIdx = callbackIdx;
			ptr->elemIdx = elem_idx;
			ptr->mass = mass;
			ptr->temperature = temperature;
			ptr->thermalConductivity = thermal_conductivity;
			ptr->overheatTemperature = float.MaxValue;
			ptr->operatingKilowatts = operating_kw;
			ptr->minX = extents.x;
			ptr->minY = extents.y;
		}
		ptr->maxX = extents.x + extents.width;
		ptr->maxY = extents.y + extents.height;
		Sim.SIM_HandleMessage(1739021608, sizeof(SimMessages.AddBuildingHeatExchangeMessage), (byte*)ptr);
	}

	// Token: 0x06004EB8 RID: 20152 RVA: 0x001BF62C File Offset: 0x001BD82C
	public unsafe static void ModifyBuildingHeatExchange(int sim_handle, Extents extents, float mass, float temperature, float thermal_conductivity, float overheat_temperature, float operating_kw, ushort element_idx)
	{
		int num = Grid.XYToCell(extents.x, extents.y);
		Debug.Assert(Grid.IsValidCell(num));
		if (!Grid.IsValidCell(num))
		{
			return;
		}
		int num2 = Grid.XYToCell(extents.x + extents.width, extents.y + extents.height);
		Debug.Assert(Grid.IsValidCell(num2));
		if (!Grid.IsValidCell(num2))
		{
			return;
		}
		SimMessages.ModifyBuildingHeatExchangeMessage* ptr;
		checked
		{
			ptr = stackalloc SimMessages.ModifyBuildingHeatExchangeMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyBuildingHeatExchangeMessage)];
			ptr->callbackIdx = sim_handle;
			ptr->elemIdx = element_idx;
			ptr->mass = mass;
			ptr->temperature = temperature;
			ptr->thermalConductivity = thermal_conductivity;
			ptr->overheatTemperature = overheat_temperature;
			ptr->operatingKilowatts = operating_kw;
			ptr->minX = extents.x;
			ptr->minY = extents.y;
		}
		ptr->maxX = extents.x + extents.width;
		ptr->maxY = extents.y + extents.height;
		Sim.SIM_HandleMessage(1818001569, sizeof(SimMessages.ModifyBuildingHeatExchangeMessage), (byte*)ptr);
	}

	// Token: 0x06004EB9 RID: 20153 RVA: 0x001BF720 File Offset: 0x001BD920
	public unsafe static void RemoveBuildingHeatExchange(int sim_handle, int callbackIdx = -1)
	{
		checked
		{
			SimMessages.RemoveBuildingHeatExchangeMessage* ptr = stackalloc SimMessages.RemoveBuildingHeatExchangeMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveBuildingHeatExchangeMessage)];
			Debug.Assert(Sim.IsValidHandle(sim_handle));
			ptr->handle = sim_handle;
			ptr->callbackIdx = callbackIdx;
			Sim.SIM_HandleMessage(-456116629, sizeof(SimMessages.RemoveBuildingHeatExchangeMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EBA RID: 20154 RVA: 0x001BF764 File Offset: 0x001BD964
	public unsafe static void ModifyBuildingEnergy(int sim_handle, float delta_kj, float min_temperature, float max_temperature)
	{
		checked
		{
			SimMessages.ModifyBuildingEnergyMessage* ptr = stackalloc SimMessages.ModifyBuildingEnergyMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyBuildingEnergyMessage)];
			Debug.Assert(Sim.IsValidHandle(sim_handle));
			ptr->handle = sim_handle;
			ptr->deltaKJ = delta_kj;
			ptr->minTemperature = min_temperature;
			ptr->maxTemperature = max_temperature;
			Sim.SIM_HandleMessage(-1348791658, sizeof(SimMessages.ModifyBuildingEnergyMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EBB RID: 20155 RVA: 0x001BF7B8 File Offset: 0x001BD9B8
	public unsafe static void RegisterBuildingToBuildingHeatExchange(int structureTemperatureHandler, int callbackIdx = -1)
	{
		checked
		{
			SimMessages.RegisterBuildingToBuildingHeatExchangeMessage* ptr = stackalloc SimMessages.RegisterBuildingToBuildingHeatExchangeMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RegisterBuildingToBuildingHeatExchangeMessage)];
			ptr->structureTemperatureHandler = structureTemperatureHandler;
			ptr->callbackIdx = callbackIdx;
			Sim.SIM_HandleMessage(-1338718217, sizeof(SimMessages.RegisterBuildingToBuildingHeatExchangeMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EBC RID: 20156 RVA: 0x001BF7F4 File Offset: 0x001BD9F4
	public unsafe static void AddBuildingToBuildingHeatExchange(int selfHandler, int buildingInContact, int cellsInContact)
	{
		checked
		{
			SimMessages.AddBuildingToBuildingHeatExchangeMessage* ptr = stackalloc SimMessages.AddBuildingToBuildingHeatExchangeMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.AddBuildingToBuildingHeatExchangeMessage)];
			ptr->selfHandler = selfHandler;
			ptr->buildingInContactHandle = buildingInContact;
			ptr->cellsInContact = cellsInContact;
			Sim.SIM_HandleMessage(-1586724321, sizeof(SimMessages.AddBuildingToBuildingHeatExchangeMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EBD RID: 20157 RVA: 0x001BF834 File Offset: 0x001BDA34
	public unsafe static void RemoveBuildingInContactFromBuildingToBuildingHeatExchange(int selfHandler, int buildingToRemove)
	{
		checked
		{
			SimMessages.RemoveBuildingInContactFromBuildingToBuildingHeatExchangeMessage* ptr = stackalloc SimMessages.RemoveBuildingInContactFromBuildingToBuildingHeatExchangeMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveBuildingInContactFromBuildingToBuildingHeatExchangeMessage)];
			ptr->selfHandler = selfHandler;
			ptr->buildingNoLongerInContactHandler = buildingToRemove;
			Sim.SIM_HandleMessage(-1993857213, sizeof(SimMessages.RemoveBuildingInContactFromBuildingToBuildingHeatExchangeMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EBE RID: 20158 RVA: 0x001BF870 File Offset: 0x001BDA70
	public unsafe static void RemoveBuildingToBuildingHeatExchange(int selfHandler, int callback = -1)
	{
		checked
		{
			SimMessages.RemoveBuildingToBuildingHeatExchangeMessage* ptr = stackalloc SimMessages.RemoveBuildingToBuildingHeatExchangeMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveBuildingToBuildingHeatExchangeMessage)];
			ptr->callbackIdx = callback;
			ptr->selfHandler = selfHandler;
			Sim.SIM_HandleMessage(697100730, sizeof(SimMessages.RemoveBuildingToBuildingHeatExchangeMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EBF RID: 20159 RVA: 0x001BF8AC File Offset: 0x001BDAAC
	public unsafe static void AddDiseaseEmitter(int callbackIdx)
	{
		checked
		{
			SimMessages.AddDiseaseEmitterMessage* ptr = stackalloc SimMessages.AddDiseaseEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.AddDiseaseEmitterMessage)];
			ptr->callbackIdx = callbackIdx;
			Sim.SIM_HandleMessage(1486783027, sizeof(SimMessages.AddDiseaseEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EC0 RID: 20160 RVA: 0x001BF8E0 File Offset: 0x001BDAE0
	public unsafe static void ModifyDiseaseEmitter(int sim_handle, int cell, byte range, byte disease_idx, float emit_interval, int emit_count)
	{
		Debug.Assert(Sim.IsValidHandle(sim_handle));
		checked
		{
			SimMessages.ModifyDiseaseEmitterMessage* ptr = stackalloc SimMessages.ModifyDiseaseEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyDiseaseEmitterMessage)];
			ptr->handle = sim_handle;
			ptr->gameCell = cell;
			ptr->maxDepth = range;
			ptr->diseaseIdx = disease_idx;
			ptr->emitInterval = emit_interval;
			ptr->emitCount = emit_count;
			Sim.SIM_HandleMessage(-1899123924, sizeof(SimMessages.ModifyDiseaseEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EC1 RID: 20161 RVA: 0x001BF944 File Offset: 0x001BDB44
	public unsafe static void RemoveDiseaseEmitter(int cb_handle, int sim_handle)
	{
		Debug.Assert(Sim.IsValidHandle(sim_handle));
		checked
		{
			SimMessages.RemoveDiseaseEmitterMessage* ptr = stackalloc SimMessages.RemoveDiseaseEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveDiseaseEmitterMessage)];
			ptr->handle = sim_handle;
			ptr->callbackIdx = cb_handle;
			Sim.SIM_HandleMessage(468135926, sizeof(SimMessages.RemoveDiseaseEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EC2 RID: 20162 RVA: 0x001BF988 File Offset: 0x001BDB88
	public unsafe static void SetSavedOptionValue(SimMessages.SimSavedOptions option, int zero_or_one)
	{
		checked
		{
			SimMessages.SetSavedOptionsMessage* ptr = stackalloc SimMessages.SetSavedOptionsMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.SetSavedOptionsMessage)];
			if (zero_or_one == 0)
			{
				SimMessages.SetSavedOptionsMessage* ptr2 = ptr;
				ptr2->clearBits = ptr2->clearBits | (byte)option;
				ptr->setBits = 0;
			}
			else
			{
				ptr->clearBits = 0;
				SimMessages.SetSavedOptionsMessage* ptr3 = ptr;
				ptr3->setBits = ptr3->setBits | (byte)option;
			}
			Sim.SIM_HandleMessage(1154135737, sizeof(SimMessages.SetSavedOptionsMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EC3 RID: 20163 RVA: 0x001BF9E0 File Offset: 0x001BDBE0
	private static void WriteKleiString(this BinaryWriter writer, string str)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(str);
		writer.Write(bytes.Length);
		if (bytes.Length != 0)
		{
			writer.Write(bytes);
		}
	}

	// Token: 0x06004EC4 RID: 20164 RVA: 0x001BFA10 File Offset: 0x001BDC10
	public unsafe static void CreateSimElementsTable(List<Element> elements)
	{
		MemoryStream memoryStream = new MemoryStream(Marshal.SizeOf(typeof(int)) + Marshal.SizeOf(typeof(Sim.Element)) * elements.Count);
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		Debug.Assert(elements.Count < 65535, "SimDLL internals assume there are fewer than 65535 elements");
		binaryWriter.Write(elements.Count);
		for (int i = 0; i < elements.Count; i++)
		{
			Sim.Element element = new Sim.Element(elements[i], elements);
			element.Write(binaryWriter);
		}
		for (int j = 0; j < elements.Count; j++)
		{
			binaryWriter.WriteKleiString(UI.StripLinkFormatting(elements[j].name));
		}
		byte[] buffer = memoryStream.GetBuffer();
		byte[] array;
		byte* ptr;
		if ((array = buffer) == null || array.Length == 0)
		{
			ptr = null;
		}
		else
		{
			ptr = &array[0];
		}
		Sim.SIM_HandleMessage(1108437482, buffer.Length, ptr);
		array = null;
	}

	// Token: 0x06004EC5 RID: 20165 RVA: 0x001BFB00 File Offset: 0x001BDD00
	public unsafe static void CreateDiseaseTable(Diseases diseases)
	{
		MemoryStream memoryStream = new MemoryStream(1024);
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(diseases.Count);
		List<Element> elements = ElementLoader.elements;
		binaryWriter.Write(elements.Count);
		for (int i = 0; i < diseases.Count; i++)
		{
			Disease disease = diseases[i];
			binaryWriter.WriteKleiString(UI.StripLinkFormatting(disease.Name));
			binaryWriter.Write(disease.id.GetHashCode());
			binaryWriter.Write(disease.strength);
			disease.temperatureRange.Write(binaryWriter);
			disease.temperatureHalfLives.Write(binaryWriter);
			disease.pressureRange.Write(binaryWriter);
			disease.pressureHalfLives.Write(binaryWriter);
			binaryWriter.Write(disease.radiationKillRate);
			for (int j = 0; j < elements.Count; j++)
			{
				ElemGrowthInfo elemGrowthInfo = disease.elemGrowthInfo[j];
				elemGrowthInfo.Write(binaryWriter);
			}
		}
		byte[] array;
		byte* ptr;
		if ((array = memoryStream.GetBuffer()) == null || array.Length == 0)
		{
			ptr = null;
		}
		else
		{
			ptr = &array[0];
		}
		Sim.SIM_HandleMessage(825301935, (int)memoryStream.Length, ptr);
		array = null;
	}

	// Token: 0x06004EC6 RID: 20166 RVA: 0x001BFC3C File Offset: 0x001BDE3C
	public unsafe static void DefineWorldOffsets(List<SimMessages.WorldOffsetData> worldOffsets)
	{
		MemoryStream memoryStream = new MemoryStream(1024);
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(worldOffsets.Count);
		foreach (SimMessages.WorldOffsetData worldOffsetData in worldOffsets)
		{
			binaryWriter.Write(worldOffsetData.worldOffsetX);
			binaryWriter.Write(worldOffsetData.worldOffsetY);
			binaryWriter.Write(worldOffsetData.worldSizeX);
			binaryWriter.Write(worldOffsetData.worldSizeY);
		}
		byte[] array;
		byte* ptr;
		if ((array = memoryStream.GetBuffer()) == null || array.Length == 0)
		{
			ptr = null;
		}
		else
		{
			ptr = &array[0];
		}
		Sim.SIM_HandleMessage(-895846551, (int)memoryStream.Length, ptr);
		array = null;
	}

	// Token: 0x06004EC7 RID: 20167 RVA: 0x001BFD0C File Offset: 0x001BDF0C
	public static void SimDataInitializeFromCells(int width, int height, Sim.Cell[] cells, float[] bgTemp, Sim.DiseaseCell[] dc, bool headless)
	{
		MemoryStream memoryStream = new MemoryStream(Marshal.SizeOf(typeof(int)) + Marshal.SizeOf(typeof(int)) + Marshal.SizeOf(typeof(Sim.Cell)) * width * height + Marshal.SizeOf(typeof(float)) * width * height + Marshal.SizeOf(typeof(Sim.DiseaseCell)) * width * height);
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(width);
		binaryWriter.Write(height);
		bool flag = Sim.IsRadiationEnabled();
		binaryWriter.Write(flag);
		binaryWriter.Write(headless);
		int num = width * height;
		for (int i = 0; i < num; i++)
		{
			cells[i].Write(binaryWriter);
		}
		for (int j = 0; j < num; j++)
		{
			binaryWriter.Write(bgTemp[j]);
		}
		for (int k = 0; k < num; k++)
		{
			dc[k].Write(binaryWriter);
		}
		byte[] buffer = memoryStream.GetBuffer();
		Sim.HandleMessage(SimMessageHashes.SimData_InitializeFromCells, buffer.Length, buffer);
	}

	// Token: 0x06004EC8 RID: 20168 RVA: 0x001BFE18 File Offset: 0x001BE018
	public static void SimDataResizeGridAndInitializeVacuumCells(Vector2I grid_size, int width, int height, int x_offset, int y_offset)
	{
		MemoryStream memoryStream = new MemoryStream(Marshal.SizeOf(typeof(int)) + Marshal.SizeOf(typeof(int)));
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(grid_size.x);
		binaryWriter.Write(grid_size.y);
		binaryWriter.Write(width);
		binaryWriter.Write(height);
		binaryWriter.Write(x_offset);
		binaryWriter.Write(y_offset);
		byte[] buffer = memoryStream.GetBuffer();
		Sim.HandleMessage(SimMessageHashes.SimData_ResizeAndInitializeVacuumCells, buffer.Length, buffer);
	}

	// Token: 0x06004EC9 RID: 20169 RVA: 0x001BFE98 File Offset: 0x001BE098
	public static void SimDataFreeCells(int width, int height, int x_offset, int y_offset)
	{
		MemoryStream memoryStream = new MemoryStream(Marshal.SizeOf(typeof(int)) + Marshal.SizeOf(typeof(int)));
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(width);
		binaryWriter.Write(height);
		binaryWriter.Write(x_offset);
		binaryWriter.Write(y_offset);
		byte[] buffer = memoryStream.GetBuffer();
		Sim.HandleMessage(SimMessageHashes.SimData_FreeCells, buffer.Length, buffer);
	}

	// Token: 0x06004ECA RID: 20170 RVA: 0x001BFF00 File Offset: 0x001BE100
	public unsafe static void Dig(int gameCell, int callbackIdx = -1, bool skipEvent = false)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		checked
		{
			SimMessages.DigMessage* ptr = stackalloc SimMessages.DigMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.DigMessage)];
			ptr->cellIdx = gameCell;
			ptr->callbackIdx = callbackIdx;
			ptr->skipEvent = skipEvent;
			Sim.SIM_HandleMessage(833038498, sizeof(SimMessages.DigMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004ECB RID: 20171 RVA: 0x001BFF4C File Offset: 0x001BE14C
	public unsafe static void SetInsulation(int gameCell, float value)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		checked
		{
			SimMessages.SetCellFloatValueMessage* ptr = stackalloc SimMessages.SetCellFloatValueMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.SetCellFloatValueMessage)];
			ptr->cellIdx = gameCell;
			ptr->value = value;
			Sim.SIM_HandleMessage(-898773121, sizeof(SimMessages.SetCellFloatValueMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004ECC RID: 20172 RVA: 0x001BFF90 File Offset: 0x001BE190
	public unsafe static void SetStrength(int gameCell, int weight, float strengthMultiplier)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		SimMessages.SetCellFloatValueMessage* ptr;
		checked
		{
			ptr = stackalloc SimMessages.SetCellFloatValueMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.SetCellFloatValueMessage)];
			ptr->cellIdx = gameCell;
		}
		int num = (int)(strengthMultiplier * 4f) & 127;
		int num2 = ((weight & 1) << 7) | num;
		ptr->value = (float)((byte)num2);
		Sim.SIM_HandleMessage(1593243982, sizeof(SimMessages.SetCellFloatValueMessage), (byte*)ptr);
	}

	// Token: 0x06004ECD RID: 20173 RVA: 0x001BFFE8 File Offset: 0x001BE1E8
	public unsafe static void SetCellProperties(int gameCell, byte properties)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		checked
		{
			SimMessages.CellPropertiesMessage* ptr = stackalloc SimMessages.CellPropertiesMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.CellPropertiesMessage)];
			ptr->cellIdx = gameCell;
			ptr->properties = properties;
			ptr->set = 1;
			Sim.SIM_HandleMessage(-469311643, sizeof(SimMessages.CellPropertiesMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004ECE RID: 20174 RVA: 0x001C0034 File Offset: 0x001BE234
	public unsafe static void ClearCellProperties(int gameCell, byte properties)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		checked
		{
			SimMessages.CellPropertiesMessage* ptr = stackalloc SimMessages.CellPropertiesMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.CellPropertiesMessage)];
			ptr->cellIdx = gameCell;
			ptr->properties = properties;
			ptr->set = 0;
			Sim.SIM_HandleMessage(-469311643, sizeof(SimMessages.CellPropertiesMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004ECF RID: 20175 RVA: 0x001C0080 File Offset: 0x001BE280
	public unsafe static void ModifyCell(int gameCell, ushort elementIdx, float temperature, float mass, byte disease_idx, int disease_count, SimMessages.ReplaceType replace_type = SimMessages.ReplaceType.None, bool do_vertical_solid_displacement = false, int callbackIdx = -1)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		Element element = ElementLoader.elements[(int)elementIdx];
		if (element.maxMass == 0f && mass > element.maxMass)
		{
			Debug.LogWarningFormat("Invalid cell modification (mass greater than element maximum): Cell={0}, EIdx={1}, T={2}, M={3}, {4} max mass = {5}", new object[] { gameCell, elementIdx, temperature, mass, element.id, element.maxMass });
			mass = element.maxMass;
		}
		if (temperature < 0f || temperature > 10000f)
		{
			Debug.LogWarningFormat("Invalid cell modification (temp out of bounds): Cell={0}, EIdx={1}, T={2}, M={3}, {4} default temp = {5}", new object[]
			{
				gameCell,
				elementIdx,
				temperature,
				mass,
				element.id,
				element.defaultValues.temperature
			});
			temperature = element.defaultValues.temperature;
		}
		if (temperature == 0f && mass > 0f)
		{
			Debug.LogWarningFormat("Invalid cell modification (zero temp with non-zero mass): Cell={0}, EIdx={1}, T={2}, M={3}, {4} default temp = {5}", new object[]
			{
				gameCell,
				elementIdx,
				temperature,
				mass,
				element.id,
				element.defaultValues.temperature
			});
			temperature = element.defaultValues.temperature;
		}
		SimMessages.ModifyCellMessage* ptr;
		checked
		{
			ptr = stackalloc SimMessages.ModifyCellMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyCellMessage)];
			ptr->cellIdx = gameCell;
			ptr->callbackIdx = callbackIdx;
			ptr->temperature = temperature;
			ptr->mass = mass;
			ptr->elementIdx = elementIdx;
		}
		ptr->replaceType = (byte)replace_type;
		ptr->diseaseIdx = disease_idx;
		ptr->diseaseCount = disease_count;
		ptr->addSubType = (do_vertical_solid_displacement ? 0 : 1);
		Sim.SIM_HandleMessage(-1252920804, sizeof(SimMessages.ModifyCellMessage), (byte*)ptr);
	}

	// Token: 0x06004ED0 RID: 20176 RVA: 0x001C0260 File Offset: 0x001BE460
	public unsafe static void ModifyDiseaseOnCell(int gameCell, byte disease_idx, int disease_delta)
	{
		checked
		{
			SimMessages.CellDiseaseModification* ptr = stackalloc SimMessages.CellDiseaseModification[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.CellDiseaseModification)];
			ptr->cellIdx = gameCell;
			ptr->diseaseIdx = disease_idx;
			ptr->diseaseCount = disease_delta;
			Sim.SIM_HandleMessage(-1853671274, sizeof(SimMessages.CellDiseaseModification), (byte*)ptr);
		}
	}

	// Token: 0x06004ED1 RID: 20177 RVA: 0x001C02A0 File Offset: 0x001BE4A0
	public unsafe static void ModifyRadiationOnCell(int gameCell, float radiationDelta, int callbackIdx = -1)
	{
		checked
		{
			SimMessages.CellRadiationModification* ptr = stackalloc SimMessages.CellRadiationModification[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.CellRadiationModification)];
			ptr->cellIdx = gameCell;
			ptr->radiationDelta = radiationDelta;
			ptr->callbackIdx = callbackIdx;
			Sim.SIM_HandleMessage(-1914877797, sizeof(SimMessages.CellRadiationModification), (byte*)ptr);
		}
	}

	// Token: 0x06004ED2 RID: 20178 RVA: 0x001C02E0 File Offset: 0x001BE4E0
	public unsafe static void ModifyRadiationParams(RadiationParams type, float value)
	{
		checked
		{
			SimMessages.RadiationParamsModification* ptr = stackalloc SimMessages.RadiationParamsModification[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RadiationParamsModification)];
			ptr->RadiationParamsType = (int)type;
			ptr->value = value;
			Sim.SIM_HandleMessage(377112707, sizeof(SimMessages.RadiationParamsModification), (byte*)ptr);
		}
	}

	// Token: 0x06004ED3 RID: 20179 RVA: 0x001C0319 File Offset: 0x001BE519
	public static ushort GetElementIndex(SimHashes element)
	{
		return ElementLoader.GetElementIndex(element);
	}

	// Token: 0x06004ED4 RID: 20180 RVA: 0x001C0324 File Offset: 0x001BE524
	public unsafe static void ConsumeMass(int gameCell, SimHashes element, float mass, byte radius, int callbackIdx = -1)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		ushort elementIndex = ElementLoader.GetElementIndex(element);
		checked
		{
			SimMessages.MassConsumptionMessage* ptr = stackalloc SimMessages.MassConsumptionMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.MassConsumptionMessage)];
			ptr->cellIdx = gameCell;
			ptr->callbackIdx = callbackIdx;
			ptr->mass = mass;
			ptr->elementIdx = elementIndex;
			ptr->radius = radius;
			Sim.SIM_HandleMessage(1727657959, sizeof(SimMessages.MassConsumptionMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004ED5 RID: 20181 RVA: 0x001C0384 File Offset: 0x001BE584
	public unsafe static void EmitMass(int gameCell, ushort element_idx, float mass, float temperature, byte disease_idx, int disease_count, int callbackIdx = -1)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		checked
		{
			SimMessages.MassEmissionMessage* ptr = stackalloc SimMessages.MassEmissionMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.MassEmissionMessage)];
			ptr->cellIdx = gameCell;
			ptr->callbackIdx = callbackIdx;
			ptr->mass = mass;
			ptr->temperature = temperature;
			ptr->elementIdx = element_idx;
			ptr->diseaseIdx = disease_idx;
			ptr->diseaseCount = disease_count;
			Sim.SIM_HandleMessage(797274363, sizeof(SimMessages.MassEmissionMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004ED6 RID: 20182 RVA: 0x001C03EC File Offset: 0x001BE5EC
	public unsafe static void ConsumeDisease(int game_cell, float percent_to_consume, int max_to_consume, int callback_idx)
	{
		if (!Grid.IsValidCell(game_cell))
		{
			return;
		}
		checked
		{
			SimMessages.ConsumeDiseaseMessage* ptr = stackalloc SimMessages.ConsumeDiseaseMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ConsumeDiseaseMessage)];
			ptr->callbackIdx = callback_idx;
			ptr->gameCell = game_cell;
			ptr->percentToConsume = percent_to_consume;
			ptr->maxToConsume = max_to_consume;
			Sim.SIM_HandleMessage(-1019841536, sizeof(SimMessages.ConsumeDiseaseMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004ED7 RID: 20183 RVA: 0x001C043C File Offset: 0x001BE63C
	public static void AddRemoveSubstance(int gameCell, SimHashes new_element, CellAddRemoveSubstanceEvent ev, float mass, float temperature, byte disease_idx, int disease_count, bool do_vertical_solid_displacement = true, int callbackIdx = -1)
	{
		ushort elementIndex = SimMessages.GetElementIndex(new_element);
		SimMessages.AddRemoveSubstance(gameCell, elementIndex, ev, mass, temperature, disease_idx, disease_count, do_vertical_solid_displacement, callbackIdx);
	}

	// Token: 0x06004ED8 RID: 20184 RVA: 0x001C0464 File Offset: 0x001BE664
	public static void AddRemoveSubstance(int gameCell, ushort elementIdx, CellAddRemoveSubstanceEvent ev, float mass, float temperature, byte disease_idx, int disease_count, bool do_vertical_solid_displacement = true, int callbackIdx = -1)
	{
		if (elementIdx == 65535)
		{
			return;
		}
		Element element = ElementLoader.elements[(int)elementIdx];
		float num = ((temperature != -1f) ? temperature : element.defaultValues.temperature);
		SimMessages.ModifyCell(gameCell, elementIdx, num, mass, disease_idx, disease_count, SimMessages.ReplaceType.None, do_vertical_solid_displacement, callbackIdx);
	}

	// Token: 0x06004ED9 RID: 20185 RVA: 0x001C04B4 File Offset: 0x001BE6B4
	public static void ReplaceElement(int gameCell, SimHashes new_element, CellElementEvent ev, float mass, float temperature = -1f, byte diseaseIdx = 255, int diseaseCount = 0, int callbackIdx = -1)
	{
		ushort elementIndex = SimMessages.GetElementIndex(new_element);
		if (elementIndex != 65535)
		{
			Element element = ElementLoader.elements[(int)elementIndex];
			float num = ((temperature != -1f) ? temperature : element.defaultValues.temperature);
			SimMessages.ModifyCell(gameCell, elementIndex, num, mass, diseaseIdx, diseaseCount, SimMessages.ReplaceType.Replace, false, callbackIdx);
		}
	}

	// Token: 0x06004EDA RID: 20186 RVA: 0x001C0508 File Offset: 0x001BE708
	public static void ReplaceAndDisplaceElement(int gameCell, SimHashes new_element, CellElementEvent ev, float mass, float temperature = -1f, byte disease_idx = 255, int disease_count = 0, int callbackIdx = -1)
	{
		ushort elementIndex = SimMessages.GetElementIndex(new_element);
		if (elementIndex != 65535)
		{
			Element element = ElementLoader.elements[(int)elementIndex];
			float num = ((temperature != -1f) ? temperature : element.defaultValues.temperature);
			SimMessages.ModifyCell(gameCell, elementIndex, num, mass, disease_idx, disease_count, SimMessages.ReplaceType.ReplaceAndDisplace, false, callbackIdx);
		}
	}

	// Token: 0x06004EDB RID: 20187 RVA: 0x001C055C File Offset: 0x001BE75C
	public unsafe static void ModifyEnergy(int gameCell, float kilojoules, float max_temperature, SimMessages.EnergySourceID id)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		if (max_temperature <= 0f)
		{
			Debug.LogError("invalid max temperature for cell energy modification");
			return;
		}
		checked
		{
			SimMessages.ModifyCellEnergyMessage* ptr = stackalloc SimMessages.ModifyCellEnergyMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyCellEnergyMessage)];
			ptr->cellIdx = gameCell;
			ptr->kilojoules = kilojoules;
			ptr->maxTemperature = max_temperature;
			ptr->id = (int)id;
			Sim.SIM_HandleMessage(818320644, sizeof(SimMessages.ModifyCellEnergyMessage), (byte*)ptr);
		}
	}

	// Token: 0x06004EDC RID: 20188 RVA: 0x001C05C0 File Offset: 0x001BE7C0
	public static void ModifyMass(int gameCell, float mass, byte disease_idx, int disease_count, CellModifyMassEvent ev, float temperature = -1f, SimHashes element = SimHashes.Vacuum)
	{
		if (element != SimHashes.Vacuum)
		{
			ushort elementIndex = SimMessages.GetElementIndex(element);
			if (elementIndex != 65535)
			{
				if (temperature == -1f)
				{
					temperature = ElementLoader.elements[(int)elementIndex].defaultValues.temperature;
				}
				SimMessages.ModifyCell(gameCell, elementIndex, temperature, mass, disease_idx, disease_count, SimMessages.ReplaceType.None, false, -1);
				return;
			}
		}
		else
		{
			SimMessages.ModifyCell(gameCell, 0, temperature, mass, disease_idx, disease_count, SimMessages.ReplaceType.None, false, -1);
		}
	}

	// Token: 0x06004EDD RID: 20189 RVA: 0x001C0628 File Offset: 0x001BE828
	public unsafe static void CreateElementInteractions(SimMessages.ElementInteraction[] interactions)
	{
		checked
		{
			fixed (SimMessages.ElementInteraction[] array = interactions)
			{
				SimMessages.ElementInteraction* ptr;
				if (interactions == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				SimMessages.CreateElementInteractionsMsg* ptr2 = stackalloc SimMessages.CreateElementInteractionsMsg[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.CreateElementInteractionsMsg)];
				ptr2->numInteractions = interactions.Length;
				ptr2->interactions = ptr;
				Sim.SIM_HandleMessage(-930289787, sizeof(SimMessages.CreateElementInteractionsMsg), (byte*)ptr2);
			}
		}
	}

	// Token: 0x06004EDE RID: 20190 RVA: 0x001C0680 File Offset: 0x001BE880
	public unsafe static void NewGameFrame(float elapsed_seconds, List<Game.SimActiveRegion> activeRegions)
	{
		Debug.Assert(activeRegions.Count > 0, "NewGameFrame cannot be called with zero activeRegions");
		Sim.NewGameFrame* ptr;
		Sim.NewGameFrame* ptr2;
		checked
		{
			ptr = stackalloc Sim.NewGameFrame[unchecked((UIntPtr)activeRegions.Count) * (UIntPtr)sizeof(Sim.NewGameFrame)];
			ptr2 = ptr;
		}
		foreach (Game.SimActiveRegion simActiveRegion in activeRegions)
		{
			Pair<Vector2I, Vector2I> region = simActiveRegion.region;
			region.first = new Vector2I(MathUtil.Clamp(0, Grid.WidthInCells - 1, simActiveRegion.region.first.x), MathUtil.Clamp(0, Grid.HeightInCells - 1, simActiveRegion.region.first.y));
			region.second = new Vector2I(MathUtil.Clamp(0, Grid.WidthInCells - 1, simActiveRegion.region.second.x), MathUtil.Clamp(0, Grid.HeightInCells - 1, simActiveRegion.region.second.y));
			ptr2->elapsedSeconds = elapsed_seconds;
			ptr2->minX = region.first.x;
			ptr2->minY = region.first.y;
			ptr2->maxX = region.second.x;
			ptr2->maxY = region.second.y;
			ptr2->currentSunlightIntensity = simActiveRegion.currentSunlightIntensity;
			ptr2->currentCosmicRadiationIntensity = simActiveRegion.currentCosmicRadiationIntensity;
			ptr2++;
		}
		Sim.SIM_HandleMessage(-775326397, sizeof(Sim.NewGameFrame) * activeRegions.Count, (byte*)ptr);
	}

	// Token: 0x06004EDF RID: 20191 RVA: 0x001C081C File Offset: 0x001BEA1C
	public unsafe static void SetDebugProperties(Sim.DebugProperties properties)
	{
		checked
		{
			Sim.DebugProperties* ptr = stackalloc Sim.DebugProperties[unchecked((UIntPtr)1) * (UIntPtr)sizeof(Sim.DebugProperties)];
			*ptr = properties;
			ptr->buildingTemperatureScale = properties.buildingTemperatureScale;
			ptr->buildingToBuildingTemperatureScale = properties.buildingToBuildingTemperatureScale;
			Sim.SIM_HandleMessage(-1683118492, sizeof(Sim.DebugProperties), (byte*)ptr);
		}
	}

	// Token: 0x06004EE0 RID: 20192 RVA: 0x001C0868 File Offset: 0x001BEA68
	public unsafe static void ModifyCellWorldZone(int cell, byte zone_id)
	{
		checked
		{
			SimMessages.CellWorldZoneModification* ptr = stackalloc SimMessages.CellWorldZoneModification[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.CellWorldZoneModification)];
			ptr->cell = cell;
			ptr->zoneID = zone_id;
			Sim.SIM_HandleMessage(-449718014, sizeof(SimMessages.CellWorldZoneModification), (byte*)ptr);
		}
	}

	// Token: 0x04003418 RID: 13336
	public const int InvalidCallback = -1;

	// Token: 0x04003419 RID: 13337
	public const float STATE_TRANSITION_TEMPERATURE_BUFER = 3f;

	// Token: 0x02001888 RID: 6280
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct AddElementConsumerMessage
	{
		// Token: 0x04007148 RID: 29000
		public int cellIdx;

		// Token: 0x04007149 RID: 29001
		public int callbackIdx;

		// Token: 0x0400714A RID: 29002
		public byte radius;

		// Token: 0x0400714B RID: 29003
		public byte configuration;

		// Token: 0x0400714C RID: 29004
		public ushort elementIdx;
	}

	// Token: 0x02001889 RID: 6281
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct SetElementConsumerDataMessage
	{
		// Token: 0x0400714D RID: 29005
		public int handle;

		// Token: 0x0400714E RID: 29006
		public int cell;

		// Token: 0x0400714F RID: 29007
		public float consumptionRate;
	}

	// Token: 0x0200188A RID: 6282
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct RemoveElementConsumerMessage
	{
		// Token: 0x04007150 RID: 29008
		public int handle;

		// Token: 0x04007151 RID: 29009
		public int callbackIdx;
	}

	// Token: 0x0200188B RID: 6283
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct AddElementEmitterMessage
	{
		// Token: 0x04007152 RID: 29010
		public float maxPressure;

		// Token: 0x04007153 RID: 29011
		public int callbackIdx;

		// Token: 0x04007154 RID: 29012
		public int onBlockedCB;

		// Token: 0x04007155 RID: 29013
		public int onUnblockedCB;
	}

	// Token: 0x0200188C RID: 6284
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct ModifyElementEmitterMessage
	{
		// Token: 0x04007156 RID: 29014
		public int handle;

		// Token: 0x04007157 RID: 29015
		public int cellIdx;

		// Token: 0x04007158 RID: 29016
		public float emitInterval;

		// Token: 0x04007159 RID: 29017
		public float emitMass;

		// Token: 0x0400715A RID: 29018
		public float emitTemperature;

		// Token: 0x0400715B RID: 29019
		public float maxPressure;

		// Token: 0x0400715C RID: 29020
		public int diseaseCount;

		// Token: 0x0400715D RID: 29021
		public ushort elementIdx;

		// Token: 0x0400715E RID: 29022
		public byte maxDepth;

		// Token: 0x0400715F RID: 29023
		public byte diseaseIdx;
	}

	// Token: 0x0200188D RID: 6285
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct RemoveElementEmitterMessage
	{
		// Token: 0x04007160 RID: 29024
		public int handle;

		// Token: 0x04007161 RID: 29025
		public int callbackIdx;
	}

	// Token: 0x0200188E RID: 6286
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct AddRadiationEmitterMessage
	{
		// Token: 0x04007162 RID: 29026
		public int callbackIdx;

		// Token: 0x04007163 RID: 29027
		public int cell;

		// Token: 0x04007164 RID: 29028
		public short emitRadiusX;

		// Token: 0x04007165 RID: 29029
		public short emitRadiusY;

		// Token: 0x04007166 RID: 29030
		public float emitRads;

		// Token: 0x04007167 RID: 29031
		public float emitRate;

		// Token: 0x04007168 RID: 29032
		public float emitSpeed;

		// Token: 0x04007169 RID: 29033
		public float emitDirection;

		// Token: 0x0400716A RID: 29034
		public float emitAngle;

		// Token: 0x0400716B RID: 29035
		public int emitType;
	}

	// Token: 0x0200188F RID: 6287
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct ModifyRadiationEmitterMessage
	{
		// Token: 0x0400716C RID: 29036
		public int handle;

		// Token: 0x0400716D RID: 29037
		public int cell;

		// Token: 0x0400716E RID: 29038
		public int callbackIdx;

		// Token: 0x0400716F RID: 29039
		public short emitRadiusX;

		// Token: 0x04007170 RID: 29040
		public short emitRadiusY;

		// Token: 0x04007171 RID: 29041
		public float emitRads;

		// Token: 0x04007172 RID: 29042
		public float emitRate;

		// Token: 0x04007173 RID: 29043
		public float emitSpeed;

		// Token: 0x04007174 RID: 29044
		public float emitDirection;

		// Token: 0x04007175 RID: 29045
		public float emitAngle;

		// Token: 0x04007176 RID: 29046
		public int emitType;
	}

	// Token: 0x02001890 RID: 6288
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct RemoveRadiationEmitterMessage
	{
		// Token: 0x04007177 RID: 29047
		public int handle;

		// Token: 0x04007178 RID: 29048
		public int callbackIdx;
	}

	// Token: 0x02001891 RID: 6289
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct AddElementChunkMessage
	{
		// Token: 0x04007179 RID: 29049
		public int gameCell;

		// Token: 0x0400717A RID: 29050
		public int callbackIdx;

		// Token: 0x0400717B RID: 29051
		public float mass;

		// Token: 0x0400717C RID: 29052
		public float temperature;

		// Token: 0x0400717D RID: 29053
		public float surfaceArea;

		// Token: 0x0400717E RID: 29054
		public float thickness;

		// Token: 0x0400717F RID: 29055
		public float groundTransferScale;

		// Token: 0x04007180 RID: 29056
		public ushort elementIdx;

		// Token: 0x04007181 RID: 29057
		public byte pad0;

		// Token: 0x04007182 RID: 29058
		public byte pad1;
	}

	// Token: 0x02001892 RID: 6290
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct RemoveElementChunkMessage
	{
		// Token: 0x04007183 RID: 29059
		public int handle;

		// Token: 0x04007184 RID: 29060
		public int callbackIdx;
	}

	// Token: 0x02001893 RID: 6291
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct SetElementChunkDataMessage
	{
		// Token: 0x04007185 RID: 29061
		public int handle;

		// Token: 0x04007186 RID: 29062
		public float temperature;

		// Token: 0x04007187 RID: 29063
		public float heatCapacity;
	}

	// Token: 0x02001894 RID: 6292
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct MoveElementChunkMessage
	{
		// Token: 0x04007188 RID: 29064
		public int handle;

		// Token: 0x04007189 RID: 29065
		public int gameCell;
	}

	// Token: 0x02001895 RID: 6293
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct ModifyElementChunkEnergyMessage
	{
		// Token: 0x0400718A RID: 29066
		public int handle;

		// Token: 0x0400718B RID: 29067
		public float deltaKJ;
	}

	// Token: 0x02001896 RID: 6294
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct ModifyElementChunkAdjusterMessage
	{
		// Token: 0x0400718C RID: 29068
		public int handle;

		// Token: 0x0400718D RID: 29069
		public float temperature;

		// Token: 0x0400718E RID: 29070
		public float heatCapacity;

		// Token: 0x0400718F RID: 29071
		public float thermalConductivity;
	}

	// Token: 0x02001897 RID: 6295
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct AddBuildingHeatExchangeMessage
	{
		// Token: 0x04007190 RID: 29072
		public int callbackIdx;

		// Token: 0x04007191 RID: 29073
		public ushort elemIdx;

		// Token: 0x04007192 RID: 29074
		public byte pad0;

		// Token: 0x04007193 RID: 29075
		public byte pad1;

		// Token: 0x04007194 RID: 29076
		public float mass;

		// Token: 0x04007195 RID: 29077
		public float temperature;

		// Token: 0x04007196 RID: 29078
		public float thermalConductivity;

		// Token: 0x04007197 RID: 29079
		public float overheatTemperature;

		// Token: 0x04007198 RID: 29080
		public float operatingKilowatts;

		// Token: 0x04007199 RID: 29081
		public int minX;

		// Token: 0x0400719A RID: 29082
		public int minY;

		// Token: 0x0400719B RID: 29083
		public int maxX;

		// Token: 0x0400719C RID: 29084
		public int maxY;
	}

	// Token: 0x02001898 RID: 6296
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ModifyBuildingHeatExchangeMessage
	{
		// Token: 0x0400719D RID: 29085
		public int callbackIdx;

		// Token: 0x0400719E RID: 29086
		public ushort elemIdx;

		// Token: 0x0400719F RID: 29087
		public byte pad0;

		// Token: 0x040071A0 RID: 29088
		public byte pad1;

		// Token: 0x040071A1 RID: 29089
		public float mass;

		// Token: 0x040071A2 RID: 29090
		public float temperature;

		// Token: 0x040071A3 RID: 29091
		public float thermalConductivity;

		// Token: 0x040071A4 RID: 29092
		public float overheatTemperature;

		// Token: 0x040071A5 RID: 29093
		public float operatingKilowatts;

		// Token: 0x040071A6 RID: 29094
		public int minX;

		// Token: 0x040071A7 RID: 29095
		public int minY;

		// Token: 0x040071A8 RID: 29096
		public int maxX;

		// Token: 0x040071A9 RID: 29097
		public int maxY;
	}

	// Token: 0x02001899 RID: 6297
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ModifyBuildingEnergyMessage
	{
		// Token: 0x040071AA RID: 29098
		public int handle;

		// Token: 0x040071AB RID: 29099
		public float deltaKJ;

		// Token: 0x040071AC RID: 29100
		public float minTemperature;

		// Token: 0x040071AD RID: 29101
		public float maxTemperature;
	}

	// Token: 0x0200189A RID: 6298
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct RemoveBuildingHeatExchangeMessage
	{
		// Token: 0x040071AE RID: 29102
		public int handle;

		// Token: 0x040071AF RID: 29103
		public int callbackIdx;
	}

	// Token: 0x0200189B RID: 6299
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct RegisterBuildingToBuildingHeatExchangeMessage
	{
		// Token: 0x040071B0 RID: 29104
		public int callbackIdx;

		// Token: 0x040071B1 RID: 29105
		public int structureTemperatureHandler;
	}

	// Token: 0x0200189C RID: 6300
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct AddBuildingToBuildingHeatExchangeMessage
	{
		// Token: 0x040071B2 RID: 29106
		public int selfHandler;

		// Token: 0x040071B3 RID: 29107
		public int buildingInContactHandle;

		// Token: 0x040071B4 RID: 29108
		public int cellsInContact;
	}

	// Token: 0x0200189D RID: 6301
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct RemoveBuildingInContactFromBuildingToBuildingHeatExchangeMessage
	{
		// Token: 0x040071B5 RID: 29109
		public int selfHandler;

		// Token: 0x040071B6 RID: 29110
		public int buildingNoLongerInContactHandler;
	}

	// Token: 0x0200189E RID: 6302
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct RemoveBuildingToBuildingHeatExchangeMessage
	{
		// Token: 0x040071B7 RID: 29111
		public int callbackIdx;

		// Token: 0x040071B8 RID: 29112
		public int selfHandler;
	}

	// Token: 0x0200189F RID: 6303
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct AddDiseaseEmitterMessage
	{
		// Token: 0x040071B9 RID: 29113
		public int callbackIdx;
	}

	// Token: 0x020018A0 RID: 6304
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ModifyDiseaseEmitterMessage
	{
		// Token: 0x040071BA RID: 29114
		public int handle;

		// Token: 0x040071BB RID: 29115
		public int gameCell;

		// Token: 0x040071BC RID: 29116
		public byte diseaseIdx;

		// Token: 0x040071BD RID: 29117
		public byte maxDepth;

		// Token: 0x040071BE RID: 29118
		private byte pad0;

		// Token: 0x040071BF RID: 29119
		private byte pad1;

		// Token: 0x040071C0 RID: 29120
		public float emitInterval;

		// Token: 0x040071C1 RID: 29121
		public int emitCount;
	}

	// Token: 0x020018A1 RID: 6305
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct RemoveDiseaseEmitterMessage
	{
		// Token: 0x040071C2 RID: 29122
		public int handle;

		// Token: 0x040071C3 RID: 29123
		public int callbackIdx;
	}

	// Token: 0x020018A2 RID: 6306
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct SetSavedOptionsMessage
	{
		// Token: 0x040071C4 RID: 29124
		public byte clearBits;

		// Token: 0x040071C5 RID: 29125
		public byte setBits;
	}

	// Token: 0x020018A3 RID: 6307
	public enum SimSavedOptions : byte
	{
		// Token: 0x040071C7 RID: 29127
		ENABLE_DIAGONAL_FALLING_SAND = 1
	}

	// Token: 0x020018A4 RID: 6308
	public struct WorldOffsetData
	{
		// Token: 0x040071C8 RID: 29128
		public int worldOffsetX;

		// Token: 0x040071C9 RID: 29129
		public int worldOffsetY;

		// Token: 0x040071CA RID: 29130
		public int worldSizeX;

		// Token: 0x040071CB RID: 29131
		public int worldSizeY;
	}

	// Token: 0x020018A5 RID: 6309
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct DigMessage
	{
		// Token: 0x040071CC RID: 29132
		public int cellIdx;

		// Token: 0x040071CD RID: 29133
		public int callbackIdx;

		// Token: 0x040071CE RID: 29134
		public bool skipEvent;
	}

	// Token: 0x020018A6 RID: 6310
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct SetCellFloatValueMessage
	{
		// Token: 0x040071CF RID: 29135
		public int cellIdx;

		// Token: 0x040071D0 RID: 29136
		public float value;
	}

	// Token: 0x020018A7 RID: 6311
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct CellPropertiesMessage
	{
		// Token: 0x040071D1 RID: 29137
		public int cellIdx;

		// Token: 0x040071D2 RID: 29138
		public byte properties;

		// Token: 0x040071D3 RID: 29139
		public byte set;

		// Token: 0x040071D4 RID: 29140
		public byte pad0;

		// Token: 0x040071D5 RID: 29141
		public byte pad1;
	}

	// Token: 0x020018A8 RID: 6312
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct SetInsulationValueMessage
	{
		// Token: 0x040071D6 RID: 29142
		public int cellIdx;

		// Token: 0x040071D7 RID: 29143
		public float value;
	}

	// Token: 0x020018A9 RID: 6313
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct ModifyCellMessage
	{
		// Token: 0x040071D8 RID: 29144
		public int cellIdx;

		// Token: 0x040071D9 RID: 29145
		public int callbackIdx;

		// Token: 0x040071DA RID: 29146
		public float temperature;

		// Token: 0x040071DB RID: 29147
		public float mass;

		// Token: 0x040071DC RID: 29148
		public int diseaseCount;

		// Token: 0x040071DD RID: 29149
		public ushort elementIdx;

		// Token: 0x040071DE RID: 29150
		public byte replaceType;

		// Token: 0x040071DF RID: 29151
		public byte diseaseIdx;

		// Token: 0x040071E0 RID: 29152
		public byte addSubType;
	}

	// Token: 0x020018AA RID: 6314
	public enum ReplaceType
	{
		// Token: 0x040071E2 RID: 29154
		None,
		// Token: 0x040071E3 RID: 29155
		Replace,
		// Token: 0x040071E4 RID: 29156
		ReplaceAndDisplace
	}

	// Token: 0x020018AB RID: 6315
	private enum AddSolidMassSubType
	{
		// Token: 0x040071E6 RID: 29158
		DoVerticalDisplacement,
		// Token: 0x040071E7 RID: 29159
		OnlyIfSameElement
	}

	// Token: 0x020018AC RID: 6316
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct CellDiseaseModification
	{
		// Token: 0x040071E8 RID: 29160
		public int cellIdx;

		// Token: 0x040071E9 RID: 29161
		public byte diseaseIdx;

		// Token: 0x040071EA RID: 29162
		public byte pad0;

		// Token: 0x040071EB RID: 29163
		public byte pad1;

		// Token: 0x040071EC RID: 29164
		public byte pad2;

		// Token: 0x040071ED RID: 29165
		public int diseaseCount;
	}

	// Token: 0x020018AD RID: 6317
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct RadiationParamsModification
	{
		// Token: 0x040071EE RID: 29166
		public int RadiationParamsType;

		// Token: 0x040071EF RID: 29167
		public float value;
	}

	// Token: 0x020018AE RID: 6318
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct CellRadiationModification
	{
		// Token: 0x040071F0 RID: 29168
		public int cellIdx;

		// Token: 0x040071F1 RID: 29169
		public float radiationDelta;

		// Token: 0x040071F2 RID: 29170
		public int callbackIdx;
	}

	// Token: 0x020018AF RID: 6319
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct MassConsumptionMessage
	{
		// Token: 0x040071F3 RID: 29171
		public int cellIdx;

		// Token: 0x040071F4 RID: 29172
		public int callbackIdx;

		// Token: 0x040071F5 RID: 29173
		public float mass;

		// Token: 0x040071F6 RID: 29174
		public ushort elementIdx;

		// Token: 0x040071F7 RID: 29175
		public byte radius;
	}

	// Token: 0x020018B0 RID: 6320
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct MassEmissionMessage
	{
		// Token: 0x040071F8 RID: 29176
		public int cellIdx;

		// Token: 0x040071F9 RID: 29177
		public int callbackIdx;

		// Token: 0x040071FA RID: 29178
		public float mass;

		// Token: 0x040071FB RID: 29179
		public float temperature;

		// Token: 0x040071FC RID: 29180
		public int diseaseCount;

		// Token: 0x040071FD RID: 29181
		public ushort elementIdx;

		// Token: 0x040071FE RID: 29182
		public byte diseaseIdx;
	}

	// Token: 0x020018B1 RID: 6321
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct ConsumeDiseaseMessage
	{
		// Token: 0x040071FF RID: 29183
		public int gameCell;

		// Token: 0x04007200 RID: 29184
		public int callbackIdx;

		// Token: 0x04007201 RID: 29185
		public float percentToConsume;

		// Token: 0x04007202 RID: 29186
		public int maxToConsume;
	}

	// Token: 0x020018B2 RID: 6322
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct ModifyCellEnergyMessage
	{
		// Token: 0x04007203 RID: 29187
		public int cellIdx;

		// Token: 0x04007204 RID: 29188
		public float kilojoules;

		// Token: 0x04007205 RID: 29189
		public float maxTemperature;

		// Token: 0x04007206 RID: 29190
		public int id;
	}

	// Token: 0x020018B3 RID: 6323
	public enum EnergySourceID
	{
		// Token: 0x04007208 RID: 29192
		DebugHeat = 1000,
		// Token: 0x04007209 RID: 29193
		DebugCool,
		// Token: 0x0400720A RID: 29194
		FierySkin,
		// Token: 0x0400720B RID: 29195
		Overheatable,
		// Token: 0x0400720C RID: 29196
		LiquidCooledFan,
		// Token: 0x0400720D RID: 29197
		ConduitTemperatureManager,
		// Token: 0x0400720E RID: 29198
		Excavator,
		// Token: 0x0400720F RID: 29199
		HeatBulb,
		// Token: 0x04007210 RID: 29200
		WarmBlooded,
		// Token: 0x04007211 RID: 29201
		StructureTemperature,
		// Token: 0x04007212 RID: 29202
		Burner,
		// Token: 0x04007213 RID: 29203
		VacuumRadiator
	}

	// Token: 0x020018B4 RID: 6324
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct VisibleCells
	{
		// Token: 0x04007214 RID: 29204
		public Vector2I min;

		// Token: 0x04007215 RID: 29205
		public Vector2I max;
	}

	// Token: 0x020018B5 RID: 6325
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct WakeCellMessage
	{
		// Token: 0x04007216 RID: 29206
		public int gameCell;
	}

	// Token: 0x020018B6 RID: 6326
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ElementInteraction
	{
		// Token: 0x04007217 RID: 29207
		public uint interactionType;

		// Token: 0x04007218 RID: 29208
		public ushort elemIdx1;

		// Token: 0x04007219 RID: 29209
		public ushort elemIdx2;

		// Token: 0x0400721A RID: 29210
		public ushort elemResultIdx;

		// Token: 0x0400721B RID: 29211
		public byte pad0;

		// Token: 0x0400721C RID: 29212
		public byte pad1;

		// Token: 0x0400721D RID: 29213
		public float minMass;

		// Token: 0x0400721E RID: 29214
		public float interactionProbability;

		// Token: 0x0400721F RID: 29215
		public float elem1MassDestructionPercent;

		// Token: 0x04007220 RID: 29216
		public float elem2MassRequiredMultiplier;

		// Token: 0x04007221 RID: 29217
		public float elemResultMassCreationMultiplier;
	}

	// Token: 0x020018B7 RID: 6327
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct CreateElementInteractionsMsg
	{
		// Token: 0x04007222 RID: 29218
		public int numInteractions;

		// Token: 0x04007223 RID: 29219
		public unsafe SimMessages.ElementInteraction* interactions;
	}

	// Token: 0x020018B8 RID: 6328
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PipeChange
	{
		// Token: 0x04007224 RID: 29220
		public int cell;

		// Token: 0x04007225 RID: 29221
		public byte layer;

		// Token: 0x04007226 RID: 29222
		public byte pad0;

		// Token: 0x04007227 RID: 29223
		public byte pad1;

		// Token: 0x04007228 RID: 29224
		public byte pad2;

		// Token: 0x04007229 RID: 29225
		public float mass;

		// Token: 0x0400722A RID: 29226
		public float temperature;

		// Token: 0x0400722B RID: 29227
		public int elementHash;
	}

	// Token: 0x020018B9 RID: 6329
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct CellWorldZoneModification
	{
		// Token: 0x0400722C RID: 29228
		public int cell;

		// Token: 0x0400722D RID: 29229
		public byte zoneID;
	}
}
