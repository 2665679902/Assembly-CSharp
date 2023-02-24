using System;

// Token: 0x02000601 RID: 1537
public class MakeBaseSolid : GameStateMachine<MakeBaseSolid, MakeBaseSolid.Instance, IStateMachineTarget, MakeBaseSolid.Def>
{
	// Token: 0x060027F2 RID: 10226 RVA: 0x000D4B1A File Offset: 0x000D2D1A
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.Enter(new StateMachine<MakeBaseSolid, MakeBaseSolid.Instance, IStateMachineTarget, MakeBaseSolid.Def>.State.Callback(MakeBaseSolid.ConvertToSolid)).Exit(new StateMachine<MakeBaseSolid, MakeBaseSolid.Instance, IStateMachineTarget, MakeBaseSolid.Def>.State.Callback(MakeBaseSolid.ConvertToVacuum));
	}

	// Token: 0x060027F3 RID: 10227 RVA: 0x000D4B50 File Offset: 0x000D2D50
	private static void ConvertToSolid(MakeBaseSolid.Instance smi)
	{
		if (smi.buildingComplete == null)
		{
			return;
		}
		int num = Grid.PosToCell(smi.gameObject);
		PrimaryElement component = smi.GetComponent<PrimaryElement>();
		Building component2 = smi.GetComponent<Building>();
		foreach (CellOffset cellOffset in smi.def.solidOffsets)
		{
			CellOffset rotatedOffset = component2.GetRotatedOffset(cellOffset);
			int num2 = Grid.OffsetCell(num, rotatedOffset);
			if (smi.def.occupyFoundationLayer)
			{
				SimMessages.ReplaceAndDisplaceElement(num2, component.ElementID, CellEventLogger.Instance.SimCellOccupierOnSpawn, component.Mass, component.Temperature, byte.MaxValue, 0, -1);
				Grid.Objects[num2, 9] = smi.gameObject;
			}
			else
			{
				SimMessages.ReplaceAndDisplaceElement(num2, SimHashes.Vacuum, CellEventLogger.Instance.SimCellOccupierOnSpawn, 0f, 0f, byte.MaxValue, 0, -1);
			}
			Grid.Foundation[num2] = true;
			Grid.SetSolid(num2, true, CellEventLogger.Instance.SimCellOccupierForceSolid);
			SimMessages.SetCellProperties(num2, 103);
			Grid.RenderedByWorld[num2] = false;
			World.Instance.OnSolidChanged(num2);
			GameScenePartitioner.Instance.TriggerEvent(num2, GameScenePartitioner.Instance.solidChangedLayer, null);
		}
	}

	// Token: 0x060027F4 RID: 10228 RVA: 0x000D4C9C File Offset: 0x000D2E9C
	private static void ConvertToVacuum(MakeBaseSolid.Instance smi)
	{
		if (smi.buildingComplete == null)
		{
			return;
		}
		int num = Grid.PosToCell(smi.gameObject);
		Building component = smi.GetComponent<Building>();
		foreach (CellOffset cellOffset in smi.def.solidOffsets)
		{
			CellOffset rotatedOffset = component.GetRotatedOffset(cellOffset);
			int num2 = Grid.OffsetCell(num, rotatedOffset);
			SimMessages.ReplaceAndDisplaceElement(num2, SimHashes.Vacuum, CellEventLogger.Instance.SimCellOccupierOnSpawn, 0f, -1f, byte.MaxValue, 0, -1);
			Grid.Objects[num2, 9] = null;
			Grid.Foundation[num2] = false;
			Grid.SetSolid(num2, false, CellEventLogger.Instance.SimCellOccupierDestroy);
			SimMessages.ClearCellProperties(num2, 103);
			Grid.RenderedByWorld[num2] = true;
			World.Instance.OnSolidChanged(num2);
			GameScenePartitioner.Instance.TriggerEvent(num2, GameScenePartitioner.Instance.solidChangedLayer, null);
		}
	}

	// Token: 0x0400177D RID: 6013
	private const Sim.Cell.Properties floorCellProperties = (Sim.Cell.Properties)103;

	// Token: 0x02001266 RID: 4710
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005DBA RID: 23994
		public CellOffset[] solidOffsets;

		// Token: 0x04005DBB RID: 23995
		public bool occupyFoundationLayer = true;
	}

	// Token: 0x02001267 RID: 4711
	public new class Instance : GameStateMachine<MakeBaseSolid, MakeBaseSolid.Instance, IStateMachineTarget, MakeBaseSolid.Def>.GameInstance
	{
		// Token: 0x06007A19 RID: 31257 RVA: 0x002C608D File Offset: 0x002C428D
		public Instance(IStateMachineTarget master, MakeBaseSolid.Def def)
			: base(master, def)
		{
		}

		// Token: 0x04005DBC RID: 23996
		[MyCmpGet]
		public BuildingComplete buildingComplete;
	}
}
