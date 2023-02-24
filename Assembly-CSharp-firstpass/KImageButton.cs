using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200005B RID: 91
public class KImageButton : KButton
{
	// Token: 0x17000085 RID: 133
	// (get) Token: 0x0600039D RID: 925 RVA: 0x00012ED5 File Offset: 0x000110D5
	// (set) Token: 0x0600039E RID: 926 RVA: 0x00012EE2 File Offset: 0x000110E2
	public Sprite Sprite
	{
		get
		{
			return this.fgImage.sprite;
		}
		set
		{
			this.fgImage.enabled = value != null;
			this.fgImage.sprite = value;
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x0600039F RID: 927 RVA: 0x00012F02 File Offset: 0x00011102
	// (set) Token: 0x060003A0 RID: 928 RVA: 0x00012F0F File Offset: 0x0001110F
	public Sprite BackgroundSprite
	{
		get
		{
			return this.bgImage.sprite;
		}
		set
		{
			this.bgImage.enabled = value != null;
			this.bgImage.sprite = value;
		}
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00012F2F File Offset: 0x0001112F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.fgImage.enabled = false;
	}

	// Token: 0x04000434 RID: 1076
	public Text text;
}
