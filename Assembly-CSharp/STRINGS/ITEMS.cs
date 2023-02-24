using System;

namespace STRINGS
{
	// Token: 0x02000D45 RID: 3397
	public class ITEMS
	{
		// Token: 0x02001CEE RID: 7406
		public class PILLS
		{
			// Token: 0x02002C78 RID: 11384
			public class PLACEBO
			{
				// Token: 0x0400B671 RID: 46705
				public static LocString NAME = "Placebo";

				// Token: 0x0400B672 RID: 46706
				public static LocString DESC = "A general, all-purpose " + UI.FormatAsLink("Medicine", "MEDICINE") + ".\n\nThe less one knows about it, the better it works.";

				// Token: 0x0400B673 RID: 46707
				public static LocString RECIPEDESC = "All-purpose " + UI.FormatAsLink("Medicine", "MEDICINE") + ".";
			}

			// Token: 0x02002C79 RID: 11385
			public class BASICBOOSTER
			{
				// Token: 0x0400B674 RID: 46708
				public static LocString NAME = "Vitamin Chews";

				// Token: 0x0400B675 RID: 46709
				public static LocString DESC = "Minorly reduces the chance of becoming sick.";

				// Token: 0x0400B676 RID: 46710
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"A supplement that minorly reduces the chance of contracting a ",
					UI.PRE_KEYWORD,
					"Germ",
					UI.PST_KEYWORD,
					"-based ",
					UI.FormatAsLink("Disease", "DISEASE"),
					".\n\nMust be taken daily."
				});
			}

			// Token: 0x02002C7A RID: 11386
			public class INTERMEDIATEBOOSTER
			{
				// Token: 0x0400B677 RID: 46711
				public static LocString NAME = "Immuno Booster";

				// Token: 0x0400B678 RID: 46712
				public static LocString DESC = "Significantly reduces the chance of becoming sick.";

