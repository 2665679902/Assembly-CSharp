using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000263 RID: 611
public class LonelyMinionConfig : IEntityConfig
{
	// Token: 0x06000C22 RID: 3106 RVA: 0x000440B6 File Offset: 0x000422B6
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x000440C0 File Offset: 0x000422C0
	public GameObject CreatePrefab()
	{
		string text = DUPLICANTS.MODIFIERS.BASEDUPLICANT.NAME;
		GameObject gameObject = EntityTemplates.CreateEntity(LonelyMinionConfig.ID, text, true);
		gameObject.AddComponent<Accessorizer>();
		gameObject.AddOrGet<WearableAccessorizer>();
		gameObject.AddComponent<Storage>().doDiseaseTransfer = false;
		gameObject.AddComponent<StateMachineController>();
		LonelyMinion.Def def = gameObject.AddOrGetDef<LonelyMinion.Def>();
		def.Personality = Db.Get().Personalities.Get(LonelyMinionConfig.MinionName);
		def.Personality.Disabled = true;
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.defaultAnim = "idle_default";
		kbatchedAnimController.initialAnim = "idle_default";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.AnimFiles = new KAnimFile[]
		{
			Assets.GetAnim("body_comp_default_kanim"),
			Assets.GetAnim("anim_idles_default_kanim"),
			Assets.GetAnim("anim_interacts_lonely_dupe_kanim")
		};
		this.ConfigurePackageOverride(gameObject);
		SymbolOverrideController symbolOverrideController = SymbolOverrideControllerUtil.AddToPrefab(gameObject);
		symbolOverrideController.applySymbolOverridesEveryFrame = true;
		symbolOverrideController.AddSymbolOverride("snapto_cheek", Assets.GetAnim("head_swap_kanim").GetData().build.GetSymbol(string.Format("cheek_00{0}", def.Personality.headShape)), 1);
		MinionConfig.ConfigureSymbols(gameObject, true);
		return gameObject;
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x00044201 File Offset: 0x00042401
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x00044203 File Offset: 0x00042403
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x00044208 File Offset: 0x00042408
	private void ConfigurePackageOverride(GameObject go)
	{
		GameObject gameObject = new GameObject("PackageSnapPoint");
		gameObject.transform.SetParent(go.transform);
		KBatchedAnimController component = go.GetComponent<KBatchedAnimController>();
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.transform.position = Vector3.forward * -0.1f;
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("mushbar_kanim") };
		kbatchedAnimController.initialAnim = "object";
		component.SetSymbolVisiblity(LonelyMinionConfig.PARCEL_SNAPTO, false);
		KBatchedAnimTracker kbatchedAnimTracker = gameObject.AddOrGet<KBatchedAnimTracker>();
		kbatchedAnimTracker.controller = component;
		kbatchedAnimTracker.symbol = LonelyMinionConfig.PARCEL_SNAPTO;
	}

	// Token: 0x04000711 RID: 1809
	public static string ID = "LonelyMinion";

	// Token: 0x04000712 RID: 1810
	public const int VOICE_IDX = -2;

	// Token: 0x04000713 RID: 1811
	public const int STARTING_SKILL_POINTS = 3;

	// Token: 0x04000714 RID: 1812
	public const int BASE_ATTRIBUTE_LEVEL = 7;

	// Token: 0x04000715 RID: 1813
	public const int AGE_MIN = 2190;

	// Token: 0x04000716 RID: 1814
	public const int AGE_MAX = 3102;

	// Token: 0x04000717 RID: 1815
	public const float MIN_IDLE_DELAY = 20f;

	// Token: 0x04000718 RID: 1816
	public const float MAX_IDLE_DELAY = 40f;

	// Token: 0x04000719 RID: 1817
	public const string IDLE_PREFIX = "idle_blinds";

	// Token: 0x0400071A RID: 1818
	public static readonly HashedString GreetingCriteraId = "Neighbor";

	// Token: 0x0400071B RID: 1819
	public static readonly HashedString FoodCriteriaId = "FoodQuality";

	// Token: 0x0400071C RID: 1820
	public static readonly HashedString DecorCriteriaId = "Decor";

	// Token: 0x0400071D RID: 1821
	public static readonly HashedString PowerCriteriaId = "SuppliedPower";

	// Token: 0x0400071E RID: 1822
	public static readonly HashedString CHECK_MAIL = "mail_pre";

	// Token: 0x0400071F RID: 1823
	public static readonly HashedString CHECK_MAIL_SUCCESS = "mail_success_pst";

	// Token: 0x04000720 RID: 1824
	public static readonly HashedString CHECK_MAIL_FAILURE = "mail_failure_pst";

	// Token: 0x04000721 RID: 1825
	public static readonly HashedString CHECK_MAIL_DUPLICATE = "mail_duplicate_pst";

	// Token: 0x04000722 RID: 1826
	public static readonly HashedString FOOD_SUCCESS = "food_like_loop";

	// Token: 0x04000723 RID: 1827
	public static readonly HashedString FOOD_FAILURE = "food_dislike_loop";

	// Token: 0x04000724 RID: 1828
	public static readonly HashedString FOOD_DUPLICATE = "food_duplicate_loop";

	// Token: 0x04000725 RID: 1829
	public static readonly HashedString FOOD_IDLE = "idle_food_quest";

	// Token: 0x04000726 RID: 1830
	public static readonly HashedString DECOR_IDLE = "idle_decor_quest";

	// Token: 0x04000727 RID: 1831
	public static readonly HashedString POWER_IDLE = "idle_power_quest";

	// Token: 0x04000728 RID: 1832
	public static readonly HashedString BLINDS_IDLE_0 = "idle_blinds_0";

	// Token: 0x04000729 RID: 1833
	public static readonly HashedString PARCEL_SNAPTO = "parcel_snapTo";

	// Token: 0x0400072A RID: 1834
	public static readonly string MinionName = "JORGE";

	// Token: 0x0400072B RID: 1835
	public static readonly string BodyAnimFile = "body_lonelyminion_kanim";
}
