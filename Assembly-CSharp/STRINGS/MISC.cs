using System;

namespace STRINGS
{
	// Token: 0x02000D48 RID: 3400
	public class MISC
	{
		// Token: 0x02001DAA RID: 7594
		public class TAGS
		{
			// Token: 0x040085C6 RID: 34246
			public static LocString OTHER = "Miscellaneous";

			// Token: 0x040085C7 RID: 34247
			public static LocString FILTER = UI.FormatAsLink("Filtration Medium", "FILTER");

			// Token: 0x040085C8 RID: 34248
			public static LocString FILTER_DESC = string.Concat(new string[]
			{
				"Filtration Mediums are materials which are supplied to some filtration buildings that are used in separating purified ",
				UI.FormatAsLink("gases", "ELEMENTS_GASSES"),
				" or ",
				UI.FormatAsLink("liquids", "ELEMENTS_LIQUID"),
				" from their polluted forms.\n\nExamples include filtering ",
				UI.FormatAsLink("Water", "WATER"),
				" from ",
				UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
				" using a ",
				UI.FormatAsLink("Water Sieve", "WATERPURIFIER"),
				", or a ",
				UI.FormatAsLink("Deodorizer", "AIRFILTER"),
				" purifying ",
				UI.FormatAsLink("Oxygen", "OXYGEN"),
				" from ",
				UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN"),
				".\n\nFiltration Mediums are a consumable that will be transformed by the filtering process to generate a by-product, like when ",
				UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
				" is the result after ",
				UI.FormatAsLink("Sand", "SAND"),
				" has been used to filter polluted water. The filtering building will cease to function once the filtering material has been consumed. Once the Filtering Material has been resupplied to the filtering building it will start working again."
			});

			// Token: 0x040085C9 RID: 34249
			public static LocString ICEORE = UI.FormatAsLink("Ice", "ICEORE");

			// Token: 0x040085CA RID: 34250
			public static LocString ICEORE_DESC = string.Concat(new string[]
			{
				"Ice is a class of materials made up mostly (if not completely) of ",
				UI.FormatAsLink("Water", "WATER"),
				" in a frozen or partially frozen form.\n\nAs a material in a frigid solid or semi-solid state, these elements are very useful as a low-cost way to cool the environment around them.\n\nWhen heated, ice will melt into its original liquified form (ie.",
				UI.FormatAsLink("Brine Ice", "BRINEICE"),
				" will liquify into ",
				UI.FormatAsLink("Brine", "BRINE"),
				"). Each ice element has a different freezing and melting point based upon its composition and state."
			});

			// Token: 0x040085CB RID: 34251
			public static LocString PHOSPHORUS = "Phosphorus";

			// Token: 0x040085CC RID: 34252
			public static LocString BUILDABLERAW = "Raw Mineral";

			// Token: 0x040085CD RID: 34253
			public static LocString BUILDABLEPROCESSED = "Refined Mineral";

			// Token: 0x040085CE RID: 34254
			public static LocString BUILDABLEANY = "Generic Buildable";

			// Token: 0x040085CF RID: 34255
			public static LocString REFINEDMETAL = UI.FormatAsLink("Refined Metal", "REFINEDMETAL");

			// Token: 0x040085D0 RID: 34256
			public static LocString REFINEDMETAL_DESC = string.Concat(new string[]
			{
				"Refined metals are purified forms of metal often used in higher-tier electronics due to their tendency to be able to withstand higher temperatures when they are made into wires. Other benefits include the increased decor value for some metals which can greatly improve the well-being of a colony.\n\nMetal ore can be refined in either the ",
				UI.FormatAsLink("Rock Crusher", "ROCKCRUSHER"),
				" or the ",
				UI.FormatAsLink("Metal Refinery", "METALREFINERY"),
				"."
			});

			// Token: 0x040085D1 RID: 34257
			public static LocString METAL = "Metal Ore";

			// Token: 0x040085D2 RID: 34258
			public static LocString PRECIOUSMETAL = "Precious Metal";

			// Token: 0x040085D3 RID: 34259
			public static LocString RAWPRECIOUSMETAL = "Precious Metal Ore";

			// Token: 0x040085D4 RID: 34260
			public static LocString PRECIOUSROCK = "Precious Rock";

			// Token: 0x040085D5 RID: 34261
			public static LocString ALLOY = "Alloy";

			// Token: 0x040085D6 RID: 34262
			public static LocString BUILDINGFIBER = "Fiber";

			// Token: 0x040085D7 RID: 34263
			public static LocString BUILDINGWOOD = "Wood";

			// Token: 0x040085D8 RID: 34264
			public static LocString CRUSHABLE = "Crushable";

			// Token: 0x040085D9 RID: 34265
			public static LocString CROPSEEDS = "Crop Seeds";

			// Token: 0x040085DA RID: 34266
			public static LocString BAGABLECREATURE = "Critter";

			// Token: 0x040085DB RID: 34267
			public static LocString SWIMMINGCREATURE = "Aquatic Critter";

			// Token: 0x040085DC RID: 34268
			public static LocString LIFE = "Life";

			// Token: 0x040085DD RID: 34269
			public static LocString LIQUIFIABLE = "Liquefiable";

			// Token: 0x040085DE RID: 34270
			public static LocString LIQUID = "Liquid";

			// Token: 0x040085DF RID: 34271
			public static LocString SPECIAL = "Special";

			// Token: 0x040085E0 RID: 34272
			public static LocString FARMABLE = "Cultivable Soil";

			// Token: 0x040085E1 RID: 34273
			public static LocString AGRICULTURE = "Agriculture";

			// Token: 0x040085E2 RID: 34274
			public static LocString COAL = "Coal";

			// Token: 0x040085E3 RID: 34275
			public static LocString BLEACHSTONE = "Bleach Stone";

			// Token: 0x040085E4 RID: 34276
			public static LocString ORGANICS = "Organic";

			// Token: 0x040085E5 RID: 34277
			public static LocString CONSUMABLEORE = "Consumable Ore";

			// Token: 0x040085E6 RID: 34278
			public static LocString ORE = "Ore";

			// Token: 0x040085E7 RID: 34279
			public static LocString BREATHABLE = "Breathable Gas";

			// Token: 0x040085E8 RID: 34280
			public static LocString UNBREATHABLE = "Unbreathable Gas";

			// Token: 0x040085E9 RID: 34281
			public static LocString GAS = "Gas";

			// Token: 0x040085EA RID: 34282
			public static LocString BURNS = "Flammable";

			// Token: 0x040085EB RID: 34283
			public static LocString UNSTABLE = "Unstable";

			// Token: 0x040085EC RID: 34284
			public static LocString TOXIC = "Toxic";

			// Token: 0x040085ED RID: 34285
			public static LocString MIXTURE = "Mixture";

			// Token: 0x040085EE RID: 34286
			public static LocString SOLID = "Solid";

			// Token: 0x040085EF RID: 34287
			public static LocString FLYINGCRITTEREDIBLE = "Bait";

			// Token: 0x040085F0 RID: 34288
			public static LocString INDUSTRIALPRODUCT = "Industrial Product";

			// Token: 0x040085F1 RID: 34289
			public static LocString INDUSTRIALINGREDIENT = "Industrial Ingredient";

			// Token: 0x040085F2 RID: 34290
			public static LocString MEDICALSUPPLIES = "Medical Supplies";

			// Token: 0x040085F3 RID: 34291
			public static LocString CLOTHES = "Clothing";

			// Token: 0x040085F4 RID: 34292
			public static LocString EMITSLIGHT = "Light Emitter";

			// Token: 0x040085F5 RID: 34293
			public static LocString BED = "Bed";

			// Token: 0x040085F6 RID: 34294
			public static LocString MESSSTATION = "Dining Table";

			// Token: 0x040085F7 RID: 34295
			public static LocString TOY = "Toy";

			// Token: 0x040085F8 RID: 34296
			public static LocString SUIT = "Suit";

			// Token: 0x040085F9 RID: 34297
			public static LocString MULTITOOL = "Multitool";

			// Token: 0x040085FA RID: 34298
			public static LocString CLINIC = "Clinic";

			// Token: 0x040085FB RID: 34299
			public static LocString RELAXATION_POINT = "Leisure Area";

			// Token: 0x040085FC RID: 34300
			public static LocString SOLIDMATERIAL = "Solid Material";

			// Token: 0x040085FD RID: 34301
			public static LocString EXTRUDABLE = "Extrudable";

			// Token: 0x040085FE RID: 34302
			public static LocString PLUMBABLE = "Plumbable";

			// Token: 0x040085FF RID: 34303
			public static LocString COMPOSTABLE = UI.FormatAsLink("Compostable", "COMPOSTABLE");

			// Token: 0x04008600 RID: 34304
			public static LocString COMPOSTABLE_DESC = string.Concat(new string[]
			{
				"Compostables are biological materials which can be put into a ",
				UI.FormatAsLink("Compost", "COMPOST"),
				" to generate clean ",
				UI.FormatAsLink("Dirt", "DIRT"),
				".\n\nComposting also generates a small amount of ",
				UI.FormatAsLink("Heat", "HEAT"),
				".\n\nOnce it starts to rot, consumable food should be composted to prevent ",
				UI.FormatAsLink("Food Poisoning", "FOODSICKNESS"),
				"."
			});

			// Token: 0x04008601 RID: 34305
			public static LocString COMPOSTBASICPLANTFOOD = "Compost Muckroot";

			// Token: 0x04008602 RID: 34306
			public static LocString EDIBLE = "Edible";

			// Token: 0x04008603 RID: 34307
			public static LocString OXIDIZER = "Oxidizer";

			// Token: 0x04008604 RID: 34308
			public static LocString COOKINGINGREDIENT = "Cooking Ingredient";

			// Token: 0x04008605 RID: 34309
			public static LocString MEDICINE = "Medicine";

			// Token: 0x04008606 RID: 34310
			public static LocString SEED = "Seed";

			// Token: 0x04008607 RID: 34311
			public static LocString ANYWATER = "Water Based";

			// Token: 0x04008608 RID: 34312
			public static LocString MARKEDFORCOMPOST = "Marked For Compost";

			// Token: 0x04008609 RID: 34313
			public static LocString MARKEDFORCOMPOSTINSTORAGE = "In Compost Storage";

			// Token: 0x0400860A RID: 34314
			public static LocString COMPOSTMEAT = "Compost Meat";

			// Token: 0x0400860B RID: 34315
			public static LocString PICKLED = "Pickled";

			// Token: 0x0400860C RID: 34316
			public static LocString PLASTIC = "Plastic";

			// Token: 0x0400860D RID: 34317
			public static LocString TOILET = "Toilet";

			// Token: 0x0400860E RID: 34318
			public static LocString MASSAGE_TABLE = "Massage Table";

			// Token: 0x0400860F RID: 34319
			public static LocString POWERSTATION = "Power Station";

			// Token: 0x04008610 RID: 34320
			public static LocString FARMSTATION = "Farm Station";

			// Token: 0x04008611 RID: 34321
			public static LocString MACHINE_SHOP = "Machine Shop";

			// Token: 0x04008612 RID: 34322
			public static LocString ANTISEPTIC = "Antiseptic";

			// Token: 0x04008613 RID: 34323
			public static LocString OIL = "Hydrocarbon";

			// Token: 0x04008614 RID: 34324
			public static LocString DECORATION = "Decoration";

			// Token: 0x04008615 RID: 34325
			public static LocString EGG = "Critter Egg";

			// Token: 0x04008616 RID: 34326
			public static LocString EGGSHELL = "Egg Shell";

			// Token: 0x04008617 RID: 34327
			public static LocString MANUFACTUREDMATERIAL = "Manufactured Material";

