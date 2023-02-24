using System;
using UnityEngine;

// Token: 0x02000C2F RID: 3119
[Serializable]
public struct ToggleState
{
	// Token: 0x04004481 RID: 17537
	public string Name;

	// Token: 0x04004482 RID: 17538
	public string on_click_override_sound_path;

	// Token: 0x04004483 RID: 17539
	public string on_release_override_sound_path;

	// Token: 0x04004484 RID: 17540
	public string sound_parameter_name;

	// Token: 0x04004485 RID: 17541
	public float sound_parameter_value;

	// Token: 0x04004486 RID: 17542
	public bool has_sound_parameter;

	// Token: 0x04004487 RID: 17543
	public Sprite sprite;

	// Token: 0x04004488 RID: 17544
	public Color color;

	// Token: 0x04004489 RID: 17545
	public Color color_on_hover;

	// Token: 0x0400448A RID: 17546
	public bool use_color_on_hover;

	// Token: 0x0400448B RID: 17547
	public bool use_rect_margins;

	// Token: 0x0400448C RID: 17548
	public Vector2 rect_margins;

	// Token: 0x0400448D RID: 17549
	public StatePresentationSetting[] additional_display_settings;
}
