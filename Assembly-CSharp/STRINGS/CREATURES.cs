using System;

namespace STRINGS
{
	// Token: 0x02000D42 RID: 3394
	public class CREATURES
	{
		// Token: 0x04004E48 RID: 20040
		public static LocString BAGGED_NAME_FMT = "Bagged {0}";

		// Token: 0x04004E49 RID: 20041
		public static LocString BAGGED_DESC_FMT = "This {0} has been captured and is now safe to relocate.";

		// Token: 0x02001CD0 RID: 7376
		public class FAMILY
		{
			// Token: 0x040083ED RID: 33773
			public static LocString HATCH = UI.FormatAsLink("Hatch", "HATCHSPECIES");

			// Token: 0x040083EE RID: 33774
			public static LocString LIGHTBUG = UI.FormatAsLink("Shine Bug", "LIGHTBUGSPECIES");

			// Token: 0x040083EF RID: 33775
			public static LocString OILFLOATER = UI.FormatAsLink("Slickster", "OILFLOATERSPECIES");

			// Token: 0x040083F0 RID: 33776
			public static LocString DRECKO = UI.FormatAsLink("Drecko", "DRECKOSPECIES");

			// Token: 0x040083F1 RID: 33777
			public static LocString GLOM = UI.FormatAsLink("Morb", "GLOMSPECIES");

			// Token: 0x040083F2 RID: 33778
			public static LocString PUFT = UI.FormatAsLink("Puft", "PUFTSPECIES");

			// Token: 0x040083F3 RID: 33779
			public static LocString PACU = UI.FormatAsLink("Pacu", "PACUSPECIES");

			// Token: 0x040083F4 RID: 33780
			public static LocString MOO = UI.FormatAsLink("Moo", "MOOSPECIES");

			// Token: 0x040083F5 RID: 33781
			public static LocString MOLE = UI.FormatAsLink("Shove Vole", "MOLESPECIES");

			// Token: 0x040083F6 RID: 33782
			public static LocString SQUIRREL = UI.FormatAsLink("Pip", "SQUIRRELSPECIES");

			// Token: 0x040083F7 RID: 33783
			public static LocString CRAB = UI.FormatAsLink("Pokeshell", "CRABSPECIES");

			// Token: 0x040083F8 RID: 33784
			public static LocString STATERPILLAR = UI.FormatAsLink("Plug Slug", "STATERPILLARSPECIES");

			// Token: 0x040083F9 RID: 33785
			public static LocString DIVERGENTSPECIES = UI.FormatAsLink("Divergent", "DIVERGENTSPECIES");

			// Token: 0x040083FA RID: 33786
			public static LocString SWEEPBOT = UI.FormatAsLink("Sweepies", "SWEEPBOT");

			// Token: 0x040083FB RID: 33787
			public static LocString SCOUTROVER = UI.FormatAsLink("Rover", "SCOUTROVER");
		}

		// Token: 0x02001CD1 RID: 7377
		public class FAMILY_PLURAL
		{
			// Token: 0x040083FC RID: 33788
			public static LocString HATCHSPECIES = UI.FormatAsLink("Hatches", "HATCHSPECIES");

			// Token: 0x040083FD RID: 33789
			public static LocString LIGHTBUGSPECIES = UI.FormatAsLink("Shine Bugs", "LIGHTBUGSPECIES");

			// Token: 0x040083FE RID: 33790
			public static LocString OILFLOATERSPECIES = UI.FormatAsLink("Slicksters", "OILFLOATERSPECIES");

			// Token: 0x040083FF RID: 33791
			public static LocString DRECKOSPECIES = UI.FormatAsLink("Dreckos", "DRECKOSPECIES");

			// Token: 0x04008400 RID: 33792
			public static LocString GLOMSPECIES = UI.FormatAsLink("Morbs", "GLOMSPECIES");

			// Token: 0x04008401 RID: 33793
			public static LocString PUFTSPECIES = UI.FormatAsLink("Pufts", "PUFTSPECIES");

			// Token: 0x04008402 RID: 33794
			public static LocString PACUSPECIES = UI.FormatAsLink("Pacus", "PACUSPECIES");

			// Token: 0x04008403 RID: 33795
			public static LocString MOOSPECIES = UI.FormatAsLink("Moos", "MOOSPECIES");

			// Token: 0x04008404 RID: 33796
			public static LocString MOLESPECIES = UI.FormatAsLink("Shove Voles", "MOLESPECIES");

			// Token: 0x04008405 RID: 33797
			public static LocString CRABSPECIES = UI.FormatAsLink("Pokeshells", "CRABSPECIES");

			// Token: 0x04008406 RID: 33798
			public static LocString SQUIRRELSPECIES = UI.FormatAsLink("Pips", "SQUIRRELSPECIES");

			// Token: 0x04008407 RID: 33799
			public static LocString STATERPILLARSPECIES = UI.FormatAsLink("Plug Slugs", "STATERPILLARSPECIES");

			// Token: 0x04008408 RID: 33800
			public static LocString BEETASPECIES = UI.FormatAsLink("Beetas", "BEETASPECIES");

			// Token: 0x04008409 RID: 33801
			public static LocString DIVERGENTSPECIES = UI.FormatAsLink("Divergents", "DIVERGENTSPECIES");

			// Token: 0x0400840A RID: 33802
			public static LocString SWEEPBOT = UI.FormatAsLink("Sweepies", "SWEEPBOT");

			// Token: 0x0400840B RID: 33803
			public static LocString SCOUTROVER = UI.FormatAsLink("Rovers", "SCOUTROVER");
		}

		// Token: 0x02001CD2 RID: 7378
		public class PLANT_MUTATIONS
		{
			// Token: 0x0400840C RID: 33804
			public static LocString PLANT_NAME_FMT = "{PlantName} ({MutationList})";

			// Token: 0x0400840D RID: 33805
			public static LocString UNIDENTIFIED = "Unidentified Subspecies";

			// Token: 0x0400840E RID: 33806
			public static LocString UNIDENTIFIED_DESC = "This seed must be identified at the " + BUILDINGS.PREFABS.GENETICANALYSISSTATION.NAME + " before it can be planted.";

			// Token: 0x0400840F RID: 33807
			public static LocString BONUS_CROP_FMT = "Bonus Crop: +{Amount} {Crop}";

			// Token: 0x02002799 RID: 10137
			public class NONE
			{
				// Token: 0x0400AA98 RID: 43672
				public static LocString NAME = "Original";
			}

			// Token: 0x0200279A RID: 10138
			public class MODERATELYLOOSE
			{
				// Token: 0x0400AA99 RID: 43673
				public static LocString NAME = "Easygoing";

				// Token: 0x0400AA9A RID: 43674
				public static LocString DESCRIPTION = "Plants with this mutation are easier to take care of, but don't yield as much produce.";
			}

			// Token: 0x0200279B RID: 10139
			public class MODERATELYTIGHT
			{
				// Token: 0x0400AA9B RID: 43675
				public static LocString NAME = "Specialized";

				// Token: 0x0400AA9C RID: 43676
				public static LocString DESCRIPTION = "Plants with this mutation are pickier about their conditions but yield more produce.";
			}

			// Token: 0x0200279C RID: 10140
			public class EXTREMELYTIGHT
			{
				// Token: 0x0400AA9D RID: 43677
				public static LocString NAME = "Superspecialized";

				// Token: 0x0400AA9E RID: 43678
				public static LocString DESCRIPTION = "Plants with this mutation are very difficult to keep alive, but produce a bounty.";
			}

			// Token: 0x0200279D RID: 10141
			public class BONUSLICE
			{
				// Token: 0x0400AA9F RID: 43679
				public static LocString NAME = "Licey";

				// Token: 0x0400AAA0 RID: 43680
				public static LocString DESCRIPTION = "Something about this mutation causes Meal Lice to pupate on this plant.";
			}

			// Token: 0x0200279E RID: 10142
			public class SUNNYSPEED
			{
				// Token: 0x0400AAA1 RID: 43681
				public static LocString NAME = "Leafy";

				// Token: 0x0400AAA2 RID: 43682
				public static LocString DESCRIPTION = "This mutation provides the plant with sun-collecting leaves, allowing faster growth.";
			}

			// Token: 0x0200279F RID: 10143
			public class SLOWBURN
			{
				// Token: 0x0400AAA3 RID: 43683
				public static LocString NAME = "Wildish";

				// Token: 0x0400AAA4 RID: 43684
				public static LocString DESCRIPTION = "These plants grow almost as slow as their wild cousins, but also consume almost no fertilizer.";
			}

			// Token: 0x020027A0 RID: 10144
			public class BLOOMS
			{
				// Token: 0x0400AAA5 RID: 43685
				public static LocString NAME = "Blooming";

				// Token: 0x0400AAA6 RID: 43686
				public static LocString DESCRIPTION = "Vestigial flowers increase the beauty of this plant. Don't inhale the pollen, though!";
			}

			// Token: 0x020027A1 RID: 10145
			public class LOADEDWITHFRUIT
			{
				// Token: 0x0400AAA7 RID: 43687
				public static LocString NAME = "Bountiful";

				// Token: 0x0400AAA8 RID: 43688
				public static LocString DESCRIPTION = "This mutation produces lots of extra produce, though it also takes a long time to pick it all!";
			}

			// Token: 0x020027A2 RID: 10146
			public class ROTTENHEAPS
			{
				// Token: 0x0400AAA9 RID: 43689
				public static LocString NAME = "Exuberant";

				// Token: 0x0400AAAA RID: 43690
				public static LocString DESCRIPTION = "Plants with this mutation grow extremely quickly, though the produce they make is sometimes questionable.";
			}

			// Token: 0x020027A3 RID: 10147
			public class HEAVYFRUIT
			{
				// Token: 0x0400AAAB RID: 43691
				public static LocString NAME = "Juicy Fruits";

				// Token: 0x0400AAAC RID: 43692
				public static LocString DESCRIPTION = "Extra water in these plump mutant veggies causes them to fall right off the plant! There's no extra nutritional value, though...";
			}
		}

		// Token: 0x02001CD3 RID: 7379
		public class SPECIES
		{
			// Token: 0x020027A4 RID: 10148
			public class CRAB
			{
				// Token: 0x0400AAAD RID: 43693
				public static LocString NAME = UI.FormatAsLink("Pokeshell", "Crab");

				// Token: 0x0400AAAE RID: 43694
				public static LocString DESC = string.Concat(new string[]
				{
					"Pokeshells are nonhostile critters that eat ",
					UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
					" and ",
					UI.FormatAsLink("Rot Piles", "COMPOST"),
					".\n\nThe shells they leave behind after molting can be crushed into ",
					UI.FormatAsLink("Lime", "LIME"),
					"."
				});

				// Token: 0x0400AAAF RID: 43695
				public static LocString EGG_NAME = UI.FormatAsLink("Pinch Roe", "Crab");

				// Token: 0x02002F68 RID: 12136
				public class BABY
				{
					// Token: 0x0400BE32 RID: 48690
					public static LocString NAME = UI.FormatAsLink("Pokeshell Spawn", "CRAB");

					// Token: 0x0400BE33 RID: 48691
					public static LocString DESC = "A snippy little Pokeshell Spawn.\n\nIn time it will mature into a fully grown " + UI.FormatAsLink("Pokeshell", "CRAB") + ".";
				}

				// Token: 0x02002F69 RID: 12137
				public class VARIANT_WOOD
				{
					// Token: 0x0400BE34 RID: 48692
					public static LocString NAME = UI.FormatAsLink("Oakshell", "CRABWOOD");

					// Token: 0x0400BE35 RID: 48693
					public static LocString DESC = string.Concat(new string[]
					{
						"Oakshells are nonhostile critters that eat ",
						UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
						", ",
						UI.FormatAsLink("Slime", "SLIMEMOLD"),
						" and ",
						UI.FormatAsLink("Rot Piles", "COMPOST"),
						".\n\nThe shells they leave behind after molting can be crushed into ",
						UI.FormatAsLink("Lumber", "WOOD"),
						".\n\nOakshells thrive in ",
						UI.FormatAsLink("Ethanol", "ETHANOL"),
						"."
					});

					// Token: 0x0400BE36 RID: 48694
					public static LocString EGG_NAME = UI.FormatAsLink("Oak Pinch Roe", "CRABWOOD");

					// Token: 0x02003112 RID: 12562
					public class BABY
					{
						// Token: 0x0400C2CF RID: 49871
						public static LocString NAME = UI.FormatAsLink("Oakshell Spawn", "CRABWOOD");

						// Token: 0x0400C2D0 RID: 49872
						public static LocString DESC = "A knotty little Oakshell Spawn.\n\nIn time it will mature into a fully grown " + UI.FormatAsLink("Oakshell", "CRABWOOD") + ".";
					}
				}

				// Token: 0x02002F6A RID: 12138
				public class VARIANT_FRESH_WATER
				{
					// Token: 0x0400BE37 RID: 48695
					public static LocString NAME = UI.FormatAsLink("Sanishell", "CRABFRESHWATER");

					// Token: 0x0400BE38 RID: 48696
					public static LocString DESC = string.Concat(new string[]
					{
						"Sanishells are nonhostile critters that thrive in ",
						UI.FormatAsLink("Water", "WATER"),
						" and eliminate ",
						UI.FormatAsLink("Germs", "DISEASE"),
						" from any liquid it inhabits.\n\nThey eat ",
						UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
						", ",
						UI.FormatAsLink("Slime", "SLIMEMOLD"),
						" and ",
						UI.FormatAsLink("Rot Piles", "COMPOST"),
						"."
					});

					// Token: 0x0400BE39 RID: 48697
					public static LocString EGG_NAME = UI.FormatAsLink("Sani Pinch Roe", "CRABFRESHWATER");

					// Token: 0x02003113 RID: 12563
					public class BABY
					{
						// Token: 0x0400C2D1 RID: 49873
						public static LocString NAME = UI.FormatAsLink("Sanishell Spawn", "CRABFRESHWATER");

						// Token: 0x0400C2D2 RID: 49874
						public static LocString DESC = "A picky little Sanishell Spawn.\n\nIn time it will mature into a fully grown " + UI.FormatAsLink("Sanishell", "CRABFRESHWATER") + ".";
					}
				}
			}

			// Token: 0x020027A5 RID: 10149
			public class BEE
			{
				// Token: 0x0400AAB0 RID: 43696
				public static LocString NAME = UI.FormatAsLink("Beeta", "BEE");

				// Token: 0x0400AAB1 RID: 43697
				public static LocString DESC = string.Concat(new string[]
				{
					"Beetas are hostile critters that thrive in ",
					UI.FormatAsLink("Radioactive", "RADIATION"),
					" environments.\n\nThey commonly gather ",
					UI.FormatAsLink("Uranium", "URANIUMORE"),
					" for their ",
					UI.FormatAsLink("Beeta Hives", "BEEHIVE"),
					" to produce ",
					UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM"),
					"."
				});

				// Token: 0x02002F6B RID: 12139
				public class BABY
				{
					// Token: 0x0400BE3A RID: 48698
					public static LocString NAME = UI.FormatAsLink("Beetiny", "BEE");

					// Token: 0x0400BE3B RID: 48699
					public static LocString DESC = "A harmless little Beetiny.\n\nIn time, it will mature into a vicious adult " + UI.FormatAsLink("Beeta", "BEE") + ".";
				}
			}

			// Token: 0x020027A6 RID: 10150
			public class CHLORINEGEYSER
			{
				// Token: 0x0400AAB2 RID: 43698
				public static LocString NAME = UI.FormatAsLink("Chlorine Geyser", "GeyserGeneric_CHLORINE_GAS");

				// Token: 0x0400AAB3 RID: 43699
				public static LocString DESC = "A highly pressurized geyser that periodically erupts with " + UI.FormatAsLink("Chlorine", "CHLORINEGAS") + ".";
			}

			// Token: 0x020027A7 RID: 10151
			public class PACU
			{
				// Token: 0x0400AAB4 RID: 43700
				public static LocString NAME = UI.FormatAsLink("Pacu", "PACU");

				// Token: 0x0400AAB5 RID: 43701
				public static LocString DESC = string.Concat(new string[]
				{
					"Pacus are aquatic creatures that can live in any liquid, such as ",
					UI.FormatAsLink("Water", "WATER"),
					" or ",
					UI.FormatAsLink("Contaminated Water", "DIRTYWATER"),
					".\n\nEvery organism in the known universe finds the Pacu extremely delicious."
				});

				// Token: 0x0400AAB6 RID: 43702
				public static LocString EGG_NAME = UI.FormatAsLink("Fry Egg", "PACU");

				// Token: 0x02002F6C RID: 12140
				public class BABY
				{
					// Token: 0x0400BE3C RID: 48700
					public static LocString NAME = UI.FormatAsLink("Pacu Fry", "PACU");

					// Token: 0x0400BE3D RID: 48701
					public static LocString DESC = "A wriggly little Pacu Fry.\n\nIn time, it will mature into an adult " + UI.FormatAsLink("Pacu", "PACU") + ".";
				}

				// Token: 0x02002F6D RID: 12141
				public class VARIANT_TROPICAL
				{
					// Token: 0x0400BE3E RID: 48702
					public static LocString NAME = UI.FormatAsLink("Tropical Pacu", "PACUTROPICAL");

					// Token: 0x0400BE3F RID: 48703
					public static LocString DESC = "Every organism in the known universe finds the Pacu extremely delicious.";

					// Token: 0x0400BE40 RID: 48704
					public static LocString EGG_NAME = UI.FormatAsLink("Tropical Fry Egg", "PACUTROPICAL");

					// Token: 0x02003114 RID: 12564
					public class BABY
					{
						// Token: 0x0400C2D3 RID: 49875
						public static LocString NAME = UI.FormatAsLink("Tropical Fry", "PACUTROPICAL");

						// Token: 0x0400C2D4 RID: 49876
						public static LocString DESC = "A wriggly little Tropical Fry.\n\nIn time it will mature into an adult Pacu morph, the " + UI.FormatAsLink("Tropical Pacu", "PACUTROPICAL") + ".";
					}
				}

				// Token: 0x02002F6E RID: 12142
				public class VARIANT_CLEANER
				{
					// Token: 0x0400BE41 RID: 48705
					public static LocString NAME = UI.FormatAsLink("Gulp Fish", "PACUCLEANER");

					// Token: 0x0400BE42 RID: 48706
					public static LocString DESC = "Every organism in the known universe finds the Pacu extremely delicious.";

					// Token: 0x0400BE43 RID: 48707
					public static LocString EGG_NAME = UI.FormatAsLink("Gulp Fry Egg", "PACUCLEANER");

					// Token: 0x02003115 RID: 12565
					public class BABY
					{
						// Token: 0x0400C2D5 RID: 49877
						public static LocString NAME = UI.FormatAsLink("Gulp Fry", "PACUCLEANER");

						// Token: 0x0400C2D6 RID: 49878
						public static LocString DESC = "A wriggly little Gulp Fry.\n\nIn time, it will mature into an adult " + UI.FormatAsLink("Gulp Fish", "PACUCLEANER") + ".";
					}
				}
			}

			// Token: 0x020027A8 RID: 10152
			public class GLOM
			{
				// Token: 0x0400AAB7 RID: 43703
				public static LocString NAME = UI.FormatAsLink("Morb", "GLOM");

				// Token: 0x0400AAB8 RID: 43704
				public static LocString DESC = "Morbs are attracted to unhygienic conditions and frequently excrete bursts of " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + ".";

				// Token: 0x0400AAB9 RID: 43705
				public static LocString EGG_NAME = UI.FormatAsLink("Morb Pod", "MORB");
			}

			// Token: 0x020027A9 RID: 10153
			public class HATCH
			{
				// Token: 0x0400AABA RID: 43706
				public static LocString NAME = UI.FormatAsLink("Hatch", "HATCH");

				// Token: 0x0400AABB RID: 43707
				public static LocString DESC = "Hatches excrete solid " + UI.FormatAsLink("Coal", "CARBON") + " as waste and may be uncovered by digging up Buried Objects.";

				// Token: 0x0400AABC RID: 43708
				public static LocString EGG_NAME = UI.FormatAsLink("Hatchling Egg", "HATCH");

				// Token: 0x02002F6F RID: 12143
				public class BABY
				{
					// Token: 0x0400BE44 RID: 48708
					public static LocString NAME = UI.FormatAsLink("Hatchling", "HATCH");

					// Token: 0x0400BE45 RID: 48709
					public static LocString DESC = "An innocent little Hatchling.\n\nIn time, it will mature into an adult " + UI.FormatAsLink("Hatch", "HATCH") + ".";
				}

				// Token: 0x02002F70 RID: 12144
				public class VARIANT_HARD
				{
					// Token: 0x0400BE46 RID: 48710
					public static LocString NAME = UI.FormatAsLink("Stone Hatch", "HATCHHARD");

					// Token: 0x0400BE47 RID: 48711
					public static LocString DESC = "Stone Hatches excrete solid " + UI.FormatAsLink("Coal", "CARBON") + " as waste and enjoy burrowing into the ground.";

					// Token: 0x0400BE48 RID: 48712
					public static LocString EGG_NAME = UI.FormatAsLink("Stone Hatchling Egg", "HATCHHARD");

					// Token: 0x02003116 RID: 12566
					public class BABY
					{
						// Token: 0x0400C2D7 RID: 49879
						public static LocString NAME = UI.FormatAsLink("Stone Hatchling", "HATCHHARD");

						// Token: 0x0400C2D8 RID: 49880
						public static LocString DESC = "A doofy little Stone Hatchling.\n\nIt matures into an adult Hatch morph, the " + UI.FormatAsLink("Stone Hatch", "HATCHHARD") + ", which loves nibbling on various rocks and metals.";
					}
				}

				// Token: 0x02002F71 RID: 12145
				public class VARIANT_VEGGIE
				{
					// Token: 0x0400BE49 RID: 48713
					public static LocString NAME = UI.FormatAsLink("Sage Hatch", "HATCHVEGGIE");

					// Token: 0x0400BE4A RID: 48714
					public static LocString DESC = "Sage Hatches excrete solid " + UI.FormatAsLink("Coal", "CARBON") + " as waste and enjoy burrowing into the ground.";

					// Token: 0x0400BE4B RID: 48715
					public static LocString EGG_NAME = UI.FormatAsLink("Sage Hatchling Egg", "HATCHVEGGIE");

					// Token: 0x02003117 RID: 12567
					public class BABY
					{
						// Token: 0x0400C2D9 RID: 49881
						public static LocString NAME = UI.FormatAsLink("Sage Hatchling", "HATCHVEGGIE");

						// Token: 0x0400C2DA RID: 49882
						public static LocString DESC = "A doofy little Sage Hatchling.\n\nIt matures into an adult Hatch morph, the " + UI.FormatAsLink("Sage Hatch", "HATCHVEGGIE") + ", which loves nibbling on organic materials.";
					}
				}

				// Token: 0x02002F72 RID: 12146
				public class VARIANT_METAL
				{
					// Token: 0x0400BE4C RID: 48716
					public static LocString NAME = UI.FormatAsLink("Smooth Hatch", "HATCHMETAL");

