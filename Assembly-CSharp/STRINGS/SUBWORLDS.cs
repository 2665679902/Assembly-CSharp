﻿using System;

namespace STRINGS
{
	// Token: 0x02000D4D RID: 3405
	public class SUBWORLDS
	{
		// Token: 0x02001E07 RID: 7687
		public static class BARREN
		{
			// Token: 0x040086F3 RID: 34547
			public static LocString NAME = "Barren";

			// Token: 0x040086F4 RID: 34548
			public static LocString DESC = string.Concat(new string[]
			{
				"Initial scans of this biome yield no signs of either ",
				UI.FormatAsLink("plant life", "PLANTS"),
				" or ",
				UI.FormatAsLink("critters", "CREATURES"),
				". It is a land devoid of ",
				UI.FormatAsLink("liquids", "ELEMENTS_LIQUID"),
				" and minuscule ",
				UI.FormatAsLink("gas", "ELEMENTS_GAS"),
				" deposits. These dry, dusty plains can be mined for building materials but there is little in the way of life sustaining resources here for a colony."
			});

			// Token: 0x040086F5 RID: 34549
			public static LocString UTILITY = string.Concat(new string[]
			{
				"The layers of sedimentary rock are predominantly made up of ",
				UI.FormatAsLink("Granite", "GRANITE"),
				" deposits, although ",
				UI.FormatAsLink("Obsidian", "OBSIDIAN"),
				" and ",
				UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK"),
				" are also present. This suggests a history of volcanic activity, though no volcanoes exist here currently.\n\nVeins of ",
				UI.FormatAsLink("Iron Ore", "IRON"),
				" deposits are evident from initial scans, as are deposits of ",
				UI.FormatAsLink("Coal", "CARBON"),
				". Both are useful in setting up a colony's power infrastructure.\n\nThough it lacks the crucial resources necessary to sustain a colony, there is nothing inherently dangerous here to harm my Duplicants. It should be safe enough to explore."
			});
		}

		// Token: 0x02001E08 RID: 7688
		public static class FOREST
		{
			// Token: 0x040086F6 RID: 34550
			public static LocString NAME = "Forest";

			// Token: 0x040086F7 RID: 34551
			public static LocString DESC = "Temperate and filled with unique " + UI.FormatAsLink("Plant", "PLANTS") + " life, this biome contains all the necessities for life support, although not in quantities sufficient to sustain a long term colony. Exploration into neighboring biomes should be a priority.";

			// Token: 0x040086F8 RID: 34552
			public static LocString UTILITY = string.Concat(new string[]
			{
				"Small pockets of ",
				UI.FormatAsLink("Oxylite", "OXYROCK"),
				" and ",
				UI.FormatAsLink("Water", "WATER"),
				" are present in the Forest Biome, but calculations reveal they will only sustain the colony for a limited time.\n\nAnalysis shows two native plants which should be prioritized for cultivation: The ",
				UI.FormatAsLink("Oxyfern", "OXYFERN"),
				", which releases ",
				UI.FormatAsLink("Oxygen", "OXYGEN"),
				" but requires ",
				UI.FormatAsLink("Water", "WATER"),
				"; and the ",
				UI.FormatAsLink("Arbor Tree", "FOREST_TREE"),
				" which provides lumber as a fuel source.\n\nA symbiotic relationship exists with the ",
				UI.FormatAsLink("Arbor Tree", "FOREST_TREE"),
				" and the native ",
				UI.FormatAsLink("Pips", "SQUIRRELSPECIES"),
				" which appear to be the only critter that can find the elusive ",
				UI.FormatAsLink("Arbor Acorns", "PLANTS"),
				".\n\nThis biome is really quite beautiful. I've noted that ",
				UI.FormatAsLink("Shine Bugs", "LIGHTBUGSPECIES"),
				" and ",
				UI.FormatAsLink("Mirth Leaf", "LEAFYPLANT"),
				" both evoke feelings of serenity in my Duplicants."
			});
		}

