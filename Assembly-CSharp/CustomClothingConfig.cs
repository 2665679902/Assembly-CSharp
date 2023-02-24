using System;
using System.Collections.Generic;
using Database;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000071 RID: 113
public class CustomClothingConfig : IEquipmentConfig
{
	// Token: 0x0600021B RID: 539 RVA: 0x0000F4EE File Offset: 0x0000D6EE
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000F4F8 File Offset: 0x0000D6F8
	public EquipmentDef CreateEquipmentDef()
	{
		Dictionary<string, float> dictionary = new Dictionary<string, float>();
		dictionary.Add("Funky_Vest", (float)TUNING.EQUIPMENT.VESTS.FUNKY_VEST_MASS);
		dictionary.Add("BasicFabric", 3f);
		ClothingWearer.ClothingInfo clothingInfo = ClothingWearer.ClothingInfo.CUSTOM_CLOTHING;
		List<AttributeModifier> list = new List<AttributeModifier>();
		EquipmentDef equipmentDef = EquipmentTemplates.CreateEquipmentDef("CustomClothing", TUNING.EQUIPMENT.CLOTHING.SLOT, SimHashes.Carbon, (float)TUNING.EQUIPMENT.VESTS.CUSTOM_CLOTHING_MASS, "shirt_decor01_kanim", TUNING.EQUIPMENT.VESTS.SNAPON0, "body_shirt_decor01_kanim", 4, list, TUNING.EQUIPMENT.VESTS.SNAPON1, true, EntityTemplates.CollisionShape.RECTANGLE, 0.75f, 0.4f, null, null);
		Descriptor descriptor = new Descriptor(string.Format("{0}: {1}", DUPLICANTS.ATTRIBUTES.THERMALCONDUCTIVITYBARRIER.NAME, GameUtil.GetFormattedDistance(ClothingWearer.ClothingInfo.CUSTOM_CLOTHING.conductivityMod)), string.Format("{0}: {1}", DUPLICANTS.ATTRIBUTES.THERMALCONDUCTIVITYBARRIER.NAME, GameUtil.GetFormattedDistance(ClothingWearer.ClothingInfo.CUSTOM_CLOTHING.conductivityMod)), Descriptor.DescriptorType.Effect, false);
		Descriptor descriptor2 = new Descriptor(string.Format("{0}: {1}", DUPLICANTS.ATTRIBUTES.DECOR.NAME, ClothingWearer.ClothingInfo.CUSTOM_CLOTHING.decorMod), string.Format("{0}: {1}", DUPLICANTS.ATTRIBUTES.DECOR.NAME, ClothingWearer.ClothingInfo.CUSTOM_CLOTHING.decorMod), Descriptor.DescriptorType.Effect, false);
		equipmentDef.additionalDescriptors.Add(descriptor);
		equipmentDef.additionalDescriptors.Add(descriptor2);
		equipmentDef.OnEquipCallBack = delegate(Equippable eq)
		{
			CoolVestConfig.OnEquipVest(eq, clothingInfo);
		};
		equipmentDef.OnUnequipCallBack = new Action<Equippable>(CoolVestConfig.OnUnequipVest);
		equipmentDef.RecipeDescription = STRINGS.EQUIPMENT.PREFABS.CUSTOMCLOTHING.RECIPE_DESC;
		foreach (EquippableFacadeResource equippableFacadeResource in Db.GetEquippableFacades().resources)
		{
			if (!(equippableFacadeResource.DefID != "CustomClothing"))
			{
				TagManager.Create(equippableFacadeResource.Id, EquippableFacade.GetNameOverride("CustomClothing", equippableFacadeResource.Id));
			}
		}
		return equipmentDef;
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
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

	// Token: 0x0600021E RID: 542 RVA: 0x0000F719 File Offset: 0x0000D919
	public void DoPostConfigure(GameObject go)
	{
		CustomClothingConfig.SetupVest(go);
		go.GetComponent<KPrefabID>().AddTag(GameTags.PedestalDisplayable, false);
	}

	// Token: 0x0400012A RID: 298
	public const string ID = "CustomClothing";

	// Token: 0x0400012B RID: 299
	public static ComplexRecipe recipe;
}