					// Token: 0x0400BE4D RID: 48717
					public static LocString DESC = string.Concat(new string[]
					{
						"Smooth Hatches enjoy burrowing into the ground and excrete ",
						UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
						" when fed ",
						UI.FormatAsLink("Metal Ore", "RAWMETAL"),
						"."
					});

					// Token: 0x0400BE4E RID: 48718
					public static LocString EGG_NAME = UI.FormatAsLink("Smooth Hatchling Egg", "HATCHMETAL");

					// Token: 0x02003118 RID: 12568
					public class BABY
					{
						// Token: 0x0400C2DB RID: 49883
						public static LocString NAME = UI.FormatAsLink("Smooth Hatchling", "HATCHMETAL");

						// Token: 0x0400C2DC RID: 49884
						public static LocString DESC = "A doofy little Smooth Hatchling.\n\nIt matures into an adult Hatch morph, the " + UI.FormatAsLink("Smooth Hatch", "HATCHMETAL") + ", which loves nibbling on different types of metals.";
					}
				}
			}

			// Token: 0x020027AA RID: 10154
			public class STATERPILLAR
			{
				// Token: 0x0400AABD RID: 43709
				public static LocString NAME = UI.FormatAsLink("Plug Slug", "STATERPILLAR");

				// Token: 0x0400AABE RID: 43710
				public static LocString DESC = "Plug Slugs are dynamic creatures that generate electrical " + UI.FormatAsLink("Power", "POWER") + " during the night.\n\nTheir power can be harnessed by leaving an exposed wire near areas where they like to sleep.";

				// Token: 0x0400AABF RID: 43711
				public static LocString EGG_NAME = UI.FormatAsLink("Slug Egg", "STATERPILLAR");

				// Token: 0x02002F73 RID: 12147
				public class BABY
				{
					// Token: 0x0400BE4F RID: 48719
					public static LocString NAME = UI.FormatAsLink("Plug Sluglet", "STATERPILLAR");

					// Token: 0x0400BE50 RID: 48720
					public static LocString DESC = "A chubby little Plug Sluglet.\n\nIn time it will mature into a fully grown " + UI.FormatAsLink("Plug Slug", "STATERPILLAR") + ".";
				}

				// Token: 0x02002F74 RID: 12148
				public class VARIANT_GAS
				{
					// Token: 0x0400BE51 RID: 48721
					public static LocString NAME = UI.FormatAsLink("Smog Slug", "STATERPILLAR");

					// Token: 0x0400BE52 RID: 48722
					public static LocString DESC = string.Concat(new string[]
					{
						"Smog Slugs are porous creatures that draw in unbreathable ",
						UI.FormatAsLink("Gases", "ELEMENTS_GAS"),
						" during the day.\n\nAt night, they sleep near exposed ",
						UI.FormatAsLink("Gas Pipes,", "GASCONDUIT"),
						" where they deposit their cache."
					});

					// Token: 0x0400BE53 RID: 48723
					public static LocString EGG_NAME = UI.FormatAsLink("Smog Slug Egg", "STATERPILLAR");

					// Token: 0x02003119 RID: 12569
					public class BABY
					{
						// Token: 0x0400C2DD RID: 49885
						public static LocString NAME = UI.FormatAsLink("Smog Sluglet", "STATERPILLAR");

						// Token: 0x0400C2DE RID: 49886
						public static LocString DESC = "A tubby little Smog Sluglet.\n\nIn time it will mature into a fully grown " + UI.FormatAsLink("Smog Slug", "STATERPILLAR") + ".";
					}
				}

				// Token: 0x02002F75 RID: 12149
				public class VARIANT_LIQUID
				{
					// Token: 0x0400BE54 RID: 48724
					public static LocString NAME = UI.FormatAsLink("Sponge Slug", "STATERPILLAR");

					// Token: 0x0400BE55 RID: 48725
					public static LocString DESC = string.Concat(new string[]
					{
						"Sponge Slugs are thirsty creatures that soak up ",
						UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
						" during the day.\n\nThey deposit their stored ",
						UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
						" into the exposed ",
						UI.FormatAsLink("Liquid Pipes", "LIQUIDCONDUIT"),
						" they sleep next to at night."
					});

					// Token: 0x0400BE56 RID: 48726
					public static LocString EGG_NAME = UI.FormatAsLink("Sponge Slug Egg", "STATERPILLAR");

					// Token: 0x0200311A RID: 12570
					public class BABY
					{
						// Token: 0x0400C2DF RID: 49887
						public static LocString NAME = UI.FormatAsLink("Sponge Sluglet", "STATERPILLAR");

						// Token: 0x0400C2E0 RID: 49888
						public static LocString DESC = "A chonky little Sponge Sluglet.\n\nIn time it will mature into a fully grown " + UI.FormatAsLink("Sponge Slug", "STATERPILLAR") + ".";
					}
				}
			}

			// Token: 0x020027AB RID: 10155
			public class DIVERGENT
			{
				// Token: 0x02002F76 RID: 12150
				public class VARIANT_BEETLE
				{
					// Token: 0x0400BE57 RID: 48727
					public static LocString NAME = UI.FormatAsLink("Sweetle", "DIVERGENTBEETLE");

					// Token: 0x0400BE58 RID: 48728
					public static LocString DESC = string.Concat(new string[]
					{
						"Sweetles are nonhostile critters that excrete large amounts of solid ",
						UI.FormatAsLink("Sucrose", "SUCROSE"),
						".\n\nThey are closely related to the ",
						UI.FormatAsLink("Grubgrub", "DIVERGENTWORM"),
						" and exhibit similar, albeit less effective farming behaviors."
					});

					// Token: 0x0400BE59 RID: 48729
					public static LocString EGG_NAME = UI.FormatAsLink("Sweetle Egg", "DIVERGENTBEETLE");

					// Token: 0x0200311B RID: 12571
					public class BABY
					{
						// Token: 0x0400C2E1 RID: 49889
						public static LocString NAME = UI.FormatAsLink("Sweetle Larva", "DIVERGENTBEETLE");

						// Token: 0x0400C2E2 RID: 49890
						public static LocString DESC = "A crawly little Sweetle Larva.\n\nIn time, it will mature into an adult " + UI.FormatAsLink("Sweetle", "DIVERGENTBEETLE") + ".";
					}
				}

				// Token: 0x02002F77 RID: 12151
				public class VARIANT_WORM
				{
					// Token: 0x0400BE5A RID: 48730
					public static LocString NAME = UI.FormatAsLink("Grubgrub", "DIVERGENTWORM");

					// Token: 0x0400BE5B RID: 48731
					public static LocString DESC = string.Concat(new string[]
					{
						"Grubgrubs form symbiotic relationships with plants, especially ",
						UI.FormatAsLink("Grubfruit Plants", "WORMPLANT"),
						", and instinctually tend to them.\n\nGrubgrubs are closely related to ",
						UI.FormatAsLink("Sweetles", "DIVERGENTBEETLE"),
						"."
					});

					// Token: 0x0400BE5C RID: 48732
					public static LocString EGG_NAME = UI.FormatAsLink("Grubgrub Egg", "DIVERGENTWORM");

					// Token: 0x0200311C RID: 12572
					public class BABY
					{
						// Token: 0x0400C2E3 RID: 49891
						public static LocString NAME = UI.FormatAsLink("Grubgrub Wormling", "DIVERGENTWORM");

						// Token: 0x0400C2E4 RID: 49892
						public static LocString DESC = "A squirmy little Grubgrub Wormling.\n\nIn time, it will mature into an adult " + UI.FormatAsLink("Grubgrub", "WORM") + " and drastically grow in size.";
					}
				}
			}

			// Token: 0x020027AC RID: 10156
			public class DRECKO
			{
				// Token: 0x0400AAC0 RID: 43712
				public static LocString NAME = UI.FormatAsLink("Drecko", "DRECKO");

				// Token: 0x0400AAC1 RID: 43713
				public static LocString DESC = string.Concat(new string[]
				{
					"Dreckos are nonhostile critters that graze on ",
					UI.FormatAsLink("Pincha Pepperplants", "SPICE_VINE"),
					", ",
					UI.FormatAsLink("Balm Lily", "SWAMPLILY"),
					" or ",
					UI.FormatAsLink("Mealwood Plants", "BASICSINGLEHARVESTPLANT"),
					".\n\nTheir backsides are covered in thick woolly fibers that only grow in ",
					UI.FormatAsLink("Hydrogen", "HYDROGEN"),
					" climates."
				});

				// Token: 0x0400AAC2 RID: 43714
				public static LocString EGG_NAME = UI.FormatAsLink("Drecklet Egg", "DRECKO");

				// Token: 0x02002F78 RID: 12152
				public class BABY
				{
					// Token: 0x0400BE5D RID: 48733
					public static LocString NAME = UI.FormatAsLink("Drecklet", "DRECKO");

					// Token: 0x0400BE5E RID: 48734
					public static LocString DESC = "A little, bug-eyed Drecklet.\n\nIn time it will mature into a fully grown " + UI.FormatAsLink("Drecko", "DRECKO") + ".";
				}

				// Token: 0x02002F79 RID: 12153
				public class VARIANT_PLASTIC
				{
					// Token: 0x0400BE5F RID: 48735
					public static LocString NAME = UI.FormatAsLink("Glossy Drecko", "DRECKOPLASTIC");

					// Token: 0x0400BE60 RID: 48736
					public static LocString DESC = string.Concat(new string[]
					{
						"Glossy Dreckos are nonhostile critters that graze on live ",
						UI.FormatAsLink("Mealwood Plants", "BASICSINGLEHARVESTPLANT"),
						" and ",
						UI.FormatAsLink("Bristle Blossoms", "PRICKLEFLOWER"),
						".\n\nTheir backsides are covered in bioplastic scales that only grow in ",
						UI.FormatAsLink("Hydrogen", "HYDROGEN"),
						" climates."
					});

					// Token: 0x0400BE61 RID: 48737
					public static LocString EGG_NAME = UI.FormatAsLink("Glossy Drecklet Egg", "DRECKOPLASTIC");

					// Token: 0x0200311D RID: 12573
					public class BABY
					{
						// Token: 0x0400C2E5 RID: 49893
						public static LocString NAME = UI.FormatAsLink("Glossy Drecklet", "DRECKOPLASTIC");

						// Token: 0x0400C2E6 RID: 49894
						public static LocString DESC = "A bug-eyed little Glossy Drecklet.\n\nIn time it will mature into an adult Drecko morph, the " + UI.FormatAsLink("Glossy Drecko", "DRECKOPLASTIC") + ".";
					}
				}
			}

			// Token: 0x020027AD RID: 10157
			public class SQUIRREL
			{
				// Token: 0x0400AAC3 RID: 43715
				public static LocString NAME = UI.FormatAsLink("Pip", "SQUIRREL");

				// Token: 0x0400AAC4 RID: 43716
				public static LocString DESC = string.Concat(new string[]
				{
					"Pips are pesky, nonhostile critters that subsist on ",
					UI.FormatAsLink("Thimble Reeds", "BASICFABRICPLANT"),
					" and ",
					UI.FormatAsLink("Arbor Tree", "FOREST_TREE"),
					" branches.\n\nThey are known to bury ",
					UI.FormatAsLink("Seeds", "PLANTS"),
					" in the ground whenever they can find a suitable area with enough space."
				});

				// Token: 0x0400AAC5 RID: 43717
				public static LocString EGG_NAME = UI.FormatAsLink("Pip Egg", "SQUIRREL");

				// Token: 0x02002F7A RID: 12154
				public class BABY
				{
					// Token: 0x0400BE62 RID: 48738
					public static LocString NAME = UI.FormatAsLink("Pipsqueak", "SQUIRREL");

					// Token: 0x0400BE63 RID: 48739
					public static LocString DESC = "A little purring Pipsqueak.\n\nIn time it will mature into a fully grown " + UI.FormatAsLink("Pip", "SQUIRREL") + ".";
				}

				// Token: 0x02002F7B RID: 12155
				public class VARIANT_HUG
				{
					// Token: 0x0400BE64 RID: 48740
					public static LocString NAME = UI.FormatAsLink("Cuddle Pip", "SQUIRREL");

					// Token: 0x0400BE65 RID: 48741
					public static LocString DESC = "Cuddle Pips are fluffy, affectionate critters who exhibit a strong snuggling instinct towards all types of eggs.\n\nThis is temporarily amplified when they are hugged by a passing Duplicant.";

					// Token: 0x0400BE66 RID: 48742
					public static LocString EGG_NAME = UI.FormatAsLink("Cuddle Pip Egg", "SQUIRREL");

					// Token: 0x0200311E RID: 12574
					public class BABY
					{
						// Token: 0x0400C2E7 RID: 49895
						public static LocString NAME = UI.FormatAsLink("Cuddle Pipsqueak", "SQUIRREL");

						// Token: 0x0400C2E8 RID: 49896
						public static LocString DESC = "A fuzzy little Cuddle Pipsqueak.\n\nIn time it will mature into a fully grown " + UI.FormatAsLink("Cuddle Pip", "SQUIRREL") + ".";
					}
				}
			}

			// Token: 0x020027AE RID: 10158
			public class OILFLOATER
			{
				// Token: 0x0400AAC6 RID: 43718
				public static LocString NAME = UI.FormatAsLink("Slickster", "OILFLOATER");

				// Token: 0x0400AAC7 RID: 43719
				public static LocString DESC = string.Concat(new string[]
				{
					"Slicksters are slimy critters that consume ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					" and exude ",
					UI.FormatAsLink("Crude Oil", "CRUDEOIL"),
					"."
				});

				// Token: 0x0400AAC8 RID: 43720
				public static LocString EGG_NAME = UI.FormatAsLink("Larva Egg", "OILFLOATER");

				// Token: 0x02002F7C RID: 12156
				public class BABY
				{
					// Token: 0x0400BE67 RID: 48743
					public static LocString NAME = UI.FormatAsLink("Slickster Larva", "OILFLOATER");

					// Token: 0x0400BE68 RID: 48744
					public static LocString DESC = "A goopy little Slickster Larva.\n\nOne day it will grow into an adult " + UI.FormatAsLink("Slickster", "OILFLOATER") + ".";
				}

				// Token: 0x02002F7D RID: 12157
				public class VARIANT_HIGHTEMP
				{
					// Token: 0x0400BE69 RID: 48745
					public static LocString NAME = UI.FormatAsLink("Molten Slickster", "OILFLOATERHIGHTEMP");

					// Token: 0x0400BE6A RID: 48746
					public static LocString DESC = string.Concat(new string[]
					{
						"Molten Slicksters are slimy critters that consume ",
						UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
						" and exude ",
						UI.FormatAsLink("Petroleum", "PETROLEUM"),
						"."
					});

					// Token: 0x0400BE6B RID: 48747
					public static LocString EGG_NAME = UI.FormatAsLink("Molten Larva Egg", "OILFLOATERHIGHTEMP");

					// Token: 0x0200311F RID: 12575
					public class BABY
					{
						// Token: 0x0400C2E9 RID: 49897
						public static LocString NAME = UI.FormatAsLink("Molten Larva", "OILFLOATERHIGHTEMP");

						// Token: 0x0400C2EA RID: 49898
						public static LocString DESC = "A goopy little Molten Larva.\n\nOne day it will grow into an adult Slickster morph, the " + UI.FormatAsLink("Molten Slickster", "OILFLOATERHIGHTEMP") + ".";
					}
				}

				// Token: 0x02002F7E RID: 12158
				public class VARIANT_DECOR
				{
					// Token: 0x0400BE6C RID: 48748
					public static LocString NAME = UI.FormatAsLink("Longhair Slickster", "OILFLOATERDECOR");

					// Token: 0x0400BE6D RID: 48749
					public static LocString DESC = "Longhair Slicksters are friendly critters that consume " + UI.FormatAsLink("Oxygen", "OXYGEN") + " and thrive in close contact with Duplicant companions.\n\nLonghairs have extremely beautiful and luxurious coats.";

					// Token: 0x0400BE6E RID: 48750
					public static LocString EGG_NAME = UI.FormatAsLink("Longhair Larva Egg", "OILFLOATERDECOR");

					// Token: 0x02003120 RID: 12576
					public class BABY
					{
						// Token: 0x0400C2EB RID: 49899
						public static LocString NAME = UI.FormatAsLink("Longhair Larva", "OILFLOATERDECOR");

						// Token: 0x0400C2EC RID: 49900
						public static LocString DESC = "A snuggly little Longhair Larva.\n\nOne day it will grow into an adult Slickster morph, the " + UI.FormatAsLink("Longhair Slickster", "OILFLOATERDECOR") + ".";
					}
				}
			}

			// Token: 0x020027AF RID: 10159
			public class PUFT
			{
				// Token: 0x0400AAC9 RID: 43721
				public static LocString NAME = UI.FormatAsLink("Puft", "PUFT");

				// Token: 0x0400AACA RID: 43722
				public static LocString DESC = "Pufts are non-aggressive critters that excrete lumps of " + UI.FormatAsLink("Slime", "SLIMEMOLD") + " with each breath.";

				// Token: 0x0400AACB RID: 43723
				public static LocString EGG_NAME = UI.FormatAsLink("Puftlet Egg", "PUFT");

				// Token: 0x02002F7F RID: 12159
				public class BABY
				{
					// Token: 0x0400BE6F RID: 48751
					public static LocString NAME = UI.FormatAsLink("Puftlet", "PUFT");

					// Token: 0x0400BE70 RID: 48752
					public static LocString DESC = "A gassy little Puftlet.\n\nIn time it will grow into an adult " + UI.FormatAsLink("Puft", "PUFT") + ".";
				}

				// Token: 0x02002F80 RID: 12160
				public class VARIANT_ALPHA
				{
					// Token: 0x0400BE71 RID: 48753
					public static LocString NAME = UI.FormatAsLink("Puft Prince", "PUFTALPHA");

					// Token: 0x0400BE72 RID: 48754
					public static LocString DESC = "The Puft Prince is a lazy critter that excretes little " + UI.FormatAsLink("Solid", "SOLID") + " lumps of whatever it has been breathing.";

					// Token: 0x0400BE73 RID: 48755
					public static LocString EGG_NAME = UI.FormatAsLink("Puftlet Prince Egg", "PUFTALPHA");

					// Token: 0x02003121 RID: 12577
					public class BABY
					{
						// Token: 0x0400C2ED RID: 49901
						public static LocString NAME = UI.FormatAsLink("Puftlet Prince", "PUFTALPHA");

						// Token: 0x0400C2EE RID: 49902
						public static LocString DESC = "A gassy little Puftlet Prince.\n\nOne day it will grow into an adult Puft morph, the " + UI.FormatAsLink("Puft Prince", "PUFTALPHA") + ".\n\nIt seems a bit snobby...";
					}
				}

				// Token: 0x02002F81 RID: 12161
				public class VARIANT_OXYLITE
				{
					// Token: 0x0400BE74 RID: 48756
					public static LocString NAME = UI.FormatAsLink("Dense Puft", "PUFTOXYLITE");

					// Token: 0x0400BE75 RID: 48757
					public static LocString DESC = "Dense Pufts are non-aggressive critters that excrete condensed " + UI.FormatAsLink("Oxylite", "OXYROCK") + " with each breath.";

					// Token: 0x0400BE76 RID: 48758
					public static LocString EGG_NAME = UI.FormatAsLink("Dense Puftlet Egg", "PUFTOXYLITE");

					// Token: 0x02003122 RID: 12578
					public class BABY
					{
						// Token: 0x0400C2EF RID: 49903
						public static LocString NAME = UI.FormatAsLink("Dense Puftlet", "PUFTOXYLITE");

						// Token: 0x0400C2F0 RID: 49904
						public static LocString DESC = "A stocky little Dense Puftlet.\n\nOne day it will grow into an adult Puft morph, the " + UI.FormatAsLink("Dense Puft", "PUFTOXYLITE") + ".";
					}
				}

				// Token: 0x02002F82 RID: 12162
				public class VARIANT_BLEACHSTONE
				{
					// Token: 0x0400BE77 RID: 48759
					public static LocString NAME = UI.FormatAsLink("Squeaky Puft", "PUFTBLEACHSTONE");

					// Token: 0x0400BE78 RID: 48760
					public static LocString DESC = "Squeaky Pufts are non-aggressive critters that excrete lumps of " + UI.FormatAsLink("Bleachstone", "BLEACHSTONE") + " with each breath.";

					// Token: 0x0400BE79 RID: 48761
					public static LocString EGG_NAME = UI.FormatAsLink("Squeaky Puftlet Egg", "PUFTBLEACHSTONE");

					// Token: 0x02003123 RID: 12579
					public class BABY
					{
						// Token: 0x0400C2F1 RID: 49905
						public static LocString NAME = UI.FormatAsLink("Squeaky Puftlet", "PUFTBLEACHSTONE");

						// Token: 0x0400C2F2 RID: 49906
						public static LocString DESC = "A frazzled little Squeaky Puftlet.\n\nOne day it will grow into an adult Puft morph, the " + UI.FormatAsLink("Squeaky Puft", "PUFTBLEACHSTONE") + ".";
					}
				}
			}

			// Token: 0x020027B0 RID: 10160
			public class MOO
			{
				// Token: 0x0400AACC RID: 43724
				public static LocString NAME = UI.FormatAsLink("Gassy Moo", "MOO");

				// Token: 0x0400AACD RID: 43725
				public static LocString DESC = string.Concat(new string[]
				{
					"Moos are extraterrestrial critters that feed on ",
					UI.FormatAsLink("Gas Grass", "GASGRASS"),
					" and excrete ",
					UI.FormatAsLink("Natural Gas", "METHANE"),
					"."
				});
			}

			// Token: 0x020027B1 RID: 10161
			public class MOLE
			{
				// Token: 0x0400AACE RID: 43726
				public static LocString NAME = UI.FormatAsLink("Shove Vole", "MOLE");

				// Token: 0x0400AACF RID: 43727
				public static LocString DESC = string.Concat(new string[]
				{
					"Shove Voles are burrowing critters that eat the ",
					UI.FormatAsLink("Regolith", "REGOLITH"),
					" collected on terrestrial surfaces.\n\nThey cannot burrow through ",
					UI.FormatAsLink("Refined Metals", "REFINEDMETAL"),
					"."
				});

				// Token: 0x0400AAD0 RID: 43728
				public static LocString EGG_NAME = UI.FormatAsLink("Shove Vole Egg", "MOLE");

				// Token: 0x02002F83 RID: 12163
				public class BABY
				{
					// Token: 0x0400BE7A RID: 48762
					public static LocString NAME = UI.FormatAsLink("Vole Pup", "MOLE");

					// Token: 0x0400BE7B RID: 48763
					public static LocString DESC = "A snuggly little pup.\n\nOne day it will grow into an adult " + UI.FormatAsLink("Shove Vole", "MOLE") + ".";
				}

				// Token: 0x02002F84 RID: 12164
				public class VARIANT_DELICACY
				{
					// Token: 0x0400BE7C RID: 48764
					public static LocString NAME = UI.FormatAsLink("Delecta Vole", "MOLEDELICACY");

