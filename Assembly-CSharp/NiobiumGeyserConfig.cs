using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200014D RID: 333
public class NiobiumGeyserConfig : IEntityConfig
{
	// Token: 0x06000668 RID: 1640 RVA: 0x00029A90 File Offset: 0x00027C90
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x00029A98 File Offset: 0x00027C98
	public GameObject CreatePrefab()
	{
		GeyserConfigurator.GeyserType geyserType = new GeyserConfigurator.GeyserType("molten_niobium", SimHashes.MoltenNiobium, GeyserConfigurator.GeyserShape.Molten, 3500f, 800f, 1600f, 150f, 6000f, 12000f, 0.005f, 0.01f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, "");
		GameObject gameObject = GeyserGenericConfig.CreateGeyser("NiobiumGeyser", "geyser_molten_niobium_kanim", 3, 3, CREATURES.SPECIES.GEYSER.MOLTEN_NIOBIUM.NAME, CREATURES.SPECIES.GEYSER.MOLTEN_NIOBIUM.DESC, geyserType.idHash, geyserType.geyserTemperature);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.DeprecatedContent, false);
		return gameObject;
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x00029B3E File Offset: 0x00027D3E
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x00029B40 File Offset: 0x00027D40
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400046A RID: 1130
	public const string ID = "NiobiumGeyser";
}
