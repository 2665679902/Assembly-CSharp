using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000C7E RID: 3198
	public class BalloonArtistFacadeResource : PermitResource
	{
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600652A RID: 25898 RVA: 0x0025ED03 File Offset: 0x0025CF03
		// (set) Token: 0x0600652B RID: 25899 RVA: 0x0025ED0B File Offset: 0x0025CF0B
		public string animFilename { get; private set; }

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x0600652C RID: 25900 RVA: 0x0025ED14 File Offset: 0x0025CF14
		// (set) Token: 0x0600652D RID: 25901 RVA: 0x0025ED1C File Offset: 0x0025CF1C
		public KAnimFile AnimFile { get; private set; }

		// Token: 0x0600652E RID: 25902 RVA: 0x0025ED28 File Offset: 0x0025CF28
		public BalloonArtistFacadeResource(string id, string name, string desc, PermitRarity rarity, string animFile, BalloonArtistFacadeType balloonFacadeType)
			: base(id, name, desc, PermitCategory.JoyResponse, rarity)
		{
			this.AnimFile = Assets.GetAnim(animFile);
			this.animFilename = animFile;
			this.balloonFacadeType = balloonFacadeType;
			Db.Get().Accessories.AddAccessories(id, this.AnimFile);
			this.balloonOverrideSymbolIDs = this.GetBalloonOverrideSymbolIDs();
			Debug.Assert(this.balloonOverrideSymbolIDs.Length != 0);
		}

		// Token: 0x0600652F RID: 25903 RVA: 0x0025ED98 File Offset: 0x0025CF98
		public override PermitPresentationInfo GetPermitPresentationInfo()
		{
			PermitPresentationInfo permitPresentationInfo = default(PermitPresentationInfo);
			permitPresentationInfo.sprite = ClothingItemResource.GetUISprite(this.AnimFile);
			permitPresentationInfo.SetFacadeForText(UI.KLEI_INVENTORY_SCREEN.BALLOON_ARTIST_FACADE_FOR);
			return permitPresentationInfo;
		}

		// Token: 0x06006530 RID: 25904 RVA: 0x0025EDD4 File Offset: 0x0025CFD4
		public BalloonOverrideSymbol GetNextOverride()
		{
			int num = this.nextSymbolIndex;
			this.nextSymbolIndex = (this.nextSymbolIndex + 1) % this.balloonOverrideSymbolIDs.Length;
			return new BalloonOverrideSymbol(this.animFilename, this.balloonOverrideSymbolIDs[num]);
		}

		// Token: 0x06006531 RID: 25905 RVA: 0x0025EE12 File Offset: 0x0025D012
		public BalloonOverrideSymbolIter GetSymbolIter()
		{
			return new BalloonOverrideSymbolIter(this);
		}

		// Token: 0x06006532 RID: 25906 RVA: 0x0025EE1F File Offset: 0x0025D01F
		public BalloonOverrideSymbol GetOverrideAt(int index)
		{
			return new BalloonOverrideSymbol(this.animFilename, this.balloonOverrideSymbolIDs[index]);
		}

		// Token: 0x06006533 RID: 25907 RVA: 0x0025EE34 File Offset: 0x0025D034
		private string[] GetBalloonOverrideSymbolIDs()
		{
			KAnim.Build build = this.AnimFile.GetData().build;
			BalloonArtistFacadeType balloonArtistFacadeType = this.balloonFacadeType;
			string[] array;
			if (balloonArtistFacadeType != BalloonArtistFacadeType.Single)
			{
				if (balloonArtistFacadeType != BalloonArtistFacadeType.ThreeSet)
				{
					throw new NotImplementedException();
				}
				array = new string[] { "body1", "body2", "body3" };
			}
			else
			{
				array = new string[] { "body" };
			}
			return array;
		}

		// Token: 0x04004665 RID: 18021
		private BalloonArtistFacadeType balloonFacadeType;

		// Token: 0x04004666 RID: 18022
		public readonly string[] balloonOverrideSymbolIDs;

		// Token: 0x04004667 RID: 18023
		public int nextSymbolIndex;
	}
}