		// Token: 0x02001E09 RID: 7689
		public static class FROZEN
		{
			// Token: 0x040086F9 RID: 34553
			public static LocString NAME = "Tundra";

			// Token: 0x040086FA RID: 34554
			public static LocString DESC = string.Concat(new string[]
			{
				"The sub-zero temperatures of the Tundra Biome provide rare frozen deposits of ",
				UI.FormatAsLink("Ice", "ICE"),
				" and ",
				UI.FormatAsLink("Snow", "SNOW"),
				", necessary for a colony's ",
				UI.FormatAsLink("Heat", "HEAT"),
				" regulation needs."
			});

			// Token: 0x040086FB RID: 34555
			public static LocString UTILITY = string.Concat(new string[]
			{
				"Far from devoid of life, this biome contains some much needed plant life, ripe for cultivation. ",
				UI.FormatAsLink("Sleet Wheat", "COLDWHEAT"),
				" provides a nutrient rich ingredient for creating complex foods, though the plants do require sub-zero temperatures to thrive.\n\nFortunately ",
				UI.FormatAsLink("Wheezewort", "COLDBREATHER"),
				" can been planted on farms to lower surrounding temperatures.\n\nCrucially, small deposits of ",
				UI.FormatAsLink("Wolframite", "WOLFRAMITE"),
				" have been detected here. This is an extremely rare metal that should be preserved for ",
				UI.FormatAsLink("Tungsten", "TUNGSTEN"),
				"production.\n\nThough my Duplicants appear more than happy to work in the Tundra Biome for short periods of time, I will need to provide proper ",
				UI.FormatAsLink("equipment", "EQUIPMENT"),
				" for them to avoid adverse affects to their well-being if they are working here for longer periods."
			});
		}

		// Token: 0x02001E0A RID: 7690
		public static class JUNGLE
		{
			// Token: 0x040086FC RID: 34556
			public static LocString NAME = "Jungle";

			// Token: 0x040086FD RID: 34557
			public static LocString DESC = string.Concat(new string[]
			{
				"Initial investigations of the Jungle Biome reveal an ecosystem filled with unique flora but centered around ",
				UI.FormatAsLink("Chlorine", "CHLORINE"),
				" and ",
				UI.FormatAsLink("Hydrogen", "HYDROGEN"),
				" gas, toxic to Duplicants. When exploring here, it is worth setting up a good system."
			});

			// Token: 0x040086FE RID: 34558
			public static LocString UTILITY = string.Concat(new string[]
			{
				"The ",
				UI.FormatAsLink("Drecko", "DRECKOSPECIES"),
				" is a relatively benign critter which can be domesticated to aid in textile and food production.\n\nThe ",
				UI.FormatAsLink("Morb", "GLOMSPECIES"),
				" only function is to produce ",
				UI.FormatAsLink("Polluted Oxygen", "POLLUTEDOXYGEN"),
				" which may be useful when establishing sustainable production loops with other critters.\n\n",
				UI.FormatAsLink("Balm Lilies", "SWAMPLILY"),
				" would be useful to cultivate for the production of critical medical materials.\n\n",
				UI.FormatAsLink("Pincha Pepperplants", "SPICE_VINE"),
				" greatly improve the nutritional and gratification of a colony's food supply. Because of their unique relationship with gravity, the plants must be orientated upside-down for proper growing. This can be accomplished by using a ",
				UI.FormatAsLink("Farm Tile", "FARMTILE"),
				".\n\nGiven the toxic gases I will have to build proper ",
				UI.FormatAsLink("Ventilation", "BUILDCATEGORYHVAC"),
				" system to both protect my Duplicants and provide the optimum environments for the native plants and critters."
			});
		}

		// Token: 0x02001E0B RID: 7691
		public static class MAGMA
		{
			// Token: 0x040086FF RID: 34559
			public static LocString NAME = "Magma";