				// Token: 0x0400B679 RID: 46713
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"A supplement that significantly reduces the chance of contracting a ",
					UI.PRE_KEYWORD,
					"Germ",
					UI.PST_KEYWORD,
					"-based ",
					UI.FormatAsLink("Disease", "DISEASE"),
					".\n\nMust be taken daily."
				});
			}

			// Token: 0x02002C7B RID: 11387
			public class ANTIHISTAMINE
			{
				// Token: 0x0400B67A RID: 46714
				public static LocString NAME = "Allergy Medication";

				// Token: 0x0400B67B RID: 46715
				public static LocString DESC = "Suppresses and prevents allergic reactions.";

				// Token: 0x0400B67C RID: 46716
				public static LocString RECIPEDESC = "A strong antihistamine Duplicants can take to halt an allergic reaction. " + ITEMS.PILLS.ANTIHISTAMINE.NAME + " will also prevent further reactions from occurring for a short time after ingestion.";
			}

			// Token: 0x02002C7C RID: 11388
			public class BASICCURE
			{
				// Token: 0x0400B67D RID: 46717
				public static LocString NAME = "Curative Tablet";

				// Token: 0x0400B67E RID: 46718
				public static LocString DESC = "A simple, easy-to-take remedy for minor germ-based diseases.";

				// Token: 0x0400B67F RID: 46719
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"Duplicants can take this to cure themselves of minor ",
					UI.PRE_KEYWORD,
					"Germ",
					UI.PST_KEYWORD,
					"-based ",
					UI.FormatAsLink("Diseases", "DISEASE"),
					".\n\nCurative Tablets are very effective against ",
					UI.FormatAsLink("Food Poisoning", "FOODSICKNESS"),
					"."
				});
			}

			// Token: 0x02002C7D RID: 11389
			public class INTERMEDIATECURE
			{
				// Token: 0x0400B680 RID: 46720
				public static LocString NAME = "Medical Pack";

				// Token: 0x0400B681 RID: 46721
				public static LocString DESC = "A doctor-administered cure for moderate ailments.";

				// Token: 0x0400B682 RID: 46722
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"A doctor-administered cure for moderate ",
					UI.FormatAsLink("Diseases", "DISEASE"),
					". ",
					ITEMS.PILLS.INTERMEDIATECURE.NAME,
					"s are very effective against ",
					UI.FormatAsLink("Slimelung", "SLIMESICKNESS"),
					".\n\nMust be administered by a Duplicant with the ",
					DUPLICANTS.ROLES.MEDIC.NAME,
					" Skill."
				});
			}

			// Token: 0x02002C7E RID: 11390
			public class ADVANCEDCURE
			{
				// Token: 0x0400B683 RID: 46723
				public static LocString NAME = "Serum Vial";

				// Token: 0x0400B684 RID: 46724
				public static LocString DESC = "A doctor-administered cure for severe ailments.";

				// Token: 0x0400B685 RID: 46725
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"An extremely powerful medication created to treat severe ",
					UI.FormatAsLink("Diseases", "DISEASE"),
					". ",
					ITEMS.PILLS.ADVANCEDCURE.NAME,
					" is very effective against ",
					UI.FormatAsLink("Zombie Spores", "ZOMBIESPORES"),
					".\n\nMust be administered by a Duplicant with the ",
					DUPLICANTS.ROLES.SENIOR_MEDIC.NAME,
					" Skill."
				});
			}

			// Token: 0x02002C7F RID: 11391
			public class BASICRADPILL
			{
				// Token: 0x0400B686 RID: 46726
				public static LocString NAME = "Basic Rad Pill";

				// Token: 0x0400B687 RID: 46727
				public static LocString DESC = "Increases a Duplicant's natural radiation absorption rate.";

				// Token: 0x0400B688 RID: 46728
				public static LocString RECIPEDESC = "A supplement that speeds up the rate at which a Duplicant body absorbs radiation, allowing them to manage increased radiation exposure.\n\nMust be taken daily.";
			}

			// Token: 0x02002C80 RID: 11392
			public class INTERMEDIATERADPILL
			{
				// Token: 0x0400B689 RID: 46729
				public static LocString NAME = "Intermediate Rad Pill";

				// Token: 0x0400B68A RID: 46730
				public static LocString DESC = "Increases a Duplicant's natural radiation absorption rate.";

				// Token: 0x0400B68B RID: 46731
				public static LocString RECIPEDESC = "A supplement that speeds up the rate at which a Duplicant body absorbs radiation, allowing them to manage increased radiation exposure.\n\nMust be taken daily.";
			}
		}

		// Token: 0x02001CEF RID: 7407
		public class FOOD
		{
			// Token: 0x04008451 RID: 33873
			public static LocString COMPOST = "Compost";

			// Token: 0x02002C81 RID: 11393
			public class FOODSPLAT
			{
				// Token: 0x0400B68C RID: 46732
				public static LocString NAME = "Food Splatter";

				// Token: 0x0400B68D RID: 46733
				public static LocString DESC = "Food smeared on the wall from a recent Food Fight";
			}

			// Token: 0x02002C82 RID: 11394
			public class BURGER
			{
				// Token: 0x0400B68E RID: 46734
				public static LocString NAME = UI.FormatAsLink("Frost Burger", "BURGER");

				// Token: 0x0400B68F RID: 46735
				public static LocString DESC = string.Concat(new string[]
				{
					UI.FormatAsLink("Meat", "MEAT"),
					" and ",
					UI.FormatAsLink("Lettuce", "LETTUCE"),
					" on a chilled ",
					UI.FormatAsLink("Frost Bun", "COLDWHEATBREAD"),
					".\n\nIt's the only burger best served cold."
				});

				// Token: 0x0400B690 RID: 46736
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					UI.FormatAsLink("Meat", "MEAT"),
					" and ",
					UI.FormatAsLink("Lettuce", "LETTUCE"),
					" on a chilled ",
					UI.FormatAsLink("Frost Bun", "COLDWHEATBREAD"),
					"."
				});
			}

			// Token: 0x02002C83 RID: 11395
			public class FIELDRATION
			{
				// Token: 0x0400B691 RID: 46737
				public static LocString NAME = UI.FormatAsLink("Nutrient Bar", "FIELDRATION");

				// Token: 0x0400B692 RID: 46738
				public static LocString DESC = "A nourishing nutrient paste, sandwiched between thin wafer layers.";
			}

			// Token: 0x02002C84 RID: 11396
			public class MUSHBAR
			{
				// Token: 0x0400B693 RID: 46739
				public static LocString NAME = UI.FormatAsLink("Mush Bar", "MUSHBAR");

				// Token: 0x0400B694 RID: 46740
				public static LocString DESC = "An edible, putrefied mudslop.\n\nMush Bars are preferable to starvation, but only just barely.";

				// Token: 0x0400B695 RID: 46741
				public static LocString RECIPEDESC = "An edible, putrefied mudslop.\n\n" + ITEMS.FOOD.MUSHBAR.NAME + "s are preferable to starvation, but only just barely.";
			}

			// Token: 0x02002C85 RID: 11397
			public class MUSHROOMWRAP
			{
				// Token: 0x0400B696 RID: 46742
				public static LocString NAME = UI.FormatAsLink("Mushroom Wrap", "MUSHROOMWRAP");

				// Token: 0x0400B697 RID: 46743
				public static LocString DESC = string.Concat(new string[]
				{
					"Flavorful ",
					UI.FormatAsLink("Mushrooms", "MUSHROOM"),
					" wrapped in ",
					UI.FormatAsLink("Lettuce", "LETTUCE"),
					".\n\nIt has an earthy flavor punctuated by a refreshing crunch."
				});

				// Token: 0x0400B698 RID: 46744
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"Flavorful ",
					UI.FormatAsLink("Mushrooms", "MUSHROOM"),
					" wrapped in ",
					UI.FormatAsLink("Lettuce", "LETTUCE"),
					"."
				});
			}

			// Token: 0x02002C86 RID: 11398
			public class MICROWAVEDLETTUCE
			{
				// Token: 0x0400B699 RID: 46745
				public static LocString NAME = UI.FormatAsLink("Microwaved Lettuce", "MICROWAVEDLETTUCE");

				// Token: 0x0400B69A RID: 46746
				public static LocString DESC = UI.FormatAsLink("Lettuce", "LETTUCE") + " scrumptiously wilted in the " + BUILDINGS.PREFABS.GAMMARAYOVEN.NAME + ".";

				// Token: 0x0400B69B RID: 46747
				public static LocString RECIPEDESC = UI.FormatAsLink("Lettuce", "LETTUCE") + " scrumptiously wilted in the " + BUILDINGS.PREFABS.GAMMARAYOVEN.NAME + ".";
			}

			// Token: 0x02002C87 RID: 11399
			public class GAMMAMUSH
			{
				// Token: 0x0400B69C RID: 46748
				public static LocString NAME = UI.FormatAsLink("Gamma Mush", "GAMMAMUSH");

				// Token: 0x0400B69D RID: 46749
				public static LocString DESC = "A disturbingly delicious mixture of irradiated dirt and water.";

				// Token: 0x0400B69E RID: 46750
				public static LocString RECIPEDESC = UI.FormatAsLink("Mush Fry", "FRIEDMUSHBAR") + " reheated in a " + BUILDINGS.PREFABS.GAMMARAYOVEN.NAME + ".";
			}

			// Token: 0x02002C88 RID: 11400
			public class FRUITCAKE
			{
				// Token: 0x0400B69F RID: 46751
				public static LocString NAME = UI.FormatAsLink("Berry Sludge", "FRUITCAKE");

				// Token: 0x0400B6A0 RID: 46752
				public static LocString DESC = "A mashed up " + UI.FormatAsLink("Bristle Berry", "PRICKLEFRUIT") + " sludge with an exceptionally long shelf life.\n\nIts aggressive, overbearing sweetness can leave the tongue feeling temporarily numb.";

				// Token: 0x0400B6A1 RID: 46753
				public static LocString RECIPEDESC = "A mashed up " + UI.FormatAsLink("Bristle Berry", "PRICKLEFRUIT") + " sludge with an exceptionally long shelf life.";
			}

			// Token: 0x02002C89 RID: 11401
			public class POPCORN
			{
				// Token: 0x0400B6A2 RID: 46754
				public static LocString NAME = UI.FormatAsLink("Popcorn", "POPCORN");

				// Token: 0x0400B6A3 RID: 46755
				public static LocString DESC = UI.FormatAsLink("Sleet Wheat Grain", "COLDWHEATSEED") + " popped in a " + BUILDINGS.PREFABS.GAMMARAYOVEN.NAME + ".\n\nCompletely devoid of any fancy flavorings.";

				// Token: 0x0400B6A4 RID: 46756
				public static LocString RECIPEDESC = "Gamma-radiated " + UI.FormatAsLink("Sleet Wheat Grain", "COLDWHEATSEED") + ".";
			}

			// Token: 0x02002C8A RID: 11402
			public class SUSHI
			{
				// Token: 0x0400B6A5 RID: 46757
				public static LocString NAME = UI.FormatAsLink("Sushi", "SUSHI");

				// Token: 0x0400B6A6 RID: 46758
				public static LocString DESC = string.Concat(new string[]
				{
					"Raw ",
					UI.FormatAsLink("Pacu Fillet", "FISHMEAT"),
					" wrapped with fresh ",
					UI.FormatAsLink("Lettuce", "LETTUCE"),
					".\n\nWhile the salt of the lettuce may initially overpower the flavor, a keen palate can discern the subtle sweetness of the fillet beneath."
				});

				// Token: 0x0400B6A7 RID: 46759
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"Raw ",
					UI.FormatAsLink("Pacu Fillet", "FISHMEAT"),
					" wrapped with fresh ",
					UI.FormatAsLink("Lettuce", "LETTUCE"),
					"."
				});
			}

			// Token: 0x02002C8B RID: 11403
			public class HATCHEGG
			{
				// Token: 0x0400B6A8 RID: 46760
				public static LocString NAME = CREATURES.SPECIES.HATCH.EGG_NAME;

				// Token: 0x0400B6A9 RID: 46761
				public static LocString DESC = string.Concat(new string[]
				{
					"An egg laid by a ",
					UI.FormatAsLink("Hatch", "HATCH"),
					".\n\nIf incubated, it will hatch into a ",
					UI.FormatAsLink("Hatchling", "HATCH"),
					"."
				});

				// Token: 0x0400B6AA RID: 46762
				public static LocString RECIPEDESC = "An egg laid by a " + UI.FormatAsLink("Hatch", "HATCH") + ".";
			}

			// Token: 0x02002C8C RID: 11404
			public class DRECKOEGG
			{
				// Token: 0x0400B6AB RID: 46763
				public static LocString NAME = CREATURES.SPECIES.DRECKO.EGG_NAME;

				// Token: 0x0400B6AC RID: 46764
				public static LocString DESC = string.Concat(new string[]
				{
					"An egg laid by a ",
					UI.FormatAsLink("Drecko", "DRECKO"),
					".\n\nIf incubated, it will hatch into a new ",
					UI.FormatAsLink("Drecklet", "DRECKO"),
					"."
				});

				// Token: 0x0400B6AD RID: 46765
				public static LocString RECIPEDESC = "An egg laid by a " + UI.FormatAsLink("Drecko", "DRECKO") + ".";
			}

			// Token: 0x02002C8D RID: 11405
			public class LIGHTBUGEGG
			{
				// Token: 0x0400B6AE RID: 46766
				public static LocString NAME = CREATURES.SPECIES.LIGHTBUG.EGG_NAME;

				// Token: 0x0400B6AF RID: 46767
				public static LocString DESC = string.Concat(new string[]
				{
					"An egg laid by a ",
					UI.FormatAsLink("Shine Bug", "LIGHTBUG"),
					".\n\nIf incubated, it will hatch into a ",
					UI.FormatAsLink("Shine Nymph", "LIGHTBUG"),
					"."
				});

				// Token: 0x0400B6B0 RID: 46768
				public static LocString RECIPEDESC = "An egg laid by a " + UI.FormatAsLink("Shine Bug", "LIGHTBUG") + ".";
			}

			// Token: 0x02002C8E RID: 11406
			public class LETTUCE
			{
				// Token: 0x0400B6B1 RID: 46769
				public static LocString NAME = UI.FormatAsLink("Lettuce", "LETTUCE");

				// Token: 0x0400B6B2 RID: 46770
				public static LocString DESC = "Crunchy, slightly salty leaves from a " + UI.FormatAsLink("Waterweed", "SEALETTUCE") + " plant.";

				// Token: 0x0400B6B3 RID: 46771
				public static LocString RECIPEDESC = "Edible roughage from a " + UI.FormatAsLink("Waterweed", "SEALETTUCE") + ".";
			}

			// Token: 0x02002C8F RID: 11407
			public class OILFLOATEREGG
			{
				// Token: 0x0400B6B4 RID: 46772
				public static LocString NAME = CREATURES.SPECIES.OILFLOATER.EGG_NAME;

				// Token: 0x0400B6B5 RID: 46773
				public static LocString DESC = string.Concat(new string[]
				{
					"An egg laid by a ",
					UI.FormatAsLink("Slickster", "OILFLOATER"),
					".\n\nIf incubated, it will hatch into a ",
					UI.FormatAsLink("Slickster Larva", "OILFLOATER"),
					"."
				});

				// Token: 0x0400B6B6 RID: 46774
				public static LocString RECIPEDESC = "An egg laid by a " + UI.FormatAsLink("Slickster", "OILFLOATER") + ".";
			}

			// Token: 0x02002C90 RID: 11408
			public class PUFTEGG
			{
				// Token: 0x0400B6B7 RID: 46775
				public static LocString NAME = CREATURES.SPECIES.PUFT.EGG_NAME;

				// Token: 0x0400B6B8 RID: 46776
				public static LocString DESC = string.Concat(new string[]
				{
					"An egg laid by a ",
					UI.FormatAsLink("Puft", "PUFT"),
					".\n\nIf incubated, it will hatch into a ",
					UI.FormatAsLink("Puftlet", "PUFT"),
					"."
				});

				// Token: 0x0400B6B9 RID: 46777
				public static LocString RECIPEDESC = "An egg laid by a " + CREATURES.SPECIES.PUFT.NAME + ".";
			}

			// Token: 0x02002C91 RID: 11409
			public class FISHMEAT
			{
				// Token: 0x0400B6BA RID: 46778
				public static LocString NAME = UI.FormatAsLink("Pacu Fillet", "FISHMEAT");

				// Token: 0x0400B6BB RID: 46779
				public static LocString DESC = "An uncooked fillet from a very dead " + CREATURES.SPECIES.PACU.NAME + ". Yum!";
			}

			// Token: 0x02002C92 RID: 11410
			public class MEAT
			{
				// Token: 0x0400B6BC RID: 46780
				public static LocString NAME = UI.FormatAsLink("Meat", "MEAT");

				// Token: 0x0400B6BD RID: 46781
				public static LocString DESC = "Uncooked meat from a very dead critter. Yum!";
			}

			// Token: 0x02002C93 RID: 11411
			public class PLANTMEAT
			{
				// Token: 0x0400B6BE RID: 46782
				public static LocString NAME = UI.FormatAsLink("Plant Meat", "PLANTMEAT");

				// Token: 0x0400B6BF RID: 46783
				public static LocString DESC = "Planty plant meat from a plant. How nice!";
			}

			// Token: 0x02002C94 RID: 11412
			public class SHELLFISHMEAT
			{
				// Token: 0x0400B6C0 RID: 46784
				public static LocString NAME = UI.FormatAsLink("Raw Shellfish", "SHELLFISHMEAT");

				// Token: 0x0400B6C1 RID: 46785
				public static LocString DESC = "An uncooked chunk of very dead " + CREATURES.SPECIES.CRAB.VARIANT_FRESH_WATER.NAME + ". Yum!";
			}

			// Token: 0x02002C95 RID: 11413
			public class MUSHROOM
			{
				// Token: 0x0400B6C2 RID: 46786
				public static LocString NAME = UI.FormatAsLink("Mushroom", "MUSHROOM");

				// Token: 0x0400B6C3 RID: 46787
				public static LocString DESC = "An edible, flavorless fungus that grew in the dark.";
			}

			// Token: 0x02002C96 RID: 11414
			public class COOKEDFISH
			{
				// Token: 0x0400B6C4 RID: 46788
				public static LocString NAME = UI.FormatAsLink("Cooked Seafood", "COOKEDFISH");

				// Token: 0x0400B6C5 RID: 46789
				public static LocString DESC = "A cooked piece of freshly caught aquatic critter.\n\nUnsurprisingly, it tastes a bit fishy.";

				// Token: 0x0400B6C6 RID: 46790
				public static LocString RECIPEDESC = "A cooked piece of freshly caught aquatic critter.";
			}

			// Token: 0x02002C97 RID: 11415
			public class COOKEDMEAT
			{
				// Token: 0x0400B6C7 RID: 46791
				public static LocString NAME = UI.FormatAsLink("Barbeque", "COOKEDMEAT");

				// Token: 0x0400B6C8 RID: 46792
				public static LocString DESC = "The cooked meat of a defeated critter.\n\nIt has a delightful smoky aftertaste.";

				// Token: 0x0400B6C9 RID: 46793
				public static LocString RECIPEDESC = "The cooked meat of a defeated critter.";
			}

			// Token: 0x02002C98 RID: 11416
			public class PICKLEDMEAL
			{
				// Token: 0x0400B6CA RID: 46794
				public static LocString NAME = UI.FormatAsLink("Pickled Meal", "PICKLEDMEAL");

				// Token: 0x0400B6CB RID: 46795
				public static LocString DESC = "Meal Lice preserved in vinegar.\n\nIt's a rarely acquired taste.";

				// Token: 0x0400B6CC RID: 46796
				public static LocString RECIPEDESC = ITEMS.FOOD.BASICPLANTFOOD.NAME + " regrettably preserved in vinegar.";
			}

			// Token: 0x02002C99 RID: 11417
			public class FRIEDMUSHBAR
			{
				// Token: 0x0400B6CD RID: 46797
				public static LocString NAME = UI.FormatAsLink("Mush Fry", "FRIEDMUSHBAR");

				// Token: 0x0400B6CE RID: 46798
				public static LocString DESC = "Deep fried, solidified mudslop.\n\nThe inside is almost completely uncooked, despite the crunch on the outside.";

				// Token: 0x0400B6CF RID: 46799
				public static LocString RECIPEDESC = "Deep fried, solidified mudslop.";
			}

			// Token: 0x02002C9A RID: 11418
			public class RAWEGG
			{
				// Token: 0x0400B6D0 RID: 46800
				public static LocString NAME = UI.FormatAsLink("Raw Egg", "RAWEGG");

				// Token: 0x0400B6D1 RID: 46801
				public static LocString DESC = "A raw Egg that has been cracked open for use in " + UI.FormatAsLink("Food", "FOOD") + " preparation.\n\nIt will never hatch.";

				// Token: 0x0400B6D2 RID: 46802
				public static LocString RECIPEDESC = "A raw egg that has been cracked open for use in " + UI.FormatAsLink("Food", "FOOD") + " preparation.";
			}

			// Token: 0x02002C9B RID: 11419
			public class COOKEDEGG
			{
				// Token: 0x0400B6D3 RID: 46803
				public static LocString NAME = UI.FormatAsLink("Omelette", "COOKEDEGG");

				// Token: 0x0400B6D4 RID: 46804
				public static LocString DESC = "Fluffed and folded Egg innards.\n\nIt turns out you do, in fact, have to break a few eggs to make it.";

				// Token: 0x0400B6D5 RID: 46805
				public static LocString RECIPEDESC = "Fluffed and folded egg innards.";
			}

			// Token: 0x02002C9C RID: 11420
			public class FRIEDMUSHROOM
			{
				// Token: 0x0400B6D6 RID: 46806
				public static LocString NAME = UI.FormatAsLink("Fried Mushroom", "FRIEDMUSHROOM");

				// Token: 0x0400B6D7 RID: 46807
				public static LocString DESC = "A fried dish made with a fruiting " + UI.FormatAsLink("Dusk Cap", "MUSHROOM") + ".\n\nIt has a thick, savory flavor with subtle earthy undertones.";

				// Token: 0x0400B6D8 RID: 46808
				public static LocString RECIPEDESC = "A fried dish made with a fruiting " + UI.FormatAsLink("Dusk Cap", "MUSHROOM") + ".";
			}

			// Token: 0x02002C9D RID: 11421
			public class PRICKLEFRUIT
			{
				// Token: 0x0400B6D9 RID: 46809
				public static LocString NAME = UI.FormatAsLink("Bristle Berry", "PRICKLEFRUIT");

				// Token: 0x0400B6DA RID: 46810
				public static LocString DESC = "A sweet, mostly pleasant-tasting fruit covered in prickly barbs.";
			}

			// Token: 0x02002C9E RID: 11422
			public class GRILLEDPRICKLEFRUIT
			{
				// Token: 0x0400B6DB RID: 46811
				public static LocString NAME = UI.FormatAsLink("Gristle Berry", "GRILLEDPRICKLEFRUIT");

				// Token: 0x0400B6DC RID: 46812
				public static LocString DESC = "The grilled bud of a " + UI.FormatAsLink("Bristle Berry", "PRICKLEFRUIT") + ".\n\nHeat unlocked an exquisite taste in the fruit, though the burnt spines leave something to be desired.";

				// Token: 0x0400B6DD RID: 46813
				public static LocString RECIPEDESC = "The grilled bud of a " + UI.FormatAsLink("Bristle Berry", "PRICKLEFRUIT") + ".";
			}

			// Token: 0x02002C9F RID: 11423
			public class SWAMPFRUIT
			{
				// Token: 0x0400B6DE RID: 46814
				public static LocString NAME = UI.FormatAsLink("Bog Jelly", "SWAMPFRUIT");

				// Token: 0x0400B6DF RID: 46815
				public static LocString DESC = "A fruit with an outer film that contains chewy gelatinous cubes.";
			}

			// Token: 0x02002CA0 RID: 11424
			public class SWAMPDELIGHTS
			{
				// Token: 0x0400B6E0 RID: 46816
				public static LocString NAME = UI.FormatAsLink("Swampy Delights", "SWAMPDELIGHTS");

				// Token: 0x0400B6E1 RID: 46817
				public static LocString DESC = "Dried gelatinous cubes from a " + UI.FormatAsLink("Bog Jelly", "SWAMPFRUIT") + ".\n\nEach cube has a wonderfully chewy texture and is lightly coated in a delicate powder.";

				// Token: 0x0400B6E2 RID: 46818
				public static LocString RECIPEDESC = "Dried gelatinous cubes from a " + UI.FormatAsLink("Bog Jelly", "SWAMPFRUIT") + ".";
			}

			// Token: 0x02002CA1 RID: 11425
			public class WORMBASICFRUIT
			{
				// Token: 0x0400B6E3 RID: 46819
				public static LocString NAME = UI.FormatAsLink("Spindly Grubfruit", "WORMBASICFRUIT");

				// Token: 0x0400B6E4 RID: 46820
				public static LocString DESC = "A " + UI.FormatAsLink("Grubfruit", "WORMSUPERFRUIT") + " that failed to develop properly.\n\nIt is nonetheless edible, and vaguely tasty.";
			}

			// Token: 0x02002CA2 RID: 11426
			public class WORMBASICFOOD
			{
				// Token: 0x0400B6E5 RID: 46821
				public static LocString NAME = UI.FormatAsLink("Roast Grubfruit Nut", "WORMBASICFOOD");

				// Token: 0x0400B6E6 RID: 46822
				public static LocString DESC = "Slow roasted " + UI.FormatAsLink("Spindly Grubfruit", "WORMBASICFRUIT") + ".\n\nIt has a smoky aroma and tastes of coziness.";

				// Token: 0x0400B6E7 RID: 46823
				public static LocString RECIPEDESC = "Slow roasted " + UI.FormatAsLink("Spindly Grubfruit", "WORMBASICFRUIT") + ".";
			}

			// Token: 0x02002CA3 RID: 11427
			public class WORMSUPERFRUIT
			{
				// Token: 0x0400B6E8 RID: 46824
				public static LocString NAME = UI.FormatAsLink("Grubfruit", "WORMSUPERFRUIT");

				// Token: 0x0400B6E9 RID: 46825
				public static LocString DESC = "A plump, healthy fruit with a honey-like taste.";
			}

			// Token: 0x02002CA4 RID: 11428
			public class WORMSUPERFOOD
			{
				// Token: 0x0400B6EA RID: 46826
				public static LocString NAME = UI.FormatAsLink("Grubfruit Preserve", "WORMSUPERFOOD");

				// Token: 0x0400B6EB RID: 46827
				public static LocString DESC = string.Concat(new string[]
				{
					"A long lasting ",
					UI.FormatAsLink("Grubfruit", "WORMSUPERFRUIT"),
					" jam preserved in ",
					UI.FormatAsLink("Sucrose", "SUCROSE"),
					".\n\nThe thick, goopy jam retains the shape of the jar when poured out, but the sweet taste can't be matched."
				});

				// Token: 0x0400B6EC RID: 46828
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"A long lasting ",
					UI.FormatAsLink("Grubfruit", "WORMSUPERFRUIT"),
					" jam preserved in ",
					UI.FormatAsLink("Sucrose", "SUCROSE"),
					"."
				});
			}

			// Token: 0x02002CA5 RID: 11429
			public class BERRYPIE
			{
				// Token: 0x0400B6ED RID: 46829
				public static LocString NAME = UI.FormatAsLink("Mixed Berry Pie", "BERRYPIE");

				// Token: 0x0400B6EE RID: 46830
				public static LocString DESC = string.Concat(new string[]
				{
					"A pie made primarily of ",
					UI.FormatAsLink("Grubfruit", "WORMSUPERFRUIT"),
					" and ",
					UI.FormatAsLink("Gristle Berries", "PRICKLEFRUIT"),
					".\n\nThe mixture of berries creates a fragrant, colorful filling that packs a sweet punch."
				});

				// Token: 0x0400B6EF RID: 46831
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"A pie made primarily of ",
					UI.FormatAsLink("Grubfruit", "WORMSUPERFRUIT"),
					" and ",
					UI.FormatAsLink("Gristle Berries", "PRICKLEFRUIT"),
					"."
				});
			}

			// Token: 0x02002CA6 RID: 11430
			public class COLDWHEATBREAD
			{
				// Token: 0x0400B6F0 RID: 46832
				public static LocString NAME = UI.FormatAsLink("Frost Bun", "COLDWHEATBREAD");

				// Token: 0x0400B6F1 RID: 46833
				public static LocString DESC = "A simple bun baked from " + UI.FormatAsLink("Sleet Wheat Grain", "COLDWHEATSEED") + " grain.\n\nEach bite leaves a mild cooling sensation in one's mouth, even when the bun itself is warm.";

				// Token: 0x0400B6F2 RID: 46834
				public static LocString RECIPEDESC = "A simple bun baked from " + UI.FormatAsLink("Sleet Wheat Grain", "COLDWHEATSEED") + " grain.";
			}

			// Token: 0x02002CA7 RID: 11431
			public class BEAN
			{
				// Token: 0x0400B6F3 RID: 46835
				public static LocString NAME = UI.FormatAsLink("Nosh Bean", "BEAN");

				// Token: 0x0400B6F4 RID: 46836
				public static LocString DESC = "The crisp bean of a " + UI.FormatAsLink("Nosh Sprout", "BEAN_PLANT") + ".\n\nEach bite tastes refreshingly natural and wholesome.";
			}

			// Token: 0x02002CA8 RID: 11432
			public class SPICENUT
			{
				// Token: 0x0400B6F5 RID: 46837
				public static LocString NAME = UI.FormatAsLink("Pincha Peppernut", "SPICENUT");

				// Token: 0x0400B6F6 RID: 46838
				public static LocString DESC = "The flavorful nut of a " + UI.FormatAsLink("Pincha Pepperplant", "SPICE_VINE") + ".\n\nThe bitter outer rind hides a rich, peppery core that is useful in cooking.";
			}

			// Token: 0x02002CA9 RID: 11433
			public class SPICEBREAD
			{
				// Token: 0x0400B6F7 RID: 46839
				public static LocString NAME = UI.FormatAsLink("Pepper Bread", "SPICEBREAD");

				// Token: 0x0400B6F8 RID: 46840
				public static LocString DESC = "A loaf of bread, lightly spiced with " + UI.FormatAsLink("Pincha Peppernut", "SPICENUT") + " for a mild bite.\n\nThere's a simple joy to be had in pulling it apart in one's fingers.";

				// Token: 0x0400B6F9 RID: 46841
				public static LocString RECIPEDESC = "A loaf of bread, lightly spiced with " + UI.FormatAsLink("Pincha Peppernut", "SPICENUT") + " for a mild bite.";
			}

			// Token: 0x02002CAA RID: 11434
			public class SURFANDTURF
			{
				// Token: 0x0400B6FA RID: 46842
				public static LocString NAME = UI.FormatAsLink("Surf'n'Turf", "SURFANDTURF");

				// Token: 0x0400B6FB RID: 46843
				public static LocString DESC = string.Concat(new string[]
				{
					"A bit of ",
					UI.FormatAsLink("Meat", "MEAT"),
					" from the land and ",
					UI.FormatAsLink("Cooked Seafood", "COOKEDFISH"),
					" from the sea.\n\nIt's hearty and satisfying."
				});

				// Token: 0x0400B6FC RID: 46844
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"A bit of ",
					UI.FormatAsLink("Meat", "MEAT"),
					" from the land and ",
					UI.FormatAsLink("Cooked Seafood", "COOKEDFISH"),
					" from the sea."
				});
			}

			// Token: 0x02002CAB RID: 11435
			public class TOFU
			{
				// Token: 0x0400B6FD RID: 46845
				public static LocString NAME = UI.FormatAsLink("Tofu", "TOFU");

				// Token: 0x0400B6FE RID: 46846
				public static LocString DESC = "A bland curd made from " + UI.FormatAsLink("Nosh Beans", "BEAN") + ".\n\nIt has an unusual but pleasant consistency.";

				// Token: 0x0400B6FF RID: 46847
				public static LocString RECIPEDESC = "A bland curd made from " + UI.FormatAsLink("Nosh Beans", "BEAN") + ".";
			}

			// Token: 0x02002CAC RID: 11436
			public class SPICYTOFU
			{
				// Token: 0x0400B700 RID: 46848
				public static LocString NAME = UI.FormatAsLink("Spicy Tofu", "SPICYTOFU");

				// Token: 0x0400B701 RID: 46849
				public static LocString DESC = ITEMS.FOOD.TOFU.NAME + " marinated in a flavorful " + UI.FormatAsLink("Pincha Peppernut", "SPICENUT") + " sauce.\n\nIt packs a delightful punch.";

				// Token: 0x0400B702 RID: 46850
				public static LocString RECIPEDESC = ITEMS.FOOD.TOFU.NAME + " marinated in a flavorful " + UI.FormatAsLink("Pincha Peppernut", "SPICENUT") + " sauce.";
			}

			// Token: 0x02002CAD RID: 11437
			public class CURRY
			{
				// Token: 0x0400B703 RID: 46851
				public static LocString NAME = UI.FormatAsLink("Curried Beans", "CURRY");

				// Token: 0x0400B704 RID: 46852
				public static LocString DESC = string.Concat(new string[]
				{
					"Chewy ",
					UI.FormatAsLink("Nosh Beans", "BEANPLANTSEED"),
					" simmered with chunks of ",
					ITEMS.INGREDIENTS.GINGER.NAME,
					".\n\nIt's so spicy!"
				});

				// Token: 0x0400B705 RID: 46853
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"Chewy ",
					UI.FormatAsLink("Nosh Beans", "BEANPLANTSEED"),
					" simmered with chunks of ",
					ITEMS.INGREDIENTS.GINGER.NAME,
					"."
				});
			}

			// Token: 0x02002CAE RID: 11438
			public class SALSA
			{
				// Token: 0x0400B706 RID: 46854
				public static LocString NAME = UI.FormatAsLink("Stuffed Berry", "SALSA");

				// Token: 0x0400B707 RID: 46855
				public static LocString DESC = "A baked " + UI.FormatAsLink("Bristle Berry", "PRICKLEFRUIT") + " stuffed with delectable spices and vibrantly flavored.";

				// Token: 0x0400B708 RID: 46856
				public static LocString RECIPEDESC = "A baked " + UI.FormatAsLink("Bristle Berry", "PRICKLEFRUIT") + " stuffed with delectable spices and vibrantly flavored.";
			}

			// Token: 0x02002CAF RID: 11439
			public class BASICPLANTFOOD
			{
				// Token: 0x0400B709 RID: 46857
				public static LocString NAME = UI.FormatAsLink("Meal Lice", "BASICPLANTFOOD");

				// Token: 0x0400B70A RID: 46858
				public static LocString DESC = "A flavorless grain that almost never wiggles on its own.";
			}

			// Token: 0x02002CB0 RID: 11440
			public class BASICPLANTBAR
			{
				// Token: 0x0400B70B RID: 46859
				public static LocString NAME = UI.FormatAsLink("Liceloaf", "BASICPLANTBAR");

				// Token: 0x0400B70C RID: 46860
				public static LocString DESC = UI.FormatAsLink("Meal Lice", "BASICPLANTFOOD") + " compacted into a dense, immobile loaf.";

				// Token: 0x0400B70D RID: 46861
				public static LocString RECIPEDESC = UI.FormatAsLink("Meal Lice", "BASICPLANTFOOD") + " compacted into a dense, immobile loaf.";
			}

			// Token: 0x02002CB1 RID: 11441
			public class BASICFORAGEPLANT
			{
				// Token: 0x0400B70E RID: 46862
				public static LocString NAME = UI.FormatAsLink("Muckroot", "BASICFORAGEPLANT");

				// Token: 0x0400B70F RID: 46863
				public static LocString DESC = "A seedless fruit with an upsettingly bland aftertaste.\n\nIt cannot be replanted.\n\nDigging up Buried Objects may uncover a " + ITEMS.FOOD.BASICFORAGEPLANT.NAME + ".";
			}

			// Token: 0x02002CB2 RID: 11442
			public class FORESTFORAGEPLANT
			{
				// Token: 0x0400B710 RID: 46864
				public static LocString NAME = UI.FormatAsLink("Hexalent Fruit", "FORESTFORAGEPLANT");

				// Token: 0x0400B711 RID: 46865
				public static LocString DESC = "A seedless fruit with an unusual rubbery texture.\n\nIt cannot be replanted.\n\nHexalent fruit is much more calorie dense than Muckroot fruit.";
			}

			// Token: 0x02002CB3 RID: 11443
			public class SWAMPFORAGEPLANT
			{
				// Token: 0x0400B712 RID: 46866
				public static LocString NAME = UI.FormatAsLink("Swamp Chard Heart", "SWAMPFORAGEPLANT");

				// Token: 0x0400B713 RID: 46867
				public static LocString DESC = "A seedless plant with a squishy, juicy center and an awful smell.\n\nIt cannot be replanted.";
			}

			// Token: 0x02002CB4 RID: 11444
			public class ROTPILE
			{
				// Token: 0x0400B714 RID: 46868
				public static LocString NAME = UI.FormatAsLink("Rot Pile", "COMPOST");

				// Token: 0x0400B715 RID: 46869
				public static LocString DESC = string.Concat(new string[]
				{
					"An inedible glop of former foodstuff.\n\n",
					ITEMS.FOOD.ROTPILE.NAME,
					"s break down into ",
					UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
					" over time."
				});
			}

			// Token: 0x02002CB5 RID: 11445
			public class COLDWHEATSEED
			{
				// Token: 0x0400B716 RID: 46870
				public static LocString NAME = UI.FormatAsLink("Sleet Wheat Grain", "COLDWHEATSEED");

				// Token: 0x0400B717 RID: 46871
				public static LocString DESC = "An edible grain that leaves a cool taste on the tongue.";
			}

			// Token: 0x02002CB6 RID: 11446
			public class BEANPLANTSEED
			{
				// Token: 0x0400B718 RID: 46872
				public static LocString NAME = UI.FormatAsLink("Nosh Bean", "BEANPLANTSEED");

				// Token: 0x0400B719 RID: 46873
				public static LocString DESC = "An inedible bean that can be processed into delicious foods.";
			}
		}

		// Token: 0x02001CF0 RID: 7408
		public class INGREDIENTS
		{
			// Token: 0x02002CB7 RID: 11447
			public class SWAMPLILYFLOWER
			{
				// Token: 0x0400B71A RID: 46874
				public static LocString NAME = UI.FormatAsLink("Balm Lily Flower", "SWAMPLILYFLOWER");

				// Token: 0x0400B71B RID: 46875
				public static LocString DESC = "A medicinal flower that soothes most minor maladies.\n\nIt is exceptionally fragrant.";
			}

			// Token: 0x02002CB8 RID: 11448
			public class GINGER
			{
				// Token: 0x0400B71C RID: 46876
				public static LocString NAME = UI.FormatAsLink("Tonic Root", "GINGER");

				// Token: 0x0400B71D RID: 46877
				public static LocString DESC = "A chewy, fibrous rhizome with a fiery aftertaste.";
			}
		}

		// Token: 0x02001CF1 RID: 7409
		public class INDUSTRIAL_PRODUCTS
		{
			// Token: 0x02002CB9 RID: 11449
			public class FUEL_BRICK
			{
				// Token: 0x0400B71E RID: 46878
				public static LocString NAME = "Fuel Brick";

				// Token: 0x0400B71F RID: 46879
				public static LocString DESC = "A densely compressed brick of combustible material.\n\nIt can be burned to produce a one-time burst of " + UI.FormatAsLink("Power", "POWER") + ".";
			}

			// Token: 0x02002CBA RID: 11450
			public class BASIC_FABRIC
			{
				// Token: 0x0400B720 RID: 46880
				public static LocString NAME = "Reed Fiber";

				// Token: 0x0400B721 RID: 46881
				public static LocString DESC = "A ball of raw cellulose used in the production of " + UI.FormatAsLink("Clothing", "EQUIPMENT") + " and textiles.";
			}

			// Token: 0x02002CBB RID: 11451
			public class TRAP_PARTS
			{
				// Token: 0x0400B722 RID: 46882
				public static LocString NAME = "Trap Components";

				// Token: 0x0400B723 RID: 46883
				public static LocString DESC = string.Concat(new string[]
				{
					"These components can be assembled into a ",
					BUILDINGS.PREFABS.CREATURETRAP.NAME,
					" and used to catch ",
					UI.FormatAsLink("Critters", "CREATURES"),
					"."
				});
			}

			// Token: 0x02002CBC RID: 11452
			public class POWER_STATION_TOOLS
			{
				// Token: 0x0400B724 RID: 46884
				public static LocString NAME = "Microchip";

				// Token: 0x0400B725 RID: 46885
				public static LocString DESC = string.Concat(new string[]
				{
					"A specialized ",
					ITEMS.INDUSTRIAL_PRODUCTS.POWER_STATION_TOOLS.NAME,
					" created by a professional engineer.\n\nTunes up ",
					UI.PRE_KEYWORD,
					"Generators",
					UI.PST_KEYWORD,
					" to increase their ",
					UI.FormatAsLink("Power", "POWER"),
					" output."
				});

				// Token: 0x0400B726 RID: 46886
				public static LocString TINKER_REQUIREMENT_NAME = "Skill: " + DUPLICANTS.ROLES.POWER_TECHNICIAN.NAME;

				// Token: 0x0400B727 RID: 46887
				public static LocString TINKER_REQUIREMENT_TOOLTIP = string.Concat(new string[]
				{
					"Can only be used by a Duplicant with ",
					DUPLICANTS.ROLES.POWER_TECHNICIAN.NAME,
					" to apply a ",
					UI.PRE_KEYWORD,
					"Tune Up",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x0400B728 RID: 46888
				public static LocString TINKER_EFFECT_NAME = "Engie's Tune-Up: {0} {1}";

				// Token: 0x0400B729 RID: 46889
				public static LocString TINKER_EFFECT_TOOLTIP = string.Concat(new string[]
				{
					"Can be used to ",
					UI.PRE_KEYWORD,
					"Tune Up",
					UI.PST_KEYWORD,
					" a generator, increasing its {0} by <b>{1}</b>."
				});
			}

			// Token: 0x02002CBD RID: 11453
			public class FARM_STATION_TOOLS
			{
				// Token: 0x0400B72A RID: 46890
				public static LocString NAME = "Micronutrient Fertilizer";

				// Token: 0x0400B72B RID: 46891
				public static LocString DESC = string.Concat(new string[]
				{
					"Specialized ",
					UI.FormatAsLink("Fertilizer", "FERTILIZER"),
					" mixed by a Duplicant with the ",
					DUPLICANTS.ROLES.FARMER.NAME,
					" Skill.\n\nIncreases the ",
					UI.PRE_KEYWORD,
					"Growth Rate",
					UI.PST_KEYWORD,
					" of one ",
					UI.FormatAsLink("Plant", "PLANTS"),
					"."
				});
			}

			// Token: 0x02002CBE RID: 11454
			public class MACHINE_PARTS
			{
				// Token: 0x0400B72C RID: 46892
				public static LocString NAME = "Custom Parts";

				// Token: 0x0400B72D RID: 46893
				public static LocString DESC = string.Concat(new string[]
				{
					"Specialized Parts crafted by a professional engineer.\n\n",
					UI.PRE_KEYWORD,
					"Jerry Rig",
					UI.PST_KEYWORD,
					" machine buildings to increase their efficiency."
				});

				// Token: 0x0400B72E RID: 46894
				public static LocString TINKER_REQUIREMENT_NAME = "Job: " + DUPLICANTS.ROLES.MECHATRONIC_ENGINEER.NAME;

				// Token: 0x0400B72F RID: 46895
				public static LocString TINKER_REQUIREMENT_TOOLTIP = string.Concat(new string[]
				{
					"Can only be used by a Duplicant with ",
					DUPLICANTS.ROLES.MECHATRONIC_ENGINEER.NAME,
					" to apply a ",
					UI.PRE_KEYWORD,
					"Jerry Rig",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x0400B730 RID: 46896
				public static LocString TINKER_EFFECT_NAME = "Engineer's Jerry Rig: {0} {1}";

				// Token: 0x0400B731 RID: 46897
				public static LocString TINKER_EFFECT_TOOLTIP = string.Concat(new string[]
				{
					"Can be used to ",
					UI.PRE_KEYWORD,
					"Jerry Rig",
					UI.PST_KEYWORD,
					" upgrades to a machine building, increasing its {0} by <b>{1}</b>."
				});
			}

			// Token: 0x02002CBF RID: 11455
			public class RESEARCH_DATABANK
			{
				// Token: 0x0400B732 RID: 46898
				public static LocString NAME = "Data Bank";

				// Token: 0x0400B733 RID: 46899
				public static LocString DESC = "Raw data that can be processed into " + UI.FormatAsLink("Interstellar Research", "RESEARCH") + " points.";
			}

			// Token: 0x02002CC0 RID: 11456
			public class ORBITAL_RESEARCH_DATABANK
			{
				// Token: 0x0400B734 RID: 46900
				public static LocString NAME = "Data Bank";

				// Token: 0x0400B735 RID: 46901
				public static LocString DESC = "Raw Data that can be processed into " + UI.FormatAsLink("Data Analysis Research", "RESEARCH") + " points.";

				// Token: 0x0400B736 RID: 46902
				public static LocString RECIPE_DESC = string.Concat(new string[]
				{
					"Databanks of raw data generated from exploring, either by exploring new areas with Duplicants, or by using an ",
					UI.FormatAsLink("Orbital Data Collection Lab", "ORBITALRESEARCHCENTER"),
					".\n\nUsed by the ",
					UI.FormatAsLink("Virtual Planetarium", "DLC1COSMICRESEARCHCENTER"),
					" to conduct research."
				});
			}

			// Token: 0x02002CC1 RID: 11457
			public class EGG_SHELL
			{
				// Token: 0x0400B737 RID: 46903
				public static LocString NAME = "Egg Shell";

				// Token: 0x0400B738 RID: 46904
				public static LocString DESC = "Can be crushed to produce " + UI.FormatAsLink("Lime", "LIME") + ".";
			}

			// Token: 0x02002CC2 RID: 11458
			public class CRAB_SHELL
			{
				// Token: 0x0400B739 RID: 46905
				public static LocString NAME = "Pokeshell Molt";

				// Token: 0x0400B73A RID: 46906
				public static LocString DESC = "Can be crushed to produce " + UI.FormatAsLink("Lime", "LIME") + ".";

				// Token: 0x02002FD1 RID: 12241
				public class VARIANT_WOOD
				{
					// Token: 0x0400BF45 RID: 48965
					public static LocString NAME = "Oakshell Molt";

					// Token: 0x0400BF46 RID: 48966
					public static LocString DESC = "Can be crushed to produce " + UI.FormatAsLink("Lumber", "WOOD") + ".";
				}
			}

			// Token: 0x02002CC3 RID: 11459
			public class BABY_CRAB_SHELL
			{
				// Token: 0x0400B73B RID: 46907
				public static LocString NAME = "Small Pokeshell Molt";

				// Token: 0x0400B73C RID: 46908
				public static LocString DESC = "Can be crushed to produce " + UI.FormatAsLink("Lime", "LIME") + ".";

				// Token: 0x02002FD2 RID: 12242
				public class VARIANT_WOOD
				{
					// Token: 0x0400BF47 RID: 48967
					public static LocString NAME = "Small Oakshell Molt";

					// Token: 0x0400BF48 RID: 48968
					public static LocString DESC = "Can be crushed to produce " + UI.FormatAsLink("Lumber", "WOOD") + ".";
				}
			}

			// Token: 0x02002CC4 RID: 11460
			public class WOOD
			{
				// Token: 0x0400B73D RID: 46909
				public static LocString NAME = "Lumber";

				// Token: 0x0400B73E RID: 46910
				public static LocString DESC = string.Concat(new string[]
				{
					"Wood harvested from an ",
					UI.FormatAsLink("Arbor Tree", "FOREST_TREE"),
					" or an ",
					UI.FormatAsLink("Oakshell", "CRABWOOD"),
					"."
				});
			}

			// Token: 0x02002CC5 RID: 11461
			public class GENE_SHUFFLER_RECHARGE
			{
				// Token: 0x0400B73F RID: 46911
				public static LocString NAME = "Vacillator Recharge";

				// Token: 0x0400B740 RID: 46912
				public static LocString DESC = "Replenishes one charge to a depleted " + BUILDINGS.PREFABS.GENESHUFFLER.NAME + ".";
			}

			// Token: 0x02002CC6 RID: 11462
			public class TABLE_SALT
			{
				// Token: 0x0400B741 RID: 46913
				public static LocString NAME = "Table Salt";

				// Token: 0x0400B742 RID: 46914
				public static LocString DESC = string.Concat(new string[]
				{
					"A seasoning that Duplicants can add to their ",
					UI.FormatAsLink("Food", "FOOD"),
					" to boost ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nDuplicants will automatically use Table Salt while sitting at a ",
					BUILDINGS.PREFABS.DININGTABLE.NAME,
					" during mealtime.\n\n<i>Only the finest grains are chosen.</i>"
				});
			}

			// Token: 0x02002CC7 RID: 11463
			public class REFINED_SUGAR
			{
				// Token: 0x0400B743 RID: 46915
				public static LocString NAME = "Refined Sugar";

				// Token: 0x0400B744 RID: 46916
				public static LocString DESC = string.Concat(new string[]
				{
					"A seasoning that Duplicants can add to their ",
					UI.FormatAsLink("Food", "FOOD"),
					" to boost ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nDuplicants will automatically use Refined Sugar while sitting at a ",
					BUILDINGS.PREFABS.DININGTABLE.NAME,
					" during mealtime.\n\n<i>Only the finest grains are chosen.</i>"
				});
			}
		}

		// Token: 0x02001CF2 RID: 7410
		public class CARGO_CAPSULE
		{
			// Token: 0x04008452 RID: 33874
			public static LocString NAME = "Care Package";

			// Token: 0x04008453 RID: 33875
			public static LocString DESC = "A delivery system for recently printed resources.\n\nIt will dematerialize shortly.";
		}

		// Token: 0x02001CF3 RID: 7411
		public class RAILGUNPAYLOAD
		{
			// Token: 0x04008454 RID: 33876
			public static LocString NAME = UI.FormatAsLink("Interplanetary Payload", "RAILGUNPAYLOAD");

			// Token: 0x04008455 RID: 33877
			public static LocString DESC = string.Concat(new string[]
			{
				"Contains resources packed for interstellar shipping.\n\nCan be launched by a ",
				BUILDINGS.PREFABS.RAILGUN.NAME,
				" or unpacked with a ",
				BUILDINGS.PREFABS.RAILGUNPAYLOADOPENER.NAME,
				"."
			});
		}

		// Token: 0x02001CF4 RID: 7412
		public class DEBRISPAYLOAD
		{
			// Token: 0x04008456 RID: 33878
			public static LocString NAME = "Rocket Debris";

			// Token: 0x04008457 RID: 33879
			public static LocString DESC = "Whatever is left over from a Rocket Self-Destruct can be recovered once it has crash-landed.";
		}

		// Token: 0x02001CF5 RID: 7413
		public class RADIATION
		{
			// Token: 0x02002CC8 RID: 11464
			public class HIGHENERGYPARITCLE
			{
				// Token: 0x0400B745 RID: 46917
				public static LocString NAME = "Radbolts";

				// Token: 0x0400B746 RID: 46918
				public static LocString DESC = string.Concat(new string[]
				{
					"A concentrated field of ",
					UI.FormatAsKeyWord("Radbolts"),
					" that can be largely redirected using a ",
					UI.FormatAsLink("Radbolt Reflector", "HIGHENERGYPARTICLEREDIRECTOR"),
					"."
				});
			}
		}

		// Token: 0x02001CF6 RID: 7414
		public class DREAMJOURNAL
		{
			// Token: 0x04008458 RID: 33880
			public static LocString NAME = "Dream Journal";

			// Token: 0x04008459 RID: 33881
			public static LocString DESC = string.Concat(new string[]
			{
				"A hand-scrawled account of ",
				UI.FormatAsLink("Pajama", "SLEEP_CLINIC_PAJAMAS"),
				"-induced dreams.\n\nCan be analyzed using a ",
				UI.FormatAsLink("Somnium Synthesizer", "MEGABRAINTANK"),
				"."
			});
		}

		// Token: 0x02001CF7 RID: 7415
		public class SPICES
		{
			// Token: 0x02002CC9 RID: 11465
			public class MACHINERY_SPICE
			{
				// Token: 0x0400B747 RID: 46919
				public static LocString NAME = UI.FormatAsLink("Machinist Spice", "MACHINERY_SPICE");

				// Token: 0x0400B748 RID: 46920
				public static LocString DESC = "Improves operating skills when ingested.";
			}

			// Token: 0x02002CCA RID: 11466
			public class PILOTING_SPICE
			{
				// Token: 0x0400B749 RID: 46921
				public static LocString NAME = UI.FormatAsLink("Rocketeer Spice", "PILOTING_SPICE");

				// Token: 0x0400B74A RID: 46922
				public static LocString DESC = "Provides a boost to piloting abilities.";
			}

			// Token: 0x02002CCB RID: 11467
			public class PRESERVING_SPICE
			{
				// Token: 0x0400B74B RID: 46923
				public static LocString NAME = UI.FormatAsLink("Freshener Spice", "PRESERVING_SPICE");

				// Token: 0x0400B74C RID: 46924
				public static LocString DESC = "Slows the decomposition of perishable foods.";
			}

			// Token: 0x02002CCC RID: 11468
			public class STRENGTH_SPICE
			{
				// Token: 0x0400B74D RID: 46925
				public static LocString NAME = UI.FormatAsLink("Brawny Spice", "STRENGTH_SPICE");

				// Token: 0x0400B74E RID: 46926
				public static LocString DESC = "Strengthens even the weakest of muscles.";
			}
		}
	}
}
