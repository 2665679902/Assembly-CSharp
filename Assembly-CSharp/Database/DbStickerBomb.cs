using System;

namespace Database
{
	// Token: 0x02000CB8 RID: 3256
	public class DbStickerBomb : PermitResource
	{
		// Token: 0x060065FF RID: 26111 RVA: 0x002724A3 File Offset: 0x002706A3
		public DbStickerBomb(string id, string stickerName, PermitRarity rarity, string animfilename, string sticker)
			: base(id, stickerName, "TODO:DbStickers", PermitCategory.Artwork, rarity)
		{
			this.id = id;
			this.sticker = sticker;
			this.animFile = Assets.GetAnim(animfilename);
		}

		// Token: 0x06006600 RID: 26112 RVA: 0x002724D8 File Offset: 0x002706D8
		public override PermitPresentationInfo GetPermitPresentationInfo()
		{
			return new PermitPresentationInfo
			{
				sprite = Def.GetUISpriteFromMultiObjectAnim(this.animFile, string.Format("{0}_{1}", "idle_sticker", this.sticker), false, string.Format("{0}_{1}", "sticker", this.sticker))
			};
		}

		// Token: 0x04004A65 RID: 19045
		public string id;

		// Token: 0x04004A66 RID: 19046
		public string sticker;

		// Token: 0x04004A67 RID: 19047
		public KAnimFile animFile;

		// Token: 0x04004A68 RID: 19048
		private const string stickerAnimPrefix = "idle_sticker";

		// Token: 0x04004A69 RID: 19049
		private const string stickerSymbolPrefix = "sticker";
	}
}
