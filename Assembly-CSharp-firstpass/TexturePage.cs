using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class TexturePage
{
	// Token: 0x06000300 RID: 768 RVA: 0x0001076C File Offset: 0x0000E96C
	public TexturePage(string name, int width, int height, TextureFormat format)
	{
		this.width = width;
		this.height = height;
		this.format = format;
		this.texture = new Texture2D(width, height, format, false);
		this.texture.name = name;
		this.texture.filterMode = FilterMode.Point;
		this.texture.wrapMode = TextureWrapMode.Clamp;
		this.SetName(name);
	}

	// Token: 0x06000301 RID: 769 RVA: 0x000107D0 File Offset: 0x0000E9D0
	public void SetName(string name)
	{
		this.texture.name = name;
	}

	// Token: 0x040003BD RID: 957
	public int width;

	// Token: 0x040003BE RID: 958
	public int height;

	// Token: 0x040003BF RID: 959
	public TextureFormat format;

	// Token: 0x040003C0 RID: 960
	public TexturePagePool pool;

	// Token: 0x040003C1 RID: 961
	public Texture2D texture;
}
