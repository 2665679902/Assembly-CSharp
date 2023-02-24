using System;
using UnityEngine;

namespace Database
{
	// Token: 0x02000C80 RID: 3200
	public class BalloonOverrideSymbolIter
	{
		// Token: 0x06006537 RID: 25911 RVA: 0x0025EF2C File Offset: 0x0025D12C
		public BalloonOverrideSymbolIter(Option<BalloonArtistFacadeResource> facade)
		{
			global::Debug.Assert(facade.IsNone() || facade.Unwrap().balloonOverrideSymbolIDs.Length != 0);
			this.facade = facade;
			if (facade.IsSome())
			{
				this.index = UnityEngine.Random.Range(0, facade.Unwrap().balloonOverrideSymbolIDs.Length);
			}
			this.Next();
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x0025EF91 File Offset: 0x0025D191
		public BalloonOverrideSymbol Current()
		{
			return this.current;
		}

		// Token: 0x06006539 RID: 25913 RVA: 0x0025EF9C File Offset: 0x0025D19C
		public BalloonOverrideSymbol Next()
		{
			if (this.facade.IsSome())
			{
				BalloonArtistFacadeResource balloonArtistFacadeResource = this.facade.Unwrap();
				this.current = new BalloonOverrideSymbol(balloonArtistFacadeResource.animFilename, balloonArtistFacadeResource.balloonOverrideSymbolIDs[this.index]);
				this.index = (this.index + 1) % balloonArtistFacadeResource.balloonOverrideSymbolIDs.Length;
				return this.current;
			}
			return default(BalloonOverrideSymbol);
		}

		// Token: 0x0400466C RID: 18028
		public readonly Option<BalloonArtistFacadeResource> facade;

		// Token: 0x0400466D RID: 18029
		private BalloonOverrideSymbol current;

		// Token: 0x0400466E RID: 18030
		private int index;
	}
}
