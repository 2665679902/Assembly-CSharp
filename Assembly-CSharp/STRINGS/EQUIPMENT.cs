using System;

namespace STRINGS
{
	// Token: 0x02000D38 RID: 3384
	public class EQUIPMENT
	{
		// Token: 0x02001BBF RID: 7103
		public class PREFABS
		{
			// Token: 0x02002309 RID: 8969
			public class OXYGEN_MASK
			{
				// Token: 0x04009A56 RID: 39510
				public static LocString NAME = UI.FormatAsLink("Oxygen Mask", "OXYGEN_MASK");

				// Token: 0x04009A57 RID: 39511
				public static LocString DESC = "Ensures my Duplicants can breathe easy... for a little while, anyways.";

				// Token: 0x04009A58 RID: 39512
				public static LocString EFFECT = "Supplies Duplicants with <style=\"oxygen\">Oxygen</style> in toxic and low breathability environments.\n\nMust be refilled with oxygen at an " + UI.FormatAsLink("Atmo Suit Dock", "SUITLOCKER") + " when depleted.";

				// Token: 0x04009A59 RID: 39513
				public static LocString RECIPE_DESC = "Supplies Duplicants with <style=\"oxygen\">Oxygen</style> in toxic and low breathability environments.";

				// Token: 0x04009A5A RID: 39514
				public static LocString GENERICNAME = "Suit";

				// Token: 0x04009A5B RID: 39515
				public static LocString WORN_NAME = UI.FormatAsLink("Worn Oxygen Mask", "OXYGEN_MASK");

				// Token: 0x04009A5C RID: 39516
				public static LocString WORN_DESC = string.Concat(new string[]
				{
					"A worn out ",
					UI.FormatAsLink("Oxygen Mask", "OXYGEN_MASK"),
					".\n\nMasks can be repaired at a ",
					UI.FormatAsLink("Crafting Station", "CRAFTINGTABLE"),
					"."
				});
			}

			// Token: 0x0200230A RID: 8970
			public class ATMO_SUIT
			{
				// Token: 0x04009A5D RID: 39517
				public static LocString NAME = UI.FormatAsLink("Atmo Suit", "ATMO_SUIT");

				// Token: 0x04009A5E RID: 39518
				public static LocString DESC = "Ensures my Duplicants can breathe easy, anytime, anywhere.";

				// Token: 0x04009A5F RID: 39519
				public static LocString EFFECT = "Supplies Duplicants with <style=\"oxygen\">Oxygen</style> in toxic and low breathability environments.\n\nMust be refilled with oxygen at an " + UI.FormatAsLink("Atmo Suit Dock", "SUITLOCKER") + " when depleted.";

				// Token: 0x04009A60 RID: 39520
				public static LocString RECIPE_DESC = "Supplies Duplicants with <style=\"oxygen\">Oxygen</style> in toxic and low breathability environments.";

				// Token: 0x04009A61 RID: 39521
				public static LocString GENERICNAME = "Suit";

				// Token: 0x04009A62 RID: 39522
				public static LocString WORN_NAME = UI.FormatAsLink("Worn Atmo Suit", "ATMO_SUIT");

				// Token: 0x04009A63 RID: 39523
				public static LocString WORN_DESC = string.Concat(new string[]
				{
					"A worn out ",
					UI.FormatAsLink("Atmo Suit", "ATMO_SUIT"),
					".\n\nSuits can be repaired at an ",
					UI.FormatAsLink("Exosuit Forge", "SUITFABRICATOR"),
					"."
				});

				// Token: 0x04009A64 RID: 39524
				public static LocString REPAIR_WORN_RECIPE_NAME = "Repair " + EQUIPMENT.PREFABS.ATMO_SUIT.NAME;

				// Token: 0x04009A65 RID: 39525
				public static LocString REPAIR_WORN_DESC = "Restore a " + UI.FormatAsLink("Worn Atmo Suit", "ATMO_SUIT") + " to working order.";
			}

			// Token: 0x0200230B RID: 8971
			public class AQUA_SUIT
			{
				// Token: 0x04009A66 RID: 39526
				public static LocString NAME = UI.FormatAsLink("Aqua Suit", "AQUA_SUIT");

				// Token: 0x04009A67 RID: 39527
				public static LocString DESC = "Because breathing underwater is better than... not.";

				// Token: 0x04009A68 RID: 39528
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Supplies Duplicants with <style=\"oxygen\">Oxygen</style> in underwater environments.\n\nMust be refilled with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" at an ",
					UI.FormatAsLink("Atmo Suit Dock", "SUITLOCKER"),
					" when depleted."
				});

				// Token: 0x04009A69 RID: 39529
				public static LocString RECIPE_DESC = "Supplies Duplicants with <style=\"oxygen\">Oxygen</style> in underwater environments.";

				// Token: 0x04009A6A RID: 39530
				public static LocString WORN_NAME = UI.FormatAsLink("Worn Lead Suit", "AQUA_SUIT");

				// Token: 0x04009A6B RID: 39531
				public static LocString WORN_DESC = string.Concat(new string[]
				{
					"A worn out ",
					UI.FormatAsLink("Aqua Suit", "AQUA_SUIT"),
					".\n\nSuits can be repaired at a ",
					UI.FormatAsLink("Crafting Station", "CRAFTINGTABLE"),
					"."
				});
			}

			// Token: 0x0200230C RID: 8972
			public class TEMPERATURE_SUIT
			{
				// Token: 0x04009A6C RID: 39532
				public static LocString NAME = UI.FormatAsLink("Thermo Suit", "TEMPERATURE_SUIT");

				// Token: 0x04009A6D RID: 39533
				public static LocString DESC = "Keeps my Duplicants cool in case things heat up.";

				// Token: 0x04009A6E RID: 39534
				public static LocString EFFECT = "Provides insulation in regions with extreme <style=\"heat\">Temperatures</style>.\n\nMust be powered at a Thermo Suit Dock when depleted.";

				// Token: 0x04009A6F RID: 39535
				public static LocString RECIPE_DESC = "Provides insulation in regions with extreme <style=\"heat\">Temperatures</style>.";

				// Token: 0x04009A70 RID: 39536
				public static LocString WORN_NAME = UI.FormatAsLink("Worn Lead Suit", "TEMPERATURE_SUIT");

