using System;

namespace STRINGS
{
	// Token: 0x02000D3D RID: 3389
	public class GAMEPLAY_EVENTS
	{
		// Token: 0x04004E3B RID: 20027
		public static LocString CANCELED = "{0} Canceled";

		// Token: 0x04004E3C RID: 20028
		public static LocString CANCELED_TOOLTIP = "The {0} event was canceled";

		// Token: 0x04004E3D RID: 20029
		public static LocString DEFAULT_OPTION_NAME = "Okay";

		// Token: 0x04004E3E RID: 20030
		public static LocString DEFAULT_OPTION_CONSIDER_NAME = "Let me think about it";

		// Token: 0x04004E3F RID: 20031
		public static LocString CHAIN_EVENT_TOOLTIP = "This event is a chain event.";

		// Token: 0x04004E40 RID: 20032
		public static LocString BONUS_EVENT_DESCRIPTION = "{effects} for {duration}";

		// Token: 0x02001CC0 RID: 7360
		public class LOCATIONS
		{
			// Token: 0x0400836D RID: 33645
			public static LocString NONE_AVAILABLE = "No location currently available";

			// Token: 0x0400836E RID: 33646
			public static LocString SUN = "The Sun";

			// Token: 0x0400836F RID: 33647
			public static LocString SURFACE = "Planetary Surface";

			// Token: 0x04008370 RID: 33648
			public static LocString PRINTING_POD = BUILDINGS.PREFABS.HEADQUARTERS.NAME;

			// Token: 0x04008371 RID: 33649
			public static LocString COLONY_WIDE = "Colonywide";
		}

		// Token: 0x02001CC1 RID: 7361
		public class TIMES
		{
			// Token: 0x04008372 RID: 33650
			public static LocString NOW = "Right now";

			// Token: 0x04008373 RID: 33651
			public static LocString IN_CYCLES = "In {0} cycles";

			// Token: 0x04008374 RID: 33652
			public static LocString UNKNOWN = "Sometime";
		}

		// Token: 0x02001CC2 RID: 7362
		public class EVENT_TYPES
		{
			// Token: 0x020025D9 RID: 9689
			public class PARTY
			{
				// Token: 0x0400A677 RID: 42615
				public static LocString NAME = "Party";

				// Token: 0x0400A678 RID: 42616
				public static LocString DESCRIPTION = "THIS EVENT IS NOT WORKING\n{host} is throwing a birthday party for {dupe}. Make sure there is an available " + ROOMS.TYPES.REC_ROOM.NAME + " for the party.\n\nSocial events are good for Duplicant morale. Rejecting this party will hurt {host} and {dupe}'s fragile ego.";

				// Token: 0x0400A679 RID: 42617
				public static LocString CANCELED_NO_ROOM_TITLE = "Party Canceled";

				// Token: 0x0400A67A RID: 42618
				public static LocString CANCELED_NO_ROOM_DESCRIPTION = "The party was canceled because no " + ROOMS.TYPES.REC_ROOM.NAME + " was available.";

				// Token: 0x0400A67B RID: 42619
				public static LocString UNDERWAY = "Party Happening";

				// Token: 0x0400A67C RID: 42620
				public static LocString UNDERWAY_TOOLTIP = "There's a party going on";

				// Token: 0x0400A67D RID: 42621
				public static LocString ACCEPT_OPTION_NAME = "Allow the party to happen";

				// Token: 0x0400A67E RID: 42622
				public static LocString ACCEPT_OPTION_DESC = "Party goers will get {goodEffect}";

				// Token: 0x0400A67F RID: 42623
				public static LocString ACCEPT_OPTION_INVALID_TOOLTIP = "A cake must be built for this event to take place.";

				// Token: 0x0400A680 RID: 42624
				public static LocString REJECT_OPTION_NAME = "Cancel the party";

				// Token: 0x0400A681 RID: 42625
				public static LocString REJECT_OPTION_DESC = "{host} and {dupe} gain {badEffect}";
			}

			// Token: 0x020025DA RID: 9690
			public class ECLIPSE
			{
				// Token: 0x0400A682 RID: 42626
				public static LocString NAME = "Eclipse";

				// Token: 0x0400A683 RID: 42627
				public static LocString DESCRIPTION = "A celestial object has obscured the sunlight";
			}