			// Token: 0x04008700 RID: 34560
			public static LocString DESC = "Temperatures in the Magma Biome can reach upwards of 1526 degrees, making it a reliable source of extreme heat that can be exploited for the purposes of producing " + UI.FormatAsLink("Power", "POWER") + " and fuel.";

			// Token: 0x04008701 RID: 34561
			public static LocString UTILITY = string.Concat(new string[]
			{
				UI.FormatAsLink("Magma", "MAGMA"),
				" is source of extreme ",
				UI.FormatAsLink("Heat", "HEAT"),
				" which can be used to transform ",
				UI.FormatAsLink("Water", "WATER"),
				" in to ",
				UI.FormatAsLink("Steam", "STEAM"),
				" or ",
				UI.FormatAsLink("Crude Oil", "CRUDEOIL"),
				" into ",
				UI.FormatAsLink("Petroleum", "PETROLEUM"),
				". In order to prevent the extreme temperatures of this biome invading other parts of my base, suitable insulation must be constructed using materials with high melting points like ",
				UI.FormatAsLink("Ceramic", "CERAMIC"),
				" or ",
				UI.FormatAsLink("Obsidian", "OBSIDIAN"),
				".\n\nThough ",
				UI.FormatAsLink("Exosuits", "EXOSUIT"),
				" will provide some protection for my Duplicants, there is still a danger they will overheat if spending an extended amount of time in this Biome. I should ensure that suitable medical facilities have been constructed nearby to take care of any medical emergencies."
			});
		}

		// Token: 0x02001E0C RID: 7692
		public static class MARSH
		{
			// Token: 0x04008702 RID: 34562
			public static LocString NAME = "Marsh";

			// Token: 0x04008703 RID: 34563
			public static LocString DESC = UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + " dominates the atmosphere of the Marsh Biome as it escapes from the " + UI.FormatAsLink("Slime", "SLIMEMOLD") + " this biome is known for.";

			// Token: 0x04008704 RID: 34564
			public static LocString UTILITY = string.Concat(new string[]
			{
				"Marsh Biomes contain large amounts of ",
				UI.FormatAsLink("Slime", "SLIMEMOLD"),
				" which can be converted into ",
				UI.FormatAsLink("Algae", "ALGAE"),
				" and provide a valuable resource for growing ",
				UI.FormatAsLink("Dusk Caps", "MUSHROOMPLANT"),
				" as well as feeding to ",
				UI.FormatAsLink("Pacus", "PACUSPECIES"),
				" for producing some higher tier ",
				UI.FormatAsLink("food", "FOOD"),
				".\n\nBecause of the high degree of probability that this biome will infect my Duplicants with ",
				UI.FormatAsLink("Slimelung", "SLIMELUNG"),
				", it may be prudent to limit access to this area to essential activities only until my Duplicants are able to set up suitable protection."
			});
		}

		// Token: 0x02001E0D RID: 7693
		public static class METALLIC
		{
			// Token: 0x04008705 RID: 34565
			public static LocString NAME = "Metallic";

			// Token: 0x04008706 RID: 34566
			public static LocString DESC = "A plethora of metals pervade the Metallic Biome making it the go-to destination for a colony ramping up production for technological advancement.";

			// Token: 0x04008707 RID: 34567
			public static LocString UTILITY = string.Concat(new string[]
			{
				UI.FormatAsLink("Gold Amalgam", "GOLDAMALGAM"),
				" ",
				UI.FormatAsLink("Aluminum Ore", "ALUMINUMORE"),
				" and ",
				UI.FormatAsLink("Cobalt Ore", "COBALTITE"),
				" are in abundant supply throughout this entire biome. Refining these metals with a ",
				UI.FormatAsLink("Metal Refinery", "METALREFINERY"),
				" will make them available for building advanced technologies.\n\nThough ",
				UI.FormatAsLink("Chlorine", "CHLORINEGAS"),
				" and ",
				UI.FormatAsLink("Hydrogen", "HYDROGEN"),
				" are the prevailing gasses in this biome, ",
				UI.FormatAsLink("Oxylite", "OXYROCK"),
				" exists in rock form and can provide ",
				UI.FormatAsLink("Oxygen", "OXYGEN"),
				" for Duplicants once they uncover it.\n\n",
				UI.FormatAsLink("Dirt", "DIRT"),
				", ",
				UI.FormatAsLink("Coal", "CARBON"),
				" and ",
				UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK"),
				" round out the rest of this biome, making it a great deposit of resources for a budding industrialized colony."
			});
		}

