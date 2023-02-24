using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000023 RID: 35
public abstract class BaseWireConfig : IBuildingConfig
{
	// Token: 0x0600009D RID: 157
	public abstract override BuildingDef CreateBuildingDef();

	// Token: 0x0600009E RID: 158 RVA: 0x00005DF0 File Offset: 0x00003FF0
	public BuildingDef CreateBuildingDef(string id, string anim, float construction_time, float[] construction_mass, float insulation, EffectorValues decor, EffectorValues noise)
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, 1, 1, anim, 10, construction_time, construction_mass, MATERIALS.ALL_METALS, 1600f, BuildLocationRule.Anywhere, decor, noise, 0.2f);
		buildingDef.ThermalConductivity = insulation;
		BuildingDef buildingDef2 = buildingDef;
		buildingDef2.Floodable = false;
		BuildingDef buildingDef3 = buildingDef2;
		buildingDef3.Overheatable = false;
		buildingDef3.Entombable = false;
		buildingDef3.ViewMode = OverlayModes.Power.ID;
		buildingDef3.ObjectLayer = ObjectLayer.Wire;
		buildingDef3.TileLayer = ObjectLayer.WireTile;
		buildingDef3.ReplacementLayer = ObjectLayer.ReplacementWire;
		buildingDef3.AudioCategory = "Metal";
		buildingDef3.AudioSize = "small";
		buildingDef3.BaseTimeUntilRepair = -1f;
		buildingDef3.SceneLayer = Grid.SceneLayer.Wires;
		buildingDef3.isKAnimTile = true;
		buildingDef3.isUtility = true;
		buildingDef3.DragBuild = true;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.WireIDs, id);
		return buildingDef3;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00005EAA File Offset: 0x000040AA
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<Wire>();
		KAnimGraphTileVisualizer kanimGraphTileVisualizer = go.AddOrGet<KAnimGraphTileVisualizer>();
		kanimGraphTileVisualizer.isPhysicalBuilding = true;
		kanimGraphTileVisualizer.connectionSource = KAnimGraphTileVisualizer.ConnectionSource.Electrical;
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00005EE1 File Offset: 0x000040E1
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.GetComponent<Constructable>().isDiggingRequired = false;
		KAnimGraphTileVisualizer kanimGraphTileVisualizer = go.AddOrGet<KAnimGraphTileVisualizer>();
		kanimGraphTileVisualizer.isPhysicalBuilding = false;
		kanimGraphTileVisualizer.connectionSource = KAnimGraphTileVisualizer.ConnectionSource.Electrical;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00005F0C File Offset: 0x0000410C
	protected void DoPostConfigureComplete(Wire.WattageRating rating, GameObject go)
	{
		go.GetComponent<Wire>().MaxWattageRating = rating;
		float maxWattageAsFloat = Wire.GetMaxWattageAsFloat(rating);
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.MAX_WATTAGE, GameUtil.GetFormattedWattage(maxWattageAsFloat, GameUtil.WattageFormatterUnit.Automatic, true)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.MAX_WATTAGE, Array.Empty<object>()), Descriptor.DescriptorType.Effect);
		BuildingDef def = go.GetComponent<Building>().Def;
		if (def.EffectDescription == null)
		{
			def.EffectDescription = new List<Descriptor>();
		}
		def.EffectDescription.Add(descriptor);
	}
}
