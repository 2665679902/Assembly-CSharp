using System;

namespace STRINGS
{
	// Token: 0x02000D3E RID: 3390
	public class ROOMS
	{
		// Token: 0x02001CC4 RID: 7364
		public class CATEGORY
		{
			// Token: 0x020025F6 RID: 9718
			public class NONE
			{
				// Token: 0x0400A6C6 RID: 42694
				public static LocString NAME = "None";
			}

			// Token: 0x020025F7 RID: 9719
			public class FOOD
			{
				// Token: 0x0400A6C7 RID: 42695
				public static LocString NAME = "Dining";
			}

			// Token: 0x020025F8 RID: 9720
			public class SLEEP
			{
				// Token: 0x0400A6C8 RID: 42696
				public static LocString NAME = "Sleep";
			}

			// Token: 0x020025F9 RID: 9721
			public class RECREATION
			{
				// Token: 0x0400A6C9 RID: 42697
				public static LocString NAME = "Recreation";
			}

			// Token: 0x020025FA RID: 9722
			public class BATHROOM
			{
				// Token: 0x0400A6CA RID: 42698
				public static LocString NAME = "Washroom";
			}

			// Token: 0x020025FB RID: 9723
			public class HOSPITAL
			{
				// Token: 0x0400A6CB RID: 42699
				public static LocString NAME = "Medical";
			}

			// Token: 0x020025FC RID: 9724
			public class INDUSTRIAL
			{
				// Token: 0x0400A6CC RID: 42700
				public static LocString NAME = "Industrial";
			}

			// Token: 0x020025FD RID: 9725
			public class AGRICULTURAL
			{
				// Token: 0x0400A6CD RID: 42701
				public static LocString NAME = "Agriculture";
			}

			// Token: 0x020025FE RID: 9726
			public class PARK
			{
				// Token: 0x0400A6CE RID: 42702
				public static LocString NAME = "Parks";
			}

			// Token: 0x020025FF RID: 9727
			public class SCIENCE
			{
				// Token: 0x0400A6CF RID: 42703
				public static LocString NAME = "Science";
			}
		}

		// Token: 0x02001CC5 RID: 7365
		public class TYPES
		{
			// Token: 0x04008375 RID: 33653
			public static LocString CONFLICTED = "Conflicted Room";

			// Token: 0x02002600 RID: 9728
			public class NEUTRAL
			{
				// Token: 0x0400A6D0 RID: 42704
				public static LocString NAME = "Miscellaneous Room";

				// Token: 0x0400A6D1 RID: 42705
				public static LocString DESCRIPTION = "An enclosed space with plenty of potential and no dedicated use.";

				// Token: 0x0400A6D2 RID: 42706
				public static LocString EFFECT = "- No effect";

				// Token: 0x0400A6D3 RID: 42707
				public static LocString TOOLTIP = "This area has walls and doors but no dedicated use";
			}

			// Token: 0x02002601 RID: 9729
			public class LATRINE
			{
				// Token: 0x0400A6D4 RID: 42708
				public static LocString NAME = "Latrine";

				// Token: 0x0400A6D5 RID: 42709
				public static LocString DESCRIPTION = "It's a step up from doing one's business in full view of the rest of the colony.\n\nUsing a toilet in an enclosed room will improve Duplicants' Morale.";

				// Token: 0x0400A6D6 RID: 42710
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400A6D7 RID: 42711
				public static LocString TOOLTIP = "Using a toilet in an enclosed room will improve Duplicants' Morale";
			}

			// Token: 0x02002602 RID: 9730
			public class PLUMBEDBATHROOM
			{
				// Token: 0x0400A6D8 RID: 42712
				public static LocString NAME = "Washroom";

				// Token: 0x0400A6D9 RID: 42713
				public static LocString DESCRIPTION = "A sanctuary of personal hygiene.\n\nUsing a fully plumbed Washroom will improve Duplicants' Morale.";

				// Token: 0x0400A6DA RID: 42714
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400A6DB RID: 42715
				public static LocString TOOLTIP = "Using a fully plumbed Washroom will improve Duplicants' Morale";
			}

			// Token: 0x02002603 RID: 9731
			public class BARRACKS
			{
				// Token: 0x0400A6DC RID: 42716
				public static LocString NAME = "Barracks";

