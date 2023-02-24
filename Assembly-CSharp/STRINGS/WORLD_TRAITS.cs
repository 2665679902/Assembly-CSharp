using System;

namespace STRINGS
{
	// Token: 0x02000D4E RID: 3406
	public static class WORLD_TRAITS
	{
		// Token: 0x04004E61 RID: 20065
		public static LocString MISSING_TRAIT = "<missing traits>";

		// Token: 0x02001E1A RID: 7706
		public static class NO_TRAITS
		{
			// Token: 0x0400872C RID: 34604
			public static LocString NAME = "<i>This world is stable and has no unusual features.</i>";

			// Token: 0x0400872D RID: 34605
			public static LocString NAME_SHORTHAND = "No unusual features";

			// Token: 0x0400872E RID: 34606
			public static LocString DESCRIPTION = "This world exists in a particularly stable configuration each time it is encountered";
		}

		// Token: 0x02001E1B RID: 7707
		public static class BOULDERS_LARGE
		{
			// Token: 0x0400872F RID: 34607
			public static LocString NAME = "Large Boulders";

			// Token: 0x04008730 RID: 34608
			public static LocString DESCRIPTION = "Huge boulders make digging through this world more difficult";
		}

		// Token: 0x02001E1C RID: 7708
		public static class BOULDERS_MEDIUM
		{
			// Token: 0x04008731 RID: 34609
			public static LocString NAME = "Medium Boulders";

			// Token: 0x04008732 RID: 34610
			public static LocString DESCRIPTION = "Mid-sized boulders make digging through this world more difficult";
		}

		// Token: 0x02001E1D RID: 7709
		public static class BOULDERS_MIXED
		{
			// Token: 0x04008733 RID: 34611
			public static LocString NAME = "Mixed Boulders";

			// Token: 0x04008734 RID: 34612
			public static LocString DESCRIPTION = "Boulders of various sizes make digging through this world more difficult";
		}

		// Token: 0x02001E1E RID: 7710
		public static class BOULDERS_SMALL
		{
			// Token: 0x04008735 RID: 34613
			public static LocString NAME = "Small Boulders";

			// Token: 0x04008736 RID: 34614
			public static LocString DESCRIPTION = "Tiny boulders make digging through this world more difficult";
		}

		// Token: 0x02001E1F RID: 7711
		public static class DEEP_OIL
		{
			// Token: 0x04008737 RID: 34615
			public static LocString NAME = "Trapped Oil";

			// Token: 0x04008738 RID: 34616
			public static LocString DESCRIPTION = string.Concat(new string[]
			{
				"Most of the ",
				UI.PRE_KEYWORD,
				"Oil",
				UI.PST_KEYWORD,
				" in this world will need to be extracted with ",
				BUILDINGS.PREFABS.OILWELLCAP.NAME,
				"s"
			});
		}

		// Token: 0x02001E20 RID: 7712
		public static class FROZEN_CORE
		{
			// Token: 0x04008739 RID: 34617
			public static LocString NAME = "Frozen Core";

			// Token: 0x0400873A RID: 34618
			public static LocString DESCRIPTION = "This world has a chilly core of solid " + ELEMENTS.ICE.NAME;
		}

		// Token: 0x02001E21 RID: 7713
		public static class GEOACTIVE
		{
			// Token: 0x0400873B RID: 34619
			public static LocString NAME = "Geoactive";

			// Token: 0x0400873C RID: 34620
			public static LocString DESCRIPTION = string.Concat(new string[]
			{
				"This world has more ",
				UI.PRE_KEYWORD,
				"Geysers",
				UI.PST_KEYWORD,
				" and ",
				UI.PRE_KEYWORD,
				"Vents",
				UI.PST_KEYWORD,
				" than usual"
			});
		}

		// Token: 0x02001E22 RID: 7714
		public static class GEODES
		{
			// Token: 0x0400873D RID: 34621
			public static LocString NAME = "Geodes";

			// Token: 0x0400873E RID: 34622
			public static LocString DESCRIPTION = "Large geodes containing rare material caches are deposited across this world";
		}

		// Token: 0x02001E23 RID: 7715
		public static class GEODORMANT
		{
			// Token: 0x0400873F RID: 34623
			public static LocString NAME = "Geodormant";

			// Token: 0x04008740 RID: 34624
			public static LocString DESCRIPTION = string.Concat(new string[]
			{
				"This world has fewer ",
				UI.PRE_KEYWORD,
				"Geysers",
				UI.PST_KEYWORD,
				" and ",
				UI.PRE_KEYWORD,
				"Vents",
				UI.PST_KEYWORD,
				" than usual"
			});
		}

		// Token: 0x02001E24 RID: 7716
		public static class GLACIERS_LARGE
		{
			// Token: 0x04008741 RID: 34625
			public static LocString NAME = "Large Glaciers";

			// Token: 0x04008742 RID: 34626
			public static LocString DESCRIPTION = "Huge chunks of primordial " + ELEMENTS.ICE.NAME + " are scattered across this world";
		}

