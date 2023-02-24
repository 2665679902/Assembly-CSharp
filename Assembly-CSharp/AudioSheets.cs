using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200043F RID: 1087
public abstract class AudioSheets : ScriptableObject
{
	// Token: 0x0600175B RID: 5979 RVA: 0x00079494 File Offset: 0x00077694
	public virtual void Initialize()
	{
		foreach (AudioSheet audioSheet in this.sheets)
		{
			foreach (AudioSheet.SoundInfo soundInfo in audioSheet.soundInfos)
			{
				if (DlcManager.IsContentActive(soundInfo.RequiredDlcId))
				{
					string text = soundInfo.Type;
					if (text == null || text == "")
					{
						text = audioSheet.defaultType;
					}
					this.CreateSound(soundInfo.File, soundInfo.Anim, text, soundInfo.MinInterval, soundInfo.Name0, soundInfo.Frame0, soundInfo.RequiredDlcId);
					this.CreateSound(soundInfo.File, soundInfo.Anim, text, soundInfo.MinInterval, soundInfo.Name1, soundInfo.Frame1, soundInfo.RequiredDlcId);
					this.CreateSound(soundInfo.File, soundInfo.Anim, text, soundInfo.MinInterval, soundInfo.Name2, soundInfo.Frame2, soundInfo.RequiredDlcId);
					this.CreateSound(soundInfo.File, soundInfo.Anim, text, soundInfo.MinInterval, soundInfo.Name3, soundInfo.Frame3, soundInfo.RequiredDlcId);
					this.CreateSound(soundInfo.File, soundInfo.Anim, text, soundInfo.MinInterval, soundInfo.Name4, soundInfo.Frame4, soundInfo.RequiredDlcId);
					this.CreateSound(soundInfo.File, soundInfo.Anim, text, soundInfo.MinInterval, soundInfo.Name5, soundInfo.Frame5, soundInfo.RequiredDlcId);
					this.CreateSound(soundInfo.File, soundInfo.Anim, text, soundInfo.MinInterval, soundInfo.Name6, soundInfo.Frame6, soundInfo.RequiredDlcId);
					this.CreateSound(soundInfo.File, soundInfo.Anim, text, soundInfo.MinInterval, soundInfo.Name7, soundInfo.Frame7, soundInfo.RequiredDlcId);
					this.CreateSound(soundInfo.File, soundInfo.Anim, text, soundInfo.MinInterval, soundInfo.Name8, soundInfo.Frame8, soundInfo.RequiredDlcId);
					this.CreateSound(soundInfo.File, soundInfo.Anim, text, soundInfo.MinInterval, soundInfo.Name9, soundInfo.Frame9, soundInfo.RequiredDlcId);
					this.CreateSound(soundInfo.File, soundInfo.Anim, text, soundInfo.MinInterval, soundInfo.Name10, soundInfo.Frame10, soundInfo.RequiredDlcId);
					this.CreateSound(soundInfo.File, soundInfo.Anim, text, soundInfo.MinInterval, soundInfo.Name11, soundInfo.Frame11, soundInfo.RequiredDlcId);
				}
			}
		}
	}

	// Token: 0x0600175C RID: 5980 RVA: 0x000797A0 File Offset: 0x000779A0
	private void CreateSound(string file_name, string anim_name, string type, float min_interval, string sound_name, int frame, string dlcId)
	{
		if (string.IsNullOrEmpty(sound_name))
		{
			return;
		}
		HashedString hashedString = file_name + "." + anim_name;
		AnimEvent animEvent = this.CreateSoundOfType(type, file_name, sound_name, frame, min_interval, dlcId);
		if (animEvent == null)
		{
			global::Debug.LogError("Unknown sound type: " + type);
			return;
		}
		List<AnimEvent> list = null;
		if (!this.events.TryGetValue(hashedString, out list))
		{
			list = new List<AnimEvent>();
			this.events[hashedString] = list;
		}
		list.Add(animEvent);
	}

	// Token: 0x0600175D RID: 5981
	protected abstract AnimEvent CreateSoundOfType(string type, string file_name, string sound_name, int frame, float min_interval, string dlcId);

	// Token: 0x0600175E RID: 5982 RVA: 0x0007981C File Offset: 0x00077A1C
	public List<AnimEvent> GetEvents(HashedString anim_id)
	{
		List<AnimEvent> list = null;
		this.events.TryGetValue(anim_id, out list);
		return list;
	}

	// Token: 0x04000CF8 RID: 3320
	public List<AudioSheet> sheets = new List<AudioSheet>();

	// Token: 0x04000CF9 RID: 3321
	public Dictionary<HashedString, List<AnimEvent>> events = new Dictionary<HashedString, List<AnimEvent>>();
}
