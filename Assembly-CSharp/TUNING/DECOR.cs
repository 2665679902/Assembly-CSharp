using System;
using STRINGS;

namespace TUNING
{
	// Token: 0x02000D21 RID: 3361
	public class DECOR
	{
		// Token: 0x04004C99 RID: 19609
		public static int LIT_BONUS = 15;

		// Token: 0x04004C9A RID: 19610
		public static readonly EffectorValues NONE = new EffectorValues
		{
			amount = 0,
			radius = 0
		};

		// Token: 0x02001B76 RID: 7030
		public class BONUS
		{
			// Token: 0x04007BF6 RID: 31734
			public static readonly EffectorValues TIER0 = new EffectorValues
			{
				amount = 10,
				radius = 1
			};

			// Token: 0x04007BF7 RID: 31735
			public static readonly EffectorValues TIER1 = new EffectorValues
			{
				amount = 15,
				radius = 2
			};

			// Token: 0x04007BF8 RID: 31736
			public static readonly EffectorValues TIER2 = new EffectorValues
			{
				amount = 20,
				radius = 3
			};

			// Token: 0x04007BF9 RID: 31737
			public static readonly EffectorValues TIER3 = new EffectorValues
			{
				amount = 25,
				radius = 4
			};

			// Token: 0x04007BFA RID: 31738
			public static readonly EffectorValues TIER4 = new EffectorValues
			{
				amount = 30,
				radius = 5
			};

			// Token: 0x04007BFB RID: 31739
			public static readonly EffectorValues TIER5 = new EffectorValues
			{
				amount = 35,
				radius = 6
			};

			// Token: 0x04007BFC RID: 31740
			public static readonly EffectorValues TIER6 = new EffectorValues
			{
				amount = 50,
				radius = 7
			};

			// Token: 0x04007BFD RID: 31741
			public static readonly EffectorValues TIER7 = new EffectorValues
			{
				amount = 80,
				radius = 7
			};

			// Token: 0x04007BFE RID: 31742
			public static readonly EffectorValues TIER8 = new EffectorValues
			{
				amount = 200,
				radius = 8
			};
		}

		// Token: 0x02001B77 RID: 7031
		public class PENALTY
		{
			// Token: 0x04007BFF RID: 31743
			public static readonly EffectorValues TIER0 = new EffectorValues
			{
				amount = -5,
				radius = 1
			};

			// Token: 0x04007C00 RID: 31744
			public static readonly EffectorValues TIER1 = new EffectorValues
			{
				amount = -10,
				radius = 2
			};

			// Token: 0x04007C01 RID: 31745
			public static readonly EffectorValues TIER2 = new EffectorValues
			{
				amount = -15,
				radius = 3
			};

			// Token: 0x04007C02 RID: 31746
			public static readonly EffectorValues TIER3 = new EffectorValues
			{
				amount = -20,
				radius = 4
			};

			// Token: 0x04007C03 RID: 31747
			public static readonly EffectorValues TIER4 = new EffectorValues
			{
				amount = -20,
				radius = 5
			};

			// Token: 0x04007C04 RID: 31748
			public static readonly EffectorValues TIER5 = new EffectorValues
			{
				amount = -25,
				radius = 6
			};
		}

		// Token: 0x02001B78 RID: 7032
		public class SPACEARTIFACT
		{
			// Token: 0x04007C05 RID: 31749
			public static readonly ArtifactTier TIER_NONE = new ArtifactTier(UI.SPACEARTIFACTS.ARTIFACTTIERS.TIER_NONE.key, DECOR.NONE, 0f);

			// Token: 0x04007C06 RID: 31750
			public static readonly ArtifactTier TIER0 = new ArtifactTier(UI.SPACEARTIFACTS.ARTIFACTTIERS.TIER0.key, DECOR.BONUS.TIER0, 0.25f);

			// Token: 0x04007C07 RID: 31751
			public static readonly ArtifactTier TIER1 = new ArtifactTier(UI.SPACEARTIFACTS.ARTIFACTTIERS.TIER1.key, DECOR.BONUS.TIER2, 0.4f);

			// Token: 0x04007C08 RID: 31752
			public static readonly ArtifactTier TIER2 = new ArtifactTier(UI.SPACEARTIFACTS.ARTIFACTTIERS.TIER2.key, DECOR.BONUS.TIER4, 0.55f);

			// Token: 0x04007C09 RID: 31753
			public static readonly ArtifactTier TIER3 = new ArtifactTier(UI.SPACEARTIFACTS.ARTIFACTTIERS.TIER3.key, DECOR.BONUS.TIER5, 0.7f);

			// Token: 0x04007C0A RID: 31754
			public static readonly ArtifactTier TIER4 = new ArtifactTier(UI.SPACEARTIFACTS.ARTIFACTTIERS.TIER4.key, DECOR.BONUS.TIER6, 0.85f);

			// Token: 0x04007C0B RID: 31755
			public static readonly ArtifactTier TIER5 = new ArtifactTier(UI.SPACEARTIFACTS.ARTIFACTTIERS.TIER5.key, DECOR.BONUS.TIER7, 1f);
		}
	}
}