			// Token: 0x020025DB RID: 9691
			public class SOLAR_FLARE
			{
				// Token: 0x0400A684 RID: 42628
				public static LocString NAME = "Solar Storm";

				// Token: 0x0400A685 RID: 42629
				public static LocString DESCRIPTION = "A solar flare is headed this way";
			}

			// Token: 0x020025DC RID: 9692
			public class CREATURE_SPAWN
			{
				// Token: 0x0400A686 RID: 42630
				public static LocString NAME = "Critter Infestation";

				// Token: 0x0400A687 RID: 42631
				public static LocString DESCRIPTION = "There was a massive influx of destructive critters";
			}

			// Token: 0x020025DD RID: 9693
			public class SATELLITE_CRASH
			{
				// Token: 0x0400A688 RID: 42632
				public static LocString NAME = "Satellite Crash";

				// Token: 0x0400A689 RID: 42633
				public static LocString DESCRIPTION = "Mysterious space junk has crashed into the surface.\n\nIt may contain useful resources or information, but it may also be dangerous. Approach with caution.";
			}

			// Token: 0x020025DE RID: 9694
			public class FOOD_FIGHT
			{
				// Token: 0x0400A68A RID: 42634
				public static LocString NAME = "Food Fight";

				// Token: 0x0400A68B RID: 42635
				public static LocString DESCRIPTION = "Duplicants will throw food at each other for recreation\n\nIt may be wasteful, but everyone who participates will benefit from a major stress reduction.";

				// Token: 0x0400A68C RID: 42636
				public static LocString UNDERWAY = "Food Fight";

				// Token: 0x0400A68D RID: 42637
				public static LocString UNDERWAY_TOOLTIP = "There is a food fight happening now";

				// Token: 0x0400A68E RID: 42638
				public static LocString ACCEPT_OPTION_NAME = "Dupes start preparing to fight.";

				// Token: 0x0400A68F RID: 42639
				public static LocString ACCEPT_OPTION_DETAILS = "(Plus morale)";

				// Token: 0x0400A690 RID: 42640
				public static LocString REJECT_OPTION_NAME = "No food fight today";

				// Token: 0x0400A691 RID: 42641
				public static LocString REJECT_OPTION_DETAILS = "Sadface";
			}

			// Token: 0x020025DF RID: 9695
			public class PLANT_BLIGHT
			{
				// Token: 0x0400A692 RID: 42642
				public static LocString NAME = "Plant Blight: {plant}";

				// Token: 0x0400A693 RID: 42643
				public static LocString DESCRIPTION = "Our {plant} crops have been afflicted by a fungal sickness!\n\nI must get the Duplicants to uproot and compost the sick plants to save our farms.";

				// Token: 0x0400A694 RID: 42644
				public static LocString SUCCESS = "Blight Managed: {plant}";

				// Token: 0x0400A695 RID: 42645
				public static LocString SUCCESS_TOOLTIP = "All the blighted {plant} plants have been dealt with, halting the infection.";
			}

			// Token: 0x020025E0 RID: 9696
			public class CRYOFRIEND
			{
				// Token: 0x0400A696 RID: 42646
				public static LocString NAME = "New Event: A Frozen Friend";

				// Token: 0x0400A697 RID: 42647
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"{dupe} has made an amazing discovery! A barely working ",
					BUILDINGS.PREFABS.CRYOTANK.NAME,
					" has been uncovered containing a {friend} inside in a frozen state.\n\n{dupe} was successful in thawing {friend} and this encounter has filled both Duplicants with a sense of hope, something they will desperately need to keep their ",
					UI.FormatAsLink("Morale", "MORALE"),
					" up when facing the dangers ahead."
				});

				// Token: 0x0400A698 RID: 42648
				public static LocString BUTTON = "{friend} is thawed!";
			}

			// Token: 0x020025E1 RID: 9697
			public class WARPWORLDREVEAL
			{
				// Token: 0x0400A699 RID: 42649
				public static LocString NAME = "New Event: Personnel Teleporter";

				// Token: 0x0400A69A RID: 42650
				public static LocString DESCRIPTION = "I've discovered a functioning teleportation device with a pre-programmed destination.\n\nIt appears to go to another " + UI.CLUSTERMAP.PLANETOID + ", and I'm fairly certain there's a return device on the other end.\n\nI could send a Duplicant through safely if I desired.";

