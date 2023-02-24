using System;

// Token: 0x02000074 RID: 116
[Serializable]
public class WidgetSoundPlayer
{
	// Token: 0x060004B5 RID: 1205 RVA: 0x00017AF2 File Offset: 0x00015CF2
	public virtual string GetDefaultPath(int idx)
	{
		return "";
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x00017AF9 File Offset: 0x00015CF9
	public virtual WidgetSoundPlayer.WidgetSoundEvent[] widget_sound_events()
	{
		return null;
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x00017AFC File Offset: 0x00015CFC
	public void Play(int sound_event_idx)
	{
		if (!this.Enabled)
		{
			return;
		}
		WidgetSoundPlayer.WidgetSoundEvent widgetSoundEvent = default(WidgetSoundPlayer.WidgetSoundEvent);
		for (int i = 0; i < this.widget_sound_events().Length; i++)
		{
			if (sound_event_idx == this.widget_sound_events()[i].idx)
			{
				widgetSoundEvent = this.widget_sound_events()[sound_event_idx];
				break;
			}
		}
		if (!KInputManager.isFocused || !widgetSoundEvent.PlaySound || widgetSoundEvent.Name == null || widgetSoundEvent.Name.Length < 0 || widgetSoundEvent.Name == "")
		{
			return;
		}
		KFMOD.PlayUISound(WidgetSoundPlayer.getSoundPath((widgetSoundEvent.OverrideAssetName == "") ? this.GetDefaultPath(widgetSoundEvent.idx) : widgetSoundEvent.OverrideAssetName));
	}

	// Token: 0x040004F8 RID: 1272
	public bool Enabled = true;

	// Token: 0x040004F9 RID: 1273
	public static Func<string, string> getSoundPath;

	// Token: 0x020009C5 RID: 2501
	[Serializable]
	public struct WidgetSoundEvent
	{
		// Token: 0x06005377 RID: 21367 RVA: 0x0009BD2D File Offset: 0x00099F2D
		public WidgetSoundEvent(int idx, string Name, string OverrideAssetName, bool PlaySound)
		{
			this.idx = idx;
			this.Name = Name;
			this.OverrideAssetName = OverrideAssetName;
			this.PlaySound = PlaySound;
		}

		// Token: 0x040021D7 RID: 8663
		public string Name;

		// Token: 0x040021D8 RID: 8664
		public string OverrideAssetName;

		// Token: 0x040021D9 RID: 8665
		public int idx;

		// Token: 0x040021DA RID: 8666
		public bool PlaySound;
	}
}
