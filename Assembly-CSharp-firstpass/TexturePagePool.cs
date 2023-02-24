using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class TexturePagePool
{
	// Token: 0x06000302 RID: 770 RVA: 0x000107DE File Offset: 0x0000E9DE
	public TexturePagePool()
	{
		this.activePages[0] = new List<TexturePage>();
		this.activePages[1] = new List<TexturePage>();
	}

	// Token: 0x06000303 RID: 771 RVA: 0x00010817 File Offset: 0x0000EA17
	private int Clamp(int value)
	{
		if (value == 0)
		{
			return 32;
		}
		if (value % 32 == 0)
		{
			return value;
		}
		return 32 + value / 32 * 32;
	}

	// Token: 0x06000304 RID: 772 RVA: 0x00010834 File Offset: 0x0000EA34
	public TexturePage Alloc(string name, int width, int height, TextureFormat format)
	{
		int num = this.Clamp(width);
		int num2 = this.Clamp(height);
		int num3 = Time.frameCount % 2;
		foreach (TexturePage texturePage in this.activePages[num3])
		{
			this.freePages.Add(texturePage);
		}
		this.activePages[num3].Clear();
		for (int i = 0; i < this.freePages.Count; i++)
		{
			TexturePage texturePage2 = this.freePages[i];
			if (texturePage2.width == num && texturePage2.height == num2 && texturePage2.format == format)
			{
				this.freePages.RemoveAt(i);
				texturePage2.SetName(name);
				return texturePage2;
			}
		}
		return new TexturePage(name, num, num2, format);
	}

	// Token: 0x06000305 RID: 773 RVA: 0x00010920 File Offset: 0x0000EB20
	public void Release(TexturePage page)
	{
		int num = (Time.frameCount + 1) % 2;
		this.activePages[num].Add(page);
	}

	// Token: 0x040003C2 RID: 962
	private List<TexturePage>[] activePages = new List<TexturePage>[2];

	// Token: 0x040003C3 RID: 963
	private List<TexturePage> freePages = new List<TexturePage>();
}
