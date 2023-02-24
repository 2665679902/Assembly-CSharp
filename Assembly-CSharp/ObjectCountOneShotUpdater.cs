using System;
using System.Collections.Generic;

// Token: 0x0200042A RID: 1066
internal class ObjectCountOneShotUpdater : OneShotSoundParameterUpdater
{
	// Token: 0x060016F4 RID: 5876 RVA: 0x0007706F File Offset: 0x0007526F
	public ObjectCountOneShotUpdater()
		: base("objectCount")
	{
	}

	// Token: 0x060016F5 RID: 5877 RVA: 0x0007708C File Offset: 0x0007528C
	public override void Update(float dt)
	{
		this.soundCounts.Clear();
	}

	// Token: 0x060016F6 RID: 5878 RVA: 0x0007709C File Offset: 0x0007529C
	public override void Play(OneShotSoundParameterUpdater.Sound sound)
	{
		UpdateObjectCountParameter.Settings settings = UpdateObjectCountParameter.GetSettings(sound.path, sound.description);
		int num = 0;
		this.soundCounts.TryGetValue(sound.path, out num);
		num = (this.soundCounts[sound.path] = num + 1);
		UpdateObjectCountParameter.ApplySettings(sound.ev, num, settings);
	}

	// Token: 0x04000CBB RID: 3259
	private Dictionary<HashedString, int> soundCounts = new Dictionary<HashedString, int>();
}
