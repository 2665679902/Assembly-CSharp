using System;
using System.Collections.Generic;

// Token: 0x02000427 RID: 1063
public class SoundEventVolumeCache : Singleton<SoundEventVolumeCache>
{
	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x060016C3 RID: 5827 RVA: 0x000765EA File Offset: 0x000747EA
	public static SoundEventVolumeCache instance
	{
		get
		{
			return Singleton<SoundEventVolumeCache>.Instance;
		}
	}

	// Token: 0x060016C4 RID: 5828 RVA: 0x000765F4 File Offset: 0x000747F4
	public void AddVolume(string animFile, string eventName, EffectorValues vals)
	{
		HashedString hashedString = new HashedString(animFile + ":" + eventName);
		if (!this.volumeCache.ContainsKey(hashedString))
		{
			this.volumeCache.Add(hashedString, vals);
			return;
		}
		this.volumeCache[hashedString] = vals;
	}

	// Token: 0x060016C5 RID: 5829 RVA: 0x00076640 File Offset: 0x00074840
	public EffectorValues GetVolume(string animFile, string eventName)
	{
		HashedString hashedString = new HashedString(animFile + ":" + eventName);
		if (!this.volumeCache.ContainsKey(hashedString))
		{
			return default(EffectorValues);
		}
		return this.volumeCache[hashedString];
	}

	// Token: 0x04000CAD RID: 3245
	public Dictionary<HashedString, EffectorValues> volumeCache = new Dictionary<HashedString, EffectorValues>();
}
