using System;

namespace STRINGS
{
	// Token: 0x02000D41 RID: 3393
	public class COLONY_ACHIEVEMENTS
	{
		// Token: 0x04004E41 RID: 20033
		public static LocString ACHIEVED_THIS_COLONY_TOOLTIP = "The current colony fulfilled this Initiative";

		// Token: 0x04004E42 RID: 20034
		public static LocString NOT_ACHIEVED_THIS_COLONY = "The current colony hasn't fulfilled this Initiative";

		// Token: 0x04004E43 RID: 20035
		public static LocString FAILED_THIS_COLONY = "The current colony cannot fulfill this Initiative";

		// Token: 0x04004E44 RID: 20036
		public static LocString ACHIEVED_OTHER_COLONY_TOOLTIP = "This Initiative was fulfilled by a past colony";

		// Token: 0x04004E45 RID: 20037
		public static LocString NOT_ACHIEVED_EVER = "This Initiative's never been fulfilled";

		// Token: 0x04004E46 RID: 20038
		public static LocString PRE_VICTORY_MESSAGE_HEADER = "- ALERT -";

		// Token: 0x04004E47 RID: 20039
		public static LocString PRE_VICTORY_MESSAGE_BODY = "IMPERATIVE ACHIEVED: {0}";

		// Token: 0x02001CCB RID: 7371
		public static class DLC
		{
			// Token: 0x0400837F RID: 33663
			public static LocString EXPANSION1 = string.Concat(new string[]
			{
				UI.PRE_KEYWORD,
				"\n\n<i>",
				UI.DLC1.NAME,
				"</i>",
				UI.PST_KEYWORD,
				" DLC Achievement"
			});
		}

		// Token: 0x02001CCC RID: 7372
		public class MISC_REQUIREMENTS
		{
			// Token: 0x04008380 RID: 33664
			public static LocString WINCONDITION_LEAVE = "The Great Escape";

			// Token: 0x04008381 RID: 33665
			public static LocString WINCONDITION_LEAVE_DESCRIPTION = "Ensure your colony's legacy by fulfilling the requirements of the Escape Imperative.";

			// Token: 0x04008382 RID: 33666
			public static LocString WINCONDITION_STAY = "Home Sweet Home";

			// Token: 0x04008383 RID: 33667
			public static LocString WINCONDITION_STAY_DESCRIPTION = "Establish your permanent home by fulfilling the requirements of the Colonize Imperative.";

			// Token: 0x04008384 RID: 33668
			public static LocString WINCONDITION_ARTIFACTS = "Cosmic Archaeology";

			// Token: 0x04008385 RID: 33669
			public static LocString WINCONDITION_ARTIFACTS_DESCRIPTION = "Uncover the past to secure your future by fullfilling the requirements of the Exploration Imperative.";

			// Token: 0x04008386 RID: 33670
			public static LocString NO_PLANTERBOX = "Locavore";

			// Token: 0x04008387 RID: 33671
			public static LocString NO_PLANTERBOX_DESCRIPTION = "Have Duplicants consume 400,000kcal of food without planting any seeds in Planter Boxes, Farm Tiles, or Hydroponic Farms.";

			// Token: 0x04008388 RID: 33672
			public static LocString EAT_MEAT = "Carnivore";

			// Token: 0x04008389 RID: 33673
			public static LocString EAT_MEAT_DESCRIPTION = "Have Duplicants eat 400,000kcal of critter meat before the 100th cycle.";

			// Token: 0x0400838A RID: 33674
			public static LocString BUILD_NATURE_RESERVES = "Some Reservations";

			// Token: 0x0400838B RID: 33675
			public static LocString BUILD_NATURE_RESERVES_DESCRIPTION = "Improve Duplicant Morale by designating {1} areas as {0}.";

			// Token: 0x0400838C RID: 33676
			public static LocString TWENTY_DUPES = "No Place Like Clone";

			// Token: 0x0400838D RID: 33677
			public static LocString TWENTY_DUPES_DESCRIPTION = "Have at least 20 living Duplicants living in the colony at one time.";

			// Token: 0x0400838E RID: 33678
			public static LocString SURVIVE_HUNDRED_CYCLES = "Turn of the Century";

			// Token: 0x0400838F RID: 33679
			public static LocString SURVIVE_HUNDRED_CYCLES_DESCRIPTION = "Reach cycle 100 with at least one living Duplicant.";