		// Token: 0x02001E0E RID: 7694
		public static class OCEAN
		{
			// Token: 0x04008708 RID: 34568
			public static LocString NAME = "Ocean";

			// Token: 0x04008709 RID: 34569
			public static LocString DESC = string.Concat(new string[]
			{
				UI.FormatAsLink("Sand", "SAND"),
				", ",
				UI.FormatAsLink("Salt", "SALT"),
				" and ",
				UI.FormatAsLink("Bleachstone", "BLEACHSTONE"),
				" abound in this unique ",
				UI.FormatAsLink("briny", "BRINE"),
				" biome."
			});

			// Token: 0x0400870A RID: 34570
			public static LocString UTILITY = string.Concat(new string[]
			{
				UI.FormatAsLink("Pokeshell", "CRABSPECIES"),
				" molt is an excellent source of ",
				UI.FormatAsLink("Lime", "LIME"),
				" but much care must be taken with domesticating this species as it can get aggressive around its eggs.\n\nHarvesting ",
				UI.FormatAsLink("Waterweed", "SEALETTUCE"),
				" provides ",
				UI.FormatAsLink("Lettuce", "LETTUCE"),
				" for many higher-tier ",
				UI.FormatAsLink("foods", "FOOD"),
				".\n\nAny water will need to be filtered through a ",
				UI.FormatAsLink("Desalinator", "DESALINATOR"),
				" to remove the ",
				UI.FormatAsLink("Salt", "SALT"),
				" from ",
				UI.FormatAsLink("Water", "WATER"),
				" in order to be useful for my Duplicants. Luckily ",
				UI.FormatAsLink("Table Salt", "SALT"),
				" can be produced using a ",
				UI.FormatAsLink("Rock Crusher", "ROCKCRUSHER"),
				" which, when combined with a ",
				UI.FormatAsLink("Mess Table", "DININGTABLE"),
				", gives my Duplicants a Morale boost."
			});
		}

		// Token: 0x02001E0F RID: 7695
		public static class OIL
		{
			// Token: 0x0400870B RID: 34571
			public static LocString NAME = "Oily";

			// Token: 0x0400870C RID: 34572
			public static LocString DESC = string.Concat(new string[]
			{
				"Viscous pools of liquid ",
				UI.FormatAsLink("Crude Oil", "CRUDEOIL"),
				" pepper the ",
				UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
				" rich environment of the Oil Biome."
			});

			// Token: 0x0400870D RID: 34573
			public static LocString UTILITY = string.Concat(new string[]
			{
				"Though ",
				UI.FormatAsLink("Oxygen", "OXYGEN"),
				" is more scarce in this biome, it's the perfect place to cultivate flora and fauna that thrive in CO2, such as domesticating ",
				UI.FormatAsLink("Slicksters", "OILFLOATERSPECIES"),
				" to create a renewable source of oil.\n\n",
				UI.FormatAsLink("Diamond", "DIAMOND"),
				" deposits can occasionally be found in the Oily biome, which will require a Duplicant with the ",
				UI.FormatAsLink("Super-Duperhard Digging", "MINING3"),
				" skill to explore properly.\n\n",
				UI.FormatAsLink("Sporechids", "EVIL_FLOWER"),
				" are beautiful, but should only be approached if a Duplicant is properly ",
				UI.FormatAsLink("equipped", "EQUIPMENT"),
				".\n\nWhile the dangers of this biome should not be underestimated, the benefits ",
				UI.FormatAsLink("Crude Oil", "CRUDEOIL"),
				" and ",
				UI.FormatAsLink("Petroleum", "PETROLEUM"),
				" will bring to my colony far outweigh the risks."
			});
		}

