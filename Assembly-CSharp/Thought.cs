using System;
using UnityEngine;

// Token: 0x020009A8 RID: 2472
public class Thought : Resource
{
	// Token: 0x0600495B RID: 18779 RVA: 0x0019B0A8 File Offset: 0x001992A8
	public Thought(string id, ResourceSet parent, Sprite icon, string mode_icon, string sound_name, string bubble, string speech_prefix, LocString hover_text, bool show_immediately = false, float show_time = 4f)
		: base(id, parent, null)
	{
		this.sprite = icon;
		if (mode_icon != null)
		{
			this.modeSprite = Assets.GetSprite(mode_icon);
		}
		this.bubbleSprite = Assets.GetSprite(bubble);
		this.sound = sound_name;
		this.speechPrefix = speech_prefix;
		this.hoverText = hover_text;
		this.showImmediately = show_immediately;
		this.showTime = show_time;
	}

	// Token: 0x0600495C RID: 18780 RVA: 0x0019B118 File Offset: 0x00199318
	public Thought(string id, ResourceSet parent, string icon, string mode_icon, string sound_name, string bubble, string speech_prefix, LocString hover_text, bool show_immediately = false, float show_time = 4f)
		: base(id, parent, null)
	{
		this.sprite = Assets.GetSprite(icon);
		if (mode_icon != null)
		{
			this.modeSprite = Assets.GetSprite(mode_icon);
		}
		this.bubbleSprite = Assets.GetSprite(bubble);
		this.sound = sound_name;
		this.speechPrefix = speech_prefix;
		this.hoverText = hover_text;
		this.showImmediately = show_immediately;
		this.showTime = show_time;
	}

	// Token: 0x04003030 RID: 12336
	public int priority;

	// Token: 0x04003031 RID: 12337
	public Sprite sprite;

	// Token: 0x04003032 RID: 12338
	public Sprite modeSprite;

	// Token: 0x04003033 RID: 12339
	public string sound;

	// Token: 0x04003034 RID: 12340
	public Sprite bubbleSprite;

	// Token: 0x04003035 RID: 12341
	public string speechPrefix;

	// Token: 0x04003036 RID: 12342
	public LocString hoverText;

	// Token: 0x04003037 RID: 12343
	public bool showImmediately;

	// Token: 0x04003038 RID: 12344
	public float showTime;
}
