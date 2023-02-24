using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200025E RID: 606
public class KeepsakeConfig : IMultiEntityConfig
{
	// Token: 0x06000C03 RID: 3075 RVA: 0x00043D3C File Offset: 0x00041F3C
	public List<GameObject> CreatePrefabs()
	{
		List<GameObject> list = new List<GameObject>();
		list.Add(KeepsakeConfig.CreateKeepsake("MegaBrain", UI.KEEPSAKES.MEGA_BRAIN.NAME, UI.KEEPSAKES.MEGA_BRAIN.DESCRIPTION, "keepsake_mega_brain_kanim", "idle", "ui", DlcManager.AVAILABLE_ALL_VERSIONS, null, SimHashes.Creature));
		list.Add(KeepsakeConfig.CreateKeepsake("CritterManipulator", UI.KEEPSAKES.CRITTER_MANIPULATOR.NAME, UI.KEEPSAKES.CRITTER_MANIPULATOR.DESCRIPTION, "keepsake_critter_manipulator_kanim", "idle", "ui", DlcManager.AVAILABLE_ALL_VERSIONS, null, SimHashes.Creature));
		list.Add(KeepsakeConfig.CreateKeepsake("LonelyMinion", UI.KEEPSAKES.LONELY_MINION.NAME, UI.KEEPSAKES.LONELY_MINION.DESCRIPTION, "keepsake_lonelyminion_kanim", "idle", "ui", DlcManager.AVAILABLE_ALL_VERSIONS, null, SimHashes.Creature));
		list.RemoveAll((GameObject x) => x == null);
		return list;
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x00043E30 File Offset: 0x00042030
	public static GameObject CreateKeepsake(string id, string name, string desc, string animFile, string initial_anim = "idle", string ui_anim = "ui", string[] dlcIDs = null, KeepsakeConfig.PostInitFn postInitFn = null, SimHashes element = SimHashes.Creature)
	{
		if (dlcIDs == null)
		{
			dlcIDs = DlcManager.AVAILABLE_ALL_VERSIONS;
		}
		if (!DlcManager.IsDlcListValidForCurrentContent(dlcIDs))
		{
			return null;
		}
		GameObject gameObject = EntityTemplates.CreateLooseEntity("keepsake_" + id.ToLower(), name, desc, 25f, true, Assets.GetAnim(animFile), initial_anim, Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.RECTANGLE, 1f, 1f, true, SORTORDER.KEEPSAKES, element, new List<Tag> { GameTags.MiscPickupable });
		gameObject.AddOrGet<OccupyArea>().OccupiedCellsOffsets = EntityTemplates.GenerateOffsets(1, 1);
		DecorProvider decorProvider = gameObject.AddOrGet<DecorProvider>();
		decorProvider.SetValues(DECOR.BONUS.TIER1);
		decorProvider.overrideName = gameObject.name;
		gameObject.AddOrGet<KSelectable>();
		gameObject.GetComponent<KBatchedAnimController>().initialMode = KAnim.PlayMode.Loop;
		if (postInitFn != null)
		{
			postInitFn(gameObject);
		}
		KPrefabID component = gameObject.GetComponent<KPrefabID>();
		component.AddTag(GameTags.PedestalDisplayable, false);
		component.AddTag(GameTags.Keepsake, false);
		return gameObject;
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x00043F0E File Offset: 0x0004210E
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x00043F10 File Offset: 0x00042110
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x02000EE9 RID: 3817
	// (Invoke) Token: 0x06006D66 RID: 28006
	public delegate void PostInitFn(GameObject gameObject);
}
