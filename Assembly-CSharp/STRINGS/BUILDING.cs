using System;

namespace STRINGS
{
	// Token: 0x02000D3F RID: 3391
	public class BUILDING
	{
		// Token: 0x02001CC9 RID: 7369
		public class STATUSITEMS
		{
			// Token: 0x0200264A RID: 9802
			public class GEOTUNER_NEEDGEYSER
			{
				// Token: 0x0400A786 RID: 42886
				public static LocString NAME = "No Geyser Selected";

				// Token: 0x0400A787 RID: 42887
				public static LocString TOOLTIP = "Select an analyzed geyser to increase its output";
			}

			// Token: 0x0200264B RID: 9803
			public class GEOTUNER_CHARGE_REQUIRED
			{
				// Token: 0x0400A788 RID: 42888
				public static LocString NAME = "Experimentation Needed";

				// Token: 0x0400A789 RID: 42889
				public static LocString TOOLTIP = "This building requires a Duplicant to produce amplification data through experimentation";
			}

			// Token: 0x0200264C RID: 9804
			public class GEOTUNER_CHARGING
			{
				// Token: 0x0400A78A RID: 42890
				public static LocString NAME = "Compiling Data";

				// Token: 0x0400A78B RID: 42891
				public static LocString TOOLTIP = "Compiling amplification data through experimentation";
			}

			// Token: 0x0200264D RID: 9805
			public class GEOTUNER_CHARGED
			{
				// Token: 0x0400A78C RID: 42892
				public static LocString NAME = "Data Remaining: {0}";

				// Token: 0x0400A78D RID: 42893
				public static LocString TOOLTIP = "This building consumes amplification data while boosting a geyser\n\nTime remaining: {0} ({1} data per second)";
			}

			// Token: 0x0200264E RID: 9806
			public class GEOTUNER_GEYSER_STATUS
			{
				// Token: 0x0400A78E RID: 42894
				public static LocString NAME = "";

				// Token: 0x0400A78F RID: 42895
				public static LocString NAME_ERUPTING = "Target is Erupting";

				// Token: 0x0400A790 RID: 42896
				public static LocString NAME_DORMANT = "Target is Not Erupting";

				// Token: 0x0400A791 RID: 42897
				public static LocString NAME_IDLE = "Target is Not Erupting";

				// Token: 0x0400A792 RID: 42898
				public static LocString TOOLTIP = "";

				// Token: 0x0400A793 RID: 42899
				public static LocString TOOLTIP_ERUPTING = "The selected geyser is erupting and will receive stored amplification data";

				// Token: 0x0400A794 RID: 42900
				public static LocString TOOLTIP_DORMANT = "The selected geyser is not erupting\n\nIt will not receive stored amplification data in this state";

				// Token: 0x0400A795 RID: 42901
				public static LocString TOOLTIP_IDLE = "The selected geyser is not erupting\n\nIt will not receive stored amplification data in this state";
			}

			// Token: 0x0200264F RID: 9807
			public class GEYSER_GEOTUNED
			{
				// Token: 0x0400A796 RID: 42902
				public static LocString NAME = "Geotuned ({0}/{1})";

				// Token: 0x0400A797 RID: 42903
				public static LocString TOOLTIP = "This geyser is being boosted by {0} out {1} of " + UI.PRE_KEYWORD + "Geotuners" + UI.PST_KEYWORD;
			}

			// Token: 0x02002650 RID: 9808
			public class RADIATOR_ENERGY_CURRENT_EMISSION_RATE
			{
				// Token: 0x0400A798 RID: 42904
				public static LocString NAME = "Currently Emitting: {ENERGY_RATE}";

				// Token: 0x0400A799 RID: 42905
				public static LocString TOOLTIP = "Currently Emitting: {ENERGY_RATE}";
			}

			// Token: 0x02002651 RID: 9809
			public class NOTLINKEDTOHEAD
			{
				// Token: 0x0400A79A RID: 42906
				public static LocString NAME = "Not Linked";

				// Token: 0x0400A79B RID: 42907
				public static LocString TOOLTIP = "This building must be built adjacent to a {headBuilding} or another {linkBuilding} in order to function";
			}

			// Token: 0x02002652 RID: 9810
			public class BAITED
			{
				// Token: 0x0400A79C RID: 42908
				public static LocString NAME = "{0} Bait";

				// Token: 0x0400A79D RID: 42909
				public static LocString TOOLTIP = "This lure is baited with {0}\n\nBait material is set during the construction of the building";
			}

			// Token: 0x02002653 RID: 9811
			public class NOCOOLANT
			{
				// Token: 0x0400A79E RID: 42910
				public static LocString NAME = "No Coolant";

				// Token: 0x0400A79F RID: 42911
				public static LocString TOOLTIP = "This building needs coolant";
			}

			// Token: 0x02002654 RID: 9812
			public class ANGERDAMAGE
			{
				// Token: 0x0400A7A0 RID: 42912
				public static LocString NAME = "Damage: Duplicant Tantrum";

				// Token: 0x0400A7A1 RID: 42913
				public static LocString TOOLTIP = "A stressed Duplicant is damaging this building";

				// Token: 0x0400A7A2 RID: 42914
				public static LocString NOTIFICATION = "Building Damage: Duplicant Tantrum";

				// Token: 0x0400A7A3 RID: 42915
				public static LocString NOTIFICATION_TOOLTIP = "Stressed Duplicants are damaging these buildings:\n\n{0}";
			}

			// Token: 0x02002655 RID: 9813
			public class PIPECONTENTS
			{
				// Token: 0x0400A7A4 RID: 42916
				public static LocString EMPTY = "Empty";

				// Token: 0x0400A7A5 RID: 42917
				public static LocString CONTENTS = "{0} of {1} at {2}";

				// Token: 0x0400A7A6 RID: 42918
				public static LocString CONTENTS_WITH_DISEASE = "\n  {0}";
			}

			// Token: 0x02002656 RID: 9814
			public class CONVEYOR_CONTENTS
			{
				// Token: 0x0400A7A7 RID: 42919
				public static LocString EMPTY = "Empty";

				// Token: 0x0400A7A8 RID: 42920
				public static LocString CONTENTS = "{0} of {1} at {2}";

				// Token: 0x0400A7A9 RID: 42921
				public static LocString CONTENTS_WITH_DISEASE = "\n  {0}";
			}

			// Token: 0x02002657 RID: 9815
			public class ASSIGNEDTO
			{
				// Token: 0x0400A7AA RID: 42922
				public static LocString NAME = "Assigned to: {Assignee}";

				// Token: 0x0400A7AB RID: 42923
				public static LocString TOOLTIP = "Only {Assignee} can use this amenity";
			}

			// Token: 0x02002658 RID: 9816
			public class ASSIGNEDPUBLIC
			{
				// Token: 0x0400A7AC RID: 42924
				public static LocString NAME = "Assigned to: Public";

				// Token: 0x0400A7AD RID: 42925
				public static LocString TOOLTIP = "Any Duplicant can use this amenity";
			}

			// Token: 0x02002659 RID: 9817
			public class ASSIGNEDTOROOM
			{
				// Token: 0x0400A7AE RID: 42926
				public static LocString NAME = "Assigned to: {0}";

				// Token: 0x0400A7AF RID: 42927
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Any Duplicant assigned to this ",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					" can use this amenity"
				});
			}

			// Token: 0x0200265A RID: 9818
			public class AWAITINGSEEDDELIVERY
			{
				// Token: 0x0400A7B0 RID: 42928
				public static LocString NAME = "Awaiting Delivery";

				// Token: 0x0400A7B1 RID: 42929
				public static LocString TOOLTIP = "Awaiting delivery of selected " + UI.PRE_KEYWORD + "Seed" + UI.PST_KEYWORD;
			}

			// Token: 0x0200265B RID: 9819
			public class AWAITINGBAITDELIVERY
			{
				// Token: 0x0400A7B2 RID: 42930
				public static LocString NAME = "Awaiting Bait";

				// Token: 0x0400A7B3 RID: 42931
				public static LocString TOOLTIP = "Awaiting delivery of selected " + UI.PRE_KEYWORD + "Bait" + UI.PST_KEYWORD;
			}

			// Token: 0x0200265C RID: 9820
			public class CLINICOUTSIDEHOSPITAL
			{
				// Token: 0x0400A7B4 RID: 42932
				public static LocString NAME = "Medical building outside Hospital";

