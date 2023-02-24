using System;
using Klei.AI;
using STRINGS;

namespace Database
{
	// Token: 0x02000C98 RID: 3224
	public class GameplayEvents : ResourceSet<GameplayEvent>
	{
		// Token: 0x0600659A RID: 26010 RVA: 0x0026B758 File Offset: 0x00269958
		public GameplayEvents(ResourceSet parent)
			: base("GameplayEvents", parent)
		{
			this.HatchSpawnEvent = base.Add(new CreatureSpawnEvent());
			this.PartyEvent = base.Add(new PartyEvent());
			this.EclipseEvent = base.Add(new EclipseEvent());
			this.SatelliteCrashEvent = base.Add(new SatelliteCrashEvent());
			this.FoodFightEvent = base.Add(new FoodFightEvent());
			string text = "MeteorShowerIronEvent";
			float num = 6000f;
			float num2 = 1.25f;
			MathUtil.MinMax minMax = new MathUtil.MinMax(100f, 400f);
			this.MeteorShowerIronEvent = base.Add(new MeteorShowerEvent(text, num, num2, new MathUtil.MinMax(300f, 1200f), minMax).AddMeteor(IronCometConfig.ID, 1f).AddMeteor(RockCometConfig.ID, 2f).AddMeteor(DustCometConfig.ID, 5f));
			string text2 = "MeteorShowerGoldEvent";
			float num3 = 3000f;
			float num4 = 0.4f;
			minMax = new MathUtil.MinMax(50f, 100f);
			this.MeteorShowerGoldEvent = base.Add(new MeteorShowerEvent(text2, num3, num4, new MathUtil.MinMax(800f, 1200f), minMax).AddMeteor(GoldCometConfig.ID, 2f).AddMeteor(RockCometConfig.ID, 0.5f).AddMeteor(DustCometConfig.ID, 5f));
			string text3 = "MeteorShowerCopperEvent";
			float num5 = 4200f;
			float num6 = 5.5f;
			minMax = new MathUtil.MinMax(100f, 400f);
			this.MeteorShowerCopperEvent = base.Add(new MeteorShowerEvent(text3, num5, num6, new MathUtil.MinMax(300f, 1200f), minMax).AddMeteor(CopperCometConfig.ID, 1f).AddMeteor(RockCometConfig.ID, 1f));
			string text4 = "MeteorShowerFullereneEvent";
			float num7 = 30f;
			float num8 = 0.66f;
			minMax = new MathUtil.MinMax(80f, 80f);
			this.MeteorShowerFullereneEvent = base.Add(new MeteorShowerEvent(text4, num7, num8, new MathUtil.MinMax(1f, 1f), minMax).AddMeteor(FullereneCometConfig.ID, 6f).AddMeteor(RockCometConfig.ID, 1f).AddMeteor(DustCometConfig.ID, 1f));
			string text5 = "MeteorShowerDustEvent";
			float num9 = 9000f;
			float num10 = 2f;
			minMax = new MathUtil.MinMax(100f, 400f);
			this.MeteorShowerDustEvent = base.Add(new MeteorShowerEvent(text5, num9, num10, new MathUtil.MinMax(300f, 1200f), minMax).AddMeteor(RockCometConfig.ID, 1f).AddMeteor(DustCometConfig.ID, 5f));
			string text6 = "GassyMooteorEvent";
			float num11 = 15f;
			float num12 = 5f;
			minMax = new MathUtil.MinMax(15f, 15f);
			this.GassyMooteorEvent = base.Add(new MeteorShowerEvent(text6, num11, num12, new MathUtil.MinMax(1f, 1f), minMax).AddMeteor(GassyMooCometConfig.ID, 1f));
			this.PrickleFlowerBlightEvent = base.Add(new PlantBlightEvent("PrickleFlowerBlightEvent", "PrickleFlower", 3600f, 30f));
			this.CryoFriend = base.Add(new SimpleEvent("CryoFriend", GAMEPLAY_EVENTS.EVENT_TYPES.CRYOFRIEND.NAME, GAMEPLAY_EVENTS.EVENT_TYPES.CRYOFRIEND.DESCRIPTION, "cryofriend_kanim", GAMEPLAY_EVENTS.EVENT_TYPES.CRYOFRIEND.BUTTON, null));
			this.WarpWorldReveal = base.Add(new SimpleEvent("WarpWorldReveal", GAMEPLAY_EVENTS.EVENT_TYPES.WARPWORLDREVEAL.NAME, GAMEPLAY_EVENTS.EVENT_TYPES.WARPWORLDREVEAL.DESCRIPTION, "warpworldreveal_kanim", GAMEPLAY_EVENTS.EVENT_TYPES.WARPWORLDREVEAL.BUTTON, null));
			this.ArtifactReveal = base.Add(new SimpleEvent("ArtifactReveal", GAMEPLAY_EVENTS.EVENT_TYPES.ARTIFACT_REVEAL.NAME, GAMEPLAY_EVENTS.EVENT_TYPES.ARTIFACT_REVEAL.DESCRIPTION, "analyzeartifact_kanim", GAMEPLAY_EVENTS.EVENT_TYPES.ARTIFACT_REVEAL.BUTTON, null));
		}

