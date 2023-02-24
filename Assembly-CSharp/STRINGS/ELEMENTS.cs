using System;

namespace STRINGS
{
	// Token: 0x02000D47 RID: 3399
	public class ELEMENTS
	{
		// Token: 0x04004E52 RID: 20050
		public static LocString ELEMENTDESCSOLID = "Resource Type: {0}\nMelting point: {1}\nHardness: {2}";

		// Token: 0x04004E53 RID: 20051
		public static LocString ELEMENTDESCLIQUID = "Resource Type: {0}\nFreezing point: {1}\nEvaporation point: {2}";

		// Token: 0x04004E54 RID: 20052
		public static LocString ELEMENTDESCGAS = "Resource Type: {0}\nCondensation point: {1}";

		// Token: 0x04004E55 RID: 20053
		public static LocString ELEMENTDESCVACUUM = "Resource Type: {0}";

		// Token: 0x04004E56 RID: 20054
		public static LocString BREATHABLEDESC = "<color=#{0}>({1})</color>";

		// Token: 0x04004E57 RID: 20055
		public static LocString THERMALPROPERTIES = "\nSpecific Heat Capacity: {SPECIFIC_HEAT_CAPACITY}\nThermal Conductivity: {THERMAL_CONDUCTIVITY}";

		// Token: 0x04004E58 RID: 20056
		public static LocString RADIATIONPROPERTIES = "Radiation Absorption Factor: {0}\nRadiation Emission/1000kg: {1}";

		// Token: 0x04004E59 RID: 20057
		public static LocString ELEMENTPROPERTIES = "Properties: {0}";

		// Token: 0x02001CFC RID: 7420
		public class STATE
		{
			// Token: 0x0400845A RID: 33882
			public static LocString SOLID = "Solid";

			// Token: 0x0400845B RID: 33883
			public static LocString LIQUID = "Liquid";

			// Token: 0x0400845C RID: 33884
			public static LocString GAS = "Gas";

			// Token: 0x0400845D RID: 33885
			public static LocString VACUUM = "None";
		}

		// Token: 0x02001CFD RID: 7421
		public class MATERIAL_MODIFIERS
		{
			// Token: 0x0400845E RID: 33886
			public static LocString EFFECTS_HEADER = "<b>Resource Effects:</b>";

			// Token: 0x0400845F RID: 33887
			public static LocString DECOR = UI.FormatAsLink("Decor", "DECOR") + ": {0}";

			// Token: 0x04008460 RID: 33888
			public static LocString OVERHEATTEMPERATURE = UI.FormatAsLink("Overheat Temperature", "HEAT") + ": {0}";

			// Token: 0x04008461 RID: 33889
			public static LocString HIGH_THERMAL_CONDUCTIVITY = UI.FormatAsLink("High Thermal Conductivity", "HEAT");

			// Token: 0x04008462 RID: 33890
			public static LocString LOW_THERMAL_CONDUCTIVITY = UI.FormatAsLink("Insulator", "HEAT");

			// Token: 0x04008463 RID: 33891
			public static LocString LOW_SPECIFIC_HEAT_CAPACITY = UI.FormatAsLink("Thermally Reactive", "HEAT");

			// Token: 0x04008464 RID: 33892
			public static LocString HIGH_SPECIFIC_HEAT_CAPACITY = UI.FormatAsLink("Slow Heating", "HEAT");

			// Token: 0x04008465 RID: 33893
			public static LocString EXCELLENT_RADIATION_SHIELD = UI.FormatAsLink("Excellent Radiation Shield", "RADIATION");

			// Token: 0x02002CDD RID: 11485
			public class TOOLTIP
			{
				// Token: 0x0400B77A RID: 46970
				public static LocString EFFECTS_HEADER = "Buildings constructed from this material will have these properties";

				// Token: 0x0400B77B RID: 46971
				public static LocString DECOR = "This material will add <b>{0}</b> to the finished building's " + UI.PRE_KEYWORD + "Decor" + UI.PST_KEYWORD;

				// Token: 0x0400B77C RID: 46972
				public static LocString OVERHEATTEMPERATURE = "This material will add <b>{0}</b> to the finished building's " + UI.PRE_KEYWORD + "Overheat Temperature" + UI.PST_KEYWORD;

				// Token: 0x0400B77D RID: 46973
				public static LocString HIGH_THERMAL_CONDUCTIVITY = string.Concat(new string[]
				{
					"This material disperses ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" because energy transfers quickly through materials with high ",
					UI.PRE_KEYWORD,
					"Thermal Conductivity",
					UI.PST_KEYWORD,
					"\n\nBetween two objects, the rate of ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" transfer will be determined by the object with the <i>lowest</i> ",
					UI.PRE_KEYWORD,
					"Thermal Conductivity",
					UI.PST_KEYWORD,
					"\n\nThermal Conductivity: {1} W per degree K difference (Oxygen: 0.024 W)"
				});

				// Token: 0x0400B77E RID: 46974
				public static LocString LOW_THERMAL_CONDUCTIVITY = string.Concat(new string[]
				{
					"This material retains ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" because energy transfers slowly through materials with low ",
					UI.PRE_KEYWORD,
					"Thermal Conductivity",
					UI.PST_KEYWORD,
					"\n\nBetween two objects, the rate of ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" transfer will be determined by the object with the <i>lowest</i> ",
					UI.PRE_KEYWORD,
					"Thermal Conductivity",
					UI.PST_KEYWORD,
					"\n\nThermal Conductivity: {1} W per degree K difference (Oxygen: 0.024 W)"
				});

				// Token: 0x0400B77F RID: 46975
				public static LocString LOW_SPECIFIC_HEAT_CAPACITY = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"Thermally Reactive",
					UI.PST_KEYWORD,
					" materials require little energy to raise in ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					", and therefore heat and cool quickly\n\nSpecific Heat Capacity: {1} DTU to raise 1g by 1K"
				});

