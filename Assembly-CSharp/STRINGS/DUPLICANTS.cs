using System;
using TUNING;

namespace STRINGS
{
	// Token: 0x02000D44 RID: 3396
	public class DUPLICANTS
	{
		// Token: 0x04004E4A RID: 20042
		public static LocString RACE_PREFIX = "Species: {0}";

		// Token: 0x04004E4B RID: 20043
		public static LocString RACE = "Duplicant";

		// Token: 0x04004E4C RID: 20044
		public static LocString NAMETITLE = "Name: ";

		// Token: 0x04004E4D RID: 20045
		public static LocString GENDERTITLE = "Gender: ";

		// Token: 0x04004E4E RID: 20046
		public static LocString ARRIVALTIME = "Age: ";

		// Token: 0x04004E4F RID: 20047
		public static LocString ARRIVALTIME_TOOLTIP = "This {1} was printed on <b>Cycle {0}</b>";

		// Token: 0x04004E50 RID: 20048
		public static LocString DESC_TOOLTIP = "About {0}s";

		// Token: 0x02001CDE RID: 7390
		public class GENDER
		{
			// Token: 0x02002914 RID: 10516
			public class MALE
			{
				// Token: 0x0400ADDF RID: 44511
				public static LocString NAME = "M";

				// Token: 0x02002FC2 RID: 12226
				public class PLURALS
				{
					// Token: 0x0400BEFE RID: 48894
					public static LocString ONE = "he";

					// Token: 0x0400BEFF RID: 48895
					public static LocString TWO = "his";
				}
			}

			// Token: 0x02002915 RID: 10517
			public class FEMALE
			{
				// Token: 0x0400ADE0 RID: 44512
				public static LocString NAME = "F";

				// Token: 0x02002FC3 RID: 12227
				public class PLURALS
				{
					// Token: 0x0400BF00 RID: 48896
					public static LocString ONE = "she";

					// Token: 0x0400BF01 RID: 48897
					public static LocString TWO = "her";
				}
			}

			// Token: 0x02002916 RID: 10518
			public class NB
			{
				// Token: 0x0400ADE1 RID: 44513
				public static LocString NAME = "X";

				// Token: 0x02002FC4 RID: 12228
				public class PLURALS
				{
					// Token: 0x0400BF02 RID: 48898
					public static LocString ONE = "they";

					// Token: 0x0400BF03 RID: 48899
					public static LocString TWO = "their";
				}
			}
		}

		// Token: 0x02001CDF RID: 7391
		public class STATS
		{
			// Token: 0x02002917 RID: 10519
			public class SUBJECTS
			{
				// Token: 0x0400ADE2 RID: 44514
				public static LocString DUPLICANT = "Duplicant";

				// Token: 0x0400ADE3 RID: 44515
				public static LocString DUPLICANT_POSSESSIVE = "Duplicant's";

				// Token: 0x0400ADE4 RID: 44516
				public static LocString DUPLICANT_PLURAL = "Duplicants";

				// Token: 0x0400ADE5 RID: 44517
				public static LocString CREATURE = "critter";

				// Token: 0x0400ADE6 RID: 44518
				public static LocString CREATURE_POSSESSIVE = "critter's";

				// Token: 0x0400ADE7 RID: 44519
				public static LocString CREATURE_PLURAL = "critters";

				// Token: 0x0400ADE8 RID: 44520
				public static LocString PLANT = "plant";

				// Token: 0x0400ADE9 RID: 44521
				public static LocString PLANT_POSESSIVE = "plant's";

				// Token: 0x0400ADEA RID: 44522
				public static LocString PLANT_PLURAL = "plants";
			}

			// Token: 0x02002918 RID: 10520
			public class BREATH
			{
				// Token: 0x0400ADEB RID: 44523
				public static LocString NAME = "Breath";

				// Token: 0x0400ADEC RID: 44524
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A Duplicant with zero remaining ",
					UI.PRE_KEYWORD,
					"Breath",
					UI.PST_KEYWORD,
					" will begin suffocating"
				});
			}

			// Token: 0x02002919 RID: 10521
			public class STAMINA
			{
				// Token: 0x0400ADED RID: 44525
				public static LocString NAME = "Stamina";

				// Token: 0x0400ADEE RID: 44526
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Duplicants will pass out from fatigue when ",
					UI.PRE_KEYWORD,
					"Stamina",
					UI.PST_KEYWORD,
					" reaches zero"
				});
			}

			// Token: 0x0200291A RID: 10522
			public class CALORIES
			{
				// Token: 0x0400ADEF RID: 44527
				public static LocString NAME = "Calories";

				// Token: 0x0400ADF0 RID: 44528
				public static LocString TOOLTIP = "This {1} can burn <b>{0}</b> before starving";
			}

			// Token: 0x0200291B RID: 10523
			public class TEMPERATURE
			{
				// Token: 0x0400ADF1 RID: 44529
				public static LocString NAME = "Body Temperature";

				// Token: 0x0400ADF2 RID: 44530
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A healthy Duplicant's ",
					UI.PRE_KEYWORD,
					"Body Temperature",
					UI.PST_KEYWORD,
					" is <b>{1}</b>"
				});

				// Token: 0x0400ADF3 RID: 44531
				public static LocString TOOLTIP_DOMESTICATEDCRITTER = string.Concat(new string[]
				{
					"This critter's ",
					UI.PRE_KEYWORD,
					"Body Temperature",
					UI.PST_KEYWORD,
					" is <b>{1}</b>"
				});
			}

			// Token: 0x0200291C RID: 10524
			public class EXTERNALTEMPERATURE
			{
				// Token: 0x0400ADF4 RID: 44532
				public static LocString NAME = "External Temperature";

				// Token: 0x0400ADF5 RID: 44533
				public static LocString TOOLTIP = "This Duplicant's environment is <b>{0}</b>";
			}

			// Token: 0x0200291D RID: 10525
			public class DECOR
			{
				// Token: 0x0400ADF6 RID: 44534
				public static LocString NAME = "Decor";

				// Token: 0x0400ADF7 RID: 44535
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Duplicants become stressed in areas with ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" lower than their expectations\n\nOpen the ",
					UI.FormatAsOverlay("Decor Overlay", global::Action.Overlay8),
					" to view current ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" values"
				});

				// Token: 0x0400ADF8 RID: 44536
				public static LocString TOOLTIP_CURRENT = "\n\nCurrent Environmental Decor: <b>{0}</b>";

				// Token: 0x0400ADF9 RID: 44537
				public static LocString TOOLTIP_AVERAGE_TODAY = "\nAverage Decor This Cycle: <b>{0}</b>";

				// Token: 0x0400ADFA RID: 44538
				public static LocString TOOLTIP_AVERAGE_YESTERDAY = "\nAverage Decor Last Cycle: <b>{0}</b>";
			}

			// Token: 0x0200291E RID: 10526
			public class STRESS
			{
				// Token: 0x0400ADFB RID: 44539
				public static LocString NAME = "Stress";

				// Token: 0x0400ADFC RID: 44540
				public static LocString TOOLTIP = "Duplicants exhibit their Stress Reactions at one hundred percent " + UI.PRE_KEYWORD + "Stress" + UI.PST_KEYWORD;
			}

			// Token: 0x0200291F RID: 10527
			public class RADIATIONBALANCE
			{
				// Token: 0x0400ADFD RID: 44541
				public static LocString NAME = "Absorbed Rad Dose";

				// Token: 0x0400ADFE RID: 44542
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Duplicants accumulate Rads in areas with ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" and recover when using the toilet\n\nOpen the ",
					UI.FormatAsOverlay("Radiation Overlay", global::Action.Overlay15),
					" to view current ",
					UI.PRE_KEYWORD,
					"Rad",
					UI.PST_KEYWORD,
					" readings"
				});

				// Token: 0x0400ADFF RID: 44543
				public static LocString TOOLTIP_CURRENT_BALANCE = string.Concat(new string[]
				{
					"Duplicants accumulate Rads in areas with ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" and recover when using the toilet\n\nOpen the ",
					UI.FormatAsOverlay("Radiation Overlay", global::Action.Overlay15),
					" to view current ",
					UI.PRE_KEYWORD,
					"Rad",
					UI.PST_KEYWORD,
					" readings"
				});

				// Token: 0x0400AE00 RID: 44544
				public static LocString CURRENT_EXPOSURE = "Current Exposure: {0}/cycle";

				// Token: 0x0400AE01 RID: 44545
				public static LocString CURRENT_REJUVENATION = "Current Rejuvenation: {0}/cycle";
			}

			// Token: 0x02002920 RID: 10528
			public class BLADDER
			{
				// Token: 0x0400AE02 RID: 44546
				public static LocString NAME = "Bladder";

				// Token: 0x0400AE03 RID: 44547
				public static LocString TOOLTIP = "Duplicants make \"messes\" if no toilets are available at one hundred percent " + UI.PRE_KEYWORD + "Bladder" + UI.PST_KEYWORD;
			}

			// Token: 0x02002921 RID: 10529
			public class HITPOINTS
			{
				// Token: 0x0400AE04 RID: 44548
				public static LocString NAME = "Health";

				// Token: 0x0400AE05 RID: 44549
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"When Duplicants reach zero ",
					UI.PRE_KEYWORD,
					"Health",
					UI.PST_KEYWORD,
					" they become incapacitated and require rescuing\n\nWhen critters reach zero ",
					UI.PRE_KEYWORD,
					"Health",
					UI.PST_KEYWORD,
					", they will die immediately"
				});
			}

			// Token: 0x02002922 RID: 10530
			public class SKIN_THICKNESS
			{
				// Token: 0x0400AE06 RID: 44550
				public static LocString NAME = "Skin Thickness";
			}

			// Token: 0x02002923 RID: 10531
			public class SKIN_DURABILITY
			{
				// Token: 0x0400AE07 RID: 44551
				public static LocString NAME = "Skin Durability";
			}

			// Token: 0x02002924 RID: 10532
			public class DISEASERECOVERYTIME
			{
				// Token: 0x0400AE08 RID: 44552
				public static LocString NAME = "Disease Recovery";
			}

			// Token: 0x02002925 RID: 10533
			public class TRUNKHEALTH
			{
				// Token: 0x0400AE09 RID: 44553
				public static LocString NAME = "Trunk Health";

				// Token: 0x0400AE0A RID: 44554
				public static LocString TOOLTIP = "Tree branches will die if they do not have a healthy trunk to grow from";
			}
		}

		// Token: 0x02001CE0 RID: 7392
		public class DEATHS
		{
			// Token: 0x02002926 RID: 10534
			public class GENERIC
			{
				// Token: 0x0400AE0B RID: 44555
				public static LocString NAME = "Death";

				// Token: 0x0400AE0C RID: 44556
				public static LocString DESCRIPTION = "{Target} has died.";
			}

			// Token: 0x02002927 RID: 10535
			public class FROZEN
			{
				// Token: 0x0400AE0D RID: 44557
				public static LocString NAME = "Frozen";

				// Token: 0x0400AE0E RID: 44558
				public static LocString DESCRIPTION = "{Target} has frozen to death.";
			}

			// Token: 0x02002928 RID: 10536
			public class SUFFOCATION
			{
				// Token: 0x0400AE0F RID: 44559
				public static LocString NAME = "Suffocation";

				// Token: 0x0400AE10 RID: 44560
				public static LocString DESCRIPTION = "{Target} has suffocated to death.";
			}

			// Token: 0x02002929 RID: 10537
			public class STARVATION
			{
				// Token: 0x0400AE11 RID: 44561
				public static LocString NAME = "Starvation";

				// Token: 0x0400AE12 RID: 44562
				public static LocString DESCRIPTION = "{Target} has starved to death.";
			}

			// Token: 0x0200292A RID: 10538
			public class OVERHEATING
			{
				// Token: 0x0400AE13 RID: 44563
				public static LocString NAME = "Overheated";

				// Token: 0x0400AE14 RID: 44564
				public static LocString DESCRIPTION = "{Target} overheated to death.";
			}

			// Token: 0x0200292B RID: 10539
			public class DROWNED
			{
				// Token: 0x0400AE15 RID: 44565
				public static LocString NAME = "Drowned";

				// Token: 0x0400AE16 RID: 44566
				public static LocString DESCRIPTION = "{Target} has drowned.";
			}

			// Token: 0x0200292C RID: 10540
			public class EXPLOSION
			{
				// Token: 0x0400AE17 RID: 44567
				public static LocString NAME = "Explosion";

				// Token: 0x0400AE18 RID: 44568
				public static LocString DESCRIPTION = "{Target} has died in an explosion.";
			}

			// Token: 0x0200292D RID: 10541
			public class COMBAT
			{
				// Token: 0x0400AE19 RID: 44569
				public static LocString NAME = "Slain";

				// Token: 0x0400AE1A RID: 44570
				public static LocString DESCRIPTION = "{Target} succumbed to their wounds after being incapacitated.";
			}

			// Token: 0x0200292E RID: 10542
			public class FATALDISEASE
			{
				// Token: 0x0400AE1B RID: 44571
				public static LocString NAME = "Succumbed to Disease";

				// Token: 0x0400AE1C RID: 44572
				public static LocString DESCRIPTION = "{Target} has died of a fatal illness.";
			}

			// Token: 0x0200292F RID: 10543
			public class RADIATION
			{
				// Token: 0x0400AE1D RID: 44573
				public static LocString NAME = "Irradiated";

				// Token: 0x0400AE1E RID: 44574
				public static LocString DESCRIPTION = "{Target} perished from excessive radiation exposure.";
			}

			// Token: 0x02002930 RID: 10544
			public class HITBYHIGHENERGYPARTICLE
			{
				// Token: 0x0400AE1F RID: 44575
				public static LocString NAME = "Struck by Radbolt";

				// Token: 0x0400AE20 RID: 44576
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"{Target} was struck by a radioactive ",
					UI.PRE_KEYWORD,
					"Radbolt",
					UI.PST_KEYWORD,
					" and perished."
				});
			}
		}

		// Token: 0x02001CE1 RID: 7393
		public class CHORES
		{
			// Token: 0x04008425 RID: 33829
			public static LocString NOT_EXISTING_TASK = "Not Existing";

			// Token: 0x04008426 RID: 33830
			public static LocString IS_DEAD_TASK = "Dead";

			// Token: 0x02002931 RID: 10545
			public class THINKING
			{
				// Token: 0x0400AE21 RID: 44577
				public static LocString NAME = "Ponder";

				// Token: 0x0400AE22 RID: 44578
				public static LocString STATUS = "Pondering";

				// Token: 0x0400AE23 RID: 44579
				public static LocString TOOLTIP = "This Duplicant is mulling over what they should do next";
			}

			// Token: 0x02002932 RID: 10546
			public class ASTRONAUT
			{
				// Token: 0x0400AE24 RID: 44580
				public static LocString NAME = "Space Mission";

				// Token: 0x0400AE25 RID: 44581
				public static LocString STATUS = "On space mission";

				// Token: 0x0400AE26 RID: 44582
				public static LocString TOOLTIP = "This Duplicant is exploring the vast universe";
			}

			// Token: 0x02002933 RID: 10547
			public class DIE
			{
				// Token: 0x0400AE27 RID: 44583
				public static LocString NAME = "Die";

				// Token: 0x0400AE28 RID: 44584
				public static LocString STATUS = "Dying";

				// Token: 0x0400AE29 RID: 44585
				public static LocString TOOLTIP = "Fare thee well, brave soul";
			}

			// Token: 0x02002934 RID: 10548
			public class ENTOMBED
			{
				// Token: 0x0400AE2A RID: 44586
				public static LocString NAME = "Entombed";

				// Token: 0x0400AE2B RID: 44587
				public static LocString STATUS = "Entombed";

				// Token: 0x0400AE2C RID: 44588
				public static LocString TOOLTIP = "Entombed Duplicants are at risk of suffocating and must be dug out by others in the colony";
			}

			// Token: 0x02002935 RID: 10549
			public class BEINCAPACITATED
			{
				// Token: 0x0400AE2D RID: 44589
				public static LocString NAME = "Incapacitated";

				// Token: 0x0400AE2E RID: 44590
				public static LocString STATUS = "Dying";

				// Token: 0x0400AE2F RID: 44591
				public static LocString TOOLTIP = "This Duplicant will die soon if they do not receive assistance";
			}

			// Token: 0x02002936 RID: 10550
			public class GENESHUFFLE
			{
				// Token: 0x0400AE30 RID: 44592
				public static LocString NAME = "Use Neural Vacillator";

				// Token: 0x0400AE31 RID: 44593
				public static LocString STATUS = "Using Neural Vacillator";

				// Token: 0x0400AE32 RID: 44594
				public static LocString TOOLTIP = "This Duplicant is being experimented on!";
			}

			// Token: 0x02002937 RID: 10551
			public class MIGRATE
			{
				// Token: 0x0400AE33 RID: 44595
				public static LocString NAME = "Use Teleporter";

				// Token: 0x0400AE34 RID: 44596
				public static LocString STATUS = "Using Teleporter";

				// Token: 0x0400AE35 RID: 44597
				public static LocString TOOLTIP = "This Duplicant's molecules are hurtling through the air!";
			}

			// Token: 0x02002938 RID: 10552
			public class DEBUGGOTO
			{
				// Token: 0x0400AE36 RID: 44598
				public static LocString NAME = "DebugGoTo";

				// Token: 0x0400AE37 RID: 44599
				public static LocString STATUS = "DebugGoTo";
			}

			// Token: 0x02002939 RID: 10553
			public class DISINFECT
			{
				// Token: 0x0400AE38 RID: 44600
				public static LocString NAME = "Disinfect";

				// Token: 0x0400AE39 RID: 44601
				public static LocString STATUS = "Going to disinfect";

				// Token: 0x0400AE3A RID: 44602
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Buildings can be disinfected to remove contagious ",
					UI.PRE_KEYWORD,
					"Germs",
					UI.PST_KEYWORD,
					" from their surface"
				});
			}

			// Token: 0x0200293A RID: 10554
			public class EQUIPPINGSUIT
			{
				// Token: 0x0400AE3B RID: 44603
				public static LocString NAME = "Equip Exosuit";

				// Token: 0x0400AE3C RID: 44604
				public static LocString STATUS = "Equipping exosuit";

				// Token: 0x0400AE3D RID: 44605
				public static LocString TOOLTIP = "This Duplicant is putting on protective gear";
			}

			// Token: 0x0200293B RID: 10555
			public class STRESSIDLE
			{
				// Token: 0x0400AE3E RID: 44606
				public static LocString NAME = "Antsy";

				// Token: 0x0400AE3F RID: 44607
				public static LocString STATUS = "Antsy";

				// Token: 0x0400AE40 RID: 44608
				public static LocString TOOLTIP = "This Duplicant is a workaholic and gets stressed when they have nothing to do";
			}

			// Token: 0x0200293C RID: 10556
			public class MOVETO
			{
				// Token: 0x0400AE41 RID: 44609
				public static LocString NAME = "Move to";

				// Token: 0x0400AE42 RID: 44610
				public static LocString STATUS = "Moving to location";

				// Token: 0x0400AE43 RID: 44611
				public static LocString TOOLTIP = "This Duplicant was manually directed to move to a specific location";
			}

			// Token: 0x0200293D RID: 10557
			public class ROCKETENTEREXIT
			{
				// Token: 0x0400AE44 RID: 44612
				public static LocString NAME = "Rocket Recrewing";

				// Token: 0x0400AE45 RID: 44613
				public static LocString STATUS = "Recrewing Rocket";

				// Token: 0x0400AE46 RID: 44614
				public static LocString TOOLTIP = "This Duplicant is getting into (or out of) their assigned rocket";
			}

			// Token: 0x0200293E RID: 10558
			public class DROPUNUSEDINVENTORY
			{
				// Token: 0x0400AE47 RID: 44615
				public static LocString NAME = "Drop Inventory";

				// Token: 0x0400AE48 RID: 44616
				public static LocString STATUS = "Dropping unused inventory";

				// Token: 0x0400AE49 RID: 44617
				public static LocString TOOLTIP = "This Duplicant is dropping carried items they no longer need";
			}

			// Token: 0x0200293F RID: 10559
			public class PEE
			{
				// Token: 0x0400AE4A RID: 44618
				public static LocString NAME = "Relieve Self";

				// Token: 0x0400AE4B RID: 44619
				public static LocString STATUS = "Relieving self";

				// Token: 0x0400AE4C RID: 44620
				public static LocString TOOLTIP = "This Duplicant didn't find a toilet in time. Oops";
			}

			// Token: 0x02002940 RID: 10560
			public class BREAK_PEE
			{
				// Token: 0x0400AE4D RID: 44621
				public static LocString NAME = "Downtime: Use Toilet";

				// Token: 0x0400AE4E RID: 44622
				public static LocString STATUS = "Downtime: Going to use toilet";

				// Token: 0x0400AE4F RID: 44623
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant has scheduled ",
					UI.PRE_KEYWORD,
					"Downtime",
					UI.PST_KEYWORD,
					" and is using their break to go to the toilet\n\nDuplicants have to use the toilet at least once per day"
				});
			}

			// Token: 0x02002941 RID: 10561
			public class STRESSVOMIT
			{
				// Token: 0x0400AE50 RID: 44624
				public static LocString NAME = "Stress Vomit";

				// Token: 0x0400AE51 RID: 44625
				public static LocString STATUS = "Stress vomiting";

				// Token: 0x0400AE52 RID: 44626
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Some people deal with ",
					UI.PRE_KEYWORD,
					"Stress",
					UI.PST_KEYWORD,
					" better than others"
				});
			}

			// Token: 0x02002942 RID: 10562
			public class UGLY_CRY
			{
				// Token: 0x0400AE53 RID: 44627
				public static LocString NAME = "Ugly Cry";

				// Token: 0x0400AE54 RID: 44628
				public static LocString STATUS = "Ugly crying";

				// Token: 0x0400AE55 RID: 44629
				public static LocString TOOLTIP = "This Duplicant is having a healthy cry to alleviate their " + UI.PRE_KEYWORD + "Stress" + UI.PST_KEYWORD;
			}

			// Token: 0x02002943 RID: 10563
			public class BINGE_EAT
			{
				// Token: 0x0400AE56 RID: 44630
				public static LocString NAME = "Binge Eat";

				// Token: 0x0400AE57 RID: 44631
				public static LocString STATUS = "Binge eating";

				// Token: 0x0400AE58 RID: 44632
				public static LocString TOOLTIP = "This Duplicant is attempting to eat their emotions due to " + UI.PRE_KEYWORD + "Stress" + UI.PST_KEYWORD;
			}

			// Token: 0x02002944 RID: 10564
			public class BANSHEE_WAIL
			{
				// Token: 0x0400AE59 RID: 44633
				public static LocString NAME = "Banshee Wail";

				// Token: 0x0400AE5A RID: 44634
				public static LocString STATUS = "Wailing";

				// Token: 0x0400AE5B RID: 44635
				public static LocString TOOLTIP = "This Duplicant is emitting ear-piercing shrieks to relieve pent-up " + UI.PRE_KEYWORD + "Stress" + UI.PST_KEYWORD;
			}

			// Token: 0x02002945 RID: 10565
			public class EMOTEHIGHPRIORITY
			{
				// Token: 0x0400AE5C RID: 44636
				public static LocString NAME = "Express Themselves";

				// Token: 0x0400AE5D RID: 44637
				public static LocString STATUS = "Expressing themselves";

				// Token: 0x0400AE5E RID: 44638
				public static LocString TOOLTIP = "This Duplicant needs a moment to express their feelings, then they'll be on their way";
			}

			// Token: 0x02002946 RID: 10566
			public class HUG
			{
				// Token: 0x0400AE5F RID: 44639
				public static LocString NAME = "Hug";

				// Token: 0x0400AE60 RID: 44640
				public static LocString STATUS = "Hugging";

				// Token: 0x0400AE61 RID: 44641
				public static LocString TOOLTIP = "This Duplicant is enjoying a big warm hug";
			}

			// Token: 0x02002947 RID: 10567
			public class FLEE
			{
				// Token: 0x0400AE62 RID: 44642
				public static LocString NAME = "Flee";

				// Token: 0x0400AE63 RID: 44643
				public static LocString STATUS = "Fleeing";

				// Token: 0x0400AE64 RID: 44644
				public static LocString TOOLTIP = "Run away!";
			}

			// Token: 0x02002948 RID: 10568
			public class RECOVERBREATH
			{
				// Token: 0x0400AE65 RID: 44645
				public static LocString NAME = "Recover Breath";

				// Token: 0x0400AE66 RID: 44646
				public static LocString STATUS = "Recovering breath";

				// Token: 0x0400AE67 RID: 44647
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002949 RID: 10569
			public class MOVETOQUARANTINE
			{
				// Token: 0x0400AE68 RID: 44648
				public static LocString NAME = "Move to Quarantine";

				// Token: 0x0400AE69 RID: 44649
				public static LocString STATUS = "Moving to quarantine";

				// Token: 0x0400AE6A RID: 44650
				public static LocString TOOLTIP = "This Duplicant will isolate themselves to keep their illness away from the colony";
			}

			// Token: 0x0200294A RID: 10570
			public class ATTACK
			{
				// Token: 0x0400AE6B RID: 44651
				public static LocString NAME = "Attack";

				// Token: 0x0400AE6C RID: 44652
				public static LocString STATUS = "Attacking";

				// Token: 0x0400AE6D RID: 44653
				public static LocString TOOLTIP = "Chaaaarge!";
			}

			// Token: 0x0200294B RID: 10571
			public class CAPTURE
			{
				// Token: 0x0400AE6E RID: 44654
				public static LocString NAME = "Wrangle";

				// Token: 0x0400AE6F RID: 44655
				public static LocString STATUS = "Wrangling";

				// Token: 0x0400AE70 RID: 44656
				public static LocString TOOLTIP = "Duplicants that possess the Critter Ranching Skill can wrangle most critters without traps";
			}

			// Token: 0x0200294C RID: 10572
			public class SINGTOEGG
			{
				// Token: 0x0400AE71 RID: 44657
				public static LocString NAME = "Sing To Egg";

				// Token: 0x0400AE72 RID: 44658
				public static LocString STATUS = "Singing to egg";

				// Token: 0x0400AE73 RID: 44659
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A gentle lullaby from a supportive Duplicant encourages developing ",
					UI.PRE_KEYWORD,
					"Eggs",
					UI.PST_KEYWORD,
					"\n\nIncreases ",
					UI.PRE_KEYWORD,
					"Incubation Rate",
					UI.PST_KEYWORD,
					"\n\nDuplicants must possess the ",
					DUPLICANTS.ROLES.RANCHER.NAME,
					" Skill to sing to an egg"
				});
			}

			// Token: 0x0200294D RID: 10573
			public class USETOILET
			{
				// Token: 0x0400AE74 RID: 44660
				public static LocString NAME = "Use Toilet";

				// Token: 0x0400AE75 RID: 44661
				public static LocString STATUS = "Going to use toilet";

				// Token: 0x0400AE76 RID: 44662
				public static LocString TOOLTIP = "Duplicants have to use the toilet at least once per day";
			}

			// Token: 0x0200294E RID: 10574
			public class WASHHANDS
			{
				// Token: 0x0400AE77 RID: 44663
				public static LocString NAME = "Wash Hands";

				// Token: 0x0400AE78 RID: 44664
				public static LocString STATUS = "Washing hands";

				// Token: 0x0400AE79 RID: 44665
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Good hygiene removes ",
					UI.PRE_KEYWORD,
					"Germs",
					UI.PST_KEYWORD,
					" and prevents the spread of ",
					UI.PRE_KEYWORD,
					"Disease",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x0200294F RID: 10575
			public class CHECKPOINT
			{
				// Token: 0x0400AE7A RID: 44666
				public static LocString NAME = "Wait at Checkpoint";

				// Token: 0x0400AE7B RID: 44667
				public static LocString STATUS = "Waiting at Checkpoint";

				// Token: 0x0400AE7C RID: 44668
				public static LocString TOOLTIP = "This Duplicant is waiting for permission to pass";
			}

			// Token: 0x02002950 RID: 10576
			public class TRAVELTUBEENTRANCE
			{
				// Token: 0x0400AE7D RID: 44669
				public static LocString NAME = "Enter Transit Tube";

				// Token: 0x0400AE7E RID: 44670
				public static LocString STATUS = "Entering Transit Tube";

				// Token: 0x0400AE7F RID: 44671
				public static LocString TOOLTIP = "Nyoooom!";
			}

			// Token: 0x02002951 RID: 10577
			public class SCRUBORE
			{
				// Token: 0x0400AE80 RID: 44672
				public static LocString NAME = "Scrub Ore";

				// Token: 0x0400AE81 RID: 44673
				public static LocString STATUS = "Scrubbing ore";

				// Token: 0x0400AE82 RID: 44674
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Material ore can be scrubbed to remove ",
					UI.PRE_KEYWORD,
					"Germs",
					UI.PST_KEYWORD,
					" present on its surface"
				});
			}

			// Token: 0x02002952 RID: 10578
			public class EAT
			{
				// Token: 0x0400AE83 RID: 44675
				public static LocString NAME = "Eat";

				// Token: 0x0400AE84 RID: 44676
				public static LocString STATUS = "Going to eat";

				// Token: 0x0400AE85 RID: 44677
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Duplicants eat to replenish their ",
					UI.PRE_KEYWORD,
					"Calorie",
					UI.PST_KEYWORD,
					" stores"
				});
			}

			// Token: 0x02002953 RID: 10579
			public class VOMIT
			{
				// Token: 0x0400AE86 RID: 44678
				public static LocString NAME = "Vomit";

				// Token: 0x0400AE87 RID: 44679
				public static LocString STATUS = "Vomiting";

				// Token: 0x0400AE88 RID: 44680
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Vomiting produces ",
					ELEMENTS.DIRTYWATER.NAME,
					" and can spread ",
					UI.PRE_KEYWORD,
					"Disease",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002954 RID: 10580
			public class RADIATIONPAIN
			{
				// Token: 0x0400AE89 RID: 44681
				public static LocString NAME = "Radiation Aches";

				// Token: 0x0400AE8A RID: 44682
				public static LocString STATUS = "Feeling radiation aches";

				// Token: 0x0400AE8B RID: 44683
				public static LocString TOOLTIP = "Radiation Aches are a symptom of " + DUPLICANTS.DISEASES.RADIATIONSICKNESS.NAME;
			}

			// Token: 0x02002955 RID: 10581
			public class COUGH
			{
				// Token: 0x0400AE8C RID: 44684
				public static LocString NAME = "Cough";

				// Token: 0x0400AE8D RID: 44685
				public static LocString STATUS = "Coughing";

				// Token: 0x0400AE8E RID: 44686
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Coughing is a symptom of ",
					DUPLICANTS.DISEASES.SLIMESICKNESS.NAME,
					" and spreads airborne ",
					UI.PRE_KEYWORD,
					"Germs",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002956 RID: 10582
			public class SLEEP
			{
				// Token: 0x0400AE8F RID: 44687
				public static LocString NAME = "Sleep";

				// Token: 0x0400AE90 RID: 44688
				public static LocString STATUS = "Sleeping";

				// Token: 0x0400AE91 RID: 44689
				public static LocString TOOLTIP = "Zzzzzz...";
			}

			// Token: 0x02002957 RID: 10583
			public class NARCOLEPSY
			{
				// Token: 0x0400AE92 RID: 44690
				public static LocString NAME = "Narcoleptic Nap";

				// Token: 0x0400AE93 RID: 44691
				public static LocString STATUS = "Narcoleptic napping";

				// Token: 0x0400AE94 RID: 44692
				public static LocString TOOLTIP = "Zzzzzz...";
			}

			// Token: 0x02002958 RID: 10584
			public class FLOORSLEEP
			{
				// Token: 0x0400AE95 RID: 44693
				public static LocString NAME = "Sleep on Floor";

				// Token: 0x0400AE96 RID: 44694
				public static LocString STATUS = "Sleeping on floor";

				// Token: 0x0400AE97 RID: 44695
				public static LocString TOOLTIP = "Zzzzzz...\n\nSleeping on the floor will give Duplicants a " + DUPLICANTS.MODIFIERS.SOREBACK.NAME;
			}

			// Token: 0x02002959 RID: 10585
			public class TAKEMEDICINE
			{
				// Token: 0x0400AE98 RID: 44696
				public static LocString NAME = "Take Medicine";

				// Token: 0x0400AE99 RID: 44697
				public static LocString STATUS = "Taking medicine";

				// Token: 0x0400AE9A RID: 44698
				public static LocString TOOLTIP = "This Duplicant is taking a dose of medicine to ward off " + UI.PRE_KEYWORD + "Disease" + UI.PST_KEYWORD;
			}

			// Token: 0x0200295A RID: 10586
			public class GETDOCTORED
			{
				// Token: 0x0400AE9B RID: 44699
				public static LocString NAME = "Visit Doctor";

				// Token: 0x0400AE9C RID: 44700
				public static LocString STATUS = "Visiting doctor";

				// Token: 0x0400AE9D RID: 44701
				public static LocString TOOLTIP = "This Duplicant is visiting a doctor to receive treatment";
			}

			// Token: 0x0200295B RID: 10587
			public class DOCTOR
			{
				// Token: 0x0400AE9E RID: 44702
				public static LocString NAME = "Treat Patient";

				// Token: 0x0400AE9F RID: 44703
				public static LocString STATUS = "Treating patient";

				// Token: 0x0400AEA0 RID: 44704
				public static LocString TOOLTIP = "This Duplicant is trying to make one of their peers feel better";
			}

			// Token: 0x0200295C RID: 10588
			public class DELIVERFOOD
			{
				// Token: 0x0400AEA1 RID: 44705
				public static LocString NAME = "Deliver Food";

				// Token: 0x0400AEA2 RID: 44706
				public static LocString STATUS = "Delivering food";

				// Token: 0x0400AEA3 RID: 44707
				public static LocString TOOLTIP = "Under thirty minutes or it's free";
			}

			// Token: 0x0200295D RID: 10589
			public class SHOWER
			{
				// Token: 0x0400AEA4 RID: 44708
				public static LocString NAME = "Shower";

				// Token: 0x0400AEA5 RID: 44709
				public static LocString STATUS = "Showering";

				// Token: 0x0400AEA6 RID: 44710
				public static LocString TOOLTIP = "This Duplicant is having a refreshing shower";
			}

			// Token: 0x0200295E RID: 10590
			public class SIGH
			{
				// Token: 0x0400AEA7 RID: 44711
				public static LocString NAME = "Sigh";

				// Token: 0x0400AEA8 RID: 44712
				public static LocString STATUS = "Sighing";

				// Token: 0x0400AEA9 RID: 44713
				public static LocString TOOLTIP = "Ho-hum.";
			}

			// Token: 0x0200295F RID: 10591
			public class RESTDUETODISEASE
			{
				// Token: 0x0400AEAA RID: 44714
				public static LocString NAME = "Rest";

				// Token: 0x0400AEAB RID: 44715
				public static LocString STATUS = "Resting";

				// Token: 0x0400AEAC RID: 44716
				public static LocString TOOLTIP = "This Duplicant isn't feeling well and is taking a rest";
			}

			// Token: 0x02002960 RID: 10592
			public class HEAL
			{
				// Token: 0x0400AEAD RID: 44717
				public static LocString NAME = "Heal";

				// Token: 0x0400AEAE RID: 44718
				public static LocString STATUS = "Healing";

				// Token: 0x0400AEAF RID: 44719
				public static LocString TOOLTIP = "This Duplicant is taking some time to recover from their wounds";
			}

			// Token: 0x02002961 RID: 10593
			public class STRESSACTINGOUT
			{
				// Token: 0x0400AEB0 RID: 44720
				public static LocString NAME = "Lash Out";

				// Token: 0x0400AEB1 RID: 44721
				public static LocString STATUS = "Lashing out";

				// Token: 0x0400AEB2 RID: 44722
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is having a ",
					UI.PRE_KEYWORD,
					"Stress",
					UI.PST_KEYWORD,
					"-induced tantrum"
				});
			}

			// Token: 0x02002962 RID: 10594
			public class RELAX
			{
				// Token: 0x0400AEB3 RID: 44723
				public static LocString NAME = "Relax";

				// Token: 0x0400AEB4 RID: 44724
				public static LocString STATUS = "Relaxing";

				// Token: 0x0400AEB5 RID: 44725
				public static LocString TOOLTIP = "This Duplicant is taking it easy";
			}

			// Token: 0x02002963 RID: 10595
			public class STRESSHEAL
			{
				// Token: 0x0400AEB6 RID: 44726
				public static LocString NAME = "De-Stress";

				// Token: 0x0400AEB7 RID: 44727
				public static LocString STATUS = "De-stressing";

				// Token: 0x0400AEB8 RID: 44728
				public static LocString TOOLTIP = "This Duplicant taking some time to recover from their " + UI.PRE_KEYWORD + "Stress" + UI.PST_KEYWORD;
			}

			// Token: 0x02002964 RID: 10596
			public class EQUIP
			{
				// Token: 0x0400AEB9 RID: 44729
				public static LocString NAME = "Equip";

				// Token: 0x0400AEBA RID: 44730
				public static LocString STATUS = "Moving to equip";

				// Token: 0x0400AEBB RID: 44731
				public static LocString TOOLTIP = "This Duplicant is putting on a piece of equipment";
			}

			// Token: 0x02002965 RID: 10597
			public class LEARNSKILL
			{
				// Token: 0x0400AEBC RID: 44732
				public static LocString NAME = "Learn Skill";

				// Token: 0x0400AEBD RID: 44733
				public static LocString STATUS = "Learning skill";

				// Token: 0x0400AEBE RID: 44734
				public static LocString TOOLTIP = "This Duplicant is learning a new " + UI.PRE_KEYWORD + "Skill" + UI.PST_KEYWORD;
			}

			// Token: 0x02002966 RID: 10598
			public class UNLEARNSKILL
			{
				// Token: 0x0400AEBF RID: 44735
				public static LocString NAME = "Unlearn Skills";

				// Token: 0x0400AEC0 RID: 44736
				public static LocString STATUS = "Unlearning skills";

				// Token: 0x0400AEC1 RID: 44737
				public static LocString TOOLTIP = "This Duplicant is unlearning " + UI.PRE_KEYWORD + "Skills" + UI.PST_KEYWORD;
			}

			// Token: 0x02002967 RID: 10599
			public class RECHARGE
			{
				// Token: 0x0400AEC2 RID: 44738
				public static LocString NAME = "Recharge Equipment";

				// Token: 0x0400AEC3 RID: 44739
				public static LocString STATUS = "Recharging equipment";

				// Token: 0x0400AEC4 RID: 44740
				public static LocString TOOLTIP = "This Duplicant is recharging their equipment";
			}

			// Token: 0x02002968 RID: 10600
			public class UNEQUIP
			{
				// Token: 0x0400AEC5 RID: 44741
				public static LocString NAME = "Unequip";

				// Token: 0x0400AEC6 RID: 44742
				public static LocString STATUS = "Moving to unequip";

				// Token: 0x0400AEC7 RID: 44743
				public static LocString TOOLTIP = "This Duplicant is removing a piece of their equipment";
			}

			// Token: 0x02002969 RID: 10601
			public class MOURN
			{
				// Token: 0x0400AEC8 RID: 44744
				public static LocString NAME = "Mourn";

				// Token: 0x0400AEC9 RID: 44745
				public static LocString STATUS = "Mourning";

				// Token: 0x0400AECA RID: 44746
				public static LocString TOOLTIP = "This Duplicant is mourning the loss of a friend";
			}

			// Token: 0x0200296A RID: 10602
			public class WARMUP
			{
				// Token: 0x0400AECB RID: 44747
				public static LocString NAME = "Warm Up";

				// Token: 0x0400AECC RID: 44748
				public static LocString STATUS = "Going to warm up";

				// Token: 0x0400AECD RID: 44749
				public static LocString TOOLTIP = "This Duplicant got too cold and is going somewhere to warm up";
			}

			// Token: 0x0200296B RID: 10603
			public class COOLDOWN
			{
				// Token: 0x0400AECE RID: 44750
				public static LocString NAME = "Cool Off";

				// Token: 0x0400AECF RID: 44751
				public static LocString STATUS = "Going to cool off";

				// Token: 0x0400AED0 RID: 44752
				public static LocString TOOLTIP = "This Duplicant got too hot and is going somewhere to cool off";
			}

			// Token: 0x0200296C RID: 10604
			public class EMPTYSTORAGE
			{
				// Token: 0x0400AED1 RID: 44753
				public static LocString NAME = "Empty Storage";

				// Token: 0x0400AED2 RID: 44754
				public static LocString STATUS = "Going to empty storage";

				// Token: 0x0400AED3 RID: 44755
				public static LocString TOOLTIP = "This Duplicant is taking items out of storage";
			}

			// Token: 0x0200296D RID: 10605
			public class ART
			{
				// Token: 0x0400AED4 RID: 44756
				public static LocString NAME = "Decorate";

				// Token: 0x0400AED5 RID: 44757
				public static LocString STATUS = "Going to decorate";

				// Token: 0x0400AED6 RID: 44758
				public static LocString TOOLTIP = "This Duplicant is going to work on their art";
			}

			// Token: 0x0200296E RID: 10606
			public class MOP
			{
				// Token: 0x0400AED7 RID: 44759
				public static LocString NAME = "Mop";

				// Token: 0x0400AED8 RID: 44760
				public static LocString STATUS = "Going to mop";

				// Token: 0x0400AED9 RID: 44761
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Mopping removes ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" from the floor and bottles them for transport"
				});
			}

			// Token: 0x0200296F RID: 10607
			public class RELOCATE
			{
				// Token: 0x0400AEDA RID: 44762
				public static LocString NAME = "Relocate";

				// Token: 0x0400AEDB RID: 44763
				public static LocString STATUS = "Going to relocate";

				// Token: 0x0400AEDC RID: 44764
				public static LocString TOOLTIP = "This Duplicant is moving a building to a new location";
			}

			// Token: 0x02002970 RID: 10608
			public class TOGGLE
			{
				// Token: 0x0400AEDD RID: 44765
				public static LocString NAME = "Change Setting";

				// Token: 0x0400AEDE RID: 44766
				public static LocString STATUS = "Going to change setting";

				// Token: 0x0400AEDF RID: 44767
				public static LocString TOOLTIP = "This Duplicant is going to change the settings on a building";
			}

			// Token: 0x02002971 RID: 10609
			public class RESCUEINCAPACITATED
			{
				// Token: 0x0400AEE0 RID: 44768
				public static LocString NAME = "Rescue Friend";

				// Token: 0x0400AEE1 RID: 44769
				public static LocString STATUS = "Rescuing friend";

				// Token: 0x0400AEE2 RID: 44770
				public static LocString TOOLTIP = "This Duplicant is rescuing another Duplicant that has been incapacitated";
			}

			// Token: 0x02002972 RID: 10610
			public class REPAIR
			{
				// Token: 0x0400AEE3 RID: 44771
				public static LocString NAME = "Repair";

				// Token: 0x0400AEE4 RID: 44772
				public static LocString STATUS = "Going to repair";

				// Token: 0x0400AEE5 RID: 44773
				public static LocString TOOLTIP = "This Duplicant is fixing a broken building";
			}

			// Token: 0x02002973 RID: 10611
			public class DECONSTRUCT
			{
				// Token: 0x0400AEE6 RID: 44774
				public static LocString NAME = "Deconstruct";

				// Token: 0x0400AEE7 RID: 44775
				public static LocString STATUS = "Going to deconstruct";

				// Token: 0x0400AEE8 RID: 44776
				public static LocString TOOLTIP = "This Duplicant is deconstructing a building";
			}

			// Token: 0x02002974 RID: 10612
			public class RESEARCH
			{
				// Token: 0x0400AEE9 RID: 44777
				public static LocString NAME = "Research";

				// Token: 0x0400AEEA RID: 44778
				public static LocString STATUS = "Going to research";

				// Token: 0x0400AEEB RID: 44779
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is working on the current ",
					UI.PRE_KEYWORD,
					"Research",
					UI.PST_KEYWORD,
					" focus"
				});
			}

			// Token: 0x02002975 RID: 10613
			public class ANALYZEARTIFACT
			{
				// Token: 0x0400AEEC RID: 44780
				public static LocString NAME = "Artifact Analysis";

				// Token: 0x0400AEED RID: 44781
				public static LocString STATUS = "Going to analyze artifacts";

				// Token: 0x0400AEEE RID: 44782
				public static LocString TOOLTIP = "This Duplicant is analyzing " + UI.PRE_KEYWORD + "Artifacts" + UI.PST_KEYWORD;
			}

			// Token: 0x02002976 RID: 10614
			public class ANALYZESEED
			{
				// Token: 0x0400AEEF RID: 44783
				public static LocString NAME = "Seed Analysis";

				// Token: 0x0400AEF0 RID: 44784
				public static LocString STATUS = "Going to analyze seeds";

				// Token: 0x0400AEF1 RID: 44785
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is analyzing ",
					UI.PRE_KEYWORD,
					"Seeds",
					UI.PST_KEYWORD,
					" to find mutations"
				});
			}

			// Token: 0x02002977 RID: 10615
			public class RETURNSUIT
			{
				// Token: 0x0400AEF2 RID: 44786
				public static LocString NAME = "Dock Exosuit";

				// Token: 0x0400AEF3 RID: 44787
				public static LocString STATUS = "Docking exosuit";

				// Token: 0x0400AEF4 RID: 44788
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is plugging an ",
					UI.PRE_KEYWORD,
					"Exosuit",
					UI.PST_KEYWORD,
					" in for refilling"
				});
			}

			// Token: 0x02002978 RID: 10616
			public class GENERATEPOWER
			{
				// Token: 0x0400AEF5 RID: 44789
				public static LocString NAME = "Generate Power";

				// Token: 0x0400AEF6 RID: 44790
				public static LocString STATUS = "Going to generate power";

				// Token: 0x0400AEF7 RID: 44791
				public static LocString TOOLTIP = "This Duplicant is producing electrical " + UI.PRE_KEYWORD + "Power" + UI.PST_KEYWORD;
			}

			// Token: 0x02002979 RID: 10617
			public class HARVEST
			{
				// Token: 0x0400AEF8 RID: 44792
				public static LocString NAME = "Harvest";

				// Token: 0x0400AEF9 RID: 44793
				public static LocString STATUS = "Going to harvest";

				// Token: 0x0400AEFA RID: 44794
				public static LocString TOOLTIP = "This Duplicant is harvesting usable materials from a mature " + UI.PRE_KEYWORD + "Plant" + UI.PST_KEYWORD;
			}

			// Token: 0x0200297A RID: 10618
			public class UPROOT
			{
				// Token: 0x0400AEFB RID: 44795
				public static LocString NAME = "Uproot";

				// Token: 0x0400AEFC RID: 44796
				public static LocString STATUS = "Going to uproot";

				// Token: 0x0400AEFD RID: 44797
				public static LocString TOOLTIP = "This Duplicant is uprooting a plant to retrieve a " + UI.PRE_KEYWORD + "Seed" + UI.PST_KEYWORD;
			}

			// Token: 0x0200297B RID: 10619
			public class CLEANTOILET
			{
				// Token: 0x0400AEFE RID: 44798
				public static LocString NAME = "Clean Outhouse";

				// Token: 0x0400AEFF RID: 44799
				public static LocString STATUS = "Going to clean";

				// Token: 0x0400AF00 RID: 44800
				public static LocString TOOLTIP = "This Duplicant is cleaning out the " + BUILDINGS.PREFABS.OUTHOUSE.NAME;
			}

			// Token: 0x0200297C RID: 10620
			public class EMPTYDESALINATOR
			{
				// Token: 0x0400AF01 RID: 44801
				public static LocString NAME = "Empty Desalinator";

				// Token: 0x0400AF02 RID: 44802
				public static LocString STATUS = "Going to clean";

				// Token: 0x0400AF03 RID: 44803
				public static LocString TOOLTIP = "This Duplicant is emptying out the " + BUILDINGS.PREFABS.DESALINATOR.NAME;
			}

			// Token: 0x0200297D RID: 10621
			public class LIQUIDCOOLEDFAN
			{
				// Token: 0x0400AF04 RID: 44804
				public static LocString NAME = "Use Fan";

				// Token: 0x0400AF05 RID: 44805
				public static LocString STATUS = "Going to use fan";

				// Token: 0x0400AF06 RID: 44806
				public static LocString TOOLTIP = "This Duplicant is attempting to cool down the area";
			}

			// Token: 0x0200297E RID: 10622
			public class ICECOOLEDFAN
			{
				// Token: 0x0400AF07 RID: 44807
				public static LocString NAME = "Use Fan";

				// Token: 0x0400AF08 RID: 44808
				public static LocString STATUS = "Going to use fan";

				// Token: 0x0400AF09 RID: 44809
				public static LocString TOOLTIP = "This Duplicant is attempting to cool down the area";
			}

			// Token: 0x0200297F RID: 10623
			public class COOK
			{
				// Token: 0x0400AF0A RID: 44810
				public static LocString NAME = "Cook";

				// Token: 0x0400AF0B RID: 44811
				public static LocString STATUS = "Going to cook";

				// Token: 0x0400AF0C RID: 44812
				public static LocString TOOLTIP = "This Duplicant is cooking " + UI.PRE_KEYWORD + "Food" + UI.PST_KEYWORD;
			}

			// Token: 0x02002980 RID: 10624
			public class COMPOUND
			{
				// Token: 0x0400AF0D RID: 44813
				public static LocString NAME = "Compound Medicine";

				// Token: 0x0400AF0E RID: 44814
				public static LocString STATUS = "Going to compound medicine";

				// Token: 0x0400AF0F RID: 44815
				public static LocString TOOLTIP = "This Duplicant is fabricating " + UI.PRE_KEYWORD + "Medicine" + UI.PST_KEYWORD;
			}

			// Token: 0x02002981 RID: 10625
			public class TRAIN
			{
				// Token: 0x0400AF10 RID: 44816
				public static LocString NAME = "Train";

				// Token: 0x0400AF11 RID: 44817
				public static LocString STATUS = "Training";

				// Token: 0x0400AF12 RID: 44818
				public static LocString TOOLTIP = "This Duplicant is busy training";
			}

			// Token: 0x02002982 RID: 10626
			public class MUSH
			{
				// Token: 0x0400AF13 RID: 44819
				public static LocString NAME = "Mush";

				// Token: 0x0400AF14 RID: 44820
				public static LocString STATUS = "Going to mush";

				// Token: 0x0400AF15 RID: 44821
				public static LocString TOOLTIP = "This Duplicant is producing " + UI.PRE_KEYWORD + "Food" + UI.PST_KEYWORD;
			}

			// Token: 0x02002983 RID: 10627
			public class COMPOSTWORKABLE
			{
				// Token: 0x0400AF16 RID: 44822
				public static LocString NAME = "Compost";

				// Token: 0x0400AF17 RID: 44823
				public static LocString STATUS = "Going to compost";

				// Token: 0x0400AF18 RID: 44824
				public static LocString TOOLTIP = "This Duplicant is dropping off organic material at the " + BUILDINGS.PREFABS.COMPOST.NAME;
			}

			// Token: 0x02002984 RID: 10628
			public class FLIPCOMPOST
			{
				// Token: 0x0400AF19 RID: 44825
				public static LocString NAME = "Flip";

				// Token: 0x0400AF1A RID: 44826
				public static LocString STATUS = "Going to flip compost";

				// Token: 0x0400AF1B RID: 44827
				public static LocString TOOLTIP = BUILDINGS.PREFABS.COMPOST.NAME + "s need to be flipped in order for their contents to compost";
			}

			// Token: 0x02002985 RID: 10629
			public class DEPRESSURIZE
			{
				// Token: 0x0400AF1C RID: 44828
				public static LocString NAME = "Depressurize Well";

				// Token: 0x0400AF1D RID: 44829
				public static LocString STATUS = "Going to depressurize well";

				// Token: 0x0400AF1E RID: 44830
				public static LocString TOOLTIP = BUILDINGS.PREFABS.OILWELLCAP.NAME + "s need to be periodically depressurized to function";
			}

			// Token: 0x02002986 RID: 10630
			public class FABRICATE
			{
				// Token: 0x0400AF1F RID: 44831
				public static LocString NAME = "Fabricate";

				// Token: 0x0400AF20 RID: 44832
				public static LocString STATUS = "Going to fabricate";

				// Token: 0x0400AF21 RID: 44833
				public static LocString TOOLTIP = "This Duplicant is crafting something";
			}

			// Token: 0x02002987 RID: 10631
			public class BUILD
			{
				// Token: 0x0400AF22 RID: 44834
				public static LocString NAME = "Build";

				// Token: 0x0400AF23 RID: 44835
				public static LocString STATUS = "Going to build";

				// Token: 0x0400AF24 RID: 44836
				public static LocString TOOLTIP = "This Duplicant is constructing a new building";
			}

			// Token: 0x02002988 RID: 10632
			public class BUILDDIG
			{
				// Token: 0x0400AF25 RID: 44837
				public static LocString NAME = "Construction Dig";

				// Token: 0x0400AF26 RID: 44838
				public static LocString STATUS = "Going to construction dig";

				// Token: 0x0400AF27 RID: 44839
				public static LocString TOOLTIP = "This Duplicant is making room for a planned construction task by performing this dig";
			}

			// Token: 0x02002989 RID: 10633
			public class DIG
			{
				// Token: 0x0400AF28 RID: 44840
				public static LocString NAME = "Dig";

				// Token: 0x0400AF29 RID: 44841
				public static LocString STATUS = "Going to dig";

				// Token: 0x0400AF2A RID: 44842
				public static LocString TOOLTIP = "This Duplicant is digging out a tile";
			}

			// Token: 0x0200298A RID: 10634
			public class FETCH
			{
				// Token: 0x0400AF2B RID: 44843
				public static LocString NAME = "Deliver";

				// Token: 0x0400AF2C RID: 44844
				public static LocString STATUS = "Delivering";

				// Token: 0x0400AF2D RID: 44845
				public static LocString TOOLTIP = "This Duplicant is delivering materials where they need to go";

				// Token: 0x0400AF2E RID: 44846
				public static LocString REPORT_NAME = "Deliver to {0}";
			}

			// Token: 0x0200298B RID: 10635
			public class JOYREACTION
			{
				// Token: 0x0400AF2F RID: 44847
				public static LocString NAME = "Joy Reaction";

				// Token: 0x0400AF30 RID: 44848
				public static LocString STATUS = "Overjoyed";

				// Token: 0x0400AF31 RID: 44849
				public static LocString TOOLTIP = "This Duplicant is taking a moment to relish in their own happiness";

				// Token: 0x0400AF32 RID: 44850
				public static LocString REPORT_NAME = "Overjoyed Reaction";
			}

			// Token: 0x0200298C RID: 10636
			public class ROCKETCONTROL
			{
				// Token: 0x0400AF33 RID: 44851
				public static LocString NAME = "Rocket Control";

				// Token: 0x0400AF34 RID: 44852
				public static LocString STATUS = "Controlling rocket";

				// Token: 0x0400AF35 RID: 44853
				public static LocString TOOLTIP = "This Duplicant is keeping their spacecraft on course";

				// Token: 0x0400AF36 RID: 44854
				public static LocString REPORT_NAME = "Rocket Control";
			}

			// Token: 0x0200298D RID: 10637
			public class STORAGEFETCH
			{
				// Token: 0x0400AF37 RID: 44855
				public static LocString NAME = "Store Materials";

				// Token: 0x0400AF38 RID: 44856
				public static LocString STATUS = "Storing materials";

				// Token: 0x0400AF39 RID: 44857
				public static LocString TOOLTIP = "This Duplicant is moving materials into storage for later use";

				// Token: 0x0400AF3A RID: 44858
				public static LocString REPORT_NAME = "Store {0}";
			}

			// Token: 0x0200298E RID: 10638
			public class EQUIPMENTFETCH
			{
				// Token: 0x0400AF3B RID: 44859
				public static LocString NAME = "Store Equipment";

				// Token: 0x0400AF3C RID: 44860
				public static LocString STATUS = "Storing equipment";

				// Token: 0x0400AF3D RID: 44861
				public static LocString TOOLTIP = "This Duplicant is transporting equipment for storage";

				// Token: 0x0400AF3E RID: 44862
				public static LocString REPORT_NAME = "Store {0}";
			}

			// Token: 0x0200298F RID: 10639
			public class REPAIRFETCH
			{
				// Token: 0x0400AF3F RID: 44863
				public static LocString NAME = "Repair Supply";

				// Token: 0x0400AF40 RID: 44864
				public static LocString STATUS = "Supplying repair materials";

				// Token: 0x0400AF41 RID: 44865
				public static LocString TOOLTIP = "This Duplicant is delivering materials to where they'll be needed to repair buildings";
			}

			// Token: 0x02002990 RID: 10640
			public class RESEARCHFETCH
			{
				// Token: 0x0400AF42 RID: 44866
				public static LocString NAME = "Research Supply";

				// Token: 0x0400AF43 RID: 44867
				public static LocString STATUS = "Supplying research materials";

				// Token: 0x0400AF44 RID: 44868
				public static LocString TOOLTIP = "This Duplicant is delivering materials where they'll be needed to conduct " + UI.PRE_KEYWORD + "Research" + UI.PST_KEYWORD;
			}

			// Token: 0x02002991 RID: 10641
			public class FARMFETCH
			{
				// Token: 0x0400AF45 RID: 44869
				public static LocString NAME = "Farming Supply";

				// Token: 0x0400AF46 RID: 44870
				public static LocString STATUS = "Supplying farming materials";

				// Token: 0x0400AF47 RID: 44871
				public static LocString TOOLTIP = "This Duplicant is delivering farming materials where they're needed to tend " + UI.PRE_KEYWORD + "Crops" + UI.PST_KEYWORD;
			}

			// Token: 0x02002992 RID: 10642
			public class FETCHCRITICAL
			{
				// Token: 0x0400AF48 RID: 44872
				public static LocString NAME = "Life Support Supply";

				// Token: 0x0400AF49 RID: 44873
				public static LocString STATUS = "Supplying critical materials";

				// Token: 0x0400AF4A RID: 44874
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is delivering materials required to perform ",
					UI.PRE_KEYWORD,
					"Life Support",
					UI.PST_KEYWORD,
					" Errands"
				});

				// Token: 0x0400AF4B RID: 44875
				public static LocString REPORT_NAME = "Life Support Supply to {0}";
			}

			// Token: 0x02002993 RID: 10643
			public class MACHINEFETCH
			{
				// Token: 0x0400AF4C RID: 44876
				public static LocString NAME = "Operational Supply";

				// Token: 0x0400AF4D RID: 44877
				public static LocString STATUS = "Supplying operational materials";

				// Token: 0x0400AF4E RID: 44878
				public static LocString TOOLTIP = "This Duplicant is delivering materials to where they'll be needed for machine operation";

				// Token: 0x0400AF4F RID: 44879
				public static LocString REPORT_NAME = "Operational Supply to {0}";
			}

			// Token: 0x02002994 RID: 10644
			public class COOKFETCH
			{
				// Token: 0x0400AF50 RID: 44880
				public static LocString NAME = "Cook Supply";

				// Token: 0x0400AF51 RID: 44881
				public static LocString STATUS = "Supplying cook ingredients";

				// Token: 0x0400AF52 RID: 44882
				public static LocString TOOLTIP = "This Duplicant is delivering materials required to cook " + UI.PRE_KEYWORD + "Food" + UI.PST_KEYWORD;
			}

			// Token: 0x02002995 RID: 10645
			public class DOCTORFETCH
			{
				// Token: 0x0400AF53 RID: 44883
				public static LocString NAME = "Medical Supply";

				// Token: 0x0400AF54 RID: 44884
				public static LocString STATUS = "Supplying medical resources";

				// Token: 0x0400AF55 RID: 44885
				public static LocString TOOLTIP = "This Duplicant is delivering the materials that will be needed to treat sick patients";

				// Token: 0x0400AF56 RID: 44886
				public static LocString REPORT_NAME = "Medical Supply to {0}";
			}

			// Token: 0x02002996 RID: 10646
			public class FOODFETCH
			{
				// Token: 0x0400AF57 RID: 44887
				public static LocString NAME = "Store Food";

				// Token: 0x0400AF58 RID: 44888
				public static LocString STATUS = "Storing food";

				// Token: 0x0400AF59 RID: 44889
				public static LocString TOOLTIP = "This Duplicant is moving edible resources into proper storage";

				// Token: 0x0400AF5A RID: 44890
				public static LocString REPORT_NAME = "Store {0}";
			}

			// Token: 0x02002997 RID: 10647
			public class POWERFETCH
			{
				// Token: 0x0400AF5B RID: 44891
				public static LocString NAME = "Power Supply";

				// Token: 0x0400AF5C RID: 44892
				public static LocString STATUS = "Supplying power materials";

				// Token: 0x0400AF5D RID: 44893
				public static LocString TOOLTIP = "This Duplicant is delivering materials to where they'll be needed for " + UI.PRE_KEYWORD + "Power" + UI.PST_KEYWORD;

				// Token: 0x0400AF5E RID: 44894
				public static LocString REPORT_NAME = "Power Supply to {0}";
			}

			// Token: 0x02002998 RID: 10648
			public class FABRICATEFETCH
			{
				// Token: 0x0400AF5F RID: 44895
				public static LocString NAME = "Fabrication Supply";

				// Token: 0x0400AF60 RID: 44896
				public static LocString STATUS = "Supplying fabrication materials";

				// Token: 0x0400AF61 RID: 44897
				public static LocString TOOLTIP = "This Duplicant is delivering materials required to fabricate new objects";

				// Token: 0x0400AF62 RID: 44898
				public static LocString REPORT_NAME = "Fabrication Supply to {0}";
			}

			// Token: 0x02002999 RID: 10649
			public class BUILDFETCH
			{
				// Token: 0x0400AF63 RID: 44899
				public static LocString NAME = "Construction Supply";

				// Token: 0x0400AF64 RID: 44900
				public static LocString STATUS = "Supplying construction materials";

				// Token: 0x0400AF65 RID: 44901
				public static LocString TOOLTIP = "This delivery will provide materials to a planned construction site";
			}

			// Token: 0x0200299A RID: 10650
			public class FETCHCREATURE
			{
				// Token: 0x0400AF66 RID: 44902
				public static LocString NAME = "Relocate Critter";

				// Token: 0x0400AF67 RID: 44903
				public static LocString STATUS = "Relocating critter";

				// Token: 0x0400AF68 RID: 44904
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is moving a ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					" to a new location"
				});
			}

			// Token: 0x0200299B RID: 10651
			public class FETCHRANCHING
			{
				// Token: 0x0400AF69 RID: 44905
				public static LocString NAME = "Ranching Supply";

				// Token: 0x0400AF6A RID: 44906
				public static LocString STATUS = "Supplying ranching materials";

				// Token: 0x0400AF6B RID: 44907
				public static LocString TOOLTIP = "This Duplicant is delivering materials for ranching activities";
			}

			// Token: 0x0200299C RID: 10652
			public class TRANSPORT
			{
				// Token: 0x0400AF6C RID: 44908
				public static LocString NAME = "Sweep";

				// Token: 0x0400AF6D RID: 44909
				public static LocString STATUS = "Going to sweep";

				// Token: 0x0400AF6E RID: 44910
				public static LocString TOOLTIP = "Moving debris off the ground and into storage improves colony " + UI.PRE_KEYWORD + "Decor" + UI.PST_KEYWORD;
			}

			// Token: 0x0200299D RID: 10653
			public class MOVETOSAFETY
			{
				// Token: 0x0400AF6F RID: 44911
				public static LocString NAME = "Find Safe Area";

				// Token: 0x0400AF70 RID: 44912
				public static LocString STATUS = "Finding safer area";

				// Token: 0x0400AF71 RID: 44913
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is ",
					UI.PRE_KEYWORD,
					"Idle",
					UI.PST_KEYWORD,
					" and looking for somewhere safe and comfy to chill"
				});
			}

			// Token: 0x0200299E RID: 10654
			public class PARTY
			{
				// Token: 0x0400AF72 RID: 44914
				public static LocString NAME = "Party";

				// Token: 0x0400AF73 RID: 44915
				public static LocString STATUS = "Partying";

				// Token: 0x0400AF74 RID: 44916
				public static LocString TOOLTIP = "This Duplicant is partying hard";
			}

			// Token: 0x0200299F RID: 10655
			public class POWER_TINKER
			{
				// Token: 0x0400AF75 RID: 44917
				public static LocString NAME = "Tinker";

				// Token: 0x0400AF76 RID: 44918
				public static LocString STATUS = "Tinkering";

				// Token: 0x0400AF77 RID: 44919
				public static LocString TOOLTIP = "Tinkering with buildings improves their functionality";
			}

			// Token: 0x020029A0 RID: 10656
			public class RANCH
			{
				// Token: 0x0400AF78 RID: 44920
				public static LocString NAME = "Ranch";

				// Token: 0x0400AF79 RID: 44921
				public static LocString STATUS = "Ranching";

				// Token: 0x0400AF7A RID: 44922
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is tending to a ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					"'s well-being"
				});

				// Token: 0x0400AF7B RID: 44923
				public static LocString REPORT_NAME = "Deliver to {0}";
			}

			// Token: 0x020029A1 RID: 10657
			public class CROP_TEND
			{
				// Token: 0x0400AF7C RID: 44924
				public static LocString NAME = "Tend";

				// Token: 0x0400AF7D RID: 44925
				public static LocString STATUS = "Tending plant";

				// Token: 0x0400AF7E RID: 44926
				public static LocString TOOLTIP = "Tending to plants increases their " + UI.PRE_KEYWORD + "Growth Rate" + UI.PST_KEYWORD;
			}

			// Token: 0x020029A2 RID: 10658
			public class DEMOLISH
			{
				// Token: 0x0400AF7F RID: 44927
				public static LocString NAME = "Demolish";

				// Token: 0x0400AF80 RID: 44928
				public static LocString STATUS = "Demolishing object";

				// Token: 0x0400AF81 RID: 44929
				public static LocString TOOLTIP = "Demolishing an object removes it permanently";
			}

			// Token: 0x020029A3 RID: 10659
			public class IDLE
			{
				// Token: 0x0400AF82 RID: 44930
				public static LocString NAME = "Idle";

				// Token: 0x0400AF83 RID: 44931
				public static LocString STATUS = "Idle";

				// Token: 0x0400AF84 RID: 44932
				public static LocString TOOLTIP = "This Duplicant cannot reach any pending " + UI.PRE_KEYWORD + "Errands" + UI.PST_KEYWORD;
			}

			// Token: 0x020029A4 RID: 10660
			public class PRECONDITIONS
			{
				// Token: 0x0400AF85 RID: 44933
				public static LocString HEADER = "The selected {Selected} could:";

				// Token: 0x0400AF86 RID: 44934
				public static LocString SUCCESS_ROW = "{Duplicant} -- {Rank}";

				// Token: 0x0400AF87 RID: 44935
				public static LocString CURRENT_ERRAND = "Current Errand";

				// Token: 0x0400AF88 RID: 44936
				public static LocString RANK_FORMAT = "#{0}";

				// Token: 0x0400AF89 RID: 44937
				public static LocString FAILURE_ROW = "{Duplicant} -- {Reason}";

				// Token: 0x0400AF8A RID: 44938
				public static LocString CONTAINS_OXYGEN = "Not enough Oxygen";

				// Token: 0x0400AF8B RID: 44939
				public static LocString IS_PREEMPTABLE = "Already assigned to {Assignee}";

				// Token: 0x0400AF8C RID: 44940
				public static LocString HAS_URGE = "No current need";

				// Token: 0x0400AF8D RID: 44941
				public static LocString IS_VALID = "Invalid";

				// Token: 0x0400AF8E RID: 44942
				public static LocString IS_PERMITTED = "Not permitted";

				// Token: 0x0400AF8F RID: 44943
				public static LocString IS_ASSIGNED_TO_ME = "Not assigned to {Selected}";

				// Token: 0x0400AF90 RID: 44944
				public static LocString IS_IN_MY_WORLD = "Outside world";

				// Token: 0x0400AF91 RID: 44945
				public static LocString IS_CELL_NOT_IN_MY_WORLD = "Already there";

				// Token: 0x0400AF92 RID: 44946
				public static LocString IS_IN_MY_ROOM = "Outside {Selected}'s room";

				// Token: 0x0400AF93 RID: 44947
				public static LocString IS_PREFERRED_ASSIGNABLE = "Not preferred assignment";

				// Token: 0x0400AF94 RID: 44948
				public static LocString IS_PREFERRED_ASSIGNABLE_OR_URGENT_BLADDER = "Not preferred assignment";

				// Token: 0x0400AF95 RID: 44949
				public static LocString HAS_SKILL_PERK = "Requires learned skill";

				// Token: 0x0400AF96 RID: 44950
				public static LocString IS_MORE_SATISFYING = "Low priority";

				// Token: 0x0400AF97 RID: 44951
				public static LocString CAN_CHAT = "Unreachable";

				// Token: 0x0400AF98 RID: 44952
				public static LocString IS_NOT_RED_ALERT = "Unavailable in Red Alert";

				// Token: 0x0400AF99 RID: 44953
				public static LocString NO_DEAD_BODIES = "Unburied Duplicant";

				// Token: 0x0400AF9A RID: 44954
				public static LocString NOT_A_ROBOT = "Unavailable to Robots";

				// Token: 0x0400AF9B RID: 44955
				public static LocString VALID_MOURNING_SITE = "Nowhere to mourn";

				// Token: 0x0400AF9C RID: 44956
				public static LocString HAS_PLACE_TO_STAND = "Nowhere to stand";

				// Token: 0x0400AF9D RID: 44957
				public static LocString IS_SCHEDULED_TIME = "Not allowed by schedule";

				// Token: 0x0400AF9E RID: 44958
				public static LocString CAN_MOVE_TO = "Unreachable";

				// Token: 0x0400AF9F RID: 44959
				public static LocString CAN_PICKUP = "Cannot pickup";

				// Token: 0x0400AFA0 RID: 44960
				public static LocString IS_AWAKE = "{Selected} is sleeping";

				// Token: 0x0400AFA1 RID: 44961
				public static LocString IS_STANDING = "{Selected} must stand";

				// Token: 0x0400AFA2 RID: 44962
				public static LocString IS_MOVING = "{Selected} is not moving";

				// Token: 0x0400AFA3 RID: 44963
				public static LocString IS_OFF_LADDER = "{Selected} is busy climbing";

				// Token: 0x0400AFA4 RID: 44964
				public static LocString NOT_IN_TUBE = "{Selected} is busy in transit";

				// Token: 0x0400AFA5 RID: 44965
				public static LocString HAS_TRAIT = "Missing required trait";

				// Token: 0x0400AFA6 RID: 44966
				public static LocString IS_OPERATIONAL = "Not operational";

				// Token: 0x0400AFA7 RID: 44967
				public static LocString IS_MARKED_FOR_DECONSTRUCTION = "Being deconstructed";

				// Token: 0x0400AFA8 RID: 44968
				public static LocString IS_NOT_BURROWED = "Is not burrowed";

				// Token: 0x0400AFA9 RID: 44969
				public static LocString IS_CREATURE_AVAILABLE_FOR_RANCHING = "No Critters Available";

				// Token: 0x0400AFAA RID: 44970
				public static LocString IS_CREATURE_AVAILABLE_FOR_FIXED_CAPTURE = "Pen Status OK";

				// Token: 0x0400AFAB RID: 44971
				public static LocString IS_MARKED_FOR_DISABLE = "Building Disabled";

				// Token: 0x0400AFAC RID: 44972
				public static LocString IS_FUNCTIONAL = "Not functioning";

				// Token: 0x0400AFAD RID: 44973
				public static LocString IS_OVERRIDE_TARGET_NULL_OR_ME = "DebugIsOverrideTargetNullOrMe";

				// Token: 0x0400AFAE RID: 44974
				public static LocString NOT_CHORE_CREATOR = "DebugNotChoreCreator";

				// Token: 0x0400AFAF RID: 44975
				public static LocString IS_GETTING_MORE_STRESSED = "{Selected}'s stress is decreasing";

				// Token: 0x0400AFB0 RID: 44976
				public static LocString IS_ALLOWED_BY_AUTOMATION = "Automated";

				// Token: 0x0400AFB1 RID: 44977
				public static LocString CAN_DO_RECREATION = "Not Interested";

				// Token: 0x0400AFB2 RID: 44978
				public static LocString DOES_SUIT_NEED_RECHARGING_IDLE = "Suit is currently charged";

				// Token: 0x0400AFB3 RID: 44979
				public static LocString DOES_SUIT_NEED_RECHARGING_URGENT = "Suit is currently charged";

				// Token: 0x0400AFB4 RID: 44980
				public static LocString HAS_SUIT_MARKER = "No Suit Checkpoint";

				// Token: 0x0400AFB5 RID: 44981
				public static LocString ALLOWED_TO_DEPRESSURIZE = "Not currently overpressure";

				// Token: 0x0400AFB6 RID: 44982
				public static LocString IS_STRESS_ABOVE_ACTIVATION_RANGE = "{Selected} is not stressed right now";

				// Token: 0x0400AFB7 RID: 44983
				public static LocString IS_NOT_ANGRY = "{Selected} is too angry";

				// Token: 0x0400AFB8 RID: 44984
				public static LocString IS_NOT_BEING_ATTACKED = "{Selected} is in combat";

				// Token: 0x0400AFB9 RID: 44985
				public static LocString IS_CONSUMPTION_PERMITTED = "Disallowed by consumable permissions";

				// Token: 0x0400AFBA RID: 44986
				public static LocString CAN_CURE = "No applicable illness";

				// Token: 0x0400AFBB RID: 44987
				public static LocString TREATMENT_AVAILABLE = "No treatable illness";

				// Token: 0x0400AFBC RID: 44988
				public static LocString DOCTOR_AVAILABLE = "No doctors available\n(Duplicants cannot treat themselves)";

				// Token: 0x0400AFBD RID: 44989
				public static LocString IS_OKAY_TIME_TO_SLEEP = "No current need";

				// Token: 0x0400AFBE RID: 44990
				public static LocString IS_NARCOLEPSING = "{Selected} is currently napping";

				// Token: 0x0400AFBF RID: 44991
				public static LocString IS_FETCH_TARGET_AVAILABLE = "No pending deliveries";

				// Token: 0x0400AFC0 RID: 44992
				public static LocString EDIBLE_IS_NOT_NULL = "Consumable Permission not allowed";

				// Token: 0x0400AFC1 RID: 44993
				public static LocString HAS_MINGLE_CELL = "Nowhere to Mingle";

				// Token: 0x0400AFC2 RID: 44994
				public static LocString EXCLUSIVELY_AVAILABLE = "Building Already Busy";

				// Token: 0x0400AFC3 RID: 44995
				public static LocString BLADDER_FULL = "Bladder isn't full";

				// Token: 0x0400AFC4 RID: 44996
				public static LocString BLADDER_NOT_FULL = "Bladder too full";

				// Token: 0x0400AFC5 RID: 44997
				public static LocString CURRENTLY_PEEING = "Currently Peeing";

				// Token: 0x0400AFC6 RID: 44998
				public static LocString HAS_BALLOON_STALL_CELL = "Has a location for a Balloon Stall";

				// Token: 0x0400AFC7 RID: 44999
				public static LocString IS_MINION = "Must be a Duplicant";

				// Token: 0x0400AFC8 RID: 45000
				public static LocString IS_ROCKET_TRAVELLING = "Rocket must be travelling";
			}
		}

		// Token: 0x02001CE2 RID: 7394
		public class SKILLGROUPS
		{
			// Token: 0x020029A5 RID: 10661
			public class MINING
			{
				// Token: 0x0400AFC9 RID: 45001
				public static LocString NAME = "Digger";
			}

			// Token: 0x020029A6 RID: 10662
			public class BUILDING
			{
				// Token: 0x0400AFCA RID: 45002
				public static LocString NAME = "Builder";
			}

			// Token: 0x020029A7 RID: 10663
			public class FARMING
			{
				// Token: 0x0400AFCB RID: 45003
				public static LocString NAME = "Farmer";
			}

			// Token: 0x020029A8 RID: 10664
			public class RANCHING
			{
				// Token: 0x0400AFCC RID: 45004
				public static LocString NAME = "Rancher";
			}

			// Token: 0x020029A9 RID: 10665
			public class COOKING
			{
				// Token: 0x0400AFCD RID: 45005
				public static LocString NAME = "Cooker";
			}

			// Token: 0x020029AA RID: 10666
			public class ART
			{
				// Token: 0x0400AFCE RID: 45006
				public static LocString NAME = "Decorator";
			}

			// Token: 0x020029AB RID: 10667
			public class RESEARCH
			{
				// Token: 0x0400AFCF RID: 45007
				public static LocString NAME = "Researcher";
			}

			// Token: 0x020029AC RID: 10668
			public class SUITS
			{
				// Token: 0x0400AFD0 RID: 45008
				public static LocString NAME = "Suit Wearer";
			}

			// Token: 0x020029AD RID: 10669
			public class HAULING
			{
				// Token: 0x0400AFD1 RID: 45009
				public static LocString NAME = "Supplier";
			}

			// Token: 0x020029AE RID: 10670
			public class TECHNICALS
			{
				// Token: 0x0400AFD2 RID: 45010
				public static LocString NAME = "Operator";
			}

			// Token: 0x020029AF RID: 10671
			public class MEDICALAID
			{
				// Token: 0x0400AFD3 RID: 45011
				public static LocString NAME = "Doctor";
			}

			// Token: 0x020029B0 RID: 10672
			public class BASEKEEPING
			{
				// Token: 0x0400AFD4 RID: 45012
				public static LocString NAME = "Tidier";
			}

			// Token: 0x020029B1 RID: 10673
			public class ROCKETRY
			{
				// Token: 0x0400AFD5 RID: 45013
				public static LocString NAME = "Pilot";
			}
		}

		// Token: 0x02001CE3 RID: 7395
		public class CHOREGROUPS
		{
			// Token: 0x020029B2 RID: 10674
			public class ART
			{
				// Token: 0x0400AFD6 RID: 45014
				public static LocString NAME = "Decorating";

				// Token: 0x0400AFD7 RID: 45015
				public static LocString DESC = string.Concat(new string[]
				{
					"Sculpt or paint to improve colony ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x0400AFD8 RID: 45016
				public static LocString ARCHETYPE_NAME = "Decorator";
			}

			// Token: 0x020029B3 RID: 10675
			public class COMBAT
			{
				// Token: 0x0400AFD9 RID: 45017
				public static LocString NAME = "Attacking";

				// Token: 0x0400AFDA RID: 45018
				public static LocString DESC = "Fight wild " + UI.FormatAsLink("Critters", "CREATURES") + ".";

				// Token: 0x0400AFDB RID: 45019
				public static LocString ARCHETYPE_NAME = "Attacker";
			}

			// Token: 0x020029B4 RID: 10676
			public class LIFESUPPORT
			{
				// Token: 0x0400AFDC RID: 45020
				public static LocString NAME = "Life Support";

				// Token: 0x0400AFDD RID: 45021
				public static LocString DESC = string.Concat(new string[]
				{
					"Maintain ",
					BUILDINGS.PREFABS.ALGAEHABITAT.NAME,
					"s, ",
					BUILDINGS.PREFABS.AIRFILTER.NAME,
					"s, and ",
					BUILDINGS.PREFABS.WATERPURIFIER.NAME,
					"s to support colony life."
				});

				// Token: 0x0400AFDE RID: 45022
				public static LocString ARCHETYPE_NAME = "Life Supporter";
			}

			// Token: 0x020029B5 RID: 10677
			public class TOGGLE
			{
				// Token: 0x0400AFDF RID: 45023
				public static LocString NAME = "Toggling";

				// Token: 0x0400AFE0 RID: 45024
				public static LocString DESC = "Enable or disable buildings, adjust building settings, and set or flip switches and sensors.";

				// Token: 0x0400AFE1 RID: 45025
				public static LocString ARCHETYPE_NAME = "Toggler";
			}

			// Token: 0x020029B6 RID: 10678
			public class COOK
			{
				// Token: 0x0400AFE2 RID: 45026
				public static LocString NAME = "Cooking";

				// Token: 0x0400AFE3 RID: 45027
				public static LocString DESC = string.Concat(new string[]
				{
					"Operate ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					" preparation buildings."
				});

				// Token: 0x0400AFE4 RID: 45028
				public static LocString ARCHETYPE_NAME = "Cooker";
			}

			// Token: 0x020029B7 RID: 10679
			public class RESEARCH
			{
				// Token: 0x0400AFE5 RID: 45029
				public static LocString NAME = "Researching";

				// Token: 0x0400AFE6 RID: 45030
				public static LocString DESC = string.Concat(new string[]
				{
					"Use ",
					UI.PRE_KEYWORD,
					"Research Stations",
					UI.PST_KEYWORD,
					" to unlock new technologies."
				});

				// Token: 0x0400AFE7 RID: 45031
				public static LocString ARCHETYPE_NAME = "Researcher";
			}

			// Token: 0x020029B8 RID: 10680
			public class REPAIR
			{
				// Token: 0x0400AFE8 RID: 45032
				public static LocString NAME = "Repairing";

				// Token: 0x0400AFE9 RID: 45033
				public static LocString DESC = "Repair damaged buildings.";

				// Token: 0x0400AFEA RID: 45034
				public static LocString ARCHETYPE_NAME = "Repairer";
			}

			// Token: 0x020029B9 RID: 10681
			public class FARMING
			{
				// Token: 0x0400AFEB RID: 45035
				public static LocString NAME = "Farming";

				// Token: 0x0400AFEC RID: 45036
				public static LocString DESC = string.Concat(new string[]
				{
					"Gather crops from mature ",
					UI.PRE_KEYWORD,
					"Plants",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x0400AFED RID: 45037
				public static LocString ARCHETYPE_NAME = "Farmer";
			}

			// Token: 0x020029BA RID: 10682
			public class RANCHING
			{
				// Token: 0x0400AFEE RID: 45038
				public static LocString NAME = "Ranching";

				// Token: 0x0400AFEF RID: 45039
				public static LocString DESC = "Tend to domesticated " + UI.FormatAsLink("Critters", "CREATURES") + ".";

				// Token: 0x0400AFF0 RID: 45040
				public static LocString ARCHETYPE_NAME = "Rancher";
			}

			// Token: 0x020029BB RID: 10683
			public class BUILD
			{
				// Token: 0x0400AFF1 RID: 45041
				public static LocString NAME = "Building";

				// Token: 0x0400AFF2 RID: 45042
				public static LocString DESC = "Construct new buildings.";

				// Token: 0x0400AFF3 RID: 45043
				public static LocString ARCHETYPE_NAME = "Builder";
			}

			// Token: 0x020029BC RID: 10684
			public class HAULING
			{
				// Token: 0x0400AFF4 RID: 45044
				public static LocString NAME = "Supplying";

				// Token: 0x0400AFF5 RID: 45045
				public static LocString DESC = "Run resources to critical buildings and urgent storage.";

				// Token: 0x0400AFF6 RID: 45046
				public static LocString ARCHETYPE_NAME = "Supplier";
			}

			// Token: 0x020029BD RID: 10685
			public class STORAGE
			{
				// Token: 0x0400AFF7 RID: 45047
				public static LocString NAME = "Storing";

				// Token: 0x0400AFF8 RID: 45048
				public static LocString DESC = "Fill storage buildings with resources when no other errands are available.";

				// Token: 0x0400AFF9 RID: 45049
				public static LocString ARCHETYPE_NAME = "Storer";
			}

			// Token: 0x020029BE RID: 10686
			public class RECREATION
			{
				// Token: 0x0400AFFA RID: 45050
				public static LocString NAME = "Relaxing";

				// Token: 0x0400AFFB RID: 45051
				public static LocString DESC = "Use leisure facilities, chat with other Duplicants, and relieve Stress.";

				// Token: 0x0400AFFC RID: 45052
				public static LocString ARCHETYPE_NAME = "Relaxer";
			}

			// Token: 0x020029BF RID: 10687
			public class BASEKEEPING
			{
				// Token: 0x0400AFFD RID: 45053
				public static LocString NAME = "Tidying";

				// Token: 0x0400AFFE RID: 45054
				public static LocString DESC = "Sweep, mop, and disinfect objects within the colony.";

				// Token: 0x0400AFFF RID: 45055
				public static LocString ARCHETYPE_NAME = "Tidier";
			}

			// Token: 0x020029C0 RID: 10688
			public class DIG
			{
				// Token: 0x0400B000 RID: 45056
				public static LocString NAME = "Digging";

				// Token: 0x0400B001 RID: 45057
				public static LocString DESC = "Mine raw resources.";

				// Token: 0x0400B002 RID: 45058
				public static LocString ARCHETYPE_NAME = "Digger";
			}

			// Token: 0x020029C1 RID: 10689
			public class MEDICALAID
			{
				// Token: 0x0400B003 RID: 45059
				public static LocString NAME = "Doctoring";

				// Token: 0x0400B004 RID: 45060
				public static LocString DESC = "Treat sick and injured Duplicants.";

				// Token: 0x0400B005 RID: 45061
				public static LocString ARCHETYPE_NAME = "Doctor";
			}

			// Token: 0x020029C2 RID: 10690
			public class MASSAGE
			{
				// Token: 0x0400B006 RID: 45062
				public static LocString NAME = "Relaxing";

				// Token: 0x0400B007 RID: 45063
				public static LocString DESC = "Take breaks for massages.";

				// Token: 0x0400B008 RID: 45064
				public static LocString ARCHETYPE_NAME = "Relaxer";
			}

			// Token: 0x020029C3 RID: 10691
			public class MACHINEOPERATING
			{
				// Token: 0x0400B009 RID: 45065
				public static LocString NAME = "Operating";

				// Token: 0x0400B00A RID: 45066
				public static LocString DESC = "Operating machinery for production, fabrication, and utility purposes.";

				// Token: 0x0400B00B RID: 45067
				public static LocString ARCHETYPE_NAME = "Operator";
			}

			// Token: 0x020029C4 RID: 10692
			public class SUITS
			{
				// Token: 0x0400B00C RID: 45068
				public static LocString ARCHETYPE_NAME = "Suit Wearer";
			}

			// Token: 0x020029C5 RID: 10693
			public class ROCKETRY
			{
				// Token: 0x0400B00D RID: 45069
				public static LocString NAME = "Rocketry";

				// Token: 0x0400B00E RID: 45070
				public static LocString DESC = "Pilot rockets";

				// Token: 0x0400B00F RID: 45071
				public static LocString ARCHETYPE_NAME = "Pilot";
			}
		}

		// Token: 0x02001CE4 RID: 7396
		public class STATUSITEMS
		{
			// Token: 0x020029C6 RID: 10694
			public class GENERIC_DELIVER
			{
				// Token: 0x0400B010 RID: 45072
				public static LocString NAME = "Delivering resources to {Target}";

				// Token: 0x0400B011 RID: 45073
				public static LocString TOOLTIP = "This Duplicant is transporting materials to <b>{Target}</b>";
			}

			// Token: 0x020029C7 RID: 10695
			public class COUGHING
			{
				// Token: 0x0400B012 RID: 45074
				public static LocString NAME = "Yucky Lungs Coughing";

				// Token: 0x0400B013 RID: 45075
				public static LocString TOOLTIP = "Hey! Do that into your elbow\n• Coughing fit was caused by " + DUPLICANTS.MODIFIERS.CONTAMINATEDLUNGS.NAME;
			}

			// Token: 0x020029C8 RID: 10696
			public class WEARING_PAJAMAS
			{
				// Token: 0x0400B014 RID: 45076
				public static LocString NAME = "Wearing " + UI.FormatAsLink("Pajamas", "SLEEP_CLINIC_PAJAMAS");

				// Token: 0x0400B015 RID: 45077
				public static LocString TOOLTIP = "This Duplicant can now produce " + UI.FormatAsLink("Dream Journals", "DREAMJOURNAL") + " when sleeping";
			}

			// Token: 0x020029C9 RID: 10697
			public class DREAMING
			{
				// Token: 0x0400B016 RID: 45078
				public static LocString NAME = "Dreaming";

				// Token: 0x0400B017 RID: 45079
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is adventuring through their own subconscious\n\nDreams are caused by wearing ",
					UI.FormatAsLink("Pajamas", "SLEEP_CLINIC_PAJAMAS"),
					"\n\n",
					UI.FormatAsLink("Dream Journal", "DREAMJOURNAL"),
					" will be ready in {time}"
				});
			}

			// Token: 0x020029CA RID: 10698
			public class SLEEPING
			{
				// Token: 0x0400B018 RID: 45080
				public static LocString NAME = "Sleeping";

				// Token: 0x0400B019 RID: 45081
				public static LocString TOOLTIP = "This Duplicant is recovering stamina";

				// Token: 0x0400B01A RID: 45082
				public static LocString TOOLTIP_DISTURBER = "\n\nThey were sleeping peacefully until they were disturbed by <b>{Disturber}</b>";
			}

			// Token: 0x020029CB RID: 10699
			public class SLEEPINGPEACEFULLY
			{
				// Token: 0x0400B01B RID: 45083
				public static LocString NAME = "Sleeping peacefully";

				// Token: 0x0400B01C RID: 45084
				public static LocString TOOLTIP = "This Duplicant is getting well-deserved, quality sleep\n\nAt this rate they're sure to feel " + UI.FormatAsLink("Well Rested", "SLEEP") + " tomorrow morning";
			}

			// Token: 0x020029CC RID: 10700
			public class SLEEPINGBADLY
			{
				// Token: 0x0400B01D RID: 45085
				public static LocString NAME = "Sleeping badly";

				// Token: 0x0400B01E RID: 45086
				public static LocString TOOLTIP = "This Duplicant's having trouble falling asleep due to noise from <b>{Disturber}</b>\n\nThey're going to feel a bit " + UI.FormatAsLink("Unrested", "SLEEP") + " tomorrow morning";
			}

			// Token: 0x020029CD RID: 10701
			public class SLEEPINGTERRIBLY
			{
				// Token: 0x0400B01F RID: 45087
				public static LocString NAME = "Can't sleep";

				// Token: 0x0400B020 RID: 45088
				public static LocString TOOLTIP = "This Duplicant was woken up by noise from <b>{Disturber}</b> and can't get back to sleep\n\nThey're going to feel " + UI.FormatAsLink("Dead Tired", "SLEEP") + " tomorrow morning";
			}

			// Token: 0x020029CE RID: 10702
			public class SLEEPINGINTERRUPTEDBYLIGHT
			{
				// Token: 0x0400B021 RID: 45089
				public static LocString NAME = "Interrupted Sleep: Bright Light";

				// Token: 0x0400B022 RID: 45090
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant can't sleep because the ",
					UI.PRE_KEYWORD,
					"Lights",
					UI.PST_KEYWORD,
					" are still on"
				});
			}

			// Token: 0x020029CF RID: 10703
			public class SLEEPINGINTERRUPTEDBYNOISE
			{
				// Token: 0x0400B023 RID: 45091
				public static LocString NAME = "Interrupted Sleep: Snoring Friend";

				// Token: 0x0400B024 RID: 45092
				public static LocString TOOLTIP = "This Duplicant is having trouble sleeping thanks to a certain noisy someone";
			}

			// Token: 0x020029D0 RID: 10704
			public class SLEEPINGINTERRUPTEDBYFEAROFDARK
			{
				// Token: 0x0400B025 RID: 45093
				public static LocString NAME = "Interrupted Sleep: Afraid of Dark";

				// Token: 0x0400B026 RID: 45094
				public static LocString TOOLTIP = "This Duplicant is having trouble sleeping because of their fear of the dark";
			}

			// Token: 0x020029D1 RID: 10705
			public class SLEEPINGINTERRUPTEDBYMOVEMENT
			{
				// Token: 0x0400B027 RID: 45095
				public static LocString NAME = "Interrupted Sleep: Bed Jostling";

				// Token: 0x0400B028 RID: 45096
				public static LocString TOOLTIP = "This Duplicant was woken up because their bed was moved";
			}

			// Token: 0x020029D2 RID: 10706
			public class REDALERT
			{
				// Token: 0x0400B029 RID: 45097
				public static LocString NAME = "Red Alert!";

				// Token: 0x0400B02A RID: 45098
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The colony is in a state of ",
					UI.PRE_KEYWORD,
					"Red Alert",
					UI.PST_KEYWORD,
					". Duplicants will not eat, sleep, use the bathroom, or engage in leisure activities while the ",
					UI.PRE_KEYWORD,
					"Red Alert",
					UI.PST_KEYWORD,
					" is active"
				});
			}

			// Token: 0x020029D3 RID: 10707
			public class ROLE
			{
				// Token: 0x0400B02B RID: 45099
				public static LocString NAME = "{Role}: {Progress} Mastery";

				// Token: 0x0400B02C RID: 45100
				public static LocString TOOLTIP = "This Duplicant is working as a <b>{Role}</b>\n\nThey have <b>{Progress}</b> mastery of this job";
			}

			// Token: 0x020029D4 RID: 10708
			public class LOWOXYGEN
			{
				// Token: 0x0400B02D RID: 45101
				public static LocString NAME = "Oxygen low";

				// Token: 0x0400B02E RID: 45102
				public static LocString TOOLTIP = "This Duplicant is working in a low breathability area";

				// Token: 0x0400B02F RID: 45103
				public static LocString NOTIFICATION_NAME = "Low " + ELEMENTS.OXYGEN.NAME + " area entered";

				// Token: 0x0400B030 RID: 45104
				public static LocString NOTIFICATION_TOOLTIP = "These Duplicants are working in areas with low " + ELEMENTS.OXYGEN.NAME + ":";
			}

			// Token: 0x020029D5 RID: 10709
			public class SEVEREWOUNDS
			{
				// Token: 0x0400B031 RID: 45105
				public static LocString NAME = "Severely injured";

				// Token: 0x0400B032 RID: 45106
				public static LocString TOOLTIP = "This Duplicant is badly hurt";

				// Token: 0x0400B033 RID: 45107
				public static LocString NOTIFICATION_NAME = "Severely injured";

				// Token: 0x0400B034 RID: 45108
				public static LocString NOTIFICATION_TOOLTIP = "These Duplicants are badly hurt and require medical attention";
			}

			// Token: 0x020029D6 RID: 10710
			public class INCAPACITATED
			{
				// Token: 0x0400B035 RID: 45109
				public static LocString NAME = "Incapacitated: {CauseOfIncapacitation}\nTime until death: {TimeUntilDeath}\n";

				// Token: 0x0400B036 RID: 45110
				public static LocString TOOLTIP = "This Duplicant is near death!\n\nAssign them to a Triage Cot for rescue";

				// Token: 0x0400B037 RID: 45111
				public static LocString NOTIFICATION_NAME = "Incapacitated";

				// Token: 0x0400B038 RID: 45112
				public static LocString NOTIFICATION_TOOLTIP = "These Duplicants are near death.\nA " + BUILDINGS.PREFABS.MEDICALCOT.NAME + " is required for rescue:";
			}

			// Token: 0x020029D7 RID: 10711
			public class BEDUNREACHABLE
			{
				// Token: 0x0400B039 RID: 45113
				public static LocString NAME = "Cannot reach bed";

				// Token: 0x0400B03A RID: 45114
				public static LocString TOOLTIP = "This Duplicant cannot reach their bed";

				// Token: 0x0400B03B RID: 45115
				public static LocString NOTIFICATION_NAME = "Unreachable bed";

				// Token: 0x0400B03C RID: 45116
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants cannot sleep because their ",
					UI.PRE_KEYWORD,
					"Beds",
					UI.PST_KEYWORD,
					" are beyond their reach:"
				});
			}

			// Token: 0x020029D8 RID: 10712
			public class COLD
			{
				// Token: 0x0400B03D RID: 45117
				public static LocString NAME = "Chilly surroundings";

				// Token: 0x0400B03E RID: 45118
				public static LocString TOOLTIP = "This Duplicant cannot retain enough heat to stay warm and may be under insulated for this area\n\nStress: <b>{StressModification}</b>\n\nCurrent Environmental Exchange: <b>{currentTransferWattage}</b>\n\nInsulation Thickness: {conductivityBarrier}";
			}

			// Token: 0x020029D9 RID: 10713
			public class DAILYRATIONLIMITREACHED
			{
				// Token: 0x0400B03F RID: 45119
				public static LocString NAME = "Daily calorie limit reached";

				// Token: 0x0400B040 RID: 45120
				public static LocString TOOLTIP = "This Duplicant has consumed their allotted " + UI.FormatAsLink("Rations", "FOOD") + " for the day";

				// Token: 0x0400B041 RID: 45121
				public static LocString NOTIFICATION_NAME = "Daily calorie limit reached";

				// Token: 0x0400B042 RID: 45122
				public static LocString NOTIFICATION_TOOLTIP = "These Duplicants have consumed their allotted " + UI.FormatAsLink("Rations", "FOOD") + " for the day:";
			}

			// Token: 0x020029DA RID: 10714
			public class DOCTOR
			{
				// Token: 0x0400B043 RID: 45123
				public static LocString NAME = "Treating Patient";

				// Token: 0x0400B044 RID: 45124
				public static LocString STATUS = "This Duplicant is going to administer medical care to an ailing friend";
			}

			// Token: 0x020029DB RID: 10715
			public class HOLDINGBREATH
			{
				// Token: 0x0400B045 RID: 45125
				public static LocString NAME = "Holding breath";

				// Token: 0x0400B046 RID: 45126
				public static LocString TOOLTIP = "This Duplicant cannot breathe in their current location";
			}

			// Token: 0x020029DC RID: 10716
			public class RECOVERINGBREATH
			{
				// Token: 0x0400B047 RID: 45127
				public static LocString NAME = "Recovering breath";

				// Token: 0x0400B048 RID: 45128
				public static LocString TOOLTIP = "This Duplicant held their breath too long and needs a moment";
			}

			// Token: 0x020029DD RID: 10717
			public class HOT
			{
				// Token: 0x0400B049 RID: 45129
				public static LocString NAME = "Toasty surroundings";

				// Token: 0x0400B04A RID: 45130
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant cannot let off enough ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" to stay cool and may be over insulated for this area\n\nStress Modification: <b>{StressModification}</b>\n\nCurrent Environmental Exchange: <b>{currentTransferWattage}</b>\n\nInsulation Thickness: {conductivityBarrier}"
				});
			}

			// Token: 0x020029DE RID: 10718
			public class HUNGRY
			{
				// Token: 0x0400B04B RID: 45131
				public static LocString NAME = "Hungry";

				// Token: 0x0400B04C RID: 45132
				public static LocString TOOLTIP = "This Duplicant would really like something to eat";
			}

			// Token: 0x020029DF RID: 10719
			public class POORDECOR
			{
				// Token: 0x0400B04D RID: 45133
				public static LocString NAME = "Drab decor";

				// Token: 0x0400B04E RID: 45134
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is depressed by the lack of ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" in this area"
				});
			}

			// Token: 0x020029E0 RID: 10720
			public class POORQUALITYOFLIFE
			{
				// Token: 0x0400B04F RID: 45135
				public static LocString NAME = "Low Morale";

				// Token: 0x0400B050 RID: 45136
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The bad in this Duplicant's life is starting to outweigh the good\n\nImproved amenities and additional ",
					UI.PRE_KEYWORD,
					"Downtime",
					UI.PST_KEYWORD,
					" would help improve their ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x020029E1 RID: 10721
			public class POOR_FOOD_QUALITY
			{
				// Token: 0x0400B051 RID: 45137
				public static LocString NAME = "Lousy Meal";

				// Token: 0x0400B052 RID: 45138
				public static LocString TOOLTIP = "The last meal this Duplicant ate didn't quite meet their expectations";
			}

			// Token: 0x020029E2 RID: 10722
			public class GOOD_FOOD_QUALITY
			{
				// Token: 0x0400B053 RID: 45139
				public static LocString NAME = "Decadent Meal";

				// Token: 0x0400B054 RID: 45140
				public static LocString TOOLTIP = "The last meal this Duplicant ate exceeded their expectations!";
			}

			// Token: 0x020029E3 RID: 10723
			public class NERVOUSBREAKDOWN
			{
				// Token: 0x0400B055 RID: 45141
				public static LocString NAME = "Nervous breakdown";

				// Token: 0x0400B056 RID: 45142
				public static LocString TOOLTIP = UI.PRE_KEYWORD + "Stress" + UI.PST_KEYWORD + " has completely eroded this Duplicant's ability to function";

				// Token: 0x0400B057 RID: 45143
				public static LocString NOTIFICATION_NAME = "Nervous breakdown";

				// Token: 0x0400B058 RID: 45144
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants have cracked under the ",
					UI.PRE_KEYWORD,
					"Stress",
					UI.PST_KEYWORD,
					" and need assistance:"
				});
			}

			// Token: 0x020029E4 RID: 10724
			public class STRESSED
			{
				// Token: 0x0400B059 RID: 45145
				public static LocString NAME = "Stressed";

				// Token: 0x0400B05A RID: 45146
				public static LocString TOOLTIP = "This Duplicant is feeling the pressure";

				// Token: 0x0400B05B RID: 45147
				public static LocString NOTIFICATION_NAME = "High stress";

				// Token: 0x0400B05C RID: 45148
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants are ",
					UI.PRE_KEYWORD,
					"Stressed",
					UI.PST_KEYWORD,
					" and need to unwind:"
				});
			}

			// Token: 0x020029E5 RID: 10725
			public class NORATIONSAVAILABLE
			{
				// Token: 0x0400B05D RID: 45149
				public static LocString NAME = "No food available";

				// Token: 0x0400B05E RID: 45150
				public static LocString TOOLTIP = "There's nothing in the colony for this Duplicant to eat";

				// Token: 0x0400B05F RID: 45151
				public static LocString NOTIFICATION_NAME = "No food available";

				// Token: 0x0400B060 RID: 45152
				public static LocString NOTIFICATION_TOOLTIP = "These Duplicants have nothing to eat:";
			}

			// Token: 0x020029E6 RID: 10726
			public class QUARANTINEAREAUNREACHABLE
			{
				// Token: 0x0400B061 RID: 45153
				public static LocString NAME = "Cannot reach quarantine";

				// Token: 0x0400B062 RID: 45154
				public static LocString TOOLTIP = "This Duplicant cannot reach their quarantine zone";

				// Token: 0x0400B063 RID: 45155
				public static LocString NOTIFICATION_NAME = "Unreachable quarantine";

				// Token: 0x0400B064 RID: 45156
				public static LocString NOTIFICATION_TOOLTIP = "These Duplicants cannot reach their assigned quarantine zones:";
			}

			// Token: 0x020029E7 RID: 10727
			public class QUARANTINED
			{
				// Token: 0x0400B065 RID: 45157
				public static LocString NAME = "Quarantined";

				// Token: 0x0400B066 RID: 45158
				public static LocString TOOLTIP = "This Duplicant has been isolated from the colony";
			}

			// Token: 0x020029E8 RID: 10728
			public class RATIONSUNREACHABLE
			{
				// Token: 0x0400B067 RID: 45159
				public static LocString NAME = "Cannot reach food";

				// Token: 0x0400B068 RID: 45160
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"There is ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					" in the colony that this Duplicant cannot reach"
				});

				// Token: 0x0400B069 RID: 45161
				public static LocString NOTIFICATION_NAME = "Unreachable food";

				// Token: 0x0400B06A RID: 45162
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants cannot access the colony's ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					":"
				});
			}

			// Token: 0x020029E9 RID: 10729
			public class RATIONSNOTPERMITTED
			{
				// Token: 0x0400B06B RID: 45163
				public static LocString NAME = "Food Type Not Permitted";

				// Token: 0x0400B06C RID: 45164
				public static LocString TOOLTIP = "This Duplicant is not allowed to eat any of the " + UI.FormatAsLink("Food", "FOOD") + " in their reach\n\nEnter the <color=#833A5FFF>CONSUMABLES</color> <color=#F44A47><b>[F]</b></color> to adjust their food permissions";

				// Token: 0x0400B06D RID: 45165
				public static LocString NOTIFICATION_NAME = "Unpermitted food";

				// Token: 0x0400B06E RID: 45166
				public static LocString NOTIFICATION_TOOLTIP = "These Duplicants' <color=#833A5FFF>CONSUMABLES</color> <color=#F44A47><b>[F]</b></color> permissions prevent them from eating any of the " + UI.FormatAsLink("Food", "FOOD") + " within their reach:";
			}

			// Token: 0x020029EA RID: 10730
			public class ROTTEN
			{
				// Token: 0x0400B06F RID: 45167
				public static LocString NAME = "Rotten";

				// Token: 0x0400B070 RID: 45168
				public static LocString TOOLTIP = "Gross!";
			}

			// Token: 0x020029EB RID: 10731
			public class STARVING
			{
				// Token: 0x0400B071 RID: 45169
				public static LocString NAME = "Starving";

				// Token: 0x0400B072 RID: 45170
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is about to die and needs ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					"!"
				});

				// Token: 0x0400B073 RID: 45171
				public static LocString NOTIFICATION_NAME = "Starvation";

				// Token: 0x0400B074 RID: 45172
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants are starving and will die if they can't find ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					":"
				});
			}

			// Token: 0x020029EC RID: 10732
			public class STRESS_SIGNAL_AGGRESIVE
			{
				// Token: 0x0400B075 RID: 45173
				public static LocString NAME = "Frustrated";

				// Token: 0x0400B076 RID: 45174
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is trying to keep their cool\n\nImprove this Duplicant's ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" before they destroy something to let off steam"
				});
			}

			// Token: 0x020029ED RID: 10733
			public class STRESS_SIGNAL_BINGE_EAT
			{
				// Token: 0x0400B077 RID: 45175
				public static LocString NAME = "Stress Cravings";

				// Token: 0x0400B078 RID: 45176
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is consumed by hunger\n\nImprove this Duplicant's ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" before they eat all the colony's ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					" stores"
				});
			}

			// Token: 0x020029EE RID: 10734
			public class STRESS_SIGNAL_UGLY_CRIER
			{
				// Token: 0x0400B079 RID: 45177
				public static LocString NAME = "Misty Eyed";

				// Token: 0x0400B07A RID: 45178
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is trying and failing to swallow their emotions\n\nImprove this Duplicant's ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" before they have a good ugly cry"
				});
			}

			// Token: 0x020029EF RID: 10735
			public class STRESS_SIGNAL_VOMITER
			{
				// Token: 0x0400B07B RID: 45179
				public static LocString NAME = "Stress Burp";

				// Token: 0x0400B07C RID: 45180
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Sort of like having butterflies in your stomach, except they're burps\n\nImprove this Duplicant's ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" before they start to stress vomit"
				});
			}

			// Token: 0x020029F0 RID: 10736
			public class STRESS_SIGNAL_BANSHEE
			{
				// Token: 0x0400B07D RID: 45181
				public static LocString NAME = "Suppressed Screams";

				// Token: 0x0400B07E RID: 45182
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is fighting the urge to scream\n\nImprove this Duplicant's ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" before they start wailing uncontrollably"
				});
			}

			// Token: 0x020029F1 RID: 10737
			public class ENTOMBEDCHORE
			{
				// Token: 0x0400B07F RID: 45183
				public static LocString NAME = "Entombed";

				// Token: 0x0400B080 RID: 45184
				public static LocString TOOLTIP = "This Duplicant needs someone to help dig them out!";

				// Token: 0x0400B081 RID: 45185
				public static LocString NOTIFICATION_NAME = "Entombed";

				// Token: 0x0400B082 RID: 45186
				public static LocString NOTIFICATION_TOOLTIP = "These Duplicants are trapped:";
			}

			// Token: 0x020029F2 RID: 10738
			public class EARLYMORNING
			{
				// Token: 0x0400B083 RID: 45187
				public static LocString NAME = "Early Bird";

				// Token: 0x0400B084 RID: 45188
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is jazzed to start the day\n• All ",
					UI.PRE_KEYWORD,
					"Attributes",
					UI.PST_KEYWORD,
					" <b>+2</b> in the morning"
				});
			}

			// Token: 0x020029F3 RID: 10739
			public class NIGHTTIME
			{
				// Token: 0x0400B085 RID: 45189
				public static LocString NAME = "Night Owl";

				// Token: 0x0400B086 RID: 45190
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is more efficient on a nighttime ",
					UI.PRE_KEYWORD,
					"Schedule",
					UI.PST_KEYWORD,
					"\n• All ",
					UI.PRE_KEYWORD,
					"Attributes",
					UI.PST_KEYWORD,
					" <b>+3</b> at night"
				});
			}

			// Token: 0x020029F4 RID: 10740
			public class SUFFOCATING
			{
				// Token: 0x0400B087 RID: 45191
				public static LocString NAME = "Suffocating";

				// Token: 0x0400B088 RID: 45192
				public static LocString TOOLTIP = "This Duplicant cannot breathe!";

				// Token: 0x0400B089 RID: 45193
				public static LocString NOTIFICATION_NAME = "Suffocating";

				// Token: 0x0400B08A RID: 45194
				public static LocString NOTIFICATION_TOOLTIP = "These Duplicants cannot breathe:";
			}

			// Token: 0x020029F5 RID: 10741
			public class TIRED
			{
				// Token: 0x0400B08B RID: 45195
				public static LocString NAME = "Tired";

				// Token: 0x0400B08C RID: 45196
				public static LocString TOOLTIP = "This Duplicant could use a nice nap";
			}

			// Token: 0x020029F6 RID: 10742
			public class IDLE
			{
				// Token: 0x0400B08D RID: 45197
				public static LocString NAME = "Idle";

				// Token: 0x0400B08E RID: 45198
				public static LocString TOOLTIP = "This Duplicant cannot reach any pending errands";

				// Token: 0x0400B08F RID: 45199
				public static LocString NOTIFICATION_NAME = "Idle";

				// Token: 0x0400B090 RID: 45200
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants cannot reach any pending ",
					UI.PRE_KEYWORD,
					"Errands",
					UI.PST_KEYWORD,
					":"
				});
			}

			// Token: 0x020029F7 RID: 10743
			public class FIGHTING
			{
				// Token: 0x0400B091 RID: 45201
				public static LocString NAME = "In combat";

				// Token: 0x0400B092 RID: 45202
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is attacking a ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					"!"
				});

				// Token: 0x0400B093 RID: 45203
				public static LocString NOTIFICATION_NAME = "Combat!";

				// Token: 0x0400B094 RID: 45204
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants have engaged a ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					" in combat:"
				});
			}

			// Token: 0x020029F8 RID: 10744
			public class FLEEING
			{
				// Token: 0x0400B095 RID: 45205
				public static LocString NAME = "Fleeing";

				// Token: 0x0400B096 RID: 45206
				public static LocString TOOLTIP = "This Duplicant is trying to escape something scary!";

				// Token: 0x0400B097 RID: 45207
				public static LocString NOTIFICATION_NAME = "Fleeing!";

				// Token: 0x0400B098 RID: 45208
				public static LocString NOTIFICATION_TOOLTIP = "These Duplicants are trying to escape:";
			}

			// Token: 0x020029F9 RID: 10745
			public class DEAD
			{
				// Token: 0x0400B099 RID: 45209
				public static LocString NAME = "Dead: {Death}";

				// Token: 0x0400B09A RID: 45210
				public static LocString TOOLTIP = "This Duplicant definitely isn't sleeping";
			}

			// Token: 0x020029FA RID: 10746
			public class LASHINGOUT
			{
				// Token: 0x0400B09B RID: 45211
				public static LocString NAME = "Lashing out";

				// Token: 0x0400B09C RID: 45212
				public static LocString TOOLTIP = "This Duplicant is breaking buildings to relieve their " + UI.PRE_KEYWORD + "Stress" + UI.PST_KEYWORD;

				// Token: 0x0400B09D RID: 45213
				public static LocString NOTIFICATION_NAME = "Lashing out";

				// Token: 0x0400B09E RID: 45214
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants broke buildings to relieve their ",
					UI.PRE_KEYWORD,
					"Stress",
					UI.PST_KEYWORD,
					":"
				});
			}

			// Token: 0x020029FB RID: 10747
			public class MOVETOSUITNOTREQUIRED
			{
				// Token: 0x0400B09F RID: 45215
				public static LocString NAME = "Exiting Exosuit area";

				// Token: 0x0400B0A0 RID: 45216
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is leaving an area where a ",
					UI.PRE_KEYWORD,
					"Suit",
					UI.PST_KEYWORD,
					" was required"
				});
			}

			// Token: 0x020029FC RID: 10748
			public class NOROLE
			{
				// Token: 0x0400B0A1 RID: 45217
				public static LocString NAME = "No Job";

				// Token: 0x0400B0A2 RID: 45218
				public static LocString TOOLTIP = "This Duplicant does not have a Job Assignment\n\nEnter the " + UI.FormatAsManagementMenu("Jobs Panel", "[J]") + " to view all available Jobs";
			}

			// Token: 0x020029FD RID: 10749
			public class DROPPINGUNUSEDINVENTORY
			{
				// Token: 0x0400B0A3 RID: 45219
				public static LocString NAME = "Dropping objects";

				// Token: 0x0400B0A4 RID: 45220
				public static LocString TOOLTIP = "This Duplicant is dropping what they're holding";
			}

			// Token: 0x020029FE RID: 10750
			public class MOVINGTOSAFEAREA
			{
				// Token: 0x0400B0A5 RID: 45221
				public static LocString NAME = "Moving to safe area";

				// Token: 0x0400B0A6 RID: 45222
				public static LocString TOOLTIP = "This Duplicant is finding a less dangerous place";
			}

			// Token: 0x020029FF RID: 10751
			public class TOILETUNREACHABLE
			{
				// Token: 0x0400B0A7 RID: 45223
				public static LocString NAME = "Unreachable toilet";

				// Token: 0x0400B0A8 RID: 45224
				public static LocString TOOLTIP = "This Duplicant cannot reach a functioning " + UI.FormatAsLink("Outhouse", "OUTHOUSE") + " or " + UI.FormatAsLink("Lavatory", "FLUSHTOILET");

				// Token: 0x0400B0A9 RID: 45225
				public static LocString NOTIFICATION_NAME = "Unreachable toilet";

				// Token: 0x0400B0AA RID: 45226
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants cannot reach a functioning ",
					UI.FormatAsLink("Outhouse", "OUTHOUSE"),
					" or ",
					UI.FormatAsLink("Lavatory", "FLUSHTOILET"),
					":"
				});
			}

			// Token: 0x02002A00 RID: 10752
			public class NOUSABLETOILETS
			{
				// Token: 0x0400B0AB RID: 45227
				public static LocString NAME = "Toilet out of order";

				// Token: 0x0400B0AC RID: 45228
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The only ",
					UI.FormatAsLink("Outhouses", "OUTHOUSE"),
					" or ",
					UI.FormatAsLink("Lavatories", "FLUSHTOILET"),
					" in this Duplicant's reach are out of order"
				});

				// Token: 0x0400B0AD RID: 45229
				public static LocString NOTIFICATION_NAME = "Toilet out of order";

				// Token: 0x0400B0AE RID: 45230
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants want to use an ",
					UI.FormatAsLink("Outhouse", "OUTHOUSE"),
					" or ",
					UI.FormatAsLink("Lavatory", "FLUSHTOILET"),
					" that is out of order:"
				});
			}

			// Token: 0x02002A01 RID: 10753
			public class NOTOILETS
			{
				// Token: 0x0400B0AF RID: 45231
				public static LocString NAME = "No Outhouses";

				// Token: 0x0400B0B0 RID: 45232
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"There are no ",
					UI.FormatAsLink("Outhouses", "OUTHOUSE"),
					" available for this Duplicant\n\n",
					UI.FormatAsLink("Outhouses", "OUTHOUSE"),
					" can be built from the ",
					UI.FormatAsBuildMenuTab("Plumbing Tab", global::Action.Plan5)
				});

				// Token: 0x0400B0B1 RID: 45233
				public static LocString NOTIFICATION_NAME = "No Outhouses built";

				// Token: 0x0400B0B2 RID: 45234
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					UI.FormatAsLink("Outhouses", "OUTHOUSE"),
					" can be built from the ",
					UI.FormatAsBuildMenuTab("Plumbing Tab", global::Action.Plan5),
					".\n\nThese Duplicants are in need of an ",
					UI.FormatAsLink("Outhouse", "OUTHOUSE"),
					":"
				});
			}

			// Token: 0x02002A02 RID: 10754
			public class FULLBLADDER
			{
				// Token: 0x0400B0B3 RID: 45235
				public static LocString NAME = "Full bladder";

				// Token: 0x0400B0B4 RID: 45236
				public static LocString TOOLTIP = "This Duplicant would really appreciate an " + UI.FormatAsLink("Outhouse", "OUTHOUSE") + " or " + UI.FormatAsLink("Lavatory", "FLUSHTOILET");
			}

			// Token: 0x02002A03 RID: 10755
			public class STRESSFULLYEMPTYINGBLADDER
			{
				// Token: 0x0400B0B5 RID: 45237
				public static LocString NAME = "Making a mess";

				// Token: 0x0400B0B6 RID: 45238
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This poor Duplicant couldn't find an ",
					UI.FormatAsLink("Outhouse", "OUTHOUSE"),
					" in time and is super embarrassed\n\n",
					UI.FormatAsLink("Outhouses", "OUTHOUSE"),
					" can be built from the ",
					UI.FormatAsBuildMenuTab("Plumbing Tab", global::Action.Plan5)
				});

				// Token: 0x0400B0B7 RID: 45239
				public static LocString NOTIFICATION_NAME = "Made a mess";

				// Token: 0x0400B0B8 RID: 45240
				public static LocString NOTIFICATION_TOOLTIP = "The " + UI.FormatAsTool("Mop Tool", global::Action.Mop) + " can be used to clean up Duplicant-related \"spills\"\n\nThese Duplicants made messes that require cleaning up:\n";
			}

			// Token: 0x02002A04 RID: 10756
			public class WASHINGHANDS
			{
				// Token: 0x0400B0B9 RID: 45241
				public static LocString NAME = "Washing hands";

				// Token: 0x0400B0BA RID: 45242
				public static LocString TOOLTIP = "This Duplicant is washing their hands";
			}

			// Token: 0x02002A05 RID: 10757
			public class SHOWERING
			{
				// Token: 0x0400B0BB RID: 45243
				public static LocString NAME = "Showering";

				// Token: 0x0400B0BC RID: 45244
				public static LocString TOOLTIP = "This Duplicant is gonna be squeaky clean";
			}

			// Token: 0x02002A06 RID: 10758
			public class RELAXING
			{
				// Token: 0x0400B0BD RID: 45245
				public static LocString NAME = "Relaxing";

				// Token: 0x0400B0BE RID: 45246
				public static LocString TOOLTIP = "This Duplicant's just taking it easy";
			}

			// Token: 0x02002A07 RID: 10759
			public class VOMITING
			{
				// Token: 0x0400B0BF RID: 45247
				public static LocString NAME = "Throwing up";

				// Token: 0x0400B0C0 RID: 45248
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant has unceremoniously hurled as the result of a ",
					UI.FormatAsLink("Disease", "DISEASE"),
					"\n\nDuplicant-related \"spills\" can be cleaned up using the ",
					UI.PRE_KEYWORD,
					"Mop Tool",
					UI.PST_KEYWORD,
					" ",
					UI.FormatAsHotKey(global::Action.Mop)
				});

				// Token: 0x0400B0C1 RID: 45249
				public static LocString NOTIFICATION_NAME = "Throwing up";

				// Token: 0x0400B0C2 RID: 45250
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"The ",
					UI.FormatAsTool("Mop Tool", global::Action.Mop),
					" can be used to clean up Duplicant-related \"spills\"\n\nA ",
					UI.PRE_KEYWORD,
					"Disease",
					UI.PST_KEYWORD,
					" has caused these Duplicants to throw up:"
				});
			}

			// Token: 0x02002A08 RID: 10760
			public class STRESSVOMITING
			{
				// Token: 0x0400B0C3 RID: 45251
				public static LocString NAME = "Stress vomiting";

				// Token: 0x0400B0C4 RID: 45252
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is relieving their ",
					UI.PRE_KEYWORD,
					"Stress",
					UI.PST_KEYWORD,
					" all over the floor\n\nDuplicant-related \"spills\" can be cleaned up using the ",
					UI.PRE_KEYWORD,
					"Mop Tool",
					UI.PST_KEYWORD,
					" ",
					UI.FormatAsHotKey(global::Action.Mop)
				});

				// Token: 0x0400B0C5 RID: 45253
				public static LocString NOTIFICATION_NAME = "Stress vomiting";

				// Token: 0x0400B0C6 RID: 45254
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"The ",
					UI.FormatAsTool("Mop Tool", global::Action.Mop),
					" can used to clean up Duplicant-related \"spills\"\n\nThese Duplicants became so ",
					UI.PRE_KEYWORD,
					"Stressed",
					UI.PST_KEYWORD,
					" they threw up:"
				});
			}

			// Token: 0x02002A09 RID: 10761
			public class RADIATIONVOMITING
			{
				// Token: 0x0400B0C7 RID: 45255
				public static LocString NAME = "Radiation vomiting";

				// Token: 0x0400B0C8 RID: 45256
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is sick due to ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" poisoning.\n\nDuplicant-related \"spills\" can be cleaned up using the ",
					UI.PRE_KEYWORD,
					"Mop Tool",
					UI.PST_KEYWORD,
					" ",
					UI.FormatAsHotKey(global::Action.Mop)
				});

				// Token: 0x0400B0C9 RID: 45257
				public static LocString NOTIFICATION_NAME = "Radiation vomiting";

				// Token: 0x0400B0CA RID: 45258
				public static LocString NOTIFICATION_TOOLTIP = "The " + UI.FormatAsTool("Mop Tool", global::Action.Mop) + " can clean up Duplicant-related \"spills\"\n\nRadiation Sickness caused these Duplicants to throw up:";
			}

			// Token: 0x02002A0A RID: 10762
			public class HASDISEASE
			{
				// Token: 0x0400B0CB RID: 45259
				public static LocString NAME = "Feeling ill";

				// Token: 0x0400B0CC RID: 45260
				public static LocString TOOLTIP = "This Duplicant has contracted a " + UI.FormatAsLink("Disease", "DISEASE") + " and requires recovery time at a " + UI.FormatAsLink("Sick Bay", "DOCTORSTATION");

				// Token: 0x0400B0CD RID: 45261
				public static LocString NOTIFICATION_NAME = "Illness";

				// Token: 0x0400B0CE RID: 45262
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants have contracted a ",
					UI.FormatAsLink("Disease", "DISEASE"),
					" and require recovery time at a ",
					UI.FormatAsLink("Sick Bay", "DOCTORSTATION"),
					":"
				});
			}

			// Token: 0x02002A0B RID: 10763
			public class BODYREGULATINGHEATING
			{
				// Token: 0x0400B0CF RID: 45263
				public static LocString NAME = "Regulating temperature at: {TempDelta}";

				// Token: 0x0400B0D0 RID: 45264
				public static LocString TOOLTIP = "This Duplicant is regulating their internal " + UI.PRE_KEYWORD + "Temperature" + UI.PST_KEYWORD;
			}

			// Token: 0x02002A0C RID: 10764
			public class BODYREGULATINGCOOLING
			{
				// Token: 0x0400B0D1 RID: 45265
				public static LocString NAME = "Regulating temperature at: {TempDelta}";

				// Token: 0x0400B0D2 RID: 45266
				public static LocString TOOLTIP = "This Duplicant is regulating their internal " + UI.PRE_KEYWORD + "Temperature" + UI.PST_KEYWORD;
			}

			// Token: 0x02002A0D RID: 10765
			public class BREATHINGO2
			{
				// Token: 0x0400B0D3 RID: 45267
				public static LocString NAME = "Inhaling {ConsumptionRate} O<sub>2</sub>";

				// Token: 0x0400B0D4 RID: 45268
				public static LocString TOOLTIP = "Duplicants require " + UI.FormatAsLink("Oxygen", "OXYGEN") + " to live";
			}

			// Token: 0x02002A0E RID: 10766
			public class EMITTINGCO2
			{
				// Token: 0x0400B0D5 RID: 45269
				public static LocString NAME = "Exhaling {EmittingRate} CO<sub>2</sub>";

				// Token: 0x0400B0D6 RID: 45270
				public static LocString TOOLTIP = "Duplicants breathe out " + UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE");
			}

			// Token: 0x02002A0F RID: 10767
			public class PICKUPDELIVERSTATUS
			{
				// Token: 0x0400B0D7 RID: 45271
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0D8 RID: 45272
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A10 RID: 10768
			public class STOREDELIVERSTATUS
			{
				// Token: 0x0400B0D9 RID: 45273
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0DA RID: 45274
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A11 RID: 10769
			public class CLEARDELIVERSTATUS
			{
				// Token: 0x0400B0DB RID: 45275
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0DC RID: 45276
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A12 RID: 10770
			public class STOREFORBUILDDELIVERSTATUS
			{
				// Token: 0x0400B0DD RID: 45277
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0DE RID: 45278
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A13 RID: 10771
			public class STOREFORBUILDPRIORITIZEDDELIVERSTATUS
			{
				// Token: 0x0400B0DF RID: 45279
				public static LocString NAME = "Allocating {Item} to {Target}";

				// Token: 0x0400B0E0 RID: 45280
				public static LocString TOOLTIP = "This Duplicant is delivering materials to a <b>{Target}</b> construction errand";
			}

			// Token: 0x02002A14 RID: 10772
			public class BUILDDELIVERSTATUS
			{
				// Token: 0x0400B0E1 RID: 45281
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0E2 RID: 45282
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A15 RID: 10773
			public class BUILDPRIORITIZEDSTATUS
			{
				// Token: 0x0400B0E3 RID: 45283
				public static LocString NAME = "Building {Target}";

				// Token: 0x0400B0E4 RID: 45284
				public static LocString TOOLTIP = "This Duplicant is constructing a <b>{Target}</b>";
			}

			// Token: 0x02002A16 RID: 10774
			public class FABRICATEDELIVERSTATUS
			{
				// Token: 0x0400B0E5 RID: 45285
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0E6 RID: 45286
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A17 RID: 10775
			public class USEITEMDELIVERSTATUS
			{
				// Token: 0x0400B0E7 RID: 45287
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0E8 RID: 45288
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A18 RID: 10776
			public class STOREPRIORITYDELIVERSTATUS
			{
				// Token: 0x0400B0E9 RID: 45289
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0EA RID: 45290
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A19 RID: 10777
			public class STORECRITICALDELIVERSTATUS
			{
				// Token: 0x0400B0EB RID: 45291
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0EC RID: 45292
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A1A RID: 10778
			public class COMPOSTFLIPSTATUS
			{
				// Token: 0x0400B0ED RID: 45293
				public static LocString NAME = "Going to flip compost";

				// Token: 0x0400B0EE RID: 45294
				public static LocString TOOLTIP = "This Duplicant is going to flip the " + BUILDINGS.PREFABS.COMPOST.NAME;
			}

			// Token: 0x02002A1B RID: 10779
			public class DECONSTRUCTDELIVERSTATUS
			{
				// Token: 0x0400B0EF RID: 45295
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0F0 RID: 45296
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A1C RID: 10780
			public class TOGGLEDELIVERSTATUS
			{
				// Token: 0x0400B0F1 RID: 45297
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0F2 RID: 45298
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A1D RID: 10781
			public class EMPTYSTORAGEDELIVERSTATUS
			{
				// Token: 0x0400B0F3 RID: 45299
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0F4 RID: 45300
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A1E RID: 10782
			public class HARVESTDELIVERSTATUS
			{
				// Token: 0x0400B0F5 RID: 45301
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0F6 RID: 45302
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A1F RID: 10783
			public class SLEEPDELIVERSTATUS
			{
				// Token: 0x0400B0F7 RID: 45303
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0F8 RID: 45304
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A20 RID: 10784
			public class EATDELIVERSTATUS
			{
				// Token: 0x0400B0F9 RID: 45305
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0FA RID: 45306
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A21 RID: 10785
			public class WARMUPDELIVERSTATUS
			{
				// Token: 0x0400B0FB RID: 45307
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0FC RID: 45308
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A22 RID: 10786
			public class REPAIRDELIVERSTATUS
			{
				// Token: 0x0400B0FD RID: 45309
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B0FE RID: 45310
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A23 RID: 10787
			public class REPAIRWORKSTATUS
			{
				// Token: 0x0400B0FF RID: 45311
				public static LocString NAME = "Repairing {Target}";

				// Token: 0x0400B100 RID: 45312
				public static LocString TOOLTIP = "This Duplicant is fixing the <b>{Target}</b>";
			}

			// Token: 0x02002A24 RID: 10788
			public class BREAKDELIVERSTATUS
			{
				// Token: 0x0400B101 RID: 45313
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B102 RID: 45314
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A25 RID: 10789
			public class BREAKWORKSTATUS
			{
				// Token: 0x0400B103 RID: 45315
				public static LocString NAME = "Breaking {Target}";

				// Token: 0x0400B104 RID: 45316
				public static LocString TOOLTIP = "This Duplicant is going totally bananas on the <b>{Target}</b>!";
			}

			// Token: 0x02002A26 RID: 10790
			public class EQUIPDELIVERSTATUS
			{
				// Token: 0x0400B105 RID: 45317
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B106 RID: 45318
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A27 RID: 10791
			public class COOKDELIVERSTATUS
			{
				// Token: 0x0400B107 RID: 45319
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B108 RID: 45320
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A28 RID: 10792
			public class MUSHDELIVERSTATUS
			{
				// Token: 0x0400B109 RID: 45321
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B10A RID: 45322
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A29 RID: 10793
			public class PACIFYDELIVERSTATUS
			{
				// Token: 0x0400B10B RID: 45323
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B10C RID: 45324
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A2A RID: 10794
			public class RESCUEDELIVERSTATUS
			{
				// Token: 0x0400B10D RID: 45325
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B10E RID: 45326
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A2B RID: 10795
			public class RESCUEWORKSTATUS
			{
				// Token: 0x0400B10F RID: 45327
				public static LocString NAME = "Rescuing {Target}";

				// Token: 0x0400B110 RID: 45328
				public static LocString TOOLTIP = "This Duplicant is saving <b>{Target}</b> from certain peril!";
			}

			// Token: 0x02002A2C RID: 10796
			public class MOPDELIVERSTATUS
			{
				// Token: 0x0400B111 RID: 45329
				public static LocString NAME = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.NAME;

				// Token: 0x0400B112 RID: 45330
				public static LocString TOOLTIP = DUPLICANTS.STATUSITEMS.GENERIC_DELIVER.TOOLTIP;
			}

			// Token: 0x02002A2D RID: 10797
			public class DIGGING
			{
				// Token: 0x0400B113 RID: 45331
				public static LocString NAME = "Digging";

				// Token: 0x0400B114 RID: 45332
				public static LocString TOOLTIP = "This Duplicant is excavating raw resources";
			}

			// Token: 0x02002A2E RID: 10798
			public class EATING
			{
				// Token: 0x0400B115 RID: 45333
				public static LocString NAME = "Eating {Target}";

				// Token: 0x0400B116 RID: 45334
				public static LocString TOOLTIP = "This Duplicant is having a meal";
			}

			// Token: 0x02002A2F RID: 10799
			public class CLEANING
			{
				// Token: 0x0400B117 RID: 45335
				public static LocString NAME = "Cleaning {Target}";

				// Token: 0x0400B118 RID: 45336
				public static LocString TOOLTIP = "This Duplicant is cleaning the <b>{Target}</b>";
			}

			// Token: 0x02002A30 RID: 10800
			public class LIGHTWORKEFFICIENCYBONUS
			{
				// Token: 0x0400B119 RID: 45337
				public static LocString NAME = "Lit Workspace";

				// Token: 0x0400B11A RID: 45338
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Better visibility from the ",
					UI.PRE_KEYWORD,
					"Light",
					UI.PST_KEYWORD,
					" is allowing this Duplicant to work faster:\n    {0}"
				});

				// Token: 0x0400B11B RID: 45339
				public static LocString NO_BUILDING_WORK_ATTRIBUTE = "{0} Speed";
			}

			// Token: 0x02002A31 RID: 10801
			public class LABORATORYWORKEFFICIENCYBONUS
			{
				// Token: 0x0400B11C RID: 45340
				public static LocString NAME = "Lab Workspace";

				// Token: 0x0400B11D RID: 45341
				public static LocString TOOLTIP = "Working in a Laboratory is allowing this Duplicant to work faster:\n    {0}";

				// Token: 0x0400B11E RID: 45342
				public static LocString NO_BUILDING_WORK_ATTRIBUTE = "{0} Speed";
			}

			// Token: 0x02002A32 RID: 10802
			public class PICKINGUP
			{
				// Token: 0x0400B11F RID: 45343
				public static LocString NAME = "Picking up {Target}";

				// Token: 0x0400B120 RID: 45344
				public static LocString TOOLTIP = "This Duplicant is retrieving <b>{Target}</b>";
			}

			// Token: 0x02002A33 RID: 10803
			public class MOPPING
			{
				// Token: 0x0400B121 RID: 45345
				public static LocString NAME = "Mopping";

				// Token: 0x0400B122 RID: 45346
				public static LocString TOOLTIP = "This Duplicant is cleaning up a nasty spill";
			}

			// Token: 0x02002A34 RID: 10804
			public class ARTING
			{
				// Token: 0x0400B123 RID: 45347
				public static LocString NAME = "Decorating";

				// Token: 0x0400B124 RID: 45348
				public static LocString TOOLTIP = "This Duplicant is hard at work on their art";
			}

			// Token: 0x02002A35 RID: 10805
			public class MUSHING
			{
				// Token: 0x0400B125 RID: 45349
				public static LocString NAME = "Mushing {Item}";

				// Token: 0x0400B126 RID: 45350
				public static LocString TOOLTIP = "This Duplicant is cooking a <b>{Item}</b>";
			}

			// Token: 0x02002A36 RID: 10806
			public class COOKING
			{
				// Token: 0x0400B127 RID: 45351
				public static LocString NAME = "Cooking {Item}";

				// Token: 0x0400B128 RID: 45352
				public static LocString TOOLTIP = "This Duplicant is cooking up a tasty <b>{Item}</b>";
			}

			// Token: 0x02002A37 RID: 10807
			public class RESEARCHING
			{
				// Token: 0x0400B129 RID: 45353
				public static LocString NAME = "Researching {Tech}";

				// Token: 0x0400B12A RID: 45354
				public static LocString TOOLTIP = "This Duplicant is intently researching <b>{Tech}</b> technology";
			}

			// Token: 0x02002A38 RID: 10808
			public class MISSIONCONTROLLING
			{
				// Token: 0x0400B12B RID: 45355
				public static LocString NAME = "Mission Controlling";

				// Token: 0x0400B12C RID: 45356
				public static LocString TOOLTIP = "This Duplicant is guiding a " + UI.PRE_KEYWORD + "Rocket" + UI.PST_KEYWORD;
			}

			// Token: 0x02002A39 RID: 10809
			public class STORING
			{
				// Token: 0x0400B12D RID: 45357
				public static LocString NAME = "Storing {Item}";

				// Token: 0x0400B12E RID: 45358
				public static LocString TOOLTIP = "This Duplicant is putting <b>{Item}</b> away in <b>{Target}</b>";
			}

			// Token: 0x02002A3A RID: 10810
			public class BUILDING
			{
				// Token: 0x0400B12F RID: 45359
				public static LocString NAME = "Building {Target}";

				// Token: 0x0400B130 RID: 45360
				public static LocString TOOLTIP = "This Duplicant is constructing a <b>{Target}</b>";
			}

			// Token: 0x02002A3B RID: 10811
			public class EQUIPPING
			{
				// Token: 0x0400B131 RID: 45361
				public static LocString NAME = "Equipping {Target}";

				// Token: 0x0400B132 RID: 45362
				public static LocString TOOLTIP = "This Duplicant is equipping a <b>{Target}</b>";
			}

			// Token: 0x02002A3C RID: 10812
			public class WARMINGUP
			{
				// Token: 0x0400B133 RID: 45363
				public static LocString NAME = "Warming up";

				// Token: 0x0400B134 RID: 45364
				public static LocString TOOLTIP = "This Duplicant got too cold and is trying to warm up";
			}

			// Token: 0x02002A3D RID: 10813
			public class GENERATINGPOWER
			{
				// Token: 0x0400B135 RID: 45365
				public static LocString NAME = "Generating power";

				// Token: 0x0400B136 RID: 45366
				public static LocString TOOLTIP = "This Duplicant is using the <b>{Target}</b> to produce electrical " + UI.PRE_KEYWORD + "Power" + UI.PST_KEYWORD;
			}

			// Token: 0x02002A3E RID: 10814
			public class HARVESTING
			{
				// Token: 0x0400B137 RID: 45367
				public static LocString NAME = "Harvesting {Target}";

				// Token: 0x0400B138 RID: 45368
				public static LocString TOOLTIP = "This Duplicant is gathering resources from a <b>{Target}</b>";
			}

			// Token: 0x02002A3F RID: 10815
			public class UPROOTING
			{
				// Token: 0x0400B139 RID: 45369
				public static LocString NAME = "Uprooting {Target}";

				// Token: 0x0400B13A RID: 45370
				public static LocString TOOLTIP = "This Duplicant is digging up a <b>{Target}</b>";
			}

			// Token: 0x02002A40 RID: 10816
			public class EMPTYING
			{
				// Token: 0x0400B13B RID: 45371
				public static LocString NAME = "Emptying {Target}";

				// Token: 0x0400B13C RID: 45372
				public static LocString TOOLTIP = "This Duplicant is removing materials from the <b>{Target}</b>";
			}

			// Token: 0x02002A41 RID: 10817
			public class TOGGLING
			{
				// Token: 0x0400B13D RID: 45373
				public static LocString NAME = "Change {Target} setting";

				// Token: 0x0400B13E RID: 45374
				public static LocString TOOLTIP = "This Duplicant is changing the <b>{Target}</b>'s setting";
			}

			// Token: 0x02002A42 RID: 10818
			public class DECONSTRUCTING
			{
				// Token: 0x0400B13F RID: 45375
				public static LocString NAME = "Deconstructing {Target}";

				// Token: 0x0400B140 RID: 45376
				public static LocString TOOLTIP = "This Duplicant is deconstructing the <b>{Target}</b>";
			}

			// Token: 0x02002A43 RID: 10819
			public class DEMOLISHING
			{
				// Token: 0x0400B141 RID: 45377
				public static LocString NAME = "Demolishing {Target}";

				// Token: 0x0400B142 RID: 45378
				public static LocString TOOLTIP = "This Duplicant is demolishing the <b>{Target}</b>";
			}

			// Token: 0x02002A44 RID: 10820
			public class DISINFECTING
			{
				// Token: 0x0400B143 RID: 45379
				public static LocString NAME = "Disinfecting {Target}";

				// Token: 0x0400B144 RID: 45380
				public static LocString TOOLTIP = "This Duplicant is disinfecting <b>{Target}</b>";
			}

			// Token: 0x02002A45 RID: 10821
			public class FABRICATING
			{
				// Token: 0x0400B145 RID: 45381
				public static LocString NAME = "Fabricating {Item}";

				// Token: 0x0400B146 RID: 45382
				public static LocString TOOLTIP = "This Duplicant is crafting a <b>{Item}</b>";
			}

			// Token: 0x02002A46 RID: 10822
			public class PROCESSING
			{
				// Token: 0x0400B147 RID: 45383
				public static LocString NAME = "Refining {Item}";

				// Token: 0x0400B148 RID: 45384
				public static LocString TOOLTIP = "This Duplicant is refining <b>{Item}</b>";
			}

			// Token: 0x02002A47 RID: 10823
			public class SPICING
			{
				// Token: 0x0400B149 RID: 45385
				public static LocString NAME = "Spicing Food";

				// Token: 0x0400B14A RID: 45386
				public static LocString TOOLTIP = "This Duplicant is making a tasty meal even tastier";
			}

			// Token: 0x02002A48 RID: 10824
			public class CLEARING
			{
				// Token: 0x0400B14B RID: 45387
				public static LocString NAME = "Sweeping {Target}";

				// Token: 0x0400B14C RID: 45388
				public static LocString TOOLTIP = "This Duplicant is sweeping away <b>{Target}</b>";
			}

			// Token: 0x02002A49 RID: 10825
			public class STUDYING
			{
				// Token: 0x0400B14D RID: 45389
				public static LocString NAME = "Analyzing";

				// Token: 0x0400B14E RID: 45390
				public static LocString TOOLTIP = "This Duplicant is conducting a field study of a Natural Feature";
			}

			// Token: 0x02002A4A RID: 10826
			public class SOCIALIZING
			{
				// Token: 0x0400B14F RID: 45391
				public static LocString NAME = "Socializing";

				// Token: 0x0400B150 RID: 45392
				public static LocString TOOLTIP = "This Duplicant is using their break to hang out";
			}

			// Token: 0x02002A4B RID: 10827
			public class MINGLING
			{
				// Token: 0x0400B151 RID: 45393
				public static LocString NAME = "Mingling";

				// Token: 0x0400B152 RID: 45394
				public static LocString TOOLTIP = "This Duplicant is using their break to chat with friends";
			}

			// Token: 0x02002A4C RID: 10828
			public class NOISEPEACEFUL
			{
				// Token: 0x0400B153 RID: 45395
				public static LocString NAME = "Peace and Quiet";

				// Token: 0x0400B154 RID: 45396
				public static LocString TOOLTIP = "This Duplicant has found a quiet place to concentrate";
			}

			// Token: 0x02002A4D RID: 10829
			public class NOISEMINOR
			{
				// Token: 0x0400B155 RID: 45397
				public static LocString NAME = "Loud Noises";

				// Token: 0x0400B156 RID: 45398
				public static LocString TOOLTIP = "This area is a bit too loud for comfort";
			}

			// Token: 0x02002A4E RID: 10830
			public class NOISEMAJOR
			{
				// Token: 0x0400B157 RID: 45399
				public static LocString NAME = "Cacophony!";

				// Token: 0x0400B158 RID: 45400
				public static LocString TOOLTIP = "It's very, very loud in here!";
			}

			// Token: 0x02002A4F RID: 10831
			public class LOWIMMUNITY
			{
				// Token: 0x0400B159 RID: 45401
				public static LocString NAME = "Under the Weather";

				// Token: 0x0400B15A RID: 45402
				public static LocString TOOLTIP = "This Duplicant has a weakened immune system and will become ill if it reaches zero";

				// Token: 0x0400B15B RID: 45403
				public static LocString NOTIFICATION_NAME = "Low Immunity";

				// Token: 0x0400B15C RID: 45404
				public static LocString NOTIFICATION_TOOLTIP = "These Duplicants are at risk of becoming sick:";
			}

			// Token: 0x02002A50 RID: 10832
			public abstract class TINKERING
			{
				// Token: 0x0400B15D RID: 45405
				public static LocString NAME = "Tinkering";

				// Token: 0x0400B15E RID: 45406
				public static LocString TOOLTIP = "This Duplicant is making functional improvements to a building";
			}

			// Token: 0x02002A51 RID: 10833
			public class CONTACTWITHGERMS
			{
				// Token: 0x0400B15F RID: 45407
				public static LocString NAME = "Contact with {Sickness} Germs";

				// Token: 0x0400B160 RID: 45408
				public static LocString TOOLTIP = "This Duplicant has encountered {Sickness} Germs and is at risk of dangerous exposure if contact continues\n\n<i>" + UI.CLICK(UI.ClickType.Click) + " to jump to last contact location</i>";
			}

			// Token: 0x02002A52 RID: 10834
			public class EXPOSEDTOGERMS
			{
				// Token: 0x0400B161 RID: 45409
				public static LocString TIER1 = "Mild Exposure";

				// Token: 0x0400B162 RID: 45410
				public static LocString TIER2 = "Medium Exposure";

				// Token: 0x0400B163 RID: 45411
				public static LocString TIER3 = "Exposure";

				// Token: 0x0400B164 RID: 45412
				public static readonly LocString[] EXPOSURE_TIERS = new LocString[]
				{
					DUPLICANTS.STATUSITEMS.EXPOSEDTOGERMS.TIER1,
					DUPLICANTS.STATUSITEMS.EXPOSEDTOGERMS.TIER2,
					DUPLICANTS.STATUSITEMS.EXPOSEDTOGERMS.TIER3
				};

				// Token: 0x0400B165 RID: 45413
				public static LocString NAME = "{Severity} to {Sickness} Germs";

				// Token: 0x0400B166 RID: 45414
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant has been exposed to a concentration of {Sickness} Germs and is at risk of waking up sick on their next shift\n\nExposed {Source}\n\nRate of Contracting {Sickness}: {Chance}\n\nResistance Rating: {Total}\n    • Base {Sickness} Resistance: {Base}\n    • ",
					DUPLICANTS.ATTRIBUTES.GERMRESISTANCE.NAME,
					": {Dupe}\n    • {Severity} Exposure: {ExposureLevelBonus}\n\n<i>",
					UI.CLICK(UI.ClickType.Click),
					" to jump to last exposure location</i>"
				});
			}

			// Token: 0x02002A53 RID: 10835
			public class GASLIQUIDEXPOSURE
			{
				// Token: 0x0400B167 RID: 45415
				public static LocString NAME_MINOR = "Eye Irritation";

				// Token: 0x0400B168 RID: 45416
				public static LocString NAME_MAJOR = "Major Eye Irritation";

				// Token: 0x0400B169 RID: 45417
				public static LocString TOOLTIP = "Ah, it stings!\n\nThis poor Duplicant got a faceful of an irritating gas or liquid";

				// Token: 0x0400B16A RID: 45418
				public static LocString TOOLTIP_EXPOSED = "Current exposure to {element} is {rate} eye irritation";

				// Token: 0x0400B16B RID: 45419
				public static LocString TOOLTIP_RATE_INCREASE = "increasing";

				// Token: 0x0400B16C RID: 45420
				public static LocString TOOLTIP_RATE_DECREASE = "decreasing";

				// Token: 0x0400B16D RID: 45421
				public static LocString TOOLTIP_RATE_STAYS = "maintaining";

				// Token: 0x0400B16E RID: 45422
				public static LocString TOOLTIP_EXPOSURE_LEVEL = "Time Remaining: {time}";
			}

			// Token: 0x02002A54 RID: 10836
			public class BEINGPRODUCTIVE
			{
				// Token: 0x0400B16F RID: 45423
				public static LocString NAME = "Super Focused";

				// Token: 0x0400B170 RID: 45424
				public static LocString TOOLTIP = "This Duplicant is focused on being super productive right now";
			}

			// Token: 0x02002A55 RID: 10837
			public class BALLOONARTISTPLANNING
			{
				// Token: 0x0400B171 RID: 45425
				public static LocString NAME = "Balloon Artist";

				// Token: 0x0400B172 RID: 45426
				public static LocString TOOLTIP = "This Duplicant is planning to hand out balloons in their downtime";
			}

			// Token: 0x02002A56 RID: 10838
			public class BALLOONARTISTHANDINGOUT
			{
				// Token: 0x0400B173 RID: 45427
				public static LocString NAME = "Balloon Artist";

				// Token: 0x0400B174 RID: 45428
				public static LocString TOOLTIP = "This Duplicant is handing out balloons to other Duplicants";
			}

			// Token: 0x02002A57 RID: 10839
			public class EXPELLINGRADS
			{
				// Token: 0x0400B175 RID: 45429
				public static LocString NAME = "Cleansing Rads";

				// Token: 0x0400B176 RID: 45430
				public static LocString TOOLTIP = "This Duplicant is, uh... \"expelling\" absorbed radiation from their system";
			}

			// Token: 0x02002A58 RID: 10840
			public class ANALYZINGGENES
			{
				// Token: 0x0400B177 RID: 45431
				public static LocString NAME = "Analyzing Plant Genes";

				// Token: 0x0400B178 RID: 45432
				public static LocString TOOLTIP = "This Duplicant is peering deep into the genetic code of an odd seed";
			}

			// Token: 0x02002A59 RID: 10841
			public class ANALYZINGARTIFACT
			{
				// Token: 0x0400B179 RID: 45433
				public static LocString NAME = "Analyzing Artifact";

				// Token: 0x0400B17A RID: 45434
				public static LocString TOOLTIP = "This Duplicant is studying an artifact";
			}

			// Token: 0x02002A5A RID: 10842
			public class RANCHING
			{
				// Token: 0x0400B17B RID: 45435
				public static LocString NAME = "Ranching";

				// Token: 0x0400B17C RID: 45436
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is tending to a ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					"'s well-being"
				});
			}
		}

		// Token: 0x02001CE5 RID: 7397
		public class DISEASES
		{
			// Token: 0x04008427 RID: 33831
			public static LocString CURED_POPUP = "Cured of {0}";

			// Token: 0x04008428 RID: 33832
			public static LocString INFECTED_POPUP = "Became infected by {0}";

			// Token: 0x04008429 RID: 33833
			public static LocString ADDED_POPFX = "{0}: {1} Germs";

			// Token: 0x0400842A RID: 33834
			public static LocString NOTIFICATION_TOOLTIP = "{0} contracted {1} from: {2}";

			// Token: 0x0400842B RID: 33835
			public static LocString GERMS = "Germs";

			// Token: 0x0400842C RID: 33836
			public static LocString GERMS_CONSUMED_DESCRIPTION = "A count of the number of germs this Duplicant is host to";

			// Token: 0x0400842D RID: 33837
			public static LocString RECUPERATING = "Recuperating";

			// Token: 0x0400842E RID: 33838
			public static LocString INFECTION_MODIFIER = "Recently consumed {0} ({1})";

			// Token: 0x0400842F RID: 33839
			public static LocString INFECTION_MODIFIER_SOURCE = "Fighting off {0} from {1}";

			// Token: 0x04008430 RID: 33840
			public static LocString INFECTED_MODIFIER = "Suppressed immune system";

			// Token: 0x04008431 RID: 33841
			public static LocString LEGEND_POSTAMBLE = "\n•  Select an infected object for more details";

			// Token: 0x04008432 RID: 33842
			public static LocString ATTRIBUTE_MODIFIER_SYMPTOMS = "{0}: {1}";

			// Token: 0x04008433 RID: 33843
			public static LocString ATTRIBUTE_MODIFIER_SYMPTOMS_TOOLTIP = "Modifies {0} by {1}";

			// Token: 0x04008434 RID: 33844
			public static LocString DEATH_SYMPTOM = "Death in {0} if untreated";

			// Token: 0x04008435 RID: 33845
			public static LocString DEATH_SYMPTOM_TOOLTIP = "Without medical treatment, this Duplicant will die of their illness in {0}";

			// Token: 0x04008436 RID: 33846
			public static LocString RESISTANCES_PANEL_TOOLTIP = "{0}";

			// Token: 0x04008437 RID: 33847
			public static LocString IMMUNE_FROM_MISSING_REQUIRED_TRAIT = "Immune: Does not have {0}";

			// Token: 0x04008438 RID: 33848
			public static LocString IMMUNE_FROM_HAVING_EXLCLUDED_TRAIT = "Immune: Has {0}";

			// Token: 0x04008439 RID: 33849
			public static LocString IMMUNE_FROM_HAVING_EXCLUDED_EFFECT = "Immunity: Has {0}";

			// Token: 0x0400843A RID: 33850
			public static LocString CONTRACTION_PROBABILITY = "{0} of {1}'s exposures to these germs will result in {2}";

			// Token: 0x02002A5B RID: 10843
			public class STATUS_ITEM_TOOLTIP
			{
				// Token: 0x0400B17D RID: 45437
				public static LocString TEMPLATE = "{InfectionSource}{Duration}{Doctor}{Fatality}{Cures}{Bedrest}\n\n\n{Symptoms}";

				// Token: 0x0400B17E RID: 45438
				public static LocString DESCRIPTOR = "<b>{0} {1}</b>\n";

				// Token: 0x0400B17F RID: 45439
				public static LocString SYMPTOMS = "{0}\n";

				// Token: 0x0400B180 RID: 45440
				public static LocString INFECTION_SOURCE = "Contracted by: {0}\n";

				// Token: 0x0400B181 RID: 45441
				public static LocString DURATION = "Time to recovery: {0}\n";

				// Token: 0x0400B182 RID: 45442
				public static LocString CURES = "Remedies taken: {0}\n";

				// Token: 0x0400B183 RID: 45443
				public static LocString NOMEDICINETAKEN = "Remedies taken: None\n";

				// Token: 0x0400B184 RID: 45444
				public static LocString FATALITY = "Fatal if untreated in: {0}\n";

				// Token: 0x0400B185 RID: 45445
				public static LocString BEDREST = "Sick Bay assignment will allow faster recovery\n";

				// Token: 0x0400B186 RID: 45446
				public static LocString DOCTOR_REQUIRED = "Sick Bay assignment required for recovery\n";

				// Token: 0x0400B187 RID: 45447
				public static LocString DOCTORED = "Received medical treatment, recovery speed is increased\n";
			}

			// Token: 0x02002A5C RID: 10844
			public class MEDICINE
			{
				// Token: 0x0400B188 RID: 45448
				public static LocString SELF_ADMINISTERED_BOOSTER = "Self-Administered: Anytime";

				// Token: 0x0400B189 RID: 45449
				public static LocString SELF_ADMINISTERED_BOOSTER_TOOLTIP = "Duplicants can give themselves this medicine, whether they are currently sick or not";

				// Token: 0x0400B18A RID: 45450
				public static LocString SELF_ADMINISTERED_CURE = "Self-Administered: Sick Only";

				// Token: 0x0400B18B RID: 45451
				public static LocString SELF_ADMINISTERED_CURE_TOOLTIP = "Duplicants can give themselves this medicine, but only while they are sick";

				// Token: 0x0400B18C RID: 45452
				public static LocString DOCTOR_ADMINISTERED_BOOSTER = "Doctor Administered: Anytime";

				// Token: 0x0400B18D RID: 45453
				public static LocString DOCTOR_ADMINISTERED_BOOSTER_TOOLTIP = "Duplicants can receive this medicine at a {Station}, whether they are currently sick or not\n\nThey cannot give it to themselves and must receive it from a friend with " + UI.PRE_KEYWORD + "Doctoring Skills" + UI.PST_KEYWORD;

				// Token: 0x0400B18E RID: 45454
				public static LocString DOCTOR_ADMINISTERED_CURE = "Doctor Administered: Sick Only";

				// Token: 0x0400B18F RID: 45455
				public static LocString DOCTOR_ADMINISTERED_CURE_TOOLTIP = "Duplicants can receive this medicine at a {Station}, but only while they are sick\n\nThey cannot give it to themselves and must receive it from a friend with " + UI.PRE_KEYWORD + "Doctoring Skills" + UI.PST_KEYWORD;

				// Token: 0x0400B190 RID: 45456
				public static LocString BOOSTER = UI.FormatAsLink("Immune Booster", "IMMUNE SYSTEM");

				// Token: 0x0400B191 RID: 45457
				public static LocString BOOSTER_TOOLTIP = "Boosters can be taken by both healthy and sick Duplicants to prevent potential disease";

				// Token: 0x0400B192 RID: 45458
				public static LocString CURES_ANY = "Alleviates " + UI.FormatAsLink("All Diseases", "DISEASE");

				// Token: 0x0400B193 RID: 45459
				public static LocString CURES_ANY_TOOLTIP = string.Concat(new string[]
				{
					"This is a nonspecific ",
					UI.PRE_KEYWORD,
					"Disease",
					UI.PST_KEYWORD,
					" treatment that can be taken by any sick Duplicant"
				});

				// Token: 0x0400B194 RID: 45460
				public static LocString CURES = "Alleviates {0}";

				// Token: 0x0400B195 RID: 45461
				public static LocString CURES_TOOLTIP = "This medicine is used to treat {0} and can only be taken by sick Duplicants";
			}

			// Token: 0x02002A5D RID: 10845
			public class SEVERITY
			{
				// Token: 0x0400B196 RID: 45462
				public static LocString BENIGN = "Benign";

				// Token: 0x0400B197 RID: 45463
				public static LocString MINOR = "Minor";

				// Token: 0x0400B198 RID: 45464
				public static LocString MAJOR = "Major";

				// Token: 0x0400B199 RID: 45465
				public static LocString CRITICAL = "Critical";
			}

			// Token: 0x02002A5E RID: 10846
			public class TYPE
			{
				// Token: 0x0400B19A RID: 45466
				public static LocString PATHOGEN = "Illness";

				// Token: 0x0400B19B RID: 45467
				public static LocString AILMENT = "Ailment";

				// Token: 0x0400B19C RID: 45468
				public static LocString INJURY = "Injury";
			}

			// Token: 0x02002A5F RID: 10847
			public class TRIGGERS
			{
				// Token: 0x0400B19D RID: 45469
				public static LocString EATCOMPLETEEDIBLE = "May cause {Diseases}";

				// Token: 0x02002FC5 RID: 12229
				public class TOOLTIPS
				{
					// Token: 0x0400BF04 RID: 48900
					public static LocString EATCOMPLETEEDIBLE = "May cause {Diseases}";
				}
			}

			// Token: 0x02002A60 RID: 10848
			public class INFECTIONSOURCES
			{
				// Token: 0x0400B19E RID: 45470
				public static LocString INTERNAL_TEMPERATURE = "Extreme internal temperatures";

				// Token: 0x0400B19F RID: 45471
				public static LocString TOXIC_AREA = "Exposure to toxic areas";

				// Token: 0x0400B1A0 RID: 45472
				public static LocString FOOD = "Eating a germ-covered {0}";

				// Token: 0x0400B1A1 RID: 45473
				public static LocString AIR = "Breathing germ-filled {0}";

				// Token: 0x0400B1A2 RID: 45474
				public static LocString SKIN = "Skin contamination";

				// Token: 0x0400B1A3 RID: 45475
				public static LocString UNKNOWN = "Unknown source";
			}

			// Token: 0x02002A61 RID: 10849
			public class DESCRIPTORS
			{
				// Token: 0x02002FC6 RID: 12230
				public class INFO
				{
					// Token: 0x0400BF05 RID: 48901
					public static LocString FOODBORNE = "Contracted via ingestion\n" + UI.HORIZONTAL_RULE;

					// Token: 0x0400BF06 RID: 48902
					public static LocString FOODBORNE_TOOLTIP = string.Concat(new string[]
					{
						"Duplicants may contract this ",
						UI.PRE_KEYWORD,
						"Disease",
						UI.PST_KEYWORD,
						" by ingesting ",
						UI.PRE_KEYWORD,
						"Food",
						UI.PST_KEYWORD,
						" contaminated with these ",
						UI.PRE_KEYWORD,
						"Germs",
						UI.PST_KEYWORD
					});

					// Token: 0x0400BF07 RID: 48903
					public static LocString AIRBORNE = "Contracted via inhalation\n" + UI.HORIZONTAL_RULE;

					// Token: 0x0400BF08 RID: 48904
					public static LocString AIRBORNE_TOOLTIP = string.Concat(new string[]
					{
						"Duplicants may contract this ",
						UI.PRE_KEYWORD,
						"Disease",
						UI.PST_KEYWORD,
						" by breathing ",
						ELEMENTS.OXYGEN.NAME,
						" containing these ",
						UI.PRE_KEYWORD,
						"Germs",
						UI.PST_KEYWORD
					});

					// Token: 0x0400BF09 RID: 48905
					public static LocString SKINBORNE = "Contracted via physical contact\n" + UI.HORIZONTAL_RULE;

					// Token: 0x0400BF0A RID: 48906
					public static LocString SKINBORNE_TOOLTIP = string.Concat(new string[]
					{
						"Duplicants may contract this ",
						UI.PRE_KEYWORD,
						"Disease",
						UI.PST_KEYWORD,
						" by touching objects contaminated with these ",
						UI.PRE_KEYWORD,
						"Germs",
						UI.PST_KEYWORD
					});

					// Token: 0x0400BF0B RID: 48907
					public static LocString SUNBORNE = "Contracted via environmental exposure\n" + UI.HORIZONTAL_RULE;

					// Token: 0x0400BF0C RID: 48908
					public static LocString SUNBORNE_TOOLTIP = string.Concat(new string[]
					{
						"Duplicants may contract this ",
						UI.PRE_KEYWORD,
						"Disease",
						UI.PST_KEYWORD,
						" through exposure to hazardous environments"
					});

					// Token: 0x0400BF0D RID: 48909
					public static LocString GROWS_ON = "Multiplies in:";

					// Token: 0x0400BF0E RID: 48910
					public static LocString GROWS_ON_TOOLTIP = string.Concat(new string[]
					{
						"These substances allow ",
						UI.PRE_KEYWORD,
						"Germs",
						UI.PST_KEYWORD,
						" to spread and reproduce"
					});

					// Token: 0x0400BF0F RID: 48911
					public static LocString NEUTRAL_ON = "Survives in:";

					// Token: 0x0400BF10 RID: 48912
					public static LocString NEUTRAL_ON_TOOLTIP = UI.PRE_KEYWORD + "Germs" + UI.PST_KEYWORD + " will survive contact with these substances, but will not reproduce";

					// Token: 0x0400BF11 RID: 48913
					public static LocString DIES_SLOWLY_ON = "Inhibited by:";

					// Token: 0x0400BF12 RID: 48914
					public static LocString DIES_SLOWLY_ON_TOOLTIP = string.Concat(new string[]
					{
						"Contact with these substances will slowly reduce ",
						UI.PRE_KEYWORD,
						"Germ",
						UI.PST_KEYWORD,
						" numbers"
					});

					// Token: 0x0400BF13 RID: 48915
					public static LocString DIES_ON = "Killed by:";

					// Token: 0x0400BF14 RID: 48916
					public static LocString DIES_ON_TOOLTIP = string.Concat(new string[]
					{
						"Contact with these substances kills ",
						UI.PRE_KEYWORD,
						"Germs",
						UI.PST_KEYWORD,
						" over time"
					});

					// Token: 0x0400BF15 RID: 48917
					public static LocString DIES_QUICKLY_ON = "Disinfected by:";

					// Token: 0x0400BF16 RID: 48918
					public static LocString DIES_QUICKLY_ON_TOOLTIP = "Contact with these substances will quickly kill these " + UI.PRE_KEYWORD + "Germs" + UI.PST_KEYWORD;

					// Token: 0x0400BF17 RID: 48919
					public static LocString GROWS = "Multiplies";

					// Token: 0x0400BF18 RID: 48920
					public static LocString GROWS_TOOLTIP = "Doubles germ count every {0}";

					// Token: 0x0400BF19 RID: 48921
					public static LocString NEUTRAL = "Survives";

					// Token: 0x0400BF1A RID: 48922
					public static LocString NEUTRAL_TOOLTIP = "Germ count remains static";

					// Token: 0x0400BF1B RID: 48923
					public static LocString DIES_SLOWLY = "Inhibited";

					// Token: 0x0400BF1C RID: 48924
					public static LocString DIES_SLOWLY_TOOLTIP = "Halves germ count every {0}";

					// Token: 0x0400BF1D RID: 48925
					public static LocString DIES = "Dies";

					// Token: 0x0400BF1E RID: 48926
					public static LocString DIES_TOOLTIP = "Halves germ count every {0}";

					// Token: 0x0400BF1F RID: 48927
					public static LocString DIES_QUICKLY = "Disinfected";

					// Token: 0x0400BF20 RID: 48928
					public static LocString DIES_QUICKLY_TOOLTIP = "Halves germ count every {0}";

					// Token: 0x0400BF21 RID: 48929
					public static LocString GROWTH_FORMAT = "    • {0}";

					// Token: 0x0400BF22 RID: 48930
					public static LocString TEMPERATURE_RANGE = "Temperature range: {0} to {1}";

					// Token: 0x0400BF23 RID: 48931
					public static LocString TEMPERATURE_RANGE_TOOLTIP = string.Concat(new string[]
					{
						"These ",
						UI.PRE_KEYWORD,
						"Germs",
						UI.PST_KEYWORD,
						" can survive ",
						UI.PRE_KEYWORD,
						"Temperatures",
						UI.PST_KEYWORD,
						" between <b>{0}</b> and <b>{1}</b>\n\nThey thrive in ",
						UI.PRE_KEYWORD,
						"Temperatures",
						UI.PST_KEYWORD,
						" between <b>{2}</b> and <b>{3}</b>"
					});

					// Token: 0x0400BF24 RID: 48932
					public static LocString PRESSURE_RANGE = "Pressure range: {0} to {1}\n";

					// Token: 0x0400BF25 RID: 48933
					public static LocString PRESSURE_RANGE_TOOLTIP = string.Concat(new string[]
					{
						"These ",
						UI.PRE_KEYWORD,
						"Germs",
						UI.PST_KEYWORD,
						" can survive between <b>{0}</b> and <b>{1}</b> of pressure\n\nThey thrive in pressures between <b>{2}</b> and <b>{3}</b>"
					});
				}
			}

			// Token: 0x02002A62 RID: 10850
			public class ALLDISEASES
			{
				// Token: 0x0400B1A4 RID: 45476
				public static LocString NAME = "All Diseases";
			}

			// Token: 0x02002A63 RID: 10851
			public class NODISEASES
			{
				// Token: 0x0400B1A5 RID: 45477
				public static LocString NAME = "NO";
			}

			// Token: 0x02002A64 RID: 10852
			public class FOODPOISONING
			{
				// Token: 0x0400B1A6 RID: 45478
				public static LocString NAME = UI.FormatAsLink("Food Poisoning", "FOODPOISONING");

				// Token: 0x0400B1A7 RID: 45479
				public static LocString LEGEND_HOVERTEXT = "Food Poisoning Germs present\n";

				// Token: 0x0400B1A8 RID: 45480
				public static LocString DESC = "Food and drinks tainted with Food Poisoning germs are unsafe to consume, as they cause vomiting and other...bodily unpleasantness.";
			}

			// Token: 0x02002A65 RID: 10853
			public class SLIMELUNG
			{
				// Token: 0x0400B1A9 RID: 45481
				public static LocString NAME = UI.FormatAsLink("Slimelung", "SLIMELUNG");

				// Token: 0x0400B1AA RID: 45482
				public static LocString LEGEND_HOVERTEXT = "Slimelung Germs present\n";

				// Token: 0x0400B1AB RID: 45483
				public static LocString DESC = string.Concat(new string[]
				{
					"Slimelung germs are found in ",
					UI.FormatAsLink("Slime", "SLIMEMOLD"),
					" and ",
					UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN"),
					". Inhaling these germs can cause Duplicants to cough and struggle to breathe."
				});
			}

			// Token: 0x02002A66 RID: 10854
			public class POLLENGERMS
			{
				// Token: 0x0400B1AC RID: 45484
				public static LocString NAME = UI.FormatAsLink("Floral Scent", "POLLENGERMS");

				// Token: 0x0400B1AD RID: 45485
				public static LocString LEGEND_HOVERTEXT = "Floral Scent allergens present\n";

				// Token: 0x0400B1AE RID: 45486
				public static LocString DESC = "Floral Scent allergens trigger excessive sneezing fits in Duplicants who possess the Allergies trait.";
			}

			// Token: 0x02002A67 RID: 10855
			public class ZOMBIESPORES
			{
				// Token: 0x0400B1AF RID: 45487
				public static LocString NAME = UI.FormatAsLink("Zombie Spores", "ZOMBIESPORES");

				// Token: 0x0400B1B0 RID: 45488
				public static LocString LEGEND_HOVERTEXT = "Zombie Spores present\n";

				// Token: 0x0400B1B1 RID: 45489
				public static LocString DESC = "Zombie Spores are a parasitic brain fungus released by " + UI.FormatAsLink("Sporechids", "EVIL_FLOWER") + ". Duplicants who touch or inhale the spores risk becoming infected and temporarily losing motor control.";
			}

			// Token: 0x02002A68 RID: 10856
			public class RADIATIONPOISONING
			{
				// Token: 0x0400B1B2 RID: 45490
				public static LocString NAME = UI.FormatAsLink("Radioactive Contamination", "RADIATIONPOISONING");

				// Token: 0x0400B1B3 RID: 45491
				public static LocString LEGEND_HOVERTEXT = "Radioactive contamination present\n";

				// Token: 0x0400B1B4 RID: 45492
				public static LocString DESC = string.Concat(new string[]
				{
					"Items tainted with Radioactive Contaminants emit low levels of ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					" that can cause ",
					UI.FormatAsLink("Radiation Sickness", "RADIATIONSICKNESS"),
					". They are unaffected by pressure or temperature, but do degrade over time."
				});
			}

			// Token: 0x02002A69 RID: 10857
			public class FOODSICKNESS
			{
				// Token: 0x0400B1B5 RID: 45493
				public static LocString NAME = UI.FormatAsLink("Food Poisoning", "FOODSICKNESS");

				// Token: 0x0400B1B6 RID: 45494
				public static LocString DESCRIPTION = "This Duplicant's last meal wasn't exactly food safe";

				// Token: 0x0400B1B7 RID: 45495
				public static LocString VOMIT_SYMPTOM = "Vomiting";

				// Token: 0x0400B1B8 RID: 45496
				public static LocString VOMIT_SYMPTOM_TOOLTIP = string.Concat(new string[]
				{
					"Duplicants periodically vomit throughout the day, producing additional ",
					UI.PRE_KEYWORD,
					"Germs",
					UI.PST_KEYWORD,
					" and losing ",
					UI.PRE_KEYWORD,
					"Calories",
					UI.PST_KEYWORD
				});

				// Token: 0x0400B1B9 RID: 45497
				public static LocString DESCRIPTIVE_SYMPTOMS = "Nonlethal. A Duplicant's body \"purges\" from both ends, causing extreme fatigue.";

				// Token: 0x0400B1BA RID: 45498
				public static LocString DISEASE_SOURCE_DESCRIPTOR = "Currently infected with {2}.\n\nThis Duplicant will produce {1} when vomiting.";

				// Token: 0x0400B1BB RID: 45499
				public static LocString DISEASE_SOURCE_DESCRIPTOR_TOOLTIP = "This Duplicant will vomit approximately every <b>{0}</b>\n\nEach time they vomit, they will release <b>{1}</b> and lose " + UI.PRE_KEYWORD + "Calories" + UI.PST_KEYWORD;
			}

			// Token: 0x02002A6A RID: 10858
			public class SLIMESICKNESS
			{
				// Token: 0x0400B1BC RID: 45500
				public static LocString NAME = UI.FormatAsLink("Slimelung", "SLIMESICKNESS");

				// Token: 0x0400B1BD RID: 45501
				public static LocString DESCRIPTION = "This Duplicant's chest congestion is making it difficult to breathe";

				// Token: 0x0400B1BE RID: 45502
				public static LocString COUGH_SYMPTOM = "Coughing";

				// Token: 0x0400B1BF RID: 45503
				public static LocString COUGH_SYMPTOM_TOOLTIP = string.Concat(new string[]
				{
					"Duplicants periodically cough up ",
					ELEMENTS.CONTAMINATEDOXYGEN.NAME,
					", producing additional ",
					UI.PRE_KEYWORD,
					"Germs",
					UI.PST_KEYWORD
				});

				// Token: 0x0400B1C0 RID: 45504
				public static LocString DESCRIPTIVE_SYMPTOMS = "Lethal without medical treatment. Duplicants experience coughing and shortness of breath.";

				// Token: 0x0400B1C1 RID: 45505
				public static LocString DISEASE_SOURCE_DESCRIPTOR = "Currently infected with {2}.\n\nThis Duplicant will produce <b>{1}</b> when coughing.";

				// Token: 0x0400B1C2 RID: 45506
				public static LocString DISEASE_SOURCE_DESCRIPTOR_TOOLTIP = "This Duplicant will cough approximately every <b>{0}</b>\n\nEach time they cough, they will release <b>{1}</b>";
			}

			// Token: 0x02002A6B RID: 10859
			public class ZOMBIESICKNESS
			{
				// Token: 0x0400B1C3 RID: 45507
				public static LocString NAME = UI.FormatAsLink("Zombie Spores", "ZOMBIESICKNESS");

				// Token: 0x0400B1C4 RID: 45508
				public static LocString DESCRIPTIVE_SYMPTOMS = "Duplicants lose much of their motor control and experience extreme discomfort.";

				// Token: 0x0400B1C5 RID: 45509
				public static LocString DESCRIPTION = "Fungal spores have infiltrated the Duplicant's head and are sending unnatural electrical impulses to their brain";

				// Token: 0x0400B1C6 RID: 45510
				public static LocString LEGEND_HOVERTEXT = "Area Causes Zombie Spores\n";
			}

			// Token: 0x02002A6C RID: 10860
			public class ALLERGIES
			{
				// Token: 0x0400B1C7 RID: 45511
				public static LocString NAME = UI.FormatAsLink("Allergic Reaction", "ALLERGIES");

				// Token: 0x0400B1C8 RID: 45512
				public static LocString DESCRIPTIVE_SYMPTOMS = "Allergens cause excessive sneezing fits";

				// Token: 0x0400B1C9 RID: 45513
				public static LocString DESCRIPTION = "Pollen and other irritants are causing this poor Duplicant's immune system to overreact, resulting in needless sneezing and congestion";
			}

			// Token: 0x02002A6D RID: 10861
			public class COLDSICKNESS
			{
				// Token: 0x0400B1CA RID: 45514
				public static LocString NAME = UI.FormatAsLink("Hypothermia", "COLDSICKNESS");

				// Token: 0x0400B1CB RID: 45515
				public static LocString DESCRIPTIVE_SYMPTOMS = "Nonlethal. Duplicants experience extreme body heat loss causing chills and discomfort.";

				// Token: 0x0400B1CC RID: 45516
				public static LocString DESCRIPTION = "This Duplicant's thought processes have been slowed to a crawl from extreme cold exposure";

				// Token: 0x0400B1CD RID: 45517
				public static LocString LEGEND_HOVERTEXT = "Area Causes Hypothermia\n";
			}

			// Token: 0x02002A6E RID: 10862
			public class SUNBURNSICKNESS
			{
				// Token: 0x0400B1CE RID: 45518
				public static LocString NAME = UI.FormatAsLink("Sunburn", "SUNBURNSICKNESS");

				// Token: 0x0400B1CF RID: 45519
				public static LocString DESCRIPTION = "Extreme sun exposure has given this Duplicant a nasty burn.";

				// Token: 0x0400B1D0 RID: 45520
				public static LocString LEGEND_HOVERTEXT = "Area Causes Sunburn\n";

				// Token: 0x0400B1D1 RID: 45521
				public static LocString SUNEXPOSURE = "Sun Exposure";

				// Token: 0x0400B1D2 RID: 45522
				public static LocString DESCRIPTIVE_SYMPTOMS = "Nonlethal. Duplicants experience temporary discomfort due to dermatological damage.";
			}

			// Token: 0x02002A6F RID: 10863
			public class HEATSICKNESS
			{
				// Token: 0x0400B1D3 RID: 45523
				public static LocString NAME = UI.FormatAsLink("Heat Stroke", "HEATSICKNESS");

				// Token: 0x0400B1D4 RID: 45524
				public static LocString DESCRIPTIVE_SYMPTOMS = "Nonlethal. Duplicants experience high fever and discomfort.";

				// Token: 0x0400B1D5 RID: 45525
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"This Duplicant's thought processes have short circuited from extreme ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" exposure"
				});

				// Token: 0x0400B1D6 RID: 45526
				public static LocString LEGEND_HOVERTEXT = "Area Causes Heat Stroke\n";
			}

			// Token: 0x02002A70 RID: 10864
			public class RADIATIONSICKNESS
			{
				// Token: 0x0400B1D7 RID: 45527
				public static LocString NAME = UI.FormatAsLink("Radioactive Contaminants", "RADIATIONSICKNESS");

				// Token: 0x0400B1D8 RID: 45528
				public static LocString DESCRIPTIVE_SYMPTOMS = "Extremely lethal. This Duplicant is not expected to survive.";

				// Token: 0x0400B1D9 RID: 45529
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"This Duplicant is leaving a trail of ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" behind them."
				});

				// Token: 0x0400B1DA RID: 45530
				public static LocString LEGEND_HOVERTEXT = "Area Causes Radiation Sickness\n";

				// Token: 0x0400B1DB RID: 45531
				public static LocString DESC = DUPLICANTS.DISEASES.RADIATIONPOISONING.DESC;
			}

			// Token: 0x02002A71 RID: 10865
			public class PUTRIDODOUR
			{
				// Token: 0x0400B1DC RID: 45532
				public static LocString NAME = UI.FormatAsLink("Trench Stench", "PUTRIDODOUR");

				// Token: 0x0400B1DD RID: 45533
				public static LocString DESCRIPTION = "\nThe pungent odor wafting off this Duplicant is nauseating to their peers";

				// Token: 0x0400B1DE RID: 45534
				public static LocString CRINGE_EFFECT = "Smelled a putrid odor";

				// Token: 0x0400B1DF RID: 45535
				public static LocString LEGEND_HOVERTEXT = "Trench Stench Germs Present\n";
			}
		}

		// Token: 0x02001CE6 RID: 7398
		public class MODIFIERS
		{
			// Token: 0x0400843B RID: 33851
			public static LocString MODIFIER_FORMAT = UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD + ": {1}";

			// Token: 0x0400843C RID: 33852
			public static LocString TIME_REMAINING = "Time Remaining: {0}";

			// Token: 0x0400843D RID: 33853
			public static LocString TIME_TOTAL = "\nDuration: {0}";

			// Token: 0x0400843E RID: 33854
			public static LocString EFFECT_HEADER = UI.PRE_POS_MODIFIER + "Effects:" + UI.PST_POS_MODIFIER;

			// Token: 0x02002A72 RID: 10866
			public class SKILLLEVEL
			{
				// Token: 0x0400B1E0 RID: 45536
				public static LocString NAME = "Skill Level";
			}

			// Token: 0x02002A73 RID: 10867
			public class ROOMPARK
			{
				// Token: 0x0400B1E1 RID: 45537
				public static LocString NAME = "Park";

				// Token: 0x0400B1E2 RID: 45538
				public static LocString TOOLTIP = "This Duplicant recently passed through a Park\n\nWow, nature sure is neat!";
			}

			// Token: 0x02002A74 RID: 10868
			public class ROOMNATURERESERVE
			{
				// Token: 0x0400B1E3 RID: 45539
				public static LocString NAME = "Nature Reserve";

				// Token: 0x0400B1E4 RID: 45540
				public static LocString TOOLTIP = "This Duplicant recently passed through a splendid Nature Reserve\n\nWow, nature sure is neat!";
			}

			// Token: 0x02002A75 RID: 10869
			public class ROOMLATRINE
			{
				// Token: 0x0400B1E5 RID: 45541
				public static LocString NAME = "Latrine";

				// Token: 0x0400B1E6 RID: 45542
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant used an ",
					BUILDINGS.PREFABS.OUTHOUSE.NAME,
					" in a ",
					UI.PRE_KEYWORD,
					"Latrine",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002A76 RID: 10870
			public class ROOMBATHROOM
			{
				// Token: 0x0400B1E7 RID: 45543
				public static LocString NAME = "Washroom";

				// Token: 0x0400B1E8 RID: 45544
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant used a ",
					BUILDINGS.PREFABS.FLUSHTOILET.NAME,
					" in a ",
					UI.PRE_KEYWORD,
					"Washroom",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002A77 RID: 10871
			public class ROOMBARRACKS
			{
				// Token: 0x0400B1E9 RID: 45545
				public static LocString NAME = "Barracks";

				// Token: 0x0400B1EA RID: 45546
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant slept in the ",
					UI.PRE_KEYWORD,
					"Barracks",
					UI.PST_KEYWORD,
					" last night and feels refreshed"
				});
			}

			// Token: 0x02002A78 RID: 10872
			public class ROOMBEDROOM
			{
				// Token: 0x0400B1EB RID: 45547
				public static LocString NAME = "Luxury Barracks";

				// Token: 0x0400B1EC RID: 45548
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant slept in a ",
					UI.PRE_KEYWORD,
					"Luxury Barracks",
					UI.PST_KEYWORD,
					" last night and feels extra refreshed"
				});
			}

			// Token: 0x02002A79 RID: 10873
			public class ROOMPRIVATEBEDROOM
			{
				// Token: 0x0400B1ED RID: 45549
				public static LocString NAME = "Private Bedroom";

				// Token: 0x0400B1EE RID: 45550
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant slept in a ",
					UI.PRE_KEYWORD,
					"Private Bedroom",
					UI.PST_KEYWORD,
					" last night and feels super refreshed"
				});
			}

			// Token: 0x02002A7A RID: 10874
			public class BEDHEALTH
			{
				// Token: 0x0400B1EF RID: 45551
				public static LocString NAME = "Bed Rest";

				// Token: 0x0400B1F0 RID: 45552
				public static LocString TOOLTIP = "This Duplicant will incrementally heal over while on " + UI.PRE_KEYWORD + "Bed Rest" + UI.PST_KEYWORD;
			}

			// Token: 0x02002A7B RID: 10875
			public class BEDSTAMINA
			{
				// Token: 0x0400B1F1 RID: 45553
				public static LocString NAME = "Sleeping in a cot";

				// Token: 0x0400B1F2 RID: 45554
				public static LocString TOOLTIP = "This Duplicant's sleeping arrangements are adequate";
			}

			// Token: 0x02002A7C RID: 10876
			public class LUXURYBEDSTAMINA
			{
				// Token: 0x0400B1F3 RID: 45555
				public static LocString NAME = "Sleeping in a comfy bed";

				// Token: 0x0400B1F4 RID: 45556
				public static LocString TOOLTIP = "This Duplicant loves their snuggly bed";
			}

			// Token: 0x02002A7D RID: 10877
			public class BARRACKSSTAMINA
			{
				// Token: 0x0400B1F5 RID: 45557
				public static LocString NAME = "Barracks";

				// Token: 0x0400B1F6 RID: 45558
				public static LocString TOOLTIP = "This Duplicant shares sleeping quarters with others";
			}

			// Token: 0x02002A7E RID: 10878
			public class LADDERBEDSTAMINA
			{
				// Token: 0x0400B1F7 RID: 45559
				public static LocString NAME = "Sleeping in a ladder bed";

				// Token: 0x0400B1F8 RID: 45560
				public static LocString TOOLTIP = "This Duplicant's sleeping arrangements are adequate";
			}

			// Token: 0x02002A7F RID: 10879
			public class BEDROOMSTAMINA
			{
				// Token: 0x0400B1F9 RID: 45561
				public static LocString NAME = "Private Bedroom";

				// Token: 0x0400B1FA RID: 45562
				public static LocString TOOLTIP = "This lucky Duplicant has their own private bedroom";
			}

			// Token: 0x02002A80 RID: 10880
			public class ROOMMESSHALL
			{
				// Token: 0x0400B1FB RID: 45563
				public static LocString NAME = "Mess Hall";

				// Token: 0x0400B1FC RID: 45564
				public static LocString TOOLTIP = "This Duplicant's most recent meal was eaten in a " + UI.PRE_KEYWORD + "Mess Hall" + UI.PST_KEYWORD;
			}

			// Token: 0x02002A81 RID: 10881
			public class ROOMGREATHALL
			{
				// Token: 0x0400B1FD RID: 45565
				public static LocString NAME = "Great Hall";

				// Token: 0x0400B1FE RID: 45566
				public static LocString TOOLTIP = "This Duplicant's most recent meal was eaten in a fancy " + UI.PRE_KEYWORD + "Great Hall" + UI.PST_KEYWORD;
			}

			// Token: 0x02002A82 RID: 10882
			public class ENTITLEMENT
			{
				// Token: 0x0400B1FF RID: 45567
				public static LocString NAME = "Entitlement";

				// Token: 0x0400B200 RID: 45568
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Duplicants will demand better ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" and accommodations with each Expertise level they gain"
				});
			}

			// Token: 0x02002A83 RID: 10883
			public class BASEDUPLICANT
			{
				// Token: 0x0400B201 RID: 45569
				public static LocString NAME = "Duplicant";
			}

			// Token: 0x02002A84 RID: 10884
			public class HOMEOSTASIS
			{
				// Token: 0x0400B202 RID: 45570
				public static LocString NAME = "Homeostasis";
			}

			// Token: 0x02002A85 RID: 10885
			public class WARMAIR
			{
				// Token: 0x0400B203 RID: 45571
				public static LocString NAME = "Warm Air";
			}

			// Token: 0x02002A86 RID: 10886
			public class COLDAIR
			{
				// Token: 0x0400B204 RID: 45572
				public static LocString NAME = "Cold Air";
			}

			// Token: 0x02002A87 RID: 10887
			public class CLAUSTROPHOBIC
			{
				// Token: 0x0400B205 RID: 45573
				public static LocString NAME = "Claustrophobic";

				// Token: 0x0400B206 RID: 45574
				public static LocString TOOLTIP = "This Duplicant recently found themselves in an upsettingly cramped space";

				// Token: 0x0400B207 RID: 45575
				public static LocString CAUSE = "This Duplicant got so good at their job that they became claustrophobic";
			}

			// Token: 0x02002A88 RID: 10888
			public class VERTIGO
			{
				// Token: 0x0400B208 RID: 45576
				public static LocString NAME = "Vertigo";

				// Token: 0x0400B209 RID: 45577
				public static LocString TOOLTIP = "This Duplicant had to climb a tall ladder that left them dizzy and unsettled";

				// Token: 0x0400B20A RID: 45578
				public static LocString CAUSE = "This Duplicant got so good at their job they became bad at ladders";
			}

			// Token: 0x02002A89 RID: 10889
			public class UNCOMFORTABLEFEET
			{
				// Token: 0x0400B20B RID: 45579
				public static LocString NAME = "Aching Feet";

				// Token: 0x0400B20C RID: 45580
				public static LocString TOOLTIP = "This Duplicant recently walked across floor without tile, much to their chagrin";

				// Token: 0x0400B20D RID: 45581
				public static LocString CAUSE = "This Duplicant got so good at their job that their feet became sensitive";
			}

			// Token: 0x02002A8A RID: 10890
			public class PEOPLETOOCLOSEWHILESLEEPING
			{
				// Token: 0x0400B20E RID: 45582
				public static LocString NAME = "Personal Bubble Burst";

				// Token: 0x0400B20F RID: 45583
				public static LocString TOOLTIP = "This Duplicant had to sleep too close to others and it was awkward for them";

				// Token: 0x0400B210 RID: 45584
				public static LocString CAUSE = "This Duplicant got so good at their job that they stopped being comfortable sleeping near other people";
			}

			// Token: 0x02002A8B RID: 10891
			public class RESTLESS
			{
				// Token: 0x0400B211 RID: 45585
				public static LocString NAME = "Restless";

				// Token: 0x0400B212 RID: 45586
				public static LocString TOOLTIP = "This Duplicant went a few minutes without working and is now completely awash with guilt";

				// Token: 0x0400B213 RID: 45587
				public static LocString CAUSE = "This Duplicant got so good at their job that they forgot how to be comfortable doing anything else";
			}

			// Token: 0x02002A8C RID: 10892
			public class UNFASHIONABLECLOTHING
			{
				// Token: 0x0400B214 RID: 45588
				public static LocString NAME = "Fashion Crime";

				// Token: 0x0400B215 RID: 45589
				public static LocString TOOLTIP = "This Duplicant had to wear something that was an affront to fashion";

				// Token: 0x0400B216 RID: 45590
				public static LocString CAUSE = "This Duplicant got so good at their job that they became incapable of tolerating unfashionable clothing";
			}

			// Token: 0x02002A8D RID: 10893
			public class BURNINGCALORIES
			{
				// Token: 0x0400B217 RID: 45591
				public static LocString NAME = "Homeostasis";
			}

			// Token: 0x02002A8E RID: 10894
			public class EATINGCALORIES
			{
				// Token: 0x0400B218 RID: 45592
				public static LocString NAME = "Eating";
			}

			// Token: 0x02002A8F RID: 10895
			public class TEMPEXCHANGE
			{
				// Token: 0x0400B219 RID: 45593
				public static LocString NAME = "Environmental Exchange";
			}

			// Token: 0x02002A90 RID: 10896
			public class CLOTHING
			{
				// Token: 0x0400B21A RID: 45594
				public static LocString NAME = "Clothing";
			}

			// Token: 0x02002A91 RID: 10897
			public class CRYFACE
			{
				// Token: 0x0400B21B RID: 45595
				public static LocString NAME = "Cry Face";

				// Token: 0x0400B21C RID: 45596
				public static LocString TOOLTIP = "This Duplicant recently had a crying fit and it shows";

				// Token: 0x0400B21D RID: 45597
				public static LocString CAUSE = string.Concat(new string[]
				{
					"Obtained from the ",
					UI.PRE_KEYWORD,
					"Ugly Crier",
					UI.PST_KEYWORD,
					" stress reaction"
				});
			}

			// Token: 0x02002A92 RID: 10898
			public class SOILEDSUIT
			{
				// Token: 0x0400B21E RID: 45598
				public static LocString NAME = "Soiled Suit";

				// Token: 0x0400B21F RID: 45599
				public static LocString TOOLTIP = "This Duplicant's suit needs to be emptied of waste\n\n(Preferably soon)";

				// Token: 0x0400B220 RID: 45600
				public static LocString CAUSE = "Obtained when a Duplicant wears a suit filled with... \"fluids\"";
			}

			// Token: 0x02002A93 RID: 10899
			public class SHOWERED
			{
				// Token: 0x0400B221 RID: 45601
				public static LocString NAME = "Showered";

				// Token: 0x0400B222 RID: 45602
				public static LocString TOOLTIP = "This Duplicant recently had a shower and feels squeaky clean!";
			}

			// Token: 0x02002A94 RID: 10900
			public class SOREBACK
			{
				// Token: 0x0400B223 RID: 45603
				public static LocString NAME = "Sore Back";

				// Token: 0x0400B224 RID: 45604
				public static LocString TOOLTIP = "This Duplicant feels achy from sleeping on the floor last night and would like a bed";

				// Token: 0x0400B225 RID: 45605
				public static LocString CAUSE = "Obtained by sleeping on the ground";
			}

			// Token: 0x02002A95 RID: 10901
			public class GOODEATS
			{
				// Token: 0x0400B226 RID: 45606
				public static LocString NAME = "Soul Food";

				// Token: 0x0400B227 RID: 45607
				public static LocString TOOLTIP = "This Duplicant had a yummy home cooked meal and is totally stuffed";

				// Token: 0x0400B228 RID: 45608
				public static LocString CAUSE = "Obtained by eating a hearty home cooked meal";

				// Token: 0x0400B229 RID: 45609
				public static LocString DESCRIPTION = "Duplicants find this home cooked meal is emotionally comforting";
			}

			// Token: 0x02002A96 RID: 10902
			public class HOTSTUFF
			{
				// Token: 0x0400B22A RID: 45610
				public static LocString NAME = "Hot Stuff";

				// Token: 0x0400B22B RID: 45611
				public static LocString TOOLTIP = "This Duplicant had an extremely spicy meal and is both exhilarated and a little " + UI.PRE_KEYWORD + "Stressed" + UI.PST_KEYWORD;

				// Token: 0x0400B22C RID: 45612
				public static LocString CAUSE = "Obtained by eating a very spicy meal";

				// Token: 0x0400B22D RID: 45613
				public static LocString DESCRIPTION = "Duplicants find this spicy meal quite invigorating";
			}

			// Token: 0x02002A97 RID: 10903
			public class SEAFOODRADIATIONRESISTANCE
			{
				// Token: 0x0400B22E RID: 45614
				public static LocString NAME = "Radiation Resistant: Aquatic Diet";

				// Token: 0x0400B22F RID: 45615
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant ate sea-grown foods, which boost ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" resistance"
				});

				// Token: 0x0400B230 RID: 45616
				public static LocString CAUSE = "Obtained by eating sea-grown foods like fish or lettuce";

				// Token: 0x0400B231 RID: 45617
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"Eating this improves ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" resistance"
				});
			}

			// Token: 0x02002A98 RID: 10904
			public class RECENTLYPARTIED
			{
				// Token: 0x0400B232 RID: 45618
				public static LocString NAME = "Partied Hard";

				// Token: 0x0400B233 RID: 45619
				public static LocString TOOLTIP = "This Duplicant recently attended a great party!";
			}

			// Token: 0x02002A99 RID: 10905
			public class NOFUNALLOWED
			{
				// Token: 0x0400B234 RID: 45620
				public static LocString NAME = "Fun Interrupted";

				// Token: 0x0400B235 RID: 45621
				public static LocString TOOLTIP = "This Duplicant is upset a party was rejected";
			}

			// Token: 0x02002A9A RID: 10906
			public class CONTAMINATEDLUNGS
			{
				// Token: 0x0400B236 RID: 45622
				public static LocString NAME = "Yucky Lungs";

				// Token: 0x0400B237 RID: 45623
				public static LocString TOOLTIP = "This Duplicant got a big nasty lungful of " + ELEMENTS.CONTAMINATEDOXYGEN.NAME;
			}

			// Token: 0x02002A9B RID: 10907
			public class MINORIRRITATION
			{
				// Token: 0x0400B238 RID: 45624
				public static LocString NAME = "Minor Eye Irritation";

				// Token: 0x0400B239 RID: 45625
				public static LocString TOOLTIP = "A gas or liquid made this Duplicant's eyes sting a little";

				// Token: 0x0400B23A RID: 45626
				public static LocString CAUSE = "Obtained by exposure to a harsh liquid or gas";
			}

			// Token: 0x02002A9C RID: 10908
			public class MAJORIRRITATION
			{
				// Token: 0x0400B23B RID: 45627
				public static LocString NAME = "Major Eye Irritation";

				// Token: 0x0400B23C RID: 45628
				public static LocString TOOLTIP = "Woah, something really messed up this Duplicant's eyes!\n\nCaused by exposure to a harsh liquid or gas";

				// Token: 0x0400B23D RID: 45629
				public static LocString CAUSE = "Obtained by exposure to a harsh liquid or gas";
			}

			// Token: 0x02002A9D RID: 10909
			public class FRESH_AND_CLEAN
			{
				// Token: 0x0400B23E RID: 45630
				public static LocString NAME = "Refreshingly Clean";

				// Token: 0x0400B23F RID: 45631
				public static LocString TOOLTIP = "This Duplicant took a warm shower and it was great!";

				// Token: 0x0400B240 RID: 45632
				public static LocString CAUSE = "Obtained by taking a comfortably heated shower";
			}

			// Token: 0x02002A9E RID: 10910
			public class BURNED_BY_SCALDING_WATER
			{
				// Token: 0x0400B241 RID: 45633
				public static LocString NAME = "Scalded";

				// Token: 0x0400B242 RID: 45634
				public static LocString TOOLTIP = "Ouch! This Duplicant showered or was doused in water that was way too hot";

				// Token: 0x0400B243 RID: 45635
				public static LocString CAUSE = "Obtained by exposure to hot water";
			}

			// Token: 0x02002A9F RID: 10911
			public class STRESSED_BY_COLD_WATER
			{
				// Token: 0x0400B244 RID: 45636
				public static LocString NAME = "Numb";

				// Token: 0x0400B245 RID: 45637
				public static LocString TOOLTIP = "Brr! This Duplicant was showered or doused in water that was way too cold";

				// Token: 0x0400B246 RID: 45638
				public static LocString CAUSE = "Obtained by exposure to icy water";
			}

			// Token: 0x02002AA0 RID: 10912
			public class SMELLEDSTINKY
			{
				// Token: 0x0400B247 RID: 45639
				public static LocString NAME = "Smelled Stinky";

				// Token: 0x0400B248 RID: 45640
				public static LocString TOOLTIP = "This Duplicant got a whiff of a certain somebody";
			}

			// Token: 0x02002AA1 RID: 10913
			public class STRESSREDUCTION
			{
				// Token: 0x0400B249 RID: 45641
				public static LocString NAME = "Receiving Massage";

				// Token: 0x0400B24A RID: 45642
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant's ",
					UI.PRE_KEYWORD,
					"Stress",
					UI.PST_KEYWORD,
					" is just melting away"
				});
			}

			// Token: 0x02002AA2 RID: 10914
			public class STRESSREDUCTION_CLINIC
			{
				// Token: 0x0400B24B RID: 45643
				public static LocString NAME = "Receiving Clinic Massage";

				// Token: 0x0400B24C RID: 45644
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Clinical facilities are improving the effectiveness of this massage\n\nThis Duplicant's ",
					UI.PRE_KEYWORD,
					"Stress",
					UI.PST_KEYWORD,
					" is just melting away"
				});
			}

			// Token: 0x02002AA3 RID: 10915
			public class UGLY_CRYING
			{
				// Token: 0x0400B24D RID: 45645
				public static LocString NAME = "Ugly Crying";

				// Token: 0x0400B24E RID: 45646
				public static LocString TOOLTIP = "This Duplicant is having a cathartic ugly cry as a result of " + UI.PRE_KEYWORD + "Stress" + UI.PST_KEYWORD;

				// Token: 0x0400B24F RID: 45647
				public static LocString NOTIFICATION_NAME = "Ugly Crying";

				// Token: 0x0400B250 RID: 45648
				public static LocString NOTIFICATION_TOOLTIP = "These Duplicants became so " + UI.FormatAsLink("Stressed", "STRESS") + " they broke down crying:";
			}

			// Token: 0x02002AA4 RID: 10916
			public class BINGE_EATING
			{
				// Token: 0x0400B251 RID: 45649
				public static LocString NAME = "Insatiable Hunger";

				// Token: 0x0400B252 RID: 45650
				public static LocString TOOLTIP = "This Duplicant is stuffing their face as a result of " + UI.PRE_KEYWORD + "Stress" + UI.PST_KEYWORD;

				// Token: 0x0400B253 RID: 45651
				public static LocString NOTIFICATION_NAME = "Binge Eating";

				// Token: 0x0400B254 RID: 45652
				public static LocString NOTIFICATION_TOOLTIP = "These Duplicants became so " + UI.FormatAsLink("Stressed", "STRESS") + " they began overeating:";
			}

			// Token: 0x02002AA5 RID: 10917
			public class BANSHEE_WAILING
			{
				// Token: 0x0400B255 RID: 45653
				public static LocString NAME = "Deafening Shriek";

				// Token: 0x0400B256 RID: 45654
				public static LocString TOOLTIP = "This Duplicant is wailing at the top of their lungs as a result of " + UI.PRE_KEYWORD + "Stress" + UI.PST_KEYWORD;

				// Token: 0x0400B257 RID: 45655
				public static LocString NOTIFICATION_NAME = "Banshee Wailing";

				// Token: 0x0400B258 RID: 45656
				public static LocString NOTIFICATION_TOOLTIP = "These Duplicants became so " + UI.FormatAsLink("Stressed", "STRESS") + " they began wailing:";
			}

			// Token: 0x02002AA6 RID: 10918
			public class BANSHEE_WAILING_RECOVERY
			{
				// Token: 0x0400B259 RID: 45657
				public static LocString NAME = "Guzzling Air";

				// Token: 0x0400B25A RID: 45658
				public static LocString TOOLTIP = "This Duplicant needs a little extra oxygen to catch their breath";
			}

			// Token: 0x02002AA7 RID: 10919
			public class METABOLISM_CALORIE_MODIFIER
			{
				// Token: 0x0400B25B RID: 45659
				public static LocString NAME = "Metabolism";

				// Token: 0x0400B25C RID: 45660
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"Metabolism",
					UI.PST_KEYWORD,
					" determines how quickly a critter burns ",
					UI.PRE_KEYWORD,
					"Calories",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002AA8 RID: 10920
			public class WORKING
			{
				// Token: 0x0400B25D RID: 45661
				public static LocString NAME = "Working";

				// Token: 0x0400B25E RID: 45662
				public static LocString TOOLTIP = "This Duplicant is working up a sweat";
			}

			// Token: 0x02002AA9 RID: 10921
			public class UNCOMFORTABLESLEEP
			{
				// Token: 0x0400B25F RID: 45663
				public static LocString NAME = "Sleeping Uncomfortably";

				// Token: 0x0400B260 RID: 45664
				public static LocString TOOLTIP = "This Duplicant collapsed on the floor from sheer exhaustion";
			}

			// Token: 0x02002AAA RID: 10922
			public class MANAGERIALDUTIES
			{
				// Token: 0x0400B261 RID: 45665
				public static LocString NAME = "Managerial Duties";

				// Token: 0x0400B262 RID: 45666
				public static LocString TOOLTIP = "Being a manager is stressful";
			}

			// Token: 0x02002AAB RID: 10923
			public class MANAGEDCOLONY
			{
				// Token: 0x0400B263 RID: 45667
				public static LocString NAME = "Managed Colony";

				// Token: 0x0400B264 RID: 45668
				public static LocString TOOLTIP = "A Duplicant is in the colony manager job";
			}

			// Token: 0x02002AAC RID: 10924
			public class FLOORSLEEP
			{
				// Token: 0x0400B265 RID: 45669
				public static LocString NAME = "Sleeping On Floor";

				// Token: 0x0400B266 RID: 45670
				public static LocString TOOLTIP = "This Duplicant is uncomfortably recovering " + UI.PRE_KEYWORD + "Stamina" + UI.PST_KEYWORD;
			}

			// Token: 0x02002AAD RID: 10925
			public class PASSEDOUTSLEEP
			{
				// Token: 0x0400B267 RID: 45671
				public static LocString NAME = "Exhausted";

				// Token: 0x0400B268 RID: 45672
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Lack of rest depleted this Duplicant's ",
					UI.PRE_KEYWORD,
					"Stamina",
					UI.PST_KEYWORD,
					"\n\nThey passed out from the fatigue"
				});
			}

			// Token: 0x02002AAE RID: 10926
			public class SLEEP
			{
				// Token: 0x0400B269 RID: 45673
				public static LocString NAME = "Sleeping";

				// Token: 0x0400B26A RID: 45674
				public static LocString TOOLTIP = "This Duplicant is recovering " + UI.PRE_KEYWORD + "Stamina" + UI.PST_KEYWORD;
			}

			// Token: 0x02002AAF RID: 10927
			public class SLEEPCLINIC
			{
				// Token: 0x0400B26B RID: 45675
				public static LocString NAME = "Nodding Off";

				// Token: 0x0400B26C RID: 45676
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is losing ",
					UI.PRE_KEYWORD,
					"Stamina",
					UI.PST_KEYWORD,
					" because of their ",
					UI.PRE_KEYWORD,
					"Pajamas",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002AB0 RID: 10928
			public class RESTFULSLEEP
			{
				// Token: 0x0400B26D RID: 45677
				public static LocString NAME = "Sleeping Peacefully";

				// Token: 0x0400B26E RID: 45678
				public static LocString TOOLTIP = "This Duplicant is getting a good night's rest";
			}

			// Token: 0x02002AB1 RID: 10929
			public class SLEEPY
			{
				// Token: 0x0400B26F RID: 45679
				public static LocString NAME = "Sleepy";

				// Token: 0x0400B270 RID: 45680
				public static LocString TOOLTIP = "This Duplicant is getting tired";
			}

			// Token: 0x02002AB2 RID: 10930
			public class HUNGRY
			{
				// Token: 0x0400B271 RID: 45681
				public static LocString NAME = "Hungry";

				// Token: 0x0400B272 RID: 45682
				public static LocString TOOLTIP = "This Duplicant is ready for lunch";
			}

			// Token: 0x02002AB3 RID: 10931
			public class STARVING
			{
				// Token: 0x0400B273 RID: 45683
				public static LocString NAME = "Starving";

				// Token: 0x0400B274 RID: 45684
				public static LocString TOOLTIP = "This Duplicant needs to eat something, soon";
			}

			// Token: 0x02002AB4 RID: 10932
			public class HOT
			{
				// Token: 0x0400B275 RID: 45685
				public static LocString NAME = "Hot";

				// Token: 0x0400B276 RID: 45686
				public static LocString TOOLTIP = "This Duplicant is uncomfortably warm";
			}

			// Token: 0x02002AB5 RID: 10933
			public class COLD
			{
				// Token: 0x0400B277 RID: 45687
				public static LocString NAME = "Cold";

				// Token: 0x0400B278 RID: 45688
				public static LocString TOOLTIP = "This Duplicant is uncomfortably cold";
			}

			// Token: 0x02002AB6 RID: 10934
			public class CARPETFEET
			{
				// Token: 0x0400B279 RID: 45689
				public static LocString NAME = "Tickled Tootsies";

				// Token: 0x0400B27A RID: 45690
				public static LocString TOOLTIP = "Walking on carpet has made this Duplicant's day a little more luxurious";
			}

			// Token: 0x02002AB7 RID: 10935
			public class WETFEET
			{
				// Token: 0x0400B27B RID: 45691
				public static LocString NAME = "Soggy Feet";

				// Token: 0x0400B27C RID: 45692
				public static LocString TOOLTIP = "This Duplicant recently stepped in " + UI.PRE_KEYWORD + "Liquid" + UI.PST_KEYWORD;

				// Token: 0x0400B27D RID: 45693
				public static LocString CAUSE = "Obtained by walking through liquid";
			}

			// Token: 0x02002AB8 RID: 10936
			public class SOAKINGWET
			{
				// Token: 0x0400B27E RID: 45694
				public static LocString NAME = "Sopping Wet";

				// Token: 0x0400B27F RID: 45695
				public static LocString TOOLTIP = "This Duplicant was recently submerged in " + UI.PRE_KEYWORD + "Liquid" + UI.PST_KEYWORD;

				// Token: 0x0400B280 RID: 45696
				public static LocString CAUSE = "Obtained from submergence in liquid";
			}

			// Token: 0x02002AB9 RID: 10937
			public class POPPEDEARDRUMS
			{
				// Token: 0x0400B281 RID: 45697
				public static LocString NAME = "Popped Eardrums";

				// Token: 0x0400B282 RID: 45698
				public static LocString TOOLTIP = "This Duplicant was exposed to an over-pressurized area that popped their eardrums";
			}

			// Token: 0x02002ABA RID: 10938
			public class ANEWHOPE
			{
				// Token: 0x0400B283 RID: 45699
				public static LocString NAME = "New Hope";

				// Token: 0x0400B284 RID: 45700
				public static LocString TOOLTIP = "This Duplicant feels pretty optimistic about their new home";
			}

			// Token: 0x02002ABB RID: 10939
			public class MEGABRAINTANKBONUS
			{
				// Token: 0x0400B285 RID: 45701
				public static LocString NAME = "Maximum Aptitude";

				// Token: 0x0400B286 RID: 45702
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is smarter and stronger than usual thanks to the ",
					UI.PRE_KEYWORD,
					"Somnium Synthesizer",
					UI.PST_KEYWORD,
					" "
				});
			}

			// Token: 0x02002ABC RID: 10940
			public class PRICKLEFRUITDAMAGE
			{
				// Token: 0x0400B287 RID: 45703
				public static LocString NAME = "Ouch!";

				// Token: 0x0400B288 RID: 45704
				public static LocString TOOLTIP = "This Duplicant ate a raw " + UI.FormatAsLink("Bristle Berry", "PRICKLEFRUIT") + " and it gave their mouth ouchies";
			}

			// Token: 0x02002ABD RID: 10941
			public class NOOXYGEN
			{
				// Token: 0x0400B289 RID: 45705
				public static LocString NAME = "No Oxygen";

				// Token: 0x0400B28A RID: 45706
				public static LocString TOOLTIP = "There is no breathable air in this area";
			}

			// Token: 0x02002ABE RID: 10942
			public class LOWOXYGEN
			{
				// Token: 0x0400B28B RID: 45707
				public static LocString NAME = "Low Oxygen";

				// Token: 0x0400B28C RID: 45708
				public static LocString TOOLTIP = "The air is thin in this area";
			}

			// Token: 0x02002ABF RID: 10943
			public class MOURNING
			{
				// Token: 0x0400B28D RID: 45709
				public static LocString NAME = "Mourning";

				// Token: 0x0400B28E RID: 45710
				public static LocString TOOLTIP = "This Duplicant is grieving the loss of a friend";
			}

			// Token: 0x02002AC0 RID: 10944
			public class NARCOLEPTICSLEEP
			{
				// Token: 0x0400B28F RID: 45711
				public static LocString NAME = "Narcoleptic Nap";

				// Token: 0x0400B290 RID: 45712
				public static LocString TOOLTIP = "This Duplicant just needs to rest their eyes for a second";
			}

			// Token: 0x02002AC1 RID: 10945
			public class BADSLEEP
			{
				// Token: 0x0400B291 RID: 45713
				public static LocString NAME = "Unrested: Too Bright";

				// Token: 0x0400B292 RID: 45714
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant tossed and turned all night because a ",
					UI.PRE_KEYWORD,
					"Light",
					UI.PST_KEYWORD,
					" was left on where they were trying to sleep"
				});
			}

			// Token: 0x02002AC2 RID: 10946
			public class BADSLEEPAFRAIDOFDARK
			{
				// Token: 0x0400B293 RID: 45715
				public static LocString NAME = "Unrested: Afraid of Dark";

				// Token: 0x0400B294 RID: 45716
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant didn't get much sleep because they were too anxious about the lack of ",
					UI.PRE_KEYWORD,
					"Light",
					UI.PST_KEYWORD,
					" to relax"
				});
			}

			// Token: 0x02002AC3 RID: 10947
			public class BADSLEEPMOVEMENT
			{
				// Token: 0x0400B295 RID: 45717
				public static LocString NAME = "Unrested: Bed Jostling";

				// Token: 0x0400B296 RID: 45718
				public static LocString TOOLTIP = "This Duplicant was woken up when a friend climbed on their ladder bed";
			}

			// Token: 0x02002AC4 RID: 10948
			public class TERRIBLESLEEP
			{
				// Token: 0x0400B297 RID: 45719
				public static LocString NAME = "Dead Tired: Snoring Friend";

				// Token: 0x0400B298 RID: 45720
				public static LocString TOOLTIP = "This Duplicant didn't get any shuteye last night because of all the racket from a friend's snoring";
			}

			// Token: 0x02002AC5 RID: 10949
			public class PEACEFULSLEEP
			{
				// Token: 0x0400B299 RID: 45721
				public static LocString NAME = "Well Rested";

				// Token: 0x0400B29A RID: 45722
				public static LocString TOOLTIP = "This Duplicant had a blissfully quiet sleep last night";
			}

			// Token: 0x02002AC6 RID: 10950
			public class CENTEROFATTENTION
			{
				// Token: 0x0400B29B RID: 45723
				public static LocString NAME = "Center of Attention";

				// Token: 0x0400B29C RID: 45724
				public static LocString TOOLTIP = "This Duplicant feels like someone's watching over them...";
			}

			// Token: 0x02002AC7 RID: 10951
			public class INSPIRED
			{
				// Token: 0x0400B29D RID: 45725
				public static LocString NAME = "Inspired";

				// Token: 0x0400B29E RID: 45726
				public static LocString TOOLTIP = "This Duplicant has had a creative vision!";
			}

			// Token: 0x02002AC8 RID: 10952
			public class NEWCREWARRIVAL
			{
				// Token: 0x0400B29F RID: 45727
				public static LocString NAME = "New Friend";

				// Token: 0x0400B2A0 RID: 45728
				public static LocString TOOLTIP = "This Duplicant is happy to see a new face in the colony";
			}

			// Token: 0x02002AC9 RID: 10953
			public class UNDERWATER
			{
				// Token: 0x0400B2A1 RID: 45729
				public static LocString NAME = "Underwater";

				// Token: 0x0400B2A2 RID: 45730
				public static LocString TOOLTIP = "This Duplicant's movement is slowed";
			}

			// Token: 0x02002ACA RID: 10954
			public class NIGHTMARES
			{
				// Token: 0x0400B2A3 RID: 45731
				public static LocString NAME = "Nightmares";

				// Token: 0x0400B2A4 RID: 45732
				public static LocString TOOLTIP = "This Duplicant was visited by something in the night";
			}

			// Token: 0x02002ACB RID: 10955
			public class WASATTACKED
			{
				// Token: 0x0400B2A5 RID: 45733
				public static LocString NAME = "Recently assailed";

				// Token: 0x0400B2A6 RID: 45734
				public static LocString TOOLTIP = "This Duplicant is stressed out after having been attacked";
			}

			// Token: 0x02002ACC RID: 10956
			public class LIGHTWOUNDS
			{
				// Token: 0x0400B2A7 RID: 45735
				public static LocString NAME = "Light Wounds";

				// Token: 0x0400B2A8 RID: 45736
				public static LocString TOOLTIP = "This Duplicant sustained injuries that are a bit uncomfortable";
			}

			// Token: 0x02002ACD RID: 10957
			public class MODERATEWOUNDS
			{
				// Token: 0x0400B2A9 RID: 45737
				public static LocString NAME = "Moderate Wounds";

				// Token: 0x0400B2AA RID: 45738
				public static LocString TOOLTIP = "This Duplicant sustained injuries that are affecting their ability to work";
			}

			// Token: 0x02002ACE RID: 10958
			public class SEVEREWOUNDS
			{
				// Token: 0x0400B2AB RID: 45739
				public static LocString NAME = "Severe Wounds";

				// Token: 0x0400B2AC RID: 45740
				public static LocString TOOLTIP = "This Duplicant sustained serious injuries that are impacting their work and well-being";
			}

			// Token: 0x02002ACF RID: 10959
			public class SANDBOXMORALEADJUSTMENT
			{
				// Token: 0x0400B2AD RID: 45741
				public static LocString NAME = "Sandbox Morale Adjustment";

				// Token: 0x0400B2AE RID: 45742
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant has had their ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" temporarily adjusted using the Sandbox tools"
				});
			}

			// Token: 0x02002AD0 RID: 10960
			public class ROTTEMPERATURE
			{
				// Token: 0x0400B2AF RID: 45743
				public static LocString UNREFRIGERATED = "Unrefrigerated";

				// Token: 0x0400B2B0 RID: 45744
				public static LocString REFRIGERATED = "Refrigerated";

				// Token: 0x0400B2B1 RID: 45745
				public static LocString FROZEN = "Frozen";
			}

			// Token: 0x02002AD1 RID: 10961
			public class ROTATMOSPHERE
			{
				// Token: 0x0400B2B2 RID: 45746
				public static LocString CONTAMINATED = "Contaminated Air";

				// Token: 0x0400B2B3 RID: 45747
				public static LocString NORMAL = "Normal Atmosphere";

				// Token: 0x0400B2B4 RID: 45748
				public static LocString STERILE = "Sterile Atmosphere";
			}

			// Token: 0x02002AD2 RID: 10962
			public class BASEROT
			{
				// Token: 0x0400B2B5 RID: 45749
				public static LocString NAME = "Base Decay Rate";
			}

			// Token: 0x02002AD3 RID: 10963
			public class FULLBLADDER
			{
				// Token: 0x0400B2B6 RID: 45750
				public static LocString NAME = "Full Bladder";

				// Token: 0x0400B2B7 RID: 45751
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant's ",
					UI.PRE_KEYWORD,
					"Bladder",
					UI.PST_KEYWORD,
					" is full"
				});
			}

			// Token: 0x02002AD4 RID: 10964
			public class DIARRHEA
			{
				// Token: 0x0400B2B8 RID: 45752
				public static LocString NAME = "Diarrhea";

				// Token: 0x0400B2B9 RID: 45753
				public static LocString TOOLTIP = "This Duplicant's gut is giving them some trouble";

				// Token: 0x0400B2BA RID: 45754
				public static LocString CAUSE = "Obtained by eating a disgusting meal";

				// Token: 0x0400B2BB RID: 45755
				public static LocString DESCRIPTION = "Most Duplicants experience stomach upset from this meal";
			}

			// Token: 0x02002AD5 RID: 10965
			public class STRESSFULYEMPTYINGBLADDER
			{
				// Token: 0x0400B2BC RID: 45756
				public static LocString NAME = "Making a mess";

				// Token: 0x0400B2BD RID: 45757
				public static LocString TOOLTIP = "This Duplicant had no choice but to empty their " + UI.PRE_KEYWORD + "Bladder" + UI.PST_KEYWORD;
			}

			// Token: 0x02002AD6 RID: 10966
			public class REDALERT
			{
				// Token: 0x0400B2BE RID: 45758
				public static LocString NAME = "Red Alert!";

				// Token: 0x0400B2BF RID: 45759
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The ",
					UI.PRE_KEYWORD,
					"Red Alert",
					UI.PST_KEYWORD,
					" is stressing this Duplicant out"
				});
			}

			// Token: 0x02002AD7 RID: 10967
			public class FUSSY
			{
				// Token: 0x0400B2C0 RID: 45760
				public static LocString NAME = "Fussy";

				// Token: 0x0400B2C1 RID: 45761
				public static LocString TOOLTIP = "This Duplicant is hard to please";
			}

			// Token: 0x02002AD8 RID: 10968
			public class WARMINGUP
			{
				// Token: 0x0400B2C2 RID: 45762
				public static LocString NAME = "Warming Up";

				// Token: 0x0400B2C3 RID: 45763
				public static LocString TOOLTIP = "This Duplicant is trying to warm back up";
			}

			// Token: 0x02002AD9 RID: 10969
			public class COOLINGDOWN
			{
				// Token: 0x0400B2C4 RID: 45764
				public static LocString NAME = "Cooling Down";

				// Token: 0x0400B2C5 RID: 45765
				public static LocString TOOLTIP = "This Duplicant is trying to cool back down";
			}

			// Token: 0x02002ADA RID: 10970
			public class DARKNESS
			{
				// Token: 0x0400B2C6 RID: 45766
				public static LocString NAME = "Darkness";

				// Token: 0x0400B2C7 RID: 45767
				public static LocString TOOLTIP = "Eep! This Duplicant doesn't like being in the dark!";
			}

			// Token: 0x02002ADB RID: 10971
			public class STEPPEDINCONTAMINATEDWATER
			{
				// Token: 0x0400B2C8 RID: 45768
				public static LocString NAME = "Stepped in polluted water";

				// Token: 0x0400B2C9 RID: 45769
				public static LocString TOOLTIP = "Gross! This Duplicant stepped in something yucky";
			}

			// Token: 0x02002ADC RID: 10972
			public class WELLFED
			{
				// Token: 0x0400B2CA RID: 45770
				public static LocString NAME = "Well fed";

				// Token: 0x0400B2CB RID: 45771
				public static LocString TOOLTIP = "This Duplicant feels satisfied after having a big meal";
			}

			// Token: 0x02002ADD RID: 10973
			public class STALEFOOD
			{
				// Token: 0x0400B2CC RID: 45772
				public static LocString NAME = "Bad leftovers";

				// Token: 0x0400B2CD RID: 45773
				public static LocString TOOLTIP = "This Duplicant is in a bad mood from having to eat stale " + UI.PRE_KEYWORD + "Food" + UI.PST_KEYWORD;
			}

			// Token: 0x02002ADE RID: 10974
			public class SMELLEDPUTRIDODOUR
			{
				// Token: 0x0400B2CE RID: 45774
				public static LocString NAME = "Smelled a putrid odor";

				// Token: 0x0400B2CF RID: 45775
				public static LocString TOOLTIP = "This Duplicant got a whiff of something unspeakably foul";
			}

			// Token: 0x02002ADF RID: 10975
			public class VOMITING
			{
				// Token: 0x0400B2D0 RID: 45776
				public static LocString NAME = "Vomiting";

				// Token: 0x0400B2D1 RID: 45777
				public static LocString TOOLTIP = "Better out than in, as they say";
			}

			// Token: 0x02002AE0 RID: 10976
			public class BREATHING
			{
				// Token: 0x0400B2D2 RID: 45778
				public static LocString NAME = "Breathing";
			}

			// Token: 0x02002AE1 RID: 10977
			public class HOLDINGBREATH
			{
				// Token: 0x0400B2D3 RID: 45779
				public static LocString NAME = "Holding breath";
			}

			// Token: 0x02002AE2 RID: 10978
			public class RECOVERINGBREATH
			{
				// Token: 0x0400B2D4 RID: 45780
				public static LocString NAME = "Recovering breath";
			}

			// Token: 0x02002AE3 RID: 10979
			public class ROTTING
			{
				// Token: 0x0400B2D5 RID: 45781
				public static LocString NAME = "Rotting";
			}

			// Token: 0x02002AE4 RID: 10980
			public class DEAD
			{
				// Token: 0x0400B2D6 RID: 45782
				public static LocString NAME = "Dead";
			}

			// Token: 0x02002AE5 RID: 10981
			public class TOXICENVIRONMENT
			{
				// Token: 0x0400B2D7 RID: 45783
				public static LocString NAME = "Toxic environment";
			}

			// Token: 0x02002AE6 RID: 10982
			public class RESTING
			{
				// Token: 0x0400B2D8 RID: 45784
				public static LocString NAME = "Resting";
			}

			// Token: 0x02002AE7 RID: 10983
			public class INTRAVENOUS_NUTRITION
			{
				// Token: 0x0400B2D9 RID: 45785
				public static LocString NAME = "Intravenous Feeding";
			}

			// Token: 0x02002AE8 RID: 10984
			public class CATHETERIZED
			{
				// Token: 0x0400B2DA RID: 45786
				public static LocString NAME = "Catheterized";

				// Token: 0x0400B2DB RID: 45787
				public static LocString TOOLTIP = "Let's leave it at that";
			}

			// Token: 0x02002AE9 RID: 10985
			public class NOISEPEACEFUL
			{
				// Token: 0x0400B2DC RID: 45788
				public static LocString NAME = "Peace and Quiet";

				// Token: 0x0400B2DD RID: 45789
				public static LocString TOOLTIP = "This Duplicant has found a quiet place to concentrate";
			}

			// Token: 0x02002AEA RID: 10986
			public class NOISEMINOR
			{
				// Token: 0x0400B2DE RID: 45790
				public static LocString NAME = "Loud Noises";

				// Token: 0x0400B2DF RID: 45791
				public static LocString TOOLTIP = "This area is a bit too loud for comfort";
			}

			// Token: 0x02002AEB RID: 10987
			public class NOISEMAJOR
			{
				// Token: 0x0400B2E0 RID: 45792
				public static LocString NAME = "Cacophony!";

				// Token: 0x0400B2E1 RID: 45793
				public static LocString TOOLTIP = "It's very, very loud in here!";
			}

			// Token: 0x02002AEC RID: 10988
			public class MEDICALCOT
			{
				// Token: 0x0400B2E2 RID: 45794
				public static LocString NAME = "Triage Cot Rest";

				// Token: 0x0400B2E3 RID: 45795
				public static LocString TOOLTIP = "Bedrest is improving this Duplicant's physical recovery time";
			}

			// Token: 0x02002AED RID: 10989
			public class MEDICALCOTDOCTORED
			{
				// Token: 0x0400B2E4 RID: 45796
				public static LocString NAME = "Receiving treatment";

				// Token: 0x0400B2E5 RID: 45797
				public static LocString TOOLTIP = "This Duplicant is receiving treatment for their physical injuries";
			}

			// Token: 0x02002AEE RID: 10990
			public class DOCTOREDOFFCOTEFFECT
			{
				// Token: 0x0400B2E6 RID: 45798
				public static LocString NAME = "Runaway Patient";

				// Token: 0x0400B2E7 RID: 45799
				public static LocString TOOLTIP = "Tsk tsk!\nThis Duplicant cannot receive treatment while out of their medical bed!";
			}

			// Token: 0x02002AEF RID: 10991
			public class POSTDISEASERECOVERY
			{
				// Token: 0x0400B2E8 RID: 45800
				public static LocString NAME = "Feeling better";

				// Token: 0x0400B2E9 RID: 45801
				public static LocString TOOLTIP = "This Duplicant is up and about, but they still have some lingering effects from their " + UI.PRE_KEYWORD + "Disease" + UI.PST_KEYWORD;

				// Token: 0x0400B2EA RID: 45802
				public static LocString ADDITIONAL_EFFECTS = "This Duplicant has temporary immunity to diseases from having beaten an infection";
			}

			// Token: 0x02002AF0 RID: 10992
			public class IMMUNESYSTEMOVERWHELMED
			{
				// Token: 0x0400B2EB RID: 45803
				public static LocString NAME = "Immune System Overwhelmed";

				// Token: 0x0400B2EC RID: 45804
				public static LocString TOOLTIP = "This Duplicant's immune system is slowly being overwhelmed by a high concentration of germs";
			}

			// Token: 0x02002AF1 RID: 10993
			public class MEDICINE_GENERICPILL
			{
				// Token: 0x0400B2ED RID: 45805
				public static LocString NAME = "Placebo";

				// Token: 0x0400B2EE RID: 45806
				public static LocString TOOLTIP = ITEMS.PILLS.PLACEBO.DESC;

				// Token: 0x0400B2EF RID: 45807
				public static LocString EFFECT_DESC = string.Concat(new string[]
				{
					"Applies the ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" effect"
				});
			}

			// Token: 0x02002AF2 RID: 10994
			public class MEDICINE_BASICBOOSTER
			{
				// Token: 0x0400B2F0 RID: 45808
				public static LocString NAME = ITEMS.PILLS.BASICBOOSTER.NAME;

				// Token: 0x0400B2F1 RID: 45809
				public static LocString TOOLTIP = ITEMS.PILLS.BASICBOOSTER.DESC;
			}

			// Token: 0x02002AF3 RID: 10995
			public class MEDICINE_INTERMEDIATEBOOSTER
			{
				// Token: 0x0400B2F2 RID: 45810
				public static LocString NAME = ITEMS.PILLS.INTERMEDIATEBOOSTER.NAME;

				// Token: 0x0400B2F3 RID: 45811
				public static LocString TOOLTIP = ITEMS.PILLS.INTERMEDIATEBOOSTER.DESC;
			}

			// Token: 0x02002AF4 RID: 10996
			public class MEDICINE_BASICRADPILL
			{
				// Token: 0x0400B2F4 RID: 45812
				public static LocString NAME = ITEMS.PILLS.BASICRADPILL.NAME;

				// Token: 0x0400B2F5 RID: 45813
				public static LocString TOOLTIP = ITEMS.PILLS.BASICRADPILL.DESC;
			}

			// Token: 0x02002AF5 RID: 10997
			public class MEDICINE_INTERMEDIATERADPILL
			{
				// Token: 0x0400B2F6 RID: 45814
				public static LocString NAME = ITEMS.PILLS.INTERMEDIATERADPILL.NAME;

				// Token: 0x0400B2F7 RID: 45815
				public static LocString TOOLTIP = ITEMS.PILLS.INTERMEDIATERADPILL.DESC;
			}

			// Token: 0x02002AF6 RID: 10998
			public class SUNLIGHT_PLEASANT
			{
				// Token: 0x0400B2F8 RID: 45816
				public static LocString NAME = "Bright and Cheerful";

				// Token: 0x0400B2F9 RID: 45817
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The strong natural ",
					UI.PRE_KEYWORD,
					"Light",
					UI.PST_KEYWORD,
					" is making this Duplicant feel light on their feet"
				});
			}

			// Token: 0x02002AF7 RID: 10999
			public class SUNLIGHT_BURNING
			{
				// Token: 0x0400B2FA RID: 45818
				public static LocString NAME = "Intensely Bright";

				// Token: 0x0400B2FB RID: 45819
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The bright ",
					UI.PRE_KEYWORD,
					"Light",
					UI.PST_KEYWORD,
					" is significantly improving this Duplicant's mood, but prolonged exposure may result in burning"
				});
			}

			// Token: 0x02002AF8 RID: 11000
			public class TOOKABREAK
			{
				// Token: 0x0400B2FC RID: 45820
				public static LocString NAME = "Downtime";

				// Token: 0x0400B2FD RID: 45821
				public static LocString TOOLTIP = "This Duplicant has a bit of time off from work to attend to personal matters";
			}

			// Token: 0x02002AF9 RID: 11001
			public class SOCIALIZED
			{
				// Token: 0x0400B2FE RID: 45822
				public static LocString NAME = "Socialized";

				// Token: 0x0400B2FF RID: 45823
				public static LocString TOOLTIP = "This Duplicant had some free time to hang out with buddies";
			}

			// Token: 0x02002AFA RID: 11002
			public class GOODCONVERSATION
			{
				// Token: 0x0400B300 RID: 45824
				public static LocString NAME = "Pleasant Chitchat";

				// Token: 0x0400B301 RID: 45825
				public static LocString TOOLTIP = "This Duplicant recently had a chance to chat with a friend";
			}

			// Token: 0x02002AFB RID: 11003
			public class WORKENCOURAGED
			{
				// Token: 0x0400B302 RID: 45826
				public static LocString NAME = "Appreciated";

				// Token: 0x0400B303 RID: 45827
				public static LocString TOOLTIP = "Someone saw how hard this Duplicant was working and gave them a compliment\n\nThis Duplicant feels great about themselves now!";
			}

			// Token: 0x02002AFC RID: 11004
			public class ISSPARKLESTREAKER
			{
				// Token: 0x0400B304 RID: 45828
				public static LocString NAME = "Sparkle Streaking";

				// Token: 0x0400B305 RID: 45829
				public static LocString TOOLTIP = "This Duplicant is currently Sparkle Streaking!\n\nBaa-ling!";
			}

			// Token: 0x02002AFD RID: 11005
			public class SAWSPARKLESTREAKER
			{
				// Token: 0x0400B306 RID: 45830
				public static LocString NAME = "Sparkle Flattered";

				// Token: 0x0400B307 RID: 45831
				public static LocString TOOLTIP = "A Sparkle Streaker's sparkles dazzled this Duplicant\n\nThis Duplicant has a spring in their step now!";
			}

			// Token: 0x02002AFE RID: 11006
			public class ISJOYSINGER
			{
				// Token: 0x0400B308 RID: 45832
				public static LocString NAME = "Yodeling";

				// Token: 0x0400B309 RID: 45833
				public static LocString TOOLTIP = "This Duplicant is currently Yodeling!";
			}

			// Token: 0x02002AFF RID: 11007
			public class HEARDJOYSINGER
			{
				// Token: 0x0400B30A RID: 45834
				public static LocString NAME = "Serenaded";

				// Token: 0x0400B30B RID: 45835
				public static LocString TOOLTIP = "A Yodeler's singing thrilled this Duplicant\n\nThis Duplicant works at a higher tempo now!";
			}

			// Token: 0x02002B00 RID: 11008
			public class HASBALLOON
			{
				// Token: 0x0400B30C RID: 45836
				public static LocString NAME = "Balloon Buddy";

				// Token: 0x0400B30D RID: 45837
				public static LocString TOOLTIP = "A Balloon Artist gave this Duplicant a balloon!\n\nThis Duplicant feels super crafty now!";
			}

			// Token: 0x02002B01 RID: 11009
			public class GREETING
			{
				// Token: 0x0400B30E RID: 45838
				public static LocString NAME = "Saw Friend";

				// Token: 0x0400B30F RID: 45839
				public static LocString TOOLTIP = "This Duplicant recently saw a friend in the halls and got to say \"hi\"\n\nIt wasn't even awkward!";
			}

			// Token: 0x02002B02 RID: 11010
			public class HUGGED
			{
				// Token: 0x0400B310 RID: 45840
				public static LocString NAME = "Hugged";

				// Token: 0x0400B311 RID: 45841
				public static LocString TOOLTIP = "This Duplicant recently received a hug from a friendly critter\n\nIt was so fluffy!";
			}

			// Token: 0x02002B03 RID: 11011
			public class ARCADEPLAYING
			{
				// Token: 0x0400B312 RID: 45842
				public static LocString NAME = "Gaming";

				// Token: 0x0400B313 RID: 45843
				public static LocString TOOLTIP = "This Duplicant is playing a video game\n\nIt looks like fun!";
			}

			// Token: 0x02002B04 RID: 11012
			public class PLAYEDARCADE
			{
				// Token: 0x0400B314 RID: 45844
				public static LocString NAME = "Played Video Games";

				// Token: 0x0400B315 RID: 45845
				public static LocString TOOLTIP = "This Duplicant recently played video games and is feeling like a champ";
			}

			// Token: 0x02002B05 RID: 11013
			public class DANCING
			{
				// Token: 0x0400B316 RID: 45846
				public static LocString NAME = "Dancing";

				// Token: 0x0400B317 RID: 45847
				public static LocString TOOLTIP = "This Duplicant is showing off their best moves.";
			}

			// Token: 0x02002B06 RID: 11014
			public class DANCED
			{
				// Token: 0x0400B318 RID: 45848
				public static LocString NAME = "Recently Danced";

				// Token: 0x0400B319 RID: 45849
				public static LocString TOOLTIP = "This Duplicant had a chance to cut loose!\n\nLeisure activities increase Duplicants' " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B07 RID: 11015
			public class JUICER
			{
				// Token: 0x0400B31A RID: 45850
				public static LocString NAME = "Drank Juice";

				// Token: 0x0400B31B RID: 45851
				public static LocString TOOLTIP = "This Duplicant had delicious fruity drink!\n\nLeisure activities increase Duplicants' " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B08 RID: 11016
			public class ESPRESSO
			{
				// Token: 0x0400B31C RID: 45852
				public static LocString NAME = "Drank Espresso";

				// Token: 0x0400B31D RID: 45853
				public static LocString TOOLTIP = "This Duplicant had delicious drink!\n\nLeisure activities increase Duplicants' " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B09 RID: 11017
			public class MECHANICALSURFBOARD
			{
				// Token: 0x0400B31E RID: 45854
				public static LocString NAME = "Stoked";

				// Token: 0x0400B31F RID: 45855
				public static LocString TOOLTIP = "This Duplicant had a rad experience on a surfboard.\n\nLeisure activities increase Duplicants' " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B0A RID: 11018
			public class MECHANICALSURFING
			{
				// Token: 0x0400B320 RID: 45856
				public static LocString NAME = "Surfin'";

				// Token: 0x0400B321 RID: 45857
				public static LocString TOOLTIP = "This Duplicant is surfin' some artificial waves!";
			}

			// Token: 0x02002B0B RID: 11019
			public class SAUNA
			{
				// Token: 0x0400B322 RID: 45858
				public static LocString NAME = "Steam Powered";

				// Token: 0x0400B323 RID: 45859
				public static LocString TOOLTIP = "This Duplicant just had a relaxing time in a sauna\n\nLeisure activities increase Duplicants' " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B0C RID: 11020
			public class SAUNARELAXING
			{
				// Token: 0x0400B324 RID: 45860
				public static LocString NAME = "Relaxing";

				// Token: 0x0400B325 RID: 45861
				public static LocString TOOLTIP = "This Duplicant is relaxing in a sauna";
			}

			// Token: 0x02002B0D RID: 11021
			public class HOTTUB
			{
				// Token: 0x0400B326 RID: 45862
				public static LocString NAME = "Hot Tubbed";

				// Token: 0x0400B327 RID: 45863
				public static LocString TOOLTIP = "This Duplicant recently unwound in a Hot Tub\n\nLeisure activities increase Duplicants' " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B0E RID: 11022
			public class HOTTUBRELAXING
			{
				// Token: 0x0400B328 RID: 45864
				public static LocString NAME = "Relaxing";

				// Token: 0x0400B329 RID: 45865
				public static LocString TOOLTIP = "This Duplicant is unwinding in a hot tub\n\nThey sure look relaxed";
			}

			// Token: 0x02002B0F RID: 11023
			public class SODAFOUNTAIN
			{
				// Token: 0x0400B32A RID: 45866
				public static LocString NAME = "Soda Filled";

				// Token: 0x0400B32B RID: 45867
				public static LocString TOOLTIP = "This Duplicant just enjoyed a bubbly beverage\n\nLeisure activities increase Duplicants' " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B10 RID: 11024
			public class VERTICALWINDTUNNELFLYING
			{
				// Token: 0x0400B32C RID: 45868
				public static LocString NAME = "Airborne";

				// Token: 0x0400B32D RID: 45869
				public static LocString TOOLTIP = "This Duplicant is having an exhilarating time in the wind tunnel\n\nWhoosh!";
			}

			// Token: 0x02002B11 RID: 11025
			public class VERTICALWINDTUNNEL
			{
				// Token: 0x0400B32E RID: 45870
				public static LocString NAME = "Wind Swept";

				// Token: 0x0400B32F RID: 45871
				public static LocString TOOLTIP = "This Duplicant recently had an exhilarating wind tunnel experience\n\nLeisure activities increase Duplicants' " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B12 RID: 11026
			public class BEACHCHAIRRELAXING
			{
				// Token: 0x0400B330 RID: 45872
				public static LocString NAME = "Totally Chill";

				// Token: 0x0400B331 RID: 45873
				public static LocString TOOLTIP = "This Duplicant is totally chillin' in a beach chair";
			}

			// Token: 0x02002B13 RID: 11027
			public class BEACHCHAIRLIT
			{
				// Token: 0x0400B332 RID: 45874
				public static LocString NAME = "Sun Kissed";

				// Token: 0x0400B333 RID: 45875
				public static LocString TOOLTIP = "This Duplicant had an amazing experience at the Beach\n\nLeisure activities increase Duplicants' " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B14 RID: 11028
			public class BEACHCHAIRUNLIT
			{
				// Token: 0x0400B334 RID: 45876
				public static LocString NAME = "Passably Relaxed";

				// Token: 0x0400B335 RID: 45877
				public static LocString TOOLTIP = "This Duplicant just had a mediocre beach experience\n\nLeisure activities increase Duplicants' " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B15 RID: 11029
			public class TELEPHONECHAT
			{
				// Token: 0x0400B336 RID: 45878
				public static LocString NAME = "Full of Gossip";

				// Token: 0x0400B337 RID: 45879
				public static LocString TOOLTIP = "This Duplicant chatted on the phone with at least one other Duplicant\n\nLeisure activities increase Duplicants' " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B16 RID: 11030
			public class TELEPHONEBABBLE
			{
				// Token: 0x0400B338 RID: 45880
				public static LocString NAME = "Less Anxious";

				// Token: 0x0400B339 RID: 45881
				public static LocString TOOLTIP = "This Duplicant got some things off their chest by talking to themselves on the phone\n\nLeisure activities increase Duplicants' " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B17 RID: 11031
			public class TELEPHONELONGDISTANCE
			{
				// Token: 0x0400B33A RID: 45882
				public static LocString NAME = "Sociable";

				// Token: 0x0400B33B RID: 45883
				public static LocString TOOLTIP = "This Duplicant is feeling sociable after chatting on the phone with someone across space\n\nLeisure activities increase Duplicants' " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B18 RID: 11032
			public class EDIBLEMINUS3
			{
				// Token: 0x0400B33C RID: 45884
				public static LocString NAME = "Grisly Meal";

				// Token: 0x0400B33D RID: 45885
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The food this Duplicant last ate was ",
					UI.PRE_KEYWORD,
					"Grisly",
					UI.PST_KEYWORD,
					"\n\nThey hope their next meal will be better"
				});
			}

			// Token: 0x02002B19 RID: 11033
			public class EDIBLEMINUS2
			{
				// Token: 0x0400B33E RID: 45886
				public static LocString NAME = "Terrible Meal";

				// Token: 0x0400B33F RID: 45887
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The food this Duplicant last ate was ",
					UI.PRE_KEYWORD,
					"Terrible",
					UI.PST_KEYWORD,
					"\n\nThey hope their next meal will be better"
				});
			}

			// Token: 0x02002B1A RID: 11034
			public class EDIBLEMINUS1
			{
				// Token: 0x0400B340 RID: 45888
				public static LocString NAME = "Poor Meal";

				// Token: 0x0400B341 RID: 45889
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The food this Duplicant last ate was ",
					UI.PRE_KEYWORD,
					"Poor",
					UI.PST_KEYWORD,
					"\n\nThey hope their next meal will be a little better"
				});
			}

			// Token: 0x02002B1B RID: 11035
			public class EDIBLE0
			{
				// Token: 0x0400B342 RID: 45890
				public static LocString NAME = "Standard Meal";

				// Token: 0x0400B343 RID: 45891
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The food this Duplicant last ate was ",
					UI.PRE_KEYWORD,
					"Average",
					UI.PST_KEYWORD,
					"\n\nThey thought it was sort of okay"
				});
			}

			// Token: 0x02002B1C RID: 11036
			public class EDIBLE1
			{
				// Token: 0x0400B344 RID: 45892
				public static LocString NAME = "Good Meal";

				// Token: 0x0400B345 RID: 45893
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The food this Duplicant last ate was ",
					UI.PRE_KEYWORD,
					"Good",
					UI.PST_KEYWORD,
					"\n\nThey thought it was pretty good!"
				});
			}

			// Token: 0x02002B1D RID: 11037
			public class EDIBLE2
			{
				// Token: 0x0400B346 RID: 45894
				public static LocString NAME = "Great Meal";

				// Token: 0x0400B347 RID: 45895
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The food this Duplicant last ate was ",
					UI.PRE_KEYWORD,
					"Great",
					UI.PST_KEYWORD,
					"\n\nThey thought it was pretty good!"
				});
			}

			// Token: 0x02002B1E RID: 11038
			public class EDIBLE3
			{
				// Token: 0x0400B348 RID: 45896
				public static LocString NAME = "Superb Meal";

				// Token: 0x0400B349 RID: 45897
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The food this Duplicant last ate was ",
					UI.PRE_KEYWORD,
					"Superb",
					UI.PST_KEYWORD,
					"\n\nThey thought it was really good!"
				});
			}

			// Token: 0x02002B1F RID: 11039
			public class EDIBLE4
			{
				// Token: 0x0400B34A RID: 45898
				public static LocString NAME = "Ambrosial Meal";

				// Token: 0x0400B34B RID: 45899
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The food this Duplicant last ate was ",
					UI.PRE_KEYWORD,
					"Ambrosial",
					UI.PST_KEYWORD,
					"\n\nThey thought it was super tasty!"
				});
			}

			// Token: 0x02002B20 RID: 11040
			public class DECORMINUS1
			{
				// Token: 0x0400B34C RID: 45900
				public static LocString NAME = "Last Cycle's Decor: Ugly";

				// Token: 0x0400B34D RID: 45901
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant thought the overall ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" yesterday was downright depressing"
				});
			}

			// Token: 0x02002B21 RID: 11041
			public class DECOR0
			{
				// Token: 0x0400B34E RID: 45902
				public static LocString NAME = "Last Cycle's Decor: Poor";

				// Token: 0x0400B34F RID: 45903
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant thought the overall ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" yesterday was quite poor"
				});
			}

			// Token: 0x02002B22 RID: 11042
			public class DECOR1
			{
				// Token: 0x0400B350 RID: 45904
				public static LocString NAME = "Last Cycle's Decor: Mediocre";

				// Token: 0x0400B351 RID: 45905
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant had no strong opinions about the colony's ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" yesterday"
				});
			}

			// Token: 0x02002B23 RID: 11043
			public class DECOR2
			{
				// Token: 0x0400B352 RID: 45906
				public static LocString NAME = "Last Cycle's Decor: Average";

				// Token: 0x0400B353 RID: 45907
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant thought the overall ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" yesterday was pretty alright"
				});
			}

			// Token: 0x02002B24 RID: 11044
			public class DECOR3
			{
				// Token: 0x0400B354 RID: 45908
				public static LocString NAME = "Last Cycle's Decor: Nice";

				// Token: 0x0400B355 RID: 45909
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant thought the overall ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" yesterday was quite nice!"
				});
			}

			// Token: 0x02002B25 RID: 11045
			public class DECOR4
			{
				// Token: 0x0400B356 RID: 45910
				public static LocString NAME = "Last Cycle's Decor: Charming";

				// Token: 0x0400B357 RID: 45911
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant thought the overall ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" yesterday was downright charming!"
				});
			}

			// Token: 0x02002B26 RID: 11046
			public class DECOR5
			{
				// Token: 0x0400B358 RID: 45912
				public static LocString NAME = "Last Cycle's Decor: Gorgeous";

				// Token: 0x0400B359 RID: 45913
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant thought the overall ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" yesterday was fantastic\n\nThey love what I've done with the place!"
				});
			}

			// Token: 0x02002B27 RID: 11047
			public class BREAK1
			{
				// Token: 0x0400B35A RID: 45914
				public static LocString NAME = "One Shift Break";

				// Token: 0x0400B35B RID: 45915
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant has had one ",
					UI.PRE_KEYWORD,
					"Downtime",
					UI.PST_KEYWORD,
					" shift in the last cycle"
				});
			}

			// Token: 0x02002B28 RID: 11048
			public class BREAK2
			{
				// Token: 0x0400B35C RID: 45916
				public static LocString NAME = "Two Shift Break";

				// Token: 0x0400B35D RID: 45917
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant has had two ",
					UI.PRE_KEYWORD,
					"Downtime",
					UI.PST_KEYWORD,
					" shifts in the last cycle"
				});
			}

			// Token: 0x02002B29 RID: 11049
			public class BREAK3
			{
				// Token: 0x0400B35E RID: 45918
				public static LocString NAME = "Three Shift Break";

				// Token: 0x0400B35F RID: 45919
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant has had three ",
					UI.PRE_KEYWORD,
					"Downtime",
					UI.PST_KEYWORD,
					" shifts in the last cycle"
				});
			}

			// Token: 0x02002B2A RID: 11050
			public class BREAK4
			{
				// Token: 0x0400B360 RID: 45920
				public static LocString NAME = "Four Shift Break";

				// Token: 0x0400B361 RID: 45921
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant has had four ",
					UI.PRE_KEYWORD,
					"Downtime",
					UI.PST_KEYWORD,
					" shifts in the last cycle"
				});
			}

			// Token: 0x02002B2B RID: 11051
			public class BREAK5
			{
				// Token: 0x0400B362 RID: 45922
				public static LocString NAME = "Five Shift Break";

				// Token: 0x0400B363 RID: 45923
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant has had five ",
					UI.PRE_KEYWORD,
					"Downtime",
					UI.PST_KEYWORD,
					" shifts in the last cycle"
				});
			}

			// Token: 0x02002B2C RID: 11052
			public class POWERTINKER
			{
				// Token: 0x0400B364 RID: 45924
				public static LocString NAME = "Engie's Tune-Up";

				// Token: 0x0400B365 RID: 45925
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A skilled Duplicant has improved this generator's ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" output efficiency\n\nApplying this effect consumed one ",
					UI.PRE_KEYWORD,
					ITEMS.INDUSTRIAL_PRODUCTS.POWER_STATION_TOOLS.NAME,
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002B2D RID: 11053
			public class FARMTINKER
			{
				// Token: 0x0400B366 RID: 45926
				public static LocString NAME = "Farmer's Touch";

				// Token: 0x0400B367 RID: 45927
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A skilled Duplicant has encouraged this ",
					UI.PRE_KEYWORD,
					"Plant",
					UI.PST_KEYWORD,
					" to grow a little bit faster\n\nApplying this effect consumed one dose of ",
					UI.PRE_KEYWORD,
					ITEMS.INDUSTRIAL_PRODUCTS.FARM_STATION_TOOLS.NAME,
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002B2E RID: 11054
			public class MACHINETINKER
			{
				// Token: 0x0400B368 RID: 45928
				public static LocString NAME = "Engie's Jerry Rig";

				// Token: 0x0400B369 RID: 45929
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A skilled Duplicant has jerry rigged this ",
					UI.PRE_KEYWORD,
					"Generator",
					UI.PST_KEYWORD,
					" to temporarily run faster"
				});
			}

			// Token: 0x02002B2F RID: 11055
			public class SPACETOURIST
			{
				// Token: 0x0400B36A RID: 45930
				public static LocString NAME = "Visited Space";

				// Token: 0x0400B36B RID: 45931
				public static LocString TOOLTIP = "This Duplicant went on a trip to space and saw the wonders of the universe";
			}

			// Token: 0x02002B30 RID: 11056
			public class SUDDENMORALEHELPER
			{
				// Token: 0x0400B36C RID: 45932
				public static LocString NAME = "Morale Upgrade Helper";

				// Token: 0x0400B36D RID: 45933
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant will receive a temporary ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" bonus to buffer the new ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" system introduction"
				});
			}

			// Token: 0x02002B31 RID: 11057
			public class EXPOSEDTOFOODGERMS
			{
				// Token: 0x0400B36E RID: 45934
				public static LocString NAME = "Food Poisoning Exposure";

				// Token: 0x0400B36F RID: 45935
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant was exposed to ",
					DUPLICANTS.DISEASES.FOODPOISONING.NAME,
					" Germs and is at risk of developing the ",
					UI.PRE_KEYWORD,
					"Disease",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002B32 RID: 11058
			public class EXPOSEDTOSLIMEGERMS
			{
				// Token: 0x0400B370 RID: 45936
				public static LocString NAME = "Slimelung Exposure";

				// Token: 0x0400B371 RID: 45937
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant was exposed to ",
					DUPLICANTS.DISEASES.SLIMELUNG.NAME,
					" and is at risk of developing the ",
					UI.PRE_KEYWORD,
					"Disease",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002B33 RID: 11059
			public class EXPOSEDTOZOMBIESPORES
			{
				// Token: 0x0400B372 RID: 45938
				public static LocString NAME = "Zombie Spores Exposure";

				// Token: 0x0400B373 RID: 45939
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant was exposed to ",
					DUPLICANTS.DISEASES.ZOMBIESPORES.NAME,
					" and is at risk of developing the ",
					UI.PRE_KEYWORD,
					"Disease",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002B34 RID: 11060
			public class FEELINGSICKFOODGERMS
			{
				// Token: 0x0400B374 RID: 45940
				public static LocString NAME = "Contracted: Food Poisoning";

				// Token: 0x0400B375 RID: 45941
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant contracted ",
					DUPLICANTS.DISEASES.FOODSICKNESS.NAME,
					" after a recent ",
					UI.PRE_KEYWORD,
					"Germ",
					UI.PST_KEYWORD,
					" exposure and will begin exhibiting symptoms shortly"
				});
			}

			// Token: 0x02002B35 RID: 11061
			public class FEELINGSICKSLIMEGERMS
			{
				// Token: 0x0400B376 RID: 45942
				public static LocString NAME = "Contracted: Slimelung";

				// Token: 0x0400B377 RID: 45943
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant contracted ",
					DUPLICANTS.DISEASES.SLIMESICKNESS.NAME,
					" after a recent ",
					UI.PRE_KEYWORD,
					"Germ",
					UI.PST_KEYWORD,
					" exposure and will begin exhibiting symptoms shortly"
				});
			}

			// Token: 0x02002B36 RID: 11062
			public class FEELINGSICKZOMBIESPORES
			{
				// Token: 0x0400B378 RID: 45944
				public static LocString NAME = "Contracted: Zombie Spores";

				// Token: 0x0400B379 RID: 45945
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant contracted ",
					DUPLICANTS.DISEASES.ZOMBIESICKNESS.NAME,
					" after a recent ",
					UI.PRE_KEYWORD,
					"Germ",
					UI.PST_KEYWORD,
					" exposure and will begin exhibiting symptoms shortly"
				});
			}

			// Token: 0x02002B37 RID: 11063
			public class SMELLEDFLOWERS
			{
				// Token: 0x0400B37A RID: 45946
				public static LocString NAME = "Smelled Flowers";

				// Token: 0x0400B37B RID: 45947
				public static LocString TOOLTIP = "A pleasant " + DUPLICANTS.DISEASES.POLLENGERMS.NAME + " wafted over this Duplicant and brightened their day";
			}

			// Token: 0x02002B38 RID: 11064
			public class HISTAMINESUPPRESSION
			{
				// Token: 0x0400B37C RID: 45948
				public static LocString NAME = "Antihistamines";

				// Token: 0x0400B37D RID: 45949
				public static LocString TOOLTIP = "This Duplicant's allergic reactions have been suppressed by medication";
			}

			// Token: 0x02002B39 RID: 11065
			public class FOODSICKNESSRECOVERY
			{
				// Token: 0x0400B37E RID: 45950
				public static LocString NAME = "Food Poisoning Antibodies";

				// Token: 0x0400B37F RID: 45951
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant recently recovered from ",
					DUPLICANTS.DISEASES.FOODSICKNESS.NAME,
					" and is temporarily immune to the ",
					UI.PRE_KEYWORD,
					"Disease",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002B3A RID: 11066
			public class SLIMESICKNESSRECOVERY
			{
				// Token: 0x0400B380 RID: 45952
				public static LocString NAME = "Slimelung Antibodies";

				// Token: 0x0400B381 RID: 45953
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant recently recovered from ",
					DUPLICANTS.DISEASES.SLIMESICKNESS.NAME,
					" and is temporarily immune to the ",
					UI.PRE_KEYWORD,
					"Disease",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002B3B RID: 11067
			public class ZOMBIESICKNESSRECOVERY
			{
				// Token: 0x0400B382 RID: 45954
				public static LocString NAME = "Zombie Spores Antibodies";

				// Token: 0x0400B383 RID: 45955
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant recently recovered from ",
					DUPLICANTS.DISEASES.ZOMBIESICKNESS.NAME,
					" and is temporarily immune to the ",
					UI.PRE_KEYWORD,
					"Disease",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002B3C RID: 11068
			public class MESSTABLESALT
			{
				// Token: 0x0400B384 RID: 45956
				public static LocString NAME = "Salted Food";

				// Token: 0x0400B385 RID: 45957
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant had the luxury of using ",
					UI.PRE_KEYWORD,
					ITEMS.INDUSTRIAL_PRODUCTS.TABLE_SALT.NAME,
					UI.PST_KEYWORD,
					" with their last meal at a ",
					BUILDINGS.PREFABS.DININGTABLE.NAME
				});
			}

			// Token: 0x02002B3D RID: 11069
			public class RADIATIONEXPOSUREMINOR
			{
				// Token: 0x0400B386 RID: 45958
				public static LocString NAME = "Minor Radiation Sickness";

				// Token: 0x0400B387 RID: 45959
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A bit of ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" exposure has made this Duplicant feel sluggish"
				});
			}

			// Token: 0x02002B3E RID: 11070
			public class RADIATIONEXPOSUREMAJOR
			{
				// Token: 0x0400B388 RID: 45960
				public static LocString NAME = "Major Radiation Sickness";

				// Token: 0x0400B389 RID: 45961
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Significant ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" exposure has left this Duplicant totally exhausted"
				});
			}

			// Token: 0x02002B3F RID: 11071
			public class RADIATIONEXPOSUREEXTREME
			{
				// Token: 0x0400B38A RID: 45962
				public static LocString NAME = "Extreme Radiation Sickness";

				// Token: 0x0400B38B RID: 45963
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Dangerously high ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" exposure is making this Duplicant wish they'd never been printed"
				});
			}

			// Token: 0x02002B40 RID: 11072
			public class RADIATIONEXPOSUREDEADLY
			{
				// Token: 0x0400B38C RID: 45964
				public static LocString NAME = "Deadly Radiation Sickness";

				// Token: 0x0400B38D RID: 45965
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Extreme ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" exposure has incapacitated this Duplicant"
				});
			}

			// Token: 0x02002B41 RID: 11073
			public class CHARGING
			{
				// Token: 0x0400B38E RID: 45966
				public static LocString NAME = "Charging";

				// Token: 0x0400B38F RID: 45967
				public static LocString TOOLTIP = "This lil bot is charging its internal battery";
			}

			// Token: 0x02002B42 RID: 11074
			public class BOTSWEEPING
			{
				// Token: 0x0400B390 RID: 45968
				public static LocString NAME = "Sweeping";

				// Token: 0x0400B391 RID: 45969
				public static LocString TOOLTIP = "This lil bot is picking up debris from the floor";
			}

			// Token: 0x02002B43 RID: 11075
			public class BOTMOPPING
			{
				// Token: 0x0400B392 RID: 45970
				public static LocString NAME = "Mopping";

				// Token: 0x0400B393 RID: 45971
				public static LocString TOOLTIP = "This lil bot is clearing liquids from the ground";
			}

			// Token: 0x02002B44 RID: 11076
			public class SCOUTBOTCHARGING
			{
				// Token: 0x0400B394 RID: 45972
				public static LocString NAME = "Charging";

				// Token: 0x0400B395 RID: 45973
				public static LocString TOOLTIP = ROBOTS.MODELS.SCOUT.NAME + " is happily charging inside " + BUILDINGS.PREFABS.SCOUTMODULE.NAME;
			}

			// Token: 0x02002B45 RID: 11077
			public class CRYOFRIEND
			{
				// Token: 0x0400B396 RID: 45974
				public static LocString NAME = "Motivated By Friend";

				// Token: 0x0400B397 RID: 45975
				public static LocString TOOLTIP = "This Duplicant feels motivated after meeting a long lost friend";
			}

			// Token: 0x02002B46 RID: 11078
			public class BONUSDREAM1
			{
				// Token: 0x0400B398 RID: 45976
				public static LocString NAME = "Good Dream";

				// Token: 0x0400B399 RID: 45977
				public static LocString TOOLTIP = "This Duplicant had a good dream and is feeling psyched!";
			}

			// Token: 0x02002B47 RID: 11079
			public class BONUSDREAM2
			{
				// Token: 0x0400B39A RID: 45978
				public static LocString NAME = "Really Good Dream";

				// Token: 0x0400B39B RID: 45979
				public static LocString TOOLTIP = "This Duplicant had a really good dream and is full of possibilities!";
			}

			// Token: 0x02002B48 RID: 11080
			public class BONUSDREAM3
			{
				// Token: 0x0400B39C RID: 45980
				public static LocString NAME = "Great Dream";

				// Token: 0x0400B39D RID: 45981
				public static LocString TOOLTIP = "This Duplicant had a great dream last night and periodically remembers another great moment they previously forgot";
			}

			// Token: 0x02002B49 RID: 11081
			public class BONUSDREAM4
			{
				// Token: 0x0400B39E RID: 45982
				public static LocString NAME = "Dream Inspired";

				// Token: 0x0400B39F RID: 45983
				public static LocString TOOLTIP = "This Duplicant is inspired from all the unforgettable dreams they had";
			}

			// Token: 0x02002B4A RID: 11082
			public class BONUSRESEARCH
			{
				// Token: 0x0400B3A0 RID: 45984
				public static LocString NAME = "Inspired Learner";

				// Token: 0x0400B3A1 RID: 45985
				public static LocString TOOLTIP = "This Duplicant is looking forward to some learning";
			}

			// Token: 0x02002B4B RID: 11083
			public class BONUSTOILET1
			{
				// Token: 0x0400B3A2 RID: 45986
				public static LocString NAME = "Small Comforts";

				// Token: 0x0400B3A3 RID: 45987
				public static LocString TOOLTIP = "This Duplicant visited the {building} and appreciated the small comforts";
			}

			// Token: 0x02002B4C RID: 11084
			public class BONUSTOILET2
			{
				// Token: 0x0400B3A4 RID: 45988
				public static LocString NAME = "Greater Comforts";

				// Token: 0x0400B3A5 RID: 45989
				public static LocString TOOLTIP = "This Duplicant used a " + BUILDINGS.PREFABS.OUTHOUSE.NAME + "and liked how comfortable it felt";
			}

			// Token: 0x02002B4D RID: 11085
			public class BONUSTOILET3
			{
				// Token: 0x0400B3A6 RID: 45990
				public static LocString NAME = "Small Luxury";

				// Token: 0x0400B3A7 RID: 45991
				public static LocString TOOLTIP = "This Duplicant visited a " + ROOMS.TYPES.LATRINE.NAME + " and feels they could get used to this luxury";
			}

			// Token: 0x02002B4E RID: 11086
			public class BONUSTOILET4
			{
				// Token: 0x0400B3A8 RID: 45992
				public static LocString NAME = "Luxurious";

				// Token: 0x0400B3A9 RID: 45993
				public static LocString TOOLTIP = "This Duplicant feels endless luxury from the " + ROOMS.TYPES.PRIVATE_BATHROOM.NAME;
			}

			// Token: 0x02002B4F RID: 11087
			public class BONUSDIGGING1
			{
				// Token: 0x0400B3AA RID: 45994
				public static LocString NAME = "Hot Diggity!";

				// Token: 0x0400B3AB RID: 45995
				public static LocString TOOLTIP = "This Duplicant did a lot of excavating and is really digging digging";
			}

			// Token: 0x02002B50 RID: 11088
			public class BONUSSTORAGE
			{
				// Token: 0x0400B3AC RID: 45996
				public static LocString NAME = "Something in Store";

				// Token: 0x0400B3AD RID: 45997
				public static LocString TOOLTIP = "This Duplicant stored something in a " + BUILDINGS.PREFABS.STORAGELOCKER.NAME + " and is feeling organized";
			}

			// Token: 0x02002B51 RID: 11089
			public class BONUSBUILDER
			{
				// Token: 0x0400B3AE RID: 45998
				public static LocString NAME = "Accomplished Builder";

				// Token: 0x0400B3AF RID: 45999
				public static LocString TOOLTIP = "This Duplicant has built many buildings and has a sense of accomplishment!";
			}

			// Token: 0x02002B52 RID: 11090
			public class BONUSOXYGEN
			{
				// Token: 0x0400B3B0 RID: 46000
				public static LocString NAME = "Fresh Air";

				// Token: 0x0400B3B1 RID: 46001
				public static LocString TOOLTIP = "This Duplicant breathed in some fresh air and is feeling refreshed";
			}

			// Token: 0x02002B53 RID: 11091
			public class BONUSGENERATOR
			{
				// Token: 0x0400B3B2 RID: 46002
				public static LocString NAME = "Exercised";

				// Token: 0x0400B3B3 RID: 46003
				public static LocString TOOLTIP = "This Duplicant ran in a Generator and has benefited from the exercise";
			}

			// Token: 0x02002B54 RID: 11092
			public class BONUSDOOR
			{
				// Token: 0x0400B3B4 RID: 46004
				public static LocString NAME = "Open and Shut";

				// Token: 0x0400B3B5 RID: 46005
				public static LocString TOOLTIP = "This Duplicant closed a door and appreciates the privacy";
			}

			// Token: 0x02002B55 RID: 11093
			public class BONUSHITTHEBOOKS
			{
				// Token: 0x0400B3B6 RID: 46006
				public static LocString NAME = "Hit the Books";

				// Token: 0x0400B3B7 RID: 46007
				public static LocString TOOLTIP = "This Duplicant did some research and is feeling smarter";
			}

			// Token: 0x02002B56 RID: 11094
			public class BONUSLITWORKSPACE
			{
				// Token: 0x0400B3B8 RID: 46008
				public static LocString NAME = "Lit";

				// Token: 0x0400B3B9 RID: 46009
				public static LocString TOOLTIP = "This Duplicant was in a well-lit environment and is feeling lit";
			}

			// Token: 0x02002B57 RID: 11095
			public class BONUSTALKER
			{
				// Token: 0x0400B3BA RID: 46010
				public static LocString NAME = "Talker";

				// Token: 0x0400B3BB RID: 46011
				public static LocString TOOLTIP = "This Duplicant engaged in small talk with a coworker and is feeling connected";
			}

			// Token: 0x02002B58 RID: 11096
			public class THRIVER
			{
				// Token: 0x0400B3BC RID: 46012
				public static LocString NAME = "Clutchy";

				// Token: 0x0400B3BD RID: 46013
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is ",
					UI.PRE_KEYWORD,
					"Stressed",
					UI.PST_KEYWORD,
					" and has kicked into hyperdrive"
				});
			}

			// Token: 0x02002B59 RID: 11097
			public class LONER
			{
				// Token: 0x0400B3BE RID: 46014
				public static LocString NAME = "Alone";

				// Token: 0x0400B3BF RID: 46015
				public static LocString TOOLTIP = "This Duplicant is more feeling focused now that they're alone";
			}

			// Token: 0x02002B5A RID: 11098
			public class STARRYEYED
			{
				// Token: 0x0400B3C0 RID: 46016
				public static LocString NAME = "Starry Eyed";

				// Token: 0x0400B3C1 RID: 46017
				public static LocString TOOLTIP = "This Duplicant loves being in space!";
			}

			// Token: 0x02002B5B RID: 11099
			public class WAILEDAT
			{
				// Token: 0x0400B3C2 RID: 46018
				public static LocString NAME = "Disturbed by Wailing";

				// Token: 0x0400B3C3 RID: 46019
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This Duplicant is feeling ",
					UI.PRE_KEYWORD,
					"Stressed",
					UI.PST_KEYWORD,
					" by someone's Banshee Wail"
				});
			}
		}

		// Token: 0x02001CE7 RID: 7399
		public class CONGENITALTRAITS
		{
			// Token: 0x02002B5C RID: 11100
			public class NONE
			{
				// Token: 0x0400B3C4 RID: 46020
				public static LocString NAME = "None";

				// Token: 0x0400B3C5 RID: 46021
				public static LocString DESC = "This Duplicant seems pretty average overall";
			}

			// Token: 0x02002B5D RID: 11101
			public class JOSHUA
			{
				// Token: 0x0400B3C6 RID: 46022
				public static LocString NAME = "Cheery Disposition";

				// Token: 0x0400B3C7 RID: 46023
				public static LocString DESC = "This Duplicant brightens others' days wherever he goes";
			}

			// Token: 0x02002B5E RID: 11102
			public class ELLIE
			{
				// Token: 0x0400B3C8 RID: 46024
				public static LocString NAME = "Fastidious";

				// Token: 0x0400B3C9 RID: 46025
				public static LocString DESC = "This Duplicant needs things done in a very particular way";
			}

			// Token: 0x02002B5F RID: 11103
			public class LIAM
			{
				// Token: 0x0400B3CA RID: 46026
				public static LocString NAME = "Germaphobe";

				// Token: 0x0400B3CB RID: 46027
				public static LocString DESC = "This Duplicant has an all-consuming fear of bacteria";
			}

			// Token: 0x02002B60 RID: 11104
			public class BANHI
			{
				// Token: 0x0400B3CC RID: 46028
				public static LocString NAME = "";

				// Token: 0x0400B3CD RID: 46029
				public static LocString DESC = "";
			}

			// Token: 0x02002B61 RID: 11105
			public class STINKY
			{
				// Token: 0x0400B3CE RID: 46030
				public static LocString NAME = "Stinkiness";

				// Token: 0x0400B3CF RID: 46031
				public static LocString DESC = "This Duplicant is genetically cursed by a pungent bodily odor";
			}
		}

		// Token: 0x02001CE8 RID: 7400
		public class TRAITS
		{
			// Token: 0x0400843F RID: 33855
			public static LocString TRAIT_DESCRIPTION_LIST_ENTRY = "\n• ";

			// Token: 0x04008440 RID: 33856
			public static LocString ATTRIBUTE_MODIFIERS = "{0}: {1}";

			// Token: 0x04008441 RID: 33857
			public static LocString CANNOT_DO_TASK = "Cannot do <b>{0} Errands</b>";

			// Token: 0x04008442 RID: 33858
			public static LocString CANNOT_DO_TASK_TOOLTIP = "{0}: {1}";

			// Token: 0x04008443 RID: 33859
			public static LocString REFUSES_TO_DO_TASK = "Cannot do <b>{0} Errands</b>";

			// Token: 0x04008444 RID: 33860
			public static LocString IGNORED_EFFECTS = "Immune to <b>{0}</b>";

			// Token: 0x04008445 RID: 33861
			public static LocString IGNORED_EFFECTS_TOOLTIP = "{0}: {1}";

			// Token: 0x04008446 RID: 33862
			public static LocString GRANTED_SKILL_SHARED_NAME = "Skilled: ";

			// Token: 0x04008447 RID: 33863
			public static LocString GRANTED_SKILL_SHARED_DESC = string.Concat(new string[]
			{
				"This Duplicant begins with a pre-learned ",
				UI.FormatAsKeyWord("Skill"),
				", but does not have increased ",
				UI.FormatAsKeyWord(DUPLICANTS.NEEDS.QUALITYOFLIFE.NAME),
				".\n\n{0}\n{1}"
			});

			// Token: 0x04008448 RID: 33864
			public static LocString GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP = "This Duplicant receives a free " + UI.FormatAsKeyWord("Skill") + " without the drawback of increased " + UI.FormatAsKeyWord(DUPLICANTS.NEEDS.QUALITYOFLIFE.NAME);

			// Token: 0x02002B62 RID: 11106
			public class CHATTY
			{
				// Token: 0x0400B3D0 RID: 46032
				public static LocString NAME = "Charismatic";

				// Token: 0x0400B3D1 RID: 46033
				public static LocString DESC = string.Concat(new string[]
				{
					"This Duplicant's so charming, chatting with them is sometimes enough to trigger an ",
					UI.PRE_KEYWORD,
					"Overjoyed",
					UI.PST_KEYWORD,
					" response"
				});
			}

			// Token: 0x02002B63 RID: 11107
			public class NEEDS
			{
				// Token: 0x02002FC7 RID: 12231
				public class CLAUSTROPHOBIC
				{
					// Token: 0x0400BF26 RID: 48934
					public static LocString NAME = "Claustrophobic";

					// Token: 0x0400BF27 RID: 48935
					public static LocString DESC = "This Duplicant feels suffocated in spaces fewer than four tiles high or three tiles wide";
				}

				// Token: 0x02002FC8 RID: 12232
				public class FASHIONABLE
				{
					// Token: 0x0400BF28 RID: 48936
					public static LocString NAME = "Fashionista";

					// Token: 0x0400BF29 RID: 48937
					public static LocString DESC = "This Duplicant dies a bit inside when forced to wear unstylish clothing";
				}

				// Token: 0x02002FC9 RID: 12233
				public class CLIMACOPHOBIC
				{
					// Token: 0x0400BF2A RID: 48938
					public static LocString NAME = "Vertigo Prone";

					// Token: 0x0400BF2B RID: 48939
					public static LocString DESC = "Climbing ladders more than four tiles tall makes this Duplicant's stomach do flips";
				}

				// Token: 0x02002FCA RID: 12234
				public class SOLITARYSLEEPER
				{
					// Token: 0x0400BF2C RID: 48940
					public static LocString NAME = "Solitary Sleeper";

					// Token: 0x0400BF2D RID: 48941
					public static LocString DESC = "This Duplicant prefers to sleep alone";
				}

				// Token: 0x02002FCB RID: 12235
				public class PREFERSWARMER
				{
					// Token: 0x0400BF2E RID: 48942
					public static LocString NAME = "Skinny";

					// Token: 0x0400BF2F RID: 48943
					public static LocString DESC = string.Concat(new string[]
					{
						"This Duplicant doesn't have much ",
						UI.PRE_KEYWORD,
						"Insulation",
						UI.PST_KEYWORD,
						", so they are more ",
						UI.PRE_KEYWORD,
						"Temperature",
						UI.PST_KEYWORD,
						" sensitive than others"
					});
				}

				// Token: 0x02002FCC RID: 12236
				public class PREFERSCOOLER
				{
					// Token: 0x0400BF30 RID: 48944
					public static LocString NAME = "Pudgy";

					// Token: 0x0400BF31 RID: 48945
					public static LocString DESC = string.Concat(new string[]
					{
						"This Duplicant has some extra ",
						UI.PRE_KEYWORD,
						"Insulation",
						UI.PST_KEYWORD,
						", so the room ",
						UI.PRE_KEYWORD,
						"Temperature",
						UI.PST_KEYWORD,
						" affects them a little less"
					});
				}

				// Token: 0x02002FCD RID: 12237
				public class SENSITIVEFEET
				{
					// Token: 0x0400BF32 RID: 48946
					public static LocString NAME = "Delicate Feetsies";

					// Token: 0x0400BF33 RID: 48947
					public static LocString DESC = "This Duplicant is a sensitive sole and would rather walk on tile than raw bedrock";
				}

				// Token: 0x02002FCE RID: 12238
				public class WORKAHOLIC
				{
					// Token: 0x0400BF34 RID: 48948
					public static LocString NAME = "Workaholic";

					// Token: 0x0400BF35 RID: 48949
					public static LocString DESC = "This Duplicant gets antsy when left idle";
				}
			}

			// Token: 0x02002B64 RID: 11108
			public class ANCIENTKNOWLEDGE
			{
				// Token: 0x0400B3D2 RID: 46034
				public static LocString NAME = "Ancient Knowledge";

				// Token: 0x0400B3D3 RID: 46035
				public static LocString DESC = "This Duplicant has knowledge from the before times\n• Starts with 3 skill points";
			}

			// Token: 0x02002B65 RID: 11109
			public class CANTRESEARCH
			{
				// Token: 0x0400B3D4 RID: 46036
				public static LocString NAME = "Yokel";

				// Token: 0x0400B3D5 RID: 46037
				public static LocString DESC = "This Duplicant isn't the brightest star in the solar system";
			}

			// Token: 0x02002B66 RID: 11110
			public class CANTBUILD
			{
				// Token: 0x0400B3D6 RID: 46038
				public static LocString NAME = "Unconstructive";

				// Token: 0x0400B3D7 RID: 46039
				public static LocString DESC = "This Duplicant is incapable of building even the most basic of structures";
			}

			// Token: 0x02002B67 RID: 11111
			public class CANTCOOK
			{
				// Token: 0x0400B3D8 RID: 46040
				public static LocString NAME = "Gastrophobia";

				// Token: 0x0400B3D9 RID: 46041
				public static LocString DESC = "This Duplicant has a deep-seated distrust of the culinary arts";
			}

			// Token: 0x02002B68 RID: 11112
			public class CANTDIG
			{
				// Token: 0x0400B3DA RID: 46042
				public static LocString NAME = "Trypophobia";

				// Token: 0x0400B3DB RID: 46043
				public static LocString DESC = "This Duplicant's fear of holes makes it impossible for them to dig";
			}

			// Token: 0x02002B69 RID: 11113
			public class HEMOPHOBIA
			{
				// Token: 0x0400B3DC RID: 46044
				public static LocString NAME = "Squeamish";

				// Token: 0x0400B3DD RID: 46045
				public static LocString DESC = "This Duplicant is of delicate disposition and cannot tend to the sick";
			}

			// Token: 0x02002B6A RID: 11114
			public class BEDSIDEMANNER
			{
				// Token: 0x0400B3DE RID: 46046
				public static LocString NAME = "Caregiver";

				// Token: 0x0400B3DF RID: 46047
				public static LocString DESC = "This Duplicant has good bedside manner and a healing touch";
			}

			// Token: 0x02002B6B RID: 11115
			public class MOUTHBREATHER
			{
				// Token: 0x0400B3E0 RID: 46048
				public static LocString NAME = "Mouth Breather";

				// Token: 0x0400B3E1 RID: 46049
				public static LocString DESC = "This Duplicant sucks up way more than their fair share of " + ELEMENTS.OXYGEN.NAME;
			}

			// Token: 0x02002B6C RID: 11116
			public class FUSSY
			{
				// Token: 0x0400B3E2 RID: 46050
				public static LocString NAME = "Fussy";

				// Token: 0x0400B3E3 RID: 46051
				public static LocString DESC = "Nothing's ever quite good enough for this Duplicant";
			}

			// Token: 0x02002B6D RID: 11117
			public class TWINKLETOES
			{
				// Token: 0x0400B3E4 RID: 46052
				public static LocString NAME = "Twinkletoes";

				// Token: 0x0400B3E5 RID: 46053
				public static LocString DESC = "This Duplicant is light as a feather on their feet";
			}

			// Token: 0x02002B6E RID: 11118
			public class STRONGARM
			{
				// Token: 0x0400B3E6 RID: 46054
				public static LocString NAME = "Buff";

				// Token: 0x0400B3E7 RID: 46055
				public static LocString DESC = "This Duplicant has muscles on their muscles";
			}

			// Token: 0x02002B6F RID: 11119
			public class NOODLEARMS
			{
				// Token: 0x0400B3E8 RID: 46056
				public static LocString NAME = "Noodle Arms";

				// Token: 0x0400B3E9 RID: 46057
				public static LocString DESC = "This Duplicant's arms have all the tensile strength of overcooked linguine";
			}

			// Token: 0x02002B70 RID: 11120
			public class AGGRESSIVE
			{
				// Token: 0x0400B3EA RID: 46058
				public static LocString NAME = "Destructive";

				// Token: 0x0400B3EB RID: 46059
				public static LocString DESC = "This Duplicant handles stress by taking their frustrations out on defenseless machines";

				// Token: 0x0400B3EC RID: 46060
				public static LocString NOREPAIR = "• Will not repair buildings while above 60% " + UI.PRE_KEYWORD + "Stress" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B71 RID: 11121
			public class UGLYCRIER
			{
				// Token: 0x0400B3ED RID: 46061
				public static LocString NAME = "Ugly Crier";

				// Token: 0x0400B3EE RID: 46062
				public static LocString DESC = string.Concat(new string[]
				{
					"If this Duplicant gets too ",
					UI.PRE_KEYWORD,
					"Stressed",
					UI.PST_KEYWORD,
					" it won't be pretty"
				});
			}

			// Token: 0x02002B72 RID: 11122
			public class BINGEEATER
			{
				// Token: 0x0400B3EF RID: 46063
				public static LocString NAME = "Binge Eater";

				// Token: 0x0400B3F0 RID: 46064
				public static LocString DESC = "This Duplicant will dangerously overeat when " + UI.PRE_KEYWORD + "Stressed" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B73 RID: 11123
			public class ANXIOUS
			{
				// Token: 0x0400B3F1 RID: 46065
				public static LocString NAME = "Anxious";

				// Token: 0x0400B3F2 RID: 46066
				public static LocString DESC = "This Duplicant collapses when put under too much pressure";
			}

			// Token: 0x02002B74 RID: 11124
			public class STRESSVOMITER
			{
				// Token: 0x0400B3F3 RID: 46067
				public static LocString NAME = "Vomiter";

				// Token: 0x0400B3F4 RID: 46068
				public static LocString DESC = "This Duplicant is liable to puke everywhere when " + UI.PRE_KEYWORD + "Stressed" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B75 RID: 11125
			public class BANSHEE
			{
				// Token: 0x0400B3F5 RID: 46069
				public static LocString NAME = "Banshee";

				// Token: 0x0400B3F6 RID: 46070
				public static LocString DESC = "This Duplicant wails uncontrollably when " + UI.PRE_KEYWORD + "Stressed" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B76 RID: 11126
			public class BALLOONARTIST
			{
				// Token: 0x0400B3F7 RID: 46071
				public static LocString NAME = "Balloon Artist";

				// Token: 0x0400B3F8 RID: 46072
				public static LocString DESC = "This Duplicant hands out balloons when they are " + UI.PRE_KEYWORD + "Overjoyed" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B77 RID: 11127
			public class SPARKLESTREAKER
			{
				// Token: 0x0400B3F9 RID: 46073
				public static LocString NAME = "Sparkle Streaker";

				// Token: 0x0400B3FA RID: 46074
				public static LocString DESC = "This Duplicant leaves a trail of happy sparkles when they are " + UI.PRE_KEYWORD + "Overjoyed" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B78 RID: 11128
			public class STICKERBOMBER
			{
				// Token: 0x0400B3FB RID: 46075
				public static LocString NAME = "Sticker Bomber";

				// Token: 0x0400B3FC RID: 46076
				public static LocString DESC = "This Duplicant will spontaneously redecorate a room when they are " + UI.PRE_KEYWORD + "Overjoyed" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B79 RID: 11129
			public class SUPERPRODUCTIVE
			{
				// Token: 0x0400B3FD RID: 46077
				public static LocString NAME = "Super Productive";

				// Token: 0x0400B3FE RID: 46078
				public static LocString DESC = "This Duplicant is super productive when they are " + UI.PRE_KEYWORD + "Overjoyed" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B7A RID: 11130
			public class HAPPYSINGER
			{
				// Token: 0x0400B3FF RID: 46079
				public static LocString NAME = "Yodeler";

				// Token: 0x0400B400 RID: 46080
				public static LocString DESC = "This Duplicant belts out catchy tunes when they are " + UI.PRE_KEYWORD + "Overjoyed" + UI.PST_KEYWORD;
			}

			// Token: 0x02002B7B RID: 11131
			public class IRONGUT
			{
				// Token: 0x0400B401 RID: 46081
				public static LocString NAME = "Iron Gut";

				// Token: 0x0400B402 RID: 46082
				public static LocString DESC = "This Duplicant can eat just about anything without getting sick";

				// Token: 0x0400B403 RID: 46083
				public static LocString SHORT_DESC = "Immune to <b>" + DUPLICANTS.DISEASES.FOODSICKNESS.NAME + "</b>";

				// Token: 0x0400B404 RID: 46084
				public static LocString SHORT_DESC_TOOLTIP = "Eating food contaminated with " + DUPLICANTS.DISEASES.FOODSICKNESS.NAME + " Germs will not affect this Duplicant";
			}

			// Token: 0x02002B7C RID: 11132
			public class STRONGIMMUNESYSTEM
			{
				// Token: 0x0400B405 RID: 46085
				public static LocString NAME = "Germ Resistant";

				// Token: 0x0400B406 RID: 46086
				public static LocString DESC = "This Duplicant's immune system bounces back faster than most";
			}

			// Token: 0x02002B7D RID: 11133
			public class SCAREDYCAT
			{
				// Token: 0x0400B407 RID: 46087
				public static LocString NAME = "Pacifist";

				// Token: 0x0400B408 RID: 46088
				public static LocString DESC = "This Duplicant abhors violence";
			}

			// Token: 0x02002B7E RID: 11134
			public class ALLERGIES
			{
				// Token: 0x0400B409 RID: 46089
				public static LocString NAME = "Allergies";

				// Token: 0x0400B40A RID: 46090
				public static LocString DESC = "This Duplicant will sneeze uncontrollably when exposed to the pollen present in " + DUPLICANTS.DISEASES.POLLENGERMS.NAME;

				// Token: 0x0400B40B RID: 46091
				public static LocString SHORT_DESC = "Allergic reaction to <b>" + DUPLICANTS.DISEASES.POLLENGERMS.NAME + "</b>";

				// Token: 0x0400B40C RID: 46092
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.DISEASES.ALLERGIES.DESCRIPTIVE_SYMPTOMS;
			}

			// Token: 0x02002B7F RID: 11135
			public class WEAKIMMUNESYSTEM
			{
				// Token: 0x0400B40D RID: 46093
				public static LocString NAME = "Biohazardous";

				// Token: 0x0400B40E RID: 46094
				public static LocString DESC = "All the vitamin C in space couldn't stop this Duplicant from getting sick";
			}

			// Token: 0x02002B80 RID: 11136
			public class IRRITABLEBOWEL
			{
				// Token: 0x0400B40F RID: 46095
				public static LocString NAME = "Irritable Bowel";

				// Token: 0x0400B410 RID: 46096
				public static LocString DESC = "This Duplicant needs a little extra time to \"do their business\"";
			}

			// Token: 0x02002B81 RID: 11137
			public class CALORIEBURNER
			{
				// Token: 0x0400B411 RID: 46097
				public static LocString NAME = "Bottomless Stomach";

				// Token: 0x0400B412 RID: 46098
				public static LocString DESC = "This Duplicant might actually be several black holes in a trench coat";
			}

			// Token: 0x02002B82 RID: 11138
			public class SMALLBLADDER
			{
				// Token: 0x0400B413 RID: 46099
				public static LocString NAME = "Small Bladder";

				// Token: 0x0400B414 RID: 46100
				public static LocString DESC = string.Concat(new string[]
				{
					"This Duplicant has a tiny, pea-sized ",
					UI.PRE_KEYWORD,
					"Bladder",
					UI.PST_KEYWORD,
					". Adorable!"
				});
			}

			// Token: 0x02002B83 RID: 11139
			public class ANEMIC
			{
				// Token: 0x0400B415 RID: 46101
				public static LocString NAME = "Anemic";

				// Token: 0x0400B416 RID: 46102
				public static LocString DESC = "This Duplicant has trouble keeping up with the others";
			}

			// Token: 0x02002B84 RID: 11140
			public class GREASEMONKEY
			{
				// Token: 0x0400B417 RID: 46103
				public static LocString NAME = "Grease Monkey";

				// Token: 0x0400B418 RID: 46104
				public static LocString DESC = "This Duplicant likes to throw a wrench into the colony's plans... in a good way";
			}

			// Token: 0x02002B85 RID: 11141
			public class MOLEHANDS
			{
				// Token: 0x0400B419 RID: 46105
				public static LocString NAME = "Mole Hands";

				// Token: 0x0400B41A RID: 46106
				public static LocString DESC = "They're great for tunneling, but finding good gloves is a nightmare";
			}

			// Token: 0x02002B86 RID: 11142
			public class FASTLEARNER
			{
				// Token: 0x0400B41B RID: 46107
				public static LocString NAME = "Quick Learner";

				// Token: 0x0400B41C RID: 46108
				public static LocString DESC = "This Duplicant's sharp as a tack and learns new skills with amazing speed";
			}

			// Token: 0x02002B87 RID: 11143
			public class SLOWLEARNER
			{
				// Token: 0x0400B41D RID: 46109
				public static LocString NAME = "Slow Learner";

				// Token: 0x0400B41E RID: 46110
				public static LocString DESC = "This Duplicant's a little slow on the uptake, but gosh do they try";
			}

			// Token: 0x02002B88 RID: 11144
			public class DIVERSLUNG
			{
				// Token: 0x0400B41F RID: 46111
				public static LocString NAME = "Diver's Lungs";

				// Token: 0x0400B420 RID: 46112
				public static LocString DESC = "This Duplicant could have been a talented opera singer in another life";
			}

			// Token: 0x02002B89 RID: 11145
			public class FLATULENCE
			{
				// Token: 0x0400B421 RID: 46113
				public static LocString NAME = "Flatulent";

				// Token: 0x0400B422 RID: 46114
				public static LocString DESC = "Some Duplicants are just full of it";

				// Token: 0x0400B423 RID: 46115
				public static LocString SHORT_DESC = "Farts frequently";

				// Token: 0x0400B424 RID: 46116
				public static LocString SHORT_DESC_TOOLTIP = "This Duplicant will periodically \"output\" " + ELEMENTS.METHANE.NAME;
			}

			// Token: 0x02002B8A RID: 11146
			public class SNORER
			{
				// Token: 0x0400B425 RID: 46117
				public static LocString NAME = "Loud Sleeper";

				// Token: 0x0400B426 RID: 46118
				public static LocString DESC = "In space, everyone can hear you snore";

				// Token: 0x0400B427 RID: 46119
				public static LocString SHORT_DESC = "Snores loudly";

				// Token: 0x0400B428 RID: 46120
				public static LocString SHORT_DESC_TOOLTIP = "This Duplicant's snoring will rudely awake nearby friends";
			}

			// Token: 0x02002B8B RID: 11147
			public class NARCOLEPSY
			{
				// Token: 0x0400B429 RID: 46121
				public static LocString NAME = "Narcoleptic";

				// Token: 0x0400B42A RID: 46122
				public static LocString DESC = "This Duplicant can and will fall asleep anytime, anyplace";

				// Token: 0x0400B42B RID: 46123
				public static LocString SHORT_DESC = "Falls asleep periodically";

				// Token: 0x0400B42C RID: 46124
				public static LocString SHORT_DESC_TOOLTIP = "This Duplicant's work will be periodically interrupted by naps";
			}

			// Token: 0x02002B8C RID: 11148
			public class INTERIORDECORATOR
			{
				// Token: 0x0400B42D RID: 46125
				public static LocString NAME = "Interior Decorator";

				// Token: 0x0400B42E RID: 46126
				public static LocString DESC = "\"Move it a little to the left...\"";
			}

			// Token: 0x02002B8D RID: 11149
			public class UNCULTURED
			{
				// Token: 0x0400B42F RID: 46127
				public static LocString NAME = "Uncultured";

				// Token: 0x0400B430 RID: 46128
				public static LocString DESC = "This Duplicant has simply no appreciation for the arts";
			}

			// Token: 0x02002B8E RID: 11150
			public class EARLYBIRD
			{
				// Token: 0x0400B431 RID: 46129
				public static LocString NAME = "Early Bird";

				// Token: 0x0400B432 RID: 46130
				public static LocString DESC = "This Duplicant always wakes up feeling fresh and efficient!";

				// Token: 0x0400B433 RID: 46131
				public static LocString EXTENDED_DESC = string.Concat(new string[]
				{
					"• Morning: <b>{0}</b> bonus to all ",
					UI.PRE_KEYWORD,
					"Attributes",
					UI.PST_KEYWORD,
					"\n• Duration: 5 Schedule Blocks"
				});

				// Token: 0x0400B434 RID: 46132
				public static LocString SHORT_DESC = "Gains morning Attribute bonuses";

				// Token: 0x0400B435 RID: 46133
				public static LocString SHORT_DESC_TOOLTIP = string.Concat(new string[]
				{
					"Morning: <b>+2</b> bonus to all ",
					UI.PRE_KEYWORD,
					"Attributes",
					UI.PST_KEYWORD,
					"\n• Duration: 5 Schedule Blocks"
				});
			}

			// Token: 0x02002B8F RID: 11151
			public class NIGHTOWL
			{
				// Token: 0x0400B436 RID: 46134
				public static LocString NAME = "Night Owl";

				// Token: 0x0400B437 RID: 46135
				public static LocString DESC = "This Duplicant does their best work when they'd ought to be sleeping";

				// Token: 0x0400B438 RID: 46136
				public static LocString EXTENDED_DESC = string.Concat(new string[]
				{
					"• Nighttime: <b>{0}</b> bonus to all ",
					UI.PRE_KEYWORD,
					"Attributes",
					UI.PST_KEYWORD,
					"\n• Duration: All Night"
				});

				// Token: 0x0400B439 RID: 46137
				public static LocString SHORT_DESC = "Gains nighttime Attribute bonuses";

				// Token: 0x0400B43A RID: 46138
				public static LocString SHORT_DESC_TOOLTIP = string.Concat(new string[]
				{
					"Nighttime: <b>+3</b> bonus to all ",
					UI.PRE_KEYWORD,
					"Attributes",
					UI.PST_KEYWORD,
					"\n• Duration: All Night"
				});
			}

			// Token: 0x02002B90 RID: 11152
			public class REGENERATION
			{
				// Token: 0x0400B43B RID: 46139
				public static LocString NAME = "Regenerative";

				// Token: 0x0400B43C RID: 46140
				public static LocString DESC = "This robust Duplicant is constantly regenerating health";
			}

			// Token: 0x02002B91 RID: 11153
			public class DEEPERDIVERSLUNGS
			{
				// Token: 0x0400B43D RID: 46141
				public static LocString NAME = "Deep Diver's Lungs";

				// Token: 0x0400B43E RID: 46142
				public static LocString DESC = "This Duplicant has a frankly impressive ability to hold their breath";
			}

			// Token: 0x02002B92 RID: 11154
			public class SUNNYDISPOSITION
			{
				// Token: 0x0400B43F RID: 46143
				public static LocString NAME = "Sunny Disposition";

				// Token: 0x0400B440 RID: 46144
				public static LocString DESC = "This Duplicant has an unwaveringly positive outlook on life";
			}

			// Token: 0x02002B93 RID: 11155
			public class ROCKCRUSHER
			{
				// Token: 0x0400B441 RID: 46145
				public static LocString NAME = "Beefsteak";

				// Token: 0x0400B442 RID: 46146
				public static LocString DESC = "This Duplicant's got muscles on their muscles!";
			}

			// Token: 0x02002B94 RID: 11156
			public class SIMPLETASTES
			{
				// Token: 0x0400B443 RID: 46147
				public static LocString NAME = "Shrivelled Tastebuds";

				// Token: 0x0400B444 RID: 46148
				public static LocString DESC = "This Duplicant could lick a Puft's backside and taste nothing";
			}

			// Token: 0x02002B95 RID: 11157
			public class FOODIE
			{
				// Token: 0x0400B445 RID: 46149
				public static LocString NAME = "Gourmet";

				// Token: 0x0400B446 RID: 46150
				public static LocString DESC = "This Duplicant's refined palate demands only the most luxurious dishes the colony can offer";
			}

			// Token: 0x02002B96 RID: 11158
			public class ARCHAEOLOGIST
			{
				// Token: 0x0400B447 RID: 46151
				public static LocString NAME = "Relic Hunter";

				// Token: 0x0400B448 RID: 46152
				public static LocString DESC = "This Duplicant was never taught the phrase \"take only pictures, leave only footprints\"";
			}

			// Token: 0x02002B97 RID: 11159
			public class DECORUP
			{
				// Token: 0x0400B449 RID: 46153
				public static LocString NAME = "Innately Stylish";

				// Token: 0x0400B44A RID: 46154
				public static LocString DESC = "This Duplicant's radiant self-confidence makes even the rattiest outfits look trendy";
			}

			// Token: 0x02002B98 RID: 11160
			public class DECORDOWN
			{
				// Token: 0x0400B44B RID: 46155
				public static LocString NAME = "Shabby Dresser";

				// Token: 0x0400B44C RID: 46156
				public static LocString DESC = "This Duplicant's clearly never heard of ironing";
			}

			// Token: 0x02002B99 RID: 11161
			public class THRIVER
			{
				// Token: 0x0400B44D RID: 46157
				public static LocString NAME = "Duress to Impress";

				// Token: 0x0400B44E RID: 46158
				public static LocString DESC = "This Duplicant kicks into hyperdrive when the stress is on";

				// Token: 0x0400B44F RID: 46159
				public static LocString SHORT_DESC = "Attribute bonuses while stressed";

				// Token: 0x0400B450 RID: 46160
				public static LocString SHORT_DESC_TOOLTIP = "More than 60% Stress: <b>+7</b> bonus to all " + UI.FormatAsKeyWord("Attributes");
			}

			// Token: 0x02002B9A RID: 11162
			public class LONER
			{
				// Token: 0x0400B451 RID: 46161
				public static LocString NAME = "Loner";

				// Token: 0x0400B452 RID: 46162
				public static LocString DESC = "This Duplicant prefers solitary pursuits";

				// Token: 0x0400B453 RID: 46163
				public static LocString SHORT_DESC = "Attribute bonuses while alone";

				// Token: 0x0400B454 RID: 46164
				public static LocString SHORT_DESC_TOOLTIP = "Only Duplicant on a world: <b>+4</b> bonus to all " + UI.FormatAsKeyWord("Attributes");
			}

			// Token: 0x02002B9B RID: 11163
			public class STARRYEYED
			{
				// Token: 0x0400B455 RID: 46165
				public static LocString NAME = "Starry Eyed";

				// Token: 0x0400B456 RID: 46166
				public static LocString DESC = "This Duplicant loves being in space";

				// Token: 0x0400B457 RID: 46167
				public static LocString SHORT_DESC = "Morale bonus while in space";

				// Token: 0x0400B458 RID: 46168
				public static LocString SHORT_DESC_TOOLTIP = "In outer space: <b>+10</b> " + UI.FormatAsKeyWord("Morale");
			}

			// Token: 0x02002B9C RID: 11164
			public class GLOWSTICK
			{
				// Token: 0x0400B459 RID: 46169
				public static LocString NAME = "Glow Stick";

				// Token: 0x0400B45A RID: 46170
				public static LocString DESC = "This Duplicant is positively glowing";

				// Token: 0x0400B45B RID: 46171
				public static LocString SHORT_DESC = "Emits low amounts of rads and light";

				// Token: 0x0400B45C RID: 46172
				public static LocString SHORT_DESC_TOOLTIP = "Emits low amounts of rads and light";
			}

			// Token: 0x02002B9D RID: 11165
			public class RADIATIONEATER
			{
				// Token: 0x0400B45D RID: 46173
				public static LocString NAME = "Radiation Eater";

				// Token: 0x0400B45E RID: 46174
				public static LocString DESC = "This Duplicant eats radiation for breakfast (and dinner)";

				// Token: 0x0400B45F RID: 46175
				public static LocString SHORT_DESC = "Converts radiation exposure into calories";

				// Token: 0x0400B460 RID: 46176
				public static LocString SHORT_DESC_TOOLTIP = "Converts radiation exposure into calories";
			}

			// Token: 0x02002B9E RID: 11166
			public class NIGHTLIGHT
			{
				// Token: 0x0400B461 RID: 46177
				public static LocString NAME = "Nyctophobic";

				// Token: 0x0400B462 RID: 46178
				public static LocString DESC = "This Duplicant will imagine scary shapes in the dark all night if no one leaves a light on";

				// Token: 0x0400B463 RID: 46179
				public static LocString SHORT_DESC = "Requires light to sleep";

				// Token: 0x0400B464 RID: 46180
				public static LocString SHORT_DESC_TOOLTIP = "This Duplicant can't sleep in complete darkness";
			}

			// Token: 0x02002B9F RID: 11167
			public class GREENTHUMB
			{
				// Token: 0x0400B465 RID: 46181
				public static LocString NAME = "Green Thumb";

				// Token: 0x0400B466 RID: 46182
				public static LocString DESC = "This Duplicant regards every plant as a potential friend";
			}

			// Token: 0x02002BA0 RID: 11168
			public class CONSTRUCTIONUP
			{
				// Token: 0x0400B467 RID: 46183
				public static LocString NAME = "Handy";

				// Token: 0x0400B468 RID: 46184
				public static LocString DESC = "This Duplicant is a swift and skilled builder";
			}

			// Token: 0x02002BA1 RID: 11169
			public class RANCHINGUP
			{
				// Token: 0x0400B469 RID: 46185
				public static LocString NAME = "Animal Lover";

				// Token: 0x0400B46A RID: 46186
				public static LocString DESC = "The fuzzy snoots! The little claws! The chitinous exoskeletons! This Duplicant's never met a critter they didn't like";
			}

			// Token: 0x02002BA2 RID: 11170
			public class CONSTRUCTIONDOWN
			{
				// Token: 0x0400B46B RID: 46187
				public static LocString NAME = "Building Impaired";

				// Token: 0x0400B46C RID: 46188
				public static LocString DESC = "This Duplicant has trouble constructing anything besides meaningful friendships";
			}

			// Token: 0x02002BA3 RID: 11171
			public class RANCHINGDOWN
			{
				// Token: 0x0400B46D RID: 46189
				public static LocString NAME = "Critter Aversion";

				// Token: 0x0400B46E RID: 46190
				public static LocString DESC = "This Duplicant just doesn't trust those beady little eyes";
			}

			// Token: 0x02002BA4 RID: 11172
			public class DIGGINGDOWN
			{
				// Token: 0x0400B46F RID: 46191
				public static LocString NAME = "Undigging";

				// Token: 0x0400B470 RID: 46192
				public static LocString DESC = "This Duplicant couldn't dig themselves out of a paper bag";
			}

			// Token: 0x02002BA5 RID: 11173
			public class MACHINERYDOWN
			{
				// Token: 0x0400B471 RID: 46193
				public static LocString NAME = "Luddite";

				// Token: 0x0400B472 RID: 46194
				public static LocString DESC = "This Duplicant always invites friends over just to make them hook up their electronics";
			}

			// Token: 0x02002BA6 RID: 11174
			public class COOKINGDOWN
			{
				// Token: 0x0400B473 RID: 46195
				public static LocString NAME = "Kitchen Menace";

				// Token: 0x0400B474 RID: 46196
				public static LocString DESC = "This Duplicant could probably figure out a way to burn ice cream";
			}

			// Token: 0x02002BA7 RID: 11175
			public class ARTDOWN
			{
				// Token: 0x0400B475 RID: 46197
				public static LocString NAME = "Unpracticed Artist";

				// Token: 0x0400B476 RID: 46198
				public static LocString DESC = "This Duplicant proudly proclaims they \"can't even draw a stick figure\"";
			}

			// Token: 0x02002BA8 RID: 11176
			public class CARINGDOWN
			{
				// Token: 0x0400B477 RID: 46199
				public static LocString NAME = "Unempathetic";

				// Token: 0x0400B478 RID: 46200
				public static LocString DESC = "This Duplicant's lack of bedside manner makes it difficult for them to nurse peers back to health";
			}

			// Token: 0x02002BA9 RID: 11177
			public class BOTANISTDOWN
			{
				// Token: 0x0400B479 RID: 46201
				public static LocString NAME = "Plant Murderer";

				// Token: 0x0400B47A RID: 46202
				public static LocString DESC = "Never ask this Duplicant to watch your ferns when you go on vacation";
			}

			// Token: 0x02002BAA RID: 11178
			public class GRANTSKILL_MINING1
			{
				// Token: 0x0400B47B RID: 46203
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.JUNIOR_MINER.NAME;

				// Token: 0x0400B47C RID: 46204
				public static LocString DESC = DUPLICANTS.ROLES.JUNIOR_MINER.DESCRIPTION;

				// Token: 0x0400B47D RID: 46205
				public static LocString SHORT_DESC = "Starts with a Tier 1 <b>Skill</b>";

				// Token: 0x0400B47E RID: 46206
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BAB RID: 11179
			public class GRANTSKILL_MINING2
			{
				// Token: 0x0400B47F RID: 46207
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.MINER.NAME;

				// Token: 0x0400B480 RID: 46208
				public static LocString DESC = DUPLICANTS.ROLES.MINER.DESCRIPTION;

				// Token: 0x0400B481 RID: 46209
				public static LocString SHORT_DESC = "Starts with a Tier 2 <b>Skill</b>";

				// Token: 0x0400B482 RID: 46210
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BAC RID: 11180
			public class GRANTSKILL_MINING3
			{
				// Token: 0x0400B483 RID: 46211
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.SENIOR_MINER.NAME;

				// Token: 0x0400B484 RID: 46212
				public static LocString DESC = DUPLICANTS.ROLES.SENIOR_MINER.DESCRIPTION;

				// Token: 0x0400B485 RID: 46213
				public static LocString SHORT_DESC = "Starts with a Tier 3 <b>Skill</b>";

				// Token: 0x0400B486 RID: 46214
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BAD RID: 11181
			public class GRANTSKILL_MINING4
			{
				// Token: 0x0400B487 RID: 46215
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.MASTER_MINER.NAME;

				// Token: 0x0400B488 RID: 46216
				public static LocString DESC = DUPLICANTS.ROLES.MASTER_MINER.DESCRIPTION;

				// Token: 0x0400B489 RID: 46217
				public static LocString SHORT_DESC = "Starts with a Tier 4 <b>Skill</b>";

				// Token: 0x0400B48A RID: 46218
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BAE RID: 11182
			public class GRANTSKILL_BUILDING1
			{
				// Token: 0x0400B48B RID: 46219
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.JUNIOR_BUILDER.NAME;

				// Token: 0x0400B48C RID: 46220
				public static LocString DESC = DUPLICANTS.ROLES.JUNIOR_BUILDER.DESCRIPTION;

				// Token: 0x0400B48D RID: 46221
				public static LocString SHORT_DESC = "Starts with a Tier 1 <b>Skill</b>";

				// Token: 0x0400B48E RID: 46222
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BAF RID: 11183
			public class GRANTSKILL_BUILDING2
			{
				// Token: 0x0400B48F RID: 46223
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.BUILDER.NAME;

				// Token: 0x0400B490 RID: 46224
				public static LocString DESC = DUPLICANTS.ROLES.BUILDER.DESCRIPTION;

				// Token: 0x0400B491 RID: 46225
				public static LocString SHORT_DESC = "Starts with a Tier 2 <b>Skill</b>";

				// Token: 0x0400B492 RID: 46226
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BB0 RID: 11184
			public class GRANTSKILL_BUILDING3
			{
				// Token: 0x0400B493 RID: 46227
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.SENIOR_BUILDER.NAME;

				// Token: 0x0400B494 RID: 46228
				public static LocString DESC = DUPLICANTS.ROLES.SENIOR_BUILDER.DESCRIPTION;

				// Token: 0x0400B495 RID: 46229
				public static LocString SHORT_DESC = "Starts with a Tier 3 <b>Skill</b>";

				// Token: 0x0400B496 RID: 46230
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BB1 RID: 11185
			public class GRANTSKILL_FARMING1
			{
				// Token: 0x0400B497 RID: 46231
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.JUNIOR_FARMER.NAME;

				// Token: 0x0400B498 RID: 46232
				public static LocString DESC = DUPLICANTS.ROLES.JUNIOR_FARMER.DESCRIPTION;

				// Token: 0x0400B499 RID: 46233
				public static LocString SHORT_DESC = "Starts with a Tier 1 <b>Skill</b>";

				// Token: 0x0400B49A RID: 46234
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BB2 RID: 11186
			public class GRANTSKILL_FARMING2
			{
				// Token: 0x0400B49B RID: 46235
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.FARMER.NAME;

				// Token: 0x0400B49C RID: 46236
				public static LocString DESC = DUPLICANTS.ROLES.FARMER.DESCRIPTION;

				// Token: 0x0400B49D RID: 46237
				public static LocString SHORT_DESC = "Starts with a Tier 2 <b>Skill</b>";

				// Token: 0x0400B49E RID: 46238
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BB3 RID: 11187
			public class GRANTSKILL_FARMING3
			{
				// Token: 0x0400B49F RID: 46239
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.SENIOR_FARMER.NAME;

				// Token: 0x0400B4A0 RID: 46240
				public static LocString DESC = DUPLICANTS.ROLES.SENIOR_FARMER.DESCRIPTION;

				// Token: 0x0400B4A1 RID: 46241
				public static LocString SHORT_DESC = "Starts with a Tier 3 <b>Skill</b>";

				// Token: 0x0400B4A2 RID: 46242
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BB4 RID: 11188
			public class GRANTSKILL_RANCHING1
			{
				// Token: 0x0400B4A3 RID: 46243
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.RANCHER.NAME;

				// Token: 0x0400B4A4 RID: 46244
				public static LocString DESC = DUPLICANTS.ROLES.RANCHER.DESCRIPTION;

				// Token: 0x0400B4A5 RID: 46245
				public static LocString SHORT_DESC = "Starts with a Tier 2 <b>Skill</b>";

				// Token: 0x0400B4A6 RID: 46246
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BB5 RID: 11189
			public class GRANTSKILL_RANCHING2
			{
				// Token: 0x0400B4A7 RID: 46247
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.SENIOR_RANCHER.NAME;

				// Token: 0x0400B4A8 RID: 46248
				public static LocString DESC = DUPLICANTS.ROLES.SENIOR_RANCHER.DESCRIPTION;

				// Token: 0x0400B4A9 RID: 46249
				public static LocString SHORT_DESC = "Starts with a Tier 3 <b>Skill</b>";

				// Token: 0x0400B4AA RID: 46250
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BB6 RID: 11190
			public class GRANTSKILL_RESEARCHING1
			{
				// Token: 0x0400B4AB RID: 46251
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.JUNIOR_RESEARCHER.NAME;

				// Token: 0x0400B4AC RID: 46252
				public static LocString DESC = DUPLICANTS.ROLES.JUNIOR_RESEARCHER.DESCRIPTION;

				// Token: 0x0400B4AD RID: 46253
				public static LocString SHORT_DESC = "Starts with a Tier 1 <b>Skill</b>";

				// Token: 0x0400B4AE RID: 46254
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BB7 RID: 11191
			public class GRANTSKILL_RESEARCHING2
			{
				// Token: 0x0400B4AF RID: 46255
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.RESEARCHER.NAME;

				// Token: 0x0400B4B0 RID: 46256
				public static LocString DESC = DUPLICANTS.ROLES.RESEARCHER.DESCRIPTION;

				// Token: 0x0400B4B1 RID: 46257
				public static LocString SHORT_DESC = "Starts with a Tier 2 <b>Skill</b>";

				// Token: 0x0400B4B2 RID: 46258
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BB8 RID: 11192
			public class GRANTSKILL_RESEARCHING3
			{
				// Token: 0x0400B4B3 RID: 46259
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.SENIOR_RESEARCHER.NAME;

				// Token: 0x0400B4B4 RID: 46260
				public static LocString DESC = DUPLICANTS.ROLES.SENIOR_RESEARCHER.DESCRIPTION;

				// Token: 0x0400B4B5 RID: 46261
				public static LocString SHORT_DESC = "Starts with a Tier 3 <b>Skill</b>";

				// Token: 0x0400B4B6 RID: 46262
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BB9 RID: 11193
			public class GRANTSKILL_RESEARCHING4
			{
				// Token: 0x0400B4B7 RID: 46263
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.NUCLEAR_RESEARCHER.NAME;

				// Token: 0x0400B4B8 RID: 46264
				public static LocString DESC = DUPLICANTS.ROLES.NUCLEAR_RESEARCHER.DESCRIPTION;

				// Token: 0x0400B4B9 RID: 46265
				public static LocString SHORT_DESC = "Starts with a Tier 3 <b>Skill</b>";

				// Token: 0x0400B4BA RID: 46266
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BBA RID: 11194
			public class GRANTSKILL_COOKING1
			{
				// Token: 0x0400B4BB RID: 46267
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.JUNIOR_COOK.NAME;

				// Token: 0x0400B4BC RID: 46268
				public static LocString DESC = DUPLICANTS.ROLES.JUNIOR_COOK.DESCRIPTION;

				// Token: 0x0400B4BD RID: 46269
				public static LocString SHORT_DESC = "Starts with a Tier 1 <b>Skill</b>";

				// Token: 0x0400B4BE RID: 46270
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BBB RID: 11195
			public class GRANTSKILL_COOKING2
			{
				// Token: 0x0400B4BF RID: 46271
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.COOK.NAME;

				// Token: 0x0400B4C0 RID: 46272
				public static LocString DESC = DUPLICANTS.ROLES.COOK.DESCRIPTION;

				// Token: 0x0400B4C1 RID: 46273
				public static LocString SHORT_DESC = "Starts with a Tier 2 <b>Skill</b>";

				// Token: 0x0400B4C2 RID: 46274
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BBC RID: 11196
			public class GRANTSKILL_ARTING1
			{
				// Token: 0x0400B4C3 RID: 46275
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.JUNIOR_ARTIST.NAME;

				// Token: 0x0400B4C4 RID: 46276
				public static LocString DESC = DUPLICANTS.ROLES.JUNIOR_ARTIST.DESCRIPTION;

				// Token: 0x0400B4C5 RID: 46277
				public static LocString SHORT_DESC = "Starts with a Tier 1 <b>Skill</b>";

				// Token: 0x0400B4C6 RID: 46278
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BBD RID: 11197
			public class GRANTSKILL_ARTING2
			{
				// Token: 0x0400B4C7 RID: 46279
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.ARTIST.NAME;

				// Token: 0x0400B4C8 RID: 46280
				public static LocString DESC = DUPLICANTS.ROLES.ARTIST.DESCRIPTION;

				// Token: 0x0400B4C9 RID: 46281
				public static LocString SHORT_DESC = "Starts with a Tier 2 <b>Skill</b>";

				// Token: 0x0400B4CA RID: 46282
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BBE RID: 11198
			public class GRANTSKILL_ARTING3
			{
				// Token: 0x0400B4CB RID: 46283
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.MASTER_ARTIST.NAME;

				// Token: 0x0400B4CC RID: 46284
				public static LocString DESC = DUPLICANTS.ROLES.MASTER_ARTIST.DESCRIPTION;

				// Token: 0x0400B4CD RID: 46285
				public static LocString SHORT_DESC = "Starts with a Tier 3 <b>Skill</b>";

				// Token: 0x0400B4CE RID: 46286
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BBF RID: 11199
			public class GRANTSKILL_HAULING1
			{
				// Token: 0x0400B4CF RID: 46287
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.HAULER.NAME;

				// Token: 0x0400B4D0 RID: 46288
				public static LocString DESC = DUPLICANTS.ROLES.HAULER.DESCRIPTION;

				// Token: 0x0400B4D1 RID: 46289
				public static LocString SHORT_DESC = "Starts with a Tier 1 <b>Skill</b>";

				// Token: 0x0400B4D2 RID: 46290
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BC0 RID: 11200
			public class GRANTSKILL_HAULING2
			{
				// Token: 0x0400B4D3 RID: 46291
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.MATERIALS_MANAGER.NAME;

				// Token: 0x0400B4D4 RID: 46292
				public static LocString DESC = DUPLICANTS.ROLES.MATERIALS_MANAGER.DESCRIPTION;

				// Token: 0x0400B4D5 RID: 46293
				public static LocString SHORT_DESC = "Starts with a Tier 2 <b>Skill</b>";

				// Token: 0x0400B4D6 RID: 46294
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BC1 RID: 11201
			public class GRANTSKILL_SUITS1
			{
				// Token: 0x0400B4D7 RID: 46295
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.SUIT_EXPERT.NAME;

				// Token: 0x0400B4D8 RID: 46296
				public static LocString DESC = DUPLICANTS.ROLES.SUIT_EXPERT.DESCRIPTION;

				// Token: 0x0400B4D9 RID: 46297
				public static LocString SHORT_DESC = "Starts with a Tier 3 <b>Skill</b>";

				// Token: 0x0400B4DA RID: 46298
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BC2 RID: 11202
			public class GRANTSKILL_TECHNICALS1
			{
				// Token: 0x0400B4DB RID: 46299
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.MACHINE_TECHNICIAN.NAME;

				// Token: 0x0400B4DC RID: 46300
				public static LocString DESC = DUPLICANTS.ROLES.MACHINE_TECHNICIAN.DESCRIPTION;

				// Token: 0x0400B4DD RID: 46301
				public static LocString SHORT_DESC = "Starts with a Tier 1 <b>Skill</b>";

				// Token: 0x0400B4DE RID: 46302
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BC3 RID: 11203
			public class GRANTSKILL_TECHNICALS2
			{
				// Token: 0x0400B4DF RID: 46303
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.POWER_TECHNICIAN.NAME;

				// Token: 0x0400B4E0 RID: 46304
				public static LocString DESC = DUPLICANTS.ROLES.POWER_TECHNICIAN.DESCRIPTION;

				// Token: 0x0400B4E1 RID: 46305
				public static LocString SHORT_DESC = "Starts with a Tier 2 <b>Skill</b>";

				// Token: 0x0400B4E2 RID: 46306
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BC4 RID: 11204
			public class GRANTSKILL_ENGINEERING1
			{
				// Token: 0x0400B4E3 RID: 46307
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.MECHATRONIC_ENGINEER.NAME;

				// Token: 0x0400B4E4 RID: 46308
				public static LocString DESC = DUPLICANTS.ROLES.MECHATRONIC_ENGINEER.DESCRIPTION;

				// Token: 0x0400B4E5 RID: 46309
				public static LocString SHORT_DESC = "Starts with a Tier 3 <b>Skill</b>";

				// Token: 0x0400B4E6 RID: 46310
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BC5 RID: 11205
			public class GRANTSKILL_BASEKEEPING1
			{
				// Token: 0x0400B4E7 RID: 46311
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.HANDYMAN.NAME;

				// Token: 0x0400B4E8 RID: 46312
				public static LocString DESC = DUPLICANTS.ROLES.HANDYMAN.DESCRIPTION;

				// Token: 0x0400B4E9 RID: 46313
				public static LocString SHORT_DESC = "Starts with a Tier 1 <b>Skill</b>";

				// Token: 0x0400B4EA RID: 46314
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BC6 RID: 11206
			public class GRANTSKILL_BASEKEEPING2
			{
				// Token: 0x0400B4EB RID: 46315
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.PLUMBER.NAME;

				// Token: 0x0400B4EC RID: 46316
				public static LocString DESC = DUPLICANTS.ROLES.PLUMBER.DESCRIPTION;

				// Token: 0x0400B4ED RID: 46317
				public static LocString SHORT_DESC = "Starts with a Tier 2 <b>Skill</b>";

				// Token: 0x0400B4EE RID: 46318
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BC7 RID: 11207
			public class GRANTSKILL_ASTRONAUTING1
			{
				// Token: 0x0400B4EF RID: 46319
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.ASTRONAUTTRAINEE.NAME;

				// Token: 0x0400B4F0 RID: 46320
				public static LocString DESC = DUPLICANTS.ROLES.ASTRONAUTTRAINEE.DESCRIPTION;

				// Token: 0x0400B4F1 RID: 46321
				public static LocString SHORT_DESC = "Starts with a Tier 4 <b>Skill</b>";

				// Token: 0x0400B4F2 RID: 46322
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BC8 RID: 11208
			public class GRANTSKILL_ASTRONAUTING2
			{
				// Token: 0x0400B4F3 RID: 46323
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.ASTRONAUT.NAME;

				// Token: 0x0400B4F4 RID: 46324
				public static LocString DESC = DUPLICANTS.ROLES.ASTRONAUT.DESCRIPTION;

				// Token: 0x0400B4F5 RID: 46325
				public static LocString SHORT_DESC = "Starts with a Tier 5 <b>Skill</b>";

				// Token: 0x0400B4F6 RID: 46326
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BC9 RID: 11209
			public class GRANTSKILL_MEDICINE1
			{
				// Token: 0x0400B4F7 RID: 46327
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.JUNIOR_MEDIC.NAME;

				// Token: 0x0400B4F8 RID: 46328
				public static LocString DESC = DUPLICANTS.ROLES.JUNIOR_MEDIC.DESCRIPTION;

				// Token: 0x0400B4F9 RID: 46329
				public static LocString SHORT_DESC = "Starts with a Tier 1 <b>Skill</b>";

				// Token: 0x0400B4FA RID: 46330
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BCA RID: 11210
			public class GRANTSKILL_MEDICINE2
			{
				// Token: 0x0400B4FB RID: 46331
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.MEDIC.NAME;

				// Token: 0x0400B4FC RID: 46332
				public static LocString DESC = DUPLICANTS.ROLES.MEDIC.DESCRIPTION;

				// Token: 0x0400B4FD RID: 46333
				public static LocString SHORT_DESC = "Starts with a Tier 2 <b>Skill</b>";

				// Token: 0x0400B4FE RID: 46334
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}

			// Token: 0x02002BCB RID: 11211
			public class GRANTSKILL_MEDICINE3
			{
				// Token: 0x0400B4FF RID: 46335
				public static LocString NAME = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_NAME + DUPLICANTS.ROLES.SENIOR_MEDIC.NAME;

				// Token: 0x0400B500 RID: 46336
				public static LocString DESC = DUPLICANTS.ROLES.SENIOR_MEDIC.DESCRIPTION;

				// Token: 0x0400B501 RID: 46337
				public static LocString SHORT_DESC = "Starts with a Tier 3 <b>Skill</b>";

				// Token: 0x0400B502 RID: 46338
				public static LocString SHORT_DESC_TOOLTIP = DUPLICANTS.TRAITS.GRANTED_SKILL_SHARED_SHORT_DESC_TOOLTIP;
			}
		}

		// Token: 0x02001CE9 RID: 7401
		public class PERSONALITIES
		{
			// Token: 0x02002BCC RID: 11212
			public class CATALINA
			{
				// Token: 0x0400B503 RID: 46339
				public static LocString NAME = "Catalina";

				// Token: 0x0400B504 RID: 46340
				public static LocString DESC = "A {0} is admired by all for her seemingly tireless work ethic. Little do people know, she's dying on the inside.";
			}

			// Token: 0x02002BCD RID: 11213
			public class NISBET
			{
				// Token: 0x0400B505 RID: 46341
				public static LocString NAME = "Nisbet";

				// Token: 0x0400B506 RID: 46342
				public static LocString DESC = "This {0} likes to punch people to show her affection. Everyone's too afraid of her to tell her it hurts.";
			}

			// Token: 0x02002BCE RID: 11214
			public class ELLIE
			{
				// Token: 0x0400B507 RID: 46343
				public static LocString NAME = "Ellie";

				// Token: 0x0400B508 RID: 46344
				public static LocString DESC = "Nothing makes an {0} happier than a big tin of glitter and a pack of unicorn stickers.";
			}

			// Token: 0x02002BCF RID: 11215
			public class RUBY
			{
				// Token: 0x0400B509 RID: 46345
				public static LocString NAME = "Ruby";

				// Token: 0x0400B50A RID: 46346
				public static LocString DESC = "This {0} asks the pressing questions, like \"Where can I get a leather jacket in space?\"";
			}

			// Token: 0x02002BD0 RID: 11216
			public class LEIRA
			{
				// Token: 0x0400B50B RID: 46347
				public static LocString NAME = "Leira";

				// Token: 0x0400B50C RID: 46348
				public static LocString DESC = "{0}s just want everyone to be happy.";
			}

			// Token: 0x02002BD1 RID: 11217
			public class BUBBLES
			{
				// Token: 0x0400B50D RID: 46349
				public static LocString NAME = "Bubbles";

				// Token: 0x0400B50E RID: 46350
				public static LocString DESC = "This {0} is constantly challenging others to fight her, regardless of whether or not she can actually take them.";
			}

			// Token: 0x02002BD2 RID: 11218
			public class MIMA
			{
				// Token: 0x0400B50F RID: 46351
				public static LocString NAME = "Mi-Ma";

				// Token: 0x0400B510 RID: 46352
				public static LocString DESC = "Ol' {0} here can't stand lookin' at people's knees.";
			}

			// Token: 0x02002BD3 RID: 11219
			public class NAILS
			{
				// Token: 0x0400B511 RID: 46353
				public static LocString NAME = "Nails";

				// Token: 0x0400B512 RID: 46354
				public static LocString DESC = "People often expect a Duplicant named \"{0}\" to be tough, but they're all pretty huge wimps.";
			}

			// Token: 0x02002BD4 RID: 11220
			public class MAE
			{
				// Token: 0x0400B513 RID: 46355
				public static LocString NAME = "Mae";

				// Token: 0x0400B514 RID: 46356
				public static LocString DESC = "There's nothing a {0} can't do if she sets her mind to it.";
			}

			// Token: 0x02002BD5 RID: 11221
			public class GOSSMANN
			{
				// Token: 0x0400B515 RID: 46357
				public static LocString NAME = "Gossmann";

				// Token: 0x0400B516 RID: 46358
				public static LocString DESC = "{0}s are major goofballs who can make anyone laugh.";
			}

			// Token: 0x02002BD6 RID: 11222
			public class MARIE
			{
				// Token: 0x0400B517 RID: 46359
				public static LocString NAME = "Marie";

				// Token: 0x0400B518 RID: 46360
				public static LocString DESC = "This {0} is positively glowing! What's her secret? Radioactive isotopes, of course.";
			}

			// Token: 0x02002BD7 RID: 11223
			public class LINDSAY
			{
				// Token: 0x0400B519 RID: 46361
				public static LocString NAME = "Lindsay";

				// Token: 0x0400B51A RID: 46362
				public static LocString DESC = "A {0} is a charming woman, unless you make the mistake of messing with one of her friends.";
			}

			// Token: 0x02002BD8 RID: 11224
			public class DEVON
			{
				// Token: 0x0400B51B RID: 46363
				public static LocString NAME = "Devon";

				// Token: 0x0400B51C RID: 46364
				public static LocString DESC = "This {0} dreams of owning their own personal computer so they can start a blog full of pictures of toast.";
			}

			// Token: 0x02002BD9 RID: 11225
			public class REN
			{
				// Token: 0x0400B51D RID: 46365
				public static LocString NAME = "Ren";

				// Token: 0x0400B51E RID: 46366
				public static LocString DESC = "Every {0} has this unshakable feeling that his life's already happened and he's just watching it unfold from a memory.";
			}

			// Token: 0x02002BDA RID: 11226
			public class FRANKIE
			{
				// Token: 0x0400B51F RID: 46367
				public static LocString NAME = "Frankie";

				// Token: 0x0400B520 RID: 46368
				public static LocString DESC = "There's nothing {0}s are more proud of than their thick, dignified eyebrows.";
			}

			// Token: 0x02002BDB RID: 11227
			public class BANHI
			{
				// Token: 0x0400B521 RID: 46369
				public static LocString NAME = "Banhi";

				// Token: 0x0400B522 RID: 46370
				public static LocString DESC = "The \"cool loner\" vibes that radiate off a {0} never fail to make the colony swoon.";
			}

			// Token: 0x02002BDC RID: 11228
			public class ADA
			{
				// Token: 0x0400B523 RID: 46371
				public static LocString NAME = "Ada";

				// Token: 0x0400B524 RID: 46372
				public static LocString DESC = "{0}s enjoy writing poetry in their downtime. Dark poetry.";
			}

			// Token: 0x02002BDD RID: 11229
			public class HASSAN
			{
				// Token: 0x0400B525 RID: 46373
				public static LocString NAME = "Hassan";

				// Token: 0x0400B526 RID: 46374
				public static LocString DESC = "If someone says something nice to a {0} he'll think about it nonstop for no less than three weeks.";
			}

			// Token: 0x02002BDE RID: 11230
			public class STINKY
			{
				// Token: 0x0400B527 RID: 46375
				public static LocString NAME = "Stinky";

				// Token: 0x0400B528 RID: 46376
				public static LocString DESC = "This {0} has never been invited to a party, which is a shame. His dance moves are incredible.";
			}

			// Token: 0x02002BDF RID: 11231
			public class JOSHUA
			{
				// Token: 0x0400B529 RID: 46377
				public static LocString NAME = "Joshua";

				// Token: 0x0400B52A RID: 46378
				public static LocString DESC = "{0}s are precious goobers. Other Duplicants are strangely incapable of cursing in a {0}'s presence.";
			}

			// Token: 0x02002BE0 RID: 11232
			public class LIAM
			{
				// Token: 0x0400B52B RID: 46379
				public static LocString NAME = "Liam";

				// Token: 0x0400B52C RID: 46380
				public static LocString DESC = "No matter how much this {0} scrubs, he can never truly feel clean.";
			}

			// Token: 0x02002BE1 RID: 11233
			public class ABE
			{
				// Token: 0x0400B52D RID: 46381
				public static LocString NAME = "Abe";

				// Token: 0x0400B52E RID: 46382
				public static LocString DESC = "{0}s are sweet, delicate flowers. They need to be treated gingerly, with great consideration for their feelings.";
			}

			// Token: 0x02002BE2 RID: 11234
			public class BURT
			{
				// Token: 0x0400B52F RID: 46383
				public static LocString NAME = "Burt";

				// Token: 0x0400B530 RID: 46384
				public static LocString DESC = "This {0} always feels great after a bubble bath and a good long cry.";
			}

			// Token: 0x02002BE3 RID: 11235
			public class TRAVALDO
			{
				// Token: 0x0400B531 RID: 46385
				public static LocString NAME = "Travaldo";

				// Token: 0x0400B532 RID: 46386
				public static LocString DESC = "A {0}'s monotonous voice and lack of facial expression makes it impossible for others to tell when he's messing with them.";
			}

			// Token: 0x02002BE4 RID: 11236
			public class HAROLD
			{
				// Token: 0x0400B533 RID: 46387
				public static LocString NAME = "Harold";

				// Token: 0x0400B534 RID: 46388
				public static LocString DESC = "Get a bunch of {0}s together in a room, and you'll have... a bunch of {0}s together in a room.";
			}

			// Token: 0x02002BE5 RID: 11237
			public class MAX
			{
				// Token: 0x0400B535 RID: 46389
				public static LocString NAME = "Max";

				// Token: 0x0400B536 RID: 46390
				public static LocString DESC = "At any given moment a {0} is viscerally reliving ten different humiliating memories.";
			}

			// Token: 0x02002BE6 RID: 11238
			public class ROWAN
			{
				// Token: 0x0400B537 RID: 46391
				public static LocString NAME = "Rowan";

				// Token: 0x0400B538 RID: 46392
				public static LocString DESC = "{0}s have exceptionally large hearts and express their emotions most efficiently by yelling.";
			}

			// Token: 0x02002BE7 RID: 11239
			public class OTTO
			{
				// Token: 0x0400B539 RID: 46393
				public static LocString NAME = "Otto";

				// Token: 0x0400B53A RID: 46394
				public static LocString DESC = "{0}s always insult people by accident and generally exist in a perpetual state of deep regret.";
			}

			// Token: 0x02002BE8 RID: 11240
			public class TURNER
			{
				// Token: 0x0400B53B RID: 46395
				public static LocString NAME = "Turner";

				// Token: 0x0400B53C RID: 46396
				public static LocString DESC = "This {0} is paralyzed by the knowledge that others have memories and perceptions of them they can't control.";
			}

			// Token: 0x02002BE9 RID: 11241
			public class NIKOLA
			{
				// Token: 0x0400B53D RID: 46397
				public static LocString NAME = "Nikola";

				// Token: 0x0400B53E RID: 46398
				public static LocString DESC = "This {0} once claimed he could build a laser so powerful it would rip the colony in half. No one asked him to prove it.";
			}

			// Token: 0x02002BEA RID: 11242
			public class MEEP
			{
				// Token: 0x0400B53F RID: 46399
				public static LocString NAME = "Meep";

				// Token: 0x0400B540 RID: 46400
				public static LocString DESC = "{0}s have a face only a two tonne Printing Pod could love.";
			}

			// Token: 0x02002BEB RID: 11243
			public class ARI
			{
				// Token: 0x0400B541 RID: 46401
				public static LocString NAME = "Ari";

				// Token: 0x0400B542 RID: 46402
				public static LocString DESC = "{0}s tend to space out from time to time, but they always pay attention when it counts.";
			}

			// Token: 0x02002BEC RID: 11244
			public class JEAN
			{
				// Token: 0x0400B543 RID: 46403
				public static LocString NAME = "Jean";

				// Token: 0x0400B544 RID: 46404
				public static LocString DESC = "Just because {0}s are a little slow doesn't mean they can't suffer from soul-crushing existential crises.";
			}

			// Token: 0x02002BED RID: 11245
			public class CAMILLE
			{
				// Token: 0x0400B545 RID: 46405
				public static LocString NAME = "Camille";

				// Token: 0x0400B546 RID: 46406
				public static LocString DESC = "This {0} loves anything that makes her feel nostalgic, including things that haven't aged well.";
			}

			// Token: 0x02002BEE RID: 11246
			public class ASHKAN
			{
				// Token: 0x0400B547 RID: 46407
				public static LocString NAME = "Ashkan";

				// Token: 0x0400B548 RID: 46408
				public static LocString DESC = "{0}s have what can only be described as a \"seriously infectious giggle\".";
			}

			// Token: 0x02002BEF RID: 11247
			public class STEVE
			{
				// Token: 0x0400B549 RID: 46409
				public static LocString NAME = "Steve";

				// Token: 0x0400B54A RID: 46410
				public static LocString DESC = "This {0} is convinced that he has psychic powers. And he knows exactly what his friends think about that.";
			}

			// Token: 0x02002BF0 RID: 11248
			public class AMARI
			{
				// Token: 0x0400B54B RID: 46411
				public static LocString NAME = "Amari";

				// Token: 0x0400B54C RID: 46412
				public static LocString DESC = "{0}s likes to keep the peace. Ironically, they're a riot at parties.";
			}

			// Token: 0x02002BF1 RID: 11249
			public class PEI
			{
				// Token: 0x0400B54D RID: 46413
				public static LocString NAME = "Pei";

				// Token: 0x0400B54E RID: 46414
				public static LocString DESC = "Every {0} spends at least half the day pretending that they remember what they came into this room for.";
			}

			// Token: 0x02002BF2 RID: 11250
			public class QUINN
			{
				// Token: 0x0400B54F RID: 46415
				public static LocString NAME = "Quinn";

				// Token: 0x0400B550 RID: 46416
				public static LocString DESC = "This {0}'s favorite genre of music is \"festive power ballad\".";
			}

			// Token: 0x02002BF3 RID: 11251
			public class JORGE
			{
				// Token: 0x0400B551 RID: 46417
				public static LocString NAME = "Jorge";

				// Token: 0x0400B552 RID: 46418
				public static LocString DESC = "{0} loves his new colony, even if their collective body odor makes his eyes water.";
			}
		}

		// Token: 0x02001CEA RID: 7402
		public class NEEDS
		{
			// Token: 0x02002BF4 RID: 11252
			public class DECOR
			{
				// Token: 0x0400B553 RID: 46419
				public static LocString NAME = "Decor Expectation";

				// Token: 0x0400B554 RID: 46420
				public static LocString PROFESSION_NAME = "Critic";

				// Token: 0x0400B555 RID: 46421
				public static LocString OBSERVED_DECOR = "Current Surroundings";

				// Token: 0x0400B556 RID: 46422
				public static LocString EXPECTATION_TOOLTIP = string.Concat(new string[]
				{
					"Most objects have ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" values that alter Duplicants' opinions of their surroundings.\nThis Duplicant desires ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" values of <b>{0}</b> or higher, and becomes ",
					UI.PRE_KEYWORD,
					"Stressed",
					UI.PST_KEYWORD,
					" in areas with lower ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x0400B557 RID: 46423
				public static LocString EXPECTATION_MOD_NAME = "Job Tier Request";
			}

			// Token: 0x02002BF5 RID: 11253
			public class FOOD_QUALITY
			{
				// Token: 0x0400B558 RID: 46424
				public static LocString NAME = "Food Quality";

				// Token: 0x0400B559 RID: 46425
				public static LocString PROFESSION_NAME = "Gourmet";

				// Token: 0x0400B55A RID: 46426
				public static LocString EXPECTATION_TOOLTIP = string.Concat(new string[]
				{
					"Each Duplicant has a minimum quality of ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					" they'll tolerate eating.\nThis Duplicant desires <b>Tier {0}<b> or better ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					", and becomes ",
					UI.PRE_KEYWORD,
					"Stressed",
					UI.PST_KEYWORD,
					" when they eat meals of lower quality."
				});

				// Token: 0x0400B55B RID: 46427
				public static LocString BAD_FOOD_MOD = "Food Quality";

				// Token: 0x0400B55C RID: 46428
				public static LocString NORMAL_FOOD_MOD = "Food Quality";

				// Token: 0x0400B55D RID: 46429
				public static LocString GOOD_FOOD_MOD = "Food Quality";

				// Token: 0x0400B55E RID: 46430
				public static LocString EXPECTATION_MOD_NAME = "Job Tier Request";

				// Token: 0x0400B55F RID: 46431
				public static LocString ADJECTIVE_FORMAT_POSITIVE = "{0} [{1}]";

				// Token: 0x0400B560 RID: 46432
				public static LocString ADJECTIVE_FORMAT_NEGATIVE = "{0} [{1}]";

				// Token: 0x0400B561 RID: 46433
				public static LocString FOODQUALITY = "\nFood Quality Score of {0}";

				// Token: 0x0400B562 RID: 46434
				public static LocString FOODQUALITY_EXPECTATION = string.Concat(new string[]
				{
					"\nThis Duplicant is content to eat ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					" with a ",
					UI.PRE_KEYWORD,
					"Food Quality",
					UI.PST_KEYWORD,
					" of <b>{0}</b> or higher"
				});

				// Token: 0x0400B563 RID: 46435
				public static int ADJECTIVE_INDEX_OFFSET = -1;

				// Token: 0x02002FCF RID: 12239
				public class ADJECTIVES
				{
					// Token: 0x0400BF36 RID: 48950
					public static LocString MINUS_1 = "Grisly";

					// Token: 0x0400BF37 RID: 48951
					public static LocString ZERO = "Terrible";

					// Token: 0x0400BF38 RID: 48952
					public static LocString PLUS_1 = "Poor";

					// Token: 0x0400BF39 RID: 48953
					public static LocString PLUS_2 = "Standard";

					// Token: 0x0400BF3A RID: 48954
					public static LocString PLUS_3 = "Good";

					// Token: 0x0400BF3B RID: 48955
					public static LocString PLUS_4 = "Great";

					// Token: 0x0400BF3C RID: 48956
					public static LocString PLUS_5 = "Superb";

					// Token: 0x0400BF3D RID: 48957
					public static LocString PLUS_6 = "Ambrosial";
				}
			}

			// Token: 0x02002BF6 RID: 11254
			public class QUALITYOFLIFE
			{
				// Token: 0x0400B564 RID: 46436
				public static LocString NAME = "Morale Requirements";

				// Token: 0x0400B565 RID: 46437
				public static LocString EXPECTATION_TOOLTIP = string.Concat(new string[]
				{
					"The more responsibilities and stressors a Duplicant has, the more they will desire additional leisure time and improved amenities.\n\nFailing to keep a Duplicant's ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" at or above their ",
					UI.PRE_KEYWORD,
					"Morale Need",
					UI.PST_KEYWORD,
					" means they will not be able to unwind, causing them ",
					UI.PRE_KEYWORD,
					"Stress",
					UI.PST_KEYWORD,
					" over time."
				});

				// Token: 0x0400B566 RID: 46438
				public static LocString EXPECTATION_MOD_NAME = "Skills Learned";

				// Token: 0x0400B567 RID: 46439
				public static LocString APTITUDE_SKILLS_MOD_NAME = "Interested Skills Learned";

				// Token: 0x0400B568 RID: 46440
				public static LocString TOTAL_SKILL_POINTS = "Total Skill Points: {0}";

				// Token: 0x0400B569 RID: 46441
				public static LocString GOOD_MODIFIER = "High Morale";

				// Token: 0x0400B56A RID: 46442
				public static LocString NEUTRAL_MODIFIER = "Sufficient Morale";

				// Token: 0x0400B56B RID: 46443
				public static LocString BAD_MODIFIER = "Low Morale";
			}

			// Token: 0x02002BF7 RID: 11255
			public class NOISE
			{
				// Token: 0x0400B56C RID: 46444
				public static LocString NAME = "Noise Expectation";
			}
		}

		// Token: 0x02001CEB RID: 7403
		public class ATTRIBUTES
		{
			// Token: 0x04008449 RID: 33865
			public static LocString VALUE = "{0}: {1}";

			// Token: 0x0400844A RID: 33866
			public static LocString TOTAL_VALUE = "\n\nTotal <b>{1}</b>: {0}";

			// Token: 0x0400844B RID: 33867
			public static LocString BASE_VALUE = "\nBase: {0}";

			// Token: 0x0400844C RID: 33868
			public static LocString MODIFIER_ENTRY = "\n    • {0}: {1}";

			// Token: 0x0400844D RID: 33869
			public static LocString UNPROFESSIONAL_NAME = "Lump";

			// Token: 0x0400844E RID: 33870
			public static LocString UNPROFESSIONAL_DESC = "This Duplicant has no discernible skills";

			// Token: 0x0400844F RID: 33871
			public static LocString PROFESSION_DESC = string.Concat(new string[]
			{
				"Expertise is determined by a Duplicant's highest ",
				UI.PRE_KEYWORD,
				"Attribute",
				UI.PST_KEYWORD,
				"\n\nDuplicants develop higher expectations as their Expertise level increases"
			});

			// Token: 0x04008450 RID: 33872
			public static LocString STORED_VALUE = "Stored value";

			// Token: 0x02002BF8 RID: 11256
			public class CONSTRUCTION
			{
				// Token: 0x0400B56D RID: 46445
				public static LocString NAME = "Construction";

				// Token: 0x0400B56E RID: 46446
				public static LocString DESC = "Determines a Duplicant's building Speed.";

				// Token: 0x0400B56F RID: 46447
				public static LocString SPEEDMODIFIER = "{0} Construction Speed";
			}

			// Token: 0x02002BF9 RID: 11257
			public class SCALDINGTHRESHOLD
			{
				// Token: 0x0400B570 RID: 46448
				public static LocString NAME = "Scalding Threshold";

				// Token: 0x0400B571 RID: 46449
				public static LocString DESC = string.Concat(new string[]
				{
					"Determines the ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" at which a Duplicant will get burned."
				});
			}

			// Token: 0x02002BFA RID: 11258
			public class DIGGING
			{
				// Token: 0x0400B572 RID: 46450
				public static LocString NAME = "Excavation";

				// Token: 0x0400B573 RID: 46451
				public static LocString DESC = "Determines a Duplicant's mining speed.";

				// Token: 0x0400B574 RID: 46452
				public static LocString SPEEDMODIFIER = "{0} Digging Speed";

				// Token: 0x0400B575 RID: 46453
				public static LocString ATTACK_MODIFIER = "{0} Attack Damage";
			}

			// Token: 0x02002BFB RID: 11259
			public class MACHINERY
			{
				// Token: 0x0400B576 RID: 46454
				public static LocString NAME = "Machinery";

				// Token: 0x0400B577 RID: 46455
				public static LocString DESC = "Determines how quickly a Duplicant uses machines.";

				// Token: 0x0400B578 RID: 46456
				public static LocString SPEEDMODIFIER = "{0} Machine Operation Speed";

				// Token: 0x0400B579 RID: 46457
				public static LocString TINKER_EFFECT_MODIFIER = "{0} Engie's Tune-Up Effect Duration";
			}

			// Token: 0x02002BFC RID: 11260
			public class LIFESUPPORT
			{
				// Token: 0x0400B57A RID: 46458
				public static LocString NAME = "Life Support";

				// Token: 0x0400B57B RID: 46459
				public static LocString DESC = string.Concat(new string[]
				{
					"Determines how efficiently a Duplicant maintains ",
					BUILDINGS.PREFABS.ALGAEHABITAT.NAME,
					"s, ",
					BUILDINGS.PREFABS.AIRFILTER.NAME,
					"s, and ",
					BUILDINGS.PREFABS.WATERPURIFIER.NAME,
					"s"
				});
			}

			// Token: 0x02002BFD RID: 11261
			public class TOGGLE
			{
				// Token: 0x0400B57C RID: 46460
				public static LocString NAME = "Toggle";

				// Token: 0x0400B57D RID: 46461
				public static LocString DESC = "Determines how efficiently a Duplicant tunes machinery, flips switches, and sets sensors.";
			}

			// Token: 0x02002BFE RID: 11262
			public class ATHLETICS
			{
				// Token: 0x0400B57E RID: 46462
				public static LocString NAME = "Athletics";

				// Token: 0x0400B57F RID: 46463
				public static LocString DESC = "Determines a Duplicant's default runspeed.";

				// Token: 0x0400B580 RID: 46464
				public static LocString SPEEDMODIFIER = "{0} Runspeed";
			}

			// Token: 0x02002BFF RID: 11263
			public class DOCTOREDLEVEL
			{
				// Token: 0x0400B581 RID: 46465
				public static LocString NAME = UI.FormatAsLink("Treatment Received", "MEDICINE") + " Effect";

				// Token: 0x0400B582 RID: 46466
				public static LocString DESC = string.Concat(new string[]
				{
					"Duplicants who receive medical care while in a ",
					BUILDINGS.PREFABS.DOCTORSTATION.NAME,
					" or ",
					BUILDINGS.PREFABS.ADVANCEDDOCTORSTATION.NAME,
					" will gain the ",
					UI.PRE_KEYWORD,
					"Treatment Received",
					UI.PST_KEYWORD,
					" effect\n\nThis effect reduces the severity of ",
					UI.PRE_KEYWORD,
					"Disease",
					UI.PST_KEYWORD,
					" symptoms"
				});
			}

			// Token: 0x02002C00 RID: 11264
			public class SNEEZYNESS
			{
				// Token: 0x0400B583 RID: 46467
				public static LocString NAME = "Sneeziness";

				// Token: 0x0400B584 RID: 46468
				public static LocString DESC = "Determines how frequently a Duplicant sneezes.";
			}

			// Token: 0x02002C01 RID: 11265
			public class GERMRESISTANCE
			{
				// Token: 0x0400B585 RID: 46469
				public static LocString NAME = "Germ Resistance";

				// Token: 0x0400B586 RID: 46470
				public static LocString DESC = string.Concat(new string[]
				{
					"Duplicants with a higher ",
					UI.PRE_KEYWORD,
					"Germ Resistance",
					UI.PST_KEYWORD,
					" rating are less likely to contract germ-based ",
					UI.PRE_KEYWORD,
					"Diseases",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x02002FD0 RID: 12240
				public class MODIFIER_DESCRIPTORS
				{
					// Token: 0x0400BF3E RID: 48958
					public static LocString NEGATIVE_LARGE = "{0} (Large Loss)";

					// Token: 0x0400BF3F RID: 48959
					public static LocString NEGATIVE_MEDIUM = "{0} (Medium Loss)";

					// Token: 0x0400BF40 RID: 48960
					public static LocString NEGATIVE_SMALL = "{0} (Small Loss)";

					// Token: 0x0400BF41 RID: 48961
					public static LocString NONE = "No Effect";

					// Token: 0x0400BF42 RID: 48962
					public static LocString POSITIVE_SMALL = "{0} (Small Boost)";

					// Token: 0x0400BF43 RID: 48963
					public static LocString POSITIVE_MEDIUM = "{0} (Medium Boost)";

					// Token: 0x0400BF44 RID: 48964
					public static LocString POSITIVE_LARGE = "{0} (Large Boost)";
				}
			}

			// Token: 0x02002C02 RID: 11266
			public class LEARNING
			{
				// Token: 0x0400B587 RID: 46471
				public static LocString NAME = "Science";

				// Token: 0x0400B588 RID: 46472
				public static LocString DESC = string.Concat(new string[]
				{
					"Determines how quickly a Duplicant conducts ",
					UI.PRE_KEYWORD,
					"Research",
					UI.PST_KEYWORD,
					" and gains ",
					UI.PRE_KEYWORD,
					"Skill Points",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x0400B589 RID: 46473
				public static LocString SPEEDMODIFIER = "{0} Skill Leveling";

				// Token: 0x0400B58A RID: 46474
				public static LocString RESEARCHSPEED = "{0} Research Speed";

				// Token: 0x0400B58B RID: 46475
				public static LocString GEOTUNER_SPEED_MODIFIER = "{0} Geotuning Speed";
			}

			// Token: 0x02002C03 RID: 11267
			public class COOKING
			{
				// Token: 0x0400B58C RID: 46476
				public static LocString NAME = "Cuisine";

				// Token: 0x0400B58D RID: 46477
				public static LocString DESC = string.Concat(new string[]
				{
					"Determines how quickly a Duplicant prepares ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x0400B58E RID: 46478
				public static LocString SPEEDMODIFIER = "{0} Cooking Speed";
			}

			// Token: 0x02002C04 RID: 11268
			public class HAPPINESSDELTA
			{
				// Token: 0x0400B58F RID: 46479
				public static LocString NAME = "Happiness";

				// Token: 0x0400B590 RID: 46480
				public static LocString DESC = "Contented " + UI.FormatAsLink("Critters", "CREATURES") + " produce usable materials with increased frequency.";
			}

			// Token: 0x02002C05 RID: 11269
			public class RADIATIONBALANCEDELTA
			{
				// Token: 0x0400B591 RID: 46481
				public static LocString NAME = "Absorbed Radiation Dose";

				// Token: 0x0400B592 RID: 46482
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Duplicants accumulate Rads in areas with ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" and recover at very slow rates\n\nOpen the ",
					UI.FormatAsOverlay("Radiation Overlay", global::Action.Overlay15),
					" to view current ",
					UI.PRE_KEYWORD,
					"Rad",
					UI.PST_KEYWORD,
					" readings"
				});
			}

			// Token: 0x02002C06 RID: 11270
			public class INSULATION
			{
				// Token: 0x0400B593 RID: 46483
				public static LocString NAME = "Insulation";

				// Token: 0x0400B594 RID: 46484
				public static LocString DESC = string.Concat(new string[]
				{
					"Highly ",
					UI.PRE_KEYWORD,
					"Insulated",
					UI.PST_KEYWORD,
					" Duplicants retain body heat easily, while low ",
					UI.PRE_KEYWORD,
					"Insulation",
					UI.PST_KEYWORD,
					" Duplicants are easier to keep cool."
				});

				// Token: 0x0400B595 RID: 46485
				public static LocString SPEEDMODIFIER = "{0} Temperature Retention";
			}

			// Token: 0x02002C07 RID: 11271
			public class STRENGTH
			{
				// Token: 0x0400B596 RID: 46486
				public static LocString NAME = "Strength";

				// Token: 0x0400B597 RID: 46487
				public static LocString DESC = string.Concat(new string[]
				{
					"Determines a Duplicant's ",
					UI.PRE_KEYWORD,
					"Carrying Capacity",
					UI.PST_KEYWORD,
					" and cleaning speed."
				});

				// Token: 0x0400B598 RID: 46488
				public static LocString CARRYMODIFIER = "{0} " + DUPLICANTS.ATTRIBUTES.CARRYAMOUNT.NAME;

				// Token: 0x0400B599 RID: 46489
				public static LocString SPEEDMODIFIER = "{0} Tidying Speed";
			}

			// Token: 0x02002C08 RID: 11272
			public class CARING
			{
				// Token: 0x0400B59A RID: 46490
				public static LocString NAME = "Medicine";

				// Token: 0x0400B59B RID: 46491
				public static LocString DESC = "Determines a Duplicant's ability to care for sick peers.";

				// Token: 0x0400B59C RID: 46492
				public static LocString SPEEDMODIFIER = "{0} Treatment Speed";

				// Token: 0x0400B59D RID: 46493
				public static LocString FABRICATE_SPEEDMODIFIER = "{0} Medicine Fabrication Speed";
			}

			// Token: 0x02002C09 RID: 11273
			public class IMMUNITY
			{
				// Token: 0x0400B59E RID: 46494
				public static LocString NAME = "Immunity";

				// Token: 0x0400B59F RID: 46495
				public static LocString DESC = string.Concat(new string[]
				{
					"Determines a Duplicant's ",
					UI.PRE_KEYWORD,
					"Disease",
					UI.PST_KEYWORD,
					" susceptibility and recovery time."
				});

				// Token: 0x0400B5A0 RID: 46496
				public static LocString BOOST_MODIFIER = "{0} Immunity Regen";

				// Token: 0x0400B5A1 RID: 46497
				public static LocString BOOST_STAT = "Immunity Attribute";
			}

			// Token: 0x02002C0A RID: 11274
			public class BOTANIST
			{
				// Token: 0x0400B5A2 RID: 46498
				public static LocString NAME = "Agriculture";

				// Token: 0x0400B5A3 RID: 46499
				public static LocString DESC = string.Concat(new string[]
				{
					"Determines how quickly and efficiently a Duplicant cultivates ",
					UI.PRE_KEYWORD,
					"Plants",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x0400B5A4 RID: 46500
				public static LocString HARVEST_SPEED_MODIFIER = "{0} Harvesting Speed";

				// Token: 0x0400B5A5 RID: 46501
				public static LocString TINKER_MODIFIER = "{0} Tending Speed";

				// Token: 0x0400B5A6 RID: 46502
				public static LocString BONUS_SEEDS = "{0} Seed Chance";

				// Token: 0x0400B5A7 RID: 46503
				public static LocString TINKER_EFFECT_MODIFIER = "{0} Farmer's Touch Effect Duration";
			}

			// Token: 0x02002C0B RID: 11275
			public class RANCHING
			{
				// Token: 0x0400B5A8 RID: 46504
				public static LocString NAME = "Husbandry";

				// Token: 0x0400B5A9 RID: 46505
				public static LocString DESC = "Determines how efficiently a Duplicant tends " + UI.FormatAsLink("Critters", "CREATURES") + ".";

				// Token: 0x0400B5AA RID: 46506
				public static LocString EFFECTMODIFIER = "{0} Groom Effect Duration";

				// Token: 0x0400B5AB RID: 46507
				public static LocString CAPTURABLESPEED = "{0} Wrangling Speed";
			}

			// Token: 0x02002C0C RID: 11276
			public class ART
			{
				// Token: 0x0400B5AC RID: 46508
				public static LocString NAME = "Creativity";

				// Token: 0x0400B5AD RID: 46509
				public static LocString DESC = string.Concat(new string[]
				{
					"Determines how quickly a Duplicant produces ",
					UI.PRE_KEYWORD,
					"Artwork",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x0400B5AE RID: 46510
				public static LocString SPEEDMODIFIER = "{0} Decorating Speed";
			}

			// Token: 0x02002C0D RID: 11277
			public class DECOR
			{
				// Token: 0x0400B5AF RID: 46511
				public static LocString NAME = "Decor";

				// Token: 0x0400B5B0 RID: 46512
				public static LocString DESC = string.Concat(new string[]
				{
					"Affects a Duplicant's ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" and their opinion of their surroundings."
				});
			}

			// Token: 0x02002C0E RID: 11278
			public class THERMALCONDUCTIVITYBARRIER
			{
				// Token: 0x0400B5B1 RID: 46513
				public static LocString NAME = "Insulation Thickness";

				// Token: 0x0400B5B2 RID: 46514
				public static LocString DESC = string.Concat(new string[]
				{
					"Determines how quickly a Duplicant retains or loses body ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" in any given area.\n\nIt is the sum of a Duplicant's ",
					UI.PRE_KEYWORD,
					"Equipment",
					UI.PST_KEYWORD,
					" and their natural ",
					UI.PRE_KEYWORD,
					"Insulation",
					UI.PST_KEYWORD,
					" values."
				});
			}

			// Token: 0x02002C0F RID: 11279
			public class DECORRADIUS
			{
				// Token: 0x0400B5B3 RID: 46515
				public static LocString NAME = "Decor Radius";

				// Token: 0x0400B5B4 RID: 46516
				public static LocString DESC = string.Concat(new string[]
				{
					"The influence range of an object's ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" value."
				});
			}

			// Token: 0x02002C10 RID: 11280
			public class DECOREXPECTATION
			{
				// Token: 0x0400B5B5 RID: 46517
				public static LocString NAME = "Decor Morale Bonus";

				// Token: 0x0400B5B6 RID: 46518
				public static LocString DESC = string.Concat(new string[]
				{
					"A Decor Morale Bonus allows Duplicants to receive ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" boosts from lower ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" values.\n\nMaintaining high ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" will allow Duplicants to learn more ",
					UI.PRE_KEYWORD,
					"Skills",
					UI.PST_KEYWORD,
					"."
				});
			}

			// Token: 0x02002C11 RID: 11281
			public class FOODEXPECTATION
			{
				// Token: 0x0400B5B7 RID: 46519
				public static LocString NAME = "Food Morale Bonus";

				// Token: 0x0400B5B8 RID: 46520
				public static LocString DESC = string.Concat(new string[]
				{
					"A Food Morale Bonus allows Duplicants to receive ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" boosts from lower quality ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					".\n\nMaintaining high ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" will allow Duplicants to learn more ",
					UI.PRE_KEYWORD,
					"Skills",
					UI.PST_KEYWORD,
					"."
				});
			}

			// Token: 0x02002C12 RID: 11282
			public class QUALITYOFLIFEEXPECTATION
			{
				// Token: 0x0400B5B9 RID: 46521
				public static LocString NAME = "Morale Need";

				// Token: 0x0400B5BA RID: 46522
				public static LocString DESC = string.Concat(new string[]
				{
					"Dictates how high a Duplicant's ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" must be kept to prevent them from gaining ",
					UI.PRE_KEYWORD,
					"Stress",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002C13 RID: 11283
			public class HYGIENE
			{
				// Token: 0x0400B5BB RID: 46523
				public static LocString NAME = "Hygiene";

				// Token: 0x0400B5BC RID: 46524
				public static LocString DESC = "Affects a Duplicant's sense of cleanliness.";
			}

			// Token: 0x02002C14 RID: 11284
			public class CARRYAMOUNT
			{
				// Token: 0x0400B5BD RID: 46525
				public static LocString NAME = "Carrying Capacity";

				// Token: 0x0400B5BE RID: 46526
				public static LocString DESC = "Determines the maximum weight that a Duplicant can carry.";
			}

			// Token: 0x02002C15 RID: 11285
			public class SPACENAVIGATION
			{
				// Token: 0x0400B5BF RID: 46527
				public static LocString NAME = "Piloting";

				// Token: 0x0400B5C0 RID: 46528
				public static LocString DESC = "Determines how long it takes a Duplicant to complete a space mission.";

				// Token: 0x0400B5C1 RID: 46529
				public static LocString DLC1_DESC = "Determines how much of a speed bonus a Duplicant provides to a rocket they are piloting.";

				// Token: 0x0400B5C2 RID: 46530
				public static LocString SPEED_MODIFIER = "{0} Rocket Speed";
			}

			// Token: 0x02002C16 RID: 11286
			public class QUALITYOFLIFE
			{
				// Token: 0x0400B5C3 RID: 46531
				public static LocString NAME = "Morale";

				// Token: 0x0400B5C4 RID: 46532
				public static LocString DESC = string.Concat(new string[]
				{
					"A Duplicant's ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" must exceed their ",
					UI.PRE_KEYWORD,
					"Morale Need",
					UI.PST_KEYWORD,
					", or they'll begin to accumulate ",
					UI.PRE_KEYWORD,
					"Stress",
					UI.PST_KEYWORD,
					".\n\n",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" can be increased by providing Duplicants higher quality ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					", allotting more ",
					UI.PRE_KEYWORD,
					"Downtime",
					UI.PST_KEYWORD,
					" in\nthe colony schedule, or building better ",
					UI.PRE_KEYWORD,
					"Bathrooms",
					UI.PST_KEYWORD,
					" and ",
					UI.PRE_KEYWORD,
					"Bedrooms",
					UI.PST_KEYWORD,
					" for them to live in."
				});

				// Token: 0x0400B5C5 RID: 46533
				public static LocString DESC_FORMAT = "{0} / {1}";

				// Token: 0x0400B5C6 RID: 46534
				public static LocString TOOLTIP_EXPECTATION = "Total <b>Morale Need</b>: {0}\n    • Skills Learned: +{0}";

				// Token: 0x0400B5C7 RID: 46535
				public static LocString TOOLTIP_EXPECTATION_OVER = "This Duplicant has sufficiently high " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;

				// Token: 0x0400B5C8 RID: 46536
				public static LocString TOOLTIP_EXPECTATION_UNDER = string.Concat(new string[]
				{
					"This Duplicant's low ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" will cause ",
					UI.PRE_KEYWORD,
					"Stress",
					UI.PST_KEYWORD,
					" over time"
				});
			}

			// Token: 0x02002C17 RID: 11287
			public class AIRCONSUMPTIONRATE
			{
				// Token: 0x0400B5C9 RID: 46537
				public static LocString NAME = "Air Consumption Rate";

				// Token: 0x0400B5CA RID: 46538
				public static LocString DESC = "Air Consumption determines how much " + ELEMENTS.OXYGEN.NAME + " a Duplicant requires per minute to live.";
			}

			// Token: 0x02002C18 RID: 11288
			public class RADIATIONRESISTANCE
			{
				// Token: 0x0400B5CB RID: 46539
				public static LocString NAME = "Radiation Resistance";

				// Token: 0x0400B5CC RID: 46540
				public static LocString DESC = string.Concat(new string[]
				{
					"Determines how easily a Duplicant repels ",
					UI.PRE_KEYWORD,
					"Radiation Sickness",
					UI.PST_KEYWORD,
					"."
				});
			}

			// Token: 0x02002C19 RID: 11289
			public class RADIATIONRECOVERY
			{
				// Token: 0x0400B5CD RID: 46541
				public static LocString NAME = "Radiation Absorption";

				// Token: 0x0400B5CE RID: 46542
				public static LocString DESC = string.Concat(new string[]
				{
					"The rate at which ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" is neutralized within a Duplicant body."
				});
			}

			// Token: 0x02002C1A RID: 11290
			public class STRESSDELTA
			{
				// Token: 0x0400B5CF RID: 46543
				public static LocString NAME = "Stress";

				// Token: 0x0400B5D0 RID: 46544
				public static LocString DESC = "Determines how quickly a Duplicant gains or reduces " + UI.PRE_KEYWORD + "Stress" + UI.PST_KEYWORD;
			}

			// Token: 0x02002C1B RID: 11291
			public class BREATHDELTA
			{
				// Token: 0x0400B5D1 RID: 46545
				public static LocString NAME = "Breath";

				// Token: 0x0400B5D2 RID: 46546
				public static LocString DESC = string.Concat(new string[]
				{
					"Determines how quickly a Duplicant gains or reduces ",
					UI.PRE_KEYWORD,
					"Breath",
					UI.PST_KEYWORD,
					"."
				});
			}

			// Token: 0x02002C1C RID: 11292
			public class BLADDERDELTA
			{
				// Token: 0x0400B5D3 RID: 46547
				public static LocString NAME = "Bladder";

				// Token: 0x0400B5D4 RID: 46548
				public static LocString DESC = string.Concat(new string[]
				{
					"Determines how quickly a Duplicant's ",
					UI.PRE_KEYWORD,
					"Bladder",
					UI.PST_KEYWORD,
					" fills or depletes."
				});
			}

			// Token: 0x02002C1D RID: 11293
			public class CALORIESDELTA
			{
				// Token: 0x0400B5D5 RID: 46549
				public static LocString NAME = "Calories";

				// Token: 0x0400B5D6 RID: 46550
				public static LocString DESC = string.Concat(new string[]
				{
					"Determines how quickly a Duplicant burns or stores ",
					UI.PRE_KEYWORD,
					"Calories",
					UI.PST_KEYWORD,
					"."
				});
			}

			// Token: 0x02002C1E RID: 11294
			public class STAMINADELTA
			{
				// Token: 0x0400B5D7 RID: 46551
				public static LocString NAME = "Stamina";

				// Token: 0x0400B5D8 RID: 46552
				public static LocString DESC = "";
			}

			// Token: 0x02002C1F RID: 11295
			public class TOXICITYDELTA
			{
				// Token: 0x0400B5D9 RID: 46553
				public static LocString NAME = "Toxicity";

				// Token: 0x0400B5DA RID: 46554
				public static LocString DESC = "";
			}

			// Token: 0x02002C20 RID: 11296
			public class IMMUNELEVELDELTA
			{
				// Token: 0x0400B5DB RID: 46555
				public static LocString NAME = "Immunity";

				// Token: 0x0400B5DC RID: 46556
				public static LocString DESC = "";
			}

			// Token: 0x02002C21 RID: 11297
			public class TOILETEFFICIENCY
			{
				// Token: 0x0400B5DD RID: 46557
				public static LocString NAME = "Bathroom Use Speed";

				// Token: 0x0400B5DE RID: 46558
				public static LocString DESC = "Determines how long a Duplicant needs to do their \"business\".";

				// Token: 0x0400B5DF RID: 46559
				public static LocString SPEEDMODIFIER = "{0} Bathroom Use Speed";
			}

			// Token: 0x02002C22 RID: 11298
			public class METABOLISM
			{
				// Token: 0x0400B5E0 RID: 46560
				public static LocString NAME = "Critter Metabolism";

				// Token: 0x0400B5E1 RID: 46561
				public static LocString DESC = string.Concat(new string[]
				{
					"Affects the rate at which a critter burns ",
					UI.PRE_KEYWORD,
					"Calories",
					UI.PST_KEYWORD,
					"."
				});
			}

			// Token: 0x02002C23 RID: 11299
			public class ROOMTEMPERATUREPREFERENCE
			{
				// Token: 0x0400B5E2 RID: 46562
				public static LocString NAME = "Temperature Preference";

				// Token: 0x0400B5E3 RID: 46563
				public static LocString DESC = string.Concat(new string[]
				{
					"Determines the minimum body ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" a Duplicant prefers to maintain."
				});
			}

			// Token: 0x02002C24 RID: 11300
			public class MAXUNDERWATERTRAVELCOST
			{
				// Token: 0x0400B5E4 RID: 46564
				public static LocString NAME = "Underwater Movement";

				// Token: 0x0400B5E5 RID: 46565
				public static LocString DESC = "Determines a Duplicant's runspeed when submerged in " + UI.PRE_KEYWORD + "Liquid" + UI.PST_KEYWORD;
			}

			// Token: 0x02002C25 RID: 11301
			public class OVERHEATTEMPERATURE
			{
				// Token: 0x0400B5E6 RID: 46566
				public static LocString NAME = "Overheat Temperature";

				// Token: 0x0400B5E7 RID: 46567
				public static LocString DESC = string.Concat(new string[]
				{
					"A building at Overheat ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" will take damage and break down if not cooled"
				});
			}

			// Token: 0x02002C26 RID: 11302
			public class FATALTEMPERATURE
			{
				// Token: 0x0400B5E8 RID: 46568
				public static LocString NAME = "Break Down Temperature";

				// Token: 0x0400B5E9 RID: 46569
				public static LocString DESC = string.Concat(new string[]
				{
					"A building at break down ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" will lose functionality and take damage"
				});
			}

			// Token: 0x02002C27 RID: 11303
			public class HITPOINTSDELTA
			{
				// Token: 0x0400B5EA RID: 46570
				public static LocString NAME = UI.FormatAsLink("Health", "HEALTH");

				// Token: 0x0400B5EB RID: 46571
				public static LocString DESC = "Health regeneration is increased when another Duplicant provides medical care to the patient";
			}

			// Token: 0x02002C28 RID: 11304
			public class DISEASECURESPEED
			{
				// Token: 0x0400B5EC RID: 46572
				public static LocString NAME = UI.FormatAsLink("Disease", "DISEASE") + " Recovery Speed Bonus";

				// Token: 0x0400B5ED RID: 46573
				public static LocString DESC = "Recovery speed bonus is increased when another Duplicant provides medical care to the patient";
			}

			// Token: 0x02002C29 RID: 11305
			public abstract class MACHINERYSPEED
			{
				// Token: 0x0400B5EE RID: 46574
				public static LocString NAME = "Machinery Speed";

				// Token: 0x0400B5EF RID: 46575
				public static LocString DESC = "Speed Bonus";
			}

			// Token: 0x02002C2A RID: 11306
			public abstract class GENERATOROUTPUT
			{
				// Token: 0x0400B5F0 RID: 46576
				public static LocString NAME = "Power Output";
			}

			// Token: 0x02002C2B RID: 11307
			public abstract class ROCKETBURDEN
			{
				// Token: 0x0400B5F1 RID: 46577
				public static LocString NAME = "Burden";
			}

			// Token: 0x02002C2C RID: 11308
			public abstract class ROCKETENGINEPOWER
			{
				// Token: 0x0400B5F2 RID: 46578
				public static LocString NAME = "Engine Power";
			}

			// Token: 0x02002C2D RID: 11309
			public abstract class FUELRANGEPERKILOGRAM
			{
				// Token: 0x0400B5F3 RID: 46579
				public static LocString NAME = "Range";
			}

			// Token: 0x02002C2E RID: 11310
			public abstract class HEIGHT
			{
				// Token: 0x0400B5F4 RID: 46580
				public static LocString NAME = "Height";
			}

			// Token: 0x02002C2F RID: 11311
			public class WILTTEMPRANGEMOD
			{
				// Token: 0x0400B5F5 RID: 46581
				public static LocString NAME = "Viable Temperature Range";

				// Token: 0x0400B5F6 RID: 46582
				public static LocString DESC = "Variance growth temperature relative to the base crop";
			}

			// Token: 0x02002C30 RID: 11312
			public class YIELDAMOUNT
			{
				// Token: 0x0400B5F7 RID: 46583
				public static LocString NAME = "Yield Amount";

				// Token: 0x0400B5F8 RID: 46584
				public static LocString DESC = "Plant production relative to the base crop";
			}

			// Token: 0x02002C31 RID: 11313
			public class HARVESTTIME
			{
				// Token: 0x0400B5F9 RID: 46585
				public static LocString NAME = "Harvest Duration";

				// Token: 0x0400B5FA RID: 46586
				public static LocString DESC = "Time it takes an unskilled Duplicant to harvest this plant";
			}

			// Token: 0x02002C32 RID: 11314
			public class DECORBONUS
			{
				// Token: 0x0400B5FB RID: 46587
				public static LocString NAME = "Decor Bonus";

				// Token: 0x0400B5FC RID: 46588
				public static LocString DESC = "Change in Decor value relative to the base crop";
			}

			// Token: 0x02002C33 RID: 11315
			public class MINLIGHTLUX
			{
				// Token: 0x0400B5FD RID: 46589
				public static LocString NAME = "Light";

				// Token: 0x0400B5FE RID: 46590
				public static LocString DESC = "Minimum lux this plant requires for growth";
			}

			// Token: 0x02002C34 RID: 11316
			public class FERTILIZERUSAGEMOD
			{
				// Token: 0x0400B5FF RID: 46591
				public static LocString NAME = "Fertilizer Usage";

				// Token: 0x0400B600 RID: 46592
				public static LocString DESC = "Fertilizer and irrigation amounts this plant requires relative to the base crop";
			}

			// Token: 0x02002C35 RID: 11317
			public class MINRADIATIONTHRESHOLD
			{
				// Token: 0x0400B601 RID: 46593
				public static LocString NAME = "Minimum Radiation";

				// Token: 0x0400B602 RID: 46594
				public static LocString DESC = "Smallest amount of ambient Radiation required for this plant to grow";
			}

			// Token: 0x02002C36 RID: 11318
			public class MAXRADIATIONTHRESHOLD
			{
				// Token: 0x0400B603 RID: 46595
				public static LocString NAME = "Maximum Radiation";

				// Token: 0x0400B604 RID: 46596
				public static LocString DESC = "Largest amount of ambient Radiation this plant can tolerate";
			}
		}

		// Token: 0x02001CEC RID: 7404
		public class ROLES
		{
			// Token: 0x02002C37 RID: 11319
			public class GROUPS
			{
				// Token: 0x0400B605 RID: 46597
				public static LocString APTITUDE_DESCRIPTION = string.Concat(new string[]
				{
					"This Duplicant will gain <b>{1}</b> ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" when learning ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" Skills"
				});

				// Token: 0x0400B606 RID: 46598
				public static LocString APTITUDE_DESCRIPTION_CHOREGROUP = string.Concat(new string[]
				{
					"{2}\n\nThis Duplicant will gain <b>+{1}</b> ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" when learning ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" Skills"
				});

				// Token: 0x0400B607 RID: 46599
				public static LocString SUITS = "Suit Wearing";
			}

			// Token: 0x02002C38 RID: 11320
			public class NO_ROLE
			{
				// Token: 0x0400B608 RID: 46600
				public static LocString NAME = UI.FormatAsLink("Unemployed", "NO_ROLE");

				// Token: 0x0400B609 RID: 46601
				public static LocString DESCRIPTION = "No job assignment";
			}

			// Token: 0x02002C39 RID: 11321
			public class JUNIOR_ARTIST
			{
				// Token: 0x0400B60A RID: 46602
				public static LocString NAME = UI.FormatAsLink("Art Fundamentals", "ARTING1");

				// Token: 0x0400B60B RID: 46603
				public static LocString DESCRIPTION = "Teaches the most basic level of art skill";
			}

			// Token: 0x02002C3A RID: 11322
			public class ARTIST
			{
				// Token: 0x0400B60C RID: 46604
				public static LocString NAME = UI.FormatAsLink("Aesthetic Design", "ARTING2");

				// Token: 0x0400B60D RID: 46605
				public static LocString DESCRIPTION = "Allows moderately attractive art to be created";
			}

			// Token: 0x02002C3B RID: 11323
			public class MASTER_ARTIST
			{
				// Token: 0x0400B60E RID: 46606
				public static LocString NAME = UI.FormatAsLink("Masterworks", "ARTING3");

				// Token: 0x0400B60F RID: 46607
				public static LocString DESCRIPTION = "Enables the painting and sculpting of masterpieces";
			}

			// Token: 0x02002C3C RID: 11324
			public class JUNIOR_BUILDER
			{
				// Token: 0x0400B610 RID: 46608
				public static LocString NAME = UI.FormatAsLink("Improved Construction I", "BUILDING1");

				// Token: 0x0400B611 RID: 46609
				public static LocString DESCRIPTION = "Marginally improves a Duplicant's construction speeds";
			}

			// Token: 0x02002C3D RID: 11325
			public class BUILDER
			{
				// Token: 0x0400B612 RID: 46610
				public static LocString NAME = UI.FormatAsLink("Improved Construction II", "BUILDING2");

				// Token: 0x0400B613 RID: 46611
				public static LocString DESCRIPTION = "Further increases a Duplicant's construction speeds";
			}

			// Token: 0x02002C3E RID: 11326
			public class SENIOR_BUILDER
			{
				// Token: 0x0400B614 RID: 46612
				public static LocString NAME = UI.FormatAsLink("Demolition", "BUILDING3");

				// Token: 0x0400B615 RID: 46613
				public static LocString DESCRIPTION = "Enables a Duplicant to deconstruct Gravitas buildings";
			}

			// Token: 0x02002C3F RID: 11327
			public class JUNIOR_RESEARCHER
			{
				// Token: 0x0400B616 RID: 46614
				public static LocString NAME = UI.FormatAsLink("Advanced Research", "RESEARCHING1");

				// Token: 0x0400B617 RID: 46615
				public static LocString DESCRIPTION = "Allows Duplicants to perform research using a " + BUILDINGS.PREFABS.ADVANCEDRESEARCHCENTER.NAME;
			}

			// Token: 0x02002C40 RID: 11328
			public class RESEARCHER
			{
				// Token: 0x0400B618 RID: 46616
				public static LocString NAME = UI.FormatAsLink("Field Research", "RESEARCHING2");

				// Token: 0x0400B619 RID: 46617
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"Duplicants can perform studies on ",
					UI.PRE_KEYWORD,
					"Geysers",
					UI.PST_KEYWORD,
					", ",
					UI.CLUSTERMAP.PLANETOID_KEYWORD,
					", and other geographical phenomena"
				});
			}

			// Token: 0x02002C41 RID: 11329
			public class SENIOR_RESEARCHER
			{
				// Token: 0x0400B61A RID: 46618
				public static LocString NAME = UI.FormatAsLink("Astronomy", "ASTRONOMY");

				// Token: 0x0400B61B RID: 46619
				public static LocString DESCRIPTION = "Enables Duplicants to study outer space using the " + BUILDINGS.PREFABS.CLUSTERTELESCOPE.NAME;
			}

			// Token: 0x02002C42 RID: 11330
			public class NUCLEAR_RESEARCHER
			{
				// Token: 0x0400B61C RID: 46620
				public static LocString NAME = UI.FormatAsLink("Applied Sciences Research", "ATOMICRESEARCH");

				// Token: 0x0400B61D RID: 46621
				public static LocString DESCRIPTION = "Enables Duplicants to study matter using the " + BUILDINGS.PREFABS.NUCLEARRESEARCHCENTER.NAME;
			}

			// Token: 0x02002C43 RID: 11331
			public class SPACE_RESEARCHER
			{
				// Token: 0x0400B61E RID: 46622
				public static LocString NAME = UI.FormatAsLink("Data Analysis Researcher", "SPACERESEARCH");

				// Token: 0x0400B61F RID: 46623
				public static LocString DESCRIPTION = "Enables Duplicants to conduct research using the " + BUILDINGS.PREFABS.DLC1COSMICRESEARCHCENTER.NAME;
			}

			// Token: 0x02002C44 RID: 11332
			public class JUNIOR_COOK
			{
				// Token: 0x0400B620 RID: 46624
				public static LocString NAME = UI.FormatAsLink("Grilling", "COOKING1");

				// Token: 0x0400B621 RID: 46625
				public static LocString DESCRIPTION = "Allows Duplicants to cook using the " + BUILDINGS.PREFABS.COOKINGSTATION.NAME;
			}

			// Token: 0x02002C45 RID: 11333
			public class COOK
			{
				// Token: 0x0400B622 RID: 46626
				public static LocString NAME = UI.FormatAsLink("Grilling II", "COOKING2");

				// Token: 0x0400B623 RID: 46627
				public static LocString DESCRIPTION = "Improves a Duplicant's cooking speed";
			}

			// Token: 0x02002C46 RID: 11334
			public class JUNIOR_MEDIC
			{
				// Token: 0x0400B624 RID: 46628
				public static LocString NAME = UI.FormatAsLink("Medicine Compounding", "MEDICINE1");

				// Token: 0x0400B625 RID: 46629
				public static LocString DESCRIPTION = "Allows Duplicants to produce medicines at the " + BUILDINGS.PREFABS.APOTHECARY.NAME;
			}

			// Token: 0x02002C47 RID: 11335
			public class MEDIC
			{
				// Token: 0x0400B626 RID: 46630
				public static LocString NAME = UI.FormatAsLink("Bedside Manner", "MEDICINE2");

				// Token: 0x0400B627 RID: 46631
				public static LocString DESCRIPTION = "Trains Duplicants to administer medicine at the " + BUILDINGS.PREFABS.DOCTORSTATION.NAME;
			}

			// Token: 0x02002C48 RID: 11336
			public class SENIOR_MEDIC
			{
				// Token: 0x0400B628 RID: 46632
				public static LocString NAME = UI.FormatAsLink("Advanced Medical Care", "MEDICINE3");

				// Token: 0x0400B629 RID: 46633
				public static LocString DESCRIPTION = "Trains Duplicants to operate the " + BUILDINGS.PREFABS.ADVANCEDDOCTORSTATION.NAME;
			}

			// Token: 0x02002C49 RID: 11337
			public class MACHINE_TECHNICIAN
			{
				// Token: 0x0400B62A RID: 46634
				public static LocString NAME = UI.FormatAsLink("Improved Tinkering", "TECHNICALS1");

				// Token: 0x0400B62B RID: 46635
				public static LocString DESCRIPTION = "Marginally improves a Duplicant's tinkering speeds";
			}

			// Token: 0x02002C4A RID: 11338
			public class OIL_TECHNICIAN
			{
				// Token: 0x0400B62C RID: 46636
				public static LocString NAME = UI.FormatAsLink("Oil Engineering", "OIL_TECHNICIAN");

				// Token: 0x0400B62D RID: 46637
				public static LocString DESCRIPTION = "Allows the extraction and refinement of " + ELEMENTS.CRUDEOIL.NAME;
			}

			// Token: 0x02002C4B RID: 11339
			public class HAULER
			{
				// Token: 0x0400B62E RID: 46638
				public static LocString NAME = UI.FormatAsLink("Improved Carrying I", "HAULING1");

				// Token: 0x0400B62F RID: 46639
				public static LocString DESCRIPTION = "Minorly increase a Duplicant's strength and carrying capacity";
			}

			// Token: 0x02002C4C RID: 11340
			public class MATERIALS_MANAGER
			{
				// Token: 0x0400B630 RID: 46640
				public static LocString NAME = UI.FormatAsLink("Improved Carrying II", "HAULING2");

				// Token: 0x0400B631 RID: 46641
				public static LocString DESCRIPTION = "Further increases a Duplicant's strength and carrying capacity for even swifter deliveries";
			}

			// Token: 0x02002C4D RID: 11341
			public class JUNIOR_FARMER
			{
				// Token: 0x0400B632 RID: 46642
				public static LocString NAME = UI.FormatAsLink("Improved Farming I", "FARMING1");

				// Token: 0x0400B633 RID: 46643
				public static LocString DESCRIPTION = "Minorly increase a Duplicant's farming skills, increasing their chances of harvesting new plant " + UI.PRE_KEYWORD + "Seeds" + UI.PST_KEYWORD;
			}

			// Token: 0x02002C4E RID: 11342
			public class FARMER
			{
				// Token: 0x0400B634 RID: 46644
				public static LocString NAME = UI.FormatAsLink("Crop Tending", "FARMING2");

				// Token: 0x0400B635 RID: 46645
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"Enables tending ",
					UI.PRE_KEYWORD,
					"Plants",
					UI.PST_KEYWORD,
					", which will increase their growth speed"
				});
			}

			// Token: 0x02002C4F RID: 11343
			public class SENIOR_FARMER
			{
				// Token: 0x0400B636 RID: 46646
				public static LocString NAME = UI.FormatAsLink("Improved Farming II", "FARMING3");

				// Token: 0x0400B637 RID: 46647
				public static LocString DESCRIPTION = "Further increases a Duplicant's farming skills";
			}

			// Token: 0x02002C50 RID: 11344
			public class JUNIOR_MINER
			{
				// Token: 0x0400B638 RID: 46648
				public static LocString NAME = UI.FormatAsLink("Hard Digging", "MINING1");

				// Token: 0x0400B639 RID: 46649
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"Allows the excavation of ",
					UI.PRE_KEYWORD,
					ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.VERYFIRM,
					UI.PST_KEYWORD,
					" materials such as ",
					ELEMENTS.GRANITE.NAME
				});
			}

			// Token: 0x02002C51 RID: 11345
			public class MINER
			{
				// Token: 0x0400B63A RID: 46650
				public static LocString NAME = UI.FormatAsLink("Superhard Digging", "MINING2");

				// Token: 0x0400B63B RID: 46651
				public static LocString DESCRIPTION = "Allows the excavation of the element " + ELEMENTS.KATAIRITE.NAME;
			}

			// Token: 0x02002C52 RID: 11346
			public class SENIOR_MINER
			{
				// Token: 0x0400B63C RID: 46652
				public static LocString NAME = UI.FormatAsLink("Super-Duperhard Digging", "MINING3");

				// Token: 0x0400B63D RID: 46653
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"Allows the excavation of ",
					UI.PRE_KEYWORD,
					ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.NEARLYIMPENETRABLE,
					UI.PST_KEYWORD,
					" elements, including ",
					ELEMENTS.DIAMOND.NAME,
					" and ",
					ELEMENTS.OBSIDIAN.NAME
				});
			}

			// Token: 0x02002C53 RID: 11347
			public class MASTER_MINER
			{
				// Token: 0x0400B63E RID: 46654
				public static LocString NAME = UI.FormatAsLink("Hazmat Digging", "MINING4");

				// Token: 0x0400B63F RID: 46655
				public static LocString DESCRIPTION = "Allows the excavation of dangerous materials like " + ELEMENTS.CORIUM.NAME;
			}

			// Token: 0x02002C54 RID: 11348
			public class SUIT_DURABILITY
			{
				// Token: 0x0400B640 RID: 46656
				public static LocString NAME = UI.FormatAsLink("Suit Sustainability Training", "SUITDURABILITY");

				// Token: 0x0400B641 RID: 46657
				public static LocString DESCRIPTION = "Suits equipped by this Duplicant lose durability " + GameUtil.GetFormattedPercent(EQUIPMENT.SUITS.SUIT_DURABILITY_SKILL_BONUS * 100f, GameUtil.TimeSlice.None) + " slower.";
			}

			// Token: 0x02002C55 RID: 11349
			public class SUIT_EXPERT
			{
				// Token: 0x0400B642 RID: 46658
				public static LocString NAME = UI.FormatAsLink("Exosuit Training", "SUITS1");

				// Token: 0x0400B643 RID: 46659
				public static LocString DESCRIPTION = "Eliminates the runspeed loss experienced while wearing exosuits";
			}

			// Token: 0x02002C56 RID: 11350
			public class POWER_TECHNICIAN
			{
				// Token: 0x0400B644 RID: 46660
				public static LocString NAME = UI.FormatAsLink("Electrical Engineering", "TECHNICALS2");

				// Token: 0x0400B645 RID: 46661
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"Enables generator ",
					UI.PRE_KEYWORD,
					"Tune-Up",
					UI.PST_KEYWORD,
					", which will temporarily provide improved ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" output"
				});
			}

			// Token: 0x02002C57 RID: 11351
			public class MECHATRONIC_ENGINEER
			{
				// Token: 0x0400B646 RID: 46662
				public static LocString NAME = UI.FormatAsLink("Mechatronics Engineering", "ENGINEERING1");

				// Token: 0x0400B647 RID: 46663
				public static LocString DESCRIPTION = "Allows the construction and maintenance of " + BUILDINGS.PREFABS.SOLIDCONDUIT.NAME + " systems";
			}

			// Token: 0x02002C58 RID: 11352
			public class HANDYMAN
			{
				// Token: 0x0400B648 RID: 46664
				public static LocString NAME = UI.FormatAsLink("Improved Strength", "BASEKEEPING1");

				// Token: 0x0400B649 RID: 46665
				public static LocString DESCRIPTION = "Minorly improves a Duplicant's physical strength";
			}

			// Token: 0x02002C59 RID: 11353
			public class PLUMBER
			{
				// Token: 0x0400B64A RID: 46666
				public static LocString NAME = UI.FormatAsLink("Plumbing", "BASEKEEPING2");

				// Token: 0x0400B64B RID: 46667
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"Allows a Duplicant to empty ",
					UI.PRE_KEYWORD,
					"Pipes",
					UI.PST_KEYWORD,
					" without making a mess"
				});
			}

			// Token: 0x02002C5A RID: 11354
			public class RANCHER
			{
				// Token: 0x0400B64C RID: 46668
				public static LocString NAME = UI.FormatAsLink("Critter Ranching I", "RANCHING1");

				// Token: 0x0400B64D RID: 46669
				public static LocString DESCRIPTION = "Allows a Duplicant to handle and care for " + UI.FormatAsLink("Critters", "CREATURES");
			}

			// Token: 0x02002C5B RID: 11355
			public class SENIOR_RANCHER
			{
				// Token: 0x0400B64E RID: 46670
				public static LocString NAME = UI.FormatAsLink("Critter Ranching II", "RANCHING2");

				// Token: 0x0400B64F RID: 46671
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"Improves a Duplicant's ",
					UI.PRE_KEYWORD,
					"Ranching",
					UI.PST_KEYWORD,
					" skills"
				});
			}

			// Token: 0x02002C5C RID: 11356
			public class ASTRONAUTTRAINEE
			{
				// Token: 0x0400B650 RID: 46672
				public static LocString NAME = UI.FormatAsLink("Rocket Piloting", "ASTRONAUTING1");

				// Token: 0x0400B651 RID: 46673
				public static LocString DESCRIPTION = "Allows a Duplicant to operate a " + BUILDINGS.PREFABS.COMMANDMODULE.NAME + " to pilot a rocket ship";
			}

			// Token: 0x02002C5D RID: 11357
			public class ASTRONAUT
			{
				// Token: 0x0400B652 RID: 46674
				public static LocString NAME = UI.FormatAsLink("Rocket Navigation", "ASTRONAUTING2");

				// Token: 0x0400B653 RID: 46675
				public static LocString DESCRIPTION = "Improves the speed that space missions are completed";
			}

			// Token: 0x02002C5E RID: 11358
			public class ROCKETPILOT
			{
				// Token: 0x0400B654 RID: 46676
				public static LocString NAME = UI.FormatAsLink("Rocket Piloting", "ROCKETPILOTING1");

				// Token: 0x0400B655 RID: 46677
				public static LocString DESCRIPTION = "Allows a Duplicant to operate a " + BUILDINGS.PREFABS.ROCKETCONTROLSTATION.NAME + " and pilot rockets";
			}

			// Token: 0x02002C5F RID: 11359
			public class SENIOR_ROCKETPILOT
			{
				// Token: 0x0400B656 RID: 46678
				public static LocString NAME = UI.FormatAsLink("Rocket Piloting II", "ROCKETPILOTING2");

				// Token: 0x0400B657 RID: 46679
				public static LocString DESCRIPTION = "Allows Duplicants to pilot rockets at faster speeds";
			}

			// Token: 0x02002C60 RID: 11360
			public class USELESSSKILL
			{
				// Token: 0x0400B658 RID: 46680
				public static LocString NAME = "W.I.P. Skill";

				// Token: 0x0400B659 RID: 46681
				public static LocString DESCRIPTION = "This skill doesn't really do anything right now.";
			}
		}

		// Token: 0x02001CED RID: 7405
		public class THOUGHTS
		{
			// Token: 0x02002C61 RID: 11361
			public class STARVING
			{
				// Token: 0x0400B65A RID: 46682
				public static LocString TOOLTIP = "Starving";
			}

			// Token: 0x02002C62 RID: 11362
			public class HOT
			{
				// Token: 0x0400B65B RID: 46683
				public static LocString TOOLTIP = "Hot";
			}

			// Token: 0x02002C63 RID: 11363
			public class COLD
			{
				// Token: 0x0400B65C RID: 46684
				public static LocString TOOLTIP = "Cold";
			}

			// Token: 0x02002C64 RID: 11364
			public class BREAKBLADDER
			{
				// Token: 0x0400B65D RID: 46685
				public static LocString TOOLTIP = "Washroom Break";
			}

			// Token: 0x02002C65 RID: 11365
			public class FULLBLADDER
			{
				// Token: 0x0400B65E RID: 46686
				public static LocString TOOLTIP = "Full Bladder";
			}

			// Token: 0x02002C66 RID: 11366
			public class HAPPY
			{
				// Token: 0x0400B65F RID: 46687
				public static LocString TOOLTIP = "Happy";
			}

			// Token: 0x02002C67 RID: 11367
			public class UNHAPPY
			{
				// Token: 0x0400B660 RID: 46688
				public static LocString TOOLTIP = "Unhappy";
			}

			// Token: 0x02002C68 RID: 11368
			public class POORDECOR
			{
				// Token: 0x0400B661 RID: 46689
				public static LocString TOOLTIP = "Poor Decor";
			}

			// Token: 0x02002C69 RID: 11369
			public class POOR_FOOD_QUALITY
			{
				// Token: 0x0400B662 RID: 46690
				public static LocString TOOLTIP = "Lousy Meal";
			}

			// Token: 0x02002C6A RID: 11370
			public class GOOD_FOOD_QUALITY
			{
				// Token: 0x0400B663 RID: 46691
				public static LocString TOOLTIP = "Delicious Meal";
			}

			// Token: 0x02002C6B RID: 11371
			public class SLEEPY
			{
				// Token: 0x0400B664 RID: 46692
				public static LocString TOOLTIP = "Sleepy";
			}

			// Token: 0x02002C6C RID: 11372
			public class DREAMY
			{
				// Token: 0x0400B665 RID: 46693
				public static LocString TOOLTIP = "Dreaming";
			}

			// Token: 0x02002C6D RID: 11373
			public class SUFFOCATING
			{
				// Token: 0x0400B666 RID: 46694
				public static LocString TOOLTIP = "Suffocating";
			}

			// Token: 0x02002C6E RID: 11374
			public class ANGRY
			{
				// Token: 0x0400B667 RID: 46695
				public static LocString TOOLTIP = "Angry";
			}

			// Token: 0x02002C6F RID: 11375
			public class RAGING
			{
				// Token: 0x0400B668 RID: 46696
				public static LocString TOOLTIP = "Raging";
			}

			// Token: 0x02002C70 RID: 11376
			public class GOTINFECTED
			{
				// Token: 0x0400B669 RID: 46697
				public static LocString TOOLTIP = "Got Infected";
			}

			// Token: 0x02002C71 RID: 11377
			public class PUTRIDODOUR
			{
				// Token: 0x0400B66A RID: 46698
				public static LocString TOOLTIP = "Smelled Something Putrid";
			}

			// Token: 0x02002C72 RID: 11378
			public class NOISY
			{
				// Token: 0x0400B66B RID: 46699
				public static LocString TOOLTIP = "Loud Area";
			}

			// Token: 0x02002C73 RID: 11379
			public class NEWROLE
			{
				// Token: 0x0400B66C RID: 46700
				public static LocString TOOLTIP = "New Skill";
			}

			// Token: 0x02002C74 RID: 11380
			public class CHATTY
			{
				// Token: 0x0400B66D RID: 46701
				public static LocString TOOLTIP = "Greeting";
			}

			// Token: 0x02002C75 RID: 11381
			public class ENCOURAGE
			{
				// Token: 0x0400B66E RID: 46702
				public static LocString TOOLTIP = "Encouraging";
			}

			// Token: 0x02002C76 RID: 11382
			public class CONVERSATION
			{
				// Token: 0x0400B66F RID: 46703
				public static LocString TOOLTIP = "Chatting";
			}

			// Token: 0x02002C77 RID: 11383
			public class CATCHYTUNE
			{
				// Token: 0x0400B670 RID: 46704
				public static LocString TOOLTIP = "WHISTLING";
			}
		}
	}
}
