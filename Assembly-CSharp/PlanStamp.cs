using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B5A RID: 2906
[AddComponentMenu("KMonoBehaviour/scripts/PlanStamp")]
public class PlanStamp : KMonoBehaviour
{
	// Token: 0x06005AA6 RID: 23206 RVA: 0x0020E949 File Offset: 0x0020CB49
	public void SetStamp(Sprite sprite, string Text)
	{
		this.StampImage.sprite = sprite;
		this.StampText.text = Text.ToUpper();
	}

	// Token: 0x04003D55 RID: 15701
	public PlanStamp.StampArt stampSprites;

	// Token: 0x04003D56 RID: 15702
	[SerializeField]
	private Image StampImage;

	// Token: 0x04003D57 RID: 15703
	[SerializeField]
	private Text StampText;

	// Token: 0x02001A07 RID: 6663
	[Serializable]
	public struct StampArt
	{
		// Token: 0x0400763D RID: 30269
		public Sprite UnderConstruction;

		// Token: 0x0400763E RID: 30270
		public Sprite NeedsResearch;

		// Token: 0x0400763F RID: 30271
		public Sprite SelectResource;

		// Token: 0x04007640 RID: 30272
		public Sprite NeedsRepair;

		// Token: 0x04007641 RID: 30273
		public Sprite NeedsPower;

		// Token: 0x04007642 RID: 30274
		public Sprite NeedsResource;

		// Token: 0x04007643 RID: 30275
		public Sprite NeedsGasPipe;

		// Token: 0x04007644 RID: 30276
		public Sprite NeedsLiquidPipe;
	}
}
