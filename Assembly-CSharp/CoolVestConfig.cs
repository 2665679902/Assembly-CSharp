using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class CoolVestConfig : IEquipmentConfig
{
	// Token: 0x06000214 RID: 532 RVA: 0x0000F278 File Offset: 0x0000D478
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000215 RID: 533 RVA: 0x0000F280 File Offset: 0x0000D480
	public EquipmentDef CreateEquipmentDef()
	{
		new Dictionary<string, float>().Add("BasicFabric", (float)TUNING.EQUIPMENT.VESTS.COOL_VEST_MASS);
		ClothingWearer.ClothingInfo clothingInfo = ClothingWearer.ClothingInfo.COOL_CLOTHING;
		List<AttributeModifier> list = new List<AttributeModifier>();
		EquipmentDef equipmentDef = EquipmentTemplates.CreateEquipmentDef("Cool_Vest", TUNING.EQUIPMENT.CLOTHING.SLOT, SimHashes.Carbon, (float)TUNING.EQUIPMENT.VESTS.COOL_VEST_MASS, TUNING.EQUIPMENT.VESTS.COOL_VEST_ICON0, TUNING.EQUIPMENT.VESTS.SNAPON0, TUNING.EQUIPMENT.VESTS.COOL_VEST_ANIM0, 4, list, TUNING.EQUIPMENT.VESTS.SNAPON1, true, EntityTemplates.CollisionShape.RECTANGLE, 0.75f, 0.4f, null, null);
		Descriptor descriptor = new Descriptor(string.Format("{0}: {1}", DUPLICANTS.ATTRIBUTES.THERMALCONDUCTIVITYBARRIER.NAME, GameUtil.GetFormattedDistance(ClothingWearer.ClothingInfo.COOL_CLOTHING.conductivityMod)), string.Format("{0}: {1}", DUPLICANTS.ATTRIBUTES.THERMALCONDUCTIVITYBARRIER.NAME, GameUtil.GetFormattedDistance(ClothingWearer.ClothingInfo.COOL_CLOTHING.conductivityMod)), Descriptor.DescriptorType.Effect, false);
		Descriptor descriptor2 = new Descriptor(string.Format("{0}: {1}", DUPLICANTS.ATTRIBUTES.DECOR.NAME, ClothingWearer.ClothingInfo.COOL_CLOTHING.decorMod), string.Format("{0}: {1}", DUPLICANTS.ATTRIBUTES.DECOR.NAME, ClothingWearer.ClothingInfo.COOL_CLOTHING.decorMod), Descriptor.DescriptorType.Effect, false);
		equipmentDef.additionalDescriptors.Add(descriptor);
		equipmentDef.additionalDescriptors.Add(descriptor2);
		equipmentDef.OnEquipCallBack = delegate(Equippable eq)
		{
			CoolVestConfig.OnEquipVest(eq, clothingInfo);
		};
		equipmentDef.OnUnequipCallBack = new Action<Equippable>(CoolVestConfig.OnUnequipVest);
		equipmentDef.RecipeDescription = STRINGS.EQUIPMENT.PREFABS.COOL_VEST.RECIPE_DESC;
		return equipmentDef;
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
	public static void OnEquipVest(Equippable eq, ClothingWearer.ClothingInfo clothingInfo)
	{
		if (eq == null || eq.assignee == null)
		{
			return;
		}
		Ownables soleOwner = eq.assignee.GetSoleOwner();
		if (soleOwner == null)
		{
			return;
		}
		ClothingWearer component = (soleOwner.GetComponent<MinionAssignablesProxy>().target as KMonoBehaviour).GetComponent<ClothingWearer>();
		if (component != null)
		{
			component.ChangeClothes(clothingInfo);
			return;
		}
		global::Debug.LogWarning("Clothing item cannot be equipped to assignee because they lack ClothingWearer component");
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000F438 File Offset: 0x0000D638
	public static void OnUnequipVest(Equippable eq)
	{
		if (eq != null && eq.assignee != null)
		{
			Ownables soleOwner = eq.assignee.GetSoleOwner();
			if (soleOwner != null)
			{
				ClothingWearer component = soleOwner.GetComponent<ClothingWearer>();
				if (component != null)
				{
					component.ChangeToDefaultClothes();
				}
			}
		}
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000F484 File Offset: 0x0000D684
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

	// Token: 0x06000219 RID: 537 RVA: 0x0000F4CD File Offset: 0x0000D6CD
	public void DoPostConfigure(GameObject go)
	{
		CoolVestConfig.SetupVest(go);
		go.GetComponent<KPrefabID>().AddTag(GameTags.PedestalDisplayable, false);
	}

	// Token: 0x04000128 RID: 296
	public const string ID = "Cool_Vest";

	// Token: 0x04000129 RID: 297
	public static ComplexRecipe recipe;
}