		// Token: 0x02001E25 RID: 7717
		public static class IRREGULAR_OIL
		{
			// Token: 0x04008743 RID: 34627
			public static LocString NAME = "Irregular Oil";

			// Token: 0x04008744 RID: 34628
			public static LocString DESCRIPTION = string.Concat(new string[]
			{
				"The ",
				UI.PRE_KEYWORD,
				"Oil",
				UI.PST_KEYWORD,
				" on this asteroid is anything but regular!"
			});
		}

		// Token: 0x02001E26 RID: 7718
		public static class MAGMA_VENTS
		{
			// Token: 0x04008745 RID: 34629
			public static LocString NAME = "Magma Channels";

			// Token: 0x04008746 RID: 34630
			public static LocString DESCRIPTION = "The " + ELEMENTS.MAGMA.NAME + " from this world's core has leaked into the mantle and crust";
		}

		// Token: 0x02001E27 RID: 7719
		public static class METAL_POOR
		{
			// Token: 0x04008747 RID: 34631
			public static LocString NAME = "Metal Poor";

			// Token: 0x04008748 RID: 34632
			public static LocString DESCRIPTION = string.Concat(new string[]
			{
				"There is a reduced amount of ",
				UI.PRE_KEYWORD,
				"Metal Ore",
				UI.PST_KEYWORD,
				" on this world, proceed with caution!"
			});
		}

		// Token: 0x02001E28 RID: 7720
		public static class METAL_RICH
		{
			// Token: 0x04008749 RID: 34633
			public static LocString NAME = "Metal Rich";

			// Token: 0x0400874A RID: 34634
			public static LocString DESCRIPTION = "This asteroid is an abundant source of " + UI.PRE_KEYWORD + "Metal Ore" + UI.PST_KEYWORD;
		}

		// Token: 0x02001E29 RID: 7721
		public static class MISALIGNED_START
		{
			// Token: 0x0400874B RID: 34635
			public static LocString NAME = "Alternate Pod Location";

			// Token: 0x0400874C RID: 34636
			public static LocString DESCRIPTION = "The " + BUILDINGS.PREFABS.HEADQUARTERSCOMPLETE.NAME + " didn't end up in the asteroid's exact center this time... but it's still nowhere near the surface";
		}

		// Token: 0x02001E2A RID: 7722
		public static class SLIME_SPLATS
		{
			// Token: 0x0400874D RID: 34637
			public static LocString NAME = "Slime Molds";

			// Token: 0x0400874E RID: 34638
			public static LocString DESCRIPTION = "Sickly " + ELEMENTS.SLIMEMOLD.NAME + " growths have crept all over this world";
		}

		// Token: 0x02001E2B RID: 7723
		public static class SUBSURFACE_OCEAN
		{
			// Token: 0x0400874F RID: 34639
			public static LocString NAME = "Subsurface Ocean";

			// Token: 0x04008750 RID: 34640
			public static LocString DESCRIPTION = "Below the crust of this world is a " + ELEMENTS.SALTWATER.NAME + " sea";
		}

		// Token: 0x02001E2C RID: 7724
		public static class VOLCANOES
		{
			// Token: 0x04008751 RID: 34641
			public static LocString NAME = "Volcanic Activity";

			// Token: 0x04008752 RID: 34642
			public static LocString DESCRIPTION = string.Concat(new string[]
			{
				"Several active ",
				UI.PRE_KEYWORD,
				"Volcanoes",
				UI.PST_KEYWORD,
				" have been detected in this world"
			});
		}

		// Token: 0x02001E2D RID: 7725
		public static class RADIOACTIVE_CRUST
		{
			// Token: 0x04008753 RID: 34643
			public static LocString NAME = "Radioactive Crust";

			// Token: 0x04008754 RID: 34644
			public static LocString DESCRIPTION = "Deposits of " + ELEMENTS.URANIUMORE.NAME + " are found in this world's crust";
		}

		// Token: 0x02001E2E RID: 7726
		public static class LUSH_CORE
		{
			// Token: 0x04008755 RID: 34645
			public static LocString NAME = "Lush Core";

			// Token: 0x04008756 RID: 34646
			public static LocString DESCRIPTION = "This world has a lush forest core";
		}

		// Token: 0x02001E2F RID: 7727
		public static class METAL_CAVES
		{
			// Token: 0x04008757 RID: 34647
			public static LocString NAME = "Metallic Caves";

			// Token: 0x04008758 RID: 34648
			public static LocString DESCRIPTION = "This world has caves of metal ore";
		}

		// Token: 0x02001E30 RID: 7728
		public static class DISTRESS_SIGNAL
		{
			// Token: 0x04008759 RID: 34649
			public static LocString NAME = "Frozen Friend";

			// Token: 0x0400875A RID: 34650
			public static LocString DESCRIPTION = "This world contains a frozen friend from a long time ago";
		}

		// Token: 0x02001E31 RID: 7729
		public static class CRASHED_SATELLITES
		{
			// Token: 0x0400875B RID: 34651
			public static LocString NAME = "Crashed Satellites";

			// Token: 0x0400875C RID: 34652
			public static LocString DESCRIPTION = "This world contains crashed radioactive satellites";
		}
	}
}
