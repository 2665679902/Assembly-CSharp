using System;
using UnityEngine;

// Token: 0x020008B9 RID: 2233
public static class Blur
{
	// Token: 0x06004049 RID: 16457 RVA: 0x00166EE9 File Offset: 0x001650E9
	public static RenderTexture Run(Texture2D image)
	{
		if (Blur.blurMaterial == null)
		{
			Blur.blurMaterial = new Material(Shader.Find("Klei/PostFX/Blur"));
		}
		return null;
	}

	// Token: 0x04002A22 RID: 10786
	private static Material blurMaterial;
}
