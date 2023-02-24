using System;
using UnityEngine;

// Token: 0x02000726 RID: 1830
public class Dream : Resource
{
	// Token: 0x06003219 RID: 12825 RVA: 0x0010C06C File Offset: 0x0010A26C
	public Dream(string id, ResourceSet parent, string background, string[] icons_sprite_names)
		: base(id, parent, null)
	{
		this.Icons = new Sprite[icons_sprite_names.Length];
		this.BackgroundAnim = background;
		for (int i = 0; i < icons_sprite_names.Length; i++)
		{
			this.Icons[i] = Assets.GetSprite(icons_sprite_names[i]);
		}
	}

	// Token: 0x0600321A RID: 12826 RVA: 0x0010C0C8 File Offset: 0x0010A2C8
	public Dream(string id, ResourceSet parent, string background, string[] icons_sprite_names, float durationPerImage)
		: this(id, parent, background, icons_sprite_names)
	{
		this.secondPerImage = durationPerImage;
	}

	// Token: 0x04001E65 RID: 7781
	public string BackgroundAnim;

	// Token: 0x04001E66 RID: 7782
	public Sprite[] Icons;

	// Token: 0x04001E67 RID: 7783
	public float secondPerImage = 2.4f;
}
