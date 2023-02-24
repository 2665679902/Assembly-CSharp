using System;

// Token: 0x02000411 RID: 1041
public class CreatureVariationSoundEvent : SoundEvent
{
	// Token: 0x060015A4 RID: 5540 RVA: 0x000703EB File Offset: 0x0006E5EB
	public CreatureVariationSoundEvent(string file_name, string sound_name, int frame, bool do_load, bool is_looping, float min_interval, bool is_dynamic)
		: base(file_name, sound_name, frame, do_load, is_looping, min_interval, is_dynamic)
	{
	}

	// Token: 0x060015A5 RID: 5541 RVA: 0x00070400 File Offset: 0x0006E600
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		string text = base.sound;
		CreatureBrain component = behaviour.GetComponent<CreatureBrain>();
		if (component != null && !string.IsNullOrEmpty(component.symbolPrefix))
		{
			string sound = GlobalAssets.GetSound(StringFormatter.Combine(component.symbolPrefix, base.name), false);
			if (!string.IsNullOrEmpty(sound))
			{
				text = sound;
			}
		}
		base.PlaySound(behaviour, text);
	}
}
