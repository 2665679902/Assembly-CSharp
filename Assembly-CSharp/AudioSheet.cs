using System;
using UnityEngine;

// Token: 0x0200043E RID: 1086
[Serializable]
public class AudioSheet
{
	// Token: 0x04000CF5 RID: 3317
	public TextAsset asset;

	// Token: 0x04000CF6 RID: 3318
	public string defaultType;

	// Token: 0x04000CF7 RID: 3319
	public AudioSheet.SoundInfo[] soundInfos;

	// Token: 0x02001058 RID: 4184
	public class SoundInfo : Resource
	{
		// Token: 0x0400572F RID: 22319
		public string File;

		// Token: 0x04005730 RID: 22320
		public string Anim;

		// Token: 0x04005731 RID: 22321
		public string Type;

		// Token: 0x04005732 RID: 22322
		public string RequiredDlcId;

		// Token: 0x04005733 RID: 22323
		public float MinInterval;

		// Token: 0x04005734 RID: 22324
		public string Name0;

		// Token: 0x04005735 RID: 22325
		public int Frame0;

		// Token: 0x04005736 RID: 22326
		public string Name1;

		// Token: 0x04005737 RID: 22327
		public int Frame1;

		// Token: 0x04005738 RID: 22328
		public string Name2;

		// Token: 0x04005739 RID: 22329
		public int Frame2;

		// Token: 0x0400573A RID: 22330
		public string Name3;

		// Token: 0x0400573B RID: 22331
		public int Frame3;

		// Token: 0x0400573C RID: 22332
		public string Name4;

		// Token: 0x0400573D RID: 22333
		public int Frame4;

		// Token: 0x0400573E RID: 22334
		public string Name5;

		// Token: 0x0400573F RID: 22335
		public int Frame5;

		// Token: 0x04005740 RID: 22336
		public string Name6;

		// Token: 0x04005741 RID: 22337
		public int Frame6;

		// Token: 0x04005742 RID: 22338
		public string Name7;

		// Token: 0x04005743 RID: 22339
		public int Frame7;

		// Token: 0x04005744 RID: 22340
		public string Name8;

		// Token: 0x04005745 RID: 22341
		public int Frame8;

		// Token: 0x04005746 RID: 22342
		public string Name9;

		// Token: 0x04005747 RID: 22343
		public int Frame9;

		// Token: 0x04005748 RID: 22344
		public string Name10;

		// Token: 0x04005749 RID: 22345
		public int Frame10;

		// Token: 0x0400574A RID: 22346
		public string Name11;

		// Token: 0x0400574B RID: 22347
		public int Frame11;
	}
}
