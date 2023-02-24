using System;
using UnityEngine;

// Token: 0x020008BA RID: 2234
public class CameraReferenceTexture : MonoBehaviour
{
	// Token: 0x0600404A RID: 16458 RVA: 0x00166F10 File Offset: 0x00165110
	private void OnPreCull()
	{
		if (this.quad == null)
		{
			this.quad = new FullScreenQuad("CameraReferenceTexture", base.GetComponent<Camera>(), this.referenceCamera.GetComponent<CameraRenderTexture>().ShouldFlip());
		}
		if (this.referenceCamera != null)
		{
			this.quad.Draw(this.referenceCamera.GetComponent<CameraRenderTexture>().GetTexture());
		}
	}

	// Token: 0x04002A23 RID: 10787
	public Camera referenceCamera;

	// Token: 0x04002A24 RID: 10788
	private FullScreenQuad quad;
}
