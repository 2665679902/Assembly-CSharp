using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020001CB RID: 459
public class DreamJournalConfig : IEntityConfig
{
	// Token: 0x06000904 RID: 2308 RVA: 0x0003520B File Offset: 0x0003340B
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00035212 File Offset: 0x00033412
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00035214 File Offset: 0x00033414
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00035218 File Offset: 0x00033418
	public GameObject CreatePrefab()
	{
		KAnimFile anim = Assets.GetAnim("dream_journal_kanim");
		GameObject gameObject = EntityTemplates.CreateLooseEntity(DreamJournalConfig.ID.Name, ITEMS.DREAMJOURNAL.NAME, ITEMS.DREAMJOURNAL.DESC, 1f, true, anim, "object", Grid.SceneLayer.BuildingFront, EntityTemplates.CollisionShape.RECTANGLE, 1f, 1f, true, 0, SimHashes.Creature, new List<Tag> { GameTags.StoryTraitResource });
		gameObject.AddOrGet<EntitySplitter>().maxStackSize = 25f;
		return gameObject;
	}

	// Token: 0x040005AE RID: 1454
	public static Tag ID = new Tag("DreamJournal");

	// Token: 0x040005AF RID: 1455
	public const float MASS = 1f;

	// Token: 0x040005B0 RID: 1456
	public const int FABRICATION_TIME_SECONDS = 300;

	// Token: 0x040005B1 RID: 1457
	private const string ANIM_FILE = "dream_journal_kanim";

	// Token: 0x040005B2 RID: 1458
	private const string INITIAL_ANIM = "object";

	// Token: 0x040005B3 RID: 1459
	public const int MAX_STACK_SIZE = 25;
}
