using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000C8A RID: 3210
	public class ClothingOutfits : ResourceSet<ClothingOutfitResource>
	{
		// Token: 0x06006569 RID: 25961 RVA: 0x002678DC File Offset: 0x00265ADC
		public ClothingOutfits(ResourceSet parent, ClothingItems items_resource)
			: base("ClothingOutfits", parent)
		{
			base.Initialize();
			this.Add("BasicBlack", new string[] { "TopBasicBlack", "BottomBasicBlack", "GlovesBasicBlack", "ShoesBasicBlack" }, UI.OUTFITS.BASIC_BLACK.NAME);
			this.Add("BasicWhite", new string[] { "TopBasicWhite", "BottomBasicWhite", "GlovesBasicWhite", "ShoesBasicWhite" }, UI.OUTFITS.BASIC_WHITE.NAME);
			this.Add("BasicRed", new string[] { "TopBasicRed", "BottomBasicRed", "GlovesBasicRed", "ShoesBasicRed" }, UI.OUTFITS.BASIC_RED.NAME);
			this.Add("BasicOrange", new string[] { "TopBasicOrange", "BottomBasicOrange", "GlovesBasicOrange", "ShoesBasicOrange" }, UI.OUTFITS.BASIC_ORANGE.NAME);
			this.Add("BasicYellow", new string[] { "TopBasicYellow", "BottomBasicYellow", "GlovesBasicYellow", "ShoesBasicYellow" }, UI.OUTFITS.BASIC_YELLOW.NAME);
			this.Add("BasicGreen", new string[] { "TopBasicGreen", "BottomBasicGreen", "GlovesBasicGreen", "ShoesBasicGreen" }, UI.OUTFITS.BASIC_GREEN.NAME);
			this.Add("BasicAqua", new string[] { "TopBasicAqua", "BottomBasicAqua", "GlovesBasicAqua", "ShoesBasicAqua" }, UI.OUTFITS.BASIC_AQUA.NAME);
			this.Add("BasicPurple", new string[] { "TopBasicPurple", "BottomBasicPurple", "GlovesBasicPurple", "ShoesBasicPurple" }, UI.OUTFITS.BASIC_PURPLE.NAME);
			this.Add("BasicPinkOrchid", new string[] { "TopBasicPinkOrchid", "BottomBasicPinkOrchid", "GlovesBasicPinkOrchid", "ShoesBasicPinkOrchid" }, UI.OUTFITS.BASIC_PINK_ORCHID.NAME);
			this.Add("BasicDeepRed", new string[] { "TopRaglanDeepRed", "ShortsBasicDeepRed", "GlovesAthleticRedDeep", "SocksAthleticDeepRed" }, UI.OUTFITS.BASIC_DEEPRED.NAME);
			this.Add("BasicOrangeSatsuma", new string[] { "TopRaglanSatsuma", "ShortsBasicSatsuma", "GlovesAthleticOrangeSatsuma", "SocksAthleticOrangeSatsuma" }, UI.OUTFITS.BASIC_SATSUMA.NAME);
			this.Add("BasicLemon", new string[] { "TopRaglanLemon", "ShortsBasicYellowcake", "GlovesAthleticYellowLemon", "SocksAthleticYellowLemon" }, UI.OUTFITS.BASIC_LEMON.NAME);
			this.Add("BasicBlueCobalt", new string[] { "TopRaglanCobalt", "ShortsBasicBlueCobalt", "GlovesAthleticBlueCobalt", "SocksAthleticBlueCobalt" }, UI.OUTFITS.BASIC_BLUE_COBALT.NAME);
			this.Add("BasicGreenKelly", new string[] { "TopRaglanKellyGreen", "ShortsBasicKellyGreen", "GlovesAthleticGreenKelly", "SocksAthleticGreenKelly" }, UI.OUTFITS.BASIC_GREEN_KELLY.NAME);
			this.Add("BasicPinkFlamingo", new string[] { "TopRaglanFlamingo", "ShortsBasicPinkFlamingo", "GlovesAthleticPinkFlamingo", "SocksAthleticPinkFlamingo" }, UI.OUTFITS.BASIC_PINK_FLAMINGO.NAME);
			this.Add("BasicGreyCharcoal", new string[] { "TopRaglanCharcoal", "ShortsBasicCharcoal", "GlovesAthleticGreyCharcoal", "SocksAthleticGreyCharcoal" }, UI.OUTFITS.BASIC_GREY_CHARCOAL.NAME);
			ClothingOutfitUtility.LoadClothingOutfitData(this);
		}

		// Token: 0x0600656A RID: 25962 RVA: 0x00267C64 File Offset: 0x00265E64
		public void Add(string id, string[] items_in_outfit, LocString name)
		{
			ClothingOutfitResource clothingOutfitResource = new ClothingOutfitResource(id, items_in_outfit, name);
			this.resources.Add(clothingOutfitResource);
		}

		// Token: 0x0600656B RID: 25963 RVA: 0x00267C86 File Offset: 0x00265E86
		public void SetDuplicantPersonalityOutfit(string personalityId, Option<string> outfit_id, ClothingOutfitUtility.OutfitType outfit_type = ClothingOutfitUtility.OutfitType.Clothing)
		{
			Db.Get().Personalities.Get(personalityId).Internal_SetOutfit(outfit_type, outfit_id);
			CustomClothingOutfits.Instance.Internal_SetDuplicantPersonalityOutfit(personalityId, outfit_id, outfit_type);
		}

		// Token: 0x02001B1D RID: 6941
		public class ClothingOutfitInfo
		{
			// Token: 0x170009E6 RID: 2534
			// (get) Token: 0x06009530 RID: 38192 RVA: 0x0031F346 File Offset: 0x0031D546
			// (set) Token: 0x06009531 RID: 38193 RVA: 0x0031F34E File Offset: 0x0031D54E
			public string id { get; set; }

			// Token: 0x170009E7 RID: 2535
			// (get) Token: 0x06009532 RID: 38194 RVA: 0x0031F357 File Offset: 0x0031D557
			// (set) Token: 0x06009533 RID: 38195 RVA: 0x0031F35F File Offset: 0x0031D55F
			public string name { get; set; }

			// Token: 0x170009E8 RID: 2536
			// (get) Token: 0x06009534 RID: 38196 RVA: 0x0031F368 File Offset: 0x0031D568
			// (set) Token: 0x06009535 RID: 38197 RVA: 0x0031F370 File Offset: 0x0031D570
			public List<ClothingOutfits.ClothingOutfitInfo.ClothingItem> items { get; set; }

			// Token: 0x0200210D RID: 8461
			public class ClothingItem
			{
				// Token: 0x17000A0C RID: 2572
				// (get) Token: 0x0600A5E2 RID: 42466 RVA: 0x0034B0AC File Offset: 0x003492AC
				// (set) Token: 0x0600A5E3 RID: 42467 RVA: 0x0034B0B4 File Offset: 0x003492B4
				public string id { get; set; }

				// Token: 0x17000A0D RID: 2573
				// (get) Token: 0x0600A5E4 RID: 42468 RVA: 0x0034B0BD File Offset: 0x003492BD
				// (set) Token: 0x0600A5E5 RID: 42469 RVA: 0x0034B0C5 File Offset: 0x003492C5
				public string name { get; set; }

				// Token: 0x17000A0E RID: 2574
				// (get) Token: 0x0600A5E6 RID: 42470 RVA: 0x0034B0CE File Offset: 0x003492CE
				// (set) Token: 0x0600A5E7 RID: 42471 RVA: 0x0034B0D6 File Offset: 0x003492D6
				public string description { get; set; }

				// Token: 0x17000A0F RID: 2575
				// (get) Token: 0x0600A5E8 RID: 42472 RVA: 0x0034B0DF File Offset: 0x003492DF
				// (set) Token: 0x0600A5E9 RID: 42473 RVA: 0x0034B0E7 File Offset: 0x003492E7
				public string category { get; set; }

				// Token: 0x17000A10 RID: 2576
				// (get) Token: 0x0600A5EA RID: 42474 RVA: 0x0034B0F0 File Offset: 0x003492F0
				// (set) Token: 0x0600A5EB RID: 42475 RVA: 0x0034B0F8 File Offset: 0x003492F8
				public string animFilename { get; set; }
			}
		}
	}
}