				// Token: 0x0400B780 RID: 46976
				public static LocString HIGH_SPECIFIC_HEAT_CAPACITY = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"Slow Heating",
					UI.PST_KEYWORD,
					" materials require a large amount of energy to raise in ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					", and therefore heat and cool slowly\n\nSpecific Heat Capacity: {1} DTU to raise 1g by 1K"
				});

				// Token: 0x0400B781 RID: 46977
				public static LocString EXCELLENT_RADIATION_SHIELD = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"Excellent Radiation Shield",
					UI.PST_KEYWORD,
					" radiation has a hard time passing through materials with a high ",
					UI.PRE_KEYWORD,
					"Radiation Absorption Factor",
					UI.PST_KEYWORD,
					" value. \n\nRadiation Absorption Factor: {1}"
				});
			}
		}

		// Token: 0x02001CFE RID: 7422
		public class HARDNESS
		{
			// Token: 0x04008466 RID: 33894
			public static LocString NA = "N/A";

			// Token: 0x04008467 RID: 33895
			public static LocString SOFT = "{0} (" + ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.SOFT + ")";

			// Token: 0x04008468 RID: 33896
			public static LocString VERYSOFT = "{0} (" + ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.VERYSOFT + ")";

			// Token: 0x04008469 RID: 33897
			public static LocString FIRM = "{0} (" + ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.FIRM + ")";

			// Token: 0x0400846A RID: 33898
			public static LocString VERYFIRM = "{0} (" + ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.VERYFIRM + ")";

			// Token: 0x0400846B RID: 33899
			public static LocString NEARLYIMPENETRABLE = "{0} (" + ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.NEARLYIMPENETRABLE + ")";

			// Token: 0x0400846C RID: 33900
			public static LocString IMPENETRABLE = "{0} (" + ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.IMPENETRABLE + ")";

			// Token: 0x02002CDE RID: 11486
			public class HARDNESS_DESCRIPTOR
			{
				// Token: 0x0400B782 RID: 46978
				public static LocString SOFT = "Soft";

				// Token: 0x0400B783 RID: 46979
				public static LocString VERYSOFT = "Very Soft";

				// Token: 0x0400B784 RID: 46980
				public static LocString FIRM = "Firm";

				// Token: 0x0400B785 RID: 46981
				public static LocString VERYFIRM = "Very Firm";

				// Token: 0x0400B786 RID: 46982
				public static LocString NEARLYIMPENETRABLE = "Nearly Impenetrable";

				// Token: 0x0400B787 RID: 46983
				public static LocString IMPENETRABLE = "Impenetrable";
			}
		}

		// Token: 0x02001CFF RID: 7423
		public class AEROGEL
		{
			// Token: 0x0400846D RID: 33901
			public static LocString NAME = UI.FormatAsLink("Aerogel", "AEROGEL");

			// Token: 0x0400846E RID: 33902
			public static LocString DESC = "";
		}

		// Token: 0x02001D00 RID: 7424
		public class ALGAE
		{
			// Token: 0x0400846F RID: 33903
			public static LocString NAME = UI.FormatAsLink("Algae", "ALGAE");

			// Token: 0x04008470 RID: 33904
			public static LocString DESC = "Algae is a cluster of non-motile, single-celled lifeforms.\n\nIt can be used to produce " + ELEMENTS.OXYGEN.NAME + " when used in a " + BUILDINGS.PREFABS.MINERALDEOXIDIZER.NAME;
		}

		// Token: 0x02001D01 RID: 7425
		public class ALUMINUMORE
		{
			// Token: 0x04008471 RID: 33905
			public static LocString NAME = UI.FormatAsLink("Aluminum Ore", "ALUMINUMORE");

			// Token: 0x04008472 RID: 33906
			public static LocString DESC = "Aluminum ore, also known as Bauxite, is a sedimentary rock high in metal content.\n\nIt can be refined into " + UI.FormatAsLink("Aluminum", "ALUMINUM") + ".";
		}

		// Token: 0x02001D02 RID: 7426
		public class ALUMINUM
		{
			// Token: 0x04008473 RID: 33907
			public static LocString NAME = UI.FormatAsLink("Aluminum", "ALUMINUM");

			// Token: 0x04008474 RID: 33908
			public static LocString DESC = string.Concat(new string[]
			{
				"(Al) Aluminum is a low density ",
				UI.FormatAsLink("Metal", "REFINEDMETAL"),
				".\n\nIt has high Thermal Conductivity and is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02001D03 RID: 7427
		public class MOLTENALUMINUM
		{
			// Token: 0x04008475 RID: 33909
			public static LocString NAME = UI.FormatAsLink("Aluminum", "MOLTENALUMINUM");

			// Token: 0x04008476 RID: 33910
			public static LocString DESC = string.Concat(new string[]
			{
				"(Al) Aluminum is a low density ",
				UI.FormatAsLink("Metal", "REFINEDMETAL"),
				" heated into a molten ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02001D04 RID: 7428
		public class ALUMINUMGAS
		{
			// Token: 0x04008477 RID: 33911
			public static LocString NAME = UI.FormatAsLink("Aluminum", "ALUMINUMGAS");

			// Token: 0x04008478 RID: 33912
			public static LocString DESC = string.Concat(new string[]
			{
				"(Al) Aluminum is a low density ",
				UI.FormatAsLink("Metal", "REFINEDMETAL"),
				" heated into a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x02001D05 RID: 7429
		public class BLEACHSTONE
		{
			// Token: 0x04008479 RID: 33913
			public static LocString NAME = UI.FormatAsLink("Bleach Stone", "BLEACHSTONE");

			// Token: 0x0400847A RID: 33914
			public static LocString DESC = string.Concat(new string[]
			{
				"Bleach stone is an unstable compound that emits unbreathable ",
				UI.FormatAsLink("Chlorine", "CHLORINEGAS"),
				".\n\nIt is useful in ",
				UI.FormatAsLink("Hygienic", "HYGIENE"),
				" processes."
			});
		}

		// Token: 0x02001D06 RID: 7430
		public class BITUMEN
		{
			// Token: 0x0400847B RID: 33915
			public static LocString NAME = UI.FormatAsLink("Bitumen", "BITUMEN");

			// Token: 0x0400847C RID: 33916
			public static LocString DESC = "Bitumen is a sticky viscous residue left behind from " + ELEMENTS.PETROLEUM.NAME + " production.";
		}

		// Token: 0x02001D07 RID: 7431
		public class BOTTLEDWATER
		{
			// Token: 0x0400847D RID: 33917
			public static LocString NAME = UI.FormatAsLink("Water", "BOTTLEDWATER");

			// Token: 0x0400847E RID: 33918
			public static LocString DESC = "(H<sub>2</sub>O) Clean " + ELEMENTS.WATER.NAME + ", prepped for transport.";
		}

		// Token: 0x02001D08 RID: 7432
		public class BRINEICE
		{
			// Token: 0x0400847F RID: 33919
			public static LocString NAME = UI.FormatAsLink("Brine Ice", "BRINEICE");

			// Token: 0x04008480 RID: 33920
			public static LocString DESC = string.Concat(new string[]
			{
				"Brine is a natural, highly concentrated solution of ",
				UI.FormatAsLink("Salt", "SALT"),
				" dissolved in ",
				UI.FormatAsLink("Water", "WATER"),
				".\n\nIt can be used in desalination processes, separating out usable salt."
			});
		}

		// Token: 0x02001D09 RID: 7433
		public class BRINE
		{
			// Token: 0x04008481 RID: 33921
			public static LocString NAME = UI.FormatAsLink("Brine", "BRINE");

			// Token: 0x04008482 RID: 33922
			public static LocString DESC = string.Concat(new string[]
			{
				"Brine is a natural, highly concentrated solution of ",
				UI.FormatAsLink("Salt", "SALT"),
				" dissolved in ",
				UI.FormatAsLink("Water", "WATER"),
				".\n\nIt can be used in desalination processes, separating out usable salt."
			});
		}

		// Token: 0x02001D0A RID: 7434
		public class CARBON
		{
			// Token: 0x04008483 RID: 33923
			public static LocString NAME = UI.FormatAsLink("Coal", "CARBON");

			// Token: 0x04008484 RID: 33924
			public static LocString DESC = "(C) Coal is a combustible fossil fuel composed of carbon.\n\nIt is useful in " + UI.FormatAsLink("Power", "POWER") + " production.";
		}

		// Token: 0x02001D0B RID: 7435
		public class REFINEDCARBON
		{
			// Token: 0x04008485 RID: 33925
			public static LocString NAME = UI.FormatAsLink("Refined Carbon", "REFINEDCARBON");

			// Token: 0x04008486 RID: 33926
			public static LocString DESC = "(C) Refined carbon is solid element purified from raw " + ELEMENTS.CARBON.NAME + ".";
		}

		// Token: 0x02001D0C RID: 7436
		public class ETHANOLGAS
		{
			// Token: 0x04008487 RID: 33927
			public static LocString NAME = UI.FormatAsLink("Ethanol", "ETHANOLGAS");

			// Token: 0x04008488 RID: 33928
			public static LocString DESC = "(C<sub>2</sub>H<sub>6</sub>O) Ethanol is an advanced chemical compound heated into a " + UI.FormatAsLink("Gaseous", "ELEMENTS_GAS") + " state.";
		}

		// Token: 0x02001D0D RID: 7437
		public class ETHANOL
		{
			// Token: 0x04008489 RID: 33929
			public static LocString NAME = UI.FormatAsLink("Ethanol", "ETHANOL");

			// Token: 0x0400848A RID: 33930
			public static LocString DESC = "(C<sub>2</sub>H<sub>6</sub>O) Ethanol is an advanced chemical compound.\n\nIt can be used as a highly effective fuel source when burned.";
		}

		// Token: 0x02001D0E RID: 7438
		public class SOLIDETHANOL
		{
			// Token: 0x0400848B RID: 33931
			public static LocString NAME = UI.FormatAsLink("Ethanol", "SOLIDETHANOL");

			// Token: 0x0400848C RID: 33932
			public static LocString DESC = "(C<sub>2</sub>H<sub>6</sub>O) Ethanol is an advanced chemical compound.\n\nIt can be used as a highly effective fuel source when burned.";
		}

		// Token: 0x02001D0F RID: 7439
		public class CARBONDIOXIDE
		{
			// Token: 0x0400848D RID: 33933
			public static LocString NAME = UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE");

			// Token: 0x0400848E RID: 33934
			public static LocString DESC = "(CO<sub>2</sub>) Carbon Dioxide is an atomically heavy chemical compound in a " + UI.FormatAsLink("Gaseous", "ELEMENTS_GAS") + " state.\n\nIt tends to sink below other gases.";
		}

		// Token: 0x02001D10 RID: 7440
		public class CARBONFIBRE
		{
			// Token: 0x0400848F RID: 33935
			public static LocString NAME = UI.FormatAsLink("Carbon Fiber", "CARBONFIBRE");

			// Token: 0x04008490 RID: 33936
			public static LocString DESC = "Carbon Fiber is a " + UI.FormatAsLink("Manufactured Material", "REFINEDMINERAL") + " with high tensile strength.";
		}

		// Token: 0x02001D11 RID: 7441
		public class CARBONGAS
		{
			// Token: 0x04008491 RID: 33937
			public static LocString NAME = UI.FormatAsLink("Carbon", "CARBONGAS");

			// Token: 0x04008492 RID: 33938
			public static LocString DESC = "(C) Carbon is an abundant, versatile element heated into a " + UI.FormatAsLink("Gaseous", "ELEMENTS_GAS") + " state.";
		}

		// Token: 0x02001D12 RID: 7442
		public class CHLORINE
		{
			// Token: 0x04008493 RID: 33939
			public static LocString NAME = UI.FormatAsLink("Chlorine", "CHLORINE");

			// Token: 0x04008494 RID: 33940
			public static LocString DESC = string.Concat(new string[]
			{
				"(Cl) Chlorine is a natural ",
				UI.FormatAsLink("Germ", "DISEASE"),
				"-killing element in a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02001D13 RID: 7443
		public class CHLORINEGAS
		{
			// Token: 0x04008495 RID: 33941
			public static LocString NAME = UI.FormatAsLink("Chlorine", "CHLORINEGAS");

			// Token: 0x04008496 RID: 33942
			public static LocString DESC = string.Concat(new string[]
			{
				"(Cl) Chlorine is a natural ",
				UI.FormatAsLink("Germ", "DISEASE"),
				"-killing element in a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x02001D14 RID: 7444
		public class CLAY
		{
			// Token: 0x04008497 RID: 33943
			public static LocString NAME = UI.FormatAsLink("Clay", "CLAY");

			// Token: 0x04008498 RID: 33944
			public static LocString DESC = "Clay is a soft, naturally occurring composite of stone and soil that hardens at high " + UI.FormatAsLink("Temperatures", "HEAT") + ".\n\nIt is a reliable <b>Construction Material</b>.";
		}

		// Token: 0x02001D15 RID: 7445
		public class BRICK
		{
			// Token: 0x04008499 RID: 33945
			public static LocString NAME = UI.FormatAsLink("Brick", "BRICK");

			// Token: 0x0400849A RID: 33946
			public static LocString DESC = "Brick is a hard, brittle material formed from heated " + ELEMENTS.CLAY.NAME + ".\n\nIt is a reliable <b>Construction Material</b>.";
		}

		// Token: 0x02001D16 RID: 7446
		public class CERAMIC
		{
			// Token: 0x0400849B RID: 33947
			public static LocString NAME = UI.FormatAsLink("Ceramic", "CERAMIC");

			// Token: 0x0400849C RID: 33948
			public static LocString DESC = "Ceramic is a hard, brittle material formed from heated " + ELEMENTS.CLAY.NAME + ".\n\nIt is a reliable <b>Construction Material</b>.";
		}

		// Token: 0x02001D17 RID: 7447
		public class CEMENT
		{
			// Token: 0x0400849D RID: 33949
			public static LocString NAME = UI.FormatAsLink("Cement", "CEMENT");

			// Token: 0x0400849E RID: 33950
			public static LocString DESC = "Cement is a refined building material used for assembling advanced buildings.";
		}

		// Token: 0x02001D18 RID: 7448
		public class CEMENTMIX
		{
			// Token: 0x0400849F RID: 33951
			public static LocString NAME = UI.FormatAsLink("Cement Mix", "CEMENTMIX");

			// Token: 0x040084A0 RID: 33952
			public static LocString DESC = "Cement Mix can be used to create " + ELEMENTS.CEMENT.NAME + " for advanced building assembly.";
		}

		// Token: 0x02001D19 RID: 7449
		public class CONTAMINATEDOXYGEN
		{
			// Token: 0x040084A1 RID: 33953
			public static LocString NAME = UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN");

			// Token: 0x040084A2 RID: 33954
			public static LocString DESC = "(O<sub>2</sub>) Polluted Oxygen is dirty, unfiltered air.\n\nIt is breathable.";
		}

		// Token: 0x02001D1A RID: 7450
		public class COPPER
		{
			// Token: 0x040084A3 RID: 33955
			public static LocString NAME = UI.FormatAsLink("Copper", "COPPER");

			// Token: 0x040084A4 RID: 33956
			public static LocString DESC = string.Concat(new string[]
			{
				"(Cu) Copper is a conductive ",
				UI.FormatAsLink("Metal", "METAL"),
				".\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02001D1B RID: 7451
		public class COPPERGAS
		{
			// Token: 0x040084A5 RID: 33957
			public static LocString NAME = UI.FormatAsLink("Copper", "COPPERGAS");

			// Token: 0x040084A6 RID: 33958
			public static LocString DESC = string.Concat(new string[]
			{
				"(Cu) Copper is a conductive ",
				UI.FormatAsLink("Metal", "METAL"),
				" heated into a ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				"."
			});
		}

		// Token: 0x02001D1C RID: 7452
		public class CREATURE
		{
			// Token: 0x040084A7 RID: 33959
			public static LocString NAME = UI.FormatAsLink("Genetic Ooze", "CREATURE");

			// Token: 0x040084A8 RID: 33960
			public static LocString DESC = "(DuPe) Ooze is a slurry of water, carbon, and dozens and dozens of trace elements.\n\nDuplicants are printed from pure " + UI.FormatAsLink("Ooze", "SOLID") + ".";
		}

		// Token: 0x02001D1D RID: 7453
		public class CRUDEOIL
		{
			// Token: 0x040084A9 RID: 33961
			public static LocString NAME = UI.FormatAsLink("Crude Oil", "CRUDEOIL");

			// Token: 0x040084AA RID: 33962
			public static LocString DESC = "Crude Oil is a raw potential " + UI.FormatAsLink("Power", "POWER") + " source composed of billions of dead, primordial organisms.";
		}

		// Token: 0x02001D1E RID: 7454
		public class PETROLEUM
		{
			// Token: 0x040084AB RID: 33963
			public static LocString NAME = UI.FormatAsLink("Petroleum", "PETROLEUM");

			// Token: 0x040084AC RID: 33964
			public static LocString NAME_TWO = UI.FormatAsLink("Petroleum", "PETROLEUM");

			// Token: 0x040084AD RID: 33965
			public static LocString DESC = string.Concat(new string[]
			{
				"Petroleum is a ",
				UI.FormatAsLink("Power", "POWER"),
				" source refined from ",
				UI.FormatAsLink("Crude Oil", "CRUDEOIL"),
				".\n\nIt is also an essential ingredient in the production of ",
				UI.FormatAsLink("Plastic", "POLYPROPYLENE"),
				"."
			});
		}

		// Token: 0x02001D1F RID: 7455
		public class SOURGAS
		{
			// Token: 0x040084AE RID: 33966
			public static LocString NAME = UI.FormatAsLink("Sour Gas", "SOURGAS");

			// Token: 0x040084AF RID: 33967
			public static LocString NAME_TWO = UI.FormatAsLink("Sour Gas", "SOURGAS");

			// Token: 0x040084B0 RID: 33968
			public static LocString DESC = string.Concat(new string[]
			{
				"Sour Gas is a hydrocarbon ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				" containing high concentrations of hydrogen sulfide.\n\nIt is a byproduct of highly heated ",
				UI.FormatAsLink("Petroleum", "PETROLEUM"),
				"."
			});
		}

		// Token: 0x02001D20 RID: 7456
		public class CRUSHEDICE
		{
			// Token: 0x040084B1 RID: 33969
			public static LocString NAME = UI.FormatAsLink("Crushed Ice", "CRUSHEDICE");

			// Token: 0x040084B2 RID: 33970
			public static LocString DESC = "(H<sub>2</sub>O) A slush of crushed, semi-solid ice.";
		}

		// Token: 0x02001D21 RID: 7457
		public class CRUSHEDROCK
		{
			// Token: 0x040084B3 RID: 33971
			public static LocString NAME = UI.FormatAsLink("Crushed Rock", "CRUSHEDROCK");

			// Token: 0x040084B4 RID: 33972
			public static LocString DESC = "Crushed Rock is " + UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK") + " crushed into a mechanical mixture.";
		}

		// Token: 0x02001D22 RID: 7458
		public class CUPRITE
		{
			// Token: 0x040084B5 RID: 33973
			public static LocString NAME = UI.FormatAsLink("Copper Ore", "CUPRITE");

			// Token: 0x040084B6 RID: 33974
			public static LocString DESC = string.Concat(new string[]
			{
				"(Cu<sub>2</sub>O) Copper Ore is a conductive ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				".\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02001D23 RID: 7459
		public class DEPLETEDURANIUM
		{
			// Token: 0x040084B7 RID: 33975
			public static LocString NAME = UI.FormatAsLink("Depleted Uranium", "DEPLETEDURANIUM");

			// Token: 0x040084B8 RID: 33976
			public static LocString DESC = string.Concat(new string[]
			{
				"(U) Depleted Uranium is ",
				UI.FormatAsLink("Uranium", "URANIUM"),
				" with a low U-235 content.\n\nIt is created as a byproduct of ",
				UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM"),
				" and is no longer suitable as fuel."
			});
		}

		// Token: 0x02001D24 RID: 7460
		public class DIAMOND
		{
			// Token: 0x040084B9 RID: 33977
			public static LocString NAME = UI.FormatAsLink("Diamond", "DIAMOND");

			// Token: 0x040084BA RID: 33978
			public static LocString DESC = "(C) Diamond is industrial-grade, high density carbon.\n\nIt is very difficult to excavate.";
		}

		// Token: 0x02001D25 RID: 7461
		public class DIRT
		{
			// Token: 0x040084BB RID: 33979
			public static LocString NAME = UI.FormatAsLink("Dirt", "DIRT");

			// Token: 0x040084BC RID: 33980
			public static LocString DESC = "Dirt is a soft, nutrient-rich substance capable of supporting life.\n\nIt is necessary in some forms of " + UI.FormatAsLink("Food", "FOOD") + " production.";
		}

		// Token: 0x02001D26 RID: 7462
		public class DIRTYICE
		{
			// Token: 0x040084BD RID: 33981
			public static LocString NAME = UI.FormatAsLink("Polluted Ice", "DIRTYICE");

			// Token: 0x040084BE RID: 33982
			public static LocString DESC = "Polluted Ice is dirty, unfiltered water frozen into a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x02001D27 RID: 7463
		public class DIRTYWATER
		{
			// Token: 0x040084BF RID: 33983
			public static LocString NAME = UI.FormatAsLink("Polluted Water", "DIRTYWATER");

			// Token: 0x040084C0 RID: 33984
			public static LocString DESC = "Polluted Water is dirty, unfiltered " + UI.FormatAsLink("Water", "WATER") + ".\n\nIt is not fit for consumption.";
		}

		// Token: 0x02001D28 RID: 7464
		public class ELECTRUM
		{
			// Token: 0x040084C1 RID: 33985
			public static LocString NAME = UI.FormatAsLink("Electrum", "ELECTRUM");

			// Token: 0x040084C2 RID: 33986
			public static LocString DESC = string.Concat(new string[]
			{
				"Electrum is a conductive ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" alloy composed of gold and silver.\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02001D29 RID: 7465
		public class ENRICHEDURANIUM
		{
			// Token: 0x040084C3 RID: 33987
			public static LocString NAME = UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM");

			// Token: 0x040084C4 RID: 33988
			public static LocString DESC = string.Concat(new string[]
			{
				"(U) Enriched Uranium is a highly ",
				UI.FormatAsLink("Radioactive", "RADIATION"),
				", refined substance.\n\nIt is primarily used to ",
				UI.FormatAsLink("Power", "POWER"),
				" potent research reactors."
			});
		}

		// Token: 0x02001D2A RID: 7466
		public class FERTILIZER
		{
			// Token: 0x040084C5 RID: 33989
			public static LocString NAME = UI.FormatAsLink("Fertilizer", "FERTILIZER");

			// Token: 0x040084C6 RID: 33990
			public static LocString DESC = "Fertilizer is a processed mixture of biological nutrients.\n\nIt aids in the growth of certain " + UI.FormatAsLink("Plants", "PLANTS") + ".";
		}

		// Token: 0x02001D2B RID: 7467
		public class PONDSCUM
		{
			// Token: 0x040084C7 RID: 33991
			public static LocString NAME = UI.FormatAsLink("Pondscum", "PONDSCUM");

			// Token: 0x040084C8 RID: 33992
			public static LocString DESC = string.Concat(new string[]
			{
				"Pondscum is a soft, naturally occurring composite of biological nutrients.\n\nIt may be processed into ",
				UI.FormatAsLink("Fertilizer", "FERTILIZER"),
				" and aids in the growth of certain ",
				UI.FormatAsLink("Plants", "PLANTS"),
				"."
			});
		}

		// Token: 0x02001D2C RID: 7468
		public class FALLOUT
		{
			// Token: 0x040084C9 RID: 33993
			public static LocString NAME = UI.FormatAsLink("Nuclear Fallout", "FALLOUT");

			// Token: 0x040084CA RID: 33994
			public static LocString DESC = string.Concat(new string[]
			{
				"Nuclear Fallout is a highly toxic gas full of ",
				UI.FormatAsLink("Radioactive Contaminants", "RADIATION"),
				". Condenses into ",
				UI.FormatAsLink("Nuclear Waste", "NUCLEARWASTE"),
				"."
			});
		}

		// Token: 0x02001D2D RID: 7469
		public class FOOLSGOLD
		{
			// Token: 0x040084CB RID: 33995
			public static LocString NAME = UI.FormatAsLink("Pyrite", "FOOLSGOLD");

			// Token: 0x040084CC RID: 33996
			public static LocString DESC = string.Concat(new string[]
			{
				"(FeS<sub>2</sub>) Pyrite is a conductive ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				".\n\nAlso known as \"Fool's Gold\", is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02001D2E RID: 7470
		public class FULLERENE
		{
			// Token: 0x040084CD RID: 33997
			public static LocString NAME = UI.FormatAsLink("Fullerene", "FULLERENE");

			// Token: 0x040084CE RID: 33998
			public static LocString DESC = "(C<sub>60</sub>) Fullerene is a form of " + UI.FormatAsLink("Carbon", "CARBON") + " consisting of spherical molecules.";
		}

		// Token: 0x02001D2F RID: 7471
		public class GLASS
		{
			// Token: 0x040084CF RID: 33999
			public static LocString NAME = UI.FormatAsLink("Glass", "GLASS");

			// Token: 0x040084D0 RID: 34000
			public static LocString DESC = "Glass is a brittle, transparent substance formed from " + UI.FormatAsLink("Sand", "SAND") + " fired at high temperatures.";
		}

		// Token: 0x02001D30 RID: 7472
		public class GOLD
		{
			// Token: 0x040084D1 RID: 34001
			public static LocString NAME = UI.FormatAsLink("Gold", "GOLD");

			// Token: 0x040084D2 RID: 34002
			public static LocString DESC = string.Concat(new string[]
			{
				"(Au) Gold is a conductive precious ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				".\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02001D31 RID: 7473
		public class GOLDAMALGAM
		{
			// Token: 0x040084D3 RID: 34003
			public static LocString NAME = UI.FormatAsLink("Gold Amalgam", "GOLDAMALGAM");

			// Token: 0x040084D4 RID: 34004
			public static LocString DESC = "Gold Amalgam is a conductive amalgam of gold and mercury.\n\nIt is suitable for building " + UI.FormatAsLink("Power", "POWER") + " systems.";
		}

		// Token: 0x02001D32 RID: 7474
		public class GOLDGAS
		{
			// Token: 0x040084D5 RID: 34005
			public static LocString NAME = UI.FormatAsLink("Gold", "GOLDGAS");

			// Token: 0x040084D6 RID: 34006
			public static LocString DESC = string.Concat(new string[]
			{
				"(Au) Gold is a conductive precious ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				", heated into a ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				"."
			});
		}

		// Token: 0x02001D33 RID: 7475
		public class GRANITE
		{
			// Token: 0x040084D7 RID: 34007
			public static LocString NAME = UI.FormatAsLink("Granite", "GRANITE");

			// Token: 0x040084D8 RID: 34008
			public static LocString DESC = "Granite is a dense composite of " + UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK") + ".\n\nIt is useful as a <b>Construction Material</b>.";
		}

		// Token: 0x02001D34 RID: 7476
		public class GRAPHITE
		{
			// Token: 0x040084D9 RID: 34009
			public static LocString NAME = UI.FormatAsLink("Graphite", "GRAPHITE");

			// Token: 0x040084DA RID: 34010
			public static LocString DESC = "(C) Graphite is the most stable form of carbon.\n\nIt has high thermal conductivity and is useful as a <b>Construction Material</b>.";
		}

		// Token: 0x02001D35 RID: 7477
		public class SOLIDNUCLEARWASTE
		{
			// Token: 0x040084DB RID: 34011
			public static LocString NAME = UI.FormatAsLink("Solid Nuclear Waste", "SOLIDNUCLEARWASTE");

			// Token: 0x040084DC RID: 34012
			public static LocString DESC = "Highly toxic solid full of " + UI.FormatAsLink("Radioactive Contaminants", "RADIATION") + ".";
		}

		// Token: 0x02001D36 RID: 7478
		public class HELIUM
		{
			// Token: 0x040084DD RID: 34013
			public static LocString NAME = UI.FormatAsLink("Helium", "HELIUM");

			// Token: 0x040084DE RID: 34014
			public static LocString DESC = "(He) Helium is an atomically lightweight, chemical " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + ".";
		}

		// Token: 0x02001D37 RID: 7479
		public class HYDROGEN
		{
			// Token: 0x040084DF RID: 34015
			public static LocString NAME = UI.FormatAsLink("Hydrogen", "HYDROGEN");

			// Token: 0x040084E0 RID: 34016
			public static LocString DESC = "(H) Hydrogen is the universe's most common and atomically light element in a " + UI.FormatAsLink("Gaseous", "ELEMENTS_GAS") + " state.";
		}

		// Token: 0x02001D38 RID: 7480
		public class ICE
		{
			// Token: 0x040084E1 RID: 34017
			public static LocString NAME = UI.FormatAsLink("Ice", "ICE");

			// Token: 0x040084E2 RID: 34018
			public static LocString DESC = "(H<sub>2</sub>O) Ice is clean water frozen into a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x02001D39 RID: 7481
		public class IGNEOUSROCK
		{
			// Token: 0x040084E3 RID: 34019
			public static LocString NAME = UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK");

			// Token: 0x040084E4 RID: 34020
			public static LocString DESC = "Igneous Rock is a composite of solidified volcanic rock.\n\nIt is useful as a <b>Construction Material</b>.";
		}

		// Token: 0x02001D3A RID: 7482
		public class ISORESIN
		{
			// Token: 0x040084E5 RID: 34021
			public static LocString NAME = UI.FormatAsLink("Isoresin", "ISORESIN");

			// Token: 0x040084E6 RID: 34022
			public static LocString DESC = "Isoresin is a crystallized sap composed of long-chain polymers.\n\nIt is used in the production of rare, high grade materials.";
		}

		// Token: 0x02001D3B RID: 7483
		public class RESIN
		{
			// Token: 0x040084E7 RID: 34023
			public static LocString NAME = UI.FormatAsLink("Resin", "RESIN");

			// Token: 0x040084E8 RID: 34024
			public static LocString DESC = "Sticky goo harvested from a grumpy tree.\n\nIt can be polymerized into " + UI.FormatAsLink("Isoresin", "ISORESIN") + " by boiling away its excess moisture.";
		}

		// Token: 0x02001D3C RID: 7484
		public class SOLIDRESIN
		{
			// Token: 0x040084E9 RID: 34025
			public static LocString NAME = UI.FormatAsLink("Resin", "SOLIDRESIN");

			// Token: 0x040084EA RID: 34026
			public static LocString DESC = "Solidified goo harvested from a grumpy tree.\n\nIt is used in the production of " + UI.FormatAsLink("Isoresin", "ISORESIN") + ".";
		}

		// Token: 0x02001D3D RID: 7485
		public class IRON
		{
			// Token: 0x040084EB RID: 34027
			public static LocString NAME = UI.FormatAsLink("Iron", "IRON");

			// Token: 0x040084EC RID: 34028
			public static LocString DESC = "(Fe) Iron is a common industrial " + UI.FormatAsLink("Metal", "RAWMETAL") + ".";
		}

		// Token: 0x02001D3E RID: 7486
		public class IRONINGOT
		{
			// Token: 0x040084ED RID: 34029
			public static LocString NAME = UI.FormatAsLink("Iron", "IRONINGOT");

			// Token: 0x040084EE RID: 34030
			public static LocString DESC = string.Concat(new string[]
			{
				"(Fe) Iron is a ",
				UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
				" made from ",
				UI.FormatAsLink("Iron Ore", "IRONORE"),
				"."
			});
		}

		// Token: 0x02001D3F RID: 7487
		public class IRONGAS
		{
			// Token: 0x040084EF RID: 34031
			public static LocString NAME = UI.FormatAsLink("Iron", "IRONGAS");

			// Token: 0x040084F0 RID: 34032
			public static LocString DESC = string.Concat(new string[]
			{
				"(Fe) Iron is a common industrial ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				", heated into a ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				"."
			});
		}

		// Token: 0x02001D40 RID: 7488
		public class IRONORE
		{
			// Token: 0x040084F1 RID: 34033
			public static LocString NAME = UI.FormatAsLink("Iron Ore", "IRONORE");

			// Token: 0x040084F2 RID: 34034
			public static LocString DESC = string.Concat(new string[]
			{
				"(Fe) Iron Ore is a soft ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				".\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02001D41 RID: 7489
		public class COBALTGAS
		{
			// Token: 0x040084F3 RID: 34035
			public static LocString NAME = UI.FormatAsLink("Cobalt", "COBALTGAS");

			// Token: 0x040084F4 RID: 34036
			public static LocString DESC = string.Concat(new string[]
			{
				"(Co) Cobalt is a ",
				UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
				", heated into a ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				"."
			});
		}

		// Token: 0x02001D42 RID: 7490
		public class COBALT
		{
			// Token: 0x040084F5 RID: 34037
			public static LocString NAME = UI.FormatAsLink("Cobalt", "COBALT");

			// Token: 0x040084F6 RID: 34038
			public static LocString DESC = string.Concat(new string[]
			{
				"(Co) Cobalt is a ",
				UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
				" made from ",
				UI.FormatAsLink("Cobalt Ore", "COBALTITE"),
				"."
			});
		}

		// Token: 0x02001D43 RID: 7491
		public class COBALTITE
		{
			// Token: 0x040084F7 RID: 34039
			public static LocString NAME = UI.FormatAsLink("Cobalt Ore", "COBALTITE");

			// Token: 0x040084F8 RID: 34040
			public static LocString DESC = string.Concat(new string[]
			{
				"(Co) Cobalt Ore is a blue-hued ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				".\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02001D44 RID: 7492
		public class KATAIRITE
		{
			// Token: 0x040084F9 RID: 34041
			public static LocString NAME = UI.FormatAsLink("Abyssalite", "KATAIRITE");

			// Token: 0x040084FA RID: 34042
			public static LocString DESC = "(Ab) Abyssalite is a resilient, crystalline element.";
		}

		// Token: 0x02001D45 RID: 7493
		public class LIME
		{
			// Token: 0x040084FB RID: 34043
			public static LocString NAME = UI.FormatAsLink("Lime", "LIME");

			// Token: 0x040084FC RID: 34044
			public static LocString DESC = "(CaCO<sub>3</sub>) Lime is a mineral commonly found in " + UI.FormatAsLink("Critter", "CREATURES") + " egg shells.\n\nIt is useful as a <b>Construction Material</b>.";
		}

		// Token: 0x02001D46 RID: 7494
		public class FOSSIL
		{
			// Token: 0x040084FD RID: 34045
			public static LocString NAME = UI.FormatAsLink("Fossil", "FOSSIL");

			// Token: 0x040084FE RID: 34046
			public static LocString DESC = "Fossil is organic matter, highly compressed and hardened into a mineral state.\n\nIt is useful as a <b>Construction Material</b>.";
		}

		// Token: 0x02001D47 RID: 7495
		public class LEADGAS
		{
			// Token: 0x040084FF RID: 34047
			public static LocString NAME = UI.FormatAsLink("Lead", "LEADGAS");

			// Token: 0x04008500 RID: 34048
			public static LocString DESC = string.Concat(new string[]
			{
				"(Pb) Lead is a soft yet extremely dense ",
				UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
				" heated into a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				"."
			});
		}

		// Token: 0x02001D48 RID: 7496
		public class LEAD
		{
			// Token: 0x04008501 RID: 34049
			public static LocString NAME = UI.FormatAsLink("Lead", "LEAD");

			// Token: 0x04008502 RID: 34050
			public static LocString DESC = string.Concat(new string[]
			{
				"(Pb) Lead is a soft yet extremely dense ",
				UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
				".\n\nIt has a low Overheat Temperature and is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02001D49 RID: 7497
		public class LIQUIDCARBONDIOXIDE
		{
			// Token: 0x04008503 RID: 34051
			public static LocString NAME = UI.FormatAsLink("Carbon Dioxide", "LIQUIDCARBONDIOXIDE");

			// Token: 0x04008504 RID: 34052
			public static LocString DESC = "(CO<sub>2</sub>) Carbon Dioxide is an unbreathable chemical compound.\n\nThis selection is currently in a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x02001D4A RID: 7498
		public class LIQUIDHELIUM
		{
			// Token: 0x04008505 RID: 34053
			public static LocString NAME = UI.FormatAsLink("Helium", "LIQUIDHELIUM");

			// Token: 0x04008506 RID: 34054
			public static LocString DESC = "(He) Helium is an atomically lightweight chemical element cooled into a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x02001D4B RID: 7499
		public class LIQUIDHYDROGEN
		{
			// Token: 0x04008507 RID: 34055
			public static LocString NAME = UI.FormatAsLink("Hydrogen", "LIQUIDHYDROGEN");

			// Token: 0x04008508 RID: 34056
			public static LocString DESC = "(H) Hydrogen is a chemical " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + ".\n\nIt freezes most substances that come into contact with it.";
		}

		// Token: 0x02001D4C RID: 7500
		public class LIQUIDOXYGEN
		{
			// Token: 0x04008509 RID: 34057
			public static LocString NAME = UI.FormatAsLink("Oxygen", "LIQUIDOXYGEN");

			// Token: 0x0400850A RID: 34058
			public static LocString DESC = "(O<sub>2</sub>) Oxygen is a breathable chemical.\n\nThis selection is in a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x02001D4D RID: 7501
		public class LIQUIDMETHANE
		{
			// Token: 0x0400850B RID: 34059
			public static LocString NAME = UI.FormatAsLink("Methane", "LIQUIDMETHANE");

			// Token: 0x0400850C RID: 34060
			public static LocString DESC = "(CH<sub>4</sub>) Methane is an alkane.\n\nThis selection is in a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x02001D4E RID: 7502
		public class LIQUIDPHOSPHORUS
		{
			// Token: 0x0400850D RID: 34061
			public static LocString NAME = UI.FormatAsLink("Phosphorus", "LIQUIDPHOSPHORUS");

			// Token: 0x0400850E RID: 34062
			public static LocString DESC = "(P) Phosphorus is a chemical element.\n\nThis selection is in a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x02001D4F RID: 7503
		public class LIQUIDPROPANE
		{
			// Token: 0x0400850F RID: 34063
			public static LocString NAME = UI.FormatAsLink("Propane", "LIQUIDPROPANE");

			// Token: 0x04008510 RID: 34064
			public static LocString DESC = string.Concat(new string[]
			{
				"(C<sub>3</sub>H<sub>8</sub>) Propane is an alkane in a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state.\n\nIt is useful in ",
				UI.FormatAsLink("Power", "POWER"),
				" production."
			});
		}

		// Token: 0x02001D50 RID: 7504
		public class LIQUIDSULFUR
		{
			// Token: 0x04008511 RID: 34065
			public static LocString NAME = UI.FormatAsLink("Liquid Sulfur", "LIQUIDSULFUR");

			// Token: 0x04008512 RID: 34066
			public static LocString DESC = string.Concat(new string[]
			{
				"(S) Sulfur is a common chemical element and byproduct of ",
				UI.FormatAsLink("Natural Gas", "METHANE"),
				" production.\n\nThis selection is in a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02001D51 RID: 7505
		public class MAFICROCK
		{
			// Token: 0x04008513 RID: 34067
			public static LocString NAME = UI.FormatAsLink("Mafic Rock", "MAFICROCK");

			// Token: 0x04008514 RID: 34068
			public static LocString DESC = string.Concat(new string[]
			{
				"Mafic Rock an ",
				UI.FormatAsLink("Iron", "IRON"),
				"-rich variation of ",
				UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK"),
				".\n\nIt is useful as a <b>Construction Material</b>."
			});
		}

		// Token: 0x02001D52 RID: 7506
		public class MAGMA
		{
			// Token: 0x04008515 RID: 34069
			public static LocString NAME = UI.FormatAsLink("Magma", "MAGMA");

			// Token: 0x04008516 RID: 34070
			public static LocString DESC = string.Concat(new string[]
			{
				"Magma is a composite of ",
				UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK"),
				" heated into a molten, ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02001D53 RID: 7507
		public class MERCURY
		{
			// Token: 0x04008517 RID: 34071
			public static LocString NAME = UI.FormatAsLink("Mercury", "MERCURY");

			// Token: 0x04008518 RID: 34072
			public static LocString DESC = "(Hg) Mercury is a metallic " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + ".";
		}

		// Token: 0x02001D54 RID: 7508
		public class MERCURYGAS
		{
			// Token: 0x04008519 RID: 34073
			public static LocString NAME = UI.FormatAsLink("Mercury", "MERCURYGAS");

			// Token: 0x0400851A RID: 34074
			public static LocString DESC = string.Concat(new string[]
			{
				"(Hg) Mercury is a ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" heated into a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x02001D55 RID: 7509
		public class METHANE
		{
			// Token: 0x0400851B RID: 34075
			public static LocString NAME = UI.FormatAsLink("Natural Gas", "METHANE");

			// Token: 0x0400851C RID: 34076
			public static LocString DESC = string.Concat(new string[]
			{
				"Natural Gas is a mixture of various alkanes in a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state.\n\nIt is useful in ",
				UI.FormatAsLink("Power", "POWER"),
				" production."
			});
		}

		// Token: 0x02001D56 RID: 7510
		public class MOLTENCARBON
		{
			// Token: 0x0400851D RID: 34077
			public static LocString NAME = UI.FormatAsLink("Carbon", "MOLTENCARBON");

			// Token: 0x0400851E RID: 34078
			public static LocString DESC = "(C) Carbon is an abundant, versatile element heated into a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x02001D57 RID: 7511
		public class MOLTENCOPPER
		{
			// Token: 0x0400851F RID: 34079
			public static LocString NAME = UI.FormatAsLink("Copper", "MOLTENCOPPER");

			// Token: 0x04008520 RID: 34080
			public static LocString DESC = string.Concat(new string[]
			{
				"(Cu) Copper is a conductive ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02001D58 RID: 7512
		public class MOLTENGLASS
		{
			// Token: 0x04008521 RID: 34081
			public static LocString NAME = UI.FormatAsLink("Glass", "MOLTENGLASS");

			// Token: 0x04008522 RID: 34082
			public static LocString DESC = "Molten Glass is a composite of granular rock, heated into a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x02001D59 RID: 7513
		public class MOLTENGOLD
		{
			// Token: 0x04008523 RID: 34083
			public static LocString NAME = UI.FormatAsLink("Gold", "MOLTENGOLD");

			// Token: 0x04008524 RID: 34084
			public static LocString DESC = string.Concat(new string[]
			{
				"(Au) Gold is a conductive precious ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02001D5A RID: 7514
		public class MOLTENIRON
		{
			// Token: 0x04008525 RID: 34085
			public static LocString NAME = UI.FormatAsLink("Iron", "MOLTENIRON");

			// Token: 0x04008526 RID: 34086
			public static LocString DESC = string.Concat(new string[]
			{
				"(Fe) Iron is a common industrial ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02001D5B RID: 7515
		public class MOLTENCOBALT
		{
			// Token: 0x04008527 RID: 34087
			public static LocString NAME = UI.FormatAsLink("Cobalt", "MOLTENCOBALT");

			// Token: 0x04008528 RID: 34088
			public static LocString DESC = string.Concat(new string[]
			{
				"(Co) Cobalt is a ",
				UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
				" heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02001D5C RID: 7516
		public class MOLTENLEAD
		{
			// Token: 0x04008529 RID: 34089
			public static LocString NAME = UI.FormatAsLink("Lead", "MOLTENLEAD");

			// Token: 0x0400852A RID: 34090
			public static LocString DESC = string.Concat(new string[]
			{
				"(Pb) Lead is an extremely dense ",
				UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
				" heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02001D5D RID: 7517
		public class MOLTENNIOBIUM
		{
			// Token: 0x0400852B RID: 34091
			public static LocString NAME = UI.FormatAsLink("Niobium", "MOLTENNIOBIUM");

			// Token: 0x0400852C RID: 34092
			public static LocString DESC = string.Concat(new string[]
			{
				"(Nb) Niobium is a ",
				UI.FormatAsLink("Rare Metal", "RAREMATERIALS"),
				" heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02001D5E RID: 7518
		public class MOLTENTUNGSTEN
		{
			// Token: 0x0400852D RID: 34093
			public static LocString NAME = UI.FormatAsLink("Tungsten", "MOLTENTUNGSTEN");

			// Token: 0x0400852E RID: 34094
			public static LocString DESC = string.Concat(new string[]
			{
				"(W) Tungsten is a crystalline ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02001D5F RID: 7519
		public class MOLTENTUNGSTENDISELENIDE
		{
			// Token: 0x0400852F RID: 34095
			public static LocString NAME = UI.FormatAsLink("Tungsten Diselenide", "MOLTENTUNGSTENDISELENIDE");

			// Token: 0x04008530 RID: 34096
			public static LocString DESC = string.Concat(new string[]
			{
				"(WSe<sub>2</sub>) Tungsten Diselenide is an inorganic ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" compound heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02001D60 RID: 7520
		public class MOLTENSTEEL
		{
			// Token: 0x04008531 RID: 34097
			public static LocString NAME = UI.FormatAsLink("Steel", "MOLTENSTEEL");

			// Token: 0x04008532 RID: 34098
			public static LocString DESC = string.Concat(new string[]
			{
				"Steel is a ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" alloy of iron and carbon, heated into a hazardous ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02001D61 RID: 7521
		public class MOLTENURANIUM
		{
			// Token: 0x04008533 RID: 34099
			public static LocString NAME = UI.FormatAsLink("Uranium", "MOLTENURANIUM");

			// Token: 0x04008534 RID: 34100
			public static LocString DESC = string.Concat(new string[]
			{
				"(U) Uranium is a highly ",
				UI.FormatAsLink("Radioactive", "RADIATION"),
				" substance, heated into a hazardous ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state.\n\nIt is a byproduct of ",
				UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM"),
				"."
			});
		}

		// Token: 0x02001D62 RID: 7522
		public class NIOBIUM
		{
			// Token: 0x04008535 RID: 34101
			public static LocString NAME = UI.FormatAsLink("Niobium", "NIOBIUM");

			// Token: 0x04008536 RID: 34102
			public static LocString DESC = string.Concat(new string[]
			{
				"(Nb) Niobium is a ",
				UI.FormatAsLink("Rare Metal", "RAREMATERIALS"),
				" with many practical applications in metallurgy and superconductor ",
				UI.FormatAsLink("Research", "RESEARCH"),
				"."
			});
		}

		// Token: 0x02001D63 RID: 7523
		public class NIOBIUMGAS
		{
			// Token: 0x04008537 RID: 34103
			public static LocString NAME = UI.FormatAsLink("Niobium", "NIOBIUMGAS");

			// Token: 0x04008538 RID: 34104
			public static LocString DESC = string.Concat(new string[]
			{
				"(Nb) Niobium is a ",
				UI.FormatAsLink("Rare Metal", "RAREMATERIALS"),
				".\n\nThis selection is in a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x02001D64 RID: 7524
		public class NUCLEARWASTE
		{
			// Token: 0x04008539 RID: 34105
			public static LocString NAME = UI.FormatAsLink("Nuclear Waste", "NUCLEARWASTE");

			// Token: 0x0400853A RID: 34106
			public static LocString DESC = string.Concat(new string[]
			{
				"Highly toxic liquid full of ",
				UI.FormatAsLink("Radioactive Contaminants", "RADIATION"),
				" which emit ",
				UI.FormatAsLink("Radiation", "RADIATION"),
				" that can be absorbed by ",
				UI.FormatAsLink("Radbolt Generators", "HIGHENERGYPARTICLESPAWNER"),
				"."
			});
		}

		// Token: 0x02001D65 RID: 7525
		public class OBSIDIAN
		{
			// Token: 0x0400853B RID: 34107
			public static LocString NAME = UI.FormatAsLink("Obsidian", "OBSIDIAN");

			// Token: 0x0400853C RID: 34108
			public static LocString DESC = "Obsidian is a brittle composite of volcanic " + UI.FormatAsLink("Glass", "GLASS") + ".";
		}

		// Token: 0x02001D66 RID: 7526
		public class OXYGEN
		{
			// Token: 0x0400853D RID: 34109
			public static LocString NAME = UI.FormatAsLink("Oxygen", "OXYGEN");

			// Token: 0x0400853E RID: 34110
			public static LocString DESC = "(O<sub>2</sub>) Oxygen is an atomically lightweight and breathable " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + ", necessary for sustaining life.\n\nIt tends to rise above other gases.";
		}

		// Token: 0x02001D67 RID: 7527
		public class OXYROCK
		{
			// Token: 0x0400853F RID: 34111
			public static LocString NAME = UI.FormatAsLink("Oxylite", "OXYROCK");

			// Token: 0x04008540 RID: 34112
			public static LocString DESC = string.Concat(new string[]
			{
				"(Ir<sub>3</sub>O<sub>2</sub>) Oxylite is a chemical compound that slowly emits breathable ",
				UI.FormatAsLink("Oxygen", "OXYGEN"),
				".\n\nExcavating ",
				ELEMENTS.OXYROCK.NAME,
				" increases its emission rate, but depletes the ore more rapidly."
			});
		}

		// Token: 0x02001D68 RID: 7528
		public class PHOSPHATENODULES
		{
			// Token: 0x04008541 RID: 34113
			public static LocString NAME = UI.FormatAsLink("Phosphate Nodules", "PHOSPHATENODULES");

			// Token: 0x04008542 RID: 34114
			public static LocString DESC = "(PO<sup>3-</sup><sub>4</sub>) Nodules of sedimentary rock containing high concentrations of phosphate.";
		}

		// Token: 0x02001D69 RID: 7529
		public class PHOSPHORITE
		{
			// Token: 0x04008543 RID: 34115
			public static LocString NAME = UI.FormatAsLink("Phosphorite", "PHOSPHORITE");

			// Token: 0x04008544 RID: 34116
			public static LocString DESC = "Phosphorite is a composite of sedimentary rock, saturated with phosphate.";
		}

		// Token: 0x02001D6A RID: 7530
		public class PHOSPHORUS
		{
			// Token: 0x04008545 RID: 34117
			public static LocString NAME = UI.FormatAsLink("Refined Phosphorus", "PHOSPHORUS");

			// Token: 0x04008546 RID: 34118
			public static LocString DESC = "(P) Refined Phosphorus is a chemical element in its " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x02001D6B RID: 7531
		public class PHOSPHORUSGAS
		{
			// Token: 0x04008547 RID: 34119
			public static LocString NAME = UI.FormatAsLink("Phosphorus", "PHOSPHORUSGAS");

			// Token: 0x04008548 RID: 34120
			public static LocString DESC = "(P) Phosphorus is a chemical element in a " + UI.FormatAsLink("Gaseous", "ELEMENTS_GAS") + " state.";
		}

		// Token: 0x02001D6C RID: 7532
		public class PROPANE
		{
			// Token: 0x04008549 RID: 34121
			public static LocString NAME = UI.FormatAsLink("Propane", "PROPANE");

			// Token: 0x0400854A RID: 34122
			public static LocString DESC = string.Concat(new string[]
			{
				"(C<sub>3</sub>H<sub>8</sub>) Propane is a natural alkane ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				".\n\nIt is useful in ",
				UI.FormatAsLink("Power", "POWER"),
				" production."
			});
		}

		// Token: 0x02001D6D RID: 7533
		public class RADIUM
		{
			// Token: 0x0400854B RID: 34123
			public static LocString NAME = UI.FormatAsLink("Radium", "RADIUM");

			// Token: 0x0400854C RID: 34124
			public static LocString DESC = string.Concat(new string[]
			{
				"(Ra) Radium is a ",
				UI.FormatAsLink("Light", "LIGHT"),
				" emitting radioactive substance.\n\nIt is useful as a ",
				UI.FormatAsLink("Power", "POWER"),
				" source."
			});
		}

		// Token: 0x02001D6E RID: 7534
		public class YELLOWCAKE
		{
			// Token: 0x0400854D RID: 34125
			public static LocString NAME = UI.FormatAsLink("Yellowcake", "YELLOWCAKE");

			// Token: 0x0400854E RID: 34126
			public static LocString DESC = string.Concat(new string[]
			{
				"(U<sub>3</sub>O<sub>8</sub>) Yellowcake is a byproduct of ",
				UI.FormatAsLink("Uranium", "URANIUM"),
				" mining.\n\nIt is useful in preparing fuel for ",
				UI.FormatAsLink("Research Reactors", "NUCLEARREACTOR"),
				".\n\nNote: Do not eat."
			});
		}

		// Token: 0x02001D6F RID: 7535
		public class ROCKGAS
		{
			// Token: 0x0400854F RID: 34127
			public static LocString NAME = UI.FormatAsLink("Rock Gas", "ROCKGAS");

			// Token: 0x04008550 RID: 34128
			public static LocString DESC = "Rock Gas is rock that has been superheated into a " + UI.FormatAsLink("Gaseous", "ELEMENTS_GAS") + " state.";
		}

		// Token: 0x02001D70 RID: 7536
		public class RUST
		{
			// Token: 0x04008551 RID: 34129
			public static LocString NAME = UI.FormatAsLink("Rust", "RUST");

			// Token: 0x04008552 RID: 34130
			public static LocString DESC = string.Concat(new string[]
			{
				"Rust is an iron oxide that forms from the breakdown of ",
				UI.FormatAsLink("Iron", "IRON"),
				".\n\nIt is useful in some ",
				UI.FormatAsLink("Oxygen", "OXYGEN"),
				" production processes."
			});
		}

		// Token: 0x02001D71 RID: 7537
		public class REGOLITH
		{
			// Token: 0x04008553 RID: 34131
			public static LocString NAME = UI.FormatAsLink("Regolith", "REGOLITH");

			// Token: 0x04008554 RID: 34132
			public static LocString DESC = "Regolith is a sandy substance composed of the various particles that collect atop terrestrial objects.\n\nIt is useful as a " + UI.FormatAsLink("Filtration Medium", "REGOLITH") + ".";
		}

		// Token: 0x02001D72 RID: 7538
		public class SALTGAS
		{
			// Token: 0x04008555 RID: 34133
			public static LocString NAME = UI.FormatAsLink("Salt", "SALTGAS");

			// Token: 0x04008556 RID: 34134
			public static LocString DESC = "(NaCl) Salt Gas is an edible chemical compound that has been superheated into a " + UI.FormatAsLink("Gaseous", "ELEMENTS_GAS") + " state.";
		}

		// Token: 0x02001D73 RID: 7539
		public class MOLTENSALT
		{
			// Token: 0x04008557 RID: 34135
			public static LocString NAME = UI.FormatAsLink("Salt", "MOLTENSALT");

			// Token: 0x04008558 RID: 34136
			public static LocString DESC = "(NaCl) Salt is an edible chemical compound that has been heated into a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x02001D74 RID: 7540
		public class SALT
		{
			// Token: 0x04008559 RID: 34137
			public static LocString NAME = UI.FormatAsLink("Salt", "SALT");

			// Token: 0x0400855A RID: 34138
			public static LocString DESC = "(NaCl) Salt, also known as sodium chloride, is an edible chemical compound.\n\nWhen refined, it can be eaten with meals to increase Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";
		}

		// Token: 0x02001D75 RID: 7541
		public class SALTWATER
		{
			// Token: 0x0400855B RID: 34139
			public static LocString NAME = UI.FormatAsLink("Salt Water", "SALTWATER");

			// Token: 0x0400855C RID: 34140
			public static LocString DESC = string.Concat(new string[]
			{
				"Salt Water is a natural, lightly concentrated solution of ",
				UI.FormatAsLink("Salt", "SALT"),
				" dissolved in ",
				UI.FormatAsLink("Water", "WATER"),
				".\n\nIt can be used in desalination processes, separating out usable salt."
			});
		}

		// Token: 0x02001D76 RID: 7542
		public class SAND
		{
			// Token: 0x0400855D RID: 34141
			public static LocString NAME = UI.FormatAsLink("Sand", "SAND");

			// Token: 0x0400855E RID: 34142
			public static LocString DESC = "Sand is a composite of granular rock.\n\nIt is useful as a " + UI.FormatAsLink("Filtration Medium", "FILTER") + ".";
		}

		// Token: 0x02001D77 RID: 7543
		public class SANDCEMENT
		{
			// Token: 0x0400855F RID: 34143
			public static LocString NAME = UI.FormatAsLink("Sand Cement", "SANDCEMENT");

			// Token: 0x04008560 RID: 34144
			public static LocString DESC = "";
		}

		// Token: 0x02001D78 RID: 7544
		public class SANDSTONE
		{
			// Token: 0x04008561 RID: 34145
			public static LocString NAME = UI.FormatAsLink("Sandstone", "SANDSTONE");

			// Token: 0x04008562 RID: 34146
			public static LocString DESC = "Sandstone is a composite of relatively soft sedimentary rock.\n\nIt is useful as a <b>Construction Material</b>.";
		}

		// Token: 0x02001D79 RID: 7545
		public class SEDIMENTARYROCK
		{
			// Token: 0x04008563 RID: 34147
			public static LocString NAME = UI.FormatAsLink("Sedimentary Rock", "SEDIMENTARYROCK");

			// Token: 0x04008564 RID: 34148
			public static LocString DESC = "Sedimentary Rock is a hardened composite of sediment layers.\n\nIt is useful as a <b>Construction Material</b>.";
		}

		// Token: 0x02001D7A RID: 7546
		public class SLIMEMOLD
		{
			// Token: 0x04008565 RID: 34149
			public static LocString NAME = UI.FormatAsLink("Slime", "SLIMEMOLD");

			// Token: 0x04008566 RID: 34150
			public static LocString DESC = string.Concat(new string[]
			{
				"Slime is a thick biomixture of algae, fungi, and mucopolysaccharides.\n\nIt can be distilled into ",
				UI.FormatAsLink("Algae", "ALGAE"),
				" and emits ",
				ELEMENTS.CONTAMINATEDOXYGEN.NAME,
				" once dug up."
			});
		}

		// Token: 0x02001D7B RID: 7547
		public class SNOW
		{
			// Token: 0x04008567 RID: 34151
			public static LocString NAME = UI.FormatAsLink("Snow", "SNOW");

			// Token: 0x04008568 RID: 34152
			public static LocString DESC = "(H<sub>2</sub>O) Snow is a mass of loose, crystalline ice particles.\n\nIt becomes " + UI.FormatAsLink("Water", "WATER") + " when melted.";
		}

		// Token: 0x02001D7C RID: 7548
		public class SOLIDCARBONDIOXIDE
		{
			// Token: 0x04008569 RID: 34153
			public static LocString NAME = UI.FormatAsLink("Carbon Dioxide", "SOLIDCARBONDIOXIDE");

			// Token: 0x0400856A RID: 34154
			public static LocString DESC = "(CO<sub>2</sub>) Carbon Dioxide is an unbreathable compound in a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x02001D7D RID: 7549
		public class SOLIDCHLORINE
		{
			// Token: 0x0400856B RID: 34155
			public static LocString NAME = UI.FormatAsLink("Chlorine", "SOLIDCHLORINE");

			// Token: 0x0400856C RID: 34156
			public static LocString DESC = string.Concat(new string[]
			{
				"(Cl) Chlorine is a natural ",
				UI.FormatAsLink("Germ", "DISEASE"),
				"-killing element in a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state."
			});
		}

		// Token: 0x02001D7E RID: 7550
		public class SOLIDCRUDEOIL
		{
			// Token: 0x0400856D RID: 34157
			public static LocString NAME = UI.FormatAsLink("Crude Oil", "SOLIDCRUDEOIL");

			// Token: 0x0400856E RID: 34158
			public static LocString DESC = "";
		}

		// Token: 0x02001D7F RID: 7551
		public class SOLIDHYDROGEN
		{
			// Token: 0x0400856F RID: 34159
			public static LocString NAME = UI.FormatAsLink("Hydrogen", "SOLIDHYDROGEN");

			// Token: 0x04008570 RID: 34160
			public static LocString DESC = "(H) Hydrogen is the universe's most common element in a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x02001D80 RID: 7552
		public class SOLIDMERCURY
		{
			// Token: 0x04008571 RID: 34161
			public static LocString NAME = UI.FormatAsLink("Mercury", "SOLIDMERCURY");

			// Token: 0x04008572 RID: 34162
			public static LocString DESC = string.Concat(new string[]
			{
				"(Hg) Mercury is a rare ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" in a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state."
			});
		}

		// Token: 0x02001D81 RID: 7553
		public class SOLIDOXYGEN
		{
			// Token: 0x04008573 RID: 34163
			public static LocString NAME = UI.FormatAsLink("Oxygen", "SOLIDOXYGEN");

			// Token: 0x04008574 RID: 34164
			public static LocString DESC = "(O<sub>2</sub>) Oxygen is a breathable element in a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x02001D82 RID: 7554
		public class SOLIDMETHANE
		{
			// Token: 0x04008575 RID: 34165
			public static LocString NAME = UI.FormatAsLink("Methane", "SOLIDMETHANE");

			// Token: 0x04008576 RID: 34166
			public static LocString DESC = "(CH<sub>4</sub>) Methane is an alkane in a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x02001D83 RID: 7555
		public class SOLIDNAPHTHA
		{
			// Token: 0x04008577 RID: 34167
			public static LocString NAME = UI.FormatAsLink("Naphtha", "SOLIDNAPHTHA");

			// Token: 0x04008578 RID: 34168
			public static LocString DESC = "Naphtha is a distilled hydrocarbon mixture in a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x02001D84 RID: 7556
		public class CORIUM
		{
			// Token: 0x04008579 RID: 34169
			public static LocString NAME = UI.FormatAsLink("Corium", "CORIUM");

			// Token: 0x0400857A RID: 34170
			public static LocString DESC = "A radioactive mixture of nuclear waste and melted reactor materials.\n\nReleases " + UI.FormatAsLink("Nuclear Fallout", "FALLOUT") + " gas.";
		}

		// Token: 0x02001D85 RID: 7557
		public class SOLIDPETROLEUM
		{
			// Token: 0x0400857B RID: 34171
			public static LocString NAME = UI.FormatAsLink("Petroleum", "SOLIDPETROLEUM");

			// Token: 0x0400857C RID: 34172
			public static LocString DESC = string.Concat(new string[]
			{
				"Petroleum is a ",
				UI.FormatAsLink("Power", "POWER"),
				" source.\n\nThis selection is in a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state."
			});
		}

		// Token: 0x02001D86 RID: 7558
		public class SOLIDPROPANE
		{
			// Token: 0x0400857D RID: 34173
			public static LocString NAME = UI.FormatAsLink("Propane", "SOLIDPROPANE");

			// Token: 0x0400857E RID: 34174
			public static LocString DESC = "(C<sub>3</sub>H<sub>8</sub>) Propane is a natural gas in a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x02001D87 RID: 7559
		public class SOLIDSUPERCOOLANT
		{
			// Token: 0x0400857F RID: 34175
			public static LocString NAME = UI.FormatAsLink("Super Coolant", "SOLIDSUPERCOOLANT");

			// Token: 0x04008580 RID: 34176
			public static LocString DESC = string.Concat(new string[]
			{
				"Super Coolant is an industrial grade ",
				UI.FormatAsLink("Fullerene", "FULLERENE"),
				" coolant.\n\nThis selection is in a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state."
			});
		}

		// Token: 0x02001D88 RID: 7560
		public class SOLIDVISCOGEL
		{
			// Token: 0x04008581 RID: 34177
			public static LocString NAME = UI.FormatAsLink("Visco-Gel", "SOLIDVISCOGEL");

			// Token: 0x04008582 RID: 34178
			public static LocString DESC = string.Concat(new string[]
			{
				"Visco-Gel is a polymer that has high surface tension when in ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" form.\n\nThis selection is in a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state."
			});
		}

		// Token: 0x02001D89 RID: 7561
		public class SYNGAS
		{
			// Token: 0x04008583 RID: 34179
			public static LocString NAME = UI.FormatAsLink("Synthesis Gas", "SYNGAS");

			// Token: 0x04008584 RID: 34180
			public static LocString DESC = "Synthesis Gas is an artificial, unbreathable " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + ".\n\nIt can be converted into an efficient fuel.";
		}

		// Token: 0x02001D8A RID: 7562
		public class MOLTENSYNGAS
		{
			// Token: 0x04008585 RID: 34181
			public static LocString NAME = UI.FormatAsLink("Molten Synthesis Gas", "SYNGAS");

			// Token: 0x04008586 RID: 34182
			public static LocString DESC = "Molten Synthesis Gas is an artificial, unbreathable " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + ".\n\nIt can be converted into an efficient fuel.";
		}

		// Token: 0x02001D8B RID: 7563
		public class SOLIDSYNGAS
		{
			// Token: 0x04008587 RID: 34183
			public static LocString NAME = UI.FormatAsLink("Solid Synthesis Gas", "SYNGAS");

			// Token: 0x04008588 RID: 34184
			public static LocString DESC = "Solid Synthesis Gas is an artificial, unbreathable " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + ".\n\nIt can be converted into an efficient fuel.";
		}

		// Token: 0x02001D8C RID: 7564
		public class STEAM
		{
			// Token: 0x04008589 RID: 34185
			public static LocString NAME = UI.FormatAsLink("Steam", "STEAM");

			// Token: 0x0400858A RID: 34186
			public static LocString DESC = string.Concat(new string[]
			{
				"(H<sub>2</sub>O) Steam is ",
				ELEMENTS.WATER.NAME,
				" that has been heated into a scalding ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				"."
			});
		}

		// Token: 0x02001D8D RID: 7565
		public class STEEL
		{
			// Token: 0x0400858B RID: 34187
			public static LocString NAME = UI.FormatAsLink("Steel", "STEEL");

			// Token: 0x0400858C RID: 34188
			public static LocString DESC = "Steel is a " + UI.FormatAsLink("Metal Alloy", "REFINEDMETAL") + " composed of iron and carbon.";
		}

		// Token: 0x02001D8E RID: 7566
		public class STEELGAS
		{
			// Token: 0x0400858D RID: 34189
			public static LocString NAME = UI.FormatAsLink("Steel", "STEELGAS");

			// Token: 0x0400858E RID: 34190
			public static LocString DESC = string.Concat(new string[]
			{
				"Steel is a superheated ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				" composed of iron and carbon."
			});
		}

		// Token: 0x02001D8F RID: 7567
		public class SULFUR
		{
			// Token: 0x0400858F RID: 34191
			public static LocString NAME = UI.FormatAsLink("Sulfur", "SULFUR");

			// Token: 0x04008590 RID: 34192
			public static LocString DESC = "(S) Sulfur is a common chemical element and byproduct of " + UI.FormatAsLink("Natural Gas", "METHANE") + " production.";
		}

		// Token: 0x02001D90 RID: 7568
		public class SULFURGAS
		{
			// Token: 0x04008591 RID: 34193
			public static LocString NAME = UI.FormatAsLink("Sulfur", "SULFURGAS");

			// Token: 0x04008592 RID: 34194
			public static LocString DESC = string.Concat(new string[]
			{
				"(S) Sulfur is a common chemical element and byproduct of ",
				UI.FormatAsLink("Natural Gas", "METHANE"),
				" production.\n\nThis selection is in a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x02001D91 RID: 7569
		public class SUPERCOOLANT
		{
			// Token: 0x04008593 RID: 34195
			public static LocString NAME = UI.FormatAsLink("Super Coolant", "SUPERCOOLANT");

			// Token: 0x04008594 RID: 34196
			public static LocString DESC = "Super Coolant is an industrial grade coolant that utilizes the unusual energy states of " + UI.FormatAsLink("Fullerene", "FULLERENE") + ".";
		}

		// Token: 0x02001D92 RID: 7570
		public class SUPERCOOLANTGAS
		{
			// Token: 0x04008595 RID: 34197
			public static LocString NAME = UI.FormatAsLink("Super Coolant", "SUPERCOOLANTGAS");

			// Token: 0x04008596 RID: 34198
			public static LocString DESC = string.Concat(new string[]
			{
				"Super Coolant is an industrial grade ",
				UI.FormatAsLink("Fullerene", "FULLERENE"),
				" coolant.\n\nThis selection is in a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x02001D93 RID: 7571
		public class SUPERINSULATOR
		{
			// Token: 0x04008597 RID: 34199
			public static LocString NAME = UI.FormatAsLink("Insulation", "SUPERINSULATOR");

			// Token: 0x04008598 RID: 34200
			public static LocString DESC = string.Concat(new string[]
			{
				"Insulation reduces ",
				UI.FormatAsLink("Heat Transfer", "HEAT"),
				" and is composed of recrystallized ",
				UI.FormatAsLink("Abyssalite", "KATAIRITE"),
				"."
			});
		}

		// Token: 0x02001D94 RID: 7572
		public class TEMPCONDUCTORSOLID
		{
			// Token: 0x04008599 RID: 34201
			public static LocString NAME = UI.FormatAsLink("Thermium", "TEMPCONDUCTORSOLID");

			// Token: 0x0400859A RID: 34202
			public static LocString DESC = "Thermium is an industrial metal alloy formulated to maximize " + UI.FormatAsLink("Heat Transfer", "HEAT") + " and thermal dispersion.";
		}

		// Token: 0x02001D95 RID: 7573
		public class TUNGSTEN
		{
			// Token: 0x0400859B RID: 34203
			public static LocString NAME = UI.FormatAsLink("Tungsten", "TUNGSTEN");

			// Token: 0x0400859C RID: 34204
			public static LocString DESC = string.Concat(new string[]
			{
				"(W) Tungsten is an extremely tough crystalline ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				".\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02001D96 RID: 7574
		public class TUNGSTENGAS
		{
			// Token: 0x0400859D RID: 34205
			public static LocString NAME = UI.FormatAsLink("Tungsten", "TUNGSTENGAS");

			// Token: 0x0400859E RID: 34206
			public static LocString DESC = string.Concat(new string[]
			{
				"(W) Tungsten is a superheated crystalline ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				".\n\nThis selection is in a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x02001D97 RID: 7575
		public class TUNGSTENDISELENIDE
		{
			// Token: 0x0400859F RID: 34207
			public static LocString NAME = UI.FormatAsLink("Tungsten Diselenide", "TUNGSTENDISELENIDE");

			// Token: 0x040085A0 RID: 34208
			public static LocString DESC = string.Concat(new string[]
			{
				"(WSe<sub>2</sub>) Tungsten Diselenide is an inorganic ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" compound with a crystalline structure.\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02001D98 RID: 7576
		public class TUNGSTENDISELENIDEGAS
		{
			// Token: 0x040085A1 RID: 34209
			public static LocString NAME = UI.FormatAsLink("Tungsten Diselenide", "TUNGSTENDISELENIDEGAS");

			// Token: 0x040085A2 RID: 34210
			public static LocString DESC = string.Concat(new string[]
			{
				"(WSe<sub>2</sub>) Tungsten Diselenide is a superheated ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" compound in a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x02001D99 RID: 7577
		public class TOXICSAND
		{
			// Token: 0x040085A3 RID: 34211
			public static LocString NAME = UI.FormatAsLink("Polluted Dirt", "TOXICSAND");

			// Token: 0x040085A4 RID: 34212
			public static LocString DESC = "Polluted Dirt is unprocessed biological waste.\n\nIt emits " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + " over time.";
		}

		// Token: 0x02001D9A RID: 7578
		public class UNOBTANIUM
		{
			// Token: 0x040085A5 RID: 34213
			public static LocString NAME = UI.FormatAsLink("Neutronium", "UNOBTANIUM");

			// Token: 0x040085A6 RID: 34214
			public static LocString DESC = "(Nt) Neutronium is a mysterious and extremely resilient element.\n\nIt cannot be excavated by any Duplicant mining tool.";
		}

		// Token: 0x02001D9B RID: 7579
		public class URANIUMORE
		{
			// Token: 0x040085A7 RID: 34215
			public static LocString NAME = UI.FormatAsLink("Uranium Ore", "URANIUMORE");

			// Token: 0x040085A8 RID: 34216
			public static LocString DESC = "(U) Uranium Ore is a highly " + UI.FormatAsLink("Radioactive", "RADIATION") + " substance.\n\nIt can be refined into fuel for research reactors.";
		}

		// Token: 0x02001D9C RID: 7580
		public class VACUUM
		{
			// Token: 0x040085A9 RID: 34217
			public static LocString NAME = UI.FormatAsLink("Vacuum", "VACUUM");

			// Token: 0x040085AA RID: 34218
			public static LocString DESC = "A vacuum is a space devoid of all matter.";
		}

		// Token: 0x02001D9D RID: 7581
		public class VISCOGEL
		{
			// Token: 0x040085AB RID: 34219
			public static LocString NAME = UI.FormatAsLink("Visco-Gel", "VISCOGEL");

			// Token: 0x040085AC RID: 34220
			public static LocString DESC = "Visco-Gel is a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " polymer with high surface tension, preventing typical liquid flow and allowing for unusual configurations.";
		}

		// Token: 0x02001D9E RID: 7582
		public class VOID
		{
			// Token: 0x040085AD RID: 34221
			public static LocString NAME = UI.FormatAsLink("Void", "VOID");

			// Token: 0x040085AE RID: 34222
			public static LocString DESC = "Cold, infinite nothingness.";
		}

		// Token: 0x02001D9F RID: 7583
		public class COMPOSITION
		{
			// Token: 0x040085AF RID: 34223
			public static LocString NAME = UI.FormatAsLink("Composition", "COMPOSITION");

			// Token: 0x040085B0 RID: 34224
			public static LocString DESC = "A mixture of two or more elements.";
		}

		// Token: 0x02001DA0 RID: 7584
		public class WATER
		{
			// Token: 0x040085B1 RID: 34225
			public static LocString NAME = UI.FormatAsLink("Water", "WATER");

			// Token: 0x040085B2 RID: 34226
			public static LocString DESC = "(H<sub>2</sub>O) Clean " + UI.FormatAsLink("Water", "WATER") + ", suitable for consumption.";
		}

		// Token: 0x02001DA1 RID: 7585
		public class WOLFRAMITE
		{
			// Token: 0x040085B3 RID: 34227
			public static LocString NAME = UI.FormatAsLink("Wolframite", "WOLFRAMITE");

			// Token: 0x040085B4 RID: 34228
			public static LocString DESC = string.Concat(new string[]
			{
				"((Fe,Mn)WO<sub>4</sub>) Wolframite is a dense Metallic element in a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state.\n\nIt is a source of ",
				UI.FormatAsLink("Tungsten", "TUNGSTEN"),
				" and is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02001DA2 RID: 7586
		public class TESTELEMENT
		{
			// Token: 0x040085B5 RID: 34229
			public static LocString NAME = UI.FormatAsLink("Test Element", "TESTELEMENT");

			// Token: 0x040085B6 RID: 34230
			public static LocString DESC = string.Concat(new string[]
			{
				"((Fe,Mn)WO<sub>4</sub>) Wolframite is a dense Metallic element in a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state.\n\nIt is a source of ",
				UI.FormatAsLink("Tungsten", "TUNGSTEN"),
				" and is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02001DA3 RID: 7587
		public class POLYPROPYLENE
		{
			// Token: 0x040085B7 RID: 34231
			public static LocString NAME = UI.FormatAsLink("Plastic", "POLYPROPYLENE");

			// Token: 0x040085B8 RID: 34232
			public static LocString DESC = "(C<sub>3</sub>H<sub>6</sub>)<sub>n</sub> " + ELEMENTS.POLYPROPYLENE.NAME + " is a thermoplastic polymer.\n\nIt is useful for constructing a variety of advanced buildings and equipment.";

			// Token: 0x040085B9 RID: 34233
			public static LocString BUILD_DESC = "Buildings made of this " + ELEMENTS.POLYPROPYLENE.NAME + " have antiseptic properties";
		}

		// Token: 0x02001DA4 RID: 7588
		public class NAPHTHA
		{
			// Token: 0x040085BA RID: 34234
			public static LocString NAME = UI.FormatAsLink("Naphtha", "NAPHTHA");

			// Token: 0x040085BB RID: 34235
			public static LocString DESC = "Naphtha a distilled hydrocarbon mixture produced from the burning of " + UI.FormatAsLink("Plastic", "POLYPROPYLENE") + ".";
		}

		// Token: 0x02001DA5 RID: 7589
		public class SLABS
		{
			// Token: 0x040085BC RID: 34236
			public static LocString NAME = UI.FormatAsLink("Building Slab", "SLABS");

			// Token: 0x040085BD RID: 34237
			public static LocString DESC = "Slabs are a refined mineral building block used for assembling advanced buildings.";
		}

		// Token: 0x02001DA6 RID: 7590
		public class TOXICMUD
		{
			// Token: 0x040085BE RID: 34238
			public static LocString NAME = UI.FormatAsLink("Polluted Mud", "TOXICMUD");

			// Token: 0x040085BF RID: 34239
			public static LocString DESC = string.Concat(new string[]
			{
				"A mixture of ",
				UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
				" and ",
				UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
				".\n\nCan be separated into its base elements using a ",
				UI.FormatAsLink("Sludge Press", "SLUDGEPRESS"),
				"."
			});
		}

		// Token: 0x02001DA7 RID: 7591
		public class MUD
		{
			// Token: 0x040085C0 RID: 34240
			public static LocString NAME = UI.FormatAsLink("Mud", "MUD");

			// Token: 0x040085C1 RID: 34241
			public static LocString DESC = string.Concat(new string[]
			{
				"A mixture of ",
				UI.FormatAsLink("Dirt", "DIRT"),
				" and ",
				UI.FormatAsLink("Water", "WATER"),
				".\n\nCan be separated into its base elements using a ",
				UI.FormatAsLink("Sludge Press", "SLUDGEPRESS"),
				"."
			});
		}

		// Token: 0x02001DA8 RID: 7592
		public class SUCROSE
		{
			// Token: 0x040085C2 RID: 34242
			public static LocString NAME = UI.FormatAsLink("Sucrose", "SUCROSE");

			// Token: 0x040085C3 RID: 34243
			public static LocString DESC = "(C<sub>12</sub>H<sub>22</sub>O<sub>11</sub>) Sucrose is the raw form of sugar.\n\nIt can be used directly for cooking, or refined and eaten with meals to increase Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";
		}

		// Token: 0x02001DA9 RID: 7593
		public class MOLTENSUCROSE
		{
			// Token: 0x040085C4 RID: 34244
			public static LocString NAME = UI.FormatAsLink("Sucrose", "MOLTENSUCROSE");

			// Token: 0x040085C5 RID: 34245
			public static LocString DESC = string.Concat(new string[]
			{
				"(C<sub>12</sub>H<sub>22</sub>O<sub>11</sub>) Sucrose is the raw form of sugar, heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state.\n\nIt can be used directly for cooking, or refined and eaten with meals to increase Duplicant ",
				UI.FormatAsLink("Morale", "MORALE"),
				"."
			});
		}
	}
}
