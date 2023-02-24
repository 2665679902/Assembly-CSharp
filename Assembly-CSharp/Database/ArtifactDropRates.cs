using System;
using TUNING;

namespace Database
{
	// Token: 0x02000C79 RID: 3193
	public class ArtifactDropRates : ResourceSet<ArtifactDropRate>
	{
		// Token: 0x06006521 RID: 25889 RVA: 0x0025D807 File Offset: 0x0025BA07
		public ArtifactDropRates(ResourceSet parent)
			: base("ArtifactDropRates", parent)
		{
			this.CreateDropRates();
		}

		// Token: 0x06006522 RID: 25890 RVA: 0x0025D81C File Offset: 0x0025BA1C
		private void CreateDropRates()
		{
			this.None = new ArtifactDropRate();
			this.None.AddItem(DECOR.SPACEARTIFACT.TIER_NONE, 1f);
			base.Add(this.None);
			this.Bad = new ArtifactDropRate();
			this.Bad.AddItem(DECOR.SPACEARTIFACT.TIER_NONE, 10f);
			this.Bad.AddItem(DECOR.SPACEARTIFACT.TIER0, 5f);
			this.Bad.AddItem(DECOR.SPACEARTIFACT.TIER1, 3f);
			this.Bad.AddItem(DECOR.SPACEARTIFACT.TIER2, 2f);
			base.Add(this.Bad);
			this.Mediocre = new ArtifactDropRate();
			this.Mediocre.AddItem(DECOR.SPACEARTIFACT.TIER_NONE, 10f);
			this.Mediocre.AddItem(DECOR.SPACEARTIFACT.TIER1, 5f);
			this.Mediocre.AddItem(DECOR.SPACEARTIFACT.TIER2, 3f);
			this.Mediocre.AddItem(DECOR.SPACEARTIFACT.TIER3, 2f);
			base.Add(this.Mediocre);
			this.Good = new ArtifactDropRate();
			this.Good.AddItem(DECOR.SPACEARTIFACT.TIER_NONE, 10f);
			this.Good.AddItem(DECOR.SPACEARTIFACT.TIER2, 5f);
			this.Good.AddItem(DECOR.SPACEARTIFACT.TIER3, 3f);
			this.Good.AddItem(DECOR.SPACEARTIFACT.TIER4, 2f);
			base.Add(this.Good);
			this.Great = new ArtifactDropRate();
			this.Great.AddItem(DECOR.SPACEARTIFACT.TIER_NONE, 10f);
			this.Great.AddItem(DECOR.SPACEARTIFACT.TIER3, 5f);
			this.Great.AddItem(DECOR.SPACEARTIFACT.TIER4, 3f);
			this.Great.AddItem(DECOR.SPACEARTIFACT.TIER5, 2f);
			base.Add(this.Great);
			this.Amazing = new ArtifactDropRate();
			this.Amazing.AddItem(DECOR.SPACEARTIFACT.TIER_NONE, 10f);
			this.Amazing.AddItem(DECOR.SPACEARTIFACT.TIER3, 3f);
			this.Amazing.AddItem(DECOR.SPACEARTIFACT.TIER4, 5f);
			this.Amazing.AddItem(DECOR.SPACEARTIFACT.TIER5, 2f);
			base.Add(this.Amazing);
			this.Perfect = new ArtifactDropRate();
			this.Perfect.AddItem(DECOR.SPACEARTIFACT.TIER_NONE, 10f);
			this.Perfect.AddItem(DECOR.SPACEARTIFACT.TIER4, 6f);
			this.Perfect.AddItem(DECOR.SPACEARTIFACT.TIER5, 4f);
			base.Add(this.Perfect);
		}

		// Token: 0x04004617 RID: 17943
		public ArtifactDropRate None;

		// Token: 0x04004618 RID: 17944
		public ArtifactDropRate Bad;

		// Token: 0x04004619 RID: 17945
		public ArtifactDropRate Mediocre;

		// Token: 0x0400461A RID: 17946
		public ArtifactDropRate Good;

		// Token: 0x0400461B RID: 17947
		public ArtifactDropRate Great;

		// Token: 0x0400461C RID: 17948
		public ArtifactDropRate Amazing;

		// Token: 0x0400461D RID: 17949
		public ArtifactDropRate Perfect;
	}
}
