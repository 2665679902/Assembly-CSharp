using System;

namespace STRINGS
{
	// Token: 0x02000D46 RID: 3398
	public class ROBOTS
	{
		// Token: 0x04004E51 RID: 20049
		public static LocString CATEGORY_NAME = "Robots";

		// Token: 0x02001CF8 RID: 7416
		public class STATS
		{
			// Token: 0x02002CCD RID: 11469
			public class INTERNALBATTERY
			{
				// Token: 0x0400B74F RID: 46927
				public static LocString NAME = "Rechargeable Battery";

				// Token: 0x0400B750 RID: 46928
				public static LocString TOOLTIP = "When this bot's battery runs out it must temporarily stop working to go recharge";
			}

			// Token: 0x02002CCE RID: 11470
			public class INTERNALCHEMICALBATTERY
			{
				// Token: 0x0400B751 RID: 46929
				public static LocString NAME = "Chemical Battery";

				// Token: 0x0400B752 RID: 46930
				public static LocString TOOLTIP = "This bot will shut down permanently when its battery runs out";
			}
		}

		// Token: 0x02001CF9 RID: 7417
		public class ATTRIBUTES
		{
			// Token: 0x02002CCF RID: 11471
			public class INTERNALBATTERYDELTA
			{
				// Token: 0x0400B753 RID: 46931
				public static LocString NAME = "Rechargeable Battery Drain";

				// Token: 0x0400B754 RID: 46932
				public static LocString TOOLTIP = "The rate at which battery life is depleted";
			}
		}

		// Token: 0x02001CFA RID: 7418
		public class STATUSITEMS
		{
			// Token: 0x02002CD0 RID: 11472
			public class CANTREACHSTATION
			{
				// Token: 0x0400B755 RID: 46933
				public static LocString NAME = "Unreachable Dock";

				// Token: 0x0400B756 RID: 46934
				public static LocString DESC = "Obstacles are preventing {0} from heading home";

				// Token: 0x0400B757 RID: 46935
				public static LocString TOOLTIP = "Obstacles are preventing {0} from heading home";
			}

			// Token: 0x02002CD1 RID: 11473
			public class MOVINGTOCHARGESTATION
			{
				// Token: 0x0400B758 RID: 46936
				public static LocString NAME = "Traveling to Dock";

				// Token: 0x0400B759 RID: 46937
				public static LocString DESC = "{0} is on its way home to recharge";

				// Token: 0x0400B75A RID: 46938
				public static LocString TOOLTIP = "{0} is on its way home to recharge";
			}

			// Token: 0x02002CD2 RID: 11474
			public class LOWBATTERY
			{
				// Token: 0x0400B75B RID: 46939
				public static LocString NAME = "Low Battery";

				// Token: 0x0400B75C RID: 46940
				public static LocString DESC = "{0}'s battery is low and needs to recharge";

				// Token: 0x0400B75D RID: 46941
				public static LocString TOOLTIP = "{0}'s battery is low and needs to recharge";
			}

			// Token: 0x02002CD3 RID: 11475
			public class LOWBATTERYNOCHARGE
			{
				// Token: 0x0400B75E RID: 46942
				public static LocString NAME = "Low Battery";

				// Token: 0x0400B75F RID: 46943
				public static LocString DESC = "{0}'s battery is low\n\nThe internal battery cannot be recharged and this robot will cease functioning after it is depleted.";

				// Token: 0x0400B760 RID: 46944
				public static LocString TOOLTIP = "{0}'s battery is low\n\nThe internal battery cannot be recharged and this robot will cease functioning after it is depleted.";
			}

			// Token: 0x02002CD4 RID: 11476
			public class DEADBATTERY
			{
				// Token: 0x0400B761 RID: 46945
				public static LocString NAME = "Shut Down";

				// Token: 0x0400B762 RID: 46946
				public static LocString DESC = "RIP {0}\n\n{0}'s battery has been depleted and cannot be recharged";

				// Token: 0x0400B763 RID: 46947
				public static LocString TOOLTIP = "RIP {0}\n\n{0}'s battery has been depleted and cannot be recharged";
			}

			// Token: 0x02002CD5 RID: 11477
			public class DUSTBINFULL
			{
				// Token: 0x0400B764 RID: 46948
				public static LocString NAME = "Dust Bin Full";

				// Token: 0x0400B765 RID: 46949
				public static LocString DESC = "{0} must return to its dock to unload";

				// Token: 0x0400B766 RID: 46950
				public static LocString TOOLTIP = "{0} must return to its dock to unload";
			}

			// Token: 0x02002CD6 RID: 11478
			public class WORKING
			{
				// Token: 0x0400B767 RID: 46951
				public static LocString NAME = "Working";

				// Token: 0x0400B768 RID: 46952
				public static LocString DESC = "{0} is working diligently. Great job, {0}!";

				// Token: 0x0400B769 RID: 46953
				public static LocString TOOLTIP = "{0} is working diligently. Great job, {0}!";
			}

			// Token: 0x02002CD7 RID: 11479
			public class UNLOADINGSTORAGE
			{
				// Token: 0x0400B76A RID: 46954
				public static LocString NAME = "Unloading";

				// Token: 0x0400B76B RID: 46955
				public static LocString DESC = "{0} is emptying out its dust bin";

				// Token: 0x0400B76C RID: 46956
				public static LocString TOOLTIP = "{0} is emptying out its dust bin";
			}

			// Token: 0x02002CD8 RID: 11480
			public class CHARGING
			{
				// Token: 0x0400B76D RID: 46957
				public static LocString NAME = "Charging";

				// Token: 0x0400B76E RID: 46958
				public static LocString DESC = "{0} is recharging its battery";

				// Token: 0x0400B76F RID: 46959
				public static LocString TOOLTIP = "{0} is recharging its battery";
			}

			// Token: 0x02002CD9 RID: 11481
			public class REACTPOSITIVE
			{
				// Token: 0x0400B770 RID: 46960
				public static LocString NAME = "Happy Reaction";

				// Token: 0x0400B771 RID: 46961
				public static LocString DESC = "This bot saw something nice!";

				// Token: 0x0400B772 RID: 46962
				public static LocString TOOLTIP = "This bot saw something nice!";
			}

			// Token: 0x02002CDA RID: 11482
			public class REACTNEGATIVE
			{
				// Token: 0x0400B773 RID: 46963
				public static LocString NAME = "Bothered Reaction";

				// Token: 0x0400B774 RID: 46964
				public static LocString DESC = "This bot saw something upsetting";

				// Token: 0x0400B775 RID: 46965
				public static LocString TOOLTIP = "This bot saw something upsetting";
			}
		}

		// Token: 0x02001CFB RID: 7419
		public class MODELS
		{
			// Token: 0x02002CDB RID: 11483
			public class SCOUT
			{
				// Token: 0x0400B776 RID: 46966
				public static LocString NAME = "Rover";

				// Token: 0x0400B777 RID: 46967
				public static LocString DESC = "A curious bot that can remotely explore new " + UI.CLUSTERMAP.PLANETOID_KEYWORD + " locations.";
			}

			// Token: 0x02002CDC RID: 11484
			public class SWEEPBOT
			{
				// Token: 0x0400B778 RID: 46968
				public static LocString NAME = "Sweepy";

				// Token: 0x0400B779 RID: 46969
				public static LocString DESC = string.Concat(new string[]
				{
					"An automated sweeping robot.\n\nSweeps up ",
					UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
					" debris and ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" spills and stores the material back in its ",
					UI.FormatAsLink("Sweepy Dock", "SWEEPBOTSTATION"),
					"."
				});
			}
		}
	}
}
