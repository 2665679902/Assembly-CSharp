using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x0200025D RID: 605
public class HeatCubeConfig : IEntityConfig
{
	// Token: 0x06000BFE RID: 3070 RVA: 0x00043CB7 File Offset: 0x00041EB7
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x00043CC0 File Offset: 0x00041EC0
	public GameObject CreatePrefab()
	{
		return EntityTemplates.CreateLooseEntity("HeatCube", "Heat Cube", "A cube that holds heat.", 1000f, true, Assets.GetAnim("artifacts_kanim"), "idle_tallstone", Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.RECTANGLE, 1f, 1f, true, SORTORDER.BUILDINGELEMENTS, SimHashes.Diamond, new List<Tag>
		{
			GameTags.MiscPickupable,
			GameTags.IndustrialIngredient
		});
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x00043D2E File Offset: 0x00041F2E
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x00043D30 File Offset: 0x00041F30
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400070C RID: 1804
	public const string ID = "HeatCube";
}
