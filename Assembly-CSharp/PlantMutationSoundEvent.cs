using System;
using UnityEngine;

// Token: 0x02000424 RID: 1060
public class PlantMutationSoundEvent : SoundEvent
{
	// Token: 0x060016BB RID: 5819 RVA: 0x00076108 File Offset: 0x00074308
	public PlantMutationSoundEvent(string file_name, string sound_name, int frame, float min_interval)
		: base(file_name, sound_name, frame, false, false, min_interval, true)
	{
	}

	// Token: 0x060016BC RID: 5820 RVA: 0x00076118 File Offset: 0x00074318
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		MutantPlant component = behaviour.controller.gameObject.GetComponent<MutantPlant>();
		Vector3 position = behaviour.GetComponent<Transform>().GetPosition();
		if (component != null)
		{
			for (int i = 0; i < component.GetSoundEvents().Count; i++)
			{
				SoundEvent.PlayOneShot(component.GetSoundEvents()[i], position, 1f);
			}
		}
	}
}
