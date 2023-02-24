using System;

// Token: 0x0200041F RID: 1055
public class LaserSoundEvent : SoundEvent
{
	// Token: 0x060016B1 RID: 5809 RVA: 0x00075D2C File Offset: 0x00073F2C
	public LaserSoundEvent(string file_name, string sound_name, int frame, float min_interval)
		: base(file_name, sound_name, frame, true, true, min_interval, false)
	{
		base.noiseValues = SoundEventVolumeCache.instance.GetVolume("LaserSoundEvent", sound_name);
	}
}