					// Token: 0x0400BE7D RID: 48765
					public static LocString DESC = string.Concat(new string[]
					{
						"Delecta Voles are burrowing critters whose bodies sprout shearable ",
						UI.FormatAsLink("Tonic Root", "GINGER"),
						" when ",
						UI.FormatAsLink("Regolith", "REGOLITH"),
						" is ingested at preferred temperatures.\n\nThey cannot burrow through ",
						UI.FormatAsLink("Refined Metals", "REFINEDMETAL"),
						"."
					});

					// Token: 0x0400BE7E RID: 48766
					public static LocString EGG_NAME = UI.FormatAsLink("Delecta Vole Egg", "MOLEDELICACY");

					// Token: 0x02003124 RID: 12580
					public class BABY
					{
						// Token: 0x0400C2F3 RID: 49907
						public static LocString NAME = UI.FormatAsLink("Delecta Vole Pup", "MOLEDELICACY");

						// Token: 0x0400C2F4 RID: 49908
						public static LocString DESC = "A tender little Delecta Vole pup.\n\nOne day it will grow into an adult Shove Vole morph, the " + UI.FormatAsLink("Delecta Vole", "MOLEDELICACY") + ".";
					}
				}
			}

			// Token: 0x020027B2 RID: 10162
			public class GREEDYGREEN
			{
				// Token: 0x0400AAD1 RID: 43729
				public static LocString NAME = UI.FormatAsLink("Avari Vine", "GREEDYGREEN");

				// Token: 0x0400AAD2 RID: 43730
				public static LocString DESC = "A rapidly growing, subterranean " + UI.FormatAsLink("Plant", "PLANTS") + ".";
			}

			// Token: 0x020027B3 RID: 10163
			public class SHOCKWORM
			{
				// Token: 0x0400AAD3 RID: 43731
				public static LocString NAME = UI.FormatAsLink("Shockworm", "SHOCKWORM");

				// Token: 0x0400AAD4 RID: 43732
				public static LocString DESC = "Shockworms are exceptionally aggressive and discharge electrical shocks to stun their prey.";
			}

			// Token: 0x020027B4 RID: 10164
			public class LIGHTBUG
			{
				// Token: 0x0400AAD5 RID: 43733
				public static LocString NAME = UI.FormatAsLink("Shine Bug", "LIGHTBUG");

				// Token: 0x0400AAD6 RID: 43734
				public static LocString DESC = "Shine Bugs emit a soft " + UI.FormatAsLink("Light", "LIGHT") + " in hopes of attracting more of their kind for company.";

				// Token: 0x0400AAD7 RID: 43735
				public static LocString EGG_NAME = UI.FormatAsLink("Shine Nymph Egg", "LIGHTBUG");

				// Token: 0x02002F85 RID: 12165
				public class BABY
				{
					// Token: 0x0400BE7F RID: 48767
					public static LocString NAME = UI.FormatAsLink("Shine Nymph", "LIGHTBUG");

					// Token: 0x0400BE80 RID: 48768
					public static LocString DESC = "A chubby baby " + UI.FormatAsLink("Shine Bug", "LIGHTBUG") + ".";
				}

				// Token: 0x02002F86 RID: 12166
				public class VARIANT_ORANGE
				{
					// Token: 0x0400BE81 RID: 48769
					public static LocString NAME = UI.FormatAsLink("Sun Bug", "LIGHTBUGORANGE");

					// Token: 0x0400BE82 RID: 48770
					public static LocString DESC = "Shine Bugs emit a soft " + UI.FormatAsLink("Light", "LIGHT") + " in hopes of attracting more of their kind for company.\n\nThe light of the Sun morph has been turned orange through selective breeding.";

					// Token: 0x0400BE83 RID: 48771
					public static LocString EGG_NAME = UI.FormatAsLink("Sun Nymph Egg", "LIGHTBUGORANGE");

					// Token: 0x02003125 RID: 12581
					public class BABY
					{
						// Token: 0x0400C2F5 RID: 49909
						public static LocString NAME = UI.FormatAsLink("Sun Nymph", "LIGHTBUGORANGE");

						// Token: 0x0400C2F6 RID: 49910
						public static LocString DESC = "A chubby baby " + UI.FormatAsLink("Shine Bug", "LIGHTBUGORANGE") + ".\n\nThis one is a Sun morph.";
					}
				}

				// Token: 0x02002F87 RID: 12167
				public class VARIANT_PURPLE
				{
					// Token: 0x0400BE84 RID: 48772
					public static LocString NAME = UI.FormatAsLink("Royal Bug", "LIGHTBUGPURPLE");

					// Token: 0x0400BE85 RID: 48773
					public static LocString DESC = "Shine Bugs emit a soft " + UI.FormatAsLink("Light", "LIGHT") + " in hopes of attracting more of their kind for company.\n\nThe light of the Royal morph has been turned purple through selective breeding.";

					// Token: 0x0400BE86 RID: 48774
					public static LocString EGG_NAME = UI.FormatAsLink("Royal Nymph Egg", "LIGHTBUGPURPLE");

					// Token: 0x02003126 RID: 12582
					public class BABY
					{
						// Token: 0x0400C2F7 RID: 49911
						public static LocString NAME = UI.FormatAsLink("Royal Nymph", "LIGHTBUGPURPLE");

						// Token: 0x0400C2F8 RID: 49912
						public static LocString DESC = "A chubby baby " + UI.FormatAsLink("Shine Bug", "LIGHTBUGPURPLE") + ".\n\nThis one is a Royal morph.";
					}
				}

				// Token: 0x02002F88 RID: 12168
				public class VARIANT_PINK
				{
					// Token: 0x0400BE87 RID: 48775
					public static LocString NAME = UI.FormatAsLink("Coral Bug", "LIGHTBUGPINK");

					// Token: 0x0400BE88 RID: 48776
					public static LocString DESC = "Shine Bugs emit a soft " + UI.FormatAsLink("Light", "LIGHT") + " in hopes of attracting more of their kind for company.\n\nThe light of the Coral morph has been turned pink through selective breeding.";

					// Token: 0x0400BE89 RID: 48777
					public static LocString EGG_NAME = UI.FormatAsLink("Coral Nymph Egg", "LIGHTBUGPINK");

					// Token: 0x02003127 RID: 12583
					public class BABY
					{
						// Token: 0x0400C2F9 RID: 49913
						public static LocString NAME = UI.FormatAsLink("Coral Nymph", "LIGHTBUGPINK");

						// Token: 0x0400C2FA RID: 49914
						public static LocString DESC = "A chubby baby " + UI.FormatAsLink("Shine Bug", "LIGHTBUGPINK") + ".\n\nThis one is a Coral morph.";
					}
				}

				// Token: 0x02002F89 RID: 12169
				public class VARIANT_BLUE
				{
					// Token: 0x0400BE8A RID: 48778
					public static LocString NAME = UI.FormatAsLink("Azure Bug", "LIGHTBUGBLUE");

					// Token: 0x0400BE8B RID: 48779
					public static LocString DESC = "Shine Bugs emit a soft " + UI.FormatAsLink("Light", "LIGHT") + " in hopes of attracting more of their kind for company.\n\nThe light of the Azure morph has been turned blue through selective breeding.";

					// Token: 0x0400BE8C RID: 48780
					public static LocString EGG_NAME = UI.FormatAsLink("Azure Nymph Egg", "LIGHTBUGBLUE");

					// Token: 0x02003128 RID: 12584
					public class BABY
					{
						// Token: 0x0400C2FB RID: 49915
						public static LocString NAME = UI.FormatAsLink("Azure Nymph", "LIGHTBUGBLUE");

						// Token: 0x0400C2FC RID: 49916
						public static LocString DESC = "A chubby baby " + UI.FormatAsLink("Shine Bug", "LIGHTBUGBLUE") + ".\n\nThis one is an Azure morph.";
					}
				}

				// Token: 0x02002F8A RID: 12170
				public class VARIANT_BLACK
				{
					// Token: 0x0400BE8D RID: 48781
					public static LocString NAME = UI.FormatAsLink("Abyss Bug", "LIGHTBUGBLACK");

					// Token: 0x0400BE8E RID: 48782
					public static LocString DESC = "This Shine Bug emits no " + UI.FormatAsLink("Light", "LIGHT") + ", but it makes up for it by having an excellent personality.";

					// Token: 0x0400BE8F RID: 48783
					public static LocString EGG_NAME = UI.FormatAsLink("Abyss Nymph Egg", "LIGHTBUGBLACK");

					// Token: 0x02003129 RID: 12585
					public class BABY
					{
						// Token: 0x0400C2FD RID: 49917
						public static LocString NAME = UI.FormatAsLink("Abyss Nymph", "LIGHTBUGBLACK");

						// Token: 0x0400C2FE RID: 49918
						public static LocString DESC = "A chubby baby " + UI.FormatAsLink("Shine Bug", "LIGHTBUGBLACK") + ".\n\nThis one is an Abyss morph.";
					}
				}

				// Token: 0x02002F8B RID: 12171
				public class VARIANT_CRYSTAL
				{
					// Token: 0x0400BE90 RID: 48784
					public static LocString NAME = UI.FormatAsLink("Radiant Bug", "LIGHTBUGCRYSTAL");

					// Token: 0x0400BE91 RID: 48785
					public static LocString DESC = "Shine Bugs emit a soft " + UI.FormatAsLink("Light", "LIGHT") + " in hopes of attracting more of their kind for company.\n\nThe light of the Radiant morph has been amplified through selective breeding.";

					// Token: 0x0400BE92 RID: 48786
					public static LocString EGG_NAME = UI.FormatAsLink("Radiant Nymph Egg", "LIGHTBUGCRYSTAL");

					// Token: 0x0200312A RID: 12586
					public class BABY
					{
						// Token: 0x0400C2FF RID: 49919
						public static LocString NAME = UI.FormatAsLink("Radiant Nymph", "LIGHTBUGCRYSTAL");

						// Token: 0x0400C300 RID: 49920
						public static LocString DESC = "A chubby baby " + UI.FormatAsLink("Shine Bug", "LIGHTBUGCRYSTAL") + ".\n\nThis one is a Radiant morph.";
					}
				}

				// Token: 0x02002F8C RID: 12172
				public class VARIANT_RADIOACTIVE
				{
					// Token: 0x0400BE93 RID: 48787
					public static LocString NAME = UI.FormatAsLink("Ionizing Bug", "LIGHTBUGRADIOACTIVE");

					// Token: 0x0400BE94 RID: 48788
					public static LocString DESC = "Shine Bugs emit a dangerously radioactive " + UI.FormatAsLink("Light", "LIGHT") + " in hopes of attracting more of their kind for company.";

					// Token: 0x0400BE95 RID: 48789
					public static LocString EGG_NAME = UI.FormatAsLink("Ionizing Nymph Egg", "LIGHTBUGCRYSTAL");

					// Token: 0x0200312B RID: 12587
					public class BABY
					{
						// Token: 0x0400C301 RID: 49921
						public static LocString NAME = UI.FormatAsLink("Ionizing Nymph", "LIGHTBUGRADIOACTIVE");

						// Token: 0x0400C302 RID: 49922
						public static LocString DESC = "A chubby baby " + UI.FormatAsLink("Shine Bug", "LIGHTBUGRADIOACTIVE") + ".\n\nThis one is an Ionizing morph.";
					}
				}
			}

			// Token: 0x020027B5 RID: 10165
			public class GEYSER
			{
				// Token: 0x0400AAD8 RID: 43736
				public static LocString NAME = UI.FormatAsLink("Steam Geyser", "GEYSER");

				// Token: 0x0400AAD9 RID: 43737
				public static LocString DESC = string.Concat(new string[]
				{
					"A highly pressurized geyser that periodically erupts, spraying ",
					UI.FormatAsLink("Steam", "STEAM"),
					" and boiling hot ",
					UI.FormatAsLink("Water", "WATER"),
					"."
				});

				// Token: 0x02002F8D RID: 12173
				public class STEAM
				{
					// Token: 0x0400BE96 RID: 48790
					public static LocString NAME = UI.FormatAsLink("Cool Steam Vent", "GeyserGeneric_STEAM");

					// Token: 0x0400BE97 RID: 48791
					public static LocString DESC = "A highly pressurized vent that periodically erupts with " + UI.FormatAsLink("Steam", "STEAM") + ".";
				}

				// Token: 0x02002F8E RID: 12174
				public class HOT_STEAM
				{
					// Token: 0x0400BE98 RID: 48792
					public static LocString NAME = UI.FormatAsLink("Steam Vent", "GeyserGeneric_HOT_STEAM");

					// Token: 0x0400BE99 RID: 48793
					public static LocString DESC = "A highly pressurized vent that periodically erupts with scalding " + UI.FormatAsLink("Steam", "STEAM") + ".";
				}

				// Token: 0x02002F8F RID: 12175
				public class SALT_WATER
				{
					// Token: 0x0400BE9A RID: 48794
					public static LocString NAME = UI.FormatAsLink("Salt Water Geyser", "GeyserGeneric_SALT_WATER");

					// Token: 0x0400BE9B RID: 48795
					public static LocString DESC = "A highly pressurized geyser that periodically erupts with " + UI.FormatAsLink("Salt Water", "SALTWATER") + ".";
				}

				// Token: 0x02002F90 RID: 12176
				public class SLUSH_SALT_WATER
				{
					// Token: 0x0400BE9C RID: 48796
					public static LocString NAME = UI.FormatAsLink("Cool Salt Slush Geyser", "GeyserGeneric_SLUSH_SALT_WATER");

					// Token: 0x0400BE9D RID: 48797
					public static LocString DESC = "A highly pressurized geyser that periodically erupts with freezing " + ELEMENTS.BRINE.NAME + ".";
				}

				// Token: 0x02002F91 RID: 12177
				public class HOT_WATER
				{
					// Token: 0x0400BE9E RID: 48798
					public static LocString NAME = UI.FormatAsLink("Water Geyser", "GeyserGeneric_HOT_WATER");

					// Token: 0x0400BE9F RID: 48799
					public static LocString DESC = "A highly pressurized geyser that periodically erupts with hot " + UI.FormatAsLink("Water", "WATER") + ".";
				}

				// Token: 0x02002F92 RID: 12178
				public class SLUSH_WATER
				{
					// Token: 0x0400BEA0 RID: 48800
					public static LocString NAME = UI.FormatAsLink("Cool Slush Geyser", "GeyserGeneric_SLUSHWATER");

					// Token: 0x0400BEA1 RID: 48801
					public static LocString DESC = "A highly pressurized geyser that periodically erupts with freezing " + ELEMENTS.DIRTYWATER.NAME + ".";
				}

				// Token: 0x02002F93 RID: 12179
				public class FILTHY_WATER
				{
					// Token: 0x0400BEA2 RID: 48802
					public static LocString NAME = UI.FormatAsLink("Polluted Water Vent", "GeyserGeneric_FILTHYWATER");

					// Token: 0x0400BEA3 RID: 48803
					public static LocString DESC = "A highly pressurized vent that periodically erupts with boiling " + UI.FormatAsLink("Contaminated Water", "DIRTYWATER") + ".";
				}

				// Token: 0x02002F94 RID: 12180
				public class SMALL_VOLCANO
				{
					// Token: 0x0400BEA4 RID: 48804
					public static LocString NAME = UI.FormatAsLink("Minor Volcano", "GeyserGeneric_SMALL_VOLCANO");

					// Token: 0x0400BEA5 RID: 48805
					public static LocString DESC = "A miniature volcano that periodically erupts with molten " + UI.FormatAsLink("Magma", "MAGMA") + ".";
				}

				// Token: 0x02002F95 RID: 12181
				public class BIG_VOLCANO
				{
					// Token: 0x0400BEA6 RID: 48806
					public static LocString NAME = UI.FormatAsLink("Volcano", "GeyserGeneric_BIG_VOLCANO");

					// Token: 0x0400BEA7 RID: 48807
					public static LocString DESC = "A massive volcano that periodically erupts with molten " + UI.FormatAsLink("Magma", "MAGMA") + ".";
				}

				// Token: 0x02002F96 RID: 12182
				public class LIQUID_CO2
				{
					// Token: 0x0400BEA8 RID: 48808
					public static LocString NAME = UI.FormatAsLink("Carbon Dioxide Geyser", "GeyserGeneric_LIQUID_CO2");

					// Token: 0x0400BEA9 RID: 48809
					public static LocString DESC = "A highly pressurized geyser that periodically erupts with boiling liquid " + UI.FormatAsLink("Carbon Dioxide", "LIQUIDCARBONDIOXIDE") + ".";
				}

				// Token: 0x02002F97 RID: 12183
				public class HOT_CO2
				{
					// Token: 0x0400BEAA RID: 48810
					public static LocString NAME = UI.FormatAsLink("Carbon Dioxide Vent", "GeyserGeneric_HOT_CO2");

					// Token: 0x0400BEAB RID: 48811
					public static LocString DESC = "A highly pressurized vent that periodically erupts with hot gaseous " + UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE") + ".";
				}

				// Token: 0x02002F98 RID: 12184
				public class HOT_HYDROGEN
				{
					// Token: 0x0400BEAC RID: 48812
					public static LocString NAME = UI.FormatAsLink("Hydrogen Vent", "GeyserGeneric_HOT_HYDROGEN");

					// Token: 0x0400BEAD RID: 48813
					public static LocString DESC = "A highly pressurized vent that periodically erupts with hot gaseous " + UI.FormatAsLink("Hydrogen", "HYDROGEN") + ".";
				}

				// Token: 0x02002F99 RID: 12185
				public class HOT_PO2
				{
					// Token: 0x0400BEAE RID: 48814
					public static LocString NAME = UI.FormatAsLink("Hot Polluted Oxygen Vent", "GeyserGeneric_HOT_PO2");

					// Token: 0x0400BEAF RID: 48815
					public static LocString DESC = "A highly pressurized vent that periodically erupts with hot " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + ".";
				}

				// Token: 0x02002F9A RID: 12186
				public class SLIMY_PO2
				{
					// Token: 0x0400BEB0 RID: 48816
					public static LocString NAME = UI.FormatAsLink("Infectious Polluted Oxygen Vent", "GeyserGeneric_SLIMY_PO2");

					// Token: 0x0400BEB1 RID: 48817
					public static LocString DESC = "A highly pressurized vent that periodically erupts with warm " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + ".";
				}

				// Token: 0x02002F9B RID: 12187
				public class CHLORINE_GAS
				{
					// Token: 0x0400BEB2 RID: 48818
					public static LocString NAME = UI.FormatAsLink("Chlorine Gas Vent", "GeyserGeneric_CHLORINE_GAS");

					// Token: 0x0400BEB3 RID: 48819
					public static LocString DESC = "A highly pressurized vent that periodically erupts with warm " + UI.FormatAsLink("Chlorine", "CHLORINEGAS") + ".";
				}

				// Token: 0x02002F9C RID: 12188
				public class METHANE
				{
					// Token: 0x0400BEB4 RID: 48820
					public static LocString NAME = UI.FormatAsLink("Natural Gas Geyser", "GeyserGeneric_METHANE");

					// Token: 0x0400BEB5 RID: 48821
					public static LocString DESC = "A highly pressurized geyser that periodically erupts with hot " + UI.FormatAsLink("Natural Gas", "METHANE") + ".";
				}

				// Token: 0x02002F9D RID: 12189
				public class MOLTEN_COPPER
				{
					// Token: 0x0400BEB6 RID: 48822
					public static LocString NAME = UI.FormatAsLink("Copper Volcano", "GeyserGeneric_MOLTEN_COPPER");

					// Token: 0x0400BEB7 RID: 48823
					public static LocString DESC = "A large volcano that periodically erupts with molten " + UI.FormatAsLink("Copper", "MOLTENCOPPER") + ".";
				}

				// Token: 0x02002F9E RID: 12190
				public class MOLTEN_IRON
				{
					// Token: 0x0400BEB8 RID: 48824
					public static LocString NAME = UI.FormatAsLink("Iron Volcano", "GeyserGeneric_MOLTEN_IRON");

					// Token: 0x0400BEB9 RID: 48825
					public static LocString DESC = "A large volcano that periodically erupts with molten " + UI.FormatAsLink("Iron", "MOLTENIRON") + ".";
				}

				// Token: 0x02002F9F RID: 12191
				public class MOLTEN_ALUMINUM
				{
					// Token: 0x0400BEBA RID: 48826
					public static LocString NAME = UI.FormatAsLink("Aluminum Volcano", "GeyserGeneric_MOLTEN_ALUMINUM");

					// Token: 0x0400BEBB RID: 48827
					public static LocString DESC = "A large volcano that periodically erupts with molten " + UI.FormatAsLink("Aluminum", "MOLTENALUMINUM") + ".";
				}

				// Token: 0x02002FA0 RID: 12192
				public class MOLTEN_TUNGSTEN
				{
					// Token: 0x0400BEBC RID: 48828
					public static LocString NAME = UI.FormatAsLink("Tungsten Volcano", "GeyserGeneric_MOLTEN_TUNGSTEN");

					// Token: 0x0400BEBD RID: 48829
					public static LocString DESC = "A large volcano that periodically erupts with molten " + UI.FormatAsLink("Tungsten", "MOLTENTUNGSTEN") + ".";
				}

				// Token: 0x02002FA1 RID: 12193
				public class MOLTEN_GOLD
				{
					// Token: 0x0400BEBE RID: 48830
					public static LocString NAME = UI.FormatAsLink("Gold Volcano", "GeyserGeneric_MOLTEN_GOLD");

					// Token: 0x0400BEBF RID: 48831
					public static LocString DESC = "A large volcano that periodically erupts with molten " + UI.FormatAsLink("Gold", "MOLTENGOLD") + ".";
				}

				// Token: 0x02002FA2 RID: 12194
				public class MOLTEN_COBALT
				{
					// Token: 0x0400BEC0 RID: 48832
					public static LocString NAME = UI.FormatAsLink("Cobalt Volcano", "GeyserGeneric_MOLTEN_COBALT");

					// Token: 0x0400BEC1 RID: 48833
					public static LocString DESC = "A large volcano that periodically erupts with molten " + UI.FormatAsLink("Cobalt", "MOLTENCOBALT") + ".";
				}

				// Token: 0x02002FA3 RID: 12195
				public class MOLTEN_NIOBIUM
				{
					// Token: 0x0400BEC2 RID: 48834
					public static LocString NAME = UI.FormatAsLink("Niobium Volcano", "NiobiumGeyser");

					// Token: 0x0400BEC3 RID: 48835
					public static LocString DESC = "A large volcano that periodically erupts with molten " + UI.FormatAsLink("Niobium", "NIOBIUM") + ".";
				}

				// Token: 0x02002FA4 RID: 12196
				public class OIL_DRIP
				{
					// Token: 0x0400BEC4 RID: 48836
					public static LocString NAME = UI.FormatAsLink("Leaky Oil Fissure", "GeyserGeneric_OIL_DRIP");

					// Token: 0x0400BEC5 RID: 48837
					public static LocString DESC = "A fissure that periodically erupts with boiling " + UI.FormatAsLink("Crude Oil", "CRUDEOIL") + ".";
				}

