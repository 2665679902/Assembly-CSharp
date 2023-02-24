using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000999 RID: 2457
public class StructureTemperatureComponents : KGameObjectSplitComponentManager<StructureTemperatureHeader, StructureTemperaturePayload>
{
	// Token: 0x060048C2 RID: 18626 RVA: 0x00197754 File Offset: 0x00195954
	public HandleVector<int>.Handle Add(GameObject go)
	{
		StructureTemperaturePayload structureTemperaturePayload = new StructureTemperaturePayload(go);
		return base.Add(go, new StructureTemperatureHeader
		{
			dirty = false,
			simHandle = -1,
			isActiveBuilding = false
		}, ref structureTemperaturePayload);
	}

	// Token: 0x060048C3 RID: 18627 RVA: 0x00197793 File Offset: 0x00195993
	public static void ClearInstanceMap()
	{
		StructureTemperatureComponents.handleInstanceMap.Clear();
	}

	// Token: 0x060048C5 RID: 18629 RVA: 0x001977A8 File Offset: 0x001959A8
	protected override void OnPrefabInit(HandleVector<int>.Handle handle)
	{
		this.InitializeStatusItem();
		base.OnPrefabInit(handle);
		StructureTemperatureHeader structureTemperatureHeader;
		StructureTemperaturePayload structureTemperaturePayload;
		base.GetData(handle, out structureTemperatureHeader, out structureTemperaturePayload);
		structureTemperaturePayload.primaryElement.getTemperatureCallback = new PrimaryElement.GetTemperatureCallback(StructureTemperatureComponents.OnGetTemperature);
		structureTemperaturePayload.primaryElement.setTemperatureCallback = new PrimaryElement.SetTemperatureCallback(StructureTemperatureComponents.OnSetTemperature);
		structureTemperatureHeader.isActiveBuilding = structureTemperaturePayload.building.Def.SelfHeatKilowattsWhenActive != 0f || structureTemperaturePayload.ExhaustKilowatts != 0f;
		base.SetHeader(handle, structureTemperatureHeader);
	}

