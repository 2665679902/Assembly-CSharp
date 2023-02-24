using System;

// Token: 0x02000917 RID: 2327
public class SetDefaults
{
	// Token: 0x060043B1 RID: 17329 RVA: 0x0017E354 File Offset: 0x0017C554
	public static void Initialize()
	{
		KSlider.DefaultSounds[0] = GlobalAssets.GetSound("Slider_Start", false);
		KSlider.DefaultSounds[1] = GlobalAssets.GetSound("Slider_Move", false);
		KSlider.DefaultSounds[2] = GlobalAssets.GetSound("Slider_End", false);
		KSlider.DefaultSounds[3] = GlobalAssets.GetSound("Slider_Boundary_Low", false);
		KSlider.DefaultSounds[4] = GlobalAssets.GetSound("Slider_Boundary_High", false);
		KScrollRect.DefaultSounds[KScrollRect.SoundType.OnMouseScroll] = GlobalAssets.GetSound("Mousewheel_Move", false);
		WidgetSoundPlayer.getSoundPath = new Func<string, string>(SetDefaults.GetSoundPath);
	}

	// Token: 0x060043B2 RID: 17330 RVA: 0x0017E3E2 File Offset: 0x0017C5E2
	private static string GetSoundPath(string sound_name)
	{
		return GlobalAssets.GetSound(sound_name, false);
	}
}