				// Token: 0x0400A6DD RID: 42717
				public static LocString DESCRIPTION = "A basic communal sleeping area for up-and-coming colonies.\n\nSleeping in Barracks will improve Duplicants' Morale.";

				// Token: 0x0400A6DE RID: 42718
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400A6DF RID: 42719
				public static LocString TOOLTIP = "Sleeping in Barracks will improve Duplicants' Morale";
			}

			// Token: 0x02002604 RID: 9732
			public class BEDROOM
			{
				// Token: 0x0400A6E0 RID: 42720
				public static LocString NAME = "Luxury Barracks";

				// Token: 0x0400A6E1 RID: 42721
				public static LocString DESCRIPTION = "An upscale communal sleeping area full of things that greatly enhance quality of rest for occupants.\n\nSleeping in a Luxury Barracks will improve Duplicants' Morale.";

				// Token: 0x0400A6E2 RID: 42722
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400A6E3 RID: 42723
				public static LocString TOOLTIP = "Sleeping in a Luxury Barracks will improve Duplicants' Morale";
			}

			// Token: 0x02002605 RID: 9733
			public class PRIVATE_BEDROOM
			{
				// Token: 0x0400A6E4 RID: 42724
				public static LocString NAME = "Private Bedroom";

				// Token: 0x0400A6E5 RID: 42725
				public static LocString DESCRIPTION = "A comfortable, roommate-free retreat where tired Duplicants can get uninterrupted rest.\n\nSleeping in a Private Bedroom will greatly improve Duplicants' Morale.";

				// Token: 0x0400A6E6 RID: 42726
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400A6E7 RID: 42727
				public static LocString TOOLTIP = "Sleeping in a Private Bedroom will greatly improve Duplicants' Morale";
			}

			// Token: 0x02002606 RID: 9734
			public class MESSHALL
			{
				// Token: 0x0400A6E8 RID: 42728
				public static LocString NAME = "Mess Hall";

				// Token: 0x0400A6E9 RID: 42729
				public static LocString DESCRIPTION = "A simple dining room setup that's easy to improve upon.\n\nEating at a mess table in a Mess Hall will increase Duplicants' Morale.";

				// Token: 0x0400A6EA RID: 42730
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400A6EB RID: 42731
				public static LocString TOOLTIP = "Eating at a Mess Table in a Mess Hall will improve Duplicants' Morale";
			}

			// Token: 0x02002607 RID: 9735
			public class KITCHEN
			{
				// Token: 0x0400A6EC RID: 42732
				public static LocString NAME = "Kitchen";

				// Token: 0x0400A6ED RID: 42733
				public static LocString DESCRIPTION = "A cooking area equipped to take meals to the next level.\n\nAdding ingredients from a Spice Grinder to foods cooked on an Electric Grill or Gas Range provides a variety of positive benefits.";

				// Token: 0x0400A6EE RID: 42734
				public static LocString EFFECT = "- Enables Spice Grinder use";

				// Token: 0x0400A6EF RID: 42735
				public static LocString TOOLTIP = "Using a Spice Grinder in a Kitchen adds benefits to foods cooked on Electric Grill or Gas Range";
			}

			// Token: 0x02002608 RID: 9736
			public class GREATHALL
			{
				// Token: 0x0400A6F0 RID: 42736
				public static LocString NAME = "Great Hall";

				// Token: 0x0400A6F1 RID: 42737
				public static LocString DESCRIPTION = "A great place to eat, with great decor and great company. Great!\n\nEating in a Great Hall will significantly improve Duplicants' Morale.";

				// Token: 0x0400A6F2 RID: 42738
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400A6F3 RID: 42739
				public static LocString TOOLTIP = "Eating in a Great Hall will significantly improve Duplicants' Morale";
			}

			// Token: 0x02002609 RID: 9737
			public class HOSPITAL
			{
				// Token: 0x0400A6F4 RID: 42740
				public static LocString NAME = "Hospital";

				// Token: 0x0400A6F5 RID: 42741
				public static LocString DESCRIPTION = "A dedicated medical facility that helps minimize recovery time.\n\nSick Duplicants assigned to medical buildings located within a Hospital are also less likely to spread Disease.";

