using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CB9 RID: 3257
	public class StickerBombs : ResourceSet<DbStickerBomb>
	{
		// Token: 0x06006601 RID: 26113 RVA: 0x0027252C File Offset: 0x0027072C
		public StickerBombs(ResourceSet parent)
			: base("StickerBombs", parent)
		{
			foreach (StickerBombs.Info info in StickerBombs.Infos_All)
			{
				this.Add(info.id, info.stickerName, info.rarity, info.animfilename, info.sticker);
			}
		}

		// Token: 0x06006602 RID: 26114 RVA: 0x00272588 File Offset: 0x00270788
		private DbStickerBomb Add(string id, string stickerType, PermitRarity rarity, string animfilename, string symbolName)
		{
			DbStickerBomb dbStickerBomb = new DbStickerBomb(id, stickerType, rarity, animfilename, symbolName);
			this.resources.Add(dbStickerBomb);
			return dbStickerBomb;
		}

		// Token: 0x06006603 RID: 26115 RVA: 0x002725AF File Offset: 0x002707AF
		public DbStickerBomb GetRandomSticker()
		{
			return this.resources.GetRandom<DbStickerBomb>();
		}

		// Token: 0x04004A6A RID: 19050
		public static StickerBombs.Info[] Infos_Default = new StickerBombs.Info[]
		{
			new StickerBombs.Info("a", STICKERNAMES.STICKER_A, PermitRarity.Universal, "sticker_a_kanim", "a"),
			new StickerBombs.Info("b", STICKERNAMES.STICKER_B, PermitRarity.Universal, "sticker_b_kanim", "b"),
			new StickerBombs.Info("c", STICKERNAMES.STICKER_C, PermitRarity.Universal, "sticker_c_kanim", "c"),
			new StickerBombs.Info("d", STICKERNAMES.STICKER_D, PermitRarity.Universal, "sticker_d_kanim", "d"),
			new StickerBombs.Info("e", STICKERNAMES.STICKER_E, PermitRarity.Universal, "sticker_e_kanim", "e"),
			new StickerBombs.Info("f", STICKERNAMES.STICKER_F, PermitRarity.Universal, "sticker_f_kanim", "f"),
			new StickerBombs.Info("g", STICKERNAMES.STICKER_G, PermitRarity.Universal, "sticker_g_kanim", "g"),
			new StickerBombs.Info("h", STICKERNAMES.STICKER_H, PermitRarity.Universal, "sticker_h_kanim", "h"),
			new StickerBombs.Info("rocket", STICKERNAMES.STICKER_ROCKET, PermitRarity.Universal, "sticker_rocket_kanim", "rocket"),
			new StickerBombs.Info("paperplane", STICKERNAMES.STICKER_PAPERPLANE, PermitRarity.Universal, "sticker_paperplane_kanim", "paperplane"),
			new StickerBombs.Info("plant", STICKERNAMES.STICKER_PLANT, PermitRarity.Universal, "sticker_plant_kanim", "plant"),
			new StickerBombs.Info("plantpot", STICKERNAMES.STICKER_PLANTPOT, PermitRarity.Universal, "sticker_plantpot_kanim", "plantpot"),
			new StickerBombs.Info("mushroom", STICKERNAMES.STICKER_MUSHROOM, PermitRarity.Universal, "sticker_mushroom_kanim", "mushroom"),
			new StickerBombs.Info("mermaid", STICKERNAMES.STICKER_MERMAID, PermitRarity.Universal, "sticker_mermaid_kanim", "mermaid"),
			new StickerBombs.Info("spacepet", STICKERNAMES.STICKER_SPACEPET, PermitRarity.Universal, "sticker_spacepet_kanim", "spacepet"),
			new StickerBombs.Info("spacepet2", STICKERNAMES.STICKER_SPACEPET2, PermitRarity.Universal, "sticker_spacepet2_kanim", "spacepet2"),
			new StickerBombs.Info("spacepet3", STICKERNAMES.STICKER_SPACEPET3, PermitRarity.Universal, "sticker_spacepet3_kanim", "spacepet3"),
			new StickerBombs.Info("spacepet4", STICKERNAMES.STICKER_SPACEPET4, PermitRarity.Universal, "sticker_spacepet4_kanim", "spacepet4"),
			new StickerBombs.Info("spacepet5", STICKERNAMES.STICKER_SPACEPET5, PermitRarity.Universal, "sticker_spacepet5_kanim", "spacepet5"),
			new StickerBombs.Info("unicorn", STICKERNAMES.STICKER_UNICORN, PermitRarity.Universal, "sticker_unicorn_kanim", "unicorn")
		};

		// Token: 0x04004A6B RID: 19051
		public static StickerBombs.Info[] Infos_Skins = new StickerBombs.Info[0];

		// Token: 0x04004A6C RID: 19052
		public static StickerBombs.Info[] Infos_All = StickerBombs.Infos_Default.Concat(StickerBombs.Infos_Skins);

		// Token: 0x02001B33 RID: 6963
		public struct Info
		{
			// Token: 0x060095CD RID: 38349 RVA: 0x00321D2B File Offset: 0x0031FF2B
			public Info(string id, string stickerName, PermitRarity rarity, string animfilename, string sticker)
			{
				this.id = id;
				this.stickerName = stickerName;
				this.rarity = rarity;
				this.animfilename = animfilename;
				this.sticker = sticker;
			}

			// Token: 0x04007AE5 RID: 31461
			public string id;

			// Token: 0x04007AE6 RID: 31462
			public string stickerName;

			// Token: 0x04007AE7 RID: 31463
			public PermitRarity rarity;

			// Token: 0x04007AE8 RID: 31464
			public string animfilename;

			// Token: 0x04007AE9 RID: 31465
			public string sticker;
		}
	}
}