			// Token: 0x04008390 RID: 33680
			public static LocString TAME_GASSYMOO = "Moovin' On Up";

			// Token: 0x04008391 RID: 33681
			public static LocString TAME_GASSYMOO_DESCRIPTION = "Find and tame a Gassy Moo.";

			// Token: 0x04008392 RID: 33682
			public static LocString SIXKELVIN_BUILDING = "Not 0K, But Pretty Cool";

			// Token: 0x04008393 RID: 33683
			public static LocString SIXKELVIN_BUILDING_DESCRIPTION = "Reduce the temperature of a building to 6 Kelvin.";

			// Token: 0x04008394 RID: 33684
			public static LocString CLEAN_ENERGY = "Super Sustainable";

			// Token: 0x04008395 RID: 33685
			public static LocString CLEAN_ENERGY_DESCRIPTION = "Generate 240,000kJ of power without using coal, natural gas, petrol or wood generators.";

			// Token: 0x04008396 RID: 33686
			public static LocString BUILD_OUTSIDE_BIOME = "Outdoor Renovations";

			// Token: 0x04008397 RID: 33687
			public static LocString BUILD_OUTSIDE_BIOME_DESCRIPTION = "Construct a building outside the initial starting biome.";

			// Token: 0x04008398 RID: 33688
			public static LocString TUBE_TRAVEL_DISTANCE = "Totally Tubular";

			// Token: 0x04008399 RID: 33689
			public static LocString TUBE_TRAVEL_DISTANCE_DESCRIPTION = "Have Duplicants travel 10,000m by Transit Tube.";

			// Token: 0x0400839A RID: 33690
			public static LocString REACH_SPACE_ANY_DESTINATION = "Space Race";

			// Token: 0x0400839B RID: 33691
			public static LocString REACH_SPACE_ANY_DESTINATION_DESCRIPTION = "Launch your first rocket into space.";

			// Token: 0x0400839C RID: 33692
			public static LocString EQUIP_N_DUPES = "And Nowhere to Go";

			// Token: 0x0400839D RID: 33693
			public static LocString EQUIP_N_DUPES_DESCRIPTION = "Have {0} Duplicants wear non-default clothing simultaneously.";

			// Token: 0x0400839E RID: 33694
			public static LocString EXOSUIT_CYCLES = "Job Suitability";

			// Token: 0x0400839F RID: 33695
			public static LocString EXOSUIT_CYCLES_DESCRIPTION = "For {0} cycles in a row, have every Duplicant in the colony complete at least one chore while wearing an Exosuit.";

			// Token: 0x040083A0 RID: 33696
			public static LocString HATCH_REFINEMENT = "Down the Hatch";

			// Token: 0x040083A1 RID: 33697
			public static LocString HATCH_REFINEMENT_DESCRIPTION = "Produce {0} of refined metal by ranching Smooth Hatches.";

			// Token: 0x040083A2 RID: 33698
			public static LocString VARIETY_OF_ROOMS = "Get a Room";

			// Token: 0x040083A3 RID: 33699
			public static LocString VARIETY_OF_ROOMS_DESCRIPTION = "Build at least one of each of the following rooms in a single colony: A Nature Reserve, a Hospital, a Recreation Room, a Great Hall, a Bedroom, a Washroom, a Greenhouse and a Stable.";

			// Token: 0x040083A4 RID: 33700
			public static LocString CURED_DISEASE = "They Got Better";

			// Token: 0x040083A5 RID: 33701
			public static LocString CURED_DISEASE_DESCRIPTION = "Cure a sick Duplicant of disease.";

			// Token: 0x040083A6 RID: 33702
			public static LocString SURVIVE_ONE_YEAR = "One Year, to be Exact";

			// Token: 0x040083A7 RID: 33703
			public static LocString SURVIVE_ONE_YEAR_DESCRIPTION = "Reach cycle 365.25 with a single colony.";

			// Token: 0x040083A8 RID: 33704
			public static LocString INSPECT_POI = "Ghosts of Gravitas";

			// Token: 0x040083A9 RID: 33705
			public static LocString INSPECT_POI_DESCRIPTION = "Recover a Database entry by inspecting facility ruins.";

			// Token: 0x040083AA RID: 33706
			public static LocString CLEAR_FOW = "Pulling Back The Veil";

