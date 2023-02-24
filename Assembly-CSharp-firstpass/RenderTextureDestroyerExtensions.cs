using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
public static class RenderTextureDestroyerExtensions
{
	// Token: 0x060002FB RID: 763 RVA: 0x0001057E File Offset: 0x0000E77E
	public static void DestroyRenderTexture(this RenderTexture render_texture)
	{
		if (RenderTextureDestroyer.Instance != null)
		{
			RenderTextureDestroyer.Instance.Add(render_texture);
			return;
		}
		UnityEngine.Object.Destroy(render_texture);
	}
}