	// Token: 0x060048C6 RID: 18630 RVA: 0x00197838 File Offset: 0x00195A38
	private void InitializeStatusItem()
	{
		if (this.operatingEnergyStatusItem != null)
		{
			return;
		}
		this.operatingEnergyStatusItem = new StatusItem("OperatingEnergy", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
		this.operatingEnergyStatusItem.resolveStringCallback = delegate(string str, object ev_data)
		{
			int num = (int)ev_data;
			HandleVector<int>.Handle handle = StructureTemperatureComponents.handleInstanceMap[num];
			StructureTemperaturePayload payload = base.GetPayload(handle);
			if (str != BUILDING.STATUSITEMS.OPERATINGENERGY.TOOLTIP)
			{
				try
				{
					return string.Format(str, GameUtil.GetFormattedHeatEnergy(payload.TotalEnergyProducedKW * 1000f, GameUtil.HeatEnergyFormatterUnit.Automatic));
				}
				catch (Exception ex)
				{
					global::Debug.LogWarning(ex);
					global::Debug.LogWarning(BUILDING.STATUSITEMS.OPERATINGENERGY.TOOLTIP);
					global::Debug.LogWarning(str);
					return str;
				}
			}
			string text = "";
			foreach (StructureTemperaturePayload.EnergySource energySource in payload.energySourcesKW)
			{
				text += string.Format(BUILDING.STATUSITEMS.OPERATINGENERGY.LINEITEM, energySource.source, GameUtil.GetFormattedHeatEnergy(energySource.value * 1000f, GameUtil.HeatEnergyFormatterUnit.DTU_S));
			}
			str = string.Format(str, GameUtil.GetFormattedHeatEnergy(payload.TotalEnergyProducedKW * 1000f, GameUtil.HeatEnergyFormatterUnit.DTU_S), text);
			return str;
		};
	}

	// Token: 0x060048C7 RID: 18631 RVA: 0x00197890 File Offset: 0x00195A90
	protected override void OnSpawn(HandleVector<int>.Handle handle)
	{
		StructureTemperatureHeader structureTemperatureHeader;
		StructureTemperaturePayload structureTemperaturePayload;
		base.GetData(handle, out structureTemperatureHeader, out structureTemperaturePayload);
		if (structureTemperaturePayload.operational != null && structureTemperatureHeader.isActiveBuilding)
		{
			structureTemperaturePayload.primaryElement.Subscribe(824508782, delegate(object ev_data)
			{
				StructureTemperatureComponents.OnActiveChanged(handle);
			});
		}
		structureTemperaturePayload.maxTemperature = ((structureTemperaturePayload.overheatable != null) ? structureTemperaturePayload.overheatable.OverheatTemperature : 10000f);
		if (structureTemperaturePayload.maxTemperature <= 0f)
		{
			global::Debug.LogError("invalid max temperature");
		}
		base.SetPayload(handle, ref structureTemperaturePayload);
		this.SimRegister(handle, ref structureTemperatureHeader, ref structureTemperaturePayload);
	}

	// Token: 0x060048C8 RID: 18632 RVA: 0x0019794C File Offset: 0x00195B4C
	private static void OnActiveChanged(HandleVector<int>.Handle handle)
	{
		StructureTemperatureHeader structureTemperatureHeader;
		StructureTemperaturePayload structureTemperaturePayload;
		GameComps.StructureTemperatures.GetData(handle, out structureTemperatureHeader, out structureTemperaturePayload);
		structureTemperaturePayload.primaryElement.InternalTemperature = structureTemperaturePayload.Temperature;
		structureTemperatureHeader.dirty = true;
		GameComps.StructureTemperatures.SetHeader(handle, structureTemperatureHeader);
	}

	// Token: 0x060048C9 RID: 18633 RVA: 0x0019798F File Offset: 0x00195B8F
	protected override void OnCleanUp(HandleVector<int>.Handle handle)
	{
		this.SimUnregister(handle);
		base.OnCleanUp(handle);
	}

	// Token: 0x060048CA RID: 18634 RVA: 0x001979A0 File Offset: 0x00195BA0
	public override void Sim200ms(float dt)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		List<StructureTemperatureHeader> list;
		List<StructureTemperaturePayload> list2;
		base.GetDataLists(out list, out list2);
		ListPool<int, StructureTemperatureComponents>.PooledList pooledList = ListPool<int, StructureTemperatureComponents>.Allocate();
		pooledList.Capacity = Math.Max(pooledList.Capacity, list.Count);
		ListPool<int, StructureTemperatureComponents>.PooledList pooledList2 = ListPool<int, StructureTemperatureComponents>.Allocate();
		pooledList2.Capacity = Math.Max(pooledList2.Capacity, list.Count);
		ListPool<int, StructureTemperatureComponents>.PooledList pooledList3 = ListPool<int, StructureTemperatureComponents>.Allocate();
		pooledList3.Capacity = Math.Max(pooledList3.Capacity, list.Count);
		for (int num4 = 0; num4 != list.Count; num4++)
		{
			StructureTemperatureHeader structureTemperatureHeader = list[num4];
			if (Sim.IsValidHandle(structureTemperatureHeader.simHandle))
			{
				pooledList.Add(num4);
				if (structureTemperatureHeader.dirty)
				{
					pooledList2.Add(num4);
					structureTemperatureHeader.dirty = false;
					list[num4] = structureTemperatureHeader;
				}
				if (structureTemperatureHeader.isActiveBuilding)
				{
					pooledList3.Add(num4);
				}
			}
		}
		foreach (int num5 in pooledList2)
		{
			StructureTemperaturePayload structureTemperaturePayload = list2[num5];
			StructureTemperatureComponents.UpdateSimState(ref structureTemperaturePayload);
		}
		foreach (int num6 in pooledList2)
		{
			if (list2[num6].pendingEnergyModifications != 0f)
			{
				StructureTemperaturePayload structureTemperaturePayload2 = list2[num6];
				SimMessages.ModifyBuildingEnergy(structureTemperaturePayload2.simHandleCopy, structureTemperaturePayload2.pendingEnergyModifications, 0f, 10000f);
				structureTemperaturePayload2.pendingEnergyModifications = 0f;
				list2[num6] = structureTemperaturePayload2;
			}
		}
		foreach (int num7 in pooledList3)
		{
			StructureTemperaturePayload structureTemperaturePayload3 = list2[num7];
			if (structureTemperaturePayload3.operational == null || structureTemperaturePayload3.operational.IsActive)
			{
				num++;
				if (!structureTemperaturePayload3.isActiveStatusItemSet)
				{
					num3++;
					structureTemperaturePayload3.primaryElement.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.OperatingEnergy, this.operatingEnergyStatusItem, structureTemperaturePayload3.simHandleCopy);
					structureTemperaturePayload3.isActiveStatusItemSet = true;
				}
				structureTemperaturePayload3.energySourcesKW = this.AccumulateProducedEnergyKW(structureTemperaturePayload3.energySourcesKW, structureTemperaturePayload3.OperatingKilowatts, BUILDING.STATUSITEMS.OPERATINGENERGY.OPERATING);
				if (structureTemperaturePayload3.ExhaustKilowatts != 0f)
				{
					num2++;
					Extents extents = structureTemperaturePayload3.GetExtents();
					int num8 = extents.width * extents.height;
					float num9 = structureTemperaturePayload3.ExhaustKilowatts * dt / (float)num8;
					for (int i = 0; i < extents.height; i++)
					{
						int num10 = extents.y + i;
						for (int j = 0; j < extents.width; j++)
						{
							int num11 = extents.x + j;
							int num12 = num10 * Grid.WidthInCells + num11;
							float num13 = Mathf.Min(Grid.Mass[num12], 1.5f) / 1.5f;
							float num14 = num9 * num13;
							SimMessages.ModifyEnergy(num12, num14, structureTemperaturePayload3.maxTemperature, SimMessages.EnergySourceID.StructureTemperature);
						}
					}
					structureTemperaturePayload3.energySourcesKW = this.AccumulateProducedEnergyKW(structureTemperaturePayload3.energySourcesKW, structureTemperaturePayload3.ExhaustKilowatts, BUILDING.STATUSITEMS.OPERATINGENERGY.EXHAUSTING);
				}
			}
			else if (structureTemperaturePayload3.isActiveStatusItemSet)
			{
				num3++;
				structureTemperaturePayload3.primaryElement.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.OperatingEnergy, null, null);
				structureTemperaturePayload3.isActiveStatusItemSet = false;
			}
			list2[num7] = structureTemperaturePayload3;
		}
		pooledList3.Recycle();
		pooledList2.Recycle();
		pooledList.Recycle();
	}

	// Token: 0x060048CB RID: 18635 RVA: 0x00197DB0 File Offset: 0x00195FB0
	private static void UpdateSimState(ref StructureTemperaturePayload payload)
	{
		DebugUtil.Assert(Sim.IsValidHandle(payload.simHandleCopy));
		float internalTemperature = payload.primaryElement.InternalTemperature;
		BuildingDef def = payload.building.Def;
		float num = def.MassForTemperatureModification;
		float operatingKilowatts = payload.OperatingKilowatts;
		float num2 = ((payload.overheatable != null) ? payload.overheatable.OverheatTemperature : 10000f);
		if (!payload.enabled || payload.bypass)
		{
			num = 0f;
		}
		Extents extents = payload.GetExtents();
		ushort idx = payload.primaryElement.Element.idx;
		SimMessages.ModifyBuildingHeatExchange(payload.simHandleCopy, extents, num, internalTemperature, def.ThermalConductivity, num2, operatingKilowatts, idx);
	}

	// Token: 0x060048CC RID: 18636 RVA: 0x00197E60 File Offset: 0x00196060
	private unsafe static float OnGetTemperature(PrimaryElement primary_element)
	{
		HandleVector<int>.Handle handle = GameComps.StructureTemperatures.GetHandle(primary_element.gameObject);
		StructureTemperaturePayload payload = GameComps.StructureTemperatures.GetPayload(handle);
		float num;
		if (Sim.IsValidHandle(payload.simHandleCopy) && payload.enabled)
		{
			if (!payload.bypass)
			{
				int handleIndex = Sim.GetHandleIndex(payload.simHandleCopy);
				num = Game.Instance.simData.buildingTemperatures[handleIndex].temperature;
			}
			else
			{
				int num2 = Grid.PosToCell(payload.primaryElement.transform.GetPosition());
				num = Grid.Temperature[num2];
			}
		}
		else
		{
			num = payload.primaryElement.InternalTemperature;
		}
		return num;
	}

	// Token: 0x060048CD RID: 18637 RVA: 0x00197F0C File Offset: 0x0019610C
	private static void OnSetTemperature(PrimaryElement primary_element, float temperature)
	{
		HandleVector<int>.Handle handle = GameComps.StructureTemperatures.GetHandle(primary_element.gameObject);
		StructureTemperatureHeader structureTemperatureHeader;
		StructureTemperaturePayload structureTemperaturePayload;
		GameComps.StructureTemperatures.GetData(handle, out structureTemperatureHeader, out structureTemperaturePayload);
		structureTemperaturePayload.primaryElement.InternalTemperature = temperature;
		structureTemperatureHeader.dirty = true;
		GameComps.StructureTemperatures.SetHeader(handle, structureTemperatureHeader);
		if (!structureTemperatureHeader.isActiveBuilding && Sim.IsValidHandle(structureTemperaturePayload.simHandleCopy))
		{
			StructureTemperatureComponents.UpdateSimState(ref structureTemperaturePayload);
			if (structureTemperaturePayload.pendingEnergyModifications != 0f)
			{
				SimMessages.ModifyBuildingEnergy(structureTemperaturePayload.simHandleCopy, structureTemperaturePayload.pendingEnergyModifications, 0f, 10000f);
				structureTemperaturePayload.pendingEnergyModifications = 0f;
				GameComps.StructureTemperatures.SetPayload(handle, ref structureTemperaturePayload);
			}
		}
	}

	// Token: 0x060048CE RID: 18638 RVA: 0x00197FB8 File Offset: 0x001961B8
	public void ProduceEnergy(HandleVector<int>.Handle handle, float delta_kilojoules, string source, float display_dt)
	{
		StructureTemperaturePayload payload = base.GetPayload(handle);
		if (Sim.IsValidHandle(payload.simHandleCopy))
		{
			SimMessages.ModifyBuildingEnergy(payload.simHandleCopy, delta_kilojoules, 0f, 10000f);
		}
		else
		{
			payload.pendingEnergyModifications += delta_kilojoules;
			StructureTemperatureHeader header = base.GetHeader(handle);
			header.dirty = true;
			base.SetHeader(handle, header);
		}
		payload.energySourcesKW = this.AccumulateProducedEnergyKW(payload.energySourcesKW, delta_kilojoules / display_dt, source);
		base.SetPayload(handle, ref payload);
	}

	// Token: 0x060048CF RID: 18639 RVA: 0x00198038 File Offset: 0x00196238
	private List<StructureTemperaturePayload.EnergySource> AccumulateProducedEnergyKW(List<StructureTemperaturePayload.EnergySource> sources, float kw, string source)
	{
		if (sources == null)
		{
			sources = new List<StructureTemperaturePayload.EnergySource>();
		}
		bool flag = false;
		for (int i = 0; i < sources.Count; i++)
		{
			if (sources[i].source == source)
			{
				sources[i].Accumulate(kw);
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			sources.Add(new StructureTemperaturePayload.EnergySource(kw, source));
		}
		return sources;
	}

	// Token: 0x060048D0 RID: 18640 RVA: 0x00198098 File Offset: 0x00196298
	public static void DoStateTransition(int sim_handle)
	{
		HandleVector<int>.Handle invalidHandle = HandleVector<int>.InvalidHandle;
		if (StructureTemperatureComponents.handleInstanceMap.TryGetValue(sim_handle, out invalidHandle))
		{
			StructureTemperatureComponents.DoMelt(GameComps.StructureTemperatures.GetPayload(invalidHandle).primaryElement);
		}
	}

	// Token: 0x060048D1 RID: 18641 RVA: 0x001980D4 File Offset: 0x001962D4
	public static void DoMelt(PrimaryElement primary_element)
	{
		Element element = primary_element.Element;
		if (element.highTempTransitionTarget != SimHashes.Unobtanium)
		{
			SimMessages.AddRemoveSubstance(Grid.PosToCell(primary_element.transform.GetPosition()), element.highTempTransitionTarget, CellEventLogger.Instance.OreMelted, primary_element.Mass, primary_element.Element.highTemp, primary_element.DiseaseIdx, primary_element.DiseaseCount, true, -1);
			Util.KDestroyGameObject(primary_element.gameObject);
		}
	}

	// Token: 0x060048D2 RID: 18642 RVA: 0x00198144 File Offset: 0x00196344
	public static void DoOverheat(int sim_handle)
	{
		HandleVector<int>.Handle invalidHandle = HandleVector<int>.InvalidHandle;
		if (StructureTemperatureComponents.handleInstanceMap.TryGetValue(sim_handle, out invalidHandle))
		{
			GameComps.StructureTemperatures.GetPayload(invalidHandle).primaryElement.gameObject.Trigger(1832602615, null);
		}
	}

	// Token: 0x060048D3 RID: 18643 RVA: 0x0019818C File Offset: 0x0019638C
	public static void DoNoLongerOverheated(int sim_handle)
	{
		HandleVector<int>.Handle invalidHandle = HandleVector<int>.InvalidHandle;
		if (StructureTemperatureComponents.handleInstanceMap.TryGetValue(sim_handle, out invalidHandle))
		{
			GameComps.StructureTemperatures.GetPayload(invalidHandle).primaryElement.gameObject.Trigger(171119937, null);
		}
	}

	// Token: 0x060048D4 RID: 18644 RVA: 0x001981D1 File Offset: 0x001963D1
	public bool IsEnabled(HandleVector<int>.Handle handle)
	{
		return base.GetPayload(handle).enabled;
	}

	// Token: 0x060048D5 RID: 18645 RVA: 0x001981E0 File Offset: 0x001963E0
	private void Enable(HandleVector<int>.Handle handle, bool isEnabled)
	{
		StructureTemperatureHeader structureTemperatureHeader;
		StructureTemperaturePayload structureTemperaturePayload;
		base.GetData(handle, out structureTemperatureHeader, out structureTemperaturePayload);
		structureTemperatureHeader.dirty = true;
		structureTemperaturePayload.enabled = isEnabled;
		base.SetData(handle, structureTemperatureHeader, ref structureTemperaturePayload);
	}

	// Token: 0x060048D6 RID: 18646 RVA: 0x00198212 File Offset: 0x00196412
	public void Enable(HandleVector<int>.Handle handle)
	{
		this.Enable(handle, true);
	}

	// Token: 0x060048D7 RID: 18647 RVA: 0x0019821C File Offset: 0x0019641C
	public void Disable(HandleVector<int>.Handle handle)
	{
		this.Enable(handle, false);
	}

	// Token: 0x060048D8 RID: 18648 RVA: 0x00198226 File Offset: 0x00196426
	public bool IsBypassed(HandleVector<int>.Handle handle)
	{
		return base.GetPayload(handle).bypass;
	}

	// Token: 0x060048D9 RID: 18649 RVA: 0x00198234 File Offset: 0x00196434
	private void Bypass(HandleVector<int>.Handle handle, bool bypass)
	{
		StructureTemperatureHeader structureTemperatureHeader;
		StructureTemperaturePayload structureTemperaturePayload;
		base.GetData(handle, out structureTemperatureHeader, out structureTemperaturePayload);
		structureTemperatureHeader.dirty = true;
		structureTemperaturePayload.bypass = bypass;
		base.SetData(handle, structureTemperatureHeader, ref structureTemperaturePayload);
	}

	// Token: 0x060048DA RID: 18650 RVA: 0x00198266 File Offset: 0x00196466
	public void Bypass(HandleVector<int>.Handle handle)
	{
		this.Bypass(handle, true);
	}

	// Token: 0x060048DB RID: 18651 RVA: 0x00198270 File Offset: 0x00196470
	public void UnBypass(HandleVector<int>.Handle handle)
	{
		this.Bypass(handle, false);
	}

	// Token: 0x060048DC RID: 18652 RVA: 0x0019827C File Offset: 0x0019647C
	protected void SimRegister(HandleVector<int>.Handle handle, ref StructureTemperatureHeader header, ref StructureTemperaturePayload payload)
	{
		if (payload.simHandleCopy != -1)
		{
			return;
		}
		PrimaryElement primaryElement = payload.primaryElement;
		if (primaryElement.Mass <= 0f)
		{
			return;
		}
		if (primaryElement.Element.IsTemperatureInsulated)
		{
			return;
		}
		payload.simHandleCopy = -2;
		string dbg_name = primaryElement.name;
		HandleVector<Game.ComplexCallbackInfo<int>>.Handle handle2 = Game.Instance.simComponentCallbackManager.Add(delegate(int sim_handle, object callback_data)
		{
			StructureTemperatureComponents.OnSimRegistered(handle, sim_handle, dbg_name);
		}, null, "StructureTemperature.SimRegister");
		BuildingDef def = primaryElement.GetComponent<Building>().Def;
		float internalTemperature = primaryElement.InternalTemperature;
		float massForTemperatureModification = def.MassForTemperatureModification;
		float operatingKilowatts = payload.OperatingKilowatts;
		Extents extents = payload.GetExtents();
		ushort idx = primaryElement.Element.idx;
		SimMessages.AddBuildingHeatExchange(extents, massForTemperatureModification, internalTemperature, def.ThermalConductivity, operatingKilowatts, idx, handle2.index);
		header.simHandle = payload.simHandleCopy;
		base.SetData(handle, header, ref payload);
	}

	// Token: 0x060048DD RID: 18653 RVA: 0x0019836C File Offset: 0x0019656C
	private static void OnSimRegistered(HandleVector<int>.Handle handle, int sim_handle, string dbg_name)
	{
		if (!GameComps.StructureTemperatures.IsValid(handle))
		{
			return;
		}
		if (!GameComps.StructureTemperatures.IsVersionValid(handle))
		{
			return;
		}
		StructureTemperatureHeader structureTemperatureHeader;
		StructureTemperaturePayload structureTemperaturePayload;
		GameComps.StructureTemperatures.GetData(handle, out structureTemperatureHeader, out structureTemperaturePayload);
		if (structureTemperaturePayload.simHandleCopy == -2)
		{
			StructureTemperatureComponents.handleInstanceMap[sim_handle] = handle;
			structureTemperatureHeader.simHandle = sim_handle;
			structureTemperaturePayload.simHandleCopy = sim_handle;
			GameComps.StructureTemperatures.SetData(handle, structureTemperatureHeader, ref structureTemperaturePayload);
			structureTemperaturePayload.primaryElement.Trigger(-1555603773, sim_handle);
			int num = Grid.PosToCell(structureTemperaturePayload.building.transform.GetPosition());
			GameScenePartitioner.Instance.TriggerEvent(num, GameScenePartitioner.Instance.contactConductiveLayer, new StructureToStructureTemperature.BuildingChangedObj(StructureToStructureTemperature.BuildingChangeType.Created, structureTemperaturePayload.building, sim_handle));
			return;
		}
		SimMessages.RemoveBuildingHeatExchange(sim_handle, -1);
	}

	// Token: 0x060048DE RID: 18654 RVA: 0x00198434 File Offset: 0x00196634
	protected unsafe void SimUnregister(HandleVector<int>.Handle handle)
	{
		if (!GameComps.StructureTemperatures.IsVersionValid(handle))
		{
			KCrashReporter.Assert(false, "Handle version mismatch in StructureTemperature.SimUnregister");
			return;
		}
		if (KMonoBehaviour.isLoadingScene)
		{
			return;
		}
		StructureTemperatureHeader structureTemperatureHeader;
		StructureTemperaturePayload structureTemperaturePayload;
		GameComps.StructureTemperatures.GetData(handle, out structureTemperatureHeader, out structureTemperaturePayload);
		if (structureTemperaturePayload.simHandleCopy != -1)
		{
			int num = Grid.PosToCell(structureTemperaturePayload.building);
			GameScenePartitioner.Instance.TriggerEvent(num, GameScenePartitioner.Instance.contactConductiveLayer, new StructureToStructureTemperature.BuildingChangedObj(StructureToStructureTemperature.BuildingChangeType.Destroyed, structureTemperaturePayload.building, structureTemperaturePayload.simHandleCopy));
			if (Sim.IsValidHandle(structureTemperaturePayload.simHandleCopy))
			{
				int handleIndex = Sim.GetHandleIndex(structureTemperaturePayload.simHandleCopy);
				structureTemperaturePayload.primaryElement.InternalTemperature = Game.Instance.simData.buildingTemperatures[handleIndex].temperature;
				SimMessages.RemoveBuildingHeatExchange(structureTemperaturePayload.simHandleCopy, -1);
				StructureTemperatureComponents.handleInstanceMap.Remove(structureTemperaturePayload.simHandleCopy);
			}
			structureTemperaturePayload.simHandleCopy = -1;
			structureTemperatureHeader.simHandle = -1;
			base.SetData(handle, structureTemperatureHeader, ref structureTemperaturePayload);
		}
	}

	// Token: 0x04002FE2 RID: 12258
	private const float MAX_PRESSURE = 1.5f;

	// Token: 0x04002FE3 RID: 12259
	private static Dictionary<int, HandleVector<int>.Handle> handleInstanceMap = new Dictionary<int, HandleVector<int>.Handle>();

	// Token: 0x04002FE4 RID: 12260
	private StatusItem operatingEnergyStatusItem;
}
