using System;
using UnityEngine;

// Token: 0x020009E3 RID: 2531
public class FillRenderTargetEffect : MonoBehaviour
{
	// Token: 0x06004BA3 RID: 19363 RVA: 0x001A918D File Offset: 0x001A738D
	public void SetFillTexture(Texture tex)
	{
		this.fillTexture = tex;
	}

	// Token: 0x06004BA4 RID: 19364 RVA: 0x001A9196 File Offset: 0x001A7396
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(this.fillTexture, null);
	}

	// Token: 0x0400318F RID: 12687
	private Texture fillTexture;
}