				// Token: 0x0400A7B5 RID: 42933
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Rebuild this medical equipment in a ",
					UI.PRE_KEYWORD,
					"Hospital",
					UI.PST_KEYWORD,
					" to more effectively quarantine sick Duplicants"
				});
			}

			// Token: 0x0200265D RID: 9821
			public class BOTTLE_EMPTIER
			{
				// Token: 0x02002F47 RID: 12103
				public static class ALLOWED
				{
					// Token: 0x0400BDEE RID: 48622
					public static LocString NAME = "Auto-Bottle: On";

					// Token: 0x0400BDEF RID: 48623
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"Duplicants may specifically fetch ",
						UI.PRE_KEYWORD,
						"Liquid",
						UI.PST_KEYWORD,
						" from a ",
						BUILDINGS.PREFABS.LIQUIDPUMPINGSTATION.NAME,
						" to bring to this location"
					});
				}

				// Token: 0x02002F48 RID: 12104
				public static class DENIED
				{
					// Token: 0x0400BDF0 RID: 48624
					public static LocString NAME = "Auto-Bottle: Off";

					// Token: 0x0400BDF1 RID: 48625
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"Duplicants may not specifically fetch ",
						UI.PRE_KEYWORD,
						"Liquid",
						UI.PST_KEYWORD,
						" from a ",
						BUILDINGS.PREFABS.LIQUIDPUMPINGSTATION.NAME,
						" to bring to this location"
					});
				}
			}

			// Token: 0x0200265E RID: 9822
			public class CANISTER_EMPTIER
			{
				// Token: 0x02002F49 RID: 12105
				public static class ALLOWED
				{
					// Token: 0x0400BDF2 RID: 48626
					public static LocString NAME = "Auto-Bottle: On";

					// Token: 0x0400BDF3 RID: 48627
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"Duplicants may specifically fetch ",
						UI.PRE_KEYWORD,
						"Gas",
						UI.PST_KEYWORD,
						" from a ",
						BUILDINGS.PREFABS.GASBOTTLER.NAME,
						" to bring to this location"
					});
				}

				// Token: 0x02002F4A RID: 12106
				public static class DENIED
				{
					// Token: 0x0400BDF4 RID: 48628
					public static LocString NAME = "Auto-Bottle: Off";

					// Token: 0x0400BDF5 RID: 48629
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"Duplicants may not specifically fetch ",
						UI.PRE_KEYWORD,
						"Gas",
						UI.PST_KEYWORD,
						" from a ",
						BUILDINGS.PREFABS.GASBOTTLER.NAME,
						" to bring to this location"
					});
				}
			}

			// Token: 0x0200265F RID: 9823
			public class BROKEN
			{
				// Token: 0x0400A7B6 RID: 42934
				public static LocString NAME = "Broken";

				// Token: 0x0400A7B7 RID: 42935
				public static LocString TOOLTIP = "This building received damage from <b>{DamageInfo}</b>\n\nIt will not function until it receives repairs";
			}

			// Token: 0x02002660 RID: 9824
			public class CHANGEDOORCONTROLSTATE
			{
				// Token: 0x0400A7B8 RID: 42936
				public static LocString NAME = "Pending Door State Change: {ControlState}";

				// Token: 0x0400A7B9 RID: 42937
				public static LocString TOOLTIP = "Waiting for a Duplicant to change control state";
			}

			// Token: 0x02002661 RID: 9825
			public class DISPENSEREQUESTED
			{
				// Token: 0x0400A7BA RID: 42938
				public static LocString NAME = "Dispense Requested";

				// Token: 0x0400A7BB RID: 42939
				public static LocString TOOLTIP = "Waiting for a Duplicant to dispense the item";
			}

			// Token: 0x02002662 RID: 9826
			public class SUIT_LOCKER
			{
				// Token: 0x02002F4B RID: 12107
				public class NEED_CONFIGURATION
				{
					// Token: 0x0400BDF6 RID: 48630
					public static LocString NAME = "Current Status: Needs Configuration";

					// Token: 0x0400BDF7 RID: 48631
					public static LocString TOOLTIP = "Set this dock to store a suit or leave it empty";
				}

				// Token: 0x02002F4C RID: 12108
				public class READY
				{
					// Token: 0x0400BDF8 RID: 48632
					public static LocString NAME = "Current Status: Empty";

					// Token: 0x0400BDF9 RID: 48633
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"This dock is ready to receive a ",
						UI.PRE_KEYWORD,
						"Suit",
						UI.PST_KEYWORD,
						", either by manual delivery or from a Duplicant returning the suit they're wearing"
					});
				}

				// Token: 0x02002F4D RID: 12109
				public class SUIT_REQUESTED
				{
					// Token: 0x0400BDFA RID: 48634
					public static LocString NAME = "Current Status: Awaiting Delivery";

					// Token: 0x0400BDFB RID: 48635
					public static LocString TOOLTIP = "Waiting for a Duplicant to deliver a " + UI.PRE_KEYWORD + "Suit" + UI.PST_KEYWORD;
				}

				// Token: 0x02002F4E RID: 12110
				public class CHARGING
				{
					// Token: 0x0400BDFC RID: 48636
					public static LocString NAME = "Current Status: Charging Suit";

					// Token: 0x0400BDFD RID: 48637
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"This ",
						UI.PRE_KEYWORD,
						"Suit",
						UI.PST_KEYWORD,
						" is docked and refueling"
					});
				}

				// Token: 0x02002F4F RID: 12111
				public class NO_OXYGEN
				{
					// Token: 0x0400BDFE RID: 48638
					public static LocString NAME = "Current Status: No Oxygen";

					// Token: 0x0400BDFF RID: 48639
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"This dock does not contain enough ",
						ELEMENTS.OXYGEN.NAME,
						" to refill a ",
						UI.PRE_KEYWORD,
						"Suit",
						UI.PST_KEYWORD
					});
				}

				// Token: 0x02002F50 RID: 12112
				public class NO_FUEL
				{
					// Token: 0x0400BE00 RID: 48640
					public static LocString NAME = "Current Status: No Fuel";

					// Token: 0x0400BE01 RID: 48641
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"This dock does not contain enough ",
						ELEMENTS.PETROLEUM.NAME,
						" to refill a ",
						UI.PRE_KEYWORD,
						"Suit",
						UI.PST_KEYWORD
					});
				}

				// Token: 0x02002F51 RID: 12113
				public class NO_COOLANT
				{
					// Token: 0x0400BE02 RID: 48642
					public static LocString NAME = "Current Status: No Coolant";

					// Token: 0x0400BE03 RID: 48643
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"This dock does not contain enough ",
						ELEMENTS.WATER.NAME,
						" to refill a ",
						UI.PRE_KEYWORD,
						"Suit",
						UI.PST_KEYWORD
					});
				}

				// Token: 0x02002F52 RID: 12114
				public class NOT_OPERATIONAL
				{
					// Token: 0x0400BE04 RID: 48644
					public static LocString NAME = "Current Status: Offline";

					// Token: 0x0400BE05 RID: 48645
					public static LocString TOOLTIP = "This dock requires " + UI.PRE_KEYWORD + "Power" + UI.PST_KEYWORD;
				}

				// Token: 0x02002F53 RID: 12115
				public class FULLY_CHARGED
				{
					// Token: 0x0400BE06 RID: 48646
					public static LocString NAME = "Current Status: Full Fueled";

					// Token: 0x0400BE07 RID: 48647
					public static LocString TOOLTIP = "This suit is fully refueled and ready for use";
				}
			}

			// Token: 0x02002663 RID: 9827
			public class SUITMARKERTRAVERSALONLYWHENROOMAVAILABLE
			{
				// Token: 0x0400A7BC RID: 42940
				public static LocString NAME = "Clearance: Vacancy Only";

				// Token: 0x0400A7BD RID: 42941
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Suited Duplicants may pass only if there is room in a ",
					UI.PRE_KEYWORD,
					"Dock",
					UI.PST_KEYWORD,
					" to store their ",
					UI.PRE_KEYWORD,
					"Suit",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002664 RID: 9828
			public class SUITMARKERTRAVERSALANYTIME
			{
				// Token: 0x0400A7BE RID: 42942
				public static LocString NAME = "Clearance: Always Permitted";

				// Token: 0x0400A7BF RID: 42943
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Suited Duplicants may pass even if there is no room to store their ",
					UI.PRE_KEYWORD,
					"Suits",
					UI.PST_KEYWORD,
					"\n\nWhen all available docks are full, Duplicants will unequip their ",
					UI.PRE_KEYWORD,
					"Suits",
					UI.PST_KEYWORD,
					" and drop them on the floor"
				});
			}

			// Token: 0x02002665 RID: 9829
			public class SUIT_LOCKER_NEEDS_CONFIGURATION
			{
				// Token: 0x0400A7C0 RID: 42944
				public static LocString NAME = "Not Configured";

				// Token: 0x0400A7C1 RID: 42945
				public static LocString TOOLTIP = "Dock settings not configured";
			}

			// Token: 0x02002666 RID: 9830
			public class CURRENTDOORCONTROLSTATE
			{
				// Token: 0x0400A7C2 RID: 42946
				public static LocString NAME = "Current State: {ControlState}";

				// Token: 0x0400A7C3 RID: 42947
				public static LocString TOOLTIP = "Current State: {ControlState}\n\nAuto: Duplicants open and close this door as needed\nLocked: Nothing may pass through\nOpen: This door will remain open";

				// Token: 0x0400A7C4 RID: 42948
				public static LocString OPENED = "Opened";

				// Token: 0x0400A7C5 RID: 42949
				public static LocString AUTO = "Auto";

				// Token: 0x0400A7C6 RID: 42950
				public static LocString LOCKED = "Locked";
			}

			// Token: 0x02002667 RID: 9831
			public class CONDUITBLOCKED
			{
				// Token: 0x0400A7C7 RID: 42951
				public static LocString NAME = "Pipe Blocked";

				// Token: 0x0400A7C8 RID: 42952
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Output ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" is blocked"
				});
			}

			// Token: 0x02002668 RID: 9832
			public class OUTPUTTILEBLOCKED
			{
				// Token: 0x0400A7C9 RID: 42953
				public static LocString NAME = "Output Blocked";

				// Token: 0x0400A7CA RID: 42954
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Output ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" is blocked"
				});
			}

			// Token: 0x02002669 RID: 9833
			public class CONDUITBLOCKEDMULTIPLES
			{
				// Token: 0x0400A7CB RID: 42955
				public static LocString NAME = "Pipe Blocked";

				// Token: 0x0400A7CC RID: 42956
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Output ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" is blocked"
				});
			}

			// Token: 0x0200266A RID: 9834
			public class SOLIDCONDUITBLOCKEDMULTIPLES
			{
				// Token: 0x0400A7CD RID: 42957
				public static LocString NAME = "Conveyor Rail Blocked";

				// Token: 0x0400A7CE RID: 42958
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Output ",
					UI.PRE_KEYWORD,
					"Conveyor Rail",
					UI.PST_KEYWORD,
					" is blocked"
				});
			}

			// Token: 0x0200266B RID: 9835
			public class OUTPUTPIPEFULL
			{
				// Token: 0x0400A7CF RID: 42959
				public static LocString NAME = "Output Pipe Full";

				// Token: 0x0400A7D0 RID: 42960
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Unable to flush contents, output ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" is blocked"
				});
			}

			// Token: 0x0200266C RID: 9836
			public class CONSTRUCTIONUNREACHABLE
			{
				// Token: 0x0400A7D1 RID: 42961
				public static LocString NAME = "Unreachable Build";

				// Token: 0x0400A7D2 RID: 42962
				public static LocString TOOLTIP = "Duplicants cannot reach this construction site";
			}

			// Token: 0x0200266D RID: 9837
			public class MOPUNREACHABLE
			{
				// Token: 0x0400A7D3 RID: 42963
				public static LocString NAME = "Unreachable Mop";

				// Token: 0x0400A7D4 RID: 42964
				public static LocString TOOLTIP = "Duplicants cannot reach this area";
			}

			// Token: 0x0200266E RID: 9838
			public class DEADREACTORCOOLINGOFF
			{
				// Token: 0x0400A7D5 RID: 42965
				public static LocString NAME = "Cooling ({CyclesRemaining} cycles remaining)";

				// Token: 0x0400A7D6 RID: 42966
				public static LocString TOOLTIP = "The radiation coming from this reactor is diminishing";
			}

			// Token: 0x0200266F RID: 9839
			public class DIGUNREACHABLE
			{
				// Token: 0x0400A7D7 RID: 42967
				public static LocString NAME = "Unreachable Dig";

				// Token: 0x0400A7D8 RID: 42968
				public static LocString TOOLTIP = "Duplicants cannot reach this area";
			}

			// Token: 0x02002670 RID: 9840
			public class STORAGEUNREACHABLE
			{
				// Token: 0x0400A7D9 RID: 42969
				public static LocString NAME = "Unreachable Storage";

				// Token: 0x0400A7DA RID: 42970
				public static LocString TOOLTIP = "Duplicants cannot reach this storage unit";
			}

			// Token: 0x02002671 RID: 9841
			public class PASSENGERMODULEUNREACHABLE
			{
				// Token: 0x0400A7DB RID: 42971
				public static LocString NAME = "Unreachable Module";

				// Token: 0x0400A7DC RID: 42972
				public static LocString TOOLTIP = "Duplicants cannot reach this rocket module";
			}

			// Token: 0x02002672 RID: 9842
			public class CONSTRUCTABLEDIGUNREACHABLE
			{
				// Token: 0x0400A7DD RID: 42973
				public static LocString NAME = "Unreachable Dig";

				// Token: 0x0400A7DE RID: 42974
				public static LocString TOOLTIP = "This construction site contains cells that cannot be dug out";
			}

			// Token: 0x02002673 RID: 9843
			public class EMPTYPUMPINGSTATION
			{
				// Token: 0x0400A7DF RID: 42975
				public static LocString NAME = "Empty";

				// Token: 0x0400A7E0 RID: 42976
				public static LocString TOOLTIP = "This pumping station cannot access any " + UI.PRE_KEYWORD + "Liquid" + UI.PST_KEYWORD;
			}

			// Token: 0x02002674 RID: 9844
			public class ENTOMBED
			{
				// Token: 0x0400A7E1 RID: 42977
				public static LocString NAME = "Entombed";

				// Token: 0x0400A7E2 RID: 42978
				public static LocString TOOLTIP = "Must be dug out by a Duplicant";

				// Token: 0x0400A7E3 RID: 42979
				public static LocString NOTIFICATION_NAME = "Building entombment";

				// Token: 0x0400A7E4 RID: 42980
				public static LocString NOTIFICATION_TOOLTIP = "These buildings are entombed and need to be dug out:";
			}

			// Token: 0x02002675 RID: 9845
			public class FABRICATORACCEPTSMUTANTSEEDS
			{
				// Token: 0x0400A7E5 RID: 42981
				public static LocString NAME = "Fabricator accepts mutant seeds";

				// Token: 0x0400A7E6 RID: 42982
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This fabricator is allowed to use ",
					UI.PRE_KEYWORD,
					"Mutant Seeds",
					UI.PST_KEYWORD,
					" as recipe ingredients"
				});
			}

			// Token: 0x02002676 RID: 9846
			public class FISHFEEDERACCEPTSMUTANTSEEDS
			{
				// Token: 0x0400A7E7 RID: 42983
				public static LocString NAME = "Fish Feeder accepts mutant seeds";

				// Token: 0x0400A7E8 RID: 42984
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This fish feeder is allowed to use ",
					UI.PRE_KEYWORD,
					"Mutant Seeds",
					UI.PST_KEYWORD,
					" as fish food"
				});
			}

			// Token: 0x02002677 RID: 9847
			public class INVALIDPORTOVERLAP
			{
				// Token: 0x0400A7E9 RID: 42985
				public static LocString NAME = "Invalid Port Overlap";

				// Token: 0x0400A7EA RID: 42986
				public static LocString TOOLTIP = "Ports on this building overlap those on another building\n\nThis building must be rebuilt in a valid location";

				// Token: 0x0400A7EB RID: 42987
				public static LocString NOTIFICATION_NAME = "Building has overlapping ports";

				// Token: 0x0400A7EC RID: 42988
				public static LocString NOTIFICATION_TOOLTIP = "These buildings must be rebuilt with non-overlapping ports:";
			}

			// Token: 0x02002678 RID: 9848
			public class GENESHUFFLECOMPLETED
			{
				// Token: 0x0400A7ED RID: 42989
				public static LocString NAME = "Vacillation Complete";

				// Token: 0x0400A7EE RID: 42990
				public static LocString TOOLTIP = "The Duplicant has completed the neural vacillation process and is ready to be released";
			}

			// Token: 0x02002679 RID: 9849
			public class OVERHEATED
			{
				// Token: 0x0400A7EF RID: 42991
				public static LocString NAME = "Damage: Overheating";

				// Token: 0x0400A7F0 RID: 42992
				public static LocString TOOLTIP = "This building is taking damage and will break down if not cooled";
			}

			// Token: 0x0200267A RID: 9850
			public class OVERLOADED
			{
				// Token: 0x0400A7F1 RID: 42993
				public static LocString NAME = "Damage: Overloading";

				// Token: 0x0400A7F2 RID: 42994
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Wire",
					UI.PST_KEYWORD,
					" is taking damage because there are too many buildings pulling ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" from this circuit\n\nSplit this ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" circuit into multiple circuits, or use higher quality ",
					UI.PRE_KEYWORD,
					"Wires",
					UI.PST_KEYWORD,
					" to prevent overloading"
				});
			}

			// Token: 0x0200267B RID: 9851
			public class LOGICOVERLOADED
			{
				// Token: 0x0400A7F3 RID: 42995
				public static LocString NAME = "Damage: Overloading";

				// Token: 0x0400A7F4 RID: 42996
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Logic Wire",
					UI.PST_KEYWORD,
					" is taking damage\n\nLimit the output to one Bit, or replace it with ",
					UI.PRE_KEYWORD,
					"Logic Ribbon",
					UI.PST_KEYWORD,
					" to prevent further damage"
				});
			}

			// Token: 0x0200267C RID: 9852
			public class OPERATINGENERGY
			{
				// Token: 0x0400A7F5 RID: 42997
				public static LocString NAME = "Heat Production: {0}/s";

				// Token: 0x0400A7F6 RID: 42998
				public static LocString TOOLTIP = "This building is producing <b>{0}</b> per second\n\nSources:\n{1}";

				// Token: 0x0400A7F7 RID: 42999
				public static LocString LINEITEM = "    • {0}: {1}\n";

				// Token: 0x0400A7F8 RID: 43000
				public static LocString OPERATING = "Normal operation";

				// Token: 0x0400A7F9 RID: 43001
				public static LocString EXHAUSTING = "Excess produced";

				// Token: 0x0400A7FA RID: 43002
				public static LocString PIPECONTENTS_TRANSFER = "Transferred from pipes";

				// Token: 0x0400A7FB RID: 43003
				public static LocString FOOD_TRANSFER = "Internal Cooling";
			}

			// Token: 0x0200267D RID: 9853
			public class FLOODED
			{
				// Token: 0x0400A7FC RID: 43004
				public static LocString NAME = "Building Flooded";

				// Token: 0x0400A7FD RID: 43005
				public static LocString TOOLTIP = "Building cannot function at current saturation";

				// Token: 0x0400A7FE RID: 43006
				public static LocString NOTIFICATION_NAME = "Flooding";

				// Token: 0x0400A7FF RID: 43007
				public static LocString NOTIFICATION_TOOLTIP = "These buildings are flooded:";
			}

			// Token: 0x0200267E RID: 9854
			public class GASVENTOBSTRUCTED
			{
				// Token: 0x0400A800 RID: 43008
				public static LocString NAME = "Gas Vent Obstructed";

				// Token: 0x0400A801 RID: 43009
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" has been obstructed and is preventing ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" flow to this vent"
				});
			}

			// Token: 0x0200267F RID: 9855
			public class GASVENTOVERPRESSURE
			{
				// Token: 0x0400A802 RID: 43010
				public static LocString NAME = "Gas Vent Overpressure";

				// Token: 0x0400A803 RID: 43011
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"High ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" or ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" pressure in this area is preventing further ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" emission\nReduce pressure by pumping ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" away or clearing more space"
				});
			}

			// Token: 0x02002680 RID: 9856
			public class DIRECTION_CONTROL
			{
				// Token: 0x0400A804 RID: 43012
				public static LocString NAME = "Use Direction: {Direction}";

				// Token: 0x0400A805 RID: 43013
				public static LocString TOOLTIP = "Duplicants will only use this building when walking by it\n\nCurrently allowed direction: <b>{Direction}</b>";

				// Token: 0x02002F54 RID: 12116
				public static class DIRECTIONS
				{
					// Token: 0x0400BE08 RID: 48648
					public static LocString LEFT = "Left";

					// Token: 0x0400BE09 RID: 48649
					public static LocString RIGHT = "Right";

					// Token: 0x0400BE0A RID: 48650
					public static LocString BOTH = "Both";
				}
			}

			// Token: 0x02002681 RID: 9857
			public class WATTSONGAMEOVER
			{
				// Token: 0x0400A806 RID: 43014
				public static LocString NAME = "Colony Lost";

				// Token: 0x0400A807 RID: 43015
				public static LocString TOOLTIP = "All Duplicants are dead or incapacitated";
			}

			// Token: 0x02002682 RID: 9858
			public class INVALIDBUILDINGLOCATION
			{
				// Token: 0x0400A808 RID: 43016
				public static LocString NAME = "Invalid Building Location";

				// Token: 0x0400A809 RID: 43017
				public static LocString TOOLTIP = "Cannot construct a building in this location";
			}

			// Token: 0x02002683 RID: 9859
			public class LIQUIDVENTOBSTRUCTED
			{
				// Token: 0x0400A80A RID: 43018
				public static LocString NAME = "Liquid Vent Obstructed";

				// Token: 0x0400A80B RID: 43019
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" has been obstructed and is preventing ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" flow to this vent"
				});
			}

			// Token: 0x02002684 RID: 9860
			public class LIQUIDVENTOVERPRESSURE
			{
				// Token: 0x0400A80C RID: 43020
				public static LocString NAME = "Liquid Vent Overpressure";

				// Token: 0x0400A80D RID: 43021
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"High ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" or ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" pressure in this area is preventing further ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" emission\nReduce pressure by pumping ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" away or clearing more space"
				});
			}

			// Token: 0x02002685 RID: 9861
			public class MANUALLYCONTROLLED
			{
				// Token: 0x0400A80E RID: 43022
				public static LocString NAME = "Manually Controlled";

				// Token: 0x0400A80F RID: 43023
				public static LocString TOOLTIP = "This Duplicant is under my control";
			}

			// Token: 0x02002686 RID: 9862
			public class LIMITVALVELIMITREACHED
			{
				// Token: 0x0400A810 RID: 43024
				public static LocString NAME = "Limit Reached";

				// Token: 0x0400A811 RID: 43025
				public static LocString TOOLTIP = "No more Mass can be transferred";
			}

			// Token: 0x02002687 RID: 9863
			public class LIMITVALVELIMITNOTREACHED
			{
				// Token: 0x0400A812 RID: 43026
				public static LocString NAME = "Amount remaining: {0}";

				// Token: 0x0400A813 RID: 43027
				public static LocString TOOLTIP = "This building will stop transferring Mass when the amount remaining reaches 0";
			}

			// Token: 0x02002688 RID: 9864
			public class MATERIALSUNAVAILABLE
			{
				// Token: 0x0400A814 RID: 43028
				public static LocString NAME = "Insufficient Resources\n{ItemsRemaining}";

				// Token: 0x0400A815 RID: 43029
				public static LocString TOOLTIP = "Crucial materials for this building are beyond reach or unavailable";

				// Token: 0x0400A816 RID: 43030
				public static LocString NOTIFICATION_NAME = "Building lacks resources";

				// Token: 0x0400A817 RID: 43031
				public static LocString NOTIFICATION_TOOLTIP = "Crucial materials are unavailable or beyond reach for these buildings:";

				// Token: 0x0400A818 RID: 43032
				public static LocString LINE_ITEM_MASS = "• {0}: {1}";

				// Token: 0x0400A819 RID: 43033
				public static LocString LINE_ITEM_UNITS = "• {0}";
			}

			// Token: 0x02002689 RID: 9865
			public class MATERIALSUNAVAILABLEFORREFILL
			{
				// Token: 0x0400A81A RID: 43034
				public static LocString NAME = "Resources Low\n{ItemsRemaining}";

				// Token: 0x0400A81B RID: 43035
				public static LocString TOOLTIP = "This building will soon require materials that are unavailable";

				// Token: 0x0400A81C RID: 43036
				public static LocString LINE_ITEM = "• {0}";
			}

			// Token: 0x0200268A RID: 9866
			public class MELTINGDOWN
			{
				// Token: 0x0400A81D RID: 43037
				public static LocString NAME = "Breaking Down";

				// Token: 0x0400A81E RID: 43038
				public static LocString TOOLTIP = "This building is collapsing";

				// Token: 0x0400A81F RID: 43039
				public static LocString NOTIFICATION_NAME = "Building breakdown";

				// Token: 0x0400A820 RID: 43040
				public static LocString NOTIFICATION_TOOLTIP = "These buildings are collapsing:";
			}

			// Token: 0x0200268B RID: 9867
			public class MISSINGFOUNDATION
			{
				// Token: 0x0400A821 RID: 43041
				public static LocString NAME = "Missing Tile";

				// Token: 0x0400A822 RID: 43042
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Build ",
					UI.PRE_KEYWORD,
					"Tile",
					UI.PST_KEYWORD,
					" beneath this building to regain function\n\nTile can be found in the ",
					UI.FormatAsBuildMenuTab("Base Tab", global::Action.Plan1),
					" of the Build Menu"
				});
			}

			// Token: 0x0200268C RID: 9868
			public class NEUTRONIUMUNMINABLE
			{
				// Token: 0x0400A823 RID: 43043
				public static LocString NAME = "Cannot Mine";

				// Token: 0x0400A824 RID: 43044
				public static LocString TOOLTIP = "This resource cannot be mined by Duplicant tools";
			}

			// Token: 0x0200268D RID: 9869
			public class NEEDGASIN
			{
				// Token: 0x0400A825 RID: 43045
				public static LocString NAME = "No Gas Intake\n{GasRequired}";

				// Token: 0x0400A826 RID: 43046
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building's ",
					UI.PRE_KEYWORD,
					"Gas Intake",
					UI.PST_KEYWORD,
					" does not have a ",
					BUILDINGS.PREFABS.GASCONDUIT.NAME,
					" connected"
				});

				// Token: 0x0400A827 RID: 43047
				public static LocString LINE_ITEM = "• {0}";
			}

			// Token: 0x0200268E RID: 9870
			public class NEEDGASOUT
			{
				// Token: 0x0400A828 RID: 43048
				public static LocString NAME = "No Gas Output";

				// Token: 0x0400A829 RID: 43049
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building's ",
					UI.PRE_KEYWORD,
					"Gas Output",
					UI.PST_KEYWORD,
					" does not have a ",
					BUILDINGS.PREFABS.GASCONDUIT.NAME,
					" connected"
				});
			}

			// Token: 0x0200268F RID: 9871
			public class NEEDLIQUIDIN
			{
				// Token: 0x0400A82A RID: 43050
				public static LocString NAME = "No Liquid Intake\n{LiquidRequired}";

				// Token: 0x0400A82B RID: 43051
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building's ",
					UI.PRE_KEYWORD,
					"Liquid Intake",
					UI.PST_KEYWORD,
					" does not have a ",
					BUILDINGS.PREFABS.LIQUIDCONDUIT.NAME,
					" connected"
				});

				// Token: 0x0400A82C RID: 43052
				public static LocString LINE_ITEM = "• {0}";
			}

			// Token: 0x02002690 RID: 9872
			public class NEEDLIQUIDOUT
			{
				// Token: 0x0400A82D RID: 43053
				public static LocString NAME = "No Liquid Output";

				// Token: 0x0400A82E RID: 43054
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building's ",
					UI.PRE_KEYWORD,
					"Liquid Output",
					UI.PST_KEYWORD,
					" does not have a ",
					BUILDINGS.PREFABS.LIQUIDCONDUIT.NAME,
					" connected"
				});
			}

			// Token: 0x02002691 RID: 9873
			public class LIQUIDPIPEEMPTY
			{
				// Token: 0x0400A82F RID: 43055
				public static LocString NAME = "Empty Pipe";

				// Token: 0x0400A830 RID: 43056
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"There is no ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" in this pipe"
				});
			}

			// Token: 0x02002692 RID: 9874
			public class LIQUIDPIPEOBSTRUCTED
			{
				// Token: 0x0400A831 RID: 43057
				public static LocString NAME = "Not Pumping";

				// Token: 0x0400A832 RID: 43058
				public static LocString TOOLTIP = "This pump is not active";
			}

			// Token: 0x02002693 RID: 9875
			public class GASPIPEEMPTY
			{
				// Token: 0x0400A833 RID: 43059
				public static LocString NAME = "Empty Pipe";

				// Token: 0x0400A834 RID: 43060
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"There is no ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" in this pipe"
				});
			}

			// Token: 0x02002694 RID: 9876
			public class GASPIPEOBSTRUCTED
			{
				// Token: 0x0400A835 RID: 43061
				public static LocString NAME = "Not Pumping";

				// Token: 0x0400A836 RID: 43062
				public static LocString TOOLTIP = "This pump is not active";
			}

			// Token: 0x02002695 RID: 9877
			public class NEEDSOLIDIN
			{
				// Token: 0x0400A837 RID: 43063
				public static LocString NAME = "No Conveyor Loader";

				// Token: 0x0400A838 RID: 43064
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Material cannot be fed onto this Conveyor system for transport\n\nEnter the ",
					UI.FormatAsBuildMenuTab("Shipping Tab", global::Action.Plan13),
					" of the Build Menu to build and connect a ",
					UI.PRE_KEYWORD,
					"Conveyor Loader",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002696 RID: 9878
			public class NEEDSOLIDOUT
			{
				// Token: 0x0400A839 RID: 43065
				public static LocString NAME = "No Conveyor Receptacle";

				// Token: 0x0400A83A RID: 43066
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Material cannot be offloaded from this Conveyor system and will backup the rails\n\nEnter the ",
					UI.FormatAsBuildMenuTab("Shipping Tab", global::Action.Plan13),
					" of the Build Menu to build and connect a ",
					UI.PRE_KEYWORD,
					"Conveyor Receptacle",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002697 RID: 9879
			public class SOLIDPIPEOBSTRUCTED
			{
				// Token: 0x0400A83B RID: 43067
				public static LocString NAME = "Conveyor Rail Backup";

				// Token: 0x0400A83C RID: 43068
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Conveyor Rail",
					UI.PST_KEYWORD,
					" cannot carry anymore material\n\nRemove material from the ",
					UI.PRE_KEYWORD,
					"Conveyor Receptacle",
					UI.PST_KEYWORD,
					" to free space for more objects"
				});
			}

			// Token: 0x02002698 RID: 9880
			public class NEEDPLANT
			{
				// Token: 0x0400A83D RID: 43069
				public static LocString NAME = "No Seeds";

				// Token: 0x0400A83E RID: 43070
				public static LocString TOOLTIP = "Uproot wild plants to obtain seeds";
			}

			// Token: 0x02002699 RID: 9881
			public class NEEDSEED
			{
				// Token: 0x0400A83F RID: 43071
				public static LocString NAME = "No Seed Selected";

				// Token: 0x0400A840 RID: 43072
				public static LocString TOOLTIP = "Uproot wild plants to obtain seeds";
			}

			// Token: 0x0200269A RID: 9882
			public class NEEDPOWER
			{
				// Token: 0x0400A841 RID: 43073
				public static LocString NAME = "No Power";

				// Token: 0x0400A842 RID: 43074
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"All connected ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" sources have lost charge"
				});
			}

			// Token: 0x0200269B RID: 9883
			public class NOTENOUGHPOWER
			{
				// Token: 0x0400A843 RID: 43075
				public static LocString NAME = "Insufficient Power";

				// Token: 0x0400A844 RID: 43076
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building does not have enough stored ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" to run"
				});
			}

			// Token: 0x0200269C RID: 9884
			public class POWERLOOPDETECTED
			{
				// Token: 0x0400A845 RID: 43077
				public static LocString NAME = "Power Loop Detected";

				// Token: 0x0400A846 RID: 43078
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A ",
					UI.PRE_KEYWORD,
					"Transformer's",
					UI.PST_KEYWORD,
					" ",
					UI.PRE_KEYWORD,
					"Power Output",
					UI.PST_KEYWORD,
					" has been connected back to its own ",
					UI.PRE_KEYWORD,
					"Input",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x0200269D RID: 9885
			public class NEEDRESOURCE
			{
				// Token: 0x0400A847 RID: 43079
				public static LocString NAME = "Resource Required";

				// Token: 0x0400A848 RID: 43080
				public static LocString TOOLTIP = "This building is missing required materials";
			}

			// Token: 0x0200269E RID: 9886
			public class NEWDUPLICANTSAVAILABLE
			{
				// Token: 0x0400A849 RID: 43081
				public static LocString NAME = "Printables Available";

				// Token: 0x0400A84A RID: 43082
				public static LocString TOOLTIP = "I am ready to print a new colony member or care package";

				// Token: 0x0400A84B RID: 43083
				public static LocString NOTIFICATION_NAME = "New Printables are available";

				// Token: 0x0400A84C RID: 43084
				public static LocString NOTIFICATION_TOOLTIP = "The Printing Pod " + UI.FormatAsHotKey(global::Action.Plan1) + " is ready to print a new Duplicant or care package.\nI'll need to select a blueprint:";
			}

			// Token: 0x0200269F RID: 9887
			public class NOAPPLICABLERESEARCHSELECTED
			{
				// Token: 0x0400A84D RID: 43085
				public static LocString NAME = "Inapplicable Research";

				// Token: 0x0400A84E RID: 43086
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building cannot produce the correct ",
					UI.PRE_KEYWORD,
					"Research Type",
					UI.PST_KEYWORD,
					" for the current ",
					UI.FormatAsLink("Research Focus", "TECH")
				});

				// Token: 0x0400A84F RID: 43087
				public static LocString NOTIFICATION_NAME = UI.FormatAsLink("Research Center", "ADVANCEDRESEARCHCENTER") + " idle";

				// Token: 0x0400A850 RID: 43088
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"These buildings cannot produce the correct ",
					UI.PRE_KEYWORD,
					"Research Type",
					UI.PST_KEYWORD,
					" for the selected ",
					UI.FormatAsLink("Research Focus", "TECH"),
					":"
				});
			}

			// Token: 0x020026A0 RID: 9888
			public class NOAPPLICABLEANALYSISSELECTED
			{
				// Token: 0x0400A851 RID: 43089
				public static LocString NAME = "No Analysis Focus Selected";

				// Token: 0x0400A852 RID: 43090
				public static LocString TOOLTIP = "Select an unknown destination from the " + UI.FormatAsManagementMenu("Starmap", global::Action.ManageStarmap) + " to begin analysis";

				// Token: 0x0400A853 RID: 43091
				public static LocString NOTIFICATION_NAME = UI.FormatAsLink("Telescope", "TELESCOPE") + " idle";

				// Token: 0x0400A854 RID: 43092
				public static LocString NOTIFICATION_TOOLTIP = "These buildings require an analysis focus:";
			}

			// Token: 0x020026A1 RID: 9889
			public class NOAVAILABLESEED
			{
				// Token: 0x0400A855 RID: 43093
				public static LocString NAME = "No Seed Available";

				// Token: 0x0400A856 RID: 43094
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The selected ",
					UI.PRE_KEYWORD,
					"Seed",
					UI.PST_KEYWORD,
					" is not available"
				});
			}

			// Token: 0x020026A2 RID: 9890
			public class NOSTORAGEFILTERSET
			{
				// Token: 0x0400A857 RID: 43095
				public static LocString NAME = "Filters Not Designated";

				// Token: 0x0400A858 RID: 43096
				public static LocString TOOLTIP = "No resources types are marked for storage in this building";
			}

			// Token: 0x020026A3 RID: 9891
			public class NOSUITMARKER
			{
				// Token: 0x0400A859 RID: 43097
				public static LocString NAME = "No Checkpoint";

				// Token: 0x0400A85A RID: 43098
				public static LocString TOOLTIP = "Docks must be placed beside a " + BUILDINGS.PREFABS.CHECKPOINT.NAME + ", opposite the side the checkpoint faces";
			}

			// Token: 0x020026A4 RID: 9892
			public class SUITMARKERWRONGSIDE
			{
				// Token: 0x0400A85B RID: 43099
				public static LocString NAME = "Invalid Checkpoint";

				// Token: 0x0400A85C RID: 43100
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building has been built on the wrong side of a ",
					BUILDINGS.PREFABS.CHECKPOINT.NAME,
					"\n\nDocks must be placed beside a ",
					BUILDINGS.PREFABS.CHECKPOINT.NAME,
					", opposite the side the checkpoint faces"
				});
			}

			// Token: 0x020026A5 RID: 9893
			public class NOFILTERELEMENTSELECTED
			{
				// Token: 0x0400A85D RID: 43101
				public static LocString NAME = "No Filter Selected";

				// Token: 0x0400A85E RID: 43102
				public static LocString TOOLTIP = "Select a resource to filter";
			}

			// Token: 0x020026A6 RID: 9894
			public class NOLUREELEMENTSELECTED
			{
				// Token: 0x0400A85F RID: 43103
				public static LocString NAME = "No Bait Selected";

				// Token: 0x0400A860 RID: 43104
				public static LocString TOOLTIP = "Select a resource to use as bait";
			}

			// Token: 0x020026A7 RID: 9895
			public class NOFISHABLEWATERBELOW
			{
				// Token: 0x0400A861 RID: 43105
				public static LocString NAME = "No Fishable Water";

				// Token: 0x0400A862 RID: 43106
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"There are no edible ",
					UI.PRE_KEYWORD,
					"Fish",
					UI.PST_KEYWORD,
					" beneath this structure"
				});
			}

			// Token: 0x020026A8 RID: 9896
			public class NOPOWERCONSUMERS
			{
				// Token: 0x0400A863 RID: 43107
				public static LocString NAME = "No Power Consumers";

				// Token: 0x0400A864 RID: 43108
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"No buildings are connected to this ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" source"
				});
			}

			// Token: 0x020026A9 RID: 9897
			public class NOWIRECONNECTED
			{
				// Token: 0x0400A865 RID: 43109
				public static LocString NAME = "No Power Wire Connected";

				// Token: 0x0400A866 RID: 43110
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building has not been connected to a ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" grid"
				});
			}

			// Token: 0x020026AA RID: 9898
			public class PENDINGDECONSTRUCTION
			{
				// Token: 0x0400A867 RID: 43111
				public static LocString NAME = "Deconstruction Errand";

				// Token: 0x0400A868 RID: 43112
				public static LocString TOOLTIP = "Building will be deconstructed once a Duplicant is available";
			}

			// Token: 0x020026AB RID: 9899
			public class PENDINGDEMOLITION
			{
				// Token: 0x0400A869 RID: 43113
				public static LocString NAME = "Demolition Errand";

				// Token: 0x0400A86A RID: 43114
				public static LocString TOOLTIP = "Object will be permanently demolished once a Duplicant is available";
			}

			// Token: 0x020026AC RID: 9900
			public class PENDINGFISH
			{
				// Token: 0x0400A86B RID: 43115
				public static LocString NAME = "Fishing Errand";

				// Token: 0x0400A86C RID: 43116
				public static LocString TOOLTIP = "Spot will be fished once a Duplicant is available";
			}

			// Token: 0x020026AD RID: 9901
			public class PENDINGHARVEST
			{
				// Token: 0x0400A86D RID: 43117
				public static LocString NAME = "Harvest Errand";

				// Token: 0x0400A86E RID: 43118
				public static LocString TOOLTIP = "Plant will be harvested once a Duplicant is available";
			}

			// Token: 0x020026AE RID: 9902
			public class PENDINGUPROOT
			{
				// Token: 0x0400A86F RID: 43119
				public static LocString NAME = "Uproot Errand";

				// Token: 0x0400A870 RID: 43120
				public static LocString TOOLTIP = "Plant will be uprooted once a Duplicant is available";
			}

			// Token: 0x020026AF RID: 9903
			public class PENDINGREPAIR
			{
				// Token: 0x0400A871 RID: 43121
				public static LocString NAME = "Repair Errand";

				// Token: 0x0400A872 RID: 43122
				public static LocString TOOLTIP = "Building will be repaired once a Duplicant is available\nReceived damage from {DamageInfo}";
			}

			// Token: 0x020026B0 RID: 9904
			public class PENDINGSWITCHTOGGLE
			{
				// Token: 0x0400A873 RID: 43123
				public static LocString NAME = "Settings Errand";

				// Token: 0x0400A874 RID: 43124
				public static LocString TOOLTIP = "Settings will be changed once a Duplicant is available";
			}

			// Token: 0x020026B1 RID: 9905
			public class PENDINGWORK
			{
				// Token: 0x0400A875 RID: 43125
				public static LocString NAME = "Work Errand";

				// Token: 0x0400A876 RID: 43126
				public static LocString TOOLTIP = "Building will be operated once a Duplicant is available";
			}

			// Token: 0x020026B2 RID: 9906
			public class POWERBUTTONOFF
			{
				// Token: 0x0400A877 RID: 43127
				public static LocString NAME = "Function Suspended";

				// Token: 0x0400A878 RID: 43128
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building has been toggled off\nPress ",
					UI.PRE_KEYWORD,
					"Enable Building",
					UI.PST_KEYWORD,
					" ",
					UI.FormatAsHotKey(global::Action.ToggleEnabled),
					" to resume its use"
				});
			}

			// Token: 0x020026B3 RID: 9907
			public class PUMPINGSTATION
			{
				// Token: 0x0400A879 RID: 43129
				public static LocString NAME = "Liquid Available: {Liquids}";

				// Token: 0x0400A87A RID: 43130
				public static LocString TOOLTIP = "This pumping station has access to: {Liquids}";
			}

			// Token: 0x020026B4 RID: 9908
			public class PRESSUREOK
			{
				// Token: 0x0400A87B RID: 43131
				public static LocString NAME = "Max Gas Pressure";

				// Token: 0x0400A87C RID: 43132
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"High ambient ",
					UI.PRE_KEYWORD,
					"Gas Pressure",
					UI.PST_KEYWORD,
					" is preventing this building from emitting gas\n\nReduce pressure by pumping ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" away or clearing more space"
				});
			}

			// Token: 0x020026B5 RID: 9909
			public class UNDERPRESSURE
			{
				// Token: 0x0400A87D RID: 43133
				public static LocString NAME = "Low Air Pressure";

				// Token: 0x0400A87E RID: 43134
				public static LocString TOOLTIP = "A minimum atmospheric pressure of <b>{TargetPressure}</b> is needed for this building to operate";
			}

			// Token: 0x020026B6 RID: 9910
			public class STORAGELOCKER
			{
				// Token: 0x0400A87F RID: 43135
				public static LocString NAME = "Storing: {Stored} / {Capacity} {Units}";

				// Token: 0x0400A880 RID: 43136
				public static LocString TOOLTIP = "This container is storing <b>{Stored}{Units}</b> of a maximum <b>{Capacity}{Units}</b>";
			}

			// Token: 0x020026B7 RID: 9911
			public class SKILL_POINTS_AVAILABLE
			{
				// Token: 0x0400A881 RID: 43137
				public static LocString NAME = "Skill Points Available";

				// Token: 0x0400A882 RID: 43138
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A Duplicant has ",
					UI.PRE_KEYWORD,
					"Skill Points",
					UI.PST_KEYWORD,
					" available"
				});
			}

			// Token: 0x020026B8 RID: 9912
			public class TANNINGLIGHTSUFFICIENT
			{
				// Token: 0x0400A883 RID: 43139
				public static LocString NAME = "Tanning Light Available";

				// Token: 0x0400A884 RID: 43140
				public static LocString TOOLTIP = "There is sufficient " + UI.FormatAsLink("Light", "LIGHT") + " here to create pleasing skin crisping";
			}

			// Token: 0x020026B9 RID: 9913
			public class TANNINGLIGHTINSUFFICIENT
			{
				// Token: 0x0400A885 RID: 43141
				public static LocString NAME = "Insufficient Tanning Light";

				// Token: 0x0400A886 RID: 43142
				public static LocString TOOLTIP = "The " + UI.FormatAsLink("Light", "LIGHT") + " here is not bright enough for that Sunny Day feeling";
			}

			// Token: 0x020026BA RID: 9914
			public class UNASSIGNED
			{
				// Token: 0x0400A887 RID: 43143
				public static LocString NAME = "Unassigned";

				// Token: 0x0400A888 RID: 43144
				public static LocString TOOLTIP = "Assign a Duplicant to use this amenity";
			}

			// Token: 0x020026BB RID: 9915
			public class UNDERCONSTRUCTION
			{
				// Token: 0x0400A889 RID: 43145
				public static LocString NAME = "Under Construction";

				// Token: 0x0400A88A RID: 43146
				public static LocString TOOLTIP = "This building is currently being built";
			}

			// Token: 0x020026BC RID: 9916
			public class UNDERCONSTRUCTIONNOWORKER
			{
				// Token: 0x0400A88B RID: 43147
				public static LocString NAME = "Construction Errand";

				// Token: 0x0400A88C RID: 43148
				public static LocString TOOLTIP = "Building will be constructed once a Duplicant is available";
			}

			// Token: 0x020026BD RID: 9917
			public class WAITINGFORMATERIALS
			{
				// Token: 0x0400A88D RID: 43149
				public static LocString NAME = "Awaiting Delivery\n{ItemsRemaining}";

				// Token: 0x0400A88E RID: 43150
				public static LocString TOOLTIP = "These materials will be delivered once a Duplicant is available";

				// Token: 0x0400A88F RID: 43151
				public static LocString LINE_ITEM_MASS = "• {0}: {1}";

				// Token: 0x0400A890 RID: 43152
				public static LocString LINE_ITEM_UNITS = "• {0}";
			}

			// Token: 0x020026BE RID: 9918
			public class WAITINGFORRADIATION
			{
				// Token: 0x0400A891 RID: 43153
				public static LocString NAME = "Awaiting Radbolts";

				// Token: 0x0400A892 RID: 43154
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building requires Radbolts to function\n\nOpen the ",
					UI.FormatAsOverlay("Radiation Overlay"),
					" ",
					UI.FormatAsHotKey(global::Action.Overlay15),
					" to view this building's radiation port"
				});
			}

			// Token: 0x020026BF RID: 9919
			public class WAITINGFORREPAIRMATERIALS
			{
				// Token: 0x0400A893 RID: 43155
				public static LocString NAME = "Awaiting Repair Delivery\n{ItemsRemaining}\n";

				// Token: 0x0400A894 RID: 43156
				public static LocString TOOLTIP = "These materials must be delivered before this building can be repaired";

				// Token: 0x0400A895 RID: 43157
				public static LocString LINE_ITEM = string.Concat(new string[]
				{
					"• ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					": <b>{1}</b>"
				});
			}

			// Token: 0x020026C0 RID: 9920
			public class MISSINGGANTRY
			{
				// Token: 0x0400A896 RID: 43158
				public static LocString NAME = "Missing Gantry";

				// Token: 0x0400A897 RID: 43159
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A ",
					UI.FormatAsLink("Gantry", "GANTRY"),
					" must be built below ",
					UI.FormatAsLink("Command Capsules", "COMMANDMODULE"),
					" and ",
					UI.FormatAsLink("Sight-Seeing Modules", "TOURISTMODULE"),
					" for Duplicant access"
				});
			}

			// Token: 0x020026C1 RID: 9921
			public class DISEMBARKINGDUPLICANT
			{
				// Token: 0x0400A898 RID: 43160
				public static LocString NAME = "Waiting To Disembark";

				// Token: 0x0400A899 RID: 43161
				public static LocString TOOLTIP = "The Duplicant inside this rocket can't come out until the " + UI.FormatAsLink("Gantry", "GANTRY") + " is extended";
			}

			// Token: 0x020026C2 RID: 9922
			public class REACTORMELTDOWN
			{
				// Token: 0x0400A89A RID: 43162
				public static LocString NAME = "Reactor Meltdown";

				// Token: 0x0400A89B RID: 43163
				public static LocString TOOLTIP = "This reactor is spilling dangerous radioactive waste and cannot be stopped";
			}

			// Token: 0x020026C3 RID: 9923
			public class ROCKETNAME
			{
				// Token: 0x0400A89C RID: 43164
				public static LocString NAME = "Parent Rocket: {0}";

				// Token: 0x0400A89D RID: 43165
				public static LocString TOOLTIP = "This module belongs to the rocket: " + UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD;
			}

			// Token: 0x020026C4 RID: 9924
			public class HASGANTRY
			{
				// Token: 0x0400A89E RID: 43166
				public static LocString NAME = "Has Gantry";

				// Token: 0x0400A89F RID: 43167
				public static LocString TOOLTIP = "Duplicants may now enter this section of the rocket";
			}

			// Token: 0x020026C5 RID: 9925
			public class NORMAL
			{
				// Token: 0x0400A8A0 RID: 43168
				public static LocString NAME = "Normal";

				// Token: 0x0400A8A1 RID: 43169
				public static LocString TOOLTIP = "Nothing out of the ordinary here";
			}

			// Token: 0x020026C6 RID: 9926
			public class MANUALGENERATORCHARGINGUP
			{
				// Token: 0x0400A8A2 RID: 43170
				public static LocString NAME = "Charging Up";

				// Token: 0x0400A8A3 RID: 43171
				public static LocString TOOLTIP = "This power source is being charged";
			}

			// Token: 0x020026C7 RID: 9927
			public class MANUALGENERATORRELEASINGENERGY
			{
				// Token: 0x0400A8A4 RID: 43172
				public static LocString NAME = "Powering";

				// Token: 0x0400A8A5 RID: 43173
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This generator is supplying energy to ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" consumers"
				});
			}

			// Token: 0x020026C8 RID: 9928
			public class GENERATOROFFLINE
			{
				// Token: 0x0400A8A6 RID: 43174
				public static LocString NAME = "Generator Idle";

				// Token: 0x0400A8A7 RID: 43175
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" source is idle"
				});
			}

			// Token: 0x020026C9 RID: 9929
			public class PIPE
			{
				// Token: 0x0400A8A8 RID: 43176
				public static LocString NAME = "Contents: {Contents}";

				// Token: 0x0400A8A9 RID: 43177
				public static LocString TOOLTIP = "This pipe is delivering {Contents}";
			}

			// Token: 0x020026CA RID: 9930
			public class CONVEYOR
			{
				// Token: 0x0400A8AA RID: 43178
				public static LocString NAME = "Contents: {Contents}";

				// Token: 0x0400A8AB RID: 43179
				public static LocString TOOLTIP = "This conveyor is delivering {Contents}";
			}

			// Token: 0x020026CB RID: 9931
			public class FABRICATORIDLE
			{
				// Token: 0x0400A8AC RID: 43180
				public static LocString NAME = "No Fabrications Queued";

				// Token: 0x0400A8AD RID: 43181
				public static LocString TOOLTIP = "Select a recipe to begin fabrication";
			}

			// Token: 0x020026CC RID: 9932
			public class FABRICATOREMPTY
			{
				// Token: 0x0400A8AE RID: 43182
				public static LocString NAME = "Waiting For Materials";

				// Token: 0x0400A8AF RID: 43183
				public static LocString TOOLTIP = "Fabrication will begin once materials have been delivered";
			}

			// Token: 0x020026CD RID: 9933
			public class FABRICATORLACKSHEP
			{
				// Token: 0x0400A8B0 RID: 43184
				public static LocString NAME = "Waiting For Radbolts ({CurrentHEP}/{HEPRequired})";

				// Token: 0x0400A8B1 RID: 43185
				public static LocString TOOLTIP = "A queued recipe requires more Radbolts than are currently stored.\n\nCurrently stored: {CurrentHEP}\nRequired for recipe: {HEPRequired}";
			}

			// Token: 0x020026CE RID: 9934
			public class TOILET
			{
				// Token: 0x0400A8B2 RID: 43186
				public static LocString NAME = "{FlushesRemaining} \"Visits\" Remaining";

				// Token: 0x0400A8B3 RID: 43187
				public static LocString TOOLTIP = "{FlushesRemaining} more Duplicants can use this amenity before it requires maintenance";
			}

			// Token: 0x020026CF RID: 9935
			public class TOILETNEEDSEMPTYING
			{
				// Token: 0x0400A8B4 RID: 43188
				public static LocString NAME = "Requires Emptying";

				// Token: 0x0400A8B5 RID: 43189
				public static LocString TOOLTIP = "This amenity cannot be used while full\n\nEmptying it will produce " + UI.FormatAsLink("Polluted Dirt", "TOXICSAND");
			}

			// Token: 0x020026D0 RID: 9936
			public class DESALINATORNEEDSEMPTYING
			{
				// Token: 0x0400A8B6 RID: 43190
				public static LocString NAME = "Requires Emptying";

				// Token: 0x0400A8B7 RID: 43191
				public static LocString TOOLTIP = "This building needs to be emptied of " + UI.FormatAsLink("Salt", "SALT") + " to resume function";
			}

			// Token: 0x020026D1 RID: 9937
			public class HABITATNEEDSEMPTYING
			{
				// Token: 0x0400A8B8 RID: 43192
				public static LocString NAME = "Requires Emptying";

				// Token: 0x0400A8B9 RID: 43193
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.FormatAsLink("Algae Terrarium", "ALGAEHABITAT"),
					" needs to be emptied of ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					"\n\n",
					UI.FormatAsLink("Bottle Emptiers", "BOTTLEEMPTIER"),
					" can be used to transport and dispose of ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					" in designated areas"
				});
			}

			// Token: 0x020026D2 RID: 9938
			public class UNUSABLE
			{
				// Token: 0x0400A8BA RID: 43194
				public static LocString NAME = "Out of Order";

				// Token: 0x0400A8BB RID: 43195
				public static LocString TOOLTIP = "This amenity requires maintenance";
			}

			// Token: 0x020026D3 RID: 9939
			public class NORESEARCHSELECTED
			{
				// Token: 0x0400A8BC RID: 43196
				public static LocString NAME = "No Research Focus Selected";

				// Token: 0x0400A8BD RID: 43197
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Open the ",
					UI.FormatAsManagementMenu("Research Tree", global::Action.ManageResearch),
					" to select a new ",
					UI.FormatAsLink("Research", "TECH"),
					" project"
				});

				// Token: 0x0400A8BE RID: 43198
				public static LocString NOTIFICATION_NAME = "No " + UI.FormatAsLink("Research Focus", "TECH") + " selected";

				// Token: 0x0400A8BF RID: 43199
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"Open the ",
					UI.FormatAsManagementMenu("Research Tree", global::Action.ManageResearch),
					" to select a new ",
					UI.FormatAsLink("Research", "TECH"),
					" project"
				});
			}

			// Token: 0x020026D4 RID: 9940
			public class NORESEARCHORDESTINATIONSELECTED
			{
				// Token: 0x0400A8C0 RID: 43200
				public static LocString NAME = "No Research Focus or Starmap Destination Selected";

				// Token: 0x0400A8C1 RID: 43201
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Select a ",
					UI.FormatAsLink("Research", "TECH"),
					" project in the ",
					UI.FormatAsManagementMenu("Research Tree", global::Action.ManageResearch),
					" or a Destination in the ",
					UI.FormatAsManagementMenu("Starmap", global::Action.ManageStarmap)
				});

				// Token: 0x0400A8C2 RID: 43202
				public static LocString NOTIFICATION_NAME = "No " + UI.FormatAsLink("Research Focus", "TECH") + " or Starmap destination selected";

				// Token: 0x0400A8C3 RID: 43203
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"Select a ",
					UI.FormatAsLink("Research", "TECH"),
					" project in the ",
					UI.FormatAsManagementMenu("Research Tree", "[R]"),
					" or a Destination in the ",
					UI.FormatAsManagementMenu("Starmap", "[Z]")
				});
			}

			// Token: 0x020026D5 RID: 9941
			public class RESEARCHING
			{
				// Token: 0x0400A8C4 RID: 43204
				public static LocString NAME = "Current " + UI.FormatAsLink("Research", "TECH") + ": {Tech}";

				// Token: 0x0400A8C5 RID: 43205
				public static LocString TOOLTIP = "Research produced at this station will be invested in {Tech}";
			}

			// Token: 0x020026D6 RID: 9942
			public class TINKERING
			{
				// Token: 0x0400A8C6 RID: 43206
				public static LocString NAME = "Tinkering: {0}";

				// Token: 0x0400A8C7 RID: 43207
				public static LocString TOOLTIP = "This Duplicant is creating {0} to use somewhere else";
			}

			// Token: 0x020026D7 RID: 9943
			public class VALVE
			{
				// Token: 0x0400A8C8 RID: 43208
				public static LocString NAME = "Max Flow Rate: {MaxFlow}";

				// Token: 0x0400A8C9 RID: 43209
				public static LocString TOOLTIP = "This valve is allowing flow at a volume of <b>{MaxFlow}</b>";
			}

			// Token: 0x020026D8 RID: 9944
			public class VALVEREQUEST
			{
				// Token: 0x0400A8CA RID: 43210
				public static LocString NAME = "Requested Flow Rate: {QueuedMaxFlow}";

				// Token: 0x0400A8CB RID: 43211
				public static LocString TOOLTIP = "Waiting for a Duplicant to adjust flow rate";
			}

			// Token: 0x020026D9 RID: 9945
			public class EMITTINGLIGHT
			{
				// Token: 0x0400A8CC RID: 43212
				public static LocString NAME = "Emitting Light";

				// Token: 0x0400A8CD RID: 43213
				public static LocString TOOLTIP = "Open the " + UI.FormatAsOverlay("Light Overlay", global::Action.Overlay5) + " to view this light's visibility radius";
			}

			// Token: 0x020026DA RID: 9946
			public class RATIONBOXCONTENTS
			{
				// Token: 0x0400A8CE RID: 43214
				public static LocString NAME = "Storing: {Stored}";

				// Token: 0x0400A8CF RID: 43215
				public static LocString TOOLTIP = "This box contains <b>{Stored}</b> of " + UI.PRE_KEYWORD + "Food" + UI.PST_KEYWORD;
			}

			// Token: 0x020026DB RID: 9947
			public class EMITTINGELEMENT
			{
				// Token: 0x0400A8D0 RID: 43216
				public static LocString NAME = "Emitting {ElementType}: {FlowRate}";

				// Token: 0x0400A8D1 RID: 43217
				public static LocString TOOLTIP = "Producing {ElementType} at " + UI.FormatAsPositiveRate("{FlowRate}");
			}

			// Token: 0x020026DC RID: 9948
			public class EMITTINGCO2
			{
				// Token: 0x0400A8D2 RID: 43218
				public static LocString NAME = "Emitting CO<sub>2</sub>: {FlowRate}";

				// Token: 0x0400A8D3 RID: 43219
				public static LocString TOOLTIP = "Producing " + ELEMENTS.CARBONDIOXIDE.NAME + " at " + UI.FormatAsPositiveRate("{FlowRate}");
			}

			// Token: 0x020026DD RID: 9949
			public class EMITTINGOXYGENAVG
			{
				// Token: 0x0400A8D4 RID: 43220
				public static LocString NAME = "Emitting " + UI.FormatAsLink("Oxygen", "OXYGEN") + ": {FlowRate}";

				// Token: 0x0400A8D5 RID: 43221
				public static LocString TOOLTIP = "Producing " + ELEMENTS.OXYGEN.NAME + " at a rate of " + UI.FormatAsPositiveRate("{FlowRate}");
			}

			// Token: 0x020026DE RID: 9950
			public class EMITTINGGASAVG
			{
				// Token: 0x0400A8D6 RID: 43222
				public static LocString NAME = "Emitting {Element}: {FlowRate}";

				// Token: 0x0400A8D7 RID: 43223
				public static LocString TOOLTIP = "Producing {Element} at a rate of " + UI.FormatAsPositiveRate("{FlowRate}");
			}

			// Token: 0x020026DF RID: 9951
			public class EMITTINGBLOCKEDHIGHPRESSURE
			{
				// Token: 0x0400A8D8 RID: 43224
				public static LocString NAME = "Not Emitting: Overpressure";

				// Token: 0x0400A8D9 RID: 43225
				public static LocString TOOLTIP = "Ambient pressure is too high for {Element} to be released";
			}

			// Token: 0x020026E0 RID: 9952
			public class EMITTINGBLOCKEDLOWTEMPERATURE
			{
				// Token: 0x0400A8DA RID: 43226
				public static LocString NAME = "Not Emitting: Too Cold";

				// Token: 0x0400A8DB RID: 43227
				public static LocString TOOLTIP = "Temperature is too low for {Element} to be released";
			}

			// Token: 0x020026E1 RID: 9953
			public class PUMPINGLIQUIDORGAS
			{
				// Token: 0x0400A8DC RID: 43228
				public static LocString NAME = "Average Flow Rate: {FlowRate}";

				// Token: 0x0400A8DD RID: 43229
				public static LocString TOOLTIP = "This building is pumping an average volume of " + UI.FormatAsPositiveRate("{FlowRate}");
			}

			// Token: 0x020026E2 RID: 9954
			public class WIRECIRCUITSTATUS
			{
				// Token: 0x0400A8DE RID: 43230
				public static LocString NAME = "Current Load: {CurrentLoadAndColor} / {MaxLoad}";

				// Token: 0x0400A8DF RID: 43231
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The current ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" load on this wire\n\nOverloading a wire will cause damage to the wire over time and cause it to break"
				});
			}

			// Token: 0x020026E3 RID: 9955
			public class WIREMAXWATTAGESTATUS
			{
				// Token: 0x0400A8E0 RID: 43232
				public static LocString NAME = "Potential Load: {TotalPotentialLoadAndColor} / {MaxLoad}";

				// Token: 0x0400A8E1 RID: 43233
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"How much wattage this network will draw if all ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" consumers on the network become active at once"
				});
			}

			// Token: 0x020026E4 RID: 9956
			public class NOLIQUIDELEMENTTOPUMP
			{
				// Token: 0x0400A8E2 RID: 43234
				public static LocString NAME = "Pump Not In Liquid";

				// Token: 0x0400A8E3 RID: 43235
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This pump must be submerged in ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" to work"
				});
			}

			// Token: 0x020026E5 RID: 9957
			public class NOGASELEMENTTOPUMP
			{
				// Token: 0x0400A8E4 RID: 43236
				public static LocString NAME = "Pump Not In Gas";

				// Token: 0x0400A8E5 RID: 43237
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This pump must be submerged in ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" to work"
				});
			}

			// Token: 0x020026E6 RID: 9958
			public class INVALIDMASKSTATIONCONSUMPTIONSTATE
			{
				// Token: 0x0400A8E6 RID: 43238
				public static LocString NAME = "Station Not In Oxygen";

				// Token: 0x0400A8E7 RID: 43239
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This station must be submerged in ",
					UI.PRE_KEYWORD,
					"Oxygen",
					UI.PST_KEYWORD,
					" to work"
				});
			}

			// Token: 0x020026E7 RID: 9959
			public class PIPEMAYMELT
			{
				// Token: 0x0400A8E8 RID: 43240
				public static LocString NAME = "High Melt Risk";

				// Token: 0x0400A8E9 RID: 43241
				public static LocString TOOLTIP = "This pipe is in danger of melting at the current " + UI.PRE_KEYWORD + "Temperature" + UI.PST_KEYWORD;
			}

			// Token: 0x020026E8 RID: 9960
			public class ELEMENTEMITTEROUTPUT
			{
				// Token: 0x0400A8EA RID: 43242
				public static LocString NAME = "Emitting {ElementTypes}: {FlowRate}";

				// Token: 0x0400A8EB RID: 43243
				public static LocString TOOLTIP = "This object is releasing {ElementTypes} at a rate of " + UI.FormatAsPositiveRate("{FlowRate}");
			}

			// Token: 0x020026E9 RID: 9961
			public class ELEMENTCONSUMER
			{
				// Token: 0x0400A8EC RID: 43244
				public static LocString NAME = "Consuming {ElementTypes}: {FlowRate}";

				// Token: 0x0400A8ED RID: 43245
				public static LocString TOOLTIP = "This object is utilizing ambient {ElementTypes} from the environment";
			}

			// Token: 0x020026EA RID: 9962
			public class SPACECRAFTREADYTOLAND
			{
				// Token: 0x0400A8EE RID: 43246
				public static LocString NAME = "Spacecraft ready to land";

				// Token: 0x0400A8EF RID: 43247
				public static LocString TOOLTIP = "A spacecraft is ready to land";

				// Token: 0x0400A8F0 RID: 43248
				public static LocString NOTIFICATION = "Space mission complete";

				// Token: 0x0400A8F1 RID: 43249
				public static LocString NOTIFICATION_TOOLTIP = "Spacecrafts have completed their missions";
			}

			// Token: 0x020026EB RID: 9963
			public class CONSUMINGFROMSTORAGE
			{
				// Token: 0x0400A8F2 RID: 43250
				public static LocString NAME = "Consuming {ElementTypes}: {FlowRate}";

				// Token: 0x0400A8F3 RID: 43251
				public static LocString TOOLTIP = "This building is consuming {ElementTypes} from storage";
			}

			// Token: 0x020026EC RID: 9964
			public class ELEMENTCONVERTEROUTPUT
			{
				// Token: 0x0400A8F4 RID: 43252
				public static LocString NAME = "Emitting {ElementTypes}: {FlowRate}";

				// Token: 0x0400A8F5 RID: 43253
				public static LocString TOOLTIP = "This building is releasing {ElementTypes} at a rate of " + UI.FormatAsPositiveRate("{FlowRate}");
			}

			// Token: 0x020026ED RID: 9965
			public class ELEMENTCONVERTERINPUT
			{
				// Token: 0x0400A8F6 RID: 43254
				public static LocString NAME = "Using {ElementTypes}: {FlowRate}";

				// Token: 0x0400A8F7 RID: 43255
				public static LocString TOOLTIP = "This building is using {ElementTypes} from storage at a rate of " + UI.FormatAsNegativeRate("{FlowRate}");
			}

			// Token: 0x020026EE RID: 9966
			public class AWAITINGCOMPOSTFLIP
			{
				// Token: 0x0400A8F8 RID: 43256
				public static LocString NAME = "Requires Flipping";

				// Token: 0x0400A8F9 RID: 43257
				public static LocString TOOLTIP = "Compost must be flipped periodically to produce " + UI.FormatAsLink("Dirt", "DIRT");
			}

			// Token: 0x020026EF RID: 9967
			public class AWAITINGWASTE
			{
				// Token: 0x0400A8FA RID: 43258
				public static LocString NAME = "Awaiting Compostables";

				// Token: 0x0400A8FB RID: 43259
				public static LocString TOOLTIP = "More waste material is required to begin the composting process";
			}

			// Token: 0x020026F0 RID: 9968
			public class BATTERIESSUFFICIENTLYFULL
			{
				// Token: 0x0400A8FC RID: 43260
				public static LocString NAME = "Batteries Sufficiently Full";

				// Token: 0x0400A8FD RID: 43261
				public static LocString TOOLTIP = "All batteries are above the refill threshold";
			}

			// Token: 0x020026F1 RID: 9969
			public class NEEDRESOURCEMASS
			{
				// Token: 0x0400A8FE RID: 43262
				public static LocString NAME = "Insufficient Resources\n{ResourcesRequired}";

				// Token: 0x0400A8FF RID: 43263
				public static LocString TOOLTIP = "The mass of material that was delivered to this building was too low\n\nDeliver more material to run this building";

				// Token: 0x0400A900 RID: 43264
				public static LocString LINE_ITEM = "• <b>{0}</b>";
			}

			// Token: 0x020026F2 RID: 9970
			public class JOULESAVAILABLE
			{
				// Token: 0x0400A901 RID: 43265
				public static LocString NAME = "Power Available: {JoulesAvailable} / {JoulesCapacity}";

				// Token: 0x0400A902 RID: 43266
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"<b>{JoulesAvailable}</b> of stored ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" available for use"
				});
			}

			// Token: 0x020026F3 RID: 9971
			public class WATTAGE
			{
				// Token: 0x0400A903 RID: 43267
				public static LocString NAME = "Wattage: {Wattage}";

				// Token: 0x0400A904 RID: 43268
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building is generating ",
					UI.FormatAsPositiveRate("{Wattage}"),
					" of ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x020026F4 RID: 9972
			public class SOLARPANELWATTAGE
			{
				// Token: 0x0400A905 RID: 43269
				public static LocString NAME = "Current Wattage: {Wattage}";

				// Token: 0x0400A906 RID: 43270
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This panel is generating ",
					UI.FormatAsPositiveRate("{Wattage}"),
					" of ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x020026F5 RID: 9973
			public class MODULESOLARPANELWATTAGE
			{
				// Token: 0x0400A907 RID: 43271
				public static LocString NAME = "Current Wattage: {Wattage}";

				// Token: 0x0400A908 RID: 43272
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This panel is generating ",
					UI.FormatAsPositiveRate("{Wattage}"),
					" of ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x020026F6 RID: 9974
			public class WATTSON
			{
				// Token: 0x0400A909 RID: 43273
				public static LocString NAME = "Next Print: {TimeRemaining}";

				// Token: 0x0400A90A RID: 43274
				public static LocString TOOLTIP = "The Printing Pod can print out new Duplicants and useful resources over time.\nThe next print will be ready in <b>{TimeRemaining}</b>";

				// Token: 0x0400A90B RID: 43275
				public static LocString UNAVAILABLE = "UNAVAILABLE";
			}

			// Token: 0x020026F7 RID: 9975
			public class FLUSHTOILET
			{
				// Token: 0x0400A90C RID: 43276
				public static LocString NAME = "{toilet} Ready";

				// Token: 0x0400A90D RID: 43277
				public static LocString TOOLTIP = "This bathroom is ready to receive visitors";
			}

			// Token: 0x020026F8 RID: 9976
			public class FLUSHTOILETINUSE
			{
				// Token: 0x0400A90E RID: 43278
				public static LocString NAME = "{toilet} In Use";

				// Token: 0x0400A90F RID: 43279
				public static LocString TOOLTIP = "This bathroom is occupied";
			}

			// Token: 0x020026F9 RID: 9977
			public class WIRECONNECTED
			{
				// Token: 0x0400A910 RID: 43280
				public static LocString NAME = "Wire Connected";

				// Token: 0x0400A911 RID: 43281
				public static LocString TOOLTIP = "This wire is connected to a network";
			}

			// Token: 0x020026FA RID: 9978
			public class WIRENOMINAL
			{
				// Token: 0x0400A912 RID: 43282
				public static LocString NAME = "Wire Nominal";

				// Token: 0x0400A913 RID: 43283
				public static LocString TOOLTIP = "This wire is able to handle the wattage it is receiving";
			}

			// Token: 0x020026FB RID: 9979
			public class WIREDISCONNECTED
			{
				// Token: 0x0400A914 RID: 43284
				public static LocString NAME = "Wire Disconnected";

				// Token: 0x0400A915 RID: 43285
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This wire is not connecting a ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" consumer to a ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" generator"
				});
			}

			// Token: 0x020026FC RID: 9980
			public class COOLING
			{
				// Token: 0x0400A916 RID: 43286
				public static LocString NAME = "Cooling";

				// Token: 0x0400A917 RID: 43287
				public static LocString TOOLTIP = "This building is cooling the surrounding area";
			}

			// Token: 0x020026FD RID: 9981
			public class COOLINGSTALLEDHOTENV
			{
				// Token: 0x0400A918 RID: 43288
				public static LocString NAME = "Gas Too Hot";

				// Token: 0x0400A919 RID: 43289
				public static LocString TOOLTIP = "Incoming pipe contents cannot be cooled more than <b>{2}</b> below the surrounding environment\n\nEnvironment: {0}\nCurrent Pipe Contents: {1}";
			}

			// Token: 0x020026FE RID: 9982
			public class COOLINGSTALLEDCOLDGAS
			{
				// Token: 0x0400A91A RID: 43290
				public static LocString NAME = "Gas Too Cold";

				// Token: 0x0400A91B RID: 43291
				public static LocString TOOLTIP = "This building cannot cool incoming pipe contents below <b>{0}</b>\n\nCurrent Pipe Contents: {0}";
			}

			// Token: 0x020026FF RID: 9983
			public class COOLINGSTALLEDHOTLIQUID
			{
				// Token: 0x0400A91C RID: 43292
				public static LocString NAME = "Liquid Too Hot";

				// Token: 0x0400A91D RID: 43293
				public static LocString TOOLTIP = "Incoming pipe contents cannot be cooled more than <b>{2}</b> below the surrounding environment\n\nEnvironment: {0}\nCurrent Pipe Contents: {1}";
			}

			// Token: 0x02002700 RID: 9984
			public class COOLINGSTALLEDCOLDLIQUID
			{
				// Token: 0x0400A91E RID: 43294
				public static LocString NAME = "Liquid Too Cold";

				// Token: 0x0400A91F RID: 43295
				public static LocString TOOLTIP = "This building cannot cool incoming pipe contents below <b>{0}</b>\n\nCurrent Pipe Contents: {0}";
			}

			// Token: 0x02002701 RID: 9985
			public class CANNOTCOOLFURTHER
			{
				// Token: 0x0400A920 RID: 43296
				public static LocString NAME = "Minimum Temperature Reached";

				// Token: 0x0400A921 RID: 43297
				public static LocString TOOLTIP = "This building cannot cool the surrounding environment below <b>{0}</b>";
			}

			// Token: 0x02002702 RID: 9986
			public class HEATINGSTALLEDHOTENV
			{
				// Token: 0x0400A922 RID: 43298
				public static LocString NAME = "Target Temperature Reached";

				// Token: 0x0400A923 RID: 43299
				public static LocString TOOLTIP = "This building cannot heat the surrounding environment beyond <b>{0}</b>";
			}

			// Token: 0x02002703 RID: 9987
			public class HEATINGSTALLEDLOWMASS_GAS
			{
				// Token: 0x0400A924 RID: 43300
				public static LocString NAME = "Insufficient Atmosphere";

				// Token: 0x0400A925 RID: 43301
				public static LocString TOOLTIP = "This building cannot operate in a vacuum";
			}

			// Token: 0x02002704 RID: 9988
			public class HEATINGSTALLEDLOWMASS_LIQUID
			{
				// Token: 0x0400A926 RID: 43302
				public static LocString NAME = "Not Submerged In Liquid";

				// Token: 0x0400A927 RID: 43303
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building must be submerged in ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" to function"
				});
			}

			// Token: 0x02002705 RID: 9989
			public class BUILDINGDISABLED
			{
				// Token: 0x0400A928 RID: 43304
				public static LocString NAME = "Building Disabled";

				// Token: 0x0400A929 RID: 43305
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Press ",
					UI.PRE_KEYWORD,
					"Enable Building",
					UI.PST_KEYWORD,
					" ",
					UI.FormatAsHotKey(global::Action.ToggleEnabled),
					" to resume use"
				});
			}

			// Token: 0x02002706 RID: 9990
			public class MISSINGREQUIREMENTS
			{
				// Token: 0x0400A92A RID: 43306
				public static LocString NAME = "Missing Requirements";

				// Token: 0x0400A92B RID: 43307
				public static LocString TOOLTIP = "There are some problems that need to be fixed before this building is operational";
			}

			// Token: 0x02002707 RID: 9991
			public class GETTINGREADY
			{
				// Token: 0x0400A92C RID: 43308
				public static LocString NAME = "Getting Ready";

				// Token: 0x0400A92D RID: 43309
				public static LocString TOOLTIP = "This building will soon be ready to use";
			}

			// Token: 0x02002708 RID: 9992
			public class WORKING
			{
				// Token: 0x0400A92E RID: 43310
				public static LocString NAME = "Nominal";

				// Token: 0x0400A92F RID: 43311
				public static LocString TOOLTIP = "This building is working as intended";
			}

			// Token: 0x02002709 RID: 9993
			public class GRAVEEMPTY
			{
				// Token: 0x0400A930 RID: 43312
				public static LocString NAME = "Empty";

				// Token: 0x0400A931 RID: 43313
				public static LocString TOOLTIP = "This memorial honors no one.";
			}

			// Token: 0x0200270A RID: 9994
			public class GRAVE
			{
				// Token: 0x0400A932 RID: 43314
				public static LocString NAME = "RIP {DeadDupe}";

				// Token: 0x0400A933 RID: 43315
				public static LocString TOOLTIP = "{Epitaph}";
			}

			// Token: 0x0200270B RID: 9995
			public class AWAITINGARTING
			{
				// Token: 0x0400A934 RID: 43316
				public static LocString NAME = "Incomplete Artwork";

				// Token: 0x0400A935 RID: 43317
				public static LocString TOOLTIP = "This building requires a Duplicant's artistic touch";
			}

			// Token: 0x0200270C RID: 9996
			public class LOOKINGUGLY
			{
				// Token: 0x0400A936 RID: 43318
				public static LocString NAME = "Crude";

				// Token: 0x0400A937 RID: 43319
				public static LocString TOOLTIP = "Honestly, Morbs could've done better";
			}

			// Token: 0x0200270D RID: 9997
			public class LOOKINGOKAY
			{
				// Token: 0x0400A938 RID: 43320
				public static LocString NAME = "Quaint";

				// Token: 0x0400A939 RID: 43321
				public static LocString TOOLTIP = "Duplicants find this art piece quite charming";
			}

			// Token: 0x0200270E RID: 9998
			public class LOOKINGGREAT
			{
				// Token: 0x0400A93A RID: 43322
				public static LocString NAME = "Masterpiece";

				// Token: 0x0400A93B RID: 43323
				public static LocString TOOLTIP = "This poignant piece stirs something deep within each Duplicant's soul";
			}

			// Token: 0x0200270F RID: 9999
			public class EXPIRED
			{
				// Token: 0x0400A93C RID: 43324
				public static LocString NAME = "Depleted";

				// Token: 0x0400A93D RID: 43325
				public static LocString TOOLTIP = "This building has no more use";
			}

			// Token: 0x02002710 RID: 10000
			public class EXCAVATOR_BOMB
			{
				// Token: 0x02002F55 RID: 12117
				public class UNARMED
				{
					// Token: 0x0400BE0B RID: 48651
					public static LocString NAME = "Unarmed";

					// Token: 0x0400BE0C RID: 48652
					public static LocString TOOLTIP = "This explosive is currently inactive";
				}

				// Token: 0x02002F56 RID: 12118
				public class ARMED
				{
					// Token: 0x0400BE0D RID: 48653
					public static LocString NAME = "Armed";

					// Token: 0x0400BE0E RID: 48654
					public static LocString TOOLTIP = "Stand back, this baby's ready to blow!";
				}

				// Token: 0x02002F57 RID: 12119
				public class COUNTDOWN
				{
					// Token: 0x0400BE0F RID: 48655
					public static LocString NAME = "Countdown: {0}";

					// Token: 0x0400BE10 RID: 48656
					public static LocString TOOLTIP = "<b>{0}</b> seconds until detonation";
				}

				// Token: 0x02002F58 RID: 12120
				public class DUPE_DANGER
				{
					// Token: 0x0400BE11 RID: 48657
					public static LocString NAME = "Duplicant Preservation Override";

					// Token: 0x0400BE12 RID: 48658
					public static LocString TOOLTIP = "Explosive disabled due to close Duplicant proximity";
				}

				// Token: 0x02002F59 RID: 12121
				public class EXPLODING
				{
					// Token: 0x0400BE13 RID: 48659
					public static LocString NAME = "Exploding";

					// Token: 0x0400BE14 RID: 48660
					public static LocString TOOLTIP = "Kaboom!";
				}
			}

			// Token: 0x02002711 RID: 10001
			public class BURNER
			{
				// Token: 0x02002F5A RID: 12122
				public class BURNING_FUEL
				{
					// Token: 0x0400BE15 RID: 48661
					public static LocString NAME = "Consuming Fuel: {0}";

					// Token: 0x0400BE16 RID: 48662
					public static LocString TOOLTIP = "<b>{0}</b> fuel remaining";
				}

				// Token: 0x02002F5B RID: 12123
				public class HAS_FUEL
				{
					// Token: 0x0400BE17 RID: 48663
					public static LocString NAME = "Fueled: {0}";

					// Token: 0x0400BE18 RID: 48664
					public static LocString TOOLTIP = "<b>{0}</b> fuel remaining";
				}
			}

			// Token: 0x02002712 RID: 10002
			public class CREATURE_TRAP
			{
				// Token: 0x02002F5C RID: 12124
				public class NEEDSBAIT
				{
					// Token: 0x0400BE19 RID: 48665
					public static LocString NAME = "Needs Bait";

					// Token: 0x0400BE1A RID: 48666
					public static LocString TOOLTIP = "This trap needs to be baited before it can be set";
				}

				// Token: 0x02002F5D RID: 12125
				public class READY
				{
					// Token: 0x0400BE1B RID: 48667
					public static LocString NAME = "Set";

					// Token: 0x0400BE1C RID: 48668
					public static LocString TOOLTIP = "This trap has been set and is ready to catch a " + UI.PRE_KEYWORD + "Critter" + UI.PST_KEYWORD;
				}

				// Token: 0x02002F5E RID: 12126
				public class SPRUNG
				{
					// Token: 0x0400BE1D RID: 48669
					public static LocString NAME = "Sprung";

					// Token: 0x0400BE1E RID: 48670
					public static LocString TOOLTIP = "This trap has caught a {0}!";
				}
			}

			// Token: 0x02002713 RID: 10003
			public class ACCESS_CONTROL
			{
				// Token: 0x02002F5F RID: 12127
				public class ACTIVE
				{
					// Token: 0x0400BE1F RID: 48671
					public static LocString NAME = "Access Restrictions";

					// Token: 0x0400BE20 RID: 48672
					public static LocString TOOLTIP = "Some Duplicants are prohibited from passing through this door by the current " + UI.PRE_KEYWORD + "Access Permissions" + UI.PST_KEYWORD;
				}

				// Token: 0x02002F60 RID: 12128
				public class OFFLINE
				{
					// Token: 0x0400BE21 RID: 48673
					public static LocString NAME = "Access Control Offline";

					// Token: 0x0400BE22 RID: 48674
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"This door has granted Emergency ",
						UI.PRE_KEYWORD,
						"Access Permissions",
						UI.PST_KEYWORD,
						"\n\nAll Duplicants are permitted to pass through it until ",
						UI.PRE_KEYWORD,
						"Power",
						UI.PST_KEYWORD,
						" is restored"
					});
				}
			}

			// Token: 0x02002714 RID: 10004
			public class REQUIRESSKILLPERK
			{
				// Token: 0x0400A93E RID: 43326
				public static LocString NAME = "Skill-Required Operation";

				// Token: 0x0400A93F RID: 43327
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Only Duplicants with one of the following ",
					UI.PRE_KEYWORD,
					"Skills",
					UI.PST_KEYWORD,
					" can operate this building:\n{Skills}"
				});
			}

			// Token: 0x02002715 RID: 10005
			public class DIGREQUIRESSKILLPERK
			{
				// Token: 0x0400A940 RID: 43328
				public static LocString NAME = "Skill-Required Dig";

				// Token: 0x0400A941 RID: 43329
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Only Duplicants with one of the following ",
					UI.PRE_KEYWORD,
					"Skills",
					UI.PST_KEYWORD,
					" can mine this material:\n{Skills}"
				});
			}

			// Token: 0x02002716 RID: 10006
			public class COLONYLACKSREQUIREDSKILLPERK
			{
				// Token: 0x0400A942 RID: 43330
				public static LocString NAME = "Colony Lacks {Skills}";

				// Token: 0x0400A943 RID: 43331
				public static LocString TOOLTIP = "{Skills} Skill required to operate\n\nOpen the " + UI.FormatAsManagementMenu("Skills Panel", global::Action.ManageSkills) + " to teach {Skills} to a Duplicant";
			}

			// Token: 0x02002717 RID: 10007
			public class CLUSTERCOLONYLACKSREQUIREDSKILLPERK
			{
				// Token: 0x0400A944 RID: 43332
				public static LocString NAME = "Local Colony Lacks {Skills}";

				// Token: 0x0400A945 RID: 43333
				public static LocString TOOLTIP = BUILDING.STATUSITEMS.COLONYLACKSREQUIREDSKILLPERK.TOOLTIP + ", or bring a Duplicant with the skill from another " + UI.CLUSTERMAP.PLANETOID;
			}

			// Token: 0x02002718 RID: 10008
			public class WORKREQUIRESMINION
			{
				// Token: 0x0400A946 RID: 43334
				public static LocString NAME = "Duplicant Operation Required";

				// Token: 0x0400A947 RID: 43335
				public static LocString TOOLTIP = "A Duplicant must be present to complete this operation";
			}

			// Token: 0x02002719 RID: 10009
			public class SWITCHSTATUSACTIVE
			{
				// Token: 0x0400A948 RID: 43336
				public static LocString NAME = "Active";

				// Token: 0x0400A949 RID: 43337
				public static LocString TOOLTIP = "This switch is currently toggled <b>On</b>";
			}

			// Token: 0x0200271A RID: 10010
			public class SWITCHSTATUSINACTIVE
			{
				// Token: 0x0400A94A RID: 43338
				public static LocString NAME = "Inactive";

				// Token: 0x0400A94B RID: 43339
				public static LocString TOOLTIP = "This switch is currently toggled <b>Off</b>";
			}

			// Token: 0x0200271B RID: 10011
			public class LOGICSWITCHSTATUSACTIVE
			{
				// Token: 0x0400A94C RID: 43340
				public static LocString NAME = "Sending a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);

				// Token: 0x0400A94D RID: 43341
				public static LocString TOOLTIP = "This switch is currently sending a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);
			}

			// Token: 0x0200271C RID: 10012
			public class LOGICSWITCHSTATUSINACTIVE
			{
				// Token: 0x0400A94E RID: 43342
				public static LocString NAME = "Sending a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x0400A94F RID: 43343
				public static LocString TOOLTIP = "This switch is currently sending a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x0200271D RID: 10013
			public class LOGICSENSORSTATUSACTIVE
			{
				// Token: 0x0400A950 RID: 43344
				public static LocString NAME = "Sending a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);

				// Token: 0x0400A951 RID: 43345
				public static LocString TOOLTIP = "This sensor is currently sending a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);
			}

			// Token: 0x0200271E RID: 10014
			public class LOGICSENSORSTATUSINACTIVE
			{
				// Token: 0x0400A952 RID: 43346
				public static LocString NAME = "Sending a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x0400A953 RID: 43347
				public static LocString TOOLTIP = "This sensor is currently sending " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x0200271F RID: 10015
			public class PLAYERCONTROLLEDTOGGLESIDESCREEN
			{
				// Token: 0x0400A954 RID: 43348
				public static LocString NAME = "Pending Toggle on Unpause";

				// Token: 0x0400A955 RID: 43349
				public static LocString TOOLTIP = "This will be toggled when time is unpaused";
			}

			// Token: 0x02002720 RID: 10016
			public class FOOD_CONTAINERS_OUTSIDE_RANGE
			{
				// Token: 0x0400A956 RID: 43350
				public static LocString NAME = "Unreachable food";

				// Token: 0x0400A957 RID: 43351
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Recuperating Duplicants must have ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					" available within <b>{0}</b> cells"
				});
			}

			// Token: 0x02002721 RID: 10017
			public class TOILETS_OUTSIDE_RANGE
			{
				// Token: 0x0400A958 RID: 43352
				public static LocString NAME = "Unreachable restroom";

				// Token: 0x0400A959 RID: 43353
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Recuperating Duplicants must have ",
					UI.PRE_KEYWORD,
					"Toilets",
					UI.PST_KEYWORD,
					" available within <b>{0}</b> cells"
				});
			}

			// Token: 0x02002722 RID: 10018
			public class BUILDING_DEPRECATED
			{
				// Token: 0x0400A95A RID: 43354
				public static LocString NAME = "Building Deprecated";

				// Token: 0x0400A95B RID: 43355
				public static LocString TOOLTIP = "This building is from an older version of the game and its use is not intended";
			}

			// Token: 0x02002723 RID: 10019
			public class TURBINE_BLOCKED_INPUT
			{
				// Token: 0x0400A95C RID: 43356
				public static LocString NAME = "All Inputs Blocked";

				// Token: 0x0400A95D RID: 43357
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This turbine's ",
					UI.PRE_KEYWORD,
					"Input Vents",
					UI.PST_KEYWORD,
					" are blocked, so it can't intake any ",
					ELEMENTS.STEAM.NAME,
					".\n\nThe ",
					UI.PRE_KEYWORD,
					"Input Vents",
					UI.PST_KEYWORD,
					" are located directly below the foundation ",
					UI.PRE_KEYWORD,
					"Tile",
					UI.PST_KEYWORD,
					" this building is resting on."
				});
			}

			// Token: 0x02002724 RID: 10020
			public class TURBINE_PARTIALLY_BLOCKED_INPUT
			{
				// Token: 0x0400A95E RID: 43358
				public static LocString NAME = "{Blocked}/{Total} Inputs Blocked";

				// Token: 0x0400A95F RID: 43359
				public static LocString TOOLTIP = "<b>{Blocked}</b> of this turbine's <b>{Total}</b> inputs have been blocked, resulting in reduced throughput";
			}

			// Token: 0x02002725 RID: 10021
			public class TURBINE_TOO_HOT
			{
				// Token: 0x0400A960 RID: 43360
				public static LocString NAME = "Turbine Too Hot";

				// Token: 0x0400A961 RID: 43361
				public static LocString TOOLTIP = "This turbine must be below <b>{Overheat_Temperature}</b> to properly process {Src_Element} into {Dest_Element}";
			}

			// Token: 0x02002726 RID: 10022
			public class TURBINE_BLOCKED_OUTPUT
			{
				// Token: 0x0400A962 RID: 43362
				public static LocString NAME = "Output Blocked";

				// Token: 0x0400A963 RID: 43363
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A blocked ",
					UI.PRE_KEYWORD,
					"Output",
					UI.PST_KEYWORD,
					" has stopped this turbine from functioning"
				});
			}

			// Token: 0x02002727 RID: 10023
			public class TURBINE_INSUFFICIENT_MASS
			{
				// Token: 0x0400A964 RID: 43364
				public static LocString NAME = "Not Enough {Src_Element}";

				// Token: 0x0400A965 RID: 43365
				public static LocString TOOLTIP = "The {Src_Element} present below this turbine must be at least <b>{Min_Mass}</b> in order to turn the turbine";
			}

			// Token: 0x02002728 RID: 10024
			public class TURBINE_INSUFFICIENT_TEMPERATURE
			{
				// Token: 0x0400A966 RID: 43366
				public static LocString NAME = "{Src_Element} Temperature Below {Active_Temperature}";

				// Token: 0x0400A967 RID: 43367
				public static LocString TOOLTIP = "This turbine requires {Src_Element} that is a minimum of <b>{Active_Temperature}</b> in order to produce power";
			}

			// Token: 0x02002729 RID: 10025
			public class TURBINE_ACTIVE_WATTAGE
			{
				// Token: 0x0400A968 RID: 43368
				public static LocString NAME = "Current Wattage: {Wattage}";

				// Token: 0x0400A969 RID: 43369
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This turbine is generating ",
					UI.FormatAsPositiveRate("{Wattage}"),
					" of ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					"\n\nIt is running at <b>{Efficiency}</b> of full capacity\n\nIncrease {Src_Element} ",
					UI.PRE_KEYWORD,
					"Mass",
					UI.PST_KEYWORD,
					" and ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" to improve output"
				});
			}

			// Token: 0x0200272A RID: 10026
			public class TURBINE_SPINNING_UP
			{
				// Token: 0x0400A96A RID: 43370
				public static LocString NAME = "Spinning Up";

				// Token: 0x0400A96B RID: 43371
				public static LocString TOOLTIP = "This turbine is currently spinning up\n\nSpinning up allows a turbine to continue running for a short period if the pressure it needs to run becomes unavailable";
			}

			// Token: 0x0200272B RID: 10027
			public class TURBINE_ACTIVE
			{
				// Token: 0x0400A96C RID: 43372
				public static LocString NAME = "Active";

				// Token: 0x0400A96D RID: 43373
				public static LocString TOOLTIP = "This turbine is running at <b>{0}RPM</b>";
			}

			// Token: 0x0200272C RID: 10028
			public class WELL_PRESSURIZING
			{
				// Token: 0x0400A96E RID: 43374
				public static LocString NAME = "Backpressure: {0}";

				// Token: 0x0400A96F RID: 43375
				public static LocString TOOLTIP = "Well pressure increases with each use and must be periodically relieved to prevent shutdown";
			}

			// Token: 0x0200272D RID: 10029
			public class WELL_OVERPRESSURE
			{
				// Token: 0x0400A970 RID: 43376
				public static LocString NAME = "Overpressure";

				// Token: 0x0400A971 RID: 43377
				public static LocString TOOLTIP = "This well can no longer function due to excessive backpressure";
			}

			// Token: 0x0200272E RID: 10030
			public class NOTINANYROOM
			{
				// Token: 0x0400A972 RID: 43378
				public static LocString NAME = "Outside of room";

				// Token: 0x0400A973 RID: 43379
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building must be built inside a ",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					" for full functionality\n\nOpen the ",
					UI.FormatAsOverlay("Room Overlay", global::Action.Overlay11),
					" to view full ",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					" status"
				});
			}

			// Token: 0x0200272F RID: 10031
			public class NOTINREQUIREDROOM
			{
				// Token: 0x0400A974 RID: 43380
				public static LocString NAME = "Outside of {0}";

				// Token: 0x0400A975 RID: 43381
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building must be built inside a {0} for full functionality\n\nOpen the ",
					UI.FormatAsOverlay("Room Overlay", global::Action.Overlay11),
					" to view full ",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					" status"
				});
			}

			// Token: 0x02002730 RID: 10032
			public class NOTINRECOMMENDEDROOM
			{
				// Token: 0x0400A976 RID: 43382
				public static LocString NAME = "Outside of {0}";

				// Token: 0x0400A977 RID: 43383
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"It is recommended to build this building inside a {0}\n\nOpen the ",
					UI.FormatAsOverlay("Room Overlay", global::Action.Overlay11),
					" to view full ",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					" status"
				});
			}

			// Token: 0x02002731 RID: 10033
			public class RELEASING_PRESSURE
			{
				// Token: 0x0400A978 RID: 43384
				public static LocString NAME = "Releasing Pressure";

				// Token: 0x0400A979 RID: 43385
				public static LocString TOOLTIP = "Pressure buildup is being safely released";
			}

			// Token: 0x02002732 RID: 10034
			public class LOGIC_FEEDBACK_LOOP
			{
				// Token: 0x0400A97A RID: 43386
				public static LocString NAME = "Feedback Loop";

				// Token: 0x0400A97B RID: 43387
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Feedback loops prevent automation grids from functioning\n\nFeedback loops occur when the ",
					UI.PRE_KEYWORD,
					"Output",
					UI.PST_KEYWORD,
					" of an automated building connects back to its own ",
					UI.PRE_KEYWORD,
					"Input",
					UI.PST_KEYWORD,
					" through the Automation grid"
				});
			}

			// Token: 0x02002733 RID: 10035
			public class ENOUGH_COOLANT
			{
				// Token: 0x0400A97C RID: 43388
				public static LocString NAME = "Awaiting Coolant";

				// Token: 0x0400A97D RID: 43389
				public static LocString TOOLTIP = "<b>{1}</b> of {0} must be present in storage to begin production";
			}

			// Token: 0x02002734 RID: 10036
			public class ENOUGH_FUEL
			{
				// Token: 0x0400A97E RID: 43390
				public static LocString NAME = "Awaiting Fuel";

				// Token: 0x0400A97F RID: 43391
				public static LocString TOOLTIP = "<b>{1}</b> of {0} must be present in storage to begin production";
			}

			// Token: 0x02002735 RID: 10037
			public class LOGIC
			{
				// Token: 0x0400A980 RID: 43392
				public static LocString LOGIC_CONTROLLED_ENABLED = "Enabled by Automation Grid";

				// Token: 0x0400A981 RID: 43393
				public static LocString LOGIC_CONTROLLED_DISABLED = "Disabled by Automation Grid";
			}

			// Token: 0x02002736 RID: 10038
			public class GANTRY
			{
				// Token: 0x0400A982 RID: 43394
				public static LocString AUTOMATION_CONTROL = "Automation Control: {0}";

				// Token: 0x0400A983 RID: 43395
				public static LocString MANUAL_CONTROL = "Manual Control: {0}";

				// Token: 0x0400A984 RID: 43396
				public static LocString EXTENDED = "Extended";

				// Token: 0x0400A985 RID: 43397
				public static LocString RETRACTED = "Retracted";
			}

			// Token: 0x02002737 RID: 10039
			public class OBJECTDISPENSER
			{
				// Token: 0x0400A986 RID: 43398
				public static LocString AUTOMATION_CONTROL = "Automation Control: {0}";

				// Token: 0x0400A987 RID: 43399
				public static LocString MANUAL_CONTROL = "Manual Control: {0}";

				// Token: 0x0400A988 RID: 43400
				public static LocString OPENED = "Opened";

				// Token: 0x0400A989 RID: 43401
				public static LocString CLOSED = "Closed";
			}

			// Token: 0x02002738 RID: 10040
			public class TOO_COLD
			{
				// Token: 0x0400A98A RID: 43402
				public static LocString NAME = "Too Cold";

				// Token: 0x0400A98B RID: 43403
				public static LocString TOOLTIP = "Either this building or its surrounding environment is too cold to operate";
			}

			// Token: 0x02002739 RID: 10041
			public class CHECKPOINT
			{
				// Token: 0x0400A98C RID: 43404
				public static LocString LOGIC_CONTROLLED_OPEN = "Clearance: Permitted";

				// Token: 0x0400A98D RID: 43405
				public static LocString LOGIC_CONTROLLED_CLOSED = "Clearance: Not Permitted";

				// Token: 0x0400A98E RID: 43406
				public static LocString LOGIC_CONTROLLED_DISCONNECTED = "No Automation";

				// Token: 0x02002F61 RID: 12129
				public class TOOLTIPS
				{
					// Token: 0x0400BE23 RID: 48675
					public static LocString LOGIC_CONTROLLED_OPEN = "Automated Checkpoint is receiving a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ", preventing Duplicants from passing";

					// Token: 0x0400BE24 RID: 48676
					public static LocString LOGIC_CONTROLLED_CLOSED = "Automated Checkpoint is receiving a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ", allowing Duplicants to pass";

					// Token: 0x0400BE25 RID: 48677
					public static LocString LOGIC_CONTROLLED_DISCONNECTED = string.Concat(new string[]
					{
						"This Checkpoint has not been connected to an ",
						UI.PRE_KEYWORD,
						"Automation",
						UI.PST_KEYWORD,
						" grid"
					});
				}
			}

			// Token: 0x0200273A RID: 10042
			public class HIGHENERGYPARTICLEREDIRECTOR
			{
				// Token: 0x0400A98F RID: 43407
				public static LocString LOGIC_CONTROLLED_STANDBY = "Incoming Radbolts: Ignore";

				// Token: 0x0400A990 RID: 43408
				public static LocString LOGIC_CONTROLLED_ACTIVE = "Incoming Radbolts: Redirect";

				// Token: 0x0400A991 RID: 43409
				public static LocString NORMAL = "Normal";

				// Token: 0x02002F62 RID: 12130
				public class TOOLTIPS
				{
					// Token: 0x0400BE26 RID: 48678
					public static LocString LOGIC_CONTROLLED_STANDBY = string.Concat(new string[]
					{
						UI.FormatAsKeyWord("Radbolt Reflector"),
						" is receiving a ",
						UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
						", ignoring incoming ",
						UI.PRE_KEYWORD,
						"Radbolts",
						UI.PST_KEYWORD
					});

					// Token: 0x0400BE27 RID: 48679
					public static LocString LOGIC_CONTROLLED_ACTIVE = string.Concat(new string[]
					{
						UI.FormatAsKeyWord("Radbolt Reflector"),
						" is receiving a ",
						UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
						", accepting incoming ",
						UI.PRE_KEYWORD,
						"Radbolts",
						UI.PST_KEYWORD
					});

					// Token: 0x0400BE28 RID: 48680
					public static LocString NORMAL = "Incoming Radbolts will be accepted and redirected";
				}
			}

			// Token: 0x0200273B RID: 10043
			public class HIGHENERGYPARTICLESPAWNER
			{
				// Token: 0x0400A992 RID: 43410
				public static LocString LOGIC_CONTROLLED_STANDBY = "Launch Radbolt: Off";

				// Token: 0x0400A993 RID: 43411
				public static LocString LOGIC_CONTROLLED_ACTIVE = "Launch Radbolt: On";

				// Token: 0x0400A994 RID: 43412
				public static LocString NORMAL = "Normal";

				// Token: 0x02002F63 RID: 12131
				public class TOOLTIPS
				{
					// Token: 0x0400BE29 RID: 48681
					public static LocString LOGIC_CONTROLLED_STANDBY = string.Concat(new string[]
					{
						UI.FormatAsKeyWord("Radbolt Generator"),
						" is receiving a ",
						UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
						", ignoring incoming ",
						UI.PRE_KEYWORD,
						"Radbolts",
						UI.PST_KEYWORD
					});

					// Token: 0x0400BE2A RID: 48682
					public static LocString LOGIC_CONTROLLED_ACTIVE = string.Concat(new string[]
					{
						UI.FormatAsKeyWord("Radbolt Generator"),
						" is receiving a ",
						UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
						", accepting incoming ",
						UI.PRE_KEYWORD,
						"Radbolts",
						UI.PST_KEYWORD
					});

					// Token: 0x0400BE2B RID: 48683
					public static LocString NORMAL = string.Concat(new string[]
					{
						"Incoming ",
						UI.PRE_KEYWORD,
						"Radbolts",
						UI.PST_KEYWORD,
						" will be accepted and redirected"
					});
				}
			}

			// Token: 0x0200273C RID: 10044
			public class AWAITINGFUEL
			{
				// Token: 0x0400A995 RID: 43413
				public static LocString NAME = "Awaiting Fuel: {0}";

				// Token: 0x0400A996 RID: 43414
				public static LocString TOOLTIP = "This building requires <b>{1}</b> of {0} to operate";
			}

			// Token: 0x0200273D RID: 10045
			public class MEGABRAINTANK
			{
				// Token: 0x02002F64 RID: 12132
				public class PROGRESS
				{
					// Token: 0x02003110 RID: 12560
					public class PROGRESSIONRATE
					{
						// Token: 0x0400C2CB RID: 49867
						public static LocString NAME = "Dream Journals: {ActivationProgress}";

						// Token: 0x0400C2CC RID: 49868
						public static LocString TOOLTIP = "Currently awaiting the Dream Journals necessary to restore this building to full functionality";
					}

					// Token: 0x02003111 RID: 12561
					public class DREAMANALYSIS
					{
						// Token: 0x0400C2CD RID: 49869
						public static LocString NAME = "Analyzing Dreams: {TimeToComplete}s";

						// Token: 0x0400C2CE RID: 49870
						public static LocString TOOLTIP = "Maximum Aptitude effect sustained while dream analysis continues";
					}
				}

				// Token: 0x02002F65 RID: 12133
				public class COMPLETE
				{
					// Token: 0x0400BE2C RID: 48684
					public static LocString NAME = "Fully Restored";

					// Token: 0x0400BE2D RID: 48685
					public static LocString TOOLTIP = "This building is functioning at full capacity";
				}
			}

			// Token: 0x0200273E RID: 10046
			public class MEGABRAINNOTENOUGHOXYGEN
			{
				// Token: 0x0400A997 RID: 43415
				public static LocString NAME = "Lacks Oxygen";

				// Token: 0x0400A998 RID: 43416
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building needs ",
					UI.PRE_KEYWORD,
					"Oxygen",
					UI.PST_KEYWORD,
					" in order to function"
				});
			}

			// Token: 0x0200273F RID: 10047
			public class NOLOGICWIRECONNECTED
			{
				// Token: 0x0400A999 RID: 43417
				public static LocString NAME = "No Automation Wire Connected";

				// Token: 0x0400A99A RID: 43418
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building has not been connected to an ",
					UI.PRE_KEYWORD,
					"Automation",
					UI.PST_KEYWORD,
					" grid"
				});
			}

			// Token: 0x02002740 RID: 10048
			public class NOTUBECONNECTED
			{
				// Token: 0x0400A99B RID: 43419
				public static LocString NAME = "No Tube Connected";

				// Token: 0x0400A99C RID: 43420
				public static LocString TOOLTIP = "The first section of tube extending from a " + BUILDINGS.PREFABS.TRAVELTUBEENTRANCE.NAME + " must connect directly upward";
			}

			// Token: 0x02002741 RID: 10049
			public class NOTUBEEXITS
			{
				// Token: 0x0400A99D RID: 43421
				public static LocString NAME = "No Landing Available";

				// Token: 0x0400A99E RID: 43422
				public static LocString TOOLTIP = "Duplicants can only exit a tube when there is somewhere for them to land within <b>two tiles</b>";
			}

			// Token: 0x02002742 RID: 10050
			public class STOREDCHARGE
			{
				// Token: 0x0400A99F RID: 43423
				public static LocString NAME = "Charge Available: {0}/{1}";

				// Token: 0x0400A9A0 RID: 43424
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building has <b>{0}</b> of stored ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					"\n\nIt consumes ",
					UI.FormatAsNegativeRate("{2}"),
					" per use"
				});
			}

			// Token: 0x02002743 RID: 10051
			public class NEEDEGG
			{
				// Token: 0x0400A9A1 RID: 43425
				public static LocString NAME = "No Egg Selected";

				// Token: 0x0400A9A2 RID: 43426
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Collect ",
					UI.PRE_KEYWORD,
					"Eggs",
					UI.PST_KEYWORD,
					" from ",
					UI.FormatAsLink("Critters", "CREATURES"),
					" to incubate"
				});
			}

			// Token: 0x02002744 RID: 10052
			public class NOAVAILABLEEGG
			{
				// Token: 0x0400A9A3 RID: 43427
				public static LocString NAME = "No Egg Available";

				// Token: 0x0400A9A4 RID: 43428
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The selected ",
					UI.PRE_KEYWORD,
					"Egg",
					UI.PST_KEYWORD,
					" is not currently available"
				});
			}

			// Token: 0x02002745 RID: 10053
			public class AWAITINGEGGDELIVERY
			{
				// Token: 0x0400A9A5 RID: 43429
				public static LocString NAME = "Awaiting Delivery";

				// Token: 0x0400A9A6 RID: 43430
				public static LocString TOOLTIP = "Awaiting delivery of selected " + UI.PRE_KEYWORD + "Egg" + UI.PST_KEYWORD;
			}

			// Token: 0x02002746 RID: 10054
			public class INCUBATORPROGRESS
			{
				// Token: 0x0400A9A7 RID: 43431
				public static LocString NAME = "Incubating: {Percent}";

				// Token: 0x0400A9A8 RID: 43432
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Egg",
					UI.PST_KEYWORD,
					" incubating cozily\n\nIt will hatch when ",
					UI.PRE_KEYWORD,
					"Incubation",
					UI.PST_KEYWORD,
					" reaches <b>100%</b>"
				});
			}

			// Token: 0x02002747 RID: 10055
			public class DETECTORQUALITY
			{
				// Token: 0x0400A9A9 RID: 43433
				public static LocString NAME = "Scan Quality: {Quality}";

				// Token: 0x0400A9AA RID: 43434
				public static LocString TOOLTIP = "This scanner dish is currently scanning at <b>{Quality}</b> effectiveness\n\nDecreased scan quality may be due to:\n    • Interference from nearby industrial machinery\n    • Rock or tile obstructing the dish's line of sight on space";
			}

			// Token: 0x02002748 RID: 10056
			public class NETWORKQUALITY
			{
				// Token: 0x0400A9AB RID: 43435
				public static LocString NAME = "Scan Network Quality: {TotalQuality}";

				// Token: 0x0400A9AC RID: 43436
				public static LocString TOOLTIP = "This scanner network is scanning at <b>{TotalQuality}</b> effectiveness\n\nIt will detect incoming objects <b>{WorstTime}</b> to <b>{BestTime}</b> before they arrive\n\nBuild multiple " + BUILDINGS.PREFABS.COMETDETECTOR.NAME + "s and ensure they're each scanning effectively for the best detection results";
			}

			// Token: 0x02002749 RID: 10057
			public class DETECTORSCANNING
			{
				// Token: 0x0400A9AD RID: 43437
				public static LocString NAME = "Scanning";

				// Token: 0x0400A9AE RID: 43438
				public static LocString TOOLTIP = "This scanner is currently scouring space for anything of interest";
			}

			// Token: 0x0200274A RID: 10058
			public class INCOMINGMETEORS
			{
				// Token: 0x0400A9AF RID: 43439
				public static LocString NAME = "Incoming Object Detected";

				// Token: 0x0400A9B0 RID: 43440
				public static LocString TOOLTIP = "Warning!\n\nHigh velocity objects on approach!";
			}

			// Token: 0x0200274B RID: 10059
			public class SPACE_VISIBILITY_NONE
			{
				// Token: 0x0400A9B1 RID: 43441
				public static LocString NAME = "No Line of Sight";

				// Token: 0x0400A9B2 RID: 43442
				public static LocString TOOLTIP = "This building has no view of space\n\nEnsure an unblocked view of the sky is available to collect " + UI.FormatAsManagementMenu("Starmap") + " data\n    • Visibility: <b>{VISIBILITY}</b>\n    • Scan Radius: <b>{RADIUS}</b> cells";
			}

			// Token: 0x0200274C RID: 10060
			public class SPACE_VISIBILITY_REDUCED
			{
				// Token: 0x0400A9B3 RID: 43443
				public static LocString NAME = "Reduced Visibility";

				// Token: 0x0400A9B4 RID: 43444
				public static LocString TOOLTIP = "This building has an inadequate or obscured view of space\n\nEnsure an unblocked view of the sky is available to collect " + UI.FormatAsManagementMenu("Starmap") + " data\n    • Visibility: <b>{VISIBILITY}</b>\n    • Scan Radius: <b>{RADIUS}</b> cells";
			}

			// Token: 0x0200274D RID: 10061
			public class LANDEDROCKETLACKSPASSENGERMODULE
			{
				// Token: 0x0400A9B5 RID: 43445
				public static LocString NAME = "Rocket lacks spacefarer module";

				// Token: 0x0400A9B6 RID: 43446
				public static LocString TOOLTIP = "A rocket must have a spacefarer module";
			}

			// Token: 0x0200274E RID: 10062
			public class PATH_NOT_CLEAR
			{
				// Token: 0x0400A9B7 RID: 43447
				public static LocString NAME = "Launch Path Blocked";

				// Token: 0x0400A9B8 RID: 43448
				public static LocString TOOLTIP = "There are obstructions in the launch trajectory of this rocket:\n    • {0}\n\nThis rocket requires a clear flight path for launch";

				// Token: 0x0400A9B9 RID: 43449
				public static LocString TILE_FORMAT = "Solid {0}";
			}

			// Token: 0x0200274F RID: 10063
			public class RAILGUN_PATH_NOT_CLEAR
			{
				// Token: 0x0400A9BA RID: 43450
				public static LocString NAME = "Launch Path Blocked";

				// Token: 0x0400A9BB RID: 43451
				public static LocString TOOLTIP = "There are obstructions in the launch trajectory of this " + UI.FormatAsLink("Interplanetary Launcher", "RAILGUN") + ":\n    • {0}\n\nThis launcher requires a clear path to launch payloads";
			}

			// Token: 0x02002750 RID: 10064
			public class RAILGUN_NO_DESTINATION
			{
				// Token: 0x0400A9BC RID: 43452
				public static LocString NAME = "No Delivery Destination";

				// Token: 0x0400A9BD RID: 43453
				public static LocString TOOLTIP = "A delivery destination has not been set    • {0}";
			}

			// Token: 0x02002751 RID: 10065
			public class NOSURFACESIGHT
			{
				// Token: 0x0400A9BE RID: 43454
				public static LocString NAME = "No Line of Sight";

				// Token: 0x0400A9BF RID: 43455
				public static LocString TOOLTIP = "This building has no view of space\n\nTo properly function, this building requires an unblocked view of space";
			}

			// Token: 0x02002752 RID: 10066
			public class ROCKETRESTRICTIONACTIVE
			{
				// Token: 0x0400A9C0 RID: 43456
				public static LocString NAME = "Access: Restricted";

				// Token: 0x0400A9C1 RID: 43457
				public static LocString TOOLTIP = "This building cannot be operated while restricted, though it can be filled\n\nControlled by a " + BUILDINGS.PREFABS.ROCKETCONTROLSTATION.NAME;
			}

			// Token: 0x02002753 RID: 10067
			public class ROCKETRESTRICTIONINACTIVE
			{
				// Token: 0x0400A9C2 RID: 43458
				public static LocString NAME = "Access: Not Restricted";

				// Token: 0x0400A9C3 RID: 43459
				public static LocString TOOLTIP = "This building's operation is not restricted\n\nControlled by a " + BUILDINGS.PREFABS.ROCKETCONTROLSTATION.NAME;
			}

			// Token: 0x02002754 RID: 10068
			public class NOROCKETRESTRICTION
			{
				// Token: 0x0400A9C4 RID: 43460
				public static LocString NAME = "Not Controlled";

				// Token: 0x0400A9C5 RID: 43461
				public static LocString TOOLTIP = "This building is not controlled by a " + BUILDINGS.PREFABS.ROCKETCONTROLSTATION.NAME;
			}

			// Token: 0x02002755 RID: 10069
			public class BROADCASTEROUTOFRANGE
			{
				// Token: 0x0400A9C6 RID: 43462
				public static LocString NAME = "Broadcaster Out of Range";

				// Token: 0x0400A9C7 RID: 43463
				public static LocString TOOLTIP = "This receiver is too far from the selected broadcaster to get signal updates";
			}

			// Token: 0x02002756 RID: 10070
			public class LOSINGRADBOLTS
			{
				// Token: 0x0400A9C8 RID: 43464
				public static LocString NAME = "Radbolt Decay";

				// Token: 0x0400A9C9 RID: 43465
				public static LocString TOOLTIP = "This building is unable to maintain the integrity of the radbolts it is storing";
			}

			// Token: 0x02002757 RID: 10071
			public class TOP_PRIORITY_CHORE
			{
				// Token: 0x0400A9CA RID: 43466
				public static LocString NAME = "Top Priority";

				// Token: 0x0400A9CB RID: 43467
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This errand has been set to ",
					UI.PRE_KEYWORD,
					"Top Priority",
					UI.PST_KEYWORD,
					"\n\nThe colony will be in ",
					UI.PRE_KEYWORD,
					"Yellow Alert",
					UI.PST_KEYWORD,
					" until this task is completed"
				});

				// Token: 0x0400A9CC RID: 43468
				public static LocString NOTIFICATION_NAME = "Yellow Alert";

				// Token: 0x0400A9CD RID: 43469
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"The following errands have been set to ",
					UI.PRE_KEYWORD,
					"Top Priority",
					UI.PST_KEYWORD,
					":"
				});
			}

			// Token: 0x02002758 RID: 10072
			public class HOTTUBWATERTOOCOLD
			{
				// Token: 0x0400A9CE RID: 43470
				public static LocString NAME = "Water Too Cold";

				// Token: 0x0400A9CF RID: 43471
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This tub's ",
					UI.PRE_KEYWORD,
					"Water",
					UI.PST_KEYWORD,
					" is below <b>{temperature}</b>\n\nIt is draining so it can be refilled with warmer ",
					UI.PRE_KEYWORD,
					"Water",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002759 RID: 10073
			public class HOTTUBTOOHOT
			{
				// Token: 0x0400A9D0 RID: 43472
				public static LocString NAME = "Building Too Hot";

				// Token: 0x0400A9D1 RID: 43473
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This tub's ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" is above <b>{temperature}</b>\n\nIt needs to cool before it can safely be used"
				});
			}

			// Token: 0x0200275A RID: 10074
			public class HOTTUBFILLING
			{
				// Token: 0x0400A9D2 RID: 43474
				public static LocString NAME = "Filling Up: ({fullness})";

				// Token: 0x0400A9D3 RID: 43475
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This tub is currently filling with ",
					UI.PRE_KEYWORD,
					"Water",
					UI.PST_KEYWORD,
					"\n\nIt will be available to use when the ",
					UI.PRE_KEYWORD,
					"Water",
					UI.PST_KEYWORD,
					" level reaches <b>100%</b>"
				});
			}

			// Token: 0x0200275B RID: 10075
			public class WINDTUNNELINTAKE
			{
				// Token: 0x0400A9D4 RID: 43476
				public static LocString NAME = "Intake Requires Gas";

				// Token: 0x0400A9D5 RID: 43477
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A wind tunnel requires ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" at the top and bottom intakes in order to operate\n\nThe intakes for this wind tunnel don't have enough gas to operate"
				});
			}

			// Token: 0x0200275C RID: 10076
			public class TEMPORAL_TEAR_OPENER_NO_TARGET
			{
				// Token: 0x0400A9D6 RID: 43478
				public static LocString NAME = "Temporal Tear not revealed";

				// Token: 0x0400A9D7 RID: 43479
				public static LocString TOOLTIP = "This machine is meant to target something in space, but the target has not yet been revealed";
			}

			// Token: 0x0200275D RID: 10077
			public class TEMPORAL_TEAR_OPENER_NO_LOS
			{
				// Token: 0x0400A9D8 RID: 43480
				public static LocString NAME = "Line of Sight: Obstructed";

				// Token: 0x0400A9D9 RID: 43481
				public static LocString TOOLTIP = "This device needs a clear view of space to operate";
			}

			// Token: 0x0200275E RID: 10078
			public class TEMPORAL_TEAR_OPENER_INSUFFICIENT_COLONIES
			{
				// Token: 0x0400A9DA RID: 43482
				public static LocString NAME = "Too few Printing Pods {progress}";

				// Token: 0x0400A9DB RID: 43483
				public static LocString TOOLTIP = "To open the Temporal Tear, this device relies on a network of activated Printing Pods {progress}";
			}

			// Token: 0x0200275F RID: 10079
			public class TEMPORAL_TEAR_OPENER_PROGRESS
			{
				// Token: 0x0400A9DC RID: 43484
				public static LocString NAME = "Charging Progress: {progress}";

				// Token: 0x0400A9DD RID: 43485
				public static LocString TOOLTIP = "This device must be charged with a high number of Radbolts\n\nOperation can commence once this device is fully charged";
			}

			// Token: 0x02002760 RID: 10080
			public class TEMPORAL_TEAR_OPENER_READY
			{
				// Token: 0x0400A9DE RID: 43486
				public static LocString NOTIFICATION = "Temporal Tear Opener fully charged";

				// Token: 0x0400A9DF RID: 43487
				public static LocString NOTIFICATION_TOOLTIP = "Push the red button to activate";
			}

			// Token: 0x02002761 RID: 10081
			public class WARPPORTALCHARGING
			{
				// Token: 0x0400A9E0 RID: 43488
				public static LocString NAME = "Recharging: {charge}";

				// Token: 0x0400A9E1 RID: 43489
				public static LocString TOOLTIP = "This teleporter will be ready for use in {cycles} cycles";
			}

			// Token: 0x02002762 RID: 10082
			public class WARPCONDUITPARTNERDISABLED
			{
				// Token: 0x0400A9E2 RID: 43490
				public static LocString NAME = "Teleporter Disabled ({x}/2)";

				// Token: 0x0400A9E3 RID: 43491
				public static LocString TOOLTIP = "This teleporter cannot be used until both the transmitting and receiving sides have been activated";
			}

			// Token: 0x02002763 RID: 10083
			public class COLLECTINGHEP
			{
				// Token: 0x0400A9E4 RID: 43492
				public static LocString NAME = "Collecting Radbolts ({x}/cycle)";

				// Token: 0x0400A9E5 RID: 43493
				public static LocString TOOLTIP = "Collecting Radbolts from ambient radiation";
			}

			// Token: 0x02002764 RID: 10084
			public class INORBIT
			{
				// Token: 0x0400A9E6 RID: 43494
				public static LocString NAME = "In Orbit: {Destination}";

				// Token: 0x0400A9E7 RID: 43495
				public static LocString TOOLTIP = "This rocket is currently in orbit around {Destination}";
			}

			// Token: 0x02002765 RID: 10085
			public class INFLIGHT
			{
				// Token: 0x0400A9E8 RID: 43496
				public static LocString NAME = "In Flight To {Destination_Asteroid}: {ETA}";

				// Token: 0x0400A9E9 RID: 43497
				public static LocString TOOLTIP = "This rocket is currently traveling to {Destination_Pad} on {Destination_Asteroid}\n\nIt will arrive in {ETA}";

				// Token: 0x0400A9EA RID: 43498
				public static LocString TOOLTIP_NO_PAD = "This rocket is currently traveling to {Destination_Asteroid}\n\nIt will arrive in {ETA}";
			}

			// Token: 0x02002766 RID: 10086
			public class DESTINATIONOUTOFRANGE
			{
				// Token: 0x0400A9EB RID: 43499
				public static LocString NAME = "Destination Out Of Range";

				// Token: 0x0400A9EC RID: 43500
				public static LocString TOOLTIP = "This rocket lacks the range to reach its destination\n\nRocket Range: {Range}\nDestination Distance: {Distance}";
			}

			// Token: 0x02002767 RID: 10087
			public class ROCKETSTRANDED
			{
				// Token: 0x0400A9ED RID: 43501
				public static LocString NAME = "Stranded";

				// Token: 0x0400A9EE RID: 43502
				public static LocString TOOLTIP = "This rocket has run out of fuel and cannot move";
			}

			// Token: 0x02002768 RID: 10088
			public class SPACEPOIHARVESTING
			{
				// Token: 0x0400A9EF RID: 43503
				public static LocString NAME = "Extracting Resources: {0}";

				// Token: 0x0400A9F0 RID: 43504
				public static LocString TOOLTIP = "Resources are being mined from this space debris";
			}

			// Token: 0x02002769 RID: 10089
			public class SPACEPOIWASTING
			{
				// Token: 0x0400A9F1 RID: 43505
				public static LocString NAME = "Cannot store resources: {0}";

				// Token: 0x0400A9F2 RID: 43506
				public static LocString TOOLTIP = "Some resources being mined from this space debris cannot be stored in this rocket";
			}

			// Token: 0x0200276A RID: 10090
			public class RAILGUNPAYLOADNEEDSEMPTYING
			{
				// Token: 0x0400A9F3 RID: 43507
				public static LocString NAME = "Ready To Unpack";

				// Token: 0x0400A9F4 RID: 43508
				public static LocString TOOLTIP = "This payload has reached its destination and is ready to be unloaded\n\nIt can be marked for unpacking manually, or automatically unpacked on arrival using a " + BUILDINGS.PREFABS.RAILGUNPAYLOADOPENER.NAME;
			}

			// Token: 0x0200276B RID: 10091
			public class MISSIONCONTROLASSISTINGROCKET
			{
				// Token: 0x0400A9F5 RID: 43509
				public static LocString NAME = "Guidance Signal: {0}";

				// Token: 0x0400A9F6 RID: 43510
				public static LocString TOOLTIP = "Once transmission is complete, Mission Control will boost targeted rocket's speed";
			}

			// Token: 0x0200276C RID: 10092
			public class MISSIONCONTROLBOOSTED
			{
				// Token: 0x0400A9F7 RID: 43511
				public static LocString NAME = "Mission Control Speed Boost: {0}";

				// Token: 0x0400A9F8 RID: 43512
				public static LocString TOOLTIP = "Mission Control has given this rocket a {0} speed boost\n\n{1} remaining";
			}

			// Token: 0x0200276D RID: 10093
			public class NOROCKETSTOMISSIONCONTROLBOOST
			{
				// Token: 0x0400A9F9 RID: 43513
				public static LocString NAME = "No Eligible Rockets in Range";

				// Token: 0x0400A9FA RID: 43514
				public static LocString TOOLTIP = "Rockets must be mid-flight and not targeted by another Mission Control Station, or already boosted";
			}

			// Token: 0x0200276E RID: 10094
			public class NOROCKETSTOMISSIONCONTROLCLUSTERBOOST
			{
				// Token: 0x0400A9FB RID: 43515
				public static LocString NAME = "No Eligible Rockets in Range";

				// Token: 0x0400A9FC RID: 43516
				public static LocString TOOLTIP = "Rockets must be mid-flight, within {0} tiles, and not targeted by another Mission Control Station or already boosted";
			}

			// Token: 0x0200276F RID: 10095
			public class AWAITINGEMPTYBUILDING
			{
				// Token: 0x0400A9FD RID: 43517
				public static LocString NAME = "Empty Errand";

				// Token: 0x0400A9FE RID: 43518
				public static LocString TOOLTIP = "Building will be emptied once a Duplicant is available";
			}

			// Token: 0x02002770 RID: 10096
			public class DUPLICANTACTIVATIONREQUIRED
			{
				// Token: 0x0400A9FF RID: 43519
				public static LocString NAME = "Activation Required";

				// Token: 0x0400AA00 RID: 43520
				public static LocString TOOLTIP = "A Duplicant is required to bring this building online";
			}

			// Token: 0x02002771 RID: 10097
			public class PILOTNEEDED
			{
				// Token: 0x0400AA01 RID: 43521
				public static LocString NAME = "Switching to Autopilot";

				// Token: 0x0400AA02 RID: 43522
				public static LocString TOOLTIP = "Autopilot will engage in {timeRemaining} if a Duplicant pilot does not assume control";
			}

			// Token: 0x02002772 RID: 10098
			public class AUTOPILOTACTIVE
			{
				// Token: 0x0400AA03 RID: 43523
				public static LocString NAME = "Autopilot Engaged";

				// Token: 0x0400AA04 RID: 43524
				public static LocString TOOLTIP = "This rocket has entered autopilot mode and will fly at reduced speed\n\nIt can resume full speed once a Duplicant pilot takes over";
			}

			// Token: 0x02002773 RID: 10099
			public class ROCKETCHECKLISTINCOMPLETE
			{
				// Token: 0x0400AA05 RID: 43525
				public static LocString NAME = "Launch Checklist Incomplete";

				// Token: 0x0400AA06 RID: 43526
				public static LocString TOOLTIP = "Critical launch tasks uncompleted\n\nRefer to the Launch Checklist in the status panel";
			}

			// Token: 0x02002774 RID: 10100
			public class ROCKETCARGOEMPTYING
			{
				// Token: 0x0400AA07 RID: 43527
				public static LocString NAME = "Unloading Cargo";

				// Token: 0x0400AA08 RID: 43528
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Rocket cargo is being unloaded into the ",
					UI.PRE_KEYWORD,
					"Rocket Platform",
					UI.PST_KEYWORD,
					"\n\nLoading of new cargo will begin once unloading is complete"
				});
			}

			// Token: 0x02002775 RID: 10101
			public class ROCKETCARGOFILLING
			{
				// Token: 0x0400AA09 RID: 43529
				public static LocString NAME = "Loading Cargo";

				// Token: 0x0400AA0A RID: 43530
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Cargo is being loaded onto the rocket from the ",
					UI.PRE_KEYWORD,
					"Rocket Platform",
					UI.PST_KEYWORD,
					"\n\nRocket cargo will be ready for launch once loading is complete"
				});
			}

			// Token: 0x02002776 RID: 10102
			public class ROCKETCARGOFULL
			{
				// Token: 0x0400AA0B RID: 43531
				public static LocString NAME = "Platform Ready";

				// Token: 0x0400AA0C RID: 43532
				public static LocString TOOLTIP = "All cargo operations are complete";
			}

			// Token: 0x02002777 RID: 10103
			public class FLIGHTALLCARGOFULL
			{
				// Token: 0x0400AA0D RID: 43533
				public static LocString NAME = "All cargo bays are full";

				// Token: 0x0400AA0E RID: 43534
				public static LocString TOOLTIP = "Rocket cannot store any more materials";
			}

			// Token: 0x02002778 RID: 10104
			public class FLIGHTCARGOREMAINING
			{
				// Token: 0x0400AA0F RID: 43535
				public static LocString NAME = "Cargo capacity remaining: {0}";

				// Token: 0x0400AA10 RID: 43536
				public static LocString TOOLTIP = "Rocket can store up to {0} more materials";
			}

			// Token: 0x02002779 RID: 10105
			public class ROCKET_PORT_IDLE
			{
				// Token: 0x0400AA11 RID: 43537
				public static LocString NAME = "Idle";

				// Token: 0x0400AA12 RID: 43538
				public static LocString TOOLTIP = "This port is idle because there is no rocket on the connected " + UI.PRE_KEYWORD + "Rocket Platform" + UI.PST_KEYWORD;
			}

			// Token: 0x0200277A RID: 10106
			public class ROCKET_PORT_UNLOADING
			{
				// Token: 0x0400AA13 RID: 43539
				public static LocString NAME = "Unloading Rocket";

				// Token: 0x0400AA14 RID: 43540
				public static LocString TOOLTIP = "Resources are being unloaded from the rocket into the local network";
			}

			// Token: 0x0200277B RID: 10107
			public class ROCKET_PORT_LOADING
			{
				// Token: 0x0400AA15 RID: 43541
				public static LocString NAME = "Loading Rocket";

				// Token: 0x0400AA16 RID: 43542
				public static LocString TOOLTIP = "Resources are being loaded from the local network into the rocket's storage";
			}

			// Token: 0x0200277C RID: 10108
			public class ROCKET_PORT_LOADED
			{
				// Token: 0x0400AA17 RID: 43543
				public static LocString NAME = "Cargo Transfer Complete";

				// Token: 0x0400AA18 RID: 43544
				public static LocString TOOLTIP = "The connected rocket has either reached max capacity for this resource type, or lacks appropriate storage modules";
			}

			// Token: 0x0200277D RID: 10109
			public class CONNECTED_ROCKET_PORT
			{
				// Token: 0x0400AA19 RID: 43545
				public static LocString NAME = "Port Network Attached";

				// Token: 0x0400AA1A RID: 43546
				public static LocString TOOLTIP = "This module has been connected to a " + BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME + " and can now load and unload cargo";
			}

			// Token: 0x0200277E RID: 10110
			public class CONNECTED_ROCKET_WRONG_PORT
			{
				// Token: 0x0400AA1B RID: 43547
				public static LocString NAME = "Incorrect Port Network";

				// Token: 0x0400AA1C RID: 43548
				public static LocString TOOLTIP = "The attached " + BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME + " is not the correct type for this cargo module";
			}

			// Token: 0x0200277F RID: 10111
			public class CONNECTED_ROCKET_NO_PORT
			{
				// Token: 0x0400AA1D RID: 43549
				public static LocString NAME = "No Rocket Ports";

				// Token: 0x0400AA1E RID: 43550
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Rocket Platform",
					UI.PST_KEYWORD,
					" has no ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME,
					" attached\n\n",
					UI.PRE_KEYWORD,
					"Solid",
					UI.PST_KEYWORD,
					", ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					", and ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME_PLURAL,
					" can be attached to load and unload cargo from a landed rocket's modules"
				});
			}

			// Token: 0x02002780 RID: 10112
			public class CLUSTERTELESCOPEALLWORKCOMPLETE
			{
				// Token: 0x0400AA1F RID: 43551
				public static LocString NAME = "Area Complete";

				// Token: 0x0400AA20 RID: 43552
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Telescope",
					UI.PST_KEYWORD,
					" has analyzed all the space visible from its current location"
				});
			}

			// Token: 0x02002781 RID: 10113
			public class ROCKETPLATFORMCLOSETOCEILING
			{
				// Token: 0x0400AA21 RID: 43553
				public static LocString NAME = "Low Clearance: {distance} Tiles";

				// Token: 0x0400AA22 RID: 43554
				public static LocString TOOLTIP = "Tall rockets may not be able to land on this " + UI.PRE_KEYWORD + "Rocket Platform" + UI.PST_KEYWORD;
			}

			// Token: 0x02002782 RID: 10114
			public class MODULEGENERATORNOTPOWERED
			{
				// Token: 0x0400AA23 RID: 43555
				public static LocString NAME = "Thrust Generation: {ActiveWattage}/{MaxWattage}";

				// Token: 0x0400AA24 RID: 43556
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Engine will generate ",
					UI.FormatAsPositiveRate("{MaxWattage}"),
					" of ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" once traveling through space\n\nRight now, it's not doing much of anything"
				});
			}

			// Token: 0x02002783 RID: 10115
			public class MODULEGENERATORPOWERED
			{
				// Token: 0x0400AA25 RID: 43557
				public static LocString NAME = "Thrust Generation: {ActiveWattage}/{MaxWattage}";

				// Token: 0x0400AA26 RID: 43558
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Engine is extracting ",
					UI.FormatAsPositiveRate("{MaxWattage}"),
					" of ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" from the thruster\n\nIt will continue generating power as long as it travels through space"
				});
			}

			// Token: 0x02002784 RID: 10116
			public class INORBITREQUIRED
			{
				// Token: 0x0400AA27 RID: 43559
				public static LocString NAME = "Grounded";

				// Token: 0x0400AA28 RID: 43560
				public static LocString TOOLTIP = "This building cannot operate from the surface of a " + UI.CLUSTERMAP.PLANETOID_KEYWORD + " and must be in space to function";
			}

			// Token: 0x02002785 RID: 10117
			public class REACTORREFUELDISABLED
			{
				// Token: 0x0400AA29 RID: 43561
				public static LocString NAME = "Refuel Disabled";

				// Token: 0x0400AA2A RID: 43562
				public static LocString TOOLTIP = "This building will not be refueled once its active fuel has been consumed";
			}

			// Token: 0x02002786 RID: 10118
			public class RAILGUNCOOLDOWN
			{
				// Token: 0x0400AA2B RID: 43563
				public static LocString NAME = "Cleaning Rails: {timeleft}";

				// Token: 0x0400AA2C RID: 43564
				public static LocString TOOLTIP = "This building automatically performs routine maintenance every {x} launches";
			}

			// Token: 0x02002787 RID: 10119
			public class FRIDGECOOLING
			{
				// Token: 0x0400AA2D RID: 43565
				public static LocString NAME = "Cooling Contents: {UsedPower}";

				// Token: 0x0400AA2E RID: 43566
				public static LocString TOOLTIP = "{UsedPower} of {MaxPower} are being used to cool the contents of this food storage";
			}

			// Token: 0x02002788 RID: 10120
			public class FRIDGESTEADY
			{
				// Token: 0x0400AA2F RID: 43567
				public static LocString NAME = "Energy Saver: {UsedPower}";

				// Token: 0x0400AA30 RID: 43568
				public static LocString TOOLTIP = "The contents of this food storage are at refrigeration temperatures\n\nEnergy Saver mode has been automatically activated using only {UsedPower} of {MaxPower}";
			}

			// Token: 0x02002789 RID: 10121
			public class TELEPHONE
			{
				// Token: 0x02002F66 RID: 12134
				public class BABBLE
				{
					// Token: 0x0400BE2E RID: 48686
					public static LocString NAME = "Babbling to no one.";

					// Token: 0x0400BE2F RID: 48687
					public static LocString TOOLTIP = "{Duplicant} just needed to vent to into the void.";
				}

				// Token: 0x02002F67 RID: 12135
				public class CONVERSATION
				{
					// Token: 0x0400BE30 RID: 48688
					public static LocString TALKING_TO = "Talking to {Duplicant} on {Asteroid}";

					// Token: 0x0400BE31 RID: 48689
					public static LocString TALKING_TO_NUM = "Talking to {0} friends.";
				}
			}

			// Token: 0x0200278A RID: 10122
			public class CREATUREMANIPULATORPROGRESS
			{
				// Token: 0x0400AA31 RID: 43569
				public static LocString NAME = "Collected Species Data {0}/{1}";

				// Token: 0x0400AA32 RID: 43570
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building requires data from multiple ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					" species to unlock its genetic manipulator\n\nSpecies scanned:"
				});

				// Token: 0x0400AA33 RID: 43571
				public static LocString NO_DATA = "No species scanned";
			}

			// Token: 0x0200278B RID: 10123
			public class CREATUREMANIPULATORMORPHMODELOCKED
			{
				// Token: 0x0400AA34 RID: 43572
				public static LocString NAME = "Current Status: Offline";

				// Token: 0x0400AA35 RID: 43573
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building cannot operate until it collects more ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					" DNA"
				});
			}

			// Token: 0x0200278C RID: 10124
			public class CREATUREMANIPULATORMORPHMODE
			{
				// Token: 0x0400AA36 RID: 43574
				public static LocString NAME = "Current Status: Online";

				// Token: 0x0400AA37 RID: 43575
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building is ready to manipulate ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					" DNA"
				});
			}

			// Token: 0x0200278D RID: 10125
			public class CREATUREMANIPULATORWAITING
			{
				// Token: 0x0400AA38 RID: 43576
				public static LocString NAME = "Waiting for a Critter";

				// Token: 0x0400AA39 RID: 43577
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building is waiting for a ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					" to get sucked into its scanning area"
				});
			}

			// Token: 0x0200278E RID: 10126
			public class CREATUREMANIPULATORWORKING
			{
				// Token: 0x0400AA3A RID: 43578
				public static LocString NAME = "Poking and Prodding Critter";

				// Token: 0x0400AA3B RID: 43579
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building is extracting genetic information from a ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					" "
				});
			}

			// Token: 0x0200278F RID: 10127
			public class SPICEGRINDERNOSPICE
			{
				// Token: 0x0400AA3C RID: 43580
				public static LocString NAME = "No Spice Selected";

				// Token: 0x0400AA3D RID: 43581
				public static LocString TOOLTIP = "Select a recipe to begin fabrication";
			}
		}

		// Token: 0x02001CCA RID: 7370
		public class DETAILS
		{
			// Token: 0x0400837D RID: 33661
			public static LocString USE_COUNT = "Uses: {0}";

			// Token: 0x0400837E RID: 33662
			public static LocString USE_COUNT_TOOLTIP = "This building has been used {0} times";
		}
	}
}