			// Token: 0x040083AB RID: 33707
			public static LocString CLEAR_FOW_DESCRIPTION = "Reveal 80% of map by exploring outside the starting biome.";

			// Token: 0x040083AC RID: 33708
			public static LocString EXPLORE_OIL_BIOME = "Slick";

			// Token: 0x040083AD RID: 33709
			public static LocString EXPLORE_OIL_BIOME_DESCRIPTION = "Enter an oil biome for the first time.";

			// Token: 0x040083AE RID: 33710
			public static LocString TAME_BASIC_CRITTERS = "Critter Whisperer";

			// Token: 0x040083AF RID: 33711
			public static LocString TAME_BASIC_CRITTERS_DESCRIPTION = "Find and tame one of every critter species in the world. Default morphs only.";

			// Token: 0x040083B0 RID: 33712
			public static LocString HATCH_A_CRITTER = "Good Egg";

			// Token: 0x040083B1 RID: 33713
			public static LocString HATCH_A_CRITTER_DESCRIPTION = "Hatch a new critter morph from an egg.";

			// Token: 0x040083B2 RID: 33714
			public static LocString BUNKER_DOOR_DEFENSE = "Immovable Object";

			// Token: 0x040083B3 RID: 33715
			public static LocString BUNKER_DOOR_DEFENSE_DESCRIPTION = "Block a meteor from hitting your base using a Bunker Door.";

			// Token: 0x040083B4 RID: 33716
			public static LocString AUTOMATE_A_BUILDING = "Red Light, Green Light";

			// Token: 0x040083B5 RID: 33717
			public static LocString AUTOMATE_A_BUILDING_DESCRIPTION = "Automate a building using sensors or switches from the Automation tab in the Build Menu.";

			// Token: 0x040083B6 RID: 33718
			public static LocString COMPLETED_SKILL_BRANCH = "To Pay the Bills";

			// Token: 0x040083B7 RID: 33719
			public static LocString COMPLETED_SKILL_BRANCH_DESCRIPTION = "Use a Duplicant's Skill Points to buy out an entire branch of the Skill Tree.";

			// Token: 0x040083B8 RID: 33720
			public static LocString GENERATOR_TUNEUP = "Finely Tuned Machine";

			// Token: 0x040083B9 RID: 33721
			public static LocString GENERATOR_TUNEUP_DESCRIPTION = "Perform {0} Tune Ups on power generators.";

			// Token: 0x040083BA RID: 33722
			public static LocString COMPLETED_RESEARCH = "Honorary Doctorate";

			// Token: 0x040083BB RID: 33723
			public static LocString COMPLETED_RESEARCH_DESCRIPTION = "Unlock every item in the Research Tree.";

			// Token: 0x040083BC RID: 33724
			public static LocString IDLE_DUPLICANTS = "Easy Livin'";

			// Token: 0x040083BD RID: 33725
			public static LocString IDLE_DUPLICANTS_DESCRIPTION = "Have Auto-Sweepers complete more deliveries to machines than Duplicants over 5 cycles.";

			// Token: 0x040083BE RID: 33726
			public static LocString COOKED_FOOD = "It's Not Raw";

			// Token: 0x040083BF RID: 33727
			public static LocString COOKED_FOOD_DESCRIPTION = "Have a Duplicant eat any cooked meal prepared at an Electric Grill or Gas Range.";

			// Token: 0x040083C0 RID: 33728
			public static LocString PLUMBED_WASHROOMS = "Royal Flush";

			// Token: 0x040083C1 RID: 33729
			public static LocString PLUMBED_WASHROOMS_DESCRIPTION = "Replace all the Outhouses and Wash Basins in your colony with Lavatories and Sinks.";

			// Token: 0x040083C2 RID: 33730
			public static LocString BASIC_COMFORTS = "Bed and Bath";

			// Token: 0x040083C3 RID: 33731
			public static LocString BASIC_COMFORTS_DESCRIPTION = "Have at least one toilet in the colony and a bed for every Duplicant.";

			// Token: 0x040083C4 RID: 33732
			public static LocString BASIC_PUMPING = "Oxygen Not Occluded";

			// Token: 0x040083C5 RID: 33733
			public static LocString BASIC_PUMPING_DESCRIPTION = "Distribute 1000" + UI.UNITSUFFIXES.MASS.KILOGRAM + " of Oxygen using gas vents.";

