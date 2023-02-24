using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000303 RID: 771
public class VendingMachineConfig : IEntityConfig
{
	// Token: 0x06000F5E RID: 3934 RVA: 0x000539C3 File Offset: 0x00051BC3
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x000539CC File Offset: 0x00051BCC
	public GameObject CreatePrefab()
	{
		string text = "VendingMachine";
		string text2 = STRINGS.BUILDINGS.PREFABS.VENDINGMACHINE.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.VENDINGMACHINE.DESC;
		float num = 100f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("vendingmachine_kanim"), "on", Grid.SceneLayer.Building, 2, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		Workable workable = gameObject.AddOrGet<Workable>();
		workable.synchronizeAnims = false;
		workable.resetProgressOnStop = true;
		SetLocker setLocker = gameObject.AddOrGet<SetLocker>();
		setLocker.machineSound = "VendingMachine_LP";
		setLocker.overrideAnim = "anim_break_kanim";
		setLocker.dropOffset = new Vector2I(1, 1);
		LoreBearerUtil.AddLoreTo(gameObject);
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x00053AC0 File Offset: 0x00051CC0
	public void OnPrefabInit(GameObject inst)
	{
		SetLocker component = inst.GetComponent<SetLocker>();
		component.possible_contents_ids = new string[][] { new string[] { "FieldRation" } };
		component.ChooseContents();
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x00053AF7 File Offset: 0x00051CF7
	public void OnSpawn(GameObject inst)
	{
	}
}
