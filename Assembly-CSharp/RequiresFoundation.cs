using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020008D7 RID: 2263
public class RequiresFoundation : KGameObjectComponentManager<RequiresFoundation.Data>, IKComponentManager
{
	// Token: 0x0600411E RID: 16670 RVA: 0x0016CBE4 File Offset: 0x0016ADE4
	public HandleVector<int>.Handle Add(GameObject go)
	{
		BuildingDef def = go.GetComponent<Building>().Def;
		int num = Grid.PosToCell(go.transform.GetPosition());
		RequiresFoundation.Data data = new RequiresFoundation.Data
		{
			cell = num,
			width = def.WidthInCells,
			height = def.HeightInCells,
			buildRule = def.BuildLocationRule,
			solid = true,
			go = go
		};
		HandleVector<int>.Handle h = base.Add(go, data);
		if (def.ContinuouslyCheckFoundation)
		{
			data.changeCallback = delegate(object d)
			{
				this.OnSolidChanged(h);
			};
			Rotatable component = data.go.GetComponent<Rotatable>();
			Orientation orientation = ((component != null) ? component.GetOrientation() : Orientation.Neutral);
			int num2 = -(def.WidthInCells - 1) / 2;
			int num3 = def.WidthInCells / 2;
			CellOffset cellOffset = new CellOffset(num2, -1);
			CellOffset cellOffset2 = new CellOffset(num3, -1);
			if (def.BuildLocationRule == BuildLocationRule.OnCeiling || def.BuildLocationRule == BuildLocationRule.InCorner)
			{
				cellOffset.y = def.HeightInCells;
				cellOffset2.y = def.HeightInCells;
			}
			else if (def.BuildLocationRule == BuildLocationRule.OnWall)
			{
				cellOffset = new CellOffset(num2 - 1, 0);
				cellOffset2 = new CellOffset(num2 - 1, def.HeightInCells);
			}
			else if (def.BuildLocationRule == BuildLocationRule.WallFloor)
			{
				cellOffset = new CellOffset(num2 - 1, -1);
				cellOffset2 = new CellOffset(num3, def.HeightInCells - 1);
			}
			CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(cellOffset, orientation);
			CellOffset rotatedCellOffset2 = Rotatable.GetRotatedCellOffset(cellOffset2, orientation);
			int num4 = Grid.OffsetCell(num, rotatedCellOffset);
			int num5 = Grid.OffsetCell(num, rotatedCellOffset2);
			Vector2I vector2I = Grid.CellToXY(num4);
			Vector2I vector2I2 = Grid.CellToXY(num5);
			float num6 = (float)Mathf.Min(vector2I.x, vector2I2.x);
			float num7 = (float)Mathf.Max(vector2I.x, vector2I2.x);
			float num8 = (float)Mathf.Min(vector2I.y, vector2I2.y);
			float num9 = (float)Mathf.Max(vector2I.y, vector2I2.y);
			Rect rect = Rect.MinMaxRect(num6, num8, num7, num9);
			data.solidPartitionerEntry = GameScenePartitioner.Instance.Add("RequiresFoundation.Add", go, (int)rect.x, (int)rect.y, (int)rect.width + 1, (int)rect.height + 1, GameScenePartitioner.Instance.solidChangedLayer, data.changeCallback);
			data.buildingPartitionerEntry = GameScenePartitioner.Instance.Add("RequiresFoundation.Add", go, (int)rect.x, (int)rect.y, (int)rect.width + 1, (int)rect.height + 1, GameScenePartitioner.Instance.objectLayers[1], data.changeCallback);
			if (def.BuildLocationRule == BuildLocationRule.BuildingAttachPoint || def.BuildLocationRule == BuildLocationRule.OnFloorOrBuildingAttachPoint)
			{
				AttachableBuilding component2 = data.go.GetComponent<AttachableBuilding>();
				component2.onAttachmentNetworkChanged = (Action<object>)Delegate.Combine(component2.onAttachmentNetworkChanged, data.changeCallback);
			}
			base.SetData(h, data);
			this.OnSolidChanged(h);
			data = base.GetData(h);
			this.UpdateSolidState(data.solid, ref data, true);
		}
		return h;
	}

