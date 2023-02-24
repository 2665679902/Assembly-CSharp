using System;

// Token: 0x02000441 RID: 1089
public class ElementsAudio
{
	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x06001762 RID: 5986 RVA: 0x00079E66 File Offset: 0x00078066
	public static ElementsAudio Instance
	{
		get
		{
			if (ElementsAudio._instance == null)
			{
				ElementsAudio._instance = new ElementsAudio();
			}
			return ElementsAudio._instance;
		}
	}

	// Token: 0x06001763 RID: 5987 RVA: 0x00079E7E File Offset: 0x0007807E
	public void LoadData(ElementsAudio.ElementAudioConfig[] elements_audio_configs)
	{
		this.elementAudioConfigs = elements_audio_configs;
	}

	// Token: 0x06001764 RID: 5988 RVA: 0x00079E88 File Offset: 0x00078088
	public ElementsAudio.ElementAudioConfig GetConfigForElement(SimHashes id)
	{
		if (this.elementAudioConfigs != null)
		{
			for (int i = 0; i < this.elementAudioConfigs.Length; i++)
			{
				if (this.elementAudioConfigs[i].elementID == id)
				{
					return this.elementAudioConfigs[i];
				}
			}
		}
		return null;
	}

	// Token: 0x04000CFA RID: 3322
	private static ElementsAudio _instance;

	// Token: 0x04000CFB RID: 3323
	private ElementsAudio.ElementAudioConfig[] elementAudioConfigs;

	// Token: 0x02001059 RID: 4185
	public class ElementAudioConfig : Resource
	{
		// Token: 0x0400574C RID: 22348
		public SimHashes elementID;

		// Token: 0x0400574D RID: 22349
		public AmbienceType ambienceType = AmbienceType.None;

		// Token: 0x0400574E RID: 22350
		public SolidAmbienceType solidAmbienceType = SolidAmbienceType.None;

		// Token: 0x0400574F RID: 22351
		public string miningSound = "";

		// Token: 0x04005750 RID: 22352
		public string miningBreakSound = "";

		// Token: 0x04005751 RID: 22353
		public string oreBumpSound = "";

		// Token: 0x04005752 RID: 22354
		public string floorEventAudioCategory = "";

		// Token: 0x04005753 RID: 22355
		public string creatureChewSound = "";
	}
}
