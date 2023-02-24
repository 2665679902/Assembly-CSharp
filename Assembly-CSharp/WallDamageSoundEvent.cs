using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000446 RID: 1094
public class WallDamageSoundEvent : SoundEvent
{
	// Token: 0x06001785 RID: 6021 RVA: 0x0007ADF8 File Offset: 0x00078FF8
	public WallDamageSoundEvent(string file_name, string sound_name, int frame, float min_interval)
		: base(file_name, sound_name, frame, true, false, min_interval, false)
	{
	}

	// Token: 0x06001786 RID: 6022 RVA: 0x0007AE08 File Offset: 0x00079008
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		Vector3 vector = default(Vector3);
		AggressiveChore.StatesInstance smi = behaviour.controller.gameObject.GetSMI<AggressiveChore.StatesInstance>();
		if (smi != null)
		{
			this.tile = smi.sm.wallCellToBreak;
			int audioCategory = WallDamageSoundEvent.GetAudioCategory(this.tile);
			vector = Grid.CellToPos(this.tile);
			vector.z = 0f;
			GameObject gameObject = behaviour.controller.gameObject;
			if (base.objectIsSelectedAndVisible)
			{
				vector = SoundEvent.AudioHighlightListenerPosition(vector);
			}
			if (base.objectIsSelectedAndVisible || SoundEvent.ShouldPlaySound(behaviour.controller, base.sound, base.soundHash, base.looping, this.isDynamic))
			{
				EventInstance eventInstance = SoundEvent.BeginOneShot(base.sound, vector, SoundEvent.GetVolume(base.objectIsSelectedAndVisible), false);
				eventInstance.setParameterByName("material_ID", (float)audioCategory, false);
				SoundEvent.EndOneShot(eventInstance);
			}
		}
	}

	// Token: 0x06001787 RID: 6023 RVA: 0x0007AEE4 File Offset: 0x000790E4
	private static int GetAudioCategory(int tile)
	{
		Element element = Grid.Element[tile];
		if (Grid.Foundation[tile])
		{
			return 12;
		}
		if (element.id == SimHashes.Dirt)
		{
			return 0;
		}
		if (element.id == SimHashes.CrushedIce || element.id == SimHashes.Ice || element.id == SimHashes.DirtyIce)
		{
			return 1;
		}
		if (element.id == SimHashes.OxyRock)
		{
			return 3;
		}
		if (element.HasTag(GameTags.Metal))
		{
			return 5;
		}
		if (element.HasTag(GameTags.RefinedMetal))
		{
			return 6;
		}
		if (element.id == SimHashes.Sand)
		{
			return 8;
		}
		if (element.id == SimHashes.Algae)
		{
			return 10;
		}
		return 7;
	}

	// Token: 0x04000D0E RID: 3342
	public int tile;
}