				// Token: 0x0400A6F6 RID: 42742
				public static LocString EFFECT = "- Quarantine sick Duplicants";

				// Token: 0x0400A6F7 RID: 42743
				public static LocString TOOLTIP = "Sick Duplicants assigned to medical buildings located within a Hospital are less likely to spread Disease";
			}

			// Token: 0x0200260A RID: 9738
			public class MASSAGE_CLINIC
			{
				// Token: 0x0400A6F8 RID: 42744
				public static LocString NAME = "Massage Clinic";

				// Token: 0x0400A6F9 RID: 42745
				public static LocString DESCRIPTION = "A soothing space with a very relaxing ambience, especially when well-decorated.\n\nReceiving massages at a Massage Clinic will significantly improve Stress reduction.";

				// Token: 0x0400A6FA RID: 42746
				public static LocString EFFECT = "- Massage stress relief bonus";

				// Token: 0x0400A6FB RID: 42747
				public static LocString TOOLTIP = "Receiving massages at a Massage Clinic will significantly improve Stress reduction";
			}

			// Token: 0x0200260B RID: 9739
			public class POWER_PLANT
			{
				// Token: 0x0400A6FC RID: 42748
				public static LocString NAME = "Power Plant";

				// Token: 0x0400A6FD RID: 42749
				public static LocString DESCRIPTION = "The perfect place for Duplicants to flex their Electrical Engineering skills.\n\nGenerators built within a Power Plant can be tuned up using power control stations to improve their power production.";

				// Token: 0x0400A6FE RID: 42750
				public static LocString EFFECT = "- Enables Power Control Station use";

				// Token: 0x0400A6FF RID: 42751
				public static LocString TOOLTIP = "Generators built within a Power Plant can be tuned up using Power Control Stations to improve their power production";
			}

			// Token: 0x0200260C RID: 9740
			public class MACHINE_SHOP
			{
				// Token: 0x0400A700 RID: 42752
				public static LocString NAME = "Machine Shop";

				// Token: 0x0400A701 RID: 42753
				public static LocString DESCRIPTION = "It smells like elbow grease.\n\nDuplicants working in a Machine Shop can maintain buildings and increase their production speed.";

				// Token: 0x0400A702 RID: 42754
				public static LocString EFFECT = "- Increased fabrication efficiency";

				// Token: 0x0400A703 RID: 42755
				public static LocString TOOLTIP = "Duplicants working in a Machine Shop can maintain buildings and increase their production speed";
			}

			// Token: 0x0200260D RID: 9741
			public class FARM
			{
				// Token: 0x0400A704 RID: 42756
				public static LocString NAME = "Greenhouse";

				// Token: 0x0400A705 RID: 42757
				public static LocString DESCRIPTION = "An enclosed agricultural space best utilized by Duplicants with Crop Tending skills.\n\nCrops grown within a Greenhouse can be tended with Farm Station fertilizer to increase their growth speed.";

				// Token: 0x0400A706 RID: 42758
				public static LocString EFFECT = "- Enables Farm Station use";

				// Token: 0x0400A707 RID: 42759
				public static LocString TOOLTIP = "Crops grown within a Greenhouse can be tended with Farm Station fertilizer to increase their growth speed";
			}

			// Token: 0x0200260E RID: 9742
			public class CREATUREPEN
			{
				// Token: 0x0400A708 RID: 42760
				public static LocString NAME = "Stable";

				// Token: 0x0400A709 RID: 42761
				public static LocString DESCRIPTION = "Critters don't mind it here, as long as things don't get too overcrowded.\n\nStabled critters can be tended at a Grooming Station to hasten their domestication and increase their production.";

				// Token: 0x0400A70A RID: 42762
				public static LocString EFFECT = "- Enables Grooming Station use";

				// Token: 0x0400A70B RID: 42763
				public static LocString TOOLTIP = "Stabled critters can be tended at a Grooming Station to hasten their domestication and increase their production";
			}

			// Token: 0x0200260F RID: 9743
			public class REC_ROOM
			{
				// Token: 0x0400A70C RID: 42764
				public static LocString NAME = "Recreation Room";

				// Token: 0x0400A70D RID: 42765
				public static LocString DESCRIPTION = "Where Duplicants go to mingle with off-duty peers and indulge in a little R&R.\n\nScheduled Downtime will further improve Morale for Duplicants visiting a Recreation Room.";

