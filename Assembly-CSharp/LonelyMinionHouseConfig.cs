using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000224 RID: 548
public class LonelyMinionHouseConfig : IBuildingConfig
{
	// Token: 0x06000AD6 RID: 2774 RVA: 0x0003D02C File Offset: 0x0003B22C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LonelyMinionHouse";
		int num = 4;
		int num2 = 6;
		string text2 = "lonely_dupe_home_kanim";
		int num3 = 1000;
		float num4 = 480f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, array, num5, buildLocationRule, LonelyMinionHouseConfig.HOUSE_DECOR, none, 0.2f);
		buildingDef.DefaultAnimState = "on";
		buildingDef.ForegroundLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.EnergyConsumptionWhenActive = 60f;
		buildingDef.AddLogicPowerPort = false;
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(2, 1);
		buildingDef.ShowInBuildMenu = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x0003D0E4 File Offset: 0x0003B2E4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<NonEssentialEnergyConsumer>();
		go.GetComponent<Deconstructable>().allowDeconstruction = false;
		Prioritizable.AddRef(go);
		go.GetComponent<Prioritizable>().SetMasterPriority(new PrioritySetting(PriorityScreen.PriorityClass.high, 5));
		Storage storage = go.AddOrGet<Storage>();
		KnockKnock knockKnock = go.AddOrGet<KnockKnock>();
		LonelyMinionHouse.Def def = go.AddOrGetDef<LonelyMinionHouse.Def>();
		storage.allowItemRemoval = false;
		storage.capacityKg = 250000f;
		storage.storageFilters = STORAGEFILTERS.NOT_EDIBLE_SOLIDS;
		storage.storageFullMargin = TUNING.STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
		storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
		storage.showCapacityStatusItem = true;
		storage.showCapacityAsMainStatus = true;
		knockKnock.triggerWorkReactions = false;
		knockKnock.synchronizeAnims = false;
		knockKnock.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_doorknock_kanim") };
		knockKnock.workAnims = new HashedString[] { "knocking_pre", "knocking_loop" };
		knockKnock.workingPstComplete = new HashedString[] { "knocking_pst" };
		knockKnock.workingPstFailed = null;
		knockKnock.SetButtonTextOverride(new ButtonMenuTextOverride
		{
			Text = CODEX.STORY_TRAITS.LONELYMINION.KNOCK_KNOCK.TEXT,
			CancelText = CODEX.STORY_TRAITS.LONELYMINION.KNOCK_KNOCK.CANCELTEXT,
			ToolTip = CODEX.STORY_TRAITS.LONELYMINION.KNOCK_KNOCK.TOOLTIP,
			CancelToolTip = CODEX.STORY_TRAITS.LONELYMINION.KNOCK_KNOCK.CANCEL_TOOLTIP
		});
		def.Story = Db.Get().Stories.LonelyMinion;
		def.CompletionData = new StoryCompleteData
		{
			KeepSakeSpawnOffset = default(CellOffset),
			CameraTargetOffset = new CellOffset(0, 3)
		};
		def.InitalLoreId = "story_trait_lonelyminion_initial";
		def.EventIntroInfo = new StoryManager.PopupInfo
		{
			Title = CODEX.STORY_TRAITS.LONELYMINION.BEGIN_POPUP.NAME,
			Description = CODEX.STORY_TRAITS.LONELYMINION.BEGIN_POPUP.DESCRIPTION,
			CloseButtonText = CODEX.STORY_TRAITS.CLOSE_BUTTON,
			TextureName = "minionhouseactivate_kanim",
			DisplayImmediate = true,
			PopupType = EventInfoDataHelper.PopupType.BEGIN
		};
		def.CompleteLoreId = "story_trait_lonelyminion_complete";
		def.EventCompleteInfo = new StoryManager.PopupInfo
		{
			Title = CODEX.STORY_TRAITS.LONELYMINION.END_POPUP.NAME,
			Description = CODEX.STORY_TRAITS.LONELYMINION.END_POPUP.DESCRIPTION,
			CloseButtonText = CODEX.STORY_TRAITS.LONELYMINION.END_POPUP.BUTTON,
			TextureName = "minionhousecomplete_kanim",
			PopupType = EventInfoDataHelper.PopupType.COMPLETE
		};
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x0003D334 File Offset: 0x0003B534
	public override void DoPostConfigureComplete(GameObject go)
	{
		UnityEngine.Object.Destroy(go.GetComponent<BuildingEnabledButton>());
		go.GetComponent<RequireInputs>().visualizeRequirements = RequireInputs.Requirements.None;
		this.ConfigureLights(go);
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x0003D354 File Offset: 0x0003B554
	private void ConfigureLights(GameObject go)
	{
		GameObject gameObject = new GameObject("FestiveLights");
		gameObject.SetActive(false);
		gameObject.transform.SetParent(go.transform);
		gameObject.AddOrGet<Light2D>();
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		KBatchedAnimController component = go.GetComponent<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = component.AnimFiles;
		kbatchedAnimController.fgLayer = Grid.SceneLayer.NoLayer;
		kbatchedAnimController.initialAnim = "meter_lights_off";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.FlipX = component.FlipX;
		kbatchedAnimController.FlipY = component.FlipY;
		KBatchedAnimTracker kbatchedAnimTracker = gameObject.AddComponent<KBatchedAnimTracker>();
		kbatchedAnimTracker.SetAnimControllers(kbatchedAnimController, component);
		kbatchedAnimTracker.symbol = "lights_target";
		kbatchedAnimTracker.offset = Vector3.zero;
		for (int i = 0; i < LonelyMinionHouseConfig.LIGHTS_SYMBOLS.Length; i++)
		{
			component.SetSymbolVisiblity(LonelyMinionHouseConfig.LIGHTS_SYMBOLS[i], false);
		}
	}

	// Token: 0x04000649 RID: 1609
	public const string ID = "LonelyMinionHouse";

	// Token: 0x0400064A RID: 1610
	public const string LORE_UNLOCK_PREFIX = "story_trait_lonelyminion_";

	// Token: 0x0400064B RID: 1611
	public const int FriendshipQuestCount = 3;

	// Token: 0x0400064C RID: 1612
	public const string METER_TARGET = "meter_storage_target";

	// Token: 0x0400064D RID: 1613
	public const string METER_ANIM = "meter";

	// Token: 0x0400064E RID: 1614
	public static readonly string[] METER_SYMBOLS = new string[] { "meter_storage", "meter_level" };

	// Token: 0x0400064F RID: 1615
	public const string BLINDS_TARGET = "blinds_target";

	// Token: 0x04000650 RID: 1616
	public const string BLINDS_PREFIX = "meter_blinds";

	// Token: 0x04000651 RID: 1617
	public static readonly string[] BLINDS_SYMBOLS = new string[] { "blinds_target", "blind", "blind_string", "blinds" };

	// Token: 0x04000652 RID: 1618
	private const string LIGHTS_TARGET = "lights_target";

	// Token: 0x04000653 RID: 1619
	private static readonly string[] LIGHTS_SYMBOLS = new string[] { "lights_target", "festive_lights", "lights_wire", "light_bulb", "snapTo_light_locator" };

	// Token: 0x04000654 RID: 1620
	public static readonly HashedString ANSWER = "answer";

	// Token: 0x04000655 RID: 1621
	public static readonly HashedString LIGHTS_OFF = "meter_lights_off";

	// Token: 0x04000656 RID: 1622
	public static readonly HashedString LIGHTS_ON = "meter_lights_on_loop";

	// Token: 0x04000657 RID: 1623
	public static readonly HashedString STORAGE = "storage_off";

	// Token: 0x04000658 RID: 1624
	public static readonly HashedString STORAGE_WORK_PST = "working_pst";

	// Token: 0x04000659 RID: 1625
	public static readonly HashedString[] STORAGE_WORKING = new HashedString[] { "working_pre", "working_loop" };

	// Token: 0x0400065A RID: 1626
	public static readonly EffectorValues HOUSE_DECOR = new EffectorValues
	{
		amount = -25,
		radius = 6
	};

	// Token: 0x0400065B RID: 1627
	public static readonly EffectorValues STORAGE_DECOR = DECOR.PENALTY.TIER1;
}