			// Token: 0x040083C6 RID: 33734
			public static LocString MASTERPIECE_PAINTING = "Art Underground";

			// Token: 0x040083C7 RID: 33735
			public static LocString MASTERPIECE_PAINTING_DESCRIPTION = "Have a Duplicant with the Masterworks skill paint a Masterpiece quality painting.";

			// Token: 0x040083C8 RID: 33736
			public static LocString FIRST_TELEPORT = "First Teleport of Call";

			// Token: 0x040083C9 RID: 33737
			public static LocString FIRST_TELEPORT_DESCRIPTION = "Teleport a Duplicant and defrost a Friend on another world.";

			// Token: 0x040083CA RID: 33738
			public static LocString SOFT_LAUNCH = "Soft Launch";

			// Token: 0x040083CB RID: 33739
			public static LocString SOFT_LAUNCH_DESCRIPTION = "Build a launchpad on a world without a teleporter.";

			// Token: 0x040083CC RID: 33740
			public static LocString LAND_ON_ALL_WORLDS = "Cluster Conquest";

			// Token: 0x040083CD RID: 33741
			public static LocString LAND_ON_ALL_WORLDS_DESCRIPTION = "Land dupes or rovers on all worlds in the cluster.";

			// Token: 0x040083CE RID: 33742
			public static LocString REACTOR_USAGE = "That's Rad!";

			// Token: 0x040083CF RID: 33743
			public static LocString REACTOR_USAGE_DESCRIPTION = "Run a Research Reactor at full capacity for {0} cycles.";

			// Token: 0x040083D0 RID: 33744
			public static LocString GMO_OK = "GMO A-OK";

			// Token: 0x040083D1 RID: 33745
			public static LocString GMO_OK_DESCRIPTION = "Successfully analyze at least one seed of all mutatable plants.";

			// Token: 0x040083D2 RID: 33746
			public static LocString SWEETER_THAN_HONEY = "Sweeter Than Honey";

			// Token: 0x040083D3 RID: 33747
			public static LocString SWEETER_THAN_HONEY_DESCRIPTION = "Extract Uranium from a Beeta hive without getting stung.";

			// Token: 0x040083D4 RID: 33748
			public static LocString RADICAL_TRIP = "Radical Trip";

			// Token: 0x040083D5 RID: 33749
			public static LocString RADICAL_TRIP_DESCRIPTION = "Have radbolts travel a cumulative {0}km.";

			// Token: 0x040083D6 RID: 33750
			public static LocString MINE_THE_GAP = "Mine the Gap";

			// Token: 0x040083D7 RID: 33751
			public static LocString MINE_THE_GAP_DESCRIPTION = "Mine 1,000,000kg from space POIs.";

			// Token: 0x040083D8 RID: 33752
			public static LocString SURVIVE_IN_A_ROCKET = "Morale High Ground";

			// Token: 0x040083D9 RID: 33753
			public static LocString SURVIVE_IN_A_ROCKET_DESCRIPTION = "Have the Duplicants in one rocket survive in space for {0} cycles in a row with a morale of {1} or higher.";

			// Token: 0x02002790 RID: 10128
			public class STATUS
			{
				// Token: 0x0400AA3E RID: 43582
				public static LocString PLATFORM_UNLOCKING_DISABLED = "Platform achievements cannot be unlocked because a debug command was used in this colony. ";

				// Token: 0x0400AA3F RID: 43583
				public static LocString PLATFORM_UNLOCKING_ENABLED = "Platform achievements can be unlocked.";

				// Token: 0x0400AA40 RID: 43584
				public static LocString EXPAND_TOOLTIP = "<i>" + UI.CLICK(UI.ClickType.Click) + " to view progress</i>";

				// Token: 0x0400AA41 RID: 43585
				public static LocString CYCLE_NUMBER = "Cycle: {0} / {1}";

				// Token: 0x0400AA42 RID: 43586
				public static LocString REMAINING_CYCLES = "Cycles remaining: {0} / {1}";

				// Token: 0x0400AA43 RID: 43587
				public static LocString FRACTIONAL_CYCLE = "Cycle: {0:0.##} / {1:0.##}";

				// Token: 0x0400AA44 RID: 43588
				public static LocString LAUNCHED_ROCKET = "Launched a Rocket into Space";

				// Token: 0x0400AA45 RID: 43589
				public static LocString LAUNCHED_ROCKET_TO_WORMHOLE = "Sent a Duplicant on a one-way mission to the furthest Starmap destination";