			// Token: 0x04008618 RID: 34328
			public static LocString STEEL = "Steel";

			// Token: 0x04008619 RID: 34329
			public static LocString RAW = "Raw Animal Product";

			// Token: 0x0400861A RID: 34330
			public static LocString ANY = "Any";

			// Token: 0x0400861B RID: 34331
			public static LocString TRANSPARENT = "Transparent";

			// Token: 0x0400861C RID: 34332
			public static LocString RAREMATERIALS = "Rare Resource";

			// Token: 0x0400861D RID: 34333
			public static LocString FARMINGMATERIAL = "Fertilizer";

			// Token: 0x0400861E RID: 34334
			public static LocString INSULATOR = "Insulator";

			// Token: 0x0400861F RID: 34335
			public static LocString RAILGUNPAYLOADEMPTYABLE = "Payload";

			// Token: 0x04008620 RID: 34336
			public static LocString NONCRUSHABLE = "Uncrushable";

			// Token: 0x04008621 RID: 34337
			public static LocString STORYTRAITRESOURCE = "Story Trait";

			// Token: 0x04008622 RID: 34338
			public static LocString COMMAND_MODULE = "Command Module";

			// Token: 0x04008623 RID: 34339
			public static LocString HABITAT_MODULE = "Habitat Module";

			// Token: 0x04008624 RID: 34340
			public static LocString COMBUSTIBLEGAS = "Combustible Gas";

			// Token: 0x04008625 RID: 34341
			public static LocString COMBUSTIBLELIQUID = UI.FormatAsLink("Combustible Liquid", "COMBUSTIBLELIQUID");

			// Token: 0x04008626 RID: 34342
			public static LocString COMBUSTIBLELIQUID_DESC = string.Concat(new string[]
			{
				"Combustible Liquids are liquids that can be burned as fuel to be used in energy production such as in a ",
				UI.FormatAsLink("Petroleum Generator", "PETROLEUMGENERATOR"),
				" or a ",
				UI.FormatAsLink("Petroleum Engine", "KEROSENEENGINE"),
				".\n\nThough these liquids have other uses, such as fertilizer for growing a ",
				UI.FormatAsLink("Nosh Bean", "BEANPLANTSEED"),
				", their primary usefulness lies in their ability to be burned for ",
				UI.FormatAsLink("power", "POWER"),
				"."
			});

			// Token: 0x04008627 RID: 34343
			public static LocString COMBUSTIBLESOLID = "Combustible Solid";

			// Token: 0x04008628 RID: 34344
			public static LocString UNIDENTIFIEDSEED = "Seed (Unidentified Mutation)";

			// Token: 0x04008629 RID: 34345
			public static LocString CHARMEDARTIFACT = "Artifact of Interest";

			// Token: 0x0400862A RID: 34346
			public static LocString GENE_SHUFFLER = "Neural Vacillator";

			// Token: 0x0400862B RID: 34347
			public static LocString WARP_PORTAL = "Teleportal";

			// Token: 0x0400862C RID: 34348
			public static LocString FARMING = "Farm Build-Delivery";

			// Token: 0x0400862D RID: 34349
			public static LocString RESEARCH = "Research Delivery";

			// Token: 0x0400862E RID: 34350
			public static LocString POWER = "Generator Delivery";

			// Token: 0x0400862F RID: 34351
			public static LocString BUILDING = "Build Dig-Delivery";

			// Token: 0x04008630 RID: 34352
			public static LocString COOKING = "Cook Delivery";

			// Token: 0x04008631 RID: 34353
			public static LocString FABRICATING = "Fabricate Delivery";

			// Token: 0x04008632 RID: 34354
			public static LocString WIRING = "Wire Build-Delivery";

			// Token: 0x04008633 RID: 34355
			public static LocString ART = "Art Build-Delivery";

			// Token: 0x04008634 RID: 34356
			public static LocString DOCTORING = "Treatment Delivery";

			// Token: 0x04008635 RID: 34357
			public static LocString CONVEYOR = "Shipping Build";

			// Token: 0x04008636 RID: 34358
			public static LocString COMPOST_FORMAT = "{Item}";

			// Token: 0x04008637 RID: 34359
			public static LocString ADVANCEDDOCTORSTATIONMEDICALSUPPLIES = "Serum Vial";

			// Token: 0x04008638 RID: 34360
			public static LocString DOCTORSTATIONMEDICALSUPPLIES = "Medical Pack";
		}

		// Token: 0x02001DAB RID: 7595
		public class STATUSITEMS
		{
			// Token: 0x02002CDF RID: 11487
			public class ATTENTIONREQUIRED
			{
				// Token: 0x0400B788 RID: 46984
				public static LocString NAME = "Attention Required!";

				// Token: 0x0400B789 RID: 46985
				public static LocString TOOLTIP = "Something in my colony needs to be attended to";
			}

			// Token: 0x02002CE0 RID: 11488
			public class SUBLIMATIONBLOCKED
			{
				// Token: 0x0400B78A RID: 46986
				public static LocString NAME = "{SubElement} emission blocked";

				// Token: 0x0400B78B RID: 46987
				public static LocString TOOLTIP = "This {Element} deposit is not exposed to air and cannot emit {SubElement}";
			}

			// Token: 0x02002CE1 RID: 11489
			public class SUBLIMATIONOVERPRESSURE
			{
				// Token: 0x0400B78C RID: 46988
				public static LocString NAME = "Inert";

				// Token: 0x0400B78D RID: 46989
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Environmental ",
					UI.PRE_KEYWORD,
					"Gas Pressure",
					UI.PST_KEYWORD,
					" is too high for this {Element} deposit to emit {SubElement}"
				});
			}

			// Token: 0x02002CE2 RID: 11490
			public class SUBLIMATIONEMITTING
			{
				// Token: 0x0400B78E RID: 46990
				public static LocString NAME = BUILDING.STATUSITEMS.EMITTINGGASAVG.NAME;

				// Token: 0x0400B78F RID: 46991
				public static LocString TOOLTIP = BUILDING.STATUSITEMS.EMITTINGGASAVG.TOOLTIP;
			}

			// Token: 0x02002CE3 RID: 11491
			public class SPACE
			{
				// Token: 0x0400B790 RID: 46992
				public static LocString NAME = "Space exposure";

