using System;

namespace TUNING
{
	// Token: 0x02000D1C RID: 3356
	public class FIXEDTRAITS
	{
		// Token: 0x02001B6D RID: 7021
		public class SUNLIGHT
		{
			// Token: 0x04007BC4 RID: 31684
			public static int DEFAULT_SPACED_OUT_SUNLIGHT = 40000;

			// Token: 0x04007BC5 RID: 31685
			public static int NONE = 0;

			// Token: 0x04007BC6 RID: 31686
			public static int VERY_VERY_LOW = (int)((float)FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 0.25f);

			// Token: 0x04007BC7 RID: 31687
			public static int VERY_LOW = (int)((float)FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 0.5f);

			// Token: 0x04007BC8 RID: 31688
			public static int LOW = (int)((float)FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 0.75f);

			// Token: 0x04007BC9 RID: 31689
			public static int MED_LOW = (int)((float)FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 0.875f);

			// Token: 0x04007BCA RID: 31690
			public static int MED = FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT;

			// Token: 0x04007BCB RID: 31691
			public static int MED_HIGH = (int)((float)FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 1.25f);

			// Token: 0x04007BCC RID: 31692
			public static int HIGH = (int)((float)FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 1.5f);

			// Token: 0x04007BCD RID: 31693
			public static int VERY_HIGH = FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 2;

			// Token: 0x04007BCE RID: 31694
			public static int VERY_VERY_HIGH = (int)((float)FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 2.5f);

			// Token: 0x04007BCF RID: 31695
			public static int VERY_VERY_VERY_HIGH = FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 3;

			// Token: 0x04007BD0 RID: 31696
			public static int DEFAULT_VALUE = FIXEDTRAITS.SUNLIGHT.VERY_HIGH;

			// Token: 0x02002115 RID: 8469
			public class NAME
			{
				// Token: 0x04009320 RID: 37664
				public static string NONE = "sunlightNone";

				// Token: 0x04009321 RID: 37665
				public static string VERY_VERY_LOW = "sunlightVeryVeryLow";

				// Token: 0x04009322 RID: 37666
				public static string VERY_LOW = "sunlightVeryLow";

				// Token: 0x04009323 RID: 37667
				public static string LOW = "sunlightLow";

				// Token: 0x04009324 RID: 37668
				public static string MED_LOW = "sunlightMedLow";

				// Token: 0x04009325 RID: 37669
				public static string MED = "sunlightMed";

				// Token: 0x04009326 RID: 37670
				public static string MED_HIGH = "sunlightMedHigh";

				// Token: 0x04009327 RID: 37671
				public static string HIGH = "sunlightHigh";

				// Token: 0x04009328 RID: 37672
				public static string VERY_HIGH = "sunlightVeryHigh";

				// Token: 0x04009329 RID: 37673
				public static string VERY_VERY_HIGH = "sunlightVeryVeryHigh";

				// Token: 0x0400932A RID: 37674
				public static string VERY_VERY_VERY_HIGH = "sunlightVeryVeryVeryHigh";

				// Token: 0x0400932B RID: 37675
				public static string DEFAULT = FIXEDTRAITS.SUNLIGHT.NAME.VERY_HIGH;
			}
		}

		// Token: 0x02001B6E RID: 7022
		public class COSMICRADIATION
		{
			// Token: 0x04007BD1 RID: 31697
			public static int BASELINE = 250;

			// Token: 0x04007BD2 RID: 31698
			public static int NONE = 0;

			// Token: 0x04007BD3 RID: 31699
			public static int VERY_VERY_LOW = (int)((float)FIXEDTRAITS.COSMICRADIATION.BASELINE * 0.25f);

			// Token: 0x04007BD4 RID: 31700
			public static int VERY_LOW = (int)((float)FIXEDTRAITS.COSMICRADIATION.BASELINE * 0.5f);

			// Token: 0x04007BD5 RID: 31701
			public static int LOW = (int)((float)FIXEDTRAITS.COSMICRADIATION.BASELINE * 0.75f);

			// Token: 0x04007BD6 RID: 31702
			public static int MED_LOW = (int)((float)FIXEDTRAITS.COSMICRADIATION.BASELINE * 0.875f);

			// Token: 0x04007BD7 RID: 31703
			public static int MED = FIXEDTRAITS.COSMICRADIATION.BASELINE;

			// Token: 0x04007BD8 RID: 31704
			public static int MED_HIGH = (int)((float)FIXEDTRAITS.COSMICRADIATION.BASELINE * 1.25f);

			// Token: 0x04007BD9 RID: 31705
			public static int HIGH = (int)((float)FIXEDTRAITS.COSMICRADIATION.BASELINE * 1.5f);

			// Token: 0x04007BDA RID: 31706
			public static int VERY_HIGH = FIXEDTRAITS.COSMICRADIATION.BASELINE * 2;

			// Token: 0x04007BDB RID: 31707
			public static int VERY_VERY_HIGH = FIXEDTRAITS.COSMICRADIATION.BASELINE * 3;

			// Token: 0x04007BDC RID: 31708
			public static int DEFAULT_VALUE = FIXEDTRAITS.COSMICRADIATION.MED;

			// Token: 0x04007BDD RID: 31709
			public static float TELESCOPE_RADIATION_SHIELDING = 0.5f;

			// Token: 0x02002116 RID: 8470
			public class NAME
			{
				// Token: 0x0400932C RID: 37676
				public static string NONE = "cosmicRadiationNone";

				// Token: 0x0400932D RID: 37677
				public static string VERY_VERY_LOW = "cosmicRadiationVeryVeryLow";

				// Token: 0x0400932E RID: 37678
				public static string VERY_LOW = "cosmicRadiationVeryLow";

				// Token: 0x0400932F RID: 37679
				public static string LOW = "cosmicRadiationLow";

				// Token: 0x04009330 RID: 37680
				public static string MED_LOW = "cosmicRadiationMedLow";

				// Token: 0x04009331 RID: 37681
				public static string MED = "cosmicRadiationMed";

				// Token: 0x04009332 RID: 37682
				public static string MED_HIGH = "cosmicRadiationMedHigh";

				// Token: 0x04009333 RID: 37683
				public static string HIGH = "cosmicRadiationHigh";

				// Token: 0x04009334 RID: 37684
				public static string VERY_HIGH = "cosmicRadiationVeryHigh";

				// Token: 0x04009335 RID: 37685
				public static string VERY_VERY_HIGH = "cosmicRadiationVeryVeryHigh";

				// Token: 0x04009336 RID: 37686
				public static string DEFAULT = FIXEDTRAITS.COSMICRADIATION.NAME.MED;
			}
		}
	}
}
