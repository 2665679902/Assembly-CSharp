using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000133 RID: 307
public class BasicFabricConfig : IEntityConfig
{
	// Token: 0x060005E0 RID: 1504 RVA: 0x000266B6 File Offset: 0x000248B6
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x000266C0 File Offset: 0x000248C0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(BasicFabricConfig.ID, ITEMS.INDUSTRIAL_PRODUCTS.BASIC_FABRIC.NAME, ITEMS.INDUSTRIAL_PRODUCTS.BASIC_FABRIC.DESC, 1f, true, Assets.GetAnim("swampreedwool_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.45f, true, SORTORDER.BUILDINGELEMENTS + BasicFabricTuning.SORTORDER, SimHashes.Creature, new List<Tag>
		{
			GameTags.IndustrialIngredient,
			GameTags.BuildingFiber
		});
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddOrGet<PrefabAttributeModifiers>().AddAttributeDescriptor(this.decorModifier);
		return gameObject;
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x00026756 File Offset: 0x00024956
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x00026758 File Offset: 0x00024958
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000403 RID: 1027
	public static string ID = "BasicFabric";

	// Token: 0x04000404 RID: 1028
	private AttributeModifier decorModifier = new AttributeModifier("Decor", 0.1f, ITEMS.INDUSTRIAL_PRODUCTS.BASIC_FABRIC.NAME, true, false, true);
}