		// Token: 0x02001E10 RID: 7696
		public static class RADIOACTIVE
		{
			// Token: 0x0400870E RID: 34574
			public static LocString NAME = "Radioactive";

			// Token: 0x0400870F RID: 34575
			public static LocString DESC = "A highly volatile environment containing a highly useful resource, this biome is invaluable when venturing into Nuclear technologies.";

			// Token: 0x04008710 RID: 34576
			public static LocString UTILITY = string.Concat(new string[]
			{
				UI.FormatAsLink("Lead Suits", "LEAD_SUIT"),
				" are imperative if my Duplicants are going to start exploring this biome as ",
				UI.FormatAsLink("Radioactive Contaminants", "RADIATIONSICKNESS"),
				" is a constant danger here.\n\n",
				UI.FormatAsLink("Beetas", "BEE"),
				" pose a double threat as they are both highly radioactive and very aggressive. If they can be domesticated, however, ",
				UI.FormatAsLink("Beeta Hives", "BEEHIVE"),
				", provide a great service turning ",
				UI.FormatAsLink("Uranium", "URANIUMORE"),
				" into ",
				UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM"),
				".\n\nWhile the Radioactive Biome, and the Beetas contained within it, should be avoided at all costs if my Duplicants do not have the correct protection, my colony will need to trek into this dangerous biome if we are going to build any higher tier nuclear technologies."
			});
		}

		// Token: 0x02001E11 RID: 7697
		public static class RUST
		{
			// Token: 0x04008711 RID: 34577
			public static LocString NAME = "Rust";

			// Token: 0x04008712 RID: 34578
			public static LocString DESC = "The orange-brown oasis of the Rust Biome is home to many unusual flora and fauna. It contains the resources for several intermediate technologies.";

			// Token: 0x04008713 RID: 34579
			public static LocString UTILITY = string.Concat(new string[]
			{
				"When combined with the ",
				UI.FormatAsLink("Rust Deoxidizer", "RUSTDEOXIDIZER"),
				", ",
				UI.FormatAsLink("Rust", "RUST"),
				" can produce many of a colony's basic needs.\n\nThe ",
				UI.FormatAsLink("Squeaky Puft", "PUFTBLEACHSTONE"),
				", a frequent resident of the Rust biome, are a renewable source of ",
				UI.FormatAsLink("Bleachstone", "BLEACHSTONE"),
				" and can be domesticated for such purposes.\n\n",
				UI.FormatAsLink("Dreckos", "DRECKOS"),
				" can also sometimes be found in these biomes, which are a great source of ",
				UI.FormatAsLink("Phosphorite", "PHOSPHORITE"),
				" and fibre for making ",
				UI.FormatAsLink("Textile Production", "CLOTHING"),
				"\n\nTwo plants found in this biome: the ",
				UI.FormatAsLink("Nosh Bean", "BEANPLANTSEED"),
				" and the ",
				UI.FormatAsLink("Dasha Saltvine", "SALTPLANT"),
				" can both produce food that will add significant Morale value for my Duplicants."
			});
		}

		// Token: 0x02001E12 RID: 7698
		public static class SANDSTONE
		{
			// Token: 0x04008714 RID: 34580
			public static LocString NAME = "Sandstone";

			// Token: 0x04008715 RID: 34581
			public static LocString DESC = "The Sandstone Biome is a temperate oasis with few inherent dangers. It's the perfect spot to get your colony up and running.";

