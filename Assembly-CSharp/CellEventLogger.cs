using System;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x02000761 RID: 1889
public class CellEventLogger : EventLogger<CellEventInstance, CellEvent>
{
	// Token: 0x060033F1 RID: 13297 RVA: 0x0011749D File Offset: 0x0011569D
	public static void DestroyInstance()
	{
		CellEventLogger.Instance = null;
	}

	// Token: 0x060033F2 RID: 13298 RVA: 0x001174A5 File Offset: 0x001156A5
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void LogCallbackSend(int cell, int callback_id)
	{
		if (callback_id != -1)
		{
			this.CallbackToCellMap[callback_id] = cell;
		}
	}

	// Token: 0x060033F3 RID: 13299 RVA: 0x001174B8 File Offset: 0x001156B8
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void LogCallbackReceive(int callback_id)
	{
		int invalidCell = Grid.InvalidCell;
		this.CallbackToCellMap.TryGetValue(callback_id, out invalidCell);
	}

	// Token: 0x060033F4 RID: 13300 RVA: 0x001174DC File Offset: 0x001156DC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		CellEventLogger.Instance = this;
		this.SimMessagesSolid = base.AddEvent(new CellSolidEvent("SimMessageSolid", "Sim Message", false, true)) as CellSolidEvent;
		this.SimCellOccupierDestroy = base.AddEvent(new CellSolidEvent("SimCellOccupierClearSolid", "Sim Cell Occupier Destroy", false, true)) as CellSolidEvent;
		this.SimCellOccupierForceSolid = base.AddEvent(new CellSolidEvent("SimCellOccupierForceSolid", "Sim Cell Occupier Force Solid", false, true)) as CellSolidEvent;
		this.SimCellOccupierSolidChanged = base.AddEvent(new CellSolidEvent("SimCellOccupierSolidChanged", "Sim Cell Occupier Solid Changed", false, true)) as CellSolidEvent;
		this.DoorOpen = base.AddEvent(new CellElementEvent("DoorOpen", "Door Open", true, true)) as CellElementEvent;
		this.DoorClose = base.AddEvent(new CellElementEvent("DoorClose", "Door Close", true, true)) as CellElementEvent;
		this.Excavator = base.AddEvent(new CellElementEvent("Excavator", "Excavator", true, true)) as CellElementEvent;
		this.DebugTool = base.AddEvent(new CellElementEvent("DebugTool", "Debug Tool", true, true)) as CellElementEvent;
		this.SandBoxTool = base.AddEvent(new CellElementEvent("SandBoxTool", "Sandbox Tool", true, true)) as CellElementEvent;
		this.TemplateLoader = base.AddEvent(new CellElementEvent("TemplateLoader", "Template Loader", true, true)) as CellElementEvent;
		this.Scenario = base.AddEvent(new CellElementEvent("Scenario", "Scenario", true, true)) as CellElementEvent;
		this.SimCellOccupierOnSpawn = base.AddEvent(new CellElementEvent("SimCellOccupierOnSpawn", "Sim Cell Occupier OnSpawn", true, true)) as CellElementEvent;
		this.SimCellOccupierDestroySelf = base.AddEvent(new CellElementEvent("SimCellOccupierDestroySelf", "Sim Cell Occupier Destroy Self", true, true)) as CellElementEvent;
		this.WorldGapManager = base.AddEvent(new CellElementEvent("WorldGapManager", "World Gap Manager", true, true)) as CellElementEvent;
		this.ReceiveElementChanged = base.AddEvent(new CellElementEvent("ReceiveElementChanged", "Sim Message", false, false)) as CellElementEvent;
		this.ObjectSetSimOnSpawn = base.AddEvent(new CellElementEvent("ObjectSetSimOnSpawn", "Object set sim on spawn", true, true)) as CellElementEvent;
		this.DecompositionDirtyWater = base.AddEvent(new CellElementEvent("DecompositionDirtyWater", "Decomposition dirty water", true, true)) as CellElementEvent;
		this.SendCallback = base.AddEvent(new CellCallbackEvent("SendCallback", true, true)) as CellCallbackEvent;
		this.ReceiveCallback = base.AddEvent(new CellCallbackEvent("ReceiveCallback", false, true)) as CellCallbackEvent;
		this.Dig = base.AddEvent(new CellDigEvent(true)) as CellDigEvent;
		this.WorldDamageDelayedSpawnFX = base.AddEvent(new CellAddRemoveSubstanceEvent("WorldDamageDelayedSpawnFX", "World Damage Delayed Spawn FX", false)) as CellAddRemoveSubstanceEvent;
		this.OxygenModifierSimUpdate = base.AddEvent(new CellAddRemoveSubstanceEvent("OxygenModifierSimUpdate", "Oxygen Modifier SimUpdate", false)) as CellAddRemoveSubstanceEvent;
		this.LiquidChunkOnStore = base.AddEvent(new CellAddRemoveSubstanceEvent("LiquidChunkOnStore", "Liquid Chunk On Store", false)) as CellAddRemoveSubstanceEvent;
		this.FallingWaterAddToSim = base.AddEvent(new CellAddRemoveSubstanceEvent("FallingWaterAddToSim", "Falling Water Add To Sim", false)) as CellAddRemoveSubstanceEvent;
		this.ExploderOnSpawn = base.AddEvent(new CellAddRemoveSubstanceEvent("ExploderOnSpawn", "Exploder OnSpawn", false)) as CellAddRemoveSubstanceEvent;
		this.ExhaustSimUpdate = base.AddEvent(new CellAddRemoveSubstanceEvent("ExhaustSimUpdate", "Exhaust SimUpdate", false)) as CellAddRemoveSubstanceEvent;
		this.ElementConsumerSimUpdate = base.AddEvent(new CellAddRemoveSubstanceEvent("ElementConsumerSimUpdate", "Element Consumer SimUpdate", false)) as CellAddRemoveSubstanceEvent;
		this.SublimatesEmit = base.AddEvent(new CellAddRemoveSubstanceEvent("SublimatesEmit", "Sublimates Emit", false)) as CellAddRemoveSubstanceEvent;
		this.Mop = base.AddEvent(new CellAddRemoveSubstanceEvent("Mop", "Mop", false)) as CellAddRemoveSubstanceEvent;
		this.OreMelted = base.AddEvent(new CellAddRemoveSubstanceEvent("OreMelted", "Ore Melted", false)) as CellAddRemoveSubstanceEvent;
		this.ConstructTile = base.AddEvent(new CellAddRemoveSubstanceEvent("ConstructTile", "ConstructTile", false)) as CellAddRemoveSubstanceEvent;
		this.Dumpable = base.AddEvent(new CellAddRemoveSubstanceEvent("Dympable", "Dumpable", false)) as CellAddRemoveSubstanceEvent;
		this.Cough = base.AddEvent(new CellAddRemoveSubstanceEvent("Cough", "Cough", false)) as CellAddRemoveSubstanceEvent;
		this.Meteor = base.AddEvent(new CellAddRemoveSubstanceEvent("Meteor", "Meteor", false)) as CellAddRemoveSubstanceEvent;
		this.ElementChunkTransition = base.AddEvent(new CellAddRemoveSubstanceEvent("ElementChunkTransition", "Element Chunk Transition", false)) as CellAddRemoveSubstanceEvent;
		this.OxyrockEmit = base.AddEvent(new CellAddRemoveSubstanceEvent("OxyrockEmit", "Oxyrock Emit", false)) as CellAddRemoveSubstanceEvent;
		this.BleachstoneEmit = base.AddEvent(new CellAddRemoveSubstanceEvent("BleachstoneEmit", "Bleachstone Emit", false)) as CellAddRemoveSubstanceEvent;
		this.UnstableGround = base.AddEvent(new CellAddRemoveSubstanceEvent("UnstableGround", "Unstable Ground", false)) as CellAddRemoveSubstanceEvent;
		this.ConduitFlowEmptyConduit = base.AddEvent(new CellAddRemoveSubstanceEvent("ConduitFlowEmptyConduit", "Conduit Flow Empty Conduit", false)) as CellAddRemoveSubstanceEvent;
		this.ConduitConsumerWrongElement = base.AddEvent(new CellAddRemoveSubstanceEvent("ConduitConsumerWrongElement", "Conduit Consumer Wrong Element", false)) as CellAddRemoveSubstanceEvent;
		this.OverheatableMeltingDown = base.AddEvent(new CellAddRemoveSubstanceEvent("OverheatableMeltingDown", "Overheatable MeltingDown", false)) as CellAddRemoveSubstanceEvent;
		this.FabricatorProduceMelted = base.AddEvent(new CellAddRemoveSubstanceEvent("FabricatorProduceMelted", "Fabricator Produce Melted", false)) as CellAddRemoveSubstanceEvent;
		this.PumpSimUpdate = base.AddEvent(new CellAddRemoveSubstanceEvent("PumpSimUpdate", "Pump SimUpdate", false)) as CellAddRemoveSubstanceEvent;
		this.WallPumpSimUpdate = base.AddEvent(new CellAddRemoveSubstanceEvent("WallPumpSimUpdate", "Wall Pump SimUpdate", false)) as CellAddRemoveSubstanceEvent;
		this.Vomit = base.AddEvent(new CellAddRemoveSubstanceEvent("Vomit", "Vomit", false)) as CellAddRemoveSubstanceEvent;
		this.Tears = base.AddEvent(new CellAddRemoveSubstanceEvent("Tears", "Tears", false)) as CellAddRemoveSubstanceEvent;
		this.Pee = base.AddEvent(new CellAddRemoveSubstanceEvent("Pee", "Pee", false)) as CellAddRemoveSubstanceEvent;
		this.AlgaeHabitat = base.AddEvent(new CellAddRemoveSubstanceEvent("AlgaeHabitat", "AlgaeHabitat", false)) as CellAddRemoveSubstanceEvent;
		this.CO2FilterOxygen = base.AddEvent(new CellAddRemoveSubstanceEvent("CO2FilterOxygen", "CO2FilterOxygen", false)) as CellAddRemoveSubstanceEvent;
		this.ToiletEmit = base.AddEvent(new CellAddRemoveSubstanceEvent("ToiletEmit", "ToiletEmit", false)) as CellAddRemoveSubstanceEvent;
		this.ElementEmitted = base.AddEvent(new CellAddRemoveSubstanceEvent("ElementEmitted", "Element Emitted", false)) as CellAddRemoveSubstanceEvent;
		this.CO2ManagerFixedUpdate = base.AddEvent(new CellModifyMassEvent("CO2ManagerFixedUpdate", "CO2Manager FixedUpdate", false)) as CellModifyMassEvent;
		this.EnvironmentConsumerFixedUpdate = base.AddEvent(new CellModifyMassEvent("EnvironmentConsumerFixedUpdate", "EnvironmentConsumer FixedUpdate", false)) as CellModifyMassEvent;
		this.ExcavatorShockwave = base.AddEvent(new CellModifyMassEvent("ExcavatorShockwave", "Excavator Shockwave", false)) as CellModifyMassEvent;
		this.OxygenBreatherSimUpdate = base.AddEvent(new CellModifyMassEvent("OxygenBreatherSimUpdate", "Oxygen Breather SimUpdate", false)) as CellModifyMassEvent;
		this.CO2ScrubberSimUpdate = base.AddEvent(new CellModifyMassEvent("CO2ScrubberSimUpdate", "CO2Scrubber SimUpdate", false)) as CellModifyMassEvent;
		this.RiverSourceSimUpdate = base.AddEvent(new CellModifyMassEvent("RiverSourceSimUpdate", "RiverSource SimUpdate", false)) as CellModifyMassEvent;
		this.RiverTerminusSimUpdate = base.AddEvent(new CellModifyMassEvent("RiverTerminusSimUpdate", "RiverTerminus SimUpdate", false)) as CellModifyMassEvent;
		this.DebugToolModifyMass = base.AddEvent(new CellModifyMassEvent("DebugToolModifyMass", "DebugTool ModifyMass", false)) as CellModifyMassEvent;
		this.EnergyGeneratorModifyMass = base.AddEvent(new CellModifyMassEvent("EnergyGeneratorModifyMass", "EnergyGenerator ModifyMass", false)) as CellModifyMassEvent;
		this.SolidFilterEvent = base.AddEvent(new CellSolidFilterEvent("SolidFilterEvent", true)) as CellSolidFilterEvent;
	}

	// Token: 0x04001FDD RID: 8157
	public static CellEventLogger Instance;

	// Token: 0x04001FDE RID: 8158
	public CellSolidEvent SimMessagesSolid;

	// Token: 0x04001FDF RID: 8159
	public CellSolidEvent SimCellOccupierDestroy;

	// Token: 0x04001FE0 RID: 8160
	public CellSolidEvent SimCellOccupierForceSolid;

	// Token: 0x04001FE1 RID: 8161
	public CellSolidEvent SimCellOccupierSolidChanged;

	// Token: 0x04001FE2 RID: 8162
	public CellElementEvent DoorOpen;

	// Token: 0x04001FE3 RID: 8163
	public CellElementEvent DoorClose;

	// Token: 0x04001FE4 RID: 8164
	public CellElementEvent Excavator;

	// Token: 0x04001FE5 RID: 8165
	public CellElementEvent DebugTool;

	// Token: 0x04001FE6 RID: 8166
	public CellElementEvent SandBoxTool;

	// Token: 0x04001FE7 RID: 8167
	public CellElementEvent TemplateLoader;

	// Token: 0x04001FE8 RID: 8168
	public CellElementEvent Scenario;

	// Token: 0x04001FE9 RID: 8169
	public CellElementEvent SimCellOccupierOnSpawn;

	// Token: 0x04001FEA RID: 8170
	public CellElementEvent SimCellOccupierDestroySelf;

	// Token: 0x04001FEB RID: 8171
	public CellElementEvent WorldGapManager;

	// Token: 0x04001FEC RID: 8172
	public CellElementEvent ReceiveElementChanged;

	// Token: 0x04001FED RID: 8173
	public CellElementEvent ObjectSetSimOnSpawn;

	// Token: 0x04001FEE RID: 8174
	public CellElementEvent DecompositionDirtyWater;

	// Token: 0x04001FEF RID: 8175
	public CellElementEvent LaunchpadDesolidify;

	// Token: 0x04001FF0 RID: 8176
	public CellCallbackEvent SendCallback;

	// Token: 0x04001FF1 RID: 8177
	public CellCallbackEvent ReceiveCallback;

	// Token: 0x04001FF2 RID: 8178
	public CellDigEvent Dig;

	// Token: 0x04001FF3 RID: 8179
	public CellAddRemoveSubstanceEvent WorldDamageDelayedSpawnFX;

	// Token: 0x04001FF4 RID: 8180
	public CellAddRemoveSubstanceEvent SublimatesEmit;

	// Token: 0x04001FF5 RID: 8181
	public CellAddRemoveSubstanceEvent OxygenModifierSimUpdate;

	// Token: 0x04001FF6 RID: 8182
	public CellAddRemoveSubstanceEvent LiquidChunkOnStore;

	// Token: 0x04001FF7 RID: 8183
	public CellAddRemoveSubstanceEvent FallingWaterAddToSim;

	// Token: 0x04001FF8 RID: 8184
	public CellAddRemoveSubstanceEvent ExploderOnSpawn;

	// Token: 0x04001FF9 RID: 8185
	public CellAddRemoveSubstanceEvent ExhaustSimUpdate;

	// Token: 0x04001FFA RID: 8186
	public CellAddRemoveSubstanceEvent ElementConsumerSimUpdate;

	// Token: 0x04001FFB RID: 8187
	public CellAddRemoveSubstanceEvent ElementChunkTransition;

	// Token: 0x04001FFC RID: 8188
	public CellAddRemoveSubstanceEvent OxyrockEmit;

	// Token: 0x04001FFD RID: 8189
	public CellAddRemoveSubstanceEvent BleachstoneEmit;

	// Token: 0x04001FFE RID: 8190
	public CellAddRemoveSubstanceEvent UnstableGround;

	// Token: 0x04001FFF RID: 8191
	public CellAddRemoveSubstanceEvent ConduitFlowEmptyConduit;

	// Token: 0x04002000 RID: 8192
	public CellAddRemoveSubstanceEvent ConduitConsumerWrongElement;

	// Token: 0x04002001 RID: 8193
	public CellAddRemoveSubstanceEvent OverheatableMeltingDown;

	// Token: 0x04002002 RID: 8194
	public CellAddRemoveSubstanceEvent FabricatorProduceMelted;

	// Token: 0x04002003 RID: 8195
	public CellAddRemoveSubstanceEvent PumpSimUpdate;

	// Token: 0x04002004 RID: 8196
	public CellAddRemoveSubstanceEvent WallPumpSimUpdate;

	// Token: 0x04002005 RID: 8197
	public CellAddRemoveSubstanceEvent Vomit;

	// Token: 0x04002006 RID: 8198
	public CellAddRemoveSubstanceEvent Tears;

	// Token: 0x04002007 RID: 8199
	public CellAddRemoveSubstanceEvent Pee;

	// Token: 0x04002008 RID: 8200
	public CellAddRemoveSubstanceEvent AlgaeHabitat;

	// Token: 0x04002009 RID: 8201
	public CellAddRemoveSubstanceEvent CO2FilterOxygen;

	// Token: 0x0400200A RID: 8202
	public CellAddRemoveSubstanceEvent ToiletEmit;

	// Token: 0x0400200B RID: 8203
	public CellAddRemoveSubstanceEvent ElementEmitted;

	// Token: 0x0400200C RID: 8204
	public CellAddRemoveSubstanceEvent Mop;

	// Token: 0x0400200D RID: 8205
	public CellAddRemoveSubstanceEvent OreMelted;

	// Token: 0x0400200E RID: 8206
	public CellAddRemoveSubstanceEvent ConstructTile;

	// Token: 0x0400200F RID: 8207
	public CellAddRemoveSubstanceEvent Dumpable;

	// Token: 0x04002010 RID: 8208
	public CellAddRemoveSubstanceEvent Cough;

	// Token: 0x04002011 RID: 8209
	public CellAddRemoveSubstanceEvent Meteor;

	// Token: 0x04002012 RID: 8210
	public CellModifyMassEvent CO2ManagerFixedUpdate;

	// Token: 0x04002013 RID: 8211
	public CellModifyMassEvent EnvironmentConsumerFixedUpdate;

	// Token: 0x04002014 RID: 8212
	public CellModifyMassEvent ExcavatorShockwave;

	// Token: 0x04002015 RID: 8213
	public CellModifyMassEvent OxygenBreatherSimUpdate;

	// Token: 0x04002016 RID: 8214
	public CellModifyMassEvent CO2ScrubberSimUpdate;

	// Token: 0x04002017 RID: 8215
	public CellModifyMassEvent RiverSourceSimUpdate;

	// Token: 0x04002018 RID: 8216
	public CellModifyMassEvent RiverTerminusSimUpdate;

	// Token: 0x04002019 RID: 8217
	public CellModifyMassEvent DebugToolModifyMass;

	// Token: 0x0400201A RID: 8218
	public CellModifyMassEvent EnergyGeneratorModifyMass;

	// Token: 0x0400201B RID: 8219
	public CellSolidFilterEvent SolidFilterEvent;

	// Token: 0x0400201C RID: 8220
	public Dictionary<int, int> CallbackToCellMap = new Dictionary<int, int>();
}