				// Token: 0x02002FA5 RID: 12197
				public class LIQUID_SULFUR
				{
					// Token: 0x0400BEC6 RID: 48838
					public static LocString NAME = UI.FormatAsLink("Liquid Sulfur Geyser", "GeyserGeneric_LIQUID_SULFUR");

					// Token: 0x0400BEC7 RID: 48839
					public static LocString DESC = "A highly pressurized geyser that periodically erupts with boiling " + UI.FormatAsLink("Sulfur", "LIQUIDSULFUR") + ".";
				}
			}

			// Token: 0x020027B6 RID: 10166
			public class METHANEGEYSER
			{
				// Token: 0x0400AADA RID: 43738
				public static LocString NAME = UI.FormatAsLink("Natural Gas Geyser", "GeyserGeneric_METHANEGEYSER");

				// Token: 0x0400AADB RID: 43739
				public static LocString DESC = "A highly pressurized geyser that periodically erupts with " + UI.FormatAsLink("Natural Gas", "METHANE") + ".";
			}

			// Token: 0x020027B7 RID: 10167
			public class OIL_WELL
			{
				// Token: 0x0400AADC RID: 43740
				public static LocString NAME = UI.FormatAsLink("Oil Reservoir", "OIL_WELL");

				// Token: 0x0400AADD RID: 43741
				public static LocString DESC = "Oil Reservoirs are rock formations with " + UI.FormatAsLink("Crude Oil", "CRUDEOIL") + " deposits beneath their surface.\n\nOil can be extracted from a reservoir with sufficient pressure.";
			}

			// Token: 0x020027B8 RID: 10168
			public class MUSHROOMPLANT
			{
				// Token: 0x0400AADE RID: 43742
				public static LocString NAME = UI.FormatAsLink("Dusk Cap", "MUSHROOMPLANT");

				// Token: 0x0400AADF RID: 43743
				public static LocString DESC = string.Concat(new string[]
				{
					"Dusk Caps produce ",
					UI.FormatAsLink("Mushrooms", "MUSHROOM"),
					", fungal growths that can be harvested for ",
					UI.FormatAsLink("Food", "FOOD"),
					"."
				});

				// Token: 0x0400AAE0 RID: 43744
				public static LocString DOMESTICATEDDESC = "This plant produces edible " + UI.FormatAsLink("Mushrooms", "MUSHROOM") + ".";
			}

			// Token: 0x020027B9 RID: 10169
			public class STEAMSPOUT
			{
				// Token: 0x0400AAE1 RID: 43745
				public static LocString NAME = UI.FormatAsLink("Steam Spout", "GEYSERS");

				// Token: 0x0400AAE2 RID: 43746
				public static LocString DESC = "A rocky vent that spouts " + UI.FormatAsLink("Steam", "STEAM") + ".";
			}

			// Token: 0x020027BA RID: 10170
			public class PROPANESPOUT
			{
				// Token: 0x0400AAE3 RID: 43747
				public static LocString NAME = UI.FormatAsLink("Propane Spout", "GEYSERS");

				// Token: 0x0400AAE4 RID: 43748
				public static LocString DESC = "A rocky vent that spouts " + ELEMENTS.PROPANE.NAME + ".";
			}

			// Token: 0x020027BB RID: 10171
			public class OILSPOUT
			{
				// Token: 0x0400AAE5 RID: 43749
				public static LocString NAME = UI.FormatAsLink("Oil Spout", "OILSPOUT");

				// Token: 0x0400AAE6 RID: 43750
				public static LocString DESC = "A rocky vent that spouts " + UI.FormatAsLink("Crude Oil", "CRUDEOIL") + ".";
			}

			// Token: 0x020027BC RID: 10172
			public class HEATBULB
			{
				// Token: 0x0400AAE7 RID: 43751
				public static LocString NAME = UI.FormatAsLink("Fervine", "HEATBULB");

				// Token: 0x0400AAE8 RID: 43752
				public static LocString DESC = "A temperature reactive, subterranean " + UI.FormatAsLink("Plant", "PLANTS") + ".";
			}

			// Token: 0x020027BD RID: 10173
			public class HEATBULBSEED
			{
				// Token: 0x0400AAE9 RID: 43753
				public static LocString NAME = UI.FormatAsLink("Fervine Bulb", "HEATBULBSEED");

				// Token: 0x0400AAEA RID: 43754
				public static LocString DESC = "A temperature reactive, subterranean " + UI.FormatAsLink("Plant", "PLANTS") + ".";
			}

			// Token: 0x020027BE RID: 10174
			public class PACUEGG
			{
				// Token: 0x0400AAEB RID: 43755
				public static LocString NAME = UI.FormatAsLink("Pacu Egg", "PACUEGG");

				// Token: 0x0400AAEC RID: 43756
				public static LocString DESC = "A tiny Pacu is nestled inside.\n\nIt is not yet ready for the world.";
			}

			// Token: 0x020027BF RID: 10175
			public class MYSTERYEGG
			{
				// Token: 0x0400AAED RID: 43757
				public static LocString NAME = UI.FormatAsLink("Mysterious Egg", "MYSTERYEGG");

				// Token: 0x0400AAEE RID: 43758
				public static LocString DESC = "What's growing inside? Something nice? Something mean?";
			}

			// Token: 0x020027C0 RID: 10176
			public class SWAMPLILY
			{
				// Token: 0x0400AAEF RID: 43759
				public static LocString NAME = UI.FormatAsLink("Balm Lily", "SWAMPLILY");

				// Token: 0x0400AAF0 RID: 43760
				public static LocString DESC = "Balm Lilies produce " + ITEMS.INGREDIENTS.SWAMPLILYFLOWER.NAME + ", a lovely bloom with medicinal properties.";

				// Token: 0x0400AAF1 RID: 43761
				public static LocString DOMESTICATEDDESC = "This plant produces medicinal " + ITEMS.INGREDIENTS.SWAMPLILYFLOWER.NAME + ".";
			}

			// Token: 0x020027C1 RID: 10177
			public class JUNGLEGASPLANT
			{
				// Token: 0x0400AAF2 RID: 43762
				public static LocString NAME = UI.FormatAsLink("Palmera Tree", "JUNGLEGASPLANT");

				// Token: 0x0400AAF3 RID: 43763
				public static LocString DESC = "A large, chlorine-dwelling " + UI.FormatAsLink("Plant", "PLANTS") + " that can be grown in farm buildings.\n\nPalmeras grow inedible buds that emit unbreathable hydrogen gas.";

				// Token: 0x0400AAF4 RID: 43764
				public static LocString DOMESTICATEDDESC = "A large, chlorine-dwelling " + UI.FormatAsLink("Plant", "PLANTS") + " that grows inedible buds which emit unbreathable hydrogen gas.";
			}

			// Token: 0x020027C2 RID: 10178
			public class PRICKLEFLOWER
			{
				// Token: 0x0400AAF5 RID: 43765
				public static LocString NAME = UI.FormatAsLink("Bristle Blossom", "PRICKLEFLOWER");

				// Token: 0x0400AAF6 RID: 43766
				public static LocString DESC = "Bristle Blossoms produce " + ITEMS.FOOD.PRICKLEFRUIT.NAME + ", a prickly edible bud.";

				// Token: 0x0400AAF7 RID: 43767
				public static LocString DOMESTICATEDDESC = "This plant produces edible " + UI.FormatAsLink("Bristle Berries", UI.StripLinkFormatting(ITEMS.FOOD.PRICKLEFRUIT.NAME)) + ".";
			}

			// Token: 0x020027C3 RID: 10179
			public class COLDWHEAT
			{
				// Token: 0x0400AAF8 RID: 43768
				public static LocString NAME = UI.FormatAsLink("Sleet Wheat", "COLDWHEAT");

				// Token: 0x0400AAF9 RID: 43769
				public static LocString DESC = string.Concat(new string[]
				{
					"Sleet Wheat produces ",
					ITEMS.FOOD.COLDWHEATSEED.NAME,
					", a chilly grain that can be processed into ",
					UI.FormatAsLink("Food", "FOOD"),
					"."
				});

				// Token: 0x0400AAFA RID: 43770
				public static LocString DOMESTICATEDDESC = "This plant produces edible " + ITEMS.FOOD.COLDWHEATSEED.NAME + ".";
			}

			// Token: 0x020027C4 RID: 10180
			public class GASGRASS
			{
				// Token: 0x0400AAFB RID: 43771
				public static LocString NAME = UI.FormatAsLink("Gas Grass", "GASGRASS");

				// Token: 0x0400AAFC RID: 43772
				public static LocString DESC = "Gas Grass.";

				// Token: 0x0400AAFD RID: 43773
				public static LocString DOMESTICATEDDESC = "An alien grass variety that is eaten by " + UI.FormatAsLink("Gassy Moos", "MOO") + ".";
			}

			// Token: 0x020027C5 RID: 10181
			public class PRICKLEGRASS
			{
				// Token: 0x0400AAFE RID: 43774
				public static LocString NAME = UI.FormatAsLink("Bluff Briar", "PRICKLEGRASS");

				// Token: 0x0400AAFF RID: 43775
				public static LocString DESC = "Bluff Briars exude pheromones causing critters to view them as especially beautiful.";

				// Token: 0x0400AB00 RID: 43776
				public static LocString DOMESTICATEDDESC = "This plant improves " + UI.FormatAsLink("Decor", "DECOR") + ".";

				// Token: 0x0400AB01 RID: 43777
				public static LocString GROWTH_BONUS = "Growth Bonus";

				// Token: 0x0400AB02 RID: 43778
				public static LocString WILT_PENALTY = "Wilt Penalty";
			}

			// Token: 0x020027C6 RID: 10182
			public class CYLINDRICA
			{
				// Token: 0x0400AB03 RID: 43779
				public static LocString NAME = UI.FormatAsLink("Bliss Burst", "CYLINDRICA");

				// Token: 0x0400AB04 RID: 43780
				public static LocString DESC = "Bliss Bursts release an explosion of " + UI.FormatAsLink("Decor", "DECOR") + " into otherwise dull environments.";

				// Token: 0x0400AB05 RID: 43781
				public static LocString DOMESTICATEDDESC = "This plant improves ambient " + UI.FormatAsLink("Decor", "DECOR") + ".";

				// Token: 0x0400AB06 RID: 43782
				public static LocString GROWTH_BONUS = "Growth Bonus";

				// Token: 0x0400AB07 RID: 43783
				public static LocString WILT_PENALTY = "Wilt Penalty";
			}

			// Token: 0x020027C7 RID: 10183
			public class TOEPLANT
			{
				// Token: 0x0400AB08 RID: 43784
				public static LocString NAME = UI.FormatAsLink("Tranquil Toes", "TOEPLANT");

				// Token: 0x0400AB09 RID: 43785
				public static LocString DESC = "Tranquil Toes improve " + UI.FormatAsLink("Decor", "DECOR") + " by giving their surroundings the visual equivalent of a foot rub.";

				// Token: 0x0400AB0A RID: 43786
				public static LocString DOMESTICATEDDESC = "This plant improves ambient " + UI.FormatAsLink("Decor", "DECOR") + ".";

				// Token: 0x0400AB0B RID: 43787
				public static LocString GROWTH_BONUS = "Growth Bonus";

				// Token: 0x0400AB0C RID: 43788
				public static LocString WILT_PENALTY = "Wilt Penalty";
			}

			// Token: 0x020027C8 RID: 10184
			public class WINECUPS
			{
				// Token: 0x0400AB0D RID: 43789
				public static LocString NAME = UI.FormatAsLink("Mellow Mallow", "WINECUPS");

				// Token: 0x0400AB0E RID: 43790
				public static LocString DESC = string.Concat(new string[]
				{
					"Mellow Mallows heighten ",
					UI.FormatAsLink("Decor", "DECOR"),
					" and alleviate ",
					UI.FormatAsLink("Stress", "STRESS"),
					" with their calming color and cradle shape."
				});

				// Token: 0x0400AB0F RID: 43791
				public static LocString DOMESTICATEDDESC = "This plant improves ambient " + UI.FormatAsLink("Decor", "DECOR") + ".";

				// Token: 0x0400AB10 RID: 43792
				public static LocString GROWTH_BONUS = "Growth Bonus";

				// Token: 0x0400AB11 RID: 43793
				public static LocString WILT_PENALTY = "Wilt Penalty";
			}

			// Token: 0x020027C9 RID: 10185
			public class EVILFLOWER
			{
				// Token: 0x0400AB12 RID: 43794
				public static LocString NAME = UI.FormatAsLink("Sporechid", "EVILFLOWER");

				// Token: 0x0400AB13 RID: 43795
				public static LocString DESC = "Sporechids have an eerily alluring appearance to mask the fact that they host particularly nasty strain of brain fungus.";

				// Token: 0x0400AB14 RID: 43796
				public static LocString DOMESTICATEDDESC = string.Concat(new string[]
				{
					"This plant improves ambient ",
					UI.FormatAsLink("Decor", "DECOR"),
					" but produces high quantities of ",
					UI.FormatAsLink("Zombie Spores", "ZOMBIESPORES"),
					"."
				});

				// Token: 0x0400AB15 RID: 43797
				public static LocString GROWTH_BONUS = "Growth Bonus";

				// Token: 0x0400AB16 RID: 43798
				public static LocString WILT_PENALTY = "Wilt Penalty";
			}

			// Token: 0x020027CA RID: 10186
			public class LEAFYPLANT
			{
				// Token: 0x0400AB17 RID: 43799
				public static LocString NAME = UI.FormatAsLink("Mirth Leaf", "POTTED_LEAFY");

				// Token: 0x0400AB18 RID: 43800
				public static LocString DESC = string.Concat(new string[]
				{
					"Mirth Leaves sport a calm green hue known for alleviating ",
					UI.FormatAsLink("Stress", "STRESS"),
					" and improving ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});

				// Token: 0x0400AB19 RID: 43801
				public static LocString DOMESTICATEDDESC = "This plant improves ambient " + UI.FormatAsLink("Decor", "DECOR") + ".";

				// Token: 0x0400AB1A RID: 43802
				public static LocString GROWTH_BONUS = "Growth Bonus";

				// Token: 0x0400AB1B RID: 43803
				public static LocString WILT_PENALTY = "Wilt Penalty";
			}

			// Token: 0x020027CB RID: 10187
			public class CACTUSPLANT
			{
				// Token: 0x0400AB1C RID: 43804
				public static LocString NAME = UI.FormatAsLink("Jumping Joya", "POTTED_CACTUS");

				// Token: 0x0400AB1D RID: 43805
				public static LocString DESC = string.Concat(new string[]
				{
					"Joyas are ",
					UI.FormatAsLink("Decorative", "DECOR"),
					" ",
					UI.FormatAsLink("Plants", "PLANTS"),
					" that are colloquially said to make gardeners \"jump for joy\"."
				});

				// Token: 0x0400AB1E RID: 43806
				public static LocString DOMESTICATEDDESC = "This plant improves ambient " + UI.FormatAsLink("Decor", "DECOR") + ".";

				// Token: 0x0400AB1F RID: 43807
				public static LocString GROWTH_BONUS = "Growth Bonus";

				// Token: 0x0400AB20 RID: 43808
				public static LocString WILT_PENALTY = "Wilt Penalty";
			}

			// Token: 0x020027CC RID: 10188
			public class BULBPLANT
			{
				// Token: 0x0400AB21 RID: 43809
				public static LocString NAME = UI.FormatAsLink("Buddy Bud", "POTTED_BULB");

				// Token: 0x0400AB22 RID: 43810
				public static LocString DESC = "Buddy Buds are leafy plants that have a positive effect on " + UI.FormatAsLink("Morale", "MORALE") + ", much like a friend.";

				// Token: 0x0400AB23 RID: 43811
				public static LocString DOMESTICATEDDESC = "This plant improves ambient " + UI.FormatAsLink("Decor", "DECOR") + ".";

				// Token: 0x0400AB24 RID: 43812
				public static LocString GROWTH_BONUS = "Growth Bonus";

				// Token: 0x0400AB25 RID: 43813
				public static LocString WILT_PENALTY = "Wilt Penalty";
			}

			// Token: 0x020027CD RID: 10189
			public class BASICSINGLEHARVESTPLANT
			{
				// Token: 0x0400AB26 RID: 43814
				public static LocString NAME = UI.FormatAsLink("Mealwood", "BASICSINGLEHARVESTPLANT");

				// Token: 0x0400AB27 RID: 43815
				public static LocString DESC = string.Concat(new string[]
				{
					"Mealwoods produce ",
					ITEMS.FOOD.BASICPLANTFOOD.NAME,
					", an oddly wriggly grain that can be harvested for ",
					UI.FormatAsLink("Food", "FOOD"),
					"."
				});

				// Token: 0x0400AB28 RID: 43816
				public static LocString DOMESTICATEDDESC = "This plant produces edible " + ITEMS.FOOD.BASICPLANTFOOD.NAME + ".";
			}

			// Token: 0x020027CE RID: 10190
			public class SWAMPHARVESTPLANT
			{
				// Token: 0x0400AB29 RID: 43817
				public static LocString NAME = UI.FormatAsLink("Bog Bucket", "SWAMPHARVESTPLANT");

				// Token: 0x0400AB2A RID: 43818
				public static LocString DESC = string.Concat(new string[]
				{
					"Bog Buckets produce juicy, sweet ",
					UI.FormatAsLink("Bog Jellies", "SWAMPFRUIT"),
					" for ",
					UI.FormatAsLink("Food", "FOOD"),
					"."
				});

				// Token: 0x0400AB2B RID: 43819
				public static LocString DOMESTICATEDDESC = "This plant produces edible " + UI.FormatAsLink("Bog Jellies", "SWAMPFRUIT") + ".";
			}

			// Token: 0x020027CF RID: 10191
			public class WORMPLANT
			{
				// Token: 0x0400AB2C RID: 43820
				public static LocString NAME = UI.FormatAsLink("Spindly Grubfruit Plant", "WORMPLANT");

				// Token: 0x0400AB2D RID: 43821
				public static LocString DESC = string.Concat(new string[]
				{
					"Spindly Grubfruit Plants produce ",
					UI.FormatAsLink("Spindly Grubfruit", "WORMBASICFRUIT"),
					" for ",
					UI.FormatAsLink("Food", "FOOD"),
					".\n\nIf it is tended by a ",
					CREATURES.FAMILY.DIVERGENTSPECIES,
					" critter, it will produce high quality fruits instead."
				});

				// Token: 0x0400AB2E RID: 43822
				public static LocString DOMESTICATEDDESC = "This plant produces edible " + ITEMS.FOOD.WORMBASICFRUIT.NAME + ".";
			}

			// Token: 0x020027D0 RID: 10192
			public class SUPERWORMPLANT
			{
				// Token: 0x0400AB2F RID: 43823
				public static LocString NAME = UI.FormatAsLink("Grubfruit Plant", "WORMPLANT");

				// Token: 0x0400AB30 RID: 43824
				public static LocString DESC = string.Concat(new string[]
				{
					"A Grubfruit Plant that has flourished after being tended by a ",
					CREATURES.FAMILY.DIVERGENTSPECIES,
					" critter.\n\nIt will produce high quality ",
					UI.FormatAsLink("Grubfruits", "WORMSUPERFRUIT"),
					"."
				});

				// Token: 0x0400AB31 RID: 43825
				public static LocString DOMESTICATEDDESC = "This plant produces edible " + ITEMS.FOOD.WORMSUPERFRUIT.NAME + ".";
			}

			// Token: 0x020027D1 RID: 10193
			public class BASICFABRICMATERIALPLANT
			{
				// Token: 0x0400AB32 RID: 43826
				public static LocString NAME = UI.FormatAsLink("Thimble Reed", "BASICFABRICPLANT");

				// Token: 0x0400AB33 RID: 43827
				public static LocString DESC = string.Concat(new string[]
				{
					"Thimble Reeds produce indescribably soft ",
					ITEMS.INDUSTRIAL_PRODUCTS.BASIC_FABRIC.NAME,
					" for ",
					UI.FormatAsLink("Clothing", "EQUIPMENT"),
					" production."
				});

				// Token: 0x0400AB34 RID: 43828
				public static LocString DOMESTICATEDDESC = "This plant produces " + ITEMS.INDUSTRIAL_PRODUCTS.BASIC_FABRIC.NAME + ".";
			}

			// Token: 0x020027D2 RID: 10194
			public class BASICFORAGEPLANTPLANTED
			{
				// Token: 0x0400AB35 RID: 43829
				public static LocString NAME = UI.FormatAsLink("Buried Muckroot", "BASICFORAGEPLANTPLANTED");

				// Token: 0x0400AB36 RID: 43830
				public static LocString DESC = "Muckroots are incapable of propagating but can be harvested for a single " + UI.FormatAsLink("Food", "FOOD") + " serving.";
			}

			// Token: 0x020027D3 RID: 10195
			public class FORESTFORAGEPLANTPLANTED
			{
				// Token: 0x0400AB37 RID: 43831
				public static LocString NAME = UI.FormatAsLink("Hexalent", "FORESTFORAGEPLANTPLANTED");

				// Token: 0x0400AB38 RID: 43832
				public static LocString DESC = "Hexalents are incapable of propagating but can be harvested for a single, calorie dense " + UI.FormatAsLink("Food", "FOOD") + " serving.";
			}

			// Token: 0x020027D4 RID: 10196
			public class SWAMPFORAGEPLANTPLANTED
			{
				// Token: 0x0400AB39 RID: 43833
				public static LocString NAME = UI.FormatAsLink("Swamp Chard", "SWAMPFORAGEPLANTPLANTED");

				// Token: 0x0400AB3A RID: 43834
				public static LocString DESC = "Swamp Chards are incapable of propagating but can be harvested for a single low quality and calorie dense " + UI.FormatAsLink("Food", "FOOD") + " serving.";
			}

			// Token: 0x020027D5 RID: 10197
			public class COLDBREATHER
			{
				// Token: 0x0400AB3B RID: 43835
				public static LocString NAME = UI.FormatAsLink("Wheezewort", "COLDBREATHER");

				// Token: 0x0400AB3C RID: 43836
				public static LocString DESC = string.Concat(new string[]
				{
					"Wheezeworts can be planted in ",
					UI.FormatAsLink("Planter Boxes", "PLANTERBOX"),
					", ",
					UI.FormatAsLink("Farm Tiles", "FARMTILE"),
					" or ",
					UI.FormatAsLink("Hydroponic Farms", "HYDROPONICFARM"),
					", and absorb ",
					UI.FormatAsLink("Heat", "Heat"),
					" by respiring through their porous outer membranes."
				});

				// Token: 0x0400AB3D RID: 43837
				public static LocString DOMESTICATEDDESC = "This plant absorbs " + UI.FormatAsLink("Heat", "Heat") + ".";
			}

			// Token: 0x020027D6 RID: 10198
			public class COLDBREATHERCLUSTER
			{
				// Token: 0x0400AB3E RID: 43838
				public static LocString NAME = UI.FormatAsLink("Wheezewort", "COLDBREATHERCLUSTER");

				// Token: 0x0400AB3F RID: 43839
				public static LocString DESC = string.Concat(new string[]
				{
					"Wheezeworts can be planted in ",
					UI.FormatAsLink("Planter Boxes", "PLANTERBOX"),
					", ",
					UI.FormatAsLink("Farm Tiles", "FARMTILE"),
					" or ",
					UI.FormatAsLink("Hydroponic Farms", "HYDROPONICFARM"),
					", and absorb ",
					UI.FormatAsLink("Heat", "Heat"),
					" by respiring through their porous outer membranes."
				});

				// Token: 0x0400AB40 RID: 43840
				public static LocString DOMESTICATEDDESC = "This plant absorbs " + UI.FormatAsLink("Heat", "Heat") + ".";
			}

			// Token: 0x020027D7 RID: 10199
			public class SPICE_VINE
			{
				// Token: 0x0400AB41 RID: 43841
				public static LocString NAME = UI.FormatAsLink("Pincha Pepperplant", "SPICE_VINE");

				// Token: 0x0400AB42 RID: 43842
				public static LocString DESC = string.Concat(new string[]
				{
					"Pincha Pepperplants produce flavorful ",
					ITEMS.FOOD.SPICENUT.NAME,
					" for spicing ",
					UI.FormatAsLink("Food", "FOOD"),
					"."
				});

				// Token: 0x0400AB43 RID: 43843
				public static LocString DOMESTICATEDDESC = "This plant produces " + ITEMS.FOOD.SPICENUT.NAME + " spices.";
			}

			// Token: 0x020027D8 RID: 10200
			public class SALTPLANT
			{
				// Token: 0x0400AB44 RID: 43844
				public static LocString NAME = UI.FormatAsLink("Dasha Saltvine", "SALTPLANT");

				// Token: 0x0400AB45 RID: 43845
				public static LocString DESC = string.Concat(new string[]
				{
					"Dasha Saltvines consume small amounts of ",
					UI.FormatAsLink("Chlorine Gas", "CHLORINE"),
					" and form sodium deposits as they grow, producing harvestable ",
					UI.FormatAsLink("Salt", "SALT"),
					"."
				});

				// Token: 0x0400AB46 RID: 43846
				public static LocString DOMESTICATEDDESC = "This plant produces unrefined " + UI.FormatAsLink("Salt", "SALT") + ".";
			}

			// Token: 0x020027D9 RID: 10201
			public class FILTERPLANT
			{
				// Token: 0x0400AB47 RID: 43847
				public static LocString NAME = UI.FormatAsLink("Hydrocactus", "FILTERPLANT");

				// Token: 0x0400AB48 RID: 43848
				public static LocString DESC = string.Concat(new string[]
				{
					"Hydrocacti act as natural ",
					UI.FormatAsLink("Water", "WATER"),
					" filters when given access to ",
					UI.FormatAsLink("Sand", "SAND"),
					"."
				});

				// Token: 0x0400AB49 RID: 43849
				public static LocString DOMESTICATEDDESC = string.Concat(new string[]
				{
					"This plant uses ",
					UI.FormatAsLink("Sand", "SAND"),
					" to convert ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					" into ",
					UI.FormatAsLink("Water", "WATER"),
					"."
				});
			}

			// Token: 0x020027DA RID: 10202
			public class OXYFERN
			{
				// Token: 0x0400AB4A RID: 43850
				public static LocString NAME = UI.FormatAsLink("Oxyfern", "OXYFERN");

				// Token: 0x0400AB4B RID: 43851
				public static LocString DESC = string.Concat(new string[]
				{
					"Oxyferns absorb ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					" and exude breathable ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					"."
				});

				// Token: 0x0400AB4C RID: 43852
				public static LocString DOMESTICATEDDESC = string.Concat(new string[]
				{
					"This plant converts ",
					UI.FormatAsLink("CO<sub>2</sub>", "CARBONDIOXIDE"),
					" into ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					"."
				});
			}

			// Token: 0x020027DB RID: 10203
			public class BEAN_PLANT
			{
				// Token: 0x0400AB4D RID: 43853
				public static LocString NAME = UI.FormatAsLink("Nosh Sprout", "BEAN_PLANT");

				// Token: 0x0400AB4E RID: 43854
				public static LocString DESC = "Nosh Sprouts thrive in colder climates and produce edible " + UI.FormatAsLink("Nosh Beans", "BEAN") + ".";

				// Token: 0x0400AB4F RID: 43855
				public static LocString DOMESTICATEDDESC = "This plant produces " + UI.FormatAsLink("Nosh Beans", "BEAN") + ".";
			}

			// Token: 0x020027DC RID: 10204
			public class WOOD_TREE
			{
				// Token: 0x0400AB50 RID: 43856
				public static LocString NAME = UI.FormatAsLink("Arbor Tree", "FOREST_TREE");

				// Token: 0x0400AB51 RID: 43857
				public static LocString DESC = "Arbor Trees grow " + UI.FormatAsLink("Arbor Tree Branches", "FOREST_TREE") + " and can be harvested for lumber.";

				// Token: 0x0400AB52 RID: 43858
				public static LocString DOMESTICATEDDESC = "This plant produces " + UI.FormatAsLink("Arbor Tree Branches", "FOREST_TREE") + " that can be harvested for lumber.";
			}

			// Token: 0x020027DD RID: 10205
			public class WOOD_TREE_BRANCH
			{
				// Token: 0x0400AB53 RID: 43859
				public static LocString NAME = UI.FormatAsLink("Arbor Tree Branch", "FOREST_TREE");

				// Token: 0x0400AB54 RID: 43860
				public static LocString DESC = "Arbor Trees Branches can be harvested for lumber.";
			}

			// Token: 0x020027DE RID: 10206
			public class SEALETTUCE
			{
				// Token: 0x0400AB55 RID: 43861
				public static LocString NAME = UI.FormatAsLink("Waterweed", "SEALETTUCE");

				// Token: 0x0400AB56 RID: 43862
				public static LocString DESC = "Waterweeds thrive in salty water and can be harvested for fresh, edible " + UI.FormatAsLink("Lettuce", "LETTUCE") + ".";

				// Token: 0x0400AB57 RID: 43863
				public static LocString DOMESTICATEDDESC = "This plant produces " + UI.FormatAsLink("Lettuce", "LETTUCE") + ".";
			}

			// Token: 0x020027DF RID: 10207
			public class CRITTERTRAPPLANT
			{
				// Token: 0x0400AB58 RID: 43864
				public static LocString NAME = UI.FormatAsLink("Saturn Critter Trap", "CRITTERTRAPPLANT");

				// Token: 0x0400AB59 RID: 43865
				public static LocString DESC = "Critter Traps are carnivorous plants that trap unsuspecting critters and consume them, releasing " + UI.FormatAsLink("Hydrogen", "HYDROGEN") + " as waste.";

				// Token: 0x0400AB5A RID: 43866
				public static LocString DOMESTICATEDDESC = "This plant eats critters and produces " + UI.FormatAsLink("Hydrogen", "HYDROGEN") + ".";
			}

			// Token: 0x020027E0 RID: 10208
			public class SAPTREE
			{
				// Token: 0x0400AB5B RID: 43867
				public static LocString NAME = UI.FormatAsLink("Experiment 52B", "SAPTREE");

				// Token: 0x0400AB5C RID: 43868
				public static LocString DESC = "A " + UI.FormatAsLink("Resin", "RESIN") + "-producing cybernetic tree that shows signs of sentience.\n\nIt is rooted firmly in place, and is waiting for some brave soul to bring it food.";
			}

			// Token: 0x020027E1 RID: 10209
			public class SEEDS
			{
				// Token: 0x02002FA6 RID: 12198
				public class LEAFYPLANT
				{
					// Token: 0x0400BEC8 RID: 48840
					public static LocString NAME = UI.FormatAsLink("Mirth Leaf Seed", "LEAFYPLANT");

					// Token: 0x0400BEC9 RID: 48841
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						UI.FormatAsLink("Mirth Leaf", "LEAFYPLANT"),
						".\n\nDigging up Buried Objects may uncover a Mirth Leaf Seed."
					});
				}