				// Token: 0x0400A70E RID: 42766
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400A70F RID: 42767
				public static LocString TOOLTIP = "Scheduled Downtime will further improve Morale for Duplicants visiting a Recreation Room";
			}

			// Token: 0x02002610 RID: 9744
			public class PARK
			{
				// Token: 0x0400A710 RID: 42768
				public static LocString NAME = "Park";

				// Token: 0x0400A711 RID: 42769
				public static LocString DESCRIPTION = "A little greenery goes a long way.\n\nPassing through natural spaces throughout the day will raise the Morale of Duplicants.";

				// Token: 0x0400A712 RID: 42770
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400A713 RID: 42771
				public static LocString TOOLTIP = "Passing through natural spaces throughout the day will raise the Morale of Duplicants";
			}

			// Token: 0x02002611 RID: 9745
			public class NATURERESERVE
			{
				// Token: 0x0400A714 RID: 42772
				public static LocString NAME = "Nature Reserve";

				// Token: 0x0400A715 RID: 42773
				public static LocString DESCRIPTION = "A lot of greenery goes an even longer way.\n\nPassing through a Nature Reserve will grant higher Morale bonuses to Duplicants than a Park.";

				// Token: 0x0400A716 RID: 42774
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400A717 RID: 42775
				public static LocString TOOLTIP = "A Nature Reserve will grant higher Morale bonuses to Duplicants than a Park";
			}

			// Token: 0x02002612 RID: 9746
			public class LABORATORY
			{
				// Token: 0x0400A718 RID: 42776
				public static LocString NAME = "Laboratory";

				// Token: 0x0400A719 RID: 42777
				public static LocString DESCRIPTION = "Where wild hypotheses meet rigorous scientific experimentation.\n\nScience stations built in a Laboratory function more efficiently.\n\nA Laboratory enables the use of the Geotuner and the Mission Control Station.";

				// Token: 0x0400A71A RID: 42778
				public static LocString EFFECT = "- Efficiency bonus";

				// Token: 0x0400A71B RID: 42779
				public static LocString TOOLTIP = "Science buildings built in a Laboratory function more efficiently\n\nA Laboratory enables Geotuner and Mission Control Station use";
			}

			// Token: 0x02002613 RID: 9747
			public class PRIVATE_BATHROOM
			{
				// Token: 0x0400A71C RID: 42780
				public static LocString NAME = "Private Bathroom";

				// Token: 0x0400A71D RID: 42781
				public static LocString DESCRIPTION = "Finally, a place to truly be alone with one's thoughts.\n\nDuplicants relieve even more Stress when using the toilet in a Private Bathroom than in a Latrine.";

				// Token: 0x0400A71E RID: 42782
				public static LocString EFFECT = "- Stress relief bonus";

				// Token: 0x0400A71F RID: 42783
				public static LocString TOOLTIP = "Duplicants relieve even more stress when using the toilet in a Private Bathroom than in a Latrine";
			}
		}

		// Token: 0x02001CC6 RID: 7366
		public class CRITERIA
		{
			// Token: 0x04008376 RID: 33654
			public static LocString HEADER = "<b>Requirements:</b>";

			// Token: 0x04008377 RID: 33655
			public static LocString NEUTRAL_TYPE = "Enclosed by wall tile";

			// Token: 0x04008378 RID: 33656
			public static LocString POSSIBLE_TYPES_HEADER = "Possible Room Types";

			// Token: 0x04008379 RID: 33657
			public static LocString NO_TYPE_CONFLICTS = "Remove conflicting buildings";

			// Token: 0x0400837A RID: 33658
			public static LocString DECOR_ITEM_CLASS = "Decor Item";

			// Token: 0x02002614 RID: 9748
			public class CRITERIA_FAILED
			{
				// Token: 0x0400A720 RID: 42784
				public static LocString MISSING_BUILDING = "Missing {0}";

				// Token: 0x0400A721 RID: 42785
				public static LocString FAILED = "{0}";
			}

			// Token: 0x02002615 RID: 9749
			public class CEILING_HEIGHT
			{
				// Token: 0x0400A722 RID: 42786
				public static LocString NAME = "Minimum height: {0} tiles";