				// Token: 0x0400AA46 RID: 43590
				public static LocString BUILT_A_ROOM = "Built a {0}.";

				// Token: 0x0400AA47 RID: 43591
				public static LocString BUILT_N_ROOMS = "Built {0}: {1} / {2}";

				// Token: 0x0400AA48 RID: 43592
				public static LocString CALORIE_SURPLUS = "Calorie surplus: {0} / {1}";

				// Token: 0x0400AA49 RID: 43593
				public static LocString TECH_RESEARCHED = "Tech researched: {0} / {1}";

				// Token: 0x0400AA4A RID: 43594
				public static LocString SKILL_BRANCH = "Unlocked an entire branch of the skill tree";

				// Token: 0x0400AA4B RID: 43595
				public static LocString CLOTHE_DUPES = "Duplicants in clothing: {0} / {1}";

				// Token: 0x0400AA4C RID: 43596
				public static LocString KELVIN_COOLING = "Coldest building: {0:##0.#}K";

				// Token: 0x0400AA4D RID: 43597
				public static LocString NO_FARM_TILES = "No farmed plants";

				// Token: 0x0400AA4E RID: 43598
				public static LocString CALORIES_FROM_MEAT = "Calories from meat: {0} / {1}";

				// Token: 0x0400AA4F RID: 43599
				public static LocString CONSUME_CALORIES = "Calories: {0} / {1}";

				// Token: 0x0400AA50 RID: 43600
				public static LocString CONSUME_ITEM = "Consume something prepared at {0}";

				// Token: 0x0400AA51 RID: 43601
				public static LocString PREPARED_SEPARATOR = " or ";

				// Token: 0x0400AA52 RID: 43602
				public static LocString BUILT_OUTSIDE_START = "Built outside the starting biome";

				// Token: 0x0400AA53 RID: 43603
				public static LocString TRAVELED_IN_TUBES = "Distance: {0:n} m / {1:n} m";

				// Token: 0x0400AA54 RID: 43604
				public static LocString ENTER_OIL_BIOME = "Entered an oil biome";

				// Token: 0x0400AA55 RID: 43605
				public static LocString VENTED_MASS = "Vented: {0} / {1}";

				// Token: 0x0400AA56 RID: 43606
				public static LocString BUILT_ONE_TOILET = "Built one toilet";

				// Token: 0x0400AA57 RID: 43607
				public static LocString BUILING_BEDS = "Built beds: {0} ({1} Needed)";

				// Token: 0x0400AA58 RID: 43608
				public static LocString BUILT_ONE_BED_PER_DUPLICANT = "Built one bed for each Duplicant";

				// Token: 0x0400AA59 RID: 43609
				public static LocString UPGRADE_ALL_BUILDINGS = "All {0} upgraded to {1}";

				// Token: 0x0400AA5A RID: 43610
				public static LocString AUTOMATE_A_BUILDING = "Automated a building";

				// Token: 0x0400AA5B RID: 43611
				public static LocString CREATE_A_PAINTING = "Created a masterpiece painting";

				// Token: 0x0400AA5C RID: 43612
				public static LocString INVESTIGATE_A_POI = "Inspected a ruin";

				// Token: 0x0400AA5D RID: 43613
				public static LocString HATCH_A_MORPH = "Hatched a critter morph";

				// Token: 0x0400AA5E RID: 43614
				public static LocString CURED_DISEASE = "Cured a disease";

				// Token: 0x0400AA5F RID: 43615
				public static LocString CHORES_OF_TYPE = "{2} errands: {0} / {1}";

				// Token: 0x0400AA60 RID: 43616
				public static LocString REVEALED = "Revealed: {0:0.##}% / {1:0.##}%";

				// Token: 0x0400AA61 RID: 43617
				public static LocString POOP_PRODUCTION = "Poop production: {0} / {1}";

				// Token: 0x0400AA62 RID: 43618
				public static LocString BLOCKED_A_COMET = "Blocked a meteor with a Bunker Door";

				// Token: 0x0400AA63 RID: 43619
				public static LocString POPULATION = "Population: {0} / {1}";

				// Token: 0x0400AA64 RID: 43620
				public static LocString TAME_A_CRITTER = "Tamed a {0}";