				// Token: 0x0400B791 RID: 46993
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This region is exposed to the vacuum of space and will result in the loss of ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" and ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" resources"
				});
			}

			// Token: 0x02002CE4 RID: 11492
			public class EDIBLE
			{
				// Token: 0x0400B792 RID: 46994
				public static LocString NAME = "Rations: {0}";

				// Token: 0x0400B793 RID: 46995
				public static LocString TOOLTIP = "Can provide " + UI.FormatAsLink("{0}", "KCAL") + " of energy to Duplicants";
			}

			// Token: 0x02002CE5 RID: 11493
			public class MARKEDFORDISINFECTION
			{
				// Token: 0x0400B794 RID: 46996
				public static LocString NAME = "Disinfect Errand";

				// Token: 0x0400B795 RID: 46997
				public static LocString TOOLTIP = "Building will be disinfected once a Duplicant is available";
			}

			// Token: 0x02002CE6 RID: 11494
			public class PENDINGCLEAR
			{
				// Token: 0x0400B796 RID: 46998
				public static LocString NAME = "Sweep Errand";

				// Token: 0x0400B797 RID: 46999
				public static LocString TOOLTIP = "Debris will be swept once a Duplicant is available";
			}

			// Token: 0x02002CE7 RID: 11495
			public class PENDINGCLEARNOSTORAGE
			{
				// Token: 0x0400B798 RID: 47000
				public static LocString NAME = "Storage Unavailable";

				// Token: 0x0400B799 RID: 47001
				public static LocString TOOLTIP = "No available " + BUILDINGS.PREFABS.STORAGELOCKER.NAME + " can accept this item\n\nMake sure the filter on your storage is correctly set and there is sufficient space remaining";
			}

			// Token: 0x02002CE8 RID: 11496
			public class MARKEDFORCOMPOST
			{
				// Token: 0x0400B79A RID: 47002
				public static LocString NAME = "Compost Errand";

				// Token: 0x0400B79B RID: 47003
				public static LocString TOOLTIP = "Object is marked and will be moved to " + BUILDINGS.PREFABS.COMPOST.NAME + " once a Duplicant is available";
			}

			// Token: 0x02002CE9 RID: 11497
			public class NOCLEARLOCATIONSAVAILABLE
			{
				// Token: 0x0400B79C RID: 47004
				public static LocString NAME = "No Sweep Destination";

				// Token: 0x0400B79D RID: 47005
				public static LocString TOOLTIP = "There are no valid destinations for this object to be swept to";
			}

			// Token: 0x02002CEA RID: 11498
			public class PENDINGHARVEST
			{
				// Token: 0x0400B79E RID: 47006
				public static LocString NAME = "Harvest Errand";

				// Token: 0x0400B79F RID: 47007
				public static LocString TOOLTIP = "Plant will be harvested once a Duplicant is available";
			}

			// Token: 0x02002CEB RID: 11499
			public class PENDINGUPROOT
			{
				// Token: 0x0400B7A0 RID: 47008
				public static LocString NAME = "Uproot Errand";

				// Token: 0x0400B7A1 RID: 47009
				public static LocString TOOLTIP = "Plant will be uprooted once a Duplicant is available";
			}

			// Token: 0x02002CEC RID: 11500
			public class WAITINGFORDIG
			{
				// Token: 0x0400B7A2 RID: 47010
				public static LocString NAME = "Dig Errand";

				// Token: 0x0400B7A3 RID: 47011
				public static LocString TOOLTIP = "Tile will be dug out once a Duplicant is available";
			}

			// Token: 0x02002CED RID: 11501
			public class WAITINGFORMOP
			{
				// Token: 0x0400B7A4 RID: 47012
				public static LocString NAME = "Mop Errand";

				// Token: 0x0400B7A5 RID: 47013
				public static LocString TOOLTIP = "Spill will be mopped once a Duplicant is available";
			}

			// Token: 0x02002CEE RID: 11502
			public class NOTMARKEDFORHARVEST
			{
				// Token: 0x0400B7A6 RID: 47014
				public static LocString NAME = "No Harvest Pending";

				// Token: 0x0400B7A7 RID: 47015
				public static LocString TOOLTIP = "Use the " + UI.FormatAsTool("Harvest Tool", global::Action.Harvest) + " to mark this plant for harvest";
			}

			// Token: 0x02002CEF RID: 11503
			public class GROWINGBRANCHES
			{
				// Token: 0x0400B7A8 RID: 47016
				public static LocString NAME = "Growing Branches";

				// Token: 0x0400B7A9 RID: 47017
				public static LocString TOOLTIP = "This tree is working hard to grow new branches right now";
			}

			// Token: 0x02002CF0 RID: 11504
			public class ELEMENTALCATEGORY
			{
				// Token: 0x0400B7AA RID: 47018
				public static LocString NAME = "{Category}";

				// Token: 0x0400B7AB RID: 47019
				public static LocString TOOLTIP = "The selected object belongs to the <b>{Category}</b> resource category";
			}

			// Token: 0x02002CF1 RID: 11505
			public class ELEMENTALMASS
			{
				// Token: 0x0400B7AC RID: 47020
				public static LocString NAME = "{Mass}";

				// Token: 0x0400B7AD RID: 47021
				public static LocString TOOLTIP = "The selected object has a mass of <b>{Mass}</b>";
			}

			// Token: 0x02002CF2 RID: 11506
			public class ELEMENTALDISEASE
			{
				// Token: 0x0400B7AE RID: 47022
				public static LocString NAME = "{Disease}";

				// Token: 0x0400B7AF RID: 47023
				public static LocString TOOLTIP = "Current disease: {Disease}";
			}

			// Token: 0x02002CF3 RID: 11507
			public class ELEMENTALTEMPERATURE
			{
				// Token: 0x0400B7B0 RID: 47024
				public static LocString NAME = "{Temp}";

				// Token: 0x0400B7B1 RID: 47025
				public static LocString TOOLTIP = "The selected object is currently <b>{Temp}</b>";
			}

			// Token: 0x02002CF4 RID: 11508
			public class MARKEDFORCOMPOSTINSTORAGE
			{
				// Token: 0x0400B7B2 RID: 47026
				public static LocString NAME = "Composted";

				// Token: 0x0400B7B3 RID: 47027
				public static LocString TOOLTIP = "The selected object is currently in the compost";
			}

			// Token: 0x02002CF5 RID: 11509
			public class BURIEDITEM
			{
				// Token: 0x0400B7B4 RID: 47028
				public static LocString NAME = "Buried Object";

				// Token: 0x0400B7B5 RID: 47029
				public static LocString TOOLTIP = "Something seems to be hidden here";

				// Token: 0x0400B7B6 RID: 47030
				public static LocString NOTIFICATION = "Buried object discovered";

				// Token: 0x0400B7B7 RID: 47031
				public static LocString NOTIFICATION_TOOLTIP = "My Duplicants have uncovered a {Uncoverable}!\n\n" + UI.CLICK(UI.ClickType.Click) + " to jump to its location.";
			}

			// Token: 0x02002CF6 RID: 11510
			public class GENETICANALYSISCOMPLETED
			{
				// Token: 0x0400B7B8 RID: 47032
				public static LocString NAME = "Genome Sequenced";

				// Token: 0x0400B7B9 RID: 47033
				public static LocString TOOLTIP = "This Station has sequenced a new seed mutation";
			}

			// Token: 0x02002CF7 RID: 11511
			public class HEALTHSTATUS
			{
				// Token: 0x02002FD3 RID: 12243
				public class PERFECT
				{
					// Token: 0x0400BF49 RID: 48969
					public static LocString NAME = "None";

					// Token: 0x0400BF4A RID: 48970
					public static LocString TOOLTIP = "This Duplicant is in peak condition";
				}

				// Token: 0x02002FD4 RID: 12244
				public class ALRIGHT
				{
					// Token: 0x0400BF4B RID: 48971
					public static LocString NAME = "None";

					// Token: 0x0400BF4C RID: 48972
					public static LocString TOOLTIP = "This Duplicant is none the worse for wear";
				}

				// Token: 0x02002FD5 RID: 12245
				public class SCUFFED
				{
					// Token: 0x0400BF4D RID: 48973
					public static LocString NAME = "Minor";

					// Token: 0x0400BF4E RID: 48974
					public static LocString TOOLTIP = "This Duplicant has a few scrapes and bruises";
				}

				// Token: 0x02002FD6 RID: 12246
				public class INJURED
				{
					// Token: 0x0400BF4F RID: 48975
					public static LocString NAME = "Moderate";

					// Token: 0x0400BF50 RID: 48976
					public static LocString TOOLTIP = "This Duplicant needs some patching up";
				}

				// Token: 0x02002FD7 RID: 12247
				public class CRITICAL
				{
					// Token: 0x0400BF51 RID: 48977
					public static LocString NAME = "Severe";

					// Token: 0x0400BF52 RID: 48978
					public static LocString TOOLTIP = "This Duplicant is in serious need of medical attention";
				}

				// Token: 0x02002FD8 RID: 12248
				public class INCAPACITATED
				{
					// Token: 0x0400BF53 RID: 48979
					public static LocString NAME = "Paralyzing";

					// Token: 0x0400BF54 RID: 48980
					public static LocString TOOLTIP = "This Duplicant will die if they do not receive medical attention";
				}

				// Token: 0x02002FD9 RID: 12249
				public class DEAD
				{
					// Token: 0x0400BF55 RID: 48981
					public static LocString NAME = "Conclusive";

					// Token: 0x0400BF56 RID: 48982
					public static LocString TOOLTIP = "This Duplicant won't be getting back up";
				}
			}

			// Token: 0x02002CF8 RID: 11512
			public class HIT
			{
				// Token: 0x0400B7BA RID: 47034
				public static LocString NAME = "{targetName} took {damageAmount} damage from {attackerName}'s attack!";
			}

			// Token: 0x02002CF9 RID: 11513
			public class OREMASS
			{
				// Token: 0x0400B7BB RID: 47035
				public static LocString NAME = MISC.STATUSITEMS.ELEMENTALMASS.NAME;

				// Token: 0x0400B7BC RID: 47036
				public static LocString TOOLTIP = MISC.STATUSITEMS.ELEMENTALMASS.TOOLTIP;
			}

			// Token: 0x02002CFA RID: 11514
			public class ORETEMP
			{
				// Token: 0x0400B7BD RID: 47037
				public static LocString NAME = MISC.STATUSITEMS.ELEMENTALTEMPERATURE.NAME;

				// Token: 0x0400B7BE RID: 47038
				public static LocString TOOLTIP = MISC.STATUSITEMS.ELEMENTALTEMPERATURE.TOOLTIP;
			}

			// Token: 0x02002CFB RID: 11515
			public class TREEFILTERABLETAGS
			{
				// Token: 0x0400B7BF RID: 47039
				public static LocString NAME = "{Tags}";

				// Token: 0x0400B7C0 RID: 47040
				public static LocString TOOLTIP = "{Tags}";
			}

			// Token: 0x02002CFC RID: 11516
			public class SPOUTOVERPRESSURE
			{
				// Token: 0x0400B7C1 RID: 47041
				public static LocString NAME = "Overpressure {StudiedDetails}";

				// Token: 0x0400B7C2 RID: 47042
				public static LocString TOOLTIP = "Spout cannot vent due to high environmental pressure";

				// Token: 0x0400B7C3 RID: 47043
				public static LocString STUDIED = "(idle in <b>{Time}</b>)";
			}

			// Token: 0x02002CFD RID: 11517
			public class SPOUTEMITTING
			{
				// Token: 0x0400B7C4 RID: 47044
				public static LocString NAME = "Venting {StudiedDetails}";

				// Token: 0x0400B7C5 RID: 47045
				public static LocString TOOLTIP = "This geyser is erupting";

				// Token: 0x0400B7C6 RID: 47046
				public static LocString STUDIED = "(idle in <b>{Time}</b>)";
			}

			// Token: 0x02002CFE RID: 11518
			public class SPOUTPRESSUREBUILDING
			{
				// Token: 0x0400B7C7 RID: 47047
				public static LocString NAME = "Rising pressure {StudiedDetails}";

				// Token: 0x0400B7C8 RID: 47048
				public static LocString TOOLTIP = "This geyser's internal pressure is steadily building";

				// Token: 0x0400B7C9 RID: 47049
				public static LocString STUDIED = "(erupts in <b>{Time}</b>)";
			}

			// Token: 0x02002CFF RID: 11519
			public class SPOUTIDLE
			{
				// Token: 0x0400B7CA RID: 47050
				public static LocString NAME = "Idle {StudiedDetails}";

				// Token: 0x0400B7CB RID: 47051
				public static LocString TOOLTIP = "This geyser is not currently erupting";

				// Token: 0x0400B7CC RID: 47052
				public static LocString STUDIED = "(erupts in <b>{Time}</b>)";
			}

			// Token: 0x02002D00 RID: 11520
			public class SPOUTDORMANT
			{
				// Token: 0x0400B7CD RID: 47053
				public static LocString NAME = "Dormant";

				// Token: 0x0400B7CE RID: 47054
				public static LocString TOOLTIP = "This geyser's geoactivity has halted\n\nIt won't erupt again for some time";
			}

			// Token: 0x02002D01 RID: 11521
			public class SPICEDFOOD
			{
				// Token: 0x0400B7CF RID: 47055
				public static LocString NAME = "Seasoned";

				// Token: 0x0400B7D0 RID: 47056
				public static LocString TOOLTIP = "This food has been improved with spice from the " + BUILDINGS.PREFABS.SPICEGRINDER.NAME;
			}

			// Token: 0x02002D02 RID: 11522
			public class PICKUPABLEUNREACHABLE
			{
				// Token: 0x0400B7D1 RID: 47057
				public static LocString NAME = "Unreachable";

				// Token: 0x0400B7D2 RID: 47058
				public static LocString TOOLTIP = "Duplicants cannot reach this object";
			}

			// Token: 0x02002D03 RID: 11523
			public class PRIORITIZED
			{
				// Token: 0x0400B7D3 RID: 47059
				public static LocString NAME = "High Priority";

				// Token: 0x0400B7D4 RID: 47060
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Errand",
					UI.PST_KEYWORD,
					" has been marked as important and will be preferred over other pending ",
					UI.PRE_KEYWORD,
					"Errands",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002D04 RID: 11524
			public class USING
			{
				// Token: 0x0400B7D5 RID: 47061
				public static LocString NAME = "Using {Target}";

				// Token: 0x0400B7D6 RID: 47062
				public static LocString TOOLTIP = "{Target} is currently in use";
			}

			// Token: 0x02002D05 RID: 11525
			public class ORDERATTACK
			{
				// Token: 0x0400B7D7 RID: 47063
				public static LocString NAME = "Pending Attack";

				// Token: 0x0400B7D8 RID: 47064
				public static LocString TOOLTIP = "Waiting for a Duplicant to murderize this defenseless " + UI.PRE_KEYWORD + "Critter" + UI.PST_KEYWORD;
			}

			// Token: 0x02002D06 RID: 11526
			public class ORDERCAPTURE
			{
				// Token: 0x0400B7D9 RID: 47065
				public static LocString NAME = "Pending Wrangle";

				// Token: 0x0400B7DA RID: 47066
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Waiting for a Duplicant to capture this ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					"\n\nOnly Duplicants with the ",
					DUPLICANTS.ROLES.RANCHER.NAME,
					" Skill can catch critters without traps"
				});
			}

			// Token: 0x02002D07 RID: 11527
			public class OPERATING
			{
				// Token: 0x0400B7DB RID: 47067
				public static LocString NAME = "In Use";

				// Token: 0x0400B7DC RID: 47068
				public static LocString TOOLTIP = "This object is currently being used";
			}

			// Token: 0x02002D08 RID: 11528
			public class CLEANING
			{
				// Token: 0x0400B7DD RID: 47069
				public static LocString NAME = "Cleaning";

				// Token: 0x0400B7DE RID: 47070
				public static LocString TOOLTIP = "This building is currently being cleaned";
			}

			// Token: 0x02002D09 RID: 11529
			public class REGIONISBLOCKED
			{
				// Token: 0x0400B7DF RID: 47071
				public static LocString NAME = "Blocked";

				// Token: 0x0400B7E0 RID: 47072
				public static LocString TOOLTIP = "Undug material is blocking off an essential tile";
			}

			// Token: 0x02002D0A RID: 11530
			public class STUDIED
			{
				// Token: 0x0400B7E1 RID: 47073
				public static LocString NAME = "Analysis Complete";

				// Token: 0x0400B7E2 RID: 47074
				public static LocString TOOLTIP = "Information on this Natural Feature has been compiled below.";
			}

			// Token: 0x02002D0B RID: 11531
			public class AWAITINGSTUDY
			{
				// Token: 0x0400B7E3 RID: 47075
				public static LocString NAME = "Analysis Pending";

				// Token: 0x0400B7E4 RID: 47076
				public static LocString TOOLTIP = "New information on this Natural Feature will be compiled once the field study is complete";
			}

			// Token: 0x02002D0C RID: 11532
			public class DURABILITY
			{
				// Token: 0x0400B7E5 RID: 47077
				public static LocString NAME = "Durability: {durability}";

				// Token: 0x0400B7E6 RID: 47078
				public static LocString TOOLTIP = "Items lose durability each time they are equipped, and can no longer be put on by a Duplicant once they reach 0% durability\n\nRepair of this item can be done in the appropriate fabrication station";
			}

			// Token: 0x02002D0D RID: 11533
			public class STOREDITEMDURABILITY
			{
				// Token: 0x0400B7E7 RID: 47079
				public static LocString NAME = "Durability: {durability}";

				// Token: 0x0400B7E8 RID: 47080
				public static LocString TOOLTIP = "Items lose durability each time they are equipped, and can no longer be put on by a Duplicant once they reach 0% durability\n\nRepair of this item can be done in the appropriate fabrication station";
			}

			// Token: 0x02002D0E RID: 11534
			public class ARTIFACTENTOMBED
			{
				// Token: 0x0400B7E9 RID: 47081
				public static LocString NAME = "Entombed Artifact";

				// Token: 0x0400B7EA RID: 47082
				public static LocString TOOLTIP = "This artifact is trapped in an obscuring shell limiting its decor. A skilled artist can remove it at the " + BUILDINGS.PREFABS.ARTIFACTANALYSISSTATION.NAME;
			}

			// Token: 0x02002D0F RID: 11535
			public class TEAROPEN
			{
				// Token: 0x0400B7EB RID: 47083
				public static LocString NAME = "Temporal Tear open";

				// Token: 0x0400B7EC RID: 47084
				public static LocString TOOLTIP = "An open passage through spacetime";
			}

			// Token: 0x02002D10 RID: 11536
			public class TEARCLOSED
			{
				// Token: 0x0400B7ED RID: 47085
				public static LocString NAME = "Temporal Tear closed";

				// Token: 0x0400B7EE RID: 47086
				public static LocString TOOLTIP = "Perhaps some technology could open the passage";
			}
		}

		// Token: 0x02001DAC RID: 7596
		public class POPFX
		{
			// Token: 0x04008639 RID: 34361
			public static LocString RESOURCE_EATEN = "Resource Eaten";
		}

		// Token: 0x02001DAD RID: 7597
		public class NOTIFICATIONS
		{
			// Token: 0x02002D11 RID: 11537
			public class BASICCONTROLS
			{
				// Token: 0x0400B7EF RID: 47087
				public static LocString NAME = "Tutorial: Basic Controls";

				// Token: 0x0400B7F0 RID: 47088
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"• I can use ",
					UI.FormatAsHotKey(global::Action.PanLeft),
					" and ",
					UI.FormatAsHotKey(global::Action.PanRight),
					" to pan my view left and right, and ",
					UI.FormatAsHotKey(global::Action.PanUp),
					" and ",
					UI.FormatAsHotKey(global::Action.PanDown),
					" to pan up and down.\n\n• ",
					UI.FormatAsHotKey(global::Action.ZoomIn),
					" lets me zoom in, and ",
					UI.FormatAsHotKey(global::Action.ZoomOut),
					" zooms out.\n\n• ",
					UI.FormatAsHotKey(global::Action.CameraHome),
					" returns my view to the Printing Pod.\n\n• I can speed or slow my perception of time using the top left corner buttons, or by pressing ",
					UI.FormatAsHotKey(global::Action.SpeedUp),
					" or ",
					UI.FormatAsHotKey(global::Action.SlowDown),
					". Pressing ",
					UI.FormatAsHotKey(global::Action.TogglePause),
					" will pause the flow of time entirely.\n\n• I'll keep records of everything I discover in my personal DATABASE ",
					UI.FormatAsHotKey(global::Action.ManageDatabase),
					" to refer back to if I forget anything important."
				});

				// Token: 0x0400B7F1 RID: 47089
				public static LocString MESSAGEBODYALT = string.Concat(new string[]
				{
					"• I can use ",
					UI.FormatAsHotKey(global::Action.AnalogCamera),
					" to pan my view.\n\n• ",
					UI.FormatAsHotKey(global::Action.ZoomIn),
					" lets me zoom in, and ",
					UI.FormatAsHotKey(global::Action.ZoomOut),
					" zooms out.\n\n• I can speed or slow my perception of time using the top left corner buttons, or by pressing ",
					UI.FormatAsHotKey(global::Action.CycleSpeed),
					". Pressing ",
					UI.FormatAsHotKey(global::Action.TogglePause),
					" will pause the flow of time entirely.\n\n• I'll keep records of everything I discover in my personal DATABASE ",
					UI.FormatAsHotKey(global::Action.ManageDatabase),
					" to refer back to if I forget anything important."
				});

				// Token: 0x0400B7F2 RID: 47090
				public static LocString TOOLTIP = "Notes on using my HUD";
			}

			// Token: 0x02002D12 RID: 11538
			public class CODEXUNLOCK
			{
				// Token: 0x0400B7F3 RID: 47091
				public static LocString NAME = "New Log Entry";

				// Token: 0x0400B7F4 RID: 47092
				public static LocString MESSAGEBODY = "I've added a new log entry to my Database";

				// Token: 0x0400B7F5 RID: 47093
				public static LocString TOOLTIP = "I've added a new log entry to my Database";
			}

			// Token: 0x02002D13 RID: 11539
			public class WELCOMEMESSAGE
			{
				// Token: 0x0400B7F6 RID: 47094
				public static LocString NAME = "Tutorial: Colony Management";

				// Token: 0x0400B7F7 RID: 47095
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"I can use the ",
					UI.FormatAsTool("Dig Tool", global::Action.Dig),
					" and the ",
					UI.FormatAsBuildMenuTab("Build Menu"),
					" in the lower left of the screen to begin planning my first construction tasks.\n\nOnce I've placed a few errands my Duplicants will automatically get to work, without me needing to direct them individually."
				});

				// Token: 0x0400B7F8 RID: 47096
				public static LocString TOOLTIP = "Notes on getting Duplicants to do my bidding";
			}

			// Token: 0x02002D14 RID: 11540
			public class STRESSMANAGEMENTMESSAGE
			{
				// Token: 0x0400B7F9 RID: 47097
				public static LocString NAME = "Tutorial: Stress Management";

				// Token: 0x0400B7FA RID: 47098
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"At 100% ",
					UI.FormatAsLink("Stress", "STRESS"),
					", a Duplicant will have a nervous breakdown and be unable to work.\n\nBreakdowns can manifest in different colony-threatening ways, such as the destruction of buildings or the binge eating of food.\n\nI can select a Duplicant and mouse over ",
					UI.FormatAsLink("Stress", "STRESS"),
					" in their STATUS TAB to view their individual ",
					UI.FormatAsLink("Stress Factors", "STRESS"),
					", and hopefully minimize them before they become a problem."
				});

				// Token: 0x0400B7FB RID: 47099
				public static LocString TOOLTIP = "Notes on keeping Duplicants happy and productive";
			}

			// Token: 0x02002D15 RID: 11541
			public class TASKPRIORITIESMESSAGE
			{
				// Token: 0x0400B7FC RID: 47100
				public static LocString NAME = "Tutorial: Priority";

				// Token: 0x0400B7FD RID: 47101
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"Duplicants always perform errands in order of highest to lowest priority. They will harvest ",
					UI.FormatAsLink("Food", "FOOD"),
					" before they build, for example, or always build new structures before they mine materials.\n\nI can open the ",
					UI.FormatAsManagementMenu("Priorities Screen", global::Action.ManagePriorities),
					" to set which Errand Types Duplicants may or may not perform, or to specialize skilled Duplicants for particular Errand Types."
				});

				// Token: 0x0400B7FE RID: 47102
				public static LocString TOOLTIP = "Notes on managing Duplicants' errands";
			}

			// Token: 0x02002D16 RID: 11542
			public class MOPPINGMESSAGE
			{
				// Token: 0x0400B7FF RID: 47103
				public static LocString NAME = "Tutorial: Polluted Water";

				// Token: 0x0400B800 RID: 47104
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					" slowly emits ",
					UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN"),
					" which accelerates the spread of ",
					UI.FormatAsLink("Disease", "DISEASE"),
					".\n\nDuplicants will also be ",
					UI.FormatAsLink("Stressed", "STRESS"),
					" by walking through Polluted Water, so I should have my Duplicants clean up spills by ",
					UI.CLICK(UI.ClickType.clicking),
					" and dragging the ",
					UI.FormatAsTool("Mop Tool", global::Action.Mop)
				});

				// Token: 0x0400B801 RID: 47105
				public static LocString TOOLTIP = "Notes on handling polluted materials";
			}

			// Token: 0x02002D17 RID: 11543
			public class LOCOMOTIONMESSAGE
			{
				// Token: 0x0400B802 RID: 47106
				public static LocString NAME = "Video: Duplicant Movement";

				// Token: 0x0400B803 RID: 47107
				public static LocString MESSAGEBODY = "Duplicants have limited jumping and climbing abilities. They can only climb two tiles high and cannot fit into spaces shorter than two tiles, or cross gaps wider than one tile. I should keep this in mind while placing errands.\n\nTo check if an errand I've placed is accessible, I can select a Duplicant and " + UI.CLICK(UI.ClickType.click) + " <b>Show Navigation</b> to view all areas within their reach.";

				// Token: 0x0400B804 RID: 47108
				public static LocString TOOLTIP = "Notes on my Duplicants' maneuverability";
			}

			// Token: 0x02002D18 RID: 11544
			public class PRIORITIESMESSAGE
			{
				// Token: 0x0400B805 RID: 47109
				public static LocString NAME = "Tutorial: Errand Priorities";

				// Token: 0x0400B806 RID: 47110
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"Duplicants will choose where to work based on the priority of the errands that I give them. I can open the ",
					UI.FormatAsManagementMenu("Priorities Screen", global::Action.ManagePriorities),
					" to set their ",
					UI.PRE_KEYWORD,
					"Duplicant Priorities",
					UI.PST_KEYWORD,
					", and the ",
					UI.FormatAsTool("Priority Tool", global::Action.Prioritize),
					" to fine tune ",
					UI.PRE_KEYWORD,
					"Building Priority",
					UI.PST_KEYWORD,
					". Many buildings will also let me change their Priority level when I select them."
				});

				// Token: 0x0400B807 RID: 47111
				public static LocString TOOLTIP = "Notes on my Duplicants' priorities";
			}

			// Token: 0x02002D19 RID: 11545
			public class FETCHINGWATERMESSAGE
			{
				// Token: 0x0400B808 RID: 47112
				public static LocString NAME = "Tutorial: Fetching Water";

				// Token: 0x0400B809 RID: 47113
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"By building a ",
					UI.FormatAsLink("Pitcher Pump", "LIQUIDPUMPINGSTATION"),
					" from the ",
					UI.FormatAsBuildMenuTab("Plumbing Tab", global::Action.Plan5),
					" over a pool of liquid, my Duplicants will be able to bottle it up and manually deliver it wherever it needs to go."
				});

				// Token: 0x0400B80A RID: 47114
				public static LocString TOOLTIP = "Notes on liquid resource gathering";
			}

			// Token: 0x02002D1A RID: 11546
			public class SCHEDULEMESSAGE
			{
				// Token: 0x0400B80B RID: 47115
				public static LocString NAME = "Tutorial: Scheduling";

				// Token: 0x0400B80C RID: 47116
				public static LocString MESSAGEBODY = "My Duplicants will only eat, sleep, work, or bathe during the times I allot for such activities.\n\nTo make the best use of their time, I can open the " + UI.FormatAsManagementMenu("Schedule Tab", global::Action.ManageSchedule) + " to adjust the colony's schedule and plan how they should utilize their day.";

				// Token: 0x0400B80D RID: 47117
				public static LocString TOOLTIP = "Notes on scheduling my Duplicants' time";
			}

			// Token: 0x02002D1B RID: 11547
			public class THERMALCOMFORT
			{
				// Token: 0x0400B80E RID: 47118
				public static LocString NAME = "Tutorial: Duplicant Temperature";

				// Token: 0x0400B80F RID: 47119
				public static LocString TOOLTIP = "Notes on helping Duplicants keep their cool";

				// Token: 0x0400B810 RID: 47120
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"Environments that are extremely ",
					UI.FormatAsLink("Hot", "HEAT"),
					" or ",
					UI.FormatAsLink("Cold", "HEAT"),
					" affect my Duplicants' internal body temperature and cause undue ",
					UI.FormatAsLink("Stress", "STRESS"),
					".\n\nOpening the ",
					UI.FormatAsOverlay("Temperature Overlay", global::Action.Overlay3),
					" and checking the <b>Thermal Tolerance</b> box allows me to view all areas where my Duplicants will feel discomfort and be unable to regulate their internal body temperature."
				});
			}

			// Token: 0x02002D1C RID: 11548
			public class TUTORIAL_OVERHEATING
			{
				// Token: 0x0400B811 RID: 47121
				public static LocString NAME = "Tutorial: Building Temperature";

				// Token: 0x0400B812 RID: 47122
				public static LocString TOOLTIP = "Notes on preventing building from breaking";

				// Token: 0x0400B813 RID: 47123
				public static LocString MESSAGEBODY = "When constructing buildings, I should always take note of their " + UI.FormatAsLink("Overheat Temperature", "HEAT") + " and plan their locations accordingly. Maintaining low ambient temperatures and good ventilation in the colony will also help keep building temperatures down.\n\nIf I allow buildings to exceed their Overheat Temperature they will begin to take damage, and if left unattended, they will break down and be unusable until repaired.";
			}

			// Token: 0x02002D1D RID: 11549
			public class LOTS_OF_GERMS
			{
				// Token: 0x0400B814 RID: 47124
				public static LocString NAME = "Tutorial: Germs and Disease";

				// Token: 0x0400B815 RID: 47125
				public static LocString TOOLTIP = "Notes on Duplicant disease risks";

				// Token: 0x0400B816 RID: 47126
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					UI.FormatAsLink("Germs", "DISEASE"),
					" such as ",
					UI.FormatAsLink("Food Poisoning", "FOODSICKNESS"),
					" and ",
					UI.FormatAsLink("Slimelung", "SLIMESICKNESS"),
					" can cause ",
					UI.FormatAsLink("Disease", "DISEASE"),
					" in my Duplicants. I can use the ",
					UI.FormatAsOverlay("Germ Overlay", global::Action.Overlay9),
					" to view all germ concentrations in my colony, and even detect the sources spawning them.\n\nBuilding Wash Basins from the ",
					UI.FormatAsBuildMenuTab("Medicine Tab", global::Action.Plan8),
					" near colony toilets will tell my Duplicants they need to wash up."
				});
			}

			// Token: 0x02002D1E RID: 11550
			public class BEING_INFECTED
			{
				// Token: 0x0400B817 RID: 47127
				public static LocString NAME = "Tutorial: Immune Systems";

				// Token: 0x0400B818 RID: 47128
				public static LocString TOOLTIP = "Notes on keeping Duplicants in peak health";

				// Token: 0x0400B819 RID: 47129
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"When Duplicants come into contact with various ",
					UI.FormatAsLink("Germs", "DISEASE"),
					", they'll need to expend points of ",
					UI.FormatAsLink("Immunity", "IMMUNE SYSTEM"),
					" to resist them and remain healthy. If repeated exposes causes their Immunity to drop to 0%, they'll be unable to resist germs and will contract the next disease they encounter.\n\nDoors with Access Permissions can be built from the BASE TAB<color=#F44A47> <b>[1]</b></color> of the ",
					UI.FormatAsLink("Build menu", "misc"),
					" to block Duplicants from entering biohazardous areas while they recover their spent immunity points."
				});
			}

			// Token: 0x02002D1F RID: 11551
			public class DISEASE_COOKING
			{
				// Token: 0x0400B81A RID: 47130
				public static LocString NAME = "Tutorial: Food Safety";

				// Token: 0x0400B81B RID: 47131
				public static LocString TOOLTIP = "Notes on managing food contamination";

				// Token: 0x0400B81C RID: 47132
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"The ",
					UI.FormatAsLink("Food", "FOOD"),
					" my Duplicants cook will only ever be as clean as the ingredients used to make it. Storing food in sterile or ",
					UI.FormatAsLink("Refrigerated", "REFRIGERATOR"),
					" environments will keep food free of ",
					UI.FormatAsLink("Germs", "DISEASE"),
					", while carefully placed hygiene stations like ",
					BUILDINGS.PREFABS.WASHBASIN.NAME,
					" or ",
					BUILDINGS.PREFABS.SHOWER.NAME,
					" will prevent the cooks from infecting the food by handling it.\n\nDangerously contaminated food can be sent to compost by ",
					UI.CLICK(UI.ClickType.clicking),
					" the <b>Compost</b> button on the selected item."
				});
			}

			// Token: 0x02002D20 RID: 11552
			public class SUITS
			{
				// Token: 0x0400B81D RID: 47133
				public static LocString NAME = "Tutorial: Atmo Suits";

				// Token: 0x0400B81E RID: 47134
				public static LocString TOOLTIP = "Notes on using atmo suits";

				// Token: 0x0400B81F RID: 47135
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					UI.FormatAsLink("Atmo Suits", "ATMO_SUIT"),
					" can be equipped to protect my Duplicants from environmental hazards like extreme ",
					UI.FormatAsLink("Heat", "Heat"),
					", airborne ",
					UI.FormatAsLink("Germs", "DISEASE"),
					", or unbreathable ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					". In order to utilize these suits, I'll need to hook up an ",
					UI.FormatAsLink("Atmo Suit Dock", "SUITLOCKER"),
					" to an ",
					UI.FormatAsLink("Atmo Suit Checkpoint", "SUITMARKER"),
					" , then store one of the suits inside.\n\nDuplicants will equip a suit when they walk past the checkpoint in the chosen direction, and will unequip their suit when walking back the opposite way."
				});
			}

			// Token: 0x02002D21 RID: 11553
			public class RADIATION
			{
				// Token: 0x0400B820 RID: 47136
				public static LocString NAME = "Tutorial: Radiation";

				// Token: 0x0400B821 RID: 47137
				public static LocString TOOLTIP = "Notes on managing radiation";

				// Token: 0x0400B822 RID: 47138
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"Objects such as ",
					UI.FormatAsLink("Uranium Ore", "URANIUMORE"),
					" and ",
					UI.FormatAsLink("Beeta Hives", "BEE"),
					" emit a ",
					UI.FormatAsLink("Radioactive", "RADIOACTIVE"),
					" energy that can be toxic to my Duplicants.\n\nI can use the ",
					UI.FormatAsOverlay("Radiation Overlay"),
					" ",
					UI.FormatAsHotKey(global::Action.Overlay15),
					" to check the scope of the Radiation field. Building thick walls around radiation emitters will dampen the field and protect my Duplicants from getting ",
					UI.FormatAsLink("Radiation Sickness", "RADIATIONSICKNESS"),
					" ."
				});
			}

			// Token: 0x02002D22 RID: 11554
			public class SPACETRAVEL
			{
				// Token: 0x0400B823 RID: 47139
				public static LocString NAME = "Tutorial: Space Travel";

				// Token: 0x0400B824 RID: 47140
				public static LocString TOOLTIP = "Notes on traveling in space";

				// Token: 0x0400B825 RID: 47141
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"Building a rocket first requires constructing a ",
					UI.FormatAsLink("Rocket Platform", "LAUNCHPAD"),
					" and adding modules from the menu. All components of the Rocket Checklist will need to be complete before being capable of launching.\n\nA ",
					UI.FormatAsLink("Telescope", "CLUSTERTELESCOPE"),
					" needs to be built on the surface of a Planetoid in order to use the ",
					UI.PRE_KEYWORD,
					"Starmap Screen",
					UI.PST_KEYWORD,
					" ",
					UI.FormatAsHotKey(global::Action.ManageStarmap),
					" to see and set course for new destinations."
				});
			}

			// Token: 0x02002D23 RID: 11555
			public class MORALE
			{
				// Token: 0x0400B826 RID: 47142
				public static LocString NAME = "Video: Duplicant Morale";

				// Token: 0x0400B827 RID: 47143
				public static LocString TOOLTIP = "Notes on Duplicant expectations";

				// Token: 0x0400B828 RID: 47144
				public static LocString MESSAGEBODY = "Food, Rooms, Decor, and Recreation all have an effect on Duplicant Morale. Good experiences improve their Morale, while poor experiences lower it. When a Duplicant's Morale is below their Expectations, they will become Stressed.\n\nDuplicants' Expectations will get higher as they are given new Skills, and the colony will have to be improved to keep up their Morale. An overview of Morale and Stress can be viewed on the Vitals screen.";
			}

			// Token: 0x02002D24 RID: 11556
			public class POWER
			{
				// Token: 0x0400B829 RID: 47145
				public static LocString NAME = "Video: Power Circuits";

				// Token: 0x0400B82A RID: 47146
				public static LocString TOOLTIP = "Notes on managing electricity";

				// Token: 0x0400B82B RID: 47147
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"Generators are considered \"Producers\" of Power, while the various buildings and machines in the colony are considered \"Consumers\". Each Consumer will pull a certain wattage from the power circuit it is connected to, which can be checked at any time by ",
					UI.CLICK(UI.ClickType.clicking),
					" the building and going to the Energy Tab.\n\nI can use the Power Overlay ",
					UI.FormatAsHotKey(global::Action.Overlay2),
					" to quickly check the status of all my circuits. If the Consumers are taking more wattage than the Generators are creating, the Batteries will drain and there will be brownouts.\n\nAdditionally, if the Consumers are pulling more wattage through the Wires than the Wires can handle, they will overload and burn out. To correct both these situations, I will need to reorganize my Consumers onto separate circuits."
				});
			}

			// Token: 0x02002D25 RID: 11557
			public class DIGGING
			{
				// Token: 0x0400B82C RID: 47148
				public static LocString NAME = "Video: Digging for Resources";

				// Token: 0x0400B82D RID: 47149
				public static LocString TOOLTIP = "Notes on buried riches";

				// Token: 0x0400B82E RID: 47150
				public static LocString MESSAGEBODY = "Everything a colony needs to get going is found in the ground. Instructing Duplicants to dig out areas means we can find food, mine resources to build infrastructure, and clear space for the colony to grow. I can access the Dig Tool with " + UI.FormatAsHotKey(global::Action.Dig) + ", which allows me to select the area where I want my Duplicants to dig.\n\nDuplicants will need to gain the Superhard Digging skill to mine Abyssalite and the Superduperhard Digging skill to mine Diamond and Obsidian. Without the proper skills, these materials will be undiggable.";
			}

			// Token: 0x02002D26 RID: 11558
			public class INSULATION
			{
				// Token: 0x0400B82F RID: 47151
				public static LocString NAME = "Video: Insulation";

				// Token: 0x0400B830 RID: 47152
				public static LocString TOOLTIP = "Notes on effective temperature management";

				// Token: 0x0400B831 RID: 47153
				public static LocString MESSAGEBODY = "The temperature of an environment can have positive or negative effects on the well-being of my Duplicants, as well as the plants and critters in my colony. Selecting " + UI.FormatAsHotKey(global::Action.Overlay3) + " will open the Temperature Overlay where I can check for any hot or cold spots.\n\nI can use a Utility building like an Ice-E Fan or a Space Heater to make an area colder or warmer. However, I will have limited success changing the temperature of a room unless I build the area with insulating tiles to prevent cold or warm air from escaping.";
			}

			// Token: 0x02002D27 RID: 11559
			public class PLUMBING
			{
				// Token: 0x0400B832 RID: 47154
				public static LocString NAME = "Video: Plumbing and Ventilation";

				// Token: 0x0400B833 RID: 47155
				public static LocString TOOLTIP = "Notes on connecting buildings with pipes";

				// Token: 0x0400B834 RID: 47156
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"When connecting pipes for plumbing, it is useful to have the Plumbing Overlay ",
					UI.FormatAsHotKey(global::Action.Overlay6),
					" selected. Each building which requires plumbing must have their Building Intake connected to the Output Pipe from a source such as a Liquid Pump. Liquid Pumps must be submerged in liquid and attached to a power source to function.\n\nBuildings often output contaminated water which must flow out of the building through piping from the Output Pipe. The water can then be expelled through a Liquid Vent, or filtered through a Water Sieve for reuse.\n\nVentilation applies the same principles to gases. Select the Ventilation Overlay ",
					UI.FormatAsHotKey(global::Action.Overlay7),
					" to see how gases are being moved around the colony."
				});
			}

			// Token: 0x02002D28 RID: 11560
			public class NEW_AUTOMATION_WARNING
			{
				// Token: 0x0400B835 RID: 47157
				public static LocString NAME = "New Automation Port";

				// Token: 0x0400B836 RID: 47158
				public static LocString TOOLTIP = "This building has a new automation port and is unintentionally connected to an existing " + BUILDINGS.PREFABS.LOGICWIRE.NAME;
			}

			// Token: 0x02002D29 RID: 11561
			public class DTU
			{
				// Token: 0x0400B837 RID: 47159
				public static LocString NAME = "Tutorial: Duplicant Thermal Units";

				// Token: 0x0400B838 RID: 47160
				public static LocString TOOLTIP = "Notes on measuring heat energy";

				// Token: 0x0400B839 RID: 47161
				public static LocString MESSAGEBODY = "My Duplicants measure heat energy in Duplicant Thermal Units or DTU.\n\n1 DTU = 1055.06 J";
			}

			// Token: 0x02002D2A RID: 11562
			public class NOMESSAGES
			{
				// Token: 0x0400B83A RID: 47162
				public static LocString NAME = "";

				// Token: 0x0400B83B RID: 47163
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002D2B RID: 11563
			public class NOALERTS
			{
				// Token: 0x0400B83C RID: 47164
				public static LocString NAME = "";

				// Token: 0x0400B83D RID: 47165
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002D2C RID: 11564
			public class NEWTRAIT
			{
				// Token: 0x0400B83E RID: 47166
				public static LocString NAME = "{0} has developed a trait";

				// Token: 0x0400B83F RID: 47167
				public static LocString TOOLTIP = "{0} has developed the trait(s):\n    • {1}";
			}

			// Token: 0x02002D2D RID: 11565
			public class RESEARCHCOMPLETE
			{
				// Token: 0x0400B840 RID: 47168
				public static LocString NAME = "Research Complete";

				// Token: 0x0400B841 RID: 47169
				public static LocString MESSAGEBODY = "Eureka! We've discovered {0} Technology.\n\nNew buildings have become available:\n  • {1}";

				// Token: 0x0400B842 RID: 47170
				public static LocString TOOLTIP = "{0} research complete!";
			}

			// Token: 0x02002D2E RID: 11566
			public class WORLDDETECTED
			{
				// Token: 0x0400B843 RID: 47171
				public static LocString NAME = "New " + UI.CLUSTERMAP.PLANETOID + " detected";

				// Token: 0x0400B844 RID: 47172
				public static LocString MESSAGEBODY = "My Duplicants' astronomical efforts have uncovered a new " + UI.CLUSTERMAP.PLANETOID + ":\n{0}";

				// Token: 0x0400B845 RID: 47173
				public static LocString TOOLTIP = "{0} discovered";
			}

			// Token: 0x02002D2F RID: 11567
			public class SKILL_POINT_EARNED
			{
				// Token: 0x0400B846 RID: 47174
				public static LocString NAME = "{Duplicant} earned a skill point!";

				// Token: 0x0400B847 RID: 47175
				public static LocString MESSAGEBODY = "These Duplicants have Skill Points that can be spent on new abilities:\n{0}";

				// Token: 0x0400B848 RID: 47176
				public static LocString LINE = "\n• <b>{0}</b>";

				// Token: 0x0400B849 RID: 47177
				public static LocString TOOLTIP = "{Duplicant} has been working hard and is ready to learn a new skill";
			}

			// Token: 0x02002D30 RID: 11568
			public class DUPLICANTABSORBED
			{
				// Token: 0x0400B84A RID: 47178
				public static LocString NAME = "Printables have been reabsorbed";

				// Token: 0x0400B84B RID: 47179
				public static LocString MESSAGEBODY = "The Printing Pod is no longer available for printing.\nCountdown to the next production has been rebooted.";

				// Token: 0x0400B84C RID: 47180
				public static LocString TOOLTIP = "Printing countdown rebooted";
			}

			// Token: 0x02002D31 RID: 11569
			public class DUPLICANTDIED
			{
				// Token: 0x0400B84D RID: 47181
				public static LocString NAME = "Duplicants have died";

				// Token: 0x0400B84E RID: 47182
				public static LocString TOOLTIP = "These Duplicants have died:";
			}

			// Token: 0x02002D32 RID: 11570
			public class FOODROT
			{
				// Token: 0x0400B84F RID: 47183
				public static LocString NAME = "Food has decayed";

				// Token: 0x0400B850 RID: 47184
				public static LocString TOOLTIP = "These " + UI.FormatAsLink("Food", "FOOD") + " items have rotted and are no longer edible:{0}";
			}

			// Token: 0x02002D33 RID: 11571
			public class FOODSTALE
			{
				// Token: 0x0400B851 RID: 47185
				public static LocString NAME = "Food has become stale";

				// Token: 0x0400B852 RID: 47186
				public static LocString TOOLTIP = "These " + UI.FormatAsLink("Food", "FOOD") + " items have become stale and could rot if not stored:";
			}

			// Token: 0x02002D34 RID: 11572
			public class YELLOWALERT
			{
				// Token: 0x0400B853 RID: 47187
				public static LocString NAME = "Yellow Alert";

				// Token: 0x0400B854 RID: 47188
				public static LocString TOOLTIP = "The colony has some top priority tasks to complete before resuming a normal schedule";
			}

			// Token: 0x02002D35 RID: 11573
			public class REDALERT
			{
				// Token: 0x0400B855 RID: 47189
				public static LocString NAME = "Red Alert";

				// Token: 0x0400B856 RID: 47190
				public static LocString TOOLTIP = "The colony is prioritizing work over their individual well-being";
			}

			// Token: 0x02002D36 RID: 11574
			public class REACTORMELTDOWN
			{
				// Token: 0x0400B857 RID: 47191
				public static LocString NAME = "Reactor Meltdown";

				// Token: 0x0400B858 RID: 47192
				public static LocString TOOLTIP = "A Research Reactor has overheated and is melting down! Extreme radiation is flooding the area";
			}

			// Token: 0x02002D37 RID: 11575
			public class HEALING
			{
				// Token: 0x0400B859 RID: 47193
				public static LocString NAME = "Healing";

				// Token: 0x0400B85A RID: 47194
				public static LocString TOOLTIP = "This Duplicant is recovering from an injury";
			}

			// Token: 0x02002D38 RID: 11576
			public class UNREACHABLEITEM
			{
				// Token: 0x0400B85B RID: 47195
				public static LocString NAME = "Unreachable resources";

				// Token: 0x0400B85C RID: 47196
				public static LocString TOOLTIP = "Duplicants cannot retrieve these resources:";
			}

			// Token: 0x02002D39 RID: 11577
			public class INVALIDCONSTRUCTIONLOCATION
			{
				// Token: 0x0400B85D RID: 47197
				public static LocString NAME = "Invalid construction location";

				// Token: 0x0400B85E RID: 47198
				public static LocString TOOLTIP = "These buildings cannot be constructed in the planned areas:";
			}

			// Token: 0x02002D3A RID: 11578
			public class MISSINGMATERIALS
			{
				// Token: 0x0400B85F RID: 47199
				public static LocString NAME = "Missing materials";

				// Token: 0x0400B860 RID: 47200
				public static LocString TOOLTIP = "These resources are not available:";
			}

			// Token: 0x02002D3B RID: 11579
			public class BUILDINGOVERHEATED
			{
				// Token: 0x0400B861 RID: 47201
				public static LocString NAME = "Damage: Overheated";

				// Token: 0x0400B862 RID: 47202
				public static LocString TOOLTIP = "Extreme heat is damaging these buildings:";
			}

			// Token: 0x02002D3C RID: 11580
			public class TILECOLLAPSE
			{
				// Token: 0x0400B863 RID: 47203
				public static LocString NAME = "Ceiling Collapse!";

				// Token: 0x0400B864 RID: 47204
				public static LocString TOOLTIP = "Falling material fell on these Duplicants and displaced them:";
			}

			// Token: 0x02002D3D RID: 11581
			public class NO_OXYGEN_GENERATOR
			{
				// Token: 0x0400B865 RID: 47205
				public static LocString NAME = "No " + UI.FormatAsLink("Oxygen Generator", "OXYGEN") + " built";

				// Token: 0x0400B866 RID: 47206
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"My colony is not producing any new ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					"\n\n",
					UI.FormatAsLink("Oxygen Diffusers", "MINERALDEOXIDIZER"),
					" can be built from the ",
					UI.FormatAsBuildMenuTab("Oxygen Tab", global::Action.Plan2)
				});
			}

			// Token: 0x02002D3E RID: 11582
			public class INSUFFICIENTOXYGENLASTCYCLE
			{
				// Token: 0x0400B867 RID: 47207
				public static LocString NAME = "Insufficient Oxygen generation";

				// Token: 0x0400B868 RID: 47208
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"My colony is consuming more ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" than it is producing, and will run out air if I do not increase production.\n\nI should check my existing oxygen production buildings to ensure they're operating correctly\n\n• ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" produced last cycle: {EmittingRate}\n• Consumed last cycle: {ConsumptionRate}"
				});
			}

			// Token: 0x02002D3F RID: 11583
			public class UNREFRIGERATEDFOOD
			{
				// Token: 0x0400B869 RID: 47209
				public static LocString NAME = "Unrefrigerated Food";

				// Token: 0x0400B86A RID: 47210
				public static LocString TOOLTIP = "These " + UI.FormatAsLink("Food", "FOOD") + " items are stored but not refrigerated:\n";
			}

			// Token: 0x02002D40 RID: 11584
			public class FOODLOW
			{
				// Token: 0x0400B86B RID: 47211
				public static LocString NAME = "Food shortage";

				// Token: 0x0400B86C RID: 47212
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The colony's ",
					UI.FormatAsLink("Food", "FOOD"),
					" reserves are low:\n\n    • {0} are currently available\n    • {1} is being consumed per cycle\n\n",
					UI.FormatAsLink("Microbe Mushers", "MICROBEMUSHER"),
					" can be built from the ",
					UI.FormatAsBuildMenuTab("Food Tab", global::Action.Plan4)
				});
			}

			// Token: 0x02002D41 RID: 11585
			public class NO_MEDICAL_COTS
			{
				// Token: 0x0400B86D RID: 47213
				public static LocString NAME = "No " + UI.FormatAsLink("Sick Bay", "DOCTORSTATION") + " built";

				// Token: 0x0400B86E RID: 47214
				public static LocString TOOLTIP = "There is nowhere for sick Duplicants receive medical care\n\n" + UI.FormatAsLink("Sick Bays", "DOCTORSTATION") + " can be built from the " + UI.FormatAsBuildMenuTab("Medicine Tab", global::Action.Plan8);
			}

			// Token: 0x02002D42 RID: 11586
			public class NEEDTOILET
			{
				// Token: 0x0400B86F RID: 47215
				public static LocString NAME = "No " + UI.FormatAsLink("Outhouse", "OUTHOUSE") + " built";

				// Token: 0x0400B870 RID: 47216
				public static LocString TOOLTIP = "My Duplicants have nowhere to relieve themselves\n\n" + UI.FormatAsLink("Outhouses", "OUTHOUSE") + " can be built from the " + UI.FormatAsBuildMenuTab("Plumbing Tab", global::Action.Plan5);
			}

			// Token: 0x02002D43 RID: 11587
			public class NEEDFOOD
			{
				// Token: 0x0400B871 RID: 47217
				public static LocString NAME = "Colony requires a food source";

				// Token: 0x0400B872 RID: 47218
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The colony will exhaust their supplies without a new ",
					UI.FormatAsLink("Food", "FOOD"),
					" source\n\n",
					UI.FormatAsLink("Microbe Mushers", "MICROBEMUSHER"),
					" can be built from the ",
					UI.FormatAsBuildMenuTab("Food Tab", global::Action.Plan4)
				});
			}

			// Token: 0x02002D44 RID: 11588
			public class HYGENE_NEEDED
			{
				// Token: 0x0400B873 RID: 47219
				public static LocString NAME = "No " + UI.FormatAsLink("Wash Basin", "WASHBASIN") + " built";

				// Token: 0x0400B874 RID: 47220
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					UI.FormatAsLink("Germs", "DISEASE"),
					" are spreading in the colony because my Duplicants have nowhere to clean up\n\n",
					UI.FormatAsLink("Wash Basins", "WASHBASIN"),
					" can be built from the ",
					UI.FormatAsBuildMenuTab("Medicine Tab", global::Action.Plan8)
				});
			}

			// Token: 0x02002D45 RID: 11589
			public class NEEDSLEEP
			{
				// Token: 0x0400B875 RID: 47221
				public static LocString NAME = "No " + UI.FormatAsLink("Cots", "COT") + " built";

				// Token: 0x0400B876 RID: 47222
				public static LocString TOOLTIP = "My Duplicants would appreciate a place to sleep\n\n" + UI.FormatAsLink("Cots", "COTS") + " can be built from the " + UI.FormatAsBuildMenuTab("Furniture Tab", global::Action.Plan9);
			}

			// Token: 0x02002D46 RID: 11590
			public class NEEDENERGYSOURCE
			{
				// Token: 0x0400B877 RID: 47223
				public static LocString NAME = "Colony requires a " + UI.FormatAsLink("Power", "POWER") + " source";

				// Token: 0x0400B878 RID: 47224
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					UI.FormatAsLink("Power", "POWER"),
					" is required to operate electrical buildings\n\n",
					UI.FormatAsLink("Manual Generators", "MANUALGENERATOR"),
					" and ",
					UI.FormatAsLink("Wire", "WIRE"),
					" can be built from the ",
					UI.FormatAsLink("Power Tab", "[3]")
				});
			}

			// Token: 0x02002D47 RID: 11591
			public class RESOURCEMELTED
			{
				// Token: 0x0400B879 RID: 47225
				public static LocString NAME = "Resources melted";

				// Token: 0x0400B87A RID: 47226
				public static LocString TOOLTIP = "These resources have melted:";
			}

			// Token: 0x02002D48 RID: 11592
			public class VENTOVERPRESSURE
			{
				// Token: 0x0400B87B RID: 47227
				public static LocString NAME = "Vent overpressurized";

				// Token: 0x0400B87C RID: 47228
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" systems have exited the ideal ",
					UI.PRE_KEYWORD,
					"Pressure",
					UI.PST_KEYWORD,
					" range:"
				});
			}

			// Token: 0x02002D49 RID: 11593
			public class VENTBLOCKED
			{
				// Token: 0x0400B87D RID: 47229
				public static LocString NAME = "Vent blocked";

				// Token: 0x0400B87E RID: 47230
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Blocked ",
					UI.PRE_KEYWORD,
					"Pipes",
					UI.PST_KEYWORD,
					" have stopped these systems from functioning:"
				});
			}

			// Token: 0x02002D4A RID: 11594
			public class OUTPUTBLOCKED
			{
				// Token: 0x0400B87F RID: 47231
				public static LocString NAME = "Output blocked";

				// Token: 0x0400B880 RID: 47232
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Blocked ",
					UI.PRE_KEYWORD,
					"Pipes",
					UI.PST_KEYWORD,
					" have stopped these systems from functioning:"
				});
			}

			// Token: 0x02002D4B RID: 11595
			public class BROKENMACHINE
			{
				// Token: 0x0400B881 RID: 47233
				public static LocString NAME = "Building broken";

				// Token: 0x0400B882 RID: 47234
				public static LocString TOOLTIP = "These buildings have taken significant damage and are nonfunctional:";
			}

			// Token: 0x02002D4C RID: 11596
			public class STRUCTURALDAMAGE
			{
				// Token: 0x0400B883 RID: 47235
				public static LocString NAME = "Structural damage";

				// Token: 0x0400B884 RID: 47236
				public static LocString TOOLTIP = "These buildings' structural integrity has been compromised";
			}

			// Token: 0x02002D4D RID: 11597
			public class STRUCTURALCOLLAPSE
			{
				// Token: 0x0400B885 RID: 47237
				public static LocString NAME = "Structural collapse";

				// Token: 0x0400B886 RID: 47238
				public static LocString TOOLTIP = "These buildings have collapsed:";
			}

			// Token: 0x02002D4E RID: 11598
			public class GASCLOUDWARNING
			{
				// Token: 0x0400B887 RID: 47239
				public static LocString NAME = "A gas cloud approaches";

				// Token: 0x0400B888 RID: 47240
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A toxic ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" cloud will soon envelop the colony"
				});
			}

			// Token: 0x02002D4F RID: 11599
			public class GASCLOUDARRIVING
			{
				// Token: 0x0400B889 RID: 47241
				public static LocString NAME = "The colony is entering a cloud of gas";

				// Token: 0x0400B88A RID: 47242
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002D50 RID: 11600
			public class GASCLOUDPEAK
			{
				// Token: 0x0400B88B RID: 47243
				public static LocString NAME = "The gas cloud is at its densest point";

				// Token: 0x0400B88C RID: 47244
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002D51 RID: 11601
			public class GASCLOUDDEPARTING
			{
				// Token: 0x0400B88D RID: 47245
				public static LocString NAME = "The gas cloud is receding";

				// Token: 0x0400B88E RID: 47246
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002D52 RID: 11602
			public class GASCLOUDGONE
			{
				// Token: 0x0400B88F RID: 47247
				public static LocString NAME = "The colony is once again in open space";

				// Token: 0x0400B890 RID: 47248
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002D53 RID: 11603
			public class AVAILABLE
			{
				// Token: 0x0400B891 RID: 47249
				public static LocString NAME = "Resource available";

				// Token: 0x0400B892 RID: 47250
				public static LocString TOOLTIP = "These resources have become available:";
			}

			// Token: 0x02002D54 RID: 11604
			public class ALLOCATED
			{
				// Token: 0x0400B893 RID: 47251
				public static LocString NAME = "Resource allocated";

				// Token: 0x0400B894 RID: 47252
				public static LocString TOOLTIP = "These resources are reserved for a planned building:";
			}

			// Token: 0x02002D55 RID: 11605
			public class INCREASEDEXPECTATIONS
			{
				// Token: 0x0400B895 RID: 47253
				public static LocString NAME = "Duplicants' expectations increased";

				// Token: 0x0400B896 RID: 47254
				public static LocString TOOLTIP = "Duplicants require better amenities over time.\nThese Duplicants have increased their expectations:";
			}

			// Token: 0x02002D56 RID: 11606
			public class NEARLYDRY
			{
				// Token: 0x0400B897 RID: 47255
				public static LocString NAME = "Nearly dry";

				// Token: 0x0400B898 RID: 47256
				public static LocString TOOLTIP = "These Duplicants will dry off soon:";
			}

			// Token: 0x02002D57 RID: 11607
			public class IMMIGRANTSLEFT
			{
				// Token: 0x0400B899 RID: 47257
				public static LocString NAME = "Printables have been reabsorbed";

				// Token: 0x0400B89A RID: 47258
				public static LocString TOOLTIP = "The care packages have been disintegrated and printable Duplicants have been Oozed";
			}

			// Token: 0x02002D58 RID: 11608
			public class LEVELUP
			{
				// Token: 0x0400B89B RID: 47259
				public static LocString NAME = "Attribute increase";

				// Token: 0x0400B89C RID: 47260
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants' ",
					UI.PRE_KEYWORD,
					"Attributes",
					UI.PST_KEYWORD,
					" have improved:"
				});

				// Token: 0x0400B89D RID: 47261
				public static LocString SUFFIX = " - {0} Skill Level modifier raised to +{1}";
			}

			// Token: 0x02002D59 RID: 11609
			public class RESETSKILL
			{
				// Token: 0x0400B89E RID: 47262
				public static LocString NAME = "Skills reset";

				// Token: 0x0400B89F RID: 47263
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants have had their ",
					UI.PRE_KEYWORD,
					"Skill Points",
					UI.PST_KEYWORD,
					" refunded:"
				});
			}

			// Token: 0x02002D5A RID: 11610
			public class BADROCKETPATH
			{
				// Token: 0x0400B8A0 RID: 47264
				public static LocString NAME = "Flight Path Obstructed";

				// Token: 0x0400B8A1 RID: 47265
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A rocket's flight path has been interrupted by a new astronomical discovery.\nOpen the ",
					UI.PRE_KEYWORD,
					"Starmap Screen",
					UI.PST_KEYWORD,
					" ",
					UI.FormatAsHotKey(global::Action.ManageStarmap),
					" to reassign rocket paths"
				});
			}

			// Token: 0x02002D5B RID: 11611
			public class SCHEDULE_CHANGED
			{
				// Token: 0x0400B8A2 RID: 47266
				public static LocString NAME = "{0}: {1}!";

				// Token: 0x0400B8A3 RID: 47267
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Duplicants assigned to ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" have started their <b>{1}</b> block.\n\n{2}\n\nOpen the ",
					UI.PRE_KEYWORD,
					"Schedule Screen",
					UI.PST_KEYWORD,
					" ",
					UI.FormatAsHotKey(global::Action.ManageSchedule),
					" to change blocks or assignments"
				});
			}

			// Token: 0x02002D5C RID: 11612
			public class GENESHUFFLER
			{
				// Token: 0x0400B8A4 RID: 47268
				public static LocString NAME = "Genes Shuffled";

				// Token: 0x0400B8A5 RID: 47269
				public static LocString TOOLTIP = "These Duplicants had their genetic makeup modified:";

				// Token: 0x0400B8A6 RID: 47270
				public static LocString SUFFIX = " has developed " + UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD;
			}

			// Token: 0x02002D5D RID: 11613
			public class HEALINGTRAITGAIN
			{
				// Token: 0x0400B8A7 RID: 47271
				public static LocString NAME = "New trait";

				// Token: 0x0400B8A8 RID: 47272
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants' injuries weren't set and healed improperly.\nThey developed ",
					UI.PRE_KEYWORD,
					"Traits",
					UI.PST_KEYWORD,
					" as a result:"
				});

				// Token: 0x0400B8A9 RID: 47273
				public static LocString SUFFIX = " has developed " + UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD;
			}

			// Token: 0x02002D5E RID: 11614
			public class COLONYLOST
			{
				// Token: 0x0400B8AA RID: 47274
				public static LocString NAME = "Colony Lost";

				// Token: 0x0400B8AB RID: 47275
				public static LocString TOOLTIP = "All Duplicants are dead or incapacitated";
			}

			// Token: 0x02002D5F RID: 11615
			public class FABRICATOREMPTY
			{
				// Token: 0x0400B8AC RID: 47276
				public static LocString NAME = "Fabricator idle";

				// Token: 0x0400B8AD RID: 47277
				public static LocString TOOLTIP = "These fabricators have no recipes queued:";
			}

			// Token: 0x02002D60 RID: 11616
			public class SUIT_DROPPED
			{
				// Token: 0x0400B8AE RID: 47278
				public static LocString NAME = "No Docks available";

				// Token: 0x0400B8AF RID: 47279
				public static LocString TOOLTIP = "An exosuit was dropped because there were no empty docks available";
			}

			// Token: 0x02002D61 RID: 11617
			public class DEATH_SUFFOCATION
			{
				// Token: 0x0400B8B0 RID: 47280
				public static LocString NAME = "Duplicants suffocated";

				// Token: 0x0400B8B1 RID: 47281
				public static LocString TOOLTIP = "These Duplicants died from a lack of " + ELEMENTS.OXYGEN.NAME + ":";
			}

			// Token: 0x02002D62 RID: 11618
			public class DEATH_FROZENSOLID
			{
				// Token: 0x0400B8B2 RID: 47282
				public static LocString NAME = "Duplicants have frozen";

				// Token: 0x0400B8B3 RID: 47283
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants died from extremely low ",
					UI.PRE_KEYWORD,
					"Temperatures",
					UI.PST_KEYWORD,
					":"
				});
			}

			// Token: 0x02002D63 RID: 11619
			public class DEATH_OVERHEATING
			{
				// Token: 0x0400B8B4 RID: 47284
				public static LocString NAME = "Duplicants have overheated";

				// Token: 0x0400B8B5 RID: 47285
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants died from extreme ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					":"
				});
			}

			// Token: 0x02002D64 RID: 11620
			public class DEATH_STARVATION
			{
				// Token: 0x0400B8B6 RID: 47286
				public static LocString NAME = "Duplicants have starved";

				// Token: 0x0400B8B7 RID: 47287
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants died from a lack of ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					":"
				});
			}

			// Token: 0x02002D65 RID: 11621
			public class DEATH_FELL
			{
				// Token: 0x0400B8B8 RID: 47288
				public static LocString NAME = "Duplicants splattered";

				// Token: 0x0400B8B9 RID: 47289
				public static LocString TOOLTIP = "These Duplicants fell to their deaths:";
			}

			// Token: 0x02002D66 RID: 11622
			public class DEATH_CRUSHED
			{
				// Token: 0x0400B8BA RID: 47290
				public static LocString NAME = "Duplicants crushed";

				// Token: 0x0400B8BB RID: 47291
				public static LocString TOOLTIP = "These Duplicants have been crushed:";
			}

			// Token: 0x02002D67 RID: 11623
			public class DEATH_SUFFOCATEDTANKEMPTY
			{
				// Token: 0x0400B8BC RID: 47292
				public static LocString NAME = "Duplicants have suffocated";

				// Token: 0x0400B8BD RID: 47293
				public static LocString TOOLTIP = "These Duplicants were unable to reach " + UI.FormatAsLink("Oxygen", "OXYGEN") + " and died:";
			}

			// Token: 0x02002D68 RID: 11624
			public class DEATH_SUFFOCATEDAIRTOOHOT
			{
				// Token: 0x0400B8BE RID: 47294
				public static LocString NAME = "Duplicants have suffocated";

				// Token: 0x0400B8BF RID: 47295
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants have asphyxiated in ",
					UI.PRE_KEYWORD,
					"Hot",
					UI.PST_KEYWORD,
					" air:"
				});
			}

			// Token: 0x02002D69 RID: 11625
			public class DEATH_SUFFOCATEDAIRTOOCOLD
			{
				// Token: 0x0400B8C0 RID: 47296
				public static LocString NAME = "Duplicants have suffocated";

				// Token: 0x0400B8C1 RID: 47297
				public static LocString TOOLTIP = "These Duplicants have asphyxiated in " + UI.FormatAsLink("Cold", "HEAT") + " air:";
			}

			// Token: 0x02002D6A RID: 11626
			public class DEATH_DROWNED
			{
				// Token: 0x0400B8C2 RID: 47298
				public static LocString NAME = "Duplicants have drowned";

				// Token: 0x0400B8C3 RID: 47299
				public static LocString TOOLTIP = "These Duplicants have drowned:";
			}

			// Token: 0x02002D6B RID: 11627
			public class DEATH_ENTOUMBED
			{
				// Token: 0x0400B8C4 RID: 47300
				public static LocString NAME = "Duplicants have been entombed";

				// Token: 0x0400B8C5 RID: 47301
				public static LocString TOOLTIP = "These Duplicants are trapped and need assistance:";
			}

			// Token: 0x02002D6C RID: 11628
			public class DEATH_RAPIDDECOMPRESSION
			{
				// Token: 0x0400B8C6 RID: 47302
				public static LocString NAME = "Duplicants pressurized";

				// Token: 0x0400B8C7 RID: 47303
				public static LocString TOOLTIP = "These Duplicants died in a low pressure environment:";
			}

			// Token: 0x02002D6D RID: 11629
			public class DEATH_OVERPRESSURE
			{
				// Token: 0x0400B8C8 RID: 47304
				public static LocString NAME = "Duplicants pressurized";

				// Token: 0x0400B8C9 RID: 47305
				public static LocString TOOLTIP = "These Duplicants died in a high pressure environment:";
			}

			// Token: 0x02002D6E RID: 11630
			public class DEATH_POISONED
			{
				// Token: 0x0400B8CA RID: 47306
				public static LocString NAME = "Duplicants poisoned";

				// Token: 0x0400B8CB RID: 47307
				public static LocString TOOLTIP = "These Duplicants died as a result of poisoning:";
			}

			// Token: 0x02002D6F RID: 11631
			public class DEATH_DISEASE
			{
				// Token: 0x0400B8CC RID: 47308
				public static LocString NAME = "Duplicants have succumbed to disease";

				// Token: 0x0400B8CD RID: 47309
				public static LocString TOOLTIP = "These Duplicants died from an untreated " + UI.FormatAsLink("Disease", "DISEASE") + ":";
			}

			// Token: 0x02002D70 RID: 11632
			public class CIRCUIT_OVERLOADED
			{
				// Token: 0x0400B8CE RID: 47310
				public static LocString NAME = "Circuit Overloaded";

				// Token: 0x0400B8CF RID: 47311
				public static LocString TOOLTIP = "These " + BUILDINGS.PREFABS.WIRE.NAME + "s melted due to excessive current demands on their circuits";
			}

			// Token: 0x02002D71 RID: 11633
			public class LOGIC_CIRCUIT_OVERLOADED
			{
				// Token: 0x0400B8D0 RID: 47312
				public static LocString NAME = "Logic Circuit Overloaded";

				// Token: 0x0400B8D1 RID: 47313
				public static LocString TOOLTIP = "These " + BUILDINGS.PREFABS.LOGICWIRE.NAME + "s melted due to more bits of data being sent over them than they can support";
			}

			// Token: 0x02002D72 RID: 11634
			public class DISCOVERED_SPACE
			{
				// Token: 0x0400B8D2 RID: 47314
				public static LocString NAME = "ALERT - Surface Breach";

				// Token: 0x0400B8D3 RID: 47315
				public static LocString TOOLTIP = "Amazing!\n\nMy Duplicants have managed to breach the surface of our rocky prison.\n\nI should be careful; the region is extremely inhospitable and I could easily lose resources to the vacuum of space.";
			}

			// Token: 0x02002D73 RID: 11635
			public class COLONY_ACHIEVEMENT_EARNED
			{
				// Token: 0x0400B8D4 RID: 47316
				public static LocString NAME = "Colony Achievement earned";

				// Token: 0x0400B8D5 RID: 47317
				public static LocString TOOLTIP = "The colony has earned a new achievement.";
			}

			// Token: 0x02002D74 RID: 11636
			public class WARP_PORTAL_DUPE_READY
			{
				// Token: 0x0400B8D6 RID: 47318
				public static LocString NAME = "Duplicant warp ready";

				// Token: 0x0400B8D7 RID: 47319
				public static LocString TOOLTIP = "{dupe} is ready to warp from the " + BUILDINGS.PREFABS.WARPPORTAL.NAME;
			}

			// Token: 0x02002D75 RID: 11637
			public class GENETICANALYSISCOMPLETE
			{
				// Token: 0x0400B8D8 RID: 47320
				public static LocString NAME = "Seed Analysis Complete";

				// Token: 0x0400B8D9 RID: 47321
				public static LocString MESSAGEBODY = "Deeply probing the genes of the {Plant} plant have led to the discovery of a promising new cultivatable mutation:\n\n<b>{Subspecies}</b>\n\n{Info}";

				// Token: 0x0400B8DA RID: 47322
				public static LocString TOOLTIP = "{Plant} Analysis complete!";
			}

			// Token: 0x02002D76 RID: 11638
			public class NEWMUTANTSEED
			{
				// Token: 0x0400B8DB RID: 47323
				public static LocString NAME = "New Mutant Seed Discovered";

				// Token: 0x0400B8DC RID: 47324
				public static LocString TOOLTIP = "A new mutant variety of the {Plant} has been found. Analyze it at the " + BUILDINGS.PREFABS.GENETICANALYSISSTATION.NAME + " to learn more!";
			}

			// Token: 0x02002D77 RID: 11639
			public class DUPLICANT_CRASH_LANDED
			{
				// Token: 0x0400B8DD RID: 47325
				public static LocString NAME = "Duplicant Crash Landed!";

				// Token: 0x0400B8DE RID: 47326
				public static LocString TOOLTIP = "A Duplicant has successfully crashed an Escape Pod onto the surface of a nearby Planetoid.";
			}
		}

		// Token: 0x02001DAE RID: 7598
		public class TUTORIAL
		{
			// Token: 0x0400863A RID: 34362
			public static LocString DONT_SHOW_AGAIN = "Don't Show Again";
		}

		// Token: 0x02001DAF RID: 7599
		public class PLACERS
		{
			// Token: 0x02002D78 RID: 11640
			public class DIGPLACER
			{
				// Token: 0x0400B8DF RID: 47327
				public static LocString NAME = "Dig";
			}

			// Token: 0x02002D79 RID: 11641
			public class MOPPLACER
			{
				// Token: 0x0400B8E0 RID: 47328
				public static LocString NAME = "Mop";
			}
		}

		// Token: 0x02001DB0 RID: 7600
		public class MONUMENT_COMPLETE
		{
			// Token: 0x0400863B RID: 34363
			public static LocString NAME = "Great Monument";

			// Token: 0x0400863C RID: 34364
			public static LocString DESC = "A feat of artistic vision and expert engineering that will doubtless inspire Duplicants for thousands of cycles to come";
		}
	}
}