				// Token: 0x0400A723 RID: 42787
				public static LocString DESCRIPTION = "Must have a ceiling height of at least {0} tiles";
			}

			// Token: 0x02002616 RID: 9750
			public class MINIMUM_SIZE
			{
				// Token: 0x0400A724 RID: 42788
				public static LocString NAME = "Minimum size: {0} tiles";

				// Token: 0x0400A725 RID: 42789
				public static LocString DESCRIPTION = "Must have an area of at least {0} tiles";
			}

			// Token: 0x02002617 RID: 9751
			public class MAXIMUM_SIZE
			{
				// Token: 0x0400A726 RID: 42790
				public static LocString NAME = "Maximum size: {0} tiles";

				// Token: 0x0400A727 RID: 42791
				public static LocString DESCRIPTION = "Must have an area no larger than {0} tiles";
			}

			// Token: 0x02002618 RID: 9752
			public class HAS_BED
			{
				// Token: 0x0400A728 RID: 42792
				public static LocString NAME = "One or more beds";

				// Token: 0x0400A729 RID: 42793
				public static LocString DESCRIPTION = "Requires at least one Cot or Comfy Bed";
			}

			// Token: 0x02002619 RID: 9753
			public class HAS_LUXURY_BED
			{
				// Token: 0x0400A72A RID: 42794
				public static LocString NAME = "One or more Comfy Beds";

				// Token: 0x0400A72B RID: 42795
				public static LocString DESCRIPTION = "Requires at least one Comfy Bed";
			}

			// Token: 0x0200261A RID: 9754
			public class LUXURY_BED_SINGLE
			{
				// Token: 0x0400A72C RID: 42796
				public static LocString NAME = "Single Comfy Bed";

				// Token: 0x0400A72D RID: 42797
				public static LocString DESCRIPTION = "Must have no more than one Comfy Bed";
			}

			// Token: 0x0200261B RID: 9755
			public class BED_SINGLE
			{
				// Token: 0x0400A72E RID: 42798
				public static LocString NAME = "Single bed";

				// Token: 0x0400A72F RID: 42799
				public static LocString DESCRIPTION = "Must have no more than one Cot or Comfy Bed";
			}

			// Token: 0x0200261C RID: 9756
			public class IS_BACKWALLED
			{
				// Token: 0x0400A730 RID: 42800
				public static LocString NAME = "Has backwall tiles";

				// Token: 0x0400A731 RID: 42801
				public static LocString DESCRIPTION = "Must be covered in backwall tiles";
			}

			// Token: 0x0200261D RID: 9757
			public class NO_COTS
			{
				// Token: 0x0400A732 RID: 42802
				public static LocString NAME = "No Cots";

				// Token: 0x0400A733 RID: 42803
				public static LocString DESCRIPTION = "Room cannot contain a Cot";
			}

			// Token: 0x0200261E RID: 9758
			public class NO_LUXURY_BEDS
			{
				// Token: 0x0400A734 RID: 42804
				public static LocString NAME = "No Comfy Beds";

				// Token: 0x0400A735 RID: 42805
				public static LocString DESCRIPTION = "Room cannot contain a Comfy Bed";
			}

			// Token: 0x0200261F RID: 9759
			public class BED_MULTIPLE
			{
				// Token: 0x0400A736 RID: 42806
				public static LocString NAME = "Beds";

				// Token: 0x0400A737 RID: 42807
				public static LocString DESCRIPTION = "Requires two or more Cots or Comfy Beds";
			}

			// Token: 0x02002620 RID: 9760
			public class BUILDING_DECOR_POSITIVE
			{
				// Token: 0x0400A738 RID: 42808
				public static LocString NAME = "Positive decor";

				// Token: 0x0400A739 RID: 42809
				public static LocString DESCRIPTION = "Requires at least one building with positive decor";
			}

			// Token: 0x02002621 RID: 9761
			public class DECORATIVE_ITEM
			{
				// Token: 0x0400A73A RID: 42810
				public static LocString NAME = "Decor item ({0})";

				// Token: 0x0400A73B RID: 42811
				public static LocString DESCRIPTION = "Requires {0} or more Decor items";
			}