				// Token: 0x0400AA65 RID: 43621
				public static LocString ARM_PERFORMANCE = "Auto-Sweepers outperformed dupes for cycles: {0} / {1}";

				// Token: 0x0400AA66 RID: 43622
				public static LocString ARM_VS_DUPE_FETCHES = "Deliveries this cycle: Auto-Sweepers: {1} Duplicants: {2}";

				// Token: 0x0400AA67 RID: 43623
				public static LocString EXOSUIT_CYCLES = "All Dupes completed a Exosuit errand for cycles: {0} / {1}";

				// Token: 0x0400AA68 RID: 43624
				public static LocString EXOSUIT_THIS_CYCLE = "Dupes who completed Exosuit errands this cycle: {0} / {1}";

				// Token: 0x0400AA69 RID: 43625
				public static LocString GENERATE_POWER = "Energy generated: {0} / {1}";

				// Token: 0x0400AA6A RID: 43626
				public static LocString NO_BUILDING = "Never built a {0}";

				// Token: 0x0400AA6B RID: 43627
				public static LocString MORALE = "{0} morale: {1}";

				// Token: 0x0400AA6C RID: 43628
				public static LocString COLLECT_ARTIFACTS = "Study different Terrestrial Artifacts at the Artifact Analysis Station.\nUnique Terrestrial Artifacts studied: {collectedCount} / {neededCount}";

				// Token: 0x0400AA6D RID: 43629
				public static LocString COLLECT_SPACE_ARTIFACTS = "Study different Space Artifacts at the Artifact Analysis Station.\nUnique Space Artifacts studied: {collectedCount} / {neededCount}";

				// Token: 0x0400AA6E RID: 43630
				public static LocString ESTABLISH_COLONIES = "Establish colonies on {goalBaseCount} asteroids by building and activating Mini-Pods.\nColonies established: {baseCount} / {neededCount}.";

				// Token: 0x0400AA6F RID: 43631
				public static LocString OPEN_TEMPORAL_TEAR = "Open the Temporal Tear by finding and activating the Temporal Tear Opener";

				// Token: 0x0400AA70 RID: 43632
				public static LocString TELEPORT_DUPLICANT = "Teleport a Duplicant to another world";

				// Token: 0x0400AA71 RID: 43633
				public static LocString DEFROST_DUPLICANT = "Defrost a Duplicant";

				// Token: 0x0400AA72 RID: 43634
				public static LocString BUILD_A_LAUNCHPAD = "Build a launchpad on a new world without a teleporter";

				// Token: 0x0400AA73 RID: 43635
				public static LocString LAND_DUPES_ON_ALL_WORLDS = "Duplicants or rovers landed on {0} of {1} planetoids";

				// Token: 0x0400AA74 RID: 43636
				public static LocString RUN_A_REACTOR = "Reactor running for cycles: {0} / {1}";

				// Token: 0x0400AA75 RID: 43637
				public static LocString ANALYZE_SEED = "Analyze {0} mutant";

				// Token: 0x0400AA76 RID: 43638
				public static LocString GET_URANIUM_WITHOUT_STING = "Got uranium out of a Beeta hive without getting stung";

				// Token: 0x0400AA77 RID: 43639
				public static LocString RADBOLT_TRAVEL = "Radbolts travelled: {0:n} m / {1:n} m";

				// Token: 0x0400AA78 RID: 43640
				public static LocString MINE_SPACE_POI = "Mined: {0:n} / {1:n} kg";

				// Token: 0x0400AA79 RID: 43641
				public static LocString SURVIVE_SPACE = "Duplicants in {3} have ended each cycle in space with at least {0} morale for: {1} / {2} cycles";

				// Token: 0x0400AA7A RID: 43642
				public static LocString SURVIVE_SPACE_COMPLETE = "Duplicants survived in space with at least {0} morale for {1} cycles.";

				// Token: 0x0400AA7B RID: 43643
				public static LocString HARVEST_HIVE = "Uranium extracted from a Beeta hive without getting stung";
			}
		}

		// Token: 0x02001CCD RID: 7373
		public class THRIVING
		{
			// Token: 0x040083DA RID: 33754
			public static LocString NAME = "Home Sweet Home";

			// Token: 0x040083DB RID: 33755
			public static LocString MYLOGNAME = "This Is Our Home";

			// Token: 0x040083DC RID: 33756
			public static LocString DESCRIPTION = "";