			// Token: 0x04008716 RID: 34582
			public static LocString UTILITY = string.Concat(new string[]
			{
				UI.FormatAsLink("Oxylite", "OXYROCK"),
				" and ",
				UI.FormatAsLink("Buried Muckroot", "BASICFORAGEPLANTPLANTED"),
				" are in sufficient supply to sustain your colony while you gather more resources.\n\n",
				UI.FormatAsLink("Dirt", "DIRT"),
				", ",
				UI.FormatAsLink("Algae", "ALGAE"),
				", ",
				UI.FormatAsLink("Copper", "COPPER"),
				" and, of course, ",
				UI.FormatAsLink("Sandstone", "SANDSTONE"),
				" provide all the materials to get basic colony essentials built.\n\nRandom ",
				UI.FormatAsLink("Shine Bugs", "LIGHTBUGSPECIES"),
				" provide Morale boosts for my Duplicants but are not a reliable light source.\n\n",
				UI.FormatAsLink("Hatch", "HATCHSPECIES"),
				" can be domesticated for food or ",
				UI.FormatAsLink("Coal", "CARBON"),
				", which will be useful if using a ",
				UI.FormatAsLink("Coal Generator", "GENERATOR"),
				" for power.\n\nAll-in-all this biome is the perfect starting spot for my colony to establish a base full of essentials, from which they can then venture out and explore."
			});
		}

		// Token: 0x02001E13 RID: 7699
		public static class WASTELAND
		{
			// Token: 0x04008717 RID: 34583
			public static LocString NAME = "Wasteland";

			// Token: 0x04008718 RID: 34584
			public static LocString DESC = "While the Wasteland Biome does not look particularly interesting, a pragmatic colony can take advantage of its selection of construction resources.";

			// Token: 0x04008719 RID: 34585
			public static LocString UTILITY = string.Concat(new string[]
			{
				"The prevalance of ",
				UI.FormatAsLink("Copper", "COPPER"),
				", ",
				UI.FormatAsLink("Sandstone", "SANDSTONE"),
				", ",
				UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK"),
				" and its ",
				UI.FormatAsLink("Iron", "IRON"),
				" rich counterpart ",
				UI.FormatAsLink("Mafic Rock", "MAFICROCK"),
				", make this a fruitful biome to explore for construction material. ",
				UI.FormatAsLink("Sand", "SAND"),
				" is also in abundance here which is useful as a filtering material.\n\nWhile the wildlife is not in abundance in the Wasteland Biome, the ",
				UI.FormatAsLink("Sweetle", "DIVERGENTBEETLE"),
				" and ",
				UI.FormatAsLink("Grubgrub", "DIVERGENTWORM"),
				" make interesting creatures to domesticate as they co-exist with the ",
				UI.FormatAsLink("Grubfruit Plants", "WORMPLANT"),
				" to produce a much higher quality food than the ",
				UI.FormatAsLink("Spindly Grubfruit Plant", "WORMPLANT"),
				" found in the wild. Additionally, the ",
				UI.FormatAsLink("Sulfur", "SULFUR"),
				" found here works both as food for the GrubGrubs and fertilizer for the Grubfruit Plants.\n\nThe abundance of ",
				UI.FormatAsLink("Oxygen", "OXYGEN"),
				" found in the Wasteland Biome makes for a low-risk area to send my Duplicants into to collect useful resources to continue with their construction projects."
			});
		}

		// Token: 0x02001E14 RID: 7700
		public static class SPACE
		{
			// Token: 0x0400871A RID: 34586
			public static LocString NAME = "Space";

			// Token: 0x0400871B RID: 34587
			public static LocString DESC = "The Space Biome is located on the scenic surface of an asteroid. Watch for dazzling meteorites to shower elements down from the sky.";

			// Token: 0x0400871C RID: 34588
			public static LocString UTILITY = string.Concat(new string[]
			{
				"Setting up ",
				UI.FormatAsLink("Solar Panels", "SOLARPANELS"),
				" on the surface will provide a source of renewable energy. However, much care must be taken to ensure ",
				UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
				" or ",
				UI.FormatAsLink("Gases", "ELEMENTS_GAS"),
				" are not sucked out into the ",
				UI.FormatAsLink("Vacuum", "VACUUM"),
				" of space. ",
				UI.FormatAsLink("Shove Voles", "MOLE"),
				" are native to this biome, and need to be wrangled or contained or they will infest the colony."
			});
		}

