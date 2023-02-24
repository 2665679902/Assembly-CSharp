using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000C7C RID: 3196
	public class BalloonArtistFacades : ResourceSet<BalloonArtistFacadeResource>
	{
		// Token: 0x06006527 RID: 25895 RVA: 0x0025EA18 File Offset: 0x0025CC18
		public BalloonArtistFacades(ResourceSet parent)
			: base("BalloonArtistFacades", parent)
		{
			foreach (BalloonArtistFacades.Info info in BalloonArtistFacades.Infos_All)
			{
				this.Add(info.id, info.name, info.desc, info.rarity, info.animFile, info.balloonFacadeType);
			}
		}

		// Token: 0x06006528 RID: 25896 RVA: 0x0025EA78 File Offset: 0x0025CC78
		public void Add(string id, string name, string desc, PermitRarity rarity, string animFile, BalloonArtistFacadeType balloonFacadeType)
		{
			BalloonArtistFacadeResource balloonArtistFacadeResource = new BalloonArtistFacadeResource(id, name, desc, rarity, animFile, balloonFacadeType);
			this.resources.Add(balloonArtistFacadeResource);
		}

		// Token: 0x0400465E RID: 18014
		public static BalloonArtistFacades.Info[] Infos_Skins = new BalloonArtistFacades.Info[]
		{
			new BalloonArtistFacades.Info("BalloonRedFireEngineLongSparkles", EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_FIREENGINE_LONG_SPARKLES.NAME, EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_FIREENGINE_LONG_SPARKLES.DESC, PermitRarity.Common, "balloon_red_fireengine_long_sparkles_kanim", BalloonArtistFacadeType.ThreeSet),
			new BalloonArtistFacades.Info("BalloonYellowLongSparkles", EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_YELLOW_LONG_SPARKLES.NAME, EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_YELLOW_LONG_SPARKLES.DESC, PermitRarity.Common, "balloon_yellow_long_sparkles_kanim", BalloonArtistFacadeType.ThreeSet),
			new BalloonArtistFacades.Info("BalloonBlueLongSparkles", EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BLUE_LONG_SPARKLES.NAME, EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BLUE_LONG_SPARKLES.DESC, PermitRarity.Common, "balloon_blue_long_sparkles_kanim", BalloonArtistFacadeType.ThreeSet),
			new BalloonArtistFacades.Info("BalloonGreenLongSparkles", EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_GREEN_LONG_SPARKLES.NAME, EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_GREEN_LONG_SPARKLES.DESC, PermitRarity.Common, "balloon_green_long_sparkles_kanim", BalloonArtistFacadeType.ThreeSet),
			new BalloonArtistFacades.Info("BalloonPinkLongSparkles", EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_PINK_LONG_SPARKLES.NAME, EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_PINK_LONG_SPARKLES.DESC, PermitRarity.Common, "balloon_pink_long_sparkles_kanim", BalloonArtistFacadeType.ThreeSet),
			new BalloonArtistFacades.Info("BalloonPurpleLongSparkles", EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_PURPLE_LONG_SPARKLES.NAME, EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_PURPLE_LONG_SPARKLES.DESC, PermitRarity.Common, "balloon_purple_long_sparkles_kanim", BalloonArtistFacadeType.ThreeSet),
			new BalloonArtistFacades.Info("BalloonBabyPacuEgg", EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BABY_PACU_EGG.NAME, EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BABY_PACU_EGG.DESC, PermitRarity.Splendid, "balloon_babypacu_egg_kanim", BalloonArtistFacadeType.ThreeSet),
			new BalloonArtistFacades.Info("BalloonBabyGlossyDreckoEgg", EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BABY_GLOSSY_DRECKO_EGG.NAME, EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BABY_GLOSSY_DRECKO_EGG.DESC, PermitRarity.Splendid, "balloon_babyglossydrecko_egg_kanim", BalloonArtistFacadeType.ThreeSet),
			new BalloonArtistFacades.Info("BalloonBabyHatchEgg", EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BABY_HATCH_EGG.NAME, EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BABY_HATCH_EGG.DESC, PermitRarity.Splendid, "balloon_babyhatch_egg_kanim", BalloonArtistFacadeType.ThreeSet),
			new BalloonArtistFacades.Info("BalloonBabyPokeshellEgg", EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BABY_POKESHELL_EGG.NAME, EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BABY_POKESHELL_EGG.DESC, PermitRarity.Splendid, "balloon_babypokeshell_egg_kanim", BalloonArtistFacadeType.ThreeSet),
			new BalloonArtistFacades.Info("BalloonBabyPuftEgg", EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BABY_PUFT_EGG.NAME, EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BABY_PUFT_EGG.DESC, PermitRarity.Splendid, "balloon_babypuft_egg_kanim", BalloonArtistFacadeType.ThreeSet),
			new BalloonArtistFacades.Info("BalloonBabyShovoleEgg", EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BABY_SHOVOLE_EGG.NAME, EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BABY_SHOVOLE_EGG.DESC, PermitRarity.Splendid, "balloon_babyshovole_egg_kanim", BalloonArtistFacadeType.ThreeSet),
			new BalloonArtistFacades.Info("BalloonBabyPipEgg", EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BABY_PIP_EGG.NAME, EQUIPMENT.PREFABS.EQUIPPABLEBALLOON.FACADES.BALLOON_BABY_PIP_EGG.DESC, PermitRarity.Splendid, "balloon_babypip_egg_kanim", BalloonArtistFacadeType.ThreeSet)
		};

		// Token: 0x0400465F RID: 18015
		public static BalloonArtistFacades.Info[] Infos_All = BalloonArtistFacades.Infos_Skins;

		// Token: 0x02001B13 RID: 6931
		public struct Info
		{
			// Token: 0x060094B7 RID: 38071 RVA: 0x0031D11B File Offset: 0x0031B31B
			public Info(string id, string name, string desc, PermitRarity rarity, string animFile, BalloonArtistFacadeType balloonFacadeType)
			{
				this.id = id;
				this.name = name;
				this.desc = desc;
				this.rarity = rarity;
				this.animFile = animFile;
				this.balloonFacadeType = balloonFacadeType;
			}

			// Token: 0x04007992 RID: 31122
			public string id;

			// Token: 0x04007993 RID: 31123
			public string name;

			// Token: 0x04007994 RID: 31124
			public string desc;

			// Token: 0x04007995 RID: 31125
			public PermitRarity rarity;

			// Token: 0x04007996 RID: 31126
			public string animFile;

			// Token: 0x04007997 RID: 31127
			public BalloonArtistFacadeType balloonFacadeType;
		}
	}
}