				// Token: 0x02002FA7 RID: 12199
				public class CACTUSPLANT
				{
					// Token: 0x0400BECA RID: 48842
					public static LocString NAME = UI.FormatAsLink("Joya Seed", "CACTUSPLANT");

					// Token: 0x0400BECB RID: 48843
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						UI.FormatAsLink("Jumping Joya", "CACTUSPLANT"),
						".\n\nDigging up Buried Objects may uncover a Joya Seed."
					});
				}

				// Token: 0x02002FA8 RID: 12200
				public class BULBPLANT
				{
					// Token: 0x0400BECC RID: 48844
					public static LocString NAME = UI.FormatAsLink("Buddy Bud Seed", "BULBPLANT");

					// Token: 0x0400BECD RID: 48845
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						UI.FormatAsLink("Buddy Bud", "BULBPLANT"),
						".\n\nDigging up Buried Objects may uncover a Buddy Bud Seed."
					});
				}

				// Token: 0x02002FA9 RID: 12201
				public class JUNGLEGASPLANT
				{
				}

				// Token: 0x02002FAA RID: 12202
				public class PRICKLEFLOWER
				{
					// Token: 0x0400BECE RID: 48846
					public static LocString NAME = UI.FormatAsLink("Blossom Seed", "PRICKLEFLOWER");

					// Token: 0x0400BECF RID: 48847
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						UI.FormatAsLink("Bristle Blossom", "PRICKLEFLOWER"),
						".\n\nDigging up Buried Objects may uncover a Blossom Seed."
					});
				}

				// Token: 0x02002FAB RID: 12203
				public class MUSHROOMPLANT
				{
					// Token: 0x0400BED0 RID: 48848
					public static LocString NAME = UI.FormatAsLink("Fungal Spore", "MUSHROOMPLANT");

					// Token: 0x0400BED1 RID: 48849
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.MUSHROOMPLANT.NAME,
						".\n\nDigging up Buried Objects may uncover a Fungal Spore."
					});
				}

				// Token: 0x02002FAC RID: 12204
				public class COLDWHEAT
				{
					// Token: 0x0400BED2 RID: 48850
					public static LocString NAME = UI.FormatAsLink("Sleet Wheat Grain", "COLDWHEAT");

					// Token: 0x0400BED3 RID: 48851
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.COLDWHEAT.NAME,
						" plant.\n\nGrain can be sown to cultivate more Sleet Wheat, or processed into ",
						UI.FormatAsLink("Food", "FOOD"),
						"."
					});
				}

				// Token: 0x02002FAD RID: 12205
				public class GASGRASS
				{
					// Token: 0x0400BED4 RID: 48852
					public static LocString NAME = UI.FormatAsLink("Gas Grass Seed", "GASGRASS");

					// Token: 0x0400BED5 RID: 48853
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.GASGRASS.NAME,
						" plant."
					});
				}

				// Token: 0x02002FAE RID: 12206
				public class PRICKLEGRASS
				{
					// Token: 0x0400BED6 RID: 48854
					public static LocString NAME = UI.FormatAsLink("Briar Seed", "PRICKLEGRASS");

					// Token: 0x0400BED7 RID: 48855
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.PRICKLEGRASS.NAME,
						".\n\nDigging up Buried Objects may uncover a Briar Seed."
					});
				}

				// Token: 0x02002FAF RID: 12207
				public class CYLINDRICA
				{
					// Token: 0x0400BED8 RID: 48856
					public static LocString NAME = UI.FormatAsLink("Bliss Burst Seed", "CYLINDRICA");

					// Token: 0x0400BED9 RID: 48857
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.CYLINDRICA.NAME,
						".\n\nDigging up Buried Objects may uncover a Bliss Burst Seed."
					});
				}

				// Token: 0x02002FB0 RID: 12208
				public class TOEPLANT
				{
					// Token: 0x0400BEDA RID: 48858
					public static LocString NAME = UI.FormatAsLink("Tranquil Toe Seed", "TOEPLANT");

					// Token: 0x0400BEDB RID: 48859
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.TOEPLANT.NAME,
						".\n\nDigging up Buried Objects may uncover a Tranquil Toe Seed."
					});
				}

				// Token: 0x02002FB1 RID: 12209
				public class WINECUPS
				{
					// Token: 0x0400BEDC RID: 48860
					public static LocString NAME = UI.FormatAsLink("Mallow Seed", "WINECUPS");

					// Token: 0x0400BEDD RID: 48861
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.WINECUPS.NAME,
						".\n\nDigging up Buried Objects may uncover a Mallow Seed."
					});
				}

				// Token: 0x02002FB2 RID: 12210
				public class EVILFLOWER
				{
					// Token: 0x0400BEDE RID: 48862
					public static LocString NAME = UI.FormatAsLink("Sporechid Seed", "EVILFLOWER");

					// Token: 0x0400BEDF RID: 48863
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.EVILFLOWER.NAME,
						".\n\nDigging up Buried Objects may uncover a ",
						CREATURES.SPECIES.SEEDS.EVILFLOWER.NAME,
						"."
					});
				}

				// Token: 0x02002FB3 RID: 12211
				public class SWAMPLILY
				{
					// Token: 0x0400BEE0 RID: 48864
					public static LocString NAME = UI.FormatAsLink("Balm Lily Seed", "SWAMPLILY");

					// Token: 0x0400BEE1 RID: 48865
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.SWAMPLILY.NAME,
						".\n\nDigging up Buried Objects may uncover a Balm Lily Seed."
					});
				}

				// Token: 0x02002FB4 RID: 12212
				public class BASICSINGLEHARVESTPLANT
				{
					// Token: 0x0400BEE2 RID: 48866
					public static LocString NAME = UI.FormatAsLink("Mealwood Seed", "BASICSINGLEHARVESTPLANT");

					// Token: 0x0400BEE3 RID: 48867
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.BASICSINGLEHARVESTPLANT.NAME,
						".\n\nDigging up Buried Objects may uncover a Mealwood Seed."
					});
				}

				// Token: 0x02002FB5 RID: 12213
				public class SWAMPHARVESTPLANT
				{
					// Token: 0x0400BEE4 RID: 48868
					public static LocString NAME = UI.FormatAsLink("Bog Bucket Seed", "SWAMPHARVESTPLANT");

					// Token: 0x0400BEE5 RID: 48869
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.SWAMPHARVESTPLANT.NAME,
						".\n\nDigging up Buried Objects may uncover a Bog Bucket Seed."
					});
				}

				// Token: 0x02002FB6 RID: 12214
				public class WORMPLANT
				{
					// Token: 0x0400BEE6 RID: 48870
					public static LocString NAME = UI.FormatAsLink("Grubfruit Seed", "WORMPLANT");

					// Token: 0x0400BEE7 RID: 48871
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.WORMPLANT.NAME,
						".\n\nDigging up Buried Objects may uncover a Grubfruit Seed."
					});
				}

				// Token: 0x02002FB7 RID: 12215
				public class COLDBREATHER
				{
					// Token: 0x0400BEE8 RID: 48872
					public static LocString NAME = UI.FormatAsLink("Wort Seed", "COLDBREATHER");

					// Token: 0x0400BEE9 RID: 48873
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.COLDBREATHER.NAME,
						".\n\nDigging up Buried Objects may uncover a Wort Seed."
					});
				}

				// Token: 0x02002FB8 RID: 12216
				public class BASICFABRICMATERIALPLANT
				{
					// Token: 0x0400BEEA RID: 48874
					public static LocString NAME = UI.FormatAsLink("Thimble Reed Seed", "BASICFABRICPLANT");

					// Token: 0x0400BEEB RID: 48875
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.BASICFABRICMATERIALPLANT.NAME,
						".\n\nDigging up Buried Objects may uncover a Thimble Reed Seed."
					});
				}

				// Token: 0x02002FB9 RID: 12217
				public class SALTPLANT
				{
					// Token: 0x0400BEEC RID: 48876
					public static LocString NAME = UI.FormatAsLink("Dasha Saltvine Seed", "SALTPLANT");

					// Token: 0x0400BEED RID: 48877
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.SALTPLANT.NAME,
						".\n\nDigging up Buried Objects may uncover a Dasha Saltvine Seed."
					});
				}

				// Token: 0x02002FBA RID: 12218
				public class FILTERPLANT
				{
					// Token: 0x0400BEEE RID: 48878
					public static LocString NAME = UI.FormatAsLink("Hydrocactus Seed", "FILTERPLANT");

					// Token: 0x0400BEEF RID: 48879
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.FILTERPLANT.NAME,
						".\n\nDigging up Buried Objects may uncover a Hydrocactus Seed."
					});
				}

				// Token: 0x02002FBB RID: 12219
				public class SPICE_VINE
				{
					// Token: 0x0400BEF0 RID: 48880
					public static LocString NAME = UI.FormatAsLink("Pincha Pepper Seed", "SPICE_VINE");

					// Token: 0x0400BEF1 RID: 48881
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						CREATURES.SPECIES.SPICE_VINE.NAME,
						".\n\nDigging up Buried Objects may uncover a Pincha Pepper Seed."
					});
				}

				// Token: 0x02002FBC RID: 12220
				public class BEAN_PLANT
				{
					// Token: 0x0400BEF2 RID: 48882
					public static LocString NAME = UI.FormatAsLink("Nosh Bean", "BEAN_PLANT");

					// Token: 0x0400BEF3 RID: 48883
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						UI.FormatAsLink("Nosh Sprout", "BEAN_PLANT"),
						".\n\nDigging up Buried Objects may uncover a Nosh Bean."
					});
				}

				// Token: 0x02002FBD RID: 12221
				public class WOOD_TREE
				{
					// Token: 0x0400BEF4 RID: 48884
					public static LocString NAME = UI.FormatAsLink("Arbor Acorn", "FOREST_TREE");

					// Token: 0x0400BEF5 RID: 48885
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of an ",
						UI.FormatAsLink("Arbor Tree", "FOREST_TREE"),
						".\n\nDigging up Buried Objects may uncover an Arbor Acorn."
					});
				}

				// Token: 0x02002FBE RID: 12222
				public class OILEATER
				{
					// Token: 0x0400BEF6 RID: 48886
					public static LocString NAME = UI.FormatAsLink("Ink Bloom Seed", "OILEATER");

					// Token: 0x0400BEF7 RID: 48887
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						UI.FormatAsLink("Plant", "Ink Bloom"),
						".\n\nDigging up Buried Objects may uncover an Ink Bloom Seed."
					});
				}

				// Token: 0x02002FBF RID: 12223
				public class OXYFERN
				{
					// Token: 0x0400BEF8 RID: 48888
					public static LocString NAME = UI.FormatAsLink("Oxyfern Seed", "OXYFERN");

					// Token: 0x0400BEF9 RID: 48889
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of an ",
						UI.FormatAsLink("Oxyfern", "OXYFERN"),
						" plant."
					});
				}

				// Token: 0x02002FC0 RID: 12224
				public class SEALETTUCE
				{
					// Token: 0x0400BEFA RID: 48890
					public static LocString NAME = UI.FormatAsLink("Waterweed Seed", "SEALETTUCE");

					// Token: 0x0400BEFB RID: 48891
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						UI.FormatAsLink("Waterweed", "SEALETTUCE"),
						".\n\nDigging up Buried Objects may uncover a Waterweed Seed."
					});
				}

				// Token: 0x02002FC1 RID: 12225
				public class CRITTERTRAPPLANT
				{
					// Token: 0x0400BEFC RID: 48892
					public static LocString NAME = UI.FormatAsLink("Saturn Critter Trap Seed", "CRITTERTRAPPLANT");

					// Token: 0x0400BEFD RID: 48893
					public static LocString DESC = string.Concat(new string[]
					{
						"The ",
						UI.FormatAsLink("Seed", "PLANTS"),
						" of a ",
						UI.FormatAsLink("Saturn Critter Trap", "CRITTERTRAPPLANT"),
						".\n\nDigging up Buried Objects may uncover a Saturn Critter Trap Seed."
					});
				}
			}
		}

		// Token: 0x02001CD4 RID: 7380
		public class STATUSITEMS
		{
			// Token: 0x04008410 RID: 33808
			public static LocString NAME_NON_GROWING_PLANT = "Wilted";

			// Token: 0x020027E2 RID: 10210
			public class DROWSY
			{
				// Token: 0x0400AB5D RID: 43869
				public static LocString NAME = "Drowsy";

				// Token: 0x0400AB5E RID: 43870
				public static LocString TOOLTIP = "This critter is looking for a place to nap";
			}

			// Token: 0x020027E3 RID: 10211
			public class SLEEPING
			{
				// Token: 0x0400AB5F RID: 43871
				public static LocString NAME = "Sleeping";

				// Token: 0x0400AB60 RID: 43872
				public static LocString TOOLTIP = "This critter is replenishing its " + UI.PRE_KEYWORD + "Stamina" + UI.PST_KEYWORD;
			}

			// Token: 0x020027E4 RID: 10212
			public class CALL_ADULT
			{
				// Token: 0x0400AB61 RID: 43873
				public static LocString NAME = "Calling Adult";

				// Token: 0x0400AB62 RID: 43874
				public static LocString TOOLTIP = "This baby's craving attention from one of its own kind";
			}

			// Token: 0x020027E5 RID: 10213
			public class HOT
			{
				// Token: 0x0400AB63 RID: 43875
				public static LocString NAME = "Toasty surroundings";

				// Token: 0x0400AB64 RID: 43876
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter cannot let off enough ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" to keep cool in this environment\n\nIt prefers ",
					UI.PRE_KEYWORD,
					"Temperatures",
					UI.PST_KEYWORD,
					" between <b>{0}</b> and <b>{1}</b>"
				});
			}

			// Token: 0x020027E6 RID: 10214
			public class COLD
			{
				// Token: 0x0400AB65 RID: 43877
				public static LocString NAME = "Chilly surroundings";

				// Token: 0x0400AB66 RID: 43878
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter cannot retain enough ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" to stay warm in this environment\n\nIt prefers ",
					UI.PRE_KEYWORD,
					"Temperatures",
					UI.PST_KEYWORD,
					" between <b>{0}</b> and <b>{1}</b>"
				});
			}

			// Token: 0x020027E7 RID: 10215
			public class CROP_TOO_DARK
			{
				// Token: 0x0400AB67 RID: 43879
				public static LocString NAME = "    • " + CREATURES.STATS.ILLUMINATION.NAME;

				// Token: 0x0400AB68 RID: 43880
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Growth will resume when ",
					UI.PRE_KEYWORD,
					"Light",
					UI.PST_KEYWORD,
					" requirements are met"
				});
			}

			// Token: 0x020027E8 RID: 10216
			public class CROP_TOO_BRIGHT
			{
				// Token: 0x0400AB69 RID: 43881
				public static LocString NAME = "    • " + CREATURES.STATS.ILLUMINATION.NAME;

				// Token: 0x0400AB6A RID: 43882
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Growth will resume when ",
					UI.PRE_KEYWORD,
					"Light",
					UI.PST_KEYWORD,
					" requirements are met"
				});
			}

			// Token: 0x020027E9 RID: 10217
			public class CROP_BLIGHTED
			{
				// Token: 0x0400AB6B RID: 43883
				public static LocString NAME = "    • Blighted";

				// Token: 0x0400AB6C RID: 43884
				public static LocString TOOLTIP = "This plant has been struck by blight and will need to be replaced";
			}

			// Token: 0x020027EA RID: 10218
			public class HOT_CROP
			{
				// Token: 0x0400AB6D RID: 43885
				public static LocString NAME = "    • " + DUPLICANTS.STATS.TEMPERATURE.NAME;

				// Token: 0x0400AB6E RID: 43886
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Growth will resume when ambient ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" is between <b>{low_temperature}</b> and <b>{high_temperature}</b>"
				});
			}

			// Token: 0x020027EB RID: 10219
			public class COLD_CROP
			{
				// Token: 0x0400AB6F RID: 43887
				public static LocString NAME = "    • " + DUPLICANTS.STATS.TEMPERATURE.NAME;

				// Token: 0x0400AB70 RID: 43888
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Growth will resume when ambient ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" is between <b>{low_temperature}</b> and <b>{high_temperature}</b>"
				});
			}

			// Token: 0x020027EC RID: 10220
			public class PERFECTTEMPERATURE
			{
				// Token: 0x0400AB71 RID: 43889
				public static LocString NAME = "Ideal Temperature";

				// Token: 0x0400AB72 RID: 43890
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter finds the current ambient ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" comfortable\n\nIdeal Range: <b>{0}</b> - <b>{1}</b>"
				});
			}

			// Token: 0x020027ED RID: 10221
			public class EATING
			{
				// Token: 0x0400AB73 RID: 43891
				public static LocString NAME = "Eating";

				// Token: 0x0400AB74 RID: 43892
				public static LocString TOOLTIP = "This critter found something tasty";
			}

			// Token: 0x020027EE RID: 10222
			public class DIGESTING
			{
				// Token: 0x0400AB75 RID: 43893
				public static LocString NAME = "Digesting";

				// Token: 0x0400AB76 RID: 43894
				public static LocString TOOLTIP = "This critter is working off a big meal";
			}

			// Token: 0x020027EF RID: 10223
			public class COOLING
			{
				// Token: 0x0400AB77 RID: 43895
				public static LocString NAME = "Chilly Breath";

				// Token: 0x0400AB78 RID: 43896
				public static LocString TOOLTIP = "This critter's respiration is having a cooling effect on the area";
			}

			// Token: 0x020027F0 RID: 10224
			public class LOOKINGFORFOOD
			{
				// Token: 0x0400AB79 RID: 43897
				public static LocString NAME = "Foraging";

				// Token: 0x0400AB7A RID: 43898
				public static LocString TOOLTIP = "This critter is hungry and looking for " + UI.PRE_KEYWORD + "Food" + UI.PST_KEYWORD;
			}

			// Token: 0x020027F1 RID: 10225
			public class LOOKINGFORLIQUID
			{
				// Token: 0x0400AB7B RID: 43899
				public static LocString NAME = "Parched";

				// Token: 0x0400AB7C RID: 43900
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter is looking for ",
					UI.PRE_KEYWORD,
					"Liquids",
					UI.PST_KEYWORD,
					" to mop up"
				});
			}

			// Token: 0x020027F2 RID: 10226
			public class LOOKINGFORGAS
			{
				// Token: 0x0400AB7D RID: 43901
				public static LocString NAME = "Seeking Gas";

				// Token: 0x0400AB7E RID: 43902
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter is on the lookout for unbreathable ",
					UI.PRE_KEYWORD,
					"Gases",
					UI.PST_KEYWORD,
					" to collect"
				});
			}

			// Token: 0x020027F3 RID: 10227
			public class IDLE
			{
				// Token: 0x0400AB7F RID: 43903
				public static LocString NAME = "Idle";

				// Token: 0x0400AB80 RID: 43904
				public static LocString TOOLTIP = "Just enjoying life, y'know?";
			}

			// Token: 0x020027F4 RID: 10228
			public class HIVE_DIGESTING
			{
				// Token: 0x0400AB81 RID: 43905
				public static LocString NAME = "Digesting";

				// Token: 0x0400AB82 RID: 43906
				public static LocString TOOLTIP = "Digesting yummy food!";
			}

			// Token: 0x020027F5 RID: 10229
			public class EXCITED_TO_GET_RANCHED
			{
				// Token: 0x0400AB83 RID: 43907
				public static LocString NAME = "Excited";

				// Token: 0x0400AB84 RID: 43908
				public static LocString TOOLTIP = "This critter heard a Duplicant call for it and is very excited!";
			}

			// Token: 0x020027F6 RID: 10230
			public class GETTING_RANCHED
			{
				// Token: 0x0400AB85 RID: 43909
				public static LocString NAME = "Being Groomed";

				// Token: 0x0400AB86 RID: 43910
				public static LocString TOOLTIP = "This critter's going to look so good when they're done";
			}

			// Token: 0x020027F7 RID: 10231
			public class EXCITED_TO_BE_RANCHED
			{
				// Token: 0x0400AB87 RID: 43911
				public static LocString NAME = "Freshly Groomed";

				// Token: 0x0400AB88 RID: 43912
				public static LocString TOOLTIP = "This critter just received some attention and feels great";
			}

			// Token: 0x020027F8 RID: 10232
			public class GETTING_WRANGLED
			{
				// Token: 0x0400AB89 RID: 43913
				public static LocString NAME = "Being Wrangled";

				// Token: 0x0400AB8A RID: 43914
				public static LocString TOOLTIP = "Someone's trying to capture this critter!";
			}

			// Token: 0x020027F9 RID: 10233
			public class BAGGED
			{
				// Token: 0x0400AB8B RID: 43915
				public static LocString NAME = "Trussed";

				// Token: 0x0400AB8C RID: 43916
				public static LocString TOOLTIP = "Tied up and ready for relocation";
			}

			// Token: 0x020027FA RID: 10234
			public class IN_INCUBATOR
			{
				// Token: 0x0400AB8D RID: 43917
				public static LocString NAME = "Incubation Complete";

				// Token: 0x0400AB8E RID: 43918
				public static LocString TOOLTIP = "This critter has hatched and is waiting to be released from its incubator";
			}

			// Token: 0x020027FB RID: 10235
			public class HYPOTHERMIA
			{
				// Token: 0x0400AB8F RID: 43919
				public static LocString NAME = "Freezing";

				// Token: 0x0400AB90 RID: 43920
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Internal ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" is dangerously low"
				});
			}

			// Token: 0x020027FC RID: 10236
			public class SCALDING
			{
				// Token: 0x0400AB91 RID: 43921
				public static LocString NAME = "Scalding";

				// Token: 0x0400AB92 RID: 43922
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Current external ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" is perilously high [<b>{ExternalTemperature}</b> / <b>{TargetTemperature}</b>]"
				});

				// Token: 0x0400AB93 RID: 43923
				public static LocString NOTIFICATION_NAME = "Scalding";

				// Token: 0x0400AB94 RID: 43924
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"Scalding ",
					UI.PRE_KEYWORD,
					"Temperatures",
					UI.PST_KEYWORD,
					" are hurting these Duplicants:"
				});
			}

			// Token: 0x020027FD RID: 10237
			public class HYPERTHERMIA
			{
				// Token: 0x0400AB95 RID: 43925
				public static LocString NAME = "Overheating";

				// Token: 0x0400AB96 RID: 43926
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Internal ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" is dangerously high [<b>{InternalTemperature}</b> / <b>{TargetTemperature}</b>]"
				});
			}

			// Token: 0x020027FE RID: 10238
			public class TIRED
			{
				// Token: 0x0400AB97 RID: 43927
				public static LocString NAME = "Fatigued";

				// Token: 0x0400AB98 RID: 43928
				public static LocString TOOLTIP = "This critter needs some sleepytime";
			}

			// Token: 0x020027FF RID: 10239
			public class BREATH
			{
				// Token: 0x0400AB99 RID: 43929
				public static LocString NAME = "Suffocating";

				// Token: 0x0400AB9A RID: 43930
				public static LocString TOOLTIP = "This critter is about to suffocate";
			}

			// Token: 0x02002800 RID: 10240
			public class DEAD
			{
				// Token: 0x0400AB9B RID: 43931
				public static LocString NAME = "Dead";

				// Token: 0x0400AB9C RID: 43932
				public static LocString TOOLTIP = "This critter won't be getting back up...";
			}

			// Token: 0x02002801 RID: 10241
			public class PLANTDEATH
			{
				// Token: 0x0400AB9D RID: 43933
				public static LocString NAME = "Dead";

				// Token: 0x0400AB9E RID: 43934
				public static LocString TOOLTIP = "This plant will produce no more harvests";

				// Token: 0x0400AB9F RID: 43935
				public static LocString NOTIFICATION = "Plants have died";

				// Token: 0x0400ABA0 RID: 43936
				public static LocString NOTIFICATION_TOOLTIP = "These plants have died and will produce no more harvests:\n";
			}

			// Token: 0x02002802 RID: 10242
			public class STRUGGLING
			{
				// Token: 0x0400ABA1 RID: 43937
				public static LocString NAME = "Struggling";

				// Token: 0x0400ABA2 RID: 43938
				public static LocString TOOLTIP = "This critter is trying to get away";
			}

			// Token: 0x02002803 RID: 10243
			public class BURROWING
			{
				// Token: 0x0400ABA3 RID: 43939
				public static LocString NAME = "Burrowing";

				// Token: 0x0400ABA4 RID: 43940
				public static LocString TOOLTIP = "This critter is trying to hide";
			}

			// Token: 0x02002804 RID: 10244
			public class BURROWED
			{
				// Token: 0x0400ABA5 RID: 43941
				public static LocString NAME = "Burrowed";

				// Token: 0x0400ABA6 RID: 43942
				public static LocString TOOLTIP = "Shh! It thinks it's hiding";
			}

			// Token: 0x02002805 RID: 10245
			public class EMERGING
			{
				// Token: 0x0400ABA7 RID: 43943
				public static LocString NAME = "Emerging";

				// Token: 0x0400ABA8 RID: 43944
				public static LocString TOOLTIP = "This critter is leaving its burrow";
			}

			// Token: 0x02002806 RID: 10246
			public class FORAGINGMATERIAL
			{
				// Token: 0x0400ABA9 RID: 43945
				public static LocString NAME = "Foraging for Materials";

				// Token: 0x0400ABAA RID: 43946
				public static LocString TOOLTIP = "This critter is stocking up on supplies for later use";
			}

			// Token: 0x02002807 RID: 10247
			public class PLANTINGSEED
			{
				// Token: 0x0400ABAB RID: 43947
				public static LocString NAME = "Planting Seed";

				// Token: 0x0400ABAC RID: 43948
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter is burying a ",
					UI.PRE_KEYWORD,
					"Seed",
					UI.PST_KEYWORD,
					" for later"
				});
			}

			// Token: 0x02002808 RID: 10248
			public class RUMMAGINGSEED
			{
				// Token: 0x0400ABAD RID: 43949
				public static LocString NAME = "Rummaging for seeds";

				// Token: 0x0400ABAE RID: 43950
				public static LocString TOOLTIP = "This critter is searching for tasty " + UI.PRE_KEYWORD + "Seeds" + UI.PST_KEYWORD;
			}

			// Token: 0x02002809 RID: 10249
			public class HUGEGG
			{
				// Token: 0x0400ABAF RID: 43951
				public static LocString NAME = "Hugging Eggs";

				// Token: 0x0400ABB0 RID: 43952
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter is snuggling up to an ",
					UI.PRE_KEYWORD,
					"Egg",
					UI.PST_KEYWORD,
					" "
				});
			}

			// Token: 0x0200280A RID: 10250
			public class HUGMINIONWAITING
			{
				// Token: 0x0400ABB1 RID: 43953
				public static LocString NAME = "Hoping for hugs";

				// Token: 0x0400ABB2 RID: 43954
				public static LocString TOOLTIP = "This critter is hoping for a Duplicant to pass by and give it a hug\n\nA hug from a Duplicant will prompt it to cuddle more eggs";
			}

			// Token: 0x0200280B RID: 10251
			public class HUGMINION
			{
				// Token: 0x0400ABB3 RID: 43955
				public static LocString NAME = "Hugging";

				// Token: 0x0400ABB4 RID: 43956
				public static LocString TOOLTIP = "This critter is happily hugging a Duplicant";
			}

			// Token: 0x0200280C RID: 10252
			public class EXPELLING_SOLID
			{
				// Token: 0x0400ABB5 RID: 43957
				public static LocString NAME = "Expelling Waste";

				// Token: 0x0400ABB6 RID: 43958
				public static LocString TOOLTIP = "This critter is doing their \"business\"";
			}

			// Token: 0x0200280D RID: 10253
			public class EXPELLING_GAS
			{
				// Token: 0x0400ABB7 RID: 43959
				public static LocString NAME = "Passing Gas";

				// Token: 0x0400ABB8 RID: 43960
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter is emitting ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					"\n\nYuck!"
				});
			}

			// Token: 0x0200280E RID: 10254
			public class EXPELLING_LIQUID
			{
				// Token: 0x0400ABB9 RID: 43961
				public static LocString NAME = "Expelling Waste";

				// Token: 0x0400ABBA RID: 43962
				public static LocString TOOLTIP = "This critter is doing their \"business\"";
			}

			// Token: 0x0200280F RID: 10255
			public class DEBUGGOTO
			{
				// Token: 0x0400ABBB RID: 43963
				public static LocString NAME = "Moving to debug location";

				// Token: 0x0400ABBC RID: 43964
				public static LocString TOOLTIP = "All that obedience training paid off";
			}

			// Token: 0x02002810 RID: 10256
			public class ATTACK_APPROACH
			{
				// Token: 0x0400ABBD RID: 43965
				public static LocString NAME = "Stalking Target";

				// Token: 0x0400ABBE RID: 43966
				public static LocString TOOLTIP = "This critter is hostile and readying to pounce!";
			}

			// Token: 0x02002811 RID: 10257
			public class ATTACK
			{
				// Token: 0x0400ABBF RID: 43967
				public static LocString NAME = "Combat!";

				// Token: 0x0400ABC0 RID: 43968
				public static LocString TOOLTIP = "This critter is on the attack!";
			}

			// Token: 0x02002812 RID: 10258
			public class ATTACKINGENTITY
			{
				// Token: 0x0400ABC1 RID: 43969
				public static LocString NAME = "Attacking";

				// Token: 0x0400ABC2 RID: 43970
				public static LocString TOOLTIP = "This critter is violently defending their young";
			}

			// Token: 0x02002813 RID: 10259
			public class PROTECTINGENTITY
			{
				// Token: 0x0400ABC3 RID: 43971
				public static LocString NAME = "Protecting";

				// Token: 0x0400ABC4 RID: 43972
				public static LocString TOOLTIP = "This creature is guarding something special to them and will likely attack if approached";
			}

			// Token: 0x02002814 RID: 10260
			public class LAYINGANEGG
			{
				// Token: 0x0400ABC5 RID: 43973
				public static LocString NAME = "Laying egg";

				// Token: 0x0400ABC6 RID: 43974
				public static LocString TOOLTIP = "Witness the miracle of life!";
			}

			// Token: 0x02002815 RID: 10261
			public class TENDINGANEGG
			{
				// Token: 0x0400ABC7 RID: 43975
				public static LocString NAME = "Tending egg";

				// Token: 0x0400ABC8 RID: 43976
				public static LocString TOOLTIP = "Nurturing the miracle of life!";
			}

			// Token: 0x02002816 RID: 10262
			public class GROWINGUP
			{
				// Token: 0x0400ABC9 RID: 43977
				public static LocString NAME = "Maturing";

				// Token: 0x0400ABCA RID: 43978
				public static LocString TOOLTIP = "This baby critter is about to reach adulthood";
			}

			// Token: 0x02002817 RID: 10263
			public class SUFFOCATING
			{
				// Token: 0x0400ABCB RID: 43979
				public static LocString NAME = "Suffocating";

				// Token: 0x0400ABCC RID: 43980
				public static LocString TOOLTIP = "This critter cannot breathe";
			}

			// Token: 0x02002818 RID: 10264
			public class HATCHING
			{
				// Token: 0x0400ABCD RID: 43981
				public static LocString NAME = "Hatching";

				// Token: 0x0400ABCE RID: 43982
				public static LocString TOOLTIP = "Here it comes!";
			}

			// Token: 0x02002819 RID: 10265
			public class INCUBATING
			{
				// Token: 0x0400ABCF RID: 43983
				public static LocString NAME = "Incubating";

				// Token: 0x0400ABD0 RID: 43984
				public static LocString TOOLTIP = "Cozily preparing to meet the world";
			}

			// Token: 0x0200281A RID: 10266
			public class CONSIDERINGLURE
			{
				// Token: 0x0400ABD1 RID: 43985
				public static LocString NAME = "Piqued";

				// Token: 0x0400ABD2 RID: 43986
				public static LocString TOOLTIP = "This critter is tempted to bite a nearby " + UI.PRE_KEYWORD + "Lure" + UI.PST_KEYWORD;
			}

			// Token: 0x0200281B RID: 10267
			public class FALLING
			{
				// Token: 0x0400ABD3 RID: 43987
				public static LocString NAME = "Falling";

				// Token: 0x0400ABD4 RID: 43988
				public static LocString TOOLTIP = "AHHHH!";
			}

			// Token: 0x0200281C RID: 10268
			public class FLOPPING
			{
				// Token: 0x0400ABD5 RID: 43989
				public static LocString NAME = "Flopping";

				// Token: 0x0400ABD6 RID: 43990
				public static LocString TOOLTIP = "Fish out of water!";
			}

			// Token: 0x0200281D RID: 10269
			public class DRYINGOUT
			{
				// Token: 0x0400ABD7 RID: 43991
				public static LocString NAME = "    • Beached";

				// Token: 0x0400ABD8 RID: 43992
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This plant must be submerged in ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" to grow"
				});
			}

			// Token: 0x0200281E RID: 10270
			public class GROWING
			{
				// Token: 0x0400ABD9 RID: 43993
				public static LocString NAME = "Growing [{PercentGrow}%]";

				// Token: 0x0400ABDA RID: 43994
				public static LocString TOOLTIP = "Next harvest: <b>{TimeUntilNextHarvest}</b>";
			}

			// Token: 0x0200281F RID: 10271
			public class CROP_SLEEPING
			{
				// Token: 0x0400ABDB RID: 43995
				public static LocString NAME = "Sleeping [{REASON}]";

				// Token: 0x0400ABDC RID: 43996
				public static LocString TOOLTIP = "Requires: {REQUIREMENTS}";

				// Token: 0x0400ABDD RID: 43997
				public static LocString REQUIREMENT_LUMINANCE = "<b>{0}</b> Lux";

				// Token: 0x0400ABDE RID: 43998
				public static LocString REASON_TOO_DARK = "Too Dark";

				// Token: 0x0400ABDF RID: 43999
				public static LocString REASON_TOO_BRIGHT = "Too Bright";
			}

			// Token: 0x02002820 RID: 10272
			public class MOLTING
			{
				// Token: 0x0400ABE0 RID: 44000
				public static LocString NAME = "Molting";

				// Token: 0x0400ABE1 RID: 44001
				public static LocString TOOLTIP = "This critter is shedding its skin. Yuck";
			}

			// Token: 0x02002821 RID: 10273
			public class CLEANING
			{
				// Token: 0x0400ABE2 RID: 44002
				public static LocString NAME = "Cleaning";

				// Token: 0x0400ABE3 RID: 44003
				public static LocString TOOLTIP = "This critter is de-germ-ifying its liquid surroundings";
			}

			// Token: 0x02002822 RID: 10274
			public class NEEDSFERTILIZER
			{
				// Token: 0x0400ABE4 RID: 44004
				public static LocString NAME = "    • " + CREATURES.STATS.FERTILIZATION.NAME;

				// Token: 0x0400ABE5 RID: 44005
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Growth will resume when ",
					UI.PRE_KEYWORD,
					"Fertilization",
					UI.PST_KEYWORD,
					" requirements are met"
				});

				// Token: 0x0400ABE6 RID: 44006
				public static LocString LINE_ITEM = "\n            • {Resource}: {Amount}";
			}

			// Token: 0x02002823 RID: 10275
			public class NEEDSIRRIGATION
			{
				// Token: 0x0400ABE7 RID: 44007
				public static LocString NAME = "    • " + CREATURES.STATS.IRRIGATION.NAME;

				// Token: 0x0400ABE8 RID: 44008
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Growth will resume when ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" requirements are met"
				});

				// Token: 0x0400ABE9 RID: 44009
				public static LocString LINE_ITEM = "\n            • {Resource}: {Amount}";
			}

			// Token: 0x02002824 RID: 10276
			public class WRONGFERTILIZER
			{
				// Token: 0x0400ABEA RID: 44010
				public static LocString NAME = "    • " + CREATURES.STATS.FERTILIZATION.NAME;

				// Token: 0x0400ABEB RID: 44011
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This farm is storing materials that are not suitable for this plant\n\nEmpty this building's ",
					UI.PRE_KEYWORD,
					"Storage",
					UI.PST_KEYWORD,
					" to remove the unusable materials"
				});

				// Token: 0x0400ABEC RID: 44012
				public static LocString LINE_ITEM = "            • {0}: {1}\n";
			}

			// Token: 0x02002825 RID: 10277
			public class WRONGIRRIGATION
			{
				// Token: 0x0400ABED RID: 44013
				public static LocString NAME = "    • " + CREATURES.STATS.FERTILIZATION.NAME;

				// Token: 0x0400ABEE RID: 44014
				public static LocString TOOLTIP = "This farm is storing materials that are not suitable for this plant\n\nEmpty this building's storage to remove the unusable materials";

				// Token: 0x0400ABEF RID: 44015
				public static LocString LINE_ITEM = "            • {0}: {1}\n";
			}

			// Token: 0x02002826 RID: 10278
			public class WRONGFERTILIZERMAJOR
			{
				// Token: 0x0400ABF0 RID: 44016
				public static LocString NAME = "    • " + CREATURES.STATS.FERTILIZATION.NAME;

				// Token: 0x0400ABF1 RID: 44017
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This farm is storing materials that are not suitable for this plant\n\n",
					UI.PRE_KEYWORD,
					"Empty Storage",
					UI.PST_KEYWORD,
					" on this building to remove the unusable materials"
				});

				// Token: 0x0400ABF2 RID: 44018
				public static LocString LINE_ITEM = "        " + CREATURES.STATUSITEMS.WRONGFERTILIZER.LINE_ITEM;
			}

			// Token: 0x02002827 RID: 10279
			public class WRONGIRRIGATIONMAJOR
			{
				// Token: 0x0400ABF3 RID: 44019
				public static LocString NAME = "    • " + CREATURES.STATS.IRRIGATION.NAME;

				// Token: 0x0400ABF4 RID: 44020
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This farm is storing materials that are not suitable for this plant\n\n",
					UI.PRE_KEYWORD,
					"Empty Storage",
					UI.PST_KEYWORD,
					" on this building to remove the incorrect materials"
				});

				// Token: 0x0400ABF5 RID: 44021
				public static LocString LINE_ITEM = "        " + CREATURES.STATUSITEMS.WRONGIRRIGATION.LINE_ITEM;
			}

			// Token: 0x02002828 RID: 10280
			public class CANTACCEPTFERTILIZER
			{
				// Token: 0x0400ABF6 RID: 44022
				public static LocString NAME = "    • " + CREATURES.STATS.FERTILIZATION.NAME;

				// Token: 0x0400ABF7 RID: 44023
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This farm plot does not accept ",
					UI.PRE_KEYWORD,
					"Fertilizer",
					UI.PST_KEYWORD,
					"\n\nMove the selected plant to a fertilization capable plot for optimal growth"
				});
			}

			// Token: 0x02002829 RID: 10281
			public class CANTACCEPTIRRIGATION
			{
				// Token: 0x0400ABF8 RID: 44024
				public static LocString NAME = "    • " + CREATURES.STATS.IRRIGATION.NAME;

				// Token: 0x0400ABF9 RID: 44025
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This farm plot does not accept ",
					UI.PRE_KEYWORD,
					"Irrigation",
					UI.PST_KEYWORD,
					"\n\nMove the selected plant to an irrigation capable plot for optimal growth"
				});
			}

			// Token: 0x0200282A RID: 10282
			public class READYFORHARVEST
			{
				// Token: 0x0400ABFA RID: 44026
				public static LocString NAME = "Harvest Ready";

				// Token: 0x0400ABFB RID: 44027
				public static LocString TOOLTIP = "This plant can be harvested for materials";
			}

			// Token: 0x0200282B RID: 10283
			public class LOW_YIELD
			{
				// Token: 0x0400ABFC RID: 44028
				public static LocString NAME = "Standard Yield";

				// Token: 0x0400ABFD RID: 44029
				public static LocString TOOLTIP = "This plant produced an average yield";
			}

			// Token: 0x0200282C RID: 10284
			public class NORMAL_YIELD
			{
				// Token: 0x0400ABFE RID: 44030
				public static LocString NAME = "Good Yield";

				// Token: 0x0400ABFF RID: 44031
				public static LocString TOOLTIP = "Comfortable conditions allowed this plant to produce a better yield\n{Effects}";

				// Token: 0x0400AC00 RID: 44032
				public static LocString LINE_ITEM = "    • {0}\n";
			}

			// Token: 0x0200282D RID: 10285
			public class HIGH_YIELD
			{
				// Token: 0x0400AC01 RID: 44033
				public static LocString NAME = "Excellent Yield";

				// Token: 0x0400AC02 RID: 44034
				public static LocString TOOLTIP = "Consistently ideal conditions allowed this plant to bear a large yield\n{Effects}";

				// Token: 0x0400AC03 RID: 44035
				public static LocString LINE_ITEM = "    • {0}\n";
			}

			// Token: 0x0200282E RID: 10286
			public class ENTOMBED
			{
				// Token: 0x0400AC04 RID: 44036
				public static LocString NAME = "Entombed";

				// Token: 0x0400AC05 RID: 44037
				public static LocString TOOLTIP = "This {0} is trapped and needs help digging out";

				// Token: 0x0400AC06 RID: 44038
				public static LocString LINE_ITEM = "    • Entombed";
			}

			// Token: 0x0200282F RID: 10287
			public class DROWNING
			{
				// Token: 0x0400AC07 RID: 44039
				public static LocString NAME = "Drowning";

				// Token: 0x0400AC08 RID: 44040
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter can't breathe in ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					"!"
				});
			}

			// Token: 0x02002830 RID: 10288
			public class DISABLED
			{
				// Token: 0x0400AC09 RID: 44041
				public static LocString NAME = "Disabled";

				// Token: 0x0400AC0A RID: 44042
				public static LocString TOOLTIP = "Something is preventing this critter from functioning!";
			}

			// Token: 0x02002831 RID: 10289
			public class SATURATED
			{
				// Token: 0x0400AC0B RID: 44043
				public static LocString NAME = "Too Wet!";

				// Token: 0x0400AC0C RID: 44044
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter likes ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					", but not that much!"
				});
			}

			// Token: 0x02002832 RID: 10290
			public class WILTING
			{
				// Token: 0x0400AC0D RID: 44045
				public static LocString NAME = "Growth Halted{Reasons}";

				// Token: 0x0400AC0E RID: 44046
				public static LocString TOOLTIP = "Growth will resume when conditions improve";
			}

			// Token: 0x02002833 RID: 10291
			public class WILTINGDOMESTIC
			{
				// Token: 0x0400AC0F RID: 44047
				public static LocString NAME = "Growth Halted{Reasons}";

				// Token: 0x0400AC10 RID: 44048
				public static LocString TOOLTIP = "Growth will resume when conditions improve";
			}

			// Token: 0x02002834 RID: 10292
			public class WILTING_NON_GROWING_PLANT
			{
				// Token: 0x0400AC11 RID: 44049
				public static LocString NAME = "Growth Halted{Reasons}";

				// Token: 0x0400AC12 RID: 44050
				public static LocString TOOLTIP = "Growth will resume when conditions improve";
			}

			// Token: 0x02002835 RID: 10293
			public class BARREN
			{
				// Token: 0x0400AC13 RID: 44051
				public static LocString NAME = "Barren";

				// Token: 0x0400AC14 RID: 44052
				public static LocString TOOLTIP = "This plant will produce no more " + UI.PRE_KEYWORD + "Seeds" + UI.PST_KEYWORD;
			}

			// Token: 0x02002836 RID: 10294
			public class ATMOSPHERICPRESSURETOOLOW
			{
				// Token: 0x0400AC15 RID: 44053
				public static LocString NAME = "    • Pressure";

				// Token: 0x0400AC16 RID: 44054
				public static LocString TOOLTIP = "Growth will resume when air pressure is between <b>{low_mass}</b> and <b>{high_mass}</b>";
			}

			// Token: 0x02002837 RID: 10295
			public class WRONGATMOSPHERE
			{
				// Token: 0x0400AC17 RID: 44055
				public static LocString NAME = "    • Atmosphere";

				// Token: 0x0400AC18 RID: 44056
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Growth will resume when submersed in one of the following ",
					UI.PRE_KEYWORD,
					"Gases",
					UI.PST_KEYWORD,
					": {elements}"
				});
			}

			// Token: 0x02002838 RID: 10296
			public class ATMOSPHERICPRESSURETOOHIGH
			{
				// Token: 0x0400AC19 RID: 44057
				public static LocString NAME = "    • Pressure";

				// Token: 0x0400AC1A RID: 44058
				public static LocString TOOLTIP = "Growth will resume when air pressure is between <b>{low_mass}</b> and <b>{high_mass}</b>";
			}

			// Token: 0x02002839 RID: 10297
			public class PERFECTATMOSPHERICPRESSURE
			{
				// Token: 0x0400AC1B RID: 44059
				public static LocString NAME = "Ideal Air Pressure";

				// Token: 0x0400AC1C RID: 44060
				public static LocString TOOLTIP = "This critter is comfortable in the current atmospheric pressure\n\nIdeal Range: <b>{0}</b> - <b>{1}</b>";
			}

			// Token: 0x0200283A RID: 10298
			public class HEALTHSTATUS
			{
				// Token: 0x0400AC1D RID: 44061
				public static LocString NAME = "Injuries: {healthState}";

				// Token: 0x0400AC1E RID: 44062
				public static LocString TOOLTIP = "Current physical status: {healthState}";
			}

			// Token: 0x0200283B RID: 10299
			public class FLEEING
			{
				// Token: 0x0400AC1F RID: 44063
				public static LocString NAME = "Fleeing";

				// Token: 0x0400AC20 RID: 44064
				public static LocString TOOLTIP = "This critter is trying to escape\nGet'em!";
			}

			// Token: 0x0200283C RID: 10300
			public class REFRIGERATEDFROZEN
			{
				// Token: 0x0400AC21 RID: 44065
				public static LocString NAME = "Deep Freeze";

				// Token: 0x0400AC22 RID: 44066
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"Temperatures",
					UI.PST_KEYWORD,
					" below <b>{PreserveTemperature}</b> are greatly prolonging the shelf-life of this food\n\n",
					UI.PRE_KEYWORD,
					"Temperatures",
					UI.PST_KEYWORD,
					" above <b>{RotTemperature}</b> spoil food more quickly"
				});
			}

			// Token: 0x0200283D RID: 10301
			public class REFRIGERATED
			{
				// Token: 0x0400AC23 RID: 44067
				public static LocString NAME = "Refrigerated";

				// Token: 0x0400AC24 RID: 44068
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Ideal ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" storage is slowing this food's ",
					UI.PRE_KEYWORD,
					"Decay Rate",
					UI.PST_KEYWORD,
					"\n\n",
					UI.PRE_KEYWORD,
					"Temperatures",
					UI.PST_KEYWORD,
					" above <b>{RotTemperature}</b> spoil food more quickly\n\nStore food below {PreserveTemperature} to further reduce spoilage."
				});
			}

			// Token: 0x0200283E RID: 10302
			public class UNREFRIGERATED
			{
				// Token: 0x0400AC25 RID: 44069
				public static LocString NAME = "Unrefrigerated";

				// Token: 0x0400AC26 RID: 44070
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This food is warm\n\n",
					UI.PRE_KEYWORD,
					"Temperatures",
					UI.PST_KEYWORD,
					" above <b>{RotTemperature}</b> spoil food more quickly"
				});
			}

			// Token: 0x0200283F RID: 10303
			public class CONTAMINATEDATMOSPHERE
			{
				// Token: 0x0400AC27 RID: 44071
				public static LocString NAME = "Pollution Exposure";

				// Token: 0x0400AC28 RID: 44072
				public static LocString TOOLTIP = "Exposure to contaminants is accelerating this food's " + UI.PRE_KEYWORD + "Decay Rate" + UI.PST_KEYWORD;
			}

			// Token: 0x02002840 RID: 10304
			public class STERILIZINGATMOSPHERE
			{
				// Token: 0x0400AC29 RID: 44073
				public static LocString NAME = "Sterile Atmosphere";

				// Token: 0x0400AC2A RID: 44074
				public static LocString TOOLTIP = "Microbe destroying conditions have decreased this food's " + UI.PRE_KEYWORD + "Decay Rate" + UI.PST_KEYWORD;
			}

			// Token: 0x02002841 RID: 10305
			public class EXCHANGINGELEMENTCONSUME
			{
				// Token: 0x0400AC2B RID: 44075
				public static LocString NAME = "Consuming {ConsumeElement} at {ConsumeRate}";

				// Token: 0x0400AC2C RID: 44076
				public static LocString TOOLTIP = "{ConsumeElement} is being used at a rate of " + UI.FormatAsNegativeRate("{ConsumeRate}");
			}

			// Token: 0x02002842 RID: 10306
			public class EXCHANGINGELEMENTOUTPUT
			{
				// Token: 0x0400AC2D RID: 44077
				public static LocString NAME = "Outputting {OutputElement} at {OutputRate}";

				// Token: 0x0400AC2E RID: 44078
				public static LocString TOOLTIP = "{OutputElement} is being expelled at a rate of " + UI.FormatAsPositiveRate("{OutputRate}");
			}

			// Token: 0x02002843 RID: 10307
			public class FRESH
			{
				// Token: 0x0400AC2F RID: 44079
				public static LocString NAME = "Fresh {RotPercentage}";

				// Token: 0x0400AC30 RID: 44080
				public static LocString TOOLTIP = "Get'em while they're hot!\n\n{RotTooltip}";
			}

			// Token: 0x02002844 RID: 10308
			public class STALE
			{
				// Token: 0x0400AC31 RID: 44081
				public static LocString NAME = "Stale {RotPercentage}";

				// Token: 0x0400AC32 RID: 44082
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					" is still edible but will soon expire\n{RotTooltip}"
				});
			}

			// Token: 0x02002845 RID: 10309
			public class SPOILED
			{
				// Token: 0x0400AC33 RID: 44083
				public static LocString NAME = "Rotten";

				// Token: 0x0400AC34 RID: 44084
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					" has putrefied and should not be consumed"
				});
			}

			// Token: 0x02002846 RID: 10310
			public class STUNTED_SCALE_GROWTH
			{
				// Token: 0x0400AC35 RID: 44085
				public static LocString NAME = "Stunted Scales";

				// Token: 0x0400AC36 RID: 44086
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter's ",
					UI.PRE_KEYWORD,
					"Scale Growth",
					UI.PST_KEYWORD,
					" is being stunted by an unfavorable environment"
				});
			}

			// Token: 0x02002847 RID: 10311
			public class RECEPTACLEINOPERATIONAL
			{
				// Token: 0x0400AC37 RID: 44087
				public static LocString NAME = "    • Farm plot inoperable";

				// Token: 0x0400AC38 RID: 44088
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This farm plot cannot grow ",
					UI.PRE_KEYWORD,
					"Plants",
					UI.PST_KEYWORD,
					" in its current state"
				});
			}

			// Token: 0x02002848 RID: 10312
			public class TRAPPED
			{
				// Token: 0x0400AC39 RID: 44089
				public static LocString NAME = "Trapped";

				// Token: 0x0400AC3A RID: 44090
				public static LocString TOOLTIP = "This critter has been contained and cannot escape";
			}

			// Token: 0x02002849 RID: 10313
			public class EXHALING
			{
				// Token: 0x0400AC3B RID: 44091
				public static LocString NAME = "Exhaling";

				// Token: 0x0400AC3C RID: 44092
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter is expelling ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" from its lungsacs"
				});
			}

			// Token: 0x0200284A RID: 10314
			public class INHALING
			{
				// Token: 0x0400AC3D RID: 44093
				public static LocString NAME = "Inhaling";

				// Token: 0x0400AC3E RID: 44094
				public static LocString TOOLTIP = "This critter is taking a deep breath";
			}

			// Token: 0x0200284B RID: 10315
			public class EXTERNALTEMPERATURE
			{
				// Token: 0x0400AC3F RID: 44095
				public static LocString NAME = "External Temperature";

				// Token: 0x0400AC40 RID: 44096
				public static LocString TOOLTIP = "External Temperature\n\nThis critter's environment is {0}";
			}

			// Token: 0x0200284C RID: 10316
			public class RECEPTACLEOPERATIONAL
			{
				// Token: 0x0400AC41 RID: 44097
				public static LocString NAME = "Farm plot operational";

				// Token: 0x0400AC42 RID: 44098
				public static LocString TOOLTIP = "This plant's farm plot is operational";
			}

			// Token: 0x0200284D RID: 10317
			public class DOMESTICATION
			{
				// Token: 0x0400AC43 RID: 44099
				public static LocString NAME = "Domestication Level: {LevelName}";

				// Token: 0x0400AC44 RID: 44100
				public static LocString TOOLTIP = "{LevelDesc}";
			}

			// Token: 0x0200284E RID: 10318
			public class HUNGRY
			{
				// Token: 0x0400AC45 RID: 44101
				public static LocString NAME = "Hungry";

				// Token: 0x0400AC46 RID: 44102
				public static LocString TOOLTIP = "This critter's tummy is rumbling";
			}

			// Token: 0x0200284F RID: 10319
			public class HIVEHUNGRY
			{
				// Token: 0x0400AC47 RID: 44103
				public static LocString NAME = "Food Supply Low";

				// Token: 0x0400AC48 RID: 44104
				public static LocString TOOLTIP = "The food reserves in this hive are running low";
			}

			// Token: 0x02002850 RID: 10320
			public class STARVING
			{
				// Token: 0x0400AC49 RID: 44105
				public static LocString NAME = "Starving\nTime until death: {TimeUntilDeath}\n";

				// Token: 0x0400AC4A RID: 44106
				public static LocString TOOLTIP = "This critter is starving and will die if it is not fed soon";

				// Token: 0x0400AC4B RID: 44107
				public static LocString NOTIFICATION_NAME = "Critter Starvation";

				// Token: 0x0400AC4C RID: 44108
				public static LocString NOTIFICATION_TOOLTIP = "These critters are starving and will die if not fed soon:";
			}

			// Token: 0x02002851 RID: 10321
			public class OLD
			{
				// Token: 0x0400AC4D RID: 44109
				public static LocString NAME = "Elderly";

				// Token: 0x0400AC4E RID: 44110
				public static LocString TOOLTIP = "This sweet ol'critter is over the hill and will pass on in <b>{TimeUntilDeath}</b>";
			}

			// Token: 0x02002852 RID: 10322
			public class DIVERGENT_WILL_TEND
			{
				// Token: 0x0400AC4F RID: 44111
				public static LocString NAME = "Moving to Plant";

				// Token: 0x0400AC50 RID: 44112
				public static LocString TOOLTIP = "This critter is off to tend a plant that's caught its attention";
			}

			// Token: 0x02002853 RID: 10323
			public class DIVERGENT_TENDING
			{
				// Token: 0x0400AC51 RID: 44113
				public static LocString NAME = "Plant Tending";

				// Token: 0x0400AC52 RID: 44114
				public static LocString TOOLTIP = "This critter is snuggling a plant to help it grow";
			}

			// Token: 0x02002854 RID: 10324
			public class NOSLEEPSPOT
			{
				// Token: 0x0400AC53 RID: 44115
				public static LocString NAME = "Nowhere To Sleep";

				// Token: 0x0400AC54 RID: 44116
				public static LocString TOOLTIP = "This critter wants to sleep but can't find a good spot to snuggle up!";
			}

			// Token: 0x02002855 RID: 10325
			public class PILOTNEEDED
			{
			}

			// Token: 0x02002856 RID: 10326
			public class ORIGINALPLANTMUTATION
			{
				// Token: 0x0400AC55 RID: 44117
				public static LocString NAME = "Original Plant";

				// Token: 0x0400AC56 RID: 44118
				public static LocString TOOLTIP = "This is the original, unmutated variant of this species.";
			}

			// Token: 0x02002857 RID: 10327
			public class UNKNOWNMUTATION
			{
				// Token: 0x0400AC57 RID: 44119
				public static LocString NAME = "Unknown Mutation";

				// Token: 0x0400AC58 RID: 44120
				public static LocString TOOLTIP = "This seed carries some unexpected genetic markers. Analyze it at the " + UI.FormatAsLink(BUILDINGS.PREFABS.GENETICANALYSISSTATION.NAME, "GENETICANALYSISSTATION") + " to learn its secrets.";
			}

			// Token: 0x02002858 RID: 10328
			public class SPECIFICPLANTMUTATION
			{
				// Token: 0x0400AC59 RID: 44121
				public static LocString NAME = "Mutant Plant: {MutationName}";

				// Token: 0x0400AC5A RID: 44122
				public static LocString TOOLTIP = "This plant is mutated with a genetic variant I call {MutationName}.";
			}

			// Token: 0x02002859 RID: 10329
			public class CROP_TOO_NONRADIATED
			{
				// Token: 0x0400AC5B RID: 44123
				public static LocString NAME = "    • Low Radiation Levels";

				// Token: 0x0400AC5C RID: 44124
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Growth will resume when ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" requirements are met"
				});
			}

			// Token: 0x0200285A RID: 10330
			public class CROP_TOO_RADIATED
			{
				// Token: 0x0400AC5D RID: 44125
				public static LocString NAME = "    • High Radiation Levels";

				// Token: 0x0400AC5E RID: 44126
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Growth will resume when ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" requirements are met"
				});
			}

			// Token: 0x0200285B RID: 10331
			public class ELEMENT_GROWTH_GROWING
			{
				// Token: 0x0400AC5F RID: 44127
				public static LocString NAME = "Picky Eater: Just Right";

				// Token: 0x0400AC60 RID: 44128
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter's ",
					UI.PRE_KEYWORD,
					"Quill Growth",
					UI.PST_KEYWORD,
					" rate is optimal\n\nPreferred food temperature range: {templo}-{temphi}"
				});

				// Token: 0x0400AC61 RID: 44129
				public static LocString PREFERRED_TEMP = "Last eaten: {element} at {temperature}";
			}

			// Token: 0x0200285C RID: 10332
			public class ELEMENT_GROWTH_STUNTED
			{
				// Token: 0x0400AC62 RID: 44130
				public static LocString NAME = "Picky Eater: {reason}";

				// Token: 0x0400AC63 RID: 44131
				public static LocString TOO_HOT = "Too Hot";

				// Token: 0x0400AC64 RID: 44132
				public static LocString TOO_COLD = "Too Cold";

				// Token: 0x0400AC65 RID: 44133
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter's ",
					UI.PRE_KEYWORD,
					"Quill Growth",
					UI.PST_KEYWORD,
					" rate has slowed because they ate food outside their preferred temperature range\n\nPreferred food temperature range: {templo}-{temphi}"
				});
			}

			// Token: 0x0200285D RID: 10333
			public class ELEMENT_GROWTH_HALTED
			{
				// Token: 0x0400AC66 RID: 44134
				public static LocString NAME = "Picky Eater: Hungry";

				// Token: 0x0400AC67 RID: 44135
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter's ",
					UI.PRE_KEYWORD,
					"Quill Growth",
					UI.PST_KEYWORD,
					" is halted because they are hungry\n\nPreferred food temperature range: {templo}-{temphi}"
				});
			}

			// Token: 0x0200285E RID: 10334
			public class ELEMENT_GROWTH_COMPLETE
			{
				// Token: 0x0400AC68 RID: 44136
				public static LocString NAME = "Picky Eater: All Done";

				// Token: 0x0400AC69 RID: 44137
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter's ",
					UI.PRE_KEYWORD,
					"Tonic Root",
					UI.PST_KEYWORD,
					" quills are fully grown\n\nPreferred food temperature range: {templo}-{temphi}"
				});
			}

			// Token: 0x0200285F RID: 10335
			public class GRAVITAS_CREATURE_MANIPULATOR_COOLDOWN
			{
				// Token: 0x0400AC6A RID: 44138
				public static LocString NAME = "Processing Sample: {percent}";

				// Token: 0x0400AC6B RID: 44139
				public static LocString TOOLTIP = "This building is busy analyzing genetic data from a recently scanned specimen\n\nRemaining: {timeleft}";
			}
		}

		// Token: 0x02001CD5 RID: 7381
		public class STATS
		{
			// Token: 0x02002860 RID: 10336
			public class HEALTH
			{
				// Token: 0x0400AC6C RID: 44140
				public static LocString NAME = "Health";
			}

			// Token: 0x02002861 RID: 10337
			public class AGE
			{
				// Token: 0x0400AC6D RID: 44141
				public static LocString NAME = "Age";

				// Token: 0x0400AC6E RID: 44142
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter will die when its ",
					UI.PRE_KEYWORD,
					"Age",
					UI.PST_KEYWORD,
					" reaches its species' maximum lifespan"
				});
			}

			// Token: 0x02002862 RID: 10338
			public class MATURITY
			{
				// Token: 0x0400AC6F RID: 44143
				public static LocString NAME = "Growth Progress";

				// Token: 0x0400AC70 RID: 44144
				public static LocString TOOLTIP = "Growth Progress\n\n";

				// Token: 0x0400AC71 RID: 44145
				public static LocString TOOLTIP_GROWING = "Predicted Maturation: <b>{0}</b>";

				// Token: 0x0400AC72 RID: 44146
				public static LocString TOOLTIP_GROWING_CROP = "Predicted Maturation Time: <b>{0}</b>\nNext harvest occurs in approximately <b>{1}</b>";

				// Token: 0x0400AC73 RID: 44147
				public static LocString TOOLTIP_GROWN = "Growth paused while plant awaits harvest";

				// Token: 0x0400AC74 RID: 44148
				public static LocString TOOLTIP_STALLED = "Poor conditions have halted this plant's growth";

				// Token: 0x0400AC75 RID: 44149
				public static LocString AMOUNT_DESC_FMT = "{0}: {1}\nNext harvest in <b>{2}</b>";

				// Token: 0x0400AC76 RID: 44150
				public static LocString GROWING = "Domestic Growth Rate";

				// Token: 0x0400AC77 RID: 44151
				public static LocString GROWINGWILD = "Wild Growth Rate";
			}

			// Token: 0x02002863 RID: 10339
			public class FERTILIZATION
			{
				// Token: 0x0400AC78 RID: 44152
				public static LocString NAME = "Fertilization";

				// Token: 0x0400AC79 RID: 44153
				public static LocString CONSUME_MODIFIER = "Consuming";

				// Token: 0x0400AC7A RID: 44154
				public static LocString ABSORBING_MODIFIER = "Absorbing";
			}

			// Token: 0x02002864 RID: 10340
			public class DOMESTICATION
			{
				// Token: 0x0400AC7B RID: 44155
				public static LocString NAME = "Domestication";

				// Token: 0x0400AC7C RID: 44156
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Fully ",
					UI.PRE_KEYWORD,
					"Tame",
					UI.PST_KEYWORD,
					" critters produce more materials than wild ones, and may even provide psychological benefits to my colony\n\nThis critter is <b>{0}</b> domesticated"
				});
			}

			// Token: 0x02002865 RID: 10341
			public class HAPPINESS
			{
				// Token: 0x0400AC7D RID: 44157
				public static LocString NAME = "Happiness";

				// Token: 0x0400AC7E RID: 44158
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"High ",
					UI.PRE_KEYWORD,
					"Happiness",
					UI.PST_KEYWORD,
					" increases a critter's productivity and indirectly improves their ",
					UI.PRE_KEYWORD,
					"Egg",
					UI.PST_KEYWORD,
					" laying rates\n\nIt also provides the satisfaction in knowing they're living a good little critter life"
				});
			}

			// Token: 0x02002866 RID: 10342
			public class WILDNESS
			{
				// Token: 0x0400AC7F RID: 44159
				public static LocString NAME = "Wildness";

				// Token: 0x0400AC80 RID: 44160
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"At 0% ",
					UI.PRE_KEYWORD,
					"Wildness",
					UI.PST_KEYWORD,
					" a critter becomes ",
					UI.PRE_KEYWORD,
					"Tame",
					UI.PST_KEYWORD,
					", increasing its ",
					UI.PRE_KEYWORD,
					"Metabolism",
					UI.PST_KEYWORD,
					" and requiring regular care from Duplicants\n\nDuplicants must possess the ",
					UI.PRE_KEYWORD,
					"Critter Ranching",
					UI.PST_KEYWORD,
					" Skill to care for critters"
				});
			}

			// Token: 0x02002867 RID: 10343
			public class FERTILITY
			{
				// Token: 0x0400AC81 RID: 44161
				public static LocString NAME = "Reproduction";

				// Token: 0x0400AC82 RID: 44162
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"At 100% ",
					UI.PRE_KEYWORD,
					"Reproduction",
					UI.PST_KEYWORD,
					", critters will reach the end of their reproduction cycle and lay a new ",
					UI.PRE_KEYWORD,
					"Egg",
					UI.PST_KEYWORD,
					"\n\nAfter an ",
					UI.PRE_KEYWORD,
					"Egg",
					UI.PST_KEYWORD,
					" is laid, ",
					UI.PRE_KEYWORD,
					"Reproduction",
					UI.PST_KEYWORD,
					" is rolled back to 0%"
				});
			}

			// Token: 0x02002868 RID: 10344
			public class INCUBATION
			{
				// Token: 0x0400AC83 RID: 44163
				public static LocString NAME = "Incubation";

				// Token: 0x0400AC84 RID: 44164
				public static LocString TOOLTIP = "Eggs hatch into brand new " + UI.FormatAsLink("Critters", "CREATURES") + " at the end of their incubation period";
			}

			// Token: 0x02002869 RID: 10345
			public class VIABILITY
			{
				// Token: 0x0400AC85 RID: 44165
				public static LocString NAME = "Viability";

				// Token: 0x0400AC86 RID: 44166
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Eggs will lose ",
					UI.PRE_KEYWORD,
					"Viability",
					UI.PST_KEYWORD,
					" over time when exposed to poor environmental conditions\n\nAt 0% ",
					UI.PRE_KEYWORD,
					"Viability",
					UI.PST_KEYWORD,
					" a critter egg will crack and produce a ",
					ITEMS.FOOD.RAWEGG.NAME,
					" and ",
					ITEMS.INDUSTRIAL_PRODUCTS.EGG_SHELL.NAME
				});
			}

			// Token: 0x0200286A RID: 10346
			public class IRRIGATION
			{
				// Token: 0x0400AC87 RID: 44167
				public static LocString NAME = "Irrigation";

				// Token: 0x0400AC88 RID: 44168
				public static LocString CONSUME_MODIFIER = "Consuming";

				// Token: 0x0400AC89 RID: 44169
				public static LocString ABSORBING_MODIFIER = "Absorbing";
			}

			// Token: 0x0200286B RID: 10347
			public class ILLUMINATION
			{
				// Token: 0x0400AC8A RID: 44170
				public static LocString NAME = "Illumination";
			}

			// Token: 0x0200286C RID: 10348
			public class THERMALCONDUCTIVITYBARRIER
			{
				// Token: 0x0400AC8B RID: 44171
				public static LocString NAME = "Thermal Conductivity Barrier";

				// Token: 0x0400AC8C RID: 44172
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Thick ",
					UI.PRE_KEYWORD,
					"Conductivity Barriers",
					UI.PST_KEYWORD,
					" increase the time it takes an object to heat up or cool down"
				});
			}

			// Token: 0x0200286D RID: 10349
			public class ROT
			{
				// Token: 0x0400AC8D RID: 44173
				public static LocString NAME = "Freshness";

				// Token: 0x0400AC8E RID: 44174
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Food items become stale at fifty percent ",
					UI.PRE_KEYWORD,
					"Freshness",
					UI.PST_KEYWORD,
					", and rot at zero percent"
				});
			}

			// Token: 0x0200286E RID: 10350
			public class SCALEGROWTH
			{
				// Token: 0x0400AC8F RID: 44175
				public static LocString NAME = "Scale Growth";

				// Token: 0x0400AC90 RID: 44176
				public static LocString TOOLTIP = "The amount of time required for this critter to regrow its scales";
			}

			// Token: 0x0200286F RID: 10351
			public class ELEMENTGROWTH
			{
				// Token: 0x0400AC91 RID: 44177
				public static LocString NAME = "Quill Growth";

				// Token: 0x0400AC92 RID: 44178
				public static LocString TOOLTIP = "The amount of time required for this critter to regrow its " + UI.PRE_KEYWORD + "Tonic Root" + UI.PST_KEYWORD;
			}

			// Token: 0x02002870 RID: 10352
			public class AIRPRESSURE
			{
				// Token: 0x0400AC93 RID: 44179
				public static LocString NAME = "Air Pressure";

				// Token: 0x0400AC94 RID: 44180
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The average ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" density of the air surrounding this plant"
				});
			}
		}

		// Token: 0x02001CD6 RID: 7382
		public class ATTRIBUTES
		{
			// Token: 0x02002871 RID: 10353
			public class INCUBATIONDELTA
			{
				// Token: 0x0400AC95 RID: 44181
				public static LocString NAME = "Incubation Rate";

				// Token: 0x0400AC96 RID: 44182
				public static LocString DESC = "";
			}

			// Token: 0x02002872 RID: 10354
			public class POWERCHARGEDELTA
			{
				// Token: 0x0400AC97 RID: 44183
				public static LocString NAME = "Power Charge Loss Rate";

				// Token: 0x0400AC98 RID: 44184
				public static LocString DESC = "";
			}

			// Token: 0x02002873 RID: 10355
			public class VIABILITYDELTA
			{
				// Token: 0x0400AC99 RID: 44185
				public static LocString NAME = "Viability Loss Rate";

				// Token: 0x0400AC9A RID: 44186
				public static LocString DESC = "";
			}

			// Token: 0x02002874 RID: 10356
			public class SCALEGROWTHDELTA
			{
				// Token: 0x0400AC9B RID: 44187
				public static LocString NAME = "Scale Growth";

				// Token: 0x0400AC9C RID: 44188
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Determines how long this ",
					UI.PRE_KEYWORD,
					"Critter's",
					UI.PST_KEYWORD,
					" scales will take to grow back."
				});
			}

			// Token: 0x02002875 RID: 10357
			public class WILDNESSDELTA
			{
				// Token: 0x0400AC9D RID: 44189
				public static LocString NAME = "Wildness";

				// Token: 0x0400AC9E RID: 44190
				public static LocString DESC = string.Concat(new string[]
				{
					"Wild creatures can survive on fewer ",
					UI.PRE_KEYWORD,
					"Calories",
					UI.PST_KEYWORD,
					" than domesticated ones."
				});
			}

			// Token: 0x02002876 RID: 10358
			public class FERTILITYDELTA
			{
				// Token: 0x0400AC9F RID: 44191
				public static LocString NAME = "Reproduction Rate";

				// Token: 0x0400ACA0 RID: 44192
				public static LocString DESC = string.Concat(new string[]
				{
					"Determines the amount of time needed for a ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					" to lay new ",
					UI.PRE_KEYWORD,
					"Eggs",
					UI.PST_KEYWORD,
					"."
				});
			}

			// Token: 0x02002877 RID: 10359
			public class MATURITYDELTA
			{
				// Token: 0x0400ACA1 RID: 44193
				public static LocString NAME = "Growth Speed";

				// Token: 0x0400ACA2 RID: 44194
				public static LocString DESC = "Determines the amount of time needed to reach maturation.";
			}

			// Token: 0x02002878 RID: 10360
			public class MATURITYMAX
			{
				// Token: 0x0400ACA3 RID: 44195
				public static LocString NAME = "Life Cycle";

				// Token: 0x0400ACA4 RID: 44196
				public static LocString DESC = "The amount of time it takes this plant to grow.";
			}

			// Token: 0x02002879 RID: 10361
			public class ROTDELTA
			{
				// Token: 0x0400ACA5 RID: 44197
				public static LocString NAME = "Freshness";

				// Token: 0x0400ACA6 RID: 44198
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Food items become stale at fifty percent ",
					UI.PRE_KEYWORD,
					"Freshness",
					UI.PST_KEYWORD,
					", and rot at zero percent"
				});
			}
		}

		// Token: 0x02001CD7 RID: 7383
		public class MODIFIERS
		{
			// Token: 0x0200287A RID: 10362
			public class DOMESTICATION_INCREASING
			{
				// Token: 0x0400ACA7 RID: 44199
				public static LocString NAME = "Happiness Increasing";

				// Token: 0x0400ACA8 RID: 44200
				public static LocString TOOLTIP = "This critter is very happy its needs are being met";
			}

			// Token: 0x0200287B RID: 10363
			public class DOMESTICATION_DECREASING
			{
				// Token: 0x0400ACA9 RID: 44201
				public static LocString NAME = "Happiness Decreasing";

				// Token: 0x0400ACAA RID: 44202
				public static LocString TOOLTIP = "Unfavorable conditions are making this critter unhappy";
			}

			// Token: 0x0200287C RID: 10364
			public class BASE_FERTILITY
			{
				// Token: 0x0400ACAB RID: 44203
				public static LocString NAME = "Base Reproduction";

				// Token: 0x0400ACAC RID: 44204
				public static LocString TOOLTIP = "This is the base speed with which critters produce new " + UI.PRE_KEYWORD + "Eggs" + UI.PST_KEYWORD;
			}

			// Token: 0x0200287D RID: 10365
			public class BASE_INCUBATION_RATE
			{
				// Token: 0x0400ACAD RID: 44205
				public static LocString NAME = "Base Incubation Rate";
			}

			// Token: 0x0200287E RID: 10366
			public class SCALE_GROWTH_RATE
			{
				// Token: 0x0400ACAE RID: 44206
				public static LocString NAME = "Scale Regrowth Rate";
			}

			// Token: 0x0200287F RID: 10367
			public class ELEMENT_GROWTH_RATE
			{
				// Token: 0x0400ACAF RID: 44207
				public static LocString NAME = "Quill Regrowth Rate";
			}

			// Token: 0x02002880 RID: 10368
			public class INCUBATOR_SONG
			{
				// Token: 0x0400ACB0 RID: 44208
				public static LocString NAME = "Lullabied";

				// Token: 0x0400ACB1 RID: 44209
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This egg was recently sung to by a kind Duplicant\n\nIncreased ",
					UI.PRE_KEYWORD,
					"Incubation Rate",
					UI.PST_KEYWORD,
					"\n\nDuplicants must possess the ",
					UI.PRE_KEYWORD,
					"Critter Ranching",
					UI.PST_KEYWORD,
					" Skill to sing to eggs"
				});
			}

			// Token: 0x02002881 RID: 10369
			public class EGGHUG
			{
				// Token: 0x0400ACB2 RID: 44210
				public static LocString NAME = "Cuddled";

				// Token: 0x0400ACB3 RID: 44211
				public static LocString TOOLTIP = "This egg was recently hugged by an affectionate critter\n\nIncreased " + UI.PRE_KEYWORD + "Incubation Rate" + UI.PST_KEYWORD;
			}

			// Token: 0x02002882 RID: 10370
			public class HUGGINGFRENZY
			{
				// Token: 0x0400ACB4 RID: 44212
				public static LocString NAME = "Hugging Spree";

				// Token: 0x0400ACB5 RID: 44213
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter was recently hugged by a Duplicant and is feeling extra affectionate\n\nWhile in this state, it hugs ",
					UI.PRE_KEYWORD,
					"Eggs",
					UI.PST_KEYWORD,
					" more frequently"
				});
			}

			// Token: 0x02002883 RID: 10371
			public class INCUBATING
			{
				// Token: 0x0400ACB6 RID: 44214
				public static LocString NAME = "Incubating";

				// Token: 0x0400ACB7 RID: 44215
				public static LocString TOOLTIP = "This egg is happily incubating";
			}

			// Token: 0x02002884 RID: 10372
			public class INCUBATING_SUPPRESSED
			{
				// Token: 0x0400ACB8 RID: 44216
				public static LocString NAME = "Growth Suppressed";

				// Token: 0x0400ACB9 RID: 44217
				public static LocString TOOLTIP = "Environmental conditions are preventing this egg from developing\n\nIt will not hatch if current conditions continue";
			}

			// Token: 0x02002885 RID: 10373
			public class RANCHED
			{
				// Token: 0x0400ACBA RID: 44218
				public static LocString NAME = "Groomed";

				// Token: 0x0400ACBB RID: 44219
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter has recently been attended to by a kind Duplicant\n\nDuplicants must possess the ",
					UI.PRE_KEYWORD,
					"Critter Ranching",
					UI.PST_KEYWORD,
					" Skill to care for critters"
				});
			}

			// Token: 0x02002886 RID: 10374
			public class HAPPY
			{
				// Token: 0x0400ACBC RID: 44220
				public static LocString NAME = "Happy";

				// Token: 0x0400ACBD RID: 44221
				public static LocString TOOLTIP = "This critter's in high spirits because all of its needs are being met\n\nIt will produce more materials as a result";
			}

			// Token: 0x02002887 RID: 10375
			public class UNHAPPY
			{
				// Token: 0x0400ACBE RID: 44222
				public static LocString NAME = "Glum";

				// Token: 0x0400ACBF RID: 44223
				public static LocString TOOLTIP = "This critter's feeling down because its needs aren't being met\n\nIt will produce less materials as a result";
			}

			// Token: 0x02002888 RID: 10376
			public class ATE_FROM_FEEDER
			{
				// Token: 0x0400ACC0 RID: 44224
				public static LocString NAME = "Ate From Feeder";

				// Token: 0x0400ACC1 RID: 44225
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter is getting more ",
					UI.PRE_KEYWORD,
					"Tame",
					UI.PST_KEYWORD,
					" because it ate from a feeder."
				});
			}

			// Token: 0x02002889 RID: 10377
			public class WILD
			{
				// Token: 0x0400ACC2 RID: 44226
				public static LocString NAME = "Wild";

				// Token: 0x0400ACC3 RID: 44227
				public static LocString TOOLTIP = "This critter is wild";
			}

			// Token: 0x0200288A RID: 10378
			public class AGE
			{
				// Token: 0x0400ACC4 RID: 44228
				public static LocString NAME = "Aging";

				// Token: 0x0400ACC5 RID: 44229
				public static LocString TOOLTIP = "Time takes its toll on all things";
			}

			// Token: 0x0200288B RID: 10379
			public class BABY
			{
				// Token: 0x0400ACC6 RID: 44230
				public static LocString NAME = "Tiny Baby!";

				// Token: 0x0400ACC7 RID: 44231
				public static LocString TOOLTIP = "This critter will grow into an adult as it ages and becomes wise to the ways of the world";
			}

			// Token: 0x0200288C RID: 10380
			public class TAME
			{
				// Token: 0x0400ACC8 RID: 44232
				public static LocString NAME = "Tame";

				// Token: 0x0400ACC9 RID: 44233
				public static LocString TOOLTIP = "This critter is " + UI.PRE_KEYWORD + "Tame" + UI.PST_KEYWORD;
			}

			// Token: 0x0200288D RID: 10381
			public class OUT_OF_CALORIES
			{
				// Token: 0x0400ACCA RID: 44234
				public static LocString NAME = "Starving";

				// Token: 0x0400ACCB RID: 44235
				public static LocString TOOLTIP = "Get this critter something to eat!";
			}

			// Token: 0x0200288E RID: 10382
			public class FUTURE_OVERCROWDED
			{
				// Token: 0x0400ACCC RID: 44236
				public static LocString NAME = "Cramped";

				// Token: 0x0400ACCD RID: 44237
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					" will become overcrowded once all nearby ",
					UI.PRE_KEYWORD,
					"Eggs",
					UI.PST_KEYWORD,
					" hatch\n\nThe selected critter has slowed its ",
					UI.PRE_KEYWORD,
					"Reproduction",
					UI.PST_KEYWORD,
					" to prevent further overpopulation"
				});
			}

			// Token: 0x0200288F RID: 10383
			public class OVERCROWDED
			{
				// Token: 0x0400ACCE RID: 44238
				public static LocString NAME = "Overcrowded";

				// Token: 0x0400ACCF RID: 44239
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This critter isn't comfortable with so many other critters in a ",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					" of this size"
				});

				// Token: 0x0400ACD0 RID: 44240
				public static LocString FISHTOOLTIP = "This critter is uncomfortable with either the size of this pool, or the number of other critters sharing it";
			}

			// Token: 0x02002890 RID: 10384
			public class CONFINED
			{
				// Token: 0x0400ACD1 RID: 44241
				public static LocString NAME = "Confined";

				// Token: 0x0400ACD2 RID: 44242
				public static LocString TOOLTIP = "This critter is trapped inside a door, tile, or confined space\n\nSounds uncomfortable!";
			}

			// Token: 0x02002891 RID: 10385
			public class DIVERGENTPLANTTENDED
			{
				// Token: 0x0400ACD3 RID: 44243
				public static LocString NAME = "Sweetle Tending";

				// Token: 0x0400ACD4 RID: 44244
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A ",
					CREATURES.SPECIES.DIVERGENT.VARIANT_BEETLE.NAME,
					" rubbed against this ",
					UI.PRE_KEYWORD,
					"Plant",
					UI.PST_KEYWORD,
					" for a tiny growth boost"
				});
			}

			// Token: 0x02002892 RID: 10386
			public class DIVERGENTPLANTTENDEDWORM
			{
				// Token: 0x0400ACD5 RID: 44245
				public static LocString NAME = "Grubgrub Rub";

				// Token: 0x0400ACD6 RID: 44246
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A ",
					CREATURES.SPECIES.DIVERGENT.VARIANT_WORM.NAME,
					" rubbed against this ",
					UI.PRE_KEYWORD,
					"Plant",
					UI.PST_KEYWORD,
					", dramatically boosting growth"
				});
			}
		}

		// Token: 0x02001CD8 RID: 7384
		public class FERTILITY_MODIFIERS
		{
			// Token: 0x02002893 RID: 10387
			public class DIET
			{
				// Token: 0x0400ACD7 RID: 44247
				public static LocString NAME = "Diet";

				// Token: 0x0400ACD8 RID: 44248
				public static LocString DESC = "Eats: {0}";
			}

			// Token: 0x02002894 RID: 10388
			public class NEARBY_CREATURE
			{
				// Token: 0x0400ACD9 RID: 44249
				public static LocString NAME = "Nearby Critters";

				// Token: 0x0400ACDA RID: 44250
				public static LocString DESC = "Penned with: {0}";
			}

			// Token: 0x02002895 RID: 10389
			public class NEARBY_CREATURE_NEG
			{
				// Token: 0x0400ACDB RID: 44251
				public static LocString NAME = "No Nearby Critters";

				// Token: 0x0400ACDC RID: 44252
				public static LocString DESC = "Not penned with: {0}";
			}

			// Token: 0x02002896 RID: 10390
			public class TEMPERATURE
			{
				// Token: 0x0400ACDD RID: 44253
				public static LocString NAME = "Temperature";

				// Token: 0x0400ACDE RID: 44254
				public static LocString DESC = "Body temperature: Between {0} and {1}";
			}

			// Token: 0x02002897 RID: 10391
			public class CROPTENDING
			{
				// Token: 0x0400ACDF RID: 44255
				public static LocString NAME = "Crop Tending";

				// Token: 0x0400ACE0 RID: 44256
				public static LocString DESC = "Tends to: {0}";
			}

			// Token: 0x02002898 RID: 10392
			public class LIVING_IN_ELEMENT
			{
				// Token: 0x0400ACE1 RID: 44257
				public static LocString NAME = "Habitat";

				// Token: 0x0400ACE2 RID: 44258
				public static LocString DESC = "Dwells in {0}";

				// Token: 0x0400ACE3 RID: 44259
				public static LocString UNBREATHABLE = "Dwells in unbreathable" + UI.FormatAsLink("Gas", "UNBREATHABLE");

				// Token: 0x0400ACE4 RID: 44260
				public static LocString LIQUID = "Dwells in " + UI.FormatAsLink("Liquid", "LIQUID");
			}
		}
	}
}
