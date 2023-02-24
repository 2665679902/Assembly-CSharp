using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200091B RID: 2331
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/SimCellOccupier")]
public class SimCellOccupier : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x170004CD RID: 1229
	// (get) Token: 0x060043CD RID: 17357 RVA: 0x0017EAD0 File Offset: 0x0017CCD0
	public bool IsVisuallySolid
	{
		get
		{
			return this.doReplaceElement;
		}
	}

	// Token: 0x060043CE RID: 17358 RVA: 0x0017EAD8 File Offset: 0x0017CCD8
	protected override void OnPrefabInit()
	{
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Normal, null);
		if (this.building.Def.IsFoundation)
		{
			this.setConstructedTile = true;
		}
	}

	// Token: 0x060043CF RID: 17359 RVA: 0x0017EB2C File Offset: 0x0017CD2C
	protected override void OnSpawn()
	{
		HandleVector<Game.CallbackInfo>.Handle callbackHandle = Game.Instance.callbackManager.Add(new Game.CallbackInfo(new System.Action(this.OnModifyComplete), false));
		int num = this.building.Def.PlacementOffsets.Length;
		float mass_per_cell = this.primaryElement.Mass / (float)num;
		this.building.RunOnArea(delegate(int offset_cell)
		{
			if (this.doReplaceElement)
			{
				SimMessages.ReplaceAndDisplaceElement(offset_cell, this.primaryElement.ElementID, CellEventLogger.Instance.SimCellOccupierOnSpawn, mass_per_cell, this.primaryElement.Temperature, this.primaryElement.DiseaseIdx, this.primaryElement.DiseaseCount, callbackHandle.index);
				callbackHandle = HandleVector<Game.CallbackInfo>.InvalidHandle;
				SimMessages.SetStrength(offset_cell, 0, this.strengthMultiplier);
				Game.Instance.RemoveSolidChangedFilter(offset_cell);
			}
			else
			{
				if (SaveGame.Instance.sandboxEnabled && Grid.Element[offset_cell].IsSolid)
				{
					SimMessages.Dig(offset_cell, -1, false);
				}
				this.ForceSetGameCellData(offset_cell);
				Game.Instance.AddSolidChangedFilter(offset_cell);
			}
			Sim.Cell.Properties simCellProperties = this.GetSimCellProperties();
			SimMessages.SetCellProperties(offset_cell, (byte)simCellProperties);
			Grid.RenderedByWorld[offset_cell] = false;
			Game.Instance.GetComponent<EntombedItemVisualizer>().ForceClear(offset_cell);
		});
		base.Subscribe<SimCellOccupier>(-1699355994, SimCellOccupier.OnBuildingRepairedDelegate);
	}

	// Token: 0x060043D0 RID: 17360 RVA: 0x0017EBBC File Offset: 0x0017CDBC
	protected override void OnCleanUp()
	{
		if (this.callDestroy)
		{
			this.DestroySelf(null);
		}
	}

	// Token: 0x060043D1 RID: 17361 RVA: 0x0017EBD0 File Offset: 0x0017CDD0
	private Sim.Cell.Properties GetSimCellProperties()
	{
		Sim.Cell.Properties properties = Sim.Cell.Properties.SolidImpermeable;
		if (this.setGasImpermeable)
		{
			properties |= Sim.Cell.Properties.GasImpermeable;
		}
		if (this.setLiquidImpermeable)
		{
			properties |= Sim.Cell.Properties.LiquidImpermeable;
		}
		if (this.setTransparent)
		{
			properties |= Sim.Cell.Properties.Transparent;
		}
		if (this.setOpaque)
		{
			properties |= Sim.Cell.Properties.Opaque;
		}
		if (this.setConstructedTile)
		{
			properties |= Sim.Cell.Properties.ConstructedTile;
		}
		if (this.notifyOnMelt)
		{
			properties |= Sim.Cell.Properties.NotifyOnMelt;
		}
		return properties;
	}

	// Token: 0x060043D2 RID: 17362 RVA: 0x0017EC30 File Offset: 0x0017CE30
	public void DestroySelf(System.Action onComplete)
	{
		this.callDestroy = false;
		for (int i = 0; i < this.building.PlacementCells.Length; i++)
		{
			int num = this.building.PlacementCells[i];
			Game.Instance.RemoveSolidChangedFilter(num);
			Sim.Cell.Properties simCellProperties = this.GetSimCellProperties();
			SimMessages.ClearCellProperties(num, (byte)simCellProperties);
			if (this.doReplaceElement && Grid.Element[num].id == this.primaryElement.ElementID)
			{
				HandleVector<int>.Handle handle = GameComps.DiseaseContainers.GetHandle(base.gameObject);
				if (handle.IsValid())
				{
					DiseaseHeader header = GameComps.DiseaseContainers.GetHeader(handle);
					header.diseaseIdx = Grid.DiseaseIdx[num];
					header.diseaseCount = Grid.DiseaseCount[num];
					GameComps.DiseaseContainers.SetHeader(handle, header);
				}
				if (onComplete != null)
				{
					HandleVector<Game.CallbackInfo>.Handle handle2 = Game.Instance.callbackManager.Add(new Game.CallbackInfo(onComplete, false));
					SimMessages.ReplaceElement(num, SimHashes.Vacuum, CellEventLogger.Instance.SimCellOccupierDestroySelf, 0f, -1f, byte.MaxValue, 0, handle2.index);
				}
				else
				{
					SimMessages.ReplaceElement(num, SimHashes.Vacuum, CellEventLogger.Instance.SimCellOccupierDestroySelf, 0f, -1f, byte.MaxValue, 0, -1);
				}
				SimMessages.SetStrength(num, 1, 1f);
			}
			else
			{
				Grid.SetSolid(num, false, CellEventLogger.Instance.SimCellOccupierDestroy);
				onComplete.Signal();
				World.Instance.OnSolidChanged(num);
				GameScenePartitioner.Instance.TriggerEvent(num, GameScenePartitioner.Instance.solidChangedLayer, null);
			}
		}
	}

	// Token: 0x060043D3 RID: 17363 RVA: 0x0017EDC3 File Offset: 0x0017CFC3
	public bool IsReady()
	{
		return this.isReady;
	}

	// Token: 0x060043D4 RID: 17364 RVA: 0x0017EDCC File Offset: 0x0017CFCC
	private void OnModifyComplete()
	{
		if (this == null || base.gameObject == null)
		{
			return;
		}
		this.isReady = true;
		base.GetComponent<PrimaryElement>().SetUseSimDiseaseInfo(true);
		Vector2I vector2I = Grid.PosToXY(base.transform.GetPosition());
		GameScenePartitioner.Instance.TriggerEvent(vector2I.x, vector2I.y, 1, 1, GameScenePartitioner.Instance.solidChangedLayer, null);
	}

	// Token: 0x060043D5 RID: 17365 RVA: 0x0017EE38 File Offset: 0x0017D038
	private void ForceSetGameCellData(int cell)
	{
		bool flag = !Grid.DupePassable[cell];
		Grid.SetSolid(cell, flag, CellEventLogger.Instance.SimCellOccupierForceSolid);
		Pathfinding.Instance.AddDirtyNavGridCell(cell);
		GameScenePartitioner.Instance.TriggerEvent(cell, GameScenePartitioner.Instance.solidChangedLayer, null);
		Grid.Damage[cell] = 0f;
	}

	// Token: 0x060043D6 RID: 17366 RVA: 0x0017EE94 File Offset: 0x0017D094
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = null;
		if (this.movementSpeedMultiplier != 1f)
		{
			list = new List<Descriptor>();
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.DUPLICANTMOVEMENTBOOST, GameUtil.AddPositiveSign(GameUtil.GetFormattedPercent(this.movementSpeedMultiplier * 100f - 100f, GameUtil.TimeSlice.None), this.movementSpeedMultiplier - 1f >= 0f)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.DUPLICANTMOVEMENTBOOST, GameUtil.GetFormattedPercent(this.movementSpeedMultiplier * 100f - 100f, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Effect);
			list.Add(descriptor);
		}
		return list;
	}

	// Token: 0x060043D7 RID: 17367 RVA: 0x0017EF3C File Offset: 0x0017D13C
	private void OnBuildingRepaired(object data)
	{
		BuildingHP buildingHP = (BuildingHP)data;
		float damage = 1f - (float)buildingHP.HitPoints / (float)buildingHP.MaxHitPoints;
		this.building.RunOnArea(delegate(int offset_cell)
		{
			WorldDamage.Instance.RestoreDamageToValue(offset_cell, damage);
		});
	}

	// Token: 0x04002D3F RID: 11583
	[MyCmpReq]
	private Building building;

	// Token: 0x04002D40 RID: 11584
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x04002D41 RID: 11585
	[SerializeField]
	public bool doReplaceElement = true;

	// Token: 0x04002D42 RID: 11586
	[SerializeField]
	public bool setGasImpermeable;

	// Token: 0x04002D43 RID: 11587
	[SerializeField]
	public bool setLiquidImpermeable;

	// Token: 0x04002D44 RID: 11588
	[SerializeField]
	public bool setTransparent;

	// Token: 0x04002D45 RID: 11589
	[SerializeField]
	public bool setOpaque;

	// Token: 0x04002D46 RID: 11590
	[SerializeField]
	public bool notifyOnMelt;

	// Token: 0x04002D47 RID: 11591
	[SerializeField]
	private bool setConstructedTile;

	// Token: 0x04002D48 RID: 11592
	[SerializeField]
	public float strengthMultiplier = 1f;

	// Token: 0x04002D49 RID: 11593
	[SerializeField]
	public float movementSpeedMultiplier = 1f;

	// Token: 0x04002D4A RID: 11594
	private bool isReady;

	// Token: 0x04002D4B RID: 11595
	private bool callDestroy = true;

	// Token: 0x04002D4C RID: 11596
	private static readonly EventSystem.IntraObjectHandler<SimCellOccupier> OnBuildingRepairedDelegate = new EventSystem.IntraObjectHandler<SimCellOccupier>(delegate(SimCellOccupier component, object data)
	{
		component.OnBuildingRepaired(data);
	});
}
