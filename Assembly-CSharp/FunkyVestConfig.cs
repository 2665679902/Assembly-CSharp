using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000075 RID: 117
public class FunkyVestConfig : IEquipmentConfig
{
	// Token: 0x0600022B RID: 555 RVA: 0x0000FA35 File Offset: 0x0000DC35
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0000FA3C File Offset: 0x0000DC3C
	public EquipmentDef CreateEquipmentDef()
	{
		new Dictionary<string, float>().Add("BasicFabric", (float)TUNING.EQUIPMENT.VESTS.FUNKY_VEST_MASS);
		ClothingWearer.ClothingInfo clothingInfo = ClothingWearer.ClothingInfo.FANCY_CLOTHING;
		List<AttributeModifier> list = new List<AttributeModifier>();
		EquipmentDef equipmentDef = EquipmentTemplates.CreateEquipmentDef("Funky_Vest", TUNING.EQUIPMENT.CLOTHING.SLOT, SimHashes.Carbon, (float)TUNING.EQUIPMENT.VESTS.FUNKY_VEST_MASS, "shirt_decor01_kanim", TUNING.EQUIPMENT.VESTS.SNAPON0, "body_shirt_decor01_kanim", 4, list, TUNING.EQUIPMENT.VESTS.SNAPON1, true, EntityTemplates.CollisionShape.RECTANGLE, 0.75f, 0.4f, null, null);
		Descriptor descriptor = new Descriptor(string.Format("{0}: {1}", DUPLICANTS.ATTRIBUTES.THERMALCONDUCTIVITYBARRIER.NAME, GameUtil.GetFormattedDistance(ClothingWearer.ClothingInfo.FANCY_CLOTHING.conductivityMod)), string.Format("{0}: {1}", DUPLICANTS.ATTRIBUTES.THERMALCONDUCTIVITYBARRIER.NAME, GameUtil.GetFormattedDistance(ClothingWearer.ClothingInfo.FANCY_CLOTHING.conductivityMod)), Descriptor.DescriptorType.Effect, false);
		Descriptor descriptor2 = new Descriptor(string.Format("{0}: {1}", DUPLICANTS.ATTRIBUTES.DECOR.NAME, ClothingWearer.ClothingInfo.FANCY_CLOTHING.decorMod), string.Format("{0}: {1}", DUPLICANTS.ATTRIBUTES.DECOR.NAME, ClothingWearer.ClothingInfo.FANCY_CLOTHING.decorMod), Descriptor.DescriptorType.Effect, false);
		equipmentDef.additionalDescriptors.Add(descriptor);
		equipmentDef.additionalDescriptors.Add(descriptor2);
		equipmentDef.OnEquipCallBack = delegate(Equippable eq)
		{
			CoolVestConfig.OnEquipVest(eq, clothingInfo);
		};
		equipmentDef.OnUnequipCallBack = new Action<Equippable>(CoolVestConfig.OnUnequipVest);
		equipmentDef.RecipeDescription = STRINGS.EQUIPMENT.PREFABS.FUNKY_VEST.RECIPE_DESC;
		return equipmentDef;
	}

	// Token: 0x0600022D RID: 557 RVA: 0x0000FB8C File Offset: 0x0000DD8C
	public static void SetupVest(GameObject go)
	{
		go.GetComponent<KPrefabID>().AddTag(GameTags.Clothes, false);
		Equippable equippable = go.GetComponent<Equippable>();
		if (equippable == null)
		{
			equippable = go.AddComponent<Equippable>();
		}
		equippable.SetQuality(global::QualityLevel.Poor);
		go.GetComponent<KBatchedAnimController>().sceneLayer = Grid.SceneLayer.BuildingBack;
	}

	// Token: 0x0600022E RID: 558 RVA: 0x0000FBD5 File Offset: 0x0000DDD5
	public void DoPostConfigure(GameObject go)
	{
		FunkyVestConfig.SetupVest(go);
		go.GetComponent<KPrefabID>().AddTag(GameTags.PedestalDisplayable, false);
	}

	// Token: 0x0400012D RID: 301
	public const string ID = "Funky_Vest";

	// Token: 0x0400012E RID: 302
	public static ComplexRecipe recipe;
}
