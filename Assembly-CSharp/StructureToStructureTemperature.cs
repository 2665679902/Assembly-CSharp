using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200099A RID: 2458
public class StructureToStructureTemperature : KMonoBehaviour
{
	// Token: 0x060048E1 RID: 18657 RVA: 0x00198650 File Offset: 0x00196850
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<StructureToStructureTemperature>(-1555603773, StructureToStructureTemperature.OnStructureTemperatureRegisteredDelegate);
	}

	// Token: 0x060048E2 RID: 18658 RVA: 0x00198669 File Offset: 0x00196869
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.DefineConductiveCells();
		GameScenePartitioner.Instance.AddGlobalLayerListener(GameScenePartitioner.Instance.contactConductiveLayer, new Action<int, object>(this.OnAnyBuildingChanged));
	}

	// Token: 0x060048E3 RID: 18659 RVA: 0x00198697 File Offset: 0x00196897
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.RemoveGlobalLayerListener(GameScenePartitioner.Instance.contactConductiveLayer, new Action<int, object>(this.OnAnyBuildingChanged));
		this.UnregisterToSIM();
		base.OnCleanUp();
	}

	// Token: 0x060048E4 RID: 18660 RVA: 0x001986C8 File Offset: 0x001968C8
	private void OnStructureTemperatureRegistered(object _sim_handle)
	{
		int num = (int)_sim_handle;
		this.RegisterToSIM(num);
	}

	// Token: 0x060048E5 RID: 18661 RVA: 0x001986E4 File Offset: 0x001968E4
	private void RegisterToSIM(int sim_handle)
	{
		string name = this.building.Def.Name;
		SimMessages.RegisterBuildingToBuildingHeatExchange(sim_handle2, Game.Instance.simComponentCallbackManager.Add(delegate(int sim_handle, object callback_data)
		{
			this.OnSimRegistered(sim_handle);
		}, null, "StructureToStructureTemperature.SimRegister").index);
	}

	// Token: 0x060048E6 RID: 18662 RVA: 0x00198731 File Offset: 0x00196931
	private void OnSimRegistered(int sim_handle)
	{
		if (sim_handle != -1)
		{
			this.selfHandle = sim_handle;
			this.hasBeenRegister = true;
			if (this.buildingDestroyed)
			{
				this.UnregisterToSIM();
				return;
			}
			this.Refresh_InContactBuildings();
		}
	}

	// Token: 0x060048E7 RID: 18663 RVA: 0x0019875A File Offset: 0x0019695A
	private void UnregisterToSIM()
	{
		if (this.hasBeenRegister)
		{
			SimMessages.RemoveBuildingToBuildingHeatExchange(this.selfHandle, -1);
		}
		this.buildingDestroyed = true;
	}

	// Token: 0x060048E8 RID: 18664 RVA: 0x00198778 File Offset: 0x00196978
	private void DefineConductiveCells()
	{
		this.conductiveCells = new List<int>(this.building.PlacementCells);
		this.conductiveCells.Remove(this.building.GetUtilityInputCell());
		this.conductiveCells.Remove(this.building.GetUtilityOutputCell());
	}

	// Token: 0x060048E9 RID: 18665 RVA: 0x001987C9 File Offset: 0x001969C9
	private void Add(StructureToStructureTemperature.InContactBuildingData buildingData)
	{
		if (this.inContactBuildings.Add(buildingData.buildingInContact))
		{
			SimMessages.AddBuildingToBuildingHeatExchange(this.selfHandle, buildingData.buildingInContact, buildingData.cellsInContact);
		}
	}

	// Token: 0x060048EA RID: 18666 RVA: 0x001987F5 File Offset: 0x001969F5
	private void Remove(int building)
	{
		if (this.inContactBuildings.Contains(building))
		{
			this.inContactBuildings.Remove(building);
			SimMessages.RemoveBuildingInContactFromBuildingToBuildingHeatExchange(this.selfHandle, building);
		}
	}

	// Token: 0x060048EB RID: 18667 RVA: 0x00198820 File Offset: 0x00196A20
	private void OnAnyBuildingChanged(int _cell, object _data)
	{
		if (this.hasBeenRegister)
		{
			StructureToStructureTemperature.BuildingChangedObj buildingChangedObj = (StructureToStructureTemperature.BuildingChangedObj)_data;
			bool flag = false;
			int num = 0;
			for (int i = 0; i < buildingChangedObj.building.PlacementCells.Length; i++)
			{
				int num2 = buildingChangedObj.building.PlacementCells[i];
				if (this.conductiveCells.Contains(num2))
				{
					flag = true;
					num++;
				}
			}
			if (flag)
			{
				int simHandler = buildingChangedObj.simHandler;
				StructureToStructureTemperature.BuildingChangeType changeType = buildingChangedObj.changeType;
				if (changeType == StructureToStructureTemperature.BuildingChangeType.Created)
				{
					StructureToStructureTemperature.InContactBuildingData inContactBuildingData = new StructureToStructureTemperature.InContactBuildingData
					{
						buildingInContact = simHandler,
						cellsInContact = num
					};
					this.Add(inContactBuildingData);
					return;
				}
				if (changeType != StructureToStructureTemperature.BuildingChangeType.Destroyed)
				{
					return;
				}
				this.Remove(simHandler);
			}
		}
	}

	// Token: 0x060048EC RID: 18668 RVA: 0x001988CC File Offset: 0x00196ACC
	private void Refresh_InContactBuildings()
	{
		foreach (StructureToStructureTemperature.InContactBuildingData inContactBuildingData in this.GetAll_InContact_Buildings())
		{
			this.Add(inContactBuildingData);
		}
	}

	// Token: 0x060048ED RID: 18669 RVA: 0x00198920 File Offset: 0x00196B20
	private List<StructureToStructureTemperature.InContactBuildingData> GetAll_InContact_Buildings()
	{
		Dictionary<Building, int> dictionary = new Dictionary<Building, int>();
		List<StructureToStructureTemperature.InContactBuildingData> list = new List<StructureToStructureTemperature.InContactBuildingData>();
		List<GameObject> buildingsInCell = new List<GameObject>();
		using (List<int>.Enumerator enumerator = this.conductiveCells.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int cell = enumerator.Current;
				buildingsInCell.Clear();
				Action<int> action = delegate(int layer)
				{
					GameObject gameObject = Grid.Objects[cell, layer];
					if (gameObject != null && !buildingsInCell.Contains(gameObject))
					{
						buildingsInCell.Add(gameObject);
					}
				};
				action(1);
				action(26);
				action(27);
				action(31);
				action(32);
				action(30);
				action(12);
				action(13);
				action(16);
				action(17);
				action(24);
				action(2);
				for (int i = 0; i < buildingsInCell.Count; i++)
				{
					Building building = ((buildingsInCell[i] == null) ? null : buildingsInCell[i].GetComponent<Building>());
					if (building != null && building.Def.UseStructureTemperature && building.PlacementCellsContainCell(cell))
					{
						if (!dictionary.ContainsKey(building))
						{
							dictionary.Add(building, 0);
						}
						Dictionary<Building, int> dictionary2 = dictionary;
						Building building2 = building;
						int num = dictionary2[building2];
						dictionary2[building2] = num + 1;
					}
				}
			}
		}
		foreach (Building building3 in dictionary.Keys)
		{
			HandleVector<int>.Handle handle = GameComps.StructureTemperatures.GetHandle(building3);
			if (handle != HandleVector<int>.InvalidHandle)
			{
				int simHandleCopy = GameComps.StructureTemperatures.GetPayload(handle).simHandleCopy;
				StructureToStructureTemperature.InContactBuildingData inContactBuildingData = new StructureToStructureTemperature.InContactBuildingData
				{
					buildingInContact = simHandleCopy,
					cellsInContact = dictionary[building3]
				};
				list.Add(inContactBuildingData);
			}
		}
		return list;
	}

	// Token: 0x04002FE5 RID: 12261
	[MyCmpGet]
	private Building building;

	// Token: 0x04002FE6 RID: 12262
	private List<int> conductiveCells;

	// Token: 0x04002FE7 RID: 12263
	private HashSet<int> inContactBuildings = new HashSet<int>();

	// Token: 0x04002FE8 RID: 12264
	private bool hasBeenRegister;

	// Token: 0x04002FE9 RID: 12265
	private bool buildingDestroyed;

	// Token: 0x04002FEA RID: 12266
	private int selfHandle;

	// Token: 0x04002FEB RID: 12267
	protected static readonly EventSystem.IntraObjectHandler<StructureToStructureTemperature> OnStructureTemperatureRegisteredDelegate = new EventSystem.IntraObjectHandler<StructureToStructureTemperature>(delegate(StructureToStructureTemperature component, object data)
	{
		component.OnStructureTemperatureRegistered(data);
	});

	// Token: 0x02001796 RID: 6038
	public enum BuildingChangeType
	{
		// Token: 0x04006D81 RID: 28033
		Created,
		// Token: 0x04006D82 RID: 28034
		Destroyed,
		// Token: 0x04006D83 RID: 28035
		Moved
	}

	// Token: 0x02001797 RID: 6039
	public struct InContactBuildingData
	{
		// Token: 0x04006D84 RID: 28036
		public int buildingInContact;

		// Token: 0x04006D85 RID: 28037
		public int cellsInContact;
	}

	// Token: 0x02001798 RID: 6040
	public struct BuildingChangedObj
	{
		// Token: 0x06008B4D RID: 35661 RVA: 0x002FF390 File Offset: 0x002FD590
		public BuildingChangedObj(StructureToStructureTemperature.BuildingChangeType _changeType, Building _building, int sim_handler)
		{
			this.changeType = _changeType;
			this.building = _building;
			this.simHandler = sim_handler;
		}

		// Token: 0x04006D86 RID: 28038
		public StructureToStructureTemperature.BuildingChangeType changeType;

		// Token: 0x04006D87 RID: 28039
		public int simHandler;

		// Token: 0x04006D88 RID: 28040
		public Building building;
	}
}
