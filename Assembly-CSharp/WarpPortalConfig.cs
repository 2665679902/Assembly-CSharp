using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000348 RID: 840
public class WarpPortalConfig : IEntityConfig
{
	// Token: 0x060010EB RID: 4331 RVA: 0x0005B964 File Offset: 0x00059B64
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x0005B96C File Offset: 0x00059B6C
	public GameObject CreatePrefab()
	{
		string text = "WarpPortal";
		string text2 = STRINGS.BUILDINGS.PREFABS.WARPPORTAL.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.WARPPORTAL.DESC;
		float num = 2000f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("warp_portal_sender_kanim"), "idle", Grid.SceneLayer.Building, 3, 3, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.AddTag(GameTags.NotRoomAssignable);
		gameObject.AddTag(GameTags.WarpTech);
		gameObject.AddTag(GameTags.Gravitas);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Operational>();
		gameObject.AddOrGet<Notifier>();
		gameObject.AddOrGet<WarpPortal>();
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGet<Ownable>().tintWhenUnassigned = false;
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("notes_teleportation", UI.USERMENUACTIONS.READLORE.SEARCH_TELEPORTER_SENDER));
		gameObject.AddOrGet<Prioritizable>();
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.sceneLayer = Grid.SceneLayer.BuildingBack;
		kbatchedAnimController.fgLayer = Grid.SceneLayer.BuildingFront;
		return gameObject;
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x0005BA68 File Offset: 0x00059C68
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<WarpPortal>().workLayer = Grid.SceneLayer.Building;
		inst.GetComponent<Ownable>().slotID = Db.Get().AssignableSlots.WarpPortal.Id;
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		inst.GetComponent<Deconstructable>();
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x0005BABD File Offset: 0x00059CBD
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000928 RID: 2344
	public const string ID = "WarpPortal";
}