		// Token: 0x0600659B RID: 26011 RVA: 0x0026BAF4 File Offset: 0x00269CF4
		private void BonusEvents()
		{
			GameplayEventMinionFilters instance = GameplayEventMinionFilters.Instance;
			GameplayEventPreconditions instance2 = GameplayEventPreconditions.Instance;
			Skills skills = Db.Get().Skills;
			RoomTypes roomTypes = Db.Get().RoomTypes;
			this.BonusDream1 = base.Add(new BonusEvent("BonusDream1", null, 1, false, 0).TriggerOnUseBuilding(1, new string[] { "Bed", "LuxuryBed" }).SetRoomConstraints(false, new RoomType[] { roomTypes.Barracks }).AddPrecondition(instance2.BuildingExists("Bed", 2))
				.AddPriorityBoost(instance2.BuildingExists("Bed", 5), 1)
				.AddPriorityBoost(instance2.BuildingExists("LuxuryBed", 1), 5)
				.TrySpawnEventOnSuccess("BonusDream2"));
			this.BonusDream2 = base.Add(new BonusEvent("BonusDream2", null, 1, false, 10).TriggerOnUseBuilding(10, new string[] { "Bed", "LuxuryBed" }).AddPrecondition(instance2.PastEventCountAndNotActive(this.BonusDream1, 1)).AddPrecondition(instance2.Or(instance2.RoomBuilt(roomTypes.Barracks), instance2.RoomBuilt(roomTypes.Bedroom)))
				.AddPriorityBoost(instance2.BuildingExists("LuxuryBed", 1), 5)
				.TrySpawnEventOnSuccess("BonusDream3"));
			this.BonusDream3 = base.Add(new BonusEvent("BonusDream3", null, 1, false, 20).TriggerOnUseBuilding(10, new string[] { "Bed", "LuxuryBed" }).AddPrecondition(instance2.PastEventCountAndNotActive(this.BonusDream2, 1)).AddPrecondition(instance2.Or(instance2.RoomBuilt(roomTypes.Barracks), instance2.RoomBuilt(roomTypes.Bedroom)))
				.TrySpawnEventOnSuccess("BonusDream4"));
			this.BonusDream4 = base.Add(new BonusEvent("BonusDream4", null, 1, false, 30).TriggerOnUseBuilding(10, new string[] { "LuxuryBed" }).AddPrecondition(instance2.PastEventCountAndNotActive(this.BonusDream2, 1)).AddPrecondition(instance2.Or(instance2.RoomBuilt(roomTypes.Barracks), instance2.RoomBuilt(roomTypes.Bedroom))));
			this.BonusToilet1 = base.Add(new BonusEvent("BonusToilet1", null, 1, false, 0).TriggerOnUseBuilding(1, new string[] { "Outhouse", "FlushToilet" }).AddPrecondition(instance2.Or(instance2.BuildingExists("Outhouse", 2), instance2.BuildingExists("FlushToilet", 1))).AddPrecondition(instance2.Or(instance2.BuildingExists("WashBasin", 2), instance2.BuildingExists("WashSink", 1)))
				.AddPriorityBoost(instance2.BuildingExists("FlushToilet", 1), 1)
				.TrySpawnEventOnSuccess("BonusToilet2"));
			this.BonusToilet2 = base.Add(new BonusEvent("BonusToilet2", null, 1, false, 10).TriggerOnUseBuilding(5, new string[] { "FlushToilet" }).AddPrecondition(instance2.BuildingExists("FlushToilet", 1)).AddPrecondition(instance2.PastEventCountAndNotActive(this.BonusToilet1, 1))
				.AddPriorityBoost(instance2.BuildingExists("FlushToilet", 2), 5)
				.TrySpawnEventOnSuccess("BonusToilet3"));
			this.BonusToilet3 = base.Add(new BonusEvent("BonusToilet3", null, 1, false, 20).TriggerOnUseBuilding(5, new string[] { "FlushToilet" }).SetRoomConstraints(false, new RoomType[] { roomTypes.Latrine, roomTypes.PlumbedBathroom }).AddPrecondition(instance2.PastEventCountAndNotActive(this.BonusToilet2, 1))
				.AddPrecondition(instance2.Or(instance2.RoomBuilt(roomTypes.Latrine), instance2.RoomBuilt(roomTypes.PlumbedBathroom)))
				.AddPriorityBoost(instance2.BuildingExists("FlushToilet", 2), 10)
				.TrySpawnEventOnSuccess("BonusToilet4"));
			this.BonusToilet4 = base.Add(new BonusEvent("BonusToilet4", null, 1, false, 30).TriggerOnUseBuilding(5, new string[] { "FlushToilet" }).SetRoomConstraints(false, new RoomType[] { roomTypes.PlumbedBathroom }).AddPrecondition(instance2.PastEventCountAndNotActive(this.BonusToilet3, 1))
				.AddPrecondition(instance2.RoomBuilt(roomTypes.PlumbedBathroom)));
			this.BonusResearch = base.Add(new BonusEvent("BonusResearch", null, 1, false, 0).AddPrecondition(instance2.BuildingExists("ResearchCenter", 1)).AddPrecondition(instance2.ResearchCompleted("FarmingTech")).AddMinionFilter(instance.HasSkillAptitude(skills.Researching1)));
			this.BonusDigging1 = base.Add(new BonusEvent("BonusDigging1", null, 1, true, 0).TriggerOnWorkableComplete(30, new Type[] { typeof(Diggable) }).AddMinionFilter(instance.Or(instance.HasChoreGroupPriorityOrHigher(Db.Get().ChoreGroups.Dig, 4), instance.HasSkillAptitude(skills.Mining1))).AddPriorityBoost(instance2.MinionsWithChoreGroupPriorityOrGreater(Db.Get().ChoreGroups.Dig, 1, 4), 1));
			this.BonusStorage = base.Add(new BonusEvent("BonusStorage", null, 1, true, 0).TriggerOnUseBuilding(10, new string[] { "StorageLocker" }).AddMinionFilter(instance.Or(instance.HasChoreGroupPriorityOrHigher(Db.Get().ChoreGroups.Hauling, 4), instance.HasSkillAptitude(skills.Hauling1))).AddPrecondition(instance2.BuildingExists("StorageLocker", 1)));
			this.BonusBuilder = base.Add(new BonusEvent("BonusBuilder", null, 1, true, 0).TriggerOnNewBuilding(10, Array.Empty<string>()).AddMinionFilter(instance.Or(instance.HasChoreGroupPriorityOrHigher(Db.Get().ChoreGroups.Build, 4), instance.HasSkillAptitude(skills.Building1))));
			this.BonusOxygen = base.Add(new BonusEvent("BonusOxygen", null, 1, false, 0).TriggerOnUseBuilding(1, new string[] { "MineralDeoxidizer" }).AddPrecondition(instance2.BuildingExists("MineralDeoxidizer", 1)).AddPrecondition(instance2.Not(instance2.PastEventCount("BonusAlgae", 1))));
			this.BonusAlgae = base.Add(new BonusEvent("BonusAlgae", "BonusOxygen", 1, false, 0).TriggerOnUseBuilding(1, new string[] { "AlgaeHabitat" }).AddPrecondition(instance2.BuildingExists("AlgaeHabitat", 1)).AddPrecondition(instance2.Not(instance2.PastEventCount("BonusOxygen", 1))));
			this.BonusGenerator = base.Add(new BonusEvent("BonusGenerator", null, 1, false, 0).TriggerOnUseBuilding(1, new string[] { "ManualGenerator" }).AddPrecondition(instance2.BuildingExists("ManualGenerator", 1)));
			this.BonusDoor = base.Add(new BonusEvent("BonusDoor", null, 1, false, 0).TriggerOnUseBuilding(1, new string[] { "Door" }).SetExtraCondition((BonusEvent.GameplayEventData data) => data.building.GetComponent<Door>().RequestedState == Door.ControlState.Locked).AddPrecondition(instance2.RoomBuilt(roomTypes.Barracks)));
			this.BonusHitTheBooks = base.Add(new BonusEvent("BonusHitTheBooks", null, 1, true, 0).TriggerOnWorkableComplete(1, new Type[]
			{
				typeof(ResearchCenter),
				typeof(NuclearResearchCenterWorkable)
			}).AddPrecondition(instance2.BuildingExists("ResearchCenter", 1)).AddMinionFilter(instance.HasSkillAptitude(skills.Researching1)));
			this.BonusLitWorkspace = base.Add(new BonusEvent("BonusLitWorkspace", null, 1, false, 0).TriggerOnWorkableComplete(1, Array.Empty<Type>()).SetExtraCondition((BonusEvent.GameplayEventData data) => data.workable.currentlyLit).AddPrecondition(instance2.CycleRestriction(10f, float.PositiveInfinity)));
			this.BonusTalker = base.Add(new BonusEvent("BonusTalker", null, 1, true, 0).TriggerOnWorkableComplete(3, new Type[] { typeof(SocialGatheringPointWorkable) }).SetExtraCondition((BonusEvent.GameplayEventData data) => (data.workable as SocialGatheringPointWorkable).timesConversed > 0).AddPrecondition(instance2.CycleRestriction(10f, float.PositiveInfinity)));
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x0026C344 File Offset: 0x0026A544
		private void VerifyEvents()
		{
			foreach (GameplayEvent gameplayEvent in this.resources)
			{
				if (gameplayEvent.animFileName == null)
				{
					DebugUtil.LogWarningArgs(new object[] { "Gameplay event anim missing: " + gameplayEvent.Id });
				}
				if (gameplayEvent is BonusEvent)
				{
					this.VerifyBonusEvent(gameplayEvent as BonusEvent);
				}
			}
		}

		// Token: 0x0600659D RID: 26013 RVA: 0x0026C3D8 File Offset: 0x0026A5D8
		private void VerifyBonusEvent(BonusEvent e)
		{
			StringEntry stringEntry;
			if (!Strings.TryGet("STRINGS.GAMEPLAY_EVENTS.BONUS." + e.Id.ToUpper() + ".NAME", out stringEntry))
			{
				DebugUtil.DevLogError(string.Concat(new string[]
				{
					"Event [",
					e.Id,
					"]: STRINGS.GAMEPLAY_EVENTS.BONUS.",
					e.Id.ToUpper(),
					" is missing"
				}));
			}
			Effect effect = Db.Get().effects.TryGet(e.effect);
			if (effect == null)
			{
				DebugUtil.DevLogError(string.Concat(new string[] { "Effect ", e.effect, "[", e.Id, "]: Missing from spreadsheet" }));
				return;
			}
			if (!Strings.TryGet("STRINGS.DUPLICANTS.MODIFIERS." + effect.Id.ToUpper() + ".NAME", out stringEntry))
			{
				DebugUtil.DevLogError(string.Concat(new string[]
				{
					"Effect ",
					e.effect,
					"[",
					e.Id,
					"]: STRINGS.DUPLICANTS.MODIFIERS.",
					effect.Id.ToUpper(),
					".NAME is missing"
				}));
			}
			if (!Strings.TryGet("STRINGS.DUPLICANTS.MODIFIERS." + effect.Id.ToUpper() + ".TOOLTIP", out stringEntry))
			{
				DebugUtil.DevLogError(string.Concat(new string[]
				{
					"Effect ",
					e.effect,
					"[",
					e.Id,
					"]: STRINGS.DUPLICANTS.MODIFIERS.",
					effect.Id.ToUpper(),
					".TOOLTIP is missing"
				}));
			}
		}

		// Token: 0x04004923 RID: 18723
		public GameplayEvent HatchSpawnEvent;

		// Token: 0x04004924 RID: 18724
		public GameplayEvent PartyEvent;

		// Token: 0x04004925 RID: 18725
		public GameplayEvent EclipseEvent;

		// Token: 0x04004926 RID: 18726
		public GameplayEvent SatelliteCrashEvent;

		// Token: 0x04004927 RID: 18727
		public GameplayEvent FoodFightEvent;

		// Token: 0x04004928 RID: 18728
		public GameplayEvent MeteorShowerIronEvent;

		// Token: 0x04004929 RID: 18729
		public GameplayEvent MeteorShowerGoldEvent;

		// Token: 0x0400492A RID: 18730
		public GameplayEvent MeteorShowerCopperEvent;

		// Token: 0x0400492B RID: 18731
		public GameplayEvent MeteorShowerFullereneEvent;

		// Token: 0x0400492C RID: 18732
		public GameplayEvent MeteorShowerDustEvent;

		// Token: 0x0400492D RID: 18733
		public GameplayEvent GassyMooteorEvent;

		// Token: 0x0400492E RID: 18734
		public GameplayEvent PrickleFlowerBlightEvent;

		// Token: 0x0400492F RID: 18735
		public GameplayEvent BonusDream1;

		// Token: 0x04004930 RID: 18736
		public GameplayEvent BonusDream2;

		// Token: 0x04004931 RID: 18737
		public GameplayEvent BonusDream3;

		// Token: 0x04004932 RID: 18738
		public GameplayEvent BonusDream4;

		// Token: 0x04004933 RID: 18739
		public GameplayEvent BonusToilet1;

		// Token: 0x04004934 RID: 18740
		public GameplayEvent BonusToilet2;

		// Token: 0x04004935 RID: 18741
		public GameplayEvent BonusToilet3;

		// Token: 0x04004936 RID: 18742
		public GameplayEvent BonusToilet4;

		// Token: 0x04004937 RID: 18743
		public GameplayEvent BonusResearch;

		// Token: 0x04004938 RID: 18744
		public GameplayEvent BonusDigging1;

		// Token: 0x04004939 RID: 18745
		public GameplayEvent BonusStorage;

		// Token: 0x0400493A RID: 18746
		public GameplayEvent BonusBuilder;

		// Token: 0x0400493B RID: 18747
		public GameplayEvent BonusOxygen;

		// Token: 0x0400493C RID: 18748
		public GameplayEvent BonusAlgae;

		// Token: 0x0400493D RID: 18749
		public GameplayEvent BonusGenerator;

		// Token: 0x0400493E RID: 18750
		public GameplayEvent BonusDoor;

		// Token: 0x0400493F RID: 18751
		public GameplayEvent BonusHitTheBooks;

		// Token: 0x04004940 RID: 18752
		public GameplayEvent BonusLitWorkspace;

		// Token: 0x04004941 RID: 18753
		public GameplayEvent BonusTalker;

		// Token: 0x04004942 RID: 18754
		public GameplayEvent CryoFriend;

		// Token: 0x04004943 RID: 18755
		public GameplayEvent WarpWorldReveal;

		// Token: 0x04004944 RID: 18756
		public GameplayEvent ArtifactReveal;
	}
}