		// Token: 0x02001E15 RID: 7701
		public static class SWAMP
		{
			// Token: 0x0400871D RID: 34589
			public static LocString NAME = "Swampy";

			// Token: 0x0400871E RID: 34590
			public static LocString DESC = string.Concat(new string[]
			{
				"With its abundence of ",
				UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN"),
				" and lack of clean ",
				UI.PRE_KEYWORD,
				"Water",
				UI.PST_KEYWORD,
				" the Swampy Biome presents some challenges for a budding colony. But, with a little hard work, it can also turn into a great starting biome with some valuable resources."
			});

			// Token: 0x0400871F RID: 34591
			public static LocString UTILITY = string.Concat(new string[]
			{
				UI.FormatAsLink("Swamp Chard", "SWAMPFORAGEPLANTPLANTED"),
				" can provide adequate nutrients for my Duplicants while they establish farms, but it cannot be planted or propogated. ",
				UI.FormatAsLink("Bog Buckets", "SWAMPHARVESTPLANT"),
				", however, provide a sweet source of nutrients that are fairly easy to farm using ",
				UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
				" and require no extra light to grow.\n\nFortunately ",
				UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
				" is abundant in this biome and will require a ",
				UI.FormatAsLink("Water Sieve", "WATERPURIFIER"),
				" to turn into something my Duplicants can drink. Additionally my Duplicants can use a ",
				UI.FormatAsLink("Sludge Press", "SLUDGEPRESS"),
				" to filter clean water from ",
				UI.FormatAsLink("Mud", "MUD"),
				" (NOTE: ",
				UI.FormatAsLink("Polluted Mud", "TOXICMUD"),
				", however, does not make clean water).\n\nMeanwhile, rudimentary power can be gained from the energy producing ",
				UI.FormatAsLink("Plug Slugs", "STATERPILLAR"),
				" that inhabit this biome.\n\nShiny ",
				UI.FormatAsLink("Cobalt Ore", "COBALTITE"),
				" can be found here, providing a adequate source of metal.\n\nWhile much of this biome is dangerous for a new colony with some deliberate research and careful planning my Duplicants can not only survive but thrive here."
			});
		}

		// Token: 0x02001E16 RID: 7702
		public static class NIOBIUM
		{
			// Token: 0x04008720 RID: 34592
			public static LocString NAME = "Niobium";

			// Token: 0x04008721 RID: 34593
			public static LocString DESC = "The Niobium Biome features only two resources yet, because " + UI.FormatAsLink("Niobium", "NIOBIUM") + " is an extremely rare and valuable element, it is worth making a special visit.";

			// Token: 0x04008722 RID: 34594
			public static LocString UTILITY = string.Concat(new string[]
			{
				"By itself ",
				UI.FormatAsLink("Niobium", "NIOBIUM"),
				" is not a particularly useful resource, but if processed through a ",
				UI.FormatAsLink("Metal Refinery", "METALREFINERY"),
				" it produces the extremely thermal conductive ",
				UI.FormatAsLink("Thermium", "TEMPCONDUCTORSOLID"),
				" which goes a long way in solving many extreme temperature issues in a colony.\n\nThe edges of this biome are filled with ",
				UI.FormatAsLink("Obsidian", "OBSIDIAN"),
				" so a Duplicant with the ",
				UI.FormatAsLink("Super-Duperhard Digging", "MINING3"),
				" skill will be required before my colony can explore here."
			});
		}

		// Token: 0x02001E17 RID: 7703
		public static class AQUATIC
		{
			// Token: 0x04008723 RID: 34595
			public static LocString NAME = "Aquatic";

			// Token: 0x04008724 RID: 34596
			public static LocString DESC = "The Aquatic Biome is flush with a huge deposit of precious " + UI.FormatAsLink("Water", "WATER") + ".";