			// Token: 0x02002622 RID: 9762
			public class DECORATIVE_ITEM_N
			{
				// Token: 0x0400A73C RID: 42812
				public static LocString NAME = "Decor item: +{0} Decor";

				// Token: 0x0400A73D RID: 42813
				public static LocString DESCRIPTION = "Requires a decorative item with a minimum Decor value of {0}";
			}

			// Token: 0x02002623 RID: 9763
			public class CLINIC
			{
				// Token: 0x0400A73E RID: 42814
				public static LocString NAME = "Medical equipment";

				// Token: 0x0400A73F RID: 42815
				public static LocString DESCRIPTION = "Requires one or more Sick Bays or Disease Clinics";
			}

			// Token: 0x02002624 RID: 9764
			public class POWER_STATION
			{
				// Token: 0x0400A740 RID: 42816
				public static LocString NAME = "Power Control Station";

				// Token: 0x0400A741 RID: 42817
				public static LocString DESCRIPTION = "Requires a single Power Control Station";
			}

			// Token: 0x02002625 RID: 9765
			public class FARM_STATION
			{
				// Token: 0x0400A742 RID: 42818
				public static LocString NAME = "Farm Station";

				// Token: 0x0400A743 RID: 42819
				public static LocString DESCRIPTION = "Requires a single Farm Station";
			}

			// Token: 0x02002626 RID: 9766
			public class CREATURE_RELOCATOR
			{
				// Token: 0x0400A744 RID: 42820
				public static LocString NAME = "Critter Relocator";

				// Token: 0x0400A745 RID: 42821
				public static LocString DESCRIPTION = "Requires a single Critter Drop-Off";
			}

			// Token: 0x02002627 RID: 9767
			public class CREATURE_FEEDER
			{
				// Token: 0x0400A746 RID: 42822
				public static LocString NAME = "Critter Feeder";

				// Token: 0x0400A747 RID: 42823
				public static LocString DESCRIPTION = "Requires a single Critter Feeder";
			}

			// Token: 0x02002628 RID: 9768
			public class RANCH_STATION
			{
				// Token: 0x0400A748 RID: 42824
				public static LocString NAME = "Grooming Station";

				// Token: 0x0400A749 RID: 42825
				public static LocString DESCRIPTION = "Requires a single Grooming Station";
			}

			// Token: 0x02002629 RID: 9769
			public class SPICE_STATION
			{
				// Token: 0x0400A74A RID: 42826
				public static LocString NAME = "Spice Grinder";

				// Token: 0x0400A74B RID: 42827
				public static LocString DESCRIPTION = "Requires a single Spice Grinder";
			}

			// Token: 0x0200262A RID: 9770
			public class COOK_TOP
			{
				// Token: 0x0400A74C RID: 42828
				public static LocString NAME = "Electric Grill or Gas Range";

				// Token: 0x0400A74D RID: 42829
				public static LocString DESCRIPTION = "Requires a single Electric Grill or Gas Range";
			}

			// Token: 0x0200262B RID: 9771
			public class REFRIGERATOR
			{
				// Token: 0x0400A74E RID: 42830
				public static LocString NAME = "Refrigerator";

				// Token: 0x0400A74F RID: 42831
				public static LocString DESCRIPTION = "Requires a single Refrigerator";
			}

			// Token: 0x0200262C RID: 9772
			public class REC_BUILDING
			{
				// Token: 0x0400A750 RID: 42832
				public static LocString NAME = "Recreational building";

				// Token: 0x0400A751 RID: 42833
				public static LocString DESCRIPTION = "Requires one or more recreational buildings";
			}

			// Token: 0x0200262D RID: 9773
			public class PARK_BUILDING
			{
				// Token: 0x0400A752 RID: 42834
				public static LocString NAME = "Park Sign";

				// Token: 0x0400A753 RID: 42835
				public static LocString DESCRIPTION = "Requires one or more Park Signs";
			}

			// Token: 0x0200262E RID: 9774
			public class MACHINE_SHOP
			{
				// Token: 0x0400A754 RID: 42836
				public static LocString NAME = "Mechanics Station";

				// Token: 0x0400A755 RID: 42837
				public static LocString DESCRIPTION = "Requires requires one or more Mechanics Stations";
			}

