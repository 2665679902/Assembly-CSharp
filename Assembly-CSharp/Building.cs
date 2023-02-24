using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Database;
using STRINGS;
using UnityEngine;

// Token: 0x0200055F RID: 1375
[AddComponentMenu("KMonoBehaviour/scripts/Building")]
public class Building : KMonoBehaviour, IGameObjectEffectDescriptor, IUniformGridObject, IApproachable
{
	// Token: 0x1700018E RID: 398
	// (get) Token: 0x06002119 RID: 8473 RVA: 0x000B45AB File Offset: 0x000B27AB
	public Orientation Orientation
	{
		get
		{
			if (!(this.rotatable != null))
			{
				return Orientation.Neutral;
			}
			return this.rotatable.GetOrientation();
		}
	}

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x0600211A RID: 8474 RVA: 0x000B45C8 File Offset: 0x000B27C8
	public int[] PlacementCells
	{
		get
		{
			if (this.placementCells == null)
			{
				this.RefreshCells();
			}
			return this.placementCells;
		}
	}

	// Token: 0x0600211B RID: 8475 RVA: 0x000B45DE File Offset: 0x000B27DE
	public Extents GetExtents()
	{
		if (this.extents.width == 0 || this.extents.height == 0)
		{
			this.RefreshCells();
		}
		return this.extents;
	}

	// Token: 0x0600211C RID: 8476 RVA: 0x000B4608 File Offset: 0x000B2808
	public Extents GetValidPlacementExtents()
	{
		Extents extents = this.GetExtents();
		extents.x--;
		extents.y--;
		extents.width += 2;
		extents.height += 2;
		return extents;
	}

