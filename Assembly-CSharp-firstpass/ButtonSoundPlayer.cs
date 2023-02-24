using System;

// Token: 0x02000076 RID: 118
[Serializable]
public class ButtonSoundPlayer : WidgetSoundPlayer
{
	// Token: 0x060004BD RID: 1213 RVA: 0x00017C8C File Offset: 0x00015E8C
	public override string GetDefaultPath(int idx)
	{
		return ButtonSoundPlayer.default_values[idx];
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00017C95 File Offset: 0x00015E95
	public override WidgetSoundPlayer.WidgetSoundEvent[] widget_sound_events()
	{
		return this.button_widget_sound_events;
	}

	// Token: 0x040004FD RID: 1277
	public static string[] default_values = new string[] { "HUD_Click_Open", "HUD_Mouseover", "Negative" };

	// Token: 0x040004FE RID: 1278
	public Func<bool> AcceptClickCondition;

	// Token: 0x040004FF RID: 1279
	public WidgetSoundPlayer.WidgetSoundEvent[] button_widget_sound_events = new WidgetSoundPlayer.WidgetSoundEvent[]
	{
		new WidgetSoundPlayer.WidgetSoundEvent(0, "On Use", "", true),
		new WidgetSoundPlayer.WidgetSoundEvent(1, "On Pointer Enter", "", true),
		new WidgetSoundPlayer.WidgetSoundEvent(2, "On Use Rejected", "", true)
	};

	// Token: 0x020009C7 RID: 2503
	public enum SoundEvents
	{
		// Token: 0x040021E1 RID: 8673
		OnClick,
		// Token: 0x040021E2 RID: 8674
		OnPointerEnter,
		// Token: 0x040021E3 RID: 8675
		OnClick_Rejected
	}
}