			// Token: 0x040083DD RID: 33757
			public static LocString MESSAGE_TITLE = "THIS IS OUR HOME";

			// Token: 0x040083DE RID: 33758
			public static LocString MESSAGE_BODY = "Few civilizations throughout time have had the privilege of understanding their origins. The one thing that matters is that we are here now, and we make the best of the world we've been given. I am proud to say...\n\nThis asteroid is our home.";

			// Token: 0x02002791 RID: 10129
			public class VIDEO_TEXT
			{
				// Token: 0x0400AA7C RID: 43644
				public static LocString FIRST = "Few civilizations throughout time have had the privilege of understanding their origins.";

				// Token: 0x0400AA7D RID: 43645
				public static LocString SECOND = "The only thing that matters is that we are here now, and we make the best of the world we've been given. I am proud to say...";

				// Token: 0x0400AA7E RID: 43646
				public static LocString THIRD = "This asteroid is our home.";
			}

			// Token: 0x02002792 RID: 10130
			public class REQUIREMENTS
			{
				// Token: 0x0400AA7F RID: 43647
				public static LocString BUILT_MONUMENT = "Build a Great Monument";

				// Token: 0x0400AA80 RID: 43648
				public static LocString BUILT_MONUMENT_DESCRIPTION = string.Concat(new string[]
				{
					"Build all three sections of a ",
					UI.PRE_KEYWORD,
					"Great Monument",
					UI.PST_KEYWORD,
					" to mark the colony as your home"
				});

				// Token: 0x0400AA81 RID: 43649
				public static LocString MINIMUM_DUPLICANTS = "Print {0} Duplicants";

				// Token: 0x0400AA82 RID: 43650
				public static LocString MINIMUM_DUPLICANTS_DESCRIPTION = "The colony must have <b>{0}</b> or more living Duplicants";

				// Token: 0x0400AA83 RID: 43651
				public static LocString MINIMUM_MORALE = "Maintain {0} Morale";

				// Token: 0x0400AA84 RID: 43652
				public static LocString MINIMUM_MORALE_DESCRIPTION = string.Concat(new string[]
				{
					"All Duplicants must have ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" of 16 or higher"
				});

				// Token: 0x0400AA85 RID: 43653
				public static LocString MINIMUM_CYCLE = "Survive {0} Cycles";

				// Token: 0x0400AA86 RID: 43654
				public static LocString MINIMUM_CYCLE_DESCRIPTION = "The colony must survive a minimum of <b>{0}</b> cycles";
			}
		}

		// Token: 0x02001CCE RID: 7374
		public class DISTANT_PLANET_REACHED
		{
			// Token: 0x040083DF RID: 33759
			public static LocString NAME = "The Great Escape";

			// Token: 0x040083E0 RID: 33760
			public static LocString MYLOGNAME = "A Colony's Hope";

			// Token: 0x040083E1 RID: 33761
			public static LocString DESCRIPTION = "";

			// Token: 0x040083E2 RID: 33762
			public static LocString MESSAGE_TITLE = "A COLONY'S HOPE";

			// Token: 0x040083E3 RID: 33763
			public static LocString MESSAGE_BODY = "Our homeworld in this universe is gone, replaced by the skeleton of a planet and a wound in the sky... But I hold out hope that other worlds exist out there, tucked away in other dimensions. I sent my Duplicant through the Temporal Tear carrying that hope on their shoulders... Perhaps one day they'll find a place to call home, and begin a thriving colony all their own.";

			// Token: 0x040083E4 RID: 33764
			public static LocString MESSAGE_TITLE_DLC1 = "A DIMENSIONAL ADVENTURE";

			// Token: 0x040083E5 RID: 33765
			public static LocString MESSAGE_BODY_DLC1 = "We have always viewed the Temporal Tear as a phenomenon to fear but, like the civilizations before us, the call to adventure asks us to confront our anxiety and leap into the unknown. As a radical action of hope, I have sent enough Duplicants through the Temporal Tear to start another colony, explore dimensions beyond ours and plant the seeds of life throughout time and space.";

			// Token: 0x02002793 RID: 10131
			public class VIDEO_TEXT
			{
				// Token: 0x0400AA87 RID: 43655
				public static LocString FIRST = "Our homeworld in this universe is gone, replaced by the skeleton of a planet and a wound in the sky... But I hold out hope that other worlds exist out there, tucked away in other dimensions.";

