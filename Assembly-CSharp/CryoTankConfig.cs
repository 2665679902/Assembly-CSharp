using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000301 RID: 769
public class CryoTankConfig : IEntityConfig
{
	// Token: 0x06000F54 RID: 3924 RVA: 0x00053759 File Offset: 0x00051959
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x00053760 File Offset: 0x00051960
	public GameObject CreatePrefab()
	{
		string text = "CryoTank";
		string text2 = STRINGS.BUILDINGS.PREFABS.CRYOTANK.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.CRYOTANK.DESC;
		float num = 100f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("cryo_chamber_kanim"), "off", Grid.SceneLayer.Building, 2, 3, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.GetComponent<KAnimControllerBase>().SetFGLayer(Grid.SceneLayer.BuildingFront);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		Workable workable = gameObject.AddOrGet<Workable>();
		workable.synchronizeAnims = false;
		workable.resetProgressOnStop = true;
		CryoTank cryoTank = gameObject.AddOrGet<CryoTank>();
		cryoTank.overrideAnim = "anim_interacts_cryo_activation_kanim";
		cryoTank.dropOffset = new CellOffset(1, 0);
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("cryotank_warning", UI.USERMENUACTIONS.READLORE.SEARCH_CRYO_TANK));
		gameObject.AddOrGet<Demolishable>().allowDemolition = false;
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x00053857 File Offset: 0x00051A57
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x00053859 File Offset: 0x00051A59
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400087D RID: 2173
	public const string ID = "CryoTank";
}