	// Token: 0x0600211D RID: 8477 RVA: 0x000B4650 File Offset: 0x000B2850
	public bool PlacementCellsContainCell(int cell)
	{
		for (int i = 0; i < this.PlacementCells.Length; i++)
		{
			if (this.PlacementCells[i] == cell)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600211E RID: 8478 RVA: 0x000B4680 File Offset: 0x000B2880
	public void RefreshCells()
	{
		this.placementCells = new int[this.Def.PlacementOffsets.Length];
		int num = Grid.PosToCell(this);
		if (num < 0)
		{
			this.extents.x = -1;
			this.extents.y = -1;
			this.extents.width = this.Def.WidthInCells;
			this.extents.height = this.Def.HeightInCells;
			return;
		}
		Orientation orientation = this.Orientation;
		for (int i = 0; i < this.Def.PlacementOffsets.Length; i++)
		{
			CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(this.Def.PlacementOffsets[i], orientation);
			int num2 = Grid.OffsetCell(num, rotatedCellOffset);
			this.placementCells[i] = num2;
		}
		int num3 = 0;
		int num4 = 0;
		Grid.CellToXY(this.placementCells[0], out num3, out num4);
		int num5 = num3;
		int num6 = num4;
		foreach (int num7 in this.placementCells)
		{
			int num8 = 0;
			int num9 = 0;
			Grid.CellToXY(num7, out num8, out num9);
			num3 = Math.Min(num3, num8);
			num4 = Math.Min(num4, num9);
			num5 = Math.Max(num5, num8);
			num6 = Math.Max(num6, num9);
		}
		this.extents.x = num3;
		this.extents.y = num4;
		this.extents.width = num5 - num3 + 1;
		this.extents.height = num6 - num4 + 1;
	}

	// Token: 0x0600211F RID: 8479 RVA: 0x000B47F0 File Offset: 0x000B29F0
	[OnDeserialized]
	internal void OnDeserialized()
	{
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		if (component != null && component.Temperature == 0f)
		{
			if (component.Element == null)
			{
				DeserializeWarnings.Instance.PrimaryElementHasNoElement.Warn(base.name + " primary element has no element.", base.gameObject);
				return;
			}
			if (!(this is BuildingUnderConstruction))
			{
				DeserializeWarnings.Instance.BuildingTemeperatureIsZeroKelvin.Warn(base.name + " is at zero degrees kelvin. Resetting temperature.", null);
				component.Temperature = component.Element.defaultValues.temperature;
			}
		}
	}

	// Token: 0x06002120 RID: 8480 RVA: 0x000B4886 File Offset: 0x000B2A86
	public void SetDescription(string desc)
	{
		this.description = desc;
	}

	// Token: 0x17000190 RID: 400
	// (get) Token: 0x06002121 RID: 8481 RVA: 0x000B488F File Offset: 0x000B2A8F
	public string Desc
	{
		get
		{
			if (this.Def.AvailableFacades.Count > 0 && !this.description.IsNullOrWhiteSpace())
			{
				return this.description;
			}
			return this.Def.Desc;
		}
	}

	// Token: 0x06002122 RID: 8482 RVA: 0x000B48C4 File Offset: 0x000B2AC4
	protected override void OnSpawn()
	{
		if (this.Def == null)
		{
			global::Debug.LogError("Missing building definition on object " + base.name);
		}
		KSelectable component = base.GetComponent<KSelectable>();
		if (component != null)
		{
			component.SetName(this.Def.Name);
			component.SetStatusIndicatorOffset(new Vector3(0f, -0.35f, 0f));
		}
		Prioritizable component2 = base.GetComponent<Prioritizable>();
		if (component2 != null)
		{
			component2.iconOffset.y = 0.3f;
		}
		if (base.GetComponent<KPrefabID>().HasTag(RoomConstraints.ConstraintTags.IndustrialMachinery))
		{
			this.scenePartitionerEntry = GameScenePartitioner.Instance.Add(base.name, base.gameObject, this.GetExtents(), GameScenePartitioner.Instance.industrialBuildings, null);
		}
		if (this.Def.Deprecated && base.GetComponent<KSelectable>() != null)
		{
			KSelectable component3 = base.GetComponent<KSelectable>();
			Building.deprecatedBuildingStatusItem = new StatusItem("BUILDING_DEPRECATED", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			component3.AddStatusItem(Building.deprecatedBuildingStatusItem, null);
		}
	}

	// Token: 0x06002123 RID: 8483 RVA: 0x000B49E3 File Offset: 0x000B2BE3
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.scenePartitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x06002124 RID: 8484 RVA: 0x000B49FB File Offset: 0x000B2BFB
	public virtual void UpdatePosition()
	{
		this.RefreshCells();
		GameScenePartitioner.Instance.UpdatePosition(this.scenePartitionerEntry, this.GetExtents());
	}

	// Token: 0x06002125 RID: 8485 RVA: 0x000B4A1C File Offset: 0x000B2C1C
	protected void RegisterBlockTileRenderer()
	{
		if (this.Def.BlockTileAtlas != null)
		{
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			if (component != null)
			{
				SimHashes visualizationElementID = this.GetVisualizationElementID(component);
				int num = Grid.PosToCell(base.transform.GetPosition());
				Constructable component2 = base.GetComponent<Constructable>();
				bool flag = component2 != null && component2.IsReplacementTile;
				World.Instance.blockTileRenderer.AddBlock(base.gameObject.layer, this.Def, flag, visualizationElementID, num);
			}
		}
	}

	// Token: 0x06002126 RID: 8486 RVA: 0x000B4AA4 File Offset: 0x000B2CA4
	public CellOffset GetRotatedOffset(CellOffset offset)
	{
		if (!(this.rotatable != null))
		{
			return offset;
		}
		return this.rotatable.GetRotatedCellOffset(offset);
	}

	// Token: 0x06002127 RID: 8487 RVA: 0x000B4AC2 File Offset: 0x000B2CC2
	private int GetBottomLeftCell()
	{
		return Grid.PosToCell(base.transform.GetPosition());
	}

	// Token: 0x06002128 RID: 8488 RVA: 0x000B4AD4 File Offset: 0x000B2CD4
	public int GetPowerInputCell()
	{
		CellOffset rotatedOffset = this.GetRotatedOffset(this.Def.PowerInputOffset);
		return Grid.OffsetCell(this.GetBottomLeftCell(), rotatedOffset);
	}

	// Token: 0x06002129 RID: 8489 RVA: 0x000B4B00 File Offset: 0x000B2D00
	public int GetPowerOutputCell()
	{
		CellOffset rotatedOffset = this.GetRotatedOffset(this.Def.PowerOutputOffset);
		return Grid.OffsetCell(this.GetBottomLeftCell(), rotatedOffset);
	}

	// Token: 0x0600212A RID: 8490 RVA: 0x000B4B2C File Offset: 0x000B2D2C
	public int GetUtilityInputCell()
	{
		CellOffset rotatedOffset = this.GetRotatedOffset(this.Def.UtilityInputOffset);
		return Grid.OffsetCell(this.GetBottomLeftCell(), rotatedOffset);
	}

	// Token: 0x0600212B RID: 8491 RVA: 0x000B4B58 File Offset: 0x000B2D58
	public int GetHighEnergyParticleInputCell()
	{
		CellOffset rotatedOffset = this.GetRotatedOffset(this.Def.HighEnergyParticleInputOffset);
		return Grid.OffsetCell(this.GetBottomLeftCell(), rotatedOffset);
	}

	// Token: 0x0600212C RID: 8492 RVA: 0x000B4B84 File Offset: 0x000B2D84
	public int GetHighEnergyParticleOutputCell()
	{
		CellOffset rotatedOffset = this.GetRotatedOffset(this.Def.HighEnergyParticleOutputOffset);
		return Grid.OffsetCell(this.GetBottomLeftCell(), rotatedOffset);
	}

	// Token: 0x0600212D RID: 8493 RVA: 0x000B4BB0 File Offset: 0x000B2DB0
	public int GetUtilityOutputCell()
	{
		CellOffset rotatedOffset = this.GetRotatedOffset(this.Def.UtilityOutputOffset);
		return Grid.OffsetCell(this.GetBottomLeftCell(), rotatedOffset);
	}

	// Token: 0x0600212E RID: 8494 RVA: 0x000B4BDB File Offset: 0x000B2DDB
	public CellOffset GetUtilityInputOffset()
	{
		return this.GetRotatedOffset(this.Def.UtilityInputOffset);
	}

	// Token: 0x0600212F RID: 8495 RVA: 0x000B4BEE File Offset: 0x000B2DEE
	public CellOffset GetUtilityOutputOffset()
	{
		return this.GetRotatedOffset(this.Def.UtilityOutputOffset);
	}

	// Token: 0x06002130 RID: 8496 RVA: 0x000B4C01 File Offset: 0x000B2E01
	public CellOffset GetHighEnergyParticleInputOffset()
	{
		return this.GetRotatedOffset(this.Def.HighEnergyParticleInputOffset);
	}

	// Token: 0x06002131 RID: 8497 RVA: 0x000B4C14 File Offset: 0x000B2E14
	public CellOffset GetHighEnergyParticleOutputOffset()
	{
		return this.GetRotatedOffset(this.Def.HighEnergyParticleOutputOffset);
	}

	// Token: 0x06002132 RID: 8498 RVA: 0x000B4C28 File Offset: 0x000B2E28
	protected void UnregisterBlockTileRenderer()
	{
		if (this.Def.BlockTileAtlas != null)
		{
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			if (component != null)
			{
				SimHashes visualizationElementID = this.GetVisualizationElementID(component);
				int num = Grid.PosToCell(base.transform.GetPosition());
				Constructable component2 = base.GetComponent<Constructable>();
				bool flag = component2 != null && component2.IsReplacementTile;
				World.Instance.blockTileRenderer.RemoveBlock(this.Def, flag, visualizationElementID, num);
			}
		}
	}

	// Token: 0x06002133 RID: 8499 RVA: 0x000B4CA5 File Offset: 0x000B2EA5
	private SimHashes GetVisualizationElementID(PrimaryElement pe)
	{
		if (!(this is BuildingComplete))
		{
			return SimHashes.Void;
		}
		return pe.ElementID;
	}

	// Token: 0x06002134 RID: 8500 RVA: 0x000B4CBB File Offset: 0x000B2EBB
	public void RunOnArea(Action<int> callback)
	{
		this.Def.RunOnArea(Grid.PosToCell(this), this.Orientation, callback);
	}

	// Token: 0x06002135 RID: 8501 RVA: 0x000B4CD8 File Offset: 0x000B2ED8
	public List<Descriptor> RequirementDescriptors(BuildingDef def)
	{
		List<Descriptor> list = new List<Descriptor>();
		BuildingComplete component = def.BuildingComplete.GetComponent<BuildingComplete>();
		if (def.RequiresPowerInput)
		{
			float wattsNeededWhenActive = component.GetComponent<IEnergyConsumer>().WattsNeededWhenActive;
			if (wattsNeededWhenActive > 0f)
			{
				string formattedWattage = GameUtil.GetFormattedWattage(wattsNeededWhenActive, GameUtil.WattageFormatterUnit.Automatic, true);
				Descriptor descriptor = new Descriptor(string.Format(UI.BUILDINGEFFECTS.REQUIRESPOWER, formattedWattage), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESPOWER, formattedWattage), Descriptor.DescriptorType.Requirement, false);
				list.Add(descriptor);
			}
		}
		if (def.InputConduitType == ConduitType.Liquid)
		{
			Descriptor descriptor2 = default(Descriptor);
			descriptor2.SetupDescriptor(UI.BUILDINGEFFECTS.REQUIRESLIQUIDINPUT, UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESLIQUIDINPUT, Descriptor.DescriptorType.Requirement);
			list.Add(descriptor2);
		}
		else if (def.InputConduitType == ConduitType.Gas)
		{
			Descriptor descriptor3 = default(Descriptor);
			descriptor3.SetupDescriptor(UI.BUILDINGEFFECTS.REQUIRESGASINPUT, UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESGASINPUT, Descriptor.DescriptorType.Requirement);
			list.Add(descriptor3);
		}
		if (def.OutputConduitType == ConduitType.Liquid)
		{
			Descriptor descriptor4 = default(Descriptor);
			descriptor4.SetupDescriptor(UI.BUILDINGEFFECTS.REQUIRESLIQUIDOUTPUT, UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESLIQUIDOUTPUT, Descriptor.DescriptorType.Requirement);
			list.Add(descriptor4);
		}
		else if (def.OutputConduitType == ConduitType.Gas)
		{
			Descriptor descriptor5 = default(Descriptor);
			descriptor5.SetupDescriptor(UI.BUILDINGEFFECTS.REQUIRESGASOUTPUT, UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESGASOUTPUT, Descriptor.DescriptorType.Requirement);
			list.Add(descriptor5);
		}
		if (component.isManuallyOperated)
		{
			Descriptor descriptor6 = default(Descriptor);
			descriptor6.SetupDescriptor(UI.BUILDINGEFFECTS.REQUIRESMANUALOPERATION, UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESMANUALOPERATION, Descriptor.DescriptorType.Requirement);
			list.Add(descriptor6);
		}
		if (component.isArtable)
		{
			Descriptor descriptor7 = default(Descriptor);
			descriptor7.SetupDescriptor(UI.BUILDINGEFFECTS.REQUIRESCREATIVITY, UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESCREATIVITY, Descriptor.DescriptorType.Requirement);
			list.Add(descriptor7);
		}
		if (def.BuildingUnderConstruction != null)
		{
			Constructable component2 = def.BuildingUnderConstruction.GetComponent<Constructable>();
			if (component2 != null && component2.requiredSkillPerk != HashedString.Invalid)
			{
				StringBuilder stringBuilder = new StringBuilder();
				List<Skill> skillsWithPerk = Db.Get().Skills.GetSkillsWithPerk(component2.requiredSkillPerk);
				for (int i = 0; i < skillsWithPerk.Count; i++)
				{
					Skill skill = skillsWithPerk[i];
					stringBuilder.Append(skill.Name);
					if (i != skillsWithPerk.Count - 1)
					{
						stringBuilder.Append(", ");
					}
				}
				string text = stringBuilder.ToString();
				list.Add(new Descriptor(UI.BUILD_REQUIRES_SKILL.Replace("{Skill}", text), UI.BUILD_REQUIRES_SKILL_TOOLTIP.Replace("{Skill}", text), Descriptor.DescriptorType.Requirement, false));
			}
		}
		return list;
	}

	// Token: 0x06002136 RID: 8502 RVA: 0x000B4F78 File Offset: 0x000B3178
	public List<Descriptor> EffectDescriptors(BuildingDef def)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (def.EffectDescription != null)
		{
			list.AddRange(def.EffectDescription);
		}
		if (def.GeneratorWattageRating > 0f && base.GetComponent<Battery>() == null)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ENERGYGENERATED, GameUtil.GetFormattedWattage(def.GeneratorWattageRating, GameUtil.WattageFormatterUnit.Automatic, true)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ENERGYGENERATED, GameUtil.GetFormattedWattage(def.GeneratorWattageRating, GameUtil.WattageFormatterUnit.Automatic, true)), Descriptor.DescriptorType.Effect);
			list.Add(descriptor);
		}
		if (def.ExhaustKilowattsWhenActive > 0f || def.SelfHeatKilowattsWhenActive > 0f)
		{
			Descriptor descriptor2 = default(Descriptor);
			string formattedHeatEnergy = GameUtil.GetFormattedHeatEnergy((def.ExhaustKilowattsWhenActive + def.SelfHeatKilowattsWhenActive) * 1000f, GameUtil.HeatEnergyFormatterUnit.Automatic);
			descriptor2.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.HEATGENERATED, formattedHeatEnergy), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.HEATGENERATED, formattedHeatEnergy), Descriptor.DescriptorType.Effect);
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x06002137 RID: 8503 RVA: 0x000B5078 File Offset: 0x000B3278
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in this.RequirementDescriptors(this.Def))
		{
			list.Add(descriptor);
		}
		foreach (Descriptor descriptor2 in this.EffectDescriptors(this.Def))
		{
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x06002138 RID: 8504 RVA: 0x000B5120 File Offset: 0x000B3320
	public override Vector2 PosMin()
	{
		Extents extents = this.GetExtents();
		return new Vector2((float)extents.x, (float)extents.y);
	}

	// Token: 0x06002139 RID: 8505 RVA: 0x000B5148 File Offset: 0x000B3348
	public override Vector2 PosMax()
	{
		Extents extents = this.GetExtents();
		return new Vector2((float)(extents.x + extents.width), (float)(extents.y + extents.height));
	}

	// Token: 0x0600213A RID: 8506 RVA: 0x000B517D File Offset: 0x000B337D
	public CellOffset[] GetOffsets()
	{
		return OffsetGroups.Use;
	}

	// Token: 0x0600213B RID: 8507 RVA: 0x000B5184 File Offset: 0x000B3384
	public int GetCell()
	{
		return Grid.PosToCell(this);
	}

	// Token: 0x04001309 RID: 4873
	public BuildingDef Def;

	// Token: 0x0400130A RID: 4874
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x0400130B RID: 4875
	[MyCmpAdd]
	private StateMachineController stateMachineController;

	// Token: 0x0400130C RID: 4876
	private int[] placementCells;

	// Token: 0x0400130D RID: 4877
	private Extents extents;

	// Token: 0x0400130E RID: 4878
	private static StatusItem deprecatedBuildingStatusItem;

	// Token: 0x0400130F RID: 4879
	private string description;

	// Token: 0x04001310 RID: 4880
	private HandleVector<int>.Handle scenePartitionerEntry;
}
