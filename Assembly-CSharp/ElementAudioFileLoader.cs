using System;

// Token: 0x02000550 RID: 1360
internal class ElementAudioFileLoader : AsyncCsvLoader<ElementAudioFileLoader, ElementsAudio.ElementAudioConfig>
{
	// Token: 0x06002091 RID: 8337 RVA: 0x000B1E2F File Offset: 0x000B002F
	public ElementAudioFileLoader()
		: base(Assets.instance.elementAudio)
	{
	}

	// Token: 0x06002092 RID: 8338 RVA: 0x000B1E41 File Offset: 0x000B0041
	public override void Run()
	{
		base.Run();
	}
}