				// Token: 0x0400A69B RID: 42651
				public static LocString BUTTON = "See Destination";
			}

			// Token: 0x020025E2 RID: 9698
			public class ARTIFACT_REVEAL
			{
				// Token: 0x0400A69C RID: 42652
				public static LocString NAME = "New Event: Artifact Analyzed";

				// Token: 0x0400A69D RID: 42653
				public static LocString DESCRIPTION = "An artifact from a past civilization was analyzed.\n\n{desc}";

				// Token: 0x0400A69E RID: 42654
				public static LocString BUTTON = "Close";
			}
		}

		// Token: 0x02001CC3 RID: 7363
		public class BONUS
		{
			// Token: 0x020025E3 RID: 9699
			public class BONUSDREAM1
			{
				// Token: 0x0400A69F RID: 42655
				public static LocString NAME = "Good Dream";

				// Token: 0x0400A6A0 RID: 42656
				public static LocString DESCRIPTION = "I've observed many improvements to {dupe}'s demeanor today. Analysis indicates unusually high amounts of dopamine in their system. There's a good chance this is due to an exceptionally good dream and analysis indicates that current sleeping conditions may have contributed to this occurrence.\n\nFurther improvements to sleeping conditions may have additional positive effects to the " + UI.FormatAsLink("Morale", "MORALE") + " of {dupe} and other Duplicants.";

				// Token: 0x0400A6A1 RID: 42657
				public static LocString CHAIN_TOOLTIP = "Improving the living conditions of {dupe} will lead to more good dreams.";
			}

			// Token: 0x020025E4 RID: 9700
			public class BONUSDREAM2
			{
				// Token: 0x0400A6A2 RID: 42658
				public static LocString NAME = "Really Good Dream";

				// Token: 0x0400A6A3 RID: 42659
				public static LocString DESCRIPTION = "{dupe} had another really good dream and the resulting release of dopamine has made this Duplicant energetic and full of possibilities! This is an encouraging byproduct of improving the living conditions of the colony.\n\nBased on these observations, building a better sleeping area for my Duplicants will have a similar effect on their " + UI.FormatAsLink("Morale", "MORALE") + ".";
			}

			// Token: 0x020025E5 RID: 9701
			public class BONUSDREAM3
			{
				// Token: 0x0400A6A4 RID: 42660
				public static LocString NAME = "Great Dream";

				// Token: 0x0400A6A5 RID: 42661
				public static LocString DESCRIPTION = "I have detected a distinct spring in {dupe}'s step today. There is a good chance that this Duplicant had another great dream last night. Such incidents are further indications that working on the care and comfort of the colony is not a waste of time.\n\nI do wonder though: What do Duplicants dream of?";
			}

			// Token: 0x020025E6 RID: 9702
			public class BONUSDREAM4
			{
				// Token: 0x0400A6A6 RID: 42662
				public static LocString NAME = "Amazing Dream";

				// Token: 0x0400A6A7 RID: 42663
				public static LocString DESCRIPTION = "{dupe}'s dream last night must have been simply amazing! Their dopamine levels are at an all time high. Based on these results, it can be safely assumed that improving the living conditions of my Duplicants will reduce " + UI.FormatAsLink("Stress", "STRESS") + " and have similar positive effects on their well-being.\n\nObservations such as this are an integral and enjoyable part of science. When I see my Duplicants happy, I can't help but share in some of their joy.";
			}

			// Token: 0x020025E7 RID: 9703
			public class BONUSTOILET1
			{
				// Token: 0x0400A6A8 RID: 42664
				public static LocString NAME = "Small Comforts";

				// Token: 0x0400A6A9 RID: 42665
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"{dupe} recently visited an Outhouse and appears to have appreciated the small comforts based on the marked increase to their ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nHigh ",
					UI.FormatAsLink("Morale", "MORALE"),
					" has been linked to a better work ethic and greater enthusiasm for complex jobs, which are essential in building a successful new colony."
				});
			}

			// Token: 0x020025E8 RID: 9704
			public class BONUSTOILET2
			{
				// Token: 0x0400A6AA RID: 42666
				public static LocString NAME = "Greater Comforts";

				// Token: 0x0400A6AB RID: 42667
				public static LocString DESCRIPTION = "{dupe} used a Lavatory and analysis shows a decided improvement to this Duplicant's " + UI.FormatAsLink("Morale", "MORALE") + ".\n\nAs my colony grows and expands, it's important not to ignore the benefits of giving my Duplicants a pleasant place to relieve themselves.";
			}

			// Token: 0x020025E9 RID: 9705
			public class BONUSTOILET3
			{
				// Token: 0x0400A6AC RID: 42668
				public static LocString NAME = "Small Luxury";

				// Token: 0x0400A6AD RID: 42669
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"{dupe} visited a ",
					ROOMS.TYPES.LATRINE.NAME,
					" and experienced luxury unlike they anything this Duplicant had previously experienced as analysis has revealed yet another boost to their ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nIt is unclear whether this development is a result of increased hygiene or whether there is something else inherently about working plumbing which would improve ",
					UI.FormatAsLink("Morale", "MORALE"),
					" in this way. Further analysis is needed."
				});
			}

			// Token: 0x020025EA RID: 9706
			public class BONUSTOILET4
			{
				// Token: 0x0400A6AE RID: 42670
				public static LocString NAME = "Greater Luxury";

				// Token: 0x0400A6AF RID: 42671
				public static LocString DESCRIPTION = "{dupe} visited a Washroom and the experience has left this Duplicant with significantly improved " + UI.FormatAsLink("Morale", "MORALE") + ". Analysis indicates this improvement should continue for many cycles.\n\nThe relationship of my Duplicants and their surroundings is an interesting aspect of colony life. I should continue to watch future developments in this department closely.";
			}

			// Token: 0x020025EB RID: 9707
			public class BONUSRESEARCH
			{
				// Token: 0x0400A6B0 RID: 42672
				public static LocString NAME = "Inspired Learner";

				// Token: 0x0400A6B1 RID: 42673
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"Analysis indicates that the appearance of a ",
					UI.PRE_KEYWORD,
					"Research Station",
					UI.PST_KEYWORD,
					" has inspired {dupe} and heightened their brain activity on a cellular level.\n\nBrain stimulation is important if my Duplicants are going to adapt and innovate in their increasingly harsh environment."
				});
			}

			// Token: 0x020025EC RID: 9708
			public class BONUSDIGGING1
			{
				// Token: 0x0400A6B2 RID: 42674
				public static LocString NAME = "Hot Diggity!";

				// Token: 0x0400A6B3 RID: 42675
				public static LocString DESCRIPTION = "Some interesting data has revealed that {dupe} has had a marked increase in physical abilities, an increase that cannot entirely be attributed to the usual improvements that occur after regular physical activity.\n\nBased on previous observations this Duplicant's positive associations with digging appear to account for this additional physical boost.\n\nThis would mean the personal preferences of my Duplicants are directly correlated to how hard they work. How interesting...";
			}

			// Token: 0x020025ED RID: 9709
			public class BONUSSTORAGE
			{
				// Token: 0x0400A6B4 RID: 42676
				public static LocString NAME = "Something in Store";

				// Token: 0x0400A6B5 RID: 42677
				public static LocString DESCRIPTION = "Data indicates that {dupe}'s activity in storing something in a Storage Bin has led to an increase in this Duplicant's physical strength as well as an overall improvement to their general demeanor.\n\nThere have been many studies connecting organization with an increase in well-being. It is possible this explains {dupe}'s " + UI.FormatAsLink("Morale", "MORALE") + " improvements.";
			}

			// Token: 0x020025EE RID: 9710
			public class BONUSBUILDER
			{
				// Token: 0x0400A6B6 RID: 42678
				public static LocString NAME = "Accomplished Builder";

				// Token: 0x0400A6B7 RID: 42679
				public static LocString DESCRIPTION = "{dupe} has been hard at work building many structures crucial to the future of the colony. It seems this activity has improved this Duplicant's budding construction and mechanical skills beyond what my models predicted.\n\nWhether this increase in ability is due to them learning new skills or simply gaining self-confidence I cannot say, but this unexpected development is a welcome surprise development.";
			}

			// Token: 0x020025EF RID: 9711
			public class BONUSOXYGEN
			{
				// Token: 0x0400A6B8 RID: 42680
				public static LocString NAME = "Fresh Air";

				// Token: 0x0400A6B9 RID: 42681
				public static LocString DESCRIPTION = "{dupe} is experiencing a sudden unexpected improvement to their physical prowess which appears to be a result of exposure to elevated levels of oxygen from passing by an Oxygen Diffuser.\n\nObservations such as this are important in documenting just how beneficial having access to oxygen is to my colony.";
			}

			// Token: 0x020025F0 RID: 9712
			public class BONUSALGAE
			{
				// Token: 0x0400A6BA RID: 42682
				public static LocString NAME = "Fresh Algae Smell";

				// Token: 0x0400A6BB RID: 42683
				public static LocString DESCRIPTION = "{dupe}'s recent proximity to an Algae Terrarium has left them feeling refreshed and exuberant and is correlated to an increase in their physical attributes. It is unclear whether these physical improvements came from the excess of oxygen or the invigorating smell of algae.\n\nIt's curious that I find myself nostalgic for the smell of algae growing in a lab. But how could this be...?";
			}

			// Token: 0x020025F1 RID: 9713
			public class BONUSGENERATOR
			{
				// Token: 0x0400A6BC RID: 42684
				public static LocString NAME = "Exercised";

				// Token: 0x0400A6BD RID: 42685
				public static LocString DESCRIPTION = "{dupe} ran in a Manual Generator and the physical activity appears to have given this Duplicant increased strength and sense of well-being.\n\nWhile not the primary reason for building Manual Generators, I am very pleased to see my Duplicants reaping the " + UI.FormatAsLink("Stress", "STRESS") + " relieving benefits to physical activity.";
			}

			// Token: 0x020025F2 RID: 9714
			public class BONUSDOOR
			{
				// Token: 0x0400A6BE RID: 42686
				public static LocString NAME = "Open and Shut";

				// Token: 0x0400A6BF RID: 42687
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"The act of closing a door has apparently lead to a decrease in the ",
					UI.FormatAsLink("Stress", "STRESS"),
					" levels of {dupe}, as well as decreased the exposure of this Duplicant to harmful ",
					UI.FormatAsLink("Germs", "GERMS"),
					".\n\nWhile it may be more efficient to group all my Duplicants together in common sleeping quarters, it's important to remember the mental benefits to privacy and space to express their individuality."
				});
			}

			// Token: 0x020025F3 RID: 9715
			public class BONUSHITTHEBOOKS
			{
				// Token: 0x0400A6C0 RID: 42688
				public static LocString NAME = "Hit the Books";

				// Token: 0x0400A6C1 RID: 42689
				public static LocString DESCRIPTION = "{dupe}'s recent Research errand has resulted in a significant increase to this Duplicant's brain activity. The discovery of newly found knowledge has given {dupe} an invigorating jolt of excitement.\n\nI am all too familiar with this feeling.";
			}

			// Token: 0x020025F4 RID: 9716
			public class BONUSLITWORKSPACE
			{
				// Token: 0x0400A6C2 RID: 42690
				public static LocString NAME = "Lit-erally Great";

				// Token: 0x0400A6C3 RID: 42691
				public static LocString DESCRIPTION = "{dupe}'s recent time in a well-lit area has greatly improved this Duplicant's ability to work with, and on, machinery.\n\nThis supports the prevailing theory that a well-lit workspace has many benefits beyond just improving my Duplicant's ability to see.";
			}

			// Token: 0x020025F5 RID: 9717
			public class BONUSTALKER
			{
				// Token: 0x0400A6C4 RID: 42692
				public static LocString NAME = "Big Small Talker";

				// Token: 0x0400A6C5 RID: 42693
				public static LocString DESCRIPTION = "{dupe}'s recent conversation with another Duplicant shows a correlation to improved serotonin and " + UI.FormatAsLink("Morale", "MORALE") + " levels in this Duplicant. It is very possible that small talk with a co-worker, however short and seemingly insignificant, will make my Duplicant's feel connected to the colony as a whole.\n\nAs the colony gets bigger and more sophisticated, I must ensure that the opportunity for such connections continue, for the good of my Duplicants' mental well being.";
			}
		}
	}
}
