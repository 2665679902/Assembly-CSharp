using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C7 RID: 711
public class PropGravitasFirstAidKitConfig : IEntityConfig
{
	// Token: 0x06000E15 RID: 3605 RVA: 0x0004D102 File Offset: 0x0004B302
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x0004D10C File Offset: 0x0004B30C
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasFirstAidKit";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASFIRSTAIDKIT.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASFIRSTAIDKIT.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_first_aid_kit_kanim"), "off", Grid.SceneLayer.Building, 1, 1, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		Workable workable = gameObject.AddOrGet<Workable>();
		workable.synchronizeAnims = false;
		workable.resetProgressOnStop = true;
		SetLocker setLocker = gameObject.AddOrGet<SetLocker>();
		setLocker.overrideAnim = "anim_interacts_clothingfactory_kanim";
		setLocker.dropOffset = new Vector2I(0, 1);
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E17 RID: 3607 RVA: 0x0004D1D0 File Offset: 0x0004B3D0
	public static string[][] GetLockerBaseContents()
	{
		string text = (DlcManager.FeatureRadiationEnabled() ? "BasicRadPill" : "IntermediateCure");
		return new string[][]
		{
			new string[] { "BasicCure", "BasicCure", "BasicCure" },
			new string[] { text, text }
		};
	}

	// Token: 0x06000E18 RID: 3608 RVA: 0x0004D229 File Offset: 0x0004B429
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		SetLocker component = inst.GetComponent<SetLocker>();
		component.possible_contents_ids = PropGravitasFirstAidKitConfig.GetLockerBaseContents();
		component.ChooseContents();
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x0004D256 File Offset: 0x0004B456
	public void OnSpawn(GameObject inst)
	{
	}
}