			// Token: 0x04008725 RID: 34597
			public static LocString UTILITY = string.Concat(new string[]
			{
				"Initially there is very little solid ground in this biome to establish a temporary base, but once a transportation network can be established to send the ",
				UI.FormatAsLink("Water", "WATER"),
				" of the Aquatic Biome to the rest of the colony, the other elements will be easier to reach.\n\n",
				UI.FormatAsLink("Sandstone", "SANDSTONE"),
				", ",
				UI.FormatAsLink("Mafic Rock", "MAFICROCK"),
				", ",
				UI.FormatAsLink("Sand", "SAND"),
				" and ",
				UI.FormatAsLink("Sedimentary Rock", "SEDIMENTARYROCK"),
				" provide readily available construction materials for setting up elementary infrastructure. The presence of ",
				UI.FormatAsLink("Oxylite", "OXYROCK"),
				" provides invaluable ",
				UI.FormatAsLink("Oxygen", "OXYGEN"),
				" which, through careful planning, should be able to sustain any Duplicants working in the area for a limited amount of time."
			});
		}

		// Token: 0x02001E18 RID: 7704
		public static class MOO
		{
			// Token: 0x04008726 RID: 34598
			public static LocString NAME = "Moo";

			// Token: 0x04008727 RID: 34599
			public static LocString DESC = string.Concat(new string[]
			{
				"The Moo Biome is the natural habitat of the charismatic ",
				UI.FormatAsLink("Gassy Moo", "MOO"),
				", a great source of ",
				UI.FormatAsLink("Natural Gas", "METHANE"),
				"."
			});

			// Token: 0x04008728 RID: 34600
			public static LocString UTILITY = string.Concat(new string[]
			{
				"In addition to ",
				UI.FormatAsLink("Natural Gas", "METHANE"),
				", the highly toxic ",
				UI.FormatAsLink("Chlorine", "CHLORINEGAS"),
				" is also present in gas form. In fact, Chlorine is present here in ",
				UI.FormatAsLink("gas", "ELEMENTS_GAS"),
				", ",
				UI.FormatAsLink("liquid", "ELEMENTS_LIQUID"),
				", and ",
				UI.FormatAsLink("solid", "ELEMENTS_SOLID"),
				" states, largely due to the presence of ",
				UI.FormatAsLink("Bleach Stone", "BLEACHSTONE"),
				".\n\n",
				UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK"),
				" and its denser form, ",
				UI.FormatAsLink("Granite", "GRANITE"),
				", provide some useful construction materials, but the real star of this biome are the ",
				UI.FormatAsLink("Gassy Moos", "MOO"),
				" who consume ",
				UI.FormatAsLink("Gas Grass", "GASGRASS"),
				" and excrete ",
				UI.FormatAsLink("Natural Gas", "METHANE"),
				". While Gassy Moos cannot be bred domestically, Gassy Mooteors regularly fall from space onto this biome, making it the best way to find a reliable source of these elusive creatures.\n\nWith no breathable ",
				UI.FormatAsLink("Oxygen", "OXYGEN"),
				" in this biome my Duplicants will need to be properly prepared before they venture too far into its depths."
			});
		}

		// Token: 0x02001E19 RID: 7705
		public static class REGOLITH
		{
			// Token: 0x04008729 RID: 34601
			public static LocString NAME = "Regolith";

			// Token: 0x0400872A RID: 34602
			public static LocString DESC = "The Regolith Biome contains a bounty of " + UI.FormatAsLink("Regolith", "REGOLITH") + " which is very useful as a material for filtration.";

			// Token: 0x0400872B RID: 34603
			public static LocString UTILITY = string.Concat(new string[]
			{
				"Lingering within the layers of Regolith are the pernicious ",
				UI.FormatAsLink("Shove Voles", "MOLESPECIES"),
				" which eat valuable resources and excrete them at half the original mass.\n\nFortunately these pests can be wrangled up and used as a good food source for my Duplicants. However, extra care must be taken to contain these critters in pens made from either double thick walls or from ",
				UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
				" since they are capable of burrowing through most other materials."
			});
		}
	}
}
