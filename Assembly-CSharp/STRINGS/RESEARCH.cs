using System;

namespace STRINGS
{
	// Token: 0x02000D43 RID: 3395
	public class RESEARCH
	{
		// Token: 0x02001CD9 RID: 7385
		public class MESSAGING
		{
			// Token: 0x04008411 RID: 33809
			public static LocString NORESEARCHSELECTED = "No research selected";

			// Token: 0x04008412 RID: 33810
			public static LocString RESEARCHTYPEREQUIRED = "{0} required";

			// Token: 0x04008413 RID: 33811
			public static LocString RESEARCHTYPEALSOREQUIRED = "{0} also required";

			// Token: 0x04008414 RID: 33812
			public static LocString NO_RESEARCHER_SKILL = "No Researchers assigned";

			// Token: 0x04008415 RID: 33813
			public static LocString NO_RESEARCHER_SKILL_TOOLTIP = "The selected research focus requires {ResearchType} to complete\n\nOpen the " + UI.FormatAsManagementMenu("Skills Panel", global::Action.ManageSkills) + " and teach a Duplicant the {ResearchType} Skill to use this building";

			// Token: 0x04008416 RID: 33814
			public static LocString MISSING_RESEARCH_STATION = "Missing Research Station";

			// Token: 0x04008417 RID: 33815
			public static LocString MISSING_RESEARCH_STATION_TOOLTIP = "The selected research focus requires a {0} to perform\n\nOpen the " + UI.FormatAsBuildMenuTab("Stations Tab", global::Action.Plan10) + " of the Build Menu to construct one";