			// Token: 0x0200262F RID: 9775
			public class FOOD_BOX
			{
				// Token: 0x0400A756 RID: 42838
				public static LocString NAME = "Food storage";

				// Token: 0x0400A757 RID: 42839
				public static LocString DESCRIPTION = "Requires one or more Ration Boxes or Refrigerators";
			}

			// Token: 0x02002630 RID: 9776
			public class LIGHT
			{
				// Token: 0x0400A758 RID: 42840
				public static LocString NAME = "Light source";

				// Token: 0x0400A759 RID: 42841
				public static LocString DESCRIPTION = "Requires one or more light sources";
			}

			// Token: 0x02002631 RID: 9777
			public class DESTRESSING_BUILDING
			{
				// Token: 0x0400A75A RID: 42842
				public static LocString NAME = "De-Stressing Building";

				// Token: 0x0400A75B RID: 42843
				public static LocString DESCRIPTION = "Requires one or more De-Stressing Building";
			}

			// Token: 0x02002632 RID: 9778
			public class MASSAGE_TABLE
			{
				// Token: 0x0400A75C RID: 42844
				public static LocString NAME = "Massage Table";

				// Token: 0x0400A75D RID: 42845
				public static LocString DESCRIPTION = "Requires one or more Massage Tables";
			}

			// Token: 0x02002633 RID: 9779
			public class MESS_STATION_SINGLE
			{
				// Token: 0x0400A75E RID: 42846
				public static LocString NAME = "Mess Table";

				// Token: 0x0400A75F RID: 42847
				public static LocString DESCRIPTION = "Requires a single Mess Table";
			}

			// Token: 0x02002634 RID: 9780
			public class NO_MESS_STATION
			{
				// Token: 0x0400A760 RID: 42848
				public static LocString NAME = "No Mess Table";

				// Token: 0x0400A761 RID: 42849
				public static LocString DESCRIPTION = "Cannot contain a Mess Table";
			}

			// Token: 0x02002635 RID: 9781
			public class MESS_STATION_MULTIPLE
			{
				// Token: 0x0400A762 RID: 42850
				public static LocString NAME = "Mess Tables";

				// Token: 0x0400A763 RID: 42851
				public static LocString DESCRIPTION = "Requires two or more Mess Tables";
			}

			// Token: 0x02002636 RID: 9782
			public class RESEARCH_STATION
			{
				// Token: 0x0400A764 RID: 42852
				public static LocString NAME = "Research station";

				// Token: 0x0400A765 RID: 42853
				public static LocString DESCRIPTION = "Requires one or more Research Stations or Super Computers";
			}

			// Token: 0x02002637 RID: 9783
			public class TOILET
			{
				// Token: 0x0400A766 RID: 42854
				public static LocString NAME = "Toilet";

				// Token: 0x0400A767 RID: 42855
				public static LocString DESCRIPTION = "Requires one or more Outhouses or Lavatories";
			}

			// Token: 0x02002638 RID: 9784
			public class FLUSH_TOILET
			{
				// Token: 0x0400A768 RID: 42856
				public static LocString NAME = "Flush Toilet";

				// Token: 0x0400A769 RID: 42857
				public static LocString DESCRIPTION = "Requires one or more Lavatories";
			}

			// Token: 0x02002639 RID: 9785
			public class NO_OUTHOUSES
			{
				// Token: 0x0400A76A RID: 42858
				public static LocString NAME = "No Outhouses";

				// Token: 0x0400A76B RID: 42859
				public static LocString DESCRIPTION = "Cannot contain basic Outhouses";
			}

			// Token: 0x0200263A RID: 9786
			public class WASH_STATION
			{
				// Token: 0x0400A76C RID: 42860
				public static LocString NAME = "Wash station";

				// Token: 0x0400A76D RID: 42861
				public static LocString DESCRIPTION = "Requires one or more Wash Basins, Sinks, Hand Sanitizers, or Showers";
			}

			// Token: 0x0200263B RID: 9787
			public class ADVANCED_WASH_STATION
			{
				// Token: 0x0400A76E RID: 42862
				public static LocString NAME = "Plumbed wash station";

				// Token: 0x0400A76F RID: 42863
				public static LocString DESCRIPTION = "Requires one or more Sinks, Hand Sanitizers, or Showers";
			}

