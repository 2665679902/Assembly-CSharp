using System;

namespace Database
{
	// Token: 0x02000C7F RID: 3199
	public readonly struct BalloonOverrideSymbol
	{
		// Token: 0x06006534 RID: 25908 RVA: 0x0025EE9C File Offset: 0x0025D09C
		public BalloonOverrideSymbol(string animFileID, string animFileSymbolID)
		{
			if (string.IsNullOrEmpty(animFileID) || string.IsNullOrEmpty(animFileSymbolID))
			{
				this = default(BalloonOverrideSymbol);
				return;
			}
			this.animFileID = animFileID;
			this.animFileSymbolID = animFileSymbolID;
			this.animFile = Assets.GetAnim(animFileID);
			this.symbol = this.animFile.Value.GetData().build.GetSymbol(animFileSymbolID);
		}

		// Token: 0x06006535 RID: 25909 RVA: 0x0025EF10 File Offset: 0x0025D110
		public void ApplyTo(BalloonArtist.Instance artist)
		{
			artist.SetBalloonSymbolOverride(this);
		}

		// Token: 0x06006536 RID: 25910 RVA: 0x0025EF1E File Offset: 0x0025D11E
		public void ApplyTo(BalloonFX.Instance balloon)
		{
			balloon.SetBalloonSymbolOverride(this);
		}

		// Token: 0x04004668 RID: 18024
		public readonly Option<KAnim.Build.Symbol> symbol;

		// Token: 0x04004669 RID: 18025
		public readonly Option<KAnimFile> animFile;

		// Token: 0x0400466A RID: 18026
		public readonly string animFileID;

		// Token: 0x0400466B RID: 18027
		public readonly string animFileSymbolID;
	}
}
