using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000302 RID: 770
public class SetLockerConfig : IEntityConfig
{
	// Token: 0x06000F59 RID: 3929 RVA: 0x00053863 File Offset: 0x00051A63
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x0005386C File Offset: 0x00051A6C
	public GameObject CreatePrefab()
	{
		string text = "SetLocker";
		string text2 = STRINGS.BUILDINGS.PREFABS.SETLOCKER.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.SETLOCKER.DESC;
		float num = 100f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("setpiece_locker_kanim"), "on", Grid.SceneLayer.Building, 1, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		Workable workable = gameObject.AddOrGet<Workable>();
		workable.synchronizeAnims = false;
		workable.resetProgressOnStop = true;
		SetLocker setLocker = gameObject.AddOrGet<SetLocker>();
		setLocker.overrideAnim = "anim_interacts_clothingfactory_kanim";
		setLocker.dropOffset = new Vector2I(0, 1);
		setLocker.numDataBanks = new int[] { 1, 4 };
		LoreBearerUtil.AddLoreTo(gameObject);
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x00053960 File Offset: 0x00051B60
	public void OnPrefabInit(GameObject inst)
	{
		SetLocker component = inst.GetComponent<SetLocker>();
		component.possible_contents_ids = new string[][]
		{
			new string[] { "Warm_Vest" },
			new string[] { "Cool_Vest" },
			new string[] { "Funky_Vest" }
		};
		component.ChooseContents();
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x000539B9 File Offset: 0x00051BB9
	public void OnSpawn(GameObject inst)
	{
	}
}
