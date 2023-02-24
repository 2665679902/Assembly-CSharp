using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002D4 RID: 724
public class PropReceptionDeskConfig : IEntityConfig
{
	// Token: 0x06000E57 RID: 3671 RVA: 0x0004DCBA File Offset: 0x0004BEBA
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x0004DCC4 File Offset: 0x0004BEC4
	public GameObject CreatePrefab()
	{
		string text = "PropReceptionDesk";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPRECEPTIONDESK.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPRECEPTIONDESK.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_reception_kanim"), "off", Grid.SceneLayer.Building, 5, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("email_pens", UI.USERMENUACTIONS.READLORE.SEARCH_ELLIESDESK));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E59 RID: 3673 RVA: 0x0004DD74 File Offset: 0x0004BF74
	public void OnPrefabInit(GameObject inst)
	{
		OccupyArea component = inst.GetComponent<OccupyArea>();
		component.objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		int num = Grid.PosToCell(inst);
		foreach (CellOffset cellOffset in component.OccupiedCellsOffsets)
		{
			Grid.GravitasFacility[Grid.OffsetCell(num, cellOffset)] = true;
		}
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x0004DDCC File Offset: 0x0004BFCC
	public void OnSpawn(GameObject inst)
	{
	}
}