				// Token: 0x0400AA88 RID: 43656
				public static LocString SECOND = "I sent my Duplicant through the Temporal Tear carrying that hope on their shoulders... Perhaps one day they'll find a place to call home, and begin a thriving colony all their own.";
			}

			// Token: 0x02002794 RID: 10132
			public class VIDEO_TEXT_DLC1
			{
				// Token: 0x0400AA89 RID: 43657
				public static LocString FIRST = "DLC1";

				// Token: 0x0400AA8A RID: 43658
				public static LocString SECOND = "DLC1";
			}

			// Token: 0x02002795 RID: 10133
			public class REQUIREMENTS
			{
				// Token: 0x0400AA8B RID: 43659
				public static LocString REACHED_SPACE_DESTINATION = "Breach the {0}";

				// Token: 0x0400AA8C RID: 43660
				public static LocString REACHED_SPACE_DESTINATION_DESCRIPTION = "Send a Duplicant on a one-way mission to the furthest Starmap destination";

				// Token: 0x0400AA8D RID: 43661
				public static LocString OPEN_TEMPORAL_TEAR = "Open the Temporal Tear";
			}
		}

		// Token: 0x02001CCF RID: 7375
		public class STUDY_ARTIFACTS
		{
			// Token: 0x040083E6 RID: 33766
			public static LocString NAME = "Cosmic Archaeology";

			// Token: 0x040083E7 RID: 33767
			public static LocString MYLOGNAME = "Artifacts";

			// Token: 0x040083E8 RID: 33768
			public static LocString DESCRIPTION = "";

			// Token: 0x040083E9 RID: 33769
			public static LocString MESSAGE_TITLE = "LINK TO OUR PAST";

			// Token: 0x040083EA RID: 33770
			public static LocString MESSAGE_BODY = "In exploring this corner of the universe we have found and assembled a collection of artifacts from another civilization. Studying these artifacts can give us a greater understanding of who we are and where we come from. Only by learning about the past can we build a brighter future, one where we learn from the mistakes of our predecessors.";

			// Token: 0x040083EB RID: 33771
			public static LocString MESSAGE_TITLE_DLC1 = "DLC1";

			// Token: 0x040083EC RID: 33772
			public static LocString MESSAGE_BODY_DLC1 = "DLC1";

			// Token: 0x02002796 RID: 10134
			public class VIDEO_TEXT
			{
				// Token: 0x0400AA8E RID: 43662
				public static LocString FIRST = "Our homeworld in this universe is gone, replaced by the skeleton of a planet and a wound in the sky... But I hold out hope that other worlds exist out there, tucked away in other dimensions.";

				// Token: 0x0400AA8F RID: 43663
				public static LocString SECOND = "I sent my Duplicant through the Temporal Tear carrying that hope on their shoulders... Perhaps one day they'll find a place to call home, and begin a thriving colony all their own.";
			}

			// Token: 0x02002797 RID: 10135
			public class VIDEO_TEXT_DLC1
			{
				// Token: 0x0400AA90 RID: 43664
				public static LocString FIRST = "DLC1";

				// Token: 0x0400AA91 RID: 43665
				public static LocString SECOND = "DLC1";
			}

			// Token: 0x02002798 RID: 10136
			public class REQUIREMENTS
			{
				// Token: 0x0400AA92 RID: 43666
				public static LocString STUDY_ARTIFACTS = "Study {artifactCount} Terrestrial Artifacts";

				// Token: 0x0400AA93 RID: 43667
				public static LocString STUDY_ARTIFACTS_DESCRIPTION = "Study {artifactCount} Terrestrial Artifacts at the Artifact Analysis Station";

				// Token: 0x0400AA94 RID: 43668
				public static LocString STUDY_SPACE_ARTIFACTS = "Study {artifactCount} Space Artifacts";

				// Token: 0x0400AA95 RID: 43669
				public static LocString STUDY_SPACE_ARTIFACTS_DESCRIPTION = "Study {artifactCount} Space Artifacts at the Artifact Analysis Station";

				// Token: 0x0400AA96 RID: 43670
				public static LocString SEVERAL_COLONIES = "Establish several colonies";

				// Token: 0x0400AA97 RID: 43671
				public static LocString SEVERAL_COLONIES_DESCRIPTION = "Establish colonies on {count} asteroids by building and activating Mini-Pods";
			}
		}
	}
}