			// Token: 0x02002899 RID: 10393
			public static class DLC
			{
				// Token: 0x0400ACE5 RID: 44261
				public static LocString EXPANSION1 = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"\n\n<i>",
					UI.DLC1.NAME,
					"</i>",
					UI.PST_KEYWORD,
					" DLC Content"
				});
			}
		}

		// Token: 0x02001CDA RID: 7386
		public class TYPES
		{
			// Token: 0x04008418 RID: 33816
			public static LocString MISSINGRECIPEDESC = "Missing Recipe Description";

			// Token: 0x0200289A RID: 10394
			public class ALPHA
			{
				// Token: 0x0400ACE6 RID: 44262
				public static LocString NAME = "Novice Research";

				// Token: 0x0400ACE7 RID: 44263
				public static LocString DESC = UI.FormatAsLink("Novice Research", "RESEARCH") + " is required to unlock basic technologies.\nIt can be conducted at a " + UI.FormatAsLink("Research Station", "RESEARCHCENTER") + ".";

				// Token: 0x0400ACE8 RID: 44264
				public static LocString RECIPEDESC = "Unlocks rudimentary technologies.";
			}

			// Token: 0x0200289B RID: 10395
			public class BETA
			{
				// Token: 0x0400ACE9 RID: 44265
				public static LocString NAME = "Advanced Research";

				// Token: 0x0400ACEA RID: 44266
				public static LocString DESC = UI.FormatAsLink("Advanced Research", "RESEARCH") + " is required to unlock improved technologies.\nIt can be conducted at a " + UI.FormatAsLink("Super Computer", "ADVANCEDRESEARCHCENTER") + ".";

				// Token: 0x0400ACEB RID: 44267
				public static LocString RECIPEDESC = "Unlocks improved technologies.";
			}

			// Token: 0x0200289C RID: 10396
			public class GAMMA
			{
				// Token: 0x0400ACEC RID: 44268
				public static LocString NAME = "Interstellar Research";

				// Token: 0x0400ACED RID: 44269
				public static LocString DESC = UI.FormatAsLink("Interstellar Research", "RESEARCH") + " is required to unlock space technologies.\nIt can be conducted at a " + UI.FormatAsLink("Virtual Planetarium", "COSMICRESEARCHCENTER") + ".";

				// Token: 0x0400ACEE RID: 44270
				public static LocString RECIPEDESC = "Unlocks cutting-edge technologies.";
			}

			// Token: 0x0200289D RID: 10397
			public class DELTA
			{
				// Token: 0x0400ACEF RID: 44271
				public static LocString NAME = "Applied Sciences Research";

				// Token: 0x0400ACF0 RID: 44272
				public static LocString DESC = UI.FormatAsLink("Applied Sciences Research", "RESEARCH") + " is required to unlock materials science technologies.\nIt can be conducted at a " + UI.FormatAsLink("Materials Study Terminal", "NUCLEARRESEARCHCENTER") + ".";

				// Token: 0x0400ACF1 RID: 44273
				public static LocString RECIPEDESC = "Unlocks next wave technologies.";
			}

			// Token: 0x0200289E RID: 10398
			public class ORBITAL
			{
				// Token: 0x0400ACF2 RID: 44274
				public static LocString NAME = "Data Analysis Research";

				// Token: 0x0400ACF3 RID: 44275
				public static LocString DESC = UI.FormatAsLink("Data Analysis Research", "RESEARCH") + " is required to unlock Data Analysis technologies.\nIt can be conducted at a " + UI.FormatAsLink("Orbital Data Collection Lab", "ORBITALRESEARCHCENTER") + ".";

				// Token: 0x0400ACF4 RID: 44276
				public static LocString RECIPEDESC = "Unlocks out-of-this-world technologies.";
			}
		}

		// Token: 0x02001CDB RID: 7387
		public class OTHER_TECH_ITEMS
		{
			// Token: 0x0200289F RID: 10399
			public class AUTOMATION_OVERLAY
			{
				// Token: 0x0400ACF5 RID: 44277
				public static LocString NAME = UI.FormatAsOverlay("Automation Overlay");

				// Token: 0x0400ACF6 RID: 44278
				public static LocString DESC = "Enables access to the " + UI.FormatAsOverlay("Automation Overlay") + ".";
			}

			// Token: 0x020028A0 RID: 10400
			public class SUITS_OVERLAY
			{
				// Token: 0x0400ACF7 RID: 44279
				public static LocString NAME = UI.FormatAsOverlay("Exosuit Overlay");

				// Token: 0x0400ACF8 RID: 44280
				public static LocString DESC = "Enables access to the " + UI.FormatAsOverlay("Exosuit Overlay") + ".";
			}

			// Token: 0x020028A1 RID: 10401
			public class JET_SUIT
			{
				// Token: 0x0400ACF9 RID: 44281
				public static LocString NAME = UI.PRE_KEYWORD + "Jet Suit" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400ACFA RID: 44282
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Jet Suits",
					UI.PST_KEYWORD,
					" at the ",
					BUILDINGS.PREFABS.SUITFABRICATOR.NAME
				});
			}

			// Token: 0x020028A2 RID: 10402
			public class OXYGEN_MASK
			{
				// Token: 0x0400ACFB RID: 44283
				public static LocString NAME = UI.PRE_KEYWORD + "Oxygen Mask" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400ACFC RID: 44284
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Oxygen Masks",
					UI.PST_KEYWORD,
					" at the ",
					BUILDINGS.PREFABS.CRAFTINGTABLE.NAME
				});
			}

			// Token: 0x020028A3 RID: 10403
			public class LEAD_SUIT
			{
				// Token: 0x0400ACFD RID: 44285
				public static LocString NAME = UI.PRE_KEYWORD + "Lead Suit" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400ACFE RID: 44286
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Lead Suits",
					UI.PST_KEYWORD,
					" at the ",
					BUILDINGS.PREFABS.SUITFABRICATOR.NAME
				});
			}

			// Token: 0x020028A4 RID: 10404
			public class ATMO_SUIT
			{
				// Token: 0x0400ACFF RID: 44287
				public static LocString NAME = UI.PRE_KEYWORD + "Atmo Suit" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400AD00 RID: 44288
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Atmo Suits",
					UI.PST_KEYWORD,
					" at the ",
					BUILDINGS.PREFABS.SUITFABRICATOR.NAME
				});
			}

			// Token: 0x020028A5 RID: 10405
			public class BETA_RESEARCH_POINT
			{
				// Token: 0x0400AD01 RID: 44289
				public static LocString NAME = UI.PRE_KEYWORD + "Advanced Research" + UI.PST_KEYWORD + " Capability";

				// Token: 0x0400AD02 RID: 44290
				public static LocString DESC = string.Concat(new string[]
				{
					"Allows ",
					UI.PRE_KEYWORD,
					"Advanced Research",
					UI.PST_KEYWORD,
					" points to be accumulated, unlocking higher technology tiers."
				});
			}

			// Token: 0x020028A6 RID: 10406
			public class GAMMA_RESEARCH_POINT
			{
				// Token: 0x0400AD03 RID: 44291
				public static LocString NAME = UI.PRE_KEYWORD + "Interstellar Research" + UI.PST_KEYWORD + " Capability";

				// Token: 0x0400AD04 RID: 44292
				public static LocString DESC = string.Concat(new string[]
				{
					"Allows ",
					UI.PRE_KEYWORD,
					"Interstellar Research",
					UI.PST_KEYWORD,
					" points to be accumulated, unlocking higher technology tiers."
				});
			}

			// Token: 0x020028A7 RID: 10407
			public class DELTA_RESEARCH_POINT
			{
				// Token: 0x0400AD05 RID: 44293
				public static LocString NAME = UI.PRE_KEYWORD + "Materials Science Research" + UI.PST_KEYWORD + " Capability";

				// Token: 0x0400AD06 RID: 44294
				public static LocString DESC = string.Concat(new string[]
				{
					"Allows ",
					UI.PRE_KEYWORD,
					"Materials Science Research",
					UI.PST_KEYWORD,
					" points to be accumulated, unlocking higher technology tiers."
				});
			}

			// Token: 0x020028A8 RID: 10408
			public class ORBITAL_RESEARCH_POINT
			{
				// Token: 0x0400AD07 RID: 44295
				public static LocString NAME = UI.PRE_KEYWORD + "Data Analysis Research" + UI.PST_KEYWORD + " Capability";

				// Token: 0x0400AD08 RID: 44296
				public static LocString DESC = string.Concat(new string[]
				{
					"Allows ",
					UI.PRE_KEYWORD,
					"Data Analysis Research",
					UI.PST_KEYWORD,
					" points to be accumulated, unlocking higher technology tiers."
				});
			}

			// Token: 0x020028A9 RID: 10409
			public class CONVEYOR_OVERLAY
			{
				// Token: 0x0400AD09 RID: 44297
				public static LocString NAME = UI.FormatAsOverlay("Conveyor Overlay");

				// Token: 0x0400AD0A RID: 44298
				public static LocString DESC = "Enables access to the " + UI.FormatAsOverlay("Conveyor Overlay") + ".";
			}
		}

		// Token: 0x02001CDC RID: 7388
		public class TREES
		{
			// Token: 0x04008419 RID: 33817
			public static LocString TITLE_FOOD = "Food";

			// Token: 0x0400841A RID: 33818
			public static LocString TITLE_POWER = "Power";

			// Token: 0x0400841B RID: 33819
			public static LocString TITLE_SOLIDS = "Solid Material";

			// Token: 0x0400841C RID: 33820
			public static LocString TITLE_COLONYDEVELOPMENT = "Colony Development";

			// Token: 0x0400841D RID: 33821
			public static LocString TITLE_RADIATIONTECH = "Radiation Technologies";

			// Token: 0x0400841E RID: 33822
			public static LocString TITLE_MEDICINE = "Medicine";

			// Token: 0x0400841F RID: 33823
			public static LocString TITLE_LIQUIDS = "Liquids";

			// Token: 0x04008420 RID: 33824
			public static LocString TITLE_GASES = "Gases";

			// Token: 0x04008421 RID: 33825
			public static LocString TITLE_SUITS = "Exosuits";

			// Token: 0x04008422 RID: 33826
			public static LocString TITLE_DECOR = "Decor";

			// Token: 0x04008423 RID: 33827
			public static LocString TITLE_COMPUTERS = "Computers";

			// Token: 0x04008424 RID: 33828
			public static LocString TITLE_ROCKETS = "Rocketry";
		}

		// Token: 0x02001CDD RID: 7389
		public class TECHS
		{
			// Token: 0x020028AA RID: 10410
			public class JOBS
			{
				// Token: 0x0400AD0B RID: 44299
				public static LocString NAME = UI.FormatAsLink("Employment", "JOBS");

				// Token: 0x0400AD0C RID: 44300
				public static LocString DESC = "Exchange the skill points earned by Duplicants for new traits and abilities.";
			}

			// Token: 0x020028AB RID: 10411
			public class IMPROVEDOXYGEN
			{
				// Token: 0x0400AD0D RID: 44301
				public static LocString NAME = UI.FormatAsLink("Air Systems", "IMPROVEDOXYGEN");

				// Token: 0x0400AD0E RID: 44302
				public static LocString DESC = "Maintain clean, breathable air in the colony.";
			}

			// Token: 0x020028AC RID: 10412
			public class FARMINGTECH
			{
				// Token: 0x0400AD0F RID: 44303
				public static LocString NAME = UI.FormatAsLink("Basic Farming", "FARMINGTECH");

				// Token: 0x0400AD10 RID: 44304
				public static LocString DESC = "Learn the introductory principles of " + UI.FormatAsLink("Plant", "PLANTS") + " domestication.";
			}

			// Token: 0x020028AD RID: 10413
			public class AGRICULTURE
			{
				// Token: 0x0400AD11 RID: 44305
				public static LocString NAME = UI.FormatAsLink("Agriculture", "AGRICULTURE");

				// Token: 0x0400AD12 RID: 44306
				public static LocString DESC = "Master the agricultural art of crop raising.";
			}

			// Token: 0x020028AE RID: 10414
			public class RANCHING
			{
				// Token: 0x0400AD13 RID: 44307
				public static LocString NAME = UI.FormatAsLink("Ranching", "RANCHING");

				// Token: 0x0400AD14 RID: 44308
				public static LocString DESC = "Tame and care for wild critters.";
			}

			// Token: 0x020028AF RID: 10415
			public class ANIMALCONTROL
			{
				// Token: 0x0400AD15 RID: 44309
				public static LocString NAME = UI.FormatAsLink("Animal Control", "ANIMALCONTROL");

				// Token: 0x0400AD16 RID: 44310
				public static LocString DESC = "Useful techniques to manage critter populations in the colony.";
			}

			// Token: 0x020028B0 RID: 10416
			public class FOODREPURPOSING
			{
				// Token: 0x0400AD17 RID: 44311
				public static LocString NAME = UI.FormatAsLink("Food Repurposing", "FOODREPURPOSING");

				// Token: 0x0400AD18 RID: 44312
				public static LocString DESC = string.Concat(new string[]
				{
					"Blend that leftover ",
					UI.FormatAsLink("Food", "FOOD"),
					" into a ",
					UI.FormatAsLink("Morale", "MORALE"),
					" boosting slurry."
				});
			}

			// Token: 0x020028B1 RID: 10417
			public class FINEDINING
			{
				// Token: 0x0400AD19 RID: 44313
				public static LocString NAME = UI.FormatAsLink("Meal Preparation", "FINEDINING");

				// Token: 0x0400AD1A RID: 44314
				public static LocString DESC = "Prepare more nutritious " + UI.FormatAsLink("Food", "FOOD") + " and store it longer before spoiling.";
			}

			// Token: 0x020028B2 RID: 10418
			public class FINERDINING
			{
				// Token: 0x0400AD1B RID: 44315
				public static LocString NAME = UI.FormatAsLink("Gourmet Meal Preparation", "FINERDINING");

				// Token: 0x0400AD1C RID: 44316
				public static LocString DESC = "Raise colony Morale by cooking the most delicious, high-quality " + UI.FormatAsLink("Foods", "FOOD") + ".";
			}

			// Token: 0x020028B3 RID: 10419
			public class GASPIPING
			{
				// Token: 0x0400AD1D RID: 44317
				public static LocString NAME = UI.FormatAsLink("Ventilation", "GASPIPING");

				// Token: 0x0400AD1E RID: 44318
				public static LocString DESC = "Rudimentary technologies for installing " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " infrastructure.";
			}

			// Token: 0x020028B4 RID: 10420
			public class IMPROVEDGASPIPING
			{
				// Token: 0x0400AD1F RID: 44319
				public static LocString NAME = UI.FormatAsLink("Improved Ventilation", "IMPROVEDGASPIPING");

				// Token: 0x0400AD20 RID: 44320
				public static LocString DESC = UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " infrastructure capable of withstanding more intense conditions, such as " + UI.FormatAsLink("Heat", "Heat") + " and pressure.";
			}

			// Token: 0x020028B5 RID: 10421
			public class FLOWREDIRECTION
			{
				// Token: 0x0400AD21 RID: 44321
				public static LocString NAME = UI.FormatAsLink("Flow Redirection", "FLOWREDIRECTION");

				// Token: 0x0400AD22 RID: 44322
				public static LocString DESC = "Balance on irrigated concave platforms for a " + UI.FormatAsLink("Morale", "MORALE") + " boost.";
			}

			// Token: 0x020028B6 RID: 10422
			public class LIQUIDDISTRIBUTION
			{
				// Token: 0x0400AD23 RID: 44323
				public static LocString NAME = UI.FormatAsLink("Liquid Distribution", "LIQUIDDISTRIBUTION");

				// Token: 0x0400AD24 RID: 44324
				public static LocString DESC = "Internal rocket hookups for liquid resources.";
			}

			// Token: 0x020028B7 RID: 10423
			public class TEMPERATUREMODULATION
			{
				// Token: 0x0400AD25 RID: 44325
				public static LocString NAME = UI.FormatAsLink("Temperature Modulation", "TEMPERATUREMODULATION");

				// Token: 0x0400AD26 RID: 44326
				public static LocString DESC = "Precise " + UI.FormatAsLink("Temperature", "HEAT") + " altering technologies to keep my colony at the perfect Kelvin.";
			}

			// Token: 0x020028B8 RID: 10424
			public class HVAC
			{
				// Token: 0x0400AD27 RID: 44327
				public static LocString NAME = UI.FormatAsLink("HVAC", "HVAC");

				// Token: 0x0400AD28 RID: 44328
				public static LocString DESC = string.Concat(new string[]
				{
					"Regulate ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" in the colony for ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" cultivation and Duplicant comfort."
				});
			}

			// Token: 0x020028B9 RID: 10425
			public class GASDISTRIBUTION
			{
				// Token: 0x0400AD29 RID: 44329
				public static LocString NAME = UI.FormatAsLink("Gas Distribution", "GASDISTRIBUTION");

				// Token: 0x0400AD2A RID: 44330
				public static LocString DESC = "Internal rocket hookups for gas resources.";
			}

			// Token: 0x020028BA RID: 10426
			public class LIQUIDTEMPERATURE
			{
				// Token: 0x0400AD2B RID: 44331
				public static LocString NAME = UI.FormatAsLink("Liquid Tuning", "LIQUIDTEMPERATURE");

				// Token: 0x0400AD2C RID: 44332
				public static LocString DESC = string.Concat(new string[]
				{
					"Easily manipulate ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" ",
					UI.FormatAsLink("Heat", "Temperatures"),
					" with these temperature regulating technologies."
				});
			}

			// Token: 0x020028BB RID: 10427
			public class INSULATION
			{
				// Token: 0x0400AD2D RID: 44333
				public static LocString NAME = UI.FormatAsLink("Insulation", "INSULATION");

				// Token: 0x0400AD2E RID: 44334
				public static LocString DESC = "Improve " + UI.FormatAsLink("Heat", "Heat") + " distribution within the colony and guard buildings from extreme temperatures.";
			}

			// Token: 0x020028BC RID: 10428
			public class PRESSUREMANAGEMENT
			{
				// Token: 0x0400AD2F RID: 44335
				public static LocString NAME = UI.FormatAsLink("Pressure Management", "PRESSUREMANAGEMENT");

				// Token: 0x0400AD30 RID: 44336
				public static LocString DESC = "Unlock technologies to manage colony pressure and atmosphere.";
			}

			// Token: 0x020028BD RID: 10429
			public class PORTABLEGASSES
			{
				// Token: 0x0400AD31 RID: 44337
				public static LocString NAME = UI.FormatAsLink("Portable Gases", "PORTABLEGASSES");

				// Token: 0x0400AD32 RID: 44338
				public static LocString DESC = "Unlock technologies to easily move gases around your colony.";
			}

			// Token: 0x020028BE RID: 10430
			public class DIRECTEDAIRSTREAMS
			{
				// Token: 0x0400AD33 RID: 44339
				public static LocString NAME = UI.FormatAsLink("Decontamination", "DIRECTEDAIRSTREAMS");

				// Token: 0x0400AD34 RID: 44340
				public static LocString DESC = "Instruments to help reduce " + UI.FormatAsLink("Germ", "DISEASE") + " spread within the base.";
			}

			// Token: 0x020028BF RID: 10431
			public class LIQUIDFILTERING
			{
				// Token: 0x0400AD35 RID: 44341
				public static LocString NAME = UI.FormatAsLink("Liquid-Based Refinement Processes", "LIQUIDFILTERING");

				// Token: 0x0400AD36 RID: 44342
				public static LocString DESC = "Use pumped " + UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID") + " to filter out unwanted elements.";
			}

			// Token: 0x020028C0 RID: 10432
			public class LIQUIDPIPING
			{
				// Token: 0x0400AD37 RID: 44343
				public static LocString NAME = UI.FormatAsLink("Plumbing", "LIQUIDPIPING");

				// Token: 0x0400AD38 RID: 44344
				public static LocString DESC = "Rudimentary technologies for installing " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " infrastructure.";
			}

			// Token: 0x020028C1 RID: 10433
			public class IMPROVEDLIQUIDPIPING
			{
				// Token: 0x0400AD39 RID: 44345
				public static LocString NAME = UI.FormatAsLink("Improved Plumbing", "IMPROVEDLIQUIDPIPING");

				// Token: 0x0400AD3A RID: 44346
				public static LocString DESC = UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " infrastructure capable of withstanding more intense conditions, such as " + UI.FormatAsLink("Heat", "Heat") + " and pressure.";
			}

			// Token: 0x020028C2 RID: 10434
			public class PRECISIONPLUMBING
			{
				// Token: 0x0400AD3B RID: 44347
				public static LocString NAME = UI.FormatAsLink("Advanced Caffeination", "PRECISIONPLUMBING");

				// Token: 0x0400AD3C RID: 44348
				public static LocString DESC = "Let Duplicants relax after a long day of subterranean digging with a shot of warm beanjuice.";
			}

			// Token: 0x020028C3 RID: 10435
			public class SANITATIONSCIENCES
			{
				// Token: 0x0400AD3D RID: 44349
				public static LocString NAME = UI.FormatAsLink("Sanitation", "SANITATIONSCIENCES");

				// Token: 0x0400AD3E RID: 44350
				public static LocString DESC = "Make daily ablutions less of a hassle.";
			}

			// Token: 0x020028C4 RID: 10436
			public class ADVANCEDSANITATION
			{
				// Token: 0x0400AD3F RID: 44351
				public static LocString NAME = UI.FormatAsLink("Advanced Sanitation", "ADVANCEDSANITATION");

				// Token: 0x0400AD40 RID: 44352
				public static LocString DESC = "Clean up dirty Duplicants.";
			}

			// Token: 0x020028C5 RID: 10437
			public class MEDICINEI
			{
				// Token: 0x0400AD41 RID: 44353
				public static LocString NAME = UI.FormatAsLink("Pharmacology", "MEDICINEI");

				// Token: 0x0400AD42 RID: 44354
				public static LocString DESC = "Compound natural cures to fight the most common " + UI.FormatAsLink("Sicknesses", "SICKNESSES") + " that plague Duplicants.";
			}

			// Token: 0x020028C6 RID: 10438
			public class MEDICINEII
			{
				// Token: 0x0400AD43 RID: 44355
				public static LocString NAME = UI.FormatAsLink("Medical Equipment", "MEDICINEII");

				// Token: 0x0400AD44 RID: 44356
				public static LocString DESC = "The basic necessities doctors need to facilitate patient care.";
			}

			// Token: 0x020028C7 RID: 10439
			public class MEDICINEIII
			{
				// Token: 0x0400AD45 RID: 44357
				public static LocString NAME = UI.FormatAsLink("Pathogen Diagnostics", "MEDICINEIII");

				// Token: 0x0400AD46 RID: 44358
				public static LocString DESC = "Stop Germs at the source using special medical automation technology.";
			}

			// Token: 0x020028C8 RID: 10440
			public class MEDICINEIV
			{
				// Token: 0x0400AD47 RID: 44359
				public static LocString NAME = UI.FormatAsLink("Micro-Targeted Medicine", "MEDICINEIV");

				// Token: 0x0400AD48 RID: 44360
				public static LocString DESC = "State of the art equipment to conquer the most stubborn of illnesses.";
			}

			// Token: 0x020028C9 RID: 10441
			public class ADVANCEDFILTRATION
			{
				// Token: 0x0400AD49 RID: 44361
				public static LocString NAME = UI.FormatAsLink("Filtration", "ADVANCEDFILTRATION");

				// Token: 0x0400AD4A RID: 44362
				public static LocString DESC = string.Concat(new string[]
				{
					"Basic technologies for filtering ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					" and ",
					UI.FormatAsLink("Gases", "ELEMENTS_GAS"),
					"."
				});
			}

			// Token: 0x020028CA RID: 10442
			public class POWERREGULATION
			{
				// Token: 0x0400AD4B RID: 44363
				public static LocString NAME = UI.FormatAsLink("Power Regulation", "POWERREGULATION");

				// Token: 0x0400AD4C RID: 44364
				public static LocString DESC = "Prevent wasted " + UI.FormatAsLink("Power", "POWER") + " with improved electrical tools.";
			}

			// Token: 0x020028CB RID: 10443
			public class COMBUSTION
			{
				// Token: 0x0400AD4D RID: 44365
				public static LocString NAME = UI.FormatAsLink("Internal Combustion", "COMBUSTION");

				// Token: 0x0400AD4E RID: 44366
				public static LocString DESC = "Fuel-powered generators for crude yet powerful " + UI.FormatAsLink("Power", "POWER") + " production.";
			}

			// Token: 0x020028CC RID: 10444
			public class IMPROVEDCOMBUSTION
			{
				// Token: 0x0400AD4F RID: 44367
				public static LocString NAME = UI.FormatAsLink("Fossil Fuels", "IMPROVEDCOMBUSTION");

				// Token: 0x0400AD50 RID: 44368
				public static LocString DESC = "Burn dirty fuels for exceptional " + UI.FormatAsLink("Power", "POWER") + " production.";
			}

			// Token: 0x020028CD RID: 10445
			public class INTERIORDECOR
			{
				// Token: 0x0400AD51 RID: 44369
				public static LocString NAME = UI.FormatAsLink("Interior Decor", "INTERIORDECOR");

				// Token: 0x0400AD52 RID: 44370
				public static LocString DESC = UI.FormatAsLink("Decor", "DECOR") + " boosting items to counteract the gloom of underground living.";
			}

			// Token: 0x020028CE RID: 10446
			public class ARTISTRY
			{
				// Token: 0x0400AD53 RID: 44371
				public static LocString NAME = UI.FormatAsLink("Artistic Expression", "ARTISTRY");

				// Token: 0x0400AD54 RID: 44372
				public static LocString DESC = "Majorly improve " + UI.FormatAsLink("Decor", "DECOR") + " by giving Duplicants the tools of artistic and emotional expression.";
			}

			// Token: 0x020028CF RID: 10447
			public class CLOTHING
			{
				// Token: 0x0400AD55 RID: 44373
				public static LocString NAME = UI.FormatAsLink("Textile Production", "CLOTHING");

				// Token: 0x0400AD56 RID: 44374
				public static LocString DESC = "Bring Duplicants the " + UI.FormatAsLink("Morale", "MORALE") + " boosting benefits of soft, cushy fabrics.";
			}

			// Token: 0x020028D0 RID: 10448
			public class ACOUSTICS
			{
				// Token: 0x0400AD57 RID: 44375
				public static LocString NAME = UI.FormatAsLink("Sound Amplifiers", "ACOUSTICS");

				// Token: 0x0400AD58 RID: 44376
				public static LocString DESC = "Precise control of the audio spectrum allows Duplicants to get funky.";
			}

			// Token: 0x020028D1 RID: 10449
			public class SPACEPOWER
			{
				// Token: 0x0400AD59 RID: 44377
				public static LocString NAME = UI.FormatAsLink("Space Power", "SPACEPOWER");

				// Token: 0x0400AD5A RID: 44378
				public static LocString DESC = "It's like power... in space!";
			}

			// Token: 0x020028D2 RID: 10450
			public class AMPLIFIERS
			{
				// Token: 0x0400AD5B RID: 44379
				public static LocString NAME = UI.FormatAsLink("Power Amplifiers", "AMPLIFIERS");

				// Token: 0x0400AD5C RID: 44380
				public static LocString DESC = "Further increased efficacy of " + UI.FormatAsLink("Power", "POWER") + " management to prevent those wasted joules.";
			}

			// Token: 0x020028D3 RID: 10451
			public class LUXURY
			{
				// Token: 0x0400AD5D RID: 44381
				public static LocString NAME = UI.FormatAsLink("Home Luxuries", "LUXURY");

				// Token: 0x0400AD5E RID: 44382
				public static LocString DESC = "Luxury amenities for advanced " + UI.FormatAsLink("Stress", "STRESS") + " reduction.";
			}

			// Token: 0x020028D4 RID: 10452
			public class ENVIRONMENTALAPPRECIATION
			{
				// Token: 0x0400AD5F RID: 44383
				public static LocString NAME = UI.FormatAsLink("Environmental Appreciation", "ENVIRONMENTALAPPRECIATION");

				// Token: 0x0400AD60 RID: 44384
				public static LocString DESC = string.Concat(new string[]
				{
					"Improve ",
					UI.FormatAsLink("Morale", "MORALE"),
					" by lazing around in ",
					UI.FormatAsLink("Light", "LIGHT"),
					" with a high Lux value."
				});
			}

			// Token: 0x020028D5 RID: 10453
			public class FINEART
			{
				// Token: 0x0400AD61 RID: 44385
				public static LocString NAME = UI.FormatAsLink("Fine Art", "FINEART");

				// Token: 0x0400AD62 RID: 44386
				public static LocString DESC = "Broader options for artistic " + UI.FormatAsLink("Decor", "DECOR") + " improvements.";
			}

			// Token: 0x020028D6 RID: 10454
			public class REFRACTIVEDECOR
			{
				// Token: 0x0400AD63 RID: 44387
				public static LocString NAME = UI.FormatAsLink("High Culture", "REFRACTIVEDECOR");

				// Token: 0x0400AD64 RID: 44388
				public static LocString DESC = "New methods for working with extremely high quality art materials.";
			}

			// Token: 0x020028D7 RID: 10455
			public class RENAISSANCEART
			{
				// Token: 0x0400AD65 RID: 44389
				public static LocString NAME = UI.FormatAsLink("Renaissance Art", "RENAISSANCEART");

				// Token: 0x0400AD66 RID: 44390
				public static LocString DESC = "The kind of art that culture legacies are made of.";
			}

			// Token: 0x020028D8 RID: 10456
			public class GLASSFURNISHINGS
			{
				// Token: 0x0400AD67 RID: 44391
				public static LocString NAME = UI.FormatAsLink("Glass Blowing", "GLASSFURNISHINGS");

				// Token: 0x0400AD68 RID: 44392
				public static LocString DESC = "The decorative benefits of glass are both apparent and transparent.";
			}

			// Token: 0x020028D9 RID: 10457
			public class SCREENS
			{
				// Token: 0x0400AD69 RID: 44393
				public static LocString NAME = UI.FormatAsLink("New Media", "SCREENS");

				// Token: 0x0400AD6A RID: 44394
				public static LocString DESC = "High tech displays with lots of pretty colors.";
			}

			// Token: 0x020028DA RID: 10458
			public class ADVANCEDPOWERREGULATION
			{
				// Token: 0x0400AD6B RID: 44395
				public static LocString NAME = UI.FormatAsLink("Advanced Power Regulation", "ADVANCEDPOWERREGULATION");

				// Token: 0x0400AD6C RID: 44396
				public static LocString DESC = "Circuit components required for large scale " + UI.FormatAsLink("Power", "POWER") + " management.";
			}

			// Token: 0x020028DB RID: 10459
			public class PLASTICS
			{
				// Token: 0x0400AD6D RID: 44397
				public static LocString NAME = UI.FormatAsLink("Plastic Manufacturing", "PLASTICS");

				// Token: 0x0400AD6E RID: 44398
				public static LocString DESC = "Stable, lightweight, durable. Plastics are useful for a wide array of applications.";
			}

			// Token: 0x020028DC RID: 10460
			public class SUITS
			{
				// Token: 0x0400AD6F RID: 44399
				public static LocString NAME = UI.FormatAsLink("Hazard Protection", "SUITS");

				// Token: 0x0400AD70 RID: 44400
				public static LocString DESC = "Vital gear for surviving in extreme conditions and environments.";
			}

			// Token: 0x020028DD RID: 10461
			public class DISTILLATION
			{
				// Token: 0x0400AD71 RID: 44401
				public static LocString NAME = UI.FormatAsLink("Distillation", "DISTILLATION");

				// Token: 0x0400AD72 RID: 44402
				public static LocString DESC = "Distill difficult mixtures down to their most useful parts.";
			}

			// Token: 0x020028DE RID: 10462
			public class CATALYTICS
			{
				// Token: 0x0400AD73 RID: 44403
				public static LocString NAME = UI.FormatAsLink("Catalytics", "CATALYTICS");

				// Token: 0x0400AD74 RID: 44404
				public static LocString DESC = "Advanced gas manipulation using unique catalysts.";
			}

			// Token: 0x020028DF RID: 10463
			public class ADVANCEDRESEARCH
			{
				// Token: 0x0400AD75 RID: 44405
				public static LocString NAME = UI.FormatAsLink("Advanced Research", "ADVANCEDRESEARCH");

				// Token: 0x0400AD76 RID: 44406
				public static LocString DESC = "The tools my colony needs to conduct more advanced, in-depth research.";
			}

			// Token: 0x020028E0 RID: 10464
			public class SPACEPROGRAM
			{
				// Token: 0x0400AD77 RID: 44407
				public static LocString NAME = UI.FormatAsLink("Space Program", "SPACEPROGRAM");

				// Token: 0x0400AD78 RID: 44408
				public static LocString DESC = "The first steps in getting a Duplicant to space.";
			}

			// Token: 0x020028E1 RID: 10465
			public class CRASHPLAN
			{
				// Token: 0x0400AD79 RID: 44409
				public static LocString NAME = UI.FormatAsLink("Crash Plan", "CRASHPLAN");

				// Token: 0x0400AD7A RID: 44410
				public static LocString DESC = "What goes up, must come down.";
			}

			// Token: 0x020028E2 RID: 10466
			public class DURABLELIFESUPPORT
			{
				// Token: 0x0400AD7B RID: 44411
				public static LocString NAME = UI.FormatAsLink("Durable Life Support", "DURABLELIFESUPPORT");

				// Token: 0x0400AD7C RID: 44412
				public static LocString DESC = "Improved devices for extended missions into space.";
			}

			// Token: 0x020028E3 RID: 10467
			public class ARTIFICIALFRIENDS
			{
				// Token: 0x0400AD7D RID: 44413
				public static LocString NAME = UI.FormatAsLink("Artificial Friends", "ARTIFICIALFRIENDS");

				// Token: 0x0400AD7E RID: 44414
				public static LocString DESC = "Sweeping advances in companion technology.";
			}

			// Token: 0x020028E4 RID: 10468
			public class ROBOTICTOOLS
			{
				// Token: 0x0400AD7F RID: 44415
				public static LocString NAME = UI.FormatAsLink("Robotic Tools", "ROBOTICTOOLS");

				// Token: 0x0400AD80 RID: 44416
				public static LocString DESC = "The goal of every great civilization is to one day make itself obsolete.";
			}

			// Token: 0x020028E5 RID: 10469
			public class LOGICCONTROL
			{
				// Token: 0x0400AD81 RID: 44417
				public static LocString NAME = UI.FormatAsLink("Smart Home", "LOGICCONTROL");

				// Token: 0x0400AD82 RID: 44418
				public static LocString DESC = "Switches that grant full control of building operations within the colony.";
			}

			// Token: 0x020028E6 RID: 10470
			public class LOGICCIRCUITS
			{
				// Token: 0x0400AD83 RID: 44419
				public static LocString NAME = UI.FormatAsLink("Advanced Automation", "LOGICCIRCUITS");

				// Token: 0x0400AD84 RID: 44420
				public static LocString DESC = "The only limit to colony automation is my own imagination.";
			}

			// Token: 0x020028E7 RID: 10471
			public class PARALLELAUTOMATION
			{
				// Token: 0x0400AD85 RID: 44421
				public static LocString NAME = UI.FormatAsLink("Parallel Automation", "PARALLELAUTOMATION");

				// Token: 0x0400AD86 RID: 44422
				public static LocString DESC = "Multi-wire automation at a fraction of the space.";
			}

			// Token: 0x020028E8 RID: 10472
			public class MULTIPLEXING
			{
				// Token: 0x0400AD87 RID: 44423
				public static LocString NAME = UI.FormatAsLink("Multiplexing", "MULTIPLEXING");

				// Token: 0x0400AD88 RID: 44424
				public static LocString DESC = "More choices for Automation signal distribution.";
			}

			// Token: 0x020028E9 RID: 10473
			public class VALVEMINIATURIZATION
			{
				// Token: 0x0400AD89 RID: 44425
				public static LocString NAME = UI.FormatAsLink("Valve Miniaturization", "VALVEMINIATURIZATION");

				// Token: 0x0400AD8A RID: 44426
				public static LocString DESC = "Smaller, more efficient pumps for those low-throughput situations.";
			}

			// Token: 0x020028EA RID: 10474
			public class HYDROCARBONPROPULSION
			{
				// Token: 0x0400AD8B RID: 44427
				public static LocString NAME = UI.FormatAsLink("Hydrocarbon Propulsion", "HYDROCARBONPROPULSION");

				// Token: 0x0400AD8C RID: 44428
				public static LocString DESC = "Low-range rocket engines with lots of smoke.";
			}

			// Token: 0x020028EB RID: 10475
			public class BETTERHYDROCARBONPROPULSION
			{
				// Token: 0x0400AD8D RID: 44429
				public static LocString NAME = UI.FormatAsLink("Improved Hydrocarbon Propulsion", "BETTERHYDROCARBONPROPULSION");

				// Token: 0x0400AD8E RID: 44430
				public static LocString DESC = "Mid-range rocket engines with lots of smoke.";
			}

			// Token: 0x020028EC RID: 10476
			public class PRETTYGOODCONDUCTORS
			{
				// Token: 0x0400AD8F RID: 44431
				public static LocString NAME = UI.FormatAsLink("Low-Resistance Conductors", "PRETTYGOODCONDUCTORS");

				// Token: 0x0400AD90 RID: 44432
				public static LocString DESC = "Pure-core wires that can handle more " + UI.FormatAsLink("Electrical", "POWER") + " current without overloading.";
			}

			// Token: 0x020028ED RID: 10477
			public class RENEWABLEENERGY
			{
				// Token: 0x0400AD91 RID: 44433
				public static LocString NAME = UI.FormatAsLink("Renewable Energy", "RENEWABLEENERGY");

				// Token: 0x0400AD92 RID: 44434
				public static LocString DESC = "Clean, sustainable " + UI.FormatAsLink("Power", "POWER") + " production that produces little to no waste.";
			}

			// Token: 0x020028EE RID: 10478
			public class BASICREFINEMENT
			{
				// Token: 0x0400AD93 RID: 44435
				public static LocString NAME = UI.FormatAsLink("Brute-Force Refinement", "BASICREFINEMENT");

				// Token: 0x0400AD94 RID: 44436
				public static LocString DESC = "Low-tech refinement methods for producing clay and renewable sources of sand.";
			}

			// Token: 0x020028EF RID: 10479
			public class REFINEDOBJECTS
			{
				// Token: 0x0400AD95 RID: 44437
				public static LocString NAME = UI.FormatAsLink("Refined Renovations", "REFINEDOBJECTS");

				// Token: 0x0400AD96 RID: 44438
				public static LocString DESC = "Improve base infrastructure with new objects crafted from " + UI.FormatAsLink("Refined Metals", "REFINEDMETAL") + ".";
			}

			// Token: 0x020028F0 RID: 10480
			public class GENERICSENSORS
			{
				// Token: 0x0400AD97 RID: 44439
				public static LocString NAME = UI.FormatAsLink("Generic Sensors", "GENERICSENSORS");

				// Token: 0x0400AD98 RID: 44440
				public static LocString DESC = "Drive automation in a variety of new, inventive ways.";
			}

			// Token: 0x020028F1 RID: 10481
			public class DUPETRAFFICCONTROL
			{
				// Token: 0x0400AD99 RID: 44441
				public static LocString NAME = UI.FormatAsLink("Computing", "DUPETRAFFICCONTROL");

				// Token: 0x0400AD9A RID: 44442
				public static LocString DESC = "Virtually extend the boundaries of Duplicant imagination.";
			}

			// Token: 0x020028F2 RID: 10482
			public class ADVANCEDSCANNERS
			{
				// Token: 0x0400AD9B RID: 44443
				public static LocString NAME = UI.FormatAsLink("Sensitive Microimaging", "ADVANCEDSCANNERS");

				// Token: 0x0400AD9C RID: 44444
				public static LocString DESC = "Computerized systems do the looking, so Duplicants don't have to.";
			}

			// Token: 0x020028F3 RID: 10483
			public class SMELTING
			{
				// Token: 0x0400AD9D RID: 44445
				public static LocString NAME = UI.FormatAsLink("Smelting", "SMELTING");

				// Token: 0x0400AD9E RID: 44446
				public static LocString DESC = "High temperatures facilitate the production of purer, special use metal resources.";
			}

			// Token: 0x020028F4 RID: 10484
			public class TRAVELTUBES
			{
				// Token: 0x0400AD9F RID: 44447
				public static LocString NAME = UI.FormatAsLink("Transit Tubes", "TRAVELTUBES");

				// Token: 0x0400ADA0 RID: 44448
				public static LocString DESC = "A wholly futuristic way to move Duplicants around the base.";
			}

			// Token: 0x020028F5 RID: 10485
			public class SMARTSTORAGE
			{
				// Token: 0x0400ADA1 RID: 44449
				public static LocString NAME = UI.FormatAsLink("Smart Storage", "SMARTSTORAGE");

				// Token: 0x0400ADA2 RID: 44450
				public static LocString DESC = "Completely automate the storage of solid resources.";
			}

			// Token: 0x020028F6 RID: 10486
			public class SOLIDTRANSPORT
			{
				// Token: 0x0400ADA3 RID: 44451
				public static LocString NAME = UI.FormatAsLink("Solid Transport", "SOLIDTRANSPORT");

				// Token: 0x0400ADA4 RID: 44452
				public static LocString DESC = "Free Duplicants from the drudgery of day-to-day material deliveries with new methods of automation.";
			}

			// Token: 0x020028F7 RID: 10487
			public class SOLIDMANAGEMENT
			{
				// Token: 0x0400ADA5 RID: 44453
				public static LocString NAME = UI.FormatAsLink("Solid Management", "SOLIDMANAGEMENT");

				// Token: 0x0400ADA6 RID: 44454
				public static LocString DESC = "Make solid decisions in " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " sorting.";
			}

			// Token: 0x020028F8 RID: 10488
			public class SOLIDDISTRIBUTION
			{
				// Token: 0x0400ADA7 RID: 44455
				public static LocString NAME = UI.FormatAsLink("Solid Distribution", "SOLIDDISTRIBUTION");

				// Token: 0x0400ADA8 RID: 44456
				public static LocString DESC = "Internal rocket hookups for " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " resources.";
			}

			// Token: 0x020028F9 RID: 10489
			public class HIGHTEMPFORGING
			{
				// Token: 0x0400ADA9 RID: 44457
				public static LocString NAME = UI.FormatAsLink("Superheated Forging", "HIGHTEMPFORGING");

				// Token: 0x0400ADAA RID: 44458
				public static LocString DESC = "Craft entirely new materials by harnessing the most extreme temperatures.";
			}

			// Token: 0x020028FA RID: 10490
			public class HIGHPRESSUREFORGING
			{
				// Token: 0x0400ADAB RID: 44459
				public static LocString NAME = UI.FormatAsLink("Pressurized Forging", "HIGHPRESSUREFORGING");

				// Token: 0x0400ADAC RID: 44460
				public static LocString DESC = "High pressure diamond forging.";
			}

			// Token: 0x020028FB RID: 10491
			public class RADIATIONPROTECTION
			{
				// Token: 0x0400ADAD RID: 44461
				public static LocString NAME = UI.FormatAsLink("Radiation Protection", "RADIATIONPROTECTION");

				// Token: 0x0400ADAE RID: 44462
				public static LocString DESC = "Shield Duplicants from dangerous amounts of radiation.";
			}

			// Token: 0x020028FC RID: 10492
			public class SKYDETECTORS
			{
				// Token: 0x0400ADAF RID: 44463
				public static LocString NAME = UI.FormatAsLink("Celestial Detection", "SKYDETECTORS");

				// Token: 0x0400ADB0 RID: 44464
				public static LocString DESC = "Turn Duplicants' eyes to the skies and discover what undiscovered wonders await out there.";
			}

			// Token: 0x020028FD RID: 10493
			public class JETPACKS
			{
				// Token: 0x0400ADB1 RID: 44465
				public static LocString NAME = UI.FormatAsLink("Jetpacks", "JETPACKS");

				// Token: 0x0400ADB2 RID: 44466
				public static LocString DESC = "Objectively the most stylish way for Duplicants to get around.";
			}

			// Token: 0x020028FE RID: 10494
			public class BASICROCKETRY
			{
				// Token: 0x0400ADB3 RID: 44467
				public static LocString NAME = UI.FormatAsLink("Introductory Rocketry", "BASICROCKETRY");

				// Token: 0x0400ADB4 RID: 44468
				public static LocString DESC = "Everything required for launching the colony's very first space program.";
			}

			// Token: 0x020028FF RID: 10495
			public class ENGINESI
			{
				// Token: 0x0400ADB5 RID: 44469
				public static LocString NAME = UI.FormatAsLink("Solid Fuel Combustion", "ENGINESI");

				// Token: 0x0400ADB6 RID: 44470
				public static LocString DESC = "Rockets that fly further, longer.";
			}

			// Token: 0x02002900 RID: 10496
			public class ENGINESII
			{
				// Token: 0x0400ADB7 RID: 44471
				public static LocString NAME = UI.FormatAsLink("Hydrocarbon Combustion", "ENGINESII");

				// Token: 0x0400ADB8 RID: 44472
				public static LocString DESC = "Delve deeper into the vastness of space than ever before.";
			}

			// Token: 0x02002901 RID: 10497
			public class ENGINESIII
			{
				// Token: 0x0400ADB9 RID: 44473
				public static LocString NAME = UI.FormatAsLink("Cryofuel Combustion", "ENGINESIII");

				// Token: 0x0400ADBA RID: 44474
				public static LocString DESC = "With this technology, the sky is your oyster. Go exploring!";
			}

			// Token: 0x02002902 RID: 10498
			public class CRYOFUELPROPULSION
			{
				// Token: 0x0400ADBB RID: 44475
				public static LocString NAME = UI.FormatAsLink("Cryofuel Propulsion", "CRYOFUELPROPULSION");

				// Token: 0x0400ADBC RID: 44476
				public static LocString DESC = "A semi-powerful engine to propel you further into the galaxy.";
			}

			// Token: 0x02002903 RID: 10499
			public class NUCLEARPROPULSION
			{
				// Token: 0x0400ADBD RID: 44477
				public static LocString NAME = UI.FormatAsLink("Radbolt Propulsion", "NUCLEARPROPULSION");

				// Token: 0x0400ADBE RID: 44478
				public static LocString DESC = "Radical technology to get you to the stars.";
			}

			// Token: 0x02002904 RID: 10500
			public class ADVANCEDRESOURCEEXTRACTION
			{
				// Token: 0x0400ADBF RID: 44479
				public static LocString NAME = UI.FormatAsLink("Advanced Resource Extraction", "ADVANCEDRESOURCEEXTRACTION");

				// Token: 0x0400ADC0 RID: 44480
				public static LocString DESC = "Bring back souvieners from the stars.";
			}

			// Token: 0x02002905 RID: 10501
			public class CARGOI
			{
				// Token: 0x0400ADC1 RID: 44481
				public static LocString NAME = UI.FormatAsLink("Solid Cargo", "CARGOI");

				// Token: 0x0400ADC2 RID: 44482
				public static LocString DESC = "Make extra use of journeys into space by mining and storing useful resources.";
			}

			// Token: 0x02002906 RID: 10502
			public class CARGOII
			{
				// Token: 0x0400ADC3 RID: 44483
				public static LocString NAME = UI.FormatAsLink("Liquid and Gas Cargo", "CARGOII");

				// Token: 0x0400ADC4 RID: 44484
				public static LocString DESC = "Extract precious liquids and gases from the far reaches of space, and return with them to the colony.";
			}

			// Token: 0x02002907 RID: 10503
			public class CARGOIII
			{
				// Token: 0x0400ADC5 RID: 44485
				public static LocString NAME = UI.FormatAsLink("Unique Cargo", "CARGOIII");

				// Token: 0x0400ADC6 RID: 44486
				public static LocString DESC = "Allow Duplicants to take their friends to see the stars... or simply bring souvenirs back from their travels.";
			}

			// Token: 0x02002908 RID: 10504
			public class NOTIFICATIONSYSTEMS
			{
				// Token: 0x0400ADC7 RID: 44487
				public static LocString NAME = UI.FormatAsLink("Notification Systems", "NOTIFICATIONSYSTEMS");

				// Token: 0x0400ADC8 RID: 44488
				public static LocString DESC = "Get all the news you need to know about your complex colony.";
			}

			// Token: 0x02002909 RID: 10505
			public class NUCLEARREFINEMENT
			{
				// Token: 0x0400ADC9 RID: 44489
				public static LocString NAME = UI.FormatAsLink("Radiation Refinement", "NUCLEAR");

				// Token: 0x0400ADCA RID: 44490
				public static LocString DESC = "Refine uranium and generate radiation.";
			}

			// Token: 0x0200290A RID: 10506
			public class NUCLEARRESEARCH
			{
				// Token: 0x0400ADCB RID: 44491
				public static LocString NAME = UI.FormatAsLink("Materials Science Research", "ATOMIC");

				// Token: 0x0400ADCC RID: 44492
				public static LocString DESC = "Harness sub-atomic particles to study the properties of matter.";
			}

			// Token: 0x0200290B RID: 10507
			public class ADVANCEDNUCLEARRESEARCH
			{
				// Token: 0x0400ADCD RID: 44493
				public static LocString NAME = UI.FormatAsLink("More Materials Science Research", "ATOMIC");

				// Token: 0x0400ADCE RID: 44494
				public static LocString DESC = "Harness sub-atomic particles to study the properties of matter even more.";
			}

			// Token: 0x0200290C RID: 10508
			public class NUCLEARSTORAGE
			{
				// Token: 0x0400ADCF RID: 44495
				public static LocString NAME = UI.FormatAsLink("Radbolt Containment", "ATOMIC");

				// Token: 0x0400ADD0 RID: 44496
				public static LocString DESC = "Build a quality cache of radbolts.";
			}

			// Token: 0x0200290D RID: 10509
			public class SOLIDSPACE
			{
				// Token: 0x0400ADD1 RID: 44497
				public static LocString NAME = UI.FormatAsLink("Solid Control", "SOLIDSPACE");

				// Token: 0x0400ADD2 RID: 44498
				public static LocString DESC = "Transport and sort " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " resources.";
			}

			// Token: 0x0200290E RID: 10510
			public class HIGHVELOCITYTRANSPORT
			{
				// Token: 0x0400ADD3 RID: 44499
				public static LocString NAME = UI.FormatAsLink("High Velocity Transport", "HIGHVELOCITY");

				// Token: 0x0400ADD4 RID: 44500
				public static LocString DESC = "Hurl things through space.";
			}

			// Token: 0x0200290F RID: 10511
			public class MONUMENTS
			{
				// Token: 0x0400ADD5 RID: 44501
				public static LocString NAME = UI.FormatAsLink("Monuments", "MONUMENTS");

				// Token: 0x0400ADD6 RID: 44502
				public static LocString DESC = "Monumental art projects.";
			}

			// Token: 0x02002910 RID: 10512
			public class BIOENGINEERING
			{
				// Token: 0x0400ADD7 RID: 44503
				public static LocString NAME = UI.FormatAsLink("Bioengineering", "BIOENGINEERING");

				// Token: 0x0400ADD8 RID: 44504
				public static LocString DESC = "Mutation station.";
			}

			// Token: 0x02002911 RID: 10513
			public class SPACECOMBUSTION
			{
				// Token: 0x0400ADD9 RID: 44505
				public static LocString NAME = UI.FormatAsLink("Advanced Combustion", "SPACECOMBUSTION");

				// Token: 0x0400ADDA RID: 44506
				public static LocString DESC = "Sweet advancements in rocket engines.";
			}

			// Token: 0x02002912 RID: 10514
			public class HIGHVELOCITYDESTRUCTION
			{
				// Token: 0x0400ADDB RID: 44507
				public static LocString NAME = UI.FormatAsLink("High Velocity Destruction", "HIGHVELOCITYDESTRUCTION");

				// Token: 0x0400ADDC RID: 44508
				public static LocString DESC = "Mine the skies.";
			}

			// Token: 0x02002913 RID: 10515
			public class SPACEGAS
			{
				// Token: 0x0400ADDD RID: 44509
				public static LocString NAME = UI.FormatAsLink("Advanced Gas Flow", "SPACEGAS");

				// Token: 0x0400ADDE RID: 44510
				public static LocString DESC = UI.FormatAsLink("Gas", "ELEMENTS_GASSES") + " engines and transportation for rockets.";
			}
		}
	}
}
