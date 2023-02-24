using System;

// Token: 0x02000075 RID: 117
[Serializable]
public class ToggleSoundPlayer : WidgetSoundPlayer
{
	// Token: 0x060004B9 RID: 1209 RVA: 0x00017BCC File Offset: 0x00015DCC
	public override string GetDefaultPath(int idx)
	{
		return ToggleSoundPlayer.default_values[idx];
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x00017BD5 File Offset: 0x00015DD5
	public override WidgetSoundPlayer.WidgetSoundEvent[] widget_sound_events()
	{
		return this.toggle_widget_sound_events;
	}

	// Token: 0x040004FA RID: 1274
	public static readonly string[] default_values = new string[] { "HUD_Click", "HUD_Click_Deselect", "HUD_Mouseover", "Negative" };

	// Token: 0x040004FB RID: 1275
	public Func<bool> AcceptClickCondition;

	// Token: 0x040004FC RID: 1276
	public WidgetSoundPlayer.WidgetSoundEvent[] toggle_widget_sound_events = new WidgetSoundPlayer.WidgetSoundEvent[]
	{
		new WidgetSoundPlayer.WidgetSoundEvent(0, "On Use On", "", true),
		new WidgetSoundPlayer.WidgetSoundEvent(1, "On Use Off", "", true),
		new WidgetSoundPlayer.WidgetSoundEvent(2, "On Pointer Enter", "", true),
		new WidgetSoundPlayer.WidgetSoundEvent(3, "On Use Rejected", "", true)
	};

	// Token: 0x020009C6 RID: 2502
	public enum SoundEvents
	{
		// Token: 0x040021DC RID: 8668
		OnClick_On,
		// Token: 0x040021DD RID: 8669
		OnClick_Off,
		// Token: 0x040021DE RID: 8670
		OnPointerEnter,
		// Token: 0x040021DF RID: 8671
		OnClick_Rejected
	}
}