				// Token: 0x04009A71 RID: 39537
				public static LocString WORN_DESC = string.Concat(new string[]
				{
					"A worn out ",
					UI.FormatAsLink("Thermo Suit", "TEMPERATURE_SUIT"),
					".\n\nSuits can be repaired at a ",
					UI.FormatAsLink("Crafting Station", "CRAFTINGTABLE"),
					"."
				});
			}

			// Token: 0x0200230D RID: 8973
			public class JET_SUIT
			{
				// Token: 0x04009A72 RID: 39538
				public static LocString NAME = UI.FormatAsLink("Jet Suit", "JET_SUIT");

				// Token: 0x04009A73 RID: 39539
				public static LocString DESC = "Allows my Duplicants to take to the skies, for a time.";

				// Token: 0x04009A74 RID: 39540
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Supplies Duplicants with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" in toxic and low breathability environments.\n\nMust be refilled with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" and ",
					UI.FormatAsLink("Petroleum", "PETROLEUM"),
					" at a ",
					UI.FormatAsLink("Jet Suit Dock", "JETSUITLOCKER"),
					" when depleted."
				});

				// Token: 0x04009A75 RID: 39541
				public static LocString RECIPE_DESC = "Supplies Duplicants with " + UI.FormatAsLink("Oxygen", "OXYGEN") + " in toxic and low breathability environments.\n\nAllows Duplicant flight.";

				// Token: 0x04009A76 RID: 39542
				public static LocString GENERICNAME = "Jet Suit";

				// Token: 0x04009A77 RID: 39543
				public static LocString TANK_EFFECT_NAME = "Fuel Tank";

				// Token: 0x04009A78 RID: 39544
				public static LocString WORN_NAME = UI.FormatAsLink("Worn Jet Suit", "JET_SUIT");

				// Token: 0x04009A79 RID: 39545
				public static LocString WORN_DESC = string.Concat(new string[]
				{
					"A worn out ",
					UI.FormatAsLink("Jet Suit", "JET_SUIT"),
					".\n\nSuits can be repaired at an ",
					UI.FormatAsLink("Exosuit Forge", "SUITFABRICATOR"),
					"."
				});
			}

			// Token: 0x0200230E RID: 8974
			public class LEAD_SUIT
			{
				// Token: 0x04009A7A RID: 39546
				public static LocString NAME = UI.FormatAsLink("Lead Suit", "LEAD_SUIT");

				// Token: 0x04009A7B RID: 39547
				public static LocString DESC = "Because exposure to radiation doesn't grant Duplicants superpowers.";

				// Token: 0x04009A7C RID: 39548
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Supplies Duplicants with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" and protection in areas with ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					".\n\nMust be refilled with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" at a ",
					UI.FormatAsLink("Lead Suit Dock", "LEADSUITLOCKER"),
					" when depleted."
				});

				// Token: 0x04009A7D RID: 39549
				public static LocString RECIPE_DESC = string.Concat(new string[]
				{
					"Supplies Duplicants with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" in toxic and low breathability environments.\n\nProtects Duplicants from ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					"."
				});

				// Token: 0x04009A7E RID: 39550
				public static LocString GENERICNAME = "Lead Suit";

				// Token: 0x04009A7F RID: 39551
				public static LocString BATTERY_EFFECT_NAME = "Suit Battery";

				// Token: 0x04009A80 RID: 39552
				public static LocString SUIT_OUT_OF_BATTERIES = "Suit Batteries Empty";

				// Token: 0x04009A81 RID: 39553
				public static LocString WORN_NAME = UI.FormatAsLink("Worn Lead Suit", "LEAD_SUIT");

				// Token: 0x04009A82 RID: 39554
				public static LocString WORN_DESC = string.Concat(new string[]
				{
					"A worn out ",
					UI.FormatAsLink("Lead Suit", "LEAD_SUIT"),
					".\n\nSuits can be repaired at an ",
					UI.FormatAsLink("Exosuit Forge", "SUITFABRICATOR"),
					"."
				});
			}

			// Token: 0x0200230F RID: 8975
			public class COOL_VEST
			{
				// Token: 0x04009A83 RID: 39555
				public static LocString NAME = UI.FormatAsLink("Cool Vest", "COOL_VEST");

				// Token: 0x04009A84 RID: 39556
				public static LocString GENERICNAME = "Clothing";

				// Token: 0x04009A85 RID: 39557
				public static LocString DESC = "Don't sweat it!";

				// Token: 0x04009A86 RID: 39558
				public static LocString EFFECT = "Protects the wearer from <style=\"heat\">Heat</style> by decreasing insulation.";

				// Token: 0x04009A87 RID: 39559
				public static LocString RECIPE_DESC = "Protects the wearer from <style=\"heat\">Heat</style> by decreasing insulation";
			}

			// Token: 0x02002310 RID: 8976
			public class WARM_VEST
			{
				// Token: 0x04009A88 RID: 39560
				public static LocString NAME = UI.FormatAsLink("Warm Sweater", "WARM_VEST");

				// Token: 0x04009A89 RID: 39561
				public static LocString GENERICNAME = "Clothing";

				// Token: 0x04009A8A RID: 39562
				public static LocString DESC = "Happiness is a warm Duplicant.";

				// Token: 0x04009A8B RID: 39563
				public static LocString EFFECT = "Protects the wearer from <style=\"heat\">Cold</style> by increasing insulation.";

				// Token: 0x04009A8C RID: 39564
				public static LocString RECIPE_DESC = "Protects the wearer from <style=\"heat\">Cold</style> by increasing insulation";
			}

			// Token: 0x02002311 RID: 8977
			public class FUNKY_VEST
			{
				// Token: 0x04009A8D RID: 39565
				public static LocString NAME = UI.FormatAsLink("Snazzy Suit", "FUNKY_VEST");

				// Token: 0x04009A8E RID: 39566
				public static LocString GENERICNAME = "Clothing";

				// Token: 0x04009A8F RID: 39567
				public static LocString DESC = "This transforms my Duplicant into a walking beacon of charm and style.";

				// Token: 0x04009A90 RID: 39568
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Increases Decor in a small area effect around the wearer. Can be upgraded to ",
					UI.FormatAsLink("Primo Garb", "CUSTOMCLOTHING"),
					" at the ",
					UI.FormatAsLink("Clothing Refashionator", "CLOTHINGALTERATIONSTATION"),
					"."
				});

				// Token: 0x04009A91 RID: 39569
				public static LocString RECIPE_DESC = "Increases Decor in a small area effect around the wearer. Can be upgraded to " + UI.FormatAsLink("Primo Garb", "CUSTOMCLOTHING") + " at the " + UI.FormatAsLink("Clothing Refashionator", "CLOTHINGALTERATIONSTATION");
			}

			// Token: 0x02002312 RID: 8978
			public class CUSTOMCLOTHING
			{
				// Token: 0x04009A92 RID: 39570
				public static LocString NAME = UI.FormatAsLink("Primo Garb", "CUSTOMCLOTHING");

				// Token: 0x04009A93 RID: 39571
				public static LocString GENERICNAME = "Clothing";

				// Token: 0x04009A94 RID: 39572
				public static LocString DESC = "This transforms my Duplicant into a colony-inspiring fashion icon.";

				// Token: 0x04009A95 RID: 39573
				public static LocString EFFECT = "Increases Decor in a small area effect around the wearer.";

				// Token: 0x04009A96 RID: 39574
				public static LocString RECIPE_DESC = "Increases Decor in a small area effect around the wearer";

				// Token: 0x02002DCF RID: 11727
				public class FACADES
				{
					// Token: 0x0400BA72 RID: 47730
					public static LocString CLUBSHIRT = UI.FormatAsLink("Purple Polyester", "CUSTOMCLOTHING");

					// Token: 0x0400BA73 RID: 47731
					public static LocString CUMMERBUND = UI.FormatAsLink("Classic Cummerbund", "CUSTOMCLOTHING");

					// Token: 0x0400BA74 RID: 47732
					public static LocString DECOR_02 = UI.FormatAsLink("Snazzier Red Suit", "CUSTOMCLOTHING");

					// Token: 0x0400BA75 RID: 47733
					public static LocString DECOR_03 = UI.FormatAsLink("Snazzier Blue Suit", "CUSTOMCLOTHING");

					// Token: 0x0400BA76 RID: 47734
					public static LocString DECOR_04 = UI.FormatAsLink("Snazzier Green Suit", "CUSTOMCLOTHING");

					// Token: 0x0400BA77 RID: 47735
					public static LocString DECOR_05 = UI.FormatAsLink("Snazzier Violet Suit", "CUSTOMCLOTHING");

					// Token: 0x0400BA78 RID: 47736
					public static LocString GAUDYSWEATER = UI.FormatAsLink("Pompom Knit", "CUSTOMCLOTHING");

					// Token: 0x0400BA79 RID: 47737
					public static LocString LIMONE = UI.FormatAsLink("Citrus Spandex", "CUSTOMCLOTHING");

					// Token: 0x0400BA7A RID: 47738
					public static LocString MONDRIAN = UI.FormatAsLink("Cubist Knit", "CUSTOMCLOTHING");

					// Token: 0x0400BA7B RID: 47739
					public static LocString OVERALLS = UI.FormatAsLink("Spiffy Overalls", "CUSTOMCLOTHING");

					// Token: 0x0400BA7C RID: 47740
					public static LocString TRIANGLES = UI.FormatAsLink("Confetti Suit", "CUSTOMCLOTHING");

					// Token: 0x0400BA7D RID: 47741
					public static LocString WORKOUT = UI.FormatAsLink("Pink Unitard", "CUSTOMCLOTHING");
				}
			}

			// Token: 0x02002313 RID: 8979
			public class CLOTHING_GLOVES
			{
				// Token: 0x04009A97 RID: 39575
				public static LocString NAME = UI.FormatAsLink("Gloves", "CLOTHING_GLOVES");

				// Token: 0x04009A98 RID: 39576
				public static LocString GENERICNAME = "Clothing";

				// Token: 0x04009A99 RID: 39577
				public static LocString DESC = "Testing desc for gloves skins";

				// Token: 0x04009A9A RID: 39578
				public static LocString EFFECT = "Testing effect for gloves skins";

				// Token: 0x04009A9B RID: 39579
				public static LocString RECIPE_DESC = "Testing recipe desc for gloves skins";

				// Token: 0x02002DD0 RID: 11728
				public class FACADES
				{
					// Token: 0x02003059 RID: 12377
					public class BASIC_BLUE_MIDDLE
					{
						// Token: 0x0400C133 RID: 49459
						public static LocString NAME = "Basic Aqua Gloves";

						// Token: 0x0400C134 RID: 49460
						public static LocString DESC = "A good, solid pair of aqua-blue gloves that go with everything.";
					}

					// Token: 0x0200305A RID: 12378
					public class BASIC_YELLOW
					{
						// Token: 0x0400C135 RID: 49461
						public static LocString NAME = "Basic Yellow Gloves";

						// Token: 0x0400C136 RID: 49462
						public static LocString DESC = "A good, solid pair of yellow gloves that go with everything.";
					}

					// Token: 0x0200305B RID: 12379
					public class BASIC_BLACK
					{
						// Token: 0x0400C137 RID: 49463
						public static LocString NAME = "Basic Black Gloves";

						// Token: 0x0400C138 RID: 49464
						public static LocString DESC = "A good, solid pair of black gloves that go with everything.";
					}

					// Token: 0x0200305C RID: 12380
					public class BASIC_PINK_ORCHID
					{
						// Token: 0x0400C139 RID: 49465
						public static LocString NAME = "Basic Bubblegum Gloves";

						// Token: 0x0400C13A RID: 49466
						public static LocString DESC = "A good, solid pair of bubblegum-pink gloves that go with everything.";
					}

					// Token: 0x0200305D RID: 12381
					public class BASIC_GREEN
					{
						// Token: 0x0400C13B RID: 49467
						public static LocString NAME = "Basic Green Gloves";

						// Token: 0x0400C13C RID: 49468
						public static LocString DESC = "A good, solid pair of green gloves that go with everything.";
					}

					// Token: 0x0200305E RID: 12382
					public class BASIC_ORANGE
					{
						// Token: 0x0400C13D RID: 49469
						public static LocString NAME = "Basic Orange Gloves";

						// Token: 0x0400C13E RID: 49470
						public static LocString DESC = "A good, solid pair of orange gloves that go with everything.";
					}

					// Token: 0x0200305F RID: 12383
					public class BASIC_PURPLE
					{
						// Token: 0x0400C13F RID: 49471
						public static LocString NAME = "Basic Purple Gloves";

						// Token: 0x0400C140 RID: 49472
						public static LocString DESC = "A good, solid pair of purple gloves that go with everything.";
					}

					// Token: 0x02003060 RID: 12384
					public class BASIC_RED
					{
						// Token: 0x0400C141 RID: 49473
						public static LocString NAME = "Basic Red Gloves";

						// Token: 0x0400C142 RID: 49474
						public static LocString DESC = "A good, solid pair of red gloves that go with everything.";
					}

					// Token: 0x02003061 RID: 12385
					public class BASIC_WHITE
					{
						// Token: 0x0400C143 RID: 49475
						public static LocString NAME = "Basic White Gloves";

						// Token: 0x0400C144 RID: 49476
						public static LocString DESC = "A good, solid pair of white gloves that go with everything.";
					}

					// Token: 0x02003062 RID: 12386
					public class GLOVES_ATHLETIC_DEEPRED
					{
						// Token: 0x0400C145 RID: 49477
						public static LocString NAME = "Team Captain Sports Gloves";

						// Token: 0x0400C146 RID: 49478
						public static LocString DESC = "Red-striped gloves for winning at any activity.";
					}

					// Token: 0x02003063 RID: 12387
					public class GLOVES_ATHLETIC_SATSUMA
					{
						// Token: 0x0400C147 RID: 49479
						public static LocString NAME = "Superfan Sports Gloves";

						// Token: 0x0400C148 RID: 49480
						public static LocString DESC = "Orange-striped gloves for enthusiastic athletes.";
					}

					// Token: 0x02003064 RID: 12388
					public class GLOVES_ATHLETIC_LEMON
					{
						// Token: 0x0400C149 RID: 49481
						public static LocString NAME = "Hype Sports Gloves";

						// Token: 0x0400C14A RID: 49482
						public static LocString DESC = "Yellow-striped gloves for athletes who seek to raise the bar.";
					}

					// Token: 0x02003065 RID: 12389
					public class GLOVES_ATHLETIC_KELLYGREEN
					{
						// Token: 0x0400C14B RID: 49483
						public static LocString NAME = "Go Team Sports Gloves";

						// Token: 0x0400C14C RID: 49484
						public static LocString DESC = "Green-striped gloves for the perenially good sport.";
					}

					// Token: 0x02003066 RID: 12390
					public class GLOVES_ATHLETIC_COBALT
					{
						// Token: 0x0400C14D RID: 49485
						public static LocString NAME = "True Blue Sports Gloves";

						// Token: 0x0400C14E RID: 49486
						public static LocString DESC = "Blue-striped gloves perfect for shaking hands after the game.";
					}

					// Token: 0x02003067 RID: 12391
					public class GLOVES_ATHLETIC_FLAMINGO
					{
						// Token: 0x0400C14F RID: 49487
						public static LocString NAME = "Pep Rally Sports Gloves";

						// Token: 0x0400C150 RID: 49488
						public static LocString DESC = "Pink-striped glove designed to withstand countless high-fives.";
					}

					// Token: 0x02003068 RID: 12392
					public class GLOVES_ATHLETIC_CHARCOAL
					{
						// Token: 0x0400C151 RID: 49489
						public static LocString NAME = "Underdog Sports Gloves";

						// Token: 0x0400C152 RID: 49490
						public static LocString DESC = "The muted stripe minimizes distractions so its wearer can focus on trying very, very hard.";
					}
				}
			}

			// Token: 0x02002314 RID: 8980
			public class CLOTHING_TOPS
			{
				// Token: 0x04009A9C RID: 39580
				public static LocString NAME = UI.FormatAsLink("Tops", "CLOTHING_TOPS");

				// Token: 0x04009A9D RID: 39581
				public static LocString GENERICNAME = "Clothing";

				// Token: 0x04009A9E RID: 39582
				public static LocString DESC = "Testing desc for tops skins";

				// Token: 0x04009A9F RID: 39583
				public static LocString EFFECT = "Testing effect for tops skins";

				// Token: 0x04009AA0 RID: 39584
				public static LocString RECIPE_DESC = "Testing recipe desc for tops skins";

				// Token: 0x02002DD1 RID: 11729
				public class FACADES
				{
					// Token: 0x02003069 RID: 12393
					public class BASIC_BLUE_MIDDLE
					{
						// Token: 0x0400C153 RID: 49491
						public static LocString NAME = "Basic Aqua Shirt";

						// Token: 0x0400C154 RID: 49492
						public static LocString DESC = "A nice aqua-blue shirt that goes with everything.";
					}

					// Token: 0x0200306A RID: 12394
					public class BASIC_BLACK
					{
						// Token: 0x0400C155 RID: 49493
						public static LocString NAME = "Basic Black Shirt";

						// Token: 0x0400C156 RID: 49494
						public static LocString DESC = "A nice black shirt that goes with everything.";
					}

					// Token: 0x0200306B RID: 12395
					public class BASIC_PINK_ORCHID
					{
						// Token: 0x0400C157 RID: 49495
						public static LocString NAME = "Basic Bubblegum Shirt";

						// Token: 0x0400C158 RID: 49496
						public static LocString DESC = "A nice bubblegum-pink shirt that goes with everything.";
					}

					// Token: 0x0200306C RID: 12396
					public class BASIC_GREEN
					{
						// Token: 0x0400C159 RID: 49497
						public static LocString NAME = "Basic Green Shirt";

						// Token: 0x0400C15A RID: 49498
						public static LocString DESC = "A nice green shirt that goes with everything.";
					}

					// Token: 0x0200306D RID: 12397
					public class BASIC_ORANGE
					{
						// Token: 0x0400C15B RID: 49499
						public static LocString NAME = "Basic Orange Shirt";

						// Token: 0x0400C15C RID: 49500
						public static LocString DESC = "A nice orange shirt that goes with everything.";
					}

					// Token: 0x0200306E RID: 12398
					public class BASIC_PURPLE
					{
						// Token: 0x0400C15D RID: 49501
						public static LocString NAME = "Basic Purple Shirt";

						// Token: 0x0400C15E RID: 49502
						public static LocString DESC = "A nice purple shirt that goes with everything.";
					}

					// Token: 0x0200306F RID: 12399
					public class BASIC_RED_BURNT
					{
						// Token: 0x0400C15F RID: 49503
						public static LocString NAME = "Basic Red Shirt";

						// Token: 0x0400C160 RID: 49504
						public static LocString DESC = "A nice red shirt that goes with everything.";
					}

					// Token: 0x02003070 RID: 12400
					public class BASIC_WHITE
					{
						// Token: 0x0400C161 RID: 49505
						public static LocString NAME = "Basic White Shirt";

						// Token: 0x0400C162 RID: 49506
						public static LocString DESC = "A nice white shirt that goes with everything.";
					}

					// Token: 0x02003071 RID: 12401
					public class BASIC_YELLOW
					{
						// Token: 0x0400C163 RID: 49507
						public static LocString NAME = "Basic Yellow Shirt";

						// Token: 0x0400C164 RID: 49508
						public static LocString DESC = "A nice yellow shirt that goes with everything.";
					}

					// Token: 0x02003072 RID: 12402
					public class RAGLANTOP_DEEPRED
					{
						// Token: 0x0400C165 RID: 49509
						public static LocString NAME = "Team Captain T-shirt";

						// Token: 0x0400C166 RID: 49510
						public static LocString DESC = "A slightly sweat-stained tee for natural leaders.";
					}

					// Token: 0x02003073 RID: 12403
					public class RAGLANTOP_COBALT
					{
						// Token: 0x0400C167 RID: 49511
						public static LocString NAME = "True Blue T-shirt";

						// Token: 0x0400C168 RID: 49512
						public static LocString DESC = "A slightly sweat-stained tee for the real team players.";
					}

					// Token: 0x02003074 RID: 12404
					public class RAGLANTOP_FLAMINGO
					{
						// Token: 0x0400C169 RID: 49513
						public static LocString NAME = "Pep Rally T-shirt";

						// Token: 0x0400C16A RID: 49514
						public static LocString DESC = "A slightly sweat-stained tee to boost team spirits.";
					}

					// Token: 0x02003075 RID: 12405
					public class RAGLANTOP_KELLYGREEN
					{
						// Token: 0x0400C16B RID: 49515
						public static LocString NAME = "Go Team T-shirt";

						// Token: 0x0400C16C RID: 49516
						public static LocString DESC = "A slightly sweat-stained tee for cheering from the sidelines.";
					}

					// Token: 0x02003076 RID: 12406
					public class RAGLANTOP_CHARCOAL
					{
						// Token: 0x0400C16D RID: 49517
						public static LocString NAME = "Underdog T-shirt";

						// Token: 0x0400C16E RID: 49518
						public static LocString DESC = "For those who don't win a lot.";
					}

					// Token: 0x02003077 RID: 12407
					public class RAGLANTOP_LEMON
					{
						// Token: 0x0400C16F RID: 49519
						public static LocString NAME = "Hype T-shirt";

						// Token: 0x0400C170 RID: 49520
						public static LocString DESC = "A slightly sweat-stained tee to wear when talking a big game.";
					}

					// Token: 0x02003078 RID: 12408
					public class RAGLANTOP_SATSUMA
					{
						// Token: 0x0400C171 RID: 49521
						public static LocString NAME = "Superfan T-shirt";

						// Token: 0x0400C172 RID: 49522
						public static LocString DESC = "A slightly sweat-stained tee for the long-time supporter.";
					}
				}
			}

			// Token: 0x02002315 RID: 8981
			public class CLOTHING_BOTTOMS
			{
				// Token: 0x04009AA1 RID: 39585
				public static LocString NAME = UI.FormatAsLink("Bottoms", "CLOTHING_BOTTOMS");

				// Token: 0x04009AA2 RID: 39586
				public static LocString GENERICNAME = "Clothing";

				// Token: 0x04009AA3 RID: 39587
				public static LocString DESC = "Testing desc for bottoms skins";

				// Token: 0x04009AA4 RID: 39588
				public static LocString EFFECT = "Testing effect for bottoms skins";

				// Token: 0x04009AA5 RID: 39589
				public static LocString RECIPE_DESC = "Testing recipe desc for bottoms skins";

				// Token: 0x02002DD2 RID: 11730
				public class FACADES
				{
					// Token: 0x02003079 RID: 12409
					public class BASIC_BLUE_MIDDLE
					{
						// Token: 0x0400C173 RID: 49523
						public static LocString NAME = "Basic Aqua Pants";

						// Token: 0x0400C174 RID: 49524
						public static LocString DESC = "A clean pair of aqua-blue pants that go with everything.";
					}

					// Token: 0x0200307A RID: 12410
					public class BASIC_PINK_ORCHID
					{
						// Token: 0x0400C175 RID: 49525
						public static LocString NAME = "Basic Bubblegum Pants";

						// Token: 0x0400C176 RID: 49526
						public static LocString DESC = "A clean pair of bubblegum-pink pants that go with everything.";
					}

					// Token: 0x0200307B RID: 12411
					public class BASIC_GREEN
					{
						// Token: 0x0400C177 RID: 49527
						public static LocString NAME = "Basic Green Pants";

						// Token: 0x0400C178 RID: 49528
						public static LocString DESC = "A clean pair of green pants that go with everything.";
					}

					// Token: 0x0200307C RID: 12412
					public class BASIC_ORANGE
					{
						// Token: 0x0400C179 RID: 49529
						public static LocString NAME = "Basic Orange Pants";

						// Token: 0x0400C17A RID: 49530
						public static LocString DESC = "A clean pair of orange pants that go with everything.";
					}

					// Token: 0x0200307D RID: 12413
					public class BASIC_PURPLE
					{
						// Token: 0x0400C17B RID: 49531
						public static LocString NAME = "Basic Purple Pants";

						// Token: 0x0400C17C RID: 49532
						public static LocString DESC = "A clean pair of purple pants that go with everything.";
					}

					// Token: 0x0200307E RID: 12414
					public class BASIC_RED
					{
						// Token: 0x0400C17D RID: 49533
						public static LocString NAME = "Basic Red Pants";

						// Token: 0x0400C17E RID: 49534
						public static LocString DESC = "A clean pair of red pants that go with everything.";
					}

					// Token: 0x0200307F RID: 12415
					public class BASIC_WHITE
					{
						// Token: 0x0400C17F RID: 49535
						public static LocString NAME = "Basic White Pants";

						// Token: 0x0400C180 RID: 49536
						public static LocString DESC = "A clean pair of white pants that go with everything.";
					}

					// Token: 0x02003080 RID: 12416
					public class BASIC_YELLOW
					{
						// Token: 0x0400C181 RID: 49537
						public static LocString NAME = "Basic Yellow Pants";

						// Token: 0x0400C182 RID: 49538
						public static LocString DESC = "A clean pair of yellow pants that go with everything.";
					}

					// Token: 0x02003081 RID: 12417
					public class BASIC_BLACK
					{
						// Token: 0x0400C183 RID: 49539
						public static LocString NAME = "Basic Black Pants";

						// Token: 0x0400C184 RID: 49540
						public static LocString DESC = "A clean pair of black pants that go with everything.";
					}

					// Token: 0x02003082 RID: 12418
					public class SHORTS_BASIC_DEEPRED
					{
						// Token: 0x0400C185 RID: 49541
						public static LocString NAME = "Team Captain Shorts";

						// Token: 0x0400C186 RID: 49542
						public static LocString DESC = "A fresh pair of shorts for natural leaders.";
					}

					// Token: 0x02003083 RID: 12419
					public class SHORTS_BASIC_SATSUMA
					{
						// Token: 0x0400C187 RID: 49543
						public static LocString NAME = "Superfan Shorts";

						// Token: 0x0400C188 RID: 49544
						public static LocString DESC = "A fresh pair of shorts for long-time supporters of...shorts.";
					}

					// Token: 0x02003084 RID: 12420
					public class SHORTS_BASIC_YELLOWCAKE
					{
						// Token: 0x0400C189 RID: 49545
						public static LocString NAME = "Yellowcake Shorts";

						// Token: 0x0400C18A RID: 49546
						public static LocString DESC = "A fresh pair of uranium-powder-colored shorts that are definitely not radioactive. Probably.";
					}

					// Token: 0x02003085 RID: 12421
					public class SHORTS_BASIC_KELLYGREEN
					{
						// Token: 0x0400C18B RID: 49547
						public static LocString NAME = "Go Team Shorts";

						// Token: 0x0400C18C RID: 49548
						public static LocString DESC = "A fresh pair of shorts for cheering from the sidelines.";
					}

					// Token: 0x02003086 RID: 12422
					public class SHORTS_BASIC_BLUE_COBALT
					{
						// Token: 0x0400C18D RID: 49549
						public static LocString NAME = "True Blue Shorts";

						// Token: 0x0400C18E RID: 49550
						public static LocString DESC = "A fresh pair of shorts for the real team players.";
					}

					// Token: 0x02003087 RID: 12423
					public class SHORTS_BASIC_PINK_FLAMINGO
					{
						// Token: 0x0400C18F RID: 49551
						public static LocString NAME = "Pep Rally Shorts";

						// Token: 0x0400C190 RID: 49552
						public static LocString DESC = "The peppiest pair of shorts this side of the asteroid.";
					}

					// Token: 0x02003088 RID: 12424
					public class SHORTS_BASIC_CHARCOAL
					{
						// Token: 0x0400C191 RID: 49553
						public static LocString NAME = "Underdog Shorts";

						// Token: 0x0400C192 RID: 49554
						public static LocString DESC = "A fresh pair of shorts. They're cleaner than they look.";
					}
				}
			}

			// Token: 0x02002316 RID: 8982
			public class CLOTHING_SHOES
			{
				// Token: 0x04009AA6 RID: 39590
				public static LocString NAME = UI.FormatAsLink("Shoes", "CLOTHING_SHOES");

				// Token: 0x04009AA7 RID: 39591
				public static LocString GENERICNAME = "Clothing";

				// Token: 0x04009AA8 RID: 39592
				public static LocString DESC = "Testing desc for shoes skins";

				// Token: 0x04009AA9 RID: 39593
				public static LocString EFFECT = "Testing effect for shoes skins";

				// Token: 0x04009AAA RID: 39594
				public static LocString RECIPE_DESC = "Testing recipe desc for shoes skins";

				// Token: 0x02002DD3 RID: 11731
				public class FACADES
				{
					// Token: 0x02003089 RID: 12425
					public class BASIC_BLUE_MIDDLE
					{
						// Token: 0x0400C193 RID: 49555
						public static LocString NAME = "Basic Aqua Shoes";

						// Token: 0x0400C194 RID: 49556
						public static LocString DESC = "A fresh pair of aqua-blue shoes that go with everything.";
					}

					// Token: 0x0200308A RID: 12426
					public class BASIC_PINK_ORCHID
					{
						// Token: 0x0400C195 RID: 49557
						public static LocString NAME = "Basic Bubblegum Shoes";

						// Token: 0x0400C196 RID: 49558
						public static LocString DESC = "A fresh pair of bubblegum-pink shoes that go with everything.";
					}

					// Token: 0x0200308B RID: 12427
					public class BASIC_GREEN
					{
						// Token: 0x0400C197 RID: 49559
						public static LocString NAME = "Basic Green Shoes";

						// Token: 0x0400C198 RID: 49560
						public static LocString DESC = "A fresh pair of green shoes that go with everything.";
					}

					// Token: 0x0200308C RID: 12428
					public class BASIC_ORANGE
					{
						// Token: 0x0400C199 RID: 49561
						public static LocString NAME = "Basic Orange Shoes";

						// Token: 0x0400C19A RID: 49562
						public static LocString DESC = "A fresh pair of orange shoes that go with everything.";
					}

					// Token: 0x0200308D RID: 12429
					public class BASIC_PURPLE
					{
						// Token: 0x0400C19B RID: 49563
						public static LocString NAME = "Basic Purple Shoes";

						// Token: 0x0400C19C RID: 49564
						public static LocString DESC = "A fresh pair of purple shoes that go with everything.";
					}

					// Token: 0x0200308E RID: 12430
					public class BASIC_RED
					{
						// Token: 0x0400C19D RID: 49565
						public static LocString NAME = "Basic Red Shoes";

						// Token: 0x0400C19E RID: 49566
						public static LocString DESC = "A fresh pair of red shoes that go with everything.";
					}

					// Token: 0x0200308F RID: 12431
					public class BASIC_WHITE
					{
						// Token: 0x0400C19F RID: 49567
						public static LocString NAME = "Basic White Shoes";

						// Token: 0x0400C1A0 RID: 49568
						public static LocString DESC = "A fresh pair of white shoes that go with everything.";
					}

					// Token: 0x02003090 RID: 12432
					public class BASIC_YELLOW
					{
						// Token: 0x0400C1A1 RID: 49569
						public static LocString NAME = "Basic Yellow Shoes";

						// Token: 0x0400C1A2 RID: 49570
						public static LocString DESC = "A fresh pair of yellow shoes that go with everything.";
					}

					// Token: 0x02003091 RID: 12433
					public class BASIC_BLACK
					{
						// Token: 0x0400C1A3 RID: 49571
						public static LocString NAME = "Basic Black Shoes";

						// Token: 0x0400C1A4 RID: 49572
						public static LocString DESC = "A fresh pair of black shoes that go with everything.";
					}

					// Token: 0x02003092 RID: 12434
					public class SOCKS_ATHLETIC_DEEPRED
					{
						// Token: 0x0400C1A5 RID: 49573
						public static LocString NAME = "Team Captain Gym Socks";

						// Token: 0x0400C1A6 RID: 49574
						public static LocString DESC = "Breathable socks with sporty red stripes.";
					}

					// Token: 0x02003093 RID: 12435
					public class SOCKS_ATHLETIC_SATSUMA
					{
						// Token: 0x0400C1A7 RID: 49575
						public static LocString NAME = "Superfan Gym Socks";

						// Token: 0x0400C1A8 RID: 49576
						public static LocString DESC = "Breathable socks with sporty orange stripes.";
					}

					// Token: 0x02003094 RID: 12436
					public class SOCKS_ATHLETIC_LEMON
					{
						// Token: 0x0400C1A9 RID: 49577
						public static LocString NAME = "Hype Gym Socks";

						// Token: 0x0400C1AA RID: 49578
						public static LocString DESC = "Breathable socks with sporty yellow stripes.";
					}

					// Token: 0x02003095 RID: 12437
					public class SOCKS_ATHLETIC_KELLYGREEN
					{
						// Token: 0x0400C1AB RID: 49579
						public static LocString NAME = "Go Team Gym Socks";

						// Token: 0x0400C1AC RID: 49580
						public static LocString DESC = "Breathable socks with sporty green stripes.";
					}

					// Token: 0x02003096 RID: 12438
					public class SOCKS_ATHLETIC_COBALT
					{
						// Token: 0x0400C1AD RID: 49581
						public static LocString NAME = "True Blue Gym Socks";

						// Token: 0x0400C1AE RID: 49582
						public static LocString DESC = "Breathable socks with sporty blue stripes.";
					}

					// Token: 0x02003097 RID: 12439
					public class SOCKS_ATHLETIC_FLAMINGO
					{
						// Token: 0x0400C1AF RID: 49583
						public static LocString NAME = "Pep Rally Gym Socks";

						// Token: 0x0400C1B0 RID: 49584
						public static LocString DESC = "Breathable socks with sporty pink stripes.";
					}

					// Token: 0x02003098 RID: 12440
					public class SOCKS_ATHLETIC_CHARCOAL
					{
						// Token: 0x0400C1B1 RID: 49585
						public static LocString NAME = "Underdog Gym Socks";

						// Token: 0x0400C1B2 RID: 49586
						public static LocString DESC = "Breathable socks that do nothing whatsoever to eliminate foot odor.";
					}
				}
			}

			// Token: 0x02002317 RID: 8983
			public class OXYGEN_TANK
			{
				// Token: 0x04009AAB RID: 39595
				public static LocString NAME = UI.FormatAsLink("Oxygen Tank", "OXYGEN_TANK");

				// Token: 0x04009AAC RID: 39596
				public static LocString GENERICNAME = "Equipment";

				// Token: 0x04009AAD RID: 39597
				public static LocString DESC = "It's like a to-go bag for your lungs.";

				// Token: 0x04009AAE RID: 39598
				public static LocString EFFECT = "Allows Duplicants to breathe in hazardous environments.\n\nDoes not work when submerged in <style=\"liquid\">Liquid</style>.";

				// Token: 0x04009AAF RID: 39599
				public static LocString RECIPE_DESC = "Allows Duplicants to breathe in hazardous environments.\n\nDoes not work when submerged in <style=\"liquid\">Liquid</style>";
			}

			// Token: 0x02002318 RID: 8984
			public class OXYGEN_TANK_UNDERWATER
			{
				// Token: 0x04009AB0 RID: 39600
				public static LocString NAME = "Oxygen Rebreather";

				// Token: 0x04009AB1 RID: 39601
				public static LocString GENERICNAME = "Equipment";

				// Token: 0x04009AB2 RID: 39602
				public static LocString DESC = "";

				// Token: 0x04009AB3 RID: 39603
				public static LocString EFFECT = "Allows Duplicants to breathe while submerged in <style=\"liquid\">Liquid</style>.\n\nDoes not work outside of liquid.";

				// Token: 0x04009AB4 RID: 39604
				public static LocString RECIPE_DESC = "Allows Duplicants to breathe while submerged in <style=\"liquid\">Liquid</style>.\n\nDoes not work outside of liquid";
			}

			// Token: 0x02002319 RID: 8985
			public class EQUIPPABLEBALLOON
			{
				// Token: 0x04009AB5 RID: 39605
				public static LocString NAME = UI.FormatAsLink("Balloon Friend", "EQUIPPABLEBALLOON");

				// Token: 0x04009AB6 RID: 39606
				public static LocString DESC = "A floating friend to reassure my Duplicants they are so very, very clever.";

				// Token: 0x04009AB7 RID: 39607
				public static LocString EFFECT = "Gives Duplicants a boost in brain function.\n\nSupplied by Duplicants with the Balloon Artist " + UI.FormatAsLink("Overjoyed", "MORALE") + " response.";

				// Token: 0x04009AB8 RID: 39608
				public static LocString RECIPE_DESC = "Gives Duplicants a boost in brain function.\n\nSupplied by Duplicants with the Balloon Artist " + UI.FormatAsLink("Overjoyed", "MORALE") + " response";

				// Token: 0x04009AB9 RID: 39609
				public static LocString GENERICNAME = "Balloon Friend";

				// Token: 0x02002DD4 RID: 11732
				public class FACADES
				{
					// Token: 0x02003099 RID: 12441
					public class DEFAULT_BALLOON
					{
						// Token: 0x0400C1B3 RID: 49587
						public static LocString NAME = UI.FormatAsLink("Balloon Friend", "EQUIPPABLEBALLOON");

						// Token: 0x0400C1B4 RID: 49588
						public static LocString DESC = "A floating friend to reassure my Duplicants that they are so very, very clever.";
					}

					// Token: 0x0200309A RID: 12442
					public class BALLOON_FIREENGINE_LONG_SPARKLES
					{
						// Token: 0x0400C1B5 RID: 49589
						public static LocString NAME = UI.FormatAsLink("Magma Glitter", "EQUIPPABLEBALLOON");

						// Token: 0x0400C1B6 RID: 49590
						public static LocString DESC = "They float <i>and</i> sparkle!";
					}

					// Token: 0x0200309B RID: 12443
					public class BALLOON_YELLOW_LONG_SPARKLES
					{
						// Token: 0x0400C1B7 RID: 49591
						public static LocString NAME = UI.FormatAsLink("Lavatory Glitter", "EQUIPPABLEBALLOON");

						// Token: 0x0400C1B8 RID: 49592
						public static LocString DESC = "Sparkly balloons in an all-too-familiar hue.";
					}

					// Token: 0x0200309C RID: 12444
					public class BALLOON_BLUE_LONG_SPARKLES
					{
						// Token: 0x0400C1B9 RID: 49593
						public static LocString NAME = UI.FormatAsLink("Wheezewort Glitter", "EQUIPPABLEBALLOON");

						// Token: 0x0400C1BA RID: 49594
						public static LocString DESC = "They float <i>and</i> sparkle!";
					}

					// Token: 0x0200309D RID: 12445
					public class BALLOON_GREEN_LONG_SPARKLES
					{
						// Token: 0x0400C1BB RID: 49595
						public static LocString NAME = UI.FormatAsLink("Mush Bar Glitter", "EQUIPPABLEBALLOON");

						// Token: 0x0400C1BC RID: 49596
						public static LocString DESC = "They float <i>and</i> sparkle!";
					}

					// Token: 0x0200309E RID: 12446
					public class BALLOON_PINK_LONG_SPARKLES
					{
						// Token: 0x0400C1BD RID: 49597
						public static LocString NAME = UI.FormatAsLink("Petal Glitter", "EQUIPPABLEBALLOON");

						// Token: 0x0400C1BE RID: 49598
						public static LocString DESC = "They float <i>and</i> sparkle!";
					}

					// Token: 0x0200309F RID: 12447
					public class BALLOON_PURPLE_LONG_SPARKLES
					{
						// Token: 0x0400C1BF RID: 49599
						public static LocString NAME = UI.FormatAsLink("Dusky Glitter", "EQUIPPABLEBALLOON");

						// Token: 0x0400C1C0 RID: 49600
						public static LocString DESC = "They float <i>and</i> sparkle!";
					}

					// Token: 0x020030A0 RID: 12448
					public class BALLOON_BABY_PACU_EGG
					{
						// Token: 0x0400C1C1 RID: 49601
						public static LocString NAME = UI.FormatAsLink("Floatie Fish", "EQUIPPABLEBALLOON");

						// Token: 0x0400C1C2 RID: 49602
						public static LocString DESC = "They do not taste as good as the real thing.";
					}

					// Token: 0x020030A1 RID: 12449
					public class BALLOON_BABY_GLOSSY_DRECKO_EGG
					{
						// Token: 0x0400C1C3 RID: 49603
						public static LocString NAME = UI.FormatAsLink("Glossy Glee", "EQUIPPABLEBALLOON");

						// Token: 0x0400C1C4 RID: 49604
						public static LocString DESC = "A happy little trio of inflatable critters.";
					}

					// Token: 0x020030A2 RID: 12450
					public class BALLOON_BABY_HATCH_EGG
					{
						// Token: 0x0400C1C5 RID: 49605
						public static LocString NAME = UI.FormatAsLink("Helium Hatches", "EQUIPPABLEBALLOON");

						// Token: 0x0400C1C6 RID: 49606
						public static LocString DESC = "A happy little trio of inflatable critters.";
					}

					// Token: 0x020030A3 RID: 12451
					public class BALLOON_BABY_POKESHELL_EGG
					{
						// Token: 0x0400C1C7 RID: 49607
						public static LocString NAME = UI.FormatAsLink("Peppy Pokeshells", "EQUIPPABLEBALLOON");

						// Token: 0x0400C1C8 RID: 49608
						public static LocString DESC = "A happy little trio of inflatable critters.";
					}

					// Token: 0x020030A4 RID: 12452
					public class BALLOON_BABY_PUFT_EGG
					{
						// Token: 0x0400C1C9 RID: 49609
						public static LocString NAME = UI.FormatAsLink("Puffed-Up Pufts", "EQUIPPABLEBALLOON");

						// Token: 0x0400C1CA RID: 49610
						public static LocString DESC = "A happy little trio of inflatable critters.";
					}

					// Token: 0x020030A5 RID: 12453
					public class BALLOON_BABY_SHOVOLE_EGG
					{
						// Token: 0x0400C1CB RID: 49611
						public static LocString NAME = UI.FormatAsLink("Voley Voley Voles", "EQUIPPABLEBALLOON");

						// Token: 0x0400C1CC RID: 49612
						public static LocString DESC = "A happy little trio of inflatable critters.";
					}

					// Token: 0x020030A6 RID: 12454
					public class BALLOON_BABY_PIP_EGG
					{
						// Token: 0x0400C1CD RID: 49613
						public static LocString NAME = UI.FormatAsLink("Pip Pip Hooray", "EQUIPPABLEBALLOON");

						// Token: 0x0400C1CE RID: 49614
						public static LocString DESC = "A happy little trio of inflatable critters.";
					}
				}
			}

			// Token: 0x0200231A RID: 8986
			public class SLEEPCLINICPAJAMAS
			{
				// Token: 0x04009ABA RID: 39610
				public static LocString NAME = UI.FormatAsLink("Pajamas", "SLEEP_CLINIC_PAJAMAS");

				// Token: 0x04009ABB RID: 39611
				public static LocString GENERICNAME = "Clothing";

				// Token: 0x04009ABC RID: 39612
				public static LocString DESC = "A soft, fleecy ticket to dreamland.";

				// Token: 0x04009ABD RID: 39613
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Helps Duplicants fall asleep by reducing ",
					UI.FormatAsLink("Stamina", "STAMINA"),
					".\n\nEnables the wearer to dream and produce ",
					UI.FormatAsLink("Dream Journals", "DREAMJOURNAL"),
					"."
				});

				// Token: 0x04009ABE RID: 39614
				public static LocString DESTROY_TOAST = "Ripped Pajamas";
			}
		}
	}
}