	// Token: 0x0600411F RID: 16671 RVA: 0x0016CF08 File Offset: 0x0016B108
	protected override void OnCleanUp(HandleVector<int>.Handle h)
	{
		RequiresFoundation.Data data = base.GetData(h);
		GameScenePartitioner.Instance.Free(ref data.solidPartitionerEntry);
		GameScenePartitioner.Instance.Free(ref data.buildingPartitionerEntry);
		AttachableBuilding component = data.go.GetComponent<AttachableBuilding>();
		if (!component.IsNullOrDestroyed())
		{
			AttachableBuilding attachableBuilding = component;
			attachableBuilding.onAttachmentNetworkChanged = (Action<object>)Delegate.Remove(attachableBuilding.onAttachmentNetworkChanged, data.changeCallback);
		}
		base.SetData(h, data);
	}

	// Token: 0x06004120 RID: 16672 RVA: 0x0016CF78 File Offset: 0x0016B178
	private void OnSolidChanged(HandleVector<int>.Handle h)
	{
		RequiresFoundation.Data data = base.GetData(h);
		SimCellOccupier component = data.go.GetComponent<SimCellOccupier>();
		if (component == null || component.IsReady())
		{
			Rotatable component2 = data.go.GetComponent<Rotatable>();
			Orientation orientation = ((component2 != null) ? component2.GetOrientation() : Orientation.Neutral);
			bool flag = BuildingDef.CheckFoundation(data.cell, orientation, data.buildRule, data.width, data.height, default(Tag));
			if (!flag && (data.buildRule == BuildLocationRule.BuildingAttachPoint || data.buildRule == BuildLocationRule.OnFloorOrBuildingAttachPoint))
			{
				List<GameObject> list = new List<GameObject>();
				AttachableBuilding.GetAttachedBelow(data.go.GetComponent<AttachableBuilding>(), ref list);
				if (list.Count > 0)
				{
					Operational component3 = list.Last<GameObject>().GetComponent<Operational>();
					if (component3 != null && component3.GetFlag(RequiresFoundation.solidFoundation))
					{
						flag = true;
					}
				}
			}
			this.UpdateSolidState(flag, ref data, false);
			base.SetData(h, data);
		}
	}

	// Token: 0x06004121 RID: 16673 RVA: 0x0016D070 File Offset: 0x0016B270
	private void UpdateSolidState(bool is_solid, ref RequiresFoundation.Data data, bool forceUpdate = false)
	{
		if (data.solid != is_solid || forceUpdate)
		{
			data.solid = is_solid;
			Operational component = data.go.GetComponent<Operational>();
			if (component != null)
			{
				component.SetFlag(RequiresFoundation.solidFoundation, is_solid);
			}
			AttachableBuilding component2 = data.go.GetComponent<AttachableBuilding>();
			if (component2 != null)
			{
				List<GameObject> list = new List<GameObject>();
				AttachableBuilding.GetAttachedAbove(component2, ref list);
				AttachableBuilding.NotifyBuildingsNetworkChanged(list, null);
			}
			data.go.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.MissingFoundation, !is_solid, this);
		}
	}

	// Token: 0x04002B7F RID: 11135
	public static readonly Operational.Flag solidFoundation = new Operational.Flag("solid_foundation", Operational.Flag.Type.Functional);

	// Token: 0x02001698 RID: 5784
	public struct Data
	{
		// Token: 0x04006A4E RID: 27214
		public int cell;

		// Token: 0x04006A4F RID: 27215
		public int width;

		// Token: 0x04006A50 RID: 27216
		public int height;

		// Token: 0x04006A51 RID: 27217
		public BuildLocationRule buildRule;

		// Token: 0x04006A52 RID: 27218
		public HandleVector<int>.Handle solidPartitionerEntry;

		// Token: 0x04006A53 RID: 27219
		public HandleVector<int>.Handle buildingPartitionerEntry;

		// Token: 0x04006A54 RID: 27220
		public bool solid;

		// Token: 0x04006A55 RID: 27221
		public GameObject go;

		// Token: 0x04006A56 RID: 27222
		public Action<object> changeCallback;
	}
}
