using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000349 RID: 841
public class WarpReceiverConfig : IEntityConfig
{
	// Token: 0x060010F0 RID: 4336 RVA: 0x0005BAC7 File Offset: 0x00059CC7
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x0005BAD0 File Offset: 0x00059CD0
	public GameObject CreatePrefab()
	{
		string id = WarpReceiverConfig.ID;
		string text = STRINGS.BUILDINGS.PREFABS.WARPRECEIVER.NAME;
		string text2 = STRINGS.BUILDINGS.PREFABS.WARPRECEIVER.DESC;
		float num = 2000f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, text, text2, num, Assets.GetAnim("warp_portal_receiver_kanim"), "idle", Grid.SceneLayer.Building, 3, 3, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.AddTag(GameTags.NotRoomAssignable);
		gameObject.AddTag(GameTags.WarpTech);
		gameObject.AddTag(GameTags.Gravitas);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Operational>();
		gameObject.AddOrGet<Notifier>();
		gameObject.AddOrGet<WarpReceiver>();
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGet<Prioritizable>();
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("notes_AI", UI.USERMENUACTIONS.READLORE.SEARCH_TELEPORTER_RECEIVER));
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.sceneLayer = Grid.SceneLayer.BuildingBack;
		kbatchedAnimController.fgLayer = Grid.SceneLayer.BuildingFront;
		return gameObject;
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x0005BBC0 File Offset: 0x00059DC0
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<WarpReceiver>().workLayer = Grid.SceneLayer.Building;
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		inst.GetComponent<Deconstructable>();
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x0005BBEB File Offset: 0x00059DEB
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000929 RID: 2345
	public static string ID = "WarpReceiver";
}
