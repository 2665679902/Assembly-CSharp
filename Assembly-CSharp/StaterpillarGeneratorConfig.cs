using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x02000322 RID: 802
public class StaterpillarGeneratorConfig : IBuildingConfig
{
	// Token: 0x06000FFA RID: 4090 RVA: 0x00056768 File Offset: 0x00054968
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x00056770 File Offset: 0x00054970
	public override BuildingDef CreateBuildingDef()
	{
		string id = StaterpillarGeneratorConfig.ID;
		int num = 1;
		int num2 = 2;
		string text = "egg_caterpillar_kanim";
		int num3 = 1000;
		float num4 = 10f;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] array = all_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFoundationRotatable;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.GeneratorWattageRating = 1600f;
		buildingDef.GeneratorBaseCapacity = 5000f;
		buildingDef.ExhaustKilowattsWhenActive = 2f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.OverheatTemperature = 423.15f;
		buildingDef.PermittedRotations = PermittedRotations.FlipV;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Plastic";
		buildingDef.RequiresPowerOutput = true;
		buildingDef.PowerOutputOffset = new CellOffset(0, 1);
		buildingDef.PlayConstructionSounds = false;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x00056840 File Offset: 0x00054A40
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x00056857 File Offset: 0x00054A57
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x00056859 File Offset: 0x00054A59
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x0005685B File Offset: 0x00054A5B
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<StaterpillarGenerator>().powerDistributionOrder = 9;
		go.GetComponent<Deconstructable>().SetAllowDeconstruction(false);
		go.AddOrGet<Modifiers>();
		go.AddOrGet<Effects>();
		go.GetComponent<KSelectable>().IsSelectable = false;
	}

	// Token: 0x040008BD RID: 2237
	public static readonly string ID = "StaterpillarGenerator";

	// Token: 0x040008BE RID: 2238
	private const int WIDTH = 1;

	// Token: 0x040008BF RID: 2239
	private const int HEIGHT = 2;
}
