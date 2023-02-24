using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000A3 RID: 163
public class BabyBeeConfig : IEntityConfig
{
	// Token: 0x060002CA RID: 714 RVA: 0x00016AEC File Offset: 0x00014CEC
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060002CB RID: 715 RVA: 0x00016AF4 File Offset: 0x00014CF4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = BeeConfig.CreateBee("BeeBaby", CREATURES.SPECIES.BEE.BABY.NAME, CREATURES.SPECIES.BEE.BABY.DESC, "baby_blarva_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Bee", null, true, 2f);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.Creatures.Walker, false);
		return gameObject;
	}

	// Token: 0x060002CC RID: 716 RVA: 0x00016B4E File Offset: 0x00014D4E
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060002CD RID: 717 RVA: 0x00016B50 File Offset: 0x00014D50
	public void OnSpawn(GameObject inst)
	{
		BaseBeeConfig.SetupLoopingSounds(inst);
	}

	// Token: 0x040001DB RID: 475
	public const string ID = "BeeBaby";
}
