using System;
using TUNING;

namespace STRINGS
{
	// Token: 0x02000D37 RID: 3383
	public class BUILDINGS
	{
		// Token: 0x02001BB9 RID: 7097
		public class PREFABS
		{
			// Token: 0x0200212E RID: 8494
			public class HEADQUARTERSCOMPLETE
			{
				// Token: 0x04009398 RID: 37784
				public static LocString NAME = UI.FormatAsLink("Printing Pod", "HEADQUARTERS");

				// Token: 0x04009399 RID: 37785
				public static LocString UNIQUE_POPTEXT = "A clone of the cloning machine? What a novel thought.\n\nAlas, it won't work.";
			}

			// Token: 0x0200212F RID: 8495
			public class EXOBASEHEADQUARTERS
			{
				// Token: 0x0400939A RID: 37786
				public static LocString NAME = UI.FormatAsLink("Mini-Pod", "EXOBASEHEADQUARTERS");

				// Token: 0x0400939B RID: 37787
				public static LocString DESC = "A quick and easy substitute, though it'll never live up to the original.";

				// Token: 0x0400939C RID: 37788
				public static LocString EFFECT = "A portable bioprinter that produces new Duplicants or care packages containing resources.\n\nOnly one Printing Pod or Mini-Pod is permitted per Planetoid.";
			}

			// Token: 0x02002130 RID: 8496
			public class AIRCONDITIONER
			{
				// Token: 0x0400939D RID: 37789
				public static LocString NAME = UI.FormatAsLink("Thermo Regulator", "AIRCONDITIONER");

				// Token: 0x0400939E RID: 37790
				public static LocString DESC = "A thermo regulator doesn't remove heat, but relocates it to a new area.";

				// Token: 0x0400939F RID: 37791
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Cools the ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" piped through it, but outputs ",
					UI.FormatAsLink("Heat", "HEAT"),
					" in its immediate vicinity."
				});
			}

			// Token: 0x02002131 RID: 8497
			public class STATERPILLAREGG
			{
				// Token: 0x040093A0 RID: 37792
				public static LocString NAME = UI.FormatAsLink("Slug Egg", "STATERPILLAREGG");

				// Token: 0x040093A1 RID: 37793
				public static LocString DESC = "The electrifying egg of the " + UI.FormatAsLink("Plug Slug", "STATERPILLAR") + ".";

				// Token: 0x040093A2 RID: 37794
				public static LocString EFFECT = "Slug Eggs can be connected to a " + UI.FormatAsLink("Power", "POWER") + " circuit as an energy source.";
			}

			// Token: 0x02002132 RID: 8498
			public class STATERPILLARGENERATOR
			{
				// Token: 0x040093A3 RID: 37795
				public static LocString NAME = UI.FormatAsLink("Plug Slug", "STATERPILLAR");

				// Token: 0x02002DBA RID: 11706
				public class MODIFIERS
				{
					// Token: 0x0400BA6C RID: 47724
					public static LocString WILD = "Wild!";

					// Token: 0x0400BA6D RID: 47725
					public static LocString HUNGRY = "Hungry!";
				}
			}

			// Token: 0x02002133 RID: 8499
			public class BEEHIVE
			{
				// Token: 0x040093A4 RID: 37796
				public static LocString NAME = UI.FormatAsLink("Beeta Hive", "BEEHIVE");

				// Token: 0x040093A5 RID: 37797
				public static LocString DESC = string.Concat(new string[]
				{
					"A moderately ",
					UI.FormatAsLink("Radioactive", "RADIATION"),
					" nest made by ",
					UI.FormatAsLink("Beetas", "BEE"),
					".\n\nConverts ",
					UI.FormatAsLink("Uranium", "URANIUMORE"),
					" into ",
					UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM"),
					" when worked by a Beeta.\nWill not function if ground below has been destroyed."
				});

				// Token: 0x040093A6 RID: 37798
				public static LocString EFFECT = "The cozy home of a Beeta.";
			}

			// Token: 0x02002134 RID: 8500
			public class ETHANOLDISTILLERY
			{
				// Token: 0x040093A7 RID: 37799
				public static LocString NAME = UI.FormatAsLink("Ethanol Distiller", "ETHANOLDISTILLERY");

				// Token: 0x040093A8 RID: 37800
				public static LocString DESC = string.Concat(new string[]
				{
					"Ethanol distillers convert ",
					ITEMS.INDUSTRIAL_PRODUCTS.WOOD.NAME,
					" into burnable ",
					ELEMENTS.ETHANOL.NAME,
					" fuel."
				});

				// Token: 0x040093A9 RID: 37801
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Refines ",
					ITEMS.INDUSTRIAL_PRODUCTS.WOOD.NAME,
					" into ",
					UI.FormatAsLink("Ethanol", "ETHANOL"),
					"."
				});
			}

			// Token: 0x02002135 RID: 8501
			public class ALGAEDISTILLERY
			{
				// Token: 0x040093AA RID: 37802
				public static LocString NAME = UI.FormatAsLink("Algae Distiller", "ALGAEDISTILLERY");

				// Token: 0x040093AB RID: 37803
				public static LocString DESC = "Algae distillers convert disease-causing slime into algae for oxygen production.";

				// Token: 0x040093AC RID: 37804
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Refines ",
					UI.FormatAsLink("Slime", "SLIMEMOLD"),
					" into ",
					UI.FormatAsLink("Algae", "ALGAE"),
					"."
				});
			}

			// Token: 0x02002136 RID: 8502
			public class OXYLITEREFINERY
			{
				// Token: 0x040093AD RID: 37805
				public static LocString NAME = UI.FormatAsLink("Oxylite Refinery", "OXYLITEREFINERY");

				// Token: 0x040093AE RID: 37806
				public static LocString DESC = "Oxylite is a solid and easily transportable source of consumable oxygen.";

				// Token: 0x040093AF RID: 37807
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Synthesizes ",
					UI.FormatAsLink("Oxylite", "OXYROCK"),
					" using ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" and a small amount of ",
					UI.FormatAsLink("Gold", "GOLD"),
					"."
				});
			}

			// Token: 0x02002137 RID: 8503
			public class FERTILIZERMAKER
			{
				// Token: 0x040093B0 RID: 37808
				public static LocString NAME = UI.FormatAsLink("Fertilizer Synthesizer", "FERTILIZERMAKER");

				// Token: 0x040093B1 RID: 37809
				public static LocString DESC = "Fertilizer synthesizers convert polluted dirt into fertilizer for domestic plants.";

				// Token: 0x040093B2 RID: 37810
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					" and ",
					UI.FormatAsLink("Phosphorite", "PHOSPHORITE"),
					" to produce ",
					UI.FormatAsLink("Fertilizer", "FERTILIZER"),
					"."
				});
			}

			// Token: 0x02002138 RID: 8504
			public class ALGAEHABITAT
			{
				// Token: 0x040093B3 RID: 37811
				public static LocString NAME = UI.FormatAsLink("Algae Terrarium", "ALGAEHABITAT");

				// Token: 0x040093B4 RID: 37812
				public static LocString DESC = "Algae colony, Duplicant colony... we're more alike than we are different.";

				// Token: 0x040093B5 RID: 37813
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Consumes ",
					UI.FormatAsLink("Algae", "ALGAE"),
					" to produce ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" and remove some ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					".\n\nGains a 10% efficiency boost in direct ",
					UI.FormatAsLink("Light", "LIGHT"),
					"."
				});

				// Token: 0x040093B6 RID: 37814
				public static LocString SIDESCREEN_TITLE = "Empty " + UI.FormatAsLink("Polluted Water", "DIRTYWATER") + " Threshold";
			}

			// Token: 0x02002139 RID: 8505
			public class BATTERY
			{
				// Token: 0x040093B7 RID: 37815
				public static LocString NAME = UI.FormatAsLink("Battery", "BATTERY");

				// Token: 0x040093B8 RID: 37816
				public static LocString DESC = "Batteries allow power from generators to be stored for later.";

				// Token: 0x040093B9 RID: 37817
				public static LocString EFFECT = "Stores " + UI.FormatAsLink("Power", "POWER") + " from generators, then provides that power to buildings.\n\nLoses charge over time.";

				// Token: 0x040093BA RID: 37818
				public static LocString CHARGE_LOSS = "{Battery} charge loss";
			}

			// Token: 0x0200213A RID: 8506
			public class FLYINGCREATUREBAIT
			{
				// Token: 0x040093BB RID: 37819
				public static LocString NAME = UI.FormatAsLink("Airborne Critter Bait", "FLYINGCREATUREBAIT");

				// Token: 0x040093BC RID: 37820
				public static LocString DESC = "The type of critter attracted by critter bait depends on the construction material.";

				// Token: 0x040093BD RID: 37821
				public static LocString EFFECT = "Attracts one type of airborne critter.\n\nSingle use.";
			}

			// Token: 0x0200213B RID: 8507
			public class AIRBORNECREATURELURE
			{
				// Token: 0x040093BE RID: 37822
				public static LocString NAME = UI.FormatAsLink("Airborne Critter Lure", "AIRBORNECREATURELURE");

				// Token: 0x040093BF RID: 37823
				public static LocString DESC = "Lures can relocate Pufts or Shine Bugs to specific locations in my colony.";

				// Token: 0x040093C0 RID: 37824
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Attracts one type of airborne critter at a time.\n\nMust be baited with ",
					UI.FormatAsLink("Slime", "SLIMEMOLD"),
					" or ",
					UI.FormatAsLink("Phosphorite", "PHOSPHORITE"),
					"."
				});
			}

			// Token: 0x0200213C RID: 8508
			public class BATTERYMEDIUM
			{
				// Token: 0x040093C1 RID: 37825
				public static LocString NAME = UI.FormatAsLink("Jumbo Battery", "BATTERYMEDIUM");

				// Token: 0x040093C2 RID: 37826
				public static LocString DESC = "Larger batteries hold more power and keep systems running longer before recharging.";

				// Token: 0x040093C3 RID: 37827
				public static LocString EFFECT = "Stores " + UI.FormatAsLink("Power", "POWER") + " from generators, then provides that power to buildings.\n\nSlightly loses charge over time.";
			}

			// Token: 0x0200213D RID: 8509
			public class BATTERYSMART
			{
				// Token: 0x040093C4 RID: 37828
				public static LocString NAME = UI.FormatAsLink("Smart Battery", "BATTERYSMART");

				// Token: 0x040093C5 RID: 37829
				public static LocString DESC = "Smart batteries send a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when they require charging.";

				// Token: 0x040093C6 RID: 37830
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Power", "POWER"),
					" from generators, then provides that power to buildings.\n\nSends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" based on the configuration of the Logic Activation Parameters.\n\nVery slightly loses charge over time."
				});

				// Token: 0x040093C7 RID: 37831
				public static LocString LOGIC_PORT = "Charge Parameters";

				// Token: 0x040093C8 RID: 37832
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when battery is less than <b>Low Threshold</b> charged, until <b>High Threshold</b> is reached again";

				// Token: 0x040093C9 RID: 37833
				public static LocString LOGIC_PORT_INACTIVE = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " when the battery is more than <b>High Threshold</b> charged, until <b>Low Threshold</b> is reached again";

				// Token: 0x040093CA RID: 37834
				public static LocString ACTIVATE_TOOLTIP = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when battery is less than <b>{0}%</b> charged, until it is <b>{1}% (High Threshold)</b> charged";

				// Token: 0x040093CB RID: 37835
				public static LocString DEACTIVATE_TOOLTIP = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " when battery is <b>{0}%</b> charged, until it is less than <b>{1}% (Low Threshold)</b> charged";

				// Token: 0x040093CC RID: 37836
				public static LocString SIDESCREEN_TITLE = "Logic Activation Parameters";

				// Token: 0x040093CD RID: 37837
				public static LocString SIDESCREEN_ACTIVATE = "Low Threshold:";

				// Token: 0x040093CE RID: 37838
				public static LocString SIDESCREEN_DEACTIVATE = "High Threshold:";
			}

			// Token: 0x0200213E RID: 8510
			public class BED
			{
				// Token: 0x040093CF RID: 37839
				public static LocString NAME = UI.FormatAsLink("Cot", "BED");

				// Token: 0x040093D0 RID: 37840
				public static LocString DESC = "Duplicants without a bed will develop sore backs from sleeping on the floor.";

				// Token: 0x040093D1 RID: 37841
				public static LocString EFFECT = "Gives one Duplicant a place to sleep.\n\nDuplicants will automatically return to their cots to sleep at night.";

				// Token: 0x02002DBB RID: 11707
				public class FACADES
				{
					// Token: 0x02002FE6 RID: 12262
					public class DEFAULT_BED
					{
						// Token: 0x0400C04D RID: 49229
						public static LocString NAME = UI.FormatAsLink("Cot", "BED");

						// Token: 0x0400C04E RID: 49230
						public static LocString DESC = "A safe place to sleep.";
					}

					// Token: 0x02002FE7 RID: 12263
					public class STARCURTAIN
					{
						// Token: 0x0400C04F RID: 49231
						public static LocString NAME = UI.FormatAsLink("Stargazer Cot", "BED");

						// Token: 0x0400C050 RID: 49232
						public static LocString DESC = "Now Duplicants can sleep beneath the stars without wearing an Atmo Suit to bed.";
					}

					// Token: 0x02002FE8 RID: 12264
					public class SCIENCELAB
					{
						// Token: 0x0400C051 RID: 49233
						public static LocString NAME = UI.FormatAsLink("Lab Cot", "BED");

						// Token: 0x0400C052 RID: 49234
						public static LocString DESC = "For the Duplicant who dreams of scientific discoveries.";
					}

					// Token: 0x02002FE9 RID: 12265
					public class STAYCATION
					{
						// Token: 0x0400C053 RID: 49235
						public static LocString NAME = UI.FormatAsLink("Staycation Cot", "BED");

						// Token: 0x0400C054 RID: 49236
						public static LocString DESC = "Like a weekend away, except... not.";
					}

					// Token: 0x02002FEA RID: 12266
					public class CREAKY
					{
						// Token: 0x0400C055 RID: 49237
						public static LocString NAME = UI.FormatAsLink("Camping Cot", "BED");

						// Token: 0x0400C056 RID: 49238
						public static LocString DESC = "It's sturdier than it looks.";
					}
				}
			}

			// Token: 0x0200213F RID: 8511
			public class BOTTLEEMPTIER
			{
				// Token: 0x040093D2 RID: 37842
				public static LocString NAME = UI.FormatAsLink("Bottle Emptier", "BOTTLEEMPTIER");

				// Token: 0x040093D3 RID: 37843
				public static LocString DESC = "A bottle emptier's Element Filter can be used to designate areas for specific liquid storage.";

				// Token: 0x040093D4 RID: 37844
				public static LocString EFFECT = "Empties bottled " + UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID") + " back into the world.";
			}

			// Token: 0x02002140 RID: 8512
			public class BOTTLEEMPTIERGAS
			{
				// Token: 0x040093D5 RID: 37845
				public static LocString NAME = UI.FormatAsLink("Canister Emptier", "BOTTLEEMPTIERGAS");

				// Token: 0x040093D6 RID: 37846
				public static LocString DESC = "A canister emptier's Element Filter can designate areas for specific gas storage.";

				// Token: 0x040093D7 RID: 37847
				public static LocString EFFECT = "Empties " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " canisters back into the world.";
			}

			// Token: 0x02002141 RID: 8513
			public class ARTIFACTCARGOBAY
			{
				// Token: 0x040093D8 RID: 37848
				public static LocString NAME = UI.FormatAsLink("Artifact Transport Module", "ARTIFACTCARGOBAY");

				// Token: 0x040093D9 RID: 37849
				public static LocString DESC = "Holds artifacts found in space.";

				// Token: 0x040093DA RID: 37850
				public static LocString EFFECT = "Allows Duplicants to store any artifacts they uncover during space missions.\n\nArtifacts become available to the colony upon the rocket's return. \n\nMust be built via " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + ".";
			}

			// Token: 0x02002142 RID: 8514
			public class CARGOBAY
			{
				// Token: 0x040093DB RID: 37851
				public static LocString NAME = UI.FormatAsLink("Cargo Bay", "CARGOBAY");

				// Token: 0x040093DC RID: 37852
				public static LocString DESC = "Duplicants will fill cargo bays with any resources they find during space missions.";

				// Token: 0x040093DD RID: 37853
				public static LocString EFFECT = "Allows Duplicants to store any " + UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID") + " found during space missions.\n\nStored resources become available to the colony upon the rocket's return.";
			}

			// Token: 0x02002143 RID: 8515
			public class CARGOBAYCLUSTER
			{
				// Token: 0x040093DE RID: 37854
				public static LocString NAME = UI.FormatAsLink("Large Cargo Bay", "CARGOBAY");

				// Token: 0x040093DF RID: 37855
				public static LocString DESC = "Holds more than a regular cargo bay.";

				// Token: 0x040093E0 RID: 37856
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to store most of the ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" found during space missions.\n\nStored resources become available to the colony upon the rocket's return. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x02002144 RID: 8516
			public class SOLIDCARGOBAYSMALL
			{
				// Token: 0x040093E1 RID: 37857
				public static LocString NAME = UI.FormatAsLink("Cargo Bay", "SOLIDCARGOBAYSMALL");

				// Token: 0x040093E2 RID: 37858
				public static LocString DESC = "Duplicants will fill cargo bays with any resources they find during space missions.";

				// Token: 0x040093E3 RID: 37859
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to store some of the ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" found during space missions.\n\nStored resources become available to the colony upon the rocket's return. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x02002145 RID: 8517
			public class SPECIALCARGOBAY
			{
				// Token: 0x040093E4 RID: 37860
				public static LocString NAME = UI.FormatAsLink("Biological Cargo Bay", "SPECIALCARGOBAY");

				// Token: 0x040093E5 RID: 37861
				public static LocString DESC = "Biological cargo bays allow Duplicants to retrieve alien plants and wildlife from space.";

				// Token: 0x040093E6 RID: 37862
				public static LocString EFFECT = "Allows Duplicants to store unusual or organic resources found during space missions.\n\nStored resources become available to the colony upon the rocket's return.";
			}

			// Token: 0x02002146 RID: 8518
			public class COMMANDMODULE
			{
				// Token: 0x040093E7 RID: 37863
				public static LocString NAME = UI.FormatAsLink("Command Capsule", "COMMANDMODULE");

				// Token: 0x040093E8 RID: 37864
				public static LocString DESC = "At least one astronaut must be assigned to the command module to pilot a rocket.";

				// Token: 0x040093E9 RID: 37865
				public static LocString EFFECT = "Contains passenger seating for Duplicant " + UI.FormatAsLink("Astronauts", "ASTRONAUTING1") + ".\n\nA Command Capsule must be the last module installed at the top of a rocket.";

				// Token: 0x040093EA RID: 37866
				public static LocString LOGIC_PORT_READY = "Rocket Checklist";

				// Token: 0x040093EB RID: 37867
				public static LocString LOGIC_PORT_READY_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when its rocket launch checklist is complete";

				// Token: 0x040093EC RID: 37868
				public static LocString LOGIC_PORT_READY_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x040093ED RID: 37869
				public static LocString LOGIC_PORT_LAUNCH = "Launch Rocket";

				// Token: 0x040093EE RID: 37870
				public static LocString LOGIC_PORT_LAUNCH_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Launch rocket";

				// Token: 0x040093EF RID: 37871
				public static LocString LOGIC_PORT_LAUNCH_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Awaits launch command";
			}

			// Token: 0x02002147 RID: 8519
			public class CLUSTERCOMMANDMODULE
			{
				// Token: 0x040093F0 RID: 37872
				public static LocString NAME = UI.FormatAsLink("Command Capsule", "CLUSTERCOMMANDMODULE");

				// Token: 0x040093F1 RID: 37873
				public static LocString DESC = "";

				// Token: 0x040093F2 RID: 37874
				public static LocString EFFECT = "";

				// Token: 0x040093F3 RID: 37875
				public static LocString LOGIC_PORT_READY = "Rocket Checklist";

				// Token: 0x040093F4 RID: 37876
				public static LocString LOGIC_PORT_READY_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when its rocket launch checklist is complete";

				// Token: 0x040093F5 RID: 37877
				public static LocString LOGIC_PORT_READY_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x040093F6 RID: 37878
				public static LocString LOGIC_PORT_LAUNCH = "Launch Rocket";

				// Token: 0x040093F7 RID: 37879
				public static LocString LOGIC_PORT_LAUNCH_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Launch rocket";

				// Token: 0x040093F8 RID: 37880
				public static LocString LOGIC_PORT_LAUNCH_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Awaits launch command";
			}

			// Token: 0x02002148 RID: 8520
			public class CLUSTERCRAFTINTERIORDOOR
			{
				// Token: 0x040093F9 RID: 37881
				public static LocString NAME = UI.FormatAsLink("Interior Hatch", "CLUSTERCRAFTINTERIORDOOR");

				// Token: 0x040093FA RID: 37882
				public static LocString DESC = "A hatch for getting in and out of the rocket.";

				// Token: 0x040093FB RID: 37883
				public static LocString EFFECT = "Warning: Do not open mid-flight.";
			}

			// Token: 0x02002149 RID: 8521
			public class ROCKETCONTROLSTATION
			{
				// Token: 0x040093FC RID: 37884
				public static LocString NAME = UI.FormatAsLink("Rocket Control Station", "ROCKETCONTROLSTATION");

				// Token: 0x040093FD RID: 37885
				public static LocString DESC = "Someone needs to be around to jiggle the controls when the screensaver comes on.";

				// Token: 0x040093FE RID: 37886
				public static LocString EFFECT = "Allows Duplicants to use pilot-operated rockets and control access to interior buildings.\n\nAssigned Duplicants must have the " + UI.FormatAsLink("Rocket Piloting", "ROCKETPILOTING1") + " skill.";

				// Token: 0x040093FF RID: 37887
				public static LocString LOGIC_PORT = "Restrict Building Usage";

				// Token: 0x04009400 RID: 37888
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Restrict access to interior buildings";

				// Token: 0x04009401 RID: 37889
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Unrestrict access to interior buildings";
			}

			// Token: 0x0200214A RID: 8522
			public class RESEARCHMODULE
			{
				// Token: 0x04009402 RID: 37890
				public static LocString NAME = UI.FormatAsLink("Research Module", "RESEARCHMODULE");

				// Token: 0x04009403 RID: 37891
				public static LocString DESC = "Data banks can be used at virtual planetariums to produce additional research.";

				// Token: 0x04009404 RID: 37892
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Completes one ",
					UI.FormatAsLink("Research Task", "RESEARCH"),
					" per space mission.\n\nProduces a small Data Bank regardless of mission destination.\n\nGenerated ",
					UI.FormatAsLink("Research Points", "RESEARCH"),
					" become available upon the rocket's return."
				});
			}

			// Token: 0x0200214B RID: 8523
			public class TOURISTMODULE
			{
				// Token: 0x04009405 RID: 37893
				public static LocString NAME = UI.FormatAsLink("Sight-Seeing Module", "TOURISTMODULE");

				// Token: 0x04009406 RID: 37894
				public static LocString DESC = "An astronaut must accompany sight seeing Duplicants on rocket flights.";

				// Token: 0x04009407 RID: 37895
				public static LocString EFFECT = "Allows one non-Astronaut Duplicant to visit space.\n\nSight-Seeing Rocket flights decrease " + UI.FormatAsLink("Stress", "STRESS") + ".";
			}

			// Token: 0x0200214C RID: 8524
			public class SCANNERMODULE
			{
				// Token: 0x04009408 RID: 37896
				public static LocString NAME = UI.FormatAsLink("Cartographic Module", "SCANNERMODULE");

				// Token: 0x04009409 RID: 37897
				public static LocString DESC = "Allows Duplicants to boldly go where other Duplicants haven't been yet.";

				// Token: 0x0400940A RID: 37898
				public static LocString EFFECT = "Automatically analyzes adjacent space while on a voyage. \n\nMust be built via " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + ".";
			}

			// Token: 0x0200214D RID: 8525
			public class HABITATMODULESMALL
			{
				// Token: 0x0400940B RID: 37899
				public static LocString NAME = UI.FormatAsLink("Solo Spacefarer Nosecone", "HABITATMODULESMALL");

				// Token: 0x0400940C RID: 37900
				public static LocString DESC = "One lucky Duplicant gets the best view from the whole rocket.";

				// Token: 0x0400940D RID: 37901
				public static LocString EFFECT = "Functions as a Command Module and a Nosecone.\n\nHolds one Duplicant traveller.\n\nOne Command Module may be installed per rocket.\n\nMust be built via " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + ". \n\nMust be built at the top of a rocket.";
			}

			// Token: 0x0200214E RID: 8526
			public class HABITATMODULEMEDIUM
			{
				// Token: 0x0400940E RID: 37902
				public static LocString NAME = UI.FormatAsLink("Spacefarer Module", "HABITATMODULEMEDIUM");

				// Token: 0x0400940F RID: 37903
				public static LocString DESC = "Allows Duplicants to survive space travel... Hopefully.";

				// Token: 0x04009410 RID: 37904
				public static LocString EFFECT = "Functions as a Command Module.\n\nHolds up to ten Duplicant travellers.\n\nOne Command Module may be installed per rocket. \n\nEngine must be built via " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + ".";
			}

			// Token: 0x0200214F RID: 8527
			public class NOSECONEBASIC
			{
				// Token: 0x04009411 RID: 37905
				public static LocString NAME = UI.FormatAsLink("Basic Nosecone", "NOSECONEBASIC");

				// Token: 0x04009412 RID: 37906
				public static LocString DESC = "Every rocket requires a nosecone to fly.";

				// Token: 0x04009413 RID: 37907
				public static LocString EFFECT = "Protects a rocket during takeoff and entry, enabling space travel.\n\nEngine must be built via " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + ". \n\nMust be built at the top of a rocket.";
			}

			// Token: 0x02002150 RID: 8528
			public class NOSECONEHARVEST
			{
				// Token: 0x04009414 RID: 37908
				public static LocString NAME = UI.FormatAsLink("Drillcone", "NOSECONEHARVEST");

				// Token: 0x04009415 RID: 37909
				public static LocString DESC = "Harvests resources from the universe.";

				// Token: 0x04009416 RID: 37910
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Enables a rocket to drill into interstellar debris and collect ",
					UI.FormatAsLink("gas", "ELEMENTS_GAS"),
					", ",
					UI.FormatAsLink("liquid", "ELEMENTS_LIQUID"),
					" and ",
					UI.FormatAsLink("solid", "ELEMENTS_SOLID"),
					" resources from space.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nMust be built at the top of a rocket with ",
					UI.FormatAsLink("gas", "ELEMENTS_GAS"),
					", ",
					UI.FormatAsLink("liquid", "ELEMENTS_LIQUID"),
					" or ",
					UI.FormatAsLink("solid", "ELEMENTS_SOLID"),
					" Cargo Module attached to store the appropriate resources."
				});
			}

			// Token: 0x02002151 RID: 8529
			public class CO2ENGINE
			{
				// Token: 0x04009417 RID: 37911
				public static LocString NAME = UI.FormatAsLink("Carbon Dioxide Engine", "CO2ENGINE");

				// Token: 0x04009418 RID: 37912
				public static LocString DESC = "Rockets can be used to send Duplicants into space and retrieve rare resources.";

				// Token: 0x04009419 RID: 37913
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses pressurized ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					" to propel rockets for short range space exploration.\n\nCarbon Dioxide Engines are relatively fast engine for their size but with limited height restrictions.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nOnce the engine has been built, more rocket modules can be added."
				});
			}

			// Token: 0x02002152 RID: 8530
			public class KEROSENEENGINE
			{
				// Token: 0x0400941A RID: 37914
				public static LocString NAME = UI.FormatAsLink("Petroleum Engine", "KEROSENEENGINE");

				// Token: 0x0400941B RID: 37915
				public static LocString DESC = "Rockets can be used to send Duplicants into space and retrieve rare resources.";

				// Token: 0x0400941C RID: 37916
				public static LocString EFFECT = "Burns " + UI.FormatAsLink("Petroleum", "PETROLEUM") + " to propel rockets for mid-range space exploration.\n\nPetroleum Engines have generous height restrictions, ideal for hauling many modules.\n\nThe engine must be built first before more rocket modules can be added.";
			}

			// Token: 0x02002153 RID: 8531
			public class KEROSENEENGINECLUSTER
			{
				// Token: 0x0400941D RID: 37917
				public static LocString NAME = UI.FormatAsLink("Petroleum Engine", "KEROSENEENGINECLUSTER");

				// Token: 0x0400941E RID: 37918
				public static LocString DESC = "More powerful rocket engines can propel heavier burdens.";

				// Token: 0x0400941F RID: 37919
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Burns ",
					UI.FormatAsLink("Petroleum", "PETROLEUM"),
					" to propel rockets for mid-range space exploration.\n\nPetroleum Engines have generous height restrictions, ideal for hauling many modules.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nOnce the engine has been built, more rocket modules can be added."
				});
			}

			// Token: 0x02002154 RID: 8532
			public class KEROSENEENGINECLUSTERSMALL
			{
				// Token: 0x04009420 RID: 37920
				public static LocString NAME = UI.FormatAsLink("Small Petroleum Engine", "KEROSENEENGINECLUSTERSMALL");

				// Token: 0x04009421 RID: 37921
				public static LocString DESC = "Rockets can be used to send Duplicants into space and retrieve rare resources.";

				// Token: 0x04009422 RID: 37922
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Burns ",
					UI.FormatAsLink("Petroleum", "PETROLEUM"),
					" to propel rockets for mid-range space exploration.\n\nSmall Petroleum Engines possess the same speed as a ",
					UI.FormatAsLink("Petroleum Engines", "KEROSENEENGINE"),
					" but have smaller height restrictions.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nOnce the engine has been built, more rocket modules can be added."
				});
			}

			// Token: 0x02002155 RID: 8533
			public class HYDROGENENGINE
			{
				// Token: 0x04009423 RID: 37923
				public static LocString NAME = UI.FormatAsLink("Hydrogen Engine", "HYDROGENENGINE");

				// Token: 0x04009424 RID: 37924
				public static LocString DESC = "Hydrogen engines can propel rockets further than steam or petroleum engines.";

				// Token: 0x04009425 RID: 37925
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Burns ",
					UI.FormatAsLink("Liquid Hydrogen", "LIQUIDHYDROGEN"),
					" to propel rockets for long-range space exploration.\n\nHydrogen Engines have the same generous height restrictions as ",
					UI.FormatAsLink("Petroleum Engines", "KEROSENEENGINE"),
					" but are slightly faster.\n\nThe engine must be built first before more rocket modules can be added."
				});
			}

			// Token: 0x02002156 RID: 8534
			public class HYDROGENENGINECLUSTER
			{
				// Token: 0x04009426 RID: 37926
				public static LocString NAME = UI.FormatAsLink("Hydrogen Engine", "HYDROGENENGINE");

				// Token: 0x04009427 RID: 37927
				public static LocString DESC = "Hydrogen engines can propel rockets further than steam or petroleum engines.";

				// Token: 0x04009428 RID: 37928
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Burns ",
					UI.FormatAsLink("Liquid Hydrogen", "LIQUIDHYDROGEN"),
					" to propel rockets for long-range space exploration.\n\nHydrogen Engines have the same generous height restrictions as ",
					UI.FormatAsLink("Petroleum Engines", "KEROSENEENGINE"),
					" but are slightly faster.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					".\n\nOnce the engine has been built, more rocket modules can be added."
				});
			}

			// Token: 0x02002157 RID: 8535
			public class SUGARENGINE
			{
				// Token: 0x04009429 RID: 37929
				public static LocString NAME = UI.FormatAsLink("Sugar Engine", "SUGARENGINE");

				// Token: 0x0400942A RID: 37930
				public static LocString DESC = "Not the most stylish way to travel space, but certainly the tastiest.";

				// Token: 0x0400942B RID: 37931
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Burns ",
					UI.FormatAsLink("Sucrose", "SUCROSE"),
					" to propel rockets for short range space exploration.\n\nSugar Engines have higher height restrictions than ",
					UI.FormatAsLink("Carbon Dioxide Engines", "CO2ENGINE"),
					", but move slower.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nOnce the engine has been built, more rocket modules can be added."
				});
			}

			// Token: 0x02002158 RID: 8536
			public class HEPENGINE
			{
				// Token: 0x0400942C RID: 37932
				public static LocString NAME = UI.FormatAsLink("Radbolt Engine", "HEPENGINE");

				// Token: 0x0400942D RID: 37933
				public static LocString DESC = "Radbolt-fueled rockets support few modules, but travel exceptionally far.";

				// Token: 0x0400942E RID: 37934
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Injects ",
					UI.FormatAsLink("Radbolts", "RADIATION"),
					" into a reaction chamber to propel rockets for long-range space exploration.\n\nRadbolt Engines are faster than ",
					UI.FormatAsLink("Hydrogen Engines", "HYDROGENENGINE"),
					" but with a more restrictive height allowance.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nOnce the engine has been built, more rocket modules can be added."
				});

				// Token: 0x0400942F RID: 37935
				public static LocString LOGIC_PORT_STORAGE = "Radbolt Storage";

				// Token: 0x04009430 RID: 37936
				public static LocString LOGIC_PORT_STORAGE_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when its Radbolt Storage is full";

				// Token: 0x04009431 RID: 37937
				public static LocString LOGIC_PORT_STORAGE_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002159 RID: 8537
			public class ORBITALCARGOMODULE
			{
				// Token: 0x04009432 RID: 37938
				public static LocString NAME = UI.FormatAsLink("Orbital Cargo Module", "ORBITALCARGOMODULE");

				// Token: 0x04009433 RID: 37939
				public static LocString DESC = "It's a generally good idea to pack some supplies when exploring unknown worlds.";

				// Token: 0x04009434 RID: 37940
				public static LocString EFFECT = "Delivers cargo to the surface of Planetoids that do not yet have a " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + ". \n\nMust be built via Rocket Platform.";
			}

			// Token: 0x0200215A RID: 8538
			public class BATTERYMODULE
			{
				// Token: 0x04009435 RID: 37941
				public static LocString NAME = UI.FormatAsLink("Battery Module", "BATTERYMODULE");

				// Token: 0x04009436 RID: 37942
				public static LocString DESC = "Charging a battery module before takeoff makes it easier to power buildings during flight.";

				// Token: 0x04009437 RID: 37943
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores the excess ",
					UI.FormatAsLink("Power", "POWER"),
					" generated by a Rocket Engine or ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					".\n\nProvides stored power to ",
					UI.FormatAsLink("Interior Rocket Outlets", "ROCKETINTERIORPOWERPLUG"),
					".\n\nLoses charge over time. \n\nMust be built via Rocket Platform."
				});
			}

			// Token: 0x0200215B RID: 8539
			public class PIONEERMODULE
			{
				// Token: 0x04009438 RID: 37944
				public static LocString NAME = UI.FormatAsLink("Trailblazer Module", "PIONEERMODULE");

				// Token: 0x04009439 RID: 37945
				public static LocString DESC = "That's one small step for Dupekind.";

				// Token: 0x0400943A RID: 37946
				public static LocString EFFECT = "Enables travel to Planetoids that do not yet have a " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + ".\n\nCan hold one Duplicant traveller.\n\nDeployment is available while in a Starmap hex adjacent to a Planetoid. \n\nMust be built via Rocket Platform.";
			}

			// Token: 0x0200215C RID: 8540
			public class SOLARPANELMODULE
			{
				// Token: 0x0400943B RID: 37947
				public static LocString NAME = UI.FormatAsLink("Solar Panel Module", "SOLARPANELMODULE");

				// Token: 0x0400943C RID: 37948
				public static LocString DESC = "Collect solar energy before takeoff and during flight.";

				// Token: 0x0400943D RID: 37949
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Sunlight", "LIGHT"),
					" into electrical ",
					UI.FormatAsLink("Power", "POWER"),
					" for use on rockets.\n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nMust be exposed to space."
				});
			}

			// Token: 0x0200215D RID: 8541
			public class SCOUTMODULE
			{
				// Token: 0x0400943E RID: 37950
				public static LocString NAME = UI.FormatAsLink("Rover's Module", "SCOUTMODULE");

				// Token: 0x0400943F RID: 37951
				public static LocString DESC = "Rover can conduct explorations of planetoids that don't have rocket platforms built.";

				// Token: 0x04009440 RID: 37952
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Deploys one ",
					UI.FormatAsLink("Rover Bot", "SCOUT"),
					" for remote Planetoid exploration.\n\nDeployment is available while in a Starmap hex adjacent to a Planetoid. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x0200215E RID: 8542
			public class PIONEERLANDER
			{
				// Token: 0x04009441 RID: 37953
				public static LocString NAME = UI.FormatAsLink("Trailblazer Lander", "PIONEERLANDER");

				// Token: 0x04009442 RID: 37954
				public static LocString DESC = "Lands a Duplicant on a Planetoid from an orbiting " + BUILDINGS.PREFABS.PIONEERMODULE.NAME + ".";
			}

			// Token: 0x0200215F RID: 8543
			public class SCOUTLANDER
			{
				// Token: 0x04009443 RID: 37955
				public static LocString NAME = UI.FormatAsLink("Rover's Lander", "SCOUTLANDER");

				// Token: 0x04009444 RID: 37956
				public static LocString DESC = string.Concat(new string[]
				{
					"Lands ",
					UI.FormatAsLink("Rover", "SCOUT"),
					" on a Planetoid when ",
					BUILDINGS.PREFABS.SCOUTMODULE.NAME,
					" is in orbit."
				});
			}

			// Token: 0x02002160 RID: 8544
			public class GANTRY
			{
				// Token: 0x04009445 RID: 37957
				public static LocString NAME = UI.FormatAsLink("Gantry", "GANTRY");

				// Token: 0x04009446 RID: 37958
				public static LocString DESC = "A gantry can be built over rocket pieces where ladders and tile cannot.";

				// Token: 0x04009447 RID: 37959
				public static LocString EFFECT = "Provides scaffolding across rocket modules to allow Duplicant access.";

				// Token: 0x04009448 RID: 37960
				public static LocString LOGIC_PORT = "Extend/Retract";

				// Token: 0x04009449 RID: 37961
				public static LocString LOGIC_PORT_ACTIVE = "<b>Extends gantry</b> when a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " signal is received";

				// Token: 0x0400944A RID: 37962
				public static LocString LOGIC_PORT_INACTIVE = "<b>Retracts gantry</b> when a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " signal is received";
			}

			// Token: 0x02002161 RID: 8545
			public class ROCKETINTERIORPOWERPLUG
			{
				// Token: 0x0400944B RID: 37963
				public static LocString NAME = UI.FormatAsLink("Power Outlet Fitting", "ROCKETINTERIORPOWERPLUG");

				// Token: 0x0400944C RID: 37964
				public static LocString DESC = "Outlets conveniently power buildings inside a cockpit using their rocket's power stores.";

				// Token: 0x0400944D RID: 37965
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Provides ",
					UI.FormatAsLink("Power", "POWER"),
					" to connected buildings.\n\nPulls power from ",
					UI.FormatAsLink("Battery Modules", "BATTERYMODULE"),
					" and Rocket Engines.\n\nMust be built within the interior of a Rocket Module."
				});
			}

			// Token: 0x02002162 RID: 8546
			public class ROCKETINTERIORLIQUIDINPUT
			{
				// Token: 0x0400944E RID: 37966
				public static LocString NAME = UI.FormatAsLink("Liquid Intake Fitting", "ROCKETINTERIORLIQUIDINPUT");

				// Token: 0x0400944F RID: 37967
				public static LocString DESC = "Begone, foul waters!";

				// Token: 0x04009450 RID: 37968
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					" to be pumped into rocket storage via ",
					UI.FormatAsLink("Pipes", "LIQUIDCONDUIT"),
					".\n\nSends liquid to the first Rocket Module with available space.\n\nMust be built within the interior of a Rocket Module."
				});
			}

			// Token: 0x02002163 RID: 8547
			public class ROCKETINTERIORLIQUIDOUTPUT
			{
				// Token: 0x04009451 RID: 37969
				public static LocString NAME = UI.FormatAsLink("Liquid Output Fitting", "ROCKETINTERIORLIQUIDOUTPUT");

				// Token: 0x04009452 RID: 37970
				public static LocString DESC = "Now if only we had some water balloons...";

				// Token: 0x04009453 RID: 37971
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					" to be drawn from rocket storage via ",
					UI.FormatAsLink("Pipes", "LIQUIDCONDUIT"),
					".\n\nDraws liquid from the first Rocket Module with the requested material.\n\nMust be built within the interior of a Rocket Module."
				});
			}

			// Token: 0x02002164 RID: 8548
			public class ROCKETINTERIORGASINPUT
			{
				// Token: 0x04009454 RID: 37972
				public static LocString NAME = UI.FormatAsLink("Gas Intake Fitting", "ROCKETINTERIORGASINPUT");

				// Token: 0x04009455 RID: 37973
				public static LocString DESC = "It's basically central-vac.";

				// Token: 0x04009456 RID: 37974
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows ",
					UI.FormatAsLink("Gases", "ELEMENTS_GAS"),
					" to be pumped into rocket storage via ",
					UI.FormatAsLink("Pipes", "GASCONDUIT"),
					".\n\nSends gas to the first Rocket Module with available space.\n\nMust be built within the interior of a Rocket Module."
				});
			}

			// Token: 0x02002165 RID: 8549
			public class ROCKETINTERIORGASOUTPUT
			{
				// Token: 0x04009457 RID: 37975
				public static LocString NAME = UI.FormatAsLink("Gas Output Fitting", "ROCKETINTERIORGASOUTPUT");

				// Token: 0x04009458 RID: 37976
				public static LocString DESC = "Refreshing breezes, on-demand.";

				// Token: 0x04009459 RID: 37977
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows ",
					UI.FormatAsLink("Gases", "ELEMENTS_GAS"),
					" to be drawn from rocket storage via ",
					UI.FormatAsLink("Pipes", "GASCONDUIT"),
					".\n\nDraws gas from the first Rocket Module with the requested material.\n\nMust be built within the interior of a Rocket Module."
				});
			}

			// Token: 0x02002166 RID: 8550
			public class ROCKETINTERIORSOLIDINPUT
			{
				// Token: 0x0400945A RID: 37978
				public static LocString NAME = UI.FormatAsLink("Conveyor Receptacle Fitting", "ROCKETINTERIORSOLIDINPUT");

				// Token: 0x0400945B RID: 37979
				public static LocString DESC = "Why organize your shelves when you can just shove everything in here?";

				// Token: 0x0400945C RID: 37980
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" to be moved into rocket storage via ",
					UI.FormatAsLink("Conveyor Rails", "SOLIDCONDUIT"),
					".\n\nSends solid material to the first Rocket Module with available space.\n\nMust be built within the interior of a Rocket Module."
				});
			}

			// Token: 0x02002167 RID: 8551
			public class ROCKETINTERIORSOLIDOUTPUT
			{
				// Token: 0x0400945D RID: 37981
				public static LocString NAME = UI.FormatAsLink("Conveyor Loader Fitting", "ROCKETINTERIORSOLIDOUTPUT");

				// Token: 0x0400945E RID: 37982
				public static LocString DESC = "For accessing your stored luggage mid-flight.";

				// Token: 0x0400945F RID: 37983
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" to be moved out of rocket storage via ",
					UI.FormatAsLink("Conveyor Rails", "SOLIDCONDUIT"),
					".\n\nDraws solid material from the first Rocket Module with the requested material.\n\nMust be built within the interior of a Rocket Module."
				});
			}

			// Token: 0x02002168 RID: 8552
			public class WATERCOOLER
			{
				// Token: 0x04009460 RID: 37984
				public static LocString NAME = UI.FormatAsLink("Water Cooler", "WATERCOOLER");

				// Token: 0x04009461 RID: 37985
				public static LocString DESC = "Chatting with friends improves Duplicants' moods and reduces their stress.";

				// Token: 0x04009462 RID: 37986
				public static LocString EFFECT = "Provides a gathering place for Duplicants during Downtime.\n\nImproves Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";
			}

			// Token: 0x02002169 RID: 8553
			public class ARCADEMACHINE
			{
				// Token: 0x04009463 RID: 37987
				public static LocString NAME = UI.FormatAsLink("Arcade Cabinet", "ARCADEMACHINE");

				// Token: 0x04009464 RID: 37988
				public static LocString DESC = "Komet Kablam-O!\nFor up to two players.";

				// Token: 0x04009465 RID: 37989
				public static LocString EFFECT = "Allows Duplicants to play video games on their breaks.\n\nIncreases Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";
			}

			// Token: 0x0200216A RID: 8554
			public class SINGLEPLAYERARCADE
			{
				// Token: 0x04009466 RID: 37990
				public static LocString NAME = UI.FormatAsLink("Single Player Arcade", "SINGLEPLAYERARCADE");

				// Token: 0x04009467 RID: 37991
				public static LocString DESC = "Space Brawler IV! For one player.";

				// Token: 0x04009468 RID: 37992
				public static LocString EFFECT = "Allows a Duplicant to play video games solo on their breaks.\n\nIncreases Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";
			}

			// Token: 0x0200216B RID: 8555
			public class PHONOBOX
			{
				// Token: 0x04009469 RID: 37993
				public static LocString NAME = UI.FormatAsLink("Jukebot", "PHONOBOX");

				// Token: 0x0400946A RID: 37994
				public static LocString DESC = "Dancing helps Duplicants get their innermost feelings out.";

				// Token: 0x0400946B RID: 37995
				public static LocString EFFECT = "Plays music for Duplicants to dance to on their breaks.\n\nIncreases Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";
			}

			// Token: 0x0200216C RID: 8556
			public class JUICER
			{
				// Token: 0x0400946C RID: 37996
				public static LocString NAME = UI.FormatAsLink("Juicer", "JUICER");

				// Token: 0x0400946D RID: 37997
				public static LocString DESC = "Fruity juice can really brighten a Duplicant's breaktime";

				// Token: 0x0400946E RID: 37998
				public static LocString EFFECT = "Provides refreshment for Duplicants on their breaks.\n\nDrinking juice increases Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";
			}

			// Token: 0x0200216D RID: 8557
			public class ESPRESSOMACHINE
			{
				// Token: 0x0400946F RID: 37999
				public static LocString NAME = UI.FormatAsLink("Espresso Machine", "ESPRESSOMACHINE");

				// Token: 0x04009470 RID: 38000
				public static LocString DESC = "A shot of espresso helps Duplicants relax after a long day.";

				// Token: 0x04009471 RID: 38001
				public static LocString EFFECT = "Provides refreshment for Duplicants on their breaks.\n\nIncreases Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";
			}

			// Token: 0x0200216E RID: 8558
			public class TELEPHONE
			{
				// Token: 0x04009472 RID: 38002
				public static LocString NAME = UI.FormatAsLink("Party Line Phone", "TELEPHONE");

				// Token: 0x04009473 RID: 38003
				public static LocString DESC = "You never know who you'll meet on the other line.";

				// Token: 0x04009474 RID: 38004
				public static LocString EFFECT = "Can be used by one Duplicant to chat with themselves or with other Duplicants in different locations.\n\nChatting increases Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";

				// Token: 0x04009475 RID: 38005
				public static LocString EFFECT_BABBLE = "{attrib}: {amount} (No One)";

				// Token: 0x04009476 RID: 38006
				public static LocString EFFECT_BABBLE_TOOLTIP = "Duplicants will gain {amount} {attrib} if they chat only with themselves.";

				// Token: 0x04009477 RID: 38007
				public static LocString EFFECT_CHAT = "{attrib}: {amount} (At least one Duplicant)";

				// Token: 0x04009478 RID: 38008
				public static LocString EFFECT_CHAT_TOOLTIP = "Duplicants will gain {amount} {attrib} if they chat with at least one other Duplicant.";

				// Token: 0x04009479 RID: 38009
				public static LocString EFFECT_LONG_DISTANCE = "{attrib}: {amount} (At least one Duplicant across space)";

				// Token: 0x0400947A RID: 38010
				public static LocString EFFECT_LONG_DISTANCE_TOOLTIP = "Duplicants will gain {amount} {attrib} if they chat with at least one other Duplicant across space.";
			}

			// Token: 0x0200216F RID: 8559
			public class MODULARLIQUIDINPUT
			{
				// Token: 0x0400947B RID: 38011
				public static LocString NAME = UI.FormatAsLink("Liquid Input Hub", "MODULARLIQUIDINPUT");

				// Token: 0x0400947C RID: 38012
				public static LocString DESC = "A hub from which to input " + UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID") + ".";
			}

			// Token: 0x02002170 RID: 8560
			public class MODULARSOLIDINPUT
			{
				// Token: 0x0400947D RID: 38013
				public static LocString NAME = UI.FormatAsLink("Solid Input Hub", "MODULARSOLIDINPUT");

				// Token: 0x0400947E RID: 38014
				public static LocString DESC = "A hub from which to input " + UI.FormatAsLink("Solids", "ELEMENTS_SOLID") + ".";
			}

			// Token: 0x02002171 RID: 8561
			public class MODULARGASINPUT
			{
				// Token: 0x0400947F RID: 38015
				public static LocString NAME = UI.FormatAsLink("Gas Input Hub", "MODULARGASINPUT");

				// Token: 0x04009480 RID: 38016
				public static LocString DESC = "A hub from which to input " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + ".";
			}

			// Token: 0x02002172 RID: 8562
			public class MECHANICALSURFBOARD
			{
				// Token: 0x04009481 RID: 38017
				public static LocString NAME = UI.FormatAsLink("Mechanical Surfboard", "MECHANICALSURFBOARD");

				// Token: 0x04009482 RID: 38018
				public static LocString DESC = "Mechanical waves make for radical relaxation time.";

				// Token: 0x04009483 RID: 38019
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Increases Duplicant ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nSome ",
					UI.FormatAsLink("Water", "WATER"),
					" gets splashed on the floor during use."
				});

				// Token: 0x04009484 RID: 38020
				public static LocString WATER_REQUIREMENT = "{element}: {amount}";

				// Token: 0x04009485 RID: 38021
				public static LocString WATER_REQUIREMENT_TOOLTIP = "This building must be filled with {amount} {element} in order to function.";

				// Token: 0x04009486 RID: 38022
				public static LocString LEAK_REQUIREMENT = "Spillage: {amount}";

				// Token: 0x04009487 RID: 38023
				public static LocString LEAK_REQUIREMENT_TOOLTIP = "This building will spill {amount} of its contents on to the floor during use, which must be replenished.";
			}

			// Token: 0x02002173 RID: 8563
			public class SAUNA
			{
				// Token: 0x04009488 RID: 38024
				public static LocString NAME = UI.FormatAsLink("Sauna", "SAUNA");

				// Token: 0x04009489 RID: 38025
				public static LocString DESC = "A steamy sauna soothes away all the aches and pains.";

				// Token: 0x0400948A RID: 38026
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Steam", "STEAM"),
					" to create a relaxing atmosphere.\n\nIncreases Duplicant ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});
			}

			// Token: 0x02002174 RID: 8564
			public class BEACHCHAIR
			{
				// Token: 0x0400948B RID: 38027
				public static LocString NAME = UI.FormatAsLink("Beach Chair", "BEACHCHAIR");

				// Token: 0x0400948C RID: 38028
				public static LocString DESC = "Soak up some relaxing sun rays.";

				// Token: 0x0400948D RID: 38029
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Duplicants can relax by lounging in ",
					UI.FormatAsLink("Sunlight", "LIGHT"),
					".\n\nIncreases Duplicant ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});

				// Token: 0x0400948E RID: 38030
				public static LocString LIGHTEFFECT_LOW = "{attrib}: {amount} (Dim Light)";

				// Token: 0x0400948F RID: 38031
				public static LocString LIGHTEFFECT_LOW_TOOLTIP = "Duplicants will gain {amount} {attrib} if this building is in light dimmer than {lux}.";

				// Token: 0x04009490 RID: 38032
				public static LocString LIGHTEFFECT_HIGH = "{attrib}: {amount} (Bright Light)";

				// Token: 0x04009491 RID: 38033
				public static LocString LIGHTEFFECT_HIGH_TOOLTIP = "Duplicants will gain {amount} {attrib} if this building is in at least {lux} light.";
			}

			// Token: 0x02002175 RID: 8565
			public class SUNLAMP
			{
				// Token: 0x04009492 RID: 38034
				public static LocString NAME = UI.FormatAsLink("Sun Lamp", "SUNLAMP");

				// Token: 0x04009493 RID: 38035
				public static LocString DESC = "An artificial ray of sunshine.";

				// Token: 0x04009494 RID: 38036
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Gives off ",
					UI.FormatAsLink("Sunlight", "LIGHT"),
					" level Lux.\n\nCan be paired with ",
					UI.FormatAsLink("Beach Chairs", "BEACHCHAIR"),
					"."
				});
			}

			// Token: 0x02002176 RID: 8566
			public class VERTICALWINDTUNNEL
			{
				// Token: 0x04009495 RID: 38037
				public static LocString NAME = UI.FormatAsLink("Vertical Wind Tunnel", "VERTICALWINDTUNNEL");

				// Token: 0x04009496 RID: 38038
				public static LocString DESC = "Duplicants love the feeling of high-powered wind through their hair.";

				// Token: 0x04009497 RID: 38039
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Must be connected to a ",
					UI.FormatAsLink("Power Source", "POWER"),
					". To properly function, the area under this building must be left vacant.\n\nIncreases Duplicants ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});

				// Token: 0x04009498 RID: 38040
				public static LocString DISPLACEMENTEFFECT = "Gas Displacement: {amount}";

				// Token: 0x04009499 RID: 38041
				public static LocString DISPLACEMENTEFFECT_TOOLTIP = "This building will displace {amount} Gas while in use.";
			}

			// Token: 0x02002177 RID: 8567
			public class TELEPORTALPAD
			{
				// Token: 0x0400949A RID: 38042
				public static LocString NAME = UI.FormatAsLink("Teleporter Pad", "TELEPORTALPAD");

				// Token: 0x0400949B RID: 38043
				public static LocString DESC = "Duplicants are just atoms as far as the pad's concerned.";

				// Token: 0x0400949C RID: 38044
				public static LocString EFFECT = "Instantly transports Duplicants and items to another portal with the same portal code.";

				// Token: 0x0400949D RID: 38045
				public static LocString LOGIC_PORT = "Portal Code Input";

				// Token: 0x0400949E RID: 38046
				public static LocString LOGIC_PORT_ACTIVE = "1";

				// Token: 0x0400949F RID: 38047
				public static LocString LOGIC_PORT_INACTIVE = "0";
			}

			// Token: 0x02002178 RID: 8568
			public class CHECKPOINT
			{
				// Token: 0x040094A0 RID: 38048
				public static LocString NAME = UI.FormatAsLink("Duplicant Checkpoint", "CHECKPOINT");

				// Token: 0x040094A1 RID: 38049
				public static LocString DESC = "Checkpoints can be connected to automated sensors to determine when it's safe to enter.";

				// Token: 0x040094A2 RID: 38050
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to pass when receiving a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					".\n\nPrevents Duplicants from passing when receiving a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					"."
				});

				// Token: 0x040094A3 RID: 38051
				public static LocString LOGIC_PORT = "Duplicant Stop/Go";

				// Token: 0x040094A4 RID: 38052
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Allow Duplicant passage";

				// Token: 0x040094A5 RID: 38053
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Prevent Duplicant passage";
			}

			// Token: 0x02002179 RID: 8569
			public class FIREPOLE
			{
				// Token: 0x040094A6 RID: 38054
				public static LocString NAME = UI.FormatAsLink("Fire Pole", "FIREPOLE");

				// Token: 0x040094A7 RID: 38055
				public static LocString DESC = "Build these in addition to ladders for efficient upward and downward movement.";

				// Token: 0x040094A8 RID: 38056
				public static LocString EFFECT = "Allows rapid Duplicant descent.\n\nSignificantly slows upward climbing.";
			}

			// Token: 0x0200217A RID: 8570
			public class FLOORSWITCH
			{
				// Token: 0x040094A9 RID: 38057
				public static LocString NAME = UI.FormatAsLink("Weight Plate", "FLOORSWITCH");

				// Token: 0x040094AA RID: 38058
				public static LocString DESC = "Weight plates can be used to turn on amenities only when Duplicants pass by.";

				// Token: 0x040094AB RID: 38059
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" when an object or Duplicant is placed atop of it.\n\nCannot be triggered by ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" or ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					"."
				});

				// Token: 0x040094AC RID: 38060
				public static LocString LOGIC_PORT_DESC = UI.FormatAsLink("Active", "LOGIC") + "/" + UI.FormatAsLink("Inactive", "LOGIC");
			}

			// Token: 0x0200217B RID: 8571
			public class KILN
			{
				// Token: 0x040094AD RID: 38061
				public static LocString NAME = UI.FormatAsLink("Kiln", "KILN");

				// Token: 0x040094AE RID: 38062
				public static LocString DESC = "Kilns can also be used to refine coal into pure carbon.";

				// Token: 0x040094AF RID: 38063
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Fires ",
					UI.FormatAsLink("Clay", "CLAY"),
					" to produce ",
					UI.FormatAsLink("Ceramic", "CERAMIC"),
					".\n\nDuplicants will not fabricate items unless recipes are queued."
				});
			}

			// Token: 0x0200217C RID: 8572
			public class LIQUIDFUELTANK
			{
				// Token: 0x040094B0 RID: 38064
				public static LocString NAME = UI.FormatAsLink("Liquid Fuel Tank", "LIQUIDFUELTANK");

				// Token: 0x040094B1 RID: 38065
				public static LocString DESC = "Storing additional fuel increases the distance a rocket can travel before returning.";

				// Token: 0x040094B2 RID: 38066
				public static LocString EFFECT = "Stores the " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " fuel piped into it to supply rocket engines.\n\nThe stored fuel type is determined by the rocket engine it is built upon.";
			}

			// Token: 0x0200217D RID: 8573
			public class LIQUIDFUELTANKCLUSTER
			{
				// Token: 0x040094B3 RID: 38067
				public static LocString NAME = UI.FormatAsLink("Large Liquid Fuel Tank", "LIQUIDFUELTANKCLUSTER");

				// Token: 0x040094B4 RID: 38068
				public static LocString DESC = "Storing additional fuel increases the distance a rocket can travel before returning.";

				// Token: 0x040094B5 RID: 38069
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores the ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" fuel piped into it to supply rocket engines.\n\nThe stored fuel type is determined by the rocket engine it is built upon. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x0200217E RID: 8574
			public class LANDING_POD
			{
				// Token: 0x040094B6 RID: 38070
				public static LocString NAME = "Spacefarer Deploy Pod";

				// Token: 0x040094B7 RID: 38071
				public static LocString DESC = "Geronimo!";

				// Token: 0x040094B8 RID: 38072
				public static LocString EFFECT = "Contains a Duplicant deployed from orbit.\n\nPod will disintegrate on arrival.";
			}

			// Token: 0x0200217F RID: 8575
			public class ROCKETPOD
			{
				// Token: 0x040094B9 RID: 38073
				public static LocString NAME = UI.FormatAsLink("Trailblazer Deploy Pod", "ROCKETPOD");

				// Token: 0x040094BA RID: 38074
				public static LocString DESC = "The Duplicant inside is equal parts nervous and excited.";

				// Token: 0x040094BB RID: 38075
				public static LocString EFFECT = "Contains a Duplicant deployed from orbit by a " + BUILDINGS.PREFABS.PIONEERMODULE.NAME + ".\n\nPod will disintegrate on arrival.";
			}

			// Token: 0x02002180 RID: 8576
			public class SCOUTROCKETPOD
			{
				// Token: 0x040094BC RID: 38076
				public static LocString NAME = UI.FormatAsLink("Rover's Doghouse", "SCOUTROCKETPOD");

				// Token: 0x040094BD RID: 38077
				public static LocString DESC = "Good luck out there, boy!";

				// Token: 0x040094BE RID: 38078
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Contains a ",
					UI.FormatAsLink("Rover", "SCOUT"),
					" deployed from an orbiting ",
					BUILDINGS.PREFABS.SCOUTMODULE.NAME,
					".\n\nPod will disintegrate on arrival."
				});
			}

			// Token: 0x02002181 RID: 8577
			public class ROCKETCOMMANDCONSOLE
			{
				// Token: 0x040094BF RID: 38079
				public static LocString NAME = UI.FormatAsLink("Rocket Cockpit", "ROCKETCOMMANDCONSOLE");

				// Token: 0x040094C0 RID: 38080
				public static LocString DESC = "Looks kinda fun.";

				// Token: 0x040094C1 RID: 38081
				public static LocString EFFECT = "Allows a Duplicant to pilot a rocket.\n\nCargo rockets must possess a Rocket Cockpit in order to function.";
			}

			// Token: 0x02002182 RID: 8578
			public class ROCKETENVELOPETILE
			{
				// Token: 0x040094C2 RID: 38082
				public static LocString NAME = UI.FormatAsLink("Rocket", "ROCKETENVELOPETILE");

				// Token: 0x040094C3 RID: 38083
				public static LocString DESC = "Keeps the space out.";

				// Token: 0x040094C4 RID: 38084
				public static LocString EFFECT = "The walls of a rocket.";
			}

			// Token: 0x02002183 RID: 8579
			public class ROCKETENVELOPEWINDOWTILE
			{
				// Token: 0x040094C5 RID: 38085
				public static LocString NAME = UI.FormatAsLink("Rocket Window", "ROCKETENVELOPEWINDOWTILE");

				// Token: 0x040094C6 RID: 38086
				public static LocString DESC = "I can see my asteroid from here!";

				// Token: 0x040094C7 RID: 38087
				public static LocString EFFECT = "The window of a rocket.";
			}

			// Token: 0x02002184 RID: 8580
			public class ROCKETWALLTILE
			{
				// Token: 0x040094C8 RID: 38088
				public static LocString NAME = UI.FormatAsLink("Rocket Wall", "ROCKETENVELOPETILE");

				// Token: 0x040094C9 RID: 38089
				public static LocString DESC = "Keeps the space out.";

				// Token: 0x040094CA RID: 38090
				public static LocString EFFECT = "The walls of a rocket.";
			}

			// Token: 0x02002185 RID: 8581
			public class SMALLOXIDIZERTANK
			{
				// Token: 0x040094CB RID: 38091
				public static LocString NAME = UI.FormatAsLink("Small Solid Oxidizer Tank", "SMALLOXIDIZERTANK");

				// Token: 0x040094CC RID: 38092
				public static LocString DESC = "Solid oxidizers allows rocket fuel to be efficiently burned in the vacuum of space.";

				// Token: 0x040094CD RID: 38093
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Fertilizer", "Fertilizer"),
					" and ",
					UI.FormatAsLink("Oxylite", "OXYROCK"),
					" for burning rocket fuels. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});

				// Token: 0x040094CE RID: 38094
				public static LocString UI_FILTER_CATEGORY = "Accepted Oxidizers";
			}

			// Token: 0x02002186 RID: 8582
			public class OXIDIZERTANK
			{
				// Token: 0x040094CF RID: 38095
				public static LocString NAME = UI.FormatAsLink("Solid Oxidizer Tank", "OXIDIZERTANK");

				// Token: 0x040094D0 RID: 38096
				public static LocString DESC = "Solid oxidizers allows rocket fuel to be efficiently burned in the vacuum of space.";

				// Token: 0x040094D1 RID: 38097
				public static LocString EFFECT = "Stores " + UI.FormatAsLink("Oxylite", "OXYROCK") + " and other oxidizers for burning rocket fuels.";

				// Token: 0x040094D2 RID: 38098
				public static LocString UI_FILTER_CATEGORY = "Accepted Oxidizers";
			}

			// Token: 0x02002187 RID: 8583
			public class OXIDIZERTANKCLUSTER
			{
				// Token: 0x040094D3 RID: 38099
				public static LocString NAME = UI.FormatAsLink("Large Solid Oxidizer Tank", "OXIDIZERTANKCLUSTER");

				// Token: 0x040094D4 RID: 38100
				public static LocString DESC = "Solid oxidizers allows rocket fuel to be efficiently burned in the vacuum of space.";

				// Token: 0x040094D5 RID: 38101
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Oxylite", "OXYROCK"),
					" and other oxidizers for burning rocket fuels.\n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});

				// Token: 0x040094D6 RID: 38102
				public static LocString UI_FILTER_CATEGORY = "Accepted Oxidizers";
			}

			// Token: 0x02002188 RID: 8584
			public class OXIDIZERTANKLIQUID
			{
				// Token: 0x040094D7 RID: 38103
				public static LocString NAME = UI.FormatAsLink("Liquid Oxidizer Tank", "LIQUIDOXIDIZERTANK");

				// Token: 0x040094D8 RID: 38104
				public static LocString DESC = "Liquid oxygen improves the thrust-to-mass ratio of rocket fuels.";

				// Token: 0x040094D9 RID: 38105
				public static LocString EFFECT = "Stores " + UI.FormatAsLink("Liquid Oxygen", "LIQUIDOXYGEN") + " for burning rocket fuels.";
			}

			// Token: 0x02002189 RID: 8585
			public class OXIDIZERTANKLIQUIDCLUSTER
			{
				// Token: 0x040094DA RID: 38106
				public static LocString NAME = UI.FormatAsLink("Liquid Oxidizer Tank", "LIQUIDOXIDIZERTANKCLUSTER");

				// Token: 0x040094DB RID: 38107
				public static LocString DESC = "Liquid oxygen improves the thrust-to-mass ratio of rocket fuels.";

				// Token: 0x040094DC RID: 38108
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Liquid Oxygen", "LIQUIDOXYGEN"),
					" for burning rocket fuels. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x0200218A RID: 8586
			public class LIQUIDCONDITIONER
			{
				// Token: 0x040094DD RID: 38109
				public static LocString NAME = UI.FormatAsLink("Thermo Aquatuner", "LIQUIDCONDITIONER");

				// Token: 0x040094DE RID: 38110
				public static LocString DESC = "A thermo aquatuner cools liquid and outputs the heat elsewhere.";

				// Token: 0x040094DF RID: 38111
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Cools the ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" piped through it, but outputs ",
					UI.FormatAsLink("Heat", "HEAT"),
					" in its immediate vicinity."
				});
			}

			// Token: 0x0200218B RID: 8587
			public class LIQUIDCARGOBAY
			{
				// Token: 0x040094E0 RID: 38112
				public static LocString NAME = UI.FormatAsLink("Liquid Cargo Tank", "LIQUIDCARGOBAY");

				// Token: 0x040094E1 RID: 38113
				public static LocString DESC = "Duplicants will fill cargo bays with any resources they find during space missions.";

				// Token: 0x040094E2 RID: 38114
				public static LocString EFFECT = "Allows Duplicants to store any " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " resources found during space missions.\n\nStored resources become available to the colony upon the rocket's return.";
			}

			// Token: 0x0200218C RID: 8588
			public class LIQUIDCARGOBAYCLUSTER
			{
				// Token: 0x040094E3 RID: 38115
				public static LocString NAME = UI.FormatAsLink("Large Liquid Cargo Tank", "LIQUIDCARGOBAY");

				// Token: 0x040094E4 RID: 38116
				public static LocString DESC = "Holds more than a regular cargo tank.";

				// Token: 0x040094E5 RID: 38117
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to store most of the ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" resources found during space missions.\n\nStored resources become available to the colony upon the rocket's return.\n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x0200218D RID: 8589
			public class LIQUIDCARGOBAYSMALL
			{
				// Token: 0x040094E6 RID: 38118
				public static LocString NAME = UI.FormatAsLink("Liquid Cargo Tank", "LIQUIDCARGOBAYSMALL");

				// Token: 0x040094E7 RID: 38119
				public static LocString DESC = "Duplicants will fill cargo tanks with whatever resources they find during space missions.";

				// Token: 0x040094E8 RID: 38120
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to store some of the ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" resources found during space missions.\n\nStored resources become available to the colony upon the rocket's return. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x0200218E RID: 8590
			public class LUXURYBED
			{
				// Token: 0x040094E9 RID: 38121
				public static LocString NAME = UI.FormatAsLink("Comfy Bed", "LUXURYBED");

				// Token: 0x040094EA RID: 38122
				public static LocString DESC = "Duplicants prefer comfy beds to cots and gain more stamina from sleeping in them.";

				// Token: 0x040094EB RID: 38123
				public static LocString EFFECT = "Provides a sleeping area for one Duplicant and restores additional " + UI.FormatAsLink("Stamina", "STAMINA") + ".\n\nDuplicants will automatically sleep in their assigned beds at night.";

				// Token: 0x02002DBC RID: 11708
				public class FACADES
				{
					// Token: 0x02002FEB RID: 12267
					public class DEFAULT_LUXURYBED
					{
						// Token: 0x0400C057 RID: 49239
						public static LocString NAME = UI.FormatAsLink("Comfy Bed", "LUXURYBED");

						// Token: 0x0400C058 RID: 49240
						public static LocString DESC = "Much comfier than a cot.";
					}

					// Token: 0x02002FEC RID: 12268
					public class GRANDPRIX
					{
						// Token: 0x0400C059 RID: 49241
						public static LocString NAME = UI.FormatAsLink("Grand Prix Bed", "LUXURYBED");

						// Token: 0x0400C05A RID: 49242
						public static LocString DESC = "Where every Duplicant wakes up a winner.";
					}

					// Token: 0x02002FED RID: 12269
					public class BOAT
					{
						// Token: 0x0400C05B RID: 49243
						public static LocString NAME = UI.FormatAsLink("Dreamboat Bed", "LUXURYBED");

						// Token: 0x0400C05C RID: 49244
						public static LocString DESC = "Ahoy! Set sail for zzzzz's.";
					}

					// Token: 0x02002FEE RID: 12270
					public class ROCKET_BED
					{
						// Token: 0x0400C05D RID: 49245
						public static LocString NAME = UI.FormatAsLink("S.S. Napmaster Bed", "LUXURYBED");

						// Token: 0x0400C05E RID: 49246
						public static LocString DESC = "Launches sleepy Duplicants into a deep-space slumber.";
					}

					// Token: 0x02002FEF RID: 12271
					public class BOUNCY_BED
					{
						// Token: 0x0400C05F RID: 49247
						public static LocString NAME = UI.FormatAsLink("Bouncy Castle Bed", "LUXURYBED");

						// Token: 0x0400C060 RID: 49248
						public static LocString DESC = "An inflatable party prop makes a surprisingly good bed.";
					}

					// Token: 0x02002FF0 RID: 12272
					public class PUFT_BED
					{
						// Token: 0x0400C061 RID: 49249
						public static LocString NAME = UI.FormatAsLink("Puft Bed", "LUXURYBED");

						// Token: 0x0400C062 RID: 49250
						public static LocString DESC = "A comfy, if somewhat 'fragrant', place to sleep.";
					}
				}
			}

			// Token: 0x0200218F RID: 8591
			public class LADDERBED
			{
				// Token: 0x040094EC RID: 38124
				public static LocString NAME = UI.FormatAsLink("Ladder Bed", "LADDERBED");

				// Token: 0x040094ED RID: 38125
				public static LocString DESC = "Duplicant's sleep will be interrupted if another Duplicant uses the ladder.";

				// Token: 0x040094EE RID: 38126
				public static LocString EFFECT = "Provides a sleeping area for one Duplicant and also functions as a ladder.\n\nDuplicants will automatically sleep in their assigned beds at night.";
			}

			// Token: 0x02002190 RID: 8592
			public class MEDICALCOT
			{
				// Token: 0x040094EF RID: 38127
				public static LocString NAME = UI.FormatAsLink("Triage Cot", "MEDICALCOT");

				// Token: 0x040094F0 RID: 38128
				public static LocString DESC = "Duplicants use triage cots to recover from physical injuries and receive aid from peers.";

				// Token: 0x040094F1 RID: 38129
				public static LocString EFFECT = "Accelerates " + UI.FormatAsLink("Health", "HEALTH") + " restoration and the healing of physical injuries.\n\nRevives incapacitated Duplicants.";
			}

			// Token: 0x02002191 RID: 8593
			public class DOCTORSTATION
			{
				// Token: 0x040094F2 RID: 38130
				public static LocString NAME = UI.FormatAsLink("Sick Bay", "DOCTORSTATION");

				// Token: 0x040094F3 RID: 38131
				public static LocString DESC = "Sick bays can be placed in hospital rooms to decrease the likelihood of disease spreading.";

				// Token: 0x040094F4 RID: 38132
				public static LocString EFFECT = "Allows Duplicants to administer basic treatments to sick Duplicants.\n\nDuplicants must possess the Bedside Manner " + UI.FormatAsLink("Skill", "ROLES") + " to treat peers.";
			}

			// Token: 0x02002192 RID: 8594
			public class ADVANCEDDOCTORSTATION
			{
				// Token: 0x040094F5 RID: 38133
				public static LocString NAME = UI.FormatAsLink("Disease Clinic", "ADVANCEDDOCTORSTATION");

				// Token: 0x040094F6 RID: 38134
				public static LocString DESC = "Disease clinics require power, but treat more serious illnesses than sick bays alone.";

				// Token: 0x040094F7 RID: 38135
				public static LocString EFFECT = "Allows Duplicants to administer powerful treatments to sick Duplicants.\n\nDuplicants must possess the Advanced Medical Care " + UI.FormatAsLink("Skill", "ROLES") + " to treat peers.";
			}

			// Token: 0x02002193 RID: 8595
			public class MASSAGETABLE
			{
				// Token: 0x040094F8 RID: 38136
				public static LocString NAME = UI.FormatAsLink("Massage Table", "MASSAGETABLE");

				// Token: 0x040094F9 RID: 38137
				public static LocString DESC = "Massage tables quickly reduce extreme stress, at the cost of power production.";

				// Token: 0x040094FA RID: 38138
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Rapidly reduces ",
					UI.FormatAsLink("Stress", "STRESS"),
					" for the Duplicant user.\n\nDuplicants will automatically seek a massage table when ",
					UI.FormatAsLink("Stress", "STRESS"),
					" exceeds breaktime range."
				});

				// Token: 0x040094FB RID: 38139
				public static LocString ACTIVATE_TOOLTIP = "Duplicants must take a massage break when their " + UI.FormatAsKeyWord("Stress") + " reaches {0}%";

				// Token: 0x040094FC RID: 38140
				public static LocString DEACTIVATE_TOOLTIP = "Breaktime ends when " + UI.FormatAsKeyWord("Stress") + " is reduced to {0}%";
			}

			// Token: 0x02002194 RID: 8596
			public class CEILINGLIGHT
			{
				// Token: 0x040094FD RID: 38141
				public static LocString NAME = UI.FormatAsLink("Ceiling Light", "CEILINGLIGHT");

				// Token: 0x040094FE RID: 38142
				public static LocString DESC = "Light reduces Duplicant stress and is required to grow certain plants.";

				// Token: 0x040094FF RID: 38143
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Provides ",
					UI.FormatAsLink("Light", "LIGHT"),
					" when ",
					UI.FormatAsLink("Powered", "POWER"),
					".\n\nIncreases Duplicant workspeed within light radius."
				});

				// Token: 0x02002DBD RID: 11709
				public class FACADES
				{
					// Token: 0x02002FF1 RID: 12273
					public class DEFAULT_CEILINGLIGHT
					{
						// Token: 0x0400C063 RID: 49251
						public static LocString NAME = UI.FormatAsLink("Ceiling Light", "CEILINGLIGHT");

						// Token: 0x0400C064 RID: 49252
						public static LocString DESC = "It does not go on the floor.";
					}

					// Token: 0x02002FF2 RID: 12274
					public class LABFLASK
					{
						// Token: 0x0400C065 RID: 49253
						public static LocString NAME = UI.FormatAsLink("Lab Flask Ceiling Light", "CEILINGLIGHT");

						// Token: 0x0400C066 RID: 49254
						public static LocString DESC = "For best results, do not fill with liquids.";
					}

					// Token: 0x02002FF3 RID: 12275
					public class FAUXPIPE
					{
						// Token: 0x0400C067 RID: 49255
						public static LocString NAME = UI.FormatAsLink("Faux Pipe Ceiling Light", "CEILINGLIGHT");

						// Token: 0x0400C068 RID: 49256
						public static LocString DESC = "The height of plumbing-inspired interior design.";
					}

					// Token: 0x02002FF4 RID: 12276
					public class MINING
					{
						// Token: 0x0400C069 RID: 49257
						public static LocString NAME = UI.FormatAsLink("Mining Ceiling Light", "CEILINGLIGHT");

						// Token: 0x0400C06A RID: 49258
						public static LocString DESC = "The protective cage makes it the safest choice for underground parties.";
					}

					// Token: 0x02002FF5 RID: 12277
					public class BLOSSOM
					{
						// Token: 0x0400C06B RID: 49259
						public static LocString NAME = UI.FormatAsLink("Blossom Ceiling Light", "CEILINGLIGHT");

						// Token: 0x0400C06C RID: 49260
						public static LocString DESC = "For Duplicants who can't keep real plants alive.";
					}

					// Token: 0x02002FF6 RID: 12278
					public class POLKADOT
					{
						// Token: 0x0400C06D RID: 49261
						public static LocString NAME = UI.FormatAsLink("Polka Dot Ceiling Light", "CEILINGLIGHT");

						// Token: 0x0400C06E RID: 49262
						public static LocString DESC = "A fun lampshade for fun spaces.";
					}
				}
			}

			// Token: 0x02002195 RID: 8597
			public class AIRFILTER
			{
				// Token: 0x04009500 RID: 38144
				public static LocString NAME = UI.FormatAsLink("Deodorizer", "AIRFILTER");

				// Token: 0x04009501 RID: 38145
				public static LocString DESC = "Oh! Citrus scented!";

				// Token: 0x04009502 RID: 38146
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Sand", "SAND"),
					" to filter ",
					UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN"),
					" from the air, reducing ",
					UI.FormatAsLink("Disease", "DISEASE"),
					" spread."
				});
			}

			// Token: 0x02002196 RID: 8598
			public class ARTIFACTANALYSISSTATION
			{
				// Token: 0x04009503 RID: 38147
				public static LocString NAME = UI.FormatAsLink("Artifact Analysis Station", "ARTIFACTANALYSISSTATION");

				// Token: 0x04009504 RID: 38148
				public static LocString DESC = "Discover the mysteries of the past.";

				// Token: 0x04009505 RID: 38149
				public static LocString EFFECT = "Analyses and extracts " + UI.FormatAsLink("Neutronium", "UNOBTANIUM") + " from artifacts of interest.";

				// Token: 0x04009506 RID: 38150
				public static LocString PAYLOAD_DROP_RATE = ITEMS.INDUSTRIAL_PRODUCTS.GENE_SHUFFLER_RECHARGE.NAME + " drop chance: {chance}";

				// Token: 0x04009507 RID: 38151
				public static LocString PAYLOAD_DROP_RATE_TOOLTIP = "This artifact has a {chance} to drop a " + ITEMS.INDUSTRIAL_PRODUCTS.GENE_SHUFFLER_RECHARGE.NAME + " when analyzed at the " + BUILDINGS.PREFABS.ARTIFACTANALYSISSTATION.NAME;
			}

			// Token: 0x02002197 RID: 8599
			public class CANVAS
			{
				// Token: 0x04009508 RID: 38152
				public static LocString NAME = UI.FormatAsLink("Blank Canvas", "CANVAS");

				// Token: 0x04009509 RID: 38153
				public static LocString DESC = "Once built, a Duplicant can paint a blank canvas to produce a decorative painting.";

				// Token: 0x0400950A RID: 38154
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be painted by a Duplicant."
				});

				// Token: 0x0400950B RID: 38155
				public static LocString POORQUALITYNAME = "Crude Painting";

				// Token: 0x0400950C RID: 38156
				public static LocString AVERAGEQUALITYNAME = "Mediocre Painting";

				// Token: 0x0400950D RID: 38157
				public static LocString EXCELLENTQUALITYNAME = "Masterpiece";

				// Token: 0x02002DBE RID: 11710
				public class FACADES
				{
					// Token: 0x02002FF7 RID: 12279
					public class ART_A
					{
						// Token: 0x0400C06F RID: 49263
						public static LocString NAME = UI.FormatAsLink("Doodle Dee Duplicant", "ART_A");

						// Token: 0x0400C070 RID: 49264
						public static LocString DESC = "A sweet, amateurish interpretation of the Duplicant form.";
					}

					// Token: 0x02002FF8 RID: 12280
					public class ART_B
					{
						// Token: 0x0400C071 RID: 49265
						public static LocString NAME = UI.FormatAsLink("Midnight Meal", "ART_B");

						// Token: 0x0400C072 RID: 49266
						public static LocString DESC = "The fast-food equivalent of high art.";
					}

					// Token: 0x02002FF9 RID: 12281
					public class ART_C
					{
						// Token: 0x0400C073 RID: 49267
						public static LocString NAME = UI.FormatAsLink("Dupa Leesa", "ART_C");

						// Token: 0x0400C074 RID: 49268
						public static LocString DESC = "Some viewers swear they've seen it blink.";
					}

					// Token: 0x02002FFA RID: 12282
					public class ART_D
					{
						// Token: 0x0400C075 RID: 49269
						public static LocString NAME = UI.FormatAsLink("The Screech", "ART_D");

						// Token: 0x0400C076 RID: 49270
						public static LocString DESC = "If art could speak, this piece would be far less popular.";
					}

					// Token: 0x02002FFB RID: 12283
					public class ART_E
					{
						// Token: 0x0400C077 RID: 49271
						public static LocString NAME = UI.FormatAsLink("Fridup Kallo", "ART_E");

						// Token: 0x0400C078 RID: 49272
						public static LocString DESC = "Scratching and sniffing the flower yields no scent.";
					}

					// Token: 0x02002FFC RID: 12284
					public class ART_F
					{
						// Token: 0x0400C079 RID: 49273
						public static LocString NAME = UI.FormatAsLink("Moopoleon Bonafarte", "ART_F");

						// Token: 0x0400C07A RID: 49274
						public static LocString DESC = "Portrait of a leader astride their mighty steed.";
					}

					// Token: 0x02002FFD RID: 12285
					public class ART_G
					{
						// Token: 0x0400C07B RID: 49275
						public static LocString NAME = UI.FormatAsLink("Expressive Genius", "ART_G");

						// Token: 0x0400C07C RID: 49276
						public static LocString DESC = "The raw emotion conveyed here often renders viewers speechless.";
					}

					// Token: 0x02002FFE RID: 12286
					public class ART_H
					{
						// Token: 0x0400C07D RID: 49277
						public static LocString NAME = UI.FormatAsLink("The Smooch", "ART_H");

						// Token: 0x0400C07E RID: 49278
						public static LocString DESC = "A candid moment of affection between two organisms.";
					}

					// Token: 0x02002FFF RID: 12287
					public class ART_I
					{
						// Token: 0x0400C07F RID: 49279
						public static LocString NAME = UI.FormatAsLink("Self-Self-Self Portrait", "ART_I");

						// Token: 0x0400C080 RID: 49280
						public static LocString DESC = "A multi-layered exploration of the artist as a subject.";
					}

					// Token: 0x02003000 RID: 12288
					public class ART_J
					{
						// Token: 0x0400C081 RID: 49281
						public static LocString NAME = UI.FormatAsLink("Nikola Devouring His Mush Bar", "ART_J");

						// Token: 0x0400C082 RID: 49282
						public static LocString DESC = "A painting that captures the true nature of hunger.";
					}

					// Token: 0x02003001 RID: 12289
					public class ART_K
					{
						// Token: 0x0400C083 RID: 49283
						public static LocString NAME = UI.FormatAsLink("Sketchy Fungi", "ART_K");

						// Token: 0x0400C084 RID: 49284
						public static LocString DESC = "The perfect painting for dark, dank spaces.";
					}
				}
			}

			// Token: 0x02002198 RID: 8600
			public class CANVASWIDE
			{
				// Token: 0x0400950E RID: 38158
				public static LocString NAME = UI.FormatAsLink("Landscape Canvas", "CANVASWIDE");

				// Token: 0x0400950F RID: 38159
				public static LocString DESC = "Once built, a Duplicant can paint a blank canvas to produce a decorative painting.";

				// Token: 0x04009510 RID: 38160
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Moderately increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be painted by a Duplicant."
				});

				// Token: 0x04009511 RID: 38161
				public static LocString POORQUALITYNAME = "Crude Painting";

				// Token: 0x04009512 RID: 38162
				public static LocString AVERAGEQUALITYNAME = "Mediocre Painting";

				// Token: 0x04009513 RID: 38163
				public static LocString EXCELLENTQUALITYNAME = "Masterpiece";

				// Token: 0x02002DBF RID: 11711
				public class FACADES
				{
					// Token: 0x02003002 RID: 12290
					public class ART_WIDE_A
					{
						// Token: 0x0400C085 RID: 49285
						public static LocString NAME = UI.FormatAsLink("The Twins", "ART_WIDE_A");

						// Token: 0x0400C086 RID: 49286
						public static LocString DESC = "The effort is admirable, though the execution is not.";
					}

					// Token: 0x02003003 RID: 12291
					public class ART_WIDE_B
					{
						// Token: 0x0400C087 RID: 49287
						public static LocString NAME = UI.FormatAsLink("Ground Zero", "ART_WIDE_B");

						// Token: 0x0400C088 RID: 49288
						public static LocString DESC = "Every story has its origin.";
					}

					// Token: 0x02003004 RID: 12292
					public class ART_WIDE_C
					{
						// Token: 0x0400C089 RID: 49289
						public static LocString NAME = UI.FormatAsLink("Still Life with Barbeque and Frost Bun", "ART_WIDE_C");

						// Token: 0x0400C08A RID: 49290
						public static LocString DESC = "Food this good deserves to be immortalized.";
					}

					// Token: 0x02003005 RID: 12293
					public class ART_WIDE_D
					{
						// Token: 0x0400C08B RID: 49291
						public static LocString NAME = UI.FormatAsLink("Composition with Three Colors", "ART_WIDE_D");

						// Token: 0x0400C08C RID: 49292
						public static LocString DESC = "All the other colors in the artist's palette had dried up.";
					}

					// Token: 0x02003006 RID: 12294
					public class ART_WIDE_E
					{
						// Token: 0x0400C08D RID: 49293
						public static LocString NAME = UI.FormatAsLink("Behold, A Fork", "ART_WIDE_E");

						// Token: 0x0400C08E RID: 49294
						public static LocString DESC = "Each tine represents a branch of science.";
					}

					// Token: 0x02003007 RID: 12295
					public class ART_WIDE_F
					{
						// Token: 0x0400C08F RID: 49295
						public static LocString NAME = UI.FormatAsLink("The Astronomer at Home", "ART_WIDE_F");

						// Token: 0x0400C090 RID: 49296
						public static LocString DESC = "Its companion piece, \"The Astronomer at Work\" was lost in a meteor shower.";
					}

					// Token: 0x02003008 RID: 12296
					public class ART_WIDE_G
					{
						// Token: 0x0400C091 RID: 49297
						public static LocString NAME = UI.FormatAsLink("Iconic Iteration", "ART_WIDE_G");

						// Token: 0x0400C092 RID: 49298
						public static LocString DESC = "For the art collector who doesn't mind a bit of repetition.";
					}

					// Token: 0x02003009 RID: 12297
					public class ART_WIDE_H
					{
						// Token: 0x0400C093 RID: 49299
						public static LocString NAME = UI.FormatAsLink("La Belle Meep", "ART_WIDE_H");

						// Token: 0x0400C094 RID: 49300
						public static LocString DESC = "A daring piece, guaranteed to cause a stir.";
					}

					// Token: 0x0200300A RID: 12298
					public class ART_WIDE_I
					{
						// Token: 0x0400C095 RID: 49301
						public static LocString NAME = UI.FormatAsLink("Glorious Vole", "ART_WIDE_I");

						// Token: 0x0400C096 RID: 49302
						public static LocString DESC = "A moody study of the renowned tunneler.";
					}
				}
			}

			// Token: 0x02002199 RID: 8601
			public class CANVASTALL
			{
				// Token: 0x04009514 RID: 38164
				public static LocString NAME = UI.FormatAsLink("Portrait Canvas", "CANVASTALL");

				// Token: 0x04009515 RID: 38165
				public static LocString DESC = "Once built, a Duplicant can paint a blank canvas to produce a decorative painting.";

				// Token: 0x04009516 RID: 38166
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Moderately increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be painted by a Duplicant."
				});

				// Token: 0x04009517 RID: 38167
				public static LocString POORQUALITYNAME = "Crude Painting";

				// Token: 0x04009518 RID: 38168
				public static LocString AVERAGEQUALITYNAME = "Mediocre Painting";

				// Token: 0x04009519 RID: 38169
				public static LocString EXCELLENTQUALITYNAME = "Masterpiece";

				// Token: 0x02002DC0 RID: 11712
				public class FACADES
				{
					// Token: 0x0200300B RID: 12299
					public class ART_TALL_A
					{
						// Token: 0x0400C097 RID: 49303
						public static LocString NAME = UI.FormatAsLink("Ode to O2", "ART_TALL_A");

						// Token: 0x0400C098 RID: 49304
						public static LocString DESC = "Even amateur art is essential to life.";
					}

					// Token: 0x0200300C RID: 12300
					public class ART_TALL_B
					{
						// Token: 0x0400C099 RID: 49305
						public static LocString NAME = UI.FormatAsLink("A Cool Wheeze", "ART_TALL_B");

						// Token: 0x0400C09A RID: 49306
						public static LocString DESC = "It certainly is colorful.";
					}

					// Token: 0x0200300D RID: 12301
					public class ART_TALL_C
					{
						// Token: 0x0400C09B RID: 49307
						public static LocString NAME = UI.FormatAsLink("Luxe Splatter", "ART_TALL_C");

						// Token: 0x0400C09C RID: 49308
						public static LocString DESC = "Chaotic, yet compelling.";
					}

					// Token: 0x0200300E RID: 12302
					public class ART_TALL_D
					{
						// Token: 0x0400C09D RID: 49309
						public static LocString NAME = UI.FormatAsLink("Pickled Meal Lice II", "ART_TALL_D");

						// Token: 0x0400C09E RID: 49310
						public static LocString DESC = "It doesn't have to taste good, it's art.";
					}

					// Token: 0x0200300F RID: 12303
					public class ART_TALL_E
					{
						// Token: 0x0400C09F RID: 49311
						public static LocString NAME = UI.FormatAsLink("Fruit Face", "ART_TALL_E");

						// Token: 0x0400C0A0 RID: 49312
						public static LocString DESC = "Rumour has it that the model was self-conscious about their uneven eyebrows.";
					}

					// Token: 0x02003010 RID: 12304
					public class ART_TALL_F
					{
						// Token: 0x0400C0A1 RID: 49313
						public static LocString NAME = UI.FormatAsLink("Girl with the Blue Scarf", "ART_TALL_F");

						// Token: 0x0400C0A2 RID: 49314
						public static LocString DESC = "The earring is nice too.";
					}

					// Token: 0x02003011 RID: 12305
					public class ART_TALL_G
					{
						// Token: 0x0400C0A3 RID: 49315
						public static LocString NAME = UI.FormatAsLink("A Farewell at Sunrise", "ART_TALL_G");

						// Token: 0x0400C0A4 RID: 49316
						public static LocString DESC = "A poetic ink painting depicting the beginning of an end.";
					}

					// Token: 0x02003012 RID: 12306
					public class ART_TALL_H
					{
						// Token: 0x0400C0A5 RID: 49317
						public static LocString NAME = UI.FormatAsLink("Conqueror of Clusters", "ART_TALL_H");

						// Token: 0x0400C0A6 RID: 49318
						public static LocString DESC = "The type of painting that ambitious Duplicants gravitate to.";
					}

					// Token: 0x02003013 RID: 12307
					public class ART_TALL_I
					{
						// Token: 0x0400C0A7 RID: 49319
						public static LocString NAME = UI.FormatAsLink("Pei Phone", "ART_TALL_I");

						// Token: 0x0400C0A8 RID: 49320
						public static LocString DESC = "When the future calls, Duplicants answer.";
					}
				}
			}

			// Token: 0x0200219A RID: 8602
			public class CO2SCRUBBER
			{
				// Token: 0x0400951A RID: 38170
				public static LocString NAME = UI.FormatAsLink("Carbon Skimmer", "CO2SCRUBBER");

				// Token: 0x0400951B RID: 38171
				public static LocString DESC = "Skimmers remove large amounts of carbon dioxide, but produce no breathable air.";

				// Token: 0x0400951C RID: 38172
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Water", "WATER"),
					" to filter ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					" from the air."
				});
			}

			// Token: 0x0200219B RID: 8603
			public class COMPOST
			{
				// Token: 0x0400951D RID: 38173
				public static LocString NAME = UI.FormatAsLink("Compost", "COMPOST");

				// Token: 0x0400951E RID: 38174
				public static LocString DESC = "Composts safely deal with biological waste, producing fresh dirt.";

				// Token: 0x0400951F RID: 38175
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Reduces ",
					UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
					" and other compostables down into ",
					UI.FormatAsLink("Dirt", "DIRT"),
					"."
				});
			}

			// Token: 0x0200219C RID: 8604
			public class COOKINGSTATION
			{
				// Token: 0x04009520 RID: 38176
				public static LocString NAME = UI.FormatAsLink("Electric Grill", "COOKINGSTATION");

				// Token: 0x04009521 RID: 38177
				public static LocString DESC = "Proper cooking eliminates foodborne disease and produces tasty, stress-relieving meals.";

				// Token: 0x04009522 RID: 38178
				public static LocString EFFECT = "Cooks a wide variety of improved " + UI.FormatAsLink("Foods", "FOOD") + ".\n\nDuplicants will not fabricate items unless recipes are queued.";
			}

			// Token: 0x0200219D RID: 8605
			public class CRYOTANK
			{
				// Token: 0x04009523 RID: 38179
				public static LocString NAME = UI.FormatAsLink("Cryotank 3000", "CRYOTANK");

				// Token: 0x04009524 RID: 38180
				public static LocString DESC = "The tank appears impossibly old, but smells crisp and brand new.\n\nA silhouette just barely visible through the frost of the glass.";

				// Token: 0x04009525 RID: 38181
				public static LocString DEFROSTBUTTON = "Defrost Friend";

				// Token: 0x04009526 RID: 38182
				public static LocString DEFROSTBUTTONTOOLTIP = "A new pal is just an icebreaker away";
			}

			// Token: 0x0200219E RID: 8606
			public class GOURMETCOOKINGSTATION
			{
				// Token: 0x04009527 RID: 38183
				public static LocString NAME = UI.FormatAsLink("Gas Range", "GOURMETCOOKINGSTATION");

				// Token: 0x04009528 RID: 38184
				public static LocString DESC = "Luxury meals increase Duplicants' morale and prevents them from becoming stressed.";

				// Token: 0x04009529 RID: 38185
				public static LocString EFFECT = "Cooks a wide variety of quality " + UI.FormatAsLink("Foods", "FOOD") + ".\n\nDuplicants will not fabricate items unless recipes are queued.";
			}

			// Token: 0x0200219F RID: 8607
			public class DININGTABLE
			{
				// Token: 0x0400952A RID: 38186
				public static LocString NAME = UI.FormatAsLink("Mess Table", "DININGTABLE");

				// Token: 0x0400952B RID: 38187
				public static LocString DESC = "Duplicants prefer to dine at a table, rather than eat off the floor.";

				// Token: 0x0400952C RID: 38188
				public static LocString EFFECT = "Gives one Duplicant a place to eat.\n\nDuplicants will automatically eat at their assigned table when hungry.";
			}

			// Token: 0x020021A0 RID: 8608
			public class DOOR
			{
				// Token: 0x0400952D RID: 38189
				public static LocString NAME = UI.FormatAsLink("Pneumatic Door", "DOOR");

				// Token: 0x0400952E RID: 38190
				public static LocString DESC = "Door controls can be used to prevent Duplicants from entering restricted areas.";

				// Token: 0x0400952F RID: 38191
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Encloses areas without blocking ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" or ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" flow.\n\nWild ",
					UI.FormatAsLink("Critters", "CRITTERS"),
					" cannot pass through doors."
				});

				// Token: 0x04009530 RID: 38192
				public static LocString PRESSURE_SUIT_REQUIRED = UI.FormatAsLink("Atmo Suit", "ATMO_SUIT") + " required {0}";

				// Token: 0x04009531 RID: 38193
				public static LocString PRESSURE_SUIT_NOT_REQUIRED = UI.FormatAsLink("Atmo Suit", "ATMO_SUIT") + " not required {0}";

				// Token: 0x04009532 RID: 38194
				public static LocString ABOVE = "above";

				// Token: 0x04009533 RID: 38195
				public static LocString BELOW = "below";

				// Token: 0x04009534 RID: 38196
				public static LocString LEFT = "on left";

				// Token: 0x04009535 RID: 38197
				public static LocString RIGHT = "on right";

				// Token: 0x04009536 RID: 38198
				public static LocString LOGIC_OPEN = "Open/Close";

				// Token: 0x04009537 RID: 38199
				public static LocString LOGIC_OPEN_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Open";

				// Token: 0x04009538 RID: 38200
				public static LocString LOGIC_OPEN_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Close and lock";

				// Token: 0x02002DC1 RID: 11713
				public static class CONTROL_STATE
				{
					// Token: 0x02003014 RID: 12308
					public class OPEN
					{
						// Token: 0x0400C0A9 RID: 49321
						public static LocString NAME = "Open";

						// Token: 0x0400C0AA RID: 49322
						public static LocString TOOLTIP = "This door will remain open";
					}

					// Token: 0x02003015 RID: 12309
					public class CLOSE
					{
						// Token: 0x0400C0AB RID: 49323
						public static LocString NAME = "Lock";

						// Token: 0x0400C0AC RID: 49324
						public static LocString TOOLTIP = "Nothing may pass through";
					}

					// Token: 0x02003016 RID: 12310
					public class AUTO
					{
						// Token: 0x0400C0AD RID: 49325
						public static LocString NAME = "Auto";

						// Token: 0x0400C0AE RID: 49326
						public static LocString TOOLTIP = "Duplicants open and close this door as needed";
					}
				}
			}

			// Token: 0x020021A1 RID: 8609
			public class ELECTROLYZER
			{
				// Token: 0x04009539 RID: 38201
				public static LocString NAME = UI.FormatAsLink("Electrolyzer", "ELECTROLYZER");

				// Token: 0x0400953A RID: 38202
				public static LocString DESC = "Water goes in one end, life sustaining oxygen comes out the other.";

				// Token: 0x0400953B RID: 38203
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Water", "WATER"),
					" into ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" and ",
					UI.FormatAsLink("Hydrogen", "HYDROGEN"),
					".\n\nBecomes idle when the area reaches maximum pressure capacity."
				});
			}

			// Token: 0x020021A2 RID: 8610
			public class RUSTDEOXIDIZER
			{
				// Token: 0x0400953C RID: 38204
				public static LocString NAME = UI.FormatAsLink("Rust Deoxidizer", "RUSTDEOXIDIZER");

				// Token: 0x0400953D RID: 38205
				public static LocString DESC = "Rust and salt goes in, oxygen comes out.";

				// Token: 0x0400953E RID: 38206
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Rust", "RUST"),
					" into ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" and ",
					UI.FormatAsLink("Chlorine", "CHLORINE"),
					".\n\nBecomes idle when the area reaches maximum pressure capacity."
				});
			}

			// Token: 0x020021A3 RID: 8611
			public class DESALINATOR
			{
				// Token: 0x0400953F RID: 38207
				public static LocString NAME = UI.FormatAsLink("Desalinator", "DESALINATOR");

				// Token: 0x04009540 RID: 38208
				public static LocString DESC = "Salt can be refined into table salt for a mealtime morale boost.";

				// Token: 0x04009541 RID: 38209
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Removes ",
					UI.FormatAsLink("Salt", "SALT"),
					" from ",
					UI.FormatAsLink("Brine", "BRINE"),
					" or ",
					UI.FormatAsLink("Salt Water", "SALTWATER"),
					", producing ",
					UI.FormatAsLink("Water", "WATER"),
					"."
				});
			}

			// Token: 0x020021A4 RID: 8612
			public class POWERTRANSFORMERSMALL
			{
				// Token: 0x04009542 RID: 38210
				public static LocString NAME = UI.FormatAsLink("Power Transformer", "POWERTRANSFORMERSMALL");

				// Token: 0x04009543 RID: 38211
				public static LocString DESC = "Limiting the power drawn by wires prevents them from incurring overload damage.";

				// Token: 0x04009544 RID: 38212
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Limits ",
					UI.FormatAsLink("Power", "POWER"),
					" flowing through the Transformer to 1000 W.\n\nConnect ",
					UI.FormatAsLink("Batteries", "BATTERY"),
					" on the large side to act as a valve and prevent ",
					UI.FormatAsLink("Wires", "WIRE"),
					" from drawing more than 1000 W.\n\nCan be rotated before construction."
				});
			}

			// Token: 0x020021A5 RID: 8613
			public class POWERTRANSFORMER
			{
				// Token: 0x04009545 RID: 38213
				public static LocString NAME = UI.FormatAsLink("Large Power Transformer", "POWERTRANSFORMER");

				// Token: 0x04009546 RID: 38214
				public static LocString DESC = "It's a power transformer, but larger.";

				// Token: 0x04009547 RID: 38215
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Limits ",
					UI.FormatAsLink("Power", "POWER"),
					" flowing through the Transformer to 4 kW.\n\nConnect ",
					UI.FormatAsLink("Batteries", "BATTERY"),
					" on the large side to act as a valve and prevent ",
					UI.FormatAsLink("Wires", "WIRE"),
					" from drawing more than 4 kW.\n\nCan be rotated before construction."
				});
			}

			// Token: 0x020021A6 RID: 8614
			public class FLOORLAMP
			{
				// Token: 0x04009548 RID: 38216
				public static LocString NAME = UI.FormatAsLink("Lamp", "FLOORLAMP");

				// Token: 0x04009549 RID: 38217
				public static LocString DESC = "Any building's light emitting radius can be viewed in the light overlay.";

				// Token: 0x0400954A RID: 38218
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Provides ",
					UI.FormatAsLink("Light", "LIGHT"),
					" when ",
					UI.FormatAsLink("Powered", "POWER"),
					".\n\nIncreases Duplicant workspeed within light radius."
				});

				// Token: 0x02002DC2 RID: 11714
				public class FACADES
				{
				}
			}

			// Token: 0x020021A7 RID: 8615
			public class FLOWERVASE
			{
				// Token: 0x0400954B RID: 38219
				public static LocString NAME = UI.FormatAsLink("Flower Pot", "FLOWERVASE");

				// Token: 0x0400954C RID: 38220
				public static LocString DESC = "Flower pots allow decorative plants to be moved to new locations.";

				// Token: 0x0400954D RID: 38221
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Houses a single ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" when sown with a ",
					UI.FormatAsLink("Seed", "PLANTS"),
					".\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});

				// Token: 0x02002DC3 RID: 11715
				public class FACADES
				{
					// Token: 0x02003017 RID: 12311
					public class DEFAULT_FLOWERVASE
					{
						// Token: 0x0400C0AF RID: 49327
						public static LocString NAME = UI.FormatAsLink("Flower Pot", "FLOWERVASE");

						// Token: 0x0400C0B0 RID: 49328
						public static LocString DESC = "The original container for plants on the move.";
					}

					// Token: 0x02003018 RID: 12312
					public class RETRO_SUNNY
					{
						// Token: 0x0400C0B1 RID: 49329
						public static LocString NAME = UI.FormatAsLink("Sunny Retro Flower Pot", "FLOWERVASE");

						// Token: 0x0400C0B2 RID: 49330
						public static LocString DESC = "A funky yellow flower pot for plants on the move.";
					}

					// Token: 0x02003019 RID: 12313
					public class RETRO_BOLD
					{
						// Token: 0x0400C0B3 RID: 49331
						public static LocString NAME = UI.FormatAsLink("Bold Retro Flower Pot", "FLOWERVASE");

						// Token: 0x0400C0B4 RID: 49332
						public static LocString DESC = "A funky red flower pot for plants on the move.";
					}

					// Token: 0x0200301A RID: 12314
					public class RETRO_BRIGHT
					{
						// Token: 0x0400C0B5 RID: 49333
						public static LocString NAME = UI.FormatAsLink("Bright Retro Flower Pot", "FLOWERVASE");

						// Token: 0x0400C0B6 RID: 49334
						public static LocString DESC = "A funky green flower pot for plants on the move.";
					}

					// Token: 0x0200301B RID: 12315
					public class RETRO_DREAMY
					{
						// Token: 0x0400C0B7 RID: 49335
						public static LocString NAME = UI.FormatAsLink("Dreamy Retro Flower Pot", "FLOWERVASE");

						// Token: 0x0400C0B8 RID: 49336
						public static LocString DESC = "A funky blue flower pot for plants on the move.";
					}

					// Token: 0x0200301C RID: 12316
					public class RETRO_ELEGANT
					{
						// Token: 0x0400C0B9 RID: 49337
						public static LocString NAME = UI.FormatAsLink("Elegant Retro Flower Pot", "FLOWERVASE");

						// Token: 0x0400C0BA RID: 49338
						public static LocString DESC = "A funky white flower pot for plants on the move.";
					}
				}
			}

			// Token: 0x020021A8 RID: 8616
			public class FLOWERVASEWALL
			{
				// Token: 0x0400954E RID: 38222
				public static LocString NAME = UI.FormatAsLink("Wall Pot", "FLOWERVASEWALL");

				// Token: 0x0400954F RID: 38223
				public static LocString DESC = "Placing a plant in a wall pot can add a spot of Decor to otherwise bare walls.";

				// Token: 0x04009550 RID: 38224
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Houses a single ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" when sown with a ",
					UI.FormatAsLink("Seed", "PLANTS"),
					".\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be hung from a wall."
				});
			}

			// Token: 0x020021A9 RID: 8617
			public class FLOWERVASEHANGING
			{
				// Token: 0x04009551 RID: 38225
				public static LocString NAME = UI.FormatAsLink("Hanging Pot", "FLOWERVASEHANGING");

				// Token: 0x04009552 RID: 38226
				public static LocString DESC = "Hanging pots can add some Decor to a room, without blocking buildings on the floor.";

				// Token: 0x04009553 RID: 38227
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Houses a single ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" when sown with a ",
					UI.FormatAsLink("Seed", "PLANTS"),
					".\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be hung from a ceiling."
				});
			}

			// Token: 0x020021AA RID: 8618
			public class FLOWERVASEHANGINGFANCY
			{
				// Token: 0x04009554 RID: 38228
				public static LocString NAME = UI.FormatAsLink("Aero Pot", "FLOWERVASEHANGINGFANCY");

				// Token: 0x04009555 RID: 38229
				public static LocString DESC = "Aero pots can be hung from the ceiling and have extremely high Decor.";

				// Token: 0x04009556 RID: 38230
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Houses a single ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" when sown with a ",
					UI.FormatAsLink("Seed", "PLANTS"),
					".\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be hung from a ceiling."
				});

				// Token: 0x02002DC4 RID: 11716
				public class FACADES
				{
				}
			}

			// Token: 0x020021AB RID: 8619
			public class FLUSHTOILET
			{
				// Token: 0x04009557 RID: 38231
				public static LocString NAME = UI.FormatAsLink("Lavatory", "FLUSHTOILET");

				// Token: 0x04009558 RID: 38232
				public static LocString DESC = "Lavatories transmit fewer germs to Duplicants' skin and require no emptying.";

				// Token: 0x04009559 RID: 38233
				public static LocString EFFECT = "Gives Duplicants a place to relieve themselves.\n\nSpreads very few " + UI.FormatAsLink("Germs", "DISEASE") + ".";
			}

			// Token: 0x020021AC RID: 8620
			public class SHOWER
			{
				// Token: 0x0400955A RID: 38234
				public static LocString NAME = UI.FormatAsLink("Shower", "SHOWER");

				// Token: 0x0400955B RID: 38235
				public static LocString DESC = "Regularly showering will prevent Duplicants spreading germs to the things they touch.";

				// Token: 0x0400955C RID: 38236
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Improves Duplicant ",
					UI.FormatAsLink("Morale", "MORALE"),
					" and removes surface ",
					UI.FormatAsLink("Germs", "DISEASE"),
					"."
				});
			}

			// Token: 0x020021AD RID: 8621
			public class CONDUIT
			{
				// Token: 0x02002DC5 RID: 11717
				public class STATUS_ITEM
				{
					// Token: 0x0400BA6E RID: 47726
					public static LocString NAME = "Marked for Emptying";

					// Token: 0x0400BA6F RID: 47727
					public static LocString TOOLTIP = "Awaiting a " + UI.FormatAsLink("Plumber", "PLUMBER") + " to clear this pipe";
				}
			}

			// Token: 0x020021AE RID: 8622
			public class GAMMARAYOVEN
			{
				// Token: 0x0400955D RID: 38237
				public static LocString NAME = UI.FormatAsLink("Gamma Ray Oven", "GAMMARAYOVEN");

				// Token: 0x0400955E RID: 38238
				public static LocString DESC = "Nuke your food";

				// Token: 0x0400955F RID: 38239
				public static LocString EFFECT = "Cooks a variety of " + UI.FormatAsLink("Foods", "FOOD") + ".\n\nDuplicants will not fabricate items unless recipes are queued.";
			}

			// Token: 0x020021AF RID: 8623
			public class GASCARGOBAY
			{
				// Token: 0x04009560 RID: 38240
				public static LocString NAME = UI.FormatAsLink("Gas Cargo Canister", "GASCARGOBAY");

				// Token: 0x04009561 RID: 38241
				public static LocString DESC = "Duplicants will fill cargo bays with any resources they find during space missions.";

				// Token: 0x04009562 RID: 38242
				public static LocString EFFECT = "Allows Duplicants to store any " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " resources found during space missions.\n\nStored resources become available to the colony upon the rocket's return.";
			}

			// Token: 0x020021B0 RID: 8624
			public class GASCARGOBAYCLUSTER
			{
				// Token: 0x04009563 RID: 38243
				public static LocString NAME = UI.FormatAsLink("Large Gas Cargo Canister", "GASCARGOBAY");

				// Token: 0x04009564 RID: 38244
				public static LocString DESC = "Holds more than a typical gas cargo canister.";

				// Token: 0x04009565 RID: 38245
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to store most of the ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" resources found during space missions.\n\nStored resources become available to the colony upon the rocket's return.\n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x020021B1 RID: 8625
			public class GASCARGOBAYSMALL
			{
				// Token: 0x04009566 RID: 38246
				public static LocString NAME = UI.FormatAsLink("Gas Cargo Canister", "GASCARGOBAYSMALL");

				// Token: 0x04009567 RID: 38247
				public static LocString DESC = "Duplicants fill cargo canisters with any resources they find during space missions.";

				// Token: 0x04009568 RID: 38248
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to store some of the ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" resources found during space missions.\n\nStored resources become available to the colony upon the rocket's return. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x020021B2 RID: 8626
			public class GASCONDUIT
			{
				// Token: 0x04009569 RID: 38249
				public static LocString NAME = UI.FormatAsLink("Gas Pipe", "GASCONDUIT");

				// Token: 0x0400956A RID: 38250
				public static LocString DESC = "Gas pipes are used to connect the inputs and outputs of ventilated buildings.";

				// Token: 0x0400956B RID: 38251
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" between ",
					UI.FormatAsLink("Outputs", "GASPIPING"),
					" and ",
					UI.FormatAsLink("Intakes", "GASPIPING"),
					".\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x020021B3 RID: 8627
			public class GASCONDUITBRIDGE
			{
				// Token: 0x0400956C RID: 38252
				public static LocString NAME = UI.FormatAsLink("Gas Bridge", "GASCONDUITBRIDGE");

				// Token: 0x0400956D RID: 38253
				public static LocString DESC = "Separate pipe systems prevent mingled contents from causing building damage.";

				// Token: 0x0400956E RID: 38254
				public static LocString EFFECT = "Runs one " + UI.FormatAsLink("Gas Pipe", "GASPIPING") + " section over another without joining them.\n\nCan be run through wall and floor tile.";
			}

			// Token: 0x020021B4 RID: 8628
			public class GASCONDUITPREFERENTIALFLOW
			{
				// Token: 0x0400956F RID: 38255
				public static LocString NAME = UI.FormatAsLink("Priority Gas Flow", "GASCONDUITPREFERENTIALFLOW");

				// Token: 0x04009570 RID: 38256
				public static LocString DESC = "Priority flows ensure important buildings are filled first when on a system with other buildings.";

				// Token: 0x04009571 RID: 38257
				public static LocString EFFECT = "Diverts " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " to a secondary input when its primary input overflows.";
			}

			// Token: 0x020021B5 RID: 8629
			public class LIQUIDCONDUITPREFERENTIALFLOW
			{
				// Token: 0x04009572 RID: 38258
				public static LocString NAME = UI.FormatAsLink("Priority Liquid Flow", "LIQUIDCONDUITPREFERENTIALFLOW");

				// Token: 0x04009573 RID: 38259
				public static LocString DESC = "Priority flows ensure important buildings are filled first when on a system with other buildings.";

				// Token: 0x04009574 RID: 38260
				public static LocString EFFECT = "Diverts " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " to a secondary input when its primary input overflows.";
			}

			// Token: 0x020021B6 RID: 8630
			public class GASCONDUITOVERFLOW
			{
				// Token: 0x04009575 RID: 38261
				public static LocString NAME = UI.FormatAsLink("Gas Overflow Valve", "GASCONDUITOVERFLOW");

				// Token: 0x04009576 RID: 38262
				public static LocString DESC = "Overflow valves can be used to prioritize which buildings should receive precious resources first.";

				// Token: 0x04009577 RID: 38263
				public static LocString EFFECT = "Fills a secondary" + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " output only when its primary output is blocked.";
			}

			// Token: 0x020021B7 RID: 8631
			public class LIQUIDCONDUITOVERFLOW
			{
				// Token: 0x04009578 RID: 38264
				public static LocString NAME = UI.FormatAsLink("Liquid Overflow Valve", "LIQUIDCONDUITOVERFLOW");

				// Token: 0x04009579 RID: 38265
				public static LocString DESC = "Overflow valves can be used to prioritize which buildings should receive precious resources first.";

				// Token: 0x0400957A RID: 38266
				public static LocString EFFECT = "Fills a secondary" + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " output only when its primary output is blocked.";
			}

			// Token: 0x020021B8 RID: 8632
			public class LAUNCHPAD
			{
				// Token: 0x0400957B RID: 38267
				public static LocString NAME = UI.FormatAsLink("Rocket Platform", "LAUNCHPAD");

				// Token: 0x0400957C RID: 38268
				public static LocString DESC = "A platform from which rockets can be launched and on which they can land.";

				// Token: 0x0400957D RID: 38269
				public static LocString EFFECT = "Precursor to construction of all other Rocket modules.\n\nAllows Rockets to launch from or land on the host Planetoid.\n\nAutomatically links up to " + BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME + UI.FormatAsLink("s", "MODULARLAUNCHPADPORTSOLID") + " built to either side of the platform.";

				// Token: 0x0400957E RID: 38270
				public static LocString LOGIC_PORT_READY = "Rocket Checklist";

				// Token: 0x0400957F RID: 38271
				public static LocString LOGIC_PORT_READY_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when its rocket is ready for flight";

				// Token: 0x04009580 RID: 38272
				public static LocString LOGIC_PORT_READY_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x04009581 RID: 38273
				public static LocString LOGIC_PORT_LANDED_ROCKET = "Landed Rocket";

				// Token: 0x04009582 RID: 38274
				public static LocString LOGIC_PORT_LANDED_ROCKET_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when its rocket is on the " + BUILDINGS.PREFABS.LAUNCHPAD.NAME;

				// Token: 0x04009583 RID: 38275
				public static LocString LOGIC_PORT_LANDED_ROCKET_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x04009584 RID: 38276
				public static LocString LOGIC_PORT_LAUNCH = "Launch Rocket";

				// Token: 0x04009585 RID: 38277
				public static LocString LOGIC_PORT_LAUNCH_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Launch rocket";

				// Token: 0x04009586 RID: 38278
				public static LocString LOGIC_PORT_LAUNCH_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Cancel launch";
			}

			// Token: 0x020021B9 RID: 8633
			public class GASFILTER
			{
				// Token: 0x04009587 RID: 38279
				public static LocString NAME = UI.FormatAsLink("Gas Filter", "GASFILTER");

				// Token: 0x04009588 RID: 38280
				public static LocString DESC = "All gases are sent into the building's output pipe, except the gas chosen for filtering.";

				// Token: 0x04009589 RID: 38281
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sieves one ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" from the air, sending it into a dedicated ",
					UI.FormatAsLink("Pipe", "GASPIPING"),
					"."
				});

				// Token: 0x0400958A RID: 38282
				public static LocString STATUS_ITEM = "Filters: {0}";

				// Token: 0x0400958B RID: 38283
				public static LocString ELEMENT_NOT_SPECIFIED = "Not Specified";
			}

			// Token: 0x020021BA RID: 8634
			public class SOLIDFILTER
			{
				// Token: 0x0400958C RID: 38284
				public static LocString NAME = UI.FormatAsLink("Solid Filter", "SOLIDFILTER");

				// Token: 0x0400958D RID: 38285
				public static LocString DESC = "All solids are sent into the building's output conveyor, except the solid chosen for filtering.";

				// Token: 0x0400958E RID: 38286
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Separates one ",
					UI.FormatAsLink("Solid Material", "ELEMENTS_SOLID"),
					" from the conveyor, sending it into a dedicated ",
					BUILDINGS.PREFABS.SOLIDCONDUIT.NAME,
					"."
				});

				// Token: 0x0400958F RID: 38287
				public static LocString STATUS_ITEM = "Filters: {0}";

				// Token: 0x04009590 RID: 38288
				public static LocString ELEMENT_NOT_SPECIFIED = "Not Specified";
			}

			// Token: 0x020021BB RID: 8635
			public class GASPERMEABLEMEMBRANE
			{
				// Token: 0x04009591 RID: 38289
				public static LocString NAME = UI.FormatAsLink("Airflow Tile", "GASPERMEABLEMEMBRANE");

				// Token: 0x04009592 RID: 38290
				public static LocString DESC = "Building with airflow tile promotes better gas circulation within a colony.";

				// Token: 0x04009593 RID: 38291
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Used to build the walls and floors of rooms.\n\nBlocks ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" flow without obstructing ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					"."
				});
			}

			// Token: 0x020021BC RID: 8636
			public class DEVPUMPGAS
			{
				// Token: 0x04009594 RID: 38292
				public static LocString NAME = UI.FormatAsLink("Dev Pump Gas", "DEVPUMPGAS");

				// Token: 0x04009595 RID: 38293
				public static LocString DESC = "Piping a pump's output to a building's intake will send gas to that building.";

				// Token: 0x04009596 RID: 38294
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Draws in ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" and runs it through ",
					UI.FormatAsLink("Pipes", "GASPIPING"),
					".\n\nMust be immersed in ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					"."
				});
			}

			// Token: 0x020021BD RID: 8637
			public class GASPUMP
			{
				// Token: 0x04009597 RID: 38295
				public static LocString NAME = UI.FormatAsLink("Gas Pump", "GASPUMP");

				// Token: 0x04009598 RID: 38296
				public static LocString DESC = "Piping a pump's output to a building's intake will send gas to that building.";

				// Token: 0x04009599 RID: 38297
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Draws in ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" and runs it through ",
					UI.FormatAsLink("Pipes", "GASPIPING"),
					".\n\nMust be immersed in ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					"."
				});
			}

			// Token: 0x020021BE RID: 8638
			public class GASMINIPUMP
			{
				// Token: 0x0400959A RID: 38298
				public static LocString NAME = UI.FormatAsLink("Mini Gas Pump", "GASMINIPUMP");

				// Token: 0x0400959B RID: 38299
				public static LocString DESC = "Mini pumps are useful for moving small quantities of gas with minimum power.";

				// Token: 0x0400959C RID: 38300
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Draws in a small amount of ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" and runs it through ",
					UI.FormatAsLink("Pipes", "GASPIPING"),
					".\n\nMust be immersed in ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					"."
				});
			}

			// Token: 0x020021BF RID: 8639
			public class GASVALVE
			{
				// Token: 0x0400959D RID: 38301
				public static LocString NAME = UI.FormatAsLink("Gas Valve", "GASVALVE");

				// Token: 0x0400959E RID: 38302
				public static LocString DESC = "Valves control the amount of gas that moves through pipes, preventing waste.";

				// Token: 0x0400959F RID: 38303
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Controls the ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" volume permitted through ",
					UI.FormatAsLink("Pipes", "GASPIPING"),
					"."
				});
			}

			// Token: 0x020021C0 RID: 8640
			public class GASLOGICVALVE
			{
				// Token: 0x040095A0 RID: 38304
				public static LocString NAME = UI.FormatAsLink("Gas Shutoff", "GASLOGICVALVE");

				// Token: 0x040095A1 RID: 38305
				public static LocString DESC = "Automated piping saves power and time by removing the need for Duplicant input.";

				// Token: 0x040095A2 RID: 38306
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Connects to an ",
					UI.FormatAsLink("Automation", "LOGIC"),
					" grid to automatically turn ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" flow on or off."
				});

				// Token: 0x040095A3 RID: 38307
				public static LocString LOGIC_PORT = "Open/Close";

				// Token: 0x040095A4 RID: 38308
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Allow gas flow";

				// Token: 0x040095A5 RID: 38309
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Prevent gas flow";
			}

			// Token: 0x020021C1 RID: 8641
			public class GASLIMITVALVE
			{
				// Token: 0x040095A6 RID: 38310
				public static LocString NAME = UI.FormatAsLink("Gas Meter Valve", "GASLIMITVALVE");

				// Token: 0x040095A7 RID: 38311
				public static LocString DESC = "Meter Valves let an exact amount of gas pass through before shutting off.";

				// Token: 0x040095A8 RID: 38312
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Connects to an ",
					UI.FormatAsLink("Automation", "LOGIC"),
					" grid to automatically turn ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" flow off when the specified amount has passed through it."
				});

				// Token: 0x040095A9 RID: 38313
				public static LocString LOGIC_PORT_OUTPUT = "Limit Reached";

				// Token: 0x040095AA RID: 38314
				public static LocString OUTPUT_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if limit has been reached";

				// Token: 0x040095AB RID: 38315
				public static LocString OUTPUT_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x040095AC RID: 38316
				public static LocString LOGIC_PORT_RESET = "Reset Meter";

				// Token: 0x040095AD RID: 38317
				public static LocString RESET_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Reset the amount";

				// Token: 0x040095AE RID: 38318
				public static LocString RESET_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Nothing";
			}

			// Token: 0x020021C2 RID: 8642
			public class GASVENT
			{
				// Token: 0x040095AF RID: 38319
				public static LocString NAME = UI.FormatAsLink("Gas Vent", "GASVENT");

				// Token: 0x040095B0 RID: 38320
				public static LocString DESC = "Vents are an exit point for gases from ventilation systems.";

				// Token: 0x040095B1 RID: 38321
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Releases ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" from ",
					UI.FormatAsLink("Gas Pipes", "GASPIPING"),
					"."
				});
			}

			// Token: 0x020021C3 RID: 8643
			public class GASVENTHIGHPRESSURE
			{
				// Token: 0x040095B2 RID: 38322
				public static LocString NAME = UI.FormatAsLink("High Pressure Gas Vent", "GASVENTHIGHPRESSURE");

				// Token: 0x040095B3 RID: 38323
				public static LocString DESC = "High pressure vents can expel gas into more highly pressurized environments.";

				// Token: 0x040095B4 RID: 38324
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Releases ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" from ",
					UI.FormatAsLink("Gas Pipes", "GASPIPING"),
					" into high pressure locations."
				});
			}

			// Token: 0x020021C4 RID: 8644
			public class GASBOTTLER
			{
				// Token: 0x040095B5 RID: 38325
				public static LocString NAME = UI.FormatAsLink("Canister Filler", "GASBOTTLER");

				// Token: 0x040095B6 RID: 38326
				public static LocString DESC = "Canisters allow Duplicants to manually deliver gases from place to place.";

				// Token: 0x040095B7 RID: 38327
				public static LocString EFFECT = "Automatically stores piped " + UI.FormatAsLink("Gases", "ELEMENTS_GAS") + " into canisters for manual transport.";
			}

			// Token: 0x020021C5 RID: 8645
			public class GENERATOR
			{
				// Token: 0x040095B8 RID: 38328
				public static LocString NAME = UI.FormatAsLink("Coal Generator", "GENERATOR");

				// Token: 0x040095B9 RID: 38329
				public static LocString DESC = "Burning coal produces more energy than manual power, but emits heat and exhaust.";

				// Token: 0x040095BA RID: 38330
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Coal", "CARBON"),
					" into electrical ",
					UI.FormatAsLink("Power", "POWER"),
					".\n\nProduces ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					"."
				});

				// Token: 0x040095BB RID: 38331
				public static LocString OVERPRODUCTION = "{Generator} overproduction";
			}

			// Token: 0x020021C6 RID: 8646
			public class GENETICANALYSISSTATION
			{
				// Token: 0x040095BC RID: 38332
				public static LocString NAME = UI.FormatAsLink("Botanical Analyzer", "GENETICANALYSISSTATION");

				// Token: 0x040095BD RID: 38333
				public static LocString DESC = "Would a mutated rose still smell as sweet?";

				// Token: 0x040095BE RID: 38334
				public static LocString EFFECT = "Identifies new " + UI.FormatAsLink("Seed", "PLANTS") + " subspecies.";
			}

			// Token: 0x020021C7 RID: 8647
			public class DEVGENERATOR
			{
				// Token: 0x040095BF RID: 38335
				public static LocString NAME = "Dev Generator";

				// Token: 0x040095C0 RID: 38336
				public static LocString DESC = "Runs on coffee.";

				// Token: 0x040095C1 RID: 38337
				public static LocString EFFECT = "Generates testing power for late nights.";
			}

			// Token: 0x020021C8 RID: 8648
			public class DEVLIFESUPPORT
			{
				// Token: 0x040095C2 RID: 38338
				public static LocString NAME = "Dev Life Support";

				// Token: 0x040095C3 RID: 38339
				public static LocString DESC = "Keeps Duplicants cozy and breathing.";

				// Token: 0x040095C4 RID: 38340
				public static LocString EFFECT = "Generates warm, oxygen-rich air.";
			}

			// Token: 0x020021C9 RID: 8649
			public class DEVRADIATIONGENERATOR
			{
				// Token: 0x040095C5 RID: 38341
				public static LocString NAME = "Dev Radiation Emitter";

				// Token: 0x040095C6 RID: 38342
				public static LocString DESC = "That's some <i>strong</i> coffee.";

				// Token: 0x040095C7 RID: 38343
				public static LocString EFFECT = "Generates on-demand radiation to keep you cozy.";
			}

			// Token: 0x020021CA RID: 8650
			public class GENERICFABRICATOR
			{
				// Token: 0x040095C8 RID: 38344
				public static LocString NAME = UI.FormatAsLink("Omniprinter", "GENERICFABRICATOR");

				// Token: 0x040095C9 RID: 38345
				public static LocString DESC = "Omniprinters are incapable of printing organic matter.";

				// Token: 0x040095CA RID: 38346
				public static LocString EFFECT = "Converts " + UI.FormatAsLink("Raw Mineral", "RAWMINERAL") + " into unique materials and objects.";
			}

			// Token: 0x020021CB RID: 8651
			public class GEOTUNER
			{
				// Token: 0x040095CB RID: 38347
				public static LocString NAME = UI.FormatAsLink("Geotuner", "GEOTUNER");

				// Token: 0x040095CC RID: 38348
				public static LocString DESC = "The targeted geyser receives stored amplification data when it is erupting.";

				// Token: 0x040095CD RID: 38349
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Increases the ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" and output of an analyzed ",
					UI.FormatAsLink("Geyser", "GEYSERS"),
					".\n\nMultiple Geotuners can be directed at a single ",
					UI.FormatAsLink("Geyser", "GEYSERS"),
					" anywhere on an asteroid."
				});

				// Token: 0x040095CE RID: 38350
				public static LocString LOGIC_PORT = "Geyser Eruption Monitor";

				// Token: 0x040095CF RID: 38351
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when geyser is erupting";

				// Token: 0x040095D0 RID: 38352
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x020021CC RID: 8652
			public class GRAVE
			{
				// Token: 0x040095D1 RID: 38353
				public static LocString NAME = UI.FormatAsLink("Tasteful Memorial", "GRAVE");

				// Token: 0x040095D2 RID: 38354
				public static LocString DESC = "Burying dead Duplicants reduces health hazards and stress on the colony.";

				// Token: 0x040095D3 RID: 38355
				public static LocString EFFECT = "Provides a final resting place for deceased Duplicants.\n\nLiving Duplicants will automatically place an unburied corpse inside.";
			}

			// Token: 0x020021CD RID: 8653
			public class HEADQUARTERS
			{
				// Token: 0x040095D4 RID: 38356
				public static LocString NAME = UI.FormatAsLink("Printing Pod", "HEADQUARTERS");

				// Token: 0x040095D5 RID: 38357
				public static LocString DESC = "New Duplicants come out here, but thank goodness, they never go back in.";

				// Token: 0x040095D6 RID: 38358
				public static LocString EFFECT = "An exceptionally advanced bioprinter of unknown origin.\n\nIt periodically produces new Duplicants or care packages containing resources.";
			}

			// Token: 0x020021CE RID: 8654
			public class HYDROGENGENERATOR
			{
				// Token: 0x040095D7 RID: 38359
				public static LocString NAME = UI.FormatAsLink("Hydrogen Generator", "HYDROGENGENERATOR");

				// Token: 0x040095D8 RID: 38360
				public static LocString DESC = "Hydrogen generators are extremely efficient, emitting next to no waste.";

				// Token: 0x040095D9 RID: 38361
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Hydrogen", "HYDROGEN"),
					" into electrical ",
					UI.FormatAsLink("Power", "POWER"),
					"."
				});
			}

			// Token: 0x020021CF RID: 8655
			public class METHANEGENERATOR
			{
				// Token: 0x040095DA RID: 38362
				public static LocString NAME = UI.FormatAsLink("Natural Gas Generator", "METHANEGENERATOR");

				// Token: 0x040095DB RID: 38363
				public static LocString DESC = "Natural gas generators leak polluted water and are best built above a waste reservoir.";

				// Token: 0x040095DC RID: 38364
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Natural Gas", "METHANE"),
					" into electrical ",
					UI.FormatAsLink("Power", "POWER"),
					".\n\nProduces ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					" and ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					"."
				});
			}

			// Token: 0x020021D0 RID: 8656
			public class NUCLEARREACTOR
			{
				// Token: 0x040095DD RID: 38365
				public static LocString NAME = UI.FormatAsLink("Research Reactor", "NUCLEARREACTOR");

				// Token: 0x040095DE RID: 38366
				public static LocString DESC = "Radbolt generators and reflectors make radiation useable by other buildings.";

				// Token: 0x040095DF RID: 38367
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM"),
					" to produce ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					" for Radbolt production.\n\nGenerates a massive amount of ",
					UI.FormatAsLink("Heat", "HEAT"),
					". Overheating will result in an explosive meltdown."
				});

				// Token: 0x040095E0 RID: 38368
				public static LocString LOGIC_PORT = "Fuel Delivery Control";

				// Token: 0x040095E1 RID: 38369
				public static LocString INPUT_PORT_ACTIVE = "Fuel Delivery Enabled";

				// Token: 0x040095E2 RID: 38370
				public static LocString INPUT_PORT_INACTIVE = "Fuel Delivery Disabled";
			}

			// Token: 0x020021D1 RID: 8657
			public class WOODGASGENERATOR
			{
				// Token: 0x040095E3 RID: 38371
				public static LocString NAME = UI.FormatAsLink("Wood Burner", "WOODGASGENERATOR");

				// Token: 0x040095E4 RID: 38372
				public static LocString DESC = "Wood burners are small and easy to maintain, but produce a fair amount of heat.";

				// Token: 0x040095E5 RID: 38373
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Burns ",
					UI.FormatAsLink("Lumber", "WOOD"),
					" to produce electrical ",
					UI.FormatAsLink("Power", "POWER"),
					".\n\nProduces ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					" and ",
					UI.FormatAsLink("Heat", "HEAT"),
					"."
				});
			}

			// Token: 0x020021D2 RID: 8658
			public class PETROLEUMGENERATOR
			{
				// Token: 0x040095E6 RID: 38374
				public static LocString NAME = UI.FormatAsLink("Petroleum Generator", "PETROLEUMGENERATOR");

				// Token: 0x040095E7 RID: 38375
				public static LocString DESC = "Petroleum generators have a high energy output but produce a great deal of waste.";

				// Token: 0x040095E8 RID: 38376
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts either ",
					UI.FormatAsLink("Petroleum", "PETROLEUM"),
					" or ",
					UI.FormatAsLink("Ethanol", "ETHANOL"),
					" into electrical ",
					UI.FormatAsLink("Power", "POWER"),
					".\n\nProduces ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					" and ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					"."
				});
			}

			// Token: 0x020021D3 RID: 8659
			public class HYDROPONICFARM
			{
				// Token: 0x040095E9 RID: 38377
				public static LocString NAME = UI.FormatAsLink("Hydroponic Farm", "HYDROPONICFARM");

				// Token: 0x040095EA RID: 38378
				public static LocString DESC = "Hydroponic farms reduce Duplicant traffic by automating irrigating crops.";

				// Token: 0x040095EB RID: 38379
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Grows one ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" from a ",
					UI.FormatAsLink("Seed", "PLANTS"),
					".\n\nCan be used as floor tile and rotated before construction.\n\nMust be irrigated through ",
					UI.FormatAsLink("Liquid Piping", "LIQUIDPIPING"),
					"."
				});
			}

			// Token: 0x020021D4 RID: 8660
			public class INSULATEDGASCONDUIT
			{
				// Token: 0x040095EC RID: 38380
				public static LocString NAME = UI.FormatAsLink("Insulated Gas Pipe", "INSULATEDGASCONDUIT");

				// Token: 0x040095ED RID: 38381
				public static LocString DESC = "Pipe insulation prevents gas contents from significantly changing temperature in transit.";

				// Token: 0x040095EE RID: 38382
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" with minimal change in ",
					UI.FormatAsLink("Temperature", "HEAT"),
					".\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x020021D5 RID: 8661
			public class GASCONDUITRADIANT
			{
				// Token: 0x040095EF RID: 38383
				public static LocString NAME = UI.FormatAsLink("Radiant Gas Pipe", "GASCONDUITRADIANT");

				// Token: 0x040095F0 RID: 38384
				public static LocString DESC = "Radiant pipes pumping cold gas can be run through hot areas to help cool them down.";

				// Token: 0x040095F1 RID: 38385
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					", allowing extreme ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" exchange with the surrounding environment.\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x020021D6 RID: 8662
			public class INSULATEDLIQUIDCONDUIT
			{
				// Token: 0x040095F2 RID: 38386
				public static LocString NAME = UI.FormatAsLink("Insulated Liquid Pipe", "INSULATEDLIQUIDCONDUIT");

				// Token: 0x040095F3 RID: 38387
				public static LocString DESC = "Pipe insulation prevents liquid contents from significantly changing temperature in transit.";

				// Token: 0x040095F4 RID: 38388
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" with minimal change in ",
					UI.FormatAsLink("Temperature", "HEAT"),
					".\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x020021D7 RID: 8663
			public class LIQUIDCONDUITRADIANT
			{
				// Token: 0x040095F5 RID: 38389
				public static LocString NAME = UI.FormatAsLink("Radiant Liquid Pipe", "LIQUIDCONDUITRADIANT");

				// Token: 0x040095F6 RID: 38390
				public static LocString DESC = "Radiant pipes pumping cold liquid can be run through hot areas to help cool them down.";

				// Token: 0x040095F7 RID: 38391
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					", allowing extreme ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" exchange with the surrounding environment.\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x020021D8 RID: 8664
			public class CONTACTCONDUCTIVEPIPEBRIDGE
			{
				// Token: 0x040095F8 RID: 38392
				public static LocString NAME = "Conduction Panel";

				// Token: 0x040095F9 RID: 38393
				public static LocString DESC = "It can transfer heat effectively even if no liquid is passing through.";

				// Token: 0x040095FA RID: 38394
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					", allowing extreme ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" exchange with overlapping buildings.\n\nCan function in a vacuum.\n\nCan be run through wall and floor tiles."
				});
			}

			// Token: 0x020021D9 RID: 8665
			public class INSULATEDWIRE
			{
				// Token: 0x040095FB RID: 38395
				public static LocString NAME = UI.FormatAsLink("Insulated Wire", "INSULATEDWIRE");

				// Token: 0x040095FC RID: 38396
				public static LocString DESC = "This stuff won't go melting if things get heated.";

				// Token: 0x040095FD RID: 38397
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Connects buildings to ",
					UI.FormatAsLink("Power", "POWER"),
					" sources in extreme ",
					UI.FormatAsLink("Heat", "HEAT"),
					".\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x020021DA RID: 8666
			public class INSULATIONTILE
			{
				// Token: 0x040095FE RID: 38398
				public static LocString NAME = UI.FormatAsLink("Insulated Tile", "INSULATIONTILE");

				// Token: 0x040095FF RID: 38399
				public static LocString DESC = "The low thermal conductivity of insulated tiles slows any heat passing through them.";

				// Token: 0x04009600 RID: 38400
				public static LocString EFFECT = "Used to build the walls and floors of rooms.\n\nReduces " + UI.FormatAsLink("Heat", "HEAT") + " transfer between walls, retaining ambient heat in an area.";
			}

			// Token: 0x020021DB RID: 8667
			public class EXTERIORWALL
			{
				// Token: 0x04009601 RID: 38401
				public static LocString NAME = UI.FormatAsLink("Drywall", "EXTERIORWALL");

				// Token: 0x04009602 RID: 38402
				public static LocString DESC = "Drywall can be used in conjunction with tiles to build airtight rooms on the surface.";

				// Token: 0x04009603 RID: 38403
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Prevents ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" and ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" loss in space.\n\nBuilds an insulating backwall behind buildings."
				});

				// Token: 0x02002DC6 RID: 11718
				public class FACADES
				{
					// Token: 0x0200301D RID: 12317
					public class DEFAULT_EXTERIORWALL
					{
						// Token: 0x0400C0BB RID: 49339
						public static LocString NAME = UI.FormatAsLink("Drywall", "EXTERIORWALL");

						// Token: 0x0400C0BC RID: 49340
						public static LocString DESC = "It gets the job done.";
					}

					// Token: 0x0200301E RID: 12318
					public class BALM_LILY
					{
						// Token: 0x0400C0BD RID: 49341
						public static LocString NAME = UI.FormatAsLink("Balm Lily Print", "EXTERIORWALL");

						// Token: 0x0400C0BE RID: 49342
						public static LocString DESC = "A mellow floral wallpaper.";
					}

					// Token: 0x0200301F RID: 12319
					public class CLOUDS
					{
						// Token: 0x0400C0BF RID: 49343
						public static LocString NAME = UI.FormatAsLink("Cloud Print", "EXTERIORWALL");

						// Token: 0x0400C0C0 RID: 49344
						public static LocString DESC = "A soft, fluffy wallpaper.";
					}

					// Token: 0x02003020 RID: 12320
					public class MUSHBAR
					{
						// Token: 0x0400C0C1 RID: 49345
						public static LocString NAME = UI.FormatAsLink("Mush Bar Print", "EXTERIORWALL");

						// Token: 0x0400C0C2 RID: 49346
						public static LocString DESC = "A gag-inducing wallpaper.";
					}

					// Token: 0x02003021 RID: 12321
					public class PLAID
					{
						// Token: 0x0400C0C3 RID: 49347
						public static LocString NAME = UI.FormatAsLink("Aqua Plaid Print", "EXTERIORWALL");

						// Token: 0x0400C0C4 RID: 49348
						public static LocString DESC = "A cozy flannel wallpaper.";
					}

					// Token: 0x02003022 RID: 12322
					public class RAIN
					{
						// Token: 0x0400C0C5 RID: 49349
						public static LocString NAME = UI.FormatAsLink("Rainy Print", "EXTERIORWALL");

						// Token: 0x0400C0C6 RID: 49350
						public static LocString DESC = "A precipitation-themed wallpaper.";
					}

					// Token: 0x02003023 RID: 12323
					public class AQUATICMOSAIC
					{
						// Token: 0x0400C0C7 RID: 49351
						public static LocString NAME = UI.FormatAsLink("Aquatic Mosaic", "EXTERIORWALL");

						// Token: 0x0400C0C8 RID: 49352
						public static LocString DESC = "A multi-hued blue wallpaper.";
					}

					// Token: 0x02003024 RID: 12324
					public class RAINBOW
					{
						// Token: 0x0400C0C9 RID: 49353
						public static LocString NAME = UI.FormatAsLink("Rainbow Stripe", "EXTERIORWALL");

						// Token: 0x0400C0CA RID: 49354
						public static LocString DESC = "A wallpaper with <i>all</i> the colors.";
					}

					// Token: 0x02003025 RID: 12325
					public class SNOW
					{
						// Token: 0x0400C0CB RID: 49355
						public static LocString NAME = UI.FormatAsLink("Snowflake Print", "EXTERIORWALL");

						// Token: 0x0400C0CC RID: 49356
						public static LocString DESC = "A wallpaper as unique as my colony.";
					}

					// Token: 0x02003026 RID: 12326
					public class SUN
					{
						// Token: 0x0400C0CD RID: 49357
						public static LocString NAME = UI.FormatAsLink("Sunshine Print", "EXTERIORWALL");

						// Token: 0x0400C0CE RID: 49358
						public static LocString DESC = "A UV-free wallpaper.";
					}

					// Token: 0x02003027 RID: 12327
					public class COFFEE
					{
						// Token: 0x0400C0CF RID: 49359
						public static LocString NAME = UI.FormatAsLink("Cafe Print", "EXTERIORWALL");

						// Token: 0x0400C0D0 RID: 49360
						public static LocString DESC = "A caffeine-themed wallpaper.";
					}

					// Token: 0x02003028 RID: 12328
					public class PASTELPOLKA
					{
						// Token: 0x0400C0D1 RID: 49361
						public static LocString NAME = UI.FormatAsLink("Pastel Polka Print", "EXTERIORWALL");

						// Token: 0x0400C0D2 RID: 49362
						public static LocString DESC = "A soothing, dotted wallpaper.";
					}

					// Token: 0x02003029 RID: 12329
					public class PASTELBLUE
					{
						// Token: 0x0400C0D3 RID: 49363
						public static LocString NAME = UI.FormatAsLink("Pastel Blue", "EXTERIORWALL");

						// Token: 0x0400C0D4 RID: 49364
						public static LocString DESC = "A soothing blue wallpaper.";
					}

					// Token: 0x0200302A RID: 12330
					public class PASTELGREEN
					{
						// Token: 0x0400C0D5 RID: 49365
						public static LocString NAME = UI.FormatAsLink("Pastel Green", "EXTERIORWALL");

						// Token: 0x0400C0D6 RID: 49366
						public static LocString DESC = "A soothing green wallpaper.";
					}

					// Token: 0x0200302B RID: 12331
					public class PASTELPINK
					{
						// Token: 0x0400C0D7 RID: 49367
						public static LocString NAME = UI.FormatAsLink("Pastel Pink", "EXTERIORWALL");

						// Token: 0x0400C0D8 RID: 49368
						public static LocString DESC = "A soothing pink wallpaper.";
					}

					// Token: 0x0200302C RID: 12332
					public class PASTELPURPLE
					{
						// Token: 0x0400C0D9 RID: 49369
						public static LocString NAME = UI.FormatAsLink("Pastel Purple", "EXTERIORWALL");

						// Token: 0x0400C0DA RID: 49370
						public static LocString DESC = "A soothing purple wallpaper.";
					}

					// Token: 0x0200302D RID: 12333
					public class PASTELYELLOW
					{
						// Token: 0x0400C0DB RID: 49371
						public static LocString NAME = UI.FormatAsLink("Pastel Yellow", "EXTERIORWALL");

						// Token: 0x0400C0DC RID: 49372
						public static LocString DESC = "A soothing yellow wallpaper.";
					}

					// Token: 0x0200302E RID: 12334
					public class BASIC_WHITE
					{
						// Token: 0x0400C0DD RID: 49373
						public static LocString NAME = UI.FormatAsLink("Fresh White", "EXTERIORWALL");

						// Token: 0x0400C0DE RID: 49374
						public static LocString DESC = "It's just so fresh and so clean.";
					}

					// Token: 0x0200302F RID: 12335
					public class DIAGONAL_RED_DEEP_WHITE
					{
						// Token: 0x0400C0DF RID: 49375
						public static LocString NAME = UI.FormatAsLink("Magma Diagonal", "EXTERIORWALL");

						// Token: 0x0400C0E0 RID: 49376
						public static LocString DESC = "A red wallpaper with a diagonal stripe.";
					}

					// Token: 0x02003030 RID: 12336
					public class DIAGONAL_ORANGE_SATSUMA_WHITE
					{
						// Token: 0x0400C0E1 RID: 49377
						public static LocString NAME = UI.FormatAsLink("Bright Diagonal", "EXTERIORWALL");

						// Token: 0x0400C0E2 RID: 49378
						public static LocString DESC = "An orange wallpaper with a diagonal stripe.";
					}

					// Token: 0x02003031 RID: 12337
					public class DIAGONAL_YELLOW_LEMON_WHITE
					{
						// Token: 0x0400C0E3 RID: 49379
						public static LocString NAME = UI.FormatAsLink("Yellowcake Diagonal", "EXTERIORWALL");

						// Token: 0x0400C0E4 RID: 49380
						public static LocString DESC = "A radiation-free wallpaper with a diagonal stripe.";
					}

					// Token: 0x02003032 RID: 12338
					public class DIAGONAL_GREEN_KELLY_WHITE
					{
						// Token: 0x0400C0E5 RID: 49381
						public static LocString NAME = UI.FormatAsLink("Algae Diagonal", "EXTERIORWALL");

						// Token: 0x0400C0E6 RID: 49382
						public static LocString DESC = "A slippery wallpaper with a diagonal stripe.";
					}

					// Token: 0x02003033 RID: 12339
					public class DIAGONAL_BLUE_COBALT_WHITE
					{
						// Token: 0x0400C0E7 RID: 49383
						public static LocString NAME = UI.FormatAsLink("H2O Diagonal", "EXTERIORWALL");

						// Token: 0x0400C0E8 RID: 49384
						public static LocString DESC = "A damp wallpaper with a diagonal stripe.";
					}

					// Token: 0x02003034 RID: 12340
					public class DIAGONAL_PINK_FLAMINGO_WHITE
					{
						// Token: 0x0400C0E9 RID: 49385
						public static LocString NAME = UI.FormatAsLink("Petal Diagonal", "EXTERIORWALL");

						// Token: 0x0400C0EA RID: 49386
						public static LocString DESC = "A pink wallpaper with a diagonal stripe.";
					}

					// Token: 0x02003035 RID: 12341
					public class DIAGONAL_GREY_CHARCOAL_WHITE
					{
						// Token: 0x0400C0EB RID: 49387
						public static LocString NAME = UI.FormatAsLink("Charcoal Diagonal", "EXTERIORWALL");

						// Token: 0x0400C0EC RID: 49388
						public static LocString DESC = "A sleek wallpaper with a diagonal stripe.";
					}

					// Token: 0x02003036 RID: 12342
					public class CIRCLE_RED_DEEP_WHITE
					{
						// Token: 0x0400C0ED RID: 49389
						public static LocString NAME = UI.FormatAsLink("Magma Wedge", "EXTERIORWALL");

						// Token: 0x0400C0EE RID: 49390
						public static LocString DESC = "It can be arranged into giant red polka dots.";
					}

					// Token: 0x02003037 RID: 12343
					public class CIRCLE_ORANGE_SATSUMA_WHITE
					{
						// Token: 0x0400C0EF RID: 49391
						public static LocString NAME = UI.FormatAsLink("Bright Wedge", "EXTERIORWALL");

						// Token: 0x0400C0F0 RID: 49392
						public static LocString DESC = "It can be arranged into giant orange polka dots.";
					}

					// Token: 0x02003038 RID: 12344
					public class CIRCLE_YELLOW_LEMON_WHITE
					{
						// Token: 0x0400C0F1 RID: 49393
						public static LocString NAME = UI.FormatAsLink("Yellowcake Wedge", "EXTERIORWALL");

						// Token: 0x0400C0F2 RID: 49394
						public static LocString DESC = "A radiation-free pattern that can be arranged into giant polka dots.";
					}

					// Token: 0x02003039 RID: 12345
					public class CIRCLE_GREEN_KELLY_WHITE
					{
						// Token: 0x0400C0F3 RID: 49395
						public static LocString NAME = UI.FormatAsLink("Algae Wedge", "EXTERIORWALL");

						// Token: 0x0400C0F4 RID: 49396
						public static LocString DESC = "It can be arranged into giant green polka dots.";
					}

					// Token: 0x0200303A RID: 12346
					public class CIRCLE_BLUE_COBALT_WHITE
					{
						// Token: 0x0400C0F5 RID: 49397
						public static LocString NAME = UI.FormatAsLink("H2O Wedge", "EXTERIORWALL");

						// Token: 0x0400C0F6 RID: 49398
						public static LocString DESC = "It can be arranged into giant blue polka dots.";
					}

					// Token: 0x0200303B RID: 12347
					public class CIRCLE_PINK_FLAMINGO_WHITE
					{
						// Token: 0x0400C0F7 RID: 49399
						public static LocString NAME = UI.FormatAsLink("Petal Wedge", "EXTERIORWALL");

						// Token: 0x0400C0F8 RID: 49400
						public static LocString DESC = "It can be arranged into giant pink polka dots.";
					}

					// Token: 0x0200303C RID: 12348
					public class CIRCLE_GREY_CHARCOAL_WHITE
					{
						// Token: 0x0400C0F9 RID: 49401
						public static LocString NAME = UI.FormatAsLink("Charcoal Wedge", "EXTERIORWALL");

						// Token: 0x0400C0FA RID: 49402
						public static LocString DESC = "It can be arranged into giant shadowy polka dots.";
					}
				}
			}

			// Token: 0x020021DC RID: 8668
			public class FARMTILE
			{
				// Token: 0x04009604 RID: 38404
				public static LocString NAME = UI.FormatAsLink("Farm Tile", "FARMTILE");

				// Token: 0x04009605 RID: 38405
				public static LocString DESC = "Duplicants can deliver fertilizer and liquids to farm tiles, accelerating plant growth.";

				// Token: 0x04009606 RID: 38406
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Grows one ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" from a ",
					UI.FormatAsLink("Seed", "PLANTS"),
					".\n\nCan be used as floor tile and rotated before construction."
				});
			}

			// Token: 0x020021DD RID: 8669
			public class LADDER
			{
				// Token: 0x04009607 RID: 38407
				public static LocString NAME = UI.FormatAsLink("Ladder", "LADDER");

				// Token: 0x04009608 RID: 38408
				public static LocString DESC = "(That means they climb it.)";

				// Token: 0x04009609 RID: 38409
				public static LocString EFFECT = "Enables vertical mobility for Duplicants.";
			}

			// Token: 0x020021DE RID: 8670
			public class LADDERFAST
			{
				// Token: 0x0400960A RID: 38410
				public static LocString NAME = UI.FormatAsLink("Plastic Ladder", "LADDERFAST");

				// Token: 0x0400960B RID: 38411
				public static LocString DESC = "Plastic ladders are mildly antiseptic and can help limit the spread of germs in a colony.";

				// Token: 0x0400960C RID: 38412
				public static LocString EFFECT = "Increases Duplicant climbing speed.";
			}

			// Token: 0x020021DF RID: 8671
			public class LIQUIDCONDUIT
			{
				// Token: 0x0400960D RID: 38413
				public static LocString NAME = UI.FormatAsLink("Liquid Pipe", "LIQUIDCONDUIT");

				// Token: 0x0400960E RID: 38414
				public static LocString DESC = "Liquid pipes are used to connect the inputs and outputs of plumbed buildings.";

				// Token: 0x0400960F RID: 38415
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" between ",
					UI.FormatAsLink("Outputs", "LIQUIDPIPING"),
					" and ",
					UI.FormatAsLink("Intakes", "LIQUIDPIPING"),
					".\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x020021E0 RID: 8672
			public class LIQUIDCONDUITBRIDGE
			{
				// Token: 0x04009610 RID: 38416
				public static LocString NAME = UI.FormatAsLink("Liquid Bridge", "LIQUIDCONDUITBRIDGE");

				// Token: 0x04009611 RID: 38417
				public static LocString DESC = "Separate pipe systems help prevent building damage caused by mingled pipe contents.";

				// Token: 0x04009612 RID: 38418
				public static LocString EFFECT = "Runs one " + UI.FormatAsLink("Liquid Pipe", "LIQUIDPIPING") + " section over another without joining them.\n\nCan be run through wall and floor tile.";
			}

			// Token: 0x020021E1 RID: 8673
			public class ICECOOLEDFAN
			{
				// Token: 0x04009613 RID: 38419
				public static LocString NAME = UI.FormatAsLink("Ice-E Fan", "ICECOOLEDFAN");

				// Token: 0x04009614 RID: 38420
				public static LocString DESC = "A Duplicant can work an Ice-E fan to temporarily cool small areas as needed.";

				// Token: 0x04009615 RID: 38421
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Ice", "ICEORE"),
					" to dissipate a small amount of the ",
					UI.FormatAsLink("Heat", "HEAT"),
					"."
				});
			}

			// Token: 0x020021E2 RID: 8674
			public class ICEMACHINE
			{
				// Token: 0x04009616 RID: 38422
				public static LocString NAME = UI.FormatAsLink("Ice Maker", "ICEMACHINE");

				// Token: 0x04009617 RID: 38423
				public static LocString DESC = "Ice makers can be used as a small renewable source of ice.";

				// Token: 0x04009618 RID: 38424
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Water", "WATER"),
					" into ",
					UI.FormatAsLink("Ice", "ICE"),
					"."
				});
			}

			// Token: 0x020021E3 RID: 8675
			public class LIQUIDCOOLEDFAN
			{
				// Token: 0x04009619 RID: 38425
				public static LocString NAME = UI.FormatAsLink("Hydrofan", "LIQUIDCOOLEDFAN");

				// Token: 0x0400961A RID: 38426
				public static LocString DESC = "A Duplicant can work a hydrofan to temporarily cool small areas as needed.";

				// Token: 0x0400961B RID: 38427
				public static LocString EFFECT = "Dissipates a small amount of the " + UI.FormatAsLink("Heat", "HEAT") + ".";
			}

			// Token: 0x020021E4 RID: 8676
			public class CREATURETRAP
			{
				// Token: 0x0400961C RID: 38428
				public static LocString NAME = UI.FormatAsLink("Critter Trap", "CREATURETRAP");

				// Token: 0x0400961D RID: 38429
				public static LocString DESC = "Critter traps cannot catch swimming or flying critters.";

				// Token: 0x0400961E RID: 38430
				public static LocString EFFECT = "Captures a living " + UI.FormatAsLink("Critter", "CRITTERS") + " for transport.\n\nSingle use.";
			}

			// Token: 0x020021E5 RID: 8677
			public class CREATUREDELIVERYPOINT
			{
				// Token: 0x0400961F RID: 38431
				public static LocString NAME = UI.FormatAsLink("Critter Drop-Off", "CREATUREDELIVERYPOINT");

				// Token: 0x04009620 RID: 38432
				public static LocString DESC = "Duplicants automatically bring captured critters to these relocation points for release.";

				// Token: 0x04009621 RID: 38433
				public static LocString EFFECT = "Releases trapped " + UI.FormatAsLink("Critters", "CRITTERS") + " back into the world.\n\nCan be used multiple times.";
			}

			// Token: 0x020021E6 RID: 8678
			public class LIQUIDFILTER
			{
				// Token: 0x04009622 RID: 38434
				public static LocString NAME = UI.FormatAsLink("Liquid Filter", "LIQUIDFILTER");

				// Token: 0x04009623 RID: 38435
				public static LocString DESC = "All liquids are sent into the building's output pipe, except the liquid chosen for filtering.";

				// Token: 0x04009624 RID: 38436
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sieves one ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" out of a mix, sending it into a dedicated ",
					UI.FormatAsLink("Filtered Output Pipe", "LIQUIDPIPING"),
					".\n\nCan only filter one liquid type at a time."
				});
			}

			// Token: 0x020021E7 RID: 8679
			public class DEVPUMPLIQUID
			{
				// Token: 0x04009625 RID: 38437
				public static LocString NAME = UI.FormatAsLink("Dev Pump Liquid", "DEVPUMPLIQUID");

				// Token: 0x04009626 RID: 38438
				public static LocString DESC = "Piping a pump's output to a building's intake will send liquid to that building.";

				// Token: 0x04009627 RID: 38439
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Draws in ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" and runs it through ",
					UI.FormatAsLink("Pipes", "LIQUIDPIPING"),
					".\n\nMust be submerged in ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					"."
				});
			}

			// Token: 0x020021E8 RID: 8680
			public class LIQUIDPUMP
			{
				// Token: 0x04009628 RID: 38440
				public static LocString NAME = UI.FormatAsLink("Liquid Pump", "LIQUIDPUMP");

				// Token: 0x04009629 RID: 38441
				public static LocString DESC = "Piping a pump's output to a building's intake will send liquid to that building.";

				// Token: 0x0400962A RID: 38442
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Draws in ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" and runs it through ",
					UI.FormatAsLink("Pipes", "LIQUIDPIPING"),
					".\n\nMust be submerged in ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					"."
				});
			}

			// Token: 0x020021E9 RID: 8681
			public class LIQUIDMINIPUMP
			{
				// Token: 0x0400962B RID: 38443
				public static LocString NAME = UI.FormatAsLink("Mini Liquid Pump", "LIQUIDMINIPUMP");

				// Token: 0x0400962C RID: 38444
				public static LocString DESC = "Mini pumps are useful for moving small quantities of liquid with minimum power.";

				// Token: 0x0400962D RID: 38445
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Draws in a small amount of ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" and runs it through ",
					UI.FormatAsLink("Pipes", "LIQUIDPIPING"),
					".\n\nMust be submerged in ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					"."
				});
			}

			// Token: 0x020021EA RID: 8682
			public class LIQUIDPUMPINGSTATION
			{
				// Token: 0x0400962E RID: 38446
				public static LocString NAME = UI.FormatAsLink("Pitcher Pump", "LIQUIDPUMPINGSTATION");

				// Token: 0x0400962F RID: 38447
				public static LocString DESC = "Pitcher pumps allow Duplicants to bottle and deliver liquids from place to place.";

				// Token: 0x04009630 RID: 38448
				public static LocString EFFECT = "Manually pumps " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " into bottles for transport.\n\nDuplicants can only carry liquids that are bottled.";
			}

			// Token: 0x020021EB RID: 8683
			public class LIQUIDVALVE
			{
				// Token: 0x04009631 RID: 38449
				public static LocString NAME = UI.FormatAsLink("Liquid Valve", "LIQUIDVALVE");

				// Token: 0x04009632 RID: 38450
				public static LocString DESC = "Valves control the amount of liquid that moves through pipes, preventing waste.";

				// Token: 0x04009633 RID: 38451
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Controls the ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" volume permitted through ",
					UI.FormatAsLink("Pipes", "LIQUIDPIPING"),
					"."
				});
			}

			// Token: 0x020021EC RID: 8684
			public class LIQUIDLOGICVALVE
			{
				// Token: 0x04009634 RID: 38452
				public static LocString NAME = UI.FormatAsLink("Liquid Shutoff", "LIQUIDLOGICVALVE");

				// Token: 0x04009635 RID: 38453
				public static LocString DESC = "Automated piping saves power and time by removing the need for Duplicant input.";

				// Token: 0x04009636 RID: 38454
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Connects to an ",
					UI.FormatAsLink("Automation", "LOGIC"),
					" grid to automatically turn ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" flow on or off."
				});

				// Token: 0x04009637 RID: 38455
				public static LocString LOGIC_PORT = "Open/Close";

				// Token: 0x04009638 RID: 38456
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Allow Liquid flow";

				// Token: 0x04009639 RID: 38457
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Prevent Liquid flow";
			}

			// Token: 0x020021ED RID: 8685
			public class LIQUIDLIMITVALVE
			{
				// Token: 0x0400963A RID: 38458
				public static LocString NAME = UI.FormatAsLink("Liquid Meter Valve", "LIQUIDLIMITVALVE");

				// Token: 0x0400963B RID: 38459
				public static LocString DESC = "Meter Valves let an exact amount of liquid pass through before shutting off.";

				// Token: 0x0400963C RID: 38460
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Connects to an ",
					UI.FormatAsLink("Automation", "LOGIC"),
					" grid to automatically turn ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" flow off when the specified amount has passed through it."
				});

				// Token: 0x0400963D RID: 38461
				public static LocString LOGIC_PORT_OUTPUT = "Limit Reached";

				// Token: 0x0400963E RID: 38462
				public static LocString OUTPUT_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if limit has been reached";

				// Token: 0x0400963F RID: 38463
				public static LocString OUTPUT_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x04009640 RID: 38464
				public static LocString LOGIC_PORT_RESET = "Reset Meter";

				// Token: 0x04009641 RID: 38465
				public static LocString RESET_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Reset the amount";

				// Token: 0x04009642 RID: 38466
				public static LocString RESET_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Nothing";
			}

			// Token: 0x020021EE RID: 8686
			public class LIQUIDVENT
			{
				// Token: 0x04009643 RID: 38467
				public static LocString NAME = UI.FormatAsLink("Liquid Vent", "LIQUIDVENT");

				// Token: 0x04009644 RID: 38468
				public static LocString DESC = "Vents are an exit point for liquids from plumbing systems.";

				// Token: 0x04009645 RID: 38469
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Releases ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" from ",
					UI.FormatAsLink("Liquid Pipes", "LIQUIDPIPING"),
					"."
				});
			}

			// Token: 0x020021EF RID: 8687
			public class MANUALGENERATOR
			{
				// Token: 0x04009646 RID: 38470
				public static LocString NAME = UI.FormatAsLink("Manual Generator", "MANUALGENERATOR");

				// Token: 0x04009647 RID: 38471
				public static LocString DESC = "Watching Duplicants run on it is adorable... the electrical power is just an added bonus.";

				// Token: 0x04009648 RID: 38472
				public static LocString EFFECT = "Converts manual labor into electrical " + UI.FormatAsLink("Power", "POWER") + ".";
			}

			// Token: 0x020021F0 RID: 8688
			public class MANUALPRESSUREDOOR
			{
				// Token: 0x04009649 RID: 38473
				public static LocString NAME = UI.FormatAsLink("Manual Airlock", "MANUALPRESSUREDOOR");

				// Token: 0x0400964A RID: 38474
				public static LocString DESC = "Airlocks can quarter off dangerous areas and prevent gases from seeping into the colony.";

				// Token: 0x0400964B RID: 38475
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Blocks ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" and ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" flow, maintaining pressure between areas.\n\nWild ",
					UI.FormatAsLink("Critters", "CRITTERS"),
					" cannot pass through doors."
				});
			}

			// Token: 0x020021F1 RID: 8689
			public class MESHTILE
			{
				// Token: 0x0400964C RID: 38476
				public static LocString NAME = UI.FormatAsLink("Mesh Tile", "MESHTILE");

				// Token: 0x0400964D RID: 38477
				public static LocString DESC = "Mesh tile can be used to make Duplicant pathways in areas where liquid flows.";

				// Token: 0x0400964E RID: 38478
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Used to build the walls and floors of rooms.\n\nDoes not obstruct ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" or ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" flow."
				});
			}

			// Token: 0x020021F2 RID: 8690
			public class PLASTICTILE
			{
				// Token: 0x0400964F RID: 38479
				public static LocString NAME = UI.FormatAsLink("Plastic Tile", "PLASTICTILE");

				// Token: 0x04009650 RID: 38480
				public static LocString DESC = "Plastic tile is mildly antiseptic and can help limit the spread of germs in a colony.";

				// Token: 0x04009651 RID: 38481
				public static LocString EFFECT = "Used to build the walls and floors of rooms.\n\nSignificantly increases Duplicant runspeed.";
			}

			// Token: 0x020021F3 RID: 8691
			public class GLASSTILE
			{
				// Token: 0x04009652 RID: 38482
				public static LocString NAME = UI.FormatAsLink("Window Tile", "GLASSTILE");

				// Token: 0x04009653 RID: 38483
				public static LocString DESC = "Window tiles provide a barrier against liquid and gas and are completely transparent.";

				// Token: 0x04009654 RID: 38484
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Used to build the walls and floors of rooms.\n\nAllows ",
					UI.FormatAsLink("Light", "LIGHT"),
					" and ",
					UI.FormatAsLink("Decor", "DECOR"),
					" to pass through."
				});
			}

			// Token: 0x020021F4 RID: 8692
			public class METALTILE
			{
				// Token: 0x04009655 RID: 38485
				public static LocString NAME = UI.FormatAsLink("Metal Tile", "METALTILE");

				// Token: 0x04009656 RID: 38486
				public static LocString DESC = "Heat travels much more quickly through metal tile than other types of flooring.";

				// Token: 0x04009657 RID: 38487
				public static LocString EFFECT = "Used to build the walls and floors of rooms.\n\nSignificantly increases Duplicant runspeed.";
			}

			// Token: 0x020021F5 RID: 8693
			public class BUNKERTILE
			{
				// Token: 0x04009658 RID: 38488
				public static LocString NAME = UI.FormatAsLink("Bunker Tile", "BUNKERTILE");

				// Token: 0x04009659 RID: 38489
				public static LocString DESC = "Bunker tile can build strong shelters in otherwise dangerous environments.";

				// Token: 0x0400965A RID: 38490
				public static LocString EFFECT = "Used to build the walls and floors of rooms.\n\nCan withstand extreme pressures and impacts.";
			}

			// Token: 0x020021F6 RID: 8694
			public class CARPETTILE
			{
				// Token: 0x0400965B RID: 38491
				public static LocString NAME = UI.FormatAsLink("Carpeted Tile", "CARPETTILE");

				// Token: 0x0400965C RID: 38492
				public static LocString DESC = "Soft on little Duplicant toesies.";

				// Token: 0x0400965D RID: 38493
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Used to build the walls and floors of rooms.\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});
			}

			// Token: 0x020021F7 RID: 8695
			public class MOULDINGTILE
			{
				// Token: 0x0400965E RID: 38494
				public static LocString NAME = UI.FormatAsLink("Trimming Tile", "MOUDLINGTILE");

				// Token: 0x0400965F RID: 38495
				public static LocString DESC = "Trimming is used as purely decorative lining for walls and structures.";

				// Token: 0x04009660 RID: 38496
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Used to build the walls and floors of rooms.\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});
			}

			// Token: 0x020021F8 RID: 8696
			public class MONUMENTBOTTOM
			{
				// Token: 0x04009661 RID: 38497
				public static LocString NAME = UI.FormatAsLink("Monument Base", "MONUMENTBOTTOM");

				// Token: 0x04009662 RID: 38498
				public static LocString DESC = "The base of a monument must be constructed first.";

				// Token: 0x04009663 RID: 38499
				public static LocString EFFECT = "Builds the bottom section of a Great Monument.\n\nCan be customized.\n\nA Great Monument must be built to achieve the Colonize Imperative.";
			}

			// Token: 0x020021F9 RID: 8697
			public class MONUMENTMIDDLE
			{
				// Token: 0x04009664 RID: 38500
				public static LocString NAME = UI.FormatAsLink("Monument Midsection", "MONUMENTMIDDLE");

				// Token: 0x04009665 RID: 38501
				public static LocString DESC = "Customized sections of a Great Monument can be mixed and matched.";

				// Token: 0x04009666 RID: 38502
				public static LocString EFFECT = "Builds the middle section of a Great Monument.\n\nCan be customized.\n\nA Great Monument must be built to achieve the Colonize Imperative.";
			}

			// Token: 0x020021FA RID: 8698
			public class MONUMENTTOP
			{
				// Token: 0x04009667 RID: 38503
				public static LocString NAME = UI.FormatAsLink("Monument Top", "MONUMENTTOP");

				// Token: 0x04009668 RID: 38504
				public static LocString DESC = "Building a Great Monument will declare to the universe that this hunk of rock is your own.";

				// Token: 0x04009669 RID: 38505
				public static LocString EFFECT = "Builds the top section of a Great Monument.\n\nCan be customized.\n\nA Great Monument must be built to achieve the Colonize Imperative.";
			}

			// Token: 0x020021FB RID: 8699
			public class MICROBEMUSHER
			{
				// Token: 0x0400966A RID: 38506
				public static LocString NAME = UI.FormatAsLink("Microbe Musher", "MICROBEMUSHER");

				// Token: 0x0400966B RID: 38507
				public static LocString DESC = "Musher recipes will keep Duplicants fed, but may impact health and morale over time.";

				// Token: 0x0400966C RID: 38508
				public static LocString EFFECT = "Produces low quality " + UI.FormatAsLink("Food", "FOOD") + " using common ingredients.\n\nDuplicants will not fabricate items unless recipes are queued.";
			}

			// Token: 0x020021FC RID: 8700
			public class MINERALDEOXIDIZER
			{
				// Token: 0x0400966D RID: 38509
				public static LocString NAME = UI.FormatAsLink("Oxygen Diffuser", "MINERALDEOXIDIZER");

				// Token: 0x0400966E RID: 38510
				public static LocString DESC = "Oxygen diffusers are inefficient, but output enough oxygen to keep a colony breathing.";

				// Token: 0x0400966F RID: 38511
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts large amounts of ",
					UI.FormatAsLink("Algae", "ALGAE"),
					" into ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					".\n\nBecomes idle when the area reaches maximum pressure capacity."
				});
			}

			// Token: 0x020021FD RID: 8701
			public class SUBLIMATIONSTATION
			{
				// Token: 0x04009670 RID: 38512
				public static LocString NAME = UI.FormatAsLink("Sublimation Station", "SUBLIMATIONSTATION");

				// Token: 0x04009671 RID: 38513
				public static LocString DESC = "Sublimation is the sublime process by which solids convert directly into gas.";

				// Token: 0x04009672 RID: 38514
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Speeds up the conversion of ",
					UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
					" into ",
					UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN"),
					".\n\nBecomes idle when the area reaches maximum pressure capacity."
				});
			}

			// Token: 0x020021FE RID: 8702
			public class ORESCRUBBER
			{
				// Token: 0x04009673 RID: 38515
				public static LocString NAME = UI.FormatAsLink("Ore Scrubber", "ORESCRUBBER");

				// Token: 0x04009674 RID: 38516
				public static LocString DESC = "Scrubbers sanitize freshly mined materials before they're brought into the colony.";

				// Token: 0x04009675 RID: 38517
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Kills a significant amount of ",
					UI.FormatAsLink("Germs", "DISEASE"),
					" present on ",
					UI.FormatAsLink("Raw Ore", "RAWMINERAL"),
					"."
				});
			}

			// Token: 0x020021FF RID: 8703
			public class OUTHOUSE
			{
				// Token: 0x04009676 RID: 38518
				public static LocString NAME = UI.FormatAsLink("Outhouse", "OUTHOUSE");

				// Token: 0x04009677 RID: 38519
				public static LocString DESC = "The colony that eats together, excretes together.";

				// Token: 0x04009678 RID: 38520
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Gives Duplicants a place to relieve themselves.\n\nRequires no ",
					UI.FormatAsLink("Piping", "LIQUIDPIPING"),
					".\n\nMust be periodically emptied of ",
					UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
					"."
				});
			}

			// Token: 0x02002200 RID: 8704
			public class APOTHECARY
			{
				// Token: 0x04009679 RID: 38521
				public static LocString NAME = UI.FormatAsLink("Apothecary", "APOTHECARY");

				// Token: 0x0400967A RID: 38522
				public static LocString DESC = "Some medications help prevent diseases, while others aim to alleviate existing illness.";

				// Token: 0x0400967B RID: 38523
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsLink("Medicine", "MEDICINE"),
					" to cure most basic ",
					UI.FormatAsLink("Diseases", "DISEASE"),
					".\n\nDuplicants must possess the Medicine Compounding ",
					UI.FormatAsLink("Skill", "ROLES"),
					" to fabricate medicines.\n\nDuplicants will not fabricate items unless recipes are queued."
				});
			}

			// Token: 0x02002201 RID: 8705
			public class ADVANCEDAPOTHECARY
			{
				// Token: 0x0400967C RID: 38524
				public static LocString NAME = UI.FormatAsLink("Nuclear Apothecary", "ADVANCEDAPOTHECARY");

				// Token: 0x0400967D RID: 38525
				public static LocString DESC = "Some medications help prevent diseases, while others aim to alleviate existing illness.";

				// Token: 0x0400967E RID: 38526
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsLink("Medicine", "MEDICINE"),
					" to cure most basic ",
					UI.FormatAsLink("Diseases", "DISEASE"),
					".\n\nDuplicants must possess the Medicine Compounding ",
					UI.FormatAsLink("Skill", "ROLES"),
					" to fabricate medicines.\n\nDuplicants will not fabricate items unless recipes are queued."
				});
			}

			// Token: 0x02002202 RID: 8706
			public class PLANTERBOX
			{
				// Token: 0x0400967F RID: 38527
				public static LocString NAME = UI.FormatAsLink("Planter Box", "PLANTERBOX");

				// Token: 0x04009680 RID: 38528
				public static LocString DESC = "Domestically grown seeds mature more quickly than wild plants.";

				// Token: 0x04009681 RID: 38529
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Grows one ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" from a ",
					UI.FormatAsLink("Seed", "PLANTS"),
					"."
				});
			}

			// Token: 0x02002203 RID: 8707
			public class PRESSUREDOOR
			{
				// Token: 0x04009682 RID: 38530
				public static LocString NAME = UI.FormatAsLink("Mechanized Airlock", "PRESSUREDOOR");

				// Token: 0x04009683 RID: 38531
				public static LocString DESC = "Mechanized airlocks open and close more quickly than other types of door.";

				// Token: 0x04009684 RID: 38532
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Blocks ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" and ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" flow, maintaining pressure between areas.\n\nFunctions as a ",
					UI.FormatAsLink("Manual Airlock", "MANUALPRESSUREDOOR"),
					" when no ",
					UI.FormatAsLink("Power", "POWER"),
					" is available.\n\nWild ",
					UI.FormatAsLink("Critters", "CRITTERS"),
					" cannot pass through doors."
				});
			}

			// Token: 0x02002204 RID: 8708
			public class BUNKERDOOR
			{
				// Token: 0x04009685 RID: 38533
				public static LocString NAME = UI.FormatAsLink("Bunker Door", "BUNKERDOOR");

				// Token: 0x04009686 RID: 38534
				public static LocString DESC = "A massive, slow-moving door which is nearly indestructible.";

				// Token: 0x04009687 RID: 38535
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Blocks ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" and ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" flow, maintaining pressure between areas.\n\nCan withstand extremely high pressures and impacts."
				});
			}

			// Token: 0x02002205 RID: 8709
			public class RATIONBOX
			{
				// Token: 0x04009688 RID: 38536
				public static LocString NAME = UI.FormatAsLink("Ration Box", "RATIONBOX");

				// Token: 0x04009689 RID: 38537
				public static LocString DESC = "Ration boxes keep food safe from hungry critters, but don't slow food spoilage.";

				// Token: 0x0400968A RID: 38538
				public static LocString EFFECT = "Stores a small amount of " + UI.FormatAsLink("Food", "FOOD") + ".\n\nFood must be delivered to boxes by Duplicants.";
			}

			// Token: 0x02002206 RID: 8710
			public class PARKSIGN
			{
				// Token: 0x0400968B RID: 38539
				public static LocString NAME = UI.FormatAsLink("Park Sign", "PARKSIGN");

				// Token: 0x0400968C RID: 38540
				public static LocString DESC = "Passing through parks will increase Duplicant Morale.";

				// Token: 0x0400968D RID: 38541
				public static LocString EFFECT = "Classifies an area as a Park or Nature Reserve.";
			}

			// Token: 0x02002207 RID: 8711
			public class RADIATIONLIGHT
			{
				// Token: 0x0400968E RID: 38542
				public static LocString NAME = UI.FormatAsLink("Radiation Lamp", "RADIATIONLIGHT");

				// Token: 0x0400968F RID: 38543
				public static LocString DESC = "Duplicants can become sick if exposed to radiation without protection.";

				// Token: 0x04009690 RID: 38544
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Emits ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					" when ",
					UI.FormatAsLink("Powered", "POWER"),
					" that can be collected by a ",
					UI.FormatAsLink("Radbolt Generator", "HIGHENERGYPARTICLESPAWNER"),
					"."
				});
			}

			// Token: 0x02002208 RID: 8712
			public class REFRIGERATOR
			{
				// Token: 0x04009691 RID: 38545
				public static LocString NAME = UI.FormatAsLink("Refrigerator", "REFRIGERATOR");

				// Token: 0x04009692 RID: 38546
				public static LocString DESC = "Food spoilage can be slowed by ambient conditions as well as by refrigerators.";

				// Token: 0x04009693 RID: 38547
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Food", "FOOD"),
					" at an ideal ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" to prevent spoilage."
				});

				// Token: 0x04009694 RID: 38548
				public static LocString LOGIC_PORT = "Full/Not Full";

				// Token: 0x04009695 RID: 38549
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when full";

				// Token: 0x04009696 RID: 38550
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002209 RID: 8713
			public class ROLESTATION
			{
				// Token: 0x04009697 RID: 38551
				public static LocString NAME = UI.FormatAsLink("Skills Board", "ROLESTATION");

				// Token: 0x04009698 RID: 38552
				public static LocString DESC = "A skills board can teach special skills to Duplicants they can't learn on their own.";

				// Token: 0x04009699 RID: 38553
				public static LocString EFFECT = "Allows Duplicants to spend Skill Points to learn new " + UI.FormatAsLink("Skills", "JOBS") + ".";
			}

			// Token: 0x0200220A RID: 8714
			public class RESETSKILLSSTATION
			{
				// Token: 0x0400969A RID: 38554
				public static LocString NAME = UI.FormatAsLink("Skill Scrubber", "RESETSKILLSSTATION");

				// Token: 0x0400969B RID: 38555
				public static LocString DESC = "Erase skills from a Duplicant's mind, returning them to their default abilities.";

				// Token: 0x0400969C RID: 38556
				public static LocString EFFECT = "Refunds a Duplicant's Skill Points for reassignment.\n\nDuplicants will lose all assigned skills in the process.";
			}

			// Token: 0x0200220B RID: 8715
			public class RESEARCHCENTER
			{
				// Token: 0x0400969D RID: 38557
				public static LocString NAME = UI.FormatAsLink("Research Station", "RESEARCHCENTER");

				// Token: 0x0400969E RID: 38558
				public static LocString DESC = "Research stations are necessary for unlocking all research tiers.";

				// Token: 0x0400969F RID: 38559
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Conducts ",
					UI.FormatAsLink("Novice Research", "RESEARCH"),
					" to unlock new technologies.\n\nConsumes ",
					UI.FormatAsLink("Dirt", "DIRT"),
					"."
				});
			}

			// Token: 0x0200220C RID: 8716
			public class ADVANCEDRESEARCHCENTER
			{
				// Token: 0x040096A0 RID: 38560
				public static LocString NAME = UI.FormatAsLink("Super Computer", "ADVANCEDRESEARCHCENTER");

				// Token: 0x040096A1 RID: 38561
				public static LocString DESC = "Super computers unlock higher technology tiers than research stations alone.";

				// Token: 0x040096A2 RID: 38562
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Conducts ",
					UI.FormatAsLink("Advanced Research", "RESEARCH"),
					" to unlock new technologies.\n\nConsumes ",
					UI.FormatAsLink("Water", "WATER"),
					".\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Advanced Research", "RESEARCHING1"),
					" skill."
				});
			}

			// Token: 0x0200220D RID: 8717
			public class NUCLEARRESEARCHCENTER
			{
				// Token: 0x040096A3 RID: 38563
				public static LocString NAME = UI.FormatAsLink("Materials Study Terminal", "NUCLEARRESEARCHCENTER");

				// Token: 0x040096A4 RID: 38564
				public static LocString DESC = "Comes with a few ions thrown in, free of charge.";

				// Token: 0x040096A5 RID: 38565
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Conducts ",
					UI.FormatAsLink("Materials Science Research", "RESEARCHDLC1"),
					" to unlock new technologies.\n\nConsumes Radbolts.\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Applied Sciences Research", "ATOMICRESEARCH"),
					" skill."
				});
			}

			// Token: 0x0200220E RID: 8718
			public class ORBITALRESEARCHCENTER
			{
				// Token: 0x040096A6 RID: 38566
				public static LocString NAME = UI.FormatAsLink("Orbital Data Collection Lab", "ORBITALRESEARCHCENTER");

				// Token: 0x040096A7 RID: 38567
				public static LocString DESC = "Orbital Data Collection Labs record data while orbiting a Planetoid and write it to a " + UI.FormatAsLink("Data Bank", "ORBITALRESEARCHDATABANK") + ". ";

				// Token: 0x040096A8 RID: 38568
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Creates ",
					UI.FormatAsLink("Data Banks", "ORBITALRESEARCHDATABANK"),
					" that can be consumed at a ",
					UI.FormatAsLink("Virtual Planetarium", "DLC1COSMICRESEARCHCENTER"),
					" to unlock new technologies.\n\nConsumes ",
					UI.FormatAsLink("Plastic", "POLYPROPYLENE"),
					" and ",
					UI.FormatAsLink("Power", "POWER"),
					"."
				});
			}

			// Token: 0x0200220F RID: 8719
			public class COSMICRESEARCHCENTER
			{
				// Token: 0x040096A9 RID: 38569
				public static LocString NAME = UI.FormatAsLink("Virtual Planetarium", "COSMICRESEARCHCENTER");

				// Token: 0x040096AA RID: 38570
				public static LocString DESC = "Planetariums allow the simulated exploration of locations discovered with a telescope.";

				// Token: 0x040096AB RID: 38571
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Conducts ",
					UI.FormatAsLink("Interstellar Research", "RESEARCH"),
					" to unlock new technologies.\n\nConsumes data from ",
					UI.FormatAsLink("Telescopes", "TELESCOPE"),
					" and ",
					UI.FormatAsLink("Research Modules", "RESEARCHMODULE"),
					".\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Astronomy", "ASTRONOMY"),
					" skill."
				});
			}

			// Token: 0x02002210 RID: 8720
			public class DLC1COSMICRESEARCHCENTER
			{
				// Token: 0x040096AC RID: 38572
				public static LocString NAME = UI.FormatAsLink("Virtual Planetarium", "DLC1COSMICRESEARCHCENTER");

				// Token: 0x040096AD RID: 38573
				public static LocString DESC = "Planetariums allow the simulated exploration of locations recorded in " + UI.FormatAsLink("Data Banks", "ORBITALRESEARCHDATABANK") + ".";

				// Token: 0x040096AE RID: 38574
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Conducts ",
					UI.FormatAsLink("Data Analysis Research", "RESEARCH"),
					" to unlock new technologies.\n\nConsumes ",
					UI.FormatAsLink("Data Banks", "ORBITALRESEARCHDATABANK"),
					" generated by exploration."
				});
			}

			// Token: 0x02002211 RID: 8721
			public class TELESCOPE
			{
				// Token: 0x040096AF RID: 38575
				public static LocString NAME = UI.FormatAsLink("Telescope", "TELESCOPE");

				// Token: 0x040096B0 RID: 38576
				public static LocString DESC = "Telescopes are necessary for learning starmaps and conducting rocket missions.";

				// Token: 0x040096B1 RID: 38577
				public static LocString EFFECT = "Maps Starmap destinations.\n\nAssigned Duplicants must possess the " + UI.FormatAsLink("Field Research", "RESEARCHING2") + " skill.\n\nBuilding must be exposed to space to function.";

				// Token: 0x040096B2 RID: 38578
				public static LocString REQUIREMENT_TOOLTIP = "A steady {0} supply is required to sustain working Duplicants.";
			}

			// Token: 0x02002212 RID: 8722
			public class CLUSTERTELESCOPE
			{
				// Token: 0x040096B3 RID: 38579
				public static LocString NAME = UI.FormatAsLink("Telescope", "CLUSTERTELESCOPE");

				// Token: 0x040096B4 RID: 38580
				public static LocString DESC = "Telescopes are necessary for studying space, allowing rocket travel to other worlds.";

				// Token: 0x040096B5 RID: 38581
				public static LocString EFFECT = "Reveals visitable Planetoids in space.\n\nAssigned Duplicants must possess the " + UI.FormatAsLink("Astronomy", "ASTRONOMY") + " skill.\n\nBuilding must be exposed to space to function.";

				// Token: 0x040096B6 RID: 38582
				public static LocString REQUIREMENT_TOOLTIP = "A steady {0} supply is required to sustain working Duplicants.";
			}

			// Token: 0x02002213 RID: 8723
			public class CLUSTERTELESCOPEENCLOSED
			{
				// Token: 0x040096B7 RID: 38583
				public static LocString NAME = UI.FormatAsLink("Enclosed Telescope", "CLUSTERTELESCOPEENCLOSED");

				// Token: 0x040096B8 RID: 38584
				public static LocString DESC = "Telescopes are necessary for studying space, allowing rocket travel to other worlds.";

				// Token: 0x040096B9 RID: 38585
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Reveals visitable Planetoids in space... in comfort!\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Astronomy", "ASTRONOMY"),
					" skill.\n\nExcellent sunburn protection (100%), partial ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					" protection (",
					GameUtil.GetFormattedPercent(FIXEDTRAITS.COSMICRADIATION.TELESCOPE_RADIATION_SHIELDING * 100f, GameUtil.TimeSlice.None),
					") .\n\nBuilding must be exposed to space to function."
				});

				// Token: 0x040096BA RID: 38586
				public static LocString REQUIREMENT_TOOLTIP = "A steady {0} supply is required to sustain working Duplicants.";
			}

			// Token: 0x02002214 RID: 8724
			public class MISSIONCONTROL
			{
				// Token: 0x040096BB RID: 38587
				public static LocString NAME = UI.FormatAsLink("Mission Control Station", "MISSIONCONTROL");

				// Token: 0x040096BC RID: 38588
				public static LocString DESC = "Like a backseat driver who actually does know better.";

				// Token: 0x040096BD RID: 38589
				public static LocString EFFECT = "Provides guidance data to rocket pilots, to improve rocket speed.\n\nMust be operated by a Duplicant with the " + UI.FormatAsLink("Astronomy", "ASTRONOMY") + " skill.\n\nRequires a clear line of sight to space in order to function.";
			}

			// Token: 0x02002215 RID: 8725
			public class MISSIONCONTROLCLUSTER
			{
				// Token: 0x040096BE RID: 38590
				public static LocString NAME = UI.FormatAsLink("Mission Control Station", "MISSIONCONTROL");

				// Token: 0x040096BF RID: 38591
				public static LocString DESC = "Like a backseat driver who actually does know better.";

				// Token: 0x040096C0 RID: 38592
				public static LocString EFFECT = "Provides guidance data to rocket pilots within range, to improve rocket speed.\n\nMust be operated by a Duplicant with the " + UI.FormatAsLink("Astronomy", "ASTRONOMY") + " skill.\n\nRequires a clear line of sight to space in order to function.";
			}

			// Token: 0x02002216 RID: 8726
			public class SCULPTURE
			{
				// Token: 0x040096C1 RID: 38593
				public static LocString NAME = UI.FormatAsLink("Large Sculpting Block", "SCULPTURE");

				// Token: 0x040096C2 RID: 38594
				public static LocString DESC = "Duplicants who have learned art skills can produce more decorative sculptures.";

				// Token: 0x040096C3 RID: 38595
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Moderately increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be sculpted by a Duplicant."
				});

				// Token: 0x040096C4 RID: 38596
				public static LocString POORQUALITYNAME = "\"Abstract\" Sculpture";

				// Token: 0x040096C5 RID: 38597
				public static LocString AVERAGEQUALITYNAME = "Mediocre Sculpture";

				// Token: 0x040096C6 RID: 38598
				public static LocString EXCELLENTQUALITYNAME = "Genius Sculpture";

				// Token: 0x02002DC7 RID: 11719
				public class FACADES
				{
					// Token: 0x0200303D RID: 12349
					public class SCULPTURE_GOOD_1
					{
						// Token: 0x0400C0FB RID: 49403
						public static LocString NAME = UI.FormatAsLink("O Cupid, My Cupid", "SCULPTURE_GOOD_1");

						// Token: 0x0400C0FC RID: 49404
						public static LocString DESC = "Ode to the bow and arrow, love's equivalent to a mining gun...but for hearts.";
					}

					// Token: 0x0200303E RID: 12350
					public class SCULPTURE_CRAP_1
					{
						// Token: 0x0400C0FD RID: 49405
						public static LocString NAME = UI.FormatAsLink("Inexplicable", "SCULPTURE_CRAP_1");

						// Token: 0x0400C0FE RID: 49406
						public static LocString DESC = "A valiant attempt at art.";
					}

					// Token: 0x0200303F RID: 12351
					public class SCULPTURE_AMAZING_2
					{
						// Token: 0x0400C0FF RID: 49407
						public static LocString NAME = UI.FormatAsLink("Plate Chucker", "SCULPTURE_AMAZING_2");

						// Token: 0x0400C100 RID: 49408
						public static LocString DESC = "A masterful portrayal of an athlete who's been banned from the communal kitchen.";
					}

					// Token: 0x02003040 RID: 12352
					public class SCULPTURE_AMAZING_3
					{
						// Token: 0x0400C101 RID: 49409
						public static LocString NAME = UI.FormatAsLink("Before Battle", "SCULPTURE_AMAZING_3");

						// Token: 0x0400C102 RID: 49410
						public static LocString DESC = "A masterful portrayal of a slingshot-wielding hero.";
					}

					// Token: 0x02003041 RID: 12353
					public class SCULPTURE_AMAZING_4
					{
						// Token: 0x0400C103 RID: 49411
						public static LocString NAME = UI.FormatAsLink("Grandiose Grub-Grub", "SCULPTURE_AMAZING_4");

						// Token: 0x0400C104 RID: 49412
						public static LocString DESC = "A masterful portrayal of a gentle, plant-tending critter.";
					}

					// Token: 0x02003042 RID: 12354
					public class SCULPTURE_AMAZING_1
					{
						// Token: 0x0400C105 RID: 49413
						public static LocString NAME = UI.FormatAsLink("The Hypothesizer", "SCULPTURE_AMAZING_1");

						// Token: 0x0400C106 RID: 49414
						public static LocString DESC = "A masterful portrayal of a scientist lost in thought.";
					}
				}
			}

			// Token: 0x02002217 RID: 8727
			public class ICESCULPTURE
			{
				// Token: 0x040096C7 RID: 38599
				public static LocString NAME = UI.FormatAsLink("Ice Block", "ICESCULPTURE");

				// Token: 0x040096C8 RID: 38600
				public static LocString DESC = "Prone to melting.";

				// Token: 0x040096C9 RID: 38601
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Majorly increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be sculpted by a Duplicant."
				});

				// Token: 0x040096CA RID: 38602
				public static LocString POORQUALITYNAME = "\"Abstract\" Ice Sculpture";

				// Token: 0x040096CB RID: 38603
				public static LocString AVERAGEQUALITYNAME = "Mediocre Ice Sculpture";

				// Token: 0x040096CC RID: 38604
				public static LocString EXCELLENTQUALITYNAME = "Genius Ice Sculpture";

				// Token: 0x02002DC8 RID: 11720
				public class FACADES
				{
					// Token: 0x02003043 RID: 12355
					public class ICESCULPTURE_CRAP
					{
						// Token: 0x0400C107 RID: 49415
						public static LocString NAME = UI.FormatAsLink("Cubi I", "ICESCULPTURE_CRAP");

						// Token: 0x0400C108 RID: 49416
						public static LocString DESC = "";
					}

					// Token: 0x02003044 RID: 12356
					public class ICESCULPTURE_AMAZING_1
					{
						// Token: 0x0400C109 RID: 49417
						public static LocString NAME = UI.FormatAsLink("Exquisite Chompers", "ICESCULPTURE_AMAZING_1");

						// Token: 0x0400C10A RID: 49418
						public static LocString DESC = "";
					}

					// Token: 0x02003045 RID: 12357
					public class ICESCULPTURE_AMAZING_2
					{
						// Token: 0x0400C10B RID: 49419
						public static LocString NAME = UI.FormatAsLink("Frosty Crustacean", "ICESCULPTURE_AMAZING_2");

						// Token: 0x0400C10C RID: 49420
						public static LocString DESC = "A masterful depiction of the mighty Pokeshell in mid-rampage.";
					}
				}
			}

			// Token: 0x02002218 RID: 8728
			public class MARBLESCULPTURE
			{
				// Token: 0x040096CD RID: 38605
				public static LocString NAME = UI.FormatAsLink("Marble Block", "MARBLESCULPTURE");

				// Token: 0x040096CE RID: 38606
				public static LocString DESC = "Duplicants who have learned art skills can produce more decorative sculptures.";

				// Token: 0x040096CF RID: 38607
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Majorly increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be sculpted by a Duplicant."
				});

				// Token: 0x040096D0 RID: 38608
				public static LocString POORQUALITYNAME = "\"Abstract\" Marble Sculpture";

				// Token: 0x040096D1 RID: 38609
				public static LocString AVERAGEQUALITYNAME = "Mediocre Marble Sculpture";

				// Token: 0x040096D2 RID: 38610
				public static LocString EXCELLENTQUALITYNAME = "Genius Marble Sculpture";

				// Token: 0x02002DC9 RID: 11721
				public class FACADES
				{
					// Token: 0x02003046 RID: 12358
					public class SCULPTURE_MARBLE_CRAP_1
					{
						// Token: 0x0400C10D RID: 49421
						public static LocString NAME = UI.FormatAsLink("Lumpy Fungus", "SCULPTURE_MARBLE_CRAP_1");

						// Token: 0x0400C10E RID: 49422
						public static LocString DESC = "";
					}

					// Token: 0x02003047 RID: 12359
					public class SCULPTURE_MARBLE_GOOD_1
					{
						// Token: 0x0400C10F RID: 49423
						public static LocString NAME = UI.FormatAsLink("Unicorn Bust", "SCULPTURE_MARBLE_GOOD_1");

						// Token: 0x0400C110 RID: 49424
						public static LocString DESC = "";
					}

					// Token: 0x02003048 RID: 12360
					public class SCULPTURE_MARBLE_AMAZING_1
					{
						// Token: 0x0400C111 RID: 49425
						public static LocString NAME = UI.FormatAsLink("The Large-ish Mermaid", "SCULPTURE_MARBLE_AMAZING_1");

						// Token: 0x0400C112 RID: 49426
						public static LocString DESC = "";
					}

					// Token: 0x02003049 RID: 12361
					public class SCULPTURE_MARBLE_AMAZING_2
					{
						// Token: 0x0400C113 RID: 49427
						public static LocString NAME = UI.FormatAsLink("Grouchy Beast", "SCULPTURE_MARBLE_AMAZING_2");

						// Token: 0x0400C114 RID: 49428
						public static LocString DESC = "";
					}

					// Token: 0x0200304A RID: 12362
					public class SCULPTURE_MARBLE_AMAZING_3
					{
						// Token: 0x0400C115 RID: 49429
						public static LocString NAME = UI.FormatAsLink("The Guardian", "SCULPTURE_MARBLE_AMAZING_3");

						// Token: 0x0400C116 RID: 49430
						public static LocString DESC = "";
					}

					// Token: 0x0200304B RID: 12363
					public class SCULPTURE_MARBLE_AMAZING_4
					{
						// Token: 0x0400C117 RID: 49431
						public static LocString NAME = UI.FormatAsLink("Truly A-Moo-Zing", "SCULPTURE_MARBLE_AMAZING_4");

						// Token: 0x0400C118 RID: 49432
						public static LocString DESC = "A masterful celebration of one of the universe's most mysterious - and flatulent - organisms.";
					}

					// Token: 0x0200304C RID: 12364
					public class SCULPTURE_MARBLE_AMAZING_5
					{
						// Token: 0x0400C119 RID: 49433
						public static LocString NAME = UI.FormatAsLink("Green Goddess", "SCULPTURE_MARBLE_AMAZING_5");

						// Token: 0x0400C11A RID: 49434
						public static LocString DESC = "A masterful celebration of the deep bond between a horticulturalist and her prize Bristle Blossom.";
					}
				}
			}

			// Token: 0x02002219 RID: 8729
			public class METALSCULPTURE
			{
				// Token: 0x040096D3 RID: 38611
				public static LocString NAME = UI.FormatAsLink("Metal Block", "METALSCULPTURE");

				// Token: 0x040096D4 RID: 38612
				public static LocString DESC = "Duplicants who have learned art skills can produce more decorative sculptures.";

				// Token: 0x040096D5 RID: 38613
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Majorly increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be sculpted by a Duplicant."
				});

				// Token: 0x040096D6 RID: 38614
				public static LocString POORQUALITYNAME = "\"Abstract\" Metal Sculpture";

				// Token: 0x040096D7 RID: 38615
				public static LocString AVERAGEQUALITYNAME = "Mediocre Metal Sculpture";

				// Token: 0x040096D8 RID: 38616
				public static LocString EXCELLENTQUALITYNAME = "Genius Metal Sculpture";

				// Token: 0x02002DCA RID: 11722
				public class FACADES
				{
					// Token: 0x0200304D RID: 12365
					public class SCULPTURE_METAL_CRAP_1
					{
						// Token: 0x0400C11B RID: 49435
						public static LocString NAME = UI.FormatAsLink("Unnatural Beauty", "SCULPTURE_METAL_CRAP_1");

						// Token: 0x0400C11C RID: 49436
						public static LocString DESC = "";
					}

					// Token: 0x0200304E RID: 12366
					public class SCULPTURE_METAL_GOOD_1
					{
						// Token: 0x0400C11D RID: 49437
						public static LocString NAME = UI.FormatAsLink("Beautiful Biohazard", "SCULPTURE_METAL_GOOD_1");

						// Token: 0x0400C11E RID: 49438
						public static LocString DESC = "";
					}

					// Token: 0x0200304F RID: 12367
					public class SCULPTURE_METAL_AMAZING_1
					{
						// Token: 0x0400C11F RID: 49439
						public static LocString NAME = UI.FormatAsLink("Insatiable Appetite", "SCULPTURE_METAL_AMAZING_1");

						// Token: 0x0400C120 RID: 49440
						public static LocString DESC = "";
					}

					// Token: 0x02003050 RID: 12368
					public class SCULPTURE_METAL_AMAZING_2
					{
						// Token: 0x0400C121 RID: 49441
						public static LocString NAME = UI.FormatAsLink("Mouth Breather", "SCULPTURE_METAL_AMAZING_2");

						// Token: 0x0400C122 RID: 49442
						public static LocString DESC = "";
					}

					// Token: 0x02003051 RID: 12369
					public class SCULPTURE_METAL_AMAZING_3
					{
						// Token: 0x0400C123 RID: 49443
						public static LocString NAME = UI.FormatAsLink("Friendly Flier", "SCULPTURE_METAL_AMAZING_3");

						// Token: 0x0400C124 RID: 49444
						public static LocString DESC = "";
					}

					// Token: 0x02003052 RID: 12370
					public class SCULPTURE_METAL_AMAZING_4
					{
						// Token: 0x0400C125 RID: 49445
						public static LocString NAME = UI.FormatAsLink("Whatta Pip", "SCULPTURE_METAL_AMAZING_4");

						// Token: 0x0400C126 RID: 49446
						public static LocString DESC = "A masterful likeness of the mischievous critter that Duplicants love to love.";
					}
				}
			}

			// Token: 0x0200221A RID: 8730
			public class SMALLSCULPTURE
			{
				// Token: 0x040096D9 RID: 38617
				public static LocString NAME = UI.FormatAsLink("Sculpting Block", "SMALLSCULPTURE");

				// Token: 0x040096DA RID: 38618
				public static LocString DESC = "Duplicants who have learned art skills can produce more decorative sculptures.";

				// Token: 0x040096DB RID: 38619
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Minorly increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be sculpted by a Duplicant."
				});

				// Token: 0x040096DC RID: 38620
				public static LocString POORQUALITYNAME = "\"Abstract\" Sculpture";

				// Token: 0x040096DD RID: 38621
				public static LocString AVERAGEQUALITYNAME = "Mediocre Sculpture";

				// Token: 0x040096DE RID: 38622
				public static LocString EXCELLENTQUALITYNAME = "Genius Sculpture";

				// Token: 0x02002DCB RID: 11723
				public class FACADES
				{
					// Token: 0x02003053 RID: 12371
					public class SCULPTURE_1x2_GOOD
					{
						// Token: 0x0400C127 RID: 49447
						public static LocString NAME = UI.FormatAsLink("Lunar Slice", "SCULPTURE_1x2_GOOD");

						// Token: 0x0400C128 RID: 49448
						public static LocString DESC = "";
					}

					// Token: 0x02003054 RID: 12372
					public class SCULPTURE_1x2_CRAP
					{
						// Token: 0x0400C129 RID: 49449
						public static LocString NAME = UI.FormatAsLink("Unrequited", "SCULPTURE_1x2_CRAP");

						// Token: 0x0400C12A RID: 49450
						public static LocString DESC = "";
					}

					// Token: 0x02003055 RID: 12373
					public class SCULPTURE_1x2_AMAZING_1
					{
						// Token: 0x0400C12B RID: 49451
						public static LocString NAME = UI.FormatAsLink("Not a Funnel", "SCULPTURE_1x2_AMAZING_1");

						// Token: 0x0400C12C RID: 49452
						public static LocString DESC = "";
					}

					// Token: 0x02003056 RID: 12374
					public class SCULPTURE_1x2_AMAZING_2
					{
						// Token: 0x0400C12D RID: 49453
						public static LocString NAME = UI.FormatAsLink("Equilibrium", "SCULPTURE_1x2_AMAZING_2");

						// Token: 0x0400C12E RID: 49454
						public static LocString DESC = "";
					}

					// Token: 0x02003057 RID: 12375
					public class SCULPTURE_1x2_AMAZING_3
					{
						// Token: 0x0400C12F RID: 49455
						public static LocString NAME = UI.FormatAsLink("Opaque Orb", "SCULPTURE_1x2_AMAZING_3");

						// Token: 0x0400C130 RID: 49456
						public static LocString DESC = "";
					}

					// Token: 0x02003058 RID: 12376
					public class SCULPTURE_1x2_AMAZING_4
					{
						// Token: 0x0400C131 RID: 49457
						public static LocString NAME = UI.FormatAsLink("Employee of the Month", "SCULPTURE_1x2_AMAZING_4");

						// Token: 0x0400C132 RID: 49458
						public static LocString DESC = "A masterful celebration of the Sweepy's unbeatable work ethic and cheerful, can-clean attitude.";
					}
				}
			}

			// Token: 0x0200221B RID: 8731
			public class SHEARINGSTATION
			{
				// Token: 0x040096DF RID: 38623
				public static LocString NAME = UI.FormatAsLink("Shearing Station", "SHEARINGSTATION");

				// Token: 0x040096E0 RID: 38624
				public static LocString DESC = string.Concat(new string[]
				{
					"Shearing stations allow ",
					UI.FormatAsLink("Dreckos", "DRECKO"),
					" and ",
					UI.FormatAsLink("Delecta Voles", "MOLEDELICACY"),
					" to be safely sheared for useful raw materials."
				});

				// Token: 0x040096E1 RID: 38625
				public static LocString EFFECT = "Allows the assigned Rancher to shear Dreckos and Delecta Voles.";
			}

			// Token: 0x0200221C RID: 8732
			public class OXYGENMASKSTATION
			{
				// Token: 0x040096E2 RID: 38626
				public static LocString NAME = UI.FormatAsLink("Oxygen Mask Station", "OXYGENMASKSTATION");

				// Token: 0x040096E3 RID: 38627
				public static LocString DESC = "Duplicants can't pass by a station if it lacks enough oxygen to fill a mask.";

				// Token: 0x040096E4 RID: 38628
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses designated ",
					UI.FormatAsLink("Metal Ores", "METAL"),
					" from filter settings to create ",
					UI.FormatAsLink("Oxygen Masks", "OXYGENMASK"),
					".\n\nAutomatically draws in ambient ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" to fill masks.\n\nMarks a threshold where Duplicants must put on or take off a mask.\n\nCan be rotated before construction."
				});
			}

			// Token: 0x0200221D RID: 8733
			public class SWEEPBOTSTATION
			{
				// Token: 0x040096E5 RID: 38629
				public static LocString NAME = UI.FormatAsLink("Sweepy's Dock", "SWEEPBOTSTATION");

				// Token: 0x040096E6 RID: 38630
				public static LocString NAMEDSTATION = "{0}'s Dock";

				// Token: 0x040096E7 RID: 38631
				public static LocString DESC = "The cute little face comes pre-installed.";

				// Token: 0x040096E8 RID: 38632
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Deploys an automated ",
					UI.FormatAsLink("Sweepy Bot", "SWEEPBOT"),
					" to sweep up ",
					UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
					" debris and ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" spills.\n\nDock stores ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					" and ",
					UI.FormatAsLink("Solids", "ELEMENTS_SOLID"),
					" gathered by the Sweepy.\n\nUses ",
					UI.FormatAsLink("Power", "POWER"),
					" to recharge the Sweepy.\n\nDuplicants will empty Dock storage into available storage bins."
				});
			}

			// Token: 0x0200221E RID: 8734
			public class OXYGENMASKMARKER
			{
				// Token: 0x040096E9 RID: 38633
				public static LocString NAME = UI.FormatAsLink("Oxygen Mask Checkpoint", "OXYGENMASKMARKER");

				// Token: 0x040096EA RID: 38634
				public static LocString DESC = "A checkpoint must have a correlating dock built on the opposite side its arrow faces.";

				// Token: 0x040096EB RID: 38635
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Marks a threshold where Duplicants must put on or take off an ",
					UI.FormatAsLink("Oxygen Mask", "OXYGEN_MASK"),
					".\n\nMust be built next to an ",
					UI.FormatAsLink("Oxygen Mask Dock", "OXYGENMASKLOCKER"),
					".\n\nCan be rotated before construction."
				});
			}

			// Token: 0x0200221F RID: 8735
			public class OXYGENMASKLOCKER
			{
				// Token: 0x040096EC RID: 38636
				public static LocString NAME = UI.FormatAsLink("Oxygen Mask Dock", "OXYGENMASKLOCKER");

				// Token: 0x040096ED RID: 38637
				public static LocString DESC = "An oxygen mask dock will store and refill masks while they're not in use.";

				// Token: 0x040096EE RID: 38638
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Oxygen Masks", "OXYGEN_MASK"),
					" and refuels them with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					".\n\nBuild next to an ",
					UI.FormatAsLink("Oxygen Mask Checkpoint", "OXYGENMASKMARKER"),
					" to make Duplicants put on masks when passing by."
				});
			}

			// Token: 0x02002220 RID: 8736
			public class SUITMARKER
			{
				// Token: 0x040096EF RID: 38639
				public static LocString NAME = UI.FormatAsLink("Atmo Suit Checkpoint", "SUITMARKER");

				// Token: 0x040096F0 RID: 38640
				public static LocString DESC = "A checkpoint must have a correlating dock built on the opposite side its arrow faces.";

				// Token: 0x040096F1 RID: 38641
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Marks a threshold where Duplicants must change into or out of ",
					UI.FormatAsLink("Atmo Suits", "ATMO_SUIT"),
					".\n\nMust be built next to an ",
					UI.FormatAsLink("Atmo Suit Dock", "SUITLOCKER"),
					".\n\nCan be rotated before construction."
				});
			}

			// Token: 0x02002221 RID: 8737
			public class SUITLOCKER
			{
				// Token: 0x040096F2 RID: 38642
				public static LocString NAME = UI.FormatAsLink("Atmo Suit Dock", "SUITLOCKER");

				// Token: 0x040096F3 RID: 38643
				public static LocString DESC = "An atmo suit dock will empty atmo suits of waste, but only one suit can charge at a time.";

				// Token: 0x040096F4 RID: 38644
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Atmo Suits", "ATMO_SUIT"),
					" and refuels them with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					".\n\nEmpties suits of ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					".\n\nBuild next to an ",
					UI.FormatAsLink("Atmo Suit Checkpoint", "SUITMARKER"),
					" to make Duplicants change into suits when passing by."
				});
			}

			// Token: 0x02002222 RID: 8738
			public class JETSUITMARKER
			{
				// Token: 0x040096F5 RID: 38645
				public static LocString NAME = UI.FormatAsLink("Jet Suit Checkpoint", "JETSUITMARKER");

				// Token: 0x040096F6 RID: 38646
				public static LocString DESC = "A checkpoint must have a correlating dock built on the opposite side its arrow faces.";

				// Token: 0x040096F7 RID: 38647
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Marks a threshold where Duplicants must change into or out of ",
					UI.FormatAsLink("Jet Suits", "JET_SUIT"),
					".\n\nMust be built next to a ",
					UI.FormatAsLink("Jet Suit Dock", "JETSUITLOCKER"),
					".\n\nCan be rotated before construction."
				});
			}

			// Token: 0x02002223 RID: 8739
			public class JETSUITLOCKER
			{
				// Token: 0x040096F8 RID: 38648
				public static LocString NAME = UI.FormatAsLink("Jet Suit Dock", "JETSUITLOCKER");

				// Token: 0x040096F9 RID: 38649
				public static LocString DESC = "Jet suit docks can refill jet suits with air and fuel, or empty them of waste.";

				// Token: 0x040096FA RID: 38650
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Jet Suits", "JET_SUIT"),
					" and refuels them with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" and ",
					UI.FormatAsLink("Petroleum", "PETROLEUM"),
					".\n\nEmpties suits of ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					".\n\nBuild next to a ",
					UI.FormatAsLink("Jet Suit Checkpoint", "JETSUITMARKER"),
					" to make Duplicants change into suits when passing by."
				});
			}

			// Token: 0x02002224 RID: 8740
			public class LEADSUITMARKER
			{
				// Token: 0x040096FB RID: 38651
				public static LocString NAME = UI.FormatAsLink("Lead Suit Checkpoint", "LEADSUITMARKER");

				// Token: 0x040096FC RID: 38652
				public static LocString DESC = "A checkpoint must have a correlating dock built on the opposite side its arrow faces.";

				// Token: 0x040096FD RID: 38653
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Marks a threshold where Duplicants must change into or out of ",
					UI.FormatAsLink("Lead Suits", "LEAD_SUIT"),
					".\n\nMust be built next to a ",
					UI.FormatAsLink("Lead Suit Dock", "LEADSUITLOCKER"),
					"\n\nCan be rotated before construction."
				});
			}

			// Token: 0x02002225 RID: 8741
			public class LEADSUITLOCKER
			{
				// Token: 0x040096FE RID: 38654
				public static LocString NAME = UI.FormatAsLink("Lead Suit Dock", "LEADSUITLOCKER");

				// Token: 0x040096FF RID: 38655
				public static LocString DESC = "Lead suit docks can refill lead suits with air and empty them of waste.";

				// Token: 0x04009700 RID: 38656
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Lead Suits", "LEAD_SUIT"),
					" and refuels them with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					".\n\nEmpties suits of ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					".\n\nBuild next to a ",
					UI.FormatAsLink("Lead Suit Checkpoint", "LEADSUITMARKER"),
					" to make Duplicants change into suits when passing by."
				});
			}

			// Token: 0x02002226 RID: 8742
			public class CRAFTINGTABLE
			{
				// Token: 0x04009701 RID: 38657
				public static LocString NAME = UI.FormatAsLink("Crafting Station", "CRAFTINGTABLE");

				// Token: 0x04009702 RID: 38658
				public static LocString DESC = "Crafting stations allow Duplicants to make oxygen masks to wear in low breathability areas.";

				// Token: 0x04009703 RID: 38659
				public static LocString EFFECT = "Produces items and equipment for Duplicant use.\n\nDuplicants will not fabricate items unless recipes are queued.";
			}

			// Token: 0x02002227 RID: 8743
			public class SUITFABRICATOR
			{
				// Token: 0x04009704 RID: 38660
				public static LocString NAME = UI.FormatAsLink("Exosuit Forge", "SUITFABRICATOR");

				// Token: 0x04009705 RID: 38661
				public static LocString DESC = "Exosuits can be filled with oxygen to allow Duplicants to safely enter hazardous areas.";

				// Token: 0x04009706 RID: 38662
				public static LocString EFFECT = "Forges protective " + UI.FormatAsLink("Exosuits", "EXOSUIT") + " for Duplicants to wear.\n\nDuplicants will not fabricate items unless recipes are queued.";
			}

			// Token: 0x02002228 RID: 8744
			public class CLOTHINGALTERATIONSTATION
			{
				// Token: 0x04009707 RID: 38663
				public static LocString NAME = UI.FormatAsLink("Clothing Refashionator", "CLOTHINGALTERATIONSTATION");

				// Token: 0x04009708 RID: 38664
				public static LocString DESC = "Allows skilled Duplicants to add extra personal pizzazz to their wardrobe.";

				// Token: 0x04009709 RID: 38665
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Upgrades ",
					UI.FormatAsLink("Snazzy Suits", "FUNKY_VEST"),
					" into ",
					UI.FormatAsLink("Primo Garb", "CUSTOM_CLOTHING"),
					".\n\nDuplicants will not fabricate items unless recipes are queued."
				});
			}

			// Token: 0x02002229 RID: 8745
			public class CLOTHINGFABRICATOR
			{
				// Token: 0x0400970A RID: 38666
				public static LocString NAME = UI.FormatAsLink("Textile Loom", "CLOTHINGFABRICATOR");

				// Token: 0x0400970B RID: 38667
				public static LocString DESC = "A textile loom can be used to spin Reed Fiber into wearable Duplicant clothing.";

				// Token: 0x0400970C RID: 38668
				public static LocString EFFECT = "Tailors Duplicant " + UI.FormatAsLink("Clothing", "EQUIPMENT") + " items.\n\nDuplicants will not fabricate items unless recipes are queued.";
			}

			// Token: 0x0200222A RID: 8746
			public class SOLIDBOOSTER
			{
				// Token: 0x0400970D RID: 38669
				public static LocString NAME = UI.FormatAsLink("Solid Fuel Thruster", "SOLIDBOOSTER");

				// Token: 0x0400970E RID: 38670
				public static LocString DESC = "Additional thrusters allow rockets to reach far away space destinations.";

				// Token: 0x0400970F RID: 38671
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Burns ",
					UI.FormatAsLink("Refined Iron", "IRON"),
					" and ",
					UI.FormatAsLink("Oxylite", "OXYROCK"),
					" to increase rocket exploration distance."
				});
			}

			// Token: 0x0200222B RID: 8747
			public class SPACEHEATER
			{
				// Token: 0x04009710 RID: 38672
				public static LocString NAME = UI.FormatAsLink("Space Heater", "SPACEHEATER");

				// Token: 0x04009711 RID: 38673
				public static LocString DESC = "A space heater will radiate heat for as long as it's powered.";

				// Token: 0x04009712 RID: 38674
				public static LocString EFFECT = "Radiates a moderate amount of " + UI.FormatAsLink("Heat", "HEAT") + ".";
			}

			// Token: 0x0200222C RID: 8748
			public class SPICEGRINDER
			{
				// Token: 0x04009713 RID: 38675
				public static LocString NAME = UI.FormatAsLink("Spice Grinder", "SPICEGRINDER");

				// Token: 0x04009714 RID: 38676
				public static LocString DESC = "Crushed seeds and other edibles make excellent meal-enhancing additives.";

				// Token: 0x04009715 RID: 38677
				public static LocString EFFECT = "Produces ingredients that add benefits to " + UI.FormatAsLink("foods", "FOOD") + " prepared at skilled cooking stations.";

				// Token: 0x04009716 RID: 38678
				public static LocString INGREDIENTHEADER = "Ingredients per 1000kcal:";
			}

			// Token: 0x0200222D RID: 8749
			public class STORAGELOCKER
			{
				// Token: 0x04009717 RID: 38679
				public static LocString NAME = UI.FormatAsLink("Storage Bin", "STORAGELOCKER");

				// Token: 0x04009718 RID: 38680
				public static LocString DESC = "Resources left on the floor become \"debris\" and lower decor when not put away.";

				// Token: 0x04009719 RID: 38681
				public static LocString EFFECT = "Stores the " + UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID") + " of your choosing.";
			}

			// Token: 0x0200222E RID: 8750
			public class STORAGELOCKERSMART
			{
				// Token: 0x0400971A RID: 38682
				public static LocString NAME = UI.FormatAsLink("Smart Storage Bin", "STORAGELOCKERSMART");

				// Token: 0x0400971B RID: 38683
				public static LocString DESC = "Smart storage bins can automate resource organization based on type and mass.";

				// Token: 0x0400971C RID: 38684
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores the ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" of your choosing.\n\nSends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" when bin is full."
				});

				// Token: 0x0400971D RID: 38685
				public static LocString LOGIC_PORT = "Full/Not Full";

				// Token: 0x0400971E RID: 38686
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when full";

				// Token: 0x0400971F RID: 38687
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x0200222F RID: 8751
			public class OBJECTDISPENSER
			{
				// Token: 0x04009720 RID: 38688
				public static LocString NAME = UI.FormatAsLink("Automatic Dispenser", "OBJECTDISPENSER");

				// Token: 0x04009721 RID: 38689
				public static LocString DESC = "Automatic dispensers will store and drop resources in small quantities.";

				// Token: 0x04009722 RID: 38690
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores any ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" delivered to it by Duplicants.\n\nDumps stored materials back into the world when it receives a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					"."
				});

				// Token: 0x04009723 RID: 38691
				public static LocString LOGIC_PORT = "Dump Trigger";

				// Token: 0x04009724 RID: 38692
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Dump all stored materials";

				// Token: 0x04009725 RID: 38693
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Store materials";
			}

			// Token: 0x02002230 RID: 8752
			public class LIQUIDRESERVOIR
			{
				// Token: 0x04009726 RID: 38694
				public static LocString NAME = UI.FormatAsLink("Liquid Reservoir", "LIQUIDRESERVOIR");

				// Token: 0x04009727 RID: 38695
				public static LocString DESC = "Reservoirs cannot receive manually delivered resources.";

				// Token: 0x04009728 RID: 38696
				public static LocString EFFECT = "Stores any " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " resources piped into it.";
			}

			// Token: 0x02002231 RID: 8753
			public class GASRESERVOIR
			{
				// Token: 0x04009729 RID: 38697
				public static LocString NAME = UI.FormatAsLink("Gas Reservoir", "GASRESERVOIR");

				// Token: 0x0400972A RID: 38698
				public static LocString DESC = "Reservoirs cannot receive manually delivered resources.";

				// Token: 0x0400972B RID: 38699
				public static LocString EFFECT = "Stores any " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " resources piped into it.";
			}

			// Token: 0x02002232 RID: 8754
			public class SMARTRESERVOIR
			{
				// Token: 0x0400972C RID: 38700
				public static LocString LOGIC_PORT = "Refill Parameters";

				// Token: 0x0400972D RID: 38701
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when reservoir is less than <b>Low Threshold</b> full, until <b>High Threshold</b> is reached again";

				// Token: 0x0400972E RID: 38702
				public static LocString LOGIC_PORT_INACTIVE = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " when reservoir is <b>High Threshold</b> full, until <b>Low Threshold</b> is reached again";

				// Token: 0x0400972F RID: 38703
				public static LocString ACTIVATE_TOOLTIP = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when reservoir is less than <b>{0}%</b> full, until it is <b>{1}% (High Threshold)</b> full";

				// Token: 0x04009730 RID: 38704
				public static LocString DEACTIVATE_TOOLTIP = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " when reservoir is <b>{0}%</b> full, until it is less than <b>{1}% (Low Threshold)</b> full";

				// Token: 0x04009731 RID: 38705
				public static LocString SIDESCREEN_TITLE = "Logic Activation Parameters";

				// Token: 0x04009732 RID: 38706
				public static LocString SIDESCREEN_ACTIVATE = "Low Threshold:";

				// Token: 0x04009733 RID: 38707
				public static LocString SIDESCREEN_DEACTIVATE = "High Threshold:";
			}

			// Token: 0x02002233 RID: 8755
			public class LIQUIDHEATER
			{
				// Token: 0x04009734 RID: 38708
				public static LocString NAME = UI.FormatAsLink("Liquid Tepidizer", "LIQUIDHEATER");

				// Token: 0x04009735 RID: 38709
				public static LocString DESC = "Tepidizers heat liquid which can kill waterborne germs.";

				// Token: 0x04009736 RID: 38710
				public static LocString EFFECT = "Warms large bodies of " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + ".\n\nMust be fully submerged.";
			}

			// Token: 0x02002234 RID: 8756
			public class SWITCH
			{
				// Token: 0x04009737 RID: 38711
				public static LocString NAME = UI.FormatAsLink("Switch", "SWITCH");

				// Token: 0x04009738 RID: 38712
				public static LocString DESC = "Switches can only affect buildings that come after them on a circuit.";

				// Token: 0x04009739 RID: 38713
				public static LocString EFFECT = "Turns " + UI.FormatAsLink("Power", "POWER") + " on or off.\n\nDoes not affect circuitry preceding the switch.";

				// Token: 0x0400973A RID: 38714
				public static LocString SIDESCREEN_TITLE = "Switch";

				// Token: 0x0400973B RID: 38715
				public static LocString TURN_ON = "Turn On";

				// Token: 0x0400973C RID: 38716
				public static LocString TURN_ON_TOOLTIP = "Turn On {Hotkey}";

				// Token: 0x0400973D RID: 38717
				public static LocString TURN_OFF = "Turn Off";

				// Token: 0x0400973E RID: 38718
				public static LocString TURN_OFF_TOOLTIP = "Turn Off {Hotkey}";
			}

			// Token: 0x02002235 RID: 8757
			public class LOGICPOWERRELAY
			{
				// Token: 0x0400973F RID: 38719
				public static LocString NAME = UI.FormatAsLink("Power Shutoff", "LOGICPOWERRELAY");

				// Token: 0x04009740 RID: 38720
				public static LocString DESC = "Automated systems save power and time by removing the need for Duplicant input.";

				// Token: 0x04009741 RID: 38721
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Connects to an ",
					UI.FormatAsLink("Automation", "LOGIC"),
					" grid to automatically turn ",
					UI.FormatAsLink("Power", "POWER"),
					" on or off.\n\nDoes not affect circuitry preceding the switch."
				});

				// Token: 0x04009742 RID: 38722
				public static LocString LOGIC_PORT = "Kill Power";

				// Token: 0x04009743 RID: 38723
				public static LocString LOGIC_PORT_ACTIVE = string.Concat(new string[]
				{
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					": Allow ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" through connected circuits"
				});

				// Token: 0x04009744 RID: 38724
				public static LocString LOGIC_PORT_INACTIVE = string.Concat(new string[]
				{
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					": Prevent ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" from flowing through connected circuits"
				});
			}

			// Token: 0x02002236 RID: 8758
			public class LOGICINTERASTEROIDSENDER
			{
				// Token: 0x04009745 RID: 38725
				public static LocString NAME = UI.FormatAsLink("Automation Broadcaster", "LOGICINTERASTEROIDSENDER");

				// Token: 0x04009746 RID: 38726
				public static LocString DESC = "Sends automation signals into space.";

				// Token: 0x04009747 RID: 38727
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" to an ",
					UI.FormatAsLink("Automation Receiver", "LOGICINTERASTEROIDRECEIVER"),
					" over vast distances in space.\n\nBoth the Automation Broadcaster and the Automation Receiver must be exposed to space to function."
				});

				// Token: 0x04009748 RID: 38728
				public static LocString DEFAULTNAME = "Unnamed Broadcaster";

				// Token: 0x04009749 RID: 38729
				public static LocString LOGIC_PORT = "Broadcasting Signal";

				// Token: 0x0400974A RID: 38730
				public static LocString LOGIC_PORT_ACTIVE = "Broadcasting: " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);

				// Token: 0x0400974B RID: 38731
				public static LocString LOGIC_PORT_INACTIVE = "Broadcasting: " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002237 RID: 8759
			public class LOGICINTERASTEROIDRECEIVER
			{
				// Token: 0x0400974C RID: 38732
				public static LocString NAME = UI.FormatAsLink("Automation Receiver", "LOGICINTERASTEROIDRECEIVER");

				// Token: 0x0400974D RID: 38733
				public static LocString DESC = "Receives automation signals from space.";

				// Token: 0x0400974E RID: 38734
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Receives a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" from an ",
					UI.FormatAsLink("Automation Broadcaster", "LOGICINTERASTEROIDSENDER"),
					" over vast distances in space.\n\nBoth the Automation Receiver and the Automation Broadcaster must be exposed to space to function."
				});

				// Token: 0x0400974F RID: 38735
				public static LocString LOGIC_PORT = "Receiving Signal";

				// Token: 0x04009750 RID: 38736
				public static LocString LOGIC_PORT_ACTIVE = "Receiving: " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);

				// Token: 0x04009751 RID: 38737
				public static LocString LOGIC_PORT_INACTIVE = "Receiving: " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002238 RID: 8760
			public class TEMPERATURECONTROLLEDSWITCH
			{
				// Token: 0x04009752 RID: 38738
				public static LocString NAME = UI.FormatAsLink("Thermo Switch", "TEMPERATURECONTROLLEDSWITCH");

				// Token: 0x04009753 RID: 38739
				public static LocString DESC = "Automated switches can be used to manage circuits in areas where Duplicants cannot enter.";

				// Token: 0x04009754 RID: 38740
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Automatically turns ",
					UI.FormatAsLink("Power", "POWER"),
					" on or off using ambient ",
					UI.FormatAsLink("Temperature", "HEAT"),
					".\n\nDoes not affect circuitry preceding the switch."
				});
			}

			// Token: 0x02002239 RID: 8761
			public class PRESSURESWITCHLIQUID
			{
				// Token: 0x04009755 RID: 38741
				public static LocString NAME = UI.FormatAsLink("Hydro Switch", "PRESSURESWITCHLIQUID");

				// Token: 0x04009756 RID: 38742
				public static LocString DESC = "A hydro switch shuts off power when the liquid pressure surrounding it surpasses the set threshold.";

				// Token: 0x04009757 RID: 38743
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Automatically turns ",
					UI.FormatAsLink("Power", "POWER"),
					" on or off using ambient ",
					UI.FormatAsLink("Liquid Pressure", "PRESSURE"),
					".\n\nDoes not affect circuitry preceding the switch.\n\nMust be submerged in ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					"."
				});
			}

			// Token: 0x0200223A RID: 8762
			public class PRESSURESWITCHGAS
			{
				// Token: 0x04009758 RID: 38744
				public static LocString NAME = UI.FormatAsLink("Atmo Switch", "PRESSURESWITCHGAS");

				// Token: 0x04009759 RID: 38745
				public static LocString DESC = "An atmo switch shuts off power when the air pressure surrounding it surpasses the set threshold.";

				// Token: 0x0400975A RID: 38746
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Automatically turns ",
					UI.FormatAsLink("Power", "POWER"),
					" on or off using ambient ",
					UI.FormatAsLink("Gas Pressure", "PRESSURE"),
					" .\n\nDoes not affect circuitry preceding the switch."
				});
			}

			// Token: 0x0200223B RID: 8763
			public class TILE
			{
				// Token: 0x0400975B RID: 38747
				public static LocString NAME = UI.FormatAsLink("Tile", "TILE");

				// Token: 0x0400975C RID: 38748
				public static LocString DESC = "Tile can be used to bridge gaps and get to unreachable areas.";

				// Token: 0x0400975D RID: 38749
				public static LocString EFFECT = "Used to build the walls and floors of rooms.\n\nIncreases Duplicant runspeed.";
			}

			// Token: 0x0200223C RID: 8764
			public class WALLTOILET
			{
				// Token: 0x0400975E RID: 38750
				public static LocString NAME = UI.FormatAsLink("Wall Toilet", "WALLTOILET");

				// Token: 0x0400975F RID: 38751
				public static LocString DESC = "Wall Toilets transmit fewer germs to Duplicants and require no emptying.";

				// Token: 0x04009760 RID: 38752
				public static LocString EFFECT = "Gives Duplicants a place to relieve themselves. Empties directly on the other side of the wall.\n\nSpreads very few " + UI.FormatAsLink("Germs", "DISEASE") + ".";
			}

			// Token: 0x0200223D RID: 8765
			public class WATERPURIFIER
			{
				// Token: 0x04009761 RID: 38753
				public static LocString NAME = UI.FormatAsLink("Water Sieve", "WATERPURIFIER");

				// Token: 0x04009762 RID: 38754
				public static LocString DESC = "Sieves cannot kill germs and pass any they receive into their waste and water output.";

				// Token: 0x04009763 RID: 38755
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Produces clean ",
					UI.FormatAsLink("Water", "WATER"),
					" from ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					" using ",
					UI.FormatAsLink("Sand", "SAND"),
					".\n\nProduces ",
					UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
					"."
				});
			}

			// Token: 0x0200223E RID: 8766
			public class DISTILLATIONCOLUMN
			{
				// Token: 0x04009764 RID: 38756
				public static LocString NAME = UI.FormatAsLink("Distillation Column", "DISTILLATIONCOLUMN");

				// Token: 0x04009765 RID: 38757
				public static LocString DESC = "Gets hot and steamy.";

				// Token: 0x04009766 RID: 38758
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Separates any ",
					UI.FormatAsLink("Contaminated Water", "DIRTYWATER"),
					" piped through it into ",
					UI.FormatAsLink("Steam", "STEAM"),
					" and ",
					UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
					"."
				});
			}

			// Token: 0x0200223F RID: 8767
			public class WIRE
			{
				// Token: 0x04009767 RID: 38759
				public static LocString NAME = UI.FormatAsLink("Wire", "WIRE");

				// Token: 0x04009768 RID: 38760
				public static LocString DESC = "Electrical wire is used to connect generators, batteries, and buildings.";

				// Token: 0x04009769 RID: 38761
				public static LocString EFFECT = "Connects buildings to " + UI.FormatAsLink("Power", "POWER") + " sources.\n\nCan be run through wall and floor tile.";
			}

			// Token: 0x02002240 RID: 8768
			public class WIREBRIDGE
			{
				// Token: 0x0400976A RID: 38762
				public static LocString NAME = UI.FormatAsLink("Wire Bridge", "WIREBRIDGE");

				// Token: 0x0400976B RID: 38763
				public static LocString DESC = "Splitting generators onto separate grids can prevent overloads and wasted electricity.";

				// Token: 0x0400976C RID: 38764
				public static LocString EFFECT = "Runs one wire section over another without joining them.\n\nCan be run through wall and floor tile.";
			}

			// Token: 0x02002241 RID: 8769
			public class HIGHWATTAGEWIRE
			{
				// Token: 0x0400976D RID: 38765
				public static LocString NAME = UI.FormatAsLink("Heavi-Watt Wire", "HIGHWATTAGEWIRE");

				// Token: 0x0400976E RID: 38766
				public static LocString DESC = "Higher wattage wire is used to avoid power overloads, particularly for strong generators.";

				// Token: 0x0400976F RID: 38767
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries more ",
					UI.FormatAsLink("Wattage", "POWER"),
					" than regular ",
					UI.FormatAsLink("Wire", "WIRE"),
					" without overloading.\n\nCannot be run through wall and floor tile."
				});
			}

			// Token: 0x02002242 RID: 8770
			public class WIREBRIDGEHIGHWATTAGE
			{
				// Token: 0x04009770 RID: 38768
				public static LocString NAME = UI.FormatAsLink("Heavi-Watt Joint Plate", "WIREBRIDGEHIGHWATTAGE");

				// Token: 0x04009771 RID: 38769
				public static LocString DESC = "Joint plates can run Heavi-Watt wires through walls without leaking gas or liquid.";

				// Token: 0x04009772 RID: 38770
				public static LocString EFFECT = "Allows " + UI.FormatAsLink("Heavi-Watt Wire", "HIGHWATTAGEWIRE") + " to be run through wall and floor tile.\n\nFunctions as regular tile.";
			}

			// Token: 0x02002243 RID: 8771
			public class WIREREFINED
			{
				// Token: 0x04009773 RID: 38771
				public static LocString NAME = UI.FormatAsLink("Conductive Wire", "WIREREFINED");

				// Token: 0x04009774 RID: 38772
				public static LocString DESC = "My Duplicants prefer the look of conductive wire to the regular raggedy stuff.";

				// Token: 0x04009775 RID: 38773
				public static LocString EFFECT = "Connects buildings to " + UI.FormatAsLink("Power", "POWER") + " sources.\n\nCan be run through wall and floor tile.";
			}

			// Token: 0x02002244 RID: 8772
			public class WIREREFINEDBRIDGE
			{
				// Token: 0x04009776 RID: 38774
				public static LocString NAME = UI.FormatAsLink("Conductive Wire Bridge", "WIREREFINEDBRIDGE");

				// Token: 0x04009777 RID: 38775
				public static LocString DESC = "Splitting generators onto separate systems can prevent overloads and wasted electricity.";

				// Token: 0x04009778 RID: 38776
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries more ",
					UI.FormatAsLink("Wattage", "POWER"),
					" than a regular ",
					UI.FormatAsLink("Wire Bridge", "WIREBRIDGE"),
					" without overloading.\n\nRuns one wire section over another without joining them.\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x02002245 RID: 8773
			public class WIREREFINEDHIGHWATTAGE
			{
				// Token: 0x04009779 RID: 38777
				public static LocString NAME = UI.FormatAsLink("Heavi-Watt Conductive Wire", "WIREREFINEDHIGHWATTAGE");

				// Token: 0x0400977A RID: 38778
				public static LocString DESC = "Higher wattage wire is used to avoid power overloads, particularly for strong generators.";

				// Token: 0x0400977B RID: 38779
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries more ",
					UI.FormatAsLink("Wattage", "POWER"),
					" than regular ",
					UI.FormatAsLink("Wire", "WIRE"),
					" without overloading.\n\nCannot be run through wall and floor tile."
				});
			}

			// Token: 0x02002246 RID: 8774
			public class WIREREFINEDBRIDGEHIGHWATTAGE
			{
				// Token: 0x0400977C RID: 38780
				public static LocString NAME = UI.FormatAsLink("Heavi-Watt Conductive Joint Plate", "WIREREFINEDBRIDGEHIGHWATTAGE");

				// Token: 0x0400977D RID: 38781
				public static LocString DESC = "Joint plates can run Heavi-Watt wires through walls without leaking gas or liquid.";

				// Token: 0x0400977E RID: 38782
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries more ",
					UI.FormatAsLink("Wattage", "POWER"),
					" than a regular ",
					UI.FormatAsLink("Heavi-Watt Joint Plate", "WIREBRIDGEHIGHWATTAGE"),
					" without overloading.\n\nAllows ",
					UI.FormatAsLink("Heavi-Watt Wire", "HIGHWATTAGEWIRE"),
					" to be run through wall and floor tile."
				});
			}

			// Token: 0x02002247 RID: 8775
			public class HANDSANITIZER
			{
				// Token: 0x0400977F RID: 38783
				public static LocString NAME = UI.FormatAsLink("Hand Sanitizer", "HANDSANITIZER");

				// Token: 0x04009780 RID: 38784
				public static LocString DESC = "Hand sanitizers kill germs more effectively than wash basins.";

				// Token: 0x04009781 RID: 38785
				public static LocString EFFECT = "Removes most " + UI.FormatAsLink("Germs", "DISEASE") + " from Duplicants.\n\nGerm-covered Duplicants use Hand Sanitizers when passing by in the selected direction.";
			}

			// Token: 0x02002248 RID: 8776
			public class WASHBASIN
			{
				// Token: 0x04009782 RID: 38786
				public static LocString NAME = UI.FormatAsLink("Wash Basin", "WASHBASIN");

				// Token: 0x04009783 RID: 38787
				public static LocString DESC = "Germ spread can be reduced by building these where Duplicants often get dirty.";

				// Token: 0x04009784 RID: 38788
				public static LocString EFFECT = "Removes some " + UI.FormatAsLink("Germs", "DISEASE") + " from Duplicants.\n\nGerm-covered Duplicants use Wash Basins when passing by in the selected direction.";
			}

			// Token: 0x02002249 RID: 8777
			public class WASHSINK
			{
				// Token: 0x04009785 RID: 38789
				public static LocString NAME = UI.FormatAsLink("Sink", "WASHSINK");

				// Token: 0x04009786 RID: 38790
				public static LocString DESC = "Sinks are plumbed and do not need to be manually emptied or refilled.";

				// Token: 0x04009787 RID: 38791
				public static LocString EFFECT = "Removes " + UI.FormatAsLink("Germs", "DISEASE") + " from Duplicants.\n\nGerm-covered Duplicants use Sinks when passing by in the selected direction.";
			}

			// Token: 0x0200224A RID: 8778
			public class DECONTAMINATIONSHOWER
			{
				// Token: 0x04009788 RID: 38792
				public static LocString NAME = UI.FormatAsLink("Decontamination Shower", "DECONTAMINATIONSHOWER");

				// Token: 0x04009789 RID: 38793
				public static LocString DESC = "Don't forget to decontaminate behind your ears.";

				// Token: 0x0400978A RID: 38794
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Water", "WATER"),
					" to remove ",
					UI.FormatAsLink("Germs", "DISEASE"),
					" and ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					".\n\nDecontaminates both Duplicants and their ",
					UI.FormatAsLink("Clothing", "EQUIPMENT"),
					"."
				});
			}

			// Token: 0x0200224B RID: 8779
			public class TILEPOI
			{
				// Token: 0x0400978B RID: 38795
				public static LocString NAME = UI.FormatAsLink("Tile", "TILEPOI");

				// Token: 0x0400978C RID: 38796
				public static LocString DESC = "";

				// Token: 0x0400978D RID: 38797
				public static LocString EFFECT = "Used to build the walls and floor of rooms.";
			}

			// Token: 0x0200224C RID: 8780
			public class POLYMERIZER
			{
				// Token: 0x0400978E RID: 38798
				public static LocString NAME = UI.FormatAsLink("Polymer Press", "POLYMERIZER");

				// Token: 0x0400978F RID: 38799
				public static LocString DESC = "Plastic can be used to craft unique buildings and goods.";

				// Token: 0x04009790 RID: 38800
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Petroleum", "PETROLEUM"),
					" into raw ",
					UI.FormatAsLink("Plastic", "POLYPROPYLENE"),
					"."
				});
			}

			// Token: 0x0200224D RID: 8781
			public class DIRECTIONALWORLDPUMPLIQUID
			{
				// Token: 0x04009791 RID: 38801
				public static LocString NAME = UI.FormatAsLink("Liquid Channel", "DIRECTIONALWORLDPUMPLIQUID");

				// Token: 0x04009792 RID: 38802
				public static LocString DESC = "Channels move more volume than pumps and require no power, but need sufficient pressure to function.";

				// Token: 0x04009793 RID: 38803
				public static LocString EFFECT = "Directionally moves large volumes of " + UI.FormatAsLink("LIQUID", "ELEMENTS_LIQUID") + " through a channel.\n\nCan be used as floor tile and rotated before construction.";
			}

			// Token: 0x0200224E RID: 8782
			public class STEAMTURBINE
			{
				// Token: 0x04009794 RID: 38804
				public static LocString NAME = UI.FormatAsLink("[DEPRECATED] Steam Turbine", "STEAMTURBINE");

				// Token: 0x04009795 RID: 38805
				public static LocString DESC = "Useful for converting the geothermal energy of magma into usable power.";

				// Token: 0x04009796 RID: 38806
				public static LocString EFFECT = string.Concat(new string[]
				{
					"THIS BUILDING HAS BEEN DEPRECATED AND CANNOT BE BUILT.\n\nGenerates exceptional electrical ",
					UI.FormatAsLink("Power", "POWER"),
					" using pressurized, ",
					UI.FormatAsLink("Scalding", "HEAT"),
					" ",
					UI.FormatAsLink("Steam", "STEAM"),
					".\n\nOutputs significantly cooler ",
					UI.FormatAsLink("Steam", "STEAM"),
					" than it receives.\n\nAir pressure beneath this building must be higher than pressure above for air to flow."
				});
			}

			// Token: 0x0200224F RID: 8783
			public class STEAMTURBINE2
			{
				// Token: 0x04009797 RID: 38807
				public static LocString NAME = UI.FormatAsLink("Steam Turbine", "STEAMTURBINE2");

				// Token: 0x04009798 RID: 38808
				public static LocString DESC = "Useful for converting the geothermal energy into usable power.";

				// Token: 0x04009799 RID: 38809
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Draws in ",
					UI.FormatAsLink("Steam", "STEAM"),
					" from the tiles directly below the machine's foundation and uses it to generate electrical ",
					UI.FormatAsLink("Power", "POWER"),
					".\n\nOutputs ",
					UI.FormatAsLink("Water", "WATER"),
					"."
				});

				// Token: 0x0400979A RID: 38810
				public static LocString HEAT_SOURCE = "Power Generation Waste";
			}

			// Token: 0x02002250 RID: 8784
			public class STEAMENGINE
			{
				// Token: 0x0400979B RID: 38811
				public static LocString NAME = UI.FormatAsLink("Steam Engine", "STEAMENGINE");

				// Token: 0x0400979C RID: 38812
				public static LocString DESC = "Rockets can be used to send Duplicants into space and retrieve rare resources.";

				// Token: 0x0400979D RID: 38813
				public static LocString EFFECT = "Utilizes " + UI.FormatAsLink("Steam", "STEAM") + " to propel rockets for space exploration.\n\nThe engine of a rocket must be built first before more rocket modules may be added.";
			}

			// Token: 0x02002251 RID: 8785
			public class STEAMENGINECLUSTER
			{
				// Token: 0x0400979E RID: 38814
				public static LocString NAME = UI.FormatAsLink("Steam Engine", "STEAMENGINECLUSTER");

				// Token: 0x0400979F RID: 38815
				public static LocString DESC = "Rockets can be used to send Duplicants into space and retrieve rare resources.";

				// Token: 0x040097A0 RID: 38816
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Utilizes ",
					UI.FormatAsLink("Steam", "STEAM"),
					" to propel rockets for space exploration.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nOnce the engine has been built, more rocket modules can be added."
				});
			}

			// Token: 0x02002252 RID: 8786
			public class SOLARPANEL
			{
				// Token: 0x040097A1 RID: 38817
				public static LocString NAME = UI.FormatAsLink("Solar Panel", "SOLARPANEL");

				// Token: 0x040097A2 RID: 38818
				public static LocString DESC = "Solar panels convert high intensity sunlight into power and produce zero waste.";

				// Token: 0x040097A3 RID: 38819
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Sunlight", "LIGHT"),
					" into electrical ",
					UI.FormatAsLink("Power", "POWER"),
					".\n\nMust be exposed to space."
				});
			}

			// Token: 0x02002253 RID: 8787
			public class COMETDETECTOR
			{
				// Token: 0x040097A4 RID: 38820
				public static LocString NAME = UI.FormatAsLink("Space Scanner", "COMETDETECTOR");

				// Token: 0x040097A5 RID: 38821
				public static LocString DESC = "Networks of many scanners will scan more efficiently than one on its own.";

				// Token: 0x040097A6 RID: 38822
				public static LocString EFFECT = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " to its automation circuit when it detects incoming objects from space.\n\nCan be configured to detect incoming meteor showers or returning space rockets.";
			}

			// Token: 0x02002254 RID: 8788
			public class OILREFINERY
			{
				// Token: 0x040097A7 RID: 38823
				public static LocString NAME = UI.FormatAsLink("Oil Refinery", "OILREFINERY");

				// Token: 0x040097A8 RID: 38824
				public static LocString DESC = "Petroleum can only be produced from the refinement of crude oil.";

				// Token: 0x040097A9 RID: 38825
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Crude Oil", "CRUDEOIL"),
					" into ",
					UI.FormatAsLink("Petroleum", "PETROLEUM"),
					" and ",
					UI.FormatAsLink("Natural Gas", "METHANE"),
					"."
				});
			}

			// Token: 0x02002255 RID: 8789
			public class OILWELLCAP
			{
				// Token: 0x040097AA RID: 38826
				public static LocString NAME = UI.FormatAsLink("Oil Well", "OILWELLCAP");

				// Token: 0x040097AB RID: 38827
				public static LocString DESC = "Water pumped into an oil reservoir cannot be recovered.";

				// Token: 0x040097AC RID: 38828
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Extracts ",
					UI.FormatAsLink("Crude Oil", "CRUDEOIL"),
					" using clean ",
					UI.FormatAsLink("Water", "WATER"),
					".\n\nMust be built atop an ",
					UI.FormatAsLink("Oil Reservoir", "OIL_WELL"),
					"."
				});
			}

			// Token: 0x02002256 RID: 8790
			public class METALREFINERY
			{
				// Token: 0x040097AD RID: 38829
				public static LocString NAME = UI.FormatAsLink("Metal Refinery", "METALREFINERY");

				// Token: 0x040097AE RID: 38830
				public static LocString DESC = "Refined metals are necessary to build advanced electronics and technologies.";

				// Token: 0x040097AF RID: 38831
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsLink("Refined Metals", "REFINEDMETAL"),
					" from raw ",
					UI.FormatAsLink("Metal Ore", "RAWMETAL"),
					".\n\nSignificantly ",
					UI.FormatAsLink("Heats", "HEAT"),
					" and outputs the ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" piped into it.\n\nDuplicants will not fabricate items unless recipes are queued."
				});

				// Token: 0x040097B0 RID: 38832
				public static LocString RECIPE_DESCRIPTION = "Extracts pure {0} from {1}.";
			}

			// Token: 0x02002257 RID: 8791
			public class GLASSFORGE
			{
				// Token: 0x040097B1 RID: 38833
				public static LocString NAME = UI.FormatAsLink("Glass Forge", "GLASSFORGE");

				// Token: 0x040097B2 RID: 38834
				public static LocString DESC = "Glass can be used to construct window tile.";

				// Token: 0x040097B3 RID: 38835
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsLink("Molten Glass", "MOLTENGLASS"),
					" from raw ",
					UI.FormatAsLink("Sand", "SAND"),
					".\n\nOutputs ",
					UI.FormatAsLink("High Temperature", "HEAT"),
					" ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					".\n\nDuplicants will not fabricate items unless recipes are queued."
				});

				// Token: 0x040097B4 RID: 38836
				public static LocString RECIPE_DESCRIPTION = "Extracts pure {0} from {1}.";
			}

			// Token: 0x02002258 RID: 8792
			public class ROCKCRUSHER
			{
				// Token: 0x040097B5 RID: 38837
				public static LocString NAME = UI.FormatAsLink("Rock Crusher", "ROCKCRUSHER");

				// Token: 0x040097B6 RID: 38838
				public static LocString DESC = "Rock Crushers loosen nuggets from raw ore and can process many different resources.";

				// Token: 0x040097B7 RID: 38839
				public static LocString EFFECT = "Inefficiently produces refined materials from raw resources.\n\nDuplicants will not fabricate items unless recipes are queued.";

				// Token: 0x040097B8 RID: 38840
				public static LocString RECIPE_DESCRIPTION = "Crushes {0} into {1}.";

				// Token: 0x040097B9 RID: 38841
				public static LocString METAL_RECIPE_DESCRIPTION = "Crushes {1} into " + UI.FormatAsLink("Sand", "SAND") + " and pure {0}.";

				// Token: 0x040097BA RID: 38842
				public static LocString LIME_RECIPE_DESCRIPTION = "Crushes {1} into {0}";

				// Token: 0x040097BB RID: 38843
				public static LocString LIME_FROM_LIMESTONE_RECIPE_DESCRIPTION = "Crushes {0} into {1} and a small amount of pure {2}";
			}

			// Token: 0x02002259 RID: 8793
			public class SLUDGEPRESS
			{
				// Token: 0x040097BC RID: 38844
				public static LocString NAME = UI.FormatAsLink("Sludge Press", "SLUDGEPRESS");

				// Token: 0x040097BD RID: 38845
				public static LocString DESC = "What Duplicant doesn't love playing with mud?";

				// Token: 0x040097BE RID: 38846
				public static LocString EFFECT = "Separates " + UI.FormatAsLink("Mud", "MUD") + " and other sludges into their base elements.\n\nDuplicants will not fabricate items unless recipes are queued.";

				// Token: 0x040097BF RID: 38847
				public static LocString RECIPE_DESCRIPTION = "Separates {0} into its base elements.";
			}

			// Token: 0x0200225A RID: 8794
			public class SUPERMATERIALREFINERY
			{
				// Token: 0x040097C0 RID: 38848
				public static LocString NAME = UI.FormatAsLink("Molecular Forge", "SUPERMATERIALREFINERY");

				// Token: 0x040097C1 RID: 38849
				public static LocString DESC = "Rare materials can be procured through rocket missions into space.";

				// Token: 0x040097C2 RID: 38850
				public static LocString EFFECT = "Processes " + UI.FormatAsLink("Rare Materials", "RAREMATERIALS") + " into advanced industrial goods.\n\nRare materials can be retrieved from space missions.\n\nDuplicants will not fabricate items unless recipes are queued.";

				// Token: 0x040097C3 RID: 38851
				public static LocString SUPERCOOLANT_RECIPE_DESCRIPTION = "Super Coolant is an industrial grade " + UI.FormatAsLink("Fullerene", "FULLERENE") + " coolant.";

				// Token: 0x040097C4 RID: 38852
				public static LocString SUPERINSULATOR_RECIPE_DESCRIPTION = string.Concat(new string[]
				{
					"Insulation reduces ",
					UI.FormatAsLink("Heat Transfer", "HEAT"),
					" and is composed of recrystallized ",
					UI.FormatAsLink("Abyssalite", "KATAIRITE"),
					"."
				});

				// Token: 0x040097C5 RID: 38853
				public static LocString TEMPCONDUCTORSOLID_RECIPE_DESCRIPTION = "Thermium is an industrial metal alloy formulated to maximize " + UI.FormatAsLink("Heat Transfer", "HEAT") + " and thermal dispersion.";

				// Token: 0x040097C6 RID: 38854
				public static LocString VISCOGEL_RECIPE_DESCRIPTION = "Visco-Gel is a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " polymer with high surface tension.";

				// Token: 0x040097C7 RID: 38855
				public static LocString YELLOWCAKE_RECIPE_DESCRIPTION = "Yellowcake is a " + UI.FormatAsLink("Solid Material", "ELEMENTS_SOLID") + " used in uranium enrichment.";

				// Token: 0x040097C8 RID: 38856
				public static LocString FULLERENE_RECIPE_DESCRIPTION = string.Concat(new string[]
				{
					"Fullerene is a ",
					UI.FormatAsLink("Solid Material", "ELEMENTS_SOLID"),
					" used in the production of ",
					UI.FormatAsLink("Super Coolant", "SUPERCOOLANT"),
					"."
				});
			}

			// Token: 0x0200225B RID: 8795
			public class THERMALBLOCK
			{
				// Token: 0x040097C9 RID: 38857
				public static LocString NAME = UI.FormatAsLink("Tempshift Plate", "THERMALBLOCK");

				// Token: 0x040097CA RID: 38858
				public static LocString DESC = "The thermal properties of construction materials determine their heat retention.";

				// Token: 0x040097CB RID: 38859
				public static LocString EFFECT = "Accelerates or buffers " + UI.FormatAsLink("Heat", "HEAT") + " dispersal based on the construction material used.\n\nHas a small area of effect.";
			}

			// Token: 0x0200225C RID: 8796
			public class POWERCONTROLSTATION
			{
				// Token: 0x040097CC RID: 38860
				public static LocString NAME = UI.FormatAsLink("Power Control Station", "POWERCONTROLSTATION");

				// Token: 0x040097CD RID: 38861
				public static LocString DESC = "Only one Duplicant may be assigned to a station at a time.";

				// Token: 0x040097CE RID: 38862
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsLink("Microchip", "POWER_STATION_TOOLS"),
					" to increase the ",
					UI.FormatAsLink("Power", "POWER"),
					" output of generators.\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Tune Up", "TECHNICALS2"),
					" trait.\n\nThis building is a necessary component of the Power Plant room."
				});
			}

			// Token: 0x0200225D RID: 8797
			public class FARMSTATION
			{
				// Token: 0x040097CF RID: 38863
				public static LocString NAME = UI.FormatAsLink("Farm Station", "FARMSTATION");

				// Token: 0x040097D0 RID: 38864
				public static LocString DESC = "This station only has an effect on crops grown within the same room.";

				// Token: 0x040097D1 RID: 38865
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsLink("Micronutrient Fertilizer", "FARM_STATION_TOOLS"),
					" to increase ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" growth rates.\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Crop Tending", "FARMING2"),
					" trait.\n\nThis building is a necessary component of the Greenhouse room."
				});
			}

			// Token: 0x0200225E RID: 8798
			public class FISHDELIVERYPOINT
			{
				// Token: 0x040097D2 RID: 38866
				public static LocString NAME = UI.FormatAsLink("Fish Release", "FISHDELIVERYPOINT");

				// Token: 0x040097D3 RID: 38867
				public static LocString DESC = "A fish release must be built above liquid to prevent released fish from suffocating.";

				// Token: 0x040097D4 RID: 38868
				public static LocString EFFECT = "Releases trapped " + UI.FormatAsLink("Pacu", "PACU") + " back into the world.\n\nCan be used multiple times.";
			}

			// Token: 0x0200225F RID: 8799
			public class FISHFEEDER
			{
				// Token: 0x040097D5 RID: 38869
				public static LocString NAME = UI.FormatAsLink("Fish Feeder", "FISHFEEDER");

				// Token: 0x040097D6 RID: 38870
				public static LocString DESC = "Build this feeder above a body of water to feed the fish within.";

				// Token: 0x040097D7 RID: 38871
				public static LocString EFFECT = "Automatically dispenses stored " + UI.FormatAsLink("Critter", "CRITTERS") + " food into the area below.\n\nDispenses once per day.";
			}

			// Token: 0x02002260 RID: 8800
			public class FISHTRAP
			{
				// Token: 0x040097D8 RID: 38872
				public static LocString NAME = UI.FormatAsLink("Fish Trap", "FISHTRAP");

				// Token: 0x040097D9 RID: 38873
				public static LocString DESC = "Trapped fish will automatically be bagged for transport.";

				// Token: 0x040097DA RID: 38874
				public static LocString EFFECT = "Attracts and traps swimming " + UI.FormatAsLink("Pacu", "PACU") + ".\n\nSingle use.";
			}

			// Token: 0x02002261 RID: 8801
			public class RANCHSTATION
			{
				// Token: 0x040097DB RID: 38875
				public static LocString NAME = UI.FormatAsLink("Grooming Station", "RANCHSTATION");

				// Token: 0x040097DC RID: 38876
				public static LocString DESC = "Grooming critters make them look nice, smell pretty, feel happy, and produce more.";

				// Token: 0x040097DD RID: 38877
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows the assigned ",
					UI.FormatAsLink("Rancher", "RANCHER"),
					" to care for ",
					UI.FormatAsLink("Critters", "CRITTERS"),
					".\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Critter Ranching", "RANCHING1"),
					" skill.\n\nThis building is a necessary component of the Stable room."
				});
			}

			// Token: 0x02002262 RID: 8802
			public class MACHINESHOP
			{
				// Token: 0x040097DE RID: 38878
				public static LocString NAME = UI.FormatAsLink("Mechanics Station", "MACHINESHOP");

				// Token: 0x040097DF RID: 38879
				public static LocString DESC = "Duplicants will only improve the efficiency of buildings in the same room as this station.";

				// Token: 0x040097E0 RID: 38880
				public static LocString EFFECT = "Allows the assigned " + UI.FormatAsLink("Engineer", "MACHINE_TECHNICIAN") + " to improve building production efficiency.\n\nThis building is a necessary component of the Machine Shop room.";
			}

			// Token: 0x02002263 RID: 8803
			public class LOGICWIRE
			{
				// Token: 0x040097E1 RID: 38881
				public static LocString NAME = UI.FormatAsLink("Automation Wire", "LOGICWIRE");

				// Token: 0x040097E2 RID: 38882
				public static LocString DESC = "Automation wire is used to connect building ports to automation gates.";

				// Token: 0x040097E3 RID: 38883
				public static LocString EFFECT = "Connects buildings to " + UI.FormatAsLink("Sensors", "LOGIC") + ".\n\nCan be run through wall and floor tile.";
			}

			// Token: 0x02002264 RID: 8804
			public class LOGICRIBBON
			{
				// Token: 0x040097E4 RID: 38884
				public static LocString NAME = UI.FormatAsLink("Automation Ribbon", "LOGICRIBBON");

				// Token: 0x040097E5 RID: 38885
				public static LocString DESC = "Logic ribbons use significantly less space to carry multiple automation signals.";

				// Token: 0x040097E6 RID: 38886
				public static LocString EFFECT = string.Concat(new string[]
				{
					"A 4-Bit ",
					BUILDINGS.PREFABS.LOGICWIRE.NAME,
					" which can carry up to four automation signals.\n\nUse a ",
					UI.FormatAsLink("Ribbon Writer", "LOGICRIBBONWRITER"),
					" to output to multiple Bits, and a ",
					UI.FormatAsLink("Ribbon Reader", "LOGICRIBBONREADER"),
					" to input from multiple Bits."
				});
			}

			// Token: 0x02002265 RID: 8805
			public class LOGICWIREBRIDGE
			{
				// Token: 0x040097E7 RID: 38887
				public static LocString NAME = UI.FormatAsLink("Automation Wire Bridge", "LOGICWIREBRIDGE");

				// Token: 0x040097E8 RID: 38888
				public static LocString DESC = "Wire bridges allow multiple automation grids to exist in a small area without connecting.";

				// Token: 0x040097E9 RID: 38889
				public static LocString EFFECT = "Runs one " + UI.FormatAsLink("Automation Wire", "LOGICWIRE") + " section over another without joining them.\n\nCan be run through wall and floor tile.";

				// Token: 0x040097EA RID: 38890
				public static LocString LOGIC_PORT = "Transmit Signal";

				// Token: 0x040097EB RID: 38891
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Pass through the " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);

				// Token: 0x040097EC RID: 38892
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Pass through the " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002266 RID: 8806
			public class LOGICRIBBONBRIDGE
			{
				// Token: 0x040097ED RID: 38893
				public static LocString NAME = UI.FormatAsLink("Automation Ribbon Bridge", "LOGICRIBBONBRIDGE");

				// Token: 0x040097EE RID: 38894
				public static LocString DESC = "Wire bridges allow multiple automation grids to exist in a small area without connecting.";

				// Token: 0x040097EF RID: 38895
				public static LocString EFFECT = "Runs one " + UI.FormatAsLink("Automation Ribbon", "LOGICRIBBON") + " section over another without joining them.\n\nCan be run through wall and floor tile.";

				// Token: 0x040097F0 RID: 38896
				public static LocString LOGIC_PORT = "Transmit Signal";

				// Token: 0x040097F1 RID: 38897
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Pass through the " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);

				// Token: 0x040097F2 RID: 38898
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Pass through the " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002267 RID: 8807
			public class LOGICGATEAND
			{
				// Token: 0x040097F3 RID: 38899
				public static LocString NAME = UI.FormatAsLink("AND Gate", "LOGICGATEAND");

				// Token: 0x040097F4 RID: 38900
				public static LocString DESC = "This gate outputs a Green Signal when both its inputs are receiving Green Signals at the same time.";

				// Token: 0x040097F5 RID: 38901
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Outputs a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" when both Input A <b>AND</b> Input B are receiving ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					".\n\nOutputs a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when even one Input is receiving ",
					UI.FormatAsAutomationState("Red", UI.AutomationState.Standby),
					"."
				});

				// Token: 0x040097F6 RID: 38902
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x040097F7 RID: 38903
				public static LocString OUTPUT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if both Inputs are receiving " + UI.FormatAsAutomationState("Green", UI.AutomationState.Active);

				// Token: 0x040097F8 RID: 38904
				public static LocString OUTPUT_INACTIVE = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " if any Input is receiving " + UI.FormatAsAutomationState("Red", UI.AutomationState.Standby);
			}

			// Token: 0x02002268 RID: 8808
			public class LOGICGATEOR
			{
				// Token: 0x040097F9 RID: 38905
				public static LocString NAME = UI.FormatAsLink("OR Gate", "LOGICGATEOR");

				// Token: 0x040097FA RID: 38906
				public static LocString DESC = "This gate outputs a Green Signal if receiving one or more Green Signals.";

				// Token: 0x040097FB RID: 38907
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Outputs a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if at least one of Input A <b>OR</b> Input B is receiving ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					".\n\nOutputs a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when neither Input A or Input B are receiving ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					"."
				});

				// Token: 0x040097FC RID: 38908
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x040097FD RID: 38909
				public static LocString OUTPUT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if any Input is receiving " + UI.FormatAsAutomationState("Green", UI.AutomationState.Active);

				// Token: 0x040097FE RID: 38910
				public static LocString OUTPUT_INACTIVE = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " if both Inputs are receiving " + UI.FormatAsAutomationState("Red", UI.AutomationState.Standby);
			}

			// Token: 0x02002269 RID: 8809
			public class LOGICGATENOT
			{
				// Token: 0x040097FF RID: 38911
				public static LocString NAME = UI.FormatAsLink("NOT Gate", "LOGICGATENOT");

				// Token: 0x04009800 RID: 38912
				public static LocString DESC = "This gate reverses automation signals, turning a Green Signal into a Red Signal and vice versa.";

				// Token: 0x04009801 RID: 38913
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Outputs a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the Input is receiving a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					".\n\nOutputs a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when its Input is receiving a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					"."
				});

				// Token: 0x04009802 RID: 38914
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x04009803 RID: 38915
				public static LocString OUTPUT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if receiving " + UI.FormatAsAutomationState("Red", UI.AutomationState.Standby);

				// Token: 0x04009804 RID: 38916
				public static LocString OUTPUT_INACTIVE = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " if receiving " + UI.FormatAsAutomationState("Green", UI.AutomationState.Active);
			}

			// Token: 0x0200226A RID: 8810
			public class LOGICGATEXOR
			{
				// Token: 0x04009805 RID: 38917
				public static LocString NAME = UI.FormatAsLink("XOR Gate", "LOGICGATEXOR");

				// Token: 0x04009806 RID: 38918
				public static LocString DESC = "This gate outputs a Green Signal if exactly one of its Inputs is receiving a Green Signal.";

				// Token: 0x04009807 RID: 38919
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Outputs a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if exactly one of its Inputs is receiving ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					".\n\nOutputs a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" if both or neither Inputs are receiving a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					"."
				});

				// Token: 0x04009808 RID: 38920
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x04009809 RID: 38921
				public static LocString OUTPUT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if exactly one of its Inputs is receiving " + UI.FormatAsAutomationState("Green", UI.AutomationState.Active);

				// Token: 0x0400980A RID: 38922
				public static LocString OUTPUT_INACTIVE = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " if both Input signals match (any color)";
			}

			// Token: 0x0200226B RID: 8811
			public class LOGICGATEBUFFER
			{
				// Token: 0x0400980B RID: 38923
				public static LocString NAME = UI.FormatAsLink("BUFFER Gate", "LOGICGATEBUFFER");

				// Token: 0x0400980C RID: 38924
				public static LocString DESC = "This gate continues outputting a Green Signal for a short time after the gate stops receiving a Green Signal.";

				// Token: 0x0400980D RID: 38925
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Outputs a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the Input is receiving a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					".\n\nContinues sending a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" for an amount of buffer time after the Input receives a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					"."
				});

				// Token: 0x0400980E RID: 38926
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x0400980F RID: 38927
				public static LocString OUTPUT_ACTIVE = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" while receiving ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					". After receiving ",
					UI.FormatAsAutomationState("Red", UI.AutomationState.Standby),
					", will continue sending ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					" until the timer has expired"
				});

				// Token: 0x04009810 RID: 38928
				public static LocString OUTPUT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ".";
			}

			// Token: 0x0200226C RID: 8812
			public class LOGICGATEFILTER
			{
				// Token: 0x04009811 RID: 38929
				public static LocString NAME = UI.FormatAsLink("FILTER Gate", "LOGICGATEFILTER");

				// Token: 0x04009812 RID: 38930
				public static LocString DESC = "This gate only lets a Green Signal through if its Input has received a Green Signal that lasted longer than the selected filter time.";

				// Token: 0x04009813 RID: 38931
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Only lets a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" through if the Input has received a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" for longer than the selected filter time.\n\nWill continue outputting a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" if the ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" did not last long enough."
				});

				// Token: 0x04009814 RID: 38932
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x04009815 RID: 38933
				public static LocString OUTPUT_ACTIVE = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" after receiving ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					" for longer than the selected filter timer"
				});

				// Token: 0x04009816 RID: 38934
				public static LocString OUTPUT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ".";
			}

			// Token: 0x0200226D RID: 8813
			public class LOGICMEMORY
			{
				// Token: 0x04009817 RID: 38935
				public static LocString NAME = UI.FormatAsLink("Memory Toggle", "LOGICMEMORY");

				// Token: 0x04009818 RID: 38936
				public static LocString DESC = "A Memory stores a Green Signal received in the Set Port (S) until the Reset Port (R) receives a Green Signal.";

				// Token: 0x04009819 RID: 38937
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Contains an internal Memory, and will output whatever signal is stored in that Memory.\n\nSignals sent to the Inputs <i>only</i> affect the Memory, and do not pass through to the Output. \n\nSending a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" to the Set Port (S) will set the memory to ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					". \n\nSending a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" to the Reset Port (R) will reset the memory back to ",
					UI.FormatAsAutomationState("Red", UI.AutomationState.Standby),
					"."
				});

				// Token: 0x0400981A RID: 38938
				public static LocString STATUS_ITEM_VALUE = "Current Value: {0}";

				// Token: 0x0400981B RID: 38939
				public static LocString READ_PORT = "MEMORY OUTPUT";

				// Token: 0x0400981C RID: 38940
				public static LocString READ_PORT_ACTIVE = "Outputs a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the internal Memory is set to " + UI.FormatAsAutomationState("Green", UI.AutomationState.Active);

				// Token: 0x0400981D RID: 38941
				public static LocString READ_PORT_INACTIVE = "Outputs a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " if the internal Memory is set to " + UI.FormatAsAutomationState("Red", UI.AutomationState.Standby);

				// Token: 0x0400981E RID: 38942
				public static LocString SET_PORT = "SET PORT (S)";

				// Token: 0x0400981F RID: 38943
				public static LocString SET_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Set the internal Memory to " + UI.FormatAsAutomationState("Green", UI.AutomationState.Active);

				// Token: 0x04009820 RID: 38944
				public static LocString SET_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": No effect";

				// Token: 0x04009821 RID: 38945
				public static LocString RESET_PORT = "RESET PORT (R)";

				// Token: 0x04009822 RID: 38946
				public static LocString RESET_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Reset the internal Memory to " + UI.FormatAsAutomationState("Red", UI.AutomationState.Standby);

				// Token: 0x04009823 RID: 38947
				public static LocString RESET_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": No effect";
			}

			// Token: 0x0200226E RID: 8814
			public class LOGICGATEMULTIPLEXER
			{
				// Token: 0x04009824 RID: 38948
				public static LocString NAME = UI.FormatAsLink("Signal Selector", "LOGICGATEMULTIPLEXER");

				// Token: 0x04009825 RID: 38949
				public static LocString DESC = "Signal Selectors can be used to select which automation signal is relevant to pass through to a given circuit";

				// Token: 0x04009826 RID: 38950
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Select which one of four Input signals should be sent out the Output, using Control Inputs.\n\nSend a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" to the two Control Inputs to determine which Input is selected."
				});

				// Token: 0x04009827 RID: 38951
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x04009828 RID: 38952
				public static LocString OUTPUT_ACTIVE = string.Concat(new string[]
				{
					"Receives a ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					" or ",
					UI.FormatAsAutomationState("Red", UI.AutomationState.Standby),
					" signal from the selected input"
				});

				// Token: 0x04009829 RID: 38953
				public static LocString OUTPUT_INACTIVE = "Nothing";
			}

			// Token: 0x0200226F RID: 8815
			public class LOGICGATEDEMULTIPLEXER
			{
				// Token: 0x0400982A RID: 38954
				public static LocString NAME = UI.FormatAsLink("Signal Distributor", "LOGICGATEDEMULTIPLEXER");

				// Token: 0x0400982B RID: 38955
				public static LocString DESC = "Signal Distributors can be used to choose which circuit should receive a given automation signal.";

				// Token: 0x0400982C RID: 38956
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Route a single Input signal out one of four possible Outputs, based on the selection made by the Control Inputs.\n\nSend a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" to the two Control Inputs to determine which Output is selected."
				});

				// Token: 0x0400982D RID: 38957
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x0400982E RID: 38958
				public static LocString OUTPUT_ACTIVE = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					" or ",
					UI.FormatAsAutomationState("Red", UI.AutomationState.Standby),
					" signal to the selected output"
				});

				// Token: 0x0400982F RID: 38959
				public static LocString OUTPUT_INACTIVE = "Nothing";
			}

			// Token: 0x02002270 RID: 8816
			public class LOGICSWITCH
			{
				// Token: 0x04009830 RID: 38960
				public static LocString NAME = UI.FormatAsLink("Signal Switch", "LOGICSWITCH");

				// Token: 0x04009831 RID: 38961
				public static LocString DESC = "Signal switches don't turn grids on and off like power switches, but add an extra signal.";

				// Token: 0x04009832 RID: 38962
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" on an ",
					UI.FormatAsLink("Automation", "LOGIC"),
					" grid."
				});

				// Token: 0x04009833 RID: 38963
				public static LocString SIDESCREEN_TITLE = "Signal Switch";

				// Token: 0x04009834 RID: 38964
				public static LocString LOGIC_PORT = "Signal Toggle";

				// Token: 0x04009835 RID: 38965
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if toggled ON";

				// Token: 0x04009836 RID: 38966
				public static LocString LOGIC_PORT_INACTIVE = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " if toggled OFF";
			}

			// Token: 0x02002271 RID: 8817
			public class LOGICPRESSURESENSORGAS
			{
				// Token: 0x04009837 RID: 38967
				public static LocString NAME = UI.FormatAsLink("Atmo Sensor", "LOGICPRESSURESENSORGAS");

				// Token: 0x04009838 RID: 38968
				public static LocString DESC = "Atmo sensors can be used to prevent excess oxygen production and overpressurization.";

				// Token: 0x04009839 RID: 38969
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" pressure enters the chosen range."
				});

				// Token: 0x0400983A RID: 38970
				public static LocString LOGIC_PORT = UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " Pressure";

				// Token: 0x0400983B RID: 38971
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if Gas pressure is within the selected range";

				// Token: 0x0400983C RID: 38972
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002272 RID: 8818
			public class LOGICPRESSURESENSORLIQUID
			{
				// Token: 0x0400983D RID: 38973
				public static LocString NAME = UI.FormatAsLink("Hydro Sensor", "LOGICPRESSURESENSORLIQUID");

				// Token: 0x0400983E RID: 38974
				public static LocString DESC = "A hydro sensor can tell a pump to refill its basin as soon as it contains too little liquid.";

				// Token: 0x0400983F RID: 38975
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" pressure enters the chosen range.\n\nMust be submerged in ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					"."
				});

				// Token: 0x04009840 RID: 38976
				public static LocString LOGIC_PORT = UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " Pressure";

				// Token: 0x04009841 RID: 38977
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if Liquid pressure is within the selected range";

				// Token: 0x04009842 RID: 38978
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002273 RID: 8819
			public class LOGICTEMPERATURESENSOR
			{
				// Token: 0x04009843 RID: 38979
				public static LocString NAME = UI.FormatAsLink("Thermo Sensor", "LOGICTEMPERATURESENSOR");

				// Token: 0x04009844 RID: 38980
				public static LocString DESC = "Thermo sensors can disable buildings when they approach dangerous temperatures.";

				// Token: 0x04009845 RID: 38981
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when ambient ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" enters the chosen range."
				});

				// Token: 0x04009846 RID: 38982
				public static LocString LOGIC_PORT = "Ambient " + UI.FormatAsLink("Temperature", "HEAT");

				// Token: 0x04009847 RID: 38983
				public static LocString LOGIC_PORT_ACTIVE = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if ambient ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" is within the selected range"
				});

				// Token: 0x04009848 RID: 38984
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002274 RID: 8820
			public class LOGICWATTAGESENSOR
			{
				// Token: 0x04009849 RID: 38985
				public static LocString NAME = UI.FormatAsLink("Wattage Sensor", "LOGICWATTSENSOR");

				// Token: 0x0400984A RID: 38986
				public static LocString DESC = "Wattage sensors can send a signal when a building has switched on or off.";

				// Token: 0x0400984B RID: 38987
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when ",
					UI.FormatAsLink("Wattage", "POWER"),
					" consumed enters the chosen range."
				});

				// Token: 0x0400984C RID: 38988
				public static LocString LOGIC_PORT = "Consumed " + UI.FormatAsLink("Wattage", "POWER");

				// Token: 0x0400984D RID: 38989
				public static LocString LOGIC_PORT_ACTIVE = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if current ",
					UI.FormatAsLink("Wattage", "POWER"),
					" is within the selected range"
				});

				// Token: 0x0400984E RID: 38990
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002275 RID: 8821
			public class LOGICHEPSENSOR
			{
				// Token: 0x0400984F RID: 38991
				public static LocString NAME = UI.FormatAsLink("Radbolt Sensor", "LOGICHEPSENSOR");

				// Token: 0x04009850 RID: 38992
				public static LocString DESC = "Radbolt sensors can send a signal when a Radbolt passes over them.";

				// Token: 0x04009851 RID: 38993
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when Radbolts detected enters the chosen range."
				});

				// Token: 0x04009852 RID: 38994
				public static LocString LOGIC_PORT = "Detected Radbolts";

				// Token: 0x04009853 RID: 38995
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if detected Radbolts are within the selected range";

				// Token: 0x04009854 RID: 38996
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002276 RID: 8822
			public class LOGICTIMEOFDAYSENSOR
			{
				// Token: 0x04009855 RID: 38997
				public static LocString NAME = UI.FormatAsLink("Cycle Sensor", "LOGICTIMEOFDAYSENSOR");

				// Token: 0x04009856 RID: 38998
				public static LocString DESC = "Cycle sensors ensure systems always turn on at the same time, day or night, every cycle.";

				// Token: 0x04009857 RID: 38999
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sets an automatic ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" and ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" schedule within one day-night cycle."
				});

				// Token: 0x04009858 RID: 39000
				public static LocString LOGIC_PORT = "Cycle Time";

				// Token: 0x04009859 RID: 39001
				public static LocString LOGIC_PORT_ACTIVE = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if current time is within the selected ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					" range"
				});

				// Token: 0x0400985A RID: 39002
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002277 RID: 8823
			public class LOGICTIMERSENSOR
			{
				// Token: 0x0400985B RID: 39003
				public static LocString NAME = UI.FormatAsLink("Timer Sensor", "LOGICTIMERSENSOR");

				// Token: 0x0400985C RID: 39004
				public static LocString DESC = "Timer sensors create automation schedules for very short or very long periods of time.";

				// Token: 0x0400985D RID: 39005
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Creates a timer to send ",
					UI.FormatAsAutomationState("Green Signals", UI.AutomationState.Active),
					" and ",
					UI.FormatAsAutomationState("Red Signals", UI.AutomationState.Standby),
					" for specific amounts of time."
				});

				// Token: 0x0400985E RID: 39006
				public static LocString LOGIC_PORT = "Timer Schedule";

				// Token: 0x0400985F RID: 39007
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " for the selected amount of Green time";

				// Token: 0x04009860 RID: 39008
				public static LocString LOGIC_PORT_INACTIVE = "Then, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " for the selected amount of Red time";
			}

			// Token: 0x02002278 RID: 8824
			public class LOGICCRITTERCOUNTSENSOR
			{
				// Token: 0x04009861 RID: 39009
				public static LocString NAME = UI.FormatAsLink("Critter Sensor", "LOGICCRITTERCOUNTSENSOR");

				// Token: 0x04009862 RID: 39010
				public static LocString DESC = "Detecting critter populations can help adjust their automated feeding and care regimens.";

				// Token: 0x04009863 RID: 39011
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" based on the number of eggs and critters in a room."
				});

				// Token: 0x04009864 RID: 39012
				public static LocString LOGIC_PORT = "Critter Count";

				// Token: 0x04009865 RID: 39013
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the number of Critters and Eggs in the Room is greater than the selected threshold.";

				// Token: 0x04009866 RID: 39014
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x04009867 RID: 39015
				public static LocString SIDESCREEN_TITLE = "Critter Sensor";

				// Token: 0x04009868 RID: 39016
				public static LocString COUNT_CRITTER_LABEL = "Count Critters";

				// Token: 0x04009869 RID: 39017
				public static LocString COUNT_EGG_LABEL = "Count Eggs";
			}

			// Token: 0x02002279 RID: 8825
			public class LOGICCLUSTERLOCATIONSENSOR
			{
				// Token: 0x0400986A RID: 39018
				public static LocString NAME = UI.FormatAsLink("Starmap Location Sensor", "LOGICCLUSTERLOCATIONSENSOR");

				// Token: 0x0400986B RID: 39019
				public static LocString DESC = "Starmap Location sensors can signal when a spacecraft is at a certain location";

				// Token: 0x0400986C RID: 39020
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Send ",
					UI.FormatAsAutomationState("Green Signals", UI.AutomationState.Active),
					" at the chosen starmap locations and ",
					UI.FormatAsAutomationState("Red Signals", UI.AutomationState.Standby),
					" everywhere else."
				});

				// Token: 0x0400986D RID: 39021
				public static LocString LOGIC_PORT = "Starmap Location Sensor";

				// Token: 0x0400986E RID: 39022
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " at the chosen starmap locations";

				// Token: 0x0400986F RID: 39023
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x0200227A RID: 8826
			public class LOGICDUPLICANTSENSOR
			{
				// Token: 0x04009870 RID: 39024
				public static LocString NAME = UI.FormatAsLink("Duplicant Motion Sensor", "DUPLICANTSENSOR");

				// Token: 0x04009871 RID: 39025
				public static LocString DESC = "Motion sensors save power by only enabling buildings when Duplicants are nearby.";

				// Token: 0x04009872 RID: 39026
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" based on whether a Duplicant is in the sensor's range."
				});

				// Token: 0x04009873 RID: 39027
				public static LocString LOGIC_PORT = "Duplicant Motion Sensor";

				// Token: 0x04009874 RID: 39028
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " while a Duplicant is in the sensor's tile range";

				// Token: 0x04009875 RID: 39029
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x0200227B RID: 8827
			public class LOGICDISEASESENSOR
			{
				// Token: 0x04009876 RID: 39030
				public static LocString NAME = UI.FormatAsLink("Germ Sensor", "LOGICDISEASESENSOR");

				// Token: 0x04009877 RID: 39031
				public static LocString DESC = "Detecting germ populations can help block off or clean up dangerous areas.";

				// Token: 0x04009878 RID: 39032
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" based on quantity of surrounding ",
					UI.FormatAsLink("Germs", "DISEASE"),
					"."
				});

				// Token: 0x04009879 RID: 39033
				public static LocString LOGIC_PORT = UI.FormatAsLink("Germ", "DISEASE") + " Count";

				// Token: 0x0400987A RID: 39034
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the number of Germs is within the selected range";

				// Token: 0x0400987B RID: 39035
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x0200227C RID: 8828
			public class LOGICELEMENTSENSORGAS
			{
				// Token: 0x0400987C RID: 39036
				public static LocString NAME = UI.FormatAsLink("Gas Element Sensor", "LOGICELEMENTSENSORGAS");

				// Token: 0x0400987D RID: 39037
				public static LocString DESC = "These sensors can detect the presence of a specific gas and alter systems accordingly.";

				// Token: 0x0400987E RID: 39038
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" when the selected ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" is detected on this sensor's tile.\n\nSends a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when the selected ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" is not present."
				});

				// Token: 0x0400987F RID: 39039
				public static LocString LOGIC_PORT = "Specific " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " Presence";

				// Token: 0x04009880 RID: 39040
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the selected Gas is detected";

				// Token: 0x04009881 RID: 39041
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x0200227D RID: 8829
			public class LOGICELEMENTSENSORLIQUID
			{
				// Token: 0x04009882 RID: 39042
				public static LocString NAME = UI.FormatAsLink("Liquid Element Sensor", "LOGICELEMENTSENSORLIQUID");

				// Token: 0x04009883 RID: 39043
				public static LocString DESC = "These sensors can detect the presence of a specific liquid and alter systems accordingly.";

				// Token: 0x04009884 RID: 39044
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" when the selected ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" is detected on this sensor's tile.\n\nSends a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when the selected ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" is not present."
				});

				// Token: 0x04009885 RID: 39045
				public static LocString LOGIC_PORT = "Specific " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " Presence";

				// Token: 0x04009886 RID: 39046
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the selected Liquid is detected";

				// Token: 0x04009887 RID: 39047
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x0200227E RID: 8830
			public class LOGICRADIATIONSENSOR
			{
				// Token: 0x04009888 RID: 39048
				public static LocString NAME = UI.FormatAsLink("Radiation Sensor", "LOGICRADIATIONSENSOR");

				// Token: 0x04009889 RID: 39049
				public static LocString DESC = "Radiation sensors can disable buildings when they detect dangerous levels of radiation.";

				// Token: 0x0400988A RID: 39050
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when ambient ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					" enters the chosen range."
				});

				// Token: 0x0400988B RID: 39051
				public static LocString LOGIC_PORT = "Ambient " + UI.FormatAsLink("Radiation", "RADIATION");

				// Token: 0x0400988C RID: 39052
				public static LocString LOGIC_PORT_ACTIVE = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if ambient ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					" is within the selected range"
				});

				// Token: 0x0400988D RID: 39053
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x0200227F RID: 8831
			public class GASCONDUITDISEASESENSOR
			{
				// Token: 0x0400988E RID: 39054
				public static LocString NAME = UI.FormatAsLink("Gas Pipe Germ Sensor", "GASCONDUITDISEASESENSOR");

				// Token: 0x0400988F RID: 39055
				public static LocString DESC = "Germ sensors can help control automation behavior in the presence of germs.";

				// Token: 0x04009890 RID: 39056
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" based on the internal ",
					UI.FormatAsLink("Germ", "DISEASE"),
					" count of the pipe."
				});

				// Token: 0x04009891 RID: 39057
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Germ", "DISEASE") + " Count";

				// Token: 0x04009892 RID: 39058
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the number of Germs in the pipe is within the selected range";

				// Token: 0x04009893 RID: 39059
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002280 RID: 8832
			public class LIQUIDCONDUITDISEASESENSOR
			{
				// Token: 0x04009894 RID: 39060
				public static LocString NAME = UI.FormatAsLink("Liquid Pipe Germ Sensor", "LIQUIDCONDUITDISEASESENSOR");

				// Token: 0x04009895 RID: 39061
				public static LocString DESC = "Germ sensors can help control automation behavior in the presence of germs.";

				// Token: 0x04009896 RID: 39062
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" based on the internal ",
					UI.FormatAsLink("Germ", "DISEASE"),
					" count of the pipe."
				});

				// Token: 0x04009897 RID: 39063
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Germ", "DISEASE") + " Count";

				// Token: 0x04009898 RID: 39064
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the number of Germs in the pipe is within the selected range";

				// Token: 0x04009899 RID: 39065
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002281 RID: 8833
			public class SOLIDCONDUITDISEASESENSOR
			{
				// Token: 0x0400989A RID: 39066
				public static LocString NAME = UI.FormatAsLink("Conveyor Rail Germ Sensor", "SOLIDCONDUITDISEASESENSOR");

				// Token: 0x0400989B RID: 39067
				public static LocString DESC = "Germ sensors can help control automation behavior in the presence of germs.";

				// Token: 0x0400989C RID: 39068
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" based on the internal ",
					UI.FormatAsLink("Germ", "DISEASE"),
					" count of the object on the rail."
				});

				// Token: 0x0400989D RID: 39069
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Germ", "DISEASE") + " Count";

				// Token: 0x0400989E RID: 39070
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the number of Germs on the object on the rail is within the selected range";

				// Token: 0x0400989F RID: 39071
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002282 RID: 8834
			public class GASCONDUITELEMENTSENSOR
			{
				// Token: 0x040098A0 RID: 39072
				public static LocString NAME = UI.FormatAsLink("Gas Pipe Element Sensor", "GASCONDUITELEMENTSENSOR");

				// Token: 0x040098A1 RID: 39073
				public static LocString DESC = "Element sensors can be used to detect the presence of a specific gas in a pipe.";

				// Token: 0x040098A2 RID: 39074
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" when the selected ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" is detected within a pipe."
				});

				// Token: 0x040098A3 RID: 39075
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " Presence";

				// Token: 0x040098A4 RID: 39076
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the configured Gas is detected";

				// Token: 0x040098A5 RID: 39077
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002283 RID: 8835
			public class LIQUIDCONDUITELEMENTSENSOR
			{
				// Token: 0x040098A6 RID: 39078
				public static LocString NAME = UI.FormatAsLink("Liquid Pipe Element Sensor", "LIQUIDCONDUITELEMENTSENSOR");

				// Token: 0x040098A7 RID: 39079
				public static LocString DESC = "Element sensors can be used to detect the presence of a specific liquid in a pipe.";

				// Token: 0x040098A8 RID: 39080
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" when the selected ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" is detected within a pipe."
				});

				// Token: 0x040098A9 RID: 39081
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " Presence";

				// Token: 0x040098AA RID: 39082
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the configured Liquid is detected within the pipe";

				// Token: 0x040098AB RID: 39083
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002284 RID: 8836
			public class SOLIDCONDUITELEMENTSENSOR
			{
				// Token: 0x040098AC RID: 39084
				public static LocString NAME = UI.FormatAsLink("Conveyor Rail Element Sensor", "SOLIDCONDUITELEMENTSENSOR");

				// Token: 0x040098AD RID: 39085
				public static LocString DESC = "Element sensors can be used to detect the presence of a specific item on a rail.";

				// Token: 0x040098AE RID: 39086
				public static LocString EFFECT = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when the selected item is detected on a rail.";

				// Token: 0x040098AF RID: 39087
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Item", "ELEMENTS_LIQUID") + " Presence";

				// Token: 0x040098B0 RID: 39088
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the configured item is detected on the rail";

				// Token: 0x040098B1 RID: 39089
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002285 RID: 8837
			public class GASCONDUITTEMPERATURESENSOR
			{
				// Token: 0x040098B2 RID: 39090
				public static LocString NAME = UI.FormatAsLink("Gas Pipe Thermo Sensor", "GASCONDUITTEMPERATURESENSOR");

				// Token: 0x040098B3 RID: 39091
				public static LocString DESC = "Thermo sensors disable buildings when their pipe contents reach a certain temperature.";

				// Token: 0x040098B4 RID: 39092
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when pipe contents enter the chosen ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" range."
				});

				// Token: 0x040098B5 RID: 39093
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " " + UI.FormatAsLink("Temperature", "HEAT");

				// Token: 0x040098B6 RID: 39094
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the contained Gas is within the selected Temperature range";

				// Token: 0x040098B7 RID: 39095
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002286 RID: 8838
			public class LIQUIDCONDUITTEMPERATURESENSOR
			{
				// Token: 0x040098B8 RID: 39096
				public static LocString NAME = UI.FormatAsLink("Liquid Pipe Thermo Sensor", "LIQUIDCONDUITTEMPERATURESENSOR");

				// Token: 0x040098B9 RID: 39097
				public static LocString DESC = "Thermo sensors disable buildings when their pipe contents reach a certain temperature.";

				// Token: 0x040098BA RID: 39098
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when pipe contents enter the chosen ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" range."
				});

				// Token: 0x040098BB RID: 39099
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " " + UI.FormatAsLink("Temperature", "HEAT");

				// Token: 0x040098BC RID: 39100
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the contained Liquid is within the selected Temperature range";

				// Token: 0x040098BD RID: 39101
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002287 RID: 8839
			public class SOLIDCONDUITTEMPERATURESENSOR
			{
				// Token: 0x040098BE RID: 39102
				public static LocString NAME = UI.FormatAsLink("Conveyor Rail Thermo Sensor", "SOLIDCONDUITTEMPERATURESENSOR");

				// Token: 0x040098BF RID: 39103
				public static LocString DESC = "Thermo sensors disable buildings when their rail contents reach a certain temperature.";

				// Token: 0x040098C0 RID: 39104
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when rail contents enter the chosen ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" range."
				});

				// Token: 0x040098C1 RID: 39105
				public static LocString LOGIC_PORT = "Internal item " + UI.FormatAsLink("Temperature", "HEAT");

				// Token: 0x040098C2 RID: 39106
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the contained item is within the selected Temperature range";

				// Token: 0x040098C3 RID: 39107
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002288 RID: 8840
			public class LOGICCOUNTER
			{
				// Token: 0x040098C4 RID: 39108
				public static LocString NAME = UI.FormatAsLink("Signal Counter", "LOGICCOUNTER");

				// Token: 0x040098C5 RID: 39109
				public static LocString DESC = "For numbers higher than ten connect multiple counters together.";

				// Token: 0x040098C6 RID: 39110
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Counts how many times a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" has been received up to a chosen number.\n\nWhen the chosen number is reached it sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" until it receives another ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					", when it resets automatically and begins counting again."
				});

				// Token: 0x040098C7 RID: 39111
				public static LocString LOGIC_PORT = "Internal Counter Value";

				// Token: 0x040098C8 RID: 39112
				public static LocString INPUT_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Increase counter by one";

				// Token: 0x040098C9 RID: 39113
				public static LocString INPUT_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Nothing";

				// Token: 0x040098CA RID: 39114
				public static LocString LOGIC_PORT_RESET = "Reset Counter";

				// Token: 0x040098CB RID: 39115
				public static LocString RESET_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Reset counter";

				// Token: 0x040098CC RID: 39116
				public static LocString RESET_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Nothing";

				// Token: 0x040098CD RID: 39117
				public static LocString LOGIC_PORT_OUTPUT = "Number Reached";

				// Token: 0x040098CE RID: 39118
				public static LocString OUTPUT_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when the counter matches the selected value";

				// Token: 0x040098CF RID: 39119
				public static LocString OUTPUT_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002289 RID: 8841
			public class LOGICALARM
			{
				// Token: 0x040098D0 RID: 39120
				public static LocString NAME = UI.FormatAsLink("Automated Notifier", "LOGICALARM");

				// Token: 0x040098D1 RID: 39121
				public static LocString DESC = "Sends a notification when it receives a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ".";

				// Token: 0x040098D2 RID: 39122
				public static LocString EFFECT = "Attach to sensors to send a notification when certain conditions are met.\n\nNotifications can be customized.";

				// Token: 0x040098D3 RID: 39123
				public static LocString LOGIC_PORT = "Notification";

				// Token: 0x040098D4 RID: 39124
				public static LocString INPUT_NAME = "INPUT";

				// Token: 0x040098D5 RID: 39125
				public static LocString INPUT_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Push notification";

				// Token: 0x040098D6 RID: 39126
				public static LocString INPUT_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Nothing";
			}

			// Token: 0x0200228A RID: 8842
			public class PIXELPACK
			{
				// Token: 0x040098D7 RID: 39127
				public static LocString NAME = UI.FormatAsLink("Pixel Pack", "PIXELPACK");

				// Token: 0x040098D8 RID: 39128
				public static LocString DESC = "Four pixels which can be individually designated different colors.";

				// Token: 0x040098D9 RID: 39129
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Pixels can be designated a color when it receives a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" and a different color when it receives a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					".\n\nInput from an ",
					UI.FormatAsLink("Automation Wire", "LOGICWIRE"),
					" controls the whole strip. Input from an ",
					UI.FormatAsLink("Automation Ribbon", "LOGICRIBBON"),
					" can control individual pixels on the strip."
				});

				// Token: 0x040098DA RID: 39130
				public static LocString LOGIC_PORT = "Color Selection";

				// Token: 0x040098DB RID: 39131
				public static LocString INPUT_NAME = "RIBBON INPUT";

				// Token: 0x040098DC RID: 39132
				public static LocString INPUT_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Display the configured " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " pixels";

				// Token: 0x040098DD RID: 39133
				public static LocString INPUT_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Display the configured " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " pixels";

				// Token: 0x040098DE RID: 39134
				public static LocString SIDESCREEN_TITLE = "Pixel Pack";
			}

			// Token: 0x0200228B RID: 8843
			public class LOGICHAMMER
			{
				// Token: 0x040098DF RID: 39135
				public static LocString NAME = UI.FormatAsLink("Hammer", "LOGICHAMMER");

				// Token: 0x040098E0 RID: 39136
				public static LocString DESC = "The hammer makes neat sounds when it strikes buildings.";

				// Token: 0x040098E1 RID: 39137
				public static LocString EFFECT = "In its default orientation, the hammer strikes the building to the left when it receives a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ".\n\nEach building has a unique sound when struck by the hammer.\n\nThe hammer does no damage when it strikes.";

				// Token: 0x040098E2 RID: 39138
				public static LocString LOGIC_PORT = "Resonating Buildings";

				// Token: 0x040098E3 RID: 39139
				public static LocString INPUT_NAME = "INPUT";

				// Token: 0x040098E4 RID: 39140
				public static LocString INPUT_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Hammer strikes once";

				// Token: 0x040098E5 RID: 39141
				public static LocString INPUT_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Nothing";
			}

			// Token: 0x0200228C RID: 8844
			public class LOGICRIBBONWRITER
			{
				// Token: 0x040098E6 RID: 39142
				public static LocString NAME = UI.FormatAsLink("Ribbon Writer", "LOGICRIBBONWRITER");

				// Token: 0x040098E7 RID: 39143
				public static LocString DESC = "Translates the signal from an " + UI.FormatAsLink("Automation Wire", "LOGICWIRE") + " to a single Bit in an " + UI.FormatAsLink("Automation Ribbon", "LOGICRIBBON");

				// Token: 0x040098E8 RID: 39144
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Writes a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" to the specified Bit of an ",
					BUILDINGS.PREFABS.LOGICRIBBON.NAME,
					"\n\n",
					BUILDINGS.PREFABS.LOGICRIBBON.NAME,
					" must be used as the output wire to avoid overloading."
				});

				// Token: 0x040098E9 RID: 39145
				public static LocString LOGIC_PORT = "1-Bit Input";

				// Token: 0x040098EA RID: 39146
				public static LocString INPUT_NAME = "INPUT";

				// Token: 0x040098EB RID: 39147
				public static LocString INPUT_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Receives " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " to be written to selected Bit";

				// Token: 0x040098EC RID: 39148
				public static LocString INPUT_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Receives " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " to to be written selected Bit";

				// Token: 0x040098ED RID: 39149
				public static LocString LOGIC_PORT_OUTPUT = "Bit Writing";

				// Token: 0x040098EE RID: 39150
				public static LocString OUTPUT_NAME = "RIBBON OUTPUT";

				// Token: 0x040098EF RID: 39151
				public static LocString OUTPUT_PORT_ACTIVE = string.Concat(new string[]
				{
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					": Writes a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" to selected Bit of an ",
					BUILDINGS.PREFABS.LOGICRIBBON.NAME
				});

				// Token: 0x040098F0 RID: 39152
				public static LocString OUTPUT_PORT_INACTIVE = string.Concat(new string[]
				{
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					": Writes a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" to selected Bit of an ",
					BUILDINGS.PREFABS.LOGICRIBBON.NAME
				});
			}

			// Token: 0x0200228D RID: 8845
			public class LOGICRIBBONREADER
			{
				// Token: 0x040098F1 RID: 39153
				public static LocString NAME = UI.FormatAsLink("Ribbon Reader", "LOGICRIBBONREADER");

				// Token: 0x040098F2 RID: 39154
				public static LocString DESC = string.Concat(new string[]
				{
					"Inputs the signal from a single Bit in an ",
					UI.FormatAsLink("Automation Ribbon", "LOGICRIBBON"),
					" into an ",
					UI.FormatAsLink("Automation Wire", "LOGICWIRE"),
					"."
				});

				// Token: 0x040098F3 RID: 39155
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Reads a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" from the specified Bit of an ",
					BUILDINGS.PREFABS.LOGICRIBBON.NAME,
					" onto an ",
					BUILDINGS.PREFABS.LOGICWIRE.NAME,
					"."
				});

				// Token: 0x040098F4 RID: 39156
				public static LocString LOGIC_PORT = "4-Bit Input";

				// Token: 0x040098F5 RID: 39157
				public static LocString INPUT_NAME = "RIBBON INPUT";

				// Token: 0x040098F6 RID: 39158
				public static LocString INPUT_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Reads a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " from selected Bit";

				// Token: 0x040098F7 RID: 39159
				public static LocString INPUT_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Reads a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " from selected Bit";

				// Token: 0x040098F8 RID: 39160
				public static LocString LOGIC_PORT_OUTPUT = "Bit Reading";

				// Token: 0x040098F9 RID: 39161
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x040098FA RID: 39162
				public static LocString OUTPUT_PORT_ACTIVE = string.Concat(new string[]
				{
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					": Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" to attached ",
					UI.FormatAsLink("Automation Wire", "LOGICWIRE")
				});

				// Token: 0x040098FB RID: 39163
				public static LocString OUTPUT_PORT_INACTIVE = string.Concat(new string[]
				{
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					": Sends a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" to attached ",
					UI.FormatAsLink("Automation Wire", "LOGICWIRE")
				});
			}

			// Token: 0x0200228E RID: 8846
			public class TRAVELTUBEENTRANCE
			{
				// Token: 0x040098FC RID: 39164
				public static LocString NAME = UI.FormatAsLink("Transit Tube Access", "TRAVELTUBEENTRANCE");

				// Token: 0x040098FD RID: 39165
				public static LocString DESC = "Duplicants require access points to enter tubes, but not to exit them.";

				// Token: 0x040098FE RID: 39166
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to enter the connected ",
					UI.FormatAsLink("Transit Tube", "TRAVELTUBE"),
					" system.\n\nStops drawing ",
					UI.FormatAsLink("Power", "POWER"),
					" once fully charged."
				});
			}

			// Token: 0x0200228F RID: 8847
			public class TRAVELTUBE
			{
				// Token: 0x040098FF RID: 39167
				public static LocString NAME = UI.FormatAsLink("Transit Tube", "TRAVELTUBE");

				// Token: 0x04009900 RID: 39168
				public static LocString DESC = "Duplicants will only exit a transit tube when a safe landing area is available beneath it.";

				// Token: 0x04009901 RID: 39169
				public static LocString EFFECT = "Quickly transports Duplicants from a " + UI.FormatAsLink("Transit Tube Access", "TRAVELTUBEENTRANCE") + " to the tube's end.\n\nOnly transports Duplicants.";
			}

			// Token: 0x02002290 RID: 8848
			public class TRAVELTUBEWALLBRIDGE
			{
				// Token: 0x04009902 RID: 39170
				public static LocString NAME = UI.FormatAsLink("Transit Tube Crossing", "TRAVELTUBEWALLBRIDGE");

				// Token: 0x04009903 RID: 39171
				public static LocString DESC = "Tube crossings can run transit tubes through walls without leaking gas or liquid.";

				// Token: 0x04009904 RID: 39172
				public static LocString EFFECT = "Allows " + UI.FormatAsLink("Transit Tubes", "TRAVELTUBE") + " to be run through wall and floor tile.\n\nFunctions as regular tile.";
			}

			// Token: 0x02002291 RID: 8849
			public class SOLIDCONDUIT
			{
				// Token: 0x04009905 RID: 39173
				public static LocString NAME = UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT");

				// Token: 0x04009906 RID: 39174
				public static LocString DESC = "Rails move materials where they'll be needed most, saving Duplicants the walk.";

				// Token: 0x04009907 RID: 39175
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Transports ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" on a track between ",
					UI.FormatAsLink("Conveyor Loader", "SOLIDCONDUITINBOX"),
					" and ",
					UI.FormatAsLink("Conveyor Receptacle", "SOLIDCONDUITOUTBOX"),
					".\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x02002292 RID: 8850
			public class SOLIDCONDUITINBOX
			{
				// Token: 0x04009908 RID: 39176
				public static LocString NAME = UI.FormatAsLink("Conveyor Loader", "SOLIDCONDUITINBOX");

				// Token: 0x04009909 RID: 39177
				public static LocString DESC = "Material filters can be used to determine what resources are sent down the rail.";

				// Token: 0x0400990A RID: 39178
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Loads ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" onto ",
					UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT"),
					" for transport.\n\nOnly loads the resources of your choosing."
				});
			}

			// Token: 0x02002293 RID: 8851
			public class SOLIDCONDUITOUTBOX
			{
				// Token: 0x0400990B RID: 39179
				public static LocString NAME = UI.FormatAsLink("Conveyor Receptacle", "SOLIDCONDUITOUTBOX");

				// Token: 0x0400990C RID: 39180
				public static LocString DESC = "When materials reach the end of a rail they enter a receptacle to be used by Duplicants.";

				// Token: 0x0400990D RID: 39181
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Unloads ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" from a ",
					UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT"),
					" into storage."
				});
			}

			// Token: 0x02002294 RID: 8852
			public class SOLIDTRANSFERARM
			{
				// Token: 0x0400990E RID: 39182
				public static LocString NAME = UI.FormatAsLink("Auto-Sweeper", "SOLIDTRANSFERARM");

				// Token: 0x0400990F RID: 39183
				public static LocString DESC = "An auto-sweeper's range can be viewed at any time by " + UI.CLICK(UI.ClickType.clicking) + " on the building.";

				// Token: 0x04009910 RID: 39184
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Automates ",
					UI.FormatAsLink("Sweeping", "CHORES"),
					" and ",
					UI.FormatAsLink("Supplying", "CHORES"),
					" errands by sucking up all nearby ",
					UI.FormatAsLink("Debris", "DECOR"),
					".\n\nMaterials are automatically delivered to any ",
					UI.FormatAsLink("Conveyor Loader", "SOLIDCONDUITINBOX"),
					", ",
					UI.FormatAsLink("Conveyor Receptacle", "SOLIDCONDUITOUTBOX"),
					", storage, or buildings within range."
				});
			}

			// Token: 0x02002295 RID: 8853
			public class SOLIDCONDUITBRIDGE
			{
				// Token: 0x04009911 RID: 39185
				public static LocString NAME = UI.FormatAsLink("Conveyor Bridge", "SOLIDCONDUITBRIDGE");

				// Token: 0x04009912 RID: 39186
				public static LocString DESC = "Separating rail systems helps ensure materials go to the intended destinations.";

				// Token: 0x04009913 RID: 39187
				public static LocString EFFECT = "Runs one " + UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT") + " section over another without joining them.\n\nCan be run through wall and floor tile.";
			}

			// Token: 0x02002296 RID: 8854
			public class SOLIDVENT
			{
				// Token: 0x04009914 RID: 39188
				public static LocString NAME = UI.FormatAsLink("Conveyor Chute", "SOLIDVENT");

				// Token: 0x04009915 RID: 39189
				public static LocString DESC = "When materials reach the end of a rail they are dropped back into the world.";

				// Token: 0x04009916 RID: 39190
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Unloads ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" from a ",
					UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT"),
					" onto the floor."
				});
			}

			// Token: 0x02002297 RID: 8855
			public class SOLIDLOGICVALVE
			{
				// Token: 0x04009917 RID: 39191
				public static LocString NAME = UI.FormatAsLink("Conveyor Shutoff", "SOLIDLOGICVALVE");

				// Token: 0x04009918 RID: 39192
				public static LocString DESC = "Automated conveyors save power and time by removing the need for Duplicant input.";

				// Token: 0x04009919 RID: 39193
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Connects to an ",
					UI.FormatAsLink("Automation", "LOGIC"),
					" grid to automatically turn ",
					UI.FormatAsLink("Solid Material", "ELEMENTS_SOLID"),
					" transport on or off."
				});

				// Token: 0x0400991A RID: 39194
				public static LocString LOGIC_PORT = "Open/Close";

				// Token: 0x0400991B RID: 39195
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Allow material transport";

				// Token: 0x0400991C RID: 39196
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Prevent material transport";
			}

			// Token: 0x02002298 RID: 8856
			public class SOLIDLIMITVALVE
			{
				// Token: 0x0400991D RID: 39197
				public static LocString NAME = UI.FormatAsLink("Conveyor Meter", "SOLIDLIMITVALVE");

				// Token: 0x0400991E RID: 39198
				public static LocString DESC = "Conveyor Meters let an exact amount of materials pass through before shutting off.";

				// Token: 0x0400991F RID: 39199
				public static LocString EFFECT = "Connects to an " + UI.FormatAsLink("Automation", "LOGIC") + " grid to automatically turn material transfer off when the specified amount has passed through it.";

				// Token: 0x04009920 RID: 39200
				public static LocString LOGIC_PORT_OUTPUT = "Limit Reached";

				// Token: 0x04009921 RID: 39201
				public static LocString OUTPUT_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if limit has been reached";

				// Token: 0x04009922 RID: 39202
				public static LocString OUTPUT_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x04009923 RID: 39203
				public static LocString LOGIC_PORT_RESET = "Reset Meter";

				// Token: 0x04009924 RID: 39204
				public static LocString RESET_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Reset the amount";

				// Token: 0x04009925 RID: 39205
				public static LocString RESET_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Nothing";
			}

			// Token: 0x02002299 RID: 8857
			public class DEVPUMPSOLID
			{
				// Token: 0x04009926 RID: 39206
				public static LocString NAME = UI.FormatAsLink("Dev Pump Solid", "DEVPUMPSOLID");

				// Token: 0x04009927 RID: 39207
				public static LocString DESC = "Piping a pump's output to a building's intake will send solids to that building.";

				// Token: 0x04009928 RID: 39208
				public static LocString EFFECT = "Generates chosen " + UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID") + " and runs it through " + UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT");
			}

			// Token: 0x0200229A RID: 8858
			public class AUTOMINER
			{
				// Token: 0x04009929 RID: 39209
				public static LocString NAME = UI.FormatAsLink("Robo-Miner", "AUTOMINER");

				// Token: 0x0400992A RID: 39210
				public static LocString DESC = "A robo-miner's range can be viewed at any time by selecting the building.";

				// Token: 0x0400992B RID: 39211
				public static LocString EFFECT = "Automatically digs out all materials in a set range.";
			}

			// Token: 0x0200229B RID: 8859
			public class CREATUREFEEDER
			{
				// Token: 0x0400992C RID: 39212
				public static LocString NAME = UI.FormatAsLink("Critter Feeder", "CREATUREFEEDER");

				// Token: 0x0400992D RID: 39213
				public static LocString DESC = "Critters tend to stay close to their food source and wander less when given a feeder.";

				// Token: 0x0400992E RID: 39214
				public static LocString EFFECT = "Automatically dispenses food for hungry " + UI.FormatAsLink("Critters", "CRITTERS") + ".";
			}

			// Token: 0x0200229C RID: 8860
			public class GRAVITASPEDESTAL
			{
				// Token: 0x0400992F RID: 39215
				public static LocString NAME = UI.FormatAsLink("Pedestal", "ITEMPEDESTAL");

				// Token: 0x04009930 RID: 39216
				public static LocString DESC = "Perception can be drastically changed by a bit of thoughtful presentation.";

				// Token: 0x04009931 RID: 39217
				public static LocString EFFECT = "Displays a single object, doubling its " + UI.FormatAsLink("Decor", "DECOR") + " value.\n\nObjects with negative Decor will gain some positive Decor when displayed.";

				// Token: 0x04009932 RID: 39218
				public static LocString DISPLAYED_ITEM_FMT = "Displayed {0}";
			}

			// Token: 0x0200229D RID: 8861
			public class ITEMPEDESTAL
			{
				// Token: 0x04009933 RID: 39219
				public static LocString NAME = UI.FormatAsLink("Pedestal", "ITEMPEDESTAL");

				// Token: 0x04009934 RID: 39220
				public static LocString DESC = "Perception can be drastically changed by a bit of thoughtful presentation.";

				// Token: 0x04009935 RID: 39221
				public static LocString EFFECT = "Displays a single object, doubling its " + UI.FormatAsLink("Decor", "DECOR") + " value.\n\nObjects with negative Decor will gain some positive Decor when displayed.";

				// Token: 0x04009936 RID: 39222
				public static LocString DISPLAYED_ITEM_FMT = "Displayed {0}";
			}

			// Token: 0x0200229E RID: 8862
			public class CROWNMOULDING
			{
				// Token: 0x04009937 RID: 39223
				public static LocString NAME = UI.FormatAsLink("Crown Moulding", "CROWNMOULDING");

				// Token: 0x04009938 RID: 39224
				public static LocString DESC = "Crown moulding is used as purely decorative trim for ceilings.";

				// Token: 0x04009939 RID: 39225
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Used to decorate the ceilings of rooms.\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});

				// Token: 0x02002DCC RID: 11724
				public class FACADES
				{
				}
			}

			// Token: 0x0200229F RID: 8863
			public class CORNERMOULDING
			{
				// Token: 0x0400993A RID: 39226
				public static LocString NAME = UI.FormatAsLink("Corner Moulding", "CORNERMOULDING");

				// Token: 0x0400993B RID: 39227
				public static LocString DESC = "Corner moulding is used as purely decorative trim for ceiling corners.";

				// Token: 0x0400993C RID: 39228
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Used to decorate the ceiling corners of rooms.\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});

				// Token: 0x02002DCD RID: 11725
				public class FACADES
				{
				}
			}

			// Token: 0x020022A0 RID: 8864
			public class EGGINCUBATOR
			{
				// Token: 0x0400993D RID: 39229
				public static LocString NAME = UI.FormatAsLink("Incubator", "EGGINCUBATOR");

				// Token: 0x0400993E RID: 39230
				public static LocString DESC = "Incubators can maintain the ideal internal conditions for several species of critter egg.";

				// Token: 0x0400993F RID: 39231
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Incubates ",
					UI.FormatAsLink("Critter", "CRITTERS"),
					" eggs until ready to hatch.\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Critter Ranching", "RANCHING1"),
					" ."
				});
			}

			// Token: 0x020022A1 RID: 8865
			public class EGGCRACKER
			{
				// Token: 0x04009940 RID: 39232
				public static LocString NAME = UI.FormatAsLink("Egg Cracker", "EGGCRACKER");

				// Token: 0x04009941 RID: 39233
				public static LocString DESC = "Raw eggs are an ingredient in certain high quality food recipes.";

				// Token: 0x04009942 RID: 39234
				public static LocString EFFECT = "Converts viable " + UI.FormatAsLink("Critter", "CRITTERS") + " eggs into cooking ingredients.\n\nCracked Eggs cannot hatch.\n\nDuplicants will not crack eggs unless tasks are queued.";

				// Token: 0x04009943 RID: 39235
				public static LocString RECIPE_DESCRIPTION = "Turns {0} into {1}.";

				// Token: 0x04009944 RID: 39236
				public static LocString RESULT_DESCRIPTION = "Cracked {0}";
			}

			// Token: 0x020022A2 RID: 8866
			public class URANIUMCENTRIFUGE
			{
				// Token: 0x04009945 RID: 39237
				public static LocString NAME = UI.FormatAsLink("Uranium Centrifuge", "URANIUMCENTRIFUGE");

				// Token: 0x04009946 RID: 39238
				public static LocString DESC = "Enriched uranium is a specialized substance that can be used to fuel powerful research reactors.";

				// Token: 0x04009947 RID: 39239
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Extracts ",
					UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM"),
					" from ",
					UI.FormatAsLink("Uranium Ore", "URANIUMORE"),
					".\n\nOutputs ",
					UI.FormatAsLink("Depleted Uranium", "DEPLETEDURANIUM"),
					" in molten form."
				});

				// Token: 0x04009948 RID: 39240
				public static LocString RECIPE_DESCRIPTION = "Convert Uranium ore to Molten Uranium and Enriched Uranium";
			}

			// Token: 0x020022A3 RID: 8867
			public class HIGHENERGYPARTICLEREDIRECTOR
			{
				// Token: 0x04009949 RID: 39241
				public static LocString NAME = UI.FormatAsLink("Radbolt Reflector", "HIGHENERGYPARTICLEREDIRECTOR");

				// Token: 0x0400994A RID: 39242
				public static LocString DESC = "We were all out of mirrors.";

				// Token: 0x0400994B RID: 39243
				public static LocString EFFECT = "Receives and redirects Radbolts from " + UI.FormatAsLink("Radbolt Generators", "HIGHENERGYPARTICLESPAWNER") + ".";

				// Token: 0x0400994C RID: 39244
				public static LocString LOGIC_PORT = "Ignore incoming Radbolts";

				// Token: 0x0400994D RID: 39245
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Allow incoming Radbolts";

				// Token: 0x0400994E RID: 39246
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Ignore incoming Radbolts";
			}

			// Token: 0x020022A4 RID: 8868
			public class MANUALHIGHENERGYPARTICLESPAWNER
			{
				// Token: 0x0400994F RID: 39247
				public static LocString NAME = UI.FormatAsLink("Manual Radbolt Generator", "MANUALHIGHENERGYPARTICLESPAWNER");

				// Token: 0x04009950 RID: 39248
				public static LocString DESC = "Radbolts are necessary for producing Materials Science research.";

				// Token: 0x04009951 RID: 39249
				public static LocString EFFECT = "Refines radioactive ores to generate Radbolts.\n\nEmits generated Radbolts in the direction of your choosing.";

				// Token: 0x04009952 RID: 39250
				public static LocString RECIPE_DESCRIPTION = "Creates " + UI.FormatAsLink("Radbolts", "RADIATION") + " by processing {0}. Also creates {1} as a byproduct.";
			}

			// Token: 0x020022A5 RID: 8869
			public class HIGHENERGYPARTICLESPAWNER
			{
				// Token: 0x04009953 RID: 39251
				public static LocString NAME = UI.FormatAsLink("Radbolt Generator", "HIGHENERGYPARTICLESPAWNER");

				// Token: 0x04009954 RID: 39252
				public static LocString DESC = "Radbolts are necessary for producing Materials Science research.";

				// Token: 0x04009955 RID: 39253
				public static LocString EFFECT = "Attracts nearby " + UI.FormatAsLink("Radiation", "RADIATION") + " to generate Radbolts.\n\nEmits generated Radbolts in the direction of your choosing when the set Radbolt threshold is reached.\n\nRadbolts collected will rapidly decay while this building is disabled.";

				// Token: 0x04009956 RID: 39254
				public static LocString LOGIC_PORT = "Do not emit Radbolts";

				// Token: 0x04009957 RID: 39255
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Emit Radbolts";

				// Token: 0x04009958 RID: 39256
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Do not emit Radbolts";
			}

			// Token: 0x020022A6 RID: 8870
			public class HEPBATTERY
			{
				// Token: 0x04009959 RID: 39257
				public static LocString NAME = UI.FormatAsLink("Radbolt Chamber", "HEPBATTERY");

				// Token: 0x0400995A RID: 39258
				public static LocString DESC = "Particles packed up and ready to go.";

				// Token: 0x0400995B RID: 39259
				public static LocString EFFECT = "Stores Radbolts in a high-energy state, ready for transport.\n\nRequires a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " to release radbolts from storage when the Radbolt threshold is reached.\n\nRadbolts in storage will rapidly decay while this building is disabled.";

				// Token: 0x0400995C RID: 39260
				public static LocString LOGIC_PORT = "Do not emit Radbolts";

				// Token: 0x0400995D RID: 39261
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Emit Radbolts";

				// Token: 0x0400995E RID: 39262
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Do not emit Radbolts";

				// Token: 0x0400995F RID: 39263
				public static LocString LOGIC_PORT_STORAGE = "Radbolt Storage";

				// Token: 0x04009960 RID: 39264
				public static LocString LOGIC_PORT_STORAGE_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when its Radbolt Storage is full";

				// Token: 0x04009961 RID: 39265
				public static LocString LOGIC_PORT_STORAGE_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x020022A7 RID: 8871
			public class HEPBRIDGETILE
			{
				// Token: 0x04009962 RID: 39266
				public static LocString NAME = UI.FormatAsLink("Radbolt Joint Plate", "HEPBRIDGETILE");

				// Token: 0x04009963 RID: 39267
				public static LocString DESC = "Allows Radbolts to pass through walls.";

				// Token: 0x04009964 RID: 39268
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Receives ",
					UI.FormatAsLink("Radbolts", "RADIATION"),
					" from ",
					UI.FormatAsLink("Radbolt Generators", "HIGHENERGYPARTICLESPAWNER"),
					" and directs them through walls. All other materials and elements will be blocked from passage."
				});
			}

			// Token: 0x020022A8 RID: 8872
			public class ASTRONAUTTRAININGCENTER
			{
				// Token: 0x04009965 RID: 39269
				public static LocString NAME = UI.FormatAsLink("Space Cadet Centrifuge", "ASTRONAUTTRAININGCENTER");

				// Token: 0x04009966 RID: 39270
				public static LocString DESC = "Duplicants must complete astronaut training in order to pilot space rockets.";

				// Token: 0x04009967 RID: 39271
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Trains Duplicants to become ",
					UI.FormatAsLink("Astronaut", "ROCKETPILOTING1"),
					".\n\nDuplicants must possess the ",
					UI.FormatAsLink("Astronaut", "ROCKETPILOTING1"),
					" trait to receive training."
				});
			}

			// Token: 0x020022A9 RID: 8873
			public class HOTTUB
			{
				// Token: 0x04009968 RID: 39272
				public static LocString NAME = UI.FormatAsLink("Hot Tub", "HOTTUB");

				// Token: 0x04009969 RID: 39273
				public static LocString DESC = "Relaxes Duplicants with massaging jets of heated liquid.";

				// Token: 0x0400996A RID: 39274
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Requires ",
					UI.FormatAsLink("Pipes", "LIQUIDPIPING"),
					" to and from tub and ",
					UI.FormatAsLink("Power", "POWER"),
					" to run the jets.\n\nWater must be a comfortable temperature and will cool rapidly.\n\nIncreases Duplicant ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});

				// Token: 0x0400996B RID: 39275
				public static LocString WATER_REQUIREMENT = "{element}: {amount}";

				// Token: 0x0400996C RID: 39276
				public static LocString WATER_REQUIREMENT_TOOLTIP = "This building must be filled with {amount} {element} in order to function.";

				// Token: 0x0400996D RID: 39277
				public static LocString TEMPERATURE_REQUIREMENT = "Minimum {element} Temperature: {temperature}";

				// Token: 0x0400996E RID: 39278
				public static LocString TEMPERATURE_REQUIREMENT_TOOLTIP = "The Hot Tub will only be usable if supplied with {temperature} {element}. If the {element} gets too cold, the Hot Tub will drain and require refilling with {element}.";
			}

			// Token: 0x020022AA RID: 8874
			public class SODAFOUNTAIN
			{
				// Token: 0x0400996F RID: 39279
				public static LocString NAME = UI.FormatAsLink("Soda Fountain", "SODAFOUNTAIN");

				// Token: 0x04009970 RID: 39280
				public static LocString DESC = "Sparkling water puts a twinkle in a Duplicant's eye.";

				// Token: 0x04009971 RID: 39281
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Creates soda from ",
					UI.FormatAsLink("Water", "WATER"),
					" and ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					".\n\nConsuming soda water increases Duplicant ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});
			}

			// Token: 0x020022AB RID: 8875
			public class UNCONSTRUCTEDROCKETMODULE
			{
				// Token: 0x04009972 RID: 39282
				public static LocString NAME = "Empty Rocket Module";

				// Token: 0x04009973 RID: 39283
				public static LocString DESC = "Something useful could be put here someday";

				// Token: 0x04009974 RID: 39284
				public static LocString EFFECT = "Can be changed into a different rocket module";
			}

			// Token: 0x020022AC RID: 8876
			public class MODULARLAUNCHPADPORT
			{
				// Token: 0x04009975 RID: 39285
				public static LocString NAME = UI.FormatAsLink("Rocket Port", "MODULARLAUNCHPADPORTSOLID");

				// Token: 0x04009976 RID: 39286
				public static LocString NAME_PLURAL = UI.FormatAsLink("Rocket Ports", "MODULARLAUNCHPADPORTSOLID");
			}

			// Token: 0x020022AD RID: 8877
			public class MODULARLAUNCHPADPORTGAS
			{
				// Token: 0x04009977 RID: 39287
				public static LocString NAME = UI.FormatAsLink("Gas Rocket Port Loader", "MODULARLAUNCHPADPORTGAS");

				// Token: 0x04009978 RID: 39288
				public static LocString DESC = "Rockets must be landed to load or unload resources.";

				// Token: 0x04009979 RID: 39289
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Loads ",
					UI.FormatAsLink("Gases", "ELEMENTS_GAS"),
					" to the storage of a linked rocket.\n\nAutomatically links when built to the side of a ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					" or another ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME,
					".\n\nUses the gas filters set on the rocket's cargo bays."
				});
			}

			// Token: 0x020022AE RID: 8878
			public class MODULARLAUNCHPADPORTLIQUID
			{
				// Token: 0x0400997A RID: 39290
				public static LocString NAME = UI.FormatAsLink("Liquid Rocket Port Loader", "MODULARLAUNCHPADPORTLIQUID");

				// Token: 0x0400997B RID: 39291
				public static LocString DESC = "Rockets must be landed to load or unload resources.";

				// Token: 0x0400997C RID: 39292
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Loads ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					" to the storage of a linked rocket.\n\nAutomatically links when built to the side of a ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					" or another ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME,
					".\n\nUses the liquid filters set on the rocket's cargo bays."
				});
			}

			// Token: 0x020022AF RID: 8879
			public class MODULARLAUNCHPADPORTSOLID
			{
				// Token: 0x0400997D RID: 39293
				public static LocString NAME = UI.FormatAsLink("Solid Rocket Port Loader", "MODULARLAUNCHPADPORTSOLID");

				// Token: 0x0400997E RID: 39294
				public static LocString DESC = "Rockets must be landed to load or unload resources.";

				// Token: 0x0400997F RID: 39295
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Loads ",
					UI.FormatAsLink("Solids", "ELEMENTS_SOLID"),
					" to the storage of a linked rocket.\n\nAutomatically links when built to the side of a ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					" or another ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME,
					".\n\nUses the solid material filters set on the rocket's cargo bays."
				});
			}

			// Token: 0x020022B0 RID: 8880
			public class MODULARLAUNCHPADPORTGASUNLOADER
			{
				// Token: 0x04009980 RID: 39296
				public static LocString NAME = UI.FormatAsLink("Gas Rocket Port Unloader", "MODULARLAUNCHPADPORTGASUNLOADER");

				// Token: 0x04009981 RID: 39297
				public static LocString DESC = "Rockets must be landed to load or unload resources.";

				// Token: 0x04009982 RID: 39298
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Unloads ",
					UI.FormatAsLink("Gases", "ELEMENTS_GAS"),
					" from the storage of a linked rocket.\n\nAutomatically links when built to the side of a ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					" or another ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME,
					".\n\nUses the gas filters set on this unloader."
				});
			}

			// Token: 0x020022B1 RID: 8881
			public class MODULARLAUNCHPADPORTLIQUIDUNLOADER
			{
				// Token: 0x04009983 RID: 39299
				public static LocString NAME = UI.FormatAsLink("Liquid Rocket Port Unloader", "MODULARLAUNCHPADPORTLIQUIDUNLOADER");

				// Token: 0x04009984 RID: 39300
				public static LocString DESC = "Rockets must be landed to load or unload resources.";

				// Token: 0x04009985 RID: 39301
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Unloads ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					" from the storage of a linked rocket.\n\nAutomatically links when built to the side of a ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					" or another ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME,
					".\n\nUses the liquid filters set on this unloader."
				});
			}

			// Token: 0x020022B2 RID: 8882
			public class MODULARLAUNCHPADPORTSOLIDUNLOADER
			{
				// Token: 0x04009986 RID: 39302
				public static LocString NAME = UI.FormatAsLink("Solid Rocket Port Unloader", "MODULARLAUNCHPADPORTSOLIDUNLOADER");

				// Token: 0x04009987 RID: 39303
				public static LocString DESC = "Rockets must be landed to load or unload resources.";

				// Token: 0x04009988 RID: 39304
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Unloads ",
					UI.FormatAsLink("Solids", "ELEMENTS_SOLID"),
					" from the storage of a linked rocket.\n\nAutomatically links when built to the side of a ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					" or another ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME,
					".\n\nUses the solid material filters set on this unloader."
				});
			}

			// Token: 0x020022B3 RID: 8883
			public class STICKERBOMB
			{
				// Token: 0x04009989 RID: 39305
				public static LocString NAME = UI.FormatAsLink("Sticker Bomb", "STICKERBOMB");

				// Token: 0x0400998A RID: 39306
				public static LocString DESC = "Surprise decor sneak attacks a Duplicant's gloomy day.";
			}

			// Token: 0x020022B4 RID: 8884
			public class HEATCOMPRESSOR
			{
				// Token: 0x0400998B RID: 39307
				public static LocString NAME = UI.FormatAsLink("Liquid Heatquilizer", "HEATCOMPRESSOR");

				// Token: 0x0400998C RID: 39308
				public static LocString DESC = "\"Room temperature\" is relative, really.";

				// Token: 0x0400998D RID: 39309
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Heats or cools ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" to match ambient ",
					UI.FormatAsLink("Air Temperature", "HEAT"),
					"."
				});
			}

			// Token: 0x020022B5 RID: 8885
			public class PARTYCAKE
			{
				// Token: 0x0400998E RID: 39310
				public static LocString NAME = UI.FormatAsLink("Triple Decker Cake", "PARTYCAKE");

				// Token: 0x0400998F RID: 39311
				public static LocString DESC = "Any way you slice it, that's a good looking cake.";

				// Token: 0x04009990 RID: 39312
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nAdds a ",
					UI.FormatAsLink("Morale", "MORALE"),
					" bonus to Duplicants' parties."
				});
			}

			// Token: 0x020022B6 RID: 8886
			public class RAILGUN
			{
				// Token: 0x04009991 RID: 39313
				public static LocString NAME = UI.FormatAsLink("Interplanetary Launcher", "RAILGUN");

				// Token: 0x04009992 RID: 39314
				public static LocString DESC = "It's tempting to climb inside but trust me... don't.";

				// Token: 0x04009993 RID: 39315
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Launches ",
					UI.FormatAsLink("Interplanetary Payloads", "RAILGUNPAYLOAD"),
					" between Planetoids.\n\nPayloads can contain ",
					UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
					", ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					", or ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" materials.\n\nCannot transport Duplicants."
				});

				// Token: 0x04009994 RID: 39316
				public static LocString SIDESCREEN_HEP_REQUIRED = "Launch cost: {current} / {required} radbolts";

				// Token: 0x04009995 RID: 39317
				public static LocString LOGIC_PORT = "Launch Toggle";

				// Token: 0x04009996 RID: 39318
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Enable payload launching.";

				// Token: 0x04009997 RID: 39319
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Disable payload launching.";
			}

			// Token: 0x020022B7 RID: 8887
			public class RAILGUNPAYLOADOPENER
			{
				// Token: 0x04009998 RID: 39320
				public static LocString NAME = UI.FormatAsLink("Payload Opener", "RAILGUNPAYLOADOPENER");

				// Token: 0x04009999 RID: 39321
				public static LocString DESC = "Payload openers can be hooked up to conveyors, plumbing and ventilation for improved sorting.";

				// Token: 0x0400999A RID: 39322
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Unpacks ",
					UI.FormatAsLink("Interplanetary Payloads", "RAILGUNPAYLOAD"),
					" delivered by Duplicants.\n\nAutomatically separates ",
					UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
					", ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					", and ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" materials and distributes them to the appropriate systems."
				});
			}

			// Token: 0x020022B8 RID: 8888
			public class LANDINGBEACON
			{
				// Token: 0x0400999B RID: 39323
				public static LocString NAME = UI.FormatAsLink("Targeting Beacon", "LANDINGBEACON");

				// Token: 0x0400999C RID: 39324
				public static LocString DESC = "Microtarget where your " + UI.FormatAsLink("Interplanetary Payload", "RAILGUNPAYLOAD") + " lands on a Planetoid surface.";

				// Token: 0x0400999D RID: 39325
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Guides ",
					UI.FormatAsLink("Interplanetary Payloads", "RAILGUNPAYLOAD"),
					" and ",
					UI.FormatAsLink("Orbital Cargo Modules", "ORBITALCARGOMODULE"),
					" to land nearby.\n\n",
					UI.FormatAsLink("Interplanetary Payloads", "RAILGUNPAYLOAD"),
					" must be launched from a ",
					UI.FormatAsLink("Interplanetary Launcher", "RAILGUN"),
					"."
				});
			}

			// Token: 0x020022B9 RID: 8889
			public class DIAMONDPRESS
			{
				// Token: 0x0400999E RID: 39326
				public static LocString NAME = UI.FormatAsLink("Diamond Press", "DIAMONDPRESS");

				// Token: 0x0400999F RID: 39327
				public static LocString DESC = "Crushes refined carbon into diamond.";

				// Token: 0x040099A0 RID: 39328
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Power", "POWER"),
					" and ",
					UI.FormatAsLink("Radbolts", "RADIATION"),
					" to crush ",
					UI.FormatAsLink("Refined Carbon", "REFINEDCARBON"),
					" into ",
					UI.FormatAsLink("Diamond", "DIAMOND"),
					".\n\nDuplicants will not fabricate items unless recipes are queued and ",
					UI.FormatAsLink("Refined Carbon", "REFINEDCARBON"),
					" has been discovered."
				});

				// Token: 0x040099A1 RID: 39329
				public static LocString REFINED_CARBON_RECIPE_DESCRIPTION = "Converts {1} to {0}";
			}

			// Token: 0x020022BA RID: 8890
			public class ESCAPEPOD
			{
				// Token: 0x040099A2 RID: 39330
				public static LocString NAME = UI.FormatAsLink("Escape Pod", "ESCAPEPOD");

				// Token: 0x040099A3 RID: 39331
				public static LocString DESC = "Delivers a Duplicant from a stranded rocket to the nearest Planetoid.";
			}

			// Token: 0x020022BB RID: 8891
			public class ROCKETINTERIORLIQUIDOUTPUTPORT
			{
				// Token: 0x040099A4 RID: 39332
				public static LocString NAME = UI.FormatAsLink("Liquid Spacefarer Output Port", "ROCKETINTERIORLIQUIDOUTPUTPORT");

				// Token: 0x040099A5 RID: 39333
				public static LocString DESC = "A direct attachment to the input port on the exterior of a rocket.";

				// Token: 0x040099A6 RID: 39334
				public static LocString EFFECT = "Allows a direct conduit connection into the " + UI.FormatAsLink("Spacefarer Module", "HABITATMODULEMEDIUM") + " of a rocket.";
			}

			// Token: 0x020022BC RID: 8892
			public class ROCKETINTERIORLIQUIDINPUTPORT
			{
				// Token: 0x040099A7 RID: 39335
				public static LocString NAME = UI.FormatAsLink("Liquid Spacefarer Input Port", "ROCKETINTERIORLIQUIDINPUTPORT");

				// Token: 0x040099A8 RID: 39336
				public static LocString DESC = "A direct attachment to the output port on the exterior of a rocket.";

				// Token: 0x040099A9 RID: 39337
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows a direct conduit connection out of the ",
					UI.FormatAsLink("Spacefarer Module", "HABITATMODULEMEDIUM"),
					" of a rocket.\nCan be used to vent ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					" to space during flight."
				});
			}

			// Token: 0x020022BD RID: 8893
			public class ROCKETINTERIORGASOUTPUTPORT
			{
				// Token: 0x040099AA RID: 39338
				public static LocString NAME = UI.FormatAsLink("Gas Spacefarer Output Port", "ROCKETINTERIORGASOUTPUTPORT");

				// Token: 0x040099AB RID: 39339
				public static LocString DESC = "A direct attachment to the input port on the exterior of a rocket.";

				// Token: 0x040099AC RID: 39340
				public static LocString EFFECT = "Allows a direct conduit connection into the " + UI.FormatAsLink("Spacefarer Module", "HABITATMODULEMEDIUM") + " of a rocket.";
			}

			// Token: 0x020022BE RID: 8894
			public class ROCKETINTERIORGASINPUTPORT
			{
				// Token: 0x040099AD RID: 39341
				public static LocString NAME = UI.FormatAsLink("Gas Spacefarer Input Port", "ROCKETINTERIORGASINPUTPORT");

				// Token: 0x040099AE RID: 39342
				public static LocString DESC = "A direct attachment leading to the output port on the exterior of the rocket.";

				// Token: 0x040099AF RID: 39343
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows a direct conduit connection out of the ",
					UI.FormatAsLink("Spacefarer Module", "HABITATMODULEMEDIUM"),
					" of the rocket.\nCan be used to vent ",
					UI.FormatAsLink("Gasses", "ELEMENTS_GAS"),
					" to space during flight."
				});
			}

			// Token: 0x020022BF RID: 8895
			public class MASSIVEHEATSINK
			{
				// Token: 0x040099B0 RID: 39344
				public static LocString NAME = UI.FormatAsLink("Anti Entropy Thermo-Nullifier", "MASSIVEHEATSINK");

				// Token: 0x040099B1 RID: 39345
				public static LocString DESC = "";

				// Token: 0x040099B2 RID: 39346
				public static LocString EFFECT = string.Concat(new string[]
				{
					"A self-sustaining machine powered by what appears to be refined ",
					UI.FormatAsLink("Neutronium", "UNOBTANIUM"),
					".\n\nAbsorbs and neutralizes ",
					UI.FormatAsLink("Heat", "HEAT"),
					" energy when provided with piped ",
					UI.FormatAsLink("Hydrogen", "HYDROGEN"),
					"."
				});
			}

			// Token: 0x020022C0 RID: 8896
			public class MEGABRAINTANK
			{
				// Token: 0x040099B3 RID: 39347
				public static LocString NAME = UI.FormatAsLink("Somnium Synthesizer", "MEGABRAINTANK");

				// Token: 0x040099B4 RID: 39348
				public static LocString DESC = "";

				// Token: 0x040099B5 RID: 39349
				public static LocString EFFECT = string.Concat(new string[]
				{
					"An organic multi-cortex repository and processing system fuelled by ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					".\n\nAnalyzes ",
					UI.FormatAsLink("Dream Journals", "DREAMJOURNAL"),
					" produced by Duplicants wearing ",
					UI.FormatAsLink("Pajamas", "SLEEP_CLINIC_PAJAMAS"),
					".\n\nProvides a sustainable boost to Duplicant skills and abilities throughout the colony."
				});
			}

			// Token: 0x020022C1 RID: 8897
			public class GRAVITASCREATUREMANIPULATOR
			{
				// Token: 0x040099B6 RID: 39350
				public static LocString NAME = UI.FormatAsLink("Critter Flux-O-Matic", "GRAVITASCREATUREMANIPULATOR");

				// Token: 0x040099B7 RID: 39351
				public static LocString DESC = "";

				// Token: 0x040099B8 RID: 39352
				public static LocString EFFECT = "An experimental DNA manipulator.\n\nAnalyzes " + UI.FormatAsLink("Critters", "CREATURES") + " to transform base morphs into random variants of their species.";
			}

			// Token: 0x020022C2 RID: 8898
			public class FACILITYBACKWALLWINDOW
			{
				// Token: 0x040099B9 RID: 39353
				public static LocString NAME = UI.FormatAsLink("Window", "FACILITYBACKWALLWINDOW");

				// Token: 0x040099BA RID: 39354
				public static LocString DESC = "";

				// Token: 0x040099BB RID: 39355
				public static LocString EFFECT = "A tall, thin window.";
			}

			// Token: 0x020022C3 RID: 8899
			public class POIBUNKEREXTERIORDOOR
			{
				// Token: 0x040099BC RID: 39356
				public static LocString NAME = UI.FormatAsLink("Security Door", "POIBUNKEREXTERIORDOOR");

				// Token: 0x040099BD RID: 39357
				public static LocString EFFECT = "A strong door with a sophisticated genetic lock.";

				// Token: 0x040099BE RID: 39358
				public static LocString DESC = "";
			}

			// Token: 0x020022C4 RID: 8900
			public class POIDOORINTERNAL
			{
				// Token: 0x040099BF RID: 39359
				public static LocString NAME = UI.FormatAsLink("Security Door", "POIDOORINTERNAL");

				// Token: 0x040099C0 RID: 39360
				public static LocString EFFECT = "A strong door with a sophisticated genetic lock.";

				// Token: 0x040099C1 RID: 39361
				public static LocString DESC = "";
			}

			// Token: 0x020022C5 RID: 8901
			public class POIFACILITYDOOR
			{
				// Token: 0x040099C2 RID: 39362
				public static LocString NAME = UI.FormatAsLink("Lobby Doors", "FACILITYDOOR");

				// Token: 0x040099C3 RID: 39363
				public static LocString EFFECT = "Large double doors that were once the main entrance to a large facility.";

				// Token: 0x040099C4 RID: 39364
				public static LocString DESC = "";
			}

			// Token: 0x020022C6 RID: 8902
			public class VENDINGMACHINE
			{
				// Token: 0x040099C5 RID: 39365
				public static LocString NAME = "Vending Machine";

				// Token: 0x040099C6 RID: 39366
				public static LocString DESC = "A pristine " + UI.FormatAsLink("Nutrient Bar", "FIELDRATION") + " dispenser.";
			}

			// Token: 0x020022C7 RID: 8903
			public class GENESHUFFLER
			{
				// Token: 0x040099C7 RID: 39367
				public static LocString NAME = "Neural Vacillator";

				// Token: 0x040099C8 RID: 39368
				public static LocString DESC = "A massive synthetic brain, suspended in saline solution.\n\nThere is a chair attached to the device with room for one person.";
			}

			// Token: 0x020022C8 RID: 8904
			public class PROPTALLPLANT
			{
				// Token: 0x040099C9 RID: 39369
				public static LocString NAME = "Potted Plant";

				// Token: 0x040099CA RID: 39370
				public static LocString DESC = "Looking closely, it appears to be fake.";
			}

			// Token: 0x020022C9 RID: 8905
			public class PROPTABLE
			{
				// Token: 0x040099CB RID: 39371
				public static LocString NAME = "Table";

				// Token: 0x040099CC RID: 39372
				public static LocString DESC = "A table and some chairs.";
			}

			// Token: 0x020022CA RID: 8906
			public class PROPDESK
			{
				// Token: 0x040099CD RID: 39373
				public static LocString NAME = "Computer Desk";

				// Token: 0x040099CE RID: 39374
				public static LocString DESC = "An intact office desk, decorated with several personal belongings and a barely functioning computer.";
			}

			// Token: 0x020022CB RID: 8907
			public class PROPFACILITYCHAIR
			{
				// Token: 0x040099CF RID: 39375
				public static LocString NAME = "Lobby Chair";

				// Token: 0x040099D0 RID: 39376
				public static LocString DESC = "A chair where visitors can comfortably wait before their appointments.";
			}

			// Token: 0x020022CC RID: 8908
			public class PROPFACILITYCOUCH
			{
				// Token: 0x040099D1 RID: 39377
				public static LocString NAME = "Lobby Couch";

				// Token: 0x040099D2 RID: 39378
				public static LocString DESC = "A couch where visitors can comfortably wait before their appointments.";
			}

			// Token: 0x020022CD RID: 8909
			public class PROPFACILITYDESK
			{
				// Token: 0x040099D3 RID: 39379
				public static LocString NAME = "Director's Desk";

				// Token: 0x040099D4 RID: 39380
				public static LocString DESC = "A spotless desk filled with impeccably organized office supplies.\n\nA photo peeks out from beneath the desk pad, depicting two beaming young women in caps and gowns.\n\nThe photo is quite old.";
			}

			// Token: 0x020022CE RID: 8910
			public class PROPFACILITYTABLE
			{
				// Token: 0x040099D5 RID: 39381
				public static LocString NAME = "Coffee Table";

				// Token: 0x040099D6 RID: 39382
				public static LocString DESC = "A low coffee table that may have once held old science magazines.";
			}

			// Token: 0x020022CF RID: 8911
			public class PROPFACILITYSTATUE
			{
				// Token: 0x040099D7 RID: 39383
				public static LocString NAME = "Gravitas Monument";

				// Token: 0x040099D8 RID: 39384
				public static LocString DESC = "A large, modern sculpture that sits in the center of the lobby.\n\nIt's an artistic cross between an hourglass shape and a double helix.";
			}

			// Token: 0x020022D0 RID: 8912
			public class PROPFACILITYCHANDELIER
			{
				// Token: 0x040099D9 RID: 39385
				public static LocString NAME = "Chandelier";

				// Token: 0x040099DA RID: 39386
				public static LocString DESC = "A large chandelier that hangs from the ceiling.\n\nIt does not appear to function.";
			}

			// Token: 0x020022D1 RID: 8913
			public class PROPFACILITYGLOBEDROORS
			{
				// Token: 0x040099DB RID: 39387
				public static LocString NAME = "Filing Cabinet";

				// Token: 0x040099DC RID: 39388
				public static LocString DESC = "A filing cabinet for storing hard copy employee records.\n\nThe contents have been shredded.";
			}

			// Token: 0x020022D2 RID: 8914
			public class PROPFACILITYDISPLAY1
			{
				// Token: 0x040099DD RID: 39389
				public static LocString NAME = "Electronic Display";

				// Token: 0x040099DE RID: 39390
				public static LocString DESC = "An electronic display projecting the blueprint of a familiar device.\n\nIt looks like a Printing Pod.";
			}

			// Token: 0x020022D3 RID: 8915
			public class PROPFACILITYDISPLAY2
			{
				// Token: 0x040099DF RID: 39391
				public static LocString NAME = "Electronic Display";

				// Token: 0x040099E0 RID: 39392
				public static LocString DESC = "An electronic display projecting the blueprint of a familiar device.\n\nIt looks like a Mining Gun.";
			}

			// Token: 0x020022D4 RID: 8916
			public class PROPFACILITYDISPLAY3
			{
				// Token: 0x040099E1 RID: 39393
				public static LocString NAME = "Electronic Display";

				// Token: 0x040099E2 RID: 39394
				public static LocString DESC = "An electronic display projecting the blueprint of a strange device.\n\nPerhaps these displays were used to entice visitors.";
			}

			// Token: 0x020022D5 RID: 8917
			public class PROPFACILITYTALLPLANT
			{
				// Token: 0x040099E3 RID: 39395
				public static LocString NAME = "Office Plant";

				// Token: 0x040099E4 RID: 39396
				public static LocString DESC = "It's survived the vacuum of space by virtue of being plastic.";
			}

			// Token: 0x020022D6 RID: 8918
			public class PROPFACILITYLAMP
			{
				// Token: 0x040099E5 RID: 39397
				public static LocString NAME = "Light Fixture";

				// Token: 0x040099E6 RID: 39398
				public static LocString DESC = "A long light fixture that hangs from the ceiling.\n\nIt does not appear to function.";
			}

			// Token: 0x020022D7 RID: 8919
			public class PROPFACILITYWALLDEGREE
			{
				// Token: 0x040099E7 RID: 39399
				public static LocString NAME = "Doctorate Degree";

				// Token: 0x040099E8 RID: 39400
				public static LocString DESC = "Certification in Applied Physics, awarded in recognition of one \"Jacquelyn A. Stern\".";
			}

			// Token: 0x020022D8 RID: 8920
			public class PROPFACILITYPAINTING
			{
				// Token: 0x040099E9 RID: 39401
				public static LocString NAME = "Landscape Portrait";

				// Token: 0x040099EA RID: 39402
				public static LocString DESC = "A painting featuring a copse of fir trees and a magnificent mountain range on the horizon.\n\nThe air in the room prickles with the sensation that I'm not meant to be here.";
			}

			// Token: 0x020022D9 RID: 8921
			public class PROPRECEPTIONDESK
			{
				// Token: 0x040099EB RID: 39403
				public static LocString NAME = "Reception Desk";

				// Token: 0x040099EC RID: 39404
				public static LocString DESC = "A full coffee cup and a note abandoned mid sentence sit behind the desk.\n\nIt gives me an eerie feeling, as if the receptionist has stepped out and will return any moment.";
			}

			// Token: 0x020022DA RID: 8922
			public class PROPELEVATOR
			{
				// Token: 0x040099ED RID: 39405
				public static LocString NAME = "Broken Elevator";

				// Token: 0x040099EE RID: 39406
				public static LocString DESC = "Out of service.\n\nThe buttons inside indicate it went down more than a dozen floors at one point in time.";
			}

			// Token: 0x020022DB RID: 8923
			public class SETLOCKER
			{
				// Token: 0x040099EF RID: 39407
				public static LocString NAME = "Locker";

				// Token: 0x040099F0 RID: 39408
				public static LocString DESC = "A basic metal locker.\n\nIt contains an assortment of personal effects.";
			}

			// Token: 0x020022DC RID: 8924
			public class PROPLIGHT
			{
				// Token: 0x040099F1 RID: 39409
				public static LocString NAME = "Light Fixture";

				// Token: 0x040099F2 RID: 39410
				public static LocString DESC = "An elegant ceiling lamp, slightly worse for wear.";
			}

			// Token: 0x020022DD RID: 8925
			public class PROPLADDER
			{
				// Token: 0x040099F3 RID: 39411
				public static LocString NAME = "Ladder";

				// Token: 0x040099F4 RID: 39412
				public static LocString DESC = "A hard plastic ladder.";
			}

			// Token: 0x020022DE RID: 8926
			public class PROPSKELETON
			{
				// Token: 0x040099F5 RID: 39413
				public static LocString NAME = "Model Skeleton";

				// Token: 0x040099F6 RID: 39414
				public static LocString DESC = "A detailed anatomical model.\n\nIt appears to be made of resin.";
			}

			// Token: 0x020022DF RID: 8927
			public class PROPSURFACESATELLITE1
			{
				// Token: 0x040099F7 RID: 39415
				public static LocString NAME = "Crashed Satellite";

				// Token: 0x040099F8 RID: 39416
				public static LocString DESC = "All that remains of a once peacefully orbiting satellite.";
			}

			// Token: 0x020022E0 RID: 8928
			public class PROPSURFACESATELLITE2
			{
				// Token: 0x040099F9 RID: 39417
				public static LocString NAME = "Wrecked Satellite";

				// Token: 0x040099FA RID: 39418
				public static LocString DESC = "All that remains of a once peacefully orbiting satellite.";
			}

			// Token: 0x020022E1 RID: 8929
			public class PROPSURFACESATELLITE3
			{
				// Token: 0x040099FB RID: 39419
				public static LocString NAME = "Crushed Satellite";

				// Token: 0x040099FC RID: 39420
				public static LocString DESC = "All that remains of a once peacefully orbiting satellite.";
			}

			// Token: 0x020022E2 RID: 8930
			public class PROPCLOCK
			{
				// Token: 0x040099FD RID: 39421
				public static LocString NAME = "Clock";

				// Token: 0x040099FE RID: 39422
				public static LocString DESC = "A simple wall clock.\n\nIt is no longer ticking.";
			}

			// Token: 0x020022E3 RID: 8931
			public class PROPGRAVITASDECORATIVEWINDOW
			{
				// Token: 0x040099FF RID: 39423
				public static LocString NAME = "Window";

				// Token: 0x04009A00 RID: 39424
				public static LocString DESC = "A tall, thin window which once pointed to a courtyard.";
			}

			// Token: 0x020022E4 RID: 8932
			public class PROPGRAVITASLABWINDOW
			{
				// Token: 0x04009A01 RID: 39425
				public static LocString NAME = "Lab Window";

				// Token: 0x04009A02 RID: 39426
				public static LocString DESC = "";

				// Token: 0x04009A03 RID: 39427
				public static LocString EFFECT = "A lab window. Formerly a portal to the outside world.";
			}

			// Token: 0x020022E5 RID: 8933
			public class PROPGRAVITASLABWINDOWHORIZONTAL
			{
				// Token: 0x04009A04 RID: 39428
				public static LocString NAME = "Lab Window";

				// Token: 0x04009A05 RID: 39429
				public static LocString DESC = "";

				// Token: 0x04009A06 RID: 39430
				public static LocString EFFECT = "A lab window.\n\nSomeone once stared out of this, contemplating the results of an experiment.";
			}

			// Token: 0x020022E6 RID: 8934
			public class PROPGRAVITASLABWALL
			{
				// Token: 0x04009A07 RID: 39431
				public static LocString NAME = "Lab Wall";

				// Token: 0x04009A08 RID: 39432
				public static LocString DESC = "";

				// Token: 0x04009A09 RID: 39433
				public static LocString EFFECT = "A regular wall that once existed in a working lab.";
			}

			// Token: 0x020022E7 RID: 8935
			public class GRAVITASCONTAINER
			{
				// Token: 0x04009A0A RID: 39434
				public static LocString NAME = "Pajama Cubby";

				// Token: 0x04009A0B RID: 39435
				public static LocString DESC = "";

				// Token: 0x04009A0C RID: 39436
				public static LocString EFFECT = "A clothing storage unit.\n\nIt contains ultra-soft sleepwear.";
			}

			// Token: 0x020022E8 RID: 8936
			public class GRAVITASLABLIGHT
			{
				// Token: 0x04009A0D RID: 39437
				public static LocString NAME = "LED Light";

				// Token: 0x04009A0E RID: 39438
				public static LocString DESC = "";

				// Token: 0x04009A0F RID: 39439
				public static LocString EFFECT = "An overhead light therapy lamp designed to soothe the minds.";
			}

			// Token: 0x020022E9 RID: 8937
			public class GRAVITASDOOR
			{
				// Token: 0x04009A10 RID: 39440
				public static LocString NAME = "Gravitas Door";

				// Token: 0x04009A11 RID: 39441
				public static LocString DESC = "";

				// Token: 0x04009A12 RID: 39442
				public static LocString EFFECT = "An office door to an office that no longer exists.";
			}

			// Token: 0x020022EA RID: 8938
			public class PROPGRAVITASWALL
			{
				// Token: 0x04009A13 RID: 39443
				public static LocString NAME = "Wall";

				// Token: 0x04009A14 RID: 39444
				public static LocString DESC = "";

				// Token: 0x04009A15 RID: 39445
				public static LocString EFFECT = "The wall of a once-great scientific facility.";
			}

			// Token: 0x020022EB RID: 8939
			public class PROPGRAVITASDISPLAY4
			{
				// Token: 0x04009A16 RID: 39446
				public static LocString NAME = "Electronic Display";

				// Token: 0x04009A17 RID: 39447
				public static LocString DESC = "An electronic display projecting the blueprint of a robotic device.\n\nIt looks like a ceiling robot.";
			}

			// Token: 0x020022EC RID: 8940
			public class PROPGRAVITASCEILINGROBOT
			{
				// Token: 0x04009A18 RID: 39448
				public static LocString NAME = "Ceiling Robot";

				// Token: 0x04009A19 RID: 39449
				public static LocString DESC = "Non-functioning robotic arms that once assisted lab technicians.";
			}

			// Token: 0x020022ED RID: 8941
			public class PROPGRAVITASFLOORROBOT
			{
				// Token: 0x04009A1A RID: 39450
				public static LocString NAME = "Robotic Arm";

				// Token: 0x04009A1B RID: 39451
				public static LocString DESC = "The grasping robotic claw designed to assist technicians in a lab.";
			}

			// Token: 0x020022EE RID: 8942
			public class PROPGRAVITASJAR1
			{
				// Token: 0x04009A1C RID: 39452
				public static LocString NAME = "Big Brain Jar";

				// Token: 0x04009A1D RID: 39453
				public static LocString DESC = "An abnormally large brain floating in embalming liquid to prevent decomposition.";
			}

			// Token: 0x020022EF RID: 8943
			public class PROPGRAVITASCREATUREPOSTER
			{
				// Token: 0x04009A1E RID: 39454
				public static LocString NAME = "Anatomy Poster";

				// Token: 0x04009A1F RID: 39455
				public static LocString DESC = "An anatomical illustration of the very first " + UI.FormatAsLink("Hatch", "HATCH") + " ever produced.\n\nWhile the ratio of egg sac to brain may appear outlandish, it is in fact to scale.";
			}

			// Token: 0x020022F0 RID: 8944
			public class PROPGRAVITASDESKPODIUM
			{
				// Token: 0x04009A20 RID: 39456
				public static LocString NAME = "Computer Podium";

				// Token: 0x04009A21 RID: 39457
				public static LocString DESC = "A clutter-proof desk to minimize distractions.\n\nThere appears to be something stored in the computer.";
			}

			// Token: 0x020022F1 RID: 8945
			public class PROPGRAVITASFIRSTAIDKIT
			{
				// Token: 0x04009A22 RID: 39458
				public static LocString NAME = "First Aid Kit";

				// Token: 0x04009A23 RID: 39459
				public static LocString DESC = "It looks like it's been used a lot.";
			}

			// Token: 0x020022F2 RID: 8946
			public class PROPGRAVITASHANDSCANNER
			{
				// Token: 0x04009A24 RID: 39460
				public static LocString NAME = "Hand Scanner";

				// Token: 0x04009A25 RID: 39461
				public static LocString DESC = "A sophisticated security device.\n\nIt appears to use a method other than fingerprints to verify an individual's identity.";
			}

			// Token: 0x020022F3 RID: 8947
			public class PROPGRAVITASLABTABLE
			{
				// Token: 0x04009A26 RID: 39462
				public static LocString NAME = "Lab Desk";

				// Token: 0x04009A27 RID: 39463
				public static LocString DESC = "The quaint research desk of a departed lab technician.\n\nPerhaps the computer stores something of interest.";
			}

			// Token: 0x020022F4 RID: 8948
			public class PROPGRAVITASROBTICTABLE
			{
				// Token: 0x04009A28 RID: 39464
				public static LocString NAME = "Robotics Research Desk";

				// Token: 0x04009A29 RID: 39465
				public static LocString DESC = "The work space of an extinct robotics technician who left behind some unfinished prototypes.";
			}

			// Token: 0x020022F5 RID: 8949
			public class PROPGRAVITASSHELF
			{
				// Token: 0x04009A2A RID: 39466
				public static LocString NAME = "Shelf";

				// Token: 0x04009A2B RID: 39467
				public static LocString DESC = "A shelf holding jars just out of reach for a short person.";
			}

			// Token: 0x020022F6 RID: 8950
			public class PROPGRAVITASJAR2
			{
				// Token: 0x04009A2C RID: 39468
				public static LocString NAME = "Sample Jar";

				// Token: 0x04009A2D RID: 39469
				public static LocString DESC = "The corpse of a proto-hatch creature meticulously preserved in a jar.";
			}

			// Token: 0x020022F7 RID: 8951
			public class WARPCONDUITRECEIVER
			{
				// Token: 0x04009A2E RID: 39470
				public static LocString NAME = "Supply Teleporter Output";

				// Token: 0x04009A2F RID: 39471
				public static LocString DESC = "The tubes at the back disappear into nowhere.";

				// Token: 0x04009A30 RID: 39472
				public static LocString EFFECT = string.Concat(new string[]
				{
					"A machine capable of teleporting ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					", ",
					UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
					", and ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" resources to another asteroid.\n\nIt can be activated by a Duplicant with the ",
					UI.FormatAsLink("Field Research", "RESEARCHING2"),
					" skill.\n\nThis is the receiving side."
				});
			}

			// Token: 0x020022F8 RID: 8952
			public class WARPCONDUITSENDER
			{
				// Token: 0x04009A31 RID: 39473
				public static LocString NAME = "Supply Teleporter Input";

				// Token: 0x04009A32 RID: 39474
				public static LocString DESC = "The tubes at the back disappear into nowhere.";

				// Token: 0x04009A33 RID: 39475
				public static LocString EFFECT = string.Concat(new string[]
				{
					"A machine capable of teleporting ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					", ",
					UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
					", and ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" resources to another asteroid.\n\nIt can be activated by a Duplicant with the ",
					UI.FormatAsLink("Field Research", "RESEARCHING2"),
					" skill.\n\nThis is the transmitting side."
				});
			}

			// Token: 0x020022F9 RID: 8953
			public class WARPPORTAL
			{
				// Token: 0x04009A34 RID: 39476
				public static LocString NAME = "Teleporter Transmitter";

				// Token: 0x04009A35 RID: 39477
				public static LocString DESC = "The functional remnants of an intricate teleportation system.\n\nThis is the outgoing side, and has one pre-programmed destination.";
			}

			// Token: 0x020022FA RID: 8954
			public class WARPRECEIVER
			{
				// Token: 0x04009A36 RID: 39478
				public static LocString NAME = "Teleporter Receiver";

				// Token: 0x04009A37 RID: 39479
				public static LocString DESC = "The functional remnants of an intricate teleportation system.\n\nThis is the incoming side.";
			}

			// Token: 0x020022FB RID: 8955
			public class TEMPORALTEAROPENER
			{
				// Token: 0x04009A38 RID: 39480
				public static LocString NAME = "Temporal Tear Opener";

				// Token: 0x04009A39 RID: 39481
				public static LocString DESC = "Infinite possibilities, with a complimentary side of meteor showers.";

				// Token: 0x04009A3A RID: 39482
				public static LocString EFFECT = "A powerful mechanism capable of tearing through the fabric of reality.";

				// Token: 0x02002DCE RID: 11726
				public class SIDESCREEN
				{
					// Token: 0x0400BA70 RID: 47728
					public static LocString TEXT = "Fire!";

					// Token: 0x0400BA71 RID: 47729
					public static LocString TOOLTIP = "The big red button.";
				}
			}

			// Token: 0x020022FC RID: 8956
			public class LONELYMINIONHOUSE
			{
				// Token: 0x04009A3B RID: 39483
				public static LocString NAME = UI.FormatAsLink("Gravitas Shipping Container", "LONELYMINIONHOUSE");

				// Token: 0x04009A3C RID: 39484
				public static LocString DESC = "Its occupant has been alone for so long, he's forgotten what friendship feels like.";

				// Token: 0x04009A3D RID: 39485
				public static LocString EFFECT = "A large transport unit from the facility's sub-sub-basement.\n\nIt has been modified into a crude yet functional temporary shelter.";
			}

			// Token: 0x020022FD RID: 8957
			public class LONELYMINIONHOUSE_COMPLETE
			{
				// Token: 0x04009A3E RID: 39486
				public static LocString NAME = UI.FormatAsLink("Gravitas Shipping Container", "LONELYMINIONHOUSE_COMPLETE");

				// Token: 0x04009A3F RID: 39487
				public static LocString DESC = "Someone lived inside it for a while.";

				// Token: 0x04009A40 RID: 39488
				public static LocString EFFECT = "A super-spacious container for the " + UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID") + " of your choosing.";
			}

			// Token: 0x020022FE RID: 8958
			public class LONELYMAILBOX
			{
				// Token: 0x04009A41 RID: 39489
				public static LocString NAME = UI.FormatAsLink("Mailbox", "LONELYMAILBOX");

				// Token: 0x04009A42 RID: 39490
				public static LocString DESC = "There's nothing quite like receiving homemade gifts in the mail.";

				// Token: 0x04009A43 RID: 39491
				public static LocString EFFECT = "Displays a single edible object.";
			}
		}

		// Token: 0x02001BBA RID: 7098
		public static class DAMAGESOURCES
		{
			// Token: 0x04007DCB RID: 32203
			public static LocString NOTIFICATION_TOOLTIP = "A {0} sustained damage from {1}";

			// Token: 0x04007DCC RID: 32204
			public static LocString CONDUIT_CONTENTS_FROZE = "pipe contents becoming too cold";

			// Token: 0x04007DCD RID: 32205
			public static LocString CONDUIT_CONTENTS_BOILED = "pipe contents becoming too hot";

			// Token: 0x04007DCE RID: 32206
			public static LocString BUILDING_OVERHEATED = "overheating";

			// Token: 0x04007DCF RID: 32207
			public static LocString CORROSIVE_ELEMENT = "corrosive element";

			// Token: 0x04007DD0 RID: 32208
			public static LocString BAD_INPUT_ELEMENT = "receiving an incorrect substance";

			// Token: 0x04007DD1 RID: 32209
			public static LocString MINION_DESTRUCTION = "an angry Duplicant. Rude!";

			// Token: 0x04007DD2 RID: 32210
			public static LocString LIQUID_PRESSURE = "neighboring liquid pressure";

			// Token: 0x04007DD3 RID: 32211
			public static LocString CIRCUIT_OVERLOADED = "an overloaded circuit";

			// Token: 0x04007DD4 RID: 32212
			public static LocString LOGIC_CIRCUIT_OVERLOADED = "an overloaded logic circuit";

			// Token: 0x04007DD5 RID: 32213
			public static LocString MICROMETEORITE = "micrometeorite";

			// Token: 0x04007DD6 RID: 32214
			public static LocString COMET = "falling space rocks";

			// Token: 0x04007DD7 RID: 32215
			public static LocString ROCKET = "rocket engine";
		}

		// Token: 0x02001BBB RID: 7099
		public static class AUTODISINFECTABLE
		{
			// Token: 0x020022FF RID: 8959
			public static class ENABLE_AUTODISINFECT
			{
				// Token: 0x04009A44 RID: 39492
				public static LocString NAME = "Enable Disinfect";

				// Token: 0x04009A45 RID: 39493
				public static LocString TOOLTIP = "Automatically disinfect this building when it becomes contaminated";
			}

			// Token: 0x02002300 RID: 8960
			public static class DISABLE_AUTODISINFECT
			{
				// Token: 0x04009A46 RID: 39494
				public static LocString NAME = "Disable Disinfect";

				// Token: 0x04009A47 RID: 39495
				public static LocString TOOLTIP = "Do not automatically disinfect this building";
			}

			// Token: 0x02002301 RID: 8961
			public static class NO_DISEASE
			{
				// Token: 0x04009A48 RID: 39496
				public static LocString TOOLTIP = "This building is clean";
			}
		}

		// Token: 0x02001BBC RID: 7100
		public static class DISINFECTABLE
		{
			// Token: 0x02002302 RID: 8962
			public static class ENABLE_DISINFECT
			{
				// Token: 0x04009A49 RID: 39497
				public static LocString NAME = "Disinfect";

				// Token: 0x04009A4A RID: 39498
				public static LocString TOOLTIP = "Mark this building for disinfection";
			}

			// Token: 0x02002303 RID: 8963
			public static class DISABLE_DISINFECT
			{
				// Token: 0x04009A4B RID: 39499
				public static LocString NAME = "Cancel Disinfect";

				// Token: 0x04009A4C RID: 39500
				public static LocString TOOLTIP = "Cancel this disinfect order";
			}

			// Token: 0x02002304 RID: 8964
			public static class NO_DISEASE
			{
				// Token: 0x04009A4D RID: 39501
				public static LocString TOOLTIP = "This building is already clean";
			}
		}

		// Token: 0x02001BBD RID: 7101
		public static class REPAIRABLE
		{
			// Token: 0x02002305 RID: 8965
			public static class ENABLE_AUTOREPAIR
			{
				// Token: 0x04009A4E RID: 39502
				public static LocString NAME = "Enable Autorepair";

				// Token: 0x04009A4F RID: 39503
				public static LocString TOOLTIP = "Automatically repair this building when damaged";
			}

			// Token: 0x02002306 RID: 8966
			public static class DISABLE_AUTOREPAIR
			{
				// Token: 0x04009A50 RID: 39504
				public static LocString NAME = "Disable Autorepair";

				// Token: 0x04009A51 RID: 39505
				public static LocString TOOLTIP = "Only repair this building when ordered";
			}
		}

		// Token: 0x02001BBE RID: 7102
		public static class AUTOMATABLE
		{
			// Token: 0x02002307 RID: 8967
			public static class ENABLE_AUTOMATIONONLY
			{
				// Token: 0x04009A52 RID: 39506
				public static LocString NAME = "Disable Manual";

				// Token: 0x04009A53 RID: 39507
				public static LocString TOOLTIP = "This building's storage may be accessed by Auto-Sweepers only\n\nDuplicants will not be permitted to add or remove materials from this building";
			}

			// Token: 0x02002308 RID: 8968
			public static class DISABLE_AUTOMATIONONLY
			{
				// Token: 0x04009A54 RID: 39508
				public static LocString NAME = "Enable Manual";

				// Token: 0x04009A55 RID: 39509
				public static LocString TOOLTIP = "This building's storage may be accessed by both Duplicants and Auto-Sweeper buildings";
			}
		}
	}
}