			// Token: 0x0200263C RID: 9788
			public class NO_INDUSTRIAL_MACHINERY
			{
				// Token: 0x0400A770 RID: 42864
				public static LocString NAME = "No industrial machinery";

				// Token: 0x0400A771 RID: 42865
				public static LocString DESCRIPTION = "Cannot contain any building labeled Industrial Machinery";
			}

			// Token: 0x0200263D RID: 9789
			public class WILDANIMAL
			{
				// Token: 0x0400A772 RID: 42866
				public static LocString NAME = "Wildlife";

				// Token: 0x0400A773 RID: 42867
				public static LocString DESCRIPTION = "Requires at least one wild critter";
			}

			// Token: 0x0200263E RID: 9790
			public class WILDANIMALS
			{
				// Token: 0x0400A774 RID: 42868
				public static LocString NAME = "More wildlife";

				// Token: 0x0400A775 RID: 42869
				public static LocString DESCRIPTION = "Requires two or more wild critters";
			}

			// Token: 0x0200263F RID: 9791
			public class WILDPLANT
			{
				// Token: 0x0400A776 RID: 42870
				public static LocString NAME = "Two wild plants";

				// Token: 0x0400A777 RID: 42871
				public static LocString DESCRIPTION = "Requires two or more wild plants";
			}

			// Token: 0x02002640 RID: 9792
			public class WILDPLANTS
			{
				// Token: 0x0400A778 RID: 42872
				public static LocString NAME = "Four wild plants";

				// Token: 0x0400A779 RID: 42873
				public static LocString DESCRIPTION = "Requires four or more wild plants";
			}

			// Token: 0x02002641 RID: 9793
			public class SCIENCE_BUILDING
			{
				// Token: 0x0400A77A RID: 42874
				public static LocString NAME = "Science building";

				// Token: 0x0400A77B RID: 42875
				public static LocString DESCRIPTION = "Requires one or more science buildings";
			}

			// Token: 0x02002642 RID: 9794
			public class SCIENCE_BUILDINGS
			{
				// Token: 0x0400A77C RID: 42876
				public static LocString NAME = "Two science buildings";

				// Token: 0x0400A77D RID: 42877
				public static LocString DESCRIPTION = "Requires two or more science buildings";
			}
		}

		// Token: 0x02001CC7 RID: 7367
		public class DETAILS
		{
			// Token: 0x0400837B RID: 33659
			public static LocString HEADER = "Room Details";

			// Token: 0x02002643 RID: 9795
			public class ASSIGNED_TO
			{
				// Token: 0x0400A77E RID: 42878
				public static LocString NAME = "<b>Assignments:</b>\n{0}";

				// Token: 0x0400A77F RID: 42879
				public static LocString UNASSIGNED = "Unassigned";
			}

			// Token: 0x02002644 RID: 9796
			public class AVERAGE_TEMPERATURE
			{
				// Token: 0x0400A780 RID: 42880
				public static LocString NAME = "Average temperature: {0}";
			}

			// Token: 0x02002645 RID: 9797
			public class AVERAGE_ATMO_MASS
			{
				// Token: 0x0400A781 RID: 42881
				public static LocString NAME = "Average air pressure: {0}";
			}

			// Token: 0x02002646 RID: 9798
			public class SIZE
			{
				// Token: 0x0400A782 RID: 42882
				public static LocString NAME = "Room size: {0} Tiles";
			}

			// Token: 0x02002647 RID: 9799
			public class BUILDING_COUNT
			{
				// Token: 0x0400A783 RID: 42883
				public static LocString NAME = "Buildings: {0}";
			}

			// Token: 0x02002648 RID: 9800
			public class CREATURE_COUNT
			{
				// Token: 0x0400A784 RID: 42884
				public static LocString NAME = "Critters: {0}";
			}

			// Token: 0x02002649 RID: 9801
			public class PLANT_COUNT
			{
				// Token: 0x0400A785 RID: 42885
				public static LocString NAME = "Plants: {0}";
			}
		}

		// Token: 0x02001CC8 RID: 7368
		public class EFFECTS
		{
			// Token: 0x0400837C RID: 33660
			public static LocString HEADER = "<b>Effects:</b>";
		}
	}
}
