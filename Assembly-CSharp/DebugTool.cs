using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007C5 RID: 1989
public class DebugTool : DragTool
{
	// Token: 0x060038C3 RID: 14531 RVA: 0x0013A8AE File Offset: 0x00138AAE
	public static void DestroyInstance()
	{
		DebugTool.Instance = null;
	}

	// Token: 0x060038C4 RID: 14532 RVA: 0x0013A8B6 File Offset: 0x00138AB6
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		DebugTool.Instance = this;
	}

	// Token: 0x060038C5 RID: 14533 RVA: 0x0013A8C4 File Offset: 0x00138AC4
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x060038C6 RID: 14534 RVA: 0x0013A8D1 File Offset: 0x00138AD1
	public void Activate(DebugTool.Type type)
	{
		this.type = type;
		this.Activate();
	}

	// Token: 0x060038C7 RID: 14535 RVA: 0x0013A8E0 File Offset: 0x00138AE0
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		PlayerController.Instance.ToolDeactivated(this);
	}

	// Token: 0x060038C8 RID: 14536 RVA: 0x0013A8F4 File Offset: 0x00138AF4
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		if (Grid.IsValidCell(cell))
		{
			switch (this.type)
			{
			case DebugTool.Type.ReplaceSubstance:
				this.DoReplaceSubstance(cell);
				return;
			case DebugTool.Type.FillReplaceSubstance:
			{
				GameUtil.FloodFillNext.Clear();
				GameUtil.FloodFillVisited.Clear();
				SimHashes elem_hash = Grid.Element[cell].id;
				GameUtil.FloodFillConditional(cell, delegate(int check_cell)
				{
					bool flag = false;
					if (Grid.Element[check_cell].id == elem_hash)
					{
						flag = true;
						this.DoReplaceSubstance(check_cell);
					}
					return flag;
				}, GameUtil.FloodFillVisited, null);
				return;
			}
			case DebugTool.Type.Clear:
				this.ClearCell(cell);
				return;
			case DebugTool.Type.AddSelection:
				DebugBaseTemplateButton.Instance.AddToSelection(cell);
				return;
			case DebugTool.Type.RemoveSelection:
				DebugBaseTemplateButton.Instance.RemoveFromSelection(cell);
				return;
			case DebugTool.Type.Deconstruct:
				this.DeconstructCell(cell);
				return;
			case DebugTool.Type.Destroy:
				this.DestroyCell(cell);
				return;
			case DebugTool.Type.Sample:
				DebugPaintElementScreen.Instance.SampleCell(cell);
				return;
			case DebugTool.Type.StoreSubstance:
				this.DoStoreSubstance(cell);
				return;
			case DebugTool.Type.Dig:
				SimMessages.Dig(cell, -1, false);
				return;
			case DebugTool.Type.Heat:
				SimMessages.ModifyEnergy(cell, 10000f, 10000f, SimMessages.EnergySourceID.DebugHeat);
				return;
			case DebugTool.Type.Cool:
				SimMessages.ModifyEnergy(cell, -10000f, 10000f, SimMessages.EnergySourceID.DebugCool);
				return;
			case DebugTool.Type.AddPressure:
				SimMessages.ModifyMass(cell, 10000f, byte.MaxValue, 0, CellEventLogger.Instance.DebugToolModifyMass, 293f, SimHashes.Oxygen);
				return;
			case DebugTool.Type.RemovePressure:
				SimMessages.ModifyMass(cell, -10000f, byte.MaxValue, 0, CellEventLogger.Instance.DebugToolModifyMass, 0f, SimHashes.Oxygen);
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x060038C9 RID: 14537 RVA: 0x0013AA6C File Offset: 0x00138C6C
	public void DoReplaceSubstance(int cell)
	{
		if (!Grid.IsValidBuildingCell(cell))
		{
			return;
		}
		Element element = (DebugPaintElementScreen.Instance.paintElement.isOn ? ElementLoader.FindElementByHash(DebugPaintElementScreen.Instance.element) : ElementLoader.elements[(int)Grid.ElementIdx[cell]]);
		if (element == null)
		{
			element = ElementLoader.FindElementByHash(SimHashes.Vacuum);
		}
		byte b = (DebugPaintElementScreen.Instance.paintDisease.isOn ? DebugPaintElementScreen.Instance.diseaseIdx : Grid.DiseaseIdx[cell]);
		float num = (DebugPaintElementScreen.Instance.paintTemperature.isOn ? DebugPaintElementScreen.Instance.temperature : Grid.Temperature[cell]);
		float num2 = (DebugPaintElementScreen.Instance.paintMass.isOn ? DebugPaintElementScreen.Instance.mass : Grid.Mass[cell]);
		int num3 = (DebugPaintElementScreen.Instance.paintDiseaseCount.isOn ? DebugPaintElementScreen.Instance.diseaseCount : Grid.DiseaseCount[cell]);
		if (num == -1f)
		{
			num = element.defaultValues.temperature;
		}
		if (num2 == -1f)
		{
			num2 = element.defaultValues.mass;
		}
		if (DebugPaintElementScreen.Instance.affectCells.isOn)
		{
			SimMessages.ReplaceElement(cell, element.id, CellEventLogger.Instance.DebugTool, num2, num, b, num3, -1);
			if (DebugPaintElementScreen.Instance.set_prevent_fow_reveal)
			{
				Grid.Visible[cell] = 0;
				Grid.PreventFogOfWarReveal[cell] = true;
			}
			else if (DebugPaintElementScreen.Instance.set_allow_fow_reveal && Grid.PreventFogOfWarReveal[cell])
			{
				Grid.PreventFogOfWarReveal[cell] = false;
			}
		}
		if (DebugPaintElementScreen.Instance.affectBuildings.isOn)
		{
			foreach (GameObject gameObject in new List<GameObject>
			{
				Grid.Objects[cell, 1],
				Grid.Objects[cell, 2],
				Grid.Objects[cell, 9],
				Grid.Objects[cell, 16],
				Grid.Objects[cell, 12],
				Grid.Objects[cell, 16],
				Grid.Objects[cell, 26]
			})
			{
				if (gameObject != null)
				{
					PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
					if (num > 0f)
					{
						component.Temperature = num;
					}
					if (num3 > 0 && b != 255)
					{
						component.ModifyDiseaseCount(int.MinValue, "DebugTool.DoReplaceSubstance");
						component.AddDisease(b, num3, "DebugTool.DoReplaceSubstance");
					}
				}
			}
		}
	}

	// Token: 0x060038CA RID: 14538 RVA: 0x0013AD30 File Offset: 0x00138F30
	public void DeconstructCell(int cell)
	{
		bool instantBuildMode = DebugHandler.InstantBuildMode;
		DebugHandler.InstantBuildMode = true;
		DeconstructTool.Instance.DeconstructCell(cell);
		if (!instantBuildMode)
		{
			DebugHandler.InstantBuildMode = false;
		}
	}

	// Token: 0x060038CB RID: 14539 RVA: 0x0013AD50 File Offset: 0x00138F50
	public void DestroyCell(int cell)
	{
		foreach (GameObject gameObject in new List<GameObject>
		{
			Grid.Objects[cell, 2],
			Grid.Objects[cell, 1],
			Grid.Objects[cell, 12],
			Grid.Objects[cell, 16],
			Grid.Objects[cell, 0],
			Grid.Objects[cell, 26]
		})
		{
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		this.ClearCell(cell);
		if (ElementLoader.elements[(int)Grid.ElementIdx[cell]].id == SimHashes.Void)
		{
			SimMessages.ReplaceElement(cell, SimHashes.Void, CellEventLogger.Instance.DebugTool, 0f, 0f, byte.MaxValue, 0, -1);
			return;
		}
		SimMessages.ReplaceElement(cell, SimHashes.Vacuum, CellEventLogger.Instance.DebugTool, 0f, 0f, byte.MaxValue, 0, -1);
	}

	// Token: 0x060038CC RID: 14540 RVA: 0x0013AE90 File Offset: 0x00139090
	public void ClearCell(int cell)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		ListPool<ScenePartitionerEntry, DebugTool>.PooledList pooledList = ListPool<ScenePartitionerEntry, DebugTool>.Allocate();
		GameScenePartitioner.Instance.GatherEntries(vector2I.x, vector2I.y, 1, 1, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
		for (int i = 0; i < pooledList.Count; i++)
		{
			Pickupable pickupable = pooledList[i].obj as Pickupable;
			if (pickupable != null && pickupable.GetComponent<MinionBrain>() == null)
			{
				Util.KDestroyGameObject(pickupable.gameObject);
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x060038CD RID: 14541 RVA: 0x0013AF18 File Offset: 0x00139118
	public void DoStoreSubstance(int cell)
	{
		if (!Grid.IsValidBuildingCell(cell))
		{
			return;
		}
		GameObject gameObject = Grid.Objects[cell, 1];
		if (gameObject == null)
		{
			return;
		}
		Storage component = gameObject.GetComponent<Storage>();
		if (component == null)
		{
			return;
		}
		Element element = (DebugPaintElementScreen.Instance.paintElement.isOn ? ElementLoader.FindElementByHash(DebugPaintElementScreen.Instance.element) : ElementLoader.elements[(int)Grid.ElementIdx[cell]]);
		if (element == null)
		{
			element = ElementLoader.FindElementByHash(SimHashes.Vacuum);
		}
		byte b = (DebugPaintElementScreen.Instance.paintDisease.isOn ? DebugPaintElementScreen.Instance.diseaseIdx : Grid.DiseaseIdx[cell]);
		float num = (DebugPaintElementScreen.Instance.paintTemperature.isOn ? DebugPaintElementScreen.Instance.temperature : element.defaultValues.temperature);
		float num2 = (DebugPaintElementScreen.Instance.paintMass.isOn ? DebugPaintElementScreen.Instance.mass : element.defaultValues.mass);
		if (num == -1f)
		{
			num = element.defaultValues.temperature;
		}
		if (num2 == -1f)
		{
			num2 = element.defaultValues.mass;
		}
		int num3 = (DebugPaintElementScreen.Instance.paintDiseaseCount.isOn ? DebugPaintElementScreen.Instance.diseaseCount : 0);
		if (element.IsGas)
		{
			component.AddGasChunk(element.id, num2, num, b, num3, false, true);
			return;
		}
		if (element.IsLiquid)
		{
			component.AddLiquid(element.id, num2, num, b, num3, false, true);
			return;
		}
		if (element.IsSolid)
		{
			component.AddOre(element.id, num2, num, b, num3, false, true);
		}
	}

	// Token: 0x040025AF RID: 9647
	public static DebugTool Instance;

	// Token: 0x040025B0 RID: 9648
	public DebugTool.Type type;

	// Token: 0x02001521 RID: 5409
	public enum Type
	{
		// Token: 0x040065B0 RID: 26032
		ReplaceSubstance,
		// Token: 0x040065B1 RID: 26033
		FillReplaceSubstance,
		// Token: 0x040065B2 RID: 26034
		Clear,
		// Token: 0x040065B3 RID: 26035
		AddSelection,
		// Token: 0x040065B4 RID: 26036
		RemoveSelection,
		// Token: 0x040065B5 RID: 26037
		Deconstruct,
		// Token: 0x040065B6 RID: 26038
		Destroy,
		// Token: 0x040065B7 RID: 26039
		Sample,
		// Token: 0x040065B8 RID: 26040
		StoreSubstance,
		// Token: 0x040065B9 RID: 26041
		Dig,
		// Token: 0x040065BA RID: 26042
		Heat,
		// Token: 0x040065BB RID: 26043
		Cool,
		// Token: 0x040065BC RID: 26044
		AddPressure,
		// Token: 0x040065BD RID: 26045
		RemovePressure,
		// Token: 0x040065BE RID: 26046
		PaintPlant
	}
}
