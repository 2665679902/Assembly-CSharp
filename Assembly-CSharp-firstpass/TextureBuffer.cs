using System;
using UnityEngine;

// Token: 0x02000045 RID: 69
public class TextureBuffer
{
	// Token: 0x060002FD RID: 765 RVA: 0x000105D8 File Offset: 0x0000E7D8
	public TextureBuffer(string name, int width, int height, TextureFormat format, FilterMode filter_mode, TexturePagePool pool)
	{
		this.name = name;
		this.format = format;
		this.pool = pool;
		this.texture = new RenderTexture(width, height, 0, TextureUtil.GetRenderTextureFormat(format));
		this.texture.name = name;
		this.texture.filterMode = filter_mode;
		this.texture.wrapMode = TextureWrapMode.Clamp;
		this.material = new Material(Shader.Find("Klei/TexturePage"));
	}

	// Token: 0x060002FE RID: 766 RVA: 0x00010654 File Offset: 0x0000E854
	public TextureRegion Lock(int x, int y, int width, int height)
	{
		TexturePage texturePage = this.pool.Alloc(this.name, width, height, this.format);
		return new TextureRegion(x, y, width, height, texturePage, this);
	}

	// Token: 0x060002FF RID: 767 RVA: 0x00010688 File Offset: 0x0000E888
	public void Unlock(TextureRegion region)
	{
		region.page.texture.Apply();
		this.material.SetVector("_Region", new Vector4((float)region.x / (float)this.texture.width, (float)region.y / (float)this.texture.height, (float)(region.x + region.page.width) / (float)this.texture.width, (float)(region.y + region.page.height) / (float)this.texture.height));
		this.material.SetTexture("_MainTex", region.page.texture);
		Graphics.Blit(region.page.texture, this.texture, this.material);
		this.pool.Release(region.page);
	}

	// Token: 0x040003B8 RID: 952
	public string name;

	// Token: 0x040003B9 RID: 953
	public TexturePagePool pool;

	// Token: 0x040003BA RID: 954
	public TextureFormat format;

	// Token: 0x040003BB RID: 955
	public RenderTexture texture;

	// Token: 0x040003BC RID: 956
	public Material material;
}
