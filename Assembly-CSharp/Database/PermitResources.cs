using System;
using System.Collections.Generic;

namespace Database
{
	// Token: 0x02000CA5 RID: 3237
	public class PermitResources : ResourceSet<PermitResource>
	{
		// Token: 0x060065C3 RID: 26051 RVA: 0x0026E610 File Offset: 0x0026C810
		public PermitResources(ResourceSet parent)
			: base("PermitResources", parent)
		{
			this.Root = new ResourceSet<Resource>("Root", null);
			this.Permits = new Dictionary<string, IEnumerable<PermitResource>>();
			this.BuildingFacades = new BuildingFacades(this.Root);
			this.Permits.Add(this.BuildingFacades.Id, this.BuildingFacades.resources);
			this.EquippableFacades = new EquippableFacades(this.Root);
			this.Permits.Add(this.EquippableFacades.Id, this.EquippableFacades.resources);
			this.ArtableStages = new ArtableStages(this.Root);
			this.Permits.Add(this.ArtableStages.Id, this.ArtableStages.resources);
			this.StickerBombs = new StickerBombs(this.Root);
			this.Permits.Add(this.StickerBombs.Id, this.StickerBombs.resources);
			this.ClothingItems = new ClothingItems(this.Root);
			this.ClothingOutfits = new ClothingOutfits(this.Root, this.ClothingItems);
			this.Permits.Add(this.ClothingItems.Id, this.ClothingItems.resources);
			this.BalloonArtistFacades = new BalloonArtistFacades(this.Root);
			this.Permits.Add(this.BalloonArtistFacades.Id, this.BalloonArtistFacades.resources);
			this.MonumentParts = new MonumentParts(this.Root);
			foreach (IEnumerable<PermitResource> enumerable in this.Permits.Values)
			{
				this.resources.AddRange(enumerable);
			}
		}

		// Token: 0x060065C4 RID: 26052 RVA: 0x0026E7EC File Offset: 0x0026C9EC
		public void PostProcess()
		{
			this.BuildingFacades.PostProcess();
		}

		// Token: 0x040049BE RID: 18878
		public ResourceSet Root;

		// Token: 0x040049BF RID: 18879
		public BuildingFacades BuildingFacades;

		// Token: 0x040049C0 RID: 18880
		public EquippableFacades EquippableFacades;

		// Token: 0x040049C1 RID: 18881
		public ArtableStages ArtableStages;

		// Token: 0x040049C2 RID: 18882
		public StickerBombs StickerBombs;

		// Token: 0x040049C3 RID: 18883
		public ClothingItems ClothingItems;

		// Token: 0x040049C4 RID: 18884
		public ClothingOutfits ClothingOutfits;

		// Token: 0x040049C5 RID: 18885
		public MonumentParts MonumentParts;

		// Token: 0x040049C6 RID: 18886
		public BalloonArtistFacades BalloonArtistFacades;

		// Token: 0x040049C7 RID: 18887
		public Dictionary<string, IEnumerable<PermitResource>> Permits;
	}
}
